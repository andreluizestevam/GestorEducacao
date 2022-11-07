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
//20/08/2014| Maxwell Almeida            | Componente com o objetivo de listar todos os serviços ambulatoriais cadastrados

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
    public partial class ListarServicoesAmbulatoriais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdListarServAmbu.DataSource = (from tbs331 in TBS331_SERVI_AMBULATORIAIS.RetornaTodosRegistros()
                                                select new
                                                {
                                                    tbs331.ID_SERVI_AMBUL,
                                                    tbs331.CO_SIGLA_SERVI_AMBUL,
                                                    tbs331.DE_SERVI_AMBUL,
                                                });

                grdListarServAmbu.DataBind();
            }
        }
    }
}