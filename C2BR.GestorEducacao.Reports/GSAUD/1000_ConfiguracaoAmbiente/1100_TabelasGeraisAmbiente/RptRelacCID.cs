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
    public partial class RptRelacCID : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacCID()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              string coOrdPor,
                              int cidGeral,
                              string coSituacao
                )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Query Relatório CID

                // Instancia o contexto

                var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                           join tbs223 in TBS223_CID.RetornaTodosRegistros() on tb117.TBS223_CID.ID_CID_GRUPO equals tbs223.ID_CID_GRUPO into l1
                           from ls in l1.DefaultIfEmpty()
                        where (cidGeral != 0 ? ls.ID_CID_GRUPO == cidGeral : 0 == 0)
                                && (tb117.CO_SITUA_CID == coSituacao)
                       select new CID
                       {
                           CO_CID = tb117.CO_CID,
                           NO_CID = tb117.NO_CID,
                           //SG_CID = tb117.CO_SIGLA_CID,
                       }).ToList();



                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;
                //Ordena a pesquisa conforme escolhido no relatório.
                bsReport.Clear();
                switch (coOrdPor)
                {
                    case "N":
                        res = res.OrderBy(w => w.NO_CID).ToList();
                        break;
                    case "C":
                        res = res.OrderBy(w => w.CO_CID).ToList();
                        break;
                    default:
                        res = res.OrderBy(w => w.NO_CID).ToList();
                        break;
                }

                int count = 0;
                foreach (CID list in res)
                {
                    count++;
                    list.count = count;

                      //Muda a cor da coluna de acordo com a ordenação escolhida
                    switch (coOrdPor)
                    {
                        case "N":
                            xrTableCell7.ForeColor = Color.RoyalBlue;
                            break;
                        case "C":
                            xrTableCell6.ForeColor = Color.RoyalBlue;
                            break;
                    }

                    bsReport.Add(list);
                }

                return 1;

            }
            catch { return 0; }
        }

                #endregion
        #endregion

        public class CID
        {
            public string CO_CID { get; set; }
            public string NO_CID { get; set; }
            public int count { get; set; }
        }
    }
}
