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


namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional
{
    public partial class RptExtratoPlantaoColabor : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtratoPlantaoColabor()
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
                              int coCol,
                              string situacaoFuncional,
                              string dataIni,
                              string dataFim,
                              bool comValor,
                              int coDept
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

                // Cria o header a partir do cod da instituicao
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                           join tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros() on tb159.ID_TIPO_PLANT equals tb153.ID_TIPO_PLANT
                           join espPla in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb159.CO_ESPEC_PLANT equals espPla.CO_ESPECIALIDADE
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL

                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE into les
                           from lespCol in les.DefaultIfEmpty()

                           join tb20 in TB20_TIPOCON.RetornaTodosRegistros() on tb03.CO_TPCON equals tb20.CO_TPCON into ltp
                           from toconCol in ltp.DefaultIfEmpty()

                           join tb25Col in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25Col.CO_EMP
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb159.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb159.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO
                           where (CoUnidFunc != 0 ? tb03.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                           && (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                           && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                           && (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                           && (situacaoFuncional != "0" ? tb03.CO_SITU_COL == situacaoFuncional : 0 == 0)
                           && (coDept != 0 ? tb159.TB14_DEPTO.CO_DEPTO == coDept : 0 == 0)
                           && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                           && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))

                           select new QuadropPlantonistas
                           {
                               //Dados do Colaborador
                               noCol = tb03.NO_COL,
                               CoCol = tb03.CO_COL,
                               matCol = tb03.CO_MAT_COL,
                               ColSex = tb03.CO_SEXO_COL,
                               ColEspec = lespCol.NO_ESPECIALIDADE,
                               funCol = tb03.DE_FUNC_COL,
                               ColUnidFunc = tb25Col.sigla,
                               ColSituaFunc = tb03.CO_SITU_COL,
                               tpContrato = toconCol.NO_TPCON,
                               valorHoraColab = tb159.VL_HORA_PLANT_COLAB,

                               //Dados do Agendamento
                               unidade = tb25.sigla,
                               local = tb14.NO_DEPTO,
                               situacao = tb159.CO_SITUA_AGEND,
                               dtAgend = tb159.DT_CADAS_AGEND,
                               EspecPlantao = espPla.NO_ESPECIALIDADE,
                               dataHRInicio = tb159.DT_INICIO_PREV,
                               dataHRFinal = tb159.DT_TERMIN_PREV,
                               RI = (tb159.FL_INCON_AGEND == "S" ? "*" : ""),
                               comValor = comValor,

                               //Dados do tipo de plantão
                               sglTpPlantao = tb153.CO_SIGLA_TIPO_PLANT,
                               qtHorasTPPlan_R = tb153.QT_HORAS,
                               hrIniTPPlan = tb153.HR_INI_TIPO_PLANT,
                           }).OrderBy(w=>w.noCol).ThenBy(p=>p.dataHRInicio).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                //Variáveis de Auxílio
                int auxCount = 0;
                int auxCoCol = 0;
                int countGeral = 0;
                int qtCo = 0;

                //Valores do Plantão
                decimal totVlPlantao = 0;
                decimal totVlPlantaoTotal = 0;
                decimal AUXvalMediaTotalColaborador = 0;
                decimal AUXvalMediaTotalGeral = 0; 

                foreach (QuadropPlantonistas at in res)
                {
                    countGeral++;
                    //parte responsável por garantir que o bloco acima seja executado apenas uma vez por colaborador
                    if (auxCoCol != at.CoCol)
                    {
                        //Zera as variáveis de auxílio
                        auxCount = 0;
                        qtCo = 0;
                        totVlPlantao = 0;
                        AUXvalMediaTotalColaborador = 0;

                        //Inicia a variável de código do colaborador com o novo colaborador
                        auxCoCol = at.CoCol;
                    }

                    //Conta quantos registros são referentes ao colaborador em questão, para concanetar a string total apenas no último registro do colaborador
                    if(auxCount == 0)
                    foreach (QuadropPlantonistas atin in res)
                    {
                        if (atin.CoCol == at.CoCol)
                            qtCo++;
                    }

                    auxCount++;

                    //Atribui o valor total dos plantões do colaborador de acordo com o escolhido no parâmetro
                    if (comValor)
                    {
                        totVlPlantaoTotal += (at.valorPlantaoColab != " - " ? decimal.Parse(at.valorPlantaoColab) : 0);
                        totVlPlantao += (at.valorPlantaoColab != " - " ? decimal.Parse(at.valorPlantaoColab) : 0);
                        at.valorPlantaoTotalColab = totVlPlantao.ToString("N2");

                        AUXvalMediaTotalGeral += (at.valorHoraColab.HasValue ? at.valorHoraColab.Value : 0);
                        AUXvalMediaTotalColaborador += (at.valorHoraColab.HasValue ? at.valorHoraColab.Value : 0);
                    }
                    else
                        at.valorPlantaoTotalColab = " - ";

                    //bloco de código que é feito apenas uma vez por colaborador para economia de desempenho
                    #region Counts Colaborador

                    if (auxCount == qtCo)
                    {
                        //Quantitativo de plantões em unidades distintas dentro dos parâmetros
                        int qtUnid = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                      where (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                         && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                         && tb159.TB03_COLABOR.CO_COL == at.CoCol
                                         && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                         && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                      select new { tb159.CO_EMP_AGEND_PLANT }).Distinct().Count();

                        //Quantitativo de plantões em locais distintos dentro dos parâmetros
                        int qtLocais = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                      where (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                         && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                         && tb159.TB03_COLABOR.CO_COL == at.CoCol
                                         && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                         && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                      select new { tb159.TB14_DEPTO.CO_DEPTO }).Distinct().Count();

                        //Quantitativo de plantões em especialidades distintas dentro dos parâmetros
                        int qtEspec = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                      where (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                         && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                         && tb159.TB03_COLABOR.CO_COL == at.CoCol
                                         && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                         && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                      select new { tb159.CO_ESPEC_PLANT }).Distinct().Count();

                        //Quantitativo de com tipos de plantões diferentes dentro dos parâmetros
                        int qtTpPlan = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                      where (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                         && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                         && tb159.TB03_COLABOR.CO_COL == at.CoCol
                                         && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                         && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                      select new { tb159.ID_TIPO_PLANT }).Distinct().Count();

                        //Quantitativo de com dias diferentes dentro dos parâmetros
                        int qtDias = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                        where (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                           && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                           && tb159.TB03_COLABOR.CO_COL == at.CoCol
                                           && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                           && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                        //select new { tb159.DT_INICIO_PREV }).Distinct().Count();
                                       select new { tb159.DT_INICIO_PREV }).Distinct().Count();

                        //Calcula a média da hora/plantão do Colaborador
                        decimal mediaHoraPlantao = AUXvalMediaTotalColaborador / qtCo;

                        //Concatenação das informações tratando se é plural ou não
                        string dia = (qtDias > 1 ? " Plantões - " : " Plantão - ");
                        string uni = (qtUnid > 1 ? " Unidades - " : " Unidade - " );
                        string loc = (qtLocais > 1 ? " Locais - " : " Local - ");
                        string esp = (qtEspec > 1 ? " Especialidades - " : " Especialidade - " );
                        string tpp = (qtTpPlan > 1 ? " Tipos de Plantão" : " Tipo de Plantão - ");
                        at.totaisInfos = "TOTAIS: " + qtDias + dia + qtUnid + uni + qtLocais + loc + qtEspec + esp + qtTpPlan + tpp + (comValor ? "R$" + mediaHoraPlantao.ToString("N2") + " Média/Hora" : "");
                    }

                    #endregion


                    //Parte executada apenas no último registro de todo o relatório, para preparar a string que será apresentada no total geral
                    #region Counts Gerais

                    if (countGeral == res.Count)
                    {
                        //Quantitativo de plantões em unidades distintas dentro dos parâmetros
                        int qtUnid = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                      where (CoUnidFunc != 0 ? tb159.TB03_COLABOR.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                                      && (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                      && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                      && (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                                      && (situacaoFuncional != "0" ? tb159.TB03_COLABOR.CO_SITU_COL == situacaoFuncional : 0 == 0)
                                      && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                      && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                      select new { tb159.CO_EMP_AGEND_PLANT }).Distinct().Count();

                        //Quantitativo de plantões em locais distintos dentro dos parâmetros
                        int qtLocais = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                        where (CoUnidFunc != 0 ? tb159.TB03_COLABOR.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                                        && (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                        && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                        && (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                                        && (situacaoFuncional != "0" ? tb159.TB03_COLABOR.CO_SITU_COL == situacaoFuncional : 0 == 0)
                                        && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                        && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                        select new { tb159.TB14_DEPTO.CO_DEPTO }).Distinct().Count();

                        //Quantitativo de plantões em especialidades distintas dentro dos parâmetros
                        int qtEspec = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                       where (CoUnidFunc != 0 ? tb159.TB03_COLABOR.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                                       && (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                       && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                       && (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                                       && (situacaoFuncional != "0" ? tb159.TB03_COLABOR.CO_SITU_COL == situacaoFuncional : 0 == 0)
                                       && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                       && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                       select new { tb159.CO_ESPEC_PLANT }).Distinct().Count();

                        //Quantitativo de com tipos de plantões diferentes dentro dos parâmetros
                        int qtTpPlan = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                        where (CoUnidFunc != 0 ? tb159.TB03_COLABOR.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                                        && (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                        && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                        && (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                                        && (situacaoFuncional != "0" ? tb159.TB03_COLABOR.CO_SITU_COL == situacaoFuncional : 0 == 0)
                                        && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                        && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                        select new { tb159.ID_TIPO_PLANT }).Distinct().Count();

                        //Quantitativo de com dias diferentes dentro dos parâmetros
                        int qtDias = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                      where (CoUnidFunc != 0 ? tb159.TB03_COLABOR.TB25_EMPRESA.CO_EMP == CoUnidFunc : 0 == 0)
                                      && (CoUnidPlant != 0 ? tb159.CO_EMP_AGEND_PLANT == CoUnidPlant : 0 == 0)
                                      && (coEspecPlant != 0 ? tb159.CO_ESPEC_PLANT == coEspecPlant : 0 == 0)
                                      && (coCol != 0 ? tb159.TB03_COLABOR.CO_COL == coCol : 0 == 0)
                                      && ((tb159.DT_INICIO_PREV.Day >= dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= dataIni1.Year))
                                      && ((tb159.DT_INICIO_PREV.Day <= dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= dataFim1.Year))
                                      select new { tb159.DT_INICIO_PREV }).Distinct().Count();

                        //Calcula a média da hora/plantão de todo o relatório
                        decimal mediaHoraPlantao = AUXvalMediaTotalGeral / res.Count;

                        //Concatenação das informações tratando se é plural ou não
                        string dia = (qtDias > 1 ? " Plantões - " : " Plantão - ");
                        string uni = (qtUnid > 1 ? " Unidades - " : " Unidade - ");
                        string loc = (qtLocais > 1 ? " Locais - " : " Local - ");
                        string esp = (qtEspec > 1 ? " Especialidades - " : " Especialidade - ");
                        string tpp = (qtTpPlan > 1 ? " Tipos de Plantão" : " Tipo de Plantão");
                        at.totaisInfosGerais = "TOTAIS:" + qtDias + dia + qtUnid + uni + qtLocais + loc + qtEspec + esp + qtTpPlan + tpp + (comValor ? " R$" + mediaHoraPlantao.ToString("N2") + " Média/Hora" : "");

                        //Mostra o valor total de plantões no relatório conforme o parâmetro
                        if (comValor)
                        {
                            at.valorPlantaoTotalRelatorio += totVlPlantaoTotal.ToString("N2");
                        }
                        else
                            at.valorPlantaoTotalRelatorio = " - ";
                    }

                    #endregion

                    //if (comValor == false)
                    //{
                    //    xrTableCell38.Text = xrTableCell39.Text = xrTableCell43.Text = xrTableCell44.Text = " - ";
                    //}

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
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

        public class QuadropPlantonistas
        {
            //Dados do Colaborador
            public string noCol { get; set; }
            public int CoCol { get; set; }
            public string matCol { get; set; }
            public string ColSex { get; set; }
            public string ColEspec { get; set; }
            public string ColUnidFunc { get; set; }
            public string ColSituaFunc { get; set; }
            public string ColSituaFunc_Valid
            {
                get
                {
                string s = "";
                switch(this.ColSituaFunc)
                {
                    case "ATI":
                        s = "Atividade Interna";
                        break;

                    case "ATE":
                        s = "Atividade Externa";
                        break;

                    case "FCE":
                        s = "Cedido";
                        break;

                    case "FES":
                        s = "Estagiário";
                        break;

                    case "LFR":
                        s = "Licença Funcional";
                        break;

                    case "LME":
                        s = "Atividade Médica";
                        break;

                    case "LMA":
                        s = "Atividade Maternidade";
                        break;

                    case "SUS":
                        s = "Suspenso";
                        break;

                    case "TRE":
                        s = "Treinamento";
                        break;

                    case "FER":
                        s = "Férias";
                        break;
                }
                    return s;
                }
            }
            public string tpContrato { get; set; }
            public string funCol { get; set; }
            public decimal? valorHoraColab { get; set; }
            public string valorHoraColab_Valid 
            {
                get
                {
                    //Mostra os valores apenas se tal opção foi escolhida na página de parâmetros
                    return (((this.valorHoraColab.HasValue) && (this.comValor == true)) ? this.valorHoraColab.Value.ToString() : " - ");
                }
            }
            public string valorPlantaoColab
            {
                get
                {
                    if ((this.valorHoraColab.HasValue) && (this.comValor == true))
                    {
                        decimal vlTot = this.qtHorasTPPlan_R * this.valorHoraColab.Value;
                        return vlTot.ToString();
                    }
                    else
                        return " - ";
                }
            }
            public string valorPlantaoTotalColab { get; set; }
            public string noColValid
            {
                get
                {
                    return this.matCol + " - " + ( this.noCol.Length > 30 ? this.noCol.Substring(0, 30) + "..." : this.noCol);
                }
            }
            public string totaisInfos { get; set; }

            //Dados do agendamento
            public DateTime dtAgend { get; set; }
            public string dtAgendValid
            {
                get
                {
                    return this.dtAgend.ToString("dd/MM/yy") + " - " + PrimeiraLetraMaiuscula(dtAgend.ToString("ddd", new CultureInfo("pt-BR")));
                }
            }
            public string unidade { get; set; }
            public string local { get; set; }
            public string EspecPlantao { get; set; }
            public string situacao { get; set; }
            public string situacaoValid
            {
                get
                {
                    string s = "";

                    switch (this.situacao)
                    {
                        case "A":
                            s = "Aberto";
                            break;
                        case "C":
                            s = "Cancelado";
                            break;
                        case "R":
                            s = "Realizado";
                            break;
                        case "P":
                            s = "Planejado";
                            break;
                        case "S":
                            s = "Suspenso";
                            break;
                    }

                    return s;
                }
            }
            public string localPlantao
            {
                get
                {
                    return this.unidade + " / " + this.local;
                }
            }
            public string RI { get; set; }
            public bool comValor { get; set; }
            public string valorPlantaoTotalRelatorio { get; set; }
            public string totaisInfosGerais { get; set; }

            public DateTime dataHRInicio { get; set; }
            public string dataHRInicio_Valid
            {
                get
                {
                    return this.dataHRInicio.ToString("dd/MM/yy") + " - " + this.dataHRInicio.ToString("HH:mm");
                }
            }
            public DateTime dataHRFinal { get; set; }
            public string dataHRFinal_Valid
            {
                get
                {
                    return this.dataHRFinal.ToString("dd/MM/yy") + " - " + this.dataHRFinal.ToString("HH:mm");
                }
            }

            //Dados do tipo de Plantão
            public string sglTpPlantao { get; set; }
            public int qtHorasTPPlan_R { get; set; }
            public string qtHorasTPPlan
            {
                get
                {
                    return this.qtHorasTPPlan_R.ToString().PadLeft(2, '0');
                }
            }
            public string hrIniTPPlan { get; set; }
            public string saida
            {
                get
                {
                    //Calcula as horas
                    DateTime dtAux = new DateTime(1999, 1, 1);
                    dtAux = dtAux.AddHours(int.Parse(this.hrIniTPPlan.Substring(0, 2)));
                    dtAux = dtAux.AddMinutes(int.Parse(this.hrIniTPPlan.Substring(3, 2)));
                    dtAux = dtAux.AddHours(this.qtHorasTPPlan_R);

                    return this.hrIniTPPlan + " - " + dtAux.ToString("HH:mm") + " (CH " + this.qtHorasTPPlan + ")";
                }
            }
        }
        #endregion
    }
}
