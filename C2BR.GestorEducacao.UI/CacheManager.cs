using System.Web.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Web;
using System;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace C2BR.GestorEducacao.UI
{
    public class CacheManager : System.Web.UI.Page
    {
        public static GestorEducacao.BusinessEntities.MSSQL.GestorEntities GetContext()
        {
            return GestorEntities.CurrentContext;
        }

        public static void GetBAnco()
        {
            var usuario = ADMUSUARIO.RetornaPelaChavePrimaria(37);
            
            TBLOGIN novoLogin = new TBLOGIN();
            novoLogin.CO_EMP = 187;
            novoLogin.ORG_CODIGO_ORGAO = 1;
            novoLogin.USR_CODIGO = 37;
            novoLogin.LGN_DATA_ACESSO = DateTime.Now;
            novoLogin.LGN_IP_USUARIO = "127.0.0.1";

            TBLOGIN.SaveOrUpdate(novoLogin);
        }
    }
}