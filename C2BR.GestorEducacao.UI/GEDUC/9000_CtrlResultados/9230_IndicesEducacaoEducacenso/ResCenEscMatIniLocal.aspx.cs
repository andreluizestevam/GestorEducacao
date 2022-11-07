//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: INFORMAÇÕES DO EDUCACENSO
// OBJETIVO:  RESULTADO DO CENSO ESCOLAR - MATRÍCULAS INICIAIS (LOCAL - ÚLTIMO ANO LETIVO)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Text;
using System.Globalization;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9230_IndicesEducacaoEducacenso
{
    public partial class ResCenEscMatIniLocal : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e) 
        {        
            if (!IsPostBack) 
            {
                string descUF = TB74_UF.RetornaTodosRegistros().Where(p => p.CODUF.Equals(LoginAuxili.CO_UF_INSTITUICAO)).FirstOrDefault().DESCRICAOUF.ToUpper().Trim().Replace(" ","+");

                descUF = descUF.Replace("Á", "%C1").Replace("Ã", "%C3").Replace("Í", "%CD").Replace("Ô", "%D4");

                string ano = (DateTime.Now.Year - 1).ToString();

                string cidade = RemoverAcentos(LoginAuxili.NO_CIDADE_INSTITUICAO.Trim().Replace(" ", "+"));

                string URL = String.Format("http://www.inep.gov.br/basica/censo/Escolar/Matricula/censoescolar_2010.asp?metodo=1&ano={0}&UF={1}&MUNICIPIO={2}&Submit=Consultar", ano, descUF, cidade);

                string result = System.Web.HttpUtility.UrlEncode(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(URL));
              
                Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "abrir('" + URL +"');", true);    
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Remove acentos da palavra
        /// </summary>
        /// <param name="texto">Texto</param>
        /// <returns>Texto sem acentos</returns>
        private string RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}