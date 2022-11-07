//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//--------------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//--------------------------------------------------------------------------------------
//  DATA         |  NOME DO PROGRAMADOR           | DESCRIÇÃO RESUMIDA
// --------------+--------------------------------+-------------------------------------
//  30/06/2016      Tayguara Acioli TA.30/06/2016   Alterei a Forma de listagem dos pacientes.
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Globalization;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Data.Objects;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod2._8121_ReplicacaoAgenda
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
                CarregaPacientes();
                CarregaDatasIniFim();
                txtDtIniHistoUsuar.Text =
                txtDtFimHistoUsuar.Text = DateTime.Now.ToString();
                Session["Horarios"] = null;
            }
        }

        //TA.30/06/2016 início

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
        //TA.30/06/2016 fim

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            #region Validacoes

            //==============>Validações dos campos 
            //Verifica se foi selecionado um Paciente
            if (ddlNomeUsu.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente para quem será agendada a consulta.");
                ddlNomeUsu.Focus();
                return;
            }

            bool SelecHorario = false;

            //Verifica se foi selecionado um horário para marcação da consulta
            foreach (GridViewRow li in grdHorario.Rows)
            {
                if (SelecHorario == false)
                {
                    if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                    {
                        SelecHorario = true;
                    }
                }
            }

            //Valida a variável booleana criada anteriormente
            if (SelecHorario == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um horário da agenda para o qual será feita a marcação da consulta.");
                grdHorario.Focus();
                return;
            }

            #endregion

            Persistencias();
        }

        /// <summary>
        /// Executa os métodos para persistência de dados
        /// </summary>
        private void Persistencias()
        {
            //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
            foreach (GridViewRow lis in grdHorario.Rows)
            {
                //Verifica a linha que foi selecionada
                if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                {
                    int coAgend = int.Parse(((HiddenField)lis.FindControl("hidCoAgenda")).Value);
                    int idAgendOrig = int.Parse(((HiddenField)lis.FindControl("hidCoAgendaOrig")).Value);
                    int coAlu = int.Parse(ddlNomeUsu.SelectedValue);

                    //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
                    TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);
                    TB07_ALUNO tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault();
                    TB03_COLABOR tb03 = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == tbs174.CO_COL).FirstOrDefault();

                    tbs174.TP_CONSU = "N";
                    tbs174.CO_EMP_ALU = tb07.CO_EMP;
                    tbs174.CO_ALU = coAlu;
                    tbs174.TB250_OPERA = (tb07.TB250_OPERA != null ? tb07.TB250_OPERA : null);
                    tbs174.TB251_PLANO_OPERA = (tb07.TB251_PLANO_OPERA != null ? tb07.TB251_PLANO_OPERA : null);
                    tbs174.FL_CONF_AGEND = "N";

                    switch (tb03.CO_CLASS_PROFI)
                    {
                        case "T":
                            tbs174.TP_AGEND_HORAR = "TO";
                            break;
                        case "O":
                            tbs174.TP_AGEND_HORAR = "OU";
                            break;
                        case "N":
                            tbs174.TP_AGEND_HORAR = "NT";
                            break;
                        case "S":
                            tbs174.TP_AGEND_HORAR = "ES";
                            break;
                        case "P":
                            tbs174.TP_AGEND_HORAR = "PI";
                            break;
                        case "F":
                            tbs174.TP_AGEND_HORAR = "FO";
                            break;
                        case "I":
                            tbs174.TP_AGEND_HORAR = "FI";
                            break;
                        case "E":
                            tbs174.TP_AGEND_HORAR = "EN";
                            break;
                        case "D":
                            tbs174.TP_AGEND_HORAR = "AO";
                            break;
                        case "M":
                            tbs174.TP_AGEND_HORAR = "AM";
                            break;
                    }

                    #region Gera Código da Consulta

                    string coUnid = LoginAuxili.CO_UNID.ToString();
                    int coEmp = LoginAuxili.CO_EMP;
                    string ano = DateTime.Now.AddYears(1).Year.ToString().Substring(2, 2);

                    var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
                               select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

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
                        seq = res.NU_REGIS_CONSUL.Substring(7, 7);
                        seq2 = int.Parse(seq);
                    }

                    seqConcat = seq2 + 1;
                    seqcon = seqConcat.ToString().PadLeft(7, '0');

                    tbs174.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

                    #endregion

                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

                    var procs = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                                 where tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idAgendOrig
                                 select tbs389).ToList();

                    foreach (var p in procs)
                    {
                        TBS386_ITENS_PLANE_AVALI tbs386 = new TBS386_ITENS_PLANE_AVALI();

                        //Dados básicos do item de planejamento
                        tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), tbs174.CO_ALU.Value);
                        tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);

                        p.TBS386_ITENS_PLANE_AVALIReference.Load();
                        p.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCEReference.Load();
                        tbs386.DE_RESUM_ACAO = p.TBS386_ITENS_PLANE_AVALI.DE_RESUM_ACAO;
                        tbs386.TBS356_PROC_MEDIC_PROCE = p.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE;
                        tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(tbs174.ID_AGEND_HORAR, p.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda

                        tbs386.DT_AGEND = tbs386.DT_INICI = tbs386.DT_FINAL = tbs174.DT_AGEND_HORAR;
                        tbs386.FL_AGEND_FEITA_PLANE = "N";

                        //Dados do cadastro e situação
                        tbs386.CO_SITUA = "A";
                        tbs386.DT_SITUA = tbs386.DT_CADAS = DateTime.Now;
                        tbs386.CO_COL_SITUA = tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_SITUA = tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_SITUA = tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs386.IP_SITUA = tbs386.IP_CADAS = Request.UserHostAddress;

                        TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);

                        TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();
                        tbs389.TBS174_AGEND_HORAR = tbs174;
                        tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                        TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);

                        tbs174.TBS370_PLANE_AVALI = tbs386.TBS370_PLANE_AVALI;
                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                    }
                }
            }

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Agendamento da Consulta realizado com Sucesso!");
        }

        /// <summary>
        /// Retorna um objeto do planejamento de determinad paciente/agenda recebidos como parâmetro
        /// </summary>
        /// <returns></returns>
        private TBS370_PLANE_AVALI RecuperaPlanejamento(int? ID_PLANE_AVALI, int CO_ALU)
        {
            if (ID_PLANE_AVALI.HasValue)
                return TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(ID_PLANE_AVALI.Value);
            else // Já que não tem ainda, cria um novo planejamento e retorna um objeto do mesmo no método
            {
                TBS370_PLANE_AVALI tbs370 = new TBS370_PLANE_AVALI();
                tbs370.CO_ALU = CO_ALU;

                //Dados do cadastro
                tbs370.DT_CADAS = DateTime.Now;
                tbs370.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs370.IP_CADAS = Request.UserHostAddress;

                //Dados da situação
                tbs370.CO_SITUA = "A";
                tbs370.DT_SITUA = DateTime.Now;
                tbs370.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs370.IP_SITUA = Request.UserHostAddress;
                TBS370_PLANE_AVALI.SaveOrUpdate(tbs370, true);

                return tbs370;
            }
        }

        /// <summary>
        /// Retorna o último número de ação encontrado (para o planejamento recebido como parâmetro) + 1
        /// </summary>
        /// <param name="CO_ALU"></param>
        /// <param name="ID_PROC"></param>
        private int RecuperaUltimoNrAcao(int ID_PLANE_AVALI)
        {
            var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                       where tbs389.TBS386_ITENS_PLANE_AVALI.TBS370_PLANE_AVALI.ID_PLANE_AVALI == ID_PLANE_AVALI
                       select new { tbs389.TBS386_ITENS_PLANE_AVALI.NR_ACAO }).OrderByDescending(w => w.NR_ACAO).FirstOrDefault();

            /*
             *Retorna o último número de ação encontrado (para a agenda e procedimento recebidos como parâmetro) + 1.
             *Se não houver, retorna o número 1
             */
            return (res != null ? res.NR_ACAO + 1 : 1);
        }

        /// <summary>
        /// Responsável por carregar os pacientes de acordo com o cpf concedido
        /// </summary>
        private void PesquisaPaciente()
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
                        && tb07.CO_SITU_ALU != "H" && tb07.CO_SITU_ALU != "O"
                       select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

            if (res != null)
                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega as datas de início e fim de consultas de um determinado profissional recebido como parâmetro
        /// </summary>
        private void CarregaDatasIniFim()
        {
            txtDtIniResCons.Text = "04/01/2016";
            txtDtFimResCons.Text = "23/12/2016";
        }

        /// <summary>
        /// Carrega a primeira data de início e final para o paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_COL"></param>
        private void CarregaDatasIniFimPaciente(int CO_ALU)
        {
            txtDtIniHistoUsuar.Text = DateTime.Now.AddMonths(-2).ToString();
            txtDtFimHistoUsuar.Text = DateTime.Now.ToString();
            var dtFim = new DateTime(DateTime.Now.Year, 1, 1);
            //var dtFim = new DateTime(DateTime.Now.AddYears(1).Year, 1, 1);
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where tbs174.CO_ALU == CO_ALU && tbs174.DT_AGEND_HORAR < dtFim
                       select new
                       {
                           tbs174.DT_AGEND_HORAR,
                       }).ToList();

            //Seta a primeira e última data de consultas do colaborador recebido como parâmetro
            if (res != null && res.Count > 0)
            {
                var fimPeri = res.LastOrDefault().DT_AGEND_HORAR;
                var iniPeri = fimPeri.AddDays(-6);

                txtDtIniHistoUsuar.Text = iniPeri.ToShortDateString();
                txtDtFimHistoUsuar.Text = fimPeri.ToShortDateString();
            }
        }

        /// <summary>
        /// Carrega o histórico de agendamentos do paciente recebido como parâmetro;
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricoPaciente(int CO_ALU)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHistoUsuar.Text) ? DateTime.Parse(txtDtIniHistoUsuar.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHistoUsuar.Text) ? DateTime.Parse(txtDtFimHistoUsuar.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       where tbs174.CO_ALU == CO_ALU
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                       //&&   a.DT_AGEND_HORAR >= dtIni && a.DT_AGEND_HORAR <= dtFim
                       select new HorarioHistoricoPaciente
                       {
                           CO_AGEND = tbs174.ID_AGEND_HORAR,
                           CO_COL = tb03.CO_COL,
                           NO_COL_RECEB = tb03.NO_COL,
                           CO_CLASS_FUNC = tb03.CO_CLASS_PROFI,
                           MATR_COL = tb03.CO_MAT_COL,
                           OPER = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : " - ",
                           STATUS = tbs174.CO_SITUA_AGEND_HORAR,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR
                       }).OrderBy(w => w.NO_COL_RECEB).ThenBy(w => w.DT).ThenBy(w => w.HR).ToList();

            grdHistorPaciente.DataSource = res;
            grdHistorPaciente.DataBind();
        }

        public class HorarioHistoricoPaciente
        {
            public int CO_AGEND { get; set; }
            public int CO_COL { get; set; }
            public string MATR_COL { get; set; }
            public string NO_COL
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0').Insert(2, ".").Insert(6, "-");
                    string noCol = (this.NO_COL_RECEB.Length > 37 ? this.NO_COL_RECEB.Substring(0, 37) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_COL_RECEB { get; set; }
            public string OPER { get; set; }
            public string STATUS { get; set; }
            public string STATUS_V
            {
                get
                {
                    switch (this.STATUS)
                    {
                        case "A":
                            return "Aberto";
                        case "C":
                            return "Cancelado";
                        case "I":
                            return "Inativo";
                        case "S":
                            return "Suspenso";
                        default:
                            return " - ";
                    }
                }
            }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DIA
            {
                get
                {
                    return DT.ToString("ddd", new CultureInfo("pt-BR"));
                }
            }
            public string DT_HORAR
            {
                get
                {
                    return this.HR + " " + DIA + " - " + this.DT.ToShortDateString();
                }
            }
            public string CO_CLASS_FUNC { get; set; }
            public string DE_CLASS_FUNC
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_FUNC);
                }
            }
        }

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private List<HorarioSaida> CarregarHorarios(int coCol, string horas, string dia)
        {
            DateTime dtIni = txtDtIniResCons.Text != "" ? DateTime.Parse(txtDtIniResCons.Text) : DateTime.Now;
            DateTime dtFim = txtDtFimResCons.Text != "" ? DateTime.Parse(txtDtFimResCons.Text) : DateTime.Now;

            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on a.CO_COL equals tb03.CO_COL
                       where a.CO_COL == coCol
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)  //dtInici
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))) //dtFimC
                       && (!String.IsNullOrEmpty(horas) ? a.HR_AGEND_HORAR == horas : 0 == 0)
                       select new HorarioSaida
                       {
                           dt = a.DT_AGEND_HORAR,
                           hr = a.HR_AGEND_HORAR,
                           CO_ALU = a.CO_ALU,
                           CO_AGEND = a.ID_AGEND_HORAR,
                           CO_COL = a.CO_COL,
                           NO_COL = tb03.NO_COL,
                           CO_TP_AGEND = tb03.CO_CLASS_PROFI
                       }).OrderBy(w => w.dt).ToList();

            var lst = new List<HorarioSaida>();

            #region Verifica os itens a serem excluídos
            if (res.Count > 0)
            {
                int aux = 0;
                foreach (var i in res)
                {
                    int d = (int)i.dt.DayOfWeek;

                    switch (d)
                    {
                        case 0:
                            if (dia != "dom")//!chkDom.Checked)
                            { lst.Add(i); }
                            break;
                        case 1:
                            if (dia != "seg")
                            { lst.Add(i); }
                            break;
                        case 2:
                            if (dia != "ter")
                            { lst.Add(i); }
                            break;
                        case 3:
                            if (dia != "qua")
                            { lst.Add(i); }
                            break;
                        case 4:
                            if (dia != "qui")
                            { lst.Add(i); }
                            break;
                        case 5:
                            if (dia != "sex")
                            { lst.Add(i); }
                            break;
                        case 6:
                            if (dia != "sab")
                            { lst.Add(i); }
                            break;
                    }
                    aux++;
                }
            }
            #endregion

            var resNew = res.Except(lst).ToList();

            //Reordena
            resNew = resNew.OrderBy(w => w.dt).ThenBy(w => w.hrC).ToList();

            return resNew;
        }

        public class HorarioSaida
        {
            //Carrega informações gerais do agendamento
            public DateTime dt { get; set; }
            public string hr { get; set; }
            public TimeSpan hrC
            {
                get
                {
                    //DateTime d = DateTime.Parse(hr);
                    return TimeSpan.Parse((hr + ":00"));
                }
            }
            public string hora
            {
                get
                {
                    string diaSemana = this.dt.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + diaSemana;
                }
            }
            public int CO_AGEND { get; set; }
            public int CO_AGEND_ORIG { get; set; }
            public string NO_COL { get; set; }
            public int? CO_COL { get; set; }
            public int? CO_ALU { get; set; }

            public string CO_TP_AGEND { get; set; }
            public string DE_TP_AGEND
            {
                get
                {
                    string tipo = " - ";
                    switch (this.CO_TP_AGEND)
                    {
                        case "O":
                            tipo = "Outros";
                            break;
                        case "T":
                            tipo = "Terapia Ocupacional";
                            break;
                        case "N":
                            tipo = "Nutrição";
                            break;
                        case "S":
                            tipo = "Estética";
                            break;
                        case "P":
                            tipo = "Psicologia";
                            break;
                        case "F":
                            tipo = "Fonoaudiologia";
                            break;
                        case "I":
                            tipo = "Fisioterapia";
                            break;
                        case "E":
                            tipo = "Enfermaria";
                            break;
                        case "D":
                            tipo = "Atendimento Odontológico";
                            break;
                        case "M":
                            tipo = "Atendimento Médico";
                            break;
                    }
                    return tipo;
                }
            }
        }

        #endregion

        #region Eventos de componentes

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaPaciente();
        }

        protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var CO_ALU = int.Parse(ddlNomeUsu.SelectedValue);
                CarregaDatasIniFimPaciente(CO_ALU);
                CarregaGridHistoricoPaciente(CO_ALU);

                Session["Horarios"] =
                grdHorario.DataSource = null;
                grdHorario.DataBind();
            }
        }

        protected void imgBtnPesqHistoUsuar_OnClick(object sender, EventArgs e)
        {
            var CO_ALU = int.Parse(ddlNomeUsu.SelectedValue);
            CarregaGridHistoricoPaciente(CO_ALU);
        }

        protected void imgBtnPesqResCons_OnClick(object sender, EventArgs e)
        {
            try
            {
                grdHorario.DataSource = null;

                foreach (GridViewRow l in grdHistorPaciente.Rows)
                {

                    CheckBox chk = (CheckBox)l.FindControl("ckSlctHora");
                    var dia = ((HiddenField)l.FindControl("hidDia")).Value;

                    var idAgendOrig = int.Parse(((HiddenField)l.FindControl("hidCoAgenda")).Value);
                    var coCol = int.Parse(((HiddenField)l.FindControl("hidCoCol")).Value);
                    var hora = ((HiddenField)l.FindControl("hidHora")).Value;

                    var hrs = CarregarHorarios(coCol, hora, dia);

                    if (hrs != null && hrs.Count > 0)
                    {
                        foreach (var h in hrs)
                            h.CO_AGEND_ORIG = idAgendOrig;

                        //var hrsAtl = new List<HorarioSaida>();

                        //if (Session["Horarios"] != null && ((List<HorarioSaida>)Session["Horarios"]).Count > 0)
                        //    foreach (var h in ((List<HorarioSaida>)Session["Horarios"]))
                        //        hrsAtl.Add(h);

                        //if (hrsAtl.Count > 0)
                        //    if (chk.Checked)
                        //    {
                        //        hrs.AddRange(hrsAtl);
                        //    }
                        //    else
                        //    {
                        //        foreach (var h in hrs)
                        //            hrsAtl.Remove(hrsAtl.FirstOrDefault(i => i.CO_AGEND == h.CO_AGEND));

                        //        hrs = hrsAtl;
                        //    }

                        //hrs = hrs.DistinctBy(h => new { h.CO_ALU, h.hora }).ToList();

                        Session["Horarios"] = hrs;
                        grdHorario.DataSource = hrs.OrderBy(h => h.dt).ThenBy(h => h.hr);
                        grdHorario.DataBind();
                    }
                    else
                    {
                        chk.Checked = false;

                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe agenda disponivel neste dia e horário para este profissional");
                    }
                }
            }
            catch (Exception se)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, se.Message);
            }
        }

        protected void ChkTodos_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdHorario.HeaderRow.FindControl("chkMarcaTodosItens"));

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox ck = (CheckBox)l.FindControl("ckSelectHr");

                if (chkMarca.Checked)
                    ck.Checked = true;
                else
                    ck.Checked = false;
            }
        }

        protected void ckSlctHora_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox atual = (CheckBox)sender;
                CarregaDatasIniFim();

                chkSeg.Checked =
                chkTer.Checked =
                chkQua.Checked =
                chkQui.Checked =
                chkSex.Checked =
                chkSab.Checked =
                chkDom.Checked = false;

                foreach (GridViewRow l in grdHistorPaciente.Rows)
                {
                    CheckBox chk = (CheckBox)l.FindControl("ckSlctHora");
                    var dia = ((HiddenField)l.FindControl("hidDia")).Value;

                    if (chk.Checked)
                    {
                        switch (dia)
                        {
                            case "seg":
                                chkSeg.Checked = true;
                                break;
                            case "ter":
                                chkTer.Checked = true;
                                break;
                            case "qua":
                                chkQua.Checked = true;
                                break;
                            case "qui":
                                chkQui.Checked = true;
                                break;
                            case "sex":
                                chkSex.Checked = true;
                                break;
                            case "sab":
                                chkSab.Checked = true;
                                break;
                            case "dom":
                                chkDom.Checked = true;
                                break;
                        }
                    }

                    if (atual.ClientID == chk.ClientID)
                    {
                        var idAgendOrig = int.Parse(((HiddenField)l.FindControl("hidCoAgenda")).Value);
                        var coCol = int.Parse(((HiddenField)l.FindControl("hidCoCol")).Value);
                        var hora = ((HiddenField)l.FindControl("hidHora")).Value;

                        var hrs = CarregarHorarios(coCol, hora, dia);

                        if (hrs != null && hrs.Count > 0)
                        {
                            foreach (var h in hrs)
                                h.CO_AGEND_ORIG = idAgendOrig;

                            var hrsAtl = new List<HorarioSaida>();

                            if (Session["Horarios"] != null && ((List<HorarioSaida>)Session["Horarios"]).Count > 0)
                                foreach (var h in ((List<HorarioSaida>)Session["Horarios"]))
                                    hrsAtl.Add(h);

                            if (hrsAtl.Count > 0)
                                if (chk.Checked)
                                {
                                    hrs.AddRange(hrsAtl);
                                }
                                else
                                {
                                    foreach (var h in hrs)
                                        hrsAtl.Remove(hrsAtl.FirstOrDefault(i => i.CO_AGEND == h.CO_AGEND));

                                    hrs = hrsAtl;
                                }

                            hrs = hrs.DistinctBy(h => new { h.CO_ALU, h.hora }).ToList();

                            Session["Horarios"] = hrs;
                            grdHorario.DataSource = hrs.OrderBy(h => h.dt).ThenBy(h => h.hr);
                            grdHorario.DataBind();
                        }
                        else
                        {
                            chk.Checked = false;

                            AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe agenda disponivel neste dia e horário para este profissional");
                        }
                    }
                }
            }
            catch (Exception se)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, se.Message);
            }
        }

        protected void grdHorario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chkT = (CheckBox)e.Row.FindControl("chkMarcaTodosItens");

                chkT.Checked = true;
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var chkH = (CheckBox)e.Row.FindControl("ckSelectHr");
                var lblOper = (Label)e.Row.FindControl("lblOperadora");
                var lblPlan = (Label)e.Row.FindControl("lblPlano");

                chkH.Checked = true;

                var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));

                if (res != null)
                {
                    res.TB250_OPERAReference.Load();
                    res.TB251_PLANO_OPERAReference.Load();

                    //Se houver operadora
                    if (res.TB250_OPERA != null)
                    {
                        lblOper.Text = res.TB250_OPERA.NM_SIGLA_OPER;

                        res.TB251_PLANO_OPERAReference.Load();
                        if (res.TB251_PLANO_OPERA != null) //Se houver plano
                            lblPlan.Text = res.TB251_PLANO_OPERA.NM_SIGLA_PLAN;
                    }
                }
            }
        }

        #endregion
    }
}