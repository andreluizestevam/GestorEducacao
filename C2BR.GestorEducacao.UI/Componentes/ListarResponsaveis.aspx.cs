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
    public partial class ListarResponsaveis : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdListarResponsaveis.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                    where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                                    select new
                                                    {
                                                        tb108.NU_CONTROLE,
                                                        tb108.CO_RESP, NO_RESP = (tb108.NO_RESP).ToUpper(), sexo = tb108.CO_SEXO_RESP == "M" ? "Masculino" : "Feminino",
                                                        tb108.NU_NIS_RESP, tb108.DT_NASC_RESP,
                                                        NU_TELE_CELU_RESP = tb108.NU_TELE_CELU_RESP.Length == 10 ?
                                                            tb108.NU_TELE_CELU_RESP.Insert(6, "-").Insert(2, " ").Insert(2, ")").Insert(0, "(") : tb108.NU_TELE_CELU_RESP,
                                                        NU_CPF_RESP = tb108.NU_CPF_RESP.Length == 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : ""
                                                    }).OrderBy( r => r.NO_RESP );
                grdListarResponsaveis.DataBind();
            }
        }
        #endregion

        protected void grdListarResponsaveis_RowDataBound(object sender, GridViewRowEventArgs e)
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