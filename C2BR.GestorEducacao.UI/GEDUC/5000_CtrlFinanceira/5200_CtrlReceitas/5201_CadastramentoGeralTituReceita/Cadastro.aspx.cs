//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE RECEITAS
// DATA DE CRIAÇÃO: 
//------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 21/03/2013| Victor Martins Machado     | Foi alterado o tamanho máximo do campo Número do documento
//           |                            | 'txtNumeroDocumento' de 18 para 20, que é o limite permitido
//           |                            | no banco de dados e no campo do layout.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 da conta contábil no cadastro
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 01/05/2013| André Nobre Vinagre        | Corrigida a questão do desconto não está aparecendo no boleto
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 21/06/2013| Victor Martins Machado     | Criado o processo de geração de um novo boleto, função EmitirNovoBoleto(), 
//           |                            | para o título e alterado o processo de impressão da 2° via do boleto
//           |                            | para registrar as alterações do nosso número do título na tabela TB045.
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

///Início das Regras de Negócios
///Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5201_CadastramentoGeralTituReceita
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> statusF = AuxiliBaseApoio.chave(statusFinanceiro.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            CarregaAluno();
            CarregaHistoricos();
            CarregaTiposDocumento();
            CarregaCentrosCusto();
            CarregaGrupoContasContabeis(ddlTipoContaA,ddlGrupoContaA);
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
            CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
            CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
            CarregaLocaisCobranca();
            CarregaSituacao();
            CarregaBancos();
            CarregaAgencias();
            CarregaContas();
            CarregaBoletos();
            CarregaAgrupadores();
            CarregaUnidadeContrato();
            CarregaTipoTFR();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = txtDataSituacao.Text = dataAtual;
                txtNumeroParcela.Enabled = txtNumeroDocumento.Enabled = true;
                ddlTipoTaxaBoleto.Enabled = ddlBoleto.Enabled = false;
            }
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            decimal retornaDecimal = 0;
            bool gerarNovoBoleto = false;
            // Valida se o Número do Documento é maior que 20, que é o limite permito para o campo.
            if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoInsercao && txtNumeroDocumento.Text.Length > 20)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Tamanho Máximo é de 20 caracteres.");
                return;
            }

            if (int.Parse(txtQuantidadeParcelas.Text) < int.Parse(txtNumeroParcela.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade de Parcelas não pode ser menor que o Número da Parcela");
                return;
            }

            if (double.Parse(txtValorTotal.Text) < double.Parse(txtValorParcela.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor Total não pode ser menor que o Valor da Parcela");
                return;
            }

            if (txtValorJuros.Text != "" ? !decimal.TryParse(txtValorJuros.Text, out retornaDecimal) : false)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Juros informado incorreto.");
                return;
            }

//--------> Se o tipo de documento for Boleto: é obrigatório informar os Dados Bancários (Banco, Agência, Conta, Cedente)
            if (ddlBanco.SelectedValue != "")
            {
                if (ddlAgencia.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Agência deve ser informada");
                    return;
                }
                else if (ddlConta.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Conta Corrente deve ser informada");
                    return;
                }
            }

            if (ddlFlagBoleto.SelectedValue == "S")
            {
                if (ddlBoleto.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Boleto deve ser informado");
                    return;
                }
            }

            if ((ddlContaContabilA.SelectedValue == ddlContaContabilB.SelectedValue) || (ddlContaContabilA.SelectedValue == ddlContaContabilC.SelectedValue) ||
                (ddlContaContabilC.SelectedValue == ddlContaContabilB.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Conta contábil ativa, de caixa e de banco devem ser diferentes.");
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoInsercao)
            {
                DateTime dtCadastro = DateTime.Now;
                int nuPar = int.Parse(txtNumeroParcela.Text);
                int tpDocto = int.Parse(ddlTipoDocumento.SelectedValue);

                var varOcoTit = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                 where iTb47.NU_DOC == txtNumeroDocumento.Text && iTb47.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP &&
                                 iTb47.DT_CAD_DOC.Year == dtCadastro.Year && iTb47.DT_CAD_DOC.Month == dtCadastro.Month
                                 && iTb47.DT_CAD_DOC.Day == dtCadastro.Day && iTb47.NU_PAR == nuPar
                                 select iTb47).FirstOrDefault();

                if (varOcoTit != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Já existe cadastrado nesta Unidade um TÍTULO com Tipo de Documento, Número, Nº da Parcela e Data Igual.");
                    return;                    
                }
                else
                {
                    var varOcoTitTp = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                       where iTb47.NU_DOC == txtNumeroDocumento.Text && iTb47.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                       && iTb47.NU_PAR == nuPar && iTb47.TB086_TIPO_DOC.CO_TIPO_DOC == tpDocto
                                       select iTb47).FirstOrDefault();

                    if (varOcoTitTp != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "(MSG) Já existe cadastrado nesta Unidade um TÍTULO com Tipo de Documento, Número, Nº da Parcela e Data Igual.");
                        return;
                    }
                }
            }

            TB47_CTA_RECEB tb47 = RetornaEntidade();

            if (tb47 == null)
            {
                tb47 = new TB47_CTA_RECEB();

                tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb47.NU_DOC = txtNumeroDocumento.Text;
                tb47.NU_PAR = int.Parse(txtNumeroParcela.Text);
                tb47.DT_CAD_DOC = DateTime.Now;
            }            

            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb47.TP_CLIENTE_DOC = ddlTipoFonte.SelectedValue;
            tb47.CO_ALU = ddlTipoFonte.SelectedValue == "A" ? int.Parse(ddlNomeFonte.SelectedValue) : (int?)null;

