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
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para cadastro de Tipos de Dor


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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1130_CtrlAnamnese._1131_TipoDores
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
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Nome do Tipo de Dor");
                return;
            }
            if (string.IsNullOrEmpty(txtSiglaTipo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Sigla do Tipo de Dor");
                return;
            }

            TBS337_TIPO_DORES tbs337 = RetornaEntidade();

            tbs337.NM_TIPO_DORES = txtNomeTipo.Text;
            tbs337.CO_SIGLA_TIPO_DORES = txtSiglaTipo.Text;
            tbs337.CO_SITUA_TIPO_DORES = ddlSituaTipo.SelectedValue;
            tbs337.DE_OBSER = txtObser.Text;

            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSituaTipo.SelectedValue)
            {
                tbs337.DT_SITUA = DateTime.Now;
                tbs337.CO_COL_SITUA = LoginAuxili.CO_COL;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tbs337.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs337.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs337.DT_CADAS = DateTime.Now;
                    break;
            }

            //CurrentPadraoCadastros.CurrentEntity = tbs337;
            TBS337_TIPO_DORES.SaveOrUpdate(tbs337, true);
            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }
        #endregion
        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TBS337_TIPO_DORES tbs337 = RetornaEntidade();


            if (tbs337 != null)
            {

                if (tbs337 != null)
                {
                    txtNomeTipo.Text = tbs337.NM_TIPO_DORES;
                    txtSiglaTipo.Text = tbs337.CO_SIGLA_TIPO_DORES;
                    ddlSituaTipo.SelectedValue =
                    hidSituacao.Value = tbs337.CO_SITUA_TIPO_DORES;
                    txtObser.Text = tbs337.DE_OBSER;
                }
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS337_TIPO_DORES</returns>
        private TBS337_TIPO_DORES RetornaEntidade()
        {
            TBS337_TIPO_DORES tbs337 = TBS337_TIPO_DORES.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs337 == null) ? new TBS337_TIPO_DORES() : tbs337;
        }
        #endregion

        #region Carregamentows

        #endregion

        #region Funções de Campo

        #endregion
    }
}