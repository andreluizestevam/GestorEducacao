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
    public partial class RptDemonTempoAtendProProfi : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonTempoAtendProProfi()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int Unidade,
              int Local,
              string Espec,
              int Profissional,
              string dataIni,
              string dataFim,
              string horaIni,
              string horaFim,
              string NomeFuncionalidade
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (NomeFuncionalidade == "")
                {
                    lblTitulo.Text = "DEMONSTRATIVO DE TEMPO DE ATENDIMENTO DE PACIENTES";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidade.ToUpper();
                }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                // Setar o header do relatorio
                this.BaseInit(header);

                DateTime DataIni = ConverterDataHora(dataIni, horaIni);
                DateTime DataFim = ConverterDataHora(dataFim, horaFim);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                            join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                            join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.ID_DEPTO_LOCAL_ATENDI equals tb14.CO_DEPTO
                            join tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs194.ID_AGEND_HORAR
                            where (Unidade != 0 ? tbs174.CO_EMP == Unidade : 0 == 0)
                                   && (Profissional != 0 ? tbs174.CO_COL_ATEND == Profissional : 0 == 0)
                                   && (Local != 0 ? tbs174.ID_DEPTO_LOCAL_ATENDI == Local : 0 == 0)
                                   && (Espec != "0" ? tb03.CO_CLASS_PROFI.Equals(Espec) : 0 == 0)
                                   && (!tbs174.CO_SITUA_AGEND_HORAR.Equals("C"))
                            select new Relatorio
                            {
                                NIS = tb07.NU_NIRE,
                                CoPaci = tb07.CO_ALU,
                                Paciente = tb07.NO_ALU,
                                CoCol = tb03.CO_COL,
                                RegistroProfissional = tb03.CO_MAT_COL,
                                Profissional = tb03.NO_APEL_COL,
                                Especialidade = tb03.DE_FUNC_COL,
                                ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                                DTAgenda = tbs174.DT_AGEND_HORAR,
                                HRAgenda = tbs174.HR_AGEND_HORAR,
                                DTRecep = tbs174.DT_PRESE,
                                DTEncam = tbs174.DT_ENCAM,
                                DTTriagem = tbs194.DT_PRE_ATEND,
                                DTAtendIni = tbs174.HR_ATEND_INICIO,
                                DTAtendFim = tbs174.HR_ATEND_FIM
                            }).Concat(from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                      join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                      join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                                      join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                                      join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.ID_DEPTO_LOCAL_ATENDI equals tb14.CO_DEPTO
                                      where (Unidade != 0 ? tbs174.CO_EMP == Unidade : 0 == 0)
                                             && (Profissional != 0 ? tbs174.CO_COL_ATEND == Profissional : 0 == 0)
                                             && (Local != 0 ? tbs174.ID_DEPTO_LOCAL_ATENDI == Local : 0 == 0)
                                             && (Espec != "0" ? tb03.CO_CLASS_PROFI.Equals(Espec) : 0 == 0)
                                             && (!tbs174.CO_SITUA_AGEND_HORAR.Equals("C"))
                                      select new Relatorio
                                      {
                                          NIS = tb07.NU_NIRE,
                                          CoPaci = tb07.CO_ALU,
                                          Paciente = tb07.NO_ALU,
                                          CoCol = tb03.CO_COL,
                                          RegistroProfissional = tb03.CO_MAT_COL,
                                          Profissional = tb03.NO_COL,
                                          Especialidade = tb03.DE_FUNC_COL,
                                          ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                                          DTAgenda = tbs174.DT_AGEND_HORAR,
                                          HRAgenda = tbs174.HR_AGEND_HORAR,
                                          DTRecep = tbs174.DT_PRESE,
                                          DTEncam = tbs174.DT_ENCAM,
                                          DTTriagem = (DateTime?)null,
                                          DTAtendIni = tbs174.HR_ATEND_INICIO,
                                          DTAtendFim = tbs174.HR_ATEND_FIM
                                      }).DistinctBy(w => w.ID_AGEND_HORAR).OrderBy(w => w.Profissional).ThenBy(w => w.DTAtendFim).ToList();

                //ress = ress.Where(a => a.DTAgend >= DataIni && a.DTAgend <= DataFim).ToList();

                var res = new List<Relatorio>();
                int qntPaci = 0;
                int qntItensAgendRecep = 0;
                int qntItensRecepCRisco = 0;
                int qntItensEncamCRisco = 0;
                int qntItensEncamIniAtend = 0;
                int qntItensIniAtend = 0;
                double medAgendRecep = 0;
                double medRecepCRisco = 0;
                double medEncamCRisco = 0;
                double medEncamIniAtend = 0;
                double medIniAtend = 0;

                int qntItensAgendRecepF = 0;
                int qntItensRecepCRiscoF = 0;
                int qntItensEncamCRiscoF = 0;
                int qntItensEncamIniAtendF = 0;
                int qntItensIniAtendF = 0;
                double medAgendRecepF = 0;
                double medRecepCRiscoF = 0;
                double medEncamCRiscoF = 0;
                double medEncamIniAtendF = 0;
                double medIniAtendF = 0;

                foreach (var item in ress)
                {
                    string strDTAgenda = item.DTAgenda.ToShortDateString();
                    item.DTAgend = ConverterDataHora(strDTAgenda, item.HRAgenda);
                }

                ress = ress.Where(a => a.DTAgend >= DataIni && a.DTAgend <= DataFim).ToList();
                foreach (var item in ress.DistinctBy(x => x.CoCol))
                {
                    if (res.Where(x => x.CoPaci == item.CoPaci).Count() == 0)
                        qntPaci++;
                    item.QntPaciente = qntPaci;

                    item.difTotalAgendRecep = new TimeSpan();
                    item.difTotalRecepCRisco = new TimeSpan();
                    item.difTotalEncamTriagem = new TimeSpan();
                    item.difTotalEncamAtendIni = new TimeSpan();
                    item.difTotalAtendIni = new TimeSpan();

                    foreach (var i in ress.Where(x => x.CoCol == item.CoCol))
                    {
                        item.QntAtend++;

                        if (i.difAgendRecep.HasValue)
                        {
                            item.difTotalAgendRecep += i.difAgendRecep.Value;
                        }
                        if (i.difRecepCRisco.HasValue)
                        {
                            item.difTotalRecepCRisco += i.difRecepCRisco.Value;
                        }
                        if (i.difEncamTriagem.HasValue)
                        {
                            item.difTotalEncamTriagem += i.difEncamTriagem.Value;
                        }
                        if (i.difEncamAtendIni.HasValue)
                        {
                            item.difTotalEncamAtendIni += i.difEncamAtendIni.Value;
                        }
                        if (i.difAtendIni.HasValue)
                        {
                            item.difTotalAtendIni += i.difAtendIni.Value;
                        }

                        medAgendRecep += (item.difAgendRecep.HasValue ? item.difAgendRecep.Value.TotalMinutes : 0);
                        qntItensAgendRecep += (item.difAgendRecep.HasValue ? 1 : 0);
                        medRecepCRisco += (item.difRecepCRisco.HasValue ? item.difRecepCRisco.Value.TotalMinutes : 0);
                        qntItensRecepCRisco += (item.difRecepCRisco.HasValue ? 1 : 0);
                        medEncamCRisco += (item.difEncamTriagem.HasValue ? item.difEncamTriagem.Value.TotalMinutes : 0);
                        qntItensEncamCRisco += (item.difEncamTriagem.HasValue ? 1 : 0);
                        medEncamIniAtend += (item.difEncamAtendIni.HasValue ? item.difEncamAtendIni.Value.TotalMinutes : 0);
                        qntItensEncamIniAtend += (item.difEncamAtendIni.HasValue ? 1 : 0);
                        medIniAtend += (item.difAtendIni.HasValue ? item.difAtendIni.Value.TotalMinutes : 0);
                        qntItensIniAtend += (item.difAtendIni.HasValue ? 1 : 0);
                    }

                    item.MedAgendRecep = qntItensAgendRecep != 0 ? medAgendRecep / qntItensAgendRecep : 0;
                    item.MedRecepCRisco = qntItensRecepCRisco != 0 ? medRecepCRisco / qntItensRecepCRisco : 0;
                    item.MedEncamCRisco = qntItensEncamCRisco != 0 ? medEncamCRisco / qntItensEncamCRisco : 0;
                    item.MedCEncamIniAtend = qntItensEncamIniAtend != 0 ? medEncamIniAtend / qntItensEncamIniAtend : 0;
                    item.MedIniAtend = qntItensAgendRecep != 0 ? medIniAtend / qntItensIniAtend : 0;



                    res.Add(item);

                    qntItensAgendRecep = 0;
                    qntItensRecepCRisco = 0;
                    qntItensEncamCRisco = 0;
                    qntItensEncamIniAtend = 0;
                    qntItensIniAtend = 0;
                    medAgendRecep = 0;
                    medRecepCRisco = 0;
                    medEncamCRisco = 0;
                    medEncamIniAtend = 0;
                    medIniAtend = 0;
                }

                foreach (var it in res)
                {
                    medAgendRecepF += it.MedAgendRecep > 0 && !Double.IsNaN(it.MedAgendRecep) ? it.MedAgendRecep : 0;
                    medEncamCRiscoF += it.MedEncamCRisco > 0 && !Double.IsNaN(it.MedEncamCRisco) ? it.MedEncamCRisco : 0;
                    medEncamIniAtendF += it.MedCEncamIniAtend > 0 && !Double.IsNaN(it.MedCEncamIniAtend) ? it.MedCEncamIniAtend : 0;
                    medIniAtendF += it.MedIniAtend > 0 && !Double.IsNaN(it.MedIniAtend) ? it.MedIniAtend : 0;
                    medRecepCRiscoF += it.MedRecepCRisco > 0 && !Double.IsNaN(it.MedRecepCRisco) ? it.MedRecepCRisco : 0;


                    qntItensAgendRecepF += ress.Where(x => x.CoCol == it.CoCol && !x.VMedAgendRecep.Equals("-")).ToList().Count();
                    qntItensRecepCRiscoF += ress.Where(x => x.CoCol == it.CoCol && !x.VMedRecepCRisco.Equals("-")).ToList().Count();
                    qntItensEncamCRiscoF += ress.Where(x => x.CoCol == it.CoCol && !x.VMedEncamCRisco.Equals("-")).ToList().Count();
                    qntItensEncamIniAtendF += ress.Where(x => x.CoCol == it.CoCol && !x.VMedCEncamIniAtend.Equals("-")).ToList().Count();
                    qntItensIniAtendF += ress.Where(x => x.CoCol == it.CoCol && !x.VMedIniAtend.Equals("-")).ToList().Count();

                    if (medAgendRecepF.ToString("N2").Equals("0,00") || medAgendRecepF < 0 || Double.IsNaN(medAgendRecepF))
                        it.MedAgendRecepFinal = 0;
                    else
                        if (qntItensAgendRecepF != 0)
                            it.MedAgendRecepFinal = medAgendRecepF / qntItensAgendRecepF;
                        else
                            it.MedAgendRecepFinal = medAgendRecepF;


                    if (medEncamIniAtendF.ToString("N2").Equals("0,00") || medEncamIniAtendF < 0 || Double.IsNaN(medEncamIniAtendF))
                        it.MedCEncamIniAtendFinal = 0;
                    else
                        if (qntItensEncamIniAtendF != 0)
                            it.MedCEncamIniAtendFinal = medEncamIniAtendF / qntItensEncamIniAtendF;
                        else
                            it.MedCEncamIniAtendFinal = medEncamIniAtendF;

                    if (medEncamCRiscoF.ToString("N2").Equals("0,00") || medEncamCRiscoF < 0 || Double.IsNaN(medEncamCRiscoF))
                        it.MedEncamCRiscoFinal = 0;
                    else
                        if (qntItensEncamCRiscoF != 0)
                            it.MedEncamCRiscoFinal = medEncamCRiscoF / qntItensEncamCRiscoF;
                        else
                            it.MedEncamCRiscoFinal = medEncamCRiscoF;

                    if (medIniAtendF.ToString("N2").Equals("0,00") || medIniAtendF < 0 || Double.IsNaN(medIniAtendF))
                        it.MedIniAtendFinal = 0;
                    else
                        if (qntItensIniAtendF != 0)
                            it.MedIniAtendFinal = medIniAtendF / qntItensIniAtendF;
                        else
                            it.MedIniAtendFinal = medIniAtendF;

                    if (medRecepCRiscoF.ToString("N2").Equals("0,00") || medRecepCRiscoF < 0 || Double.IsNaN(medRecepCRiscoF))
                        it.MedRecepCRiscoFinal = 0;
                    else
                        if (qntItensRecepCRiscoF != 0)
                            it.MedRecepCRiscoFinal = medRecepCRiscoF / qntItensRecepCRiscoF;
                        else
                            it.MedRecepCRiscoFinal = medRecepCRiscoF;

                    xrTableCell31.Text = it.MedAgendRecepFinal.ToString("N2");
                    xrTableCell32.Text = it.MedRecepCRiscoFinal.ToString("N2");
                    xrTableCell34.Text = it.MedEncamCRiscoFinal.ToString("N2");
                    xrTableCell40.Text = it.MedCEncamIniAtendFinal.ToString("N2");
                    xrTableCell35.Text = it.MedIniAtendFinal.ToString("N2");
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res.DistinctBy(x => x.CoCol))
                {
                    bsReport.Add(item);
                }
                return 1;

            }
            catch { return 0; }
        }

        #endregion

        public class Relatorio
        {
            public decimal? NIS { get; set; }
            public int? CoPaci { get; set; }
            public int CoCol { get; set; }
            public string Paciente { get; set; }
            public string V_Paciente
            {
                get
                {
                    if (this.NIS.HasValue)
                    {
                        return (this.Paciente.Length >= 40 ? this.NIS.Value.ToString("0000000000") + " - " + this.Paciente.Substring(0, 37) + "..." : this.NIS.Value.ToString("0000000000") + " - " + this.Paciente);
                    }
                    else
                    {
                        return (this.Paciente.Length >= 40 ? this.Paciente.Substring(0, 37) + "..." : this.Paciente);
                    }
                }
            }
            public string RegistroProfissional { get; set; }
            public string Profissional { get; set; }
            public string V_Profissional
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.RegistroProfissional))
                    {
                        return (this.Profissional.Length >= 30 ? this.RegistroProfissional + " - " + this.Profissional.Substring(0, 27) + "..." : this.RegistroProfissional + " - " + this.Profissional);
                    }
                    else
                    {
                        return (this.Profissional.Length >= 30 ? this.Profissional.Substring(0, 27) + "..." : this.Profissional);
                    }
                }
            }
            public string Especialidade { get; set; }
            public string V_Especialidade
            {
                get
                {
                    return (String.IsNullOrEmpty(this.Especialidade) ? "Sem Registro" : this.Especialidade);
                }
            }
            public int QntPaciente { get; set; }
            public int ID_AGEND_HORAR { get; set; }

            public TimeSpan? difTotalAgendRecep { get; set; }
            public string V_difTotalAgendRecep
            {
                get
                {
                    if (this.difTotalAgendRecep.HasValue)
                    {
                        return difTotalAgendRecep.Value.Hours.ToString("00") + ":" + difTotalAgendRecep.Value.Minutes.ToString("00");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public TimeSpan? difTotalRecepCRisco { get; set; }
            public string V_difTotalRecepCRisco
            {
                get
                {
                    if (this.difTotalRecepCRisco.HasValue)
                    {
                        return difTotalRecepCRisco.Value.Hours.ToString("00") + ":" + difTotalRecepCRisco.Value.Minutes.ToString("00");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public TimeSpan? difTotalEncamTriagem { get; set; }
            public string V_difTotalEncamTriagem
            {
                get
                {
                    if (this.difTotalEncamTriagem.HasValue)
                    {
                        return difTotalEncamTriagem.Value.Hours.ToString("00") + ":" + difTotalEncamTriagem.Value.Minutes.ToString("00");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public TimeSpan? difTotalEncamAtendIni { get; set; }
            public string V_difTotalEncamAtendIni
            {
                get
                {
                    if (this.difTotalEncamAtendIni.HasValue)
                    {
                        return difTotalEncamAtendIni.Value.Hours.ToString("00") + ":" + difTotalEncamAtendIni.Value.Minutes.ToString("00");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public TimeSpan? difTotalAtendIni { get; set; }
            public string V_difTotalAtendIni
            {
                get
                {
                    if (this.difTotalAtendIni.HasValue)
                    {
                        return difTotalAtendIni.Value.Hours.ToString("00") + ":" + difTotalAtendIni.Value.Minutes.ToString("00");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string DuracaoTotal
            {
                get
                {
                    TimeSpan timeTotal = new TimeSpan();

                    if (this.difTotalAgendRecep.HasValue && !this.VMedAgendRecep.Equals("-"))
                    {
                        timeTotal += this.difTotalAgendRecep.Value;
                    }
                    if (this.difTotalRecepCRisco.HasValue && !this.VMedRecepCRisco.Equals("-"))
                    {
                        timeTotal += this.difTotalRecepCRisco.Value;
                    }
                    if (this.difTotalEncamTriagem.HasValue && !this.VMedEncamCRisco.Equals("-"))
                    {
                        timeTotal += this.difTotalEncamTriagem.Value;
                    }
                    if (this.difTotalEncamAtendIni.HasValue && !this.VMedCEncamIniAtend.Equals("-"))
                    {
                        timeTotal += this.difTotalEncamAtendIni.Value;
                    }
                    if (this.difTotalAtendIni.HasValue && !this.VMedIniAtend.Equals("-"))
                    {
                        timeTotal += this.difTotalAtendIni.Value;
                    }
                    if ((timeTotal.Hours < 0 || Double.IsNaN(timeTotal.Hours)) || (timeTotal.Minutes < 0 || Double.IsNaN(timeTotal.Minutes)))
                        return "-";
                    else
                        //return timeTotal.Hours.ToString() + ":" + timeTotal.Minutes.ToString();
                        return (timeTotal.Days.ToString("00") + "d:" + timeTotal.Hours.ToString("00") + "h:" + timeTotal.Minutes.ToString("00") + "m").ToString();

                }
            }

            public double DuracaoTotalFinal
            {
                get
                {
                    TimeSpan timeTotal = new TimeSpan();

                    if (this.difTotalAgendRecep.HasValue && !this.VMedAgendRecep.Equals("-"))
                    {
                        timeTotal += this.difTotalAgendRecep.Value;
                    }
                    if (this.difTotalRecepCRisco.HasValue && !this.VMedRecepCRisco.Equals("-"))
                    {
                        timeTotal += this.difTotalRecepCRisco.Value;
                    }
                    if (this.difTotalEncamTriagem.HasValue && !this.VMedEncamCRisco.Equals("-"))
                    {
                        timeTotal += this.difTotalEncamTriagem.Value;
                    }
                    if (this.difTotalEncamAtendIni.HasValue && !this.VMedCEncamIniAtend.Equals("-"))
                    {
                        timeTotal += this.difTotalEncamAtendIni.Value;
                    }
                    if (this.difTotalAtendIni.HasValue && !this.VMedIniAtend.Equals("-"))
                    {
                        timeTotal += this.difTotalAtendIni.Value;
                    }

                    if ((timeTotal.TotalMinutes < 0 || Double.IsNaN(timeTotal.TotalMinutes)))
                        return 0;
                    else
                        return timeTotal.TotalMinutes;
                }
            }

            public TimeSpan? difAgendRecep
            {
                get
                {
                    if (this.DTRecep.HasValue)
                    {
                        return this.DTRecep.Value.Subtract(this.DTAgend);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public TimeSpan? difRecepCRisco
            {
                get
                {
                    if (this.DTRecep.HasValue && this.DTTriagem.HasValue)
                    {
                        return this.DTTriagem.Value.Subtract(this.DTRecep.Value);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public TimeSpan? difEncamTriagem
            {
                get
                {
                    if (this.DTTriagem.HasValue && this.DTEncam.HasValue)
                    {
                        return this.DTEncam.Value.Subtract(this.DTTriagem.Value);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public TimeSpan? difEncamAtendIni
            {
                get
                {
                    if (this.DTEncam.HasValue && this.DTAtendIni.HasValue)
                    {
                        return this.DTAtendIni.Value.Subtract(this.DTEncam.Value);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public TimeSpan? difAtendIni
            {
                get
                {
                    if (this.DTAtendFim.HasValue && this.DTAtendIni.HasValue)
                    {
                        return this.DTAtendFim.Value.Subtract(this.DTAtendIni.Value);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public DateTime DTAgenda { get; set; }
            public string HRAgenda { get; set; }
            public DateTime DTAgend { get; set; }
            public string DataAgend
            {
                get
                {
                    return this.DTAgend.ToString("dd/MM/yy") + " " + this.DTAgend.ToString("HH:mm");
                }
            }
            public String IntervAgendRecep
            {
                get
                {
                    if (this.difAgendRecep.HasValue)
                    {
                        return difAgendRecep.Value.Hours + ":" + difAgendRecep.Value.Minutes;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public DateTime? DTRecep { get; set; }
            public string DataRecep
            {
                get
                {
                    if (this.DTRecep.HasValue)
                    {
                        return this.DTRecep.Value.ToString("dd/MM/yy") + " " + this.DTRecep.Value.ToString("HH:mm");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public String IntervRecepCRisco
            {
                get
                {
                    if (this.difRecepCRisco.HasValue)
                    {
                        return difRecepCRisco.Value.Hours + ":" + difRecepCRisco.Value.Minutes;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public DateTime? DTEncam { get; set; }
            public string DataEncam
            {
                get
                {
                    if (this.DTEncam.HasValue)
                    {
                        return this.DTEncam.Value.ToString("dd/MM/yy") + " " + this.DTEncam.Value.ToString("HH:mm");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public String IntervEncamTriagem
            {
                get
                {
                    if (this.difEncamTriagem.HasValue)
                    {
                        return difEncamTriagem.Value.Hours + ":" + difEncamTriagem.Value.Minutes;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public DateTime? DTTriagem { get; set; }
            public string DataTriagem
            {
                get
                {
                    if (this.DTTriagem.HasValue)
                    {
                        return this.DTTriagem.Value.ToString("dd/MM/yy") + " " + this.DTTriagem.Value.ToString("HH:mm");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public String IntervEncamAtendIni
            {
                get
                {
                    if (this.difEncamAtendIni.HasValue)
                    {
                        return difEncamAtendIni.Value.Hours + ":" + difEncamAtendIni.Value.Minutes;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public DateTime? DTAtendIni { get; set; }
            public string DataAtendIni
            {
                get
                {
                    if (this.DTAtendIni.HasValue)
                    {
                        return this.DTAtendIni.Value.ToString("dd/MM/yy") + " " + this.DTAtendIni.Value.ToString("HH:mm");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public String IntervAtendIni
            {
                get
                {
                    if (this.difAtendIni.HasValue)
                    {
                        return difAtendIni.Value.Hours + ":" + difAtendIni.Value.Minutes;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public DateTime? DTAtendFim { get; set; }
            public string DataAtendFim
            {
                get
                {
                    if (this.DTAtendFim.HasValue)
                    {
                        return this.DTAtendFim.Value.ToString("dd/MM/yy") + " " + this.DTAtendFim.Value.ToString("HH:mm");
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public double MedAgendRecepFinal { get; set; }
            public double MedAgendRecep { get; set; }
            public string VMedAgendRecep
            {
                get
                {
                    if (this.MedAgendRecep.ToString("N2").Equals("0,00") || this.MedAgendRecep < 0 || Double.IsNaN(this.MedAgendRecep))
                        return "-";
                    else
                        return this.MedAgendRecep.ToString("N2");
                }
            }
            public double MedRecepCRiscoFinal { get; set; }
            public double MedRecepCRisco { get; set; }
            public string VMedRecepCRisco
            {
                get
                {
                    if (this.MedRecepCRisco.ToString("N2").Equals("0,00") || this.MedRecepCRisco < 0 || Double.IsNaN(this.MedRecepCRisco))
                        return "-";
                    else
                        return this.MedRecepCRisco.ToString("N2");
                }
            }
            public double MedEncamCRiscoFinal { get; set; }
            public double MedEncamCRisco { get; set; }
            public string VMedEncamCRisco
            {
                get
                {
                    if (this.MedEncamCRisco.ToString("N2").Equals("0,00") || this.MedEncamCRisco < 0 || Double.IsNaN(this.MedEncamCRisco))
                        return "-";
                    else
                        return this.MedEncamCRisco.ToString("N2");
                }
            }
            public double MedCEncamIniAtendFinal { get; set; }
            public double MedCEncamIniAtend { get; set; }
            public string VMedCEncamIniAtend
            {
                get
                {
                    if (this.MedCEncamIniAtend.ToString("N2").Equals("0,00") || this.MedCEncamIniAtend < 0 || Double.IsNaN(this.MedCEncamIniAtend))
                        return "-";
                    else
                        return this.MedCEncamIniAtend.ToString("N2");
                }
            }
            public double MedIniAtendFinal { get; set; }
            public double MedIniAtend { get; set; }
            public string VMedIniAtend
            {
                get
                {
                    if (this.MedIniAtend.ToString("N2").Equals("0,00") || this.MedIniAtend < 0 || Double.IsNaN(this.MedIniAtend))
                        return "-";
                    else
                        return this.MedIniAtend.ToString("N2");
                }
            }

            public int QntAtend { get; set; }
            public int QntAtendTotal { get; set; }
        }

        protected DateTime ConverterDataHora(string data, string hora)
        {
            IFormatProvider culture = new CultureInfo("en-US", true);
            string formato = "dd/MM/yyyy HH:mm";

            string dataConvert;

            dataConvert = data + " " + hora;

            DateTime Data = DateTime.ParseExact(dataConvert, formato, culture);

            return Data;
        }
    }
}