//--------> Faz a associação como a tabela de responsável
            if (tb47.CO_ALU != null && (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoInsercao || tb47.TB108_RESPONSAVEL == null))
            {
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tb47.CO_ALU.Value);
                tb07.TB108_RESPONSAVELReference.Load();
                tb47.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL;
            }

            tb47.TB103_CLIENTE = ddlTipoFonte.SelectedValue == "A" ? null : TB103_CLIENTE.RetornaPelaChavePrimaria(int.Parse(ddlNomeFonte.SelectedValue));
            tb47.QT_PAR = int.Parse(txtQuantidadeParcelas.Text);
            tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(int.Parse(ddlTipoDocumento.SelectedValue));
            tb47.CO_REF_DOCU = txtCodRefDocumento.Text;
            //tb47.CO_CON_RECFIX = ddlContrato.SelectedValue != "" ? ddlContrato.SelectedValue : null;
            //tb47.CO_ADITI_RECFIX = ddlAditivo.SelectedValue != "" ? (int?)int.Parse(ddlAditivo.SelectedValue) : null;
            tb47.DT_EMISS_DOCTO = DateTime.Parse(txtDataDocumento.Text);
            tb47.DT_VEN_DOC = DateTime.Parse(txtDataVencimento.Text);
            tb47.FL_TIPO_COB = ddlTipoLocalCobranca.SelectedValue;
            tb47.FL_TIPO_PREV_RECEB = ddlTpPrevReceb.SelectedValue;
            if (ddlTipoLocalCobranca.SelectedValue == "B")
            {
                tb47.TB101_LOCALCOBRANCA = null;
                tb47.IDEBANCO = ddlBanco.SelectedValue != "" ? ddlBanco.SelectedValue : null;
                tb47.CO_AGENCIA = ddlAgencia.SelectedValue != "" ? (int?)int.Parse(ddlAgencia.SelectedValue) : null;
                tb47.NU_CTA_DOC = ddlConta.SelectedValue != "" ? ddlConta.SelectedValue : null;
            }
            else
            {
                tb47.TB101_LOCALCOBRANCA = ddlLocalCobranca.SelectedValue != "" ? TB101_LOCALCOBRANCA.RetornaPelaChavePrimaria(int.Parse(ddlLocalCobranca.SelectedValue)) : null;                
                tb47.IDEBANCO = null;
                tb47.CO_AGENCIA = null;
                tb47.NU_CTA_DOC = null;
            }            

            tb47.FL_EMITE_BOLETO = ddlFlagBoleto.SelectedValue;
            int idBoleto;

            if (int.TryParse(ddlBoleto.SelectedValue, out idBoleto))
            {
                var boletoEscolhido = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(idBoleto);
                if (tb47 != null && tb47.TB227_DADOS_BOLETO_BANCARIO != null)
                {
                    if (!tb47.TB227_DADOS_BOLETO_BANCARIO.Equals(boletoEscolhido))
                        gerarNovoBoleto = true;
                }

                tb47.TB227_DADOS_BOLETO_BANCARIO = boletoEscolhido;
            }

            tb47.TB39_HISTORICO = ddlHistorico.SelectedValue != "" ? TB39_HISTORICO.RetornaPelaChavePrimaria(int.Parse(ddlHistorico.SelectedValue)) : null;            
            tb47.DE_COM_HIST = txtComplementoHistorico.Text;
            tb47.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(int.Parse(ddlCentroCusto.SelectedValue));
            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(int.Parse(ddlContaContabilA.SelectedValue));
            tb47.CO_SEQU_PC_CAIXA = ddlContaContabilC.SelectedValue != "" ? (int?)int.Parse(ddlContaContabilC.SelectedValue) : null;
            tb47.CO_SEQU_PC_BANCO = ddlContaContabilB.SelectedValue != "" ? (int?)int.Parse(ddlContaContabilB.SelectedValue) : null;
            tb47.VR_TOT_DOC = decimal.Parse(txtValorTotal.Text);
            tb47.VR_PAR_DOC = decimal.Parse(txtValorParcela.Text);
            tb47.VR_MUL_DOC = txtValorMulta.Text != "" ? (decimal?)decimal.Parse(txtValorMulta.Text) : null;
            tb47.CO_FLAG_TP_VALOR_MUL = chkValorMultaPercentual.Checked ? "P" : "V";
            //Formatando o valor do Juros para salvar
            decimal jurosDec = decimal.Zero;
            bool convertido = false;
            if(txtValorJuros.Text != "")
                convertido = decimal.TryParse(string.Format("{0:0.0000}", decimal.Parse(txtValorJuros.Text)), out jurosDec);
            tb47.VR_JUR_DOC = convertido ? (decimal?)jurosDec : null;
            tb47.CO_FLAG_TP_VALOR_JUR = chkValorJurosPercentual.Checked ? "P" : "V";
            tb47.VR_DES_DOC = txtValorDesconto.Text != "" ? (decimal?)decimal.Parse(txtValorDesconto.Text) : null;
            tb47.CO_FLAG_TP_VALOR_DES = chkValorDescontoPercentual.Checked ? "P" : "V";
            tb47.VR_OUT_DOC = txtOutroValor.Text != "" ? (decimal?)decimal.Parse(txtOutroValor.Text) : null;
            tb47.CO_FLAG_TP_VALOR_OUT = chkOutroValorPercentual.Checked ? "P" : "V";
            tb47.VL_DES_BOLSA_ALUNO = txtDescontoBolsa.Text != "" ? (decimal?)decimal.Parse(txtDescontoBolsa.Text) : null;
            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = chkValorDescontoBolsaPercentual.Checked ? "P" : "V";
            tb47.CO_AGRUP_RECDESP = (ddlAgrupador.SelectedValue != "") ? (int?)int.Parse(ddlAgrupador.SelectedValue) : null;
            if (ddlSituacao.SelectedValue == statusF[statusFinanceiro.A])
            {
                tb47.VR_PAG = null;
                tb47.VR_MUL_PAG = null;
                tb47.VR_JUR_PAG = null;
                tb47.VR_DES_PAG = null;
                tb47.VR_OUT_PAG = null;
                tb47.DT_REC_DOC = null;
                tb47.CO_CAIXA = null;
                tb47.CO_COL_BAIXA = null;
                tb47.DT_MOV_CAIXA = null;
                tb47.FL_ORIGEM_PGTO = null;
            }
            //tb47.VR_PAG = txtValorRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorRecebido.Text) : null;
            //tb47.VR_MUL_PAG = txtValorMultaRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorMultaRecebido.Text) : null;
            //tb47.VR_JUR_PAG = txtValorJurosRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorJurosRecebido.Text) : null;
            //tb47.VR_DES_PAG = txtValorDescontoRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorDescontoRecebido.Text) : null;
            //tb47.VR_OUT_PAG = txtValorOutroRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorOutroRecebido.Text) : null;
            tb47.DE_OBS = txtObservacao.Text;
            tb47.DE_OBS_BOL_MAT = txtObservacaoMatricula.Text;
            //tb47.CO_BARRA_DOC = txtCodigoBarras.Text;
            tb47.CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            tb47.IC_SIT_DOC = ddlSituacao.SelectedValue;
            if ((ddlSituacao.SelectedValue == statusF[statusFinanceiro.C]) && (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoAlteracao))
            {
                var ocoTb296 = (from iTb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                where iTb296.CO_EMP_CAIXA == tb47.CO_EMP && iTb296.NU_DOC_CAIXA == tb47.NU_DOC && iTb296.NU_PAR_DOC_CAIXA.Value == tb47.NU_PAR
                                    && iTb296.DT_DOC_CAIXA.Value.Year == tb47.DT_CAD_DOC.Year && iTb296.DT_DOC_CAIXA.Value.Month == tb47.DT_CAD_DOC.Month
                                    && iTb296.DT_DOC_CAIXA.Value.Day == tb47.DT_CAD_DOC.Day
                                select iTb296).ToList();

                foreach (var iTb296 in ocoTb296)
                {
                    var ocoTb156 = (from iTb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                                    where iTb156.CO_CAIXA_MOVIMENTO == iTb296.CO_SEQMOV_CAIXA
                                    select iTb156).ToList();

                    foreach (var viTb156 in ocoTb156)
                    {
                        viTb156.CO_STATUS = "C";
                        viTb156.DT_STATUS = DateTime.Now;
                        GestorEntities.SaveOrUpdate(viTb156, true);
                    }

                    iTb296.FLA_SITU_DOC = "C";
                    GestorEntities.SaveOrUpdate(iTb296, true);
                }
            }
            tb47.DT_SITU_DOC = DateTime.Now;                                              
            tb47.DT_ALT_REGISTRO = DateTime.Now;
            
            CurrentPadraoCadastros.CurrentEntity = tb47;
            if (gerarNovoBoleto)
                EmitirBoleto(true, false);
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                
                TB47_CTA_RECEB tb47 = RetornaEntidade();

                if (tb47 != null)
                {
                    if (GestorEntities.Delete(tb47) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                    else
                    {
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro excluído com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Título não encontrado.");
                }
            }            
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
                tb47.TB086_TIPO_DOCReference.Load();
                tb47.TB099_CENTRO_CUSTOReference.Load();
                tb47.TB101_LOCALCOBRANCAReference.Load();
                tb47.TB103_CLIENTEReference.Load();
                tb47.TB116_TIPO_DEV_DOCReference.Load();
                tb47.TB25_EMPRESAReference.Load();
                tb47.TB39_HISTORICOReference.Load();
                tb47.TB56_PLANOCTAReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

                txtCodRefDocumento.Text = tb47.CO_REF_DOCU;
                txtCodigoBarras.Text = tb47.CO_BARRA_DOC;
                txtComplementoHistorico.Text = tb47.DE_COM_HIST;
                txtDataCadastro.Text = tb47.DT_CAD_DOC.ToString("dd/MM/yyyy");
                txtDataRecebimento.Text = tb47.DT_REC_DOC != null ? tb47.DT_REC_DOC.Value.ToString("dd/MM/yyyy") : "";
                txtDataVencimento.Text = tb47.DT_VEN_DOC.ToString("dd/MM/yyyy");
                txtDataDocumento.Text = tb47.DT_EMISS_DOCTO.ToString("dd/MM/yyyy");
                txtDataSituacao.Text = tb47.DT_SITU_DOC != null ? tb47.DT_SITU_DOC.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                txtDescontoBolsa.Text = tb47.VL_DES_BOLSA_ALUNO != null ? (tb47.VL_DES_BOLSA_ALUNO ?? 0).ToString("n2") : "";
                txtValorDescontoBolsaRecebido.Text = tb47.VR_DES_BOLSA_PAG != null ? (tb47.VR_DES_BOLSA_PAG ?? 0).ToString("n2") : "";
                txtNumeroDocumento.Text = tb47.NU_DOC;
                txtNumeroParcela.Text = tb47.NU_PAR.ToString();
                txtObservacao.Text = tb47.DE_OBS;
                if (!String.IsNullOrEmpty(tb47.DE_OBS_BOL_MAT))
                    txtObservacaoMatricula.Text = tb47.DE_OBS_BOL_MAT;
                else
                {
                    var modulo = tb47.CO_MODU_CUR.HasValue ? TB44_MODULO.RetornaPelaChavePrimaria(tb47.CO_MODU_CUR.Value) : null;
                    var curso = tb47.CO_CUR.HasValue ? TB01_CURSO.RetornaPeloCoCur(tb47.CO_CUR.Value) : null;
                    var turma = tb47.CO_TUR.HasValue ? TB129_CADTURMAS.RetornaPelaChavePrimaria(tb47.CO_TUR.Value) : null;

                    txtObservacaoMatricula.Text = (!String.IsNullOrEmpty(tb47.CO_ANO_MES_MAT) ? tb47.CO_ANO_MES_MAT.Trim() : "")
                                                + (modulo != null ? " - " + modulo.DE_MODU_CUR : "")
                                                + (curso != null ? " - " + curso.NO_CUR : "")
                                                + (turma != null ? " - " + turma.NO_TURMA : "");
                }
                txtQuantidadeParcelas.Text = tb47.QT_PAR.ToString();
                txtValorDesconto.Text = String.Format("{0:N}", tb47.VR_DES_DOC);
                txtValorDescontoRecebido.Text = String.Format("{0:N}", tb47.VR_DES_PAG);
                txtOutroValor.Text = String.Format("{0:N}", tb47.VR_OUT_DOC);
                txtValorOutroRecebido.Text = String.Format("{0:N}", tb47.VR_OUT_PAG);
                txtValorJuros.Text = String.Format("{0:0.0000}", tb47.VR_JUR_DOC);
                txtValorJurosRecebido.Text = String.Format("{0:N}", tb47.VR_JUR_PAG);
                txtValorMulta.Text = String.Format("{0:N}", tb47.VR_MUL_DOC);
                txtValorMultaRecebido.Text = String.Format("{0:N}", tb47.VR_MUL_PAG);
                txtValorParcela.Text = String.Format("{0:N}", tb47.VR_PAR_DOC);
                txtValorRecebido.Text = String.Format("{0:N}", tb47.VR_PAG);
                txtValorTotal.Text = String.Format("{0:N}", tb47.VR_TOT_DOC);
                txtNossoNum.Text =    tb47.CO_NOS_NUM != null ? tb47.CO_NOS_NUM.ToString() : "";
                CarregaCentrosCusto();
                ddlCentroCusto.SelectedValue = tb47.TB099_CENTRO_CUSTO != null ? tb47.TB099_CENTRO_CUSTO.CO_CENT_CUSTO.ToString() : "";

                // Conta Contábil Ativa
                tb47.TB56_PLANOCTA.TB54_SGRP_CTAReference.Load();
                tb47.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                tb47.TB56_PLANOCTA.TB055_SGRP2_CTAReference.Load();

                ddlTipoContaA.SelectedValue = tb47.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
                ddlGrupoContaA.SelectedValue = tb47.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
                ddlSubGrupoContaA.SelectedValue = tb47.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
                ddlSubGrupo2ContaA.SelectedValue = tb47.TB56_PLANOCTA.TB055_SGRP2_CTA != null ? tb47.TB56_PLANOCTA.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
                ddlContaContabilA.SelectedValue = tb47.TB56_PLANOCTA.CO_SEQU_PC.ToString();
                CarregaCodigoContaContabil(ddlContaContabilA, txtCodigoContaContabilA);

                // Conta Contábil Caixa
                if (tb47.CO_SEQU_PC_CAIXA != null)
                {
                    TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb47.CO_SEQU_PC_CAIXA);
                    tb56.TB54_SGRP_CTAReference.Load();
                    tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    tb56.TB055_SGRP2_CTAReference.Load();

                    ddlTipoContaC.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                    CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
                    ddlGrupoContaC.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                    CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
                    ddlSubGrupoContaC.SelectedValue = tb56.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                    CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
                    ddlSubGrupo2ContaC.SelectedValue = tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                    CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
                    ddlContaContabilC.SelectedValue = tb56.CO_SEQU_PC.ToString();
                    CarregaCodigoContaContabil(ddlContaContabilC, txtCodigoContaContabilC);
                }                

                // Conta Contábil Banco
                if (tb47.CO_SEQU_PC_BANCO != null)
                {
                    TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb47.CO_SEQU_PC_BANCO);
                    tb56.TB54_SGRP_CTAReference.Load();
                    tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    tb56.TB055_SGRP2_CTAReference.Load();

                    ddlTipoContaB.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                    CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
                    ddlGrupoContaB.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                    CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
                    ddlSubGrupoContaB.SelectedValue = tb56.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                    CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
                    ddlSubGrupo2ContaB.SelectedValue = tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                    CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
                    ddlContaContabilB.SelectedValue = tb56.CO_SEQU_PC.ToString();
                    CarregaCodigoContaContabil(ddlContaContabilB, txtCodigoContaContabilB);
                }                

                ddlHistorico.SelectedValue = tb47.TB39_HISTORICO != null ? tb47.TB39_HISTORICO.CO_HISTORICO.ToString() : "";
                CarregaLocaisCobranca();                
                ddlSituacao.SelectedValue = tb47.IC_SIT_DOC;
                ddlTipoDocumento.SelectedValue = tb47.TB086_TIPO_DOC != null ? tb47.TB086_TIPO_DOC.CO_TIPO_DOC.ToString() : "";
                ddlTipoFonte.SelectedValue = tb47.TP_CLIENTE_DOC;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao) && ddlTipoFonte.SelectedValue == "A")
                {
                    ddlUnidadeContrato.Enabled = false;
                }

                ddlFlagBoleto.SelectedValue = tb47.FL_EMITE_BOLETO;

                if (ddlFlagBoleto.SelectedValue == "N")
                {
                    ddlBoleto.SelectedValue = ddlTipoTaxaBoleto.SelectedValue = "";
                    ddlBoleto.Enabled = ddlTipoTaxaBoleto.Enabled = false;
                }
                else
                {
                    ddlBoleto.Enabled = ddlTipoTaxaBoleto.Enabled = true;
                }

                ddlUnidadeContrato.SelectedValue = tb47.CO_EMP_UNID_CONT.ToString();
                if ((tb47.TB227_DADOS_BOLETO_BANCARIO != null) && (ddlFlagBoleto.SelectedValue != "N"))
                {
                    ddlTipoTaxaBoleto.SelectedValue = tb47.TB227_DADOS_BOLETO_BANCARIO.TP_TAXA_BOLETO;
                    CarregaBoletos();
                    ddlBoleto.SelectedValue = tb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO.ToString();
                }

                ddlTipoLocalCobranca.SelectedValue = tb47.FL_TIPO_COB;

                if (ddlTipoLocalCobranca.SelectedValue == "B")
                {
                    ddlBanco.Enabled = ddlAgencia.Enabled = ddlConta.Enabled = ddlLocalCobranca.Enabled = true;

                    ddlBanco.SelectedValue = tb47.IDEBANCO != null ? tb47.IDEBANCO : "";

                    CarregaAgencias();
                    ddlAgencia.SelectedValue = tb47.CO_AGENCIA != null ? tb47.CO_AGENCIA.ToString() : "";

                    CarregaContas();
                    ddlConta.SelectedValue = tb47.NU_CTA_DOC != null ? tb47.NU_CTA_DOC : "";
                    ddlLocalCobranca.Enabled = false;
                }
                else
                {
                    ddlLocalCobranca.Enabled = true;
                    ddlLocalCobranca.SelectedValue = tb47.TB101_LOCALCOBRANCA != null ? tb47.TB101_LOCALCOBRANCA.CO_LOC_COB.ToString() : "";
                }

                chkValorDescontoBolsaPercentual.Checked = tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P";
                chkValorDescontoPercentual.Checked = tb47.CO_FLAG_TP_VALOR_DES == "P";
                chkValorJurosPercentual.Checked = tb47.CO_FLAG_TP_VALOR_JUR == "P";
                chkValorMultaPercentual.Checked = tb47.CO_FLAG_TP_VALOR_MUL == "P";
                chkOutroValorPercentual.Checked = tb47.CO_FLAG_TP_VALOR_OUT == "P";
                ddlTpPrevReceb.SelectedValue = tb47.FL_TIPO_PREV_RECEB != null ? tb47.FL_TIPO_PREV_RECEB : "B";
                ddlTipoReceb.SelectedValue = tb47.FL_TIPO_RECEB != null ? tb47.FL_TIPO_RECEB : "";

                ddlTpPrevReceb.Enabled = tb47.IC_SIT_DOC != "Q";

