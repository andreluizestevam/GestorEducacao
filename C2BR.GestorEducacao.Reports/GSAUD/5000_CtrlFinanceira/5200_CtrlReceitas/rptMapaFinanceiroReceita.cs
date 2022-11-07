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
    public partial class rptMapaFinanceiroReceita : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public rptMapaFinanceiroReceita()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int coUniContr,
                              DateTime dtP_DT_INI,
                              DateTime dtP_DT_FIM,
                              string infos,
                              string NO_RELATORIO,
                              string strP_IC_SIT_DOC,
                              bool incluCance)
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
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "RELATÓRIO ANALÍTICO FINANCEIRO DE RECEITAS POR PERÍODO*");

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;



                var rest = (from tbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                            where (tbs47.CO_EMP == strP_CO_EMP)
                            && (coUniContr != 0 ? tbs47.CO_EMP_UNID_CONT == coUniContr : 0 == 0)
                            && ((tbs47.DT_VEN_DOC >= dtP_DT_INI) && (tbs47.DT_VEN_DOC <= dtP_DT_FIM))
                            && (strP_IC_SIT_DOC != "-1" ? tbs47.IC_SIT_DOC == strP_IC_SIT_DOC : 0 == 0)
                            && (!incluCance && strP_IC_SIT_DOC != "C" ? tbs47.IC_SIT_DOC != "C" : 0 == 0)
                            group tbs47 by tbs47.DT_VEN_DOC into grp
                            select new MapaFinaceiroReceita
                            {
                                // Previsão Financeira
                                DtVenctoPF = grp.Key,

                                VrDocumetoPF = grp.Where(x => x.FL_TIPO_PREV_RECEB == "B").Any() ? grp.Where(x => x.FL_TIPO_PREV_RECEB == "B").Sum(x => x.VL_PAR_DOC) : 0,
                                VrCartaoPF = grp.Where(x => x.FL_TIPO_PREV_RECEB == "C").Any() ? grp.Where(x => x.FL_TIPO_PREV_RECEB == "C").Sum(x => x.VL_PAR_DOC) : 0,
                                VrChequePF = grp.Where(x => x.FL_TIPO_PREV_RECEB == "H").Any() ? grp.Where(x => x.FL_TIPO_PREV_RECEB == "H").Sum(x => x.VL_PAR_DOC) : 0,
                                VrDinheiroPF = grp.Where(x => x.FL_TIPO_PREV_RECEB == "D").Any() ? grp.Where(x => x.FL_TIPO_PREV_RECEB != "D").Sum(x => x.VL_PAR_DOC) : 0,
                                VrMistaPF = grp.Where(x => x.FL_TIPO_PREV_RECEB == "M").Any() ? grp.Where(x => x.FL_TIPO_PREV_RECEB != "M").Sum(x => x.VL_PAR_DOC) : 0,

                                vrBancoRD = grp.Where(x => x.FL_TIPO_RECEB == "B" && x.IC_SIT_DOC == "Q").Sum(x => x.VL_PAG) ?? 0,
                                vrDinheiroRD = grp.Where(x => x.FL_TIPO_RECEB != "D" && x.IC_SIT_DOC == "Q").Sum(x => x.VL_PAG) ?? 0,
                                vrChequeRD = grp.Where(x => x.FL_TIPO_RECEB == "H" && x.IC_SIT_DOC == "Q").Sum(x => x.VL_PAG) ?? 0,
                                vrCartaoRD = grp.Where(x => x.FL_TIPO_RECEB == "C" && x.IC_SIT_DOC == "Q").Sum(x => x.VL_PAG) ?? 0,
                                vrMistaRD = grp.Where(x => x.FL_TIPO_RECEB == "M" && x.IC_SIT_DOC == "Q").Sum(x => x.VL_PAG) ?? 0,
                                vrJrsMultRD = (grp.Sum(x => x.VL_JUR_PAG) ?? 0) + (grp.Sum(x => x.VL_MUL_PAG) ?? 0),

                                QdtVenc = grp.Where(x => x.IC_SIT_DOC == "A" && grp.Key < DateTime.Today).Count(),
                                VencTotal = grp.Where(x => x.IC_SIT_DOC == "A" && grp.Key < DateTime.Today).Sum(x => x.VL_PAR_DOC)
                            }).OrderBy(p => p.DtVenctoPF);


                var res = rest.ToList();


                //
                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (MapaFinaceiroReceita at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        private void xrTableCell43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell txt = (XRTableCell)sender;
            if (txt.Text == "")
                txt.NullValueText = "0,00";

        }

    }

    public class MapaFinaceiroReceita
    {
        public DateTime DtVenctoPF { get; set; }
        public decimal? VrDocumetoPF { get; set; }
        public decimal PcDocumentoPF
        {
            get
            {
                if (VrTotalPF != 0)
                    return ((VrDocumetoPF ?? 0) * 100) / VrTotalPF.Value;
                else
                    return 0;
            }
        }
        public decimal? VrCartaoPF { get; set; }
        public decimal PcCartaoPF
        {
            get
            {
                if (VrTotalPF != 0)
                    return ((VrCartaoPF ?? 0) * 100) / VrTotalPF.Value;
                else
                    return 0;
            }
        }
        public decimal? VrChequePF { get; set; }
        public decimal PcChequePF
        {
            get
            {
                if (VrTotalPF != 0)
                    return ((VrChequePF ?? 0) * 100) / VrTotalPF.Value;
                else
                    return 0;
            }
        }
        public decimal? VrDinheiroPF { get; set; }
        public decimal PcDinheiroPF
        {
            get
            {
                if (VrTotalPF != 0)
                    return ((VrDinheiroPF ?? 0) * 100) / VrTotalPF.Value;
                else
                    return 0;
            }
        }
        public decimal? VrMistaPF { get; set; }
        public decimal PcMistaPF
        {
            get
            {
                if (VrTotalPF != 0)
                    return ((VrMistaPF ?? 0) * 100) / VrTotalPF.Value;
                else
                    return 0;
            }
        }
        public decimal? VrTotalPF
        {
            get
            {
                return (VrDocumetoPF + VrCartaoPF + VrChequePF + VrDinheiroPF + VrDinheiroPF) ?? 0;
            }
        }
        // Resumo Origem
        public decimal? AlunosRO { get; set; }
        public decimal? DiversasRO { get; set; }

        // Recebimento do Dia
        public decimal? vrBancoRD { get; set; }
        public decimal PcBancoRD
        {
            get
            {
                if (vrTotalRD != 0)
                    return ((vrBancoRD ?? 0) * 100) / vrTotalRD.Value;
                else
                    return 0;
            }
        }
        public decimal? vrDinheiroRD { get; set; }
        public decimal PcDinheiroRD
        {
            get
            {
                if (vrTotalRD != 0)
                    return ((vrDinheiroRD ?? 0) * 100) / vrTotalRD.Value;
                else
                    return 0;
            }
        }
        public decimal? vrCartaoRD { get; set; }
        public decimal PcCartaoRD
        {
            get
            {
                if (vrTotalRD != 0)
                    return ((vrCartaoRD ?? 0) * 100) / vrTotalRD.Value;
                else
                    return 0;
            }
        }
        public decimal? vrChequeRD { get; set; }
        public decimal PcChequeRD
        {
            get
            {
                if (vrTotalRD != 0)
                    return ((vrChequeRD ?? 0) * 100) / vrTotalRD.Value;
                else
                    return 0;
            }
        }
        public decimal? vrMistaRD { get; set; }
        public decimal PcMistaRD
        {
            get
            {
                if (vrTotalRD != 0)
                    return ((vrMistaRD ?? 0) * 100) / vrTotalRD.Value;
                else
                    return 0;
            }
        }
        public decimal? vrJrsMultRD { get; set; }
        public decimal? vrTotalRD
        {
            get
            {
                return (vrBancoRD + vrDinheiroRD + vrCartaoRD + vrChequeRD + vrJrsMultRD + vrMistaRD) ?? 0;
            }
        }
        public decimal? PcPF
        {
            get
            {
                if (VrTotalPF > 0)
                    return ((vrTotalRD ?? 0) * 100) / (VrTotalPF ?? 0);
                else
                    return 0;
            }
        }
        // Vencido
        public int QdtVenc { get; set; }
        public decimal? VencTotal { get; set; }
        public decimal? TotalVenc { get { return VencTotal ?? 0; } }
    }
}
