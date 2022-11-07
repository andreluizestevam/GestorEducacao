//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DOTAÇÃO ORÇAMENTÁRIA
// OBJETIVO: CADASTRAMENTO DE ORIGEM FINANCEIRA DOTAÇÃO ORÇAMENTÁRIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1133_CadastramentoOrigemFinanceira
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
            if (Page.IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB304_ORIGE_FINAN tb304 = RetornaEntidade();

            if (tb304 == null)
            {
                tb304 = new TB304_ORIGE_FINAN();
                tb304.DT_CADAS_ORIGE_FINAN = DateTime.Now;
            }

            tb304.DE_ORIGE_FINAN = txtDescricaoOrigeFinan.Text;
            tb304.SIGLA_ORIGE_FINAN = txtSiglaOrigeFinan.Text.ToUpper();
            tb304.DT_SITUA_ORIGE_FINAN = DateTime.Now;
            tb304.CO_SITUA_ORIGE_FINAN = ddlSituacao.SelectedValue;
            tb304.NR_CONTR_ORIGE_FINAN = txtNumContr.Text != "" ? txtNumContr.Text : null;
            tb304.DE_OBS_ORIGE_FINAN = txtObser.Text != "" ? txtObser.Text : null;

            CurrentPadraoCadastros.CurrentEntity = tb304;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB304_ORIGE_FINAN tb304 = RetornaEntidade();

            if (tb304 != null)
            {
                txtDescricaoOrigeFinan.Text = tb304.DE_ORIGE_FINAN;
                txtSiglaOrigeFinan.Text = tb304.SIGLA_ORIGE_FINAN;
                txtNumContr.Text = tb304.NR_CONTR_ORIGE_FINAN != null ? tb304.NR_CONTR_ORIGE_FINAN : "";
                txtObser.Text = tb304.DE_OBS_ORIGE_FINAN != null ? tb304.DE_OBS_ORIGE_FINAN : "";                
                txtDtSituacao.Text = tb304.DT_SITUA_ORIGE_FINAN.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb304.CO_SITUA_ORIGE_FINAN;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB304_ORIGE_FINAN</returns>
        private TB304_ORIGE_FINAN RetornaEntidade()
        {
            return TB304_ORIGE_FINAN.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}