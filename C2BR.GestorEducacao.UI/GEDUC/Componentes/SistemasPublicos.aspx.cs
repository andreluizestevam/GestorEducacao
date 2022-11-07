//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: SISTEMAS PÚBLICOS
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
using Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Security;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class SistemasPublicos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            grdSistemasPublicos.DataSource = (from tb298 in TB298_CONEXAO_WEB.RetornaTodosRegistros()
                                              where tb298.TP_CONEX_WEB == "S" && tb298.CO_STAT_CONEX_WEB == "A"
                                              select new
                                              {
                                                  tb298.ID_CONEX_WEB, tb298.CO_ORGAO_CONEX_WEB, tb298.NO_TITULO_CONEX_WEB,
                                                  tb298.DE_OBJETO_CONEX_WEB, tb298.DE_URL_CONEX_WEB
                                              }).ToList().OrderBy( c => c.NO_TITULO_CONEX_WEB );

             grdSistemasPublicos.DataBind();
        }
        #endregion

        protected void grdSistemasPublicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
//--------> Criação do link externo (para outro endereço web) das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("title", DataBinder.Eval(e.Row.DataItem, "DE_OBJETO_CONEX_WEB").ToString());
                e.Row.Attributes.Add("onclick", String.Format("window.open('{0}')", DataBinder.Eval(e.Row.DataItem, "DE_URL_CONEX_WEB").ToString()));
            }
        }
    }
}