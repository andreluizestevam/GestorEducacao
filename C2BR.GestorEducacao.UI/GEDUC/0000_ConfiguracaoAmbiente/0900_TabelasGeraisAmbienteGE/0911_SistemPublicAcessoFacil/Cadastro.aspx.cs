//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0911_SistemPublicAcessoFacil
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
            if (IsPostBack)
                return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) 
            {
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB298_CONEXAO_WEB tb298 = RetornaEntidade();

            tb298.TP_CONEX_WEB = ddlTipoConexao.SelectedValue;
            tb298.CO_ORGAO_CONEX_WEB = txtOrgao.Text;
            tb298.NO_TITULO_CONEX_WEB = txtSisteServi.Text;
            tb298.DE_OBJETO_CONEX_WEB = txtDescricao.Text;
            tb298.DE_URL_CONEX_WEB = txtURLConexao.Text;
            tb298.CO_STAT_CONEX_WEB = ddlStatus.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb298;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB298_CONEXAO_WEB tb298 = RetornaEntidade();

            if (tb298 != null)
            {
                ddlTipoConexao.SelectedValue = tb298.TP_CONEX_WEB;
                txtOrgao.Text = tb298.CO_ORGAO_CONEX_WEB;
                txtSisteServi.Text = tb298.NO_TITULO_CONEX_WEB;
                txtDescricao.Text = tb298.DE_OBJETO_CONEX_WEB;
                txtURLConexao.Text = tb298.DE_URL_CONEX_WEB;
                ddlStatus.SelectedValue = tb298.CO_STAT_CONEX_WEB;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB298_CONEXAO_WEB</returns>
        private TB298_CONEXAO_WEB RetornaEntidade()
        {
            TB298_CONEXAO_WEB tb298 = TB298_CONEXAO_WEB.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb298 == null) ? new TB298_CONEXAO_WEB() : tb298;
        }

        #endregion       
    }
}