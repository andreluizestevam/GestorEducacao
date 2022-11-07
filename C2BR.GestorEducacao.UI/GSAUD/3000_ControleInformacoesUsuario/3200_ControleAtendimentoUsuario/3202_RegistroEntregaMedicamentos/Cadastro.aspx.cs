//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: REGISTRA SOLICITAÇÃO DE SERVIÇOS.
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
using System.Web.Security;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3202_RegistroEntregaMedicamentos
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> tipoR = AuxiliBaseApoio.chave(tipoRecebimentoFinanceiro.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
            CurrentPadraoCadastros.BarraCadastro.OnLoaded += new BarraCadastro.OnLoadHandler(BarraCadastroOnLoaded);
        }

        void BarraCadastroOnLoaded()
        {
            CurrentPadraoCadastros.BarraCadastro.HabilitarBotoes(CurrentPadraoCadastros.BarraCadastro.botaoDelete, false);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaBusca();

            if (IsPostBack) return;
            Session.Remove("ListaDoc");
            hdfOcorRegis.Value = "0";

            CarregaUnidades(ddlUnidadeEntrega);
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDadosBoleto();
            CarregaHistoricos();
            CarregaAgrupadores();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                ddlUnidadeEntrega.SelectedValue = LoginAuxili.CO_EMP.ToString();

                CarregaDadosBoleto();
                CarregaSolicitacoes();
                CarregaNumeroSolicitacao();

                //------------> Faz a habilitação dos campos
                HabilitaCampos(true);

                txtDataCadastro.Enabled = txtNire.Enabled = txtResponsavel.Enabled =
                txtNumeroSolicitacao.Enabled = txtAtendente.Enabled = false;

                txtAtendente.Text = LoginAuxili.NOME_USU_LOGADO;
                txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //------------> Carrega a informação de data de previsão
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb25.TB83_PARAMETROReference.Load();

                int? intDiasEntregaSolic = tb25.TB83_PARAMETRO.NU_DIAS_ENT_SOL;

                txtPrevisao.Text = intDiasEntregaSolic == null ?
                    DateTime.Now.ToString("dd/MM/yyyy") : txtPrevisao.Text = DateTime.Now.AddDays((int)intDiasEntregaSolic).ToString("dd/MM/yyyy");
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    chkSMS.Enabled = ddlUnidadeEntrega.Enabled = txtObservacao.Enabled =
                    txtPrevisao.Enabled = txtTelefone.Enabled = txtTelefoneResp.Enabled = true;
                }
                HabilitaCampos(true);
            }

            CarregaTotal();
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool ocoItemSolic = false;
            bool ocoRegisTitu = false;
            int qtdItemTotal = 0;
            int qtdTotalParce = 0;
            int numParce = 0;

            //if (chkConsolValorTitul.Checked && verificarQuitar())
            //{
            //    chkConsolValorTitul.Checked = false;
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possível consolidar pois existe(m) registro(s) marcado(s) para ser quitado.");
            //    return;
            //}

            if (CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique != CurrentPadraoCadastros.BarraCadastro.botaoDelete)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                    {
                        int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;
                        qtdItemTotal = qtdItemTotal + qtde;
                        ocoItemSolic = true;
                    }
                }

                if (!ocoItemSolic)
                {
                    AuxiliPagina.EnvioMensagemSucesso(this, "Deve ser selecionado pelo menos um item para registro de solicitação.");
                    return;
                }

                if (hdfOcorRegis.Value != "0")
                {
                    AuxiliPagina.EnvioMensagemSucesso(this, "Item já cadastrado. Selecionar outra opção.");
                    return;
                }

                //if (chkConsolValorTitul.Checked)
                //{
                //    if (ddlHistorico.SelectedValue == "" || ddlAgrupador.SelectedValue == "")
                //    {
                //        AuxiliPagina.EnvioMensagemSucesso(this, "Histórico e Agrupador devem ser informados.");
                //        return;
                //    }
                //}

                TB64_SOLIC_ATEND tb64 = RetornaEntidade();
                #region Alterar
                //--------> Só é permitida a alteração quando for uma nova solicitação ou se a solicitação estiver em aberto
                if (tb64.CO_SIT == null || tb64.CO_SIT == SituacaoSolicitacao.A.ToString())
                {
                    //------------> Carrega algumas informações dos parâmetros da unidade escolar
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb25.TB83_PARAMETROReference.Load();
                    TB83_PARAMETRO tb83 = tb25.TB83_PARAMETRO;

                    if (tb83.FLA_CTRLE_DIAS_SOLIC == "S" && tb83.NU_DIAS_ENT_SOL != null && DateTime.Parse(txtPrevisao.Text) > DateTime.Now.AddDays((int)tb83.NU_DIAS_ENT_SOL))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Previsão não pode ultrapassar " + DateTime.Now.AddDays((int)tb83.NU_DIAS_ENT_SOL).ToString("dd/MM/yyyy"));
                        return;
                    }

                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlAluno.SelectedValue));
                    tb07.TB25_EMPRESAReference.Load();

                    if (tb64.CO_SOLI_ATEN == 0)
                    {
                        tb64.CO_ALU = tb07.CO_ALU;
                        tb64.CO_EMP_ALU = tb07.TB25_EMPRESA.CO_EMP;
                        tb64.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
                        tb64.CO_EMP = LoginAuxili.CO_EMP;
                        tb64.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    }

                    tb64.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidadeEntrega.SelectedValue));

                    tb07.TB108_RESPONSAVELReference.Load();

                    tb64.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL;
                    //tb64.CO_ISEN_TAXA = chkIsento.Checked ? "S" : "N";
                    //if (!chkIsento.Checked)
                    //{
                    //    if (txtValorTotal.Text != "")
                    //    {
                    //        tb64.VL_TOTA_TAXA = Decimal.Parse(txtValorTotal.Text);
                    //    }
                    //}
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                        tb64.QT_TOTA_ITENS = qtdItemTotal;
                    tb64.ANO_SOLI_ATEN = DateTime.Now.Year;
                    tb64.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                    tb64.CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
                    tb64.CO_SIT = SituacaoSolicitacao.A.ToString();
                    tb64.CO_TELE_CONT = txtTelefone.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                    tb64.CO_TUR = int.Parse(ddlTurma.SelectedValue);
                    tb64.DE_OBS_SOLI = txtObservacao.Text;
                    tb64.DT_PREV_ENTR = DateTime.Parse(txtPrevisao.Text);
                    tb64.DT_SOLI_ATEN = DateTime.Parse(txtDataCadastro.Text);
                    tb64.MES_SOLI_ATEN = DateTime.Now.Month;
                    tb64.NU_DCTO_SOLIC = txtNumeroSolicitacao.Text;

                    tb64.NU_TELE_RESP_SOLIC_ATEND = txtTelefoneResp.Text.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");

                    if (chkSMS.Checked)
                        tb64.CO_FLA_SMS_SOLIC_ATEND = FlagEnvioSMS.S.ToString();
                    else
                        tb64.CO_FLA_SMS_SOLIC_ATEND = FlagEnvioSMS.N.ToString();

                    //------------> Quando é inserção, primeiro salva a solicitação e posteriormente os tipos de solicitação selecionados
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) && tb64.CO_SOLI_ATEN == 0)
                    {
                        if (GestorEntities.SaveOrUpdate(tb64) <= 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");
                            return;
                        }
                    }
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    {
                        //----------------> Varre toda a gride de Solicitações
                        foreach (GridViewRow linha in grdSolicitacoes.Rows)
                        {
                            if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                            {
                                TB65_HIST_SOLICIT tb65 = TB65_HIST_SOLICIT.RetornaPelaChavePrimaria(tb64.CO_EMP, tb64.CO_ALU, tb64.CO_CUR, tb64.CO_SOLI_ATEN, Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));

                                if (tb65.CO_SITU_SOLI == SituacaoItemSolicitacao.A.ToString() || tb65.CO_SITU_SOLI == SituacaoItemSolicitacao.D.ToString() ||
                                    tb65.CO_SITU_SOLI == SituacaoItemSolicitacao.F.ToString())
                                    tb65.TB25_EMPRESA = tb64.TB25_EMPRESA1;

                                if (GestorEntities.SaveOrUpdate(tb65) < 0)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item");
                                    return;
                                }

                                if (chkSMS.Checked)
                                {
                                    if (chkSMS.Checked && AuxiliValidacao.IsConnected())
                                    {
                                        if (txtTelefoneResp.Text != "")
                                        {
                                            var unidade = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                           join iTB83 in TB83_PARAMETRO.RetornaTodosRegistros() on iTb25.CO_EMP equals iTB83.CO_EMP
                                                           select new { iTb25.sigla, iTB83.DES_SMS_SECRE_SOLIC }).FirstOrDefault();

                                            if (unidade.DES_SMS_SECRE_SOLIC != null)
                                            {
                                                SMSAuxili.EnvioSMS(unidade.sigla,
                                                              unidade.DES_SMS_SECRE_SOLIC,
                                                              "55" + txtTelefoneResp.Text.Replace("(", "").Replace(")", "").Replace("-", ""),
                                                              DateTime.Now.Ticks.ToString());
                                            }
                                            else
                                            {
                                                SMSAuxili.EnvioSMS(unidade.sigla,
                                                              "(Portal Educacao) Foi efetuada a solicitação de algum(s) serviço(s) de secretaria.",
                                                              "55" + txtTelefoneResp.Text.Replace("(", "").Replace(")", "").Replace("-", ""),
                                                              DateTime.Now.Ticks.ToString());
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                #endregion
                    #region Novo
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    {
                        //decimal dcmTotal = 0;
                        int nuPar = 0;
                        //----------------> Varre toda a gride de Solicitações
                        foreach (GridViewRow linha in grdSolicitacoes.Rows)
                        {
                            if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                            {
                                int? coSequPc, coSequPCBanco, coSequPCCaixa, coHistorico, coAgrupador = null;
                                string nuDoc = "";
                                nuPar++;

                                var lTb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                             where iTb25.CO_EMP == LoginAuxili.CO_EMP
                                             select new { iTb25.CO_CTSOL_EMP, iTb25.CO_CTA_BANCO, iTb25.CO_CTA_CAIXA }).FirstOrDefault();

                                var tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));
                                tb66.TB89_UNIDADESReference.Load();
                                decimal? valorUnitario = tb66.VL_UNIT_SOLI;
                                //dcmTotal += valorUnitario.HasValue ? valorUnitario.Value : 0;
                                int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;
                                if (valorUnitario != null)
                                {
                                    qtdTotalParce = qtdTotalParce + 1;
                                }

                                //if (chkConsolValorTitul.Checked)
                                //{
                                //    coSequPc = lTb25.CO_CTSOL_EMP;
                                //    coSequPCBanco = lTb25.CO_CTA_BANCO;
                                //    coSequPCCaixa = lTb25.CO_CTA_CAIXA;
                                //    coHistorico = int.Parse(ddlHistorico.SelectedValue);
                                //    coAgrupador = int.Parse(ddlAgrupador.SelectedValue);
                                //    nuDoc = "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(10, '0') + ".01";
                                //}
                                //else
                                //{
                                    coSequPc = tb66.CO_SEQU_PC;
                                    coSequPCBanco = tb66.CO_SEQU_PC_BANCO;
                                    coSequPCCaixa = tb66.CO_SEQU_PC_CAIXA;
                                    coHistorico = tb66.ID_HISTOR_FINANC_TPSOLIC;
                                    coAgrupador = tb66.ID_AGRUP_RECEI_TPSOLIC;
                                    nuDoc = "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(10, '0') + "." + nuPar.ToString().PadLeft(2, '0');
                                //}

                                TB65_HIST_SOLICIT tb65 =
                                    new TB65_HIST_SOLICIT
                                    {
                                        TB25_EMPRESA = tb64.TB25_EMPRESA1,
                                        CO_EMP = tb64.CO_EMP,
                                        CO_ALU = tb64.CO_ALU,
                                        CO_CUR = tb64.CO_CUR,
                                        TB64_SOLIC_ATEND = tb64,
                                        CO_SITU_SOLI = SituacaoItemSolicitacao.A.ToString(),
                                        DT_STATUS = DateTime.Now,
                                        TB66_TIPO_SOLIC = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0])),
                                        //VA_SOLI_ATEN = chkIsento.Checked ? null : valorUnitario,
                                        QT_ITENS_SOLI_ATEN = qtde,
                                        CO_SEQU_PC = coSequPc,
                                        CO_SEQU_PC_BANCO = coSequPCBanco,
                                        CO_SEQU_PC_CAIXA = coSequPCCaixa,
                                        ID_AGRUP_RECEI_TPSOLIC = coAgrupador,
                                        ID_HISTOR_FINANC_TPSOLIC = coHistorico,
                                        TB89_UNIDADES = tb66.TB89_UNIDADES,
                                        //NU_DOC_RECEB_SOLIC = chkIsento.Checked ? null : nuDoc
                                    };

                                if (GestorEntities.SaveOrUpdate(tb65) < 0)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item");
                                    return;
                                }
                            }
                        }
                    }
                }
                    #endregion

                if (1 == 1)
                {
                    if (chkSMS.Checked)
                    {
                        if (chkSMS.Checked && AuxiliValidacao.IsConnected())
                        {
                            if (txtTelefoneResp.Text != "")
                            {
                                var unidade = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               join iTB83 in TB83_PARAMETRO.RetornaTodosRegistros() on iTb25.CO_EMP equals iTB83.CO_EMP
                                               select new { iTb25.sigla, iTB83.DES_SMS_SECRE_SOLIC }).FirstOrDefault();

                                if (unidade.DES_SMS_SECRE_SOLIC != null)
                                {
                                    SMSAuxili.EnvioSMS(unidade.sigla,
                                                  unidade.DES_SMS_SECRE_SOLIC,
                                                  "55" + txtTelefoneResp.Text.Replace("(", "").Replace(")", "").Replace("-", ""),
                                                  DateTime.Now.Ticks.ToString());
                                }
                                else
                                {
                                    SMSAuxili.EnvioSMS(unidade.sigla,
                                                  "(Portal Educacao) Foi efetuada a solicitação de algum(s) serviço(s) de secretaria.",
                                                  "55" + txtTelefoneResp.Text.Replace("(", "").Replace(")", "").Replace("-", ""),
                                                  DateTime.Now.Ticks.ToString());
                                }
                            }
                        }
                    }

                    //if ((txtValorTotal.Text != "") && (!chkIsento.Checked) && (chkAtualiFinan.Checked) && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    //{
                    //    List<TitulosSolicitacao> lst = new List<TitulosSolicitacao>();

                    //    if (chkConsolValorTitul.Checked)
                    //    {
                    //        #region Titulo Consolidado

                    //        if (Decimal.Parse(txtValorTotal.Text) > 0)
                    //        {
                    //            qtdTotalParce = 1;
                    //            int coEmp = LoginAuxili.CO_EMP;
                    //            int coHist = int.Parse(ddlHistorico.SelectedValue);
                    //            int coAgrup = int.Parse(ddlAgrupador.SelectedValue);

                    //            var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    //            if (tb149.TP_CTRLE_SECRE_ESCOL == "I")
                    //            {
                    //                if (tb149.FL_INCLU_TAXA_CAR_SECRE == "S")
                    //                {
                    //                    var inforFinan = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                    //                                      join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                    //                                      where tb25.CO_EMP == coEmp
                    //                                      select new
                    //                                      {
                    //                                          tb25.CO_CTSOL_EMP,
                    //                                          tb83.ID_BOLETO_SERV_SECRE
                    //                                      }).First();

                    //                    if (inforFinan.CO_CTSOL_EMP != null)
                    //                    {
                    //                        TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    //                        int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

                    //                        TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    //                        tb07.TB108_RESPONSAVELReference.Load();
                    //                        TB108_RESPONSAVEL tb108 = tb07.TB108_RESPONSAVEL;
                    //                        tb108.TB74_UFReference.Load();

                    //                        if (tb108 == null || tb25.CO_CTSOL_EMP == null)
                    //                        {
                    //                            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    //                                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro salvo com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //                            else
                    //                                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //                        }

                    //                        //--------> Faz a verificação para saber se já existe registro cadastrado no Contas a Receber
                    //                        TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(tb25.CO_EMP, "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(10, '0') + ".01", 1);

                    //                        //--------> Se não, cria um novo registro
                    //                        if (tb47 == null)
                    //                        {
                    //                            tb47 = new TB47_CTA_RECEB();
                    //                            tb47.CO_ALU = tb07.CO_ALU;

                    //                            TB07_ALUNO aluno = TB07_ALUNO.RetornaPeloCoAlu(tb07.CO_ALU);
                    //                            aluno.TB108_RESPONSAVELReference.Load();
                    //                            tb47.TB108_RESPONSAVEL = aluno.TB108_RESPONSAVEL;
                    //                            tb47.CO_ANO_MES_MAT = TB08_MATRCUR.RetornaPeloCurso(tb07.CO_ALU, int.Parse(ddlSerieCurso.SelectedValue)).CO_ANO_MES_MAT;
                    //                            tb47.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
                    //                            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb25.CO_EMP);
                    //                            tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                    //                            tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                    //                            tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                    //                            tb47.CO_FLAG_TP_VALOR_DES = "V";
                    //                            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                    //                            tb47.CO_FLAG_TP_VALOR_OUT = "V";
                    //                            tb47.FL_EMITE_BOLETO = tb149.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                    //                            tb47.CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
                    //                            tb47.CO_NOS_NUM = tb25.CO_PROX_NOS_NUM;
                    //                            tb47.DE_COM_HIST = "Serviço de Secretaria N° " + txtNumeroSolicitacao.Text;
                    //                            tb47.DT_ALT_REGISTRO = DateTime.Now;
                    //                            tb47.DT_CAD_DOC = DateTime.Now;
                    //                            tb47.DT_EMISS_DOCTO = DateTime.Now;
                    //                            tb47.DT_SITU_DOC = DateTime.Now;
                    //                            tb47.DT_VEN_DOC = DateTime.Parse(txtPrevisao.Text);
                    //                            tb47.IC_SIT_DOC = "A";
                    //                            tb47.NU_DOC = "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(10, '0') + ".01";
                    //                            lst.Add(new TitulosSolicitacao
                    //                            {
                    //                                NuDoc = tb47.NU_DOC,
                    //                                NuPar = 1
                    //                            });
                    //                            tb47.NU_PAR = 1;
                    //                            tb47.QT_PAR = 1;
                    //                            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    //                            tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                    //                            tb47.TB099_CENTRO_CUSTO = tb25.CO_CENT_CUSSOL.HasValue ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value) : null;
                    //                            tb47.TB25_EMPRESA = tb25;
                    //                            tb47.TB227_DADOS_BOLETO_BANCARIO = tb149.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? inforFinan.ID_BOLETO_SERV_SECRE != null ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria((int)inforFinan.ID_BOLETO_SERV_SECRE) : null : null;
                    //                            tb47.TB39_HISTORICO = coHist != 0 ? TB39_HISTORICO.RetornaPelaChavePrimaria(coHist) : null;
                    //                            tb47.CO_AGRUP_RECDESP = coAgrup;
                    //                            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTSOL_EMP.Value);
                    //                            tb47.CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                    //                            tb47.CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                    //                            tb47.TP_CLIENTE_DOC = "A";
                    //                            tb47.VR_JUR_DOC = (tb25.VL_PEC_JUROS == null ? tb25.VL_PEC_JUROS.GetValueOrDefault() : decimal.Parse(string.Format("{0:0.0000}", tb25.VL_PEC_JUROS)));
                    //                            tb47.VR_MUL_DOC = tb25.VL_PEC_MULTA;
                    //                            tb47.VR_PAR_DOC = decimal.Parse(txtValorTotal.Text);
                    //                            tb47.VR_TOT_DOC = decimal.Parse(txtValorTotal.Text);

                    //                            GestorEntities.SaveOrUpdate(tb47);
                    //                        }

                    //                        hdfOcorRegis.Value = "1";
                    //                        HabilitaCampos(false);
                    //                        grdSolicitacoes.Enabled = false;
                    //                        //lnkBolCarne.Enabled = lnkRecSolic.Enabled = true;
                    //                        lblResul.InnerText = "Registro cadastrado com sucesso. Para impressão do boleto e recibo clicar no botão correspondente.";
                    //                        lblResul.Visible = true;
                    //                        //AuxiliPagina.EnvioMensagemSucesso(this, "Registro cadastrado com sucesso. Para impressão do boleto clicar no botão 'BOLETO'.");
                    //                        //return;
                    //                    }
                    //                    else
                    //                    {
                    //                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //                    }
                    //                }
                    //            }//Fim tipo de controle de secretária escola I
                    //            else
                    //            {
                    //                var inforBol = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                    //                                join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                    //                                where tb25.CO_EMP == coEmp
                    //                                select new
                    //                                {
                    //                                    tb83.FL_INCLU_TAXA_CAR_SECRE,
                    //                                    tb25.CO_FLAG_GERA_BOLETO_SERV_SECR,
                    //                                    tb83.ID_BOLETO_SERV_SECRE,
                    //                                    tb25.CO_CTSOL_EMP
                    //                                }).First();

                    //                if (inforBol.FL_INCLU_TAXA_CAR_SECRE == "S")
                    //                {
                    //                    if (inforBol.CO_CTSOL_EMP != null)
                    //                    {
                    //                        TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    //                        int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

                    //                        TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    //                        tb07.TB108_RESPONSAVELReference.Load();
                    //                        TB108_RESPONSAVEL tb108 = tb07.TB108_RESPONSAVEL;
                    //                        tb108.TB74_UFReference.Load();

                    //                        if (tb108 == null)
                    //                        {
                    //                            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //                        }

                    //                        if (tb25.CO_CTSOL_EMP == null)
                    //                        {
                    //                            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //                        }

                    //                        //--------> Faz a verificação para saber se já existe registro cadastrado no Contas a Receber
                    //                        TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(tb25.CO_EMP, "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(5, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(7, '0') + ".01", 1);

                    //                        //--------> Se não, cria um novo registro
                    //                        if (tb47 == null)
                    //                        {
                    //                            tb47 = new TB47_CTA_RECEB();
                    //                            tb47.CO_ALU = tb07.CO_ALU;

                    //                            TB07_ALUNO aluno = TB07_ALUNO.RetornaPeloCoAlu(tb07.CO_ALU);
                    //                            aluno.TB108_RESPONSAVELReference.Load();
                    //                            tb47.TB108_RESPONSAVEL = aluno.TB108_RESPONSAVEL;
                    //                            tb47.CO_ANO_MES_MAT = TB08_MATRCUR.RetornaPeloCurso(tb07.CO_ALU, int.Parse(ddlSerieCurso.SelectedValue)).CO_ANO_MES_MAT;
                    //                            tb47.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
                    //                            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb25.CO_EMP);
                    //                            tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                    //                            tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                    //                            tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                    //                            tb47.CO_FLAG_TP_VALOR_DES = "V";
                    //                            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                    //                            tb47.CO_FLAG_TP_VALOR_OUT = "V";
                    //                            tb47.FL_EMITE_BOLETO = inforBol.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                    //                            tb47.CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
                    //                            tb47.CO_NOS_NUM = tb25.CO_PROX_NOS_NUM;
                    //                            tb47.DE_COM_HIST = "Serviço de Secretaria N° " + txtNumeroSolicitacao.Text;
                    //                            tb47.DT_ALT_REGISTRO = DateTime.Now;
                    //                            tb47.DT_CAD_DOC = DateTime.Now;
                    //                            tb47.DT_EMISS_DOCTO = DateTime.Now;
                    //                            tb47.DT_SITU_DOC = DateTime.Now;
                    //                            tb47.DT_VEN_DOC = DateTime.Parse(txtPrevisao.Text);
                    //                            tb47.IC_SIT_DOC = "A";
                    //                            tb47.NU_DOC = "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(5, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(7, '0') + ".01";
                    //                            lst.Add(new TitulosSolicitacao
                    //                            {
                    //                                NuDoc = tb47.NU_DOC,
                    //                                NuPar = 1
                    //                            });
                    //                            tb47.NU_PAR = 1;
                    //                            tb47.QT_PAR = 1;
                    //                            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    //                            tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                    //                            tb47.TB099_CENTRO_CUSTO = tb25.CO_CENT_CUSSOL.HasValue ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value) : null;
                    //                            tb47.TB25_EMPRESA = tb25;
                    //                            tb47.TB227_DADOS_BOLETO_BANCARIO = inforBol.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? inforBol.ID_BOLETO_SERV_SECRE != null ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria((int)inforBol.ID_BOLETO_SERV_SECRE) : null : null;
                    //                            tb47.TB39_HISTORICO = coHist != 0 ? TB39_HISTORICO.RetornaPelaChavePrimaria(coHist) : null;
                    //                            tb47.CO_AGRUP_RECDESP = coAgrup;
                    //                            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTSOL_EMP.Value);
                    //                            tb47.CO_SEQU_PC_BANCO = tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                    //                            tb47.CO_SEQU_PC_CAIXA = tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                    //                            tb47.TP_CLIENTE_DOC = "A";
                    //                            tb47.VR_JUR_DOC = (tb25.VL_PEC_JUROS == null ? tb25.VL_PEC_JUROS.GetValueOrDefault() : decimal.Parse(string.Format("{0:0.0000}", tb25.VL_PEC_JUROS)));
                    //                            tb47.VR_MUL_DOC = tb25.VL_PEC_MULTA;
                    //                            tb47.VR_PAR_DOC = decimal.Parse(txtValorTotal.Text);
                    //                            tb47.VR_TOT_DOC = decimal.Parse(txtValorTotal.Text);

                    //                            GestorEntities.SaveOrUpdate(tb47);
                    //                        }

                    //                        hdfOcorRegis.Value = "1";
                    //                        HabilitaCampos(false);
                    //                        grdSolicitacoes.Enabled = false;
                    //                        //lnkBolCarne.Enabled = lnkRecSolic.Enabled = true;
                    //                        lblResul.InnerText = "Registro cadastrado com sucesso. Para impressão do boleto e recibo clicar no botão correspondente.";
                    //                        lblResul.Visible = true;
                    //                        //AuxiliPagina.EnvioMensagemSucesso(this, "Registro cadastrado com sucesso. Para impressão do boleto clicar no botão 'BOLETO'.");
                    //                        //return;
                    //                    }
                    //                    else
                    //                    {
                    //                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //                    }
                    //                }
                    //            }//Fim tipo de controle de secretária escola fora I
                    //            Session["ListaDoc"] = lst;
                    //        }//If existe valor total
                    //        else
                    //            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //        #endregion
                    //    }
                    //    else
                    //    {
                    //        #region Título Não Consolidado
                    //        //----------------> Varre toda a gride de Solicitações
                    //        foreach (GridViewRow linha in grdSolicitacoes.Rows)
                    //        {
                    //            if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                    //            {
                    //                var tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));
                    //                decimal? valorUnitario = tb66.VL_UNIT_SOLI;
                    //                //dcmTotal += valorUnitario.HasValue ? valorUnitario.Value : 0;
                    //                int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;

                    //                if (valorUnitario != null)
                    //                {
                    //                    int coEmp = LoginAuxili.CO_EMP;
                    //                    int coHist = tb66.ID_HISTOR_FINANC_TPSOLIC != null ? tb66.ID_HISTOR_FINANC_TPSOLIC.Value : 0;
                    //                    int coAgrup = tb66.ID_AGRUP_RECEI_TPSOLIC != null ? tb66.ID_AGRUP_RECEI_TPSOLIC.Value : 0;
                    //                    int coSequPc = tb66.CO_SEQU_PC != null ? tb66.CO_SEQU_PC.Value : 0;
                    //                    int coSequPcBanco = tb66.CO_SEQU_PC_BANCO != null ? tb66.CO_SEQU_PC_BANCO.Value : 0;
                    //                    int coSequPcCaixa = tb66.CO_SEQU_PC_CAIXA != null ? tb66.CO_SEQU_PC_CAIXA.Value : 0;

                    //                    var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    //                    if (tb149.TP_CTRLE_SECRE_ESCOL == "I")
                    //                    {
                    //                        if (tb149.FL_INCLU_TAXA_CAR_SECRE == "S")
                    //                        {
                    //                            var inforFinan = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                    //                                              join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                    //                                              where tb25.CO_EMP == coEmp
                    //                                              select new
                    //                                              {
                    //                                                  tb25.CO_CTSOL_EMP,
                    //                                                  tb83.ID_BOLETO_SERV_SECRE
                    //                                              }).First();

                    //                            if (inforFinan.CO_CTSOL_EMP != null || coSequPc != 0)
                    //                            {
                    //                                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    //                                int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

                    //                                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    //                                tb07.TB108_RESPONSAVELReference.Load();
                    //                                TB108_RESPONSAVEL tb108 = tb07.TB108_RESPONSAVEL;
                    //                                tb108.TB74_UFReference.Load();

                    //                                numParce = numParce + 1;
                    //                                //--------> Faz a verificação para saber se já existe registro cadastrado no Contas a Receber
                    //                                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(tb25.CO_EMP, "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(10, '0') + "." + numParce.ToString().PadLeft(2, '0'), numParce);

                    //                                //--------> Se não, cria um novo registro
                    //                                if (tb47 == null && tb108 != null)
                    //                                {
                    //                                    tb47 = new TB47_CTA_RECEB();
                    //                                    tb47.CO_ALU = tb07.CO_ALU;

                    //                                    TB07_ALUNO aluno = TB07_ALUNO.RetornaPeloCoAlu(tb07.CO_ALU);
                    //                                    aluno.TB108_RESPONSAVELReference.Load();
                    //                                    tb47.TB108_RESPONSAVEL = aluno.TB108_RESPONSAVEL;
                    //                                    tb47.CO_ANO_MES_MAT = TB08_MATRCUR.RetornaPeloCurso(tb07.CO_ALU, int.Parse(ddlSerieCurso.SelectedValue)).CO_ANO_MES_MAT;
                    //                                    tb47.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
                    //                                    tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb25.CO_EMP);
                    //                                    tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                    //                                    tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_DES = "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_OUT = "V";
                    //                                    tb47.FL_EMITE_BOLETO = tb149.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                    //                                    tb47.CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
                    //                                    tb47.CO_NOS_NUM = tb25.CO_PROX_NOS_NUM;
                    //                                    tb47.DE_COM_HIST = "Serviço de Secretaria N° " + txtNumeroSolicitacao.Text;
                    //                                    tb47.DT_ALT_REGISTRO = DateTime.Now;
                    //                                    tb47.DT_CAD_DOC = DateTime.Now;
                    //                                    tb47.DT_EMISS_DOCTO = DateTime.Now;
                    //                                    tb47.DT_SITU_DOC = DateTime.Now;
                    //                                    tb47.DT_VEN_DOC = DateTime.Parse(txtPrevisao.Text);
                    //                                    tb47.IC_SIT_DOC = "A";
                    //                                    tb47.NU_DOC = "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(10, '0') + "." + numParce.ToString().PadLeft(2, '0');
                    //                                    lst.Add(new TitulosSolicitacao
                    //                                    {
                    //                                        NuDoc = tb47.NU_DOC,
                    //                                        NuPar = numParce
                    //                                    });
                    //                                    tb47.NU_PAR = numParce;
                    //                                    tb47.QT_PAR = qtdTotalParce;
                    //                                    tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    //                                    tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                    //                                    tb47.TB099_CENTRO_CUSTO = tb25.CO_CENT_CUSSOL.HasValue ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value) : null;
                    //                                    tb47.TB25_EMPRESA = tb25;
                    //                                    tb47.TB227_DADOS_BOLETO_BANCARIO = tb149.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? inforFinan.ID_BOLETO_SERV_SECRE != null ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria((int)inforFinan.ID_BOLETO_SERV_SECRE) : null : null;
                    //                                    tb47.TB39_HISTORICO = coHist != 0 ? TB39_HISTORICO.RetornaPelaChavePrimaria(coHist) : tb25.CO_HIST_SOL != null ? TB39_HISTORICO.RetornaPelaChavePrimaria(tb25.CO_HIST_SOL.Value) : null;
                    //                                    tb47.CO_AGRUP_RECDESP = coAgrup != 0 ? (int?)coAgrup : null;
                    //                                    tb47.TB56_PLANOCTA = coSequPc != 0 ? TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc) : TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTSOL_EMP.Value);
                    //                                    tb47.CO_SEQU_PC_BANCO = coSequPcBanco != 0 ? coSequPcBanco : tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                    //                                    tb47.CO_SEQU_PC_CAIXA = coSequPcCaixa != 0 ? coSequPcCaixa : tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                    //                                    tb47.TP_CLIENTE_DOC = "A";
                    //                                    tb47.VR_JUR_DOC = (tb25.VL_PEC_JUROS == null ? tb25.VL_PEC_JUROS.GetValueOrDefault() : decimal.Parse(string.Format("{0:0.0000}", tb25.VL_PEC_JUROS)));
                    //                                    tb47.VR_MUL_DOC = tb25.VL_PEC_MULTA;
                    //                                    tb47.VR_PAR_DOC = valorUnitario.Value * qtde;
                    //                                    tb47.VR_TOT_DOC = decimal.Parse(txtValorTotal.Text);

                    //                                    #region Verifica se é para quitar ou não o título
                    //                                    if (((CheckBox)linha.Cells[6].Controls[0]).Checked)
                    //                                    {
                    //                                        tb47.FL_TIPO_RECEB = tipoR[tipoRecebimentoFinanceiro.B];
                    //                                        tb47.FL_ORIGEM_PGTO = "X";
                    //                                        tb47.CO_COL_BAIXA = LoginAuxili.CO_COL;
                    //                                        tb47.DT_REC_DOC = DateTime.Now;
                    //                                        tb47.DT_MOV_CAIXA = DateTime.Now;
                    //                                        tb47.IC_SIT_DOC = "Q";

                    //                                        tb47.VL_EXCE_PAG = null;
                    //                                        tb47.VR_MUL_PAG = tb47.VR_MUL_DOC;
                    //                                        tb47.VR_JUR_PAG = tb47.VR_JUR_DOC;
                    //                                        tb47.VR_DES_PAG = null;
                    //                                        tb47.VR_DES_BOLSA_PAG = null;
                    //                                        tb47.VR_OUT_PAG = null;
                    //                                        tb47.VR_PAG = tb47.VR_PAR_DOC;
                    //                                    }
                    //                                    #endregion


                    //                                    GestorEntities.SaveOrUpdate(tb47);

                    //                                    ocoRegisTitu = true;
                    //                                }
                    //                            }
                    //                        }
                    //                    }//Fim tipo de controle de secretária escola I
                    //                    else
                    //                    {
                    //                        var inforBol = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                    //                                        join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                    //                                        where tb25.CO_EMP == coEmp
                    //                                        select new
                    //                                        {
                    //                                            tb83.FL_INCLU_TAXA_CAR_SECRE,
                    //                                            tb25.CO_FLAG_GERA_BOLETO_SERV_SECR,
                    //                                            tb83.ID_BOLETO_SERV_SECRE,
                    //                                            tb25.CO_CTSOL_EMP
                    //                                        }).First();

                    //                        if (inforBol.FL_INCLU_TAXA_CAR_SECRE == "S")
                    //                        {
                    //                            if (inforBol.CO_CTSOL_EMP != null || coSequPc != 0)
                    //                            {
                    //                                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    //                                int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

                    //                                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    //                                tb07.TB108_RESPONSAVELReference.Load();
                    //                                TB108_RESPONSAVEL tb108 = tb07.TB108_RESPONSAVEL;
                    //                                tb108.TB74_UFReference.Load();

                    //                                numParce = numParce + 1;
                    //                                //--------> Faz a verificação para saber se já existe registro cadastrado no Contas a Receber
                    //                                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(tb25.CO_EMP, "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(5, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(7, '0') + "." + numParce.ToString().PadLeft(2, '0'), numParce);

                    //                                //--------> Se não, cria um novo registro
                    //                                if (tb47 == null && tb108 != null)
                    //                                {
                    //                                    tb47 = new TB47_CTA_RECEB();
                    //                                    tb47.CO_ALU = tb07.CO_ALU;

                    //                                    TB07_ALUNO aluno = TB07_ALUNO.RetornaPeloCoAlu(tb07.CO_ALU);
                    //                                    aluno.TB108_RESPONSAVELReference.Load();
                    //                                    tb47.TB108_RESPONSAVEL = aluno.TB108_RESPONSAVEL;
                    //                                    tb47.CO_ANO_MES_MAT = TB08_MATRCUR.RetornaPeloCurso(tb07.CO_ALU, int.Parse(ddlSerieCurso.SelectedValue)).CO_ANO_MES_MAT;
                    //                                    tb47.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
                    //                                    tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb25.CO_EMP);
                    //                                    tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                    //                                    tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_DES = "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                    //                                    tb47.CO_FLAG_TP_VALOR_OUT = "V";
                    //                                    tb47.FL_EMITE_BOLETO = inforBol.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                    //                                    tb47.CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
                    //                                    tb47.CO_NOS_NUM = tb25.CO_PROX_NOS_NUM;
                    //                                    tb47.DE_COM_HIST = "Serviço de Secretaria N° " + txtNumeroSolicitacao.Text;
                    //                                    tb47.DT_ALT_REGISTRO = DateTime.Now;
                    //                                    tb47.DT_CAD_DOC = DateTime.Now;
                    //                                    tb47.DT_EMISS_DOCTO = DateTime.Now;
                    //                                    tb47.DT_SITU_DOC = DateTime.Now;
                    //                                    tb47.DT_VEN_DOC = DateTime.Parse(txtPrevisao.Text);
                    //                                    tb47.IC_SIT_DOC = "A";
                    //                                    tb47.NU_DOC = "SE" + txtNumeroSolicitacao.Text.Substring(2, 2) + "." + txtNumeroSolicitacao.Text.Substring(5, 2) + "." + txtNumeroSolicitacao.Text.Substring(8, 6).PadLeft(7, '0') + "." + numParce.ToString().PadLeft(2, '0');
                    //                                    lst.Add(new TitulosSolicitacao
                    //                                    {
                    //                                        NuDoc = tb47.NU_DOC,
                    //                                        NuPar = numParce
                    //                                    });
                    //                                    tb47.NU_PAR = numParce;
                    //                                    tb47.QT_PAR = qtdTotalParce;
                    //                                    tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    //                                    tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                    //                                    tb47.TB099_CENTRO_CUSTO = tb25.CO_CENT_CUSSOL.HasValue ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value) : null;
                    //                                    tb47.TB25_EMPRESA = tb25;
                    //                                    tb47.TB227_DADOS_BOLETO_BANCARIO = inforBol.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? inforBol.ID_BOLETO_SERV_SECRE != null ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria((int)inforBol.ID_BOLETO_SERV_SECRE) : null : null;
                    //                                    tb47.TB39_HISTORICO = coHist != 0 ? TB39_HISTORICO.RetornaPelaChavePrimaria(coHist) : tb25.CO_HIST_SOL != null ? TB39_HISTORICO.RetornaPelaChavePrimaria(tb25.CO_HIST_SOL.Value) : null;
                    //                                    tb47.CO_AGRUP_RECDESP = coAgrup != 0 ? (int?)coAgrup : null;
                    //                                    tb47.TB56_PLANOCTA = coSequPc != 0 ? TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc) : TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTSOL_EMP.Value);
                    //                                    tb47.CO_SEQU_PC_BANCO = coSequPcBanco != 0 ? coSequPcBanco : tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                    //                                    tb47.CO_SEQU_PC_CAIXA = coSequPcCaixa != 0 ? coSequPcCaixa : tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                    //                                    tb47.TP_CLIENTE_DOC = "A";
                    //                                    tb47.VR_JUR_DOC = (tb25.VL_PEC_JUROS == null ? tb25.VL_PEC_JUROS.GetValueOrDefault() : decimal.Parse(string.Format("{0:0.0000}", tb25.VL_PEC_JUROS)));
                    //                                    tb47.VR_MUL_DOC = tb25.VL_PEC_MULTA;
                    //                                    tb47.VR_PAR_DOC = valorUnitario.Value * qtde;
                    //                                    tb47.VR_TOT_DOC = decimal.Parse(txtValorTotal.Text);

                    //                                    #region Verifica se é para quitar ou não o título
                    //                                    if (((CheckBox)linha.Cells[6].Controls[0]).Checked)
                    //                                    {
                    //                                        tb47.FL_TIPO_RECEB = tipoR[tipoRecebimentoFinanceiro.B];
                    //                                        tb47.FL_ORIGEM_PGTO = "X";
                    //                                        tb47.CO_COL_BAIXA = LoginAuxili.CO_COL;
                    //                                        tb47.DT_REC_DOC = DateTime.Now;
                    //                                        tb47.DT_MOV_CAIXA = DateTime.Now;
                    //                                        tb47.IC_SIT_DOC = "Q";

                    //                                        tb47.VL_EXCE_PAG = null;
                    //                                        tb47.VR_MUL_PAG = tb47.VR_MUL_DOC;
                    //                                        tb47.VR_JUR_PAG = tb47.VR_JUR_DOC;
                    //                                        tb47.VR_DES_PAG = null;
                    //                                        tb47.VR_DES_BOLSA_PAG = null;
                    //                                        tb47.VR_OUT_PAG = null;
                    //                                        tb47.VR_PAG = tb47.VR_PAR_DOC;
                    //                                    }
                    //                                    #endregion

                    //                                    GestorEntities.SaveOrUpdate(tb47);

                    //                                    ocoRegisTitu = true;
                    //                                }
                    //                            }
                    //                        }//Fim incluir taxa de secretaria
                    //                    }//Fim tipo de controle de secretária escola fora I
                    //                }//Fim tipo de valor unitário
                    //            }//Fim if se marcado ou não

                    //        }//Fim foreach na grid

                    //        if (ocoRegisTitu)
                    //        {
                    //            hdfOcorRegis.Value = "1";
                    //            HabilitaCampos(false);
                    //            grdSolicitacoes.Enabled = false;
                    //            //lnkBolCarne.Enabled = lnkRecSolic.Enabled = true;
                    //            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro(s) e título(s) cadastrado(s) com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    //        }
                    //        Session["ListaDoc"] = lst;
                    //        #endregion
                    //    }
                    //}
                    else
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB64_SOLIC_ATEND tb64 = RetornaEntidade();
            TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            if (tb64 != null)
            {
                tb64.TB25_EMPRESA1Reference.Load();
                tb64.TB03_COLABORReference.Load();
                tb64.TB108_RESPONSAVELReference.Load();

                if (tb64.CO_SIT != SituacaoSolicitacao.A.ToString())
                    HabilitaCampos(false);

                ddlUnidadeEntrega.SelectedValue = tb64.TB25_EMPRESA1.CO_EMP.ToString();

                ddlModalidade.SelectedValue = tb64.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb64.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb64.CO_TUR.ToString();
                CarregaAluno();

                //------------> Carrega informações do Aluno
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tb64.CO_ALU);
                txtNire.Text = tb07.NU_NIRE.ToString("00000000").Insert(5, ".").Insert(2, ".");

                ddlAluno.SelectedValue = tb64.CO_ALU.ToString();
                txtTelefone.Text = tb64.CO_TELE_CONT.ToString();

                if (tb64.TB108_RESPONSAVEL != null)
                    txtResponsavel.Text = tb64.TB108_RESPONSAVEL.NO_RESP.ToString();

                txtTelefoneResp.Text = tb64.NU_TELE_RESP_SOLIC_ATEND != null ? tb64.NU_TELE_RESP_SOLIC_ATEND.ToString() : "";
                txtNumeroSolicitacao.Text = tb64.NU_DCTO_SOLIC;

                var tb65 = from lTb65 in TB65_HIST_SOLICIT.RetornaTodosRegistros()
                           join lTb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros() on lTb65.CO_TIPO_SOLI equals lTb66.CO_TIPO_SOLI
                           where lTb65.CO_EMP.Equals(LoginAuxili.CO_EMP)
                           && lTb65.CO_ALU.Equals(tb64.CO_ALU) && lTb65.CO_CUR.Equals(tb64.CO_CUR) && lTb65.CO_SOLI_ATEN.Equals(tb64.CO_SOLI_ATEN)
                           select new
                           {
                               Codigo = lTb66.CO_TIPO_SOLI,
                               Descricao = lTb66.NO_TIPO_SOLI,
                               Valor = lTb65.VA_SOLI_ATEN,
                               DescUnidade = lTb65.TB89_UNIDADES != null ? lTb65.TB89_UNIDADES.SG_UNIDADE : "",
                               Qtde = lTb65.QT_ITENS_SOLI_ATEN,
                               Total = lTb65.VA_SOLI_ATEN != null ? lTb65.VA_SOLI_ATEN * lTb65.QT_ITENS_SOLI_ATEN : 0,
                               Inclu = false,
                               Checked = true,
                               quitar = false
                           };



                if (tb65 != null)
                {
                    grdSolicitacoes.DataKeyNames = new string[] { "Codigo" };
                    grdSolicitacoes.DataSource = tb65;
                    grdSolicitacoes.DataBind();
                }

                //chkIsento.Checked = tb64.CO_ISEN_TAXA == "S";
                txtObservacao.Text = tb64.DE_OBS_SOLI;
                txtPrevisao.Text = tb64.DT_PREV_ENTR.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = tb64.DT_SOLI_ATEN.ToString("dd/MM/yyyy");
                txtAtendente.Text = tb64.TB03_COLABOR.NO_COL;

                //chkIsento.Enabled = chkConsolValorTitul.Enabled = chkAtualiFinan.Enabled = false;

                if (tb64.CO_FLA_SMS_SOLIC_ATEND == "N" || tb64.CO_FLA_SMS_SOLIC_ATEND == null)
                    chkSMS.Checked = false;
                else
                    chkSMS.Checked = true;
            }

        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB64_SOLIC_ATEND</returns>
        private TB64_SOLIC_ATEND RetornaEntidade()
        {
            TB64_SOLIC_ATEND tb64 = TB64_SOLIC_ATEND.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb64 == null) ? new TB64_SOLIC_ATEND() : tb64;
        }

        /// <summary>
        /// Método que habilita ou desabilita campos da tela do formulário
        /// </summary>
        /// <param name="habilita">Boolean habilita</param>
        private void HabilitaCampos(bool habilita)
        {
            foreach (WebControl control in ulDados.Controls.OfType<TextBox>())
                control.Enabled = habilita;
            foreach (WebControl control in ulDados.Controls.OfType<RadioButtonList>())
                control.Enabled = habilita;
            foreach (WebControl control in ulDados.Controls.OfType<CheckBoxList>())
                control.Enabled = habilita;
            foreach (WebControl control in ulDados.Controls.OfType<DropDownList>())
                control.Enabled = habilita;
            foreach (WebControl control in ulDados.Controls.OfType<CheckBox>())
                control.Enabled = habilita;
        }

        /// <summary>
        /// Método que atualiza o resultado do txtValorTotal de acordo com os itens selecionados
        /// </summary>
        private void CarregaTotal()
        {
            var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            if (tb149.TP_CTRLE_SECRE_ESCOL == "I")
            {
                if (tb149.FL_APRE_VALOR_SERV_SECRE == "S")
                {
                    TB64_SOLIC_ATEND tb64 = RetornaEntidade();

                    if (tb64 == null || tb64.VL_TOTA_TAXA == null)
                    {
                    }
                    //else
                        //txtValorTotal.Text = tb64.VL_TOTA_TAXA.Value.ToString();
                }
            }
            else
            {
                TB83_PARAMETRO tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                if (tb83.FL_APRE_VALOR_SERV_SECRE == "S")
                {
                    TB64_SOLIC_ATEND tb64 = RetornaEntidade();

                    if (tb64 == null || tb64.VL_TOTA_TAXA == null)
                    {
                    }
                    //else
                      //  txtValorTotal.Text = tb64.VL_TOTA_TAXA.Value.ToString();
                }
            }
            //Mostrar / ocultar valores
            if (tb149 != null && (tb149.FL_APRE_VALOR_SERV_SECRE == null || tb149.FL_APRE_VALOR_SERV_SECRE == "N"))
            {
                grdSolicitacoes.Columns[2].Visible = false;
                grdSolicitacoes.Columns[5].Visible = false;
                //txtValorTotal.Visible = false;
                //chkAtualiFinan.Enabled = false;
                //chkIsento.Enabled = false;
                //chkConsolValorTitul.Enabled = false;
            }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Histórico
        /// </summary>
        private void CarregaHistoricos()
        {
            //ddlHistorico.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
            //                           where tb39.FLA_TIPO_HISTORICO == "C"
            //                           select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            //ddlHistorico.DataTextField = "DE_HISTORICO";
            //ddlHistorico.DataValueField = "CO_HISTORICO";
            //ddlHistorico.DataBind();

            //ddlHistorico.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {

        }

        /// <summary>
        /// Método que habilita campos da gride
        /// </summary>
        protected void HabilitaCamposGride()
        {
            int retornaInt = 0;
            decimal dcmValor = 0;
            decimal valorTotal = 0;

            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                //------------> Faz a verificação dos itens marcados na Grid de Itens Emprestados
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Enabled = true;
                    ((CheckBox)linha.Cells[6].Controls[0]).Enabled = true;
                    if (((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text == "")
                    {
                        ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text = "1";
                        //qtdTotalItem = qtdTotalItem + 1;
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                            valorTotal = valorTotal + dcmValor;
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                    else
                    {
                        if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text, out retornaInt))
                        {
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                if (retornaInt > 0)
                                {
                                    //qtdTotalItem = qtdTotalItem + retornaInt;
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                    valorTotal = valorTotal + (dcmValor * retornaInt);
                                }
                                else
                                {
                                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor).ToString("#,##0.00");
                                    valorTotal = valorTotal + dcmValor;
                                }
                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                        else
                        {
                            ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                            //qtdTotalItem = qtdTotalItem + 1;
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                valorTotal = valorTotal + dcmValor;
                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                    }
                }
                else
                {
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Enabled = false;
                    ((CheckBox)linha.Cells[6].Controls[0]).Enabled = true;
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "";
                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                }
            }

            //txtValorTotal.Text = valorTotal.ToString("#,##0.00");
        }

        /// <summary>
        /// Método que calcula o valor total da linha gride
        /// </summary>
        protected void CalculaValorTotal()
        {
            int retornaInt = 0;
            decimal dcmValor = 0;
            decimal valorTotal = 0;

            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                //------------> Faz a verificação dos itens marcados na Grid de Itens Emprestados
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text, out retornaInt))
                    {
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            if (retornaInt > 0)
                            {
                                //qtdTotalItem = qtdTotalItem + retornaInt;
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                valorTotal = valorTotal + (dcmValor * retornaInt);
                            }
                            else
                            {
                                ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                                //qtdTotalItem = qtdTotalItem + 1;
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor).ToString("#,##0.00");
                                valorTotal = valorTotal + dcmValor;
                            }
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                    else
                    {
                        ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                        //qtdTotalItem = qtdTotalItem + 1;
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                            valorTotal = valorTotal + dcmValor;
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                }
            }

            //txtValorTotal.Text = valorTotal.ToString("#,##0.00");
        }

        /// <summary>
        /// Método que carrega o número da solicitação
        /// </summary>
        private void CarregaNumeroSolicitacao()
        {
            var tb64 = (from lTb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                        where lTb64.ANO_SOLI_ATEN == DateTime.Now.Year && lTb64.MES_SOLI_ATEN == DateTime.Now.Month
                        select new { lTb64.CO_SOLI_ATEN }).ToList().OrderBy(p => p.CO_SOLI_ATEN);

            int coSolic = 0;

            if (tb64.Count() > 0)
                coSolic = tb64.Last().CO_SOLI_ATEN;

            txtNumeroSolicitacao.Text = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString("00") + "." + (coSolic + 1).ToString("000000");
        }

        /// <summary>
        /// Método que carrega a Gride de Solicitações
        /// </summary>
        private void CarregaSolicitacoes()
        {
            var lstTb66 = from tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros()
                          select new
                          {
                              Codigo = tb66.CO_TIPO_SOLI,
                              Descricao = tb66.NO_TIPO_SOLI,
                              Valor = tb66.VL_UNIT_SOLI != null ? tb66.VL_UNIT_SOLI.Value : 0,
                              DescUnidade = tb66.TB89_UNIDADES != null ? tb66.TB89_UNIDADES.SG_UNIDADE : "",
                              Qtde = "",
                              Total = "",
                              Inclu = true,
                              Checked = false,
                              quitar = false
                          };

            grdSolicitacoes.DataKeyNames = new string[] { "Codigo" };
            grdSolicitacoes.DataSource = lstTb66;
            grdSolicitacoes.DataBind();

            var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        /// <param name="ddl">DropDown da unidade</param>
        private void CarregaUnidades(DropDownList ddl)
        {
            ddl.DataSource = (from e in TB25_EMPRESA.RetornaTodosRegistros()
                              where e.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              select new { e.NO_FANTAS_EMP, e.CO_EMP });

            ddl.DataTextField = "NO_FANTAS_EMP";
            ddl.DataValueField = "CO_EMP";
            ddl.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                            where tb01.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (turma != 0)
            {
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                                       && (serie != 0 ? tb08.CO_CUR == serie : serie == 0) && tb08.TB25_EMPRESA.CO_EMP == coEmp
                                       && (turma != 0 ? tb08.CO_TUR == turma : turma == 0)
                                       select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy(m => m.NO_ALU);

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
            }
            else
                ddlAluno.Items.Clear();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega informações do boleto de solicitação de itens de secretaria da unidade logada
        /// </summary>
        private void CarregaDadosBoleto()
        {
            //txtBoletoBanca.Text = "";
            int coEmp = LoginAuxili.CO_EMP;

            var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            if (tb149.TP_CTRLE_SECRE_ESCOL == "I")
            {
                if (tb149.FL_INCLU_TAXA_CAR_SECRE == "S")
                {
                    if (tb149.CO_FLAG_GERA_BOLETO_SERV_SECR == "S")
                    {
                        var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmp);

                        if (tb83.ID_BOLETO_SERV_SECRE != null)
                        {
                            var dataSource = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                                              where tb227.ID_BOLETO == tb83.ID_BOLETO_SERV_SECRE
                                              select new
                                              {
                                                  text = "Banco: " + tb227.TB224_CONTA_CORRENTE.IDEBANCO + " - CC: " + tb227.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + " - Cedente: " + tb227.CO_CEDENTE.Trim()
                                              }).FirstOrDefault();

                            if (dataSource != null)
                            {
                                //txtBoletoBanca.Text = dataSource.text;
                            }
                            else
                            {
                                //txtBoletoBanca.Text = "NENHUM - COM REG CAR";
                            }
                        }
                        else
                        {
                            //txtBoletoBanca.Text = "NENHUM - COM REG CAR";
                        }
                    }
                    else
                    {
                        //txtBoletoBanca.Text = "NENHUM - COM REG CAR";
                    }
                }
                else
                {
                    //txtBoletoBanca.Text = "NENHUM - SEM REG CAR";
                }
            }
            else
            {
                var inforBol = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                                where tb25.CO_EMP == coEmp
                                select new
                                {
                                    tb83.FL_INCLU_TAXA_CAR_SECRE,
                                    tb25.CO_FLAG_GERA_BOLETO_SERV_SECR,
                                    tb83.ID_BOLETO_SERV_SECRE
                                }).First();

                if (inforBol.FL_INCLU_TAXA_CAR_SECRE == "S")
                {
                    if (inforBol.CO_FLAG_GERA_BOLETO_SERV_SECR == "S")
                    {
                        if (inforBol.ID_BOLETO_SERV_SECRE != null)
                        {
                            var dataSource = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                                              where tb227.ID_BOLETO == inforBol.ID_BOLETO_SERV_SECRE
                                              select new
                                              {
                                                  text = "Banco: " + tb227.TB224_CONTA_CORRENTE.IDEBANCO + " - CC: " + tb227.TB224_CONTA_CORRENTE.CO_CONTA + " - Cedente: " + tb227.CO_CEDENTE
                                              }).FirstOrDefault();

                            if (dataSource != null)
                            {
                                //txtBoletoBanca.Text = dataSource.text;
                            }
                            else
                            {
                               // txtBoletoBanca.Text = "NENHUM - COM REG CAR";
                            }
                        }
                        else
                        {
                           // txtBoletoBanca.Text = "NENHUM - COM REG CAR";
                        }
                    }
                    else
                    {
                      //  txtBoletoBanca.Text = "NENHUM - COM REG CAR";
                    }
                }
                else
                {
                   // txtBoletoBanca.Text = "NENHUM - SEM REG CAR";
                }
            }
        }

        /// <summary>
        /// Método para geração do boleto
        /// </summary>
        protected void GeraBoleto()
        {
            int qtdTit = 1;
            string strInstruBoleto = "";

            List<TitulosSolicitacao> lst = (Session["ListaDoc"] as List<TitulosSolicitacao>);
            if (lst != null)
            {
                //--------> Instancia um novo conjunto de dados de boleto na sessão
                Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

                foreach (TitulosSolicitacao item in lst)
                {
                    TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, item.NuDoc, item.NuPar);

                    //--------> Recupera dados do Responsável do Aluno
                    var s = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                             where tb07.CO_ALU == tb47.CO_ALU
                             join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                             join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                             select new
                             {
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

                    //------------> Se o título for gerado para um aluno:
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

                    //------------> Obtém a unidade
                    TB25_EMPRESA unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                    //------------> Informações do Boleto
                    boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                    boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;
                    boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                    boleto.Valor = tb47.VR_PAR_DOC; //valor da parcela do documento
                    boleto.Vencimento = tb47.DT_VEN_DOC;

                    //------------> Informações do Cedente
                    boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                    boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                        tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                    boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                    boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                    boleto.CpfCnpjCedente = unidade.CO_CPFCGC_EMP;
                    boleto.NomeCedente = unidade.NO_RAZSOC_EMP;

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                    {
                        var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                        strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                    }

                    //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                        strInstruBoleto = tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                        strInstruBoleto = strInstruBoleto + tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                    if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                        strInstruBoleto = strInstruBoleto + tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                    boleto.Instrucoes = strInstruBoleto;

                    //------------> Chave do Título do Contas a Receber
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

                    //------------> Faz a adição de instruções ao Boleto
                    boleto.Instrucoes += "<br>";

                    //------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                    string CnpjCPF = "";

                    //------------> Ano Refer: - Matrícula: - Nº NIRE:
                    //------------> Modalidade: - Série: - Turma: - Turno:
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
                        CnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                                        " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") +
                                        " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                                        "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                                        " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO;

                        boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                    }

                    boleto.Instrucoes += CnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";

                    //------------> Informações do Sacado
                    boleto.BairroSacado = s.BAIRRO;
                    boleto.CepSacado = s.CEP;
                    boleto.CidadeSacado = s.CIDADE;
                    boleto.CpfCnpjSacado = s.CPFCNPJ;
                    boleto.EnderecoSacado = s.ENDERECO + " " + s.NUMERO + " " + s.COMPL;
                    boleto.NomeSacado = s.NOME;
                    boleto.UfSacado = s.UF;

                    ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);
                    if ((qtdTit != lst.Count()) && (lst.Count() > 1))
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }

                    qtdTit++;
                };
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Dados para impressão do boleto não foram carregados corretamente.");
                return;
            }

            //--------> Gera e exibe os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }
        /// <summary>
        /// Verifica se foi marcado a opção quitar o título na gride de solicitações
        /// </summary>
        /// <returns></returns>
        private bool verificarQuitar()
        {
            bool quitar = false;
            ///Verifica se existe ao menos uma marcação para quitar o título
            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                if (((CheckBox)linha.Cells[6].Controls[0]).Checked)
                    quitar = true;
            }
            return quitar;
        }
        #endregion

        #region escolhas
        protected void cblSolicitacoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTotal();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlUnidadeEducacional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSolicitacoes();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDadosBoleto();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAluno.SelectedValue != "")
            {
                int coAlu = int.Parse(ddlAluno.SelectedValue);

                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                tb07.TB108_RESPONSAVELReference.Load();

                txtNire.Text = tb07.NU_NIRE.ToString("00000000").Insert(5, ".").Insert(2, ".");
                if (tb07.TB108_RESPONSAVEL != null)
                {
                    txtResponsavel.Text = tb07.TB108_RESPONSAVEL.NO_RESP;
                    txtTelefoneResp.Text = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP;
                }
                else
                    txtResponsavel.Text = txtTelefoneResp.Text = "";
            }
            else
                txtNire.Text = "";
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCamposGride();
        }

        protected void txtQtdeSolic_TextChanged(object sender, EventArgs e)
        {
            CalculaValorTotal();
        }

        protected void lnkBolCarne_Click(object sender, EventArgs e)
        {
            //GeraBoleto();
            //imgRecMatric.Src = "/Library/IMG/Gestor_IcoImpres.ico";
            //lblRecibo.Text = "RECIBO";
            //imgBolCarne.Src = "/Library/IMG/Gestor_IcoImpres.ico";
            //lblBoleto.Text = "BOLETO";
        }

        protected void lnkRecSolic_Click(object sender, EventArgs e)
        {
        }

        protected void chkIsento_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkIsento.Checked)
            //{
            //    chkAtualiFinan.Checked = chkConsolValorTitul.Checked = chkAtualiFinan.Enabled = chkConsolValorTitul.Enabled = ddlHistorico.Enabled = ddlAgrupador.Enabled = false;
            //    ddlHistorico.SelectedValue = ddlAgrupador.SelectedValue = "";
            //}
            //else
            //{
            //    chkAtualiFinan.Enabled = true;
            //}
        }

        protected void chkAtualiFinan_CheckedChanged(object sender, EventArgs e)
        {
            //chkConsolValorTitul.Enabled = chkAtualiFinan.Checked;
        }

        protected void chkConsolValorTitul_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {

                if (verificarQuitar())
                {

                    ((CheckBox)sender).Checked = false;
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possível consolidar pois existe(m) registro(s) marcado(s) para ser quitado.");
                }
            }
            if (((CheckBox)sender).Checked)
            {
                //ddlHistorico.Enabled = ddlAgrupador.Enabled = true;
            }
            else
            {
                //ddlHistorico.Enabled = ddlAgrupador.Enabled = false;
                //ddlHistorico.SelectedValue = ddlAgrupador.SelectedValue = "";
            }
        }
        #endregion

        #region Classe
        public class TitulosSolicitacao
        {
            public int NuPar { get; set; }
            public string NuDoc { get; set; }
        }
        #endregion
    }
}