using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Configuration;

namespace C2BR.GestorEducacao.UI
{
    public partial class temporario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationSettings.AppSettings.AllKeys.Where(f => f == "SistemaEmManutencao").Count() > 0)
            {
                Boolean manutencao = false;
                if (Boolean.TryParse(ConfigurationSettings.AppSettings["SistemaEmManutencao"].ToString(), out manutencao)
                    && !manutencao)
                    AuxiliPagina.RedirecionaParaPaginaDefault();
            }
        }
    }
}