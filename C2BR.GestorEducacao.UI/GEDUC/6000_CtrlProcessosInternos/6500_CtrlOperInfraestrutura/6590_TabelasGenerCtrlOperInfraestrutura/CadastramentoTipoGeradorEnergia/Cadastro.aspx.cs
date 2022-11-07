//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: INFRAESTRUTURA  DE ENGENHARIA
// SUBMÓDULO: TABELAS DE APOIO - MANUTENÇÃO DE INFRA
// OBJETIVO:  CADASTRAMENTO DO TIPO DE GERADOR DE ENERGIA.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.CadastramentoTipoGeradorEnergia
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
            TB185_GERAD_ENERG tb185 = RetornaEntidade();

            tb185.DE_GERAD_ENERG = txtDescricao.Text;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb185.CO_SIGLA_GERAD_ENERG = txtSigla.Text.Trim().ToUpper();

            CurrentPadraoCadastros.CurrentEntity = tb185;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB185_GERAD_ENERG tb185 = RetornaEntidade();

            if (tb185 != null)
            {
                txtSigla.Text = tb185.CO_SIGLA_GERAD_ENERG.ToString();
                txtDescricao.Text = tb185.DE_GERAD_ENERG;  
            }  
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB185_GERAD_ENERG</returns>
        private TB185_GERAD_ENERG RetornaEntidade()
        {
            TB185_GERAD_ENERG tb185 = TB185_GERAD_ENERG.RetornaPeloCodigo(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb185 == null) ? new TB185_GERAD_ENERG() : tb185;
        }
        #endregion
    }
}
