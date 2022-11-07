//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: BREADCRUMB
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Components
{
    public partial class BreadCrumb : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            int moduloId = 0;
            if (Request.QueryString["moduloId"] != null)
            {
                if (int.TryParse(Request.QueryString["moduloId"].ToString(), out moduloId))
                    CriaBreadCrumb(moduloId);    
            }            
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Cria o breadCrumb de acordo com o ID do módulo passado.
        /// </summary>
        /// <param name="moduloId">Id do módulo</param>
        private void CriaBreadCrumb(int moduloId)
        {
            Session[SessoesHttp.IdModuloCorrente] = moduloId;

            var resultado  = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                              where admModulo.ideAdmModulo == moduloId
                              select new { ModuloPai = admModulo.ADMMODULO2, ModuloVo = admModulo.ADMMODULO2.ADMMODULO2 }).FirstOrDefault();

            lblTextBreadCrumb.Text = String.Format("{0} » {1}", resultado.ModuloVo.nomModulo, resultado.ModuloPai.nomModulo);
        }
        #endregion
    }
}