//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: TIPOS DE BOLSA ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2908_AgrupadorBolsa
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                txtDtCadas.Text = txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB317_AGRUP_BOLSA tb317 = RetornaEntidade();

            tb317.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb317.NO_AGRUP_BOLSA = txtNome.Text;
            tb317.DE_AGRUP_BOLSA = txtDescricao.Text != "" ? txtDescricao.Text : null;
            tb317.CO_REFER_AGRUP_BOLSA = txtCodRef.Text != "" ? txtCodRef.Text : null;

            tb317.DT_SITUA_AGRUP_BOLSA = DateTime.Now;
            tb317.CO_SITUA_AGRUP_BOLSA = ddlSituacao.SelectedValue;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb317.DT_CADAS_AGRUP_BOLSA = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb317;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB317_AGRUP_BOLSA tb317 = RetornaEntidade();

            if (tb317 != null)
            {
                txtNome.Text = tb317.NO_AGRUP_BOLSA;
                txtDescricao.Text = tb317.DE_AGRUP_BOLSA;
                txtCodRef.Text = tb317.CO_REFER_AGRUP_BOLSA;
                txtDtCadas.Text = tb317.DT_CADAS_AGRUP_BOLSA.ToString("dd/MM/yyyy");
                txtDtSituacao.Text = tb317.DT_SITUA_AGRUP_BOLSA.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb317.CO_SITUA_AGRUP_BOLSA;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB317_AGRUP_BOLSA</returns>
        private TB317_AGRUP_BOLSA RetornaEntidade()
        {
            TB317_AGRUP_BOLSA tb317 = TB317_AGRUP_BOLSA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb317 == null) ? new TB317_AGRUP_BOLSA() : tb317;
        }
        #endregion
    }
}