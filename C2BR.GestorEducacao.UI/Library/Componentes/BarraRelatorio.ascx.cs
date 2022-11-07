//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.DataClasses;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class BarraRelatorio : System.Web.UI.UserControl
    {
        #region Propriedades

        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }
        #endregion

        #region Eventos

        #region Eventos (Declaração)

        public delegate void OnActionHandler();
        public event OnActionHandler OnAction;
        #endregion

        #region Eventos da Página

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            if (OnAction != null)
                OnAction();            
        }      
        #endregion

        #endregion
    }
}