//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources;
using System.Web.UI;
using System.Web.UI.WebControls;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliPagina
    {
        #region Métodos Redirecionamento

        /// <summary>
        /// Redireciona para a página de cadastro.
        /// </summary>
        /// <param name="strOperacao">Operação utilizada</param>
        /// <param name="queryStringIds">Ids da query string</param>
        public static void RedirecionaParaPaginaCadastro(string strOperacao, string queryStringIds)
        {
            string strURL = HttpContext.Current.Request.Url.AbsoluteUri;

            if (!String.IsNullOrEmpty(HttpContext.Current.Request.Url.Query))
                if (HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf(HttpContext.Current.Request.Url.Query) > 0)
                    strURL = HttpContext.Current.Request.Url.AbsoluteUri.Remove(HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf(HttpContext.Current.Request.Url.Query));

            HttpContext.Current.Response.Redirect(strURL.ToLower().Replace("busca.aspx", String.Format("cadastro.aspx?{0}={1}&{2}", QueryStrings.Operacao,
                                                                                                    strOperacao, queryStringIds)));
        }

        /// <summary>
        /// Redireciona para a página de busca.
        /// </summary>
        public static void RedirecionaParaPaginaBusca()
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Replace("cadastro.aspx", "busca.aspx"));
        }

        /// <summary>
        /// Retorna uma string com a URL da página de busca.
        /// </summary>
        /// <returns>String com a URL da página de busca</returns>
        public static string RetornaURLRedirecionamentoPaginaBusca()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Replace("cadastro.aspx", "busca.aspx");
        }

        /// <summary>
        /// Faz o redirecionamento para a própria página de cadastro.
        /// </summary>
        public static void RedirecionaParaPaginaCadastro()
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Replace("busca.aspx", "cadastro.aspx"));
        }

        /// <summary>
        /// Faz o redirecionamento para a página de mensagem informando se foi efetuado com "Sucesso" ou "Erro".
        /// </summary>
        /// <param name="strMensagem">Mensagem a ser exibida</param>
        /// <param name="strURLAnterior">URL anterior</param>
        /// <param name="TipoMessagemRedirecionamento">Tipo da mensagem de retorno</param>
        public static void RedirecionaParaPaginaMensagem(string strMensagem, string strURLAnterior, C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento TipoMessagemRedirecionamento)
        {
            strMensagem = HttpContext.Current.Server.UrlEncode(strMensagem);
            HttpContext.Current.Response.Redirect(String.Format("/RedirecionaMensagem.aspx?{0}={1}&{2}={3}&{4}={5}",
                                                                QueryStrings.MessagemQueryString,
                                                                strMensagem,
                                                                QueryStrings.TipoMessagemRedirecionamento,
                                                                Enum.GetName(typeof(C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento), TipoMessagemRedirecionamento),
                                                                QueryStrings.RedirecionaParaURL,
                                                                strURLAnterior.Replace('?', '&').Replace("#", ""))
                                                                );
        }

        /// <summary>
        /// Faz o redirecionamento para a página de mensagem informando se o relatório foi gerado com "Sucesso" ou "Erro".
        /// </summary>
        /// <param name="strMensagem">Mensagem a ser exibida</param>
        /// <param name="strURLAnterior">URL anterior</param>
        /// <param name="TipoMessagemRedirecionamento">Tipo da mensagem de retorno</param>
        public static void RedirecionaParaPaginaMensagemRelatorio(string strMensagem, string strURLAnterior, C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento TipoMessagemRedirecionamento)
        {
            strMensagem = HttpContext.Current.Server.UrlEncode(strMensagem);
            HttpContext.Current.Session["ApresentaRelatorio"] = 1;
            HttpContext.Current.Response.Redirect(String.Format("/RedirecionaMensagem.aspx?{0}={1}&{2}={3}&{4}={5}",
                                                                QueryStrings.MessagemQueryString,
                                                                strMensagem,
                                                                QueryStrings.TipoMessagemRedirecionamento,
                                                                Enum.GetName(typeof(C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento), TipoMessagemRedirecionamento),
                                                                QueryStrings.RedirecionaParaURL,
                                                                strURLAnterior.Replace('?', '&').Replace("#", ""))
                                                                );
        }

        /// <summary>
        /// Não faz o redirecionamento para nenhuma página, apresentando apenas a mensagem informada. 
        /// </summary>
        /// <param name="strMensagem">Mensagem a ser exibida</param>
        /// <param name="TipoMessagemRedirecionamento">Tipo da mensagem de retorno</param>
        public static void RedirecionaParaNenhumaPagina(string strMensagem, C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento TipoMessagemRedirecionamento)
        {
            strMensagem = HttpContext.Current.Server.UrlEncode(strMensagem);
            HttpContext.Current.Server.Transfer(String.Format("/RedirecionaMensagem.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                                                                QueryStrings.MessagemQueryString,
                                                                strMensagem,
                                                                QueryStrings.TipoMessagemRedirecionamento,
                                                                Enum.GetName(typeof(C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento), TipoMessagemRedirecionamento),
                                                                QueryStrings.RedirecionaParaURL,
                                                                "",
                                                                QueryStrings.NoPageToRedirect,
                                                                0)
                                                                );

        }

        /// <summary>
        /// Redireciona para página informada com a mensagem de sucesso.
        /// </summary>
        /// <param name="strMensagem">Mensagem a ser exibida</param>
        /// <param name="strURLAnterior">URL anterior</param>
        public static void RedirecionaParaPaginaSucesso(string strMensagem, string strURLAnterior)
        {
            RedirecionaParaPaginaMensagem(strMensagem, strURLAnterior, RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
        }

        /// <summary>
        /// Redireciona para a página de aviso de manutenção
        /// </summary>
        public static void RedirecionaParaPaginaManutencao()
        {
            HttpContext.Current.Response.Redirect("/temporario.aspx");
        }

        /// <summary>
        /// Redireciona para a página de login
        /// </summary>
        public static void RedirecionaParaPaginaDefault()
        {
            HttpContext.Current.Response.Redirect("/Default.aspx");
        }

        /// <summary>
        /// Redireciona para página informada com a mensagem de erro.
        /// </summary>
        /// <param name="strMensagem">Mensagem a ser exibida</param>
        /// <param name="strURLAnterior">URL anterior</param>
        public static void RedirecionaParaPaginaErro(string strMensagem, string strURLAnterior)
        {
            RedirecionaParaPaginaMensagem(strMensagem, strURLAnterior, RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Abre a URL informada em uma nova janela do browser
        /// </summary>
        /// <param name="paginaCorrente">Pagina corrente</param>
        /// <param name="pStrUrl">URL de abertura da nova janela</param>
        public static void AbreNovaJanela(Page paginaCorrente, string pStrUrl)
        {
            string strURL = String.Format("{0}", pStrUrl);
            paginaCorrente.ClientScript.RegisterStartupScript(paginaCorrente.GetType(), "script", "window.open(\"" + strURL + "\");", true);
        }

        /// <summary>
        /// Metodo genérico para os relatórios que analisa o retorno da dll e exibe mensagens informativas.
        /// </summary>
        /// <param name="intRetorno">Código do retorno</param>
        /// <param name="strURLPagina">URL anterior</param>
        public static void TrataRetornoRelatorio(int intRetorno, string strURLPagina)
        {
            strURLPagina += String.Format("?moduloNome={0}", HttpContext.Current.Request.QueryString["moduloNome"]);

            //--------> Para relatóro gerado com sucesso.
            if (intRetorno == 1)
                AuxiliPagina.RedirecionaParaPaginaMensagemRelatorio("Aguarde! Relatório sendo disponibilizado...", strURLPagina + "&ApresentaRelatorio=1", RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
            else
                //------------> Quando dá erro na geração do relatório.
                if (intRetorno == 0)
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Erro na geração do Relatório! Tente novamente.", strURLPagina, RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
                else
                    //----------------> Quando a consulta retornou zero registros.
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Não existem dados para a impressão aos parâmetros informados.", strURLPagina, RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
        }

        /// <summary>
        /// Metodo genérico para os relatórios que analisa o retorno da dll e exibe mensagens informativas.
        /// </summary>
        /// <param name="intRetorno">Código do retorno</param>
        /// <param name="strURLPagina">URL anterior</param>
        public static void TrataRetornoRelatorioNovo(int intRetorno, string strURLPagina, Page page)
        {
            strURLPagina += String.Format("?moduloNome={0}", HttpContext.Current.Request.QueryString["moduloNome"]);

            //--------> Para relatóro gerado com sucesso.
            if (intRetorno == 1)
                AuxiliPagina.RedirecionaParaPaginaMensagemRelatorio("Aguarde! Relatório sendo disponibilizado...", strURLPagina + "&ApresentaRelatorio=1", RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
            else
                //------------> Quando dá erro na geração do relatório.
                if (intRetorno == 0)
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Erro na geração do Relatório! Tente novamente.", strURLPagina, RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
                else
                    //----------------> Quando a consulta retornou zero registros.
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Não existem dados para a impressão aos parâmetros informados.", strURLPagina, RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
        }
        #endregion

        #region Métodos Mensagens

        /// <summary>
        /// Apresenta na página selecionada a mensagem de Erro.
        /// </summary>
        /// <param name="paginaSelec">Página selecionada</param>
        /// <param name="strMensagErro">Mensagem apresentada</param>
        public static void EnvioMensagemErro(Page paginaSelec, string strMensagErro)
        {
            paginaSelec.Validators.Add(new CustomValidator { IsValid = false, ErrorMessage = strMensagErro });
            paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "Alert", "javascript:document.getElementById('divValidationSummary').setAttribute(\"style\", \"display: block\");", true);
        }

        /// <summary>
        /// Apresenta na página selecionada a mensagem de Erro quando em um PopUp.
        /// </summary>
        /// <param name="paginaSelec">Página selecionada</param>
        /// <param name="strMensagErro">Mensagem apresentada</param>
        public static void EnvioMensagemErroPopUp(Page paginaSelec, string strMensagErro)
        {
            paginaSelec.Validators.Add(new CustomValidator { IsValid = false, ErrorMessage = strMensagErro });
            paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "Alert", "javascript:alert('" + strMensagErro + "');", true);
        }

        /// <summary>
        /// Realiza um aviso no sistema sobre mostrando um dialog
        /// </summary>
        /// <param name="paginaSelec">Pagina selecionada</param>
        /// <param name="strAviso">Mensagem a ser apresentada</param>
        public static void EnvioAvisoGeralSistema(Page paginaSelec, string strAviso)
        {
            //paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "AvisoGeralSistema", "javascript:avisoGeralSistema('" + strAviso + "')", true);
            paginaSelec.Validators.Add(new CustomValidator { IsValid = false, ErrorMessage = strAviso });
            paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "AvisoGeralSistema", "javascript:alert('" + strAviso + "')", true);
        }

        /// <summary>
        /// Executa java script no sistema
        /// </summary>
        /// <param name="paginaSelec"></param>
        /// <param name="strAviso"></param>
        public static void ExecutarJavaScriptSistema(Page paginaSelec, string strAviso)
        {
            paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "Executar", "javascript:" + strAviso, true);
        }

        /// <summary>
        /// Apresenta na página selecionada a mensagem de Sucesso.
        /// </summary>
        /// <param name="paginaSelec">Página selecionada</param>
        /// <param name="strMensagErro">Mensagem apresentada</param>
        public static void EnvioMensagemSucesso(Page paginaSelec, string strMensagSucesso)
        {
            paginaSelec.Validators.Add(new CustomValidator { IsValid = false, ErrorMessage = strMensagSucesso });
            paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "Alert", "javascript:alert('" + strMensagSucesso + "');", true);
        }

        public static void EnvioMengaemConfirmacao(Page paginaSelec, string strMensagConfirmacao)
        {
            paginaSelec.Validators.Add(new CustomValidator { IsValid = false, ErrorMessage = strMensagConfirmacao });
            paginaSelec.ClientScript.RegisterStartupScript(paginaSelec.GetType(), "Alert", "javascript:alert('" + strMensagConfirmacao + "');", true);
        }
        #endregion


        internal static void TrataRetornoRelatorio(string lRetorno, string p)
        {
            throw new NotImplementedException();
        }
    }
}