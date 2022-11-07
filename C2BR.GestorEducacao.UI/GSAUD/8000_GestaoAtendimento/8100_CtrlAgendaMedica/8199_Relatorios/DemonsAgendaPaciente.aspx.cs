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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class DemonsAgendaPaciente : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/DemonsAgendaPaciente.aspx");
            int Operadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
            int Plano = ddlPlano.SelectedValue != "" ? int.Parse(ddlPlano.SelectedValue) : 0;
            string situacao = ddlSituacao.SelectedValue;

            string infos, parametros;
            int lRetorno, coEmp = LoginAuxili.CO_EMP;
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            int profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;
            int paciente = int.Parse(ddlPaciente.SelectedValue);

            parametros = "( Paciente : " + ddlPaciente.SelectedItem.Text.ToUpper()
                + " - Plano : " + ddlPlano.SelectedItem.Text.ToUpper()
                + " - Profissional : " + ddlProfissional.SelectedItem.Text.ToUpper()
                + " - Situação : " + ddlSituacao.SelectedItem.Text.ToUpper() + " - Período: " + dataIni + " à " + dataFim + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDemonsAgendaPaciente fpcb = new RptDemonsAgendaPaciente();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, Operadora, Plano, profissional, paciente, situacao, dataIni, dataFim, NomeFuncionalidade.ToUpper(), bool.Parse(this.rblSemProc.SelectedValue));

            Session["Report"] = fpcb;
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
                CarregaOperadoras();
                CarregaClassificacoesFuncionais();
                CarregaProfissional();
            }
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true, true, false);
        }

        private void CarregarPlanosSaude(DropDownList ddlOperadora)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, true);
        }

        private void CarregaClassificacoesFuncionais()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncional, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        private void CarregaProfissional()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            string Classificacao = ddlClassFuncional.SelectedValue != "" ? ddlClassFuncional.SelectedValue : "";

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                        && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                        && (Classificacao != "0" ? tb03.CO_CLASS_PROFI == Classificacao : 0 == 0)
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

        private void CarregarPacientes()
        {
            if (ddlProfissional.SelectedValue == "0" && ddlOperadora.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, false);
            else
            {
                int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
                int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
                int Operadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
                int Profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;

                AuxiliCarregamentos.CarregaPacientesAgendamento(ddlPaciente, false, UnidadeDeCadastro, UnidadeDeContrato, Operadora, Profissional);
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

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlOperadora);
            CarregarPacientes();
        }

        protected void ddlClassFuncional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissional();
        }

        protected void ddlProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
        }
    }
}