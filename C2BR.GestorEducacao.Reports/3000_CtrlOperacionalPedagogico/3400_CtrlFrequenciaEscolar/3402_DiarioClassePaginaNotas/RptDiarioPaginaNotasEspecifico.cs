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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3403_ClassesDiarioNotas;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3402_DiarioClassePaginaNotas
{
    public partial class RptDiarioPaginaNotasEspecifico : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDiarioPaginaNotasEspecifico()
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
                            DateTime dataInicial,
                            DateTime dataFinal,
                            string strProfessorCod,
                            string strProfessor,
                            string strMateria,
                            DateTime dtIniBim,
                            DateTime dtFimBim,
                            bool ImprimeMedias,
                            bool PresenAst,
                            bool AssinFreq
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
                                         NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
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
                                         NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
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

                var lst = (from tb132 in ctx.TB132_FREQ_ALU
                           join tb07 in ctx.TB07_ALUNO on tb132.TB07_ALUNO.CO_ALU equals tb07.CO_ALU
                           join tb08 in ctx.TB08_MATRCUR on tb07.CO_ALU equals tb08.CO_ALU
                           join tb083 in ctx.TB83_PARAMETRO on tb08.CO_EMP equals tb083.CO_EMP
                           join tb03Coord in ctx.TB03_COLABOR on tb083.CO_COOR1 equals tb03Coord.CO_COL into lco
                           from lCoord in lco.DefaultIfEmpty()
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb06 in ctx.TB06_TURMAS on tb08.CO_TUR equals tb06.CO_TUR
                           join tb129 in ctx.TB129_CADTURMAS on tb06.CO_TUR equals tb129.CO_TUR
                           join tb02 in ctx.TB02_MATERIA on tb132.CO_MAT equals tb02.CO_MAT
                           join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb079 in ctx.TB079_HIST_ALUNO on tb132.CO_MAT equals tb079.CO_MAT
                           join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_MAT equals tb43.CO_MAT
                           join tbtrans in ctx.TB_TRANSF_INTERNA on tb132.TB07_ALUNO.CO_ALU equals tbtrans.TB07_ALUNO.CO_ALU into l1
                           from ltbt in l1.DefaultIfEmpty()
                           where tb08.CO_EMP == strP_CO_EMP
                           && tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb08.CO_CUR == strP_CO_CUR
                           && tb08.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                           && tb08.CO_SIT_MAT != "C"
                           && tb132.FL_HOMOL_FREQU == "S"
                           && tb132.TB01_CURSO.CO_EMP == tb08.CO_EMP
                           && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == tb08.TB44_MODULO.CO_MODU_CUR
                           && tb132.TB01_CURSO.CO_CUR == tb08.CO_CUR
                           && tb132.CO_TUR == strP_CO_TUR
                           && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                           && ((tb132.DT_FRE >= dtIniBim) && (tb132.DT_FRE <= dtFimBim))
                           && tb079.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb079.CO_MAT == tb132.CO_MAT
                           && tb079.CO_CUR == tb132.TB01_CURSO.CO_CUR
                           && tb079.CO_ANO_REF == strP_CO_ANO_REFER
                           && tb079.CO_ALU == tb132.TB07_ALUNO.CO_ALU
                           && (ltbt != null ? ltbt.CO_TURMA_ATUAL == tb132.CO_TUR : 0 == 0)
                           && tb43.CO_EMP == tb079.CO_EMP
                           && tb43.CO_MAT == tb079.CO_MAT
                           && tb43.CO_CUR == tb079.CO_CUR
                           && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                           select new DiarioNotasEspecifico
                           {
                               noModuCur = tb08.TB44_MODULO.DE_MODU_CUR,
                               noCur = tb01.NO_CUR,
                               coPeriTur = tb06.CO_PERI_TUR,
                               noTurma = tb129.NO_TURMA,
                               Disciplina = tb107.NO_MATERIA,
                               noAlu = tb07.NO_ALU,
                               coAlu = tb07.CO_ALU,
                               nuNire = tb07.NU_NIRE,
                               coSituAlu = tb08.CO_SIT_MAT,
                               coBimestre = strP_BIMESTRE,
                               //mesBimestre = strP_MES,
                               coAtiv = tb132.CO_ATIV_PROF_TUR,
                               dtInicialBimestre = dtIniBim,
                               dtFinalBimestre = dtFimBim,
                               professorCod = strProfessorCod,
                               professor = strProfessor,
                               materia = strMateria,
                               turmaUnica = tb06.CO_FLAG_RESP_TURMA,

                               noCol = noCol,
                               noCoord = (lCoord != null ? lCoord.NO_COL : ""),
                               
                               FL_AGRUPADORA_V = tb43.FL_DISCI_AGRUPA,
                               CO_MODU_CUR = tb08.TB44_MODULO.CO_MODU_CUR,
                               CO_CUR = tb01.CO_CUR,
                               CO_ANO_R = tb08.CO_ANO_MES_MAT,
                               ID_MATERIA = tb107.ID_MATERIA,
                               CO_MAT = tb02.CO_MAT,

                               VL_NOTA_BIM = (strP_BIMESTRE == "B1" ? tb079.VL_MEDIA_BIM1 : strP_BIMESTRE == "B2" ? tb079.VL_MEDIA_BIM2 :
strP_BIMESTRE == "B3" ? tb079.VL_MEDIA_BIM3 : tb079.VL_MEDIA_BIM4),
                               VL_RECU_BIM = (strP_BIMESTRE == "B1" ? tb079.VL_RECU_BIM1 : strP_BIMESTRE == "B2" ? tb079.VL_RECU_BIM2 :
                               strP_BIMESTRE == "B3" ? tb079.VL_RECU_BIM3 : tb079.VL_RECU_BIM4),
                               VL_MEDIA_FINAL = tb079.VL_MEDIA_FINAL,
                               VL_RECUP_FINAL = tb079.VL_PROVA_FINAL,

                               VL_CONSE_BIM = (strP_BIMESTRE == "B1" ? tb079.VL_CONSE_BIM1 : strP_BIMESTRE == "B2" ? tb079.VL_CONSE_BIM2 :
strP_BIMESTRE == "B3" ? tb079.VL_CONSE_BIM3 : tb079.VL_CONSE_BIM4),
                               VL_CONSE_FINAL = tb079.VL_CONSE_FINAL,

                           }).DistinctBy(ha => ha.coAlu).OrderBy(o => o.noAlu).ToList();

                //Ordena os alunos colocando os transferidos no final da listagem
                var res = lst.OrderBy(w => w.ORD_SITU).ThenBy(w => w.noAlu).ToList();

                //Inicializa array para guardar as datas que existem lançamentos de frequências
                List<DateTime> dataExisList = new List<DateTime>();

                #endregion


                if (res == null || res.Count() <= 0)
                    return null;


                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                int aux = 0;
                foreach (DiarioNotasEspecifico at in res)
                {
                    aux++;
                    at.Num = aux;
                    bsReport.Add(at);
                }

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
    }
}