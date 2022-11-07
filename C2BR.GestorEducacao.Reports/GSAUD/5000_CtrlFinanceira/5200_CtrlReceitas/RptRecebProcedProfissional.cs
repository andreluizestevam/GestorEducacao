using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptRecebProcedProfissional : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRecebProcedProfissional()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string infos,
              string parametros,
              int coEmp,
              int Profissional,
              string dataIni,
              string dataFim
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
                DateTime DataFinal = Convert.ToDateTime(dataFim).AddDays(1);//adiciono um dia por causa que o tem hora

                var res = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros().Where(c => c.DT_CADAS >= DataInical && c.DT_CADAS <= DataFinal)
                           join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                           join tb21 in TB21_TIPOCAL.RetornaTodosRegistros() on tb03.CO_TPCAL equals tb21.CO_TPCAL
                           where Profissional != 0 ? tbs390.CO_COL_ATEND == Profissional : true
                           select new Relatorio
                           {
                               DataFatur = tbs396.DT_CADAS,
                               Contrato = tbs390.NU_CONTRATO,

                               Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                               TipoReceb = tb21.CO_SIGLA_TPCAL,
                               vlrSalario = tb03.VL_SALAR_COL.HasValue ? tb03.VL_SALAR_COL.Value : 0,
                               CargaHoraria = tb03.NU_CARGA_HORARIA,

                               Contrat = tbs396.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.NM_SIGLA_OPER,
                               CodProc = tbs396.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                               NomProc = tbs396.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               vlrProcedimento = tbs396.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault() != null ? tbs396.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault().VL_BASE : 0,
                               
                               vlrCombinado = tbs396.VL_PROC,
                               vlrDescontoFatu = tbs396.VL_DSCTO_PROC.HasValue ? tbs396.VL_DSCTO_PROC.Value : 0
                           }).OrderBy(w => new { w.DataFatur }).ToList();

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
            public DateTime DataFatur { get; set; }
            public string Contrato { get; set; }

            public string Contrat { get; set; }
            public string CodProc { get; set; }
            public string NomProc { get; set; }

            private string tipo;
            public string TipoReceb
            {
                get
                {
                    switch (tipo)
                    {
                        case "D":
                            return "Diário";
                        case "S":
                            return "Semanal";
                        case "M":
                            return "Mensal";
                        case "H":
                            return "Hora";
                        case "T":
                            return "Tarefa";
                        case "P":
                            return "Percentual";
                        default:
                            return " - ";
                    }
                }
                set
                {
                    tipo = value;
                }
            }
            
            public double vlrSalario { get; set; }
            public int CargaHoraria { get; set; }

            private decimal receber;
            public decimal vlrReceber
            {
                get
                {
                    switch (tipo)
                    {
                        case "D":
                            return (decimal)vlrSalario * 30;
                        case "S":
                            return (decimal)vlrSalario * 4;
                        case "M":
                            return (decimal)vlrSalario;
                        case "H":
                            return (decimal)vlrSalario * CargaHoraria;
                        case "T":
                            return (decimal)vlrSalario;
                        case "P":
                            return ((decimal)vlrSalario/100) * vlrFaturado;
                        default:
                            return (decimal)vlrSalario;
                    }
                }
                set
                {
                    receber = value;
                }
            }

            public decimal vlrProcedimento { get; set; }
            public decimal vlrCombinado { get; set; }
            public decimal vlrDescontoFatu { get; set; }

            public decimal vlrFaturado
            {
                get
                {
                    return vlrCombinado - vlrDescontoFatu;
                }
            }
        }
    }
}
