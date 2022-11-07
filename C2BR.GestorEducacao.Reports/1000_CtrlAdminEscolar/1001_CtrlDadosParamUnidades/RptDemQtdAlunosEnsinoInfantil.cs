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
    public partial class RptDemQtdAlunosEnsinoInfantil : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptDemQtdAlunosEnsinoInfantil()
        {
            InitializeComponent();
        }

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
                           select new ReportAlunosPorUnidSerie
                           {
                               Unidade = grp.Key.NO_FANTAS_EMP,
                               Sigla = grp.Key.sigla,
                               TipoUnidade = grp.Key.NO_TIPOEMP,
                               Inep = grp.Key.NU_INEP,
                               Qtd1I = grp.Count(x => x.cur.CO_SERIE_REFER == "1I"),
                               Qtd2I = grp.Count(x => x.cur.CO_SERIE_REFER == "2I"),
                               Qtd3I = grp.Count(x => x.cur.CO_SERIE_REFER == "3I"),
                               Qtd4I = grp.Count(x => x.cur.CO_SERIE_REFER == "4I"),
                               Qtd5I = grp.Count(x => x.cur.CO_SERIE_REFER == "5I")
                           }).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                foreach (var r in res)
                    bsReport.Add(r);

                return 1;
            }
            catch { return 0; }
        }

        public class ReportAlunosPorUnidSerie
        {
            public string Unidade { get; set; }
            public int? Inep { get; set; }
            public string TipoUnidade { get; set; }
            public string Sigla { get; set; }
            public int Qtd1I { get; set; }
            public int Qtd2I { get; set; }
            public int Qtd3I { get; set; }
            public int Qtd4I { get; set; }
            public int Qtd5I { get; set; }
            public int Total { get { return Qtd1I + Qtd2I + Qtd3I + Qtd4I + Qtd5I; } }
        }
    }
}
