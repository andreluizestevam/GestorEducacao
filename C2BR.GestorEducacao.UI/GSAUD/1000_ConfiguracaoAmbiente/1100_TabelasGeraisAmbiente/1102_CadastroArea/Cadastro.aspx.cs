//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/03/14 | Débora Lohane              | Criação da funcionalidade para cadastro de regioes

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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1102_CadastroArea
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
                CarregaRegiao();

        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }


        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB907_AREA tb907 = RetornaEntidade();
            int regiao = int.Parse(ddlRegiao.SelectedValue);
            tb907.TB906_REGIAO = TB906_REGIAO.RetornaTodosRegistros().First(f => f.ID_REGIAO == regiao);
            tb907.NM_AREA = txtArea.Text;
            tb907.SIGLA = txtSigla.Text;

            CurrentPadraoCadastros.CurrentEntity = tb907;
        }

        #endregion
        #region Métodos

        private void CarregaFormulario()
        {
            TB907_AREA tb907 = RetornaEntidade();

            if (tb907 != null)
            {
                txtSigla.Text = tb907.SIGLA;
                txtArea.Text = tb907.NM_AREA;
                int regiao = int.Parse(ddlRegiao.SelectedValue);
                ddlRegiao.SelectedValue = tb907.TB906_REGIAO.ID_REGIAO.ToString();
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB907_AREA</returns>
        private TB907_AREA RetornaEntidade()
        {
            TB907_AREA tb907 = TB907_AREA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
             return (tb907 == null) ? new TB907_AREA() : tb907;
        }
        #endregion
        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros().OrderBy(r => r.ID_REGIAO);

            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();
        }

    }
}