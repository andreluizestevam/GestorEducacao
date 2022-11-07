using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3800_CtrlOperacionalProfessores
{
    public partial class RptPlanoAulaSerieTurma : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptPlanoAulaSerieTurma()
        {
            InitializeComponent();
        }

        #region Init Report

        /// <summary>
        /// Esta função carrega o relatório de Atividades Realizadas do Professor
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="codEmp"></param>
        /// <param name="infos"></param>
        /// <param name="strP_CO_EMP"></param>
        /// <param name="strP_CO_COL"></param>
        /// <param name="strP_CO_ANO_MES_MAT"></param>
        /// <param name="strP_CO_MODU_CUR"></param>
        /// <param name="strP_CO_CUR"></param>
        /// <param name="strP_CO_TUR"></param>
        /// <returns></returns>
        public int InitReport(string parametros,
                                int codEmp,
                                string infos,
                                string strP_CO_EMP,
                                string strP_CO_MODU_CUR,
                                string strP_CO_CUR,
                                string strP_CO_TUR,
                                string strP_ID_MATERIA,
                                string strP_DT_INI,
                                string strP_DT_FIM,
                                string strP_TP_ATIV,
                                string strP_CO_COL,
                                string strP_TURMA,
                                string strP_SERIE)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                // Conversão das variáveis necessárias
                int intP_CO_EMP = int.Parse(strP_CO_EMP);
                int intP_CO_MODU_CUR = int.Parse(strP_CO_MODU_CUR);
                int intP_CO_CUR = int.Parse(strP_CO_CUR);
                int intP_CO_TUR = int.Parse(strP_CO_TUR);
                int intP_ID_MATERIA = strP_ID_MATERIA != "0" ? int.Parse(strP_ID_MATERIA) : 0;
                DateTime dtP_DT_INI = Convert.ToDateTime(strP_DT_INI + " 00:00:00");
                DateTime dtP_DT_FIM = Convert.ToDateTime(strP_DT_FIM + " 23:59:59");
                int intP_CO_COL = strP_CO_COL != "0" ? int.Parse(strP_CO_COL) : 0;
                #region Query

                var lst = (from tb17 in ctx.TB17_PLANO_AULA
                           join tb119 in ctx.TB119_ATIV_PROF_TURMA
                            on tb17.CO_ATIV_PROF_TUR equals tb119.CO_ATIV_PROF_TUR into t1
                           from tb119 in t1.DefaultIfEmpty()
                           join tb03 in ctx.TB03_COLABOR
                            on tb17.CO_COL equals tb03.CO_COL
                           join tb01 in ctx.TB01_CURSO
                            on tb17.CO_CUR equals tb01.CO_CUR
                           join tb06 in ctx.TB06_TURMAS
                            on tb17.CO_TUR equals tb06.CO_TUR
                           join tb129 in ctx.TB129_CADTURMAS
                            on tb17.CO_TUR equals tb129.CO_TUR
                           join tb02 in ctx.TB02_MATERIA
                            on tb17.CO_MAT equals tb02.CO_MAT
                           join tb107 in ctx.TB107_CADMATERIAS
                            on tb02.ID_MATERIA equals tb107.ID_MATERIA

                           where (intP_CO_CUR != 0 ? tb17.CO_CUR == intP_CO_CUR : 0 == 0)
                           && (intP_CO_TUR != 0 ? tb17.CO_TUR == intP_CO_TUR : 0 == 0)
                           && (intP_CO_EMP != 0 ? tb17.CO_EMP == intP_CO_EMP : 0 == 0)
                           && (intP_CO_MODU_CUR != 0 ? tb17.CO_MODU_CUR == intP_CO_MODU_CUR : 0 == 0)
                           && (tb17.DT_PREV_PLA >= dtP_DT_INI)
                           && (tb17.DT_PREV_PLA <= dtP_DT_FIM)
                           && (strP_ID_MATERIA != "0" ? tb107.ID_MATERIA == intP_ID_MATERIA : 0 == 0)
                           && (strP_CO_COL != "0" ? tb17.CO_COL == intP_CO_COL : 0 == 0)
                           && (strP_TP_ATIV != "T" ? tb17.FLA_HOMOLOG == strP_TP_ATIV : 0 == 0)

                           select new PlanoAulas
                           {
                               dataPrev = tb17.DT_PREV_PLA,
                               TA = tb17.NU_TEMP_PLA,
                               horaIni = tb17.HR_INI_AULA_PLA,
                               horaFim = tb17.HR_FIM_AULA_PLA,
                               MATERIA = tb107.NO_MATERIA,
                               TEMA = tb17.DE_TEMA_AULA,
                               qtHoras = tb17.QT_CARG_HORA_PLA,
                               OBJETIVO = "OBJETIVO: " + (tb17.DE_OBJE_AULA != null && tb17.DE_OBJE_AULA != "" ? tb17.DE_OBJE_AULA : "*****"),
                               Metodologia = "METODOLOGIA: " + (tb17.DE_METODOLOGIA != null && tb17.DE_METODOLOGIA != "" ? tb17.DE_METODOLOGIA : "*****"),
                               stHomolog = tb17.FLA_HOMOLOG,
                               stExec = tb17.FLA_EXECUTADA_ATIV,
                               PROFESSOR = tb03.NO_COL,
                               Serie = strP_SERIE,
                               Turma = strP_TURMA,
                               contAber = 0,
                               contCanc = 0,
                               contExec = 0,
                               stSituacao = tb17.CO_SITU_PLA
                           }
                           ).Distinct();

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                int cExec = res.Where(r => r.stExec).Count();
                int cCanc = res.Where(r => r.stSituacao == "C").Count();
                int cAber = res.Where(r => r.stExec == false).Count();

                foreach (PlanoAulas at in res)
                {
                    at.contAber = cAber;
                    at.contCanc = cCanc;
                    at.contExec = cExec;
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class PlanoAulas
        {
            public DateTime dataPrev { get; set; }
            public string DATA
            {
                get
                {
                    return dataPrev.Date.ToString("dd/MM/yyyy");
                }
            }
            public decimal? TA { get; set; }
            public string horaIni { get; set; }
            public string horaFim { get; set; }
            public decimal qtHoras { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public int CH
            {
                get
                {
                    return Convert.ToInt32(qtHoras);
                }
            }
            public int contExec { get; set; }
            public int contCanc { get; set; }
            public int contAber { get; set; }
            public string HORARIO
            {
                get
                {
                    return horaIni + "/" + horaFim;
                }
            }
            public string MATERIA { get; set; }
            public string TEMA { get; set; }
            public string OBJETIVO { get; set; }
            public string Metodologia { get; set; }
            public string stHomolog { get; set; }
            public bool stExec { get; set; }
            public string stSituacao { get; set; }
            public string STATUS
            {
                get
                {
                    string status = "";
                    if (stSituacao != "C")
                    {
                        if (!stExec)
                        {
                            status += "Em Aberto ";
                        }
                        else
                        {
                            status += "Executada ";
                        }

                        if (stHomolog == "N")
                        {
                            status += "Não Homologada";
                        }
                        else
                        {
                            status += "Homologada";
                        }
                    }
                    else
                    {
                        status = "Cancelado";
                    }

                    return status;
                }
            }
            public string PROFESSOR { get; set; }
        }

    }
}
