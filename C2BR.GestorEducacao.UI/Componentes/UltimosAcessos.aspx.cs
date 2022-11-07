//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: ÚLTIMOS ACESSOS DO USUÁRIO
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
    public partial class UltimosAcessos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginAuxili.CO_COL != 0 && LoginAuxili.CO_UNID_FUNC != 0)
                {
                    var tb236 = (from lTb236 in TB236_LOG_ATIVIDADES.RetornaTodosRegistros()
                                 join admModulo in ADMMODULO.RetornaTodosRegistros() on lTb236.IDEADMMODULO equals admModulo.ideAdmModulo
                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb236.CO_EMP_ATIVI_LOG equals tb25.CO_EMP
                                 where lTb236.CO_COL == LoginAuxili.CO_COL && lTb236.CO_EMP == LoginAuxili.CO_UNID_FUNC
                                 select new
                                 {
                                     lTb236.DT_ATIVI_LOG, tb25.sigla, admModulo.nomModulo, lTb236.NR_IP_ACESS_ATIVI_LOG,
                                     ACAO_LOG = lTb236.CO_ACAO_ATIVI_LOG == "X" ? "Sem Ação" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "G" ? "Gravação" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "I" ? "Inclusão" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "E" ? "Alteração" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "D" ? "Exclusão" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "P" ? "Consulta" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "S" ? "Script" :
                                            lTb236.CO_ACAO_ATIVI_LOG == "R" ? "Relatório" : ""
                                 }).OrderByDescending( l => l.DT_ATIVI_LOG ).Take(50);

                    grdUltimosAcessos.DataSource = tb236;
                    grdUltimosAcessos.DataBind();
                }
            }
        }
        #endregion

        protected void grdUltimosAcessos_RowDataBound(object sender, GridViewRowEventArgs e)
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