using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Reflection;
using System.Data;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5104_ReceberContrato
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
                    txtCreditos = decimal.Parse(txtCredito.Text);

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

            if (!String.IsNullOrEmpty(hidAgendReceb.Value) || !String.IsNullOrEmpty(hidAgendAvalReceb.Value))
            {
                #region Salva Recebimento

                var idAgend = !string.IsNullOrEmpty(hidAgendReceb.Value) ? int.Parse(hidAgendReceb.Value) : 0;
                var idAtend = !string.IsNullOrEmpty(hidAgendAvalReceb.Value) ? int.Parse(hidAgendAvalReceb.Value) : 0;
                TBS363_CONSUL_PAGTO tbs363 = new TBS363_CONSUL_PAGTO();

                if (!String.IsNullOrEmpty(hidIdRecebimento.Value))
                    tbs363 = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(int.Parse(hidIdRecebimento.Value));
                else
                {
                    if (idAgend != 0)
                    {
                        tbs363 = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idAgend).FirstOrDefault() ?? new TBS363_CONSUL_PAGTO();
                        tbs363.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgend);
                    }
                    else if (idAtend != 0)
                    {
                        tbs363 = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.TBS372_AGEND_AVALI.ID_AGEND_AVALI == idAtend).FirstOrDefault() ?? new TBS363_CONSUL_PAGTO();
                        tbs363.TBS372_AGEND_AVALI = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAtend);
                    }
                }

                //tbs363.DE_TIPO = drpTipoRecebimento.SelectedValue;
                tbs363.VL_TOTAL = (!string.IsNullOrEmpty(txtVlrReceb.Text) ? decimal.Parse(txtVlrReceb.Text) : 0);
                //tbs363.QT_PARCE = (!string.IsNullOrEmpty(txtParcelas.Text) ? int.Parse(txtParcelas.Text) : 0);

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

                //tbs363.FL_NOTA_FISCAL = (chkNotaFiscal.Checked ? "S" : "N");
                //tbs363.NU_NOTA_FISCAL = !String.IsNullOrEmpty(txtNotaFiscal.Text) ? int.Parse(txtNotaFiscal.Text) : (int?)null;

                //tbs363.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");

                //tbs363.TB250_OPERA = !String.IsNullOrEmpty(drpContratacao.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(drpContratacao.SelectedValue)) : null;
                //tbs363.TB251_PLANO_OPERA = !String.IsNullOrEmpty(drpPlanoReceb.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(drpPlanoReceb.SelectedValue)) : null;

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
                        var txtNumAutoriCredito = (TextBox)i.FindControl("txtNumAutoriCredito");

                        TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                        tbs364.TBS363_CONSUL_PAGTO = tbs363;
                        tbs364.CO_BANDE = ddlBandCredito.SelectedValue;
                        tbs364.CO_NUMER = txtNumCartaoCredito.Text;
                        tbs364.NO_TITUL = txtTitulCredito.Text;
                        tbs364.DT_VENCI = txtVencimentoCredito.Text;
                        tbs364.VL_PAGTO = decimal.Parse(txtVlrCredito.Text);
                        tbs364.NU_AUTORIZACAO = txtNumAutoriCredito.Text;
                        tbs364.QT_PARCE = !String.IsNullOrEmpty(txtParcelasCredito.Text) ? int.Parse(txtParcelasCredito.Text) : (int?)null;
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

        private void CarregarPacientesComparecimento(DropDownList drp)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).DistinctBy(p => p.CO_ALU).ToList();
            
            if (res != null && res.Count > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res.OrderBy(r => r.NO_ALU);
                drp.DataBind();
            }

            drp.Items.Insert(0, new ListItem("Selecione", ""));
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
                               tbs364.NU_AUTORIZACAO
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dt.NewRow();
                    linha["BANDEIRA"] = i.CO_BANDE;
                    linha["NUMERO"] = i.CO_NUMER;
                    linha["TITULAR"] = i.NO_TITUL;
                    linha["VENCIMENTO"] = i.DT_VENCI;
                    linha["CREDITO"] = i.VL_PAGTO;
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
                var txtNumAutoriCredito = (TextBox)li.FindControl("txtNumAutoriCredito");

                string bandeira, numero, titular, vencimento, credito, autorizacao;

                //Coleta os valores do dtv da modal popup
                bandeira = dt.Rows[aux]["BANDEIRA"].ToString();
                numero = dt.Rows[aux]["NUMERO"].ToString();
                titular = dt.Rows[aux]["TITULAR"].ToString();
                vencimento = dt.Rows[aux]["VENCIMENTO"].ToString();
                credito = dt.Rows[aux]["CREDITO"].ToString();
                autorizacao = dt.Rows[aux]["AUTORIZACAO"].ToString();

                //Seta os valores nos campos da modal popup
                CarregaBandeira(ddlBandCredito);
                ddlBandCredito.SelectedValue = bandeira;
                txtNumCartaoCredito.Text = numero;
                txtTitulCredito.Text = titular;
                txtVencimentoCredito.Text = vencimento;
                txtVlrCredito.Text = credito;
                txtNumAutoriCredito.Text = autorizacao;
                aux++;
            }
        }

        private DataTable CriarGridCredito()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(CriarColuna("BANDEIRA"));
            dt.Columns.Add(CriarColuna("NUMERO"));
            dt.Columns.Add(CriarColuna("TITULAR"));
            dt.Columns.Add(CriarColuna("VENCIMENTO"));
            dt.Columns.Add(CriarColuna("CREDITO"));
            dt.Columns.Add(CriarColuna("AUTORIZACAO"));

            foreach (GridViewRow li in grdCartaoCredito.Rows)
            {
                var linha = dt.NewRow();
                linha["BANDEIRA"] = ((DropDownList)li.FindControl("ddlBandCredito")).SelectedValue;
                linha["NUMERO"] = ((TextBox)li.FindControl("txtNumCartaoCredito")).Text;
                linha["TITULAR"] = ((TextBox)li.FindControl("txtTitulCredito")).Text;
                linha["VENCIMENTO"] = ((TextBox)li.FindControl("txtVencimentoCredito")).Text;
                linha["CREDITO"] = ((TextBox)li.FindControl("txtVlrCredito")).Text;
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

        private void LimparDadosRecebimento()
        {
            hidIdRecebimento.Value =
            hidAgendAvalReceb.Value =
            hidAgendReceb.Value =
            txtRAP.Text =
            txtResponsavelReceb.Text =
            txtVlrReceb.Text = "";

            chkDinheiro.Checked =
            chkCheque.Checked =
            chkDebito.Checked =
            chkCredito.Checked =
            chkTransferencia.Checked =
            chkDeposito.Checked =
            chkBoleto.Checked =
            chkOutros.Checked = false;

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

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            drpPacienteReceb.Items.Clear();
            CarregarPacientesComparecimento(drpPacienteReceb);

            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
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
            LimparDadosRecebimento();

            if (!String.IsNullOrEmpty(drpPacienteReceb.SelectedValue))
            {
                var pac = int.Parse(drpPacienteReceb.SelectedValue);
                var dt = DateTime.Now;//.Parse(txtDtReceb.Text);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           where tbs174.CO_EMP == LoginAuxili.CO_EMP
                           && tbs174.CO_ALU == pac
                           && tbs174.DT_AGEND_HORAR == dt
                           select new
                           {
                               tbs174.ID_AGEND_HORAR,
                               tbs174.NU_REGIS_CONSUL,
                               tbs174.FL_CORTESIA,
                               ID_OPER = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.ID_OPER : 0,
                               ID_PLAN = tbs174.TB251_PLANO_OPERA != null ? tbs174.TB251_PLANO_OPERA.ID_PLAN : 0
                           }).FirstOrDefault();

                if (res == null)
                {
                    var res_ = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                                where tbs372.FL_TIPO_AGENDA == "C"
                                && tbs372.CO_ALU == pac
                                && tbs372.DT_AGEND == dt
                                select new
                                {
                                    tbs372.ID_AGEND_AVALI,
                                    tbs372.TBS367_RECEP_SOLIC,
                                    tbs372.FL_CORTESIA,
                                    ID_OPER = tbs372.TB250_OPERA != null ? tbs372.TB250_OPERA.ID_OPER : 0,
                                    ID_PLAN = tbs372.TB251_PLANO_OPERA != null ? tbs372.TB251_PLANO_OPERA.ID_PLAN : 0
                                }).FirstOrDefault();

                    if (res_ != null)
                    {
                        hidAgendAvalReceb.Value = res_.ID_AGEND_AVALI.ToString();

                        if (res_.TBS367_RECEP_SOLIC != null && res_.TBS367_RECEP_SOLIC.FirstOrDefault() != null)
                            txtRAP.Text = res_.TBS367_RECEP_SOLIC.FirstOrDefault().NU_REGIS_RECEP_SOLIC;

                        //drpContratacao.SelectedValue = res_.ID_OPER.ToString();
                        //CarregarPlanosRecebimento();
                        //if (drpPlanoReceb.Items.Contains(new ListItem("", res_.ID_PLAN.ToString())))
                        //    drpPlanoReceb.SelectedValue = res_.ID_PLAN.ToString();
                        //chkCortesia.Checked = (res_.FL_CORTESIA == "S" ? true : false);
                    }
                }
                else
                {
                    hidAgendReceb.Value = res.ID_AGEND_HORAR.ToString();
                    txtRAP.Text = res.NU_REGIS_CONSUL;

                    //drpContratacao.SelectedValue = res.ID_OPER.ToString();
                    //CarregarPlanosRecebimento();
                    //if (drpPlanoReceb.Items.Contains(new ListItem("", res.ID_PLAN.ToString())))
                    //    drpPlanoReceb.SelectedValue = res.ID_PLAN.ToString();
                    //chkCortesia.Checked = (res.FL_CORTESIA == "S" ? true : false);
                }

                var p = TB07_ALUNO.RetornaPeloCoAlu(pac);
                if (p != null)
                {
                    p.TB108_RESPONSAVELReference.Load();

                    if (p.TB108_RESPONSAVEL != null)
                        txtResponsavelReceb.Text = p.TB108_RESPONSAVEL.NO_RESP;
                }

                TBS363_CONSUL_PAGTO tbs363 = null;

                if (!String.IsNullOrEmpty(hidAgendReceb.Value))
                {
                    var id = int.Parse(hidAgendReceb.Value);
                    tbs363 = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.TBS174_AGEND_HORAR.ID_AGEND_HORAR == id).FirstOrDefault();
                }
                else if (!String.IsNullOrEmpty(hidAgendAvalReceb.Value))
                {
                    var id = int.Parse(hidAgendAvalReceb.Value);
                    tbs363 = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(c => c.TBS372_AGEND_AVALI.ID_AGEND_AVALI == id).FirstOrDefault();
                }

                if (tbs363 != null)
                {
                    txtVlrReceb.Text = tbs363.VL_TOTAL.ToString();

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

                    txtObsReceb.Text = tbs363.DE_OBSERVACOES;

                    hidIdRecebimento.Value = tbs363.ID_CONSUL_PAGTO.ToString();

                    CarregarGridDebito(CriarGridDebito(), tbs363.ID_CONSUL_PAGTO);
                    CarregarGridCredito(CriarGridCredito(), tbs363.ID_CONSUL_PAGTO);
                    CarregarGridCheque(CriarGridCheque(), tbs363.ID_CONSUL_PAGTO);
                }
            }
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
                    var retorno = rpt.InitReport(int.Parse(drpPacienteReceb.SelectedValue), tbs363.ID_CONSUL_PAGTO, txtRAP.Text, DateTime.Now/*Parse(txtDtReceb.Text)*/, txtVlrReceb.Text, AuxiliFormatoExibicao.RetornarValorPorExtenso(decimal.Parse(txtVlrReceb.Text)), infos, LoginAuxili.CO_EMP);

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

        #endregion
    }
}