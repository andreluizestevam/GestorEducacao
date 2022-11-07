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
    public partial class RptHistoricoRecebimento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptHistoricoRecebimento()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string parametros,
              string infos,
              int coEmp,
              int Paciente,
              string tipo,
              int contratacao,
              int plano,
              string cortesia,
              string notaFiscal,
              string dataIni,
              string dataFim
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

                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim).AddDays(1);//adiciono um dia por causa que o tem hora

                var res = (from tbs363 in TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.DT_CADAS >= DataInical && c.DT_CADAS <= DataFinal)
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs363.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           where Paciente != 0 ? tbs174.CO_ALU == Paciente : Paciente == 0
                           && contratacao != 0 ? tbs363.TB250_OPERA.ID_OPER == contratacao : true
                           && plano != 0 ? tbs363.TB251_PLANO_OPERA.ID_PLAN == plano : true
                           && !String.IsNullOrEmpty(cortesia) ? tbs363.FL_CORTESIA == cortesia : true
                           && !String.IsNullOrEmpty(notaFiscal) ? tbs363.FL_NOTA_FISCAL == notaFiscal : true
                           select new Relatorio
                           {
                               Paciente = tb07.NO_ALU,
                               Responsavel = tb108.NO_RESP,
                               DataAgenda = tbs174.DT_AGEND_HORAR,
                               DataRecibo = tbs363.DT_CADAS,
                               Cortesia = tbs363.FL_CORTESIA,
                               Contratacao = tbs363.TB250_OPERA != null ? tbs363.TB250_OPERA.NM_SIGLA_OPER : " - ",
                               ContratParticular = tbs363.TB250_OPERA != null ? (tbs363.TB250_OPERA.FL_INSTI_OPERA != null && tbs363.TB250_OPERA.FL_INSTI_OPERA == "S") : false,
                               Tipo = tbs363.DE_TIPO,
                               vlrDinheiro = tbs363.VL_DINHE.HasValue ? tbs363.VL_DINHE.Value : 0,
                               vlrCheque = tbs363.VL_CHEQUE.HasValue ? tbs363.VL_CHEQUE.Value : 0,
                               vlrDebito = tbs363.VL_CARTA_DEBI.HasValue ? tbs363.VL_CARTA_DEBI.Value : 0,
                               vlrCredito = tbs363.VL_CARTA_CRED.HasValue ? tbs363.VL_CARTA_CRED.Value : 0,
                               vlrTransferencia = tbs363.VL_TRANS.HasValue ? tbs363.VL_TRANS.Value : 0,
                               vlrDeposito = tbs363.VL_DEPOS.HasValue ? tbs363.VL_DEPOS.Value : 0,
                               vlrBoleto = tbs363.VL_BOLETO.HasValue ? tbs363.VL_BOLETO.Value : 0,
                               vlrOutros = tbs363.VL_OUTRO.HasValue ? tbs363.VL_OUTRO.Value : 0,
                               vlrTotal = tbs363.VL_TOTAL
                           }).OrderBy(w => new { w.DataRecibo }).ToList();
                
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
            public string Paciente { get; set; }
            public string Responsavel { get; set; }
            public DateTime DataAgenda { get; set; }
            public DateTime DataRecibo { get; set; }

            public string Cortesia { get; set; }
            public string Contratacao { get; set; }
            public bool ContratParticular { get; set; }
            public string Classificacao
            {
                get
                {
                    if (!String.IsNullOrEmpty(Cortesia) && Cortesia == "S")
                        return "(X)" + Contratacao;
                    else if (ContratParticular)
                        return "($)" + Contratacao;
                    else
                        return "(G)" + Contratacao;
                }
            }

            private string tipo;
            public string Tipo
            {
                get
                {
                    switch (tipo)
                    {
                        case "C":
                            return "CONS";
                        case "P":
                            return "PROC";
                        case "S":
                            return "SERV";
                        case "E":
                            return "EXAM";
                        case "O":
                            return "OUTR";
                        default:
                            return " - ";
                    }
                }
                set
                {
                    tipo = value;
                }
            }

            public decimal vlrDinheiro { get; set; }
            public decimal vlrCheque { get; set; }
            public decimal vlrDebito { get; set; }
            public decimal vlrCredito { get; set; }
            public decimal vlrTransferencia { get; set; }
            public decimal vlrDeposito { get; set; }
            public decimal vlrBoleto { get; set; }
            public decimal vlrOutros { get; set; }
            public decimal vlrTotal { get; set; }
        }
    }
}
