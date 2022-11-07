using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1109_CtrlRestrAtend
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
            if (!IsPostBack)
            {

            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o código");
                return;
            }
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o nome");
                return;
            }
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a descrição");
                return;
            }

            try
            {

                TBS379_RESTR_ATEND tbs379 = RetornaEntidade();

                tbs379.CO_RESTR = txtCodigo.Text;
                tbs379.NM_RESTR = txtNome.Text;
                tbs379.DE_RESTR = txtDescricao.Text;

                if (hidSituacao.Value != ddlSituacao.SelectedValue)
                {
                    tbs379.CO_SITUA = ddlSituacao.SelectedValue;
                    tbs379.DT_SITUA = DateTime.Now;
                    tbs379.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs379.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs379.IP_SITUA = Request.UserHostAddress;
                }

                //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
                switch (tbs379.EntityState)
                {
                    case EntityState.Added:
                    case EntityState.Detached:
                        tbs379.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs379.DT_CADAS = DateTime.Now;
                        tbs379.IP_CADAS = Request.UserHostAddress;
                        tbs379.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        break;
                }

                CurrentPadraoCadastros.CurrentEntity = tbs379;
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível realizar esta operação " + ex.Message);
            }
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            try
            {
                TBS379_RESTR_ATEND tbs379 = RetornaEntidade();

                if (tbs379 != null)
                {
                    txtCodigo.Text = tbs379.CO_RESTR;
                    txtNome.Text = tbs379.NM_RESTR;
                    txtDescricao.Text = tbs379.DE_RESTR;
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao caregar  formulário" + " - " + ex.Message);
                return;
            }

        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS379_RESTR_ATEND</returns>
        private TBS379_RESTR_ATEND RetornaEntidade()
        {

            TBS379_RESTR_ATEND tb251 = TBS379_RESTR_ATEND.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb251 == null) ? new TBS379_RESTR_ATEND() : tb251;
        }


        #endregion
    }
}