using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptComissionamento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptComissionamento()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string parametros,
              string infos,
              int coEmp,
              int unidade,
              int profissional,
              int grup,
              int sgrup,
              int proc,
              string situ,
              string tipo
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.lblTitulo.Text = titulo.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                // Setar o header do relatorio
                this.BaseInit(header);

                var res = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                           where (unidade != 0 ? tbs410.TB25_EMPRESA.CO_EMP == unidade : 0 == 0)
                           && (profissional != 0 ? tbs410.TB03_COLABOR.CO_COL == profissional : 0 == 0)
                           && (grup != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                           && (sgrup != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                           && (proc != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == proc : 0 == 0)
                           && (situ != "0" ? tbs410.CO_SITUA == situ : 0 == 0)
                           && (tipo != "0" ? (tipo == "AVL" ? tbs410.VL_AVALIA.HasValue
                                           : (tipo == "CBR" ? tbs410.VL_COBRAN.HasValue
                                           : (tipo == "CNT" ? tbs410.VL_CONTRT.HasValue
                                           : (tipo == "IPC" ? tbs410.VL_INDC_PAC.HasValue
                                           : (tipo == "IPR" ? tbs410.VL_INDC_PROC.HasValue
                                    /*PLA*/: tbs410.VL_PLANEJ.HasValue))))) : 0 == 0)
                           select new Relatorio
                           {
                               NO_COL = tbs410.TB03_COLABOR.NO_APEL_COL,
                               GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                               SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                               PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,

                               PC_AVALIACAO = tbs410.PC_AVALIA,
                               VL_AVALIACAO = tbs410.VL_AVALIA,
                               PC_COBRANCA = tbs410.PC_COBRAN,
                               VL_COBRANCA = tbs410.VL_COBRAN,
                               PC_CONTRATO = tbs410.PC_CONTRT,
                               VL_CONTRATO = tbs410.VL_CONTRT,
                               PC_IND_PACIENTE = tbs410.PC_INDC_PAC,
                               VL_IND_PACIENTE = tbs410.VL_INDC_PAC,
                               PC_IND_PROCEDIMENTO = tbs410.PC_INDC_PROC,
                               VL_IND_PROCEDIMENTO = tbs410.VL_INDC_PROC,
                               PC_PLANEJAMENTO = tbs410.PC_PLANEJ,
                               VL_PLANEJAMENTO = tbs410.VL_PLANEJ,

                               STATUS = tbs410.CO_SITUA
                           }).OrderBy(a => a.NO_COL).ToList();
                
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
            public string NO_COL { get; set; }
            public string GRUPO { get; set; }
            public string SUBGRUPO { get; set; }
            public string PROCED { get; set; }
            public string STATUS { get; set; }

            public string PC_AVALIACAO { get; set; }
            public decimal? VL_AVALIACAO { get; set; }
            public string DE_AVALIACAO
            {
                get
                {
                    return VL_AVALIACAO.HasValue ? ((PC_AVALIACAO == "S" ? "" : "R$ ") + VL_AVALIACAO.Value.ToString("N") + (PC_AVALIACAO == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_COBRANCA { get; set; }
            public decimal? VL_COBRANCA { get; set; }
            public string DE_COBRANCA
            {
                get
                {
                    return VL_COBRANCA.HasValue ? ((PC_COBRANCA == "S" ? "" : "R$ ") + VL_COBRANCA.Value.ToString("N") + (PC_COBRANCA == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_CONTRATO { get; set; }
            public decimal? VL_CONTRATO { get; set; }
            public string DE_CONTRATO
            {
                get
                {
                    return VL_CONTRATO.HasValue ? ((PC_CONTRATO == "S" ? "" : "R$ ") + VL_CONTRATO.Value.ToString("N") + (PC_CONTRATO == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_IND_PACIENTE { get; set; }
            public decimal? VL_IND_PACIENTE { get; set; }
            public string DE_IND_PACIENTE
            {
                get
                {
                    return VL_IND_PACIENTE.HasValue ? ((PC_IND_PACIENTE == "S" ? "" : "R$ ") + VL_IND_PACIENTE.Value.ToString("N") + (PC_IND_PACIENTE == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_IND_PROCEDIMENTO { get; set; }
            public decimal? VL_IND_PROCEDIMENTO { get; set; }
            public string DE_IND_PROCEDIMENTO
            {
                get
                {
                    return VL_IND_PROCEDIMENTO.HasValue ? ((PC_IND_PROCEDIMENTO == "S" ? "" : "R$ ") + VL_IND_PROCEDIMENTO.Value.ToString("N") + (PC_IND_PROCEDIMENTO == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_PLANEJAMENTO { get; set; }
            public decimal? VL_PLANEJAMENTO { get; set; }
            public string DE_PLANEJAMENTO
            {
                get
                {
                    return VL_PLANEJAMENTO.HasValue ? ((PC_PLANEJAMENTO == "S" ? "" : "R$ ") + VL_PLANEJAMENTO.Value.ToString("N") + (PC_PLANEJAMENTO == "S" ? " %" : "")) : " - ";
                }
            }
        }
    }
}
