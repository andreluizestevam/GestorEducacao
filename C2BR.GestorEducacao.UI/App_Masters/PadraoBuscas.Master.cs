//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Collections;
using System.Data.Objects.DataClasses;
using System.Text;
using System.Collections.Specialized;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.App_Masters
{
    public partial class PadraoBuscas : System.Web.UI.MasterPage
    {
        #region Propriedades

        public string QueryStringIds { get; set; }

        public int RowIndex
        {
            get
            {
                int rowIndexSelec = -1;

                int.TryParse(HFSelectedRow.Value, out rowIndexSelec);

                return rowIndexSelec;
            }
        }

        public GridView GridBusca { get { return this.grdBusca; } }

        RegistroLog registroLog = new RegistroLog();
        #endregion

        #region Eventos

        #region Eventos (Declaração)

        public delegate void OnAcaoBuscaDefineGridViewHandler();
        public event OnAcaoBuscaDefineGridViewHandler OnAcaoBuscaDefineGridView;

        public delegate void OnDefineColunasGridViewHandler();
        public event OnDefineColunasGridViewHandler OnDefineColunasGridView;

        public delegate void OnDefineQueryStringIdsHandler();
        public event OnDefineQueryStringIdsHandler OnDefineQueryStringIds;

        public delegate void OnGridRowDataBoundHandler(object sender, GridViewRowEventArgs e);
        public event OnGridRowDataBoundHandler OnGridRowDataBound;

        #endregion        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                if (OnDefineColunasGridView != null && grdBusca.Columns.Count == 0)
                    OnDefineColunasGridView();

                registroLog.RegistroLOG(null, RegistroLog.NENHUMA_ACAO);
            }

            if (lblMensagCampoObrig.Text == "")
                DefineMensagem("", MensagensAjuda.MessagemBusca);
        }        

        #region Eventos da Grid

        protected void grdBusca_DataBound(object sender, EventArgs e)
        {
            GridViewRow grdViewRow = grdBusca.BottomPagerRow;

            if (grdViewRow != null)
            {
                DropDownList ddlPaginaLista = (DropDownList)grdViewRow.Cells[0].FindControl("ddlGrdPaginas");

                if (ddlPaginaLista != null)
                    for (int i = 0; i < grdBusca.PageCount; i++)
                    {
                        int numeroPagina = i + 1;
                        ListItem lstItem = new ListItem(numeroPagina.ToString());

                        if (i == grdBusca.PageIndex)
                            lstItem.Selected = true;

                        ddlPaginaLista.Items.Add(lstItem);
                    }
            }
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", String.Format("document.getElementById('{0}').value = {1}", HFSelectedRow.ClientID, e.Row.RowIndex.ToString()));

            if (OnGridRowDataBound != null)
                OnGridRowDataBound(sender, e);
        }                      

        protected void grdBusca_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (OnDefineQueryStringIds != null)
                OnDefineQueryStringIds();

            if ((RowIndex >= 0) && (RowIndex <= grdBusca.PageSize))
            {
                if (((GridView)sender).DataKeys.Count >= RowIndex)
                {
                    IOrderedDictionary ordDicDataKeyValues = ((GridView)sender).DataKeys[RowIndex].Values;

                    foreach (DictionaryEntry dicEntDataKey in ordDicDataKeyValues)
                        QueryStringIds = QueryStringIds.Replace(dicEntDataKey.Key.ToString(), dicEntDataKey.Value.ToString());

//----------------> Faz o registro na tabela de log de acordo com a ação executada
                    registroLog.RegistroLOG(null, RegistroLog.ACAO_EDICAO);

                    AuxiliPagina.RedirecionaParaPaginaCadastro(QueryStrings.OperacaoAlteracao, QueryStringIds);
                }
                else
                    EnviaMensagemErro("Não foi possível carregar item selecionado, entre em contato com o Suporte Técnico e informe dados da tela atual.");
            }
            else
                EnviaMensagemErro("Selecione um item antes de executar uma operação.");
        }

        protected void ddlGrdPaginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow grdViewRow = grdBusca.BottomPagerRow;

            if (grdViewRow != null)
            {
                DropDownList ddlListaPagina = (DropDownList)grdViewRow.Cells[0].FindControl("ddlGrdPaginas");
                BindGrdBusca(ddlListaPagina.SelectedIndex);
            }
        }

        protected void grdBusca_PageIndexChanging(object sender, GridViewPageEventArgs e) { BindGrdBusca(e.NewPageIndex); }  
        #endregion

        #endregion

        #region Métodos

        private void BindGrdBusca(int newPageIndex)
        {
            if (OnAcaoBuscaDefineGridView != null)
                OnAcaoBuscaDefineGridView();

            fldGrid.Visible = true;

            grdBusca.PageIndex = newPageIndex;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Configura as Query Strings para carregar as informações na tela de cadastro de acordo com os DataKeyNames configurados na Grid de Busca.
        /// </summary>
        /// <param name="lstQueryStringIds">Lista de KeyValuePair(Key = nome da QueryString , Value = DataKeyName)</param>  
        public void DefineQueryStringIdsFromDataKeyNames(List<KeyValuePair<string, string>> lstQueryStringIds)
        {
            StringBuilder strBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> queryStringId in lstQueryStringIds)
                strBuilder.AppendFormat("{0}={1}&", queryStringId.Key, queryStringId.Value);

            strBuilder.AppendFormat("{0}={1}&", "moduloNome", Request.QueryString["moduloNome"].ToString());

            QueryStringIds = strBuilder.ToString();
        }    

        /// <summary>
        /// Define a mensagem que aparecerá na parte superior da tela de busca.
        /// </summary>
        /// <param name="strMsgObrigatoria">Mensagem de campo obrigatório</param>
        /// <param name="strMsgGenerica">Mensagem genérica</param>
        public void DefineMensagem(string strMsgObrigatoria, string strMsgGenerica)
        {
            lblMensagCampoObrig.Text = strMsgObrigatoria;
            lblMensagGenerica.Text = strMsgGenerica;
            lblMensagParamPesquisa.Text = MensagensAjuda.MessagemParametroBusca;
            lblMensagGrid.Text = MensagensAjuda.MessagemResultadoBusca;
        }

        /// <summary>
        /// Faz a validação para saber se existe inconsistência e apresenta a mensagem de erro informada.
        /// </summary>
        /// <param name="mensagemErro">Mensagem de erro</param>
        public void EnviaMensagemErro(string mensagemErro)
        {
            Page.Validators.Add(new CustomValidator { IsValid = false, ErrorMessage = mensagemErro });
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "javascript:alert('" + mensagemErro + "');", true);
        }
        #endregion    

        protected void btnBusca_Click(object sender, EventArgs e)
        {
            if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            if (Page.IsValid)
            {
                registroLog.RegistroLOG(null, RegistroLog.ACAO_PESQUISA);
                BindGrdBusca(0);
            }
        }
    }
}
