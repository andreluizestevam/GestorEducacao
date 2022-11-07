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
    public partial class RptDemonTempoAtendOdontoPaci : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonTempoAtendOdontoPaci()
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
                                Profissional = tb03.NO_APEL_COL,
                                Especialidade = tb63.NO_SIGLA_ESPECIALIDADE,
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
                                          Profissional = tb03.NO_APEL_COL,
                                          Especialidade = tb63.NO_SIGLA_ESPECIALIDADE,
                                          ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                                          DTAgenda = tbs174.DT_AGEND_HORAR,
                                          HRAgenda = tbs174.HR_AGEND_HORAR,
                                          DTRecep = tbs174.DT_PRESE,
                                          DTEncam = tbs174.DT_ENCAM,
                                          DTTriagem = (DateTime?)null,
                                          DTAtendIni = tbs174.HR_ATEND_INICIO,
                                          DTAtendFim = tbs174.HR_ATEND_FIM
                                      }).DistinctBy(w => w.ID_AGEND_HORAR).OrderBy(w => w.Paciente).ThenBy(w => w.Profissional).ThenBy(w => w.DTAtendFim).ToList();

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

                foreach (var item in ress)
                {
                    string strDTAgenda = item.DTAgenda.ToShortDateString();
                    item.DTAgend = ConverterDataHora(strDTAgenda, item.HRAgenda);
                }

                ress = ress.Where(a => a.DTAgend >= DataIni && a.DTAgend <= DataFim).ToList();
                foreach (var item in ress)
                {
                    if (res.Where(x => x.CoPaci == item.CoPaci).Count() == 0)
                        qntPaci++;
                    item.QntPaciente = qntPaci;                    

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

                    res.Add(item);
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                {
                    item.MedAgendRecep = qntItensAgendRecep != 0 ? medAgendRecep / qntItensAgendRecep : 0;
                    item.MedRecepCRisco = qntItensRecepCRisco != 0 ? medRecepCRisco / qntItensRecepCRisco : 0;
                    item.MedEncamCRisco = qntItensEncamCRisco != 0 ? medEncamCRisco / qntItensEncamCRisco : 0;
                    item.MedCEncamIniAtend = qntItensEncamIniAtend != 0 ? medEncamIniAtend / qntItensEncamIniAtend : 0;
                    item.MedIniAtend = qntItensAgendRecep != 0 ? medIniAtend / qntItensIniAtend : 0;

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
            public string Profissional { get; set; }
            public string Especialidade { get; set; }
            public int QntPaciente { get; set; }
            public int ID_AGEND_HORAR { get; set; }

            public TimeSpan? difAgendRecep
            {
                get
                {
                    if (this.DTRecep.HasValue)
                    {
                        return (DateTime)this.DTRecep - this.DTAgend;
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
                        return (DateTime)this.DTTriagem - (DateTime)this.DTRecep;
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
                        return (DateTime)this.DTEncam - this.DTTriagem;
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
                        return (DateTime)this.DTAtendIni - this.DTEncam;
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
                        return (DateTime)this.DTAtendFim - this.DTAtendIni;
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
            public string DuracaoTotal
            {
                get
                {
                    TimeSpan timeTotal = new TimeSpan();
                    TimeSpan intervalo1;
                    TimeSpan intervalo2;
                    TimeSpan intervalo3;
                    TimeSpan intervalo4;
                    TimeSpan intervalo5;
                    if (this.DTAgend != null && this.DTRecep.HasValue)
                    {
                        intervalo1 = (DateTime)this.DTRecep - this.DTAgend;
                        timeTotal = timeTotal + intervalo1;
                    }
                    if (this.DTRecep.HasValue && this.DTEncam.HasValue)
                    {
                        intervalo2 = (DateTime)this.DTEncam - (DateTime)this.DTRecep;
                        timeTotal = timeTotal + intervalo2;
                    }
                    if (this.DTEncam.HasValue && this.DTTriagem.HasValue)
                    {
                        intervalo3 = (DateTime)this.DTTriagem - (DateTime)this.DTEncam;
                        timeTotal = timeTotal + intervalo3;
                    }
                    if (this.DTTriagem.HasValue && this.DTAtendIni.HasValue)
                    {
                        intervalo4 = (DateTime)this.DTAtendIni - (DateTime)this.DTTriagem;
                        timeTotal = timeTotal + intervalo4;
                    }
                    if (this.DTAtendIni.HasValue && this.DTAtendFim.HasValue)
                    {
                        intervalo5 = (DateTime)this.DTAtendFim - (DateTime)this.DTAtendIni;
                        timeTotal = timeTotal + intervalo5;
                    }
                    return (timeTotal.Hours + ":" + timeTotal.Minutes).ToString();
                }
            }
            public double DuracaoTotalFinal
            {
                get
                {
                    TimeSpan timeTotal = new TimeSpan();
                    TimeSpan intervalo1;
                    TimeSpan intervalo2;
                    TimeSpan intervalo3;
                    TimeSpan intervalo4;
                    TimeSpan intervalo5;
                    if (this.DTAgend != null && this.DTRecep.HasValue)
                    {
                        intervalo1 = (DateTime)this.DTRecep - this.DTAgend;
                        timeTotal = timeTotal + intervalo1;
                    }
                    if (this.DTRecep.HasValue && this.DTEncam.HasValue)
                    {
                        intervalo2 = (DateTime)this.DTEncam - (DateTime)this.DTRecep;
                        timeTotal = timeTotal + intervalo2;
                    }
                    if (this.DTEncam.HasValue && this.DTTriagem.HasValue)
                    {
                        intervalo3 = (DateTime)this.DTTriagem - (DateTime)this.DTEncam;
                        timeTotal = timeTotal + intervalo3;
                    }
                    if (this.DTTriagem.HasValue && this.DTAtendIni.HasValue)
                    {
                        intervalo4 = (DateTime)this.DTAtendIni - (DateTime)this.DTTriagem;
                        timeTotal = timeTotal + intervalo4;
                    }
                    if (this.DTAtendIni.HasValue && this.DTAtendFim.HasValue)
                    {
                        intervalo5 = (DateTime)this.DTAtendFim - (DateTime)this.DTAtendIni;
                        timeTotal = timeTotal + intervalo5;
                    }
                    return timeTotal.TotalMinutes;
                }
            }
            public double MedAgendRecep { get; set; }
            public string VMedAgendRecep
            {
                get
                {
                    return this.MedAgendRecep.ToString("N2") + "m";
                }
            }
            public double MedRecepCRisco { get; set; }
            public string VMedRecepCRisco
            {
                get
                {
                    return this.MedRecepCRisco.ToString("N2") + "m";
                }
            }
            public double MedEncamCRisco { get; set; }
            public string VMedEncamCRisco
            {
                get
                {
                    return this.MedEncamCRisco.ToString("N2") + "m";
                }
            }
            public double MedCEncamIniAtend { get; set; }
            public string VMedCEncamIniAtend
            {
                get
                {
                    return this.MedCEncamIniAtend.ToString("N2") + "m";
                }
            }
            public double MedIniAtend { get; set; }
            public string VMedIniAtend
            {
                get
                {
                    return this.MedIniAtend.ToString("N2") + "m";
                }
            }
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
