//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE PESQUISAS INSITUCIONAIS
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
using System.Data.Sql;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1332_CadastramentoPesquisaInst
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
                CarregaTipo();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coTipoAval = ddlTipAva.SelectedValue != "" ? int.Parse(ddlTipAva.SelectedValue) : 0;

            TB72_TIT_QUES_AVAL tb72 = RetornaEntidade();

            if (tb72 == null)
            {
                tb72 = new TB72_TIT_QUES_AVAL();
                tb72.TB73_TIPO_AVAL = TB73_TIPO_AVAL.RetornaPelaChavePrimaria(coTipoAval);
            }

            tb72.NO_TITU_AVAL = txtItemAva.Text;

            CurrentPadraoCadastros.CurrentEntity = tb72;
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB72_TIT_QUES_AVAL tb72 = RetornaEntidade();

            if (tb72 != null)
            {
                ddlTipAva.SelectedValue = tb72.CO_TIPO_AVAL.ToString();
                txtItemAva.Text = tb72.NO_TITU_AVAL;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB72_TIT_QUES_AVAL</returns>
        private TB72_TIT_QUES_AVAL RetornaEntidade()
        {
            return TB72_TIT_QUES_AVAL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Avaliação
        /// </summary>
        private void CarregaTipo()
        {
            ddlTipAva.DataSource = TB73_TIPO_AVAL.RetornaTodosRegistros();

            ddlTipAva.DataTextField = "NO_TIPO_AVAL";
            ddlTipAva.DataValueField = "CO_TIPO_AVAL";
            ddlTipAva.DataBind();

            ddlTipAva.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}
