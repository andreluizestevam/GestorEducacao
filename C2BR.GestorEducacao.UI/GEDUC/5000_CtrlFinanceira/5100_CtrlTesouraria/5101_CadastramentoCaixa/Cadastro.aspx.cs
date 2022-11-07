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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5101_CadastramentoCaixa
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
                TB113_PARAM_CAIXA tb113 = RetornaEntidade();

                if (tb113 == null)
                {
                    tb113 = new TB113_PARAM_CAIXA();
                    tb113.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                }

                tb113.DE_CAIXA = txtNomeCaixa.Text;
                tb113.CO_SIGLA_CAIXA = txtSiglaCaixa.Text.ToUpper();
                tb113.CO_FLAG_USO_CAIXA = ddlUsoCaixa.SelectedValue;
                tb113.DT_CADAS_CAIXA = DateTime.Parse(txtDataSituacao.Text);
                tb113.CO_SITU_CAIXA = ddlSituacao.SelectedValue;

                CurrentPadraoCadastros.CurrentEntity = tb113;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB113_PARAM_CAIXA tb113 = RetornaEntidade();

            if (tb113 != null)
            {
                txtNomeCaixa.Text = tb113.DE_CAIXA;
                txtSiglaCaixa.Text = tb113.CO_SIGLA_CAIXA;
                ddlUsoCaixa.Text = tb113.CO_FLAG_USO_CAIXA;
                ddlSituacao.SelectedValue = tb113.CO_SITU_CAIXA;
                txtDataSituacao.Text = tb113.DT_CADAS_CAIXA.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB113_PARAM_CAIXA</returns>
        private TB113_PARAM_CAIXA RetornaEntidade()
        {
            return TB113_PARAM_CAIXA.RetornaPelaChavePrimaria(int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoEmp) != null ? QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoEmp) : "0"), int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null ? QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) : "0"));
        }
        #endregion
    }
}