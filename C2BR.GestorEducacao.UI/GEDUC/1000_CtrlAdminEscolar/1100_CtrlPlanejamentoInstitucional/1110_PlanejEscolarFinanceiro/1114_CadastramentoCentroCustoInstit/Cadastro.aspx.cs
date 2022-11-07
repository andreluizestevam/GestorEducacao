//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DOS CENTROS DE CUSTO DA INSTITUIÇÃO
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1114_CadastramentoCentroCustoInstit
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

            CarregaDepto();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coDepto = int.Parse(ddlDepartamento.SelectedValue);

            TB099_CENTRO_CUSTO tb099 = RetornaEntidade();

            tb099.NU_CTA_CENT_CUSTO = txtNumConta.Text;
            tb099.DE_CENT_CUSTO = txtDescricao.Text;
            tb099.SIG_CENT_CUSTO = txtSigla.Text.Trim().ToUpper();
            tb099.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDepto);
            tb099.CO_SITU_CC = ddlSituacao.SelectedValue;
            tb099.DT_SITU_CC = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb099;            
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB099_CENTRO_CUSTO tb099 = RetornaEntidade();

            if (tb099 != null)
            {
                txtNumConta.Text = tb099.NU_CTA_CENT_CUSTO;
                txtDescricao.Text = tb099.DE_CENT_CUSTO;
                txtSigla.Text = tb099.SIG_CENT_CUSTO;
                CarregaDepto();
                ddlDepartamento.SelectedValue = (from lTb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                                                 where lTb099.CO_CENT_CUSTO == tb099.CO_CENT_CUSTO
                                                 select new { lTb099.TB14_DEPTO.CO_DEPTO }).FirstOrDefault().CO_DEPTO.ToString();

                ddlSituacao.SelectedValue = tb099.CO_SITU_CC;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB099_CENTRO_CUSTO</returns>
        private TB099_CENTRO_CUSTO RetornaEntidade()
        {
            TB099_CENTRO_CUSTO tb099 = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb099 == null) ? new TB099_CENTRO_CUSTO() : tb099;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Departamento
        /// </summary>
        private void CarregaDepto()
        {
            ddlDepartamento.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                          where tb14.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                          select new { tb14.CO_DEPTO, tb14.NO_DEPTO });

            ddlDepartamento.DataTextField = "NO_DEPTO";
            ddlDepartamento.DataValueField = "CO_DEPTO";
            ddlDepartamento.DataBind();

            ddlDepartamento.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}
