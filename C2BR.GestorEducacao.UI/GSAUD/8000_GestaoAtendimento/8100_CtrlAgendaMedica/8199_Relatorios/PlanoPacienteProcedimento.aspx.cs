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
    public partial class PlanoPacienteProcedimento : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }
        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/PlanoPacienteProcedimento.aspx");
            int Operadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
            int Plano = ddlPlano.SelectedValue != "" ? int.Parse(ddlPlano.SelectedValue) : 0;
            string situacao = ddlSituacaoAlu.SelectedValue;

            string infos, parametros, tb;
            string oper;
            int lRetorno, coEmp = LoginAuxili.CO_EMP;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            int CoUnidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int profissional = int.Parse(ddlProfissional.SelectedValue);
            int paciente =ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            string Periodo = dataIni + " à " + dataFim;

            if (CoUnidade != 0)
            {
                tb = TB25_EMPRESA.RetornaPelaChavePrimaria(CoUnidade).sigla;
            }
            else
            {
                tb = "Todos";
            }
            if (Operadora != 0)
            {
                oper = TB250_OPERA.RetornaPelaChavePrimaria(Operadora).NM_SIGLA_OPER.ToString();
            }
            else
            {
                oper = "Todos";
            }
            parametros = "( Unidade : " + tb.ToUpper()
                + " - Operadora: " + oper.ToUpper() + " - Plano: "
                + ddlPlano.SelectedItem.Text.ToUpper() + " -  Profissional: " + ddlProfissional.SelectedItem.Text.ToUpper()
                + " - Situação : " + ddlSituacaoAlu.SelectedItem.Text.ToUpper() + "  - Período: " + Periodo.ToUpper() + " )";
            RptPlanoPacienteProced fpcb = new RptPlanoPacienteProced();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, Operadora, Plano, profissional, situacao, paciente, dataIni, dataFim, NomeFuncionalidade.ToUpper(), bool.Parse(this.rblSemProc.SelectedValue));

            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginAuxili.FLA_USR_DEMO)
                {
                    IniPeri.Text = LoginAuxili.DATA_INICIO_USU_DEMO.ToShortDateString();
                    FimPeri.Text = LoginAuxili.DATA_FINAL_USU_DEMO.ToShortDateString();
                }

                CarregaOperadoras();
                CarregaUnidade();
                CarregaProfissionaisSaude();
            }
        }

        #region Paciente

        //Pesquisa pacientes por textbox e depois dropdown baseado na pesquisa

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            int operadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
            int profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                           && (operadora != 0 ? tb07.TB250_OPERA.ID_OPER == operadora : 0 == 0)
                           && (profissional != 0 ? tbs174.CO_COL == profissional : 0 == 0)
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

        #endregion

        private void CarregarPacientes()
        {
            if (ddlProfissional.SelectedValue == "0" && ddlOperadora.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, true);
            else
            {
                int Operadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
                int Profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                           where (Operadora != 0 ? tb07.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                            && (Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0)
                           select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

                if (res != null)
                {
                    ddlPaciente.DataTextField = "NO_ALU";
                    ddlPaciente.DataValueField = "CO_ALU";
                    ddlPaciente.DataSource = res;
                    ddlPaciente.DataBind();
                }

                ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true, true, false);

        }

        private void CarregarPlanosSaude(DropDownList ddlOperadora)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, true);
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            CarregaProfissionaisSaude();
        }

        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        protected void CarregaProfissionaisSaude()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, true, "0");
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlOperadora);
            CarregarPacientes();
        }

        protected void ddlProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
        }
    }
}