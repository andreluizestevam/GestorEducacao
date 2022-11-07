//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *****
// SUBMÓDULO: *****
// OBJETIVO: CADASTRAMENTO DE GRUPOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1131_CadastroGrupoPlanejExecu
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
            if (!IsPostBack)
            {
                if (CurrentPadraoCadastros.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB260_GRUPO tb260 = RetornaEntidade();

            tb260.CO_GRUPO = txtCodigo.Text;
            tb260.NOM_GRUPO = txtNome.Text;
            tb260.TP_GRUPO = "O";
            tb260.DT_SITUA_GRUPO = DateTime.Parse(txtDtSituacao.Text);
            tb260.FL_SITUA_GRUPO = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb260;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB260_GRUPO tb260 = RetornaEntidade();

            if (tb260 != null)
            {
                txtCodigo.Text = tb260.CO_GRUPO;
                txtNome.Text = tb260.NOM_GRUPO;
                txtDtSituacao.Text = tb260.DT_SITUA_GRUPO.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb260.FL_SITUA_GRUPO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB260_GRUPO</returns>
        private TB260_GRUPO RetornaEntidade()
        {
            TB260_GRUPO tb260 = TB260_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb260 == null) ? new TB260_GRUPO() : tb260;
        }
        #endregion        
    }
}