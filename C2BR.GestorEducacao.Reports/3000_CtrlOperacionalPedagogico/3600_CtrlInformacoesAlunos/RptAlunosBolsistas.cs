using System;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using DevExpress.XtraReports.UI;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptAlunosBolsistas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        private int qtdeOcor, qtdeTotOcor = 0;
        private decimal totValorDesctoBolsa, totValorDesctoEspec, totValorLiqui, totGValorDesctoBolsa, totGValorDesctoEspec, totGValorLiqui = 0;
 
        #region ctor
        public RptAlunosBolsistas()
        {
            InitializeComponent();
        }
        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              int codUndContrato,
                              string coAnoMesmat,
                              int coModuCur,
                              int coCur,
                              int coTur,
                              string tpGrupoBolsa,
                              int coTipoBolsa,
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

                #region Query Report

                var lst = (from tb08 in ctx.TB08_MATRCUR
                           join tb07 in ctx.TB07_ALUNO on tb08.CO_ALU equals tb07.CO_ALU
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb08.CO_TUR equals tb129.CO_TUR
                           where tb08.CO_EMP == codEmp
                           && (coAnoMesmat != "T" ? tb08.CO_ANO_MES_MAT == coAnoMesmat : coAnoMesmat == "T")
                           && (coModuCur != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == coModuCur : coModuCur == 0)
                           && (coCur != 0 ? tb08.CO_CUR == coCur : coCur == 0)
                           && (coTur != 0 ? tb08.CO_TUR == coTur : coTur == 0)
                           && (codUndContrato != 0 ? tb08.CO_EMP_UNID_CONT == codUndContrato : codUndContrato == 0)
                           //&& tb07.TB148_TIPO_BOLSA != null
                           && (coTipoBolsa != 0 ? tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA == coTipoBolsa : coTipoBolsa == 0)
                           && (tpGrupoBolsa != "T" ? tb07.TB148_TIPO_BOLSA.TP_GRUPO_BOLSA == tpGrupoBolsa : tpGrupoBolsa == "T")
                           select new AlunosBolsistas
                           {
                               NIRE = tb07.NU_NIRE,
                               Aluno = tb07.NO_ALU,
                               CPFResponsavel = tb08.TB108_RESPONSAVEL.NU_CPF_RESP,
                               DesGrupoBolsa = tb08.TB148_TIPO_BOLSA != null ? tb07.TB148_TIPO_BOLSA.TP_GRUPO_BOLSA == "C" ? "CON" : "BOL" : "XXX",
                               NomeBolsa = tb08.TB148_TIPO_BOLSA != null ? tb07.TB148_TIPO_BOLSA.NO_TIPO_BOLSA : "XXXXX",

                               NoDesconto = tb08.NO_DESCONTO,
                               VlMensalidade = tb08.VL_PAR_MOD_MAT,
                               VlContrato = tb08.VL_TOT_MODU_MAT,
                               VlDesctoBolsa = tb08.VL_DES_BOL_MOD_MAT,
                               VlDesctoEspecial = tb08.VL_DES_MOD_MAT,
                               VlLiquido = tb08.VL_TOT_MODU_MAT != null ? (tb08.VL_TOT_MODU_MAT ?? 0) - (tb08.VL_DES_BOL_MOD_MAT ?? 0) - (tb08.VL_DES_MOD_MAT ?? 0) : 0,
                               Modalidade = tb08.TB44_MODULO.DE_MODU_CUR,
                               Serie = tb01.NO_CUR,
                               Turma = tb129.NO_TURMA,
                               Ano = tb08.CO_ANO_MES_MAT,
                               CoTur = tb08.CO_TUR
                           }).OrderBy(p => p.Modalidade).OrderBy(p => p.Serie).OrderBy(p => p.Turma).OrderBy(p => p.Aluno).ToList();

                var res = lst;

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper Report

        public class AlunosBolsistas
        {
            public string Ano { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public int? CoTur { get; set; }
            public int NIRE { get; set; }
            public string Aluno { get; set; }
            public string CPFResponsavel { get; set; }
            public string DesGrupoBolsa { get; set; }
            public string NomeBolsa { get; set; }
            public string NoDesconto { get; set; }

            public string Desconto
            {
                get
                {
                    string Des = "";
                    if (NoDesconto == null)
                    {
                        Des = DesGrupoBolsa + " " + NomeBolsa;
                    }
                    else
                    {
                        Des = NoDesconto;
                    }
                    return Des;
                }
            }

            public decimal? VlMensalidade { get; set; }
            public decimal? VlContrato { get; set; }
            public decimal? VlLiquido { get; set; }
            public decimal? VlDesctoBolsa { get; set; }
            public decimal? VlDesctoEspecial { get; set; }

            public string DescNIRE
            {
                get
                {
                    return this.NIRE.ToString().PadLeft(9, '0');
                }
            }

            public string DescSerie
            {
                get
                {
                    return "Ano: " + this.Ano.Trim() + " - Modalidade: " + this.Modalidade.ToUpper() + " - Série: " + this.Serie.ToUpper() + " - Turma: " + this.Turma.ToUpper();
                }
            }

            public string DescAluno
            {
                get
                {
                    return this.Aluno.ToUpper();
                }
            }

            public string DescCPFResp
            {
                get
                {
                    return Funcoes.Format(this.CPFResponsavel,TipoFormat.CPF);
                }
            }

            public decimal PercDescBolsa
            {
                get
                {
                    if ((!VlContrato.HasValue || VlContrato == 0) && (!VlDesctoBolsa.HasValue))
                        return 0;

                    return ((VlDesctoBolsa ?? 0) * 100) / VlContrato.Value;
                }
            }

            public decimal PercDescEspec
            {
                get
                {
                    if ((!VlContrato.HasValue || VlContrato == 0) && (!VlDesctoEspecial.HasValue))
                        return 0;

                    return ((VlDesctoEspecial ?? 0) * 100) / VlContrato.Value;
                }
            }

            public decimal PercVlLiquido
            {
                get
                {
                    if ((!VlContrato.HasValue || VlContrato == 0) && (!VlLiquido.HasValue))
                        return 0;

                    return ((VlLiquido ?? 0) * 100) / (VlContrato == null ? 1 : VlContrato.Value);
                }
            }
        }

        #endregion

        private void DetailContent_AfterPrint(object sender, EventArgs e)
        {
            if (lblDesctoBolsa.Text != "")
            {
                totValorDesctoBolsa = totValorDesctoBolsa + decimal.Parse(lblDesctoBolsa.Text);
            }
            if (lblDesctoEspec.Text != "")
            {
                totValorDesctoEspec = totValorDesctoEspec + decimal.Parse(lblDesctoEspec.Text);
            }
            if (lblValorLiquido.Text != "")
            {
                totValorLiqui = totValorLiqui + decimal.Parse(lblValorLiquido.Text);
            }

            qtdeOcor++;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (totValorDesctoBolsa > 0)
            {
                lblPercDesctoBolsa.Text = String.Format("{0:0.0}", ((totValorDesctoBolsa/qtdeOcor) * 100) / totValorDesctoBolsa);
            }
            else
                lblPercDesctoBolsa.Text = String.Format("{0:0.0}", 0);

            if (totValorDesctoEspec > 0)
            {
                lblPercDesctoEspec.Text = String.Format("{0:0.0}", ((totValorDesctoEspec/qtdeOcor) * 100) / totValorDesctoEspec);
            }
            else
                lblPercDesctoEspec.Text = String.Format("{0:0.0}", 0);

            if (totValorLiqui > 0)
            {
                lblPercValorLiqui.Text = String.Format("{0:0.0}", ((totValorLiqui/qtdeOcor) * 100) / totValorLiqui);
            }            
            else
                lblPercValorLiqui.Text = String.Format("{0:0.0}", 0);

            totGValorDesctoEspec = totGValorDesctoEspec + totValorDesctoEspec;
            totGValorDesctoBolsa = totGValorDesctoBolsa + totValorDesctoBolsa;
            totGValorLiqui = totGValorLiqui + totValorLiqui;
            qtdeTotOcor = qtdeTotOcor + qtdeOcor;

            totValorDesctoEspec = totValorDesctoBolsa = totValorLiqui = qtdeOcor = 0;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (totGValorDesctoBolsa > 0)
            {
                lblGPercDesctoBolsa.Text = String.Format("{0:0.0}", ((totGValorDesctoBolsa/qtdeTotOcor) * 100) / totGValorDesctoBolsa);
            }
            else
                lblGPercDesctoBolsa.Text = String.Format("{0:0.0}", 0);

            if (totGValorDesctoEspec > 0)
            {
                lblGPercDesctoEspec.Text = String.Format("{0:0.0}", ((totGValorDesctoEspec/qtdeTotOcor) * 100) / totGValorDesctoEspec);
            }
            else
                lblGPercDesctoEspec.Text = String.Format("{0:0.0}", 0);

            if (totGValorLiqui > 0)
            {
                lblGPercValorLiqui.Text = String.Format("{0:0.0}", ((totGValorLiqui/qtdeTotOcor) * 100) / totGValorLiqui);
            }
            else
                lblGPercValorLiqui.Text = String.Format("{0:0.0}", 0);
        }

    }
}
