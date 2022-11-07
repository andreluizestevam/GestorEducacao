//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE GERAL DE CHEQUES
// OBJETIVO: CADASTRAMENTO DE CHEQUES EMITIDOS OU RECEBIDOS PELA INSTITUIÇÃO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5811_CadastramentoChequeEmitidoInstit
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
                CarregaNomeFantasia();
                CarregaBanco();
                CarregaAgencia();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlSituacao.SelectedValue = "A";
                    txtDataSituacao.Text = txtDtEmissao.Text = dataAtual;
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    txtNumDocto.Enabled = false;

                CarregaFormulario();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                DateTime dtEmissao, dtVencimento;
                if (txtDtEmissao.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Selecione a data de emitir.");
                    return;
                }
                else
                    dtEmissao = DateTime.Parse(txtDtEmissao.Text);
                if (txtDtVencimento.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Selecione a data de vencimento.");
                    return;
                }
                else
                {
                    dtVencimento = DateTime.Parse(txtDtVencimento.Text);
                    if (dtEmissao > dtVencimento)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "A data de vencimento tem que ser maior ou igual que a data para emitir.");
                        return;
                    }
                }
                if (txtCPF.Text == "" && !AuxiliValidacao.ValidaCpf(txtCPF.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "O CPF informado é inválido. Por favor informe um CPF válido.");
                    return;
                }
                tb158_cheques tb158 = RetornaEntidade();
                
                if (tb158 == null)
                {
                    tb158 = new tb158_cheques();
                    tb158.TB29_BANCO = TB29_BANCO.RetornaPelaChavePrimaria(ddlBanco.SelectedValue);
                    tb158.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb158.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                }
                else
                {
                    tb158.TB29_BANCOReference.Load();
                    tb158.TB25_EMPRESAReference.Load();
                    tb158.TB000_INSTITUICAOReference.Load();
                    tb158.TB25_EMPRESAReference.Load();
                }

                tb158.co_agencia = int.Parse(ddlAgencia.SelectedValue);
                tb158.nome = txtNomeCheque.Text;
                tb158.nu_cheque = txtNumCheque.Text;
                tb158.tp_cliente_cheque = ddlTpCliente.SelectedValue;
                if (ddlTpCliente.SelectedValue == "R")
                    tb158.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(ddlNomeFantasia.SelectedValue));
                else
                    tb158.TB103_CLIENTE = TB103_CLIENTE.RetornaPelaChavePrimaria(int.Parse(ddlNomeFantasia.SelectedValue));
                tb158.fl_depositar = !cbDepositar.Checked;
                tb158.dt_emissao = dtEmissao;
                tb158.cpf = txtCPF.Text.Replace(".", "").Replace("-", "");
                tb158.dt_sit = DateTime.Now;
                tb158.dt_vencimento = dtVencimento;
                tb158.ic_sit = ddlSituacao.SelectedValue;
                tb158.nu_cheque = txtNumCheque.Text;
                tb158.nu_conta = txtConta.Text;
                tb158.nu_doc = txtNumDocto.Text;
                tb158.observacao = txtObservacoes.Text != "" ? txtObservacoes.Text : null;
                tb158.valor = Decimal.Parse(txtValor.Text);

                CurrentPadraoCadastros.CurrentEntity = tb158;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            tb158_cheques tb158 = RetornaEntidade();

            if (tb158 != null)
            {
                tb158.TB000_INSTITUICAOReference.Load();
                tb158.TB25_EMPRESAReference.Load();
                tb158.TB29_BANCOReference.Load();

                if (tb158.tp_cliente_cheque == "R")
                    tb158.TB108_RESPONSAVELReference.Load();
                else
                    tb158.TB103_CLIENTEReference.Load();

                ddlBanco.SelectedValue = tb158.TB29_BANCO.IDEBANCO;
                CarregaAgencia();
                ddlAgencia.SelectedValue = tb158.co_agencia.ToString();

                if (tb158.tp_cliente_cheque == "R")
                    ddlNomeFantasia.SelectedValue = tb158.TB108_RESPONSAVEL.CO_RESP.ToString();
                else
                    ddlNomeFantasia.SelectedValue = tb158.TB103_CLIENTE.CO_CLIENTE.ToString();

                txtConta.Text = tb158.nu_conta.ToString();
                txtNumCheque.Text = tb158.nu_cheque;
              
                txtValor.Text = tb158.valor.ToString("#,##0.00");
                cbDepositar.Checked = !(tb158.fl_depositar ?? true);
                txtNomeCheque.Text = tb158.nome;
                txtCPF.Text = tb158.cpf;
                txtDtEmissao.Text = tb158.dt_emissao.ToString("dd/MM/yyyy");
                txtDtVencimento.Text = tb158.dt_vencimento.ToString("dd/MM/yyyy");
                txtNumDocto.Text = tb158.nu_doc;
                txtObservacoes.Text = tb158.observacao != null ? tb158.observacao : "";
                ddlSituacao.SelectedValue = tb158.ic_sit;
                txtDataSituacao.Text = tb158.dt_sit.ToString();          
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade tb158_cheques</returns>
        private tb158_cheques RetornaEntidade()
        {
            return tb158_cheques.RetornaPelaChavePrimaria(int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("coCheque") != null ? QueryStringAuxili.RetornaQueryStringPelaChave("coCheque") : "0"), int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("codOrgao") != null ? QueryStringAuxili.RetornaQueryStringPelaChave("codOrgao") : "0"));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        protected void CarregaBanco()
        {
            ddlBanco.DataSource = TB29_BANCO.RetornaTodosRegistros();

            ddlBanco.DataTextField = "IDEBANCO";
            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        protected void CarregaAgencia()
        {
            string ideBanco = ddlBanco.SelectedValue != "" ? ddlBanco.SelectedValue : "0";

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros()
                                     where tb30.IDEBANCO == ideBanco
                                     select new { tb30.CO_AGENCIA }).OrderBy(a => a.CO_AGENCIA);

            ddlAgencia.DataTextField = "CO_AGENCIA";
            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataBind();

            ddlAgencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Nome Fantasia
        /// </summary>
        protected void CarregaNomeFantasia()
        {
            if (ddlTpCliente.SelectedValue == "R")
            {
                ddlNomeFantasia.DataSource = TB108_RESPONSAVEL.RetornaTodosRegistros().Where( r => r.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO );

                ddlNomeFantasia.DataTextField = "NO_RESP";
                ddlNomeFantasia.DataValueField = "CO_RESP";
                ddlNomeFantasia.DataBind();

                ddlNomeFantasia.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlNomeFantasia.DataSource = TB103_CLIENTE.RetornaTodosRegistros().OrderBy(c => c.NO_FAN_CLI);

                ddlNomeFantasia.DataTextField = "NO_FAN_CLI";
                ddlNomeFantasia.DataValueField = "CO_CLIENTE";
                ddlNomeFantasia.DataBind();

                ddlNomeFantasia.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencia();
        }

        protected void ddlTpCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaNomeFantasia();
        }        
    }
}