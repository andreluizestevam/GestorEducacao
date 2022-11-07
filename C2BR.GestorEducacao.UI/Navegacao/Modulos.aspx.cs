//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Navegacao
{
    public partial class Modulos : System.Web.UI.Page
    {
        #region Propriedades

        public int ModPaiAreaConhe;
        public string PanelTitle { get; set; }
        public string PanelTitleImage { get; set; }
        public string ModuloURL { get; set; }
        public bool IsList { get; set; }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Valida a data da licença da instituição
                if (LoginAuxili.DtInicioLicenca.Date > DateTime.Today || LoginAuxili.DtFimLicenca.Date < DateTime.Today)
                {
                    string script = "window.parent.location = '{0}';";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", string.Format(script, "/logout.aspx"), true);

                    return;
                }

                int modPaiId = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.ModPai);
                ModPaiAreaConhe = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.ModPai);
                Session[SessoesHttp.ModulosAtalhoPaiId] = modPaiId;

                ObjectQuery<ADMMODULO> dataSource = RetornaModulos(modPaiId);

                if (dataSource != null && dataSource.Count() > 0)
                {
                    var resultado = from r in dataSource
                                    select new
                                    {
                                        r.ideAdmModulo,
                                        r.nomModulo,
                                        r.nomDescricao,
                                        r.nomItemMenu,
                                        r.numOrdemMenu,
                                        r.nomURLModulo,
                                        Icon = "/Navegacao/Icones/" + r.imgIcone,
                                        PanelTitle = r.ADMMODULO2.nomModulo,
                                        PanelTitleImage = "/Navegacao/Icones/" + r.ADMMODULO2.imgIcone,
                                        IsList = r.ADMMODULO2.flaTipoItemSubMenu.Equals("LST")
                                    };

                    this.PanelTitle = resultado.First().PanelTitle;
                    this.PanelTitleImage = resultado.First().PanelTitleImage;
                    this.IsList = resultado.First().IsList;

                    rptModulos.DataSource = resultado;
                    rptModulos.DataBind();
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
            ModuloURL = String.Format("/Navegacao/Funcionalidades.aspx?{0}=", QueryStrings.ModPai);

            try
            {
                if ((LoginAuxili.ORG_CODIGO_ORGAO == 0) || (LoginAuxili.CO_EMP == 0) || (LoginAuxili.IDEADMUSUARIO == 0))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

                return (ObjectQuery<ADMMODULO>)ADMMODULO.RetornaUsuarioModulos(LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, LoginAuxili.IDEADMUSUARIO, ideModPai).OrderBy(m => m.numOrdemMenu);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                return null;
            }

        }
        #endregion

        protected void rptModulos_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            //--------> Converte o CommandArgument para inteiro
            int selectedModuleId = (int.Parse(e.CommandArgument.ToString()));
            //--------> Retorna da sessão o id do pai dos modulos da lista de primeiro nivel
            int modulosAtalhoPaiId = int.Parse(Session[SessoesHttp.ModulosAtalhoPaiId].ToString());

            //--------> Retorna a lista de modulos pelo id do pai selecionado
            ObjectQuery<ADMMODULO> dataSource = RetornaModulos(selectedModuleId);

            if ((LoginAuxili.ORG_CODIGO_ORGAO == 0) || (LoginAuxili.CO_EMP == 0) || (LoginAuxili.IDEADMUSUARIO == 0))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            var modulosList = ADMMODULO.RetornaUsuarioModulos(LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, LoginAuxili.IDEADMUSUARIO, modulosAtalhoPaiId).ToList();

            //--------> Encontra o modulo selecionado na lista
            var selectedModulo = modulosList.Find(m => m.ideAdmModulo.Equals(selectedModuleId));
            modulosList.Remove(selectedModulo);

            if (selectedModulo == null)
            {
                selectedModulo = (from m in ADMMODULO.RetornaTodosRegistros()
                                  where m.ideAdmModulo.Equals(selectedModuleId)
                                  select new { m.ADMMODULO2 }).FirstOrDefault().ADMMODULO2;

                //------------> Coloca o modulo selecionado em primeiro
                modulosList.Remove(selectedModulo);
                modulosList.Insert(0, selectedModulo);
            }

            //--------> Verifica se o datasource foi preenchido e associa ao repeater
            if (dataSource != null)
            {
                var resultado = from r in dataSource
                                select new
                                {
                                    r.ideAdmModulo,
                                    r.nomModulo,
                                    r.nomDescricao,
                                    r.nomItemMenu,
                                    r.numOrdemMenu,
                                    r.nomURLModulo,
                                    Icon = "/Navegacao/Images/Icones/" + r.imgIcone,
                                    PanelTitle = r.ADMMODULO2.nomModulo,
                                    PanelTitleImage = "/Navegacao/Images/Icones/" + r.ADMMODULO2.imgIcone,
                                    IsList = r.ADMMODULO2.flaTipoItemSubMenu.Equals("LST")
                                };

                this.PanelTitle = resultado.First().PanelTitle;
                this.PanelTitleImage = resultado.First().PanelTitleImage;
                this.IsList = resultado.First().IsList;

                rptModulos.DataSource = resultado;

                //------------> Executa o bind em todos os componentes
                this.DataBind();
            }
        }
    }
}