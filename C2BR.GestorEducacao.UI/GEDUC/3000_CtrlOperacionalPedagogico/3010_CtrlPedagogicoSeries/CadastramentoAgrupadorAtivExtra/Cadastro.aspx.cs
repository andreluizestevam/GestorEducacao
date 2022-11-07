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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.CadastramentoAgrupadorAtivExtra
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
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB318_AGRUP_ATIVEXTRA tb318 = RetornaEntidade();

            tb318.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb318.DE_AGRUP_ATIVEXTRA = txtNome.Text;
            tb318.CO_SIGLA_AGRUP_ATIVEXTRA = txtCodRef.Text != "" ? txtCodRef.Text : null;

            tb318.DT_SITUA_AGRUP_ATIVEXTRA = DateTime.Now;
            tb318.CO_SITUA_AGRUP_ATIVEXTRA = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb318;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB318_AGRUP_ATIVEXTRA tb318 = RetornaEntidade();

            if (tb318 != null)
            {                
                txtNome.Text = tb318.DE_AGRUP_ATIVEXTRA;
                txtCodRef.Text = tb318.CO_SIGLA_AGRUP_ATIVEXTRA;
                txtDtSituacao.Text = tb318.DT_SITUA_AGRUP_ATIVEXTRA.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb318.CO_SITUA_AGRUP_ATIVEXTRA;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB318_AGRUP_ATIVEXTRA</returns>
        private TB318_AGRUP_ATIVEXTRA RetornaEntidade()
        {
            TB318_AGRUP_ATIVEXTRA tb318 = TB318_AGRUP_ATIVEXTRA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb318 == null) ? new TB318_AGRUP_ATIVEXTRA() : tb318;
        }
        #endregion
    }
}