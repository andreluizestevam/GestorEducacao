//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: NEGOCIAÇÃO DE DÉBITOS (CONTAS A RECEBER)
// OBJETIVO: NEGOCIAÇÃO DE TÍTULOS DE RECEITAS/MENSALIDADES
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5600_CtrlNegociacoesDebitos.F5601_NegociacaoTituloReceita
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
                AuxiliPagina.RedirecionaParaPaginaErro("Selecione um título para negociação.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

            if (Page.IsPostBack)
                return;

            for (int i = 1; i <= 31; i++)
            {
                ddlMelhorDia.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            ddlMelhorDia.SelectedValue = "1";

            CarregaUnidade();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (grdNegociacao.Rows.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Primeiro deve ser efetuada a negociação das parcelas.");
                return;
            }

            if (Session[SessoesHttp.CodigoNegociacao] != null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Negociação já efetuada.");
                return;
            }

            int idCliente = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("idCliente");
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            int coEmpUnidCont = int.Parse(ddlUnidadeContrato.SelectedValue);
            int coResp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("coResp");
            string tpCliente = QueryStringAuxili.RetornaQueryStringPelaChave("tpCliente");
            int modalidade = 0;
            int serie = 0;
            int turma = 0;
            int semestre = 0;
            int anoMesMat = 0;
            decimal valorEntr;

            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmpUnidCont);

            if (tb25.CO_CTAMAT_EMP == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe conta contábil de negociação associada a unidade.");
                return;
            }

//--------> Faz o cadastro de negociação na TB144_NEGOCIACAO
            TB144_NEGOCIACAO tb144 = new TB144_NEGOCIACAO();
            tb144.TB25_EMPRESA = coEmp != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp) : TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb144.DE_OBSERVACAO = txtObservacoes.Text != "" ? txtObservacoes.Text : null;
            tb144.DT_ALT_REGISTRO = DateTime.Now;
            tb144.DT_NEGOCIACAO = DateTime.Now;
            tb144.TP_CLIENTE_DOC = tpCliente;

            if (tpCliente == "O")
                tb144.TB103_CLIENTE = TB103_CLIENTE.RetornaPelaChavePrimaria(idCliente);
            else
                tb144.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(idCliente);

            tb144.VL_ENTRADA = decimal.TryParse(txtValorEntrada.Text, out valorEntr) ? valorEntr : 0;
            tb144.VL_TOTAL_NEGOCIACAO = txtValorLiquido.Text != "" ? decimal.Parse(txtValorLiquido.Text) : 0;

            TB144_NEGOCIACAO.SaveOrUpdate(tb144, true);

//--------> Retorna os códigos das negociações 
            var lastCoNegociacao = (from lTb144 in TB144_NEGOCIACAO.RetornaTodosRegistros()
                                    where lTb144.CO_UNID == tb144.TB25_EMPRESA.CO_EMP
                                    select new { lTb144.CO_NEGOCIACAO }).ToList();                        

            if (tpCliente != "O")
            {
                var tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                            where lTb08.CO_ALU == idCliente
                            select new
                            {
                                lTb08.CO_CUR, lTb08.CO_TUR, lTb08.TB44_MODULO.CO_MODU_CUR, lTb08.NU_SEM_LET, lTb08.CO_ANO_MES_MAT
                            }).ToList().Last();

                modalidade = tb08.CO_MODU_CUR;
                serie = tb08.CO_CUR;
                turma = tb08.CO_TUR.Value;
                semestre = int.Parse(tb08.NU_SEM_LET);
                anoMesMat = int.Parse(tb08.CO_ANO_MES_MAT.Trim());
            }

            TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);            

//--------> Faz o cadastro no contas a receber do valor de entrada pago
            if (decimal.TryParse(txtValorEntrada.Text, out valorEntr))
            {
                if (valorEntr > 0)
                {
                    TB47_CTA_RECEB tb47_Entrada = new TB47_CTA_RECEB();
                    tb47_Entrada.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb47_Entrada.TB000_INSTITUICAO = tb000;
                    tb47_Entrada.NU_DOC = "NE" + DateTime.Now.ToString("yy") + "." + lastCoNegociacao.ToList().Last().CO_NEGOCIACAO.ToString().PadLeft(10, '0') + "." + "01E";
                    tb47_Entrada.NU_PAR = 1;
                    tb47_Entrada.QT_PAR = 1;
                    tb47_Entrada.DT_CAD_DOC = DateTime.Now;
                    tb47_Entrada.DE_COM_HIST = "VALOR DE ENTRADA - NEGOCIAÇÃO DE TITULOS - Nº " + lastCoNegociacao.ToList().Last().CO_NEGOCIACAO.ToString("000000");
                    tb47_Entrada.VR_TOT_DOC = valorEntr;
                    tb47_Entrada.VR_PAR_DOC = valorEntr;
                    tb47_Entrada.VR_PAG = valorEntr;
                    tb47_Entrada.DT_VEN_DOC = DateTime.Now;
                    tb47_Entrada.DT_REC_DOC = DateTime.Now;
                    tb47_Entrada.DT_EMISS_DOCTO = DateTime.Now;
                    tb47_Entrada.DT_SITU_DOC = DateTime.Now;
                    tb47_Entrada.TB101_LOCALCOBRANCA = TB101_LOCALCOBRANCA.RetornaPelaChavePrimaria(0);
                    tb47_Entrada.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTAMAT_EMP.Value);
                    tb47_Entrada.CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                    tb47_Entrada.CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                    if (tb25.CO_HIST_MAT != null)
                        tb47_Entrada.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(tb25.CO_HIST_MAT.Value);
                    tb47_Entrada.IC_SIT_DOC = "Q";
                    tb47_Entrada.CO_FLAG_TP_VALOR_MUL = "V";
                    tb47_Entrada.CO_FLAG_TP_VALOR_JUR = "P";
                    tb47_Entrada.CO_FLAG_TP_VALOR_DES = "V";
                    tb47_Entrada.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                    tb47_Entrada.CO_FLAG_TP_VALOR_OUT = "V";
                    tb47_Entrada.FL_TIPO_COB = "I";
                    tb47_Entrada.FL_EMITE_BOLETO = "S";
                    if (tb25.CO_CENT_CUSMAT != null)
                        tb47_Entrada.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSMAT.Value);
                    tb47_Entrada.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);
                    tb47_Entrada.TP_CLIENTE_DOC = tpCliente;
                    tb47_Entrada.CO_EMP_UNID_CONT = coEmpUnidCont;

                    if (tpCliente != "O")
                    {
                        tb47_Entrada.CO_ALU = idCliente;
                        tb47_Entrada.CO_ANO_MES_MAT = anoMesMat.ToString();
                        tb47_Entrada.NU_SEM_LET = semestre.ToString();
                        tb47_Entrada.CO_CUR = serie;
                        tb47_Entrada.CO_TUR = turma;
                        tb47_Entrada.CO_MODU_CUR = modalidade;
                        tb47_Entrada.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);
                    }
                    else
                        tb47_Entrada.TB103_CLIENTE = TB103_CLIENTE.RetornaPelaChavePrimaria(idCliente);
                    tb47_Entrada.DT_ALT_REGISTRO = DateTime.Now;

                    TB47_CTA_RECEB.SaveOrUpdate(tb47_Entrada, true);
                }
            }            
            /////////////////////////

            List<TB47_CTA_RECEB> lstTb47 = new List<TB47_CTA_RECEB>();

            TB47_CTA_RECEB tb47;

