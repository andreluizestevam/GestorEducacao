//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: INFORMAÇÕES DO ENEM
// OBJETIVO:  BOLETIM INDIVIDUAL DE RESULTADOS - ANOS ANTERIORES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9220_IndicesEducacaoEnem
{
    public partial class BoletimIndivResul : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) 
        {        
          if (!IsPostBack)
              Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "abrir('http://sistemasenem.inep.gov.br/EnemResultado/');", true);          
        }   
    }
}