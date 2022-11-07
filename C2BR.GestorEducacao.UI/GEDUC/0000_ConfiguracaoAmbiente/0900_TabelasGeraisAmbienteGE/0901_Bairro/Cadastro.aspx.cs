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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0901_Bairro
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

            CarregaUfs();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) 
            {
                ddlUfCB.Enabled = ddlCidadeCB.Enabled = true;
                ddlUfCB.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                CarregaCidades();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB905_BAIRRO tb905 = RetornaEntidade();

            if (tb905.CO_BAIRRO == 0) 
            {
                tb905.CO_CIDADE = int.Parse(ddlCidadeCB.SelectedValue);
                tb905.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(tb905.CO_CIDADE);
            }
            tb905.CO_UF = ddlUfCB.SelectedValue;
            tb905.NO_BAIRRO = txtBairroCB.Text;

            CurrentPadraoCadastros.CurrentEntity = tb905;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB905_BAIRRO tb905 = RetornaEntidade();

            if (tb905 != null)
            {
                ddlUfCB.SelectedValue = tb905.CO_UF;
                CarregaCidades();
                ddlCidadeCB.SelectedValue = tb905.CO_CIDADE.ToString();
                txtBairroCB.Text = tb905.NO_BAIRRO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB905_BAIRRO</returns>
        private TB905_BAIRRO RetornaEntidade()
        {
            TB905_BAIRRO tb905 = TB905_BAIRRO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb905 == null) ? new TB905_BAIRRO() : tb905;
        }

        #endregion        
        
        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUfs()
        {
            ddlUfCB.DataSource = TB74_UF.RetornaTodosRegistros();
            ddlUfCB.DataTextField = "CODUF";
            ddlUfCB.DataValueField = "CODUF";
            ddlUfCB.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidadeCB.DataSource = (from tb904 in TB904_CIDADE.RetornaPeloUF(ddlUfCB.SelectedValue)
                                      select new { tb904.CO_CIDADE, tb904.NO_CIDADE });

            ddlCidadeCB.DataTextField = "NO_CIDADE";
            ddlCidadeCB.DataValueField = "CO_CIDADE";
            ddlCidadeCB.DataBind();
        }
        #endregion

        protected void ddlUfCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }
    }
}