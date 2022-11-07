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
// 03/03/14 | Débora Lohane              | Criação da funcionalidade para cadastro de subareas

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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1103_CadastroSubArea
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

            CarregaRegiao();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                CarregaRegiao();
                CarregaArea();
            }

        }


        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB908_SUBAREA tb908 = RetornaEntidade();
            int area = int.Parse(ddlArea.SelectedValue);

            tb908.TB907_AREA = TB907_AREA.RetornaTodosRegistros().First(f => f.ID_AREA == area);
            tb908.NM_SUBAREA = txtSubarea.Text;
            tb908.SIGLA = txtSigla.Text;

            CurrentPadraoCadastros.CurrentEntity = tb908;
        }



        #endregion
        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB908_SUBAREA tb908 = RetornaEntidade();

            if (tb908 != null)
            {
                tb908.TB907_AREAReference.Load();

                var area = TB907_AREA.RetornaPelaChavePrimaria(tb908.TB907_AREA.ID_AREA);
                area.TB906_REGIAOReference.Load();

                var regiao = TB906_REGIAO.RetornaPelaChavePrimaria(area.TB906_REGIAO.ID_REGIAO);

                ddlRegiao.SelectedValue = regiao.ID_REGIAO.ToString();

                CarregaArea();
                ddlArea.SelectedValue = tb908.TB907_AREA.ID_AREA.ToString();
                txtSubarea.Text = tb908.NM_SUBAREA;
                txtSigla.Text = tb908.SIGLA;
            }
        }

        private TB908_SUBAREA RetornaEntidade()
        {
            TB908_SUBAREA tb908 = TB908_SUBAREA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb908 == null) ? new TB908_SUBAREA() : tb908;
        }

        #endregion

        #region Carregamento DropDown
        /// Método que carrega o dropdown de Regiao
        /// </summary>
        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros();
            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Area
        /// </summary>
        private void CarregaArea()
        {
            int idRegiao = int.Parse(ddlRegiao.SelectedValue);
            ddlArea.DataSource = (from tb906 in TB907_AREA.RetornaPelaRegiao(idRegiao)
                                  select new { tb906.NM_AREA, tb906.ID_AREA });

            ddlArea.DataTextField = "NM_AREA";
            ddlArea.DataValueField = "ID_AREA";
            ddlArea.DataBind();
        }

              #endregion

        protected void ddlRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaArea();
        }

    }
}