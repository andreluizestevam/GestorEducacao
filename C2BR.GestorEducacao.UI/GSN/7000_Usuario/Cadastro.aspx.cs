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

namespace C2BR.GestorEducacao.UI.GSN._7000_Usuario
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

            if(Request.Browser.IsMobileDevice)
         {
            txtMobiDevice.Value = "M";
         }
         else
         {
            txtMobiDevice.Value = "W";
         }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN005_USUARIO TSN005;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN005 = new TSN005_USUARIO();
                TSN005.NM_USUARIO = txtNome.Text;
                TSN005.NU_CPF = txtCPF.Text;
                TSN005.DE_EMAIL = txtEmail.Text;
                TSN005.CO_SITUACAO = ddlStatus.SelectedValue;
                TSN005.DT_SITUACAO = DateTime.Now;
                TSN005.DT_CADASTRO = DateTime.Now;
                TSN005.CO_IP_CADASTRO = Library.Auxiliares.Util.RetornaIpCliente();
                TSN005.CO_ORIGEM_CADASTRO = txtMobiDevice.Value;
                TSN005.CO_SEXO = ddlSexo.SelectedValue;
                TSN005.DT_NASCIMENTO = Convert.ToDateTime(txtDataNascimento.Text);
                TSN005.IM_FOTO_USUARIO_URL = txtImagem.Text;             
            }
            else
                TSN005 = RetornaEntidade();

            TSN005.NM_USUARIO = txtNome.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN005;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN005_USUARIO TSN005 = RetornaEntidade();

            if (TSN005 != null)
            {
                txtNome.Text = TSN005.NM_USUARIO;
                txtCPF.Text = TSN005.NU_CPF;
                txtEmail.Text = TSN005.DE_EMAIL;
                ddlStatus.SelectedValue = TSN005.CO_SITUACAO;
                ddlSexo.SelectedValue = TSN005.CO_SEXO;
                txtDataNascimento.Text = TSN005.DT_NASCIMENTO.ToString();
                txtImagem.Text = TSN005.DE_EMAIL;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN005_USUARIO</returns>
        private TSN005_USUARIO RetornaEntidade()
        {

            TSN005_USUARIO TSN005 = TSN005_USUARIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN005 == null) ? new TSN005_USUARIO() : TSN005;
        }
        #endregion
    }
}