//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE DESPESAS (CONTAS A PAGAR)
// OBJETIVO: BAIXA DE TÍTULOS DE DESPESAS/COMPROMISSOS DE PAGAMENTOS
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5302_BaixaTituloDespesaPagto
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.RedirecionaParaPaginaErro("Funcionalidade apenas de alteração, não disponível inclusão.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            DateTime dataRecebimento = DateTime.Parse(txtDataRecebimento.Text);

            if (dataRecebimento > DateTime.Now.Date) 
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de recebimento não pode ser maior que a data atual");
                return;
            }

            if (txtValorRecebido.Text=="0,00")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor Recebido deve ser maior que zero");
                return;
            }

            TB38_CTA_PAGAR tb38 = RetornaEntidade();

            tb38.VR_MUL_PAG = txtValorMultaRecebido.Text != "" ? decimal.Parse(txtValorMultaRecebido.Text) : 0;
            tb38.VR_JUR_PAG = txtValorJurosRecebido.Text != "" ? decimal.Parse(txtValorJurosRecebido.Text) : 0;
            tb38.VR_DES_PAG = txtValorDescontoRecebido.Text != "" ? decimal.Parse(txtValorDescontoRecebido.Text) : 0;
            tb38.VR_PAG = txtValorRecebido.Text != "" ? decimal.Parse(txtValorRecebido.Text) : 0;
            tb38.DT_REC_DOC = dataRecebimento;
            tb38.IC_SIT_DOC = chkQuitado.Checked ? "Q" : "P";
            tb38.DT_ALT_REGISTRO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb38;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB38_CTA_PAGAR tb38 = RetornaEntidade();

            if (tb38 != null)
            {
                tb38.TB086_TIPO_DOCReference.Load();
                tb38.TB099_CENTRO_CUSTOReference.Load();
                tb38.TB101_LOCALCOBRANCAReference.Load();
                tb38.TB41_FORNECReference.Load();
                tb38.TB25_EMPRESAReference.Load();
                tb38.TB39_HISTORICOReference.Load();
                tb38.TB56_PLANOCTAReference.Load();

                txtDataRecebimento.Text = tb38.DT_REC_DOC != null ? tb38.DT_REC_DOC.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");

                TB37_RECDES_FIXA tb37 = null;

                if (tb38.IC_SIT_DOC == "Q")
                {
                    btnAtualizarValores.Visible = txtDataRecebimento.Enabled = txtValorDescontoRecebido.Enabled =
                    txtValorJurosRecebido.Enabled = txtValorMultaRecebido.Enabled = txtValorRecebido.Enabled = chkQuitado.Enabled = false;
                }
                else if (tb38.IC_SIT_DOC == "P")
                    txtValorDescontoRecebido.Enabled = txtValorJurosRecebido.Enabled = txtValorMultaRecebido.Enabled = false;

                chkQuitado.Checked = tb38.IC_SIT_DOC == "Q";

                if (tb38.CO_ADITI_DESFIX != null)
                    tb37 = TB37_RECDES_FIXA.RetornaPelaChavePrimaria(tb38.CO_EMP, tb38.CO_CON_DESFIX, tb38.CO_ADITI_DESFIX.Value);

                txtContrato.Text = tb37 != null ? tb37.CO_CON_RECDES : "";
                txtNumeroAditivo.Text = tb38.CO_ADITI_DESFIX.ToString();
                txtNumeroPublicacao.Text = tb37 != null ? tb37.NU_PUBLI_RECDES : "";
                txtNumeroParcela.Text = tb38.NU_PAR.ToString();
                txtQuantidadeParcelas.Text = tb38.QT_PAR.ToString();
                txtNumeroDocumento.Text = tb38.NU_DOC;
                txtTipoDocumento.Text = tb38.TB086_TIPO_DOC != null ? tb38.TB086_TIPO_DOC.DES_TIPO_DOC : "";

                txtValorDescontoRecebido.Text = (tb38.VR_DES_PAG != null ? tb38.VR_DES_PAG.Value : 0).ToString("N2");
                txtValorJurosRecebido.Text = (tb38.VR_JUR_PAG != null ? tb38.VR_JUR_PAG.Value : 0).ToString("N2");
                txtValorMultaRecebido.Text = (tb38.VR_MUL_PAG != null ? tb38.VR_MUL_PAG.Value : 0).ToString("N2");
                txtValorRecebido.Text = (tb38.VR_PAG != null ? tb38.VR_PAG.Value : 0).ToString("N2");

                txtRazaoSocial.Text = tb38.TB41_FORNEC.NO_FAN_FOR;

                txtCodigoFornecedor.Text = (tb38.TB41_FORNEC.TP_FORN == "F" && tb38.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 11) ? tb38.TB41_FORNEC.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                    ((tb38.TB41_FORNEC.TP_FORN == "J" && tb38.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 14) ? tb38.TB41_FORNEC.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb38.TB41_FORNEC.CO_CPFCGC_FORN);

                CalculaValores(tb38);

                txtDataCadastro.Text = tb38.DT_CAD_DOC.ToString("dd/MM/yyyy");
                txtDataVencimento.Text = tb38.DT_VEN_DOC.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB38_CTA_PAGAR</returns>
        private TB38_CTA_PAGAR RetornaEntidade()
        {
            DateTime dtCadas = DateTime.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("dtCadas"));
            string nuDoc = QueryStringAuxili.RetornaQueryStringPelaChave("doc");
            int nuPar = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("par");

            var tb38 = (from iTb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                        where iTb38.CO_EMP == LoginAuxili.CO_EMP
                        && iTb38.NU_DOC == nuDoc
                        && iTb38.NU_PAR == nuPar
                        && iTb38.DT_CAD_DOC.Year == dtCadas.Year && iTb38.DT_CAD_DOC.Month == dtCadas.Month
                        && iTb38.DT_CAD_DOC.Day == dtCadas.Day && iTb38.DT_CAD_DOC.Hour == dtCadas.Hour
                        && iTb38.DT_CAD_DOC.Minute == dtCadas.Minute && iTb38.DT_CAD_DOC.Second == dtCadas.Second
                        select iTb38).FirstOrDefault();            

            return tb38;
        }

        /// <summary>
        /// Método que faz o cálculo dos valores e preenche os campos correspondentes no formulário
        /// </summary>
        /// <param name="tb38">Entidade TB38_CTA_PAGAR</param>
        private void CalculaValores(TB38_CTA_PAGAR tb38)
        {
            txtValorParcela.Text = txtValor.Text = tb38.VR_PAR_DOC.ToString("N2");

            //--------> Recebe a data de Recebimento
            DateTime dtRecebimento = DateTime.Parse(txtDataRecebimento.Text).Date;

            decimal dcmValorMulta = 0;
            decimal dcmValorJuros = 0;
            decimal dcmValorDescto = 0;
            int diasAtraso = 0;

            //--------> Faz a verificação se o título está vencido, se sim, faz o cálculo de multas e juros
            if (dtRecebimento.Date > tb38.DT_VEN_DOC.Date)
            {
                dcmValorMulta = (tb38.CO_FLAG_TP_VALOR_MUL == "V" ?
                    (tb38.VR_MUL_DOC.HasValue ? tb38.VR_MUL_DOC : 0) :
                    (tb38.VR_PAR_DOC * (tb38.VR_MUL_DOC.HasValue ? tb38.VR_MUL_DOC : 0) / 100)).Value;

                //------------> Faz o cálculo do juros de acordo com o valor da parcela, valor do juros e dias de atraso
                diasAtraso = (dtRecebimento - tb38.DT_VEN_DOC.Date).Days;
                dcmValorJuros = (tb38.CO_FLAG_TP_VALOR_JUR == "V" ?
                    (tb38.VR_JUR_DOC.HasValue ? tb38.VR_JUR_DOC : 0) :
                    (tb38.VR_PAR_DOC * diasAtraso * (tb38.VR_JUR_DOC.HasValue ? tb38.VR_JUR_DOC : 0) / 100)).Value;
            }

            dcmValorDescto = (tb38.CO_FLAG_TP_VALOR_DES == "V" ?
                    (tb38.VR_DES_DOC.HasValue ? tb38.VR_DES_DOC : 0) :
                    (tb38.VR_PAR_DOC * (tb38.VR_DES_DOC.HasValue ? tb38.VR_DES_DOC : 0) / 100)).Value;

            txtValorJuros.Text = dcmValorJuros.ToString("N2");
            txtValorJurosRecebido.Text = (dcmValorJuros * diasAtraso).ToString("N2");
            txtValorMulta.Text = txtValorMultaRecebido.Text = dcmValorMulta.ToString("N2");
            txtValorDesconto.Text = txtValorDescontoRecebido.Text = dcmValorDescto.ToString("N2");
            txtValorRecebido.Text = (tb38.VR_PAR_DOC + dcmValorMulta + (dcmValorJuros * diasAtraso) - dcmValorDescto).ToString("N2");
        }  
        #endregion

        protected void btnAtualizarValores_Click(object sender, EventArgs e)
        {
            decimal valorFinal = 0;

            valorFinal = txtValor.Text != "" ? decimal.Parse(txtValor.Text) : 0;

            valorFinal = txtValorMultaRecebido.Text != "" ? valorFinal + decimal.Parse(txtValorMultaRecebido.Text) : 0;

            valorFinal = txtValorJurosRecebido.Text != "" ? valorFinal + decimal.Parse(txtValorJurosRecebido.Text) : valorFinal;

            valorFinal = txtValorDescontoRecebido.Text != "" ? valorFinal - decimal.Parse(txtValorDescontoRecebido.Text) : valorFinal;

            if (valorFinal < 0)
            {
                CalculaValores(RetornaEntidade());
            }
            else
            {
                txtValorRecebido.Text = valorFinal.ToString("N2");
            }
        }
    }
}