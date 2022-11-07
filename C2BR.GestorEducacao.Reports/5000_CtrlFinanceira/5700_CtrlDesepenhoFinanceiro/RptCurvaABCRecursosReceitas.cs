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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5700_CtrlDesepenhoFinanceiro
{
    public partial class RptCurvaABCRecursosReceitas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private decimal valorSumTitulos = 0;
        private decimal totGeralValorTitul = 0;
        private int numGeralTitul = 0;
        private int numSeq = 1;

        public RptCurvaABCRecursosReceitas()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int coEmpRef,
                               string situDoc,
                               string tpCliente,
                               DateTime dtInicio,
                               DateTime dtFim,
                               int strP_CO_AGRUP,
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

                #region Query Titulos/Fornecedor

                var lst = tpCliente == "A" ?
                    (situDoc == "A" || situDoc == "C" ? 
                    from tb47 in ctx.TB47_CTA_RECEB
                        join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                        where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                        && tb47.CO_EMP == coEmpRef
                        && tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                        && tb47.IC_SIT_DOC == situDoc
                        && tb47.TP_CLIENTE_DOC == tpCliente
                        && (strP_CO_AGRUP != 0 ? tb47.CO_AGRUP_RECDESP == strP_CO_AGRUP : strP_CO_AGRUP == 0)
                        group tb47 by tb07.CO_ALU into g
                        orderby g.Key
                        select new TitulosReceitaCliente 
                        { 
                            Codigo = g.Key, 
                            NumeroTitulos = g.Count(), 
                            TotalValorTitulo = g.Sum(p => p.VR_PAR_DOC)
                        }:
                    from tb47 in ctx.TB47_CTA_RECEB
                        join tb07 in ctx.TB07_ALUNO on tb47.CO_ALU equals tb07.CO_ALU
                        where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                        && tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                        && tb47.IC_SIT_DOC == situDoc
                        && tb47.TP_CLIENTE_DOC == tpCliente
                        && tb47.CO_EMP == coEmpRef
                        group tb47 by tb07.CO_ALU into g
                        select new TitulosReceitaCliente 
                        { 
                            Codigo = g.Key, 
                            NumeroTitulos = g.Count(), 
                            TotalValorTitulo = g.Sum(p => p.VR_PAG ?? 0)
                        }
                     )
                     :
                     (situDoc == "A" || situDoc == "C" ? 
                     from tb47 in ctx.TB47_CTA_RECEB
                        where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                        && tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                        && tb47.IC_SIT_DOC == situDoc
                        && tb47.TP_CLIENTE_DOC == tpCliente
                        && tb47.CO_EMP == coEmpRef
                        group tb47 by tb47.TB103_CLIENTE.CO_CLIENTE into g
                        orderby g.Key
                        select new TitulosReceitaCliente
                        {
                            Codigo = g.Key,
                            NumeroTitulos = g.Count(),
                            TotalValorTitulo = g.Sum(p => p.VR_PAR_DOC)
                        } :
                     from tb47 in ctx.TB47_CTA_RECEB
                        where tb47.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                        && tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                        && tb47.IC_SIT_DOC == situDoc
                        && tb47.TP_CLIENTE_DOC == tpCliente
                        && tb47.CO_EMP == coEmpRef
                        group tb47 by tb47.TB103_CLIENTE.CO_CLIENTE into g
                        select new TitulosReceitaCliente
                        {
                            Codigo = g.Key,
                            NumeroTitulos = g.Count(),
                            TotalValorTitulo = g.Sum(p => p.VR_PAG ?? 0)
                        }
                     );

                var res = lst.ToList();

                if (res.Count > 0)
                {
                    this.valorSumTitulos = situDoc == "A" || situDoc == "C" ? (from tb47 in ctx.TB47_CTA_RECEB
                                                                               where tb47.CO_EMP == coEmpRef
                                                                               && tb47.IC_SIT_DOC == situDoc
                                                                               && tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                                                                               && tb47.TP_CLIENTE_DOC == tpCliente
                                                                               select new { tb47.VR_PAR_DOC }).Sum(p => p.VR_PAR_DOC) :
                                        (from tb47 in ctx.TB47_CTA_RECEB
                                         where tb47.CO_EMP == coEmpRef
                                         && tb47.IC_SIT_DOC == situDoc
                                         && tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                                         && tb47.TP_CLIENTE_DOC == tpCliente
                                         select new { tb47.VR_PAG }).Sum(p => p.VR_PAG ?? 0);
                }                

                foreach (var iLst in res)
                {
                    var cliente = tpCliente == "A" ? (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                       where tb07.CO_ALU == iLst.Codigo
                                       select new { nome = tb07.NO_ALU, codigo = tb07.NU_NIRE, codigoDesc = "" }).FirstOrDefault() :
                                       (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                        where tb103.CO_CLIENTE == iLst.Codigo
                                        select new 
                                        { 
                                            nome = tb103.NO_FAN_CLI,
                                            codigo = tb103.CO_CLIENTE,
                                            codigoDesc = (tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length >= 11) ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                            ((tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length >= 14) ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI) 
                                        }).FirstOrDefault();

                    iLst.Cliente = cliente.nome;
                    iLst.CodigoDesc = tpCliente == "A" ? cliente.codigo.ToString() : cliente.codigoDesc;
                    iLst.TotalGeralValorTitulo = valorSumTitulos;
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta a lista no DataSource do Relatorio
                bsReport.Clear();
                foreach (TitulosReceitaCliente at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Titulos/Cliente ou Aluno do Relatorio

        public class TitulosReceitaCliente
        {
            public decimal TotalValorTitulo { get; set; }
            public string Cliente { get; set; }
            public int Codigo { get; set; }
            public int NumeroTitulos { get; set; }            
            public string CodigoDesc { get; set; }
            public decimal TotalGeralValorTitulo { get; set; }

            public decimal? TotalDesc
            {
                get
                {
                    return this.TotalGeralValorTitulo > 0 ? (this.TotalValorTitulo * 100) / TotalGeralValorTitulo : 0;
                }
            }
        }        
        #endregion

        private void lblNumSeq_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblNumSeq.Text = numSeq.ToString();

            numSeq++;
        }

        private void lblDeficiencia_AfterPrint(object sender, EventArgs e)
        {
            numGeralTitul = numGeralTitul + int.Parse(lblDeficiencia.Text);
        }

        private void lblValorTotalTit_AfterPrint(object sender, EventArgs e)
        {
            totGeralValorTitul = totGeralValorTitul + decimal.Parse(lblValorTotalTit.Text);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotTitulos.Text = numGeralTitul.ToString().PadLeft(3, '0');
            lblTotValorTitulos.Text = String.Format("{0:#,##0.00}", totGeralValorTitul);
        }
    }
}
