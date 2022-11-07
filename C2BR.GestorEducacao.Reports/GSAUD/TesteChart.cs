using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD
{
    public partial class TesteChart : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public TesteChart()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport()
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = "TESTE DE GRÁFICOS";

                // Cria o header a partir do cod da instituicao
                //var header = ReportHeader.GetHeaderFromEmpresa(221);
                //if (header == null)
                    //return -1;

                // Inicializa o header
                //base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           group tb03 by new {
                               UF = tb03.CO_ESTA_ENDE_COL
                           } into g
                           select new ValGrafico
                           {
                               x = g.Key.UF,
                               y = g.Count()
                           }).ToList();

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (var o in res)
                    bsReport.Add(o);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ValGrafico
        {
            public int y { get; set; }
            public string x { get; set; }
        }
    }
}
