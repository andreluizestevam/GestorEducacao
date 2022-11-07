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
using System.Drawing;
using System.Transactions;
using C2BR.GestorEducacao.UI.Enumerados.Enumerados;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8264_AtendimentoUnificado
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
                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                IniPeriAG.Text = FimPeriAG.Text = txtDtAtend.Text = data.ToString();
                txtIniAgenda.Text = data.AddDays(-5).ToString();
                txtFimAgenda.Text = data.AddDays(15).ToString();

                //carregarOperadoras(ddlOperOrc);
                carregarOperadoras(ddlOperProcPlan);
                //CarregarPlanos(ddlPlanOrc, ddlOperOrc);
                CarregarPlanos(ddlPlanProcPlan, ddlOperProcPlan);

                CarregaAgendamentos();

                CarregaGrupoMedicamento(ddlGrupo);
                CarregaGrupoMedicamento(drpGrupoMedic, true);
                CarregaSubGrupoMedicamento(ddlGrupo, ddlSubGrupo);
                CarregaSubGrupoMedicamento(drpGrupoMedic, drpSubGrupoMedic, true);
                CarregaUnidade();

                CarregaGrupos();
                CarregaOperadoras();
                CarregaSubGrupos();

                CarregarProfissionais(drpProfResp);
                CarregarProfissionais(drpTecnAtend);
                CarregarProfissionais(drpProfAuxi);

                carregarLocal();
                ddlLocal.SelectedValue = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_DEPTO.ToString();

                //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
                if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M"))
                    drpProfResp.Enabled = false;

                if ((LoginAuxili.FLA_PROFESSOR == "S"))
                    drpProfResp.SelectedValue = LoginAuxili.CO_COL.ToString();

                txtVlBase.Enabled = txtVlCusto.Enabled = txtVlRestitu.Enabled = true;

                //if (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_CLASS_PROFI != "D")
                //    divBtnOdontograma.Visible = false;
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            Persistencias(false);
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Realiza as persistências pertinentes às informações de atendimento
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
                    DropDownList ddlContrat = (((DropDownList)i.FindControl("ddlOperOrc")));
                    DropDownList ddlPlan = (((DropDownList)i.FindControl("ddlPlanOrc")));

                    if (string.IsNullOrEmpty(ddlContrat.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, string.Format("Na linha nº {0} do orçamento o contrato de saúde não está informado", i.RowIndex + 1));
                        ddlContrat.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(ddlPlan.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, string.Format("Na linha nº {0} do orçamento o plano de saúde não está informado", i.RowIndex + 1));
                        ddlPlan.Focus();
                        return;
                    }

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

                #endregion

                #endregion

                #region Persistências

                if (finalizar)
                    ExecutarFuncaoPadrao("PararCronometro();");

                #region Grava o atendimento

                var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);

                TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

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

                tbs390.DE_OBSER = txtObserAtend.Text;
                tbs390.DE_CONSI = hidTxtObserv.Value;
                tbs390.DE_QXA_PRINC = txtQueixa.Text;
                tbs390.DE_HDA = txtHDA.Text;
                tbs390.DE_EXM_FISIC = txtExameFis.Text;
                tbs390.DE_HIP_DIAGN = txtHipotese.Text;
                tbs390.DE_ALERGIA = txtAlergiaMedic.Text;
                tbs390.FL_ALERGIA = chkAlergiaMedic.Checked ? "S" : "N";
                tbs390.FL_SITU_FATU = hidCkOrcamAprov.Value == "S" ? "A" : "N";//Aprovado ou Negado
                tbs390.TB14_DEPTO = !string.IsNullOrEmpty(ddlLocal.SelectedValue) ? TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocal.SelectedValue)) : null;

                //Dados de quem realizou o atendimento
                tbs390.DT_REALI = DateTime.Parse(txtDtAtend.Text).Add(TimeSpan.Parse(DateTime.Now.ToShortTimeString()));
                tbs390.CO_COL_ATEND = int.Parse(drpProfResp.SelectedValue);
                tbs390.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_COL_TEC_ATEND = !String.IsNullOrEmpty(drpTecnAtend.SelectedValue) ? int.Parse(drpTecnAtend.SelectedValue) : (int?)null;
                tbs390.CO_EMP_TEC_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_COL_AUX_ATEND = !String.IsNullOrEmpty(drpProfAuxi.SelectedValue) ? int.Parse(drpProfAuxi.SelectedValue) : (int?)null;
                tbs390.CO_EMP_AUX_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue)).CO_COL;
                tbs390.DT_VALID_ORCAM = (!string.IsNullOrEmpty(hidDtValidOrcam.Value) ? DateTime.Parse(hidDtValidOrcam.Value) : (DateTime?)null);
                tbs390.VL_DSCTO_ORCAM = 0;
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
                #endregion

                #region Atualiza a agenda de ação

                //Atualizo apenas que a ação foi realizada

                tbs174.FL_SITUA_ACAO = "R";
                tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

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

                        if (!String.IsNullOrEmpty(hidHoras.Value) && !String.IsNullOrEmpty(hidMinutos.Value))
                            tbs174ob.HR_DURACAO_ATEND = int.Parse(hidHoras.Value).ToString("D2") + ":" + int.Parse(hidMinutos.Value).ToString("D2");

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ob, true);

                        continue;
                    }
                }

                #endregion

                #region Armazena o Orçamento

                //var tbs396s = TBS396_ATEND_ORCAM.RetornaTodosRegistros().Where(o => o.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs390.ID_ATEND_AGEND).ToList();

                //foreach (var tbs396 in tbs396s)
                //    TBS396_ATEND_ORCAM.Delete(tbs396, true);

                //Realiza as persistências do orçamento
                foreach (GridViewRow i in grdProcedOrcam.Rows)
                {
                    DropDownList ddlProc = (((DropDownList)i.FindControl("ddlProcedOrc")));
                    DropDownList ddlContrat = (((DropDownList)i.FindControl("ddlOperOrc")));
                    DropDownList ddlPlan = (((DropDownList)i.FindControl("ddlPlanOrc")));
                    DropDownList ddlDente = (((DropDownList)i.FindControl("ddlNumDente")));
                    TextBox txtQtd = (((TextBox)i.FindControl("txtQtdProcedOrc")));
                    TextBox txtValor = (((TextBox)i.FindControl("txtValorProcedOrc")));
                    TextBox txtValorDesc = (((TextBox)i.FindControl("txtValorDescOrc")));
                    CheckBox chkHomolItem = (((CheckBox)i.FindControl("chkHomolItem")));
                    CheckBox chkCortItem = (((CheckBox)i.FindControl("chkCortItem")));
                    TextBox idOrcam = (((TextBox)i.FindControl("txtAtendOrcam")));

                    var _tbs396 = !string.IsNullOrEmpty(idOrcam.Text) ? TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(int.Parse(idOrcam.Text)) : null;
                    TBS396_ATEND_ORCAM tbs396 = _tbs396 != null ? _tbs396 : new TBS396_ATEND_ORCAM();
                    tbs396.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue));
                    if (_tbs396 == null)
                    {
                        tbs396.TBS390_ATEND_AGEND = tbs390;
                        tbs396.NU_REGIS = tbs390.NU_REGIS;
                        tbs396.DT_CADAS = DateTime.Now;
                        tbs396.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs396.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs396.CO_EMP_COL_CADAS = emp_col;
                        tbs396.IP_CADAS = Request.UserHostAddress;
                    }
                    tbs396.QT_PROC = int.Parse(txtQtd.Text);
                    tbs396.VL_PROC = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : 0);
                    tbs396.DE_OBSER = hidObsOrcam.Value;
                    tbs396.VL_DSCTO_PROC = (!string.IsNullOrEmpty(txtValorDesc.Text) ? decimal.Parse(txtValorDesc.Text) : 0);
                    tbs396.CO_SITU = (chkHomolItem.Checked ? "S" : "N");
                    tbs396.CO_CONTRAT = (!String.IsNullOrEmpty(ddlContrat.SelectedValue) ? int.Parse(ddlContrat.SelectedValue) : 0);
                    tbs396.CO_PLAN = (!String.IsNullOrEmpty(ddlPlan.SelectedValue) ? int.Parse(ddlPlan.SelectedValue) : 0);
                    tbs396.FL_CORT = (chkCortItem.Checked ? "S" : "N");
                    tbs396.NU_DENTE = int.Parse(ddlDente.SelectedValue);

                    TBS396_ATEND_ORCAM.SaveOrUpdate(tbs396, true);
                }

                #endregion

                TBS370_PLANE_AVALI tbs370 = null;
                TBS386_ITENS_PLANE_AVALI tbs386 = null;
                TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = null;
                TBS356_PROC_MEDIC_PROCE tbs356 = null;

                #region Tratamento
                if (finalizar)
                {
                    foreach (GridViewRow row in grdProcedAprovados.Rows)
                    {
                        if (((CheckBox)row.Cells[1].FindControl("chkProcedimento")).Checked && ((CheckBox)row.Cells[1].FindControl("chkProcedimento")).Enabled)
                        {
                            int hidTrataPlano = int.Parse(((HiddenField)row.Cells[1].FindControl("hidIdTrataPlano")).Value);
                            int hidIdAtendOrcam = int.Parse(((HiddenField)row.Cells[1].FindControl("hidIdAtendOrcam")).Value);
                            int hidIdProcedimento = int.Parse(((HiddenField)row.Cells[1].FindControl("hidIdProcedimento")).Value);
                            var tbs396 = TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(hidIdAtendOrcam);
                            tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(hidIdProcedimento);
                            var tbs458 = hidTrataPlano > 0 ? TBS458_TRATA_PLANO.RetornaPelaChavePrimaria((hidTrataPlano)) : new TBS458_TRATA_PLANO();
                            if (hidTrataPlano == 0)
                            {
                                tbs458.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs458.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs458.DT_CADAS = DateTime.Now;
                                tbs458.IP_CADAS = Request.UserHostName;

                                #region Gera Código da Consulta

                                string coUnid = LoginAuxili.CO_UNID.ToString();
                                int coEmp = LoginAuxili.CO_EMP;
                                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                                var res = (from tbs458pesq in TBS458_TRATA_PLANO.RetornaTodosRegistros()
                                           where tbs458pesq.NU_REGIS != null
                                           select new { tbs458pesq.NU_REGIS }).OrderByDescending(w => w.NU_REGIS).FirstOrDefault();

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
                                    seq = res.NU_REGIS.Substring(7, 7);
                                    seq2 = int.Parse(seq);
                                }

                                seqConcat = seq2 + 1;
                                seqcon = seqConcat.ToString().PadLeft(7, '0');

                                tbs458.NU_REGIS = ano + coUnid.PadLeft(3, '0') + "PL" + seqcon;
                                #endregion

                                tbs458.DE_OBSER = "";
                                tbs458.TBS396_ATEND_ORCAM = tbs396;
                                tbs458.TBS174_AGEND_HORAR = tbs174;
                            }
                            //A - ativo, F - finalizado, C - cancelado
                            tbs458.CO_SITUA = "F";
                            tbs458.IP_SITUA = Request.UserHostName;
                            tbs458.DT_SITUA = DateTime.Now;
                            tbs458.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs458.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs458 = TBS458_TRATA_PLANO.SaveOrUpdate(tbs458);

                            tbs370 = RecuperaPlanejamento((int?)null, tbs174.CO_ALU.Value);
                            tbs386 = RecuperarItensPlanejamento((int?)null, tbs370.ID_PLANE_AVALI, tbs356.ID_PROC_MEDI_PROCE, tbs174.ID_AGEND_HORAR, tbs396.ID_ATEND_ORCAM);
                            tbs389 = RecuperarAssocItensPlaneAgend(tbs174, tbs386);
                        }
                    }
                    tbs174.TBS370_PLANE_AVALI = tbs370;
                    tbs174.TBS356_PROC_MEDIC_PROCE = tbs356;
                    tbs174.TBS386_ITENS_PLANE_AVALI = tbs386;
                }

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                #endregion

                #region Armazena os Medicamentos

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

                    tbs399.DT_CADAS = DateTime.Now;
                    tbs399.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs399.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs399.CO_EMP_COL_CADAS = emp_col;
                    tbs399.IP_CADAS = Request.UserHostAddress;
                    TBS399_ATEND_MEDICAMENTOS.SaveOrUpdate(tbs399, true);
                }

                #endregion

                #region Armazena os Exames

                foreach (var tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros().Where(e => e.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs390.ID_ATEND_AGEND).ToList())
                    TBS398_ATEND_EXAMES.Delete(tbs398, true);

                //Realiza as persistências do orçamento
                foreach (GridViewRow i in grdExame.Rows)
                {
                    DropDownList ddlExame = (DropDownList)i.FindControl("ddlExame");

                    TBS398_ATEND_EXAMES tbs398 = new TBS398_ATEND_EXAMES();
                    tbs398.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlExame.SelectedValue));
                    tbs398.TBS390_ATEND_AGEND = tbs390;
                    tbs398.DE_OBSER = (!string.IsNullOrEmpty(hidObserExame.Value) ? hidObserExame.Value : null);

                    tbs398.DT_CADAS = DateTime.Now;
                    tbs398.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs398.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs398.CO_EMP_COL_CADAS = emp_col;
                    tbs398.IP_CADAS = Request.UserHostAddress;
                    TBS398_ATEND_EXAMES.SaveOrUpdate(tbs398, true);
                }

                #endregion

                #endregion

                RecarregarGrids(atend, tbs174.CO_ALU.Value);

                //if (finalizar)
                //    BtnSalvar.OnClientClick = BtnFinalizar.OnClientClick = "alert('Atendimento já finalizado!'); return false;";

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Atendimento " + (finalizar ? "FINALIZADO" : "SALVO") + " com sucesso!");
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar! Entre em contato com o suporte! Erro: " + e.Message);
            }
        }

        private void carregarLocal()
        {
            ddlLocal.DataSource = TB14_DEPTO.RetornaTodosRegistros();
            ddlLocal.DataTextField = "NO_DEPTO";
            ddlLocal.DataValueField = "CO_DEPTO";
            ddlLocal.DataBind();
            ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS356_PROC_MEDIC_PROCE RetornaEntidade()
        {
            TBS356_PROC_MEDIC_PROCE tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs356 == null) ? new TBS356_PROC_MEDIC_PROCE() : tbs356;
        }

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo2, false, true, false, C2BR.GestorEducacao.UI.Library.Auxiliares.AuxiliCarregamentos.ETiposClassificacoes.atendimento);
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

        private void CarregaDentes(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new ListItem("Não Especificado", "99"));
            ddl.Items.Insert(0, new ListItem("48", "48"));
            ddl.Items.Insert(0, new ListItem("47", "47"));
            ddl.Items.Insert(0, new ListItem("46", "46"));
            ddl.Items.Insert(0, new ListItem("45", "45"));
            ddl.Items.Insert(0, new ListItem("44", "44"));
            ddl.Items.Insert(0, new ListItem("43", "43"));
            ddl.Items.Insert(0, new ListItem("42", "42"));
            ddl.Items.Insert(0, new ListItem("41", "41"));
            ddl.Items.Insert(0, new ListItem("38", "38"));
            ddl.Items.Insert(0, new ListItem("37", "37"));
            ddl.Items.Insert(0, new ListItem("36", "36"));
            ddl.Items.Insert(0, new ListItem("35", "35"));
            ddl.Items.Insert(0, new ListItem("34", "34"));
            ddl.Items.Insert(0, new ListItem("33", "33"));
            ddl.Items.Insert(0, new ListItem("32", "32"));
            ddl.Items.Insert(0, new ListItem("31", "31"));
            ddl.Items.Insert(0, new ListItem("28", "28"));
            ddl.Items.Insert(0, new ListItem("27", "27"));
            ddl.Items.Insert(0, new ListItem("26", "26"));
            ddl.Items.Insert(0, new ListItem("25", "25"));
            ddl.Items.Insert(0, new ListItem("24", "24"));
            ddl.Items.Insert(0, new ListItem("23", "23"));
            ddl.Items.Insert(0, new ListItem("22", "22"));
            ddl.Items.Insert(0, new ListItem("21", "21"));
            ddl.Items.Insert(0, new ListItem("18", "18"));
            ddl.Items.Insert(0, new ListItem("17", "17"));
            ddl.Items.Insert(0, new ListItem("16", "16"));
            ddl.Items.Insert(0, new ListItem("15", "15"));
            ddl.Items.Insert(0, new ListItem("14", "14"));
            ddl.Items.Insert(0, new ListItem("13", "13"));
            ddl.Items.Insert(0, new ListItem("12", "12"));
            ddl.Items.Insert(0, new ListItem("11", "11"));
            ddl.Items.Insert(0, new ListItem("Todos", "0"));
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
                               ID_ATEND_ORCAM = tbs396.ID_ATEND_ORCAM,
                               ID_PROC_MEDI_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE != null ? tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE : 0,
                               NM_PROC_MEDI_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE != null ? tbs396.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI : "",
                               tbs396.QT_PROC,
                               tbs396.VL_PROC,
                               tbs396.DE_OBSER,
                               VL_DESC = tbs396.VL_DSCTO_PROC != null ? tbs396.VL_DSCTO_PROC : 0,
                               CO_CONTRAT = tbs396.CO_CONTRAT != null ? tbs396.CO_CONTRAT : 0,
                               CO_PLAN = tbs396.CO_PLAN != null ? tbs396.CO_PLAN : 0,
                               HOMOL = String.IsNullOrEmpty(tbs396.CO_SITU) ? Boolean.FalseString : tbs396.CO_SITU.Equals("S") ? Boolean.TrueString : Boolean.FalseString,
                               CORT = String.IsNullOrEmpty(tbs396.FL_CORT) ? Boolean.FalseString : tbs396.FL_CORT.Equals("S") ? Boolean.TrueString : Boolean.FalseString,
                               DENTE = tbs396.NU_DENTE != null ? tbs396.NU_DENTE : 0
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["CONTRAT"] = i.CO_CONTRAT;
                    linha["PLANO"] = i.CO_PLAN;
                    linha["PROCED"] = i.ID_PROC_MEDI_PROCE;
                    linha["NMPROCED"] = i.NM_PROC_MEDI_PROCE;
                    linha["QTD"] = i.QT_PROC;
                    linha["CORT"] = i.CORT;
                    linha["VALOR"] = i.VL_PROC;
                    linha["VALORUNIT"] = "";
                    linha["VALORDESC"] = i.VL_DESC;
                    linha["VALORTOTAL"] = ((i.VL_PROC * i.QT_PROC) - (i.VL_DESC != null ? i.VL_DESC : 0));
                    linha["HOMOL"] = i.HOMOL;
                    linha["DENTE"] = i.DENTE;
                    linha["ID_ATEND_ORCAM"] = i.ID_ATEND_ORCAM;

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
        protected void ExcluiItemGridOrcamento(int Index, int IdOrcamento)
        {
            DataTable dtV = CriarColunasELinhaGridOrcamento();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_ORC"] = dtV;

            var tbs396 = TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(IdOrcamento);
            if (tbs396 != null)
            {
                var tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == tbs396.ID_ATEND_ORCAM).ToList();

                foreach (var item in tbs458)
                {
                    item.TBS174_AGEND_HORARReference.Load();
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(item.TBS174_AGEND_HORAR.ID_AGEND_HORAR);
                    tbs174.CO_ALU = (int?)null;
                    var Tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(X => X.TBS174_AGEND_HORAR.ID_AGEND_HORAR == tbs174.ID_AGEND_HORAR).ToList();
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
                    TBS458_TRATA_PLANO.Delete(item, true);
                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                }
                TBS396_ATEND_ORCAM.Delete(tbs396, true);
            }
            carregaGridNovaComContextoOrcamento();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridOrcamento()
        {
            DataTable dtV = CriarColunasELinhaGridOrcamento();

            DataRow linha = dtV.NewRow();
            linha["CONTRAT"] = "";
            linha["PLANO"] = "";
            linha["PROCED"] = "";
            linha["NMPROCED"] = "";
            linha["DENTE"] = "0";
            linha["QTD"] = "1";
            linha["CORT"] = Boolean.FalseString;
            linha["VALOR"] = "";
            linha["VALORUNIT"] = "";
            linha["VALORDESC"] = "";
            linha["VALORTOTAL"] = "";
            linha["HOMOL"] = Boolean.TrueString;
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
            dcATM.ColumnName = "CONTRAT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

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
            dcATM.ColumnName = "DENTE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTD";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CORT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALORUNIT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALORDESC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALORTOTAL";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_ATEND_ORCAM";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "HOMOL";
            dtV.Columns.Add(dcATM);


            DataRow linha;
            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                linha = dtV.NewRow();
                linha["CONTRAT"] = (((DropDownList)li.FindControl("ddlOperOrc")).SelectedValue);
                linha["PLANO"] = (((DropDownList)li.FindControl("ddlPlanOrc")).SelectedValue);
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProcedOrc")).SelectedValue);
                linha["NMPROCED"] = (((TextBox)li.FindControl("txtCodigProcedOrc")).Text);
                linha["QTD"] = (((DropDownList)li.FindControl("ddlNumDente")).Text);
                linha["QTD"] = (((TextBox)li.FindControl("txtQtdProcedOrc")).Text);
                linha["VALOR"] = (((TextBox)li.FindControl("txtValorProcedOrc")).Text);
                linha["VALORUNIT"] = (((HiddenField)li.FindControl("hidValUnitProc")).Value);
                linha["VALORDESC"] = (((TextBox)li.FindControl("txtValorDescOrc")).Text);
                linha["VALORTOTAL"] = (((TextBox)li.FindControl("txtValorTotalOrc")).Text);
                linha["HOMOL"] = (((CheckBox)li.FindControl("chkHomolItem")).Checked);
                linha["CORT"] = (((CheckBox)li.FindControl("chkCortItem")).Checked);
                linha["ID_ATEND_ORCAM"] = (((TextBox)li.FindControl("txtAtendOrcam")).Text);
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
                DropDownList ddlCodigoi, DdlOperOrc, DdlPlanOrc, DdlNumDente;
                TextBox txtNmProced, txtQtdProced, txtVlProced, txtValorDescOrc, txtValorTotalOrc, txtAtendOrcam;
                HiddenField hidVlUnit;
                CheckBox chkHomolItem, chkCortItem;
                DdlOperOrc = ((DropDownList)li.FindControl("ddlOperOrc"));
                DdlPlanOrc = ((DropDownList)li.FindControl("ddlPlanOrc"));
                DdlNumDente = ((DropDownList)li.FindControl("ddlNumDente"));
                ddlCodigoi = ((DropDownList)li.FindControl("ddlProcedOrc"));
                txtNmProced = ((TextBox)li.FindControl("txtCodigProcedOrc"));
                txtQtdProced = ((TextBox)li.FindControl("txtQtdProcedOrc"));
                txtVlProced = ((TextBox)li.FindControl("txtValorProcedOrc"));
                txtValorDescOrc = ((TextBox)li.FindControl("txtValorDescOrc"));
                txtValorTotalOrc = ((TextBox)li.FindControl("txtValorTotalOrc"));
                hidVlUnit = ((HiddenField)li.FindControl("hidValUnitProc"));
                chkHomolItem = (CheckBox)li.FindControl("chkHomolItem");
                chkCortItem = ((CheckBox)li.FindControl("chkCortItem"));
                txtAtendOrcam = ((TextBox)li.FindControl("txtAtendOrcam"));
                string codigo, nmProced, qtdProced, vlProced, vlUnitProced, contrat, plan, desc, total, homol, cort, dente, id;

                //Coleta os valores do dtv da modal popup
                codigo = dtV.Rows[aux]["PROCED"].ToString();
                nmProced = dtV.Rows[aux]["NMPROCED"].ToString();
                dente = dtV.Rows[aux]["DENTE"].ToString();
                qtdProced = dtV.Rows[aux]["QTD"].ToString();
                vlProced = dtV.Rows[aux]["VALOR"].ToString();
                vlUnitProced = dtV.Rows[aux]["VALORUNIT"].ToString();
                contrat = dtV.Rows[aux]["CONTRAT"].ToString();
                plan = dtV.Rows[aux]["PLANO"].ToString();
                desc = dtV.Rows[aux]["VALORDESC"].ToString();
                total = dtV.Rows[aux]["VALORTOTAL"].ToString();
                homol = dtV.Rows[aux]["HOMOL"].ToString();
                cort = dtV.Rows[aux]["CORT"].ToString();
                id = dtV.Rows[aux]["ID_ATEND_ORCAM"].ToString();

                if (DdlOperOrc.Items.Count <= 0)
                {
                    carregarOperadoras(DdlOperOrc);
                }

                if (DdlPlanOrc.Items.Count <= 0)
                {
                    CarregarPlanos(DdlPlanOrc, DdlOperOrc);
                }

                CarregaDentes(DdlNumDente);

                DdlOperOrc.SelectedValue = contrat;

                DdlPlanOrc.SelectedValue = plan;


                var opr = !String.IsNullOrEmpty(DdlOperOrc.SelectedValue) ? int.Parse(DdlOperOrc.SelectedValue) : 0;

                var tb03 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);

                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where (opr != 0 ? tbs356.TB250_OPERA.ID_OPER == opr : 0 == 0)
                           && tbs356.CO_TIPO_PROC_MEDI != "EX"
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
                DdlNumDente.SelectedValue = dente;
                txtQtdProced.Text = qtdProced;
                txtVlProced.Text = vlProced;
                txtValorDescOrc.Text = desc;
                txtValorTotalOrc.Text = total;
                chkHomolItem.Checked = Boolean.Parse(homol);
                chkCortItem.Checked = Boolean.Parse(cort);
                txtAtendOrcam.Text = id;

                if (chkCortItem.Checked)
                {
                    txtVlProced.Enabled = false;
                    txtValorDescOrc.Enabled = false;
                    txtValorTotalOrc.Enabled = false;
                }
                else
                {
                    txtVlProced.Enabled = true;
                    txtValorDescOrc.Enabled = true;
                    txtValorTotalOrc.Enabled = true;
                }

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

            DataRow linha;

            foreach (GridViewRow li in grdExame.Rows)
            {
                linha = dtV.NewRow();
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlExame")).SelectedValue);
                linha["NMPROCED"] = (((TextBox)li.FindControl("txtCodigProcedPla")).Text);
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
                ddlCodigoi = ((DropDownList)li.FindControl("ddlExame"));
                txtNmProced = ((TextBox)li.FindControl("txtCodigProcedPla"));

                string codigo, nmProced;

                //Coleta os valores do dtv da modal popup
                codigo = dtV.Rows[aux]["PROCED"].ToString();
                nmProced = dtV.Rows[aux]["NMPROCED"].ToString();

                var opr = !String.IsNullOrEmpty(ddlOperProcPlan.SelectedValue) ? int.Parse(ddlOperProcPlan.SelectedValue) : 0;

                //Seta os valores nos campos da modal popup
                CarregarProcedimentos(ddlCodigoi, opr, "EX");
                ddlCodigoi.SelectedValue = codigo;
                txtNmProced.Text = nmProced;
                aux++;
            }
        }

        protected void carregaGridMedic(int idAtendAgend)
        {
            DataTable dtV = CriarColunasELinhaGridMedic();

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
                           FORNEC = tb90.TB41_FORNEC != null ? tb90.TB41_FORNEC.NO_SIGLA_FORN : " - ",
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
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanop, ddlOperp, false, false, true);
        }

        /// <summary>
        /// Carrega as operadoras
        /// </summary>
        /// <param name="ddlOperp"></param>
        private void carregarOperadoras(DropDownList ddlOperp)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperp, false, false, true, false);
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

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                           //&& (tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_AGEND_ENCAM == "S")
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!string.IsNullOrEmpty(txtNomePacPesqAgendAtend.Text) ? tb07.NO_ALU.Contains(txtNomePacPesqAgendAtend.Text) : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
                       select new saidaPacientes
                       {
                           AgendaHora = tbs174.HR_AGEND_HORAR,
                           CO_ALU = tbs174.CO_ALU,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                           PACIENTE = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU,
                           DT_NASC_PAC = tb07.DT_NASC_ALU,
                           SX = tb07.CO_SEXO_ALU,
                           TP_DEF = tb07.TP_DEF,
                           PASTA_CONTROL = tb07.DE_PASTA_CONTR,
                           podeClicar = true,
                           //podeClicar = (tbs174.FL_AGEND_ENCAM == "S" && tbs174.CO_SITUA_AGEND_HORAR != "R" ? true : false),

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,

                           Cortesia = tbs174.FL_CORTESIA,
                           Contratacao = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : "",
                           ContratParticular = tbs174.TB250_OPERA != null ? (tbs174.TB250_OPERA.FL_INSTI_OPERA != null && tbs174.TB250_OPERA.FL_INSTI_OPERA == "S") : false
                       }).ToList();

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
            public string PASTA_CONTROL { get; set; }

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
                           DE_ACAO = (IIlaten != null ? (!string.IsNullOrEmpty(IIlaten.DE_HIP_DIAGN) ? IIlaten.DE_HIP_DIAGN : " - ") : (!string.IsNullOrEmpty(tbs174.DE_ACAO_PLAN) ? tbs174.DE_ACAO_PLAN : " - ")),
                           PROFISSIONAL = tb03 != null ? tb03.NO_APEL_COL : " - ",
                           ID_AGENDA = tbs174.ID_AGEND_HORAR,

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,
                           podeClicar = true
                           //podeClicar = (tbs174.FL_SITUA_ACAO != null ? (tbs174.FL_SITUA_ACAO != "R" ? true : false) : true)
                       }).ToList();

            res = res.OrderBy(w => w.dtAgenda).ThenBy(w => w.hora).ToList();

            grdHistoricoAgenda.DataSource = res;
            grdHistoricoAgenda.DataBind();
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

        private void CarregarProcedimentosAprovados(int CO_ALU)
        {
            grdProcedAprovados.DataSource = null;
            grdProcedAprovados.DataBind();

            var res = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                       where tbs390.TBS174_AGEND_HORAR.CO_ALU == CO_ALU
                       && (tbs390.FL_SITU_FATU == "A" || tbs390.FL_SITU_FATU == "F")
                       select new
                       {
                           Situacao = tbs396.CO_SITU,
                           IdAtendOrcam = tbs396.ID_ATEND_ORCAM,
                           IdAtendAgend = tbs390.ID_ATEND_AGEND,
                           IdAgendHorar = tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                           dtAgenda = tbs396.DT_CADAS,
                           Profissional = tb03 != null ? tb03.NO_APEL_COL : " - ",
                           Procedimento = tbs396.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           idProcedimento = tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                       }).ToList();

            var listProc = new List<saidaProcedimentoAprovados>();
            foreach (var r in res)
            {
                int idtbs174 = !string.IsNullOrEmpty(hidIdAgenda.Value) ? TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).ID_AGEND_HORAR : 0;
                var tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == r.IdAtendOrcam && x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idtbs174).FirstOrDefault();
                var _tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == r.IdAtendOrcam).FirstOrDefault();
                if (_tbs458 != null)
                {
                    _tbs458.TBS174_AGEND_HORARReference.Load();
                }
                var p = new saidaProcedimentoAprovados();
                p.dtAgenda = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? _tbs458.DT_SITUA.Value.Date : r.dtAgenda;
                p.IdAtendAgend = r.IdAtendAgend;
                p.IdProcMedicProce = r.idProcedimento;
                p.IdTrataPlano = _tbs458 != null ? _tbs458.ID_TRATA_PLANO : 0;
                p.imagem_URL_ACAO = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? "/Library/IMG/PGS_IC_Positivo.png" : (_tbs458 != null && _tbs458.CO_SITUA.Equals("C")) || r.Situacao.Equals("C") ? "/Library/IMG/PGS_IC_Cancelado.png" : "/Library/IMG/PGS_IC_Negativo.png";
                p.isCheck = tbs458 != null ? true : false;
                p.nomeProcedimento = _tbs458 != null ? r.Procedimento : "*" + r.Procedimento;
                p.nomeProfissional = r.Profissional;
                p.IdAtendHorar = idtbs174;
                p.IdAtendOrcam = r.IdAtendOrcam;
                p.tooltip = _tbs458 != null ? "Procedimento planejado (Tratamento)" : "Procedimento sem planejamento de tratamento";
                p.desabilitar = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? false : (_tbs458 != null && _tbs458.CO_SITUA.Equals("C")) || r.Situacao.Equals("C") ? false : true;
                listProc.Add(p);
            }
            grdProcedAprovados.DataSource = listProc.OrderBy(w => w.dtAgenda).ToList();
            grdProcedAprovados.DataBind();
        }

        public class saidaProcedimentoAprovados
        {
            public int IdAtendOrcam { get; set; }
            public int IdAtendHorar { get; set; }
            public int IdAtendAgend { get; set; }
            public int IdProcMedicProce { get; set; }
            public int IdTrataPlano { get; set; }
            public string nomeProcedimento { get; set; }
            public string nomeProfissional { get; set; }
            public DateTime dtAgenda { get; set; }
            public string imagem_URL_ACAO { get; set; }
            public bool isCheck { get; set; }
            public string tooltip { get; set; }
            public bool desabilitar { get; set; }
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

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where tbs174.ID_AGEND_HORAR == ID_AGEND_HORAR
                       select new
                       {
                           tbs174.DT_AGEND_HORAR,
                           tbs174.DE_ACAO_PLAN,
                       }).FirstOrDefault();

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
            txtQueixa.Text =
            txtHDA.Text =
            txtExameFis.Text =
            txtHipotese.Text =
            txtObserAtend.Text =
            txtAlergiaMedic.Text =
            txtDtValidade.Text =
            txtVlTotalOrcam.Text =
            BtnSalvar.OnClientClick =
            BtnFinalizar.OnClientClick = "";

            chkAprovado.Text = "Aprovado";
            chkAprovado.Enabled = true;
            chkAlergiaMedic.Checked = false;

            if (grdProcedOrcam.Rows.Count != 0 || grdExame.Rows.Count != 0 || grdMedicamentos.Rows.Count != 0)
                LimparGridsAgendamento();

            var Atend = TBS390_ATEND_AGEND.RetornaTodosRegistros().Where(a => a.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR).FirstOrDefault();

            if (Atend != null)
            {
                hidIdAtendimento.Value = Atend.ID_ATEND_AGEND.ToString();

                if (drpProfResp.Items.FindByValue(Atend.CO_COL_ATEND.ToString()) != null)
                    drpProfResp.SelectedValue = Atend.CO_COL_ATEND.ToString();

                if (drpTecnAtend.Items.FindByValue(Atend.CO_COL_TEC_ATEND.ToString()) != null)
                    drpTecnAtend.SelectedValue = Atend.CO_COL_TEC_ATEND.ToString();

                if (drpProfAuxi.Items.FindByValue(Atend.CO_COL_AUX_ATEND.ToString()) != null)
                    drpProfAuxi.SelectedValue = Atend.CO_COL_AUX_ATEND.ToString();

                //if (Atend.CO_SITUA == "F")
                //    BtnSalvar.OnClientClick = BtnFinalizar.OnClientClick = "alert('Atendimento já finalizado!'); return false;";

                hidTxtObserv.Value = txtObservacoes.Text = Atend.DE_CONSI;
                txtObserAtend.Text = Atend.DE_OBSER;
                txtQueixa.Text = Atend.DE_QXA_PRINC;
                txtHDA.Text = Atend.DE_HDA;
                txtExameFis.Text = Atend.DE_EXM_FISIC;
                txtHipotese.Text = Atend.DE_HIP_DIAGN;
                txtAlergiaMedic.Text = Atend.DE_ALERGIA;
                chkAlergiaMedic.Checked = Atend.FL_ALERGIA == "S" ? true : false;

                if (Atend.DT_VALID_ORCAM.HasValue)
                    txtDtValidade.Text = hidDtValidOrcam.Value = Atend.DT_VALID_ORCAM.Value.ToShortDateString();
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

                carregaGridOrcamento(Atend.ID_ATEND_AGEND);
                carregaGridExame(Atend.ID_ATEND_AGEND);
                carregaGridMedic(Atend.ID_ATEND_AGEND);
            }
        }

        private void LimparGridsAgendamento()
        {
            grdProcedOrcam.DataSource =
            grdHistoricoAgenda.DataSource =
            grdExame.DataSource =
            grdProcedAprovados.DataSource =
            grdMedicamentos.DataSource = null;

            grdProcedOrcam.DataBind();
            grdHistoricoAgenda.DataBind();
            grdExame.DataBind();
            grdProcedAprovados.DataBind();
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
                           hr_Consul = tbs174.HR_AGEND_HORAR,

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
                       }).OrderByDescending(w => w.hr_Consul).ThenBy(w => w.NO_PAC_).ToList();

            grdPacAtestado.DataSource = res;
            grdPacAtestado.DataBind();

            AbreModalPadrao("AbreModalAtestado();");
        }

        public class PacientesAtestado
        {
            public string hr_Consul { get; set; }
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

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.DT_AGEND_HORAR == dtAtdo
                       && (LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M" && LoginAuxili.FLA_PROFESSOR == "S" ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null && res.Count > 0)
            {
                drpPacienteGuia.DataTextField = "NO_ALU";
                drpPacienteGuia.DataValueField = "CO_ALU";
                drpPacienteGuia.DataSource = res;
                drpPacienteGuia.DataBind();
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
            int itens = 0;
            int iCort = 0;
            //decimal VlDesconto = 0;
            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                //Coleta os valores da linha
                string Valor, Item;
                bool Homol, Cort;
                Valor = ((TextBox)li.FindControl("txtValorTotalOrc")).Text;
                Item = ((TextBox)li.FindControl("txtQtdProcedOrc")).Text;
                Homol = ((CheckBox)li.FindControl("chkHomolItem")).Checked;
                Cort = ((CheckBox)li.FindControl("chkCortItem")).Checked;

                if (Cort)
                    iCort++;

                if (Homol)
                {
                    //Soma os valores com os valores das outras linhas da grid
                    if (!Cort)
                        VlTotal += (!string.IsNullOrEmpty(Valor) ? decimal.Parse(Valor) : 0);
                    itens += (!string.IsNullOrEmpty(Item) ? int.Parse(Item) : 0);
                }
            }

            //Debita o valor do desconto
            //VlTotal -= VlDesconto;

            //Seta os valores nos textboxes
            txtVlTotalOrcam.Text = VlTotal.ToString("N2");
            txtItensTotalOrcam.Text = itens.ToString();
            txtCortTotalOcam.Text = iCort.ToString();
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

            CarregarProcedimentosAprovados(CO_ALU);

            grdProcedOrcam.DataSource = null;
            grdProcedOrcam.DataBind();

            carregaGridOrcamento(ID_AGEND_HORAR);
        }

        private void CarregarProcedimentosPlanejamento(int coAlu)
        {
            var res = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                       where tbs390.TBS174_AGEND_HORAR.CO_ALU == coAlu
                       && (tbs390.FL_SITU_FATU == "A" && tbs396.CO_SITU != "C")
                       select (new
                       {
                           CO_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                           DE_OBSER = tbs396.DE_OBSER,
                           ID_ATEND_AGEND = tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND,
                           ID_ATEND_ORCAM = tbs396.ID_ATEND_ORCAM,
                           ID_PROC_MEDI_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           NO_PROCE = tbs396.TBS356_PROC_MEDIC_PROCE.NM_REDUZ_PROC_MEDI,
                           NU_DENTE = tbs396.NU_DENTE,
                           SITUACAO = tbs396.CO_SITU
                       })).OrderBy(x => x.NO_PROCE).ToList();

            var listPlanejamento = new List<Planejamento>();
            if (res.Count > 0)
            {
                foreach (var item in res)
                {
                    var plan = new Planejamento();
                    plan.CO_PROCE = item.CO_PROCE;
                    plan.DE_OBSER = item.DE_OBSER;
                    plan.ID_ATEND_AGEND = item.ID_ATEND_AGEND;
                    plan.ID_ATEND_ORCAM = item.ID_ATEND_ORCAM;
                    plan.ID_PROC_MEDI_PROCE = item.ID_PROC_MEDI_PROCE;
                    plan.NO_PROCE = item.NO_PROCE;
                    plan.NU_DENTE = item.NU_DENTE;

                    var tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == item.ID_ATEND_ORCAM).FirstOrDefault();
                    plan.ID_TRATA_PLANO = tbs458 != null ? tbs458.ID_TRATA_PLANO : (int?)null;

                    if ((tbs458 == null ||

                        (tbs458 != null && !tbs458.CO_SITUA.Equals("F"))))
                    {
                        listPlanejamento.Add(plan);
                    }
                }
            }

            grdPlanejamento.DataSource = listPlanejamento;
            grdPlanejamento.DataBind();
        }

        public class Planejamento
        {
            public int? ID_TRATA_PLANO { get; set; }
            public int ID_ATEND_ORCAM { get; set; }
            public int ID_PROC_MEDI_PROCE { get; set; }
            public int ID_ATEND_AGEND { get; set; }
            public string CO_PROCE { get; set; }
            public string NO_PROCE { get; set; }
            public int? NU_DENTE { get; set; }
            public string DE_OBSER { get; set; }
            public bool IS_CHECK_PLAN { get { return this.ID_TRATA_PLANO.HasValue ? true : false; } }
        }

        private void CarregarAgendaPlanejamento(int coALu)
        {
            DateTime dtIni = DateTime.Now.Date;
            DateTime dtFim = DateTime.Now.Date.AddDays(30);
            int responsavel = !string.IsNullOrEmpty(drpProfResp.SelectedValue) ? int.Parse(drpProfResp.SelectedValue) : 0;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where (tbs174.CO_COL == responsavel && tbs174.CO_ALU == null && tbs174.CO_SITUA_AGEND_HORAR.Equals("A"))
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)))
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       select (new AgendaPlanejamento
                       {
                           CO_COL = tbs174.CO_COL.Value,
                           DT_ATEND_HORAR = tbs174.DT_AGEND_HORAR,
                           HR_ATEND_HORAR = tbs174.HR_AGEND_HORAR,
                           ID_ATEND_HORAR = tbs174.ID_AGEND_HORAR,
                           CO_DEPT = tbs174.CO_DEPT.HasValue ? tbs174.CO_DEPT.Value : 0,
                           CK_ATEND_HORAR = false,
                           Tipo = tbs174.TP_AGEND_HORAR
                       })).Concat(from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                  where (tbs174.CO_COL == responsavel && tbs174.CO_ALU == coALu && tbs174.CO_SITUA_AGEND_HORAR.Equals("A"))
                                  && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)))
                                  && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                                  select (new AgendaPlanejamento
                                  {
                                      CO_COL = tbs174.CO_COL.Value,
                                      DT_ATEND_HORAR = tbs174.DT_AGEND_HORAR,
                                      HR_ATEND_HORAR = tbs174.HR_AGEND_HORAR,
                                      ID_ATEND_HORAR = tbs174.ID_AGEND_HORAR,
                                      CO_DEPT = tbs174.CO_DEPT.HasValue ? tbs174.CO_DEPT.Value : 0,
                                      CK_ATEND_HORAR = true,
                                      Tipo = tbs174.TP_AGEND_HORAR
                                  })).ToList();

            var listAgenda = new List<AgendaPlanejamento>();
            foreach (var item in res)
            {
                var tbs390 = TBS390_ATEND_AGEND.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == item.ID_ATEND_HORAR).FirstOrDefault();
                var tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == item.ID_ATEND_HORAR).FirstOrDefault();
                bool ok = tbs458 != null && tbs458.CO_SITUA.Equals("F") || tbs390 != null ? false : true;
                if (ok)
                {
                    var ap = new AgendaPlanejamento();
                    ap.CK_ATEND_HORAR = item.CK_ATEND_HORAR;
                    ap.CO_COL = item.CO_COL;
                    ap.CO_DEPT = item.CO_DEPT;
                    ap.DT_ATEND_HORAR = item.DT_ATEND_HORAR;
                    ap.HR_ATEND_HORAR = item.HR_ATEND_HORAR;
                    ap.ID_ATEND_HORAR = item.ID_ATEND_HORAR;
                    ap.Tipo = item.Tipo;
                    listAgenda.Add(ap);
                }
            }

            grdAgendaPlanejamento.DataSource = listAgenda.OrderBy(x => x.DT_ATEND_HORAR).ThenBy(x => x.HR_ATEND_HORAR);
            grdAgendaPlanejamento.DataBind();
            foreach (GridViewRow row in grdAgendaPlanejamento.Rows)
            {
                if (((CheckBox)row.Cells[0].FindControl("chkAgendaPlanejamento")).Checked)
                {
                    row.BackColor = ColorTranslator.FromHtml("#ffe4c4");
                }
            }
        }

        public class AgendaPlanejamento
        {
            public bool CK_ATEND_HORAR { get; set; }
            public int ID_ATEND_HORAR { get; set; }
            public int CO_COL { get; set; }
            public DateTime DT_ATEND_HORAR { get; set; }
            public string DATA_AGENDA { get { return this.DT_ATEND_HORAR.ToShortDateString(); } }
            public string HR_ATEND_HORAR { get; set; }
            public int CO_DEPT { get; set; }
            public string LOCAL { get { return this.CO_DEPT > 0 ? TB14_DEPTO.RetornaPelaChavePrimaria(CO_DEPT).NO_DEPTO : "NR"; } }
            public string Tipo { get; set; }
            public string tpAgenda
            {
                get
                {
                    switch (this.Tipo)
                    {
                        case "O":
                            return "Outros";
                        case "P":
                            return "Procedimento";
                        case "V":
                            return "Vacina";
                        case "C":
                            return "Cirúrgia";
                        case "E":
                            return "Exame";
                        case "R":
                            return "Retorno";
                        case "N":
                            return "Normal";
                        default:
                            return "Sem registro";
                    }

                }
            }
        }

        private void CarregarProntuarioOdonto(int coAlu, DateTime dtIni, DateTime dtFim)
        {
            var res = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                       where tbs390.TBS174_AGEND_HORAR.CO_ALU == coAlu
                       && (tbs390.FL_SITU_FATU == "A" || tbs390.FL_SITU_FATU == "F")
                       && (tbs396.DT_CADAS >= dtIni && tbs396.DT_CADAS <= dtFim)
                       select new
                       {
                           nuDente = tbs396.NU_DENTE,
                           dtOrcamento = tbs396.DT_CADAS,
                           IdAtendOrcam = tbs396.ID_ATEND_ORCAM,
                           IdAtendAgend = tbs390.ID_ATEND_AGEND,
                           IdAgendHorar = tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                           dtAgenda = tbs396.DT_CADAS,
                           Profissional = tb03 != null ? tb03.NO_APEL_COL : " - ",
                           Procedimento = tbs396.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           idProcedimento = tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           Local = tbs390.TB14_DEPTO != null ? tbs390.TB14_DEPTO.NO_DEPTO : "-"
                       }).ToList();

            var listProc = new List<saidaProntuarioOdonto>();
            foreach (var r in res)
            {
                int idtbs174 = !string.IsNullOrEmpty(hidIdAgenda.Value) ? TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).ID_AGEND_HORAR : 0;
                var _tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == r.IdAtendOrcam).FirstOrDefault();
                var p = new saidaProntuarioOdonto();
                p.dtOrcamento = r.dtOrcamento;
                p.dtAgenda = r.dtAgenda;
                p.IdAtendAgend = r.IdAtendAgend;
                p.IdProcMedicProce = r.idProcedimento;
                p.IdTrataPlano = _tbs458 != null ? _tbs458.ID_TRATA_PLANO : 0;
                p.imagem_URL_ACAO = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? "/Library/IMG/PGS_IC_Positivo.png" : "/Library/IMG/PGS_IC_Negativo.png";
                p.nomeProcedimento = _tbs458 != null ? r.Procedimento : "*" + r.Procedimento;
                p.nomeProfissional = r.Profissional;
                p.IdAtendHorar = idtbs174;
                p.dtRealizado = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? _tbs458.DT_SITUA.Value : (DateTime?)null;
                p.IdAtendOrcam = r.IdAtendOrcam;
                p.nuDente = r.nuDente.HasValue ? r.nuDente.Value : 0;
                p.LOCAL = r.Local;
                listProc.Add(p);
            }

            grdProntuarioOdonto.DataSource = listProc.OrderBy(w => w.dtAgenda).ToList();
            grdProntuarioOdonto.DataBind();
        }

        public class saidaProntuarioOdonto
        {
            public int? nuDente { get; set; }
            public int IdAtendOrcam { get; set; }
            public int IdAtendHorar { get; set; }
            public int IdAtendAgend { get; set; }
            public int IdProcMedicProce { get; set; }
            public int IdTrataPlano { get; set; }
            public string nomeProcedimento { get; set; }
            public string nomeProfissional { get; set; }
            public DateTime dtAgenda { get; set; }
            public DateTime dtOrcamento { get; set; }
            public string DT_ORCAMENTO { get { return this.dtOrcamento.ToString("dd/MM/yyyy HH:mm"); } }
            public DateTime? dtRealizado { get; set; }
            public string DT_REALIZADO { get { return this.dtRealizado.HasValue ? this.dtRealizado.Value.ToString("dd/MM/yyyy HH:mm") : ""; } }
            public string imagem_URL_ACAO { get; set; }
            public string LOCAL { get; set; }
        }

        #endregion

        #region Funções de Campo

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
            LimparGridsAgendamento();

            grdProcedAprovados.DataSource = null;
            grdProcedAprovados.DataBind();

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.ClientID == atual.ClientID)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);

                    var tbs390 = TBS390_ATEND_AGEND.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == tbs174.ID_AGEND_HORAR).FirstOrDefault();

                    ddlLocal.SelectedValue = tbs390 != null && tbs390.TB14_DEPTO != null ? tbs390.TB14_DEPTO.CO_DEPTO.ToString() : tbs174.CO_DEPT.HasValue ? tbs174.CO_DEPT.Value.ToString() : "";

                    if (tbs174.CO_SITUA_AGEND_HORAR == "A" && !String.IsNullOrEmpty(tbs174.FL_AGEND_ENCAM) && tbs174.FL_AGEND_ENCAM != "N")
                    {
                        if ((atual.Checked && tbs174.FL_AGEND_ENCAM == "S") || (!atual.Checked && tbs174.FL_AGEND_ENCAM == "A"))
                            hidAgendSelec.Value = idAgenda.ToString();
                        else
                            hidAgendSelec.Value = "";

                        if (tbs174.FL_AGEND_ENCAM == "S")
                            lblConfEncam.Text = "Deseja encaminhar o paciente para atendimento?";
                        else if (tbs174.FL_AGEND_ENCAM == "A")
                            lblConfEncam.Text = "Deseja retornar a situação do paciente para encaminhado?";
                    }
                    else
                        hidAgendSelec.Value = "";

                    if (!(chk.Checked && tbs174.CO_SITUA_AGEND_HORAR == "A" && tbs174.FL_AGEND_ENCAM == "A"))
                        ExecutarFuncaoPadrao("ZerarCronometro();");

                    if (chk.Checked)
                    {
                        CarregaAgendaPlanejamento(tbs174.CO_ALU.Value);

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
                    {
                        hidIdAgenda.Value = "";
                    }

                    CarregarProcedimentosAprovados(tbs174.CO_ALU.Value);
                }
                else
                {
                    chk.Checked = false;
                }
            }

            if (!String.IsNullOrEmpty(hidAgendSelec.Value))
                AbreModalPadrao("AbreModalEncamAtend();");
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
                    tbs174.CO_COL_ATEND = int.Parse(drpProfResp.SelectedValue);
                    tbs174.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue)).CO_EMP;
                    tbs174.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                    tbs174.IP_ATEND = Request.UserHostAddress;
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
            grdHistoricoAgenda.DataSource =
            grdProcedAprovados.DataSource = null;
            grdHistoricoAgenda.DataBind();
            grdProcedAprovados.DataBind();
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

        #region Anamnese Rápida

        protected void btnAnamRapida_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                try
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                    var tbs459 = TBS459_ATEND_ANAMN_RAPID.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == tbs390.ID_ATEND_AGEND).FirstOrDefault();
                    if (tbs459 == null)
                    {
                        AbreModalPadrao("AbreModalAnamRApida();");
                    }
                    else
                    {
                        txtAnamRapMotivoCosulta.Value = tbs459.DE_MOTIV;
                        chkAnamRapRoerUnha.Checked = tbs459.FL_ROER_UNHA.Value ? true : false;
                        chkAnamRapRangerDente.Checked = tbs459.FL_RANGE_DENTE.Value ? true : false;
                        chkAnamRapMauHalito.Checked = tbs459.FL_MAU_HALIT.Value ? true : false;
                        chkAnamRapAcumAlimenDente.Checked = tbs459.FL_ACUMU_ALIME_ENTRE_DENTE.Value ? true : false;
                        chkAnamRapATM.Checked = tbs459.FL_ESTAL_DOR_ARTIC_MANDI_ATM.Value ? true : false;
                        chkAnamRapApertaDente.Checked = tbs459.FL_APERT_DENTE.Value ? true : false;
                        chkAnamRapSangraDente.Checked = tbs459.FL_SANGR_DENTE.Value ? true : false;
                        chkAnamRapTartaro.Checked = tbs459.FL_TARTA.Value ? true : false;
                        chkAnamRapZumbidoOuvido.Checked = tbs459.FL_ZUMBI_OUVID.Value ? true : false;
                        ddlTrataOdonto.SelectedValue = tbs459.ULTIM_TRATA_ODONT.HasValue ? tbs459.ULTIM_TRATA_ODONT.Value.ToString() : "";
                        chkAnamRapGengiva.Checked = tbs459.FL_MELHO_GENGI.Value ? true : false;
                        chkAnamRapRestauraMetalica.Checked = tbs459.FL_MELHO_RESTA_METAL.Value ? true : false;
                        chkAnamRapFormaDente.Checked = tbs459.FL_MELHO_FORMA_DENTE.Value ? true : false;
                        chkAnamRapRestauraAntiga.Checked = tbs459.FL_MELHO_RESTA_ANTIG.Value ? true : false;
                        chkAnamRapCorDente.Checked = tbs459.FL_MELHO_COR_DENTE.HasValue ? true : false;
                        chkAnamRapImplanteDente.Checked = tbs459.FL_MELHO_IMPLA_DENTE.Value ? true : false;
                        chkAnamRapProtese.Checked = tbs459.FL_MELHO_PROTE.Value ? true : false;
                        txtAnamRapObsMelhorAtendido.Value = tbs459.DE_OBSER_MELHO_ATEND;
                        ddlEstadoSaude.SelectedValue = tbs459.ESTAD_SAUDE_ATUAL.HasValue ? tbs459.ESTAD_SAUDE_ATUAL.Value.ToString() : "";
                        if (tbs459.FL_FUMAR.HasValue)
                        {
                            ddlFuma.SelectedValue = tbs459.FL_FUMAR.Value ? "1" : "0";
                        }
                        if (tbs459.CUIDA_MEDIC_ATUAL.HasValue)
                        {
                            AnamRapCuidadoMedico.SelectedValue = tbs459.CUIDA_MEDIC_ATUAL.Value ? "1" : "0";
                        }
                        if (tbs459.FL_BEBER.HasValue)
                        {
                            ddlBebe.SelectedValue = tbs459.FL_BEBER.Value ? "1" : "0";
                        }
                        if (tbs459.TRATA_MEDIC_ATUAL.HasValue)
                        {
                            AnamRapTrata.SelectedValue = tbs459.TRATA_MEDIC_ATUAL.Value ? "1" : "0";
                            txtAnamRapTrataSim.Enabled = tbs459.TRATA_MEDIC_ATUAL.Value ? true : false;
                        }
                        txtAnamRapTrataSim.Text = tbs459.DE_TRATA_MEDIC_MOTIV;                        
                        if (tbs459.ULTIM_TRATA_MEDIC.HasValue)
                        {
                            AnamRapTrata.SelectedValue = tbs459.ULTIM_TRATA_MEDIC.Value.ToString();
                        }
                        chkAnamRapArticulares.Checked = tbs459.FL_TEVE_DOENC_ARTIC.Value ? true : false;
                        chkAnamRapCardioVasculares.Checked = tbs459.FL_TEVE_DOENC_CARDI_VASCU.Value ? true : false;
                        chkAnamRapEndocrinas.Checked = tbs459.FL_TEVE_DOENC_ENDOC.Value ? true : false;
                        chkAnamRapGastricas.Checked = tbs459.FL_TEVE_DOENC_GASTR.Value ? true : false;
                        chkAnamRapHepaticas.Checked = tbs459.FL_TEVE_DOENC_HEPAT.Value ? true : false;
                        chkAnamRapNeurologicas.Checked = tbs459.FL_TEVE_DOENC_NEURO.Value ? true : false;
                        chkAnamRapPulmonares.Checked = tbs459.FL_TEVE_DOENC_PULMO.Value ? true : false;
                        chkAnamRapRenais.Checked = tbs459.FL_TEVE_DOENC_RENAI.Value ? true : false;
                        chkAnamRapSanguineas.Checked = tbs459.FL_TEVE_DOENC_SENGU.Value ? true : false;
                        chkAnamRapNenhumaDoenca.Checked = tbs459.FL_TEVE_DOENC_NENHU.Value ? true : false;
                        chkAnamRapOutrasDoenca.Checked = tbs459.FL_TEVE_DOENC_OUTRA.Value ? true : false;
                        if (chkAnamRapOutrasDoenca.Checked)
                        {
                            txtAnamRapDeOutrasDoenca.Enabled = true;
                        }
                        txtAnamRapDeOutrasDoenca.Text = tbs459.DE_TEVE_DOENC;
                        if (tbs459.FL_FAZ_USO_MEDIC.HasValue)
                        {
                            AnamRapTrataMedic.SelectedValue = tbs459.FL_FAZ_USO_MEDIC.Value ? "1" : "0";
                        }
                        if (tbs459.FL_FAZ_USO_MEDIC.HasValue)
                        {
                            AnamRapMedicAtual.SelectedValue = tbs459.FL_FAZ_USO_MEDIC.Value ? "1" : "0";
                            txtAnamRapMedicAtualQual.Enabled = tbs459.FL_FAZ_USO_MEDIC.Value ? true : false;
                        }
                        txtAnamRapMedicAtualQual.Text = tbs459.DE_FAZ_USO_MEDIC;
                        chkAnamRapAlergAnestesico.Checked = tbs459.FL_ALERG_ANEST.Value ? true : false;
                        chkAnamRapAlergAntibiotico.Checked = tbs459.FL_ALERG_ANTIB.Value ? true : false;
                        chkAnamRapAlergIodo.Checked = tbs459.FL_ALERG_IODO.Value ? true : false;
                        chkAnamRapAlergNenhum.Checked = tbs459.FL_ALERG_NENHU.Value ? true : false;
                        chkAnamRapAlergOutros.Checked = tbs459.FL_ALERG_OUTRO.Value ? true : false;
                        if (chkAnamRapAlergOutros.Checked)
                        {
                            txtAnamRapAlerg.Enabled = true;   
                        }
                        txtAnamRapAlerg.Text = tbs459.DE_ALERG_MEDIC;
                        //PARA MULHERES
                        if (tbs459.MENSTR_REGUL.HasValue)
                        {
                            AnamRapMenstrua.SelectedValue = tbs459.MENSTR_REGUL.Value.ToString();
                        }
                        if (tbs459.FL_ALTER_GENGI_MENSTR.HasValue)
                        {
                            AnamRapMenstruaAlterGengiva.SelectedValue = tbs459.FL_ALTER_GENGI_MENSTR.Value ? "1" : "0";
                        }
                        if (tbs459.FL_GRAVI.HasValue)
                        {
                            AnamRapGravida.SelectedValue = tbs459.FL_GRAVI.Value ? "1" : "0";
                        }
                        if (tbs459.FL_UTILI_ANTI_CONCE.HasValue)
                        {
                            AnamRapAntiConcepcional.SelectedValue = tbs459.FL_UTILI_ANTI_CONCE.Value ? "1" : "0";
                            txAnamRapAntiConcepcionalNome.Enabled = tbs459.FL_UTILI_ANTI_CONCE.Value ? true : false;
                            RapAntiConcepcionalMotivo.Enabled = tbs459.FL_UTILI_ANTI_CONCE.Value ? true : false;
                        }
                        txAnamRapAntiConcepcionalNome.Text = tbs459.DE_UTILI_ANTI_CONCE;
                        if (tbs459.MOTIV_UTILI_ANTI_CONCE.HasValue)
                        {
                            RapAntiConcepcionalMotivo.SelectedValue = tbs459.MOTIV_UTILI_ANTI_CONCE.Value.ToString();
                        }
                        AbreModalPadrao("AbreModalAnamRApida();");
                    }
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento selecionado.");
                return;
            }

        }

        protected void btnSalvarAnamRap_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hidIdAtendimento.Value))
                {
                    int idAtendAgend = int.Parse(hidIdAtendimento.Value);
                    var _tbs459 = TBS459_ATEND_ANAMN_RAPID.RetornaTodosRegistros().Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == idAtendAgend).FirstOrDefault();
                    var tbs459 = _tbs459 != null ? _tbs459 : new TBS459_ATEND_ANAMN_RAPID();
                    if (_tbs459 == null)
                    {
                        var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                        tbs459.TBS390_ATEND_AGEND = tbs390;
                        tbs459.DT_CADAS = DateTime.Now;
                        tbs459.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs459.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs459.IP_CADAS = Request.UserHostName;
                    }
                    tbs459.DT_SITUA = DateTime.Now;
                    tbs459.IP_SITUA = Request.UserHostName;
                    tbs459.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs459.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs459.DE_MOTIV = txtAnamRapMotivoCosulta.Value;
                    tbs459.FL_ROER_UNHA = chkAnamRapRoerUnha.Checked ? true : false;
                    tbs459.FL_RANGE_DENTE = chkAnamRapRangerDente.Checked ? true : false;
                    tbs459.FL_MAU_HALIT = chkAnamRapMauHalito.Checked ? true : false;
                    tbs459.FL_ACUMU_ALIME_ENTRE_DENTE = chkAnamRapAcumAlimenDente.Checked ? true : false;
                    tbs459.FL_ESTAL_DOR_ARTIC_MANDI_ATM = chkAnamRapATM.Checked ? true : false;
                    tbs459.FL_APERT_DENTE = chkAnamRapApertaDente.Checked ? true : false;
                    tbs459.FL_SANGR_DENTE = chkAnamRapSangraDente.Checked ? true : false;
                    tbs459.FL_TARTA = chkAnamRapTartaro.Checked ? true : false;
                    tbs459.FL_ZUMBI_OUVID = chkAnamRapZumbidoOuvido.Checked ? true : false;
                    if (!string.IsNullOrEmpty(ddlTrataOdonto.SelectedValue))
                    {
                        tbs459.ULTIM_TRATA_ODONT = int.Parse(ddlTrataOdonto.SelectedValue) == (int)TratamentoTempo.menosDe01Ano ? (int)TratamentoTempo.menosDe01Ano : int.Parse(ddlTrataOdonto.SelectedValue) == (int)TratamentoTempo.de01A03Anos ? (int)TratamentoTempo.de01A03Anos : int.Parse(ddlTrataOdonto.SelectedValue) == (int)TratamentoTempo.maisDe03Anos ? (int)TratamentoTempo.maisDe03Anos : (int?)null;
                        tbs459.DE_ULTIM_TRATA_ODONT = int.Parse(ddlTrataOdonto.SelectedValue) == (int)TratamentoTempo.menosDe01Ano ? TratamentoTempo.menosDe01Ano.ToString() : int.Parse(ddlTrataOdonto.SelectedValue) == (int)TratamentoTempo.de01A03Anos ? TratamentoTempo.de01A03Anos.ToString() : int.Parse(ddlTrataOdonto.SelectedValue) == (int)TratamentoTempo.maisDe03Anos ? TratamentoTempo.maisDe03Anos.ToString() : null;
                    }
                    else
                    {
                        tbs459.ULTIM_TRATA_ODONT = (int?)null;
                        tbs459.DE_ULTIM_TRATA_ODONT = null;
                    }
                    tbs459.FL_MELHO_GENGI = chkAnamRapGengiva.Checked ? true : false;
                    tbs459.FL_MELHO_RESTA_METAL = chkAnamRapRestauraMetalica.Checked ? true : false;
                    tbs459.FL_MELHO_FORMA_DENTE = chkAnamRapFormaDente.Checked ? true : false;
                    tbs459.FL_MELHO_RESTA_ANTIG = chkAnamRapRestauraAntiga.Checked ? true : false;
                    tbs459.FL_MELHO_COR_DENTE = chkAnamRapCorDente.Checked ? true : false;
                    tbs459.FL_MELHO_IMPLA_DENTE = chkAnamRapImplanteDente.Checked ? true : false;
                    tbs459.FL_MELHO_PROTE = chkAnamRapProtese.Checked ? true : false;
                    tbs459.DE_OBSER_MELHO_ATEND = txtAnamRapObsMelhorAtendido.Value;
                    if (!string.IsNullOrEmpty(ddlEstadoSaude.SelectedValue))
                    {
                        tbs459.ESTAD_SAUDE_ATUAL = int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Pessimo ? (int)EstadoSaude.Pessimo : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Ruim ? (int)EstadoSaude.Ruim : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Regular ? (int)EstadoSaude.Regular : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Bom ? (int)EstadoSaude.Bom : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Otimo ? (int)EstadoSaude.Otimo : (int?)null;
                        tbs459.DE_ESTAD_SAUDE_ATUAL = int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Pessimo ? EstadoSaude.Pessimo.ToString() : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Ruim ? EstadoSaude.Ruim.ToString() : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Regular ? EstadoSaude.Regular.ToString() : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Bom ? EstadoSaude.Bom.ToString() : int.Parse(ddlEstadoSaude.SelectedValue) == (int)EstadoSaude.Otimo ? EstadoSaude.Otimo.ToString() : null;
                    }
                    else
                    {
                        tbs459.ESTAD_SAUDE_ATUAL = (int?)null;
                        tbs459.DE_ESTAD_SAUDE_ATUAL = null;
                    }
                    if (!string.IsNullOrEmpty(ddlFuma.SelectedValue))
                    {
                        tbs459.FL_FUMAR = ddlFuma.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.FL_FUMAR = (bool?)null;
                    }
                    if (!string.IsNullOrEmpty(AnamRapCuidadoMedico.SelectedValue))
                    {
                        tbs459.CUIDA_MEDIC_ATUAL = AnamRapCuidadoMedico.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.CUIDA_MEDIC_ATUAL = (bool?)null;
                    }
                    if (!string.IsNullOrEmpty(ddlBebe.SelectedValue))
                    {
                        tbs459.FL_BEBER = ddlBebe.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.FL_BEBER = (bool?)null;
                    }
                    if (!string.IsNullOrEmpty(AnamRapTrata.SelectedValue))
                    {
                        tbs459.TRATA_MEDIC_ATUAL = AnamRapTrata.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.TRATA_MEDIC_ATUAL = (bool?)null;
                    }
                    tbs459.DE_TRATA_MEDIC_MOTIV = txtAnamRapTrataSim.Text;
                    if (!string.IsNullOrEmpty(AnamRapTrata.SelectedValue))
                    {
                        tbs459.ULTIM_TRATA_MEDIC = int.Parse(AnamRapTrata.SelectedValue) == (int)TratamentoTempo.menosDe01Ano ? (int)TratamentoTempo.menosDe01Ano : int.Parse(AnamRapTrata.SelectedValue) == (int)TratamentoTempo.de01A03Anos ? (int)TratamentoTempo.de01A03Anos : int.Parse(AnamRapTrata.SelectedValue) == (int)TratamentoTempo.maisDe03Anos ? (int)TratamentoTempo.maisDe03Anos : (int?)null;
                        tbs459.DE_ULTIM_TRATA_MEDIC = int.Parse(AnamRapTrata.SelectedValue) == (int)TratamentoTempo.menosDe01Ano ? TratamentoTempo.menosDe01Ano.ToString() : int.Parse(AnamRapTrata.SelectedValue) == (int)TratamentoTempo.de01A03Anos ? TratamentoTempo.de01A03Anos.ToString() : int.Parse(AnamRapTrata.SelectedValue) == (int)TratamentoTempo.maisDe03Anos ? TratamentoTempo.maisDe03Anos.ToString() : null;
                    }
                    else
                    {
                        tbs459.ULTIM_TRATA_MEDIC = (int?)null;
                        tbs459.DE_ULTIM_TRATA_MEDIC = null;
                    }
                    tbs459.FL_TEVE_DOENC_ARTIC = chkAnamRapArticulares.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_CARDI_VASCU = chkAnamRapCardioVasculares.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_ENDOC = chkAnamRapEndocrinas.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_GASTR = chkAnamRapGastricas.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_HEPAT = chkAnamRapHepaticas.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_NEURO = chkAnamRapNeurologicas.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_PULMO = chkAnamRapPulmonares.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_RENAI = chkAnamRapRenais.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_SENGU = chkAnamRapSanguineas.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_NENHU = chkAnamRapNenhumaDoenca.Checked ? true : false;
                    tbs459.FL_TEVE_DOENC_OUTRA = chkAnamRapOutrasDoenca.Checked ? true : false;
                    tbs459.DE_TEVE_DOENC = txtAnamRapDeOutrasDoenca.Text;
                    if (!string.IsNullOrEmpty(AnamRapMedicAtual.SelectedValue))
                    {
                        tbs459.FL_FAZ_USO_MEDIC = AnamRapMedicAtual.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.FL_FAZ_USO_MEDIC = (bool?)null; 
                    }
                    tbs459.DE_FAZ_USO_MEDIC = txtAnamRapMedicAtualQual.Text;
                    tbs459.FL_ALERG_ANEST = chkAnamRapAlergAnestesico.Checked ? true : false;
                    tbs459.FL_ALERG_ANTIB = chkAnamRapAlergAntibiotico.Checked ? true : false;
                    tbs459.FL_ALERG_IODO = chkAnamRapAlergIodo.Checked ? true : false;
                    tbs459.FL_ALERG_NENHU = chkAnamRapAlergNenhum.Checked ? true : false;
                    tbs459.FL_ALERG_OUTRO = chkAnamRapAlergOutros.Checked ? true : false;
                    tbs459.DE_ALERG_MEDIC = txtAnamRapAlerg.Text;
                    //PARA MULHERES
                    if (!string.IsNullOrEmpty(AnamRapMenstrua.SelectedValue))
                    {
                        tbs459.MENSTR_REGUL    = int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.Sim ? (int)Menstruacao.Sim : int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.Não ? (int)Menstruacao.Não : int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.aindaNaoMenstrua ? (int)Menstruacao.aindaNaoMenstrua : int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.naoMenstruaMais ? (int)Menstruacao.naoMenstruaMais : (int?)null;
                        tbs459.DE_MENSTR_REGUL = int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.Sim ? Menstruacao.Sim.ToString() : int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.Não ? Menstruacao.Não.ToString() : int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.aindaNaoMenstrua ? Menstruacao.aindaNaoMenstrua.ToString() : int.Parse(AnamRapMenstrua.SelectedValue) == (int)Menstruacao.naoMenstruaMais ? Menstruacao.naoMenstruaMais.ToString() : null;
                    }
                    else
                    {
                        tbs459.MENSTR_REGUL = (int?)null; 
                        tbs459.DE_MENSTR_REGUL = null;
                    }
                    if (!string.IsNullOrEmpty(AnamRapMenstruaAlterGengiva.SelectedValue))
                    {
                        tbs459.FL_ALTER_GENGI_MENSTR = AnamRapMenstruaAlterGengiva.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.FL_ALTER_GENGI_MENSTR = (bool?)null;
                    }
                    if (!string.IsNullOrEmpty(AnamRapGravida.SelectedValue))
                    {
                        tbs459.FL_GRAVI = AnamRapGravida.SelectedValue.Equals("1") ? true : false;
                    }
                    else
                    {
                        tbs459.FL_GRAVI = (bool?)null;
                    }
                    if (!string.IsNullOrEmpty(AnamRapAntiConcepcional.SelectedValue))
                    {
                        tbs459.FL_UTILI_ANTI_CONCE = AnamRapAntiConcepcional.SelectedValue.Equals("1") ? true : false;
                        tbs459.DE_UTILI_ANTI_CONCE = txAnamRapAntiConcepcionalNome.Text;
                    }
                    else
                    {
                        tbs459.FL_UTILI_ANTI_CONCE = (bool?)null;
                        tbs459.DE_UTILI_ANTI_CONCE = null;
                    }
                    if (!string.IsNullOrEmpty(RapAntiConcepcionalMotivo.SelectedValue))
                    {
                        tbs459.MOTIV_UTILI_ANTI_CONCE = int.Parse(RapAntiConcepcionalMotivo.SelectedValue) == (int)MotivoAntiConcepcional.evitarGestacao ? (int)MotivoAntiConcepcional.evitarGestacao : int.Parse(RapAntiConcepcionalMotivo.SelectedValue) == (int)MotivoAntiConcepcional.controleHormonal ? (int)MotivoAntiConcepcional.controleHormonal : (int?)null;
                        tbs459.DE_MOTIV_UTILI_ANTI_CONCE = int.Parse(RapAntiConcepcionalMotivo.SelectedValue) == (int)MotivoAntiConcepcional.evitarGestacao ? MotivoAntiConcepcional.evitarGestacao.ToString() : int.Parse(RapAntiConcepcionalMotivo.SelectedValue) == (int)MotivoAntiConcepcional.controleHormonal ? MotivoAntiConcepcional.controleHormonal.ToString() : null;
                    }
                    else
                    {
                        tbs459.MOTIV_UTILI_ANTI_CONCE    = (int?)null;
                        tbs459.DE_MOTIV_UTILI_ANTI_CONCE = null;
                    }
                    TBS459_ATEND_ANAMN_RAPID.SaveOrUpdate(tbs459, true);
                    AuxiliPagina.EnvioMensagemSucesso(this.Page, "Anamnese salva com sucesso!");
                }
                else
                {
                    hidIdAnamneseRapida.Value = "";
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento selecionado.");
                    return;
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
        }

        protected void AnamRapTrata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnamRapTrata.SelectedValue.Equals("1"))
            {
                txtAnamRapTrataSim.Enabled = true;
            }
            else
            {
                txtAnamRapTrataSim.Enabled = false;
                txtAnamRapTrataSim.Text = "";
            }
            AbreModalPadrao("AbreModalAnamRApida();");
        }

        protected void chkAnamRapOutrasDoenca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnamRapOutrasDoenca.Checked)
            {
                txtAnamRapDeOutrasDoenca.Enabled = true;
            }
            else
            {
                txtAnamRapDeOutrasDoenca.Enabled = false;
                txtAnamRapDeOutrasDoenca.Text = "";
            }
            AbreModalPadrao("AbreModalAnamRApida();");
        }

        protected void AnamRapMedicAtual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnamRapMedicAtual.SelectedValue.Equals("1"))
            {
                txtAnamRapMedicAtualQual.Enabled = true;
            }
            else
            {
                txtAnamRapMedicAtualQual.Enabled = false;
                txtAnamRapMedicAtualQual.Text = "";
            }
            AbreModalPadrao("AbreModalAnamRApida();");
        }

        protected void chkAnamRapAlergOutros_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnamRapAlergOutros.Checked)
            {
                txtAnamRapAlerg.Enabled = true;
            }
            else
            {
                txtAnamRapAlerg.Enabled = false;
                txtAnamRapAlerg.Text = "";
            }
            AbreModalPadrao("AbreModalAnamRApida();");
        }

        protected void AnamRapAntiConcepcional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnamRapAntiConcepcional.SelectedValue.Equals("1"))
            {
                txAnamRapAntiConcepcionalNome.Enabled = true;
                RapAntiConcepcionalMotivo.Enabled = true;
            }
            else
            {
                txAnamRapAntiConcepcionalNome.Enabled = false;
                txAnamRapAntiConcepcionalNome.Text = "";
                RapAntiConcepcionalMotivo.Enabled = false;
                RapAntiConcepcionalMotivo.SelectedValue = "";
            }
            AbreModalPadrao("AbreModalAnamRApida();");
        }

        #endregion

        #region Cancelar Procedimento

        protected void imgCancelarProcedimento(object sender, EventArgs e)
        {
            TBS370_PLANE_AVALI tbs370 = null;
            TBS386_ITENS_PLANE_AVALI tbs386 = null;
            TBS356_PROC_MEDIC_PROCE tbs356 = null;
            var imgAtual = (ImageButton)sender;
            int coAlu = 0;
            try
            {
                foreach (GridViewRow row in grdProcedAprovados.Rows)
                {
                    if (((ImageButton)row.Cells[5].FindControl("imgCancelarProcedimento")).ClientID == imgAtual.ClientID)
                    {
                        int hidTrataPlano = int.Parse(((HiddenField)row.Cells[1].FindControl("hidIdTrataPlano")).Value);
                        int hidIdAtendOrcam = int.Parse(((HiddenField)row.Cells[1].FindControl("hidIdAtendOrcam")).Value);
                        int hidIdProcedimento = int.Parse(((HiddenField)row.Cells[1].FindControl("hidIdProcedimento")).Value);
                        var tbs396 = TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(hidIdAtendOrcam);
                        tbs396.TBS390_ATEND_AGENDReference.Load();
                        tbs396.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                        if (tbs396.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR != null)
                        {
                            coAlu = tbs396.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.CO_ALU.Value;
                        }
                        tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(hidIdProcedimento);
                        var tbs458 = TBS458_TRATA_PLANO.RetornaPelaChavePrimaria((hidTrataPlano));
                        if (tbs458 != null)
                        {
                            tbs458.TBS174_AGEND_HORARReference.Load();
                            //A - ativo, F - finalizado, C - cancelado
                            tbs458.CO_SITUA = "C";
                            tbs458.IP_SITUA = Request.UserHostName;
                            tbs458.DT_SITUA = DateTime.Now;
                            tbs458.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs458.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs458 = TBS458_TRATA_PLANO.SaveOrUpdate(tbs458);

                            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(tbs458.TBS174_AGEND_HORAR.ID_AGEND_HORAR);
                            tbs174.TBS370_PLANE_AVALIReference.Load();
                            int? ID_PLANE_AVALI = tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null;
                            tbs370 = RecuperaPlanejamento(ID_PLANE_AVALI, tbs174.CO_ALU.Value);
                            var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == tbs174.ID_AGEND_HORAR).ToList();
                            foreach (var item in tbs389)
                            {
                                item.TBS386_ITENS_PLANE_AVALIReference.Load();
                                var _tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros().Where(x => x.ID_ITENS_PLANE_AVALI == item.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI && x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE && x.DT_AGEND == tbs174.DT_AGEND_HORAR).FirstOrDefault();
                                if (_tbs386 != null)
                                {
                                    tbs386 = RecuperarItensPlanejamento(_tbs386.ID_ITENS_PLANE_AVALI, tbs370.ID_PLANE_AVALI, tbs356.ID_PROC_MEDI_PROCE, tbs174.ID_AGEND_HORAR, tbs396.ID_ATEND_ORCAM, true);
                                }
                            }
                        }
                        tbs396.CO_SITU = "C";
                        tbs396.FL_FATURADO = null;
                        TBS396_ATEND_ORCAM.SaveOrUpdate(tbs396, true);
                    }
                }
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Procedimento cancelado com sucesso!");
                if (coAlu > 0)
                {
                    CarregarProcedimentosAprovados(coAlu);
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }
        }

        #endregion

        #region Prontuario Odonto

        protected void lnkpProntuarioOdonto_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                tbs390.TB07_ALUNOReference.Load();
                txtDtInicialProntuarioOdonto.Text = DateTime.Now.Date.ToString();
                txtDtFinalProntuarioOdonto.Text = DateTime.Now.Date.AddDays(1).ToString();
                CarregarProntuarioOdonto(tbs390.TB07_ALUNO.CO_ALU, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
                string sexo = !string.IsNullOrEmpty(tbs390.TB07_ALUNO.CO_SEXO_ALU) && tbs390.TB07_ALUNO.CO_SEXO_ALU.Equals("F") ? "Sexo Feminino /" : tbs390.TB07_ALUNO.CO_SEXO_ALU.Equals("M") ? "Sexo Masculino /" : "Sexo não informado /";
                lblNomeModalProntOdonto.Text = tbs390.TB07_ALUNO.NO_ALU + " (" + tbs390.TB07_ALUNO.NU_NIRE.toNire() + ") / " + sexo + " Idade " + AuxiliFormatoExibicao.FormataDataNascimento(tbs390.TB07_ALUNO.DT_NASC_ALU, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto);
                AbreModalPadrao("AbreModalProntuarioOdonto();");
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento selecionado.");
                return;
            }
        }

        protected void imgPesqProcAcoesPO_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDtInicialProntuarioOdonto.Text) || !string.IsNullOrEmpty(txtDtFinalProntuarioOdonto.Text))
            {
                try
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                    tbs390.TB07_ALUNOReference.Load();
                    DateTime dtIni = DateTime.Parse(txtDtInicialProntuarioOdonto.Text);
                    DateTime dtFim = DateTime.Parse(txtDtFinalProntuarioOdonto.Text);
                    CarregarProntuarioOdonto(tbs390.TB07_ALUNO.CO_ALU, dtIni, dtFim);
                    AbreModalPadrao("AbreModalProntuarioOdonto();");
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, informe uma data válida.");
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, informe uma data válida.");
                return;
            }
        }

        #endregion

        #region Planejamento

        protected void lnkPlanejamento_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                tbs390.TB07_ALUNOReference.Load();
                CarregarProcedimentosPlanejamento(tbs390.TB07_ALUNO.CO_ALU);
                tbs390.TBS174_AGEND_HORARReference.Load();
                CarregarAgendaPlanejamento(tbs390.TBS174_AGEND_HORAR.CO_ALU.Value);
                AbreModalPadrao("AbreModalPlanejamento();");
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento selecionado.");
                return;
            }
        }

        protected void chkPlanejamento_CheckedChanged(object sender, EventArgs e)
        {
            var chkAtual = (CheckBox)sender;
            int count = 0;
            int atendimento = 0;
            foreach (GridViewRow row in grdPlanejamento.Rows)
            {
                var isCheck = ((CheckBox)row.Cells[0].FindControl("chkPlanejamento"));
                if (isCheck.ClientID == chkAtual.ClientID && isCheck.Checked)
                {
                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                    tbs390.TBS174_AGEND_HORARReference.Load();
                    CarregarAgendaPlanejamento(tbs390.TBS174_AGEND_HORAR.CO_ALU.Value);
                    string idTrataPlano = ((HiddenField)row.Cells[0].FindControl("hidIdTrataPlano")).Value;
                    atendimento = int.Parse(((HiddenField)row.Cells[0].FindControl("hidIdAtendAgend")).Value);
                    if (!string.IsNullOrEmpty(idTrataPlano))
                    {
                        idProcMedi.Value = "";
                        var tbs458 = TBS458_TRATA_PLANO.RetornaPelaChavePrimaria(int.Parse(idTrataPlano));
                        tbs458.TBS396_ATEND_ORCAMReference.Load();
                        var _tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == tbs458.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM).ToList();
                        foreach (var item in _tbs458)
                        {
                            item.TBS174_AGEND_HORARReference.Load();
                            foreach (GridViewRow li in grdAgendaPlanejamento.Rows)
                            {
                                var hidTrataPlano = (HiddenField)li.Cells[0].FindControl("hidIdTrataPlano");
                                int hidIdAgendHorar = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdAtendHorar")).Value);
                                //var hidIdProcMediProceAgenda = ((HiddenField)li.Cells[0].FindControl("hidIdProcMediProceAgenda")).Value;
                                //int hidIdProcMediProceAgenda = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdProcMediProceAgenda")).Value);
                                //var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == item.TBS174_AGEND_HORAR.ID_AGEND_HORAR).ToList();
                                //List<int> idProc = new List<int>();
                                //foreach (var i in tbs389)
                                //{
                                //    i.TBS386_ITENS_PLANE_AVALIReference.Load(); 
                                //    idProc.Add(TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(i.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI).ID_ITENS_PLANE_AVALI);
                                //}
                                CheckBox chk = ((CheckBox)li.Cells[0].FindControl("chkAgendaPlanejamento"));
                                if (hidIdAgendHorar == item.TBS174_AGEND_HORAR.ID_AGEND_HORAR)
                                {
                                    hidTrataPlano.Value = tbs458.ID_TRATA_PLANO.ToString();
                                    li.BackColor = ColorTranslator.FromHtml("#ADD8E6");
                                    chk.Checked = true;
                                    count++;
                                    break;
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    idProcMedi.Value = ((HiddenField)row.Cells[0].FindControl("hidIdProcMediProce")).Value;
                    //}
                }
            }
            if (count == 0)
            {
                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                tbs390.TBS174_AGEND_HORARReference.Load();
                CarregarAgendaPlanejamento(tbs390.TBS174_AGEND_HORAR.CO_ALU.Value);
            }
            AbreModalPadrao("AbreModalPlanejamento();");
        }

        protected void chkAgendaPlanejamento_CheckedChanged(object sender, EventArgs e)
        {
            var chkAtual = (CheckBox)sender;
            foreach (GridViewRow row in grdAgendaPlanejamento.Rows)
            {
                var isCheck = ((CheckBox)row.Cells[0].FindControl("chkAgendaPlanejamento"));
                if (isCheck.ClientID == chkAtual.ClientID && isCheck.Checked)
                {
                    ((HiddenField)row.Cells[0].FindControl("hidIdProcMediProceAgenda")).Value = idProcMedi.Value;
                    row.BackColor = ColorTranslator.FromHtml("#ADD8E6");
                }
                else
                {
                    Color cor = ColorTranslator.FromHtml("#ADD8E6");
                    if (isCheck.Checked && row.BackColor == cor)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#ADD8E6");
                    }
                    else if (isCheck.Checked)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#ffe4c4");
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFF0");
                    }
                }
            }
            AbreModalPadrao("AbreModalPlanejamento();");
        }

        protected void btnSalvarAgendaPlanejamento_Click(object sender, EventArgs e)
        {
            try
            {
                Color cor = ColorTranslator.FromHtml("#ADD8E6");
                int coAlu = 0;
                foreach (GridViewRow li in grdAgendaPlanejamento.Rows)
                {
                    var chk = ((CheckBox)li.Cells[0].FindControl("chkAgendaPlanejamento"));
                    if (chk.Checked && li.BackColor == cor)
                    {
                        int agenda = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdAtendHorar")).Value);
                        var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(agenda);
                        var Tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(X => X.TBS174_AGEND_HORAR.ID_AGEND_HORAR == tbs174.ID_AGEND_HORAR).ToList();

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


                        tbs174.CO_SITUA_AGEND_HORAR = "A";

                        foreach (GridViewRow linha in grdPlanejamento.Rows)
                        {
                            var isChk = ((CheckBox)linha.Cells[0].FindControl("chkPlanejamento"));
                            if (isChk.Checked)
                            {
                                int _orcamento = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdAtendOrcam")).Value);
                                var _tbs396 = TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(_orcamento);
                                int _procedimento = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdProcMediProce")).Value);
                                var _tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(_procedimento);
                                string idTrataPlano = ((HiddenField)linha.Cells[0].FindControl("hidIdTrataPlano")).Value;

                                _tbs356.TB250_OPERAReference.Load();
                                _tbs396.TBS390_ATEND_AGENDReference.Load();
                                _tbs396.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();

                                var tbs458 = !string.IsNullOrEmpty(idTrataPlano) ? TBS458_TRATA_PLANO.RetornaPelaChavePrimaria(int.Parse(idTrataPlano)) : new TBS458_TRATA_PLANO();
                                if (string.IsNullOrEmpty(idTrataPlano))
                                {
                                    tbs458.CO_COL_CADAS = LoginAuxili.CO_COL;
                                    tbs458.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                    tbs458.DT_CADAS = DateTime.Now;
                                    tbs458.IP_CADAS = Request.UserHostName;

                                    #region Gera Código da Consulta

                                    string coUnid = LoginAuxili.CO_UNID.ToString();
                                    int coEmp = LoginAuxili.CO_EMP;
                                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                                    var res = (from tbs458pesq in TBS458_TRATA_PLANO.RetornaTodosRegistros()
                                               where tbs458pesq.NU_REGIS != null
                                               select new { tbs458pesq.NU_REGIS }).OrderByDescending(w => w.NU_REGIS).FirstOrDefault();

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
                                        seq = res.NU_REGIS.Substring(7, 7);
                                        seq2 = int.Parse(seq);
                                    }

                                    seqConcat = seq2 + 1;
                                    seqcon = seqConcat.ToString().PadLeft(7, '0');

                                    tbs458.NU_REGIS = ano + coUnid.PadLeft(3, '0') + "PL" + seqcon;
                                    #endregion
                                }
                                //A - ativo, F - finalizado, C - cancelado
                                tbs458.CO_SITUA = "A";
                                tbs458.IP_SITUA = Request.UserHostName;
                                tbs458.DT_SITUA = DateTime.Now;
                                tbs458.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs458.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs458.DE_OBSER = ((TextBox)linha.Cells[5].FindControl("txtObervacaoPlanejamento")).Text;
                                tbs458.TBS396_ATEND_ORCAM = _tbs396;
                                tbs458.TBS174_AGEND_HORAR = tbs174;
                                //procedimento
                                tbs174.TP_CONSU = "P";
                                tbs458 = TBS458_TRATA_PLANO.SaveOrUpdate(tbs458);

                                tbs174.CO_ALU = tbs174.CO_ALU.HasValue ? tbs174.CO_ALU.Value : _tbs396.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.CO_ALU.Value;
                                var tbs370 = RecuperaPlanejamento((int?)null, tbs174.CO_ALU.Value);
                                var tbs386 = RecuperarItensPlanejamento((int?)null, tbs370.ID_PLANE_AVALI, _tbs356.ID_PROC_MEDI_PROCE, tbs174.ID_AGEND_HORAR, _tbs396.ID_ATEND_ORCAM);
                                var tbs389 = RecuperarAssocItensPlaneAgend(tbs174, tbs386);
                                tbs174.TBS370_PLANE_AVALI = tbs370;
                                tbs174.TBS386_ITENS_PLANE_AVALI = tbs386;
                                tbs174.TBS356_PROC_MEDIC_PROCE = _tbs356;
                                tbs174.TB250_OPERA = _tbs356.TB250_OPERA != null ? _tbs356.TB250_OPERA : null;
                                tbs174.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(_procedimento);
                                coAlu = tbs174.CO_ALU.Value;
                            }
                        }
                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174);
                    }
                }
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Tratamento salvo com sucesso!");
                if (coAlu > 0)
                {
                    CarregarProcedimentosAprovados(coAlu);
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Naõ foi possível efetuar o registro, por favor, tente novamente ou entre em contato com o suporte. " + ex.Message);
                return;
            }
        }

        private TBS370_PLANE_AVALI RecuperaPlanejamento(int? ID_PLANE_AVALI, int CO_ALU)
        {
            if (ID_PLANE_AVALI.HasValue)
                return TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(ID_PLANE_AVALI.Value);
            else // Já que não tem ainda, cria um novo planejamento e retorna um objeto do mesmo no método
            {
                TBS370_PLANE_AVALI tbs370 = new TBS370_PLANE_AVALI();
                tbs370.CO_ALU = CO_ALU;

                //Dados do cadastro
                tbs370.DT_CADAS = DateTime.Now;
                tbs370.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs370.IP_CADAS = Request.UserHostAddress;

                //Dados da situação
                tbs370.CO_SITUA = "A";
                tbs370.DT_SITUA = DateTime.Now;
                tbs370.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs370.IP_SITUA = Request.UserHostAddress;
                TBS370_PLANE_AVALI.SaveOrUpdate(tbs370);

                return tbs370;
            }
        }

        private TBS386_ITENS_PLANE_AVALI RecuperarItensPlanejamento(int? ID_ITENS_PLANE_AVALI, int ID_PLANE_AVALI, int ID_PROC_MEDI_PROCE, int? ID_AGEND_HORAR, int ID_ATEND_ORCAM, bool Cancelar = false)
        {
            if (ID_ITENS_PLANE_AVALI.HasValue)
            {
                var res = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(ID_ITENS_PLANE_AVALI.Value);

                if (Cancelar)
                {
                    var TBS389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == ID_ITENS_PLANE_AVALI.Value).FirstOrDefault();
                    int idTbs174 = TBS389.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
                    var _TBS389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idTbs174).ToList();
                    if (_TBS389.Count == 1)
                    {
                        var _tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idTbs174);
                        _tbs174.CO_SITUA_AGEND_HORAR = "C";
                        _tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;
                        _tbs174.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                        _tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        _tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                        TBS174_AGEND_HORAR.SaveOrUpdate(_tbs174, true);
                    }
                    res.CO_SITUA = "C";
                    res.CO_COL_SITUA = LoginAuxili.CO_COL;
                    res.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
                    res.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    res.DT_SITUA = DateTime.Now;
                    res.IP_SITUA = Request.UserHostName;
                    TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(res);
                }
                return res;
            }
            else
            {
                var tbs396 = TBS396_ATEND_ORCAM.RetornaPelaChavePrimaria(ID_ATEND_ORCAM);
                var tbs174 = ID_AGEND_HORAR.HasValue ? TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGEND_HORAR.Value) : null;
                var tbs386 = new TBS386_ITENS_PLANE_AVALI();
                tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs386.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                tbs386.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
                tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs386.CO_SITUA = "A";
                tbs386.DT_CADAS = DateTime.Now;
                tbs386.DT_SITUA = DateTime.Now;
                tbs386.TBS370_PLANE_AVALI = TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(ID_PLANE_AVALI);
                tbs386.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(ID_PROC_MEDI_PROCE);
                tbs386.NR_ACAO = RecuperaUltimoNrAcao(ID_PLANE_AVALI);
                tbs386.QT_SESSO = ID_AGEND_HORAR.HasValue ? TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(tbs174.ID_AGEND_HORAR, ID_PROC_MEDI_PROCE).Count : 1; //Conta quantos itens existem na lista para este mesmo e agenda
                tbs386.DT_INICI = ID_AGEND_HORAR.HasValue ? tbs174.DT_AGEND_HORAR : DateTime.Now; //Verifica qual a primeira data na lista
                tbs386.DT_FINAL = ID_AGEND_HORAR.HasValue ? tbs174.DT_AGEND_HORAR : DateTime.Now; //Verifica qual a última data na lista
                tbs386.FL_AGEND_FEITA_PLANE = "N";
                tbs386.QT_PROCED = 1;
                tbs386.IP_CADAS = Request.UserHostName;
                tbs386.IP_SITUA = Request.UserHostName;
                tbs386.FL_CORTESIA = tbs386.FL_CORTESIA;

                //Data prevista é a data do agendamento associado
                tbs386.DT_AGEND = tbs174.DT_AGEND_HORAR;

                TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);
                return tbs386;
            }
        }

        private TBS389_ASSOC_ITENS_PLANE_AGEND RecuperarAssocItensPlaneAgend(TBS174_AGEND_HORAR tbs174, TBS386_ITENS_PLANE_AVALI tbs386)
        {
            TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == tbs386.ID_ITENS_PLANE_AVALI).FirstOrDefault();
            if (tbs389 == null)
            {
                tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();
                tbs389.TBS174_AGEND_HORAR = tbs174;
                tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
            }
            TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389);
            return tbs389;
        }

        private int RecuperaUltimoNrAcao(int ID_PLANE_AVALI)
        {
            var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                       where tbs389.TBS386_ITENS_PLANE_AVALI.TBS370_PLANE_AVALI.ID_PLANE_AVALI == ID_PLANE_AVALI
                       select new { tbs389.TBS386_ITENS_PLANE_AVALI.NR_ACAO }).OrderByDescending(w => w.NR_ACAO).FirstOrDefault();
            return (res != null ? res.NR_ACAO + 1 : 1);
        }

        #endregion

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
            TextBox txtOrcam;
            int idOrcam = 0;
            int aux = 0;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcOrc");
                    txtOrcam = ((TextBox)linha.FindControl("txtAtendOrcam"));

                    if (img.ClientID == atual.ClientID)
                    {
                        aux = linha.RowIndex;
                        idOrcam = !string.IsNullOrEmpty(txtOrcam.Text) ? int.Parse(txtOrcam.Text) : 0;
                    }
                }
            }
            ExcluiItemGridOrcamento(aux, idOrcam);

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void ddlOperOrc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;

            foreach (GridViewRow li in grdProcedOrcam.Rows)
            {
                DropDownList ddlOper = (DropDownList)li.FindControl("ddlOperOrc");
                DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanOrc");
                DropDownList ddlCodigoi = ((DropDownList)li.FindControl("ddlProcedOrc"));
                if (atual.ClientID == ddlOper.ClientID)
                {
                    CarregarPlanos(ddlPlan, ddlOper);
                    var opr = !String.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0;

                    var tb03 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);

                    var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                               where (opr != 0 ? tbs356.TB250_OPERA.ID_OPER == opr : 0 == 0)
                               && tbs356.CO_TIPO_PROC_MEDI != "EX"
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
                    }
                    break;
                }
            }

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
                    TextBox txtDesc, txtQnt, txtValorUnit, txtValorTotal;

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                        {
                            HiddenField hidValorUnitario = (HiddenField)linha.FindControl("hidValUnitProc");
                            //textbox que vai receber valor calculado
                            TextBox txtValor = (TextBox)linha.FindControl("txtValorProcedOrc");
                            TextBox txtQtd = (TextBox)linha.FindControl("txtQtdProcedOrc");
                            DropDownList ddlContrat = (DropDownList)linha.FindControl("ddlOperOrc");
                            DropDownList ddlPlano = (DropDownList)linha.FindControl("ddlPlanOrc");
                            CalcularPreencherValoresTabelaECalculado(ddl,
                                ddlContrat.SelectedValue,
                                ddlPlano.SelectedValue,
                                hidValorUnitario);

                            txtDesProced.Text = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue)).NM_PROC_MEDI;
                            CalculaValorTotalProcedimento((!string.IsNullOrEmpty(txtQtd.Text) ? int.Parse(txtQtd.Text) : 0),
                                hidValorUnitario.Value, txtValor);

                            txtDesc = (TextBox)linha.FindControl("txtValorDescOrc");
                            txtQnt = (TextBox)linha.FindControl("txtQtdProcedOrc");
                            txtValorUnit = (TextBox)linha.FindControl("txtValorProcedOrc");
                            txtValorTotal = (TextBox)linha.FindControl("txtValorTotalOrc");

                            int qnt = (String.IsNullOrEmpty(txtQnt.Text) ? 1 : int.Parse(txtQnt.Text));
                            decimal desc = (String.IsNullOrEmpty(txtDesc.Text) ? 0 : decimal.Parse(txtDesc.Text));
                            decimal unit = (String.IsNullOrEmpty(txtValorUnit.Text) ? 0 : decimal.Parse(txtValorUnit.Text));
                            decimal total = 0;

                            total = (unit * qnt) - desc;

                            txtValorTotal.Text = total.ToString();

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
            TextBox atual = (TextBox)sender;
            TextBox txt, txtQnt, txtValorUnit, txtValorTotal;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    txt = (TextBox)linha.FindControl("txtValorDescOrc");
                    txtQnt = (TextBox)linha.FindControl("txtQtdProcedOrc");
                    txtValorUnit = (TextBox)linha.FindControl("txtValorProcedOrc");
                    txtValorTotal = (TextBox)linha.FindControl("txtValorTotalOrc");

                    if (txt.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            int qnt = (String.IsNullOrEmpty(txtQnt.Text) ? 1 : int.Parse(txtQnt.Text));
                            decimal desc = (String.IsNullOrEmpty(txt.Text) ? 0 : decimal.Parse(txt.Text));
                            decimal unit = (String.IsNullOrEmpty(txtValorUnit.Text) ? 0 : decimal.Parse(txtValorUnit.Text));
                            decimal total = 0;

                            total = (unit * qnt) - desc;

                            txtValorTotal.Text = total.ToString();

                            CarregarValoresTotaisFooter();
                        }
                    }
                }
            }

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void chkHomolItem_OnCheckedChanged(object sender, EventArgs e)
        {
            CarregarValoresTotaisFooter();

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void chkCortItem_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    chk = (CheckBox)linha.FindControl("chkCortItem");
                    if (chk.ClientID == atual.ClientID)
                    {
                        TextBox txtValorProcedOrc = (TextBox)linha.FindControl("txtValorProcedOrc");
                        TextBox txtValorDescOrc = (TextBox)linha.FindControl("txtValorDescOrc");
                        TextBox txtValorTotalOrc = (TextBox)linha.FindControl("txtValorTotalOrc");

                        if (chk.Checked)
                        {
                            txtValorProcedOrc.Enabled = false;
                            txtValorDescOrc.Enabled = false;
                            txtValorTotalOrc.Enabled = false;
                        }
                        else
                        {
                            txtValorProcedOrc.Enabled = true;
                            txtValorDescOrc.Enabled = true;
                            txtValorTotalOrc.Enabled = true;
                        }
                    }
                }
                CarregarValoresTotaisFooter();
            }

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void txtValorProcedOrc_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt, txtQnt, txtValorDesc, txtValorTotal;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    txt = (TextBox)linha.FindControl("txtValorProcedOrc");
                    txtQnt = (TextBox)linha.FindControl("txtQtdProcedOrc");
                    txtValorDesc = (TextBox)linha.FindControl("txtValorDescOrc");
                    txtValorTotal = (TextBox)linha.FindControl("txtValorTotalOrc");

                    if (txt.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            int qnt = (String.IsNullOrEmpty(txtQnt.Text) ? 1 : int.Parse(txtQnt.Text));
                            decimal desc = (String.IsNullOrEmpty(txtValorDesc.Text) ? 0 : decimal.Parse(txtValorDesc.Text));
                            decimal unit = (String.IsNullOrEmpty(txt.Text) ? 0 : decimal.Parse(txt.Text));
                            decimal total = 0;

                            total = (unit * qnt) - desc;

                            txtValorTotal.Text = total.ToString();

                            CarregarValoresTotaisFooter();
                        }
                    }
                }
            }

            AbreModalPadrao("AbreModalOrcamentos();");
        }

        protected void txtQtdProcedOrc_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt, txtProced, txtValorDesc, txtValorTotal;
            if (grdProcedOrcam.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedOrcam.Rows)
                {
                    txt = (TextBox)linha.FindControl("txtQtdProcedOrc");
                    txtProced = (TextBox)linha.FindControl("txtValorProcedOrc");
                    txtValorDesc = (TextBox)linha.FindControl("txtValorDescOrc");
                    txtValorTotal = (TextBox)linha.FindControl("txtValorTotalOrc");
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

                            int qnt = (String.IsNullOrEmpty(txt.Text) ? 1 : int.Parse(txt.Text));
                            decimal desc = (String.IsNullOrEmpty(txtValorDesc.Text) ? 0 : decimal.Parse(txtValorDesc.Text));
                            decimal unit = (String.IsNullOrEmpty(txtProced.Text) ? 0 : decimal.Parse(txtProced.Text));
                            decimal total = 0;

                            total = (unit * qnt) - desc;

                            txtValorTotal.Text = total.ToString();

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
            txtObserExame.Text = hidObserExame.Value;

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
                    TextBox txtDesProced = (TextBox)linha.FindControl("txtCodigProcedPla");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                            txtDesProced.Text = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue)).NM_PROC_MEDI);
                        else
                            txtDesProced.Text = ""; // Limpa os dois campos caso esteja deselecionando o procedimento
                    }
                }
            }

            AbreModalPadrao("AbreModalExames();");
        }

        #endregion

        #region Medicamentos

        protected void lnkMedic_OnClick(object sender, EventArgs e)
        {
            txtObserMedicam.Text = hidObserMedicam.Value;

            grdPesqMedic.DataSource = null;
            grdPesqMedic.DataBind();

            AbreModalPadrao("AbreModalMedicamentos();");
        }

        protected void imgbPesqMedic_OnClick(object sender, EventArgs e)
        {
            if (drpGrupoMedic.SelectedValue != "0" || drpSubGrupoMedic.SelectedValue != "0" || (!String.IsNullOrEmpty(txtMedicamento.Text) && rdbMedic.Checked) || (!String.IsNullOrEmpty(txtPrincipio.Text) && rdbPrinc.Checked))
                CarregarMedicamentos();
            else
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Por favor informe pelo menos um dos parametros de pesquisa e tente novamente!");

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
            TBS356_PROC_MEDIC_PROCE tbs356 = RetornaEntidade();

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
                    tbs392.TB07_ALUNOReference.Load();
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

            txtQxsFicha.Text = txtQueixa.Text;
            txtAnamneseFicha.Text = txtHDA.Text;
            txtDiagnosticoFicha.Text = txtHipotese.Text;
            txtExameFicha.Text = txtExameFis.Text;
            txtObsFicha.Text = txtObserAtend.Text;

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

            foreach (GridViewRow li in grdPacAtestado.Rows)
            {
                RadioButton rdo = (((RadioButton)li.FindControl("rbtPaciente")));

                if (rdo.Checked)
                {
                    var nmPac = ((HiddenField)li.FindControl("hidNmPac")).Value;

                    if (chkAtestado.Checked)
                    {
                        var rgPac = ((HiddenField)li.FindControl("hidRgPac")).Value;
                        var hora = ((HiddenField)li.FindControl("hidHora")).Value;

                        var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                        RptAtestadoMedico2 fpcb = new RptAtestadoMedico2();
                        var lRetorno = fpcb.InitReport("Atestado Médico", infos, LoginAuxili.CO_EMP, nmPac, txtQtdDias.Text, chkCid.Checked, txtCid.Text, rgPac, txtDtAtestado.Text, hora, LoginAuxili.CO_COL);

                        GerarRelatorioPadrão(fpcb, lRetorno);
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

        #region Guia

        protected void BtnGuia_OnClick(object sender, EventArgs e)
        {
            var data = DateTime.Now;

            if (LoginAuxili.FLA_USR_DEMO)
                data = LoginAuxili.DATA_INICIO_USU_DEMO;

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

        protected void BtnReceituario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor finalizar o atendimento para realizar emissão do receituário");
                return;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            RptReceitMedic2 fpcb = new RptReceitMedic2();
            var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

            GerarRelatorioPadrão(fpcb, lRetorno);
        }

        protected void BtnExames_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor finalizar o atendimento para realizar emissão de guia de exames");
                return;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            RptReceitExames2 rpt = new RptReceitExames2();
            var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        protected void lnkbOrcamento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdAtendimento.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor finalizar o atendimento para realizar emissão de guia de exames");
                return;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptOrcamento2 fpcb = new RptOrcamento2();
            var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, int.Parse(hidIdAtendimento.Value));

            GerarRelatorioPadrão(fpcb, lRetorno);
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

            string titulo = "PRONTUÁRIO ODONTOLÓGICO";
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var dtIni = data.AddMonths(-1).ToShortDateString();
            var dtFim = data.ToShortDateString();

            C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuarioOdonto rpt = new C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuarioOdonto();
            var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, paciente, true, true, dtIni, dtFim, true, dtIni, dtFim, titulo.ToUpper());

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        protected void lnkbOdontograma_OnClick(object sender, EventArgs e)
        {
            AbreModalPadrao("AbreModalOdontograma();");
        }

        protected void lnkbImprimirOdontograma_OnClick(object sender, EventArgs e)
        {

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