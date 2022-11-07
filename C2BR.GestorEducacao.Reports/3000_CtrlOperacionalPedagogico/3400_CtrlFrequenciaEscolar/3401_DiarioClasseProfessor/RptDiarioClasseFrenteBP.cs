﻿using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3401_DiarioClasseProfessor
{
    public partial class RptDiarioClasseFrenteBP : DevExpress.XtraReports.UI.XtraReport
    {
        public RptDiarioClasseFrenteBP()
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
            //int strP_MES,
                              string strP_PROF_RESP,
                              DateTime dataInicial,
                              DateTime dataFinal,
                              string strProfessorCod,
                              string strProfessor,
                              string strMateria,
                              DateTime dtIniBim,
                              DateTime dtFimBim,
                              int CO_COL,
                              bool PresenAst,
                              bool AssinFreq
                             )
        {
            try
            {
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;


                //trata mostra/não mostra assinatura
                xrLabel7.Visible = xrLabel8.Visible = xrLabel9.Visible = xrLabel12.Visible = xrLabel10.Visible =
                    xrLabel1.Visible = xrLabel2.Visible = xrLabel3.Visible = xrLabel4.Visible = xrLabel11.Visible =
                    AssinFreq;

                #region Query Colaborador Parametrizada
                decimal dtRef = decimal.Parse(strP_CO_ANO_REFER);
                int intAno = int.Parse(strP_CO_ANO_REFER);

                #region Retorna as datas de lançamento de frequência

                string turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR).CO_FLAG_RESP_TURMA;

                int ano = strP_CO_ANO_REFER != "" ? int.Parse(strP_CO_ANO_REFER) : 0;
                var lstDtFreq = new List<RelDataFre>();

                if (turmaUnica == "S")
                {
                    //lstDtFreq = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                    //             join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb132.TB01_CURSO.CO_CUR equals tbresp.CO_CUR
                    //             where tb132.FL_HOMOL_FREQU == "S"
                    //                 //&& tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                    //             && tb132.TB01_CURSO.CO_MODU_CUR == strP_CO_MODU_CUR
                    //             && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                    //             && tb132.CO_TUR == strP_CO_TUR
                    //             && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                    //                 //&& tb132.CO_BIMESTRE == strP_BIMESTRE
                    //                 //&& tb132.DT_FRE.Month == strP_MES
                    //             && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                    //                 //&& ((tb132.DT_FRE.Month >= dtIniBim.Month) && (tb132.DT_FRE.Month <= dtFimBim.Month))
                    //             && ((tb132.DT_FRE >= dtIniBim) && (tb132.DT_FRE <= dtFimBim))
                    //                 //Se for turma única trás de todas as disciplinas, se não, filtra a disciplina
                    //                 //&& (turmaUnica == "S" ? 0 == 0 : tbresp.CO_MAT == tb132.CO_MAT)

                    //             //&& tb132.CO_ATIV_PROF_TUR != 0
                    //             && tbresp.CO_MODU_CUR == strP_CO_MODU_CUR
                    //             && tbresp.CO_CUR == tb132.TB01_CURSO.CO_CUR
                    //             && tbresp.CO_ANO_REF == ano
                    //             && tbresp.CO_COL_RESP == CO_COL

                    lstDtFreq = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                 join tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros() on tb132.CO_ATIV_PROF_TUR equals tb119.CO_ATIV_PROF_TUR
                                 join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb119.CO_COL_ATIV equals tbresp.CO_COL_RESP 
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
                                     //Se for turma única trás de todas as disciplinas, se não, filtra a disciplina
                                     //&& (turmaUnica == "S" ? 0 == 0 : tbresp.CO_MAT == tb132.CO_MAT)

                                 //&& tb132.CO_ATIV_PROF_TUR != 0
                                 && tbresp.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tbresp.CO_CUR == tb132.TB01_CURSO.CO_CUR
                                 && tbresp.CO_ANO_REF == ano
                                 && tbresp.CO_COL_RESP == CO_COL
                                 && tbresp.CO_COL_RESP == CO_COL
                                 && tb119.CO_COL_ATIV == CO_COL
                                 && tb119.CO_COL_ATIV == tbresp.CO_COL_RESP

                                 select new RelDataFre
                                 {
                                     DT_FRE = tb132.DT_FRE,
                                     NR_TEMPO = tb132.NR_TEMPO,
                                     CO_ATIV = tb132.CO_ATIV_PROF_TUR
                                 }).Distinct().OrderBy(w => w.DT_FRE).ToList();
                }
                else
                {
                    lstDtFreq = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                 join tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros() on tb132.CO_ATIV_PROF_TUR equals tb119.CO_ATIV_PROF_TUR
                                 join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb119.CO_COL_ATIV equals tbresp.CO_COL_RESP 
                                 where tb132.FL_HOMOL_FREQU == "S"
                                 && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tbresp.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tbresp.CO_CUR == tb132.TB01_CURSO.CO_CUR
                                 && tbresp.CO_ANO_REF == ano
                                 && tbresp.CO_COL_RESP == CO_COL
                                 && tb119.CO_COL_ATIV == CO_COL
                                 && tb119.CO_COL_ATIV == tbresp.CO_COL_RESP
                                     //&& tb132.TB01_CURSO.CO_MODU_CUR == strP_CO_MODU_CUR
                                 && tb132.TB01_CURSO.CO_CUR == strP_CO_CUR
                                 && tb132.CO_TUR == strP_CO_TUR
                                 && tb132.CO_ANO_REFER_FREQ_ALUNO == dtRef
                                 && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                                 && ((tb132.DT_FRE >= dtIniBim) && (tb132.DT_FRE <= dtFimBim))
                                     //Se for turma única trás de todas as disciplinas, se não, filtra a disciplina
                                 && (tbresp.CO_MAT == tb132.CO_MAT)

                                 select new RelDataFre
                                 {
                                     DT_FRE = tb132.DT_FRE,
                                     NR_TEMPO = tb132.NR_TEMPO,
                                     CO_ATIV = tb132.CO_ATIV_PROF_TUR
                                 }).Distinct().OrderBy(w => w.DT_FRE).ToList();
                }

                if (lstDtFreq == null || lstDtFreq.Count() <= 0)
                    return null;

                // Pega a primeira data de lançamento
                DateTime dtIni = lstDtFreq.OrderBy(o => o.DT_FRE).First().DT_FRE;

                // Pega a ultima data de lançamento
                DateTime dtFim = lstDtFreq.OrderByDescending(o => o.DT_FRE).First().DT_FRE;
                #endregion

                string coFlagResp = (from coFlag in ctx.TB06_TURMAS where coFlag.CO_EMP == strP_CO_EMP && coFlag.CO_MODU_CUR == strP_CO_MODU_CUR && coFlag.CO_CUR == strP_CO_CUR && coFlag.CO_TUR == strP_CO_TUR select new { coFlag.CO_FLAG_RESP_TURMA }).FirstOrDefault().CO_FLAG_RESP_TURMA;

                //Coleta o nome do professor
                string noCol = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == CO_COL).FirstOrDefault().NO_COL;

                #region Retorna as quantidades de aula cadastradas na grade do curso/série
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
                //           join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb132.CO_MAT equals tbresp.CO_MAT
                //           where tb08.CO_EMP == strP_CO_EMP
                //&& tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                //&& tb08.CO_CUR == strP_CO_CUR
                //&& tb08.CO_TUR == strP_CO_TUR
                //&& tb08.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                //&& tb08.CO_SIT_MAT != "C"
                //&& tb132.FL_HOMOL_FREQU == "S"
                //&& tb132.TB01_CURSO.CO_EMP == tb08.CO_EMP
                //&& tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == tb08.TB44_MODULO.CO_MODU_CUR
                //&& tb132.TB01_CURSO.CO_CUR == tb08.CO_CUR
                //&& tb132.CO_TUR == tb08.CO_TUR
                //&& (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                //&& tb132.CO_BIMESTRE == strP_BIMESTRE
                //&& tbresp.CO_MODU_CUR == strP_CO_MODU_CUR
                //&& tbresp.CO_CUR == tb132.TB01_CURSO.CO_CUR
                //&& tbresp.CO_ANO_REF == ano
                //&& tbresp.CO_COL_RESP == CO_COL

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
                           join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb132.TB01_CURSO.CO_CUR equals tbresp.CO_CUR
                           join tbtrans in ctx.TB_TRANSF_INTERNA on tb132.TB07_ALUNO.CO_ALU equals tbtrans.TB07_ALUNO.CO_ALU into l1
                           from ltbt in l1.DefaultIfEmpty()
                           where tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb08.CO_EMP == strP_CO_EMP
                           && tb08.CO_CUR == strP_CO_CUR
                           && tb08.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                           && tb08.CO_SIT_MAT != "C"
                           && tb132.FL_HOMOL_FREQU == "S"
                           && tb132.TB01_CURSO.CO_EMP == tb08.CO_EMP
                           && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == tb08.TB44_MODULO.CO_MODU_CUR
                           && tb132.TB01_CURSO.CO_CUR == tb08.CO_CUR
                           && tb132.CO_TUR == strP_CO_TUR
                           && (strP_CO_MAT != 0 ? tb132.CO_MAT == strP_CO_MAT : 0 == 0)
                               //Se for turma única trás de todas as disciplinas, se não, filtra a disciplina
                           && (turmaUnica == "S" ? 0 == 0 : tbresp.CO_MAT == tb132.CO_MAT)
                           //&& tb132.CO_BIMESTRE == strP_BIMESTRE
                           && ((tb132.DT_FRE >= dtIniBim) && (tb132.DT_FRE <= dtFimBim))
                           && tbresp.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tbresp.CO_CUR == tb132.TB01_CURSO.CO_CUR
                           && tbresp.CO_ANO_REF == ano
                           && tbresp.CO_COL_RESP == CO_COL
                           && (ltbt != null ? ltbt.CO_TURMA_ATUAL == tb132.CO_TUR : 0 == 0)
                           //&& (tb132.DT_FRE.Month == strP_MES)
                           //&& tb132.CO_ATIV_PROF_TUR != 0

                           select new DiarioClasseFrente
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

                               noCoord = tb03Coord.CO_MAT_COL + " - " + tb03Coord.NO_COL,

                               coTur = tb132.CO_TUR,
                               //Responsável por garantir que futuramente em ordenação, os alunos transferidos fiquem no final
                               ORD_SITU = (ltbt != null || tb08.CO_SIT_MAT == "X" || tb08.CO_SIT_MAT == "T" ? 1 : 0),
                               DT_TRANS = (ltbt != null ? ltbt.DT_EFETI_TRANSF : tb08.DT_SIT_MAT),
                           }).DistinctBy(ha => ha.coAlu).OrderBy(o => o.noAlu);

                #endregion

                var res = lst.ToList();

                int num = 1;
                int c = 1;
                int tf = 0;
                foreach (DiarioClasseFrente dcf in res)
                {
                    //Inicializa array para guardar as datas que existem lançamentos de frequências
                    List<DateTime> dataExisList = new List<DateTime>();

                    dcf.Num = num;
                    foreach (RelDataFre rdf in lstDtFreq)
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
                                           select new ListaFrequencia
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
                                    dcf.tc08 = v != null && v.CO_FLAG_FREQ_ALUNO != null ? v.CO_FLAG_FREQ_ALUNO == "S" ?(PresenAst ? "*" : "P") : "F" : " - ";
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
                                           select new ListaFrequencia
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
                foreach (DiarioClasseFrente at in res)
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
            return false;/* Inserido para retornar sempre como não existe, pois esta regra foi desativada em 04/02/2014 
             à pedido do Cordova

            Maxwell Almeida - Analista Desenvolvedor

            O objetivo deste método juntamente à toda a regra no geral, era que, ao haver dois lançamentos em Períodos
            diferentes no mesmo dia, fosse apresentada apenas a data apenas no primeiro.
            */
        }

        #region Classe Lista Pauta Chamada Verso

        /*
         * Esta classe foi criada para receber as datas da consulta que retorna as datas que possuem lançamento de frequência
         * */
        public class RelDataFre
        {
            public DateTime DT_FRE { get; set; }
            public int? NR_TEMPO { get; set; }
            public int? CO_ATIV { get; set; }
        }

        public class DiarioClasseFrente
        {
            public string noCoord { get; set; }
            public string noCol { get; set; }
            public string turmaUnica { get; set; }
            public string nomeProf
            {
                get
                {
                    return this.noCol.ToUpper();
                }
            }
            public int coAtiv { get; set; }
            public string noModuCur { get; set; }
            public string noCur { get; set; }
            public string ModSerie
            {
                get
                {
                    return this.noModuCur + " / " + this.noCur;
                }
            }
            public int? CO_ATIV { get; set; }
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
            public string noTurma { get; set; }
            public string Disciplina { get; set; }
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

            public string ParametrosFrente1
            {
                get
                {
                    string bimestreD = this.Bimestre + " (" + this.dtInicialBimestre.ToShortDateString() + " a " + this.dtFinalBimestre.ToShortDateString() +
                        ") - Aulas (Previstas/Realizadas): " + ((this.AulasPrev != null) ? this.AulasPrev.Value.ToString() : "**") + "/" + this.AulasDadas + ") ";
                    return bimestreD;
                }
            }

            public string ParametrosFrente2
            {
                get
                {
                    string bimestreD = "";
                    if (this.turmaUnica != "S")
                    {
                        bimestreD = "Matéria: " + this.materia.ToUpper() + " - Professor(a): " + this.nomeProf.ToUpper();
                    }
                    else
                    {
                        bimestreD = "Professor(a): " + this.nomeProf.ToUpper();
                    }
                    return bimestreD;
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

            //Parâmetros para fazer query e saber se o aulo foi transferido de turma
            public int coTur { get; set; }

            public DateTime DT_TRANS { get; set; }
            public int Num { get; set; }
            public string noAlu { get; set; }
            public int coAlu { get; set; }
            public int nuNire { get; set; }
            public string Nire
            {
                get
                {
                    int l = this.nuNire.ToString().Length;
                    string n = this.nuNire.ToString();
                    while (l < 7)
                    {
                        n = "0" + n;
                        l = n.Length;
                    }
                    return n;
                }
            }
            public string NomeAluno
            {
                get
                {
                    //Se o aluno tiver registro de transferência, concatena o seu nire e nome, com a data na qual foi realizada
                    string noAlu = "";
                    if (this.ORD_SITU == 0) //Se não estiver transferido, usa o nome até 30 caracteres
                        noAlu = (this.noAlu.Length > 30 ? this.noAlu.Substring(0, 30).ToUpper() + "..." : this.noAlu);
                    else //Se estiver transferido, usa até 21 caracteres, dando assim, espaço para a data de transferência
                        noAlu = (this.noAlu.Length > 21 ? this.noAlu.Substring(0, 21).ToUpper() + "..." : this.noAlu);

                    return this.Nire + " - " + noAlu + (this.ORD_SITU == 1 ? " - " + this.DT_TRANS.ToString("dd/MM/yy") : "");
                }
            }
            public int ORD_SITU { get; set; }
            public string coSituAlu { get; set; }
            public string ST
            {
                get
                {
                    //Verifica se existe algum registro de transferência interna entre turmas
                    bool res = (from tbtrans in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                where tbtrans.TB07_ALUNO.CO_ALU == this.coAlu
                                && tbtrans.CO_TURMA_ATUAL == this.coTur
                                select tbtrans).Any();
                    if (res == true)
                    {
                        return "TRI";
                    }
                    else
                    {
                        string r = "";
                        switch (this.coSituAlu)
                        {
                            case "A":
                                r = "MAT";
                                break;
                            case "T":
                                r = "TRA";
                                break;
                            case "X":
                                r = "TRE";
                                break;
                            case "F":
                                r = "FIN";
                                break;
                            case "C":
                                r = "CAN";
                                break;
                        }
                        return r;
                    }
                }
            }
            //public int mesBimestre { get; set; }

            //public string mesBimestreE
            //{

            //    get
            //    {
            //        string nome = "";

            //        switch (this.mesBimestre.ToString())
            //        {
            //            case "1":
            //                nome = "Janeiro";
            //                break;
            //            case "2":
            //                nome = "Fevereiro";
            //                break;
            //            case "3":
            //                nome = "março";
            //                break;
            //            case "4":
            //                nome = "Abril";
            //                break;
            //            case "5":
            //                nome = "Maio";
            //                break;
            //            case "6":
            //                nome = "Junho";
            //                break;
            //            case "7":
            //                nome = "Julho";
            //                break;
            //            case "8":
            //                nome = "Agosto";
            //                break;
            //            case "9":
            //                nome = "Setembro";
            //                break;
            //            case "10":
            //                nome = "Outubro";
            //                break;
            //            case "11":
            //                nome = "Novembro";
            //                break;
            //            case "12":
            //                nome = "Dezembro";

            //                break;
            //        }
            //        return nome;
            //    }
            //}

            public DateTime dtInicialBimestre { get; set; }
            public DateTime dtFinalBimestre { get; set; }
            public string professorCod { get; set; }
            public string professor { get; set; }
            public string materia { get; set; }

            #region Variáveis das 47 colunas
            public string tc01 { get; set; }
            public string tc02 { get; set; }
            public string tc03 { get; set; }
            public string tc04 { get; set; }
            public string tc05 { get; set; }
            public string tc06 { get; set; }
            public string tc07 { get; set; }
            public string tc08 { get; set; }
            public string tc09 { get; set; }
            public string tc10 { get; set; }
            public string tc11 { get; set; }
            public string tc12 { get; set; }
            public string tc13 { get; set; }
            public string tc14 { get; set; }
            public string tc15 { get; set; }
            public string tc16 { get; set; }
            public string tc17 { get; set; }
            public string tc18 { get; set; }
            public string tc19 { get; set; }
            public string tc20 { get; set; }
            public string tc21 { get; set; }
            public string tc22 { get; set; }
            public string tc23 { get; set; }
            public string tc24 { get; set; }
            public string tc25 { get; set; }
            public string tc26 { get; set; }
            public string tc27 { get; set; }
            public string tc28 { get; set; }
            public string tc29 { get; set; }
            public string tc30 { get; set; }
            public string tc31 { get; set; }
            public string tc32 { get; set; }
            public string tc33 { get; set; }
            public string tc34 { get; set; }
            public string tc35 { get; set; }
            public string tc36 { get; set; }
            public string tc37 { get; set; }
            public string tc38 { get; set; }
            public string tc39 { get; set; }
            public string tc40 { get; set; }
            public string tc41 { get; set; }
            public string tc42 { get; set; }
            public string tc43 { get; set; }
            public string tc44 { get; set; }
            public string tc45 { get; set; }
            public string tc46 { get; set; }
            public string tc47 { get; set; }

            #endregion

            #region Variáveis das 47 colunas de datas
            public string tcDt01 { get; set; }
            public string tcDt02 { get; set; }
            public string tcDt03 { get; set; }
            public string tcDt04 { get; set; }
            public string tcDt05 { get; set; }
            public string tcDt06 { get; set; }
            public string tcDt07 { get; set; }
            public string tcDt08 { get; set; }
            public string tcDt09 { get; set; }
            public string tcDt10 { get; set; }
            public string tcDt11 { get; set; }
            public string tcDt12 { get; set; }
            public string tcDt13 { get; set; }
            public string tcDt14 { get; set; }
            public string tcDt15 { get; set; }
            public string tcDt16 { get; set; }
            public string tcDt17 { get; set; }
            public string tcDt18 { get; set; }
            public string tcDt19 { get; set; }
            public string tcDt20 { get; set; }
            public string tcDt21 { get; set; }
            public string tcDt22 { get; set; }
            public string tcDt23 { get; set; }
            public string tcDt24 { get; set; }
            public string tcDt25 { get; set; }
            public string tcDt26 { get; set; }
            public string tcDt27 { get; set; }
            public string tcDt28 { get; set; }
            public string tcDt29 { get; set; }
            public string tcDt30 { get; set; }
            public string tcDt31 { get; set; }
            public string tcDt32 { get; set; }
            public string tcDt33 { get; set; }
            public string tcDt34 { get; set; }
            public string tcDt35 { get; set; }
            public string tcDt36 { get; set; }
            public string tcDt37 { get; set; }
            public string tcDt38 { get; set; }
            public string tcDt39 { get; set; }
            public string tcDt40 { get; set; }
            public string tcDt41 { get; set; }
            public string tcDt42 { get; set; }
            public string tcDt43 { get; set; }
            public string tcDt44 { get; set; }
            public string tcDt45 { get; set; }
            public string tcDt46 { get; set; }
            public string tcDt47 { get; set; }

            public string tcMe01 { get; set; }
            public string tcMe02 { get; set; }
            public string tcMe03 { get; set; }
            public string tcMe04 { get; set; }
            public string tcMe05 { get; set; }
            public string tcMe06 { get; set; }
            public string tcMe07 { get; set; }
            public string tcMe08 { get; set; }
            public string tcMe09 { get; set; }
            public string tcMe10 { get; set; }
            public string tcMe11 { get; set; }
            public string tcMe12 { get; set; }
            public string tcMe13 { get; set; }
            public string tcMe14 { get; set; }
            public string tcMe15 { get; set; }
            public string tcMe16 { get; set; }
            public string tcMe17 { get; set; }
            public string tcMe18 { get; set; }
            public string tcMe19 { get; set; }
            public string tcMe20 { get; set; }
            public string tcMe21 { get; set; }
            public string tcMe22 { get; set; }
            public string tcMe23 { get; set; }
            public string tcMe24 { get; set; }
            public string tcMe25 { get; set; }
            public string tcMe26 { get; set; }
            public string tcMe27 { get; set; }
            public string tcMe28 { get; set; }
            public string tcMe29 { get; set; }
            public string tcMe30 { get; set; }
            public string tcMe31 { get; set; }
            public string tcMe32 { get; set; }
            public string tcMe33 { get; set; }
            public string tcMe34 { get; set; }
            public string tcMe35 { get; set; }
            public string tcMe36 { get; set; }
            public string tcMe37 { get; set; }
            public string tcMe38 { get; set; }
            public string tcMe39 { get; set; }
            public string tcMe40 { get; set; }
            public string tcMe41 { get; set; }
            public string tcMe42 { get; set; }
            public string tcMe43 { get; set; }
            public string tcMe44 { get; set; }
            public string tcMe45 { get; set; }
            public string tcMe46 { get; set; }
            public string tcMe47 { get; set; }

            #endregion

            #region Variáveis das 47 colunas de tempo
            public string tcTp01 { get; set; }
            public string tcTp02 { get; set; }
            public string tcTp03 { get; set; }
            public string tcTp04 { get; set; }
            public string tcTp05 { get; set; }
            public string tcTp06 { get; set; }
            public string tcTp07 { get; set; }
            public string tcTp08 { get; set; }
            public string tcTp09 { get; set; }
            public string tcTp10 { get; set; }
            public string tcTp11 { get; set; }
            public string tcTp12 { get; set; }
            public string tcTp13 { get; set; }
            public string tcTp14 { get; set; }
            public string tcTp15 { get; set; }
            public string tcTp16 { get; set; }
            public string tcTp17 { get; set; }
            public string tcTp18 { get; set; }
            public string tcTp19 { get; set; }
            public string tcTp20 { get; set; }
            public string tcTp21 { get; set; }
            public string tcTp22 { get; set; }
            public string tcTp23 { get; set; }
            public string tcTp24 { get; set; }
            public string tcTp25 { get; set; }
            public string tcTp26 { get; set; }
            public string tcTp27 { get; set; }
            public string tcTp28 { get; set; }
            public string tcTp29 { get; set; }
            public string tcTp30 { get; set; }
            public string tcTp31 { get; set; }
            public string tcTp32 { get; set; }
            public string tcTp33 { get; set; }
            public string tcTp34 { get; set; }
            public string tcTp35 { get; set; }
            public string tcTp36 { get; set; }
            public string tcTp37 { get; set; }
            public string tcTp38 { get; set; }
            public string tcTp39 { get; set; }
            public string tcTp40 { get; set; }
            public string tcTp41 { get; set; }
            public string tcTp42 { get; set; }
            public string tcTp43 { get; set; }
            public string tcTp44 { get; set; }
            public string tcTp45 { get; set; }
            public string tcTp46 { get; set; }
            public string tcTp47 { get; set; }

            #endregion

            public string TF { get; set; }
        }

        public class ListaFrequencia
        {
            public string CO_FLAG_FREQ_ALUNO { get; set; }
            public DateTime DT_LANCTO_FREQ_ALUNO { get; set; }
            public int coAtividade { get; set; }
            public int? NumeroTempo { get; set; }
            public string NR_TEMPO
            {
                get
                {
                    return (this.NumeroTempo == null ? "" : this.NumeroTempo.ToString() + "º");
                }
            }

        }
        #endregion
    }
}