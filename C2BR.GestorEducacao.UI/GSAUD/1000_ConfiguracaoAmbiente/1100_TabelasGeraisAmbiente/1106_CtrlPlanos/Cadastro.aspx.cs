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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1106_CtrlPlanos
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
                CarregaOperadorasPlanSaude();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(ddlOperadora.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Operadora");
                return;
            }

            if (string.IsNullOrEmpty(txtNomePlano.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar  o nome do plano  ");
                return;
            }
            if (string.IsNullOrEmpty(txtSigla.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a sigla");
                return;
            }


            TB251_PLANO_OPERA tb251 = RetornaEntidade();

            tb251.NOM_PLAN = txtNomePlano.Text;
            tb251.NM_SIGLA_PLAN = txtSigla.Text;

            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSitu.SelectedValue)
            {
                int IdOperadora = Convert.ToInt32(ddlOperadora.SelectedValue);

                tb251.TB250_OPERA = TB250_OPERA.RetornaTodosRegistros().First(f => f.ID_OPER == IdOperadora);
                tb251.FL_SITU_PLAN = ddlSitu.SelectedValue;
                tb251.ST_PLAN = ddlSitu.SelectedValue;
                tb251.DT_CADAS = DateTime.Now;
                tb251.CO_COL_SITU = LoginAuxili.CO_COL;
                tb251.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tb251.IP_SITU_PLAN = Request.UserHostAddress;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tb251.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tb251.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tb251.DT_CADAS = DateTime.Now;
                    tb251.IP_CADAS = Request.UserHostAddress;
                    tb251.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tb251;
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            try
            {
                TB251_PLANO_OPERA tb251 = RetornaEntidade();

                if (tb251 != null)
                {
                    tb251.TB250_OPERAReference.Load();

                    ddlOperadora.SelectedValue = tb251.TB250_OPERA.ID_OPER.ToString();
                    txtSigla.Text =  tb251.NM_SIGLA_PLAN;
                    txtNomePlano.Text = tb251.NOM_PLAN;
                    txtSigla.Text = tb251.NM_SIGLA_PLAN;
                    ddlSitu.SelectedValue = tb251.FL_SITU_PLAN;
                    hidSituacao.Value = tb251.FL_SITU_PLAN;

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
        /// <returns>Entidade TB251_PLANO_OPERA</returns>
        private TB251_PLANO_OPERA RetornaEntidade()
        {
            TB251_PLANO_OPERA tb251 = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb251 == null) ? new TB251_PLANO_OPERA() : tb251;
        }

        private void CarregaOperadorasPlanSaude()
        {
            try
            {
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false);
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }


        }

        #endregion
    }
}