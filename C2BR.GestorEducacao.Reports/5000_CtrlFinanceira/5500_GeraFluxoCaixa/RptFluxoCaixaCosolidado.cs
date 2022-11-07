using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa
{
    public partial class RptFluxoCaixaConsolidado : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptFluxoCaixaConsolidado()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              int codEmpRef,
                              DateTime dtInicio,
                              DateTime dtFim,
                              string origemPgto,
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

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Saldo/Data Inicial

                DateTime dtSaldoInicio = dtInicio;

                var siEmp = (from e in ctx.TB25_EMPRESA
                             where e.CO_EMP == codEmpRef
                             select new
                             {
                                 Data = e.DT_SALDO_INICIAL,
                                 Valor = e.VL_SALDO_INICIAL
                             }).FirstOrDefault();

                decimal? si = 0;

                if (dtInicio > siEmp.Data.Value)
                {
                    si = (from fc in ctx.TB319_FLUXO_CAIXA
                          where fc.DT_MOVIM_FLUXO_CAIXA <= dtInicio
                          select fc.VL_MOVIM_FLUXO_CAIXA
                          ).ToList().OfType<decimal?>().Sum();
                }

                decimal saldoInicial = (siEmp.Valor ?? 0) + (si ?? 0);

                #endregion

                #region Query Fluxo

                List<FluxoCaixaRelatorio> lst = null;

                lst = (from fc in ctx.TB319_FLUXO_CAIXA
                       where fc.TB320_CTRL_FLUXO_CAIXA.TB25_EMPRESA.CO_EMP == codEmpRef
                       && (origemPgto != "T" ? fc.FL_ORIGEM_PGTO == origemPgto : origemPgto == "T")
                       && fc.DT_MOVIM_FLUXO_CAIXA >= dtInicio
                       && fc.DT_MOVIM_FLUXO_CAIXA <= dtFim
                       select fc).ToList().GroupBy(x => x.DT_MOVIM_FLUXO_CAIXA.Date)
                       .Select(y => new FluxoCaixaRelatorio
                           {
                               DtMovimento = y.Key,
                               QTT = y.Count(),
                               // Contas a Pagar
                               CAP_QTB = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "D" && x.FL_ORIGEM_PGTO == "B").Count(),
                               CAP_VL_QTB = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "D" && x.FL_ORIGEM_PGTO == "B")
                                    .Sum(x => x.VL_MOVIM_FLUXO_CAIXA),
                               CAP_QTC = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "D" && x.FL_ORIGEM_PGTO == "C").Count(),
                               CAP_VL_QTC = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "D" && x.FL_ORIGEM_PGTO == "C")
                                    .Sum(x => x.VL_MOVIM_FLUXO_CAIXA),
                               // Contas a Receber
                               CAR_QTB = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "R" && x.FL_ORIGEM_PGTO == "B").Count(),
                               CAR_VL_QTB = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "R" && x.FL_ORIGEM_PGTO == "B")
                                    .Sum(x => x.VL_MOVIM_FLUXO_CAIXA),
                               CAR_QTC = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "R" && x.FL_ORIGEM_PGTO == "C").Count(),
                               CAR_VL_QTC = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "R" && x.FL_ORIGEM_PGTO == "C")
                                    .Sum(x => x.VL_MOVIM_FLUXO_CAIXA),

                               Despesa = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "D").Sum(x => x.VL_MOVIM_FLUXO_CAIXA),
                               Receita = y.Where(x => x.TP_MOVIM_FLUXO_CAIXA == "R").Sum(x => x.VL_MOVIM_FLUXO_CAIXA)
                           }).OrderBy(p => p.DtMovimento).ToList();

                #endregion

                // Erro: não encontrou registros
                if (lst == null || lst.Count == 0)
                    return -1;

                decimal saldoAtual = saldoInicial;

                foreach (var fc in lst)
                {
                    saldoAtual = saldoAtual - (fc.Despesa ?? 0) + (fc.Receita ?? 0);
                    fc.Saldo = saldoAtual;
                }

                celSaldoFinal.Text = saldoAtual.ToString("n");

                lst.Insert(0, new FluxoCaixaRelatorio()
                {
                    //Descricao = "Saldo Inicial",
                    DtMovimento = dtSaldoInicio,
                    Saldo = saldoInicial
                });

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (var fc in lst)
                    bsReport.Add(fc);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper

        public class FluxoCaixaRelatorio
        {
            public DateTime DtMovimento { get; set; }
            public int? QTT { get; set; }
            public int? CAP_QTB { get; set; }
            public int? CAP_QTC { get; set; }
            public decimal? CAP_VL_QTB { get; set; }
            public decimal? CAP_VL_QTC { get; set; }
            public int? CAR_QTB { get; set; }
            public int? CAR_QTC { get; set; }
            public decimal? CAR_VL_QTB { get; set; }
            public decimal? CAR_VL_QTC { get; set; }
            public decimal? Receita { get; set; }
            public decimal? Despesa { get; set; }
            public decimal? Saldo { get; set; }
        }

        #endregion

        #region Events

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel lbl = sender as XRLabel;

            decimal vl;
            if (decimal.TryParse(lbl.Text, out vl))
            {
                if (vl == 0)
                    lbl.ForeColor = Color.Black;
                else if (vl < 0)
                    lbl.ForeColor = Color.Red;
                else
                    lbl.ForeColor = Color.Navy;

            }
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cel = sender as XRTableCell;
            DateTime dt = DateTime.ParseExact(cel.Text, "dd/MM/yy", null);
            if (dt > DateTime.Today)
                cel.Row.ForeColor = Color.Navy;
        }

        #endregion
    }
}
