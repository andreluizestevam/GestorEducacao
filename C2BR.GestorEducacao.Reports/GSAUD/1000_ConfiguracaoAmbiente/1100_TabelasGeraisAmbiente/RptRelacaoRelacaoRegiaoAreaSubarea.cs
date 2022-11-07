//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 05/03/14 | Débora Lohane              | Criação da funcionalidade para geração de relatorios(regiao, area, subarea)


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

namespace C2BR.GestorEducacao.Reports.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente
{
    public partial class RptRelacaoRelacaoRegiaoAreaSubarea : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoRelacaoRegiaoAreaSubarea()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(int regiao,
                                int area,
                                int subarea,
                                string ordenacao,
                                string infosRodape,
                                string parametros,
                                int coEmp)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infosRodape;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Query Relatório (Regiao, Area, Subarea)

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                List<RegiaoAreaSubarea> lst = new List<RegiaoAreaSubarea>();

                lst = (from tb906 in ctx.TB906_REGIAO
                       join tb907 in ctx.TB907_AREA on tb906.ID_REGIAO equals tb907.TB906_REGIAO.ID_REGIAO
                       join tb908 in ctx.TB908_SUBAREA on tb907.ID_AREA equals tb908.TB907_AREA.ID_AREA
                       where
                       ((regiao) != 0 ? tb906.ID_REGIAO == regiao : regiao == 0)
                       && ((area) != 0 ? tb907.ID_AREA == area : area == 0)
                       && ((subarea) != 0 ? tb908.ID_SUBAREA == subarea : subarea == 0)
                       select new RegiaoAreaSubarea
                       {

                           NomeRegiao = tb906.NM_REGIAO,
                           NomeArea = tb907.NM_AREA,
                           NomeSubarea = tb908.NM_SUBAREA,

                           SiglaRegiao = tb906.SIGLA,
                           SiglaArea = tb907.SIGLA,
                           SiglaSubarea = tb908.SIGLA

                       }).ToList();



                // Erro: não encontrou registros
                if (lst.Count == 0)
                    return -1;
                //Ordena a pesquisa conforme escolhido no relatório.
                bsReport.Clear();
                List<RegiaoAreaSubarea> lista = new List<RegiaoAreaSubarea>();
                switch (ordenacao)
                {
                    case "regiao":
                        lista = lst.OrderBy(r => r.NomeRegiao).ToList();
                        break;
                    case "area":
                        lista = lst.OrderBy(a => a.NomeArea).ToList();
                        break;
                    case "subarea":
                        lista = lst.OrderBy(sb => sb.NomeSubarea).ToList();
                        break;
                    default:
                        break;
                }
                foreach (RegiaoAreaSubarea list in lista)
                    bsReport.Add(list);

                return 1;

            }
            catch { return 0; }
        }

                #endregion
        #endregion
        #region Classe Regiao/Area/Subarea

        public class RegiaoAreaSubarea
        {

            public string NomeRegiao { get; set; }
            public string NomeArea { get; set; }
            public string NomeSubarea { get; set; }

            public string SiglaRegiao { get; set; }
            public string SiglaArea { get; set; }
            public string SiglaSubarea { get; set; }

            public string NomeSiglaRegiao { get {

                string nomeRegiao = this.NomeRegiao;
                string siglaRegiao = this.SiglaRegiao;
                string retorno;

                retorno = siglaRegiao +"-"+ nomeRegiao;
                return retorno;
            
            } }

            public string NomeSiglaArea { get {

                string nomeArea = this.NomeArea;
                string siglaArea = this.SiglaArea;
                string retorno;

                retorno = siglaArea + "-" + nomeArea;
                return retorno;
            } }

            public string NomeSiglaSubarea { get {

                string nomeSubarea = this.NomeSubarea;
                string siglaSubarea = this.SiglaSubarea;
                string retorno;

                retorno = siglaSubarea + "-" + nomeSubarea;
                return retorno;
            
            } }

        }

        #endregion

    }
}
