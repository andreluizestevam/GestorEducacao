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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptMapaAtendColab : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMapaAtendColab()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int CoUnidade,
                              string ClassFuncio,
                              int CoProfissional,
                              string dataIniString,
                              string dataFimString,
                              string Titulo )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (Titulo == "")
                    lblTitulo.Text = "-";
                else
                    lblTitulo.Text = Titulo.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                DateTime dtIni;
                DateTime.TryParse(dataIniString, out dtIni);
                DateTime dtFim;
                DateTime.TryParse(dataFimString, out dtFim);
                int Dias = DateTime.DaysInMonth(dtIni.Year, dtIni.Month);
                // Setar o header do relatorio
                this.BaseInit(header);
                Calendar c = new GregorianCalendar();
                //Coleta os pacientes que tiverem agendamentos nos parâmetros informados
                #region Pacientes
                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb21 in TB21_TIPOCAL.RetornaTodosRegistros() on tb03.CO_TPCAL equals tb21.CO_TPCAL into l1
                           from IItipopgto in l1.DefaultIfEmpty()
                           where (CoUnidade != 0 ? tbs174.CO_EMP == CoUnidade : 0 == 0)
                           && (ClassFuncio != "0" ? tb03.CO_CLASS_PROFI == ClassFuncio : 0 == 0)
                           && (CoProfissional != 0 ? tb03.CO_COL == CoProfissional : 0 == 0)
                           && (tbs174.DT_AGEND_HORAR >= dtIni)
                           && (tbs174.DT_AGEND_HORAR <= dtFim)
                           select new Relatorio
                           {
                               coCol = tb03.CO_COL,
                               Colaborador_R = tb03.NO_COL,
                               Data = tbs174.DT_AGEND_HORAR,
                               coProfi = tb03.CO_CLASS_PROFI,
                               tipoPgto = IItipopgto.CO_SIGLA_TPCAL,
                               valorPgto = tb03.VL_SALAR_COL,
                               tcDt01 = 01,
                               tcDt02 = 02,
                               tcDt03 = 03,
                               tcDt04 = 04,
                               tcDt05 = 05,
                               tcDt06 = 06,
                               tcDt07 = 07,
                               tcDt08 = 08,
                               tcDt09 = 09,
                               tcDt10 = 10,
                               tcDt11 = 11,
                               tcDt12 = 12,
                               tcDt13 = 13,
                               tcDt14 = 14,
                               tcDt15 = 15,
                               tcDt16 = 16,
                               tcDt17 = 17,
                               tcDt18 = 18,
                               tcDt19 = 19,
                               tcDt20 = 20,
                               tcDt21 = 21,
                               tcDt22 = 22,
                               tcDt23 = 23,
                               tcDt24 = 24,
                               tcDt25 = 25,
                               tcDt26 = 26,
                               tcDt27 = 27,
                               tcDt28 = 28,
                               tcDt29 = 29,
                               tcDt30 = 30,
                               tcDt31 = 31,

                               st01 = "-",
                               st02 = "-",
                               st03 = "-",
                               st04 = "-",
                               st05 = "-",
                               st06 = "-",
                               st07 = "-",
                               st08 = "-",
                               st09 = "-",
                               st10 = "-",
                               st11 = "-",
                               st12 = "-",
                               st13 = "-",
                               st14 = "-",
                               st15 = "-",
                               st16 = "-",
                               st17 = "-",
                               st18 = "-",
                               st19 = "-",
                               st20 = "-",
                               st21 = "-",
                               st22 = "-",
                               st23 = "-",
                               st24 = "-",
                               st25 = "-",
                               st26 = "-",
                               st27 = "-",
                               st28 = "-",
                               st29 = "-",
                               st30 = "-",
                           }).DistinctBy(a => a.coCol).OrderBy(a => a.Colaborador).ToList();

                #endregion

                //Se o período escolhido for do mesmo mês, então trata os finais de semana e feriado
                if (dtIni.Month == dtFim.Month)
                {
                    #region Destaca os dias que caírem nos sábados ou domingos                    
                    for (int i = 1; i <= Dias; i++)
                    {
                        DateTime dt = new DateTime(dtIni.Year, dtIni.Month, i);
                        //Se tem algum feriado no mês e dia em questão
                        bool temFeriado = (TBS376_AGENDA_FERIADOS.RetornaTodosRegistros().Where(w => w.DT_FERIA.Day == dt.Day
                            && w.DT_FERIA.Month == dt.Month && w.CO_SITUA == "A").Any());

                        switch (dt.Day)
                        {
                            case 1:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr1.BackColor = Color.LightGray;

                                break;
                            case 2:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr2.BackColor = Color.LightGray;

                                break;
                            case 3:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr3.BackColor = Color.LightGray;

                                break;
                            case 4:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr4.BackColor = Color.LightGray;

                                break;
                            case 5:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr5.BackColor = Color.LightGray;

                                break;
                            case 6:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr6.BackColor = Color.LightGray;

                                break;
                            case 7:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr7.BackColor = Color.LightGray;

                                break;
                            case 8:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr8.BackColor = Color.LightGray;

                                break;
                            case 9:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr9.BackColor = Color.LightGray;

                                break;
                            case 10:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr10.BackColor = Color.LightGray;

                                break;
                            case 11:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr11.BackColor = Color.LightGray;

                                break;
                            case 12:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr12.BackColor = Color.LightGray;

                                break;
                            case 13:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr13.BackColor = Color.LightGray;

                                break;
                            case 14:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr14.BackColor = Color.LightGray;

                                break;
                            case 15:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr15.BackColor = Color.LightGray;

                                break;
                            case 16:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr16.BackColor = Color.LightGray;

                                break;
                            case 17:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr17.BackColor = Color.LightGray;

                                break;
                            case 18:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr18.BackColor = Color.LightGray;

                                break;
                            case 19:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr19.BackColor = Color.LightGray;

                                break;
                            case 20:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr20.BackColor = Color.LightGray;

                                break;
                            case 21:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr21.BackColor = Color.LightGray;

                                break;
                            case 22:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr22.BackColor = Color.LightGray;

                                break;
                            case 23:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr23.BackColor = Color.LightGray;

                                break;
                            case 24:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr24.BackColor = Color.LightGray;

                                break;
                            case 25:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr25.BackColor = Color.LightGray;

                                break;
                            case 26:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr26.BackColor = Color.LightGray;

                                break;
                            case 27:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr27.BackColor = Color.LightGray;

                                break;
                            case 28:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr28.BackColor = Color.LightGray;

                                break;
                            case 29:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr29.BackColor = Color.LightGray;

                                break;
                            case 30:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr30.BackColor = Color.LightGray;

                                break;
                            case 31:
                                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || temFeriado)
                                    xr31.BackColor = Color.LightGray;

                                break;
                            default:
                                break;
                        }
                    }
                    #endregion
                }

                decimal ValorTotalGeral = 0;

                #region Variáveis Totais dos Dias

                int aux1, aux2, aux3, aux4, aux5, aux6, aux7, aux8, aux9, aux10, aux11, aux12, aux13, aux14, aux15, aux16, aux17, aux18, aux19, aux20, aux21, aux22, aux23, aux24, aux25, aux26, aux27, aux28, aux29, aux30, aux31, auxCount;
                aux1 = aux2 = aux3 = aux4 = aux5 = aux6 = aux7 = aux8 = aux9 = aux10 = aux11 = aux12 = aux13 = aux14 = aux15 = aux16 = aux17 = aux18 = aux19 = aux20 = aux21 = aux22 = aux23 = aux24 = aux25 = aux26 = aux27 = aux28 = aux29 = aux30 = aux31 = auxCount = 0;
                #endregion

                //Para cada paciente encontrado na listagem
                foreach (var item in res)
                {
                    auxCount++;
                    item.TotalF = 0;
                    item.TotalFJ = 0;
                    item.TotalPR = 0;
                    item.QTD = 0;

                    #region Lista de agendamentos
                    //Coleta a lista de agendamentos do profissional para o período informado
                    var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                                where (CoUnidade != 0 ? tbs174.CO_EMP == CoUnidade : 0 == 0)
                                && (ClassFuncio != "0" ? tb03.CO_CLASS_PROFI == ClassFuncio : 0 == 0)
                                && (CoProfissional != 0 ? tb03.CO_COL == CoProfissional : 0 == 0)
                                && tb03.CO_COL == item.coCol
                                && (tbs174.DT_AGEND_HORAR >= dtIni)
                                && (tbs174.DT_AGEND_HORAR <= dtFim)
                                select new
                                {
                                    IdAgenda = tbs174.ID_AGEND_HORAR,
                                    coCol = tb03.CO_COL,
                                    Data = tbs174.DT_AGEND_HORAR,
                                    Status = tbs174.FL_CONF_AGEND,
                                    Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                    cancelJustif = tbs174.FL_JUSTI_CANCE,
                                }).ToList();

                    #endregion

                    decimal valorTotalColab = 0;

                    #region Laço para cada Dia do Mês

                    //Para cada dia disponível no mês
                    for (int i = 1; i < Dias; i++)
                    {
                        //Se tiver qualquer agendamento do profissional em contexto para o dia
                        if (ress.Where(w => w.Data.Day == i).Any())
                        {
                            //Cria o objeto do registro para facilitar
                            var obPac = new ObjetoColaborador();
                            obPac.IdAgenda = ress.Where(w => w.Data.Day == i).FirstOrDefault().IdAgenda;
                            obPac.coCol = ress.Where(w => w.Data.Day == i).FirstOrDefault().coCol;
                            obPac.Status = ress.Where(w => w.Data.Day == i).FirstOrDefault().Status;
                            obPac.Data = ress.Where(w => w.Data.Day == i).FirstOrDefault().Data;
                            obPac.Situacao = ress.Where(w => w.Data.Day == i).FirstOrDefault().Situacao;

                            string Status;
                            if (ress.Where(w => w.Data.Day == i && w.Status == "S").Any()) //Se tiver algum, apresenta a quantidade
                                Status = ress.Where(w => w.Data.Day == i && w.Status == "S").Count().ToString().PadLeft(2, '0');
                            else //Se não tiver nenhum, apresenta A
                                Status = "A";

                            //De acordo com o dia, seta na coluna correspondente e soma a quantidade de presenças quando o forem
                            #region Seta Informações

                            int aux = 0;
                            switch (i)
                            {
                                case 1:
                                    item.st01 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux1 += aux;
                                    break;
                                case 2:
                                    item.st02 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux2 += aux;
                                    break;
                                case 3:
                                    item.st03 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux3 += aux;
                                    break;
                                case 4:
                                    item.st04 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux4 += aux;
                                    break;
                                case 5:
                                    item.st05 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux5 += aux;
                                    break;
                                case 6:
                                    item.st06 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux6 += aux;
                                    break;
                                case 7:
                                    item.st07 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux7 += aux;
                                    break;
                                case 8:
                                    item.st08 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux8 += aux;
                                    break;
                                case 9:
                                    item.st09 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux9 += aux;
                                    break;
                                case 10:
                                    item.st10 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux10 += aux;
                                    break;
                                case 11:
                                    item.st11 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux11 += aux;
                                    break;
                                case 12:
                                    item.st12 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux12 += aux;
                                    break;
                                case 13:
                                    item.st13 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux13 += aux;
                                    break;
                                case 14:
                                    item.st14 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux14 += aux;
                                    break;
                                case 15:
                                    item.st15 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux15 += aux;
                                    break;
                                case 16:
                                    item.st16 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux16 += aux;
                                    break;
                                case 17:
                                    item.st17 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux17 += aux;
                                    break;
                                case 18:
                                    item.st18 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux18 += aux;
                                    break;
                                case 19:
                                    item.st19 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux19 += aux;
                                    break;
                                case 20:
                                    item.st20 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux20 += aux;
                                    break;
                                case 21:
                                    item.st21 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux21 += aux;
                                    break;
                                case 22:
                                    item.st22 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux22 += aux;
                                    break;
                                case 23:
                                    item.st23 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux23 += aux;
                                    break;
                                case 24:
                                    item.st24 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux24 += aux;
                                    break;
                                case 25:
                                    item.st25 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux25 += aux;
                                    break;
                                case 26:
                                    item.st26 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux26 += aux;
                                    break;
                                case 27:
                                    item.st27 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux27 += aux;
                                    break;
                                case 28:
                                    item.st28 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux28 += aux;
                                    break;
                                case 29:
                                    item.st29 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux29 += aux;
                                    break;
                                case 30:
                                    item.st30 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux30 += aux;
                                    break;
                                case 31:
                                    item.st31 = Status;
                                    if (int.TryParse(Status, out aux))
                                        aux31 += aux;
                                    break;
                                default:
                                    break;
                            }

                            #endregion
                        }
                    }
                    #endregion

                    //Cancelamento
                    item.TotalF = ress.Where(w => w.Situacao == "C" && w.cancelJustif == "N").Count();
                    //Cancelamento justificado
                    item.TotalFJ = ress.Where(w => w.Situacao == "C" && w.cancelJustif == "S").Count();
                    //Presenças
                    item.TotalPR = ress.Where(w => w.Status == "S").Count();
                    //Quantidade total somando falta não justificada e presença até 31/12/2015
                    //Nova regra soma também as faltas justificadas
                    if (item.Data < new DateTime(2016, 1, 1))
                        item.QTD = item.TotalF + item.TotalPR;
                    else
                        item.QTD = item.TotalF + item.TotalPR + item.TotalFJ;

                    switch (item.tipoPgto)
                    {
                        case "T":
                            decimal vlaux = (decimal)(item.QTD * item.valorPgto ?? 0);
                            ValorTotalGeral += vlaux; // Soma ao valor total geral
                            valorTotalColab += vlaux; //Soma ao valor total do colaborador
                            break;
                        default:
                            break;
                    }
                    item.ValorTotal = valorTotalColab.ToString("N2");

                    //Executa esse bloco apenas na última iteração do laço
                    if (res.Count == auxCount)
                    {
                        item.aux1 = aux1.ToString("00");
                        item.aux2 = aux2.ToString("00");
                        item.aux3 = aux3.ToString("00");
                        item.aux4 = aux4.ToString("00");
                        item.aux5 = aux5.ToString("00");
                        item.aux6 = aux6.ToString("00");
                        item.aux7 = aux7.ToString("00");
                        item.aux8 = aux8.ToString("00");
                        item.aux9 = aux9.ToString("00");

                        item.aux10 = aux10.ToString("00");
                        item.aux11 = aux11.ToString("00");
                        item.aux12 = aux12.ToString("00");
                        item.aux13 = aux13.ToString("00");
                        item.aux14 = aux14.ToString("00");
                        item.aux15 = aux15.ToString("00");
                        item.aux16 = aux16.ToString("00");
                        item.aux17 = aux17.ToString("00");
                        item.aux18 = aux18.ToString("00");
                        item.aux19 = aux19.ToString("00");

                        item.aux20 = aux20.ToString("00");
                        item.aux21 = aux21.ToString("00");
                        item.aux22 = aux22.ToString("00");
                        item.aux23 = aux23.ToString("00");
                        item.aux24 = aux24.ToString("00");
                        item.aux25 = aux25.ToString("00");
                        item.aux26 = aux26.ToString("00");
                        item.aux27 = aux27.ToString("00");
                        item.aux28 = aux28.ToString("00");
                        item.aux29 = aux29.ToString("00");

                        item.aux30 = aux30.ToString("00");
                        item.aux31 = aux31.ToString("00");
                    }
                }

                if (res.Count == 0)
                    return -1;

                xrTableCell16.Text = ValorTotalGeral.ToString("N2");

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                    bsReport.Add(item);

                return 1;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private decimal CalcularPreencherValoresTabelaECalculado(int idProc, int? idOper, int? idPlan)
        {
            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (idOper.HasValue)
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                Funcoes.ValoresProcedimentosMedicos ob = Funcoes.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), 0, 0);

                return ob.VL_CALCULADO;
            }
            else
                return 0;
        }

        #endregion

        public class ObjetoColaborador
        {
            public int IdAgenda { get; set; }
            public int coCol { get; set; }
            public string Status { get; set; }
            public string Situacao { get; set; }
            public DateTime Data { get; set; }
        }

        public class Relatorio
        {
            public string tipoPgto { get; set; }
            public double? valorPgto { get; set; }
            public string tpContrato
            {
                get
                {
                    //Se tiver operadora é com plano, se não, é particular
                    return (this.idOper.HasValue ? "PL" : "PA");
                }
            }
            public int? idOper { get; set; }
            public int? idPlan { get; set; }
            public int TotalF { get; set; }
            public int TotalFJ { get; set; }
            public int TotalPR { get; set; }
            public int QTD { get; set; }
            public string Colaborador_R { get; set; }
            public string Colaborador
            {
                get
                {
                    return (this.Colaborador_R.Length > 28 ? this.Colaborador_R.Substring(0, 28) + "..." : this.Colaborador_R);
                }
            }
            public string coProfi { get; set; }
            public string classProfi
            {
                get
                {
                    return Funcoes.GetNomeClassificacaoFuncional(this.coProfi, true);
                }
            }
            public int coCol { get; set; }
            public int Dia { get; set; }
            public string Status { get; set; }
            public DateTime Data { get; set; }
            public string ValorTotal { get; set; }

            #region Variáveis das 31 colunas de datas
            public int tcDt01 { get; set; }
            public int tcDt02 { get; set; }
            public int tcDt03 { get; set; }
            public int tcDt04 { get; set; }
            public int tcDt05 { get; set; }
            public int tcDt06 { get; set; }
            public int tcDt07 { get; set; }
            public int tcDt08 { get; set; }
            public int tcDt09 { get; set; }
            public int tcDt10 { get; set; }
            public int tcDt11 { get; set; }
            public int tcDt12 { get; set; }
            public int tcDt13 { get; set; }
            public int tcDt14 { get; set; }
            public int tcDt15 { get; set; }
            public int tcDt16 { get; set; }
            public int tcDt17 { get; set; }
            public int tcDt18 { get; set; }
            public int tcDt19 { get; set; }
            public int tcDt20 { get; set; }
            public int tcDt21 { get; set; }
            public int tcDt22 { get; set; }
            public int tcDt23 { get; set; }
            public int tcDt24 { get; set; }
            public int tcDt25 { get; set; }
            public int tcDt26 { get; set; }
            public int tcDt27 { get; set; }
            public int tcDt28 { get; set; }
            public int tcDt29 { get; set; }
            public int tcDt30 { get; set; }
            public int tcDt31 { get; set; }

            #endregion

            #region Variáveis das 31 colunas
            public string st01 { get; set; }
            public string st02 { get; set; }
            public string st03 { get; set; }
            public string st04 { get; set; }
            public string st05 { get; set; }
            public string st06 { get; set; }
            public string st07 { get; set; }
            public string st08 { get; set; }
            public string st09 { get; set; }
            public string st10 { get; set; }
            public string st11 { get; set; }
            public string st12 { get; set; }
            public string st13 { get; set; }
            public string st14 { get; set; }
            public string st15 { get; set; }
            public string st16 { get; set; }
            public string st17 { get; set; }
            public string st18 { get; set; }
            public string st19 { get; set; }
            public string st20 { get; set; }
            public string st21 { get; set; }
            public string st22 { get; set; }
            public string st23 { get; set; }
            public string st24 { get; set; }
            public string st25 { get; set; }
            public string st26 { get; set; }
            public string st27 { get; set; }
            public string st28 { get; set; }
            public string st29 { get; set; }
            public string st30 { get; set; }
            public string st31 { get; set; }
            #endregion

            #region Variáveis 31 das colunas de Totais

            public string aux1 { get; set; }
            public string aux2 { get; set; }
            public string aux3 { get; set; }
            public string aux4 { get; set; }
            public string aux5 { get; set; }
            public string aux6 { get; set; }
            public string aux7 { get; set; }
            public string aux8 { get; set; }
            public string aux9 { get; set; }
            public string aux10 { get; set; }
            public string aux11 { get; set; }
            public string aux12 { get; set; }
            public string aux13 { get; set; }
            public string aux14 { get; set; }
            public string aux15 { get; set; }
            public string aux16 { get; set; }
            public string aux17 { get; set; }
            public string aux18 { get; set; }
            public string aux19 { get; set; }
            public string aux20 { get; set; }
            public string aux21 { get; set; }
            public string aux22 { get; set; }
            public string aux23 { get; set; }
            public string aux24 { get; set; }
            public string aux25 { get; set; }
            public string aux26 { get; set; }
            public string aux27 { get; set; }
            public string aux28 { get; set; }
            public string aux29 { get; set; }
            public string aux30 { get; set; }
            public string aux31 { get; set; }

            #endregion
        }
    }
}
