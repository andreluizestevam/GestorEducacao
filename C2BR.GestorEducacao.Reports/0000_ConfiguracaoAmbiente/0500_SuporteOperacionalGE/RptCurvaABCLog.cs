using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE
{
    public partial class RptCurvaABCLog : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptCurvaABCLog()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmpRef,
                               int coUnid,
                               int codCol,
                               string strAcao,
                               DateTime dtInicio,
                               DateTime dtFim,
                               string infos,
                               int Ordenacao,
                               string coTipoOrdem,
                               string noPeriodo,
                               string Classificacao,
                               string NO_RELATORIO
            )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                var header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(codEmpRef);

                if (header == null)
                    return 0;

                //Seta o título dinamicamente
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "RESUMO DE ATIVIDADES REALIZADAS P/ CLIENTES" + " - " + noPeriodo + "*");

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                var res = new List<LogAtividUsuario>();
                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");

                if (Classificacao == "A")
                {
                    #region Query por Atividade

                    res = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                           join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                           from tb25 in ctx.TB25_EMPRESA
                           where tb236.ORG_CODIGO_ORGAO == codInst
                            && (codCol != 0 ? tb236.CO_COL == codCol : codCol == 0)
                            && (coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == coUnid : 0 == 0)
                            && (strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == strAcao : strAcao == "T")
                            && tb236.DT_ATIVI_LOG >= dtInicio && tb236.DT_ATIVI_LOG <= dtFim
                           select new LogAtividUsuario
                           {
                               ID_ATIVIDADE = admModul.ideAdmModulo,
                               NO_FLEX_ATIV_COL = admModul.nomModulo,
                               CO_COL = 0,
                               CO_CLASSIFICACAO = Classificacao,

                               codInst = codInst,
                               codCol = codCol,
                               coUnid = coUnid,
                               strAcao = strAcao,
                               dtInicio = dtInicio,
                               dtFim = dtFim,
                           }).OrderBy(p => p.NO_FLEX_ATIV_COL).DistinctBy(w => w.ID_ATIVIDADE).ToList();

                    #endregion

                }
                else
                {
                    #region Query por Colaborador

                    res = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb236.CO_COL equals tb03.CO_COL
                           from tb25 in ctx.TB25_EMPRESA
                           where tb236.ORG_CODIGO_ORGAO == codInst
                            && (codCol != 0 ? tb236.CO_COL == codCol : codCol == 0)
                            && (coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == coUnid : 0 == 0)
                            && (strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == strAcao : strAcao == "T")
                            && tb236.DT_ATIVI_LOG >= dtInicio && tb236.DT_ATIVI_LOG <= dtFim
                           select new LogAtividUsuario
                           {
                               ID_ATIVIDADE = 0,
                               CO_COL = tb236.CO_COL,
                               NO_FLEX_ATIV_COL = tb03.NO_COL,
                               CO_CLASSIFICACAO = Classificacao,

                               codInst = codInst,
                               codCol = codCol,
                               coUnid = coUnid,
                               strAcao = strAcao,
                               dtInicio = dtInicio,
                               dtFim = dtFim,
                           }).DistinctBy(w => w.CO_COL).OrderBy(p => p.NO_FLEX_ATIV_COL).ToList();

                    #endregion
                }

                #region Ordenação

                //Ordena e classifica de acordo com o escolhido pelo usuário
                switch (Ordenacao)
                {
                    case 1:
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.NO_FLEX_ATIV_COL).ToList();
                        else
                            res = res.OrderByDescending(o => o.NO_FLEX_ATIV_COL).ToList();
                        break;
                    case 2:
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.USU).ThenBy(w => w.NO_FLEX_ATIV_COL).ToList();
                        else
                            res = res.OrderByDescending(o => o.USU).ThenByDescending(w => w.NO_FLEX_ATIV_COL).ToList();
                        break;
                    case 3:
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.TMP_HORAS).ThenBy(w => w.NO_FLEX_ATIV_COL).ToList();
                        else
                            res = res.OrderByDescending(o => o.TMP_HORAS).ThenByDescending(w => w.NO_FLEX_ATIV_COL).ToList();
                        break;
                    case 4:
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.SACAO).ThenBy(w => w.NO_FLEX_ATIV_COL).ToList();
                        else
                            res = res.OrderByDescending(o => o.SACAO).ThenByDescending(w => w.NO_FLEX_ATIV_COL).ToList();
                        break;
                    case 5:
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.CACAO).ThenBy(w => w.NO_FLEX_ATIV_COL).ToList();
                        else
                            res = res.OrderByDescending(o => o.CACAO).ThenByDescending(w => w.NO_FLEX_ATIV_COL).ToList();
                        break;
                    case 6:
                        if (coTipoOrdem == "C")
                            res = res.OrderBy(o => o.TOTAL).ThenBy(w => w.NO_FLEX_ATIV_COL).ToList();
                        else
                            res = res.OrderByDescending(o => o.TOTAL).ThenByDescending(w => w.NO_FLEX_ATIV_COL).ToList();
                        break;
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                //Altera de acordo com a classificação desejada
                if (Classificacao == "A")
                {
                    xrTableCell6.Text = "ATIVIDADE SISTÊMICA";
                    xrTableCell8.Text = "QUA";
                    xrTableCell7.Text = "TAF";
                    xrLabel1.Text = "Legenda Quantidades:QUA (Qtde Usuários Acessos) - TAF (Tempo Acesso Funcionalidade) - QASA (Qtde Acesso Sem Ação) - QACA (Qtde Acesso Com Ação)";

                }
                else
                {
                    xrTableCell6.Text = "USUÁRIO";
                    xrTableCell8.Text = "QFA";
                    xrTableCell7.Text = "TAU";
                    xrLabel1.Text = "Legenda Quantidades:QFA (Qtde Funcionalidades Acessadas) - TAU (Tempo de Acesso do Usuário) - QASA (Qtde Acesso Sem Ação) - QACA (Qtde Acesso Com Ação)";
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                decimal perUsu = 0;
                decimal perTotal = 0;
                int qtHorasTotal = 0;
                int qtMinutosTotal = 0;

                foreach (LogAtividUsuario at in res)
                {
                    perTotal += decimal.Parse(at.TOTAL_PER);
                    perUsu += decimal.Parse(at.USU_PER);

                    at.TOTAL_TOTAL_PER = decimal.Floor(perTotal).ToString();
                    at.TOTAL_USU_PER = decimal.Floor(perUsu).ToString();

                    at.TMP = (at.TMP_HORAS + "h " + at.TMP_MINUTOS.ToString().PadLeft(2, '0') + "m");

                    //------------------------------

                    qtHorasTotal += at.TMP_HORAS;
                    qtMinutosTotal += at.TMP_MINUTOS;
                    if (qtMinutosTotal >= 60)
                    {
                        qtMinutosTotal = qtMinutosTotal - 60;
                        qtHorasTotal += 1;
                    }
                    at.TOTAL_TMP = qtHorasTotal + "h " + qtMinutosTotal.ToString().PadLeft(2, '0') + "m";

                    //------------------------------

                    SetaCoresOrdenacao(Ordenacao);

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Seta cor nas colunas referentes ao objeto de uma ordenação escolhida nos parâmetros
        /// </summary>
        /// <param name="Ordenacao"></param>
        protected void SetaCoresOrdenacao(int Ordenacao)
        {
            //Muda a cor da coluna de acordo com a ordenação escolhida
            //switch (Ordenacao)
            //{
            //    case 1:
            //        xrTableCell10.ForeColor = Color.RoyalBlue;
            //        break;
            //    case 2:
            //        xrTableCell11.ForeColor = xrTableCell29.ForeColor = xrTableCell19.ForeColor = xrTableCell30.ForeColor = Color.RoyalBlue;
            //        break;
            //    case 3:
            //        xrTableCell12.ForeColor = xrTableCell20.ForeColor = Color.RoyalBlue;
            //        break;
            //    case 4:
            //        xrTableCell13.ForeColor = xrTableCell21.ForeColor = Color.RoyalBlue;
            //        break;
            //    case 5:
            //        xrTableCell14.ForeColor = xrTableCell22.ForeColor = xrTableCell15.ForeColor = xrTableCell23.ForeColor = Color.RoyalBlue;
            //        break;
            //}
        }

        #endregion

        #region Classe
        public class saidaAux
        {
            public DateTime DT { get; set; }
            public string DT_V
            {
                get
                {
                    return this.DT.ToString("dd/MM/yyyy");
                }
            }
            public int ID_LOG { get; set; }
        }

        public class LogAtividUsuario
        {
            public int CO_COL { get; set; }
            public string CO_CLASSIFICACAO { get; set; }

            public int ID_ATIVIDADE { get; set; }
            public int codInst { get; set; }
            public int codCol { get; set; }
            public int coUnid { get; set; }
            public string strAcao { get; set; }
            public DateTime dtInicio { get; set; }
            public DateTime dtFim { get; set; }

            public string NO_FLEX_ATIV_COL { get; set; }

            //Quantidade total de acessos por usuario
            public int USU
            {
                get
                {
                    var ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                      //Executa dinâmicamente de acordo com a opção selecionada na página de filtro
                                   && (this.ID_ATIVIDADE != 0 ? admModul.ideAdmModulo == this.ID_ATIVIDADE : 0 == 0)
                                   && (this.CO_COL != 0 ? tb236.CO_COL == this.CO_COL : 0 == 0)
                                  select new
                                  {
                                      tb236.CO_COL,
                                      admModul.ideAdmModulo,
                                  }).ToList();

                    if (this.CO_CLASSIFICACAO == "A") // Se for classificado por atividade, faz distinct em colaborador
                        return ressub.DistinctBy(w => w.CO_COL).Count();
                    else // Se for classificado por colaborador, faz distinct em atividade
                        return ressub.DistinctBy(w => w.ideAdmModulo).Count();
                }
            }
            public string USU_V
            {
                get
                {
                    return this.USU.ToString().PadLeft(2, '0');
                }
            }
            public string USU_PER
            {
                get
                {
                    if (this.TOTAL_USU > 0)
                    {
                        decimal aux1 = this.USU * 100;
                        decimal aux2 = aux1 / this.TOTAL_USU;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1")); // calcula porcentagem
                    }
                    else
                        return "0";
                }
            }

            //Quantidade total de tempo
            public string TMP { get; set; }
            public int TMP_HORAS
            {
                get
                {
                    var ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                      //Executa dinâmicamente de acordo com a opção selecionada na página de filtro
                                   && (this.ID_ATIVIDADE != 0 ? admModul.ideAdmModulo == this.ID_ATIVIDADE : 0 == 0)
                                   && (this.CO_COL != 0 ? tb236.CO_COL == this.CO_COL : 0 == 0)
                                  select new saidaAux
                                  {
                                      ID_LOG = tb236.ID_LOG_ATIVIDADES,
                                      DT = tb236.DT_ATIVI_LOG,
                                  }).DistinctBy(w => w.ID_LOG).OrderBy(w => w.DT).ToList();

                    var aux = ressub.DistinctBy(w => w.DT_V).ToList();

                    //Calcula, para cada dia de acesso, o tempo entre o primeiro e último acesso
                    int horas = 0;
                    int minutos = 0;
                    foreach (var i in aux)
                    {
                        DateTime primData = ressub.Where(w => w.DT_V == i.DT_V).FirstOrDefault().DT; // Primeira vez que a funcionalidade foi acessada
                        DateTime ultiData = ressub.Where(w => w.DT_V == i.DT_V).LastOrDefault().DT; // última vez que a funcionalidade foi acessada

                        TimeSpan ts = ultiData.Subtract(primData);
                        horas += ts.Hours + (ts.Days * 24); // Soma a quantidade de horas com (Quantidade de dias * 24) do dia;
                        minutos += ts.Minutes;

                        //Calcula se os minutos passaram de 60, caso tenham passado, subtrai 60 e soma mais um nas horas
                        if (minutos >= 60)
                        {
                            minutos = minutos - 60;
                            horas += 1;
                        }
                    }

                    this.TMP_MINUTOS = minutos;
                    return horas;
                }
            }
            public int TMP_MINUTOS { get; set; }
            //{
            //    get
            //    {
            //        var ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
            //                      join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
            //                      where tb236.ORG_CODIGO_ORGAO == this.codInst
            //                       && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
            //                       && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
            //                       && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
            //                       && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
            //                          //Executa dinâmicamente de acordo com a opção selecionada na página de filtro
            //                       && (this.ID_ATIVIDADE != 0 ? admModul.ideAdmModulo == this.ID_ATIVIDADE : 0 == 0)
            //                       && (this.CO_COL != 0 ? tb236.CO_COL == this.CO_COL : 0 == 0)
            //                      select new
            //                      {
            //                          tb236.ID_LOG_ATIVIDADES,
            //                          tb236.DT_ATIVI_LOG,
            //                      }).DistinctBy(w => w.ID_LOG_ATIVIDADES).OrderBy(w => w.DT_ATIVI_LOG).ToList();

            //        DateTime primData = (ressub.Count > 0 ? ressub.FirstOrDefault().DT_ATIVI_LOG : DateTime.Now); // Primeira vez que a funcionalidade foi acessada
            //        DateTime ultiData = (ressub.Count > 0 ? ressub.LastOrDefault().DT_ATIVI_LOG : DateTime.Now); // última vez que a funcionalidade foi acessada

            //        TimeSpan ts = ultiData.Subtract(primData);
            //        int min = ts.Minutes;
            //        return min;
            //    }
            //}

            //Quantidade total de acessos sem acao
            public int SACAO
            {
                get
                {
                    int ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                      //Executa dinâmicamente de acordo com a opção selecionada na página de filtro
                                   && (this.ID_ATIVIDADE != 0 ? admModul.ideAdmModulo == this.ID_ATIVIDADE : 0 == 0)
                                   && (this.CO_COL != 0 ? tb236.CO_COL == this.CO_COL : 0 == 0)
                                   && tb236.CO_ACAO_ATIVI_LOG == "X"
                                  select new
                                  {
                                      tb236.ID_LOG_ATIVIDADES,
                                  }).Distinct().Count();

                    return ressub;
                }
            }
            public string SACAO_V
            {
                get
                {
                    return this.SACAO.ToString().PadLeft(2, '0');
                }
            }

            //Quantidade total de acessos com acao
            public int CACAO
            {
                get
                {
                    int ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                   && tb236.CO_ACAO_ATIVI_LOG != "X"
                                      //Executa dinâmicamente de acordo com a opção selecionada na página de filtro
                                   && (this.ID_ATIVIDADE != 0 ? admModul.ideAdmModulo == this.ID_ATIVIDADE : 0 == 0)
                                   && (this.CO_COL != 0 ? tb236.CO_COL == this.CO_COL : 0 == 0)
                                  select new
                                  {
                                      tb236.ID_LOG_ATIVIDADES,
                                  }).Distinct().Count();

                    return ressub;
                }
            }
            public string CACAO_V
            {
                get
                {
                    return this.CACAO.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL
            {
                get
                {
                    int ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                      //Executa dinâmicamente de acordo com a opção selecionada na página de filtro
                                   && (this.ID_ATIVIDADE != 0 ? admModul.ideAdmModulo == this.ID_ATIVIDADE : 0 == 0)
                                   && (this.CO_COL != 0 ? tb236.CO_COL == this.CO_COL : 0 == 0)
                                  select new
                                  {
                                      tb236.ID_LOG_ATIVIDADES,
                                  }).Distinct().Count();

                    return ressub;
                }
            }
            public string TOTAL_V
            {
                get
                {
                    return this.TOTAL.ToString().PadLeft(2, '0');
                }
            }
            public string TOTAL_PER
            {
                get
                {
                    if (this.TOTAL_TOTAL > 0)
                    {
                        decimal aux1 = this.TOTAL * 100;
                        decimal aux2 = aux1 / this.TOTAL_TOTAL;
                        return (aux2 >= 100 ? decimal.Floor(aux2).ToString() : aux2.ToString("N1"));// calcula porcentagem
                    }
                    else
                        return "0";
                }
            }

            //Totais de usuários ou atividades, dependendo do parâmetro recebido pela página
            public int TOTAL_USU
            {
                get
                {
                    var ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                  select new
                                  {
                                      tb236.CO_COL,
                                      admModul.ideAdmModulo,
                                  }).ToList();

                    if (this.CO_CLASSIFICACAO == "A") // Se for classificado por atividade, faz distinct em colaborador
                        return ressub.DistinctBy(w => w.CO_COL).Count();
                    else // Se for classificado por colaborador, faz distinct em atividade
                        return ressub.DistinctBy(w => w.ideAdmModulo).Count();
                }
            }
            public string TOTAL_USU_V
            {
                get
                {
                    return this.TOTAL_USU.ToString().PadLeft(2, '0');
                }
            }
            public string TOTAL_USU_PER { get; set; }

            //Total de logs Com Ação
            public int TOTAL_CACAO
            {
                get
                {
                    int ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                   && tb236.CO_ACAO_ATIVI_LOG != "X"
                                  select new
                                  {
                                      tb236.ID_LOG_ATIVIDADES,
                                  }).Distinct().Count();

                    return ressub;
                }
            }
            public string TOTAL_CACAO_V
            {
                get
                {
                    return this.TOTAL_CACAO.ToString().PadLeft(2, '0');
                }
            }

            //Total de logs sem ação
            public int TOTAL_SACAO
            {
                get
                {
                    int ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                   && tb236.CO_ACAO_ATIVI_LOG == "X"
                                  select new
                                  {
                                      tb236.ID_LOG_ATIVIDADES,
                                  }).Distinct().Count();

                    return ressub;
                }
            }
            public string TOTAL_SACAO_V
            {
                get
                {
                    return this.TOTAL_SACAO.ToString().PadLeft(2, '0');
                }
            }

            //Total Geral com ou sem ação
            public int TOTAL_TOTAL
            {
                get
                {
                    int ressub = (from tb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                  join admModul in ADMMODULO.RetornaTodosRegistros() on tb236.IDEADMMODULO equals admModul.ideAdmModulo
                                  where tb236.ORG_CODIGO_ORGAO == this.codInst
                                   && (this.codCol != 0 ? tb236.CO_COL == this.codCol : this.codCol == 0)
                                   && (this.coUnid != 0 ? tb236.CO_EMP_ATIVI_LOG == this.coUnid : 0 == 0)
                                   && (this.strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == this.strAcao : this.strAcao == "T")
                                   && tb236.DT_ATIVI_LOG >= this.dtInicio && tb236.DT_ATIVI_LOG <= this.dtFim
                                  select new
                                  {
                                      tb236.ID_LOG_ATIVIDADES,
                                  }).Distinct().Count();

                    return ressub;
                }
            }
            public string TOTAL_TOTAL_V
            {
                get
                {
                    return this.TOTAL_TOTAL.ToString().PadLeft(2, '0');
                }
            }
            public string TOTAL_TOTAL_PER { get; set; }

            public string TOTAL_TMP { get; set; }
        }

        #endregion
    }
}
