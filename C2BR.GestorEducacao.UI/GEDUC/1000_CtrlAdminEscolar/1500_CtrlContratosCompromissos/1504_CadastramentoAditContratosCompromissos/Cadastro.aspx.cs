//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: CONTROLE DE CONTRATOS DE COMPROMISSOS
// OBJETIVO: CADASTRAMENTO DE ADITIVOS DE CONTRATOS DE COMPROMISSOS INSTITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1504_CadastramentoAditContratosCompromissos
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
                AuxiliPagina.RedirecionaParaPaginaErro("Selecione um contrato para gerar aditivo.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

            if (!Page.IsPostBack)
            {
                CarregaNomeFantasia();
                CarregaDepartamento();
                CarregaCentroCusto();
                CarregaHistorico();
                CarregaSubGrupo();
                CarregaConta();
                CarregaTipoDocumento();
                CarregaCategorias();
                CarregaSubCategorias();
                CarregaBoleto();
                CarregaAnosDotacaoOrcamentaria();
                CarregaDotacaoOrcamentaria();
                CarregaOrdenadores();
                this.hdfOcorrCadas.Value = "N";
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                TB312_CONTR_COMPR tb312 = RetornaEntidade();

                bool flagTodosStaAberto = true;

                if (tb312 != null)
                {
                    if (ddlTipoReceita.SelectedValue == "C")
                    {
//--------------------> Retorna a lista de Contas lançadas para a receita fixa selecionada
                        List<TB47_CTA_RECEB> lstTb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                                        where lTb47.CO_EMP == tb312.CO_EMP && lTb47.CO_CON_RECFIX == tb312.CO_CONTR_COMPR
                                                        select lTb47).ToList();

//--------------------> Faz a verificação para saber se existe alguma conta com o status diferente de "Em aberto"
                        foreach (TB47_CTA_RECEB tb47 in lstTb47)
                        {
                            if (tb47.IC_SIT_DOC != "A")
                            {
                                flagTodosStaAberto = false;
                                break;
                            }
                        }

//--------------------> Se todas as contas estiverem com o status "Em aberto", exclui as mesmas e depois exclui a receita fixa
                        if (flagTodosStaAberto)
                        {
                            foreach (TB47_CTA_RECEB tb47 in lstTb47)
                            {
                                TB47_CTA_RECEB.Delete(tb47, true);
                            }

                            CurrentPadraoCadastros.CurrentEntity = tb312;
                        }
                        else
                            AuxiliPagina.EnvioMensagemErro(this, "Há Contas cadastradas para a Receita atual com Staus diferente de  \"Aberta\". Não é possível excluir essa Receita.");

                        return;
                    }
                    else if (ddlTipoReceita.SelectedValue == "D")
                    {
                        if (tb312 != null)
                        {
//------------------------> Retorna a lista de Contas lançadas para a Despesa fixa selecionada
                            List<TB38_CTA_PAGAR> lstTb38 = (from lTb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                                            where lTb38.CO_EMP == tb312.CO_EMP && lTb38.NU_DOC == tb312.CO_CONTR_COMPR
                                                            select lTb38).ToList();

//------------------------> Faz a verificação para saber se existe alguma conta com o status diferente de "Em aberto"
                            foreach (TB38_CTA_PAGAR tb38 in lstTb38)
                            {
                                if (tb38.IC_SIT_DOC != "A")
                                {
                                    flagTodosStaAberto = false;
                                    break;
                                }
                            }

//------------------------> Se todas as contas estiverem com o status "Em aberto", exclui as mesmas e depois exclui a Despesa fixa
                            if (flagTodosStaAberto)
                            {
                                foreach (TB38_CTA_PAGAR tb38 in lstTb38)
                                {
                                    TB38_CTA_PAGAR.Delete(tb38, true);
                                }

                                CurrentPadraoCadastros.CurrentEntity = tb312;
                            }
                            else
                                AuxiliPagina.EnvioMensagemErro(this, "Há Contas cadastradas para o Contrato atual com Staus diferente de  \"Em Aberto\". Não é possível excluí-lo.");
                        }

                        return;
                    }

                }
            }

            if (ddlAnoReferDotac.SelectedValue != "")
            {
                if (int.Parse(ddlAnoReferDotac.SelectedValue) != DateTime.Parse(txtDtContrato.Text).Year)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Ano da dotação orçamentária deve ser igual ao ano do contrato.");
                    return;
                }
            }

            if (this.hdfOcorrCadas.Value == "S")
            {
                AuxiliPagina.RedirecionaParaPaginaSucesso("Item já editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }

            if (chkGeraBoleto.Checked)
            {
                if (ddlPadraoBoletBanca.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Padrão de boleto bancário deve ser informado.");
                    return;
                }
            }

            if (Page.IsValid)
            {
                int intRetorno;
                decimal decimalRetorno;
                DateTime dataRetorno;
                TB314_SUB_CATEG_CONTR tb314 = TB314_SUB_CATEG_CONTR.RetornaPelaChavePrimaria(int.Parse(ddlSubCateg.SelectedValue));
                tb314.TB313_CATEG_CONTRReference.Load();

                TB312_CONTR_COMPR tb312_Original = RetornaEntidade();
                tb312_Original.TB000_INSTITUICAOReference.Load();
                tb312_Original.TB103_CLIENTEReference.Load();
                tb312_Original.TB25_EMPRESAReference.Load();

                if (tb312_Original.DT_CONTR_COMPR > DateTime.Parse(txtDtInicioContrato.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data de Início de Contrato deve ser maior que data de assinatura.");
                    return;
                }

                TB312_CONTR_COMPR tb312 = new TB312_CONTR_COMPR();

                tb312.CO_CONTR_COMPR = tb312_Original.CO_CONTR_COMPR;
                tb312.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb312_Original.TB25_EMPRESA.CO_EMP);
                tb312.TB000_INSTITUICAO = tb312_Original.TB000_INSTITUICAO;
                tb312.TB103_CLIENTE = tb312_Original.TB103_CLIENTE;
                tb312.CO_ADITI_CONTR_COMPR = int.Parse(txtNumAditivo.Text);

                tb312.DT_CONTR_COMPR = DateTime.Parse(txtDtContrato.Text);
                tb312.NM_TITUL_CONTR = txtTitulContrat.Text;
                tb312.DE_OBJET_CONTR = txtObjetContrat.Text != "" ? txtObjetContrat.Text : null;
                tb312.VR_PRIME_PARC_CONTR_COMPR = decimal.Parse(txtValorPrimParc.Text);
                tb312.FL_ATUAL_FINAN = chkAtualFinan.Checked ? "S" : "N";
                tb312.TB227_DADOS_BOLETO_BANCARIO = ddlPadraoBoletBanca.SelectedValue != "" ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlPadraoBoletBanca.SelectedValue)) : null;
                tb312.TP_CON_CONTR_COMPR = ddlTipoReceita.SelectedValue;
                tb312.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(int.TryParse(ddlContaContabil.SelectedValue, out intRetorno) ? intRetorno : 0);
                tb312.CO_CTA_LCX = int.TryParse(ddlContaContabil.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
                tb312.TB39_HISTORICO = ddlHistLancamento.SelectedValue != "" ? TB39_HISTORICO.RetornaPelaChavePrimaria(int.Parse(ddlHistLancamento.SelectedValue)) : null;
                tb312.TB099_CENTRO_CUSTO = ddlCentroCusto.SelectedValue != "" ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(int.Parse(ddlCentroCusto.SelectedValue)) : null;
                tb312.NU_PUBLI_CONTR_COMPR = txtNumPublicacao.Text != "" ? txtNumPublicacao.Text : null;
                tb312.DT_PUBLI_CONTR_COMPR = txtDtPublicacao.Text != "" ? DateTime.Parse(txtDtPublicacao.Text) : (DateTime?)null;
                tb312.NU_EMPEN_CONTR_COMPR = txtNumEmpen.Text != "" ? txtNumEmpen.Text : null;
                tb312.DT_EMPEN_CONTR_COMPR = txtDtEmpenho.Text != "" ? DateTime.Parse(txtDtEmpenho.Text) : (DateTime?)null;
                tb312.TB086_TIPO_DOC = ddlTipoDocumento.SelectedValue != "" ? TB086_TIPO_DOC.RetornaTodosRegistros().Where(t => t.SIG_TIPO_DOC == ddlTipoDocumento.SelectedValue).FirstOrDefault() : null;
                tb312.CO_DOC_CONTR_COMPR = txtNumDoc.Text != "" ? txtNumDoc.Text : null;
                tb312.DT_INI_CONTR_COMPR = DateTime.Parse(txtDtInicioContrato.Text);
                tb312.DT_FIM_CONTR_COMPR = DateTime.Parse(txtDtFimContrato.Text);
                tb312.QT_PARC_CONTR_COMPR = int.TryParse(txtQtParcelas.Text, out intRetorno) ? intRetorno : 1;
                tb312.DT_VENC_PRIM_PARC = DateTime.Parse(txtDtPrimeiroVencto.Text);
                tb312.NU_DIA_INTER_CONTR_COMPR = int.TryParse(txtDiasIntervalo.Text, out intRetorno) ? (int?)intRetorno : null;
                tb312.VR_CONTR_COMPR = decimal.Parse(txtValorDocumento.Text);
                tb312.DT_CAN_CONTR_COMPR = DateTime.TryParse(txtDtCancelamento.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb312.VR_MUL_CONTR_COMPR = decimal.TryParse(txtMulta.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb312.CO_FLAG_TP_VALOR_MUL = cbFlagPercentualMulta.Checked ? "P" : "V";
                tb312.VR_JUR_CONTR_COMPR = decimal.TryParse(txtJuros.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb312.CO_FLAG_TP_VALOR_JUR = cbFlagPercentualJuros.Checked ? "P" : "V";
                tb312.VR_DES_CONTR_COMPR = decimal.TryParse(txtDesconto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb312.CO_FLAG_TP_VALOR_DES = cbFlagPercentualDesconto.Checked ? "P" : "V";
                tb312.ID_CATEG_CONTR = int.Parse(ddlCateg.SelectedValue);
                tb312.ID_SUB_CATEG_CONTR = int.Parse(ddlSubCateg.SelectedValue);
                tb312.DE_LOCAL_PUBLIC_CONTR = txtLocalPubli.Text != "" ? txtLocalPubli.Text : null;
                tb312.DE_OBSER_CONTR = txtObserContr.Text != "" ? txtObserContr.Text : null;
                tb312.NU_ELEME_DESPE = txtElemeDespe.Text != "" ? (int?)int.Parse(txtElemeDespe.Text) : null;
                tb312.TB03_COLABOR = ddlOrdenRespo.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlOrdenRespo.SelectedValue)) : null;
                tb312.TB305_DOTAC_ORCAM = ddlDotacOrcam.SelectedValue != "" ? TB305_DOTAC_ORCAM.RetornaPelaChavePrimaria(int.Parse(ddlDotacOrcam.SelectedValue)) : null;
                tb312.DT_CAD_CONTR_COMPR = DateTime.Now;
                tb312.IC_SIT_CONTR_COMPR = ddlStatus.SelectedValue;
                tb312.DT_ALT_REGISTRO = DateTime.Now;

                int intSalvo = TB312_CONTR_COMPR.SaveOrUpdate(tb312, true);

                if ((intSalvo > 0) && (ddlTipoReceita.SelectedValue == "C") && (chkAtualFinan.Checked))
                {
                    var lstTb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   where lTb47.CO_EMP == tb312.CO_EMP && lTb47.CO_CONTR_COMPR == tb312.CO_CONTR_COMPR
                                   && lTb47.CO_ADITI_CONTR_COMPR.Value == tb312.CO_ADITI_CONTR_COMPR
                                   select lTb47).ToList();                    

//----------------> Lança cada parcela no Contas a Receber
                    for (int i = 0; i < tb312.QT_PARC_CONTR_COMPR; i++)
                    {
                        tb312.TB25_EMPRESAReference.Load();
                        tb312.TB000_INSTITUICAOReference.Load();

                        if (lstTb47.Count() < tb312.QT_PARC_CONTR_COMPR)
                            lstTb47.Add(new TB47_CTA_RECEB());

                        double doubleDiasIntervalo;
                        double.TryParse(tb312.NU_DIA_INTER_CONTR_COMPR.ToString(), out doubleDiasIntervalo);
                        DateTime dtVencto = i.Equals(0) ? tb312.DT_VENC_PRIM_PARC : tb312.DT_VENC_PRIM_PARC.AddDays(doubleDiasIntervalo * (i));

                        if (lstTb47[i].CO_CONTR_COMPR == null)
                        {
                            lstTb47[i].TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb312.TB25_EMPRESA.CO_EMP);
                            lstTb47[i].CO_EMP = tb312.CO_EMP;
                            lstTb47[i].NU_DOC = "CT" + dtVencto.ToString("yy") + "." + tb312.CO_CONTR_COMPR.PadLeft(10, '0') + "." + (i + 1).ToString("D2") + "A";
                            lstTb47[i].CO_CONTR_COMPR = tb312.CO_CONTR_COMPR;
                            lstTb47[i].CO_ADITI_CONTR_COMPR = tb312.CO_ADITI_CONTR_COMPR;
                            lstTb47[i].NU_PAR = i + 1;
                            lstTb47[i].QT_PAR = tb312.QT_PARC_CONTR_COMPR;
                            lstTb47[i].DT_CAD_DOC = tb312.DT_CAD_CONTR_COMPR;
                            lstTb47[i].TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb312.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);
                        }

                        var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                    where iTb25.CO_EMP == tb312.TB25_EMPRESA.CO_EMP
                                    select new { iTb25.CO_CTA_CAIXA, iTb25.CO_CTA_BANCO }).FirstOrDefault();

                        lstTb47[i].DE_COM_HIST = null;
                        lstTb47[i].VR_TOT_DOC = tb312.VR_CONTR_COMPR;
                        lstTb47[i].VR_PAR_DOC = i.Equals(0) ? tb312.VR_PRIME_PARC_CONTR_COMPR : (tb312.VR_CONTR_COMPR - tb312.VR_PRIME_PARC_CONTR_COMPR) / (tb312.QT_PARC_CONTR_COMPR - 1);
                        lstTb47[i].DT_VEN_DOC = dtVencto;
                        lstTb47[i].DT_EMISS_DOCTO = DateTime.Parse(txtDtContrato.Text);
                        lstTb47[i].TB227_DADOS_BOLETO_BANCARIO = ddlPadraoBoletBanca.SelectedValue != "" ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlPadraoBoletBanca.SelectedValue)) : null;
                        lstTb47[i].FL_TIPO_COB = "I";
                        lstTb47[i].TB39_HISTORICO = tb312.TB39_HISTORICO;
                        lstTb47[i].TB086_TIPO_DOC = tb312.TB086_TIPO_DOC;
                        lstTb47[i].TB099_CENTRO_CUSTO = tb312.TB099_CENTRO_CUSTO;
                        lstTb47[i].VR_MUL_DOC = tb312.VR_MUL_CONTR_COMPR;
                        lstTb47[i].VR_JUR_DOC = tb312.VR_JUR_CONTR_COMPR;
                        lstTb47[i].VR_DES_DOC = tb312.VR_DES_CONTR_COMPR;
                        lstTb47[i].CO_FLAG_TP_VALOR_MUL = tb312.CO_FLAG_TP_VALOR_MUL;
                        lstTb47[i].CO_FLAG_TP_VALOR_JUR = tb312.CO_FLAG_TP_VALOR_JUR;
                        lstTb47[i].CO_FLAG_TP_VALOR_DES = tb312.CO_FLAG_TP_VALOR_DES;
                        lstTb47[i].CO_FLAG_TP_VALOR_OUT = "V";
                        lstTb47[i].CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                        lstTb47[i].FL_EMITE_BOLETO = ddlTipoDocumento.SelectedValue == "BOL" ? "S" : "N";
                        lstTb47[i].IC_SIT_DOC = tb312.IC_SIT_CONTR_COMPR;
                        lstTb47[i].TP_CLIENTE_DOC = "O";
                        lstTb47[i].TB103_CLIENTE = tb312.TB103_CLIENTE;
                        lstTb47[i].TB56_PLANOCTA = tb312.TB56_PLANOCTA;
                        lstTb47[i].CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                        lstTb47[i].CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                        lstTb47[i].DT_ALT_REGISTRO = DateTime.Now;
                        lstTb47[i].DT_SITU_DOC = DateTime.Now;

                        lstTb47[i].DE_OBS = "(CONTRATO DE RECEITA Nº " + tb312.CO_CONTR_COMPR.PadLeft(10, '0') + " - ADITIVO: " + tb312.CO_ADITI_CONTR_COMPR.ToString("D2") +
                        " - DATA: " + tb312.DT_CONTR_COMPR.ToString("dd/MM/yy") + " - CATEG: " + tb314.TB313_CATEG_CONTR.CO_CATEG_CONTR +
                        " - SUBCAT: " + tb314.CO_SUB_CATEG_CONTR + ")";
                        GestorEntities.SaveOrUpdate(lstTb47[i]);
                    }

                    if (chkGeraBoleto.Checked)
                    {
                        GeraBoleto(tb312.CO_CONTR_COMPR, tb312.CO_ADITI_CONTR_COMPR);

                        AuxiliPagina.EnvioMensagemSucesso(this.Page, "Item editado com sucesso.");

                        this.hdfOcorrCadas.Value = "S";
                        return;
                    }

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
                else if ((intSalvo > 0) && (ddlTipoReceita.SelectedValue == "D") && (chkAtualFinan.Checked))
                {
                    var lstTb38 = (from lTb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                   where lTb38.CO_EMP == tb312.CO_EMP && lTb38.CO_CONTR_COMPR == tb312.CO_CONTR_COMPR
                                   && lTb38.CO_ADITI_CONTR_COMPR.Value == tb312.CO_ADITI_CONTR_COMPR
                                   select lTb38).ToList();

//----------------> Lança cada parcela no Contas a Pagar
                    for (int i = 0; i < tb312.QT_PARC_CONTR_COMPR; i++)
                    {
                        tb312.TB25_EMPRESAReference.Load();
                        tb312.TB000_INSTITUICAOReference.Load();

                        if (lstTb38.Count < i + 1)
                            lstTb38.Add(new TB38_CTA_PAGAR());

                        int diasIntervalo;
                        int.TryParse(tb312.NU_DIA_INTER_CONTR_COMPR.ToString(), out diasIntervalo);

                        if (lstTb38[i].CO_CONTR_COMPR == null)
                        {
                            lstTb38[i].TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb312.TB25_EMPRESA.CO_EMP);
                            lstTb38[i].CO_EMP = tb312.CO_EMP;
                            lstTb38[i].NU_PAR = i + 1;
                            lstTb38[i].QT_PAR = tb312.QT_PARC_CONTR_COMPR;
                            lstTb38[i].TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb312.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);
                            lstTb38[i].NU_DOC = tb312.CO_DOC_CONTR_COMPR;
                            lstTb38[i].DT_CAD_DOC = tb312.DT_CAD_CONTR_COMPR;
                            lstTb38[i].CO_CONTR_COMPR = tb312.CO_CONTR_COMPR;
                            lstTb38[i].CO_ADITI_CONTR_COMPR = tb312.CO_ADITI_CONTR_COMPR;
                        }

                        if (lstTb38[i].TB000_INSTITUICAO == null)
                            lstTb38[i].TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb312.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);
                        if (lstTb38[i].TB25_EMPRESA == null)
                            lstTb38[i].TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb312.TB25_EMPRESA.CO_EMP);

                        lstTb38[i].DE_COM_HIST = null;
                        lstTb38[i].VR_TOT_DOC = tb312.VR_CONTR_COMPR;                        
                        lstTb38[i].VR_PAR_DOC = tb312.VR_CONTR_COMPR / tb312.QT_PARC_CONTR_COMPR;
                        lstTb38[i].DT_VEN_DOC = i.Equals(0) ? tb312.DT_VENC_PRIM_PARC : tb312.DT_VENC_PRIM_PARC.AddDays(diasIntervalo * i);
                        lstTb38[i].DT_EMISS_DOCTO = tb312.DT_INI_CONTR_COMPR;
                        lstTb38[i].TB39_HISTORICO = tb312.TB39_HISTORICO;
                        lstTb38[i].TB086_TIPO_DOC = tb312.TB086_TIPO_DOC;
                        lstTb38[i].TB099_CENTRO_CUSTO = tb312.TB099_CENTRO_CUSTO;
                        lstTb38[i].VR_MUL_DOC = tb312.VR_MUL_CONTR_COMPR;
                        lstTb38[i].VR_JUR_DOC = tb312.VR_JUR_CONTR_COMPR;
                        lstTb38[i].VR_DES_DOC = tb312.VR_DES_CONTR_COMPR;
                        lstTb38[i].CO_FLAG_TP_VALOR_MUL = tb312.CO_FLAG_TP_VALOR_MUL;
                        lstTb38[i].CO_FLAG_TP_VALOR_JUR = tb312.CO_FLAG_TP_VALOR_JUR;
                        lstTb38[i].CO_FLAG_TP_VALOR_DES = tb312.CO_FLAG_TP_VALOR_DES;
                        lstTb38[i].CO_FLAG_TP_VALOR_DES_ANTEC = "V";
                        lstTb38[i].IC_SIT_DOC = tb312.IC_SIT_CONTR_COMPR;
                        lstTb38[i].TB41_FORNEC = tb312.TB41_FORNEC;
                        lstTb38[i].TB56_PLANOCTA = tb312.TB56_PLANOCTA;
                        lstTb38[i].DT_ALT_REGISTRO = DateTime.Now;
                        lstTb38[i].DT_SITU_DOC = DateTime.Now;

                        GestorEntities.SaveOrUpdate(lstTb38[i]);
                    }

                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Item adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    else
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
                else if (intSalvo > 0)
                {
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Item adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    else
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
                else
                {
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao adicionar item!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    else
                        AuxiliPagina.RedirecionaParaPaginaErro("Erro ao editar item!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }
            else
            {
                AuxiliPagina.RedirecionaParaPaginaErro("Erro na página, por favor entre em contato com o suporte!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB312_CONTR_COMPR tb312 = RetornaEntidade();

            if (tb312 != null)
            {
                tb312.TB099_CENTRO_CUSTOReference.Load();
                tb312.TB39_HISTORICOReference.Load();
                tb312.TB56_PLANOCTAReference.Load();
                tb312.TB56_PLANOCTA.TB54_SGRP_CTAReference.Load();
                tb312.TB086_TIPO_DOCReference.Load();
                tb312.TB103_CLIENTE.TB905_BAIRROReference.Load();
                tb312.TB103_CLIENTE.TB905_BAIRRO.TB904_CIDADEReference.Load();
                tb312.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                tb312.TB03_COLABORReference.Load();
                tb312.TB305_DOTAC_ORCAMReference.Load();

                if (tb312.TB099_CENTRO_CUSTO != null)
                    tb312.TB099_CENTRO_CUSTO.TB14_DEPTOReference.Load();

                ddlTipoCliente.SelectedValue = tb312.TB103_CLIENTE.TP_CLIENTE;
                ddlNomeFantasia.SelectedValue = tb312.TB103_CLIENTE.CO_CLIENTE.ToString();
                if (tb312.TB103_CLIENTE.TP_CLIENTE == "F")
                {
                    lblCNPJ.Text = "CPF";
                    txtCNPJ.CssClass = "txtCPF";
                    lblCNPJ.ToolTip = "Informe o CPF";
                }
                else
                {
                    lblCNPJ.Text = "CNPJ";
                    txtCNPJ.CssClass = "txtCNPJ";
                    lblCNPJ.ToolTip = "Informe o CNPJ";
                }
                txtCNPJ.Text = tb312.TB103_CLIENTE.CO_CPFCGC_CLI;
                txtUF.Text = tb312.TB103_CLIENTE.CO_UF_CLI;

                if (tb312.TB103_CLIENTE.TB905_BAIRRO != null)
                {
                    txtCidade.Text = tb312.TB103_CLIENTE.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE;
                }

                ddlTipoReceita.SelectedValue = tb312.TP_CON_CONTR_COMPR;
                CarregaSubGrupo();
                ddlSubGrupoReceita.SelectedValue = tb312.TB56_PLANOCTA != null ? tb312.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString() : "";
                CarregaConta();
                ddlContaContabil.SelectedValue = tb312.TB56_PLANOCTA != null ? tb312.TB56_PLANOCTA.CO_SEQU_PC.ToString() : "";
                ddlHistLancamento.SelectedValue = tb312.TB39_HISTORICO != null ? tb312.TB39_HISTORICO.CO_HISTORICO.ToString() : "";
                ddlDepartamento.SelectedValue = tb312.TB099_CENTRO_CUSTO != null ? (tb312.TB099_CENTRO_CUSTO.TB14_DEPTO != null ? tb312.TB099_CENTRO_CUSTO.TB14_DEPTO.CO_DEPTO.ToString() : "") : "";
                CarregaCentroCusto();
                ddlCentroCusto.SelectedValue = tb312.TB099_CENTRO_CUSTO != null ? tb312.TB099_CENTRO_CUSTO.CO_CENT_CUSTO.ToString() : "";
                ddlCateg.SelectedValue = tb312.ID_CATEG_CONTR.ToString();
                CarregaSubCategorias();
                ddlSubCateg.SelectedValue = tb312.ID_SUB_CATEG_CONTR.ToString();
                txtTitulContrat.Text = tb312.NM_TITUL_CONTR;
                txtObjetContrat.Text = tb312.DE_OBJET_CONTR != null ? tb312.DE_OBJET_CONTR : null;
                txtNumContrato.Text = tb312.CO_CONTR_COMPR;
                txtDtContrato.Text = tb312.DT_CONTR_COMPR.ToString("dd/MM/yyyy");
                txtNumPublicacao.Text = tb312.NU_PUBLI_CONTR_COMPR;
                txtDtPublicacao.Text = tb312.DT_PUBLI_CONTR_COMPR != null ? tb312.DT_PUBLI_CONTR_COMPR.Value.ToString("dd/MM/yyyy") : "";
                txtMulta.Text = tb312.VR_MUL_CONTR_COMPR.ToString();
                txtJuros.Text = tb312.VR_JUR_CONTR_COMPR.ToString();
                txtDesconto.Text = tb312.VR_DES_CONTR_COMPR.ToString();
                cbFlagPercentualMulta.Checked = tb312.CO_FLAG_TP_VALOR_MUL == "P";
                cbFlagPercentualJuros.Checked = tb312.CO_FLAG_TP_VALOR_JUR == "P";
                cbFlagPercentualDesconto.Checked = tb312.CO_FLAG_TP_VALOR_DES == "P";
                txtLocalPubli.Text = tb312.DE_LOCAL_PUBLIC_CONTR != null ? tb312.DE_LOCAL_PUBLIC_CONTR : "";
                txtNumEmpen.Text = tb312.NU_EMPEN_CONTR_COMPR != null ? tb312.NU_EMPEN_CONTR_COMPR : "";
                txtDtEmpenho.Text = tb312.DT_EMPEN_CONTR_COMPR != null ? tb312.DT_EMPEN_CONTR_COMPR.Value.ToString("dd/MM/yyyy") : "";
                if (tb312.TB305_DOTAC_ORCAM != null)
                {
                    ddlAnoReferDotac.SelectedValue = tb312.TB305_DOTAC_ORCAM.CO_ANO_REF.ToString();
                    CarregaDotacaoOrcamentaria();
                    ddlDotacOrcam.SelectedValue = tb312.TB305_DOTAC_ORCAM.ID_DOTAC_ORCAM.ToString();
                }
                txtElemeDespe.Text = tb312.NU_ELEME_DESPE != null ? tb312.NU_ELEME_DESPE.ToString() : "";
                ddlOrdenRespo.SelectedValue = tb312.TB03_COLABOR != null ? tb312.TB03_COLABOR.CO_COL.ToString() : "";

                int intMaxNumAditivo = (from lTb312 in TB312_CONTR_COMPR.RetornaTodosRegistros()
                                        where lTb312.CO_EMP == tb312.CO_EMP && lTb312.CO_CONTR_COMPR == tb312.CO_CONTR_COMPR
                                        select lTb312).Max(r => r.CO_ADITI_CONTR_COMPR) + 1;

                txtNumAditivo.Text = intMaxNumAditivo.ToString();
                txtDtCadastro.Text = txtDtStatus.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB312_CONTR_COMPR</returns>
        private TB312_CONTR_COMPR RetornaEntidade()
        {
            return TB312_CONTR_COMPR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(Resources.QueryStrings.CoEmp),
                QueryStringAuxili.RetornaQueryStringPelaChave("con"),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("adi"));
        }

        /// <summary>
        /// Método para geração do boleto
        /// </summary>
        protected void GeraBoleto(string CO_CONTR_COMPR, int CO_ADITI_CONTR_COMPR)
        {
//--------> Instancia um novo conjunto de dados de boleto na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            var lstTb47 = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb47.CO_EMP equals tb25.CO_EMP
                           join lTb103 in TB103_CLIENTE.RetornaTodosRegistros() on tb47.TB103_CLIENTE.CO_CLIENTE equals lTb103.CO_CLIENTE
                           where tb47.CO_CONTR_COMPR == CO_CONTR_COMPR && tb47.CO_ADITI_CONTR_COMPR == CO_ADITI_CONTR_COMPR
                           select new
                           {
                               tb47,
                               tb25.CO_CPFCGC_EMP,
                               tb25.NO_RAZSOC_EMP,
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
                           }).ToList();

            string strInstruBoleto = "";

            int iGrdNeg = 1;
//--------> Varre os títulos do contrato informado
            foreach (var iTb47 in lstTb47)
            {
                strInstruBoleto = "";

                iTb47.tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                iTb47.tb47.TB103_CLIENTEReference.Load();

                if (iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM != null)
                {
                    InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                    //------------> Informações do Boleto
                    boleto.Carteira = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                    boleto.CodigoBanco = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;
                    boleto.NossoNumero = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    boleto.NumeroDocumento = iTb47.tb47.NU_DOC + "-" + iTb47.tb47.NU_PAR;
                    boleto.Valor = iTb47.tb47.VR_PAR_DOC; //valor da parcela do documento
                    boleto.Vencimento = iTb47.tb47.DT_VEN_DOC;

                    //------------> Informações do Cedente
                    boleto.NumeroConvenio = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                    boleto.Agencia = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                        iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                    boleto.CodigoCedente = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                    boleto.Conta = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                    boleto.CpfCnpjCedente = iTb47.CO_CPFCGC_EMP;
                    boleto.NomeCedente = iTb47.NO_RAZSOC_EMP;

                    if (iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                    {
                        var desc = boleto.Valor - iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                        strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                    }

                    //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                    if (iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                        strInstruBoleto = iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                    if (iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                        strInstruBoleto = strInstruBoleto + iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                    if (iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                        strInstruBoleto = strInstruBoleto + iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                    boleto.Instrucoes = strInstruBoleto;

                    //------------> Chave do Título do Contas a Receber
                    boleto.CO_EMP = iTb47.tb47.CO_EMP;
                    boleto.NU_DOC = iTb47.tb47.NU_DOC;
                    boleto.NU_PAR = iTb47.tb47.NU_PAR;
                    boleto.DT_CAD_DOC = iTb47.tb47.DT_CAD_DOC;

                    string multaMoraDesc = "";

                    //------------> Informações da Multa
                    multaMoraDesc += iTb47.tb47.VR_MUL_DOC != null && iTb47.tb47.VR_MUL_DOC.Value != 0 ?
                        (iTb47.tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + iTb47.tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
                        (boleto.Valor * (decimal)iTb47.tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + iTb47.tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

                    //------------> Informações da Mora
                    multaMoraDesc += iTb47.tb47.VR_JUR_DOC != null && iTb47.tb47.VR_JUR_DOC.Value != 0 ?
                         (iTb47.tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + iTb47.tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
                         (boleto.Valor * (decimal)iTb47.tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
                            iTb47.tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

                    //------------> Informações do desconto
                    multaMoraDesc += iTb47.tb47.VR_DES_DOC != null && iTb47.tb47.VR_DES_DOC.Value != 0 ?
                         (iTb47.tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + iTb47.tb47.VR_DES_DOC.Value.ToString("0.00") + "% (R$ " +
                         (boleto.Valor * (decimal)iTb47.tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
                            iTb47.tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

                    //------------> Faz a adição de instruções ao Boleto
                    boleto.Instrucoes += "(*) " + multaMoraDesc + "<br>";

                    boleto.Instrucoes += "</br>" + "(" + iTb47.NOME + ")";

                    boleto.Instrucoes += "</br>" + "(Contrato: " + (iTb47.tb47.CO_CONTR_COMPR != null ? iTb47.tb47.CO_CONTR_COMPR : "XXXXX") +
                        " - Aditivo: " + (iTb47.tb47.CO_ADITI_CONTR_COMPR != null ? iTb47.tb47.CO_ADITI_CONTR_COMPR.Value.ToString("00") : "XX") +
                        " - Parcela: " + iTb47.tb47.NU_PAR.ToString("00") + ")";

                    //------------> Informações do Sacado
                    boleto.BairroSacado = iTb47.BAIRRO;
                    boleto.CepSacado = iTb47.CEP;
                    boleto.CidadeSacado = iTb47.CIDADE;
                    boleto.CpfCnpjSacado = iTb47.CPFCNPJ;
                    boleto.EnderecoSacado = iTb47.ENDERECO + " " + iTb47.NUMERO + " " + iTb47.COMPL;
                    boleto.NomeSacado = iTb47.NOME;
                    boleto.UfSacado = iTb47.UF;

                    //------------> Adiciona o título atual na Sessão
                    ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                    if ((iGrdNeg != lstTb47.Count) && (lstTb47.Count > 1))
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(iTb47.tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }

                    iGrdNeg++;
                }
            }

//--------> Gera e exibe os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
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
            ddlNomeFantasia.DataSource = TB103_CLIENTE.RetornaTodosRegistros().OrderBy( c => c.NO_FAN_CLI );

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
        /// Método que carrega o dropdown de SubGrupo de Receitas
        /// </summary>
        private void CarregaSubGrupo()
        {
            ddlSubGrupoReceita.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                                             where tb54.TB53_GRP_CTA.TP_GRUP_CTA == ddlTipoReceita.SelectedValue
                                             select new { tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA });

            ddlSubGrupoReceita.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupoReceita.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupoReceita.DataBind();

            ddlSubGrupoReceita.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Contas Contábil
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
        /// Método que carrega o dropdown de Categorias
        /// </summary>
        private void CarregaCategorias()
        {
            ddlCateg.DataSource = (from tb313 in TB313_CATEG_CONTR.RetornaTodosRegistros()
                                   where tb313.CO_SITUACAO == "A"
                                   select new { tb313.ID_CATEG_CONTR, tb313.NM_CATEG_CONTR }).OrderBy(g => g.NM_CATEG_CONTR);

            ddlCateg.DataValueField = "ID_CATEG_CONTR";
            ddlCateg.DataTextField = "NM_CATEG_CONTR";
            ddlCateg.DataBind();

            ddlCateg.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubCategorias
        /// </summary>
        private void CarregaSubCategorias()
        {
            int idCateg = ddlCateg.SelectedValue != "" ? int.Parse(ddlCateg.SelectedValue) : 0;

            ddlSubCateg.DataSource = (from tb314 in TB314_SUB_CATEG_CONTR.RetornaTodosRegistros()
                                      where tb314.TB313_CATEG_CONTR.ID_CATEG_CONTR == idCateg && tb314.CO_SITUACAO == "A"
                                      select new { tb314.ID_SUB_CATEG_CONTR, tb314.NM_SUB_CATEG_CONTR }).OrderBy(g => g.NM_SUB_CATEG_CONTR);

            ddlSubCateg.DataValueField = "ID_SUB_CATEG_CONTR";
            ddlSubCateg.DataTextField = "NM_SUB_CATEG_CONTR";
            ddlSubCateg.DataBind();

            ddlSubCateg.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoleto()
        {
            AuxiliCarregamentos.CarregaBoletos(ddlPadraoBoletBanca, LoginAuxili.CO_EMP, "D", 0, 0, true);
        }

        /// <summary>
        /// Método que carrega dropdown de Ano de Referência da Dotaçao Orçamentária
        /// </summary>
        private void CarregaAnosDotacaoOrcamentaria()
        {
            ddlAnoReferDotac.DataSource = (from tb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                                           select new { tb305.CO_ANO_REF }).Distinct();

            ddlAnoReferDotac.DataTextField = "CO_ANO_REF";
            ddlAnoReferDotac.DataValueField = "CO_ANO_REF";
            ddlAnoReferDotac.DataBind();

            ddlAnoReferDotac.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega dropdown de Dotaçao Orçamentária
        /// </summary>
        private void CarregaDotacaoOrcamentaria()
        {
            int anoRefer = ddlAnoReferDotac.SelectedValue != "" ? int.Parse(ddlAnoReferDotac.SelectedValue) : 0;

            var resultado = (from tb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                             where tb305.CO_ANO_REF == anoRefer
                             select new
                             {
                                 tb305.ID_DOTAC_ORCAM,
                                 tb305.CO_ANO_REF,
                                 tb305.TB261_SUBGRUPO.CO_SUBGRUPO,
                                 tb305.TB261_SUBGRUPO.TB260_GRUPO.CO_GRUPO,
                                 tb305.CO_DOTAC_ORCAM
                             }).ToList();

            ddlDotacOrcam.DataSource = (from result in resultado
                                        select new
                                        {
                                            result.ID_DOTAC_ORCAM,
                                            CO_DOTAC_ORCAM = string.Format("{0}.{1}.{2}.{3}",
                                            result.CO_ANO_REF.ToString("0000"), result.CO_GRUPO,
                                            result.CO_SUBGRUPO, result.CO_DOTAC_ORCAM.ToString("000"))
                                        }).OrderBy(r => r.CO_DOTAC_ORCAM);

            ddlDotacOrcam.DataTextField = "CO_DOTAC_ORCAM";
            ddlDotacOrcam.DataValueField = "ID_DOTAC_ORCAM";
            ddlDotacOrcam.DataBind();

            ddlDotacOrcam.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Ordenadores
        /// </summary>
        private void CarregaOrdenadores()
        {
            ddlOrdenRespo.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                        where tb03.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                                        select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(g => g.NO_COL);

            ddlOrdenRespo.DataValueField = "CO_COL";
            ddlOrdenRespo.DataTextField = "NO_COL";
            ddlOrdenRespo.DataBind();

            ddlOrdenRespo.Items.Insert(0, new ListItem("Selecione", ""));
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
        /*
        protected void ddlNomeFantasia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coCliente = ddlNomeFantasia.SelectedValue != "" ? int.Parse(ddlNomeFantasia.SelectedValue) : 0;

            ddlTipoCliente.SelectedValue = txtUF.Text = txtCidade.Text = "";

            if (coCliente != 0)
            {
                TB103_CLIENTE tb103 = TB103_CLIENTE.RetornaPelaChavePrimaria(coCliente);

                if (tb103 != null)
                {
                    ddlTipoCliente.SelectedValue = tb103.TP_CLIENTE;
                    if (tb103.TP_CLIENTE == "F")
                    {
                        lblCNPJ.Text = "CPF";
                        txtCNPJ.CssClass = "txtCPF";
                        lblCNPJ.ToolTip = "Informe o CPF";
                    }
                    else
                    {
                        lblCNPJ.Text = "CNPJ";
                        txtCNPJ.CssClass = "txtCNPJ";
                        lblCNPJ.ToolTip = "Informe o CNPJ";
                    }

                    txtCNPJ.Text = tb103.CO_CPFCGC_CLI;
                    txtUF.Text = tb103.CO_UF_CLI;

                    tb103.TB905_BAIRROReference.Load();
                    tb103.TB905_BAIRRO.TB904_CIDADEReference.Load();

                    if (tb103.TB905_BAIRRO != null)
                    {
                        txtCidade.Text = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE;
                    }
                }
            }
        }
        */
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

        protected void ddlCateg_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubCategorias();
        }

        protected void ddlAnoReferDotac_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDotacaoOrcamentaria();
        }

        protected void chkGeraBoleto_CheckedChanged(object sender, EventArgs e)
        {
            ddlPadraoBoletBanca.Enabled = chkGeraBoleto.Checked;
            if (!chkGeraBoleto.Checked)
            {
                ddlPadraoBoletBanca.SelectedValue = "";
            }
        }
    }
}