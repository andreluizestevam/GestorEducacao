//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: LISTA DOENÇAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//19/08/2014| Maxwell Almeida            | Criação do componente de listagem de exames

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
    public partial class ListarTiposExames : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdListarExames.DataSource = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                              join tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros() on tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO equals tbs354.ID_PROC_MEDIC_GRUPO
                                              join tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros() on tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP equals tbs355.ID_PROC_MEDIC_SGRUP
                                              where tbs356.CO_TIPO_PROC_MEDI == "EX"
                                                select new
                                                {
                                                    tbs356.ID_PROC_MEDI_PROCE,
                                                    tbs356.CO_PROC_MEDI,
                                                    tbs354.NM_PROC_MEDIC_GRUPO,
                                                    tbs355.NM_PROC_MEDIC_SGRUP,
                                                    tbs356.NM_PROC_MEDI,
                                                });

                grdListarExames.DataBind();
            }
        }
        #endregion
    }
}