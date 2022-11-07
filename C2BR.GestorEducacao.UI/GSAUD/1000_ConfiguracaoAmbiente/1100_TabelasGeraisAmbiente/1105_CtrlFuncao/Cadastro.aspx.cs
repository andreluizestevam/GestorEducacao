using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Data;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlFuncao
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


            TBS366_FUNCAO_SIMPL tb366 = RetornaEntidade();

            tb366.NM_FUNCAO = txtNomeOper.Text;



            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSituacao.SelectedValue)
            {
                tb366.FL_SITUA = ddlSituacao.SelectedValue;
                tb366.DT_SITUA = DateTime.Now;
                tb366.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tb366.CO_COL_SITUA = LoginAuxili.CO_COL;
                tb366.IP_SITUA = Request.UserHostAddress;

            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tb366.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tb366.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tb366.DT_CADAS = DateTime.Now;
                    tb366.IP_CADAS = Request.UserHostAddress;
                    tb366.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tb366;
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TBS366_FUNCAO_SIMPL tb366 = RetornaEntidade();

            if (tb366 != null)
            {

                txtNomeOper.Text = tb366.NM_FUNCAO;
                ddlSituacao.SelectedValue = tb366.FL_SITUA;

            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB250_OPERA</returns>
        private TBS366_FUNCAO_SIMPL RetornaEntidade()
        {

            TBS366_FUNCAO_SIMPL tb366 = TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb366 == null) ? new TBS366_FUNCAO_SIMPL() : tb366;
        }

        #endregion
    }
}