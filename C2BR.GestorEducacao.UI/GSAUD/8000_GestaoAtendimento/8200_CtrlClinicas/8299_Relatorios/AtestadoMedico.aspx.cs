using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8221_AtendimentoOdonto;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios
{
    public partial class AtestadoMedico : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            try
            {
                if (String.IsNullOrEmpty(ddlPaciente.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um paciente para gerar o atestado.");
                    return;
                }

                if (String.IsNullOrEmpty(ddlAtendimento.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um atendimento para gerar o atestado.");
                    return;
                }

                if (rbAtestado.Checked)
                {
                    if (String.IsNullOrEmpty(txtDiasAtest.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Insira os dias referentes ao atestado");
                        return;
                    }

                    if (String.IsNullOrEmpty(ddlCid.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Escolha o CID referente ao atestado");
                        return;
                    }

                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(ddlAtendimento.SelectedValue));
                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlPaciente.SelectedValue));
                    tb07.TB108_RESPONSAVELReference.Load();
                    var tb117 = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(int.Parse(ddlCid.SelectedValue));
                    var tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb07.TB108_RESPONSAVEL.CO_RESP);

                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    RptAtestadoMedico2 fpcb = new RptAtestadoMedico2();

                    TBS333_ATEST_MEDIC_PACIE tbs333 = new TBS333_ATEST_MEDIC_PACIE();
                    tbs333.ID_DOCUM = 0;

                    tbs333.IDE_CID = tb117.IDE_CID;
                    tbs333.ID_ATEND_MEDIC = tbs390.ID_ATEND_AGEND;
                    tbs333.CO_ALU = tb07.CO_ALU;
                    tbs333.QT_DIAS = int.Parse(txtDiasAtest.Text);
                    tbs333.DT_ATEST_MEDIC = tbs390.DT_REALI;
                    tbs333.DT_CADAS = DateTime.Now;
                    tbs333.CO_EMP_MEDIC = LoginAuxili.CO_EMP;
                    tbs333.CO_COL_MEDIC = LoginAuxili.CO_COL;
                    tbs333.CO_EMP = LoginAuxili.CO_EMP;
                    tbs333.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb07.TB108_RESPONSAVEL.CO_RESP);

                    #region Sequencial NR Registro

                    string coUnid = LoginAuxili.CO_UNID.ToString();
                    int coEmp = LoginAuxili.CO_EMP;
                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                    var res = (from tbs333pesq in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                               where tbs333pesq.CO_EMP == coEmp && tbs333pesq.NU_REGIS_ATEST_MEDIC != null
                               select new { tbs333pesq.NU_REGIS_ATEST_MEDIC }).OrderByDescending(w => w.NU_REGIS_ATEST_MEDIC).FirstOrDefault();

                    string seq;
                    int seq2;
                    int seqConcat;
                    string seqcon;
                    if (res == null)
                    {
                        seq2 = 1;
                    }
                    else
                    {
                        seq = res.NU_REGIS_ATEST_MEDIC.Substring(7, 6);
                        seq2 = int.Parse(seq);
                    }

                    seqConcat = seq2 + 1;
                    seqcon = seqConcat.ToString().PadLeft(6, '0');

                    tbs333.NU_REGIS_ATEST_MEDIC = "AT" + ano + coUnid.PadLeft(3, '0') + seqcon;
                    #endregion

                    var lRetorno = fpcb.InitReport("Atestado Médico", infos, LoginAuxili.CO_EMP, tb07.NO_ALU, txtDiasAtest.Text, chkMostraCid.Checked, tb117.CO_CID, tb07.CO_RG_ALU, tbs390.DT_REALI.ToString("dd/MM/yyyy"), tbs390.DT_REALI.ToString("HH:mm"), LoginAuxili.CO_COL);

                    if (lRetorno > 0)
                    {
                        TBS333_ATEST_MEDIC_PACIE.SaveOrUpdate(tbs333, true);
                    }

                    GerarRelatorioPadrão(fpcb, lRetorno);
                }

                if (rbComparecimento.Checked)
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(ddlAtendimento.SelectedValue));
                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlPaciente.SelectedValue));
                    tb07.TB108_RESPONSAVELReference.Load();
                    var tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb07.TB108_RESPONSAVEL.CO_RESP);

                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    RptDclComparecimento fpcb = new RptDclComparecimento();
                    var lRetorno = fpcb.InitReport("Declaração de Comparecimento", infos, LoginAuxili.CO_EMP, tb07.NO_ALU, tb108.NO_RESP, ddlPeriodoCompar.SelectedItem.Text, tbs390.DT_REALI.ToString("dd/MM/yyyy"), LoginAuxili.CO_COL);

                    GerarRelatorioPadrão(fpcb, lRetorno);
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
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
                CarregarCID();
                CarregarPeriodoCompar();

                ddlAtendimento.Items.Insert(0, new ListItem("Preencha os filtros"));
            }
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            int unidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tb07.CO_ALU equals tbs390.TB07_ALUNO.CO_ALU
                       where (!String.IsNullOrEmpty(txtNomePacPesq.Text) ? tb07.NO_ALU.Contains(txtNomePacPesq.Text) : 0 == 0)
                           && (unidadeDeCadastro != 0 ? tb07.CO_EMP == unidadeDeCadastro : 0 == 0)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));

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

        private void CarregaAtendimentos()
        {
            ddlAtendimento.Items.Clear();
            ddlAtendimento.DataSource = null;
            ddlAtendimento.DataBind();

            int coPaci = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            int coUnidade = int.Parse(ddlUnidadeCadastro.SelectedValue);
            DateTime dtIni = DateTime.TryParse(IniPeri.Text, out dtIni) ? DateTime.Parse(IniPeri.Text) : DateTime.Now;
            DateTime dtFim = DateTime.TryParse(FimPeri.Text, out dtFim) ? DateTime.Parse(FimPeri.Text) : DateTime.Now;

            var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                       where (coUnidade != 0 ? coUnidade == tbs390.CO_EMP_ATEND : 0 == 0)
                           && (coPaci != 0 ? coPaci == tbs390.TB07_ALUNO.CO_ALU : 0 == 0)
                           && (tbs390.TP_ATEND.Equals("A"))
                           && (tbs390.CO_COL_ATEND == LoginAuxili.CO_COL || tbs390.CO_COL_TEC_ATEND == LoginAuxili.CO_COL || tbs390.CO_COL_AUX_ATEND == LoginAuxili.CO_COL)
                       select new Atendimento
                       {
                           coAtend = tbs390.ID_ATEND_AGEND,
                           nuAtend = tbs390.NU_REGIS,
                           dtAtend = tbs390.DT_REALI
                       }).OrderByDescending(x => x.dtAtend).ToList();

            res = res.Where(x => x.dtAtend >= dtIni && x.dtAtend <= dtFim).ToList();

            if (res != null)
            {
                ddlAtendimento.DataTextField = "desAtend";
                ddlAtendimento.DataValueField = "coAtend";
                ddlAtendimento.DataSource = res;
                ddlAtendimento.DataBind();
                ddlAtendimento.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlAtendimento.Items.Insert(0, new ListItem("Sem atendimento", ""));
            }
        }

        private class Atendimento
        {
            public string nuAtend { get; set; }
            public int coAtend { get; set; }
            public DateTime dtAtend { get; set; }
            public string desAtend
            {
                get
                {
                    return this.dtAtend.ToString("dd/MM/yyyy HH:mm") + " - " + this.nuAtend;
                }
            }
        }

        private void CarregarPacientes()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tb07.CO_ALU equals tbs390.TB07_ALUNO.CO_ALU
                       where (UnidadeDeCadastro != 0 ? tb07.CO_EMP == UnidadeDeCadastro : 0 == 0)
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

        private void CarregarCID()
        {
            AuxiliCarregamentos.CarregaCID(ddlCid, false, true);
        }

        private void CarregarPeriodoCompar()
        {
            ddlPeriodoCompar.Items.Insert(0, new ListItem("Dia", "D"));
            ddlPeriodoCompar.Items.Insert(0, new ListItem("Noite", "N"));
            ddlPeriodoCompar.Items.Insert(0, new ListItem("Tarde", "T"));
            ddlPeriodoCompar.Items.Insert(0, new ListItem("Manhã", "M"));
        }

        private void GerarRelatorioPadrão(DevExpress.XtraReports.UI.XtraReport rpt, int lRetorno)
        {
            if (lRetorno == 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro na geração do Relatório! Tente novamente.");
            else if (lRetorno < 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem dados para a impressão do formulário solicitado.");
            else
            {
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                //----------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");

                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }
        }

        protected void dllUnidadeCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacientes();
        }

        protected void txtPeriodo_OnTextChanged(object sender, EventArgs e)
        {
            CarregaAtendimentos();
        }

        protected void ddlPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAtendimentos();
        }

        protected void rbAtestado_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.ClientID == rbAtestado.ClientID)
            {
                rbComparecimento.Checked = false;
            }
            else
                rbComparecimento.Checked = true;

            if (rb.ClientID == rbComparecimento.ClientID)
            {
                rbAtestado.Checked = false;
            }
            else
                rbAtestado.Checked = true;
        }
    }
}