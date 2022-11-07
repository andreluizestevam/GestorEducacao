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

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7130_CtrlPlantoes
{
    public partial class RptDemonGerencPlantoes : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonGerencPlantoes()
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
                              int coTipoContrato,
                              string coTipoOrdem,
                              bool comGraficos,
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
                this.lblTitulo.Text = "DEMONSTRATIVO GERENCIAL DE PLANTÕES DE SAÚDE" + Periodo;

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
                               && (coTipoContrato != 0 ? tb03.CO_TPCON == coTipoContrato : 0 == 0)
                               && tb03.FL_PERM_PLANT == "S"
                               && (situacaoFuncional != "0" ? tb03.CO_SITU_COL == situacaoFuncional : 0 == 0)
                               select new DemonGerenPlantoes
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
                                   coTipoContrato = coTipoContrato,

                               }).DistinctBy(w => w.CoCol).ToList();

                    //Ordena e classifica de acordo com o escolhido pelo usuário
                    switch (Ordenacao)
                    {
                        case 1:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.noCol).ToList();
                            break;
                        case 2:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(o => o.funCol).ThenBy(w => w.VTP).ThenBy(b => b.unidCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.funCol).ThenByDescending(w => w.VTP).ThenByDescending(b => b.unidCol).ToList();
                            break;
                        case 3:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(w => w.unidCol).ThenBy(o => o.VTP).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.unidCol).ThenByDescending(o => o.VTP).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 4:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTUP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTUP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 5:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTHT).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTHT).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 6:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTDP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTDP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 7:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTEP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTEP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 8:
                            if(coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTIP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTIP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MHP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.MHP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VTP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.VTP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int auxCount = 0;
                    decimal totperQTHT = 0;
                    decimal totperQTDP = 0;
                    decimal totperQTIP = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;

                        totperQTHT += decimal.Parse(at.QTHT_PER);
                        totperQTDP += decimal.Parse(at.QTDP_PER);
                        totperQTIP += decimal.Parse(at.QTIP_PER);

                        at.TOTAL_QTDP_PER = decimal.Floor(totperQTDP).ToString();
                        at.TOTAL_QTHT_PER = decimal.Floor(totperQTHT).ToString();
                        at.TOTAL_QTIP_PER = decimal.Floor(totperQTIP).ToString();

                        if (auxCount == res.Count)
                            PreparaPieChart(CoUnidPlant, coEspecPlant, coTipoContrato, dataIni1, dataFim1, xrChart2);

                        bsReport.Add(at);
                    }

                    return 1;
                }
                else
                {
                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               join tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros() on tb03.CO_COL equals tb159.TB03_COLABOR.CO_COL into l1
                               from lag in l1.DefaultIfEmpty()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lag.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
                               join tb25Col in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25Col.CO_EMP
                               where (CoUnidFunc != 0 ? tb03.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                               && (coTipoContrato != 0 ? tb03.CO_TPCON == coTipoContrato : 0 == 0)
                               && tb03.FL_PERM_PLANT == "S"
                               && (CoUnidPlant != 0 ? lag.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                               && (coEspecPlant != 0 ? lag.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                               && (situacaoFuncional != "0" ? tb03.CO_SITU_COL == situacaoFuncional : 0 == 0)
                               && (coDept != 0 ? lag.TB14_DEPTO.CO_DEPTO == coDept : 0 == 0)
                               && ((lag.DT_INICIO_PREV >= dataIni1) && (lag.DT_INICIO_PREV <= dataFim1))
                               //&& ((lag.DT_INICIO_PREV.Day >= dataIni1.Day) && (lag.DT_INICIO_PREV.Month >= dataIni1.Month) && (lag.DT_INICIO_PREV.Year >= dataIni1.Year))
                               //&& ((lag.DT_INICIO_PREV.Day <= dataFim1.Day) && (lag.DT_INICIO_PREV.Month <= dataFim1.Month) && (lag.DT_INICIO_PREV.Year <= dataFim1.Year))

                               select new DemonGerenPlantoes
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
                                   coTipoContrato = coTipoContrato,

                                   unidadePlan = tb25.NO_FANTAS_EMP,

                               }).DistinctBy(w => w.CoCol).ToList();

                    //Ordena e classifica de acordo com o escolhido pelo usuário
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
                                res = res.OrderBy(o => o.funCol).ThenBy(w => w.VTP).ThenBy(b => b.unidCol).ToList();
                            else
                                res = res.OrderByDescending(o => o.funCol).ThenByDescending(w => w.VTP).ThenByDescending(b => b.unidCol).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.unidCol).ThenBy(o => o.VTP).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.unidCol).ThenByDescending(o => o.VTP).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTUP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTUP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTHT).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTHT).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTDP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTDP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTEP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTEP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QTIP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.QTIP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MHP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.MHP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VTP).ThenBy(o => o.unidadePlan).ThenBy(o => o.noCol).ToList();
                            else
                                res = res.OrderByDescending(w => w.VTP).ThenByDescending(o => o.unidadePlan).ThenByDescending(o => o.noCol).ToList();
                            break;
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int auxCount = 0;
                    decimal totperQTHT = 0;
                    decimal totperQTDP = 0;
                    decimal totperQTIP = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;

                        totperQTHT += decimal.Parse(at.QTHT_PER);
                        totperQTDP += decimal.Parse(at.QTDP_PER);
                        totperQTIP += decimal.Parse(at.QTIP_PER);

                        at.TOTAL_QTDP_PER = decimal.Floor(totperQTDP).ToString();
                        at.TOTAL_QTHT_PER = decimal.Floor(totperQTHT).ToString();
                        at.TOTAL_QTIP_PER = decimal.Floor(totperQTIP).ToString();

                        SetaCoresOrdenacao(Ordenacao);

                        if(auxCount == res.Count)
                            PreparaPieChart(CoUnidPlant, coEspecPlant, coTipoContrato, dataIni1, dataFim1, xrChart2);

                        bsReport.Add(at);
                    }

                    return 1;
                }
            }
            catch { return 0; }
        }

        #endregion

        /// <summary>
        /// Seta cor nas colunas referentes ao objeto de uma ordenação escolhida nos parâmetros
        /// </summary>
        /// <param name="Ordenacao"></param>
        protected void SetaCoresOrdenacao(int Ordenacao)
        {
            //Muda a cor da coluna de acordo com a ordenação escolhida
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
                    xrTableCell28.ForeColor = xrTableCell32.ForeColor = xrTableCell57.ForeColor = xrTableCell56.ForeColor = Color.RoyalBlue;
                    break;
                case 6:
                    xrTableCell30.ForeColor = xrTableCell33.ForeColor = xrTableCell55.ForeColor = xrTableCell54.ForeColor = Color.RoyalBlue;
                    break;
                case 7:
                    xrTableCell29.ForeColor = xrTableCell31.ForeColor = xrTableCell53.ForeColor = Color.RoyalBlue;
                    break;
                case 8:
                    xrTableCell35.ForeColor = xrTableCell34.ForeColor = xrTableCell50.ForeColor = xrTableCell49.ForeColor = Color.RoyalBlue;
                    break;
                case 9:
                    xrTableCell37.ForeColor = xrTableCell38.ForeColor = xrTableCell47.ForeColor = Color.RoyalBlue;
                    break;
                case 10:
                    xrTableCell39.ForeColor = xrTableCell40.ForeColor = xrTableCell45.ForeColor = xrTableCell44.ForeColor = Color.RoyalBlue;
                    break;
            }
        }

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

        /// <summary>
        /// Calcula as Inconsistências 
        /// </summary>
        public static void PreparaPieChart(int CoUnidPlant, int coEspecPlant, int coTipoContrato, DateTime dataIni1, DateTime dataFim1, XRChart xr)
        {
            var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb159.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
                       where (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                          && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                          && (coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == coTipoContrato : 0 == 0)
                          && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                       select new pieChartClass 
                       { 
                           NO_EMP = tb25.sigla,
                           CO_EMP = tb25.CO_EMP,
                           CoUnidPlant = CoUnidPlant,
                           coEspecPlant = coEspecPlant,
                           dataIni1 = dataIni1,
                           dataFim1 = dataFim1,
                           coTipoContrato = coTipoContrato,
                       }).Distinct().OrderBy(w => w.NO_EMP).ToList();

            Series series1 = new Series("nova", ViewType.Pie);
            //Adiciona os devidos argumentos
            foreach (pieChartClass pi in res)
            {
                series1.Points.Add(new DevExpress.XtraCharts.SeriesPoint(pi.NO_EMP, pi.QTIP));
            }
            series1.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series1.LegendPointOptions.PointView = PointView.ArgumentAndValues;
            xr.Series.Add(series1);

        }

        public class pieChartClass
        {
            //Dados de auxlilio para querys
            public int CoUnidPlant { get; set; }
            public int coEspecPlant { get; set; }
            public DateTime dataIni1 { get; set; }
            public DateTime dataFim1 { get; set; }
            public int coTipoContrato { get; set; }

            public string NO_EMP { get; set; }
            public int CO_EMP { get; set; }
            public int QTIP
            {
                get
                {
                    int qtRI = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                   && (this.coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == this.coTipoContrato : 0 == 0)
                                   && tb159.FL_INCON_AGEND == "S"
                                   && tb159.CO_EMP_AGEND_PLANT == this.CO_EMP
                                   && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                select new { tb159.CO_AGEND_PLANT_COLAB }).Distinct().Count();
                    
                    return qtRI;
                }
            }
        }
        public class DemonGerenPlantoes
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
            public string unidadePlan { get; set; }
            public int NCL { get; set; }

            //Dados de auxlilio para querys
            public int CoUnidPlant { get; set; }
            public int coEspecPlant { get; set; }
            public DateTime dataIni1 { get; set; }
            public DateTime dataFim1 { get; set; }
            public int instituicao { get; set; }
            public int coEmp { get; set; }
            public int coTipoContrato { get; set; }

            //Quantidade total de Unidades e percentual
            public int QTUP
            {
                get
                {
                    int qtUnid = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                     && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                     && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                     //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                     //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                  select new { tb159.CO_EMP_AGEND_PLANT }).Distinct().Count();

                    return qtUnid;
                }
            }
            public string QTUP_V
            {
                get
                {
                    return this.QTUP.ToString().PadLeft(2, '0');
                }
            }
            public string QTUP_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTUP > 0)
                    {
                        decimal aux1 = this.QTUP * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTUP;
                        //return aux2.ToString("N1");
                        if (aux2 >= 100)
                            return decimal.Floor(aux2).ToString();
                        else
                            return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de horas e percentual
            public int QTHT
            {
                get
                {
                    var qtHoras = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                   join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                                  where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                     && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                     && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                     //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                     //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                  select new { tb153.QT_HORAS }).ToList();

                    int horas = 0;
                    //Loop para calcular a soma total de horas de plantão
                    foreach (var li in qtHoras)
                    {
                        horas += li.QT_HORAS;
                    }

                    return horas;
                }
            }
            public string QTHT_V
            {
                get
                {
                    return this.QTHT.ToString().PadLeft(2, '0');
                }
            }
            public string QTHT_PER
            {
                get
                {
                    int horas = QTHT;

                    //Calcula a quantidade total de dias dentro do período em contexto
                    //TimeSpan ts = dataFim1.Subtract(dataIni1);
                    //int qtHorasTotal = int.Parse(ts.TotalHours.ToString());

                    if (this.TOTAL_QTHT > 0)
                    {
                        //Calcula o percentual
                        decimal aux1 = horas * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTHT;
                        if(aux2 >= 100)
                            return decimal.Floor(aux2).ToString();
                        else
                            return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de dias e percentual
            public int QTDP
            {
                get
                {
                    int qtDias = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                     && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                     && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                     //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                     //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                  select new { tb159.CO_AGEND_PLANT_COLAB }).Distinct().Count();

                    //Calcula a quantidade total de dias dentro do período em contexto
                    //TimeSpan ts = dataFim1.Subtract(dataIni1);
                    //int qtDiasTotal = int.Parse(ts.TotalDays.ToString());

                    return qtDias;
                }
            }
            public string QTDP_V
            {
                get
                {
                    return this.QTDP.ToString().PadLeft(2, '0');
                }
            }
            public string QTDP_PER
            {
                get
                {
                    //Calcula o percentual de dias trabalhados dentro do período
                    if (this.TOTAL_QTDP > 0)
                    {
                        decimal aux1 = this.QTDP * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTDP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de especialidades e percentual
            public int QTEP
            {
                get
                {
                    int qtEspec = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                   where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                      && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                      && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                      && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                      //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                      //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                   select new { tb159.CO_ESPEC_PLANT }).Distinct().Count();

                    return qtEspec;
                }
            }
            public string QTEP_V
            {
                get
                {
                    return this.QTEP.ToString().PadLeft(2, '0');
                }
            }
            public string QTEP_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTEP > 0)
                    {
                        decimal aux1 = this.QTEP * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTEP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Inconsistências encontratas e percentual
            public int QTIP
            {
                get
                {
                    int qtRI = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                   && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                   && tb159.FL_INCON_AGEND == "S"
                                   && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                   //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                   //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                select new { tb159.CO_AGEND_PLANT_COLAB }).Distinct().Count();

                    return qtRI;
                }
            }
            public string QTIP_V
            {
                get
                {
                    return this.QTIP.ToString().PadLeft(2, '0');
                }
            }
            public string QTIP_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QTIP > 0)
                    {
                        decimal aux1 = this.QTIP * 100;
                        decimal aux2 = aux1 / this.TOTAL_QTIP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula a Média do valor/hora do colaborador em questão
            public decimal MHP
            {
                get
                {
                    var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                                where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                   && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                   && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                   //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                   //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                select new { tb159.VL_HORA_PLANT_COLAB, tb153.QT_HORAS }).ToList();

                    //Percorre a lista contabilizando todos os valores referentes à plantões do colaborador em contexto
                    decimal valMed = 0;
                    foreach (var li in res)
                    {
                        if(li.VL_HORA_PLANT_COLAB.HasValue)
                            valMed += li.VL_HORA_PLANT_COLAB.Value;
                    }

                    if (res.Count != 0)
                    {
                        decimal tot = valMed / res.Count;
                        return tot;
                    }
                    else
                        return 0;
                }
            }
            public string MHP_V
            {
                get
                {
                    return this.MHP.ToString("N2");
                }
            }
            public string MHP_PER
            {
                get
                {
                    decimal percent = 0;
                    decimal mhpv = this.MHP;
                    if (mhpv > 0)
                    {
                        //if (this.MediaHoraPlanGeral < mhpv)
                        //    percent = (1 - (this.MediaHoraPlanGeral / mhpv)) * 100;
                        //else
                            percent = ((mhpv / this.MediaHoraPlanGeral) - 1) * 100;

                        return (percent == 100 ? decimal.Floor(percent).ToString() : percent.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula o valor total investido em plantões no colaborador em questão
            public decimal VTP
            {
                get
                {
                    var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                               join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                               where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                  && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                  && tb159.TB03_COLABOR.CO_COL == this.CoCol
                                  && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                  //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                  //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                               select new { tb159.VL_HORA_PLANT_COLAB, tb153.QT_HORAS, tb159.TB03_COLABOR.CO_COL }).ToList();

                    //Percorre a lista contabilizando todos os valores referentes à plantões do colaborador em contexto e em geral, 
                    decimal valMed = 0;
                    foreach (var li in res)
                    {
                        //if(li.CO_COL == this.CoCol)
                        if(li.VL_HORA_PLANT_COLAB.HasValue)
                            valMed += li.QT_HORAS * li.VL_HORA_PLANT_COLAB.Value;
                    }

                    return valMed;
                }
            }
            public string VTP_V
            {
                get
                {
                    return this.VTP.ToString("N2");
                }
            }
            public string VTP_PER
            {
                get
                {
                    var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                               join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                               where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                  && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                  && ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                  && ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                               select new { tb159.VL_HORA_PLANT_COLAB, tb153.QT_HORAS, tb159.TB03_COLABOR.CO_COL }).ToList();

                    //Percorre a lista contabilizando todos os valores referentes à plantões do colaborador em contexto e em geral, 
                    decimal valGeral = 0;

                    foreach (var li in res)
                    {
                        if(li.VL_HORA_PLANT_COLAB.HasValue)
                            valGeral += li.QT_HORAS * li.VL_HORA_PLANT_COLAB.Value;
                    }

                    //Calcula o percentual e set no atributo correspondente
                    if (valGeral > 0)
                    {
                        decimal vtp = this.VTP;
                        decimal aux1 = vtp * 100;
                        decimal aux2 = aux1 / valGeral;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula informações adicionais
            public decimal MediaHoraPlanGeral
            {
                get
                {
                    var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                               join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                               where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                  && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                  && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                  //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                  //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                               select new { tb159.VL_HORA_PLANT_COLAB, tb153.QT_HORAS, tb159.TB03_COLABOR.CO_COL }).ToList();

                    //Percorre a lista contabilizando todos os valores referentes à plantões do colaborador em contexto
                    decimal valMedGeral = 0;
                    foreach (var li in res)
                    {
                        if(li.VL_HORA_PLANT_COLAB.HasValue)
                            valMedGeral += li.VL_HORA_PLANT_COLAB.Value;
                    }

                    if (res.Count > 0)
                        return valMedGeral / res.Count;
                    else
                        return 0;
                }
            }
            public string MediaHoraPlanGeral_VALID
            {
                get
                {
                    return this.MediaHoraPlanGeral.ToString("N2");
                }
            }

            //Calcula os totais
            public decimal TotalPlantao
            {
                get
                {
                    var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                               join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                               where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                  && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                  && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                  //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                  //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                               select new { tb159.VL_HORA_PLANT_COLAB, tb153.QT_HORAS, tb159.TB03_COLABOR.CO_COL }).ToList();

                    //Percorre a lista contabilizando todos os valores referentes à plantões em contexto geral, 
                    decimal valMed = 0;
                    foreach (var li in res)
                    {
                        if(li.VL_HORA_PLANT_COLAB.HasValue)
                            valMed += li.QT_HORAS * li.VL_HORA_PLANT_COLAB.Value;
                    }

                    return valMed;
                }
            }
            public string TotalPlantao_VALID
            {
                get
                {
                    return this.TotalPlantao.ToString("N2");
                }
            }

            public int TOTAL_QTUP
            {
                get
                {
                    int qtUnid = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                     && (this.coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == this.coTipoContrato : 0 == 0)
                                     && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                     //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                     //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                  select new { tb159.CO_EMP_AGEND_PLANT }).Distinct().Count();

                    return qtUnid;
                }
            }
            public string TOTAL_QTUP_V
            {
                get
                {
                    return this.TOTAL_QTUP.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QTHT
            {
                get
                {
                    int qtHoras = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                   join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                                   where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                      && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                      && (this.coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == this.coTipoContrato : 0 == 0)
                                      && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                      //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                      //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                   select new { tb153.QT_HORAS }).Sum(W=>W.QT_HORAS);

                    return qtHoras;
                }
            }
            public string TOTAL_QTHT_V
            {
                get
                {
                    return this.TOTAL_QTHT.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QTEP
            {
                get
                {
                    int qtEspec = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                   where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                      && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                      && (this.coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == this.coTipoContrato : 0 == 0)
                                      && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                      //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                      //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                   select new { tb159.CO_ESPEC_PLANT }).Distinct().Count();

                    return qtEspec;
                }
            }
            public string TOTAL_QTEP_V
            {
                get
                {
                    return this.TOTAL_QTEP.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QTIP
            {
                get
                {
                    int qtRI = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                   && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                   && (this.coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == this.coTipoContrato : 0 == 0)
                                   && tb159.FL_INCON_AGEND == "S"
                                   && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                   //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                   //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                select new { tb159.CO_AGEND_PLANT_COLAB }).Distinct().Count();

                    return qtRI;
                }
            }
            public string TOTAL_QTIP_V
            {
                get
                {
                    return this.TOTAL_QTIP.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QTDP
            {
                get
                {
                    int qtDias = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                  where (this.CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == this.CoUnidPlant : 0 == 0)
                                     && (this.coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == this.coEspecPlant : 0 == 0)
                                     && (this.coTipoContrato != 0 ? tb159.TB03_COLABOR.CO_TPCON == this.coTipoContrato : 0 == 0)
                                     && ((tb159.DT_INICIO_PREV >= dataIni1) && (tb159.DT_INICIO_PREV <= dataFim1))
                                     //&& ((tb159.DT_INICIO_PREV.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                     //&& ((tb159.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                  select new { tb159.CO_AGEND_PLANT_COLAB }).Distinct().Count();

                    return qtDias;
                }
            }
            public string TOTAL_QTDP_V
            {
                get
                {
                    return this.TOTAL_QTDP.ToString().PadLeft(2, '0');
                }
            }

            public string TOTAL_QTHT_PER { get; set; }
            public string TOTAL_QTDP_PER { get; set; }
            public string TOTAL_QTIP_PER { get; set; }

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

