//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: ENTREGA DE SOLICITAÇÃO DE SERVIÇOS.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.EntregaSolicitacaoServicos
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
            if (IsPostBack)
                return;            

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione uma solicitação.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);            
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {            
//--------> Faz a verificação para saber se alguma solicitação está habilitada
            bool flagSelecionado = false;
            bool flagHabilitado = false;

            foreach (GridViewRow linha in grvDocumentos.Rows)
                if (((CheckBox)linha.Cells[0].FindControl("chkEntregue")).Enabled)
                    flagHabilitado = true;

            if (!flagHabilitado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma solicitação está disponível para entrega");
                return;
            }
            
//--------> Varre a grid para saber se existe alguma entrega selecionada
            foreach (GridViewRow linha in grvDocumentos.Rows)
                if (((CheckBox)linha.Cells[0].FindControl("chkEntregue")).Checked && ((CheckBox)linha.Cells[0].FindControl("chkEntregue")).Enabled)
                    flagSelecionado = true;

            if (!flagSelecionado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "No mínimo uma solicitação deve ser escolhida");
                return;
            }

            TB64_SOLIC_ATEND tb64 = RetornaEntidade();

            foreach (GridViewRow linha in grvDocumentos.Rows)
            {
//------------> Verifica se documento foi checado e está habilitado
                if (((CheckBox)linha.Cells[0].FindControl("chkEntregue")).Checked && ((CheckBox)linha.Cells[0].FindControl("chkEntregue")).Enabled) 
                {
//----------------> Carrega o item selecionado
                    var tb65 = TB65_HIST_SOLICIT.RetornaPelaChavePrimaria(tb64.CO_EMP, tb64.CO_ALU, tb64.CO_CUR, tb64.CO_SOLI_ATEN, int.Parse(linha.Cells[10].Text));
                    
//----------------> Faz a atualização do item selecionado
                    tb65.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                    tb65.CO_SITU_SOLI = SituacaoItemSolicitacao.E.ToString();
                    tb65.DT_ENTR_SOLI = DateTime.Now;
                    tb65.NO_RECE_SOLI = txtNomeRecebedor.Text;
                    tb65.TP_DOCTO_RECE = ddlTipoDocRecebimento.SelectedValue;
                    tb65.NU_DOCTO_RECE = txtNumeroDocRecebedor.Text;
                    
                    if (GestorEntities.SaveOrUpdate(tb65) <= 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");
                        return;
                    }
                }
            }

            bool todosCancelados = true;
            bool todosEntreguesOuCancelados = true;

