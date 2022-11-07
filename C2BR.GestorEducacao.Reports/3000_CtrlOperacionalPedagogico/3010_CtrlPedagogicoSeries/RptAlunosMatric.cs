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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries
{
    public partial class RptAlunosMatric : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptAlunosMatric()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codUni,
                               int codMod,
                               int codCurso,
                               string ano,
                               int codTurma,
                               string situacao,
                               int ordem,
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

                #region Query Alunos/Serie

                var lst = from mm in ctx.TB08_MATRCUR
                          from a in ctx.TB07_ALUNO
                          from t in ctx.TB06_TURMAS
                          from c in ctx.TB01_CURSO
                          from r in ctx.TB108_RESPONSAVEL
                          from ct in ctx.TB129_CADTURMAS
                          from u in ctx.TB25_EMPRESA
                          where
                           mm.CO_EMP == a.CO_EMP && mm.CO_EMP == t.CO_EMP &&
                           mm.CO_EMP == c.CO_EMP && a.CO_ALU == mm.CO_ALU &&
                           mm.CO_TUR == t.CO_TUR && ct.CO_TUR == t.CO_TUR &&
                           mm.TB44_MODULO.CO_MODU_CUR == t.CO_MODU_CUR &&
                           t.CO_MODU_CUR == ct.TB44_MODULO.CO_MODU_CUR &&
                           mm.CO_CUR == c.CO_CUR && r.CO_RESP == a.TB108_RESPONSAVEL.CO_RESP &&
                           mm.CO_EMP == u.CO_EMP
                           && (codMod != 0 ? mm.TB44_MODULO.CO_MODU_CUR == codMod : 0 == 0)
                           && (codCurso != 0 ? c.CO_CUR == codCurso : 0 == 0)
                           && (codTurma != 0 ? ct.CO_TUR == codTurma : 0 == 0)
                           && (codUni != 0 ? mm.CO_EMP == codUni : 0 == 0)
                           && mm.CO_ANO_MES_MAT == ano
                          select new AlunoMatric
                          {
                              Sexo = a.CO_SEXO_ALU,
                              CodCurso = mm.CO_CUR,
                              CodEmpresa = mm.CO_EMP,
                              CodMod = mm.TB44_MODULO.CO_MODU_CUR,
                              AnoMat = mm.CO_ANO_MES_MAT,
                              CodTurma = mm.CO_TUR.Value,
                              DataNascimento = a.DT_NASC_ALU,
                              Matricula = a.NU_NIRE,
                              IdentMEC = "00000000",
                              Nome = a.NO_ALU,
                              Responsavel = r.NO_RESP,
                              Situacao = mm.CO_SIT_MAT,
                              Telefone = a.NU_TELE_RESI_ALU,
                              Turma = ct.CO_SIGLA_TURMA,
                              NO_MODALIDADE = mm.TB44_MODULO.DE_MODU_CUR,
                              NO_CURSO = c.NO_CUR,
                              NO_TURMA = ct.NO_TURMA,
                              NO_UNIDADE = u.sigla
                          };

                if (situacao != "0")
                    lst = lst.Where(x => x.Situacao == situacao);

                var res = lst.OrderByDescending(w => w.NO_MODALIDADE).ThenByDescending(w => w.NO_CURSO).ThenByDescending(w => w.NO_TURMA).ThenBy(w => w.Nome).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                if (ordem != 0)
                {
                    switch (ordem)
                    {
                        case 1: //Unidade, Série e Turma
                            res = res.OrderBy(r => r.NO_UNIDADE).ThenBy(r => r.NO_CURSO).ThenBy(r => r.Turma).ToList();
                            break;
                        case 2: //Identificação MEC
                            res = res.OrderBy(r => r.IdentMEC).ToList();
                            break;
                        case 3: //Nome do Aluno
                            res = res.OrderBy(r => r.Nome).ToList();
                            break;
                        case 4: //Responsável
                            res = res.OrderBy(r => r.Responsavel).ToList();
                            break;
                        case 5: //Idade
                            res = res.OrderBy(r => r.Idade).ToList();
                            break;
                        case 6: //Situação
                            res = res.OrderBy(r => r.SituacaoDesc).ToList();
                            break;
                        default:
                            break;
                    }
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (AlunoMatric at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Alunos/Series do Relatorio

        public class AlunoMatric
        {
            public int CodMod { get; set; }
            public int CodCurso { get; set; }
            public int CodTurma { get; set; }
            public int CodEmpresa { get; set; }
            public string AnoMat { get; set; }
            public string Sexo { get; set; }
            public int Matricula { get; set; }
            public string IdentMEC { get; set; }
            public string Nome { get; set; }
            public string Responsavel { get; set; }
            public DateTime? DataNascimento { get; set; }
            public string Situacao { get; set; }
            public string Telefone { get; set; }
            public string Turma { get; set; }

            public string NO_UNIDADE { get; set; }
            public string NO_MODALIDADE { get; set; }
            public string NO_CURSO { get; set; }
            public string NO_TURMA { get; set; }
            public string CONCAT_MOD
            {
                get
                {
                    return this.NO_MODALIDADE + " - " + this.NO_CURSO + " - " + this.NO_TURMA;
                }
            }

            public string NomeToUpper
            {
                get { return this.Nome.ToUpper(); }
            }
            public string MatriculDesc
            {
                get
                {
                    return this.Matricula.ToString().PadLeft(9, '0').Insert(1, ".").Insert(5, ".").Insert(9, "-");
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

            public string SituacaoDesc
            {
                get
                {
                    if (this.Situacao == "A")
                        return "Matriculado";
                    else if (this.Situacao == "I")
                        return "Inativo";
                    else if (this.Situacao == "A")
                        return "Matriculado";
                    else
                        return "Matriculado";
                }
            }

            public int Idade
            {
                get { return (this.DataNascimento.HasValue ? Funcoes.GetIdade(this.DataNascimento.Value) : 0); }
            }
        }

        #endregion
    }
}
