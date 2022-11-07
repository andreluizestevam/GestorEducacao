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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3404_ClassesDiario;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3405_ModelosDiario
{
    public partial class RptDiarioClasseFrenteBimSupremo : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDiarioClasseFrenteBimSupremo()
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

                //trata mostra/não mostra assinatura
                xrLabel7.Visible = xrLabel8.Visible = xrLabel9.Visible = xrLabel12.Visible = xrLabel10.Visible =
                    xrLabel1.Visible = xrLabel2.Visible = xrLabel3.Visible = xrLabel4.Visible = xrLabel11.Visible =
                    AssinFreq;

                #region Retorna as datas de lançamento de frequência

                var lstDtFreq = (from tb132 in ctx.TB132_FREQ_ALU
                                 where tb132.FL_HOMOL_FREQU == "S"
                                     //&& tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tb132.TB01_CURSO.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                                 && tb132.CO_TUR == strP_CO_TUR
                                 && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                                     //&& tb132.CO_BIMESTRE == strP_BIMESTRE
                                     //&& tb132.DT_FRE.Month == strP_MES
                                 && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                                     //&& ((tb132.DT_FRE.Month >= dtIniBim.Month) && (tb132.DT_FRE.Month <= dtFimBim.Month))
                                 && ((tb132.DT_FRE >= dtIniBim) && (tb132.DT_FRE <= dtFimBim))
                                 //&& tb132.CO_ATIV_PROF_TUR != 0

                                 select new DiarioFrente.RelDataFre
                                 {
                                     DT_FRE = tb132.DT_FRE,
                                     NR_TEMPO = tb132.NR_TEMPO,
                                     CO_ATIV = tb132.CO_ATIV_PROF_TUR
                                 }).Distinct().OrderBy(w => w.DT_FRE).ToList();

                if (lstDtFreq == null || lstDtFreq.Count() <= 0)
                    return null;
                
                // Pega a primeira data de lançamento
                DateTime dtIni = lstDtFreq.OrderBy(o => o.DT_FRE).First().DT_FRE;

                // Pega a ultima data de lançamento
                DateTime dtFim = lstDtFreq.OrderByDescending(o => o.DT_FRE).First().DT_FRE;
                #endregion

                string coFlagResp = (from coFlag in ctx.TB06_TURMAS where coFlag.CO_EMP == strP_CO_EMP && coFlag.CO_MODU_CUR == strP_CO_MODU_CUR && coFlag.CO_CUR == strP_CO_CUR && coFlag.CO_TUR == strP_CO_TUR select new { coFlag.CO_FLAG_RESP_TURMA }).FirstOrDefault().CO_FLAG_RESP_TURMA;

                string noCol = "";

                #region Retorna o nome do professor
                if ((from resp in ctx.TB_RESPON_MATERIA where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == intAno && coFlagResp != "S" ? resp.CO_MAT == strP_CO_MAT : 0 == 0 select resp).Any())
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
                                         NO_COL =  tb03.NO_COL
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

                #region Retorna as quantidades de aula cadastradas na grade do curso/série
                var lstQtAulas = (from tb43 in ctx.TB43_GRD_CURSO
                                  where tb43.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                  && tb43.CO_CUR == strP_CO_CUR
                                  && tb43.CO_ANO_GRADE == strP_CO_ANO_REFER
                                  && tb43.CO_MAT == strP_CO_MAT //Antes era incerto pois não verificava a disciplina como agora
                                  select new
                                  {
                                      tb43.QT_AULAS_BIM1,
                                      tb43.QT_AULAS_BIM2,
                                      tb43.QT_AULAS_BIM3,
                                      tb43.QT_AULAS_BIM4
                                  }).ToList().FirstOrDefault();
                #endregion

                #region Retorna os dados dos alunos

                //var strMes = strP_MES.ToString();

                //var lst = (from tb07 in ctx.TB07_ALUNO
                //           join tb08 in ctx.TB08_MATRCUR on tb07.CO_ALU equals tb08.CO_ALU
                //           join tb132 in ctx.TB132_FREQ_ALU on tb07.CO_ALU equals tb132.TB07_ALUNO.CO_ALU
                //           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                //           join tb06 in ctx.TB06_TURMAS on tb08.CO_TUR equals tb06.CO_TUR
                //           join tb129 in ctx.TB129_CADTURMAS on tb06.CO_TUR equals tb129.CO_TUR
                //           join tb02 in ctx.TB02_MATERIA on tb132.CO_MAT equals tb02.CO_MAT
                //           join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                //           where tb08.CO_EMP == strP_CO_EMP
                //           && tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                //           && tb08.CO_CUR == strP_CO_CUR
                //           && tb08.CO_TUR == strP_CO_TUR
                //           && tb08.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                //           && tb08.CO_SIT_MAT != "C"
                //           && tb132.FL_HOMOL_FREQU == "S"
                //           && tb132.TB01_CURSO.CO_EMP == tb08.CO_EMP
                //           && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == tb08.TB44_MODULO.CO_MODU_CUR
                //           && tb132.TB01_CURSO.CO_CUR == tb08.CO_CUR
                //           && tb132.CO_TUR == tb08.CO_TUR
                //           && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                //           && tb132.CO_BIMESTRE == strP_BIMESTRE
                //&& (tb132.DT_FRE.Month == strP_MES)
                //&& tb132.CO_ATIV_PROF_TUR != 0
                var lst = (from tb132 in ctx.TB132_FREQ_ALU
                           join tb07 in ctx.TB07_ALUNO on tb132.TB07_ALUNO.CO_ALU equals tb07.CO_ALU
                           join tb08 in ctx.TB08_MATRCUR on tb07.CO_ALU equals tb08.CO_ALU
                           join tb083 in ctx.TB83_PARAMETRO on tb08.CO_EMP equals tb083.CO_EMP
                           join tb03Coord in ctx.TB03_COLABOR on tb083.CO_COOR1 equals tb03Coord.CO_COL
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
                             && tb43.CO_EMP == tb079.CO_EMP
                             && tb43.CO_MAT == tb079.CO_MAT
                             && tb43.CO_CUR == tb079.CO_CUR
                             && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                             && tb43.ID_MATER_AGRUP == null
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
                           select new DiarioNotasSupremo
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
                               CO_CUR = tb01.CO_CUR,
                               CO_ANO_R = tb08.CO_ANO_MES_MAT,
                               ID_MATERIA = tb107.ID_MATERIA,
                               VL_NOTA_BIM = (strP_BIMESTRE == "B1" ? tb079.VL_MEDIA_BIM1 : strP_BIMESTRE == "B2" ? tb079.VL_MEDIA_BIM2 :
strP_BIMESTRE == "B3" ? tb079.VL_MEDIA_BIM3 : tb079.VL_MEDIA_BIM4),

                               noCoord = tb03Coord.CO_MAT_COL + " - " + tb03Coord.NO_COL,
                               NT_MAX_ATIV = tb43.VL_NOTA_MAXIM_ATIVI,
                               NT_MAX_PROV = tb43.VL_NOTA_MAXIM_PROVA,
                               NT_MAX_SIMU = tb43.VL_NOTA_MAXIM_SIMUL,

                               //Responsável por garantir que futuramente em ordenação, os alunos transferidos fiquem no final
                               ORD_SITU = (ltbt != null || tb08.CO_SIT_MAT == "X" || tb08.CO_SIT_MAT == "T" ? 1 : 0),
                               DT_TRANS = (ltbt != null ? ltbt.DT_EFETI_TRANSF : tb08.DT_SIT_MAT),
                           }).DistinctBy(ha => ha.coAlu).OrderBy(o => o.noAlu).ToList();

                #endregion

                //var res = lst.ToList();
                //Ordena os alunos colocando os transferidos no final da listagem
                var res = lst.OrderBy(w => w.ORD_SITU).ThenBy(w => w.noAlu).ToList();

                int num = 1;
                int c = 1;
                int tf = 0;
                foreach (DiarioNotasSupremo dcf in res)
                {
                    //Inicializa array para guardar as datas que existem lançamentos de frequências
                    List<DateTime> dataExisList = new List<DateTime>();

                    dcf.Num = num;
                    foreach (DiarioFrente.RelDataFre rdf in lstDtFreq)
                    {
                        string codigoSemAtividade = string.Empty;
                        if (rdf.NR_TEMPO != null)
                        {
                            #region Apresenta as informações quando existe o tempo lançado na frequência
                            //===> Retorna as frequências para uma data e aluno
                            var lstFreq = (from tb132 in ctx.TB132_FREQ_ALU
                                           where tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                           && tb132.FL_HOMOL_FREQU == "S"
                                           && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                                           && tb132.CO_TUR == strP_CO_TUR
                                           && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                                           && tb132.TB07_ALUNO.CO_ALU == dcf.coAlu
                                               //&& tb132.CO_BIMESTRE == strP_BIMESTRE
                                           && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                                           && tb132.DT_FRE == rdf.DT_FRE
                                               //&& tb132.CO_ATIV_PROF_TUR != 0


                                               //&& rdf.NR_TEMPO != null ? tb132.NR_TEMPO == rdf.NR_TEMPO : tb132.NR_TEMPO == null
                                           && tb132.NR_TEMPO == rdf.NR_TEMPO
                                           select new DiarioFrente.ListaFrequencia
                                           {
                                               CO_FLAG_FREQ_ALUNO = tb132.CO_FLAG_FREQ_ALUNO,
                                               //DT_LANCTO_FREQ_ALUNO = tb132.DT_LANCTO_FREQ_ALUNO,
                                               DT_LANCTO_FREQ_ALUNO = tb132.DT_FRE,
                                               coAtividade = tb132.CO_ATIV_PROF_TUR,
                                               NumeroTempo = tb132.NR_TEMPO
                                               //                VL_NOTA_BIM = (strP_BIMESTRE == "B1" ? tb079.VL_MEDIA_BIM1 : strP_BIMESTRE == "B2" ? tb079.VL_MEDIA_BIM2 :
                                               //strP_BIMESTRE == "B3" ? tb079.VL_MEDIA_BIM3 : tb079.VL_MEDIA_BIM4),
                                           }).OrderBy(o => o.DT_LANCTO_FREQ_ALUNO).FirstOrDefault();

                            var v = lstFreq;

                            //foreach (var v in lstFreq)
                            //{

                            if (v != null && v.CO_FLAG_FREQ_ALUNO != null)
                            {
                                if (v.CO_FLAG_FREQ_ALUNO == "N")
                                {
                                    tf++;
                                }
                            }

                            //foreach (var v in lstFreq)
                            //{
                            #region Gera as 48 colunas de data e de frequências
                            codigoSemAtividade = rdf.CO_ATIV == null || rdf.CO_ATIV == 0 ? "" : "";

                            switch (c)
                            {
                                case 1:
                                    dcf.tc01 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt01 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe01 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp01 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 2:
                                    dcf.tc02 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt02 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe02 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp02 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 3:
                                    dcf.tc03 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt03 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe03 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp03 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 4:
                                    dcf.tc04 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt04 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe04 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp04 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 5:
                                    dcf.tc05 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt05 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe05 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp05 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 6:
                                    dcf.tc06 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt06 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe06 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp06 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 7:
                                    dcf.tc07 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt07 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe07 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp07 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 8:
                                    dcf.tc08 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt08 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe08 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;

                                    }
                                    dcf.tcTp08 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 9:
                                    dcf.tc09 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt09 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe09 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp09 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 10:
                                    dcf.tc10 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt10 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe10 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp10 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 11:
                                    dcf.tc11 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt11 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe11 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp11 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 12:
                                    dcf.tc12 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt12 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe12 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp12 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 13:
                                    dcf.tc13 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt13 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe13 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp13 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 14:
                                    dcf.tc14 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt14 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe14 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp14 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 15:
                                    dcf.tc15 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt15 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe15 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp15 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 16:
                                    dcf.tc16 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt16 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe16 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp16 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 17:
                                    dcf.tc17 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt17 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe17 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp17 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 18:
                                    dcf.tc18 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt18 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe18 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp18 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 19:
                                    dcf.tc19 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt19 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe19 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp19 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 20:
                                    dcf.tc20 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt20 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe20 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp20 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 21:
                                    dcf.tc21 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt21 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe21 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp21 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 22:
                                    dcf.tc22 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt22 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe22 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp22 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 23:
                                    dcf.tc23 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt23 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe23 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp23 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 24:
                                    dcf.tc24 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt24 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe24 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp24 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 25:
                                    dcf.tc25 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    {
                                        dcf.tcDt25 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe25 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp25 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 26:
                                    dcf.tc26 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    {
                                        dcf.tcDt26 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe26 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp26 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 27:
                                    dcf.tc27 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt27 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe27 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp27 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 28:
                                    dcf.tc28 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt28 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe28 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp28 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 29:
                                    dcf.tc29 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt29 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe29 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp29 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 30:
                                    dcf.tc30 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt30 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe30 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp30 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 31:
                                    dcf.tc31 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt31 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe31 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp31 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 32:
                                    dcf.tc32 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt32 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe32 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp32 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 33:
                                    dcf.tc33 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt33 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe33 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp33 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 34:
                                    dcf.tc34 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt34 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe34 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp34 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 35:
                                    dcf.tc35 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt35 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe35 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp35 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 36:
                                    dcf.tc36 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt36 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe36 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp36 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 37:
                                    dcf.tc37 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt37 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe37 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;

                                    }
                                    dcf.tcTp37 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 38:
                                    dcf.tc38 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt38 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe38 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp38 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 39:
                                    dcf.tc39 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt39 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe39 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp39 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 40:
                                    dcf.tc40 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt40 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe40 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp40 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 41:
                                    dcf.tc41 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt41 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe41 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp41 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 42:
                                    dcf.tc42 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt42 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe42 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp42 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 43:
                                    dcf.tc43 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt43 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe43 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp43 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 44:
                                    dcf.tc44 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt44 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe44 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp44 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 45:
                                    dcf.tc45 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt45 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe45 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp45 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 46:
                                    dcf.tc46 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt46 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe46 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp46 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 47:
                                    dcf.tc47 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt47 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe47 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp47 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                            }
                            #endregion
                            //}
                            //}
                            #endregion
                        }
                        else
                        {
                            #region Apresenta as informaçÕes quando não existe o tempo lançado na frequência
                            //===> Retorna as frequências para uma data e aluno
                            var lstFreq = (from tb132 in ctx.TB132_FREQ_ALU
                                           where tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                           && tb132.FL_HOMOL_FREQU == "S"
                                           && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                                           && tb132.CO_TUR == strP_CO_TUR
                                           && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                                           && tb132.TB07_ALUNO.CO_ALU == dcf.coAlu
                                               //&& tb132.CO_BIMESTRE == strP_BIMESTRE
                                           && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                                           && tb132.DT_FRE == rdf.DT_FRE
                                           //&& rdf.NR_TEMPO != null ? tb132.NR_TEMPO == rdf.NR_TEMPO : tb132.NR_TEMPO == null
                                           //&& tb132.NR_TEMPO == rdf.NR_TEMPO
                                           //&& tb132.CO_ATIV_PROF_TUR != 0
                                           select new DiarioFrente.ListaFrequencia
                                           {
                                               CO_FLAG_FREQ_ALUNO = tb132.CO_FLAG_FREQ_ALUNO,
                                               //DT_LANCTO_FREQ_ALUNO = tb132.DT_LANCTO_FREQ_ALUNO,
                                               DT_LANCTO_FREQ_ALUNO = tb132.DT_FRE,
                                               coAtividade = tb132.CO_ATIV_PROF_TUR,
                                               NumeroTempo = tb132.NR_TEMPO
                                           }).OrderBy(o => o.DT_LANCTO_FREQ_ALUNO).FirstOrDefault();

                            var v = lstFreq;

                            //foreach (var v in lstFreq)
                            //{

                            if (v != null && v.CO_FLAG_FREQ_ALUNO != null)
                            {
                                if (v.CO_FLAG_FREQ_ALUNO == "N")
                                {
                                    tf++;
                                }
                            }

                            //foreach (var v in lstFreq)
                            //{
                            #region Gera as 48 colunas de data e de frequências
                            codigoSemAtividade = rdf.CO_ATIV == null || rdf.CO_ATIV == 0 ? "" : "";
                            switch (c)
                            {
                                case 1:
                                    dcf.tc01 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt01 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe01 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp01 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 2:
                                    dcf.tc02 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt02 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe02 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp02 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 3:
                                    dcf.tc03 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt03 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe03 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp03 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 4:
                                    dcf.tc04 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt04 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe04 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp04 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 5:
                                    dcf.tc05 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt05 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe05 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp05 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 6:
                                    dcf.tc06 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt06 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe06 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp06 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 7:
                                    dcf.tc07 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt07 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe07 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp07 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 8:
                                    dcf.tc08 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt08 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe08 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp08 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 9:
                                    dcf.tc09 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt09 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe09 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp09 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 10:
                                    dcf.tc10 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt10 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe10 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp10 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 11:
                                    dcf.tc11 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt11 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe11 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp11 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 12:
                                    dcf.tc12 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt12 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe12 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp12 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 13:
                                    dcf.tc13 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt13 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe13 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp13 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 14:
                                    dcf.tc14 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt14 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe14 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp14 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 15:
                                    dcf.tc15 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt15 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe15 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp15 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 16:
                                    dcf.tc16 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt16 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe16 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp16 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 17:
                                    dcf.tc17 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt17 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe17 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp17 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 18:
                                    dcf.tc18 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt18 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe18 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp18 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 19:
                                    dcf.tc19 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt19 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe19 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp19 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 20:
                                    dcf.tc20 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt20 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe20 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp20 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 21:
                                    dcf.tc21 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt21 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe21 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp21 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 22:
                                    dcf.tc22 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt22 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe22 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp22 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 23:
                                    dcf.tc23 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt23 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe23 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp23 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 24:
                                    dcf.tc24 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt24 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe24 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp24 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 25:
                                    dcf.tc25 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt25 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe25 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp25 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 26:
                                    dcf.tc26 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt26 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe26 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp26 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 27:
                                    dcf.tc27 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt27 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe27 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp27 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 28:
                                    dcf.tc28 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt28 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe28 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp28 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 29:
                                    dcf.tc29 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt29 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe29 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp29 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 30:
                                    dcf.tc30 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt30 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe30 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp30 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 31:
                                    dcf.tc31 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt31 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe31 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp31 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 32:
                                    dcf.tc32 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt32 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe32 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp32 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 33:
                                    dcf.tc33 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt33 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe33 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp33 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 34:
                                    dcf.tc34 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt34 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe34 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp34 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 35:
                                    dcf.tc35 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt35 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe35 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp35 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 36:
                                    dcf.tc36 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt36 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe36 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp36 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 37:
                                    dcf.tc37 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt37 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe37 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp37 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 38:
                                    dcf.tc38 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt38 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe38 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp38 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 39:
                                    dcf.tc39 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt39 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe39 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp39 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 40:
                                    dcf.tc40 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt40 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe40 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp40 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 41:
                                    dcf.tc41 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt41 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe41 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp41 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 42:
                                    dcf.tc42 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt42 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe42 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp42 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 43:
                                    dcf.tc43 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt43 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe43 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp43 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 44:
                                    dcf.tc44 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt44 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe44 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp44 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 45:
                                    dcf.tc45 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt45 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe45 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp45 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 46:
                                    dcf.tc46 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt46 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe46 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp46 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                                case 47:
                                    dcf.tc47 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ? (PresenAst ? "*" : "P") : "F" : " - ";
                                    if (!VerificaExisteData(dataExisList, rdf.DT_FRE))
                                    {
                                        dcf.tcDt47 = rdf.DT_FRE.ToString("dd") + codigoSemAtividade;
                                        dcf.tcMe47 = rdf.DT_FRE.ToString("MM") + codigoSemAtividade;
                                    }
                                    dcf.tcTp47 = v != null ? v.NR_TEMPO : "";
                                    c++;
                                    break;
                            }
                            #endregion
                            //}
                            //}
                            #endregion
                        }
                    }
                    dcf.TF = tf.ToString();
                    dcf.AulasDadas = lstDtFreq.Count();
                    dcf.qtAulasB1 = lstQtAulas.QT_AULAS_BIM1;
                    dcf.qtAulasB2 = lstQtAulas.QT_AULAS_BIM2;
                    dcf.qtAulasB3 = lstQtAulas.QT_AULAS_BIM3;
                    dcf.qtAulasB4 = lstQtAulas.QT_AULAS_BIM4;
                    dcf.noCol = noCol;
                    num++;
                    c = 1;
                    tf = 0;
                }

                // Manipulação de visualização dos dias

                foreach (var item in res)
                {

                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (DiarioNotasSupremo at in res)
                    bsReport.Add(at);

                #endregion

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

            //return tem;
            return false; /* Inserido para retornar sempre como não existe, pois esta regra foi desativada em 04/02/2014 
             à pedido do Cordova

            Maxwell Almeida - Analista Desenvolvedor

            O objetivo deste método juntamente à toda a regra no geral, era que, ao haver dois lançamentos em Períodos
            diferentes no mesmo dia, fosse apresentada apenas a data apenas no primeiro.
            */
        }
    }
}
