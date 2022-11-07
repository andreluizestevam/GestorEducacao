//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Text;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3110_HistoricoSalarioRespon
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
                CarregaUnidades();
                CarregaAssociados();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario()
        {
            CarregaFormulario();
        }
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int intUnidade = ddlUnidade.SelectedValue != "" ? Int32.Parse(ddlUnidade.SelectedValue) : 0;
            int intAssociado = ddlAssociado.SelectedValue != "" ? Int32.Parse(ddlAssociado.SelectedValue) : 0;
            string princiRend = txtPrincipalRendimento.Text.Contains(".") ? txtPrincipalRendimento.Text.Substring(0, txtPrincipalRendimento.Text.IndexOf(",")).Replace(".", "") : txtPrincipalRendimento.Text;
            string princiDescontos = txtPrincipalDescontos.Text.Contains(".") ? txtPrincipalDescontos.Text.Substring(0, txtPrincipalDescontos.Text.IndexOf(",")).Replace(".", "") : txtPrincipalDescontos.Text;
            string princiLiquido = txtPrincipalLiquido.Text.Contains(".") ? txtPrincipalLiquido.Text.Substring(0, txtPrincipalLiquido.Text.IndexOf(",")).Replace(".", "") : txtPrincipalLiquido.Text;
            string extraRend = txtExtraRendimento.Text.Contains(".") ? txtExtraRendimento.Text.Substring(0, txtExtraRendimento.Text.IndexOf(",")).Replace(".", "") : txtExtraRendimento.Text;
            string extraDescontos = txtExtraDescontos.Text.Contains(".") ? txtExtraDescontos.Text.Substring(0, txtExtraDescontos.Text.IndexOf(",")).Replace(".", "") : txtExtraDescontos.Text;
            string extraLiquido = txtExtraLiquido.Text.Contains(".") ? txtExtraLiquido.Text.Substring(0, txtExtraLiquido.Text.IndexOf(",")).Replace(".", "") : txtExtraLiquido.Text;
            TBG151_HISTO_SALAR_RESPO tbg151 = RetornaEntidade();

            if (tbg151 == null)
            {
                tbg151 = new TBG151_HISTO_SALAR_RESPO();
            }
            tbg151.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(intUnidade);
            tbg151.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(intAssociado);
            tbg151.ANO_MES = txtAnoMes.Text.Replace("/","");
            tbg151.VL_PRINC_REND = Int32.Parse(princiRend);
            tbg151.VL_PRINC_DESC = Int32.Parse(princiDescontos);
            tbg151.VL_PRINC_LIQU = Int32.Parse(princiLiquido);
            tbg151.VL_EXTRA_REND = Int32.Parse(extraRend);
            tbg151.VL_EXTRA_DESC = Int32.Parse(extraDescontos);
            tbg151.VL_EXTRA_LIQU = Int32.Parse(extraLiquido);
            tbg151.CO_SITUA = tbg151.ID_HISTO_SALAR_RESPO != 0 ? ddlSituacao.SelectedValue : "A";
            tbg151.DT_SITUA = DateTime.Now;
            tbg151.IP_SITUA = LoginAuxili.IP_USU;
            tbg151.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP,LoginAuxili.CO_COL);

            CurrentCadastroMasterPage.CurrentEntity = tbg151;
        }

        #region "Carregamento"

        void CarregaFormulario()
        {
            TBG151_HISTO_SALAR_RESPO tbg151 = RetornaEntidade(Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));

            if (tbg151 != null)
            {
                hdnIdHistoSalar.Value = tbg151.ID_HISTO_SALAR_RESPO.ToString();
                CarregaEntidade(tbg151);
            }
        }
        /// <summary>
        /// Método que carrega informações dos Itens de Patrimônio selecionado
        /// </summary>
        /// <param name="tbg151">Entidade TBG151_HISTO_SALAR_RESPO</param>
        private void CarregaEntidade(TBG151_HISTO_SALAR_RESPO tbg151)
        {
            liSituacao.Visible = true;

            tbg151.TB108_RESPONSAVELReference.Load();
            tbg151.TB25_EMPRESAReference.Load();

            CarregaUnidades();
            CarregaAssociados();

            ddlUnidade.SelectedValue = tbg151.TB25_EMPRESA.CO_EMP.ToString();
            ddlAssociado.SelectedValue = tbg151.TB108_RESPONSAVEL.CO_RESP.ToString();
            ddlSituacao.SelectedValue = tbg151.CO_SITUA;
            txtAnoMes.Text = tbg151.ANO_MES;
            txtPrincipalRendimento.Text = tbg151.VL_PRINC_REND.ToString();
            txtPrincipalDescontos.Text = tbg151.VL_PRINC_DESC.ToString();
            txtPrincipalLiquido.Text = tbg151.VL_PRINC_LIQU.ToString();
            txtExtraRendimento.Text = tbg151.VL_EXTRA_REND.ToString();
            txtExtraDescontos.Text = tbg151.VL_EXTRA_DESC.ToString();
            txtExtraLiquido.Text = tbg151.VL_EXTRA_LIQU.ToString();
        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBG151_HISTO_SALAR_RESPO</returns>
        private TBG151_HISTO_SALAR_RESPO RetornaEntidade()
        {
            if (QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null)
                return TBG151_HISTO_SALAR_RESPO.RetornaPelaChavePrimaria(decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));
            else
                return TBG151_HISTO_SALAR_RESPO.RetornaPelaChavePrimaria(0);
        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="codPatr">Id do idHistoSalar</param>
        /// <returns>Entidade TBG151_HISTO_SALAR_RESPO</returns>
        private TBG151_HISTO_SALAR_RESPO RetornaEntidade(int idHistoSalar)
        {
            return TBG151_HISTO_SALAR_RESPO.RetornaPelaChavePrimaria(idHistoSalar);
        }     
        //====> Método que carrega o DropDown de Unidades
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }); ;

            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataBind();
        }
        //====> Método que carrega o DropDown de Associados
        private void CarregaAssociados()
        {
            ddlAssociado.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                       select new { tb108.CO_RESP, tb108.NO_RESP }); ;
            ddlAssociado.DataTextField = "NO_RESP";
            ddlAssociado.DataValueField = "CO_RESP";
            ddlAssociado.DataBind();
        }
        #endregion
    }
}