using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores
{
    public partial class RptColaborCursosFormacao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptColaborCursosFormacao()
        {
            InitializeComponent();
        } 

        #endregion

        #region Init report

        public int InitReport(int codEmp, int codFun, string infos)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador

                var func = (from c in ctx.TB03_COLABOR

                            where c.CO_COL == codFun
                            select new Colaborador
                            {
                                Nome = c.NO_COL,
                                Categoria = (c.FLA_PROFESSOR == "N") ? "Funcionário" : "Professor",
                                Sexo = c.CO_SEXO_COL,
                                DtNascimento = c.DT_NASC_COL,
                                Matricula = c.CO_MAT_COL,
                                Deficiencia = c.TP_DEF,

                            }).FirstOrDefault();

                #endregion

                #region Cursos do Funcionário

                var lstCur = (from cf in ctx.TB62_CURSO_FORM.Include("TB100_ESPECIALIZACAO")
                              where cf.CO_COL == codFun
                              select new Curso
                              {
                                  CargaHoraria = cf.NU_CARGA_HORARIA,
                                  Conclusao = cf.CO_MESANO_FIM,
                                  Instituicao = cf.NO_INSTIT_CURSO,
                                  Local = cf.NO_CIDADE_CURSO + "/" + cf.CO_UF_CURSO,
                                  Nome = cf.TB100_ESPECIALIZACAO.DE_ESPEC,
                                  Tipo = cf.TB100_ESPECIALIZACAO.TP_ESPEC
                              }).ToList();

                if (lstCur.Count == 0)
                    return -1;

                func.Cursos = lstCur;

                #endregion

                // Se não encontrou o funcionário
                if (func == null)
                    return -1;

                // Adiciona o Funcionario ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(func);

                return 1;
            }
            catch { return 0; }
        } 

        #endregion

        #region Colaborador Class Helper

        public class Colaborador
        {
            public Colaborador()
            {
                this.Cursos = new List<Curso>();
            }

            public string Nome { get; set; }
            public string Categoria { get; set; }

            public string _matricula;
            public string Matricula
            {
                get { return this._matricula.Format(TipoFormat.MatriculaColaborador); }
                set { this._matricula = value; }
            }

            public DateTime DtNascimento { get; set; }
            public string DtNascStr
            {
                get
                {
                    return string.Format("{0:d} ({1})", DtNascimento, Funcoes.GetIdade(DtNascimento));
                }
            }

            public string Sexo { get; set; }
            public string _deficiencia;
            public string Deficiencia
            {
                get { return Funcoes.GetDeficienciaColabor(this._deficiencia); }
                set { this._deficiencia = value; }
            }

            public List<Curso> Cursos { get; set; }
        }

        public class Curso
        {
            public string Nome { get; set; }
            public int CargaHoraria { get; set; }
            public string Instituicao { get; set; }
            public string Conclusao { get; set; }
            public string _tipo;
            public string Tipo
            {
                get { return Funcoes.GetTipoEspecializacao(this._tipo); }
                set { this._tipo = value; }
            }
            public string Local { get; set; }
        } 

        #endregion
    }
}