//--------> Faz o lançameto da(s) parcela(s) no contas a receber
            for (int i = 0; i < grdNegociacao.Rows.Count; i++)
            {
                tb47 = new TB47_CTA_RECEB();

                tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                tb47.CO_EMP_UNID_CONT = coEmpUnidCont;
                tb47.NU_DOC = grdNegociacao.Rows[i].Cells[0].Text;
                tb47.NU_PAR = int.Parse(grdNegociacao.Rows[i].Cells[1].Text);
                tb47.QT_PAR = grdNegociacao.Rows.Count;
                tb47.DT_CAD_DOC = DateTime.Now;
                tb47.DE_COM_HIST = "VALOR PARCELA NEGOCIAÇÃO.";
                tb47.VR_TOT_DOC = decimal.Parse(txtValorLiquido.Text);
                tb47.VR_PAR_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[3].Text);
                tb47.DT_VEN_DOC = DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text);
                tb47.DT_EMISS_DOCTO = DateTime.Now;
                tb47.DT_SITU_DOC = DateTime.Now;

                if (tb25.CO_HIST_MAT != null)
                    tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(tb25.CO_HIST_MAT.Value);
                if (tb25.CO_CENT_CUSMAT != null)
                    tb47.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSMAT.Value);
                tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTAMAT_EMP.Value);
                tb47.CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                tb47.CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                tb47.TB000_INSTITUICAO = tb000;
                tb47.TB101_LOCALCOBRANCA = TB101_LOCALCOBRANCA.RetornaPelaChavePrimaria(0);
                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);
                tb47.CO_FLAG_TP_VALOR_MUL = "V";
                tb47.CO_FLAG_TP_VALOR_JUR = "P";
                tb47.CO_FLAG_TP_VALOR_DES = "V";
                tb47.CO_FLAG_TP_VALOR_OUT = "V";
                tb47.FL_TIPO_COB = "I";
                tb47.FL_EMITE_BOLETO = "S";
                tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                tb47.IC_SIT_DOC = "A";
                tb47.TP_CLIENTE_DOC = "A";
                tb47.DE_OBS_BOL_MAT = txtObservacoes.Text != "" ? txtObservacoes.Text : null;
                tb47.DE_OBS = "NEGOCIAÇÃO DE TITULOS Nº " + lastCoNegociacao.ToList().Last().CO_NEGOCIACAO.ToString("000000") + " - EM " + DateTime.Now.ToShortDateString();
                tb47.TP_CLIENTE_DOC = tpCliente;

                if (tb47.TP_CLIENTE_DOC != "O")
                {
                    tb47.CO_ALU = idCliente;
                    tb47.CO_ANO_MES_MAT = anoMesMat.ToString();
                    tb47.NU_SEM_LET = semestre.ToString();
                    tb47.CO_CUR = serie;
                    tb47.CO_TUR = turma;
                    tb47.CO_MODU_CUR = modalidade;
                    tb47.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);
                }
                else
                    tb47.TB103_CLIENTE = TB103_CLIENTE.RetornaPelaChavePrimaria(idCliente);

                var boletoRefer = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                                   join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on tb227.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                                   where tb227.TP_TAXA_BOLETO == "N" && tb225.CO_EMP == coEmpUnidCont
                                   select new { tb227.ID_BOLETO }).FirstOrDefault();
                if (boletoRefer != null)
                {
                    tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(boletoRefer.ID_BOLETO);
                }
                
                tb47.DT_ALT_REGISTRO = DateTime.Now;

                lstTb47.Add(tb47);
            }

            TB145_NEGDOCNOVOS tb145;
            foreach (TB47_CTA_RECEB iTb47 in lstTb47)
            {
                TB47_CTA_RECEB.SaveOrUpdate(iTb47, true);

                tb145 = new TB145_NEGDOCNOVOS();

//------------> Adiciona contratos antigos negociados na tabela de negociações antigas
                tb145.TB144_NEGOCIACAO = TB144_NEGOCIACAO.RetornaPelaChavePrimaria(lastCoNegociacao.ToList().Last().CO_NEGOCIACAO);
                tb145.CO_UNID = iTb47.CO_EMP;
                tb145.NU_DOC = iTb47.NU_DOC;
                tb145.NU_PAR = iTb47.NU_PAR;
                tb145.DT_CAD_DOC = iTb47.DT_CAD_DOC;

                TB145_NEGDOCNOVOS.SaveOrUpdate(tb145, true);
            }             

            TB47_CTA_RECEB tb47_parc;
            TB146_NEGDOCANTIGOS tb146;

