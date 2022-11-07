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
    public partial class RptPosicInadimPorSerieTurma : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        private decimal totParcValorDocto, totParcValorPago, totParcValorMulta, totParcValorJuros, totParcValorDescto, totParcValorTotal, totParcValorSaldo, valorSumTitulos = 0;
        private int qtdeTitAluno = 0;
        public DateTime Dataref;


        public RptPosicInadimPorSerieTurma()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_EMP_UNID_CONT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
            //DateTime strP_DT_INI,
            //DateTime strP_DT_FIM,
                              int strP_CO_AGRUP,
                              string infos,
                              DateTime strDt_Ini,
                              DateTime strDt_Fim)
        {
            try
            {
                Dataref = strDt_Fim;

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

                #region Query Colaborador Parametrizada

                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           join tb25 in ctx.TB25_EMPRESA on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && tb47.CO_EMP == strP_CO_EMP_REF
                           && (strP_CO_MODU_CUR != 0 ? tb47.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                           && (strP_CO_CUR != 0 ? tb47.CO_CUR == strP_CO_CUR : 0 == 0)
                           && (strP_CO_TUR != 0 ? tb47.CO_TUR == strP_CO_TUR : 0 == 0)
                           && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "P" || tb47.IC_SIT_DOC == "R")
                           && (tb47.DT_VEN_DOC > strDt_Ini && tb47.DT_VEN_DOC < strDt_Fim)
                           && (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : 0 == 0)
                           && (strP_CO_EMP_UNID_CONT != -1 ? tb47.CO_EMP_UNID_CONT == strP_CO_EMP_UNID_CONT : 0 == 0)
                           select new RelPosicInadPorSerieTurma
                           {
                               SiglaUnid = tb25.sigla,
                               Status = tb47.IC_SIT_DOC,
                               Matricula = tb07.NU_NIRE,
                               Aluno = tb07.NO_ALU,
                               CPFResponsavel = tb47.TB108_RESPONSAVEL.NU_CPF_RESP,
                               Documento = tb47.NU_DOC,
                               DataVencimento = tb47.DT_VEN_DOC,
                               ValorDocumento = tb47.VR_PAR_DOC,
                               ValorPago = tb47.VR_PAG != null ? tb47.VR_PAG : 0,
                               Juros = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) : (tb47.VR_JUR_PAG != null ? tb47.VR_JUR_PAG : 0),
                               Multa = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) : (tb47.VR_MUL_PAG != null ? tb47.VR_MUL_PAG : 0),
                               Desconto = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_PAG != null ? tb47.VR_DES_PAG : 0),
                               TpMulta = tb47.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb47.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb47.CO_FLAG_TP_VALOR_DES,
                               Parcela = tb47.NU_PAR
                           }).OrderBy(p => p.Aluno).ThenBy(p => p.DataVencimento);

                var res = lst.ToList();

                this.qtdeTitAluno = (from tb47 in ctx.TB47_CTA_RECEB
                                     where tb47.CO_EMP == strP_CO_EMP_REF
                                     && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "P" || tb47.IC_SIT_DOC == "R")
                                     && tb47.DT_VEN_DOC >= strDt_Ini
                                     && tb47.DT_VEN_DOC <= strDt_Fim
                                     && tb47.TP_CLIENTE_DOC == "A"
                                     select new { tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, tb47.CO_EMP }).Count();

                this.valorSumTitulos = (from tb47 in ctx.TB47_CTA_RECEB
                                        where tb47.CO_EMP == strP_CO_EMP_REF
                                        && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "Q" || tb47.IC_SIT_DOC == "P" || tb47.IC_SIT_DOC == "R")
                                        && tb47.DT_VEN_DOC >= strDt_Ini
                                        && tb47.DT_VEN_DOC <= strDt_Fim
                                        && tb47.TP_CLIENTE_DOC == "A"
                                        select new { tb47.VR_PAR_DOC }).Sum(p => p.VR_PAR_DOC);

                foreach (var iLst in res)
                {
                    iLst.DataDif = DateTime.Now.Subtract(iLst.DataVencimento);
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPosicInadPorSerieTurma at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Posição de Inadimplência por Série/Turma

        public class RelPosicInadPorSerieTurma
        {
            public string SiglaUnid { get; set; }
            public string Status { get; set; }
            public string CPFResponsavel { get; set; }
            public int Matricula { get; set; }
            public string Aluno { get; set; }
            public DateTime DataVencimento { get; set; }
            public string Documento { get; set; }
            public decimal ValorDocumento { get; set; }
            public decimal? ValorPago { get; set; }
            public decimal? Juros { get; set; }
            public decimal? Multa { get; set; }
            public decimal? Desconto { get; set; }
            public string TpMulta { get; set; }
            public string TpJuros { get; set; }
            public string TpDesc { get; set; }
            public decimal? Result { get; set; }
            public TimeSpan DataDif { get; set; }
            public int Parcela { get; set; }



            public string NomeTruncado
            {
                get
                {
                    if (Aluno.Length >= 15)
                    {
                        return Aluno.Substring(0, 15) + "...";
                    }
                    else
                    {
                        return Aluno;
                    }

                }
            }



            public String MyMatricula
            {
                get { return String.Format("00{0}", Matricula); }

            }

            public int QtdeDias
            {
                get
                {
                    return this.DataDif.Days;
                }
            }

            public string CPFDesc
            {
                get
                {
                    return Funcoes.Format(this.CPFResponsavel, TipoFormat.CPF);
                }
            }

            public decimal? JurosDesc
            {
                get
                {
                    if (Convert.ToDecimal(this.DataDif.Days) > 0 && (this.Status == "A" || this.Status == "R"))
                    {
                        if (this.TpJuros == "P")
                        {
                            Result = ((this.ValorDocumento / 100) * this.Juros) * Convert.ToDecimal(this.DataDif.Days);
                            return Result;

                        }
                        else if (this.TpJuros == "V")
                        {
                            Result = (this.Juros) * Convert.ToDecimal(this.DataDif.Days);
                            return Result;
                        }
                    }
                    else if (this.Status == "P")
                    {
                        return this.Juros;
                    }
                    else
                    {
                        return null;
                    }

                    return null;
                }
            }

            public decimal? MultaDesc
            {
                get
                {
                    if (Convert.ToDecimal(this.DataDif.Days) > 0 && (this.Status == "A" || this.Status == "R"))
                    {
                        if (this.TpMulta == "P")
                        {
                            Result = ((this.ValorDocumento / 100) * this.Multa);
                            return Result;
                        }
                        else
                        {
                            return Multa;
                        }
                    }
                    else if (this.Status == "P")
                    {
                        return this.Multa;
                    }
                    else
                        return null;
                }
            }

            public decimal? DescontoDesc
            {
                get
                {
                    if (this.TpDesc == "P" && (this.Status == "A" || this.Status == "R"))
                    {
                        Result = ((this.ValorDocumento / 100) * this.Desconto);
                        return Result;
                    }
                    else
                    {
                        return this.Desconto;
                    }
                }
            }

            public decimal? TotalDesc
            {
                get
                {
                    if (Convert.ToDecimal(this.DataDif.Days) > 0 && (this.Status == "A" || this.Status == "R"))
                    {
                        return this.ValorDocumento +
                            (this.JurosDesc != null ? this.JurosDesc : 0) +
                            (this.MultaDesc != null ? this.MultaDesc : 0) -
                            (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                    else if (this.Status == "P")
                    {
                        return this.ValorDocumento - this.ValorPago;
                    }
                    else
                    {
                        return this.ValorDocumento - (this.DescontoDesc != null ? this.DescontoDesc : 0);
                    }
                }
            }

            public decimal? Saldo
            {
                get
                {
                    if (this.Status == "A" || this.Status == "R")
                    {
                        return this.TotalDesc;
                    }
                    else
                    {
                        return this.ValorDocumento - this.ValorPago;
                    }
                }
            }

            public string NomeDesc
            {
                get
                {
                    return this.Aluno + " - Nº NIRE: " + this.Matricula.ToString();
                }
            }

            public String Data2DigAno
            {
                get
                {
                    return DataVencimento.ToString("dd/MM/yy");

                }
            }
        }
        #endregion

        #region eventos de componentes do relatório

        private void lblValorDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totParcValorDocto = totParcValorDocto + decimal.Parse(lblValorDocto.Text);
            }
        }

        private void lblMultaDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblMultaDocto.Text != "")
            {
                totParcValorMulta = totParcValorMulta + decimal.Parse(lblMultaDocto.Text);
            }
        }

        private void lblJurosDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblJurosDocto.Text != "")
            {
                totParcValorJuros = totParcValorJuros + decimal.Parse(lblJurosDocto.Text);
            }
        }

        private void lblDesctoDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblDesctoDocto.Text != "")
            {
                totParcValorDescto = totParcValorDescto + decimal.Parse(lblDesctoDocto.Text);
            }
        }

        private void lblValorTotDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorTotDocto.Text != "")
            {
                totParcValorTotal = totParcValorTotal + decimal.Parse(lblValorTotDocto.Text);
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotParcDocto.Text = String.Format("{0:#,##0.00}", totParcValorDocto);
            lblTotPago.Text = String.Format("{0:#,##0.00}", totParcValorPago);
            lblTotParcMulta.Text = String.Format("{0:#,##0.00}", totParcValorMulta);
            lblTotParcJuros.Text = String.Format("{0:#,##0.00}", totParcValorJuros);
            lblTotParcDescto.Text = String.Format("{0:#,##0.00}", totParcValorDescto);
            lblTotParcTot.Text = String.Format("{0:#,##0.00}", totParcValorTotal);
            lblTotSaldo.Text = String.Format("{0:#,##0.00}", totParcValorSaldo);

            lblDescDescTotal.Text = "( Tot Titulo: " + this.qtdeTitAluno.ToString() + " - Inadim. Média: R$ " +
                String.Format("{0:#,##0.00}", totParcValorDocto / this.qtdeTitAluno) + " (Tot CAR Período: R$ " + String.Format("{0:#,##0.00}", this.valorSumTitulos) +
                " - Inadim.: " + String.Format("{0:#,##0.00}", (totParcValorDocto * 100) / this.valorSumTitulos) + "%)";
        }

        private void lblSexo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var obj = (XRTableCell)sender;

            if (int.Parse(obj.Text) > 0)
            {
                obj.Text = "+" + obj.Text;
                obj.ForeColor = Color.Red;
            }
            else if (int.Parse(obj.Text) < 0)
            {
                obj.ForeColor = Color.Blue;
            }
            else
            {
                obj.Text = "+" + obj.Text;
                obj.ForeColor = Color.Black;
            }

        }

        private void lblValorPago_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorPago.Text != "")
            {
                totParcValorPago = totParcValorPago + decimal.Parse(lblValorPago.Text);
            }
        }

        private void lblValorSlado_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorSaldo.Text != "")
            {
                totParcValorSaldo = totParcValorSaldo + decimal.Parse(lblValorSaldo.Text);
            }
        }

        #endregion
    }
}
