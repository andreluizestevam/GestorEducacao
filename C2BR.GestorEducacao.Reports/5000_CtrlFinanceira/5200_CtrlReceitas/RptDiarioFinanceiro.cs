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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
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
                              int strP_CO_EMP_REF,
                              int coUniContr,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string strP_CO_ANO_MES_MAT,
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

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Titulos Parametrizada

                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           join tb03 in ctx.TB03_COLABOR on tb47.CO_COL_BAIXA equals tb03.CO_COL
                           join tb315 in ctx.TB315_AGRUP_RECDESP on tb47.CO_AGRUP_RECDESP equals tb315.ID_AGRUP_RECDESP into sr
                           from x in sr.DefaultIfEmpty()
                           join tb113 in ctx.TB113_PARAM_CAIXA on tb47.CO_CAIXA equals tb113.CO_CAIXA into cr
                           from y in cr.DefaultIfEmpty()
                           join tb25 in ctx.TB25_EMPRESA on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP
                           join tb01 in ctx.TB01_CURSO on tb47.CO_CUR equals tb01.CO_CUR into l1
                           from lcu in l1.DefaultIfEmpty()
                           join tb129 in ctx.TB129_CADTURMAS on tb47.CO_TUR equals tb129.CO_TUR into l2
                           from ltu in l2.DefaultIfEmpty()
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (coUniContr != 0 ? tb47.CO_EMP_UNID_CONT == coUniContr : coUniContr == 0)
                           && (strP_CO_EMP_REF != -1 ? tb47.CO_EMP == strP_CO_EMP_REF : strP_CO_EMP_REF == -1)
                           && (tb47.IC_SIT_DOC == "Q" || tb47.IC_SIT_DOC == "P")
                           && (strP_CO_TIPO_DOC != -1 ? tb47.TB086_TIPO_DOC.CO_TIPO_DOC == strP_CO_TIPO_DOC : strP_CO_TIPO_DOC == -1)
                           && (strP_CO_MODU_CUR != 0 ? tb47.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0)
                           && (strP_CO_CUR != 0 ? tb47.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0)
                           //&& (strP_CO_ANO_MES_MAT != "-1" ? tb47.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT : strP_CO_ANO_MES_MAT == "-1")
                           && (tpPagto != "-1" ? tb47.FL_ORIGEM_PGTO == tpPagto : tpPagto == "-1")
                           && (strP_CO_TUR != 0 ? tb47.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0)
                           && (tb47.DT_REC_DOC >= strP_DT_INI && tb47.DT_REC_DOC <= strP_DT_FIM)
                           && (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == -1)
                           && (tb47.TP_CLIENTE_DOC == "A" || tb47.TP_CLIENTE_DOC == "R")
                           select new RelPosRecReceitas
                           {
                               Responsavel = tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") + " - " + tb07.TB108_RESPONSAVEL.NO_RESP,
                               NomeAluno = tb07.NO_ALU,
                               NIREAluno = tb07.NU_NIRE,
                               DataMov = tb47.DT_REC_DOC,
                               DataVen = tb47.DT_VEN_DOC,
                               Status = tb47.IC_SIT_DOC,
                               Documento = tb47.NU_DOC,
                               DataPagamento = tb47.DT_REC_DOC,
                               DescontoPago = tb47.VR_DES_PAG,
                               JurosPago = tb47.VR_JUR_PAG,
                               MultaPago = tb47.VR_MUL_PAG,
                               ValorPago = tb47.VR_PAG,
                               TipoBaixa = tb47.FL_ORIGEM_PGTO != null ? tb47.FL_ORIGEM_PGTO : "",
                               ColaboradorBaixa = tb03.NO_COL,
                               MatricColaboradorBaixa = tb03.CO_MAT_COL,
                               Agrupador = x.DE_SITU_AGRUP_RECDESP,
                               TipoDocto = tb47.TB086_TIPO_DOC != null ? tb47.TB086_TIPO_DOC.DES_TIPO_DOC : "",
                               NomeCaixa = y.DE_CAIXA,
                               ApeliColaboradorBaixa = tb03.NO_APEL_COL,
                               Banco = tb47.TB227_DADOS_BOLETO_BANCARIO != null ? tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.IDEBANCO : "",
                               Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO != null ? tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_AGENCIA : 0,
                               Conta = tb47.TB227_DADOS_BOLETO_BANCARIO != null ? tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA : "",
                               DigConta = tb47.TB227_DADOS_BOLETO_BANCARIO != null ? tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA : "",
                               DataMovCaixa = tb47.DT_MOV_CAIXA,
                               SiglaUnidContr = tb25.sigla,
                               Modalidade = lcu.TB44_MODULO.DE_MODU_CUR,
                               Serie = lcu.NO_CUR,
                               Turma = ltu.CO_SIGLA_TURMA,
                               ValorDocumento = tb47.VR_PAR_DOC

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

                    //Altera as colunas caso a empresa logada seja saúde ou educação
                    string coTipoUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).CO_TIPO_UNID;
                    switch (coTipoUnid)
                    {
                        case "PGS":
                            xrTableCell10.Text = xrTableCell11.Text = xrTableCell14.Text =
                            at.Modalidade = at.Serie = at.Turma = "";
                            xrTableCell16.Text = "NIRE-PACIENTE";
                            break;
                        case "PGE":
                        default:
                            xrTableCell10.Text = xrTableCell11.Text = xrTableCell14.Text =
                            xrTableCell16.Text = "NIRE-ALUNO";
                            break;
                    }

                    #endregion

                    at.FormaPagto = (at.TipoBaixa == "C" ? "Caixa" : (at.TipoBaixa == "X" ? "Baixa CAR" : "Banco"));
                    if (at.TipoBaixa == "C" && strP_CO_Pagto != null && at.Documento != null)
                    {
                        var tipos = (from tb296 in ctx.TB296_CAIXA_MOVIMENTO
                                     join tb156 in ctx.TB156_FormaPagamento on tb296.CO_SEQMOV_CAIXA equals tb156.CO_CAIXA_MOVIMENTO
                                     join tb118 in ctx.TB118_TIPO_RECEB on tb156.CO_TIPO_REC equals tb118.CO_TIPO_REC
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
