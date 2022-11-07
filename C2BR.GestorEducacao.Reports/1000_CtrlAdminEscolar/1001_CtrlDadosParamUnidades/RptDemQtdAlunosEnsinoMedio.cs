using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptDemQtdAlunosEnsinoMedio : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptDemQtdAlunosEnsinoMedio()
        {
            InitializeComponent();
        } 

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codOrg, string ano, string situacao, string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Principal

                var res = (from m in ctx.TB08_MATRCUR
                           join cur in ctx.TB01_CURSO on m.CO_CUR equals cur.CO_CUR into cr
                           from cur in cr.DefaultIfEmpty()
                           where m.CO_ANO_MES_MAT.Trim() == ano
                           && m.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codOrg
                           && (situacao != "O" ? m.CO_SIT_MAT == situacao : true)
                           group new { cur, m.TB25_EMPRESA }
                           by new
                           {
                               m.TB25_EMPRESA.NO_FANTAS_EMP,
                               m.TB25_EMPRESA.sigla,
                               m.TB25_EMPRESA.TB24_TPEMPRESA.NO_TIPOEMP,
                               m.TB25_EMPRESA.NU_INEP
                           } into grp
                           select new ReportAlunosPorUnidSerieMedio
                           {
                               Unidade = grp.Key.NO_FANTAS_EMP,
                               Sigla = grp.Key.sigla,
                               TipoUnidade = grp.Key.NO_TIPOEMP,
                               Inep = grp.Key.NU_INEP,
                               Qtd1A = grp.Count(x => x.cur.CO_SERIE_REFER == "1M"),
                               Qtd2A = grp.Count(x => x.cur.CO_SERIE_REFER == "2M"),
                               Qtd3A = grp.Count(x => x.cur.CO_SERIE_REFER == "3M")
                           });

                //string q = (res as ObjectQuery).ToTraceString();


                #endregion

                // Erro: não encontrou registros
                if (res.Count() == 0)
                    return -1;

                #region Query Todas as Unidades

                var res2 = (from e in ctx.TB25_EMPRESA
                            where e.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codOrg && 
                            !res.Select(x => x.Unidade).Contains(e.NO_FANTAS_EMP)
                            select new ReportAlunosPorUnidSerieMedio
                            {
                                Unidade = e.NO_FANTAS_EMP,
                                Sigla = e.sigla,
                                TipoUnidade = e.TB24_TPEMPRESA.NO_TIPOEMP,
                                Inep = e.NU_INEP,
                                Qtd1A = 0,
                                Qtd2A = 0,
                                Qtd3A = 0
                            });

                var resT = res.ToList();
                resT.AddRange(res2);

                #endregion

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                foreach (var r in resT)
                    bsReport.Add(r);

                return 1;
            }
            catch { return 0; }
        } 

        #endregion

        #region Class Helper

        public class ReportAlunosPorUnidSerieMedio
        {
            public string Unidade { get; set; }
            public int? Inep { get; set; }
            public string TipoUnidade { get; set; }
            public string Sigla { get; set; }
            public int Qtd1A { get; set; }
            public int Qtd2A { get; set; }
            public int Qtd3A { get; set; }
            public int Total { get { return Qtd1A + Qtd2A + Qtd3A; } }
        } 

        #endregion
    }
}
