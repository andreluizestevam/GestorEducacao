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
    public partial class RptPlanoPacienteProced : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptPlanoPacienteProced()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int Unidade,
              int Operadora,
              int Plano,
              int Profissional,
              string situacao,
              int paciente,
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
                {
                    lblTitulo.Text = "-";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidade.ToUpper();
                }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                // Setar o header do relatorio
                this.BaseInit(header);

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= DataInical && a.DT_AGEND_HORAR <= DataFinal)
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                            join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR into prc
                            from tbs389 in prc.Where(a => a.TBS386_ITENS_PLANE_AVALI.CO_SITUA != "C").DefaultIfEmpty()
                            join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros().Where(i => i.CO_SITUA != "C") on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI into grp
                            from tbs386 in grp.DefaultIfEmpty()
                            where (Operadora != 0 ? tbs174.TB250_OPERA.ID_OPER.Equals(Operadora) : Operadora == 0)
                                   && (Plano != 0 ? tbs174.TB251_PLANO_OPERA.ID_PLAN.Equals(Plano) : Plano == 0)
                                   && (Unidade != 0 ? tbs174.CO_EMP == Unidade : Unidade == 0)
                                   && (situacao != "" ? tb07.CO_SITU_ALU == situacao : situacao == "")
                                   && (Profissional != 0 ? tbs174.CO_COL == Profissional : Profissional == 0)
                                   && (paciente != 0 ? tbs174.CO_ALU == paciente : paciente == 0)
                            select new Relatorio
                            {
                                NomProc = tbs386.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                CodProc = tbs386.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                                valorUnitario = tbs386.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.Where(x => x.FL_STATU.Equals("A")).FirstOrDefault() != null ? tbs386.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.Where(x => x.FL_STATU.Equals("A")).FirstOrDefault().VL_BASE : 0,
                                CodPac = tb07.CO_ALU,
                                Paciente = tb07.NO_ALU,
                                Sexo = tb07.CO_SEXO_ALU == "" ? "-" : tb07.CO_SEXO_ALU == null ? "-" : tb07.CO_SEXO_ALU,
                                DataNasc = tb07.DT_NASC_ALU.Value,
                                Operadora = tbs174.TB250_OPERA.NM_SIGLA_OPER == null ? "-" : tbs174.TB250_OPERA.NM_SIGLA_OPER,
                                Plano = tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN == null ? "-" : tbs174.TB251_PLANO_OPERA.NM_SIGLA_PLAN,
                                DataInical = DataInical,
                                DataFinal = DataFinal,
                                sitAgn = tbs174.CO_SITUA_AGEND_HORAR,
                                flgConAgn = tbs174.FL_CONF_AGEND,
                                flgEncAgn = tbs174.FL_AGEND_ENCAM,
                                flgJusCan = tbs174.FL_JUSTI_CANCE
                            }).OrderBy(w => w.Paciente).ToList();

                var res = new List<Relatorio>();

                foreach (var item in ress)
                {
                    if ((!comProced && item.CodProc != "-") || comProced)
                    {
                        item.QPA = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.flgJusCan != "M").Count();
                        item.QCA = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "C" && (String.IsNullOrEmpty(i.flgJusCan) || i.flgJusCan == "C" || i.flgJusCan == "P")).Count();
                        item.QFA = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "C" && i.flgJusCan == "N").Count();
                        item.QFJ = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "C" && i.flgJusCan == "S").Count();
                        item.QPR = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "A" && i.flgConAgn == "S" && (i.flgEncAgn == "N" || String.IsNullOrEmpty(i.flgEncAgn))).Count();
                        item.QEN = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "A" && i.flgConAgn == "S" && i.flgEncAgn == "S").Count();
                        item.QAT = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "R" && i.flgConAgn == "S" && i.flgEncAgn == "S").Count();
                        item.QIN = ress.Where(i => i.CodPac == item.CodPac && i.CodProc == item.CodProc && i.sitAgn == "A" && i.flgConAgn == "N").Count();

                        res.Add(item);
                    }
                }

                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res.DistinctBy(q => new { q.CodPac, q.CodProc }))
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
            public int idProc { get; set; }
            public DateTime DataInical { get; set; }
            public DateTime DataFinal { get; set; }
            public string Paciente { get; set; }
            public string Sexo { get; set; }
            public DateTime? DataNasc { get; set; }
            public string Data
            {
                get
                {
                    if (this.DataNasc == null)
                    {
                        return "-";
                    }
                    else
                    {
                        DateTime data = Convert.ToDateTime(DataNasc);
                        return data.ToShortDateString();
                    }
                }
            }
            public string Plano { get; set; }
            public string Operadora { get; set; }
            public int CodPac { get; set; }
            public int ID_AGEND_HORAR { get; set; }

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
                    return this.QPA - this.QCA;
                }
            }

            public decimal valorUnitario { get; set; }

            public decimal ValorTotal
            {
                get
                {
                    if (this.QPF == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return this.valorUnitario * this.QPF;
                    }
                }
            }

        }
    }
}
