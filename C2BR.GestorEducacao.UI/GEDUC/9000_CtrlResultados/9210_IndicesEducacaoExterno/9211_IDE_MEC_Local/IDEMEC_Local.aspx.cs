//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: DESEMPENHO, ÍNDICES E ESTATÍSTICAS
// SUBMÓDULO: ÍNDICES E INDICADORES EDUCAÇÃO (EXTERNOS)
// OBJETIVO:  INDICADORES DEMOGRÁFICOS E EDUCACIONAIS (LOCAL)
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9210_IndicesEducacaoExterno.F9211_IDE_MEC_Local
{
    public partial class IDEMEC_Local : System.Web.UI.Page
    {
        #region Variáveis

        private static string strURL_MEC_IDE = "http://ide.mec.gov.br//2008/gerarTabela.php?municipio=";

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) 
            {
                ddlUF.Items.FindByText(C2BR.GestorEducacao.UI.Library.Auxiliares.LoginAuxili.CO_UF_INSTITUICAO).Selected = true;
                CarregaCidades(ddlUF.SelectedValue);
                if (ddlCidade.Items.Count > 0) 
                {
                    if (ddlCidade.Items.FindByText(C2BR.GestorEducacao.UI.Library.Auxiliares.LoginAuxili.NO_CIDADE_INSTITUICAO) != null)
                        ddlCidade.Items.FindByText(C2BR.GestorEducacao.UI.Library.Auxiliares.LoginAuxili.NO_CIDADE_INSTITUICAO).Selected = true;
                    else
                        ddlCidade.SelectedIndex = 0;
                }

                if (ddlCidade.SelectedValue != string.Empty)
                    LiteralHtml.Text = RemoveAdditionalData(RetornaFonteHtml(ddlCidade.SelectedValue));
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que retorna o código HTML
        /// </summary>
        /// <param name="strId">Id da URL</param>
        /// <returns>String do HTML gerado</returns>
        private string RetornaFonteHtml(string strId)
        {
            HttpWebRequest webRequest = HttpWebRequest.Create(string.Concat(strURL_MEC_IDE, strId)) as HttpWebRequest;
            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            string strFonteHtml = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.UTF8).ReadToEnd();
            webResponse.Close();
            return strFonteHtml;
        }

        /// <summary>
        /// Método que modifica código HTML
        /// </summary>
        /// <param name="strDados">String de dados</param>
        /// <returns>String de dados alterada</returns>
        private string RemoveAdditionalData(string strDados)
        {
            strDados = strDados.Replace(@"src=""/novo/img/imprimir.png""", @"src=""../../../../Library/IMG/Gestor_IcoImprimir.png""");
            strDados = strDados.Replace(@"href=""/novo/relatorio/pdf/coibge/5208707/tipo/municipios""", @"href=""""");
            strDados = strDados.Replace(@"src=""/novo/img/nova_consulta.gif""", @"src=""../../../../Library/IMG/Gestor_LogoIndiceDemogr.gif""");
            strDados = strDados.Replace(@"<div align=""right"" style=""padding:3px;""", @"<div style=""display:none;""");
            strDados = strDados.Replace(@"<div class=""barra_governo"">", @"<!--<div class=""barra_governo"">");
            strDados = strDados.Replace(@"<div align=""center"">", @"--><div style=""overflow-y: auto; height: 445px;"" align=""center"">");
            strDados = strDados.Replace(@"/2011/img/logo.gif", @"../../../../Library/IMG/Gestor_LogoIndiceDemogr.gif");
            strDados = strDados.Replace(@"href=""/novo""", @"href=""""");
            return strDados;
        }

        /// <summary>
        /// Método que carrega o dropdown de Municípios
        /// </summary>
        /// <param name="strUf">UF</param>
        private void CarregaCidades(string strUf)
        {
            string strCaminhoArquivoXML = HttpContext.Current.Request.MapPath("~/GEDUC/9000_CtrlResultados/9210_IndicesEducacaoExterno/9211_IDE_MEC_Local/Library/Municipios.xml");
            System.Xml.Linq.XElement xml = System.Xml.Linq.XElement.Load(strCaminhoArquivoXML, System.Xml.Linq.LoadOptions.SetBaseUri | System.Xml.Linq.LoadOptions.SetLineInfo);
            IEnumerable<System.Xml.Linq.XElement> ieElementos = xml.Elements();

            var resultado = (from result in ieElementos
                             where result.Element("Uf").Value == strUf
                             select new { Id = result.Element("Id").Value, Nome = result.Element("Nome").Value }).OrderBy(o => o.Nome);

            ddlCidade.DataSource = resultado;
            ddlCidade.DataTextField = "Nome";
            ddlCidade.DataValueField = "Id";
            ddlCidade.DataBind();
        }
        #endregion

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (ddlUF.SelectedValue != "")
                CarregaCidades(ddlUF.SelectedValue);
        }        
  }
}