using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1230_CrtlMovimentacaoFuncional
{
    public partial class RptListaCargoFunciColab : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptListaCargoFunciColab()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int codEmpRef,
                               int codCoCol,
                               DateTime DtInicial,
                               DateTime DtFinal,
                               string infos)
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

                #region Query Colaborador Parametrizada

                var lst = (from tb59 in ctx.TB59_GESTOR_UNIDAD
                           from tb14 in ctx.TB14_DEPTO.DefaultIfEmpty()
                           where tb59.TB03_COLABOR.ORG_CODIGO_ORGAO == codInst
                            && (codEmpRef != 0 ? tb59.TB25_EMPRESA1.CO_EMP == codEmpRef : codEmpRef == 0)
                            && (codCoCol != 0 ? tb59.TB03_COLABOR.CO_COL == codCoCol : codCoCol == 0)
                            && (codInst != 0 ? tb59.TB03_COLABOR.ORG_CODIGO_ORGAO == codInst : codInst == 0)
                            && tb59.DT_INICIO_ATIVID >= DtInicial
                            && tb59.DT_INICIO_ATIVID <= DtFinal
                            && tb14.CO_DEPTO == tb59.TB14_DEPTO.CO_DEPTO
                           select new ListaCargoFunc
                           {
                               Matricula = tb59.TB03_COLABOR.CO_MAT_COL,
                               Nome = tb59.TB03_COLABOR.NO_COL,
                               DataNascto = tb59.TB03_COLABOR.DT_NASC_COL,
                               Sexo = tb59.TB03_COLABOR.CO_SEXO_COL,
                               Telefone = tb59.TB03_COLABOR.NU_TELE_CELU_COL,
                               Categoria = tb59.TB03_COLABOR.FLA_PROFESSOR,
                               Deficiencia = tb59.TB03_COLABOR.TP_DEF,
                               CPF = tb59.TB03_COLABOR.NU_CPF_COL,
                               Departamento = tb14.NO_DEPTO,
                               NomeUnidade = tb59.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP,
                               UnidadeDestino = tb59.TB25_EMPRESA.sigla,
                               UnidadeOrigem = tb59.TB25_EMPRESA1.sigla,
                               Funcao = tb59.TB15_FUNCAO.NO_FUN
                           }).OrderBy(p => p.UnidadeDestino).ThenBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ListaCargoFunc at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador Parametrizado do Relatorio

        public class ListaCargoFunc
        {
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public DateTime DataNascto { get; set; }
            public string TipoContrato { get; set; }
            public DateTime DataAdmissao { get; set; }
            public string Funcao { get; set; }
            public string GrauInstrucao { get; set; }
            public string UFCidadeBairro { get; set; }
            public string Telefone { get; set; }
            public string Sexo { get; set; }
            public string Deficiencia { get; set; }
            public string CPF { get; set; }
            public string Departamento { get; set; }
            public string NomeUnidade { get; set; }
            public int CargaHoraria { get; set; }
            public string Categoria { get; set; }
            public double? Salario { get; set; }
            public string TipoMov { get; set; }
            public DateTime DataInicioMov { get; set; }
            public DateTime DataFinalMov { get; set; }
            public DateTime DataCadastro { get; set; }
            public string Motivo { get; set; }
            public string TpRemuneracao { get; set; }
            public string UnidadeOrigem { get; set; }
            public string UnidadeDestino { get; set; }

            public string MotivoDesc
            {
                get
                {
                    if (this.Motivo == null)
                        return "-";
                    else
                    {
                        return Funcoes.GetTipoMotivoMov(this.Motivo);
                    }
                }
            }

            public string TipoMovDesc
            {
                get
                {
                    if (this.TipoMov == "MI")
                    {
                        return "Movimentação Interna";
                    }
                    if (this.TipoMov == "ME")
                    {
                        return "Movimentação Externa";
                    }
                    if (this.TipoMov == "TE")
                    {
                        return "Tranferência Externa";
                    }
                    return null;
                }
            }

            public string DataCadastroDesc
            {
                get
                {
                    if (this.DataCadastro == null)
                        return "-";
                    else
                    {
                        return DataCadastro.ToString("dd/MM/yy");
                    }
                }
            }

            public string DataIMovDesc
            {
                get
                {
                    if (this.DataInicioMov == null)
                        return "-";
                    else
                    {
                        return DataInicioMov.ToString("dd/MM/yy");
                    }
                }
            }

            public string DataFMovDesc
            {
                get
                {
                    if (this.DataFinalMov == null)
                        return "-";
                    else
                    {
                        return DataFinalMov.ToString("dd/MM/yy");
                    }
                }
            }

            public string CPFDesc
            {
                get
                {
                    return Funcoes.Format(this.CPF, TipoFormat.CPF);
                }
            }

            public string CategoriaDesc
            {
                get
                {
                    return Funcoes.GetCategoriaColabor(this.Categoria);
                }
            }

            public string MatriculDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.Matricula))
                        return "-";

                    return this.Matricula.Insert(5, "-").Insert(2, ".");
                }
            }

            public string DeficienciaDesc
            {
                get
                {
                    return Funcoes.GetDeficienciaColabor(this.Deficiencia);
                }
            }

            public string TelefoneDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.Telefone))
                        return "-";

                    string pattern = @"(\d{2})(\d{4})(\d{4})";
                    return Regex.Replace(this.Telefone, pattern, "($1) $2-$3");
                }
            }

            public string DataNasctoDesc
            {
                get
                {
                    if (this.DataNascto == null)
                        return "-";
                    else
                    {
                        return DataNascto.ToString("dd/MM/yy") + " (" + Funcoes.GetIdade(this.DataNascto) + ")";
                    }
                }
            }

            public string DataAdmissaoDesc
            {
                get
                {
                    if (this.DataNascto == null)
                        return "-";
                    else
                    {
                        return DataNascto.ToString("dd/MM/yy");
                    }
                }
            }
        }
        #endregion

    }
}
