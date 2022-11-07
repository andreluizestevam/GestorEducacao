using System;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptPrevPorDtVencimento : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptPrevPorDtVencimento()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              int codEmp,
                              int codEmpRef,
                              DateTime dtIni,
                              DateTime dtFim,
                              string infos,
                              int coModu,
                              int coCur,
                              int coTur,
                              string NO_RELATORIO,
                              string strP_IC_SIT_DOC,
                              bool incluCance)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "RELATÓRIO DE INADIMPLÊNCIA*");

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                var lst = (from tb47 in ctx.TB47_CTA_RECEB
                           join tb296 in ctx.TB296_CAIXA_MOVIMENTO on tb47.NU_DOC equals tb296.NU_DOC_CAIXA into mcx
                           from tb296 in mcx.DefaultIfEmpty()
                           join tb156 in ctx.TB156_FormaPagamento on tb296.CO_SEQMOV_CAIXA equals tb156.CO_CAIXA_MOVIMENTO into fp
                           from tb156 in fp.DefaultIfEmpty()
                           join tb118 in ctx.TB118_TIPO_RECEB on tb156.CO_TIPO_REC equals tb118.CO_TIPO_REC into tpr
                           from tb118 in tpr.DefaultIfEmpty()
                           where tb47.TB25_EMPRESA.CO_EMP == codEmpRef
                           && (tb47.DT_VEN_DOC >= dtIni && tb47.DT_VEN_DOC <= dtFim)
                           && tb47.IC_SIT_DOC != "C"
                           && (coModu != 0 ? tb47.CO_MODU_CUR == coModu : 0 == 0)
                           && (coCur != 0 ? tb47.CO_CUR == coCur : 0 == 0)
                           && (coTur != 0 ? tb47.CO_TUR == coTur : 0 == 0)
                           && (strP_IC_SIT_DOC != "-1" ? tb47.IC_SIT_DOC == strP_IC_SIT_DOC : 0 == 0)
                           && (!incluCance && strP_IC_SIT_DOC != "C" ? tb47.IC_SIT_DOC != "C" : 0 == 0)
                           group new { tb47, tb296, tb156, tb118 } by tb47.DT_VEN_DOC into grp
                           select new PrevisaoPorDataVenc
                           {
                               DtVencimento = grp.Key,
                               VlNormal = grp.Sum(x => x.tb47.VR_PAR_DOC),
                               VlNegociado = 0,
                               VlDinheiro = grp.Where(x => x.tb118.DE_SIG_RECEB == "DE" || x.tb118.DE_SIG_RECEB == "TB" || x.tb47.FL_ORIGEM_PGTO == "B").Sum(x => x.tb47.VR_PAG),
                               VlCheque = grp.Where(x => x.tb118.DE_SIG_RECEB == "CH").Sum(x => x.tb47.VR_PAG),
                               VlCartao = grp.Where(x => x.tb118.DE_SIG_RECEB == "CC" || x.tb118.DE_SIG_RECEB == "CD").Sum(x => x.tb47.VR_PAG),
                               VlMultaJuros = (grp.Sum(x => x.tb47.VR_MUL_PAG) ?? 0) + (grp.Sum(x => x.tb47.VR_JUR_PAG) ?? 0),
                               VlVencidos = grp.Where(x => x.tb47.IC_SIT_DOC == "A" && grp.Key < DateTime.Today).Sum(x => x.tb47.VR_PAR_DOC),
                               VlVincendos = grp.Where(x => x.tb47.IC_SIT_DOC == "A" && grp.Key >= DateTime.Today).Sum(x => x.tb47.VR_PAR_DOC),
                           }).OrderBy(p => p.DtVencimento);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Helper Report

        public class PrevisaoPorDataVenc
        {
            public decimal? VlVencidos { get; set; }
            public DateTime DtVencimento { get; set; }
            public decimal? VlNormal { get; set; }
            public int VlNegociado { get; set; }
            public decimal? VlDinheiro { get; set; }
            public decimal? VlCheque { get; set; }
            public decimal? VlCartao { get; set; }
            public decimal? VlMultaJuros { get; set; }
            public decimal? VlVincendos { get; set; }

            public decimal PercRecebidos
            {
                get
                {
                    if (!VlNormal.HasValue || VlNormal == 0)
                        return 0;

                    return ((VlDinheiro ?? 0) + (VlCheque ?? 0) + (VlCartao ?? 0)) / VlNormal.Value;
                }
            }
        }

        #endregion
    }
}
