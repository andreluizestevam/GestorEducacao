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
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para cadastro de Itens de ISDA


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
using System.Data;
using Resources;
namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1120_CtrISDA._1122_Itens_ISDA
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        #region Eventos
        string salvaDTSitu;
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
                carregaTiposISDA();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtNomeTipo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Nome do Tipo ISDA");
                return;
            }
            if (string.IsNullOrEmpty(txtSiglaTipo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Sigla do Tipo ISDA");
                return;
            }

            TBS336_ISDA_ITENS tbs336 = RetornaEntidade();

            tbs336.NM_ITEM_ISDA = txtNomeTipo.Text;
            tbs336.CO_SIGLA_ITEM_ISDA = txtSiglaTipo.Text;
            tbs336.CO_SITUA_ITEM_ISDA = ddlSituaTipo.SelectedValue;
            tbs336.ID_TIPO_ISDA = (!string.IsNullOrEmpty(ddlTipoISDA.SelectedValue) ? int.Parse(ddlTipoISDA.SelectedValue) : (int?)null);

            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSituaTipo.SelectedValue)
            {
                tbs336.DT_SITUA = DateTime.Now;
                tbs336.CO_COL_SITUA = LoginAuxili.CO_COL;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tbs336.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs336.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs336.DT_CADAS = DateTime.Now;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs336;
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TBS336_ISDA_ITENS tbs336 = RetornaEntidade();


            if (tbs336 != null)
            {

                if (tbs336 != null)
                {
                    txtNomeTipo.Text = tbs336.NM_ITEM_ISDA;
                    txtSiglaTipo.Text = tbs336.CO_SIGLA_ITEM_ISDA;
                    ddlSituaTipo.SelectedValue =
                    hidSituacao.Value = tbs336.CO_SITUA_ITEM_ISDA;
                }
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS336_ISDA_ITENS</returns>
        private TBS336_ISDA_ITENS RetornaEntidade()
        {
            TBS336_ISDA_ITENS tbs336 = TBS336_ISDA_ITENS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs336 == null) ? new TBS336_ISDA_ITENS() : tbs336;
        }
        #endregion

        #region Carregamentos

        //Carrega os tipos ISDA
        private void carregaTiposISDA()
        {
            AuxiliCarregamentos.CarregaTiposISDA(ddlTipoISDA, false, false);
            ddlTipoISDA.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Funções de Campo

        #endregion
    }
}