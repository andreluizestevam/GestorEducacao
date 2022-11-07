using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario._8271_Relatorios
{
    public partial class RelProntuario : System.Web.UI.Page
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
                var data = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                CarregaUnidade(ddlUnidadeCadastro);
                CarregaUnidade(ddlUnidadeContrato);
                CarregarPacientes();
                txtDtIniAgenda.Text = txtDtIniEvolucao.Text = data.AddMonths(-1).ToShortDateString();
                txtDtFimAgenda.Text = txtDtFimEvolucao.Text = data.ToShortDateString();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (chkAgenda.Checked && (String.IsNullOrEmpty(txtDtIniAgenda.Text) || String.IsNullOrEmpty(txtDtFimAgenda.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor informe o periódo do agendamento!");
                return;
            }

            if (chkEvolucao.Checked && (String.IsNullOrEmpty(txtDtIniEvolucao.Text) || String.IsNullOrEmpty(txtDtFimEvolucao.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor informe o periódo da evolução!");
                return;
            }

            string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8270_Prontuario/8271_Relatorios/RelProntuario.aspx");

            string infos;
            int lRetorno, coEmp = LoginAuxili.CO_EMP;
            int paciente = int.Parse(ddlPaciente.SelectedValue);

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptProntuario rpt = new RptProntuario();
            lRetorno = rpt.InitReport(infos, coEmp, paciente, chkAnamnese.Checked, chkAgenda.Checked, txtDtIniAgenda.Text, txtDtFimAgenda.Text, chkEvolucao.Checked, txtDtIniEvolucao.Text, txtDtFimEvolucao.Text, titulo.ToUpper());

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregarPacientes()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                       where (UnidadeDeCadastro != 0 ? tb07.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb07.CO_EMP_ORIGEM == UnidadeDeContrato : 0 == 0)
                        //&& tbs174.CO_SITUA_AGEND_HORAR != "R" && tbs174.CO_SITUA_AGEND_HORAR != "C"
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void dllUnidadeCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
        }

        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
        }
    }
}