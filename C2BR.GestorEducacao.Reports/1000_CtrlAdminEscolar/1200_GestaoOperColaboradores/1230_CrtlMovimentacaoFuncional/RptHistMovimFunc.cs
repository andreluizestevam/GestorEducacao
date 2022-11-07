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
    public partial class RptHistMovimFunc : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        int total, MI, ME, TE, R, S = 0;        

        public RptHistMovimFunc()
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

                var lst = (from tb286 in ctx.TB286_MOVIM_TRANSF_FUNCI
                           from tb03 in ctx.TB03_COLABOR
                           from tb25 in ctx.TB25_EMPRESA.DefaultIfEmpty()
                           from tb285 in ctx.TB285_INSTIT_TRANSF.DefaultIfEmpty()
                           from tb14 in ctx.TB14_DEPTO.DefaultIfEmpty()
                           where tb03.CO_COL == tb286.TB03_COLABOR.CO_COL && tb03.CO_EMP == tb286.TB25_EMPRESA1.CO_EMP &&
                            tb25.CO_EMP == tb286.TB25_EMPRESA1.CO_EMP //tb285.ID_INSTIT_TRANSF == tb286.TB285_INSTIT_TRANSF.ID_INSTIT_TRANSF &&
                            && tb14.CO_DEPTO == tb286.TB14_DEPTO.CO_DEPTO
                            && tb03.ORG_CODIGO_ORGAO == codInst 
                            && (codEmpRef != 0 ? tb286.TB25_EMPRESA1.CO_EMP == codEmpRef : codEmpRef == 0)
                            && (codCoCol != 0 ? tb286.TB03_COLABOR.CO_COL == codCoCol : codCoCol == 0)
                            && (codInst != 0 ? tb03.ORG_CODIGO_ORGAO == codInst : codInst == 0)
                            && tb286.DT_INI_MOVIM_TRANSF_FUNCI >= DtInicial
                            && tb286.DT_FIM_MOVIM_TRANSF_FUNCI <= DtFinal
                           select new HistMovimFunc
                           {
                               Matricula = tb03.CO_MAT_COL,
                               Nome = tb03.NO_COL,
                               DataNascto = tb03.DT_NASC_COL,
                               Sexo = tb03.CO_SEXO_COL,
                               Telefone = tb03.NU_TELE_CELU_COL,
                               DataAdmissao = tb03. DT_INIC_ATIV_COL,
                               Categoria = tb03.FLA_PROFESSOR,
                               Deficiencia = tb03.TP_DEF,
                               CPF = tb03.NU_CPF_COL,
                               Departamento = tb14.NO_DEPTO,
                               NomeUnidade = tb03.TB25_EMPRESA.NO_FANTAS_EMP,
                               CargaHoraria = tb03.NU_CARGA_HORARIA,
                               Salario = tb03.VL_SALAR_COL,
                               TipoMov = tb286.CO_TIPO_MOVIM,
                               Unidade = tb25.sigla,
                               DataInicioMov = tb286.DT_INI_MOVIM_TRANSF_FUNCI,
                               DataFinalMov = (DateTime)tb286.DT_FIM_MOVIM_TRANSF_FUNCI,
                               DataCadastro = tb286.DT_CADAST,
                               Motivo = tb286.CO_MOTIVO_AFAST,
                               TpRemuneracao = tb286.CO_TIPO_REMUN,
                               Destino = tb25.NO_FANTAS_EMP
                           }).OrderBy(p => p.DataCadastro).ThenBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (HistMovimFunc at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador Parametrizado do Relatorio

        public class HistMovimFunc
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
            public string Unidade { get; set; }
            public DateTime DataInicioMov { get; set; }
            public DateTime DataFinalMov { get; set; }
            public DateTime DataCadastro { get; set; }
            public string Motivo { get; set; }
            public string TpRemuneracao { get; set; }
            public string Destino { get; set; }

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

        private void lblTM_AfterPrint(object sender, EventArgs e)
        {
            if (lblTM.Text == "Movimentação Interna")
            {
                MI = MI + 1;
            }
            if (lblTM.Text == "Movimentação Externa")
            {
                ME = ME + 1;
            }
            if (lblTM.Text == "Transferência Externa")
            {
                TE = TE + 1;
            }
                
        }

        

        private void lblNome_AfterPrint(object sender, EventArgs e)
        {
            if (lblNome.Text != "")
            {
                total = total + 1;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbltotal.Text = String.Format("Quantidade de movimentações: (" + total.ToString() + " Geral) - (MI: " + MI.ToString() 
                + " / TE: "+ TE.ToString() + " / ME: " + ME.ToString() + ") - ( "+ R.ToString() + " Remunerado(s) - "+ S.ToString() + " Não Remunerado(s))");

            total = 0;
        }

        private void lblTpRemuneracao_AfterPrint(object sender, EventArgs e)
        {
            if (lblTpRemuneracao.Text == "I")
            {
                R = R + 1;
            }
            if (lblTpRemuneracao.Text == "P")
            {
                R = R + 1;
            }
            if (lblTpRemuneracao.Text == "S")
            {
                S = S + 1;
            }
        }

        


    }
}
