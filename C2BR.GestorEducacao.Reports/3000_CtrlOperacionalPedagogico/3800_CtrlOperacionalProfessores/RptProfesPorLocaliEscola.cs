using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3800_CtrlOperacionalProfessores
{
    public partial class RptProfesPorLocaliEscola : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptProfesPorLocaliEscola()
        {
            InitializeComponent();
        }

        #region Init Report

        /// <summary>
        /// Esta função carrega o relatório de Atividades Realizadas do Professor
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="codEmp"></param>
        /// <param name="infos"></param>
        /// <param name="strP_CO_EMP"></param>
        /// <param name="strP_CO_COL"></param>
        /// <param name="strP_CO_ANO_MES_MAT"></param>
        /// <param name="strP_CO_MODU_CUR"></param>
        /// <param name="strP_CO_CUR"></param>
        /// <param name="strP_CO_TUR"></param>
        /// <returns></returns>
        public int InitReport(string parametros,
                                int codEmp,
                                string infos,
                                string strP_CO_EMP)
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

                // Conversão das variáveis necessárias
                int intP_CO_EMP = int.Parse(strP_CO_EMP);
                #region Query

                var lst = (from c in ctx.TB03_COLABOR
                           join gr in ctx.TB18_GRAUINS
                                on c.CO_INST equals gr.CO_INST into t1
                           from gr in t1.DefaultIfEmpty()
                           join tc in ctx.TB20_TIPOCON
                                on c.CO_TPCON equals tc.CO_TPCON into t2
                           from tc in t2.DefaultIfEmpty()
                           join e in ctx.TB25_EMPRESA
                                on c.CO_EMP equals e.CO_EMP into t3
                           from e in t3.DefaultIfEmpty()
                           join ci in ctx.TB904_CIDADE
                                on c.CO_CIDADE equals ci.CO_CIDADE into t4
                           from ci in t4.DefaultIfEmpty()
                           join b in ctx.TB905_BAIRRO
                                on c.CO_BAIRRO equals b.CO_BAIRRO into t5
                           from b in t5.DefaultIfEmpty()
                           join esp in ctx.TB100_ESPECIALIZACAO
                                on c.CO_ESPEC equals esp.CO_ESPEC into t6
                           from esp in t6.DefaultIfEmpty()

                           where (intP_CO_EMP != 0 ? c.CO_EMP == intP_CO_EMP : 0 == 0)

                           select new ProfesPorLocaliEscola
                           {
                               matricula = c.CO_MAT_COL,
                               nome = c.NO_COL,
                               coEspecializacao = esp.TP_ESPEC,
                               curFormacao = esp.DE_ESPEC == "" ? " - " : esp.DE_ESPEC,
                               tpContrato = tc.NO_TPCON,
                               coSituacao = c.CO_SITU_COL,
                               ch = c.NU_CARGA_HORARIA,
                               cidade = ci.NO_CIDADE,
                               bairro = b.NO_BAIRRO,
                               ue = e.sigla
                           }

                           ).Distinct();

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ProfesPorLocaliEscola at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ProfesPorLocaliEscola
        {
            public string matricula { get; set; }
            public string nome { get; set; }
            public string coEspecializacao { get; set; }
            public string grauInstrucao
            {
                get
                {
                    string esp = "";
                    switch (this.coEspecializacao)
                    {
                        case "MB":
                            esp = "MBA";
                            break;
                        case "PG":
                            esp = "Pós-Graduação";
                            break;
                        case "PD":
                            esp = "Pós-Doutorado";
                            break;
                        case "SU":
                            esp = "Superior";
                            break;
                        case "ES":
                            esp = "Especialização";
                            break;
                        case "ME":
                            esp = "Mestrado";
                            break;
                        case "DO":
                            esp = "Doutorado";
                            break;
                        case "TE":
                            esp = "Técnico";
                            break;
                        case "OU":
                            esp = "Outro";
                            break;
                        default:
                            esp = " - ";
                            break;
                    }
                    return esp;
                }
            }
            public string curFormacao { get; set; }
            public string tpContrato { get; set; }
            public string coSituacao { get; set; }
            public string situacao
            {
                get
                {
                    string sit = "";
                    switch (this.coSituacao) {
                        case "ATI":
                            sit = "Atividade Interna";
                            break;
                        case "ATE":
                            sit = "Atividade Externa";
                            break;
                        case "FCE":
                            sit = "Cedido";
                            break;
                        case "FES":
                            sit = "Estagiário";
                            break;
                        case "LFR":
                            sit = "Licença Funcional";
                            break;
                        case "LME":
                            sit = "Licença Médica";
                            break;
                        case "LMA":
                            sit = "Licença Maternidade";
                            break;
                        case "SUS":
                            sit = "Suspenso";
                            break;
                        case "TRE":
                            sit = "Treinamento";
                            break;
                        case "FER":
                            sit = "Férias";
                            break;
                        default:
                            sit = " - ";
                            break;
                    }
                    return sit;
                }
            }
            public int ch { get; set; }
            public string telefone { get; set; }
            public string cidade { get; set; }
            public string bairro { get; set; }
            public string cidadeBairro
            {
                get
                {
                    return this.cidade + " - " + this.bairro;
                }
            }
            public string ue { get; set; }
        }
    }
}
