//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Navegacao
{
    public partial class AreasConhecimento : System.Web.UI.Page
    {
        #region Propriedades

        public string ModuloURL { get; set; }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                object dataSource = RetornaAreasConhecimento();

                if (dataSource != null)
                {
                    rptAreasConhecimento.DataSource = dataSource;
                    rptAreasConhecimento.DataBind();
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Retorna, como objeto, a consulta a área de conhecimento
        /// </summary>
        /// <returns></returns>
        private object RetornaAreasConhecimento()
        {
            object areasConhecimento = null;

            try
            {
                if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

                ObjectQuery<ADMMODULO> userAreasConhecimento = ADMMODULO.RetornaAreasConhecimento(LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, LoginAuxili.IDEADMUSUARIO);

                if (userAreasConhecimento != null)
                {
                    var resultado = from r in userAreasConhecimento
                                    orderby r.numOrdemMenu
                                    select new
                                    {
                                        r.ideAdmModulo,
                                        r.nomModulo,
                                        r.nomDescricao,
                                        r.nomItemMenu,
                                        r.numOrdemMenu,
                                        Icon = "/Navegacao/Icones/" + r.imgIcone
                                    };

                    if (resultado.FirstOrDefault() != null)
                    {
                        areasConhecimento = resultado;
                        ModuloURL = String.Format("/Navegacao/Modulos.aspx?{0}=", QueryStrings.ModPai);
                    }
                }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                return null;
            }
            return areasConhecimento;
        }
        #endregion
    }
}