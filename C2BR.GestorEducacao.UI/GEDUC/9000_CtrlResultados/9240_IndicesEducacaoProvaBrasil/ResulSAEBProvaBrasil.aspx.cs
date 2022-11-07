// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: INFORMAÇÕES DO PROVA BRASIL
// OBJETIVO:  RESULTADOS DA SAEB/PROVA BRASIL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9240_IndicesEducacaoProvaBrasil
{
    public partial class ResulSAEBProvaBrasil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) 
        {        
            if (!IsPostBack) 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "abrir('http://sistemasprovabrasil2.inep.gov.br/ProvaBrasilResultados/home.seam');", true);    

        }   
    }
}