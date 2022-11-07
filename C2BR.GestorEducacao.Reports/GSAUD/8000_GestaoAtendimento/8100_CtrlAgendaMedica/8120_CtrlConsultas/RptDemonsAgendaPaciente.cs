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
    public partial class RptDemonsAgendaPaciente : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonsAgendaPaciente()
        {
            InitializeComponent();
        }

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int Operadora,
              int Plano,
              int Profissional,
              int Paciente,
              string situacao,
              string dataIni,
              string dataFim,
              string NomeFuncionalidade,
              bool comProced
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (NomeFuncionalidade == "")
                    lblTitulo.Text = "-";
                else
                    lblTitulo.Text = NomeFuncionalidade.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                // Setar o header do relatorio
                this.BaseInit(header);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= DataInical && a.DT_AGEND_HORAR <= DataFinal && a.CO_ALU == Paciente)
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                            join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR into prc
                            from tbs389 in prc.Where(a => a.TBS386_ITENS_PLANE_AVALI.CO_SITUA != "C").DefaultIfEmpty()
                            join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI into grp
                            from tbs386 in grp.DefaultIfEmpty()
                            where (Operadora != 0 ? tbs174.TB250_OPERA.ID_OPER.Equals(Operadora) : Operadora == 0
                                   && Plano != 0 ? tbs174.TB251_PLANO_OPERA.ID_PLAN.Equals(Plano) : Plano == 0
                                   && Profissional != 0 ? tbs174.CO_COL == Profissional : Profissional == 0
                                   && situacao != "" ?
                                     situacao == "QCA" ? tbs174.CO_SITUA_AGEND_HORAR == "C" && (String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) || tbs174.FL_JUSTI_CANCE == "C" || tbs174.FL_JUSTI_CANCE == "M")
                                   : situacao == "QFA" ? tbs174.CO_SITUA_AGEND_HORAR == "C" && tbs174.FL_JUSTI_CANCE == "N"
                                   : situacao == "QFJ" ? tbs174.CO_SITUA_AGEND_HORAR == "C" && tbs174.FL_JUSTI_CANCE == "S"
                                   : situacao == "QPR" ? tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONF_AGEND == "S" && (tbs174.FL_AGEND_ENCAM == "N" || tbs174.FL_AGEND_ENCAM == null)
                                   : situacao == "QEN" ? tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_CONF_AGEND == "S" && tbs174.FL_AGEND_ENCAM == "S"
                                   : situacao == "QAT" ? tbs174.CO_SITUA_AGEND_HORAR == "R" && tbs174.FL_CONF_AGEND == "S" && tbs174.FL_AGEND_ENCAM == "S"
                            /*QIN*/: tbs174.CO_SITUA_AGEND_HORAR == "R" && tbs174.FL_CONF_AGEND == "N"
                                   : 0 == 0)
                            select new Relatorio
                            {
                                NomProc = tbs386.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                CodProc = tbs386.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                                valorUnitario = tbs386.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault() != null ? tbs386.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault().VL_BASE : 0,
                                CodPac = tb07.CO_ALU,
                                Paciente = tb07.NO_ALU,
                                Sexo = tb07.CO_SEXO_ALU == "" ? "-" : tb07.CO_SEXO_ALU == null ? "-" : tb07.CO_SEXO_ALU,
                                NumAtendimento = tbs174.NU_REGIS_CONSUL,
                                DataAgend = tbs174.DT_AGEND_HORAR,
                                HoraAgend = tbs174.HR_AGEND_HORAR,
                                Operadora = tbs174.TB250_OPERA.NM_SIGLA_OPER == null ? "-" : tbs174.TB250_OPERA.NM_SIGLA_OPER,
                                Plano = tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN == null ? "-" : tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN,
                                DataInical = DataInical,
                                DataFinal = DataFinal,
                                Profissional = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                                Classificacao = tb03.CO_CLASS_PROFI,
                                sitAgn = tbs174.CO_SITUA_AGEND_HORAR,
                                flgConAgn = tbs174.FL_CONF_AGEND,
                                flgEncAgn = tbs174.FL_AGEND_ENCAM,
                                flgJusCan = tbs174.FL_JUSTI_CANCE
                            }).OrderBy(w => new { w.DataAgend, w.HoraAgend }).ToList();
                
                var res = new List<Relatorio>();

                foreach (var item in ress)
                {
                    if ((!comProced && item.CodProc != "-") || comProced)
                    {
                        item.QPA = ress.Count();
                        item.QCA = ress.Where(i => i.sitAgn == "C" && (String.IsNullOrEmpty(i.flgJusCan) || i.flgJusCan == "C" || i.flgJusCan == "M")).Count();
                        item.QFA = ress.Where(i => i.sitAgn == "C" && i.flgJusCan == "N").Count();
                        item.QFJ = ress.Where(i => i.sitAgn == "C" && i.flgJusCan == "S").Count();
                        item.QPR = ress.Where(i => i.sitAgn == "A" && i.flgConAgn == "S" && (i.flgEncAgn == "N" || String.IsNullOrEmpty(i.flgEncAgn))).Count();
                        item.QEN = ress.Where(i => i.sitAgn == "A" && i.flgConAgn == "S" && i.flgEncAgn == "S").Count();
                        item.QAT = ress.Where(i => i.sitAgn == "R" && i.flgConAgn == "S" && i.flgEncAgn == "S").Count();
                        item.QIN = ress.Where(i => i.sitAgn == "A" && i.flgConAgn == "N").Count();

                        res.Add(item);
                    }
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                {
                    bsReport.Add(item);
                }
                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public DateTime DataInical { get; set; }
            public DateTime DataFinal { get; set; }
            public string Profissional { get; set; }
            public string Paciente { get; set; }
            public string Sexo { get; set; }
            public DateTime DataAgend { get; set; }
            public string HoraAgend { get; set; }
            public string NumAtendimento { get; set; }
            public string Plano { get; set; }
            public string Operadora { get; set; }
            public int CodPac { get; set; }

            private string codProc;

            public string CodProc
            {
                get
                {
                    return !String.IsNullOrEmpty(codProc) ? codProc : "-";
                }
                set
                {
                    codProc = value;
                }
            }

            private string nomProc;

            public string NomProc
            {
                get
                {
                    return !String.IsNullOrEmpty(nomProc) ? nomProc : "** Sem Procedimento **";
                }
                set
                {
                    nomProc = value;
                }
            }
            public decimal valorUnitario { get; set; }

            public string Data 
            {
                get
                {
                    return this.DataAgend.ToShortDateString() + " " + this.HoraAgend;
                }
            }

            private string classifc;

            public string Classificacao
            {
                get
                {
                    return Funcoes.GetNomeClassificacaoFuncional(classifc, false);
                }

                set
                {
                    classifc = value;
                }
            }

            public string Situacao
            {
                get
                {
                    var s = " - ";

                    if (this.sitAgn == "C" && (String.IsNullOrEmpty(this.flgJusCan) || this.flgJusCan == "C" || this.flgJusCan == "M"))
                        s = "Cancelado";
                    else if (this.sitAgn == "C" && this.flgJusCan == "N")
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
                               
            public string sitAgn { get; set; }
            public string flgConAgn { get; set; }
            public string flgEncAgn { get; set; }
            public string flgJusCan { get; set; }

            public int QPA { get; set; }
            public int QCA { get; set; }
            public int QFA { get; set; }
            public int QFJ { get; set; }
            public int QPR { get; set; }
            public int QEN { get; set; }
            public int QAT { get; set; }
            public int QIN { get; set; }

            public decimal QPF
            {
                get
                {
                    return this.QPA - this.QFJ - this.QIN - this.QCA;
                }
            }
        }
    }
}
