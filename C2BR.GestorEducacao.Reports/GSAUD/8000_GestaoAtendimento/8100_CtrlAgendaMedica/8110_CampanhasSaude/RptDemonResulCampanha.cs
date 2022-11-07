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
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8110_CampanhasSaude
{
    public partial class RptDemonResulCampanha : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonResulCampanha()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              string coClassPor,
                              string coTipo,
                              string dataIni,
                              string dataFim,
                              bool comValor,
                              bool comGraficos,
                              int ordenacao,
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

                //Mostra a "Band" com o Gráfico apenas caso isso tenha sido solicitado na página de parâmetros
                ReportHeader.Visible = comGraficos;
                GroupHeader1.Visible = DetailContent.Visible = ReportFooter.Visible = comRelatorio;

                //Retorna mensagem padrão de sem dados, caso não tenha sido escolhido Com gráficos nem Com Relatório
                if (comGraficos == false && comRelatorio == false)
                    return -1;

                // Cria o header a partir do cod da instituicao
                var header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                //Altera o Select de acordo com escolhido pelo usuário
                //=================================================================FILTRA CASO TENHA SIDO ESCOLHIDO POR UNIDADE============

                #region Classificado por UNIDADE
                if (coClassPor == "U")
                {
                    var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                               join tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros() on tbs339.ID_CAMPAN equals tbs341.TBS339_CAMPSAUDE.ID_CAMPAN into l1
                               from lcamp in l1.DefaultIfEmpty()

                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lcamp.CO_EMP_ATEND equals tb25.CO_EMP into lep
                               from lEmp in lep.DefaultIfEmpty()
                               where (coTipo != "0" ? tbs339.CO_TIPO_CAMPAN == coTipo : 0 == 0)
                               && ((tbs339.DT_INICI_CAMPAN >= dataIni1) && (tbs339.DT_INICI_CAMPAN <= dataFim1))
                               select new EquipeCampanha
                               {
                                   ID_CAMPAN = tbs339.ID_CAMPAN,
                                   NO_CAMPAN = tbs339.NM_CAMPAN,
                                   CO_TIPO = tbs339.CO_TIPO_CAMPAN,
                                   DataInicio = tbs339.DT_INICI_CAMPAN,
                                   DataFinal = tbs339.DT_TERMI_CAMPAN,
                                   NomeMultavel = (lEmp != null ? lEmp.sigla : " Outro(a) "),
                                   CO_EMP = (lEmp != null ? lEmp.CO_EMP : 0),
                                   comValores = comValor,
                                   coClassPor = coClassPor
                               }).Distinct().ToList();

                    //Ordena e classifica de acordo com os parâmetros escolhidos pelo usuário
                    #region Ordenações e Classificações

                    switch (ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_CAMPAN).ThenBy(w => w.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_CAMPAN).ThenByDescending(w => w.NomeMultavel).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.tipo_Valid).ThenBy(w => w.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(o => o.tipo_Valid).ThenByDescending(w => w.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.NomeMultavel).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList().ToList();
                            else
                                res = res.OrderByDescending(w => w.NomeMultavel).ThenByDescending(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList().ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QDC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QDC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QPC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QPC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_RECEITA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_RECEITA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_DESPESA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_DESPESA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_SALDO).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_SALDO).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.CMA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.CMA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                    }
                    #endregion

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int count = 0;
                    foreach (EquipeCampanha at in res)
                    {
                        count++;

                        #region Calcula total despesas

                        var resplafin = (from tbs346 in TBS346_PLANO_FINAN_CAMPAN.RetornaTodosRegistros()
                                         where tbs346.TBS339_CAMPSAUDE.ID_CAMPAN == at.ID_CAMPAN
                                         && tbs346.TP_LANCA == "D"
                                         select new { tbs346.VL_LANC }).ToList();

                        decimal desp = 0;
                        foreach (var li in resplafin)
                        {
                            desp += li.VL_LANC;
                        }

                        at.TOTAL_OUTRAS_DESPESAS = desp;

                        #endregion

                        //Faz o somatório para coletar os totais dos itens
                        #region

                        int totqac = 0;
                        int totqpc = 0;
                        int totqdc = 0;
                        decimal totVLD = 0;
                        decimal totVLG = 0;
                        decimal totSalddes = 0;
                        foreach (EquipeCampanha li in res)
                        {
                            totqac += li.QAC;
                            totqpc += li.QPC;
                            totqdc += li.QDC;
                            totVLD += li.VL_DIARIO;
                            totVLG += li.VL_FINAL;
                            totSalddes += li.TOTAL_OUTRAS_DESPESAS;
                        }
                        at.TOTAL_QAC = totqac;
                        at.TOTAL_QDC = totqdc;
                        at.TOTAL_QPC = totqpc;
                        at.TOTAL_VL_DIARIO = totVLD;
                        at.TOTAL_VL_FINAL = totVLG;

                        #endregion

                        if (count == res.Count)
                            MontaOGrafico(totVLD, totVLG, totSalddes);

                        at.NCL = count;
                        controlaCoresReport(ordenacao);

                        //Calcula o Total CMA 
                        #region total CMA

                        int countCMA = 0;
                        decimal auxCMA = 0;
                        foreach (EquipeCampanha li in res)
                        {
                            countCMA++;
                            auxCMA++;
                        }
                        if (countCMA > 0)
                            at.TOTAL_CMA = auxCMA / countCMA;
                        else
                            at.TOTAL_CMA = 0;

                        #endregion

                        //Muda o Nome da coluna de acordo com a classificaçãoText escolhida no parâmetro
                        switch (coClassPor)
                        {
                            case "C":
                                xrTableCell19.Text = "";
                                break;
                            case "U":
                                xrTableCell19.Text = "UNIDADE";
                                break;
                            case "B":
                                xrTableCell19.Text = "CIDADE/BAIRRO";
                                break;
                        }

                        bsReport.Add(at);
                    }

                    return 1;
                }
                #endregion

                //=================================================================FILTRA CASO TENHA SIDO ESCOLHIDO POR CAMPANHA============
                #region Classificado pro CAMPANHA

                else if (coClassPor == "C")
                {
                    var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                               where (coTipo != "0" ? tbs339.CO_TIPO_CAMPAN == coTipo : 0 == 0)
                               && ((tbs339.DT_INICI_CAMPAN >= dataIni1) && (tbs339.DT_INICI_CAMPAN <= dataFim1))
                               select new EquipeCampanha
                               {
                                   ID_CAMPAN = tbs339.ID_CAMPAN,
                                   NO_CAMPAN = tbs339.NM_CAMPAN,
                                   CO_TIPO = tbs339.CO_TIPO_CAMPAN,
                                   DataInicio = tbs339.DT_INICI_CAMPAN,
                                   DataFinal = tbs339.DT_TERMI_CAMPAN,
                                   NomeMultavel = "",
                                   comValores = comValor,
                                   coClassPor = coClassPor
                               }).Distinct().ToList();

                    //Ordena e classifica de acordo com os parâmetros escolhidos pelo usuário
                    #region Ordenações e Classificações

                    switch (ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_CAMPAN).ThenBy(w => w.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_CAMPAN).ThenByDescending(w => w.NomeMultavel).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.tipo_Valid).ThenBy(w => w.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(o => o.tipo_Valid).ThenByDescending(w => w.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.NomeMultavel).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList().ToList();
                            else
                                res = res.OrderByDescending(w => w.NomeMultavel).ThenByDescending(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList().ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QDC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QDC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QPC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QPC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_RECEITA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_RECEITA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_DESPESA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_DESPESA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_SALDO).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_SALDO).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.CMA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.CMA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                    }
                    #endregion

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int count = 0;
                    foreach (EquipeCampanha at in res)
                    {
                        count++;

                        #region Calcula total despesas

                        var resplafin = (from tbs346 in TBS346_PLANO_FINAN_CAMPAN.RetornaTodosRegistros()
                                         where tbs346.TBS339_CAMPSAUDE.ID_CAMPAN == at.ID_CAMPAN
                                         && tbs346.TP_LANCA == "D"
                                         select new { tbs346.VL_LANC }).ToList();

                        decimal desp = 0;
                        foreach (var li in resplafin)
                        {
                            desp += li.VL_LANC;
                        }

                        at.TOTAL_OUTRAS_DESPESAS = desp;

                        #endregion

                        //Faz o somatório para coletar os totais dos itens
                        #region

                        int totqac = 0;
                        int totqpc = 0;
                        int totqdc = 0;
                        decimal totVLD = 0;
                        decimal totVLG = 0;
                        decimal totSalddes = 0;
                        foreach (EquipeCampanha li in res)
                        {
                            totqac += li.QAC;
                            totqpc += li.QPC;
                            totqdc += li.QDC;
                            totVLD += li.VL_DIARIO;
                            totVLG += li.VL_FINAL;
                            totSalddes += li.TOTAL_OUTRAS_DESPESAS;
                        }
                        at.TOTAL_QAC = totqac;
                        at.TOTAL_QDC = totqdc;
                        at.TOTAL_QPC = totqpc;
                        at.TOTAL_VL_DIARIO = totVLD;
                        at.TOTAL_VL_FINAL = totVLG;

                        #endregion

                        if (count == res.Count)
                            MontaOGrafico(totVLD, totVLG, totSalddes);

                        at.NCL = count;
                        controlaCoresReport(ordenacao);

                        //Calcula o Total CMA 
                        #region total CMA

                        int countCMA = 0;
                        decimal auxCMA = 0;
                        foreach (EquipeCampanha li in res)
                        {
                            countCMA++;
                            auxCMA += li.CMA;
                        }
                        if (countCMA > 0)
                            at.TOTAL_CMA = auxCMA / countCMA;
                        else
                            at.TOTAL_CMA = 0;

                        #endregion

                        //Muda o Nome da coluna de acordo com a classificaçãoText escolhida no parâmetro
                        switch (coClassPor)
                        {
                            case "C":
                                xrTableCell19.Text = "";
                                xrTableCell23.WidthF = xrTableCell16.WidthF = 700;
                                break;
                            case "U":
                                xrTableCell19.Text = "UNIDADE";
                                break;
                            case "B":
                                xrTableCell19.Text = "CIDADE/BAIRRO";
                                break;
                        }

                        bsReport.Add(at);
                    }

                    return 1;
                }
                #endregion

                //=================================================================FILTRA CASO TENHA SIDO ESCOLHIDO POR CIDADE/BAIRRO============
                #region Classificado por CIDADE/BAIRRO
                else
                {
                    var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()

                               join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tbs339.CO_CIDAD_LOCAL_CAMPAN equals tb904.CO_CIDADE into lcid
                               from cida in lcid.DefaultIfEmpty()
                               join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tbs339.CO_BAIRRO_LOCAL_CAMPAN equals tb905.CO_BAIRRO into lbai
                               from bair in lbai.DefaultIfEmpty()
                               where (coTipo != "0" ? tbs339.CO_TIPO_CAMPAN == coTipo : 0 == 0)
                               && ((tbs339.DT_INICI_CAMPAN >= dataIni1) && (tbs339.DT_INICI_CAMPAN <= dataFim1))
                               select new EquipeCampanha
                               {
                                   ID_CAMPAN = tbs339.ID_CAMPAN,
                                   NO_CAMPAN = tbs339.NM_CAMPAN,
                                   CO_TIPO = tbs339.CO_TIPO_CAMPAN,
                                   //Muda o que é enviado à esta propriedade de acordo com a opção escolhida pelo usuário
                                   NomeMultavel = (cida != null && bair != null ? cida.NO_CIDADE + " / " + bair.NO_BAIRRO : " - "),
                                   coClassPor = coClassPor,
                                   CO_BAIRRO = (bair != null ? bair.CO_BAIRRO : (int?)null),
                                   CO_CIDADE = (cida != null ? cida.CO_CIDADE : (int?)null),
                               }).OrderBy(w => w.NO_CAMPAN).ThenBy(r => r.NomeMultavel).ToList();

                    //Ordena e classifica de acordo com os parâmetros escolhidos pelo usuário
                    #region Ordenações e Classificações

                    switch (ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_CAMPAN).ThenBy(w => w.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_CAMPAN).ThenByDescending(w => w.NomeMultavel).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.tipo_Valid).ThenBy(w => w.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(o => o.tipo_Valid).ThenByDescending(w => w.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.NomeMultavel).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList().ToList();
                            else
                                res = res.OrderByDescending(w => w.NomeMultavel).ThenByDescending(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList().ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QDC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QDC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QPC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QPC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAC).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAC).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.MDA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.MDA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_RECEITA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_RECEITA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 9:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_DESPESA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_DESPESA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 10:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.VL_SALDO).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.VL_SALDO).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                        case 11:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.CMA).ThenBy(o => o.NO_CAMPAN).ThenBy(b => b.NomeMultavel).ToList();
                            else
                                res = res.OrderByDescending(w => w.CMA).ThenBy(o => o.NO_CAMPAN).ThenByDescending(b => b.NomeMultavel).ToList();
                            break;
                    }
                    #endregion

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int count = 0;
                    foreach (EquipeCampanha at in res)
                    {
                        count++;

                        #region Calcula total despesas

                        var resplafin = (from tbs346 in TBS346_PLANO_FINAN_CAMPAN.RetornaTodosRegistros()
                                         where tbs346.TBS339_CAMPSAUDE.ID_CAMPAN == at.ID_CAMPAN
                                         && tbs346.TP_LANCA == "D"
                                         select new { tbs346.VL_LANC }).ToList();

                        decimal desp = 0;
                        foreach (var li in resplafin)
                        {
                            desp += li.VL_LANC;
                        }

                        at.TOTAL_OUTRAS_DESPESAS = desp;

                        #endregion

                        //Faz o somatório para coletar os totais dos itens
                        #region

                        int totqac = 0;
                        int totqpc = 0;
                        int totqdc = 0;
                        decimal totVLD = 0;
                        decimal totVLG = 0;
                        decimal totSalddes = 0;
                        foreach (EquipeCampanha li in res)
                        {
                            totqac += li.QAC;
                            totqpc += li.QPC;
                            totqdc += li.QDC;
                            totVLD += li.VL_DIARIO;
                            totVLG += li.VL_FINAL;
                            totSalddes += li.TOTAL_OUTRAS_DESPESAS;
                        }
                        at.TOTAL_QAC = totqac;
                        at.TOTAL_QDC = totqdc;
                        at.TOTAL_QPC = totqpc;
                        at.TOTAL_VL_DIARIO = totVLD;
                        at.TOTAL_VL_FINAL = totVLG;

                        #endregion

                        if (count == res.Count)
                            MontaOGrafico(totVLD, totVLG, totSalddes);

                        at.NCL = count;
                        controlaCoresReport(ordenacao);
                        //Muda o Nome da coluna de acordo com a classificaçãoText escolhida no parâmetro
                        switch (coClassPor)
                        {
                            case "C":
                                xrTableCell19.Text = "";
                                break;
                            case "U":
                                xrTableCell19.Text = "UNIDADE";
                                break;
                            case "B":
                                xrTableCell19.Text = "CIDADE/BAIRRO";
                                break;
                        }

                        bsReport.Add(at);
                    }

                    return 1;
                }
                #endregion
            }
            catch { return 0; }
        }

        private void controlaCoresReport(int Ordenacao)
        {
            switch (Ordenacao)
            {
                case 1:
                    xrTableCell23.ForeColor = Color.RoyalBlue;
                    break;
                case 2:
                    xrTableCell22.ForeColor = Color.RoyalBlue;
                    break;
                case 3:
                    xrTableCell24.ForeColor = Color.RoyalBlue;
                    break;
                case 4:
                    xrTableCell25.ForeColor = xrTableCell44.ForeColor = Color.RoyalBlue;
                    break;
                case 5:
                    xrTableCell26.ForeColor = xrTableCell45.ForeColor = Color.RoyalBlue;
                    break;
                case 6:
                    xrTableCell27.ForeColor = xrTableCell28.ForeColor = xrTableCell46.ForeColor = xrTableCell47.ForeColor = Color.RoyalBlue;
                    break;
                case 7:
                    xrTableCell30.ForeColor = xrTableCell49.ForeColor = Color.RoyalBlue;
                    break;
                case 8:
                    xrTableCell31.ForeColor = xrTableCell50.ForeColor = Color.RoyalBlue;
                    break;
                case 9:
                    xrTableCell34.ForeColor = xrTableCell53.ForeColor = Color.RoyalBlue;
                    break;
                case 10:
                    xrTableCell35.ForeColor = xrTableCell54.ForeColor = Color.RoyalBlue;
                    break;
                case 11:
                    xrTableCell36.ForeColor = xrTableCell37.ForeColor = xrTableCell55.ForeColor = xrTableCell56.ForeColor = Color.RoyalBlue;
                    break;
            }
        }

        /// <summary>
        /// Prepara o gráfico em pizza recebendo os valores necessários
        /// </summary>
        /// <param name="totDiario"></param>
        /// <param name="totFinal"></param>
        /// <param name="totOutros"></param>
        private void MontaOGrafico(decimal totDiario, decimal totFinal, decimal totOutros)
        {
            //Alimenta o Terceiro Gráfico
            Series series3 = new Series("nova3", ViewType.Pie);
            series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("R$ Diária", totDiario));
            series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("R$ Gratificação", totFinal));
            series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("R$ Outros", totOutros));
            series3.LegendPointOptions.PointView = PointView.ArgumentAndValues;
            series3.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series3.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            xrChart3.Series.Add(series3);
        }

        #endregion

        #region Classe

        public class EquipeCampanha
        {
            public string NO_CAMPAN { get; set; }
            public string CO_TIPO { get; set; }
            public string CONCAT
            {
                get
                {
                    //Concatena ou não, dependendo do parâmetro escolhido pelo usuário
                    if (this.coClassPor == "C")
                        return this.NO_CAMPAN;
                    else
                        return (this.NO_CAMPAN.Length > 45 ? this.NO_CAMPAN.Substring(0, 45) + "..." : this.NO_CAMPAN);
                }
            }
            public string tipo_Valid
            {
                get
                {
                    string tipo = "";
                    switch (this.CO_TIPO)
                    {
                        case "V":
                            tipo = "VA";
                            break;
                        case "A":
                            tipo = "AC";
                            break;
                        case "P":
                            tipo = "PR";
                            break;
                    }

                    return tipo;
                }
            }

            //Coluna que é alterada de acordo com a escolha no parâmetro do relatório
            public string NomeMultavel { get; set; }

            //Dados de auxlilio para querys
            public int ID_CAMPAN { get; set; }
            public int CO_EMP { get; set; }
            public DateTime DataInicio { get; set; }
            public DateTime DataFinal { get; set; }
            public bool comValores { get; set; }
            public string coClassPor { get; set; }
            public int? CO_BAIRRO { get; set; }
            public int? CO_CIDADE { get; set; }
            public int NCL { get; set; }

            /// <summary>
            /// Calcula a quantidade de dias programada para a campanha
            /// </summary>
            public int QDC
            {
                get
                {
                    TimeSpan ts = this.DataFinal - this.DataInicio;
                    return ts.Days;
                }
            }
            public string QDC_V
            {
                get
                {
                    return this.QDC.ToString().PadLeft(2, '0');
                }
            }
            public int TOTAL_QDC { get; set; }

            /// <summary>
            /// Calcula a Quantidade de profissionais para a campanha e unidade em questão
            /// </summary>
            public int QPC
            {
                get
                {
                    int res = 0;
                    switch (this.coClassPor)
                    {
                        //Escolhido por Unidade
                        case "U":
                            res = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                                   where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                       //Caso o contexto seja em uma determinada unidade, filtra qual a unidade do colaborador, caso não tenha unidade específica, traz todos os colaboradores sem unidades específica
                                   && (this.CO_EMP != 0 ? tbs340.CO_EMP_COLABO_CAMPAN == this.CO_EMP : tbs340.CO_EMP_COLABO_CAMPAN == null)
                                   select new { tbs340.ID_CAMPSAUDE_COLABOR }).Count();
                            break;
                        //Escolhido por campanha
                        case "C":
                            res = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                                   where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                   select new { tbs340.ID_CAMPSAUDE_COLABOR }).Count();
                            break;
                    }
                    return res;
                }
            }
            public string QPC_V
            {
                get
                {
                    return this.QPC.ToString().PadLeft(2, '0');
                }
            }
            public int TOTAL_QPC { get; set; }

            /// <summary>
            /// Calcula a Quantidade total de atendimentos de Campanha para a campanha e unidade em questão
            /// </summary>
            public int QAC
            {
                get
                {
                    int qac = 0;
                    //Retorna os dados de acordo com a classificação
                    switch (this.coClassPor)
                    {

                        //Escolhido por Unidade
                        case "U":
                            qac = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                                   //Caso o contexto seja em uma determinada unidade, filtra qual a unidade do Usuário, caso não tenha unidade específica, traz todos os Usuário sem unidades específica
                                   where tbs341.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                   && (this.CO_EMP != 0 ? tbs341.CO_EMP_ATEND == this.CO_EMP : tbs341.CO_EMP_ATEND == null)
                                   select new { tbs341.ID_CAMP_ATEND }).Count();
                            break;

                        //Escolhido Por Campanha
                        case "C":
                            qac = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                                   where tbs341.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                   select new { tbs341.ID_CAMP_ATEND }).Count();
                            break;

                        //Escolhido por Cidade/Bairro
                        case "B":
                            qac = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs341.CO_ALU equals tb07.CO_ALU
                                   //Caso o contexto seja em uma determinada unidade, filtra qual a unidade do Usuário, caso não tenha unidade específica, traz todos os Usuário sem unidades específica
                                   where tbs341.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                   && (this.CO_CIDADE.HasValue ? tb07.TB905_BAIRRO.CO_CIDADE == this.CO_CIDADE : tb07.TB905_BAIRRO == null)
                                   && (this.CO_BAIRRO.HasValue ? tb07.TB905_BAIRRO.CO_BAIRRO == this.CO_BAIRRO : tb07.TB905_BAIRRO == null)
                                   select new { tbs341.ID_CAMP_ATEND }).Count();
                            break;
                    }
                    return qac;
                }
            }
            public string QAC_V
            {
                get
                {
                    return this.QAC.ToString().PadLeft(2, '0');
                }
            }
            public string QAC_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QAC > 0)
                    {
                        decimal aux1 = this.QAC * 100;
                        decimal aux2 = aux1 / this.TOTAL_QAC;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            /// <summary>
            /// Calcula a média de atendimentos para a Campanha e Unidade em contexto
            /// </summary>
            public decimal MDA
            {
                get
                {
                    decimal mdi = 0;
                    if (this.QDC > 0)
                    {
                        mdi = this.QAC / (decimal)this.QDC;
                        return mdi;
                    }
                    else
                        return 0;
                }
            }
            public string MDA_V
            {
                get
                {
                    return this.MDA.ToString("N2").PadLeft(2, '0');
                }
            }

            /// <summary>
            /// Retorna o Valor da receita prevista para a campanha em questão
            /// </summary>
            public decimal VL_RECEITA
            {
                get
                {
                    var res = (from tbs346 in TBS346_PLANO_FINAN_CAMPAN.RetornaTodosRegistros()
                               where tbs346.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                               select new { tbs346.VL_LANC, tbs346.TP_LANCA }).ToList();

                    decimal valor = 0;
                    foreach (var li in res)
                    {
                        valor += (li.TP_LANCA == "C" ? li.VL_LANC : -li.VL_LANC);
                    }

                    return valor;
                }
            }
            public string VL_RECEITA_V
            {
                get
                {
                    return (this.comValores ? this.VL_RECEITA.ToString("N2") : " - ");
                }
            }

            /// <summary>
            /// Calcula o valor diário total para a campanha em questão
            /// </summary>
            public decimal VL_DIARIO
            {
                get
                {
                    decimal valor = 0;

                    //Alterna de acordo com escolha do usuário
                    switch (this.coClassPor)
                    {

                        //Escolhido por Campanha
                        case "C":
                            var res = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                                       where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                       select new { tbs340.VL_DIARI_COLABO_CAMPAN }).ToList();

                            foreach (var li in res)
                            {
                                valor += (li.VL_DIARI_COLABO_CAMPAN.HasValue ? this.QDC * li.VL_DIARI_COLABO_CAMPAN.Value : 0);
                            }
                            break;

                        //Escolhido por Unidade
                        case "U":
                            var res2 = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                                        where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                            //Caso o contexto seja em uma determinada unidade, filtra qual a unidade do colaborador, caso não tenha unidade específica, traz todos os colaboradores sem unidades específica
                                        && (this.CO_EMP != 0 ? tbs340.CO_EMP_COLABO_CAMPAN == this.CO_EMP : tbs340.CO_EMP_COLABO_CAMPAN == null)
                                        select new { tbs340.VL_DIARI_COLABO_CAMPAN }).ToList();

                            foreach (var li in res2)
                            {
                                valor += (li.VL_DIARI_COLABO_CAMPAN.HasValue ? this.QDC * li.VL_DIARI_COLABO_CAMPAN.Value : 0);
                            }
                            break;
                    }
                    return valor;
                }
            }
            public string VL_DIARIO_V
            {
                get
                {
                    if (this.coClassPor == "U" || this.coClassPor == "C")
                        return (this.comValores ? this.VL_DIARIO.ToString("N2") : " - ");
                    else
                        return " - ";
                }
            }
            public decimal TOTAL_VL_DIARIO { get; set; }

            /// <summary>
            /// Calcula o valor final para a campanha de acordo com os informados nos colaboradores participantes
            /// </summary>
            public decimal VL_FINAL
            {
                get
                {
                    decimal valor = 0;

                    //Alterna de acordo com escolha do usuário
                    switch (this.coClassPor)
                    {

                        //Escolhido por Campanha
                        case "C":
                            var res = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                                       where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                       //Caso o contexto seja em uma determinada unidade, filtra qual a unidade do colaborador, caso não tenha unidade específica, traz todos os colaboradores sem unidades específica
                                       select new { tbs340.VL_FINAL_COLABO_CAMPAN }).ToList();

                            foreach (var li in res)
                            {
                                valor += (li.VL_FINAL_COLABO_CAMPAN.HasValue ? li.VL_FINAL_COLABO_CAMPAN.Value : 0);
                            }
                            break;

                        //Escolhido por Unidade
                        case "U":
                            var res2 = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                                        where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == this.ID_CAMPAN
                                            //Caso o contexto seja em uma determinada unidade, filtra qual a unidade do colaborador, caso não tenha unidade específica, traz todos os colaboradores sem unidades específica
                                        && (this.CO_EMP != 0 ? tbs340.CO_EMP_LOCAL_CAMP == this.CO_EMP : tbs340.CO_EMP_COLABO_CAMPAN == null)
                                        select new { tbs340.VL_FINAL_COLABO_CAMPAN }).ToList();

                            foreach (var li in res2)
                            {
                                valor += (li.VL_FINAL_COLABO_CAMPAN.HasValue ? li.VL_FINAL_COLABO_CAMPAN.Value : 0);
                            }
                            break;
                    }
                    return valor;
                }
            }
            public string VL_FINAL_V
            {
                get
                {
                    return (this.comValores ? this.VL_FINAL.ToString("N2") : " - ");
                }
            }
            public decimal TOTAL_VL_FINAL { get; set; }

            public decimal TOTAL_OUTRAS_DESPESAS { get; set; }

            /// <summary>
            /// Calcula o valor da despesa de acordo com os valores diários e finais
            /// </summary>
            public decimal VL_DESPESA
            {
                get
                {
                    return this.VL_FINAL + this.VL_DIARIO;
                }
            }
            public string VL_DESPESA_V
            {
                get
                {
                    return (this.comValores ? this.VL_DESPESA.ToString("N2") : " - ");
                }
            }

            /// <summary>
            /// Calcula O Saldo para a campanha e Unidade de acordo com os valores diário e final
            /// </summary>
            public decimal VL_SALDO
            {
                get
                {
                    return this.VL_RECEITA - this.VL_DIARIO - this.VL_FINAL;
                }
            }
            public string VL_SALDO_V
            {
                get
                {
                    return (this.comValores ? this.VL_SALDO.ToString("N2") : " - ");
                }
            }

            /// <summary>
            /// Calcula o custo médio de atendimento dividindo o valor total dos custos pela quantidade de atendimentos
            /// </summary>
            public decimal CMA
            {
                get
                {
                    if (this.QAC > 0)
                    {
                        decimal md = this.VL_DIARIO + this.VL_FINAL;
                        md = md / this.QAC;
                        return md;
                    }
                    else
                        return this.VL_DIARIO + this.VL_FINAL;
                }
            }
            public string CMA_V
            {
                get
                {
                    return (this.comValores == true ? this.CMA.ToString("N2") : " - ");
                }
            }
            public string CMA_PER
            {
                get
                {
                    decimal percent = 0;
                    decimal CMA = this.CMA;
                    if (CMA > 0)
                    {
                        //if (this.TOTAL_CMA < CMA)
                        //    percent = (1 - (this.TOTAL_CMA / CMA)) * 100;
                        //else
                        percent = ((CMA / this.TOTAL_CMA) - 1) * 100;

                        return (percent == 100 ? decimal.Floor(percent).ToString() : percent.ToString("N1"));
                    }
                    else
                        return "0";
                }
            }

            public int TOTAL_QAC { get; set; }

            public decimal TOTAL_CMA { get; set; }
            public string TOTAL_CMA_V
            {
                get
                {
                    return this.TOTAL_CMA.ToString("N2");
                }
            }

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

