using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GSN._4000_Tipo_Credenciado
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
                txtSIGLA.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN014_TIPO_CREDENCIADO TSN014;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN014 = new TSN014_TIPO_CREDENCIADO();
                TSN014.DE_SIGLA = txtSIGLA.Text;
                TSN014.DE_TIPO_CREDENCIADO = txtTIPO_CREDENCIADO.Text;
                TSN014.CO_SITUACAO = ddlStatus.SelectedValue;
                TSN014.DT_SITUACAO = DateTime.Now;
            }
            else
                TSN014 = RetornaEntidade();

            TSN014.DE_SIGLA = txtSIGLA.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN014;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN014_TIPO_CREDENCIADO TSN014 = RetornaEntidade();

            if (TSN014 != null)
            {
                txtSIGLA.Text = TSN014.DE_SIGLA;
                txtTIPO_CREDENCIADO.Text = TSN014.DE_TIPO_CREDENCIADO;
                ddlStatus.SelectedValue = TSN014.CO_SITUACAO;
                
            }
        }

        //protected void BtnSalvar_OnClick(object sender, EventArgs e)
        //{
       //     Persistencias(false);
        //}


        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN014_TIPO_CREDENCIADO</returns>
        private TSN014_TIPO_CREDENCIADO RetornaEntidade()
        {
            TSN014_TIPO_CREDENCIADO TSN014 = TSN014_TIPO_CREDENCIADO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN014 == null) ? new TSN014_TIPO_CREDENCIADO() : TSN014;
        }
        #endregion
    }
}