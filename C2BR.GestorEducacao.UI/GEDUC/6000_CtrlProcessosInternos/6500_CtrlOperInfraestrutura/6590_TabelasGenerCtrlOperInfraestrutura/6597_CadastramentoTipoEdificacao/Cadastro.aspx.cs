﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: INFRAESTRUTURA  DE ENGENHARIA
// SUBMÓDULO: TABELAS DE APOIO - MANUTENÇÃO DE INFRA
// OBJETIVO: CADASTRAMENTO DO TIPO DE EDIFICAÇÃO.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.F6597_CadastramentoTipoEdificacao
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                txtSigla.Enabled = false;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB179_TIPO_EDIFI tb179 = RetornaEntidade();

            tb179.DE_TIPO_EDIFI = txtDescricao.Text;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb179.CO_SIGLA_TIPO_EDIFI = txtSigla.Text.Trim().ToUpper();

            CurrentPadraoCadastros.CurrentEntity = tb179;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB179_TIPO_EDIFI tb179 = RetornaEntidade();

            if (tb179 != null)
            {
                txtSigla.Text = tb179.CO_SIGLA_TIPO_EDIFI.ToString();
                txtDescricao.Text = tb179.DE_TIPO_EDIFI; 
            }       
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB179_TIPO_EDIFI</returns>
        private TB179_TIPO_EDIFI RetornaEntidade()
        {
            TB179_TIPO_EDIFI tb179 = TB179_TIPO_EDIFI.RetornaPeloCoTipoEdifi(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb179 == null) ? new TB179_TIPO_EDIFI() : tb179;
        }
        #endregion        
    }
}