//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: LISTA RESPONSÁVEIS
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
    public partial class ListarCEPsEndereco : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strLogra = Request.QueryString["strEndereco"] != null ? Request.QueryString["strEndereco"].ToString().Replace("*", " ") : "";

                var result = (from tb235 in TB235_CEP.RetornaTodosRegistros()
                              where tb235.NO_ENDER_CEP.Contains(strLogra)
                              select new
                              {
                                  tb235.CO_CEP, tb235.NO_ENDER_CEP,
                                  tb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA
                              }).OrderBy(r => r.NO_ENDER_CEP).ThenBy(r => r.DE_TIPO_LOGRA).ThenBy(r => r.CO_CEP).ToList();

                grdListarCEPsEndereco.DataSource = from r in result
                                                   select new
                                                   {
                                                       CO_CEP = string.Format("{0:00000-000}", r.CO_CEP),
                                                       r.DE_TIPO_LOGRA, r.NO_ENDER_CEP
                                                   };

                grdListarCEPsEndereco.DataBind();
            }
        }
        #endregion

        protected void grdListarCEPsEndereco_RowDataBound(object sender, GridViewRowEventArgs e)
        {
//--------> Criação do estilo das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
            }
        }
    }
}