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
    public partial class Buscas : System.Web.UI.MasterPage
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
                registroLog.RegistroLOG(null, RegistroLog.NENHUMA_ACAO);
            }

            if (lblMensagCampoObrig.Text == "")
                DefineMensagem("", MensagensAjuda.MessagemBusca);
        }       

        #endregion

        #region Métodos

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
            lblMensagGrid.Text = "Marque os itens que deseja inserir no arquivo de remessa.";
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

    }
}
