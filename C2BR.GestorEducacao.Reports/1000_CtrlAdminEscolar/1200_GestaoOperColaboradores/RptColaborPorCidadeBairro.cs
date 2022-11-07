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
    public partial class RptColaborPorCidadeBairro : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public static int totMasc, totFemin, totDefic = 0;

        public RptColaborPorCidadeBairro()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int coEmpCol,
                               string codSituacao,
                               int codEmpLocacao,
                               string codUF,
                               int codCidade,
                               int codBairro,
                               string infos,
                               string NomeFuncionalidadeCadastrada
            )
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
                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "EMISSÃO DA RELAÇÃO DE COLABORADORES POR CIDADE E BAIRRO*";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador/CidadeBairro

                var lst = (from tb03 in ctx.TB03_COLABOR
                           from tb905 in ctx.TB905_BAIRRO
                           where tb03.CO_BAIRRO == tb905.CO_BAIRRO
                           && (codEmpLocacao != 0 ? tb03.CO_EMP_UNID_CONT == codEmpLocacao : codEmpLocacao == 0)
                            && (coEmpCol != 0 ? tb03.TB25_EMPRESA.CO_EMP == coEmpCol : coEmpCol == 0)
                            && tb03.ORG_CODIGO_ORGAO == codInst
                            && (codBairro != 0 ? tb03.CO_BAIRRO == codBairro : codBairro == 0)
                            && (codCidade != 0 ? tb905.TB904_CIDADE.CO_CIDADE == codCidade : codCidade == 0)
                            && (codSituacao != "T" ? tb03.CO_SITU_COL == codSituacao : codSituacao == "T")
                            && (codUF != "T" ? tb905.CO_UF == codUF : codUF == "T")
                           select new ColaborPorCidadeBairro
                           {
                               Matricula = tb03.CO_MAT_COL,
                               Nome = tb03.NO_COL,
                               DataNascto = tb03.DT_NASC_COL,
                               Sexo = tb03.CO_SEXO_COL,
                               SiglaUnid = tb03.TB25_EMPRESA.sigla,
                               Deficiencia = tb03.TP_DEF,
                               Situacao = tb03.CO_SITU_COL,
                               Logradouro = tb03.DE_ENDE_COL,
                               Numero = tb03.NU_ENDE_COL,
                               Complemento = tb03.DE_COMP_ENDE_COL,
                               Telefone = tb03.NU_TELE_CELU_COL,
                               Bairro = tb905.NO_BAIRRO,
                               Cidade = tb905.TB904_CIDADE.NO_CIDADE,
                               UF = tb905.CO_UF
                           }).OrderBy(p => p.UF).ThenBy(p => p.Cidade).ThenBy(p => p.Bairro).ThenBy(p => p.Nome);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ColaborPorCidadeBairro at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador/CidadeBairro do Relatorio

        public class ColaborPorCidadeBairro
        {
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public string Sexo { get; set; }
            public DateTime DataNascto { get; set; }
            public string Deficiencia { get; set; }
            public string Situacao { get; set; }
            public string Logradouro { get; set; }
            public decimal? Numero { get; set; }
            public string Complemento { get; set; }
            public string SiglaUnid { get; set; }
            public string Telefone { get; set; }


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

            public string SituacaoDesc
            {
                get
                {
                    return Funcoes.GetSituacaoColabor(this.Situacao);
                }
            }

            public string CidadeUFDesc
            {
                get
                {
                    return "Cidade/UF: " + Cidade + "/" + UF;
                }
            }

            public string EnderecoDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.Logradouro))
                        return "-";
                    else
                    {
                        string endereco = "";

                        endereco = Logradouro;

                        if (this.Numero != null)
                            endereco = endereco + ", " + Numero.ToString();

                        if (this.Complemento != null)
                            endereco = endereco + " - " + Complemento;

                        return endereco;
                    }
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

            public int Idade
            {
                get { return Funcoes.GetIdade(this.DataNascto); }
            }
        }
        #endregion

        private void GroupFooter1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTotalFuncionario.Text = String.Format("Total de funcionários: {0} (Masculino: {1} - Feminino: {2} - Deficiente: {3})",
            (totMasc + totFemin).ToString("D4"), totMasc.ToString("D4"), totFemin.ToString("D4"), totDefic.ToString("D4"));

            totMasc = totFemin = totDefic = 0;
        }

        private void lblSexo_AfterPrint(object sender, EventArgs e)
        {
            if (lblSexo.Text == "M")
                totMasc = totMasc + 1;
            else
            {
                totFemin = totFemin + 1;
            }
        }

        private void lblDeficiencia_AfterPrint(object sender, EventArgs e)
        {
            if (lblDeficiencia.Text != "Nenhuma")
            {
                totDefic = totDefic + 1;
            }
        }

    }
}
