//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: LISTA RESERVAS DE MATRÍCULAS DOS ALUNOS
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
    public partial class ListarReservasMat : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                grdListarReservasMat.DataSource = (from tb052 in TB052_RESERV_MATRI.RetornaTodosRegistros()
                                                   where tb052.CO_EMP_CADASTRO == LoginAuxili.CO_EMP && tb052.CO_STATUS == "A"
                                                   select new
                                                   {
                                                       tb052.NU_RESERVA, tb052.TB01_CURSO.NO_CUR, tb052.TB01_CURSO.TB44_MODULO.DE_MODU_CUR,
                                                       aluno = tb052.TB07_ALUNO != null ? tb052.TB07_ALUNO.NO_ALU : tb052.NO_ALU,
                                                       turno = tb052.CO_PERI_TUR == "M" ? "Matutino" : tb052.CO_PERI_TUR == "N" ? "Noturno" : "Vespertino",
                                                       cpfResp = tb052.TB108_RESPONSAVEL != null ? tb052.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb052.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".")                                    
                                                   }).OrderBy( r => r.NU_RESERVA );
                grdListarReservasMat.DataBind();
            }
        }
        #endregion

        protected void grdListarReservasMat_RowDataBound(object sender, GridViewRowEventArgs e)
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