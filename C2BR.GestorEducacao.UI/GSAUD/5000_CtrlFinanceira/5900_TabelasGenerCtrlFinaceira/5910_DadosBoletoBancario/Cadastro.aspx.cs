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
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5910_DadosBoletoBancario
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
                CarregaContas();
                CarregaCarteiras();

                txtVlBolCLi.Enabled = false;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (chkTxBolCli.Checked == true)
            {
                if (txtVlBolCLi.Text == "" || txtVlBolCLi.Text == "0,00")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Taxa de Boleto ao Cliente deve ser informado se a caixa correspondente esteja marcada!");
                    return;
                }
                if (txtVlBolCLi.Text.Length > 5)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Taxa de Boleto ao Cliente deve ser no máximo R$ 99,99");
                    return;
                }
            }
           
            

            if (Page.IsValid)
            {
                TB227_DADOS_BOLETO_BANCARIO tb227 = RetornaEntidade();

                if (tb227 == null)
                    tb227 = new TB227_DADOS_BOLETO_BANCARIO();

                string strIdeBanco = ddlBanco.SelectedValue;
                int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;
                string coConta = ddlConta.SelectedValue;

                tb227.TB224_CONTA_CORRENTE = TB224_CONTA_CORRENTE.RetornaPelaChavePrimaria(strIdeBanco, coAgencia, coConta);
                tb227.IDEBANCO_CARTEIRA = strIdeBanco;
                tb227.CO_CARTEIRA = ddlCarteira.SelectedValue.Trim();
                tb227.NU_CONVENIO = int.Parse(txtNumeroConvenio.Text);
                tb227.CO_CEDENTE = txtCodigoCedente.Text.Trim();
                tb227.DE_INSTR1_BOLETO_BANCO = txtInstrucoesL1.Text != "" ? txtInstrucoesL1.Text.Trim() : null;
                tb227.DE_INSTR2_BOLETO_BANCO = txtInstrucoesL2.Text != "" ? txtInstrucoesL2.Text.Trim() : null;
                tb227.DE_INSTR3_BOLETO_BANCO = txtInstrucoesL3.Text != "" ? txtInstrucoesL3.Text.Trim() : null;
                tb227.TP_TAXA_BOLETO = ddlTipoTaxaBoleto.SelectedValue.Trim();
                               
                tb227.FL_TX_BOL_CLI = chkTxBolCli.Checked == true ? "S" : "N";
                decimal valorTaxa = new decimal();

                if (chkTxBolCli.Checked && txtVlBolCLi.Text != "")
                    decimal.TryParse(txtVlBolCLi.Text, out valorTaxa);
                tb227.VR_TX_BOL_CLI = valorTaxa;
                CurrentPadraoCadastros.CurrentEntity = tb227;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB227_DADOS_BOLETO_BANCARIO tb227 = RetornaEntidade();

            if (tb227 != null)
            {
                tb227.TB224_CONTA_CORRENTEReference.Load();
                tb227.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();

                ddlBanco.SelectedValue = tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.IDEBANCO;
                CarregaAgencias();
                ddlAgencia.SelectedValue = tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA.ToString();
                CarregaCarteiras();
                string valor = tb227.CO_CARTEIRA;
                ddlCarteira.SelectedValue = valor;
                CarregaContas();
                ddlConta.SelectedValue = tb227.TB224_CONTA_CORRENTE.CO_CONTA;
                txtNumeroConvenio.Text = tb227.NU_CONVENIO.ToString();
                txtCodigoCedente.Text = tb227.CO_CEDENTE;
                txtInstrucoesL1.Text = tb227.DE_INSTR1_BOLETO_BANCO;
                txtInstrucoesL2.Text = tb227.DE_INSTR2_BOLETO_BANCO;
                txtInstrucoesL3.Text = tb227.DE_INSTR3_BOLETO_BANCO;
                ddlTipoTaxaBoleto.SelectedValue = tb227.TP_TAXA_BOLETO;
                if (tb227.FL_TX_BOL_CLI == "S")
                {
                    txtVlBolCLi.Text = tb227.VR_TX_BOL_CLI.ToString();
                    chkTxBolCli.Checked = true;
                    txtVlBolCLi.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB227_DADOS_BOLETO_BANCARIO</returns>
        private TB227_DADOS_BOLETO_BANCARIO RetornaEntidade()
        {
            return (TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id)));
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
                                     where (strIdeBanco != "" ? tb30.IDEBANCO == strIdeBanco : strIdeBanco == "")
                                     select new
                                     {
                                         tb30.CO_AGENCIA,
                                         DESCRICAO = string.IsNullOrEmpty(ddlBanco.SelectedValue) ?
                                                 string.Format("({0}) {1} - {2}", tb30.IDEBANCO, tb30.CO_AGENCIA, tb30.NO_AGENCIA) :
                                                 string.Format("{0} - {1}", tb30.CO_AGENCIA, tb30.NO_AGENCIA)
                                     }).OrderBy(a => a.DESCRICAO);

            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataTextField = "DESCRICAO";
            ddlAgencia.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Contas
        /// </summary>
        private void CarregaContas()
        {
            string strIdeBanco = ddlBanco.SelectedValue;
            int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;

            ddlConta.DataSource = (from tb224 in TB224_CONTA_CORRENTE.RetornaTodosRegistros()
                                   where tb224.TB30_AGENCIA.IDEBANCO == strIdeBanco && tb224.TB30_AGENCIA.CO_AGENCIA == coAgencia
                                   && tb224.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                   select new { tb224.CO_CONTA }).OrderBy( a => a.CO_CONTA );

            ddlConta.DataValueField = "CO_CONTA";
            ddlConta.DataTextField = "CO_CONTA";
            ddlConta.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Carteiras
        /// </summary>
        private void CarregaCarteiras()
        {
            string strIdeBanco = ddlBanco.SelectedValue;

            ddlCarteira.DataSource = (from tb226 in TB226_CARTEIRA_BANCO.RetornaTodosRegistros()
                                      where tb226.TB29_BANCO.IDEBANCO == strIdeBanco
                                      select new { tb226.CO_CARTEIRA, tb226.DE_CARTEIRA });

            ddlCarteira.DataValueField = "CO_CARTEIRA";
            ddlCarteira.DataTextField = "DE_CARTEIRA";
            ddlCarteira.DataBind();
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
            CarregaContas();
            CarregaCarteiras();
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContas();
        }

        protected void chkTxBolCliChanged_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTxBolCli.Checked == true)
            {
                txtVlBolCLi.Enabled = true;
            }
            else
            {
                txtVlBolCLi.Enabled = false;
                txtVlBolCLi.Text = "";
            }
        }
    }
}
