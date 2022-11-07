//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: TIPO DE ENDEREÇO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0908_TipoEndereco
{
    public partial class Cadastro : System.Web.UI.Page 
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e) {
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
            TB238_TIPO_ENDERECO tb238 = RetornaEntidade();

            tb238.CD_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            tb238.CO_TIPO_ENDERECO = txtCodigoTE.Text;
            tb238.NM_TIPO_ENDERECO = txtNomeTE.Text;
            tb238.CO_SITUACAO = ddlSituacaoTE.SelectedValue;
            tb238.DT_SITUACAO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb238;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario() 
        {
            TB238_TIPO_ENDERECO tb238 = RetornaEntidade();

            if (tb238 != null)
            {
                txtCodigoTE.Text = tb238.CO_TIPO_ENDERECO;
                txtNomeTE.Text = tb238.NM_TIPO_ENDERECO;
                ddlSituacaoTE.SelectedValue = tb238.CO_SITUACAO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB238_TIPO_ENDERECO</returns>
        private TB238_TIPO_ENDERECO RetornaEntidade() 
        {
            TB238_TIPO_ENDERECO tb238 = TB238_TIPO_ENDERECO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb238 == null) ? new TB238_TIPO_ENDERECO() : tb238;
        }
        #endregion
    }
}