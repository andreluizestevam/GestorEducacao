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

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2900_TabelasGenerCtrlOperSecretaria._2910_GrupoItemSolicitacao
{
    public partial class Cadsatro : System.Web.UI.Page
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

        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB061_GRUPO_SOLIC tb061 = RetornaEntidade();

            tb061.NM_GRUPO_SOLIC = txtNome.Text;
            tb061.CO_GRUPO_SOLIC = txtCodigo.Text;
            tb061.DT_GRUPO_SOLIC = DateTime.Parse(txtData.Text);
            tb061.CO_SITUA_GRUPO_SOLIC = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb061;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB061_GRUPO_SOLIC tb061 = RetornaEntidade();
            if (tb061 != null)
            {
                txtNome.Text = tb061.NM_GRUPO_SOLIC;
                txtCodigo.Text = tb061.CO_GRUPO_SOLIC;
                txtData.Text = tb061.DT_GRUPO_SOLIC.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb061.CO_SITUA_GRUPO_SOLIC;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB061_GRUPO_SOLIC</returns>
        private TB061_GRUPO_SOLIC RetornaEntidade()
        {
            TB061_GRUPO_SOLIC obj = TB061_GRUPO_SOLIC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (obj == null) ? new TB061_GRUPO_SOLIC() : obj;
        }

        #endregion
    }
}