using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Data;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Objects;
using System.Reflection;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento
{
    public partial class Painel : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private static Dictionary<string, string> tipoDeficiencia = AuxiliBaseApoio.chave(tipoDeficienciaAluno.ResourceManager);
        
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
                var dtIni = DateTime.Now;
                var dtFim = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                {
                    dtIni = LoginAuxili.DATA_INICIO_USU_DEMO;
                    dtFim = LoginAuxili.DATA_FINAL_USU_DEMO;
                }
                //carregaLocal();
                carregaLocalAtendiM1();
                carregaLocalAtendiM2();
                carregaLocalTriagemM1();
                carregaLocalTriagemM2();

                txtHrCancelamento.Text = dtIni.ToShortTimeString();
                IniPeri.Text = txtDtIniAgendaAval.Text = txtDtCancelamento.Text = dtIni.ToString();
                FimPeri.Text = txtDtFimAgendaAval.Text = dtFim.ToString();
                CarregaAgendamentoAvaliacoes();
                CarregaConsultasAgendadas();

                txtCPFResp.Text = "00000000000";
                txtDtNascResp.Text = "01/01/1900";
                txtNuIDResp.Text = "000000";
                txtOrgEmiss.Text = "SSP";


                //------------> Tamanho da imagem de Paciente
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                //------------> Tamanho da imagem de Paciente na Modal
                updImagePacienteMODAL.ImagemLargura = 70;
                updImagePacienteMODAL.ImagemAltura = 85;

                //upImagemAluno.MostraProcurar = false;

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUfMODAL, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFRespMODAL, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFRGPaciente, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlProfiPai, false);
                AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlProfissaoNomeMae, false);
                AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlFuncao, false);
                AuxiliCarregamentos.CarregaIndicadores(ddlIndicacao, false);
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadoraPacie, false);
                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanoPacie, ddlOperadoraPacie, false);
                AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoriaPacie, ddlPlanoPacie, false);
                AuxiliCarregamentos.CarregaDeficienciasNova(ddlDeficienciaAlu, false);

                carregaCidade(ddlCidade, ddlUF);
                carregaBairro(ddlUF, ddlCidade, ddlBairro);
                carregaCidade(ddlCidadeMODAL, ddlUfMODAL);
                carregaBairro(ddlUfMODAL, ddlCidadeMODAL, ddlBairroMODAL);
                CarregaDadosUnidLogada();
                VerificarNireAutomatico();
                //carregaGridMedicosPlantonistas();

                CarregaOperadoras();
                CarregaPlanos();
                CarregaCategorias();
                CarregaBotoes();
            }

        }

        private void CarregaBotoes()
        {
            var empresa = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            LiAgenda.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_AGEND == "S";
            //LiEncaixe.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_ENCAI == "S";
            LiMovimentacao.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_MOVIM == "S";
            LiGuia.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_GUIA == "S";
            LiFichaAten.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_FICHA == "S";
            //LiRecSimples.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_ATEND == "S";
            //LiRecContrato.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_CONTR == "S";
            //LiRecCaixa.Visible = empresa.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_CAIXA == "S";
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
        }

        #endregion

        #region Métodos

        #region Métodos da Aba de Registro de Solicitações

        /// <summary>
        /// Executa os processos cabíveis na recepção
        /// </summary>
        private void FinalizarRecepcao()
        {
            if (string.IsNullOrEmpty(hidCoAlu.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o paciente!");
                return;
            }

            if (string.IsNullOrEmpty(hidCoResp.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o responsável!");
                return;
            }

            int coEmpColLogado = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

            TBS367_RECEP_SOLIC tbs367 = new TBS367_RECEP_SOLIC();

            //Os dados da Recepção na tbs367
            #region Salva na tbs367

            //#region Trata sequencial
            ////Trata para gerar um Código do Encaminhamento
            //var res2 = (from tbs367pesq in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
            //            select new { tbs367pesq.NU_REGIS_RECEP_SOLIC }).OrderByDescending(w => w.NU_REGIS_RECEP_SOLIC).FirstOrDefault();

            //string seq;
            //int seq2;
            //int seqConcat;
            //string seqcon;
            //string ano = DateTime.Now.Year.ToString().Substring(2, 2);
            //string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

            //if (res2 == null)
            //    seq2 = 1;
            //else
            //{
            //    seq = res2.NU_REGIS_RECEP_SOLIC.Substring(6, 6);
            //    seq2 = int.Parse(seq);
            //}

            //seqConcat = seq2 + 1;
            //seqcon = seqConcat.ToString().PadLeft(6, '0');

            //string CodigoRecepcao = string.Format("RAP{0}{1}{2}", ano, mes, seqcon);
            //#endregion

            //#region Dados do Usuário Logado

            //#endregion

            //#region Dados do Colaborador selecionado para análise

            //int? coEmpColAnalise = (!string.IsNullOrEmpty(hidCoCol.Value) ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(hidCoCol.Value)).CO_EMP : (int?)null);
            //int coAlu = int.Parse(hidCoAlu.Value);
            //int coResp = int.Parse(hidCoResp.Value);

            //#endregion


            ////======================> Dados Gerais
            //tbs367.TBS372_AGEND_AVALI = (!string.IsNullOrEmpty(hidIdAgendaAvalicao.Value) ? TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaAvalicao.Value)) : null);
            //tbs367.NU_REGIS_RECEP_SOLIC = CodigoRecepcao;
            //tbs367.CO_EMP = LoginAuxili.CO_EMP;
            //tbs367.CO_ALU = coAlu;
            //tbs367.CO_RESP = coResp;

            ////======================> Dados do Cadastro
            //tbs367.DT_CADAS = DateTime.Now;
            //tbs367.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            //tbs367.CO_COL_CADAS = LoginAuxili.CO_COL;
            //tbs367.CO_EMP_COL_CADAS = coEmpColLogado;
            //tbs367.IP_CADAS = Request.UserHostAddress;

            ////======================> Dados do Colaborador que irá analisar previamente
            //tbs367.CO_COL_ANALI = (!string.IsNullOrEmpty(hidCoCol.Value) ? int.Parse(hidCoCol.Value) : (int?)null);
            //tbs367.CO_EMP_COL_ANALI = (!coEmpColAnalise.HasValue ? coEmpColAnalise.Value : (int?)null);

            ////======================> Dados da Situação
            //tbs367.CO_SITUA = "A";
            //tbs367.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            //tbs367.CO_COL_SITUA = LoginAuxili.CO_COL;
            //tbs367.CO_EMP_COL_SITUA = coEmpColLogado;
            //tbs367.IP_SITUA = Request.UserHostAddress;
            //tbs367.DT_SITUA = DateTime.Now;

            //TBS367_RECEP_SOLIC.SaveOrUpdate(tbs367);

            #endregion

            #region Atualiza o Agendamento de Avaliação

            //======================> Atualiza Dados da Situação do agendamento de avaliação
            TBS372_AGEND_AVALI tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaAvalicao.Value));
            tbs372.CO_SITUA = "E";
            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs372.CO_EMP_COL_SITUA = coEmpColLogado;
            tbs372.IP_SITUA = Request.UserHostAddress;
            tbs372.DT_SITUA = DateTime.Now;

            //Atualiza dados de quem realizou o encaminhamento
            tbs372.DT_ENCAM = DateTime.Now;
            tbs372.CO_COL_ENCAM = LoginAuxili.CO_COL;
            tbs372.CO_EMP_COL_ENCAM = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs372.CO_EMP_ENCAM = LoginAuxili.CO_EMP;
            tbs372.IP_ENCAM = Request.UserHostAddress;

            TBS372_AGEND_AVALI.SaveOrUpdate(tbs372, true);

            //Grava um log do Encaminhamento da Avaliação
            #region Grava o log

            TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs380 = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();

            tbs380.TBS372_AGEND_AVALI = tbs372;
            tbs380.FL_TIPO_LOG = "E";
            tbs380.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs380.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs380.DT_CADAS = DateTime.Now;
            tbs380.IP_CADAS = Request.UserHostAddress;

            TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs380, true);

            #endregion

            #endregion

            //Percorre a grid de solicitações e persiste as informações
            foreach (GridViewRow i in grdSolicitacoes.Rows)
            {
                #region Coleta os Dados
                string ddlOper, ddlPlan, ddlCateg, ddlProc, txtQtde, txtNumero, txtNuGuia,
                    txtNuAutor, txtVlTotal, txtVlDscto, vlUnit;

                ddlOper = (((HiddenField)i.Cells[0].FindControl("hidIdOper")).Value);
                ddlPlan = (((HiddenField)i.Cells[0].FindControl("hidIdPlano")).Value);
                ddlCateg = (((HiddenField)i.Cells[0].FindControl("hidIdCateg")).Value);
                txtNumero = (((HiddenField)i.Cells[0].FindControl("hidNuPlano")).Value);
                txtNuGuia = (((HiddenField)i.Cells[0].FindControl("hidNuGuia")).Value);
                txtNuAutor = (((HiddenField)i.Cells[0].FindControl("hidNuAutor")).Value);


                ddlProc = ((DropDownList)i.Cells[3].FindControl("ddlProcedimento")).SelectedValue;
                vlUnit = ((HiddenField)i.Cells[3].FindControl("hidValUnitProc")).Value;
                txtQtde = ((TextBox)i.Cells[4].FindControl("txtQtde")).Text;
                txtVlTotal = ((TextBox)i.Cells[5].FindControl("txtVlUnitario")).Text;
                txtVlDscto = ((TextBox)i.Cells[6].FindControl("txtVlDesconto")).Text;

                #region Validações

                #endregion

                //Realiza as persistências apenas se houverem sido informados o procedimento e a quantidade
                if ((!string.IsNullOrEmpty(ddlProc) && (!string.IsNullOrEmpty(txtQtde))))
                {
                    decimal ValorLiquido = 0;
                    //Calcula valor líquido multiplicando a quantidade de sessões pelo valor unitário e subtraindo o desconto
                    if ((!string.IsNullOrEmpty(txtQtde)) && (!string.IsNullOrEmpty(txtVlTotal)))
                        ValorLiquido = ((Convert.ToInt32(txtQtde) * Convert.ToDecimal(txtVlTotal)) - (!string.IsNullOrEmpty(txtVlDscto) ? Convert.ToDecimal(txtVlDscto) : 0));

                #endregion

                    //Salva objeto da entidade tbs368 que armazena os itens solicitados em uma recepção
                    #region Salva entidade tbs368

                    TBS368_RECEP_SOLIC_ITENS tbs368 = new TBS368_RECEP_SOLIC_ITENS();

                    //======================> Dados Gerais
                    tbs368.TBS367_RECEP_SOLIC = tbs367;
                    tbs368.NU_REGIS_RECEP_SOLIC = tbs367.NU_REGIS_RECEP_SOLIC;

                    //======================> Dados do Plano de Saúde (Quando houver)
                    tbs368.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper)) : null);
                    tbs368.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan)) : null);
                    tbs368.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCateg) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCateg)) : null);
                    tbs368.NU_PLAN_SAUDE = (!string.IsNullOrEmpty(txtNumero) ? int.Parse(txtNumero) : (int?)null);
                    tbs368.NU_GUIA = (!string.IsNullOrEmpty(txtNuGuia) ? txtNuGuia : null);
                    tbs368.NU_AUTOR = (!string.IsNullOrEmpty(txtNuAutor) ? txtNuAutor : null);
                    tbs368.NU_QTDE_SESSO = (!string.IsNullOrEmpty(txtQtde) ? int.Parse(txtQtde) : (int?)null);

                    //======================> Dados do procedimento e valores
                    tbs368.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc));
                    tbs368.VL_UNIT = (!string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null);
                    tbs368.VL_DSCTO = (!string.IsNullOrEmpty(txtVlDscto) ? decimal.Parse(txtVlDscto) : (decimal?)null);
                    tbs368.VL_LIQUI = ValorLiquido;

                    //======================> Dados do Cadastro
                    tbs368.DT_CADAS = DateTime.Now;
                    tbs368.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs368.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs368.CO_EMP_COL_CADAS = coEmpColLogado;
                    tbs368.IP_CADAS = Request.UserHostAddress;

                    //======================> Dados da Situação
                    tbs368.CO_SITUA = "A";
                    tbs368.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs368.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs368.CO_EMP_COL_SITUA = coEmpColLogado;
                    tbs368.IP_SITUA = Request.UserHostAddress;
                    tbs368.DT_SITUA = DateTime.Now;

                    TBS368_RECEP_SOLIC_ITENS.SaveOrUpdate(tbs368);

                    #endregion

                    //Salva objeto da entidade tbs369 que armazena os itens solicitados em em regulação na recepção
                    #region Salva Entidade tbs369

                    TBS369_RECEP_REGUL_ITENS tbs369 = new TBS369_RECEP_REGUL_ITENS();

                    //======================> Dados Gerais
                    tbs369.TBS368_RECEP_SOLIC_ITENS = tbs368;
                    tbs369.TBS367_RECEP_SOLIC = tbs367;
                    tbs369.NU_REGIS_RECEP_SOLIC = tbs367.NU_REGIS_RECEP_SOLIC;

                    //======================> Dados do Procedimento e Valores
                    tbs369.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc));
                    tbs369.VL_UNIT = (!string.IsNullOrEmpty(txtVlTotal) ? decimal.Parse(txtVlTotal) : (decimal?)null);
                    tbs369.VL_DSCTO = (!string.IsNullOrEmpty(txtVlDscto) ? decimal.Parse(txtVlDscto) : (decimal?)null);
                    tbs369.VL_LIQUI = ValorLiquido;

                    tbs369.NU_QTDE_SESSO_GUIA = (!string.IsNullOrEmpty(txtQtde) ? int.Parse(txtQtde) : (int?)null);

                    //======================> Dados do Cadastro
                    tbs369.DT_CADAS = DateTime.Now;
                    tbs369.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs369.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs369.CO_EMP_COL_CADAS = coEmpColLogado;
                    tbs369.IP_CADAS = Request.UserHostAddress;

                    //======================> Dados da Situação
                    tbs369.CO_SITUA = "A";
                    tbs369.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs369.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs369.CO_EMP_COL_SITUA = coEmpColLogado;
                    tbs369.IP_SITUA = Request.UserHostAddress;
                    tbs369.DT_SITUA = DateTime.Now;

                    TBS369_RECEP_REGUL_ITENS.SaveOrUpdate(tbs369, true);

                    #endregion
                }
            }

            #region Já salva Registro na Avaliação correspondente

            #region Trata sequencial
            //Trata para gerar um Código do Encaminhamento
            var res2av = (from tbs381pesq in TBS381_AVALI_RECEP.RetornaTodosRegistros()
                          select new { tbs381pesq.NU_REGIS_AVALI }).OrderByDescending(w => w.NU_REGIS_AVALI).FirstOrDefault();

            string seqav;
            int seq2av;
            int seqConcatav;
            string seqconav;
            string anoav = DateTime.Now.Year.ToString().Substring(2, 2);
            string mesav = DateTime.Now.Month.ToString().PadLeft(2, '0');

            if (res2av == null)
                seq2av = 1;
            else
            {
                seqav = res2av.NU_REGIS_AVALI.Substring(6, 6);
                seq2av = int.Parse(seqav);
            }

            seqConcatav = seq2av + 1;
            seqconav = seqConcatav.ToString().PadLeft(6, '0');

            string CodigoAvaliacao = string.Format("AV{0}{1}{2}", anoav, mesav, seqconav);
            #endregion

            TBS381_AVALI_RECEP tbs381 = new TBS381_AVALI_RECEP();
            tbs381.NU_REGIS_RECEP_SOLIC = tbs367.NU_REGIS_RECEP_SOLIC;
            tbs381.TBS367_RECEP_SOLIC = tbs367;
            tbs381.NU_REGIS_AVALI = CodigoAvaliacao;

            //Dados do cadastro
            tbs381.DT_CADAS = DateTime.Now;
            tbs381.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs381.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs381.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs381.CO_COL_ANALI = LoginAuxili.CO_COL;
            tbs381.CO_EMP_COL_ANALI = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs381.IP_CADAS = Request.UserHostAddress;

            //Dados da situação
            tbs381.DT_SITUA = DateTime.Now;
            tbs381.CO_SITUA = "P"; //pré-avaliação
            tbs381.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs381.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs381.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs381.IP_SITUA = Request.UserHostAddress;

            TBS381_AVALI_RECEP.SaveOrUpdate(tbs381, true);

            #endregion

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro de Recepção realizado com Sucesso!!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        /// <summary>
        /// Calcula e seta o valor total de um determinado procedimento de acordo com o parâmetro
        /// </summary>
        private void CalculaValorTotalProcedimento(int qt, string vlProcUnit, TextBox txtValor)
        {
            //Identifica o resultado multiplicando as sessões pelo valor unitário
            decimal result = qt * (!string.IsNullOrEmpty(vlProcUnit) ? decimal.Parse(vlProcUnit) : 0);
            //Insere o valor calculado no campo de valor resultado
            txtValor.Text = result.ToString("N2");
        }

        /// <summary>
        /// Percorre a grid de solicitações e totaliza os valores referentes
        /// </summary>
        private void CarregarValoresTotaisFooter()
        {
            decimal VlTotal = 0;
            decimal VlDesconto = 0;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                //Coleta os valores da linha
                string Valor, Desconto;
                Valor = ((TextBox)li.Cells[5].FindControl("txtVlUnitario")).Text;
                Desconto = ((TextBox)li.Cells[6].FindControl("txtVlDesconto")).Text;

                //Soma os valores com os valores das outras linhas da grid
                VlTotal += (!string.IsNullOrEmpty(Valor) ? decimal.Parse(Valor) : 0);
                VlDesconto += (!string.IsNullOrEmpty(Desconto) ? decimal.Parse(Desconto) : 0);
            }

            decimal vlLiquido = VlTotal - VlDesconto;

            //Seta os valores nos textboxes
            txtVlBaseTotal.Text = VlTotal.ToString("N2");
            txtVlDescontoTotal.Text = VlDesconto.ToString("N2");
            txtVlLiquidoTotal.Text = vlLiquido.ToString("N2");
        }

        /// <summary>
        /// Carrega os procedimentos de acordo com operadora grupo e subgrupo;
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarProcedimentosMedicos(DropDownList ddl, DropDownList ddlGrupo, DropDownList ddlSubGrupo, string idOper)
        {
            int oper = (!string.IsNullOrEmpty(idOper) ? int.Parse(idOper) : 0);
            int idGrupo = (!string.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0);
            int idSubGrupo = (!string.IsNullOrEmpty(ddlSubGrupo.SelectedValue) ? int.Parse(ddlSubGrupo.SelectedValue) : 0);

            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                       //Se o id da operadora recebido for inválido(0), então filtrará os procedimentos apenas da instituição
                       where //(oper != 0 ? tbs356.TB250_OPERA.ID_OPER == oper : 0 == 0)
                        (idGrupo != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == idGrupo : 0 == 0)
                       && (idSubGrupo != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == idSubGrupo : 0 == 0)
                       select new
                       {
                           ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                           NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                           CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                           PROC_MEDI_CONCAT = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI
                       }).OrderBy(w => w.NM_PROC_MEDI).ToList();

            ddl.DataTextField = "PROC_MEDI_CONCAT";
            ddl.DataValueField = "ID_PROC_MEDI_PROCE";
            ddl.DataSource = res;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridSolicitacoes(int idAgendAval)
        {
            DataTable dtV = CriarColunasELinhaGridSolicitacoes();

            if (idAgendAval != 0)
            {
                var res = (from tbs373 in TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros()
                           join tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros() on tbs373.TBS372_AGEND_AVALI.ID_AGEND_AVALI equals tbs372.ID_AGEND_AVALI
                           where tbs372.ID_AGEND_AVALI == idAgendAval
                           select new
                           {
                               //ID_AGEND_AVALI = tbs372.ID_AGEND_AVALI,
                               ID_AGEND_AVALI_ITENS = tbs373.ID_AGEND_AVALI_ITENS,
                               ID_OPER = tbs372.TB250_OPERA != null ? tbs372.TB250_OPERA.ID_OPER : 0,
                               ID_PLAN = tbs372.TB251_PLANO_OPERA != null ? tbs372.TB251_PLANO_OPERA.ID_PLAN : 0,
                               ID_CATEG_PLANO_SAUDE = tbs372.TB367_CATEG_PLANO_SAUDE != null ? tbs372.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE : 0,
                               ID_PROC_MEDIC_GRUPO = tbs373.TBS356_PROC_MEDIC_PROCE != null ? tbs373.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO : 0,
                               ID_PROC_MEDIC_SGRUP = tbs373.TBS356_PROC_MEDIC_PROCE != null ? tbs373.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP : 0,
                               ID_PROC_MEDI_PROCE = tbs373.TBS356_PROC_MEDIC_PROCE != null ? tbs373.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE : 0,
                               tbs373.QT_SESSO,
                               tbs373.VL_PROC_UNIT
                           }).ToList();

                dtV.Clear();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["OPERADORA"] = i.ID_OPER;
                    linha["PLANO"] = i.ID_PLAN;
                    linha["CATEGORIA"] = i.ID_CATEG_PLANO_SAUDE;
                    linha["NUMERO"] = "";
                    linha["NUGUIA"] = "";
                    linha["NUAUTOR"] = "";
                    linha["IDITEMSOLIC"] = i.ID_AGEND_AVALI_ITENS;
                    linha["IDAGENDA"] = idAgendAval;
                    linha["GRUPO"] = i.ID_PROC_MEDIC_GRUPO;
                    linha["SUBGRUPO"] = i.ID_PROC_MEDIC_SGRUP;
                    linha["PROCEDIMENTO"] = i.ID_PROC_MEDI_PROCE;
                    linha["QTDE"] = i.QT_SESSO;
                    linha["VLUNITARIO"] = i.VL_PROC_UNIT;
                    linha["VLDESCONTO"] = "";
                    dtV.Rows.Add(linha);
                }
            }

            HttpContext.Current.Session.Add("GridSolic_EU", dtV);

            carregaGridNovaComContexto(idAgendAval);
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridSolicitacoes(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridSolicitacoes();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_EU"] = dtV;

            carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridSolicitacoes()
        {
            DataTable dtV = CriarColunasELinhaGridSolicitacoes();
            var linha = dtV.NewRow();
            linha["OPERADORA"] = "";
            linha["PLANO"] = "";
            linha["CATEGORIA"] = "";
            linha["NUMERO"] = "";
            linha["NUGUIA"] = "";
            linha["NUAUTOR"] = "";
            linha["IDITEMSOLIC"] = "";
            linha["IDAGENDA"] = "";
            linha["GRUPO"] = "";
            linha["SUBGRUPO"] = "";
            linha["PROCEDIMENTO"] = "";
            linha["QTDE"] = "";
            linha["VLUNITARIO"] = "";
            linha["VLDESCONTO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_EU"] = dtV;

            carregaGridNovaComContexto();
        }

        private DataTable CriarColunasELinhaGridSolicitacoes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;
            //dtV.Clear();

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPERADORA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CATEGORIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUMERO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUGUIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUAUTOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDITEMSOLIC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDAGENDA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "GRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SUBGRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLUNITARIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLDESCONTO";
            dtV.Columns.Add(dcATM);




            DataRow linha;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                linha = dtV.NewRow();
                linha["OPERADORA"] = (((HiddenField)li.Cells[0].FindControl("hidIdOper")).Value);
                linha["PLANO"] = (((HiddenField)li.Cells[0].FindControl("hidIdPlano")).Value);
                linha["CATEGORIA"] = (((HiddenField)li.Cells[0].FindControl("hidIdCateg")).Value);
                linha["NUMERO"] = (((HiddenField)li.Cells[0].FindControl("hidNuPlano")).Value);
                linha["NUGUIA"] = (((HiddenField)li.Cells[0].FindControl("hidNuGuia")).Value);
                linha["NUAUTOR"] = (((HiddenField)li.Cells[0].FindControl("hidNuAutor")).Value);
                linha["IDITEMSOLIC"] = (((HiddenField)li.Cells[0].FindControl("hidItemSolic")).Value);
                linha["IDAGENDA"] = (((HiddenField)li.Cells[0].FindControl("hidAgendaSolic")).Value);

                linha["GRUPO"] = ((DropDownList)li.Cells[1].FindControl("ddlGrupoProc")).SelectedValue;
                linha["SUBGRUPO"] = ((DropDownList)li.Cells[2].FindControl("ddlSubGrupo")).SelectedValue;
                linha["PROCEDIMENTO"] = ((DropDownList)li.Cells[3].FindControl("ddlProcedimento")).SelectedValue;
                linha["QTDE"] = ((TextBox)li.Cells[4].FindControl("txtQtde")).Text;
                linha["VLUNITARIO"] = ((TextBox)li.Cells[5].FindControl("txtVlUnitario")).Text;
                linha["VLDESCONTO"] = ((TextBox)li.Cells[6].FindControl("txtVlDesconto")).Text;

                dtV.Rows.Add(linha);
            }
            return dtV;
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContexto(int idAgendAval = 0)
        {
            try
            {
                DataTable dtV = new DataTable();
                dtV = (DataTable)Session["GridSolic_EU"];
                grdSolicitacoes.DataSource = dtV;
                grdSolicitacoes.DataBind();

                int aux = 0;
                foreach (GridViewRow li in grdSolicitacoes.Rows)
                {
                    DropDownList ddlProc, ddlGrupo, ddlSubGrupo;
                    HiddenField ddlOper, ddlPlan, ddlCateg, txtNumeroPlano, txtNuGuia, txtNuAutor, txtIdItemSolic, txtIdAgenda;
                    TextBox txtQtde, txtVlUnitario, txtVlDesconto;
                    ddlOper = (((HiddenField)li.Cells[0].FindControl("hidIdOper")));
                    ddlPlan = (((HiddenField)li.Cells[0].FindControl("hidIdPlano")));
                    ddlCateg = (((HiddenField)li.Cells[0].FindControl("hidIdCateg")));
                    txtNumeroPlano = (((HiddenField)li.Cells[0].FindControl("hidNuPlano")));
                    txtNuGuia = (((HiddenField)li.Cells[0].FindControl("hidNuGuia")));
                    txtNuAutor = (((HiddenField)li.Cells[0].FindControl("hidNuAutor")));
                    txtIdItemSolic = ((HiddenField)li.Cells[0].FindControl("hidItemSolic"));
                    txtIdAgenda = ((HiddenField)li.Cells[0].FindControl("hidAgendaSolic"));

                    ddlGrupo = (DropDownList)li.Cells[1].FindControl("ddlGrupoProc");
                    ddlSubGrupo = (DropDownList)li.Cells[2].FindControl("ddlSubGrupo");
                    ddlProc = (DropDownList)li.Cells[3].FindControl("ddlProcedimento");
                    txtQtde = (TextBox)li.Cells[4].FindControl("txtQtde");
                    txtVlUnitario = (TextBox)li.Cells[5].FindControl("txtVlUnitario");
                    txtVlDesconto = (TextBox)li.Cells[6].FindControl("txtVlDesconto");

                    string Grupo, SubGrupo, Operadora, Plano, Categoria, nuPlano, nuGuia, nuAutor, nuQtde, Procedimento, vlUnit, vlDscto, idItemSolic, idAgenda;

                    //Coleta os valores do dtv da modal popup
                    Operadora = dtV.Rows[aux]["OPERADORA"].ToString();
                    Plano = dtV.Rows[aux]["PLANO"].ToString();
                    Categoria = dtV.Rows[aux]["CATEGORIA"].ToString();
                    nuPlano = dtV.Rows[aux]["NUMERO"].ToString();
                    nuGuia = dtV.Rows[aux]["NUGUIA"].ToString();
                    nuAutor = dtV.Rows[aux]["NUAUTOR"].ToString();
                    idItemSolic = dtV.Rows[aux]["IDITEMSOLIC"].ToString();
                    idAgenda = idAgendAval.ToString();

                    //Coleta os valores do dtv da grid
                    Grupo = dtV.Rows[aux]["GRUPO"].ToString();
                    SubGrupo = dtV.Rows[aux]["SUBGRUPO"].ToString();
                    Procedimento = dtV.Rows[aux]["PROCEDIMENTO"].ToString();
                    nuQtde = dtV.Rows[aux]["QTDE"].ToString();
                    vlUnit = dtV.Rows[aux]["VLUNITARIO"].ToString();
                    vlDscto = dtV.Rows[aux]["VLDESCONTO"].ToString();

                    //Seta os valores nos campos da modal popup
                    ddlOper.Value = Operadora;
                    ddlPlan.Value = Plano;
                    ddlCateg.Value = Categoria;
                    txtNumeroPlano.Value = nuPlano;
                    txtNuGuia.Value = nuGuia;
                    txtNuAutor.Value = nuAutor;
                    txtIdItemSolic.Value = idItemSolic;
                    txtIdAgenda.Value = idAgenda;

                    //Seta os valores na grid
                    txtQtde.Text = nuQtde;
                    txtVlUnitario.Text = vlUnit;
                    txtVlDesconto.Text = vlDscto;

                    CarregarGrupos(ddlGrupo); // carrega grupo e seta valor
                    ddlGrupo.SelectedValue = Grupo;

                    CarregaSubGrupos(ddlSubGrupo, ddlGrupo); // carrega subgrupo e seta valor
                    ddlSubGrupo.SelectedValue = SubGrupo;

                    CarregarProcedimentosMedicos(ddlProc, ddlGrupo, ddlSubGrupo, ddlOper.Value); // carrega procedimentos médicos e seta valor
                    ddlProc.SelectedValue = Procedimento;
                    aux++;
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
        }

        /// <summary>
        /// Carrega os grupos de procedimentos em ddl recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarGrupos(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddl, true);
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        private void CarregaSubGrupos(DropDownList ddlSubGrupo, DropDownList ddlGrupoProc)
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, ddlGrupoProc, true);
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, string idOperStr, string idPlanStr, HiddenField hidValorUnitario)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(idOperStr) ? int.Parse(idOperStr) : 0);
            int idPlan = (!string.IsNullOrEmpty(idPlanStr) ? int.Parse(idPlanStr) : 0);

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (!string.IsNullOrEmpty(idOperStr))
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), idOper, idPlan);
                hidValorUnitario.Value = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }

        /// <summary>
        /// Abre a modal com informações de plano de saúde
        /// </summary>
        private void AbreModalInfosPlanos()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfoPlano();",
                true
            );
        }

        /// <summary>
        /// Abre a modal com informações de plano de saúde
        /// </summary>
        private void AbreModalCancelamentoAgenda()
        {
            txtDtCancelamento.Text = DateTime.Now.ToShortDateString();
            txtHrCancelamento.Text = DateTime.Now.ToShortTimeString();
            rdblTiposCancelamento.ClearSelection();
            txtObserCancelamento.Text = "";

            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalCancelamentoAgenda();",
                true
            );
        }

        /// <summary>
        /// Abre a modal com informações de plano de saúde
        /// </summary>
        private void AbreModalInfosCadastrais()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosCadastrais();",
                true
            );
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

        #endregion

        /// <summary>
        /// Carrega os logs do agendamento recebido como parâmetro
        /// </summary>
        private void CarregaGridLog(int ID_AGEND_HORAR)
        {
            var res = (from tbs375 in TBS375_LOG_ALTER_STATUS_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs375.CO_COL_CADAS equals tb03.CO_COL
                       where tbs375.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                       select new saidaLog
                       {
                           //idTeste = tbs375.ID_LOG_ALTER_STATUS_AGEND_HORAR,
                           Data = tbs375.DT_CADAS,
                           NO_PROFI = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                           FL_TIPO = tbs375.FL_TIPO_LOG,
                           FL_CONFIR_AGEND = tbs375.FL_CONFIR_AGEND,
                           CO_SITUA_AGEND = tbs375.CO_SITUA_AGEND_HORAR,
                           FL_AGEND_ENCAM = tbs375.FL_AGEND_ENCAM,
                           FL_FALTA_JUSTIF = tbs375.FL_JUSTI,
                           OBS = tbs375.DE_OBSER,
                       }).ToList();


            var histoAgend = (from tbs430 in TBS430_HISTO_AGEND_HORAR.RetornaTodosRegistros()
                              join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs430.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL_SITUA equals tb03.CO_COL
                              where tbs430.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                              select new
                              {
                                  histoNome = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                                  histoData = tbs430.DT_AGEND_HORAR,
                              }).ToList();

            saidaLog ha = new saidaLog();
            foreach (var t in histoAgend)
            {
                ha.Data = t.histoData;
                ha.NO_PROFI = t.histoNome;
                ha.FL_TIPO = "A";

                res.Add(ha);
            }
            //Coleta os dados de cadastro e inclui no log
            #region Coleta dados de Cadastro

            //Coleta os dados
            var dados = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                         join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL_SITUA equals tb03.CO_COL
                         where tbs174.ID_AGEND_HORAR == ID_AGEND_HORAR
                         select new
                         {
                             NO = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                             DT = tbs174.DT_SITUA_AGEND_HORAR,
                         }).FirstOrDefault();

            //Insere em novo objeto do tipo saidaLog
            saidaLog i = new saidaLog();
            i.Data = dados.DT;
            i.NO_PROFI = dados.NO;
            i.FL_TIPO = "A";

            res.Add(i); //Adiciona o novo item na lista
            res = res.OrderBy(w => w.Data).ThenBy(w => w.NO_PROFI).ToList(); //Ordena de acordo com a data e nome;

            #endregion

            #region Coleta dados de Efetivação

            //Coleta os dados
            var resAtend = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                            where tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                            select new
                            {
                                NO = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                                DT = tbs390.DT_REALI,
                            }).FirstOrDefault();

            if (resAtend != null)
            {
                //Insere em novo objeto do tipo saidaLog
                saidaLog at = new saidaLog();
                at.Data = dados.DT;
                at.NO_PROFI = dados.NO;
                at.FL_TIPO = "R";

                res.Add(at); //Adiciona o novo item na lista
                res = res.OrderBy(w => w.Data).ThenBy(w => w.NO_PROFI).ToList(); //Ordena de acordo com a data e nome;
            }

            #endregion

            grdLogAgendamento.DataSource = res;
            grdLogAgendamento.DataBind();
        }

        public class saidaLog
        {
            public int idTeste { get; set; }
            public DateTime Data { get; set; }
            public string Data_V
            {
                get
                {
                    return this.Data.ToString("dd/MM/yy") + " - " + this.Data.ToString("HH:mm");
                }
            }
            public string NO_PROFI { get; set; }
            public string NO_PROFI_V
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.NO_PROFI))
                        return (this.NO_PROFI.Length > 42 ? this.NO_PROFI.Substring(0, 42) + "..." : this.NO_PROFI);
                    else
                        return " - ";
                }
            }
            public string FL_TIPO { get; set; }
            public string NO_TIPO
            {
                get
                {
                    switch (this.FL_TIPO)
                    {
                        case "P":
                            return "Presença";
                        case "C":
                            return "Cancelamento";
                        case "T": //TA.07/06/2016
                            return "Triagem";
                        case "E":
                            return "Encaminhamento";
                        case "A":
                            return "Cadastro";
                        case "R":
                            return "Atendimento";
                        case "M":
                            return "Movimentação";
                        case "W":
                            return "Movimentação";
                        default:
                            return " - ";
                    }
                }
            }
            public string FL_CONFIR_AGEND { get; set; }
            public string CO_SITUA_AGEND { get; set; }
            public string FL_TIPO_AGENDA { get; set; }
            public string FL_TIPO_AGENDA_AVALI { get; set; }
            public string FL_AGEND_ENCAM { get; set; }
            public string FL_FALTA_JUSTIF { get; set; }
            public string DE_TIPO
            {
                get
                {
                    string s;
                    //Trata de acordo com o tipo
                    switch (this.FL_TIPO)
                    {
                        //Trata quando é PRESENÇA
                        case "P":
                            s = (this.FL_CONFIR_AGEND == "S" ? "Alterado para Presente" : "Alterado para Ausente");
                            break;
                        //Trata quando é CANCELAMENTO
                        case "C":
                            s = (this.CO_SITUA_AGEND == "C" ? (this.FL_FALTA_JUSTIF == "C" ? "Alterado para Cancelado (Clínica)" : (this.FL_FALTA_JUSTIF == "P" ? "Alterado para Cancelado (Paciente)" : (this.FL_FALTA_JUSTIF == "S" ? "Alterado para Cancelado (Justificado)" : "Alterado para Cancelado (Não Justificado)"))) : "Alterado para Agendado");
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = (this.FL_AGEND_ENCAM == "S" ? "Encaminhado para a Atendimento" : "Remoção de encaminhamento");
                            break;
                        //Trata quando é de Triagem //TA.07/06/2016
                        case "T":
                            s = (this.FL_AGEND_ENCAM == "S" ? "Encaminhado para a Triagem" : "Remoção de encaminhamento");
                            break;
                        //Esse é inserido na "Mão", se for CADASTRO, então verifica se foi cadastrado como lista de espera ou consulta
                        case "A":
                            s = "Inserção de registro de Agendamento";
                            break;
                        //Trata quando é ATENDIMENTO
                        case "R":
                            s = "Atendimento realizado";
                            break;
                        case "W":
                            s = "Movimentação de agendamento";
                            break;
                        case "M":
                            s = "Movimentação de agendamento";
                            break;
                        default:
                            s = " - ";
                            break;
                    }
                    return s;
                }
            }
            public string CAMINHO_IMAGEM
            {
                get
                {
                    string s;
                    //Trata de acordo com o tipo
                    switch (this.FL_TIPO)
                    {
                        //Trata quando é PRESENÇA
                        case "P":
                            s = (this.FL_CONFIR_AGEND == "S" ? "/Library/IMG/PGS_PacienteChegou.ico" : "/Library/IMG/PGS_PacienteNaoChegou.ico");
                            break;
                        //Trata quando é CANCELAMENTO
                        case "C":
                            if (this.CO_SITUA_AGEND == "C")
                            {
                                if (this.FL_FALTA_JUSTIF == "S")
                                    s = "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png";
                                else
                                    s = "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png";
                            }
                            else
                                s = "/Library/IMG/PGS_SF_AgendaConfirmada.png";
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                            break;
                        //Trata quando é de Triagem //TA.07/06/2016
                        case "T":
                            s = "/Library/IMG/PGS_SF_Triagem.png";
                            break;
                        //Trata quando é de CADASTRO
                        case "A":
                            s = "/Library/IMG/PGN_IconeTelaCadastro2.png";
                            break;
                        //Trata quando é ATENDIMENTO
                        case "R":
                            s = "/Library/IMG/PGS_SF_AgendaRealizada.png";
                            break;
                        //Trata quando é MOVIMENTACÃO
                        case "M":
                            s = "/Library/IMG/PGS_SF_AgendamentoCanceladoPorMovimentacao.png";
                            break;
                        case "W":
                            s = "/Library/IMG/PGS_SF_AgendamentoPorMovimentacao.png";
                            break;
                        default:
                            s = "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                            break;
                    }
                    return s;
                }
            }
            public string OBS { get; set; }
        }

        //private void carregaLocal()
        //{
        //    var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
        //               where tb14.FL_RECEP.Equals("S")
        //               select new
        //               {
        //                   tb14.NO_DEPTO,
        //                   tb14.CO_DEPTO
        //               }).OrderBy(x => x.NO_DEPTO);

        //    ddlLocal.DataTextField = "NO_DEPTO";
        //    ddlLocal.DataValueField = "CO_DEPTO";
        //    ddlLocal.DataSource = res;
        //    ddlLocal.DataBind();

        //    ddlLocalTriagemM2.Items.Insert(0, new ListItem("Selecione", ""));
        //    //ddlLocal.Items.Insert(0, new ListItem("Todos", ""));
        //}

        private void carregaLocalAtendiM1()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_CONSU.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocalAtendimentoM1.DataTextField = "NO_DEPTO";
            ddlLocalAtendimentoM1.DataValueField = "CO_DEPTO";
            ddlLocalAtendimentoM1.DataSource = res;
            ddlLocalAtendimentoM1.DataBind();

            ddlLocalAtendimentoM1.Items.Insert(0, new ListItem("Selecione o local para atendimento", ""));
        }

        private void carregaLocalAtendiM3()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_CONSU.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocalAtendimentoM3.DataTextField = "NO_DEPTO";
            ddlLocalAtendimentoM3.DataValueField = "CO_DEPTO";
            ddlLocalAtendimentoM3.DataSource = res;
            ddlLocalAtendimentoM3.DataBind();

            ddlLocalAtendimentoM3.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregaLocalAtendiM2()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_CONSU.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocalAtendimentoM2.DataTextField = "NO_DEPTO";
            ddlLocalAtendimentoM2.DataValueField = "CO_DEPTO";
            ddlLocalAtendimentoM2.DataSource = res;
            ddlLocalAtendimentoM2.DataBind();

            ddlLocalAtendimentoM2.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void carregaLocalTriagemM1()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_AVALI_RISCO.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocalTriagemM1.DataTextField = "NO_DEPTO";
            ddlLocalTriagemM1.DataValueField = "CO_DEPTO";
            ddlLocalTriagemM1.DataSource = res;
            ddlLocalTriagemM1.DataBind();

            ddlLocalTriagemM1.Items.Insert(0, new ListItem("Selecione o local para avaliação", ""));
        }

        private void carregaLocalTriagemM2()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_AVALI_RISCO.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocalTriagemM2.DataTextField = "NO_DEPTO";
            ddlLocalTriagemM2.DataValueField = "CO_DEPTO";
            ddlLocalTriagemM2.DataSource = res;
            ddlLocalTriagemM2.DataBind();

            ddlLocalTriagemM2.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePorfPesq.Visible =
            imgbPesqProfNome.Visible = !ocultar;
            ddlNomeProf.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }
        /// <summary>
        /// Carrega a Grid de Registros de Consultas em aberto para o dia
        /// </summary>
        private void CarregaConsultasAgendadas()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);
            var nomePac = txtNomePacPesqAgendAtend.Text.Trim().ToUpper();
            var nomeProf = txtNomeProfPesqAtend.Text.Trim();
            //var nomeOperadora = txtNomeContratoPesqAtend.Text.Trim();
            //int local = ddlLocal.SelectedValue.Equals("") ? -1 : int.Parse(ddlLocal.SelectedValue);
            int local = -1;
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       //join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs174.ID_OPER equals tb250.ID_OPER
                       //join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                       where (tbs174.CO_EMP == LoginAuxili.CO_EMP)
                           //&& (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" /*&& tbs174.FL_JUSTI_CANCE != "C" && tbs174.FL_JUSTI_CANCE != "P"*/ : "" == "")
                       && (!String.IsNullOrEmpty(nomeProf) ? tb03.NO_COL.Contains(nomeProf) : 0 == 0)
                       //&& (!String.IsNullOrEmpty(nomeOperadora) ? tb250.NOM_OPER.Contains(nomeOperadora) : true)
                       && (!string.IsNullOrEmpty(nomePac) ? tb07.NO_ALU.ToUpper().Contains(nomePac) : 0 == 0)
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (tb07.CO_SITU_ALU == "A")
                       && (tb03.CO_SITU_COL == "ATI")
                       && (tbs174.ID_DEPTO_LOCAL_RECEP.HasValue ? (local == -1 ? 0 == 0 : tbs174.ID_DEPTO_LOCAL_RECEP == local) : (local == -1 ? 0 == 0 : tbs174.CO_DEPT == local))
                       select new Consultas
                       {
                           NO_RESP = tb07.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           NO_COL = tb03.NO_APEL_COL,
                           CO_ALU = tb07.CO_ALU,
                           CO_RESP = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP : (int?)null),
                           TELEFONE = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP : null),

                           //Dados para o nome do responsável e telefone
                           FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                           FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                           NO_PAI = tb07.NO_PAI_ALU,
                           NO_MAE = tb07.NO_MAE_ALU,
                           TELEFONE_MAE = tb07.NU_TEL_MAE,
                           TELEFONE_PAI = tb07.NU_TEL_PAI,

                           CO_COL = tbs174.CO_COL,
                           CO_AGEND_MEDIC = tbs174.ID_AGEND_HORAR,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           dt_Consul = tbs174.DT_AGEND_HORAR,
                           hr_Consul = tbs174.HR_AGEND_HORAR,
                           CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                           FL_CONF = tbs174.FL_CONF_AGEND,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           NO_CLASS_PROFI = !string.IsNullOrEmpty(tb03.DE_FUNC_COL) ? tb03.DE_FUNC_COL : "S/R",
                           TELEFONE_PROFI = tb03.NU_TELE_CELU_COL,
                           FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,
                           FL_SITUA_TRIAGEM = tbs174.FL_SITUA_TRIAGEM,
                           flPreAtendimento = tbs174.FL_PRE_ATEND,
                           //LOCAL = String.IsNullOrEmpty(tb14.CO_SIGLA_DEPTO) ? "-" : tb14.CO_SIGLA_DEPTO,
                           TP_CONSUL = tbs174.TP_CONSU,

                           Cortesia = tbs174.FL_CORTESIA,
                           Contratacao = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : "",
                           ContratParticular = tbs174.TB250_OPERA != null ? (tbs174.TB250_OPERA.FL_INSTI_OPERA != null && tbs174.TB250_OPERA.FL_INSTI_OPERA == "S") : false,

                           flPendFinanc = !String.IsNullOrEmpty(tb07.FL_PENDE_FINAN_GER) ? tb07.FL_PENDE_FINAN_GER == "S" : false,
                           FaltasConsec = !String.IsNullOrEmpty(tb07.FL_FALTOSO) ? tb07.FL_FALTOSO == "S" : false
                       }).OrderBy(w => w.dt_Consul).ThenBy(y => y.hr_Consul).ThenBy(x => x.NO_PAC_RECEB).ToList();

            grdAgendamentos.DataSource = res.Where(x => x.CO_SITU != "M");            //05/05/17 Where para não mostrar cancelados por movimentação na recepção;
            grdAgendamentos.DataBind();

        }

        /// <summary>
        /// Preenche a Grid de Consultas com os registros previamente cadastrados na funcionalidade e processo de registro de consulta.
        /// </summary>
        public class Consultas
        {
            public string TELEFONE_PROFI { get; set; }
            public string NU_TELEFONE_PROFI_V
            {
                get
                {
                    return (AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_PROFI));
                }
            }

            public int? CO_RESP { get; set; }
            public string NO_COL { get; set; }
            public int CO_ALU { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - "
                        + (this.NO_PAC_RECEB.Length > 23 ? this.NO_PAC_RECEB.Substring(0, 23) + "..." : this.NO_PAC_RECEB));
                }
            }
            public int NU_NIRE { get; set; }
            public string NO_PAC_RECEB { get; set; }
            public string NO_RESP { get; set; }
            public string TELEFONE { get; set; }

            //Insumo para tratar o nome do responsável dinamicamente
            public string FL_PAI_RESP { get; set; }
            public string FL_MAE_RESP { get; set; }
            public string NO_PAI { get; set; }
            public string NO_MAE { get; set; }
            public string NO_RESP_DINAMICO
            {
                get
                {
                    return AuxiliFormatoExibicao.TrataNomeResponsavel(this.FL_PAI_RESP, this.FL_MAE_RESP, this.NO_PAI, this.NO_MAE, this.NO_RESP);
                }
            }
            public string NO_RESP_DINAMICO_V
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.NO_RESP_DINAMICO))
                        return (this.NO_RESP_DINAMICO.Length > 40 ? this.NO_RESP_DINAMICO.Substring(0, 40) + "..." : this.NO_RESP_DINAMICO);
                    else
                        return " - ";
                }
            }
            public string TELEFONE_MAE { get; set; }
            public string TELEFONE_PAI { get; set; }
            public string TELEFONE_RESP_DINAMICO
            {
                get
                {
                    if (this.FL_MAE_RESP == "S" && this.FL_PAI_RESP == "S") //Se o pai e a mãe forem responsáveis
                    {
                        return (!string.IsNullOrEmpty(this.TELEFONE_MAE) ? //Se houver telefone da mãe, o retorna.
                        AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_MAE) :
                        AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_PAI)); //Se não, retorna o telefone do pai.
                    }
                    else if (this.FL_MAE_RESP == "S") //Se só a mãe for a responsável
                        return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_MAE);
                    else if (this.FL_PAI_RESP == "S") //Se só o pai for o responsável
                        return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_PAI);
                    else //Se nenhum dos dois forem responsáveis, retorna o telefone do responsável associado
                        return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE);
                }
            }

            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.dt_nascimento.HasValue)
                    {
                        int anos = DateTime.Now.Year - dt_nascimento.Value.Year;

                        if (DateTime.Now.Month < dt_nascimento.Value.Month || (DateTime.Now.Month == dt_nascimento.Value.Month && DateTime.Now.Day < dt_nascimento.Value.Day))
                            anos--;

                        idade = anos.ToString();
                    }
                    return idade;
                }
            }
            public string CO_SEXO { get; set; }

            public int? CO_COL { get; set; }
            public int CO_AGEND_MEDIC { get; set; }

            //Carrega informações gerais do agendamento
            public DateTime dt_Consul { get; set; }
            public string hr_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy") + " - " + this.hr_Consul;
                }
            }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI { get; set; }
            public int CO_ESPEC { get; set; }

            public string FL_AGEND_ENCAM { get; set; }
            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
                }
            }
            public string CO_SITU { get; set; }
            public string faltaJustif { get; set; }
            public string FL_SITUA_TRIAGEM { get; set; }
            //public string LOCAL { get; set; }
            public string TP_CONSUL { get; set; }
            public string TP_CONSUL_VALID
            {
                get
                {
                    string tipo = "";
                    switch (this.TP_CONSUL)
                    {
                        case "I":
                            tipo = "Inicial";
                            break;
                        case "N":
                            tipo = "Normal";
                            break;
                        case "R":
                            tipo = "Retorno";
                            break;
                        case "M":
                            tipo = "Emergêncial";
                            break;
                        case "P":
                            tipo = "Procedimento";
                            break;
                        case "E":
                            tipo = "Exame";
                            break;
                        case "C":
                            tipo = "Cirurgia";
                            break;
                        case "V":
                            tipo = "Vacina";
                            break;
                        case "O":
                            tipo = "Outros";
                            break;
                        case "U":
                            tipo = "Urgência";
                            break;

                        default:
                            tipo = "S/R";
                            break;
                    }
                    return tipo;
                }
            }

            public string imagem_URL
            {
                get
                {
                    if (CO_SITU == "W")
                    {
                        if (this.FL_CONF == "S")
                            return "/Library/IMG/PGS_SF_AgendaConfirmada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendamentoPorMovimentacao.png";
                    }
                    else if (CO_SITU == "M")
                        return "/Library/IMG/PGS_SF_AgendamentoCanceladoPorMovimentacao.png";
                    else
                        return AuxiliFormatoExibicao.RetornarUrlImagemAgend(this.CO_SITU, this.FL_AGEND_ENCAM, this.FL_CONF, faltaJustif, FL_SITUA_TRIAGEM);
                }

            }
            public string imagem_TIP
            {
                get
                {
                    return faltaJustif == "P" ? AuxiliFormatoExibicao.RetornarToolTipImagemAgend("Paciente") : AuxiliFormatoExibicao.RetornarToolTipImagemAgend(this.imagem_URL);
                }
            }

            public string Cortesia { get; set; }
            public string Contratacao { get; set; }
            public bool ContratParticular { get; set; }

            public bool flPendFinanc { get; set; }
            public bool FaltasConsec { get; set; }

            public string tpContr_TXT
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarTextoAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }

            public string tpContr_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }

            public string tpContr_CLS
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarClasseAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }
            public string flPreAtendimento { get; set; }
        }

        /// <summary>
        /// Carrega os agendamentos de avaliações
        /// </summary>
        private void CarregaAgendamentoAvaliacoes()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniAgendaAval.Text) ? DateTime.Parse(txtDtIniAgendaAval.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimAgendaAval.Text) ? DateTime.Parse(txtDtFimAgendaAval.Text) : DateTime.Now);
            string noPac = txtNomePacPesqAgendAvaliacao.Text.Trim();

            var res = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                       join tbs380 in TBS380_LOG_ALTER_STATUS_AGEND_AVALI.RetornaTodosRegistros() on tbs372.ID_AGEND_AVALI equals tbs380.TBS372_AGEND_AVALI.ID_AGEND_AVALI into lgs
                       where (!string.IsNullOrEmpty(noPac) ? tb07.NO_ALU.ToUpper().Contains(noPac) : 0 == 0)
                       && tbs372.FL_TIPO_AGENDA != "L"
                       && (EntityFunctions.TruncateTime(tbs372.DT_AGEND) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs372.DT_AGEND) <= EntityFunctions.TruncateTime(dtFim))
                       select new saidaAgendaAvaliacoes
                       {
                           ID_AGENDA_AVAL = tbs372.ID_AGEND_AVALI,
                           CO_ALU = tbs372.CO_ALU,
                           CO_RESP = tbs372.CO_RESP,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           NO_RESP = tb07.TB108_RESPONSAVEL.NO_RESP,
                           TELEFONE = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                           dt_Consul = tbs372.DT_AGEND.Value,
                           hr_Consul = tbs372.HR_AGEND,
                           FL_CONF = tbs372.FL_CONFIR_AGEND,
                           CO_SITU = tbs372.CO_SITUA,
                           FL_JUST = lgs.OrderByDescending(l => l.ID_LOG_ALTER_STATUS_AGEND_AVALI).FirstOrDefault().FL_JUSTI,
                           FL_ST_CAD_ALU = tbs372.FL_ST_CAD_ALU,
                           FL_ST_CAD_RSP = tbs372.FL_ST_CAD_RSP,
                           FL_ST_SLC_AVL = tbs372.FL_ST_SLC_AVL,
                           FL_ST_REG_INF = tbs372.FL_ST_REG_INF,

                           TIPO = tbs372.FL_TIPO_AGENDA,
                           LOCAL = !String.IsNullOrEmpty(tbs372.DE_LOCAL) ? tbs372.DE_LOCAL : tb07.TB25_EMPRESA.sigla,

                           //Dados para o nome do responsável
                           FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                           FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                           NO_PAI = tb07.NO_PAI_ALU,
                           NO_MAE = tb07.NO_MAE_ALU,

                           Cortesia = tbs372.FL_CORTESIA,
                           Contratacao = tbs372.TB250_OPERA != null ? tbs372.TB250_OPERA.NM_SIGLA_OPER : "",
                           ContratParticular = tbs372.TB250_OPERA != null ? (tbs372.TB250_OPERA.FL_INSTI_OPERA != null && tbs372.TB250_OPERA.FL_INSTI_OPERA == "S") : false
                       }).OrderBy(c => c.dt_Consul).ThenBy(c => c.hr_Consul).ToList();

            grdAgendaAvaliacoes.DataSource = res;
            grdAgendaAvaliacoes.DataBind();
        }

        public class saidaAgendaAvaliacoes
        {
            public int ID_AGENDA_AVAL { get; set; }

            //Insumo para tratar o nome do responsável dinamicamente
            public string FL_PAI_RESP { get; set; }
            public string FL_MAE_RESP { get; set; }
            public string NO_PAI { get; set; }
            public string NO_MAE { get; set; }
            public string NO_RESP_DINAMICO
            {
                get
                {
                    string NomePaiConcat = "";
                    #region Nome do Pai

                    if (!string.IsNullOrEmpty(this.NO_PAI))
                    {
                        var nomePai = this.NO_PAI.Split(' ');
                        string nomePai1 = nomePai[0];
                        string nomePai2 = "";
                        try
                        {
                            nomePai2 = nomePai[1];
                        }
                        catch (Exception e)
                        {
                        }
                        NomePaiConcat = nomePai1 + " " + nomePai2;
                    }

                    #endregion

                    string NomeMaeConcat = "";
                    #region Nome da Mãe

                    if (!string.IsNullOrEmpty(this.NO_MAE))
                    {
                        var nomeMae = this.NO_MAE.Split(' ');
                        string nomeMae1 = nomeMae[0];
                        string nomeMae2 = "";
                        try
                        {
                            nomeMae2 = nomeMae[1];
                        }
                        catch (Exception e)
                        {
                        }
                        NomeMaeConcat = nomeMae1 + " " + nomeMae2;
                    }

                    #endregion

                    //Se for os dois, concatena
                    if ((this.FL_PAI_RESP == "S") && (this.FL_MAE_RESP == "S"))
                        return NomePaiConcat + " - " + NomeMaeConcat;
                    else if (this.FL_PAI_RESP == "S") //Se for só pai
                        return NomePaiConcat;
                    else if (this.FL_MAE_RESP == "S") //Se for só mãe
                        return NomeMaeConcat;
                    else
                        return this.NO_RESP; //Se não for nenhum dos dois, retorna o nome do responsável associado
                }
            }
            public string NO_RESP_DINAMICO_V
            {
                get
                {
                    return (this.NO_RESP_DINAMICO.Length > 40 ? this.NO_RESP_DINAMICO.Substring(0, 40) + "..." : this.NO_RESP_DINAMICO);
                }
            }

            public int CO_ALU { get; set; }
            public int CO_RESP { get; set; }

            private string TIPO_;
            public string TIPO
            {
                get
                {
                    switch (TIPO_)
                    {
                        case "C":
                            return "CONS";
                        case "P":
                            return "PROC";
                        default:
                            return " - ";
                    }
                }
                set
                {
                    TIPO_ = value;
                }
            }
            public string LOCAL { get; set; }
            public DateTime dt_Consul { get; set; }
            public string hr_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy") + " - " + this.hr_Consul;
                }
            }

            public string NO_PAC
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - "
                        + (this.NO_PAC_RECEB.Length > 35 ? this.NO_PAC_RECEB.Substring(0, 35) + "..." : this.NO_PAC_RECEB));
                }
            }
            public int NU_NIRE { get; set; }
            public string NO_PAC_RECEB { get; set; }
            public string NO_RESP { get; set; }
            public string TELEFONE { get; set; }
            public string TELEFONE_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.TELEFONE) ? AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE) : " - ");
                }
            }

            public string FL_CONF { get; set; }
            public string CO_SITU { get; set; }
            public string FL_JUST { get; set; }

            public string FL_ST_CAD_ALU { get; set; }
            public string FL_ST_CAD_RSP { get; set; }
            public string FL_ST_SLC_AVL { get; set; }
            public string FL_ST_REG_INF { get; set; }

            public string imagem_URL
            {
                get
                {
                    if (this.CO_SITU == "A")
                    {
                        if (this.FL_CONF == "S")//Presente
                            return "/Library/IMG/PGS_SF_AgendaConfirmada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                    }
                    else if (this.CO_SITU == "E")
                        return "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                    else if (this.CO_SITU == "R")
                        return "/Library/IMG/PGS_SF_AgendaRealizada.png";
                    else if (this.CO_SITU == "C")
                    {
                        if (this.FL_JUST == "S")
                            return "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png";
                        else
                            return "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png";
                    }
                    else
                        return "/Library/IMG/Gestor_SemImagem.png";
                }
            }

            public string imagem_TIP
            {
                get
                {
                    switch (this.imagem_URL)
                    {
                        case "/Library/IMG/PGS_SF_AgendaEmAberto.png":
                            return "Agendamento em aberto";
                        case "/Library/IMG/PGS_SF_AgendaConfirmada.png":
                            return "Paciente presente";
                        case "/Library/IMG/PGS_SF_AgendaEncaminhada.png":
                            return "Agendamento Encaminhado";
                        case "/Library/IMG/PGS_SF_AgendaRealizada.png":
                            return "Agendamento Realizado";
                        case "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png":
                            return "Agendamento com Cancelamento Justificada";
                        case "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png":
                            return "Agendamento com Cancelamento Não Justificada";
                        default:
                            return " - ";
                    }
                }
            }

            public string Cortesia { get; set; }
            public string Contratacao { get; set; }
            public bool ContratParticular { get; set; }

            public string tpContr_TXT
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarTextoAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }

            public string tpContr_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }

            public string tpContr_CLS
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarClasseAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }
        }

        /// <summary>
        /// Método responsável por carregar os médicos plantonistas na grid correspondente
        /// </summary>
        /// <param name="CO_ESPEC"></param>
        private void carregaGridMedicosPlantonistas(string CLASS_PROFI = "0", int CO_DEPTO = 0)
        {

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO into l1
                       from ls in l1.DefaultIfEmpty()
                       where tb03.FL_PROFI_AVALI == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       select new MedicosPlantonistas
                       {
                           NO_COL_V = tb03.NO_COL,
                           co_col = tb03.CO_COL,
                           co_emp_col_pla = tb03.CO_EMP,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           LOCAL = ls.CO_SIGLA_DEPTO,
                           CO_ESPEC = tb03.CO_ESPEC,
                           TELEFONE = tb03.NU_TELE_CELU_COL,
                       }).OrderBy(w => w.NO_COL_V).ToList();

            if (res.Count > 0)
            {
                grdMedicosPlanto.DataSource = res;
                grdMedicosPlanto.DataBind();
            }
            else
            {
                grdMedicosPlanto.DataSource = null;
                grdMedicosPlanto.DataBind();
            }
        }

        /// <summary>
        /// Classe de saída para a Grid de Médicos
        /// </summary>
        public class MedicosPlantonistas
        {
            public string NO_COL_V { get; set; }
            public string NO_COL
            {
                get
                {
                    return (this.NO_COL_V.Length > 60 ? this.NO_COL_V.Substring(0, 60) + "..." : this.NO_COL_V);
                }
            }
            public int co_col { get; set; }
            public int co_emp_col_pla { get; set; }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_PROFI);
                }
            }
            public int? CO_ESPEC { get; set; }
            public string CO_TIPO_RISCO { get; set; }
            public string LOCAL { get; set; }

            public string TELEFONE { get; set; }
            public string TELEFONE_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.TELEFONE) ? AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE) : " - ");
                }
            }
        }

        protected void CarregarDadosMovimentacao(object sender = null, EventArgs e = null)
        {
            //Verificar se a data é anterior a hoje!
            //if (!String.IsNullOrEmpty(txtDtMovimOrigem.Text) && DateTime.Parse(txtDtMovimOrigem.Text) >= DateTime.Now.Date)
            //{
            lnkbMovimentar.OnClientClick = "";
            CarregaPacientesMovimentacao();
            //}
            //else
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possivel realizar a movimentação de datas anteriores a hoje!");

            AbreModalPadrao("AbreModalMovimentacao();");
        }

        private void CarregaPacientesMovimentacao()
        {
            if (!String.IsNullOrEmpty(drpProfiOrig.SelectedValue) && !String.IsNullOrEmpty(txtDtMovimOrigem.Text))
            {
                DateTime dtMov = DateTime.Parse(txtDtMovimOrigem.Text);
                int profOrigem = int.Parse(drpProfiOrig.SelectedValue);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           where tbs174.CO_EMP == LoginAuxili.CO_EMP
                                && tbs174.CO_COL == profOrigem
                                && tbs174.DT_AGEND_HORAR == dtMov
                                && (tbs174.CO_SITUA_AGEND_HORAR == "A" || tbs174.CO_SITUA_AGEND_HORAR == "W")
                                && tbs174.FL_CONF_AGEND == "N"
                           select new PacientesMovimentacao
                           {
                               NO_RESP_ = tb07.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                               NO_PAC_RECEB = (!string.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU),
                               NU_NIRE = tb07.NU_NIRE,
                               CO_ALU = tb07.CO_ALU,
                               CO_RESP = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP : (int?)null),

                               //Dados para o nome do responsável e telefone
                               FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                               FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                               NO_PAI = tb07.NO_PAI_ALU,
                               NO_MAE = tb07.NO_MAE_ALU,
                               TELEFONE = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP : null),
                               TELEFONE_MAE = tb07.NU_TEL_MAE,
                               TELEFONE_PAI = tb07.NU_TEL_PAI,

                               ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                               dt_Consul = tbs174.DT_AGEND_HORAR,
                               hr_Consul = tbs174.HR_AGEND_HORAR
                           }).OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).ToList();

                if (res.Count == 0)
                    lnkbMovimentar.OnClientClick = "alert('Não existe pacientes para realizar a movimentação nesse periódo!'); return false;";

                grdPacMovimentacoes.DataSource = res;
            }
            else
                grdPacMovimentacoes.DataSource = null;

            grdPacMovimentacoes.DataBind();
        }

        /// <summary>
        /// Preenche a Grid de Consultas com os registros previamente cadastrados na funcionalidade e processo de registro de consulta.
        /// </summary>
        public class PacientesMovimentacao
        {
            public int ID_AGEND_HORAR { get; set; }
            public int CO_ALU { get; set; }
            public int? CO_RESP { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NU_NIRE.ToString().PadLeft(7, '0') + " - "
                        + (this.NO_PAC_RECEB.Length > 43 ? this.NO_PAC_RECEB.Substring(0, 43) + "..." : this.NO_PAC_RECEB));
                }
            }
            public int NU_NIRE { get; set; }
            public string NO_PAC_RECEB { get; set; }

            //Insumo para tratar o nome do responsável dinamicamente
            public string FL_PAI_RESP { get; set; }
            public string FL_MAE_RESP { get; set; }
            public string NO_PAI { get; set; }
            public string NO_MAE { get; set; }
            public string NO_RESP_ { get; set; }
            public string NO_RESP
            {
                get
                {
                    return AuxiliFormatoExibicao.TrataNomeResponsavel(this.FL_PAI_RESP, this.FL_MAE_RESP, this.NO_PAI, this.NO_MAE, this.NO_RESP_);
                }
            }
            public string TELEFONE { get; set; }
            public string TELEFONE_MAE { get; set; }
            public string TELEFONE_PAI { get; set; }
            public string TELEFONE_RESP
            {
                get
                {
                    if (this.FL_MAE_RESP == "S" && this.FL_PAI_RESP == "S") //Se o pai e a mãe forem responsáveis
                    {
                        return (!string.IsNullOrEmpty(this.TELEFONE_MAE) ? //Se houver telefone da mãe, o retorna.
                        AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_MAE) :
                        AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_PAI)); //Se não, retorna o telefone do pai.
                    }
                    else if (this.FL_MAE_RESP == "S") //Se só a mãe for a responsável
                        return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_MAE);
                    else if (this.FL_PAI_RESP == "S") //Se só o pai for o responsável
                        return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_PAI);
                    else //Se nenhum dos dois forem responsáveis, retorna o telefone do responsável associado
                        return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE);
                }
            }

            //Carrega informações gerais do agendamento
            public DateTime dt_Consul { get; set; }
            public string hr_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy") + " " + this.hr_Consul;
                }
            }
        }

        private void CarregarPacientesGuia(DropDownList drp)
        {
            drp.Items.Clear();

            var nomePac = txtPacienteGuia.Text;
            var prontPac = !String.IsNullOrEmpty(txtProntuarioGuia.Text) ? int.Parse(txtProntuarioGuia.Text) : 0;
            var cpfPac = !String.IsNullOrEmpty(txtCpfGuia.Text) ? txtCpfGuia.Text.Replace(".", "").Replace("-", "").Trim() : "";

            var res_1 = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                         join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                         where (!String.IsNullOrEmpty(nomePac) ? tb07.NO_ALU.Contains(nomePac) : true)
                         && (prontPac != 0 ? tb07.NU_NIRE == prontPac : true)
                         && (!String.IsNullOrEmpty(cpfPac) ? tb07.NU_CPF_ALU == cpfPac : true)
                         && tbs174.CO_EMP == LoginAuxili.CO_EMP
                         select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            var res_2 = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                         join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                         where (!String.IsNullOrEmpty(nomePac) ? tb07.NO_ALU.Contains(nomePac) : true)
                         && (prontPac != 0 ? tb07.NU_NIRE == prontPac : true)
                         && (!String.IsNullOrEmpty(cpfPac) ? tb07.NU_CPF_ALU == cpfPac : true)
                         && tbs372.FL_TIPO_AGENDA == "C"
                         select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            var res = res_1.Union(res_2);

            if (res != null && res.Count() > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res;
                drp.DataBind();
            }

            if (res != null && res.Count() == 1)
            {
                drp.SelectedValue = res.FirstOrDefault().CO_ALU.ToString();
                CarregaAgendGuia();
            }

            drp.Items.Insert(0, new ListItem("EM BRANCO", "0"));
        }

        private void CarregaAgendGuia()
        {
            ddlAgendGuia.Items.Clear();
            txtProntuarioGuia.Text = "";
            txtCpfGuia.Text = "";

            int coAlu = !String.IsNullOrEmpty(drpPacienteGuia.SelectedValue) ? int.Parse(drpPacienteGuia.SelectedValue) : 0;
            var dtIniAgen = !String.IsNullOrEmpty(txtDtGuiaIni.Text) ? DateTime.Parse(txtDtGuiaIni.Text) : (DateTime?)null;
            var dtFinAgen = !String.IsNullOrEmpty(txtDtGuiaFim.Text) ? DateTime.Parse(txtDtGuiaFim.Text) : (DateTime?)null;

            if (coAlu != 0)
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                           where (tbs174.CO_ALU == coAlu)
                           && (dtIniAgen.HasValue ? tbs174.DT_AGEND_HORAR >= dtIniAgen : true)
                           && (dtFinAgen.HasValue ? tbs174.DT_AGEND_HORAR <= dtFinAgen : true)
                           select new AgendGuia
                           {
                               dtAgend = tbs174.DT_AGEND_HORAR,
                               hrAgend = tbs174.HR_AGEND_HORAR,
                               nuRegis = " - " + tbs174.NU_REGIS_CONSUL,
                               idAgend = tbs174.ID_AGEND_HORAR,
                               matriculaProfissional = tb03.CO_MAT_COL,
                               nomeProfissional = tb03.NO_COL,
                               registroProfissional = tb03.CO_SIGLA_ENTID_PROFI + " " + tb03.NU_ENTID_PROFI + " " + tb03.CO_UF_ENTID_PROFI,
                               local = tb14.NO_DEPTO
                           }).ToList();

                if (res != null && res.Count > 0)
                {
                    ddlAgendGuia.DataTextField = "NO_AGEND";
                    ddlAgendGuia.DataValueField = "idAgend";
                    ddlAgendGuia.DataSource = res;
                    ddlAgendGuia.DataBind();
                }

                var pac = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                pac.TB250_OPERAReference.Load();
                if (pac != null)
                {
                    txtProntuarioGuia.Text = pac.NU_NIRE.ToString();
                    txtCpfGuia.Text = !String.IsNullOrEmpty(pac.NU_CPF_ALU) ? AuxiliFormatoExibicao.preparaCPFCNPJ(pac.NU_CPF_ALU) : "";

                    if (pac.TB250_OPERA != null && drpOperGuia.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                        drpOperGuia.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
                }
            }

            ddlAgendGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));
        }

        public class AgendGuia
        {
            public string matriculaProfissional;
            public string nomeProfissional;
            public string registroProfissional;
            public DateTime dtAgend { get; set; }
            public string hrAgend { get; set; }
            public string nuRegis { get; set; }
            public string local { get; set; }
            public int idAgend { get; set; }

            public string NO_AGEND
            {
                get
                {
                    try
                    {
                        return this.dtAgend.ToString("dd/MM/yyyy") + " " + this.hrAgend + this.nuRegis + " - " + this.matriculaProfissional + " " + this.nomeProfissional + " " + this.registroProfissional + " " + local;
                    }
                    catch (Exception)
                    {
                        return this.dtAgend.ToString("dd/MM/yyyy") + " " + this.hrAgend + "  " + this.nuRegis;
                    }
                }
            }
        }

        /// <summary>
        /// Responsável por carregar os profissionais
        /// </summary>
        private void CarregaProfissionaisGuia()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfissionalGuia, 0, false, "0", true);

            if (drpProfissionalGuia.Items.FindByValue(LoginAuxili.CO_COL.ToString()) != null)
                drpProfissionalGuia.SelectedValue = LoginAuxili.CO_COL.ToString();
        }

        /// <summary>
        /// Método que controla visibilidade das Tabs da tela de matrícula
        /// </summary>
        /// <param name="tab">Nome da tab</param>
        protected void ControlaTabs(string tab)
        {/*
            tabInfosCadas.Style.Add("display", "none");
            tabSelAgenda.Style.Add("display", "none");
            tabSelAvaliador.Style.Add("display", "none");
            tabRegisItens.Style.Add("display", "none");
            tabSelInfosResp.Style.Add("display", "none");*/

            if (tab == "ICA")
                AbreModalPadrao("AbreModalInfosCadasPac();");
            //tabInfosCadas.Style.Add("display", "block");
            else if (tab == "SAV")
                AbreModalPadrao("AbreModalSelAvaliador();");
            //tabSelAvaliador.Style.Add("display", "block");
            else if (tab == "RGI")
                AbreModalPadrao("AbreModalRegisItens();");
            //tabRegisItens.Style.Add("display", "block");
            else if (tab == "ICR")
                AbreModalPadrao("AbreModalInfosCadasResp();");
            //tabSelInfosResp.Style.Add("display", "block");
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade(DropDownList ddlCidades, DropDownList ddlUfs)
        {
            string uf = ddlUfs.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidades, false, uf, LoginAuxili.CO_EMP, true, true);
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro(DropDownList ddlUf, DropDownList ddlCidades, DropDownList ddlBairros)
        {
            string uf = ddlUf.SelectedValue;
            int cid = ddlCidades.SelectedValue != "" ? int.Parse(ddlCidades.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaBairros(ddlBairros, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as informações da unidade logada em campos definidos
        /// </summary>
        private void CarregaDadosUnidLogada()
        {
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            res.TB83_PARAMETROReference.Load();

            //Verifica se existe integração com o financeiro
            //if (res.TB83_PARAMETRO != null)
            //    chkRespFinanc.Visible = res.TB83_PARAMETRO.FL_INTEG_FINAN == "S" ? true : false;

            txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_EMP;
            txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_END_EMP;
        }

        /// <summary>
        /// Gera um CPF válido
        /// </summary>
        /// <param name="ComPontos"></param>
        /// <returns></returns>
        private string GerarNovoCPF(bool ComPontos)
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            if (ComPontos)
                semente = string.Format("{0}.{1}.{2}-{3}", semente.Substring(0, 3), semente.Substring(3, 3), semente.Substring(6, 3), semente.Substring(9, 2));

            return semente;
        }

        /// <summary>
        /// Método responsável por receber os valores por parâmetros e inserir nos campos correspondentes
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="Nome"></param>
        /// <param name="sexo"></param>
        /// <param name="dtNasc"></param>
        /// <param name="RG"></param>
        /// <param name="ORGrg"></param>
        /// <param name="UFrg"></param>
        /// <param name="TelFixo"></param>
        /// <param name="TelCelu"></param>
        /// <param name="TelCome"></param>
        /// <param name="Whats"></param>
        /// <param name="Face"></param>
        /// <param name="CEP"></param>
        /// <param name="UF"></param>
        /// <param name="Cidade"></param>
        /// <param name="Bairro"></param>
        /// <param name="Logradouro"></param>
        /// <param name="Email"></param>
        private void CarregarDadosResponsavel(string cpf, string Nome, string sexo, DateTime dtNasc, string RG,
            string ORGrg, string UFrg, string TelFixo, string TelCelu, string TelCome, string Whats, string Face,
            string CEP, string UF, int Cidade, int? Bairro, string Logradouro, string Email)
        {
            txtCPFResp.Text = cpf;
            txtNomeResp.Text = Nome;
            ddlSexResp.SelectedValue = sexo;
            txtDtNascResp.Text = dtNasc.ToString();
            txtNuIDResp.Text = RG;
            txtOrgEmiss.Text = ORGrg;
            ddlUFOrgEmis.SelectedValue = UFrg;
            txtTelFixResp.Text = TelFixo;
            txtTelCelResp.Text = TelCelu;
            txtTelComResp.Text = TelCome;
            txtNuWhatsResp.Text = Whats;
            txtCEP.Text = CEP;
            ddlUF.SelectedValue = UF;
            carregaCidade(ddlCidade, ddlUF);
            ddlCidade.SelectedValue = (Cidade != 0 ? Cidade.ToString() : "");
            carregaBairro(ddlUF, ddlCidade, ddlBairro);
            ddlBairro.SelectedValue = (Bairro != 0 && Cidade != 0 ? Bairro.ToString() : "");
            txtLograEndResp.Text = Logradouro;
            txtEmailResp.Text = Email;
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                if (!string.IsNullOrEmpty(txtCpfPaci.Text))
                {
                    //Se tiver cpf no paciente, carrega no responsável, se não tiver, deixa os 000...
                    txtCPFResp.Text = (!string.IsNullOrEmpty(txtCpfPaci.Text) ? txtCpfPaci.Text : txtCPFResp.Text);
                    txtNomeResp.Text = txtnompac.Text;
                    txtDtNascResp.Text = txtDtNascPaci.Text;
                    ddlSexResp.SelectedValue = ddlSexoPaci.SelectedValue;
                    txtTelCelResp.Text = txtTelCelPaci.Text;
                    txtTelFixResp.Text = txtTelResPaci.Text;
                    ddlGrParen.SelectedValue = "OU";
                    txtNuWhatsResp.Text = txtWhatsPaci.Text;

                    //PesquisaCarregaResp((int?)null, txtCPFResp.Text);

                    //txtEmailPaci.Enabled = false;
                    //txtCPFMOD.Enabled = false;
                    //txtnompac.Enabled = false;
                    //txtDtNascPaci.Enabled = false;
                    //ddlSexoPaci.Enabled = false;
                    //txtTelCelPaci.Enabled = false;
                    //txtTelCelPaci.Enabled = false;
                    //txtTelResPaci.Enabled = false;
                    //ddlGrParen.Enabled = false;
                    //txtWhatsPaci.Enabled = false;
                }
                else
                {
                    //AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente não foi encontrado, para concluir a operação, cadastre um CPF válido.");
                    txtCPFResp.Focus();

                    chkPaciEhResp.Checked = false;
                    chkPaciMoraCoResp.Checked = false;
                    return;
                }
            }
            else
            {
                txtCPFResp.Text = "";
                txtNomeResp.Text = "";
                txtDtNascResp.Text = "01/01/1900";
                ddlSexResp.SelectedValue = "";
                txtTelCelResp.Text = "";
                txtTelFixResp.Text = "";
                txtEmailResp.Text = "";
                txtNuWhatsResp.Text = "";

                //txtCPFMOD.Enabled = true;
                //txtnompac.Enabled = true;
                //txtDtNascPaci.Enabled = true;
                //ddlSexoPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelResPaci.Enabled = true;
                //ddlGrParen.Enabled = true;
                //txtEmailPaci.Enabled = true;
                //txtWhatsPaci.Enabled = true;
                //hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavelMODAL()
        {
            if (chkPaciEhRespMODAL.Checked)
            {
                if (!string.IsNullOrEmpty(txtCPFPaciMODAL.Text))
                {
                    //Se tiver cpf no paciente, carrega no responsável, se não tiver, deixa os 000...
                    txtCPFRespMODAL.Text = (!string.IsNullOrEmpty(txtCPFPaciMODAL.Text) ? txtCPFPaciMODAL.Text : txtCPFRespMODAL.Text);
                    txtNomeRespMODAL.Text = txtNomePaciMODAL.Text;
                    txtDtNascRespMODAL.Text = txtDtNascPaciMODAL.Text;
                    ddlSexoRespMODAL.SelectedValue = ddlSexoPaciMODAL.SelectedValue;
                    txtTelCelRespMODAL.Text = txtCelPaciMODAL.Text;
                    txtTelFixoRespMODAL.Text = txtFixPaciMODAL.Text;
                    ddlGrauParenMODAL.SelectedValue = "OU";
                    txtEmailRespMODAL.Text = txtEmailMODAL.Text;
                    txtWhatsRespMODAL.Text = txtNuWhatsPaciMODAL.Text;

                    //PesquisaCarregaRespMODAL((int?)null, txtCPFRespMODAL.Text);
                }
                else
                {
                    //AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente não foi encontrado, para concluir a operação, cadastre um CPF válido.");
                    txtCPFResp.Focus();

                    chkPaciEhRespMODAL.Checked = false;
                    chkPaciMoraCoRespMODAL.Checked = false;
                    return;
                }
            }
            else
            {
                txtCPFRespMODAL.Text = "000.000.000-00";
                txtNomeRespMODAL.Text = "";
                txtDtNascRespMODAL.Text = "01/01/1900";
                ddlSexoRespMODAL.SelectedValue = "";
                txtTelCelRespMODAL.Text = "";
                txtTelFixoRespMODAL.Text = "";
                txtEmailRespMODAL.Text = "";
                txtWhatsRespMODAL.Text = "";
            }
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp, string cpfRespParam = null)
        {
            string cpfResp = (string.IsNullOrEmpty(cpfRespParam) ?
                txtCPFResp.Text.Replace(".", "").Replace("-", "") : cpfRespParam.Replace(".", "").Replace("-", ""));

            if (!string.IsNullOrEmpty(cpfResp))
            {
                if (co_resp.HasValue)
                {
                    var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                               where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                               select tb108).FirstOrDefault();

                    if (res != null)
                    {
                        txtNomeResp.Text = res.NO_RESP.ToUpper();
                        txtCPFResp.Text = res.NU_CPF_RESP;
                        txtNuIDResp.Text = res.CO_RG_RESP;
                        txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                        ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                        txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                        ddlSexResp.SelectedValue = res.CO_SEXO_RESP;

                        txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_ENDE_RESP;
                        ddlUF.SelectedValue = res.CO_ESTA_RESP;
                        txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_RESP;

                        carregaCidade(ddlCidade, ddlUF);
                        if (res.CO_CIDADE != null && ddlCidade.Items.FindByValue(res.CO_CIDADE.ToString()) != null)
                            ddlCidade.SelectedValue = res.CO_CIDADE.ToString();
                        carregaBairro(ddlUF, ddlCidade, ddlBairro);
                        if (res.CO_BAIRRO != null && ddlBairro.Items.FindByValue(res.CO_BAIRRO.ToString()) != null)
                            ddlBairro.SelectedValue = res.CO_BAIRRO.ToString();

                        txtEmailResp.Text = res.DES_EMAIL_RESP;
                        txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                        txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                        txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                        txtTelComResp.Text = res.NU_TELE_COME_RESP;
                        txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                        hidCoResp.Value = res.CO_RESP.ToString();
                        //this.lblComRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE == "S" ? true : false);
                        //this.lblSemRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE != "S" ? true : false);

                        res.TBS366_FUNCAO_SIMPLReference.Load();
                        if (res.TBS366_FUNCAO_SIMPL != null)
                            ddlFuncao.SelectedValue = res.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString();
                    }
                }
            }
            else
            {
                //AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente não foi encontrado, para concluir a operação, cadastre um CPF válido.");
                txtCPFResp.Focus();
                return;
            }

        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaRespMODAL(int? co_resp, string cpfRespParam = null)
        {
            string cpfResp = (string.IsNullOrEmpty(cpfRespParam) ?
                txtCPFRespMODAL.Text.Replace(".", "").Replace("-", "") : cpfRespParam.Replace(".", "").Replace("-", ""));

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select tb108).FirstOrDefault();

            if (res != null)
            {
                txtNomeRespMODAL.Text = res.NO_RESP.ToUpper();
                txtCPFRespMODAL.Text = res.NU_CPF_RESP;
                txtNuRGRespMODAL.Text = res.CO_RG_RESP;
                txtORGEmissRespMODAL.Text = res.CO_ORG_RG_RESP;
                ddlUfMODAL.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascRespMODAL.Text = res.DT_NASC_RESP.ToString();
                ddlSexoRespMODAL.SelectedValue = res.CO_SEXO_RESP;
                txtEmailRespMODAL.Text = res.DES_EMAIL_RESP;
                txtTelCelRespMODAL.Text = res.NU_TELE_CELU_RESP;
                txtTelFixoRespMODAL.Text = res.NU_TELE_RESI_RESP;
                txtWhatsRespMODAL.Text = res.NU_TELE_WHATS_RESP;
                txtTelComRespMODAL.Text = res.NU_TELE_COME_RESP;
                txtFaceRespMODAL.Text = res.NM_FACEBOOK_RESP;
                hidCoRespModal.Value = res.CO_RESP.ToString();
                //this.lblComRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE == "S" ? true : false);
                //this.lblSemRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE != "S" ? true : false);
            }
        }

        /// <summary>
        /// Pesquisa se já existe um Paciente com o CPF informado na tabela de Pacientes, se já existe ele preenche as informações do Paciente 
        /// </summary>
        private void PesquisaCarregaPaci(int ID, int coResp)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_ALU == ID
                       select tb07).FirstOrDefault();

            if (res != null)
            {
                res.TBS366_FUNCAO_SIMPLReference.Load();
                res.TBS366_FUNCAO_SIMPL1Reference.Load();
                res.TBS377_INDIC_PACIENTESReference.Load();
                res.TB250_OPERAReference.Load();
                res.TB251_PLANO_OPERAReference.Load();
                res.TB367_CATEG_PLANO_SAUDEReference.Load();
                res.TBS387_DEFICReference.Load();

                txtNuProntuario.Text = res.NU_NIRE.ToString();
                txtCpfPaci.Text = res.NU_CPF_ALU;
                txtNuNisPaci.Text = res.NU_NIS.ToString();
                txtnompac.Text = res.NO_ALU.ToUpper();
                ddlSituacaoAlu.SelectedValue = res.CO_SITU_ALU;
                txtApelidoPaciente.Text = !String.IsNullOrEmpty(res.NO_APE_ALU) ? res.NO_APE_ALU.ToUpper() : "";
                ddlSexoPaci.SelectedValue = res.CO_SEXO_ALU;
                txtDtNascPaci.Text = res.DT_NASC_ALU.ToString();
                ddlOrigemPaci.SelectedValue = res.CO_ORIGEM_ALU;
                txtNaturalidade.Text = res.DE_NATU_ALU;
                ddlEstadoCivil.SelectedValue = res.CO_ESTADO_CIVIL;
                txtNomeMae.Text = !String.IsNullOrEmpty(res.NO_MAE_ALU) ? res.NO_MAE_ALU.ToUpper() : "";
                ddlProfissaoNomeMae.SelectedValue = (res.TBS366_FUNCAO_SIMPL != null ? res.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString() : "");
                txtNomePai.Text = !String.IsNullOrEmpty(res.NO_PAI_ALU) ? res.NO_PAI_ALU.ToUpper() : "";
                ddlProfiPai.SelectedValue = (res.TBS366_FUNCAO_SIMPL1 != null ? res.TBS366_FUNCAO_SIMPL1.ID_FUNCAO_SIMPL.ToString() : "");
                txtAnoAtiv.Text = res.CO_ANO_ORI;
                txtMesAtiv.Text = res.CO_MES_REFER.ToString();
                txtNuCarSaude.Text = res.NU_CARTAO_SAUDE;
                ddlDeficienciaAlu.SelectedValue = (res.TBS387_DEFIC != null ? res.TBS387_DEFIC.ID_DEFIC.ToString() : "");
                ddlEtniaAlu.SelectedValue = res.TP_RACA;
                txtAltura.Text = res.NU_ALTU.ToString();
                txtPeso.Text = res.NU_PESO.ToString();
                ddlIndicacao.SelectedValue = (res.TBS377_INDIC_PACIENTES != null ? res.TBS377_INDIC_PACIENTES.ID_INDIC_PACIENTES.ToString() : "");
                txtNumeroRGPaciente.Text = res.CO_RG_ALU;
                txtOrgEmissRGPaciente.Text = res.CO_ORG_RG_ALU;
                ddlUFRGPaciente.SelectedValue = res.CO_ESTA_RG_ALU;
                txtTelResPaci.Text = res.NU_TELE_RESI_ALU;
                txtTelCelPaci.Text = res.NU_TELE_CELU_ALU;
                txtTeComePaci.Text = res.NU_TELE_COME_ALU;
                txtWhatsPaci.Text = res.NU_TELE_WHATS_ALU;
                txtFacePaci.Text = res.NO_FACEB_PACI;
                txtTelMae.Text = res.NU_TEL_MAE;
                txtTelPai.Text = res.NU_TEL_PAI;
                ddlGrParen.SelectedValue = res.CO_GRAU_PAREN_RESP;
                hidCoPac.Value = res.CO_ALU.ToString();
                ddlEtniaAlu.SelectedValue = res.TP_RACA;

                ddlObitoMae.SelectedValue = res.FLA_OBITO_MAE;
                ddlObitoPai.SelectedValue = res.FLA_OBITO_PAI;
                chkMaeResp.Checked = (res.FL_MAE_RESP_ATEND == "S" ? true : false);
                chkPaiResp.Checked = (res.FL_PAI_RESP_ATEND == "S" ? true : false);
                chkMaeResp.Enabled = (ddlObitoMae.SelectedValue == "S" ? false : true);
                chkPaiResp.Enabled = (ddlObitoPai.SelectedValue == "S" ? false : true);

                chkMaeRespFinanc.Checked = (res.FL_MAE_RESP_FINAN == "S" ? true : false);
                chkMaeRespFinanc.Enabled = (ddlObitoMae.SelectedValue == "S" ? false : true);

                chkPaiRespFinanc.Checked = (res.FL_PAI_RESP_FINAN == "S" ? true : false);
                chkPaiRespFinanc.Enabled = (ddlObitoPai.SelectedValue == "S" ? false : true);

                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadoraPacie, false);
                ddlOperadoraPacie.SelectedValue = (res.TB250_OPERA != null ? res.TB250_OPERA.ID_OPER.ToString() : "");
                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanoPacie, ddlOperadoraPacie, false);
                ddlPlanoPacie.SelectedValue = (res.TB251_PLANO_OPERA != null ? res.TB251_PLANO_OPERA.ID_PLAN.ToString() : "");
                AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoriaPacie, ddlPlanoPacie, false);
                ddlCategoriaPacie.SelectedValue = (res.TB367_CATEG_PLANO_SAUDE != null ? res.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE.ToString() : "");
                txtNuPlanoPacie.Text = res.NU_PLANO_SAUDE;
                txtVencPlano.Text = res.DT_VENC_PLAN.HasValue ? res.DT_VENC_PLAN.Value.ToShortDateString() : "";

                //txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_ENDE_ALU;
                //ddlUF.SelectedValue = res.CO_ESTA_ALU;
                //txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_ALU;

                //res.TB905_BAIRROReference.Load();
                //carregaCidade(ddlCidade, ddlUF);
                //if (res.TB905_BAIRRO != null && ddlCidade.Items.FindByValue(res.TB905_BAIRRO.CO_CIDADE.ToString()) != null)
                //    ddlCidade.SelectedValue = res.TB905_BAIRRO.CO_CIDADE.ToString();
                //carregaBairro(ddlUF, ddlCidade, ddlBairro);
                //if (res.TB905_BAIRRO != null && ddlBairro.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString()) != null)
                //    ddlBairro.SelectedValue = res.TB905_BAIRRO.CO_BAIRRO.ToString();

                res.ImageReference.Load();
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                if (res.Image != null)
                    upImagemAluno.CarregaImagem(res.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);

                PesquisaCarregaResp(coResp);
            }
        }

        /// <summary>
        /// Pesquisa se já existe um Paciente com o CPF informado na tabela de Pacientes, se já existe ele preenche as informações do Paciente 
        /// </summary>
        private void PesquisaCarregaPaciMODAL(int ID, int? coResp)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_ALU == ID
                       select tb07).FirstOrDefault();

            if (res != null)
            {
                txtNomePaciMODAL.Text = res.NO_ALU.ToUpper();
                txtCPFPaciMODAL.Text = res.NU_CPF_ALU;
                txtNuProntuMODAL.Text = res.NU_NIRE.ToString().PadLeft(7, '0');
                txtSUSPaciMODAL.Text = res.NU_NIS.ToString();
                txtDtNascPaciMODAL.Text = res.DT_NASC_ALU.ToString();
                ddlSexoPaciMODAL.SelectedValue = res.CO_SEXO_ALU;
                txtCelPaciMODAL.Text = res.NU_TELE_CELU_ALU;
                txtFixPaciMODAL.Text = res.NU_TELE_RESI_ALU;
                ddlGrauParenMODAL.SelectedValue = res.CO_GRAU_PAREN_RESP;
                txtEmailMODAL.Text = res.NO_EMAIL_PAI;
                txtNuWhatsPaciMODAL.Text = res.NU_TELE_WHATS_ALU;
                hidCoPacModal.Value = res.CO_ALU.ToString();
                ddlEtniaPaciMODAL.SelectedValue = res.TP_RACA;
                txtPastaControl.Text = res.DE_PASTA_CONTR;
                txtTelMigrado.Text = res.TEL_MIGRAR;
                txtLograMODAL.Text = txtLograMODALAUXILIAR.Text = res.DE_ENDE_ALU;
                ddlUfMODAL.SelectedValue = res.CO_ESTA_ALU;
                txtCEPPaciMODAL.Text = txtCEPPaciMODALAUXILIAR.Text = res.CO_CEP_ALU;

                res.TB905_BAIRROReference.Load();
                carregaCidade(ddlCidadeMODAL, ddlUfMODAL);
                if (res.TB905_BAIRRO != null && ddlCidadeMODAL.Items.FindByValue(res.TB905_BAIRRO.CO_CIDADE.ToString()) != null)
                    ddlCidadeMODAL.SelectedValue = res.TB905_BAIRRO.CO_CIDADE.ToString();
                carregaBairro(ddlUfMODAL, ddlCidadeMODAL, ddlBairroMODAL);
                if (res.TB905_BAIRRO != null && ddlBairroMODAL.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString()) != null)
                    ddlBairroMODAL.SelectedValue = res.TB905_BAIRRO.CO_BAIRRO.ToString();

                res.ImageReference.Load();
                updImagePacienteMODAL.ImagemLargura = 70;
                updImagePacienteMODAL.ImagemAltura = 85;

                if (res.Image != null)
                    updImagePacienteMODAL.CarregaImagem(res.Image.ImageId);
                else
                    updImagePacienteMODAL.CarregaImagem(0);

                res.TB108_RESPONSAVELReference.Load();

                //if (res.TB108_RESPONSAVEL != null)
                //    PesquisaCarregaResp(res.TB108_RESPONSAVEL.CO_RESP);
                if (coResp.HasValue)
                    PesquisaCarregaRespMODAL(coResp);
            }
        }

        /// <summary>
        /// Verifica o nire
        /// </summary>
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
                        txtNuProntuario.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNuProntuario.Text = "1";
                }
            }
        }

        /// <summary>
        /// Carrega as Operadoras
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false, true, false, true);
        }

        /// <summary>
        /// Carrega os Planos de saúde de acordo com a operadora
        /// </summary>
        private void CarregaPlanos()
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, false, true, false, true);
        }

        /// <summary>
        /// Carrega as categorias de acordo com o plano
        /// </summary>
        private void CarregaCategorias()
        {
            AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoria, ddlPlano, false, true, false, true);
        }

        public enum EObjetoLogAgenda
        {
            paraAtendimento,
            paraAvaliacao
        }

        /// <summary>
        /// Salva o log de alteração de status de agenda
        /// </summary>
        /// <param name="CO_TIPO_ALTERACAO"></param>
        private void SalvaLogAlteracaoStatusAgenda(int idAgenda, string CO_TIPO_ALTERACAO, bool flagSim, EObjetoLogAgenda etipo)
        {
            //Se for para atendimento
            if (etipo == EObjetoLogAgenda.paraAtendimento)
            {
                #region Para Atendimento

                TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();

                tbs375.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                tbs375.FL_TIPO_LOG = CO_TIPO_ALTERACAO;
                tbs375.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs375.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs375.DT_CADAS = DateTime.Now;
                tbs375.IP_CADAS = Request.UserHostAddress;
                //Se for triagem, salva no log para diferenciar do encaminhamento para triagem. TA.06/07/2016
                tbs375.DE_OBSER = (CO_TIPO_ALTERACAO == "E" ? "ENCAMINHADO" : (CO_TIPO_ALTERACAO == "T" ? "TRIAGEM" : (CO_TIPO_ALTERACAO == "P" ? "ATENDIMENTO" : null)));
                //Se for de presença, verifica se o parâmetro recebido é como presente ou não
                tbs375.FL_CONFIR_AGEND = (CO_TIPO_ALTERACAO == "P" ? (flagSim ? "S" : "N") : null);
                //Se for de encaminhamento, verifica se o parâmetro recebido é como sim ou não
                tbs375.FL_AGEND_ENCAM = (CO_TIPO_ALTERACAO == "E" ? (flagSim ? "S" : "N") : (CO_TIPO_ALTERACAO == "T" ? (flagSim ? "T" : "N") : null));

                TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375, true);

                if ((CO_TIPO_ALTERACAO == "P") || (CO_TIPO_ALTERACAO == "E" || CO_TIPO_ALTERACAO == "T"))
                {
                    TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                    tbs174.FL_CONF_AGEND = (flagSim ? "S" : "N"); //Salva este apenas se a alteração for para presença
                    tbs174.FL_AGEND_ENCAM = (CO_TIPO_ALTERACAO == "E" ? (flagSim ? "S" : "N") : (CO_TIPO_ALTERACAO == "T" ? (flagSim ? "T" : "N") : null)); //Salva este apenas se a alteração for para encaminhamento

                    //Realiza esses processos apenas se a alteração no registro for do tipo de PRESENÇA
                    #region Se for uma alteração da Presença

                    if (CO_TIPO_ALTERACAO == "P")
                    {
                        if (flagSim) //Se for presença SIM, grava as informações pertinentes
                        {
                            tbs174.DT_PRESE = DateTime.Now;
                            tbs174.CO_COL_PRESE = LoginAuxili.CO_COL;
                            tbs174.CO_EMP_COL_PRESE = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs174.CO_EMP_PRESE = LoginAuxili.CO_EMP;
                            tbs174.IP_PRESE = Request.UserHostAddress;
                            if (!String.IsNullOrEmpty(LocalAtend.Value))
                                tbs174.ID_DEPTO_LOCAL_ATENDI = int.Parse(LocalAtend.Value);
                        }
                        else //Se for presença NÃO, grava os campos de presença como NULL
                        {
                            tbs174.DT_PRESE = (DateTime?)null;
                            tbs174.CO_COL_PRESE =
                            tbs174.CO_EMP_COL_PRESE =
                            tbs174.CO_EMP_PRESE = (int?)null;
                            tbs174.IP_PRESE = null;
                        }
                    }

                    #endregion

                    //Realiza esses processos apenas se a alteração no registro for do tipo de ENCAMINHAMENTO
                    #region Se for uma alteração da Encaminhamento

                    if (CO_TIPO_ALTERACAO == "E" || CO_TIPO_ALTERACAO == "T")
                    {
                        if (flagSim) //Se for presença SIM, grava as informações pertinentes
                        {
                            tbs174.DT_ENCAM = DateTime.Now;
                            tbs174.CO_COL_ENCAM = LoginAuxili.CO_COL;
                            tbs174.CO_EMP_COL_ENCAM = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs174.CO_EMP_ENCAM = LoginAuxili.CO_EMP;
                            tbs174.IP_ENCAM = Request.UserHostAddress;
                            if (CO_TIPO_ALTERACAO == "E" && !String.IsNullOrEmpty(LocalAtend.Value))
                                tbs174.ID_DEPTO_LOCAL_ATENDI = int.Parse(LocalAtend.Value);
                            else if (CO_TIPO_ALTERACAO == "T" && !String.IsNullOrEmpty(LocalTriagem.Value))
                                tbs174.ID_DEPTO_LOCAL_TRIAGEM = int.Parse(LocalTriagem.Value);
                        }
                        else //Se for presença NÃO, grava os campos de presença como NULL
                        {
                            tbs174.DT_ENCAM = (DateTime?)null;
                            tbs174.CO_COL_ENCAM =
                            tbs174.CO_EMP_COL_ENCAM =
                            tbs174.CO_EMP_ENCAM = (int?)null;
                            tbs174.IP_ENCAM = null;
                        }
                    }

                    #endregion

                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                }

                #endregion
            }
            else if (etipo == EObjetoLogAgenda.paraAvaliacao) //Se for para avaliação //TA.06/07/2016
            {
                #region Para Avaliação

                TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs380 = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();

                tbs380.TBS372_AGEND_AVALI = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgenda);
                tbs380.FL_TIPO_LOG = CO_TIPO_ALTERACAO;
                tbs380.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs380.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs380.DT_CADAS = DateTime.Now;
                tbs380.IP_CADAS = Request.UserHostAddress;
                //Se for de presença, verifica se o parâmetro recebido é como presente ou não
                tbs380.FL_CONFIR_AGEND = (CO_TIPO_ALTERACAO == "P" || CO_TIPO_ALTERACAO == "R" ? (flagSim ? "S" : "N") : null);

                TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs380, true);

                if (CO_TIPO_ALTERACAO == "P" || CO_TIPO_ALTERACAO == "R")
                {
                    TBS372_AGEND_AVALI tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgenda);
                    tbs372.FL_CONFIR_AGEND = (flagSim ? "S" : "N");

                    //Realiza esses processos apenas se a alteração no registro for do tipo de PRESENÇA
                    #region Se for uma alteração da Presença

                    if (flagSim) //Se for presença SIM, grava as informações pertinentes
                    {
                        if (CO_TIPO_ALTERACAO == "P")
                        {
                            tbs372.DT_PRESE = DateTime.Now;
                            tbs372.CO_COL_PRESE = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_PRESE = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.CO_EMP_PRESE = LoginAuxili.CO_EMP;
                            tbs372.IP_PRESE = Request.UserHostAddress;
                        }
                        else
                        {
                            tbs372.CO_SITUA = "R";
                            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.IP_SITUA = Request.UserHostAddress;
                            tbs372.DT_SITUA = DateTime.Now;

                            tbs372.DT_ENCAM = DateTime.Now;
                            tbs372.CO_COL_ENCAM = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_ENCAM = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.CO_EMP_ENCAM = LoginAuxili.CO_EMP;
                            tbs372.IP_ENCAM = Request.UserHostAddress;
                        }
                    }
                    else //Se for presença NÃO, grava os campos de presença como NULL
                    {
                        if (CO_TIPO_ALTERACAO == "P")
                        {
                            tbs372.DT_PRESE = (DateTime?)null;
                            tbs372.CO_COL_PRESE =
                            tbs372.CO_EMP_COL_PRESE =
                            tbs372.CO_EMP_PRESE = (int?)null;
                            tbs372.IP_PRESE = null;
                        }
                        else
                        {
                            tbs372.CO_SITUA = "A";
                            tbs372.DT_SITUA = DateTime.Now;
                            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs372.IP_SITUA = Request.UserHostAddress;

                            tbs372.DT_ENCAM = (DateTime?)null;
                            tbs372.CO_COL_ENCAM =
                            tbs372.CO_EMP_COL_ENCAM =
                            tbs372.CO_EMP_ENCAM = (int?)null;
                            tbs372.IP_ENCAM = null;
                        }
                    }

                    #endregion

                    TBS372_AGEND_AVALI.SaveOrUpdate(tbs372, true);
                }

                #endregion
            }
        }

        private void SalvarAtualizacaoCadastro(string tpAtualiz)
        {
            TBS372_AGEND_AVALI tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaAvalicao.Value));

            switch (tpAtualiz)
            {
                case "CadAlu":
                    tbs372.FL_ST_CAD_ALU = "S";
                    break;
                case "CadRes":
                    tbs372.FL_ST_CAD_RSP = "S";
                    break;
                case "SlcAvl":
                    tbs372.FL_ST_SLC_AVL = "S";
                    break;
                case "RegItn":
                    tbs372.FL_ST_REG_INF = "S";
                    break;
                default:
                    break;
            }

            TBS372_AGEND_AVALI.SaveOrUpdate(tbs372, true);

            CarregaAgendamentoAvaliacoes();
        }

        #endregion

        #region Funções de Campo

        protected void imgSituacao_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;

            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgSituacao");
                    if (img.ClientID == atual.ClientID)
                    {
                        int paciente = int.Parse(((HiddenField)linha.FindControl("hidCoPac")).Value);
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where tb07.CO_ALU == paciente
                                   select new { tb07.NO_ALU, tb07.CO_SEXO_ALU, tb07.DT_NASC_ALU }).FirstOrDefault();

                        //Atribui as informações da linha clicada aos campos correspondentes na modal
                        txtNomePaciMODLOG.Text = res.NO_ALU;
                        txtSexoMODLOG.Text = res.CO_SEXO_ALU;

                        #region Calculo de idade

                        //Calcula a idade do paciente
                        DateTime dataNasc = Convert.ToDateTime(res.DT_NASC_ALU);
                        int anoNasc = int.Parse(dataNasc.ToString("yyyy"));
                        int mesNasc = int.Parse(dataNasc.ToString("MM"));
                        int diaNasc = int.Parse(dataNasc.ToString("dd"));

                        int anoAtual = DateTime.Now.Year;
                        int mesAtual = DateTime.Now.Month;
                        int diaAtual = DateTime.Now.Day;

                        int anoIdade = anoAtual - anoNasc;

                        if (mesAtual < mesNasc)
                        {
                            anoIdade = anoIdade - 1;
                        }
                        if (mesAtual == mesNasc)
                        {
                            if (diaAtual < diaNasc)
                            {
                                anoIdade = anoIdade - 1;
                            }
                        }
                        #endregion

                        txtIdadeMODLOG.Text = Convert.ToString(anoIdade);

                        AbreModalPadrao("AbreModalLog();");
                    }
                }
            }
        }

        protected void ddlOperadoraPacie_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanoPacie, ddlOperadoraPacie, false);
            ControlaTabs("ICR");
        }

        protected void ddlPlanoPacie_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoriaPacie, ddlPlanoPacie, false);
            ControlaTabs("ICR");
        }

        protected void ddlLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConsultasAgendadas();
        }

        protected void imgPesqAgendaAtendimento_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(IniPeri.Text) && !String.IsNullOrEmpty(FimPeri.Text))
            {
                var inicio = DateTime.Parse(IniPeri.Text);
                var fim = DateTime.Parse(FimPeri.Text);

                if (inicio > fim)
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período final não pode ser menor que o inicial!");
                else
                {
                    var t = fim - inicio;

                    if (t.Days <= 30 || (!String.IsNullOrEmpty(txtNomePacPesqAgendAtend.Text) || !String.IsNullOrEmpty(txtNomeProfPesqAtend.Text)))
                        CarregaConsultasAgendadas();
                    else
                        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período não pode ser maior do que 30 dias!");
                }
            }
        }

        protected void imgPesqAgendaAval_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDtIniAgendaAval.Text) && !String.IsNullOrEmpty(txtDtFimAgendaAval.Text))
            {
                var inicio = DateTime.Parse(txtDtIniAgendaAval.Text);
                var fim = DateTime.Parse(txtDtFimAgendaAval.Text);

                if (inicio > fim)
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período final não pode ser menor que o inicial!");
                else
                {
                    var t = fim - inicio;

                    if (t.Days <= 46 || !String.IsNullOrEmpty(txtNomePacPesqAgendAvaliacao.Text))
                        CarregaAgendamentoAvaliacoes();
                    else
                        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período não pode ser maior do que 45 dias!");
                }
            }
        }

        protected void chkselect2_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdMedicosPlanto.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                        chk.Checked = false;
                    else
                    {
                        if (chk.Checked)
                            hidCoCol.Value = (((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                        else
                            hidCoCol.Value = "";
                    }
                }
            }

            /*if (!String.IsNullOrEmpty(hidCoCol.Value))
                ControlaTabs("RGI");*/
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade(ddlCidade, ddlUF);
            ddlCidade.Focus();
            ControlaTabs("ICR");
        }

        protected void ddlUfMODAL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade(ddlCidadeMODAL, ddlUfMODAL);
            ddlCidadeMODAL.Focus();

            AbreModalInfosCadastrais();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro(ddlUF, ddlCidade, ddlBairro);
            ddlBairro.Focus();
            ControlaTabs("ICR");
        }

        protected void ddlCidadeMODAL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro(ddlUfMODAL, ddlCidadeMODAL, ddlBairroMODAL);
            ddlBairroMODAL.Focus();

            AbreModalInfosCadastrais();
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
                    carregaCidade(ddlCidade, ddlUF);
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro(ddlUF, ddlCidade, ddlBairro);
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
            ControlaTabs("ICR");
        }

        protected void imgPesqCEPMODAL_OnClick(object sender, EventArgs e)
        {
            if (txtCEPPaciMODAL.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEPPaciMODAL.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograMODAL.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfMODAL.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    carregaCidade(ddlCidadeMODAL, ddlUfMODAL);
                    ddlCidadeMODAL.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro(ddlUfMODAL, ddlCidadeMODAL, ddlBairroMODAL);
                    ddlBairroMODAL.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLograMODAL.Text = txtLograMODALAUXILIAR.Text = "";
                    ddlBairroMODAL.SelectedValue = "";
                    ddlCidadeMODAL.SelectedValue = "";
                    ddlUfMODAL.SelectedValue = "";
                }
            }
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            chkPaciMoraCoResp.Checked = chkPaciEhResp.Checked;

            ControlaTabs("ICR");
        }

        protected void chkPaciEhRespMODAL_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPaciEhRespMODAL.Checked)
                chkPaciMoraCoRespMODAL.Checked = true;

            carregaPaciehoResponsavelMODAL();
            AbreModalInfosCadastrais();
        }

        protected void ddlObitoMae_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Se tiver alguma responsabilidade marcada, avisa 
            if (ddlObitoMae.SelectedValue == "S")
            {
                if (chkMaeResp.Checked)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor desmarcar que a mãe é a Responsável pelo Acompanhamento para poder alterar para óbito");
                    chkMaeResp.Focus();
                    ddlObitoMae.SelectedValue = "N";
                    return;
                }

                if (chkMaeRespFinanc.Checked)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor desmarcar que a mãe é a Responsável Financeira para poder alterar para óbito");
                    chkMaeRespFinanc.Focus();
                    ddlObitoMae.SelectedValue = "N";
                    return;
                }
            }
            chkMaeResp.Enabled = chkMaeRespFinanc.Enabled = (ddlObitoMae.SelectedValue == "S" ? false : true);

            ControlaTabs("ICA");
        }

        protected void ddlObitoPai_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Se tiver alguma responsabilidade marcada, avisa 
            if (ddlObitoPai.SelectedValue == "S")
            {
                if (chkPaiResp.Checked)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor desmarcar que o Pai é o Responsável pelo Acompanhamento para poder alterar para óbito");
                    chkPaiResp.Focus();
                    ddlObitoPai.SelectedValue = "N";
                    return;
                }

                if (chkMaeRespFinanc.Checked)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor desmarcar que o Pai é o Responsável Financeira para poder alterar para óbito");
                    chkMaeRespFinanc.Focus();
                    ddlObitoPai.SelectedValue = "N";
                    return;
                }
            }
            chkPaiResp.Enabled = chkPaiRespFinanc.Enabled = (ddlObitoPai.SelectedValue == "S" ? false : true);

            ControlaTabs("ICA");
        }

        protected void chkMaeRespFinanc_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPaiRespFinanc.Checked)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Só pode haver um responsável financeiro");
                chkMaeRespFinanc.Checked = false;
                chkPaiRespFinanc.Focus();
                return;
            }

            ControlaTabs("ICA");
        }

        protected void chkPaiRespFinanc_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkMaeRespFinanc.Checked)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Só pode haver um responsável financeiro");
                chkPaiRespFinanc.Checked = false;
                chkMaeRespFinanc.Focus();
                return;
            }

            ControlaTabs("ICA");
        }

        protected void lnkConfirmarInfoCadas_OnClick(object sender, EventArgs e)
        {
            bool erros = false;

            #region Validações

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            if (txtNuProntuario.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Número do PRONTUÁRIO do Paciente é Requerido"); erros = true; }

            if (txtnompac.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Paciente é Requerido"); erros = true; }

            if (ddlSituacaoAlu.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "A Situação do Paciente é Requerida"); erros = true; }

            if (txtApelidoPaciente.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Apelido do Paciente é Requerido"); erros = true; }

            if (ddlSexoPaci.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }

            if (txtDtNascPaci.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }

            if (txtNomeMae.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome da Mãe do Paciente é Requerido"); erros = true; }

            if (txtNomePai.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Pai do Paciente é Requerido"); erros = true; }

            //if ((txtAnoAtiv.Text == "") || (txtMesAtiv.Text == ""))
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Ano e Mês de entrada do Paciente são Requeridos"); erros = true; }

            #endregion

            if (erros != true)
            {
                #region Persistências

                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoAlu.Value));

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                #region Bloco foto
                int codImagem = upImagemAluno.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion
                tb07.NU_NIRE = int.Parse(txtNuProntuario.Text);
                tb07.NU_CPF_ALU = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();
                tb07.NU_NIS = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
                tb07.NO_ALU = txtnompac.Text.ToUpper();
                tb07.CO_SITU_ALU = ddlSituacaoAlu.SelectedValue;
                tb07.NO_APE_ALU = txtApelidoPaciente.Text.ToUpper();
                tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                tb07.CO_ORIGEM_ALU = ddlOrigemPaci.SelectedValue;
                tb07.DE_NATU_ALU = (!string.IsNullOrEmpty(txtNaturalidade.Text) ? txtNaturalidade.Text : null);
                tb07.CO_ESTADO_CIVIL = ddlEstadoCivil.SelectedValue;
                tb07.NO_MAE_ALU = txtNomeMae.Text;
                tb07.FLA_OBITO_MAE = ddlObitoMae.SelectedValue;
                tb07.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlProfissaoNomeMae.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlProfissaoNomeMae.SelectedValue)) : null);
                tb07.NO_PAI_ALU = (!string.IsNullOrEmpty(txtNomePai.Text) ? txtNomePai.Text : null);
                tb07.FLA_OBITO_PAI = ddlObitoPai.SelectedValue;
                tb07.TBS366_FUNCAO_SIMPL1 = (!string.IsNullOrEmpty(ddlProfiPai.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlProfiPai.SelectedValue)) : null);
                tb07.CO_ANO_ORI = txtAnoAtiv.Text;
                tb07.CO_MES_REFER = (!string.IsNullOrEmpty(txtMesAtiv.Text) ? int.Parse(txtMesAtiv.Text) : (int?)null);
                tb07.NU_CARTAO_SAUDE = (!string.IsNullOrEmpty(txtNuCarSaude.Text) ? txtNuCarSaude.Text : null);
                tb07.TBS387_DEFIC = (!string.IsNullOrEmpty(ddlDeficienciaAlu.SelectedValue) ? TBS387_DEFIC.RetornaPelaChavePrimaria(int.Parse(ddlDeficienciaAlu.SelectedValue)) : null);
                tb07.TP_RACA = ddlEtniaAlu.SelectedValue;
                tb07.NU_ALTU = (!string.IsNullOrEmpty(txtAltura.Text) ? decimal.Parse(txtAltura.Text) : (decimal?)null);
                tb07.NU_PESO = (!string.IsNullOrEmpty(txtPeso.Text) ? decimal.Parse(txtPeso.Text) : (decimal?)null);
                tb07.TBS377_INDIC_PACIENTES = (!string.IsNullOrEmpty(ddlIndicacao.SelectedValue) ? TBS377_INDIC_PACIENTES.RetornaPelaChavePrimaria(int.Parse(ddlIndicacao.SelectedValue)) : null);
                tb07.CO_RG_ALU = (!string.IsNullOrEmpty(txtNumeroRGPaciente.Text) ? txtNumeroRGPaciente.Text : null);
                tb07.CO_ORG_RG_ALU = (!string.IsNullOrEmpty(txtOrgEmissRGPaciente.Text) ? txtOrgEmissRGPaciente.Text : null);
                tb07.CO_ESTA_RG_ALU = (!string.IsNullOrEmpty(ddlUFRGPaciente.SelectedValue) ? ddlUFRGPaciente.SelectedValue : null);
                tb07.NU_TELE_RESI_ALU = (!string.IsNullOrEmpty(txtTelResPaci.Text) ? txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
                tb07.NU_TELE_CELU_ALU = (!string.IsNullOrEmpty(txtTelCelPaci.Text) ? txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
                tb07.NU_TELE_COME_ALU = (!string.IsNullOrEmpty(txtTeComePaci.Text) ? txtTeComePaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
                tb07.NU_TELE_WHATS_ALU = (!string.IsNullOrEmpty(txtWhatsPaci.Text) ? txtWhatsPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
                tb07.NO_FACEB_PACI = (!string.IsNullOrEmpty(txtFacePaci.Text) ? txtFacePaci.Text : null);
                tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;

                tb07.NU_TEL_MAE = (!string.IsNullOrEmpty(txtTelMae.Text) ? txtTelMae.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim() : null);
                tb07.NU_TEL_PAI = (!string.IsNullOrEmpty(txtTelPai.Text) ? txtTelPai.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim() : null);

                tb07.FL_MAE_RESP_FINAN = (chkMaeRespFinanc.Checked ? "S" : "N");
                tb07.FL_MAE_RESP_ATEND = (chkMaeResp.Checked ? "S" : "N");
                tb07.FL_PAI_RESP_FINAN = (chkPaiRespFinanc.Checked ? "S" : "N");
                tb07.FL_PAI_RESP_ATEND = (chkPaiResp.Checked ? "S" : "N");

                //Endereço
                //tb07.CO_CEP_ALU = txtCEP.Text;
                //tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                //tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                //tb07.DE_ENDE_ALU = txtLograEndResp.Text;

                //Salva os valores para os campos not null da tabela de Usuário
                tb07 = TB07_ALUNO.SaveOrUpdate(tb07);

                #endregion

                #endregion

                SalvarAtualizacaoCadastro("CadAlu");
            }
            else
                ControlaTabs("ICA");
        }

        protected void lnkConfirmarInfosCadasResponsavel_OnClick(object sender, EventArgs e)
        {
            bool erros = false;

            #region Validações

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeResp.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Responsável é Requerido"); erros = true; }

            if (ddlSexResp.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }

            if (txtDtNascResp.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }

            if (txtNuIDResp.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Número da Identidade do Responsável é Requerido"); erros = true; }

            if (txtCEP.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CEP do Endereço é Requerido"); erros = true; }

            if (ddlUF.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O UF do Endereço  é Requerida"); erros = true; }

            if (ddlCidade.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "A Cidade é Requerida"); erros = true; }

            if (ddlBairro.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Bairro é Requerido"); erros = true; }

            if (txtLograEndResp.Text == "")
            { AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Logradouro é Requerido"); erros = true; }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            var cpfValido = true;

            if (!String.IsNullOrEmpty(txtCPFResp.Text))
            {
                if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                    txtCPFResp.Focus();
                    erros = true;
                }
            }
            else if (tb25.FL_CPF_RESP_OBRIGATORIO == "S")
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do responsável é obrigatório");
                txtCPFResp.Focus();
                erros = true;
            }
            else if (tb25.FL_CPF_RESP_OBRIGATORIO == "N" && String.IsNullOrEmpty(txtCPFResp.Text))
            {
                if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    var cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    //Enquanto existir, calcula um novo cpf
                    while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado || w.NU_CONTROLE == cpfGerado).Any())
                        cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    txtCPFResp.Text = cpfGerado;
                }
                cpfValido = false;
            }

            #endregion

            if (erros != true)
            {
                var tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value));

                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                var cpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");

                if (tb108.NU_CONTROLE != cpfResp && (cpfValido || (!cpfValido && String.IsNullOrEmpty(tb108.NU_CONTROLE))))
                    tb108.NU_CONTROLE = cpfResp;

                tb108.CO_CEP_RESP = txtCEP.Text;
                tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                tb108.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                tb108.NO_RESP = txtNomeResp.Text.ToUpper();
                tb108.NU_CPF_RESP = cpfResp;
                tb108.FL_CPF_VALIDO = cpfValido ? "S" : "N";
                tb108.CO_RG_RESP = txtNuIDResp.Text;
                tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                tb108.CO_CEP_RESP = txtCEP.Text;
                tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);

                #endregion

                #region Altera o paciente com os dados de Plano

                if (!string.IsNullOrEmpty(hidCoAlu.Value))
                {
                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoAlu.Value));
                    tb07.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadoraPacie.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadoraPacie.SelectedValue)) : null);
                    tb07.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlanoPacie.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanoPacie.SelectedValue)) : null);
                    tb07.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCategoriaPacie.SelectedValue) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCategoriaPacie.SelectedValue)) : null);
                    tb07.NU_PLANO_SAUDE = (!string.IsNullOrEmpty(txtNuPlanoPacie.Text) ? txtNuPlanoPacie.Text : null);
                    tb07.DT_VENC_PLAN = !String.IsNullOrEmpty(txtVencPlano.Text) ? DateTime.Parse(txtVencPlano.Text) : (DateTime?)null;

                    if (chkPaciEhResp.Checked)
                    {
                        tb07.CO_RG_ALU = txtNuIDResp.Text;
                        tb07.CO_ORG_RG_ALU = txtOrgEmiss.Text;
                        tb07.CO_ESTA_RG_ALU = ddlUFOrgEmis.SelectedValue;
                        tb07.NO_WEB_ALU = txtEmailResp.Text;
                        tb07.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);

                        tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                        tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                        tb07.CO_CEP_ALU = txtCEP.Text;
                        tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                    }

                    TB07_ALUNO.SaveOrUpdate(tb07, true);
                }

                #endregion

                SalvarAtualizacaoCadastro("CadRes");
            }
            else
                ControlaTabs("ICR");
        }

        protected void lnkConfirmarInfoCadasMODAL_OnClick(object sender, EventArgs e)
        {
            bool erros = false;

            #region Validações

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            if (ddlSexoPaciMODAL.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }

            if (txtDtNascPaciMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }

            if (txtNuProntuMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do PRONTUÁRIO do Paciente é Requerido"); erros = true; }

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeRespMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }

            if (txtCPFRespMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }

            if (ddlSexoRespMODAL.SelectedValue == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }

            if (txtDtNascRespMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }

            if (txtNuRGRespMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Número da Identidade do Responsável é Requerido"); erros = true; }

            if (txtCEPPaciMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do Endereço é Requerido"); erros = true; }

            if (ddlUfMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O UF do Endereço  é Requerida"); erros = true; }

            if (ddlCidadeMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade é Requerida"); erros = true; }

            if (ddlBairroMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro é Requerido"); erros = true; }

            if (txtLograMODAL.Text == "")
            { AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro é Requerido"); erros = true; }

            #endregion

            if (erros != true)
            {
                #region Persistências

                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPacModal.Value));
                TB108_RESPONSAVEL tb108;

                if (!string.IsNullOrEmpty(hidCoRespModal.Value)) // Se houver responsável, instancia o objeto da entidade
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoRespModal.Value));
                else // Se não houver responsável, instancia um novo objeto da entidade
                    tb108 = new TB108_RESPONSAVEL();

                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                tb108.CO_CEP_RESP = txtCEPPaciMODAL.Text;
                tb108.CO_ESTA_RESP = ddlUfMODAL.SelectedValue;
                tb108.CO_BAIRRO = int.Parse(ddlBairroMODAL.SelectedValue);
                tb108.CO_CIDADE = int.Parse(ddlCidadeMODAL.SelectedValue);
                tb108.DE_ENDE_RESP = txtLograMODAL.Text;
                //tb108.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                tb108.NO_RESP = txtNomeRespMODAL.Text.ToUpper();
                tb108.NU_CPF_RESP = txtCPFRespMODAL.Text.Replace("-", "").Replace(".", "").Trim();
                tb108.CO_RG_RESP = txtNuRGRespMODAL.Text;
                tb108.CO_ORG_RG_RESP = txtORGEmissRespMODAL.Text;
                tb108.CO_ESTA_RG_RESP = ddlUFRespMODAL.SelectedValue;
                tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascRespMODAL.Text);
                tb108.CO_SEXO_RESP = ddlSexoRespMODAL.SelectedValue;
                tb108.DES_EMAIL_RESP = txtEmailRespMODAL.Text;
                tb108.NU_TELE_CELU_RESP = txtTelCelRespMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelFixoRespMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_WHATS_RESP = txtNuWhatsPaciMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_COME_RESP = txtTelComRespMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                #region Bloco foto
                int codImagem = updImagePacienteMODAL.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion

                tb07.CO_ORIGEM_ALU = ddlOrigemPaciMODAL.SelectedValue;
                tb07.NO_ALU = txtNomePaciMODAL.Text.ToUpper();
                tb07.NU_NIRE = int.Parse(txtNuProntuMODAL.Text);
                tb07.NU_CPF_ALU = txtCPFPaciMODAL.Text.Replace(".", "").Replace("-", "").Trim();
                tb07.NU_NIS = (!string.IsNullOrEmpty(txtSUSPaciMODAL.Text) ? decimal.Parse(txtSUSPaciMODAL.Text) : (decimal?)null);
                tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaciMODAL.Text);
                tb07.CO_SEXO_ALU = ddlSexoPaciMODAL.SelectedValue;
                tb07.NU_TELE_CELU_ALU = txtCelPaciMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_RESI_ALU = txtFixPaciMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_WHATS_ALU = txtNuWhatsPaciMODAL.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.CO_GRAU_PAREN_RESP = ddlGrauParenMODAL.SelectedValue;
                tb07.NO_EMAIL_PAI = txtEmailMODAL.Text;
                tb07.TP_RACA = ddlEtniaPaciMODAL.SelectedValue != "" ? ddlEtniaPaciMODAL.SelectedValue : null;
                tb07.DE_PASTA_CONTR = txtPastaControl.Text;
                tb07.TEL_MIGRAR = txtTelMigrado.Text;
                //Endereço
                tb07.CO_CEP_ALU = txtCEPPaciMODAL.Text;
                tb07.CO_ESTA_ALU = ddlUfMODAL.SelectedValue;
                tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroMODAL.SelectedValue));
                tb07.DE_ENDE_ALU = txtLograMODAL.Text;

                //Salva os valores para os campos not null da tabela de Usuário
                tb07.NU_NIRE = int.Parse(txtNuProntuMODAL.Text);

                tb07 = TB07_ALUNO.SaveOrUpdate(tb07);

                #endregion

                #endregion

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Alterações nas informações cadastrais do paciente e responsável salvas com êxito!");
                CarregaConsultasAgendadas();
            }
        }

        protected void lnkCadastroCompleto_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("3106_CadastramentoUsuariosSimp/Busca.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                this.Session[SessoesHttp.URLOrigem] = HttpContext.Current.Request.Url.PathAndQuery;
                this.Session[SessoesHttp.CodigoMatriculaAluno] = hidCoPacModal.Value;

                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo.Replace("Busca", "Cadastro"), HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
            }
        }

        protected void lnkConfirmarAvaliador_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidIdAgendaAvalicao.Value))
            {
                try
                {
                    TBS372_AGEND_AVALI tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaAvalicao.Value));

                    var _tbs460 = TBS460_AGEND_AVALI_PROFI.RetornaTodosRegistros().Where(x => x.TBS372_AGEND_AVALI.ID_AGEND_AVALI == tbs372.ID_AGEND_AVALI).ToList();
                    foreach (var item in _tbs460)
                    {
                        TBS460_AGEND_AVALI_PROFI.Delete(item, true);
                    }
                    int count = 0;
                    foreach (GridViewRow row in grdMedicosPlanto.Rows)
                    {
                        bool chk = ((CheckBox)row.Cells[0].FindControl("chkProfAvali")).Checked;
                        if (chk)
                        {
                            string _coCol = ((HiddenField)row.Cells[0].FindControl("hidcoCol")).Value;
                            string _coEmp = ((HiddenField)row.Cells[0].FindControl("hidcoEmpColPla")).Value;
                            var tbs460 = new TBS460_AGEND_AVALI_PROFI();
                            tbs460.CO_COL_AVALI = int.Parse(_coCol);
                            tbs460.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs460.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs460.CO_EMP_AVALI = int.Parse(_coEmp);
                            tbs460.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs460.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs460.CO_SITUA = "A";
                            tbs460.DT_CADAS = DateTime.Now;
                            tbs460.DT_SITUA = DateTime.Now;
                            tbs460.IP_CADAS = Request.UserHostAddress;
                            tbs460.TBS372_AGEND_AVALI = tbs372;
                            TBS460_AGEND_AVALI_PROFI.SaveOrUpdate(tbs460, true);
                            count += 1;
                        }
                    }
                    if (count == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum avaliador selecionado!");
                    }
                    else
                    {
                        SalvarAtualizacaoCadastro("SlcAvl");
                        AuxiliPagina.EnvioMensagemSucesso(this.Page, "Registro alterado com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                }
            }
        }

        protected void imgInfosPlano_OnClick(object sender, EventArgs e)
        {

            CarregaOperadoras();
            CarregaPlanos();
            CarregaCategorias();
            hidIndexGridRefer.Value = txtNuPlano.Text = txtNuGuia.Text = txtNuAutorizacao.Text = "";

            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[0].FindControl("imgInfosPlano");

                    if (img.ClientID == atual.ClientID)
                    {
                        #region Coleta os valores na grid

                        string idOper = (((HiddenField)linha.Cells[0].FindControl("hidIdOper")).Value);
                        string idPlan = (((HiddenField)linha.Cells[0].FindControl("hidIdPlano")).Value);
                        string idCateg = (((HiddenField)linha.Cells[0].FindControl("hidIdCateg")).Value);
                        string nuPlan = (((HiddenField)linha.Cells[0].FindControl("hidNuPlano")).Value);
                        string nuGuia = (((HiddenField)linha.Cells[0].FindControl("hidNuGuia")).Value);
                        string nuAutor = (((HiddenField)linha.Cells[0].FindControl("hidNuAutor")).Value);

                        #endregion

                        #region Seta as informações na modal

                        //Se tiver operadora
                        if (!string.IsNullOrEmpty(idOper))
                        {
                            ddlOperadora.SelectedValue = idOper;
                            CarregaPlanos();

                            //Se tiver plano
                            if (!string.IsNullOrEmpty(idPlan))
                            {
                                ddlPlano.SelectedValue = idPlan;
                                CarregaCategorias();

                                //Se tiver categoria
                                if (!string.IsNullOrEmpty(idCateg))
                                    ddlCategoria.SelectedValue = idCateg;
                            }
                        }
                        hidIndexGridRefer.Value = linha.RowIndex.ToString();
                        txtNuPlano.Text = nuPlan;
                        txtNuGuia.Text = nuGuia;
                        txtNuAutorizacao.Text = nuAutor;

                        #endregion

                        AbreModalInfosPlanos();
                    }
                }
            }
        }

        protected void imgPresente_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgPresente");
                    if (img.ClientID == atual.ClientID)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoPac")).Value);
                        ImageButton imgCancel = (ImageButton)linha.FindControl("imgCancelar");
                        ImageButton imgEncami = (ImageButton)linha.FindControl("imgEncam");
                        bool flFaltasConsec = bool.Parse(((HiddenField)linha.FindControl("hidFaltasConsec")).Value);

                        var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                   join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                                   where tb25.CO_EMP == LoginAuxili.CO_EMP
                                   select new
                                   {
                                       tb83.FL_PERM_ATEND_TRIAGEM
                                   }).FirstOrDefault();

                        //Se não estiver confirmado, confirma, gera o log e altera a imagem
                        if (caminho == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                        {
                            //SalvaLogAlteracaoStatusAgenda(idAgenda, "P", true, EObjetoLogAgenda.paraAtendimento);
                            //img.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                            //imgCancel.Enabled = false;
                            //imgEncami.Enabled = true;

                            //lblTresFaltasAnteriores.Visible = false;

                            //if (flFaltasConsec)
                            //{
                            //    lblTresFaltasAnteriores.Visible = true;

                            //    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                            //    tb07.FL_FALTOSO = "N";

                            //    TB07_ALUNO.SaveOrUpdate(tb07, true);
                            //}

                            #region Responsável por prepara opção de já realizar encaminhamento

                            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                (res.FL_PERM_ATEND_TRIAGEM == "S" ? "AbreProximoPasso();" : "AbreModalAtend();"),
                                true
                            );
                            carregaLocalAtendiM3();
                            hidIndexGridAtend.Value = linha.RowIndex.ToString();

                            #endregion
                        }
                        else //Se estiver confirmado, desconfirma, gera o log e altera a imagem
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "P", false, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_PacienteNaoChegou.ico";
                            imgCancel.Enabled = true;
                            imgEncami.Enabled = false;
                        }
                    }
                }
            }

            CarregaConsultasAgendadas();
        }

        protected void imgEncamPre_OnClick(object sender, EventArgs e)
        {
            var imgb = (ImageButton)sender;
            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgEncam");
                    if (img.ClientID == imgb.ClientID)
                    {
                        hidIndexGridAtend.Value = linha.RowIndex.ToString();
                        var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                   join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                                   where tb25.CO_EMP == LoginAuxili.CO_EMP
                                   select new
                                   {
                                       tb83.FL_PERM_ATEND_TRIAGEM
                                   }).FirstOrDefault();

                        if (res.FL_PERM_ATEND_TRIAGEM == "S")
                        {
                            if (imgb.ImageUrl == "/Library/IMG/PGS_IC_EncaminharIn.png")
                            {
                                imgEncam_OnClick(sender, e);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(
                                    this.Page,
                                    this.GetType(),
                                    "Acao",
                                    "AbreModalEncaminhamento()",
                                    true
                                );
                            }
                        }
                        else
                        {
                            imgEncam_OnClick(sender, e);
                        }
                    }
                }
            }
        }

        protected void imgEncam_OnClick(object sender, EventArgs e)
        {
            var imgb = (ImageButton)sender;
            imgb.OnClientClick = "if (!window.confirm('Tem certeza de que deseja alterar o status de Encaminhamento?')) return false;";

            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgEncam");
                    if (img.ClientID == atual.ClientID)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        ImageButton imgCancel = (ImageButton)linha.FindControl("imgCancelar");
                        ImageButton imgPresen = (ImageButton)linha.FindControl("imgPresente");

                        //Se não estiver encaminhado, ENCAMINHA, gera o log e altera a imagem
                        if (caminho == "/Library/IMG/PGS_IC_EncaminharOut.png")
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "E", true, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_IC_EncaminharIn.png";

                            //Se estiver encaminhado, não faz sentido alterar para cancelado ou que não veio
                            imgCancel.Enabled = imgPresen.Enabled = false;
                        }
                        else //Se estiver encaminhado, DESENCAMINHA, gera o log e altera a imagem
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "E", false, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_IC_EncaminharOut.png";
                            imgCancel.Enabled = true;

                            //Se não estiver encaminhado, permite alterar para cancelado ou que não veio
                            imgPresen.Enabled = true;

                            //Só libera o botão de cancelamento, se o paciente ainda estiver com status de não chegou
                            if (imgPresen.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                                imgCancel.Enabled = true;
                            else
                                imgCancel.Enabled = false;
                        }
                    }
                }
            }

            CarregaConsultasAgendadas();
        }

        protected void imgCancelar_OnClick(object sender, EventArgs e)
        {
            divCance.Visible = divDesCance.Visible = false;
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgCancelar");
                    ImageButton imgPresenca = (ImageButton)linha.FindControl("imgPresente");

                    if (img.ClientID == atual.ClientID)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        hidIdAgendaCancel.Value = idAgenda.ToString();
                        hidTipoAgenda.Value = "AT";

                        var ag = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);

                        if (ag.CO_SITUA_AGEND_HORAR != "C")
                            divCance.Visible = true;
                        else
                            divDesCance.Visible = true;

                        AbreModalCancelamentoAgenda();
                    }
                }
            }
        }

        protected void imgInfosCadasPac_OnClick(object sender, EventArgs e)
        {
            EditarInfoCadastrais((ImageButton)sender, "imgInfosCadasPac", "AbreModalInfosCadasPac();");
        }

        protected void imgInfosCadasRes_OnClick(object sender, EventArgs e)
        {
            EditarInfoCadastrais((ImageButton)sender, "imgInfosCadasRes", "AbreModalInfosCadasResp();");
        }

        protected void imgSelAvaliador_OnClick(object sender, EventArgs e)
        {
            var imgAtual = (ImageButton)sender;
            List<int> ids = new List<int>();
            List<int> coCol = new List<int>();
            int idAgendAval = 0;

            foreach (GridViewRow linha in grdAgendaAvaliacoes.Rows)
            {
                ImageButton img = (ImageButton)linha.FindControl("imgSelAvaliador");

                if (img.ClientID == imgAtual.ClientID)
                {
                    idAgendAval = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidAgendaAval")).Value);
                    hidIdAgendaAvalicao.Value = ((HiddenField)linha.Cells[0].FindControl("hidAgendaAval")).Value;
                }
            }

            var tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgendAval);

            if (tbs372 != null)
            {
                var tbs460 = TBS460_AGEND_AVALI_PROFI.RetornaTodosRegistros().Where(x => x.TBS372_AGEND_AVALI.ID_AGEND_AVALI == tbs372.ID_AGEND_AVALI).ToList();
                foreach (var item in tbs460)
                {
                    ids.Add(item.ID_AGEND_AVALI_PROFI);
                    coCol.Add(item.CO_COL_AVALI);
                }
            }

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FL_PROFI_AVALI == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       select new MedicosAvali
                       {
                           co_col = tb03.CO_COL,
                           check = coCol.Contains(tb03.CO_COL) ? true : false,
                           _NO_COL = tb03.NO_COL,
                           //colaborador = tb03.CO_COL,
                           _co_emp_col_pla = tb03.CO_EMP,
                           _CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           _CO_ESPEC = tb03.CO_ESPEC,
                           _TELEFONE = tb03.NU_TELE_CELU_COL,
                       }).OrderBy(w => w._NO_COL).ToList();

            if (res.Count > 0)
            {
                grdMedicosPlanto.DataSource = res;
                grdMedicosPlanto.DataBind();
            }
            else
            {
                grdMedicosPlanto.DataSource = null;
                grdMedicosPlanto.DataBind();
            }

            AbreModalPadrao("AbreModalSelAvaliador();");
        }

        public class MedicosAvali
        {
            public bool check { get; set; }
            public int co_col { get; set; }
            public string _NO_COL { get; set; }
            public string NO_COL
            {
                get
                {
                    return (this._NO_COL.Length > 60 ? this._NO_COL.Substring(0, 60) + "..." : this._NO_COL);
                }
            }
            //public int colaborador { get; set; }
            public int _co_emp_col_pla { get; set; }
            public string _CO_CLASS_PROFI { get; set; }
            public string _NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this._CO_CLASS_PROFI);
                }
            }
            public int? _CO_ESPEC { get; set; }
            public string _CO_TIPO_RISCO { get; set; }

            public string _TELEFONE { get; set; }
            public string _TELEFONE_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this._TELEFONE) ? AuxiliFormatoExibicao.PreparaTelefone(this._TELEFONE) : " - ");
                }
            }
        }

        protected void imgRegisItens_OnClick(object sender, EventArgs e)
        {
            EditarInfoCadastrais((ImageButton)sender, "imgRegisItens", "AbreModalRegisItens();");
        }

        private void EditarInfoCadastrais(ImageButton imgAtual, string controle, string funcao)
        {
            if (grdAgendaAvaliacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaAvaliacoes.Rows)
                {
                    ImageButton img = (ImageButton)linha.FindControl(controle);

                    if (img.ClientID == imgAtual.ClientID)
                    {
                        var coAlu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAlu")).Value);
                        var coResp = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoResp")).Value);
                        var idAgendAval = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidAgendaAval")).Value);

                        PesquisaCarregaPaci(coAlu, coResp);
                        carregaGridSolicitacoes(idAgendAval);
                        hidCoAlu.Value = coAlu.ToString();
                        hidCoResp.Value = coResp.ToString();
                        hidIdAgendaAvalicao.Value = idAgendAval.ToString();
                        IDAGENDA.Value = idAgendAval.ToString();
                        AbreModalPadrao(funcao);
                    }
                }
            }
        }

        protected void imgPresenteAA_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendaAvaliacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaAvaliacoes.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgPresenteAA");
                    if (img.ClientID == atual.ClientID)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidAgendaAval")).Value);

                        SalvaLogAlteracaoStatusAgenda(idAgenda, "P", (img.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico"), EObjetoLogAgenda.paraAvaliacao);

                        var tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgenda);

                        if (tbs372 != null && tbs372.FL_TIPO_AGENDA == "P" && img.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                        {
                            hidAgendSelec.Value = idAgenda.ToString();

                            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                "AbreModalEncamAtend();",
                                true
                            );
                        }
                    }
                }
            }

            CarregaAgendamentoAvaliacoes();
        }

        protected void imgEncamAA_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendaAvaliacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaAvaliacoes.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgEncamAA");
                    if (img.ClientID == atual.ClientID)
                    {
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoAlu")).Value);
                        int coResp = int.Parse(((HiddenField)linha.FindControl("hidCoResp")).Value);
                        string idAgendAval = ((HiddenField)linha.FindControl("hidAgendaAval")).Value;

                        hidCoAlu.Value = coAlu.ToString();
                        hidCoResp.Value = coResp.ToString();
                        hidIdAgendaAvalicao.Value = idAgendAval;

                        var tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(idAgendAval));

                        if (tbs372 != null && tbs372.FL_TIPO_AGENDA == "P")
                            SalvaLogAlteracaoStatusAgenda(int.Parse(idAgendAval), "R", (img.ImageUrl == "/Library/IMG/PGS_IC_EncaminharOut.png"), EObjetoLogAgenda.paraAvaliacao);
                        else
                            FinalizarRecepcao();
                    }
                }
            }

            CarregaAgendamentoAvaliacoes();
        }

        protected void imgCancelarAA_OnClick(object sender, EventArgs e)
        {
            divCance.Visible = divDesCance.Visible = false;
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendaAvaliacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaAvaliacoes.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgCancelarAA");

                    if (img.ClientID == atual.ClientID)
                    {
                        int idAgendAval = int.Parse(((HiddenField)linha.FindControl("hidAgendaAval")).Value);
                        hidIdAgendaCancel.Value = idAgendAval.ToString();
                        hidTipoAgenda.Value = "AV";

                        var tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(idAgendAval);

                        if (tbs372.CO_SITUA != "C")
                            divCance.Visible = true;
                        else
                            divDesCance.Visible = true;

                        AbreModalCancelamentoAgenda();
                    }
                }
            }
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanos();
            AbreModalInfosPlanos();
        }

        protected void ddlPlano_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCategorias();
            AbreModalInfosPlanos();
        }

        protected void lnkConfirmarPlano_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidIndexGridRefer.Value))
            {
                //Percorre a grid de solicitações, guardando as informações preenchidas na modal
                foreach (GridViewRow li in grdSolicitacoes.Rows)
                {
                    if ((int.Parse(hidIndexGridRefer.Value)) == li.RowIndex)
                    {
                        HiddenField idOper = (((HiddenField)li.Cells[0].FindControl("hidIdOper")));
                        HiddenField idPlan = (((HiddenField)li.Cells[0].FindControl("hidIdPlano")));
                        HiddenField idCateg = (((HiddenField)li.Cells[0].FindControl("hidIdCateg")));
                        HiddenField nuPlan = (((HiddenField)li.Cells[0].FindControl("hidNuPlano")));
                        HiddenField nuGuia = (((HiddenField)li.Cells[0].FindControl("hidNuGuia")));
                        HiddenField nuAutor = (((HiddenField)li.Cells[0].FindControl("hidNuAutor")));

                        idOper.Value = ddlOperadora.SelectedValue;
                        idPlan.Value = ddlPlano.SelectedValue;
                        idCateg.Value = ddlCategoria.SelectedValue;
                        nuPlan.Value = txtNuPlano.Text;
                        nuGuia.Value = txtNuGuia.Text;
                        nuAutor.Value = txtNuAutorizacao.Text;

                        DropDownList ddlGrupoProc, ddlSubGrupo, ddlProc;
                        ddlGrupoProc = (DropDownList)li.Cells[1].FindControl("ddlGrupoProc");
                        ddlSubGrupo = (DropDownList)li.Cells[2].FindControl("ddlSubGrupo");
                        ddlProc = (DropDownList)li.Cells[3].FindControl("ddlProcedimento");

                        CarregarProcedimentosMedicos(ddlProc, ddlGrupoProc, ddlSubGrupo, idOper.Value);
                    }
                }
                ControlaTabs("RGI");
            }
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlProc;
            string idOper, idPlan;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    idOper = ((HiddenField)linha.Cells[0].FindControl("hidIdOper")).Value;
                    idPlan = ((HiddenField)linha.Cells[0].FindControl("hidIdPlano")).Value;
                    ddlProc = (DropDownList)linha.Cells[3].FindControl("ddlProcedimento");
                    HiddenField hidValorUnitario = (HiddenField)linha.Cells[3].FindControl("hidValUnitProc");
                    //textbox que vai receber valor calculado
                    TextBox txtValor = (TextBox)linha.Cells[5].FindControl("txtVlUnitario");

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    //if (ddlProc.ClientID == atual.ClientID)
                    CalcularPreencherValoresTabelaECalculado(ddlProc, idOper, idPlan, hidValorUnitario);

                    TextBox txtqtde = (TextBox)linha.Cells[4].FindControl("txtQtde");
                    //Quantidade de sessões
                    int qt = (!string.IsNullOrEmpty(txtqtde.Text) ? int.Parse(txtqtde.Text) : 0);
                    CalculaValorTotalProcedimento(qt, hidValorUnitario.Value, txtValor);
                    CarregarValoresTotaisFooter();
                }
            }

            ControlaTabs("RGI");
        }

        protected void imgInfos_OnClick(object sender, EventArgs e)
        {
            txtCodProc.Text = txtNomeProc.Text = txtObservacaoProc.Text = txtVlUnitProc.Text = "";

            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[5].FindControl("imgInfos");

                    if (img.ClientID == atual.ClientID)
                    {
                        string vlUnit = ((HiddenField)linha.Cells[3].FindControl("hidValUnitProc")).Value;
                        string procSelec = ((DropDownList)linha.Cells[3].FindControl("ddlProcedimento")).SelectedValue;

                        if (!string.IsNullOrEmpty(procSelec))
                        {
                            int proc = int.Parse(procSelec);
                            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                       where tbs356.ID_PROC_MEDI_PROCE == proc
                                       select new
                                       {
                                           tbs356.DE_OBSE_PROC_MEDI,
                                           tbs356.NM_PROC_MEDI,
                                           tbs356.ID_PROC_MEDI_PROCE,
                                           tbs356.CO_PROC_MEDI,
                                       }).FirstOrDefault();

                            if (res != null)
                            {
                                txtCodProc.Text = res.CO_PROC_MEDI;
                                txtNomeProc.Text = res.NM_PROC_MEDI;
                                txtObservacaoProc.Text = res.DE_OBSE_PROC_MEDI;
                                txtVlUnitProc.Text = vlUnit;
                            }
                        }

                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalInfoProcedimento();",
                            true
                        );
                    }
                }
            }
        }

        protected void txtQtde_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;
            DropDownList ddlProc;
            string idOper, idPlan;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    txt = (TextBox)linha.Cells[4].FindControl("txtQtde");
                    if (txt.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            idOper = ((HiddenField)linha.Cells[0].FindControl("hidIdOper")).Value;
                            idPlan = ((HiddenField)linha.Cells[0].FindControl("hidIdPlano")).Value;
                            ddlProc = (DropDownList)linha.Cells[3].FindControl("ddlProcedimento");
                            HiddenField hidValorUnitario = (HiddenField)linha.Cells[3].FindControl("hidValUnitProc");
                            //textbox que vai receber valor calculado
                            TextBox txtValor = (TextBox)linha.Cells[5].FindControl("txtVlUnitario");


                            //valor unitário do procedimento
                            string vlProcUnit = ((HiddenField)linha.Cells[3].FindControl("hidValUnitProc")).Value;
                            //Quantidade de sessões
                            int qt = int.Parse(txt.Text);
                            CalcularPreencherValoresTabelaECalculado(ddlProc, idOper, idPlan, hidValorUnitario);
                            CalculaValorTotalProcedimento(qt, vlProcUnit, txtValor);
                            CarregarValoresTotaisFooter();
                        }
                    }
                }
            }

            ControlaTabs("RGI");
        }

        protected void txtVlDesconto_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    txt = (TextBox)linha.Cells[6].FindControl("txtVlDesconto");
                    if (txt.ClientID == atual.ClientID)
                        CarregarValoresTotaisFooter();
                }
            }

            ControlaTabs("RGI");
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[7].FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridSolicitacoes(aux);
            CarregarValoresTotaisFooter();

            ControlaTabs("RGI");
        }

        protected void btnMaisSolicitacoes_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridSolicitacoes();
            ControlaTabs("RGI");
        }

        protected void ddlGrupoProc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    DropDownList ddlGrupoProc = (DropDownList)linha.Cells[1].FindControl("ddlGrupoProc");
                    DropDownList ddlSubGrupo = (DropDownList)linha.Cells[2].FindControl("ddlSubGrupo");
                    DropDownList ddlProc = (DropDownList)linha.Cells[3].FindControl("ddlProcedimento");
                    string idOper = ((HiddenField)linha.Cells[0].FindControl("hidIdOper")).Value;

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    if (ddlGrupoProc.ClientID == atual.ClientID)
                    {
                        CarregaSubGrupos(ddlSubGrupo, ddlGrupoProc);
                        CarregarProcedimentosMedicos(ddlProc, ddlGrupoProc, ddlSubGrupo, idOper);
                    }
                }
            }

            ControlaTabs("RGI");
        }

        protected void ddlSubGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    DropDownList ddlGrupoProc = (DropDownList)linha.Cells[1].FindControl("ddlGrupoProc");
                    DropDownList ddlSubGrupo = (DropDownList)linha.Cells[2].FindControl("ddlSubGrupo");
                    DropDownList ddlProc = (DropDownList)linha.Cells[3].FindControl("ddlProcedimento");
                    string idOper = ((HiddenField)linha.Cells[0].FindControl("hidIdOper")).Value;

                    //Carrega os procedimentos de acordo com grupo e subgrupo
                    if (ddlSubGrupo.ClientID == atual.ClientID)
                        CarregarProcedimentosMedicos(ddlProc, ddlGrupoProc, ddlSubGrupo, idOper);
                }
            }

            ControlaTabs("RGI");
        }

        protected void lnkFinalizar_OnClick(object sender, EventArgs e)
        {
            SalvarAtualizacaoCadastro("RegItn");
            SalvarItemSolicitacao();
        }

        private void SalvarItemSolicitacao()
        {
            TBS373_AGEND_AVALI_ITENS tbs373;
            TBS372_AGEND_AVALI tbs372;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                var ddlOper = (((HiddenField)li.Cells[0].FindControl("hidIdOper")));
                var ddlPlan = (((HiddenField)li.Cells[0].FindControl("hidIdPlano")));
                var ddlCateg = (((HiddenField)li.Cells[0].FindControl("hidIdCateg")));
                var txtNumeroPlano = (((HiddenField)li.Cells[0].FindControl("hidNuPlano")));
                var txtNuGuia = (((HiddenField)li.Cells[0].FindControl("hidNuGuia")));
                var txtNuAutor = (((HiddenField)li.Cells[0].FindControl("hidNuAutor")));
                var txtIdItemSolic = ((HiddenField)li.Cells[0].FindControl("hidItemSolic"));
                HiddenField hidValorUnitario = (HiddenField)li.Cells[3].FindControl("hidValUnitProc");

                var ddlGrupo = (DropDownList)li.Cells[1].FindControl("ddlGrupoProc");
                var ddlSubGrupo = (DropDownList)li.Cells[2].FindControl("ddlSubGrupo");
                var ddlProc = (DropDownList)li.Cells[3].FindControl("ddlProcedimento");
                var txtQtde = (TextBox)li.Cells[4].FindControl("txtQtde");
                var txtVlUnitario = (TextBox)li.Cells[5].FindControl("txtVlUnitario");
                var txtVlDesconto = (TextBox)li.Cells[6].FindControl("txtVlDesconto");



                if (int.Parse(IDAGENDA.Value) > 0)
                {
                    if ((!string.IsNullOrEmpty(ddlProc.SelectedItem.Value)))
                    {
                        //TBS373_AGEND_AVALI_ITENS Tbs373 = TBS373_AGEND_AVALI_ITENS.RetornaPelaChavePrimaria(idItemValue);
                        tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(IDAGENDA.Value));

                        //Salva objeto da entidade tbs373 que armazena os itens solicitados em uma recepção                    
                        tbs373 = new TBS373_AGEND_AVALI_ITENS();

                        //======================> Dados Gerais
                        tbs373.TBS372_AGEND_AVALI = tbs372;
                        tbs373.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedItem.Value) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedItem.Value)) : null);
                        tbs373.VL_PROC_UNIT = (!string.IsNullOrEmpty(txtVlUnitario.Text) ? Convert.ToDecimal(txtVlUnitario.Text) : (decimal?)null);
                        tbs373.QT_SESSO = (!string.IsNullOrEmpty(txtQtde.Text) ? int.Parse(txtQtde.Text) : (int?)null);
                        tbs373.TP_CONTR = "P";

                        //======================> Dados do Cadastro
                        tbs373.DT_CADAS = DateTime.Now;
                        tbs373.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs373.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs373.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                        tbs373.IP_CADAS = Request.UserHostAddress;

                        //======================> Dados da Situação
                        tbs373.CO_SITUA = "A";
                        tbs373.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs373.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs373.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
                        tbs373.IP_SITUA = Request.UserHostAddress;
                        tbs373.DT_SITUA = DateTime.Now;
                        tbs373.TBS387_DEFIC = null;
                        TBS373_AGEND_AVALI_ITENS.SaveOrUpdate(tbs373);
                    }
                }
                else
                {
                    if ((!string.IsNullOrEmpty(ddlProc.SelectedItem.Value)))
                    {
                        CalcularPreencherValoresTabelaECalculado(ddlProc, ddlOper.Value, ddlPlan.Value, hidValorUnitario);

                        tbs373 = TBS373_AGEND_AVALI_ITENS.RetornaPelaChavePrimaria(int.Parse(IDAGENDA.Value));
                        tbs372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(tbs373.TBS372_AGEND_AVALI.ID_AGEND_AVALI);

                        tbs373.TBS372_AGEND_AVALI = tbs372;
                        tbs373.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedItem.Value) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedItem.Value)) : null);
                        tbs373.VL_PROC_UNIT = (!string.IsNullOrEmpty(txtVlUnitario.Text) ? Convert.ToDecimal(txtVlUnitario.Text) : (decimal?)null);
                        tbs373.QT_SESSO = (!string.IsNullOrEmpty(txtQtde.Text) ? int.Parse(txtQtde.Text) : (int?)null);
                        //tbs373.TP_CONTR = "P";

                        //======================> Dados do Cadastro
                        //tbs373.DT_CADAS = DateTime.Now;
                        //tbs373.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        //tbs373.CO_COL_CADAS = LoginAuxili.CO_COL;
                        //tbs373.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                        //tbs373.IP_CADAS = Request.UserHostAddress;

                        //======================> Dados da Situação
                        tbs373.CO_SITUA = "A";
                        tbs373.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs373.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs373.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
                        tbs373.IP_SITUA = Request.UserHostAddress;
                        tbs373.DT_SITUA = DateTime.Now;
                        tbs373.TBS387_DEFIC = null;
                        TBS373_AGEND_AVALI_ITENS.SaveOrUpdate(tbs373);
                    }
                }
            }
        }


        protected void lnkConfirmaCancelamento_OnClick(object sender, EventArgs e)
        {
            //this.ClientScript.RegisterStartupScript(this.GetType(), "mensagem", "documento.onload = alert('');", true);
            //return;

            if (rdblTiposCancelamento.SelectedValue == "" && divCance.Visible)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe o tipo do cancelamento, e tente novamente.");
                AbreModalCancelamentoAgenda();
                return;
            }

            //Cancelamento de agendamento para Atendimento
            if (hidTipoAgenda.Value == "AT")
            {
                #region para Atendimento

                TBS174_AGEND_HORAR tbs174ant = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaCancel.Value));

                TBS430_HISTO_AGEND_HORAR tbs430new = new TBS430_HISTO_AGEND_HORAR();

                tbs430new.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaCancel.Value));
                tbs430new.CO_COL_AGEND_HORAR = tbs174ant.CO_COL_SITUA;
                tbs430new.DT_AGEND_HORAR = tbs174ant.DT_AGEND_HORAR;
                tbs430new.DT_SITUA_AGEND_HORAR = tbs174ant.DT_SITUA_AGEND_HORAR;
                tbs430new.HR_AGEND_HORAR = tbs174ant.HR_AGEND_HORAR;
                tbs430new.CO_SITUA_AGEND_HORAR = "A";

                //Salva o log de alteração de status
                TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();
                tbs375.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaCancel.Value));

                tbs375.FL_JUSTI = rdblTiposCancelamento.SelectedValue;
                tbs375.DE_OBSER = (!string.IsNullOrEmpty(txtObserCancelamento.Text) ? txtObserCancelamento.Text : null);
                //Se estiver cancelada, vai gerar o log para abertura, senão, para cancelamento
                tbs375.CO_SITUA_AGEND_HORAR = (tbs174ant.CO_SITUA_AGEND_HORAR == "C" ? "A" : "C");

                tbs375.FL_TIPO_LOG = "C";
                tbs375.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs375.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs375.DT_CADAS = DateTime.Now;
                tbs375.IP_CADAS = Request.UserHostAddress;
                TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375);

                //Se estiver cancelado, abre, se não, cancela
                tbs174ant.CO_SITUA_AGEND_HORAR = (tbs174ant.CO_SITUA_AGEND_HORAR == "C" ? "A" : "C");
                tbs174ant.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs174ant.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs174ant.DT_SITUA_AGEND_HORAR = DateTime.Now;

                //Grava as informações de cancelamento
                #region Grava informações de situação de Cancelamento

                //Se estiver com status de aberto grava as informações de cancelamento como NULL
                if (tbs174ant.CO_SITUA_AGEND_HORAR == "A")
                {
                    tbs174ant.FL_JUSTI_CANCE =
                    tbs174ant.DE_OBSER_CANCE = null;
                    tbs174ant.DT_CANCE = (DateTime?)null;
                    tbs174ant.CO_COL_CANCE =
                    tbs174ant.CO_EMP_CANCE = (int?)null;
                    tbs174ant.IP_CANCE = null;
                }
                else //Se estiver com status de cancelado, grava as informações de cancelamento
                {
                    tbs174ant.FL_JUSTI_CANCE = rdblTiposCancelamento.SelectedValue;
                    tbs174ant.DE_OBSER_CANCE = (!string.IsNullOrEmpty(txtObserCancelamento.Text) ? txtObserCancelamento.Text : null);
                    tbs174ant.DT_CANCE = DateTime.Now;
                    tbs174ant.CO_COL_CANCE = LoginAuxili.CO_COL;
                    tbs174ant.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                    tbs174ant.IP_CANCE = Request.UserHostAddress;
                }

                #endregion

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ant, true);

                //Atualiza a imagem de cancelamento 
                foreach (GridViewRow li in grdAgendamentos.Rows)
                {
                    if ((((HiddenField)li.Cells[6].FindControl("hidIdAgend")).Value) == hidIdAgendaCancel.Value)
                    {
                        ImageButton imgCance = (ImageButton)li.Cells[7].FindControl("imgCancelar");
                        ImageButton imgPrese = (ImageButton)li.Cells[6].FindControl("imgPresente");
                        ImageButton imgEncam = (ImageButton)li.FindControl("imgEncam");

                        if (tbs375.CO_SITUA_AGEND_HORAR == "C") //Se estiver cancelando
                        {
                            imgCance.ImageUrl = "/Library/IMG/PGS_IC_Cancelado.png";
                            imgPrese.Enabled = imgEncam.Enabled = false;

                            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                       where tbs174.CO_ALU == tbs174ant.CO_ALU
                                       && tbs174.DT_AGEND_HORAR <= tbs174ant.DT_AGEND_HORAR
                                       select tbs174).OrderByDescending(a => new { a.DT_AGEND_HORAR, a.HR_AGEND_HORAR }).ToList();

                            if (res != null && res.Count > 0)
                            {
                                var numFaltas = 0;
                                var numAgends = 0;

                                foreach (var i in res)
                                {
                                    if ((i.DT_AGEND_HORAR == tbs174ant.DT_AGEND_HORAR && TimeSpan.Parse(i.HR_AGEND_HORAR) < TimeSpan.Parse(tbs174ant.HR_AGEND_HORAR)) || i.DT_AGEND_HORAR < tbs174ant.DT_AGEND_HORAR)
                                    {
                                        numAgends++;

                                        if (i.CO_SITUA_AGEND_HORAR == "C")
                                            numFaltas++;
                                    }

                                    if (numAgends == 2)
                                        break;
                                }

                                if (numFaltas == 2)
                                {
                                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174ant.CO_ALU.Value);

                                    tb07.FL_FALTOSO = "S";

                                    TB07_ALUNO.SaveOrUpdate(tb07, true);

                                    AuxiliPagina.EnvioMensagemErro(this.Page, "O paciente já possui mais duas faltas anteriores a essa!");
                                }
                            }
                        }
                        else //Se estiver liberando
                        {
                            imgCance.ImageUrl = "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                            imgPrese.Enabled = imgEncam.Enabled = true;
                        }
                    }
                }

                CarregaConsultasAgendadas();
                if (tbs174ant.CO_SITUA_AGEND_HORAR == "A")
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Cancelamento do agendamento desfeito com sucesso!");
                else
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Agendamento cancelado com sucesso!");

                #endregion
            }
            else if (hidTipoAgenda.Value == "AV") //Cancelamento de agendamento para Avaliação
            {
                #region para Avaliação

                TBS372_AGEND_AVALI tbs372ant = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaCancel.Value));

                //Salva o log de alteração de status
                TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs380 = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();
                tbs380.TBS372_AGEND_AVALI = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaCancel.Value));

                tbs380.FL_JUSTI = rdblTiposCancelamento.SelectedValue;
                tbs380.DE_OBSER = (!string.IsNullOrEmpty(txtObserCancelamento.Text) ? txtObserCancelamento.Text : null);
                //Se estiver cancelada, vai gerar o log para abertura, senão, para cancelamento
                tbs380.CO_SITUA_AGEND = (tbs372ant.CO_SITUA == "C" ? "A" : "C");

                tbs380.FL_TIPO_LOG = "C";
                tbs380.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs380.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs380.DT_CADAS = DateTime.Now;
                tbs380.IP_CADAS = Request.UserHostAddress;
                TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs380);

                //Atualiza a imagem de cancelamento 
                foreach (GridViewRow li in grdAgendaAvaliacoes.Rows)
                {
                    if ((((HiddenField)li.FindControl("hidAgendaAval")).Value) == hidIdAgendaCancel.Value)
                    {
                        //Se estiver cancelado, abre, senão, cancela
                        tbs372ant.CO_SITUA = (tbs372ant.CO_SITUA == "C" ? "A" : "C");
                        tbs372ant.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs372ant.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs372ant.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs372ant.DT_SITUA = DateTime.Now;
                        tbs372ant.IP_SITUA = Request.UserHostAddress;

                        //Grava as informações de cancelamento
                        #region Grava informações de situação de Cancelamento

                        //Se estiver com status de aberto grava as informações de cancelamento como NULL
                        if (tbs372ant.CO_SITUA == "A")
                        {
                            tbs372ant.DE_OBSER_CANCE = null;
                            tbs372ant.DT_CANCE = (DateTime?)null;
                            tbs372ant.CO_COL_CANCE =
                            tbs372ant.CO_EMP_CANCE = (int?)null;
                            tbs372ant.IP_CANCE = null;
                        }
                        else //Se estiver com status de cancelado, grava as informações de cancelamento
                        {
                            tbs372ant.DE_OBSER_CANCE = (!string.IsNullOrEmpty(txtObserCancelamento.Text) ? txtObserCancelamento.Text : null);
                            tbs372ant.DT_CANCE = DateTime.Now;
                            tbs372ant.CO_COL_CANCE = LoginAuxili.CO_COL;
                            tbs372ant.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                            tbs372ant.IP_CANCE = Request.UserHostAddress;
                        }

                        #endregion

                        TBS372_AGEND_AVALI.SaveOrUpdate(tbs372ant, true);
                    }
                }

                CarregaAgendamentoAvaliacoes();

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Status de Item de Agenda alterado com sucesso!");

                #endregion
            }

            rdblTiposCancelamento.ClearSelection();
        }

        protected void grdAgendamentos_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                string flConfir = ((HiddenField)e.Row.FindControl("hidFlConfirmado")).Value;
                string flEncam = ((HiddenField)e.Row.FindControl("hidFlEncaminhado")).Value;
                string coSitua = ((HiddenField)e.Row.FindControl("hidCoSitua")).Value;
                ImageButton imgCancel = (ImageButton)e.Row.FindControl("imgCancelar");
                ImageButton imgPresenca = (ImageButton)e.Row.FindControl("imgPresente");
                ImageButton imgEncaminh = (ImageButton)e.Row.FindControl("imgEncam");
                ImageButton imgSituacao = (ImageButton)e.Row.FindControl("imgSituacao");
                bool flPendFinac = bool.Parse(((HiddenField)e.Row.FindControl("hidPendFinanc")).Value);
                bool flFaltasConsec = bool.Parse(((HiddenField)e.Row.FindControl("hidFaltasConsec")).Value);

                #region Trata Confirmação

                //Se estiver confirmado faz as ações necessárias
                if (flConfir == "S")
                {
                    imgCancel.Enabled = false;
                    imgPresenca.Enabled = imgEncaminh.Enabled = true;

                    imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                }

                if (coSitua == "M")
                {
                    imgCancel.Enabled = false;
                    imgPresenca.Enabled = false;
                    imgEncaminh.Enabled = false;
                }
                #endregion

                #region Trata Encaminhamento

                //Se estiver Encaminhado faz as ações necessárias
                if (flEncam == "S" || flEncam == "A" || flEncam == "T")
                {
                    imgCancel.Enabled = imgPresenca.Enabled = false;
                    imgEncaminh.Enabled = true;

                    imgEncaminh.ImageUrl = "/Library/IMG/PGS_IC_EncaminharIn.png";

                }

                //Insere mensagem de confirmação personalizada para alteração de status de encaminhado
                //imgEncaminh.OnClientClick = "if (!window.confirm('Tem certeza de que deseja alterar o status de Encaminhamento?')) return false;";

                #endregion

                #region Trata Cancelamento

                //Se estiver cancelado, faz as ações necessárias
                if (coSitua == "C")
                {
                    imgCancel.Enabled = true;
                    imgPresenca.Enabled = imgEncaminh.Enabled = false;

                    imgCancel.ImageUrl = "/Library/IMG/PGS_IC_Cancelado.png";
                }

                #endregion

                if (flPendFinac || flFaltasConsec)
                    e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void grdAgendaAvaliacoes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                string flConfir = (((HiddenField)e.Row.FindControl("hidFlConfirmado")).Value);
                string coSitua = (((HiddenField)e.Row.FindControl("hidCoSitua")).Value);
                string flCadAlu = (((HiddenField)e.Row.FindControl("hidIdCadAlu")).Value);
                string flCadRes = (((HiddenField)e.Row.FindControl("hidIdCadRes")).Value);
                string flSlcAvl = (((HiddenField)e.Row.FindControl("hidIdSlcAvl")).Value);
                string tipoAgend = (((HiddenField)e.Row.FindControl("hitTipoAgend")).Value);
                //string flRegIts = (((HiddenField)e.Row.FindControl("hidIdRegItn")).Value);
                //ImageButton imgInfosCadasPac = (ImageButton)e.Row.FindControl("imgInfosCadasPac");
                //ImageButton imgInfosCadasRes = (ImageButton)e.Row.FindControl("imgInfosCadasRes");
                ImageButton imgSelAvaliador = (ImageButton)e.Row.FindControl("imgSelAvaliador");
                ImageButton imgRegisItens = (ImageButton)e.Row.FindControl("imgRegisItens");
                ImageButton imgCancel = (ImageButton)e.Row.FindControl("imgCancelarAA");
                ImageButton imgPresenca = (ImageButton)e.Row.FindControl("imgPresenteAA");
                ImageButton imgEncam = (ImageButton)e.Row.FindControl("imgEncamAA");

                if (coSitua == "A")
                {
                    if (flConfir == "S")
                    {
                        if (tipoAgend == "PROC" || (flCadAlu == "S" && flCadRes == "S" && flSlcAvl == "S"))
                            imgEncam.OnClientClick = "return confirm ('Confirmar o encaminhamento do paciente?');";
                        else
                            imgEncam.OnClientClick = "alert('Antes de encaminhar verifique o registro de informações do paciente!'); return false;";

                        imgCancel.OnClientClick = "alert('Não é possivel cancelar após o registro da presença do paciente!'); return false;";

                        imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                    }
                    else
                    {
                        imgEncam.OnClientClick = "alert('Não é possivel encaminhar antes de registrar a presença do paciente e editar suas informações!'); return false;";
                        //imgInfosCadasPac.OnClientClick =
                        //imgInfosCadasRes.OnClientClick =
                        imgSelAvaliador.OnClientClick =
                            //imgRegisItens.OnClientClick = "alert('É necessário o registro da presença do paciente!'); return false;";

                        imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteNaoChegou.ico";
                    }
                }
                else if (coSitua == "E" || coSitua == "R")
                {
                    imgEncam.OnClientClick = "alert('Não é possivel desfazer após o encaminhamento do paciente!'); return false;";
                    imgCancel.OnClientClick = "alert('Não é possivel cancelar após o encaminhamento do paciente!'); return false;";
                    imgPresenca.OnClientClick = "alert('Não é possivel desmarcar após o encaminhamento do paciente!'); return false;";
                    //imgInfosCadasPac.OnClientClick =
                    //imgInfosCadasRes.OnClientClick =
                    imgSelAvaliador.OnClientClick =
                    imgRegisItens.OnClientClick = "alert('Não é possivel editar após o encaminhamento do paciente!'); return false;";

                    imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                    imgEncam.ImageUrl = "/Library/IMG/PGS_IC_EncaminharIn.png";
                }
                else if (coSitua == "C")
                {
                    imgEncam.OnClientClick = "alert('Não é possivel encaminhar após o cancelamento do agendamento!'); return false;";
                    imgPresenca.OnClientClick = "alert('Não é possivel realizar presença após o cancelamento do agendamento!'); return false;";
                    //31/05/2017 Adones - Permitir o descancelamento por pedido da criar e autorizado por cezar 
                    //imgCancel.OnClientClick = "alert('Não é possivel desfazer após o cancelamento do agendamento'); return false;";
                    //imgInfosCadasPac.OnClientClick =
                    //imgInfosCadasRes.OnClientClick =
                    imgSelAvaliador.OnClientClick =
                    imgRegisItens.OnClientClick = "alert('Não é possivel editar após o cancelamento do agendamento!'); return false;";

                    imgCancel.ImageUrl = "/Library/IMG/PGS_IC_Cancelado.png";
                }
            }
        }

        protected void imgInfosCadasPaciente_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgInfosCadasPaciente");

                    if (img.ClientID == atual.ClientID)
                    {
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoPac")).Value);
                        string coResp = ((HiddenField)linha.FindControl("hidCoResp")).Value;

                        AbreModalInfosCadastrais();
                        PesquisaCarregaPaciMODAL(coAlu, (!string.IsNullOrEmpty(coResp) ? int.Parse(coResp) : (int?)null));
                        hidCoPacModal.Value = coAlu.ToString();
                        hidCoRespModal.Value = coResp.ToString();
                    }
                }
            }
        }

        protected void ddlLocalAtendimentoM1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                "AbreProximoPasso();",
                                true
                            );
        }

        protected void ddlLocalAtendimentoM2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                "AbreModalEncaminhamento();",
                                true
                            );
        }

        protected void ddlLocalAtendimentoM3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                "AbreModalAtend();",
                                true
                            );
        }

        protected void ddlLocalTriagemM1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                "AbreProximoPasso();",
                                true
                            );
        }

        protected void ddlLocalTriagemM2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                "AbreModalEncaminhamento();",
                                true
                            );
        }

        protected void lnkEncamSim_OnClick(object sender, EventArgs e)
        {
            LinkButton atual = (LinkButton)sender;
            if (atual.ClientID == lnkEncAtendimento.ClientID)
            {
                if (String.IsNullOrEmpty(ddlLocalAtendimentoM1.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um local/departamento para atendimento.");
                    return;
                }

                LocalAtend.Value = ddlLocalAtendimentoM1.SelectedValue;
            }

            if (atual.ClientID == lnkDirEncAtendimento.ClientID)
            {
                if (String.IsNullOrEmpty(ddlLocalAtendimentoM2.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um local/departamento para atendimento.");
                    return;
                }

                LocalAtend.Value = ddlLocalAtendimentoM2.SelectedValue;
            }

            if (atual.ClientID == lnkEncAtendimento3.ClientID)
            {
                if (String.IsNullOrEmpty(ddlLocalAtendimentoM3.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um local/departamento para atendimento.");
                    return;
                }

                LocalAtend.Value = ddlLocalAtendimentoM3.SelectedValue;
            }

            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    if (linha.RowIndex.ToString() == hidIndexGridAtend.Value)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        ImageButton imgCancel = (ImageButton)linha.FindControl("imgCancelar");
                        ImageButton imgPresen = (ImageButton)linha.FindControl("imgPresente");
                        ImageButton img = (ImageButton)linha.FindControl("imgEncam");
                        string caminho = img.ImageUrl;

                        //Se não estiver encaminhado, ENCAMINHA, gera o log e altera a imagem
                        if (caminho == "/Library/IMG/PGS_IC_EncaminharOut.png")
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "E", true, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_IC_EncaminharIn.png";

                            //Se estiver encaminhado, não faz sentido alterar para cancelado ou que não veio
                            imgCancel.Enabled = imgPresen.Enabled = false;
                        }
                        else //Se estiver encaminhado, DESENCAMINHA, gera o log e altera a imagem
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "E", false, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_IC_EncaminharOut.png";
                            imgCancel.Enabled = true;

                            //Se não estiver encaminhado, permite alterar para cancelado ou que não veio
                            imgPresen.Enabled = true;

                            //Só libera o botão de cancelamento, se o paciente ainda estiver com status de não chegou
                            if (imgPresen.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                                imgCancel.Enabled = true;
                            else
                                imgCancel.Enabled = false;
                        }
                    }
                }
            }

            CarregaConsultasAgendadas();
        }

        protected void lnkNaoEncaminhar_OnClick(object sender, EventArgs e)
        {
            LinkButton atual = (LinkButton)sender;
            ImageButton img;
            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgPresente");
                    if (linha.RowIndex.ToString() == hidIndexGridAtend.Value)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoPac")).Value);
                        ImageButton imgCancel = (ImageButton)linha.FindControl("imgCancelar");
                        ImageButton imgEncami = (ImageButton)linha.FindControl("imgEncam");
                        bool flFaltasConsec = bool.Parse(((HiddenField)linha.FindControl("hidFaltasConsec")).Value);

                        var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                   join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                                   where tb25.CO_EMP == LoginAuxili.CO_EMP
                                   select new
                                   {
                                       tb83.FL_PERM_ATEND_TRIAGEM
                                   }).FirstOrDefault();

                        //Se não estiver confirmado, confirma, gera o log e altera a imagem
                        if (caminho == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "P", true, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                            imgCancel.Enabled = false;
                            imgEncami.Enabled = true;

                            lblTresFaltasAnteriores.Visible = false;

                            if (flFaltasConsec)
                            {
                                lblTresFaltasAnteriores.Visible = true;

                                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                                tb07.FL_FALTOSO = "N";

                                TB07_ALUNO.SaveOrUpdate(tb07, true);
                            }

                            #region Responsável por prepara opção de já realizar encaminhamento



                            hidIndexGridAtend.Value = linha.RowIndex.ToString();

                            #endregion
                        }
                        else //Se estiver confirmado, desconfirma, gera o log e altera a imagem
                        {
                            //SalvaLogAlteracaoStatusAgenda(idAgenda, "P", false, EObjetoLogAgenda.paraAtendimento);
                            //img.ImageUrl = "/Library/IMG/PGS_PacienteNaoChegou.ico";
                            //imgCancel.Enabled = true;
                            //imgEncami.Enabled = false;
                        }
                    }
                }
            }

            CarregaConsultasAgendadas();
        }


        protected void lnkEncamTriagem_OnClick(object sender, EventArgs e)
        {
            LinkButton atual = (LinkButton)sender;
            if (atual.ClientID == lnkEncTriagem.ClientID)
            {
                if (String.IsNullOrEmpty(ddlLocalTriagemM1.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um local/departamento para avaliação de risco.");
                    return;
                }

                LocalTriagem.Value = ddlLocalTriagemM1.SelectedValue;
            }

            if (atual.ClientID == lnkDirEncTriagem.ClientID)
            {
                if (String.IsNullOrEmpty(ddlLocalTriagemM2.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um local/departamento para avaliação de risco.");
                    return;
                }

                LocalTriagem.Value = ddlLocalTriagemM2.SelectedValue;
            }

            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    if (linha.RowIndex.ToString() == hidIndexGridAtend.Value)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        ImageButton imgCancel = (ImageButton)linha.FindControl("imgCancelar");
                        ImageButton imgPresen = (ImageButton)linha.FindControl("imgPresente");
                        ImageButton img = (ImageButton)linha.FindControl("imgEncam");
                        string caminho = img.ImageUrl;

                        //Se não estiver encaminhado, ENCAMINHA, gera o log e altera a imagem
                        if (caminho == "/Library/IMG/PGS_IC_EncaminharOut.png")
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "T", true, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_IC_EncaminharIn.png";

                            //Se estiver encaminhado, não faz sentido alterar para cancelado ou que não veio
                            imgCancel.Enabled = imgPresen.Enabled = false;
                        }
                        else //Se estiver encaminhado, DESENCAMINHA, gera o log e altera a imagem
                        {
                            SalvaLogAlteracaoStatusAgenda(idAgenda, "T", false, EObjetoLogAgenda.paraAtendimento);
                            img.ImageUrl = "/Library/IMG/PGS_IC_EncaminharOut.png";
                            imgCancel.Enabled = true;

                            //Se não estiver encaminhado, permite alterar para cancelado ou que não veio
                            imgPresen.Enabled = true;

                            //Só libera o botão de cancelamento, se o paciente ainda estiver com status de não chegou
                            if (imgPresen.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                                imgCancel.Enabled = true;
                            else
                                imgCancel.Enabled = false;
                        }
                    }
                }
            }

            CarregaConsultasAgendadas();
        }

        protected void lnkEncamNao_OnClick(object sender, EventArgs e)
        {

        }

        protected void lnkbAtendSim_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hidAgendSelec.Value))
                SalvaLogAlteracaoStatusAgenda(int.Parse(hidAgendSelec.Value), "R", true, EObjetoLogAgenda.paraAvaliacao);

            CarregaAgendamentoAvaliacoes();
        }


        protected void lnkEncaixe_OnClick(object sender, EventArgs e)
        {
            //AbreModalPadrao("AbreModalEncaixe();");
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("8120_RegistroConsulMedMod22/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                this.Session[SessoesHttp.URLOrigem] = HttpContext.Current.Request.Url.PathAndQuery;

                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}{2}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo), "&T=Triagem"));
            }
        }

        protected void lnkbAgend_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("8120_RegistroConsulMedMod22/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                this.Session[SessoesHttp.URLOrigem] = HttpContext.Current.Request.Url.PathAndQuery;

                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
            }
        }

        protected void lnkbMovimentacao_OnClick(object sender, EventArgs e)
        {
            var data = DateTime.Now;

            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;

            txtDtMovimOrigem.Text = txtDtMovimDestino.Text = data.ToShortDateString();

            AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfiOrig, 0, false, "0", true);
            AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfiDest, 0, false, "0", true);

            grdPacMovimentacoes.DataSource = null;
            grdPacMovimentacoes.DataBind();

            AbreModalPadrao("AbreModalMovimentacao();");
        }

        protected void grdPacMovimentacoes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (!String.IsNullOrEmpty(txtDtMovimDestino.Text) && !String.IsNullOrEmpty(drpProfiDest.SelectedValue))
                {
                    DateTime dtMov = DateTime.Parse(txtDtMovimDestino.Text);
                    int profDest = int.Parse(drpProfiDest.SelectedValue);

                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               where tbs174.CO_EMP == LoginAuxili.CO_EMP
                                    && tbs174.CO_COL == profDest
                                    && tbs174.DT_AGEND_HORAR == dtMov
                                    && !tbs174.CO_SITUA_AGEND_HORAR.Equals("R")
                               //&& (tbs174.CO_SITUA_AGEND_HORAR == "C" || tbs174.CO_SITUA_AGEND_HORAR == "M" || tbs174.CO_ALU == null)
                               select new
                               {
                                   ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                                   HR_AGEND_HORAR = tbs174.CO_ALU.HasValue ? tbs174.HR_AGEND_HORAR + "*" : tbs174.HR_AGEND_HORAR,
                               }).OrderBy(w => w.HR_AGEND_HORAR).ToList();

                    if (res.Count > 0)
                    {
                        var drp = (DropDownList)e.Row.FindControl("drpHoraDest");

                        drp.DataTextField = "HR_AGEND_HORAR";
                        drp.DataValueField = "ID_AGEND_HORAR";
                        drp.DataSource = res;
                        drp.DataBind();

                        drp.Items.Insert(0, new ListItem("", ""));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
               this.Page,
               this.GetType(),
               "",
               "alert('Não há horários disponíveis para o profissional de destino na data informada.');",
               true
           );
                    }
                }
            }
        }

        protected void lnkbMovimentar_OnClick(object sender, EventArgs e)
        {
            #region Validações
            var erros = 0;
            if (grdPacMovimentacoes.Rows.Count != 0)
            {
                int ck = 0;
                var agds = new List<string>();
                foreach (GridViewRow r in grdPacMovimentacoes.Rows)
                {
                    CheckBox chkPaciente = (CheckBox)r.FindControl("chkPaciente");

                    if (chkPaciente.Checked)
                    {
                        var drp = (DropDownList)r.FindControl("drpHoraDest");

                        if (String.IsNullOrEmpty(drp.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um horário para os agendamentos marcados para movimentação");
                            drp.Focus();
                            erros++;
                        }
                        else
                        {
                            if (agds.Contains(drp.SelectedValue))
                            {
                                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Não é possivel movimentar dois agendamentos para o mesmo horário");
                                drp.Focus();
                                erros++;
                            }
                            else
                                agds.Add(drp.SelectedValue);
                        }

                        ck++;
                    }
                }

                if (ck == 0)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Deve ser selecionado pelo menos um paciente para ser movimentado!");
                    erros++;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Não existe pacientes para realizar a movimentação!");
                erros++;
            }

            #endregion

            #region Persistencias

            if (erros == 0)
            {
                foreach (GridViewRow r in grdPacMovimentacoes.Rows)
                {
                    CheckBox chkPaciente = (CheckBox)r.FindControl("chkPaciente");

                    if (chkPaciente.Checked)
                    {
                        int idAgend = int.Parse(((HiddenField)r.FindControl("hidIdAgendHorar")).Value);
                        var drpHora = (DropDownList)r.FindControl("drpHoraDest");

                        var agAtl = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgend);

                        #region Salvar novo Agendamento

                        var agNvo = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(drpHora.SelectedValue));

                        if (agNvo.CO_SITUA_AGEND_HORAR == "C")
                        {
                            agNvo = new TBS174_AGEND_HORAR();
                            agNvo.CO_COL = !String.IsNullOrEmpty(drpProfiDest.SelectedValue) ? int.Parse(drpProfiDest.SelectedValue) : 0;
                            agNvo.DT_AGEND_HORAR = !String.IsNullOrEmpty(txtDtMovimDestino.Text) ? DateTime.Parse(txtDtMovimDestino.Text) : DateTime.Now;
                            agNvo.HR_AGEND_HORAR = drpHora.SelectedItem.Text.Replace("*", "");
                            agNvo.HR_DURACAO_AGENDA = agAtl.HR_DURACAO_AGENDA;
                        }

                        agNvo.CO_ALU = agAtl.CO_ALU;
                        agNvo.CO_COL_SITUA = LoginAuxili.CO_COL;
                        agNvo.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        agNvo.CO_EMP = agAtl.CO_EMP;
                        agNvo.CO_EMP_ALU = agAtl.CO_EMP_ALU;
                        agNvo.CO_ESPEC = agAtl.CO_ESPEC;
                        agNvo.CO_DEPT = agAtl.CO_DEPT;
                        agNvo.CO_SITUA_AGEND_HORAR = "W";
                        agNvo.DT_SITUA_AGEND_HORAR = DateTime.Now;
                        agNvo.CO_TIPO_PROC_MEDI = agAtl.CO_TIPO_PROC_MEDI;
                        agNvo.TP_CONSU = agAtl.TP_CONSU;
                        agNvo.FL_CONF_AGEND = agAtl.FL_CONF_AGEND;
                        agNvo.FL_AGEND_CONSU = agAtl.FL_AGEND_CONSU;
                        agNvo.FL_ENCAI_AGEND = agAtl.FL_ENCAI_AGEND;
                        agNvo.FL_CONFIR_CONSUL_SMS = agAtl.FL_CONFIR_CONSUL_SMS;
                        agNvo.NU_REGIS_CONSUL = agAtl.NU_REGIS_CONSUL;
                        agNvo.TP_AGEND_HORAR = agAtl.TP_AGEND_HORAR;
                        agNvo.DE_ACAO_PLAN = agAtl.DE_ACAO_PLAN;

                        agAtl.TB250_OPERAReference.Load();
                        if (agAtl.TB250_OPERA != null)
                            agNvo.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(agAtl.TB250_OPERA.ID_OPER);

                        agAtl.TB251_PLANO_OPERAReference.Load();
                        if (agAtl.TB251_PLANO_OPERA != null)
                            agNvo.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(agAtl.TB251_PLANO_OPERA.ID_PLAN);

                        TBS174_AGEND_HORAR.SaveOrUpdate(agNvo, true);

                        #endregion

                        #region Atualizar Procedimentos

                        var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(a => a.TBS174_AGEND_HORAR.ID_AGEND_HORAR == agAtl.ID_AGEND_HORAR).ToList();

                        foreach (var i in tbs389)
                        {
                            i.TBS174_AGEND_HORAR = agNvo;

                            i.TBS386_ITENS_PLANE_AVALIReference.Load();
                            if (i.TBS386_ITENS_PLANE_AVALI != null)
                            {
                                i.TBS386_ITENS_PLANE_AVALI.DT_AGEND =
                                i.TBS386_ITENS_PLANE_AVALI.DT_INICI =
                                i.TBS386_ITENS_PLANE_AVALI.DT_FINAL = agNvo.DT_AGEND_HORAR;

                                TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(i);
                            }
                        }

                        #endregion

                        #region Atualizar Agendamento antigo
                        //Situação de agendamento vai para "M" - Movimentado
                        agAtl.CO_SITUA_AGEND_HORAR = "M";
                        agAtl.CO_COL_SITUA = LoginAuxili.CO_COL;
                        agAtl.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        agAtl.DT_SITUA_AGEND_HORAR = DateTime.Now;

                        agAtl.FL_JUSTI_CANCE = "";
                        agAtl.DE_OBSER_CANCE = "Movimentação de agenda";
                        agAtl.DT_CANCE = DateTime.Now;
                        agAtl.CO_COL_CANCE = LoginAuxili.CO_COL;
                        agAtl.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                        agAtl.IP_CANCE = Request.UserHostAddress;
                        //agAtl.CO_ALU = (int?)null;

                        var Tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(X => X.TBS174_AGEND_HORAR.ID_AGEND_HORAR == agAtl.ID_AGEND_HORAR).ToList();

                        if (Tbs389.Count > 0)
                        {
                            foreach (var i in Tbs389)
                            {
                                i.TBS174_AGEND_HORARReference.Load();
                                i.TBS386_ITENS_PLANE_AVALIReference.Load();

                                var Tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(i.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI);
                                Tbs386.TBS370_PLANE_AVALIReference.Load();

                                var Tbs370 = TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(Tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);

                                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(i, true);

                                TBS386_ITENS_PLANE_AVALI.Delete(Tbs386, true);

                                TBS370_PLANE_AVALI.Delete(Tbs370, true);
                            }
                        }

                        TBS174_AGEND_HORAR.SaveOrUpdate(agAtl, true);

                        #endregion

                        #region Atualizar log Agendamento antigo

                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();
                        tbs375.TBS174_AGEND_HORAR = agAtl;

                        tbs375.FL_JUSTI = "M";
                        var medicoDestino = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_COL == agNvo.CO_COL).FirstOrDefault();
                        tbs375.DE_OBSER = "Movimentado para " + medicoDestino.CO_COL + " - " + medicoDestino.NO_APEL_COL + " (" + agNvo.DT_AGEND_HORAR.ToString("dd/MM/yyyy") + " - " + agNvo.HR_AGEND_HORAR.ToString() + ")";
                        tbs375.CO_SITUA_AGEND_HORAR = "M";
                        //Tipo de log vai para "M" - Movimentado
                        tbs375.FL_TIPO_LOG = "M";
                        tbs375.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs375.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs375.DT_CADAS = DateTime.Now;
                        tbs375.IP_CADAS = Request.UserHostAddress;

                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375);

                        #endregion
                        #region Atualizar log Agendamento novo

                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375Novo = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();
                        tbs375Novo.TBS174_AGEND_HORAR = agNvo;

                        tbs375Novo.FL_JUSTI = "W";
                        var idProfissionalOrigem = int.Parse(drpProfiOrig.SelectedValue);
                        var profissionalOrigem = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_COL == idProfissionalOrigem).FirstOrDefault();
                        int? idLocal = agAtl.ID_DEPTO_LOCAL_RECEP;
                        var local = idLocal != null ? TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(idLocal.ToString())).NO_DEPTO : "S/R";
                        tbs375Novo.DE_OBSER = "Movimentado de " + +profissionalOrigem.CO_COL + " - " + profissionalOrigem.NO_APEL_COL + " (" + agAtl.DT_AGEND_HORAR.ToString("dd/MM/yyyy") + " - " + agAtl.HR_AGEND_HORAR.ToString() + ") - (" + local + ")";
                        tbs375Novo.CO_SITUA_AGEND_HORAR = "W";
                        //Tipo de log vai para "M" - Movimentado
                        tbs375Novo.FL_TIPO_LOG = "W";
                        tbs375Novo.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs375Novo.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs375Novo.DT_CADAS = DateTime.Now;
                        tbs375Novo.IP_CADAS = Request.UserHostAddress;

                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375Novo);

                        #endregion
                    }
                }

                CarregaPacientesMovimentacao();

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Movimentação de agenda realizada com sucesso!");
            }

            #endregion

            AbreModalPadrao("AbreModalMovimentacao();");
        }

        protected void lnkbFichaAtend_OnClick(object sender, EventArgs e)
        {
            List<int> listAlus = new List<int>();

            if (grdAgendamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendamentos.Rows)
                {
                    string flEncam = (((HiddenField)linha.FindControl("hidFlEncaminhado")).Value);

                    if (flEncam == "S")
                    {
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoPac")).Value);

                        listAlus.Add(coAlu);
                    }
                }
            }

            if (grdAgendaAvaliacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaAvaliacoes.Rows)
                {
                    string coSitua = (((HiddenField)linha.FindControl("hidCoSitua")).Value);

                    if (coSitua == "E" || coSitua == "R")
                    {
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hidCoAlu")).Value);

                        listAlus.Add(coAlu);
                    }
                }
            }

            if (listAlus.Count > 0)
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where listAlus.Contains(tb07.CO_ALU)
                           select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

                if (res != null)
                {
                    drpPacienteFicha.DataTextField = "NO_ALU";
                    drpPacienteFicha.DataValueField = "CO_ALU";
                    drpPacienteFicha.DataSource = res;
                    drpPacienteFicha.DataBind();
                }

                AbreModalPadrao("AbreModalFichaAtendimento();");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Deve existir pelo menos um paciente encaminhado!");
        }

        protected void lnkbImprimirFicha_Click(object sender, EventArgs e)
        {
            int paciente = int.Parse(drpPacienteFicha.SelectedValue);

            string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaAtend rpt = new RptFichaAtend();
            var retorno = rpt.InitReport("FICHA DE ATENDIMENTO", infos, LoginAuxili.CO_EMP, paciente, 0, txtObsFicha.Text, txtQxsFicha.Text);

            GerarRelatorioPadrão(rpt, retorno);
        }

        protected void lnkbGuia_OnClick(object sender, EventArgs e)
        {
            var data = DateTime.Now;

            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;

            txtDtGuia.Text = data.ToShortDateString();
            txtPacienteGuia.Text = "";
            txtDtGuiaIni.Text = "";
            txtDtGuiaFim.Text = "";
            txtObsGuia.Text = "";
            OcultarPesquisaPacienteGuia(false);
            txtObsGuia.Attributes.Add("MaxLength", "180");
            drpOperGuia.Items.Clear();
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(drpOperGuia, false, false, false, true, false);
            drpOperGuia.Items.Insert(0, new ListItem("PADRÃO", "0"));
            CarregaProfissionaisGuia();
            CarregaAgendGuia();
            txtProntuarioGuia.Text = "";
            txtProntuarioGuia.Enabled = false;
            txtCpfGuia.Text = "";
            txtCpfGuia.Enabled = false;
            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        protected void drpPacienteGuia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendGuia();
            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        protected void imgPesqPacienteGuia_OnClick(object sender, EventArgs e)
        {
            if (chkProntuarioGuia.Checked && String.IsNullOrEmpty(txtProntuarioGuia.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe o número do Prontuário para usar esse filtro.");
                txtProntuarioGuia.Focus();
            }
            else if (chkCpfGuia.Checked && String.IsNullOrEmpty(txtCpfGuia.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe o número do CPF para usar esse filtro.");
                txtCpfGuia.Focus();
            }
            else if (!chkProntuarioGuia.Checked && !chkCpfGuia.Checked && !String.IsNullOrEmpty(txtPacienteGuia.Text) && txtPacienteGuia.Text.Length < 3)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Digite pelo menos 3 letras para filtar por paciente.");
                txtPacienteGuia.Focus();
            }
            else if (!chkProntuarioGuia.Checked && !chkCpfGuia.Checked && String.IsNullOrEmpty(txtPacienteGuia.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Escolha uma opção de filtro ou digite o nome ou parte do nome do paciente.");
            }
            else
            {
                OcultarPesquisaPacienteGuia(true);
                CarregarPacientesGuia(drpPacienteGuia);
            }

            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        private void OcultarPesquisaPacienteGuia(bool ocultar)
        {
            txtPacienteGuia.Visible =
            imgPesqPacienteGuia.Visible = !ocultar;
            drpPacienteGuia.Visible =
            imgVoltarPesqPacienteGuia.Visible = ocultar;
        }

        protected void imgVoltarPesqPacienteGuia_OnClick(object sender, EventArgs e)
        {
            txtProntuarioGuia.Text =
            txtCpfGuia.Text = "";

            OcultarPesquisaPacienteGuia(false);
            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        protected void imgPesqPeriodoGuia_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(drpPacienteGuia.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe o Paciente e tente novamente.");
            }
            else if (String.IsNullOrEmpty(txtDtGuiaIni.Text) || String.IsNullOrEmpty(txtDtGuiaFim.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe o período corretamente e tente novamente.");
                txtDtGuiaIni.Focus();
            }
            else
                CarregaAgendGuia();

            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        protected void lnkbImprimirGuia_OnClick(object sender, EventArgs e)
        {
            int paciente = !String.IsNullOrEmpty(drpPacienteGuia.SelectedValue) ? int.Parse(drpPacienteGuia.SelectedValue) : 0;
            var profissional = !String.IsNullOrEmpty(drpProfissionalGuia.SelectedValue) ? int.Parse(drpProfissionalGuia.SelectedValue) : LoginAuxili.CO_COL;
            int agend = int.Parse(ddlAgendGuia.SelectedValue);

            DateTime? dtIni = chkGuiaConsol.Checked ? !String.IsNullOrEmpty(txtDtGuiaIni.Text) ? DateTime.Parse(txtDtGuiaIni.Text) : (DateTime?)null : (DateTime?)null;
            DateTime? dtFim = chkGuiaConsol.Checked ? !String.IsNullOrEmpty(txtDtGuiaFim.Text) ? DateTime.Parse(txtDtGuiaFim.Text) : (DateTime?)null : (DateTime?)null;

            if (chkGuiaConsol.Checked && (!dtIni.HasValue || !dtFim.HasValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Para emitir a guia com procedimentos consolidados, preencha o período que deseja consolidar.");
                txtDtGuiaIni.Focus();
                AbreModalPadrao("AbreModalGuiaPlano();");
                return;
            }

            RptGuiaAtend rpt = new RptGuiaAtend();
            var retorno = rpt.InitReport(paciente, txtObsGuia.Text, drpOperGuia.SelectedValue, txtDtGuia.Text, profissional, agend, chkGuiaConsol.Checked, dtIni, dtFim);

            GerarRelatorioPadrão(rpt, retorno);
        }

        protected void lnkbRecCaixa_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("5103_RecebPagamCompromisso/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
        }

        protected void lnkbRecebimento_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("5102_Receber/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
        }

        protected void btnAnamRapida_Click(object sender, EventArgs e)
        {
            RptAnamneseRapida rpt = new RptAnamneseRapida();
            var retorno = rpt.InitReport(LoginAuxili.CO_EMP);

            GerarRelatorioPadrão(rpt, retorno);
        }

        protected void imgbPesqProfNome_OnClick(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlNomeProf, 0, true, "0", true);

            //ddlNomeProf.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisa(true);
            ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalEncaixe();",
                            true
                        );
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        protected void ddlNomeProf_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeProf.SelectedValue))
            {
                var coProfi = int.Parse(ddlNomeProf.SelectedValue);
                var profissional = TB03_COLABOR.RetornaPeloCoCol(coProfi);



                //  var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                //       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                //       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                //       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                //       where (tbs174.CO_EMP == LoginAuxili.CO_EMP)
                //       && where (tb03 == profissional )
                //        select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

                //ScriptManager.RegisterStartupScript(
                //             this.Page,
                //             this.GetType(),
                //             "Acao",
                //             "AbreModalEncaixe();",
                //             true
                //         );

            }
        }

        #region Finalizar Atendimento

        protected void imgPesqAgendMod_OnClick(object sender, EventArgs e)
        {
            int coCol = !string.IsNullOrEmpty(drpProSolicitado.SelectedValue) ? int.Parse(drpProSolicitado.SelectedValue) : 0;
            int coAlu = !string.IsNullOrEmpty(ddlPacienteFinalizar.SelectedValue) ? int.Parse(ddlPacienteFinalizar.SelectedValue) : 0;
            CarregaGridAgendMod(coCol, coAlu);
            AbreFinalizarAgend();
        }

        protected void imgPesProfSolicitado_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaProfSolicitado(true);

            string nome = txtProSolicitado.Text;

            var res = TB03_COLABOR.RetornaTodosRegistros()
                        .Where(x => x.NO_COL.Contains(nome) && x.FLA_PROFESSOR == "S" && x.CO_SITU_COL == "ATI")
                        .Select(x => new
                        {
                            NomeCol = x.NO_COL,
                            coCol = x.CO_COL
                        })
                         .OrderBy(x => x.NomeCol);

            drpProSolicitado.DataSource = res;
            drpProSolicitado.DataTextField = "NomeCol";
            drpProSolicitado.DataValueField = "coCol";
            drpProSolicitado.DataBind();

            drpProSolicitado.Items.Insert(0, new ListItem("Selecione", ""));
            AbreFinalizarAgend();
        }

        private void OcultarPesquisaProfSolicitado(bool ocultar)
        {
            txtProSolicitado.Visible =
            imgPesProfSolicitado.Visible = !ocultar;
            drpProSolicitado.Visible =
            imgVoltarPesProfSOlicitado.Visible = ocultar;
        }

        protected void imgVoltarPesProfSOlicitado_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaProfSolicitado(false);
            AbreFinalizarAgend();
        }

        protected void lnkFinalizarAgend_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in grdAgendMod.Rows)
            {
                try
                {
                    var hidAgendHorar = ((HiddenField)row.Cells[0].FindControl("hidIdAgendHorar")).Value;
                    bool chk = ((CheckBox)row.Cells[0].FindControl("chkFinalizar")).Checked;
                    if (chk && !string.IsNullOrEmpty(hidAgendHorar))
                    {
                        var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidAgendHorar));
                        #region Atualiza a agenda de ação

                        //Atualiza apenas que a ação foi realizada

                        tbs174.CO_SITUA_AGEND_HORAR = "R";
                        tbs174.FL_SITUA_ACAO = "R";
                        tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                        count += 1;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
                }
            }
            if (count == 0)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Nenhuma agenda foi selecionada.");
                AbreFinalizarAgend();
            }
            else
            {
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Agenda(s) finalizada(s) com sucesso!");
                CarregaConsultasAgendadas();
            }
        }

        protected void imgPesqPaciente_OnClick(object sender, EventArgs e)
        {
            if (txtNomePaciente.Text.Length < 3)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Digite pelo menos 3 letras para filtar por paciente.");
                txtNomePaciente.Focus();
                AbreFinalizarAgend();
                return;
            }

            OcultarPesquisaPaciente(true);
            string nome = txtNomePaciente.Text;

            var pac = TB07_ALUNO.RetornaTodosRegistros()
                      .Where(x => x.NO_ALU.Contains(nome) && x.CO_SITU_ALU == "A")
                      .Select(x => new
                      {
                          x.NO_ALU,
                          x.CO_ALU
                      }).OrderBy(x => x.NO_ALU);

            ddlPacienteFinalizar.DataSource = pac;
            ddlPacienteFinalizar.DataTextField = "NO_ALU";
            ddlPacienteFinalizar.DataValueField = "CO_ALU";
            ddlPacienteFinalizar.DataBind();
            ddlPacienteFinalizar.Items.Insert(0, new ListItem("Selecione", ""));
            AbreFinalizarAgend();
        }

        private void OcultarPesquisaPaciente(bool ocultar)
        {
            txtNomePaciente.Visible =
            imgPesqPaciente.Visible = !ocultar;
            ddlPacienteFinalizar.Visible =
            imgVoltarPesqPaciente.Visible = ocultar;
        }

        protected void imgVoltarPesqPaciente_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaPaciente(false);
            AbreFinalizarAgend();
        }

        protected void AbreFinalizarAgend_Click(object sender, EventArgs e)
        {
            //int local = ddlLocal.SelectedValue.Equals("") ? 0 : int.Parse(ddlLocal.SelectedValue);
            //if (local > 0)
            //{
            //    txtUnidadeFinalizar.Text = TB14_DEPTO.RetornaPelaChavePrimaria(local).NO_DEPTO;
            //}
            dtIniMod.Text = DateTime.Now.ToString();
            dtFimMod.Text = DateTime.Now.ToString();
            drpProSolicitado.DataSource = null;
            drpProSolicitado.DataBind();
            OcultarPesquisaProfSolicitado(false);
            ddlPacienteFinalizar.DataSource = null;
            ddlPacienteFinalizar.DataBind();
            OcultarPesquisaPaciente(false);
            grdAgendMod.DataSource = null;
            grdAgendMod.DataBind();
            AbreFinalizarAgend();
        }

        private void AbreFinalizarAgend()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreFinalizarAgend();",
                true
            );
        }

        private void CarregaGridAgendMod(int coCol, int coAlu)
        {
            DateTime dtIni = (String.IsNullOrEmpty(dtIniMod.Text) ? DateTime.Now.Date : DateTime.Parse(dtIniMod.Text));
            DateTime dtFim = (String.IsNullOrEmpty(dtFimMod.Text) ? DateTime.Now.Date : DateTime.Parse(dtFimMod.Text));
            //int local = ddlLocal.SelectedValue.Equals("") ? -1 : int.Parse(ddlLocal.SelectedValue);
            int local = -1;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU.Value equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                       where (coCol > 0 ? tb03.CO_COL == coCol : 0 == 0)
                       && (coAlu > 0 ? tb07.CO_ALU == coAlu : 0 == 0)
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (tbs174.CO_SITUA_AGEND_HORAR == "A")
                       && (tb07.CO_SITU_ALU == "A")
                       && (tb03.CO_SITU_COL == "ATI")
                       && (tbs174.ID_DEPTO_LOCAL_RECEP.HasValue ? (local == -1 ? 0 == 0 : tbs174.ID_DEPTO_LOCAL_RECEP == local) : (local == -1 ? 0 == 0 : tbs174.CO_DEPT == local))
                       select new GrdModal
                       {
                           CO_COL = tbs174.CO_COL.Value,
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           DT_AGEND = tbs174.DT_AGEND_HORAR,
                           CO_PACI = tbs174.CO_ALU,
                           HR_AGEND = tbs174.HR_AGEND_HORAR,
                           SITU = tbs174.CO_SITUA_AGEND_HORAR,
                           LOCAL = tb14.CO_SIGLA_DEPTO,
                           FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                           FL_CONF = tbs174.FL_CONF_AGEND,
                           FL_SITUA_TRIAGEM = tbs174.FL_SITUA_TRIAGEM
                       }).OrderBy(w => w.DT_AGEND).ThenBy(w => w.HR_AGEND).ToList();


            if (res.Count > 0)
            {
                grdAgendMod.DataSource = res;
                grdAgendMod.DataBind();
            }
            else
            {
                grdAgendMod.DataSource = null;
                grdAgendMod.DataBind();
            }
        }

        public class GrdModal
        {
            public int ID_AGEND_HORAR { get; set; }
            public DateTime DT_AGEND { get; set; }
            public String DE_DT_AGEND
            {
                get
                {
                    return this.DT_AGEND.ToShortDateString();
                }
            }
            public String HR_AGEND { get; set; }
            public String LOCAL { get; set; }
            public int? CO_PACI { get; set; }
            public String NO_PACI
            {
                get
                {
                    return (this.CO_PACI.HasValue ? TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).NO_ALU.Length > 45 ? TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).NO_ALU.Substring(0, 45) + "..." : TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).NO_ALU : " - ");
                }
            }
            public int CO_COL { get; set; }
            public String NO_PROF
            {
                get
                {
                    return (TB03_COLABOR.RetornaPeloCoCol(this.CO_COL).NO_APEL_COL.Length > 30 ? TB03_COLABOR.RetornaPeloCoCol(this.CO_COL).NO_APEL_COL.Substring(0, 30) + "..." : TB03_COLABOR.RetornaPeloCoCol(this.CO_COL).NO_APEL_COL);
                }
            }
            public String PASTA
            {
                get { return this.CO_PACI.HasValue ? !string.IsNullOrEmpty(TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).DE_PASTA_CONTR) ? TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).DE_PASTA_CONTR : "-" : "-"; }
            }
            public String SITU { get; set; }
            public String FL_AGEND_ENCAM { get; set; }
            public String FL_CONF { get; set; }
            public String FL_SITUA_TRIAGEM { get; set; }
            public String DE_SITU
            {
                get
                {
                    //Trata as situações possíveis
                    if (this.SITU == "A")
                    {
                        if (this.FL_AGEND_ENCAM == "S")
                            return "Encaminhado";
                        else if (this.FL_AGEND_ENCAM == "A")
                            return "Atendimento";
                        else if (this.FL_AGEND_ENCAM == "T")
                            return "Triagem";
                        else if (this.FL_CONF == "S" && this.FL_SITUA_TRIAGEM == "S")
                            return "Presente";
                        else if (this.FL_CONF == "S")
                            return "Presente";
                        else if ((this.CO_PACI != null && !TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).CO_SITU_ALU.Equals("I")) || (this.CO_PACI <= 0 && !TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).CO_SITU_ALU.Equals("I")))
                            return "Agendado";
                        else if (this.CO_PACI != null && TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).CO_SITU_ALU.Equals("I"))
                            return "Inativo";
                        else
                            return "Livre";
                    }
                    else if (this.SITU == "C")
                    {
                        return "Cancelado";
                    }
                    else if (this.SITU == "R")
                        return "Relalizado";
                    else if (this.SITU == "M")
                        return "Movimentado";
                    else
                        return "-";
                }
            }
        }

        #endregion

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