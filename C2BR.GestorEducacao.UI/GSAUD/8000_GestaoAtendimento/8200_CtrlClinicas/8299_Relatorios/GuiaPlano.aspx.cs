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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios
{
    public partial class GuiaPlano : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (String.IsNullOrEmpty(drpPacienteGuia.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um paciente para gerar a guia.");
                return;
            }

            int paciente = int.Parse(drpPacienteGuia.SelectedValue);
            int agend = int.Parse(ddlAgendGuia.SelectedValue);

            DateTime? dtIni = chkGuiaConsol.Checked ? !String.IsNullOrEmpty(txtDtGuiaIni.Text) ? DateTime.Parse(txtDtGuiaIni.Text) : (DateTime?)null : (DateTime?)null;
            DateTime? dtFim = chkGuiaConsol.Checked ? !String.IsNullOrEmpty(txtDtGuiaFim.Text) ? DateTime.Parse(txtDtGuiaFim.Text) : (DateTime?)null : (DateTime?)null;

            if (chkGuiaConsol.Checked && (dtIni == null || dtFim == null))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para emitir a guia com procedimentos consolidados, preencha o período.");
                txtDtGuiaIni.Focus();
                txtDtGuiaFim.Focus();
                return;
            }

            RptGuiaAtend rpt = new RptGuiaAtend();
            var retorno = rpt.InitReport(paciente, txtObsGuia.Text, drpOperGuia.SelectedValue, txtDtGuia.Text, LoginAuxili.CO_COL, agend, chkGuiaConsol.Checked, dtIni, dtFim);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(retorno, this.AppRelativeVirtualPath);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = DateTime.Now;
                txtDtGuia.Text = data.ToShortDateString();
                txtObsGuia.Text = "";
                txtObsGuia.Attributes.Add("MaxLength", "180");
                drpOperGuia.Items.Clear();
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(drpOperGuia, false, false, false, true, false);
                drpOperGuia.Items.Insert(0, new ListItem("PADRÃO", "0"));
                drpPacienteGuia.Items.Clear();
                OcultarPesquisaPacienteGuia(false);
                CarregaAgendGuia();
            }
        }

        protected void txtDtGuia_OnTextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDtGuia.Text))
            {
                drpPacienteGuia.Items.Clear();
                CarregarPacientesComparecimento(drpPacienteGuia, DateTime.Parse(txtDtGuia.Text));
                drpPacienteGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));
                chkGuiaConsol.Checked = false;
                txtDtGuiaIni.Text = "";
                txtDtGuiaFim.Text = "";
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");

            CarregaAgendGuia();
        }
        
        private void CarregaAgendGuia()
        {
            int coAlu = string.IsNullOrEmpty(drpPacienteGuia.SelectedValue) ? 0 :  int.Parse(drpPacienteGuia.SelectedValue);
            DateTime dtGuia = DateTime.Parse(txtDtGuia.Text);

            if (coAlu != 0)
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.DT_AGEND_HORAR == dtGuia)
                           where (tbs174.CO_ALU == coAlu)
                           select new AgendGuia { dtAgend = tbs174.DT_AGEND_HORAR, hrAgend = tbs174.HR_AGEND_HORAR, nuRegis = tbs174.NU_REGIS_CONSUL, idAgend = tbs174.ID_AGEND_HORAR }).ToList();

                if (res != null)
                {
                    ddlAgendGuia.DataTextField = "NO_AGEND";
                    ddlAgendGuia.DataValueField = "idAgend";
                    ddlAgendGuia.DataSource = res;
                    ddlAgendGuia.DataBind();
                }
            }

            ddlAgendGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));
        }

        public class AgendGuia
        {
            public DateTime dtAgend { get; set; }
            public string hrAgend { get; set; }
            public string nuRegis { get; set; }
            public int idAgend { get; set; }

            public string NO_AGEND
            {
                get
                {
                    return this.dtAgend.ToString("dd/MM/yyyy") + " " + this.hrAgend + "  " + this.nuRegis;
                }
            }
        }

        private void CarregarPacientesGuia(DropDownList drp, string nome)
        {
            drp.Items.Clear();
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tb07.NO_ALU.Contains(nome) && tbs174.CO_EMP == LoginAuxili.CO_EMP
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            var res_ = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                        where tb07.NO_ALU.Contains(nome) && tbs372.FL_TIPO_AGENDA == "C"
                        select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res_ != null && res_.Count > 0 && res != null)
                foreach (var i in res_)
                    res.Add(i);

            if (res != null && res.Count > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res;
                drp.DataBind();
            }
            drp.Items.Insert(0, new ListItem("EM BRANCO", "0"));
        }

        private void CarregarPacientesComparecimento(DropDownList drp, DateTime dt)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.CO_EMP == LoginAuxili.CO_EMP && tbs174.DT_AGEND_HORAR == dt
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            var res_ = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                        where tbs372.FL_TIPO_AGENDA == "C" && tbs372.DT_AGEND == dt
                        select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res_ != null && res_.Count > 0 && res != null)
                foreach (var i in res_)
                    res.Add(i);

            if (res != null && res.Count > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res;
                drp.DataBind();
            }
        }

        protected void drpPacienteGuia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendGuia();
        }

        protected void imgPesqPacienteGuia_OnClick(object sender, EventArgs e)
        {
            if (txtPacienteGuia.Text.Length < 3)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Digite pelo menos 3 letras para filtar por paciente.");
                txtPacienteGuia.Focus();
                return;
            }

            OcultarPesquisaPacienteGuia(true);
            string nome = txtPacienteGuia.Text;

            CarregarPacientesGuia(drpPacienteGuia, nome);
        }

        private void OcultarPesquisaPacienteGuia(bool ocultar)
        {
            txtPacienteGuia.Visible =
            imgPesqPacienteGuia.Visible = !ocultar;
            drpPacienteGuia.Visible =
            imgVoltarPesqPacienteGuia.Visible = ocultar;
        }

        protected void imgVoltarPesqPacienteGuia_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaPacienteGuia(false);
        }
    }
}