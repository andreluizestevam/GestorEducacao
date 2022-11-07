//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRA FUNCIONALIDADES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0202_CadastraFuncionalidades
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
                CarregaModulos();
                chkInforGerenFuncCFU.Attributes.Add("onClick", "javascript:__doPostBack('" + chkInforGerenFuncCFU.UniqueID + "','Select$0')");

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    HabilitaDadosGerenciais();            
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            ADMMODULO admModulo = RetornaEntidade();

            admModulo.ADMMODULO2 = ddlModuloPaiFuncCFU.SelectedValue != "" ? ADMMODULO.RetornaPelaChavePrimaria(int.Parse(ddlModuloPaiFuncCFU.SelectedValue)) : null;
            admModulo.nomModulo = txtNomeFuncCFU.Text;
            admModulo.nomItemMenu = txtNomeItemMenuFuncCFU.Text;
            admModulo.nomDescricao = txtDescricaoFuncCFU.Text;
            admModulo.nomURLModulo = txtNomUrlModuloFuncCFU.Text;
            admModulo.numOrdemMenu = int.Parse(txtOrdemMenuFuncCFU.Text);
            //admModulo.flaTipoItemSubMenu = ddlTipoItemSubmenuFuncCFU.SelectedValue != "" ? ddlTipoItemSubmenuFuncCFU.SelectedValue.ToUpper() : null;
            admModulo.flaTipoItemSubMenu = null;
            admModulo.CO_FLAG_TIPO_ICONE = ddlTipoIconeFuncCFU.SelectedValue != "" ? ddlTipoIconeFuncCFU.SelectedValue : null;
            admModulo.flaStatus = ddlStatusFuncCFU.SelectedValue;
            admModulo.imgIcone = txtIconeFuncCFU.Text;
            
//--------> Dados Gerenciais
            admModulo.CO_FLAG_INFOR_GEREN = chkInforGerenFuncCFU.Checked ? "S" : "N";
            admModulo.nomModulo_GEREN = txtNomeGerencialFuncCFU.Text;
            admModulo.nomDescricaoGEREN = txtDescGerenFuncCFU.Text;
            admModulo.CO_FLAG_TIPO_ICONE_GEREN = ddlTipoIconeGerenFuncCFU.SelectedValue != "" ? ddlTipoIconeGerenFuncCFU.SelectedValue : null;                        
//----------

            CurrentPadraoCadastros.CurrentEntity = admModulo;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            var admModulo = RetornaEntidade();

            if (admModulo != null)
            {
                admModulo.ADMMODULO2Reference.Load();
                ddlModuloPaiFuncCFU.SelectedValue = admModulo.ADMMODULO2 != null ? admModulo.ADMMODULO2.ideAdmModulo.ToString() : "";
                txtCodigoFuncCFU.Text = admModulo.ideAdmModulo.ToString();
                txtNomeFuncCFU.Text = admModulo.nomModulo;
                txtNomeItemMenuFuncCFU.Text = admModulo.nomItemMenu;
                txtDescricaoFuncCFU.Text = admModulo.nomDescricao;
                txtNomUrlModuloFuncCFU.Text = admModulo.nomURLModulo;
                txtOrdemMenuFuncCFU.Text = admModulo.numOrdemMenu.ToString();
                ddlTipoIconeFuncCFU.SelectedValue = admModulo.CO_FLAG_TIPO_ICONE;
                ddlStatusFuncCFU.SelectedValue = admModulo.flaStatus;
                txtIconeFuncCFU.Text = admModulo.imgIcone;

//------------> Dados Gerenciais
                chkInforGerenFuncCFU.Checked = admModulo.CO_FLAG_INFOR_GEREN == "S";
                txtNomeGerencialFuncCFU.Text = admModulo.nomModulo_GEREN;
                txtDescGerenFuncCFU.Text = admModulo.nomDescricaoGEREN;
                ddlTipoIconeGerenFuncCFU.SelectedValue = admModulo.CO_FLAG_TIPO_ICONE_GEREN;
//-------------

//------------> Chamada do método de habilitaçao de Informaçoes Gerenciais
                HabilitaDadosGerenciais();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade ADMMODULO</returns>
        private ADMMODULO RetornaEntidade()
        {
            ADMMODULO admModulo = ADMMODULO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (admModulo == null) ? new ADMMODULO() : admModulo;
        }

        /// <summary>
        /// Método que habilita os dados gerenciais de acordo com o checkbox (chkInforGerenFuncCFU)
        /// </summary>
        protected void HabilitaDadosGerenciais()
        {
            if (chkInforGerenFuncCFU.Checked)
            {
                liDesGerenFuncCFU.Visible = liNomModuloGerenFuncCFU.Visible = liTipoIconeGerenFuncCFU.Visible = liBarraTituloFuncCFU.Visible = true;
                liBarraTituloFuncCFU.Style.Add("background-color", "#EEEEEE;"); liBarraTituloFuncCFU.Style.Add("margin-top", "5px;");
                liBarraTituloFuncCFU.Style.Add("margin-bottom", "2px;"); liBarraTituloFuncCFU.Style.Add("padding", "5px;");
                liBarraTituloFuncCFU.Style.Add("text-align", "center;"); liBarraTituloFuncCFU.Style.Add("width", "325px;");
                liBarraTituloFuncCFU.Style.Add("height", "10px;"); liBarraTituloFuncCFU.Style.Add("clear", "both;");
            }
            else
            {
                liDesGerenFuncCFU.Visible = liNomModuloGerenFuncCFU.Visible = liTipoIconeGerenFuncCFU.Visible = liBarraTituloFuncCFU.Visible = false;
                txtNomeGerencialFuncCFU.Text = "";
                txtDescGerenFuncCFU.Text = "";
                ddlTipoIconeGerenFuncCFU.SelectedValue = "";
            }
        }
        #endregion

        #region Carregamento DropDown
        
        /// <summary>
        /// Método que carrega o dropdown de Módulo Pai
        /// </summary>
        private void CarregaModulos()
        {
            ddlModuloPaiFuncCFU.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                              where (admModulo.flaTipoItemSubMenu == "ATM" || admModulo.flaTipoItemSubMenu == "LST")                                              
                                              select new { admModulo.nomModulo, admModulo.ideAdmModulo }).OrderBy(a => a.nomModulo);

            ddlModuloPaiFuncCFU.DataTextField = "nomModulo";
            ddlModuloPaiFuncCFU.DataValueField = "ideAdmModulo";
            ddlModuloPaiFuncCFU.DataBind();

            ddlModuloPaiFuncCFU.Items.Insert(0, new ListItem("", ""));
        } 
        #endregion

        protected void chkInforGerenFuncCFU_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaDadosGerenciais();
        }          
    }
}
