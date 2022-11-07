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
// 10/12/14 | Maxwell Almeida            | Criação da funcionalidade para Cadastro de Operadoras


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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1108_CtrlFeriados
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
                
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtNomeferiado.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o nome do feriado");
                return;
            }

            TBS376_AGENDA_FERIADOS tbs376 = RetornaEntidade();

            tbs376.NM_FERIA = txtNomeferiado.Text;
            tbs376.TP_FERIA = ddlTipoFeriado.SelectedValue;
            tbs376.DT_FERIA = Convert.ToDateTime(txtData.Text);
            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSituacao.SelectedValue)
            {
                tbs376.CO_SITUA = ddlSituacao.SelectedValue;
                tbs376.DT_SITUA = DateTime.Now;
                tbs376.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs376.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs376.IP_SITUA = Request.UserHostAddress;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tbs376.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs376.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs376.DT_CADAS = DateTime.Now;
                    tbs376.IP_CADAS = Request.UserHostAddress;
                    tbs376.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs376;
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            try
            {
                TBS376_AGENDA_FERIADOS tbs372 = RetornaEntidade();

                if (tbs372 != null)
                {

                    txtNomeferiado.Text = tbs372.NM_FERIA;
                    hidSituacao.Value = tbs372.CO_SITUA;
                    txtData.Text = Convert.ToString(tbs372.DT_FERIA);
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao caregar  formulário" + " - " + ex.Message);
                return;
            }

        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS376_AGENDA_FERIADOS</returns>
        private TBS376_AGENDA_FERIADOS RetornaEntidade()
        {

            TBS376_AGENDA_FERIADOS tb251 = TBS376_AGENDA_FERIADOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb251 == null) ? new TBS376_AGENDA_FERIADOS() : tb251;
        }


        #endregion
    }
}