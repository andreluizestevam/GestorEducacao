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

namespace C2BR.GestorEducacao.UI.GSN._6000_Medicamentos
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
                txtPrincipioAtivo.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN020_MEDICAMENTOS TSN020;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN020 = new TSN020_MEDICAMENTOS();
                TSN020.DE_PRINCIPIO_ATIVO = txtPrincipioAtivo.Text;
                TSN020.DE_NOME_APRESENTACAO = txtNomeApresentacao.Text;
                TSN020.DE_INDICACAO = txtIndicacao.Text;
                TSN020.DT_CRIACAO = DateTime.Now;
                TSN020.FL_ATIVO = ddlStatus.SelectedValue;
            }
            else
                TSN020 = RetornaEntidade();

            TSN020.DE_PRINCIPIO_ATIVO = txtPrincipioAtivo.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN020;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN020_MEDICAMENTOS TSN020 = RetornaEntidade();

            if (TSN020 != null)
            {
                txtPrincipioAtivo.Text = TSN020.DE_PRINCIPIO_ATIVO;
                txtNomeApresentacao.Text = TSN020.DE_NOME_APRESENTACAO;
                txtIndicacao.Text = TSN020.DE_INDICACAO;
                ddlStatus.SelectedValue = TSN020.FL_ATIVO;
            }
        }
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN020_MEDICAMENTOS</returns>
        private TSN020_MEDICAMENTOS RetornaEntidade()
        {

            TSN020_MEDICAMENTOS TSN020 = TSN020_MEDICAMENTOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN020 == null) ? new TSN020_MEDICAMENTOS() : TSN020;
        }
        #endregion
    }
}