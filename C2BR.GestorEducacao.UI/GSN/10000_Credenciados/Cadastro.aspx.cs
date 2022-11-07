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

namespace C2BR.GestorEducacao.UI.GSN._10000_Credenciados
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
                txtNome.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN012_CREDENCIADOS TSN012;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN012 = new TSN012_CREDENCIADOS();
                TSN012.NM_PRESTADOR = txtNome.Text;
                TSN012.DE_CONSELHO = txtConselho.Text;
                TSN012.CO_SITUA_CREDENCIADO = ddlSituacao.SelectedValue;
                TSN012.CO_UNID_PRINCIPAL = Convert.ToInt32(txtCodUnidade.Text);
                TSN012.CO_UNID_FATURAMENTO = Convert.ToInt32(txtCodUnidadeFaturamento.Text);
                TSN012.FL_UNID_PRINCIPAL = ddlUnidPrincipal.SelectedValue;
                TSN012.FL_ACESSIBILIDADE = ddlAcessibilidade.SelectedValue;
                TSN012.DT_SITUA_CREDENCIADO = DateTime.Now;
            }
            else
                TSN012 = RetornaEntidade();

            TSN012.NM_PRESTADOR = txtNome.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN012;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN012_CREDENCIADOS TSN012 = RetornaEntidade();

            if (TSN012 != null)
            {
                txtNome.Text = TSN012.NM_PRESTADOR;
                txtConselho.Text = TSN012.DE_CONSELHO;
                ddlUnidPrincipal.SelectedValue = TSN012.FL_UNID_PRINCIPAL;
                txtCodUnidadeFaturamento.Text = Convert.ToString(TSN012.CO_UNID_FATURAMENTO);
                txtCodUnidade.Text = Convert.ToString(TSN012.CO_UNID_PRINCIPAL);
                ddlAcessibilidade.SelectedValue = TSN012.FL_ACESSIBILIDADE;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN012_CREDENCIADOS</returns>
        private TSN012_CREDENCIADOS RetornaEntidade()
        {

            TSN012_CREDENCIADOS TSN012 = TSN012_CREDENCIADOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN012 == null) ? new TSN012_CREDENCIADOS() : TSN012;
        }
        #endregion
    }
}