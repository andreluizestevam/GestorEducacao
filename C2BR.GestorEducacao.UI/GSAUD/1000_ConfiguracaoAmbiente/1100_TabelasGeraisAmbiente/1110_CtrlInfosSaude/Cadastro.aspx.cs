using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Data;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1110_CtrlInfosSaude
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

        //===> Chamada do método de preenchimento do formulário da funcionalidade ===>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //===> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar ===>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
          
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o nome");
                return;
            }
            if (string.IsNullOrEmpty(txtSigla.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Sigla");
                return;
            }

            try
            {
                TBS382_INFOS_GERAIS tbs382 = RetornaEntidade();
                tbs382.NM_SIGLA_INFOS_GERAIS =  txtSigla.Text;
                tbs382.NM_INFOS_GERAIS = txtNome.Text;
                tbs382.DE_INFOS_GERAIS = txtDescricao.Text;

                if (hidSituacao.Value != ddlSituacao.SelectedValue)
                {
                    tbs382.CO_SITUA = ddlSituacao.SelectedValue;
                    tbs382.DT_SITUA = DateTime.Now;
                    tbs382.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs382.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs382.IP_SITUA = Request.UserHostAddress;
                }

                //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
                switch (tbs382.EntityState)
                {
                    case EntityState.Added:
                    case EntityState.Detached:
                        tbs382.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs382.DT_CADAS = DateTime.Now;
                        tbs382.IP_CADAS = Request.UserHostAddress;
                        tbs382.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        break;
                }

                CurrentPadraoCadastros.CurrentEntity = tbs382;
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
                TBS382_INFOS_GERAIS tbs382 = RetornaEntidade();

                if (tbs382 != null)
                {
                    txtSigla.Text = tbs382.NM_SIGLA_INFOS_GERAIS;
                    txtNome.Text = tbs382.NM_INFOS_GERAIS;
                    txtDescricao.Text = tbs382.DE_INFOS_GERAIS;
                    ddlSituacao.SelectedValue = tbs382.CO_SITUA.ToString();
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
        private TBS382_INFOS_GERAIS RetornaEntidade()
        {

            TBS382_INFOS_GERAIS tb382 = TBS382_INFOS_GERAIS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb382 == null) ? new TBS382_INFOS_GERAIS() : tb382;
        }

        #endregion
    }
}