//------------> Campos que devem ser desabilitados na alteração
                ddlTipoFonte.Enabled = ddlNomeFonte.Enabled = txtQuantidadeParcelas.Enabled = false;
                //ddlContrato.Enabled = ddlAditivo.Enabled

                if (tb47.TP_CLIENTE_DOC == "A")
                {
                    CarregaAluno();
                    ddlNomeFonte.SelectedValue = tb47.CO_ALU.ToString();

                    TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tb47.CO_ALU.Value);
                    txtCodigoFonte.Text = tb07.NU_NIRE.ToString();

                    //------------> Preenche os campos informados com o nome e cpf do responsavel do aluno 
                    tb47.TB108_RESPONSAVELReference.Load();
                    if (tb47.TB108_RESPONSAVEL != null)
                    {
                        txtNomeResponsavel.Text = tb47.TB108_RESPONSAVEL.NO_RESP;
                        txtCpfResponsavel.Text = tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Length == 11 ? tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(3, ".").Insert(7, ".").Insert(11, "-") : tb07.TB108_RESPONSAVEL.NU_CPF_RESP;
                    }
                }
                else
                {
                    CarregaFontesReceita();
                    ddlNomeFonte.SelectedValue = tb47.TB103_CLIENTE.CO_CLIENTE.ToString();
                    CarregaCodigoFonte(false);
                }

                ddlAgrupador.SelectedValue = (tb47.CO_AGRUP_RECDESP.HasValue) ? tb47.CO_AGRUP_RECDESP.ToString() : "";

                txtLocalPagto.Text = tb47.IC_SIT_DOC == "Q" && tb47.FL_ORIGEM_PGTO != null ? tb47.FL_ORIGEM_PGTO == "B" ? "Rede Bancária" : "Caixa Unidade" : "";

                
            }
        }

        

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidaed TB47_CTA_RECEB</returns>
        private TB47_CTA_RECEB RetornaEntidade()
        {
            return TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringPelaChave("doc"),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("par"));
        }

        /// <summary>
        /// Método que gera Boleto
        /// </summary>
        private void EmitirBoleto(bool boletoNovo, bool abrirBoleto = true)
        {
            if (ddlUnidadeContrato.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Primeiro deve ser seleciona a unidade de contrato.");
                return;
            }

            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            string strTipoFonte = ddlTipoFonte.SelectedValue;
            int id = int.Parse(ddlNomeFonte.SelectedValue);
            int coEmp = int.Parse(ddlUnidadeContrato.SelectedValue);            

//--------> Recebe as chaves primáris do Título
            string strNudoc = txtNumeroDocumento.Text;
            int intNuPar = Convert.ToInt32(txtNumeroParcela.Text);
            string strInstruBoleto = "";

            TB47_CTA_RECEB novoTb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, strNudoc, intNuPar);            

            novoTb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

            if (novoTb47.TB227_DADOS_BOLETO_BANCARIO != null)
            {
                if (ddlBoleto.SelectedValue != novoTb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO.ToString())
                {
                    novoTb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoleto.SelectedValue));

