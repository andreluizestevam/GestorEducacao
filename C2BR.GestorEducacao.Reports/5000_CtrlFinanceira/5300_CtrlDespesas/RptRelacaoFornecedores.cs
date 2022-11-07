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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas
{
    public partial class RptRelacaoFornecedores : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoFornecedores()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               string strCPF_CNPJ,
                               string strP_NO_FORNE,
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

                #region Query Fornecedores

                var lst = (from tb41 in ctx.TB41_FORNEC
                           where tb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == codInst
                           && (strCPF_CNPJ != "" ? tb41.CO_CPFCGC_FORN == strCPF_CNPJ : strCPF_CNPJ == "")
                           && (strP_NO_FORNE != "" ? tb41.NO_FAN_FOR.Contains(strP_NO_FORNE) : strP_NO_FORNE == "")
                           select new RelacFornec
                           {
                               Nome = tb41.NO_FAN_FOR,
                               CNPJCPF = tb41.CO_CPFCGC_FORN,
                               Telefone = tb41.CO_TEL1_FORN,
                               Fax = tb41.CO_FAX_FORN,
                               UF = tb41.CO_UF_FORN,
                               Cidade = tb41.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               TipoForne = tb41.TP_FORN
                           }).OrderBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os fornecedores no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelacFornec at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relação de Fornecedores do Relatorio

        public class RelacFornec
        {
            public string Nome { get; set; }
            public string CNPJCPF { get; set; }
            public string Telefone { get; set; }
            public string Fax { get; set; }
            public string UF { get; set; }
            public string Cidade { get; set; }
            public string TipoForne { get; set; }

            public string TelefoneDesc
            {
                get
                {
                    return this.Telefone != "" ? Funcoes.Format(this.Telefone, TipoFormat.Telefone) : "";
                }
            }

            public string FaxDesc
            {
                get
                {
                    return this.Fax != "" ? Funcoes.Format(this.Fax, TipoFormat.Telefone) : "";
                }
            }

            public string CNPJCPFDesc
            {
                get
                {
                    return this.TipoForne == "F" ? Funcoes.Format(this.CNPJCPF, TipoFormat.CPF) : Funcoes.Format(this.CNPJCPF, TipoFormat.CNPJ);
                }
            }
        }
        #endregion
    }
}
