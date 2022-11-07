//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public static class QueryStringAuxili
    {
        #region Propriedades

        public static string OperacaoCorrenteQueryString { get { return RetornaOperacaoCorrenteQueryString(); } }
        #endregion

        #region Retornos QueryString

        /// <summary>
        /// Retorna o valor, como string, de acordo com a chave da querystring informada
        /// </summary>
        /// <param name="queryStringChave">Chave da querystring</param>
        /// <returns>Valor da querystring como string</returns>
        public static string QueryStringValor(this string queryStringChave)
        {
            return HttpContext.Current.Request.QueryString.Get(queryStringChave);
        }

        /// <summary>
        /// Retorna o valor, como inteiro, de acordo com a chave da querystring informada
        /// </summary>
        /// <param name="queryStringChave"></param>
        /// <returns>Valor da querystring como inteiro</returns>
        public static int QueryStringValorInt(this string queryStringChave)
        {
            int queryStringValorInt = 0;

            int.TryParse(HttpContext.Current.Request.QueryString.Get(queryStringChave), out queryStringValorInt);

            return queryStringValorInt;
        }
        #endregion

        #region Métodos Retorno

        /// <summary>
        /// Método que retorna a operação corrente(como string) da querystring
        /// </summary>
        /// <returns>Retorna a operação corrente(como string) da querystring</returns>
        public static string RetornaOperacaoCorrenteQueryString()
        {
            string queryStringOperacao = QueryStrings.NenhumaOperacao;

            if (HttpContext.Current.Request.QueryString[QueryStrings.Operacao] == QueryStrings.OperacaoExclusao)
                queryStringOperacao = QueryStrings.OperacaoExclusao;
            else if (HttpContext.Current.Request.QueryString[QueryStrings.Operacao] == QueryStrings.OperacaoAlteracao)
                queryStringOperacao = QueryStrings.OperacaoAlteracao;
            else if (HttpContext.Current.Request.QueryString[QueryStrings.Operacao] == QueryStrings.OperacaoInsercao)
                queryStringOperacao = QueryStrings.OperacaoInsercao;
            else if (HttpContext.Current.Request.QueryString[QueryStrings.Operacao] == QueryStrings.OperacaoDetalhe)
                queryStringOperacao = QueryStrings.OperacaoDetalhe;
            else if (HttpContext.Current.Request.QueryString[QueryStrings.Operacao] == QueryStrings.OperacaoBusca)
                queryStringOperacao = QueryStrings.OperacaoBusca;
            else if (HttpContext.Current.Request.QueryString.Count == 0)
                queryStringOperacao = QueryStrings.OperacaoInsercao;
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains(QueryStrings.PaginaURLIFrame))
                queryStringOperacao = QueryStrings.PaginaURLIFrame;

            return queryStringOperacao;
        }

        /// <summary>
        /// Retorna o valor, como string, de acordo com o nome da chave da querystring informada
        /// </summary>
        /// <param name="nomeChave">Nome da chave da querystring</param>
        /// <returns>Valor da querystring como string</returns>
        public static string RetornaQueryStringPelaChave(string nomeChave) 
        {
            return HttpContext.Current.Request.QueryString[nomeChave];
        }

        /// <summary>
        /// Retorna o valor, como inteiro, de acordo com o nome da chave da querystring informada
        /// </summary>
        /// <param name="nomeChave">Nome da chave da querystring</param>
        /// <returns>Valor da querystring como inteiro</returns>
        public static int RetornaQueryStringComoIntPelaChave(string nomeChave)
        {
            int queryStringRetornoInt = 0;
            int.TryParse(HttpContext.Current.Request.QueryString[nomeChave], out queryStringRetornoInt);

            return queryStringRetornoInt;
        }
        #endregion

        #region Métodos Redirecionamento 

        /// <summary>
        /// Redireciona para determinada página de acordo com a operação informada.
        /// </summary>
        /// <param name="strOperacao">Operação utilizada</param>
        /// <param name="QueryStringValor">Valor da QueryString</param>
        public static void RedirecionaParaOperacao(string strOperacao, object QueryStringValor)
        {
            string strURL = "";

            if (HttpContext.Current.Request.QueryString.Count > 0)
                strURL = HttpContext.Current.Request.Url.AbsoluteUri.Remove(HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('?'));
            else
                strURL = HttpContext.Current.Request.Url.AbsoluteUri;

            HttpContext.Current.Response.Redirect(String.Format("{0}?{1}={2}",
                strURL,
                strOperacao,
                QueryStringValor));
        }

        /// <summary>
        /// Redireciona para determinada página de acordo com a operação informada.
        /// </summary>
        /// <param name="queryStrings">Lista de QueryStrings</param>
        public static void RedirecionaParaOperacao(List<KeyValuePair<string, object>> queryStrings)
        {
            string strURL = "";

            if (HttpContext.Current.Request.QueryString.Count > 0)
                strURL = HttpContext.Current.Request.Url.AbsoluteUri.Remove(HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('?'));
            else
                strURL = HttpContext.Current.Request.Url.AbsoluteUri;

            strURL += "?";

            foreach (KeyValuePair<string, object> queryString in queryStrings)
                strURL += String.Format("{0}={1}&",
                                        queryStrings.First().Key,
                                        queryStrings.First().Value);


            HttpContext.Current.Response.Redirect(strURL);
        }

        /// <summary>
        /// Redireciona para determinada página de acordo com as operações informadas.
        /// </summary>
        /// <param name="strDeOperacao">Da operação</param>
        /// <param name="strParaOperacao">Para operação</param>
        public static void RedirecionaParaOperacao(string strDeOperacao, string strParaOperacao)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(strDeOperacao))
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace(strDeOperacao, strParaOperacao));
            else
                AuxiliPagina.RedirecionaParaPaginaBusca();
        }
        #endregion
    }
}
