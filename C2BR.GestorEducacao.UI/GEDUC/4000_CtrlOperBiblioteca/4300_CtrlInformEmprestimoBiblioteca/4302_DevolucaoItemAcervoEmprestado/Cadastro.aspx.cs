//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: REGISTRO E BAIXA DE EMPRÉSTIMOS
// OBJETIVO: DEVOLUÇÃO DE ITENS DE ACERVO BIBLIOGRÁFICO EMPRESTADOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4302_DevolucaoItemAcervoEmprestado
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variáveis

        private static List<ItensBaixa> lstItensBaixa;

        #endregion        

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;
            txtDataBaixa.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (IsPostBack) return;

            lstItensBaixa = new List<ItensBaixa>();
            grdItensEmprestados.DataSource = null;
            grdItensEmprestados.DataBind();
            grdItensBaixa.DataSource = null;
            grdItensBaixa.DataBind();
            CarregaUnidade();
            CarregaDadosBoleto();
        }

//====> Processo de Baixa de Empréstimo de Biblioteca
        protected void btnBaixaEmpre_Click(object sender, EventArgs e)
        {
            int intIndiceItem = 0;

            if (lstItensBaixa.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Deve ser selecionado pelo menos um item para baixa.");
                return;
            }

            TB123_EMPR_BIB_ITENS tb123 = new TB123_EMPR_BIB_ITENS();

            foreach (ItensBaixa iItensBaixa in lstItensBaixa)
            {
                TB204_ACERVO_ITENS tb204 = TB204_ACERVO_ITENS.RetornaPelaChavePrimaria(iItensBaixa.ORG_CODIGO_ORGAO, iItensBaixa.CO_ISBN_ACER, 
                                                          iItensBaixa.CO_ACERVO_AQUISI, iItensBaixa.CO_ACERVO_ITENS, iItensBaixa.CO_EMP_ITENS);

                tb123 = TB123_EMPR_BIB_ITENS.RetornaPelaChavePrimaria(iItensBaixa.CO_EMPR_BIB_ITENS);

                tb123.TB03_COLABORReference.Load();
                tb123.TB36_EMPR_BIBLIOTReference.Load();
                tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOTReference.Load();
                tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNOReference.Load();
                tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB03_COLABORReference.Load();
                tb123.DT_REAL_DEVO_ACER = DateTime.Now;
                tb123.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                if (((TextBox)grdItensBaixa.Rows[intIndiceItem].Cells[5].FindControl("txtPagEntrega")).Text != "")
                    tb123.NU_PAGINA_ACERVO_BAIXA = int.Parse(((TextBox)grdItensBaixa.Rows[intIndiceItem].Cells[5].FindControl("txtPagEntrega")).Text.Trim());

                if (((TextBox)grdItensBaixa.Rows[intIndiceItem].Cells[3].FindControl("txtOBSEntrega")).Text != "")
                    tb123.DE_OBS_EMP_BAIXA = ((TextBox)grdItensBaixa.Rows[intIndiceItem].Cells[3].FindControl("txtOBSEntrega")).Text.Trim();

                tb123.CO_ISEN_MULT_ACER = ((CheckBox)grdItensBaixa.Rows[intIndiceItem].Cells[7].FindControl("ckIsento")).Checked ? "S" : "N";

                if (((CheckBox)grdItensBaixa.Rows[intIndiceItem].Cells[7].FindControl("ckIsento")).Checked)
                    tb123.VL_MULT_RECE_ACER = 0;
                else
                    tb123.VL_MULT_RECE_ACER = Decimal.Parse(((TextBox)grdItensBaixa.Rows[intIndiceItem].Cells[6].FindControl("txtMultaPag")).Text);

                tb123.VL_MULT_ATRA_ACER = Decimal.Parse(((TextBox)grdItensBaixa.Rows[intIndiceItem].Cells[6].FindControl("txtMultaPag")).Text);

//------------> Faz a atualização dos campos do item NU_PAGINA_ACERVO_BAIXA, DE_OBS_EMP_BAIXA, CO_ISEN_MULT_ACER, VL_MULT_RECE_ACER, VL_MULT_ATRA_ACER
                if (GestorEntities.SaveOrUpdate(tb123) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar Baixa do item de empréstimo");
                    return;
                }

//------------> Faz a alteração da situação do Livro para Disponivel
                tb204.CO_SITU_ACERVO_ITENS = "D";

                if (GestorEntities.SaveOrUpdate(tb204) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao atualizar item do acervo");
                    return;
                }
                intIndiceItem = intIndiceItem + 1;
            }

//------------> Faz a verificação se o checkbox de impressão de boleto está marcado, se sim, gera o título no contas a receber e gera o boleto para o pagamento
            if (ckImprimeBoleto.Checked)
            {
                decimal dcmValorPago;

                if (txtValorPago.Text == "" || !decimal.TryParse(txtValorPago.Text, out dcmValorPago))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Informe Valor Pago válido.");
                    return;
                }
                else
                {
//----------------> Grava título no contas a receber
                    GeraTituloTB47(tb123, dcmValorPago);
                    LimpaCampo();
                }
            }
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Baixa Realizada com Sucesso", Request.Url.AbsoluteUri);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que preenche o Grid de Itens de Baixa com o Itens selecionado no Grid de Itens Emprestados
        /// </summary>
        protected void PreencheGridItensBaixa()
        {
            grdItensBaixa.DataKeyNames = new string[] { "CO_EMPR_BIB_ITENS", "ORG_CODIGO_ORGAO", "CO_ACERVO_AQUISI", "CO_ACERVO_ITENS", "CO_EMP_ITENS", "RowGridEmprestimo" };

            foreach (GridViewRow linha in grdItensEmprestados.Rows)
            {
//------------> Faz a verificação dos itens marcados na Grid de Itens Emprestados
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    string strObsEmpr = grdItensEmprestados.DataKeys[linha.RowIndex].Values[0].ToString();
                    int intNumPagEmpr = Convert.ToInt32(grdItensEmprestados.DataKeys[linha.RowIndex].Values[1].ToString());
                    int intCoEmprBibItens = Convert.ToInt32(grdItensEmprestados.DataKeys[linha.RowIndex].Values[2].ToString());
                    int intOrgCodigoOrgao = Convert.ToInt32(grdItensEmprestados.DataKeys[linha.RowIndex].Values[3].ToString());
                    int intCoAcervoAquisi = Convert.ToInt32(grdItensEmprestados.DataKeys[linha.RowIndex].Values[4].ToString());
                    int intCoAcervoItens = Convert.ToInt32(grdItensEmprestados.DataKeys[linha.RowIndex].Values[5].ToString());
                    int intCoEmpItens = Convert.ToInt32(grdItensEmprestados.DataKeys[linha.RowIndex].Values[6].ToString());

                    Decimal dcmCoIsbnAcer = 0;
                    if (linha.Cells[1].Text.Trim() != "")
                        dcmCoIsbnAcer = Decimal.Parse(linha.Cells[1].Text.Trim().Replace("-", ""));

                    string strNumEmpr = linha.Cells[4].Text.Trim();
                    string strCodigoInterno = linha.Cells[2].Text.Trim();

                    string strTituloObra = TB35_ACERVO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO, dcmCoIsbnAcer).NO_ACERVO;

                    int intQtdDiasAtraso = 0;
                    if (linha.Cells[6].Text.Trim() != "")
                        intQtdDiasAtraso = int.Parse(linha.Cells[6].Text.Trim());

                    Decimal dcmVlMulta = 0;
                    if (linha.Cells[7].Text.Trim() != "")
                        dcmVlMulta = Decimal.Parse(linha.Cells[7].Text.Trim());

//----------------> Faz a verificação para saber se itens já existem no ItensBaixaSelecionados
                    if (!lstItensBaixa.Exists(r => r.idItenBaixa == strNumEmpr + strCodigoInterno))
                    {
                        lstItensBaixa.Add(new ItensBaixa
                        {
                            RowGridEmprestimo = linha.RowIndex, CO_EMPR_BIB_ITENS = intCoEmprBibItens, ORG_CODIGO_ORGAO = intOrgCodigoOrgao,
                            CO_ACERVO_AQUISI = intCoAcervoAquisi, CO_ACERVO_ITENS = intCoAcervoItens, CO_EMP_ITENS = intCoEmpItens,
                            CO_ISBN_ACER = dcmCoIsbnAcer, idItenBaixa = strNumEmpr + strCodigoInterno, CodigoInterno = strCodigoInterno,
                            IsentaMulta = "N", NumPagEntrega = 0, ObsEntrega = "", TituloObra = strTituloObra, NumPagEmpr = intNumPagEmpr,
                            ObsEmpr = strObsEmpr, VlMulta = dcmVlMulta, QtdDiasAtraso = intQtdDiasAtraso
                        });
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemSucesso(this, "Item já foi selecionado para baixa!");
                        ((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked = false;
                        return;
                    }

                    ((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked = false;
                }
            }

            txtTotalIsencao.Text = "";

            grdItensBaixa.DataSource = lstItensBaixa;
            grdItensBaixa.DataBind();

            CalculaVlMultaGridItensBaixa();
        }

        /// <summary>
        /// Método que limpa os campos informados
        /// </summary>
        protected void LimpaCampo()
        {
            ckImprimeBoleto.Checked = false;
            txtValorPago.Text = txtTotalMulta.Text = txtTotalIsencao.Text = ddlTipoUsuario.SelectedValue = ddlUsuario.SelectedValue = "";
            grdItensBaixa.Dispose();
            grdItensBaixa.DataBind();
            grdItensEmprestados.Dispose();
            grdItensEmprestados.DataBind();
        }

        /// <summary>
        /// Método que faz o cálculo dos Valores da Multa do Itens e preenche o campo txtTotalMulta com a soma
        /// </summary>
        protected void CalculaVlMultaGridItensBaixa()
        {
            Decimal dcmValolMultaTotal = 0;

//--------> Faz o somatório dos valores da multa do Itens
            foreach (GridViewRow linha in grdItensBaixa.Rows)
            {
                if (((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text.Trim() != "")
                    dcmValolMultaTotal = dcmValolMultaTotal + Decimal.Parse(((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text);
            }

            txtTotalMulta.Text = dcmValolMultaTotal.ToString();
        }

        /// <summary>
        /// Método que faz a Remoção do(s) Iten(s) de Baixa
        /// </summary>
        /// <param name="indexLinhaExcluir">Índice da linha a ser excluída</param>
        private void BindGridItemBaixa(int? indexLinhaExcluir)
        {
            int coEmprBibItem;

            if (indexLinhaExcluir.HasValue)
            {
                coEmprBibItem = int.Parse(grdItensBaixa.DataKeys[indexLinhaExcluir.Value].Values[0].ToString());

                if (lstItensBaixa.Exists(r => r.CO_EMPR_BIB_ITENS == coEmprBibItem))
                    lstItensBaixa.RemoveAll(o => o.CO_EMPR_BIB_ITENS == coEmprBibItem);
            }

            if (lstItensBaixa.Count() > 0)
                grdItensBaixa.DataSource = lstItensBaixa;
            else
                grdItensBaixa.Dispose();

            grdItensBaixa.DataBind();
            CalculaValorIsencao();
        }

        /// <summary>
        /// Método que faz o Cálculo do valor de isenção do Grid de Itens de Baixa
        /// </summary>
        protected void CalculaValorIsencao()
        {
            Decimal dcmValorTotalMulta = 0;
            Decimal dcmValorTotalIsencao = 0;

            foreach (GridViewRow linha in grdItensBaixa.Rows)
            {
//------------> Faz a verificação dos itens marcados na Grid
                if (((CheckBox)linha.Cells[7].FindControl("ckIsento")).Checked)
                {
                    if (((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text != "")
                    {
                        dcmValorTotalMulta = dcmValorTotalMulta + Decimal.Parse(((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text);
                        dcmValorTotalIsencao = dcmValorTotalIsencao + Decimal.Parse(((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text);
                    }
                }
                else
                {
                    if (((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text != "")
                        dcmValorTotalMulta = dcmValorTotalMulta + Decimal.Parse(((TextBox)linha.Cells[6].FindControl("txtMultaPag")).Text);
                }
            }

//--------> Valor Total de Multa de todos os itens menos(-) os itens com a flag "ckIsento" marcada
            txtTotalMulta.Text = (dcmValorTotalMulta - dcmValorTotalIsencao).ToString();
            txtTotalIsencao.Text = dcmValorTotalIsencao.ToString();
        }

        /// <summary>
        /// Método que gera Título no Contas a Receber
        /// </summary>
        /// <param name="tb123">Entidade TB123_EMPR_BIB_ITENS</param>
        /// <param name="dcmValorTotalTitulo">Valor total do empréstimo</param>
        private void GeraTituloTB47(TB123_EMPR_BIB_ITENS tb123, Decimal dcmValorTotalTitulo)
        {
            tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNO.TB108_RESPONSAVELReference.Load();
            if (tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNO.TB108_RESPONSAVEL == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Título pois o usuário não está associado a um responsável.");
                return;
            }

//--------> Carrega Informações da conta da Biblioteca
            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            if (tb25.CO_CTABIB_EMP == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Título pois o conta contábil de biblioteca não foi cadastrada na unidade.");
                return;
            }

            if (tb25.CO_CTA_CAIXA == null || tb25.CO_CTA_BANCO == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Título pois o conta contábil de caixa e banco devem ser cadastrados na unidade.");
                return;
            }

            TB47_CTA_RECEB tb47 = new TB47_CTA_RECEB();

            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb47.CO_EMP_UNID_CONT = LoginAuxili.CO_EMP;

            tb47.NU_DOC = "BI" + DateTime.Now.ToString("yy") + "." + tb123.CO_EMPR_BIB_ITENS.ToString().PadLeft(10,'0') + ".01";
            tb47.NU_PAR = 1;                                                                                         
            tb47.DT_CAD_DOC = DateTime.Now;                                                                           
            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);                          
            tb47.CO_NOS_NUM = null;                                                                                    
            tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);
            tb47.DE_COM_HIST = "Emprestimo de Biblioteca Nº:" + tb123.TB36_EMPR_BIBLIOT.CO_NUM_EMP;                
            tb47.DT_ALT_REGISTRO = DateTime.Now;                                                                    
            tb47.DT_VEN_DOC = DateTime.Now.AddDays(5);                                                                 
            tb47.IC_SIT_DOC = "A";                                                                         

            int idBoleto;
            tb47.TB227_DADOS_BOLETO_BANCARIO = int.TryParse(ddlBoleto.SelectedValue, out idBoleto) ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(idBoleto) : null;
            tb47.NU_CTA_DOC = tb25.CO_CTA_BAN_BIBLI;                                                          
            tb47.QT_PAR = 1;                                                                                           
            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTABIB_EMP.Value);
            tb47.CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO;
            tb47.CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA;
            tb47.CO_ALU = tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TP_USU_BIB == "A" ? tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNO.CO_ALU : (int?)null;
            tb47.TP_CLIENTE_DOC = "A";
            tb47.VR_PAR_DOC = dcmValorTotalTitulo;
            tb47.VR_TOT_DOC = dcmValorTotalTitulo;                                                                             
            tb47.DT_EMISS_DOCTO = DateTime.Now;                                                                          
            tb47.CO_FLAG_TP_VALOR_DES = "V";                                                                             
            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";                                                              
            tb47.CO_FLAG_TP_VALOR_JUR = "V";                                                                             
            tb47.CO_FLAG_TP_VALOR_MUL = "V";
            tb47.CO_FLAG_TP_VALOR_OUT = "V";
            tb47.FL_EMITE_BOLETO = "S";
            tb47.TB108_RESPONSAVEL = tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNO.TB108_RESPONSAVEL;

            GestorEntities.SaveOrUpdate(tb47);

//--------> Exibe o boleto passando como parâmetro a Entidade do Contas a Receber
            EmitirBoleto(tb47);
        }

        /// <summary>
        /// Método que Exibe o Boleto Bancário
        /// </summary>
        /// <param name="tb47">Entidade TB47_CTA_RECEB</param>
        private void EmitirBoleto(TB47_CTA_RECEB tb47)
        {
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            string strCnpjCPF = "";
            string strNomeBairro;
            string strMultaMoraDesc = "";
            string strInstruBoleto = "";

            tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
            if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Boleto Associado!");
                return;
            }
            tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
            tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
            tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
            tb47.TB108_RESPONSAVELReference.Load();
            tb47.TB103_CLIENTEReference.Load();

            if (tb47.TB108_RESPONSAVEL == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                return;
            }

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                return;
            }

//--------> Recebe a Unidade
            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            InformacoesBoletoBancario b = new InformacoesBoletoBancario();

//--------> Informações do Boleto
            b.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
            b.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;
            b.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
            b.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
            b.Valor = tb47.VR_TOT_DOC;
            b.Vencimento = tb47.DT_VEN_DOC; 

//--------> Informações do Cedente
            b.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;         

            b.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                        tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

            b.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
            b.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + '-' + tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim();
            b.CpfCnpjCedente = tb25.CO_CPFCGC_EMP;
            b.NomeCedente = tb25.NO_RAZSOC_EMP;

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
            {
                var desc = b.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                ; strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(b.Vencimento.Year, b.Vencimento.Month, 5).ToShortDateString() + ") o valor : " + string.Format("{0:C}", desc) + "<br>";
            }

//--------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
            if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                strInstruBoleto = tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                strInstruBoleto = strInstruBoleto + tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

            if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                strInstruBoleto = strInstruBoleto + tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

            b.Instrucoes = strInstruBoleto;

//--------> Chaves primárias do Contas a Receber
            b.CO_EMP = tb25.CO_EMP;
            b.NU_DOC = tb47.NU_DOC;
            b.NU_PAR = tb47.NU_PAR;
            b.DT_CAD_DOC = tb47.DT_CAD_DOC;

//--------> Informações da Multa
            strMultaMoraDesc += tb47.VR_MUL_DOC != null && tb47.VR_MUL_DOC.Value != 0 ?
                (tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
                (b.Valor * (decimal)tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

//--------> Informações da Mora
            strMultaMoraDesc += tb47.VR_JUR_DOC != null && tb47.VR_JUR_DOC.Value != 0 ?
                 (tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
                 (b.Valor * (decimal)tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
                    tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

//--------> Informações do desconto
            strMultaMoraDesc += tb47.VR_DES_DOC != null && tb47.VR_DES_DOC.Value != 0 ?
                 (tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + tb47.VR_DES_DOC.Value.ToString("0.00") + "% (R$ " +
                 (b.Valor * (decimal)tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
                    tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

//--------> Faz a adição de instruções ao Boleto
            b.Instrucoes += "(*) " + strMultaMoraDesc + "<br>";

//--------> Ano Refer: - Matrícula: - Nº NIRE:
//--------> Modalidade: - Série: - Turma: - Turno:
            var inforAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                              join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                              join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                              where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                              && tb08.CO_ALU == tb47.CO_ALU
                              select new
                              {
                                  tb08.TB44_MODULO.DE_MODU_CUR,
                                  tb01.NO_CUR,
                                  tb129.CO_SIGLA_TURMA,
                                  tb08.CO_ANO_MES_MAT,
                                  tb08.CO_ALU_CAD,
                                  tb08.TB07_ALUNO.NU_NIRE,
                                  tb08.TB07_ALUNO.NO_ALU,
                                  TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                              }).FirstOrDefault();

            if (inforAluno != null)
            {
                strCnpjCPF = "Ano Refer: " + inforAluno.CO_ANO_MES_MAT.Trim() + " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") + " - Nº NIRE: " +
                    inforAluno.NU_NIRE.ToString() + "<br> Modalidade: " + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                    " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO + " <br> Aluno(a): " + inforAluno.NO_ALU;
            }

            b.Instrucoes += strCnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";

            tb47.TB108_RESPONSAVEL.TB74_UFReference.Load();
            tb47.TB108_RESPONSAVEL.TB904_CIDADEReference.Load();
            strNomeBairro = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(tb47.TB108_RESPONSAVEL.CO_BAIRRO.ToString())).NO_BAIRRO;
            var cidade = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(tb47.TB108_RESPONSAVEL.CO_CIDADE.ToString()));

//--------> Informações do Sacado
            b.BairroSacado = strNomeBairro != "" ? strNomeBairro : "";
            b.CepSacado = tb47.TB108_RESPONSAVEL.CO_CEP_RESP;          
            b.CidadeSacado = cidade.NO_CIDADE;
            b.CpfCnpjSacado = tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Length >= 11 ? tb47.TB108_RESPONSAVEL.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.TB108_RESPONSAVEL.NU_CPF_RESP;
            b.EnderecoSacado = tb47.TB108_RESPONSAVEL.DE_ENDE_RESP + " " + tb47.TB108_RESPONSAVEL.NU_ENDE_RESP + " " + tb47.TB108_RESPONSAVEL.DE_COMP_RESP;
            b.NomeSacado = tb47.TB108_RESPONSAVEL.NO_RESP;            
            b.UfSacado = cidade.CO_UF;

            ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(b);

//--------> Faz a exibição e gera os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Usuário de acordo com o Tipo
        /// </summary>
        /// <param name="strTipoUsuario">Tipo de usuário</param>
        protected void CarregaUsuario(string strTipoUsuario)
        {
            int coEmp = ddlUnidadeUsuario.SelectedValue != "" ? int.Parse(ddlUnidadeUsuario.SelectedValue) : 0;

            switch (strTipoUsuario)
            {
//------------> Usuários do Tipo Aluno
                case "A":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where tb205.TP_USU_BIB == "A" && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp
                                             select new { tb205.TB07_ALUNO.NO_ALU, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_ALU );

                    ddlUsuario.DataTextField = "NO_ALU";
                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataBind();

                    ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
                    break;

//------------> Usuários do Tipo Professor
                case "P":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where tb205.TP_USU_BIB == "P" && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp
                                             select new { tb205.TB03_COLABOR.NO_COL, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_COL );

                    ddlUsuario.DataTextField = "NO_COL";
                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataBind();

                    ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
                    break;

//------------> Usuários do Tipo Funcionário
                case "F":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where tb205.TP_USU_BIB == "F" && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp
                                             select new { tb205.TB03_COLABOR.NO_COL, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_COL );

                    ddlUsuario.DataTextField = "NO_COL";
                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataBind();

                    ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
                    break;

//------------> Usuários do Tipo Outros
                case "O":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where tb205.TP_USU_BIB == "O" && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             select new { tb205.NO_USU_BIB, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_USU_BIB );

                    ddlUsuario.DataTextField = "NO_USU_BIB";
                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataBind();

                    ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades do Usuário
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidadeUsuario.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                            where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                            select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidadeUsuario.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeUsuario.DataValueField = "CO_EMP";
            ddlUnidadeUsuario.DataBind();

            ddlUnidadeUsuario.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos Bancários associadas a Unidade Logada
        /// </summary>
        private void CarregaDadosBoleto()
        {
            ddlBoleto.DataSource = from tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros()
                                   join tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros() on tb225.TB224_CONTA_CORRENTE equals tb227.TB224_CONTA_CORRENTE
                                   where tb225.CO_EMP == LoginAuxili.CO_EMP && tb227.TP_TAXA_BOLETO == "B"
                                   select new
                                   {
                                        tb227.ID_BOLETO, DESCRICAO = "Banco: " + tb225.IDEBANCO + " - CC: " + tb225.CO_CONTA + " - Cedente: " + tb227.CO_CEDENTE
                                   };

            ddlBoleto.DataValueField = "ID_BOLETO";
            ddlBoleto.DataTextField = "DESCRICAO";
            ddlBoleto.DataBind();

            ddlBoleto.Items.Insert(0, new ListItem("Selecione", ""));
        }
        
        /// <summary>
        /// Método que carrega a grid de Itens Emprestados
        /// </summary>
        public void CarregaGridItensEmprestados()
        {
            int coUsuarioBibliot = ddlUsuario.SelectedValue != "" ? int.Parse(ddlUsuario.SelectedValue) : 0;

            grdItensEmprestados.DataKeyNames = new string[] { "DE_OBS_EMP", "NU_PAGINA_ACERVO_ITENS", "CO_EMPR_BIB_ITENS", "ORG_CODIGO_ORGAO", "CO_ACERVO_AQUISI", "CO_ACERVO_ITENS", "CO_EMP" };

            var lstItensEmpres = from tb36 in TB36_EMPR_BIBLIOT.RetornaTodosRegistros()
                                 join tb123 in TB123_EMPR_BIB_ITENS.RetornaTodosRegistros() on tb36.CO_NUM_EMP equals tb123.TB36_EMPR_BIBLIOT.CO_NUM_EMP
                                 where tb36.TB205_USUARIO_BIBLIOT.CO_USUARIO_BIBLIOT == coUsuarioBibliot && tb123.DT_REAL_DEVO_ACER == null
                                 select new
                                 {
                                     tb123.TB204_ACERVO_ITENS.CO_ISBN_ACER, tb123.TB204_ACERVO_ITENS.CO_CTRL_INTERNO, tb123.DE_OBS_EMP, tb123.DT_PREV_DEVO_ACER,
                                     tb123.TB204_ACERVO_ITENS.TB35_ACERVO.NO_ACERVO, tb123.TB204_ACERVO_ITENS.NU_PAGINA_ACERVO_ITENS, tb36.CO_NUM_EMP,
                                     tb123.CO_EMPR_BIB_ITENS, ORG_CODIGO_ORGAO = tb123.TB204_ACERVO_ITENS.ORG_CODIGO_ORGAO, tb36.VL_MULT_DIA_ATRASO,
                                     CO_ACERVO_AQUISI = tb123.TB204_ACERVO_ITENS.CO_ACERVO_AQUISI, CO_ACERVO_ITENS = tb123.TB204_ACERVO_ITENS.CO_ACERVO_ITENS, 
                                     CO_EMP = tb123.TB204_ACERVO_ITENS.TB25_EMPRESA.CO_EMP
                                 };

            if (lstItensEmpres.Count() > 0)
            {
                var resultado = from result in lstItensEmpres
                                select new
                                {
                                    result.CO_ISBN_ACER, result.CO_CTRL_INTERNO, result.NO_ACERVO, result.CO_NUM_EMP, result.DT_PREV_DEVO_ACER, result.DE_OBS_EMP,
                                    result.NU_PAGINA_ACERVO_ITENS, DIAS = 0, VL_MULT_ATRASO = result.VL_MULT_DIA_ATRASO, result.CO_EMPR_BIB_ITENS,
                                    result.ORG_CODIGO_ORGAO, result.CO_ACERVO_AQUISI, result.CO_ACERVO_ITENS, result.CO_EMP
                                };

                if (resultado.Count() > 0)
                {
                    grdItensEmprestados.PageSize = 6;
                    grdItensEmprestados.DataSource = resultado;
                    grdItensEmprestados.DataBind();
                }
            }
            else
            {
                grdItensEmprestados.Dispose();
                grdItensEmprestados.DataBind();
            }
        } 
        #endregion                                                      

        protected void grdItensEmprestados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
//------------> Faz o cálculo da quantidade de dias de atraso na entrega
                int intDiasAtraso;
                decimal dcmValorMultaAtraso;

                int.TryParse(DateTime.Now.Subtract(DateTime.Parse(e.Row.Cells[5].Text)).Days.ToString(), out intDiasAtraso);
                decimal.TryParse(e.Row.Cells[7].Text, out dcmValorMultaAtraso);

                ((Label)e.Row.Cells[6].FindControl("lblDiasGrid")).Text = intDiasAtraso.ToString();

                if (e.Row.Cells[1].Text != "")
                    e.Row.Cells[1].Text = Decimal.Parse(e.Row.Cells[1].Text).ToString("000-00-0000-000-0");

                if (intDiasAtraso > 0)
                {
                    decimal valorTotalMulta;
                    valorTotalMulta = intDiasAtraso * dcmValorMultaAtraso;
                    e.Row.Cells[7].Text = valorTotalMulta.ToString();
                }
                else
                {
                    e.Row.Cells[7].Text = "0";
                    e.Row.Cells[6].Text = "0";
                }
            }
        }

        protected void grdItensBaixa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindGridItemBaixa(e.RowIndex);
        }

        protected void ckImprimeBoleto_CheckedChanged(object sender, EventArgs e)
        {
            rfvBoleto.Enabled = ckImprimeBoleto.Checked;
        }

        protected void ckIsento_CheckedChanged(object sender, EventArgs e)
        {
            CalculaValorIsencao();
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            PreencheGridItensBaixa();
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
//--------> Verifica se o tipo de usuários é todos, se sim, desabilita o DropDown de Unidades Escolares
            ckImprimeBoleto.Visible = false; lblckImprimeBoleto.Visible = false; ddlBoleto.Visible = false;

            if (ddlTipoUsuario.SelectedValue == "A")
                ckImprimeBoleto.Visible = lblckImprimeBoleto.Visible = ddlBoleto.Visible = true;

            if (ddlTipoUsuario.SelectedValue == "O")
                ddlUnidadeUsuario.Visible = lblUnidadeUsuario.Visible = false;
            else
                ddlUnidadeUsuario.Visible = lblUnidadeUsuario.Visible = true;

            CarregaUsuario(ddlTipoUsuario.SelectedValue);
        }

        protected void ddlUnidadeUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario(ddlTipoUsuario.SelectedValue);
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridItensEmprestados();

//--------> Limpa o Grid de Baixa
            lstItensBaixa = new List<ItensBaixa>();
            grdItensBaixa.DataSource = null;
            grdItensBaixa.DataBind();
        }

        #region Tipo Itens de Baixa

//----> Representa os Dados de Itens de Baixa
        private class ItensBaixa
        {
            public int RowGridEmprestimo { get; set; }
            public int CO_EMPR_BIB_ITENS { get; set; }
            public int ORG_CODIGO_ORGAO { get; set; }
            public int CO_ACERVO_AQUISI { get; set; }
            public int CO_ACERVO_ITENS { get; set; }
            public int CO_EMP_ITENS { get; set; }
            public Decimal CO_ISBN_ACER { get; set; }
            public string idItenBaixa { get; set; }
            public string CodigoInterno { get; set; }
            public string TituloObra { get; set; }
            public string ObsEmpr { get; set; }
            public string ObsEntrega { get; set; }
            public int NumPagEmpr { get; set; }
            public int NumPagEntrega { get; set; }
            public string IsentaMulta { get; set; }
            public Decimal VlMulta { get; set; }
            public int QtdDiasAtraso { get; set; }
        }
        #endregion
    }
}