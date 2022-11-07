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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5900_TabelasGenerCtrlFinaceira
{
    public partial class RptRelacCtasUnidade : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacCtasUnidade()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int codEmpRef,
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

                #region Query Contas/Unidade

                var lst = (from tb225 in ctx.TB225_CONTAS_UNIDADE
                           from tb25 in ctx.TB25_EMPRESA
                           where tb225.TB224_CONTA_CORRENTE.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                            && (codEmpRef != 0 ? tb225.CO_EMP == codEmpRef : codEmpRef == 0)
                            && tb25.CO_EMP == tb225.CO_EMP
                           select new ContasUnidade
                           {
                               Banco = tb225.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.DESBANCO,
                               Agencia = tb225.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA,
                               Conta = tb225.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + "-" + tb225.TB224_CONTA_CORRENTE.CO_DIG_CONTA,
                               Gerente = tb225.TB224_CONTA_CORRENTE.NO_GER_CTA,
                               TelefoneGerente = tb225.TB224_CONTA_CORRENTE.NU_TEL_GER_CTA,
                               EmiteBoleto = tb225.TB224_CONTA_CORRENTE.FLAG_EMITE_BOLETO_BANC,
                               DataAbertura = tb225.TB224_CONTA_CORRENTE.DT_ABERT_CTA,
                               Status = tb225.TB224_CONTA_CORRENTE.CO_STATUS,
                               Unidade = tb25.NO_FANTAS_EMP
                           }).OrderBy(p => p.Unidade).ThenBy(p => p.Banco);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta as contas no DataSource do Relatorio
                bsReport.Clear();
                foreach (ContasUnidade at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Contas Unidade do Relatorio

        public class ContasUnidade
        {
            public string Banco { get; set; }
            public int Agencia { get; set; }
            public DateTime DataAbertura { get; set; }
            public string Conta { get; set; }
            public string Gerente { get; set; }
            public string TelefoneGerente { get; set; }
            public string EmiteBoleto { get; set; }
            public string Status { get; set; }
            public string Unidade { get; set; }

            public string TelefoneDesc
            {
                get
                {
                    if (string.IsNullOrEmpty(this.TelefoneGerente))
                        return "-";

                    string pattern = @"(\d{2})(\d{4})(\d{4})";
                    return Regex.Replace(this.TelefoneGerente, pattern, "($1) $2-$3");
                }
            }

            public string StatusDesc
            {
                get
                {
                    if (this.Status == "A")
                        return "Ativa";
                    else
                        return "Inativa";
                }
            }

            public string EmiteBoletoDesc
            {
                get
                {
                    if (this.EmiteBoleto == "S")
                        return "Sim";
                    else
                        return "Não";
                }
            }
        }
        #endregion
    }
}
