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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod2._8123_ReplicacaoAgendaProfSimp
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
                CarregaProfissionais();
                txtDtIniHistoUsuar.Text =
                txtDtFimHistoUsuar.Text = DateTime.Now.ToString();
            }
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            Persistencias();
        }

        /// <summary>
        /// Executa os métodos para persistência de dados
        /// </summary>
        private void Persistencias()
        {
            foreach (GridViewRow l in grdHistorPaciente.Rows)
            {
                CheckBox chk = (CheckBox)l.FindControl("ckSlctHora");

                if (chk.Checked)
                {
                    var idAgendOrig = int.Parse(((HiddenField)l.FindControl("hidCoAgenda")).Value);
                    var dia = ((HiddenField)l.FindControl("hidDia")).Value;
                    var coAlu = int.Parse(((HiddenField)l.FindControl("hidCoAlu")).Value);
                    var coCol = int.Parse(ddlNomeUsu.SelectedValue);
                    var hora = ((HiddenField)l.FindControl("hidHora")).Value;

                    var procs = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                               where tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idAgendOrig
                               select tbs389).ToList();

                    var hrs = CarregarHorarios(coCol, coAlu, hora, dia);

                    foreach (var h in hrs)
                    {
                        int coAgend = h.CO_AGEND;

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
        /// Responsável por carregar os profissionais
        /// </summary>
        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlNomeUsu, 0, false, "0", true);
        }

        /// <summary>
        /// Carrega a primeira data de início e final para o paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_COL"></param>
        private void CarregaDatasIniFimProficional(int CO_COL)
        {
            txtDtIniHistoUsuar.Text = DateTime.Now.AddMonths(-2).ToString();
            txtDtFimHistoUsuar.Text = DateTime.Now.ToString();
            var dtFim = new DateTime(DateTime.Now.Year, 1, 1);
            //var dtFim = new DateTime(DateTime.Now.AddYears(1).Year, 1, 1);
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where tbs174.CO_COL == CO_COL && tbs174.CO_ALU != null && tbs174.DT_AGEND_HORAR < dtFim
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
        private void CarregaGridHistoricoPaciente(int CO_COL)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHistoUsuar.Text) ? DateTime.Parse(txtDtIniHistoUsuar.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHistoUsuar.Text) ? DateTime.Parse(txtDtFimHistoUsuar.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.CO_COL == CO_COL
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                       //&&   a.DT_AGEND_HORAR >= dtIni && a.DT_AGEND_HORAR <= dtFim
                       select new HorarioHistoricoPaciente
                       {
                           CO_AGEND = tbs174.ID_AGEND_HORAR,
                           CO_ALU = tb07.CO_ALU,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           OPER = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : " - ",
                           STATUS = tbs174.CO_SITUA_AGEND_HORAR,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR
                       }).OrderBy(w => w.NO_PAC_RECEB).ThenBy(w => w.DT).ThenBy(w => w.HR).ToList();

            grdHistorPaciente.DataSource = res;
            grdHistorPaciente.DataBind();
        }
        
        public class HorarioHistoricoPaciente
        {
            public int CO_AGEND { get; set; }
            public int CO_ALU { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - "
                        + (this.NO_PAC_RECEB.Length > 45 ? this.NO_PAC_RECEB.Substring(0, 45) + "..." : this.NO_PAC_RECEB));
                }
            }
            public int NU_NIRE { get; set; }
            public string NO_PAC_RECEB { get; set; }
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
        }

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private List<HorarioSaida> CarregarHorarios(int coCol, int coAlu, string horas, string dia)
        {
            DateTime dtIni = new DateTime(2016, 1, 4);
            DateTime dtFim = new DateTime(2016, 12, 23);
            
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
                           CO_AGEND = a.ID_AGEND_HORAR,
                           CO_COL = a.CO_COL,
                           NO_COL = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                           CO_TP_AGEND = tb03.CO_CLASS_PROFI,
                           CO_ALU = coAlu
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
            public string DIA
            {
                get
                {
                    return dt.ToString("ddd", new CultureInfo("pt-BR"));
                }
            }
            public string hora
            {
                get
                {
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + this.DIA;
                }
            }
            public int CO_AGEND { get; set; }
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
                        case "T":
                            tipo = "Terapia Ocupacional";
                            break;
                        case "O":
                            tipo = "Outros";
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
        
        protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var CO_COL = int.Parse(ddlNomeUsu.SelectedValue);
                CarregaDatasIniFimProficional(CO_COL);
                CarregaGridHistoricoPaciente(CO_COL);
            }
        }
    }
}