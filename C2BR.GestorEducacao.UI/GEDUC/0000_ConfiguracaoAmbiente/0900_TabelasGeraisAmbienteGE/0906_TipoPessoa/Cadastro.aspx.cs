//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: TIPO DE PESSOA
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
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0906_TipoPessoa
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
        private void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB237_TIPO_PESSOA tb237 = RetornaEntidade();

            tb237.CD_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            tb237.CO_TIPO_PESSOA = txtCodigoTP.Text;
            tb237.NM_TIPO_PESSOA = txtNomeTP.Text;
            tb237.CO_SITUACAO = ddlSituacaoTP.SelectedValue;
            tb237.DT_SITUACAO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb237;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario() 
        {
            TB237_TIPO_PESSOA tb237 = RetornaEntidade();

            if (tb237 != null)
            {
                txtCodigoTP.Text = tb237.CO_TIPO_PESSOA;
                txtNomeTP.Text = tb237.NM_TIPO_PESSOA;
                ddlSituacaoTP.SelectedValue = tb237.CO_SITUACAO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB237_TIPO_PESSOA</returns>
        private TB237_TIPO_PESSOA RetornaEntidade() 
        {
            TB237_TIPO_PESSOA tb237 = TB237_TIPO_PESSOA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb237 == null) ? new TB237_TIPO_PESSOA() : tb237;
        }
        #endregion
    }
}