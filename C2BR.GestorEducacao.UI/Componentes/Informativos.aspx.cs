//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: INFORMATIVOS
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Services;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class Informativos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<TB138_INFORMATIVOS> tb138 = (from iTb138 in TB138_INFORMATIVOS.RetornaTodosRegistros()
                                                  where (iTb138.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP || iTb138.TB25_EMPRESA == null)
                                                  && iTb138.DT_INICI_PUBLIC <= DateTime.Now && iTb138.DT_FINAL_PUBLIC >= DateTime.Now
                                                  select iTb138).ToList();

                grdInformativos.DataSource = tb138;
                grdInformativos.DataBind();
            }
        }              
        #endregion

        #region Métodos

        /// <summary>
        /// Faz o redirecionamento para a funcionalidade do sistema de acordo com o ID do módulo informado.
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

        protected void grdInformativos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                TB138_INFORMATIVOS tb138 = (TB138_INFORMATIVOS)e.Row.DataItem;

//------------> Criação do estilo das linhas da GRID
                //e.Row.CssClass = tb138.TP_TITUL_URL.ToLower().ToLower().Equals("e") || tb138.TP_TITUL_URL.ToLower().ToLower().Equals("i") ? "activeLine" : "inactiveLine";
                
//------------> Criação do ToolTip das linhas da GRID
                e.Row.Attributes.Add("title", tb138.DE_OBS_INFOR);

                if (tb138.TP_TITUL_URL.ToLower().ToLower().Equals("e") && !String.IsNullOrEmpty(tb138.CO_URL_EXT))
                {
//----------------> Criação do link externo (para outro endereço web) das linhas da GRID
                    e.Row.Attributes.Add("onclick", String.Format("window.open('{0}')", tb138.CO_URL_EXT));
                }
                else if (tb138.TP_TITUL_URL.ToLower().Equals("i"))
                {
                    ADMMODULO admModulo = ADMMODULO.RetornaPelaChavePrimaria((int)tb138.IDEADMMODULO);
//----------------> Criação do link interno (para outra funcionalidade) das linhas da GRID
                    string strURL = String.Format("{0}?moduloNome={1}&", admModulo.nomURLModulo, Server.UrlEncode(admModulo.nomModulo));
                    e.Row.Attributes.Add("onclick", "javascript:openAsIframe('" + strURL + "')");
                }
            }
        }

        protected void grdInformativos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idInfor = (int)((GridView)(e.CommandSource)).DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0];

            TB138_INFORMATIVOS tb138 = (from iTb138 in TB138_INFORMATIVOS.RetornaTodosRegistros()
                                       where iTb138.ID_INFOR == idInfor && iTb138.TP_TITUL_URL == "I"
                                       select iTb138).FirstOrDefault();

            if (tb138 != null)
            {
                ADMMODULO admModulo = ADMMODULO.RetornaPelaChavePrimaria((int)tb138.IDEADMMODULO);

                if (admModulo != null)
                {
                    Session[SessoesHttp.IdModuloCorrente] = admModulo.ideAdmModulo;

                    if (!String.IsNullOrEmpty(admModulo.nomURLModulo))
                        RedirecionaFuncionalidade(admModulo.ideAdmModulo);
                    else
                        Response.Redirect(Request.Url.AbsolutePath);
                }
            }            
        }
    }
}