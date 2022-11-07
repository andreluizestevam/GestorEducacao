using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;
using System.Data;
using System.Web;
using System.Resources;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7200_CtrlColaborPerson
{
    public partial class RptRelFuncParamPorFuncao : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelFuncParamPorFuncao()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int UnidCadastro,
                        int UnidContrato,
                        int regiao,
                        int area,
                        int subarea,
                        string uf,
                        int Cidade,
                        int Bairro,
                        int classFunc,
                        int categoria,
                        int especializa,
                        bool comGrafico,
                        bool comRelatorio
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                //Mostra a "Band" com o Gráfico apenas caso isso tenha sido solicitado na página de parâmetros
                ReportHeader.Visible = comGrafico;
                GroupHeader1.Visible = DetailContent.Visible = ReportFooter.Visible = comRelatorio;

                //Retorna mensagem padrão de sem dados, caso não tenha sido escolhido Com gráficos nem Com Relatório
                if (comGrafico == false && comRelatorio == false)
                    return -1;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           join tb128 in TB128_FUNCA_FUNCI.RetornaTodosRegistros() on tb03.TB128_FUNCA_FUNCI.ID_FUNCA_FUNCI equals tb128.ID_FUNCA_FUNCI
                           where (classFunc != 0 ? tb128.ID_FUNCA_FUNCI == classFunc : 0 == 0)
                           && (UnidContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidContrato : 0 == 0)
                           && (uf != "0" ? tb03.CO_ESTA_ENDE_COL == uf : 0 == 0)
                           && (Cidade != 0 ? tb03.CO_CIDADE == Cidade : 0 == 0)
                           && (Bairro != 0 ? tb03.CO_BAIRRO == Bairro : 0 == 0)
                           && (categoria != 0 ? tb03.TB127_CATEG_FUNCI.ID_CATEG_FUNCI == categoria : 0 == 0)
                           && (especializa != 0 ? tb03.CO_ESPEC == especializa : 0 == 0)
                           select new
                           {
                               //coEmp = tb03.CO_EMP,
                               //noEmp = tb25.NO_FANTAS_EMP,

                               noFunc = tb128.NO_FUNCA_FUNCI,
                               flPermPlant = tb03.FL_PERM_PLANT,
                               flAtiviInter = tb03.FL_ATIVI_INTER,
                               flAtiviExter = tb03.FL_ATIVI_EXTER,
                               flAtiviDomic = tb03.FL_ATIVI_DOMIC,
                               coSitu = tb03.CO_SITU_COL,
                               coTpCon = tb03.CO_TPCON,
                           }).OrderBy(m => m.noFunc).ToList();

                var l = (from r in res
                         group r by new
                         {
                             noFunc = r.noFunc
                         } into g
                         select new DemonstrativoDistruibuicaoFunc
                         {
                             noFuncao = g.Key.noFunc,

                             ATI = g.Count(c => c.coSitu == "ATI"),
                             FER = g.Count(c => c.coSitu == "FER"),
                             LME = g.Count(c => c.coSitu == "LME"),
                             LMA = g.Count(c => c.coSitu == "LMA"),

                             QPL = g.Count(c => c.flPermPlant == "S"),
                             QAI = g.Count(c => c.flAtiviInter == "S"),
                             QAE = g.Count(c => c.flAtiviExter == "S"),
                             QAD = g.Count(c => c.flAtiviDomic == "S"),

                             CLT = g.Count(c => c.coTpCon == 7),
                             EST = g.Count(c => c.coTpCon == 5),
                             RPA = g.Count(c => c.coTpCon == 6),
                             PJ = g.Count(c => c.coTpCon == 3),
                             CTR = g.Count(c => c.coTpCon == 8),
                             OTR = g.Count(c => c.coTpCon == 14),

                             total = g.Count()
                         }).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();


                int auxCount = 0;
                int ATI, FER, LME, LMA, QPL, QAI, QAE, QAD, CLT, EST, RPA, PJ, CTR, OTR;
                ATI = FER = LME = LMA = QPL = QAI = QAE = QAD = CLT = EST = RPA = PJ = CTR = OTR = 0;

                foreach (DemonstrativoDistruibuicaoFunc at in l)
                {
                    auxCount++;

                    //Só faz no último registro para economia de desempenho
                    if (auxCount == l.Count)
                    {
                        //Calcula os totais para criar os Gráficos
                        foreach (DemonstrativoDistruibuicaoFunc li in l)
                        {
                            ATI += li.ATI;
                            FER += li.FER;
                            LME += li.LME;
                            LMA += li.LMA;

                            QPL += li.QPL;
                            QAI += li.QAI;
                            QAE += li.QAE;
                            QAD += li.QAD;

                            CLT += li.CLT;
                            EST += li.EST;
                            RPA += li.RPA;
                            PJ += li.PJ;
                            CTR += li.CTR;
                            OTR += li.OTR;
                            PJ += li.PJ;
                        }
                    }

                    bsReport.Add(at);
                }

                #region Tratamento dos Gráficos

                //Alimenta o Primeiro Gráfico
                Series series1 = new Series("nova", ViewType.Doughnut3D);
                series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("ATI", ATI));
                series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("FER", FER));
                series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("LME", LME));
                series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("LMA", LMA));
                series1.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                series1.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                xrChart1.Series.Add(series1);

                //Alimenta o Segundo Gráfico
                Series series2 = new Series("nova2", ViewType.Doughnut);
                series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QPL", QPL));
                series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QAI", QAI));
                series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QAE", QAE));
                series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QAD", QAD));
                series2.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                series2.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                series2.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                xrChart2.Series.Add(series2);

                //Alimenta o Terceiro Gráfico
                Series series3 = new Series("nova3", ViewType.Pie);
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("CLT", CLT));
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("EST", EST));
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("RPA", RPA));
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("PJ", PJ));
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("CTR", CTR));
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("OTR", OTR));
                series3.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                series3.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                series3.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                xrChart3.Series.Add(series3);

                #endregion

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class DemonstrativoDistruibuicaoFunc
        {
            //situação do Funcionário
            public string Situacao { get; set; }

            //Informações da Empresa
            public string noFuncao { get; set; }
            public int? total { get; set; }

            //Informações Situação Funcional
            public int ATI { get; set; }
            public int FER { get; set; }
            public int LME { get; set; }
            public int LMA { get; set; }

            //Informações Atividade Funcional
            public int QPL { get; set; }
            public int QAI { get; set; }
            public int QAE { get; set; }
            public int QAD { get; set; }

            //Informações Contrato
            public int CLT { get; set; }
            public int EST { get; set; }
            public int RPA { get; set; }
            public int PJ { get; set; }
            public int CTR { get; set; }
            public int OTR { get; set; }
        }
    }
}

