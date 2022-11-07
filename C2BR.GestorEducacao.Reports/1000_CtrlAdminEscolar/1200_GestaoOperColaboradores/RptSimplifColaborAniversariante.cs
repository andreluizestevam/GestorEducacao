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
    public partial class RptSimplifColaborAniversariante : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptSimplifColaborAniversariante()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int coEmpCol,
                               string flaProf,
                               int mes,
                               string infos,
            string NomeFuncionalidadeCadastrada,
             int codEmpLocacao
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
                    lblTitulo.Text = "EMISSÃO DA RELAÇÃO DE COLABORADORES ANIVERSARIANTES (SIMPLIFICADA)* ";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador/Aniversariante

                var lst = (from tb03 in ctx.TB03_COLABOR
                           from tb15 in ctx.TB15_FUNCAO
                           where tb03.CO_FUN == tb15.CO_FUN
                            && (coEmpCol != 0 ? tb03.TB25_EMPRESA.CO_EMP == coEmpCol : coEmpCol == 0)
                              && (codEmpLocacao != 0 ? tb03.CO_EMP_UNID_CONT == codEmpLocacao : codEmpLocacao == 0)
                            && tb03.ORG_CODIGO_ORGAO == codInst
                            && (mes != 0 ? tb03.DT_NASC_COL.Month == mes : mes == 0)
                            && (flaProf != "T" ? tb03.FLA_PROFESSOR == flaProf : flaProf == "T")
                           select new ColaborAniversariante
                           {
                               Funcao = tb15.NO_FUN,
                               Matricula = tb03.CO_MAT_COL,
                               Nome = tb03.NO_COL,
                               DataNascto = tb03.DT_NASC_COL,
                               Sexo = tb03.CO_SEXO_COL,
                               NomeUnidade = tb03.TB25_EMPRESA.NO_FANTAS_EMP,
                               Deficiencia = tb03.TP_DEF,
                               Situacao = tb03.CO_SITU_COL,
                               TelefoneMovel = tb03.NU_TELE_CELU_COL,
                               TelefoneFixo = tb03.NU_TELE_RESI_COL,
                               Email = tb03.CO_EMAIL_FUNC_COL
                           }).OrderBy(p => p.DataNascto.Month)
                           .ThenBy(p => p.DataNascto.Day);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ColaborAniversariante at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador/Aniversariante do Relatorio

        public class ColaborAniversariante
        {
            public string Funcao { get; set; }
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public string Sexo { get; set; }
            public DateTime DataNascto { get; set; }
            public string Deficiencia { get; set; }
            public string Situacao { get; set; }
            public string NomeUnidade { get; set; }
            public string TelefoneMovel { get; set; }
            public string TelefoneFixo { get; set; }
            public string Email { get; set; }


            public string MatriculDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.Matricula))
                        return "-";

                    return this.Matricula.Insert(5, "-").Insert(2, ".");
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
                        return DataNascto.ToString("dd/MM/yyyy");
                    }
                }
            }

            public string TelefoneMovelDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.TelefoneMovel))
                        return " * ";

                    string pattern = @"(\d{2})(\d{4})(\d{4})";
                    return Regex.Replace(this.TelefoneMovel, pattern, "($1) $2-$3");
                }
            }

            public string TelefoneFixoDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.TelefoneFixo))
                        return " * ";

                    string pattern = @"(\d{2})(\d{4})(\d{4})";
                    return Regex.Replace(this.TelefoneFixo, pattern, "($1) $2-$3");
                }
            }

            public int Idade
            {
                get { return Funcoes.GetIdade(this.DataNascto); }
            }
        }
        #endregion

        private void bsReport_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
