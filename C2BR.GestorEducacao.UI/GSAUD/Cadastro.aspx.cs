using C2BR.GestorEducacao.BusinessEntities.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace C2BR.GestorEducacao.UI.GSAUD
{
    public partial class Cadastro1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            plsql.Text = "SELECT table_catalog, table_schema, table_name, table_type FROM information_schema.tables";
        }

        protected void btnsql_Click(object sender, EventArgs e)
        {
            SQLDirectAcess acesso = new SQLDirectAcess();
            if (plsql.Text != "")
            {
                if (plsql.Text.ToUpper().Contains("SELECT"))
                {
                    GridView1.DataSource = acesso.retornacolunas(plsql.Text);
                    GridView1.DataBind();
                }
                else
                {
                    acesso.InsereAltera(plsql.Text);
                }
            }
        }
    }
}