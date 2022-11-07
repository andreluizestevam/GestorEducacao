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
    public partial class RptDemQtdAlunosEnsinoFundamental : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptDemQtdAlunosEnsinoFundamental()
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
                           select new ReportAlunosPorUnidSerieFundamental
                           {
                               Unidade = grp.Key.NO_FANTAS_EMP,
                               Sigla = grp.Key.sigla,
                               TipoUnidade = grp.Key.NO_TIPOEMP,
                               Inep = grp.Key.NU_INEP,
                               Qtd1A = grp.Count(x => x.cur.CO_SERIE_REFER == "1F"),
                               Qtd2A = grp.Count(x => x.cur.CO_SERIE_REFER == "2F"),
                               Qtd3A = grp.Count(x => x.cur.CO_SERIE_REFER == "3F"),
                               Qtd4A = grp.Count(x => x.cur.CO_SERIE_REFER == "4F"),
                               Qtd5A = grp.Count(x => x.cur.CO_SERIE_REFER == "5F"),
                               Qtd6A = grp.Count(x => x.cur.CO_SERIE_REFER == "6F"),
                               Qtd7A = grp.Count(x => x.cur.CO_SERIE_REFER == "7F"),
                               Qtd8A = grp.Count(x => x.cur.CO_SERIE_REFER == "8F"),
                               Qtd9A = grp.Count(x => x.cur.CO_SERIE_REFER == "9F")
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
                            select new ReportAlunosPorUnidSerieFundamental
                            {
                                Unidade = e.NO_FANTAS_EMP,
                                Sigla = e.sigla,
                                TipoUnidade = e.TB24_TPEMPRESA.NO_TIPOEMP,
                                Inep = e.NU_INEP,
                                Qtd1A = 0,
                                Qtd2A = 0,
                                Qtd3A = 0,
                                Qtd4A = 0,
                                Qtd5A = 0,
                                Qtd6A = 0,
                                Qtd7A = 0,
                                Qtd8A = 0,
                                Qtd9A = 0
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

        public class ReportAlunosPorUnidSerieFundamental
        {
            public string Unidade { get; set; }
            public int? Inep { get; set; }
            public string TipoUnidade { get; set; }
            public string Sigla { get; set; }
            public int Qtd1A { get; set; }
            public int Qtd2A { get; set; }
            public int Qtd3A { get; set; }
            public int Qtd4A { get; set; }
            public int Qtd5A { get; set; }
            public int Qtd6A { get; set; }
            public int Qtd7A { get; set; }
            public int Qtd8A { get; set; }
            public int Qtd9A { get; set; }
            public int Total { get { return Qtd1A + Qtd2A + Qtd3A + Qtd4A + Qtd5A + Qtd6A + Qtd7A + Qtd8A + Qtd9A; } }
        } 

        #endregion
    }
}
