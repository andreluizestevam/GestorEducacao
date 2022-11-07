using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios
{
    public partial class FichaAtendimento : System.Web.UI.Page
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
                CarregaUnidade(ddlUnidadeCadastro);
                CarregaUnidade(ddlUnidadeContrato);
                CarregarPacientes();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8299_Relatorios/FichaAtendimento.aspx");

            int paciente = int.Parse(ddlPaciente.SelectedValue);
            int atendimento = int.Parse(drpAtendimento.SelectedValue);

            string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaAtend2 rpt = new RptFichaAtend2();
            var retorno = rpt.InitReport(titulo, infos, LoginAuxili.CO_EMP, paciente, atendimento);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(retorno, this.AppRelativeVirtualPath);
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregarPacientes()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.CO_ALU equals tb07.CO_ALU
                       where (UnidadeDeCadastro != 0 ? tb07.CO_EMP == UnidadeDeCadastro : 0 == 0)
                       && (UnidadeDeContrato != 0 ? tb07.CO_EMP_ORIGEM == UnidadeDeContrato : 0 == 0)
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

        protected void ddlPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            liNumAtend.Visible = false;

            if (!String.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                int paciente = int.Parse(ddlPaciente.SelectedValue);

                var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                           where tbs390.TBS174_AGEND_HORAR.CO_ALU == paciente
                           select new Atendimento
                           {
                               idAtend = tbs390.ID_ATEND_AGEND,
                               Data = tbs390.TBS174_AGEND_HORAR.DT_AGEND_HORAR,
                               numRAP = tbs390.NU_REGIS,
                               hipotose = tbs390.DE_HIP_DIAGN
                           }).ToList();

                if (res != null && res.Count > 0)
                {
                    liNumAtend.Visible = true;

                    drpAtendimento.DataTextField = "Atend";
                    drpAtendimento.DataValueField = "idAtend";
                    drpAtendimento.DataSource = res;
                    drpAtendimento.DataBind();
                    drpAtendimento.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O paciente selecionado não possui nenhum Atendimento!");
            }
        }

        public class Atendimento
        {
            public int idAtend { get; set; }

            public DateTime Data { get; set; }

            public string numRAP { get; set; }

            public string hipotose { get; set; }

            public string Atend
            {
                get
                {
                    return numRAP + " - " + Data.ToShortDateString() + (!String.IsNullOrEmpty(hipotose) ? " - " + (hipotose.Length <= 30 ? hipotose : hipotose.Substring(0, 30) + "...") : "");
                }
            }
        }
    }
}