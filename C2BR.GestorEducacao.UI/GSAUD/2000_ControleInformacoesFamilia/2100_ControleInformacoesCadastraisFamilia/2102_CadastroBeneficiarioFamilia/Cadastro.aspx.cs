//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Artem.Web.UI.Controls;


namespace C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2102_CadastroBeneficiarioFamilia
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaFamilias();
                CarregaBeneficiarios();
                CarregaGrau();

                int intFamilia = ddlFamilia.SelectedValue != "" ? Int32.Parse(ddlFamilia.SelectedValue) : 0;

                txtCodigo.Text = TB075_FAMILIA.RetornaPelaChavePrimaria(intFamilia).CO_FAMILIA;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario()
        {
            CarregaFormulario();
        }

        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int intFamilia = ddlFamilia.SelectedValue != "" ? Int32.Parse(ddlFamilia.SelectedValue) : 0;
            int intBeneficiario = ddlBeneficiario.SelectedValue != "" ? Int32.Parse(ddlBeneficiario.SelectedValue) : 0;
            int intEmp = TB07_ALUNO.RetornaPeloCoAlu(intBeneficiario).CO_EMP;
          
            TBG076_FAMIL_BENEF tbg76 = RetornaEntidade();

            if (tbg76 == null)
            {
                tbg76 = new TBG076_FAMIL_BENEF();
            }

            tbg76.TB075_FAMILIA = TB075_FAMILIA.RetornaPelaChavePrimaria(intFamilia);
            tbg76.TP_BENEF_FAMIL = "A";
            tbg76.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(intEmp);
            tbg76.CO_BENEF_FAMIL = intBeneficiario;
            tbg76.CO_GRAU_PAREN = ddlGrau.SelectedValue;
            tbg76.DE_OBSER_BENEF = txtObs.Text;
            tbg76.CO_SITUA = tbg76.ID_FAMIL_BENEF != 0 ? ddlSituacao.SelectedValue : "A";
            tbg76.DT_SITUA = DateTime.Now;
            tbg76.IP_SITUA = LoginAuxili.IP_USU;
            tbg76.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

            CurrentCadastroMasterPage.CurrentEntity = tbg76;
        }

        #region "Carregamento"

        void CarregaFormulario()
        {
            TBG076_FAMIL_BENEF tbg76 = RetornaEntidade(Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));

            if (tbg76 != null)
            {
                CarregaEntidade(tbg76);
            }
        }
        /// <summary>
        /// Método que carrega informações dos Itens de Patrimônio selecionado
        /// </summary>
        /// <param name="tbg76">Entidade TBG076_FAMIL_BENEF</param>
        private void CarregaEntidade(TBG076_FAMIL_BENEF tbg76)
        {
            liSituacao.Visible = true;

            tbg76.TB075_FAMILIAReference.Load();
            tbg76.TB25_EMPRESAReference.Load();

            CarregaFamilias();
            CarregaBeneficiarios();
            CarregaGrau();

            txtCodigo.Text = tbg76.TB075_FAMILIA.CO_FAMILIA;
            txtCodigo.Enabled = false;
            ddlFamilia.SelectedValue = tbg76.TB075_FAMILIA.CO_FAMILIA.ToString();
            ddlBeneficiario.SelectedValue = tbg76.CO_BENEF_FAMIL.ToString();
            ddlGrau.SelectedValue = tbg76.CO_GRAU_PAREN;
            txtObs.Text = tbg76.DE_OBSER_BENEF;
            ddlSituacao.SelectedValue = tbg76.CO_SITUA;         
        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBG076_FAMIL_BENEF</returns>
        private TBG076_FAMIL_BENEF RetornaEntidade()
        {
            if (QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null)
                return TBG076_FAMIL_BENEF.RetornaPelaChavePrimaria(decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));
            else
                return TBG076_FAMIL_BENEF.RetornaPelaChavePrimaria(0);
        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="idBenefFamil">Id do beneficiario da familia</param>
        /// <returns>Entidade TBG076_FAMIL_BENEF</returns>
        private TBG076_FAMIL_BENEF RetornaEntidade(int idBenefFamil)
        {
            return TBG076_FAMIL_BENEF.RetornaPelaChavePrimaria(idBenefFamil);
        }
        //====> Método que carrega o DropDown de Famílias
        private void CarregaFamilias()
        {
            ddlFamilia.DataSource = (from tb75 in TB075_FAMILIA.RetornaTodosRegistros()
                                     select new { tb75.ID_FAMILIA, tb75.NO_RESP_FAM }); ;

            ddlFamilia.DataValueField = "ID_FAMILIA";
            ddlFamilia.DataTextField = "NO_RESP_FAM";
            ddlFamilia.DataBind();
        }
        //====> Método que carrega o DropDown de Associados
        private void CarregaBeneficiarios()
        {
            int intFamilia = ddlFamilia.SelectedValue != "" ? Int32.Parse(ddlFamilia.SelectedValue) : 0;
            ddlBeneficiario.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                          where tb07.TB075_FAMILIA.ID_FAMILIA == intFamilia
                                       select new { tb07.CO_ALU, tb07.NO_ALU }); ;
            ddlBeneficiario.DataTextField = "NO_ALU";
            ddlBeneficiario.DataValueField = "CO_ALU";
            ddlBeneficiario.DataBind();
        }

        private void CarregaGrau()
        {
            //ddlGrau.Items.Clear();
            //ddlGrau.Items.Insert(0, new ListItem("Pai/Mãe", ParentescoResponsavel.PM.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Avô/Avó", ParentescoResponsavel.AV.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Filho(a)", ParentescoResponsavel.FI.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Enteado(a)", ParentescoResponsavel.EN.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Neto(a)", ParentescoResponsavel.NE.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Irmão(a)", ParentescoResponsavel.IR.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Sobrinho(a)", ParentescoResponsavel.SO.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Tio(a)", ParentescoResponsavel.TI.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Primo(a)", ParentescoResponsavel.PR.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Tutor(a)", ParentescoResponsavel.TU.ToString()));
            //ddlGrau.Items.Insert(0, new ListItem("Selecione", "0"));

            ddlGrau.DataSource = (from tbGrau in TBTIPO_GRAU_PARENT.RetornaTodosRegistros()
                                  select new { tbGrau.CO_SIGLA_GRAU, tbGrau.NO_GRAU }); ;
            ddlGrau.DataTextField = "NO_GRAU";
            ddlGrau.DataValueField = "CO_SIGLA_GRAU";
            ddlGrau.DataBind();
        }
        #endregion

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBeneficiarios();

            int intFamilia = ddlFamilia.SelectedValue != "" ? Int32.Parse(ddlFamilia.SelectedValue) : 0;

            txtCodigo.Text = TB075_FAMILIA.RetornaPelaChavePrimaria(intFamilia).CO_FAMILIA;
        }
    }
}