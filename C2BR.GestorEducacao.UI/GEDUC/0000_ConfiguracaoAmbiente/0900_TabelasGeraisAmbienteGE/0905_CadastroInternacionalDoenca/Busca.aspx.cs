//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRO INTERNACIONAL DE DOENÇAS.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0905_CadastroInternacionalDoenca
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaCIDGeral();
            }
        }


        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_CID" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CID",
                HeaderText = "CID"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CID",
                HeaderText = "Doença"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOME_GERAL",
                HeaderText = "CID GERAL"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int cidGEral = (!string.IsNullOrEmpty(ddlCIDGeral.SelectedValue) ? int.Parse(ddlCIDGeral.SelectedValue) : 0);
            var resultado = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                             join tbs223 in TBS223_CID.RetornaTodosRegistros() on tb117.TBS223_CID.ID_CID_GRUPO equals tbs223.ID_CID_GRUPO into l1
                             from ls in l1.DefaultIfEmpty()
                             where (txtDoencaCID.Text != "" ? tb117.NO_CID.Contains(txtDoencaCID.Text) : 0 == 0)
                             && (txtCID.Text != "" ? tb117.CO_CID.Contains(txtCID.Text) : 0 == 0)
                             && (cidGEral != 0 ? ls.ID_CID_GRUPO == cidGEral : 0 == 0)
                             && (tb117.CO_SITUA_CID == ddlSituacao.SelectedValue)
                             select new
                             {
                                 tb117.IDE_CID,
                                 tb117.CO_CID,
                                 tb117.NO_CID,
                                 NOME_GERAL = ls.NO_CID_GRUPO,
                             }).OrderBy(c => c.CO_CID);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_CID"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        /// <summary>
        /// Carrega todos os CID Gerais
        /// </summary>
        private void carregaCIDGeral()
        {
            var res = (from tbs223 in TBS223_CID.RetornaTodosRegistros()
                       where tbs223.CO_SITUA_CID_GRUPO == "A"
                       select new
                       {
                           nomeCID = tbs223.NO_CID_GRUPO,
                           idCID = tbs223.ID_CID_GRUPO,
                       }).ToList();

            if (res != null)
            {
                ddlCIDGeral.DataTextField = "nomeCID";
                ddlCIDGeral.DataValueField = "idCID";
                ddlCIDGeral.DataSource = res;
                ddlCIDGeral.DataBind();
            }
            ddlCIDGeral.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion
    }
}
