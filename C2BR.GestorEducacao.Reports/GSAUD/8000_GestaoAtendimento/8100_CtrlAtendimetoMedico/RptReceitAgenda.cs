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
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    public partial class RptReceitAgenda : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptReceitAgenda()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(
                        string infos,
                        int coEmp,
                        int coAtendimento
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(coAtendimento);

                tbs390.TBS174_AGEND_HORARReference.Load();

                var dataIni = tbs390.TBS174_AGEND_HORAR.DT_AGEND_HORAR.AddMonths(-1);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= dataIni)
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb03_Atend in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03_Atend.CO_COL
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs390.CO_EMP_ATEND equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                           where tbs174.CO_ALU == tbs390.TBS174_AGEND_HORAR.CO_ALU
                           select new Agenda
                           {
                               //Informações do Paciente
                               Paciente = tb07.NO_ALU,
                               Nascimento = tb07.DT_NASC_ALU.HasValue ? tb07.DT_NASC_ALU.Value : new DateTime(1900, 1, 1),
                               SexoPac = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-",
                               NirePaci = tb07.NU_NIRE,

                               //Dados do atendimento 
                               Profissional = tb03.NO_COL,
                               TipoConsulta_ = tbs174.TP_AGEND_HORAR,
                               DataAgend = tbs174.DT_AGEND_HORAR,
                               HoraAgend = tbs174.HR_AGEND_HORAR,
                               sitAgn = tbs174.CO_SITUA_AGEND_HORAR,
                               flgConAgn = tbs174.FL_CONF_AGEND,
                               flgEncAgn = tbs174.FL_AGEND_ENCAM,
                               flgJusCan = tbs174.FL_JUSTI_CANCE,

                               //Informações do Médico
                               nomeMedico = tb03_Atend.NO_COL,
                               sexoMedico = tb03_Atend.CO_SEXO_COL,
                               SIGLA_ENT = tb03_Atend.CO_SIGLA_ENTID_PROFI,
                               NUMER_ENT = tb03_Atend.NU_ENTID_PROFI,
                               UF_ENT = tb03_Atend.CO_UF_ENTID_PROFI,
                               DT_ENT = tb03_Atend.DT_EMISS_ENTID_PROFI,

                               //Informações gerais
                               nomeCidade = tb904.NO_CIDADE,
                               nomeUF = tb904.CO_UF,
                               unidEmiss = tb25.NO_FANTAS_EMP,
                               dataAtend = tbs390.DT_CADAS,
                               codigoAtend = coAtendimento
                           }).OrderBy(a => a.DataAgend).ThenBy(a => a.HoraAgend).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Agenda at in res)
                {
                    //Trata a label de informações do dia
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    int dia = DateTime.Now.Day;
                    int ano = DateTime.Now.Year;
                    string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                    string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                    string data = diasemana + ", " + dia + " de " + mes + " de " + ano;

                    //Atribui algumas informações à label's no relatório
                    this.lblDoutor.Text = at.MedicoValid;

                    this.lblCRM.Text = at.ENT_CONCAT;
                    this.Parametros = at.dadosAtend;

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class Agenda
        {
            //Informações do Paciente
            public string Paciente { get; set; }
            public DateTime Nascimento { get; set; }
            public string NascPac
            {
                get
                {
                    return Nascimento.ToShortDateString();
                }
            }
            public string SexoPac { get; set; }
            public string idadePac
            {
                get
                {
                    return Funcoes.FormataDataNascimento(Nascimento);
                }
            }
            public int NirePaci { get; set; }
            public string PacienteNireValid
            {
                get
                {
                    return this.NirePaci + " - " + this.Paciente;
                }
            }

            //Dados do atendimento
            public int codigoAtend { get; set; }
            public DateTime dataAtend { get; set; }

            public DateTime DataAgend { get; set; }
            public string HoraAgend { get; set; }

            public string Data
            {
                get
                {
                    string diaSemana = this.DataAgend.ToString("ddd", new System.Globalization.CultureInfo("pt-BR"));
                    return this.DataAgend.ToString("dd/MM/yy") + " " + this.HoraAgend + " - " + diaSemana;
                }
            }

            public string Profissional { get; set; }
            public string TipoConsulta_ { get; set; }
            public string TipoConsulta
            {
                get
                {
                    string tipo = " - ";
                    switch (this.TipoConsulta_)
                    {
                        case "TO":
                            tipo = "Terapia Ocupacional";
                            break;
                        case "OU":
                            tipo = "Outros";
                            break;
                        case "NT":
                            tipo = "Nutrição";
                            break;
                        case "ES":
                            tipo = "Estética";
                            break;
                        case "PI":
                            tipo = "Piscicologia";
                            break;
                        case "FO":
                            tipo = "Fonoaudiologia";
                            break;
                        case "FI":
                            tipo = "Fisioterapia";
                            break;
                        case "EN":
                            tipo = "Enfermaria";
                            break;
                        case "AO":
                            tipo = "Atendimento Odontológico";
                            break;
                        case "AM":
                            tipo = "Atendimento Médico";
                            break;
                    }
                    return tipo;
                }
            }

            public string sitAgn { get; set; }
            public string flgConAgn { get; set; }
            public string flgEncAgn { get; set; }
            public string flgJusCan { get; set; }

            public string Situacao
            {
                get
                {
                    var s = " - ";

                    if (this.sitAgn == "C" && this.flgJusCan == "N")
                        s = "Falta";
                    else if (this.sitAgn == "C" && this.flgJusCan == "S")
                        s = "Falta Just.";
                    else if (this.sitAgn == "A" && this.flgConAgn == "S" && (this.flgEncAgn == "N" || String.IsNullOrEmpty(this.flgEncAgn)))
                        s = "Presença";
                    else if (this.sitAgn == "A" && this.flgConAgn == "S" && this.flgEncAgn == "S")
                        s = "Encaminhado";
                    else if (this.sitAgn == "R" && this.flgConAgn == "S" && this.flgEncAgn == "S")
                        s = "Atendido";
                    else if (this.sitAgn == "A" && this.flgConAgn == "N")
                        s = "Em Aberto";

                    return s;
                }
            }   
            public string dadosAtend
            {
                get
                {
                    return "(ATENDIMENTO Nº " + codigoAtend + " - Em " + this.dataAtend.ToString("dd/MM/yy") + " às " + this.dataAtend.ToString("HH:mm") + ")";
                }
            }

            //Informações do Médico
            public string nomeMedico { get; set; }
            public string CRMMedico { get; set; }
            public string sexoMedico { get; set; }
            public string MedicoValid
            {
                get
                {
                    return (this.sexoMedico == "M" ? "Dr. " : "Dra. ") + this.nomeMedico;
                }
            }
            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }

            //Informações Gerais
            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
            public string unidEmiss { get; set; }
        }
    }
}

