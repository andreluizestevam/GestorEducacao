//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: CADASTRAMENTO DE ACORDOS/ADITIVOS DE COMPROMISSOS DE RECEITAS EXTERNAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5403_CadastramentoAditCompReceitaFixa
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
                AuxiliPagina.RedirecionaParaPaginaErro("Selecione uma receita fixa para gerar aditivo.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

            if (!Page.IsPostBack)
            {
                CarregaNomeFantasia();
                CarregaDepartamento();
                CarregaCentroCusto();
                CarregaHistorico();
                CarregaTipoReceita();
                CarregaSubGrupo();
                CarregaConta();
                CarregaTipoDocumento();

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
//----------------> Retorna a lista de Contas lançadas para a receita fixa selecionada
                    List<TB47_CTA_RECEB> lstTb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                                    where lTb47.CO_EMP == tb37.CO_EMP && lTb47.NU_DOC == tb37.CO_CON_RECDES
                                                    select lTb47).ToList();

//----------------> Faz a verificação para saber se existe alguma conta com o status diferente de "Em aberto"
                    foreach (TB47_CTA_RECEB tb47 in lstTb47)
                    {
                        if (tb47.IC_SIT_DOC != "A")
                        {
                            flagTodosStaAberto = false;
                            break;
                        }
                    }

//----------------> Se todas as contas estiverem com o status "Em aberto", exclui as mesmas e depois exclui a receita fixa
                    if (flagTodosStaAberto)
                    {
                        foreach (TB47_CTA_RECEB tb47 in lstTb47)
                        {
                            TB47_CTA_RECEB.Delete(tb47, true);
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
                tb37_Original.TB103_CLIENTEReference.Load();
                tb37_Original.TB25_EMPRESAReference.Load();

                TB37_RECDES_FIXA tb37 = new TB37_RECDES_FIXA();

                tb37.CO_CON_RECDES = tb37_Original.CO_CON_RECDES;
                tb37.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb37_Original.TB25_EMPRESA.CO_EMP);
                tb37.TB000_INSTITUICAO = tb37_Original.TB000_INSTITUICAO;
                tb37.TB103_CLIENTE = tb37_Original.TB103_CLIENTE;
                tb37.CO_ADITI_RECDES = int.Parse(txtNumAditivo.Text);

//------------> Receita Fixa (Crédito = "C")
                tb37.TP_CON_RECDES = "C";
                tb37.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(int.TryParse(ddlContaContabil.SelectedValue, out intRetorno) ? intRetorno : 0);
                tb37.CO_CTA_LCX = int.TryParse(ddlContaContabil.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
                tb37.TB39_HISTORICO = ddlHistLancamento.SelectedValue != "" ? TB39_HISTORICO.RetornaPelaChavePrimaria(int.Parse(ddlHistLancamento.SelectedValue)) : null;
                tb37.TB099_CENTRO_CUSTO = ddlCentroCusto.SelectedValue != "" ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(int.Parse(ddlCentroCusto.SelectedValue)) : null;
                tb37.CO_ADITI_RECDES = int.Parse(txtNumAditivo.Text);
                tb37.NU_PUBLI_RECDES = txtNumPublicacao.Text != "" ? txtNumPublicacao.Text : null;
                tb37.DT_PUBLI_RECDES = txtDtPublicacao.Text != "" ? DateTime.Parse(txtDtPublicacao.Text) : (DateTime?)null;
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
                tb37.IC_SIT_RECDES = ddlStatus.SelectedValue;
                tb37.DT_ALT_REGISTRO = DateTime.Now;
                
                int intSalvo = TB37_RECDES_FIXA.SaveOrUpdate(tb37, true);

                if (intSalvo > 0)
                {
                    var lstTb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                                    where lTb47.CO_EMP == tb37.CO_EMP
                                                    && lTb47.CO_CON_RECFIX == tb37.CO_CON_RECDES
                                                    && lTb47.CO_ADITI_RECFIX.Value == tb37.CO_ADITI_RECDES
                                                    select lTb47).ToList();

//----------------> Lança cada parcela no Contas a Receber
                    for (int i = 0; i < tb37.QT_PARC_RECDES; i++)
                    {
                        tb37.TB25_EMPRESAReference.Load();
                        tb37.TB000_INSTITUICAOReference.Load();

                        if (lstTb47.Count < tb37.QT_PARC_RECDES)
                            lstTb47.Add(new TB47_CTA_RECEB());

                        double doubleDiasIntervalo;
                        double.TryParse(tb37.NU_DIA_INTER_RECDES.ToString(), out doubleDiasIntervalo);

                        if (lstTb47[i].CO_CON_RECFIX == null)
                        {
                            lstTb47[i].CO_EMP = tb37.CO_EMP;
                            lstTb47[i].CO_EMP_UNID_CONT = tb37.CO_EMP;
                            lstTb47[i].NU_DOC = tb37.CO_DOC_RECDES;
                            lstTb47[i].CO_CON_RECFIX = tb37.CO_CON_RECDES;
                            lstTb47[i].CO_ADITI_RECFIX = tb37.CO_ADITI_RECDES;
                            lstTb47[i].NU_PAR = i + 1;
                            lstTb47[i].QT_PAR = tb37.QT_PARC_RECDES;
                            lstTb47[i].DT_CAD_DOC = tb37.DT_CAD_RECDES;
                            lstTb47[i].TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb37.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);
                        }

                        var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                    where iTb25.CO_EMP == tb37.TB25_EMPRESA.CO_EMP
                                    select new { iTb25.CO_CTA_CAIXA, iTb25.CO_CTA_BANCO }).FirstOrDefault();

                        lstTb47[i].DE_COM_HIST = null;
                        lstTb47[i].VR_TOT_DOC = tb37.VR_RECDES;
                        lstTb47[i].VR_PAR_DOC = tb37.VR_RECDES / tb37.QT_PARC_RECDES;
                        lstTb47[i].DT_VEN_DOC = i.Equals(0) ? tb37.DT_VENC_PRIM_PARC : tb37.DT_VENC_PRIM_PARC.AddDays(doubleDiasIntervalo * i);
                        lstTb47[i].DT_EMISS_DOCTO = tb37.DT_INI_CON_RECDES;
                        lstTb47[i].TB39_HISTORICO = tb37.TB39_HISTORICO;
                        lstTb47[i].TB086_TIPO_DOC = tb37.TB086_TIPO_DOC;
                        lstTb47[i].TB099_CENTRO_CUSTO = tb37.TB099_CENTRO_CUSTO;
                        lstTb47[i].VR_MUL_DOC = tb37.VR_MUL_RECDES;
                        lstTb47[i].VR_JUR_DOC = tb37.VR_JUR_RECDES;
                        lstTb47[i].VR_DES_DOC = tb37.VR_DES_RECDES;
                        lstTb47[i].CO_FLAG_TP_VALOR_MUL = tb37.CO_FLAG_TP_VALOR_MUL;
                        lstTb47[i].CO_FLAG_TP_VALOR_JUR = tb37.CO_FLAG_TP_VALOR_JUR;
                        lstTb47[i].CO_FLAG_TP_VALOR_DES = tb37.CO_FLAG_TP_VALOR_DES;
                        lstTb47[i].CO_FLAG_TP_VALOR_OUT = "V";
                        lstTb47[i].CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                        lstTb47[i].FL_EMITE_BOLETO = ddlTipoDocumento.SelectedValue == "BOL" ? "S" : "N";                        
                        lstTb47[i].IC_SIT_DOC = tb37.IC_SIT_RECDES;
                        lstTb47[i].TP_CLIENTE_DOC = "O";
                        lstTb47[i].TB103_CLIENTE = tb37.TB103_CLIENTE;
                        lstTb47[i].TB56_PLANOCTA = tb37.TB56_PLANOCTA;
                        lstTb47[i].CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                        lstTb47[i].CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                        lstTb47[i].DT_ALT_REGISTRO = DateTime.Now;
                        lstTb47[i].DT_SITU_DOC = DateTime.Now;

                        GestorEntities.SaveOrUpdate(lstTb47[i]);                   
                    }

                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Item adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    else
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
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
                tb37.TB103_CLIENTE.TB905_BAIRROReference.Load();
                tb37.TB103_CLIENTE.TB905_BAIRRO.TB904_CIDADEReference.Load();

                if (tb37.TB099_CENTRO_CUSTO != null)
                    tb37.TB099_CENTRO_CUSTO.TB14_DEPTOReference.Load();

                ddlTipoCliente.SelectedValue = tb37.TB103_CLIENTE.TP_CLIENTE;
                ddlNomeFantasia.SelectedValue = tb37.TB103_CLIENTE.CO_CLIENTE.ToString();
                if (tb37.TB103_CLIENTE.TP_CLIENTE != "F")
                {
                    lblCNPJ.Text = "CNPJ";
                    txtCNPJ.CssClass = "txtCNPJ";
                    lblCNPJ.ToolTip = "Informe o CNPJ";                    
                }
                else
                {
                    lblCNPJ.Text = "CPF";
                    txtCNPJ.CssClass = "txtCPF";
                    lblCNPJ.ToolTip = "Informe o CPF";
                }
                txtCNPJ.Text = tb37.TB103_CLIENTE.CO_CPFCGC_CLI;
                txtLogradouro.Text = tb37.TB103_CLIENTE.DE_END_CLI;
                txtUF.Text = tb37.TB103_CLIENTE.CO_UF_CLI;

                if (tb37.TB103_CLIENTE.TB905_BAIRRO != null)
                {
                    txtCidade.Text = tb37.TB103_CLIENTE.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE;
                    txtBairro.Text = tb37.TB103_CLIENTE.TB905_BAIRRO.NO_BAIRRO;
                }

                txtComplemento.Text = tb37.TB103_CLIENTE.DE_COM_CLI;
                txtCEP.Text = tb37.TB103_CLIENTE.CO_CEP_CLI;
                ddlTipoReceita.SelectedValue = tb37.TB56_PLANOCTA != null ? tb37.TB56_PLANOCTA.TB54_SGRP_CTA.CO_GRUP_CTA.ToString() : "";
                CarregaSubGrupo();
                ddlSubGrupoReceita.SelectedValue = tb37.TB56_PLANOCTA != null ? tb37.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString() : "";
                CarregaConta();
                ddlContaContabil.SelectedValue = tb37.TB56_PLANOCTA != null ? tb37.TB56_PLANOCTA.CO_SEQU_PC.ToString() : "";
                ddlHistLancamento.SelectedValue = tb37.TB39_HISTORICO != null ? tb37.TB39_HISTORICO.CO_HISTORICO.ToString() : "";
                ddlContaContabil.SelectedValue = tb37.CO_CTA_LCX.ToString();
                ddlDepartamento.SelectedValue = tb37.TB099_CENTRO_CUSTO != null ? (tb37.TB099_CENTRO_CUSTO.TB14_DEPTO != null ? tb37.TB099_CENTRO_CUSTO.TB14_DEPTO.CO_DEPTO.ToString() : "") : "";
                CarregaCentroCusto();
                ddlCentroCusto.SelectedValue = tb37.TB099_CENTRO_CUSTO != null ? tb37.TB099_CENTRO_CUSTO.CO_CENT_CUSTO.ToString() : "";
                txtNumContrato.Text = tb37.CO_CON_RECDES;

                int intMaxNumAditivo = (from lTb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                                        where lTb37.CO_EMP == tb37.CO_EMP && lTb37.CO_CON_RECDES == tb37.CO_CON_RECDES
                                        select lTb37).Max(r => r.CO_ADITI_RECDES) + 1;

                txtNumAditivo.Text = intMaxNumAditivo.ToString();
                txtDtCadastro.Text = txtDtStatus.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Documento
        /// </summary>
        private void CarregaTipoDocumento()
        {
            ddlTipoDocumento.DataSource = (from tb086 in TB086_TIPO_DOC.RetornaTodosRegistros()
                                           select new { tb086.SIG_TIPO_DOC, tb086.DES_TIPO_DOC });

            ddlTipoDocumento.DataTextField = "DES_TIPO_DOC";
            ddlTipoDocumento.DataValueField = "SIG_TIPO_DOC";
            ddlTipoDocumento.DataBind();

            ddlTipoDocumento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Nome Fantasia
        /// </summary>
        private void CarregaNomeFantasia()
        {
            ddlNomeFantasia.DataSource = TB103_CLIENTE.RetornaTodosRegistros().OrderBy(c => c.NO_FAN_CLI);

            ddlNomeFantasia.DataTextField = "NO_FAN_CLI";
            ddlNomeFantasia.DataValueField = "CO_CLIENTE";
            ddlNomeFantasia.DataBind();

            ddlNomeFantasia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamento
        /// </summary>
        private void CarregaDepartamento()
        {
            ddlDepartamento.DataSource = from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                         where tb14.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                         select new { tb14.CO_DEPTO, tb14.NO_DEPTO };

            ddlDepartamento.DataTextField = "NO_DEPTO";
            ddlDepartamento.DataValueField = "CO_DEPTO";
            ddlDepartamento.DataBind();

            ddlDepartamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Receita
        /// </summary>
        private void CarregaTipoReceita()
        {
            ddlTipoReceita.DataSource = TB53_GRP_CTA.RetornaTodosRegistros();

            ddlTipoReceita.DataTextField = "DE_GRUP_CTA";
            ddlTipoReceita.DataValueField = "CO_GRUP_CTA";
            ddlTipoReceita.DataBind();

            ddlTipoReceita.SelectedValue = "1";

            ddlTipoReceita.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo de Receitas
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
                                           select new { tb56.CO_CONTA_PC, tb56.DE_CONTA_PC }).OrderBy( p => p.DE_CONTA_PC );

            ddlContaContabil.DataTextField = "DE_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_CONTA_PC";
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

        protected void ddlNomeFantasia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coCliente = ddlNomeFantasia.SelectedValue != "" ? int.Parse(ddlNomeFantasia.SelectedValue) : 0;

            ddlTipoCliente.SelectedValue = txtCNPJ.Text = txtLogradouro.Text = txtUF.Text = 
            txtCidade.Text = txtBairro.Text = txtComplemento.Text = txtCEP.Text = "";

            if (coCliente != 0)
            {
                TB103_CLIENTE tb103 = TB103_CLIENTE.RetornaPelaChavePrimaria(coCliente);

                if (tb103 != null)
                {
                    ddlTipoCliente.SelectedValue = tb103.TP_CLIENTE;
                    txtCNPJ.Text = tb103.CO_CPFCGC_CLI;
                    txtLogradouro.Text = tb103.DE_END_CLI;
                    txtUF.Text = tb103.CO_UF_CLI;

                    tb103.TB905_BAIRROReference.Load();
                    tb103.TB905_BAIRRO.TB904_CIDADEReference.Load();

                    if (tb103.TB905_BAIRRO != null)
                    {
                        txtCidade.Text = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE;
                        txtBairro.Text = tb103.TB905_BAIRRO.NO_BAIRRO;
                    }

                    txtComplemento.Text = tb103.DE_COM_CLI;
                    txtCEP.Text = tb103.CO_CEP_CLI;
                }
            }
        }

        protected void ddlTipoReceita_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
            CarregaConta();
        }

        protected void ddlSubGrupoReceita_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCentroCusto();
        }
    }
}