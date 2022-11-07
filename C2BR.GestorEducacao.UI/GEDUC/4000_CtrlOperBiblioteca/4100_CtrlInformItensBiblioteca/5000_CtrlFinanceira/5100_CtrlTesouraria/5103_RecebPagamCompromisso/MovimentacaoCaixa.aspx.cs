//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE TESOURARIA (CAIXA)
// OBJETIVO: REGISTRO DE RECEBIMENTO OU PAGAMENTO DE COMPROMISSOS FINANCEIROS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 25/03/2013| André Nobre Vinagre        | - Corrigida a lógica de inserção quando pagamento de título
//           |                            | - Criado o link de controle de cheques
//           |                            |
// ----------+----------------------------+-------------------------------------
// 02/04/2013| André Nobre Vinagre        | - Criado um link que redireciona para matrícula
//           |                            | - Tratada a flag de redirecionamento para matrícula
//           |                            |
// ----------+----------------------------+-------------------------------------
// 08/05/2013| André Nobre Vinagre        | - Corrigida inconsistência quanto ao carregamento dos
//           |                            | responsáveis pelo título ( estava carregando utilizando
//           |                            | o vínculo do aluno/responsável e não do título/responsável )
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5103_RecebPagamCompromisso
{
    public partial class MovimentacaoCaixa : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                CompareValidatorDataAtual.ValueToCompare = txtDataQuitacao.Text = dataAtual;
                CarregaFormulario();

                string strTpMov = chkCredito.Checked ? "C" : "D";
                string strTpBenef = "";
                int intCodBenef = 0;

                ListaBeneficiarios("C", "A");
                ListaAgrupador("R");

                var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                                  where adUs.CodUsuario == LoginAuxili.CO_COL
                                  select new { adUs.FLA_ALT_REG_PAG_MAT }).FirstOrDefault();

                if (admUsu != null)
                {
                    if (admUsu.FLA_ALT_REG_PAG_MAT == "S")
                    {
                        ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                                   where admMod.nomURLModulo.Contains("2107_MatriculaAluno/Matricula")
                                                   select admMod).FirstOrDefault();

                        if (admModuloMatr != null)
                        {
                            lnkMatriAluno.HRef = "/" + String.Format("{0}?moduloNome=+Matrícula+de+Alunos+Novos&", admModuloMatr.nomURLModulo);
                        }
                    }
                }

                ADMMODULO admModulo = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                      where admMod.nomURLModulo.Contains("5811_CadastramentoChequeEmitidoInstit")
                                      select admMod).FirstOrDefault();

                if (admModulo != null)
                {
                    lnkCadCheque.HRef = "/" + String.Format("{0}?moduloNome=+Cadastramento+de+Cheques+Emitidos+ou+Recebido+pela+Instituição.&", admModulo.nomURLModulo);       
                }
                else
                {
                    lnkCadCheque.HRef = "/RedirecionaMensagem.aspx?moduloNome=+Cadastramento+de+Cheques+Emitidos+ou+Recebido+pela+Instituição.nptored=true&msgType=Error&msg=Funcionalidade de Cadastro de Cheques Indisponível.";
                }

                if (ddlBenef.Items.Count > 0)
                {
                    ddlBenef.SelectedIndex = 0;
                    intCodBenef = int.Parse(ddlBenef.SelectedValue);
                }

                if (chkCredito.Checked)
                    strTpBenef = chkBenif1.Checked ? "A" : "C";
                else
                    strTpBenef = "F";

                if (intCodBenef > 0)
                    MontaGridContratos(strTpMov, strTpBenef, intCodBenef, 0);

                MontaGridFormPagamento();
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
            grdContratos.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

            var listaContratos = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                 join resp in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb47.TB108_RESPONSAVEL.CO_RESP equals resp.CO_RESP
                                 where (tb47.CO_EMP == LoginAuxili.CO_EMP) && tb47.CO_EMP == tb07.CO_EMP && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R")
                                 && tb47.TP_CLIENTE_DOC == "A" && tb47.CO_ALU == 0
                                 select new
                                 {
                                     tb47.CO_EMP,
                                     tb47.NU_DOC,
                                     tb47.NU_PAR,
                                     tb47.DT_CAD_DOC,
                                     NO_IDENTIF = tb07.NO_ALU,
                                     ID_CLIENTE_DOC = tb07.CO_ALU,
                                     tb47.VR_PAR_DOC,
                                     tb47.VR_JUR_DOC,
                                     tb47.VR_MUL_DOC,
                                     tb47.DT_VEN_DOC,
                                     tb47.VR_DES_DOC,
                                     tb47.DE_COM_HIST,
                                     VR_DEBITO = tb47.VR_PAR_DOC +
                                     (tb47.CO_FLAG_TP_VALOR_JUR == "V" ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) : (tb47.VR_JUR_DOC != null ? (tb47.VR_JUR_DOC * tb47.VR_PAR_DOC) / 100 : 0)) -
                                     (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)) +
                                     (tb47.CO_FLAG_TP_VALOR_MUL == "V" ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) : (tb47.VR_MUL_DOC != null ? (tb47.VR_MUL_DOC * tb47.VR_PAR_DOC) / 100 : 0)),
                                     CEDENTE = resp.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".")
                                 };

            BoundField bf4 = new BoundField();
            bf4.DataField = "NO_IDENTIF";
            bf4.HeaderText = "Referente";
            bf4.ItemStyle.Width = 270;
            bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            grdContratos.Columns.Add(bf4);

            BoundField bf1 = new BoundField();
            bf1.DataField = "NU_DOC";
            bf1.HeaderText = "Nº Doc";
            grdContratos.Columns.Add(bf1);

            BoundField bf3 = new BoundField();
            bf3.DataField = "NU_PAR";
            bf3.HeaderText = "PA";
            bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdContratos.Columns.Add(bf3);

            BoundField bf2 = new BoundField();
            bf2.DataField = "DT_CAD_DOC";
            bf2.HeaderText = "Cadastro";
            bf2.DataFormatString = "{0:d}";
            grdContratos.Columns.Add(bf2);

            BoundField bf9 = new BoundField();
            bf9.DataField = "DT_VEN_DOC";
            bf9.HeaderText = "Vencto";
            bf9.DataFormatString = "{0:d}";
            bf9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdContratos.Columns.Add(bf9);

            BoundField bf6 = new BoundField();
            bf6.DataField = "VR_PAR_DOC";
            bf6.HeaderText = "R$ Parcela";
            bf6.DataFormatString = "{0:N}";
            bf6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            grdContratos.Columns.Add(bf6);

            BoundField bf7 = new BoundField();
            bf7.DataField = "DE_COM_HIST";
            bf7.HeaderText = "Histórico";
            bf7.ItemStyle.Width = 270;
            bf7.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            grdContratos.Columns.Add(bf7);

            grdContratos.DataSource = listaContratos;

            grdContratos.DataBind();
        }

        /// <summary>
        /// Método que monta a grid de Forma de Pagamento
        /// </summary>
        protected void MontaGridFormPagamento()
        {
            grdFormPag.DataKeyNames = new string[] { "CO_TIPO_REC" };

            string strTipo = chkCredito.Checked ? "C" : "D";

            grdFormPag.DataSource = from tb118 in TB118_TIPO_RECEB.RetornaTodosRegistros()
                                    where tb118.CLA_TIPO_MOVIM == "T" || tb118.CLA_TIPO_MOVIM == strTipo
                                    select new { tb118.CO_TIPO_REC, tb118.DE_SIG_RECEB, tb118.DE_RECEBIMENTO };
            grdFormPag.DataBind();
        }

        /// <summary>
        /// Método que Monta o Contrato Selecionado
        /// </summary>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <param name="DT_CAD_DOC">Data de cadastro</param>
        /// <param name="strTpBenef">Tipo de beneficiário</param>
        private void MontaContratoSelecionado(string NU_DOC, int NU_PAR, DateTime DT_CAD_DOC, string strTpBenef)
        {
            LimpaCampos();
            decimal descto = 0;

            if (strTpBenef == "A")
            {
                var conta = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU                             
                             where tb47.CO_EMP == LoginAuxili.CO_EMP && tb47.NU_DOC == NU_DOC && tb47.NU_PAR == NU_PAR && tb47.DT_CAD_DOC.Day == DT_CAD_DOC.Day
                             && tb47.DT_CAD_DOC.Month == DT_CAD_DOC.Month && tb47.DT_CAD_DOC.Year == DT_CAD_DOC.Year && tb47.CO_EMP == tb07.CO_EMP
                             select new
                             {
                                 tb07.NO_ALU,
                                 tb07.NO_APE_ALU,
                                 tb07.NU_TELE_CELU_ALU,
                                 tb07.NU_CPF_ALU,
                                 DES_TIPO_DOC = tb47.TB086_TIPO_DOC.DES_TIPO_DOC != null ? tb47.TB086_TIPO_DOC.DES_TIPO_DOC : null,
                                 tb47.VR_DES_DOC,
                                 tb47.NU_DOC,
                                 tb47.NU_PAR,
                                 tb47.DT_CAD_DOC,
                                 tb47.DT_VEN_DOC,
                                 tb47.DE_COM_HIST,
                                 tb47.CO_BARRA_DOC,
                                 tb47.VR_PAR_DOC,
                                 tb47.VR_MUL_DOC,
                                 tb47.CO_FLAG_TP_VALOR_MUL,
                                 tb47.CO_FLAG_TP_VALOR_JUR,
                                 tb47.VR_JUR_DOC,
                                 tb47.CO_FLAG_TP_VALOR_DES,
                                 tb47.VL_DES_BOLSA_ALUNO, tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO,
                                 DE_HISTORICO = tb47.TB39_HISTORICO != null ? tb47.TB39_HISTORICO.DE_HISTORICO : ""
                             }).FirstOrDefault();

                if (conta != null)
                {
                    lblTipo.Text = conta.DES_TIPO_DOC != null ? conta.DES_TIPO_DOC : "";
                    lblDocNum.Text = conta.NU_DOC;
                    lblDocParc.Text = conta.NU_PAR.ToString("D2");
                    lblDtEmissao.Text = conta.DT_CAD_DOC.ToString("dd/MM/yyyy");
                    lblHistorico.Text = conta.DE_HISTORICO;
                    lblDtVencto.Text = conta.DT_VEN_DOC.ToString("dd/MM/yyyy");
                    lblCodBarras.Text = conta.CO_BARRA_DOC != null ? conta.CO_BARRA_DOC : "";
                    DateTime startTime = conta.DT_VEN_DOC;
                    DateTime endTime = DateTime.Now;

                    if (startTime > endTime)
                        lblQtdDias.Style.Add("color", "blue");
                    else
                        lblQtdDias.Style.Add("color", "red");

                    lblQtdDias.Style.Add("float", "right");

                    TimeSpan span = endTime.Subtract(startTime);
                    int diasAtraso = span.Days;

                    lblQtdDias.Text = span.Days.ToString();
                    lblValDoctoSis.Text = conta.VR_PAR_DOC.ToString("#,##0.00");
                    lblValDoctoInf.Text = conta.VR_PAR_DOC.ToString("#,##0.00");

                    if (diasAtraso <= 0)
                    {
                        lblValCorSis.Text = lblValMulSis.Text = txtValMulInf.Text = txtValCorInf.Text = "0,00";
                        txtValMulInf.Enabled = txtValCorInf.Enabled = false;
                        descto = conta.VR_DES_DOC != null ? conta.CO_FLAG_TP_VALOR_DES == "V" ? (decimal)conta.VR_DES_DOC : (decimal)(conta.VR_PAR_DOC * (conta.VR_DES_DOC / 100)) : 0;
                        descto = conta.VL_DES_BOLSA_ALUNO != null ? conta.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "V" ? (decimal)conta.VL_DES_BOLSA_ALUNO + descto : (decimal)(conta.VR_PAR_DOC * (conta.VL_DES_BOLSA_ALUNO / 100)) + descto : descto;
                        lblValDesctoSis.Text = String.Format("{0:N}", descto);
                    }
                    else
                    {
                        txtValMulInf.Enabled = txtValCorInf.Enabled = true;
                        lblValMulSis.Text = conta.CO_FLAG_TP_VALOR_MUL == "V" ? String.Format("{0:N}", conta.VR_MUL_DOC) : String.Format("{0:N}", conta.VR_PAR_DOC * (conta.VR_MUL_DOC / 100));
                        lblValCorSis.Text = conta.CO_FLAG_TP_VALOR_JUR == "V" ? String.Format("{0:N}", conta.VR_JUR_DOC * diasAtraso) : String.Format("{0:N}", (conta.VR_PAR_DOC * (conta.VR_JUR_DOC / 100)) * diasAtraso);

                        if (lblValCorSis.Text == "")
                            lblValCorSis.Text = "0,00";

                        if (lblValMulSis.Text == "")
                            lblValMulSis.Text = "0,00";

                        txtValMulInf.Text = String.Format("{0:N}", Decimal.Parse(lblValMulSis.Text));
                        txtValCorInf.Text = String.Format("{0:N}", Decimal.Parse(lblValCorSis.Text));
                        lblValDesctoSis.Text = "0,00";
                    }                    

                    if (lblValDesctoSis.Text == "")
                        lblValDesctoSis.Text = "0,00";

                    txtValAdcInf.Enabled = txtValDesInf.Enabled = true;

                    lblValSubTotalSis.Text = String.Format("{0:N}", conta.VR_PAR_DOC + Decimal.Parse(lblValMulSis.Text) + Decimal.Parse(lblValCorSis.Text));
                    txtValAdcInf.Text = String.Format("{0:N}", Decimal.Parse(lblValAdcSis.Text));
                    lblValSubTotInf.Text = lblValSubTotalSis.Text;
                    lblValTotalSis.Text = String.Format("{0:N}", (conta.VR_PAR_DOC + Decimal.Parse(lblValMulSis.Text) + Decimal.Parse(lblValCorSis.Text) - Decimal.Parse(lblValDesctoSis.Text)));
                    txtValDesInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDesctoSis.Text));
                    lblValTotInf.Text = lblValTotalSis.Text;
                }
            }
            else if (strTpBenef == "F")
            {
                var conta = (from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                             where tb38.CO_EMP.Equals(LoginAuxili.CO_EMP)
                             && tb38.NU_DOC == NU_DOC
                             && tb38.NU_PAR == NU_PAR
                             && tb38.DT_CAD_DOC.Day == DT_CAD_DOC.Day
                             && tb38.DT_CAD_DOC.Month == DT_CAD_DOC.Month
                             && tb38.DT_CAD_DOC.Year == DT_CAD_DOC.Year
                             && tb38.IC_SIT_DOC != "C" && tb38.IC_SIT_DOC != "Q"
                             select new
                             {
                                 tb38.TB41_FORNEC.NO_FAN_FOR,
                                 tb38.TB41_FORNEC.CO_TEL1_FORN,
                                 tb38.TB41_FORNEC.CO_CPFCGC_FORN,
                                 tb38.VR_DES_DOC,
                                 DES_TIPO_DOC = tb38.TB086_TIPO_DOC.DES_TIPO_DOC != null ? tb38.TB086_TIPO_DOC.DES_TIPO_DOC : null,
                                 tb38.NU_DOC,
                                 tb38.NU_PAR,
                                 tb38.DT_CAD_DOC,
                                 tb38.DT_VEN_DOC,
                                 tb38.DE_COM_HIST,
                                 tb38.CO_BARRA_DOC,
                                 tb38.VR_PAR_DOC,
                                 tb38.VR_MUL_DOC,
                                 tb38.CO_FLAG_TP_VALOR_MUL,
                                 tb38.CO_FLAG_TP_VALOR_JUR,
                                 tb38.VR_JUR_DOC,
                                 tb38.CO_FLAG_TP_VALOR_DES,
                                 DE_HISTORICO = tb38.TB39_HISTORICO != null ? tb38.TB39_HISTORICO.DE_HISTORICO : ""
                             }).FirstOrDefault();

                if (conta != null)
                {
                    lblTipo.Text = conta.DES_TIPO_DOC != null ? conta.DES_TIPO_DOC : "";
                    lblDocNum.Text = conta.NU_DOC;
                    lblDocParc.Text = conta.NU_PAR.ToString("D2");
                    lblDtEmissao.Text = conta.DT_CAD_DOC.ToString("dd/MM/yyyy");
                    lblHistorico.Text = conta.DE_HISTORICO;
                    lblDtVencto.Text = conta.DT_VEN_DOC.ToString("dd/MM/yyyy");
                    lblCodBarras.Text = conta.CO_BARRA_DOC != null ? conta.CO_BARRA_DOC : "";
                    DateTime startTime = conta.DT_VEN_DOC;
                    DateTime endTime = DateTime.Now;

                    if (startTime > endTime)
                        lblQtdDias.Style.Add("color", "blue");
                    else
                        lblQtdDias.Style.Add("color", "red");

                    TimeSpan span = endTime.Subtract(startTime);
                    int diasAtraso = span.Days;

                    lblQtdDias.Text = span.Days.ToString("D4");
                    lblValDoctoSis.Text = conta.VR_PAR_DOC.ToString("#,##0.00");
                    lblValDoctoInf.Text = conta.VR_PAR_DOC.ToString("#,##0.00");
                    lblValMulSis.Text = conta.CO_FLAG_TP_VALOR_MUL == "V" ? String.Format("{0:N}", conta.VR_MUL_DOC) : String.Format("{0:N}", conta.VR_PAR_DOC * (conta.VR_MUL_DOC / 100));

                    if (diasAtraso <= 0)
                    {
                        lblValCorSis.Text = lblValMulSis.Text = txtValMulInf.Text = txtValCorInf.Text = "0,00";
                        txtValMulInf.Enabled = txtValCorInf.Enabled = false;
                        lblValDesctoSis.Text = conta.CO_FLAG_TP_VALOR_DES == "V" ? String.Format("{0:N}", conta.VR_DES_DOC) : String.Format("{0:N}", conta.VR_PAR_DOC * (conta.VR_DES_DOC / 100));
                    }
                    else
                    {
                        txtValMulInf.Enabled = txtValCorInf.Enabled = true;
                        lblValMulSis.Text = conta.CO_FLAG_TP_VALOR_MUL == "V" ? String.Format("{0:N}", conta.VR_MUL_DOC) : String.Format("{0:N}", conta.VR_PAR_DOC * (conta.VR_MUL_DOC / 100));
                        lblValCorSis.Text = conta.CO_FLAG_TP_VALOR_JUR == "V" ? String.Format("{0:N}", conta.VR_JUR_DOC * diasAtraso) : String.Format("{0:N}", (conta.VR_PAR_DOC * (conta.VR_JUR_DOC / 100)) * diasAtraso);

                        if (lblValCorSis.Text == "")
                            lblValCorSis.Text = "0,00";

                        if (lblValMulSis.Text == "")
                            lblValMulSis.Text = "0,00";

                        txtValMulInf.Text = String.Format("{0:N}", Decimal.Parse(lblValMulSis.Text));
                        txtValCorInf.Text = String.Format("{0:N}", Decimal.Parse(lblValCorSis.Text));
                    }                    

                    if (lblValDesctoSis.Text == "")
                        lblValDesctoSis.Text = "0,00";

                    txtValAdcInf.Enabled = txtValDesInf.Enabled = true;

                    lblValSubTotalSis.Text = String.Format("{0:N}", conta.VR_PAR_DOC + Decimal.Parse(lblValMulSis.Text) + Decimal.Parse(lblValCorSis.Text));
                    txtValAdcInf.Text = String.Format("{0:N}", Decimal.Parse(lblValAdcSis.Text));
                    lblValSubTotInf.Text = lblValSubTotalSis.Text;
                    lblValTotalSis.Text = String.Format("{0:N}", (conta.VR_PAR_DOC + Decimal.Parse(lblValMulSis.Text) + Decimal.Parse(lblValCorSis.Text) - Decimal.Parse(lblValDesctoSis.Text)));
                    txtValDesInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDesctoSis.Text));
                    lblValTotInf.Text = lblValTotalSis.Text;
                }
            }
            else
            {
                var conta = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                             where tb47.CO_EMP.Equals(LoginAuxili.CO_EMP) && tb47.NU_DOC == NU_DOC && tb47.NU_PAR == NU_PAR
                             && tb47.DT_CAD_DOC.Day == DT_CAD_DOC.Day && tb47.DT_CAD_DOC.Month == DT_CAD_DOC.Month && tb47.DT_CAD_DOC.Year == DT_CAD_DOC.Year
                             select new
                             {
                                 tb47.TB103_CLIENTE.NO_FAN_CLI,
                                 tb47.TB103_CLIENTE.NO_SIGLA_CLIEN,
                                 tb47.TB103_CLIENTE.CO_TEL1_CLI,
                                 tb47.CO_BARRA_DOC,
                                 tb47.NU_DOC,
                                 DES_TIPO_DOC = tb47.TB086_TIPO_DOC.DES_TIPO_DOC != null ? tb47.TB086_TIPO_DOC.DES_TIPO_DOC : null,
                                 tb47.VR_DES_DOC,
                                 tb47.NU_PAR,
                                 tb47.TB103_CLIENTE.CO_CPFCGC_CLI,
                                 tb47.DT_CAD_DOC,
                                 tb47.DT_VEN_DOC,
                                 tb47.DE_COM_HIST,
                                 tb47.VR_PAR_DOC,
                                 tb47.VR_MUL_DOC,
                                 tb47.CO_FLAG_TP_VALOR_MUL,
                                 tb47.CO_FLAG_TP_VALOR_JUR,
                                 tb47.VR_JUR_DOC,
                                 tb47.CO_FLAG_TP_VALOR_DES,
                                 DE_HISTORICO = tb47.TB39_HISTORICO != null ? tb47.TB39_HISTORICO.DE_HISTORICO : "",
                                 tb47.VL_DES_BOLSA_ALUNO, tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO
                             }).FirstOrDefault();

                if (conta != null)
                {
                    lblTipo.Text = conta.DES_TIPO_DOC != null ? conta.DES_TIPO_DOC : "";
                    lblDocNum.Text = conta.NU_DOC;
                    lblDocParc.Text = conta.NU_PAR.ToString("D2");
                    lblDtEmissao.Text = conta.DT_CAD_DOC.ToString("dd/MM/yyyy");
                    lblHistorico.Text = conta.DE_HISTORICO;
                    lblDtVencto.Text = conta.DT_VEN_DOC.ToString("dd/MM/yyyy");
                    lblCodBarras.Text = conta.CO_BARRA_DOC != null ? conta.CO_BARRA_DOC : "";
                    DateTime startTime = conta.DT_VEN_DOC;
                    DateTime endTime = DateTime.Now;

                    if (startTime > endTime)
                        lblQtdDias.Style.Add("color", "blue");
                    else
                        lblQtdDias.Style.Add("color", "red");

                    TimeSpan span = endTime.Subtract(startTime);
                    int diasAtraso = span.Days;

                    lblQtdDias.Text = span.Days.ToString();

                    lblValDoctoSis.Text = conta.VR_PAR_DOC.ToString("#,##0.00");
                    lblValDoctoInf.Text = conta.VR_PAR_DOC.ToString("#,##0.00");
                    lblValMulSis.Text = conta.CO_FLAG_TP_VALOR_MUL == "V" ? String.Format("{0:N}", conta.VR_MUL_DOC) : String.Format("{0:N}", conta.VR_PAR_DOC * (conta.VR_MUL_DOC / 100));

                    if (diasAtraso <= 0)
                    {
                        lblValCorSis.Text = lblValMulSis.Text = txtValMulInf.Text = txtValCorInf.Text = "0,00";
                        txtValMulInf.Enabled = txtValCorInf.Enabled = false;
                        descto = conta.VR_DES_DOC != null ? conta.CO_FLAG_TP_VALOR_DES == "V" ? (decimal)conta.VR_DES_DOC : (decimal)(conta.VR_PAR_DOC * (conta.VR_DES_DOC / 100)) : 0;
                        descto = conta.VL_DES_BOLSA_ALUNO != null ? conta.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "V" ? (decimal)conta.VL_DES_BOLSA_ALUNO + descto : (decimal)(conta.VR_PAR_DOC * (conta.VL_DES_BOLSA_ALUNO / 100)) + descto : descto;
                        lblValDesctoSis.Text = String.Format("{0:N}", descto);
                    }
                    else
                    {
                        txtValMulInf.Enabled = txtValCorInf.Enabled = true;
                        lblValMulSis.Text = conta.CO_FLAG_TP_VALOR_MUL == "V" ? String.Format("{0:N}", conta.VR_MUL_DOC) : String.Format("{0:N}", conta.VR_PAR_DOC * (conta.VR_MUL_DOC / 100));
                        lblValCorSis.Text = conta.CO_FLAG_TP_VALOR_JUR == "V" ? String.Format("{0:N}", conta.VR_JUR_DOC * diasAtraso) : String.Format("{0:N}", (conta.VR_PAR_DOC * (conta.VR_JUR_DOC / 100)) * diasAtraso);

                        if (lblValMulSis.Text == "")
                            lblValMulSis.Text = "0,00";

                        if (lblValCorSis.Text == "")
                            lblValCorSis.Text = "0,00";

                        txtValMulInf.Text = String.Format("{0:N}", Decimal.Parse(lblValMulSis.Text));
                        txtValCorInf.Text = String.Format("{0:N}", Decimal.Parse(lblValCorSis.Text));
                    }                    

                    if (lblValDesctoSis.Text == "")
                        lblValDesctoSis.Text = "0,00";

                    txtValAdcInf.Enabled = txtValDesInf.Enabled = true;

                    lblValSubTotalSis.Text = String.Format("{0:N}", conta.VR_PAR_DOC + Decimal.Parse(lblValMulSis.Text) + Decimal.Parse(lblValCorSis.Text));
                    lblValSubTotInf.Text = lblValSubTotalSis.Text;
                    lblValTotalSis.Text = String.Format("{0:N}", (conta.VR_PAR_DOC + Decimal.Parse(lblValMulSis.Text) + Decimal.Parse(lblValCorSis.Text) - Decimal.Parse(lblValDesctoSis.Text)));
                    txtValDesInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDesctoSis.Text));
                    lblValTotInf.Text = lblValTotalSis.Text;
                }
            }

            if (grdContratos.Rows.Count > 0)
            {
                if (grdFormPag.Rows.Count > 0)
                {
                    foreach (GridViewRow linha in grdFormPag.Rows)
                    {
                        ((TextBox)linha.Cells[3].FindControl("txtValorFP")).Enabled = true;
                        ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Enabled = true;
                        ((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Enabled = true;
                        if (int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value) == 5)
                        {
                            ((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Enabled = false;
                        }
                    }
                }
            }
            else
            {
                txtValMulInf.Enabled = txtValCorInf.Enabled = txtValAdcInf.Enabled = txtValDesInf.Enabled = false;

                if (grdFormPag.Rows.Count > 0)
                {
                    foreach (GridViewRow linha in grdFormPag.Rows)
                    {
                        ((TextBox)linha.Cells[3].FindControl("txtValorFP")).Enabled = false;
                        ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Enabled = false;
                        ((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Método que Monta a grid de Contratos
        /// </summary>
        /// <param name="strTipoMovi">Tipo de movimentação</param>
        /// <param name="strTipoBenef">Tipo de beneficiário</param>
        /// <param name="intCodigoBenef">Código do beneficiário</param>
        /// <param name="intAgrupador">Código do Agrupador</param>
        protected void MontaGridContratos(string strTipoMovi, string strTipoBenef, int intCodigoBenef, int intAgrupador)
        {
            LimpaCampos();
            if (strTipoMovi == "C")
            {
                if (strTipoBenef == "A")
                {
                    var listaContratos = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                         join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                         join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb47.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                         where (tb47.CO_EMP == LoginAuxili.CO_EMP) && tb47.CO_EMP == tb07.CO_EMP && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R")
                                         && tb47.TP_CLIENTE_DOC == "A" && tb47.TB108_RESPONSAVEL.CO_RESP == intCodigoBenef
                                         && (intAgrupador != 0 ? tb47.CO_AGRUP_RECDESP == intAgrupador : intAgrupador == 0)
                                         select new
                                         {
                                             tb47.CO_EMP,
                                             tb47.NU_DOC,
                                             tb47.NU_PAR,
                                             tb47.DT_CAD_DOC,
                                             NO_IDENTIF = tb07.NO_ALU,
                                             ID_CLIENTE_DOC = tb07.CO_ALU,
                                             tb47.VR_PAR_DOC,
                                             tb47.VR_JUR_DOC,
                                             tb47.VR_MUL_DOC,
                                             tb47.DT_VEN_DOC,
                                             tb47.VR_DES_DOC,
                                             DE_COM_HIST = tb47.TB39_HISTORICO.DE_HISTORICO,
                                             VR_DEBITO = tb47.VR_PAR_DOC +
                                             (tb47.CO_FLAG_TP_VALOR_JUR == "V" ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) : (tb47.VR_JUR_DOC != null ? (tb47.VR_JUR_DOC * tb47.VR_PAR_DOC) / 100 : 0)) -
                                             (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)) +
                                             (tb47.CO_FLAG_TP_VALOR_MUL == "V" ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) : (tb47.VR_MUL_DOC != null ? (tb47.VR_MUL_DOC * tb47.VR_PAR_DOC) / 100 : 0)),
                                             CEDENTE = tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".")
                                         };
                    grdContratos.DataSource = listaContratos;
                }
                else
                {
                    var listaContratos = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                         join tb103 in TB103_CLIENTE.RetornaTodosRegistros() on tb47.TB103_CLIENTE.CO_CLIENTE equals tb103.CO_CLIENTE
                                         where (tb47.CO_EMP == LoginAuxili.CO_EMP) && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R")
                                         && tb47.TP_CLIENTE_DOC == "O" && tb47.TB103_CLIENTE.CO_CLIENTE == intCodigoBenef
                                         && (intAgrupador != 0 ? tb47.CO_AGRUP_RECDESP == intAgrupador : intAgrupador == 0)
                                         select new
                                         {
                                             tb47.CO_EMP,
                                             tb47.NU_DOC,
                                             tb47.NU_PAR,
                                             tb47.DT_CAD_DOC,
                                             NO_IDENTIF = tb103.NO_FAN_CLI,
                                             tb47.VR_PAR_DOC,
                                             ID_CLIENTE_DOC = tb103.CO_CLIENTE,
                                             tb47.DT_VEN_DOC,
                                             tb47.VR_DES_DOC,
                                             DE_COM_HIST = tb47.TB39_HISTORICO.DE_HISTORICO,
                                             tb47.VR_JUR_DOC,
                                             tb47.VR_MUL_DOC,
                                             VR_DEBITO = tb47.VR_PAR_DOC +
                                             (tb47.CO_FLAG_TP_VALOR_JUR == "V" ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) : (tb47.VR_JUR_DOC != null ? (tb47.VR_JUR_DOC * tb47.VR_PAR_DOC) / 100 : 0)) -
                                             (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)) +
                                             (tb47.CO_FLAG_TP_VALOR_MUL == "V" ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) : (tb47.VR_MUL_DOC != null ? (tb47.VR_MUL_DOC * tb47.VR_PAR_DOC) / 100 : 0)),
                                             CEDENTE = tb103.CO_CPFCGC_CLI.Length == 14 ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI.Length == 11 ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb103.CO_CPFCGC_CLI,

                                         };

                    grdContratos.DataSource = listaContratos;
                }
            }
            else
            {
                var listaContratos = from tb38 in TB38_CTA_PAGAR.RetornaTodosRegistros()
                                     where (tb38.CO_EMP == LoginAuxili.CO_EMP) && tb38.IC_SIT_DOC == "A" && tb38.TB41_FORNEC.CO_FORN == intCodigoBenef
                                     && (intAgrupador != 0 ? tb38.CO_AGRUP_RECDESP == intAgrupador : intAgrupador == 0)
                                     select new
                                     {
                                         tb38.CO_EMP,
                                         tb38.NU_DOC,
                                         tb38.NU_PAR,
                                         tb38.DT_CAD_DOC,
                                         NO_IDENTIF = tb38.TB41_FORNEC.NO_FAN_FOR,
                                         ID_CLIENTE_DOC = tb38.TB41_FORNEC.CO_FORN,
                                         tb38.VR_PAR_DOC,
                                         tb38.VR_JUR_DOC,
                                         tb38.VR_MUL_DOC,
                                         tb38.DT_VEN_DOC,
                                         tb38.VR_DES_DOC,
                                         CEDENTE = "",
                                         DE_COM_HIST = tb38.TB39_HISTORICO.DE_HISTORICO,
                                         VR_DEBITO = tb38.VR_PAR_DOC +
                                         (tb38.CO_FLAG_TP_VALOR_JUR == "V" ? (tb38.VR_JUR_DOC != null ? tb38.VR_JUR_DOC : 0) : (tb38.VR_JUR_DOC != null ? (tb38.VR_JUR_DOC * tb38.VR_PAR_DOC) / 100 : 0)) -
                                         (tb38.CO_FLAG_TP_VALOR_DES == "V" ? (tb38.VR_DES_DOC != null ? tb38.VR_DES_DOC : 0) : (tb38.VR_DES_DOC != null ? (tb38.VR_DES_DOC * tb38.VR_PAR_DOC) / 100 : 0)) +
                                         (tb38.CO_FLAG_TP_VALOR_MUL == "V" ? (tb38.VR_MUL_DOC != null ? tb38.VR_MUL_DOC : 0) : (tb38.VR_MUL_DOC != null ? (tb38.VR_MUL_DOC * tb38.VR_PAR_DOC) / 100 : 0))
                                     };

                grdContratos.DataSource = listaContratos;
            }

            grdContratos.DataBind();

            if (grdContratos.Rows.Count > 0)
            {
                if (grdFormPag.Rows.Count > 0)
                {
                    foreach (GridViewRow linha in grdFormPag.Rows)
                    {
                        ((TextBox)linha.Cells[3].FindControl("txtValorFP")).Enabled = true;
                        ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Enabled = true;
                        ((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Enabled = true;
                        if (int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value) == 5)
                            ((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Enabled = false;
                    }
                }
            }
            else
            {
                txtValMulInf.Enabled = txtValCorInf.Enabled = txtValAdcInf.Enabled = txtValDesInf.Enabled = false;

                if (grdFormPag.Rows.Count > 0)
                {
                    foreach (GridViewRow linha in grdFormPag.Rows)
                    {
                        ((TextBox)linha.Cells[2].FindControl("txtValorFP")).Enabled = false;
                        ((TextBox)linha.Cells[3].FindControl("txtObservacao")).Enabled = false;
                        ((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Beneficiários de acordo com o Tipo
        /// </summary>
        /// <param name="strTipoMovi">Tipo de movimentação</param>
        /// <param name="strTipoBenef">Tipo de beneficiário</param>
        protected void ListaBeneficiarios(string strTipoMovi, string strTipoBenef)
        {
            if (strTipoMovi == "C")
            {
                if (strTipoBenef == "A")
                {
                    ddlBenef.DataSource = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                           where iTb47.TB108_RESPONSAVEL.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO &&
                                           iTb47.CO_EMP == LoginAuxili.CO_EMP
                                           select new { iTb47.TB108_RESPONSAVEL.CO_RESP, iTb47.TB108_RESPONSAVEL.NO_RESP, iTb47.TB108_RESPONSAVEL.NU_CPF_RESP }
                                           ).DistinctBy(x => x.NU_CPF_RESP).OrderBy(p => p.NO_RESP);

                    ddlBenef.DataTextField = "NO_RESP";
                    ddlBenef.DataValueField = "CO_RESP";

                    ddlBenef.DataBind();
                }
                else
                {
                    ddlBenef.DataSource = TB103_CLIENTE.RetornaTodosRegistros();

                    ddlBenef.DataTextField = "NO_FAN_CLI";
                    ddlBenef.DataValueField = "CO_CLIENTE";

                    ddlBenef.DataBind();
                }
            }
            else
            {
                ddlBenef.DataSource = TB41_FORNEC.RetornaTodosRegistros();

                ddlBenef.DataTextField = "NO_FAN_FOR";
                ddlBenef.DataValueField = "CO_FORN";

                ddlBenef.DataBind();
            }
        }

        protected void ListaAgrupador(string strTipoMovi)
        {
            if (string.IsNullOrEmpty(strTipoMovi))
                return;

            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == strTipoMovi
                                       && tb315.CO_SITU_AGRUP_RECDESP == "A"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP })
                                       .OrderBy(p => p.DE_SITU_AGRUP_RECDESP);

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que limpa os campos informados
        /// </summary>
        protected void LimpaCampos()
        {
            lblTipo.Text = lblDocNum.Text = lblDocParc.Text = lblDtEmissao.Text = txtValAdcInf.Text =
            lblHistorico.Text = lblDtVencto.Text = lblCodBarras.Text = lblQtdDias.Text = txtValMulInf.Text = txtValCorInf.Text = txtValDesInf.Text = "";
            lblValDoctoSis.Text = lblValMulSis.Text = lblValCorSis.Text = lblValAdcSis.Text = lblValSubTotalSis.Text =
            lblValDesctoSis.Text = lblValTotalSis.Text = lblValDoctoInf.Text = lblValSubTotInf.Text = lblValTotInf.Text = "0,00";
            txtValMulInf.Enabled = txtValCorInf.Enabled = txtValAdcInf.Enabled = txtValDesInf.Enabled = false;
            MontaGridFormPagamento();
        }
        #endregion

        protected void btnQuitarTitulo_Click(object sender, EventArgs e)
        {
            if (lblDocNum.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Primeiro selecione um título.");
                return;
            }

            int coCaixa = int.Parse(Session[SessoesHttp.CodigoCaixa].ToString());

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
                        if (((TextBox)row.Cells[3].FindControl("txtValorFP")).Text != "")
                            dcmTotalGridFormPag = dcmTotalGridFormPag + Decimal.Parse(((TextBox)row.Cells[3].FindControl("txtValorFP")).Text);
                    }
                    dcmDifValor = Decimal.Parse(lblValTotInf.Text) - dcmTotalGridFormPag;
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

                DateTime dataCadastro = DateTime.Parse(lblDtEmissao.Text);
                int intNuPar = int.Parse(lblDocParc.Text);

                var ocoTb296 = (from iTb296 in TB296_CAIXA_MOVIMENTO.RetornaTodosRegistros()
                                where iTb296.CO_EMP == LoginAuxili.CO_EMP && iTb296.NU_DOC_CAIXA == lblDocNum.Text && iTb296.NU_PAR_DOC_CAIXA.Value == intNuPar
                                    && iTb296.DT_DOC_CAIXA.Value.Year == dataCadastro.Year && iTb296.DT_DOC_CAIXA.Value.Month == dataCadastro.Month
                                    && iTb296.DT_DOC_CAIXA.Value.Day == dataCadastro.Day
                                select iTb296).ToList();

                foreach (var iTb296 in ocoTb296)
                {
                    var ocoTb156 = (from iTb156 in TB156_FormaPagamento.RetornaTodosRegistros()
                                    where iTb156.CO_CAIXA_MOVIMENTO == iTb296.CO_SEQMOV_CAIXA
                                    select iTb156).ToList();

                    foreach (var viTb156 in ocoTb156)
                    {
                        GestorEntities.Delete(viTb156, true);
                    }

                    GestorEntities.Delete(iTb296, true);
                }

                TB296_CAIXA_MOVIMENTO tb296 = new TB296_CAIXA_MOVIMENTO();

                tb296.TB295_CAIXA = TB295_CAIXA.RetornaPelaChavePrimaria(varTb295.CO_EMP, varTb295.CO_CAIXA, varTb295.CO_COLABOR_CAIXA, varTb295.DT_MOVIMENTO);
                tb296.CO_USUARIO = varTb295.CO_USUARIO_ABERT;
                tb296.DT_REGISTRO = DateTime.Now;
                tb296.FLA_SITU_DOC = "A";          

                if (chkCredito.Checked)
                {
                    var varTb47 = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   where tb47.CO_EMP == LoginAuxili.CO_EMP && tb47.NU_DOC == lblDocNum.Text && tb47.NU_PAR == intNuPar
                                   && tb47.DT_CAD_DOC.Year == dataCadastro.Year && tb47.DT_CAD_DOC.Month == dataCadastro.Month
                                   && tb47.DT_CAD_DOC.Day == dataCadastro.Day
                                   select tb47).FirstOrDefault();

                    if (varTb47 != null)
                    {
                        varTb47.TB086_TIPO_DOCReference.Load();
                        varTb47.TB56_PLANOCTAReference.Load();
                        varTb47.TB39_HISTORICOReference.Load();
                        varTb47.TB099_CENTRO_CUSTOReference.Load();
                        tb296.CO_TIPO_DOC = varTb47.TB086_TIPO_DOC != null ? (int?)varTb47.TB086_TIPO_DOC.CO_TIPO_DOC : null;
                        tb296.DT_VEN_DOC = varTb47.DT_VEN_DOC;
                        tb296.CO_SEQU_PC = varTb47.TB56_PLANOCTA != null ? (int?)varTb47.TB56_PLANOCTA.CO_SEQU_PC : null;
                        tb296.NU_DOC_CAIXA = varTb47.NU_DOC;
                        tb296.NU_PAR_DOC_CAIXA = varTb47.NU_PAR;
                        tb296.DT_DOC_CAIXA = varTb47.DT_CAD_DOC;
                        tb296.CO_EMP_CAIXA = varTb47.CO_EMP;
                        tb296.DT_PAGTO_DOC = DateTime.Parse(txtDataQuitacao.Text);
                        tb296.VR_DOCU = varTb47.VR_PAR_DOC;
                        tb296.CO_HISTORICO = varTb47.TB39_HISTORICO != null ? (int?)varTb47.TB39_HISTORICO.CO_HISTORICO : null;
                        tb296.CO_CENT_CUSTO = varTb47.TB099_CENTRO_CUSTO != null ? (int?)varTb47.TB099_CENTRO_CUSTO.CO_CENT_CUSTO : null;
                        tb296.IDEBANCO = varTb47.IDEBANCO;
                        tb296.NU_AGE_DOC = varTb47.CO_AGENCIA != null ? varTb47.CO_AGENCIA.ToString() : null;
                        tb296.NU_CTA_DOC = varTb47.NU_CTA_DOC;
                        tb296.TP_CLIENTE_DOC = varTb47.TP_CLIENTE_DOC;
                        tb296.DE_OBS_DOC = varTb47.DE_OBS;
                        tb296.VR_MUL_DOC = txtValMulInf.Text != "" ? (decimal?)Decimal.Parse(txtValMulInf.Text) : null;
                        tb296.VR_JUR_DOC = txtValCorInf.Text != "" ? (decimal?)Decimal.Parse(txtValCorInf.Text) : null;
                        tb296.VR_DES_DOC = txtValDesInf.Text != "" ? (decimal?)Decimal.Parse(txtValDesInf.Text) : null;
                        tb296.VR_ADC_DOC = txtValAdcInf.Text != "" ? (decimal?)Decimal.Parse(txtValAdcInf.Text) : null;
                        tb296.VR_LIQ_DOC = lblValTotInf.Text != "" ? (decimal?)Decimal.Parse(lblValTotInf.Text) : null;
                        tb296.TP_OPER_CAIXA = "C";

                        if (chkBenif1.Checked)
                            tb296.CO_ALU = varTb47.CO_ALU; //int.Parse(ddlBenef.SelectedValue);
                        else
                            tb296.CO_CLIENTE = int.Parse(ddlBenef.SelectedValue);

                        TB296_CAIXA_MOVIMENTO.SaveOrUpdate(tb296, true);

                        varTb47.VR_JUR_PAG = txtValCorInf.Text != "" ? (decimal?)Decimal.Parse(txtValCorInf.Text) : null;
                        varTb47.VR_MUL_PAG = txtValMulInf.Text != "" ? (decimal?)Decimal.Parse(txtValMulInf.Text) : null;
                        varTb47.VR_DES_PAG = txtValDesInf.Text != "" ? (decimal?)Decimal.Parse(txtValDesInf.Text) : null;
                        varTb47.VR_PAG = lblValTotInf.Text != "" ? (decimal?)Decimal.Parse(lblValTotInf.Text) : null;
                        varTb47.DT_REC_DOC = DateTime.Parse(txtDataQuitacao.Text);
                        varTb47.CO_CAIXA = coCaixa;
                        varTb47.CO_COL_BAIXA = varTb295.CO_COLABOR_CAIXA;
                        varTb47.DT_MOV_CAIXA = DateTime.Now;
                        varTb47.DT_ALT_REGISTRO = DateTime.Now;
                        varTb47.IC_SIT_DOC = "Q";
                        varTb47.FL_ORIGEM_PGTO = "C";

                        int ctTipoRecbto = 0;
                        int idTipoRecbto = 0;

                        foreach (GridViewRow linha in grdFormPag.Rows)
                        {
                            if (((TextBox)linha.Cells[3].FindControl("txtValorFP")).Text != "")
                            {
                                if (Decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtValorFP")).Text) > 0)
                                {
                                    ctTipoRecbto++;
                                    if ((((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Text == "") && (int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value) != 5))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser informada.");
                                        return;
                                    }

                                    TB156_FormaPagamento tb156 = new TB156_FormaPagamento();

                                    tb156.VR_RECEBIDO = Decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtValorFP")).Text);
                                    tb156.DE_OBS = ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Text != "" ? ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Text : null;
                                    tb156.CO_TIPO_REC = int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value);
                                    tb156.CO_STATUS = "A";
                                    tb156.DT_STATUS = DateTime.Now;
                                    tb156.DT_CADASTRO = DateTime.Now;
                                    idTipoRecbto = int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value);

                                    if (int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value) != 5)
                                    {
                                        if (int.Parse(((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Text) == 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser maior que zero.");
                                            return;
                                        }

                                        tb156.NU_QTDE = int.Parse(((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Text);
                                    }
                                    else
                                        tb156.NU_QTDE = 0;
                                    tb156.CO_CAIXA_MOVIMENTO = tb296.CO_SEQMOV_CAIXA;

                                    TB156_FormaPagamento.SaveOrUpdate(tb156, true);
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
                        
                        TB47_CTA_RECEB.SaveOrUpdate(varTb47, true);
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
                                   where tb38.CO_EMP == LoginAuxili.CO_EMP && tb38.NU_DOC == lblDocNum.Text && tb38.NU_PAR == intNuPar
                                   && tb38.DT_CAD_DOC.Year == dataCadastro.Year && tb38.DT_CAD_DOC.Month == dataCadastro.Month
                                   && tb38.DT_CAD_DOC.Day == dataCadastro.Day
                                   select tb38).FirstOrDefault();

                    if (varTb38 != null)
                    {
                        varTb38.TB39_HISTORICOReference.Load();
                        varTb38.TB099_CENTRO_CUSTOReference.Load();
                        varTb38.TB30_AGENCIAReference.Load();
                        varTb38.TB086_TIPO_DOCReference.Load();
                        varTb38.TB56_PLANOCTAReference.Load();

                        tb296.CO_TIPO_DOC = varTb38.TB086_TIPO_DOC != null ? (int?)varTb38.TB086_TIPO_DOC.CO_TIPO_DOC : null;
                        tb296.DT_VEN_DOC = varTb38.DT_VEN_DOC;
                        tb296.CO_SEQU_PC = varTb38.TB56_PLANOCTA != null ? (int?)varTb38.TB56_PLANOCTA.CO_SEQU_PC : null;
                        tb296.NU_DOC_CAIXA = varTb38.NU_DOC;
                        tb296.VR_DOCU = varTb38.VR_PAR_DOC;
                        tb296.CO_HISTORICO = varTb38.TB39_HISTORICO != null ? (int?)varTb38.TB39_HISTORICO.CO_HISTORICO : null;
                        tb296.CO_CENT_CUSTO = varTb38.TB099_CENTRO_CUSTO != null ? (int?)varTb38.TB099_CENTRO_CUSTO.CO_CENT_CUSTO : null;
                        tb296.IDEBANCO = varTb38.TB30_AGENCIA != null ? varTb38.TB30_AGENCIA.IDEBANCO : null;
                        tb296.NU_AGE_DOC = varTb38.TB30_AGENCIA != null ? varTb38.TB30_AGENCIA.CO_AGENCIA.ToString() : null;
                        tb296.NU_CTA_DOC = varTb38.NU_CTA_DOC;
                        tb296.NU_PAR_DOC_CAIXA = varTb38.NU_PAR;
                        tb296.DT_DOC_CAIXA = varTb38.DT_CAD_DOC;
                        tb296.DT_PAGTO_DOC = DateTime.Parse(txtDataQuitacao.Text);
                        tb296.TP_CLIENTE_DOC = "F";
                        tb296.CO_EMP_CAIXA = varTb38.CO_EMP;
                        tb296.DE_OBS_DOC = varTb38.DE_OBS;
                        tb296.VR_MUL_DOC = txtValMulInf.Text != "" ? (decimal?)Decimal.Parse(txtValMulInf.Text) : null;
                        tb296.VR_JUR_DOC = txtValCorInf.Text != "" ? (decimal?)Decimal.Parse(txtValCorInf.Text) : null;
                        tb296.VR_DES_DOC = txtValDesInf.Text != "" ? (decimal?)Decimal.Parse(txtValDesInf.Text) : null;
                        tb296.VR_ADC_DOC = txtValAdcInf.Text != "" ? (decimal?)Decimal.Parse(txtValAdcInf.Text) : null;
                        tb296.VR_LIQ_DOC = lblValTotInf.Text != "" ? (decimal?)Decimal.Parse(lblValTotInf.Text) : null;
                        tb296.CO_FORN = int.Parse(ddlBenef.SelectedValue);
                        tb296.TP_OPER_CAIXA = "D";

                        TB296_CAIXA_MOVIMENTO.SaveOrUpdate(tb296, true);

                        varTb38.VR_JUR_PAG = txtValCorInf.Text != "" ? (decimal?)Decimal.Parse(txtValCorInf.Text) : null;
                        varTb38.VR_MUL_PAG = txtValMulInf.Text != "" ? (decimal?)Decimal.Parse(txtValMulInf.Text) : null;
                        varTb38.VR_DES_PAG = txtValDesInf.Text != "" ? (decimal?)Decimal.Parse(txtValDesInf.Text) : null;
                        varTb38.VR_PAG = lblValTotInf.Text != "" ? (decimal?)Decimal.Parse(lblValTotInf.Text) : null;
                        varTb38.DT_REC_DOC = DateTime.Parse(txtDataQuitacao.Text);
                        varTb38.DT_ALT_REGISTRO = DateTime.Now;
                        varTb38.IC_SIT_DOC = "Q";
                        varTb38.FL_ORIGEM_PGTO = "C";

                        TB38_CTA_PAGAR.SaveOrUpdate(varTb38, true);

                        foreach (GridViewRow linha in grdFormPag.Rows)
                        {
                            if (((TextBox)linha.Cells[3].FindControl("txtValorFP")).Text != "")
                            {
                                if ((((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Text == "") && (int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value) != 5))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser informada.");
                                    return;
                                }

                                TB156_FormaPagamento tb156 = new TB156_FormaPagamento();

                                tb156.VR_RECEBIDO = Decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtValorFP")).Text);
                                tb156.DE_OBS = ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Text != "" ? ((TextBox)linha.Cells[4].FindControl("txtObservacao")).Text : null;
                                tb156.CO_TIPO_REC = int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value);
                                tb156.CO_STATUS = "A";
                                tb156.DT_STATUS = DateTime.Now;
                                tb156.DT_CADASTRO = DateTime.Now;

                                if (int.Parse(((HiddenField)linha.Cells[4].FindControl("hdCO_TIPO_REC")).Value) != 5)
                                {
                                    if (int.Parse(((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Text) == 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade da forma de pagamento deve ser informada.");
                                        return;
                                    }

                                    tb156.NU_QTDE = int.Parse(((TextBox)linha.Cells[2].FindControl("txtQtdeFP")).Text);
                                }
                                else
                                    tb156.NU_QTDE = 0;

                                tb156.CO_CAIXA_MOVIMENTO = tb296.CO_SEQMOV_CAIXA;

                                TB156_FormaPagamento.SaveOrUpdate(tb156, true);
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

        protected void chkCredito_CheckedChanged(object sender, EventArgs e)
        {
            MontaGridFormPagamento();
            chkDebito.Checked = false;

            if (chkBenif1.Text != "Aluno")
            {
                grdContratos.DataSource = null;
                grdContratos.DataBind();
                chkBenif1.Text = "Aluno";
                chkBenif1.Checked = chkBenif2.Visible = true;
                chkBenif2.Checked = false;

                ListaBeneficiarios("C", "A");
                ListaAgrupador("R");

                int codBenef = ddlBenef.Items.Count > 0 ? int.Parse(ddlBenef.SelectedValue) : 0;
                int codAgrup = int.Parse(ddlAgrupador.SelectedValue);
                MontaGridContratos("C", "A", codBenef, codAgrup);
            }
        }

        protected void chkDebito_CheckedChanged(object sender, EventArgs e)
        {
            MontaGridFormPagamento();
            chkCredito.Checked = false;

            if (chkBenif1.Text != "Fornecedor")
            {
                grdContratos.DataSource = null;
                grdContratos.DataBind();
                chkBenif1.Text = "Fornecedor";
                chkBenif1.Checked = true;
                chkBenif2.Visible = chkBenif2.Checked = false;

                ListaBeneficiarios("D", "F");
                ListaAgrupador("D");

                int codBenef = ddlBenef.Items.Count > 0 ? int.Parse(ddlBenef.SelectedValue) : 0;
                int codAgrup = int.Parse(ddlAgrupador.SelectedValue);
                MontaGridContratos("D", "F", codBenef, codAgrup);
            }
        }

        protected void chkBenif1_CheckedChanged(object sender, EventArgs e)
        {
            chkBenif1.Checked = true;

            if (chkBenif2.Checked)
            {
                ListaBeneficiarios("C", "A");

                int codBenef = ddlBenef.Items.Count > 0 ? int.Parse(ddlBenef.SelectedValue) : 0;
                int codAgrup = int.Parse(ddlAgrupador.SelectedValue);
                MontaGridContratos("C", "A", codBenef, codAgrup);
            }

            chkBenif2.Checked = false;
        }

        protected void chkBenif2_CheckedChanged(object sender, EventArgs e)
        {
            chkBenif2.Checked = true;

            if (chkBenif1.Checked)
            {
                ListaBeneficiarios("C", "C");

                int codBenef = ddlBenef.Items.Count > 0 ? int.Parse(ddlBenef.SelectedValue) : 0;
                int codAgrup = int.Parse(ddlAgrupador.SelectedValue);
                MontaGridContratos("C", "C", codBenef, codAgrup);
            }

            chkBenif1.Checked = false;
        }

        protected void grdContratos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dataVencto = DateTime.Parse(e.Row.Cells[5].Text);

                    if (dataVencto < DateTime.Now)
                        e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(242, 220, 219);
                    else
                    {
                        e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                        e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
                    }
                }
                else
                {
                    e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#DDDDDD'; this.style.cursor='hand';");
                    e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'");
                }
            }            
        }

        protected void grdContratos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdContratos.Rows.Count > 0)
            {
                DateTime dtCadas = DateTime.Parse(grdContratos.SelectedRow.Cells[4].Text);

                string strTpBenef = "";

                if (chkCredito.Checked)
                    strTpBenef = chkBenif1.Checked ? "A" : "C";
                else
                    strTpBenef = "F";

                MontaContratoSelecionado(grdContratos.SelectedRow.Cells[2].Text, int.Parse(grdContratos.SelectedRow.Cells[3].Text), dtCadas, strTpBenef);
            }
        }

        protected void ddlBenef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBenef.Items.Count > 0)
            {
                string strTpMov = chkCredito.Checked ? "C" : "D";
                string strTpBenef = "";
                int codBenef = ddlBenef.Items.Count > 0 ? int.Parse(ddlBenef.SelectedValue) : 0;


                if (chkCredito.Checked)
                    strTpBenef = chkBenif1.Checked ? "A" : "C";
                else
                    strTpBenef = "F";

                int codAgrup = int.Parse(ddlAgrupador.SelectedValue);
                MontaGridContratos(strTpMov, strTpBenef, codBenef, codAgrup);
            }
        }

        protected void ddlAgrupador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTpMov = chkCredito.Checked ? "C" : "D";
            string strTpBenef = "";
            int codBenef = ddlBenef.Items.Count > 0 ? int.Parse(ddlBenef.SelectedValue) : 0;

            if (chkCredito.Checked)
                strTpBenef = chkBenif1.Checked ? "A" : "C";
            else
                strTpBenef = "F";

            int codAgrup = int.Parse(ddlAgrupador.SelectedValue);
            MontaGridContratos(strTpMov, strTpBenef, codBenef, codAgrup);
        }

        protected void txtValMulInf_TextChanged(object sender, EventArgs e)
        {
            decimal dcmValAdcInf = 0;
            decimal dcmAboMul = 0;
            decimal dcmAboCor = 0;
            decimal dcmAboDes = 0;
            int coCaixa = int.Parse(Session[SessoesHttp.CodigoCaixa].ToString());

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
                if ((varTb295.VR_PERCENTUAL_ABONO_MULTA != null) && (decimal.TryParse(txtValMulInf.Text, out dcmAboMul)))
                {
                    if (decimal.Parse(lblValMulSis.Text) > 0)
                    {
                        dcmAboMul = decimal.Parse(lblValMulSis.Text) - (decimal.Parse(lblValMulSis.Text) * Decimal.Parse(String.Format("{0:N}", varTb295.VR_PERCENTUAL_ABONO_MULTA)) / 100);

                        if (decimal.Parse(txtValMulInf.Text) < dcmAboMul)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Abono de Multa não pode ser superior ao porcentual permitido ao usuário caixa.");
                            txtValMulInf.Text = "";
                            txtValMulInf.Text = lblValMulSis.Text;
                            dcmAboMul = decimal.Parse(lblValMulSis.Text);
                            return;
                        }
                        else
                            dcmAboMul = decimal.Parse(txtValMulInf.Text);
                    }
                }
                else
                {
                    if (!decimal.TryParse(txtValMulInf.Text, out dcmAboMul))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número não válido.");
                        txtValMulInf.Text = lblValMulSis.Text;
                        //return;
                    }
                }

                if (decimal.TryParse(txtValCorInf.Text, out dcmAboCor))
                    dcmAboCor = decimal.Parse(txtValCorInf.Text);
                else
                    dcmAboCor = 0;

                if (decimal.TryParse(txtValAdcInf.Text, out dcmValAdcInf))
                    dcmValAdcInf = decimal.Parse(txtValAdcInf.Text);
                else
                    dcmValAdcInf = 0;

                if (decimal.TryParse(txtValDesInf.Text, out dcmAboDes))
                    dcmAboDes = decimal.Parse(txtValDesInf.Text);
                else
                    dcmAboDes = 0;

                lblValSubTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + dcmAboMul + dcmAboCor + dcmValAdcInf);
                lblValTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + dcmAboMul + dcmAboCor - dcmAboDes + dcmValAdcInf);
            }
        }

        protected void txtValCorInf_TextChanged(object sender, EventArgs e)
        {
            decimal valAdcInf = 0;
            decimal aboMul = 0;
            decimal aboCor = 0;
            decimal aboDes = 0;
            int coCaixa = int.Parse(Session[SessoesHttp.CodigoCaixa].ToString());

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
                if ((varTb295.VR_PERCENTUAL_ABONO_CORRECAO != null) && (decimal.TryParse(txtValCorInf.Text, out aboCor)))
                {
                    if (decimal.Parse(lblValCorSis.Text) > 0)
                    {
                        aboCor = decimal.Parse(lblValCorSis.Text) - (decimal.Parse(lblValCorSis.Text) * Decimal.Parse(String.Format("{0:N}", varTb295.VR_PERCENTUAL_ABONO_CORRECAO)) / 100);

                        if (decimal.Parse(txtValCorInf.Text) < aboCor)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Abono de Correção não pode ser superior ao porcentual permitido ao usuário caixa.");
                            txtValCorInf.Text = lblValCorSis.Text;
                            aboCor = decimal.Parse(lblValCorSis.Text);
                            //return;
                        }
                        else
                            aboCor = decimal.Parse(txtValCorInf.Text);
                    }
                }
                else
                {
                    if (!decimal.TryParse(txtValCorInf.Text, out aboCor))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número não válido.");
                        txtValCorInf.Text = lblValCorSis.Text;
                        // return;
                    }
                }

                if (decimal.TryParse(txtValMulInf.Text, out aboMul))
                    aboMul = decimal.Parse(txtValMulInf.Text);
                else
                    aboMul = 0;

                if (decimal.TryParse(txtValAdcInf.Text, out valAdcInf))
                    valAdcInf = decimal.Parse(txtValAdcInf.Text);
                else
                    valAdcInf = 0;

                if (decimal.TryParse(txtValDesInf.Text, out aboDes))
                    aboDes = decimal.Parse(txtValDesInf.Text);
                else
                    aboDes = 0;

                lblValSubTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + aboMul + aboCor + valAdcInf);
                lblValTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + aboMul + aboCor - aboDes + valAdcInf);
            }
        }

        protected void txtValAdcInf_TextChanged(object sender, EventArgs e)
        {
            decimal valAdcInf = 0;
            decimal aboMul = 0;
            decimal aboCor = 0;
            decimal aboDes = 0;

            if (decimal.TryParse(txtValAdcInf.Text, out valAdcInf))
                valAdcInf = decimal.Parse(txtValAdcInf.Text);
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor Adicionado informado não pode ser gerado.");
                txtValAdcInf.Text = lblValAdcSis.Text;
                //return;
            }

            if (decimal.TryParse(txtValMulInf.Text, out aboMul))
                aboMul = decimal.Parse(txtValMulInf.Text);
            else
                aboMul = 0;

            if (decimal.TryParse(txtValCorInf.Text, out aboCor))
                aboCor = decimal.Parse(txtValCorInf.Text);
            else
                aboCor = 0;

            if (decimal.TryParse(txtValDesInf.Text, out aboDes))
                aboDes = decimal.Parse(txtValDesInf.Text);
            else
                aboDes = 0;

            lblValSubTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + aboMul + aboCor + valAdcInf);
            lblValTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + aboMul + aboCor - aboDes + valAdcInf);
        }

        protected void txtValDesInf_TextChanged(object sender, EventArgs e)
        {
            decimal valAdcInf = 0;
            decimal aboMul = 0;
            decimal aboCor = 0;
            decimal aboDes = 0;

            int coCaixa = int.Parse(Session[SessoesHttp.CodigoCaixa].ToString());

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
                if ((varTb295.VR_PERCENTUAL_ABONO_DESCONTO != null) && (decimal.TryParse(txtValDesInf.Text, out aboDes)))
                {
                    if (decimal.Parse(lblValDesctoSis.Text) > 0)
                    {
                        aboDes = (decimal.Parse(lblValDesctoSis.Text) * Decimal.Parse(String.Format("{0:N}", varTb295.VR_PERCENTUAL_ABONO_DESCONTO)) / 100) + decimal.Parse(lblValDesctoSis.Text);

                        if (decimal.Parse(txtValDesInf.Text) > aboDes)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Abono de Desconto não pode ser superior ao porcentual permitido ao usuário caixa.");
                            txtValDesInf.Text = lblValDesctoSis.Text;
                            aboDes = decimal.Parse(lblValDesctoSis.Text);
                            //return;
                        }
                        else
                            aboDes = decimal.Parse(txtValDesInf.Text);
                    }
                }
                else
                {
                    if (!decimal.TryParse(txtValDesInf.Text, out aboDes))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número não válido.");
                        txtValDesInf.Text = lblValDesctoSis.Text;
                        //return;
                    }
                }

                if (decimal.TryParse(txtValMulInf.Text, out aboMul))
                    aboMul = decimal.Parse(txtValMulInf.Text);
                else
                    aboMul = 0;

                if (decimal.TryParse(txtValCorInf.Text, out aboCor))
                    aboCor = decimal.Parse(txtValCorInf.Text);
                else
                    aboCor = 0;

                if (decimal.TryParse(txtValAdcInf.Text, out valAdcInf))
                    valAdcInf = decimal.Parse(txtValAdcInf.Text);
                else
                    valAdcInf = 0;

                lblValSubTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + aboMul + aboCor + valAdcInf);
                lblValTotInf.Text = String.Format("{0:N}", Decimal.Parse(lblValDoctoInf.Text) + aboMul + aboCor - aboDes + valAdcInf);
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
                        if ((linha.Cells[2].Text == lblDocNum.Text) && (int.Parse(linha.Cells[3].Text) == int.Parse(lblDocParc.Text)))
                            ((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked = false;
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
                        DateTime dataCadastro = DateTime.Parse(linha.Cells[4].Text);

                        string tpBenef = "";

                        if (chkCredito.Checked)
                            tpBenef = chkBenif1.Checked ? "A" : "C";
                        else
                            tpBenef = "F";

                        MontaContratoSelecionado(linha.Cells[2].Text, int.Parse(linha.Cells[3].Text), dataCadastro, tpBenef);
                    }
                }
            }

            if (!chkContrato)
                LimpaCampos();
        }
    }
}