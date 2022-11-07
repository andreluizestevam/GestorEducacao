using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSN._2000_Artigos
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                txtDescricao.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN001_ARTIGOS TSN001;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN001 = new TSN001_ARTIGOS();
                TSN001.DE_ARTIGO = txtDescricao.Text;
                TSN001.DE_LINK = txtLink.Text;
                TSN001.IM_ARTIGO_URL = txtImagem.Text;
                TSN001.DT_CRIACAO = DateTime.Now;
                TSN001.FL_ATIVO = "A";
            }
            else
                TSN001 = RetornaEntidade();

            TSN001.DE_ARTIGO = txtDescricao.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN001;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN001_ARTIGOS TSN001 = RetornaEntidade();

            if (TSN001 != null)
            {
                txtDescricao.Text = TSN001.DE_ARTIGO;
                txtLink.Text = TSN001.DE_LINK;
                txtImagem.Text = TSN001.IM_ARTIGO_URL;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN001_ARTIGOS</returns>
        private TSN001_ARTIGOS RetornaEntidade()
        {

            TSN001_ARTIGOS TSN001 = TSN001_ARTIGOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN001 == null) ? new TSN001_ARTIGOS() : TSN001;
        }
        #endregion
    }
}