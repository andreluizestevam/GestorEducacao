
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: Registrar atendimento das consultas da tbs174, devidamente cruzando informações com os planejamentos   
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR  |   O.S.  | DESCRIÇÃO RESUMIDA
// ---------+-----------------------+---------+--------------------------------
// 14/07/14 | Maxwell Almeida       |         | Criação da funcionalidade para registro de atendimento Odontológico
// ---------+-----------------------+---------+--------------------------------
// 27/04/16 | Filipe Rodrigues      | FSP0035 | Alteração na exibição da lista de profissionais para não aparecer caso não seja master
// 11/10/17 | Diogo Gomes           |         | Inclusão do botao de óbito e alteração do local do botao salvar
//
// TIPOS DE SITUAÇÃO DE AGENDA (CO_SITUA_AGENDA_HORAR)
// A - EM ABERTO
// A - (FL_AGEND_ENCAM = S e FL_SITUA_TRIAGEM = S) - PRESENÇA APÓS RETORNO DA TRIAGEM
// A - (FL_AGEND_ENCAM = T) - ENCAMINHADO PARA TRIAGEM (AVALIAÇÃO DE RISCO)
// P - PRESENÇA
// E - ENCAMINHAMENTO PARA ATENDIMENTO CLINICO
// C - CANCELADO (FL_FALTA_JUSTIF = P - PACIENTE | FL_FALTA_JUSTIF = C - CLINICA | FL_FALTA_JUSTIF = S - FALTA JUSTIFICADA | FL_FALTA_JUSTIF = NULL OU "" - FALTA NÃO JUSTIFICADA)
// M - MOVIMENTAÇÃO DE AGENDA - ORIGEM
// W - MOVIMENTAÇÃO DE AGENDA - DESTINO
// X - ATENDIMENTO EM ESPERA
// R - ATENDIMENTO REALIZADO - NORMAL
// O - ATENDIMENTO REALIZADO - ÓBITO
// OBS: LINHA DE CONTROLE 1088
//
//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data.Objects;
using System.Reflection;
using System.Data;
using Resources;
using System.IO;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8221_AtendimentoOdonto;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;
using Excel;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8274_AtendimentoClinicoSimplificado
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

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

                var tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

                if (LoginAuxili.FLA_PROFESSOR != "S") // || tb03.CO_CLASS_PROFI == null || tb03.CO_CLASS_PROFI == "O")
                {
                    hdfAvisoPermissao.Value = "N";
                }

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                IniPeriAG.Text = FimPeriAG.Text = txtDtAtend.Text = data.ToString();
                txtIniAgenda.Text = data.AddDays(-5).ToString();
                txtFimAgenda.Text = data.AddDays(15).ToString();

                txtHrAtend.Text = data.ToShortTimeString();

                carregaLocal();
                if (tb03 != null && tb03.CO_DEPTO.HasValue)
                {
                    ddlLocal.SelectedValue = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL).CO_DEPTO.ToString();
                }
                else
                {
                    ddlLocal.SelectedValue = "";
                }
                carregarOperadoras(ddlOperOrc);
                carregarOperadoras(ddlOperProcPlan);
                carregarOperadoras(ddlOperPlanoServAmbu);
                CarregarPlanos(ddlPlanOrc, ddlOperOrc);
                CarregarPlanos(ddlPlanProcPlan, ddlOperProcPlan);
                CarregarPlanos(ddlOperPlanoServAmbu, ddlPlanoServAmbu);
                carregarQualificacaoProntuario(DropDownList1);
                CarregaAgendamentos();

                carregaClassificacaoRisco();
                CarregaGrupoMedicamento(ddlGrupo);
                CarregaGrupoMedicamento(drpGrupoMedic, true);
                CarregaSubGrupoMedicamento(ddlGrupo, ddlSubGrupo);
                CarregaSubGrupoMedicamento(drpGrupoMedic, drpSubGrupoMedic, true);
                CarregaUnidade();
                CarregarBotoes();
                CarregaGrupos();
                CarregaOperadoras();
                CarregaSubGrupos();
                Datas_Cons_Paciente(-1);
                CarregarProfissionais(drpProfResp);

                //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
                if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M"))
                    drpProfResp.Enabled = false;

                if ((LoginAuxili.FLA_PROFESSOR == "S"))
                    drpProfResp.SelectedValue = LoginAuxili.CO_COL.ToString();

                txtVlBase.Enabled = txtVlCusto.Enabled = txtVlRestitu.Enabled = true;
                if (!String.IsNullOrEmpty(drpProfResp.SelectedValue))
                {
                    CarregaModelosReceituario(DropDownListModelo, int.Parse(drpProfResp.SelectedValue), "");
                    CarregaModelosExames(DropDownListModeloExames, int.Parse(drpProfResp.SelectedValue),"");
                    CarregaModelosAmbulatorio(DropDownListModeloAmb, int.Parse(drpProfResp.SelectedValue), "");
                    CarregaModelosEncam(DropDownListModeloEncam, int.Parse(drpProfResp.SelectedValue), "");
                }
                if (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_CLASS_PROFI != "D")
                    divBtnOdontograma.Visible = false;
            }
        }

        protected void Datas_Cons_Paciente(int idPaciente)
        {
            var ListaDatas = new List<TBS174_AGEND_HORAR>();
            //DateTime? dataFim;
            DateTime data = DateTime.Now;

            if (idPaciente != null && idPaciente > 0)
            {
                ListaDatas = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.CO_ALU == idPaciente).OrderBy(a => a.DT_AGEND_HORAR).ToList();

                txtIniAgenda.Text = ListaDatas.FirstOrDefault().DT_AGEND_HORAR.ToString();
                txtFimAgenda.Text = ListaDatas.LastOrDefault().DT_AGEND_HORAR.ToString();
            }
            else
            {
                txtIniAgenda.Text = data.AddDays(-5).ToString();
                txtFimAgenda.Text = data.AddDays(15).ToString();
            }
        }


        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            Persistencias(false);
        }

        protected void BtnObito_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : 0;

            if (idAtendAgend > 0)
            {
                var tbs454 = TBS454_OBITO.RetornarUmRegistro(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend);
                if (tbs454 != null)
                {
                    tbs454.TBS390_ATEND_AGENDReference.Load();
                    tbs454.TBS390_ATEND_AGEND.TB07_ALUNOReference.Load();
                    txtPacienteObito.Text = tbs454.TBS390_ATEND_AGEND.TB07_ALUNO.NO_ALU;
                    txtRegAtendimentoObito.Text = tbs454.TBS390_ATEND_AGEND.NU_REGIS;
                    txtDataObito.Text = tbs454.DT_OBITO.ToShortDateString();
                    txtHoraObito.Text = tbs454.HR_OBITO.Hours + ":" + (tbs454.HR_OBITO.Minutes > 0 ? tbs454.HR_OBITO.Minutes.ToString() : "00");
                }
                else
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                    tbs390.TB07_ALUNOReference.Load();
                    txtPacienteObito.Text = tbs390.TB07_ALUNO.NO_ALU;
                    txtRegAtendimentoObito.Text = tbs390.NU_REGIS;
                    txtDataObito.Text = DateTime.Now.ToShortDateString();
                    txtHoraObito.Text = DateTime.Now.ToShortTimeString();
                }

                AbreModalPadrao("AbreModalObito();");
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento foi encontrado, por favor selecione um ou salve o registro corrente.");
            }
        }

        protected void btnSalvarObito_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : 0;
            try
            {
                if (idAtendAgend > 0)
                {
                    var res = TBS454_OBITO.RetornarUmRegistro(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend);
                    if (res == null)
                    {
                        var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                        tbs390.TB07_ALUNOReference.Load();
                        var tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(tbs390.TB07_ALUNO.CO_ALU, tbs390.TB07_ALUNO.CO_EMP);
                        //Situação do paciente passa a ser Obito
                        tb07.CO_SITU_ALU = "O";

                        var tbs454 = new TBS454_OBITO();
                        tbs454.TBS390_ATEND_AGEND = tbs390;
                        tbs454.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs454.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs454.DE_OBITO = txtObsObito.Text;
                        tbs454.DT_CADAS = DateTime.Now;
                        tbs454.DT_OBITO = DateTime.Parse(txtDataObito.Text);
                        tbs454.HR_OBITO = TimeSpan.Parse(txtHoraObito.Text);
                        tbs454.IP_CADAS = Request.UserHostAddress;

                        TBS454_OBITO.SaveOrUpdate(tbs454, true);
                        TB07_ALUNO.SaveOrUpdate(tb07, true);

                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro efetuado com sucesso.", Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        throw new ArgumentException("O registro do paciente já consta como Óbito.");
                    }
                }
                else
                {
                    throw new ArgumentException("Nenhum atendimento foi encontrado, por favor selecione um ou salve o registro corrente.");
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Realiza as persistências pertinentes às informações de atendimento
        /// 
        /// </summary>
        private void Persistencias(Boolean finalizar)
        {
            try
            {
                #region Validações

                /*if (!string.IsNullOrEmpty(hidIdAtendimento.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Atendimento já finalizado!");
                    return;
                }*/
               
                if (string.IsNullOrEmpty(hidIdAgenda.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda que deseja atender!");
                    grdPacientes.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(drpProfResp.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o profissional responsável pelo atendimento!");
                    drpProfResp.Focus();
                    return;
                }

                #region Valida Procedimento Orçamento

                //Verifica se os campos do procedimento de orçamento foram informados
                foreach (GridViewRow i in grdProcedOrcam.Rows)
                {
                    DropDownList ddlProc = (((DropDownList)i.FindControl("ddlProcedOrc")));
                    TextBox txtQtd = (((TextBox)i.FindControl("txtQtdProcedOrc")));

                    if (string.IsNullOrEmpty(ddlProc.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, string.Format("Na linha nº {0} do orçamento o procedimento não está informado", i.RowIndex + 1));
                        ddlProc.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtQtd.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, string.Format("Na linha nº {0} do orçamento a quantidade não está informada", i.RowIndex + 1));
                        txtQtd.Focus();
                        return;
                    }
                }

                #endregion

                #region Valida Exames
                if (PanelExamesPesquisa.Visible)
                {
                    foreach (GridViewRow i in grdExame.Rows)
                    {
                        DropDownList ddlExame = (DropDownList)i.FindControl("ddlExame");

                        if (string.IsNullOrEmpty(ddlExame.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, string.Format("Na linha nº {0} do procedimento de plano de saúde, o procedimento não está informado", i.RowIndex + 1));
                            ddlExame.Focus();
                            return;
                        }
                    }
                }

                #endregion

                #endregion
                 
                #region Persistências

                if (finalizar)
                    ExecutarFuncaoPadrao("PararCronometro();");

                #region Grava o atendimento

                var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

                TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);

                if (tbs390.ID_ATEND_AGEND == 0)
                {
                    #region Trata sequencial
                    //Trata para gerar um Código do Encaminhamento
                    var res2 = (from tbs390pesq in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                select new { tbs390pesq.NU_REGIS }).OrderByDescending(w => w.NU_REGIS).FirstOrDefault();

                    string seq;
                    int seq2;
                    int seqConcat;
                    string seqcon;
                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                    string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                    if (res2 == null)
                        seq2 = 1;
                    else
                    {
                        seq = res2.NU_REGIS.Substring(6, 6);
                        seq2 = int.Parse(seq);
                    }

                    seqConcat = seq2 + 1;
                    seqcon = seqConcat.ToString().PadLeft(6, '0');

                    string CodigoAtendimento = string.Format("AT{0}{1}{2}", ano, mes, seqcon);
                    #endregion

                    tbs390.TBS174_AGEND_HORAR = tbs174;
                    tbs390.TB07_ALUNO = tb07;
                    tbs390.NU_REGIS = CodigoAtendimento;

                    //Dados de quem cadastrou o atendimento
                    tbs390.DT_CADAS = DateTime.Now;
                    tbs390.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs390.CO_EMP_COL_CADAS = emp_col;
                    tbs390.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs390.IP_CADAS = Request.UserHostAddress;
                }


                tbs390.DE_CONSI = hidTxtObserv.Value;
               //d tbs390.DE_QXA_PRINC = txtQueixa.Text;
                tbs390.DE_HDA = txtHDA.Text;


                tbs390.FL_SITU_FATU = hidCkOrcamAprov.Value == "S" ? "A" : "N";//Aprovado ou Negado



                tbs390.NU_SENHA_ATEND = txtSenha.Text;
                tbs390.CO_TIPO_RISCO = !String.IsNullOrEmpty(ddlClassRisco.SelectedValue) ? int.Parse(ddlClassRisco.SelectedValue) : (int?)null;

                //Dados de quem realizou o atendimento
                var data = !String.IsNullOrEmpty(txtDtAtend.Text) ? txtDtAtend.Text : DateTime.Now.ToShortDateString();
                var hora = !String.IsNullOrEmpty(txtHrAtend.Text) ? txtHrAtend.Text : DateTime.Now.ToShortTimeString();
                tbs390.DT_REALI = DateTime.Parse(data).Add(TimeSpan.Parse(hora));
                tbs390.CO_COL_ATEND = int.Parse(drpProfResp.SelectedValue);
                tbs390.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue)).CO_COL;
                tbs390.DT_VALID_ORCAM = (!string.IsNullOrEmpty(hidDtValidOrcam.Value) ? DateTime.Parse(hidDtValidOrcam.Value) : (DateTime?)null);
                tbs390.VL_DSCTO_ORCAM = (!string.IsNullOrEmpty(txtVlDscto.Text) ? decimal.Parse(txtVlDscto.Text) : (decimal?)null);
                tbs390.VL_TOTAL_ORCAM = (!string.IsNullOrEmpty(txtVlTotalOrcam.Text) ? decimal.Parse(txtVlTotalOrcam.Text) : (decimal?)null);

                if (LoginAuxili.CO_DEPTO != 0)
                    tbs390.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(LoginAuxili.CO_DEPTO);

                //Dados da situação do atendimento
                tbs390.CO_SITUA = finalizar ? "F" : "A";
                tbs390.DT_SITUA = DateTime.Now;
                tbs390.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs390.CO_EMP_COL_SITUA = emp_col;
                tbs390.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs390.IP_SITUA = Request.UserHostAddress;

                TBS390_ATEND_AGEND.SaveOrUpdate(tbs390);
                hidIdAtendimento.Value = tbs390.ID_ATEND_AGEND.ToString();
                var tbs400 = new TBS400_PRONT_MASTER();
                tbs400.ANAMNSE = txtHDA.Text;
                GravarProntuario(tb07, tbs400);
                #endregion

                #region Atualiza a agenda de ação

                //Atualizo apenas que a ação foi realizada

                tbs174.FL_SITUA_ACAO = "R";
                tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

                #endregion

                #region Atualiza o Paciente





                #endregion

                #region Atualiza a agenda de atendimento
                var atend = 0;
                //Aqui atualizo que a agenda foi atendida
                foreach (GridViewRow i in grdPacientes.Rows)
                {
                    if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
                    {
                        atend = int.Parse(((HiddenField)i.FindControl("hidIdAgenda")).Value);
                        TBS174_AGEND_HORAR tbs174ob = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(atend);

                        if (finalizar)
                            tbs174ob.CO_SITUA_AGEND_HORAR = "R";
                        tbs174ob.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs174ob.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs174ob.DT_SITUA_AGEND_HORAR = DateTime.Now;
                        tbs174ob.HR_ATEND_FIM = DateTime.Now;

                        if (!String.IsNullOrEmpty(hidHoras.Value) && !String.IsNullOrEmpty(hidMinutos.Value))
                            tbs174ob.HR_DURACAO_ATEND = int.Parse(hidHoras.Value).ToString("D2") + ":" + int.Parse(hidMinutos.Value).ToString("D2");

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ob, true);

                        continue;
                    }
                }

                #endregion

                #region Armazena o Orçamento

                var tbs396s = TBS396_ATEND_ORCAM.RetornaTodosRegistros().Where(o => o.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs390.ID_ATEND_AGEND).ToList();

                foreach (var tbs396 in tbs396s)
                    TBS396_ATEND_ORCAM.Delete(tbs396, true);

                //Realiza as persistências do orçamento
                foreach (GridViewRow i in grdProcedOrcam.Rows)
                {
                    DropDownList ddlProc = (((DropDownList)i.FindControl("ddlProcedOrc")));
                    TextBox txtQtd = (((TextBox)i.FindControl("txtQtdProcedOrc")));
                    TextBox txtValor = (((TextBox)i.FindControl("txtValorProcedOrc")));

                    TBS396_ATEND_ORCAM tbs396 = new TBS396_ATEND_ORCAM();
                    tbs396.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue));
                    tbs396.TBS390_ATEND_AGEND = tbs390;
                    tbs396.QT_PROC = int.Parse(txtQtd.Text);
                    tbs396.VL_PROC = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : 0);
                    tbs396.DE_OBSER = hidObsOrcam.Value;
                    tbs396.NU_REGIS = tbs390.NU_REGIS;

                    tbs396.DT_CADAS = DateTime.Now;
                    tbs396.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs396.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs396.CO_EMP_COL_CADAS = emp_col;
                    tbs396.IP_CADAS = Request.UserHostAddress;
                    TBS396_ATEND_ORCAM.SaveOrUpdate(tbs396, true);
                }

                #endregion

                #region Armazena os Medicamentos

                SalvaMedicamentos();
                #endregion

                #region Armazena os Exames


                SalvaExames();
                #endregion

               
                #endregion
            //Encaminhamento
                SalvaEncam();
                RecarregarGrids(atend, tbs174.CO_ALU.Value);

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Atendimento " + (finalizar ? "FINALIZADO" : "SALVO") + " com sucesso!");
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar! Entre em contato com o suporte! Erro: " + e.Message);
            }
        }

        private void SalvaEncam()
        {
            var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

            TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            if (!string.IsNullOrEmpty(ASPxHtmlEditorEncam.Html))
            {

                TBS462_ATEND_ENCAM_LIVRE tbs462 = new TBS462_ATEND_ENCAM_LIVRE();

                tbs462.TBS390_ATEND_AGEND = tbs390;
                tbs462.DE_OBSER = (!string.IsNullOrEmpty(TextBoxObserEncam.Text) ? TextBoxObserEncam.Text : null);
                tbs462.DT_CADAS = DateTime.Now;
                tbs462.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs462.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs462.CO_EMP_COL_CADAS = emp_col;
                tbs462.DE_CONTE_MODEL = ASPxHtmlEditorEncam.Html;
                if (!string.IsNullOrEmpty(DropDownListModeloExames.SelectedValue))
                    tbs462.ID_ATEND_ENCAM_LIVRE = int.Parse(DropDownListModeloEncam.SelectedValue);

                tbs462.IP_CADAS = Request.UserHostAddress;
                TBS462_ATEND_ENCAM_LIVRE.SaveOrUpdate(tbs462, true);
                var tbs400 = new TBS400_PRONT_MASTER();
                tbs400.ANAMNSE = "Encaminhamento Modelo Livre - Nº " + tbs462.ID_ATEND_ENCAM_LIVRE;
                GravarProntuario(tb07, tbs400);
                //ASPxHtmlEditorEncam.Html = "";
            }
        }

        private void SalvaExames()
        {
            var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

            TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            if (PanelExamesPesquisa.Visible)
            {
                foreach (var tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros().Where(e => e.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs390.ID_ATEND_AGEND).ToList())
                    TBS398_ATEND_EXAMES.Delete(tbs398, true);
                foreach (GridViewRow i in grdExame.Rows)
                {
                    DropDownList ddlExame = (DropDownList)i.FindControl("ddlExame");

                    TBS398_ATEND_EXAMES tbs398 = new TBS398_ATEND_EXAMES();
                    tbs398.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlExame.SelectedValue));
                    tbs398.TBS390_ATEND_AGEND = tbs390;
                    tbs398.DE_OBSER = (!string.IsNullOrEmpty(hidObserExame.Value) ? hidObserExame.Value : null);
                    tbs398.FL_TIPO_PRESC_EXAMES = "P";
                    tbs398.DT_CADAS = DateTime.Now;
                    tbs398.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs398.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs398.CO_EMP_COL_CADAS = emp_col;
                    tbs398.IP_CADAS = Request.UserHostAddress;
                    TBS398_ATEND_EXAMES.SaveOrUpdate(tbs398, true);
                    var tbs400 = new TBS400_PRONT_MASTER();
                    tbs400.ANAMNSE = "Prescrição de Exames Modelo Pesquisa - N° " + tbs398.ID_ATEND_EXAMES;
                    GravarProntuario(tb07, tbs400);
                }
            }
            else if (PanelExamesLivre.Visible && !string.IsNullOrEmpty(ASPxHtmlEditorExame.Html))
            {

                TBS398_ATEND_EXAMES tbs398 = new TBS398_ATEND_EXAMES();

                tbs398.TBS390_ATEND_AGEND = tbs390;
                tbs398.DE_OBSER = (!string.IsNullOrEmpty(hidObserExame.Value) ? hidObserExame.Value : null);
                tbs398.DE_CONTE_MODEL_MEDIC = ASPxHtmlEditorExame.Html;
                if (!string.IsNullOrEmpty(DropDownListModeloExames.SelectedValue))
                    tbs398.TBS461_ATEND_MODEL_PRESC_MEDIC = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloExames.SelectedValue));
                tbs398.FL_TIPO_PRESC_EXAMES = "L";
                tbs398.DT_CADAS = DateTime.Now;
                tbs398.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs398.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs398.CO_EMP_COL_CADAS = emp_col;
                tbs398.IP_CADAS = Request.UserHostAddress;
                TBS398_ATEND_EXAMES.SaveOrUpdate(tbs398, true);
                var tbs400 = new TBS400_PRONT_MASTER();
                tbs400.ANAMNSE = "Prescrição de Exames Modelo Livre - Nº " + tbs398.ID_ATEND_EXAMES;
                GravarProntuario(tb07, tbs400);
                // ASPxHtmlEditorExame.Html = "";
            }
        }

        private void SalvaMedicamentos()
        {
            var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

            TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            if (PanelPesquisa.Visible)
            {
                foreach (var tbs399 in TBS399_ATEND_MEDICAMENTOS.RetornaTodosRegistros().Where(m => m.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs390.ID_ATEND_AGEND).ToList())
                    TBS399_ATEND_MEDICAMENTOS.Delete(tbs399, true);
                //Realiza as persistências do orçamento
                foreach (GridViewRow i in grdMedicamentos.Rows)
                {
                    var lblIdMedic = (Label)i.FindControl("lblIdMedic");
                    var lblPresc = (Label)i.FindControl("lblPrescricao");
                    var lblUso = (Label)i.FindControl("lblUso");
                    var lblQtd = (Label)i.FindControl("lblQtd");

                    TBS399_ATEND_MEDICAMENTOS tbs399 = new TBS399_ATEND_MEDICAMENTOS();
                    tbs399.TB90_PRODUTO = TB90_PRODUTO.RetornaPeloCoProd(int.Parse(lblIdMedic.Text));
                    tbs399.QT_MEDIC = (!string.IsNullOrEmpty(lblQtd.Text) ? int.Parse(lblQtd.Text) : (int?)null);
                    tbs399.QT_USO = (!string.IsNullOrEmpty(lblUso.Text) ? int.Parse(lblUso.Text) : (int?)null);
                    tbs399.DE_PRESC = (!string.IsNullOrEmpty(lblPresc.Text) ? lblPresc.Text : null);
                    tbs399.DE_PRINC_ATIVO = tbs399.TB90_PRODUTO.NO_PRINCIPIO_ATIVO;
                    tbs399.DE_OBSER = (!string.IsNullOrEmpty(hidObserMedicam.Value) ? hidObserMedicam.Value : null);
                    tbs399.TBS390_ATEND_AGEND = tbs390;
                    // tbs399.TBS461_ATEND_MODEL_PRESC_MEDIC = RadioButtonLivre.Checked ? TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModelo.SelectedValue)) : null;
                    tbs399.DE_CONTE_MODEL_MEDIC = RadioButtonLivre.Checked ? ASPxHtmlEditor1.Html : "";
                    tbs399.FL_RECEI_CONTR_ESPEC = RadioButtonContEspecial.Checked ? "S" : "N";
                    tbs399.FL_TIPO_PRESC_MEDIC = "P";
                    tbs399.DT_CADAS = DateTime.Now;
                    tbs399.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs399.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs399.CO_EMP_COL_CADAS = emp_col;
                    tbs399.IP_CADAS = Request.UserHostAddress;
                    TBS399_ATEND_MEDICAMENTOS.SaveOrUpdate(tbs399, true);
                    var tbs400 = new TBS400_PRONT_MASTER();
                    tbs400.ANAMNSE = "Prescrição de Medicamentos Modelo Pesquisa Medicamentos - N° " + tbs399.ID_ATEND_MEDICAMENTOS;
                    GravarProntuario(tb07, tbs400);

                }

            }
            else if (PanelLivre.Visible && !string.IsNullOrEmpty(ASPxHtmlEditor1.Html))
            {
                TBS399_ATEND_MEDICAMENTOS tbs399 = new TBS399_ATEND_MEDICAMENTOS();

                //tbs399.DE_PRINC_ATIVO = tbs399.TB90_PRODUTO.NO_PRINCIPIO_ATIVO;
                tbs399.DE_OBSER = (!string.IsNullOrEmpty(hidObserMedicam.Value) ? hidObserMedicam.Value : null);
                tbs399.TBS390_ATEND_AGEND = tbs390;
                tbs399.TBS461_ATEND_MODEL_PRESC_MEDIC = string.IsNullOrEmpty(DropDownListModelo.SelectedValue) ? null : TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModelo.SelectedValue));
                tbs399.DE_CONTE_MODEL_MEDIC = ASPxHtmlEditor1.Html;
                tbs399.FL_TIPO_PRESC_MEDIC = "L";
                tbs399.FL_RECEI_CONTR_ESPEC = RadioButtonContEspecial.Checked ? "S" : "N";
                tbs399.DT_CADAS = DateTime.Now;
                tbs399.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs399.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs399.CO_EMP_COL_CADAS = emp_col;
                tbs399.IP_CADAS = Request.UserHostAddress;
                TBS399_ATEND_MEDICAMENTOS.SaveOrUpdate(tbs399, true);


                var tbs400 = new TBS400_PRONT_MASTER();
                tbs400.ANAMNSE = "Prescrição de Medicamentos Modelo Livre - N° " + tbs399.ID_ATEND_MEDICAMENTOS;
                GravarProntuario(tb07, tbs400);
                ASPxHtmlEditor1.Html = "";
            }
        }

        private void GravarProntuario(TB07_ALUNO tb07, TBS400_PRONT_MASTER tbs400)
        {
            tbs400.CO_ALU = tb07.CO_ALU;
            tbs400.CO_COL = LoginAuxili.CO_COL;
            tbs400.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs400.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs400.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs400.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
            tbs400.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
            tbs400.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs400.CO_SITUA = "A";
            tbs400.DT_CADAS = DateTime.Now;
            tbs400.DT_SITUA = DateTime.Now;
            tbs400.IP_CADAS = Request.UserHostAddress;
            tbs400.IP_SITUA = Request.UserHostAddress;
            tbs400.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(LoginAuxili.CO_DEPTO);
            tbs400.TBS418_CLASS_PRONT = RetornaProntuarioPadrao();

            var ultimoElemento = TBS400_PRONT_MASTER.RetornaTodosRegistros().ToList().OrderByDescending(x => x.ID_PRONT_MASTER).FirstOrDefault();
            string nuRegis = (DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ultimoElemento.ID_PRONT_MASTER + 1).ToString();
            tbs400.NU_REGIS = nuRegis;
            tbs400.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tbs400.TBS390_ATEND_AGEND = String.IsNullOrEmpty(hidIdAtendimento.Value) ? null : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
            tbs400.TBS401_PRONT_INTENS = null;

            TBS400_PRONT_MASTER.SaveOrUpdate(tbs400);
        }

        /// <summary>
        /// Carrega as classificações de risco
        /// </summary>
        private void carregaClassificacaoRisco()
        {
            AuxiliCarregamentos.CarregaClassificacaoRisco(ddlClassRisco, false);
        }

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo2, false, true, false, C2BR.GestorEducacao.UI.Library.Auxiliares.AuxiliCarregamentos.ETiposClassificacoes.atendimento);
        }

        private void carregaLocal()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_CONSU.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_SIGLA_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocal.DataTextField = "NO_DEPTO";
            ddlLocal.DataValueField = "CO_DEPTO";
            ddlLocal.DataSource = res;
            ddlLocal.DataBind();

            ddlLocal.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Carrega so SubGrupos
        /// </summary>
        private void CarregaSubGrupos()
        {
            int coGrupo = ddlGrupo2.SelectedValue != "" ? int.Parse(ddlGrupo2.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo2, coGrupo, false);
        }

        /// <summary>
        /// Carrega as Operadoras de Planos de Saúde
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOper, false, false);
            ddlOper.Items.Insert(0, new ListItem("", ""));
        }

        protected void carregaGridOrcamento(int idAtendAgend)
        {
            DataTable dtV = CriarColunasELinhaGridOrcamento();

            if (idAtendAgend != 0)
            {
                var res = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                           where tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend
                           select new
                           {
                               ID_PROC_MEDI_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE != null ? tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE : 0,
                               NM_PROC_MEDI_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE != null ? tbs396.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI : "",
                               tbs396.QT_PROC,
                               tbs396.VL_PROC,
                               tbs396.DE_OBSER
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["PROCED"] = i.ID_PROC_MEDI_PROCE;
                    linha["NMPROCED"] = i.NM_PROC_MEDI_PROCE;
                    linha["QTD"] = i.QT_PROC;
                    linha["VALOR"] = i.VL_PROC;
                    linha["VALORUNIT"] = "";
                    dtV.Rows.Add(linha);

                    txtObsOrcam.Text = hidObsOrcam.Value = i.DE_OBSER;
                }
            }

            HttpContext.Current.Session.Add("GridSolic_PROC_ORC", dtV);

            carregaGridNovaComContextoOrcamento();
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridOrcamento(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridOrcamento();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_ORC"] = dtV;

            carregaGridNovaComContextoOrcamento();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridOrcamento()
        {
            DataTable dtV = CriarColunasELinhaGridOrcamento();

            DataRow linha = dtV.NewRow();
            linha["PROCED"] = "";
            linha["NMPROCED"] = "";
            linha["QTD"] = "1";
            linha["VALOR"] = "";
            linha["VALORUNIT"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_PROC_ORC"] = dtV;

            carregaGridNovaComContextoOrcamento();
        }

        private DataTable CriarColunasELinhaGridOrcamento()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NMPROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTD";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALORUNIT";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                linha = dtV.NewRow();
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProcedOrc")).SelectedValue);
                linha["NMPROCED"] = (((TextBox)li.FindControl("txtCodigProcedOrc")).Text);
                linha["QTD"] = (((TextBox)li.FindControl("txtQtdProcedOrc")).Text);
                linha["VALOR"] = (((TextBox)li.FindControl("txtValorProcedOrc")).Text);
                linha["VALORUNIT"] = (((HiddenField)li.FindControl("hidValUnitProc")).Value);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContextoOrcamento()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_ORC"];

            grdProcedOrcam.DataSource = dtV;
            grdProcedOrcam.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                DropDownList ddlCodigoi;
                TextBox txtNmProced, txtQtdProced, txtVlProced;
                HiddenField hidVlUnit;
                ddlCodigoi = ((DropDownList)li.FindControl("ddlProcedOrc"));
                txtNmProced = ((TextBox)li.FindControl("txtCodigProcedOrc"));
                txtQtdProced = ((TextBox)li.FindControl("txtQtdProcedOrc"));
                txtVlProced = ((TextBox)li.FindControl("txtValorProcedOrc"));
                hidVlUnit = ((HiddenField)li.FindControl("hidValUnitProc"));

                string codigo, nmProced, qtdProced, vlProced, vlUnitProced;

                //Coleta os valores do dtv da modal popup
                codigo = dtV.Rows[aux]["PROCED"].ToString();
                nmProced = dtV.Rows[aux]["NMPROCED"].ToString();
                qtdProced = dtV.Rows[aux]["QTD"].ToString();
                vlProced = dtV.Rows[aux]["VALOR"].ToString();
                vlUnitProced = dtV.Rows[aux]["VALORUNIT"].ToString();

                var opr = !String.IsNullOrEmpty(ddlOperOrc.SelectedValue) ? int.Parse(ddlOperOrc.SelectedValue) : 0;

                var tb03 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);

                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where (opr != 0 ? tbs356.TB250_OPERA.ID_OPER == opr : 0 == 0)
                           && (tbs356.FL_PROC_PROFISSIONAL_SAUDE == (tb03.FL_TECNICO == "S" ? "N" : "S")
                           || tbs356.FL_PROC_TECNICO == (tb03.FL_TECNICO == "S" ? "S" : "N"))
                           select new
                           {
                               ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                               NM_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI,
                           }).OrderBy(w => w.NM_PROC_MEDI).ToList();

                if (res != null)
                {
                    ddlCodigoi.DataTextField = "NM_PROC_MEDI";
                    ddlCodigoi.DataValueField = "ID_PROC_MEDI_PROCE";
                    ddlCodigoi.DataSource = res;
                    ddlCodigoi.DataBind();

                    ddlCodigoi.Items.Insert(0, new ListItem("Selecione", ""));
                    ddlCodigoi.SelectedValue = codigo;
                }

                txtNmProced.Text = nmProced;
                txtQtdProced.Text = qtdProced;
                txtVlProced.Text = vlProced;
                aux++;
            }

            CarregarValoresTotaisFooter();
        }

        protected void carregaGridExame(int idAtendAgend)
        {
            DataTable dtV = CriarColunasELinhaGridExame();

            if (idAtendAgend != 0)
            {
                var res = (from tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros()
                           where tbs398.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend
                           select new
                           {
                               ID_PROC_MEDI_PROCE = tbs398.TBS356_PROC_MEDIC_PROCE != null ? tbs398.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE : 0,
                               NM_PROC_MEDI_PROCE = tbs398.TBS356_PROC_MEDIC_PROCE != null ? tbs398.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI : "",
                               tbs398.DE_OBSER
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["PROCED"] = i.ID_PROC_MEDI_PROCE;
                    linha["NMPROCED"] = i.NM_PROC_MEDI_PROCE;
                    dtV.Rows.Add(linha);

                    txtObserExame.Text = hidObserExame.Value = i.DE_OBSER;
                }
            }

            HttpContext.Current.Session.Add("GridSolic_PROC_PLA", dtV);

            carregaGridNovaComContextoExame();
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridExame(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridExame();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_PLA"] = dtV;

            carregaGridNovaComContextoExame();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridExame()
        {
            DataTable dtV = CriarColunasELinhaGridExame();

            DataRow linha = dtV.NewRow();
            linha["PROCED"] = "";
            linha["NMPROCED"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_PROC_PLA"] = dtV;

            carregaGridNovaComContextoExame();
        }

        private DataTable CriarColunasELinhaGridExame()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NMPROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALOR";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdExame.Rows)
            {
                linha = dtV.NewRow();
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlExame")).SelectedValue);
                linha["NMPROCED"] = (((TextBox)li.FindControl("txtCodigProcedPla")).Text);
                linha["VALOR"] = (((TextBox)li.FindControl("txtValorProced")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        /// <summary>
        /// Carrega o a DataTable em Session (procedimentos do Plano de Saude) com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContextoExame()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_PLA"];

            grdExame.DataSource = dtV;
            grdExame.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdExame.Rows)
            {
                DropDownList ddlCodigoi;
                TextBox txtNmProced;
                TextBox valorProced;
                ddlCodigoi = ((DropDownList)li.FindControl("ddlExame"));
                txtNmProced = ((TextBox)li.FindControl("txtCodigProcedPla"));
                valorProced = ((TextBox)li.FindControl("txtValorProced"));

                string codigo, nmProced, vlrProced;

                //Coleta os valores do dtv da modal popup
                codigo = dtV.Rows[aux]["PROCED"].ToString();
                nmProced = dtV.Rows[aux]["NMPROCED"].ToString();
                vlrProced = dtV.Rows[aux]["VALOR"].ToString();

                var opr = 0;

                if (!String.IsNullOrEmpty(ddlPlanProcPlan.SelectedValue) && int.Parse(ddlPlanProcPlan.SelectedValue) != 0)
                {
                    var plan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanProcPlan.SelectedValue));
                    plan.TB250_OPERAReference.Load();
                    opr = plan.TB250_OPERA != null ? plan.TB250_OPERA.ID_OPER : 0;
                }

                CarregarProcedimentos(ddlCodigoi, opr, "EX");
                ddlCodigoi.SelectedValue = codigo;
                txtNmProced.Text = nmProced;
                valorProced.Text = vlrProced;
                aux++;
            }
        }

        protected void carregaGridMedic(int idAtendAgend)
        {
            DataTable dtV = CriarColunasELinhaGridMedic();
            try
            {
                if (idAtendAgend != 0)
                {
                    var res = (from tbs399 in TBS399_ATEND_MEDICAMENTOS.RetornaTodosRegistros()
                               where tbs399.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend
                               select new
                               {
                                   tbs399.TB90_PRODUTO.CO_PROD,
                                   tbs399.TB90_PRODUTO.NO_PROD,
                                   tbs399.QT_MEDIC,
                                   tbs399.QT_USO,
                                   tbs399.DE_PRESC,
                                   tbs399.DE_OBSER
                               }).ToList();

                    foreach (var i in res)
                    {
                        var linha = dtV.NewRow();
                        linha["ID_MEDIC"] = i.CO_PROD;
                        linha["MEDIC"] = i.NO_PROD;
                        linha["USO"] = i.QT_USO;
                        linha["QTD"] = i.QT_MEDIC;
                        linha["PRESC"] = i.DE_PRESC;
                        dtV.Rows.Add(linha);
                        txtObserMedicam.Text = hidObserMedicam.Value = i.DE_OBSER;
                    }
                }
            }catch(Exception){
                try
                {
                    var medicametos = (from tbs399 in TBS399_ATEND_MEDICAMENTOS.RetornaTodosRegistros()
                               where tbs399.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend
                               select new
                               {
                                   tbs399.DE_CONTE_MODEL_MEDIC,
                                   tbs399.TBS461_ATEND_MODEL_PRESC_MEDIC.ID_MODEL_MEDIC
                                 
                               }).ToList();
                    ASPxHtmlEditor1.Html = medicametos.First().DE_CONTE_MODEL_MEDIC;
                    DropDownListModelo.SelectedValue = medicametos.First().ID_MODEL_MEDIC.ToString();
                }catch(Exception){
                
                }
            }
            HttpContext.Current.Session.Add("GridSolic_PROC_MEDIC", dtV);

            carregaGridNovaComContextoMedic();
        }

        /// <summary>
        /// Exclui item (medicamento) da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGridMedic(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridMedic();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_MEDIC"] = dtV;

            carregaGridNovaComContextoMedic();
        }

        /// <summary>
        /// Cria uma nova linha na Grid de Medicamentos
        /// </summary>
        protected void CriaNovaLinhaGridMedic()
        {
            DataTable dtV = CriarColunasELinhaGridMedic();

            foreach (GridViewRow l in grdPesqMedic.Rows)
            {
                var rdb = (RadioButton)l.FindControl("rdbMedicamento");

                if (rdb.Checked)
                {
                    DataRow linha = dtV.NewRow();
                    linha["ID_MEDIC"] = ((HiddenField)l.FindControl("hidIdMedic")).Value;
                    linha["MEDIC"] = ((HiddenField)l.FindControl("hidNomeMedic")).Value;
                    linha["PRINC"] = ((HiddenField)l.FindControl("hidPrincAtiv")).Value;
                    linha["USO"] = txtUso.Text;
                    linha["QTD"] = txtQuantidade.Text;
                    linha["PRESC"] = txtPrescricao.Text;
                    dtV.Rows.Add(linha);
                }
            }

            Session["GridSolic_PROC_MEDIC"] = dtV;

            carregaGridNovaComContextoMedic();
        }

        private DataTable CriarColunasELinhaGridMedic()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_MEDIC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "MEDIC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PRINC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "USO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTD";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PRESC";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdMedicamentos.Rows)
            {
                linha = dtV.NewRow();
                linha["ID_MEDIC"] = ((Label)li.FindControl("lblIdMedic")).Text;
                linha["MEDIC"] = ((Label)li.FindControl("lblMedic")).Text;
                linha["PRINC"] = ((Label)li.FindControl("lblPrincipio")).Text;
                linha["USO"] = ((Label)li.FindControl("lblUso")).Text;
                linha["QTD"] = ((Label)li.FindControl("lblQtd")).Text;
                linha["PRESC"] = ((Label)li.FindControl("lblPrescricao")).Text;
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContextoMedic()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_MEDIC"];

            grdMedicamentos.DataSource = dtV;
            grdMedicamentos.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdMedicamentos.Rows)
            {
                Label lblIdMedic, lblMedic, lblPrincipio, lblPrescricao, lblUso, lblQtd;
                lblIdMedic = (Label)li.FindControl("lblIdMedic");
                lblMedic = (Label)li.FindControl("lblMedic");
                lblPrincipio = (Label)li.FindControl("lblPrincipio");
                lblUso = (Label)li.FindControl("lblUso");
                lblQtd = (Label)li.FindControl("lblQtd");
                lblPrescricao = (Label)li.FindControl("lblPrescricao");

                string idMedic, medic, princ, uso, qtd, presc;

                //Coleta os valores do dtv da modal popup
                idMedic = dtV.Rows[aux]["ID_MEDIC"].ToString();
                medic = dtV.Rows[aux]["MEDIC"].ToString();
                princ = dtV.Rows[aux]["PRINC"].ToString();
                uso = dtV.Rows[aux]["USO"].ToString();
                qtd = dtV.Rows[aux]["QTD"].ToString();
                presc = dtV.Rows[aux]["PRESC"].ToString();

                //Seta os valores nos campos da modal popup
                lblIdMedic.Text = idMedic;
                lblMedic.Text = medic;
                lblPrincipio.Text = princ;
                lblUso.Text = uso;
                lblQtd.Text = qtd;
                lblPrescricao.Text = presc;
                aux++;
            }
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
        private void CarregaModelosReceituario(DropDownList DropDownListModelo, int ColaborarId, string ModeloId)
        {
            AuxiliCarregamentos.CarregaModelos(DropDownListModelo, ColaborarId, ModeloId, "M");

        }
        private void CarregaModelosExames(DropDownList DropDownListModelo, int ColaborarId, string ModeloId)
        {
            AuxiliCarregamentos.CarregaModelos(DropDownListModelo, ColaborarId, ModeloId,"E");

        }
        private void CarregaModelosEncam(DropDownList DropDownListModelo, int ColaborarId, string ModeloId)
        {
            AuxiliCarregamentos.CarregaModelos(DropDownListModeloEncam, ColaborarId, ModeloId, "C");

        }
        private void CarregaModelosAmbulatorio(DropDownList DropDownListModelo, int ColaborarId, string ModeloId)
        {
            AuxiliCarregamentos.CarregaModelos(DropDownListModelo, ColaborarId, ModeloId, "A");

        }
        private void CarregarProfissionais(DropDownList drp)
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(drp, LoginAuxili.CO_EMP, false, "0");
        }

        /// <summary>
        /// Responsável por carregar os medicamentos cadastrados como ativos na base
        /// </summary>
        private void CarregarMedicamentos()
        {
            var grupo = !String.IsNullOrEmpty(drpGrupoMedic.SelectedValue) ? int.Parse(drpGrupoMedic.SelectedValue) : 0;
            var subGrupo = !String.IsNullOrEmpty(drpSubGrupoMedic.SelectedValue) ? int.Parse(drpSubGrupoMedic.SelectedValue) : 0;
            var nome = !String.IsNullOrEmpty(txtMedicamento.Text) && rdbMedic.Checked ? txtMedicamento.Text : "";
            var princ = !String.IsNullOrEmpty(txtPrincipio.Text) && rdbPrinc.Checked ? txtPrincipio.Text : "";

            var res = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                       where (grupo != 0 ? tb90.TB260_GRUPO.ID_GRUPO == grupo : true)
                       && (subGrupo != 0 ? tb90.TB261_SUBGRUPO.ID_SUBGRUPO == subGrupo : true)
                       && (!String.IsNullOrEmpty(nome) ? tb90.NO_PROD.Contains(nome) : true)
                       && (!String.IsNullOrEmpty(princ) ? tb90.NO_PRINCIPIO_ATIVO.Contains(princ) : true)
                       select new
                       {
                           tb90.CO_PROD,
                           tb90.NO_PROD,
                           tb90.DES_PROD,
                           tb90.NO_PRINCIPIO_ATIVO,
                           FORNEC = tb90.TB41_FORNEC != null ? (!string.IsNullOrEmpty(tb90.TB41_FORNEC.NO_SIGLA_FORN) ? tb90.TB41_FORNEC.NO_SIGLA_FORN : tb90.TB41_FORNEC.NO_FAN_FOR) : " - ",
                           tb90.VL_UNIT_PROD
                       }).OrderBy(w => w.NO_PROD).ToList();

            grdPesqMedic.DataSource = res;
            grdPesqMedic.DataBind();
        }
        /// <summary>
        /// Carrega os procedimentos em dropdownlist
        /// </summary>
        /// <param name="ddlprocp"></param>
        private void CarregarProcedimentos(DropDownList ddlprocp, int oper = 0, string tipoProc = null)
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlprocp, oper, false, true, tipoProc, true);
        }

        /// <summary>
        /// Carrega os planos de saúde de acordo com a operadora
        /// </summary>
        /// <param name="ddlPlanop"></param>
        /// <param name="ddlOperp"></param>
        private void CarregarPlanos(DropDownList ddlPlanop, DropDownList ddlOperp)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanop, ddlOperp, true, true, true);
        }

        /// <summary>
        /// Carrega as operadoras
        /// </summary>
        /// <param name="ddlOperp"></param>
        private void carregarOperadoras(DropDownList ddlOperp)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperp, true, true, true, false);
        }

        private void CarregaGrupoMedicamento(DropDownList drp, bool relatorio = false)
        {
            AuxiliCarregamentos.CarregaGruposItens(drp, relatorio);
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubGrupoMedicamento(DropDownList drpGrupo, DropDownList drpSubGrupo, bool relatorio = false)
        {
            int idGrupo = drpGrupo.SelectedValue != "" ? int.Parse(drpGrupo.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaSubGruposItens(drpSubGrupo, idGrupo, relatorio);
        }

        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb89 in TB89_UNIDADES.RetornaTodosRegistros()
                                     select new { tb89.NO_UNID_ITEM, tb89.CO_UNID_ITEM });

            ddlUnidade.DataTextField = "NO_UNID_ITEM";
            ddlUnidade.DataValueField = "CO_UNID_ITEM";
            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega a lista de agendamentos
        /// </summary>
        private void CarregaAgendamentos()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);

            txtIniAgenda.Text = dtIni.AddDays(-5).ToString();
            txtFimAgenda.Text = dtFim.AddDays(15).ToString();

            int local = String.IsNullOrEmpty(ddlLocal.SelectedValue) ? 0 : int.Parse(ddlLocal.SelectedValue);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                           //&& (tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_AGEND_ENCAM == "S")
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!string.IsNullOrEmpty(txtNomePacPesqAgendAtend.Text) ? tb07.NO_ALU.Contains(txtNomePacPesqAgendAtend.Text) : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
                           //Paciente em O(óbito), E(encaminhados para internação) e I(internados)
                       && (local == 0 ? 0 == 0 : tbs174.ID_DEPTO_LOCAL_ATENDI == local)
                       select new saidaPacientes
                       {
                           //CO_TIPO_RISCO = tbs194.CO_TIPO_RISCO,
                           saiuTriagem = tbs174.FL_SITUA_TRIAGEM,
                           AgendaHora = tbs174.HR_AGEND_HORAR,
                           CO_ALU = tbs174.CO_ALU,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           PACIENTE = tb07.NO_ALU,
                           DT_NASC_PAC = tb07.DT_NASC_ALU,
                           SX = tb07.CO_SEXO_ALU,
                           TP_DEF = tb07.TP_DEF,
                           podeClicar = true,
                           //podeClicar = (tbs174.FL_AGEND_ENCAM == "S" && tbs174.CO_SITUA_AGEND_HORAR != "R" ? true : false),

                           Situacao = tb07.CO_SITU_ALU.Equals("O") || tb07.CO_SITU_ALU.Equals("H") ? tb07.CO_SITU_ALU : tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,

                           Cortesia = tbs174.FL_CORTESIA,
                           Contratacao = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : "",
                           ContratParticular = tbs174.TB250_OPERA != null ? (tbs174.TB250_OPERA.FL_INSTI_OPERA != null && tbs174.TB250_OPERA.FL_INSTI_OPERA == "S") : false
                       }).ToList();

            foreach (var item in res)
            {
                var tbs194 = TBS194_PRE_ATEND.RetornarUmRegistro(p => p.ID_AGEND_HORAR == item.ID_AGEND_HORAR);

                if (tbs194 != null)
                {
                    item.CO_TIPO_RISCO = tbs194.CO_TIPO_RISCO;
                }
            }

            res = res.OrderByDescending(w => w.DT).ThenBy(w => w.hora).ThenBy(w => w.PACIENTE).ToList();

            grdPacientes.DataSource = res;
            grdPacientes.DataBind();
        }

        public class saidaPacientes
        {
            public string AgendaHora { get; set; }
            public TimeSpan hora
            {
                get
                {
                    return TimeSpan.Parse((AgendaHora));
                }
            }
            public int? CO_ALU { get; set; }
            public bool podeClicar { get; set; }
            public int ID_AGEND_HORAR { get; set; }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DTHR
            {
                get
                {
                    return this.DT.ToString("dd/MM/yy") + " - " + this.HR;
                }
            }

            //Dados do Paciente
            public string PACIENTE { get; set; }
            public DateTime? DT_NASC_PAC { get; set; }
            public string IDADE
            {
                get
                {
                    return AuxiliFormatoExibicao.FormataDataNascimento(this.DT_NASC_PAC, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto);
                }
            }
            public string SX { get; set; }
            public string TP_DEF { get; set; }

            //Trata a imagem
            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
            public string saiuTriagem { get; set; }
            public string imagem_URL
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarUrlImagemAgend(Situacao, agendaEncamin, agendaConfirm, faltaJustif, saiuTriagem);
                }
            }
            public string imagem_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipImagemAgend(this.imagem_URL);
                }
            }

            public int CO_TIPO_RISCO { get; set; }

            //Trata as cores de acordo com a classificação de risco
            public bool DIV_1
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 1 ? true : false);
                }
            }
            public bool DIV_2
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 2 ? true : false);
                }
            }
            public bool DIV_3
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 3 ? true : false);
                }
            }
            public bool DIV_4
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 4 ? true : false);
                }
            }
            public bool DIV_5
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 5 ? true : false);
                }
            }
            public string Cortesia { get; set; }
            public string Contratacao { get; set; }
            public bool ContratParticular { get; set; }

            public string tpContr_URL
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarURLAgendContrat(Contratacao, Cortesia, ContratParticular);
                }
            }

            public string tpContr_TIP
            {
                get
                {
                    var txt = AuxiliFormatoExibicao.RetornarTextoAgendContrat(Contratacao, Cortesia, ContratParticular);
                    var tip = AuxiliFormatoExibicao.RetornarToolTipAgendContrat(Contratacao, Cortesia, ContratParticular);

                    return txt + " - " + tip;
                }
            }
        }

        /// <summary>
        /// Carrega a lista de agendamentos do paciente recebido como parâmetro 
        /// </summary>
        /// <param name="CO_ALU"></param>

        private void CarregaAgendaPlanejamento(int CO_ALU)
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtIniAgenda.Text) ? DateTime.Parse(txtIniAgenda.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtFimAgenda.Text) ? DateTime.Parse(txtFimAgenda.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into late
                       from IIlaten in late.DefaultIfEmpty()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on IIlaten.CO_COL_ATEND equals tb03.CO_COL into col
                       from tb03 in col.DefaultIfEmpty()
                       where tbs174.CO_ALU == CO_ALU
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
                       select new saidaHistoricoAgenda
                       {
                           horaAgenda = tbs174.HR_AGEND_HORAR,
                           dtAgenda = tbs174.DT_AGEND_HORAR,

                           //DE_ACAO = (IIlaten != null ? (!string.IsNullOrEmpty(IIlaten.DE_HIP_DIAGN) ? IIlaten.DE_HIP_DIAGN : " - ") : (!string.IsNullOrEmpty(tbs174.DE_ACAO_PLAN) ? tbs174.DE_ACAO_PLAN : " - ")),

                           ID_AGENDA = tbs174.ID_AGEND_HORAR,
                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,
                           podeClicar = true
                           //podeClicar = (tbs174.FL_SITUA_ACAO != null ? (tbs174.FL_SITUA_ACAO != "R" ? true : false) : true)
                       }).ToList();


            foreach (saidaHistoricoAgenda r in res)
            {
                var result = retornaAcaoProfissional(r.ID_AGENDA);
                r.DE_ACAO = result.DE_ACAO;
                r.PROFISSIONAL = result.PROFISSIONAL;
            }

            res = res.OrderBy(w => w.dtAgenda).ThenBy(w => w.hora).ToList();

            grdHistoricoAgenda.DataSource = res;
            grdHistoricoAgenda.DataBind();
        }
        protected saidaHistoricoAgenda retornaAcaoProfissional(int idAgenda)
        {
            saidaHistoricoAgenda saida = new saidaHistoricoAgenda();
            //var Listatbs375 = TBS375_LOG_ALTER_STATUS_AGEND_HORAR.RetornaTodosRegistros().OrderBy(x => x.TBS174_AGEND_HORAR.DT_AGEND_HORAR);
            var tbs174 = TBS174_AGEND_HORAR.RetornaTodosRegistros().FirstOrDefault(x => x.ID_AGEND_HORAR == idAgenda);

            if (tbs174 != null)
            {
                string profissional = TB03_COLABOR.RetornaTodosRegistros().FirstOrDefault(c => c.CO_COL == tbs174.CO_COL).NO_APEL_COL;
                saida.PROFISSIONAL = !string.IsNullOrEmpty(profissional) ? profissional : " - ";

                switch (tbs174.CO_SITUA_AGEND_HORAR)
                {
                    case "P":
                        saida.DE_ACAO = "Presença";
                        break;
                    case "C":
                        saida.DE_ACAO = "Cancelamento";
                        break;
                    case "T":
                        saida.DE_ACAO = "Tipo Agenda";
                        break;
                    case "E":
                        saida.DE_ACAO = "Encaminhamento";
                        break;
                    case "A":
                        saida.DE_ACAO = "Cadastro";
                        break;
                    case "R":
                        saida.DE_ACAO = "Atendimento";
                        break;
                    default: break;
                }
            }
            else
            {
                saida.PROFISSIONAL = " - ";
                saida.DE_ACAO = "      -       ";
            }
            return saida;
        }

        public class saidaHistoricoAgenda
        {
            public bool podeClicar { get; set; }
            public TimeSpan hora
            {
                get
                {
                    return TimeSpan.Parse((horaAgenda));
                }
            }
            public string horaAgenda { get; set; }
            public int ID_AGENDA { get; set; }
            public DateTime dtAgenda { get; set; }
            public string dtAgenda_V
            {
                get
                {
                    return this.dtAgenda.ToString("dd/MM/yy") + " - " + this.horaAgenda;
                }
            }
            public string DE_ACAO { get; set; }
            public string PROFISSIONAL { get; set; }

            //Trata a imagem
            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
            public string imagem_URL
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarUrlImagemAgend(Situacao, agendaEncamin, agendaConfirm, faltaJustif);
                }
            }
            public string imagem_TIP
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarToolTipImagemAgend(this.imagem_URL);
                }
            }
        }

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
                           Data = tbs375.DT_CADAS,
                           NO_PROFI = tb03.CO_MAT_COL + " - " + tb03.NO_COL,
                           FL_TIPO = tbs375.FL_TIPO_LOG,
                           FL_CONFIR_AGEND = tbs375.FL_CONFIR_AGEND,
                           CO_SITUA_AGEND = tbs375.CO_SITUA_AGEND_HORAR,
                           FL_AGEND_ENCAM = tbs375.FL_AGEND_ENCAM,
                           FL_FALTA_JUSTIF = tbs375.FL_JUSTI,
                           OBS = tbs375.DE_OBSER,
                       }).ToList();

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
                        case "T":
                            return "Tipo Agenda";
                        case "E":
                            return "Encaminhamento";
                        case "A":
                            return "Cadastro";
                        case "R":
                            return "Atendimento";
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
                            s = (this.CO_SITUA_AGEND == "C" ? "Alterado para Cancelado" : "Alterado para Agendado");
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = (this.FL_AGEND_ENCAM == "S" ? "Encaminhado para a Atendimento" : "Remoção de encaminhamento para Atendimento");
                            break;
                        //Esse é inserido na "Mão", se for CADASTRO, então verifica se foi cadastrado como lista de espera ou consulta
                        case "A":
                            s = "Inserção de registro de Agendamento";
                            break;
                        //Trata quando é ATENDIMENTO
                        case "R":
                            s = "Atendimento realizado";
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
                                s = "/Library/IMG/PGS_ConsultaAtiva.png";
                            break;
                        //Trata quando é de ENCAMINHAMENTO
                        case "E":
                            s = "/Library/IMG/PGS_SF_AgendaEncaminhada.png";
                            break;
                        //Trata quando é de CADASTRO
                        case "A":
                            s = "/Library/IMG/PGN_IconeTelaCadastro2.png";
                            break;
                        //Trata quando é ATENDIMENTO
                        case "R":
                            s = "/Library/IMG/PGS_SF_AgendaRealizada.png";
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

        /// <summary>
        /// Carrega os dados do agendamento recebido como parâmetro
        /// </summary>
        /// <param name="ID_AGEND_HORAR"></param>
        private void CarregaDadosAgendamento(int ID_AGEND_HORAR)
        {
            hidIdAgenda.Value = ID_AGEND_HORAR.ToString();
            var coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGEND_HORAR).CO_ALU.Value;

            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
          
            hidIdAtendimento.Value =
            hidDtValidOrcam.Value =
            hidCkOrcamAprov.Value =
            hidObserMedicam.Value =
            hidObserExame.Value =
            hidObsOrcam.Value =
            txtObsOrcam.Text =
            txtObserMedicam.Text =
            txtObserExame.Text =
            txtObservacoes.Text =
           // txtQueixa.Text =
            txtHDA.Text =
            txtDtValidade.Text =
            txtVlDscto.Text =
            txtVlTotalOrcam.Text =
            txtSenha.Text =
            ddlClassRisco.SelectedValue =
            btnSalvaAtend.OnClientClick =
            BtnFinalizar.OnClientClick = "";



            chkAprovado.Text = "Aprovado";
            chkAprovado.Enabled = true;


            var tbs194 = TBS194_PRE_ATEND.RetornaTodosRegistros().FirstOrDefault(p => p.CO_ALU == coAlu);
            /*select new
            {
                TIPO_RISCO = tbs194.CO_TIPO_RISCO
            }).FirstOrDefault();*/

            if (tbs194 != null)
            {
                ddlClassRisco.SelectedValue = (tbs194.CO_TIPO_RISCO.ToString() ?? "");
            }

            txtHrAtend.Text = DateTime.Now.ToShortTimeString();

            if (grdProcedOrcam.Rows.Count != 0 || grdExame.Rows.Count != 0 || grdMedicamentos.Rows.Count != 0)
                LimparGridsAgendamento();

            var Atend = TBS390_ATEND_AGEND.RetornaTodosRegistros().Where(a => a.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR).FirstOrDefault();

            if (Atend != null)
            {
                hidIdAtendimento.Value = Atend.ID_ATEND_AGEND.ToString();

                if (drpProfResp.Items.FindByValue(Atend.CO_COL_ATEND.ToString()) != null)
                    drpProfResp.SelectedValue = Atend.CO_COL_ATEND.ToString();

                //if (Atend.CO_SITUA == "F")
                //    BtnSalvar.OnClientClick = BtnFinalizar.OnClientClick = "alert('Atendimento já finalizado!'); return false;";



                hidTxtObserv.Value = txtObservacoes.Text = Atend.DE_CONSI;

                //txtQueixa.Text = Atend.DE_QXA_PRINC;
                txtHDA.Text = Atend.DE_HDA;


                if (Atend.DT_VALID_ORCAM.HasValue)
                    txtDtValidade.Text = hidDtValidOrcam.Value = Atend.DT_VALID_ORCAM.Value.ToShortDateString();
                if (Atend.VL_DSCTO_ORCAM.HasValue)
                    txtVlDscto.Text = Atend.VL_DSCTO_ORCAM.Value.ToString();
                if (Atend.VL_TOTAL_ORCAM.HasValue)
                    txtVlTotalOrcam.Text = Atend.VL_TOTAL_ORCAM.Value.ToString();
                if (!String.IsNullOrEmpty(Atend.FL_SITU_FATU) && Atend.FL_SITU_FATU != "N")
                {
                    if (Atend.FL_SITU_FATU == "F")
                    {
                        chkAprovado.Text = "Faturado";
                        chkAprovado.Enabled = false;
                    }

                    chkAprovado.Checked = true;
                    hidCkOrcamAprov.Value = "S";
                }

                ddlClassRisco.SelectedValue = (Atend.CO_TIPO_RISCO.ToString() ?? "");
                txtSenha.Text = Atend.NU_SENHA_ATEND;
                txtDtAtend.Text = Atend.DT_REALI.ToShortDateString();
                txtHrAtend.Text = Atend.DT_REALI.ToShortTimeString();

                carregaGridOrcamento(Atend.ID_ATEND_AGEND);
                carregaGridExame(Atend.ID_ATEND_AGEND);
                carregaGridMedic(Atend.ID_ATEND_AGEND);
                }
        }

        private void LimparGridsAgendamento()
        {
            grdProcedOrcam.DataSource =
            grdExame.DataSource =
            grdMedicamentos.DataSource = null;

            grdProcedOrcam.DataBind();
            grdExame.DataBind();
            grdMedicamentos.DataBind();
        }

        private void CarregarPacientesDisponiveisAtestado()
        {
            if (String.IsNullOrEmpty(txtDtAtestado.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");
                return;
            }

            DateTime dtAtdo = DateTime.Parse(txtDtAtestado.Text);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.DT_AGEND_HORAR == dtAtdo
                       && tbs174.CO_SITUA_AGEND_HORAR == "R"
                       && (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M" && LoginAuxili.FLA_PROFESSOR == "S" ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       select new PacientesAtestado
                       {
                           hrConsul = tbs174.DT_PRESE,
                           hrAgend = tbs174.HR_AGEND_HORAR,
                           NO_PAC_ = tb07.NO_ALU,
                           RG_PAC = !String.IsNullOrEmpty(tb07.CO_RG_ALU) ? tb07.CO_RG_ALU + " - " + (!String.IsNullOrEmpty(tb07.CO_ORG_RG_ALU) ? tb07.CO_ORG_RG_ALU + "/" + tb07.CO_ESTA_RG_ALU : "") : " - ",
                           NO_RESP_ = tb07.TB108_RESPONSAVEL.NO_RESP,
                           NO_PAC_RECEB = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           CO_ALU = tb07.CO_ALU,
                           CO_RESP = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP : (int?)null),

                           FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                           FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                           NO_PAI = tb07.NO_PAI_ALU,
                           NO_MAE = tb07.NO_MAE_ALU
                       }).OrderByDescending(w => w.hrConsul).ThenBy(w => w.NO_PAC_).ToList();

            grdPacAtestado.DataSource = res;
            grdPacAtestado.DataBind();

            AbreModalPadrao("AbreModalAtestado();");
        }

        public class PacientesAtestado
        {
            public DateTime? hrConsul { get; set; }
            public string hrAgend { get; set; }
            public string hr_Consul
            {
                get
                {
                    if (this.hrConsul.HasValue)
                    {
                        return this.hrConsul.Value.ToString("HH:mm");
                    }
                    else
                    {
                        return this.hrAgend;
                    }
                }
            }
            public int CO_ALU { get; set; }
            public int? CO_RESP { get; set; }
            public string RG_PAC { get; set; }
            public string NO_PAC_ { get; set; }
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
            public string NO_RESP_IMP { get { return AuxiliFormatoExibicao.TrataNomeResponsavel(this.FL_PAI_RESP, this.FL_MAE_RESP, this.NO_PAI, this.NO_MAE, this.NO_RESP_, false); } }
            public string NO_RESP
            {
                get
                {
                    var nmResp = AuxiliFormatoExibicao.TrataNomeResponsavel(this.FL_PAI_RESP, this.FL_MAE_RESP, this.NO_PAI, this.NO_MAE, this.NO_RESP_);

                    if (nmResp == null)
                        return " - ";

                    nmResp = (nmResp.Length > 28 ? nmResp.Substring(0, 28) + "..." : nmResp);

                    return nmResp;
                }
            }
        }

        private void CarregarPacientesGuia()
        {
            if (String.IsNullOrEmpty(txtDtGuia.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");
                return;
            }

            DateTime dtAtdo = DateTime.Parse(txtDtGuia.Text);

            //var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
            //           where tbs174.DT_AGEND_HORAR == dtAtdo
            //           && (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M" && LoginAuxili.FLA_PROFESSOR == "S" ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
            //           select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
            if (!string.IsNullOrEmpty( hidIdAgenda.Value ))
            {
                if (TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU != null)
                {
                    var IdPaciente = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;
                    var res = TB07_ALUNO.RetornaTodosRegistros().Where(x => x.CO_ALU == IdPaciente);
                    if (res != null )
                    {
                        var paciente = res.First();
                        paciente.TB250_OPERAReference.Load();
                        if (paciente.TB250_OPERA != null)
                            drpOperGuia.SelectedValue = paciente.TB250_OPERA.ID_OPER.ToString();
                        drpPacienteGuia.DataTextField = "NO_ALU";
                        drpPacienteGuia.DataValueField = "CO_ALU";
                        drpPacienteGuia.DataSource = res;
                        drpPacienteGuia.DataBind();
                        drpPacienteGuia.SelectedValue = IdPaciente.ToString();
                    }
                }
            }
            drpPacienteGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));

            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        public void CarregarAnexosAssociados(Int32 coAlu)
        {
            var res = (from tbs392 in TBS392_ANEXO_ATEND.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs392.CO_COL_CADAS equals tb03.CO_COL
                       where tbs392.TB07_ALUNO.CO_ALU == coAlu
                       select new Anexo
                       {
                           ID_ANEXO_ATEND = tbs392.ID_ANEXO_ATEND,
                           NM_TITULO = tbs392.NM_TITULO,
                           TP_ANEXO = tbs392.TP_ANEXO,
                           DE_OBSER = tbs392.DE_OBSER,
                           DT_CADAS = tbs392.DT_CADAS,
                           NU_REGIS = tbs392.TBS390_ATEND_AGEND != null ? tbs392.TBS390_ATEND_AGEND.NU_REGIS : tbs392.TBS416_EXAME_RESUL.NU_REGISTRO,
                           NM_PROC_MEDI = tbs392.TBS416_EXAME_RESUL != null ? tbs392.TBS416_EXAME_RESUL.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI : "-",
                           registResult = tbs392.TBS416_EXAME_RESUL != null ? tbs392.TBS416_EXAME_RESUL.NU_REGISTRO : "",
                           SOLICITANTE = tbs392.TBS390_ATEND_AGEND != null ? tb03.NO_APEL_COL : ""
                       }).ToList();

            grdAnexos.DataSource = res;
            grdAnexos.DataBind();
        }

        public class Anexo
        {
            public int ID_ANEXO_ATEND { get; set; }
            public string NM_TITULO { get; set; }
            public string NU_REGIS { get; set; }
            public string NM_PROC_MEDI { get; set; }
            public DateTime DT_CADAS { get; set; }
            public string DE_OBSER { get; set; }
            public string DE_OBSER_RES
            {
                get
                {
                    return DE_OBSER.Length > 77 ? DE_OBSER.Substring(0, 77) + "..." : DE_OBSER;
                }
            }

            public string TP_ANEXO { get; set; }
            public string URL_TP_ANEXO
            {
                get
                {
                    switch (TP_ANEXO)
                    {
                        case "F":
                            return "/Library/IMG/PGS_IC_Imagens.jpg";
                        case "V":
                            return "/Library/IMG/PGS_IC_Imagens2.png";
                        case "U":
                            return "/Library/IMG/PGS_IC_ArquivoAudio.png";
                        case "A":
                        default:
                            return "/Library/IMG/PGS_IC_Anexo.png";
                    }
                }
            }
            public string registResult { get; set; }
            public string solic { get; set; }
            public string SOLICITANTE
            {
                get
                {
                    if (String.IsNullOrEmpty(solic) && !String.IsNullOrEmpty(registResult))
                    {
                        solic = "-";

                        var eex = TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros().Where(e => e.NU_REGISTRO == registResult).FirstOrDefault();

                        if (eex != null)
                            solic = eex.NO_SOLICITANTE;
                        else
                        {
                            var ein = TBS398_ATEND_EXAMES.RetornaTodosRegistros().Where(e => e.TBS390_ATEND_AGEND.NU_REGIS == registResult).FirstOrDefault();

                            if (ein != null)
                                solic = TB03_COLABOR.RetornaPeloCoCol(ein.CO_COL_CADAS).NO_APEL_COL;
                        }
                    }

                    return solic;
                }
                set
                {
                    solic = value;
                }
            }
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
            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                //Coleta os valores da linha
                string Valor;
                Valor = ((TextBox)li.FindControl("txtValorProcedOrc")).Text;
                VlDesconto = (!string.IsNullOrEmpty(txtVlDscto.Text) ? decimal.Parse(txtVlDscto.Text) : 0);

                //Soma os valores com os valores das outras linhas da grid
                VlTotal += (!string.IsNullOrEmpty(Valor) ? decimal.Parse(Valor) : 0);
            }

            //Debita o valor do desconto
            VlTotal -= VlDesconto;

            //Seta os valores nos textboxes
            txtVlTotalOrcam.Text = VlTotal.ToString("N2");
        }

        private void RecarregarGrids(int ID_AGEND_HORAR, int CO_ALU)
        {
            CarregaAgendamentos();

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

                if (idAgenda == ID_AGEND_HORAR)
                    ((CheckBox)li.FindControl("chkSelectPaciente")).Checked = true;
            }

            CarregaAgendaPlanejamento(CO_ALU);

            foreach (GridViewRow i in grdHistoricoAgenda.Rows)
            {
                string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;

                if (!String.IsNullOrEmpty(hidIdAgenda.Value) && idAgendaHist == hidIdAgenda.Value)
                    ((CheckBox)i.FindControl("chkSelectHistAge")).Checked = true;
            }
        }

        #endregion

            

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

        private void ExecutarFuncaoPadrao(string funcao)
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Funcao",
                funcao,
                true
            );
        }

        protected void imgSituacao_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;

            ImageButton img;
            if (grdPacientes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdPacientes.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgSituacao");
                    if (img.ClientID == atual.ClientID)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        //Atribui as informações da linha clicada aos campos correspondentes na modal
                        txtNomePaciMODLOG.Text = ((Label)linha.FindControl("lblNomPaci")).Text;
                        txtSexoMODLOG.Text = linha.Cells[3].Text;
                        txtIdadeMODLOG.Text = linha.Cells[4].Text;

                        AbreModalPadrao("AbreModalLog();");
                    }
                }
            }
        }

        protected void imgSituacaoHistorico_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdHistoricoAgenda.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHistoricoAgenda.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgSituacaoHistorico");
                    if (img.ClientID == atual.ClientID)
                    {
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgenda")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        foreach (GridViewRow i in grdPacientes.Rows)
                        {
                            if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
                            {
                                //Atribui as informações da linha clicada aos campos correspondentes na modal
                                txtNomePaciMODLOG.Text = ((Label)i.FindControl("lblNomPaci")).Text;
                                txtSexoMODLOG.Text = i.Cells[3].Text;
                                txtIdadeMODLOG.Text = i.Cells[4].Text;
                            }
                        }

                        AbreModalPadrao("AbreModalLog();");
                    }
                }
            }
        }

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            hidIdAgenda.Value = "";
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.ClientID == atual.ClientID)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                    hidIdAgenda.Value = idAgenda.ToString();
                    //if (tbs174.FL_AGEND_ENCAM == "T")
                    //{
                    //    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Paciente em atendimento no acolhimento (triagem). Favor solicitar à recepção o encaminhamento do paciente para atendimento médico.");
                    //    chk.Checked = false;
                    //    CarregaAgendaPlanejamento(0);
                    //}

                    if (tbs174.FL_AGEND_ENCAM == "A" && tbs174.CO_COL_ATEND != LoginAuxili.CO_COL)
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Este Paciente já está sendo atendido por outro profissional.");
                        chk.Checked = false;
                    }
                    else if (tbs174.FL_AGEND_ENCAM == "T")
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Paciente em atendimento no acolhimento (triagem). Favor solicitar à recepção o encaminhamento do paciente para atendimento médico.");
                        chk.Checked = false;
                        CarregaAgendaPlanejamento(0);
                    }
                    else if (tbs174.FL_AGEND_ENCAM != "S" && tbs174.FL_AGEND_ENCAM != "A")
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O paciente deve está com o status de encaminhado (círculo roxo) para poder iniciar um atendimento.");
                        chk.Checked = false;
                    }
                    else
                    {

                        if ((tbs174.CO_SITUA_AGEND_HORAR == SituacaoAgenda.ABERTO || tbs174.CO_SITUA_AGEND_HORAR == SituacaoAgenda.ESPERA) && !String.IsNullOrEmpty(tbs174.FL_AGEND_ENCAM) && tbs174.FL_AGEND_ENCAM != "N" && tbs174.FL_AGEND_ENCAM != "T")
                        {
                            if ((atual.Checked && tbs174.FL_AGEND_ENCAM == "S") || (atual.Checked && tbs174.FL_AGEND_ENCAM == "A"))
                                hidAgendSelec.Value = idAgenda.ToString();
                            else
                                hidAgendSelec.Value = "";

                            //carrega informações de paciente
                            var paciente = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);

                            if (tbs174.FL_AGEND_ENCAM == "S")
                            {
                                divEncaminAtendCheck.Visible = true;
                                divEncaminAtendCheck.Checked = true;
                                if (paciente != null)
                                    lblConfEncam.Text = "Deseja chamar o paciente " + paciente.NO_ALU + " para atendimento?";
                                else
                                    lblConfEncam.Text = "Deseja encaminhar o paciente para atendimento?";
                            }
                            else if (tbs174.FL_AGEND_ENCAM == "A")
                            {
                                divEncaminAtendCheck.Visible = false;
                                if (paciente != null)
                                    lblConfEncam.Text = "Deseja retornar a situação do paciente " + paciente.NO_ALU + " para encaminhado?";
                                else
                                    lblConfEncam.Text = "Deseja retornar a situação do paciente para encaminhado?";
                            }

                            if (!String.IsNullOrEmpty(hidAgendSelec.Value))
                                AbreModalPadrao("AbreModalEncamAtend();");
                        }
                        else
                            hidAgendSelec.Value = "";

                        if (!(chk.Checked && tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_AGEND_ENCAM == "A"))
                            ExecutarFuncaoPadrao("ZerarCronometro();");

                        if (chk.Checked)
                        {
                            CarregaAgendaPlanejamento(tbs174.CO_ALU.Value);
                          
                            Datas_Cons_Paciente(tbs174.CO_ALU.Value);
                            //Percorre a grid de histórico de atendimento, e ao achar a 
                            foreach (GridViewRow i in grdHistoricoAgenda.Rows)
                            {
                                string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;
                                if (idAgendaHist == idAgenda.ToString())
                                {
                                    CheckBox chkHistAg = (((CheckBox)i.FindControl("chkSelectHistAge")));
                                    chkHistAg.Checked = true;

                                    hidIdAgenda.Value = idAgendaHist.ToString();
                                    CarregaDadosAgendamento(idAgenda);
                                }
                            }
                        }
                        else
                            hidIdAgenda.Value = "";
                    }
                }
                else
                    chk.Checked = false;
            }
        }

        protected void lnkbAtendSim_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hidAgendSelec.Value))
            {
                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidAgendSelec.Value));

                if (tbs174.FL_AGEND_ENCAM == "S")
                {
                    tbs174.FL_AGEND_ENCAM = "A";

                    tbs174.DT_ATEND = DateTime.Now;
                    tbs174.CO_COL_ATEND = LoginAuxili.CO_COL;
                    tbs174.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs174.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                    tbs174.IP_ATEND = Request.UserHostAddress;
                    tbs174.HR_ATEND_INICIO = DateTime.Now;
                    // colocar aqui código pra abrir modal de prontuários
                    if(divEncaminAtendCheck.Checked)
                        AbrirProntuarioPadrao();
                }
                else if (tbs174.FL_AGEND_ENCAM == "A")
                {
                    tbs174.FL_AGEND_ENCAM = "S";

                    tbs174.DT_ATEND = (DateTime?)null;
                    tbs174.CO_COL_ATEND =
                    tbs174.CO_EMP_COL_ATEND =
                    tbs174.CO_EMP_ATEND = (int?)null;
                    tbs174.IP_ATEND = null;
                }

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174);

                RecarregarGrids(tbs174.ID_AGEND_HORAR, tbs174.CO_ALU.Value);

                if (tbs174.FL_AGEND_ENCAM == "A")
                    ExecutarFuncaoPadrao("IniciarCronometro();");
                else
                    ExecutarFuncaoPadrao("ZerarCronometro();");
            }
        }

        protected void lnkbAtendNao_OnClick(object sender, EventArgs e)
        {
            if (divEncaminAtendCheck.Checked)
                AbrirProntuarioPadrao();                
        }

        protected void chkSelectHistAge_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            foreach (GridViewRow li in grdHistoricoAgenda.Rows)
            {
                chk = (((CheckBox)li.FindControl("chkSelectHistAge")));

                if (chk.ClientID == atual.ClientID)
                {
                    if (chk.Checked)
                    {
                        int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);

                        hidIdAgenda.Value = idAgenda.ToString();
                        CarregaDadosAgendamento(idAgenda);
                    }
                    else
                        hidIdAgenda.Value = "";
                }
                else
                    chk.Checked = false;
            }
        }

        protected void imgPesqAgendamentos_OnClick(object sender, EventArgs e)
        {
            grdHistoricoAgenda.DataSource = null;
            grdHistoricoAgenda.DataBind();
            LimparGridsAgendamento();
            CarregaAgendamentos();
            ExecutarFuncaoPadrao("ZerarCronometro();");
        }

        protected void imgPesqHistAgend_OnClick(object sender, EventArgs e)
        {
            foreach (GridViewRow li in grdPacientes.Rows)
            {
                var chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.Checked)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    int coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda).CO_ALU.Value;
                    CarregaAgendaPlanejamento(coAlu);

                    //Percorre a grid de histórico de atendimento, e ao achar a 
                    foreach (GridViewRow i in grdHistoricoAgenda.Rows)
                    {
                        string idAgendaHist = ((HiddenField)i.FindControl("hidIdAgenda")).Value;
                        if (idAgendaHist == idAgenda.ToString())
                        {
                            CheckBox chkHistAg = (((CheckBox)i.FindControl("chkSelectHistAge")));
                            chkHistAg.Checked = true;

                            hidIdAgenda.Value = idAgendaHist.ToString();
                            CarregaDadosAgendamento(idAgenda);
                        }
                    }
                }
            }

            ExecutarFuncaoPadrao("ZerarCronometro();");
        }

        protected void btnSalvarInternar_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : 0;

            try
            {
                if (idAtendAgend > 0)
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                    tbs390.TB07_ALUNOReference.Load();
                    var tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(tbs390.TB07_ALUNO.CO_ALU, tbs390.TB07_ALUNO.CO_EMP);
                    if (tb07.CO_SITU_ALU != "O" || tb07.CO_SITU_ALU != "H")
                    {
                        var tbs448 = !string.IsNullOrEmpty(hidIdAtendimentoInternar.Value) ? TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoInternar.Value)) : new TBS448_ATEND_INTER_HOSPI();
                        var idTbs448 = !string.IsNullOrEmpty(hidIdAtendimentoInternar.Value) ? tbs448.ID_ATEND_INTER : 0;
                        var tbs451 = TBS451_INTER_REGIST.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idTbs448).FirstOrDefault();

                        #region Validações
                        if (tbs451 != null)
                        {
                            throw new ArgumentException("Esta requisição de internação já foi registrada, alteração não permitida.");
                        }

                        if (string.IsNullOrEmpty(txtDataProvavelAH.Text) || DateTime.Compare(DateTime.Now.Date, DateTime.Parse(txtDataProvavelAH.Text)) > 0)
                        {
                            txtDataProvavelAH.Focus();
                            throw new ArgumentException("Por favor, selecione uma data válida para a Admissão Hospitalar.");
                        }

                        if (string.IsNullOrEmpty(txtDS.Text))
                        {
                            txtDS.Focus();
                            throw new ArgumentException("Por favor, selecione a quantidade de diárias solicitadas.");
                        }

                        if (string.IsNullOrEmpty(drpCaraterInternacao.SelectedValue))
                        {
                            drpCaraterInternacao.Focus();
                            throw new ArgumentException("Por favor, selecione o caráter da internação.");
                        }

                        if (string.IsNullOrEmpty(drpTipoInternacao.SelectedValue))
                        {
                            drpTipoInternacao.Focus();
                            throw new ArgumentException("Por favor, selecione o tipo de internação.");
                        }

                        if (string.IsNullOrEmpty(drpRegimeInternacao.SelectedValue))
                        {
                            drpRegimeInternacao.Focus();
                            throw new ArgumentException("Por favor, selecione o regime de internação.");
                        }

                        if (string.IsNullOrEmpty(drpTipoAcomodacao.SelectedValue))
                        {
                            throw new ArgumentException("Por favor, selecione o tipo de acomodação.");
                        }

                        if (string.IsNullOrEmpty(drpTipoDoenca.SelectedValue))
                        {
                            throw new ArgumentException("Por favor, selecione o tipo de doença.");
                        }

                        if (string.IsNullOrEmpty(txtTDRP.Text))
                        {
                            txtTDRP.Focus();
                            throw new ArgumentException("Por favor, insira um número inteiro e um tipo d tempo para a hipótese e diagnóstico do tipo de doença.");
                        }

                        if (string.IsNullOrEmpty(drpTDRP.SelectedValue))
                        {
                            txtTDRP.Focus();
                            throw new ArgumentException("Por favor, insira um número inteiro e um tipo d tempo para a hipótese e diagnóstico do tipo de doença.");
                        }

                        if (string.IsNullOrEmpty(drpIndicacaoAcidente.SelectedValue))
                        {
                            drpIndicacaoAcidente.Focus();
                            throw new ArgumentException("Por favor, insira um número inteiro e um tipo d tempo para a hipótese e diagnóstico do tipo de doença.");
                        }

                        #endregion

                        #region TBS448
                        tbs448.TBS390_ATEND_AGEND = tbs390;
                        tbs448.CO_COL_SOLIC_INTER = LoginAuxili.CO_COL;
                        tbs448.CO_EMP_SOLIC_INTER = LoginAuxili.CO_EMP;
                        tbs448.CO_EMP_USUAR_LOGAD = LoginAuxili.CO_EMP;
                        tbs448.CO_PRIOR_INTER = drpClassRiscoInternar.SelectedValue;
                        //A = Ativo e I = inativo
                        tbs448.CO_SITUA_ENCAM_INTER = "A";
                        tbs448.DE_OBSER_INTER = txtIndicacaoClinica.Text;
                        tbs448.DT_PREVI_INTER = DateTime.Parse(txtDataProvavelAH.Text).Date;
                        tbs448.HR_PREVI_INTER = TimeSpan.Parse(DateTime.Now.ToShortTimeString());
                        tbs448.DT_SITUA_ENCAM_INTER = DateTime.Now;
                        tbs448.DT_SOLIC_INTER = DateTime.Now;
                        tbs448.QT_INDIC_DIAS_INTER = decimal.Parse(txtDS.Text);
                        tbs448.QT_TEMPO_DOENC_PACIE = decimal.Parse(txtTDRP.Text);
                        // (A) Ano; M (Meses); D (Dias)
                        tbs448.TP_TEMPO_DOENC_PACIE = drpTDRP.SelectedValue;
                        tbs448.TBS442_TIPO_CARAT = TBS442_TIPO_CARAT.RetornaPelaChavePrimaria(int.Parse(drpCaraterInternacao.SelectedValue));
                        tbs448.TBS443_TIPO_INTER = TBS443_TIPO_INTER.RetornaPelaChavePrimaria(int.Parse(drpTipoInternacao.SelectedValue));
                        tbs448.TBS444_TIPO_REGIM = TBS444_TIPO_REGIM.RetornaPelaChavePrimaria(int.Parse(drpRegimeInternacao.SelectedValue));
                        tbs448.TBS446_TIPO_DOENCA = TBS446_TIPO_DOENCA.RetornaPelaChavePrimaria(int.Parse(drpTipoDoenca.SelectedValue));
                        tbs448.TBS445_TIPO_INDIC_ACIDE = TBS445_TIPO_INDIC_ACIDE.RetornaPelaChavePrimaria(int.Parse(drpIndicacaoAcidente.SelectedValue));
                        if (!string.IsNullOrEmpty(drpTipoAcomodacao.SelectedValue))
                        {
                            tbs448.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(drpTipoAcomodacao.SelectedValue));
                        }

                        #region Trata sequencial
                        //Trata para gerar um Código do Encaminhamento
                        if (tbs448.ID_ATEND_INTER <= 0)
                        {
                            var res2 = (from tbs390pesq in TBS448_ATEND_INTER_HOSPI.RetornaTodosRegistros()
                                        select new { tbs390pesq.NU_REGIS_INTER }).OrderByDescending(w => w.NU_REGIS_INTER).FirstOrDefault();

                            string seq;
                            int seq2;
                            int seqConcat;
                            string seqcon;
                            string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                            string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                            if (res2 == null)
                                seq2 = 1;
                            else
                            {
                                seq = res2.NU_REGIS_INTER.Substring(6, 6);
                                seq2 = int.Parse(seq);
                            }

                            seqConcat = seq2 + 1;
                            seqcon = seqConcat.ToString().PadLeft(6, '0');

                            string CodigoAtendimento = string.Format("EI{0}{1}{2}", ano, mes, seqcon);
                            tbs448.NU_REGIS_INTER = CodigoAtendimento;
                        }

                        #endregion

                        TBS448_ATEND_INTER_HOSPI.SaveOrUpdate(tbs448, true);
                        tbs390.CO_SITUA = "F";//Finaliza o atendimento
                        int atend = 0;
                        foreach (GridViewRow i in grdPacientes.Rows)
                        {
                            if (((CheckBox)i.FindControl("chkSelectPaciente")).Checked)
                            {
                                atend = int.Parse(((HiddenField)i.FindControl("hidIdAgenda")).Value);
                                TBS174_AGEND_HORAR tbs174ob = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(atend);

                                tbs174ob.CO_SITUA_AGEND_HORAR = "R";
                                tbs174ob.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs174ob.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs174ob.DT_SITUA_AGEND_HORAR = DateTime.Now;
                                tbs174ob.HR_ATEND_FIM = DateTime.Now;

                                if (!String.IsNullOrEmpty(hidHoras.Value) && !String.IsNullOrEmpty(hidMinutos.Value))
                                    tbs174ob.HR_DURACAO_ATEND = int.Parse(hidHoras.Value).ToString("D2") + ":" + int.Parse(hidMinutos.Value).ToString("D2");

                                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ob, true);
                                break;
                            }
                        }
                        #endregion

                        //altera a situação, informando que este paciente está encaminhado para a internação
                        tb07.CO_SITU_ALU = "E";
                        TB07_ALUNO.SaveOrUpdate(tb07, true);


                        #region TBS449

                        int idAtendInter = tbs448.ID_ATEND_INTER;
                        var restbs449 = TBS449_ATEND_INTER_CID.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idAtendInter).ToList();
                        TBS449_ATEND_INTER_CID tbs449 = null;
                        if (restbs449.Count > 0)
                        {
                            foreach (var item in restbs449)
                            {
                                tbs449 = TBS449_ATEND_INTER_CID.RetornaPelaChavePrimaria(item.ID_ATEND_INTER_CID);
                                foreach (GridViewRow row in grdCIDInternar.Rows)
                                {
                                    bool chk = ((CheckBox)row.Cells[0].FindControl("chcCIDPrincipal")).Checked;
                                    //Informa qual(is) o(s) CID(s) principal(is) da internação
                                    tbs449.IS_CID_PRINC = chk ? "S" : "N";
                                    TBS449_ATEND_INTER_CID.SaveOrUpdate(tbs449, true);
                                }
                            }
                        }
                        else
                        {
                            var tbs438 = TBS438_ITENS_ATENDIMENTO.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend && x.TP_ITEM.Equals("C")).ToList();
                            foreach (var item in tbs438)
                            {
                                foreach (GridViewRow row in grdCIDInternar.Rows)
                                {
                                    string strIdCID = ((HiddenField)row.Cells[1].FindControl("idListaCIDInternar")).Value;
                                    tbs449 = new TBS449_ATEND_INTER_CID();
                                    item.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                                    if (item.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == int.Parse(strIdCID))
                                    {
                                        tbs449.TB117_CODIGO_INTERNACIONAL_DOENCA = item.TB117_CODIGO_INTERNACIONAL_DOENCA;
                                        tbs449.TBS448_ATEND_INTER_HOSPI = tbs448;

                                        bool chk = ((CheckBox)row.Cells[0].FindControl("chcCIDPrincipal")).Checked;
                                        //Informa qual(is) o(s) CID(s) principal(is) da internação
                                        tbs449.IS_CID_PRINC = chk ? "S" : "N";
                                        TBS449_ATEND_INTER_CID.SaveOrUpdate(tbs449, true);
                                    }
                                }
                            }
                        }

                        #endregion

                        #region TBS450

                        TBS450_ATEND_INTER_PROCE_MEDIC tbs450 = null;
                        var restbs450 = TBS450_ATEND_INTER_PROCE_MEDIC.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == tbs448.ID_ATEND_INTER).ToList();
                        if (restbs450.Count > 0)
                        {
                            foreach (var item in restbs450)
                            {
                                tbs450 = item;
                                item.TBS356_PROC_MEDIC_PROCEReference.Load();
                                if (tbs450.CO_TIPO_REGIS_PROCE == "IN")
                                {
                                    foreach (GridViewRow row in grdProcedimentoInternar.Rows)
                                    {
                                        int idProc = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                                        if (idProc != item.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE)
                                        {
                                            tbs450.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc);
                                            tbs450.TBS448_ATEND_INTER_HOSPI = TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(idAtendInter);
                                        }
                                        //Internação
                                        string strQtdeProc = ((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text;
                                        tbs450.QT_PROCE_INTER = !string.IsNullOrEmpty(strQtdeProc) ? decimal.Parse(strQtdeProc) : 0;
                                        string strVlUnit = ((TextBox)row.Cells[5].FindControl("VlUnitarioProcedimentoInternar")).Text;
                                        tbs450.VL_UNITA_PROCE_INTER = !string.IsNullOrEmpty(strVlUnit) ? decimal.Parse(strVlUnit) : 0;
                                        tbs450.VL_TOTAL_PROCE_INTER = tbs450.QT_PROCE_INTER * tbs450.VL_UNITA_PROCE_INTER;
                                        TBS450_ATEND_INTER_PROCE_MEDIC.SaveOrUpdate(tbs450, true);
                                    }
                                }
                                else
                                {
                                    foreach (GridViewRow row in grdOPMInternar.Rows)
                                    {
                                        int idProc = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                                        if (idProc != item.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE)
                                        {
                                            tbs450.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc);
                                            tbs450.TBS448_ATEND_INTER_HOSPI = TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(idAtendInter);
                                        }
                                        //OPM
                                        string strQtdeProc = ((TextBox)row.Cells[4].FindControl("qtdOPM")).Text;
                                        tbs450.QT_PROCE_INTER = !string.IsNullOrEmpty(strQtdeProc) ? decimal.Parse(strQtdeProc) : 0;
                                        string strVlUnit = ((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text;
                                        tbs450.VL_UNITA_PROCE_INTER = !string.IsNullOrEmpty(strVlUnit) ? decimal.Parse(strVlUnit) : 0;
                                        tbs450.VL_TOTAL_PROCE_INTER = tbs450.QT_PROCE_INTER * tbs450.VL_UNITA_PROCE_INTER;
                                        tbs450.FABRI_OPM = ((TextBox)row.Cells[5].FindControl("fabricanteOPM")).Text;
                                        TBS450_ATEND_INTER_PROCE_MEDIC.SaveOrUpdate(tbs450, true);
                                    }
                                }
                            }

                        }
                        else
                        {
                            foreach (GridViewRow row in grdProcedimentoInternar.Rows)
                            {
                                if (grdProcedimentoInternar.Rows.Count > 0)
                                {
                                    tbs450 = new TBS450_ATEND_INTER_PROCE_MEDIC();
                                    int idProc = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                                    tbs450.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc);
                                    tbs450.TBS448_ATEND_INTER_HOSPI = TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(idAtendInter);
                                    //Internação
                                    tbs450.CO_TIPO_REGIS_PROCE = "IN";
                                    string strQtdeProc = ((TextBox)row.Cells[4].FindControl("qtdProcedimento")).Text;
                                    tbs450.QT_PROCE_INTER = !string.IsNullOrEmpty(strQtdeProc) ? decimal.Parse(strQtdeProc) : 0;
                                    string strVlUnit = ((TextBox)row.Cells[5].FindControl("VlUnitarioProcedimentoInternar")).Text;
                                    tbs450.VL_UNITA_PROCE_INTER = !string.IsNullOrEmpty(strVlUnit) ? decimal.Parse(strVlUnit) : 0;
                                    tbs450.VL_TOTAL_PROCE_INTER = tbs450.QT_PROCE_INTER * tbs450.VL_UNITA_PROCE_INTER;
                                    TBS450_ATEND_INTER_PROCE_MEDIC.SaveOrUpdate(tbs450, true);
                                }
                            }
                            foreach (GridViewRow row in grdOPMInternar.Rows)
                            {
                                if (grdOPMInternar.Rows.Count > 0)
                                {
                                    tbs450 = new TBS450_ATEND_INTER_PROCE_MEDIC();
                                    int idProc = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                                    tbs450.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc);
                                    tbs450.TBS448_ATEND_INTER_HOSPI = TBS448_ATEND_INTER_HOSPI.RetornaPelaChavePrimaria(idAtendInter);
                                    //OPM
                                    tbs450.CO_TIPO_REGIS_PROCE = "OP";
                                    string strQtdeProc = ((TextBox)row.Cells[4].FindControl("qtdOPM")).Text;
                                    tbs450.QT_PROCE_INTER = !string.IsNullOrEmpty(strQtdeProc) ? decimal.Parse(strQtdeProc) : 0;
                                    string strVlUnit = ((TextBox)row.Cells[6].FindControl("VlUnitarioOPM")).Text;
                                    tbs450.VL_UNITA_PROCE_INTER = !string.IsNullOrEmpty(strVlUnit) ? decimal.Parse(strVlUnit) : 0;
                                    tbs450.VL_TOTAL_PROCE_INTER = tbs450.QT_PROCE_INTER * tbs450.VL_UNITA_PROCE_INTER;
                                    tbs450.FABRI_OPM = ((TextBox)row.Cells[5].FindControl("fabricanteOPM")).Text;
                                    TBS450_ATEND_INTER_PROCE_MEDIC.SaveOrUpdate(tbs450, true);
                                }
                            }
                        }
                        hidIdAtendimentoInternar.Value = tbs448.ID_ATEND_INTER.ToString();
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Operação realizada com sucesso!", Request.Url.AbsoluteUri);
                        #endregion
                    }
                    else
                    {
                        throw new ArgumentException("Este registro não pode ser inserido, por favor selecione um atendimento onde o paciente ainda não foi encaminhado para internação ou não veio a óbito.");
                    }
                }
                else
                {
                    throw new ArgumentException("Por favor, selecione um atendimento ou salve o registro atual.");
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }
        }

        protected void ddlTipoProcedimentoInternar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idProcedimento = !string.IsNullOrEmpty(ddlTipoProcedimentoInternar.SelectedValue) ? int.Parse(ddlTipoProcedimentoInternar.SelectedValue) : 0;

            if (idProcedimento > 0)
            {
                List<ProcedimentoInternar> procItens = new List<ProcedimentoInternar>();

                foreach (GridViewRow row in grdProcedimentoInternar.Rows)
                {
                    ProcedimentoInternar proc = new ProcedimentoInternar();
                    proc.IdProcedimento = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                    proc.TipoProcedimento = row.Cells[1].Text;
                    proc.NomeProcedimento = row.Cells[2].Text;
                    proc.CodigoProcedimento = row.Cells[3].Text;
                    proc.QuantidadeProcedimento = !string.IsNullOrEmpty(row.Cells[4].Text) ? int.Parse(row.Cells[4].Text) : (int?)null;
                    proc.VlUnitarioProcedimento = !string.IsNullOrEmpty(row.Cells[5].Text) ? decimal.Parse(row.Cells[5].Text) : (decimal?)null;
                    proc.VlTotalProcedimento = !string.IsNullOrEmpty(row.Cells[6].Text) ? decimal.Parse(row.Cells[6].Text) : (decimal?)null;

                    procItens.Add(proc);
                }

                var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.ID_PROC_MEDI_PROCE == idProcedimento)
                            .Join(TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros(), x => x.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP, y => y.ID_PROC_MEDIC_SGRUP, (x, y) => new { x, y })
                            .Join(TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros(), z => z.x.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO, w => w.ID_PROC_MEDIC_GRUPO, (z, w) => new { z, w })
                            .Select(a => new
                            {
                                a.z.x.ID_PROC_MEDI_PROCE,
                                nmProcedimento = a.w.NM_PROC_MEDIC_GRUPO + " - " + a.z.y.NM_PROC_MEDIC_SGRUP,
                                a.z.x.NM_PROC_MEDI,
                                a.z.x.CO_PROC_MEDI,
                            }).FirstOrDefault();

                var tbs353 = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE)
                            .Select(w => new
                            {
                                w.VL_BASE
                            }).FirstOrDefault();

                if (tbs356 != null)
                {
                    var procTb = new ProcedimentoInternar();
                    procTb.IdProcedimento = tbs356.ID_PROC_MEDI_PROCE;
                    procTb.TipoProcedimento = tbs356.nmProcedimento;
                    procTb.NomeProcedimento = tbs356.NM_PROC_MEDI;
                    procTb.CodigoProcedimento = tbs356.CO_PROC_MEDI;
                    if (tbs353 != null)
                        procTb.VlUnitarioProcedimento = tbs353.VL_BASE;
                    procItens.Add(procTb);
                }

                grdProcedimentoInternar.DataSource = procItens;
                grdProcedimentoInternar.DataBind();
            }
            AbreModalPadrao("AbreModalInternar();");
        }
        protected void btnDelProcedimentoInternar_OnClick(object sender, EventArgs e)
        {
            ImageButton clickedButton = (ImageButton)sender;
            GridViewRow linha = (GridViewRow)clickedButton.Parent.Parent;
            int index = linha.RowIndex;

            List<ProcedimentoInternar> listProcInternar = new List<ProcedimentoInternar>();
            foreach (GridViewRow row in grdProcedimentoInternar.Rows)
            {
                int idProc = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                if (row.RowIndex != index)
                {
                    ProcedimentoInternar proc = new ProcedimentoInternar();
                    proc.IdProcedimento = idProc;
                    proc.TipoProcedimento = row.Cells[1].Text;
                    proc.NomeProcedimento = row.Cells[2].Text;
                    proc.CodigoProcedimento = row.Cells[3].Text;
                    proc.QuantidadeProcedimento = !string.IsNullOrEmpty(row.Cells[4].Text) ? int.Parse(row.Cells[4].Text) : (int?)null;
                    proc.VlUnitarioProcedimento = !string.IsNullOrEmpty(row.Cells[5].Text) ? decimal.Parse(row.Cells[5].Text) : (decimal?)null;
                    proc.VlTotalProcedimento = !string.IsNullOrEmpty(row.Cells[5].Text) ? decimal.Parse(row.Cells[5].Text) : (decimal?)null;
                    listProcInternar.Add(proc);
                }
                else
                {
                    var tbs450 = TBS450_ATEND_INTER_PROCE_MEDIC.RetornaPelaChavePrimaria(idProc);
                    if (tbs450 != null)
                    {
                        TBS450_ATEND_INTER_PROCE_MEDIC.Delete(tbs450, true);
                    }
                }

            }
            AbreModalPadrao("AbreModalInternar();");
        }
        protected void ddlOPMInternar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idProcedimento = !string.IsNullOrEmpty(ddlOPMInternar.SelectedValue) ? int.Parse(ddlOPMInternar.SelectedValue) : 0;

            if (idProcedimento > 0)
            {
                List<OPMInternar> opmItens = new List<OPMInternar>();

                foreach (GridViewRow row in grdOPMInternar.Rows)
                {
                    OPMInternar opm = new OPMInternar();
                    opm.IdOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                    opm.TipoOPM = row.Cells[1].Text;
                    opm.NomeOPM = row.Cells[2].Text;
                    opm.CodigoOPM = row.Cells[3].Text;
                    opm.QuantidadeOPM = !string.IsNullOrEmpty(row.Cells[4].Text) ? int.Parse(row.Cells[4].Text) : (int?)null;
                    opm.FabricanteOPM = row.Cells[5].Text;
                    opm.VlUnitarioOPM = !string.IsNullOrEmpty(row.Cells[6].Text) ? decimal.Parse(row.Cells[6].Text) : (decimal?)null;
                    opm.VlTotalOPM = !string.IsNullOrEmpty(row.Cells[7].Text) ? decimal.Parse(row.Cells[7].Text) : (decimal?)null;

                    opmItens.Add(opm);
                }

                var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.ID_PROC_MEDI_PROCE == idProcedimento)
                            .Join(TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros(), x => x.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP, y => y.ID_PROC_MEDIC_SGRUP, (x, y) => new { x, y })
                            .Join(TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros(), z => z.x.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO, w => w.ID_PROC_MEDIC_GRUPO, (z, w) => new { z, w })
                            .Select(a => new
                            {
                                a.z.x.ID_PROC_MEDI_PROCE,
                                nmProcedimento = a.w.NM_PROC_MEDIC_GRUPO + " - " + a.z.y.NM_PROC_MEDIC_SGRUP,
                                a.z.x.NM_PROC_MEDI,
                                a.z.x.CO_PROC_MEDI,
                            }).FirstOrDefault();

                var tbs353 = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE)
                            .Select(w => new
                            {
                                w.VL_BASE
                            }).FirstOrDefault();

                if (tbs356 != null)
                {
                    var opmTb = new OPMInternar();
                    opmTb.IdOPM = tbs356.ID_PROC_MEDI_PROCE;
                    opmTb.TipoOPM = tbs356.nmProcedimento;
                    opmTb.NomeOPM = tbs356.NM_PROC_MEDI;
                    opmTb.CodigoOPM = tbs356.CO_PROC_MEDI;
                    if (tbs353 != null)
                        opmTb.VlUnitarioOPM = tbs353.VL_BASE;
                    opmItens.Add(opmTb);
                }

                grdOPMInternar.DataSource = opmItens;
                grdOPMInternar.DataBind();
            }
            AbreModalPadrao("AbreModalInternar();");
        }
        protected void btnDelOPMInternar_OnClick(object sender, EventArgs e)
        {
            ImageButton clickedButton = (ImageButton)sender;
            GridViewRow linha = (GridViewRow)clickedButton.Parent.Parent;
            int index = linha.RowIndex;

            List<OPMInternar> listOPMInternar = new List<OPMInternar>();
            foreach (GridViewRow row in grdOPMInternar.Rows)
            {
                int idOPM = !string.IsNullOrEmpty(row.Cells[0].Text) ? int.Parse(row.Cells[0].Text) : 0;
                if (row.RowIndex != index)
                {
                    OPMInternar opm = new OPMInternar();
                    opm.IdOPM = idOPM;
                    opm.TipoOPM = row.Cells[1].Text;
                    opm.NomeOPM = row.Cells[2].Text;
                    opm.CodigoOPM = row.Cells[3].Text;
                    opm.QuantidadeOPM = !string.IsNullOrEmpty(row.Cells[4].Text) ? int.Parse(row.Cells[4].Text) : (int?)null;
                    opm.FabricanteOPM = row.Cells[5].Text;
                    opm.VlUnitarioOPM = !string.IsNullOrEmpty(row.Cells[6].Text) ? decimal.Parse(row.Cells[6].Text) : (decimal?)null;
                    opm.VlTotalOPM = !string.IsNullOrEmpty(row.Cells[7].Text) ? decimal.Parse(row.Cells[7].Text) : (decimal?)null;
                    listOPMInternar.Add(opm);
                }
                else
                {
                    var tbs450 = TBS450_ATEND_INTER_PROCE_MEDIC.RetornaPelaChavePrimaria(idOPM);
                    if (tbs450 != null)
                    {
                        TBS450_ATEND_INTER_PROCE_MEDIC.Delete(tbs450, true);
                    }
                }

            }
            AbreModalPadrao("AbreModalInternar();");
        }
        protected void lnkbProntuario_OnClick(object sender, EventArgs e)
        {
            int paciente = 0;
            var data = DateTime.Now;

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                var chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.Checked)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);
                    if (tbs174 != null)
                    {
                        paciente = tbs174.CO_ALU.Value;
                        data = tbs174.DT_AGEND_HORAR;
                    }
                }
            }

            if (paciente == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessario selecionar um paciente para a emissão!");
                return;
            }

            string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8270_Prontuario/8271_Relatorios/RelProntuario.aspx");
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var dtIni = data.AddMonths(-1).ToShortDateString();
            var dtFim = data.ToShortDateString();

            C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuario rpt = new C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuario();
            var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, paciente, true, true, dtIni, dtFim, true, dtIni, dtFim, titulo.ToUpper());

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        #region Orçamento

        protected void lnkOrcamento_OnClick(object sender, EventArgs e)
        {
            txtObsOrcam.Text = hidObsOrcam.Value;
            txtDtValidade.Text = hidDtValidOrcam.Value;
            chkAprovado.Checked = hidCkOrcamAprov.Value == "S" ? true : false;

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void btnAddProcOrc_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridOrcamento();

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void imgExcOrc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcOrc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridOrcamento(aux);

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void ddlOperOrc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanos(ddlPlanOrc, ddlOperOrc);
            carregaGridNovaComContextoOrcamento();

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void ddlProcedOrc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlProcedOrc");
                    TextBox txtDesProced = (TextBox)linha.FindControl("txtCodigProcedOrc");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                        {
                            HiddenField hidValorUnitario = (HiddenField)linha.Cells[3].FindControl("hidValUnitProc");
                            //textbox que vai receber valor calculado
                            TextBox txtValor = (TextBox)linha.FindControl("txtValorProcedOrc");
                            TextBox txtQtd = (TextBox)linha.FindControl("txtQtdProcedOrc");
                            CalcularPreencherValoresTabelaECalculado(ddl,
                                ddlOperOrc.SelectedValue,
                                ddlPlanOrc.SelectedValue,
                                hidValorUnitario);

                            txtDesProced.Text = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue)).NM_PROC_MEDI;
                            CalculaValorTotalProcedimento((!string.IsNullOrEmpty(txtQtd.Text) ? int.Parse(txtQtd.Text) : 0),
                                hidValorUnitario.Value, txtValor);
                            CarregarValoresTotaisFooter();
                        }
                        else
                            txtDesProced.Text = ""; // Limpa os dois campos caso esteja deselecionando o procedimento
                    }
                }
            }

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void txtVlDscto_OnTextChanged(object sender, EventArgs e)
        {
            CarregarValoresTotaisFooter();

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void txtValorProcedOrc_OnTextChanged(object sender, EventArgs e)
        {
            CarregarValoresTotaisFooter();

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void txtQtdProcedOrc_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    txt = (TextBox)linha.FindControl("txtQtdProcedOrc");
                    if (txt.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            //textbox que vai receber valor calculado
                            TextBox txtValor = (TextBox)linha.FindControl("txtValorProcedOrc");
                            //valor unitário do procedimento
                            string vlProcUnit = ((HiddenField)linha.FindControl("hidValUnitProc")).Value;
                            //Quantidade de sessões
                            int qt = int.Parse(txt.Text);
                            CalculaValorTotalProcedimento(qt, vlProcUnit, txtValor);
                            CarregarValoresTotaisFooter();
                        }
                    }
                }
            }

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        #endregion

        #region Exames

        protected void lnkExame_OnClick(object sender, EventArgs e)
        {
            var res = (from modelo in TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().Where(x => x.FL_TIPO_MODELO == "X")
                       select new { modelo.ID_MODEL_MEDIC, modelo.NO_MODEL_MEDIC }).OrderBy(w => w.ID_MODEL_MEDIC).OrderBy(x => x.NO_MODEL_MEDIC).ToList();

            if (res.Count > 0)
            {
                DropDownListModeloGuiaExames.DataValueField = "ID_MODEL_MEDIC";
                DropDownListModeloGuiaExames.DataTextField = "NO_MODEL_MEDIC";
                DropDownListModeloGuiaExames.DataSource = res;
                DropDownListModeloGuiaExames.DataBind();
                DropDownListModeloGuiaExames.Items.Insert(0, new ListItem("Selecione", ""));
            }
            txtObserExame.Text = hidObserExame.Value;
            DropDownListModeloExames.Enabled = PanelExamesLivre.Visible;
            RadioButtonLivreExames.Checked = PanelExamesLivre.Visible;
            RadioButtonPesquisaExames.Checked = PanelExamesPesquisa.Visible;
            DropDownListModeloExames.SelectedValue = !string.IsNullOrEmpty(HiddenFieldModeloExamesID.Value) ? HiddenFieldModeloExamesID.Value : "";
            CarregaModelosExam();
            AbreModalPadrao("AbreModalExames();");
        }

        protected void lnkAddProcPla_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridExame();

            AbreModalPadrao("AbreModalExames();");
        }

        protected void imgExcPla_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdExame.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdExame.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcPla");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridExame(aux);

            AbreModalPadrao("AbreModalExames();");
        }

        protected void ddlOperProcPlan_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanos(ddlPlanProcPlan, ddlOperProcPlan);

            AbreModalPadrao("AbreModalExames();");
        }

        protected void ddlPlanProcPlan_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaGridNovaComContextoExame();

            AbreModalPadrao("AbreModalExames();");
        }

        protected void ddlExame_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdExame.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdExame.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlExame");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        TextBox txtDesProced = (TextBox)linha.FindControl("txtCodigProcedPla");
                        TextBox vlrProced = (TextBox)linha.FindControl("txtValorProced");

                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                        {
                            var proc = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue));

                            txtDesProced.Text = proc != null ? proc.NM_PROC_MEDI : "-";

                            proc.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                            if (proc.TBS353_VALOR_PROC_MEDIC_PROCE != null && proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null)
                                vlrProced.Text = proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE.ToString();
                        }
                        else
                        {
                            txtDesProced.Text = ""; // Limpa os dois campos caso esteja deselecionando o procedimento
                            vlrProced.Text = "";
                        }
                    }
                }
            }

            AbreModalPadrao("AbreModalExames();");
        }

        #endregion

        #region Medicamentos

        protected void DropDownListModelo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModeloMed();
            AbreModalPadrao("AbreModalMedicamentos();");
        }

        private void CarregaModeloMed()
        {
            if (!String.IsNullOrEmpty(DropDownListModelo.SelectedValue))
            {
                HiddenFieldModeloID.Value = DropDownListModelo.SelectedValue;
                var modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModelo.SelectedValue));
                ASPxHtmlEditor1.Html = modelo.DE_CONTE_MODEL_MEDIC;
                TextBoxNomeModelo.Text = modelo.NO_MODEL_MEDIC;
            }
            else
            {
                HiddenFieldModeloID.Value = "";
                ASPxHtmlEditor1.Html = "";
                TextBoxNomeModelo.Text = "";
            }
          
        }
        protected void drpProfResp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(drpProfResp.SelectedValue))
            {
                CarregaModelosReceituario(DropDownListModelo, int.Parse(drpProfResp.SelectedValue), HiddenFieldModeloID.Value);
                CarregaModelosExames(DropDownListModeloExames, int.Parse(drpProfResp.SelectedValue), HiddenFieldModeloExamesID.Value);
                HiddenFieldModeloID.Value = "";
                HiddenFieldModeloExamesID.Value = "";
            }
        }
        protected void lnkMedic_OnClick(object sender, EventArgs e)
        {
            txtObserMedicam.Text = hidObserMedicam.Value;
            RadioButtonPesquisa.Checked = PanelPesquisa.Visible;
            RadioButtonLivre.Checked = PanelLivre.Visible;
            rdbMedic.Checked = true;
            RadioButtonContEspecial.Checked = HiddenFieldTipoReceituario.Value == "E";
            RadioButtonNormal.Checked = HiddenFieldTipoReceituario.Value != "E";
            grdPesqMedic.DataSource = null;
            grdPesqMedic.DataBind();
            DropDownListModelo.SelectedValue = !string.IsNullOrEmpty(HiddenFieldModeloID.Value) ? HiddenFieldModeloID.Value : "";
            CarregaModeloMed();
            AbreModalPadrao("AbreModalMedicamentos();");
        }

        protected void imgbPesqMedic_OnClick(object sender, EventArgs e)
        {
            //if (drpGrupoMedic.SelectedValue != "0" || drpSubGrupoMedic.SelectedValue != "0" || (!String.IsNullOrEmpty(txtMedicamento.Text) && rdbMedic.Checked) || (!String.IsNullOrEmpty(txtPrincipio.Text) && rdbPrinc.Checked))
            CarregarMedicamentos();
            /*else
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Por favor informe pelo menos um dos parametros de pesquisa e tente novamente!");*/

            AbreModalPadrao("AbreModalMedicamentos();");
        }

        protected void lnkAddMedicam_OnClick(object sender, EventArgs e)
        {
            var marcado = false;
            foreach (GridViewRow l in grdPesqMedic.Rows)
            {
                var rdb = (RadioButton)l.FindControl("rdbMedicamento");

                if (rdb.Checked)
                {
                    marcado = true;
                    continue;
                }
            }

            if (!marcado)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um medicamento!");
            else
                CriaNovaLinhaGridMedic();

            AbreModalPadrao("AbreModalMedicamentos();");
        }

        protected void imgExcMedic_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdMedicamentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdMedicamentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcMedic");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridMedic(aux);

            AbreModalPadrao("AbreModalMedicamentos();");
        }

        protected void drpGrupoMedic_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupoMedicamento(drpGrupoMedic, drpSubGrupoMedic, true);
            AbreModalPadrao("AbreModalMedicamentos();");
        }
        protected void RadioButtonLivre_OnCheckedChanged(object sender, EventArgs e)
        {
            PanelPesquisa.Visible = false;
            PanelLivre.Visible = true;
            AbreModalPadrao("AbreModalMedicamentos();");
        }
        protected void RadioButtonAmbNormal_OnCheckedChanged(object sender, EventArgs e)
        {
            PanelLivreAmbulatorio.Visible = false;
            PanelNormalAmbulatorio.Visible = true;
            DropDownListModeloAmb.Enabled = false;
            DropDownListModeloAmb.SelectedValue = "";
            AbreModalPadrao("AbreModalAmbulatorio();");
        }
        protected void RadioButtonAmbLivre_OnCheckedChanged(object sender, EventArgs e)
        {
            PanelNormalAmbulatorio.Visible = false;
            DropDownListModeloAmb.Enabled = true;
            PanelLivreAmbulatorio.Visible = true;
            AbreModalPadrao("AbreModalAmbulatorio();");
        }
        protected void DropDownListModeloAmb_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModelosAmb();

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        private void CarregaModelosAmb()
        {
            if (!String.IsNullOrEmpty(DropDownListModeloAmb.SelectedValue))
            {
                HiddenFieldModeloAmbID.Value = DropDownListModeloAmb.SelectedValue;
                var modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloAmb.SelectedValue));
                ASPxHtmlEditorProcedimento.Html = modelo.DE_CONTE_MODEL_MEDIC;
                TextBoxNomeModeloAmb.Text = modelo.NO_MODEL_MEDIC;
            }
            else
            {
                ASPxHtmlEditorProcedimento.Html = "";
                HiddenFieldModeloAmbID.Value = "";
                TextBoxNomeModeloAmb.Text = "";
            }
        }
        protected void DropDownListModeloEncam_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownListModeloEncam.SelectedValue))
            {
                HiddenFieldModeloAmbID.Value = DropDownListModeloEncam.SelectedValue;
                var modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloEncam.SelectedValue));
                ASPxHtmlEditorEncam.Html = modelo.DE_CONTE_MODEL_MEDIC;
                TextBoxModeloEncam.Text = modelo.NO_MODEL_MEDIC;
            }
            else
            {
                ASPxHtmlEditorEncam.Html = "";
                TextBoxModeloEncam.Text = "";
            }
            AbreModalPadrao("AbreModalEncam();");
        }
        protected void RadioButtonEncamLivre_OnCheckedChanged(object sender, EventArgs e)
        {
           
        }
        
        protected void RadioButtonContEspecial_OnCheckedChanged(object sender, EventArgs e)
        {
            HiddenFieldTipoReceituario.Value = "E";
            AbreModalPadrao("AbreModalMedicamentos();");
        }
        protected void RadioButtonNormal_OnCheckedChanged(object sender, EventArgs e)
        {
            HiddenFieldTipoReceituario.Value = "";
            AbreModalPadrao("AbreModalMedicamentos();");
        }
        protected void DropDownListModeloGuiaExames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownListModeloGuiaExames.SelectedValue))
            {
                Response.Clear();
                //Nome do arquivo que será visto no momento do download
                var modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloGuiaExames.SelectedValue));
                Response.AddHeader("content-disposition", "attachment;filename=" + modelo.NO_MODEL_MEDIC + ".xls");
                Response.ContentType = "application/ms-excel";
                //Ira escrever o conteudo do arquivo no buffer do response
                Response.WriteFile(@modelo.DE_ENDER_ARQUI_EXTER);
                Response.End();
            }
            AbreModalPadrao("AbreModalExames();");
        }
        protected void DropDownListModeloGuia_OnSelectedIndexChanged(object sender, EventArgs e)
        {          
            if (!String.IsNullOrEmpty(DropDownListModeloGuia.SelectedValue))
            {
                Response.Clear();
                //Nome do arquivo que será visto no momento do download
                var modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloGuia.SelectedValue));
                Response.AddHeader("content-disposition", "attachment;filename=" + modelo.NO_MODEL_MEDIC+ ".xls");
                Response.ContentType = "application/ms-excel";
                //Ira escrever o conteudo do arquivo no buffer do response
                Response.WriteFile(@modelo.DE_ENDER_ARQUI_EXTER);
                Response.End();
               
            }
            AbreModalPadrao("AbreModalGuiaPlano();");
        }
        protected void RadioButtonGuiaLivre_OnCheckedChanged(object sender, EventArgs e)
        {
            DropDownListModeloGuia.Enabled = true;
            txtDtGuia.Enabled = false;
            drpPacienteGuia.Enabled = false;
            drpOperGuia.Enabled = false;
            txtObsGuia.Enabled = false;
            lnkbImprimirGuia.Enabled = false;
            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        protected void RadioButtonGuiaExames_OnCheckedChanged(object sender, EventArgs e)
        {
            DropDownListModeloGuiaExames.Enabled = true;
            PanelGuiaExames.Visible = true;
            PanelExamesPesquisa.Visible = false;
            PanelExamesLivre.Visible = false;
            AbreModalPadrao("AbreModalExames();");
        }
        protected void RadioButtonGuiaInterno_OnCheckedChanged(object sender, EventArgs e)
        {
            DropDownListModeloGuia.Enabled = false;
            txtDtGuia.Enabled = true;
            drpPacienteGuia.Enabled = true;
            drpOperGuia.Enabled = true;
            txtObsGuia.Enabled = true;
            lnkbImprimirGuia.Enabled = true;
            AbreModalPadrao("AbreModalGuiaPlano();");
        }
        protected void RadioButtonPesquisa_OnCheckedChanged(object sender, EventArgs e)
        {
            PanelPesquisa.Visible = true;
            PanelLivre.Visible = false;
            AbreModalPadrao("AbreModalMedicamentos();");
        }
        protected void RadioButtonLivreExames_OnCheckedChanged(object sender, EventArgs e)
        {
            DropDownListModeloExames.Enabled = true;
            PanelExamesPesquisa.Visible = false;
            PanelGuiaExames.Visible = false;
            PanelExamesLivre.Visible = true;
            AbreModalPadrao("AbreModalExames();");
        }
        protected void RadioButtonPesquisaExames_OnCheckedChanged(object sender, EventArgs e)
        {              
            PanelExamesPesquisa.Visible = true;
            PanelGuiaExames.Visible = false;
            DropDownListModeloExames.Enabled = false;
            PanelExamesLivre.Visible = false;
            AbreModalPadrao("AbreModalExames();");
        }
        protected void  DropDownListModeloExames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModelosExam();
            AbreModalPadrao("AbreModalExames();");
        }

        private void CarregaModelosExam()
        {

            if (!String.IsNullOrEmpty(DropDownListModeloExames.SelectedValue))
            {
                HiddenFieldModeloExamesID.Value = DropDownListModeloExames.SelectedValue;
                var modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloExames.SelectedValue));
                ASPxHtmlEditorExame.Html = modelo.DE_CONTE_MODEL_MEDIC;
                TextBoxNomeModeloExame.Text = modelo.NO_MODEL_MEDIC;
            }
            else
            {
                HiddenFieldModeloExamesID.Value = "";
                ASPxHtmlEditorExame.Html = "";
                TextBoxNomeModeloExame.Text = "";
            }
        }
        #endregion

        #region Novo Exame

        protected void imgNovoExam_OnClick(object sender, EventArgs e)
        {
            AbreModalPadrao("AbreModalNovoExam();");
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
            ScriptManager.RegisterStartupScript(
                   this.Page,
                   this.GetType(),
                   "Acao",
                   "AbreModalNovoExam();",
                   true
               );
        }

        protected void lnkNovoExam_OnClick(object sender, EventArgs e)
        {
            #region Novo Exame
            TBS356_PROC_MEDIC_PROCE tbs356 = new TBS356_PROC_MEDIC_PROCE();

            tbs356.NM_PROC_MEDI = txtNoProcedimento.Text;
            tbs356.TBS354_PROC_MEDIC_GRUPO = TBS354_PROC_MEDIC_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo2.SelectedValue));
            tbs356.TBS355_PROC_MEDIC_SGRUP = TBS355_PROC_MEDIC_SGRUP.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo2.SelectedValue));
            tbs356.CO_PROC_MEDI = txtCodProcMedic.Text;
            tbs356.CO_TIPO_PROC_MEDI = "EX";
            tbs356.QT_AUXI_PROC_MEDI = (!string.IsNullOrEmpty(txtQTAux.Text) ? decimal.Parse(txtQTAux.Text) : (decimal?)null);
            //-----------------------------------------------------------------------------------------------------------------------------
            tbs356.QT_SESSO_AUTOR = (!string.IsNullOrEmpty(txtQtSecaoAutorizada.Text) ? int.Parse(txtQtSecaoAutorizada.Text) : (int?)null);

            tbs356.CO_CLASS_FUNCI = "M;";
            //---------------------------------------------------------------------------------------------------------------------------------------
            tbs356.QT_ANES_PROC_MEDI = (!string.IsNullOrEmpty(txtQTAnest.Text) ? decimal.Parse(txtQTAnest.Text) : (decimal?)null);
            tbs356.DE_OBSE_PROC_MEDI = (!string.IsNullOrEmpty(txtObsProced.Text) ? txtObsProced.Text : null);
            tbs356.FL_AUTO_PROC_MEDI = (chkRequerAuto.Checked ? "S" : "N");

            //Agrupadora
            /*if (!string.IsNullOrEmpty(ddlAgrupadora.SelectedValue))
            {
                TBS356_PROC_MEDIC_PROCE tbs356ob = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlAgrupadora.SelectedValue));
                tbs356.ID_AGRUP_PROC_MEDI_PROCE = (!string.IsNullOrEmpty(ddlAgrupadora.SelectedValue) ? int.Parse(ddlAgrupadora.SelectedValue) : (int?)null);
                tbs356.CO_OPER_AGRUP = tbs356ob.CO_OPER;
            }
            else*/
            {
                tbs356.ID_AGRUP_PROC_MEDI_PROCE = (int?)null;
                tbs356.CO_OPER_AGRUP = null;
            }

            //Operadora
            tbs356.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
            //tbs356.CO_OPER = (!string.IsNullOrEmpty(txtCodOper.Text) ? txtCodOper.Text : null);

            tbs356.CO_COL_SITU_PROC_MEDIC = LoginAuxili.CO_COL;
            tbs356.CO_SITU_PROC_MEDI = "A";
            tbs356.DT_SITU_PROC_MEDI = DateTime.Now;

            //Salva essas informações apenas quando for cadastro novo
            switch (tbs356.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs356.DT_CADAS_PROC = DateTime.Now;
                    tbs356.CO_COL_CADAS_PROC = LoginAuxili.CO_COL;
                    tbs356.CO_EMP_CADAS_PROC = LoginAuxili.CO_EMP;
                    tbs356.IP_CADAS_PROC = Request.UserHostAddress;
                    break;
            }

            //Se for operação de inserção, salva as informações de valores inseridas.
            #region Salva Valores

            //Persiste as informações de valores
            TBS353_VALOR_PROC_MEDIC_PROCE tbs353 = new TBS353_VALOR_PROC_MEDIC_PROCE();
            tbs353.TBS356_PROC_MEDIC_PROCE = tbs356;
            tbs353.VL_CUSTO = decimal.Parse(txtVlCusto.Text);
            tbs353.VL_BASE = decimal.Parse(txtVlBase.Text);
            tbs353.VL_RESTI = (!string.IsNullOrEmpty(txtVlRestitu.Text) ? decimal.Parse(txtVlRestitu.Text) : (decimal?)null);
            tbs353.CO_COL_LANC = LoginAuxili.CO_COL;
            tbs353.CO_EMP_LANC = LoginAuxili.CO_EMP;
            tbs353.IP_LANC = Request.UserHostAddress;
            tbs353.DT_LANC = DateTime.Now;
            tbs353.FL_STATU = "A";
            TBS353_VALOR_PROC_MEDIC_PROCE.SaveOrUpdate(tbs353, true);

            #endregion

            //CurrentPadraoCadastros.CurrentEntity = tbs356;
            TBS356_PROC_MEDIC_PROCE.SaveOrUpdate(tbs356);

            #endregion
        }

        #endregion

        #region Novo Medicamento

        protected void lnkNovoMedicam_OnClick(object sender, EventArgs e)
        {
            AbreModalPadrao("AbreModalNovoMedic();");
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupoMedicamento(ddlGrupo, ddlSubGrupo);
            AbreModalPadrao("AbreModalNovoMedic();");
        }

        protected void lnkNovoMedic_OnClick(object sender, EventArgs e)
        {
            #region Novo Medicamentos

            ////Realiza as pers   istências do orçamento

            TB90_PRODUTO tb90 = new TB90_PRODUTO();

            ////--------> Como o CO_EMP é campo chave, só é permitido inserí-lo quando for inclusão

            tb90.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
         
            ////Informações padrões
            tb90.NO_PROD = txtNProduto.Text;
            tb90.DES_PROD = txtDescProduto.Text;
            tb90.NO_PROD_RED = txtNReduz.Text;
            tb90.CO_REFE_PROD = txtCodRef.Text;
            tb90.TB260_GRUPO = ddlGrupo.SelectedValue != "" ? TB260_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue)) : null;
            tb90.TB261_SUBGRUPO = ddlSubGrupo.SelectedValue != "" ? TB261_SUBGRUPO.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo.SelectedValue)) : null;
            tb90.TB124_TIPO_PRODUTO = TB124_TIPO_PRODUTO.RetornaPelaChavePrimaria(16);//Seleciona o tipo Medicamento
            //tb90.TB124_TIPO_PRODUTO = ddlTipoProduto.SelectedValue != "" ? TB124_TIPO_PRODUTO.RetornaPelaChavePrimaria(int.Parse(ddlTipoProduto.SelectedValue)) : null;
            //tb90.TB95_CATEGORIA = ddlCategoria.SelectedValue != "" ? TB95_CATEGORIA.RetornaPelaChavePrimaria(int.Parse(ddlCategoria.SelectedValue)) : null;
            //tb90.FLA_IMPORTADO = ddlImportado.SelectedValue;

            ////Salva data de cadastro somente se for o caso
            switch (tb90.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tb90.DT_CADA_PROD = DateTime.Now;
                    break;
            }

            //tb90.CO_SITU_PROD = ddlSituacao.SelectedValue;
            tb90.DT_ALT_REGISTRO = DateTime.Now;


            ////Características
            tb90.TB89_UNIDADES = ddlUnidade.SelectedValue != "" ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)) : null;
            //tb90.QT_QX = (!string.IsNullOrEmpty(txtQtCx.Text) ? int.Parse(txtQtCx.Text) : (int?)null);
            //tb90.CO_BARRAS = (!string.IsNullOrEmpty(txtCodBarr.Text) ? txtCodBarr.Text : null);
            //tb90.TB93_MARCA = ddlMarca.SelectedValue != "" ? TB93_MARCA.RetornaPelaChavePrimaria(int.Parse(ddlMarca.SelectedValue)) : null;
            //tb90.TB97_COR = ddlCor.SelectedValue != "" ? TB97_COR.RetornaPelaChavePrimaria(int.Parse(ddlCor.SelectedValue)) : null;
            //tb90.TB98_TAMANHO = ddlTamanho.SelectedValue != "" ? TB98_TAMANHO.RetornaPelaChavePrimaria(int.Parse(ddlTamanho.SelectedValue)) : null;
            //tb90.NU_PESO_PROD = txtPeso.Text != "" ? Convert.ToDecimal(txtPeso.Text) : 0;
            //tb90.NU_VOL_PROD = txtVolume.Text != "" ? Convert.ToDecimal(txtVolume.Text) : 0;

            ////Quantidades
            //tb90.CO_UNID_FAB = (ddlUnidFab.SelectedValue != "" ? int.Parse(ddlUnidFab.SelectedValue) : (int?)null);
            //tb90.QT_UNID_FAB = (!string.IsNullOrEmpty(txtQtdeFab.Text) ? int.Parse(txtQtdeFab.Text) : (int?)null);
            //tb90.CO_UNID_COMPRA = (ddlUnidComp.SelectedValue != "" ? int.Parse(ddlUnidComp.SelectedValue) : (int?)null);
            //tb90.QT_UNID_COMPRA = (!string.IsNullOrEmpty(txtQtdeComp.Text) ? int.Parse(txtQtdeComp.Text) : (int?)null);
            //tb90.CO_UNID_VENDA = (ddlUnidComp.SelectedValue != "" ? int.Parse(ddlUnidComp.SelectedValue) : (int?)null);
            //tb90.QT_UNID_VENDA = (!string.IsNullOrEmpty(txtQtdeVend.Text) ? int.Parse(txtQtdeVend.Text) : (int?)null);

            ////Segurança
            //tb90.QT_SEG_MIN = (!string.IsNullOrEmpty(txtSegMin.Text) ? int.Parse(txtSegMin.Text) : (int?)null);
            //tb90.QT_SEG_MAX = (!string.IsNullOrEmpty(txtSegMax.Text) ? int.Parse(txtSegMax.Text) : (int?)null);

            ////Outras Informações
            //tb90.FL_FARM_POP = (chkFarmPopul.Checked ? "S" : "N");
            //tb90.CO_FABRICANTE = ddlFabricante.SelectedValue != "" ? int.Parse(ddlFabricante.SelectedValue) : (int?)null;
            //tb90.NU_CO_FABRICANTE = (!string.IsNullOrEmpty(txtCodFabricante.Text) ? int.Parse(txtCodFabricante.Text) : (int?)null);
            //tb90.CO_FORNECEDOR = ddlFornecedor.SelectedValue != "" ? int.Parse(ddlFornecedor.SelectedValue) : (int?)null;
            //tb90.QT_DIAS_FORNECEDOR = (!string.IsNullOrEmpty(txtDiasEntrForne.Text) ? int.Parse(txtDiasEntrForne.Text) : (int?)null);
            tb90.CO_MS_ANVISA = null;
            //tb90.NU_NCM = (!string.IsNullOrEmpty(txtNNCM.Text) ? txtNNCM.Text : null);
            //tb90.CO_TIPO_PSICO = ddlTipoPsico.SelectedValue != "" ? int.Parse(ddlTipoPsico.SelectedValue) : (int?)null;
            tb90.NO_PRINCIPIO_ATIVO = (!string.IsNullOrEmpty(txtPrinAtiv.Text) ? txtPrinAtiv.Text : null);
            //tb90.CO_COR_ALERTA = (ddlCorAlerta.SelectedValue != "" ? int.Parse(ddlCorAlerta.SelectedValue) : (int?)null);
            //tb90.DE_OBSERVACAO = (!string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : null);

            ////Dados Tributários
            //tb90.NU_POR_GRP = (!string.IsNullOrEmpty(txtPerTribuGrp.Text) ? decimal.Parse(txtPerTribuGrp.Text) : (decimal?)null);
            //tb90.NU_GRP = (!string.IsNullOrEmpty(txtGrpTribu.Text) ? decimal.Parse(txtGrpTribu.Text) : (decimal?)null);
            //tb90.CO_CLASSIFICACAO = ddlClassTribu.SelectedValue != "" ? int.Parse(ddlClassTribu.SelectedValue) : (int?)null;
            //tb90.CO_PIS_COFINS = ddlTribPISConfins.SelectedValue != "" ? int.Parse(ddlTribPISConfins.SelectedValue) : (int?)null;

            ////Dados Comerciais
            //tb90.FL_MANTER_MARGEM = chkManterMargem.Checked ? "S" : "N";
            //tb90.FL_MANTER_VENDA = chkManterValVenda.Checked ? "S" : "N";
            //tb90.VL_CUSTO = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);
            //tb90.VL_VENDA = (!string.IsNullOrEmpty(txtValVenda.Text) ? decimal.Parse(txtValVenda.Text) : (decimal?)null);
            //tb90.VL_VENDA_MAX = (!string.IsNullOrEmpty(txtValVendMax.Text) ? decimal.Parse(txtValVendMax.Text) : (decimal?)null);
            //tb90.NU_POR_MARG = (!string.IsNullOrEmpty(txtPerMarg.Text) ? decimal.Parse(txtPerMarg.Text) : (decimal?)null);
            ////tb90.NU_POR_DESC = (!string.IsNullOrEmpty(txtPerDescMarg.Text) ? decimal.Parse(txtPerDescMarg.Text) : (decimal?)null);
            //tb90.NU_POR_COMI = (!string.IsNullOrEmpty(txtPerComi.Text) ? decimal.Parse(txtPerComi.Text) : (decimal?)null);
            //tb90.NU_POR_DESC_COMI = (!string.IsNullOrEmpty(txtPerDescComi.Text) ? decimal.Parse(txtPerDescComi.Text) : (decimal?)null);
            //tb90.NU_POR_PROM = (!string.IsNullOrEmpty(txtPerProm.Text) ? decimal.Parse(txtPerProm.Text) : (decimal?)null);
            //tb90.DT_VALIDADE = (!string.IsNullOrEmpty(txtValidade.Text) ? DateTime.Parse(txtValidade.Text) : (DateTime?)null);
            //tb90.CO_CONVENIO = ddlConvenio.SelectedValue != "" ? int.Parse(ddlConvenio.SelectedValue) : (int?)null;
            //tb90.NO_CLAS_ABC = ddlCLasABC.SelectedValue;
            ////tb90.NO_ROTATIVO = (!string.IsNullOrEmpty(txtRotativ.Text) ? txtRotativ.Text : null);

            //tb90.VL_UNIT_PROD = txtValor.Text != "" ? Convert.ToDecimal(txtValor.Text) : 0;
            tb90.NU_DUR_PROD = 0;
            //string strNomeUsuario = LoginAuxili.NOME_USU_LOGADO;
            //tb90.NOM_USUARIO = strNomeUsuario.ToString().Substring(0, (strNomeUsuario.Length > 30 ? 29 : strNomeUsuario.Length));


            TB90_PRODUTO.SaveOrUpdate(tb90, true);
            drpGrupoMedic.SelectedValue = ddlGrupo.SelectedValue;
            drpSubGrupoMedic.SelectedValue = ddlSubGrupo.SelectedValue;
            txtMedicamento.Text = tb90.NO_PROD;
            CarregarMedicamentos();
            #endregion
        }

        private void CarregarBotoes()
        {
            var empresa = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            empresa.TB83_PARAMETROReference.Load();
            BtnProntuCon.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_PRONT_PAD == "S";
            ButtonProntuMod.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_PRONT_MOD == "S";
            lnkMedic.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEN_MED == "S";
            lnkExame.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_EXAM == "S";
            lnkAmbul.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_AMB == "S";
            BtnGuia.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_GUIA == "S";
            BtnFicha.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_FIXA_ATEND == "S";
            BtnAtestado.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_ATEST == "S";
            lnkbAnexos.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_ARQ == "S";
            BtnObserv.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_OBSERV == "S";
            ButtonEncam.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_ENCAM == "S";
            BtnInternar.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_INTERNACAO == "S";
            BtnLaudo.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_LAUDO_TEC == "S";
            ButtonCirurgia.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_CIRURGIA == "S";
            btnSalvaAtend.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_SALVAR == "S";
            ButtonEspera.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_ESPERA == "S";
            BtnFinalizar.Visible = empresa.TB83_PARAMETRO.FL_OPCAO_ATEND_FINALIZAR == "S";
        }

        #endregion

        protected void BtnSalvar_OnClick(object sender, EventArgs e)
        {
            Persistencias(false);
        }

        protected void BtnFinalizar_OnClick(object sender, EventArgs e)
        {
            Persistencias(true);
        }

        #region Anexos

        protected void lnkbAnexos_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAgenda.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda!");
                grdPacientes.Focus();
                return;
            }

            var co_alu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;

            CarregarAnexosAssociados(co_alu);

            AbreModalPadrao("AbreModalAnexos();");
        }

        protected void lnkbAnexar_OnClick(object sender, EventArgs e)
        {
            if (!flupAnexo.HasFile)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor selecionar o arquivo");
                AbreModalPadrao("AbreModalAnexos();");
                return;
            }

            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para anexar o arquivo");
                return;
            }

            try
            {
                var tbs392 = new TBS392_ANEXO_ATEND();
                tbs392.TBS390_ATEND_AGEND = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                var co_alu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;
                tbs392.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(co_alu);
                tbs392.NM_TITULO = txtNomeAnexo.Text;
                tbs392.TP_ANEXO = drpTipoAnexo.SelectedValue;
                tbs392.DE_OBSER = txtObservAnexo.Text;

                Stream fs = flupAnexo.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                tbs392.ANEXO = bytes;
                tbs392.NM_ANEXO = flupAnexo.PostedFile.FileName;
                tbs392.EX_ANEXO = Path.GetExtension(flupAnexo.FileName);
                tbs392.NU_CLEN_ANEXO = flupAnexo.PostedFile.ContentLength;
                tbs392.DE_CTIP_ANEXO = flupAnexo.PostedFile.ContentType;

                //Dados do cadastro e da situação
                tbs392.CO_SITUA = "A";
                tbs392.DT_CADAS = tbs392.DT_SITUA = DateTime.Now;
                tbs392.CO_COL_CADAS = tbs392.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs392.CO_EMP_COL_CADAS = tbs392.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                tbs392.CO_EMP_CADAS = tbs392.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs392.IP_CADAS = tbs392.IP_SITUA = Request.UserHostAddress;

                TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Arquivo anexado com sucesso!");
            }
            catch (Exception erro)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Ocorreu um erro ao salvar o arquivo. Erro:" + erro.Message);
                AbreModalPadrao("AbreModalAnexos();");
                return;
            }
        }

        protected void imgbBxrAnexo_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            foreach (GridViewRow li in grdAnexos.Rows)
            {
                img = (ImageButton)li.FindControl("imgbBxrAnexo");

                if (img.ClientID == atual.ClientID)
                {
                    var idAnexo = int.Parse(((HiddenField)li.FindControl("hidIdAnexo")).Value);

                    var tbs392 = TBS392_ANEXO_ATEND.RetornaPelaChavePrimaria(idAnexo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = tbs392.DE_CTIP_ANEXO;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + tbs392.NM_ANEXO);
                    Response.BinaryWrite(tbs392.ANEXO);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void imgbExcAnexo_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            foreach (GridViewRow li in grdAnexos.Rows)
            {
                img = (ImageButton)li.FindControl("imgbExcAnexo");

                if (img.ClientID == atual.ClientID)
                {
                    var idAnexo = int.Parse(((HiddenField)li.FindControl("hidIdAnexo")).Value);

                    var tbs392 = TBS392_ANEXO_ATEND.RetornaPelaChavePrimaria(idAnexo);

                    TBS392_ANEXO_ATEND.Delete(tbs392, true);

                    AuxiliPagina.EnvioMensagemSucesso(this.Page, "Arquivo EXCLUIDO com sucesso!");
                }
            }
        }

        #endregion

        #region Campo Livre

        protected void BtnObserv_OnClick(object sender, EventArgs e)
        {
            txtObservacoes.Text = hidTxtObserv.Value;

            AbreModalPadrao("AbreModalObservacao();");
        }
        protected void BtnInternar_OnClick(object sender, EventArgs e)
        {
            int idAtendAgend = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : 0;

            if (idAtendAgend > 0)
            {
                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                var tbs448 = TBS448_ATEND_INTER_HOSPI.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).FirstOrDefault();
                var idTbs448 = tbs448 != null ? tbs448.ID_ATEND_INTER : 0;
                var tbs451 = TBS451_INTER_REGIST.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == idTbs448).FirstOrDefault();

                txtRegAtendimentoInternar.Text = tbs390.NU_REGIS;
                tbs390.TB07_ALUNOReference.Load();
                txtNomePacienteInternar.Text = tbs390.TB07_ALUNO.NO_ALU;
                drpCaraterInternacao.DataSource = TBS442_TIPO_CARAT.RetornaTodosRegistros().OrderBy(x => x.NO_CARAT);
                drpCaraterInternacao.DataTextField = "NO_CARAT";
                drpCaraterInternacao.DataValueField = "ID_CARAT";
                drpCaraterInternacao.DataBind();
                drpCaraterInternacao.Items.Insert(0, new ListItem("Selecione", ""));

                drpTipoInternacao.DataSource = TBS443_TIPO_INTER.RetornaTodosRegistros().OrderBy(x => x.NO_TP_INTER);
                drpTipoInternacao.DataTextField = "NO_TP_INTER";
                drpTipoInternacao.DataValueField = "ID_TP_INTER";
                drpTipoInternacao.DataBind();
                drpTipoInternacao.Items.Insert(0, new ListItem("Selecione", ""));

                drpRegimeInternacao.DataSource = TBS444_TIPO_REGIM.RetornaTodosRegistros().OrderBy(x => x.NO_TP_REGIM);
                drpRegimeInternacao.DataTextField = "NO_TP_REGIM";
                drpRegimeInternacao.DataValueField = "ID_TP_REGIM";
                drpRegimeInternacao.DataBind();
                drpRegimeInternacao.Items.Insert(0, new ListItem("Selecione", ""));

                drpTipoDoenca.DataSource = TBS446_TIPO_DOENCA.RetornaTodosRegistros().OrderBy(x => x.NO_TP_DOENCA);
                drpTipoDoenca.DataTextField = "NO_TP_DOENCA";
                drpTipoDoenca.DataValueField = "ID_TP_DOENCA";
                drpTipoDoenca.DataBind();
                drpTipoDoenca.Items.Insert(0, new ListItem("Selecione", ""));

                drpIndicacaoAcidente.DataSource = TBS445_TIPO_INDIC_ACIDE.RetornaTodosRegistros().OrderBy(x => x.NO_TP_ACIDE);
                drpIndicacaoAcidente.DataTextField = "NO_TP_ACIDE";
                drpIndicacaoAcidente.DataValueField = "ID_TP_ACIDE";
                drpIndicacaoAcidente.DataBind();
                drpIndicacaoAcidente.Items.Insert(0, new ListItem("Selecione", ""));

                drpTipoAcomodacao.DataSource = TB14_DEPTO.RetornaTodosRegistros().Where(x => x.FL_INTER.Equals("S") || x.FL_UTI.Equals("S"));
                drpTipoAcomodacao.DataTextField = "NO_DEPTO";
                drpTipoAcomodacao.DataValueField = "CO_DEPTO";
                drpTipoAcomodacao.DataBind();
                drpTipoAcomodacao.Items.Insert(0, new ListItem("Selecione", ""));

                ddlTipoProcedimentoInternar.DataSource = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.CO_TIPO_PROC_MEDI.Equals("IN")).OrderBy(x => x.NM_PROC_MEDI);
                ddlTipoProcedimentoInternar.DataTextField = "NM_PROC_MEDI";
                ddlTipoProcedimentoInternar.DataValueField = "ID_PROC_MEDI_PROCE";
                ddlTipoProcedimentoInternar.DataBind();
                ddlTipoProcedimentoInternar.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTipoProcedimentoInternar.Enabled = tbs451 == null ? true : false;

                ddlOPMInternar.DataSource = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.CO_TIPO_PROC_MEDI.Equals("OP")).OrderBy(x => x.NM_PROC_MEDI);
                ddlOPMInternar.DataTextField = "NM_PROC_MEDI";
                ddlOPMInternar.DataValueField = "ID_PROC_MEDI_PROCE";
                ddlOPMInternar.DataBind();
                ddlOPMInternar.Items.Insert(0, new ListItem("Selecione", ""));
                ddlOPMInternar.Enabled = tbs451 == null ? true : false;

                lblInfoInternar.Text = tbs451 != null ? "Internação " + tbs451.NU_REGIS + " registrada no dia " + tbs451.DT_INTER.ToShortDateString() + " às " + tbs451.HR_INTER.Value.Hours + ":" + (tbs451.HR_INTER.Value.Minutes > 0 ? tbs451.HR_INTER.Value.Minutes.ToString() : "00") + "." : "";

                if (tbs448 != null)
                {
                    txtRegAtendimentoInternar.Text = tbs390.NU_REGIS;
                    txtRegAtendimentoInternar.Enabled = tbs451 == null ? true : false;
                    txtEncInternacao.Text = tbs448.NU_REGIS_INTER;
                    txtEncInternacao.Enabled = tbs451 == null ? true : false;
                    drpClassRiscoInternar.SelectedValue = tbs448.CO_PRIOR_INTER;
                    drpClassRiscoInternar.Enabled = tbs451 == null ? true : false;
                    txtIndicacaoClinica.Text = tbs448.DE_OBSER_INTER;
                    txtIndicacaoClinica.Enabled = tbs451 == null ? true : false;
                    tbs448.TBS442_TIPO_CARATReference.Load();
                    drpCaraterInternacao.SelectedValue = tbs448.TBS442_TIPO_CARAT.ID_CARAT.ToString();
                    drpCaraterInternacao.Enabled = tbs451 == null ? true : false;
                    tbs448.TBS443_TIPO_INTERReference.Load();
                    drpTipoInternacao.SelectedValue = tbs448.TBS443_TIPO_INTER.ID_TP_INTER.ToString();
                    drpTipoInternacao.Enabled = tbs451 == null ? true : false;
                    drpTipoAcomodacao.Enabled = tbs451 == null ? true : false;
                    tbs448.TBS444_TIPO_REGIMReference.Load();
                    drpRegimeInternacao.SelectedValue = tbs448.TBS444_TIPO_REGIM.ID_TP_REGIM.ToString();
                    drpRegimeInternacao.Enabled = tbs451 == null ? true : false;
                    txtDS.Text = tbs448.QT_INDIC_DIAS_INTER.ToString();
                    txtDS.Enabled = tbs451 == null ? true : false;
                    tbs448.TB14_DEPTOReference.Load();
                    drpTipoAcomodacao.SelectedValue = tbs448.TB14_DEPTO.CO_DEPTO.ToString();
                    drpTipoAcomodacao.Enabled = tbs451 == null ? true : false;
                    txtDataProvavelAH.Text = tbs448.DT_PREVI_INTER.Value.ToShortDateString();
                    txtDataProvavelAH.Enabled = tbs451 == null ? true : false;
                    tbs448.TBS446_TIPO_DOENCAReference.Load();
                    drpTipoDoenca.SelectedValue = tbs448.TBS446_TIPO_DOENCA.ID_TP_DOENCA.ToString();
                    drpTipoDoenca.Enabled = tbs451 == null ? true : false;
                    txtTDRP.Text = tbs448.QT_TEMPO_DOENC_PACIE.ToString();
                    txtTDRP.Enabled = tbs451 == null ? true : false;
                    drpTDRP.SelectedValue = tbs448.TP_TEMPO_DOENC_PACIE;
                    drpTDRP.Enabled = tbs451 == null ? true : false;
                    tbs448.TBS445_TIPO_INDIC_ACIDEReference.Load();
                    drpIndicacaoAcidente.SelectedValue = tbs448.TBS445_TIPO_INDIC_ACIDE.ID_TP_ACIDE.ToString();
                    drpIndicacaoAcidente.Enabled = tbs451 == null ? true : false;

                    var tbs449 = TBS449_ATEND_INTER_CID.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == tbs448.ID_ATEND_INTER).ToList();
                    var tbs438 = TBS438_ITENS_ATENDIMENTO.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend && x.TP_ITEM.Equals("C")).ToList();
                    List<CIDInternar> itensCID = new List<CIDInternar>();
                    foreach (var row in tbs438)
                    {
                        CIDInternar cid = new CIDInternar();
                        cid.idItem = row.ID_ITEM;
                        row.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                        cid.idCID = row.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID;
                        string isCIDPrincipal = "";

                        foreach (var item in tbs449)
                        {
                            item.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                            if (item.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid.idCID)
                            {
                                isCIDPrincipal = (TBS449_ATEND_INTER_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid.idCID)).FirstOrDefault().IS_CID_PRINC;
                            }
                        }
                        cid.isPrincipal = isCIDPrincipal.Equals("S") ? true : false;
                        cid.coCID = row.TB117_CODIGO_INTERNACIONAL_DOENCA.CO_CID;
                        int protocolo = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid.idCID).Count();
                        cid.existeProtocolo = protocolo > 0 ? true : false;
                        itensCID.Add(cid);
                    }
                    grdCIDInternar.DataSource = itensCID;
                    grdCIDInternar.Enabled = tbs451 == null ? true : false;
                    grdCIDInternar.DataBind();

                    var tbs450 = TBS450_ATEND_INTER_PROCE_MEDIC.RetornaTodosRegistros().Where(x => x.TBS448_ATEND_INTER_HOSPI.ID_ATEND_INTER == tbs448.ID_ATEND_INTER).ToList();
                    List<ProcedimentoInternar> procInternar = new List<ProcedimentoInternar>();
                    List<OPMInternar> opmInternar = new List<OPMInternar>();

                    foreach (var item in tbs450)
                    {
                        if (item.CO_TIPO_REGIS_PROCE.Equals("IN"))
                        {
                            ProcedimentoInternar proc = new ProcedimentoInternar();
                            proc.IdProcedimento = item.ID_ATEND_INTER_PROCE;
                            item.TBS356_PROC_MEDIC_PROCEReference.Load();
                            item.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUPReference.Load();
                            item.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPOReference.Load();
                            proc.IdProcedimento = item.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE;
                            proc.TipoProcedimento = item.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_AREA_GRUPO + " - " + item.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP;
                            proc.NomeProcedimento = item.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI;
                            proc.CodigoProcedimento = item.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI;
                            proc.QuantidadeProcedimento = int.Parse(item.QT_PROCE_INTER.ToString());
                            proc.VlUnitarioProcedimento = item.VL_UNITA_PROCE_INTER;
                            proc.VlTotalProcedimento = item.VL_TOTAL_PROCE_INTER;
                            procInternar.Add(proc);
                        }
                        else
                        {
                            OPMInternar opm = new OPMInternar();
                            opm.IdOPM = item.ID_ATEND_INTER_PROCE;
                            item.TBS356_PROC_MEDIC_PROCEReference.Load();
                            item.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUPReference.Load();
                            item.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPOReference.Load();
                            opm.IdOPM = item.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE;
                            opm.TipoOPM = item.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_AREA_GRUPO + " - " + item.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP;
                            opm.NomeOPM = item.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI;
                            opm.CodigoOPM = item.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI;
                            opm.QuantidadeOPM = int.Parse(item.QT_PROCE_INTER.ToString());
                            opm.VlUnitarioOPM = item.VL_UNITA_PROCE_INTER;
                            opm.VlTotalOPM = item.VL_TOTAL_PROCE_INTER;
                            opm.FabricanteOPM = item.FABRI_OPM;
                            opmInternar.Add(opm);
                        }
                    }

                    grdProcedimentoInternar.DataSource = procInternar;
                    grdProcedimentoInternar.Enabled = tbs451 == null ? true : false;
                    grdProcedimentoInternar.DataBind();
                    grdOPMInternar.DataSource = opmInternar;
                    grdOPMInternar.Enabled = tbs451 == null ? true : false;
                    grdOPMInternar.DataBind();
                    btnSalvarInternar.Enabled = tbs451 == null ? true : false;
                }
                else
                {
                    txtDataProvavelAH.Text = DateTime.Now.ToShortDateString();

                    List<CIDInternar> itensCID = new List<CIDInternar>();
                    var tbs438 = TBS438_ITENS_ATENDIMENTO.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend && x.TP_ITEM.Equals("C")).ToList();
                    foreach (var row in tbs438)
                    {
                        CIDInternar cid = new CIDInternar();
                        cid.idItem = row.ID_ITEM;
                        row.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                        cid.idCID = row.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID;
                        string isCIDPrincipal = "";
                        cid.isPrincipal = isCIDPrincipal.Equals("S") ? true : false;
                        cid.coCID = row.TB117_CODIGO_INTERNACIONAL_DOENCA.CO_CID;
                        int protocolo = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid.idCID).Count();
                        cid.existeProtocolo = protocolo > 0 ? true : false;
                        itensCID.Add(cid);
                    }
                    grdCIDInternar.DataSource = itensCID.DistinctBy(x => x.idCID);
                    grdCIDInternar.DataBind();
                }
                AbreModalPadrao("AbreModalInternar();");
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento foi encontrado. Por favor selecione um atendimento ou salve o registro em curso.");
                return;
            }
        }
        protected void lnkbSalvarObserv_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAgenda.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda!");
                grdPacientes.Focus();
                return;
            }

            hidTxtObserv.Value = txtObservacoes.Text;
        }

        protected void lnkbImprimirObserv_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAgenda.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda para definir o paciente!");
                grdPacientes.Focus();
                return;
            }

            var coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU;

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptLaudo rpt = new RptLaudo();
            var lRetorno = rpt.InitReport("OBSERVAÇÕES DO ATENDIMETNO", infos, LoginAuxili.CO_EMP, coAlu.Value, txtObservacoes.Text, DateTime.Now, LoginAuxili.CO_COL);

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        #endregion

        #region Ficha de Atendimento

        protected void lnkFicha_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAgenda.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente através da agenda!");
                grdPacientes.Focus();
                return;
            }

           // txtQxsFicha.Text = txtQueixa.Text;
            txtAnamneseFicha.Text = txtHDA.Text;


            AbreModalPadrao("AbreModalFichaAtendimento();");
        }

        protected void lnkbImprimirFicha_Click(object sender, EventArgs e)
        {
            var co_alu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;

            string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaAtend2 rpt = new RptFichaAtend2();
            var retorno = rpt.InitReport("FICHA DE ATENDIMENTO", infos, LoginAuxili.CO_EMP, co_alu, 0, txtObsFicha.Text, txtQxsFicha.Text, txtAnamneseFicha.Text, txtDiagnosticoFicha.Text, txtExameFicha.Text);

            GerarRelatorioPadrão(rpt, retorno);
        }

        #endregion

        #region Atestado

        protected void BtnAtestado_Click(object sender, EventArgs e)
        {
            var data = DateTime.Now;

            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;

            txtDtAtestado.Text = data.ToShortDateString();
            chkAtestado.Checked = chkCid.Checked = true;
            chkComparecimento.Checked = false;

            AtivarDesativarAtestado(chkAtestado.Checked);
            CarregarPacientesDisponiveisAtestado();

            AbreModalPadrao("AbreModalAtestado();");
        }

        protected void rbtPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdoAtual = (RadioButton)sender;

            foreach (GridViewRow li in grdPacAtestado.Rows)
            {
                RadioButton rdo = (((RadioButton)li.FindControl("rbtPaciente")));

                if (rdo.ClientID != rdoAtual.ClientID)
                    rdo.Checked = false;
            }

            AbreModalPadrao("AbreModalAtestado();");
        }

        protected void txtDtAtestado_OnTextChanged(object sender, EventArgs e)
        {
            CarregarPacientesDisponiveisAtestado();
        }

        protected void chkAtestado_OnCheckedChanged(object sender, EventArgs e)
        {
            AtivarDesativarAtestado(chkAtestado.Checked);
            AbreModalPadrao("AbreModalAtestado();");
        }

        protected void chkComparecimento_OnCheckedChanged(object sender, EventArgs e)
        {
            AtivarDesativarAtestado(chkAtestado.Checked);
            AbreModalPadrao("AbreModalAtestado();");
        }

        private void AtivarDesativarAtestado(bool ativar)
        {
            txtQtdDias.Enabled =
            txtCid.Enabled =
            chkCid.Enabled = ativar;

            drpPrdComparecimento.Enabled = !ativar;
        }

        protected void lnkbGerarAtestado_Click(object sender, EventArgs e)
        {
            if (grdPacAtestado.Rows.Count == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem pacientes para a emissão neste período!");
                return;
            }

            if (!chkAtestado.Checked && !chkComparecimento.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessario selecionar um tipo de documento para a emissão!");
                return;
            }

            int ck = 0;


            TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);

            foreach (GridViewRow li in grdPacAtestado.Rows)
            {
                RadioButton rdo = (((RadioButton)li.FindControl("rbtPaciente")));

                if (rdo.Checked)
                {
                    var nmPac = ((HiddenField)li.FindControl("hidNmPac")).Value;

                    if (tbs390 != null)
                    {
                        if (chkAtestado.Checked)
                        {
                            try
                            {
                                var rgPac = ((HiddenField)li.FindControl("hidRgPac")).Value;
                                var hora = ((HiddenField)li.FindControl("hidHora")).Value;

                                var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                                RptAtestadoMedico2 fpcb = new RptAtestadoMedico2();

                                TBS333_ATEST_MEDIC_PACIE tbs333 = new TBS333_ATEST_MEDIC_PACIE();
                                tbs333.ID_DOCUM = 0;

                                HiddenField paciente = (HiddenField)li.Cells[0].FindControl("hidCoALuAtes");
                                HiddenField coResp = (HiddenField)li.Cells[0].FindControl("hidCoRespAtest");

                                tbs333.IDE_CID = !string.IsNullOrEmpty(txtCid.Text) ? int.Parse(txtCid.Text) : 0;
                                tbs333.ID_ATEND_MEDIC = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : tbs390.ID_ATEND_AGEND;
                                tbs333.CO_ALU = int.Parse(paciente.Value);
                                tbs333.QT_DIAS = int.Parse(txtQtdDias.Text);
                                tbs333.DT_ATEST_MEDIC = DateTime.Parse(txtDtAtestado.Text);
                                tbs333.DT_CADAS = DateTime.Now;
                                tbs333.CO_EMP_MEDIC = LoginAuxili.CO_EMP;
                                tbs333.CO_COL_MEDIC = LoginAuxili.CO_COL;
                                tbs333.CO_EMP = LoginAuxili.CO_EMP;
                                tbs333.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(coResp.Value));

                                #region Sequencial NR Registro

                                string coUnid = LoginAuxili.CO_UNID.ToString();
                                int coEmp = LoginAuxili.CO_EMP;
                                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                                var res = (from tbs333pesq in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                                           where tbs333pesq.CO_EMP == coEmp && tbs333pesq.NU_REGIS_ATEST_MEDIC != null
                                           select new { tbs333pesq.NU_REGIS_ATEST_MEDIC }).OrderByDescending(w => w.NU_REGIS_ATEST_MEDIC).FirstOrDefault();

                                string seq;
                                int seq2;
                                int seqConcat;
                                string seqcon;
                                if (res == null)
                                {
                                    seq2 = 1;
                                }
                                else
                                {
                                    seq = res.NU_REGIS_ATEST_MEDIC.Substring(7, 6);
                                    seq2 = int.Parse(seq);
                                }

                                seqConcat = seq2 + 1;
                                seqcon = seqConcat.ToString().PadLeft(6, '0');

                                tbs333.NU_REGIS_ATEST_MEDIC = "AT" + ano + coUnid.PadLeft(3, '0') + seqcon;


                                var lRetorno = fpcb.InitReport("Atestado Médico", infos, LoginAuxili.CO_EMP, nmPac, txtQtdDias.Text, chkCid.Checked, txtCid.Text, rgPac, txtDtAtestado.Text, hora, LoginAuxili.CO_COL);

                                if (lRetorno > 0)
                                {
                                    TBS333_ATEST_MEDIC_PACIE.SaveOrUpdate(tbs333, true);
                                }

                                GerarRelatorioPadrão(fpcb, lRetorno);
                            }
                            catch (Exception ex)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                                return;
                            }
                        }
                        else
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, salve o atendimento.");
                            return;
                        }
                    }

                    if (chkComparecimento.Checked)
                    {
                        var nmResp = ((HiddenField)li.FindControl("hidNmResp")).Value;

                        var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                        RptDclComparecimento fpcb = new RptDclComparecimento();
                        var lRetorno = fpcb.InitReport("Declaração de Comparecimento", infos, LoginAuxili.CO_EMP, nmPac, nmResp, this.drpPrdComparecimento.SelectedItem.Text, txtDtAtestado.Text, LoginAuxili.CO_COL);

                        GerarRelatorioPadrão(fpcb, lRetorno);
                    }

                    ck++;
                }
            }

            if (ck == 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser selecionado pelo menos um paciente!");
        }
                                #endregion
        #endregion

        #region Guia

        protected void BtnGuia_OnClick(object sender, EventArgs e)
        {
            var data = DateTime.Now;
            var res = (from modelo in TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().Where(x => x.FL_TIPO_MODELO == "G")
                       select new { modelo.ID_MODEL_MEDIC, modelo.NO_MODEL_MEDIC }).OrderBy(w => w.ID_MODEL_MEDIC).OrderBy(x => x.NO_MODEL_MEDIC).ToList();

            if (res.Count > 0)
            {
                DropDownListModeloGuia.DataValueField = "ID_MODEL_MEDIC";
                DropDownListModeloGuia.DataTextField = "NO_MODEL_MEDIC";
                DropDownListModeloGuia.DataSource = res;
                DropDownListModeloGuia.DataBind();
                DropDownListModeloGuia.Items.Insert(0, new ListItem("Selecione", ""));
            }
            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;
            RadioButtonGuiaInterno.Checked = !DropDownListModeloGuia.Enabled;
            RadioButtonGuiaLivre.Checked = DropDownListModeloGuia.Enabled;
            txtDtGuia.Text = data.ToShortDateString();
            txtObsGuia.Text = "";
            txtObsGuia.Attributes.Add("MaxLength", "180");
            drpOperGuia.Items.Clear();
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(drpOperGuia, false, false, false, true, false);
            drpOperGuia.Items.Insert(0, new ListItem("PADRÃO", "0"));

            CarregarPacientesGuia();
        }

        protected void txtDtGuia_OnTextChanged(object sender, EventArgs e)
        {
            CarregarPacientesGuia();
        }

        protected void lnkbImprimirGuia_OnClick(object sender, EventArgs e)
        {
            int paciente = int.Parse(drpPacienteGuia.SelectedValue);

            RptGuiaAtend rpt = new RptGuiaAtend();
            var lRetorno = rpt.InitReport(paciente, txtObsGuia.Text, drpOperGuia.SelectedValue);

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        #endregion

        #region Laudo

        protected void BtnLaudo_Click(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaPacientes(drpPacienteLaudo, LoginAuxili.CO_EMP, false, true);
            txtDtLaudo.Text = DateTime.Now.ToShortDateString();
            txtObsLaudo.Text = hidIdLaudo.Value = "";
            txtTituloLaudo.Text = "LAUDO TÉCNICO";
            AbreModalPadrao("AbreModalLaudo();");
        }

        protected void drpPacienteLaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pac = int.Parse(drpPacienteLaudo.SelectedValue);

            var tbs403 = TBS403_LAUDOS.RetornaTodosRegistros().Where(l => l.TB07_ALUNO.CO_ALU == pac).ToList().LastOrDefault();

            hidIdLaudo.Value = tbs403 != null ? tbs403.ID_LAUDO.ToString() : "";

            if (tbs403 == null)
                tbs403 = TBS403_LAUDOS.RetornaTodosRegistros().ToList().LastOrDefault();

            if (tbs403 != null)
            {
                txtTituloLaudo.Text = tbs403.DE_TITULO;
                txtDtLaudo.Text = tbs403.DT_LAUDO.ToShortDateString();
                txtObsLaudo.Text = tbs403.DE_LAUDO;
            }

            AbreModalPadrao("AbreModalLaudo();");
        }

        private void SalvarLaudo(TBS403_LAUDOS tbs403)
        {
            tbs403.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(drpPacienteLaudo.SelectedValue));
            tbs403.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tbs403.DT_EMISSAO = DateTime.Now;
            tbs403.DT_LAUDO = !String.IsNullOrEmpty(txtDtLaudo.Text) ? DateTime.Parse(txtDtLaudo.Text) : DateTime.Now;
            tbs403.DE_TITULO = !String.IsNullOrEmpty(txtTituloLaudo.Text) ? txtTituloLaudo.Text : "LAUDO TÉCNICO";
            tbs403.DE_LAUDO = txtObsLaudo.Text;

            TBS403_LAUDOS.SaveOrUpdate(tbs403);
        }

        protected void lnkbImprimirLaudo_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(drpPacienteLaudo.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para realizar a emissão do laudo");
                AbreModalPadrao("AbreModalLaudo();");
                return;
            }

            #region Salvar Laudo

            var tbs403 = new TBS403_LAUDOS();

            if (!String.IsNullOrEmpty(hidIdLaudo.Value))
            {
                tbs403 = TBS403_LAUDOS.RetornaPelaChavePrimaria(int.Parse(hidIdLaudo.Value));

                //Caso tenha alterado algum dado do laudo atual ele salva como um novo laudo
                //caso contrario só carrega as entidades para emitir o relatório
                if (tbs403 != null && (tbs403.DT_LAUDO != DateTime.Parse(txtDtLaudo.Text) || tbs403.DE_LAUDO != txtObsLaudo.Text || tbs403.DE_TITULO != txtTituloLaudo.Text))
                {
                    tbs403 = new TBS403_LAUDOS();
                    SalvarLaudo(tbs403);
                }
                else
                {
                    tbs403.TB07_ALUNOReference.Load();
                    tbs403.TB03_COLABORReference.Load();
                }
            }
            else
                SalvarLaudo(tbs403);

            #endregion

            RptLaudo rpt = new RptLaudo();
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            var lRetorno = rpt.InitReport(tbs403.DE_TITULO, infos, LoginAuxili.CO_EMP, tbs403.TB07_ALUNO.CO_ALU, tbs403.DE_LAUDO, tbs403.DT_LAUDO, tbs403.TB03_COLABOR.CO_COL);

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        #endregion

        #region Prontuário Convencional

        private void CarregarModalProntuCon(int coAlu, int qualifPront, DateTime? ini, DateTime? fim)
        {
            try
            {
                var tbs400s = TBS400_PRONT_MASTER.RetornaTodosRegistros()
                        .Where(x => (x.CO_ALU == coAlu)).ToList();
                var tbs400 = TBS400_PRONT_MASTER.RetornaTodosRegistros()
                        .Where(x => (x.CO_ALU == coAlu)
                               && (qualifPront > 0 ? x.TBS418_CLASS_PRONT.ID_CLASS_PRONT == qualifPront : true)
                               && (ini.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) >= EntityFunctions.TruncateTime(ini.Value) : true)
                               && (fim.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) <= EntityFunctions.TruncateTime(fim.Value) : true)
                               ).ToList();

                string texto = "";

                if (tbs400 != null)
                {
                    foreach (var it in tbs400.OrderByDescending(x => x.DT_CADAS.Date).GroupBy(x => x.DT_CADAS.Date).Select(grp => grp.First()).ToList())
                    {
                        it.TB14_DEPTOReference.Load();
                        it.TBS418_CLASS_PRONTReference.Load();
                        string qual = it.TBS418_CLASS_PRONT != null ? it.TBS418_CLASS_PRONT.NO_CLASS_PRONT : "Sem Qualificação";
                        var tb03 = TB03_COLABOR.RetornaPeloCoCol((it.CO_COL.HasValue ? it.CO_COL.Value : it.CO_COL_CADAS));
                        texto += "<b style='color:blue; font-weight: 600;'>" + it.DT_CADAS.ToString("dd/MM/yyyy") + "  -  " + tb03.NO_APEL_COL + "  " + tb03.CO_SIGLA_ENTID_PROFI + " " + tb03.NU_ENTID_PROFI + "/" + tb03.CO_UF_ENTID_PROFI + " - " + qual + " " + (it.TB14_DEPTO != null ? "  -  " + it.TB14_DEPTO.CO_SIGLA_DEPTO : "") + "</b>" + "</br>" + "<p></br>";
                        foreach (var it2 in tbs400.OrderByDescending(x => x.DT_CADAS).Where(x => x.DT_CADAS.Date == it.DT_CADAS.Date))
                        {
                            texto += "" + it2.DT_CADAS.ToString("HH:mm") + "  -  " + it2.ANAMNSE.Replace("<BR>", "</p>") + "</p></br>";
                        }
                }
                }
                divObsProntuCon.InnerHtml = texto;
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        private void carregarQualificacaoProntuario(DropDownList ddl)
        {

            var padrao = RetornaProntuarioPadrao();
            var res = TBS418_CLASS_PRONT.RetornaTodosRegistros()
                        .Select(x => new
                        {
                            x.NO_CLASS_PRONT,
                            x.ID_CLASS_PRONT
                        });
            ddl.DataSource = res;
            ddl.DataTextField = "NO_CLASS_PRONT";
            ddl.DataValueField = "ID_CLASS_PRONT";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Todos", ""));
            ddl.SelectedValue = padrao.ID_CLASS_PRONT.ToString();
        }

        private TBS418_CLASS_PRONT RetornaProntuarioPadrao()
        {
            var padrao = new TBS418_CLASS_PRONT();
            var lista = TBS418_CLASS_PRONT.RetornaTodosRegistros().Where(x => x.NO_CLASS_PRONT == "Prontuário Padrão");
            if (lista.Any())
            {
                padrao = lista.FirstOrDefault();
            }
            else
            {
                padrao.NO_CLASS_PRONT = "Prontuário Padrão";
                padrao.CO_SITUA = "A";
                padrao.DE_CLASS_PRONT = "Prontuário Padrão";
                var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                //Dados de quem cadastrou o atendimento
                padrao.DT_CADAS = DateTime.Now;
                padrao.CO_COL_CADAS = LoginAuxili.CO_COL;
                padrao.CO_EMP_COL_CADAS = emp_col;
                padrao.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                padrao.IP_CADAS = Request.UserHostAddress;
                //Dados da situação do atendiment
                padrao.DT_SITUA = DateTime.Now;
                padrao.CO_COL_SITUA = LoginAuxili.CO_COL;
                padrao.CO_EMP_COL_SITUA = emp_col;
                padrao.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                padrao.IP_SITUA = Request.UserHostAddress;
                TBS418_CLASS_PRONT.SaveOrUpdate(padrao);
            }
            return padrao;
        }

        protected void AbrirProntuarioPadrao()
        {
            try
            {
                TextBoxConfirDataPront.Visible = false;
                TextBoxConfirHoraPront.Visible = false;
                labelCadObsProntuCon.Visible = false;
                txtCadObsProntuCon.Visible = false;
                descricaoTextBox.Visible = false;
                Label9.Visible = false;
                liConfirmarBotao.Visible = false;
                labelConfirmHora.Visible = false;
                //divObsProntuCon.Style.Clear();
                
                divObsProntuCon.Style.Add("font-size", "15px");
                divObsProntuCon.Style.Add("width", "707px");
                divObsProntuCon.Style.Add("height", "322px");

                divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
                if (!string.IsNullOrEmpty(hidIdAgenda.Value))
                {
                    var coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;
                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    txtNumPront.Text = tb07.NU_NIRE.toNire();
                    txtNumPasta.Text = tb07.DE_PASTA_CONTR;

                    txtPacienteProntuCon.Text = tb07.NO_ALU;
                    if (!string.IsNullOrEmpty(txtPacienteProntuCon.Text))
                    {
                        BuscaPaciente();
                        drpPacienteProntuCon.SelectedValue = tb07.CO_ALU.ToString();
                        var prontuarioMaisAntigo = TBS400_PRONT_MASTER.RetornaTodosRegistros().Where(x => x.CO_ALU == tb07.CO_ALU).OrderBy(x => x.DT_CADAS).FirstOrDefault();
                        if(prontuarioMaisAntigo != null)
                            txtIniPront.Text = prontuarioMaisAntigo.DT_CADAS.ToShortDateString();
                        else
                            txtIniPront.Text = DateTime.Now.ToShortDateString();
                    }
                }
                else
                {
                    OcultarPesquisa(false);
                    txtIniPront.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                    //CarregarModalProntuCon();
                    divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
                    drpPacienteProntuCon.SelectedValue = "";
                }

                carregarQualificacaoProntuario(ddlQualifPront);

                txtPacienteProntuCon.Text = "";
                // drpPacienteProntuCon.DataSource = null;
                ddlQualifPront.SelectedValue = "";
                //drpPacienteProntuCon.DataBind();
                //ddlQualifPront.SelectedValue = "";

                txtFimPront.Text = DateTime.Now.ToShortDateString();

                //AbreModalPadrao("AbreModalProntuCon();");
                if (!string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                    CarregarModalProntuCon(int.Parse(drpPacienteProntuCon.SelectedValue), 0, DateTime.Parse(txtIniPront.Text), DateTime.Parse(txtFimPront.Text));
                AbreModalPadrao("AbreModalProntuCon();");
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        protected void BtnProntuCon_Click(object sender, EventArgs e)
        {
            try
            {
                TextBoxConfirDataPront.Visible = true;
                TextBoxConfirHoraPront.Visible = true;
                labelCadObsProntuCon.Visible = true;
                txtCadObsProntuCon.Visible = true;
                descricaoTextBox.Visible = true;
                labelConfirmHora.Visible = true;
                liConfirmarBotao.Visible = true;
                Label9.Visible = true;

                divObsProntuCon.Style.Add("font-size", "12px");
                divObsProntuCon.Style.Add("width", "707px");
                divObsProntuCon.Style.Add("height", "182px");

                TextBoxConfirDataPront.Text = DateTime.Now.ToString("dd/mm/yyyy");
                TextBoxConfirHoraPront.Text = DateTime.Now.ToString("HH:mm");
                divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
                if (!string.IsNullOrEmpty(hidIdAgenda.Value))
                {
                    var coAlu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;
                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    txtNumPront.Text = tb07.NU_NIRE.toNire();
                    txtNumPasta.Text = tb07.DE_PASTA_CONTR;
                   
                    txtPacienteProntuCon.Text = tb07.NO_ALU;
                    if (!string.IsNullOrEmpty(txtPacienteProntuCon.Text))
                    {
                        BuscaPaciente();
                        drpPacienteProntuCon.SelectedValue = tb07.CO_ALU.ToString();
                        var agenda = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.CO_ALU == tb07.CO_ALU).OrderBy(x => x.DT_AGEND_HORAR).First();
                        txtIniPront.Text = agenda.DT_AGEND_HORAR.ToShortDateString();
                    }
                }
                else
                {
                    OcultarPesquisa(false);
                    txtIniPront.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                    //CarregarModalProntuCon();
                    divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
                    drpPacienteProntuCon.SelectedValue = "";
                }

                carregarQualificacaoProntuario(ddlQualifPront);
              
                txtPacienteProntuCon.Text = "";
               // drpPacienteProntuCon.DataSource = null;
                ddlQualifPront.SelectedValue = "";
                //drpPacienteProntuCon.DataBind();
                //ddlQualifPront.SelectedValue = "";
              
                txtFimPront.Text = DateTime.Now.ToShortDateString();
              
                //AbreModalPadrao("AbreModalProntuCon();");
                if (!string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                   CarregarModalProntuCon(int.Parse(drpPacienteProntuCon.SelectedValue), 0, DateTime.Parse(txtIniPront.Text), DateTime.Parse(txtFimPront.Text));
                AbreModalPadrao("AbreModalProntuCon();");
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            BuscaPaciente();
        }

        private void BuscaPaciente()
        {
            if (txtPacienteProntuCon.Text.Length < 3)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Digite pelo menos 3 letras para consultar o paciente.");
                txtPacienteProntuCon.Focus();
               
                return;
            }

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtPacienteProntuCon.Text)
                             && tb07.CO_SITU_ALU != "H" && tb07.CO_SITU_ALU != "O"
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                drpPacienteProntuCon.DataTextField = "NO_ALU";
                drpPacienteProntuCon.DataValueField = "CO_ALU";
                drpPacienteProntuCon.DataSource = res;
                drpPacienteProntuCon.DataBind();
            }

            drpPacienteProntuCon.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisa(true);

            AbreModalPadrao("AbreModalProntuCon();");
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);

            //CarregarModalProntuCon();
            divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";

            AbreModalPadrao("AbreModalProntuCon();");
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtPacienteProntuCon.Visible =
            imgbPesqPacienteProntuCon.Visible = !ocultar;
            drpPacienteProntuCon.Visible =
            imgbVoltPacienteProntuCon.Visible = ocultar;
        }

        protected void drpPacienteProntuCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumPasta.Text = "";
            txtNumPront.Text = "";
            //ddlQualifPront.SelectedValue = "";
          
            if (!string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
            {
                int idPaciente = int.Parse(drpPacienteProntuCon.SelectedValue);
                var paciente = TB07_ALUNO.RetornaPeloCoAlu(idPaciente);
                var agendas =  TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.CO_ALU == idPaciente).OrderBy(x => x.DT_AGEND_HORAR);
                var agenda = agendas.Any() ?agendas.First(): null;
                txtIniPront.Text = agenda != null ? agenda.DT_AGEND_HORAR.ToShortDateString() : DateTime.Now.AddDays(-15).ToShortDateString();
                txtNumPront.Text = paciente.NU_NIRE.toNire();
                txtNumPasta.Text = paciente.DE_PASTA_CONTR;
                int idQualifProntuario = string.IsNullOrEmpty(ddlQualifPront.SelectedValue) ? 0: int.Parse(ddlQualifPront.SelectedValue);
                CarregarModalProntuCon(int.Parse(drpPacienteProntuCon.SelectedValue), idQualifProntuario, DateTime.Parse(txtIniPront.Text), DateTime.Parse(txtFimPront.Text));
            }
            AbreModalPadrao("AbreModalProntuCon();");
        }

        //protected void chkNumPront_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkNumPront.Checked)
        //    {
        //        txtNumPront.Enabled = true;
        //        txtNumPront.Text = null;
        //        chkNumPasta.Checked = false;
        //        chkNumPasta.Enabled = false;
        //        chkNumPasta.Text = null;
        //        txtNumPasta.Enabled = false;
        //        txtNumPasta.Text = null;
        //    }
        //    else
        //    {
        //        chkNumPasta.Checked = false;
        //        txtNumPront.Text = null;
        //        chkNumPasta.Enabled = true;
        //        chkNumPasta.Text = null;
        //        txtNumPasta.Enabled = true;
        //        txtNumPasta.Text = null;
        //    }
        //    AbreModalPadrao("AbreModalProntuCon();");
        //}

        protected void imgBtnPesqPront_OnClick(object sender, EventArgs e)
        {
            try
            {
                string pasta = txtNumPasta.Text;
                int nire = !string.IsNullOrEmpty(txtNumPront.Text) ? int.Parse(txtNumPront.Text) : 0;
                int pac = !string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue) ? int.Parse(drpPacienteProntuCon.SelectedValue) : 0;
                int qual = !string.IsNullOrEmpty(ddlQualifPront.SelectedValue) ? int.Parse(ddlQualifPront.SelectedValue) : 0;
                DateTime? ini = !string.IsNullOrEmpty(txtIniPront.Text) ? DateTime.Parse(txtIniPront.Text) : (DateTime?)null;
                DateTime? fim = !string.IsNullOrEmpty(txtFimPront.Text) ? DateTime.Parse(txtFimPront.Text) : (DateTime?)null;

                var tb07 = TB07_ALUNO.RetornaTodosRegistros()
                            .Where(x => !string.IsNullOrEmpty(pasta) ? x.DE_PASTA_CONTR == pasta : false
                                   || pac > 0 ? x.CO_ALU == pac : false
                                   || nire > 0 ? x.NU_NIRE == nire : false)
                            .Select(x => new
                            {
                                x.NU_NIRE,
                                x.CO_ALU,
                                x.NO_ALU,
                                x.DE_PASTA_CONTR
                            })
                            .FirstOrDefault();

                if (tb07 != null)
                {
                    txtNumPront.Text = tb07.NU_NIRE.toNire();
                    txtNumPasta.Text = tb07.DE_PASTA_CONTR;
                    if (string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                    {
                        OcultarPesquisa(true);
                        var _tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(x => x.CO_ALU == tb07.CO_ALU).Select(x => new { x.CO_ALU, x.NO_ALU });
                        drpPacienteProntuCon.DataSource = _tb07;
                        drpPacienteProntuCon.DataTextField = "NO_ALU";
                        drpPacienteProntuCon.DataValueField = "CO_ALU";
                        drpPacienteProntuCon.DataBind();
                    }
                    CarregarModalProntuCon(tb07.CO_ALU, qual, ini, fim);
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Paciente não encontrado!");
                    AbreModalPadrao("AbreModalProntuCon();");
                }

                AbreModalPadrao("AbreModalProntuCon();");
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
                AbreModalPadrao("AbreModalProntuCon();");
            }
        }

        //protected void chkNumPasta_CheckedChanged(object sender, EventArgs e) 
        //{
        //    if (chkNumPasta.Checked)
        //    {
        //        chkNumPront.Checked = false;
        //        chkNumPront.Enabled = false;
        //        txtNumPront.Enabled = false;
        //        txtNumPront.Text = null;
        //        txtNumPasta.Enabled = true;
        //        txtNumPasta.Text = null;
        //    }
        //    else
        //    {
        //        chkNumPront.Enabled = true;
        //        txtNumPront.Text = null;
        //        chkNumPasta.Enabled = true;
        //        chkNumPasta.Text = null;
        //        txtNumPasta.Enabled = true;
        //        txtNumPasta.Text = null;
        //    }
        //    AbreModalPadrao("AbreModalProntuCon();");
        //}

        private void SalvarProntuCon(TBS400_PRONT_MASTER tbs400)
        {
            try
            {
                if (String.IsNullOrEmpty(txtCadObsProntuCon.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário inserir uma anamnese.");
                    txtCadObsProntuCon.Focus();
                    return;
                    AbreModalPadrao("AbreModalProntuCon();");
                }
                if (String.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para salvar o prontuário");
                    drpPacienteProntuCon.Focus();
                    return;
                    AbreModalPadrao("AbreModalProntuCon();");
                }

                tbs400.ANAMNSE = txtCadObsProntuCon.Text;
                tbs400.CO_ALU = int.Parse(drpPacienteProntuCon.SelectedValue);
                tbs400.CO_COL = LoginAuxili.CO_COL;
                tbs400.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs400.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs400.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs400.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                tbs400.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
                tbs400.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs400.CO_SITUA = "A";
                tbs400.DT_CADAS = tbs400.DT_CADAS == null ? DateTime.Now : tbs400.DT_CADAS;
                tbs400.DT_SITUA = DateTime.Now;
                tbs400.IP_CADAS = Request.UserHostAddress;
                tbs400.IP_SITUA = Request.UserHostAddress;
                tbs400.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(LoginAuxili.CO_DEPTO);
                tbs400.TBS418_CLASS_PRONT = !string.IsNullOrEmpty(ddlQualifPront.Text) ? TBS418_CLASS_PRONT.RetornaPelaChavePrimaria(int.Parse(ddlQualifPront.Text)) : RetornaProntuarioPadrao();

                var ultimoElemento = TBS400_PRONT_MASTER.RetornaTodosRegistros().ToList().OrderByDescending(x => x.ID_PRONT_MASTER).FirstOrDefault();
                string nuRegis = (DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ultimoElemento.ID_PRONT_MASTER + 1).ToString();
                tbs400.NU_REGIS = nuRegis;
                tbs400.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tbs400.TBS390_ATEND_AGEND = String.IsNullOrEmpty(hidIdAtendimento.Value) ? null : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                tbs400.TBS401_PRONT_INTENS = null;

                TBS400_PRONT_MASTER.SaveOrUpdate(tbs400);

                //CarregarModalProntuCon();

                AbreModalPadrao("AbreModalProntuCon();");
            }
            catch { }
        }

        protected void imgBRel_OnClick(object sender, EventArgs e)
        {
            try
            {
                string pasta = txtNumPasta.Text;
                int nire = !string.IsNullOrEmpty(txtNumPront.Text) ? int.Parse(txtNumPront.Text) : 0;
                int pac = !string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue) ? int.Parse(drpPacienteProntuCon.SelectedValue) : 0;
                int qual = !string.IsNullOrEmpty(ddlQualifPront.SelectedValue) ? int.Parse(ddlQualifPront.SelectedValue) : 0;
                DateTime? ini = !string.IsNullOrEmpty(txtIniPront.Text) ? DateTime.Parse(txtIniPront.Text) : (DateTime?)null;
                DateTime? fim = !string.IsNullOrEmpty(txtFimPront.Text) ? DateTime.Parse(txtFimPront.Text) : (DateTime?)null;

                var tb07 = TB07_ALUNO.RetornaTodosRegistros()
                            .Where(x => !string.IsNullOrEmpty(pasta) ? x.DE_PASTA_CONTR == pasta : false
                                   || pac > 0 ? x.CO_ALU == pac : false
                                   || nire > 0 ? x.NU_NIRE == nire : false)
                            .Select(x => new
                            {
                                x.NU_NIRE,
                                x.CO_ALU,
                                x.NO_ALU,
                                x.DE_PASTA_CONTR
                            })
                            .FirstOrDefault();

                if (tb07 != null)
                {
                    var tbs400 = TBS400_PRONT_MASTER.RetornaTodosRegistros()
                        .Where(x => (x.CO_ALU == pac)
                               && (qual > 0 ? x.TBS418_CLASS_PRONT.ID_CLASS_PRONT == qual : 0 == 0)
                               && (ini.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) >= EntityFunctions.TruncateTime(ini.Value) : 0 == 0)
                               && (fim.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) <= EntityFunctions.TruncateTime(fim.Value) : 0 == 0)
                               ).ToList();

                    var idPront = new List<int>();

                    if (tbs400 != null)
                    {
                        foreach (var it in tbs400)
                        {
                            idPront.Add(it.ID_PRONT_MASTER);
                        }
                    }
                    txtNumPront.Text = tb07.NU_NIRE.toNire();
                    txtNumPasta.Text = tb07.DE_PASTA_CONTR;
                    RptProntConvencional rpt = new RptProntConvencional();
                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    var lRetorno = rpt.InitReport(tb07.CO_ALU, idPront, infos, LoginAuxili.CO_EMP, qual, ini, fim);

                    GerarRelatorioPadrão(rpt, lRetorno);
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Paciente não encontrado!");
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        protected void lnkbImprimirProntuCon_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para salvar o prontuário");
                    AbreModalPadrao("AbreModalProntuCon();");
                    return;
                }

                #region Salvar Laudo

                var tbs400 = new TBS400_PRONT_MASTER();
                var dataP = !String.IsNullOrEmpty(TextBoxConfirDataPront.Text) ? TextBoxConfirDataPront.Text : DateTime.Now.ToShortDateString();
                var horaP = !String.IsNullOrEmpty(TextBoxConfirHoraPront.Text) ? TextBoxConfirHoraPront.Text : DateTime.Now.ToShortTimeString();
                if (!String.IsNullOrEmpty(hidIdProntuCon.Value))
                {
                    tbs400 = TBS400_PRONT_MASTER.RetornaPelaChavePrimaria(int.Parse(hidIdProntuCon.Value));

                    //Caso tenha alterado algum dado do laudo atual ele salva como um novo laudo
                    //caso contrario só carrega as entidades para emitir o relatório
                    if (tbs400 != null)
                    {

                        tbs400 = new TBS400_PRONT_MASTER();

                        tbs400.DT_CADAS = DateTime.Parse(dataP).Add(TimeSpan.Parse(horaP));
                        SalvarProntuCon(tbs400);
                    }
                }
                else
                {
                    tbs400.DT_CADAS = DateTime.Parse(dataP).Add(TimeSpan.Parse(horaP));
                    SalvarProntuCon(tbs400);
                   
                }

                CarregarModalProntuCon(int.Parse(drpPacienteProntuCon.SelectedValue), 0, DateTime.Parse(txtIniPront.Text), DateTime.Parse(txtFimPront.Text));

                #endregion

                #region Grava o atendimento

                var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

                TBS390_ATEND_AGEND tbs390 = new TBS390_ATEND_AGEND();

                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);

                if (tbs390.ID_ATEND_AGEND == 0)
                {
                    #region Trata sequencial
                    //Trata para gerar um Código do Encaminhamento
                    var res2 = (from tbs390pesq in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                select new { tbs390pesq.NU_REGIS }).OrderByDescending(w => w.NU_REGIS).FirstOrDefault();

                    string seq;
                    int seq2;
                    int seqConcat;
                    string seqcon;
                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                    string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                    if (res2 == null)
                        seq2 = 1;
                    else
                    {
                        seq = res2.NU_REGIS.Substring(6, 6);
                        seq2 = int.Parse(seq);
                    }

                    seqConcat = seq2 + 1;
                    seqcon = seqConcat.ToString().PadLeft(6, '0');

                    string CodigoAtendimento = string.Format("AT{0}{1}{2}", ano, mes, seqcon);
                    #endregion

                    tbs390.TBS174_AGEND_HORAR = tbs174;
                    tbs390.TB07_ALUNO = tb07;
                    tbs390.NU_REGIS = CodigoAtendimento;

                    //Dados de quem cadastrou o atendimento
                    tbs390.DT_CADAS = DateTime.Now;
                    tbs390.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs390.CO_EMP_COL_CADAS = emp_col;
                    tbs390.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs390.IP_CADAS = Request.UserHostAddress;
                }


                tbs390.DE_CONSI = hidTxtObserv.Value;
                //d tbs390.DE_QXA_PRINC = txtQueixa.Text;
                tbs390.DE_HDA = txtCadObsProntuCon.Text;

                txtCadObsProntuCon.Text = "";
                tbs390.FL_SITU_FATU = hidCkOrcamAprov.Value == "S" ? "A" : "N";//Aprovado ou Negado



                tbs390.NU_SENHA_ATEND = txtSenha.Text;
                tbs390.CO_TIPO_RISCO = !String.IsNullOrEmpty(ddlClassRisco.SelectedValue) ? int.Parse(ddlClassRisco.SelectedValue) : (int?)null;

                //Dados de quem realizou o atendimento
                var data = !String.IsNullOrEmpty(txtDtAtend.Text) ? txtDtAtend.Text : DateTime.Now.ToShortDateString();
                var hora = !String.IsNullOrEmpty(txtHrAtend.Text) ? txtHrAtend.Text : DateTime.Now.ToShortTimeString();
                tbs390.DT_REALI = DateTime.Parse(data).Add(TimeSpan.Parse(hora));
                tbs390.CO_COL_ATEND = int.Parse(drpProfResp.SelectedValue);
                tbs390.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue)).CO_COL;
                tbs390.DT_VALID_ORCAM = (!string.IsNullOrEmpty(hidDtValidOrcam.Value) ? DateTime.Parse(hidDtValidOrcam.Value) : (DateTime?)null);
                tbs390.VL_DSCTO_ORCAM = (!string.IsNullOrEmpty(txtVlDscto.Text) ? decimal.Parse(txtVlDscto.Text) : (decimal?)null);
                tbs390.VL_TOTAL_ORCAM = (!string.IsNullOrEmpty(txtVlTotalOrcam.Text) ? decimal.Parse(txtVlTotalOrcam.Text) : (decimal?)null);

                if (LoginAuxili.CO_DEPTO != 0)
                    tbs390.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(LoginAuxili.CO_DEPTO);

                //Dados da situação do atendimento
                tbs390.CO_SITUA = "A";
                tbs390.DT_SITUA = DateTime.Now;
                tbs390.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs390.CO_EMP_COL_SITUA = emp_col;
                tbs390.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs390.IP_SITUA = Request.UserHostAddress;

                TBS390_ATEND_AGEND.SaveOrUpdate(tbs390);
                hidIdAtendimento.Value = tbs390.ID_ATEND_AGEND.ToString();
                #endregion
                //RptLaudo rpt = new RptLaudo();
                //var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                //var lRetorno = rpt.InitReport(tbs403.DE_TITULO, infos, LoginAuxili.CO_EMP, tbs403.TB07_ALUNO.CO_ALU, tbs403.DE_LAUDO, tbs403.DT_LAUDO, tbs403.TB03_COLABOR.CO_COL);

                //GerarRelatorioPadrão(rpt, lRetorno);
            }
            catch { }
        }

        #endregion

        #region Ambulatorio

        protected void ddlServAmbu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl, ddlTipo;

            if (grdServAmbulatoriais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdServAmbulatoriais.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlServAmbulatorial");
                    ddlTipo = (DropDownList)linha.FindControl("ddlGridTipoAmbul");

                    if (ddl.ClientID == atual.ClientID)
                    {
                        TextBox txtCodigo = (TextBox)linha.FindControl("txtCodigoServAmbulatorial");
                        TextBox txtValor = (TextBox)linha.FindControl("txtValorServAmbulatorial");
                        TextBox txtComplemento = (TextBox)linha.FindControl("txtComplementoServAmbulatorial");
                        txtCodigo.Text = "";
                        txtValor.Text = "";
                        txtComplemento.Text = "";
                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                        {
                            if (ddlTipo.SelectedValue.Equals("P"))
                            {
                                var proc = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue));

                                txtCodigo.Text = proc != null ? proc.CO_PROC_MEDI : "-";

                                proc.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                                if (proc.TBS353_VALOR_PROC_MEDIC_PROCE != null && proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null)
                                    txtValor.Text = proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE.ToString();
                            }
                            else if (ddlTipo.SelectedValue.Equals("M"))
                            {
                                var med = TB90_PRODUTO.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue), LoginAuxili.CO_EMP);
                                txtCodigo.Text = med != null ? med.CO_REFE_PROD : "-";
                                txtValor.Text = med != null ? med.VL_UNIT_PROD.ToString() : "0,00";
                            }
                        }
                        else
                        {
                            txtCodigo.Text = ""; // Limpa os dois campos caso esteja desselecionando o procedimento
                            txtValor.Text = "";
                            txtComplemento.Text = "";
                            ddl.Items.Clear();
                        }

                    }
                }
            }
            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void ddlOperPlanoServAmbu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanos(ddlPlanoServAmbu, ddlOperPlanoServAmbu);

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void ddlPlanoServAmbu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaGridNovaComContextoAmbulatorio();

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void imgServAmbulatoriaisPla_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdServAmbulatoriais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdServAmbulatoriais.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgServAmbulatoriaisPla");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridServAmbulatoriais(aux);

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void ExcluiItemGridServAmbulatoriais(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridServAmbulatoriais();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_AMB"] = dtV;

            carregaGridNovaComContextoAmbulatorio();
        }

        protected void lnkAddProcPlaAmbulatorial_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridServicosAmulatoriais();

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void btnGuiaServAmbulatoriais_OnClick(object sender, EventArgs e)
        {
            int lRetorno = 0;
            if (string.IsNullOrEmpty(hidIdAtendimento.Value) || string.IsNullOrEmpty(didIdServAmbulatorial.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para realizar emissão da guia ambulatorial.");
                return;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaAmbulatorial rpt = new RptGuiaAmbulatorial();
            lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value), int.Parse(didIdServAmbulatorial.Value));
            GerarRelatorioPadrão(rpt, lRetorno);
        }

        protected void lnkAmbul_OnClick(object sender, EventArgs e)
        {
            txtObsServAmbulatoriais.Text = hidObsSerAmbulatoriais.Value;
            DropDownListModeloAmb.Enabled = PanelLivreAmbulatorio.Visible;
            RadioButtonAmbLivre.Checked = PanelLivreAmbulatorio.Visible;
            RadioButtonAmbNormal.Checked = PanelNormalAmbulatorio.Visible;
            int idAgenda = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : 0;
            DropDownListModeloAmb.SelectedValue = !string.IsNullOrEmpty(HiddenFieldModeloAmbID.Value) ? HiddenFieldModeloAmbID.Value : "";
            CarregaModelosAmb();

            if (idAgenda > 0)
            {
                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAgenda);
                tbs390.TBS174_AGEND_HORARReference.Load();
                tbs390.TBS174_AGEND_HORAR.TB250_OPERAReference.Load();
                tbs390.TBS174_AGEND_HORAR.TB251_PLANO_OPERAReference.Load();
                carregarOperadoras(ddlOperPlanoServAmbu);
                ddlOperPlanoServAmbu.SelectedValue = tbs390.TBS174_AGEND_HORAR.TB250_OPERA != null ? tbs390.TBS174_AGEND_HORAR.TB250_OPERA.ID_OPER.ToString() : "0";
                CarregarPlanos(ddlPlanoServAmbu, ddlOperPlanoServAmbu);
                ddlPlanoServAmbu.SelectedValue = tbs390.TBS174_AGEND_HORAR.TB251_PLANO_OPERA != null ? tbs390.TBS174_AGEND_HORAR.TB251_PLANO_OPERA.ID_PLAN.ToString() : "0";
                     
                foreach (GridViewRow row in grdServAmbulatoriais.Rows)
                {
                    //DropDownList ddlServAmbulatoriais = (DropDownList)row.FindControl("ddlServAmbulatorial");
                    //string txtComplemento = ((TextBox)row.FindControl("txtComplementoServAmbulatorial")).Text;
                    DropDownList drpOper = (DropDownList)row.FindControl("ddlGridContratacaoAmbul");
                    DropDownList drpPlano = (DropDownList)row.FindControl("ddlGridPlanoAmbul");
                    //TextBox txtCodigo = (TextBox)row.FindControl("txtCodigoServAmbulatorial");
                    //TextBox txtValor = (TextBox)row.FindControl("txtValorServAmbulatorial");
                    //string drpTipo = ((DropDownList)row.FindControl("ddlGridTipoAmbul")).SelectedValue;
                    //int idOper = !string.IsNullOrEmpty(drpOper.SelectedValue) ? int.Parse(drpOper.SelectedValue) : 0;
                    //int idPlan = !string.IsNullOrEmpty(drpPlano.SelectedValue) ? int.Parse(drpPlano.SelectedValue) : 0;
                    if (row.RowIndex - 1 == 0)
                    {
                        carregarOperadoras(drpOper);
                        if (!string.IsNullOrEmpty(ddlOperPlanoServAmbu.SelectedValue))
                        {
                            drpOper.SelectedValue = ddlOperPlanoServAmbu.SelectedValue;
                        }
                        CarregarPlanos(drpPlano, drpOper);
                        if (!string.IsNullOrEmpty(ddlPlanoServAmbu.SelectedValue))
                        {
                            drpPlano.SelectedValue = ddlPlanoServAmbu.SelectedValue;
                        }
                    }
                    //}
                }
                AbreModalPadrao("AbreModalAmbulatorio();");
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi selecionado nenhum atendimento.");
                return;
            }
        }

        protected void ddlGridContratacaoAmbul_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdServAmbulatoriais.Rows)
            {
                DropDownList drp = (DropDownList)row.FindControl("ddlGridContratacaoAmbul");
                DropDownList drpPlano = (DropDownList)row.FindControl("ddlGridPlanoAmbul");
                DropDownList ddlNmProced = (DropDownList)row.FindControl("ddlServAmbulatorial");
                DropDownList drpTipo = (DropDownList)row.FindControl("ddlGridTipoAmbul");
                TextBox txtCodigo = (TextBox)row.FindControl("txtCodigoServAmbulatorial");
                TextBox txtValor = (TextBox)row.FindControl("txtValorServAmbulatorial");
                TextBox txtComplemento = (TextBox)row.FindControl("txtComplementoServAmbulatorial");
                CarregarPlanos(drpPlano, drp);
                drpTipo.SelectedValue = "";
                ddlNmProced.Items.Clear();
                txtCodigo.Text = "";
                txtValor.Text = "";
                txtComplemento.Text = "";
            }
            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void ddlGridTipoAmbul_SelectedIndexChanged(object sender, EventArgs e)
        {
            int aux = 0;
            foreach (GridViewRow row in grdServAmbulatoriais.Rows)
            {
                ImageButton atual = (ImageButton)sender;
                ImageButton img;
                img = (ImageButton)row.FindControl("imgServAmbulatoriaisPla");
                if (img.ClientID == atual.ClientID)
                    aux = row.RowIndex;

                DropDownList drp = (DropDownList)row.FindControl("ddlGridContratacaoAmbul");
                DropDownList drpPlano = (DropDownList)row.FindControl("ddlGridPlanoAmbul");
                DropDownList ddlNmProced = (DropDownList)row.FindControl("ddlServAmbulatorial");
                TextBox txtCodigo = (TextBox)row.FindControl("txtCodigoServAmbulatorial");
                TextBox txtValor = (TextBox)row.FindControl("txtValorServAmbulatorial");
                TextBox txtComplemento = (TextBox)row.FindControl("txtComplementoServAmbulatorial");
                string palavra = ((TextBox)row.FindControl("txtDefServAmbulatorial")).Text;
                string drpTipo = ((DropDownList)row.FindControl("ddlGridTipoAmbul")).SelectedValue;
                int opr = !string.IsNullOrEmpty(drp.SelectedValue) ? int.Parse(drp.SelectedValue) : 0;

                ddlNmProced.Items.Clear();
                txtCodigo.Text = "";
                txtValor.Text = "";
                txtComplemento.Text = "";

                if (drpTipo.Equals("P"))
                {
                    var res = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                        .Where(x => (!string.IsNullOrEmpty(palavra) ? x.NM_PROC_MEDI.Contains(palavra) : true) && (opr != 0 ? x.TB250_OPERA.ID_OPER == opr : 0 == 0))
                               .Select(x => new
                               {
                                   ID = x.ID_PROC_MEDI_PROCE,
                                   NOME = x.NM_PROC_MEDI
                               }).OrderBy(x => x.NOME);

                    ddlNmProced.DataSource = res;
                    ddlNmProced.DataTextField = "NOME";
                    ddlNmProced.DataValueField = "ID";
                    ddlNmProced.DataBind();
                    ddlNmProced.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else if (drpTipo.Equals("M"))
                {
                    var res = TB90_PRODUTO.RetornaTodosRegistros()
                                               .Join(TB96_ESTOQUE.RetornaTodosRegistros(), a => a.CO_PROD, b => b.TB90_PRODUTO.CO_PROD, (a, b) => new { a, b })
                                               .Where(x => (!string.IsNullOrEmpty(palavra) ? x.a.NO_PROD.Contains(palavra) : true) && (x.a.TB124_TIPO_PRODUTO != null && x.a.TB124_TIPO_PRODUTO.CO_TIPO_CLASS_PROD.Equals("M"))
                                                   //&& x.a.CO_SITU_PROD.Equals("A")   
                                                   && x.b.QT_SALDO_EST > 0)
                                               .Select(x => new
                                               {
                                                   x.a.CO_PROD,
                                                   NOME = x.a.TBS457_CLASS_TERAP.DE_CLASS_TERAP != null ? (x.a.NO_PROD_RED + " - " + x.a.TBS457_CLASS_TERAP.DE_CLASS_TERAP) : x.a.NO_PROD_RED
                                               }).OrderBy(x => x.NOME).Take(200);

                    ddlNmProced.DataSource = res;
                    ddlNmProced.DataTextField = "NOME";
                    ddlNmProced.DataValueField = "CO_PROD";
                    ddlNmProced.DataBind();
                    ddlNmProced.Items.Insert(0, new ListItem("Selecione", ""));
                }
            }
            OcultarPesquisaServAmbul(true, aux);
            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void imgbVoltarPesqServAmbul_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdServAmbulatoriais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdServAmbulatoriais.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgServAmbulatoriaisPla");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            OcultarPesquisaServAmbul(false, aux);
        }

        private void OcultarPesquisaServAmbul(bool ocultar, int index)
        {
            foreach (GridViewRow row in grdServAmbulatoriais.Rows)
            {
                DropDownList ddlNmProced = (DropDownList)row.FindControl("ddlServAmbulatorial");
                ImageButton imgPesq = (ImageButton)row.FindControl("imgbPesqServAmbul");
                TextBox txtServ = (TextBox)row.FindControl("txtDefServAmbulatorial");
                ImageButton imgPesqVolt = (ImageButton)row.FindControl("imgbVoltarPesqServAmbul");
                if (row.RowIndex == index)
                {
                    txtServ.Visible =
                    imgPesq.Visible = !ocultar;
                    ddlNmProced.Visible =
                    imgPesqVolt.Visible = ocultar;
                }
            }
            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void CriaNovaLinhaGridServicosAmulatoriais()
        {
            DataTable dtV = CriarColunasELinhaGridServAmbulatoriais();

            DataRow linha = dtV.NewRow();
            linha["CONTRATACAO"] = "";
            linha["PLANO"] = "";
            linha["TIPO"] = "";
            linha["NMPROCED"] = "";
            linha["CODIGO"] = "";
            linha["COMPLEMENTO"] = "";
            linha["VALOR"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_PROC_AMB"] = dtV;

            carregaGridNovaComContextoAmbulatorio();
        }

        private DataTable CriarColunasELinhaGridServAmbulatoriais()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CODIGO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NMPROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "COMPLEMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TIPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CONTRATACAO";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdServAmbulatoriais.Rows)
            {
                linha = dtV.NewRow();
                linha["CODIGO"] = (((TextBox)li.FindControl("txtCodigoServAmbulatorial")).Text);
                linha["NMPROCED"] = (((DropDownList)li.FindControl("ddlServAmbulatorial")).Text);
                linha["COMPLEMENTO"] = (((TextBox)li.FindControl("txtComplementoServAmbulatorial")).Text);
                linha["VALOR"] = (((TextBox)li.FindControl("txtValorServAmbulatorial")).Text);
                linha["PLANO"] = (((DropDownList)li.FindControl("ddlGridPlanoAmbul")).SelectedValue);
                linha["TIPO"] = (((DropDownList)li.FindControl("ddlGridTipoAmbul")).SelectedValue);
                linha["CONTRATACAO"] = (((DropDownList)li.FindControl("ddlGridContratacaoAmbul")).SelectedValue);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        protected void carregaGridNovaComContextoAmbulatorio()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_AMB"];

            grdServAmbulatoriais.DataSource = dtV;
            grdServAmbulatoriais.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdServAmbulatoriais.Rows)
            {
                DropDownList ddlContratacao;
                DropDownList ddlPlano;
                DropDownList ddlTipo;
                DropDownList ddlNmProced;
                TextBox txtCodigo;
                TextBox txtComplemento;
                TextBox txtValor;
                ddlContratacao = ((DropDownList)li.FindControl("ddlGridContratacaoAmbul"));
                ddlPlano = ((DropDownList)li.FindControl("ddlGridPlanoAmbul"));
                ddlTipo = ((DropDownList)li.FindControl("ddlGridTipoAmbul"));
                ddlNmProced = ((DropDownList)li.FindControl("ddlServAmbulatorial"));
                txtCodigo = ((TextBox)li.FindControl("txtCodigoServAmbulatorial"));
                txtComplemento = ((TextBox)li.FindControl("txtComplementoServAmbulatorial"));
                txtValor = ((TextBox)li.FindControl("txtValorServAmbulatorial"));

                string codigo, nmProced, vlrProced, complemento, plano, tipo, contratacao;

                //Coleta os valores do dtv da modal popup
                codigo = dtV.Rows[aux]["CODIGO"].ToString();
                nmProced = dtV.Rows[aux]["NMPROCED"].ToString();
                complemento = dtV.Rows[aux]["COMPLEMENTO"].ToString();
                vlrProced = dtV.Rows[aux]["VALOR"].ToString();
                plano = dtV.Rows[aux]["PLANO"].ToString();
                tipo = dtV.Rows[aux]["TIPO"].ToString();
                contratacao = dtV.Rows[aux]["CONTRATACAO"].ToString();


                var opr = 0;

                if (!String.IsNullOrEmpty(ddlOperPlanoServAmbu.SelectedValue) && (!String.IsNullOrEmpty(ddlPlanoServAmbu.SelectedValue) && int.Parse(ddlPlanoServAmbu.SelectedValue) != 0))
                {
                    var plan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanoServAmbu.SelectedValue));
                    plan.TB250_OPERAReference.Load();
                    opr = plan.TB250_OPERA != null ? plan.TB250_OPERA.ID_OPER : 0;
                }

                carregarOperadoras(ddlContratacao);
                ddlContratacao.SelectedValue = contratacao;
                CarregarPlanos(ddlPlano, ddlContratacao);
                ddlPlano.SelectedValue = plano;
                ddlTipo.SelectedValue = tipo;
                if (!string.IsNullOrEmpty(contratacao))
                {
                    opr = int.Parse(contratacao) > 0 ? int.Parse(contratacao) : 0;
                }
                if (tipo.Equals("P"))
                {
                    CarregarProcedimentos(ddlNmProced, opr);
                }
                else if (tipo.Equals("M"))
                {
                    var res = TB90_PRODUTO.RetornaTodosRegistros()
                                               .Join(TB96_ESTOQUE.RetornaTodosRegistros(), a => a.CO_PROD, b => b.TB90_PRODUTO.CO_PROD, (a, b) => new { a, b })
                                               .Where(x => (x.a.TB124_TIPO_PRODUTO != null && x.a.TB124_TIPO_PRODUTO.CO_TIPO_CLASS_PROD.Equals("M"))
                                                   //&& x.a.CO_SITU_PROD.Equals("A")   
                                                   && x.b.QT_SALDO_EST > 0)
                                               .Select(x => new
                                               {
                                                   x.a.CO_PROD,
                                                   NOME = x.a.TBS457_CLASS_TERAP.DE_CLASS_TERAP != null ? (x.a.NO_PROD_RED + " - " + x.a.TBS457_CLASS_TERAP.DE_CLASS_TERAP) : x.a.NO_PROD_RED
                                               }).OrderBy(x => x.NOME).Take(200);

                    ddlNmProced.DataSource = !string.IsNullOrEmpty(nmProced) ? res : res.Take(200);
                    ddlNmProced.DataTextField = "NOME";
                    ddlNmProced.DataValueField = "CO_PROD";
                    ddlNmProced.DataBind();
                }
                else
                {
                    ddlNmProced.DataSource = null;
                    ddlNmProced.DataTextField = "";
                    ddlNmProced.DataValueField = "";
                    ddlNmProced.DataBind();
                }
                ddlNmProced.SelectedValue = nmProced;
                txtCodigo.Text = codigo;
                txtComplemento.Text = complemento;
                txtValor.Text = vlrProced;
                aux++;
            }
        }

        protected void btnSalvarServAmbulatorial_OnClick(object sender, EventArgs e)
        {
            txtObsServAmbulatoriais.Text = hidObsSerAmbulatoriais.Value;

            int idAgenda = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : 0;
            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            if (idAgenda > 0)
            {
                try
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAgenda);
                    TBS426_SERVI_AMBUL tbs426 = new TBS426_SERVI_AMBUL();
                    tbs426.TBS390_ATEND_AGEND = tbs390;
                    tbs390.TBS174_AGEND_HORARReference.Load();
                    tbs426.TBS174_AGEND_HORAR = tbs390.TBS174_AGEND_HORAR;
                    tbs426.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs426.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs426.DT_CADASTRO = DateTime.Now;
                    tbs426.DE_OBSER = hidObsSerAmbulatoriais.Value;
                    tbs426.IP_CADAS = Request.UserHostAddress;
                    tbs426.FL_TIPO_AMBUL = "N";
                   
                    TBS426_SERVI_AMBUL.SaveOrUpdate(tbs426, true);

                    if (tbs426.ID_SERVI_AMBUL > 0)
                    {
                        if (PanelNormalAmbulatorio.Visible)
                        {
                            #region
                            if (grdServAmbulatoriais.Rows.Count > 0)
                            {
                                foreach (GridViewRow i in grdServAmbulatoriais.Rows)
                                {
                                    DropDownList ddlServAmbulatoriais = (DropDownList)i.FindControl("ddlServAmbulatorial");
                                    string txtComplemento = ((TextBox)i.FindControl("txtComplementoServAmbulatorial")).Text;
                                    DropDownList drpOper = (DropDownList)i.FindControl("ddlGridContratacaoAmbul");
                                    DropDownList drpPlano = (DropDownList)i.FindControl("ddlGridPlanoAmbul");
                                    TextBox txtCodigo = (TextBox)i.FindControl("txtCodigoServAmbulatorial");
                                    TextBox txtValor = (TextBox)i.FindControl("txtValorServAmbulatorial");
                                    string drpTipo = ((DropDownList)i.FindControl("ddlGridTipoAmbul")).SelectedValue;
                                    int idOper = !string.IsNullOrEmpty(drpOper.SelectedValue) ? int.Parse(drpOper.SelectedValue) : 0;
                                    int idPlan = !string.IsNullOrEmpty(drpPlano.SelectedValue) ? int.Parse(drpPlano.SelectedValue) : 0;
                                    #region
                                    if (ddlServAmbulatoriais != null)
                                    {
                                        TBS427_SERVI_AMBUL_ITENS tbs427 = new TBS427_SERVI_AMBUL_ITENS();
                                        tbs427.TBS426_SERVI_AMBUL = tbs426;
                                        #region
                                        if (!string.IsNullOrEmpty(drpTipo))
                                        {
                                            tbs427.TIPO_SERVI_AMBUL = drpTipo;
                                            if (drpTipo.Equals("P"))
                                            {
                                                tbs427.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlServAmbulatoriais.SelectedValue));
                                                if (idOper == 0 || idPlan == 0)
                                                {
                                                    throw new ArgumentException("Por favor selecione o a contratação e/ou plano do serviço ambulatorial.");
                                                }
                                            }
                                            else if (drpTipo.Equals("M"))
                                            {
                                                tbs427.TB90_PRODUTO = TB90_PRODUTO.RetornaPelaChavePrimaria(int.Parse(ddlServAmbulatoriais.SelectedValue), LoginAuxili.CO_EMP);
                                            }
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Por favor selecione o tipo do serviço ambulatorial.");
                                        }
                                        #endregion
                                        tbs427.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(idOper);
                                        tbs427.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(idPlan);
                                        tbs427.DE_COMPL = txtComplemento;
                                        TBS427_SERVI_AMBUL_ITENS.SaveOrUpdate(tbs427, true);
                                        didIdServAmbulatorial.Value = tbs426.ID_SERVI_AMBUL.ToString();

                                        TBS428_APLIC_SERVI_AMBUL tbs428 = new TBS428_APLIC_SERVI_AMBUL();
                                        tbs428.IP_APLIC_SERVI_AMBUL = Request.UserHostAddress;
                                        tbs428.IS_APLIC_SERVI_AMBUL = "N";
                                        tbs428.CO_COL_APLIC = LoginAuxili.CO_COL; ;
                                        tbs428.CO_EMP_APLIC = LoginAuxili.CO_EMP;
                                        tbs428.DT_APLIC__SERVI_AMBUL = DateTime.Now;
                                        tbs428.TBS427_SERVI_AMBUL_ITENS = TBS427_SERVI_AMBUL_ITENS.RetornaPelaChavePrimaria(tbs427.ID_LISTA_SERVI_AMBUL);

                                        TBS428_APLIC_SERVI_AMBUL.SaveOrUpdate(tbs428, true);
                                        var tbs400 = new TBS400_PRONT_MASTER();
                                        tbs400.ANAMNSE = "Ambulatório Modelo Normal - N° " + tbs426.ID_SERVI_AMBUL;
                                        GravarProntuario(tb07, tbs400);
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Por favor selecione o serviço ambulatorial.");
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                TBS426_SERVI_AMBUL.Delete(tbs426, true);
                            }
                            #endregion
                        }
                        else if (!string.IsNullOrEmpty(ASPxHtmlEditorProcedimento.Html))
                        {

                            TBS428_APLIC_SERVI_AMBUL tbs428 = new TBS428_APLIC_SERVI_AMBUL();
                            tbs428.IP_APLIC_SERVI_AMBUL = Request.UserHostAddress;
                            tbs428.IS_APLIC_SERVI_AMBUL = "N";
                            tbs428.CO_COL_APLIC = LoginAuxili.CO_COL; ;
                            tbs428.CO_EMP_APLIC = LoginAuxili.CO_EMP;
                            tbs428.DT_APLIC__SERVI_AMBUL = DateTime.Now;
                            tbs426.TBS461_ATEND_MODEL_PRESC_MEDIC = string.IsNullOrEmpty(DropDownListModeloAmb.SelectedValue) ? null : TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaPelaChavePrimaria(int.Parse(DropDownListModeloAmb.SelectedValue));
                            tbs426.DE_CONTE_MODEL = ASPxHtmlEditorProcedimento.Html;
                            tbs426.FL_TIPO_AMBUL = "L";
                            tbs428.TBS427_SERVI_AMBUL_ITENS = null;
                            TBS428_APLIC_SERVI_AMBUL.SaveOrUpdate(tbs428, true);
                            var tbs400 = new TBS400_PRONT_MASTER();
                            tbs400.ANAMNSE = "Ambulatório Modelo Livre - N° " + tbs428.ID_APLIC_SERVI_AMBUL;
                            GravarProntuario(tb07, tbs400);
                            //ASPxHtmlEditorProcedimento.Html = "";
                        }
                        else
                        {
                            throw new ArgumentException("Por favor digite o texto.");
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento.");
                return;
            }
        }

        #endregion
       // 
        protected void LinkButtonSalvarModeloExame_Click(object sender, EventArgs e)
        {
            TBS461_ATEND_MODEL_PRESC_MEDIC Modelo = new TBS461_ATEND_MODEL_PRESC_MEDIC();
            try
            {
                Modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().First(x => x.NO_MODEL_MEDIC == TextBoxNomeModeloExame.Text && x.FL_TIPO_MODELO == "E");
            }
            catch (Exception)
            {

            }
            Modelo.NO_MODEL_MEDIC = TextBoxNomeModeloExame.Text;
            Modelo.DT_SITUA_MODEL_MEDIC = DateTime.Now;
            Modelo.DE_CONTE_MODEL_MEDIC = ASPxHtmlEditorExame.Html;
            Modelo.CO_SIGLA_MODEL_MEDI = "";
            Modelo.FL_TIPO_MODELO = "E";
            var colaborador = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue));
            Modelo.TB03_COLABOR = colaborador;
            Modelo.CO_SITUA_MODEL_MEDIC = "A";
            RadioButtonLivreExames.Checked = true;
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveOrUpdate(Modelo);
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveChanges();
            if (!String.IsNullOrEmpty(drpProfResp.SelectedValue))
            {
                CarregaModelosExames(DropDownListModeloExames, int.Parse(drpProfResp.SelectedValue), Modelo.ID_MODEL_MEDIC.ToString());             
            }
            //ASPxHtmlEditorExame.Html = "";
            //TextBoxNomeModeloExame.Text = "";
            AbreModalPadrao("AbreModalExames();");
            //// divSalvarModeloReceita
        }

        protected void ButtonSalvarModeloEncam_Click(object sender, EventArgs e)
        {
            TBS461_ATEND_MODEL_PRESC_MEDIC Modelo = new TBS461_ATEND_MODEL_PRESC_MEDIC();
            try
            {
                Modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().First(x => x.NO_MODEL_MEDIC == TextBoxModeloEncam.Text && x.FL_TIPO_MODELO == "C");
            }
            catch (Exception)
            {

            }
            Modelo.NO_MODEL_MEDIC = TextBoxModeloEncam.Text;
            Modelo.DT_SITUA_MODEL_MEDIC = DateTime.Now;
            Modelo.DE_CONTE_MODEL_MEDIC = ASPxHtmlEditorEncam.Html;
            Modelo.CO_SIGLA_MODEL_MEDI = "";
            Modelo.FL_TIPO_MODELO = "C";
            var colaborador = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue));
            Modelo.TB03_COLABOR = colaborador;
            Modelo.CO_SITUA_MODEL_MEDIC = "A";
            //RadioButtonLivreExames.Checked = true;
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveOrUpdate(Modelo);
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveChanges();
            if (!String.IsNullOrEmpty(drpProfResp.SelectedValue))
            {
                CarregaModelosEncam(DropDownListModeloEncam, int.Parse(drpProfResp.SelectedValue), Modelo.ID_MODEL_MEDIC.ToString());             
            }
            //ASPxHtmlEditorEncam.Html = "";
            //TextBoxModeloEncam.Text = "";
            AbreModalPadrao("AbreModalEncam();");
        }
        protected void ButtonSalvarModeloAmb_Click(object sender, EventArgs e)
        {
            TBS461_ATEND_MODEL_PRESC_MEDIC Modelo = new TBS461_ATEND_MODEL_PRESC_MEDIC();
            try
            {
                Modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().First(x => x.NO_MODEL_MEDIC == TextBoxNomeModeloAmb.Text && x.FL_TIPO_MODELO == "A");
            }
            catch (Exception)
            {

            }
          
            Modelo.NO_MODEL_MEDIC = TextBoxNomeModeloAmb.Text;
            Modelo.DT_SITUA_MODEL_MEDIC = DateTime.Now;
            Modelo.DE_CONTE_MODEL_MEDIC = ASPxHtmlEditorProcedimento.Html;
            Modelo.CO_SIGLA_MODEL_MEDI = "";
            Modelo.FL_TIPO_MODELO = "A";
            var colaborador = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue));
            Modelo.TB03_COLABOR = colaborador;
            Modelo.CO_SITUA_MODEL_MEDIC = "A";
            RadioButtonAmbLivre.Checked = true;
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveOrUpdate(Modelo);
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveChanges();
            if (!String.IsNullOrEmpty(drpProfResp.SelectedValue))
            {
                CarregaModelosAmbulatorio(DropDownListModeloAmb, int.Parse(drpProfResp.SelectedValue), Modelo.ID_MODEL_MEDIC.ToString());
            }
            //ASPxHtmlEditorProcedimento.Html = "";
            //TextBoxNomeModeloAmb.Text  ="";
            AbreModalPadrao("AbreModalAmbulatorio();");
            //// divSalvarModeloReceita
        }
        protected void ButtonSalvarModelo_Click(object sender, EventArgs e)
        {
            TBS461_ATEND_MODEL_PRESC_MEDIC Modelo = new TBS461_ATEND_MODEL_PRESC_MEDIC();
            try
            {
                Modelo = TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().First(x => x.NO_MODEL_MEDIC == TextBoxNomeModelo.Text && x.FL_TIPO_MODELO == "M");
            }catch(Exception){
              
            }
            Modelo.NO_MODEL_MEDIC = TextBoxNomeModelo.Text;
            Modelo.DT_SITUA_MODEL_MEDIC = DateTime.Now;
            Modelo.DE_CONTE_MODEL_MEDIC = ASPxHtmlEditor1.Html;
            Modelo.CO_SIGLA_MODEL_MEDI = "";
            Modelo.FL_TIPO_MODELO = "M";
            var colaborador = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue));
            Modelo.TB03_COLABOR = colaborador;
            Modelo.CO_SITUA_MODEL_MEDIC = "A";
            RadioButtonLivre.Checked = true;
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveOrUpdate(Modelo);
            TBS461_ATEND_MODEL_PRESC_MEDIC.SaveChanges();
            if (!String.IsNullOrEmpty(drpProfResp.SelectedValue))
            {
                CarregaModelosReceituario(DropDownListModelo, int.Parse(drpProfResp.SelectedValue), Modelo.ID_MODEL_MEDIC.ToString());
            }
            //ASPxHtmlEditor1.Html = "";
            //TextBoxNomeModelo.Text = "";
            AbreModalPadrao("AbreModalMedicamentos();");
            //// divSalvarModeloReceita
        }
        protected void BtnReceituario_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para realizar emissão do receituário");
                return;
            }
            SalvaMedicamentos();
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            if (RadioButtonPesquisa.Checked)
            {
                RptReceitMedic2 fpcb = new RptReceitMedic2();
                var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));
                GerarRelatorioPadrão(fpcb, lRetorno);
            }
            else 
            {
                RptReceitLivre fpcb = new RptReceitLivre();
                var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));
                GerarRelatorioPadrão(fpcb, lRetorno);
            }
        }

        protected void BtnEncam_OnClick(object sender, EventArgs e)
        {
            AbreModalPadrao("AbreModalEncam();");
        }
        
        protected void BtnExames_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para realizar emissão de guia de exames");
                return;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            RptReceitExames2 fpcb = new RptReceitExames2();
            var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

            GerarRelatorioPadrão(fpcb, lRetorno);
        }


        protected void BtnImprmirEmcam_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para imprimir o Encaminhamento");
            //    return;
            //}
            SalvaEncam();
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            RptEncamLivre fpcb = new RptEncamLivre();
            var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

            GerarRelatorioPadrão(fpcb, lRetorno);
        }
        protected void BtnGuiaExames_OnClick(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para realizar emissão de guia de exames");
            //    return;
            //}
            SalvaExames();
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            if (PanelExamesPesquisa.Visible)
            {
                RptGuiaExames rpt = new RptGuiaExames();
                var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

                GerarRelatorioPadrão(rpt, lRetorno);
            }
            else {
                RptGuiaExamesLivre rpt = new RptGuiaExamesLivre();
                var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

                GerarRelatorioPadrão(rpt, lRetorno);
            }

           
        }

        protected void lnkbOrcamento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para realizar emissão de guia de exames");
                return;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptOrcamento2 fpcb = new RptOrcamento2();
            var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

            GerarRelatorioPadrão(fpcb, lRetorno);
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

        protected void ddlLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendamentos();
        }
        
    }
    public class CIDInternar
    {
        public bool isPrincipal { get; set; }
        public int? idItem { get; set; }
        public int idCID { get; set; }
        public string descCID { get; set; }
        public string coCID { get; set; }
        public bool existeProtocolo { get; set; }
    }
    public class OPMInternar
    {
        public int IdOPM { get; set; }
        public string TipoOPM { get; set; }
        public string NomeOPM { get; set; }
        public string CodigoOPM { get; set; }
        public int? QuantidadeOPM { get; set; }
        public string FabricanteOPM { get; set; }
        public decimal? VlUnitarioOPM { get; set; }
        public decimal? VlTotalOPM { get; set; }
    }
    public class ProcedimentoInternar
    {
        public int IdProcedimento { get; set; }
        public string TipoProcedimento { get; set; }
        public string NomeProcedimento { get; set; }
        public string CodigoProcedimento { get; set; }
        public int? QuantidadeProcedimento { get; set; }
        public decimal? VlUnitarioProcedimento { get; set; }
        public decimal? VlTotalProcedimento { get; set; }
    }
}