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
    public partial class RptPosicInadimFichaRespon : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal totParcValorDocto, totParcValorPago, totParcValorMulta, totParcValorJuros, totParcValorDescto, totParcValorTotal, totParcValorSaldo = 0;
        private decimal totGeralValorDocto, totGeralValorPago, totGeralValorMulta, totGeralValorJuros, totGeralValorDescto, totGeralValorTotal, totGeralValorSaldo = 0;

        public RptPosicInadimFichaRespon()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_EMP_UNID_CONT,
                              int strP_CO_RESP,
                              int strP_CO_AGRUP,
                              string infos,
                              DateTime strDt_Ini,
                              DateTime strDt_Fim)
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

                #region Query Colaborador Parametrizada

                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                           join tb01 in ctx.TB01_CURSO on tb47.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb47.CO_TUR equals tb129.CO_TUR
                           join tb25 in ctx.TB25_EMPRESA on tb47.CO_EMP_UNID_CONT equals tb25.CO_EMP
                           join tb315 in ctx.TB315_AGRUP_RECDESP on tb47.CO_AGRUP_RECDESP equals tb315.ID_AGRUP_RECDESP
                           where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strP_CO_EMP_REF != -1 ? tb47.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                           && (strP_CO_RESP != 0 ? tb47.TB108_RESPONSAVEL.CO_RESP == strP_CO_RESP : 0 == 0)
                           && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "P" || tb47.IC_SIT_DOC == "R")
                           && tb47.DT_VEN_DOC < DateTime.Now
                           && (tb47.DT_VEN_DOC > strDt_Ini && tb47.DT_VEN_DOC < strDt_Fim)
                           && (strP_CO_AGRUP != -1 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : 0 == 0)
                           && (strP_CO_EMP_UNID_CONT != -1 ? tb47.CO_EMP_UNID_CONT == strP_CO_EMP_UNID_CONT : 0 == 0)
                           select new RelPosicInadFichaRespo
                           {
                               CO_EMP_LOGADA = strP_CO_EMP,
                               SiglaUnid = tb25.sigla,
                               Status = tb47.IC_SIT_DOC,
                               Matricula = tb07.NU_NIRE,
                               Aluno = tb07.NO_ALU,
                               DescSerie = " - Mod: " + tb01.TB44_MODULO.DE_MODU_CUR + " - Série: " + tb01.NO_CUR +
                               " - Turma: " + tb129.NO_TURMA + " - Convênio: " + (tb07.TB148_TIPO_BOLSA != null ? tb07.TB148_TIPO_BOLSA.DE_TIPO_BOLSA + ")" : "Nenhum )"),
                               Responsavel = tb47.TB108_RESPONSAVEL.NO_RESP,
                               CpfRespon = (tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Length == 11 ? tb47.TB108_RESPONSAVEL.NU_CPF_RESP : "00000000000"),
                               Agrupador = tb315.DE_SITU_AGRUP_RECDESP,
                               DataMov = tb47.DT_CAD_DOC,
                               Documento = tb47.NU_DOC,
                               DataVencimento = tb47.DT_VEN_DOC,
                               ValorDocumento = tb47.VR_PAR_DOC,
                               ValorPago = tb47.VR_PAG != null ? tb47.VR_PAG : 0,
                               Juros = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0 : tb47.VR_JUR_PAG != null ? tb47.VR_JUR_PAG : 0,
                               Multa = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0 : tb47.VR_MUL_PAG != null ? tb47.VR_MUL_PAG : 0,
                               Desconto = (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") ? tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0 : tb47.VR_DES_PAG != null ? tb47.VR_DES_PAG : 0,
                               TpMulta = tb47.CO_FLAG_TP_VALOR_MUL,
                               TpJuros = tb47.CO_FLAG_TP_VALOR_JUR,
                               TpDesc = tb47.CO_FLAG_TP_VALOR_DES,
                               Parcela = tb47.NU_PAR
                           }).OrderBy(p => p.Responsavel).ThenBy(p => p.Aluno).ThenBy(p => p.DataVencimento);

                var res = lst.ToList();

               

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelPosicInadFichaRespo at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Posição de Inadimplência por Responsável do Relatorio

        public class RelPosicInadFichaRespo
        {
            public int CO_EMP_LOGADA { get; set; }
            public string SiglaUnid { get; set; }
            public string Responsavel { get; set; }
            public string CpfRespon { get; set; }
            public string Status { get; set; }
            public int Matricula { get; set; }
            public string Agrupador { get; set; }
            public string Aluno { get; set; }
            public string Aluno_V
            {
                get
                {
                    string dadosRespon = "  -  Responsável: (" + CpfRespon.Insert(3, ".").Insert(7, ".").Insert(11, "-") + " - " + Responsavel + ")";

                    string coTipoUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(this.CO_EMP_LOGADA).CO_TIPO_UNID;
                    if(coTipoUnid == "PGS")
                        return "Paciente: " + this.Aluno + dadosRespon;
                    else
                        return "Aluno: " + this.Aluno + dadosRespon;
                }
            }
            public string DescSerie { get; set; }
            public DateTime DataMov { get; set; }
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
           
            public int Parcela { get; set; }

            public int QtdeDias
            {
                get
                {
                    TimeSpan DataDif = DateTime.Now.Subtract(this.DataVencimento);
                    return DataDif.Days;
                }
            }

            public decimal? JurosDesc
            {
                get
                {
                    if (this.QtdeDias > 0 && (this.Status == "A" || this.Status == "R"))
                    {
                        if (this.TpJuros == "P")
                        {
                            return ((this.ValorDocumento * this.Juros)/100) * this.QtdeDias;
                        }
                        else if (this.TpJuros == "V")
                        {
                            return (this.Juros) * this.QtdeDias;

                        }
                    }
                    else if (this.Status == "P")
                    {
                        return this.Juros;
                    }
                    else
                    {
                        return 0;
                    }

                    return 0;
                }
            }

            public decimal? MultaDesc
            {
                get
                {
                    if (this.QtdeDias > 0 && (this.Status == "A" || this.Status == "R"))
                    {
                        if (this.TpMulta == "P")
                        {
                            return ((this.ValorDocumento / 100) * this.Multa);
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
                        return 0;
                }
            }

            public decimal? DescontoDesc
            {
                get
                {
                    return ((this.ValorDocumento / 100) * this.Desconto != null ? this.Desconto : 0);
                }
            }

            public decimal? TotalDesc
            {
                get
                {
                    if (this.QtdeDias > 0 && (this.Status == "A" || this.Status == "R"))
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
                        return this.ValorPago - this.TotalDesc;
                    }
                }
            }

            public string NomeDesc
            {
                get
                {
                    string coTipoUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(this.CO_EMP_LOGADA).CO_TIPO_UNID;
                    return "(NIRE: " + this.Matricula.ToString() + (coTipoUnid == "PGE" ? this.DescSerie : " )");
                }
            }
        }
        #endregion

        private void lblValorDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblValorDocto.Text != "")
            {
                totParcValorDocto = totParcValorDocto + decimal.Parse(lblValorDocto.Text);
            }
        }

        private void lblJurosDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblJurosDocto.Text != "")
            {
                totParcValorJuros = totParcValorJuros + decimal.Parse(lblJurosDocto.Text);
            }
        }

        private void lblMultaDocto_AfterPrint(object sender, EventArgs e)
        {
            if (lblMultaDocto.Text != "")
            {
                totParcValorMulta = totParcValorMulta + decimal.Parse(lblMultaDocto.Text);
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

            totGeralValorDocto = totGeralValorDocto + totParcValorDocto;
            totGeralValorPago = totGeralValorPago + totParcValorPago;
            totGeralValorMulta = totGeralValorMulta + totParcValorMulta;
            totGeralValorJuros = totGeralValorJuros + totParcValorJuros;
            totGeralValorDescto = totGeralValorDescto + totParcValorDescto;
            totGeralValorTotal = totGeralValorTotal + totParcValorTotal;
            totGeralValorSaldo = totGeralValorSaldo + totParcValorSaldo;

            totParcValorDocto = totParcValorPago = totParcValorMulta = totParcValorJuros = totParcValorDescto = totParcValorTotal = totParcValorSaldo = 0;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
         
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void lblValorPago_AfterPrint(object sender, EventArgs e)
        {
           
        }

        private void lblSaldo_AfterPrint(object sender, EventArgs e)
        {
            if (lblSaldo.Text != "")
            {
                totParcValorSaldo = totParcValorSaldo + decimal.Parse(lblSaldo.Text);
            }
        }

        private void xrTableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "+" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (int.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "-" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else
                {
                    obj.ForeColor = Color.Black;
                }
            }
        }

        private void lblSaldo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (decimal.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.Blue;
                    obj.Text = "+"+ obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (decimal.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Red;
                    obj.Text = "-" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else
                {
                    obj.ForeColor = Color.Black;
                }
            }
        }

        private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
            {
                if (decimal.Parse(obj.Text) > 0)
                {
                    obj.ForeColor = Color.BlueViolet;
                    obj.Text = "+" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else if (decimal.Parse(obj.Text) < 0)
                {
                    obj.ForeColor = Color.Green;
                    obj.Text = "-" + obj.Text.Replace("+", "").Replace("-", "");
                }
                else
                {
                    obj.ForeColor = Color.Black;
                }
            }
        }
    }
}
