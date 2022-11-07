//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE DESPESAS (CONTAS A PAGAR)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE DESPESAS/COMPROMISSOS DE PAGAMENTOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 11/04/2013| André Nobre Vinagre        | - Colocado o botão para redirecionar para Cadastro
//           |                            | de Fornecedor
//           |                            |
// ----------+----------------------------+-------------------------------------
// 12/04/2013| André Nobre Vinagre        | - Corrigido posicionamento do botão de redirecionamento
//           |                            | para o Cadastro de Fornecedor
//           |                            |
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 da conta contábil no cadastro
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5301_CadastramentoTituloDespesaPgto
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
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            CarregaHistoricos();
            CarregaTiposDocumento();
            CarregaCentrosCusto();
            CarregaGrupoContasContabeis();
            CarregaSubgrupo();
            CarregaSubgrupo2();
            CarregaContasContabeis();
            CarregaLocaisCobranca();
            CarregaBancos();
            CarregaAgencias();
            CarregaContratos();
            CarregaFornecedores();
            CarregaAgrupadores();

            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("6234_CadastroFornecedorProdServ")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                lnkCadasForne.HRef = "/" + String.Format("{0}?moduloNome=+Cadastro+de+Fornecedores+de+Produtos+e+Serviços&", admModuloMatr.nomURLModulo);
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = txtDataSituacao.Text = dataAtual;
                txtNumeroParcela.Enabled = txtNumeroDocumento.Enabled = true;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
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

            if (ddlTipoDocumento.SelectedIndex > 0 && TB086_TIPO_DOC.RetornaPeloCoTipoDoc(int.Parse(ddlTipoDocumento.SelectedValue)).SIG_TIPO_DOC == "BOL")
            {
                if (ddlBanco.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Banco deve ser informado");
                    return;
                }
                else if (ddlAgencia.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Agência deve ser informada");
                    return;
                }
                else if (txtConta.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "N° da Conta deve ser informado");
                    return;
                }
            }

            int coForn = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

            TB38_CTA_PAGAR tb38 = RetornaEntidade();

            if (tb38 == null)
            {
                tb38 = new TB38_CTA_PAGAR();

                tb38.CO_EMP = LoginAuxili.CO_EMP;
                tb38.NU_DOC = txtNumeroDocumento.Text;
                tb38.NU_PAR = int.Parse(txtNumeroParcela.Text);
                tb38.DT_CAD_DOC = DateTime.Now;
            }

            tb38.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb38.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb38.TB41_FORNEC = TB41_FORNEC.RetornaPelaChavePrimaria(coForn);
            tb38.QT_PAR = int.Parse(txtQuantidadeParcelas.Text);
            tb38.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(int.Parse(ddlTipoDocumento.SelectedValue));
            tb38.CO_REF_DOCU = txtCodRefDocumento.Text;
            tb38.CO_CON_DESFIX = ddlContrato.SelectedValue != "" ? ddlContrato.SelectedValue : null;
            tb38.CO_ADITI_DESFIX = ddlAditivo.SelectedValue != "" ? (int?)int.Parse(ddlAditivo.SelectedValue) : null;
            tb38.DT_EMISS_DOCTO = DateTime.Parse(txtDataDocumento.Text);
            tb38.DT_VEN_DOC = DateTime.Parse(txtDataVencimento.Text);
            tb38.FL_TIPO_COB = ddlTipoCobranca.SelectedValue != "" ? ddlTipoCobranca.SelectedValue : null;
            tb38.TB101_LOCALCOBRANCA = TB101_LOCALCOBRANCA.RetornaPelaChavePrimaria(int.Parse(ddlLocalCobranca.SelectedValue));
            tb38.TB30_AGENCIA = ddlAgencia.SelectedValue != "" ? TB30_AGENCIA.RetornaPelaChavePrimaria(ddlBanco.SelectedValue, int.Parse(ddlAgencia.SelectedValue)) : null;
            tb38.NU_CTA_DOC = txtConta.Text;
            tb38.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(int.Parse(ddlHistorico.SelectedValue));
            tb38.DE_COM_HIST = txtComplementoHistorico.Text;
            tb38.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(int.Parse(ddlCentroCusto.SelectedValue));
            tb38.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(int.Parse(ddlContaContabil.SelectedValue));
            tb38.VR_TOT_DOC = decimal.Parse(txtValorTotal.Text);
            tb38.VR_PAR_DOC = decimal.Parse(txtValorParcela.Text);
            tb38.VR_MUL_DOC = txtValorMulta.Text != "" ? (decimal?)decimal.Parse(txtValorMulta.Text) : null;
            tb38.CO_FLAG_TP_VALOR_MUL = chkValorMultaPercentual.Checked ? "P" : "V";
            //Formatando o valor do Juros para salvar
            decimal jurosDec = decimal.Zero;
            bool convertido = decimal.TryParse(txtValorJuros.Text, out jurosDec);
            tb38.VR_JUR_DOC = convertido ? (decimal?)jurosDec : null;
            tb38.CO_FLAG_TP_VALOR_JUR = chkValorJurosPercentual.Checked ? "P" : "V";
            tb38.VR_DES_DOC = txtValorDesconto.Text != "" ? (decimal?)decimal.Parse(txtValorDesconto.Text) : null;
            tb38.CO_FLAG_TP_VALOR_DES = chkValorDescontoPercentual.Checked ? "P" : "V";
            tb38.VR_DES_ANTEC = txtDescontoBolsa.Text != "" ? (decimal?)decimal.Parse(txtDescontoBolsa.Text) : null;
            tb38.CO_FLAG_TP_VALOR_DES_ANTEC = chkValorDescontoBolsaPercentual.Checked ? "P" : "V";
            tb38.VR_PAG = txtValorRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorRecebido.Text) : null;
            tb38.VR_MUL_PAG = txtValorMultaRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorMultaRecebido.Text) : null;
            tb38.VR_JUR_PAG = txtValorJurosRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorJurosRecebido.Text) : null;
            tb38.VR_DES_PAG = txtValorDescontoRecebido.Text != "" ? (decimal?)decimal.Parse(txtValorDescontoRecebido.Text) : null;
            tb38.DE_OBS = txtObservacao.Text;
            tb38.CO_BARRA_DOC = txtCodigoBarras.Text;
            tb38.IC_SIT_DOC = ddlSituacao.SelectedValue;
            tb38.CO_AGRUP_RECDESP = (ddlAgrupador.SelectedValue != "") ? (int?)int.Parse(ddlAgrupador.SelectedValue) : null;
            tb38.DT_SITU_DOC = DateTime.Now;
            tb38.DT_ALT_REGISTRO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb38;
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                TB38_CTA_PAGAR tb38 = RetornaEntidade();

                if (tb38 != null)
                {
                    if (GestorEntities.Delete(tb38) <= 0)
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
                tb38.TB30_AGENCIAReference.Load();

                txtCodRefDocumento.Text = tb38.CO_REF_DOCU;
                txtCodigoBarras.Text = tb38.CO_BARRA_DOC;
                txtComplementoHistorico.Text = tb38.DE_COM_HIST;
                txtConta.Text = tb38.NU_CTA_DOC;
                txtDataCadastro.Text = tb38.DT_CAD_DOC.ToString("dd/MM/yyyy");
                txtDataRecebimento.Text = tb38.DT_REC_DOC != null ? tb38.DT_REC_DOC.Value.ToString("dd/MM/yyyy") : "";
                txtDataVencimento.Text = tb38.DT_VEN_DOC.ToString("dd/MM/yyyy");
                txtDataDocumento.Text = tb38.DT_EMISS_DOCTO.ToString("dd/MM/yyyy");
                txtDataSituacao.Text = tb38.DT_SITU_DOC != null ? tb38.DT_SITU_DOC.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                txtDescontoBolsa.Text = tb38.VR_DES_ANTEC.ToString();
                txtNumeroDocumento.Text = tb38.NU_DOC;
                txtNumeroParcela.Text = tb38.NU_PAR.ToString();
                txtObservacao.Text = tb38.DE_OBS;
                txtQuantidadeParcelas.Text = tb38.QT_PAR.ToString();
                txtValorDesconto.Text = String.Format("{0:0.00}", tb38.VR_DES_DOC);
                txtValorDescontoRecebido.Text = String.Format("{0:0.00}", tb38.VR_DES_PAG);
                txtValorJuros.Text = String.Format("{0:0.0000}", tb38.VR_JUR_DOC);
                txtValorJurosRecebido.Text = String.Format("{0:0.00}", tb38.VR_JUR_PAG);
                txtValorMulta.Text = String.Format("{0:0.00}", tb38.VR_MUL_DOC);
                txtValorMultaRecebido.Text = String.Format("{0:0.00}", tb38.VR_MUL_PAG);
                txtValorParcela.Text = String.Format("{0:0.00}", tb38.VR_PAR_DOC);
                txtValorRecebido.Text = String.Format("{0:0.00}", tb38.VR_PAG);
                txtValorTotal.Text = String.Format("{0:0.00}", tb38.VR_TOT_DOC);
                ddlCentroCusto.SelectedValue = tb38.TB099_CENTRO_CUSTO != null ? tb38.TB099_CENTRO_CUSTO.CO_CENT_CUSTO.ToString() : "";
                tb38.TB56_PLANOCTA.TB54_SGRP_CTAReference.Load();
                tb38.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                

                ddlTipoConta.SelectedValue = tb38.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                CarregaGrupoContasContabeis();
                ddlGrupoConta.SelectedValue = tb38.TB56_PLANOCTA.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                CarregaSubgrupo();
                ddlSubGrupoConta.SelectedValue = tb38.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                CarregaSubgrupo2();
                ddlSubGrupo2Conta.SelectedValue = tb38.TB56_PLANOCTA.TB055_SGRP2_CTA != null ? tb38.TB56_PLANOCTA.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                CarregaContasContabeis();
                ddlContaContabil.SelectedValue = tb38.TB56_PLANOCTA.CO_SEQU_PC.ToString();
                CarregaCodigoContaContabil();
                
                ddlAgrupador.SelectedValue = (tb38.CO_AGRUP_RECDESP.HasValue) ? tb38.CO_AGRUP_RECDESP.ToString() : "";
                ddlHistorico.SelectedValue = tb38.TB39_HISTORICO != null ? tb38.TB39_HISTORICO.CO_HISTORICO.ToString() : "";
                ddlLocalCobranca.SelectedValue = tb38.TB101_LOCALCOBRANCA != null ? tb38.TB101_LOCALCOBRANCA.CO_LOC_COB.ToString() : "";
                ddlSituacao.SelectedValue = tb38.IC_SIT_DOC;
                ddlTipoDocumento.SelectedValue = tb38.TB086_TIPO_DOC != null ? tb38.TB086_TIPO_DOC.CO_TIPO_DOC.ToString() : "";
                ddlBanco.SelectedValue = tb38.TB30_AGENCIA != null ? tb38.TB30_AGENCIA.IDEBANCO : "";
                CarregaAgencias();
                ddlAgencia.SelectedValue = tb38.TB30_AGENCIA != null ? tb38.TB30_AGENCIA.CO_AGENCIA.ToString() : "";

                ddlTipoCobranca.SelectedValue = tb38.FL_TIPO_COB != null ? tb38.FL_TIPO_COB : "";

                if (ddlTipoCobranca.SelectedValue == "B")
                {
                    ddlBanco.Enabled = ddlAgencia.Enabled = txtConta.Enabled = true;
                }
                else
                {
                    ddlBanco.Enabled = ddlAgencia.Enabled = txtConta.Enabled = false;
                    ddlBanco.SelectedValue = ddlAgencia.SelectedValue = txtConta.Text = "";
                }

                chkValorDescontoBolsaPercentual.Checked = tb38.CO_FLAG_TP_VALOR_DES_ANTEC == "P";
                chkValorDescontoPercentual.Checked = tb38.CO_FLAG_TP_VALOR_DES == "P";
                chkValorJurosPercentual.Checked = tb38.CO_FLAG_TP_VALOR_JUR == "P";
                chkValorMultaPercentual.Checked = tb38.CO_FLAG_TP_VALOR_MUL == "P";

                ddlTpPes.SelectedValue = tb38.TB41_FORNEC.TP_FORN;
                CarregaFornecedores();
                ddlNomeFonte.SelectedValue = tb38.TB41_FORNEC.CO_FORN.ToString();
                CarregaCodigoFornecedor();

                CarregaContratos();
                ddlContrato.SelectedValue = tb38.CO_CON_DESFIX != null ? tb38.CO_CON_DESFIX.ToString() : "";
                CarregaAditivos();
                ddlAditivo.SelectedValue = tb38.CO_ADITI_DESFIX != null ? tb38.CO_ADITI_DESFIX.ToString() : "";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB38_CTA_PAGAR</returns>
        private TB38_CTA_PAGAR RetornaEntidade()
        {
            return TB38_CTA_PAGAR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringPelaChave("doc"),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("par"));
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Contratos
        /// </summary>
        private void CarregaContratos()
        {
            int coForn = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

            ddlContrato.Items.Clear();

            if (coForn != 0)
            {
                ddlContrato.DataSource = (from tb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                                          where tb37.TB41_FORNEC.CO_FORN == coForn && tb37.TP_CON_RECDES == "D"
                                          && tb37.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
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
                ddlAditivo.DataSource = (from tb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                                         where tb37.CO_CON_RECDES == coConRecDes
                                         select new { tb37.CO_ADITI_RECDES }).OrderBy(r => r.CO_ADITI_RECDES);

                ddlAditivo.DataTextField = "CO_ADITI_RECDES";
                ddlAditivo.DataValueField = "CO_ADITI_RECDES";
                ddlAditivo.DataBind();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Fornecedores
        /// </summary>
        private void CarregaFornecedores()
        {
            txtCodigoFonte.Text = txtApelidoFonte.Text = "";
            ddlNomeFonte.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                       where tb41.TP_FORN == ddlTpPes.SelectedValue && tb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                       select new { tb41.CO_FORN, tb41.DE_RAZSOC_FORN }).OrderBy(f => f.DE_RAZSOC_FORN);

            ddlNomeFonte.DataTextField = "DE_RAZSOC_FORN";
            ddlNomeFonte.DataValueField = "CO_FORN";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
            ddlNomeFonte.Enabled = true;
        }

        /// <summary>
        /// Método que carrega informação do Código do Fornecedor selecionado
        /// </summary>
        private void CarregaCodigoFornecedor()
        {
            int coForn = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

            txtCodigoFonte.Text = txtApelidoFonte.Text = "";

            if (coForn == 0)
                return;

            TB41_FORNEC tb41 = TB41_FORNEC.RetornaPelaChavePrimaria(coForn);
            txtCodigoFonte.Text = (tb41.TP_FORN == "F" && tb41.CO_CPFCGC_FORN.Length >= 11) ? tb41.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb41.TP_FORN == "J" && tb41.CO_CPFCGC_FORN.Length >= 14) ? tb41.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb41.CO_CPFCGC_FORN);
            txtApelidoFonte.Text = tb41.NO_FAN_FOR;
        }

        /// <summary>
        /// Método que carrega o dropdown de Históricos
        /// </summary>
        private void CarregaHistoricos()
        {
            ddlHistorico.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                       where tb39.FLA_TIPO_HISTORICO == "D"
                                       select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataBind();

            ddlHistorico.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Documento
        /// </summary>
        private void CarregaTiposDocumento()
        {
            ddlTipoDocumento.DataSource = (from tb086 in TB086_TIPO_DOC.RetornaTodosRegistros()
                                           select new { tb086.CO_TIPO_DOC, tb086.DES_TIPO_DOC });

            ddlTipoDocumento.DataTextField = "DES_TIPO_DOC";
            ddlTipoDocumento.DataValueField = "CO_TIPO_DOC";
            ddlTipoDocumento.DataBind();

            ddlTipoDocumento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros()
                                   select new { tb29.IDEBANCO });

            ddlBanco.DataTextField = "IDEBANCO";
            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        private void CarregaAgencias()
        {
            string ideBanco = ddlBanco.SelectedValue;

            ddlAgencia.Items.Clear();

            if (ideBanco != "")
            {
                ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaPeloBanco(ideBanco)
                                         select new { tb30.CO_AGENCIA }).OrderBy(a => a.CO_AGENCIA);

                ddlAgencia.DataTextField = "CO_AGENCIA";
                ddlAgencia.DataValueField = "CO_AGENCIA";
                ddlAgencia.DataBind();
            }

            ddlAgencia.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Centro de Custo
        /// </summary>
        private void CarregaCentrosCusto()
        {
            ddlCentroCusto.Items.Clear();

            var lTb099 = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                          where tb099.TB14_DEPTO.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                          select new { tb099.CO_CENT_CUSTO, tb099.DE_CENT_CUSTO, tb099.TB14_DEPTO.CO_SIGLA_DEPTO }).OrderBy(c => c.CO_SIGLA_DEPTO).ThenBy(c => c.DE_CENT_CUSTO);

            foreach (var iTb099 in lTb099)
                ddlCentroCusto.Items.Add(new ListItem(iTb099.CO_SIGLA_DEPTO + " - " + iTb099.DE_CENT_CUSTO, iTb099.CO_CENT_CUSTO.ToString()));

            ddlCentroCusto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Conta Contábil
        /// </summary>
        private void CarregaGrupoContasContabeis()
        {
            var res = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                        where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && tb53.TP_GRUP_CTA == ddlTipoConta.SelectedValue
                       select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA, tb53.NR_GRUP_CTA }).OrderBy(p => p.NR_GRUP_CTA).ToList();

            ddlGrupoConta.DataSource = from r in res
                                       select new
                                       {
                                           r.CO_GRUP_CTA,
                                           DE_GRUP_CTA = r.NR_GRUP_CTA.ToString().PadLeft(2, '0') + " - " + r.DE_GRUP_CTA
                                       };

            ddlGrupoConta.DataTextField = "DE_GRUP_CTA";
            ddlGrupoConta.DataValueField = "CO_GRUP_CTA";
            ddlGrupoConta.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubgrupo()
        {
            int coGrupCta = ddlGrupoConta.SelectedValue != "" ? int.Parse(ddlGrupoConta.SelectedValue) : 0;

            var result = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                          where tb54.CO_GRUP_CTA == coGrupCta
                          select new { tb54.DE_SGRUP_CTA, tb54.CO_SGRUP_CTA, tb54.NR_SGRUP_CTA }).ToList();

            ddlSubGrupoConta.DataSource = (from res in result
                                      select new
                                      {
                                          DE_SGRUP_CTA = res.NR_SGRUP_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP_CTA,
                                          res.CO_SGRUP_CTA
                                      }).OrderBy(p => p.DE_SGRUP_CTA);

            ddlSubGrupoConta.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupoConta.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupoConta.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo2
        /// </summary>
        private void CarregaSubgrupo2()
        {
            int coGrupCta = ddlGrupoConta.SelectedValue != "" ? int.Parse(ddlGrupoConta.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupoConta.SelectedValue != "" ? int.Parse(ddlSubGrupoConta.SelectedValue) : 0;

            var result = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                          join tb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on tb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals tb54.CO_SGRUP_CTA
                          where tb54.CO_GRUP_CTA == coGrupCta && tb54.CO_SGRUP_CTA == coSubGrupo
                          select new
                          {
                              tb055.NR_SGRUP2_CTA,
                              tb055.DE_SGRUP2_CTA,
                              tb055.CO_SGRUP2_CTA
                          }).ToList();

            ddlSubGrupo2Conta.DataSource = (from res in result
                                       select new
                                       {
                                           DE_SGRUP2_CTA = res.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP2_CTA,
                                           res.CO_SGRUP2_CTA
                                       });

            ddlSubGrupo2Conta.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2Conta.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2Conta.DataBind();

            ddlSubGrupo2Conta.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contábil
        /// </summary>
        private void CarregaContasContabeis()
        {
            int coGrupo = ddlGrupoConta.SelectedValue != "" ? int.Parse(ddlGrupoConta.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupoConta.SelectedValue != "" ? int.Parse(ddlSubGrupoConta.SelectedValue) : 0;
            int coSubGrupo2 = ddlSubGrupo2Conta.SelectedValue != "" ? int.Parse(ddlSubGrupo2Conta.SelectedValue) : 0;

            var res = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                        where tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                        && tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == ddlTipoConta.SelectedValue
                        && tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == coGrupo
                        && (coSubGrupo2 != 0 ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSubGrupo2 : 0 == 0)
                       select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC, tb56.NU_CONTA_PC }).OrderBy(p => p.NU_CONTA_PC).ToList();

            ddlContaContabil.DataSource = from r in res
                                          select new
                                          {
                                              r.CO_SEQU_PC,
                                              DE_CONTA_PC = r.NU_CONTA_PC.ToString().PadLeft(4, '0') + " - " + r.DE_CONTA_PC
                                          };

            ddlContaContabil.DataTextField = "DE_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_SEQU_PC";
            ddlContaContabil.DataBind();

            ddlContaContabil.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega informações da Conta Contábil selecionada
        /// </summary>
        private void CarregaCodigoContaContabil()
        {
            int coSequPc = ddlContaContabil.SelectedValue != "" ? int.Parse(ddlContaContabil.SelectedValue) : 0;

            txtCodigoContaContabil.Text = "";

            if (coSequPc == 0)
                return;

            TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc);
            tb56.TB54_SGRP_CTAReference.Load();
            tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
            string tipoConta = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "P" ? "2" : "4";
            TB055_SGRP2_CTA tb55 = TB055_SGRP2_CTA.RetornaPelaChavePrimaria(tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA : 0);
            txtCodigoContaContabil.Text = tipoConta + "." + tb56.TB54_SGRP_CTA.TB53_GRP_CTA.NR_GRUP_CTA.ToString().PadLeft(2, '0') + "." + tb56.TB54_SGRP_CTA.NR_SGRUP_CTA.ToString().PadLeft(3, '0')
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

            ddlLocalCobranca.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de CarregaLocais
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "D" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlNomeFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoFornecedor();
            CarregaContratos();
            CarregaAditivos();
        }

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedIndex > 0 && TB086_TIPO_DOC.RetornaPeloCoTipoDoc(int.Parse(ddlTipoDocumento.SelectedValue)).SIG_TIPO_DOC == "BOL")
            {
                
            }
            else
            {
                ddlAgencia.Enabled = txtConta.Enabled = ddlBanco.Enabled = false;
                ddlAgencia.Text = txtConta.Text = "";
                ddlBanco.SelectedIndex = 0;
            }
        }

        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis();
            CarregaSubgrupo();
            CarregaSubgrupo2();
            CarregaContasContabeis();
        }

        protected void ddlGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo();
            CarregaSubgrupo2();
            CarregaContasContabeis();
        }

        protected void ddlSubGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2();
            CarregaContasContabeis();
        }

        protected void ddlSubGrupo2Conta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis();
        }

        protected void ddlContaContabil_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil();
        }

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
        }

        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAditivos();
        }

        protected void ddlTpPes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFornecedores();
        }

        protected void ddlTipoCobranca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCobranca.SelectedValue == "B")
            {
                CarregaBancos();
                CarregaAgencias();
                ddlBanco.Enabled = ddlAgencia.Enabled = txtConta.Enabled = true;
            }
            else
            {
                ddlBanco.Enabled = ddlAgencia.Enabled = txtConta.Enabled = false;
                ddlBanco.SelectedValue = ddlAgencia.SelectedValue = txtConta.Text = "";
            }
        }
    }
}