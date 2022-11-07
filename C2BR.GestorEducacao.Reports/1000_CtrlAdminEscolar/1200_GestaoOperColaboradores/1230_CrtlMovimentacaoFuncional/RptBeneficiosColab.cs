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
    public partial class RptBeneficiosColab : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptBeneficiosColab()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmpRef,
                               int codCoCol,
                               int cdTpBenef,
                               string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmpRef);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                var lst = (from tb287 in ctx.TB287_COLABOR_BENEF
                           from tb14 in ctx.TB14_DEPTO
                           from tb15 in ctx.TB15_FUNCAO
                           where tb287.TB03_COLABOR.ORG_CODIGO_ORGAO == codInst 
                            && tb287.TB03_COLABOR.CO_DEPTO == tb14.CO_DEPTO
                            && tb287.TB03_COLABOR.CO_FUN == tb15.CO_FUN
                            && (codEmpRef != 0 ? tb287.TB03_COLABOR.CO_EMP == codEmpRef : codEmpRef == 0)
                            && (codCoCol != 0 ? tb287.TB03_COLABOR.CO_COL == codCoCol : codCoCol == 0)
                            && (cdTpBenef != 0 ? tb287.TB286_TIPO_BENECIF.ID_BENEFICIO == cdTpBenef : cdTpBenef == 0)
                            select new BeneficiosColab
                           {
                               Matricula = tb287.TB03_COLABOR.CO_MAT_COL,
                               Nome = tb287.TB03_COLABOR.NO_COL,
                               DataNascto = tb287.TB03_COLABOR.DT_NASC_COL,
                               Sexo = tb287.TB03_COLABOR.CO_SEXO_COL,
                               Telefone = tb287.TB03_COLABOR.NU_TELE_CELU_COL,
                               Categoria = tb287.TB03_COLABOR.FLA_PROFESSOR,
                               Deficiencia = tb287.TB03_COLABOR.TP_DEF,
                               CPF = tb287.TB03_COLABOR.NU_CPF_COL,
                               Departamento = tb14.NO_DEPTO,
                               NomeUnidade = tb287.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP,
                               Funcao = tb15.NO_FUN,
                               TipoBeneficio = tb287.TB286_TIPO_BENECIF.NO_BENEFICIO
                           }).OrderBy(p => p.TipoBeneficio).ThenBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (BeneficiosColab at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Benefícios/Colaborador Parametrizado do Relatorio

        public class BeneficiosColab
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
            public string TipoBeneficio { get; set; }

            public string CPFDesc
            {
                get
                {
                    return Funcoes.Format(this.CPF, TipoFormat.CPF);
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

            public string SexoDesc
            {
                get
                {
                    if (this.Sexo == "M")
                        return "Masculino";
                    else
                    {
                        return "Feminino";
                    }
                }
            }
        }
        #endregion

    }
}
