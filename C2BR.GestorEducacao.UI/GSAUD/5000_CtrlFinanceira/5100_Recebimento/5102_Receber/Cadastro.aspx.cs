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
using Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Reflection;
using System.Data;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5102_Receber
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

                txtDtNotaFiscal.Text =
                txtDtReceb.Text = data.ToShortDateString();

                CarregaOperadorasPlanSaude(drpContratacao);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP, true);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP, true);
                CarregarIndicacao();

                OcultarPesquisa(false);
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            #region Validação

            if (String.IsNullOrEmpty(drpPacienteReceb.SelectedValue))
            {
                ReportarErro("Selecione um paciente!", drpPacienteReceb);
                return;
            }

            if (String.IsNullOrEmpty(drpContratacao.SelectedValue))
            {
                ReportarErro("Selecione um tipo de contratação!", drpContratacao);
                return;
            }

            if (chkNotaFiscal.Checked && String.IsNullOrEmpty(txtNotaFiscal.Text))
            {
                ReportarErro("O número da nota fiscal não foi informado!", txtNotaFiscal);
                return;
            }

            decimal vlTotal = 0;

            if (chkDinheiro.Checked)
            {
                if (String.IsNullOrEmpty(txtDinheiro.Text))
                {
                    ReportarErro("O valor em dinheiro não foi informado!", txtDinheiro);
                    return;
                }

                vlTotal += decimal.Parse(txtDinheiro.Text);
            }

            if (chkTransferencia.Checked)
            {
                if (String.IsNullOrEmpty(txtTransferencia.Text))
                {
                    ReportarErro("O valor transferido não foi informado!", txtTransferencia);
                    return;
                }

                vlTotal += decimal.Parse(txtTransferencia.Text);
            }

            if (chkDeposito.Checked)
            {
                if (String.IsNullOrEmpty(txtDeposito.Text))
                {
                    ReportarErro("O valor depositado não foi informado!", txtDeposito);
                    return;
                }

                vlTotal += decimal.Parse(txtDeposito.Text);
            }

            if (chkBoleto.Checked)
            {
                if (String.IsNullOrEmpty(txtBoleto.Text))
                {
                    ReportarErro("O valor em boleto não foi informado!", txtBoleto);
                    return;
                }

                vlTotal += decimal.Parse(txtBoleto.Text);
            }

            if (chkOutros.Checked)
            {
                if (String.IsNullOrEmpty(txtOutros.Text))
                {
                    ReportarErro("O valor em outras formas não foi informado!", txtOutros);
                    return;
                }

                vlTotal += decimal.Parse(txtOutros.Text);
            }

            if (chkDebito.Checked)
            {
                decimal txtDebitos, vlDebitos = 0;
                if (String.IsNullOrEmpty(txtDebito.Text))
                {
                    ReportarErro("O valor de débito total não foi informado!", txtDebito);
                    return;
                }
                else
                    txtDebitos = decimal.Parse(txtDebito.Text);

                if (String.IsNullOrEmpty(txtParcelasDebito.Text))
                {
                    ReportarErro("A quantidade de cartões não foi informada!", txtParcelasDebito);
                    return;
                }/*
                else if (grdCartaoDebito.Rows.Count != int.Parse(txtParcelasDebito.Text))
                {
                    ReportarErro("A quantidade de cartões informada é diferente da cadastrada!", txtParcelasDebito);
                    return;
                }*/

                foreach (GridViewRow i in grdCartaoDebito.Rows)
                {
                    var txtVlrDebito = (TextBox)i.FindControl("txtVlrDebito");

                    if (String.IsNullOrEmpty(txtVlrDebito.Text))
                    {
                        ReportarErro("Informe o valor debitado!", txtVlrDebito);
                        return;
                    }

                    vlDebitos += decimal.Parse(txtVlrDebito.Text);
                }

                if (vlDebitos != 0 && vlDebitos != txtDebitos)
                {
                    ReportarErro("O valor de débito total informado é diferente do cadastrado no(s) cartão(ões)!", txtDebito);
                    return;
                }

                vlTotal += txtDebitos;
            }

            if (chkCredito.Checked)
            {
                decimal txtCreditos, vlCreditos = 0;
                if (String.IsNullOrEmpty(txtCredito.Text))
                {
                    ReportarErro("O valor de crédito total não foi informado!", txtCredito);
                    return;
                }
                else
                {
                    txtCreditos = decimal.Parse(txtCredito.Text);
                }

                if (String.IsNullOrEmpty(txtParcelasCredito.Text))
                {
                    ReportarErro("A quantidade de cartões não foi informada!", txtParcelasCredito);
                    return;
                }
                else if (grdCartaoCredito.Rows.Count != int.Parse(txtParcelasCredito.Text))
                {
                    ReportarErro("A quantidade de cartões informada é diferente da cadastrada!", txtParcelasCredito);
                    return;
                }

                foreach (GridViewRow i in grdCartaoCredito.Rows)
                {
                    var txtVlrCredito = (TextBox)i.FindControl("txtVlrCredito");

                    if (String.IsNullOrEmpty(txtVlrCredito.Text))
                    {
                        ReportarErro("Informe o valor creditado!", txtVlrCredito);
                        return;
                    }

                    vlCreditos += decimal.Parse(txtVlrCredito.Text);
                }

                if (vlCreditos != 0 && vlCreditos != txtCreditos)
                {
                    ReportarErro("O valor de crédito total informado é diferente do cadastrado no(s) cartão(ões)!", txtCredito);
                    return;
                }

                vlTotal += txtCreditos;
            }

            if (chkCheque.Checked)
            {
                decimal txtCheques, vlCheques = 0;
                if (String.IsNullOrEmpty(txtCheque.Text))
                {
                    ReportarErro("O valor de cheque total não foi informado!", txtCheque);
                    return;
                }
                else
                    txtCheques = decimal.Parse(txtCheque.Text);

                if (String.IsNullOrEmpty(txtParcelasCheque.Text))
                {
                    ReportarErro("A quantidade de cheques não foi informada!", txtParcelasCheque);
                    return;
                }/*
                else if (grdCheque.Rows.Count != int.Parse(txtParcelasCheque.Text))
                {
                    ReportarErro("A quantidade de cheques informada é diferente da cadastrada!", txtParcelasCheque);
                    return;
                }*/

                foreach (GridViewRow i in grdCheque.Rows)
                {
                    var txtVlrCheque = (TextBox)i.FindControl("txtVlrCheque");

                    if (String.IsNullOrEmpty(txtVlrCheque.Text))
                    {
                        ReportarErro("Informe o valor do cheque!", txtVlrCheque);
                        return;
                    }

                    vlCheques += decimal.Parse(txtVlrCheque.Text);
                }

                if (vlCheques != 0 && vlCheques != txtCheques)
                {
                    ReportarErro("O valor de cheque(s) total informado é diferente do cadastrado no(s) cheque(s)!", txtCheque);
                    return;
                }

                vlTotal += txtCheques;
            }

            if (vlTotal != 0 && vlTotal != decimal.Parse(txtVlrReceb.Text))
            {
                ReportarErro("O valor total informado é diferente da soma dos valores descritos!", txtVlrReceb);
                return;
            }

            #endregion

            if (!String.IsNullOrEmpty(drpRAP.SelectedValue))
            {
                #region Salva Recebimento

                int idRap = !string.IsNullOrEmpty(drpRAP.SelectedValue) ? int.Parse(drpRAP.SelectedValue) : 0;
                TBS363_CONSUL_PAGTO tbs363 = new TBS363_CONSUL_PAGTO();

                tbs363 = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idRap || c.TBS372_AGEND_AVALI.ID_AGEND_AVALI == idRap).FirstOrDefault() ?? new TBS363_CONSUL_PAGTO();

                try
                {
                    tbs363.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idRap);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    tbs363.TBS372_AGEND_AVALI = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idRap);
                }

                tbs363.DE_TIPO = (!string.IsNullOrEmpty(drpTipoRecebimento.SelectedValue) ? drpTipoRecebimento.SelectedValue : "CO");
                tbs363.VL_TOTAL = (!string.IsNullOrEmpty(txtVlrReceb.Text) ? decimal.Parse(txtVlrReceb.Text) : 0);
                tbs363.QT_PARCE = 0;

                tbs363.FL_DINHE = (chkDinheiro.Checked ? "S" : "N");
                tbs363.VL_DINHE = (!string.IsNullOrEmpty(txtDinheiro.Text) ? decimal.Parse(txtDinheiro.Text) : (decimal?)null);

                tbs363.FL_CHEQUE = (chkCheque.Checked ? "S" : "N");
                tbs363.VL_CHEQUE = (!string.IsNullOrEmpty(txtCheque.Text) ? decimal.Parse(txtCheque.Text) : (decimal?)null);
                tbs363.QT_PARCE_CHEQUE = (!string.IsNullOrEmpty(txtParcelasCheque.Text) ? int.Parse(txtParcelasCheque.Text) : (int?)null);

                tbs363.FL_CARTA_DEBI = (chkDebito.Checked ? "S" : "N");
                tbs363.VL_CARTA_DEBI = (!string.IsNullOrEmpty(txtDebito.Text) ? decimal.Parse(txtDebito.Text) : (decimal?)null);
                tbs363.QT_PARCE_CARTA_DEBI = (!string.IsNullOrEmpty(txtParcelasDebito.Text) ? int.Parse(txtParcelasDebito.Text) : (int?)null);

                tbs363.FL_CARTA_CRED = (chkCredito.Checked ? "S" : "N");
                tbs363.VL_CARTA_CRED = (!string.IsNullOrEmpty(txtCredito.Text) ? decimal.Parse(txtCredito.Text) : (decimal?)null);
                tbs363.QT_PARCE_CARTA_CRED = (!string.IsNullOrEmpty(txtParcelasCredito.Text) ? int.Parse(txtParcelasCredito.Text) : (int?)null);

                tbs363.FL_TRANS = (chkTransferencia.Checked ? "S" : "N");
                tbs363.VL_TRANS = (!string.IsNullOrEmpty(txtTransferencia.Text) ? decimal.Parse(txtTransferencia.Text) : (decimal?)null);

                tbs363.FL_DEPOS = (chkDeposito.Checked ? "S" : "N");
                tbs363.VL_DEPOS = (!string.IsNullOrEmpty(txtDeposito.Text) ? decimal.Parse(txtDeposito.Text) : (decimal?)null);

                tbs363.FL_BOLETO = (chkBoleto.Checked ? "S" : "N");
                tbs363.VL_BOLETO = (!string.IsNullOrEmpty(txtBoleto.Text) ? decimal.Parse(txtBoleto.Text) : (decimal?)null);
                tbs363.QT_PARCE_BOLETO = (!string.IsNullOrEmpty(txtParcelasBoleto.Text) ? int.Parse(txtParcelasBoleto.Text) : (int?)null);

                tbs363.FL_OUTRO = (chkOutros.Checked ? "S" : "N");
                tbs363.VL_OUTRO = (!string.IsNullOrEmpty(txtOutros.Text) ? decimal.Parse(txtOutros.Text) : (decimal?)null);

                tbs363.FL_NOTA_FISCAL = (chkNotaFiscal.Checked ? "S" : "N");
                tbs363.NU_NOTA_FISCAL = !String.IsNullOrEmpty(txtNotaFiscal.Text) ? int.Parse(txtNotaFiscal.Text) : (int?)null;

                tbs363.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");

                tbs363.TB250_OPERA = !String.IsNullOrEmpty(drpContratacao.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(drpContratacao.SelectedValue)) : null;
                tbs363.TB251_PLANO_OPERA = !String.IsNullOrEmpty(drpPlanoReceb.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(drpPlanoReceb.SelectedValue)) : null;

                tbs363.FL_RECIBO = "N";
                tbs363.NU_RECIBO = 0;

                tbs363.DE_OBSERVACOES = txtObsReceb.Text;

                tbs363.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs363.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs363.DT_CADAS = DateTime.Now;

                TBS363_CONSUL_PAGTO.SaveOrUpdate(tbs363);

                #endregion

                #region Salva Cartões e Cheques

                if (chkDebito.Checked)
                    foreach (GridViewRow i in grdCartaoDebito.Rows)
                    {
                        var ddlBcoDebito = (DropDownList)i.FindControl("ddlBcoDebito");
                        var txtAgenDebito = (TextBox)i.FindControl("txtAgenDebito");
                        var txtNumContaDebito = (TextBox)i.FindControl("txtNumContaDebito");
                        var txtNumCartaoDebito = (TextBox)i.FindControl("txtNumCartaoDebito");
                        var txtTitulDebito = (TextBox)i.FindControl("txtTitulDebito");
                        var txtVlrDebito = (TextBox)i.FindControl("txtVlrDebito");
                        var txtNumAutoriDebito = (TextBox)i.FindControl("txtNumAutoriDebito");

                        TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                        tbs364.TBS363_CONSUL_PAGTO = tbs363;
                        tbs364.CO_BCO = ddlBcoDebito.SelectedValue;
                        tbs364.NR_AGENCI = txtAgenDebito.Text;
                        tbs364.NR_CONTA = txtNumContaDebito.Text;
                        tbs364.CO_NUMER = txtNumCartaoDebito.Text;
                        tbs364.NO_TITUL = txtTitulDebito.Text;
                        tbs364.VL_PAGTO = decimal.Parse(txtVlrDebito.Text);
                        tbs364.NU_AUTORIZACAO = txtNumAutoriDebito.Text;
                        tbs364.QT_PARCE = !String.IsNullOrEmpty(txtParcelasDebito.Text) ? int.Parse(txtParcelasDebito.Text) : (int?)null;
                        tbs364.FL_TIPO_TRANSAC = "D";

                        TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                    }

                if (chkCredito.Checked)
                    foreach (GridViewRow i in grdCartaoCredito.Rows)
                    {
                        var ddlBandCredito = (DropDownList)i.FindControl("ddlBandCredito");
                        var txtNumCartaoCredito = (TextBox)i.FindControl("txtNumCartaoCredito");
                        var txtTitulCredito = (TextBox)i.FindControl("txtTitulCredito");
                        var txtVencimentoCredito = (TextBox)i.FindControl("txtVencimentoCredito");
                        var txtVlrCredito = (TextBox)i.FindControl("txtVlrCredito");
                        var txtQtdParcelas = (TextBox)i.FindControl("txtQtdParcelas");
                        var txtQtdDias = (TextBox)i.FindControl("txtQtdDias");
                        var txtNumAutoriCredito = (TextBox)i.FindControl("txtNumAutoriCredito");

                        TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                        tbs364.TBS363_CONSUL_PAGTO = tbs363;
                        tbs364.CO_BANDE = ddlBandCredito.SelectedValue;
                        tbs364.CO_NUMER = txtNumCartaoCredito.Text;
                        tbs364.NO_TITUL = txtTitulCredito.Text;
                        tbs364.DT_VENCI = txtVencimentoCredito.Text;
                        tbs364.VL_PAGTO = decimal.Parse(txtVlrCredito.Text);
                        tbs364.NU_AUTORIZACAO = txtNumAutoriCredito.Text;
                        tbs364.QT_PARCE = !String.IsNullOrEmpty(txtQtdParcelas.Text) ? int.Parse(txtQtdParcelas.Text) : (int?)null;
                        tbs364.QT_DIAS = !String.IsNullOrEmpty(txtQtdDias.Text) ? int.Parse(txtQtdDias.Text) : (int?)null;
                        tbs364.FL_TIPO_TRANSAC = "C";

                        TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                    }

                if (chkCheque.Checked)
                    foreach (GridViewRow i in grdCheque.Rows)
                    {
                        var ddlBcoCheque = (DropDownList)i.FindControl("ddlBcoCheque");
                        var txtAgenCheque = (TextBox)i.FindControl("txtAgenCheque");
                        var txtNumContaCheque = (TextBox)i.FindControl("txtNumContaCheque");
                        var txtNumCheque = (TextBox)i.FindControl("txtNumCheque");
                        var txtCPFCheque = (TextBox)i.FindControl("txtCPFCheque");
                        var txtTitulCheque = (TextBox)i.FindControl("txtTitulCheque");
                        var txtVlrCheque = (TextBox)i.FindControl("txtVlrCheque");
                        var txtVencimentoCheque = (TextBox)i.FindControl("txtVencimentoCheque");

                        TBS365_PAGTO_CHEQUE tbs365 = new TBS365_PAGTO_CHEQUE();

                        tbs365.TBS363_CONSUL_PAGTO = tbs363;
                        tbs365.CO_BCO = ddlBcoCheque.SelectedValue;
                        tbs365.NR_AGENCI = txtAgenCheque.Text;
                        tbs365.NR_CONTA = txtNumContaCheque.Text;
                        tbs365.NR_CHEQUE = txtNumCheque.Text;
                        tbs365.NR_CPF = txtCPFCheque.Text.Replace(".", "").Replace("-", "");
                        tbs365.NO_TITUL = txtTitulCheque.Text;
                        tbs365.VL_PAGTO = decimal.Parse(txtVlrCheque.Text);
                        tbs365.DT_VENC = !String.IsNullOrEmpty(txtVencimentoCheque.Text) ? DateTime.Parse(txtVencimentoCheque.Text) : (DateTime?)null;

                        TBS365_PAGTO_CHEQUE.SaveOrUpdate(tbs365, true);
                    }

                #endregion

                hidIdRecebimento.Value = tbs363.ID_CONSUL_PAGTO.ToString();

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Recebimento realizado com sucesso!");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi localizado o atendimento do paciente!");
        }

        #endregion

        #region Carregamentos

        private void CarregaOperadorasPlanSaude(DropDownList ddl)
        {
            var oper = TB250_OPERA.RetornaTodosRegistros().Where(x => x.FL_SITU_OPER.Equals("A")).DistinctBy(x => x.NM_SIGLA_OPER).OrderBy(x => x.NOM_OPER).ToList();

            ddl.DataTextField = "NM_SIGLA_OPER";
            ddl.DataValueField = "ID_OPER";
            ddl.DataSource = oper;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));

            //CarregarPlanosRecebimento(drpPlanoReceb);
        }

        private void CarregarPacientesComparecimento(DropDownList drp, DateTime dt)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.CO_EMP == LoginAuxili.CO_EMP
                       && tbs174.DT_AGEND_HORAR == dt && tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).DistinctBy(p => p.CO_ALU).ToList();

            var res_ = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                        where tbs372.FL_TIPO_AGENDA == "C"
                        && tbs372.DT_AGEND == dt && tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                        select new { tb07.NO_ALU, tb07.CO_ALU }).DistinctBy(p => p.CO_ALU).ToList();

            if (res_ != null && res_.Count > 0 && res != null)
                foreach (var i in res_)
                    res.Add(i);

            if (res != null && res.Count > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res.OrderBy(r => r.NO_ALU);
                drp.DataBind();
            }
        }

        private void CarregarPlanosRecebimento(int idOper)
        {

            var res = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                       where (idOper != 0 ? tb251.TB250_OPERA.ID_OPER == idOper : true) && tb251.FL_SITU_PLAN == "A"
                       select new { tb251.NOM_PLAN, tb251.ID_PLAN, tb251.NM_SIGLA_PLAN });

            drpPlanoReceb.DataTextField = "NOM_PLAN";
            drpPlanoReceb.DataValueField = "ID_PLAN";
            drpPlanoReceb.DataSource = res;
            drpPlanoReceb.DataBind();

            drpPlanoReceb.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        protected void CarregaBanco(DropDownList drp)
        {
            drp.DataSource = TB29_BANCO.RetornaTodosRegistros();

            drp.DataTextField = "DESBANCO";
            drp.DataValueField = "IDEBANCO";
            drp.DataBind();

            drp.Items.Insert(0, new ListItem("Nenhum", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bandeiras
        /// </summary>
        protected void CarregaBandeira(DropDownList drp)
        {
            drp.Items.Add(new ListItem("Nenhum", "N"));
            drp.Items.Add(new ListItem("American Express", "AmeExp"));
            drp.Items.Add(new ListItem("Cartão BNDES", "BNDES"));
            drp.Items.Add(new ListItem("Diners Club", "DinClub"));
            drp.Items.Add(new ListItem("Elo", "Elo"));
            drp.Items.Add(new ListItem("HiperCard", "HipCar"));
            drp.Items.Add(new ListItem("MasterCard", "MasCar"));
            drp.Items.Add(new ListItem("SoroCred", "SorCr"));
            drp.Items.Add(new ListItem("Visa", "Vis"));
            drp.Items.Add(new ListItem("Outros", "O"));
        }

        private void CarregarGridDebito(DataTable dt, int consl = 0)
        {
            if (consl != 0)
            {
                var res = (from tbs364 in TBS364_PAGTO_CARTAO.RetornaTodosRegistros()
                           where tbs364.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == consl
                           && tbs364.FL_TIPO_TRANSAC == "D"
                           select new
                           {
                               tbs364.CO_BCO,
                               tbs364.NR_AGENCI,
                               tbs364.NR_CONTA,
                               tbs364.CO_NUMER,
                               tbs364.NO_TITUL,
                               tbs364.VL_PAGTO,
                               tbs364.NU_AUTORIZACAO
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dt.NewRow();
                    linha["BANCO"] = i.CO_BCO;
                    linha["AGENCIA"] = i.NR_AGENCI;
                    linha["CONTA"] = i.NR_CONTA;
                    linha["NUMERO"] = i.CO_NUMER;
                    linha["TITULAR"] = i.NO_TITUL;
                    linha["DEBITO"] = i.VL_PAGTO;
                    linha["AUTORIZACAO"] = i.NU_AUTORIZACAO;
                    dt.Rows.Add(linha);
                }
            }

            grdCartaoDebito.DataSource = dt;
            grdCartaoDebito.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdCartaoDebito.Rows)
            {
                var ddlBcoDebito = (DropDownList)li.FindControl("ddlBcoDebito");
                var txtAgenDebito = (TextBox)li.FindControl("txtAgenDebito");
                var txtNumContaDebito = (TextBox)li.FindControl("txtNumContaDebito");
                var txtNumCartaoDebito = (TextBox)li.FindControl("txtNumCartaoDebito");
                var txtTitulDebito = (TextBox)li.FindControl("txtTitulDebito");
                var txtVlrDebito = (TextBox)li.FindControl("txtVlrDebito");
                var txtNumAutoriDebito = (TextBox)li.FindControl("txtNumAutoriDebito");

                string banco, agencia, conta, numero, titular, debito, autorizacao;

                //Coleta os valores do dtv da modal popup
                banco = dt.Rows[aux]["BANCO"].ToString();
                agencia = dt.Rows[aux]["AGENCIA"].ToString();
                conta = dt.Rows[aux]["CONTA"].ToString();
                numero = dt.Rows[aux]["NUMERO"].ToString();
                titular = dt.Rows[aux]["TITULAR"].ToString();
                debito = dt.Rows[aux]["DEBITO"].ToString();
                autorizacao = dt.Rows[aux]["AUTORIZACAO"].ToString();

                //Seta os valores nos campos da modal popup
                CarregaBanco(ddlBcoDebito);
                ddlBcoDebito.SelectedValue = banco;
                txtAgenDebito.Text = agencia;
                txtNumContaDebito.Text = conta;
                txtNumCartaoDebito.Text = numero;
                txtTitulDebito.Text = titular;
                txtVlrDebito.Text = debito;
                txtNumAutoriDebito.Text = autorizacao;
                aux++;
            }
        }

        private DataTable CriarGridDebito()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(CriarColuna("BANCO"));
            dt.Columns.Add(CriarColuna("AGENCIA"));
            dt.Columns.Add(CriarColuna("CONTA"));
            dt.Columns.Add(CriarColuna("NUMERO"));
            dt.Columns.Add(CriarColuna("TITULAR"));
            dt.Columns.Add(CriarColuna("DEBITO"));
            dt.Columns.Add(CriarColuna("AUTORIZACAO"));

            foreach (GridViewRow li in grdCartaoDebito.Rows)
            {
                var linha = dt.NewRow();
                linha["BANCO"] = ((DropDownList)li.FindControl("ddlBcoDebito")).SelectedValue;
                linha["AGENCIA"] = ((TextBox)li.FindControl("txtAgenDebito")).Text;
                linha["CONTA"] = ((TextBox)li.FindControl("txtNumContaDebito")).Text;
                linha["NUMERO"] = ((TextBox)li.FindControl("txtNumCartaoDebito")).Text;
                linha["TITULAR"] = ((TextBox)li.FindControl("txtTitulDebito")).Text;
                linha["DEBITO"] = ((TextBox)li.FindControl("txtVlrDebito")).Text;
                linha["AUTORIZACAO"] = ((TextBox)li.FindControl("txtNumAutoriDebito")).Text;
                dt.Rows.Add(linha);
            }

            return dt;
        }

        private void CarregarGridCredito(DataTable dt, int consl = 0)
        {
            if (consl != 0)
            {
                var res = (from tbs364 in TBS364_PAGTO_CARTAO.RetornaTodosRegistros()
                           where tbs364.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == consl
                           && tbs364.FL_TIPO_TRANSAC == "C"
                           select new
                           {
                               tbs364.CO_BANDE,
                               tbs364.CO_NUMER,
                               tbs364.NO_TITUL,
                               tbs364.DT_VENCI,
                               tbs364.VL_PAGTO,
                               tbs364.NU_AUTORIZACAO,
                               tbs364.QT_PARCE
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dt.NewRow();
                    linha["BANDEIRA"] = i.CO_BANDE;
                    linha["NUMERO"] = i.CO_NUMER;
                    linha["TITULAR"] = i.NO_TITUL;
                    linha["VENCIMENTO"] = i.DT_VENCI;
                    linha["CREDITO"] = i.VL_PAGTO;
                    linha["QTP"] = i.QT_PARCE;
                    linha["QTD"] = i.QT_PARCE;
                    linha["AUTORIZACAO"] = i.NU_AUTORIZACAO;
                    dt.Rows.Add(linha);
                }
            }

            grdCartaoCredito.DataSource = dt;
            grdCartaoCredito.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdCartaoCredito.Rows)
            {
                var ddlBandCredito = (DropDownList)li.FindControl("ddlBandCredito");
                var txtNumCartaoCredito = (TextBox)li.FindControl("txtNumCartaoCredito");
                var txtTitulCredito = (TextBox)li.FindControl("txtTitulCredito");
                var txtVencimentoCredito = (TextBox)li.FindControl("txtVencimentoCredito");
                var txtVlrCredito = (TextBox)li.FindControl("txtVlrCredito");
                var txtQtdParcelas = (TextBox)li.FindControl("txtQtdParcelas");
                var txtQtdDias = (TextBox)li.FindControl("txtQtdDias");
                var txtNumAutoriCredito = (TextBox)li.FindControl("txtNumAutoriCredito");

                string bandeira, numero, titular, vencimento, credito, qtp, qtd, autorizacao;

                //Coleta os valores do dtv da modal popup
                bandeira = dt.Rows[aux]["BANDEIRA"].ToString();
                numero = dt.Rows[aux]["NUMERO"].ToString();
                titular = dt.Rows[aux]["TITULAR"].ToString();
                vencimento = dt.Rows[aux]["VENCIMENTO"].ToString();
                credito = dt.Rows[aux]["CREDITO"].ToString();
                qtp = dt.Rows[aux]["QTP"].ToString();
                qtd = dt.Rows[aux]["QTD"].ToString();
                autorizacao = dt.Rows[aux]["AUTORIZACAO"].ToString();

                //Seta os valores nos campos da modal popup
                CarregaBandeira(ddlBandCredito);
                ddlBandCredito.SelectedValue = bandeira;
                txtNumCartaoCredito.Text = numero;
                txtTitulCredito.Text = titular;
                txtVencimentoCredito.Text = vencimento;
                txtVlrCredito.Text = credito;
                txtQtdParcelas.Text = qtp;
                txtQtdDias.Text = qtd;
                txtNumAutoriCredito.Text = autorizacao;
                aux++;
            }
        }

        private DataTable CriarGridCredito()
        {
            DataTable dt = new DataTable();
            dt.Rows.Clear();

            dt.Columns.Add(CriarColuna("BANDEIRA"));
            dt.Columns.Add(CriarColuna("NUMERO"));
            dt.Columns.Add(CriarColuna("TITULAR"));
            dt.Columns.Add(CriarColuna("VENCIMENTO"));
            dt.Columns.Add(CriarColuna("CREDITO"));
            dt.Columns.Add(CriarColuna("QTP"));
            dt.Columns.Add(CriarColuna("QTD"));
            dt.Columns.Add(CriarColuna("AUTORIZACAO"));

            foreach (GridViewRow li in grdCartaoCredito.Rows)
            {
                var linha = dt.NewRow();
                linha["BANDEIRA"] = ((DropDownList)li.FindControl("ddlBandCredito")).SelectedValue;
                linha["NUMERO"] = ((TextBox)li.FindControl("txtNumCartaoCredito")).Text;
                linha["TITULAR"] = ((TextBox)li.FindControl("txtTitulCredito")).Text;
                linha["VENCIMENTO"] = ((TextBox)li.FindControl("txtVencimentoCredito")).Text;
                linha["CREDITO"] = ((TextBox)li.FindControl("txtVlrCredito")).Text;
                linha["QTP"] = ((TextBox)li.FindControl("txtQtdParcelas")).Text;
                linha["QTD"] = ((TextBox)li.FindControl("txtQtdDias")).Text;
                linha["AUTORIZACAO"] = ((TextBox)li.FindControl("txtNumAutoriCredito")).Text;
                dt.Rows.Add(linha);
            }

            return dt;
        }

        private void CarregarGridCheque(DataTable dt, int consl = 0)
        {
            if (consl != 0)
            {
                var res = (from tbs365 in TBS365_PAGTO_CHEQUE.RetornaTodosRegistros()
                           where tbs365.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == consl
                           select new
                           {
                               tbs365.CO_BCO,
                               tbs365.NR_AGENCI,
                               tbs365.NR_CONTA,
                               tbs365.NR_CHEQUE,
                               tbs365.NR_CPF,
                               tbs365.NO_TITUL,
                               tbs365.VL_PAGTO,
                               tbs365.DT_VENC
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dt.NewRow();
                    linha["BANCO"] = i.CO_BCO;
                    linha["AGENCIA"] = i.NR_AGENCI;
                    linha["CONTA"] = i.NR_CONTA;
                    linha["NUMERO"] = i.NR_CHEQUE;
                    linha["CPF"] = i.NR_CPF;
                    linha["TITULAR"] = i.NO_TITUL;
                    linha["CHEQUE"] = i.VL_PAGTO;
                    linha["VENCIMENTO"] = i.DT_VENC;
                    dt.Rows.Add(linha);
                }
            }

            grdCheque.DataSource = dt;
            grdCheque.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdCheque.Rows)
            {
                var ddlBcoCheque = (DropDownList)li.FindControl("ddlBcoCheque");
                var txtAgenCheque = (TextBox)li.FindControl("txtAgenCheque");
                var txtNumContaCheque = (TextBox)li.FindControl("txtNumContaCheque");
                var txtNumCheque = (TextBox)li.FindControl("txtNumCheque");
                var txtCPFCheque = (TextBox)li.FindControl("txtCPFCheque");
                var txtTitulCheque = (TextBox)li.FindControl("txtTitulCheque");
                var txtVlrCheque = (TextBox)li.FindControl("txtVlrCheque");
                var txtVencimentoCheque = (TextBox)li.FindControl("txtVencimentoCheque");

                string banco, agencia, conta, numero, cpf, titular, cheque, vencimento;

                //Coleta os valores do dtv da modal popup
                banco = dt.Rows[aux]["BANCO"].ToString();
                agencia = dt.Rows[aux]["AGENCIA"].ToString();
                conta = dt.Rows[aux]["CONTA"].ToString();
                numero = dt.Rows[aux]["NUMERO"].ToString();
                cpf = dt.Rows[aux]["CPF"].ToString();
                titular = dt.Rows[aux]["TITULAR"].ToString();
                cheque = dt.Rows[aux]["CHEQUE"].ToString();
                vencimento = dt.Rows[aux]["VENCIMENTO"].ToString();

                //Seta os valores nos campos da modal popup
                CarregaBanco(ddlBcoCheque);
                ddlBcoCheque.SelectedValue = banco;
                txtAgenCheque.Text = agencia;
                txtNumContaCheque.Text = conta;
                txtNumCheque.Text = numero;
                txtCPFCheque.Text = cpf;
                txtTitulCheque.Text = titular;
                txtVlrCheque.Text = cheque;
                txtVencimentoCheque.Text = vencimento;
                aux++;
            }
        }

        private DataTable CriarGridCheque()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(CriarColuna("BANCO"));
            dt.Columns.Add(CriarColuna("AGENCIA"));
            dt.Columns.Add(CriarColuna("CONTA"));
            dt.Columns.Add(CriarColuna("NUMERO"));
            dt.Columns.Add(CriarColuna("CPF"));
            dt.Columns.Add(CriarColuna("TITULAR"));
            dt.Columns.Add(CriarColuna("CHEQUE"));
            dt.Columns.Add(CriarColuna("VENCIMENTO"));
            var l = dt.NewRow();

            foreach (GridViewRow li in grdCheque.Rows)
            {
                var linha = dt.NewRow();
                linha["BANCO"] = ((DropDownList)li.FindControl("ddlBcoCheque")).SelectedValue;
                linha["AGENCIA"] = ((TextBox)li.FindControl("txtAgenCheque")).Text;
                linha["CONTA"] = ((TextBox)li.FindControl("txtNumContaCheque")).Text;
                linha["NUMERO"] = ((TextBox)li.FindControl("txtNumCheque")).Text;
                linha["CPF"] = ((TextBox)li.FindControl("txtCPFCheque")).Text;
                linha["TITULAR"] = ((TextBox)li.FindControl("txtTitulCheque")).Text;
                linha["CHEQUE"] = ((TextBox)li.FindControl("txtVlrCheque")).Text;
                linha["VENCIMENTO"] = ((TextBox)li.FindControl("txtVencimentoCheque")).Text;
                dt.Rows.Add(linha);
            }

            return dt;
        }

        private static DataColumn CriarColuna(String nome)
        {
            DataColumn dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = nome;
            return dc;
        }

        public void CarregaGrdRaps()
        {

            //int paciente = int.Parse(drpPacienteReceb.SelectedValue);
            //var rapAvaliacao = from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
            //                   join tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros() on tbs372.ID_AGEND_AVALI equals tbs367.TBS372_AGEND_AVALI.ID_AGEND_AVALI
            //                   where tbs367.CO_ALU == paciente
            //                   select tbs367.NU_REGIS_RECEP_SOLIC;
            try
            {
                if (drpRAP.SelectedValue != null)
                {
                    var rap = int.Parse(drpRAP.SelectedValue);


                    var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                               join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into ps
                               from tbs353 in ps.DefaultIfEmpty()
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs389.TBS174_AGEND_HORAR.CO_COL equals tb03.CO_COL
                               where tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == rap
                               select new grdRaps
                               {
                                   TP_PROCE = tbs356.CO_TIPO_PROC_MEDI,
                                   NU_PROCE = tbs356.CO_PROC_MEDI,
                                   NM_PROCE = tbs356.NM_PROC_MEDI,
                                   GRP_PROCE = tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                                   SGRP_PROCE = tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                   VLR_PROCE = (tbs386.VL_PROCED.Value != null ? tbs386.VL_PROCED.Value : (tbs353.VL_BASE != null ? tbs353.VL_BASE : 0)),
                                   SOLICI_PROCE = tb03.NO_COL,
                                   SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                                   NUMER_ENT = tb03.NU_ENTID_PROFI,
                                   UF_ENT = tb03.CO_UF_ENTID_PROFI
                               }).ToList();

                    if (res != null)
                    {
                        grdRap.DataSource = res;
                        grdRap.DataBind();
                    }
                }
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O paciente não possui agenda marcada.");
            }

        }

        private void LimparDadosRecebimento()
        {
            hidIdRecebimento.Value =
            hidAgendAvalReceb.Value =
            hidAgendReceb.Value =
            txtResponsavelReceb.Text =
            txtVlrReceb.Text =
            drpContratacao.SelectedValue = "";
            drpPlanoReceb.Items.Clear();
            //drpRAP.SelectedValue = "";
            //drpPlanoReceb.SelectedValue =

            drpTipoRecebimento.SelectedValue = "-";
            //txtParcelas.Text = "1";

            chkDinheiro.Checked =
            chkCheque.Checked =
            chkDebito.Checked =
            chkCredito.Checked =
            chkTransferencia.Checked =
            chkDeposito.Checked =
            chkBoleto.Checked =
            chkOutros.Checked =
            chkNotaFiscal.Checked =
            chkCortesia.Checked = false;

            txtDinheiro.Text =
            txtCheque.Text =
            txtParcelasCheque.Text =
            txtDebito.Text =
            txtParcelasDebito.Text =
            txtCredito.Text =
            txtParcelasCredito.Text =
            txtTransferencia.Text =
            txtDeposito.Text =
            txtBoleto.Text =
            txtParcelasBoleto.Text =
            txtOutros.Text =
            txtNotaFiscal.Text =
            txtObsReceb.Text = "";

            if (grdCartaoDebito.Rows.Count > 0)
            {
                grdCartaoDebito.DataSource = null;
                grdCartaoDebito.DataBind();
            }

            if (grdCartaoCredito.Rows.Count > 0)
            {
                grdCartaoCredito.DataSource = null;
                grdCartaoCredito.DataBind();
            }

            if (grdCheque.Rows.Count > 0)
            {
                grdCheque.DataSource = null;
                grdCheque.DataBind();
            }
        }

        #endregion

        #region Funções de Campo

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            VerificarNireAutomatico();
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            AbreModalPadrao("AbreModalInfosCadas();");

        }

        protected void imgbListaRap_OnClick(object sender, EventArgs e)
        {
            CarregaGrdRaps();
            if (drpRAP.SelectedValue != "")
            {
                AbreModalPadrao("AbreModalListaRaps();");
            }
        }

        private void VerificarNireAutomatico()
        {
            string strTipoNireAuto = "";
            //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
            var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                        where iTb25.CO_EMP == LoginAuxili.CO_EMP
                        select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

            strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";

            if (strTipoNireAuto != "")
            {
                //----------------> Faz a verificação para saber se o NIRE é automático ou não
                if (strTipoNireAuto == "N")
                {
                }
                else
                {
                    ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                    int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                    if (lastCoAlu != null)
                    {
                        txtNuNis.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNuNis.Text = "1";
                }
            }
        }

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

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
        }

        private void PesquisaCarregaResp(int? co_resp)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RG_RESP,
                           tb108.CO_ORG_RG_RESP,
                           tb108.NU_CPF_RESP,
                           tb108.CO_ESTA_RG_RESP,
                           tb108.DT_NASC_RESP,
                           tb108.CO_SEXO_RESP,
                           tb108.CO_CEP_RESP,
                           tb108.CO_ESTA_RESP,
                           tb108.CO_CIDADE,
                           tb108.CO_BAIRRO,
                           tb108.DE_ENDE_RESP,
                           tb108.DES_EMAIL_RESP,
                           tb108.NU_TELE_CELU_RESP,
                           tb108.NU_TELE_RESI_RESP,
                           tb108.CO_ORIGEM_RESP,
                           tb108.CO_RESP,
                           tb108.NU_TELE_WHATS_RESP,
                           tb108.NM_FACEBOOK_RESP,
                           tb108.NU_TELE_COME_RESP,
                       }).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP;
                txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                txtCEP.Text = res.CO_CEP_RESP;
                ddlUF.SelectedValue = res.CO_ESTA_RESP;
                carregaCidade();
                ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
            }
            //updCadasUsuario.Update();
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
        }

        private void CarregarIndicacao()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlIndicacao, LoginAuxili.CO_EMP, false, "0", true, 0, false);
        }

        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaBairros(ddlBairro, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            chkPaciMoraCoResp.Checked = chkPaciEhResp.Checked;

            AbreModalPadrao("AbreModalInfosCadas();");
        }

        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                txtCPFMOD.Text = txtCPFResp.Text;
                txtnompac.Text = txtNomeResp.Text;
                txtDtNascPaci.Text = txtDtNascResp.Text;
                ddlSexoPaci.SelectedValue = ddlSexResp.SelectedValue;
                txtTelCelPaci.Text = txtTelCelResp.Text;
                txtTelResPaci.Text = txtTelFixResp.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailPaci.Text = txtEmailResp.Text;
                txtWhatsPaci.Text = txtNuWhatsResp.Text;

                txtEmailPaci.Enabled = false;
                txtCPFMOD.Enabled = false;
                txtnompac.Enabled = false;
                txtDtNascPaci.Enabled = false;
                ddlSexoPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelResPaci.Enabled = false;
                ddlGrParen.Enabled = false;
                txtWhatsPaci.Enabled = false;
            }
            else
            {
                txtCPFMOD.Text = "";
                txtnompac.Text = "";
                txtDtNascPaci.Text = "";
                ddlSexoPaci.SelectedValue = "";
                txtTelCelPaci.Text = "";
                txtTelResPaci.Text = "";
                ddlGrParen.SelectedValue = "";
                txtEmailPaci.Text = "";
                txtWhatsPaci.Text = "";

                txtCPFMOD.Enabled = true;
                txtnompac.Enabled = true;
                txtDtNascPaci.Enabled = true;
                ddlSexoPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelResPaci.Enabled = true;
                ddlGrParen.Enabled = true;
                txtEmailPaci.Enabled = true;
                txtWhatsPaci.Enabled = true;
                hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        protected void imgPesqCEP_OnClick(object sender, EventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograEndResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    carregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLograEndResp.Text = "";
                    ddlBairro.SelectedValue = "";
                    ddlCidade.SelectedValue = "";
                    ddlUF.SelectedValue = "";
                }
            }
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        protected void lnkCadastroCompleto_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("3106_CadastramentoUsuariosSimp/Busca.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                this.Session[SessoesHttp.URLOrigem] = HttpContext.Current.Request.Url.PathAndQuery;

                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo.Replace("Busca", "Cadastro"), HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
            }
        }

        protected void lnkSalvar_OnClick(object sender, EventArgs e)
        {
            try
            {
                AbreModalPadrao("AbreModalInfosCadas();");
                if (string.IsNullOrEmpty(txtnompac.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Paciente é requerido");
                    txtnompac.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtNomeResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Responsável é requerido");
                    txtNomeResp.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(ddlSexoPaci.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Sexo do Paciente é requerido");
                    ddlSexoPaci.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtNuNis.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nire do Paciente é requerido");
                    txtNuNis.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtApelido.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O campo apelido e  obrigatório ");
                    txtApelido.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtDtNascResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Data nascimento e obrigatório ");
                    txtDtNascResp.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtDtNascPaci.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Data nascimento  e obrigatório ");
                    txtDtNascPaci.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(ddlBairro.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione o bairro");
                    ddlBairro.Focus();
                    return;
                }

                var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                var cpfValido = true;

                if (!String.IsNullOrEmpty(txtCPFResp.Text))
                {
                    if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                        txtCPFResp.Focus();
                        return;
                    }
                }
                else if (tb25.FL_CPF_RESP_OBRIGATORIO == "S")
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do responsável é obrigatório");
                    txtCPFResp.Focus();
                    return;
                }
                else if (tb25.FL_CPF_RESP_OBRIGATORIO == "N" && String.IsNullOrEmpty(txtCPFResp.Text))
                {
                    var cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    //Enquanto existir, calcula um novo cpf
                    while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado || w.NU_CONTROLE == cpfGerado).Any())
                        cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    txtCPFResp.Text = cpfGerado;
                    cpfValido = false;
                }

                if (!String.IsNullOrEmpty(txtCPFMOD.Text))
                {
                    if (!AuxiliValidacao.ValidaCpf(txtCPFMOD.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do paciente invalido!");
                        txtCPFMOD.Focus();
                        return;
                    }
                }
                else if (tb25.FL_CPF_PAC_OBRIGATORIO == "S")
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente é obrigatório");
                    txtCPFMOD.Focus();
                    return;
                }

                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                TB108_RESPONSAVEL tb108;

                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                var cpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");
                var resp = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NU_CPF_RESP == cpfResp).FirstOrDefault();

                var respExist = false;
                if (resp == null && !cpfValido)
                {
                    var dtNasc = DateTime.Parse(txtDtNascResp.Text);
                    var res = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NO_RESP == txtNomeResp.Text && r.DT_NASC_RESP == dtNasc).FirstOrDefault();

                    if (res != null)
                    {
                        resp = res;
                        respExist = true;
                    }
                }

                if (resp != null && (!String.IsNullOrEmpty(cpfResp) || respExist))
                    tb108 = resp;
                else if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    string NomeApeliResp = "";

                    if (!string.IsNullOrEmpty(txtNomeResp.Text))
                    {
                        var nomeResp = txtNomeResp.Text.Split(' ');
                        NomeApeliResp = nomeResp[0] + (nomeResp.Length > 1 ? " " + nomeResp[1] : "");
                    }

                    tb108 = new TB108_RESPONSAVEL();

                    tb108.NU_CONTROLE =
                    tb108.NU_CPF_RESP = cpfResp;
                    tb108.FL_CPF_VALIDO = cpfValido ? "S" : "N";
                    tb108.NO_RESP = txtNomeResp.Text.ToUpper();
                    tb108.NO_APELIDO_RESP = NomeApeliResp.ToUpper();
                    tb108.CO_RG_RESP = txtNuIDResp.Text;
                    tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                    tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                    tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                    tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                    tb108.CO_CEP_RESP = txtCEP.Text;
                    tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                    tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                    tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                    tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                    tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                    tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.CO_ORIGEM_RESP = "NN";
                    tb108.CO_SITU_RESP = "A";
                    tb108.DT_SITU_RESP = DateTime.Now;

                    //Atribui valores vazios para os campos not null da tabela de Responsável.
                    tb108.FL_NEGAT_CHEQUE = "V";
                    tb108.FL_NEGAT_SERASA = "V";
                    tb108.FL_NEGAT_SPC = "V";
                    tb108.CO_INST = 0;
                    tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
                }
                else
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value));

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                TB07_ALUNO tb07;

                //Verifica antes se já existe o paciente algum paciente com o mesmo CPF informado nos campos, caso não exista, cria um novo
                var cpfPac = txtCPFMOD.Text.Replace(".", "").Replace("-", "");
                var pac = TB07_ALUNO.RetornaTodosRegistros().Where(a => a.NU_CPF_ALU == cpfPac).FirstOrDefault();

                var pacExist = false;
                if (pac == null && String.IsNullOrEmpty(cpfPac) || (pac != null && String.IsNullOrEmpty(cpfPac)))
                {
                    var dtNasc = DateTime.Parse(txtDtNascPaci.Text);
                    var res = TB07_ALUNO.RetornaTodosRegistros().Where(p => p.NO_ALU == txtnompac.Text && p.DT_NASC_ALU == dtNasc).FirstOrDefault();

                    if (res != null)
                    {
                        pac = res;
                        pacExist = true;
                    }
                }

                if (pac != null && (!String.IsNullOrEmpty(cpfPac) || pacExist))
                    tb07 = pac;
                else if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    tb07 = new TB07_ALUNO();

                    #region Bloco foto
                    int codImagem = upImagemAluno.GravaImagem();
                    tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                    #endregion

                    tb07.NO_ALU = txtnompac.Text.ToUpper();
                    tb07.NU_CPF_ALU = cpfPac;
                    tb07.NU_NIS = decimal.Parse(txtNuNis.Text);
                    tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                    tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                    tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_WHATS_ALU = txtWhatsPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                    tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                    tb07.CO_EMP = LoginAuxili.CO_EMP;
                    tb07.TB25_EMPRESA1 = tb25;
                    tb07.TB25_EMPRESA = tb25;
                    tb07.TB108_RESPONSAVEL = tb108;
                    tb07.NO_APE_ALU = txtApelido.Text.ToUpper();
                    if (chkPaciMoraCoResp.Checked)
                    {
                        tb07.CO_CEP_ALU = txtCEP.Text;
                        tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                        tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                        tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                    }

                    //Salva os valores para os campos not null da tabela de Usuário
                    tb07.CO_INST = LoginAuxili.ORG_CODIGO_ORGAO;
                    tb07.DT_CADA_ALU = tb07.DT_SITU_ALU = tb07.DT_ENTRA_INSTI = DateTime.Now;
                    tb07.CO_SITU_ALU = "A";
                    tb07.TP_DEF = "N";
                    tb07.FL_LIST_ESP = "N";

                    #region Campo De Indicação
                    if (!String.IsNullOrEmpty(ddlIndicacao.SelectedValue))
                    {
                        tb07.CO_EMP_INDICACAO = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlIndicacao.SelectedValue)).CO_EMP;
                        tb07.CO_COL_INDICACAO = int.Parse(ddlIndicacao.SelectedValue);
                        tb07.DT_INDICACAO = DateTime.Now;
                    }
                    #endregion

                    #region trata para criação do nire

                    var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                    int nir = 0;
                    if (res == null)
                    {
                        nir = 1;
                    }
                    else
                    {
                        nir = res.NU_NIRE;
                    }

                    int nirTot = nir + 1;

                    #endregion

                    tb07.NU_NIRE = nirTot;

                    tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
                }
                else
                    tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value));

                divResp.Visible = false;
                divSuccessoMessage.Visible = true;
                lblMsg.Text = "Usuário salvo com êxito!";
                lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                lblMsg.Visible = true;
                lblMsgAviso.Visible = true;

                OcultarPesquisa(true);
                //updTopo.Update();

                #endregion
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            drpPacienteReceb.Items.Clear();
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(r => r.NO_ALU);

            drpPacienteReceb.DataSource = res;
            drpPacienteReceb.DataTextField = "NO_ALU";
            drpPacienteReceb.DataValueField = "CO_ALU";
            drpPacienteReceb.DataBind();

            drpPacienteReceb.Items.Insert(0, new ListItem("Selecione", ""));
            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
            LimparDadosRecebimento();
            drpRAP.Items.Clear();
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePacPesq.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            drpPacienteReceb.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        protected void drpPacienteReceb_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimparDadosRecebimento();
                drpRAP.Items.Clear();
                if (!String.IsNullOrEmpty(drpPacienteReceb.SelectedValue))
                {
                    var pac = int.Parse(drpPacienteReceb.SelectedValue);

                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               where tbs174.CO_EMP == LoginAuxili.CO_EMP
                               && tbs174.CO_ALU == pac
                               select new
                               {
                                   tbs174.ID_AGEND_HORAR,
                                   //tbs174.NU_REGIS_CONSUL,
                                   tbs174.FL_CORTESIA,
                                   ID_OPER = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.ID_OPER : 0,
                                   ID_PLAN = tbs174.TB251_PLANO_OPERA != null ? tbs174.TB251_PLANO_OPERA.ID_PLAN : 0,
                                   tbs174.VL_CONSUL
                               }).FirstOrDefault();

                    var res_ = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                                where /*tbs372.FL_TIPO_AGENDA == "C"*/
                                tbs372.CO_ALU == pac
                                select new
                                {
                                    tbs372.ID_AGEND_AVALI,
                                    //tbs372.TBS367_RECEP_SOLIC,
                                    tbs372.FL_CORTESIA,
                                    ID_OPER = tbs372.TB250_OPERA != null ? tbs372.TB250_OPERA.ID_OPER : 0,
                                    ID_PLAN = tbs372.TB251_PLANO_OPERA != null ? tbs372.TB251_PLANO_OPERA.ID_PLAN : 0
                                }).FirstOrDefault();

                    hidAgendReceb.Value = ""; // (res != null ? res.ID_AGEND_HORAR.ToString() : (res_ != null ? res_.ID_AGEND_AVALI.ToString() : ""));
                    if (res != null)
                    {
                        if (res.ID_OPER != 0)
                        {
                            drpContratacao.SelectedValue = res.ID_OPER.ToString();
                        }
                    }
                    else
                    {
                        if (res_ != null)
                        {
                            if (res_.ID_OPER != 0)
                            {
                                drpContratacao.SelectedValue = res_.ID_OPER.ToString();
                            }
                        }
                    }

                    //CarregarPlanosRecebimento(drpPlanoReceb);

                    //if ((res != null && drpPlanoReceb.Items.Contains(new ListItem("", res.ID_PLAN.ToString()))))
                    if (res != null && res.ID_PLAN != 0)
                    {
                        drpPlanoReceb.SelectedValue = res.ID_PLAN.ToString();
                    }
                    //else if ((res_ != null && drpPlanoReceb.Items.Contains(new ListItem("", res_.ID_PLAN.ToString()))))
                    else if (res_ != null && res_.ID_PLAN != 0)
                    {
                        drpPlanoReceb.SelectedValue = res_.ID_PLAN.ToString();
                    }

                    chkCortesia.Checked = (res != null ? (res.FL_CORTESIA == "S" ? true : false) : (res_ != null ? (res_.FL_CORTESIA == "S" ? true : false) : false));

                    txtVlrReceb.Text = (res != null ? res.VL_CONSUL.ToString() : "");

                    DateTime dtRap = DateTime.Parse("01/01/1900");

                    var rap174 = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                  where tbs174.CO_EMP == LoginAuxili.CO_EMP
                                  && tbs174.CO_ALU == pac
                                  select new RAP
                                  {
                                      IdRap = tbs174.ID_AGEND_HORAR,
                                      DataRap = tbs174.DT_AGEND_HORAR,
                                      HoraRap = tbs174.HR_AGEND_HORAR,
                                      TipoRap = "CO",
                                      NuRap = tbs174.NU_REGIS_CONSUL
                                  }
                                  ).Concat(
                                            from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                                            join tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros() on tbs372.ID_AGEND_AVALI equals tbs367.TBS372_AGEND_AVALI.ID_AGEND_AVALI
                                            where /*tbs372.FL_TIPO_AGENDA == "C"*/
                                            tbs372.CO_ALU == pac
                                            select new RAP
                                            {
                                                IdRap = tbs367.ID_RECEP_SOLIC,
                                                DataRap = (tbs372.DT_AGEND.HasValue ? tbs372.DT_AGEND.Value : dtRap),
                                                HoraRap = tbs372.HR_AGEND,
                                                TipoRap = "CO",
                                                NuRap = tbs367.NU_REGIS_RECEP_SOLIC
                                            }
                                   ).Distinct().OrderByDescending(x => x.DataRap);

                    drpRAP.DataSource = rap174;
                    drpRAP.DataTextField = "deRap";
                    drpRAP.DataValueField = "IdRap";
                    drpRAP.DataBind();
                    drpRAP.Items.Insert(0, new ListItem("Selecione", ""));



                    var p = TB07_ALUNO.RetornaPeloCoAlu(pac);
                    if (p != null)
                    {
                        p.TB108_RESPONSAVELReference.Load();

                        if (p.TB108_RESPONSAVEL != null)
                            txtResponsavelReceb.Text = p.TB108_RESPONSAVEL.NO_RESP;
                    }
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
        }

        protected void txtDtReceb_OnTextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDtReceb.Text))
            {
                LimparDadosRecebimento();
                OcultarPesquisa(false);
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");
        }

        protected void drpContratacao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idOper = (!string.IsNullOrEmpty(drpContratacao.SelectedValue) ? int.Parse(drpContratacao.SelectedValue) : 0);

            CarregarPlanosRecebimento(idOper);
        }

        protected void lnkAddCartaoDebito_OnClick(object sender, EventArgs e)
        {
            DataTable dt = CriarGridDebito();

            DataRow linha = dt.NewRow();
            linha["BANCO"] = "";
            linha["AGENCIA"] = "";
            linha["CONTA"] = "";
            linha["NUMERO"] = "";
            linha["TITULAR"] = "";
            linha["DEBITO"] = "";
            linha["AUTORIZACAO"] = "";
            dt.Rows.Add(linha);

            CarregarGridDebito(dt);
        }

        protected void imgExcCartaoDebito_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = -1;
            if (grdCartaoDebito.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdCartaoDebito.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcCartaoDebito");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }

                if (aux != -1)
                {
                    DataTable dt = CriarGridDebito();
                    dt.Rows.RemoveAt(aux);

                    CarregarGridDebito(dt);
                }
            }
        }

        protected void lnkAddCartaoCredito_OnClick(object sender, EventArgs e)
        {
            DataTable dt = CriarGridCredito();

            DataRow linha = dt.NewRow();
            linha["BANDEIRA"] = "";
            linha["NUMERO"] = "";
            linha["TITULAR"] = "";
            linha["VENCIMENTO"] = "";
            linha["CREDITO"] = "";
            linha["QTP"] = "";
            linha["QTD"] = "";
            linha["AUTORIZACAO"] = "";
            dt.Rows.Add(linha);

            CarregarGridCredito(dt);
        }

        protected void imgExcCartaoCredito_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = -1;
            if (grdCartaoCredito.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdCartaoCredito.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcCartaoCredito");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }

                if (aux != -1)
                {
                    DataTable dt = CriarGridCredito();
                    dt.Rows.RemoveAt(aux);

                    CarregarGridCredito(dt);
                }
            }
        }

        protected void lnkAddCheque_OnClick(object sender, EventArgs e)
        {
            DataTable dt = CriarGridCheque();

            DataRow linha = dt.NewRow();
            linha["BANCO"] = "";
            linha["AGENCIA"] = "";
            linha["CONTA"] = "";
            linha["NUMERO"] = "";
            linha["CPF"] = "";
            linha["TITULAR"] = "";
            linha["CHEQUE"] = "";
            linha["VENCIMENTO"] = "";
            dt.Rows.Add(linha);

            CarregarGridCheque(dt);
        }

        protected void imgExcCheque_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = -1;
            if (grdCheque.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdCheque.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcCheque");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }

                if (aux != -1)
                {
                    DataTable dt = CriarGridCheque();
                    dt.Rows.RemoveAt(aux);

                    CarregarGridCheque(dt);
                }
            }
        }

        protected void lnkbEmitirRecibo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hidIdRecebimento.Value))
            {
                var tbs363 = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(int.Parse(hidIdRecebimento.Value));

                if (tbs363 != null)
                {
                    if (tbs363.FL_CORTESIA == "S")
                    {
                        ReportarErro("Não é possivel emitir um recibo para uma cortesia!");
                        return;
                    }

                    if (tbs363.FL_RECIBO != "S")
                    {
                        var res = (from c in TBS363_CONSUL_PAGTO.RetornaTodosRegistros()
                                   where c.FL_RECIBO == "S"
                                   select new { c.NU_RECIBO }).OrderByDescending(n => n.NU_RECIBO).FirstOrDefault();

                        if (res != null)
                            tbs363.NU_RECIBO = res.NU_RECIBO + 1;
                        else
                            tbs363.NU_RECIBO = 1;

                        tbs363.FL_RECIBO = "S";

                        TBS363_CONSUL_PAGTO.SaveOrUpdate(tbs363);
                    }

                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

                    RptRecibo rpt = new RptRecibo();
                    var retorno = rpt.InitReport(int.Parse(drpPacienteReceb.SelectedValue), tbs363.ID_CONSUL_PAGTO, "",/*txtRAP.Text,*/ DateTime.Parse(txtDtReceb.Text), txtVlrReceb.Text, AuxiliFormatoExibicao.RetornarValorPorExtenso(decimal.Parse(txtVlrReceb.Text)), infos, LoginAuxili.CO_EMP);

                    GerarRelatorioPadrão(rpt, retorno);
                }
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário finalizar o recebimento para gerar o recibo!");
        }

        private void ReportarErro(String msg, Control ctl = null)
        {
            if (ctl != null)
                ctl.Focus();

            AuxiliPagina.EnvioMensagemErro(this.Page, msg);
        }

        private void GerarRelatorioPadrão(DevExpress.XtraReports.UI.XtraReport rpt, int lRetorno)
        {
            if (lRetorno == 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro na geração do Relatório! Tente novamente.");
            else if (lRetorno < 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem dados para a impressão do formulário solicitado.");
            else
            {
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                //----------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");

                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }
        }

        protected void lnkbRecepcao_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("8250_RecepcaoEncaminhamento/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
        }

        protected void CarregaValorTotal(object sender, EventArgs e)
        {
            int idRap;

            if (drpRAP.SelectedValue != null && drpRAP.SelectedValue != "")
            {
                idRap = int.Parse(drpRAP.SelectedValue);
            }
            else
            {
                idRap = 0;
            }

            TBS363_CONSUL_PAGTO tbs363 = null;

            tbs363 = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idRap || c.TBS372_AGEND_AVALI.ID_AGEND_AVALI == idRap).FirstOrDefault();

            if (tbs363 != null)
            {
                drpTipoRecebimento.SelectedValue = tbs363.DE_TIPO;
                drpTipoRecebimento.Enabled = false;
                txtVlrReceb.Text = tbs363.VL_TOTAL.ToString();
                //txtParcelas.Text = tbs363.QT_PARCE.ToString();

                chkDinheiro.Checked = (tbs363.FL_DINHE == "S" ? true : false);
                if (tbs363.VL_DINHE.HasValue)
                    txtDinheiro.Text = tbs363.VL_DINHE.Value.ToString();

                chkCheque.Checked = (tbs363.FL_CHEQUE == "S" ? true : false);
                if (tbs363.VL_CHEQUE.HasValue)
                    txtCheque.Text = tbs363.VL_CHEQUE.Value.ToString();
                txtParcelasCheque.Text = tbs363.QT_PARCE_CHEQUE.ToString();

                chkDebito.Checked = (tbs363.FL_CARTA_DEBI == "S" ? true : false);
                if (tbs363.VL_CARTA_DEBI.HasValue)
                    txtDebito.Text = tbs363.VL_CARTA_DEBI.Value.ToString();
                txtParcelasDebito.Text = tbs363.QT_PARCE_CARTA_DEBI.ToString();

                chkCredito.Checked = (tbs363.FL_CARTA_CRED == "S" ? true : false);
                if (tbs363.VL_CARTA_CRED.HasValue)
                    txtCredito.Text = tbs363.VL_CARTA_CRED.Value.ToString();
                txtParcelasCredito.Text = tbs363.QT_PARCE_CARTA_CRED.ToString();

                chkTransferencia.Checked = (tbs363.FL_TRANS == "S" ? true : false);
                if (tbs363.VL_TRANS.HasValue)
                    txtTransferencia.Text = tbs363.VL_TRANS.Value.ToString();

                chkDeposito.Checked = (tbs363.FL_DEPOS == "S" ? true : false);
                if (tbs363.VL_DEPOS.HasValue)
                    txtDeposito.Text = tbs363.VL_DEPOS.Value.ToString();

                chkBoleto.Checked = (tbs363.FL_BOLETO == "S" ? true : false);
                if (tbs363.VL_BOLETO.HasValue)
                    txtBoleto.Text = tbs363.VL_BOLETO.Value.ToString();
                txtParcelasBoleto.Text = tbs363.QT_PARCE_BOLETO.ToString();

                chkOutros.Checked = (tbs363.FL_OUTRO == "S" ? true : false);
                if (tbs363.VL_OUTRO.HasValue)
                    txtOutros.Text = tbs363.VL_OUTRO.Value.ToString();

                chkNotaFiscal.Checked = (tbs363.FL_NOTA_FISCAL == "S" ? true : false);
                if (tbs363.NU_NOTA_FISCAL.HasValue)
                    txtNotaFiscal.Text = tbs363.NU_NOTA_FISCAL.Value.ToString();

                chkCortesia.Checked = (tbs363.FL_CORTESIA == "S" ? true : false);

                tbs363.TB250_OPERAReference.Load();
                if (tbs363.TB250_OPERA != null)
                {
                    drpContratacao.SelectedValue = tbs363.TB250_OPERA.ID_OPER.ToString();
                    if (!string.IsNullOrEmpty(drpContratacao.SelectedValue))
                    {
                        CarregarPlanosRecebimento(int.Parse(drpContratacao.SelectedValue));
                    }

                    tbs363.TB251_PLANO_OPERAReference.Load();
                    //if (tbs363.TB251_PLANO_OPERA != null && drpPlanoReceb.Items.Contains(new ListItem("", tbs363.TB251_PLANO_OPERA.ID_PLAN.ToString())))
                    drpPlanoReceb.SelectedValue = tbs363.TB251_PLANO_OPERA.ID_PLAN.ToString();
                }

                txtObsReceb.Text = tbs363.DE_OBSERVACOES;

                hidIdRecebimento.Value = tbs363.ID_CONSUL_PAGTO.ToString();

                CarregarGridDebito(CriarGridDebito(), tbs363.ID_CONSUL_PAGTO);
                CarregarGridCredito(CriarGridCredito(), tbs363.ID_CONSUL_PAGTO);
                CarregarGridCheque(CriarGridCheque(), tbs363.ID_CONSUL_PAGTO);
            }
            else
            {

                var res = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                                .Join(TBS174_AGEND_HORAR.RetornaTodosRegistros(), a => a.TBS174_AGEND_HORAR.ID_AGEND_HORAR, b => b.ID_AGEND_HORAR, (a, b) => new { a, b })
                                .Join(TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros(), c => c.a.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI, d => d.ID_ITENS_PLANE_AVALI, (c, d) => new { c, d })
                                .Where(x => x.c.b.ID_AGEND_HORAR == idRap)
                                .Select(w => new
                                {
                                    w.d.VL_PROCED,
                                    w.d.QT_PROCED
                                }).ToList();

                decimal valorTotal = 0;

                foreach (var x in res)
                {
                    valorTotal += (x.VL_PROCED.HasValue && x.QT_PROCED.HasValue ? x.VL_PROCED.Value * x.QT_PROCED.Value : 0);
                }

                //decimal vlProcedimento = 0;

                txtVlrReceb.Text = valorTotal.ToString("N2");

                hidAgendReceb.Value =
                drpContratacao.SelectedValue = "";
                drpTipoRecebimento.SelectedValue = "-";
                chkDinheiro.Checked =
                chkCheque.Checked =
                chkDebito.Checked =
                chkCredito.Checked =
                chkTransferencia.Checked =
                chkDeposito.Checked =
                chkBoleto.Checked =
                chkOutros.Checked =
                chkNotaFiscal.Checked =
                chkCortesia.Checked = false;

                txtDinheiro.Text =
                txtCheque.Text =
                txtParcelasCheque.Text =
                txtDebito.Text =
                txtParcelasDebito.Text =
                txtCredito.Text =
                txtParcelasCredito.Text =
                txtTransferencia.Text =
                txtDeposito.Text =
                txtBoleto.Text =
                txtParcelasBoleto.Text =
                txtOutros.Text =
                txtNotaFiscal.Text =
                txtObsReceb.Text = "";

                if (grdCartaoDebito.Rows.Count > 0)
                {
                    grdCartaoDebito.DataSource = null;
                    grdCartaoDebito.DataBind();
                }

                if (grdCartaoCredito.Rows.Count > 0)
                {
                    grdCartaoCredito.DataSource = null;
                    grdCartaoCredito.DataBind();
                }

                if (grdCheque.Rows.Count > 0)
                {
                    grdCheque.DataSource = null;
                    grdCheque.DataBind();
                }
            }

        }

        #endregion

        #region classes

        public class Rap { }

        public class RAP
        {
            public int IdRap { get; set; }
            public DateTime DataRap { get; set; }
            public string HoraRap { get; set; }
            public string TipoRap { get; set; }
            public string NuRap { get; set; }

            public string DeRap
            {
                get { return DataRap.ToShortDateString() + " - " + HoraRap + " - " + TipoRap + " - " + NuRap; }
            }
        }

        public class grdRaps
        {
            public string TP_PROCE { get; set; }
            public string NU_PROCE { get; set; }
            public string NM_PROCE { get; set; }
            public string GRP_PROCE { get; set; }
            public string SGRP_PROCE { get; set; }
            public decimal VLR_PROCE { get; set; }
            public string SOLICI_PROCE { get; set; }
            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public string CRM_SOLICI_PROCE
            {
                get
                {
                    string valor = this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT;
                    if (valor != "  - ")
                        return valor;
                    else
                        return "S/R";
                }
            }

        }

        #endregion
    }
}