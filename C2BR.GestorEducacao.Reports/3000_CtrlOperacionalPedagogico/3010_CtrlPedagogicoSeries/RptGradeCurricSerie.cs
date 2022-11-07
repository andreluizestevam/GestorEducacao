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
    public partial class RptGradeCurricSerie : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGradeCurricSerie()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codEmpRef,
                               int codMod,
                               int codCurso,
                               int strP_CO_ANO_INI,
                               int strP_CO_ANO_FIM,
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

                #region Query Grade Série

                var resultado = (from tb43 in ctx.TB43_GRD_CURSO
                          join tb01 in ctx.TB01_CURSO on tb43.CO_CUR equals tb01.CO_CUR
                          join tb02 in ctx.TB02_MATERIA on tb43.CO_MAT equals tb02.CO_MAT
                          join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                          where
                           tb01.CO_EMP == codEmpRef
                           && (codCurso != 0 ? tb01.CO_CUR == codCurso : 0 == 0)
                           && (codMod != 0 ? tb01.TB44_MODULO.CO_MODU_CUR == codMod : 0 == 0)
                          select new
                          {
                              Ano = tb43.CO_ANO_GRADE,
                              Modalidade = tb01.TB44_MODULO.DE_MODU_CUR,
                              Serie = tb01.NO_CUR,
                              Disciplina = tb107.NO_MATERIA,
                              SiglaDisciplina = tb107.NO_SIGLA_MATERIA,
                              CargaHoraria = tb02.QT_CARG_HORA_MAT,
                              Credito = tb02.QT_CRED_MAT
                          }).ToList().OrderBy(p => p.Ano).ThenBy(p => p.Disciplina);

                var lst = (from iRes in resultado
                          where Convert.ToInt32(iRes.Ano) >= strP_CO_ANO_INI
                           && Convert.ToInt32(iRes.Ano) <= strP_CO_ANO_FIM
                           select new GradeSerie
                          {
                              Ano = iRes.Ano,
                              Modalidade = iRes.Modalidade,
                              Serie = iRes.Serie,
                              Disciplina = iRes.Disciplina,
                              SiglaDisciplina = iRes.SiglaDisciplina.ToUpper(),
                              CargaHoraria = iRes.CargaHoraria,
                              Credito = iRes.Credito
                          }).ToList().OrderBy(p => p.Ano).ThenBy(p => p.Disciplina);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (GradeSerie at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Grade de Série do Relatorio

        public class GradeSerie
        {
            public string Ano { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string SiglaDisciplina { get; set; }
            public string Disciplina { get; set; }
            public decimal? CargaHoraria { get; set; }
            public decimal? Credito { get; set; }
        }

        #endregion
    }
}
