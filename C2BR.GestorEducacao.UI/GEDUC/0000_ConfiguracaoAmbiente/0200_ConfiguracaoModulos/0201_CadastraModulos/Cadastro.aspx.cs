//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRA MÓDULOS/FUNCIONALIDADES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections;
using System.Configuration;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0201_CadastraModulos
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
                CarregaModulos();            
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            ADMMODULO admModulo = RetornaEntidade();

            if (admModulo == null)
                admModulo = new ADMMODULO();

            admModulo.nomModulo = txtNomeModCMF.Text;
            admModulo.nomItemMenu = txtNomeItemMenuModCMF.Text;
            admModulo.ADMMODULO2 = ddlModuloPaiCMF.SelectedValue != "" ? ADMMODULO.RetornaPelaChavePrimaria(int.Parse(ddlModuloPaiCMF.SelectedValue)) : null;
            admModulo.nomDescricao = txtDescricaoModCMF.Text;
            admModulo.numOrdemMenu = int.Parse(txtOrdemMenuModCMF.Text);
            admModulo.flaTipoItemSubMenu = ddlTipoItemSubmenuModCMF.SelectedValue != "" ? ddlTipoItemSubmenuModCMF.SelectedValue.ToUpper() : null;
            admModulo.flaStatus = ddlStatusModCMF.SelectedValue.ToUpper();
            admModulo.imgIcone = txtIconeModCMF.Text;

            CurrentPadraoCadastros.CurrentEntity = admModulo;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            var admmModulo = RetornaEntidade();

            if (admmModulo != null)
            {
                if (!admmModulo.ADMMODULO2Reference.IsLoaded)
                {
                    admmModulo.ADMMODULO2Reference.Load();
                }

                txtCodigoModCMF.Text = admmModulo.ideAdmModulo.ToString();
                txtNomeModCMF.Text = admmModulo.nomModulo;
                txtNomeItemMenuModCMF.Text = admmModulo.nomItemMenu;
                //admmModulo.ADMMODULO2Reference.Load();
                ddlModuloPaiCMF.SelectedValue = admmModulo.ADMMODULO2 != null ? admmModulo.ADMMODULO2.ideAdmModulo.ToString() : "";
                txtDescricaoModCMF.Text = admmModulo.nomDescricao;
                txtOrdemMenuModCMF.Text = admmModulo.numOrdemMenu.ToString();
                ddlTipoItemSubmenuModCMF.SelectedValue = admmModulo.flaTipoItemSubMenu;
                ddlStatusModCMF.SelectedValue = admmModulo.flaStatus;
                txtIconeModCMF.Text = admmModulo.imgIcone;
            }            
        }

//====> 
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade ADMMODULO</returns>
        private ADMMODULO RetornaEntidade()
        {
            ADMMODULO admModulo = ADMMODULO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (admModulo == null) ? new ADMMODULO() : admModulo;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de módulos
        /// </summary>
        private void CarregaModulos()
        {
            ddlModuloPaiCMF.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                          where (admModulo.flaTipoItemSubMenu == "ATM" || admModulo.flaTipoItemSubMenu == "LST")
                                          && admModulo.ADMMODULO2 == null
                                          select new { admModulo.nomModulo, admModulo.ideAdmModulo }).OrderBy( a => a.nomModulo );

            ddlModuloPaiCMF.DataTextField = "nomModulo";
            ddlModuloPaiCMF.DataValueField = "ideAdmModulo";
            ddlModuloPaiCMF.DataBind();

            ddlModuloPaiCMF.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}
