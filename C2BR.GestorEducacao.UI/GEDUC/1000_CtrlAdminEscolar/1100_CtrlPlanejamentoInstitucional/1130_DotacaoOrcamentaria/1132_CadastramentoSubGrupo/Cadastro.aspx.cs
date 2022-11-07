//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DOTAÇÃO ORÇAMENTÁRIA
// OBJETIVO: CADASTRAMENTO SUBGRUPO DOTAÇÃO ORÇAMENTÁRIA
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
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1132_CadastramentoSubGrupo
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

            CarregaGrupo();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                ddlGrupo.Enabled = false;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB303_DOTAC_SUBGRUPO tb303 = RetornaEntidade();

            if (tb303 == null)
            {
                tb303 = new TB303_DOTAC_SUBGRUPO();

                tb303.TB302_DOTAC_GRUPO = TB302_DOTAC_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue));
                tb303.DT_CADAS_DOTAC_SUBGRUPO = DateTime.Now;
            }

            tb303.CO_DOTAC_SUBGRUPO = int.Parse(txtNumSubGrupo.Text);
            tb303.DE_DOTAC_SUBGRUPO = txtDescricaoSubGrupo.Text;
            tb303.SIGLA_DOTAC_SUBGRUPO = txtSiglaSubGrupo.Text.ToUpper();
            tb303.DT_SITUA_DOTAC_SUBGRUPO = DateTime.Now;
            tb303.CO_SITUA_DOTAC_SUBGRUPO = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb303;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB303_DOTAC_SUBGRUPO tb303 = RetornaEntidade();

            if (tb303 != null)
            {
                tb303.TB302_DOTAC_GRUPOReference.Load();

                ddlGrupo.SelectedValue = tb303.TB302_DOTAC_GRUPO.ID_DOTAC_GRUPO.ToString();
                txtNumSubGrupo.Text = tb303.CO_DOTAC_SUBGRUPO.ToString();
                txtDescricaoSubGrupo.Text = tb303.DE_DOTAC_SUBGRUPO;
                txtSiglaSubGrupo.Text = tb303.SIGLA_DOTAC_SUBGRUPO;
                ddlSituacao.SelectedValue = tb303.CO_SITUA_DOTAC_SUBGRUPO;
                txtDtSituacao.Text = tb303.DT_SITUA_DOTAC_SUBGRUPO.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB303_DOTAC_SUBGRUPO</returns>
        private TB303_DOTAC_SUBGRUPO RetornaEntidade()
        {
            return TB303_DOTAC_SUBGRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion   
     
        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupo
        /// </summary>
        private void CarregaGrupo()
        {
            ddlGrupo.DataSource = (from tb302 in TB302_DOTAC_GRUPO.RetornaTodosRegistros()
                                   select new { tb302.DE_DOTAC_GRUPO, tb302.ID_DOTAC_GRUPO });

            ddlGrupo.DataTextField = "DE_DOTAC_GRUPO";
            ddlGrupo.DataValueField = "ID_DOTAC_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}