using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro
{
    public partial class RptMovFinacConsolContasReceb : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMovFinacConsolContasReceb()
        {
            InitializeComponent();
        }


        public int InitReport(string parametros,
                              int  strP_CO_EMP,
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

                //var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           

                //           select tb47);

                //var res = lst.toList();  

                //// Erro: não encontrou registros
                //if (res.Count == 0)
                //    return -1;

                // Seta a lista no DataSource do Relatorio
                //bsReport.Clear();
                //foreach (RelPlanejConta at in res)
                //    bsReport.Add(at);

                return 1;
            }
            catch
            {
                return 0;
            }



        }



    }
}
