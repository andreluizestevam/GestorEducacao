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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptDemonConsulMedic : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonConsulMedic()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              string Periodo,
                              int coUnid,
                              int coDepto,
                              int coEspec,
                              string Classific,
                              int coOrdenacao,
                              string coTipoOrdem,
                              string dataIni,
                              string dataFim,
                              bool comGraficos,
                              bool comRelatorio,
                              bool VerValor,
                              string NomeRelatorio
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
                if (VerValor == true)
                {
                    tblValor.Visible = true;

                    xrValor.Text = "VALOR";
                }
                else
                {
                    tblValor.Text = "";
                    xrValor.Text = "";
                }
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                if (NomeRelatorio == "")
                {
                    this.lblTitulo.Text = "DEMONSTRATIVO GERENCIAL DE CONSULTAS MÉDICAS" + Periodo;
                }
                else
                {
                    this.lblTitulo.Text = NomeRelatorio + Periodo;
                }

                lblLegenda.Text = "QAP (Quantidade de Atendimento Planejado) | QAR (Quantidade de Atendimento Realizado) | QAC (Quantidade de Atendimento Cancelado) | QAM (Quantidade de Atendimento Movimentado | QCN (Quantidade de Consulta Normal) | QCR (Quantidade de Consulta Retorno) | QCP (Quantidade de Procedimentos) | QCE (Quantidade de Exames) | QCC (Quantidade de Cirurgias) | QCV (Quantidade de Vacinas) | QCO (Quantidade de Outros) | MAD (Média de Atendimento Diário) | MDE (Média de Atendimento por Especialidade) | MAP (Média de Colaboradores por Consulta))";

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

                AlteraNomeColunaMultavel(Classific);

                #endregion

                #region Classificado por UNIDADE

                if (Classific == "U")
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               where (coUnid != 0 ? tbs174.CO_EMP == coUnid : 0 == 0)
                               && (coDepto != 0 ? tbs174.CO_DEPT == coDepto : 0 == 0)
                               && (coEspec != 0 ? tbs174.CO_ESPEC == coEspec : 0 == 0)
                               && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))

                               select new DemonGerenPlantoes
                               {
                                   coEmpLinha = tb25.CO_EMP,
                                   NO_EMP = tb25.NO_FANTAS_EMP,

                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                                   CO_DEPTO = coDepto,
                                   CO_ESPEC = coEspec,
                                   coUnidConsul = coUnid,
                                   coClassPor = Classific,
                                   Valor = tbs174.VL_CONSUL,
                               }).DistinctBy(w => w.coEmpLinha).ToList();

                    //Ordena e classifica de acordo com o escolhido pelo usuário
                    switch (coOrdenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.QAP).ThenBy(w => w.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.QAP).ThenByDescending(w => w.NO_EMP).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAM).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAM).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCN).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCN).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCP).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCV).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCV).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 12:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCO).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCO).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 13:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAD).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAD).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 14:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 15:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAP).ThenByDescending(o => o.NO_EMP).ToList();
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
                    decimal valorTotal = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;
                        valorTotal += at.Valor.HasValue ? at.Valor.Value : 0;
                        at.ValorTotal = valorTotal;

                        //totperQTHT += decimal.Parse(at.QTHT_PER);
                        totperQTDP += decimal.Parse(at.QCN_PER);
                        totperQTIP += decimal.Parse(at.QCU_PER);

                        at.TOTAL_QTDP_PER = decimal.Floor(totperQTDP).ToString();
                        at.TOTAL_QTHT_PER = decimal.Floor(totperQTHT).ToString();
                        at.TOTAL_QTIP_PER = decimal.Floor(totperQTIP).ToString();

                        SetaCoresOrdenacao(coOrdenacao, Classific);

                        if (auxCount == res.Count)
                            AlimentaGraficoTipos(at.TOTAL_QCN, at.TOTAL_QCR, at.TOTAL_QCP, at.TOTAL_QCE, at.TOTAL_QCC, at.TOTAL_QCV, at.TOTAL_QCO);

                        //if (auxCount == res.Count)
                        //    PreparaPieChart(CoUnidPlant, coEspecPlant, coTipoContrato, dataIni1, dataFim1, xrChart2);

                        bsReport.Add(at);
                    }

                    return 1;
                }
                #endregion

                #region Classificado por PROFISSIONAL

                else if (Classific == "P")
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where (coUnid != 0 ? tbs174.CO_EMP == coUnid : 0 == 0)
                               && (coDepto != 0 ? tbs174.CO_DEPT == coDepto : 0 == 0)
                               && (coEspec != 0 ? tbs174.CO_ESPEC == coEspec : 0 == 0)
                               && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))

                               select new DemonGerenPlantoes
                               {
                                   NO_COL = tb03.NO_COL,
                                   CoColLinha = tb03.CO_COL,
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                                   CO_DEPTO = coDepto,
                                   CO_ESPEC = coEspec,
                                   coUnidConsul = coUnid,
                                   coClassPor = Classific,
                                   Valor = tbs174.VL_CONSUL,
                               }).DistinctBy(w => w.CoColLinha).ToList();

                    //Ordena e classifica de acordo com o escolhido pelo usuário
                    switch (coOrdenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.QAP).ThenBy(w => w.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.QAP).ThenByDescending(w => w.NO_EMP).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAM).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAM).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCN).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCN).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCP).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCV).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCV).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 12:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCO).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCO).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 13:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAD).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAD).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 14:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 15:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAP).ThenByDescending(o => o.NO_EMP).ToList();
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
                    decimal valorTotal = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;
                        valorTotal += at.Valor.HasValue ? at.Valor.Value : 0;
                        at.ValorTotal = valorTotal;

                        //totperQTHT += decimal.Parse(at.QTHT_PER);
                        totperQTDP += decimal.Parse(at.QCN_PER);
                        totperQTIP += decimal.Parse(at.QCU_PER);

                        at.TOTAL_QTDP_PER = decimal.Floor(totperQTDP).ToString();
                        at.TOTAL_QTHT_PER = decimal.Floor(totperQTHT).ToString();
                        at.TOTAL_QTIP_PER = decimal.Floor(totperQTIP).ToString();

                        SetaCoresOrdenacao(coOrdenacao, Classific);

                        if (auxCount == res.Count)
                            AlimentaGraficoTipos(at.TOTAL_QCN, at.TOTAL_QCR, at.TOTAL_QCP, at.TOTAL_QCE, at.TOTAL_QCC, at.TOTAL_QCV, at.TOTAL_QCO);

                        //if (auxCount == res.Count)
                        //    PreparaPieChart(CoUnidPlant, coEspecPlant, coTipoContrato, dataIni1, dataFim1, xrChart2);

                        bsReport.Add(at);
                    }

                    return 1;
                }

                #endregion

                #region Classificado por ESPECIALIDADE

                else if (Classific == "E")
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                               where (coUnid != 0 ? tbs174.CO_EMP == coUnid : 0 == 0)
                               && (coDepto != 0 ? tbs174.CO_DEPT == coDepto : 0 == 0)
                               && (coEspec != 0 ? tbs174.CO_ESPEC == coEspec : 0 == 0)
                               && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))

                               select new DemonGerenPlantoes
                               {
                                   coEspecLinha = tb63.CO_ESPECIALIDADE,
                                   NO_ESPEC = tb63.NO_ESPECIALIDADE,
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                                   CO_DEPTO = coDepto,
                                   CO_ESPEC = coEspec,
                                   coUnidConsul = coUnid,
                                   coClassPor = Classific,
                                   Valor = tbs174.VL_CONSUL,
                               }).DistinctBy(w => w.coEspecLinha).ToList();

                    //Ordena e classifica de acordo com o escolhido pelo usuário
                    switch (coOrdenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.QAP).ThenBy(w => w.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.QAP).ThenByDescending(w => w.NO_EMP).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAM).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAM).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCN).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCN).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCP).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCV).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCV).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 12:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCO).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCO).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 13:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAD).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAD).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 14:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 15:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAP).ThenByDescending(o => o.NO_EMP).ToList();
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
                    decimal valorTotal = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;
                        valorTotal += at.Valor.HasValue ? at.Valor.Value : 0;
                        at.ValorTotal = valorTotal;

                        //totperQTHT += decimal.Parse(at.QTHT_PER);
                        totperQTDP += decimal.Parse(at.QCN_PER);
                        totperQTIP += decimal.Parse(at.QCU_PER);

                        at.TOTAL_QTDP_PER = decimal.Floor(totperQTDP).ToString();
                        at.TOTAL_QTHT_PER = decimal.Floor(totperQTHT).ToString();
                        at.TOTAL_QTIP_PER = decimal.Floor(totperQTIP).ToString();

                        SetaCoresOrdenacao(coOrdenacao, Classific);

                        if (auxCount == res.Count)
                            AlimentaGraficoTipos(at.TOTAL_QCN, at.TOTAL_QCR, at.TOTAL_QCP, at.TOTAL_QCE, at.TOTAL_QCC, at.TOTAL_QCV, at.TOTAL_QCO);

                        //if (auxCount == res.Count)
                        //    PreparaPieChart(CoUnidPlant, coEspecPlant, coTipoContrato, dataIni1, dataFim1, xrChart2);

                        bsReport.Add(at);
                    }

                    return 1;
                }

                #endregion

                #region Classificado por CIDADE / BAIRRO

                //else if (Classific == "B")
                else
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_CIDADE equals tb904.CO_CIDADE into lcid
                               from cida in lcid.DefaultIfEmpty()
                               join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into lbai
                               from bair in lbai.DefaultIfEmpty()
                               where (coUnid != 0 ? tbs174.CO_EMP == coUnid : 0 == 0)
                               && (coDepto != 0 ? tbs174.CO_DEPT == coDepto : 0 == 0)
                               && (coEspec != 0 ? tbs174.CO_ESPEC == coEspec : 0 == 0)
                               && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))

                               select new DemonGerenPlantoes
                               {
                                   coBairroLinha = (bair != null ? bair.CO_BAIRRO : 0),
                                   NO_CIDABAIRRO = (cida != null && bair != null ? cida.NO_CIDADE + " / " + bair.NO_BAIRRO : " Outros "),
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                                   CO_DEPTO = coDepto,
                                   CO_ESPEC = coEspec,
                                   coUnidConsul = coUnid,
                                   coClassPor = Classific,
                                   Valor = tbs174.VL_CONSUL,
                               }).DistinctBy(w => w.coBairroLinha).ToList();

                    //Ordena e classifica de acordo com o escolhido pelo usuário
                    switch (coOrdenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.QAP).ThenBy(w => w.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(o => o.QAP).ThenByDescending(w => w.NO_EMP).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAM).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAM).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCN).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCN).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCR).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCR).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCP).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCC).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCC).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCV).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCV).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 12:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QCO).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.QCO).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 13:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAD).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAD).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 14:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDE).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDE).ThenByDescending(o => o.NO_EMP).ToList();
                            break;
                        case 15:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MAP).ThenBy(o => o.NO_EMP).ToList();
                            else
                                res = res.OrderByDescending(w => w.MAP).ThenByDescending(o => o.NO_EMP).ToList();
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
                    decimal valorTotal = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;
                        valorTotal += at.Valor.HasValue ? at.Valor.Value : 0;
                        at.ValorTotal = valorTotal;

                        //totperQTHT += decimal.Parse(at.QTHT_PER);
                        totperQTDP += decimal.Parse(at.QCN_PER);
                        totperQTIP += decimal.Parse(at.QCU_PER);

                        at.TOTAL_QTDP_PER = decimal.Floor(totperQTDP).ToString();
                        at.TOTAL_QTHT_PER = decimal.Floor(totperQTHT).ToString();
                        at.TOTAL_QTIP_PER = decimal.Floor(totperQTIP).ToString();

                        SetaCoresOrdenacao(coOrdenacao, Classific);

                        if (auxCount == res.Count)
                            AlimentaGraficoTipos(at.TOTAL_QCN, at.TOTAL_QCR, at.TOTAL_QCP, at.TOTAL_QCE, at.TOTAL_QCC, at.TOTAL_QCV, at.TOTAL_QCO);

                        //if (auxCount == res.Count)
                        //    PreparaPieChart(CoUnidPlant, coEspecPlant, coTipoContrato, dataIni1, dataFim1, xrChart2);

                        bsReport.Add(at);
                    }

                    return 1;
                }

                #endregion
            }
            catch { return 0; }
        }

        #endregion

        /// <summary>
        /// Seta cor nas colunas referentes ao objeto de uma ordenação escolhida nos parâmetros
        /// </summary>
        /// <param name="Ordenacao"></param>
        protected void SetaCoresOrdenacao(int Ordenacao, string coClassPor)
        {
            //Muda a cor da coluna de acordo com a ordenação escolhida
            switch (Ordenacao)
            {
                case 1:
                    xrTableCell25.ForeColor = Color.RoyalBlue;
                    break;
                case 2:
                    xrTableCell1.ForeColor = xrTableCell56.ForeColor = Color.RoyalBlue;
                    break;
                case 3:
                    xrTableCell16.ForeColor = xrTableCell58.ForeColor = Color.RoyalBlue;
                    break;
                case 4:
                    xrTableCell14.ForeColor = xrTableCell13.ForeColor = xrTableCell61.ForeColor = xrTableCell62.ForeColor = Color.RoyalBlue;
                    break;
                case 5:
                    xrTableCell10.ForeColor = xrTableCell11.ForeColor = xrTableCell64.ForeColor = xrTableCell65.ForeColor = Color.RoyalBlue;
                    break;
                case 6:
                    xrTableCell9.ForeColor = xrTableCell12.ForeColor = xrTableCell67.ForeColor = xrTableCell68.ForeColor = Color.RoyalBlue;
                    break;
                case 7:
                    xrTableCell6.ForeColor = xrTableCell19.ForeColor = xrTableCell70.ForeColor = xrTableCell71.ForeColor = Color.RoyalBlue;
                    break;
                case 8:
                    xrTableCell7.ForeColor = xrTableCell18.ForeColor = xrTableCell73.ForeColor = xrTableCell74.ForeColor = Color.RoyalBlue;
                    break;
                case 9:
                    xrTableCell8.ForeColor = tblValor.ForeColor = xrTableCell76.ForeColor = xrTableCell76.ForeColor = Color.RoyalBlue;
                    break;
            }
        }

        /// <summary>
        /// Altera o Nome da Coluna Multável de acordo com opção escolhida
        /// </summary>
        /// <param name="coClassPor"></param>
        private void AlteraNomeColunaMultavel(string coClassPor)
        {
            switch (coClassPor)
            {
                case "U":
                    this.colMutaveis.Text = "UNIDADE";
                    break;
                case "P":
                    this.colMutaveis.Text = "PROFISSIONAL";
                    break;
                case "E":
                    this.colMutaveis.Text = "ESPECIALIDADE";
                    break;
                case "B":
                    this.colMutaveis.Text = "CIDADE / BAIRRO";
                    break;
            }
        }

        /// <summary>
        /// Alimenta o Segundo Gráfico
        /// </summary>
        /// <param name="QCN"></param>
        /// <param name="QCR"></param>
        /// <param name="QCU"></param>
        private void AlimentaGraficoTipos(int QCN, int QCR, int QCP, int QCE, int QCC, int QCV, int QCO)
        {
            //Alimenta o Segundo Gráfico
            Series series2 = new Series("nova2", ViewType.Doughnut);
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCN", QCN));
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCR", QCR));
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCP", QCP));
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCE", QCE));
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCC", QCC));
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCV", QCV));
            series2.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QCO", QCO));
            series2.LegendPointOptions.PointView = PointView.ArgumentAndValues;
            series2.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series2.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            xrChart2.Series.Add(series2);
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

        public class DemonGerenPlantoes
        {
            //Dados do Colaborador
            public string matCol { get; set; }
            public string noColValid
            {
                get
                {
                    return this.matCol + " - " + this.NO_COL;
                }
            }
            public int NCL { get; set; }
            public string nomeMultavel
            {
                get
                {
                    string s = "";
                    switch (this.coClassPor)
                    {
                        case "U":
                            s = this.NO_EMP.ToUpper();
                            break;

                        case "P":
                            s = this.NO_COL.ToUpper();
                            break;

                        case "E":
                            s = this.NO_ESPEC.ToUpper();
                            break;

                        case "B":
                            s = this.NO_CIDABAIRRO.ToUpper();
                            break;
                    }
                    return s;
                }
            }

            public decimal? Valor { get; set; }
            public decimal? ValorTotal { get; set; }
            public string NO_EMP { get; set; }
            public string NO_COL { get; set; }
            public string NO_ESPEC { get; set; }
            public string NO_CIDABAIRRO { get; set; }

            //Dados de auxlilio para querys
            public int coEmpLinha { get; set; }
            public int CoColLinha { get; set; }
            public int coEspecLinha { get; set; }
            public int coBairroLinha { get; set; }

            public int coUnidConsul { get; set; }
            public int CO_DEPTO { get; set; }
            public int CO_ESPEC { get; set; }
            public DateTime dataIni1 { get; set; }
            public DateTime dataFim1 { get; set; }
            public string coClassPor { get; set; }

            //Quantidade total de Atendimentos Planejados
            public int QAP
            {
                get
                {
                    int qtQAP = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQAP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQAP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQAP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQAP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }

                    return qtQAP;
                }
            }
            public string QAP_V
            {
                get
                {
                    return this.QAP.ToString().PadLeft(1, '0');
                }
            }

            ////Quantidade total de Atendimentos Realizados
            public int QAR
            {
                get
                {
                    int qtQAR = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQAR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQAR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQAR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        case "B":
                            qtQAR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.CO_SITUA_AGEND_HORAR == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQAR;
                }
            }
            public string QAR_V
            {
                get
                {
                    return this.QAR.ToString().PadLeft(1, '0');
                }
            }


            //Porcentagem dos atendimentos realizados em relação aos planejados
            public string QAPQAR_PER
            {
                get
                {
                    if (this.QAP > 0)
                    {
                        decimal aux1 = this.QAR * 100;
                        decimal aux2 = aux1 / this.QAP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Atendimentos Cancelados
            public int QAC
            {
                get
                {
                    int qtQAC = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQAC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQAC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQAC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        case "B":
                            qtQAC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.CO_SITUA_AGEND_HORAR == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQAC;
                }
            }
            public string QAC_V
            {
                get
                {
                    return this.QAC.ToString().PadLeft(1, '0');
                }
            }

            //Porcentagem dos atendimentos CANCELADOS em relação aos planejados
            public string QAPQAC_PER
            {
                get
                {
                    if (this.QAP > 0)
                    {
                        decimal aux1 = this.QAC * 100;
                        decimal aux2 = aux1 / this.QAP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Atendimentos Movimentados
            public int QAM
            {
                get
                {
                    int qtQAM = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQAM = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "M"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQAM = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "M"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQAM = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.CO_SITUA_AGEND_HORAR == "M"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        case "B":
                            qtQAM = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.CO_SITUA_AGEND_HORAR == "M"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQAM;
                }
            }
            public string QAM_V
            {
                get
                {
                    return this.QAC.ToString().PadLeft(1, '0');
                }
            }

            //Porcentagem dos atendimentos CANCELADOS em relação aos planejados
            public string QAPQAM_PER
            {
                get
                {
                    if (this.QAP > 0)
                    {
                        decimal aux1 = this.QAM * 100;
                        decimal aux2 = aux1 / this.QAP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas Normais
            public int QCN
            {
                get
                {
                    int qtQCN = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "N"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "N"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "N"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "N"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }

                    return qtQCN;
                }
            }
            public string QCN_V
            {
                get
                {
                    return this.QCN.ToString().PadLeft(1, '0');
                }
            }
            public string QCN_PER
            {
                get
                {
                    if (this.TOTAL_QCN > 0)
                    {
                        decimal aux1 = this.QCN * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCN;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Retorno
            public int QCR
            {
                get
                {
                    int qtQCR = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "R"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCR;
                }
            }
            public string QCR_V
            {
                get
                {
                    return this.QCR.ToString().PadLeft(1, '0');
                }
            }
            public string QCR_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCR > 0)
                    {
                        decimal aux1 = this.QCR * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCR;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Procedimento
            public int QCP
            {
                get
                {
                    int qtQCP = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "P"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "P"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "P"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "P"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCP;
                }
            }
            public string QCP_V
            {
                get
                {
                    return this.QCP.ToString().PadLeft(1, '0');
                }
            }
            public string QCP_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCP > 0)
                    {
                        decimal aux1 = this.QCP * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCP;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Exame
            public int QCE
            {
                get
                {
                    int qtQCE = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCE = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "E"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCE = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "E"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCE = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "E"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCE = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "E"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCE;
                }
            }
            public string QCE_V
            {
                get
                {
                    return this.QCE.ToString().PadLeft(1, '0');
                }
            }
            public string QCE_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCE > 0)
                    {
                        decimal aux1 = this.QCE * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCE;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Cirurgia
            public int QCC
            {
                get
                {
                    int qtQCC = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "C"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCC;
                }
            }
            public string QCC_V
            {
                get
                {
                    return this.QCC.ToString().PadLeft(1, '0');
                }
            }
            public string QCC_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCC > 0)
                    {
                        decimal aux1 = this.QCC * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCC;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Vacina
            public int QCV
            {
                get
                {
                    int qtQCV = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCV = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "V"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCV = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "V"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCV = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "V"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCV = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "V"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCV;
                }
            }
            public string QCV_V
            {
                get
                {
                    return this.QCV.ToString().PadLeft(1, '0');
                }
            }
            public string QCV_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCV > 0)
                    {
                        decimal aux1 = this.QCV * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCV;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Outros
            public int QCO
            {
                get
                {
                    int qtQCO = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCO = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "O"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCO = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "O"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCO = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "O"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCO = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "O"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCO;
                }
            }
            public string QCO_V
            {
                get
                {
                    return this.QCO.ToString().PadLeft(1, '0');
                }
            }
            public string QCO_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCO > 0)
                    {
                        decimal aux1 = this.QCO * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCO;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Consultas de Urgência
            public int QCU
            {
                get
                {
                    int qtQCN = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_EMP == this.coEmpLinha
                                     && tbs174.TP_CONSU == "U"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_COL == this.CoColLinha
                                     && tbs174.TP_CONSU == "U"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && tbs174.CO_ESPEC == this.coEspecLinha
                                     && tbs174.TP_CONSU == "U"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                     && tbs174.TP_CONSU == "U"
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }
                    return qtQCN;
                }
            }
            public string QCU_V
            {
                get
                {
                    return this.QCU.ToString().PadLeft(1, '0');
                }
            }
            public string QCU_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QCU > 0)
                    {
                        decimal aux1 = this.QCU * 100;
                        decimal aux2 = aux1 / this.TOTAL_QCU;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula a quantidade total de consultas dentro do período selecionado, para usar nas querys à frente
            public int QT_CONSUL_PERIODO
            {
                get
                {
                    TimeSpan ts = this.dataFim1.Subtract(this.dataIni1);
                    int qtDias = ts.Days;
                    int qtConsuls = 0;

                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtConsuls = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                         where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                         && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                         && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                         && tbs174.CO_ALU != null
                                         && tbs174.CO_EMP == this.coEmpLinha
                                         && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                         select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtConsuls = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                         where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                         && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                         && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                         && tbs174.CO_ALU != null
                                         && tbs174.CO_COL == this.CoColLinha
                                         && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                         select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Profissional
                        case "E":
                            qtConsuls = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                         where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                         && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                         && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                         && tbs174.CO_ALU != null
                                         && tbs174.CO_ESPEC == this.coEspecLinha
                                         && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                         select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtConsuls = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                         join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                         where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                         && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                         && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                         && tbs174.CO_ALU != null
                                         && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                         && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                         select new { tbs174.ID_AGEND_HORAR }).Count();
                            break;
                    }

                    return qtConsuls;
                }
            }

            //Calcula a Média de Atendimentos Diária
            public decimal MAD
            {
                get
                {
                    TimeSpan ts = this.dataFim1.Subtract(this.dataIni1);
                    int qtDias = ts.Days;

                    return (qtDias > 0 ? Math.Round((this.QT_CONSUL_PERIODO / (decimal)qtDias), 1) : 0);
                }
            }
            public string MAD_V
            {
                get
                {
                    return this.MAD.ToString("N1");
                }
            }
            public string MAD_PER
            {
                get
                {
                    decimal percent = 0;
                    decimal mhpv = this.MAD;
                    if (mhpv > 0)
                    {
                        //if (this.MediaHoraPlanGeral < mhpv)
                        //    percent = (1 - (this.MediaHoraPlanGeral / mhpv)) * 100;
                        //else
                        percent = ((mhpv / this.MAD_GERAL) - 1) * 100;

                        return (percent == 100 ? decimal.Floor(percent).ToString() : percent.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula a Média de Atendimentos por Especialidades
            public decimal MDE
            {
                get
                {
                    int qtEspec = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtEspec = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && tbs174.CO_EMP == this.coEmpLinha
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_ESPEC }).Distinct().Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtEspec = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && tbs174.CO_COL == this.CoColLinha
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_ESPEC }).Distinct().Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtEspec = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && tbs174.CO_ESPEC == this.coEspecLinha
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_ESPEC }).Distinct().Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtEspec = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_ESPEC }).Distinct().Count();
                            break;
                    }
                    return (qtEspec > 0 ? Math.Round((this.QT_CONSUL_PERIODO / (decimal)qtEspec), 1) : 0);
                }
            }
            public string MDE_V
            {
                get
                {
                    return this.MDE.ToString("N1");
                }
            }
            public string MDE_PER
            {
                get
                {
                    decimal percent = 0;
                    decimal mhpv = this.MDE;
                    if (mhpv > 0)
                    {
                        //if (this.MediaHoraPlanGeral < mhpv)
                        //    percent = (1 - (this.MediaHoraPlanGeral / mhpv)) * 100;
                        //else
                        percent = ((mhpv / this.MDE_GERAL) - 1) * 100;

                        return (percent == 100 ? decimal.Floor(percent).ToString() : percent.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula a média de colaboradores por consulta
            public decimal MAP
            {
                get
                {
                    int qtProfi = 0;
                    switch (this.coClassPor)
                    {
                        //Por Unidade
                        case "U":
                            qtProfi = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && tbs174.CO_EMP == this.coEmpLinha
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_COL }).Distinct().Count();
                            break;

                        //Por Profissional
                        case "P":
                            qtProfi = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && tbs174.CO_COL == this.CoColLinha
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_COL }).Distinct().Count();
                            break;

                        //Por Especialidade
                        case "E":
                            qtProfi = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && tbs174.CO_ESPEC == this.coEspecLinha
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_COL }).Distinct().Count();
                            break;

                        //Por Cidade/Bairro
                        case "B":
                            qtProfi = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                       where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                       && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                       && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                       && tbs174.CO_ALU != null
                                       && (this.coBairroLinha != 0 ? tb07.TB905_BAIRRO.CO_BAIRRO == this.coBairroLinha : tb07.TB905_BAIRRO == null)
                                       && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                       select new { tbs174.CO_COL }).Distinct().Count();
                            break;
                    }
                    return (qtProfi > 0 ? Math.Round((this.QT_CONSUL_PERIODO / (decimal)qtProfi), 1) : 0);
                }
            }
            public string MAP_V
            {
                get
                {
                    return this.MAP.ToString("N1");
                }
            }
            public string MAP_PER
            {
                get
                {
                    decimal percent = 0;
                    decimal mhpv = this.MAP;
                    if (mhpv > 0)
                    {
                        //if (this.MediaHoraPlanGeral < mhpv)
                        //    percent = (1 - (this.MediaHoraPlanGeral / mhpv)) * 100;
                        //else
                        percent = ((mhpv / this.MAP_GERAL) - 1) * 100;

                        return (percent == 100 ? decimal.Floor(percent).ToString() : percent.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            //Calcula os totais

            public int QAP_TOTAL
            {
                get
                {
                    int qtQAP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQAP;
                }
            }

            public int QAR_TOTAL
            {
                get
                {
                    int qtQAR = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.CO_SITUA_AGEND_HORAR == "R"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();
                    return qtQAR;
                }
            }

            public int QAC_TOTAL
            {
                get
                {
                    int qtQAC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.CO_SITUA_AGEND_HORAR == "C"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();
                    return qtQAC;
                }
            }

            public int QAM_TOTAL
            {
                get
                {
                    int qtQAM = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.CO_SITUA_AGEND_HORAR == "M"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();
                    return qtQAM;
                }
            }

            public int TOTAL_QCN
            {
                get
                {
                    int qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "N"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCN;
                }
            }
            public string TOTAL_QCN_V
            {
                get
                {
                    return this.TOTAL_QCN.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCR
            {
                get
                {
                    int qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "R"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCN;
                }
            }
            public string TOTAL_QCR_V
            {
                get
                {
                    return this.TOTAL_QCR.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCP
            {
                get
                {
                    int qtQCP = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "P"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCP;
                }
            }
            public string TOTAL_QCP_V
            {
                get
                {
                    return this.TOTAL_QCP.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCE
            {
                get
                {
                    int qtQCE = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "E"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCE;
                }
            }
            public string TOTAL_QCE_V
            {
                get
                {
                    return this.TOTAL_QCE.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCC
            {
                get
                {
                    int qtQCC = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "C"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCC;
                }
            }
            public string TOTAL_QCC_V
            {
                get
                {
                    return this.TOTAL_QCC.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCV
            {
                get
                {
                    int qtQCV = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "V"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCV;
                }
            }
            public string TOTAL_QCV_V
            {
                get
                {
                    return this.TOTAL_QCV.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCO
            {
                get
                {
                    int qtQCO = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "O"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCO;
                }
            }
            public string TOTAL_QCO_V
            {
                get
                {
                    return this.TOTAL_QCO.ToString().PadLeft(1, '0');
                }
            }

            public int TOTAL_QCU
            {
                get
                {
                    int qtQCN = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                                 where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                 && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                 && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                 && tbs174.CO_ALU != null
                                 && tbs174.TP_CONSU == "U"
                                 && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                 select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtQCN;
                }
            }
            public string TOTAL_QCU_V
            {
                get
                {
                    return this.TOTAL_QCU.ToString().PadLeft(1, '0');
                }
            }

            /// <summary>
            /// Calcula a quantidade total de consultas dentro do período selecionado NO GERAL, para usar nas querys à frente
            /// </summary>
            public int QT_CONSUL_PERIODO_GERAL
            {
                get
                {
                    TimeSpan ts = this.dataFim1.Subtract(this.dataIni1);
                    int qtDias = ts.Days;

                    int qtConsuls = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                     && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                     && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                     && tbs174.CO_ALU != null
                                     && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                     select new { tbs174.ID_AGEND_HORAR }).Count();

                    return qtConsuls;
                }
            }

            /// <summary>
            /// Calcula a Média de Atendimento Diária GERAL dentro do período
            /// </summary>
            public decimal MAD_GERAL
            {
                get
                {
                    TimeSpan ts = this.dataFim1.Subtract(this.dataIni1);
                    int qtDias = ts.Days;

                    return this.QT_CONSUL_PERIODO_GERAL / (decimal)qtDias;
                }
            }
            public string MAD_GERAL_V
            {
                get
                {
                    return this.MAD_GERAL.ToString("N1");
                }
            }

            /// <summary>
            /// Calcula a média geral MDE
            /// </summary>
            public decimal MDE_GERAL
            {
                get
                {
                    int qtEspec = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                   where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                   && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                   && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                   && tbs174.CO_ALU != null
                                   && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                   select new { tbs174.CO_ESPEC }).Distinct().Count();

                    return this.QT_CONSUL_PERIODO_GERAL / (decimal)qtEspec;
                }
            }
            public string MDE_GERAL_V
            {
                get
                {
                    return this.MDE_GERAL.ToString("N1");
                }
            }

            /// <summary>
            /// Calcula a média geral MAP
            /// </summary>
            public decimal MAP_GERAL
            {
                get
                {
                    int qtProfi = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                   where (this.coUnidConsul != 0 ? tbs174.CO_EMP == this.coUnidConsul : 0 == 0)
                                   && (CO_DEPTO != 0 ? tbs174.CO_DEPT == CO_DEPTO : 0 == 0)
                                   && (CO_ESPEC != 0 ? tbs174.CO_ESPEC == CO_ESPEC : 0 == 0)
                                   && tbs174.CO_ALU != null
                                   && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                   select new { tbs174.CO_COL }).Distinct().Count();

                    return this.QT_CONSUL_PERIODO_GERAL / (decimal)qtProfi;
                }
            }
            public string MAP_GERAL_V
            {
                get
                {
                    return this.MAP_GERAL.ToString("N1");
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

