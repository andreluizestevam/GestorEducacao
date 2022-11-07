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
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para cadastro de Tipos de ISDA


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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1120_CtrISDA._1121_Tipos_ISDA
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

            TBS335_ISDA_TIPO tbs335 = RetornaEntidade();

            tbs335.NM_TIPO_ISDA = txtNomeTipo.Text;
            tbs335.CO_SIGLA_ISDA = txtSiglaTipo.Text;
            tbs335.CO_SITUA_ISDA = ddlSituaTipo.SelectedValue;

            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSituaTipo.SelectedValue)
            {
                tbs335.DT_SITUA = DateTime.Now;
                tbs335.CO_COL_SITUA = LoginAuxili.CO_COL;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tbs335.EntityState)
            { 
                case EntityState.Added:
                case EntityState.Detached:
                    tbs335.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs335.DT_CADAS = DateTime.Now;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs335;
        }
        #endregion
        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TBS335_ISDA_TIPO tbs335 = RetornaEntidade();


            if (tbs335 != null)
            {

                if (tbs335 != null)
                {
                    txtNomeTipo.Text = tbs335.NM_TIPO_ISDA;
                    txtSiglaTipo.Text = tbs335.CO_SIGLA_ISDA;
                    ddlSituaTipo.SelectedValue = 
                    hidSituacao.Value = tbs335.CO_SITUA_ISDA;
                    hdTipoIsda.Value = tbs335.ID_TIPO_ISDA.ToString();
                    CarregaISDAAssociados();
                }
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB904_CIDADE</returns>
        private TBS335_ISDA_TIPO RetornaEntidade()
        {
            TBS335_ISDA_TIPO tbs335 = TBS335_ISDA_TIPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs335 == null) ? new TBS335_ISDA_TIPO() : tbs335;
        }
        #endregion

        #region Carregamentows

        /// <summary>
        /// Carrega os ISDA associados ao Tipo ISDA em contexto
        /// </summary>
        private void CarregaISDAAssociados()
        { 
            int tipo = (!string.IsNullOrEmpty(hdTipoIsda.Value) ? int.Parse(hdTipoIsda.Value) : 0);
            var res = (from tbs336 in TBS336_ISDA_ITENS.RetornaTodosRegistros()
                       where tbs336.ID_TIPO_ISDA == tipo
                       select new
                       {
                           tbs336.NM_ITEM_ISDA,
                           tbs336.ID_ITEM_ISDA,
                           tbs336.CO_SIGLA_ITEM_ISDA,
                       }).OrderBy(w=>w.NM_ITEM_ISDA).ToList();

            if (res.Count > 0)
                infoAgrup.Visible = true;

            grdISDA.DataSource = res;
            grdISDA.DataBind();
        }

        /// <summary>
        /// Método responsável por deletar a associação do item selecionado na grid
        /// </summary>
        private void DeletaAssociacao()
        {
            foreach (GridViewRow li in grdISDA.Rows)
            {
                if ((((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked) == true)
                {
                    int isda = int.Parse((((HiddenField)li.Cells[0].FindControl("hidcoitem")).Value));
                    TBS336_ISDA_ITENS tbs336 = TBS336_ISDA_ITENS.RetornaPelaChavePrimaria(isda);
                    tbs336.ID_TIPO_ISDA = (int?)null;
                    TBS336_ISDA_ITENS.SaveOrUpdate(tbs336, true);
                }
            }
            CarregaISDAAssociados();
        }

        #endregion

        #region Funções de Campo

        protected void lnkApagaAssoci_OnClick(object sender, EventArgs e)
        {
            DeletaAssociacao();
        }

        #endregion
    }
}