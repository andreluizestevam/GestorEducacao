using System;
using System.Web.UI;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._TiposDocumentos
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

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB121_TIPO_DOC_MATRICULA tb121 = RetornaEntidade();

            if (tb121 == null)
                tb121 = new TB121_TIPO_DOC_MATRICULA();

            tb121.DE_TP_DOC_MAT = txtDescricao.Text;
            tb121.SIG_TP_DOC_MAT = txtSigla.Text.ToUpper().Trim();

            CurrentPadraoCadastros.CurrentEntity = tb121;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB121_TIPO_DOC_MATRICULA tb121 = RetornaEntidade();

            if (tb121 != null)
            {
                txtDescricao.Text = tb121.DE_TP_DOC_MAT;
                txtSigla.Text = tb121.SIG_TP_DOC_MAT.Trim();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB121_TIPO_DOC_MATRICULA</returns>
        private TB121_TIPO_DOC_MATRICULA RetornaEntidade()
        {
            return TB121_TIPO_DOC_MATRICULA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion        
    }
}
