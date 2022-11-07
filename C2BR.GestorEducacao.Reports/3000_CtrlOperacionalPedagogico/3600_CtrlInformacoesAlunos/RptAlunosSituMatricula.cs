using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptAlunosSituMatricula : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptAlunosSituMatricula()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codUndCont,
                               string anoRefer,
                               int codMod,
                               int codCurso,
                               DateTime dtInicio,
                               DateTime dtFim,
                               int codTurma,
                               string situacao,
                               int CodOrdem,
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

                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");

                #region Query Alunos

                var lst = from mat in ctx.TB08_MATRCUR
                          join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                          join turma in ctx.TB06_TURMAS on mat.CO_TUR equals turma.CO_TUR
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          where mat.DT_SIT_MAT >= dtInicio && mat.DT_SIT_MAT <= dtFim
                          && (codUndCont != 0 ? turma.CO_EMP == codUndCont : 0 == 0)
                          && (codTurma != 0 ? turma.CO_TUR == codTurma : 0 == 0)
                          && (codCurso != 0 ? cur.CO_CUR == codCurso : 0 == 0)
                          && (codMod != 0 ? turma.CO_MODU_CUR == codMod : 0 == 0)
                          && (situacao != "U" ? mat.CO_SIT_MAT == situacao : 0 == 0)
                          && (anoRefer != "T" ? mat.CO_ANO_MES_MAT == anoRefer : 0 == 0)
                          select new Aluno
                          {
                              Sexo = alu.CO_SEXO_ALU,
                              Serie = cur.CO_SIGL_CUR,
                              UndCont = mat.TB25_EMPRESA.sigla,
                              Modalidade = mat.TB44_MODULO.CO_SIGLA_MODU_CUR,
                              Turma = turma.TB129_CADTURMAS.CO_SIGLA_TURMA,
                              DataNascimento = alu.DT_NASC_ALU,
                              Nire = alu.NU_NIRE,
                              Nis = alu.NU_NIS,
                              Nome = alu.NO_ALU,
                              Responsavel = alu.TB108_RESPONSAVEL.NO_RESP,
                              CPFResp = alu.TB108_RESPONSAVEL.NU_CPF_RESP,
                              Situacao = mat.CO_SIT_MAT,
                              DataMatricula = mat.DT_SIT_MAT,
                              Deficiencia = alu.TP_DEF,
                              AnoRefer = mat.CO_ANO_MES_MAT
                          };

                var res = lst.ToList();

                switch (CodOrdem)
                {
                    case 1:
                        res = lst.OrderBy(p => new { p.UndCont, p.Nome }).ToList();
                        break;
                    case 2:
                        res = lst.OrderBy(p => new { p.Modalidade, p.Serie, p.Turma, p.Nome }).ToList();
                        break;
                    case 3:
                        res = lst.OrderBy(p => new { p.DataMatricula, p.Nome }).ToList();
                        break;
                    case 4:
                        res = lst.OrderBy(p => new { p.Nome }).ToList();
                        break;
                    case 5:
                        res = lst.OrderBy(p => new { p.Responsavel, p.Nome }).ToList();
                        break;                   
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Aluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Alunos do Relatorio

        public class Aluno
        {
            public string AnoRefer { get; set; }
            public string Serie { get; set; }
            public DateTime? DataNascimento { get; set; }
            public decimal? Nis { get; set; }
            public string Nome { get; set; }
            public string Responsavel { get; set; }
            public string CPFResp { get; set; }
            public string Sexo { get; set; }
            public string UndCont { get; set; }
            public string Modalidade { get; set; }
            public string Turma { get; set; }
            public int Nire { get; set; }
            public string Situacao { get; set; }
            public DateTime DataMatricula { get; set; }
            public string Deficiencia { get; set; }

            public string NomeDesc
            {
                get { return this.Nome.ToUpper(); }
            }

            public string DataNascDesc
            {
                get { return this.DataNascimento != null ? this.DataNascimento.Value.ToString("dd/MM/yyyy") : "XX/XX/XX"; }
            }

            public string SituacaoDesc
            {
                get
                {
                    string res = "{0} / {1:dd/MM/yy}";

                    if (this.Situacao == "A")
                        return string.Format(res, "Matriculado", DataMatricula);
                    else if (this.Situacao == "C")
                        return string.Format(res, "Cancelada", DataMatricula);
                    else if (this.Situacao == "T")
                        return string.Format(res, "Trancada", DataMatricula);
                    else if (this.Situacao == "X")
                        return string.Format(res, "Transferida", DataMatricula);
                    else if (this.Situacao == "F")
                        return string.Format(res, "Finalizada", DataMatricula);
                    else
                        return string.Format(res, "Pendente", DataMatricula);
                }
            }

            public string Idade
            {
                get { return this.DataNascimento != null ? Funcoes.GetIdade(this.DataNascimento.Value).ToString() : "XX"; }
            }

            public string DeficienciaDesc
            {
                get { return Funcoes.GetDeficienciaColabor(this.Deficiencia); }
            }

            public string SerieTurma
            {
                get { return this.Serie + "/" + this.Turma; }
            }

            public string NireDesc
            {
                get { return this.Nire.ToString().PadLeft(9, '0'); }
            }

            public string RespDescricao { get { return this.Responsavel != null ? (CPFResp.Format(TipoFormat.CPF) + " - " + Responsavel.ToUpper()) : "*****"; } }            
        }

        #endregion
    }
}
