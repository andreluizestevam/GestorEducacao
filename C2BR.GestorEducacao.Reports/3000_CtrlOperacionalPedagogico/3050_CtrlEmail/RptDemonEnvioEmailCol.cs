using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;
using C2BR.GestorEducacao.Reports;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3050_CtrlEmail
{
    public partial class RptDemonEnvioEmailCol : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonEnvioEmailCol()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                                string infos,
                                int coEmp,
                                int CO_COL,
                                string strP_DT_INI,
                                string strP_DT_FIM,
                                string nomeFunc
            )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                C2BR.GestorEducacao.Reports.ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                lblTitulo.Text = nomeFunc;
                
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Conversão de variáveis
                DateTime DT_INI = strP_DT_INI != "" ? Convert.ToDateTime(strP_DT_INI) : DateTime.MinValue;
                DateTime DT_FIM = strP_DT_FIM != "" ? Convert.ToDateTime(strP_DT_FIM) : DateTime.MaxValue;

                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               join tb425 in TB423_EMAIL_USUAR_GEMPR.RetornaTodosRegistros() on tb03.CO_COL equals tb425.TB03_COLABOR.CO_COL
                               where
                               tb03.CO_EMP == coEmp
                               && (CO_COL != 0 ? tb03.CO_COL == CO_COL : 0 == 0)
                               && (DT_INI <= tb425.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL)
                               && (DT_FIM >= tb425.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL)
                               select new DemonEnvioEmailCol
                               {
                                   rltFunc = tb03.TB128_FUNCA_FUNCI.NO_FUNCA_FUNCI,
                                   rltCod = tb03.NU_CPF_COL,
                                   rltNome = tb03.NO_COL.Trim(),                                
                                   rltData = tb425.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL,
                                   rltEmail = tb425.TB03_COLABOR.CO_EMAI_COL,
                                   rltAssun = tb425.TB418_CONTR_EMAIL.DE_ASSUN,
                                   rltSit = tb425.TB418_CONTR_EMAIL.CO_SITUA_EMAIL 
                               }).Distinct().ToList();

                    res = res.OrderBy(w => w.rltNome).ToList();

                    if (res.Count == 0)
                        return -1;

                    // Seta os dados no DataSource do Relatorio
                    bsReport.Clear();

                    foreach (DemonEnvioEmailCol at in res)
                    {
                        bsReport.Add(at);
                    }
                
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Demonstrativo de Envio de Email

        public class DemonEnvioEmailCol
        {
            //Nota dos tipos  de atividades
            public string rltCod { get; set; }
            public string rltNome { get; set; }
            public string rltFunc { get; set; }
            public DateTime rltData { get; set; }
            public string rltEmail { get; set; }
            public string rltAssun { get; set; }
            public string rltSit { get; set; }
            public string rltSitua
            {
                get 
                {
                    string situacao = "";
  
                    if (rltSit == "E")
                    {
                        situacao = "Enviado";
                    }
                    else if (rltSit == "A")
                    {
                        situacao = "Aberto";
                    }
                    else
                    {
                        situacao = "Cancelado";
                    }

                    return situacao;
                }
            }
        }

        #endregion
    }
}
