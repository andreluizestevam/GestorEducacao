using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptUnidadeEscolaNucleo : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptUnidadeEscolaNucleo()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codNucleo, string infos)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Ocorrencias

                var res = (from tb25 in ctx.TB25_EMPRESA
                           join nuc in ctx.TB_NUCLEO_INST on tb25.CO_NUCLEO equals nuc.CO_NUCLEO into nu
                           from nuc in nu.DefaultIfEmpty()
                           join bai in ctx.TB905_BAIRRO on tb25.CO_BAIRRO equals bai.CO_BAIRRO into ba
                           from bai in ba.DefaultIfEmpty()
                           where tb25.CO_NUCLEO == codNucleo
                           select new UnidadePorNucleo
                           {
                               Inep = tb25.NU_INEP,
                               Cnpj = tb25.CO_CPFCGC_EMP,
                               Sigla = tb25.sigla,
                               Nome = tb25.NO_FANTAS_EMP,
                               Nucleo = nuc.DE_NUCLEO,
                               Bairro = bai.NO_BAIRRO,
                               Telefone = tb25.CO_TEL1_EMP,
                               Diretor = "-",
                               DiretorNucleo = "-"
                           }).DistinctBy(x => x.Inep).OrderBy(x => x.Nome).ToList();

                if (res.Count() == 0)
                    return -1;

                #endregion

                // Adiciona os movimentos ao DataSource do Relatório
                bsReport.Clear();

                foreach (var m in res)
                    bsReport.Add(m);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class UnidadePorNucleo

        public class UnidadePorNucleo
        {
            public int? Inep { get; set; }
            public string StrInep
            {
                get { return (!Inep.HasValue ? "-" : Inep.ToString()); }
            }

            public string Cnpj { get; set; }
            public string StrCnpj
            {
                get { return (string.IsNullOrEmpty(Cnpj) ? "-" : Funcoes.Format(Cnpj, TipoFormat.CNPJ)); }
            }

            public string Sigla { get; set; }
            public string Nome { get; set; }
            public string Nucleo { get; set; }
            public string Bairro { get; set; }
            public string DiretorNucleo { get; set; }
            public string Diretor { get; set; }
            public string Telefone { get; set; }
            public string StrTelefone
            {
                get { return (string.IsNullOrEmpty(Telefone) ? "-" : Funcoes.Format(Telefone, TipoFormat.Telefone)); }
            }
        }

        #endregion

    }
}