//----------------> Salva o novo boleto no título                    
                    GestorEntities.SaveOrUpdate(novoTb47);
                    GestorEntities.CurrentContext.SaveChanges();
                }
            }
            else
            {
                novoTb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoleto.SelectedValue));
                novoTb47.FL_EMITE_BOLETO = "S";
//------------> Salva o novo boleto no título                    
                GestorEntities.SaveOrUpdate(novoTb47);
                GestorEntities.CurrentContext.SaveChanges();
            }

//--------> Recebe o Título de Contas a Receber
            TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, strNudoc, intNuPar);

            tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

            if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Boleto Associado!");
                return;
            }

            tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
            tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
            tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
            tb47.TB108_RESPONSAVELReference.Load();
            tb47.TB103_CLIENTEReference.Load();

            int coResp = tb47.TB108_RESPONSAVEL != null ? tb47.TB108_RESPONSAVEL.CO_RESP : 0;

            var varResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                           where tb108.CO_RESP == coResp && strTipoFonte == "A"
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                           select new
                           {
                               BAIRRO = tb905.NO_BAIRRO,
                               CEP = tb108.CO_CEP_RESP,
                               CIDADE = tb905.TB904_CIDADE.NO_CIDADE,
                               CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP,
                               ENDERECO = tb108.DE_ENDE_RESP,
                               NUMERO = tb108.NU_ENDE_RESP,
                               COMPL = tb108.DE_COMP_RESP,
                               NOME = tb108.NO_RESP,
                               UF = tb905.CO_UF
                           }).FirstOrDefault();

            var tb103 = (from lTb103 in TB103_CLIENTE.RetornaTodosRegistros()
                         where lTb103.CO_CLIENTE == id && strTipoFonte == "O"
                         select new
                         {
                             BAIRRO = lTb103.TB905_BAIRRO.NO_BAIRRO,
                             CEP = lTb103.CO_CEP_CLI,
                             CIDADE = lTb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                             CPFCNPJ = (lTb103.TP_CLIENTE == "F" && lTb103.CO_CPFCGC_CLI.Length >= 11) ? lTb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                       ((lTb103.TP_CLIENTE == "J" && lTb103.CO_CPFCGC_CLI.Length >= 14) ? lTb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : lTb103.CO_CPFCGC_CLI),
                             ENDERECO = lTb103.DE_END_CLI,
                             NUMERO = lTb103.NU_END_CLI,
                             COMPL = lTb103.DE_COM_CLI,
                             NOME = lTb103.NO_FAN_CLI,
                             UF = lTb103.CO_UF_CLI
                         }).FirstOrDefault();

            var varSacado = strTipoFonte == "A" ? varResp : tb103;            

