//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: SUPORTE OPERACIONAL DA BASE DE DADOS
// OBJETIVO: RESTAURAR BASE DE DADOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Management.Smo;
using C2BR.GestorEducacao.DatabaseManagement;
using Resources;
using System.Configuration;
using System.Data.SqlClient;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Collections.Specialized;
using System.Collections;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0502_RestaurarBaseDados
{
    public partial class RestaurarBase : System.Web.UI.Page
    {
        public PadraoGenericas CurrentPadraoBuscas { get { return (App_Masters.PadraoGenericas)Master; } }

        #region Propriedades

        public int RowIndex
        {
            get
            {
                int indexLinhaSelec = -1;

                int.TryParse(hfSelectedRow.Value, out indexLinhaSelec);

                return indexLinhaSelec;
            }
        } 
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentPadraoBuscas.DefineMensagem("", "Selecione um item da tabela abaixo e clique no botão 'Restaurar Base de Dados', <br /> ou simplesmente dê um 'Duplo clique' no item desejado na tabela para efetuar a Restauração dos Dados.");

                DataView dataView  = SQLServer.GetBackupSetHistory(ConfigurationManager.AppSettings.Get(AppSettings.DatabaseName), ConfigurationManager.AppSettings.Get(AppSettings.BackupSetName),
                                                                            SQLServer.GetSQLServer(new SqlConnection(ConfigurationManager.AppSettings.Get(AppSettings.BackupConnectionString)))).DefaultView;
                dataView.Sort = "BackupFinishDate DESC";
                grdBackupsRBD.DataSource = dataView;
                grdBackupsRBD.DataBind();
            }
        }
        #endregion

        protected void grdBackupsRBD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (RowIndex >= 0 && RowIndex <= grdBackupsRBD.PageSize)
            {
                if (((GridView)sender).DataKeys.Count >= RowIndex)
                {
                    IOrderedDictionary dataKeyValues = ((GridView)sender).DataKeys[RowIndex].Values;

                    foreach (DictionaryEntry dataKey in dataKeyValues)
                    {
                        if (dataKey.Key.ToString().Equals("ID"))
                        {
                            SQLServer sqlServer = new SQLServer(ConfigurationManager.AppSettings.Get(AppSettings.DatabaseName),
                                                    ConfigurationManager.AppSettings.Get(AppSettings.BackupConnectionString));
                            try
                            {
                                sqlServer.Restore((int)dataKey.Value);
                            }
                            catch (SqlException sqlException)
                            {
                                AuxiliPagina.RedirecionaParaPaginaErro(sqlException.Message, Request.Url.AbsoluteUri);
                            }
                        }
                    }
                }
                else
                    AuxiliPagina.EnvioMensagemErro(this, "Nenhuma DataKey encontrada, entre em contato com o suporte e informe o nome da tela atual.");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this, "Selecione um item antes de executar uma operação.");
        }        

        protected void grdBackupsRBD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", String.Format("document.getElementById('{0}').value = {1}", hfSelectedRow.ClientID, e.Row.RowIndex.ToString()));
        }               
    }
}
