using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3501_RelacDependenAlunos
{
    public partial class RptReçacAlunosDependenciaOrAluno : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptReçacAlunosDependenciaOrAluno()
        {
            InitializeComponent();
        }


        #region Init Report

        public int InitReport(
                           string parametros,
                           string infos,
                           int coEmp,
                           string anoLetivo,
                           int modalidade,
                           int serieCurso,
                           int Turma,
                           int Disciplina,
                           int Professor
                           )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                DateTime dtAtual = DateTime.Now;

                var lst = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb07.CO_ALU equals tb48.CO_ALU
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb48.CO_CUR equals tb01.CO_CUR
                           join tb01Org in TB01_CURSO.RetornaTodosRegistros() on tb48.CO_CUR_ORIGE equals tb01Org.CO_CUR
                           join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb48.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb48.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA

                           join tbrep in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb02.CO_MAT equals tbrep.CO_MAT into l1
                           from ls in l1.DefaultIfEmpty()
                           where (ls != null ? ((dtAtual >= ls.DT_INICIO) && (dtAtual <= ls.DT_FINAL)) : 0 == 0)

                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on ls.CO_COL_RESP equals tb03.CO_COL into l2
                           from lco in l2.DefaultIfEmpty()

                           where tb48.CO_CUR != tb48.CO_CUR_ORIGE
                           && (anoLetivo != "0" ? tb48.CO_ANO_MES_MAT == anoLetivo : 0 == 0)
                           && (modalidade != 0 ? tb48.CO_MODU_CUR == modalidade : 0 == 0)
                           && (serieCurso != 0 ? tb48.CO_CUR == serieCurso : 0 == 0)
                           && (Turma != 0 ? tb48.CO_TUR == Turma : 0 == 0)
                           && (Disciplina != 0 ? tb48.CO_MAT == Disciplina : 0 == 0)

                           select new AlunosDependencia
                           {
                               ano = tb48.CO_ANO_MES_MAT,
                               NomeAluno = tb07.NO_ALU,
                               nuNire = tb07.NU_NIRE,
                               Modalidade = tb44.DE_MODU_CUR,
                               Disciplina = tb107.NO_MATERIA,
                               NoSerieAnoAtual = tb01.NO_CUR,
                               NoSerieAnoAnter = tb01Org.NO_CUR,
                               Professor = lco.NO_COL,
                           }).OrderBy(w => w.Modalidade).ThenBy(y => y.NomeAluno).ThenBy(z => z.Disciplina).Distinct();

                var res = lst.ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                int nume = 0;
                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (AlunosDependencia at in lst)
                {
                    nume++;
                    at.Num = nume;

                    bsReport.Add(at);
                    this.lblinfoTop.Text = "Ano Letivo: " + at.ano + " - " + "Aluno: " + at.alunoValid;
                }

                return 1;

                #endregion ;

            }
            catch { return 0; }
        }

        #endregion

        public class AlunosDependencia
        {
            public string ano { get; set; }

            public string NomeAluno { get; set; }
            public int nuNire { get; set; }
            public string alunoValid
            {
                get
                {
                    string nire = this.nuNire.ToString().PadLeft(7, '0');
                    string nome = this.NomeAluno.Length > 25 ? this.NomeAluno.Substring(0, 25).ToUpper() + "..." : this.NomeAluno.ToUpper();

                    return nire + " - " + nome;
                }
            }

            public string Modalidade { get; set; }
            public string NoSerieAnoAtual { get; set; }
            public string NoSerieAnoAnter { get; set; }
            public string Disciplina { get; set; }
            public string Professor { get; set; }
            public string ProfessorValid
            {
                get
                {
                    return (this.Professor != null ? this.Professor : " *** ");
                }
            }
            public int Num { get; set; }
            public string dataHorario
            {
                get
                {
                    return " - ";
                }
            }
        }
    }
}