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

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5400_CtrlReceitaDespesaFixa
{
    public partial class RptRelacaoClienteOrdenaDespesa : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoClienteOrdenaDespesa()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,
                               int codEmp,
                               int coEmpRef,
                               string strCPF_CNPJ,
                               string strP_NO_CLI,
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

                var lst = (from tb103 in ctx.TB103_CLIENTE
                           where (strCPF_CNPJ != "" ? tb103.CO_CPFCGC_CLI == strCPF_CNPJ : strCPF_CNPJ == "")
                           && (strP_NO_CLI != "" ? tb103.NO_FAN_CLI.Contains(strP_NO_CLI) : strP_NO_CLI == "")
                           select new RelacCliente
                           {
                               Nome = tb103.NO_FAN_CLI,
                               CNPJCPF = tb103.CO_CPFCGC_CLI,
                               Telefone = tb103.CO_TEL1_CLI,
                               Fax = tb103.CO_FAX_CLI,
                               UF = tb103.CO_UF_CLI,
                               Cidade = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               TipoForne = tb103.TP_CLIENTE
                           }).OrderBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os fornecedores no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelacCliente at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Relação de Clientes do Relatorio

        public class RelacCliente
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
