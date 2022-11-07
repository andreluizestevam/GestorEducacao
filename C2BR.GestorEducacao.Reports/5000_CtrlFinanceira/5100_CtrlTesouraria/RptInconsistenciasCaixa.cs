using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria
{
    public partial class RptInconsistenciasCaixa : C2BR.GestorEducacao.Reports.RptRetrato
    {
        //private bool isEmpty = false;
        //private bool isEmptyInconsist = false;

        #region ctor

        public RptInconsistenciasCaixa()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              int codEmpRef,
                              List<RegistroInformado> regInformados,
                              int codCaixa,
                              DateTime dtMovimento,
                              int codUsuario,
                              string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var tpFormasPgtos = (from fp in ctx.TB118_TIPO_RECEB
                                     select new { fp.CO_TIPO_REC, fp.DE_RECEBIMENTO })
                                    .OrderBy(x => x.DE_RECEBIMENTO).ToList();

                var analisados = (from tb296 in ctx.TB296_CAIXA_MOVIMENTO
                                  join tb156 in ctx.TB156_FormaPagamento on tb296.CO_SEQMOV_CAIXA equals tb156.CO_CAIXA_MOVIMENTO
                                  join tb47 in ctx.TB47_CTA_RECEB on tb296.NU_DOC_CAIXA equals tb47.NU_DOC
                                  where tb296.TP_OPER_CAIXA.Equals("C")
                                  && tb296.TB295_CAIXA.TB03_COLABOR.CO_EMP == codEmpRef
                                  && tb296.TB295_CAIXA.CO_CAIXA == codCaixa
                                  && tb296.TB295_CAIXA.DT_MOVIMENTO == dtMovimento
                                  && tb296.TB295_CAIXA.TB03_COLABOR.CO_COL == codUsuario
                                  && tb47.NU_PAR == tb296.NU_PAR_DOC_CAIXA && tb47.DT_CAD_DOC == tb296.DT_DOC_CAIXA
                                  && tb47.CO_EMP == tb296.CO_EMP_CAIXA && tb296.FLA_SITU_DOC == "A"
                                  select new
                                  {
                                      tb156.CO_TIPO_REC,
                                      tb156.VR_RECEBIDO,
                                      tb296.DT_REGISTRO,
                                      tb47.DT_VEN_DOC,
                                      tb47.NU_PAR,
                                      Nome = (tb296.TP_CLIENTE_DOC == "A" ? tb47.TB108_RESPONSAVEL.NO_RESP : tb47.TB103_CLIENTE.NO_FAN_CLI),
                                      tb296.NU_DOC_CAIXA,
                                      tb47.TB086_TIPO_DOC.SIG_TIPO_DOC,
                                      tb156.DE_OBS
                                  }).ToList();

                List<AnaliseInconsistencia> res = new List<AnaliseInconsistencia>();

                tpFormasPgtos.ForEach(f =>
                {
                    AnaliseInconsistencia ai = new AnaliseInconsistencia();
                    ai.TpPagto = f.DE_RECEBIMENTO;

                    int qtdInf = regInformados.Any(x => x.CodTpPagto == f.CO_TIPO_REC)
                        ? regInformados.First(x => x.CodTpPagto == f.CO_TIPO_REC).Qtd
                        : 0;

                    decimal vlInf = regInformados.Any(x => x.CodTpPagto == f.CO_TIPO_REC)
                        ? (regInformados.First(x => x.CodTpPagto == f.CO_TIPO_REC).Valor ?? 0)
                        : 0;

                    int qtdAna = analisados.Where(x => x.CO_TIPO_REC == f.CO_TIPO_REC).Count();
                    decimal vlAna = (analisados.Where(x => x.CO_TIPO_REC == f.CO_TIPO_REC)
                        .Sum(x => x.VR_RECEBIDO) ?? 0);

                    string str = (qtdInf == qtdAna && vlInf == vlAna) ? "OK" : "Inconsistência";

                    if (!analisados.Any(x => x.CO_TIPO_REC == f.CO_TIPO_REC))
                    {
                        ai.Descricao = string.Format("Informado: {0} / {1:c2}   -   Análise: 0 / R$ 0,00 ({2})",
                            qtdInf, vlInf, str);
                        //if (vlInf == 0 && qtdInf == 0)
                            //this.isEmpty = true;
                    }
                    else
                    {
                        ai.Descricao = string.Format("Informado: {0} / {1:c2}   -   Análise: {2:'+'0;'-'0} / {3:'+'#,##0.00;'-'#,##0.00} ({4})",
                               qtdInf, vlInf, (qtdInf - qtdAna), (vlInf - vlAna), str);

                        if (qtdInf == qtdAna && vlInf == vlAna)
                        {
                            //this.isEmptyInconsist = true;
                        }
                        else
                        {
                            analisados.Where(x => x.CO_TIPO_REC == f.CO_TIPO_REC).ToList()
                               .ForEach(a =>
                               {
                                   RegistroAnalisado ra = new RegistroAnalisado();

                                   ra.Documento = a.NU_DOC_CAIXA;
                                   ra.DtMovimento = a.DT_REGISTRO;
                                   ra.DtVencimento = a.DT_VEN_DOC;
                                   ra.Parcela = a.NU_PAR;
                                   ra.Responsavel = a.Nome;
                                   ra.Valor = (a.VR_RECEBIDO ?? 0);
                                   ra.Observacao = a.DE_OBS;
                                   ra.SiglaTpDocumento = a.SIG_TIPO_DOC;

                                   ai.Registros.Add(ra);
                               });
                        }
                    }

                    res.Add(ai);
                });

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                foreach (var r in res)
                    bsReport.Add(r);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper Registro Informado

        public class RegistroInformado
        {
            public int CodTpPagto { get; set; }
            public int Qtd { get; set; }
            public decimal? Valor { get; set; }
        }

        #endregion

        #region Class Helper Registros Analisados

        public class AnaliseInconsistencia
        {
            public AnaliseInconsistencia()
            {
                this.Registros = new List<RegistroAnalisado>();
            }

            public string TpPagto { get; set; }
            public string Descricao { get; set; }
            public List<RegistroAnalisado> Registros { get; set; }
        }

        public class RegistroAnalisado
        {
            public string Responsavel { get; set; }
            public DateTime DtMovimento { get; set; }
            public DateTime DtVencimento { get; set; }
            public string Documento { get; set; }
            public string SiglaTpDocumento { get; set; }
            public string Observacao { get; set; }
            public decimal Valor { get; set; }
            public int Parcela { get; set; }

            public string DescObservacao
            {
                get
                {
                    return this.Observacao != null ? (this.Observacao.Length > 22) ? this.Observacao.Substring(0, 22) + "..." : this.Observacao : "";
                }
            }

            public string DescResponsavel
            {
                get
                {
                    return (this.Responsavel.Length > 30) ? this.Responsavel.Substring(0, 30) + "..." : this.Responsavel;
                }
            }
        }

        #endregion

        private void footerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (!string.IsNullOrEmpty(lblCont.Text))
            //    return;

            //if (!this.isEmpty)
            //    lblResult.Text = "Nenhum registro no caixa.";
            //else if (!this.isEmptyInconsist)
            //    lblResult.Text = "Nenhuma inconsistência encontrada.";
        }
    }
}