using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios
{
    public partial class RelatorioPlanoTratamento : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            if (String.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um paciente para gerar o relatório.");
                return;
            }

            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8299_Relatorios/RelatorioPlanoTratamento.aspx");

            string infos, parametros;
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            int profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;
            int paciente = !String.IsNullOrEmpty(ddlPaciente.SelectedValue) ? int.Parse(ddlPaciente.SelectedValue) : 0;
            int local = int.Parse(ddlLocal.SelectedValue);
            int planTrat = int.Parse(ddlLocal.SelectedValue);


            parametros = "( Paciente : " + ddlPaciente.SelectedItem.Text.ToUpper()
                        + " - Profissional : " + ddlProfissional.SelectedItem.Text.ToUpper()
                        + " - Local : " + ddlLocal.SelectedItem.Text.ToUpper()
                        + " - Plano Tratamento : " + ddlPlanoTrat.SelectedItem.Text.ToUpper()
                        + " - Período: " + dataIni + " à " + dataFim + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptPlanoTratamento rpt = new RptPlanoTratamento();
            var lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, local, profissional, paciente, planTrat, dataIni, dataFim, NomeFuncionalidade.ToUpper());

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dtIni = DateTime.Now;
                var dtFim = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                {
                    dtIni = LoginAuxili.DATA_INICIO_USU_DEMO;
                    dtFim = LoginAuxili.DATA_FINAL_USU_DEMO;
                }

                IniPeri.Text = dtIni.ToShortDateString();
                FimPeri.Text = dtFim.ToShortDateString();

                CarregaUnidade(ddlUnidadeCadastro);
                CarregaUnidade(ddlUnidadeContrato);
                CarregaLocal();
                CarregaProfissional();
                CarregaPlanoTratamento();
            }
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNomePacPesq.Text))
            {
                int unidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
                int unidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
                int profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_EMP equals tbs174.CO_EMP
                           where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                               && (unidadeDeCadastro != 0 ? tb07.CO_EMP == unidadeDeCadastro : 0 == 0)
                               && (profissional != 0 ? tbs174.CO_COL == profissional : 0 == 0)
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

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaPlanoTratamento()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            int coAlu = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            int coProfi = ddlProfissional.SelectedValue != "0" ? int.Parse(ddlProfissional.SelectedValue) : 0;

            var res = (from tbs458 in TBS458_TRATA_PLANO.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs458.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                       where (coAlu != 0 ? tbs174.CO_ALU == coAlu : 0 == 0)
                       && (coProfi != 0 ? tbs458.CO_COL_CADAS == coProfi : 0 == 0)
                       && tbs458.CO_SITUA.Equals("A")
                       select new PlanoTrat { nuRegis = tbs458.NU_REGIS, IDTrata = tbs458.ID_TRATA_PLANO, Local = tb14.CO_SIGLA_DEPTO, DTTrata = tbs458.DT_CADAS }).ToList();

            ddlPlanoTrat.Items.Clear();

            if (res.Count > 0)
            {
                ddlPlanoTrat.DataValueField = "IDTrata";
                ddlPlanoTrat.DataTextField = "NMTrata";
                ddlPlanoTrat.DataSource = res;
                ddlPlanoTrat.DataBind();
            }

            ddlPlanoTrat.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private class PlanoTrat
        {
            public string nuRegis { get; set; }
            public DateTime DTTrata { get; set; }
            public string Local { get; set; }
            public string NMTrata
            {
                get
                {
                    return this.nuRegis + " - " + this.DTTrata.ToString("dd/MM/yyyy HH:mm") + " - " + this.Local;
                }
            }
            public int IDTrata { get; set; }
        }

        private void CarregaProfissional()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                        && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            ddlProfissional.Items.Clear();

            if (res.Count > 0)
            {
                ddlProfissional.DataValueField = "CO_COL";
                ddlProfissional.DataTextField = "NO_COL";
                ddlProfissional.DataSource = res;
                ddlProfissional.DataBind();
                ddlProfissional.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
                ddlProfissional.Items.Insert(0, new ListItem("Todos", "0"));

            CarregarPacientes();
        }

        private void CarregaLocal()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.CO_SITUA_DEPTO.Equals("A")
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).OrderBy(W => W.NO_DEPTO).ToList();
            ddlLocal.Items.Clear();
            if (res.Count > 0)
            {
                ddlLocal.DataValueField = "CO_DEPTO";
                ddlLocal.DataTextField = "NO_DEPTO";
                ddlLocal.DataSource = res;
                ddlLocal.DataBind();
                ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
                ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CarregarPacientes()
        {
            if (ddlProfissional.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, false);
            else
            {
                int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
                int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
                int Profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                           where (UnidadeDeCadastro != 0 ? tb07.CO_EMP == UnidadeDeCadastro : 0 == 0)
                            && (Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0)
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
        }

        protected void dllUnidadeCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissional();
        }

        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissional();
        }

        protected void ddlProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
            CarregaPlanoTratamento();
        }

        protected void ddlPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanoTratamento();
        }
    }
}