//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: COMO FAZER
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
    public partial class ComoFazer : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[SessoesHttp.IdModuloCorrente] != null)
                {
                    int idModuloCorrente = 0;
                    int.TryParse(Session[SessoesHttp.IdModuloCorrente].ToString(), out idModuloCorrente);

                    List<TBPROX_PASSOS> comoFazer = TBPROX_PASSOS.RetornaPeloIDeAdmModulo(idModuloCorrente).ToList();

                    grdComoFazer.DataSource = comoFazer;
                    grdComoFazer.DataBind();
                }
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Redireciona para a funcionalidade no sistema de acordo com o ID do módulo informado
        /// </summary>
        /// <param name="IdModulo">Id do módulo</param>
        static void RedirecionaFuncionalidade(int IdModulo)
        {
            ADMMODULO admModulo = (from lAdmModulo in ADMMODULO.RetornaTodosRegistros()
                                   where lAdmModulo.ideAdmModulo == IdModulo 
                                   select lAdmModulo).FirstOrDefault();

            HttpContext.Current.Session[Resources.SessoesHttp.IdModuloCorrente] = IdModulo;

            QueryStringAuxili.RedirecionaParaOperacao(QueryStrings.PaginaURLIFrame, (object)String.Format("{0}&ititle={1}", 
                                       admModulo.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModulo.nomModulo)));
        }
        #endregion

        protected void grdComoFazer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                TBPROX_PASSOS comoFazer = (TBPROX_PASSOS)e.Row.DataItem;
                
//------------> Criação do estilo das linhas da GRID
                e.Row.CssClass = comoFazer.CO_FLAG_LINK.ToLower().ToLower().Equals("s")
                                || comoFazer.CO_FLAG_REFER_VALID.ToLower().Equals("s") ? "activeLine" : "inactiveLine";

                if (e.Row.CssClass.Equals("activeLine"))
//----------------> Criação do ToolTip das linhas da GRID
                    e.Row.Attributes.Add("title", "Clique para ir ao próximo passo.");

                if (comoFazer.CO_FLAG_LINK.ToLower().ToLower().Equals("s") && !String.IsNullOrEmpty(comoFazer.DE_URL_EXTERNA))
                {                    
//----------------> Criação do link externo (para outro endereço web) das linhas da GRID
                    e.Row.Attributes.Add("onclick", String.Format("window.open('{0}')", comoFazer.DE_URL_EXTERNA));
                }
                else
                {
                    comoFazer.ADMMODULO1Reference.Load();
                    if (comoFazer.ADMMODULO1 != null)
                    {
//--------------------> Criação do link interno (para outra funcionalidade) das linhas da GRID
                        string strTarefaURl = String.Format("{0}?moduloNome={1}&", comoFazer.ADMMODULO1.nomURLModulo, Server.UrlEncode(comoFazer.ADMMODULO1.nomModulo));
                        e.Row.Attributes.Add("onclick", "javascript:openAsIframe('" + strTarefaURl + "')");
                    }
                }
            }
        }

        protected void grdComoFazer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idComoFazer = (int)((GridView)(e.CommandSource)).DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0];

            TBPROX_PASSOS comoFazer = (from lComoFazer in TBPROX_PASSOS.RetornaTodosRegistros().Include(typeof(ADMMODULO).Name)
                                       where lComoFazer.CO_PROXIPASSOS == idComoFazer && lComoFazer.ADMMODULO1.ideAdmModulo > 0
                                       select lComoFazer).FirstOrDefault();

            if (comoFazer != null)
                comoFazer.ADMMODULO1Reference.Load();

                if (comoFazer.ADMMODULO1 != null)
                {
                    Session[SessoesHttp.IdModuloCorrente] = comoFazer.ADMMODULO1.ideAdmModulo;

                    if (!String.IsNullOrEmpty(comoFazer.ADMMODULO1.nomURLModulo))
                        RedirecionaFuncionalidade(comoFazer.ADMMODULO1.ideAdmModulo);
                    else
                        Response.Redirect(Request.Url.AbsolutePath);
                }
        }        
    }
}