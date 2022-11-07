//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PROGRAMAS SOCIAIS
// OBJETIVO: CADASTRAMENTO DE PROGRAMAS SOCIAIS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1600_ProgramasSociais.F1602_TipoProgrSociais
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = txtDataSituacao.Text = dataAtual;
                txtInstituicao.Text = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NOME_ORGAO;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                TB215_PROGR_TIPO_SOCEDU tb215 = RetornaEntidade();

                if (tb215 == null)
                {
                    tb215 = new TB215_PROGR_TIPO_SOCEDU();

                    tb215.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    tb215.DT_CADAS_PROGR_TP_SOCEDU = DateTime.Now;
                }

                tb215.NO_PROGR_TP_SOCEDU = txtTipo.Text.Trim();
                tb215.NO_SIGLA_PROGR_TP_SOCEDU = txtSigla.Text.Trim().ToUpper();
                tb215.CO_SITU_PROGR_TP_SOCEDU = ddlSituacao.SelectedValue;
                tb215.DT_SITU_PROGR_TP_SOCEDU = DateTime.Now;

                CurrentCadastroMasterPage.CurrentEntity = tb215;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB215_PROGR_TIPO_SOCEDU tb215 = RetornaEntidade();

            if (tb215 != null)
            {
                tb215.TB000_INSTITUICAOReference.Load();

                txtInstituicao.Text = tb215.TB000_INSTITUICAO.ORG_NOME_ORGAO;
                txtTipo.Text = tb215.NO_PROGR_TP_SOCEDU;
                txtSigla.Text = tb215.NO_SIGLA_PROGR_TP_SOCEDU;
                ddlSituacao.SelectedValue = tb215.CO_SITU_PROGR_TP_SOCEDU;
                txtDataSituacao.Text = tb215.DT_SITU_PROGR_TP_SOCEDU.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = tb215.DT_CADAS_PROGR_TP_SOCEDU.ToString("dd/MM/yyyy");
            }      
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB215_PROGR_TIPO_SOCEDU</returns>
        private TB215_PROGR_TIPO_SOCEDU RetornaEntidade()
        {
            return TB215_PROGR_TIPO_SOCEDU.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion  
    }
}