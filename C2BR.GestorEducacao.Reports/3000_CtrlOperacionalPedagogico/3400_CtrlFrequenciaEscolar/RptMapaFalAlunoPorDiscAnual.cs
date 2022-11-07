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
    public partial class RptMapaFalAlunoPorDiscAnual : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptMapaFalAlunoPorDiscAnual()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,                              
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string strP_CO_ANO_REF,
                              int strP_CO_ALU,
                              int strP_CO_MAT)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                this.VisiblePageHeader = false;
                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Pauta Chamada

                var lst = (from f in ctx.TB48_GRADE_ALUNO
                           join c in ctx.TB01_CURSO
                                on f.CO_CUR equals c.CO_CUR
                            join a in ctx.TB07_ALUNO
                                on f.CO_ALU equals a.CO_ALU
                            where (coEmp != 0 ? f.CO_EMP == coEmp : 0 == 0)
                            && (strP_CO_CUR != 0 ? f.CO_CUR == strP_CO_CUR : 0 == 0)
                            && (strP_CO_TUR != 0 ? f.CO_TUR == strP_CO_TUR : 0 == 0)
                            && (strP_CO_MODU_CUR != 0 ? f.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                            && (strP_CO_ANO_REF != "0" ? f.CO_ANO_MES_MAT == strP_CO_ANO_REF : 0 == 0)
                            && (strP_CO_MAT != 0 ? f.CO_MAT == strP_CO_MAT : 0 == 0)
                            && (strP_CO_ALU != 0 ? f.CO_ALU == strP_CO_ALU : 0 == 0)

                           select new RelMapaFalAlunoPorDiscAnual
                           {
                               noAlu = a.NO_ALU,
                               coMat = f.CO_MAT,
                               coCur = c.CO_CUR,
                               coAlu = a.CO_ALU,
                               coModuCur = f.CO_MODU_CUR,
                               coTur = f.CO_TUR,
                               //dtFreqAno = coAno
                           }).Distinct().OrderBy(r => r.noAlu);

                // Erro: não encontrou registros
                if (lst.ToList().Count == 0)
                    return -1;

                var res = lst.ToList();

                #endregion                

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelMapaFalAlunoPorDiscAnual at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }            
        }

        #endregion

        #region Classe Lista Pauta Chamada

        public class RelMapaFalAlunoPorDiscAnual
        {
            public string noAlu { get; set; }
            public int coAlu { get; set; }
            public int coMat { get; set; }
            public int coCur { get; set; }
            public int coTur { get; set; }
            public int coModuCur { get; set; }
            public int dtFreqAno { get; set; }
            public int TotJan
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 1 select new { f.DT_FRE }).Count();
                }
            }
            public int TotFev
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 2 select new { f.DT_FRE }).Count();
                }
            }
            public int TotMar
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 3 select new { f.DT_FRE }).Count();
                }
            }
            public int TotAbr
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 4 select new { f.DT_FRE }).Count();
                }
            }
            public int TotMai
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 5 select new { f.DT_FRE }).Count();
                }
            }
            public int TotJun
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 6 select new { f.DT_FRE }).Count();
                }
            }
            public int TotJul
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 7 select new { f.DT_FRE }).Count();
                }
            }
            public int TotAgo
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 8 select new { f.DT_FRE }).Count();
                }
            }
            public int TotSet
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 9 select new { f.DT_FRE }).Count();
                }
            }
            public int TotOut
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 10 select new { f.DT_FRE }).Count();
                }
            }
            public int TotNov
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 11 select new { f.DT_FRE }).Count();
                }
            }
            public int TotDez
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno && f.DT_FRE.Month == 12 select new { f.DT_FRE }).Count();
                }
            }
            public int Total
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    return (from f in ctx.TB132_FREQ_ALU where f.TB07_ALUNO.CO_ALU == coAlu && f.CO_MAT == coMat && f.TB01_CURSO.CO_CUR == coCur && f.CO_TUR == coTur && f.TB01_CURSO.CO_MODU_CUR == coModuCur && f.CO_FLAG_FREQ_ALUNO == "N" && f.DT_FRE.Year == dtFreqAno select new { f.DT_FRE }).Count();
                }
            }
        }
        #endregion
    }
}
