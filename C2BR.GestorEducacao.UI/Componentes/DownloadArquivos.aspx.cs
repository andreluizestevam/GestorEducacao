//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: DOWNLOAD DE ARQUIVOS
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
    public partial class DownloadArquivos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            grdDownloadArquivos.DataSource = (from arquivoCompartilhado in ArquivoCompartilhado.RetornaTodosRegistros()
                                              where arquivoCompartilhado.Ativo == true && arquivoCompartilhado.DataValidade >= DateTime.Now
                                              select new
                                              {
                                                  arquivoCompartilhado.NomeArquivo, arquivoCompartilhado.URL, DataPublicacao = arquivoCompartilhado.DataPublicacao,
                                                  Descricao = arquivoCompartilhado.Descricao.Substring(0, (arquivoCompartilhado.Descricao.Length < 35) ? arquivoCompartilhado.Descricao.Length : 35),
                                                  SiglaUnidade = arquivoCompartilhado.TB25_EMPRESA.sigla, DataValidade = arquivoCompartilhado.DataValidade, arquivoCompartilhado.ArquivoCompartilhadoId
                                              }).ToList().OrderBy( a => a.NomeArquivo );

             grdDownloadArquivos.DataBind();
        }
        #endregion

        protected void grdDownloadArquivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
//--------> Criação dos links para download
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("title", DataBinder.Eval(e.Row.DataItem, "Descricao").ToString());
                e.Row.Attributes.Add("onclick", String.Format("window.open('{0}')", DataBinder.Eval(e.Row.DataItem, "URL").ToString()));
            }
        }
    }
}