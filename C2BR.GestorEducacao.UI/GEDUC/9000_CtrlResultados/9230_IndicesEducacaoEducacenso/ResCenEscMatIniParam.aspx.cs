// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: INFORMAÇÕES DO EDUCACENSO
// OBJETIVO:  RESULTADO DO CENSO ESCOLAR - MATRÍCULAS INICIAIS (PARAMETRIZADO)
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
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9230_IndicesEducacaoEducacenso
{
    public partial class ResCenEscMatIniParam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) 
        {        
            if (!IsPostBack) 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "abrir('http://www.inep.gov.br/basica/censo/Escolar/matricula/default.asp');", true);              
        }   
    }
}