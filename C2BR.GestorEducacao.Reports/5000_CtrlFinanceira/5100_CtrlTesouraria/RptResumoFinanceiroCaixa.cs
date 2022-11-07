using System;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using DevExpress.XtraReports.UI;
using System.Drawing;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria
{
    public partial class RptResumoFinanceiroCaixa : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptResumoFinanceiroCaixa()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              int codEmpRef,
                              int codUndContrato,
                              int codCaixa,
                              int codCol,
                              string situCaixa,
                              DateTime dtIni,
                              DateTime dtFim,
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

                #region Query Report

                var lst = (from tb296 in ctx.TB296_CAIXA_MOVIMENTO
                           join tb295 in ctx.TB295_CAIXA on tb296.CO_CAIXA equals tb295.CO_CAIXA
                           join tb113 in ctx.TB113_PARAM_CAIXA on tb295.CO_CAIXA equals tb113.CO_CAIXA
                           join colCaixa in ctx.TB03_COLABOR on tb295.CO_COLABOR_CAIXA equals colCaixa.CO_COL into colCx
                           from colCaixa in colCx.DefaultIfEmpty()

                           #region Query Removida

                           //join tb47 in ctx.TB47_CTA_RECEB on tb296.NU_DOC_CAIXA equals tb47.NU_DOC into tit
                           //from tb47 in tit.DefaultIfEmpty()

                           //join usrAbertura in ctx.ADMUSUARIO on tb295.CO_USUARIO_ABERT equals usrAbertura.ideAdmUsuario into usrA
                           //from usrAbertura in usrA.DefaultIfEmpty()
                           //join colAbertura in ctx.TB03_COLABOR on usrAbertura.CodUsuario equals colAbertura.CO_COL into colA
                           //from colAbertura in colA.DefaultIfEmpty()

                           //join usrFechamento in ctx.ADMUSUARIO on tb295.CO_USUARIO_FECHA equals usrFechamento.ideAdmUsuario into usrF
                           //from usrFechamento in usrF.DefaultIfEmpty()
                           //join colFechamento in ctx.TB03_COLABOR on usrFechamento.CodUsuario equals colFechamento.CO_COL into colF
                           //from colFechamento in colF.DefaultIfEmpty() 
                           #endregion

                           where tb295.CO_EMP == codEmpRef
                           && (tb295.DT_MOVIMENTO >= dtIni && tb295.DT_MOVIMENTO <= dtFim)
                           && (codCaixa != 0 ? tb295.CO_CAIXA == codCaixa : codCaixa == 0)
                           && (codCol != 0 ? tb295.CO_COLABOR_CAIXA == codCol : codCol == 0)
                               //&& (codUndContrato != 0 ? tb47.CO_EMP_UNID_CONT == codUndContrato : codUndContrato == 0)
                           && (situCaixa != "T" ? tb113.CO_SITU_CAIXA == situCaixa : situCaixa == "T")
                           group new
                           {
                               tb295.DT_MOVIMENTO,
                               tb295.DT_ABERTURA_CAIXA,
                               tb295.HR_ABERTURA_CAIXA,
                               tb295.DT_FECHAMENTO_CAIXA,
                               tb295.HR_FECHAMENTO_CAIXA,
                               tb295.VR_ABERTURA_CAIXA,
                               tb295.VR_APORTE_CAIXA,
                               tb295.VR_SANGRIA_CAIXA,
                               tb295.VR_DIFERENCA_CAIXA,
                               tb296.TP_OPER_CAIXA,
                               tb296.VR_LIQ_DOC,
                               MovDtMovimento = tb296.DT_MOVIMENTO,
                               tb113.CO_SIGLA_CAIXA,
                               colCaixa.CO_MAT_COL,
                               tb295.CO_USUARIO_FECHA,
                               tb295.CO_USUARIO_ABERT
                               //AplColAbertura = colAbertura.NO_APEL_COL,
                               //MatColFechamento = colFechamento.CO_MAT_COL
                           } by new { tb296.DT_MOVIMENTO, tb296.CO_CAIXA } into grp
                           select new ResumoFinanceiro
                           {
                               DtMovimento = grp.Key.DT_MOVIMENTO,
                               Caixa = grp.FirstOrDefault().CO_SIGLA_CAIXA,
                               Colaborador = grp.FirstOrDefault().CO_MAT_COL,
                               AberturaData = grp.FirstOrDefault().DT_ABERTURA_CAIXA,

                               AberturaHora = grp.FirstOrDefault().HR_ABERTURA_CAIXA,
                               IdAberturaUsuario = grp.FirstOrDefault().CO_USUARIO_ABERT,
                               //AberturaUsuario = grp.FirstOrDefault().AplColAbertura,
                               AberturaValor = grp.FirstOrDefault().VR_ABERTURA_CAIXA,

                               FechamentoData = grp.FirstOrDefault().DT_FECHAMENTO_CAIXA,
                               FechamentoHora = grp.FirstOrDefault().HR_FECHAMENTO_CAIXA,
                               IdFechamentoUsuario = grp.FirstOrDefault().CO_USUARIO_FECHA,
                               //FechamentoUsuario = grp.FirstOrDefault().MatColFechamento,

                               VlAporte = grp.FirstOrDefault().VR_APORTE_CAIXA,
                               VlSangria = grp.FirstOrDefault().VR_SANGRIA_CAIXA,
                               VlDiferenca = grp.FirstOrDefault().VR_DIFERENCA_CAIXA,

                               VlRecebido = grp.Where(x => x.TP_OPER_CAIXA == "C").Sum(x => (x.VR_LIQ_DOC ?? 0)),
                               QtdeVlRecebido = grp.Where(x => x.TP_OPER_CAIXA == "C").Count(),
                               VlPago = grp.Where(x => x.TP_OPER_CAIXA == "D").Sum(x => (x.VR_LIQ_DOC ?? 0)),
                               QtdeVlPago = grp.Where(x => x.TP_OPER_CAIXA == "D").Count(),
                               Situacao = grp.FirstOrDefault().DT_FECHAMENTO_CAIXA == null ? "Aberto" : "Fechado"
                           }).OrderBy(p => p.DtMovimento).ToList();

                foreach (var item in lst)
                {
                    if (item.IdAberturaUsuario != null)
                    {
                        var func = (from usrFechamento in ctx.ADMUSUARIO
                                    join colFechamento in ctx.TB03_COLABOR on usrFechamento.CodUsuario equals colFechamento.CO_COL
                                    where usrFechamento.ideAdmUsuario == item.IdAberturaUsuario
                                    select new { colFechamento.CO_MAT_COL }).FirstOrDefault();

                        item.AberturaUsuario = func != null ? func.CO_MAT_COL : "";
                    }

                    if (item.IdFechamentoUsuario != null)
                    {
                        var func = (from usrFechamento in ctx.ADMUSUARIO
                                    join colFechamento in ctx.TB03_COLABOR on usrFechamento.CodUsuario equals colFechamento.CO_COL
                                    where usrFechamento.ideAdmUsuario == item.IdFechamentoUsuario
                                    select new { colFechamento.CO_MAT_COL }).FirstOrDefault();

                        item.FechamentoUsuario = func != null ? func.CO_MAT_COL : "";
                    }
                }                            

                var res = lst;

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                int cont = res.Count;
                decimal vlMedio = res.Sum(x => (x.VlResultado ?? 0)) / cont;
                this.lblResumo.Text = string.Format(lblResumo.Text, cont, vlMedio);

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper Report

        public class ResumoFinanceiro
        {
            public DateTime DtMovimento { get; set; }
            public string Caixa { get; set; }
            public string Colaborador { get; set; }

            public DateTime AberturaData { get; set; }
            public string AberturaHora { get; set; }
            public int? IdAberturaUsuario { get; set; }
            public string AberturaUsuario { get; set; }
            public decimal? AberturaValor { get; set; }

            public DateTime? FechamentoData { get; set; }
            public string FechamentoHora { get; set; }
            public int? IdFechamentoUsuario { get; set; }
            public string FechamentoUsuario { get; set; }

            public decimal? VlAporte { get; set; }
            public decimal? VlSangria { get; set; }
            public decimal? VlRecebido { get; set; }
            public int QtdeVlRecebido { get; set; }
            public decimal? VlPago { get; set; }
            public int QtdeVlPago { get; set; }
            public decimal? VlDiferenca { get; set; }
            public string Situacao { get; set; }

            public string DescDiferenca
            {
                get
                {
                    return this.VlDiferenca != null ? (this.VlDiferenca != 0) ? "Sim" : "Não" : "-";
                }
            }

            public decimal? VlResultado
            {
                get
                {
                    return ((AberturaValor ?? 0) + (VlAporte ?? 0) + (VlRecebido ?? 0)) - ((VlSangria ?? 0 + (VlPago ?? 0)));
                }
            }
        }

        #endregion

        private void lblStatus_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text == "Aberto")
                obj.ForeColor = Color.Red;
            else
                obj.ForeColor = Color.Black;
        }
    }
}
