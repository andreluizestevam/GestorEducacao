using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas;

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios
{
    public partial class HistoricoFinanceiro : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string infos, titulo, parametros;
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            int paciente = int.Parse(ddlPaciente.SelectedValue);

            parametros = "( Paciente : " + ddlPaciente.SelectedItem.Text.ToUpper()
                + " - Tipo : " + drpTipoRecebimento.SelectedItem.Text.ToUpper()
                + " - Contratação : " + drpContratacao.SelectedItem.Text.ToUpper()
                + " - Cortesia : " + drpCortesia.SelectedItem.Text.ToUpper()
                + " - Nota Fiscal : " + drpNotaFiscal.SelectedItem.Text.ToUpper() 
                + " - Período: " + dataIni + " à " + dataFim + " )";

            titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/5000_CtrlFinanceira/5299_Relatorios/HistoricoFinanceiro.aspx");
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptHistoricoRecebimento rpt = new RptHistoricoRecebimento();
            var lRetorno = rpt.InitReport(titulo, parametros, infos, LoginAuxili.CO_EMP, paciente, drpTipoRecebimento.SelectedValue, int.Parse(drpContratacao.SelectedValue), int.Parse(drpPlano.SelectedValue), drpCortesia.SelectedValue, drpNotaFiscal.SelectedValue, dataIni, dataFim);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));

            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePacPesq.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlPaciente.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPeri.Text = DateTime.Now.ToShortDateString();
                FimPeri.Text = DateTime.Now.ToShortDateString();

                CarregaOperadoras(drpContratacao);
                CarregaUnidade(ddlUnidade);
                CarregarPacientes();
            }
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregarPacientes()
        {
            if (ddlUnidade.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, true);
            else
                AuxiliCarregamentos.CarregaPacientes(ddlPaciente, int.Parse(ddlUnidade.SelectedValue), true);
        }

        private void CarregaOperadoras(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(drop, true, true);
        }

        protected void dllUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
        }

        protected void drpContratacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(drpPlano, drpContratacao, true, true);
        }
    }
}