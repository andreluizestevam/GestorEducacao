using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data;
using System.Data.Objects;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9301_CadastroExamesEsternos
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                CarregaUnidades(ddlLocalPaciente);
                CarregaUnidades(ddlUnidHist);
                CarregaProfissionais();
                txtDtIniHist.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                txtDtFimHist.Text = DateTime.Now.ToShortDateString();

                CarregaOperadoras();

                CarregarGrupos();
                CarregarSubGrupos();
            }
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            #region Validacoes

            //Verifica se foi selecionado um Paciente
            if (String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente para quem será agendado o atendimento");
                ddlNomeUsu.Focus();
                return;
            }

            bool SelecProced = false;

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                if (((CheckBox)i.FindControl("chkProced")).Checked)
                {
                    SelecProced = true;

                    var txtQtd = (TextBox)i.FindControl("txtQtd");
                    var txtVlUnit = (TextBox)i.FindControl("txtVlUnit");

                    if (String.IsNullOrEmpty(txtQtd.Text) || txtQtd.Text == "0")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade deve ser informada e não pode ser zero");
                        txtQtd.Focus();
                        return;
                    }

                    if (String.IsNullOrEmpty(txtVlUnit.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor informe o valor do procedimeto");
                        txtVlUnit.Focus();
                        return;
                    }
                }
            }

            if (!SelecProced)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um procedimento");
                grdProcedimentos.Focus();
                return;
            }

            #endregion

            #region Persistencias

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                if (((CheckBox)i.FindControl("chkProced")).Checked)
                {
                    var hidIdProc = (HiddenField)i.FindControl("hidIdProc");
                    var ddlCortesia = (DropDownList)i.FindControl("ddlCortesia");
                    var txtQtd = (TextBox)i.FindControl("txtQtd");
                    var txtVlUnit = (TextBox)i.FindControl("txtVlUnit");
                    var ddlRequis = (DropDownList)i.FindControl("ddlRequis");
                    var txtSolicitante = (TextBox)i.FindControl("txtSolicitante");
                    var ddlSiglaEntid = (DropDownList)i.FindControl("ddlSiglaEntid");
                    var txtNumEntid = (TextBox)i.FindControl("txtNumEntid");

                    var tbs411 = new TBS411_EXAMES_ESTERNOS();

                    var proc = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(hidIdProc.Value));
                    proc.TB250_OPERAReference.Load();
                    proc.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                    tbs411.TBS356_PROC_MEDIC_PROCE = proc;
                    tbs411.TB250_OPERA = proc.TB250_OPERA;
                    tbs411.CO_ALU = int.Parse(ddlNomeUsu.SelectedValue);

                    tbs411.FL_CORTESIA = ddlCortesia.SelectedValue;
                    tbs411.NU_QTD_PROCED = int.Parse(txtQtd.Text);
                    tbs411.VL_PROCED = decimal.Parse(txtVlUnit.Text);
                    tbs411.FL_REQUISICAO = ddlRequis.SelectedValue;

                    if (tbs411.FL_REQUISICAO == "S")
                    {
                        tbs411.NO_SOLICITANTE = txtSolicitante.Text;
                        tbs411.CO_SIGLA_ENTID_SOLIC = ddlSiglaEntid.SelectedValue;
                        tbs411.NU_ENTID_SOLIC = txtNumEntid.Text;
                    }

                    var valor = proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A");
                    if (valor != null)
                        tbs411.VL_PROCED_BASE = valor.VL_BASE;

                    //Dados da situação e cadastro do exame
                    tbs411.CO_SITUA = "A";
                    tbs411.DT_SITUA = tbs411.DT_CADAS = DateTime.Now;
                    tbs411.CO_COL_SITUA = tbs411.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs411.CO_EMP_COL_SITUA = tbs411.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs411.CO_EMP_SITUA = tbs411.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs411.IP_SITUA = tbs411.IP_CADAS = Request.UserHostAddress;

                    #region Trata sequencial

                    var res2 = (from tbs411pesq in TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                                select new { tbs411pesq.NU_REGISTRO }).OrderByDescending(w => w.NU_REGISTRO).FirstOrDefault();

                    string seq;
                    int seq2 = 0;
                    int seqConcat;
                    string seqcon;
                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                    string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                    if (res2 != null && res2.NU_REGISTRO != null)
                    {
                        seq = res2.NU_REGISTRO.Substring(6, 6);
                        seq2 = int.Parse(seq);
                    }

                    seqConcat = seq2 + 1;
                    seqcon = seqConcat.ToString().PadLeft(6, '0');

                    tbs411.NU_REGISTRO = string.Format("EE{0}{1}{2}", ano, mes, seqcon);

                    #endregion

                    TBS411_EXAMES_ESTERNOS.SaveOrUpdate(tbs411, true);
                }
            }

            #endregion

            AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento de Atendimento(s) realizado(s) com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Carrega as unidades de acordo com a Instituição logada.
        /// </summary>
        private void CarregaUnidades(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true, true, false);
        }

        /// <summary>
        /// Responsável por carregar os profissionais
        /// </summary>
        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfissional, 0, true, "0", true);
        }

        /// <summary>
        /// Sobrecarga do método que carrega as operadoras de plano de saúde
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlContratacao, true);
        }

        /// <summary>
        /// Carrega os grupos de procedimentos
        /// </summary>
        private void CarregarGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo, true);
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        private void CarregarSubGrupos()
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, ddlGrupo, true);
        }

        /// <summary>
        /// Responsável por carregar os pacientes de acordo com o cpf concedido
        /// </summary>
        private void PesquisaPaciente()
        {
            //Verifica se o usuário optou por pesquisar por CPF ou por NIRE
            if (chkPesqCpf.Checked)
            {
                string cpf = (txtCPFPaci.Text != "" ? txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim() : "");

                //Valida se o usuário digitou ou não o CPF
                if (txtCPFPaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF para pesquisa");
                    return;
                }

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpf
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                if (res != null && ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
                {
                    ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                    CarregarGridHistoricoPaciente(res.CO_ALU);
                }
            }
            else if (chkPesqNire.Checked)
            {
                //Valida se o usuário deixou o campo em branco.
                if (txtNirePaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIRE para pesquisa");
                    return;
                }

                int nire = int.Parse(txtNirePaci.Text.Trim());

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_NIRE == nire
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                if (res != null && ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
                {
                    ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                    CarregarGridHistoricoPaciente(res.CO_ALU);
                }
            }
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            if (drpProfissional.SelectedValue == "0" && ddlLocalPaciente.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
            else
            {
                int localPaciente = ddlLocalPaciente.SelectedValue != "" ? int.Parse(ddlLocalPaciente.SelectedValue) : 0;
                int Profissional = drpProfissional.SelectedValue != "" ? int.Parse(drpProfissional.SelectedValue) : 0;

                var res = new List<Paciente>();

                if (localPaciente != 0)
                {
                    res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where (localPaciente != 0 ? tb07.CO_EMP_ORIGEM == localPaciente : 0 == 0)
                           select new Paciente { NO_ALU = tb07.NO_ALU, CO_ALU = tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
                }
                else
                {
                    res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                           where (Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0)
                           select new Paciente { NO_ALU = tb07.NO_ALU, CO_ALU = tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
                }

                if (res != null)
                {
                    ddlNomeUsu.DataTextField = "NO_ALU";
                    ddlNomeUsu.DataValueField = "CO_ALU";
                    ddlNomeUsu.DataSource = res;
                    ddlNomeUsu.DataBind();
                }

                ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        public class Paciente
        {
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
        }

        /// <summary>
        /// Carrega o histórico de agendamentos do paciente recebido como parâmetro;
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregarGridHistoricoPaciente(int CO_ALU)
        {
            int unid = (!string.IsNullOrEmpty(ddlUnidHist.SelectedValue) ? int.Parse(ddlUnidHist.SelectedValue) : 0);
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHist.Text) ? DateTime.Parse(txtDtIniHist.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHist.Text) ? DateTime.Parse(txtDtFimHist.Text) : DateTime.Now);

            var res = (from tbs411 in TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs411.CO_EMP_CADAS equals tb25.CO_EMP
                       where tbs411.CO_ALU == CO_ALU && tbs411.CO_SITUA != "C"
                       && (unid != 0 ? tbs411.CO_EMP_CADAS == unid : 0 == 0)
                       && (EntityFunctions.TruncateTime(tbs411.DT_CADAS) >= EntityFunctions.TruncateTime(dtIni)
                       && EntityFunctions.TruncateTime(tbs411.DT_CADAS) <= EntityFunctions.TruncateTime(dtFim))
                       select new Historico
                       {
                           ID_EXAME = tbs411.ID_EXAME,
                           DT = tbs411.DT_CADAS,
                           UNID = tb25.sigla,
                           GRUPO = tbs411.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                           SUBGRUPO = tbs411.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                           PROCEDIMENTO = tbs411.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           CONTRAT = tbs411.TB250_OPERA.NM_SIGLA_OPER,
                           SOLICITANTE = tbs411.NO_SOLICITANTE,
                           CORTESIA = tbs411.FL_CORTESIA == "S" ? "Sim" : "Não",
                           QTD = tbs411.NU_QTD_PROCED.HasValue ? tbs411.NU_QTD_PROCED.Value : 1,
                           VL_PROC = tbs411.VL_PROCED.HasValue ? tbs411.VL_PROCED.Value : 0,
                           STATUS = tbs411.CO_SITUA,
                           OR = tbs411.TBS390_ATEND_AGEND.ID_ATEND_AGEND != null ? "AT" : "EX"
                       }).OrderBy(w => w.DT).ToList();

            grdHistorPaciente.DataSource = res;
            grdHistorPaciente.DataBind();
        }

        public class Historico
        {
            public int ID_EXAME { get; set; }
            public DateTime DT { get; set; }
            public string DT_HORAR
            {
                get
                {
                    return this.DT.ToShortDateString() + " " + this.DT.ToShortTimeString();
                }
            }
            public string UNID { get; set; }
            public string GRUPO { get; set; }
            public string SUBGRUPO { get; set; }
            public string PROCEDIMENTO { get; set; }
            public string CONTRAT { get; set; }
            public string CORTESIA { get; set; }
            public string SOLICITANTE { get; set; }

            public int QTD { get; set; }
            public decimal VL_PROC { get; set; }
            public decimal VL_TOTAL
            {
                get
                {
                    return QTD * VL_PROC;
                }
            }

            private string situ;
            public string STATUS
            {
                get
                {
                    switch (situ)
                    {
                        case "A":
                            return "Aberto";
                        case "P":
                            return "Pago";
                        default:
                            return " - ";
                    }
                }
                set
                {
                    situ = value;
                }
            }

            public string OR { get; set; }
            public string ORIGEM { get { return this.OR; } }
            public string ORIGEM_TOOLTIP
            {
                get
                {
                    switch (this.ORIGEM)
                    {
                        case "AT":
                            return "Atendimento";
                        case "EX":
                            return "Exame Externo";
                        default:
                            return "";
                    }
                }
            }
        }

        public void CarregarProcedimentos()
        {
            var coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            var coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            var idOper = ddlContratacao.SelectedValue != "" ? int.Parse(ddlContratacao.SelectedValue) : 0;

            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(v => v.FL_STATU == "A") on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into vlr
                       from tbs353 in vlr.DefaultIfEmpty()
                       where tbs356.CO_SITU_PROC_MEDI == "A" && tbs356.FL_USO_EXTERNO == "S"
                       && (coGrupo != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == coGrupo : 0 == 0)
                       && (coSubGrupo != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == coSubGrupo : 0 == 0)
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : 0 == 0)
                       select new Procedimetos
                       {
                           ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                           NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                           VL_PROCED = tbs353 != null ? tbs353.VL_BASE : 0
                       }).OrderBy(w => w.NM_PROC_MEDI);

            grdProcedimentos.DataSource = res;
            grdProcedimentos.DataBind();
        }

        public class Procedimetos
        {
            public int ID_PROC_MEDI_PROCE { get; set; }
            public string NM_PROC_MEDI { get; set; }
            public decimal VL_PROCED { get; set; }
        }

        #endregion

        #region Eventos de componentes

        protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtNirePaci.Enabled = true;
                chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void chkPesqCpf_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtCPFPaci.Enabled = true;
                chkPesqNire.Checked = txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void imgCpfPac_OnClick(object sender, EventArgs e)
        {
            CarregaPacientes();
            PesquisaPaciente();
            OcultarPesquisa(true);
        }

        protected void drpProfissional_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPacientes();
            OcultarPesquisa(true);
        }

        protected void ddlLocalPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPacientes();
            OcultarPesquisa(true);
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlNomeUsu.DataTextField = "NO_ALU";
                ddlNomeUsu.DataValueField = "CO_ALU";
                ddlNomeUsu.DataSource = res;
                ddlNomeUsu.DataBind();
            }

            ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));

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
            ddlNomeUsu.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblSitPaciente.Text = " - ";

            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var coAlu = int.Parse(ddlNomeUsu.SelectedValue);
                CarregarGridHistoricoPaciente(coAlu);

                var pac = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                pac.TB250_OPERAReference.Load();

                if (pac.TB250_OPERA != null && ddlContratacao.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                    ddlContratacao.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
                else
                    ddlContratacao.SelectedValue = "0";

                switch (pac.CO_SITU_ALU)
                {
                    case "A":
                        lblSitPaciente.Text = "EM ATENDIMENTO";
                        lblSitPaciente.CssClass = "sitPacPadrao";
                        break;
                    case "V":
                        lblSitPaciente.Text = "EM ANÁLISE";
                        lblSitPaciente.CssClass = "sitPacAnalise";
                        break;
                    case "E":
                        lblSitPaciente.Text = "ALTA (NORMAL)";
                        lblSitPaciente.CssClass = "sitPacAlta";
                        break;
                    case "D":
                        lblSitPaciente.Text = "ALTA (DESISTÊNCIA)";
                        lblSitPaciente.CssClass = "sitPacAlta";
                        break;
                    //case "I":
                    //    lblSitPaciente.Text = "Inativo";
                    //    lblSitPaciente.CssClass = "";
                    //    break;
                }
            }
        }

        protected void imgPesqHist_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var coAlu = int.Parse(ddlNomeUsu.SelectedValue);
                CarregarGridHistoricoPaciente(coAlu);
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente para a pesquisa");
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton btnAtual = (ImageButton)sender;

            foreach (GridViewRow i in grdHistorPaciente.Rows)
            {
                var btn = (ImageButton)i.FindControl("imgExc");

                if (btn.ClientID == btnAtual.ClientID)
                {
                    var hidIdExame = (HiddenField)i.FindControl("hidIdExame");

                    var tbs411 = TBS411_EXAMES_ESTERNOS.RetornaPelaChavePrimaria(int.Parse(hidIdExame.Value));

                    tbs411.CO_SITUA = "C";
                    tbs411.DT_SITUA = DateTime.Now;
                    tbs411.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs411.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs411.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs411.IP_SITUA = Request.UserHostAddress;

                    TBS411_EXAMES_ESTERNOS.SaveOrUpdate(tbs411, true);

                    break;
                }
            }

            var coAlu = int.Parse(ddlNomeUsu.SelectedValue);
            CarregarGridHistoricoPaciente(coAlu);

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Atendimento CANCELADO com Sucesso!");
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarSubGrupos();
        }

        protected void imgbPesqProced_OnClick(object sender, EventArgs e)
        {
            CarregarProcedimentos();
        }

        protected void chkTodosItens_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkTodos = (CheckBox)grdProcedimentos.HeaderRow.FindControl("chkTodosItens");

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                CheckBox chk = (CheckBox)i.FindControl("chkProced");

                chk.Checked = chkTodos.Checked;

                var ddlCortesia = (DropDownList)i.FindControl("ddlCortesia");
                var txtQtd = (TextBox)i.FindControl("txtQtd");
                var txtVlUnit = (TextBox)i.FindControl("txtVlUnit");
                var ddlRequis = (DropDownList)i.FindControl("ddlRequis");
                var txtSolicitante = (TextBox)i.FindControl("txtSolicitante");
                var ddlSiglaEntid = (DropDownList)i.FindControl("ddlSiglaEntid");
                var txtNumEntid = (TextBox)i.FindControl("txtNumEntid");

                ddlCortesia.Enabled =
                txtQtd.Enabled =
                txtVlUnit.Enabled =
                ddlRequis.Enabled =
                txtSolicitante.Enabled =
                ddlSiglaEntid.Enabled =
                txtNumEntid.Enabled = chk.Checked;

                if (chk.Checked && ddlRequis.SelectedValue != "S")
                {
                    txtSolicitante.Enabled =
                    ddlSiglaEntid.Enabled =
                    txtNumEntid.Enabled = false;
                }
            }
        }

        protected void chkProced_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAtual = (CheckBox)sender;

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                CheckBox chk = (CheckBox)i.FindControl("chkProced");

                if (chk.ClientID == chkAtual.ClientID)
                {
                    var ddlCortesia = (DropDownList)i.FindControl("ddlCortesia");
                    var txtQtd = (TextBox)i.FindControl("txtQtd");
                    var txtVlUnit = (TextBox)i.FindControl("txtVlUnit");
                    var ddlRequis = (DropDownList)i.FindControl("ddlRequis");
                    var txtSolicitante = (TextBox)i.FindControl("txtSolicitante");
                    var ddlSiglaEntid = (DropDownList)i.FindControl("ddlSiglaEntid");
                    var txtNumEntid = (TextBox)i.FindControl("txtNumEntid");

                    ddlCortesia.Enabled =
                    txtQtd.Enabled =
                    txtVlUnit.Enabled =
                    ddlRequis.Enabled =
                    txtSolicitante.Enabled =
                    ddlSiglaEntid.Enabled =
                    txtNumEntid.Enabled = chk.Checked;

                    if (chk.Checked && ddlRequis.SelectedValue != "S")
                    {
                        txtSolicitante.Enabled =
                        ddlSiglaEntid.Enabled =
                        txtNumEntid.Enabled = false;
                    }

                    chk.Focus();
                    break;
                }
            }
        }

        protected void txtQtd_OnTextChanged(object sender, EventArgs e)
        {
            var txtAtual = (TextBox)sender;

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                var txt = (TextBox)i.FindControl("txtQtd");

                if (txt.ClientID == txtAtual.ClientID)
                {
                    var txtVlUnit = (TextBox)i.FindControl("txtVlUnit");
                    var txtVlTotal = (TextBox)i.FindControl("txtVlTotal");

                    if (!String.IsNullOrEmpty(txt.Text) && !String.IsNullOrEmpty(txtVlUnit.Text))
                        txtVlTotal.Text = (int.Parse(txt.Text) * decimal.Parse(txtVlUnit.Text)).ToString();

                    txt.Focus();
                    break;
                }
            }
        }

        protected void txtVlUnit_OnTextChanged(object sender, EventArgs e)
        {
            var txtAtual = (TextBox)sender;

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                var txt = (TextBox)i.FindControl("txtVlUnit");

                if (txt.ClientID == txtAtual.ClientID)
                {
                    var txtQtd = (TextBox)i.FindControl("txtQtd");
                    var txtVlTotal = (TextBox)i.FindControl("txtVlTotal");

                    if (!String.IsNullOrEmpty(txtQtd.Text) && !String.IsNullOrEmpty(txt.Text))
                        txtVlTotal.Text = (int.Parse(txtQtd.Text) * decimal.Parse(txt.Text)).ToString();

                    txt.Focus();
                    break;
                }
            }
        }

        protected void ddlRequis_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var drpAtual = (DropDownList)sender;

            foreach (GridViewRow i in grdProcedimentos.Rows)
            {
                var drp = (DropDownList)i.FindControl("ddlRequis");

                if (drp.ClientID == drpAtual.ClientID)
                {
                    var txtSolicitante = (TextBox)i.FindControl("txtSolicitante");
                    var ddlSiglaEntid = (DropDownList)i.FindControl("ddlSiglaEntid");
                    var txtNumEntid = (TextBox)i.FindControl("txtNumEntid");

                    txtSolicitante.Enabled =
                    ddlSiglaEntid.Enabled =
                    txtNumEntid.Enabled = drp.SelectedValue == "S";

                    drp.Focus();
                    break;
                }
            }
        }

        #endregion
    }
}