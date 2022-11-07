
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


namespace C2BR.GestorEducacao.UI.GEDUC._6000_CtrlProcessosInternos._6300_CtrlOperPatrimonio._6310_CtrlManutencaoItensPatrimonio._6312_RegistroCargaItemPatrimonio
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
                CarregaDropDowns();
                CarregaCodigo();
                CarregaUnidades();
                CarregaTipoPatrimonio();
                CarregaColaboradores();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario()
        {
            CarregaFormulario();
        }
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int intTipoPatrimonio = ddlTipoPatrimonio.SelectedValue != "" ? Int32.Parse(ddlTipoPatrimonio.SelectedValue) : 0;
            long intCodigoPatrimonio = ddlCodigo.SelectedValue != "" ? Int64.Parse(ddlCodigo.SelectedValue) : 0;
            TB232_PATRI_CARGA_ITEM tb232 = RetornaEntidade();

            if (tb232 == null)
            {
                tb232 = new TB232_PATRI_CARGA_ITEM();
            }
            tb232.TB213_CLASSIF_PATR = TB213_CLASSIF_PATR.RetornaPelaChavePrimaria(intTipoPatrimonio);
            tb232.TB212_ITENS_PATRIMONIO = TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(intCodigoPatrimonio);
            tb232.CO_EMP_CARGA = Int32.Parse(ddlUnidadePatrimonio.SelectedValue);
            tb232.DH_CARGA_PATR = RetornaDateTime(txtDataCargaPatrimonio.Text, txtHoraCargaPatrimonio.Text);
            tb232.CO_EMP_COL_CARGA = Int32.Parse(ddlUnidadeColaborador.SelectedValue);
            tb232.CO_COL_CARGA = Int32.Parse(ddlColaborador.SelectedValue);
            tb232.TP_CARGA_PATR = ddlTipoCargaPatrimonio.SelectedValue;
            tb232.DE_OBSER_CARGA = txtDescPatrimonio.Text;
            tb232.CO_SITUA_STATU = ddlSituacaoStatus.SelectedValue;
            tb232.DT_SITUA_DATA = DateTime.UtcNow;
            tb232.CO_SITUA_RESPO = Int32.Parse(ddlSituacaoResponsavel.SelectedValue);
            tb232.NR_IP_CARGA_PATR = LoginAuxili.IP_USU;           

            CurrentCadastroMasterPage.CurrentEntity = tb232;
        }

        #region "Carregamento"

        void CarregaFormulario()
        {
            TB232_PATRI_CARGA_ITEM tb232 = RetornaEntidade(Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));

            if (tb232 != null)
            {
                hdnIdCargaPatr.Value = tb232.ID_CARGA_PATR.ToString();
                CarregaCargaPatrimonio(tb232);
            }
        }
        /// <summary>
        /// Método que carrega informações dos Itens de Patrimônio selecionado
        /// </summary>
        /// <param name="tb212">Entidade TB232_PATRI_CARGA_ITEM</param>
        private void CarregaCargaPatrimonio(TB232_PATRI_CARGA_ITEM tb232)
        {
            tb232.TB212_ITENS_PATRIMONIOReference.Load();
            tb232.TB213_CLASSIF_PATRReference.Load();

            CarregaDropDowns();
            CarregaCodigo();
            CarregaUnidades();
            CarregaTipoPatrimonio();
            CarregaColaboradores();

            ddlCodigo.SelectedValue = tb232.TB212_ITENS_PATRIMONIO.COD_PATR.ToString();
            txtDataCargaPatrimonio.Text = tb232.DH_CARGA_PATR.ToString("dd/MM/yyyy");
            ddlUnidadePatrimonio.SelectedValue = tb232.CO_EMP_CARGA.ToString();
            ddlTipoPatrimonio.SelectedValue = tb232.TB213_CLASSIF_PATR.CO_CLASSIF_PATR.ToString();
            ddlUnidadeColaborador.SelectedValue = tb232.CO_EMP_COL_CARGA.ToString();
            ddlColaborador.SelectedValue = tb232.CO_COL_CARGA.ToString();
            ddlTipoCargaPatrimonio.SelectedValue = tb232.TP_CARGA_PATR;
            txtDescPatrimonio.Text = tb232.DE_OBSER_CARGA;
            ddlSituacaoStatus.SelectedValue = tb232.CO_SITUA_STATU.ToString();
            ddlSituacaoResponsavel.SelectedValue = tb232.CO_SITUA_RESPO.ToString();
            txtHoraCargaPatrimonio.Text = RetornaHora(tb232.DH_CARGA_PATR);

        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB232_PATRI_CARGA_ITEM</returns>
        private TB232_PATRI_CARGA_ITEM RetornaEntidade()
        {
            if (QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null)
                return TB232_PATRI_CARGA_ITEM.RetornaPelaChavePrimaria(decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));
            else
                return TB232_PATRI_CARGA_ITEM.RetornaPelaChavePrimaria(0);
        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="codPatr">Id do patrimônio</param>
        /// <returns>Entidade TB232_PATRI_CARGA_ITEM</returns>
        private TB232_PATRI_CARGA_ITEM RetornaEntidade(int idCargaPatr)
        {
            return TB232_PATRI_CARGA_ITEM.RetornaPelaChavePrimaria(idCargaPatr);
        }
        //====> Método que carrega os DropDowns estatícos
        private void CarregaDropDowns()
        {
            ddlTipoCargaPatrimonio.Items.Insert(0, new ListItem("Temporária", "T"));
            ddlTipoCargaPatrimonio.Items.Insert(1, new ListItem("Definitiva", "D"));
            ddlTipoCargaPatrimonio.Items.Insert(2, new ListItem("Outras", "O"));

            ddlSituacaoStatus.Items.Insert(0, new ListItem("Ativa", "A"));
            ddlSituacaoStatus.Items.Insert(1, new ListItem("Inativa", "I"));
            ddlSituacaoStatus.Items.Insert(2, new ListItem("Cancelada", "C"));
            ddlSituacaoStatus.Items.Insert(2, new ListItem("Repassada", "R"));
        }
        private void CarregaCodigo()
        {
            ddlCodigo.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                    select new { tb212.COD_PATR, tb212.NOM_PATR });

            ddlCodigo.DataValueField = "COD_PATR";
            ddlCodigo.DataTextField = "NOM_PATR";
            ddlCodigo.DataBind();
        }
        private void CarregaTipoPatrimonio()
        {
            ddlTipoPatrimonio.DataSource = (from tb213 in TB213_CLASSIF_PATR.RetornaTodosRegistros()
                                            select new { tb213.CO_CLASSIF_PATR, tb213.NO_CLASSIF_PATR });

            ddlTipoPatrimonio.DataValueField = "CO_CLASSIF_PATR";
            ddlTipoPatrimonio.DataTextField = "NO_CLASSIF_PATR";
            ddlTipoPatrimonio.DataBind();
        }
        //====> Método que carrega o DropDown de Unidades de Patrimonio
        private void CarregaUnidades()
        {
            var ds = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                      select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });
            ddlUnidadePatrimonio.DataSource = ds;

            ddlUnidadePatrimonio.DataValueField = "CO_EMP";
            ddlUnidadePatrimonio.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadePatrimonio.DataBind();

            ddlUnidadeColaborador.DataSource = ds;

            ddlUnidadeColaborador.DataValueField = "CO_EMP";
            ddlUnidadeColaborador.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeColaborador.DataBind();
        }

        //====> Método que carrega o DropDown de Colaboradores
        private void CarregaColaboradores()
        {
            int idUnidadeColaborador = ddlUnidadeColaborador.SelectedValue != "" ? int.Parse(ddlUnidadeColaborador.SelectedValue) : 0;
            var ds = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_EMP.Equals(idUnidadeColaborador)
                      select new { tb03.CO_COL, tb03.NO_COL });
            ddlColaborador.DataSource = ds;
            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlSituacaoResponsavel.DataSource = ds;
            ddlSituacaoResponsavel.DataTextField = "NO_COL";
            ddlSituacaoResponsavel.DataValueField = "CO_COL";
            ddlSituacaoResponsavel.DataBind();
        }
        #endregion

        protected void ddlUnidadeColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        private DateTime RetornaDateTime(string data, string hora)
        {
            var dia = Int32.Parse(data.Substring(0, 2));
            var mes = Int32.Parse(data.Substring(data.IndexOf("/") + 1, 2));
            var ano = Int32.Parse(data.Substring(data.LastIndexOf("/") + 1, 4));
            var horas = Int32.Parse(hora.Substring(0, 2));
            var minutos = Int32.Parse(hora.Substring(hora.IndexOf(":") + 1, 2));
            return new DateTime(ano, mes, dia, horas, minutos, 0);
        }

        private string RetornaHora(DateTime data)
        {
            var strData = data.ToString();
            return strData.Substring(strData.IndexOf(" ")+1);
        }
    }
}