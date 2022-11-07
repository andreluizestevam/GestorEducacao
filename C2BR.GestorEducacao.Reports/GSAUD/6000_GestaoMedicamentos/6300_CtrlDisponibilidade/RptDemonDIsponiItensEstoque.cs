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
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6300_CtrlDisponibilidade
{
    public partial class RptDemonDIsponiItensEstoque : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonDIsponiItensEstoque()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              int Grupo,
                              int SubGrupo,
                              int Item,
                              string dataIni,
                              string dataFim,
                              string classific,
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

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                           join tb140 in TB124_TIPO_PRODUTO.RetornaTodosRegistros() on tb90.TB124_TIPO_PRODUTO.CO_TIP_PROD equals tb140.CO_TIP_PROD into la1
                           from tpProd in la1.DefaultIfEmpty()
                           join tb260 in TB260_GRUPO.RetornaTodosRegistros() on tb90.TB260_GRUPO.ID_GRUPO equals tb260.ID_GRUPO
                           join tb261 in TB261_SUBGRUPO.RetornaTodosRegistros() on tb90.TB261_SUBGRUPO.ID_SUBGRUPO equals tb261.ID_SUBGRUPO
                           join tb95 in TB95_CATEGORIA.RetornaTodosRegistros() on tb90.TB95_CATEGORIA.CO_CATEG equals tb95.CO_CATEG into la2
                           from catProd in la2.DefaultIfEmpty()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb90.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                           join tb89 in TB89_UNIDADES.RetornaTodosRegistros() on tb90.TB89_UNIDADES.CO_UNID_ITEM equals tb89.CO_UNID_ITEM
                           where (coUnid != 0 ? tb90.TB25_EMPRESA.CO_EMP == coUnid : 0 == 0)
                           && (Grupo != 0 ? tb260.ID_GRUPO == Grupo : 0 == 0)
                           && (SubGrupo != 0 ? tb261.ID_SUBGRUPO == SubGrupo : 0 == 0)
                           && (Item != 0 ? tb90.CO_PROD == Item : 0 == 0)
                           select new DemonDisponiItens
                           {
                               NomProduto = tb90.NO_PROD,
                               NomReduzido = tb90.NO_PROD_RED,
                               CodProd = tb90.CO_REFE_PROD,
                               NoTipoProd = (tpProd != null ? tpProd.DE_TIP_PROD : " - "),
                               NoGrupo = tb260.CO_GRUPO,
                               NoSubGrupo = tb261.NOM_SUBGRUPO,
                               //NoCategoria = (catProd != null ? catProd.DES_CATEG : " - "),
                               Unidade = tb25.sigla,
                               UnidMedida = tb89.NO_UNID_ITEM,
                               dataIni = dataIni1,
                               dataFim = dataFim1,
                               CO_PROD = tb90.CO_PROD,
                               CO_EMP = tb25.CO_EMP,
                           }).ToList();

                //Ordena e classifica de acordo com o escolhido pelo usuário
                switch (classific)
                {
                    case "1":
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.NomProduto).ThenBy(y => y.Unidade).ToList();
                        else
                            res = res.OrderByDescending(o => o.NomProduto).ThenByDescending(y => y.Unidade).ToList();
                        break;
                    case "2":
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.NoSubGrupo).ThenBy(y => y.Unidade).ToList();
                        else
                            res = res.OrderByDescending(o => o.NoSubGrupo).ThenByDescending(y => y.Unidade).ToList();
                        break;
                }

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                int auxCount = 0;
                decimal TOTALQTE = 0;
                int TOTALQTS = 0;
                foreach (DemonDisponiItens at in res)
                {
                    auxCount++;

                    decimal totQte = 0;
                    int totQts = 0;
                    decimal totSaldo = 0;
                    if (auxCount == res.Count)
                    {
                        foreach (DemonDisponiItens li in res)
                        {
                            totQte += li.QTE;
                            totQts += li.QTS;
                            totSaldo += decimal.Parse(li.SALDO);
                        }
                        at.TOTAL_QTS = totQts;
                        at.TOTAL_QTE = totQte;
                        at.TOT_SALDO = totSaldo;

                        TOTALQTE = totQte;
                        TOTALQTS = totQts;
                    }

                    //Muda a cor da coluna de acordo com a ordenação escolhida
                    switch (coTipoOrdem)
                    {
                        case "1":
                            xrTableCell16.ForeColor = Color.RoyalBlue;
                            break;
                        case "2":
                            xrTableCell21.ForeColor = Color.RoyalBlue;
                            break;
                    }

                    bsReport.Add(at);
                }

                //Alimenta o Terceiro Gráfico
                Series series3 = new Series("nova3", ViewType.Pie);
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTE - Qtde Entrada", TOTALQTE));
                series3.Points.Add(new DevExpress.XtraCharts.SeriesPoint("QTS - Qtde Saída", TOTALQTS));
                series3.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                series3.LegendPointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                series3.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                xrChart2.Series.Add(series3);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class DemonDisponiItens
        {
            public string NomProduto { get; set; }
            public string NomReduzido { get; set; }
            public string CodProd { get; set; }
            public string NoTipoProd { get; set; }
            public string NoGrupo { get; set; }
            public string NoSubGrupo { get; set; }
            public string NoCategoria { get; set; }
            public string Unidade { get; set; }
            public string UnidMedida { get; set; }
            public decimal QTE
            {
                get
                {
                    //Contabiliza a quantidade de entradas para o produto em questão dentro do período desejado
                    var res = (from tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros()
                               join tb93 in TB93_TIPO_MOVIMENTO.RetornaTodosRegistros() on tb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV equals tb93.CO_TIPO_MOV
                               where tb93.FLA_TP_MOV == "E"
                               && tb91.CO_PROD == this.CO_PROD
                               && ((tb91.DT_MOV_PROD >= this.dataIni) && (tb91.DT_MOV_PROD <= this.dataFim))
                               select new { tb91.QT_MOV_PROD }).ToList();

                    decimal quantidade = 0;
                    if (res != null)
                    {
                        foreach (var li in res)
                        {
                            quantidade += li.QT_MOV_PROD;
                        }
                    }

                    return int.Parse((decimal.Floor(quantidade)).ToString());
                }
            }
            public decimal TOTAL_QTE { get; set; }
            public int QTS
            {
                get
                {
                    //Contabiliza a quantidade de entradas para o produto em questão dentro do período desejado
                    var res = (from tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros()
                               join tb93 in TB93_TIPO_MOVIMENTO.RetornaTodosRegistros() on tb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV equals tb93.CO_TIPO_MOV
                               where tb93.FLA_TP_MOV == "S"
                               && tb91.CO_PROD == this.CO_PROD
                               && ((tb91.DT_MOV_PROD >= this.dataIni) && (tb91.DT_MOV_PROD <= this.dataFim))
                               select new { tb91.QT_MOV_PROD }).ToList();

                    decimal quantidade = 0;
                    if (res != null)
                    {
                        foreach (var li in res)
                        {
                            quantidade += li.QT_MOV_PROD;
                        }
                    }

                    return int.Parse((decimal.Floor(quantidade)).ToString());
                }
            }
            public decimal TOTAL_QTS { get; set; }
            public string SALDO
            {
                get
                {
                    var varTb96 = TB96_ESTOQUE.RetornaPelaChavePrimaria(this.CO_EMP, this.CO_PROD);

                    if (varTb96 != null)
                        return varTb96.QT_SALDO_EST.ToString("N1");
                    else
                        return "0";
                }
            }
            public decimal TOT_SALDO { get; set; } 

            //Usados na querys
            public DateTime dataIni { get; set; }
            public DateTime dataFim { get; set; }
            public int CO_PROD { get; set; }
            public int CO_EMP { get; set; }
            public string x
            {
                get
                {
                    return "TOTAL";
                }
            }
        }
    }
}

