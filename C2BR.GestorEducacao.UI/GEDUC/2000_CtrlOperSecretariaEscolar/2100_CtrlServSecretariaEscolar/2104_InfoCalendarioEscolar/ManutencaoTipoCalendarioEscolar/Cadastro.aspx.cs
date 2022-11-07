//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: INFORMAÇÕES DE CALENDÁRIO ESCOLAR
// OBJETIVO: MANUTENÇÃO DE TIPO DE CALENDARIO ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoTipoCalendarioEscolar
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
            if (IsPostBack) return;
            
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                txtDataSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB152_CALENDARIO_TIPO tb152 = RetornaEntidade();

            tb152.CAT_NOME_TIPO_CALEN = txtDescricao.Text;
            tb152.CAT_SIGLA_TIPO_CALEN = txtSigla.Text.ToUpper();
            tb152.CAT_SITUA_TIPO_CALEN = ddlSituacao.SelectedValue;
            tb152.CAT_DATA_SITUA_TIPO_CALEN = DateTime.Parse(txtDataSituacao.Text);
            
            CurrentPadraoCadastros.CurrentEntity = tb152;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB152_CALENDARIO_TIPO tb152 = RetornaEntidade();

            if (tb152 != null)
            {
                txtDescricao.Text = tb152.CAT_NOME_TIPO_CALEN;
                txtSigla.Text = tb152.CAT_SIGLA_TIPO_CALEN;
                ddlSituacao.SelectedValue = tb152.CAT_SITUA_TIPO_CALEN;
                txtDataSituacao.Text = tb152.CAT_DATA_SITUA_TIPO_CALEN.ToString("dd/MM/yyyy");                
            }            
        }

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        /// <returns>Entidade TB152_CALENDARIO_TIPO</returns>
        private TB152_CALENDARIO_TIPO RetornaEntidade()
        {
            TB152_CALENDARIO_TIPO tb152 = TB152_CALENDARIO_TIPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb152 == null) ? new TB152_CALENDARIO_TIPO() : tb152;
        }
        #endregion        
    }
}
