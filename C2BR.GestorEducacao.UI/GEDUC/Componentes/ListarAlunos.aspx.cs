//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: LISTA ALUNOS
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
    public partial class ListarAlunos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               var resultado  = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                              select new
                                              {
                                                  tb07.CO_ALU, NO_ALU = (tb07.NO_ALU).ToUpper(), tb07.NU_NIRE, situAlu = tb07.CO_SITU_ALU == "A" ? "Ativo" : "Inativo",
                                                  tb07.DT_NASC_ALU, sexo = tb07.CO_SEXO_ALU == "M" ? "Masculino" : "Feminino", 
                                                  Resp = tb07.TB108_RESPONSAVEL != null ? (tb07.TB108_RESPONSAVEL.NO_RESP).ToUpper() : "",
                                                  cpfResp = tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : ""
                                              }).ToList().OrderBy(u => u.NO_ALU);

               grdListarAlunos.DataSource = from res in resultado
                                            select new
                                           {
                                               res.CO_ALU, res.NO_ALU,
                                               NU_NIRE = res.NU_NIRE.ToString().PadLeft(9,'0'),
                                               res.situAlu, res.DT_NASC_ALU, res.sexo,
                                               res.Resp, res.cpfResp
                                           };

                grdListarAlunos.DataBind();
            }
        }
        #endregion

        protected void grdListarAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
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