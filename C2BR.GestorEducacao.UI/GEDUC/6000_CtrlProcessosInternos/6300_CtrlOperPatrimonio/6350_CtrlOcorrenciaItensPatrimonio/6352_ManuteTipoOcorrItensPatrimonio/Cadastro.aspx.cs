//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: REGISTRO DE OCORRÊNCIA DE ITENS DE PATRIMÔNIO
// OBJETIVO: MANUTENÇÃO DO TIPO DE OCORRÊNCIA DE ITENS DE PATRIMÔNIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6350_CtrlOcorrenciaItensPatrimonio.F6352_ManuteTipoOcorrItensPatrimonio
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
            TB229_TIPO_OCOR_PATRIMONIO tb229 = RetornaEntidade();

            tb229.DE_TIPO_OCORR = txtDescricao.Text;

            CurrentPadraoCadastros.CurrentEntity = tb229;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB229_TIPO_OCOR_PATRIMONIO tb229 = RetornaEntidade();

            if (tb229 != null)
                txtDescricao.Text = tb229.DE_TIPO_OCORR;
        }

//====> 
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB229_TIPO_OCOR_PATRIMONIO</returns>
        private TB229_TIPO_OCOR_PATRIMONIO RetornaEntidade()
        {
            TB229_TIPO_OCOR_PATRIMONIO tb229 = TB229_TIPO_OCOR_PATRIMONIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb229 == null) ? new TB229_TIPO_OCOR_PATRIMONIO() : tb229;
        }
        #endregion
    }
}
