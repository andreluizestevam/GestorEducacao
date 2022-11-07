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


namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8300_CtrlPreAtendimentos
{
    public partial class RptExtratoPreAtendimentos : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtratoPreAtendimentos()
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
                var res = (from tb194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb194.CO_EMP equals tb25.CO_EMP
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb194.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                           where (tb194.DT_PRE_ATEND >= DataInical && tb194.DT_PRE_ATEND <= DataFinal
                           && (coUnid != 0 ?  tb25.CO_EMP == coUnid : 0 == 0)
                           && (ClasseRisco != 0 ? tb194.CO_TIPO_RISCO == ClasseRisco : 0==0)
                           && (especialidade != 0 ?  tb194.CO_ESPEC == especialidade : 0==0)
                            && (Sexo != "" ? tb194.CO_SEXO_USU == Sexo : 0 == 0)
                           )
                           select new RequisicaoExtratoPreAtendimento
                           {
                               NumeroProntuario = tb194.CO_PRE_ATEND,
                               DataHora = tb194.DT_PRE_ATEND,
                               NomePaciente = tb194.NO_USU,
                               DataNascimento = tb194.DT_NASC_USU,
                               Sexo = tb194.CO_SEXO_USU,
                               Responsavel = tb194.NO_RESP,
                               ClasseDeRisco = tb194.CO_TIPO_RISCO,
                               Especialidade = tb63.NO_ESPECIALIDADE,
                               NomeUnidade = tb25.NO_FANTAS_EMP

                           }).OrderBy(w => w.DataHora).OrderBy(y => y.DataHora).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();


                foreach (RequisicaoExtratoPreAtendimento at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class RequisicaoExtratoPreAtendimento
        {
            public string NumeroProntuario { get; set; }
            public DateTime DataNascimento { get; set; }
            public DateTime DataHora { get; set; }
            public string NomePaciente { get; set; }
            public string NomePacienteValor
            {
                get
                {

                    return (this.NomePaciente.Length > 30 ? this.NomePaciente.Substring(0, 30) + "..." : this.NomePaciente);
                }

            }
            public string NomeUnidade { get; set; }
            public string Idade
            {
                get
                {
                    return (this.DataNascimento != null ? Funcoes.FormataDataNascimento(this.DataNascimento) : " - ");

                }

            }
            public string Sexo { get; set; }
            public string Responsavel { get; set; }
            public string ResponsavelValor 
            {

                get 
                {
                    return (this.Responsavel.Length > 30 ? this.Responsavel.Substring(0, 30) + "..." : this.Responsavel);
                }
            }
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

        }
    }
}
