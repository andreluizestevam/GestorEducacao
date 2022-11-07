//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes.PesquisaAvaliacao
{
    public partial class TituloAvaliacao : System.Web.UI.UserControl
    {
        int codTipo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.Url.ToString().ToLower().Contains("cadastro.aspx"))
            {
                codTipo = Convert.ToInt32(Session["codTipo"]);

                if (codTipo > 0)
                {
                    txtTipo.Text = TB73_TIPO_AVAL.RetornaPelaChavePrimaria(codTipo).NO_TIPO_AVAL;                    
                }

                lbl_status.Text = "";
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            codTipo = Convert.ToInt32(Session["codTipo"]);

            TB72_TIT_QUES_AVAL titulo = new TB72_TIT_QUES_AVAL();

            titulo.CO_TIPO_AVAL = codTipo;
            titulo.TB73_TIPO_AVAL = TB73_TIPO_AVAL.RetornaPelaChavePrimaria(codTipo);
            titulo.NO_TITU_AVAL = txtNTitulo.Text;

            TB72_TIT_QUES_AVAL.SaveOrUpdate(titulo);
        }
    }
}