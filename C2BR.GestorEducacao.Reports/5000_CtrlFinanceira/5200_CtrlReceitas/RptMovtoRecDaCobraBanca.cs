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


namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptMovtoRecDaCobraBanca : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMovtoRecDaCobraBanca()
        {
            InitializeComponent();
        }

        public int InitReport(string strParametrosRelatorio
                                , int CODIGO_ORGAO
                                , int CO_EMP
                                , string strP_IDEBANCO
                                , int intP_CO_AGENCIA
                                , string strP_CO_CONTA
                                , string strP_STATUS
                                , string strContrato
                                , DateTime dtInicio
                                , DateTime dtFim
                                , string strINFOS
                             )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = strINFOS;
                this.Parametros = strParametrosRelatorio;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(CO_EMP);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Titulos Parametrizada


                var lst = (from arb in ctx.TB321_ARQ_RET_BOLETO
                           join cr in ctx.TB47_CTA_RECEB on arb.NU_DCTO_RECEB equals cr.NU_DOC into resultado
                           from cr in resultado.DefaultIfEmpty()
                           join al in ctx.TB07_ALUNO on cr.CO_ALU equals al.CO_ALU into resultado1
                           from al in resultado1.DefaultIfEmpty()

                           where (strP_IDEBANCO == "-1" ? 0 == 0 : arb.IDEBANCO == strP_IDEBANCO)
                           && (intP_CO_AGENCIA == -1 ? 0 == 0 : arb.CO_AGENCIA == intP_CO_AGENCIA)
                           && (strP_CO_CONTA == "-1" ? 0 == 0 : arb.CO_CONTA == strP_CO_CONTA)
                           && (strP_IDEBANCO == "-1" ? 0 == 0 : arb.IDEBANCO == strP_IDEBANCO)
                           && (strP_STATUS == "-1" ? 0 == 0 : arb.FL_STATUS == strP_STATUS)
                           && (strContrato == "-1" ? 0 == 0 : 0 == 0)
                           && (arb.DT_VENCIMENTO >= dtInicio && arb.DT_VENCIMENTO <= dtFim)
                           && arb.CO_EMP == CO_EMP
                           select new RelRecCobraBanca
                           {
                               NomeResp = cr == null ? null : cr.TB108_RESPONSAVEL.NO_RESP,
                               CpfResp = cr == null ? null : cr.TB108_RESPONSAVEL.NU_CPF_RESP,
                               NomeAluno = arb.FL_STATUS == "I" ? "Inconsistente" : al == null ? null : al.NO_ALU,
                               NireAluno = al == null ? 0 : al.NU_NIRE,
                               DataCredito = arb.DT_CREDITO,
                               DataVencimento = arb.DT_VENCIMENTO,
                               NossoNumero = arb.NU_NOSSO_NUMERO,
                               ValorOriginal = arb.VL_TITULO,
                               ValorMulta = arb.VL_MULTA,
                               ValorJuros = arb.VL_JUROS,
                               ValorDesconto = arb.VL_DESCTO,
                               ValorPago = arb.VL_PAGO,
                               ValorTarifa = arb.VL_TARIFAS,
                               Banco = arb.IDEBANCO,
                               CoAgencia = arb.CO_AGENCIA,
                               DiAgencia = arb.DI_AGENCIA,
                               Conta = arb.CO_CONTA,
                               DiConta = arb.CO_DIG_CONTA,
                               Status = arb.FL_STATUS,
                               Documento = arb.NU_DCTO_RECEB,
                               Lote = arb.NU_LOTE_ARQUI
                           });

                var res = lst.ToList();


                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();

                foreach (RelRecCobraBanca at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #region Classe
        public class RelRecCobraBanca
        {
            public int NumResp { get; set; }
            public string Documento { get; set; }
            public string NomeAluno { get; set; }
            public int NireAluno { get; set; }
            public string NireEAluno
       
            {
                get
                {
                    return this.NomeAluno == "Inconsistente"?"***Título sem referência no Cadastro Financeiro***":this.NireAluno.ToString() + " - " + this.NomeAluno;
                }
            }
            public string NomeResp { get; set; }
            public string CpfResp { get; set; }
            public string CpfEResp
            {
                get
                {
                    return this.CpfResp + " - " + this.NomeResp;
                }
            }
            public DateTime DataCredito { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Status { get; set; }
            public string NossoNumero { get; set; }
            public string NumConvenio { get; set; }
            public string Banco { get; set; }
            public int? CoAgencia { get; set; }
            public string DiAgencia { get; set; }
            public string Conta { get; set; }
            public string DiConta { get; set; }
            public string UnidContrato { get; set; }
            public decimal ValorOriginal { get; set; }
            public decimal ValorPago { get; set; }
            public decimal? ValorPagoLiq
            {
                get
                {
                    decimal valor = this.ValorPago - ((this.ValorMulta == null ? 0 : (decimal)this.ValorMulta + this.ValorJuros == null ? 0 : (decimal)this.ValorJuros) - this.ValorDesconto == null ? 0 : (decimal)this.ValorDesconto);
                    if (valor == 0)
                        return null;
                    else
                        return valor;
                }
            }
            public decimal? ValorJuros { get; set; }
            public decimal? ValorMulta { get; set; }
            public decimal? ValorDesconto { get; set; }
            public decimal? ValorTarifa { get; set; }
            public decimal? Lote { get; set; }

        }
        #endregion

    }
}
