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
    public partial class RtpBalancetePlanoContas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RtpBalancetePlanoContas()
        {
            InitializeComponent();
        }


        public int InitReport(int codInst,
                                string parametros,
                                int strP_CO_EMP,
                                string strP_UNI_CTR,
                                string strP_TIPO,
                                string strP_CO_GRUP_CTA,
                                string strP_CO_SGRUP_CTA,
                                string strP_CO_SGRUP2_CTA,
                                string strP_DATA_INI,
                                string strP_DATA_FIM,
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

                int intP_CO_GRUP_CTA = (strP_CO_GRUP_CTA != "T") ? Convert.ToInt32(strP_CO_GRUP_CTA) : 0;
                int intP_CO_SGRUP_CTA = (strP_CO_SGRUP_CTA != "T") ? Convert.ToInt32(strP_CO_SGRUP_CTA) : 0;
                int intP_CO_SGRUP2_CTA = (strP_CO_SGRUP2_CTA != "T") ? Convert.ToInt32(strP_CO_SGRUP2_CTA) : 0;

                int intP_UNI_CTR = (strP_UNI_CTR != "T") ? int.Parse(strP_UNI_CTR) : 0;

                DateTime? dtini = strP_DATA_INI != "" ? Convert.ToDateTime(strP_DATA_INI) : DateTime.MinValue;
                DateTime? dtfim = strP_DATA_FIM != "" ? Convert.ToDateTime(strP_DATA_FIM) : DateTime.MinValue;
                #region Query

                var lstDesp = (from tb56 in ctx.TB56_PLANOCTA
                               join tb55 in TB055_SGRP2_CTA.RetornaTodosRegistros() on tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA equals tb55.CO_SGRUP2_CTA into resultado1
                               from tb55 in resultado1.DefaultIfEmpty()
                               where
                                   (strP_CO_GRUP_CTA != "T" ? tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == intP_CO_GRUP_CTA : strP_CO_GRUP_CTA == "T")
                                   && (strP_CO_SGRUP_CTA != "T" ? tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == intP_CO_SGRUP_CTA : strP_CO_SGRUP_CTA == "T")
                                   && (strP_CO_SGRUP2_CTA != "T" ? tb55.CO_SGRUP2_CTA == intP_CO_SGRUP2_CTA : strP_CO_SGRUP2_CTA == "T")
                               select new PlanoContas
                               {
                                   Descricao = tb56.DE_CONTA_PC,
                                   CodPlano = tb56.NU_CONTA_PC,
                                   SubGrupo = tb56.TB54_SGRP_CTA.DE_SGRUP_CTA,
                                   CodSubGroup = tb56.TB54_SGRP_CTA.NR_SGRUP_CTA,
                                   SubGrupo2 = tb55.DE_SGRUP2_CTA,
                                   CodSubGroup2 = tb55.NR_SGRUP2_CTA,
                                   Tipo = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA,
                                   Grupo = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.DE_GRUP_CTA,
                                   CodGrupo = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.NR_GRUP_CTA,
                                   CNPJInstit = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_NUMERO_CNPJ,
                                   Valor = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "D" || tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "P" ?
                                   (tb56.TB38_CTA_PAGAR.Where(x => x.DT_CAD_DOC >= dtini && x.DT_CAD_DOC <= dtfim)
                                        .Sum(x => x.IC_SIT_DOC == "A" || x.IC_SIT_DOC == "P" ? x.VR_PAR_DOC : x.IC_SIT_DOC == "Q" ? x.VR_PAG : 0)) :
                                    tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "C" ?
                                    (tb56.TB47_CTA_RECEB.Where(x => x.DT_CAD_DOC >= dtini && x.DT_CAD_DOC <= dtfim)
                                    .Sum(x => x.IC_SIT_DOC == "A" || x.IC_SIT_DOC == "P" ? x.VR_PAR_DOC : x.IC_SIT_DOC == "Q" ? x.VR_PAG : 0)) :
                                  (tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "A" ? (
                                   tb56.DE_CONTA_PC == "Banco" ?                             
                                        (from tbBanco in ctx.TB47_CTA_RECEB
                                         where (tbBanco.CO_SEQU_PC_BANCO == tb56.CO_SEQU_PC &&  tbBanco.FL_ORIGEM_PGTO == "B")
                                        && (tbBanco.DT_CAD_DOC >= dtini && tbBanco.DT_CAD_DOC <= dtfim)
                                         select tbBanco.VR_PAR_DOC).Sum()  : 
                                   tb56.DE_CONTA_PC == "Caixa" ?
                                        (from tbCaixa in ctx.TB47_CTA_RECEB
                                         where (tbCaixa.CO_SEQU_PC_CAIXA == tb56.CO_SEQU_PC && tbCaixa.FL_ORIGEM_PGTO == "C" )
                                        && (tbCaixa.DT_CAD_DOC >= dtini && tbCaixa.DT_CAD_DOC <= dtfim)
                                        select tbCaixa.VR_PAR_DOC).Sum() 
                                    : (tb56.TB47_CTA_RECEB.Where(x => x.DT_CAD_DOC >= dtini && x.DT_CAD_DOC <= dtfim)
                                    .Sum(x => x.IC_SIT_DOC == "A" || x.IC_SIT_DOC == "P" ? x.VR_PAR_DOC : x.IC_SIT_DOC == "Q" ? x.VR_PAG : 0))):0 ) 
                               });

                var lst = (from l in lstDesp
                           group l by
                           new
                           {
                               l.Tipo,
                               l.Grupo,
                               l.CodGrupo,
                               l.SubGrupo,
                               l.CodSubGroup,
                               l.SubGrupo2,
                               l.CodSubGroup2,
                               l.Descricao,
                               l.CodPlano,
                               l.CNPJInstit
                           } into g
                           select new PlanoContas
                                {
                                    Descricao = g.Key.Descricao,
                                    CodPlano = g.Key.CodPlano,
                                    SubGrupo = g.Key.SubGrupo,
                                    CodSubGroup = g.Key.CodSubGroup,
                                    SubGrupo2 = g.Key.SubGrupo2,
                                    CodSubGroup2 = g.Key.CodSubGroup2,
                                    Tipo = g.Key.Tipo,
                                    Grupo = g.Key.Grupo,
                                    CodGrupo = g.Key.CodGrupo,
                                    CNPJInstit = g.Key.CNPJInstit,
                                    Valor = g.Sum(x => x.Valor ?? 0)
                                });
                var res = lst.OrderBy(p => p.CodGrupo).OrderBy(p => p.CodSubGroup).OrderBy(p => p.CodSubGroup2).OrderBy(p => p.CodPlano).ToList();

                #endregion

                // var res = lst.OrderBy(x => new { x.CodGrupo, x.CodSubGroup, x.CodPlano }).ThenBy(x => x.CodGrupo).ToList();
                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os titulos no DataSource do Relatorio
                bsReport.Clear();
                foreach (PlanoContas at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }


        public class PlanoContas
        {
            public DateTime Data { get; set; }
            public string Tipo { get; set; }
            public string Descricao { get; set; }
            public int? CodPlano { get; set; }
            public string SubGrupo { get; set; }
            public int? CodSubGroup { get; set; }
            public string SubGrupo2 { get; set; }
            public int? CodSubGroup2 { get; set; }
            public string Grupo { get; set; }
            public int? CodGrupo { get; set; }
            public string Situacao { get; set; }
            public decimal CNPJInstit { get; set; }

            public decimal? Valor { get; set; }

            public string GetId
            {
                get
                {
                    return Tipo == "C" ? "3" : Tipo == "D" ? "4" : Tipo == "I" ? "5" : Tipo == "A" ? "1" : Tipo == "P" ? "2" : "6";
                }
            }

            public string DescTipo
            {
                get
                {
                    return Tipo == "C" ? "Receita" : Tipo == "D" ? "Despesa" : Tipo == "T" ? "Título" : Tipo == "I" ? "Investimento" : Tipo == "A" ? "Ativo" : "Passivo";
                }
            }

            public string GetCodGrupo
            {
                get
                {
                    if (CNPJInstit != 10689657000161)
                    {
                        return GetId + "." + CodGrupo.ToString().PadLeft(2, '0');
                    }
                    else
                        return GetId + "." + CodGrupo;
                }
            }


            public string GetCodSubGrupo
            {
                get
                {
                    if (CNPJInstit != 10689657000161)
                    {
                        return GetId + "." + CodGrupo.ToString().PadLeft(2, '0') + "." + CodSubGroup.ToString().PadLeft(3, '0');
                    }
                    else
                        return GetId + "." + CodGrupo.ToString() + "." + CodSubGroup.ToString();
                }
            }

            public string GetCodSubGrupo2
            {
                get
                {
                    if (CNPJInstit != 10689657000161)
                    {
                        return GetId + "." + CodGrupo.ToString().PadLeft(2, '0') + "." + CodSubGroup.ToString().PadLeft(3, '0') + "." + (CodSubGroup2 != null ? CodSubGroup2.ToString().PadLeft(3, '0') : "000");
                    }
                    else
                        return GetId + "." + CodGrupo.ToString() + "." + CodSubGroup.ToString() + "." + (CodSubGroup2 != null ? CodSubGroup2.ToString() : "0") ;
                }
            }

            public string GetCodPlano
            {
                get
                {
                    if (CNPJInstit != 10689657000161)
                    {
                        return GetId + "." + CodGrupo.ToString().PadLeft(2, '0') + "." + CodSubGroup.ToString().PadLeft(3, '0') + "." + (CodSubGroup2 != null ? CodSubGroup2.ToString().PadLeft(3, '0') : "000") + "." + CodPlano.ToString().PadLeft(4, '0');
                    }
                    else
                        return GetId + "." + CodGrupo.ToString() + "." + CodSubGroup.ToString() + "." + (CodSubGroup2 != null ? CodSubGroup2.ToString() : "0") + "." + CodPlano.ToString().PadLeft(4, '0');
                }
            }
        }

        private void lblTotalTipo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /*XRTableCell obj = (XRTableCell)sender;
            if (lblDescTipo.Text == "Receita")
            {
                obj.Text = this.vlReceitaTotal != null ? String.Format("{0:R$#,##0.00}", this.vlReceitaTotal) : "0,00";
            }
            else
            {
                obj.Text = this.vlDespesaTotal != null ? String.Format("{0:R$#,##0.00}", this.vlDespesaTotal) : "0,00";
            }*/
        }
    }
}
