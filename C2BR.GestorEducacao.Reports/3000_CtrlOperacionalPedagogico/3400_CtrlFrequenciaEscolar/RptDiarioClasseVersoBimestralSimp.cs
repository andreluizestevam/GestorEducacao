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


namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar
{
    public partial class RptDiarioClasseVersoBimestralSimp : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDiarioClasseVersoBimestralSimp()
        {
            InitializeComponent();
        }

        #region Init Report

        public XtraReport InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string infos,
                              string strP_CO_ANO_REFER,
                              string strP_BIMESTRE,
                              int strP_CO_MAT,
                              string strP_PROF_RESP,
            //int strP_MES,
                              DateTime dataInicial,
                              DateTime dataFinal,
                              string strProfessorCod,
                              string strProfessor,
                              string strMateria,
                              DateTime dtIniBim,
                              DateTime dtFimBim
                                )
        {
            try
            {
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                decimal dtRef = decimal.Parse(strP_CO_ANO_REFER);
                int intAno = int.Parse(strP_CO_ANO_REFER);

                #region Retorna as datas de lançamento de frequência

                ///Periodo do bimestre
                var unidade = (from tb82 in ctx.TB82_DTCT_EMP
                               where tb82.CO_EMP == strP_CO_EMP
                               select tb82
                                  ).FirstOrDefault();


                DateTime dtIni = new DateTime();
                DateTime dtFim = new DateTime();

                switch (strP_BIMESTRE)
                {
                    case "B1":
                        dtIni = unidade.DT_LACTO_INICI_BIM1 ?? new DateTime();
                        dtFim = unidade.DT_LACTO_FINAL_BIM1 ?? new DateTime();
                        break;
                    case "B2":
                        dtIni = unidade.DT_LACTO_INICI_BIM2 ?? new DateTime();
                        dtFim = unidade.DT_LACTO_FINAL_BIM2 ?? new DateTime();
                        break;
                    case "B3":
                        dtIni = unidade.DT_LACTO_INICI_BIM3 ?? new DateTime();
                        dtFim = unidade.DT_LACTO_FINAL_BIM3 ?? new DateTime();
                        break;
                    case "B4":
                        dtIni = unidade.DT_LACTO_INICI_BIM4 ?? new DateTime();
                        dtFim = unidade.DT_LACTO_FINAL_BIM4 ?? new DateTime();
                        break;
                }
                ///
                #endregion

                string coFlagResp = (from coFlag in ctx.TB06_TURMAS where coFlag.CO_EMP == strP_CO_EMP && coFlag.CO_MODU_CUR == strP_CO_MODU_CUR && coFlag.CO_CUR == strP_CO_CUR && coFlag.CO_TUR == strP_CO_TUR select new { coFlag.CO_FLAG_RESP_TURMA }).FirstOrDefault().CO_FLAG_RESP_TURMA;

                string noCol = "";

                #region Retorna o nome do professor
                if ((from resp in ctx.TB_RESPON_MATERIA
                     where resp.CO_MODU_CUR == strP_CO_MODU_CUR
                         && resp.CO_CUR == strP_CO_CUR
                         && resp.CO_TUR == strP_CO_TUR
                         && resp.CO_ANO_REF == intAno
                         && coFlagResp != "S" ? resp.CO_MAT == strP_CO_MAT : 0 == 0
                     select resp).Any())
                {
                    if (coFlagResp == "S")
                    {
                        if ((from resp in ctx.TB_RESPON_MATERIA join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == intAno select new { tb03.NO_COL }).Any())
                        {
                            noCol = (from resp in ctx.TB_RESPON_MATERIA
                                     join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL
                                     where resp.CO_MODU_CUR == strP_CO_MODU_CUR
                                     && resp.CO_CUR == strP_CO_CUR
                                     && resp.CO_TUR == strP_CO_TUR
                                     && resp.CO_ANO_REF == intAno
                                     select new
                                     {
                                         NO_COL = tb03.NO_COL
                                     }).FirstOrDefault().NO_COL;
                        }
                    }
                    else
                    {
                        if ((from resp in ctx.TB_RESPON_MATERIA join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == intAno && resp.CO_MAT == strP_CO_MAT select new { tb03.NO_COL }).Any())
                        {
                            noCol = (from resp in ctx.TB_RESPON_MATERIA
                                     join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL
                                     where resp.CO_MODU_CUR == strP_CO_MODU_CUR
                                     && resp.CO_CUR == strP_CO_CUR
                                     && resp.CO_TUR == strP_CO_TUR
                                     && resp.CO_ANO_REF == intAno
                                     && (strP_CO_MAT != 0 ? resp.CO_MAT == strP_CO_MAT : resp.CO_MAT == null)
                                     select new
                                     {
                                         NO_COL = tb03.NO_COL
                                     }).FirstOrDefault().NO_COL;
                        }
                    }
                }
                #endregion
                //var queryCo = (from tb083 in ctx.TB83_PARAMETRO
                //               join tb03 in ctx.TB03_COLABOR on tb083.CO_DIR1 equals tb03.CO_COL
                //               where tb083.CO_EMP == strP_CO_EMP
                //               select new
                //               {
                //                   tb083.CO_DIR1,
                //                   coDir1 = tb083.CO_DIR1,
                //                   coSecre1 = tb083.TB03_COLABOR.CO_COL,
                //                   nomeDir1 = tb03.NO_COL,
                //                   nomeSecr1 = tb083.TB03_COLABOR.NO_COL
                //               }).ToList();

                var lstQtAulas = (from tb43 in ctx.TB43_GRD_CURSO
                                  where tb43.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                  && tb43.CO_CUR == strP_CO_CUR
                                  && tb43.CO_ANO_GRADE == strP_CO_ANO_REFER
                                  select new
                                  {
                                      tb43.QT_AULAS_BIM1,
                                      tb43.QT_AULAS_BIM2,
                                      tb43.QT_AULAS_BIM3,
                                      tb43.QT_AULAS_BIM4
                                  }).ToList().FirstOrDefault();

                var lst = (from tb119 in ctx.TB119_ATIV_PROF_TURMA
                           join tb083 in ctx.TB83_PARAMETRO on tb119.CO_EMP equals tb083.CO_EMP
                           join tb03 in ctx.TB03_COLABOR on tb083.CO_DIR1 equals tb03.CO_COL into l1
                           from ldir in l1.DefaultIfEmpty()
                           join tb44 in ctx.TB44_MODULO on tb119.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb06 in ctx.TB06_TURMAS on tb119.CO_TUR equals tb06.CO_TUR
                           join tb129 in ctx.TB129_CADTURMAS on tb06.CO_TUR equals tb129.CO_TUR
                           where tb119.FL_HOMOL_DIARIO == "S"
                           && tb119.CO_EMP == strP_CO_EMP
                           && tb119.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb119.CO_CUR == strP_CO_CUR
                           && tb119.CO_TUR == strP_CO_TUR
                           && tb119.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                           && (tb119.DT_ATIV_REAL >= dtIniBim && tb119.DT_ATIV_REAL <= dtFimBim)
                           //&& tb119.CO_BIMESTRE == strP_BIMESTRE
                           && (strP_CO_MAT == 0 ? 0 == 0 : tb119.CO_MAT == strP_CO_MAT)
                           && tb119.CO_TEMPO_ATIV != null
                               //&& (tb119.DT_ATIV_REAL.Month == strP_MES)
                           && tb083.CO_EMP == strP_CO_EMP


                           select new DiarioClasseVerso
                           {
                               noModuCur = tb44.DE_MODU_CUR,
                               coPeriTur = tb06.CO_PERI_TUR,
                               noTurma = tb129.NO_TURMA,
                               coMat = tb119.CO_MAT,
                               numTempo = tb119.CO_TEMPO_ATIV,
                               dtAtv = tb119.DT_ATIV_REAL,
                               deResAtv_R = tb119.DE_RES_ATIV,
                               deTemaAtv_R = tb119.DE_TEMA_AULA,
                               dtInicialBimestre = dtIniBim,
                               dtFinalBimestre = dtFimBim,
                               professorCod = strProfessorCod,
                               professor = strProfessor,
                               materia = strMateria,
                               nomeDir1 = (ldir != null ? ldir.NO_COL : ""),
                               nomeSecr1 = tb083.TB03_COLABOR.NO_COL


                           }).Distinct().OrderBy(o => o.dtAtv);

                //if (lst == null || lst.Count() <= 0)
                //    return null;

                if (lst.Count() > 0)
                {



                    int? codigoAluno = lst.First().coAluno;
                    if (codigoAluno != null)
                    {
                        lst = lst.Where(a => a.coAluno == codigoAluno).OrderBy(o => o.dtAtv);
                    }
                }



                var res = lst.ToList();

                //Inicializa array para guardar as datas que existem lançamentos de frequências
                List<DateTime> dataExisList = new List<DateTime>();

                int i = 1;
                foreach (DiarioClasseVerso dcv in res)
                {
                    dcv.dtIni = dtIni.ToString("dd/MM/yyyy");
                    dcv.dtFim = dtFim.ToString("dd/MM/yyyy");
                    dcv.AulasDadas = lst.Count();
                    dcv.qtAulasB1 = lstQtAulas.QT_AULAS_BIM1;
                    dcv.qtAulasB2 = lstQtAulas.QT_AULAS_BIM2;
                    dcv.qtAulasB3 = lstQtAulas.QT_AULAS_BIM3;
                    dcv.qtAulasB4 = lstQtAulas.QT_AULAS_BIM4;
                    dcv.noCol = noCol;
                    dcv.coBimestre = strP_BIMESTRE;

                    if (!VerificaExisteData(dataExisList, dcv.dtAtv))
                    {
                        dcv.Posicao = i.ToString();
                        i++;
                        dcv.DataAtiv = dcv.dtAtv.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        dcv.DataAtiv = "";
                        dcv.Posicao = "";
                    }
                }

                #endregion





                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (DiarioClasseVerso at in res)
                    bsReport.Add(at);

                return this;
            }
            catch { throw; }
        }

        #endregion

        /// <summary>
        /// Verifica se existe a data recebida como parâmetro na lista recebida como parâmetro. Também adiciona na lista a data recebida
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="dt"></param>
        /// <returns>Retorna TRUE caso exista e FALSO caso não exista</returns>
        private bool VerificaExisteData(List<DateTime> lst, DateTime dt)
        {
            //Adiciona na lista a data recebida
            bool tem = (lst.Where(w => w.Equals(dt)).Any());

            lst.Add(dt);

            return tem;
        }

        #region Classe Lista Pauta Chamada Verso


        public class DiarioClasseVerso
        {
            public string noCol { get; set; }
            public string nomeSecr1 { get; set; }
            public string nomeDir1 { get; set; }

            public string nomeProf
            {
                get
                {
                    return this.noCol.ToUpper();
                }
            }
            public int? coAluno { get; set; }
            public int? numTempo { get; set; }
            public string nomeTempo
            {
                get
                {
                    return ((this.numTempo != null && this.numTempo == 0) ? "Sem Regis" : this.numTempo.ToString() + "º");
                }
            }
            public string noModuCur { get; set; }
            public string noCur { get; set; }
            public string ModSerie
            {
                get
                {
                    return this.noModuCur + " / " + this.noCur;
                }
            }

            public string coPeriTur { get; set; }
            public string Turno
            {
                get
                {
                    string t = "";
                    switch (this.coPeriTur)
                    {
                        case "M":
                            t = "Matutino";
                            break;
                        case "V":
                            t = "Vespertino";
                            break;
                        case "N":
                            t = "Noturno";
                            break;
                    }
                    return t;
                }
            }

            public int mesBimestre { get; set; }

            public string mesBimestreE
            {

                get
                {
                    string nome = "";

                    switch (this.mesBimestre.ToString())
                    {
                        case "1":
                            nome = "Janeiro";
                            break;
                        case "2":
                            nome = "Fevereiro";
                            break;
                        case "3":
                            nome = "março";
                            break;
                        case "4":
                            nome = "Abril";
                            break;
                        case "5":
                            nome = "Maio";
                            break;
                        case "6":
                            nome = "Junho";
                            break;
                        case "7":
                            nome = "Julho";
                            break;
                        case "8":
                            nome = "Agosto";
                            break;
                        case "9":
                            nome = "Setembro";
                            break;
                        case "10":
                            nome = "Outubro";
                            break;
                        case "11":
                            nome = "Novembro";
                            break;
                        case "12":
                            nome = "Dezembro";

                            break;
                    }
                    return nome;
                }
            }

            public DateTime dtInicialBimestre { get; set; }
            public DateTime dtFinalBimestre { get; set; }
            public string professorCod { get; set; }
            public string professor { get; set; }
            public string materia { get; set; }

            public string ParametrosVerso1
            {
                get
                {
                    string bimestreD = "Matéria: " + this.materia.ToUpper() + " - Professor(a): " + ((this.nomeProf.ToUpper() != "") ? this.nomeProf.ToString() : "***") +" )";
                    return bimestreD;
                }

            }

            public string ParametrosVerso2
            {
                get
                {
                    string bimestreD = this.Bimestre + " (" + this.dtInicialBimestre.ToShortDateString() + " a " + this.dtFinalBimestre.ToShortDateString() + ") - Mês Referência: " + this.mesBimestreE.ToUpper() +
                        " - Aulas (Previstas/Realizadas):" + ((this.AulasPrev != null) ? this.AulasPrev.Value.ToString() : "**") + "/" + this.AulasDadas;
                    return bimestreD;
                }
            }

            public string noTurma { get; set; }
            public int coMat { get; set; }
            public string Disciplina
            {
                get
                {
                    if (this.coMat == 0)
                    {
                        var ctx = GestorEntities.CurrentContext;
                        var mat = (from tb02 in ctx.TB02_MATERIA
                                   where tb02.CO_MAT == this.coMat
                                   join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   select new
                                   {
                                       tb107.NO_MATERIA
                                   }
                            ).FirstOrDefault();
                        return (mat == null ? "" : mat.NO_MATERIA);
                    }
                    else
                        return "";
                }
            }

            public string coBimestre { get; set; }
            public string Bimestre
            {
                get
                {
                    string b = "";
                    switch (this.coBimestre)
                    {
                        case "B1":
                            b = "1° Bimestre";
                            break;
                        case "B2":
                            b = "2° Bimestre";
                            break;
                        case "B3":
                            b = "3° Bimestre";
                            break;
                        case "B4":
                            b = "4° Bimestre";
                            break;
                    }
                    return b;
                }
            }

            public int? qtAulasB1 { get; set; }
            public int? qtAulasB2 { get; set; }
            public int? qtAulasB3 { get; set; }
            public int? qtAulasB4 { get; set; }
            public int? AulasPrev
            {
                get
                {
                    int? b = 0;
                    switch (this.coBimestre)
                    {
                        case "B1":
                            b = this.qtAulasB1;
                            break;
                        case "B2":
                            b = this.qtAulasB2;
                            break;
                        case "B3":
                            b = this.qtAulasB3;
                            break;
                        case "B4":
                            b = this.qtAulasB4;
                            break;
                    }
                    return b;
                }
            }
            public int AulasDadas { get; set; }

            public string dtIni { get; set; }
            public string dtFim { get; set; }
            public DateTime dtAtv { get; set; }
            public string DataAtiv { get; set; }
            public string deResAtv_R { get; set; }
            public string deResAtv
            {
                get
                {
                    return this.deResAtv_R.ToUpper();
                }
            }
            public string deTemaAtv_R { get; set; }
            public string deTemaAtv
            {
                get
                {
                    return this.deTemaAtv_R.ToUpper();
                }
            }
            public string Posicao { get; set; }
            public string nomeSecretario
            {
                get
                {
                    string nome = (this.nomeSecr1 != null ? (this.nomeSecr1 != "") ? this.nomeSecr1.ToString() : "***" : "***");
                    return nome;
                }

            }
            public string nomeDiretor
            {
                get
                {
                    string nome = ((this.nomeDir1 != "") ? this.nomeDir1.ToString() : "***");

                    return nome;
                }
            }
            public string nomeProfessor
            {
                get
                {
                    string nome = ((this.nomeProf != "") ? this.nomeProf.ToString() : "***");
                    return nome;
                }
            }

        }
        #endregion
    }
}
