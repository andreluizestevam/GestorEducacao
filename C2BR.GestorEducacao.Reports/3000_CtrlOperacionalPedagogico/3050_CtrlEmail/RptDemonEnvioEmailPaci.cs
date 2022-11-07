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
    public partial class RptDemonEnvioEmailPaci : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonEnvioEmailPaci()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                                string infos,                                
                                int coEmp,
                                int CO_ALU,
                                string NomeFuncionalidadeCadastrada,
                                DateTime strP_DT_INI,
                                DateTime strP_DT_FIM
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

                lblTitulo.Text = NomeFuncionalidadeCadastrada;
                
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Conversão de variáveis
                DateTime DT_INI = strP_DT_INI.ToString() != "" ? strP_DT_INI : DateTime.MinValue;
                DateTime DT_FIM = strP_DT_FIM.ToString() != "" ? strP_DT_FIM : DateTime.MaxValue;

                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                               join tb424 in TB424_EMAIL_USUAR_GSAUD.RetornaTodosRegistros() on tb07.CO_ALU equals tb424.TB07_ALUNO.CO_ALU
                               where
                               (CO_ALU != 0 ? tb07.CO_ALU == CO_ALU : 0 == 0)
                               && (strP_DT_INI.CompareTo(tb424.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL) <= 0)
                               && (strP_DT_FIM.CompareTo(tb424.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL) >= 0)
                               select new DemonEnvioEmailPaci
                               {
                                rltCPF = tb07.NU_CPF_ALU,
                                rltCpfResp = tb108.NU_CPF_RESP ?? "-",
                                rltNome = tb07.NO_ALU.Trim(),
                                rltData = tb424.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL,
                                rltEmail = tb424.TB108_RESPONSAVEL.DES_EMAIL_RESP,
                                rltAssun = tb424.TB418_CONTR_EMAIL.DE_ASSUN,
                                rltSit = tb424.TB418_CONTR_EMAIL.CO_SITUA_EMAIL 
                               }).Distinct().ToList();

                    res = res.OrderBy(w => w.rltNome).ToList();

                    if (res.Count == 0)
                        return -1;

                    // Seta os dados no DataSource do Relatorio
                    bsReport.Clear();

                    foreach (DemonEnvioEmailPaci at in res)
                    {
                        bsReport.Add(at);
                    }
                
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Demonstrativo de Envio de Email

        public class DemonEnvioEmailPaci
        {
            //Nota dos tipos  de atividades
            public string rltCPF { get; set; }
            public string rltNome { get; set; }
            public string rltCpfResp { get; set; }
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

        private void xrTableRow1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
