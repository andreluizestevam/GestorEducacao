using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro
{
    public partial class RptPosicaoFinancConsolidada : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptPosicaoFinancConsolidada()
        {
            InitializeComponent();
        }

        #region Init Report


        public int InitReport(int codInst,
                                string parametros,
                                string strP_UNID_CON,
                                List<string> strSIT_PAG,
                                string strP_TIPO,
                                DateTime? strP_DT_INI,
                                DateTime? strP_DT_fIM,
                                string infos)
        {
            try
            {

                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codInst);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);
                #endregion

                // Instancia o contexto
               
                var ctx = GestorEntities.CurrentContext;

                List<FinancConsolidada> lst = null;
                if (strP_TIPO == "C")
                {
                    // aqui está o Linq for C#
                    lst = (from tb47 in ctx.TB47_CTA_RECEB
                           // clausula Where
                           where tb47.TB099_CENTRO_CUSTO != null
                           && strSIT_PAG.Contains(tb47.IC_SIT_DOC)
                           && (tb47.DT_CAD_DOC >= strP_DT_INI && tb47.DT_CAD_DOC <= strP_DT_fIM)
                           group tb47 by new // Group by
                           {
                               tb47.TB099_CENTRO_CUSTO.NU_CTA_CENT_CUSTO,
                               tb47.TB099_CENTRO_CUSTO.DE_CENT_CUSTO
                           } into g
                           select new FinancConsolidada // onde selecionamos os campos
                           {
                               Titulo = g.Key.NU_CTA_CENT_CUSTO + " - " + g.Key.DE_CENT_CUSTO,
                               TDA = g.Where(p => p.IC_SIT_DOC == "A").Count(),
                               TDQ = g.Where(p => p.IC_SIT_DOC == "Q").Count(),
                               TDP = g.Where(p => p.IC_SIT_DOC == "P").Count(),
                               TDoc = g.Count(),
                               Docto = g.Sum(x => x.VR_PAR_DOC),
                               Juros = g.Sum(x => x.VR_JUR_PAG != null ? x.VR_JUR_PAG.Value : 0),
                               Multa = g.Sum(x => x.VR_MUL_PAG != null ? x.VR_MUL_PAG.Value : 0)
                           }).ToList();
                }
                else if (strP_TIPO == "P")
                {
                    lst = (from TB47 in ctx.TB47_CTA_RECEB
                           where
                            strSIT_PAG.Contains(TB47.IC_SIT_DOC)
                           && (TB47.DT_CAD_DOC >= strP_DT_INI && TB47.DT_CAD_DOC <= strP_DT_fIM)
                           group TB47 by new
                           {
                               TB47.TB56_PLANOCTA.NU_CONTA_PC,
                               TB47.TB56_PLANOCTA.DE_CONTA_PC
                           } into g
                           select new FinancConsolidada
                           {

                               Codigo = g.Key.NU_CONTA_PC,
                               Titulo = g.Key.DE_CONTA_PC,
                               TDA = g.Where(p => p.IC_SIT_DOC == "A").Count(),
                               TDQ = g.Where(p => p.IC_SIT_DOC == "Q").Count(),
                               TDP = g.Where(p => p.IC_SIT_DOC == "P").Count(),
                               TDoc = g.Count(),
                               Docto = g.Sum(x => x.VR_PAR_DOC),
                               Juros = g.Sum(x => x.VR_JUR_PAG != null ? x.VR_JUR_PAG.Value : 0),
                               Multa = g.Sum(x => x.VR_MUL_PAG != null ? x.VR_MUL_PAG.Value : 0)
                           }).ToList();
                }
                else if (strP_TIPO == "M")
                {
                    lst = (from tb39 in ctx.TB39_HISTORICO
                           from tb47 in tb39.TB47_CTA_RECEB
                           where
                           strSIT_PAG.Contains(tb47.IC_SIT_DOC) &&
                          (tb47.DT_CAD_DOC >= strP_DT_INI && tb47.DT_CAD_DOC <= strP_DT_fIM)
                           group tb47 by new
                           {
                               tb47.TB39_HISTORICO.CO_HISTORICO,
                               tb47.TB39_HISTORICO.DE_HISTORICO
                           } into g
                           select new FinancConsolidada
                           {
                               Codigo = g.Key.CO_HISTORICO,
                               Titulo = g.Key.DE_HISTORICO,
                               TDA = g.Where(p => p.IC_SIT_DOC == "A").Count(),
                               TDQ = g.Where(p => p.IC_SIT_DOC == "Q").Count(),
                               TDP = g.Where(p => p.IC_SIT_DOC == "P").Count(),
                               TDoc = g.Count(),
                               Docto = g.Sum(x => x.VR_PAR_DOC),
                               Juros = g.Sum(x => x.VR_JUR_PAG != null ? x.VR_JUR_PAG.Value : 0),
                               Multa = g.Sum(x => x.VR_MUL_PAG != null ? x.VR_MUL_PAG.Value : 0)
                           }).ToList();
                }
                var res = lst.OrderBy(x => x.Titulo).ToList();

                if (res == null || res.Count == 0)
                    return -1;

                // Seta os titulos no DataSource do Relatorio
                bsReport.Clear();
                foreach (FinancConsolidada at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }
        #endregion





    }

    public class FinancConsolidada
    {
        public int? Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao
        {
            get
            {
                return (Codigo != null ? Codigo.ToString() + " - " : "") + Titulo;
            }
        }
        public decimal TDA { get; set; }
        public decimal TDP { get; set; }
        public decimal TDQ { get; set; }
        public decimal TDoc { get; set; }
        public decimal Docto { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal PDA
        {
            get
            {
                return TDA * 100 / TDoc;
            }
        }
        public decimal PDP
        {
            get
            {
                return TDP * 100 / TDoc;
            }
        }
        public decimal PDQ
        {
            get
            {
                return TDQ * 100 / TDoc;
            }
        }
        public decimal Total
        {
            get
            {
                return Docto + Juros + Multa;
            }
        }
    }
}
