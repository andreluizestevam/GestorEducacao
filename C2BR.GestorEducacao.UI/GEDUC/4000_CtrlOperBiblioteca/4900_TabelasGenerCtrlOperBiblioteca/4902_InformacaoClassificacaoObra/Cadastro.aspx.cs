//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL BIBILOTECA
// OBJETIVO: INFORMAÇÕES/DADOS - CLASSIFICAÇÃO DA OBRA.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4900_TabelasGenerCtrlOperBiblioteca.F4902_InformacaoClassificacaoObra
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
                CarregaAreaConhecimento();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {            
            TB32_CLASSIF_ACER tb32 = RetornaEntidade();

            tb32.NO_CLAS_ACER = txtDescricao.Text;
            tb32.TB31_AREA_CONHEC = TB31_AREA_CONHEC.RetornaPelaChavePrimaria(int.Parse(ddlAreaConhe.SelectedValue));

            CurrentPadraoCadastros.CurrentEntity = tb32;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB32_CLASSIF_ACER tb32 = RetornaEntidade();

            if (tb32 != null)
            {
                txtDescricao.Text = tb32.NO_CLAS_ACER;
                ddlAreaConhe.SelectedValue = tb32.TB31_AREA_CONHEC.CO_AREACON.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB32_CLASSIF_ACER</returns>
        private TB32_CLASSIF_ACER RetornaEntidade()
        {
            TB32_CLASSIF_ACER tb32 = TB32_CLASSIF_ACER.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb32 == null) ? new TB32_CLASSIF_ACER() : tb32;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Área de Conhecimento
        /// </summary>
        private void CarregaAreaConhecimento()
        {
            ddlAreaConhe.DataSource = TB31_AREA_CONHEC.RetornaTodosRegistros().Where( d => d.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO );

            ddlAreaConhe.DataTextField = "NO_AREACON";
            ddlAreaConhe.DataValueField = "CO_AREACON";
            ddlAreaConhe.DataBind();            
        }
        
        #endregion
    }
}