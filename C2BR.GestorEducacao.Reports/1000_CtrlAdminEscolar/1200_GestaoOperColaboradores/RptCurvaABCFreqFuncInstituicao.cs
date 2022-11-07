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
    public partial class RptCurvaABCFreqFuncInstituicao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptCurvaABCFreqFuncInstituicao()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codInst, DateTime dtInicio, DateTime dtFim, string infos)
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
                           where tb199.TB25_EMPRESA.CO_EMP == codInst &&
                           tb199.DT_FREQ >= dtInicio && tb199.DT_FREQ <= dtFim
                           && tb199.STATUS == "A"
                           group tb199 by new { tb199.FLA_PRESENCA, tb199.TB03_COLABOR.NO_COL } into grp
                           select new FrequenciaPorInstituicao
                           {
                               Tipo = grp.Key.FLA_PRESENCA == "S" ? "Presença" : "Falta",
                               Funcionario = grp.Key.NO_COL,
                               Qtd = grp.Count()
                           }).OrderBy(p => p.Funcionario);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                #region Labels

                this.lblInst.Text = (from tb25 in ctx.TB25_EMPRESA
                                     where tb25.CO_EMP == codInst
                                     select tb25.NO_FANTAS_EMP).First();

                this.lblPeriodo.Text = string.Format("{0} à {1}", dtInicio.ToString("dd/MM/yyyy"),
                    dtFim.ToString("dd/MM/yyyy"));

                #endregion

                int qtdPresenca = res.Where(x => x.Tipo == "Presença").Sum(x => x.Qtd);
                int qtdFalta = res.Where(x => x.Tipo == "Falta").Sum(x => x.Qtd);

                foreach (FrequenciaPorInstituicao at in res)
                {
                    if (at.Tipo == "Presença")
                        at.Percentual = qtdPresenca > 0 ? (((decimal)at.Qtd / qtdPresenca) * 100) : 0;
                    else
                        at.Percentual = qtdFalta > 0 ? (((decimal)at.Qtd / qtdFalta) * 100) : 0;
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (FrequenciaPorInstituicao at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe FrequenciaPorInstituicao do Relatorio

        public class FrequenciaPorInstituicao
        {
            public string Funcionario { get; set; }
            public string Tipo { get; set; }
            public int Qtd { get; set; }
            public decimal Percentual { get; set; }
        }
        #endregion
    }
}