//--------> Faz a verificação para saber se todos os itens estão cancelados e/ou entregues
            foreach (TB65_HIST_SOLICIT lstTb65 in tb64.TB65_HIST_SOLICIT)
            {
                if (lstTb65.CO_SITU_SOLI != SituacaoItemSolicitacao.C.ToString())
                {
                    todosCancelados = false;

                    if (lstTb65.CO_SITU_SOLI != SituacaoItemSolicitacao.E.ToString())
                        todosEntreguesOuCancelados = false;
                }
            }

            if (todosCancelados)
                tb64.CO_SIT = SituacaoSolicitacao.C.ToString();
            else if (todosEntreguesOuCancelados)
                tb64.CO_SIT = SituacaoSolicitacao.F.ToString();
            else
                tb64.CO_SIT = SituacaoSolicitacao.A.ToString();

            CurrentPadraoCadastros.CurrentEntity = tb64;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB64_SOLIC_ATEND tb64 = RetornaEntidade();

            if (tb64 != null)
            {
//------------> Se solicitação finalizada ou cancelada, não poderá fazer alteração
                if (tb64.CO_SIT == "C" || tb64.CO_SIT == "F")
                    AuxiliPagina.RedirecionaParaPaginaErro("Não permite alteração porque a solicitação está finalizada ou cancelada.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

                CarregaDocumentos(tb64);
                txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;
                txtDataEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataSolicitacao.Text = tb64.DT_SOLI_ATEN.ToString("dd/MM/yyyy");
                txtNumeroSolicitacao.Text = tb64.NU_DCTO_SOLIC;

                CarregaModalidadeSerieTurma(CarregaAluno(tb64));

                DateTime dataAtual = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");

                var tb47 = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                            where iTb47.CO_ALU == tb64.CO_ALU && iTb47.IC_SIT_DOC == "A"
                            && iTb47.DT_VEN_DOC < dataAtual
                            select iTb47).ToList();

                lblPendenFinan.Visible = tb47.Count() > 0;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB64_SOLIC_ATEND</returns>
        private TB64_SOLIC_ATEND RetornaEntidade()
        {
            TB64_SOLIC_ATEND tb64 = TB64_SOLIC_ATEND.RetornaPeloID(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb64 == null) ? new TB64_SOLIC_ATEND() : tb64;
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega informações do aluno
        /// </summary>
        /// <param name="tb64">Entidade TB64_SOLIC_ATEND</param>
        /// <returns>Entidade TB07_ALUNO</returns>
        private TB07_ALUNO CarregaAluno(TB64_SOLIC_ATEND tb64) 
        {
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tb64.CO_ALU);

            txtNire.Text = tb07.NU_NIRE.ToString().PadLeft(9, '0');
            txtAluno.Text = tb07.NO_ALU;

            tb07.TB108_RESPONSAVELReference.Load();
            txtResponsavelAluno.Text = tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NO_RESP : "";
            txtCpfResp.Text = tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NU_CPF_RESP : "";

            return tb07;
        }

        /// <summary>
        /// Método que carrega Modalidade, Série e turma da matrícula do aluno selecionado
        /// </summary>
        /// <param name="tb07">Entidade TB07_ALUNO</param>
        private void CarregaModalidadeSerieTurma(TB07_ALUNO tb07)
        {
            TB08_MATRCUR tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where lTb08.CO_ALU == tb07.CO_ALU
                                 select lTb08).OrderBy( m => m.CO_ANO_MES_MAT ).FirstOrDefault();

            tb08.TB44_MODULOReference.Load();

            txtSerie.Text = TB01_CURSO.RetornaPelaChavePrimaria(tb07.CO_EMP, tb08.TB44_MODULO.CO_MODU_CUR, tb08.CO_CUR).NO_CUR;
            txtModalidade.Text = tb08.TB44_MODULO.DE_MODU_CUR;

            TB06_TURMAS tb06 = TB06_TURMAS.RetornaPelaChavePrimaria(tb07.CO_EMP, tb08.TB44_MODULO.CO_MODU_CUR, tb08.CO_CUR, (int)tb08.CO_TUR);
            tb06.TB129_CADTURMASReference.Load();

            if (tb08.CO_TUR != null)
                txtTurma.Text = tb06.TB129_CADTURMAS.NO_TURMA;
        }

        /// <summary>
        /// Método que carrega a grid de Documentos
        /// </summary>
        /// <param name="tb64">Entidade TB64_SOLIC_ATEND</param>
        private void CarregaDocumentos(TB64_SOLIC_ATEND tb64) 
        {
            bool ocorBoleto = false;

            var documentos = (from tb65 in TB65_HIST_SOLICIT.RetornaTodosRegistros()
                             where tb65.CO_SOLI_ATEN == tb64.CO_SOLI_ATEN && tb65.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             select new Documentos
                             {
                                 DT_FIM_SOLI = tb65.DT_FIM_SOLI,
                                 NO_TIPO_SOLI = tb65.TB66_TIPO_SOLIC.NO_TIPO_SOLI,
                                 DE_LOCALI_SOLI = tb65.DE_LOCALI_SOLI,
                                 DT_PREV_ENTR = tb65.TB64_SOLIC_ATEND.DT_PREV_ENTR,
                                 CHECKED = tb65.CO_SITU_SOLI.ToUpper() == "E",
                                 CO_TIPO_SOLI = tb65.CO_TIPO_SOLI,
                                 CO_SITU_SOLI = tb65.CO_SITU_SOLI == "A" ? "Aberta" : (tb65.CO_SITU_SOLI == "C" ? "Cancelada" : (tb65.CO_SITU_SOLI == "E" ? "Entregue" : (tb65.CO_SITU_SOLI == "D" ? "Disponível" : (tb65.CO_SITU_SOLI == "T" ? "Em Trânsito" : "Finalizada")))),
                                 ENABLED = (tb65.CO_SITU_SOLI.ToUpper() == "F" || tb65.CO_SITU_SOLI.ToUpper() == "D"),
                                 VA_SOLI_ATEN = tb65.VA_SOLI_ATEN,
                                 Unidade = tb65.TB89_UNIDADES != null ? tb65.TB89_UNIDADES.SG_UNIDADE : "",
                                 QT_ITENS_SOLI_ATEN = tb65.QT_ITENS_SOLI_ATEN,
                                 ValorTotal = tb65.VA_SOLI_ATEN != null && tb65.QT_ITENS_SOLI_ATEN != null ? tb65.VA_SOLI_ATEN * tb65.QT_ITENS_SOLI_ATEN : null,
                                 NU_DOC_RECEB_SOLIC = tb65.NU_DOC_RECEB_SOLIC,
                                 URLImage = ""
                            });
            
            var res = documentos.ToList().OrderBy(p => p.NO_TIPO_SOLI);

            foreach (var item in res)
            {
                if (item.NU_DOC_RECEB_SOLIC != null)
                {
                    var ocoTb47 = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   where tb47.NU_DOC == item.NU_DOC_RECEB_SOLIC && tb47.IC_SIT_DOC == "A"
                                   select tb47).ToList();

                    item.URLImage = ocoTb47.Count() > 0 ? "~/Library/IMG/Gestor_BtnDel.png" : "~/Library/IMG/Gestor_CheckSucess.png";
                    //item.ENABLED = !(ocoTb47.Count() > 0) && item.ENABLED;
                }
                else
                    item.URLImage = "~/Library/IMG/Gestor_BtnEdit.png";

                if ((ocorBoleto == false) && item.ENABLED && item.NU_DOC_RECEB_SOLIC != null)
                {
                    ocorBoleto = true;
                }                
            }

            lnkBolCarne.Enabled = ocorBoleto;

            grvDocumentos.DataSource = res;

            grvDocumentos.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            AuxiliCarregamentos.CarregaBoletos(ddlBoletoSolic, LoginAuxili.CO_EMP, "S", 0, 0, false, false);

            ddlBoletoSolic.Items.Insert(0, new ListItem("", "0"));
        }
        #endregion

        protected void lnkBolCarne_Click(object sender, EventArgs e)
        {
            int ocorSelecGride = 0;

            foreach (GridViewRow linha in grvDocumentos.Rows)
            {
                //------------> Gerará boleto para os títulos selecionados
                if (((CheckBox)linha.FindControl("chkEntregue")).Checked)
                {
                    ocorSelecGride++;

                    if (ocorSelecGride > 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "(MSG) Deve ser selecionado apenas um item para impressão do boleto.");
                        return;
                    }
                }
            }

            if (ocorSelecGride == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "(MSG) Deve ser selecionado um item para impressão do boleto.");
                return;
            }
            //--------> Faz a instância de uma nova lista de InformacoesBoletoBancario na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //string numeroDocum = "";
            //bool ocoNumeroDocum = false;
            bool ocoAlterBoleto = false;
            int idBoleto = int.Parse(ddlBoletoSolic.SelectedValue);

            //--------> Varre toda a grid de Documentos
            foreach (GridViewRow linha in grvDocumentos.Rows)
            {
                //------------> Gerará boleto para os títulos selecionados
                if (((CheckBox)linha.FindControl("chkEntregue")).Checked)
                {
                    //----------------> Recebe as chaves primáris do Título
                    string strNudoc = ((HiddenField)linha.Cells[12].FindControl("hdNU_DOC_RECEB_SOLIC")).Value;
                    string strTipoFonte = "A";
                    string strInstruBoleto = "";

                    //if (numeroDocum == "")
                    //{
                    //    numeroDocum = strNudoc;
                    //}
                    //else
                    //{
                    //    if (numeroDocum == strNudoc)
                    //    {
                    //        ocoNumeroDocum = true;
                    //    }
                    //}

                    //----------------> Recebe o Título de Contas a Receber
                    TB47_CTA_RECEB tb47 = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                           where iTb47.NU_DOC == strNudoc
                                           select iTb47).FirstOrDefault();

                    if (tb47 == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "(MSG) Item selecionado não possui registro no contas a receber.");
                        return;
                    }
                    else if (tb47 != null) //&& !ocoNumeroDocum)
                    {
                        if (tb47.IC_SIT_DOC == "Q" || tb47.IC_SIT_DOC == "P")
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "(MSG) Título do item selecionado já foi quitado.");
                            return;
                        }

                        if (tb47.IC_SIT_DOC == "C")
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "(MSG) Título do item selecionado foi cancelado.");
                            return;
                        }

                        tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

                        if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                        {
                            var inforFinan = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                              join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on iTb25.CO_EMP equals tb83.CO_EMP
                                              where iTb25.CO_EMP == LoginAuxili.CO_EMP
                                              select new
                                              {
                                                  tb83.ID_BOLETO_SERV_SECRE
                                              }).First();

                            if (inforFinan.ID_BOLETO_SERV_SECRE == null)
                            {
                                if (idBoleto == 0)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this, "(MSG) Não é possivel gerar o Boleto. Selecione um boleto para impressão.");
                                    return;
                                }
                                else
                                {
                                    tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(idBoleto);
                                    tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                                    ocoAlterBoleto = true;
                                }
                            }
                            else
                            {
                                tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(inforFinan.ID_BOLETO_SERV_SECRE.Value);
                                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                                ocoAlterBoleto = true;
                            }
                            
                        }

                        tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                        tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                        tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                        tb47.TB108_RESPONSAVELReference.Load();
                        tb47.TB103_CLIENTEReference.Load();

                        int coResp = tb47.TB108_RESPONSAVEL != null ? tb47.TB108_RESPONSAVEL.CO_RESP : 0;
                        //--------> Faz a recuperação dos dados do Responsável do Aluno
                        var varResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                       where tb108.CO_RESP == coResp
                                       join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                                       join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                                       select new
                                       {
                                           BAIRRO = tb905.NO_BAIRRO,
                                           CEP = tb108.CO_CEP_RESP,
                                           CIDADE = tb904.NO_CIDADE,
                                           CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP,
                                           ENDERECO = tb108.DE_ENDE_RESP,
                                           NUMERO = tb108.NU_ENDE_RESP,
                                           COMPL = tb108.DE_COMP_RESP,
                                           NOME = tb108.NO_RESP,
                                           UF = tb904.CO_UF
                                       }).FirstOrDefault();

                        var varSacado = varResp;

                        //----------------> Faz a verificação para saber se o Título é gerado para o Aluno ou não
                        if (strTipoFonte == "A")
                        {
                            if (tb47.TB108_RESPONSAVEL == null)
                            {
                                AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                                return;
                            }
                        }

                        if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                            return;
                        }

                        //----------------> Recebe a Unidade
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

                        boleto.Desconto =
                            ((!tb47.VR_DES_DOC.HasValue ? 0
                                : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                    ? (boleto.Valor * tb47.VR_DES_DOC.Value / 100)
                                    : tb47.VR_DES_DOC.Value))
                            + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                                : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                    ? (boleto.Valor * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                    : tb47.VL_DES_BOLSA_ALUNO.Value)));

                        //----------------> Faz a adição de instruções ao Boleto
                        boleto.Instrucoes += "<br>";

                        string strCnpjCPF = "";

                        //----------------> Faz a adição de Instruções de Informações do Responsável do Aluno ou Informações do Cliente, de acordo com o tipo                    
                        if (strTipoFonte == "A")
                        {
                            //--------------------> Ano Refer: - Matrícula: - Nº NIRE:
                            //--------------------> Modalidade: - Série: - Turma: - Turno:
                            var inforAluno = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                              join tb07 in TB07_ALUNO.RetornaTodosRegistros() on iTb47.CO_ALU equals tb07.CO_ALU
                                              join tb01 in TB01_CURSO.RetornaTodosRegistros() on iTb47.CO_CUR equals tb01.CO_CUR
                                              join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on iTb47.CO_TUR equals tb129.CO_TUR
                                              where iTb47.CO_EMP == tb47.CO_EMP && iTb47.CO_CUR == tb47.CO_CUR && iTb47.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                              && iTb47.CO_ALU == tb47.CO_ALU
                                              select new
                                              {
                                                  tb01.TB44_MODULO.DE_MODU_CUR,
                                                  tb01.NO_CUR,
                                                  tb129.CO_SIGLA_TURMA,
                                                  iTb47.CO_ANO_MES_MAT,
                                                  tb07.NU_NIRE,
                                                  tb07.NO_ALU
                                              }).FirstOrDefault();

                            if (inforAluno != null)
                            {
                                var inforMatr = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                                 where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                                 && tb08.CO_ALU == tb47.CO_ALU
                                                 select new
                                                 {
                                                     tb08.CO_ALU_CAD,
                                                     TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                                 }).FirstOrDefault();

                                strCnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                                    " - Matrícula: " + (inforMatr != null ? inforMatr.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") : "XXXXX") +
                                     " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                                     "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                                     " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + (inforMatr != null ? inforMatr.TURNO : "XXXXX");

                                boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                            }

                            boleto.Instrucoes += strCnpjCPF + "<br>Referente: " + (tb47.DE_COM_HIST != null ? tb47.DE_COM_HIST : "Serviço/Atividade contratado.");
                        }

                        //----------------> Informações do Sacado
                        boleto.BairroSacado = varSacado.BAIRRO;
                        boleto.CepSacado = varSacado.CEP;
                        boleto.CidadeSacado = varSacado.CIDADE;
                        boleto.CpfCnpjSacado = varSacado.CPFCNPJ;
                        boleto.EnderecoSacado = varSacado.ENDERECO + " " + varSacado.NUMERO + " " + varSacado.COMPL;
                        boleto.NomeSacado = varSacado.NOME;
                        boleto.UfSacado = varSacado.UF;

                        //----------------> Faz a adição do Título na Sessão da lista de Boletos
                        ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                        if (ocoAlterBoleto)
                        {
                            //TB47_CTA_RECEB.SaveOrUpdate(tb47, true);
                            GestorEntities.CurrentContext.SaveChanges();
                        }
                    }                    
                }
            }

            //--------> Faz a exibição e gera os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

        protected void chkEntregue_CheckedChanged(object sender, EventArgs e)
        {
            //--------> Varre toda a grid de Documentos
            foreach (GridViewRow linha in grvDocumentos.Rows)
            {
                //------------> Gerará boleto para os títulos selecionados
                if (((CheckBox)linha.FindControl("chkEntregue")).Checked)
                {
                    string strNudoc = ((HiddenField)linha.Cells[12].FindControl("hdNU_DOC_RECEB_SOLIC")).Value;

                    TB47_CTA_RECEB tb47 = (from iTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                           where iTb47.NU_DOC == strNudoc
                                           select iTb47).FirstOrDefault();

                    tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO != null)
                    {                        
                        ddlBoletoSolic.Enabled = false;

                        var result = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                                      join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb227.CO_MODU_CUR equals tb44.CO_MODU_CUR into mod
                                      from tb44 in mod.DefaultIfEmpty()
                                      join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb227.CO_CUR equals tb01.CO_CUR into cur
                                      from tb01 in cur.DefaultIfEmpty()
                                      where tb227.ID_BOLETO == tb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO
                                      select new
                                      {
                                          tb227.ID_BOLETO,
                                          tb227.TB224_CONTA_CORRENTE,
                                          NO_MODU = tb44 != null ? (!String.IsNullOrEmpty(tb44.CO_SIGLA_MODU_CUR) ? tb44.CO_SIGLA_MODU_CUR : tb44.DE_MODU_CUR) : "",
                                          NO_CUR = tb01 != null ? (!String.IsNullOrEmpty(tb01.CO_SIGL_CUR) ? tb01.CO_SIGL_CUR : tb01.NO_CUR) : ""
                                      }).ToList();

                        var result2 = (from res in result
                                       join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on res.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                                       where tb225.CO_EMP == LoginAuxili.CO_EMP && res.TB224_CONTA_CORRENTE.CO_AGENCIA == tb225.CO_AGENCIA
                                       && tb225.IDEBANCO == res.TB224_CONTA_CORRENTE.IDEBANCO
                                       select new
                                       {
                                           res.ID_BOLETO,
                                           DESCRICAO = string.Format("BCO {0} - AGE {1} - CTA {2}{3}", res.TB224_CONTA_CORRENTE.IDEBANCO,
                                           res.TB224_CONTA_CORRENTE.CO_AGENCIA, res.TB224_CONTA_CORRENTE.CO_CONTA, (!String.IsNullOrEmpty(res.NO_MODU) ? (" - MOD " + res.NO_MODU + " - CUR " + res.NO_CUR) : ""))
                                       }).OrderBy(b => b.DESCRICAO);

                        ddlBoletoSolic.DataSource = result2;

                        ddlBoletoSolic.DataValueField = "ID_BOLETO";
                        ddlBoletoSolic.DataTextField = "DESCRICAO";
                        ddlBoletoSolic.DataBind();
                    }
                    else
                    {
                        var inforFinan = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                              join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on iTb25.CO_EMP equals tb83.CO_EMP
                                              where iTb25.CO_EMP == LoginAuxili.CO_EMP
                                              select new
                                              {
                                                  tb83.ID_BOLETO_SERV_SECRE
                                              }).First();

                        if (inforFinan.ID_BOLETO_SERV_SECRE != null)
                        {
                            ddlBoletoSolic.Enabled = false;
                            var result = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                                          join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb227.CO_MODU_CUR equals tb44.CO_MODU_CUR into mod
                                          from tb44 in mod.DefaultIfEmpty()
                                          join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb227.CO_CUR equals tb01.CO_CUR into cur
                                          from tb01 in cur.DefaultIfEmpty()
                                          where tb227.ID_BOLETO == inforFinan.ID_BOLETO_SERV_SECRE
                                          select new
                                          {
                                              tb227.ID_BOLETO,
                                              tb227.TB224_CONTA_CORRENTE,
                                              NO_MODU = tb44 != null ? (!String.IsNullOrEmpty(tb44.CO_SIGLA_MODU_CUR) ? tb44.CO_SIGLA_MODU_CUR : tb44.DE_MODU_CUR) : "",
                                              NO_CUR = tb01 != null ? (!String.IsNullOrEmpty(tb01.CO_SIGL_CUR) ? tb01.CO_SIGL_CUR : tb01.NO_CUR) : ""
                                          }).ToList();

                            var result2 = (from res in result
                                           join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on res.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                                           where tb225.CO_EMP == LoginAuxili.CO_EMP && res.TB224_CONTA_CORRENTE.CO_AGENCIA == tb225.CO_AGENCIA
                                           && tb225.IDEBANCO == res.TB224_CONTA_CORRENTE.IDEBANCO
                                           select new
                                           {
                                               res.ID_BOLETO,
                                               DESCRICAO = string.Format("BCO {0} - AGE {1} - CTA {2}{3}", res.TB224_CONTA_CORRENTE.IDEBANCO,
                                               res.TB224_CONTA_CORRENTE.CO_AGENCIA, res.TB224_CONTA_CORRENTE.CO_CONTA, (!String.IsNullOrEmpty(res.NO_MODU) ? (" - MOD " + res.NO_MODU + " - CUR " + res.NO_CUR) : ""))
                                           }).OrderBy(b => b.DESCRICAO);

                            ddlBoletoSolic.DataSource = result2;

                            ddlBoletoSolic.DataValueField = "ID_BOLETO";
                            ddlBoletoSolic.DataTextField = "DESCRICAO";
                            ddlBoletoSolic.DataBind();
                        }
                        else
                        {
                            ddlBoletoSolic.Enabled = true;
                            CarregaBoletos();
                        }                        
                    }
                    return;
                }
            }
        }
                                
        public class Documentos
        {
            public string NO_TIPO_SOLI { get; set; }
            public DateTime? DT_FIM_SOLI { get; set; }
            public DateTime DT_PREV_ENTR { get; set; }
            public bool CHECKED { get; set; }
            public bool ENABLED { get; set; }
            public int CO_TIPO_SOLI { get; set; }
            public string CO_SITU_SOLI { get; set; }
            public decimal? VA_SOLI_ATEN { get; set; }
            public string Unidade { get; set; }
            public int? QT_ITENS_SOLI_ATEN { get; set; }
            public decimal? ValorTotal { get; set; }
            public string URLImage { get; set; }
            public string DE_LOCALI_SOLI { get; set; }
            public string NU_DOC_RECEB_SOLIC { get; set; }            
        }
    }
}
