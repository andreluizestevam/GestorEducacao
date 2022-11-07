//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES EDUCAÇÃO (EXTERNOS)
// OBJETIVO:  ÍNDICE DE DESENVOLVIMENTO DA EDUCAÇÃO BÁSICA (IDEB)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9210_IndicesEducacaoExterno.F9213_IDEB
{
    public partial class IDEB : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e) 
        {        
            if (!IsPostBack)          
                Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "abrir('http://sistemasideb.inep.gov.br/resultado/');", true);  
        }   
  }
}