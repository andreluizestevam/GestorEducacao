//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: FECHAMENTO DE CAIXA
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
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5100_CtrlTesouraria;
using System.Web;
using System.Reflection;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5105_FechamentoCaixa
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um caixa.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

            imgRpt.Src = "/Library/IMG/Gestor_IcoImpres.ico";
            lblRpt.Text = "INCONSISTÊNCIAS";

            if (Session["ApresentaRelatorio"] != null)
            {
                if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1 && LinkButton1.Enabled && int.Parse(hdVerifRelac.Value) == 1)
                {
                    AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                    //----------------> Limpa a var de sessão com o url do relatório.
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
                    hdVerifRelac.Value = "0";
                    //----------------> Limpa a ref da url utilizada para carregar o relatório.
                    PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                }
            }

            if (!Page.IsPostBack)
            {
                hdVerifPagCaixa.Value = hdVerifRelac.Value = hdVerifRecCaixa.Value = "0";
                CarregaFormulario();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB295_CAIXA tb295 = RetornaEntidade();

            if (tb295 != null)
            {
                tb295.TB03_COLABORReference.Load();
                tb295.TB03_COLABORReference.Load();

                MontaGridAporteSangria(tb295.CO_EMP, tb295.CO_CAIXA, tb295.DT_MOVIMENTO, tb295.TB03_COLABOR.CO_COL);

                var tb113 = (from lTb113 in TB113_PARAM_CAIXA.RetornaTodosRegistros()
                             where lTb113.TB25_EMPRESA.CO_EMP == tb295.TB03_COLABOR.CO_EMP && lTb113.CO_CAIXA == tb295.CO_CAIXA
                             select new { lTb113.DE_CAIXA, lTb113.CO_SIGLA_CAIXA }).FirstOrDefault();

                txtNomeCaixa.Text = tb113.DE_CAIXA;
                txtSiglaCaixa.Text = tb113.CO_SIGLA_CAIXA;
                txtDataMovto.Text = tb295.DT_MOVIMENTO.ToString("dd/MM/yyyy");
                txtNoOperCaixa.Text = tb295.TB03_COLABOR.NO_COL;
                txtValor.Text = tb295.VR_ABERTURA_CAIXA.ToString();

                divGrdFormPagRec.Visible = divgrdResulAnaRec.Visible = libtnNegociacao.Visible = divGrdFormPagPag.Visible = divgrdResulAnaPag.Visible = true;
                MontaGridFormPagamentoRec();
                MontaGridVeriPagamentoRec();
                MontaGridFormPagamentoPag();
                MontaGridVeriPagamentoPag();
            }
        }

        //====> 
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB295_CAIXA</returns>
        private TB295_CAIXA RetornaEntidade()
        {
            int coEmp = int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoEmp) != null ? QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoEmp) : "0");
            int coCaixa = int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("coCaixa") != null ? QueryStringAuxili.RetornaQueryStringPelaChave("coCaixa") : "0");
            int coColCaixa = int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("coColCaixa") != null ? QueryStringAuxili.RetornaQueryStringPelaChave("coColCaixa") : "0");
            string dtMov = QueryStringAuxili.RetornaQueryStringPelaChave("dtMov") != null ? QueryStringAuxili.RetornaQueryStringPelaChave("dtMov") : "01/01/1900";
            DateTime dataMovto = DateTime.Parse(dtMov);

            return TB295_CAIXA.RetornaPelaChavePrimaria(coEmp, coCaixa, coColCaixa, dataMovto);
        }

        /// <summary>
        /// Método que monta a grid de Forma de Pagamento "Recebimento"
        /// </summary>
        protected void MontaGridFormPagamentoRec()
        {
            grdFormPagRec.DataKeyNames = new string[] { "CO_TIPO_REC" };

            var tb118 = from lTb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                        select new { lTb118.CO_TIPO_REC, lTb118.DE_SIG_RECEB, lTb118.DE_RECEBIMENTO };

            DataTable Dt = new DataTable();

            Dt.Columns.Add("CO_TIPO_REC");

            Dt.Columns.Add("DE_SIG_RECEB");

            Dt.Columns.Add("DE_RECEBIMENTO");

            if (tb118 != null)
            {
                foreach (var item in tb118)
                {
                    Dt.Rows.Add(item.CO_TIPO_REC, item.DE_SIG_RECEB, item.DE_RECEBIMENTO);
                }
            }

            grdFormPagRec.DataSource = Dt;
            grdFormPagRec.DataBind();

            foreach (GridViewRow linha in grdFormPagRec.Rows)
            {
                if (int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCO_TIPO_REC")).Value) == 5)
                {
                    ((TextBox)linha.Cells[1].FindControl("txtQtdeFP")).Enabled = false;
                    ((TextBox)linha.Cells[1].FindControl("txtQtdeFP")).Text = "";
                    ((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text = "0,00";
                }
                else
                {
                    ((TextBox)linha.Cells[1].FindControl("txtQtdeFP")).Text = "0";
                    ((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text = "0,00";
                }
            }

            lblTotQtdRec.Text = "00";
            lblTotValRec.Text = "0,00";
        }

        /// <summary>
        /// Método que monta a grid de Forma de Pagamento "Pagamento"
        /// </summary>
        protected void MontaGridFormPagamentoPag()
        {
            grdFormPagPag.DataKeyNames = new string[] { "CO_TIPO_REC" };

            var tb118 = from lTb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                        select new { lTb118.CO_TIPO_REC, lTb118.DE_SIG_RECEB, lTb118.DE_RECEBIMENTO };

            DataTable Dt = new DataTable();

            Dt.Columns.Add("CO_TIPO_REC");

            Dt.Columns.Add("DE_SIG_RECEB");

            Dt.Columns.Add("DE_RECEBIMENTO");

            if (tb118 != null)
            {
                foreach (var item in tb118)
                {
                    Dt.Rows.Add(item.CO_TIPO_REC, item.DE_SIG_RECEB, item.DE_RECEBIMENTO);
                }
            }

            grdFormPagPag.DataSource = Dt;
            grdFormPagPag.DataBind();

            foreach (GridViewRow linha in grdFormPagPag.Rows)
            {
                if (int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCO_TIPO_REC")).Value) == 5)
                {
                    ((TextBox)linha.Cells[1].FindControl("txtQtdeFP")).Enabled = false;
                    ((TextBox)linha.Cells[1].FindControl("txtQtdeFP")).Text = "";
                    ((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text = "0,00";
                }
                else
                {
                    ((TextBox)linha.Cells[1].FindControl("txtQtdeFP")).Text = "0";
                    ((TextBox)linha.Cells[2].FindControl("txtValorFP")).Text = "0,00";
                }
            }

            lblTotQtdPag.Text = "00";
            lblTotValPag.Text = "0,00";
        }

        /// <summary>
        /// Método que monta a grid de Verificação de "Recebimento"
        /// </summary>
        protected void MontaGridVeriPagamentoRec()
        {
            grdResulAnaRec.DataKeyNames = new string[] { "CO_TIPO_REC" };

            var tb118 = from lTb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                        select new { lTb118.CO_TIPO_REC, lTb118.DE_SIG_RECEB, lTb118.DE_RECEBIMENTO };

            DataTable Dt = new DataTable();

            Dt.Columns.Add("CO_TIPO_REC");

            Dt.Columns.Add("DE_SIG_RECEB");

            Dt.Columns.Add("DE_RECEBIMENTO");

            if (tb118 != null)
            {
                foreach (var item in tb118)
                {
                    Dt.Rows.Add(item.CO_TIPO_REC, item.DE_SIG_RECEB, item.DE_RECEBIMENTO);
                }
            }

            grdResulAnaRec.DataSource = Dt;
            grdResulAnaRec.DataBind();
        }

        /// <summary>
        /// Método que monta a grid de Verificação de "Pagamento"
        /// </summary>
        protected void MontaGridVeriPagamentoPag()
        {
            grdResulAnaPag.DataKeyNames = new string[] { "CO_TIPO_REC" };

            var tb118 = from lTb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                        select new { lTb118.CO_TIPO_REC, lTb118.DE_SIG_RECEB, lTb118.DE_RECEBIMENTO };

            DataTable Dt = new DataTable();

            Dt.Columns.Add("CO_TIPO_REC");

            Dt.Columns.Add("DE_SIG_RECEB");

            Dt.Columns.Add("DE_RECEBIMENTO");

            if (tb118 != null)
            {
                foreach (var item in tb118)
                {
                    Dt.Rows.Add(item.CO_TIPO_REC, item.DE_SIG_RECEB, item.DE_RECEBIMENTO);
                }
            }

            grdResulAnaPag.DataSource = Dt;
            grdResulAnaPag.DataBind();
        }

        /// <summary>
        /// Método que monta a grid de Aporte/Sangria
        /// </summary>
        /// <param name="coEmp">Id da unidade</param>
        /// <param name="coCaixa">Id do caixa</param>
        /// <param name="dtMovto">Data da movimentação</param>
        /// <param name="coCol">Id do funcionário</param>
        protected void MontaGridAporteSangria(int coEmp, int coCaixa, DateTime dtMovto, int coCol)
        {
            grdAporteSangria.DataKeyNames = new string[] { "CO_OPER_CAIXA" };

            var apoSan = (from tb297 in TB297_OPER_CAIXA.RetornaTodosRegistros()
                          join admUsuario in ADMUSUARIO.RetornaTodosRegistros() on tb297.CO_USUARIO equals admUsuario.ideAdmUsuario
                          join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                          where tb297.TB295_CAIXA.CO_CAIXA == coCaixa && tb297.TB295_CAIXA.TB03_COLABOR.CO_EMP == coEmp
                          && tb297.TB295_CAIXA.TB03_COLABOR.CO_COL == coCol && tb297.TB295_CAIXA.DT_MOVIMENTO == dtMovto && admUsuario.CO_EMP == tb03.CO_EMP
                          select new
                          {
                              tb297.CO_OPER_CAIXA,
                              tb297.DT_CADASTRO,
                              tb297.HR_CADASTRO,
                              TIPO = tb297.FLA_TIPO == "A" ? "APORTE" : "SANGRIA",
                              tb297.VALOR,
                              tb297.TB118_TIPO_RECEB.DE_RECEBIMENTO,
                              CO_MAT_COL = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "-")
                          }).OrderBy(o => o.DT_CADASTRO);

            grdAporteSangria.DataSource = apoSan;
            grdAporteSangria.DataBind();

            if (grdAporteSangria.Rows.Count > 0)
            {
                decimal dcmValorTotSang = 0;
                int intQtdeTotSang = 0;
                decimal dcmValorTotApor = 0;
                int inQtdeTotApor = 0;

                foreach (GridViewRow grdApSa in grdAporteSangria.Rows)
                {
                    if (grdApSa.Cells[3].Text == "APORTE")
                    {
                        dcmValorTotApor = dcmValorTotApor + Decimal.Parse(grdApSa.Cells[4].Text);
                        inQtdeTotApor++;
                    }
                    else
                    {
                        dcmValorTotSang = dcmValorTotSang + Decimal.Parse(grdApSa.Cells[4].Text);
                        intQtdeTotSang++;
                    }
                }

                lblTotAporte.Text = "Total Aporte: " + inQtdeTotApor + " / ";
                lblValTotAporte.Text = String.Format("{0:N}", dcmValorTotApor);

                lblTotSangria.Text = "Total Sangria: " + intQtdeTotSang + " / ";
                lblValTotSangria.Text = String.Format("{0:N}", dcmValorTotSang);
            }
        }
        #endregion

        protected void btnConfirmaCaixa_Click(object sender, EventArgs e)
        {
            if (int.Parse(hdVerifRecCaixa.Value) == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário que todos os itens de Recebimento sejam aprovados.");
                return;
            }

            if (int.Parse(hdVerifPagCaixa.Value) == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário que todos os itens de Pagamento sejam aprovados.");
                return;
            }

            TB295_CAIXA tb295 = RetornaEntidade();

            int coCaixa = tb295.CO_CAIXA;

            var varTb295 = (from lTb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where lTb295.CO_EMP == LoginAuxili.CO_EMP && lTb295.CO_CAIXA == coCaixa
                            && lTb295.DT_FECHAMENTO_CAIXA == null && lTb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP
                            select lTb295).FirstOrDefault();

            if (varTb295 == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Caixa não foi encontrado.");
                return;
            }
            else
            {
                var varTb296 = (from tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                where tb296.TB295_CAIXA.TB03_COLABOR.CO_EMP == varTb295.CO_EMP && tb296.TB295_CAIXA.CO_CAIXA == varTb295.CO_CAIXA
                                && tb296.TB295_CAIXA.DT_MOVIMENTO == varTb295.DT_MOVIMENTO && tb296.TB295_CAIXA.TB03_COLABOR.CO_COL == varTb295.CO_COLABOR_CAIXA
                                && tb296.FLA_SITU_DOC == "A"
                                select new { tb296.VR_LIQ_DOC, tb296.TP_OPER_CAIXA });

                decimal? dcmTotVrRecebido = 0;
                decimal? dcmTotVrPago = 0;

                foreach (var CM1 in varTb296)
                {
                    if (CM1.TP_OPER_CAIXA == "C")
                        dcmTotVrRecebido = dcmTotVrRecebido + CM1.VR_LIQ_DOC;
                    else
                        dcmTotVrPago = dcmTotVrPago + CM1.VR_LIQ_DOC;
                }

                var varTb297 = (from lTb297 in TB297_OPER_CAIXA.RetornaTodosRegistros()
                                where lTb297.TB295_CAIXA.TB03_COLABOR.CO_EMP == varTb295.CO_EMP && lTb297.TB295_CAIXA.CO_CAIXA == varTb295.CO_CAIXA
                                && lTb297.TB295_CAIXA.DT_MOVIMENTO == varTb295.DT_MOVIMENTO && lTb297.TB295_CAIXA.TB03_COLABOR.CO_COL == varTb295.CO_COLABOR_CAIXA
                                select new { lTb297.VALOR, lTb297.FLA_TIPO });

                decimal? dcmTotVrSangria = 0;
                decimal? dcmTotVrAporte = 0;

                foreach (var CM1 in varTb297)
                {
                    if (CM1.FLA_TIPO == "A")
                        dcmTotVrAporte = dcmTotVrAporte + CM1.VALOR;
                    else
                        dcmTotVrSangria = dcmTotVrSangria + CM1.VALOR;
                }

                varTb295.VR_RECEBIDO_CAIXA = dcmTotVrRecebido;
                varTb295.VR_PAGO_PELO_CAIXA = dcmTotVrPago;
                varTb295.VR_SANGRIA_CAIXA = dcmTotVrSangria;
                varTb295.VR_APORTE_CAIXA = dcmTotVrAporte;
                varTb295.VR_SALDO_CAIXA = (dcmTotVrRecebido + dcmTotVrAporte) - (dcmTotVrPago + dcmTotVrSangria);
                varTb295.DT_FECHAMENTO_CAIXA = DateTime.Now;
                varTb295.HR_FECHAMENTO_CAIXA = DateTime.Now.ToString("HH:mm");
                varTb295.CO_USUARIO_FECHA = LoginAuxili.IDEADMUSUARIO;

                TB295_CAIXA.SaveOrUpdate(varTb295, true);

                TB113_PARAM_CAIXA cai = TB113_PARAM_CAIXA.RetornaPeloCoCaixa(coCaixa);
                cai.CO_FLAG_USO_CAIXA = "F";
                TB113_PARAM_CAIXA.SaveOrUpdate(cai, true);
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Operação Realizada com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        protected void btnAnaRecCaixa_Click(object sender, EventArgs e)
        {
            hdVerifRecCaixa.Value = "1";
            bool possuiInconsistencia = false;

            TB295_CAIXA tb295 = RetornaEntidade();

            int coCaixa = tb295.CO_CAIXA;

            var varTb295 = (from lTb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where lTb295.CO_EMP == LoginAuxili.CO_EMP && lTb295.CO_CAIXA == coCaixa && lTb295.DT_FECHAMENTO_CAIXA == null
                            && lTb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP
                            select new
                            {
                                lTb295.CO_CAIXA,
                                lTb295.TB03_COLABOR.CO_EMP,
                                lTb295.TB03_COLABOR.CO_COL,
                                lTb295.DT_MOVIMENTO
                            }).FirstOrDefault();

            if (varTb295 == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Caixa não foi encontrado.");
                return;
            }

            foreach (GridViewRow rowGFP in grdFormPagRec.Rows)
            {
                foreach (GridViewRow rowGRA in grdResulAnaRec.Rows)
                {
                    int coTipRecgrdFP = int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value);
                    int coTipRecgrdRA = int.Parse(((HiddenField)rowGRA.Cells[2].FindControl("hdCO_TIPO_REC")).Value);

                    if (coTipRecgrdFP == coTipRecgrdRA)
                    {
                        var vrReceb = (from tb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                                       join tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros() on tb156.CO_CAIXA_MOVIMENTO equals tb296.CO_SEQMOV_CAIXA
                                       where tb296.TB295_CAIXA.CO_CAIXA == varTb295.CO_CAIXA && tb296.TB295_CAIXA.TB03_COLABOR.CO_EMP == varTb295.CO_EMP
                                       && tb296.TB295_CAIXA.TB03_COLABOR.CO_COL == varTb295.CO_COL && tb296.TB295_CAIXA.DT_MOVIMENTO == varTb295.DT_MOVIMENTO
                                       && tb156.CO_TIPO_REC == coTipRecgrdRA && tb296.TP_OPER_CAIXA.Equals("C") && tb296.FLA_SITU_DOC == "A"
                                       select new { tb156.VR_RECEBIDO, tb156.NU_QTDE });

                        decimal? dcmValorReal = 0;
                        int? intQtdeReal = 0;

                        foreach (var v1 in vrReceb)
                        {
                            dcmValorReal = v1.VR_RECEBIDO != null ? dcmValorReal + v1.VR_RECEBIDO : dcmValorReal;
                            intQtdeReal = v1.NU_QTDE != null ? intQtdeReal + v1.NU_QTDE : intQtdeReal;
                        }

                        if ((dcmValorReal != null) && (intQtdeReal != null))
                        {
                            decimal dcmValorFP = ((TextBox)rowGFP.Cells[2].FindControl("txtValorFP")).Text != "" ? Decimal.Parse(((TextBox)rowGFP.Cells[2].FindControl("txtValorFP")).Text) : 0;
                            int intQtdeFP = 0;

                            if (int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value) != 5)
                            {
                                intQtdeFP = ((TextBox)rowGFP.Cells[1].FindControl("txtQtdeFP")).Text != "" ? int.Parse(((TextBox)rowGFP.Cells[1].FindControl("txtQtdeFP")).Text) : 0;
                            }
                            else
                            {
                                ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "--";
                            }

                            if ((dcmValorFP == dcmValorReal) && (intQtdeFP == intQtdeReal))
                            {
                                ((System.Web.UI.WebControls.Image)rowGRA.Cells[2].FindControl("imgValorFP")).ImageUrl = "~/Library/IMG/Gestor_CheckSucess.png";
                                ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "OK";
                                ((TextBox)rowGRA.Cells[1].FindControl("txtValorRA")).Text = "OK";
                            }
                            else
                            {
                                possuiInconsistencia = true;

                                if (intQtdeFP == intQtdeReal)
                                    ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "OK";

                                if (dcmValorFP == dcmValorReal)
                                    ((TextBox)rowGRA.Cells[1].FindControl("txtValorRA")).Text = "OK";

                                if (dcmValorFP != dcmValorReal)
                                    ((TextBox)rowGRA.Cells[1].FindControl("txtValorRA")).Text = String.Format("{0:N}", dcmValorFP - dcmValorReal);

                                if ((intQtdeFP != intQtdeReal) && (int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value) != 5))
                                {
                                    ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = (intQtdeFP - intQtdeReal).ToString();

                                    if (intQtdeFP - intQtdeReal > 0)
                                        ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "+" + ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text;
                                }

                                ((System.Web.UI.WebControls.Image)rowGRA.Cells[2].FindControl("imgValorFP")).ImageUrl = "~/Library/IMG/Gestor_BtnDel.png";
                                hdVerifRecCaixa.Value = "0";
                            }
                        }
                    }
                }
            }

            if (possuiInconsistencia)
                LinkButton1.Enabled = true;
        }

        protected void btnRptInconsistencias_Click(object sender, EventArgs e)
        {
            TB295_CAIXA tb295 = RetornaEntidade();

            List<RptInconsistenciasCaixa.RegistroInformado> lstInf = new List<RptInconsistenciasCaixa.RegistroInformado>();
            foreach (GridViewRow rowGFP in grdFormPagRec.Rows)
            {
                foreach (GridViewRow rowGRA in grdResulAnaRec.Rows)
                {
                    int coTipRecgrdFP = int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value);
                    int coTipRecgrdRA = int.Parse(((HiddenField)rowGRA.Cells[2].FindControl("hdCO_TIPO_REC")).Value);

                    if (coTipRecgrdFP == coTipRecgrdRA)
                    {
                        decimal dcmValorFP = ((TextBox)rowGFP.Cells[2].FindControl("txtValorFP")).Text != "" ? Decimal.Parse(((TextBox)rowGFP.Cells[2].FindControl("txtValorFP")).Text) : 0;
                        int intQtdeFP = 0;

                        if (int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value) != 5)
                            intQtdeFP = ((TextBox)rowGFP.Cells[1].FindControl("txtQtdeFP")).Text != "" ? int.Parse(((TextBox)rowGFP.Cells[1].FindControl("txtQtdeFP")).Text) : 0;

                        lstInf.Add(new RptInconsistenciasCaixa.RegistroInformado()
                        {
                            CodTpPagto = coTipRecgrdFP,
                            Qtd = intQtdeFP,
                            Valor = dcmValorFP
                        });
                    }
                }
            }

            string strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            //string param = string.Format("(Nome CX: {0} - Usuário CX: {1} / {2} - Dt Abertura: {3:dd/MM/yy} - Vl Abertura: {4:c2}",
            //    tb295.
            RptInconsistenciasCaixa rpt = new RptInconsistenciasCaixa();
            rpt.InitReport("", LoginAuxili.CO_EMP, tb295.CO_EMP, lstInf, tb295.CO_CAIXA, tb295.DT_MOVIMENTO, tb295.CO_COLABOR_CAIXA, strINFOS);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            HttpContext.Current.Session["ApresentaRelatorio"] = 1;
            hdVerifRelac.Value = "1";
        }

        protected void btnAnaPagCaixa_Click(object sender, EventArgs e)
        {
            hdVerifPagCaixa.Value = "1";

            TB295_CAIXA tb295 = RetornaEntidade();

            int coCaixa = tb295.CO_CAIXA;

            var varTb295 = (from lTb295 in TB295_CAIXA.RetornaTodosRegistros()
                            where lTb295.CO_EMP == LoginAuxili.CO_EMP && lTb295.CO_CAIXA == coCaixa
                            && lTb295.DT_FECHAMENTO_CAIXA == null && lTb295.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP
                            select new
                            {
                                lTb295.CO_CAIXA,
                                lTb295.TB03_COLABOR.CO_EMP,
                                lTb295.TB03_COLABOR.CO_COL,
                                lTb295.DT_MOVIMENTO
                            }).FirstOrDefault();

            if (varTb295 == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Caixa não foi encontrado.");
                return;
            }

            foreach (GridViewRow rowGFP in grdFormPagPag.Rows)
            {
                foreach (GridViewRow rowGRA in grdResulAnaPag.Rows)
                {
                    int coTipRecgrdFP = int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value);
                    int coTipRecgrdRA = int.Parse(((HiddenField)rowGRA.Cells[2].FindControl("hdCO_TIPO_REC")).Value);

                    if (coTipRecgrdFP == coTipRecgrdRA)
                    {
                        var vrReceb = (from tb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                                       join tb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros() on tb156.CO_CAIXA_MOVIMENTO equals tb296.CO_SEQMOV_CAIXA
                                       where tb296.TB295_CAIXA.CO_CAIXA == varTb295.CO_CAIXA && tb296.TB295_CAIXA.TB03_COLABOR.CO_EMP == varTb295.CO_EMP
                                       && tb296.TB295_CAIXA.TB03_COLABOR.CO_COL == varTb295.CO_COL && tb296.TB295_CAIXA.DT_MOVIMENTO == varTb295.DT_MOVIMENTO
                                       && tb156.CO_TIPO_REC == coTipRecgrdRA && tb296.TP_OPER_CAIXA.Equals("D") && tb296.FLA_SITU_DOC == "A"
                                       select new { tb156.VR_RECEBIDO, tb156.NU_QTDE });

                        decimal? dcmValorReal = 0;
                        int? intQtdeReal = 0;

                        foreach (var v1 in vrReceb)
                        {
                            dcmValorReal = v1.VR_RECEBIDO != null ? dcmValorReal + v1.VR_RECEBIDO : dcmValorReal;
                            intQtdeReal = v1.NU_QTDE != null ? intQtdeReal + v1.NU_QTDE : intQtdeReal;
                        }

                        if ((dcmValorReal != null) && (intQtdeReal != null))
                        {
                            decimal dcmValorFP = ((TextBox)rowGFP.Cells[2].FindControl("txtValorFP")).Text != "" ? Decimal.Parse(((TextBox)rowGFP.Cells[2].FindControl("txtValorFP")).Text) : 0;
                            int intQtdeFP = 0;

                            if (int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value) != 5)
                                intQtdeFP = ((TextBox)rowGFP.Cells[1].FindControl("txtQtdeFP")).Text != "" ? int.Parse(((TextBox)rowGFP.Cells[1].FindControl("txtQtdeFP")).Text) : 0;
                            else
                                ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "--";

                            if ((dcmValorFP == dcmValorReal) && (intQtdeFP == intQtdeReal))
                            {
                                ((System.Web.UI.WebControls.Image)rowGRA.Cells[2].FindControl("imgValorFP")).ImageUrl = "~/Library/IMG/Gestor_CheckSucess.png";
                                ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "OK";
                                ((TextBox)rowGRA.Cells[1].FindControl("txtValorRA")).Text = "OK";
                            }
                            else
                            {
                                if (intQtdeFP == intQtdeReal)
                                    ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "OK";

                                if (dcmValorFP == dcmValorReal)
                                    ((TextBox)rowGRA.Cells[1].FindControl("txtValorRA")).Text = "OK";

                                if (dcmValorFP != dcmValorReal)
                                    ((TextBox)rowGRA.Cells[1].FindControl("txtValorRA")).Text = String.Format("{0:N}", dcmValorFP - dcmValorReal);

                                if ((intQtdeFP != intQtdeReal) && (int.Parse(((HiddenField)rowGFP.Cells[2].FindControl("hdCO_TIPO_REC")).Value) != 5))
                                {
                                    ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = (intQtdeFP - intQtdeReal).ToString();

                                    if (intQtdeFP - intQtdeReal > 0)
                                        ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text = "+" + ((TextBox)rowGRA.Cells[0].FindControl("txtQtdeRA")).Text;
                                }

                                ((System.Web.UI.WebControls.Image)rowGRA.Cells[2].FindControl("imgValorFP")).ImageUrl = "~/Library/IMG/Gestor_BtnDel.png";
                                hdVerifPagCaixa.Value = "0";
                            }
                        }
                    }
                }
            }
        }

        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            AuxiliPagina.RedirecionaParaPaginaBusca();
        }

        protected void txtQtdeFPR_TextChanged(object sender, EventArgs e)
        {
            if (grdFormPagRec.Rows.Count > 0)
            {
                int intQtdeTotal = 0;

                foreach (GridViewRow rowGFPR in grdFormPagRec.Rows)
                {
                    if (((TextBox)rowGFPR.Cells[1].FindControl("txtQtdeFP")).Text.Replace("_", "") != "")
                        intQtdeTotal = intQtdeTotal + int.Parse(((TextBox)rowGFPR.Cells[1].FindControl("txtQtdeFP")).Text.Replace("_", ""));
                }

                lblTotQtdRec.Text = intQtdeTotal.ToString();
            }
        }

        protected void txtValorFPR_TextChanged(object sender, EventArgs e)
        {
            if (grdFormPagRec.Rows.Count > 0)
            {
                decimal dcmValorTotal = 0;

                foreach (GridViewRow rowGFPR in grdFormPagRec.Rows)
                {
                    if (((TextBox)rowGFPR.Cells[2].FindControl("txtValorFP")).Text != "")
                        dcmValorTotal = dcmValorTotal + Decimal.Parse(((TextBox)rowGFPR.Cells[2].FindControl("txtValorFP")).Text);
                }

                lblTotValRec.Text = String.Format("{0:N}", dcmValorTotal);
            }
        }

        protected void txtQtdeFPP_TextChanged(object sender, EventArgs e)
        {
            if (grdFormPagPag.Rows.Count > 0)
            {
                int intQtdeTotal = 0;

                foreach (GridViewRow rowGFPR in grdFormPagPag.Rows)
                {
                    if (((TextBox)rowGFPR.Cells[1].FindControl("txtQtdeFP")).Text.Replace("_", "") != "")
                        intQtdeTotal = intQtdeTotal + int.Parse(((TextBox)rowGFPR.Cells[1].FindControl("txtQtdeFP")).Text.Replace("_", ""));
                }

                lblTotQtdPag.Text = intQtdeTotal.ToString();
            }
        }

        protected void txtValorFPP_TextChanged(object sender, EventArgs e)
        {
            if (grdFormPagPag.Rows.Count > 0)
            {
                decimal dcmValorTotal = 0;

                foreach (GridViewRow rowGFPR in grdFormPagPag.Rows)
                {
                    if (((TextBox)rowGFPR.Cells[2].FindControl("txtValorFP")).Text != "")
                        dcmValorTotal = dcmValorTotal + Decimal.Parse(((TextBox)rowGFPR.Cells[2].FindControl("txtValorFP")).Text);
                }

                lblTotValPag.Text = String.Format("{0:N}", dcmValorTotal);
            }
        }
    }
}