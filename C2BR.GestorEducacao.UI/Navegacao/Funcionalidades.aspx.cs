//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Navegacao
{
    public partial class Funcionalidades : System.Web.UI.Page
    {
        #region Propriedades

        public string PanelTitle { get; set; }
        public string PanelTitleImage { get; set; }
        public bool IsList { get; set; }
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //André
                if (HttpContext.Current.Request.Url.AbsoluteUri.ToUpper().Contains("LOCALHOST"))
                {
                    //tbpesquisa.Visible = true;
                    
                }
                int idModuloPai = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.ModPai);

                ObjectQuery<ADMMODULO> admModulo = admModulo = RetornaModulos(idModuloPai);

                if (admModulo != null && admModulo.Count() > 0)
                {
                    var resultados = (from r in admModulo
                                      select new
                                      {
                                        r.ideAdmModulo, r.nomModulo, r.nomDescricao, r.nomItemMenu, r.numOrdemMenu, r.nomURLModulo, r.imgIcone,
                                        r.ADMMODULO2, PanelTitle = r.ADMMODULO2.nomModulo, IsList = r.ADMMODULO2.flaTipoItemSubMenu.Equals("LST")
                                      }).OrderBy(r => r.numOrdemMenu).ToList();

                    string fraseErro = HttpUtility.UrlEncode("Funcionalidade não Disponível ao usuário");
                    var resultado2 = from r in resultados
                                     select new
                                     {
                                       r.IsList, 
                                       r.PanelTitle, 
                                       r.ADMMODULO2, 
                                       r.ideAdmModulo, 
                                       r.nomModulo, 
                                       r.nomDescricao, 
                                       r.nomItemMenu,
                                       r.numOrdemMenu, 
                                       r.imgIcone, 
                                       Icon = "/Navegacao/Icones/" + r.imgIcone, 
                                       PanelTitleImage = "/Navegacao/Icones/" + r.ADMMODULO2.imgIcone,                                       
                                       nomURLModulo = (!String.IsNullOrEmpty(r.nomURLModulo)) ? String.Format("{0}?{1}={2}&moduloNome={3}&moduloId={4}", 
                                       r.nomURLModulo, QueryStrings.ModuloId, 
                                       r.ideAdmModulo, 
                                       r.nomModulo,
                                       r.ideAdmModulo) : ("/RedirecionaMensagem.aspx?moduloNome=" + r.nomModulo + "&nptored=true&msgType=Error&msg=" + fraseErro + ".")
                                     };
                    this.PanelTitle = resultado2.First().PanelTitle;
                    this.PanelTitleImage = resultado2.First().PanelTitleImage;
                    this.IsList = resultado2.First().IsList;

                    rptModuloItens.DataSource = resultado2;
                    rptModuloItens.DataBind();
                }
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Retorna o ObjectQuery de ADMMODULO de acordo com o Id do módulo pai
        /// </summary>
        /// <param name="ideModPai">Id do módulo pai</param>
        /// <returns></returns>
        protected ObjectQuery<ADMMODULO> RetornaModulos(int ideModPai)
        {
            try
            {
                if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

                return (ObjectQuery<ADMMODULO>)ADMMODULO.RetornaUsuarioModulos(LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, LoginAuxili.IDEADMUSUARIO, ideModPai).OrderBy(m => m.numOrdemMenu);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                return null;
            }
            
        }
        protected ObjectQuery<ADMMODULO> RetornaModulos(string nomemodulo)
        {
            try
            {
                if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

                return (ObjectQuery<ADMMODULO>)ADMMODULO.RetornaUsuarioModulos(nomemodulo);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                return null;
            }
        }
        #endregion        
    }
}