using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptQuadroDemUnidBairro : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptQuadroDemUnidBairro()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codOrg, int codCid, int codBairro, string infos)
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

                #region Query Ocorrencias

                var res = (from bai in ctx.TB905_BAIRRO
                           join emp in ctx.TB25_EMPRESA on bai.CO_BAIRRO equals emp.CO_BAIRRO into e
                           from emp in e.DefaultIfEmpty()
                           where bai.CO_CIDADE == codCid
                           && emp.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codOrg
                           && (codBairro == 0 ? true : bai.CO_BAIRRO == codBairro)
                           group new { bai, emp } by bai.NO_BAIRRO into grp
                           select new UnidadeEfetivoPorBairro
                           {
                               Bairro = grp.Key,
                               QtdUnidades = grp.Count(),
                               Funcionarios = (from f in ctx.TB03_COLABOR
                                               where f.FLA_PROFESSOR == "N"
                                               && grp.Select(x => x.emp.CO_EMP).Contains(f.CO_EMP)
                                               select f.CO_COL).Count(),
                               Professores = (from p in ctx.TB03_COLABOR
                                              where p.FLA_PROFESSOR == "S"
                                              && grp.Select(x => x.emp.CO_EMP).Contains(p.CO_EMP)
                                              select p.CO_COL).Count(),
                               Alunos = (from a in ctx.TB07_ALUNO
                                         where grp.Select(x => x.emp.CO_EMP).Contains(a.CO_EMP)
                                         select a.CO_ALU).Count()
                           }).OrderBy(x => x.Bairro).ToList();

                if (res.Count() == 0)
                    return -1;

                #endregion

                // Adiciona os movimentos ao DataSource do Relatório
                bsReport.Clear();

                int qtdTotalUnid = res.Sum(x => x.QtdUnidades);
                int qtdAlunos = res.Sum(x => x.Alunos);
                int qtdProf = res.Sum(x => x.Professores);
                int qtdFunc = res.Sum(x => x.Funcionarios);

                foreach (var r in res)
                {
                    r.MediaAlunos = r.QtdUnidades == 0 ? 0 : (decimal)r.Alunos / (decimal)r.QtdUnidades;
                    r.MediaFuncionarios = r.QtdUnidades == 0 ? 0 : (decimal)r.Funcionarios / (decimal)r.QtdUnidades;
                    r.MediaProfessores = r.QtdUnidades == 0 ? 0 : (decimal)r.Professores / (decimal)r.QtdUnidades;

                    r.MediaTotalAlunos = qtdTotalUnid == 0 ? 0 : (decimal)qtdAlunos / (decimal)qtdTotalUnid;
                    r.MediaTotalFuncionarios = qtdTotalUnid == 0 ? 0 : (decimal)qtdFunc / (decimal)qtdTotalUnid;
                    r.MediaTotalProfessores = qtdTotalUnid == 0 ? 0 : (decimal)qtdProf / (decimal)qtdTotalUnid;

                    r.PercentAlunos = qtdAlunos == 0 ? 0 : ((decimal)r.Alunos / (decimal)qtdAlunos) * (decimal)100;
                    r.PercentFuncionarios = qtdProf == 0 ? 0 : ((decimal)r.Funcionarios / (decimal)qtdFunc) * (decimal)100;
                    r.PercentProfessores = qtdFunc == 0 ? 0 : ((decimal)r.Professores / (decimal)qtdProf) * (decimal)100;
                }

                foreach (var m in res)
                    bsReport.Add(m);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class UnidadePorBairro

        public class UnidadeEfetivoPorBairro
        {
            public string Bairro { get; set; }
            public int QtdUnidades { get; set; }

            public int Funcionarios { get; set; }
            public int Professores { get; set; }
            public int Alunos { get; set; }

            public decimal MediaFuncionarios { get; set; }
            public decimal MediaProfessores { get; set; }
            public decimal MediaAlunos { get; set; }

            public decimal MediaTotalFuncionarios { get; set; }
            public decimal MediaTotalProfessores { get; set; }
            public decimal MediaTotalAlunos { get; set; }

            public decimal PercentFuncionarios { get; set; }
            public decimal PercentProfessores { get; set; }
            public decimal PercentAlunos { get; set; }
        }

        #endregion

    }
}
