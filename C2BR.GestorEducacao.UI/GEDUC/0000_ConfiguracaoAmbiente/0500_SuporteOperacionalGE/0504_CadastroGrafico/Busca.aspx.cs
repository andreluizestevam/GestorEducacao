//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0504_CadastroGrafico
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }                

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_GRAFI_GERAL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TIPO_GRAFI",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_TITULO_GRAFI",
                HeaderText = "Título"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_STATUS_GRAFI",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb307 in TB307_GRAFI_GERAL.RetornaTodosRegistros()
                             where ddlTipoGrafi.SelectedValue != "T" ? tb307.CO_TIPO_GRAFI == ddlTipoGrafi.SelectedValue : ddlTipoGrafi.SelectedValue == "T"
                               && ddlStatus.SelectedValue != "T" ? tb307.CO_STATUS_GRAFI == ddlStatus.SelectedValue : ddlStatus.SelectedValue == "T"
                               && txtTitulGrafi.Text != "" ? tb307.NM_TITULO_GRAFI.Contains(txtTitulGrafi.Text) : txtTitulGrafi.Text == ""
                               select new 
                               {
                                   CO_TIPO_GRAFI = tb307.CO_TIPO_GRAFI == "C" ? "Coluna" : tb307.CO_TIPO_GRAFI == "P" ? "Pirâmide" : "Pizza",
                                   tb307.NM_TITULO_GRAFI, tb307.ID_GRAFI_GERAL, CO_STATUS_GRAFI = tb307.CO_STATUS_GRAFI == "A" ? "Ativa" : "Inativa"
                               }).OrderBy(b => b.CO_TIPO_GRAFI).ThenBy(b => b.NM_TITULO_GRAFI);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_GRAFI_GERAL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}