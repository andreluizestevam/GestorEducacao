using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

namespace C2BR.GestorEducacao.UI.App_Masters
{
    public partial class PadraoGenericas : System.Web.UI.MasterPage
    {
        #region Propriedades

        RegistroLog registroLog = new RegistroLog();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
//------------> Faz o registro na tabela de log de acordo com a ação executada
                registroLog.RegistroLOG(null, RegistroLog.NENHUMA_ACAO);
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Define a mensagem que aparecerá na parte superior da tela.
        /// </summary>
        /// <param name="strMsgObrigatoria">Mensagem de campo obrigatório</param>
        /// <param name="strMsgGenerica">Mensagem genérica</param>
        public void DefineMensagem(string strMsgObrigatoria, string strMsgGenerica)
        {
            lblMensagCampoObrig.Text = strMsgObrigatoria;
            lblMensagGenerica.Text = strMsgGenerica;
        }
        #endregion
    }
}
