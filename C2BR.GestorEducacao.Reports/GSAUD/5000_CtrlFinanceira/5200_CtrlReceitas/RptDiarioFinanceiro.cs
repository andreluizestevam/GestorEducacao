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
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptDiarioFinanceiro : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDiarioFinanceiro()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int coUniContr,
                              int strP_CO_TIPO_DOC,
                              DateTime strP_DT_INI,
                              DateTime strP_DT_FIM,
                              int strP_CO_AGRUP,
                              string tpPagto,
                              int strP_CO_Pagto,
                              string infos, string NO_RELATORIO)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "DIÁRIO FINANCEIRO DE RECEITAS*");

                if (header == null)
                   return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion
                
                #region Query Titulos Parametrizada

                var lst = (from tbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs47.CO_ALU equals tb07.CO_ALU
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs47.CO_COL_BAIXA equals tb03.CO_COL into col
                           from tb03 in col.DefaultIfEmpty()
                           join tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros() on tbs47.CO_AGRUP_RECDESP equals tb315.ID_AGRUP_RECDESP into sr
                           from x in sr.DefaultIfEmpty()
                           join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tbs47.CO_CAIXA equals tb113.CO_CAIXA into cr
                           from y in cr.DefaultIfEmpty()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs47.CO_EMP_UNID_CONT equals tb25.CO_EMP
                           where tbs47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strP_CO_EMP != 0 ? tbs47.CO_EMP == strP_CO_EMP : strP_CO_EMP == 0)
                           && (coUniContr != 0 ? tbs47.CO_EMP_UNID_CONT == coUniContr : coUniContr == 0)
                           && (tbs47.IC_SIT_DOC == "Q" || tbs47.IC_SIT_DOC == "P")
                           && (strP_CO_TIPO_DOC != -1 ? tbs47.TB086_TIPO_DOC.CO_TIPO_DOC == strP_CO_TIPO_DOC : strP_CO_TIPO_DOC == -1)
                           && (tpPagto != "-1" ? tbs47.FL_ORIGEM_PGTO == tpPagto : tpPagto == "-1")
                           && (tbs47.DT_REC_DOC >= strP_DT_INI && tbs47.DT_REC_DOC <= strP_DT_FIM)
                           && (strP_CO_AGRUP != -1 ? tbs47.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == -1)
                           && (tbs47.TP_CLIENTE_DOC == "A" || tbs47.TP_CLIENTE_DOC == "R")
                           select new RelPosRecReceitas
                           {
                               Responsavel = tbs47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") + " - " + tb07.TB108_RESPONSAVEL.NO_RESP,
                               NomeAluno = tb07.NO_ALU,
                               NIREAluno = tb07.NU_NIRE,
                               DataMov = tbs47.DT_REC_DOC,
                               DataVen = tbs47.DT_VEN_DOC,
                               Status = tbs47.IC_SIT_DOC,
                               Documento = tbs47.NU_DOC,
                               DataPagamento = tbs47.DT_REC_DOC,
                               DescontoPago = tbs47.VL_DES_PAG,
                               JurosPago = tbs47.VL_JUR_PAG,
                               MultaPago = tbs47.VL_MUL_PAG,
                               ValorPago = tbs47.VL_PAG,
                               TipoBaixa = tbs47.FL_ORIGEM_PGTO != null ? tbs47.FL_ORIGEM_PGTO : "",
                               ColaboradorBaixa = tb03.NO_COL,
                               MatricColaboradorBaixa = tb03.CO_MAT_COL,
                               Agrupador = x.DE_SITU_AGRUP_RECDESP,
                               TipoDocto = tbs47.TB086_TIPO_DOC != null ? tbs47.TB086_TIPO_DOC.DES_TIPO_DOC : "",
                               NomeCaixa = y.DE_CAIXA,
                               ApeliColaboradorBaixa = tb03.NO_APEL_COL,
                               Banco = tbs47.TB227_DADOS_BOLETO_BANCARIO != null ? tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.IDEBANCO : "",
                               Agencia = tbs47.TB227_DADOS_BOLETO_BANCARIO != null ? tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_AGENCIA : 0,
                               Conta = tbs47.TB227_DADOS_BOLETO_BANCARIO != null ? tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA : "",
                               DigConta = tbs47.TB227_DADOS_BOLETO_BANCARIO != null ? tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA : "",
                               DataMovCaixa = tbs47.DT_MOV_CAIXA,
                               SiglaUnidContr = tb25.sigla,
                               ValorDocumento = tbs47.VL_PAR_DOC

                           }).OrderBy(p => p.DataMov);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPosRecReceitas at in res)
                {

                    #region Definições de tipagem de unidade

                    xrTableCell10.Text = xrTableCell11.Text = xrTableCell14.Text =
                    at.Modalidade = at.Serie = at.Turma = "";
                    xrTableCell16.Text = "NIRE-PACIENTE";

                    #endregion

                    at.FormaPagto = (at.TipoBaixa == "C" ? "Caixa" : (at.TipoBaixa == "X" ? "Baixa CAR" : "Banco"));
                    if (at.TipoBaixa == "C" && strP_CO_Pagto != null && at.Documento != null)
                    {
                        var tipos = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                     join tb156 in TB156_FormaPagamento.RetornaTodosRegistros() on tb296.CO_SEQMOV_CAIXA equals tb156.CO_CAIXA_MOVIMENTO
                                     join tb118 in TB118_TIPO_RECEB.RetornaTodosRegistros() on tb156.CO_TIPO_REC equals tb118.CO_TIPO_REC
                                     where tb296.NU_DOC_CAIXA == at.Documento
                                     && (strP_CO_Pagto == -1 ? 0 == 0 : tb118.CO_TIPO_REC == strP_CO_Pagto)
                                     select new { tb156.CO_FORMAPAGAMENTO, tb296.NU_DOC_CAIXA, tb118.CO_TIPO_REC, tb118.DE_RECEBIMENTO, tb118.DE_SIG_RECEB }
                                         ).Distinct();
                        if (tipos != null && tipos.Count() > 0)
                        {
                            at.FormaPagto += "/";
                            if (tipos.Count() > 1)
                            {
                                foreach (var linha in tipos)
                                {
                                    at.FormaPagto += " " + linha.DE_SIG_RECEB.ToString().Trim();
                                }
                            }
                            else
                                at.FormaPagto += " " + tipos.First().DE_RECEBIMENTO.ToString().Trim();
                            var res2 = tipos.Where(t => t.NU_DOC_CAIXA == at.Documento);
                            if (res2 != null && res2.Count() > 0)
                                bsReport.Add(at);
                        }
                        
                    }
                    else
                        bsReport.Add(at);
                }
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relacao de Receitas Parametrizado do Relatorio

        public class RelPosRecReceitas
        {
            public string TipoBaixa { get; set; }
            public string NomeAluno { get; set; }
            public int NIREAluno { get; set; }
            public string Responsavel { get; set; }
            public string ColaboradorBaixa { get; set; }
            public string MatricColaboradorBaixa { get; set; }
            public string ApeliColaboradorBaixa { get; set; }
            public string Agrupador { get; set; }
            public string TipoDocto { get; set; }
            public string NomeCaixa { get; set; }
            public string Banco { get; set; }
            public int Agencia { get; set; }
            public string Conta { get; set; }
            public string DigConta { get; set; }
            public string SiglaUnidContr { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }

            public DateTime? DataMov { get; set; }
            public DateTime? DataMovCaixa { get; set; }
            public DateTime? DataPagamento { get; set; }
            public DateTime? DataVen { get; set; }
            public string Status { get; set; }
            public string Documento { get; set; }
            public decimal ValorDocumento { get; set; }
            public decimal? Result { get; set; }
            public decimal? ValorPago { get; set; }
            public decimal? JurosPago { get; set; }
            public decimal? MultaPago { get; set; }
            public decimal? DescontoPago { get; set; }
            public TimeSpan DataDif { get; set; }

            public string DescAluno
            {
                get
                {
                    return (this.NIREAluno.ToString() + " - " + this.NomeAluno.ToUpper()).Length > 27 ? (this.NIREAluno.ToString() + " - " + this.NomeAluno.ToUpper()).Substring(0, 27) + "..." : this.NIREAluno.ToString() + " - " + this.NomeAluno.ToUpper();
                }
            }

            public string DescResp
            {
                get
                {
                    return (this.Responsavel.ToUpper()).Length > 37 ? (this.Responsavel.ToUpper()).Substring(0, 37) + "..." : this.Responsavel.ToUpper();
                }
            }

            public string DescPagto
            {
                get
                {
                    return this.TipoBaixa == "C" ? (this.NomeCaixa != null ? this.NomeCaixa : "XXX") + " - Matr.: " + (this.MatricColaboradorBaixa != null ? this.MatricColaboradorBaixa.Insert(5, "-").Insert(2, ".") : "") + " - " + (this.DataMovCaixa != null ? this.DataMovCaixa.Value.ToString("dd/MM/yy") : "XX/XX/XX")
                        : this.TipoBaixa == "X" ? "Matr. Respo: " + (this.MatricColaboradorBaixa != null ? this.MatricColaboradorBaixa.Insert(5, "-").Insert(2, ".") : "") + " - " + (this.DataMovCaixa != null ? this.DataMovCaixa.Value.ToString("dd/MM/yy") : "XX/XX/XX")
                        : "BCO: " + this.Banco + " - AG: " + (this.Agencia != 0 ? this.Agencia.ToString() : "") + " - CC: " + this.Conta.Trim() + "-" + this.DigConta.Trim() + " - " + (this.DataMovCaixa != null ? this.DataMovCaixa.Value.ToString("dd/MM/yy") : "XX/XX/XX");
                }
            }

            public string FormaPagto
            {
                get;
                set;
            }
        }
        #endregion
    }
}
