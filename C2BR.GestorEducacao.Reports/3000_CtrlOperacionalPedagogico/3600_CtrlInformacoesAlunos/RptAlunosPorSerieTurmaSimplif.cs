using System;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using System.Drawing;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptAlunosPorSerieTurmaSimplif : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor
        public RptAlunosPorSerieTurmaSimplif()
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
                              string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisibleNumeroPage = false;

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
                           && tb08.CO_SIT_MAT != "C"
                           select new AlunosSerieTurmaSimplif
                           {
                               NIRE = tb07.NU_NIRE,
                               Aluno = tb07.NO_ALU,
                               Modalidade = tb08.TB44_MODULO.DE_MODU_CUR,
                               Serie = tb01.NO_CUR,
                               Turma = tb129.NO_TURMA,
                               dtNasc = tb07.DT_NASC_ALU.Value,
                               coSexo = tb07.CO_SEXO_ALU,
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

        public class AlunosSerieTurmaSimplif
        {
            public string Ano { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string coSexo { get; set; }
            public DateTime? dtNasc { get; set; }
            public int? CoTur { get; set; }
            public int NIRE { get; set; }
            public string Aluno { get; set; }

            public string sexo
            {
                get
                {
                    string s = "";

                    switch (this.coSexo)
                    {
                        case "M":
                            s = "MAS";
                            break;
                        case "F":
                            s = "FEM";
                            break;
                    }

                    return s;
                }
            }

            public string Nascimento
            {
                get
                {
                    if (dtNasc.HasValue)
                    {
                        return dtNasc.Value.ToString("dd/MM/yyyy") + " (" + Funcoes.GetIdade(this.dtNasc.Value).ToString() + ")"; 
                    }
                    else
                    {
                        return " - " ;
                    }
                   
                }
            }

            public string DescNIRE
            {
                get
                {
                    return this.NIRE.ToString().PadLeft(9,'0');
                }
            }

            public string DescAluno
            {
                get
                {
                    return this.Aluno.ToUpper();
                }
            }

            public string DescSerie
            {
                get
                {
                    return "Ano: " + this.Ano.Trim() + " - Modalidade: " + this.Modalidade.ToUpper() + " - Série: " + this.Serie.ToUpper() + " - Turma: " + this.Turma.ToUpper();
                }
            }
        }

        #endregion
    }
}
