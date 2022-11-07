using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores
{
    public partial class RptCurvaABCFreqFuncFuncao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptCurvaABCFreqFuncFuncao()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codFuncao, DateTime dtInicio, DateTime dtFim, string infos)
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

                #region Query Frequencia

                var lst = (from tb199 in ctx.TB199_FREQ_FUNC
                           join f in ctx.TB15_FUNCAO on tb199.TB03_COLABOR.CO_FUN equals f.CO_FUN into fi
                           from f in fi.DefaultIfEmpty()
                           where tb199.TB25_EMPRESA.CO_EMP == codEmp && tb199.DT_FREQ >= dtInicio
                           && tb199.DT_FREQ <= dtFim && tb199.STATUS == "A" &&
                           (codFuncao == 0 ? true : f.CO_FUN == codFuncao)
                           group tb199 by new { tb199.FLA_PRESENCA, f.NO_FUN } into grp
                           select new FrequenciaPorFuncao
                           {
                               Tipo = grp.Key.FLA_PRESENCA == "S" ? "Presença" : "Falta",
                               Funcao = grp.Key.NO_FUN,
                               Qtd = grp.Count()
                           }).OrderBy(p => p.Funcao);

                var sql = (lst as ObjectQuery).ToTraceString();

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                #region Labels

                this.lblPeriodo.Text = string.Format("{0} à {1}", dtInicio.ToString("dd/MM/yyyy"),
                    dtFim.ToString("dd/MM/yyyy"));

                #endregion

                int qtdPresenca = res.Where(x => x.Tipo == "Presença").Sum(x => x.Qtd);
                int qtdFalta = res.Where(x => x.Tipo == "Falta").Sum(x => x.Qtd);

                foreach (FrequenciaPorFuncao f in res)
                {
                    if (f.Tipo == "Presença")
                        f.Percentual = qtdPresenca > 0 ? (((decimal)f.Qtd / qtdPresenca) * 100) : 0;
                    else
                        f.Percentual = qtdFalta > 0 ? (((decimal)f.Qtd / qtdFalta) * 100) : 0;
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (FrequenciaPorFuncao f in res)
                    bsReport.Add(f);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe FrequenciaPorFuncao do Relatorio

        public class FrequenciaPorFuncao
        {
            public string Funcao { get; set; }
            public string Tipo { get; set; }
            public int Qtd { get; set; }
            public decimal Percentual { get; set; }
        }
        #endregion
    }
}
