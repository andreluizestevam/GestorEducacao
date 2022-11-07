//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1903_CursosFormacaoEspecializacao
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
            TB100_ESPECIALIZACAO tb100 = RetornaEntidade();

            tb100.DE_ESPEC = txtDescricao.Text;
            tb100.NO_SIGLA_ESPEC = txtSigla.Text.Trim().ToUpper();
            tb100.TP_ESPEC = ddlTipo.SelectedValue;
            tb100.QT_PONTU = int.Parse(txtPontuacao.Text);
            tb100.FLA_PROMO = ddlPromocao.SelectedValue;
            
            CurrentPadraoCadastros.CurrentEntity = tb100;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB100_ESPECIALIZACAO tb100 = RetornaEntidade();

            if (tb100 != null)
            {
                txtCodigo.Text = tb100.CO_ESPEC.ToString();
                txtDescricao.Text = tb100.DE_ESPEC;
                txtSigla.Text = tb100.NO_SIGLA_ESPEC;
                ddlTipo.SelectedValue = tb100.TP_ESPEC;
                txtPontuacao.Text = tb100.QT_PONTU.ToString();
                ddlPromocao.SelectedValue = tb100.FLA_PROMO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB100_ESPECIALIZACAO</returns>
        private TB100_ESPECIALIZACAO RetornaEntidade()
        {
            TB100_ESPECIALIZACAO tb100 = TB100_ESPECIALIZACAO.RetornaPeloCoEspec(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb100 == null) ? new TB100_ESPECIALIZACAO() : tb100;
        }
        #endregion        
    }
}