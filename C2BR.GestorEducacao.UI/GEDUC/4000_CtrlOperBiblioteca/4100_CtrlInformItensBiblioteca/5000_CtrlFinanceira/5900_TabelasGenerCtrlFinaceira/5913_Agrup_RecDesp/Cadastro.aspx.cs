//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: CADASTRAMENTO DE CAIXAS
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
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5913_Agrup_RecDesp
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
            if (!Page.IsPostBack)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    ddlTpAgrupador.SelectedValue = "T";
                    ddlSituacao.SelectedValue = "A";
                    txtDataSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                CarregaFormulario();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                TB315_AGRUP_RECDESP tb315 = RetornaEntidade();

                if (tb315 == null)
                {
                    tb315 = new TB315_AGRUP_RECDESP();
                }

                tb315.DE_SITU_AGRUP_RECDESP = txtNomeAgrupador.Text;
                tb315.CO_AGRUP_RECDESP = txtSiglaAgrupador.Text.ToUpper();
                tb315.TP_AGRUP_RECDESP = ddlTpAgrupador.SelectedValue;
                tb315.DT_SITU_AGRUP_RECDESP = DateTime.Parse(txtDataSituacao.Text);
                tb315.CO_SITU_AGRUP_RECDESP = ddlSituacao.SelectedValue;

                CurrentPadraoCadastros.CurrentEntity = tb315;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB315_AGRUP_RECDESP tb315 = RetornaEntidade();

            if (tb315 != null)
            {
                txtNomeAgrupador.Text = tb315.DE_SITU_AGRUP_RECDESP;
                txtSiglaAgrupador.Text = tb315.CO_AGRUP_RECDESP;
                ddlTpAgrupador.Text = tb315.TP_AGRUP_RECDESP;
                ddlSituacao.SelectedValue = tb315.CO_SITU_AGRUP_RECDESP;
                txtDataSituacao.Text = tb315.DT_SITU_AGRUP_RECDESP.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB113_PARAM_CAIXA</returns>
        private TB315_AGRUP_RECDESP RetornaEntidade()
        {
            return TB315_AGRUP_RECDESP.RetornaPelaChavePrimaria(int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null ? QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) : "0"));
        }
        #endregion
    }
}