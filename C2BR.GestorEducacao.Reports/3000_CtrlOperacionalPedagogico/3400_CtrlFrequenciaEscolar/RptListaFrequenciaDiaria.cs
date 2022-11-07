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
    public partial class RptListaFrequenciaDiaria : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptListaFrequenciaDiaria()
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
                              string dataFreq,
                              string infos,
                              string NO_RELATORIO,
                              int strP_CO_MAT)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                if (header == null)
                    return 0;

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "LISTA DE FREQUÊNCIA NOMINAL*");

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion
                
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

              #region Retorna o nome do professor
                                string coFlagResp = (from coFlag in ctx.TB06_TURMAS where coFlag.CO_EMP == strP_CO_EMP && coFlag.CO_MODU_CUR == strP_CO_MODU_CUR && coFlag.CO_CUR == strP_CO_CUR && coFlag.CO_TUR == strP_CO_TUR select new { coFlag.CO_FLAG_RESP_TURMA }).FirstOrDefault().CO_FLAG_RESP_TURMA;
                int anoI = int.Parse(strP_CO_ANO_MES_MAT);
                string noCol = "";
                if ((from resp in ctx.TB_RESPON_MATERIA where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == anoI ? resp.CO_MAT == strP_CO_MAT : 0 == 0 select resp).Any())
                {
                    if (coFlagResp == "S")
                    {
                        if ((from resp in ctx.TB_RESPON_MATERIA join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == anoI select new { tb03.NO_COL }).Any())
                        {
                            noCol = (from resp in ctx.TB_RESPON_MATERIA
                                     join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL
                                     where resp.CO_MODU_CUR == strP_CO_MODU_CUR
                                     && resp.CO_CUR == strP_CO_CUR
                                     && resp.CO_TUR == strP_CO_TUR
                                     && resp.CO_ANO_REF == anoI
                                     select new
                                     {
                                         NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
                                     }).FirstOrDefault().NO_COL;
                        }
                    }
                    else
                    {
                        if ((from resp in ctx.TB_RESPON_MATERIA join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == strP_CO_MODU_CUR && resp.CO_CUR == strP_CO_CUR && resp.CO_TUR == strP_CO_TUR && resp.CO_ANO_REF == anoI && resp.CO_MAT == strP_CO_MAT select new { tb03.NO_COL }).Any())
                        {
                            noCol = (from resp in ctx.TB_RESPON_MATERIA
                                     join tb03 in ctx.TB03_COLABOR on resp.CO_COL_RESP equals tb03.CO_COL
                                     where resp.CO_MODU_CUR == strP_CO_MODU_CUR
                                     && resp.CO_CUR == strP_CO_CUR
                                     && resp.CO_TUR == strP_CO_TUR
                                     && resp.CO_ANO_REF == anoI
                                     && (strP_CO_MAT != 0 ? resp.CO_MAT == strP_CO_MAT : resp.CO_MAT == null)
                                     select new
                                     {
                                         NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
                                     }).FirstOrDefault().NO_COL;
                        }
                    }
                }
                #endregion

                #region Query Pauta Chamada

                var lst = (from tb08 in ctx.TB08_MATRCUR
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb08.CO_TUR equals tb129.CO_TUR
                           join tb083 in ctx.TB83_PARAMETRO on tb08.CO_EMP equals tb083.CO_EMP
                           join tb03Coord in ctx.TB03_COLABOR on tb083.CO_COOR1 equals tb03Coord.CO_COL into L1
                           from lcoor in L1.DefaultIfEmpty()
                           where tb08.CO_EMP == strP_CO_EMP_REF
                           && tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb08.CO_CUR == strP_CO_CUR
                           && tb08.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT
                           && tb08.CO_TUR == strP_CO_TUR
                           && tb08.CO_ANO_MES_MAT == tb08.CO_ANO_MES_MAT && tb08.CO_CUR == tb08.CO_CUR && tb08.CO_TUR == tb08.CO_TUR
                           select new RelListaPautaChamada
                           {
                               NomeAluno = tb08.TB07_ALUNO.NO_ALU,
                               NIRE = tb08.TB07_ALUNO.NU_NIRE,
                               AnoReferencia = tb08.CO_ANO_MES_MAT,
                               CoModuCur = tb08.TB44_MODULO.CO_MODU_CUR,
                               CoCur = tb08.CO_CUR,
                               CoTur = tb08.CO_TUR,
                               Situacao = (tb08.CO_SIT_MAT == "A" ? "Matriculado" : tb08.CO_SIT_MAT == "C" ? "Cancelado" : tb08.CO_SIT_MAT == "X" ? "Transferido" :
                               tb08.CO_SIT_MAT == "T" ? "MT" : tb08.CO_SIT_MAT == "D" ? "DE" : tb08.CO_SIT_MAT == "I" ? "IN" :
                               tb08.CO_SIT_MAT == "J" ? "JU" : tb08.CO_SIT_MAT == "Y" ? "TC" : "SM"),

                               noCoord = (lcoor != null ? lcoor.NO_COL : ""),
                               nomeProf = noCol,
                           }).Distinct().OrderBy(p => p.NomeAluno);

                // Erro: não encontrou registros
                if (lst.ToList().Count == 0)
                    return -1;

                var res = lst.ToList();

                #endregion

                int countII = 0;
                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelListaPautaChamada at in res)
                {
                    countII++;
                    at.contador = countII;
                    bsReport.Add(at);
                }

                //Completa a lista até o 40 com registros vazios, para o(s) alunos(s) excedentes
                while (countII <= 44)
                {
                    countII++;
                    RelListaPautaChamada rl = new RelListaPautaChamada();
                    rl.AnoReferencia = "";
                    rl.NomeAluno = "";
                    rl.contador = countII;
                    bsReport.Add(rl);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Lista Pauta Chamada

        public class RelListaPautaChamada
        {
            public string nomeProf { get; set; }
            public string noCoord { get; set; }
            public int contador { get; set; }
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
                    return this.NIRE != null ? this.NIRE.ToString().PadLeft(7, '0') + " - " + this.NomeAluno : "";
                }
            }
        }
        #endregion
    }
}
