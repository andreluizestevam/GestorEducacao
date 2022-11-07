using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3050_CtrlEmail;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3060_CtrlEmailInstitucional._3069_Relatorios
{
    public partial class DemonEnvioEmailParce : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaParceiros();

            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            imprime();
        }

        private void imprime()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros;
            int strP_CO_EMP, CO_PARCE;
            string strP_DT_INI, strP_DT_FIM;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            CO_PARCE = int.Parse(ddlParce.SelectedValue);
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;

            //--------> Prepara a linha de parâmetros
            string dtIni = (strP_DT_INI != "" ? strP_DT_INI : "Todos");
            string dtFim = (strP_DT_FIM != "" ? strP_DT_FIM : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Período inicial: " + dtIni + " - Período final: " + dtFim + ")"; 

            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3060_CtrlEmail/3069_Relatorios/DemonEnvioEmailParce.aspx");
            RptDemonEnvioEmailParce rpt = new RptDemonEnvioEmailParce();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, CO_PARCE, strP_DT_INI, strP_DT_FIM, NomeFuncionalidadeCadastrada);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        
        /// <summary>
        /// Método responsável por carregar os alunos
        /// </summary>
        private void CarregaParceiros()
        {
            var res = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                       where tb421.CO_COL_INDIC_PARCE == LoginAuxili.CO_EMP
                       select new { tb421.CO_PARCE, tb421.DE_RAZSOC_PARCE }).OrderBy(w => w.DE_RAZSOC_PARCE).ToList();

            if (res != null)
            {
                ddlParce.DataTextField = "DE_RAZSOC_PARCE";
                ddlParce.DataValueField = "CO_PARCE";
                ddlParce.DataSource = res;
                ddlParce.DataBind();
            }

            ddlParce.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region SelectedIndexChanged

        protected void ddlParce_SelectedIndexChanged(Object sender, EventArgs e)
        {
            
        }

        #endregion

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            PadraoRelatoriosCorrente_OnAcaoGeraRelatorio();
        }


    }
}