//--------> Lança a(s) parcela(s) no contas a receber                        
            foreach (GridViewRow row in grdContratos.Rows)
            {
                if (((CheckBox)row.Cells[0].FindControl("ckSelect")).Checked)
                {
                    int intCoEmp = Convert.ToInt32(grdContratos.DataKeys[row.RowIndex].Values[0]);
                    string strNuDoc = grdContratos.DataKeys[row.RowIndex].Values[1].ToString();
                    int intNuPar = Convert.ToInt32(grdContratos.DataKeys[row.RowIndex].Values[2]);
                    DateTime dataContrato = Convert.ToDateTime(grdContratos.DataKeys[row.RowIndex].Values[3]);

                    tb47_parc = RetornaEntidade(intCoEmp, strNuDoc, intNuPar, dataContrato);                    
//----------------> Muda a situação do contrato para C (cancelado) e pega o código da negociação criado e adiciona a string de observação do contrato
                    tb47_parc.IC_SIT_DOC = "C";
                    tb47_parc.DE_OBS = "NEGOCIAÇÃO DE TÍTULOS - EM " + DateTime.Now.ToShortDateString() + " - Nº " + lastCoNegociacao.ToList().Last().CO_NEGOCIACAO.ToString("000000");
                    TB47_CTA_RECEB.SaveOrUpdate(tb47_parc, true);

                    tb146 = new TB146_NEGDOCANTIGOS();

//----------------> Adiciona contratos antigos negociados na tabela de negociações antigas
                    tb146.TB144_NEGOCIACAO = TB144_NEGOCIACAO.RetornaPelaChavePrimaria(lastCoNegociacao.ToList().Last().CO_NEGOCIACAO);
                    tb146.CO_UNID = intCoEmp;
                    tb146.NU_DOC = strNuDoc;
                    tb146.NU_PAR = intNuPar;
                    tb146.DT_CAD_DOC = dataContrato;

                    TB146_NEGDOCANTIGOS.SaveOrUpdate(tb146, true);
                }
            }

            btnGeraNegociacao.Enabled = btnJurNegoc.Enabled = false;

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Negociação efetuada com sucesso. Novos títulos incluídos no financeiro.");

            btnGeraBoleto.Enabled = true;

            this.Session[SessoesHttp.CodigoNegociacao] = lastCoNegociacao.ToList().Last().CO_NEGOCIACAO;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            HabilitaCampos(false);
            int idCliente = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("idCliente");
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            int coResp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("coResp");
            this.Session[SessoesHttp.CodigoNegociacao] = null;

            grdContratos.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

            if (QueryStringAuxili.RetornaQueryStringPelaChave("tpCliente").ToString() != "O")
            {                
                var listaContratos = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                     join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                     where (coEmp != 0 ? tb47.CO_EMP == coEmp : coEmp == 0) && tb47.IC_SIT_DOC == "A" && tb47.TP_CLIENTE_DOC == "A"
                                     && tb47.CO_ALU == idCliente && tb47.TB108_RESPONSAVEL.CO_RESP == coResp
                                     select new
                                     {
                                         tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, NO_IDENTIF = tb07.NO_ALU, ID_CLIENTE_DOC = tb07.CO_ALU,
                                         tb47.VR_PAR_DOC, tb47.VR_JUR_DOC, tb47.VR_MUL_DOC, tb47.DT_VEN_DOC, tb47.VR_DES_DOC,
                                         VR_DEBITO = tb47.DT_VEN_DOC < DateTime.Now ? (tb47.VR_PAR_DOC +
                                         (tb47.CO_FLAG_TP_VALOR_JUR == "V" ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) : (tb47.VR_JUR_DOC != null ? (tb47.VR_JUR_DOC * tb47.VR_PAR_DOC) / 100 : 0)) -
                                         (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)) +
                                         (tb47.CO_FLAG_TP_VALOR_MUL == "V" ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) : (tb47.VR_MUL_DOC != null ? (tb47.VR_MUL_DOC * tb47.VR_PAR_DOC) / 100 : 0))) :
                                         tb47.VR_PAR_DOC - (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)),
                                         CEDENTE = tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, "."), RESPONSAVEL = tb108.NO_RESP
                                     };

                txtNomeCedente.Text = listaContratos.First().NO_IDENTIF;
                spaCedente.InnerText = "CPF Resp";
                txtCedente.Text = listaContratos.First().CEDENTE.ToString();
                spaNomeResp.Visible = true;
                txtNomeResp.Visible = true;
                txtNomeResp.Text = listaContratos.First().RESPONSAVEL.ToString();

                BoundField bf1 = new BoundField();
                bf1.DataField = "NU_DOC";
                bf1.HeaderText = "Nº Doc";                
                grdContratos.Columns.Add(bf1);

                BoundField bf3 = new BoundField();
                bf3.DataField = "NU_PAR";
                bf3.HeaderText = "Nº Parc";                
                bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf3);

                BoundField bf6 = new BoundField();
                bf6.DataField = "VR_PAR_DOC";
                bf6.HeaderText = "R$ Parc";
                bf6.DataFormatString = "{0:N}";
                bf6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf6);

                BoundField bf7 = new BoundField();
                bf7.DataField = "VR_MUL_DOC";
                bf7.HeaderText = "R$ Multa";
                bf7.DataFormatString = "{0:N}";
                bf7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf7);

                BoundField bf8 = new BoundField();
                bf8.DataField = "VR_JUR_DOC";
                bf8.HeaderText = "R$ Juros";
                bf8.DataFormatString = "{0:N}";
                bf8.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf8);

                BoundField bf4 = new BoundField();
                bf4.DataField = "VR_DES_DOC";
                bf4.HeaderText = "R$ Descto";
                bf4.DataFormatString = "{0:N}";
                bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf4);

                BoundField bf5 = new BoundField();
                bf5.DataField = "VR_DEBITO";
                bf5.HeaderText = "R$ Débito";
                bf5.DataFormatString = "{0:N}";
                bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf5);

                BoundField bf9 = new BoundField();
                bf9.DataField = "DT_VEN_DOC";
                bf9.HeaderText = "Dt Venc";                
                bf9.DataFormatString = "{0:d}";
                bf9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf9);

                grdContratos.DataSource = listaContratos;
            }
            else
            {
                var listaContratos = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                     join tb103 in TB103_CLIENTE.RetornaTodosRegistros() on tb47.TB103_CLIENTE.CO_CLIENTE equals tb103.CO_CLIENTE
                                     where (coEmp != 0 ? tb47.CO_EMP == coEmp : coEmp == 0) && tb47.IC_SIT_DOC == "A"
                                     && tb47.TP_CLIENTE_DOC == "O" && tb47.TB103_CLIENTE.CO_CLIENTE == idCliente
                                     select new
                                     {
                                         tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, NO_IDENTIF = tb103.NO_FAN_CLI, ID_CLIENTE_DOC = tb103.CO_CLIENTE,
                                         tb47.VR_PAR_DOC, tb47.VR_JUR_DOC, tb47.VR_MUL_DOC, tb47.DT_VEN_DOC, tb47.VR_DES_DOC, RESPONSAVEL = "",
                                         VR_DEBITO = tb47.DT_VEN_DOC < DateTime.Now ? (tb47.VR_PAR_DOC +
                                         (tb47.CO_FLAG_TP_VALOR_JUR == "V" ? (tb47.VR_JUR_DOC != null ? tb47.VR_JUR_DOC : 0) : (tb47.VR_JUR_DOC != null ? (tb47.VR_JUR_DOC * tb47.VR_PAR_DOC) / 100 : 0)) -
                                         (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)) +
                                         (tb47.CO_FLAG_TP_VALOR_MUL == "V" ? (tb47.VR_MUL_DOC != null ? tb47.VR_MUL_DOC : 0) : (tb47.VR_MUL_DOC != null ? (tb47.VR_MUL_DOC * tb47.VR_PAR_DOC) / 100 : 0))) :
                                         tb47.VR_PAR_DOC - (tb47.CO_FLAG_TP_VALOR_DES == "V" ? (tb47.VR_DES_DOC != null ? tb47.VR_DES_DOC : 0) : (tb47.VR_DES_DOC != null ? (tb47.VR_DES_DOC * tb47.VR_PAR_DOC) / 100 : 0)),
                                         CEDENTE = tb103.CO_CPFCGC_CLI.Length == 14 ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI.Length == 11 ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb103.CO_CPFCGC_CLI
                                     };

                txtNomeCedente.Text = listaContratos.First().NO_IDENTIF;
                spaCedente.InnerText = "CNPJ/CPF";
                txtCedente.Text = listaContratos.First().CEDENTE.ToString();
                spaNomeResp.Visible = false;
                txtNomeResp.Visible = false;

                BoundField bf1 = new BoundField();
                bf1.DataField = "NU_DOC";
                bf1.HeaderText = "Nº Doc";
                grdContratos.Columns.Add(bf1);

                BoundField bf3 = new BoundField();
                bf3.DataField = "NU_PAR";
                bf3.HeaderText = "Nº Parc";
                bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf3);

                BoundField bf6 = new BoundField();
                bf6.DataField = "VR_PAR_DOC";
                bf6.HeaderText = "R$ Parc";
                bf6.DataFormatString = "{0:N}";
                bf6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf6);

                BoundField bf7 = new BoundField();
                bf7.DataField = "VR_MUL_DOC";
                bf7.HeaderText = "R$ Multa";
                bf7.DataFormatString = "{0:N}";
                bf7.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf7);

                BoundField bf8 = new BoundField();
                bf8.DataField = "VR_JUR_DOC";
                bf8.HeaderText = "R$ Juros";
                bf8.DataFormatString = "{0:N}";
                bf8.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf8);

                BoundField bf4 = new BoundField();
                bf4.DataField = "VR_DES_DOC";
                bf4.HeaderText = "R$ Descto";
                bf4.DataFormatString = "{0:N}";
                bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf4);

                BoundField bf5 = new BoundField();
                bf5.DataField = "VR_DEBITO";
                bf5.HeaderText = "R$ Débito";
                bf5.DataFormatString = "{0:N}";
                bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf5);

                BoundField bf9 = new BoundField();
                bf9.DataField = "DT_VEN_DOC";
                bf9.HeaderText = "Dt Venc";
                bf9.DataFormatString = "{0:d}";
                bf9.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdContratos.Columns.Add(bf9);

                grdContratos.DataSource = listaContratos;
            }

            grdContratos.DataBind();            

            if (grdContratos.Rows.Count > 5)
            {
                divGrdContratos.Style.Value = "overflow: auto;overflow-x: hidden;height:285px;width:550px;margin-left: 25px;overflow-y: auto;border: 1px solid #CCCCCC;";
                ulFooterGrdContratos.Style.Value = "width:550px;margin-top:0px !important;padding-left: 25px;";
            }
            else
            {
                ulFooterGrdContratos.Style.Value = "width:550px;margin-top:0px !important;padding-left: 50px;";
                divGrdContratos.Style.Value = "overflow-x: hidden;width:550px;margin-left: 50px;height:285px;overflow-y: auto;border: 1px solid #CCCCCC;";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="coEmp">Id da unidade</param>
        /// <param name="strNuDoc">Número do documento</param>
        /// <param name="intNuPar">Número da parcela</param>
        /// <param name="dataCadDoc">Data de cadastro</param>
        /// <returns>Entidade TB47_CTA_RECEB</returns>
        private TB47_CTA_RECEB RetornaEntidade(int coEmp, string strNuDoc, int intNuPar, DateTime dataCadDoc)
        {
            return TB47_CTA_RECEB.RetornaPelaChavePrimaria(coEmp, strNuDoc, intNuPar, dataCadDoc);
        }

        /// <summary>
        /// Método que carrega informações dos valores do Título
        /// </summary>
        private void CarregaValoresTitulo()
        {
            HabilitaCampos(true);
            txtValorDebito.Text = String.Format("{0:n2}", txtTotSelecionado.Text);
            txtValorSubTotal.Text = String.Format("{0:n2}", txtTotSelecionado.Text);
            txtValorBase.Text = String.Format("{0:n2}", txtTotSelecionado.Text);
            txtValorLiquido.Text = String.Format("{0:n2}", txtTotSelecionado.Text);
            txtQtdeParcelas.Enabled = txtVencPrimParcela.Enabled = ddlMelhorDia.Enabled = grdContratos.Enabled = false;
            libtnNegociacao.Style.Add("background-color", "#FFDAB9");
            btnGeraNegociacao.Text = "Refazer";
        }

        /// <summary>
        /// Método que carrega informações dos valores pagos do Título
        /// </summary>
        /// <param name="tb47">Entidade TB47_CTA_RECEB</param>
        private void CalculaValoresPagos(TB47_CTA_RECEB tb47)
        {
            txtValorDebito.Text = String.Format("{0:n2}", tb47.VR_PAR_DOC);

            decimal dcmValorEntrada = txtValorEntrada.Text != "" ? decimal.Parse(txtValorEntrada.Text) : 0;
            decimal dcmValorJuros = txtValorJuros.Text != "" ? decimal.Parse(string.Format("{0:0.0000}", decimal.Parse(txtValorJuros.Text))) : 0;
            decimal dcmValorDesconto = txtValorDesconto.Text != "" ? decimal.Parse(txtValorDesconto.Text) : 0;
            decimal dcmValorLiquido = txtValorDebito.Text != "" ? decimal.Parse(txtValorDebito.Text) : 0;

//--------> Calcula o valor Líquido da Negociação
            if (dcmValorDesconto > 0)
                dcmValorLiquido = dcmValorLiquido - dcmValorDesconto;

            if (dcmValorEntrada > 0)
                dcmValorLiquido = dcmValorLiquido - dcmValorEntrada;

            if (dcmValorJuros > 0)
                dcmValorLiquido = dcmValorLiquido - ((dcmValorJuros / 100) * dcmValorLiquido);

            txtValorLiquido.Text = String.Format("{0:n2}", dcmValorLiquido);
        }

        /// <summary>
        /// Método que monta a Grid de Negociação
        /// </summary>
        protected void MontaGridNegociacao()
        {
            int intMesSelecionado;
            string strParcelas = txtQtdeParcelas.Text;
            int coNegociacao = 0;

            var lastCoNegociacao = (from lTb144 in TB144_NEGOCIACAO.RetornaTodosRegistros()
                                    where lTb144.CO_UNID == LoginAuxili.CO_EMP
                                    select new { lTb144.CO_NEGOCIACAO }).ToList();

            if (lastCoNegociacao.Count() > 0)
            {
                coNegociacao = lastCoNegociacao.ToList().Last().CO_NEGOCIACAO + 1;
            }
            else
                coNegociacao = 1;

            strParcelas = strParcelas.Replace("_", "");

            if (strParcelas != "")
            {
                grdNegociacao.DataKeyNames = new string[] { "numContrato" };

                DataTable Dt = new DataTable();

                Dt.Columns.Add("numContrato");

                Dt.Columns.Add("numParcela");

                Dt.Columns.Add("dtVencimento");

                Dt.Columns.Add("valorParcela");                

                //Dt.Rows.Add(DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + '.' + "01", "1", txtVencPrimParcela.Text, txtValorParcela.Text);
                Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + "." + "01", "1", txtVencPrimParcela.Text, txtValorParcela.Text);

                if (int.Parse(strParcelas) > 1)
                {
                    for (int i = 2; i <= int.Parse(strParcelas); i++)
                    {
                        if ((DateTime.Parse(txtVencPrimParcela.Text).Month + i - 1) <= 12)
                        {
                            intMesSelecionado = DateTime.Parse(txtVencPrimParcela.Text).Month + i - 1;
                            if (intMesSelecionado == 2)
                            {
                                if (int.Parse(ddlMelhorDia.SelectedValue) <= 29)
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), ddlMelhorDia.SelectedValue + "/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year).ToString(), txtValorParcela.Text);
                                else
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), "28/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year).ToString(), txtValorParcela.Text);
                            }
                            else if ((intMesSelecionado == 4) || (intMesSelecionado == 6) || (intMesSelecionado == 9) || (intMesSelecionado == 11))
                            {
                                if (int.Parse(ddlMelhorDia.SelectedValue) <= 30)
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), ddlMelhorDia.SelectedValue + "/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year).ToString(), txtValorParcela.Text);
                                else
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), "30/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year).ToString(), txtValorParcela.Text);
                            }
                            else
                            {
                                Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), ddlMelhorDia.SelectedValue.ToString() + "/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year).ToString(), txtValorParcela.Text);
                            }
                        }
                        else
                        {
                            intMesSelecionado = (DateTime.Parse(txtVencPrimParcela.Text).Month + i - 1) - 12;
                            if (intMesSelecionado == 2)
                            {
                                if (int.Parse(ddlMelhorDia.SelectedValue) <= 29)
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + +'.' + i.ToString("D2"), i.ToString(), ddlMelhorDia.SelectedValue + "/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year + 1).ToString(), txtValorParcela.Text);
                                else
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), "28/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year + 1).ToString(), txtValorParcela.Text);
                            }
                            else if ((intMesSelecionado == 4) || (intMesSelecionado == 6) || (intMesSelecionado == 9) || (intMesSelecionado == 11))
                            {
                                if (int.Parse(ddlMelhorDia.SelectedValue) <= 30)
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), ddlMelhorDia.SelectedValue + "/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year + 1).ToString(), txtValorParcela.Text);
                                else
                                    Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), "30/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year + 1).ToString(), txtValorParcela.Text);
                            }
                            else
                            {
                                Dt.Rows.Add("NE" + DateTime.Parse(txtVencPrimParcela.Text).ToString("yy") + "." + coNegociacao.ToString().PadLeft(10, '0') + '.' + i.ToString("D2"), i.ToString(), ddlMelhorDia.SelectedValue.ToString() + "/" + intMesSelecionado.ToString("00") + "/" + (DateTime.Parse(txtVencPrimParcela.Text).Year + 1).ToString(), txtValorParcela.Text);
                            }
                        }
                    }
                }

                grdNegociacao.DataSource = Dt;

                grdNegociacao.DataBind();

                if (grdNegociacao.Rows.Count > 0)
                    txtTotalgrdNegoc.Text = txtValorLiquido.Text;
            }
        }

        /// <summary>
        /// Método que Habilita/Desabilita os campos informados
        /// </summary>
        /// <param name="flag">Boolean habilita</param>
        private void HabilitaCampos(bool flag)
        {
            txtValorDesconto.Enabled = txtValorEntrada.Enabled = txtValorJuros.Enabled = txtQtdeParcelas.Enabled = flag;
            ddlMelhorDia.Enabled = txtVencPrimParcela.Enabled = txtObservacoes.Enabled = btnJurNegoc.Enabled = flag;
        }

        /// <summary>
        /// Método que limpa os campos listados abaixo
        /// </summary>
        private void LimpaCampos()
        {
            txtValorDesconto.Text = txtValorEntrada.Text = txtValorJuros.Text = txtQtdeParcelas.Text = txtValorParcela.Text =            
            txtVencPrimParcela.Text = txtObservacoes.Text = txtValorDebito.Text = txtValorSubTotal.Text = txtValorLiquido.Text = txtValorBase.Text = "";
            ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();
            ddlMelhorDia.SelectedValue = "1";
            grdNegociacao.DataSource = null;
            grdNegociacao.DataBind();
        }

        /// <summary>
        /// Método que gera boleto dos itens selecionados
        /// </summary>
        /// <param name="coEmp">Id da unidade</param>
        /// <param name="strTpCliente">Tipo de cliente</param>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="coNegoc">Código da negociação</param>
        private void GeraBoleto(int coEmp, string strTpCliente, int id_cliente, int coNegoc)
        {
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            string strTipoCliente = strTpCliente;
            if (strTipoCliente == "R")
                strTipoCliente = "A";
            int id = id_cliente;
            int coNegociacao = coNegoc;

            var varResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                           where tb07.CO_ALU == id && tb07.CO_EMP == coEmp && strTipoCliente == "A"
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                           select new
                           {
                               BAIRRO = tb905.NO_BAIRRO, CEP = tb108.CO_CEP_RESP, CIDADE = tb904.NO_CIDADE,
                               CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP,
                               ENDERECO = tb108.DE_ENDE_RESP, NUMERO = tb108.NU_ENDE_RESP, COMPL = tb108.DE_COMP_RESP,
                               NOME = tb108.NO_RESP, UF = tb904.CO_UF
                           }).FirstOrDefault();

            var varTb103 = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                            where tb103.CO_CLIENTE == id && strTipoCliente == "O"
                            select new
                            {
                                BAIRRO = tb103.TB905_BAIRRO.NO_BAIRRO, CEP = tb103.CO_CEP_CLI, CIDADE = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                CPFCNPJ = (tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length >= 11) ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                          ((tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length >= 14) ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI),
                                ENDERECO = tb103.DE_END_CLI, NUMERO = tb103.NU_END_CLI, COMPL = tb103.DE_COM_CLI,
                                NOME = tb103.NO_FAN_CLI, UF = tb103.CO_UF_CLI
                           }).FirstOrDefault();

            var varSacado = strTipoCliente == "A" ? varResp : varTb103;

            int iGrdNeg = 1;
