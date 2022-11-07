using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;


namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria
{
    public partial class RptExtratoMovimentoCaixa : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region cab

        public RptExtratoMovimentoCaixa()
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
                              string operacao,
                              int codResponsavel,
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
                           join tb03 in ctx.TB03_COLABOR on tb296.TB295_CAIXA.CO_COLABOR_CAIXA equals tb03.CO_COL
                           join tb07 in ctx.TB07_ALUNO on tb296.CO_ALU equals tb07.CO_ALU
                           where tb296.TB295_CAIXA.CO_EMP == codEmpRef
                           && (tb296.TB295_CAIXA.DT_MOVIMENTO >= dtIni && tb296.TB295_CAIXA.DT_MOVIMENTO <= dtFim)
                           && (codCaixa != 0 ? tb296.TB295_CAIXA.CO_CAIXA == codCaixa : codCaixa == 0)
                           && (codCol != 0 ? tb296.TB295_CAIXA.CO_COLABOR_CAIXA == codCol : codCol == 0)
                           select new ResumoFinanceiro
                           {
                               DtMovimento = tb296.TB295_CAIXA.DT_MOVIMENTO,
                               AberturaHora = tb296.TB295_CAIXA.HR_ABERTURA_CAIXA,
                               TipoOperacao = tb296.TP_OPER_CAIXA == "C" ? "REC" : "PAG",
                               //TipoOperacao = tb296.TP_OPER_CAIXA,
                               Colaborador = tb03.CO_MAT_COL,
                               VlRecebido = tb296.TP_OPER_CAIXA == "C" ? tb296.VR_LIQ_DOC : 0,
                               VlPago = tb296.TP_OPER_CAIXA == "D" ? tb296.VR_LIQ_DOC : 0,
                               Documento = tb296.NU_DOC_CAIXA,
                               Sequencial = tb296.CO_SEQMOV_CAIXA,
                              
                           }).OrderBy(p => p.DtMovimento).ToList();

                foreach (var item in lst)
                {
                    if (item.TipoOperacao == "REC")
                    {
                        var tb47 = (from iTb47 in ctx.TB47_CTA_RECEB
                                    join tb07 in ctx.TB07_ALUNO on iTb47.CO_ALU equals tb07.CO_ALU                                    
                                    where iTb47.NU_DOC == item.Documento
                                    select new { tb07.NU_NIRE, iTb47.TB108_RESPONSAVEL.NO_RESP }).FirstOrDefault();
                                    
                        if (tb47 != null)
                        {
                            item.AlunNire = tb47.NU_NIRE;
                            item.Responsavel = tb47.NO_RESP;
                        }
                    }
                    else
                    {
                        var tb38 = (from iTb38 in ctx.TB38_CTA_PAGAR
                                    where iTb38.NU_DOC == item.Documento
                                    select new { iTb38.TB41_FORNEC.CO_FORN, iTb38.TB41_FORNEC.NO_FAN_FOR }).FirstOrDefault();
                        
                        if (tb38 != null)
                        {
                            item.AlunNire = tb38.CO_FORN;
                            item.Responsavel = tb38.NO_FAN_FOR;
                        }
                    }
                }

                var res = lst;

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                //int cont = res.Count;
                //decimal vlMedio = res.Sum(x => (x.VlResultado ?? 0)) / cont;
                //this.lblResumo.Text = string.Format(lblResumo.Text, cont, vlMedio);

                foreach (var at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion


        //===

        #region Class Helper Report

        public class ResumoFinanceiro
        {
            public DateTime DtMovimento { get; set; }
            public string Documento { get; set; }
            public string Caixa { get; set; }
            public string Colaborador { get; set; }
            public string TipoOperacao { get; set; }
            public int? AlunNire { get; set; }
            public int Sequencial { get; set; }
            public string Responsavel { get; set; }
            public DateTime AberturaData { get; set; }
            public string AberturaHora { get; set; }
            public decimal? VlRecebido { get; set; }
            public decimal? VlPago { get; set; }

            public string AlunNireDec
            {
                get
                {
                    return this.AlunNire.ToString().PadLeft(9, '0').Insert(1, ".").Insert(5, ".").Insert(9, "-");
                }
            }
        }
        #endregion
    }
}
