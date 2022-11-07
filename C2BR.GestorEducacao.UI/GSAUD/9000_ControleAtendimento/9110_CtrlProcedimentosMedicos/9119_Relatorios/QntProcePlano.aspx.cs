using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9119_Relatorios
{
    public partial class QntProcePlano : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (String.IsNullOrEmpty(txtIniPeri.Text) || String.IsNullOrEmpty(txtFimPeri.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, insira um período.");
                return;
            }

            var operadora = int.Parse(ddlOperadora.SelectedValue);
            var plano = int.Parse(ddlPlano.SelectedValue);
            var unid = int.Parse(ddlUnidade.SelectedValue);
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;
            var dtInical = !String.IsNullOrEmpty(txtIniPeri.Text) ? Convert.ToDateTime(txtIniPeri.Text) : DateTime.Now;
            var dtFinal = !String.IsNullOrEmpty(txtFimPeri.Text) ? Convert.ToDateTime(txtFimPeri.Text) : DateTime.Now;

            string nmFunc = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/9000_ControleAtendimento/9110_CtrlProcedimentosMedicos/9119_Relatorios/QntProceAmbul.aspx");

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var parametros = "(Unidade : " + ddlUnidade.SelectedItem.Text.ToUpper()
                            + " - Procedimento : " + ddlProcedimento.SelectedItem.Text.ToUpper()
                           + " - Período: " + txtIniPeri.Text + " à " + txtFimPeri.Text + " )";

            RptQntProcePlano rpt = new RptQntProcePlano();
            var lRetorno = rpt.InitReport(nmFunc, parametros, infos, unid, idProc, operadora, plano, dtInical, dtFinal);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaOperadora();
                CarregaPlano();
                CarregaProcedimentos();
            }
        }

        private void CarregaOperadora()
        {
            ddlOperadora.Items.Clear();

            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);            
        }

        private void CarregaPlano()
        {
            ddlPlano.Items.Clear();

            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, true);
        }

        private void CarregaUnidade()
        {
            ddlUnidade.Items.Clear();

            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaProcedimentos()
        {
            ddlProcedimento.Items.Clear();

            CarregarProcedimentos(ddlProcedimento, int.Parse(ddlOperadora.SelectedValue));
        }

        /// <summary>
        /// Carrega os procedimentos em dropdownlist
        /// </summary>
        /// <param name="ddlprocp"></param>
        private void CarregarProcedimentos(DropDownList ddlprocp, int oper = 0, string tipoProc = null)
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlprocp, oper, true, true, tipoProc, true);
        }

        public void OnSelectedIndexChanged_Operadora(object sender, EventArgs e)
        {
            CarregaPlano();
            CarregaProcedimentos();
        }

    }
}