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
    public partial class DemonEnvioEmailCol : System.Web.UI.Page
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
                CarregaColaboradores();

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
            int strP_CO_EMP, CO_COL;
            string strP_DT_INI, strP_DT_FIM;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            CO_COL = int.Parse(ddlCol.SelectedValue);
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;

            //--------> Prepara a linha de parâmetros
            string dtIni = (strP_DT_INI != "" ? strP_DT_INI : "Todos");
            string dtFim = (strP_DT_FIM != "" ? strP_DT_FIM : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Período inicial: " + dtIni + " - Período final: " + dtFim + ")"; 

            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3060_CtrlEmail/3069_Relatorios/DemonEnvioEmailCol.aspx");
            RptDemonEnvioEmailCol rpt = new RptDemonEnvioEmailCol();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, CO_COL, strP_DT_INI, strP_DT_FIM, NomeFuncionalidadeCadastrada);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        
        /// <summary>
        /// Método responsável por carregar os alunos
        /// </summary>
        private void CarregaColaboradores()
        {
            var res = (from tb403 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb403.CO_EMP == LoginAuxili.CO_EMP
                       select new { tb403.CO_COL, tb403.NO_COL }).OrderBy(w => w.NO_COL).ToList();

            if (res != null)
            {
                ddlCol.DataTextField = "NO_COL";
                ddlCol.DataValueField = "CO_COL";
                ddlCol.DataSource = res;
                ddlCol.DataBind();
            }

            ddlCol.Items.Insert(0, new ListItem("Todos", "0"));
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