//--------> Varre toda a grid de Negociação
            foreach (GridViewRow linha in grdNegociacao.Rows)
            {
//------------> Gerarará boleto para as contas checadas
                if (((CheckBox)linha.FindControl("ckSelect")).Checked)
                {
                    //string strNudoc = linha.Cells[0].Text + "/" + coNegoc.ToString("D6") + "N";
                    string strNudoc = "NE" + DateTime.Now.ToString("yy") + "." + coNegoc.ToString().PadLeft(10, '0') + "." + int.Parse(linha.Cells[1].Text).ToString("D2");
                    int intNuPar = int.Parse(linha.Cells[1].Text);
                    string strInstruBoleto = "";

                    TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(coEmp, strNudoc, intNuPar);

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

                    if (strTipoCliente == "A")
                    {
                        if (tb47.TB108_RESPONSAVEL == null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                            return;
                        }
                    }
                    else
                    {
                        if (tb47.TB103_CLIENTE == null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Cliente!");
                            return;
                        }
                    }

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                        return;
                    }

//----------------> Recebe a unidade                    
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP_UNID_CONT);

                    InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

//----------------> Informações do Boleto
                    boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                    boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;
                    boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                    boleto.Valor = tb47.VR_PAR_DOC;
                    boleto.Vencimento = tb47.DT_VEN_DOC;

