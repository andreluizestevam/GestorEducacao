using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3204_CtrlEncaminhamentos
{
    public partial class RptExtratoEmcamiamento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtratoEmcamiamento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              int especialidade,
                              int ClasseRisco,
                              string Sexo,
                              int Profsaude,
                              string DataIni,
                              string DataFim,
                              string no_relatorio

            )
        {
            try
            {
                #region Inicializa o header/Labels

                DateTime dataIni1;
                if (!DateTime.TryParse(DataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(DataFim, out dataFim1))
                {
                    return 0;
                }

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion
                DateTime DataInical = Convert.ToDateTime(DataIni);
                DateTime DataFinal = Convert.ToDateTime(DataFim);
                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(no_relatorio) ? no_relatorio : "EXTRATO DE REQUISICAO DE EXAMES *");

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                var res = (from tb195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb195.CO_COL equals tb03.CO_COL
                           join tb194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tb195.CO_ALU equals tb194.CO_ALU
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb194.CO_EMP equals tb25.CO_EMP
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb194.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                           where (tb194.DT_PRE_ATEND >= DataInical && tb194.DT_PRE_ATEND <= DataFinal
                           && (coUnid != 0 ? tb25.CO_EMP == coUnid : 0 == 0)
                           && (ClasseRisco != 0 ? tb194.CO_TIPO_RISCO == ClasseRisco : 0 == 0)
                           && (especialidade != 0 ? tb194.CO_ESPEC == especialidade : 0 == 0)
                           && (Sexo != "" ? tb194.CO_SEXO_USU == Sexo : 0 == 0)
                           && (Profsaude != 0 ? tb03.CO_COL == Profsaude : 0 == 0)
                           )
                           select new ExtratoEmcamiamentoMedico
                           {
                               NumeroProntuario = tb194.CO_PRE_ATEND,
                               DataHora = tb194.DT_PRE_ATEND,
                               NomePaciente = tb194.NO_USU,
                               DataNascimento = tb194.DT_NASC_USU,
                               Sexo = tb194.CO_SEXO_USU,
                               ClasseDeRisco = tb194.CO_TIPO_RISCO,
                               Especialidade = tb63.NO_ESPECIALIDADE,
                               NomeUnidade = tb25.NO_FANTAS_EMP,
                               ProfSaude = tb03.NO_COL

                           }).OrderBy(w => w.DataHora).OrderBy(y => y.DataHora).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();


                foreach (ExtratoEmcamiamentoMedico at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoEmcamiamentoMedico
        {
            public string NumeroProntuario { get; set; }
            public DateTime DataNascimento { get; set; }
            public DateTime DataHora { get; set; }
            public string NomePaciente { get; set; }
            public string NomePacienteValor
            {
                get
                {

                    return (this.NomePaciente.Length > 23 ? this.NomePaciente.Substring(0, 23) + "..." : this.NomePaciente);
                }

            }
            public string NomeUnidade { get; set; }
            public string Idade
            {
                get
                {
                    return Funcoes.FormataDataNascimento(this.DataNascimento);

                }

            }
            public string Sexo { get; set; }
            public int ClasseDeRisco { get; set; }
            public string ClasseDeRiscoValor
            {

                get
                {
                    switch (ClasseDeRisco)
                    {
                        case 1:
                            return "Emergência";
                            break;
                        case 2:
                            return "Muito Urgente";
                            break;
                        case 3:
                            return "Urgente";
                            break;
                        case 4:
                            return "Pouco Urgente";
                            break;
                        case 5:
                            return "Não Urgente";
                            break;

                        default:
                            return "0";
                            break;
                    }
                }
            }
            public string Especialidade { get; set; }
            public string ProfSaude { get; set; }
            public string ProfSaudeV
            {
                get { return (this.ProfSaude.Length > 23 ? this.ProfSaude.Substring(0, 22) + "..." : this.ProfSaude); }
            }


        }
    }
}
