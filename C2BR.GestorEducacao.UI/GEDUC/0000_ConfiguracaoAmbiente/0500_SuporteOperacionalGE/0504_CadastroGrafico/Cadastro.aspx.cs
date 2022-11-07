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
//  07/02/14|  Vinicius Reis             | Comentado código que ao alterar a 
//          |                            | combo de classificação era apagado
//          |                            | as combos de modulo e grupo de inf

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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0504_CadastroGrafico
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) 
            {
                CarregaModulos();
                CarregaGrupoInformacao();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ddlTipoGrafi.SelectedValue != "P")
            {
                if (ddlModulo.SelectedValue == "" || ddlGrupoInfor.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Módulo e Grupo de Informações devem ser selecionados para essa classificação.");
                    return;
                }
            }
            TB307_GRAFI_GERAL tb307 = RetornaEntidade();

            tb307.ID_MODULO = ddlModulo.SelectedValue != "" ? (int?)int.Parse(ddlModulo.SelectedValue) : null;
            tb307.ID_SUB_MODULO = ddlGrupoInfor.SelectedValue != "" ? (int?)int.Parse(ddlGrupoInfor.SelectedValue) : null;
            tb307.NM_TITULO_GRAFI = txtTitulGrafi.Text;
            tb307.DE_QUERY_GRAFI = txtQueryGrafi.Text;
            tb307.CO_TIPO_GRAFI = ddlTipoGrafi.SelectedValue;
            tb307.CO_CLASS_GRAFI = ddlClassiGrafi.SelectedValue;
            tb307.CO_STATUS_GRAFI = ddlStatus.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb307;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB307_GRAFI_GERAL tb307 = RetornaEntidade();

            if (tb307 != null)
            {
                CarregaModulos();
                ddlModulo.SelectedValue = tb307.ID_MODULO != null ? tb307.ID_MODULO.ToString() : "";
                CarregaGrupoInformacao();
                ddlGrupoInfor.SelectedValue = tb307.ID_SUB_MODULO != null ? tb307.ID_SUB_MODULO.ToString() : "";
                txtTitulGrafi.Text = tb307.NM_TITULO_GRAFI;
                txtQueryGrafi.Text = tb307.DE_QUERY_GRAFI;
                ddlTipoGrafi.SelectedValue = tb307.CO_TIPO_GRAFI;
                ddlClassiGrafi.SelectedValue = tb307.CO_CLASS_GRAFI;
                ddlStatus.SelectedValue = tb307.CO_STATUS_GRAFI;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB307_GRAFI_GERAL</returns>
        private TB307_GRAFI_GERAL RetornaEntidade()
        {
            TB307_GRAFI_GERAL tb307 = TB307_GRAFI_GERAL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb307 == null) ? new TB307_GRAFI_GERAL() : tb307;
        }

        #endregion       

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Módulos
        /// </summary>
        private void CarregaModulos()
        {
            ddlModulo.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                                    where admModulo.flaStatus == "A" && admModulo.ADMMODULO2 == null
                                    select new { admModulo.ideAdmModulo, admModulo.nomItemMenu, admModulo.numOrdemMenu }).Distinct().OrderBy(b => b.numOrdemMenu);

            ddlModulo.DataValueField = "ideAdmModulo";
            ddlModulo.DataTextField = "nomItemMenu";
            ddlModulo.DataBind();

            ddlModulo.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Informações
        /// </summary>
        private void CarregaGrupoInformacao()
        {
            int modulo = ddlModulo.SelectedValue != "" ? int.Parse(ddlModulo.SelectedValue) : 0;

            ddlGrupoInfor.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                                        where admModulo.flaStatus == "A" && admModulo.ADMMODULO2.ideAdmModulo == modulo
                                        select new { admModulo.ideAdmModulo, admModulo.nomItemMenu, admModulo.numOrdemMenu }).Distinct().OrderBy(b => b.numOrdemMenu);

            ddlGrupoInfor.DataValueField = "ideAdmModulo";
            ddlGrupoInfor.DataTextField = "nomItemMenu";
            ddlGrupoInfor.DataBind();

            ddlGrupoInfor.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoInformacao();
        }

        protected void ddlClassiGrafi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClassiGrafi.SelectedValue == "P")
            {
                //ddlModulo.SelectedValue = "";
                //ddlGrupoInfor.SelectedValue = "";
                //ddlModulo.Enabled = ddlGrupoInfor.Enabled = false;
            }
            else
            {
                ddlModulo.Enabled = ddlGrupoInfor.Enabled = true;
            }
        }
    }
}