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

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores
{
    public partial class RptDemoDepartamento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public static double media, total = 0;

        public RptDemoDepartamento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int codDepCol,
                               string strFlaProfessor,
                               string codSexo,
                               string codDeficiencia,
                               string infos,
                               bool Escola,
           int codEmpLocacao,
            string NomeFuncionalidadeCadastrada
                )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "EMISSÃO DO DEMONSTRATIVO FINANCEIRO (SALÁRIO) DE COLABORADORES *";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
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

                var lst = (from tb03 in ctx.TB03_COLABOR
                           from tb15 in ctx.TB15_FUNCAO
                           from tb20 in ctx.TB20_TIPOCON
                           from tb18 in ctx.TB18_GRAUINS
                           from tb905 in ctx.TB905_BAIRRO
                           from tb14 in ctx.TB14_DEPTO
                           from tb21 in ctx.TB21_TIPOCAL
                           where tb03.CO_FUN == tb15.CO_FUN && tb03.CO_TPCON == tb20.CO_TPCON &&
                            tb03.CO_INST == tb18.CO_INST && tb03.CO_BAIRRO == tb905.CO_BAIRRO
                              && (codEmpLocacao != 0 ? tb03.CO_EMP_UNID_CONT == codEmpLocacao : codEmpLocacao == 0)
                            && (codDepCol != 0 ? tb03.CO_DEPTO == codDepCol : codDepCol == 0)
                            && tb03.CO_TPCAL == tb21.CO_TPCAL
                            && tb03.ORG_CODIGO_ORGAO == codInst
                            && tb03.CO_DEPTO == tb14.CO_DEPTO
                            && (strFlaProfessor != "T" ? tb03.FLA_PROFESSOR == strFlaProfessor : tb03.FLA_PROFESSOR == tb03.FLA_PROFESSOR)
                            && (codDeficiencia != "T" ? tb03.TP_DEF == codDeficiencia : tb03.TP_DEF == tb03.TP_DEF)
                            && (codSexo != "T" ? tb03.CO_SEXO_COL == codSexo : tb03.CO_SEXO_COL == tb03.CO_SEXO_COL)
                           select new DemoDepartamento
                           {
                               Matricula = tb03.CO_MAT_COL,
                               Nome = tb03.NO_COL,
                               DataNascto = tb03.DT_NASC_COL,
                               Sexo = tb03.CO_SEXO_COL,
                               TipoContrato = tb20.NO_TPCON,
                               GrauInstrucao = tb18.NO_INST,
                               UFCidadeBairro = tb905.NO_BAIRRO + "/" + tb905.TB904_CIDADE.NO_CIDADE + "/" + tb905.CO_UF,
                               Telefone = tb03.NU_TELE_CELU_COL,
                               DataAdmissao = tb03.DT_INIC_ATIV_COL,
                               Funcao = tb03.DE_FUNC_COL,
                               Categoria = tb03.FLA_PROFESSOR,
                               Deficiencia = tb03.TP_DEF,
                               CPF = tb03.NU_CPF_COL,
                               Departamento = tb14.NO_DEPTO,
                               SiglaDepto = tb14.CO_SIGLA_DEPTO,
                               NomeUnidade = tb03.TB25_EMPRESA.NO_FANTAS_EMP,
                               CargaHoraria = tb03.NU_CARGA_HORARIA,
                               Salario = tb03.VL_SALAR_COL,
                               escola = Escola,
                               TPV = tb21.NO_TPCAL
                           }).OrderBy(p => p.Departamento).ThenBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (DemoDepartamento at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador Parametrizado do Relatorio

        public class DemoDepartamento
        {
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public DateTime DataNascto { get; set; }
            public string TipoContrato { get; set; }
            public DateTime DataAdmissao { get; set; }
            public string Funcao { get; set; }
            public string SiglaDepto { get; set; }
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
            public string TPV { get; set; }
            public bool escola { get; set; }

            public double? SalarioBase
            {
                get
                {
                    if (this.TPV == "Mensal")
                    {
                        return this.Salario;
                    }
                    if (this.TPV == "Semanal")
                    {
                        return this.Salario * 4;
                    }
                    if (this.TPV == "Hora")
                    {
                        return this.Salario * this.CargaHoraria;
                    }

                    return null;
                }
            }

            public double? SalarioBaseDesc
            {
                get
                {
                    if (this.TPV == "Mensal")
                    {
                        return this.Salario;
                    }
                    if (this.TPV == "Semanal")
                    {
                        return this.Salario * 4;
                    }
                    if (this.TPV == "Hora")
                    {
                        return this.Salario * this.CargaHoraria;
                    }

                    return null;
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
                    if (escola)
                        return Funcoes.GetCategoriaColabor(this.Categoria);
                    else
                    {
                        string s = "";
                        switch (this.Categoria)
                        {
                            case "N":
                                s = "Funcionário";
                                break;
                            case "S":
                                s = "Profissional Saúde";
                                break;
                            case "P":
                                s = "Prestador Serviços";
                                break;
                            case "E":
                                s = "Estagiário(a)";
                                break;
                            case "O":
                                s = "Outro";
                                break;
                        }
                        return s;
                    }
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

        private void lblSalarioBase_AfterPrint(object sender, EventArgs e)
        {
            total = total + double.Parse(lblSalarioBase.Text != "" ? lblSalarioBase.Text : "0");

            if (lblNome.Text != "")
            {
                media = media + 1;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            media = total / media;

            lblValorTotal.Text = String.Format("{0:c2}", total);
            lblCustoMedio.Text = String.Format("{0:c2}", media);

            total = 0;
            media = 0;
        }
    }
}
