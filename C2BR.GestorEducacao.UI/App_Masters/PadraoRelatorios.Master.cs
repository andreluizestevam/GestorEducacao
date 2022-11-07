//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.DataClasses;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Reflection;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.App_Masters
{
    public partial class PadraoRelatorios : System.Web.UI.MasterPage
    {
        #region Propriedades

        RegistroLog registroLog = new RegistroLog();
        #endregion

        #region Eventos (Declaração)

        public delegate void OnAcaoGeraRelatorioHandler();
        public event OnAcaoGeraRelatorioHandler OnAcaoGeraRelatorio;
        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BarraRelatorioRef.OnAction += new BarraRelatorio.OnActionHandler(BarraRelatorioRef_OnAction);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                registroLog.RegistroLOG(null, RegistroLog.NENHUMA_ACAO);

            DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemRelatorio);

//--------> Faz a validação para saber se o evento é de exibição do relatório gerado.
            if (Session["ApresentaRelatorio"] != null)
                if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                {
                    Session.Remove("ApresentaRelatorio");
                    AuxiliPagina.AbreNovaJanela(this.content.Page, Session["URLRelatorio"].ToString());
//----------------> Limpa a var de sessão com o url do relatório.
                    Session.Remove("URLRelatorio");
                    PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    this.Request.QueryString.Remove("ApresentaRelatorio");
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                }
        }

        void BarraRelatorioRef_OnAction()
        {
            if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            registroLog.RegistroLOG(null, RegistroLog.ACAO_RELATORIO);
            if (OnAcaoGeraRelatorio != null)
                OnAcaoGeraRelatorio();
        } 
        #endregion

        #region Métodos

        /// <summary>
        /// Define a mensagem que aparecerá na parte superior da tela do relatório.
        /// </summary>
        /// <param name="strMsgObrigatoria">Mensagem de campo obrigatório</param>
        /// <param name="strMsgGenerica">Mensagem genérica</param>
        public void DefineMensagem(string strMsgObrigatoria, string strMsgGenerica)
        {
            lblMensagCampoObrig.Text = strMsgObrigatoria.ToString();
            lblMensagGenerica.Text = strMsgGenerica.ToString();
        }
        #endregion               
    }
}
