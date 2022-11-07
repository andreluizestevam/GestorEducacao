using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraCharts;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8210_RecepcaoDeAvaliacao
{
    public partial class RptExtAgendamento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtAgendamento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                             int CoUnidade,
                             int Paciente, int Operadora, int Plano, int Categoria, string dataIni, string dataFim, string Situacao, string Tipo, string Titulo
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (Titulo == "")
                {
                    lblTitulo.Text = "Extrato de agendamento consulta de Avaliação ".ToUpper();
                }
                else
                {
                    lblTitulo.Text = Titulo.ToUpper();
                }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);
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
                var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                           join tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros() on tb07.CO_ALU equals tbs372.CO_ALU
                           //Colocar  CO_EMP o  CO_EMP_CADAS  E temporário 
                           where (CoUnidade != 0 ? tbs372.CO_EMP_CADAS == CoUnidade : 0 == 0)
                           && (Paciente != 0 ? tb07.CO_ALU == Paciente : 0 == 0)
                           && (Plano != 0 ? tbs372.TB251_PLANO_OPERA.ID_PLAN == Plano : 0 == 0)
                           && (Categoria != 0 ? tbs372.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE == Categoria : 0 == 0)
                           && (Operadora != 0 ? tbs372.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                           && (Tipo != "0" ? tbs372.FL_TIPO_AGENDA == Tipo : 0 == 0)
                            && (Situacao == "A" ? tbs372.CO_SITUA == Situacao : Situacao == "R" ? tbs372.CO_SITUA == Situacao : Situacao == "F" ? tbs372.CO_SITUA == Situacao : 0 == 0)
                           && ((tbs372.DT_CADAS >= dataIni1) && (tbs372.DT_CADAS <= dataFim1))
                           select new ExtratoAgendamento
                            {
                                DataHoraRecebe = tbs372.DT_AGEND,
                                HoraRecebe = tbs372.HR_AGEND,
                                DataCadastro = tbs372.DT_CADAS,
                                Paciente = tb07.NO_ALU.ToUpper(),
                                Nascimento = tb07.DT_NASC_ALU,
                                Neri = tb07.NU_NIRE,
                                Responsavel = tb108.NO_RESP.Length > 25 ? tb108.NO_RESP.Substring(0, 20).ToUpper() + "..." : tb108.NO_RESP.ToUpper(),
                                Psi = tbs372.CO_INDIC_PROFI_PSICO == "S" ? "PSI" : "-",
                                Fon = tbs372.CO_INDIC_PROFI_FONOA == "S" ? "FON" : "-",
                                Teo = tbs372.CO_INDIC_PROFI_TEROC == "S" ? "TEO" : "-",
                                Fis = tbs372.CO_INDIC_PROFI_FISIO == "S" ? "FIS" : "-",
                                Peg = tbs372.CO_INDIC_PROFI_PEG == "S" ? "PEG" : "-",
                                Out = tbs372.CO_INDIC_PROFI_OUTRO == "S" ? "OUT" : "-",
                                TelefoneRecebe = tb07.NU_TELE_CELU_ALU == "" ? "-" : tb07.NU_TELE_CELU_ALU,
                                NomeOperadora = tbs372.TB250_OPERA.NM_SIGLA_OPER == "" ? "-" : tbs372.TB250_OPERA.NM_SIGLA_OPER == null ? "-" : tbs372.TB250_OPERA.NM_SIGLA_OPER,
                                DataSituacaoRecebe = tbs372.DT_SITUA,
                                Operadora = tbs372.TP_CONTR_PLANO == "S" ? true : false,
                                Particular = tbs372.TP_CONTR_PARTI == "S" ? true : false,
                                Outro = tbs372.TP_CONTR_OUTRO == "S" ? true : false,
                                SiglaPlano = tbs372.TB251_PLANO_OPERA.NM_SIGLA_PLAN,
                                FLsitu = tbs372.FL_TIPO_AGENDA,
                                SitucaoRecebe = tbs372.CO_SITUA,//"A" ? "Aberto" : tbs372.CO_SITUA == "C" ? "Cancelado" : tbs372.CO_SITUA == "F" ? "Finalizado" : "-",
                                Observacoes = tbs372.DE_OBSER_NECES
                            }).ToList();

                if (res.Count == 0)
                    return -1;
                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res.OrderByDescending(w => w.FLsitu).ThenBy(w => w.DataHoraRecebe.Value).ThenBy(w => w.HoraRecebe).ThenBy(a => a.NomeOperadora).ToList())
                {
                    bsReport.Add(item);
                }
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoAgendamento
        {
            public string DataHora
            {
                get
                {
                    if (DataHoraRecebe.HasValue)
                        return DataHoraRecebe.Value.ToString("dd/MM/yy") + " - " + HoraRecebe;
                    else
                        return DataCadastro.ToString("dd/MM/yy - hh:mm");
                    /*if (DataHoraRecebe == null)
                    {
                        return DataCadastro.ToString("dd/MM/yy - hh:mm");
                    }
                    else
                    {
                        string data;

                        if (FLsitu != "L")
                            data = this.DataHoraRecebe.Value.ToString("dd/MM/yy") + " - " + this.HoraRecebe;
                        else
                            data = this.DataCadastro.ToString("dd/MM/yy - hh:mm");

                        return data;
                    }*/
                }
            }
            public string HoraRecebe { get; set; }
            public DateTime? DataHoraRecebe { get; set; }
            public DateTime DataCadastro { get; set; }

            public string Paciente { get; set; }
            public DateTime? Nascimento { get; set; }
            public int Neri { get; set; }
            public string PacienteNeri
            {
                get
                {
                    if (Paciente.Length > 25)
                    {
                        return this.Paciente.Substring(0, 20).ToUpper() + "...";
                    }
                    else
                    {
                        return this.Paciente.ToUpper();
                    }
                }
            }

            public string Responsavel { get; set; }
            public string Psi { get; set; }
            public string Fon { get; set; }
            public string Teo { get; set; }
            public string Fis { get; set; }
            public string Peg { get; set; }
            public string Out { get; set; }

            public string Observacoes { get; set; }

            public string Plano { get; set; }
            public string TipoDeContratacao
            {
                get
                {
                    if (Operadora && Outro && Particular)
                    {
                        return "Misto";
                    }
                    else if (Operadora && Outro)
                    {
                        return "Misto";
                    }
                    else if (Operadora && Particular)
                    {
                        return "Misto";
                    }
                    else if (Outro && Particular)
                    {
                        return "Misto";
                    }
                    else if (Outro)
                    {
                        return "Outros";
                    }
                    else if (Particular)
                    {
                        return "Particular";
                    }
                    else if (Operadora)
                    {
                        return string.IsNullOrEmpty(this.SiglaPlano) ? "-" : this.SiglaPlano;
                    }
                    return "-";
                }
            }
            public string TelefoneRecebe { get; set; }
            public string Telefone
            {
                get
                {

                    return Funcoes.Format(TelefoneRecebe, TipoFormat.Telefone);

                }

            }
            public string NomeOperadora { get; set; }
            public DateTime DataSituacaoRecebe { get; set; }
            public string DataSituacao
            {
                get
                {
                    return this.DataSituacaoRecebe.ToString("dd/MM/yy");


                }
            }

            public bool Operadora { get; set; }
            public bool Particular { get; set; }
            public bool Outro { get; set; }
            public string SitucaoRecebe { get; set; }
            public string FLsitu { get; set; }

            public string Situacao
            {

                get
                {
                    if (FLsitu == "L" && SitucaoRecebe == "A")
                        return "LE";// em Aberto";
                    else if (FLsitu == "L" && SitucaoRecebe == "C")
                        return "LE";// Cancelada";
                    else if (FLsitu == "P" && SitucaoRecebe == "A")
                        return "PR";// em Aberto";
                    else if (FLsitu == "P" && SitucaoRecebe == "R")
                        return "PR";// Relizada";
                    else if (FLsitu == "P" && SitucaoRecebe == "C")
                        return "PR";// Cancelada";
                    else if (SitucaoRecebe == "A")
                        return "AV";// em Aberto";
                    else if (SitucaoRecebe == "R")
                        return "AV";// Relizada";
                    else if (SitucaoRecebe == "C")
                        return "AV";// Cancelada";
                    else
                        return " - ";

                }
            }
            public string SiglaPlano { get; set; }

        }

    }
}
