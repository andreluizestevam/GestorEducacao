using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptRecebAgendProced : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRecebAgendProced()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string infos,
              string parametros,
              int coEmp,
              int Profissional,
              int Local,
              string dataIni,
              string dataFim,
              bool comProcedimentos
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.lblTitulo.Text = titulo.ToUpper();
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                // Setar o header do relatorio
                this.BaseInit(header);

                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(c => c.DT_AGEND_HORAR >= DataInical && c.DT_AGEND_HORAR <= DataFinal)
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                           where (Profissional != 0 ? tbs174.CO_COL == Profissional : true)
                           && (comProcedimentos ? tbs174.TBS356_PROC_MEDIC_PROCE != null : true)
                           && (Local > 0 ? Local == tbs174.CO_DEPT : 0 == 0)
                           select new Relatorio
                           {
                               DataAgend = tbs174.DT_AGEND_HORAR,
                               HoraAgend = tbs174.HR_AGEND_HORAR,

                               Profissional = tb03.NO_COL,
                               Local = tb14.CO_SIGLA_DEPTO,

                               Contrat = tbs174.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.NM_SIGLA_OPER,
                               CodProc = tbs174.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                               NomProc = tbs174.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               vlrProcedimento = tbs174.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null ? tbs174.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE : 0,

                               vlrProcedBase = tbs174.VL_CONSU_BASE,
                               vlrCombinado = tbs174.VL_CONSUL
                           }).OrderBy(w => new { w.DataAgend, w.HoraAgend }).ToList();

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
            public string Profissional { get; set; }
            public string Local { get; set; }
            public DateTime DataAgend { get; set; }
            public string HoraAgend { get; set; }

            public string Data
            {
                get
                {
                    return this.DataAgend.ToShortDateString() + " - " + this.HoraAgend;
                }
            }

            public string Contrat { get; set; }
            public string CodProc { get; set; }
            public string NomProc { get; set; }

            public decimal vlrProcedimento { get; set; }
            public decimal? vlrProcedBase { get; set; }
            public decimal? vlrCombinado { get; set; }
        }
    }
}
