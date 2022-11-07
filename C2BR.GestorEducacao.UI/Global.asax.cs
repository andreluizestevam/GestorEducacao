//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using System.IO;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI
{
    public class Global : System.Web.HttpApplication
    {
        private void RecoverFromWebResourceError()
        {
            Type myType = typeof(System.Web.Handlers.AssemblyResourceLoader);

            FieldInfo handlerExistsField = myType.GetField("_handlerExists", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo handlerExistenceCheckedField = myType.GetField("_handlerExistenceChecked", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if ((((Boolean)handlerExistsField.GetValue(null)) == false) && (((Boolean)handlerExistenceCheckedField.GetValue(null)) == true))
                handlerExistenceCheckedField.SetValue(null, false);

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            RecoverFromWebResourceError();
        }


        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //CacheManager.GetBAnco();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.UserAgent.IndexOf("MSIE") > 0 && (Request.UserAgent.IndexOf("MSIE 5") > 0 || Request.UserAgent.IndexOf("MSIE 6") > 0 || Request.UserAgent.IndexOf("MSIE 7") > 0))
                Server.Transfer("BrowserDesatualizado.aspx");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            string strPastaRelatorio;

//--------> Faz a exclusão da pasta temporária de relatórios utilizados
            strPastaRelatorio = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + Session.SessionID.ToString() + "\\";
            if (Directory.Exists(@strPastaRelatorio))
                Directory.Delete(@strPastaRelatorio, true);

//--------> Faz a exclusão da pasta temporária de documentos utilizados
            strPastaRelatorio = HttpRuntime.AppDomainAppPath + "TMP_Documentos\\" + Session.SessionID.ToString() + "\\";
            if (Directory.Exists(@strPastaRelatorio))
                Directory.Delete(@strPastaRelatorio, true);
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}