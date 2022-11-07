
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5100_CtrlTesouraria._5110_ManutMovFinCaixa
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlDtMovto.Enabled = false;
                ddlFuncCaixa.Enabled = false;
                grdContratos.Enabled = false;
                ddlCaixa.Enabled = false;
                ddlTpMovimento.Enabled = false;
                CarregaFuncionarios();
                MontaGridFormPagamento(0);
                txtMotivo.Enabled = false;
            }
            
        }

        protected void btnSaveMovimento_Click(object sender, EventArgs e)
        {
            //aqui verificar se a gride tem ocorrencia
            // tu vai mexer nesse evento aqui.. ai tu define c cordova as regras de negocio.
            // p saber oq vai acontecer na alteração.. eu coloquei p excluir todas as formas de pa-
            //gamento e popular de novo qdo nova condição == "A"
            // tem q saber quais os campos q vão ter alterados na TB47..
            //verificar o esquema da data de quitação..
            //essas paradas..
            // fiz quase tudo.. só faltam esses es..
            // e algumas coisas de layout tb q cordova vai pedir p ajustar..
            //


            if (grdContratos.Rows.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe movimentação para os parâmetros informados.");
                return;
            }
            int contOco = 0;
            int coSeqMovto = 0;
            string tpMov = null;
            int coCaixa = 0;
            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        contOco++;
                        // aqui estou pegando o id do movimento selecionado
                        coSeqMovto = int.Parse(linha.Cells[1].Text);
                        tpMov = linha.Cells[2].Text;
                        coCaixa = int.Parse(linha.Cells[14].Text);
                    }
                }
            }

            //aqui verifico se ele checou um item na gride
            if (contOco == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma movimentação foi selecionada.");
                return;
            }

            //aqui verifico se ele selecionou mais de um item.. mas vc vai fazer esse tratamento em outro lugar q aqui n pode ocorrer..
            if (contOco > 1)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Mais de uma movimentação foi selecionada.");
                return;
            }

            //int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue):0;

            var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                            select new
                            {
                                tb295.CO_EMP,
                                tb295.CO_CAIXA,
                                tb295.DT_MOVIMENTO,
                                tb295.CO_COLABOR_CAIXA,
                                tb295.CO_USUARIO_ABERT
                            }).FirstOrDefault();

           if (varTb295 != null)
            {
                if (grdFormPag.Rows.Count > 0)
                {
                    decimal dcmDifValor = 0;
                    decimal dcmTotalGridFormPag = 0;
                    foreach (GridViewRow row in grdFormPag.Rows)
                    {
                        if (((TextBox)row.Cells[3].FindControl("txtValorNC")).Text != "")
                        {
                            decimal valor = Decimal.Parse(((TextBox)row.Cells[3].FindControl("txtValorNC")).Text);
                            decimal qtd = Decimal.Parse(((TextBox)row.Cells[3].FindControl("txtQtdeNC")).Text == "" || ((TextBox)row.Cells[3].FindControl("txtQtdeNC")).Text == "0" ? "1" : ((TextBox)row.Cells[3].FindControl("txtQtdeNC")).Text);
                            dcmTotalGridFormPag = dcmTotalGridFormPag + (qtd * valor);
                        }
                    }
                    dcmDifValor = Decimal.Parse(txtPagoDANC.Text) - dcmTotalGridFormPag;
                    if (dcmDifValor > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Valor informado nas Formas de Pagamento é menor que o valor do documento.");
                        return;
                    }

                    if (dcmDifValor < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Valor informado nas Formas de Pagamento é maior que o valor do documento.");
                        return;
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser informado os valores pagos de acordo com a Forma de Pagamento.");
                    return;
                }


                // Guardar log
                TB192_CAIXA_LOGMOV logmov = new TB192_CAIXA_LOGMOV();

                // aqui vou carregar informações da movimentação selecionada
                TB296_CAIXA_MOVIMENTO tb296 = TB296_CAIXA_MOVIMENTO.RetornaPeloSequencial(coSeqMovto);
                
                #region Dados LogMov Original
                logmov.CO_ALU = tb296.CO_ALU;
                logmov.CO_CENT_CUSTO = tb296.CO_CENT_CUSTO;
                logmov.CO_CAIXA = tb296.CO_CAIXA;
                logmov.CO_CLIENTE = tb296.CO_CLIENTE;
                logmov.CO_COLABOR_CAIXA = tb296.CO_COLABOR_CAIXA;
                logmov.CO_DEVOLUCAO = tb296.CO_DEVOLUCAO;
                logmov.CO_EMP = tb296.CO_EMP;
                logmov.CO_EMP_CAIXA = tb296.CO_EMP_CAIXA;
                logmov.CO_FORN = tb296.CO_FORN;
                logmov.CO_HISTORICO = tb296.CO_HISTORICO;
                logmov.CO_SEQU_PC = tb296.CO_SEQU_PC;
                logmov.CO_TIPO_DOC = tb296.CO_TIPO_DOC;
                logmov.CO_TIPO_REC = tb296.CO_TIPO_REC;
                logmov.CO_USUARIO = tb296.CO_USUARIO;
                logmov.DE_OBS_DOC = tb296.DE_OBS_DOC;
                logmov.DT_DOC_CAIXA = tb296.DT_DOC_CAIXA;
                logmov.DT_MOVIMENTO = tb296.DT_MOVIMENTO;
                logmov.DT_PAGTO_DOC = tb296.DT_PAGTO_DOC;
                logmov.DT_REGISTRO_ORI = tb296.DT_REGISTRO;
                logmov.DT_VEN_DOC = tb296.DT_VEN_DOC;
                logmov.FLA_GER_AUTO = tb296.FLA_GER_AUTO;
                logmov.FLA_SITU_DOC_ORI = tb296.FLA_SITU_DOC;
                logmov.IDEBANCO = tb296.IDEBANCO;
                logmov.NU_AGE_DOC = tb296.NU_AGE_DOC;
                logmov.NU_CTA_DOC = tb296.NU_CTA_DOC;
                logmov.NU_DOC_CAIXA = tb296.NU_DOC_CAIXA;
                logmov.NU_PAR_DOC_CAIXA = tb296.NU_PAR_DOC_CAIXA;
                logmov.TP_CLIENTE_DOC = tb296.TP_CLIENTE_DOC;
                logmov.TP_OPER_CAIXA_ORI = tb296.TP_OPER_CAIXA;
                logmov.VR_ADC_DOC_ORI = tb296.VR_ADC_DOC;
                logmov.VR_DES_DOC_ORI = tb296.VR_DES_DOC;
                logmov.VR_DOCU = tb296.VR_DOCU;
                logmov.VR_JUR_DOC_ORI = tb296.VR_JUR_DOC;
                logmov.VR_LIQ_DOC_ORI = tb296.VR_LIQ_DOC;
                logmov.VR_MUL_DOC_ORI = tb296.VR_MUL_DOC;
                logmov.DE_MOTIV_CANCEL = tb296.DE_MOTIV_CANCEL;
                //logmov.CO_TIPO_REC = tb296.CO_TIPO_REC;
                #endregion
               
                tb296.DT_REGISTRO = DateTime.Now;
                tb296.FLA_SITU_DOC = ddlNovaCond.SelectedValue;
                
                if (tpMov == "REC")
                {
                    //aqui to carregando o titulo associado a esse movimento
                    var varTb47 = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   where tb47.CO_EMP == LoginAuxili.CO_EMP && tb47.NU_DOC == tb296.NU_DOC_CAIXA && tb47.NU_PAR == tb296.NU_PAR_DOC_CAIXA
                                   && tb47.DT_CAD_DOC.Year == tb296.DT_DOC_CAIXA.Value.Year && tb47.DT_CAD_DOC.Month == tb296.DT_DOC_CAIXA.Value.Month
                                   && tb47.DT_CAD_DOC.Day == tb296.DT_DOC_CAIXA.Value.Day
                                   select tb47).FirstOrDefault();

                    if (varTb47 != null)
                    {
                        

                        if (ddlNovaCond.SelectedValue == "A")
                        {
                            varTb47.VR_JUR_PAG = txtJurosDANC.Text != "" ? (decimal?)Decimal.Parse(txtJurosDANC.Text) : null;
                            varTb47.VR_MUL_PAG = txtMultaDANC.Text != "" ? (decimal?)Decimal.Parse(txtMultaDANC.Text) : null;
                            varTb47.VR_DES_PAG = txtDesctoDANC.Text != "" ? (decimal?)Decimal.Parse(txtDesctoDANC.Text) : null;
                            varTb47.VR_PAG = txtPagoDANC.Text != "" ? (decimal?)Decimal.Parse(txtPagoDANC.Text) : null;
                            varTb47.VR_TOT_DOC = txtVlTituloDANC.Text != "" ? Decimal.Parse(txtVlTituloDANC.Text) : 0; 
                            varTb47.IC_SIT_DOC = "Q";
                            tb296.VR_DOCU = txtVlTituloDANC.Text != "" ? Decimal.Parse(txtVlTituloDANC.Text) : 0; 
                            tb296.VR_MUL_DOC = txtMultaDANC.Text != "" ? (decimal?)Decimal.Parse(txtMultaDANC.Text) : null;
                            tb296.VR_JUR_DOC = txtJurosDANC.Text != "" ? (decimal?)Decimal.Parse(txtJurosDANC.Text) : null;
                            tb296.VR_DES_DOC = txtDesctoDANC.Text != "" ? (decimal?)Decimal.Parse(txtDesctoDANC.Text) : null;
                            tb296.VR_ADC_DOC = txtAdiciDANC.Text != "" ? (decimal?)Decimal.Parse(txtAdiciDANC.Text) : null;
                            tb296.VR_LIQ_DOC = txtPagoDANC.Text != "" ? (decimal?)Decimal.Parse(txtPagoDANC.Text) : null;
                            tb296.TP_OPER_CAIXA = "C";
                           
                            #region Dados LogMov atual
                            logmov.DT_REGISTRO = tb296.DT_REGISTRO;
                            logmov.FLA_SITU_DOC = tb296.FLA_SITU_DOC;
                            logmov.TP_OPER_CAIXA = tb296.TP_OPER_CAIXA;
                            logmov.VR_ADC_DOC = tb296.VR_ADC_DOC;
                            logmov.VR_DES_DOC = tb296.VR_DES_DOC;
                            logmov.VR_JUR_DOC = tb296.VR_JUR_DOC;
                            logmov.VR_LIQ_DOC = tb296.VR_LIQ_DOC;
                            logmov.VR_MUL_DOC = tb296.VR_MUL_DOC;
                            logmov.VR_DOCU = tb296.VR_DOCU;
                            
                            #endregion

                            TB296_CAIXA_MOVIMENTO.SaveOrUpdate(tb296, true);
                        }
                        else
                        {
                            
                            varTb47.VR_JUR_PAG = null;
                            varTb47.VR_MUL_PAG = null;
                            varTb47.VR_DES_PAG = null;
                            varTb47.VR_PAG = null;
                            varTb47.DT_REC_DOC = null;
                            varTb47.IC_SIT_DOC = "A";

                            tb296.FLA_SITU_DOC = "C";
                            tb296.DE_MOTIV_CANCEL = txtMotivo.Text;
                            #region Dados LogMov atual
                            logmov.DT_REGISTRO = tb296.DT_REGISTRO;
                            logmov.FLA_SITU_DOC = tb296.FLA_SITU_DOC;
                            logmov.TP_OPER_CAIXA = tb296.TP_OPER_CAIXA;
                            logmov.VR_ADC_DOC = null;
                            logmov.VR_DES_DOC = null;
                            logmov.VR_JUR_DOC = null;
                            logmov.VR_LIQ_DOC = null;
                            logmov.VR_MUL_DOC = null;
                            logmov.DE_MOTIV_CANCEL = txtMotivo.Text;
                            #endregion
                        }
                        
                        varTb47.CO_CAIXA = coCaixa;
                        varTb47.CO_COL_BAIXA = varTb295.CO_COLABOR_CAIXA;
                        varTb47.DT_MOV_CAIXA = DateTime.Now;
                        varTb47.DT_ALT_REGISTRO = DateTime.Now;                        
                        varTb47.FL_ORIGEM_PGTO = "C";

                        int ctTipoRecbto = 0;
                        int idTipoRecbto = 0;

                        #region Deletar ocorrências da TB156
                        var ocoTb156 = (from iTb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                                        where iTb156.CO_CAIXA_MOVIMENTO == coSeqMovto
                                        select iTb156).ToList();

                        foreach (var viTb156 in ocoTb156)
                        {
                            if (ddlNovaCond.SelectedValue == "A")
                            {
                                GestorEntities.Delete(viTb156, true);
                            }
                            else
                            {
                                viTb156.CO_STATUS = "C";
                                GestorEntities.SaveOrUpdate(viTb156, true);
                            }
                        }
                        #endregion
                        if (ddlNovaCond.SelectedValue == "A")
                        {
                            //aqui to varrendo a gride de forma de pagamento
                            foreach (GridViewRow linha in grdFormPag.Rows)
                            {
                                if ((((TextBox)linha.Cells[5].FindControl("txtValorNC")).Text != "") && (ddlNovaCond.SelectedValue == "A"))
                                {
                                    if (Decimal.Parse(((TextBox)linha.Cells[5].FindControl("txtValorNC")).Text) > 0)
                                    {
                                        ctTipoRecbto++;
                                        if ((((TextBox)linha.Cells[4].FindControl("txtQtdeNC")).Text == "") && (int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value) != 5))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser informada.");
                                            return;
                                        }


                                        TB156_FormaPagamento tb156 = new TB156_FormaPagamento();

                                        tb156.VR_RECEBIDO = Decimal.Parse(((TextBox)linha.Cells[5].FindControl("txtValorNC")).Text);
                                        tb156.DE_OBS = ((TextBox)linha.Cells[6].FindControl("txtObservacaoNC")).Text != "" ? ((TextBox)linha.Cells[6].FindControl("txtObservacaoNC")).Text : null;
                                        tb156.CO_TIPO_REC = int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value);
                                        tb156.CO_STATUS = "A";
                                        tb156.DT_STATUS = DateTime.Now;
                                        tb156.DT_CADASTRO = DateTime.Now;
                                        idTipoRecbto = int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value);

                                        if (int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value) != 5)
                                        {
                                            if (int.Parse(((TextBox)linha.Cells[4].FindControl("txtQtdeNC")).Text) == 0)
                                            {
                                                AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser maior que zero.");
                                                return;
                                            }

                                            tb156.NU_QTDE = int.Parse(((TextBox)linha.Cells[4].FindControl("txtQtdeNC")).Text);
                                        }
                                        else
                                            tb156.NU_QTDE = 0;
                                        tb156.CO_CAIXA_MOVIMENTO = tb296.CO_SEQMOV_CAIXA;

                                        #region Dados Log Forma de pagamento
                                        TB193_FORMAPAGAMENTO_LOG logFormPg = new TB193_FORMAPAGAMENTO_LOG();
                                        logFormPg.CO_CAIXA_MOVIMENTO = tb156.CO_CAIXA_MOVIMENTO;
                                        logFormPg.CO_STATUS = tb156.CO_STATUS;
                                        logFormPg.CO_TIPO_REC = tb156.CO_TIPO_REC;
                                        logFormPg.DE_OBS = tb156.DE_OBS;
                                        logFormPg.DE_OBS_ORI = ((TextBox)linha.Cells[2].FindControl("txtObservacao")).Text != "" ? ((TextBox)linha.Cells[2].FindControl("txtObservacao")).Text : null;
                                        logFormPg.DT_CADASTRO = tb156.DT_CADASTRO;
                                        logFormPg.DT_STATUS = tb156.DT_STATUS;
                                        logFormPg.NU_QTDE = tb156.NU_QTDE;
                                        logFormPg.NU_QTDE_ORI = ((TextBox)linha.Cells[0].FindControl("txtQtdeFP")).Text != "" ? int.Parse(((TextBox)linha.Cells[0].FindControl("txtQtdeFP")).Text) : 0;
                                        logFormPg.VR_RECEBIDO = tb156.VR_RECEBIDO;
                                        logFormPg.VR_RECEBIDO_ORI = ((TextBox)linha.Cells[1].FindControl("txtValorFP")).Text != "" ? Decimal.Parse(((TextBox)linha.Cells[1].FindControl("txtValorFP")).Text) : 0;

                                        logmov.TB193_FORMAPAGAMENTO_LOG.Add(logFormPg);
                                        #endregion

                                        TB156_FormaPagamento.SaveOrUpdate(tb156, true);
                                    }
                                }
                                else {
                                    // Formas de pagamentos que foram retirados na manutenção
                                    if ((((TextBox)linha.Cells[1].FindControl("txtValorFP")).Text != "") && (ddlNovaCond.SelectedValue == "A"))
                                    {
                                        #region Dados Log Forma de pagamento
                                        TB193_FORMAPAGAMENTO_LOG logFormPg = new TB193_FORMAPAGAMENTO_LOG();
                                        logFormPg.CO_CAIXA_MOVIMENTO = tb296.CO_SEQMOV_CAIXA;
                                        logFormPg.CO_STATUS = "A";
                                        logFormPg.CO_TIPO_REC = int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value); ;
                                        logFormPg.DE_OBS = null;
                                        logFormPg.DE_OBS_ORI = ((TextBox)linha.Cells[2].FindControl("txtObservacao")).Text != "" ? ((TextBox)linha.Cells[2].FindControl("txtObservacao")).Text : null;
                                        logFormPg.DT_CADASTRO = DateTime.Now;
                                        logFormPg.DT_STATUS = DateTime.Now;
                                        logFormPg.NU_QTDE = 0;
                                        logFormPg.NU_QTDE_ORI = ((TextBox)linha.Cells[0].FindControl("txtQtdeFP")).Text != "" ? int.Parse(((TextBox)linha.Cells[0].FindControl("txtQtdeFP")).Text) : 0;
                                        logFormPg.VR_RECEBIDO = 0;
                                        logFormPg.VR_RECEBIDO_ORI = ((TextBox)linha.Cells[1].FindControl("txtValorFP")).Text != "" ? Decimal.Parse(((TextBox)linha.Cells[1].FindControl("txtValorFP")).Text) : 0;

                                        logmov.TB193_FORMAPAGAMENTO_LOG.Add(logFormPg);
                                        #endregion
                                    }
                                }
                            }

                            if (ctTipoRecbto > 1)
                            {
                                varTb47.FL_TIPO_RECEB = "M";
                            }
                            else
                            {
                                if (idTipoRecbto != 0)
                                {
                                    //5 => Dinheiro, 2 => Cheque, 9 e 16 => Cartão, O restante => Banco
                                    varTb47.FL_TIPO_RECEB = idTipoRecbto == 5 ? "D" : idTipoRecbto == 2 ? "H" : (idTipoRecbto == 9 || idTipoRecbto == 16) ? "C" : "B";
                                }
                            }
                        }
                        TB47_CTA_RECEB.SaveOrUpdate(varTb47, true);

                        // Salva log
                        LogMovCaixa logcaixa = new LogMovCaixa();
                        logcaixa.AtualizaLOG(logmov);
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número da conta não foi encontrado.");
                        return;
                    }
                }
                else
                {
                    var varTb38 = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                   where tb38.CO_EMP == LoginAuxili.CO_EMP && tb38.NU_DOC == tb296.NU_DOC_CAIXA && tb38.NU_PAR == tb296.NU_PAR_DOC_CAIXA
                                   && tb38.DT_CAD_DOC.Year == tb296.DT_DOC_CAIXA.Value.Year && tb38.DT_CAD_DOC.Month == tb296.DT_DOC_CAIXA.Value.Month
                                   && tb38.DT_CAD_DOC.Day == tb296.DT_DOC_CAIXA.Value.Day
                                   select tb38).FirstOrDefault();

                    if (varTb38 != null)
                    {
                                            


                        if (ddlNovaCond.SelectedValue == "A")
                        {
                            varTb38.VR_JUR_PAG = txtJurosDANC.Text != "" ? (decimal?)Decimal.Parse(txtJurosDANC.Text) : null;
                            varTb38.VR_MUL_PAG = txtMultaDANC.Text != "" ? (decimal?)Decimal.Parse(txtMultaDANC.Text) : null;
                            varTb38.VR_DES_PAG = txtDesctoDANC.Text != "" ? (decimal?)Decimal.Parse(txtDesctoDANC.Text) : null;
                            varTb38.VR_PAG = txtPagoDANC.Text != "" ? (decimal?)Decimal.Parse(txtPagoDANC.Text) : null;
                            varTb38.DT_ALT_REGISTRO = DateTime.Now;
                            varTb38.IC_SIT_DOC = "Q";
                            varTb38.FL_ORIGEM_PGTO = "C";
                            varTb38.VR_TOT_DOC = txtVlTituloDANC.Text != "" ? Decimal.Parse(txtVlTituloDANC.Text) : 0; 

                            TB38_CTA_PAGAR.SaveOrUpdate(varTb38, true);
                            tb296.VR_DOCU = txtVlTituloDANC.Text != "" ? Decimal.Parse(txtVlTituloDANC.Text) : 0; 
                            tb296.VR_MUL_DOC = txtMultaDANC.Text != "" ? (decimal?)Decimal.Parse(txtMultaDANC.Text) : null;
                            tb296.VR_JUR_DOC = txtJurosDANC.Text != "" ? (decimal?)Decimal.Parse(txtJurosDANC.Text) : null;
                            tb296.VR_DES_DOC = txtDesctoDANC.Text != "" ? (decimal?)Decimal.Parse(txtDesctoDANC.Text) : null;
                            tb296.VR_ADC_DOC = txtAdiciDANC.Text != "" ? (decimal?)Decimal.Parse(txtAdiciDANC.Text) : null;
                            tb296.VR_LIQ_DOC = txtPagoDANC.Text != "" ? (decimal?)Decimal.Parse(txtPagoDANC.Text) : null;
                            tb296.TP_OPER_CAIXA = "C";

                            TB296_CAIXA_MOVIMENTO.SaveOrUpdate(tb296, true);
                        }
                        else
                        {
                            varTb38.VR_JUR_PAG = null;
                            varTb38.VR_MUL_PAG = null;
                            varTb38.VR_DES_PAG = null;
                            varTb38.VR_PAG = null;
                            varTb38.DT_ALT_REGISTRO = DateTime.Now;
                            varTb38.IC_SIT_DOC = "A";
                            varTb38.FL_ORIGEM_PGTO = "C";

                            varTb38.DT_REC_DOC = null;

                            tb296.FLA_SITU_DOC = "C";
                            tb296.DE_MOTIV_CANCEL = txtMotivo.Text;
                            TB38_CTA_PAGAR.SaveOrUpdate(varTb38, true);
                        }

                        #region Deletar ocorrências da TB156
                        var ocoTb156 = (from iTb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                                        where iTb156.CO_CAIXA_MOVIMENTO == coSeqMovto
                                        select iTb156).ToList();

                        foreach (var viTb156 in ocoTb156)
                        {
                            GestorEntities.Delete(viTb156, true);
                        }
                        #endregion
                        if (ddlNovaCond.SelectedValue == "A")
                        {
                            foreach (GridViewRow linha in grdFormPag.Rows)
                            {
                                if (((TextBox)linha.Cells[5].FindControl("txtValorNC")).Text != "")
                                {
                                    if ((((TextBox)linha.Cells[4].FindControl("txtQtdeNC")).Text == "") && (int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value) != 5))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser informada.");
                                        return;
                                    }

                                    TB156_FormaPagamento tb156 = new TB156_FormaPagamento();

                                    tb156.VR_RECEBIDO = Decimal.Parse(((TextBox)linha.Cells[5].FindControl("txtValorNC")).Text);
                                    tb156.DE_OBS = ((TextBox)linha.Cells[6].FindControl("txtObservacaoNC")).Text != "" ? ((TextBox)linha.Cells[6].FindControl("txtObservacaoNC")).Text : null;
                                    tb156.CO_TIPO_REC = int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value);

                                    if (int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCO_TIPO_REC")).Value) != 5)
                                    {
                                        if (int.Parse(((TextBox)linha.Cells[4].FindControl("txtQtdeNC")).Text) == 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser informada.");
                                            return;
                                        }

                                        tb156.NU_QTDE = int.Parse(((TextBox)linha.Cells[4].FindControl("txtQtdeNC")).Text);
                                    }
                                    else
                                        tb156.NU_QTDE = 0;

                                    tb156.CO_CAIXA_MOVIMENTO = tb296.CO_SEQMOV_CAIXA;

                                    TB156_FormaPagamento.SaveOrUpdate(tb156, true);
                                }
                            }
                        }
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número da conta não foi encontrado.");
                        return;
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Número de Caixa inexistente.");
                return;
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Operação Realizada com sucesso", Request.Url.AbsoluteUri);
        }



        #region Carrega Dropdown
        private void CarregaFuncionarios()
        {
            ddlNomeCaixa.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                       where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                                       && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S")
                                       && admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(a => a.NO_COL);

            ddlNomeCaixa.DataTextField = "NO_COL";
            ddlNomeCaixa.DataValueField = "CO_COL";
            ddlNomeCaixa.DataBind();
        }

        private void CarregaFuncionariosCaixa()
        {

            ddlFuncCaixa.Enabled = true;
            var data = ddlDtMovto.SelectedValue != "" ? ddlDtMovto.SelectedValue : null;
            DateTime data2 = DateTime.Now;
            if (data != null)
            {
                data2 = DateTime.Parse(data);
            }
            var cx2 = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
            ddlFuncCaixa.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                       join cx in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros() on tb03.CO_COL equals cx.CO_COLABOR_CAIXA 
                                       where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                                       && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S")
                                       && (admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO)
                                       && (cx2 != 0 ? cx.CO_CAIXA == cx2 : 0 == 0)
                                       && (data != null ? cx.DT_MOVIMENTO == data2 : 0 == 0)
                                       select new { tb03.CO_COL, tb03.NO_COL }).Distinct().OrderBy(a => a.NO_COL);

            ddlFuncCaixa.DataTextField = "NO_COL";
            ddlFuncCaixa.DataValueField = "CO_COL";
            ddlFuncCaixa.DataBind();
            ddlFuncCaixa.Items.Insert(0, new ListItem("Todos", ""));

            DateTime? dtMovtoCaixa = ddlDtMovto.SelectedValue != "" ? (DateTime?)DateTime.Parse(ddlDtMovto.SelectedValue) : null;
            int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
            int coColCaixa = ddlFuncCaixa.SelectedValue != "" ? int.Parse(ddlFuncCaixa.SelectedValue) : 0;
            
            MontaGridMovimento(dtMovtoCaixa, coCaixa, coColCaixa, ddlTpMovimento.SelectedValue);
        }

        private void CarregaDatas()
        {
            ddlDtMovto.Items.Clear();
            ddlDtMovto.Items.Insert(0, new ListItem("Todas", ""));
            var dtMovtos = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                            where tb296.CO_EMP.Equals(LoginAuxili.CO_EMP) 
                            select new { tb296.DT_MOVIMENTO }).Distinct().OrderBy(c => c.DT_MOVIMENTO);

            string data_atual = "";

            foreach (var data in dtMovtos.Distinct().OrderBy(c => c.DT_MOVIMENTO))
            {
                if (data.DT_MOVIMENTO.ToString("dd/MM/yyyy") != data_atual)
                {
                    ddlDtMovto.Items.Add(new ListItem(data.DT_MOVIMENTO.ToString("dd/MM/yyyy"), data.DT_MOVIMENTO.ToString("dd/MM/yyyy")));
                    data_atual = data.DT_MOVIMENTO.ToString("dd/MM/yyyy");
                }
            }
            CarregaCaixas();
            
           
           

        }

        private void CarregaCaixas()
        {
            if (ddlDtMovto.SelectedValue != "")
            {
                ddlCaixa.Enabled = true;
                ddlCaixa.Items.Clear();
                var data = ddlDtMovto.SelectedValue != null ? ddlDtMovto.SelectedValue : null;
                DateTime data2 = DateTime.Now;
                if (data != null)
                {
                     data2 =  DateTime.Parse(data);
                }
                var caixas = (from pc in TB113_PARAM_CAIXA.RetornaTodosRegistros()
                              join cm in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                              on pc.CO_CAIXA equals cm.CO_CAIXA

                              where (data != null ? cm.DT_MOVIMENTO == data2 : 0 == 0)
                              && (pc.CO_FLAG_USO_CAIXA.Equals("A"))
                              && (pc.CO_SITU_CAIXA.Equals("A"))
                              && (pc.CO_EMP.Equals(LoginAuxili.CO_EMP))

                              select new { pc.DE_CAIXA, pc.CO_CAIXA }).Distinct();

                foreach (var cxa in caixas)
                {
                    ddlCaixa.Items.Add(new ListItem(cxa.DE_CAIXA, cxa.CO_CAIXA.ToString()));
                }
               
                ddlCaixa.Items.Insert(0, new ListItem("Todas", ""));
                CarregaFuncionariosCaixa();
               
            }
            else
            {
                ddlCaixa.Enabled = true;
                ddlCaixa.Items.Clear();
                var data = ddlDtMovto.SelectedValue != null ? ddlDtMovto.SelectedValue : null;
                if (data != null && data != "")
                {
                    DateTime data2 = DateTime.Parse(data);
                }
                var caixas = (from pc in TB113_PARAM_CAIXA.RetornaTodosRegistros()
                              join cm in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                              on pc.CO_CAIXA equals cm.CO_CAIXA

                              where (pc.CO_FLAG_USO_CAIXA.Equals("A"))
                              && (pc.CO_SITU_CAIXA.Equals("A"))
                              && (pc.CO_EMP.Equals(LoginAuxili.CO_EMP))

                              select new { pc.DE_CAIXA, pc.CO_CAIXA }).Distinct();

                foreach (var cxa in caixas)
                {
                    ddlCaixa.Items.Add(new ListItem(cxa.DE_CAIXA, cxa.CO_CAIXA.ToString()));
                }
                 ddlCaixa.Items.Insert(0, new ListItem("Todas", ""));
                 CarregaFuncionariosCaixa();
               
                
                     }

        }
        #endregion



        #region Métodos "SelectIndexChanged"
        protected void ddlDataMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {            
            CarregaCaixas();
            divGrdContratos.Attributes.Remove("display");
            divGrdContratos.Attributes.Add("display", "block");
            divTelaExportacaoCarregando.Attributes.Remove("display");
            divTelaExportacaoCarregando.Attributes.Add("display","none");
        }

        protected void ddlCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionariosCaixa();
            
       }

        protected void ddlFuncionarioCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime? dtMovtoCaixa = ddlDtMovto.SelectedValue != "" ? (DateTime?)DateTime.Parse(ddlDtMovto.SelectedValue) : null;
            int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
            int coColCaixa = ddlFuncCaixa.SelectedValue != "" ? int.Parse(ddlFuncCaixa.SelectedValue) : 0;
            
            MontaGridMovimento(dtMovtoCaixa, coCaixa, coColCaixa, ddlTpMovimento.SelectedValue);
           
        }

        protected void ddlTipoMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime? dtMovtoCaixa = ddlDtMovto.SelectedValue != "" ? (DateTime?)DateTime.Parse(ddlDtMovto.SelectedValue) : null;
            int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
            int coColCaixa = ddlFuncCaixa.SelectedValue != "" ? int.Parse(ddlFuncCaixa.SelectedValue) : 0;
            MontaGridMovimento(dtMovtoCaixa, coCaixa, coColCaixa, ddlTpMovimento.SelectedValue);
            
        }

        protected void ddlNomeCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        #endregion



        protected void Autenticar_Click(object sender, EventArgs e)
        {
            string strSenhaMD5 = LoginAuxili.GerarMD5(txtSenha.Text);
            string senha = txtSenha.Text;


            var varTb295 = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                            where admUsuario.CO_EMP == tb03.CO_EMP && admUsuario.FLA_MANUT_CAIXA == "S"
                            && (admUsuario.TipoUsuario == "F" || admUsuario.TipoUsuario == "S")
                            && admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                            && admUsuario.desSenha == strSenhaMD5
                            select new { admUsuario.CodUsuario }).FirstOrDefault();

            if (varTb295 != null)
            {
                AuxiliPagina.EnvioMensagemSucesso(this, "Autenticação Efetuada com Sucesso!");
                 //ddlCaixa.Enabled = true;
                ddlDtMovto.Enabled = true;
                //ddlFuncCaixa.Enabled = true;
                grdContratos.Enabled = true;
                ddlTpMovimento.Enabled = true;
                
                //CarregaCaixas();
                CarregaDatas();
               //CarregaFuncionariosCaixa();
                DateTime? dtMovtoCaixa = ddlDtMovto.SelectedValue != "" ? (DateTime?)DateTime.Parse(ddlDtMovto.SelectedValue) : null;
                int coCaixa = ddlCaixa.SelectedValue != "" ? int.Parse(ddlCaixa.SelectedValue) : 0;
                int coColCaixa = ddlFuncCaixa.SelectedValue != "" ? int.Parse(ddlFuncCaixa.SelectedValue) : 0;
                
                MontaGridMovimento(dtMovtoCaixa, coCaixa, coColCaixa, ddlTpMovimento.SelectedValue);
                
            }
            else
                MontaGridFormPagamento(0);
                AuxiliPagina.EnvioMensagemErro(this, "Senha incorreta.");
                
        }

        protected void grdContratos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DateTime dataVencto = DateTime.Parse(e.Row.Cells[5].Text);

                    //if (dataVencto < DateTime.Now)
                    //    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(242, 220, 219);
                    //else
                    //{
                        e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                        e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
                   // }
                }
                else
                {
                    e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
                }
            }
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            bool chkContrato = false;

            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        if (linha.Cells[1].Text == hdfCoSeqSelec.Value)
                        {
                            ((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked = false;
                            hdfCoSeqSelec.Value = "";
                        }
                    }
                }
            }

            // Varre cada linha do grid
            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        chkContrato = true;
                        hdfCoSeqSelec.Value = linha.Cells[1].Text;

                        MontaContratoSelecionado(int.Parse(linha.Cells[1].Text));
                    }
                }
            }

            if (!chkContrato)
                LimpaCampos();
        }

        protected void grdContratos_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (grdContratos.Rows.Count > 0)
            {
                DateTime dtCadas = DateTime.Parse(grdContratos.SelectedRow.Cells[4].Text);

                string strTpBenef = "";

                if (chkCredito.Checked)
                    strTpBenef = chkBenif1.Checked ? "A" : "C";
                else
                    strTpBenef = "F";

                MontaContratoSelecionado(grdContratos.SelectedRow.Cells[2].Text, int.Parse(grdContratos.SelectedRow.Cells[3].Text), dtCadas, strTpBenef);
            
              }*/
        }
           

        /// <summary>
        /// Método que preenche a gride de movimentos de acordo com os parâmetros informados
        /// </summary>
        /// <param name="dtMovtoCaixa">Data de movimento do caixa</param>
        /// <param name="coCaixa">Código do caixa</param>
        /// <param name="coColCaixa">Código do funcionário do caixa</param>
        /// <param name="tpMovto">Tipo de movimento do caixa</param>
        protected void MontaGridMovimento(DateTime? dtMovtoCaixa, int coCaixa, int coColCaixa, string tpMovto)
        {
            LimpaCampos();
            if (tpMovto == "T")
            {
                MontaGridFormPagamento(0);
               var listaContratos = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                      join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tb296.CO_CAIXA equals tb113.CO_CAIXA
                                      join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb296.CO_COLABOR_CAIXA equals tb03.CO_COL
                                      join tb47 in TB47_CTA_RECEB.RetornaTodosRegistros() on tb296.NU_DOC_CAIXA equals tb47.NU_DOC
                                      where (tb296.CO_EMP == LoginAuxili.CO_EMP)
                                      && (dtMovtoCaixa != null ? (tb296.DT_MOVIMENTO.Year == dtMovtoCaixa.Value.Year && tb296.DT_MOVIMENTO.Month == dtMovtoCaixa.Value.Month &&
                                      tb296.DT_MOVIMENTO.Day == dtMovtoCaixa.Value.Day) : 0 == 0)
                                      && (coCaixa != 0 ? tb296.CO_CAIXA == coCaixa : 0 == 0)
                                      && (coColCaixa != 0 ? tb296.CO_COLABOR_CAIXA == coColCaixa : 0 == 0)
                                      && (tpMovto != "T" ? tb296.TP_OPER_CAIXA == tpMovto : 0 == 0)
                                      select new
                                      {
                                          Tipo = tb296.TP_OPER_CAIXA == "C" ? "REC" : "PAG",
                                          Documento = tb296.NU_DOC_CAIXA,
                                          Parcela = tb296.NU_PAR_DOC_CAIXA,
                                          DataDocto = tb296.DT_DOC_CAIXA,
                                          Valor = tb296.VR_DOCU,
                                          ValorPago = tb296.VR_LIQ_DOC,
                                          Caixa = tb113.DE_CAIXA,
                                          FuncCaixa = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                          Sequencial = tb296.CO_SEQMOV_CAIXA,
                                          DataVencto = tb296.DT_VEN_DOC,
                                          Responsavel = tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(3, ".").Insert(7, ".").Insert(11, "-"),
                                          TipoDocto = tb47.TB086_TIPO_DOC.DES_TIPO_DOC,
                                          HistoDocumento = tb47.DE_COM_HIST,
                                          CondiçaoAtual = tb296.FLA_SITU_DOC,
                                          coCaixa = tb296.CO_CAIXA
                                      }).ToList().OrderBy(p => p.Sequencial);

                    grdContratos.DataSource = listaContratos;
            }
            else if (tpMovto == "C")
            {
                MontaGridFormPagamento(0);
                var listaContratos = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                      join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tb296.CO_CAIXA equals tb113.CO_CAIXA
                                      join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb296.CO_COLABOR_CAIXA equals tb03.CO_COL
                                      join tb47 in TB47_CTA_RECEB.RetornaTodosRegistros() on tb296.NU_DOC_CAIXA equals tb47.NU_DOC
                                      where (tb296.CO_EMP == LoginAuxili.CO_EMP)
                                      && (dtMovtoCaixa != null ? (tb296.DT_MOVIMENTO.Year == dtMovtoCaixa.Value.Year && tb296.DT_MOVIMENTO.Month == dtMovtoCaixa.Value.Month &&
                                      tb296.DT_MOVIMENTO.Day == dtMovtoCaixa.Value.Day) : 0 == 0)
                                      && (coCaixa != 0 ? tb296.CO_CAIXA == coCaixa : 0 == 0)
                                      && (coColCaixa != 0 ? tb296.CO_COLABOR_CAIXA == coColCaixa : 0 == 0)
                                      && (tpMovto != "T" ? tb296.TP_OPER_CAIXA == tpMovto : 0 == 0)
                                      select new
                                      {
                                          Tipo = tb296.TP_OPER_CAIXA == "C" ? "REC" : "PAG",
                                          Documento = tb296.NU_DOC_CAIXA,
                                          Parcela = tb296.NU_PAR_DOC_CAIXA,
                                          DataDocto = tb296.DT_DOC_CAIXA,
                                          Valor = tb296.VR_DOCU,
                                          ValorPago = tb296.VR_LIQ_DOC,
                                          Caixa = tb113.DE_CAIXA,
                                          FuncCaixa = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                          Sequencial = tb296.CO_SEQMOV_CAIXA,
                                          DataVencto = tb296.DT_VEN_DOC,
                                          Responsavel = tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(3, ".").Insert(7, ".").Insert(11, "-"),
                                          TipoDocto = tb47.TB086_TIPO_DOC.DES_TIPO_DOC,
                                          HistoDocumento = tb47.DE_COM_HIST,
                                          CondiçaoAtual = tb296.FLA_SITU_DOC,
                                          coCaixa = tb296.CO_CAIXA
                                          
                                      }).ToList().OrderBy(p => p.Sequencial);

                    grdContratos.DataSource = listaContratos;
            }
            else if (tpMovto == "D")
            {
                MontaGridFormPagamento(0);
                var listaContratos = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                      join tb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros() on tb296.TB295_CAIXA.CO_CAIXA equals tb113.CO_CAIXA
                                      join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb296.TB295_CAIXA.CO_COLABOR_CAIXA equals tb03.CO_COL
                                      join tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros() on tb296.NU_DOC_CAIXA equals tb38.NU_DOC
                                      where (tb296.CO_EMP == LoginAuxili.CO_EMP)
                                      && (dtMovtoCaixa != null ? (tb296.DT_MOVIMENTO.Year == dtMovtoCaixa.Value.Year && tb296.DT_MOVIMENTO.Month == dtMovtoCaixa.Value.Month &&
                                      tb296.DT_MOVIMENTO.Day == dtMovtoCaixa.Value.Day) : dtMovtoCaixa == null)
                                      && (coCaixa != 0 ? tb296.CO_CAIXA == coCaixa : 0 == 0)
                                      && (coColCaixa != 0 ? tb296.CO_COLABOR_CAIXA == coColCaixa : 0 == 0)
                                      && (tpMovto != "T" ? tb296.TP_OPER_CAIXA == tpMovto : 0 == 0)
                                      select new
                                      {
                                          Tipo = tb296.TP_OPER_CAIXA == "C" ? "REC" : "PAG",
                                          Documento = tb296.NU_DOC_CAIXA,
                                          Parcela = tb296.NU_PAR_DOC_CAIXA,
                                          DataDocto = tb296.DT_DOC_CAIXA,
                                          Valor = tb296.VR_DOCU,
                                          ValorPago = tb296.VR_LIQ_DOC,
                                          Caixa = tb113.DE_CAIXA,
                                          FuncCaixa = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                          Sequencial = tb296.CO_SEQMOV_CAIXA,
                                          DataVencto = tb296.DT_VEN_DOC,
                                          Responsavel = tb38.TB41_FORNEC.CO_CPFCGC_FORN.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-"),
                                          TipoDocto = tb38.TB086_TIPO_DOC.DES_TIPO_DOC,
                                          HistoDocumento = tb38.DE_COM_HIST,
                                          CondiçaoAtual = tb296.FLA_SITU_DOC,
                                          coCaixa = tb296.CO_CAIXA
                                      }).ToList().OrderBy(p => p.Sequencial);

                grdContratos.DataSource = listaContratos;
            }
            
         

            grdContratos.DataBind();
        }

        /// <summary>
        /// Carrega informações do movimento selecionado
        /// </summary>
        /// <param name="CO_SEQMOV_CAIXA">Id sequencial da movimentação</param>
        private void MontaContratoSelecionado(int CO_SEQMOV_CAIXA)
        {
            LimpaCampos();

            if (ddlTpMovimento.SelectedValue == "C")
            {
                var conta = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                             where tb296.CO_SEQMOV_CAIXA == CO_SEQMOV_CAIXA
                             select new
                             {
                                 tb296.VR_ADC_DOC, tb296.VR_DES_DOC, tb296.VR_JUR_DOC, tb296.VR_MUL_DOC, tb296.VR_LIQ_DOC,tb296.FLA_SITU_DOC,tb296.VR_DOCU

                             }).FirstOrDefault();

                if (conta != null)
                {

                    txtVlTitulo.Text = conta.VR_DOCU.ToString("#,##0.00");
                    txtMultaDA.Text = conta.VR_MUL_DOC != null ? conta.VR_MUL_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtJurosDA.Text = conta.VR_JUR_DOC != null ? conta.VR_JUR_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtAdiciDA.Text = conta.VR_ADC_DOC != null ? conta.VR_ADC_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtDesctoDA.Text = conta.VR_DES_DOC != null ? conta.VR_DES_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtPagoDA.Text = conta.VR_LIQ_DOC != null ? conta.VR_LIQ_DOC.Value.ToString("#,##0.00") : "0,00";

                    txtVlTituloDANC.Text = conta.VR_DOCU.ToString("#,##0.00");
                    txtMultaDANC.Text = conta.VR_MUL_DOC != null ? conta.VR_MUL_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtJurosDANC.Text = conta.VR_JUR_DOC != null ? conta.VR_JUR_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtAdiciDANC.Text = conta.VR_ADC_DOC != null ? conta.VR_ADC_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtDesctoDANC.Text = conta.VR_DES_DOC != null ? conta.VR_DES_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtPagoDANC.Text = conta.VR_LIQ_DOC != null ? conta.VR_LIQ_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtCondAtual.Text = conta.FLA_SITU_DOC == "A" ? "Ativo" : "Cancelado";
                   
                    if (conta.FLA_SITU_DOC == "A")
                    {
                        ListItem[] itens = new ListItem[]
                        {
                            new ListItem("Ativo","A"),
                            new ListItem("Cancelado","C")
                        };

                        ddlNovaCond.Items.AddRange(itens);

                    }
                    else
                    {
                        ListItem[] itens = new ListItem[]
                        {
                            new ListItem("Cancelado","C"),
                            new ListItem("Ativo","A")
                        };

                        ddlNovaCond.Items.AddRange(itens);
                    }

                    MontaGridFormPagamento(CO_SEQMOV_CAIXA);

                             
                }
            }
            else
            {
                var conta = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                             where tb296.CO_SEQMOV_CAIXA == CO_SEQMOV_CAIXA
                             select new
                             {
                                 tb296.VR_ADC_DOC,
                                 tb296.VR_DES_DOC,
                                 tb296.VR_JUR_DOC,
                                 tb296.VR_MUL_DOC,
                                 tb296.VR_LIQ_DOC,
                                 tb296.FLA_SITU_DOC,
                                 tb296.VR_DOCU,
                                

                             }).FirstOrDefault();

                if (conta != null)
                {

                    txtVlTitulo.Text = conta.VR_DOCU.ToString("#,##0.00");
                    txtMultaDA.Text = conta.VR_MUL_DOC != null ? conta.VR_MUL_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtJurosDA.Text = conta.VR_JUR_DOC != null ? conta.VR_JUR_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtAdiciDA.Text = conta.VR_ADC_DOC != null ? conta.VR_ADC_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtDesctoDA.Text = conta.VR_DES_DOC != null ? conta.VR_DES_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtPagoDA.Text = conta.VR_LIQ_DOC != null ? conta.VR_LIQ_DOC.Value.ToString("#,##0.00") : "0,00";

                    txtVlTituloDANC.Text = conta.VR_DOCU.ToString("#,##0.00");
                    txtMultaDANC.Text = conta.VR_MUL_DOC != null ? conta.VR_MUL_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtJurosDANC.Text = conta.VR_JUR_DOC != null ? conta.VR_JUR_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtAdiciDANC.Text = conta.VR_ADC_DOC != null ? conta.VR_ADC_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtDesctoDANC.Text = conta.VR_DES_DOC != null ? conta.VR_DES_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtPagoDANC.Text = conta.VR_LIQ_DOC != null ? conta.VR_LIQ_DOC.Value.ToString("#,##0.00") : "0,00";
                    txtCondAtual.Text = conta.FLA_SITU_DOC == "A" ? "Ativo" : "Cancelado";

                    if (conta.FLA_SITU_DOC == "A")
                    {
                        ListItem[] itens = new ListItem[]
                        {
                            new ListItem("Ativo","A"),
                            new ListItem("Cancelado","C")
                        };

                        ddlNovaCond.Items.AddRange(itens);

                    }
                    else
                    {
                        ListItem[] itens = new ListItem[]
                        {
                            new ListItem("Cancelado","C"),
                            new ListItem("Ativo","A")
                        };

                        ddlNovaCond.Items.AddRange(itens);
                    }



                    txtMultaDANC.AutoPostBack = true;
                    txtJurosDANC.AutoPostBack = true;
                    txtAdiciDANC.AutoPostBack = true;
                    txtDesctoDANC.AutoPostBack = true;


                    MontaGridFormPagamento(CO_SEQMOV_CAIXA);


                }
                
             

            }
            

      
        }

        /// <summary>
        /// Método que carrega a gride de forma de pagamento
        /// </summary>
        /// <param name="CO_SEQMOV_CAIXA">Id da movimentação</param>
        protected void MontaGridFormPagamento(int CO_SEQMOV_CAIXA)
        {
            var resultado = (from tb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                            where tb118.CLA_TIPO_MOVIM == "T" || tb118.CLA_TIPO_MOVIM == ddlTpMovimento.SelectedValue
                            select new FormaPagamento
                            { 
                                CO_TIPO_REC = tb118.CO_TIPO_REC,
                                DE_SIG_RECEB = tb118.DE_SIG_RECEB,
                                DE_RECEBIMENTO = tb118.DE_RECEBIMENTO 
                            }).ToList();

            foreach (var item in resultado)
            {
                var tb156 = (from iTb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                             where iTb156.CO_CAIXA_MOVIMENTO == CO_SEQMOV_CAIXA && iTb156.CO_TIPO_REC == item.CO_TIPO_REC
                             select new { iTb156.DE_OBS, iTb156.VR_RECEBIDO, iTb156.NU_QTDE }).FirstOrDefault();

                if (tb156 != null)
                {
                    item.VR_RECEBIDO = tb156.VR_RECEBIDO;
                    item.NU_QTDE = tb156.NU_QTDE;
                    item.DE_OBS = tb156.DE_OBS;
                }
             }

            grdFormPag.DataKeyNames = new string[] { "CO_TIPO_REC" };

            grdFormPag.DataSource = resultado;
            grdFormPag.DataBind();
        }

        /// <summary>
        /// Método que limpa os campos informados
        /// </summary>
        protected void LimpaCampos()
        {
            txtAdiciDA.Text = txtAdiciDANC.Text = txtCondAtual.Text = txtDesctoDA.Text = txtDesctoDANC.Text = txtJurosDA.Text = txtJurosDANC.Text = txtMultaDA.Text =
                txtMultaDANC.Text = txtPagoDA.Text = txtPagoDANC.Text = "";
            grdFormPag.DataSource = null;
            grdFormPag.DataBind();
            MontaGridFormPagamento(0);
        }


        #region Funções de textchanged dos valores

        protected void txtMultaDANC_TextChanged(object sender, EventArgs e)
        {
            decimal dcmValAdcInf = 0;
            decimal dcmAboMul = 0;
            decimal dcmAboCor = 0;
            decimal dcmAboDes = 0;
            int coCaixa = 0;

            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        coCaixa = int.Parse(linha.Cells[14].Text);
                    }
                }
            }


            var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                            select new
                            {
                                tb295.VR_PERCENTUAL_ABONO_MULTA,
                                tb295.VR_PERCENTUAL_ABONO_DESCONTO,
                                tb295.VR_PERCENTUAL_ABONO_CORRECAO
                            }).FirstOrDefault();

            if (varTb295 != null)
            {
                if ((varTb295.VR_PERCENTUAL_ABONO_MULTA != null) && (decimal.TryParse(txtMultaDANC.Text, out dcmAboMul)))
                {
                    if (decimal.Parse(txtMultaDA.Text) > 0)
                    {
                        dcmAboMul = decimal.Parse(txtMultaDA.Text) - (decimal.Parse(txtMultaDA.Text) * Decimal.Parse(String.Format("{0:N}", varTb295.VR_PERCENTUAL_ABONO_MULTA)) / 100);

                        if (decimal.Parse(txtMultaDANC.Text) < dcmAboMul)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Abono de Multa não pode ser superior ao porcentual permitido ao usuário caixa.");
                            txtMultaDANC.Text = "";
                            txtMultaDANC.Text = txtMultaDA.Text;
                            dcmAboMul = decimal.Parse(txtMultaDA.Text);
                            return;
                        }
                        else
                            dcmAboMul = decimal.Parse(txtMultaDANC.Text);
                    }
                }
                else
                {
                    if (!decimal.TryParse(txtMultaDANC.Text, out dcmAboMul))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número não válido.");
                        txtMultaDANC.Text = txtMultaDA.Text;
                        //return;
                    }
                }

                if (decimal.TryParse(txtJurosDANC.Text, out dcmAboCor))
                    dcmAboCor = decimal.Parse(txtJurosDANC.Text);
                else
                    dcmAboCor = 0;

                if (decimal.TryParse(txtAdiciDANC.Text, out dcmValAdcInf))
                    dcmValAdcInf = decimal.Parse(txtAdiciDANC.Text);
                else
                    dcmValAdcInf = 0;

                if (decimal.TryParse(txtDesctoDANC.Text, out dcmAboDes))
                    dcmAboDes = decimal.Parse(txtDesctoDANC.Text);
                else
                    dcmAboDes = 0;

                txtPagoDANC.Text = String.Format("{0:N}", Decimal.Parse(txtVlTituloDANC.Text) + dcmAboMul + dcmAboCor - dcmAboDes + dcmValAdcInf);
            }
        }

        protected void txtJurosDANC_TextChanged(object sender, EventArgs e)
        {
            decimal valAdcInf = 0;
            decimal aboMul = 0;
            decimal aboCor = 0;
            decimal aboDes = 0;
            int coCaixa = 0;

            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        coCaixa = int.Parse(linha.Cells[14].Text);
                    }
                }
            }


            var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                            select new
                            {
                                tb295.VR_PERCENTUAL_ABONO_MULTA,
                                tb295.VR_PERCENTUAL_ABONO_DESCONTO,
                                tb295.VR_PERCENTUAL_ABONO_CORRECAO
                            }).FirstOrDefault();

            if (varTb295 != null)
            {
                if ((varTb295.VR_PERCENTUAL_ABONO_CORRECAO != null) && (decimal.TryParse(txtJurosDANC.Text, out aboCor)))
                {
                    if (decimal.Parse(txtJurosDA.Text) > 0)
                    {
                        aboCor = decimal.Parse(txtJurosDA.Text) - (decimal.Parse(txtJurosDA.Text) * Decimal.Parse(String.Format("{0:N}", varTb295.VR_PERCENTUAL_ABONO_CORRECAO)) / 100);

                        if (decimal.Parse(txtJurosDANC.Text) < aboCor)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Abono de Correção não pode ser superior ao porcentual permitido ao usuário caixa.");
                            txtJurosDANC.Text = txtJurosDA.Text;
                            aboCor = decimal.Parse(txtJurosDA.Text);
                            //return;
                        }
                        else
                            aboCor = decimal.Parse(txtJurosDANC.Text);
                    }
                }
                else
                {
                    if (!decimal.TryParse(txtJurosDANC.Text, out aboCor))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número não válido.");
                        txtJurosDANC.Text = txtJurosDA.Text;
                        // return;
                    }
                }

                if (decimal.TryParse(txtMultaDANC.Text, out aboMul))
                    aboMul = decimal.Parse(txtMultaDANC.Text);
                else
                    aboMul = 0;

                if (decimal.TryParse(txtAdiciDANC.Text, out valAdcInf))
                    valAdcInf = decimal.Parse(txtAdiciDANC.Text);
                else
                    valAdcInf = 0;

                if (decimal.TryParse(txtDesctoDANC.Text, out aboDes))
                    aboDes = decimal.Parse(txtDesctoDANC.Text);
                else
                    aboDes = 0;

                txtPagoDANC.Text = String.Format("{0:N}", Decimal.Parse(txtVlTituloDANC.Text) + aboMul + aboCor - aboDes + valAdcInf);
            }
        }

        protected void txtAdiciDANC_TextChanged(object sender, EventArgs e)
        {
            decimal valAdcInf = 0;
            decimal aboMul = 0;
            decimal aboCor = 0;
            decimal aboDes = 0;

            if (decimal.TryParse(txtAdiciDANC.Text, out valAdcInf))
                valAdcInf = decimal.Parse(txtAdiciDANC.Text);
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor Adicionado informado não pode ser gerado.");
                txtAdiciDANC.Text = txtAdiciDA.Text;
                //return;
            }

            if (decimal.TryParse(txtMultaDANC.Text, out aboMul))
                aboMul = decimal.Parse(txtMultaDANC.Text);
            else
                aboMul = 0;

            if (decimal.TryParse(txtJurosDANC.Text, out aboCor))
                aboCor = decimal.Parse(txtJurosDANC.Text);
            else
                aboCor = 0;

            if (decimal.TryParse(txtDesctoDANC.Text, out aboDes))
                aboDes = decimal.Parse(txtDesctoDANC.Text);
            else
                aboDes = 0;

            txtPagoDANC.Text = String.Format("{0:N}", Decimal.Parse(txtVlTituloDANC.Text) + aboMul + aboCor - aboDes + valAdcInf);
        }

        protected void txtDesctoDANC_TextChanged(object sender, EventArgs e)
        {
            decimal valAdcInf = 0;
            decimal aboMul = 0;
            decimal aboCor = 0;
            decimal aboDes = 0;

            int coCaixa = 0;

            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        coCaixa = int.Parse(linha.Cells[14].Text);
                    }
                }
            }


            var varTb295 = (from tb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where tb295.CO_CAIXA == coCaixa && tb295.DT_FECHAMENTO_CAIXA == null
                            select new
                            {
                                tb295.VR_PERCENTUAL_ABONO_MULTA,
                                tb295.VR_PERCENTUAL_ABONO_DESCONTO,
                                tb295.VR_PERCENTUAL_ABONO_CORRECAO
                            }).FirstOrDefault();

            if (varTb295 != null)
            {
                if ((varTb295.VR_PERCENTUAL_ABONO_DESCONTO != null) && (decimal.TryParse(txtDesctoDANC.Text, out aboDes)))
                {
                    if (decimal.Parse(txtDesctoDA.Text) > 0)
                    {
                        aboDes = (decimal.Parse(txtDesctoDA.Text) * Decimal.Parse(String.Format("{0:N}", varTb295.VR_PERCENTUAL_ABONO_MULTA)) / 100) + decimal.Parse(txtDesctoDA.Text);

                        if (decimal.Parse(txtDesctoDANC.Text) > aboDes)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Abono de Desconto não pode ser superior ao porcentual permitido ao usuário caixa.");
                            txtDesctoDANC.Text = txtDesctoDA.Text;
                            aboDes = decimal.Parse(txtDesctoDA.Text);
                            //return;
                        }
                        else
                            aboDes = decimal.Parse(txtDesctoDANC.Text);
                    }
                }
                else
                {
                    if (!decimal.TryParse(txtDesctoDANC.Text, out aboDes))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número não válido.");
                        txtDesctoDANC.Text = txtDesctoDA.Text;
                        //return;
                    }
                }

                if(decimal.Parse(txtDesctoDANC.Text) > decimal.Parse(txtPagoDANC.Text))
                {
                    txtDesctoDANC.Text = txtPagoDANC.Text;
                    aboDes = decimal.Parse(txtDesctoDANC.Text);
                }

                if (decimal.TryParse(txtMultaDANC.Text, out aboMul))
                    aboMul = decimal.Parse(txtMultaDANC.Text);
                else
                    aboMul = 0;

                if (decimal.TryParse(txtJurosDANC.Text, out aboCor))
                    aboCor = decimal.Parse(txtJurosDANC.Text);
                else
                    aboCor = 0;

                if (decimal.TryParse(txtAdiciDANC.Text, out valAdcInf))
                    valAdcInf = decimal.Parse(txtAdiciDANC.Text);
                else
                    valAdcInf = 0;

                txtPagoDANC.Text = String.Format("{0:N}", Decimal.Parse(txtVlTituloDANC.Text) + aboMul + aboCor - aboDes + valAdcInf);
            }
        }

        #endregion

        protected void ddlTxtCondAtual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNovaCond.SelectedValue == "C")
            {
                txtMotivo.Enabled = true;
            }
            else
            {
                txtMotivo.Enabled = false;
            }
        }
    }
   
    public class FormaPagamento
    {
        public int CO_TIPO_REC { get; set; }
        public string DE_SIG_RECEB { get; set; }
        public string DE_RECEBIMENTO { get; set; }
        public string DE_OBS { get; set; }
        public decimal? VR_RECEBIDO { get; set; }
        public int? NU_QTDE { get; set; }
    }
/*
    public class ListaMovimento
    {        
        public int Sequencial { get; set; }
        public string Caixa { get; set; }
        public string FuncCaixa { get; set; }
        public string Tipo { get; set; }
        public string Responsavel { get; set; }
        public string TipoDocto { get; set; }
        public string Documento { get; set; }
        public string HistoDocumento { get; set; }
        public int? Parcela { get; set; }
        public DateTime? DataDocto { get; set; }
        public DateTime? DataVencto { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValorPago { get; set; }
    }*/
}