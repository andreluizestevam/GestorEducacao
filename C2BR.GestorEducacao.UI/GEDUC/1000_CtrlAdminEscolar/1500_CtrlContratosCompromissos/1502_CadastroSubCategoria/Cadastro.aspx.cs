//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: TABELAS DE APOIO - ESTOQUE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1502_CadastroSubCategoria
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
                CarregaCategorias();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB314_SUB_CATEG_CONTR tb314 = RetornaEntidade();

            tb314.CO_SUB_CATEG_CONTR = txtCodigo.Text;
            tb314.NM_SUB_CATEG_CONTR = txtNome.Text;
            tb314.TB313_CATEG_CONTR = TB313_CATEG_CONTR.RetornaPelaChavePrimaria(int.Parse(ddlCateg.SelectedValue));
            tb314.CO_SITUACAO = ddlStatus.SelectedValue;
            tb314.DT_SITUACAO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb314;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB314_SUB_CATEG_CONTR tb314 = RetornaEntidade();

            if (tb314 != null)
            {
                tb314.TB313_CATEG_CONTRReference.Load();
                txtCodigo.Text = tb314.CO_SUB_CATEG_CONTR;
                txtNome.Text = tb314.NM_SUB_CATEG_CONTR;
                ddlCateg.SelectedValue = tb314.TB313_CATEG_CONTR.ID_CATEG_CONTR.ToString();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB314_SUB_CATEG_CONTR</returns>
        private TB314_SUB_CATEG_CONTR RetornaEntidade()
        {
            TB314_SUB_CATEG_CONTR tb314 = TB314_SUB_CATEG_CONTR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb314 == null) ? new TB314_SUB_CATEG_CONTR() : tb314;
        }
        #endregion        

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Categorias
        /// </summary>
        private void CarregaCategorias()
        {
            ddlCateg.DataSource = (from tb313 in TB313_CATEG_CONTR.RetornaTodosRegistros()    
                                   where tb313.CO_SITUACAO == "A"
                                   select new { tb313.ID_CATEG_CONTR, tb313.NM_CATEG_CONTR }).OrderBy(g => g.NM_CATEG_CONTR);

            ddlCateg.DataValueField = "ID_CATEG_CONTR";
            ddlCateg.DataTextField = "NM_CATEG_CONTR";
            ddlCateg.DataBind();

            ddlCateg.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}