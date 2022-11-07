using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas
{
    public partial class RptQuadroAgendamentos2A4 : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptQuadroAgendamentos2A4()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int UnidadeCadastro,
                              int UnidadeContrato,
                              int Especialidade,
                              string ClassificacaoProfissional,
                              int ProfissionalSaude,
                              int UnidadeConsulta,
                              int DeptLocal,
                              int EspecialidadeConsulta,
                              string noSituacao,
                              string noStatus,
                              string dataIni,
                              string dataFim,
                              bool tirarSab,
                              bool tirarDom,
                              bool VerValor,
            //int Unidade,
            //int LocalDept,
            //int Espec,
            //int Medico,
            //string status,
            //string situa,
            //string dataIni,
            //string dataFim,
                              string horariosLivres,
                              string NO_RELATORIO,
                              bool Pasta
                               )
        {
            try
            {
                #region Setar o Header e as Labels

                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }
                //for (DateTime i = dataIni1; i == dataFim1; i++)
                //{

                //}

                //while (dataIni1 == dataFim1)
                //{
                //    string diaSemana = dataIni1.ToString("ddd", new CultureInfo("pt-BR"));
                //    switch (diaSemana)
                //    {
                //        case "seg":
                //            lblSegunda.Text = "SEGUNDA -" + dataIni1.AddDays(1).ToShortDateString();
                //            break;
                //        case "ter":
                //            lblTerca.Text = "TERÇA -" + dataIni1.AddDays(2).ToShortDateString();
                //            break;
                //        case "qua":
                //            lblQuarta.Text = "QUARTA -" + dataIni1.AddDays(3).ToShortDateString();
                //            break;
                //        case "qui":
                //            lblQuinta.Text = "QUINTA -" + dataIni1.AddDays(4).ToShortDateString();
                //            break;
                //        case "sex":
                //            lblSexta.Text = "SEXTA -" + dataIni1.AddDays(5).ToShortDateString();
                //            break;
                //        case "sab":
                //            LblSabado.Text = "SÁBADO -" + dataIni1.AddDays(6).ToShortDateString();
                //            break;
                //        case "dom":
                //            lblDomingo.Text = "DOMINGO -" + dataIni1.ToShortDateString();
                //            break;
                //        default:
                //            break;
                //    }
                //}


                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                lblTitulo.Text = NO_RELATORIO;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Grade Horário

                DateTime DataInicial = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                var resultado = (from tbs174 in ctx.TBS174_AGEND_HORAR
                                 where
                           ((Especialidade != 0 ? tbs174.CO_ESPEC == Especialidade : 0 == 0)
                           && (ProfissionalSaude != 0 ? tbs174.CO_COL == ProfissionalSaude : 0 == 0)
                           && (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                           && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                           && (EspecialidadeConsulta != 0 ? tbs174.CO_EMP == EspecialidadeConsulta : 0 == 0)
                           && (noSituacao != "0" ? (noSituacao == "G" ? tbs174.CO_ALU != null : tbs174.CO_SITUA_AGEND_HORAR == noSituacao) : 0 == 0)
                           && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0) 
                           //|| tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONSU_AVALIA == "S"
                           )
                           && (tbs174.DT_AGEND_HORAR >= DataInicial && tbs174.DT_AGEND_HORAR <= DataFinal)
                                 select new
                                 {
                                     horarioAgenda = tbs174.HR_AGEND_HORAR,
                                 }).ToList().OrderBy(p => p.horarioAgenda).Distinct();

                // Erro: não encontrou registros
                if (resultado.ToList().Count == 0)
                    return -1;

                var lstGradeHorario = new List<GradeHorario>();
                System.Globalization.CultureInfo culInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                var agend = (from tbs174 in ctx.TBS174_AGEND_HORAR
                             join tb25 in ctx.TB25_EMPRESA on tbs174.CO_EMP equals tb25.CO_EMP into l1
                             from lemp in l1.DefaultIfEmpty()
                             join tb07 in ctx.TB07_ALUNO on tbs174.CO_ALU equals tb07.CO_ALU into l2
                             from lPac in l2.DefaultIfEmpty()
                             join tb14 in ctx.TB14_DEPTO on tbs174.CO_DEPT equals tb14.CO_DEPTO
                             where ((Especialidade != 0 ? tbs174.CO_ESPEC == Especialidade : 0 == 0)
                                    && (ProfissionalSaude != 0 ? tbs174.CO_COL == ProfissionalSaude : 0 == 0)
                                    && (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                                    && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                                    && (EspecialidadeConsulta != 0 ? tbs174.CO_EMP == EspecialidadeConsulta : 0 == 0)
                                    && (noSituacao != "0" ? (noSituacao == "G" ? tbs174.CO_ALU != null : tbs174.CO_SITUA_AGEND_HORAR == noSituacao) : 0 == 0)
                                    && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0)
                                    //|| tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONSU_AVALIA == "S"
                                    )
                                    && (tbs174.DT_AGEND_HORAR >= DataInicial && tbs174.DT_AGEND_HORAR <= DataFinal)
                             select new
                             {
                                 local = tb14.CO_SIGLA_DEPTO,
                                 Status = tbs174.CO_SITUA_AGEND_HORAR,
                                 flConfirm = tbs174.FL_CONF_AGEND,
                                 flJustifCance = tbs174.FL_JUSTI_CANCE,
                                 flEncam = tbs174.FL_AGEND_ENCAM,
                                 dataAgenda = tbs174.DT_AGEND_HORAR,
                                 horarioAgenda = tbs174.HR_AGEND_HORAR,
                                 unid = lemp.sigla,
                                 noPac = (lPac != null ? !String.IsNullOrEmpty(lPac.NO_APE_ALU) ? lPac.NO_APE_ALU : lPac.NO_ALU : "***"),
                                 coAul = (lPac != null && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" && tbs174.FL_JUSTI_CANCE != "C" && tbs174.FL_JUSTI_CANCE != "P" : "" == "") ? lPac.CO_ALU : (int?)null),
                                 NU_NIRE = (lPac != null ? lPac.NU_NIRE : 0),
                                 coProc = tbs174.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                                 TP_AGENDAMENTO = tbs174.TP_CONSU,
                                 nrAcao = (tbs174.TBS386_ITENS_PLANE_AVALI != null ? tbs174.TBS386_ITENS_PLANE_AVALI.NR_ACAO : (int?)null),
                                 pasta = Pasta ? lPac.DE_PASTA_CONTR : "",
                                 consuAvalia = tbs174.FL_CONSU_AVALIA
                             }).ToList();

                agend = agend.DistinctBy(p => p.noPac + p.consuAvalia + p.coAul + p.dataAgenda + p.horarioAgenda).ToList();

                if (agend.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        GradeHorario g = new GradeHorario();
                        g.HrInicio = item.horarioAgenda;

                        g.noPac0 =
                        g.noPac1 =
                        g.noPac2 =
                        g.noPac3 =
                        g.noPac4 = "---";

                        foreach (var iAgend in agend)
                        {
                            string d1, d2, d3, d4, d5;
                            int a = 0, b = 0;

                            if (tirarSab)
                                a++;

                            if (tirarDom)
                                a++;

                            if (a > 0 && ((dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Saturday && tirarSab) || (dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Sunday && tirarDom)))
                                b = a + b;

                            d1 = dataIni1.AddDays(b).ToShortDateString();
                            lblSegunda.Text = dataIni1.AddDays(b).ToShortDateString() + "           (" + dataIni1.AddDays(b).ToString("dddd", new CultureInfo("pt-BR")).ToUpper() + ")";

                            b++;

                            if (a > 0 && ((dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Saturday && tirarSab) || (dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Sunday && tirarDom)))
                                b = a + b;

                            d2 = dataIni1.AddDays(b).ToShortDateString();
                            lblTerca.Text = dataIni1.AddDays(b).ToShortDateString() + "             (" + dataIni1.AddDays(b).ToString("dddd", new CultureInfo("pt-BR")).ToUpper() + ")";

                            b++;

                            if (a > 0 && ((dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Saturday && tirarSab) || (dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Sunday && tirarDom)))
                                b = a + b;

                            d3 = dataIni1.AddDays(b).ToShortDateString();
                            lblQuarta.Text = dataIni1.AddDays(b).ToShortDateString() + "             (" + dataIni1.AddDays(b).ToString("dddd", new CultureInfo("pt-BR")).ToUpper() + ")";

                            b++;

                            if (a > 0 && ((dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Saturday && tirarSab) || (dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Sunday && tirarDom)))
                                b = a + b;

                            d4 = dataIni1.AddDays(b).ToShortDateString();
                            lblQuinta.Text = dataIni1.AddDays(b).ToShortDateString() + "             (" + dataIni1.AddDays(b).ToString("dddd", new CultureInfo("pt-BR")).ToUpper() + ")";

                            b++;

                            if (a > 0 && ((dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Saturday && tirarSab) || (dataIni1.AddDays(b).DayOfWeek == DayOfWeek.Sunday && tirarDom)))
                                b = a + b;

                            d5 = dataIni1.AddDays(b).ToShortDateString();
                            lblSexta.Text = dataIni1.AddDays(b).ToShortDateString() + "              (" + dataIni1.AddDays(b).ToString("dddd", new CultureInfo("pt-BR")).ToUpper() + ")";

                            //Coloca as informações apenas se a agenda for para este horário
                            if (iAgend.horarioAgenda == item.horarioAgenda)
                            {
                                //Prepara o texto para as celulas
                                //string textoCelulas = "";
                                string textoNeriNome = "";
                                string textoProc = "";
                                string local = "";
                                string tpAgend = "";
                                string pasta = "";

                                if (!iAgend.coAul.HasValue)
                                {
                                    textoProc = horariosLivres;
                                    local = iAgend.local;
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(iAgend.noPac))
                                    {
                                        var n = iAgend.noPac.Split(' ');
                                        textoNeriNome = n.Length > 1 ? (n[0] + " " + (n[1].Length <= 3 ? n[1] + (n.Length > 2 ? " " + n[2] : "") : n[1])) : n[0];
                                    }

                                       textoProc = "AG";
                                       tpAgend = "Avaliação";
                                       if (iAgend.consuAvalia != "S")
                                       {
                                           textoProc = RecuperaSituacaoAgenda(iAgend.Status, iAgend.flConfirm, iAgend.flEncam, iAgend.flJustifCance);
                                           tpAgend = iAgend.TP_AGENDAMENTO != null ? iAgend.TP_AGENDAMENTO.Equals("P") ? "Procedimento" : iAgend.TP_AGENDAMENTO.Equals("E") ? "Exame" : iAgend.TP_AGENDAMENTO.Equals("N") ?
                                               "Consulta" : iAgend.TP_AGENDAMENTO.Equals("V") ? "Vacina" : iAgend.TP_AGENDAMENTO.Equals("C") ? "Cirurgia" : "Outro" : "-";
                                       }

                                    local = !string.IsNullOrEmpty(iAgend.pasta) ? iAgend.local + " - " + iAgend.pasta : iAgend.local;
                                }

                                if (iAgend.dataAgenda.ToShortDateString() == d1)
                                {
                                    if (g.noPac0.Contains(horariosLivres) && textoProc != horariosLivres)
                                        g.noPac0 = g.noPac0.Replace(horariosLivres, "");

                                    if (g.noPac0 == "---" || String.IsNullOrEmpty(g.noPac0))
                                        g.noPac0 = textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                    else if (textoProc != horariosLivres)
                                        //g.noPac0 += " / " + textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                        g.noPac0 += (!string.IsNullOrEmpty(textoNeriNome)) ? " \n/ " + textoNeriNome + " (" + textoProc + ") " : string.Empty;

                                    g.local0 = ObterLocal(g.local0, iAgend.pasta, iAgend.local);
                                    g.tpAgend0 = ObterTpAgenda(tpAgend, g.tpAgend0);
                                    g.noPasta0 = ObterPasta(pasta, g.noPasta0);
                                }
                                else if (iAgend.dataAgenda.ToShortDateString() == d2)
                                {
                                    if (g.noPac1.Contains(horariosLivres) && textoProc != horariosLivres)
                                        g.noPac1 = g.noPac1.Replace(horariosLivres, "");

                                    if (g.noPac1 == "---" || String.IsNullOrEmpty(g.noPac1))
                                        g.noPac1 = textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                    else if (textoProc != horariosLivres)
                                        //g.noPac1 += " / " + textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                        g.noPac1 += " / ...";

                                    g.local1 = ObterLocal(g.local1, iAgend.pasta, iAgend.local);
                                    g.tpAgend1 = ObterTpAgenda(tpAgend, g.tpAgend1);
                                    g.noPasta1 = ObterPasta(pasta, g.noPasta1);

                                    //g.local1 = !string.IsNullOrEmpty(iAgend.pasta) ? iAgend.local + " - " + iAgend.pasta : iAgend.local; ;
                                    //g.tpAgend1 = tpAgend;
                                    //g.noPasta1 = pasta;
                                }
                                else if (iAgend.dataAgenda.ToShortDateString() == d3)
                                {
                                    if (g.noPac2.Contains(horariosLivres) && textoProc != horariosLivres)
                                        g.noPac2 = g.noPac2.Replace(horariosLivres, "");

                                    if (g.noPac2 == "---" || String.IsNullOrEmpty(g.noPac2))
                                        g.noPac2 = textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                    else if (textoProc != horariosLivres)
                                        //g.noPac2 += " / " + textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                        g.noPac2 += " / ...";

                                    g.local2 = ObterLocal(g.local2, iAgend.pasta, iAgend.local);
                                    g.tpAgend2 = ObterTpAgenda(tpAgend, g.tpAgend2);
                                    g.noPasta2 = ObterPasta(pasta, g.noPasta2);
                                }
                                else if (iAgend.dataAgenda.ToShortDateString() == d4)
                                {
                                    if (g.noPac3.Contains(horariosLivres) && textoProc != horariosLivres)
                                        g.noPac3 = g.noPac3.Replace(horariosLivres, "");

                                    if (g.noPac3 == "---" || String.IsNullOrEmpty(g.noPac3))
                                        g.noPac3 = textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                    else if (textoProc != horariosLivres)
                                        //g.noPac3 += " / " + textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                        g.noPac3 += " / ...";

                                    g.local3 = ObterLocal(g.local3, iAgend.pasta, iAgend.local);
                                    g.tpAgend3 = ObterTpAgenda(tpAgend, g.tpAgend3);
                                    g.noPasta3 = ObterPasta(pasta, g.noPasta3);
                                }
                                else if (iAgend.dataAgenda.ToShortDateString() == d5)
                                {
                                    if (g.noPac4.Contains(horariosLivres) && textoProc != horariosLivres)
                                        g.noPac4 = g.noPac4.Replace(horariosLivres, "");

                                    if (g.noPac4 == "---" || String.IsNullOrEmpty(g.noPac4))
                                        g.noPac4 = textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                    else if (textoProc != horariosLivres)
                                        //g.noPac4 += " / " + textoNeriNome + (textoProc != horariosLivres ? " (" + textoProc + ")" : textoProc);
                                        g.noPac4 += " / ...";

                                    g.local4 = ObterLocal(g.local4, iAgend.pasta, iAgend.local);
                                    g.tpAgend4 = ObterTpAgenda(tpAgend, g.tpAgend4);
                                    g.noPasta4 = ObterPasta(pasta, g.noPasta4);
                                }
                            }
                        }

                        lstGradeHorario.Add(g);
                    }
                }

                //this.lblLegenda.Text = strLegenda + " )";
                var res = lstGradeHorario.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (GradeHorario at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Métodos privados
       
        /// <summary>
        /// Retorna uma string formatada com os dados de local
        /// </summary>
        private string ObterLocal(string local, string iAgendPasta, string iAgendLocal)
        {

            var resultado = string.Empty;

            if (string.IsNullOrEmpty(local))
            {
                local = !string.IsNullOrEmpty(iAgendPasta) ? iAgendLocal + " - " + iAgendPasta : iAgendLocal;
            }
            else
            {
                local += " \n/" + iAgendLocal + ((!string.IsNullOrEmpty(iAgendPasta)) ? " - " + iAgendPasta : string.Empty);
            }

            return local;
        }
        /// <summary>
        /// Retorna uma string formatada com os dados de TpAgenda
        /// </summary>
        private string ObterTpAgenda(string tpAgenda, string gTpAgenda)
        {

            if (string.IsNullOrEmpty(gTpAgenda))
            {
                gTpAgenda = tpAgenda;
            }
            else
            {
                gTpAgenda += "\n/" + tpAgenda;
            }

            return gTpAgenda;
        }

        /// <summary>
        /// Retorna uma string formatada com o nome da pasta
        /// </summary>
        private string ObterPasta(string pasta, string noPasta)
        {
            if (string.IsNullOrEmpty(noPasta))
            {
                noPasta = pasta;
            }
            else
            {
                noPasta += "\n/" + pasta;
            }

            return noPasta;
        }

        #endregion

        #region Classe Grade de Horário do Relatorio

        /// <summary>
        /// Retorna o nome da situação
        /// </summary>
        /// <param name="CO_SITUA"></param>
        private string RecuperaSituacaoAgenda(string CO_SITUA, string FL_CONFIR, string FL_ENCAM, string fl_justif)
        {
            string s = "";
            switch (CO_SITUA)
            {
                case "C":
                    //Se estiver como cancelado, verifica se foi falta justificada ou não
                    s = fl_justif != null ? (fl_justif == "S" ? "FJ" : "FA") : "CA";
                    break;
                case "A":
                    //Se estiver ativo, verifica se está como presente e encaminhado, ou apenas agendado
                    if (FL_ENCAM == "S")
                        s = "EN";
                    else
                        s = (FL_CONFIR == "S" ? "PR" : "AG");
                    break;
                case "R":
                    //Se estiver R, foi realizada
                    s = "RE";
                    break;
                default:
                    s = "**";
                    break;
            }
            return s;
        }

        public class GradeHorario
        {
            public string Status { get; set; }
            public string HrInicio { get; set; }
            public string HrFim { get; set; }
            public string Turno { get; set; }
            public int Tempo { get; set; }
            //-----------------------------------------------------------------------------------
            public string nire0 { get; set; }
            public string nire1 { get; set; }
            public string nire2 { get; set; }
            public string nire3 { get; set; }
            public string nire4 { get; set; }
            public string nire5 { get; set; }
            public string nire6 { get; set; }
            //-----------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------
            public string local0 { get; set; }
            public string local1 { get; set; }
            public string local2 { get; set; }
            public string local3 { get; set; }
            public string local4 { get; set; }
            public string local5 { get; set; }
            public string local6 { get; set; }
            //-----------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------
            public string noPac0 { get; set; }
            public string noPac1 { get; set; }
            public string noPac2 { get; set; }
            public string noPac3 { get; set; }
            public string noPac4 { get; set; }
            //-----------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------
            public string noPasta0 { get; set; }
            public string noPasta1 { get; set; }
            public string noPasta2 { get; set; }
            public string noPasta3 { get; set; }
            public string noPasta4 { get; set; }
            //-----------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------
            public string tpAgend0 { get; set; }
            public string tpAgend1 { get; set; }
            public string tpAgend2 { get; set; }
            public string tpAgend3 { get; set; }
            public string tpAgend4 { get; set; }
            //-----------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------

            public string Disciplina2 { get; set; }
            public string Disciplina3 { get; set; }
            public string Disciplina4 { get; set; }
            public string Disciplina5 { get; set; }
            public string Disciplina6 { get; set; }
            public string Disciplina7 { get; set; }

            public string TempoAulaDesc
            {
                get
                {
                    return this.Tempo.ToString() + "º Tempo - " + this.HrInicio + " / " + this.HrFim;
                }
            }
        }

        #endregion
    }
}
