//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *****
// SUBMÓDULO: *****
// OBJETIVO: CADASTRAMENTO DE SUBGRUPOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1138_CadastroSubGrupoDotac
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
                CarregaGrupos();
                if (CurrentPadraoCadastros.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB261_SUBGRUPO tb261 = RetornaEntidade();

            tb261.CO_SUBGRUPO = txtCodigo.Text;
            tb261.NOM_SUBGRUPO = txtNome.Text;
            tb261.TB260_GRUPO = TB260_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue));
            tb261.DT_SITUA_SUBGRUPO = DateTime.Parse(txtDtSituacao.Text);
            tb261.FL_SITUA_SUBGRUPO = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb261;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB261_SUBGRUPO tb261 = RetornaEntidade();

            if (tb261 != null)
            {
                tb261.TB260_GRUPOReference.Load();
                txtCodigo.Text = tb261.CO_SUBGRUPO;
                txtNome.Text = tb261.NOM_SUBGRUPO;
                ddlGrupo.SelectedValue = tb261.TB260_GRUPO.ID_GRUPO.ToString();
                txtDtSituacao.Text = tb261.DT_SITUA_SUBGRUPO.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb261.FL_SITUA_SUBGRUPO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB261_SUBGRUPO</returns>
        private TB261_SUBGRUPO RetornaEntidade()
        {
            TB261_SUBGRUPO tb261 = TB261_SUBGRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb261 == null) ? new TB261_SUBGRUPO() : tb261;
        }
        #endregion        

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                   where tb260.TP_GRUPO == "D" 
                                   select new { tb260.ID_GRUPO, tb260.NOM_GRUPO }).OrderBy(g => g.NOM_GRUPO);

            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}