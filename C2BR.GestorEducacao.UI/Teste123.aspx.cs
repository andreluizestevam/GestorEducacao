using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

namespace C2BR.GestorEducacao.UI
{
    public partial class Teste123 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucEnderecoSN.FkEndereco = EnumAuxili.FkEnderecoSN.Usuario;
            ucEnderecoSN.IdFk = 1;
            ucEnderecoSN.IdEndereco = 2;
            ucEnderecoSN.Manutencao = EnumAuxili.TipoManutencao.Pesquisa;
        }
    }
}