//--------> Faz a verificação para saber se o Título é gerado para o Aluno ou não
            if (strTipoFonte == "A")
            {
                if (tb47.TB108_RESPONSAVEL == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                    return;
                }
            }
            else
            {
                if (tb47.TB103_CLIENTE == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Cliente!");
                    return;
                }
            }

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                return;
            }

//--------> Recebe a Unidade
            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

            InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

//--------> Informações do Boleto
            boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
            boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

            if (String.IsNullOrEmpty(tb47.CO_NOS_NUM) || boletoNovo)
            {
                #region Gera um novo nosso número
                //===> Atribui um novo nosso número ao título
                tb47.CO_NOS_NUM = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                GestorEntities.SaveOrUpdate(tb47, true);

                //===> Incluí o nosso número na tabela de nossos números por título
                TB045_NOS_NUM tb045 = new TB045_NOS_NUM();
                tb045.NU_DOC = tb47.NU_DOC;
                tb045.NU_PAR = tb47.NU_PAR;
                tb045.DT_CAD_DOC = tb47.DT_CAD_DOC;
                tb045.DT_NOS_NUM = DateTime.Now;
                tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                //===> Pega as informações da empresa/unidade
                TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);
                tb045.TB25_EMPRESA = emp;
                //===> Pega as informações do colaborador
                TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                tb045.TB03_COLABOR = tb03;
                tb045.CO_NOS_NUM = tb47.CO_NOS_NUM;
                tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;

                //===> Atualiza o próximo nosso número na tabela TB29_BANCO
                TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);
                long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                int casas = u.CO_PROX_NOS_NUM.Length;
                string mask = string.Empty;
                foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                GestorEntities.SaveOrUpdate(u, true);
                #endregion

                boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
            }
            else
            {
                boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
            }
            boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;

            if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
            TB227_DADOS_BOLETO_BANCARIO tb227 = tb47.TB227_DADOS_BOLETO_BANCARIO;

            if (tb227.FL_TX_BOL_CLI != "S")
                boleto.Valor = tb47.VR_PAR_DOC;
            else
                boleto.Valor = tb47.VR_PAR_DOC + tb227.VR_TX_BOL_CLI.Value;

            boleto.Vencimento = tb47.DT_VEN_DOC;

//--------> Informações do Cedente
            boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
            boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA != "" ? ("-" +
                                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA) : "");

            boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
            boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + '-' + tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim();
            boleto.CpfCnpjCedente = tb25.CO_CPFCGC_EMP;
            boleto.NomeCedente = tb25.NO_RAZSOC_EMP;

            boleto.Desconto =
                        ((!tb47.VR_DES_DOC.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                ? (boleto.Valor * tb47.VR_DES_DOC.Value / 100)
                                : tb47.VR_DES_DOC.Value))
                        + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                ? (boleto.Valor * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                : tb47.VL_DES_BOLSA_ALUNO.Value)));

            /**
             * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
             * */
            #region Valida layout do boleto gerado
            //TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB000_INSTITUICAOReference.Load();
            tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
            //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

            if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
            {
                switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                {
                    case "M":
                        strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                        break;
                }
            }
            else
            {
                switch (tb25.TP_BOLETO_BANC)
                {
                    case "M":
                        strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                        break;
                }
            }
            #endregion

            //Alterações feitas para o Instituto Fenix, deve se criar uma parametrização para fazer essas alterações
            boleto.Valor -= boleto.Desconto;
            boleto.Desconto = 0;

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
            {
                var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
            }

            //--------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
            if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

            boleto.Instrucoes = strInstruBoleto;

            boleto.CO_EMP = tb47.CO_EMP;
            boleto.NU_DOC = tb47.NU_DOC;
            boleto.NU_PAR = tb47.NU_PAR;
            boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

//            string strMultaMoraDesc = "";

////--------> Informações da Multa
//            strMultaMoraDesc += tb47.VR_MUL_DOC != null && tb47.VR_MUL_DOC.Value != 0 ?
//                (tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
//                (boleto.Valor * (decimal)tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

////--------> Informações da Mora
//            strMultaMoraDesc += tb47.VR_JUR_DOC != null && tb47.VR_JUR_DOC.Value != 0 ?
//                 (tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
//                 (boleto.Valor * (decimal)tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
//                    tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

////--------> Informações do desconto
//            strMultaMoraDesc += tb47.VR_DES_DOC != null && tb47.VR_DES_DOC.Value != 0 ?
//                 (tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + tb47.VR_DES_DOC.Value.ToString() + "% (R$ " +
//                 (boleto.Valor * (decimal)tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
//                    tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

//            strMultaMoraDesc += tb47.VL_DES_BOLSA_ALUNO != null && tb47.VL_DES_BOLSA_ALUNO.Value != 0 ?
//                         (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P" ? " - Descto Bolsa: " + tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00") + "% (R$ " +
//                         (boleto.Valor * (decimal)tb47.VL_DES_BOLSA_ALUNO.Value / 100).ToString("0.00") + ")" : " - Descto Bolsa: R$ " +
//                            tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00")) : "";

//--------> Faz a adição de instruções ao Boleto
            boleto.Instrucoes += "<br>";

            string strCnpjCPF = "";

