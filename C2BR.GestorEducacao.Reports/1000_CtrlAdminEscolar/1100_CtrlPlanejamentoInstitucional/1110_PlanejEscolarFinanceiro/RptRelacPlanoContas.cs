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
    public partial class RptRelacPlanoContas : C2BR.GestorEducacao.Reports.RptRetrato
    {
        private int total = 0;

        public RptRelacPlanoContas()
        {
            InitializeComponent();
        }

        #region Init Report
        //strP_CO_EMP, strP_CO_GRUP_CTA, strP_CO_SGRUP_CTA 
        public int InitReport(int codInst,
                                string parametros,
                                string strP_CO_EMP,
                                string strP_TIPO,
                                string strP_CO_GRUP_CTA,
                                string strP_CO_SGRUP_CTA,
                                string strP_CO_SGRUP2_CTA,
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

                int intP_CO_GRUP_CTA = (strP_CO_GRUP_CTA != "T") ? Convert.ToInt32(strP_CO_GRUP_CTA) : 0;
                int intP_CO_SGRUP_CTA = (strP_CO_SGRUP_CTA != "T") ? Convert.ToInt32(strP_CO_SGRUP_CTA) : 0;
                int intP_CO_SGRUP2_CTA = (strP_CO_SGRUP2_CTA != "T") ? Convert.ToInt32(strP_CO_SGRUP2_CTA) : 0;
                #region Query
                var lst = (from tb56 in ctx.TB56_PLANOCTA
                           join tb55 in ctx.TB055_SGRP2_CTA on tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA equals tb55.CO_SGRUP2_CTA into resultado1
                           from tb55 in resultado1.DefaultIfEmpty()
                           where (strP_CO_GRUP_CTA != "T" ? tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == intP_CO_GRUP_CTA : 0 == 0)
                           && (strP_CO_SGRUP_CTA != "T" ? tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == intP_CO_SGRUP_CTA : 0 == 0)
                           && (strP_CO_SGRUP2_CTA != "T" && tb55 != null ? tb55.CO_SGRUP2_CTA == intP_CO_SGRUP2_CTA : 0 == 0)
                           && (strP_TIPO != "0" ? tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == strP_TIPO : 0 == 0) 
                           //&& tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst  
                           select new PlanoContas
                           {
                               Descricao = tb56.DE_CONTA_PC,
                               CodPlano = tb56.NU_CONTA_PC,
                               CNPJInstit = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_NUMERO_CNPJ,
                               SubGrupo = tb56.TB54_SGRP_CTA.DE_SGRUP_CTA,
                               CodSubGroup = tb56.TB54_SGRP_CTA.NR_SGRUP_CTA,
                               SubGrupo2 = tb55.DE_SGRUP2_CTA,
                               CodSubGroup2 = tb55.NR_SGRUP2_CTA,
                               Tipo = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA,
                               Grupo = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.DE_GRUP_CTA,
                               CodGrupo = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.NR_GRUP_CTA
                           });

                #endregion

                var res = lst.OrderBy(x => new { x.CodGrupo, x.CodSubGroup, x.CodSubGroup2, x.CodPlano }).ThenBy(x => x.CodGrupo).ToList();
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
        #endregion

        public class PlanoContas
        {
            public string Tipo { get; set; }
            public string Descricao { get; set; }
            public int? CodPlano { get; set; }
            public string SubGrupo { get; set; }
            public int? CodSubGroup { get; set; }
            public string SubGrupo2 { get; set; }
            public int? CodSubGroup2 { get; set; }
            public string Grupo { get; set; }
            public int? CodGrupo { get; set; }
            public decimal CNPJInstit { get; set; }

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
                    return Tipo == "C" ? "Receita" : Tipo == "D" ? "Custo e Despesa" : Tipo == "T" ? "Título" : Tipo == "I" ? "Investimento" : Tipo == "A" ? "Ativo" : "Passivo";
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
                        return GetId + "." + CodGrupo.ToString() + "." + CodSubGroup.ToString() + "." + (CodSubGroup2 != null ? CodSubGroup2.ToString() : "0");
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

        private void xrTableCell14_AfterPrint(object sender, EventArgs e)
        {
            total = total + 1;
        }
    }
}
