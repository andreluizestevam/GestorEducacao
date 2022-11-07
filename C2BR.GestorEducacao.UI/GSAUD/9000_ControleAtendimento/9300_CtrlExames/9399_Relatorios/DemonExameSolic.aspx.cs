using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9399_Relatorios
{
    public partial class DemonExameSolic : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            /*if (String.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um paciente e tente novamente");
                return;
            }

            if (!ulDadosPesquisa.Visible)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi encontrado para o paciente selecionado nenhum resultado de exame!");
                return;
            }

            var coAlu = !String.IsNullOrEmpty(ddlPaciente.SelectedValue) ? int.Parse(ddlPaciente.SelectedValue) : 0;
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;
            var dtInical = !String.IsNullOrEmpty(txtIniPeri.Text) ? Convert.ToDateTime(txtIniPeri.Text) : DateTime.Now;
            var dtFinal = !String.IsNullOrEmpty(txtFimPeri.Text) ? Convert.ToDateTime(txtFimPeri.Text) : DateTime.Now;

            string nmFunc = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/9000_ControleAtendimento/9300_CtrlExames/9399_Relatorios/DemonExameSolic.aspx");

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var parametros = "( Paciente : " + ddlPaciente.SelectedItem.Text.ToUpper()
                           + " - Contratação : " + ddlOperadora.SelectedItem.Text.ToUpper()
                           + " - Procedimento : " + ddlProcedimento.SelectedItem.Text.ToUpper()
                           + " - Período: " + txtIniPeri.Text + " à " + txtFimPeri.Text + " )";

            RptResultadoExame rpt = new RptResultadoExame();
            var lRetorno = rpt.InitReport(nmFunc, parametros, infos, unid, coAlu, idOper, idProc, dtInical, dtFinal);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);*/
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaOperadoras();
                CarregaSolicitante();
            }
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

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaSolicitante()
        {
            AuxiliCarregamentos.CarregaMedicos(ddlSolic,LoginAuxili.CO_EMP,true);
        }

        private void CarregaOperadoras()
        {
            ddlOperadora.Items.Clear();

            //var coAlu = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;

            var res = (from tbs416 in TBS416_EXAME_RESUL.RetornaTodosRegistros()
                       join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs416.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER equals tb250.ID_OPER
                       //where tbs416.TB07_ALUNO.CO_ALU == coAlu
                       select new { 
                           tb250.ID_OPER, 
                           tb250.NM_SIGLA_OPER
                       }).OrderBy(o => o.NM_SIGLA_OPER).DistinctBy(o => o.ID_OPER).ToList();

            if (res != null)
            {
                ddlOperadora.DataTextField = "NM_SIGLA_OPER";
                ddlOperadora.DataValueField = "ID_OPER";
                ddlOperadora.DataSource = res;
                ddlOperadora.DataBind();
            }

            ddlOperadora.Items.Insert(0, new ListItem("Todos", "0"));
            CarregaProcedimentos();
        }

        private void CarregaProcedimentos()
        {
            ddlProcedimento.Items.Clear();

            //var coAlu = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            var idOper = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;

            var res = (from tbs416 in TBS416_EXAME_RESUL.RetornaTodosRegistros()
                       join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs416.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                       //where tbs416.TB07_ALUNO.CO_ALU == coAlu
                       where (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : 0 == 0)
                       select new
                       {
                           tbs356.ID_PROC_MEDI_PROCE,
                           tbs356.NM_PROC_MEDI
                       }).OrderBy(p => p.NM_PROC_MEDI).DistinctBy(p => p.ID_PROC_MEDI_PROCE).ToList();

            if (res != null)
            {
                ddlProcedimento.DataTextField = "NM_PROC_MEDI";
                ddlProcedimento.DataValueField = "ID_PROC_MEDI_PROCE";
                ddlProcedimento.DataSource = res;
                ddlProcedimento.DataBind();
            }

            ddlProcedimento.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlOperadora_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos();
        }

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ulDadosPesquisa.Visible = false;

            if (!String.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                var coAlu = int.Parse(ddlPaciente.SelectedValue);

                var res = TBS416_EXAME_RESUL.RetornaTodosRegistros().Where(r => r.TB07_ALUNO.CO_ALU == coAlu).ToList();

                if (res != null && res.Count > 0)
                {
                    ulDadosPesquisa.Visible = true;
                    CarregaOperadoras();
                    CarregaProcedimentos();
                    txtIniPeri.Text = res.OrderBy(r => r.DT_CADAS).FirstOrDefault().DT_CADAS.ToShortDateString();
                    txtFimPeri.Text = res.OrderByDescending(r => r.DT_CADAS).FirstOrDefault().DT_CADAS.ToShortDateString();
                }
                else
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi encontrado para o paciente selecionado nenhum resultado de exame!");
            }
        }
    }
}