//--------> Faz a adição de Instruções de Informações do Responsável do Aluno ou Informações do Cliente, de acordo com o tipo                    
            if (strTipoFonte == "A")
            {
//------------> Ano Refer: - Matrícula: - Nº NIRE:
//------------> Modalidade: - Série: - Turma: - Turno:
                var inforAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                  join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                  where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                  && tb08.CO_ALU == tb47.CO_ALU
                                  select new
                                  {
                                      tb08.TB44_MODULO.DE_MODU_CUR,
                                      tb01.NO_CUR,
                                      tb129.CO_SIGLA_TURMA,
                                      tb08.CO_ANO_MES_MAT,
                                      tb08.CO_ALU_CAD,
                                      tb08.TB07_ALUNO.NU_NIRE,
                                      tb08.TB07_ALUNO.NO_ALU,
                                      TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                  }).FirstOrDefault();

                if (inforAluno != null)
                {
                    strCnpjCPF += "Ano Refer: " + inforAluno.CO_ANO_MES_MAT.Trim() + 
                                 // " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") + 
                                  " - Nº NIRE: " + inforAluno.NU_NIRE.ToString() + 
                                  "<br> Modalidade: " + inforAluno.DE_MODU_CUR + 
                                  " - Série: " + inforAluno.NO_CUR +
                                  " - Turma: " + inforAluno.CO_SIGLA_TURMA + 
                                  " - Turno: " + inforAluno.TURNO + 
                                  " <br> Aluno(a): " + inforAluno.NO_ALU;
                }
                else
                {
                    //strCnpjCPF += "Aluno(a): " + TB07_ALUNO.RetornaPeloCoAlu((int)tb47.CO_ALU).NO_ALU;
                    strCnpjCPF += "Nº NIRE: " + TB07_ALUNO.RetornaPeloCoAlu((int)tb47.CO_ALU).NU_NIRE +
                                  " - Aluno(a): " + TB07_ALUNO.RetornaPeloCoAlu((int)tb47.CO_ALU).NO_ALU +
                                  //Se não tiver ano definido no contas a receber, pega o ano atual
                                  "<br>Ano: " + (!string.IsNullOrEmpty(tb47.CO_ANO_MES_MAT) ? tb47.CO_ANO_MES_MAT.Trim() : DateTime.Now.Year.ToString());

                    if (tb47.NU_DOC.Substring(0, 2) == "MU")
                    {
                        string tur = "";
                        if (tb47.CO_MODU_CUR != null && tb47.CO_CUR != null && tb47.CO_TUR != null)
                        {
                            tur = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, tb47.CO_MODU_CUR.Value, tb47.CO_CUR.Value, tb47.CO_TUR.Value).CO_PERI_TUR;
                            tur = tur == "M" ? "Matutino" : tur == "N" ? "Noturno" : "Vespertino";
                        }
                        string noMod = tb47.CO_MODU_CUR != null ? TB44_MODULO.RetornaPelaChavePrimaria(tb47.CO_MODU_CUR.Value).CO_SIGLA_MODU_CUR : "******";
                        string noCur = tb47.CO_CUR != null ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, tb47.CO_MODU_CUR.Value, tb47.CO_CUR.Value).NO_CUR : "****";
                        string noTur = tb47.CO_TUR != null ? TB129_CADTURMAS.RetornaPelaChavePrimaria(tb47.CO_TUR.Value).CO_SIGLA_TURMA : "*****";
                        string coPer = tb47.CO_TUR != null ? tur : "*****";
                        strCnpjCPF +=
                                  " - Modalidade: " + noMod +
                                  " - Série: " + noCur +
                                  " - Turma: " + noTur +
                                  " - Turno: " + coPer;
                    }
                }

                tb47.TB39_HISTORICOReference.Load();
                boleto.Instrucoes += strCnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + (tb47.TB39_HISTORICO != null ? " / " + tb47.TB39_HISTORICO.DE_HISTORICO : "") + " ***";
            }
            else
            {
                boleto.Instrucoes += "</br>" + "(" + tb47.TB103_CLIENTE.NO_FAN_CLI + ")";

                boleto.Instrucoes += "</br>" + "(Contrato: " + (tb47.CO_CON_RECFIX != null ? tb47.CO_CON_RECFIX : "XXXXX") +
                    " - Aditivo: " + (tb47.CO_ADITI_RECFIX != null ? tb47.CO_ADITI_RECFIX.Value.ToString("00") : "XX") +
                    " - Parcela: " + tb47.NU_PAR.ToString("00") + ")";
            }
            #region Informa o valor do documento eo tipo de documento
            string tpdoc = "";
            if (strTipoFonte == "A")
            {
                switch (tb47.NU_DOC.Substring(0, 2))
                {
                    case "MN":
                        tpdoc = "de Mensalidade";
                        break;
                    case "SM":
                        tpdoc = "de Serviços de Secretaria";
                        break;
                    case "MU":
                        tpdoc = "de Material Coletivo/Uniforme";
                        break;
                    default:
                        tpdoc = "de Diversos";
                        break;
                }
            }
            else
            {
                tpdoc = "do Contrato";
            }

            //if (tb227.FL_TX_BOL_CLI != "S")
            //{
            //    boleto.Instrucoes += "<br> Valor " + tpdoc + ":" + " R$" + tb47.VR_PAR_DOC.ToString("N2");
            //}
            //else
            //{
            //    boleto.Instrucoes += "<br> Valor " + tpdoc + ":" + " R$" + tb47.VR_PAR_DOC.ToString("N2") + " + " + " R$" + tb227.VR_TX_BOL_CLI.Value.ToString("N2") + "(Taxa de Emissão do Boleto) ";
            //}
            #endregion
//--------> Informações do Sacado
            boleto.BairroSacado = varSacado.BAIRRO;
            boleto.CepSacado = varSacado.CEP;
            boleto.CidadeSacado = varSacado.CIDADE;
            boleto.CpfCnpjSacado = varSacado.CPFCNPJ;
            boleto.EnderecoSacado = varSacado.ENDERECO + " " + varSacado.NUMERO + " " + varSacado.COMPL;
            boleto.NomeSacado = varSacado.NOME;
            boleto.UfSacado = varSacado.UF;

