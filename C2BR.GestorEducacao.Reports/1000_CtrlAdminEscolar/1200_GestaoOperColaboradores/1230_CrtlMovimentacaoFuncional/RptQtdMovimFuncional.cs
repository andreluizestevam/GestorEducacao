using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1230_CrtlMovimentacaoFuncional
{
    public partial class RptQtdMovimFuncional : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region Fields

        private int anoBase;

        #endregion

        #region ctor

        public RptQtdMovimFuncional()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codOrgao, int codEmpRef, int anoBase, string infos)
        {
            try
            {
                #region Calcula os anos

                if (anoBase == 0)
                    this.anoBase = DateTime.Now.Year;
                else
                    this.anoBase = anoBase;

                List<int> lstAnos = new List<int>() { anoBase, anoBase - 1, anoBase - 2, anoBase - 3, anoBase - 4, };

                #endregion

                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Ocorrencias

                var res = (from tb286 in ctx.TB286_MOVIM_TRANSF_FUNCI
                           where (codEmpRef != 0 ? tb286.TB25_EMPRESA1.CO_EMP == codEmpRef : codEmpRef == 0)
                           && tb286.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codOrgao
                           && lstAnos.Contains(tb286.DT_INI_MOVIM_TRANSF_FUNCI.Year)
                           group tb286 by new
                           {
                               tb286.TB25_EMPRESA1.NO_FANTAS_EMP,
                               tb286.DT_INI_MOVIM_TRANSF_FUNCI.Year
                           } into grp
                           select new Res
                           {
                               Ano = grp.Key.Year,
                               Unidade = grp.Key.NO_FANTAS_EMP,
                               TotalME = grp.Where(x => x.CO_TIPO_MOVIM == "ME").Count(),
                               TotalTE = grp.Where(x => x.CO_TIPO_MOVIM == "TE").Count(),
                               TotalMI = grp.Where(x => x.CO_TIPO_MOVIM == "MI").Count()
                           }).ToList();

                if (res.Count() == 0)
                    return -1;

                List<QuantMovimento> lm = res.DistinctBy(x => x.Unidade)
                    .Select(x => new QuantMovimento { Unidade = x.Unidade }).ToList();

                foreach (var mv in lm)
                {
                    mv.Ano1 = this.anoBase;
                    mv.Ano1ME = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano1).DefaultIfEmpty(new Res()).First().TotalME;
                    mv.Ano1TE = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano1).DefaultIfEmpty(new Res()).First().TotalTE;
                    mv.Ano1MI = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano1).DefaultIfEmpty(new Res()).First().TotalMI;

                    mv.Ano2 = this.anoBase - 1;
                    mv.Ano2ME = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano2).DefaultIfEmpty(new Res()).First().TotalME;
                    mv.Ano2TE = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano2).DefaultIfEmpty(new Res()).First().TotalTE;
                    mv.Ano2MI = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano2).DefaultIfEmpty(new Res()).First().TotalMI;

                    mv.Ano3 = this.anoBase - 2;
                    mv.Ano3ME = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano3).DefaultIfEmpty(new Res()).First().TotalME;
                    mv.Ano3TE = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano3).DefaultIfEmpty(new Res()).First().TotalTE;
                    mv.Ano3MI = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano3).DefaultIfEmpty(new Res()).First().TotalMI;

                    mv.Ano4 = this.anoBase - 3;
                    mv.Ano4ME = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano4).DefaultIfEmpty(new Res()).First().TotalME;
                    mv.Ano4TE = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano4).DefaultIfEmpty(new Res()).First().TotalTE;
                    mv.Ano4MI = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano4).DefaultIfEmpty(new Res()).First().TotalMI;

                    mv.Ano5 = this.anoBase - 4;
                    mv.Ano5ME = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano5).DefaultIfEmpty(new Res()).First().TotalME;
                    mv.Ano5TE = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano5).DefaultIfEmpty(new Res()).First().TotalTE;
                    mv.Ano5MI = res.Where(x => x.Unidade == mv.Unidade && x.Ano == mv.Ano5).DefaultIfEmpty(new Res()).First().TotalMI;
                }

                #endregion

                // Adiciona os movimentos ao DataSource do Relatório
                bsReport.Clear();

                foreach (var m in lm)
                    bsReport.Add(m);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Cell Anos

        private void celAno_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cel = sender as XRTableCell;
            int sub = int.Parse(cel.Tag as string);
            cel.Text = (this.anoBase - sub).ToString();
        }

        #endregion

        #region Class QuantMovimento

        public class QuantMovimento
        {
            public string Unidade { get; set; }
            public int Ano1 { get; set; }
            public int Ano1ME { get; set; }
            public int Ano1MI { get; set; }
            public int Ano1TE { get; set; }
            public int Ano1Total { get { return Ano1ME + Ano1MI + Ano1TE; } }
            public int Ano2 { get; set; }
            public int Ano2ME { get; set; }
            public int Ano2MI { get; set; }
            public int Ano2TE { get; set; }
            public int Ano2Total { get { return Ano2ME + Ano2MI + Ano2TE; } }
            public int Ano3 { get; set; }
            public int Ano3ME { get; set; }
            public int Ano3MI { get; set; }
            public int Ano3TE { get; set; }
            public int Ano3Total { get { return Ano3ME + Ano3MI + Ano3TE; } }
            public int Ano4 { get; set; }
            public int Ano4ME { get; set; }
            public int Ano4MI { get; set; }
            public int Ano4TE { get; set; }
            public int Ano4Total { get { return Ano4ME + Ano4MI + Ano4TE; } }
            public int Ano5 { get; set; }
            public int Ano5ME { get; set; }
            public int Ano5MI { get; set; }
            public int Ano5Total { get { return Ano5ME + Ano5MI + Ano5TE; } }
            public int Ano5TE { get; set; }

            public int TotalGeral { get { return Ano1Total + Ano2Total + Ano3Total + Ano4Total + Ano5Total; } }
        }

        class Res
        {
            public int Ano { get; set; }
            public int TotalME { get; set; }
            public int TotalMI { get; set; }
            public int TotalTE { get; set; }
            public string Unidade { get; set; }
        }

        #endregion
    }
}
