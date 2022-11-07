using System;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using DevExpress.XtraReports.UI;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno
{
    public partial class RptResumoDiarioMatriculas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptResumoDiarioMatriculas()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              int codUndContrato,
                              string coAnoMesmat,
                              int coModuCur,
                              int coCur,
                              int coTur,
                              DateTime dtInicio,
                              DateTime dtFim,
                              string infos)
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

                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Report

                var lst = (from tb08 in ctx.TB08_MATRCUR
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb08.CO_TUR equals tb129.CO_TUR
                           where (coAnoMesmat != "T" ? tb08.CO_ANO_MES_MAT == coAnoMesmat : coAnoMesmat == "T")
                           && (coModuCur != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == coModuCur : coModuCur == 0)
                           && (coCur != 0 ? tb08.CO_CUR == coCur : coCur == 0)
                           && (coTur != 0 ? tb08.CO_TUR == coTur : coTur == 0)
                           && (codUndContrato != 0 ? tb08.CO_EMP_UNID_CONT == codUndContrato : codUndContrato == 0)
                           && (tb08.DT_EFE_MAT >= dtInicio && tb08.DT_EFE_MAT <= dtFim)
                           group new
                           {
                               tb08.DT_EFE_MAT,
                               tb08.DT_EFE_MAT.Year,
                               tb08.DT_EFE_MAT.Month,
                               tb08.DT_EFE_MAT.Day,
                               tb08.VL_DES_BOL_MOD_MAT,
                               tb08.VL_PAR_MOD_MAT,
                               tb08.VL_TOT_MODU_MAT,
                               tb08.VL_DES_MOD_MAT,
                               tb01.CO_SIGL_CUR,
                               tb129.CO_SIGLA_TURMA,
                               tb08.TB44_MODULO.DE_MODU_CUR
                           } by new { tb08.DT_EFE_MAT.Year, tb08.DT_EFE_MAT.Month, tb08.DT_EFE_MAT.Day, tb08.TB44_MODULO.DE_MODU_CUR, tb01.CO_SIGL_CUR, tb129.CO_SIGLA_TURMA } into grp
                           select new ResumoDiarioMatricula
                           {
                               VlMensalidade = grp.Sum(x => (x.VL_PAR_MOD_MAT ?? 0)),
                               VlContrato = grp.Sum(x => (x.VL_TOT_MODU_MAT ?? 0)),
                               VlDesctoBolsa = grp.Sum(x => (x.VL_DES_BOL_MOD_MAT ?? 0)),
                               VlDesctoEspecial = grp.Sum(x => (x.VL_DES_MOD_MAT ?? 0)),
                               Modalidade = grp.FirstOrDefault().DE_MODU_CUR,
                               Serie = grp.FirstOrDefault().CO_SIGL_CUR,
                               Turma = grp.FirstOrDefault().CO_SIGLA_TURMA,
                               DtEfetivacao = grp.FirstOrDefault().DT_EFE_MAT,
                               Qtd = grp.Count()
                           }).OrderBy(p => p.DtEfetivacao).ThenBy(p => p.Modalidade).ToList();

                var res = lst;

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper Report

        public class ResumoDiarioMatricula
        {
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public decimal? VlMensalidade { get; set; }
            public decimal? VlContrato { get; set; }
            public decimal? VlDesctoBolsa { get; set; }
            public decimal? VlDesctoEspecial { get; set; }
            public DateTime DtEfetivacao { get; set; }
            public int Qtd { get; set; }

            public decimal VlLiquido
            {
                get
                {
                    if ((!this.VlContrato.HasValue || this.VlContrato == 0))
                        return 0;

                    return this.VlContrato.Value - this.VlDesctoBolsa.Value - this.VlDesctoEspecial.Value;
                }
            }

            public decimal PercDescBolsa
            {
                get
                {
                    if ((!VlContrato.HasValue || VlContrato == 0) && (!VlDesctoBolsa.HasValue))
                        return 0;

                    if (this.VlContrato.Value != 0)
                        return ((VlDesctoBolsa ?? 0) * 100) / VlContrato.Value;

                    else
                        return decimal.Parse("0,00");
                }
            }

            public decimal PercDescEspec
            {
                get
                {
                    if ((!VlContrato.HasValue || VlContrato == 0) && (!VlDesctoEspecial.HasValue))
                        return 0;

                    if(this.VlContrato != 0)
                        return ((VlDesctoEspecial ?? 0) * 100) / VlContrato.Value;

                    else
                        return decimal.Parse("0,00");
                }
            }

            public decimal PercVlLiquido
            {
                get
                {
                    if ((!VlContrato.HasValue || VlContrato == 0))
                        return 0;

                    if (this.VlContrato != 0)
                        return ((this.VlContrato.Value - this.VlDesctoBolsa.Value - this.VlDesctoEspecial.Value) * 100) / VlContrato.Value;

                    else
                        return decimal.Parse("0,00");
                }
            }
        }

        #endregion
    }
}