//--------> Faz a adição do Título na Sessão da lista de Boletos
            ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

            //If que verifica se o novo boleto deve ou não ser aberto, por padrão é true
            if (abrirBoleto)
            {
                //--------> Faz a exibição e gera os boletos
                BoletoBancarioHelpers.GeraBoletos(this);

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Título salvo e boleto impresso com sucesso - Visualização em nova aba ou popup.");
            }
        }
        #endregion

        #region Carregamento DropDown
        /*
        /// <summary>
        /// Método que carrega o dropdown de Contratos
        /// </summary>
        private void CarregaContratos()
        {
            ddlContrato.Items.Clear();

            if (ddlTipoFonte.SelectedValue == "O" && ddlNomeFonte.SelectedValue != "")
            {
                int coCliente = int.Parse(ddlNomeFonte.SelectedValue);

                ddlContrato.DataSource = (from tb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                                          where tb37.TB103_CLIENTE.CO_CLIENTE == coCliente && tb37.TP_CON_RECDES == "C"
                                          select new { tb37.CO_CON_RECDES }).Distinct();

                ddlContrato.DataTextField = "CO_CON_RECDES";
                ddlContrato.DataValueField = "CO_CON_RECDES";
                ddlContrato.DataBind();
            }

            ddlContrato.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Aditivos
        /// </summary>
        private void CarregaAditivos()
        {
            string coConRecDes = ddlContrato.SelectedValue;

            ddlAditivo.Items.Clear();

            if (coConRecDes != "")
            {
                ddlAditivo.DataSource = from tb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                                        where tb37.CO_CON_RECDES == coConRecDes
                                        select new { tb37.CO_ADITI_RECDES };

                ddlAditivo.DataTextField = "CO_ADITI_RECDES";
                ddlAditivo.DataValueField = "CO_ADITI_RECDES";
                ddlAditivo.DataBind();
            }
        }*/

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            ddlNomeFonte.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                       where tb07.TB108_RESPONSAVEL != null
                                       select new { tb07.CO_ALU, tb07.NO_ALU });

            ddlNomeFonte.DataTextField = "NO_ALU";
            ddlNomeFonte.DataValueField = "CO_ALU";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Nome da Fonte
        /// </summary>
        private void CarregaFontesReceita()
        {
            ddlNomeFonte.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                       select new { tb103.CO_CLIENTE, tb103.NO_FAN_CLI }).OrderBy( c => c.NO_FAN_CLI );

            ddlNomeFonte.DataTextField = "NO_FAN_CLI";
            ddlNomeFonte.DataValueField = "CO_CLIENTE";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que preenche alguns campos informados abaixo de acordo com o tipo do cliente "A"luno ou "C"liente
        /// </summary>
        /// <param name="flagAluno">Boolean aluno</param>
        private void CarregaCodigoFonte(bool flagAluno)
        {
            int coNomeFonte = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

            if (coNomeFonte == 0)
                return;

            if (flagAluno)
            {
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coNomeFonte);
                txtCodigoFonte.Text = tb07.NU_NIRE.ToString();

//------------> Preenche os campos informados com o nome e cpf do responsavel do aluno 
                tb07.TB108_RESPONSAVELReference.Load();
                if (tb07.TB108_RESPONSAVEL != null)
                {
                    txtNomeResponsavel.Text = tb07.TB108_RESPONSAVEL.NO_RESP;
                    txtCpfResponsavel.Text = tb07.TB108_RESPONSAVEL.NU_CPF_RESP.Length == 11 ? tb07.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(3, ".").Insert(7, ".").Insert(11, "-") : tb07.TB108_RESPONSAVEL.NU_CPF_RESP;   
                }                
            }
            else
            {
                TB103_CLIENTE tb103 = TB103_CLIENTE.RetornaPelaChavePrimaria(coNomeFonte);
                txtCodigoFonte.Text = tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length == 11 ? tb103.CO_CPFCGC_CLI.Insert(8, "-").Insert(5, ".").Insert(2, ".") :
                    (tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length == 14 ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Histórico
        /// </summary>
        private void CarregaHistoricos()
        {
            ddlHistorico.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                       where tb39.FLA_TIPO_HISTORICO == "C"
                                       select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Documento
        /// </summary>
        private void CarregaTiposDocumento()
        {
            ddlTipoDocumento.DataSource = (from tb086 in TB086_TIPO_DOC.RetornaTodosRegistros()
                                           select new { tb086.CO_TIPO_DOC, tb086.DES_TIPO_DOC }).OrderBy( t => t.DES_TIPO_DOC );

            ddlTipoDocumento.DataTextField = "DES_TIPO_DOC";
            ddlTipoDocumento.DataValueField = "CO_TIPO_DOC";
            ddlTipoDocumento.DataBind();

            ddlTipoDocumento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Centro de Custo
        /// </summary>
        private void CarregaCentrosCusto()
        {
            ddlCentroCusto.Items.Clear();
            
            var varTb099 = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                            where tb099.TB14_DEPTO.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                            select new { tb099.CO_CENT_CUSTO, tb099.DE_CENT_CUSTO, tb099.TB14_DEPTO.CO_SIGLA_DEPTO }
                            ).OrderBy(r => r.CO_SIGLA_DEPTO).ThenBy(r => r.DE_CENT_CUSTO);

            foreach (var iTb099 in varTb099)
                ddlCentroCusto.Items.Add(new ListItem(iTb099.CO_SIGLA_DEPTO + " - " + iTb099.DE_CENT_CUSTO, iTb099.CO_CENT_CUSTO.ToString()));

            ddlCentroCusto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Conta Contábil
        /// </summary>
        private void CarregaGrupoContasContabeis(DropDownList ddltipo, DropDownList ddlGrupo)
        {
            var res = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                       where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           && tb53.TP_GRUP_CTA == ddltipo.SelectedValue
                       select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA, tb53.NR_GRUP_CTA }).OrderBy(p => p.NR_GRUP_CTA).ToList();

            ddlGrupo.DataSource = from r in res
                                       select new
                                       {
                                           r.CO_GRUP_CTA,
                                           DE_GRUP_CTA = r.NR_GRUP_CTA.ToString().PadLeft(2, '0') + " - " + r.DE_GRUP_CTA
                                       };

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubgrupo(DropDownList ddlGrupo, DropDownList ddlSubGrupo)
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            var result = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                          where tb54.CO_GRUP_CTA == coGrupCta
                          select new { tb54.DE_SGRUP_CTA, tb54.CO_SGRUP_CTA, tb54.NR_SGRUP_CTA }).ToList();
            
            ddlSubGrupo.DataSource = (from res in result
                                      select new
                                      {
                                          DE_SGRUP_CTA = res.NR_SGRUP_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP_CTA,
                                          res.CO_SGRUP_CTA
                                      }).OrderBy(p => p.DE_SGRUP_CTA);

            ddlSubGrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo2
        /// </summary>
        private void CarregaSubgrupo2(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlSubGrupo2)
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            var result = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                          join tb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on tb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals tb54.CO_SGRUP_CTA
                          where tb54.CO_GRUP_CTA == coGrupCta && tb54.CO_SGRUP_CTA == coSubGrupo
                          select new
                          {
                              tb055.NR_SGRUP2_CTA,
                              tb055.DE_SGRUP2_CTA,
                              tb055.CO_SGRUP2_CTA
                          }).ToList();

            ddlSubGrupo2.DataSource = (from res in result
                                       select new
                                       {
                                           DE_SGRUP2_CTA = res.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP2_CTA,
                                           res.CO_SGRUP2_CTA
                                       });

            ddlSubGrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2.DataBind();

            ddlSubGrupo2.Items.Insert(0, new ListItem("",""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contábil
        /// </summary>
        private void CarregaContasContabeis(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlSubGrupo2, DropDownList ddlCtaContabil)
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coSubGrupo2 = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : 0;
            

            ddlCtaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                        where tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        && tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == coGrupo
                                        && tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSubGrupo
                                        && (coSubGrupo2 != 0 ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSubGrupo2 : 0 == 0)
                                        select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy(p => p.DE_CONTA_PC);

            ddlCtaContabil.DataTextField = "DE_CONTA_PC";
            ddlCtaContabil.DataValueField = "CO_SEQU_PC";
            ddlCtaContabil.DataBind();

            ddlCtaContabil.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega informações da Conta Contábil selecionada
        /// </summary>
        private void CarregaCodigoContaContabil(DropDownList ddlCtaContabil, TextBox txtCodiConta)
        {
            int coSequPc = ddlCtaContabil.SelectedValue != "" ? int.Parse(ddlCtaContabil.SelectedValue) : 0;

            txtCodiConta.Text = "";

            if (coSequPc == 0) 
                return;

            TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc);
            tb56.TB54_SGRP_CTAReference.Load();
            TB055_SGRP2_CTA tb55 = TB055_SGRP2_CTA.RetornaPelaChavePrimaria(tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA : 0);
            tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
            string tipoConta = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "A" ? "1" : "3";

            txtCodiConta.Text = tipoConta + "." + tb56.TB54_SGRP_CTA.TB53_GRP_CTA.NR_GRUP_CTA.ToString().PadLeft(2, '0') + "." + tb56.TB54_SGRP_CTA.NR_SGRUP_CTA.ToString().PadLeft(3, '0')
                + "." + (tb55 != null ? tb55.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') : "XXX") 
                + "." + tb56.NU_CONTA_PC.ToString().PadLeft(4, '0');
        }

        /// <summary>
        /// Método que carrega o dropdown de Locais de Cobrança
        /// </summary>
        private void CarregaLocaisCobranca()
        {
            ddlLocalCobranca.DataSource = (from tb101 in TB101_LOCALCOBRANCA.RetornaTodosRegistros()
                                           select new { tb101.CO_LOC_COB, tb101.NO_FAN_COB });

            ddlLocalCobranca.DataTextField = "NO_FAN_COB";
            ddlLocalCobranca.DataValueField = "CO_LOC_COB";
            ddlLocalCobranca.DataBind();

            ddlLocalCobranca.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega os tipos de status financeiro
        /// </summary>
        private void CarregaSituacao()
        {
            ddlSituacao.Items.Clear();
            ddlSituacao.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusFinanceiro.ResourceManager, true));
        }


        /// <summary>
        /// Carrega os tipos de TFR de acordo com a unidade logada
        /// </summary>
        private void CarregaTipoTFR()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    ddlTipoFonte.Items.Clear();
                    ddlTipoFonte.Items.Insert(0, new ListItem("Não Paciente", "O"));
                    ddlTipoFonte.Items.Insert(0, new ListItem("Paciente", "A"));
                    break;

                case "PGE":
                    ddlTipoFonte.Items.Clear();
                    ddlTipoFonte.Items.Insert(0, new ListItem("Não Aluno", "O"));
                    ddlTipoFonte.Items.Insert(0, new ListItem("Aluno", "A"));
                    break;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros()
                                   where tb225.CO_EMP == LoginAuxili.CO_EMP
                                   select new { tb225.IDEBANCO }).OrderBy(b => b.IDEBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "IDEBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        private void CarregaAgencias()
        {
            string strIdeBanco = ddlBanco.SelectedValue;

            ddlAgencia.Items.Clear();

            if (strIdeBanco != "")
            {
                ddlAgencia.DataSource = (from tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros()
                                         where tb225.IDEBANCO == strIdeBanco && tb225.CO_EMP == LoginAuxili.CO_EMP
                                         select new { tb225.CO_AGENCIA }).OrderBy(a => a.CO_AGENCIA);

                ddlAgencia.DataValueField = "CO_AGENCIA";
                ddlAgencia.DataTextField = "CO_AGENCIA";
                ddlAgencia.DataBind();
            }

            ddlAgencia.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Contas
        /// </summary>
        private void CarregaContas()
        {
            int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;

            ddlConta.Items.Clear();

            if (coAgencia != 0)
            {
                ddlConta.DataSource = (from tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros()
                                       where tb225.IDEBANCO == ddlBanco.SelectedValue && tb225.CO_AGENCIA == coAgencia
                                       && tb225.CO_EMP == LoginAuxili.CO_EMP
                                       select new { tb225.CO_CONTA }).OrderBy(c => c.CO_CONTA);

                ddlConta.DataValueField = "CO_CONTA";
                ddlConta.DataTextField = "CO_CONTA";
                ddlConta.DataBind();
            }

            ddlConta.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            if (ddlTipoTaxaBoleto.SelectedValue != "" && ddlUnidadeContrato.SelectedValue != "")
            {
                int coEmp = int.Parse(ddlUnidadeContrato.SelectedValue);

                AuxiliCarregamentos.CarregaBoletos(ddlBoleto, coEmp, ddlTipoTaxaBoleto.SelectedValue, 0, 0, false, false);
            }

            ddlBoleto.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Selecione", ""));
        }
        
            /// <summary>
        /// Método que carrega o dropdown de Unidades de Contrato
        /// </summary>
        private void CarregaUnidadeContrato()
        {
            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion

        #region Eventos de componentes do sistema

        protected void ddlTipoFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoFonte.Text = txtNomeResponsavel.Text = txtCpfResponsavel.Text = "";
             
            if (ddlTipoFonte.SelectedIndex == 0)
            {
                CarregaAluno();
                CarregaCodigoFonte(true);
            }
            else
            {
                CarregaFontesReceita();
                CarregaCodigoFonte(false);
            }

            //CarregaContratos();
            //CarregaAditivos();
        }

        protected void ddlNomeFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFonte.SelectedValue == "A")
                CarregaCodigoFonte(true);
            else
                CarregaCodigoFonte(false);

            //CarregaContratos();
            //CarregaAditivos();
        }

        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupo2Conta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlContaContabil_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil(ddlContaContabilA, txtCodigoContaContabilA);
        }

        protected void ddlTipoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlGrupoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlSubGrupoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlSubGrupo2ContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlContaContabilB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil(ddlContaContabilB, txtCodigoContaContabilB);
        }

        protected void ddlTipoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlGrupoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlSubGrupoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlSubGrupo2ContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlContaContabilC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil(ddlContaContabilC, txtCodigoContaContabilC);
        }
        /*
        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAditivos();
        }*/

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
            CarregaContas();
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContas();
        }
        
        protected void lnkBoleto_Click(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Para gerar boleto primeiro o título deve ser salvo.");
                return;
            }

            if (ddlBoleto.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, porque não existe boleto selecionado.");
                return;
            }
            else
            {
                if (ddlSituacao.SelectedValue == statusF[statusFinanceiro.Q])
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, porque título está quitado.");
                    return;
                }

                EmitirBoleto(false);                
            }
        }

        protected void lnkNovoBoleto_Click(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Para gerar boleto primeiro o título deve ser salvo.");
                return;
            }

            if (ddlBoleto.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, porque não existe boleto selecionado.");
                return;
            }
            else
            {
                if (ddlSituacao.SelectedValue == statusF[statusFinanceiro.Q])
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, porque título está quitado.");
                    return;
                }

                EmitirBoleto(true);
            }
        }

        protected void ddlTipoTaxaBoleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletos();
        }

        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagBoleto.SelectedValue == "S")
            {
                CarregaBoletos();    
            }            
        }

        protected void ddlTipoLocalCobranca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoLocalCobranca.SelectedValue == "B")
            {
                ddlBanco.Enabled = ddlAgencia.Enabled = ddlConta.Enabled =  true;

                ddlLocalCobranca.Enabled = false;
            }
            else
            {
                ddlBanco.Enabled = ddlAgencia.Enabled = ddlConta.Enabled = false;
                ddlLocalCobranca.Enabled = true;
                ddlBanco.SelectedValue = ddlAgencia.SelectedValue = ddlConta.SelectedValue = ddlLocalCobranca.SelectedValue = "";
            }
        }

        protected void ddlFlagBoleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagBoleto.SelectedValue == "N")
            {
                ddlBoleto.SelectedValue = ddlTipoTaxaBoleto.SelectedValue = "";
                ddlBoleto.Enabled = ddlTipoTaxaBoleto.Enabled = false;
            }
            else
            {
                ddlBoleto.Enabled = ddlTipoTaxaBoleto.Enabled = true;
            }
        }

        #endregion
    }
}