using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5300_CtrlGerencial;

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5300_CtrlGerencial._5399_Relatorios
{
    public partial class AtendimentosUnidades : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AuxiliCarregamentos.CarregaUFs(drpUF, false, null, false);
                CarregarCidades();
                CarregarUnidades();
                drpMesReferencia.SelectedValue = DateTime.Now.Month.ToString();
                txtAno.Text = DateTime.Now.Year.ToString();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/5000_CtrlFinanceira/5300_CtrlGerencial/5399_Relatorios/AtendimentosUnidades.aspx");

            string infos, parametros;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "( UF : " + drpUF.SelectedItem.Text.ToUpper()
                + " - Cidade : " + drpCidade.SelectedItem.Text.ToUpper()
                + " - Unidade: " + drpUnidade.SelectedItem.Text.ToUpper()
                + " - Mês: " + drpMesReferencia.SelectedItem.Text.ToUpper() 
                + " - Ano: " + txtAno.Text + " )";

            var uf = !String.IsNullOrEmpty(drpUF.SelectedValue) ? drpUF.SelectedValue : LoginAuxili.CO_UF_EMP;
            var cidade = !String.IsNullOrEmpty(drpCidade.SelectedValue) ? int.Parse(drpCidade.SelectedValue) : 0;
            var unidade = !String.IsNullOrEmpty(drpUnidade.SelectedValue) ? int.Parse(drpUnidade.SelectedValue) : 0;
            var mes = !String.IsNullOrEmpty(drpMesReferencia.SelectedValue) ? int.Parse(drpMesReferencia.SelectedValue) : DateTime.Now.Month;
            var ano = !String.IsNullOrEmpty(txtAno.Text) ? int.Parse(txtAno.Text) : DateTime.Now.Year;

            RptAtendimentosUnid rpt = new RptAtendimentosUnid();
            var lRetorno = rpt.InitReport(titulo, infos, parametros, LoginAuxili.CO_EMP, uf, cidade, unidade, mes, ano);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        private void CarregarCidades()
        {
            string uf = drpUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(drpCidade, true, uf, LoginAuxili.CO_EMP, true, true);
        }

        private void CarregarUnidades()
        {
            var cidade = !String.IsNullOrEmpty(drpCidade.SelectedValue) ? int.Parse(drpCidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaUnidade(drpUnidade, LoginAuxili.ORG_CODIGO_ORGAO, cidade, true);
        }

        protected void drpUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarCidades();
            CarregarUnidades();
        }

        protected void drpCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarUnidades();
        }
    }
}