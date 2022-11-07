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
    public partial class RptPautaChamadaSerieTurma : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptPautaChamadaSerieTurma()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              int strP_CO_MAT,
                              int strP_MES,
                              string infos,
                              string NO_RELATORIO)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "FOLHA MENSAL DE CHAMADA DE ALUNOS POR CURSO*");


                if (header == null)
                    return 0;

                //this.VisiblePageHeader = false;
                // Setar o header do relatorio
                this.BaseInit(header);

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Retorna o nome do professor
                int ANOIN = int.Parse(strP_CO_ANO_MES_MAT);
                string noCol = "";
                if ((from resp in ctx.TB_RESPON_MATERIA where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == ANOIN ? resp.CO_MAT == strP_CO_MAT : 0 == 0 select resp).Any())
                {
                    if (strP_CO_MAT != 0)
                    {
                        if ((from resp in ctx.TB_RESPON_MATERIA join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == ANOIN && resp.CO_MAT == strP_CO_MAT select new { tb03.NO_COL }).Any())
                        {
                            noCol = (from resp in ctx.TB_RESPON_MATERIA
                                     join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL
                                     where resp.CO_MODU_CUR == strP_CO_MODU_CUR
                                     && resp.CO_CUR == strP_CO_CUR
                                     && (strP_CO_TUR != 0 ? resp.CO_TUR == strP_CO_TUR : true)
                                     && resp.CO_ANO_REF == ANOIN
                                     && (strP_CO_MAT != 0 ? resp.CO_MAT == strP_CO_MAT : resp.CO_MAT == null)
                                     select new
                                     {
                                         NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
                                     }).FirstOrDefault().NO_COL;
                        }
                    }
                    else
                        noCol = "";
                }
                #endregion

                #endregion

                if ((strP_MES == 4) || (strP_MES == 6) || (strP_MES == 9) || (strP_MES == 11))
                {
                    xrTC31.Text = "";
                }

                if (strP_MES == 2)
                {
                    xrTC31.Text = xrTC30.Text = "";
                    if (!DateTime.IsLeapYear(int.Parse(strP_CO_ANO_MES_MAT.Trim())))
                    {
                        xrTC29.Text = "";
                    }
                }

                #region Query Pauta Chamada

                var lst = (from tb48 in ctx.TB48_GRADE_ALUNO
                           join tb01 in ctx.TB01_CURSO on tb48.CO_CUR equals tb01.CO_CUR
                           join tb08 in ctx.TB08_MATRCUR on tb48.CO_ALU equals tb08.CO_ALU
                           join tb129 in ctx.TB129_CADTURMAS on tb48.CO_TUR equals tb129.CO_TUR
                           join tb083 in ctx.TB83_PARAMETRO on tb08.CO_EMP equals tb083.CO_EMP
                           join tb03Coord in ctx.TB03_COLABOR on tb083.CO_COOR1 equals tb03Coord.CO_COL into l1
                           from lcoo in l1.DefaultIfEmpty()
                           where tb48.CO_EMP == strP_CO_EMP_REF
                           && tb48.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb48.CO_CUR == strP_CO_CUR
                           && tb48.CO_ANO_MES_MAT.Equals((strP_CO_ANO_MES_MAT).Trim())
                           && tb48.CO_TUR == strP_CO_TUR
                           && (strP_CO_MAT != 0 ? tb48.CO_MAT == strP_CO_MAT : strP_CO_MAT == 0)
                           && tb48.CO_ANO_MES_MAT.Equals(tb08.CO_ANO_MES_MAT) && tb48.CO_CUR == tb08.CO_CUR && tb48.CO_TUR == tb08.CO_TUR
                           && !tb08.CO_SIT_MAT.Equals("C")
                           select new RelListaPautaChamada
                           {
                               NomeAluno = tb48.TB07_ALUNO.NO_ALU,
                               NIRE = tb48.TB07_ALUNO.NU_NIRE,
                               AnoReferencia = tb48.CO_ANO_MES_MAT,
                               CoModuCur = tb48.CO_MODU_CUR,
                               CoCur = tb48.CO_CUR,
                               CoTur = tb48.CO_TUR,
                               Situacao = (tb08.CO_SIT_MAT == "A" ? "MA" : tb08.CO_SIT_MAT == "C" ? "CA" : tb08.CO_SIT_MAT == "X" ? "TE" :
                               tb08.CO_SIT_MAT == "T" ? "MT" : tb08.CO_SIT_MAT == "D" ? "DE" : tb08.CO_SIT_MAT == "I" ? "IN" :
                               tb08.CO_SIT_MAT == "J" ? "JU" : tb08.CO_SIT_MAT == "Y" ? "TC" : "SM"),

                               nomeProf = noCol,
                               noCoord = (lcoo != null ? lcoo.NO_COL : ""),
                           }).Distinct().OrderBy(p => p.NomeAluno);

                // Erro: não encontrou registros
                if (lst.ToList().Count == 0)
                    return -1;

                var res = lst.ToList();

                foreach (var iLst in res)
                {
                    int anoRefer = int.Parse(iLst.AnoReferencia);

                    var tbResponMat = (from tbRespMat in ctx.TB_RESPON_MATERIA
                                       join tb03 in ctx.TB03_COLABOR on tbRespMat.CO_COL_RESP equals tb03.CO_COL
                                       where (tbRespMat.CO_ANO_REF == anoRefer) && (tbRespMat.CO_MODU_CUR == iLst.CoModuCur) && (tbRespMat.CO_CUR == iLst.CoCur)
                                       && (tbRespMat.CO_TUR == iLst.CoTur)
                                       && (strP_CO_MAT != 0 ? tbRespMat.CO_MAT == strP_CO_MAT : strP_CO_MAT == 0)
                                       select new
                                       {
                                           CO_MAT_COL = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                           tb03.NO_COL
                                       }).FirstOrDefault();

                    if (tbResponMat != null)
                    {
                        iLst.Responsavel = "( Professor Responsável: " + tbResponMat.CO_MAT_COL + " " + tbResponMat.NO_COL + " )";
                    }
                    else
                    {
                        iLst.Responsavel = "( Professor Responsável: ***** )";
                    }
                }

                if (res.Count < 40)
                {
                    for (int i = res.Count; i < 40; i++)
                    {
                        res.Add(new RelListaPautaChamada
                        {
                            NomeAluno = null,
                            NIRE = null,
                            AnoReferencia = null,
                            CoModuCur = null,
                            CoCur = null,
                            CoTur = null,
                            Materia = null
                        });
                    }
                }                                                

                #endregion

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelListaPautaChamada at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Lista Pauta Chamada

        public class RelListaPautaChamada
        {
            //Dados padrão
            public string nomeProf { get; set; }
            public string noCoord { get; set; }

            public string NomeAluno { get; set; }
            public string Situacao { get; set; }
            public int? NIRE { get; set; }
            public string AnoReferencia { get; set; }
            public int? CoModuCur { get; set; }
            public int? CoCur { get; set; }
            public int? CoTur { get; set; }
            public string MesRefer { get; set; }
            public string Responsavel { get; set; }
            public string Materia { get; set; }

            public string DescAlu
            {
                get
                {
                    return this.NIRE != null ? this.NIRE.ToString().PadLeft(9, '0') + " - " + this.NomeAluno.ToUpper() : "";
                }
            }
        }
        #endregion
    }
}
