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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    public partial class RptDemonAtendMedic : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonAtendMedic()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              int coEspec,
                              string dataIni,
                              string dataFim,
                              int Ordenacao,
                              string periodo,
                              string coTipoOrdem,
                              bool ApenasComAtendimento,
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
                this.lblTitulo.Text = "DEMONSTRATIVO DE ATENDIMENTO MÉDICO" + " - " + periodo;

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

                //Filtra se o usuário escolheu para emitir apenas com atendimentos ou todas
                if (ApenasComAtendimento)
                {
                    var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP equals tb25.CO_EMP
                               join tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros() on tbs219.ID_ENCAM_MEDIC equals tbs195.ID_ENCAM_MEDIC into len
                               from encam in len.DefaultIfEmpty()
                               join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on encam.CO_ESPEC equals tb63.CO_ESPECIALIDADE into les
                               from espEnca in les.DefaultIfEmpty()

                               join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs219.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR into lcon
                               from consul in lcon.DefaultIfEmpty()
                               join tb63ob in TB63_ESPECIALIDADE.RetornaTodosRegistros() on consul.CO_ESPEC equals tb63ob.CO_ESPECIALIDADE into lesc
                               from espConsu in lesc.DefaultIfEmpty()
                               select new DemonGerenPlantoes
                               {
                                   NO_UNID = tb25.NO_FANTAS_EMP,
                                   NO_ESPEC = (espConsu != null ? espConsu.NO_ESPECIALIDADE : espEnca.NO_ESPECIALIDADE),
                                   CO_ESPEC = (espConsu != null ? espConsu.CO_ESPECIALIDADE : espEnca.CO_ESPECIALIDADE),
                                   CO_UNID = tb25.CO_EMP,
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,
                               }).Distinct().ToList();

                    //Classifica e ordena de acordo com o parâmetro recebido
                    switch (Ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_ESPEC).ThenBy(b => b.NO_UNID).ThenBy(w => w.QPMC).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_ESPEC).ThenByDescending(b => b.NO_UNID).ThenByDescending(w => w.QPMC).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QPMC).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QPMC).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAMC).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAMC).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QEPA).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QEPA).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QEEM).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QEEM).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QEAT).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QEAT).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAMT).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAMT).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    //Faz uma lista das especialidades geradas no relatório
                    var Espc = res.DistinctBy(w => w.CO_ESPEC);

                    int auxCount = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {

                        auxCount++;
                        at.NCL = auxCount;

                        int QPMC = 0;
                        int QEPA = 0;
                        foreach (DemonGerenPlantoes li in res)
                        {
                            QPMC += li.QPMC;
                            QEPA += li.QEPA;
                        }
                        at.TOTAL_QPMC = QPMC;
                        at.TOTAL_QEPA = QEPA;

                        CarregaCores(Ordenacao);


                        //Contabiliza os totais de diferenças, apenas quando for o último registro do RES
                        if (auxCount == res.Count)
                        {
                            int totalDifCA = 0;
                            int totalDifEM = 0;
                            int totalDifPE = 0;
                            foreach (DemonGerenPlantoes li in res)
                            {
                                totalDifCA += li.DIF_CA;
                                totalDifPE += li.DIF_PE;
                                totalDifEM += li.DIF_EM;
                            }
                            at.TOTAL_DIF_CA = totalDifCA;
                            at.TOTAL_DIF_PE = totalDifPE;
                            at.TOTAL_DIF_EM = totalDifEM;
                        }

                        bsReport.Add(at);
                    }

                    return 1;
                }
                else
                {
                    var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb25.CO_EMP equals tb63.CO_EMP
                               select new DemonGerenPlantoes
                               {
                                   NO_UNID = tb25.NO_FANTAS_EMP,
                                   NO_ESPEC = tb63.NO_ESPECIALIDADE,
                                   CO_ESPEC = tb63.CO_ESPECIALIDADE,
                                   CO_UNID = tb25.CO_EMP,
                                   dataIni1 = dataIni1,
                                   dataFim1 = dataFim1,

                               }).ToList();

                    //Classifica e ordena de acordo com o parâmetro recebido
                    switch (Ordenacao)
                    {
                        case 1:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 2:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(o => o.NO_ESPEC).ThenBy(b => b.NO_UNID).ThenBy(w => w.QPMC).ToList();
                            else
                                res = res.OrderByDescending(o => o.NO_ESPEC).ThenByDescending(b => b.NO_UNID).ThenByDescending(w => w.QPMC).ToList();
                            break;
                        case 3:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QPMC).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QPMC).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 4:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAMC).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAMC).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 5:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QEPA).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QEPA).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 6:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QEEM).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QEEM).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 7:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QEAT).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QEAT).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                        case 8:
                            if (coTipoOrdem == "C")
                                res = res.OrderBy(w => w.QAMT).ThenBy(o => o.NO_UNID).ToList();
                            else
                                res = res.OrderByDescending(w => w.QAMT).ThenByDescending(o => o.NO_UNID).ToList();
                            break;
                    }

                    // Erro: não encontrou registros
                    if (res.Count == 0)
                        return -1;

                    // Adiciona ao DataSource do Relatório
                    bsReport.Clear();

                    int auxCount = 0;
                    //decimal totperQTHT = 0;
                    //decimal totperQTDP = 0;
                    //decimal totperQTIP = 0;
                    foreach (DemonGerenPlantoes at in res)
                    {
                        auxCount++;
                        at.NCL = auxCount;

                        int QPMC = 0;
                        int QEPA = 0;
                        foreach (DemonGerenPlantoes li in res)
                        {
                            QPMC += li.QPMC;
                            QEPA += li.QEPA;
                        }
                        at.TOTAL_QPMC = QPMC;
                        at.TOTAL_QEPA = QEPA;

                        CarregaCores(Ordenacao);

                        //Contabiliza os totais de diferenças, apenas quando for o último registro do RES

                        #region
                        if (auxCount == res.Count)
                        {
                            int totalDifCA = 0;
                            int totalDifEM = 0;
                            int totalDifPE = 0;
                            foreach (DemonGerenPlantoes li in res)
                            {
                                totalDifCA += li.DIF_CA;
                                totalDifPE += li.DIF_PE;
                                totalDifEM += li.DIF_EM;
                            }
                            at.TOTAL_DIF_CA = totalDifCA;
                            at.TOTAL_DIF_PE = totalDifPE;
                            at.TOTAL_DIF_EM = totalDifEM;
                        }
                        #endregion

                        bsReport.Add(at);
                    }

                    return 1;
                }
            }
            catch { return 0; }
        }

        /// <summary>
        /// Altera as cores das colunas de acordo com os parâmetros
        /// </summary>
        /// <param name="Ordenacao"></param>
        private void CarregaCores(int Ordenacao)
        {
            //Muda a cor da coluna de acordo com a ordenação escolhida
            switch (Ordenacao)
            {
                case 1:
                    xrTableCell22.ForeColor = Color.RoyalBlue;
                    break;
                case 2:
                    xrTableCell69.ForeColor = Color.RoyalBlue;
                    break;
                case 3:
                    xrTableCell54.ForeColor = xrTableCell57.ForeColor = xrTableCell63.ForeColor = xrTableCell28.ForeColor = Color.RoyalBlue;
                    break;
                case 4:
                    xrTableCell61.ForeColor = xrTableCell47.ForeColor = xrTableCell58.ForeColor = xrTableCell52.ForeColor = Color.RoyalBlue;
                    break;
                case 5:
                    xrTableCell30.ForeColor = xrTableCell21.ForeColor = xrTableCell37.ForeColor = xrTableCell34.ForeColor = Color.RoyalBlue;
                    break;
                case 6:
                    xrTableCell15.ForeColor = xrTableCell60.ForeColor = xrTableCell23.ForeColor = xrTableCell16.ForeColor = Color.RoyalBlue;
                    break;
                case 7:
                    xrTableCell9.ForeColor = xrTableCell38.ForeColor = xrTableCell10.ForeColor = xrTableCell48.ForeColor = Color.RoyalBlue;
                    break;
                case 8:
                    xrTableCell39.ForeColor = xrTableCell40.ForeColor = xrTableCell45.ForeColor = xrTableCell44.ForeColor = Color.RoyalBlue;
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

        public class DemonGerenPlantoes
        {
            //Dados do Colaborador
            public string NO_UNID { get; set; }
            public string NO_UNID_V
            {
                get
                {
                    return this.NO_UNID.ToUpper();
                }
            }
            public string NO_DEPTO { get; set; }
            public string NO_ESPEC { get; set; }
            public string CONCAT
            {
                get
                {
                    return this.NO_UNID_V + " - " + this.NO_ESPEC;
                }
            }
            public int NCL { get; set; }

            //Dados de auxlilio para querys
            public int CO_UNID { get; set; }
            public int CO_ESPEC { get; set; }
            public DateTime dataIni1 { get; set; }
            public DateTime dataFim1 { get; set; }

            //Quantidade total de Marcações de Consulta e Percentual
            public int QPMC
            {
                get
                {
                    int qtMarCon = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                    where tbs174.CO_EMP == this.CO_UNID
                                      && tbs174.CO_ESPEC == this.CO_ESPEC
                                      && tbs174.CO_ALU != null
                                      && ((tbs174.DT_AGEND_HORAR >= this.dataIni1) && (tbs174.DT_AGEND_HORAR <= this.dataFim1))
                                    //&& ((tbs174.DT_AGEND_HORAR.Day >= this.dataIni1.Day) && (tb159.DT_INICIO_PREV.Month >= this.dataIni1.Month) && (tb159.DT_INICIO_PREV.Year >= this.dataIni1.Year))
                                    //&& ((tbs174.DT_INICIO_PREV.Day <= this.dataFim1.Day) && (tb159.DT_INICIO_PREV.Month <= this.dataFim1.Month) && (tb159.DT_INICIO_PREV.Year <= this.dataFim1.Year))
                                    select new { tbs174.ID_AGEND_HORAR }).Distinct().Count();

                    return qtMarCon;
                }
            }
            public string QPMC_V
            {
                get
                {
                    return this.QPMC.ToString().PadLeft(2, '0');
                }
            }
            public string QPMC_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QPMC > 0)
                    {
                        decimal aux1 = this.QPMC * 100;
                        decimal aux2 = aux1 / this.TOTAL_QPMC;
                        //return aux2.ToString("N1");
                        if (aux2 >= 100)
                            return decimal.Floor(aux2).ToString("N1");
                        else
                            return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Atendimento Médico Programado
            public int QAMC
            {
                get
                {
                    int qtAtendProg = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs219.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                                       where tbs219.CO_EMP == this.CO_UNID
                                       && tbs174.CO_ESPEC == this.CO_ESPEC
                                       && ((tbs219.DT_ATEND_MEDIC >= dataIni1) && (tbs219.DT_ATEND_MEDIC <= this.dataFim1))
                                       select new { tbs219.ID_ATEND_MEDIC }).Count();

                    return qtAtendProg;
                }
            }
            public string QAMC_V
            {
                get
                {
                    return this.QAMC.ToString().PadLeft(2, '0');
                }
            }
            public string QAMC_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QAMC > 0)
                    {
                        decimal aux1 = this.QAMC * 100;
                        decimal aux2 = aux1 / this.TOTAL_QAMC;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            /// <summary>
            /// Calcula a diferença entre QAMC e QPMC para saber quantas consultas não foram atendidas
            /// </summary>
            public int DIF_CA
            {
                get
                {
                    return this.QAMC - this.QPMC;
                }
            }

            ////Quantidade total de Pré-Atendimentos
            public int QEPA
            {
                get
                {
                    int qtPrAtend = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                                     where tbs194.CO_EMP == this.CO_UNID
                                        && tbs194.CO_ESPEC == this.CO_ESPEC
                                        && ((tbs194.DT_PRE_ATEND >= this.dataIni1) && (tbs194.DT_PRE_ATEND <= this.dataFim1))
                                     select new { tbs194.ID_PRE_ATEND }).Count();

                    return qtPrAtend;
                }
            }
            public string QEPA_V
            {
                get
                {
                    return this.QEPA.ToString().PadLeft(2, '0');
                }
            }
            public string QEPA_PER
            {
                get
                {
                    int horas = QEPA;

                    if (this.TOTAL_QEPA > 0)
                    {
                        //Calcula o percentual
                        decimal aux1 = horas * 100;
                        decimal aux2 = aux1 / this.TOTAL_QEPA;
                        if (aux2 >= 100)
                            return decimal.Floor(aux2).ToString("N1");
                        else
                            return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            ////Quantidade total de Encaminhamentos Médicos
            public int QEEM
            {
                get
                {
                    int qtEncam = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                   where tbs195.CO_EMP_ENCAM_MEDIC == this.CO_UNID
                                   && tbs195.CO_ESPEC == this.CO_ESPEC
                                   && ((tbs195.DT_ENCAM_MEDIC >= this.dataIni1) && (tbs195.DT_ENCAM_MEDIC <= this.dataFim1))
                                   select new { tbs195.ID_ENCAM_MEDIC }).Count();
                    return qtEncam;
                }
            }
            public string QEEM_V
            {
                get
                {
                    return this.QEEM.ToString().PadLeft(2, '0');
                }
            }
            public string QEEM_PER
            {
                get
                {
                    //Calcula o percentual de dias trabalhados dentro do período
                    if (this.TOTAL_QEEM > 0)
                    {
                        decimal aux1 = this.QEEM * 100;
                        decimal aux2 = aux1 / this.TOTAL_QEEM;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            /// <summary>
            /// Calcula a diferença entre QEEM e QEAT para saber quantos pré-atendimentos não foram encaminhados
            /// </summary>
            public int DIF_PE
            {
                get
                {
                    return this.QEEM - this.QEPA;
                }
            }

            ////Quantidade total de Atendimento Médico Emergencial
            public int QEAT
            {
                get
                {
                    int qtAtendEmer = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                                       join tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros() on tbs219.ID_ENCAM_MEDIC equals tbs195.ID_ENCAM_MEDIC
                                       where tbs219.CO_EMP == this.CO_UNID
                                       && tbs195.CO_ESPEC == this.CO_ESPEC
                                       && ((tbs219.DT_ATEND_MEDIC >= dataIni1) && (tbs219.DT_ATEND_MEDIC <= this.dataFim1))
                                       select new { tbs219.ID_ATEND_MEDIC }).Count();

                    return qtAtendEmer;
                }
            }
            public string QEAT_V
            {
                get
                {
                    return this.QEAT.ToString().PadLeft(2, '0');
                }
            }
            public string QEAT_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QEAT > 0)
                    {
                        decimal aux1 = this.QEAT * 100;
                        decimal aux2 = aux1 / this.TOTAL_QEAT;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            public int QAMT
            {
                get
                {
                    return this.QEAT + this.QAMC;
                }
            }
            public string QAMT_PER
            {
                get
                {
                    //Calcula o percentual e set no atributo correspondente
                    if (this.TOTAL_QAMT > 0)
                    {
                        decimal aux1 = this.QAMT * 100;
                        decimal aux2 = aux1 / this.TOTAL_QAMT;
                        return aux2.ToString("N1");
                    }
                    else
                        return "0";
                }
            }

            /// <summary>
            /// Calcula a diferença entre QAMC e QPMC para saber quantos encaminhamentos não foram atendidos
            /// </summary>
            public int DIF_EM
            {
                get
                {
                    return this.QEAT - this.QEEM;
                }
            }

            //Calcula a Média de Atendimento Médico Total
            public int TOTAL_QPMC { get; set; }
            public string TOTAL_QPMC_V
            {
                get
                {
                    return this.TOTAL_QPMC.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QAMC
            {
                get
                {
                    int qtAtendProg = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs219.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                                       where ((tbs219.DT_ATEND_MEDIC >= dataIni1) && (tbs219.DT_ATEND_MEDIC <= this.dataFim1))
                                       select new { tbs219.ID_ATEND_MEDIC }).Count();

                    return qtAtendProg;
                }
            }
            public string TOTAL_QAMC_V
            {
                get
                {
                    return this.TOTAL_QAMC.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_DIF_CA { get; set; }

            public int TOTAL_QEPA { get; set; }
            public string TOTAL_QEPA_V
            {
                get
                {
                    return this.TOTAL_QEPA.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_QEEM
            {
                get
                {
                    int qtEncam = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                                   where ((tbs195.DT_ENCAM_MEDIC >= this.dataIni1) && (tbs195.DT_ENCAM_MEDIC <= this.dataFim1))
                                   select new { tbs195.ID_ENCAM_MEDIC }).Count();
                    return qtEncam;
                }
            }
            public string TOTAL_QEEM_V
            {
                get
                {
                    return this.TOTAL_QEEM.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_DIF_PE { get; set; }

            public int TOTAL_QEAT
            {
                get
                {
                    int qtAtendEmer = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                                       join tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros() on tbs219.ID_ENCAM_MEDIC equals tbs195.ID_ENCAM_MEDIC
                                       where ((tbs219.DT_ATEND_MEDIC >= dataIni1) && (tbs219.DT_ATEND_MEDIC <= this.dataFim1))
                                       select new { tbs219.ID_ATEND_MEDIC }).Count();

                    return qtAtendEmer;
                }
            }
            public string TOTAL_QEAT_V
            {
                get
                {
                    return this.TOTAL_QEAT.ToString().PadLeft(2, '0');
                }
            }

            public int TOTAL_DIF_EM { get; set; }

            public int TOTAL_QAMT
            {
                get
                {
                    return this.TOTAL_QAMC + this.TOTAL_QEAT;
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
