//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: CADASTRAMENTO ACORDOS/ADITIVOS DE COMPROMISSOS DE DESPESAS FIXAS EXTERNAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5405_CadastramentoAditCompDespesaFixaExt
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
                AuxiliPagina.RedirecionaParaPaginaErro("Selecione uma despesa fixa para gerar aditivo.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

            if (!Page.IsPostBack)
            {
                CarregaFornecedores();
                CarregaCentroCusto();
                CarregaDepartamentos();
                CarregaHistorico();
                CarregaTipoReceita();
                CarregaTiposDocumento();
                ddlTipoReceita.SelectedValue = "2";
                CarregaSubGrupo();
                CarregaConta();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDtCadastro.Text = txtDtStatus.Text = dataAtual;
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    txtDtInicioContrato.Enabled = txtDtFimContrato.Enabled = true;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                TB37_RECDES_FIXA tb37 = RetornaEntidade();

                bool flagTodosStaAberto = true;

                if (tb37 != null)
                {
//----------------> Retorna a lista de Contas lançadas para a Despesa fixa selecionada
                    List<TB38_CTA_PAGAR> lstTb38 = (from lTb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                                    where lTb38.CO_EMP.Equals(tb37.CO_EMP)
                                                    && lTb38.NU_DOC.Equals(tb37.CO_CON_RECDES)
                                                    select lTb38).ToList();

//----------------> Faz a verificação para saber se existe alguma conta com o status diferente de "Em aberto"
                    foreach (TB38_CTA_PAGAR tb38 in lstTb38)
                    {
                        if (tb38.IC_SIT_DOC != "A")
                        {
                            flagTodosStaAberto = false;
                            break;
                        }
                    }

//----------------> Se todas as contas estiverem com o status "Em aberto", exclui as mesmas e depois exclui a Despesa fixa
                    if (flagTodosStaAberto)
                    {
                        foreach (TB38_CTA_PAGAR tb38 in lstTb38)
                        {
                            TB38_CTA_PAGAR.Delete(tb38, true);
                        }
                        CurrentPadraoCadastros.CurrentEntity = tb37;
                    }
                    else
                        AuxiliPagina.EnvioMensagemErro(this, "Há Contas cadastradas para a Receita atual com Staus diferente de  \"Aberta\". Não é possível excluir essa Receita.");
                }

                return;
            }

            if (Page.IsValid)
            {
                int intRetorno;
                decimal decimalRetorno;
                DateTime dataRetorno;

                TB37_RECDES_FIXA tb37_Original = RetornaEntidade();
                tb37_Original.TB000_INSTITUICAOReference.Load();
                tb37_Original.TB25_EMPRESAReference.Load();
                tb37_Original.TB41_FORNECReference.Load();

                TB37_RECDES_FIXA tb37 = new TB37_RECDES_FIXA();

                tb37.CO_CON_RECDES = tb37_Original.CO_CON_RECDES;
                tb37.TB25_EMPRESA = tb37_Original.TB25_EMPRESA;
                tb37.TB000_INSTITUICAO = tb37_Original.TB000_INSTITUICAO;
                tb37.TB41_FORNEC = tb37_Original.TB41_FORNEC;
                tb37.CO_ADITI_RECDES = int.Parse(txtNumAditivo.Text);

//------------> Despesa Fixa (Dédito = "D")
                tb37.TP_CON_RECDES = "D";
                tb37.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(int.TryParse(ddlContaContabil.SelectedValue, out intRetorno) ? intRetorno : 0);
                tb37.TB39_HISTORICO = ddlHistLancamento.SelectedValue != "" ? TB39_HISTORICO.RetornaPelaChavePrimaria(int.Parse(ddlHistLancamento.SelectedValue)) : null;
                tb37.TB099_CENTRO_CUSTO = ddlCentroCusto.SelectedValue != "" ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(int.Parse(ddlCentroCusto.SelectedValue)) : null;
                tb37.CO_ADITI_RECDES = int.Parse(txtNumAditivo.Text);
                tb37.NU_PUBLI_RECDES = txtNumPublicacao.Text != "" ? txtNumPublicacao.Text : null;
                tb37.DT_PUBLI_RECDES = txtDataPublicacao.Text != "" ? (DateTime?)DateTime.Parse(txtDataPublicacao.Text) : null;
                tb37.TB086_TIPO_DOC = ddlTipoDocumento.SelectedValue != "" ? TB086_TIPO_DOC.RetornaTodosRegistros().Where(t => t.SIG_TIPO_DOC == ddlTipoDocumento.SelectedValue).FirstOrDefault() : null;
                tb37.CO_DOC_RECDES = txtNumDoc.Text != "" ? txtNumDoc.Text : null;
                tb37.DT_INI_CON_RECDES = DateTime.Parse(txtDtInicioContrato.Text);
                tb37.DT_FIM_CON_RECDES = DateTime.Parse(txtDtFimContrato.Text);
                tb37.QT_PARC_RECDES = int.TryParse(txtQtParcelas.Text, out intRetorno) ? intRetorno : 1;
                tb37.DT_VENC_PRIM_PARC = DateTime.Parse(txtDtPrimeiroVencto.Text);
                tb37.NU_DIA_INTER_RECDES = int.TryParse(txtDiasIntervalo.Text, out intRetorno) ? (int?)intRetorno : null;
                tb37.VR_RECDES = decimal.Parse(txtValorDocumento.Text);
                tb37.DT_CAN_RECDES = DateTime.TryParse(txtDtCancelamento.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb37.VR_MUL_RECDES = decimal.TryParse(txtMulta.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb37.CO_FLAG_TP_VALOR_MUL = cbFlagPercentualMulta.Checked ? "P" : "V";
                tb37.VR_JUR_RECDES = decimal.TryParse(txtJuros.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb37.CO_FLAG_TP_VALOR_JUR = cbFlagPercentualJuros.Checked ? "P" : "V";
                tb37.VR_DES_RECDES = decimal.TryParse(txtDesconto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb37.CO_FLAG_TP_VALOR_DES = cbFlagPercentualDesconto.Checked ? "P" : "V";
                tb37.DT_CAD_RECDES = DateTime.Now;
                tb37.DT_ALT_REGISTRO = DateTime.Now;
                tb37.IC_SIT_RECDES = ddlStatus.SelectedValue;

                if (TB37_RECDES_FIXA.SaveOrUpdate(tb37, true) > 0)
                {
//----------------> Lança cada parcela no Contas a Pagar
                    for (int i = 0; i < tb37.QT_PARC_RECDES; i++)
                    {
                        TB38_CTA_PAGAR tb38 = new TB38_CTA_PAGAR();

                        double doubleDiasIntervalo;
                        double.TryParse(tb37.NU_DIA_INTER_RECDES.ToString(), out doubleDiasIntervalo);

                        tb38.TB25_EMPRESA = tb37.TB25_EMPRESA;
                        tb38.CO_EMP = tb37.CO_EMP;
                        tb38.NU_DOC = tb37.CO_DOC_RECDES;
                        tb38.CO_CON_DESFIX = tb37.CO_CON_RECDES;
                        tb38.NU_PAR = i + 1;
                        tb38.QT_PAR = tb37.QT_PARC_RECDES;
                        tb38.DT_CAD_DOC = tb37.DT_CAD_RECDES;
                        tb38.TB000_INSTITUICAO = tb37_Original.TB000_INSTITUICAO;
                        tb38.DE_COM_HIST = null;
                        tb38.VR_TOT_DOC = tb37.VR_RECDES;
                        tb38.VR_PAR_DOC = tb37.VR_RECDES / tb37.QT_PARC_RECDES;
                        tb38.DT_VEN_DOC = i.Equals(0) ? tb37.DT_VENC_PRIM_PARC : tb37.DT_VENC_PRIM_PARC.AddDays(doubleDiasIntervalo * i);
                        tb38.DT_EMISS_DOCTO = tb37.DT_INI_CON_RECDES;
                        tb38.TB39_HISTORICO = tb37.TB39_HISTORICO;
                        tb38.TB086_TIPO_DOC = tb37.TB086_TIPO_DOC;
                        tb38.TB099_CENTRO_CUSTO = tb37.TB099_CENTRO_CUSTO;
                        tb38.VR_MUL_DOC = tb37.VR_MUL_RECDES;
                        tb38.VR_JUR_DOC = tb37.VR_JUR_RECDES;
                        tb38.VR_DES_DOC = tb37.VR_DES_RECDES;
                        tb38.CO_FLAG_TP_VALOR_MUL = tb37.CO_FLAG_TP_VALOR_MUL;
                        tb38.CO_FLAG_TP_VALOR_JUR = tb37.CO_FLAG_TP_VALOR_JUR;
                        tb38.CO_FLAG_TP_VALOR_DES = tb37.CO_FLAG_TP_VALOR_DES;
                        tb38.CO_FLAG_TP_VALOR_DES_ANTEC = "V";
                        tb38.IC_SIT_DOC = tb37.IC_SIT_RECDES;
                        tb38.TB41_FORNEC = tb37.TB41_FORNEC;
                        tb38.TB56_PLANOCTA = tb37.TB56_PLANOCTA;
                        tb38.DT_ALT_REGISTRO = DateTime.Now;

                        TB38_CTA_PAGAR.SaveOrUpdate(tb38, true);
                    }

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Item adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
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
            TB37_RECDES_FIXA tb37 = RetornaEntidade();

            if (tb37 != null)
            {
                tb37.TB099_CENTRO_CUSTOReference.Load();
                tb37.TB39_HISTORICOReference.Load();
                tb37.TB56_PLANOCTAReference.Load();
                tb37.TB56_PLANOCTA.TB54_SGRP_CTAReference.Load();
                tb37.TB086_TIPO_DOCReference.Load();
                tb37.TB41_FORNECReference.Load();
                tb37.TB41_FORNEC.TB905_BAIRROReference.Load();
                tb37.TB41_FORNEC.TB905_BAIRRO.TB904_CIDADEReference.Load();

                if (tb37.TB099_CENTRO_CUSTO != null)
                    tb37.TB099_CENTRO_CUSTO.TB14_DEPTOReference.Load();

                ddlNomeFantasia.SelectedValue = tb37.TB41_FORNEC.CO_FORN.ToString();
                txtCNPJ.Text = (tb37.TB41_FORNEC.TP_FORN == "F" && tb37.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 11) ? tb37.TB41_FORNEC.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb37.TB41_FORNEC.TP_FORN == "J" && tb37.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 14) ? tb37.TB41_FORNEC.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb37.TB41_FORNEC.CO_CPFCGC_FORN);
                txtLogradouro.Text = tb37.TB41_FORNEC.DE_END_FORN;
                txtUF.Text = tb37.TB41_FORNEC.CO_UF_FORN;
                if (tb37.TB41_FORNEC.TB905_BAIRRO != null)
                {
                    txtCidade.Text = tb37.TB41_FORNEC.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE;
                    txtBairro.Text = tb37.TB41_FORNEC.TB905_BAIRRO.NO_BAIRRO;
                }
                txtComplemento.Text = tb37.TB41_FORNEC.DE_COM_FORN;
                txtCEP.Text = tb37.TB41_FORNEC.CO_CEP_FORN;
                ddlTipoReceita.SelectedValue = tb37.TB56_PLANOCTA != null ? tb37.TB56_PLANOCTA.TB54_SGRP_CTA.CO_GRUP_CTA.ToString() : "";
                CarregaSubGrupo();
                ddlSubGrupoReceita.SelectedValue = tb37.TB56_PLANOCTA != null ? tb37.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString() : "";
                CarregaConta();
                ddlContaContabil.SelectedValue = tb37.TB56_PLANOCTA != null ? tb37.TB56_PLANOCTA.CO_SEQU_PC.ToString() : "";
                ddlHistLancamento.SelectedValue = tb37.TB39_HISTORICO != null ? tb37.TB39_HISTORICO.CO_HISTORICO.ToString() : "";
                ddlDepartamento.SelectedValue = tb37.TB099_CENTRO_CUSTO != null && tb37.TB099_CENTRO_CUSTO.TB14_DEPTO != null ? tb37.TB099_CENTRO_CUSTO.TB14_DEPTO.CO_DEPTO.ToString() : "";
                CarregaCentroCusto();
                ddlCentroCusto.SelectedValue = tb37.TB099_CENTRO_CUSTO != null ? tb37.TB099_CENTRO_CUSTO.CO_CENT_CUSTO.ToString() : "";
                txtNumContrato.Text = tb37.CO_CON_RECDES;

                int intMaxNumAditivo = (from lTb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                                        where lTb37.CO_EMP == tb37.CO_EMP && lTb37.CO_CON_RECDES == tb37.CO_CON_RECDES
                                        select new { lTb37.CO_ADITI_RECDES }).Max( r => r.CO_ADITI_RECDES ) + 1;

                txtNumAditivo.Text = intMaxNumAditivo.ToString();
                txtDtCadastro.Text = tb37.DT_CAD_RECDES.ToString("dd/MM/yyyy");
                txtDtStatus.Text = tb37.DT_ALT_REGISTRO != null ? tb37.DT_ALT_REGISTRO.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                txtDataPublicacao.Text = tb37.DT_PUBLI_RECDES != null ? tb37.DT_PUBLI_RECDES.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB37_RECDES_FIXA</returns>
        private TB37_RECDES_FIXA RetornaEntidade()
        {
            return TB37_RECDES_FIXA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(Resources.QueryStrings.CoEmp), 
                QueryStringAuxili.RetornaQueryStringPelaChave("con"),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("adi"));
        }
        #endregion

        #region Carregamentro DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Despesa
        /// </summary>
        private void CarregaTipoReceita()
        {
            ddlTipoReceita.DataSource = TB53_GRP_CTA.RetornaTodosRegistros();

            ddlTipoReceita.DataTextField = "DE_GRUP_CTA";
            ddlTipoReceita.DataValueField = "CO_GRUP_CTA";
            ddlTipoReceita.DataBind();

            ddlTipoReceita.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Despesa
        /// </summary>
        private void CarregaSubGrupo()
        {
            int coGrupCta = ddlTipoReceita.SelectedValue != "" ? int.Parse(ddlTipoReceita.SelectedValue) : 0;

            ddlSubGrupoReceita.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                                             where tb54.CO_GRUP_CTA == coGrupCta
                                             select new { tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA });

            ddlSubGrupoReceita.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupoReceita.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupoReceita.DataBind();

            ddlSubGrupoReceita.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contábil
        /// </summary>
        private void CarregaConta()
        {
            int coSGrupCta = ddlSubGrupoReceita.SelectedValue != "" ? int.Parse(ddlSubGrupoReceita.SelectedValue) : 0;

            ddlContaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                           where tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
                                           select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy(p => p.DE_CONTA_PC);

            ddlContaContabil.DataTextField = "DE_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_SEQU_PC";
            ddlContaContabil.DataBind();

            ddlContaContabil.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Centro de Custo
        /// </summary>
        private void CarregaCentroCusto()
        {
            int coDepto = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;

            ddlCentroCusto.DataSource = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                                         where tb099.TB14_DEPTO.CO_DEPTO == coDepto
                                         select new { tb099.CO_CENT_CUSTO, tb099.DE_CENT_CUSTO });

            ddlCentroCusto.DataTextField = "DE_CENT_CUSTO";
            ddlCentroCusto.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCusto.DataBind();

            ddlCentroCusto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Histórico de Lançamento
        /// </summary>
        private void CarregaHistorico()
        {
            ddlHistLancamento.DataSource = from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                           where !tb39.FLA_TIPO_HISTORICO.Equals("D")
                                           select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO };

            ddlHistLancamento.DataTextField = "DE_HISTORICO";
            ddlHistLancamento.DataValueField = "CO_HISTORICO";
            ddlHistLancamento.DataBind();

            ddlHistLancamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Fornecedores
        /// </summary>
        private void CarregaFornecedores() 
        {
            ddlNomeFantasia.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                          select new { tb41.NO_FAN_FOR, tb41.CO_FORN }).OrderBy(f => f.NO_FAN_FOR);

            ddlNomeFantasia.DataTextField = "NO_FAN_FOR";
            ddlNomeFantasia.DataValueField = "CO_FORN";
            ddlNomeFantasia.DataBind();

            ddlNomeFantasia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamentos
        /// </summary>
        private void CarregaDepartamentos() 
        {
            ddlDepartamento.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                          where tb14.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                          select new { tb14.CO_DEPTO, tb14.NO_DEPTO });

            ddlDepartamento.DataTextField = "NO_DEPTO";
            ddlDepartamento.DataValueField = "CO_DEPTO";
            ddlDepartamento.DataBind();

            ddlDepartamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Documento
        /// </summary>
        private void CarregaTiposDocumento() 
        {
            ddlTipoDocumento.DataSource = (from tb086 in TB086_TIPO_DOC.RetornaTodosRegistros()
                                           select new { tb086.SIG_TIPO_DOC, tb086.DES_TIPO_DOC });

            ddlTipoDocumento.DataTextField = "DES_TIPO_DOC";
            ddlTipoDocumento.DataValueField = "SIG_TIPO_DOC";
            ddlTipoDocumento.DataBind();

            ddlTipoDocumento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Validadores

        protected void cvDataTerminoContrato_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtFimContrato.Text).Date < DateTime.Parse(txtDtInicioContrato.Text).Date)
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataPrimeiroVencto_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtPrimeiroVencto.Text).Date < DateTime.Parse(txtDtInicioContrato.Text).Date)
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataStatus_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtStatus.Text).Date < DateTime.Parse(txtDtCadastro.Text).Date)
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataCancelamento_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtCancelamento.Text).Date < DateTime.Parse(txtDtCadastro.Text).Date && txtDtCancelamento.Text != "")
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }
        #endregion

        protected void ddlSubGrupoReceita_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCentroCusto();
        }

        protected void ddlNomeFantasia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coCliente = ddlNomeFantasia.SelectedValue != "" ? int.Parse(ddlNomeFantasia.SelectedValue) : 0;

            txtCNPJ.Text = txtLogradouro.Text = txtUF.Text = txtCidade.Text = txtBairro.Text = txtComplemento.Text = txtCEP.Text = "";

            if (coCliente != 0)
            {
                TB41_FORNEC tb41 = TB41_FORNEC.RetornaPelaChavePrimaria(coCliente);

                if (tb41 != null)
                {
                    txtCNPJ.Text = (tb41.TP_FORN == "F" && tb41.CO_CPFCGC_FORN.Length >= 11) ? tb41.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb41.TP_FORN == "J" && tb41.CO_CPFCGC_FORN.Length >= 14) ? tb41.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb41.CO_CPFCGC_FORN);
                    txtLogradouro.Text = tb41.DE_END_FORN;
                    txtUF.Text = tb41.CO_UF_FORN;

                    tb41.TB905_BAIRROReference.Load();
                    tb41.TB905_BAIRRO.TB904_CIDADEReference.Load();

                    if (tb41.TB905_BAIRRO != null)
                    {
                        txtCidade.Text = tb41.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE;
                        txtBairro.Text = tb41.TB905_BAIRRO.NO_BAIRRO;
                    }

                    txtComplemento.Text = tb41.DE_COM_FORN;
                    txtCEP.Text = tb41.CO_CEP_FORN;
                }
            }
        }
    }
}