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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5800_CtrlDiversosFinanceiro._5810_CtrlCheques
{
    public partial class RptRelacaoChequeEmitidoInstituicao : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacaoChequeEmitidoInstituicao()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int codEmpRef,
                               string strTipoCliente,
                               int codCliente,
                               DateTime dtInicio,
                               DateTime dtFim,
                               string strSituacao,
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

                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");
                #region Query Cheques/Unidade

                var lst = (from tb158 in ctx.tb158_cheques
                           where tb158.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strTipoCliente != "T" ? tb158.tp_cliente_cheque == strTipoCliente : 0 == 0)
                            && (codCliente != 0 ? (strTipoCliente != "T" ? strTipoCliente == "C" ? tb158.TB103_CLIENTE.CO_CLIENTE == codCliente : tb158.TB108_RESPONSAVEL.CO_RESP == codCliente : 0 == 0) : 0 == 0)
                            && tb158.TB25_EMPRESA.CO_EMP == codEmpRef && tb158.dt_emissao >= dtInicio && tb158.dt_emissao <= dtFim
                            && (strSituacao != "T" ? tb158.ic_sit == strSituacao : 0 == 0)
                           select new ChequesUnidade
                           {
                               Banco = tb158.TB29_BANCO.IDEBANCO,
                               Agencia = tb158.co_agencia,
                               Conta = tb158.nu_conta,
                               TipoCliente = tb158.tp_cliente_cheque == "C" ? "OUTR" : "RESP",
                               NomeCliente = tb158.TB108_RESPONSAVEL != null ? tb158.TB108_RESPONSAVEL.NO_RESP : tb158.TB103_CLIENTE.NO_FAN_CLI,
                               Valor = tb158.valor,
                               DataEmissao = tb158.dt_emissao,
                               Status = tb158.ic_sit == "A" ? "Em aberto" : tb158.ic_sit == "C" ? "Cancelado" : "Baixado",
                               DataVecto = tb158.dt_vencimento,
                               DataSituacao = tb158.dt_sit,
                               situacao = tb158.ic_sit,
                               NumeroCheque = tb158.nu_cheque,
                               NumeroDocto = tb158.nu_doc
                           }).OrderBy(p => p.NomeCliente).ThenBy(p => p.Banco);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta as contas no DataSource do Relatorio
                bsReport.Clear();
                foreach (ChequesUnidade at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Cheques Unidade do Relatorio

        public class ChequesUnidade
        {
            public string TipoCliente { get; set; }
            public string NomeCliente { get; set; }
            public string Banco { get; set; }
            public int Agencia { get; set; }
            public string Conta { get; set; }
            public DateTime DataEmissao { get; set; }
            public DateTime DataVecto { get; set; }
            public DateTime DataSituacao { get; set; }
            public string situacao { get; set; }
            public string NumeroCheque { get; set; }
            public string NumeroDocto { get; set; }
            public string Status { get; set; }
            public decimal Valor { get; set; }

            public string ContaDesc
            {
                get
                {
                    return this.Banco + " / " + this.Agencia.ToString() + " / " + this.Conta;
                }
            }
        }
        #endregion
    }
}
