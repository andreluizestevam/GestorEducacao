//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: INFRAESTRUTURA  DE ENGENHARIA
// SUBMÓDULO: TABELAS DE APOIO - MANUTENÇÃO DE INFRA
// OBJETIVO:  CADASTRAMENTO DO SISTEMA DE AQUECIMENTO.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.CadastramentoSistemaAquecimento
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
            TB186_SISTE_AQUEC tb186 = RetornaEntidade();

            tb186.DE_SISTE_AQUEC = txtDescricao.Text;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb186.CO_SIGLA_SISTE_AQUEC = txtSigla.Text.Trim().ToUpper();

            CurrentPadraoCadastros.CurrentEntity = tb186;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB186_SISTE_AQUEC tb186 = RetornaEntidade();

            if (tb186 != null)
            {
                txtSigla.Text = tb186.CO_SIGLA_SISTE_AQUEC.ToString();
                txtDescricao.Text = tb186.DE_SISTE_AQUEC;  
            }                      
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB186_SISTE_AQUEC</returns>
        private TB186_SISTE_AQUEC RetornaEntidade()
        {
            TB186_SISTE_AQUEC tb186 = TB186_SISTE_AQUEC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb186 == null) ? new TB186_SISTE_AQUEC() : tb186;
        }
        #endregion        
    }
}
