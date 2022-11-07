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
    public partial class RptDemonEnvioEmail : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemonEnvioEmail()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                                string infos,
                                int strP_CO_EMP_REF,
                                int strP_CO_CUR,
                                int strP_CO_MODU_CUR,
                                int strP_CO_TUR,
                                int coEmp,
                                int CO_ALU,
                                string NomeFuncionalidadeCadastrada,
                                int CO_RESP,
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

                lblTitulo.Text = "DEMONSTRATIVO DE ENVIO DE EMAIL";
                
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Conversão de variáveis
                DateTime DT_INI = strP_DT_INI.ToString() != "" ? strP_DT_INI : DateTime.MinValue;
                DateTime DT_FIM = strP_DT_FIM.ToString() != "" ? strP_DT_FIM : DateTime.MaxValue;

                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                               join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                               join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb43.CO_CUR
                               join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                               join tb419 in TB419_EMAIL_USUAR_GEDUC.RetornaTodosRegistros() on tb07.CO_ALU equals tb419.TB07_ALUNO.CO_ALU
                               where
                               tb43.CO_EMP == tb08.CO_EMP
                               && tb43.CO_CUR == tb08.CO_CUR
                               && (strP_CO_EMP_REF != 0 ? tb08.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                               && (strP_CO_MODU_CUR != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                               && (strP_CO_CUR != 0 ? tb08.CO_CUR == strP_CO_CUR : 0 == 0)
                               && (strP_CO_TUR != 0 ? tb08.CO_TUR == strP_CO_TUR : 0 == 0)
                               && (CO_ALU != 0 ? tb07.CO_ALU == CO_ALU : 0 == 0)
                               && (CO_RESP != 0 ? tb07.TB108_RESPONSAVEL.CO_RESP == CO_RESP : 0 == 0)
                               && (strP_DT_INI.CompareTo(tb419.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL) <= 0)
                               && (strP_DT_FIM.CompareTo(tb419.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL) >= 0)
                               select new DemonEnvioEmail
                               {
                                descRltNire = tb07.NU_NIRE,
                                rltNome = tb07.NO_ALU.Trim(),
                                rltModal = tb08.TB44_MODULO.CO_SIGLA_MODU_CUR,
                                rltCurso = tb01.CO_SIGL_CUR,
                                rltTurma = tb129.CO_SIGLA_TURMA,
                                rltData = tb419.TB418_CONTR_EMAIL.DT_ENVIO_EMAIL,
                                rltEmail = tb419.TB108_RESPONSAVEL.DES_EMAIL_RESP,
                                rltAssun = tb419.TB418_CONTR_EMAIL.DE_ASSUN,
                                rltSit = tb419.TB418_CONTR_EMAIL.CO_SITUA_EMAIL 
                               }).Distinct().ToList();

                    res = res.OrderBy(w => w.rltNome).ToList();

                    if (res.Count == 0)
                        return -1;

                    // Seta os dados no DataSource do Relatorio
                    bsReport.Clear();

                    foreach (DemonEnvioEmail at in res)
                    {
                        bsReport.Add(at);
                    }
                
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Demonstrativo de Envio de Email

        public class DemonEnvioEmail
        {
            //Nota dos tipos  de atividades
            public int descRltNire { get; set; }
            public string rltNire { get { return this.descRltNire.ToString().PadLeft(7, '0'); } }
            public string rltNome { get; set; }
            public string rltModal { get; set; }
            public string rltCurso { get; set; }
            public string rltTurma { get; set; }
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
