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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5908_ContaCorrente
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
            {
                CarregaBancos();
                CarregaAgencias();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDataSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
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
                TB224_CONTA_CORRENTE tb224 = RetornaEntidade();

                decimal decimalRetorno;

                if (tb224 == null)
                {
                    tb224 = new TB224_CONTA_CORRENTE();

                    tb224.IDEBANCO = ddlBanco.SelectedValue;
                    tb224.CO_AGENCIA = int.Parse(ddlAgencia.SelectedValue);
                    tb224.CO_CONTA = txtNumeroConta.Text;
                    tb224.TB30_AGENCIA = TB30_AGENCIA.RetornaPelaChavePrimaria(tb224.IDEBANCO, tb224.CO_AGENCIA);
                    tb224.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                }

                tb224.CO_DIG_CONTA = txtDigitoConta.Text.ToUpper();
                tb224.VL_LIMITE_CH_ESP = decimal.TryParse(txtLimiteChequeEspecial.Text, out decimalRetorno) ? decimalRetorno : (decimal?)null;
                tb224.DT_VENCTO_CH_ESP = txtDataVencimentoChequeEspecial.Text != "" ? DateTime.Parse(txtDataVencimentoChequeEspecial.Text) : (DateTime?)null;
                tb224.VL_LIMITE_CREDITO = decimal.TryParse(txtLimiteCredito.Text, out decimalRetorno) ? decimalRetorno : (decimal?)null;
                tb224.FLAG_EMITE_BOLETO_BANC = ddlFlagEmiteBoleto.SelectedValue;
                tb224.NO_GER_CTA = txtNomeGerenteConta.Text != "" ? txtNomeGerenteConta.Text : null;
                tb224.NU_TEL_GER_CTA = txtTelefoneGerenteConta.Text != "" ? txtTelefoneGerenteConta.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") : null;
                tb224.NO_EMAIL_GER_CTA = txtEmailGerenteConta.Text != "" ? txtEmailGerenteConta.Text : null;
                tb224.DT_ABERT_CTA = DateTime.Parse(txtDataAberturaConta.Text);
                tb224.TP_CONTA = ddlTipoConta.SelectedValue;
                tb224.CO_STATUS = ddlSituacao.SelectedValue;
                tb224.DT_STATUS = DateTime.Parse(txtDataSituacao.Text);

                CurrentPadraoCadastros.CurrentEntity = tb224;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB224_CONTA_CORRENTE tb224 = RetornaEntidade();

            if (tb224 != null)
            {
                ddlBanco.SelectedValue = tb224.IDEBANCO;
                CarregaAgencias();
                ddlAgencia.SelectedValue = tb224.CO_AGENCIA.ToString();
                txtNumeroConta.Text = tb224.CO_CONTA;
                txtDigitoConta.Text = tb224.CO_DIG_CONTA;
                txtLimiteChequeEspecial.Text = tb224.VL_LIMITE_CH_ESP != null ? string.Format("{0:N2}", tb224.VL_LIMITE_CH_ESP.Value) : "";
                txtDataVencimentoChequeEspecial.Text = tb224.DT_VENCTO_CH_ESP != null ? tb224.DT_VENCTO_CH_ESP.Value.ToString("dd/MM/yyyy") : "";
                txtLimiteCredito.Text = tb224.VL_LIMITE_CREDITO != null ? string.Format("{0:N2}", tb224.VL_LIMITE_CREDITO.Value) : ""; ;
                ddlFlagEmiteBoleto.SelectedValue = tb224.FLAG_EMITE_BOLETO_BANC;
                txtNomeGerenteConta.Text = tb224.NO_GER_CTA;
                txtTelefoneGerenteConta.Text = tb224.NU_TEL_GER_CTA;
                txtEmailGerenteConta.Text = tb224.NO_EMAIL_GER_CTA;
                txtDataAberturaConta.Text = tb224.DT_ABERT_CTA.ToString("dd/MM/yyyy");
                ddlTipoConta.SelectedValue = tb224.TP_CONTA;
                ddlSituacao.SelectedValue = tb224.CO_STATUS;
                txtDataSituacao.Text = tb224.DT_STATUS.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB224_CONTA_CORRENTE</returns>
        private TB224_CONTA_CORRENTE RetornaEntidade()
        {
            return TB224_CONTA_CORRENTE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave("ideBanco"),
                                                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("coAgencia"),
                                                QueryStringAuxili.RetornaQueryStringPelaChave("coConta"));
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

        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        private void CarregaAgencias()
        {
            string strIdeBanco = ddlBanco.SelectedValue;

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros().AsEnumerable()
                                     where tb30.IDEBANCO == strIdeBanco
                                     select new
                                     {
                                         tb30.CO_AGENCIA,
                                         DESCRICAO = string.Format("{0} - {1}", tb30.CO_AGENCIA, tb30.NO_AGENCIA)
                                     }).OrderBy(a => a.DESCRICAO);

            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataTextField = "DESCRICAO";
            ddlAgencia.DataBind();
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
        }
    }
}
