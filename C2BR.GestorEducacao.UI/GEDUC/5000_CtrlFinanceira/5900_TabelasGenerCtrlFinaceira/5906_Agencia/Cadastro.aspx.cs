//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5906_Agencia
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
            if (!Page.IsPostBack)
                CarregaBancos();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                CurrentPadraoCadastros.CurrentEntity = RetornaEntidade();
                return;
            }

            if (Page.IsValid)
            {
                TB30_AGENCIA tb30 = RetornaEntidade();

                if (tb30 == null)
                {
                    tb30 = new TB30_AGENCIA();

                    tb30.IDEBANCO = ddlBanco.SelectedValue;
                    tb30.TB29_BANCO = TB29_BANCO.RetornaPelaChavePrimaria(tb30.IDEBANCO);
                    tb30.CO_AGENCIA = int.Parse(txtCodigoAgencia.Text.Trim());
                }

                tb30.DI_AGENCIA = txtDigitoAgencia.Text == "" ? "" : txtDigitoAgencia.Text.Trim().ToUpper();
                tb30.NO_AGENCIA = txtNomeAgencia.Text.Trim();
                tb30.NO_GERENTE = txtNomeGerenteAgencia.Text.Trim();
                tb30.NO_EMAIL = txtEmail.Text != "" ? txtEmail.Text : null;
                tb30.NU_TELEFONE = txtTelefoneAgencia.Text != "" ? txtTelefoneAgencia.Text.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") : null;
                tb30.NU_TEL_GERENTE = txtTelefoneGerenteAgencia.Text != "" ? txtTelefoneGerenteAgencia.Text.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") : null;
                tb30.NU_FAX_AGENCIA = txtFaxAgencia.Text != "" ? txtFaxAgencia.Text.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") : null;

                CurrentPadraoCadastros.CurrentEntity = tb30;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB30_AGENCIA tb30 = RetornaEntidade();

            if (tb30 != null)
            {
                ddlBanco.SelectedValue = tb30.IDEBANCO;
                txtCodigoAgencia.Text = tb30.CO_AGENCIA.ToString();
                txtDigitoAgencia.Text = tb30.DI_AGENCIA ?? tb30.DI_AGENCIA.ToString();
                txtNomeAgencia.Text = tb30.NO_AGENCIA;
                txtNomeGerenteAgencia.Text = tb30.NO_GERENTE;
                txtEmail.Text = tb30.NO_EMAIL;
                txtTelefoneAgencia.Text = tb30.NU_TELEFONE;
                txtTelefoneGerenteAgencia.Text = tb30.NU_TEL_GERENTE;
                txtFaxAgencia.Text = tb30.NU_FAX_AGENCIA;
                ///Desabilitando os que não podem ser alterados
                ddlBanco.Enabled = false;
                txtCodigoAgencia.Enabled = false;
                txtDigitoAgencia.Enabled = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns></returns>
        private TB30_AGENCIA RetornaEntidade()
        {
            return TB30_AGENCIA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave("banco"), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy(b => b.DESBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();
        }
        #endregion
    }
}
