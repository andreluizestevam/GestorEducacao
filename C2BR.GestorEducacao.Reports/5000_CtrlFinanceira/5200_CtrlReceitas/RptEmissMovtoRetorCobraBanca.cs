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
    public partial class RptEmissMovtoRetorCobraBanca : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptEmissMovtoRetorCobraBanca()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              string strP_IDEBANCO,
                              int strP_CO_AGENCIA,
                              string strP_CO_CONTA,
                              string strP_STATUS,
                              string strP_DT_INI_VENC,
                              string strP_DT_FIM_VENC,
                              string strP_DT_INI_CREDI,
                              string strP_DT_FIM_CREDI,
                              string strP_ORDEM,
                              string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Titulos Parametrizada

                DateTime dtIniCredi = strP_DT_INI_CREDI != "" ? DateTime.Parse(strP_DT_INI_CREDI) : DateTime.Now;
                DateTime dtFimCredi = strP_DT_FIM_CREDI != "" ? DateTime.Parse(strP_DT_FIM_CREDI) : DateTime.Now;
                DateTime dtIniVenc = strP_DT_INI_VENC != "" ? DateTime.Parse(strP_DT_INI_VENC) : DateTime.Now;
                DateTime dtFimVenc = strP_DT_FIM_VENC != "" ? DateTime.Parse(strP_DT_FIM_VENC) : DateTime.Now;

                var lst = (from tb321 in ctx.TB321_ARQ_RET_BOLETO
                           where (strP_IDEBANCO != "" ? tb321.IDEBANCO == strP_IDEBANCO : strP_IDEBANCO == "")
                           //&& (strP_CO_EMP_REF != 0 ? tb47.CO_EMP == strP_CO_EMP_REF : strP_CO_EMP_REF == 0)
                           && (strP_CO_AGENCIA != 0 ? tb321.CO_AGENCIA == strP_CO_AGENCIA : strP_CO_AGENCIA == 0)
                           && (strP_CO_CONTA != "" ? tb321.CO_CONTA == strP_CO_CONTA : strP_CO_CONTA == "")
                           && (strP_IDEBANCO != "" ? tb321.IDEBANCO == strP_IDEBANCO : strP_IDEBANCO == "")
                           && (strP_STATUS != "T" ? tb321.FL_STATUS == strP_STATUS : strP_STATUS == "T")
                           && (strP_DT_INI_VENC != "" ? tb321.DT_VENCIMENTO >= dtIniVenc : strP_DT_INI_VENC == "")
                           && (strP_DT_FIM_VENC != "" ? tb321.DT_VENCIMENTO <= dtFimVenc : strP_DT_FIM_VENC == "")
                           && (strP_DT_INI_CREDI != "" ? tb321.DT_CREDITO >= dtIniCredi : strP_DT_INI_CREDI == "")
                           && (strP_DT_FIM_CREDI != "" ? tb321.DT_CREDITO <= dtFimCredi : strP_DT_FIM_CREDI == "")
                           && tb321.CO_EMP == strP_CO_EMP
                           select new RelTitulosRetorno
                           {
                               DataCredito = tb321.DT_CREDITO,
                               DataVencimento = tb321.DT_VENCIMENTO,
                               NossoNumero = tb321.NU_NOSSO_NUMERO,
                               ValorDocumento = tb321.VL_TITULO,
                               Multa = tb321.VL_MULTA,
                               Juros = tb321.VL_JUROS,
                               Desconto = tb321.VL_DESCTO,
                               ValorPago = tb321.VL_PAGO,
                               Tarifa = tb321.VL_TARIFAS,
                               Banco = tb321.IDEBANCO,
                               CoAgencia = tb321.CO_AGENCIA,
                               DiAgencia = tb321.DI_AGENCIA,
                               Conta = tb321.CO_CONTA,
                               DiConta = tb321.CO_DIG_CONTA,
                               Status = tb321.FL_STATUS,
                               Documento = tb321.NU_DCTO_RECEB,
                               Lote = tb321.NU_LOTE_ARQUI
                           });

                var res = lst.ToList();

                switch (strP_ORDEM)
                {
                    case "V":
                        res = lst.OrderBy(p => new { p.DataVencimento }).ToList();
                        break;
                    case "C":
                        res = lst.OrderBy(p => new { p.DataCredito }).ToList();
                        break;
                    case "B":
                        res = lst.OrderBy(p => new { p.Banco, p.CoAgencia, p.Conta }).ToList();
                        break;
                    case "N":
                        res = lst.OrderBy(p => new { p.NossoNumero }).ToList();
                        break;
                    case "S":
                        res = lst.OrderBy(p => new { p.Status }).ToList();
                        break;
                }
                /*
                if (strP_ORDEM == "V")
                {
                    res.OrderBy(p => p.DataVencimento);
                }
                else if (strP_ORDEM == "C")
                {
                    res.OrderBy(p => p.DataCredito);
                }
                else if (strP_ORDEM == "B")
                {
                    res.OrderBy(p => p.Banco).ThenBy(p=> p.CoAgencia).ThenBy(p => p.Conta);
                }
                else if (strP_ORDEM == "N")
                {
                    res.OrderBy(p => p.NossoNumero);
                }
                else
                {
                    res.OrderBy(p => p.Status);
                }
                */
                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();

                foreach (RelTitulosRetorno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relacao de Titulos do Aluno Parametrizado do Relatorio

        public class RelTitulosRetorno
        {
            public string Documento { get; set; }
            public DateTime DataCredito { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Status { get; set; }
            public string NossoNumero { get; set; }
            public string Banco { get; set; }
            public int? CoAgencia { get; set; }
            public string DiAgencia { get; set; }
            public string Conta { get; set; }
            public string DiConta { get; set; }
            public string UnidContrato { get; set; }
            public decimal ValorDocumento { get; set; }
            public decimal ValorPago { get; set; }
            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public decimal? Tarifa { get; set; }
            public decimal? Lote { get; set; }

            public string DescAgen
            {
                get
                {
                    // Linha comentada para retirar a máscara da agência
                    //return this.CoAgencia != null && this.DiAgencia != null ? this.CoAgencia.ToString() + "-" + this.DiAgencia : "";
                    return this.CoAgencia + this.DiAgencia;
                }
            }

            public string DescLote
            {
                get
                {
                    return this.Lote != null ? this.Lote.ToString().PadLeft(4,'0') : "";
                }
            }

            public string DescConta
            {
                get
                {
                    // Linha comentada para retirar a máscara da conta
                    //return this.Conta != null && this.DiConta != null ? this.Conta.Trim() + "-" + this.DiConta : "";
                    return this.Conta.Substring(0,10) + this.DiConta;
                }
            }

            public decimal Total
            {
                get
                {
                    return (this.ValorDocumento + (this.Multa ?? 0) + (this.Juros ?? 0) - (this.Desconto ?? 0));
                }
            }


            public decimal? ValorDif
            {
                get
                {
                    return this.ValorPago - ((this.ValorDocumento + (this.Multa ?? 0) + (this.Juros ?? 0) - (this.Desconto ?? 0)));
                }
            }

            public string StatusDesc
            {
                get
                {
                    if (this.Status == "P")
                    {
                        return "Pend. Baixa";
                    }
                    else if (this.Status == "I")
                    {
                        return "Inconsistente";
                    }
                    else if (this.Status == "D")
                    {
                        return "Mov Duplicado";
                    }
                    else if (this.Status == "Q")
                    {
                        return "Já Pago";
                    }
                    else if (this.Status == "C")
                    {
                        return "Tit Cancelado";
                    }
                    else
                    {
                        return "Baixado";
                    }
                }
            }
        }
        #endregion
    }
}
