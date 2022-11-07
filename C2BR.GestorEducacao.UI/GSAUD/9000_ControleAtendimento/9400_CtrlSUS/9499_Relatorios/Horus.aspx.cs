using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9400_CtrlSUS._9499_Relatorios
{
    public partial class Horus : System.Web.UI.Page
    {
        public PadraoCadastros PadraoCorrente { get { return (PadraoCadastros)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Write("<script>window.open('http://portalsaude.saude.gov.br/index.php/o-ministerio/principal/leia-mais-o-ministerio/220-sctie-raiz/daf-raiz/ceaf-sctie/qualifarsus-raiz/horus/l2-horus/18713','_blank');</script>");

                ScriptManager.RegisterStartupScript(
                    this.Page,
                    this.GetType(),
                    "acao",
                    "BackToHome();",
                    true
                    );              
            }
        }
    }
}