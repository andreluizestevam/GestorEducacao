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

namespace C2BR.GestorEducacao.Reports._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE
{
    public partial class RptLogAtividUsuario : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptLogAtividUsuario()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codInst,                               
                               int codEmpRef,
                               int codFuncio,
                               int codCol,
                               string strAcao,
                               DateTime dtInicio,
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
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmpRef);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador Parametrizada

                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");

                var lst = (from tb236 in ctx.TB236_LOG_ATIVIDADES
                           from tb03 in ctx.TB03_COLABOR
                           from admModul in ctx.ADMMODULO
                           from tb25 in ctx.TB25_EMPRESA
                           where tb236.ORG_CODIGO_ORGAO == codInst
                            && tb236.CO_COL == tb03.CO_COL
                            && tb236.IDEADMMODULO == admModul.ideAdmModulo
                            && tb236.CO_EMP_ATIVI_LOG == tb25.CO_EMP
                            && (codCol != 0 ? tb03.CO_COL == codCol : codCol == 0)
                            && (codFuncio != 0 ? admModul.ideAdmModulo == codFuncio : codFuncio == 0)
                            && (strAcao != "T" ? tb236.CO_ACAO_ATIVI_LOG == strAcao : strAcao == "T")
                            && tb236.DT_ATIVI_LOG >= dtInicio && tb236.DT_ATIVI_LOG <= dtFim
                           select new LogAtividUsuario
                           {
                               Matricula = tb03.CO_MAT_COL,
                               Nome = tb03.NO_COL,
                               DataAcesso = tb236.DT_ATIVI_LOG,
                               Funcionalidade = admModul.nomModulo,
                               CodigoAcao = tb236.CO_ACAO_ATIVI_LOG,
                               Tabela = tb236.CO_TABEL_ATIVI_LOG,
                               Unidade = tb25.NO_FANTAS_EMP,
                               NumAcesso = tb236.NR_ACESS_ATIVI_LOG,
                               IP = tb236.NR_IP_ACESS_ATIVI_LOG
                           }).OrderBy( p => p.Unidade ).ThenBy(p => p.DataAcesso).ThenBy(p => p.Funcionalidade).ThenBy(p => p.Nome);


                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (LogAtividUsuario at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador Parametrizado do Relatorio

        public class LogAtividUsuario
        {
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public DateTime DataAcesso { get; set; }
            public string Funcionalidade { get; set; }
            public string CodigoAcao { get; set; }
            public string Tabela { get; set; }
            public string Unidade { get; set; }
            public int NumAcesso { get; set; }
            public string IP { get; set; }

            public string MatriculaDesc
            {
                get
                {
                    if (this.Nome == null)
                        return "-";
                    else
                    {
                        return this.Matricula.Insert(5, "-").Insert(2, ".") + " (" + this.Nome + ")";
                    }
                }
            }

            public string DescAcao
            {
                get
                {
                    if (this.CodigoAcao == "G")
                    {
                        return "Gravação";
                    }
                    else if (this.CodigoAcao == "E")
                    {
                        return "Alteração";
                    }
                    else if (this.CodigoAcao == "D")
                    {
                        return "Exclusão";
                    }
                    else if (this.CodigoAcao == "R")
                    {
                        return "Relatório";
                    }
                    else if (this.CodigoAcao == "P")
                    {
                        return "Consulta";
                    }
                    else
                    {
                        return "Sem Ação";
                    }
                }
            }
        }
        #endregion
    }
}
