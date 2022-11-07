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
    public partial class RptColaborParametrizada : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptColaborParametrizada()
        {
            InitializeComponent();
        }
        #endregion

        #region Init Report

        public int InitReport(string parametros,
                               int coEmpLog,
                               int codEmp,
                               string strFlaProfessor,
                               int codFuncao,
                               int codInstrucao,
                               string codDeficiencia,
                               string codSexo,
                               string codUF,
                               int codCidade,
                               int codBairro,
                               string infos,
                               int codEmpLocacao,
                               string NomeFuncionalidadeCadastrada,
                               string ClassifiProfi
            )
        {
            try
            {
                #region Setar o Header e as Labels
                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "EMISSÃO DA RELAÇÃO DE COLABORADORES PARAMETRIZADA * ";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmpLog);
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
                          where tb03.CO_FUN == tb15.CO_FUN && tb03.CO_TPCON == tb20.CO_TPCON &&
                           tb03.CO_INST == tb18.CO_INST && tb03.CO_BAIRRO == tb905.CO_BAIRRO
                           && (codEmp != 0 ? tb03.TB25_EMPRESA.CO_EMP == codEmp : 0 == 0)
                           && (codEmpLocacao != 0 ? tb03.CO_EMP_UNID_CONT == codEmpLocacao : codEmpLocacao == 0)
                           && (strFlaProfessor != "T" ? tb03.FLA_PROFESSOR == strFlaProfessor : strFlaProfessor == "T")
                           && (codFuncao != 0 ? tb03.CO_FUN == codFuncao : codFuncao == 0)
                           && (codInstrucao != 0 ? tb03.CO_INST == codInstrucao : codInstrucao == 0)
                           && (codDeficiencia != "T" ? tb03.TP_DEF == codDeficiencia : codDeficiencia == "T")
                           && (codSexo != "T" ? tb03.CO_SEXO_COL == codSexo : codSexo == "T")
                           && (codUF != "T" ? tb03.CO_ESTA_ENDE_COL == codUF : codUF == "T")
                           && (codCidade != 0 ? tb905.TB904_CIDADE.CO_CIDADE == codCidade : codCidade == 0)
                           && (codBairro != 0 ? tb905.CO_BAIRRO == codBairro : codBairro == 0)
                           && (ClassifiProfi != "0" ? tb03.CO_CLASS_PROFI == ClassifiProfi : 0 == 0)
                          select new ColaborParam
                          {
                              Matricula = tb03.CO_MAT_COL,
                              Nome_Receb = tb03.NO_COL,
                              cpf = tb03.NU_CPF_COL,
                              DataNascto = tb03.DT_NASC_COL,
                              Sexo = tb03.CO_SEXO_COL,
                              TipoContrato = tb20.NO_TPCON,
                              GrauInstrucao = tb18.NO_INST,
                              UFCidadeBairro = tb905.NO_BAIRRO + "/" + tb905.TB904_CIDADE.NO_CIDADE + "/" + tb905.CO_UF,
                              Telefone = tb03.NU_TELE_CELU_COL,
                              DataAdmissao = tb03.DT_INIC_ATIV_COL,
                              Funcao = tb03.DE_FUNC_COL,
                          }).OrderBy(p => p.Nome_Receb);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ColaborParam at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador Parametrizado do Relatorio

        public class ColaborParam
        {
            public string Matricula { get; set; }
            public string Nome
            {
                get
                {
                    return this.cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-") + " - " + this.Nome_Receb;
                }
            }
            public string Nome_Receb { get; set; }
            public DateTime DataNascto { get; set; }
            public string TipoContrato { get; set; }
            public DateTime DataAdmissao { get; set; }
            public string Funcao { get; set; }
            public string GrauInstrucao { get; set; }
            public string UFCidadeBairro { get; set; }
            public string Telefone { get; set; }
            public string Sexo { get; set; }
            public string cpf { get; set; }
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
        }
        #endregion
    }
}
