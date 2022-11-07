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
    public partial class TipoAvaliacao : System.Web.UI.UserControl
    {
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTipo.Text) || String.IsNullOrEmpty(txtObjetivo.Text))
                lbl_status.Text = "Tipo de Avaliação e Objetivo são requeridos";
            else
            {
                TB73_TIPO_AVAL tp = new TB73_TIPO_AVAL();

                tp.NO_TIPO_AVAL = txtTipo.Text.Replace(",","");
                tp.DE_OBJE_AVAL = txtObjetivo.Text.Replace(",", "");
                tp.DE_OBSE_AVAL = txtObs.Text.Replace(",", "");
                tp.CO_ESTI_AVAL = "A";

                int result = TB73_TIPO_AVAL.SaveOrUpdate(tp).CO_TIPO_AVAL;

                if (result > 0)
                    lbl_status.Text = "Incluido com Sucesso";
                else
                    lbl_status.Text = "Erro";
            }
        }
    }
}