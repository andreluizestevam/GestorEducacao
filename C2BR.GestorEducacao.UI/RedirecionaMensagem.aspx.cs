//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *****
// SUBMÓDULO: *****
// OBJETIVO: REDIRECIONAR MENSAGENS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 21/03/2013| André Nobre Vinagre        | Aumentei o tempo de redirecionamento para 
//           |                            | a funcionalidade de Atualização dos Portais.
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI
{
    public partial class RedirecionaMensagem : System.Web.UI.Page
    {
        #region Propriedades

        public string MensagemTipo { get { return QueryStrings.TipoMessagemRedirecionamento.QueryStringValor(); } }

        public enum TipoMessagemRedirecionamento
        {
            Sucess,
            Error
        }
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string script = "window.parent.location.href = '{0}';";

                if (!String.IsNullOrEmpty(QueryStrings.RedirecionaParaURL.QueryStringValor()))
                {
                    string url = String.Format("{0}?moduloNome={1}", QueryStrings.RedirecionaParaURL.QueryStringValor(), Request.QueryString["moduloNome"]);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", string.Format(script, url), true);
                    Response.Redirect(url);
                }
                else
                {
                    string url = Request.Url.AbsolutePath.Replace("RedirecionaMensagem.aspx", "Default.aspx");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", string.Format(script, url), true);
                    //Response.Redirect(Request.Url.AbsolutePath.Replace("RedirecionaMensagem.aspx", "Default.aspx"));
                }
            }
            else
            {
                lblMessage.Text = HttpContext.Current.Server.UrlDecode(QueryStrings.MessagemQueryString.QueryStringValor());

                //if (Request.QueryString.AllKeys.Contains(QueryStrings.NoPageToRedirect))
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "setTimeout(\"parent.window.location = '/'\", 2500);", true);
            }
        }
        #endregion        
    }
}
