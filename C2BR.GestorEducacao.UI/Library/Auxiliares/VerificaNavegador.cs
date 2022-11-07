using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class VerificaNavegador
    {
            public static string RetornaOrigemCadastro()
            {
                string VerificaNav;
                HttpContext Mobile = HttpContext.Current;
                if (Mobile.Request.Browser.IsMobileDevice)
                {
                    return VerificaNav = "M";
                }
                else
                {
                    return VerificaNav = "W";
                }
            }
        }
    }
