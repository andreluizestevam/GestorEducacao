//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class ChatOnline : System.Web.UI.UserControl
    {
        public string bgColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int coSala { get; set; }
        public int coEmp { get; set; }
        public string titChat { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (bgColor == null) { bgColor = "#cef"; }
                if (Width == 0) { Width = 300; }
                if (Height == 0) { Height = 300; }
                if (coSala == 0) { coSala = 1; }
                if (coEmp == 0) { coEmp = 221; }
                if (titChat == null) { titChat = "CHAT ONLINE"; }

                grdMsg.Width = Width;
                grdMsg.Height = Height;

                CarregaGridMsg();
            }
        }

        public void CarregaGridMsg()
        {
            var res = (from msg in TB170_MENSAGEM.RetornaTodosRegistros()
                       join usu in ADMUSUARIO.RetornaTodosRegistros() on msg.ADMUSUARIO.ideAdmUsuario equals usu.ideAdmUsuario
                       where msg.TB161_SALA.CO_SALA == coSala
                       select new
                       {
                           NO_USU = usu.desLogin,
                           DE_MSG = msg.DE_MSG
                       });

            grdMsg.DataSource = res;
            grdMsg.DataBind();
        }
    }
}