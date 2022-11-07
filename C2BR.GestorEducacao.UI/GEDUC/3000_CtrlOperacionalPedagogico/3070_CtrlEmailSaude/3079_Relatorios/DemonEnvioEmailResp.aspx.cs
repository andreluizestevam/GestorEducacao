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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3070_CtrlEmailSaude._3079_Relatorios
{
    public partial class DemonEnvioEmailResp : System.Web.UI.Page
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
                CarregaResponsavel();

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
            DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            CO_COL = int.Parse(ddlCol.SelectedValue);
            strP_DT_INI = Convert.ToDateTime(txtDataPeriodoIni.Text);
            strP_DT_FIM = Convert.ToDateTime(txtDataPeriodoFim.Text);

            //--------> Prepara a linha de parâmetros
            string dtIni = (strP_DT_INI.ToString() != "" ? strP_DT_INI.ToString() : "Todos");
            string dtFim = (strP_DT_FIM.ToString() != "" ? strP_DT_FIM.ToString() : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Período inicial: " + dtIni + " - Período final: " + dtFim + ")"; 

            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3070_CtrlEmailSaude/3079_Relatorios/DemonEnvioEmailResp.aspx");
            RptDemonEnvioEmailResp rpt = new RptDemonEnvioEmailResp();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, CO_COL, NomeFuncionalidadeCadastrada, strP_DT_INI, strP_DT_FIM);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        
        /// <summary>
        /// Método responsável por carregar os alunos
        /// </summary>
        private void CarregaResponsavel()
        {
            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       select new { tb108.CO_RESP, tb108.NO_RESP }).OrderBy(w => w.NO_RESP).ToList();

            if (res != null)
            {
                ddlCol.DataTextField = "NO_RESP";
                ddlCol.DataValueField = "CO_RESP";
                ddlCol.DataSource = res;
                ddlCol.DataBind();
            }

            ddlCol.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region SelectedIndexChanged

        protected void ddlParce_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            PadraoRelatoriosCorrente_OnAcaoGeraRelatorio();
        }


    }
}