//----------------> Informações do Cedente
                    boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                    boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA;

                    boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                    boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + '-' + tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim();
                    boleto.CpfCnpjCedente = tb25.CO_CPFCGC_EMP;
                    boleto.NomeCedente = tb25.NO_RAZSOC_EMP;

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                    {
                        var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                        strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                    }

//----------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                        strInstruBoleto = tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                        strInstruBoleto = strInstruBoleto + tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                        strInstruBoleto = strInstruBoleto + tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                    boleto.Instrucoes = strInstruBoleto;

                    boleto.CO_EMP = tb47.CO_EMP;
                    boleto.NU_DOC = tb47.NU_DOC;
                    boleto.NU_PAR = tb47.NU_PAR;
                    boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

                    string strMultaMoraDesc = "";

//----------------> Informações da Multa
                    strMultaMoraDesc += tb47.VR_MUL_DOC != null && tb47.VR_MUL_DOC.Value != 0 ?
                        (tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
                        (boleto.Valor * (decimal)tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

//----------------> Informações da Mora
                    strMultaMoraDesc += tb47.VR_JUR_DOC != null && tb47.VR_JUR_DOC.Value != 0 ?
                         (tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
                         (boleto.Valor * (decimal)tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
                            tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

//----------------> Informações do desconto
                    strMultaMoraDesc += tb47.VR_DES_DOC != null && tb47.VR_DES_DOC.Value != 0 ?
                         (tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + tb47.VR_DES_DOC.Value.ToString("0.00") + "% (R$ " +
                         (boleto.Valor * (decimal)tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
                            tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

//----------------> Faz a adição de instruções ao Boleto
                    boleto.Instrucoes += "(*) " + strMultaMoraDesc + "<br>";

                    string strCnpjCPF = "";

//----------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                    if (strTipoCliente == "A")
                    {
//--------------------> Ano Refer: - Matrícula: - Nº NIRE:
//--------------------> Modalidade: - Série: - Turma: - Turno:
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

                        boleto.Instrucoes += strCnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";
                    }
                    else if (strTipoCliente == "O")
                    {
                        boleto.Instrucoes += "</br>" + "(" + tb47.TB103_CLIENTE.NO_FAN_CLI + ")";

                        boleto.Instrucoes += "</br>" + "(Contrato: " + (tb47.CO_CON_RECFIX != null ? tb47.CO_CON_RECFIX : "XXXXX") +
                            " - Aditivo: " + (tb47.CO_ADITI_RECFIX != null ? tb47.CO_ADITI_RECFIX.Value.ToString("00") : "XX") +
                            " - Parcela: " + tb47.NU_PAR.ToString("00") + ")";
                    }

//----------------> Informações do Sacado
                    boleto.BairroSacado = varSacado.BAIRRO;
                    boleto.CepSacado = varSacado.CEP;
                    boleto.CidadeSacado = varSacado.CIDADE;
                    boleto.CpfCnpjSacado = varSacado.CPFCNPJ;
                    boleto.EnderecoSacado = varSacado.ENDERECO + " " + varSacado.NUMERO + " " + varSacado.COMPL;
                    boleto.NomeSacado = varSacado.NOME;
                    boleto.UfSacado = varSacado.UF;

                    ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                    if ((iGrdNeg != grdNegociacao.Rows.Count) && (grdNegociacao.Rows.Count > 1))
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

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

//--------> Faz a exibição e gera os Boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

//====> Método que carrega o DropDown de Unidades de Contrato
        private void CarregaUnidade()
        {
            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.sigla };

            ddlUnidadeContrato.DataTextField = "sigla";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion        

        protected void grdNegociacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrdNegoc(e.NewPageIndex);
        }

        private void BindGrdNegoc(int indiceNovaPagina)
        {
            grdNegociacao.PageIndex = indiceNovaPagina;
            grdNegociacao.DataBind();
        }

        protected void grdNegociacao_DataBound(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = grdNegociacao.BottomPagerRow;

            if (gridViewRow != null)
            {
                DropDownList ddlListaPaginas = (DropDownList)gridViewRow.Cells[0].FindControl("ddlGrdPages");

                if (ddlListaPaginas != null)
                    for (int i = 0; i < grdNegociacao.PageCount; i++)
                    {
                        int numeroPagina = i + 1;
                        ListItem item = new ListItem(numeroPagina.ToString());

                        if (i == grdNegociacao.PageIndex)
                            item.Selected = true;

                        ddlListaPaginas.Items.Add(item);
                    }
            }
        }

        protected void ddlGrdPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = grdNegociacao.BottomPagerRow;
            if (gridViewRow != null)
            {
                DropDownList ddlListaPaginas = (DropDownList)gridViewRow.Cells[0].FindControl("ddlGrdPages");
                BindGrdNegoc(ddlListaPaginas.SelectedIndex);
            }
        }

        protected void grdContratos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double doubleValorTotDebito = 0;
      
            foreach (GridViewRow linha in grdContratos.Rows)
            {
                doubleValorTotDebito = doubleValorTotDebito + double.Parse(linha.Cells[7].Text);
            }

            txtTotDebitos.Text = String.Format("{0:n2}", doubleValorTotDebito);
        }

        public GridView NegocGrid { get { return this.grdNegociacao; } }

        protected void btnGeraNegociacao_Click(object sender, EventArgs e)
        {
            if (!txtValorDesconto.Enabled)
            {
                foreach (GridViewRow linha in grdContratos.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        CarregaValoresTitulo();
                        return;
                    }
                }

                AuxiliPagina.EnvioMensagemErro(this, "Deve ser selecionado pelo menos um contrato.");
            }
            else
            {
                grdContratos.Enabled = true;
                libtnNegociacao.Style.Add("background-color", "#F1FFEF");
                btnGeraNegociacao.Text = "Negociar";
                HabilitaCampos(false);
                LimpaCampos();
            }
        }

        protected void txtValorDesconto_TextChanged(object sender, EventArgs e)
        {
            decimal decOutput = 0;

            if (decimal.TryParse(this.txtValorDesconto.Text, out decOutput))
                txtValorSubTotal.Text = (decimal.Parse(txtValorDebito.Text) - decimal.Parse(txtValorDesconto.Text != "" ? txtValorDesconto.Text : "0")).ToString();
            else
                txtValorDesconto.Text = "";
            txtValorBase.Text = txtValorSubTotal.Text;
            txtValorLiquido.Text = txtValorBase.Text;
            txtValorEntrada_TextChanged(sender, e);
        }

        protected void txtValorEntrada_TextChanged(object sender, EventArgs e)
        {
            decimal decOutput = 0;

            if (decimal.TryParse(this.txtValorEntrada.Text, out decOutput))
                txtValorBase.Text = (decimal.Parse(txtValorSubTotal.Text) - decimal.Parse(txtValorEntrada.Text != "" ? txtValorEntrada.Text : "0")).ToString();
            else
                txtValorEntrada.Text = "";

            txtValorLiquido.Text = txtValorBase.Text;
        }

        protected void btnJurNegoc_Click(object sender, EventArgs e)
        {
            decimal decOutput = 0;

            if (decimal.TryParse(this.txtValorJuros.Text, out decOutput))
            {
                txtValorLiquido.Text = String.Format("{0:n2}", decimal.Parse(txtValorBase.Text) + (decOutput / 100) * decimal.Parse(txtValorBase.Text));
            }
            else
            {
                txtValorJuros.Text = "";
                txtValorLiquido.Text = String.Format("{0:n2}", decimal.Parse(txtValorBase.Text));
            }

            txtQtdeParcelas.Text = "1";
            txtValorParcela.Text = txtValorLiquido.Text;
            ddlMelhorDia.SelectedValue = DateTime.Now.Day.ToString();
            txtVencPrimParcela.Text = DateTime.Now.ToShortDateString();
            MontaGridNegociacao();
            txtQtdeParcelas.Enabled = ddlMelhorDia.Enabled = txtVencPrimParcela.Enabled = true;
        }

        protected void btnGeraBoleto_Click(object sender, EventArgs e)
        {
            bool flagSelGridBol = false;

            foreach (GridViewRow linha in grdNegociacao.Rows)
            {
//------------> Gerará boleto para as contas checadas
                if (((CheckBox)linha.FindControl("ckSelect")).Checked)
                    flagSelGridBol = true;
            }

            if (!flagSelGridBol)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione uma parcela na grid de negociacao.");
                return;
            }

            int idCliente = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("idCliente");
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
            string tpCliente = QueryStringAuxili.RetornaQueryStringPelaChave("tpCliente");

            coEmp = coEmp != 0 ? coEmp : LoginAuxili.CO_EMP;

            GeraBoleto(coEmp, tpCliente, idCliente, int.Parse(Session[SessoesHttp.CodigoNegociacao].ToString()));
        }

        protected void txtQtdeParcelas_TextChanged(object sender, EventArgs e)
        {
            if (txtQtdeParcelas.Text != "___")
            {
                string strParcelas = txtQtdeParcelas.Text;
                strParcelas = strParcelas.Replace("_", "");

                if (strParcelas != "")
                {
                    if (int.Parse(strParcelas) > 0)
                    {
                        txtValorParcela.Text = String.Format("{0:n2}", decimal.Parse(txtValorLiquido.Text) / int.Parse(strParcelas));
                        txtVencPrimParcela_TextChanged(sender, e);
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Quantidade de parcelas deve ser maior que zero");
                        return;
                    }

                }
            }
        }

        protected void txtVencPrimParcela_TextChanged(object sender, EventArgs e)
        {
            DateTime dataRetorno;

            if (DateTime.TryParse(txtVencPrimParcela.Text, out dataRetorno))
            {
                if (DateTime.Parse(txtVencPrimParcela.Text) >= DateTime.Now )
	            {
		            MontaGridNegociacao();
	            }
                else
                {
                    txtVencPrimParcela.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    MontaGridNegociacao();
                }                
            }
            else
            {
                txtVencPrimParcela.Text = DateTime.Now.ToString("dd/MM/yyyy");
                MontaGridNegociacao();
            }
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            double doubleValorTotSelecionado = 0;      
            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    doubleValorTotSelecionado = doubleValorTotSelecionado + double.Parse(linha.Cells[7].Text);
            }

            txtTotSelecionado.Text = String.Format("{0:n2}", doubleValorTotSelecionado);
        }

        protected void ddlMelhorDia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtdeParcelas_TextChanged(sender, e);
        }
    }
}