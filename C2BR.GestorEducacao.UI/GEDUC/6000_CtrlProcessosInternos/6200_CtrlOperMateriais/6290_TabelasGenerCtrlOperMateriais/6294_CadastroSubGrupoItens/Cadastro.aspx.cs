//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: TABELAS DE APOIO - ESTOQUE
// OBJETIVO: CADASTRAMENTO DE SUBGRUPOS DE ITENS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6294_CadastroSubGrupoItens
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDataAlter.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CarregaGrupos();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB88_SUBGRUPO_ITENS tb88 = RetornaEntidade();

            tb88.NO_SUBGRP_ITEM = txtNome.Text;
            tb88.DT_ALT_REGISTRO = DateTime.Now;
            tb88.TB87_GRUPO_ITENS = TB87_GRUPO_ITENS.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue));

            CurrentPadraoCadastros.CurrentEntity = tb88;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB88_SUBGRUPO_ITENS tb88 = RetornaEntidade();

            if (tb88 != null)
            {
                tb88.TB87_GRUPO_ITENSReference.Load();

                txtNome.Text = tb88.NO_SUBGRP_ITEM;
                ddlGrupo.SelectedValue = tb88.TB87_GRUPO_ITENS.CO_GRUPO_ITEM.ToString();
                txtDataAlter.Text = tb88.DT_ALT_REGISTRO.Value.ToString("dd/MM/yyyy");
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB88_SUBGRUPO_ITENS</returns>
        private TB88_SUBGRUPO_ITENS RetornaEntidade()
        {
            TB88_SUBGRUPO_ITENS tb88 = TB88_SUBGRUPO_ITENS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb88 == null) ? new TB88_SUBGRUPO_ITENS() : tb88;
        }
        #endregion        

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            ddlGrupo.DataSource = (from tb87 in TB87_GRUPO_ITENS.RetornaTodosRegistros()
                                   select new { tb87.CO_GRUPO_ITEM, tb87.NO_GRUPO_ITEM }).OrderBy(g => g.NO_GRUPO_ITEM);

            ddlGrupo.DataValueField = "CO_GRUPO_ITEM";
            ddlGrupo.DataTextField = "NO_GRUPO_ITEM";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}