//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//----------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//----------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR          | DESCRIÇÃO RESUMIDA
// -----------+-------------------------------+-------------------------------------
// 05/07/2016 | Tayguara Acioli  TA.05/07/2016| Adicionei a pop up de registro de ocorrências, que fica na master PadraoCadastros.Master.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data.Objects;
using Resources;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8401_Contrato;

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5101_Orcamento
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                txtIniPeriAtend.Text = txtFimPeriAtend.Text = data.ToShortDateString();

                CarregarAtendimentos();
                CarregaBoletos();
                CarregaTiposContrato();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            Persistencias();
        }

        private void Persistencias()
        {
            try
            {
                #region Validação

                if (Session["Atendimentos"] != null && ((List<int>)Session["Atendimentos"]).Count == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Ocorreu um erro por favor tente novamente, caso persista informe o suporte técnico.");
                    return;
                }

                if (grdProcedOrcam.Rows.Count == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não existem procedimentos para efetuar o faturamento.");
                    return;
                }
                else
                {
                    var val = false;

                    foreach (GridViewRow linha in grdProcedOrcam.Rows)
                    {
                        var chk = (CheckBox)linha.FindControl("chkSelectProc");

                        if (chk.Checked)
                        {
                            val = true;
                            break;
                        }
                    }
                    
                    if (!val)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não existem procedimentos SELECIONADOS para efetuar o faturamento.");
                        return;
                    }
                }

                if (String.IsNullOrEmpty(txtVlLiqui.Text) || decimal.Parse(txtVlLiqui.Text) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel finalizar o faturamento com o valor total liquido como ZERO ou em branco.");
                    return;
                }

                #endregion

                #region Atualiza o financeiro

                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                var atends = (List<int>)Session["Atendimentos"];

                var numContAux = !String.IsNullOrEmpty(tb25.NU_ULTIMO_CONTRATO) ? int.Parse(tb25.NU_ULTIMO_CONTRATO) : 0;
                numContAux++;
                var numContrt = numContAux.ToString("D7");

                TBS404_CONTRATOS tbs404 = new TBS404_CONTRATOS();

                var parcelasSalvas = false;
                foreach (var atend in atends)
                {
                    TBS390_ATEND_AGEND tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(atend);
                    tbs390.TBS174_AGEND_HORARReference.Load();

                    TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs390.TBS174_AGEND_HORAR.CO_ALU.Value);
                    tb07.TB108_RESPONSAVELReference.Load();

                    if (!parcelasSalvas)
                    {
                        tbs404.NU_CONTRATO = numContrt;
                        tbs404.TB07_ALUNO = tb07;
                        tbs404.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL;
                        tbs404.QT_PAR = grdNegociacao.Rows.Count;
                        tbs404.VL_DSCTO_FATU = decimal.Parse(txtVlDescto.Text);
                        tbs404.VL_TOTAL_FATU = decimal.Parse(txtVlTotal.Text);

                        tbs404.DT_CADAS = DateTime.Now;
                        tbs404.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs404.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs404.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs404.IP_CADAS = Request.UserHostAddress;

                        foreach (GridViewRow li in grdNegociacao.Rows)
                        {
                            TBS47_CTA_RECEB tbs47 = new TBS47_CTA_RECEB();

                            tbs47.NU_CONTRATO = numContrt;
                            tbs47.TBS390_ATEND_AGEND = tbs390;
                            tbs47.CO_ALU = tb07.CO_ALU;
                            tbs47.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL;
                            tbs47.TB25_EMPRESA = tb25;
                            tbs47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                            tbs47.NU_DOC = li.Cells[0].Text;
                            tbs47.NU_PAR = int.Parse(li.Cells[1].Text);
                            tbs47.QT_PAR = grdNegociacao.Rows.Count;
                            tbs47.DT_CAD_DOC =
                            tbs47.DT_EMISS_DOCTO = DateTime.Now;
                            tbs47.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs47.DE_COM_HIST = "CONTRATO NÚMERO " + tbs47.NU_CONTRATO;
                            tbs47.DT_VEN_DOC = DateTime.Parse(li.Cells[2].Text);
                            tbs47.VL_PAR_DOC = decimal.Parse(li.Cells[3].Text);
                            tbs47.VL_DES_DOC = decimal.Parse(li.Cells[4].Text);
                            tbs47.VL_MUL_DOC = decimal.Parse(li.Cells[6].Text);
                            tbs47.VL_JUR_DOC = decimal.Parse(string.Format("{0:0.0000}", decimal.Parse(li.Cells[7].Text)));
                            tbs47.VL_TOT_DOC = decimal.Parse(txtVlTotal.Text);
                            tbs47.VL_LIQ_DOC = decimal.Parse(txtVlLiqui.Text);

                            if (chkTaxaContrato.Checked && !String.IsNullOrEmpty(txtTaxaContrato.Text))
                                tbs47.VL_TAX_CONTRAT = decimal.Parse(txtTaxaContrato.Text);

                            tbs47.CO_AGRUP_RECDESP = TB83_PARAMETRO.RetornaPelaChavePrimaria(tb25.CO_EMP).CO_AGRUP_REC;

                            tbs47.FL_EMITE_BOLETO = "S"; //chkBoleto.Checked ? "S" : "N";
                            tbs47.FL_TIPO_PREV_RECEB = "B";
                            //-----------------> Salvando o tipo de documento "Boleto Bancário"
                            tbs47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);

                            if (drpBoleto.SelectedValue != "")
                                tbs47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(drpBoleto.SelectedValue));

                            tbs47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                            tbs47.CO_FLAG_TP_VALOR_MUL = "P";
                            tbs47.CO_FLAG_TP_VALOR_JUR = "V";
                            tbs47.CO_FLAG_TP_VALOR_DES = "V";
                            tbs47.CO_FLAG_TP_VALOR_OUT = "V";

                            tbs47.IC_SIT_DOC = "A";
                            tbs47.TP_CLIENTE_DOC = "A";

                            tbs47.DE_OBS = "PAGAMENTO DE PROCEDIMENTOS";

                            tbs47.DT_SITU_DOC = DateTime.Now;
                            tbs47.DT_ALT_REGISTRO = DateTime.Now;

                            TBS47_CTA_RECEB.SaveOrUpdate(tbs47);
                        }

                        parcelasSalvas = true;
                    }

                    tbs390.NU_CONTRATO = numContrt;
                    tbs390.VL_DSCTO_FATU = decimal.Parse(txtVlDescto.Text);
                    tbs390.VL_TOTAL_FATU = decimal.Parse(txtVlTotal.Text);
                    tbs390.FL_SITU_FATU = "F";//Faturado

                    TBS390_ATEND_AGEND.SaveOrUpdate(tbs390);
                }

                tb25.NU_ULTIMO_CONTRATO = numContrt;
                TB25_EMPRESA.SaveOrUpdate(tb25);

                #endregion

                #region Salvar Desconto(s) da(s) Parcela(s)

                foreach (GridViewRow i in grdProcedOrcam.Rows)
                {
                    var chk = (CheckBox)i.FindControl("chkSelectProc");
                    var idAtendOrc = ((HiddenField)i.FindControl("hidIdItemOrc")).Value;
                    var txtDesc = (TextBox)i.FindControl("txtVlDescProcedOrc");

                    TBS396_ATEND_ORCAM tbs396 = TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(int.Parse(idAtendOrc));

                    if (chk.Checked)
                    {
                        var hidIndc = (HiddenField)grdIndicacao.Rows[i.RowIndex].FindControl("hidIndicador");

                        if (!String.IsNullOrEmpty(hidIndc.Value))
                        {
                            tbs396.CO_COL_INDICACAO = int.Parse(hidIndc.Value);
                            tbs396.CO_EMP_INDICACAO = TB03_COLABOR.RetornaPeloCoCol(int.Parse(hidIndc.Value)).CO_EMP;
                        }

                        if (!String.IsNullOrEmpty(txtDesc.Text))
                            tbs396.VL_DSCTO_PROC = decimal.Parse(txtDesc.Text);

                        tbs404.TBS396_ATEND_ORCAM.Add(tbs396);

                        tbs396.FL_FATURADO = "S";
                        tbs396.DT_FATU = DateTime.Now;
                        tbs396.CO_COL_FATU = LoginAuxili.CO_COL;
                    }
                    else
                        tbs396.FL_FATURADO = "N";

                    chk.Enabled =
                    txtDesc.Enabled = false;

                    TBS396_ATEND_ORCAM.SaveOrUpdate(tbs396);
                }

                DesabilitarCampos(false);
                hidFinalizado.Value = "S";

                TBS404_CONTRATOS.SaveOrUpdate(tbs404);

                #endregion

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Atendimento realizado com sucesso!");
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar! Entre em contato com o suporte! Erro: " + e.Message);
            }
        }

        #endregion

        #region Carregamentos

        private void CarregaBoletos()
        {
            var result = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                          select new { tb227.ID_BOLETO, tb227.TB224_CONTA_CORRENTE }).ToList();

            var res = (from tb227 in result
                       join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on tb227.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                       where tb225.CO_EMP == LoginAuxili.CO_EMP && tb227.TB224_CONTA_CORRENTE.CO_AGENCIA == tb225.CO_AGENCIA
                       && tb225.IDEBANCO == tb227.TB224_CONTA_CORRENTE.IDEBANCO
                       select new
                       {
                           tb227.ID_BOLETO,
                           DESCRICAO = string.Format("BCO {0} - AGE {1} - CTA {2}", tb227.TB224_CONTA_CORRENTE.IDEBANCO,
                           tb227.TB224_CONTA_CORRENTE.CO_AGENCIA, tb227.TB224_CONTA_CORRENTE.CO_CONTA)
                       }).OrderBy(b => b.DESCRICAO);

            drpBoleto.DataSource = res;
            drpBoleto.DataValueField = "ID_BOLETO";
            drpBoleto.DataTextField = "DESCRICAO";
            drpBoleto.DataBind();
            drpBoleto.Items.Insert(0, new ListItem("Nenhum", ""));

            drpBoletoPopUp.DataSource = res;
            drpBoletoPopUp.DataValueField = "ID_BOLETO";
            drpBoletoPopUp.DataTextField = "DESCRICAO";
            drpBoletoPopUp.DataBind();
            drpBoletoPopUp.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaTiposContrato()
        {
            drpTipContrato.DataSource = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                                         where tb009.TP_DOCUM == "CO"
                                         select new { tb009.ID_DOCUM, tb009.NM_DOCUM }).ToList();

            drpTipContrato.DataTextField = "NM_DOCUM";
            drpTipContrato.DataValueField = "ID_DOCUM";
            drpTipContrato.DataBind();

            drpTipContrato.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarIndicadores(DropDownList drp)
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(drp, LoginAuxili.CO_EMP, false, "0", true, 0, false);
        }

        /// <summary>
        /// Carrega a lista de agendamentos
        /// </summary>
        private void CarregarAtendimentos()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtIniPeriAtend.Text) ? DateTime.Parse(txtIniPeriAtend.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtFimPeriAtend.Text) ? DateTime.Parse(txtFimPeriAtend.Text) : DateTime.Now);
            
            var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                       where ((EntityFunctions.TruncateTime(tbs390.DT_REALI) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs390.DT_REALI) <= EntityFunctions.TruncateTime(dtFim)))
                       && (!string.IsNullOrEmpty(txtNomePacAtend.Text) ? tb07.NO_ALU.Contains(txtNomePacAtend.Text) : 0 == 0)
                       && tbs390.FL_SITU_FATU == "A"
                       select new saidaPacientes
                       {
                           ID_ATEND = tbs390.ID_ATEND_AGEND,
                           RAP = tbs390.NU_REGIS,

                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           PACIENTE_R = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           DT_NASC_PAC = tb07.DT_NASC_ALU,
                           SX = tb07.CO_SEXO_ALU,

                           PROFISSIONAL = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL
                       }).ToList();

            res = res.OrderByDescending(w => w.DT).ThenByDescending(w => w.HR).ThenBy(w => w.PACIENTE_R).ToList();

            grdPacientes.DataSource = res;
            grdPacientes.DataBind();
        }

        public class saidaPacientes
        {
            public int ID_ATEND { get; set; }
            public string RAP { get; set; }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DTHR
            {
                get
                {
                    return this.DT.ToString("dd/MM/yy") + " - " + this.HR;
                }
            }

            //Dados do Paciente
            public string PACIENTE_R { get; set; }
            public int NU_NIRE { get; set; }
            public string PACIENTE
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.PACIENTE_R);
                }
            }
            public DateTime? DT_NASC_PAC { get; set; }
            public string IDADE
            {
                get
                {
                    return AuxiliFormatoExibicao.FormataDataNascimento(this.DT_NASC_PAC, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto);
                }
            }
            public string SX { get; set; }

            private string PROFISSIONAL_;
            public string PROFISSIONAL {
                get
                {
                    return PROFISSIONAL_;
                }
                set
                {
                    PROFISSIONAL_ = value;
                }
            }
        }

        protected void carregaGridOrcamento()
        {
            if (Session["Atendimentos"] != null && ((List<int>)Session["Atendimentos"]).Count > 0)
            {
                var atends = (List<int>)Session["Atendimentos"];

                var res = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                           join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                           where atends.Contains(tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND)
                           && tbs396.CO_SITU.Equals("S")
                           select new
                           {
                               tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND,
                               tbs396.ID_ATEND_ORCAM,
                               tbs396.QT_PROC,
                               tbs396.VL_PROC,
                               tbs396.DE_OBSER,
                               VL_DSCTO_PROC = tbs396.VL_DSCTO_PROC != null ? tbs396.VL_DSCTO_PROC : 0,
                               tbs356.NM_PROC_MEDI,
                               tbs356.CO_PROC_MEDI,
                               TOTAL = tbs396.VL_PROC - (tbs396.VL_DSCTO_PROC != null ? tbs396.VL_DSCTO_PROC : 0)
                           }).ToList();

                if (res != null && res.Count > 0)
                {
                    var obs = "";
                    var idAux = 0;

                    foreach (var r in res)
                        if (idAux != r.ID_ATEND_AGEND)
                        {
                            idAux = r.ID_ATEND_AGEND;
                            if (!String.IsNullOrEmpty(r.DE_OBSER))
                                obs += r.DE_OBSER + (atends.Count > 1 ? ";" : "");
                        }

                    txtObsOrcam.Text = obs;
                }

                grdIndicacao.DataSource =
                grdProcedOrcam.DataSource = res;
                grdIndicacao.DataBind();
                grdProcedOrcam.DataBind();

                lnkbIndicacao.Enabled = res.Count > 0;
            }
        }

        protected void AtualizarDadosOrcamento(object sender = null, EventArgs e = null)
        {
            if (grdProcedOrcam.Rows.Count != 0)
            {
                decimal vlProc = 0, vlDesc = 0, vlTotal = 0;

                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    var chk = (CheckBox)linha.FindControl("chkSelectProc");

                    if (chk.Checked)
                    {
                        var txtDesc = (TextBox)linha.FindControl("txtVlDescProcedOrc");
                        var lblValorTotal = (Label)linha.FindControl("lblValorTotal");
                        string vlProcOrc = ((HiddenField)linha.FindControl("hidVlProcOrc")).Value;

                        vlProc += (!string.IsNullOrEmpty(vlProcOrc) ? decimal.Parse(vlProcOrc) : 0);
                        vlDesc += (!string.IsNullOrEmpty(txtDesc.Text) ? decimal.Parse(txtDesc.Text) : 0);
                        vlTotal += (!string.IsNullOrEmpty(lblValorTotal.Text) ? decimal.Parse(lblValorTotal.Text) : 0);
                    }
                    else
                    {
                        var chkTodos = (CheckBox)linha.FindControl("chkSelectProc");
                        chkTodos.Checked = false;
                    }
                }

                txtVlTotal.Text = txtVlTotalProcs.Text = vlProc.ToString("N2");
                txtVlDescto.Text = txtVlDescProcs.Text = vlDesc.ToString("N2");
                txtVlLiqui.Text = txtVlLiqdProcs.Text = vlTotal.ToString("N2");

                MontaGridNegociacao();
            }
        }

        protected void MontaGridNegociacao(object sender = null, EventArgs e = null)
        {
            if (grdProcedOrcam.Rows.Count != 0)
            {
                //Quantidade de parcelas não pode ser em branco ou zero
                if (chkNumParcelas.Checked && (String.IsNullOrEmpty(txtNumParcelas.Text) || int.Parse(txtNumParcelas.Text) == 0))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas não pode ser zero ou em branco");
                    txtNumParcelas.Focus();
                    return;
                }

                //Data da primeira não pode ser menor do que hoje
                if (chkAlterPrimParcela.Checked && !String.IsNullOrEmpty(txtDtPrimParcela.Text) && DateTime.Parse(txtDtPrimParcela.Text) < DateTime.Now)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A data da primeira parcela não pode ser menor do que hoje");
                    txtDtPrimParcela.Focus();
                    return;
                }

                //Valor da primeira não pode ser zero
                if (chkAlterPrimParcela.Checked && !String.IsNullOrEmpty(txtValorPrimParce.Text) && decimal.Parse(txtValorPrimParce.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O valor da primeira parcela não pode ser zero");
                    txtValorPrimParce.Focus();
                    return;
                }

                //Melhor dia da parcela não pode ser em branco ou zero
                if (chkDiaParcela.Checked && (String.IsNullOrEmpty(txtDiaParcela.Text) || int.Parse(txtDiaParcela.Text) == 0))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O dia das parcelas não pode ser zero ou em branco");
                    txtDiaParcela.Focus();
                    return;
                }

                var vlTotalProcs = !String.IsNullOrEmpty(txtVlTotalProcs.Text) ? decimal.Parse(txtVlTotalProcs.Text) : 0;
                var vlDescProcs = !String.IsNullOrEmpty(txtVlDescProcs.Text) ? decimal.Parse(txtVlDescProcs.Text) : 0;
                var vlLiquidProcs = !String.IsNullOrEmpty(txtVlLiqdProcs.Text) ? decimal.Parse(txtVlLiqdProcs.Text) : 0;

                var qtdParcelas = !String.IsNullOrEmpty(txtNumParcelas.Text) ? int.Parse(txtNumParcelas.Text) : 1;

                decimal vlTotalDescEspec = 0;
                int qtdMeses = 0, mesInicial = 0;
                if (!String.IsNullOrEmpty(drpTipoDesctoOrcam.SelectedValue))
                {
                    vlTotalDescEspec = !String.IsNullOrEmpty(txtDesctoOrcam.Text) ? decimal.Parse(txtDesctoOrcam.Text) : 0;
                    decimal vlDescFinal = 0;

                    if (drpTipoDesctoOrcam.SelectedValue == "T")
                    {
                        if (chkTipoDesctoOrcam.Checked)
                            vlTotalDescEspec = ((vlTotalDescEspec / 100) * vlLiquidProcs);

                        vlDescProcs += vlTotalDescEspec;

                        vlDescFinal = vlDescProcs;
                    }
                    else if (drpTipoDesctoOrcam.SelectedValue == "M")
                    {
                        qtdMeses = !String.IsNullOrEmpty(txtQtdeMesesDesctoOrcam.Text) ? int.Parse(txtQtdeMesesDesctoOrcam.Text) : 1;
                        mesInicial = !String.IsNullOrEmpty(txtMesIniDesconto.Text) ? int.Parse(txtMesIniDesconto.Text) : 0;

                        vlDescFinal = vlDescProcs + (vlTotalDescEspec * qtdMeses);
                    }

                    txtVlDescto.Text = vlDescFinal.ToString("N2");
                    txtVlLiqui.Text = (vlTotalProcs - vlDescFinal).ToString("N2");
                }

                var Parcelas = new List<Negociacao>();

                var vlParcelaReal = vlTotalProcs / qtdParcelas;
                var vlDescParcela = vlDescProcs / qtdParcelas;
                var dtParcela = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, (!String.IsNullOrEmpty(txtDiaParcela.Text) ? int.Parse(txtDiaParcela.Text) : 5));

                var vlPrimParcela = vlParcelaReal;
                var vlDescPrimParcela = vlDescParcela;
                var dtPrimParcela = chkAlterPrimParcela.Checked && !String.IsNullOrEmpty(txtDtPrimParcela.Text) ? DateTime.Parse(txtDtPrimParcela.Text) : dtParcela;

                if (chkAlterPrimParcela.Checked && qtdParcelas > 1)
                {
                    if (!String.IsNullOrEmpty(txtValorPrimParce.Text))
                        vlPrimParcela = decimal.Parse(txtValorPrimParce.Text);

                    vlParcelaReal = (vlTotalProcs - vlPrimParcela) / (qtdParcelas - 1);
                    vlDescPrimParcela = 0;
                    vlDescParcela = vlDescProcs / (qtdParcelas - 1);
                }

                var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                var numContrt = int.Parse(TB25_EMPRESA.RetornaUltimoNumContrato(LoginAuxili.CO_EMP));
                numContrt++;

                for (int i = 0; i < qtdParcelas; i++)
                {
                    var parcela = new Negociacao();
                    var vlDescParc = i == 0 ? vlDescPrimParcela : vlDescParcela;

                    parcela.numDoc = "CT" + (i == 0 ? dtPrimParcela : dtParcela.AddMonths(i)).ToString("yy") + "." + numContrt.ToString("D7") + "." + (i + 1).ToString("D2");
                    parcela.numParcela = i + 1;
                    parcela.dtVencimento = i == 0 ? dtPrimParcela : dtParcela.AddMonths(i); 
                    parcela.vlParcela = i == 0 ? vlPrimParcela : vlParcelaReal;
                    parcela.vlDesconto = mesInicial == 0 && qtdMeses == 0 ? vlDescParc
                        : (qtdMeses == 0 ? vlDescParc : (mesInicial == (i + 1) ? vlDescParc + vlTotalDescEspec : vlDescParc));
                    parcela.vlLiquido = parcela.vlParcela - parcela.vlDesconto;
                    parcela.vlMulta = tb83.VL_PERCE_MULTA != null ? (decimal)tb83.VL_PERCE_MULTA : 0;
                    parcela.vlJuros = tb83.VL_PERCE_JUROS != null ? (decimal)tb83.VL_PERCE_JUROS : 0;

                    Parcelas.Add(parcela);

                    if (parcela.vlDesconto != vlDescParc)
                    {
                        qtdMeses--;
                        mesInicial++;
                    }
                }

                grdNegociacao.DataSource = Parcelas;
                grdNegociacao.DataBind();
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem procedimentos para serem faturados");
        }

        public class Negociacao
        {
            public string numDoc { get; set; }

            public int numParcela { get; set; }

            public DateTime dtVencimento { get; set; }

            public decimal vlParcela { get; set; }

            public decimal vlDesconto { get; set; }

            public decimal vlLiquido { get; set; }

            public decimal vlMulta { get; set; }

            public decimal vlJuros { get; set; }
        }

        private void LimparDados()
        {
            chkNumParcelas.Checked =
            chkAlterPrimParcela.Checked =
            chkTaxaContrato.Checked =
            chkDiaParcela.Checked =
            chkBoleto.Checked =
            chkTipoDesctoOrcam.Checked = false;

            txtObsOrcam.Text =
            txtVlTotalProcs.Text =
            txtVlDescProcs.Text =
            txtVlLiqdProcs.Text =
            txtValorPrimParce.Text =
            txtDtPrimParcela.Text =
            txtTaxaContrato.Text =
            drpBoleto.SelectedValue =
            drpTipoDesctoOrcam.SelectedValue =
            txtQtdeMesesDesctoOrcam.Text =
            txtDesctoOrcam.Text =
            txtMesIniDesconto.Text =
            txtVlTotal.Text =
            txtVlDescto.Text =
            txtVlLiqui.Text = "";

            txtNumParcelas.Text = "6";
            txtDiaParcela.Text = "5";

            grdNegociacao.DataSource = null;
            grdNegociacao.DataBind();
            grdProcedOrcam.DataSource = null;
            grdProcedOrcam.DataBind();
        }

        private void DesabilitarCampos(bool enabled)
        {
            chkNumParcelas.Enabled =
            chkAlterPrimParcela.Enabled =
            chkTaxaContrato.Enabled =
            chkDiaParcela.Enabled =
            chkBoleto.Enabled =
            chkTipoDesctoOrcam.Enabled =

            txtValorPrimParce.Enabled =
            txtDtPrimParcela.Enabled =
            txtTaxaContrato.Enabled =
            drpBoleto.Enabled =
            drpTipoDesctoOrcam.Enabled =
            txtQtdeMesesDesctoOrcam.Enabled =
            txtDesctoOrcam.Enabled =
            txtMesIniDesconto.Enabled =
            txtVlTotal.Enabled =
            txtVlDescto.Enabled =
            txtVlLiqui.Enabled =
            txtNumParcelas.Enabled =
            txtDiaParcela.Enabled =
            lnkbFinalizar.Enabled = enabled;

            //Desabilitar atendimentos e procedimentos
            if (grdProcedOrcam.HeaderRow != null)
                ((CheckBox)grdProcedOrcam.HeaderRow.FindControl("chkTodosProcs")).Enabled = enabled;

            foreach (GridViewRow li in grdPacientes.Rows)
                ((CheckBox)li.FindControl("chkSelectPaciente")).Enabled = enabled;
        }

        #endregion

        #region Funções de Campo

        private void AbreModalPadrao(string funcao)
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                funcao,
                true
            );
        }

        protected void imgPesqAtendimentos_OnClick(object sender, EventArgs e)
        {
            LimparDados();

            chkNumParcelas.Enabled =
            chkAlterPrimParcela.Enabled =
            chkTaxaContrato.Enabled =
            chkDiaParcela.Enabled =
            chkBoleto.Enabled =
            drpTipoDesctoOrcam.Enabled =
            lnkbFinalizar.Enabled = true;
            hidFinalizado.Value = "N";

            CarregarAtendimentos();
        }

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            LimparDados();
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            List<int> Atends = new List<int>();

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.Checked)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

                    Atends.Add(idAgenda);
                }
            }

            Session["Atendimentos"] = Atends;

            carregaGridOrcamento();
            AtualizarDadosOrcamento();
        }

        protected void lnkbIndicacao_Click(object sender, EventArgs e)
        {
            if (grdIndicacao.HeaderRow != null)
            {
                var drpIndcUnic = (DropDownList)grdIndicacao.HeaderRow.FindControl("drpIndicadorUnico");

                CarregarIndicadores(drpIndcUnic);
            }

            foreach (GridViewRow l in grdIndicacao.Rows)
            {
                var hidIndc = (HiddenField)l.FindControl("hidIndicador");
                var drpIndc = (DropDownList)l.FindControl("drpIndicador");

                CarregarIndicadores(drpIndc);
                
                drpIndc.SelectedValue = hidIndc.Value;
            }

            AbreModalPadrao("AbreModalIndicacao();");
        }

        protected void drpIndicadorUnico_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var drpUnic = (DropDownList)sender;

            foreach (GridViewRow l in grdIndicacao.Rows)
            {
                var drpIndc = (DropDownList)l.FindControl("drpIndicador");

                drpIndc.SelectedValue = drpUnic.SelectedValue;
            }

            AbreModalPadrao("AbreModalIndicacao();");
        }

        protected void lnkbSalvarIndic_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow l in grdIndicacao.Rows)
            {
                var hidIndc = (HiddenField)l.FindControl("hidIndicador");
                var drpIndc = (DropDownList)l.FindControl("drpIndicador");

                hidIndc.Value = drpIndc.SelectedValue;
            }
        }

        protected void chkTodosProcs_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                var chk = (CheckBox)li.FindControl("chkSelectProc");

                chk.Checked = atual.Checked;
            }

            AtualizarDadosOrcamento();
        }

        protected void txtVlDescProcedOrc_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    var txtDesc = (TextBox)linha.FindControl("txtVlDescProcedOrc");
                    if (txtDesc.ClientID == atual.ClientID)
                    {
                        //campo que vai receber valor calculado
                        var lblValorTotal = (Label)linha.FindControl("lblValorTotal");
                        //valor unitário do procedimento
                        string vlProcOrc = ((HiddenField)linha.FindControl("hidVlProcOrc")).Value;

                        var vlProc = !string.IsNullOrEmpty(vlProcOrc) ? decimal.Parse(vlProcOrc) : 0;
                        var vlDesc = !string.IsNullOrEmpty(txtDesc.Text) ? decimal.Parse(txtDesc.Text) : 0;
                        
                        if (vlDesc == 0)
                            lblValorTotal.Text = vlProc.ToString("N2");
                        else if (vlDesc >= vlProc)
                        {
                            txtDesc.Text = vlProc.ToString("N2");
                            lblValorTotal.Text = "0,00";
                        }
                        else
                            lblValorTotal.Text = (vlProc - vlDesc).ToString("N2");

                        AtualizarDadosOrcamento();
                    }
                }
            }
        }

        protected void lnkbFinalizar_OnClick(object sender, EventArgs e)
        {
            Persistencias();
        }

        protected void lnkBoleto_Click(object sender, EventArgs e)
        {
            if (Session["Atendimentos"] != null && ((List<int>)Session["Atendimentos"]).Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel emitir antes de finalizar o faturamento.");
                return;
            }
            else
            {
                TBS390_ATEND_AGEND tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(((List<int>)Session["Atendimentos"]).FirstOrDefault());

                if (tbs390.FL_SITU_FATU != "F")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel emitir antes de finalizar o faturamento.");
                    return;
                }
                else
                {
                    if (String.IsNullOrEmpty(drpBoleto.SelectedValue) && String.IsNullOrEmpty(drpBoletoPopUp.SelectedValue))
                    {
                        AbreModalPadrao("AbreModalBoleto();");
                        return;
                    }
                    else if (String.IsNullOrEmpty(drpBoleto.SelectedValue))
                    {
                        chkBoleto.Checked = true;
                        drpBoleto.SelectedValue = drpBoletoPopUp.SelectedValue;
                    }

                    if (String.IsNullOrEmpty(drpBoleto.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, pois não existe boleto selecionado.");
                        return;
                    }
                    else
                    {
                        //--------> Instancia um novo conjunto de dados de boleto na sessão
                        Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

                        tbs390.TBS174_AGEND_HORARReference.Load();

                        int coAlu = tbs390.TBS174_AGEND_HORAR.CO_ALU.Value;

                        //--------> Recupera dados do Responsável do Aluno
                        var s = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                 join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                 join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO into bai
                                 from tb905 in bai.DefaultIfEmpty()
                                 join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE into cid
                                 from tb904 in cid.DefaultIfEmpty()
                                 where tb07.CO_ALU == coAlu
                                 select new
                                 {
                                     tb07.NU_NIRE,
                                     tb07.NO_ALU,
                                     NOME = tb108.NO_RESP,
                                     BAIRRO = tb905.NO_BAIRRO,
                                     CEP = tb108.CO_CEP_RESP,
                                     CIDADE = tb904.NO_CIDADE,
                                     ENDERECO = tb108.DE_ENDE_RESP,
                                     NUMERO = tb108.NU_ENDE_RESP,
                                     COMPL = tb108.DE_COMP_RESP,
                                     UF = tb904.CO_UF,
                                     CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP
                                 }).FirstOrDefault();

                        int iGrdNeg = 1;
                        //--------> Varre os títulos da grid
                        foreach (GridViewRow row in grdNegociacao.Rows)
                        {
                            string nuDoc = row.Cells[0].Text;
                            int nuPar = int.Parse(row.Cells[1].Text);
                            string strInstruBoleto = "";

                            TBS47_CTA_RECEB tbs47 = TBS47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, nuDoc, nuPar);

                            tbs47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                            if (tbs47.TB227_DADOS_BOLETO_BANCARIO == null)
                                tbs47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(drpBoleto.SelectedValue));

                            tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                            tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                            tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                            tbs47.TB108_RESPONSAVELReference.Load();

                            //------------> Se o título for gerado para um aluno:
                            if (tbs47.TB108_RESPONSAVEL == null)
                            {
                                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                                return;
                            }

                            if (tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                            {
                                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                                return;
                            }

                            //------------> Obtém a unidade
                            TB25_EMPRESA unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                            InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                            //------------> Informações do Boleto
                            boleto.Carteira = tbs47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                            boleto.CodigoBanco = tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

                            /*
                             * Esta parte do código valida se o título já possui um nosso número, se já tiver, ele usa o NossoNúmero do título, registrado na tabela TB47, caso contrário,
                             * ele pega o próximo NossoNúmero registrado no banco, tabela TB29.
                             * */
                            if (tbs47.CO_NOS_NUM != null)
                            {
                                boleto.NossoNumero = tbs47.CO_NOS_NUM.Trim();
                            }
                            else
                            {
                                boleto.NossoNumero = tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                            }
                            boleto.NumeroDocumento = tbs47.NU_DOC + "-" + tbs47.NU_PAR;
                            boleto.Valor = tbs47.VL_PAR_DOC; //valor da parcela do documento
                            boleto.Vencimento = tbs47.DT_VEN_DOC;

                            //------------> Informações do Cedente
                            boleto.NumeroConvenio = tbs47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                            boleto.Agencia = tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                                tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                            boleto.CodigoCedente = tbs47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                            boleto.Conta = tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                            boleto.CpfCnpjCedente = AuxiliFormatoExibicao.preparaCPFCNPJ(unidade.CO_CPFCGC_EMP);
                            boleto.NomeCedente = unidade.NO_RAZSOC_EMP + " " + boleto.CpfCnpjCedente;

                            boleto.Desconto = !tbs47.VL_DES_DOC.HasValue ? 0 : tbs47.VL_DES_DOC.Value;

                            /**
                             * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
                             * */
                            #region Valida layout do boleto gerado
                            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

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

                            if (tbs47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tbs47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                            {
                                var desc = boleto.Valor - tbs47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                                strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                            }

                            //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                            if (tbs47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                                strInstruBoleto += tbs47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                            if (tbs47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                                strInstruBoleto += tbs47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                            if (tbs47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                                strInstruBoleto += tbs47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                            boleto.Instrucoes = strInstruBoleto;

                            //------------> Chave do Título do Contas a Receber
                            boleto.CO_EMP = tbs47.CO_EMP;
                            boleto.NU_DOC = tbs47.NU_DOC;
                            boleto.NU_PAR = tbs47.NU_PAR;
                            boleto.DT_CAD_DOC = tbs47.DT_CAD_DOC;

                            //------------> Faz a adição de instruções ao Boleto
                            boleto.Instrucoes += "<br>";
                            //boleto.Instrucoes += "(*) " + multaMoraDesc + "<br>";


                            //------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                            string CnpjCPF = "";

                            CnpjCPF += "Paciente: " + s.NU_NIRE.ToString().PadLeft(7, '0') + " - " + s.NO_ALU;

                            tbs47.TBS390_ATEND_AGENDReference.Load();

                            if (tbs47.TBS390_ATEND_AGEND != null)
                            {
                                CnpjCPF += "<br> Unidade Atendimento: " + TB25_EMPRESA.RetornaPelaChavePrimaria(tbs47.TBS390_ATEND_AGEND.CO_EMP_ATEND).NO_FANTAS_EMP +
                                                "<br> Nº Contrato: " + tbs47.NU_CONTRATO +
                                                " Data: " + tbs47.TBS390_ATEND_AGEND.DT_CADAS.ToShortDateString();
                            }

                            tbs47.TB39_HISTORICOReference.Load();
                            if (tbs47.TB39_HISTORICO != null)
                                CnpjCPF += "<br>*** Referente: " + tbs47.TB39_HISTORICO.DE_HISTORICO + " ***";

                            boleto.Instrucoes += CnpjCPF;
                            //boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);

                            //------------> Informações do Sacado
                            boleto.BairroSacado = s.BAIRRO;
                            boleto.CepSacado = s.CEP;
                            boleto.CidadeSacado = s.CIDADE;
                            boleto.CpfCnpjSacado = s.CPFCNPJ;
                            boleto.EnderecoSacado = s.ENDERECO + " " + s.NUMERO + " " + s.COMPL;
                            boleto.NomeSacado = s.NOME;
                            boleto.UfSacado = s.UF;

                            //------------> Adiciona o título atual na Sessão
                            ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                            /*
                             * Esta validação verifica se o título já possui NossoNúmaro, se não for o caso, ele atualiza o título incluíndo um novo NossoNúmero, e atualiza a tabela
                             * TB29 para incrementar o próximo NossoNúmero do banco.
                             * */
                            if (tbs47.CO_NOS_NUM == null)
                            {
                                if ((iGrdNeg <= grdNegociacao.Rows.Count) && (grdNegociacao.Rows.Count > 1))
                                {
                                    TB29_BANCO tb29 = TB29_BANCO.RetornaPelaChavePrimaria(tbs47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                                    /*
                                     * Esta parte do código atualiza o NossoNúmero do título (TB47).
                                     * Esta linha foi incluída para resolver o problema de boletos diferentes sendo gerados para um mesmo
                                     * título
                                     * */
                                    tbs47.CO_NOS_NUM = tb29.CO_PROX_NOS_NUM;
                                    GestorEntities.SaveOrUpdate(tbs47, true);

                                    //===> Incluí o nosso número na tabela de nossos números por título
                                    TB045_NOS_NUM tb045 = new TB045_NOS_NUM();
                                    tb045.NU_DOC = tbs47.NU_DOC;
                                    tb045.NU_PAR = tbs47.NU_PAR;
                                    tb045.DT_CAD_DOC = tbs47.DT_CAD_DOC;
                                    tb045.DT_NOS_NUM = DateTime.Now;
                                    tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                                    //===> Pega as informações da empresa/unidade
                                    TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tbs47.CO_EMP);
                                    tb045.TB25_EMPRESA = emp;
                                    //===> Pega as informações do colaborador
                                    TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                                    tb045.TB03_COLABOR = tb03;
                                    tb045.CO_NOS_NUM = tbs47.CO_NOS_NUM;
                                    tb045.CO_BARRA_DOC = tbs47.CO_BARRA_DOC;
                                    GestorEntities.SaveOrUpdate(tb045, true);

                                    long nossoNumero = long.Parse(tb29.CO_PROX_NOS_NUM) + 1;
                                    int casas = tb29.CO_PROX_NOS_NUM.Length;
                                    string mask = string.Empty;
                                    foreach (char ch in tb29.CO_PROX_NOS_NUM) mask += "0";
                                    tb29.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                                    GestorEntities.SaveOrUpdate(tb29, true);
                                }
                            }

                            iGrdNeg++;
                        }

                        //--------> Gera e exibe os boletos
                        BoletoBancarioHelpers.GeraBoletos(this, true);
                    }
                }
            }
        }

        protected void lnkContrato_Click(object sender, EventArgs e)
        {
            if (Session["Atendimentos"] != null && ((List<int>)Session["Atendimentos"]).Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel emitir antes de finalizar o faturamento.");
                return;
            }
            else
            {
                TBS390_ATEND_AGEND tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(((List<int>)Session["Atendimentos"]).FirstOrDefault());

                if (tbs390.FL_SITU_FATU != "F")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel emitir antes de finalizar o faturamento.");
                    return;
                }
                else
                {
                    tbs390.TB009_RTF_DOCTOSReference.Load();
                    if (tbs390.TB009_RTF_DOCTOS == null)
                        AbreModalPadrao("AbreModalContrato();");
                    else
                        GerarContrato(tbs390.NU_CONTRATO);
                }
            }
        }

        protected void lnkbImprimirContrato_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(drpTipContrato.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o Tipo do Contrato.");
                return;
            }
            if (Session["Atendimentos"] != null && ((List<int>)Session["Atendimentos"]).Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel emitir antes de finalizar o faturamento.");
                return;
            }

            var atends = (List<int>)Session["Atendimentos"];
            var numContrat = "";
            foreach (var a in atends)
            {
                TBS390_ATEND_AGEND tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(a);

                tbs390.FL_SITU_FATU = "F";
                tbs390.TB009_RTF_DOCTOS = TB009_RTF_DOCTOS.RetornaPelaChavePrimaria(int.Parse(drpTipContrato.SelectedValue));

                TBS390_ATEND_AGEND.SaveOrUpdate(tbs390, true);

                numContrat = tbs390.NU_CONTRATO;
            }

            var tbs404 = TBS404_CONTRATOS.RetornaPeloNumContrato(numContrat);

            tbs404.NU_CONTRATO = numContrat;
            tbs404.TB009_RTF_DOCTOS = TB009_RTF_DOCTOS.RetornaPelaChavePrimaria(int.Parse(drpTipContrato.SelectedValue));

            TBS404_CONTRATOS.SaveOrUpdate(tbs404);

            GerarContrato(numContrat);
        }

        private void GerarContrato(string numContrato)
        {
            RptContrato rpt = new RptContrato();
            var lRetorno = rpt.InitReport(LoginAuxili.CO_EMP, numContrato);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        #endregion
    }
}