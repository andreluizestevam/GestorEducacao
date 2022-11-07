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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1107_CtrlCategoria
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
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar  a operadora");
                return;
            }
            if (string.IsNullOrEmpty(ddlPlano.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o plano");
                return;
            }
            if (string.IsNullOrEmpty(txtNomeCategoria.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar  o nome da categoria  ");
                return;
            }
            if (string.IsNullOrEmpty(txtSigla.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a sigla");
                return;
            }
            TB367_CATEG_PLANO_SAUDE tb367 = RetornaEntidade();

            tb367.NM_CATEG = txtNomeCategoria.Text;
            tb367.NM_SIGLA_CATEG = txtSigla.Text;

            //--------------------------------------------------------------------------------------------------
            int IdOperadora = Convert.ToInt32(ddlOperadora.SelectedValue);
            int IdPlano = Convert.ToInt32(ddlPlano.SelectedValue);
            tb367.TB250_OPERA = TB250_OPERA.RetornaTodosRegistros().First(f => f.ID_OPER == IdOperadora);
            tb367.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaTodosRegistros().First(f => f.ID_PLAN == IdPlano);

            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSitu.SelectedValue)
            {
                tb367.FL_SITUA = ddlSitu.SelectedValue;
                tb367.DT_CADAS = DateTime.Now;
                tb367.DT_SITUA = DateTime.Now;
                tb367.CO_COL_SITUA = LoginAuxili.CO_COL;
                tb367.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tb367.IP_SITUA = Request.UserHostAddress;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tb367.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tb367.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tb367.DT_CADAS = DateTime.Now;
                    tb367.IP_CADAS = Request.UserHostAddress;
                    tb367.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tb367;
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB367_CATEG_PLANO_SAUDE tb367 = RetornaEntidade();

            if (tb367 != null)
            {
                tb367.TB251_PLANO_OPERAReference.Load();
                tb367.TB250_OPERAReference.Load();

                string p = TB250_OPERA.RetornaTodosRegistros().Where(a => a.ID_OPER == tb367.TB251_PLANO_OPERA.ID_PLAN).ToString();
                ddlOperadora.SelectedValue = tb367.TB250_OPERA.ID_OPER.ToString();
                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, false);
                ddlPlano.SelectedValue = tb367.TB251_PLANO_OPERA.ID_PLAN.ToString();

                      
                txtNomeCategoria.Text = tb367.NM_CATEG;
                txtSigla.Text = tb367.NM_SIGLA_CATEG;
                ddlSitu.SelectedValue = tb367.FL_SITUA;
                hidSituacao.Value = tb367.FL_SITUA;

            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB251_PLANO_OPERA</returns>
        private TB367_CATEG_PLANO_SAUDE RetornaEntidade()
        {
            TB367_CATEG_PLANO_SAUDE tb367 = TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb367 == null) ? new TB367_CATEG_PLANO_SAUDE() : tb367;
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


        private void CarregaPlanosSaude()
        {
            try
            {
                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, false);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }


        }

        protected void ddOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanosSaude();
        }
        #endregion
    }
}