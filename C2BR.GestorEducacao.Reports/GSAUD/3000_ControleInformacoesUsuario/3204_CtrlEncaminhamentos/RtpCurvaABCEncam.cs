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
using System.Web;
//using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3204_CtrlEncaminhamentos
{
    public partial class RtpCurvaABCEncam : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RtpCurvaABCEncam()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int CoUnidFunc,
                              int CoUnidPlant,
                              int coEspecPlant,
                              string situacaoFuncional,
                              string dataIni,
                              string dataFim,
                              int coDept,
                              string Periodo,
                              int Ordenacao,
                              int coInst,
                              bool apenasAgendados,
                              bool comGraficos,
                              string coTipoOrdem,
                              bool comRelatorio
            )
        {
            try
            {
                #region Inicializa o header/Labels

                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.lblTitulo.Text = "CURVA ABC DE ENCAMINHAMENTOS" + Periodo;

                //Mostra a "Band" com o Gráfico apenas caso isso tenha sido solicitado na página de parâmetros
                ReportHeader.Visible = comGraficos;
                GroupHeader1.Visible = DetailContent.Visible = ReportFooter.Visible = comRelatorio;

                //Retorna mensagem padrão de sem dados, caso não tenha sido escolhido Com gráficos nem Com Relatório
                if (comGraficos == false && comRelatorio == false)
                    return -1;

                // Cria o header a partir do cod da instituicao
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                //Dependendo do escolhido pelo usuário, mostra ou não os colaboradores com/sem plantões
                if (apenasAgendados == false)
                {
                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               join tb25Col in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25Col.CO_EMP
                               where (CoUnidFunc != 0 ? tb03.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                                   //                               && tb03.FL_PERM_PLANT == "S"
                               && (situacaoFuncional != "0" ? tb03.CO_SITU_COL == situacaoFuncional : 0 == 0)
                               select new CurvaABCEncaminhamentos
                               {
                                   //Dados do Colaborador
                                   noCol = tb03.NO_COL,
                                   CoCol = tb03.CO_COL,
                                   matCol = tb03.CO_MAT_COL,
                                   funCol = tb03.DE_FUNC_COL,
                                   unidCol = tb25Col.sigla,

                                   //Dados de auxlilio para querys
                                   CoUnidPlant = CoUnidPlant,
                                   coEspecPlant = coEspecPlant,
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                                   instituicao = coInst,
                                   coEmp = coEmp,

                               }).DistinctBy(w => w.CoCol).ToList();

                    //Ordena e classifica de acordo com os parâmetros escolhidos pelo usuário
                    switch (Ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.noCol).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.funCol).ThenBy(w => w.unidCol).ThenBy(b => b.noCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.funCol).ThenByDescending(w => w.unidCol).ThenByDescending(b => b.noCol).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.unidCol).ThenBy(o => o.funCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.unidCol).ThenByDescending(o => o.funCol).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTUE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTUE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTEM).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTEM).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTMU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTMU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTUG).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTUG).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTPU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTPU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTNU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTNU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTTE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTTE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int auxCount = 0;
                    decimal TOTAL_QTEM_PER = 0;
                    decimal TOTAL_QTMU_PER = 0;
                    decimal TOTAL_QTUG_PER = 0;
                    decimal TOTAL_QTPU_PER = 0;
                    decimal TOTAL_QTNU_PER = 0;
                    decimal TOTAL_QTTE_PER = 0;

                    int a;
                    int bi;
                    int c;
                    int d;
                    int e;
                    a = bi = c = d = e = 0;
                    foreach (CurvaABCEncaminhamentos at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;

                        TOTAL_QTEM_PER += decimal.Parse(at.QTEM_PER);
                        TOTAL_QTMU_PER += decimal.Parse(at.QTPU_PER);
                        TOTAL_QTUG_PER += decimal.Parse(at.QTUG_PER);
                        TOTAL_QTPU_PER += decimal.Parse(at.QTPU_PER);
                        TOTAL_QTNU_PER += decimal.Parse(at.QTNU_PER);
                        TOTAL_QTTE_PER += decimal.Parse(at.QTTE_PER);

                        at.TOTAL_QTEM_PER = decimal.Floor(TOTAL_QTEM_PER).ToString();
                        at.TOTAL_QTMU_PER = decimal.Floor(TOTAL_QTMU_PER).ToString();
                        at.TOTAL_QTUG_PER = decimal.Floor(TOTAL_QTUG_PER).ToString();
                        at.TOTAL_QTPU_PER = decimal.Floor(TOTAL_QTPU_PER).ToString();
                        at.TOTAL_QTNU_PER = decimal.Floor(TOTAL_QTNU_PER).ToString();
                        at.TOTAL_QTTE_PER = decimal.Floor(TOTAL_QTTE_PER).ToString();

                        a = at.TOTAL_QTEM;
                        bi = at.TOTAL_QTMU;
                        c = at.TOTAL_QTUG;
                        d = at.TOTAL_QTPU;
                        e = at.TOTAL_QTNU;

                        controlaCoresReport(Ordenacao);

                        bsReport.Add(at);
                    }

                    Series series1 = new Series("nova", ViewType.Pie);
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTEM", a));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTMU", bi));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTUG", c));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTPU", d));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTNU", e));
                    series1.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                    series1.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    xrChart3.Series.Add(series1);

                    return 1;
                }
                else
                {
                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               join tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros() on tb03.CO_COL equals tbs195.CO_COL
                               join tb25Col in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25Col.CO_EMP
                               where (CoUnidFunc != 0 ? tb03.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                               && tb03.FL_PERM_PLANT == "S"
                               && (CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == CoUnidPlant : 0 == 0)
                               && (coEspecPlant != 0 ? tbs195.CO_ESPEC == coEspecPlant : 0 == 0)
                               && (situacaoFuncional != "0" ? tb03.CO_SITU_COL == situacaoFuncional : 0 == 0)
                               && (coDept != 0 ? tbs195.CO_DEPTO_ENCAM_MEDIC == coDept : 0 == 0)
                               && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                               //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= dataIni1.Year))
                               //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= dataFim1.Year))

                               select new CurvaABCEncaminhamentos
                               {
                                   //Dados do Colaborador
                                   noCol = tb03.NO_COL,
                                   CoCol = tb03.CO_COL,
                                   matCol = tb03.CO_MAT_COL,
                                   funCol = tb03.DE_FUNC_COL,
                                   unidCol = tb25Col.sigla,

                                   //Dados de auxlilio para querys
                                   CoUnidPlant = CoUnidPlant,
                                   coEspecPlant = coEspecPlant,
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                                   instituicao = coInst,
                                   coEmp = coEmp,
                               }).DistinctBy(w => w.CoCol).ToList();

                    //Ordena e classifica de acordo com os parâmetros escolhidos pelo usuário
                    switch (Ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.noCol).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.funCol).ThenBy(w => w.unidCol).ThenBy(b => b.noCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.funCol).ThenByDescending(w => w.unidCol).ThenByDescending(b => b.noCol).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.unidCol).ThenBy(o => o.funCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.unidCol).ThenByDescending(o => o.funCol).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTUE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTUE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTEM).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTEM).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTMU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTMU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTUG).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTUG).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTPU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTPU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTNU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTNU).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTTE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTTE).ThenBy(o => o.unidCol).ThenBy(o => o.noCol).ToList();
                            break;
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int auxCount = 0;
                    decimal TOTAL_QTEM_PER = 0;
                    decimal TOTAL_QTMU_PER = 0;
                    decimal TOTAL_QTUG_PER = 0;
                    decimal TOTAL_QTPU_PER = 0;
                    decimal TOTAL_QTNU_PER = 0;
                    decimal TOTAL_QTTE_PER = 0;

                    int a;
                    int bi;
                    int c;
                    int d;
                    int e;
                    a = bi = c = d = e = 0;

                    foreach (CurvaABCEncaminhamentos at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;

                        TOTAL_QTEM_PER += decimal.Parse(at.QTEM_PER);
                        TOTAL_QTMU_PER += decimal.Parse(at.QTPU_PER);
                        TOTAL_QTUG_PER += decimal.Parse(at.QTUG_PER);
                        TOTAL_QTPU_PER += decimal.Parse(at.QTPU_PER);
                        TOTAL_QTNU_PER += decimal.Parse(at.QTNU_PER);
                        TOTAL_QTTE_PER += decimal.Parse(at.QTTE_PER);

                        at.TOTAL_QTEM_PER = decimal.Floor(TOTAL_QTEM_PER).ToString();
                        at.TOTAL_QTMU_PER = decimal.Floor(TOTAL_QTMU_PER).ToString();
                        at.TOTAL_QTUG_PER = decimal.Floor(TOTAL_QTUG_PER).ToString();
                        at.TOTAL_QTPU_PER = decimal.Floor(TOTAL_QTPU_PER).ToString();
                        at.TOTAL_QTNU_PER = decimal.Floor(TOTAL_QTNU_PER).ToString();
                        at.TOTAL_QTTE_PER = decimal.Floor(TOTAL_QTTE_PER).ToString();

                        a = at.TOTAL_QTEM;
                        bi = at.TOTAL_QTMU;
                        c = at.TOTAL_QTUG;
                        d = at.TOTAL_QTPU;
                        e = at.TOTAL_QTNU;

                        controlaCoresReport(Ordenacao);

                        //Muda a cor da coluna de acordo com a ordenação escolhida


                        bsReport.Add(at);
                    }

                    Series series1 = new Series("nova", ViewType.Pie);
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTEM", a));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTMU", bi));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTUG", c));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTPU", d));
                    series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTNU", e));
                    series1.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                    series1.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    xrChart3.Series.Add(series1);

                    return 1;
                }
            }
            catch { return 0; }
        }

        private void controlaCoresReport(int Ordenacao)
        {
            switch (Ordenacao)
            {
                case 1:
                    xrTableCell22.ForeColor = Color.RoyalBlue;
                    break;
                case 2:
                    xrTableCell25.ForeColor = Color.RoyalBlue;
                    break;
                case 3:
                    xrTableCell23.ForeColor = Color.RoyalBlue;
                    break;
                case 4:
                    xrTableCell27.ForeColor = xrTableCell26.ForeColor = xrTableCell59.ForeColor = Color.RoyalBlue;
                    break;
                case 5:
                    xrTableCell30.ForeColor = xrTableCell33.ForeColor = xrTableCell55.ForeColor = xrTableCell54.ForeColor = Color.RoyalBlue;
                    break;
                case 6:
                    xrTableCell29.ForeColor = xrTableCell31.ForeColor = xrTableCell53.ForeColor = xrTableCell51.ForeColor = Color.RoyalBlue;
                    break;
                case 7:
                    xrTableCell35.ForeColor = xrTableCell34.ForeColor = xrTableCell50.ForeColor = xrTableCell49.ForeColor = Color.RoyalBlue;
                    break;
                case 8:
                    xrTableCell37.ForeColor = xrTableCell38.ForeColor = xrTableCell47.ForeColor = xrTableCell46.ForeColor = Color.RoyalBlue;
                    break;
                case 9:
                    xrTableCell39.ForeColor = xrTableCell45.ForeColor = xrTableCell40.ForeColor = xrTableCell44.ForeColor = Color.RoyalBlue;
                    break;
                case 10:
                    xrTableCell28.ForeColor = xrTableCell32.ForeColor = xrTableCell57.ForeColor = xrTableCell56.ForeColor = Color.RoyalBlue;
                    break;
            }
        }

        #endregion

        #region Class Extrato Plantão

        /// <summary>
        /// Método responsável por colocar primeira letra em maiúsculo
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public static string PrimeiraLetraMaiuscula(string palavra)
        {
            char primeira = char.ToUpper(palavra[0]);
            return primeira + palavra.Substring(1);
        }

        public class DadosPieChart
        {
            public string NOME { get; set; }
            public int qtde { get; set; }
        }

        public class CurvaABCEncaminhamentos
        {
            //Dados do Colaborador
            public string noCol { get; set; }
            public int CoCol { get; set; }
            public string matCol { get; set; }
            public string funCol { get; set; }
            public string unidCol { get; set; }
            public string noColValid
            {
                get
                {
                    return this.matCol + " - " + (this.noCol.Length > 34 ? this.noCol.Substring(0, 34) + "..." : this.noCol);
                }
            }
            public int NCL { get; set; }

            //Dados de auxlilio para querys
            public int CoUnidPlant { get; set; }
            public int coEspecPlant { get; set; }
            public DateTime dataIni1 { get; set; }
            public DateTime dataFim1 { get; set; }
            public int instituicao { get; set; }
            public int coEmp { get; set; }

            //Quantidade total de Unidades e percentual
            public int QTUE
            {
                get
                {
                    int qtUnid = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                     && tbs195.CO_COL == this.CoCol
                                     && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                  select new { tbs195.CO_EMP_ENCAM_MEDIC }).Distinct().Count();

                    return qtUnid;
                }
            }
            public string QTUE_V
            {
                get
                {
                    return this.QTUE.ToString().PadLeft(2, '0');
                }
            }
            public string QTUE_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTUE > 0)
                    {
                        decimal aux1 = this.QTUE * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTUE;
                        if (aux2 >= 100)
                            return decimal.Floor(aux2).ToString("N1");
                        else
                            return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de encaminhamentos com classificação EMERGÊNCIA
            public int QTEM
            {
                get
                {
                    var qtEmer = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                     && tbs195.CO_COL == this.CoCol
                                     && tbs195.NR_CLASS_RISCO == 1
                                    && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                  select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtEmer;
                }
            }
            public string QTEM_V
            {
                get
                {
                    return this.QTEM.ToString().PadLeft(2, '0');
                }
            }
            public string QTEM_PER
            {
                get
                {
                    int horas = QTEM;

                    //Calcula a quantidade total de dias dentro do período em contexto
                    //TimeSpan ts = dataFim1.Subtract(dataIni1);
                    //int qtHorasTotal = int.Parse(ts.TotalHours.ToString());

                    if (this.TOTAL_QTEM > 0)
                    {
                        //Calcula o percentual
                        decimal aux1 = horas * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTEM;
                        if (aux2 >= 100)
                            return decimal.Floor(aux2).ToString("N1");
                        else
                            return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de encaminhamentos com classificação MUITO URGENTE
            public int QTMU
            {
                get
                {
                    int qtMedUrg = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                    where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                       && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                       && tbs195.CO_COL == this.CoCol
                                       && tbs195.NR_CLASS_RISCO == 2
                                       && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                    //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                    //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                    select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtMedUrg;
                }
            }
            public string QTMU_V
            {
                get
                {
                    return this.QTMU.ToString().PadLeft(2, '0');
                }
            }
            public string QTMU_PER
            {
                get
                {
                    //Calcula o percentual de dias trabalhados dentro do período
                    if (this.TOTAL_QTMU > 0)
                    {
                        decimal aux1 = this.QTMU * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTMU;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de encaminhamentos com classificação URGENTE
            public int QTUG
            {
                get
                {
                    int qtUrg = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                 where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                    && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                    && tbs195.CO_COL == this.CoCol
                                    && tbs195.NR_CLASS_RISCO == 3
                                  && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                 //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                 //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                 select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtUrg;
                }
            }
            public string QTUG_V
            {
                get
                {
                    return this.QTUG.ToString().PadLeft(2, '0');
                }
            }
            public string QTUG_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTUG > 0)
                    {
                        decimal aux1 = this.QTUG * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTUG;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de encaminhamentos com classificação POUCO URGENTE
            public int QTPU
            {
                get
                {
                    int qtPoucUrg = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                     where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                        && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                        && tbs195.CO_COL == this.CoCol
                                        && tbs195.NR_CLASS_RISCO == 4
                                      && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                     //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                     //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                     select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtPoucUrg;
                }
            }
            public string QTPU_V
            {
                get
                {
                    return this.QTPU.ToString().PadLeft(2, '0');
                }
            }
            public string QTPU_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTPU > 0)
                    {
                        decimal aux1 = this.QTPU * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTPU;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de encaminhamentos com classificação NÃO URGENTE
            public int QTNU
            {
                get
                {
                    int qtNaoUrge = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                     where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                        && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                        && tbs195.CO_COL == this.CoCol
                                        && tbs195.NR_CLASS_RISCO == 5
                                      && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                     //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                     //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                     select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtNaoUrge;
                }
            }
            public string QTNU_V
            {
                get
                {
                    return this.QTNU.ToString().PadLeft(2, '0');
                }
            }
            public string QTNU_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTNU > 0)
                    {
                        decimal aux1 = this.QTPU * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTNU;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            //Quantidade total de encaminhamentos dentro dos parâmetros
            public int QTTE
            {
                get
                {
                    int qtEncam = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                   where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                      && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                      && tbs195.CO_COL == this.CoCol
                                   && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                   //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                   //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                   select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtEncam;
                }
            }
            public string QTTE_V
            {
                get
                {
                    return this.QTTE.ToString().PadLeft(2, '0');
                }
            }
            public string QTTE_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTTE > 0)
                    {
                        decimal aux1 = this.QTTE * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTTE;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            //-----------------------------------------------------------------Calcula os totais-----------------------------------------------------------------
            //Total quantidade de unidades de encaminhamentos
            public int TOTAL_QTUE
            {
                get
                {
                    int qtUnid = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                     && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                  select new { tbs195.CO_EMP_ENCAM_MEDIC }).Distinct().Count();

                    return qtUnid;
                }
            }
            public string TOTAL_QTUE_V
            {
                get
                {
                    return this.TOTAL_QTUE.ToString().PadLeft(2, '0');
                }
            }

            //Quantidade total de encaminhamentos com classificação EMERGÊNCIA
            public int TOTAL_QTEM
            {
                get
                {
                    int qtEm = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                   && tbs195.NR_CLASS_RISCO == 1
                                   && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtEm;
                }
            }
            public string TOTAL_QTEM_V
            {
                get
                {
                    return this.TOTAL_QTEM.ToString().PadLeft(2, '0');
                }
            }
            public IList QTEM_LISTA
            {
                get
                {
                    IList qtEm = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                     && tbs195.NR_CLASS_RISCO == 1
                                       && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                  select new { tbs195.ID_ENCAM_MEDIC, tbs195.NR_CLASS_RISCO }).ToList();

                    return qtEm;
                }
            }

            public List<int> QTGERAL
            {
                get
                {
                    List<int> qtgeral = new List<int>();

                    qtgeral.Add(this.TOTAL_QTEM);
                    qtgeral.Add(this.TOTAL_QTMU);
                    qtgeral.Add(this.TOTAL_QTUG);
                    qtgeral.Add(this.TOTAL_QTPU);
                    qtgeral.Add(this.TOTAL_QTNU);

                    return qtgeral;
                }
            }


            //Quantidade total de encaminhamentos com classificação MUITO URGENTE
            public int TOTAL_QTMU
            {
                get
                {
                    int qtMU = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                   && tbs195.NR_CLASS_RISCO == 2
                                   && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtMU;
                }
            }
            public string TOTAL_QTMU_V
            {
                get
                {
                    return this.TOTAL_QTMU.ToString().PadLeft(2, '0');
                }
            }
            public IList QTMU_LISTA
            {
                get
                {
                    IList qtMU = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                     && tbs195.NR_CLASS_RISCO == 2
                                       && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                  //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                  select new { tbs195.ID_ENCAM_MEDIC, tbs195.NR_CLASS_RISCO }).ToList();

                    return qtMU;
                }
            }
            public IList COLABORADORES
            {
                get
                {
                    IList col = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                 where tb03.FL_PERM_PLANT == "S"
                                 select new { tb03.CO_COL }).ToList();
                    return col;
                }
            }
            public IList CLASSIFICACOES
            {
                get
                {
                    IList classifi = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                      select new { tbs195.NR_CLASS_RISCO }).Distinct().ToList();
                    return classifi;
                }
            }

            //Quantidade total de encaminhamentos com classificação URGENTE
            public int TOTAL_QTUG
            {
                get
                {
                    int qtUG = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                   && tbs195.NR_CLASS_RISCO == 3
                                   && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtUG;
                }
            }
            public string TOTAL_QTUG_V
            {
                get
                {
                    return this.TOTAL_QTUG.ToString().PadLeft(2, '0');
                }
            }

            //Quantidade total de encaminhamentos com classificação POUCO URGENTE
            public int TOTAL_QTPU
            {
                get
                {
                    int qtPU = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                   && tbs195.NR_CLASS_RISCO == 4
                                   && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtPU;
                }
            }
            public string TOTAL_QTPU_V
            {
                get
                {
                    return this.TOTAL_QTPU.ToString().PadLeft(2, '0');
                }
            }

            //Quantidade total de encaminhamentos com classificação NÃO URGENTE
            public int TOTAL_QTNU
            {
                get
                {
                    int qtNU = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                   && tbs195.NR_CLASS_RISCO == 4
                                   && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtNU;
                }
            }
            public string TOTAL_QTNU_V
            {
                get
                {
                    return this.TOTAL_QTNU.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QTTE
            {
                get
                {
                    int qtEncam = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                   where (this.CoUnidPlant != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == this.CoUnidPlant : 0 == 0)
                                      && (this.coEspecPlant != 0 ? tbs195.CO_ESPEC == this.coEspecPlant : 0 == 0)
                                     && ((tbs195.DT_ENCAM_MEDIC >= dataIni1) && (tbs195.DT_ENCAM_MEDIC <= dataFim1))
                                   //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day >= this.dataIni1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month >= this.dataIni1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year >= this.dataIni1.Year))
                                   //&& ((tbs195.DT_ENCAM_MEDIC.Value.Day <= this.dataFim1.Day) && (tbs195.DT_ENCAM_MEDIC.Value.Month <= this.dataFim1.Month) && (tbs195.DT_ENCAM_MEDIC.Value.Year <= this.dataFim1.Year))
                                   select new { tbs195.ID_ENCAM_MEDIC }).ToList().Count;

                    return qtEncam;
                }
            }
            public string TOTAL_QTTE_V
            {
                get
                {
                    return this.TOTAL_QTTE.ToString().PadLeft(2, '0');
                }
            }

            public string TOTAL_QTEM_PER { get; set; }
            public string TOTAL_QTMU_PER { get; set; }
            public string TOTAL_QTUG_PER { get; set; }
            public string TOTAL_QTPU_PER { get; set; }
            public string TOTAL_QTNU_PER { get; set; }
            public string TOTAL_QTTE_PER { get; set; }
            public string x
            {
                get
                {
                    return "TOTAL";
                }
            }
        }
        #endregion
    }
}
