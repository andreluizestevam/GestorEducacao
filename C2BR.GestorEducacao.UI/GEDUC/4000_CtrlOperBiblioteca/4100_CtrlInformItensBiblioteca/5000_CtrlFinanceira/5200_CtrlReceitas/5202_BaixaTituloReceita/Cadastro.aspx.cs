//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: BAIXA DE TÍTULOS DE RECEITAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/05/2013| André Nobre Vinagre        |Corrigido para pegar o responsável pelo título e não o
//           |                            |responsável do aluno.
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5202_BaixaTituloReceita
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

            if (!Page.IsPostBack)
            {
                txtDataRecebimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtValorRecebido.Enabled = txtValorJurosRecebido.Enabled = txtValorMultaRecebido.Enabled =
                txtValorDescontoRecebido.Enabled = txtValorDescontoBolsaRecebido.Enabled = txtValorRecebido.Enabled = true;
                CarregarTipoRecebimento();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB47_CTA_RECEB tb47 = RetornaEntidade();

            if (txtValorExcedente.Text != "")
            {
                if (tb47.VL_EXCE_PAG.HasValue)
                    tb47.VL_EXCE_PAG += decimal.Parse(txtValorExcedente.Text);
                else
                    tb47.VL_EXCE_PAG = decimal.Parse(txtValorExcedente.Text);
            }
            //tb47.VL_EXCE_PAG = txtValorExcedente.Text != "" ? decimal.Parse(txtValorExcedente.Text) : 0;

            if (txtValorMultaRecebido.Text != "")
            {
                if (tb47.VR_MUL_PAG.HasValue)
                    tb47.VR_MUL_PAG += decimal.Parse(txtValorMultaRecebido.Text);
                else
                    tb47.VR_MUL_PAG = decimal.Parse(txtValorMultaRecebido.Text);
            }
            //tb47.VR_MUL_PAG = txtValorMultaRecebido.Text != "" ? decimal.Parse(txtValorMultaRecebido.Text) : 0;

            if (txtValorJurosRecebido.Text != "")
            {
                if (tb47.VR_JUR_PAG.HasValue)
                    tb47.VR_JUR_PAG += decimal.Parse(txtValorJurosRecebido.Text);
                else
                    tb47.VR_JUR_PAG = decimal.Parse(txtValorJurosRecebido.Text);
            }
            //tb47.VR_JUR_PAG = txtValorJurosRecebido.Text != "" ? decimal.Parse(txtValorJurosRecebido.Text) : 0;

            if (txtValorDescontoRecebido.Text != "")
            {
                if (tb47.VR_DES_PAG.HasValue)
                    tb47.VR_DES_PAG += decimal.Parse(txtValorDescontoRecebido.Text);
                else
                    tb47.VR_DES_PAG = decimal.Parse(txtValorDescontoRecebido.Text);
            }
            //tb47.VR_DES_PAG = txtValorDescontoRecebido.Text != "" ? decimal.Parse(txtValorDescontoRecebido.Text) : 0;

            if (txtValorDescontoBolsaRecebido.Text != "")
            {
                if (tb47.VR_DES_BOLSA_PAG.HasValue)
                    tb47.VR_DES_BOLSA_PAG += decimal.Parse(txtValorDescontoBolsaRecebido.Text);
                else
                    tb47.VR_DES_BOLSA_PAG = decimal.Parse(txtValorDescontoBolsaRecebido.Text);
            }
            //tb47.VR_DES_BOLSA_PAG = txtValorDescontoBolsaRecebido.Text != "" ? decimal.Parse(txtValorDescontoBolsaRecebido.Text) : 0;

            if (txtOutrosValoresRecebidos.Text != "")
            {
                if (tb47.VR_OUT_PAG.HasValue)
                    tb47.VR_OUT_PAG += decimal.Parse(txtOutrosValoresRecebidos.Text);
                else
                    tb47.VR_OUT_PAG = decimal.Parse(txtOutrosValoresRecebidos.Text);
            }
            //tb47.VR_OUT_PAG = txtOutrosValores.Text != "" ? decimal.Parse(txtOutrosValores.Text) : 0;
            
            if (txtValorRecebido.Text != "")
            {
                if (tb47.VR_PAG.HasValue)
                    tb47.VR_PAG += decimal.Parse(txtValorRecebido.Text);
                else
                    tb47.VR_PAG = decimal.Parse(txtValorRecebido.Text);
            }

            tb47.FL_TIPO_RECEB = ddlTipoReceb.SelectedValue;
            tb47.FL_ORIGEM_PGTO = "X";
            tb47.CO_COL_BAIXA = LoginAuxili.CO_COL;
            tb47.DT_REC_DOC = DateTime.Parse(txtDataRecebimento.Text);
            tb47.DT_MOV_CAIXA = DateTime.Parse(txtDataRecebimento.Text);
            tb47.DT_ALT_REGISTRO = DateTime.Now;
            tb47.IC_SIT_DOC = chkQuitado.Checked ? "Q" : "P";

            CurrentPadraoCadastros.CurrentEntity = tb47;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB47_CTA_RECEB tb47 = RetornaEntidade();

            if (tb47 != null)
            {
                TB37_RECDES_FIXA tb37 = null;

                if (tb47.CO_ADITI_RECFIX.HasValue)
                    tb37 = TB37_RECDES_FIXA.RetornaPelaChavePrimaria(tb47.CO_EMP, tb47.CO_CON_RECFIX, tb47.CO_ADITI_RECFIX.Value);

                tb47.TB086_TIPO_DOCReference.Load();
                tb47.TB099_CENTRO_CUSTOReference.Load();
                tb47.TB101_LOCALCOBRANCAReference.Load();
                tb47.TB103_CLIENTEReference.Load();
                tb47.TB116_TIPO_DEV_DOCReference.Load();
                tb47.TB25_EMPRESAReference.Load();
                tb47.TB39_HISTORICOReference.Load();
                tb47.TB56_PLANOCTAReference.Load();
                tb47.TB108_RESPONSAVELReference.Load();

                txtContrato.Text = tb37 != null ? tb37.CO_CON_RECDES : "";
                txtNumeroAditivo.Text = tb47.CO_ADITI_RECFIX.ToString();
                txtNumeroPublicacao.Text = tb37 != null ? tb37.NU_PUBLI_RECDES : "";
                txtNumeroParcela.Text = tb47.NU_PAR.ToString();
                txtQuantidadeParcelas.Text = tb47.QT_PAR.ToString();
                txtNumeroDocumento.Text = tb47.NU_DOC;
                txtTipoDocumento.Text = tb47.TB086_TIPO_DOC != null ? tb47.TB086_TIPO_DOC.DES_TIPO_DOC : "";
                chkQuitado.Checked = tb47.IC_SIT_DOC == "Q";
                txtValorTotalPago.Text = tb47.VR_PAG != null ? tb47.VR_PAG.Value.ToString("N2") : "0";

                if (tb47.TP_CLIENTE_DOC == "A" || tb47.TP_CLIENTE_DOC == "R")
                {
                    //var aluno = (from a in TB07_ALUNO.RetornaTodosRegistros()
                    //             where a.CO_ALU == tb47.CO_ALU
                    //             select new { a.TB108_RESPONSAVEL.NO_RESP, a.TB108_RESPONSAVEL.NU_CPF_RESP, a.NO_ALU, a.NU_NIRE }).FirstOrDefault();

                    lblDescNome.InnerText = "Nome do Responsável";

                    if (tb47.TB108_RESPONSAVEL != null)
                    {
                        txtCodigo.Text = tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".");
                        txtNome.Text = tb47.TB108_RESPONSAVEL.NO_RESP;
                    }                    
                }
                else
                {                    
                    lblDescNome.InnerText = "Nome do Cliente";
                    txtCodigo.Text = tb47.TB103_CLIENTE.TP_CLIENTE == "F" && tb47.TB103_CLIENTE.CO_CPFCGC_CLI.Length >= 11 ?
                        tb47.TB103_CLIENTE.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                        ((tb47.TB103_CLIENTE.TP_CLIENTE == "J" && tb47.TB103_CLIENTE.CO_CPFCGC_CLI.Length >= 14) ?
                            tb47.TB103_CLIENTE.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.TB103_CLIENTE.CO_CPFCGC_CLI);
                    txtNome.Text = tb47.TB103_CLIENTE.NO_FAN_CLI;
                }

//------------> Faz o carregamento dos valores necessários
                if (tb47.IC_SIT_DOC == "P")
                {
                    decimal valorResidual = 0;

                    CarregaValoresTitulo(tb47);

                    valorResidual = decimal.Parse(txtValorTotal.Text) - decimal.Parse(txtValorTotalPago.Text);

                    if (valorResidual > 0)
                        txtValorResidual.Text = (valorResidual).ToString("N2");
                    else
                    {
                        valorResidual = 0;
                        txtValorResidual.Text = (valorResidual).ToString("N2");
                    }

                    txtValorRecebido.Text = txtValorResidual.Text;
                }
                else
                {
                    CarregaValoresTitulo(tb47);

                    txtValorJurosRecebido.Text = txtValorJuros.Text;
                    txtValorMultaRecebido.Text = txtValorMulta.Text;
                    txtValorDescontoRecebido.Text = txtValorDesconto.Text;
                    txtValorDescontoBolsaRecebido.Text = txtDescontoBolsa.Text;
                    txtOutrosValoresRecebidos.Text = txtOutrosValores.Text;
                    txtValorResidual.Text = txtValorParcela.Text;
                    txtValorRecebido.Text = txtValorTotal.Text;
                }

                decimal dcmValorExcedente = Convert.ToDecimal(tb47.VR_PAG.HasValue ? tb47.VR_PAG : 0);
                decimal dcmValorTotalPago = decimal.Parse(txtValorTotalPago.Text) - decimal.Parse(txtValorTotal.Text);
                dcmValorExcedente -= tb47.VR_PAR_DOC;

                if (dcmValorTotalPago > 0)
                    txtValorExcedente.Text = dcmValorTotalPago.ToString("N2");
                            

                txtDataCadastro.Text = tb47.DT_CAD_DOC.ToString("dd/MM/yyyy");
                txtDataVencimento.Text = tb47.DT_VEN_DOC.ToString("dd/MM/yyyy");

//------------> Carrega a quantidade de dias do documento
                DateTime dataRecebimento = DateTime.Parse(txtDataRecebimento.Text).Date;
                int diasAtraso = (dataRecebimento - tb47.DT_VEN_DOC.Date).Days;
                if (diasAtraso > 0)
                {
                    txtDiasDocto.Text = "+" + diasAtraso.ToString();
                    txtDiasDocto.ForeColor = System.Drawing.Color.Red;
                }
                else if (diasAtraso < 0)
                {
                    txtDiasDocto.Text = diasAtraso.ToString();
                    txtDiasDocto.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    txtDiasDocto.Text = "0";                    
                    txtDiasDocto.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB47_CTA_RECEB</returns>
        private TB47_CTA_RECEB RetornaEntidade()
        {           
            DateTime dtCadas = DateTime.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("dtCadas"));
            string nuDoc = QueryStringAuxili.RetornaQueryStringPelaChave("doc");
            int nuPar = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("par");

            var tb47 = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                        where iTb47.CO_EMP == LoginAuxili.CO_EMP
                        && iTb47.NU_DOC == nuDoc
                        && iTb47.NU_PAR == nuPar
                        && iTb47.DT_CAD_DOC.Year == dtCadas.Year && iTb47.DT_CAD_DOC.Month == dtCadas.Month
                        && iTb47.DT_CAD_DOC.Day == dtCadas.Day && iTb47.DT_CAD_DOC.Hour == dtCadas.Hour
                        && iTb47.DT_CAD_DOC.Minute == dtCadas.Minute && iTb47.DT_CAD_DOC.Second == dtCadas.Second
                        select iTb47).FirstOrDefault();

            return tb47;
        }

        /// <summary>
        /// Método que carrega informações de valores do Título
        /// </summary>
        /// <param name="tb47">Entidade TB47_CTA_RECEB</param>
        private void CarregaValoresTitulo(TB47_CTA_RECEB tb47)
        {
            ///Recebe a data de Recebimento
            DateTime dataRecebimento = DateTime.Parse(txtDataRecebimento.Text).Date;
            AuxiliCalculos.valoresCalculadosTitulo valores = AuxiliCalculos.calculaValoresTitulo(tb47, dataRecebimento);

            txtValorParcela.Text = valores.valorParcela.toReal();
            txtValorJuros.Text = valores.valorJuros.toReal(); ;
            txtValorMulta.Text = valores.valorDesconto.toReal();
            txtValorDesconto.Text = valores.valorDesconto.toReal();
            txtDescontoBolsa.Text = valores.valorDescontoBolsa.toReal();
            txtOutrosValores.Text = valores.valorOutros.toReal();
            txtValorTotal.Text = valores.valorTotal.toReal();
        }

        /// <summary>
        /// Método que faz os cálculo dos Valores Pagos
        /// </summary>
        /// <param name="tb47">Entidade TB47_CTA_RECEB</param>
        private void CalculaValoresPagos(TB47_CTA_RECEB tb47)
        {
            txtValorParcela.Text = (tb47.VR_PAR_DOC).ToString("N2");

//--------> Recebe a data de Recebimento
            DateTime dataRecebimento = DateTime.Parse(txtDataRecebimento.Text).Date;

            decimal dcmValorMulta = 0;
            decimal dcmValorJuros = 0;
            decimal dcmValorDescto = 0;
            decimal dcmValorOutros = 0;
            decimal dcmValorDesctoBolsa = 0;

//--------> Faz a verificação se o título está vencido, se sim, faz o cálculo de multas e juros
            if (dataRecebimento.Date > tb47.DT_VEN_DOC.Date)
            {
                dcmValorMulta = (tb47.CO_FLAG_TP_VALOR_MUL == "V" ?
                    (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) :
                    (tb47.VR_PAR_DOC * (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) / 100)).Value;

//------------> Faz o cálculo do juros de acordo com o valor da parcela, valor do juros e dias de atraso
                int diasAtraso = (dataRecebimento - tb47.DT_VEN_DOC.Date).Days;
                dcmValorJuros = (tb47.CO_FLAG_TP_VALOR_JUR == "V" ?
                    (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) :
                    (tb47.VR_PAR_DOC * diasAtraso * (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) / 100)).Value;

                dcmValorOutros = (tb47.CO_FLAG_TP_VALOR_OUT == "V" ?
                    (tb47.VR_OUT_DOC != null ? tb47.VR_OUT_DOC : 0) :
                    (tb47.VR_PAR_DOC * (tb47.VR_OUT_DOC != null ? tb47.VR_OUT_DOC : 0) / 100)).Value;
            }

            dcmValorDescto = (tb47.CO_FLAG_TP_VALOR_DES == "V" ?
                    (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) :
                    (tb47.VR_PAR_DOC * (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) / 100)).Value;

            dcmValorDesctoBolsa = (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "V" ?
                    (tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0) :
                    (tb47.VR_PAR_DOC * (tb47.VL_DES_BOLSA_ALUNO != null ? tb47.VL_DES_BOLSA_ALUNO : 0) / 100)).Value;

            txtValorJurosRecebido.Text = (dcmValorJuros).ToString("N2");
            txtValorMultaRecebido.Text = (dcmValorMulta).ToString("N2");
            txtValorDescontoRecebido.Text = (dcmValorDescto).ToString("N2");
            txtValorDescontoBolsaRecebido.Text = (dcmValorDesctoBolsa).ToString("N2");
            txtOutrosValoresRecebidos.Text = (dcmValorOutros).ToString("N2");
            txtValorRecebido.Text = (tb47.VR_PAR_DOC + dcmValorMulta + dcmValorJuros - dcmValorDescto - dcmValorDesctoBolsa + dcmValorOutros - (tb47.VR_PAG != null ? tb47.VR_PAG : 0)).Value.ToString("N2");
        }

        /// <summary>
        /// Método que faz os cálculo dos Valores Pagos
        /// </summary>
        private void CalculaValoresPagos()
        {
            decimal dcmValorMulta = 0;
            decimal dcmValorJuros = 0;
            decimal dcmValorDescto = 0;
            decimal dcmValorOutros = 0;
            decimal dcmValorDesctoBolsa = 0;

            dcmValorMulta = txtValorMultaRecebido.Text != "" ? decimal.Parse(txtValorMultaRecebido.Text) : 0;
            dcmValorJuros = txtValorJurosRecebido.Text != "" ? decimal.Parse(txtValorJurosRecebido.Text) : 0;
            dcmValorDescto = txtValorDescontoRecebido.Text != "" ? decimal.Parse(txtValorDescontoRecebido.Text) : 0;
            dcmValorOutros = txtOutrosValoresRecebidos.Text != "" ? decimal.Parse(txtOutrosValoresRecebidos.Text) : 0;
            dcmValorDesctoBolsa = txtValorDescontoBolsaRecebido.Text != "" ? decimal.Parse(txtValorDescontoBolsaRecebido.Text) : 0;

            txtValorRecebido.Text = (decimal.Parse(txtValorResidual.Text) + dcmValorMulta + dcmValorJuros + dcmValorOutros - dcmValorDescto - dcmValorDesctoBolsa).ToString("N2");
            calcularExcedente();
        }

        /// <summary>
        /// Realiza o cálculo do excedente
        /// </summary>
        private void calcularExcedente()
        {
            Decimal totalRecebido = txtValorRecebido.Text == "" ? 0 : decimal.Parse(txtValorRecebido.Text);
            Decimal valorTotal = txtValorTotal.Text == "" ? 0 : decimal.Parse(txtValorTotal.Text);
            Decimal valorE = totalRecebido - valorTotal;
            txtValorExcedente.Text = valorE > 0 ? valorE.ToString("N2") : "";
        }

        private void CarregarTipoRecebimento()
        {
            ddlTipoReceb.Items.Clear();
            ddlTipoReceb.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoRecebimentoFinanceiro.ResourceManager));
            ddlTipoReceb.SelectedValue = tipoRecebimentoFinanceiro.B;
        }

        #endregion

        #region Eventos de componentes
        protected void btnAtualizarValores_Click(object sender, EventArgs e)
        {
            //if (decimal.Parse(txtValorTotalPago.Text) == 0)
            //    CalculaValoresPagos(RetornaEntidade());
            //else
            //    
            CalculaValoresPagos();
        }

        protected void txtValorMultaRecebido_TextChanged(object sender, EventArgs e)
        {
            CalculaValoresPagos();
        }

        protected void txtValorJurosRecebido_TextChanged(object sender, EventArgs e)
        {
            CalculaValoresPagos();
        }

        protected void txtValorDescontoRecebido_TextChanged(object sender, EventArgs e)
        {
            CalculaValoresPagos();
        }

        protected void txtValorDescontoBolsaRecebido_TextChanged(object sender, EventArgs e)
        {
            CalculaValoresPagos();
        }

        protected void txtOutrosValoresRecebidos_TextChanged(object sender, EventArgs e)
        {
            CalculaValoresPagos();
        }

        protected void txtValorRecebido_TextChanged(object sender, EventArgs e)
        {
            calcularExcedente();
        }

        #endregion

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            calcularExcedente();
        }
    }
}