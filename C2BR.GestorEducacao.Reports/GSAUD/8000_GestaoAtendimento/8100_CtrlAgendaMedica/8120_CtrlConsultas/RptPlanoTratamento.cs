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
    public partial class RptPlanoTratamento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptPlanoTratamento()
        {
            InitializeComponent();
        }

        public int InitReport(
              string parametros,
              string infos,
              int coEmp,
              int local,
              int Profissional,
              int Paciente,
              int planTrat,
              string dataIni,
              string dataFim,
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

                var ress = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                            join tbs458 in TBS458_TRATA_PLANO.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs458.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs458.CO_COL_CADAS equals tb03.CO_COL
                            join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs174.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                            join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                            where ((Profissional != 0 ? tbs458.CO_COL_CADAS == Profissional : Profissional == 0)
                                   && tbs174.CO_ALU == Paciente
                                   && (local != 0 ? tbs174.CO_DEPT == local : 0 == 0)
                                   && (planTrat != 0 ? tbs458.ID_TRATA_PLANO == planTrat : 0 == 0))
                            select new Relatorio
                            {
                                coAlu = tb07.CO_ALU,
                                Paciente = tb07.NO_ALU.ToUpper(),
                                SxPaci = tb07.CO_SEXO_ALU,
                                NasciPaci = tb07.DT_NASC_ALU,
                                ResPaci = tb07.NU_TELE_RESI_ALU,
                                CeluPaci = tb07.NU_TELE_CELU_ALU,
                                DTAgend = tbs174.DT_AGEND_HORAR,
                                HRAgend = tbs174.HR_AGEND_HORAR,
                                DTTrat = tbs458.DT_CADAS,
                                Procedimento = tbs356.NM_REDUZ_PROC_MEDI,
                                NUTrat = tbs458.NU_REGIS,
                                Observacao = tbs458.DE_OBSER,
                                Situa = tbs174.CO_SITUA_AGEND_HORAR.Equals("A") ? "Agendado" : tbs174.CO_SITUA_AGEND_HORAR.Equals("R") ? "Realizado" : "Cancelado",
                                Local = tb14.CO_SIGLA_DEPTO,
                                Profissional = tb03.NO_APEL_COL
                            }).OrderBy(w => w.Paciente).ToList();

                var res = new List<Relatorio>();

                ress.Where(a => a.DTAgend >= DataInical && a.DTAgend <= DataFinal);

                if (ress.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in ress)
                {
                    bsReport.Add(item);
                }
                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public int coAlu { get; set; }
            public string Paciente { get; set; }
            public string SxPaci { get; set; }
            public DateTime? NasciPaci { get; set; }
            public string IdadePaci
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.NasciPaci.HasValue)
                    {
                        int anos = DateTime.Now.Year - this.NasciPaci.Value.Year;

                        if (DateTime.Now.Month < this.NasciPaci.Value.Month || (DateTime.Now.Month == this.NasciPaci.Value.Month && DateTime.Now.Day < this.NasciPaci.Value.Day))
                            anos--;

                        idade = anos.ToString("00");
                    }
                    return idade;
                }
            }
            public string ResPaci { get; set; }
            public string CeluPaci { get; set; }
            public string DesContatPaci
            {
                get
                {
                    string contat = "";
                    if (!String.IsNullOrEmpty(this.CeluPaci) && !String.IsNullOrEmpty(this.ResPaci))
                        contat = this.ResPaci + " / " + this.CeluPaci;
                    if (String.IsNullOrEmpty(this.CeluPaci) && !String.IsNullOrEmpty(this.ResPaci))
                        contat = this.ResPaci;
                    if (!String.IsNullOrEmpty(this.CeluPaci) && String.IsNullOrEmpty(this.ResPaci))
                        contat = this.CeluPaci;
                    return contat;
                }
            }
            public string Paciente_V
            {
                get
                {
                    return this.Paciente + " (Sexo: " + this.SxPaci + " | " + "Idade: " + this.IdadePaci + (!String.IsNullOrEmpty(this.DesContatPaci) ? (" | Contato: " + this.DesContatPaci) : "") + ")";
                }
            }
            public DateTime DTAgend { get; set; }
            public string HRAgend { get; set; }
            public string DTAgend_V
            {
                get
                {
                    return this.DTAgend.ToString("dd/MM/yyyy") + " " + this.HRAgend;
                }
            }
            public DateTime DTTrat { get; set; }
            public string DTTrat_V
            {
                get
                {
                    return this.DTTrat.ToString("dd/MM/yyyy HH:mm");
                }
            }
            public string Profissional { get; set; }
            public string Procedimento { get; set; }
            public string NUTrat { get; set; }
            public string Observacao { get; set; }
            public string Situa { get; set; }
            public string Local { get; set; }
        }
    }
}
