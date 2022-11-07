//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: TROCAR ESCOLA
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
    public partial class TrocarEscola : System.Web.UI.Page
    {
        #region Váriaveis

        int qtdLinhasGrid = 0;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginAuxili.IDEADMUSUARIO != 0)
                {
                    var tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros()
                                 where lTb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO && lTb134.FLA_STATUS == "A"
                                 && lTb134.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new
                                 {
                                     lTb134.TB25_EMPRESA.CO_EMP, lTb134.TB25_EMPRESA.sigla, lTb134.TB25_EMPRESA.NO_FANTAS_EMP,
                                     NO_TIPOEMP = (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_SUPER == "S" ? "[Superior]" : "") +
                                     (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_MEDIO == "S" ? " [Médio]" : "") +
                                     (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_FUNDA == "S" ? " [Fundamental]" : "") +
                                     (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_INFAN == "S" ? " [Infantil]" : "") +
                                     (lTb134.TB25_EMPRESA.CO_FLAG_ENSIN_OUTRO == "S" ? " [Outros]" : ""),
                                     PERFIL = lTb134.AdmPerfilAcesso.nomeTipoPerfilAcesso
                                 }).OrderBy( u => u.NO_FANTAS_EMP );

                    grdUnidadeEducacional.DataSource = tb134;
                    grdUnidadeEducacional.DataBind();
                }
            }
        }
        #endregion

        protected void grdUnidadeEducacional_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
//--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdUnidadeEducacional.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;
            }
        }

        protected void grdUnidadeEducacional_SelectedIndexChanged(object sender, EventArgs e)
        {            
//--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdUnidadeEducacional.DataKeys[grdUnidadeEducacional.SelectedIndex].Value != null)
            {                
                int coEmp = Convert.ToInt32(grdUnidadeEducacional.DataKeys[grdUnidadeEducacional.SelectedIndex].Value);
                var tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros()
                             join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lTb134.TB25_EMPRESA.CO_CIDADE equals tb904.CO_CIDADE
                             where lTb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO && lTb134.TB25_EMPRESA.CO_EMP == coEmp
                             select new
                             {
                                 lTb134.TB25_EMPRESA.CO_EMP, lTb134.TB25_EMPRESA.NO_FANTAS_EMP, lTb134.TB25_EMPRESA.DE_END_EMP, lTb134.TB25_EMPRESA.CO_UF_EMP,
                                 tb904.NO_CIDADE, lTb134.TB25_EMPRESA.CO_TEL1_EMP,
                                 LOGO_IMAGE_ID = lTb134.TB25_EMPRESA.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_USO_LOGO == "U" ? (lTb134.TB25_EMPRESA.Image != null ? lTb134.TB25_EMPRESA.Image.ImageId : 0)
                                 : lTb134.TB25_EMPRESA.TB000_INSTITUICAO.Image3 != null ? lTb134.TB25_EMPRESA.TB000_INSTITUICAO.Image3.ImageId : 0
                             }).FirstOrDefault();

                if (tb134 != null)
                {                    
//----------------> Alterando o código da Unidade em uso para o código da Unidade Educacional Selecionada
                    LoginAuxili.CO_EMP = tb134.CO_EMP;
                    
//----------------> Alterando o nome Fantasia da Unidade em uso para o nome Fantasia da Unidade Educacional Selecionada
                    LoginAuxili.NO_FANTAS_EMP_ALTERADA = tb134.NO_FANTAS_EMP;
                    LoginAuxili.CO_UF_EMP = tb134.CO_UF_EMP;
                    LoginAuxili.NO_CIDADE_EMP = tb134.NO_CIDADE;
                    LoginAuxili.TELEFONE_EMP = tb134.CO_TEL1_EMP;
                    LoginAuxili.ID_IMG_EMPRESA_LOGADA = tb134.LOGO_IMAGE_ID;
                                        
//----------------> Redirecionando para a tela Default
                    Session[SessoesHttp.IdModuloCorrente] = 0;
                    FormsAuthentication.RedirectFromLoginPage(LoginAuxili.NOME_USU_LOGADO, true);
                }
            }
        }
    }
}