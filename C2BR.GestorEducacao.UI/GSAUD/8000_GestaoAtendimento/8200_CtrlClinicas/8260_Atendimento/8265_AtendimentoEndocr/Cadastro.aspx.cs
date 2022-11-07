﻿//=============================================================================
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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8265_AtendimentoEndocr
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
                txtHrAtend.Text = data.ToShortTimeString();

                carregarOperadoras(ddlOperProcPlan);
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

                CarregarFormulasAplicacao();
                CarregarPlanosAlimentares();

                CarregarProfissionais(drpProfResp);

                //Se o logado não for master, e for profissional de saúde, mostra e seleciona ele de padrão
                if ((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M"))
                    drpProfResp.Enabled = false;

                if ((LoginAuxili.FLA_PROFESSOR == "S"))
                    drpProfResp.SelectedValue = LoginAuxili.CO_COL.ToString();

                txtVlBase.Enabled = txtVlCusto.Enabled = txtVlRestitu.Enabled = true;

                if (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_CLASS_PROFI != "D" && TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_CLASS_PROFI != "M")
                    lnkMedic.Visible = false;
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

                tbs390.DE_OBSER = txtObserAtend.Text;
                tbs390.DE_CONSI = hidTxtObserv.Value;
                tbs390.DE_QXA_PRINC = txtQueixa.Text;
                tbs390.DE_HDA = txtHDA.Text;
                tbs390.DE_EXM_FISIC = txtExameFis.Text;
                tbs390.DE_HIP_DIAGN = txtHipotese.Text;
                tbs390.DE_ACAO_REALI = txtAcaoPlanejada.Text;

                tbs390.NU_PESO_ATUAL = !String.IsNullOrEmpty(txtPesoAtual.Text) ? decimal.Parse(txtPesoAtual.Text) : (decimal?)null;
                tbs390.NU_GORD_ATUAL = !String.IsNullOrEmpty(txtGordAtual.Text) ? decimal.Parse(txtGordAtual.Text) : (decimal?)null;
                tbs390.NU_PESO_ANTER = !String.IsNullOrEmpty(txtPesoAnterior.Text) ? decimal.Parse(txtPesoAnterior.Text) : (decimal?)null;
                tbs390.NU_GORD_ANTER = !String.IsNullOrEmpty(txtGordAnterior.Text) ? decimal.Parse(txtGordAnterior.Text) : (decimal?)null;
                tbs390.NU_PESO_DESEJ = !String.IsNullOrEmpty(txtPesoDesejado.Text) ? decimal.Parse(txtPesoDesejado.Text) : (decimal?)null;
                tbs390.NU_GORD_DESEJ = !String.IsNullOrEmpty(txtGordDesejada.Text) ? decimal.Parse(txtGordDesejada.Text) : (decimal?)null;
                tbs390.NU_PESO_META = !String.IsNullOrEmpty(txtPesoMeta.Text) ? decimal.Parse(txtPesoMeta.Text) : (decimal?)null;
                tbs390.NU_GORD_META = !String.IsNullOrEmpty(txtGordMeta.Text) ? decimal.Parse(txtGordMeta.Text) : (decimal?)null;
                tbs390.NU_PESO_MAGRO = !String.IsNullOrEmpty(txtPesoMagro.Text) ? decimal.Parse(txtPesoMagro.Text) : (decimal?)null;
                tbs390.NU_GORD_MAGRO = !String.IsNullOrEmpty(txtGordMagra.Text) ? decimal.Parse(txtGordMagra.Text) : (decimal?)null;
                tbs390.NU_PESO_GORDO = !String.IsNullOrEmpty(txtPesoGordo.Text) ? decimal.Parse(txtPesoGordo.Text) : (decimal?)null;
                tbs390.NU_GORD_GORDO = !String.IsNullOrEmpty(txtGordGorda.Text) ? decimal.Parse(txtGordGorda.Text) : (decimal?)null;

                tbs390.TBS406_FORML_APLIC = !String.IsNullOrEmpty(drpFormlAplic.SelectedValue) ? TBS406_FORML_APLIC.RetornaPelaChavePrimaria(int.Parse(drpFormlAplic.SelectedValue)) : null;
                tbs390.DE_FORML_APLIC = txtFormlAplic.Text;
                tbs390.TBS407_PLANOS_ALIMEN = !String.IsNullOrEmpty(drpPlanoAliment.SelectedValue) ? TBS407_PLANOS_ALIMEN.RetornaPelaChavePrimaria(int.Parse(drpPlanoAliment.SelectedValue)) : null;
                tbs390.DE_PLANO_ALIMENT = txtPlanoAliment.Text;

                tbs390.NU_SENHA_ATEND = txtSenha.Text;

                tbs390.DE_ALERGIA = txtAlergiaMedic.Text;
                tbs390.FL_ALERGIA = chkAlergiaMedic.Checked ? "S" : "N";
                tbs390.FL_SITU_FATU = "N";//Negado

                //Dados de quem realizou o atendimento
                var data = !String.IsNullOrEmpty(txtDtAtend.Text) ? txtDtAtend.Text : DateTime.Now.ToShortDateString();
                var hora = !String.IsNullOrEmpty(txtHrAtend.Text) ? txtHrAtend.Text : DateTime.Now.ToShortTimeString();
                tbs390.DT_REALI = DateTime.Parse(data).Add(TimeSpan.Parse(hora));
                tbs390.CO_COL_ATEND = int.Parse(drpProfResp.SelectedValue);
                tbs390.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_COL_TEC_ATEND = int.Parse(drpProfResp.SelectedValue);
                tbs390.CO_EMP_TEC_ATEND = LoginAuxili.CO_EMP;
                tbs390.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(int.Parse(drpProfResp.SelectedValue)).CO_COL;

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

                #region Atualiza o Paciente

                tb07.NU_ALTU = !String.IsNullOrEmpty(txtAltura.Text) ? decimal.Parse(txtAltura.Text) : (decimal?)null;

                tb07.FL_DIABE = chkDiabetes.Checked ? "S" : "N";
                tb07.CO_TIPO_DIABE = drpTipoDiabete.SelectedValue;

                tb07.FL_HIPER = chkHipertensao.Checked ? "S" : "N";
                tb07.DE_HIPER = txtHipertensao.Text;

                tb07.FL_FUMAN = drpFumante.SelectedValue;
                tb07.NU_TEMPO_FUMAN = !String.IsNullOrEmpty(txtFumanteAnos.Text) ? int.Parse(txtFumanteAnos.Text) : (int?)null;

                tb07.FL_ALCOO = drpAlcool.SelectedValue;
                tb07.NU_TEMPO_ALCOO = !String.IsNullOrEmpty(txtAlcoolAnos.Text) ? int.Parse(txtAlcoolAnos.Text) : (int?)null;

                tb07.FL_CIRUR = chkCirurgia.Checked ? "S" : "N";
                tb07.DE_CIRUR = txtCirurgia.Text;

                tb07.FL_ALERGIA = chkAlergiaMedic.Checked ? "S" : "N";
                tb07.DE_ALERGIA = txtAlergiaMedic.Text;

                tb07.DE_MEDIC_USO_CONTI = txtMedicacaoCont.Text;

                #endregion

                #region Atualiza a agenda de ação

                //Atualizo apenas que a ação foi realizada

                tbs174.FL_SITUA_ACAO = "R";
                tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

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

        private void CarregarFormulasAplicacao()
        {
            AuxiliCarregamentos.CarregarFormulasAplicacao(drpFormlAplic, false);
        }

        private void CarregarPlanosAlimentares()
        {
            AuxiliCarregamentos.CarregarPlanosAlimentares(drpPlanoAliment, false);
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

            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGEND_HORAR).CO_ALU.Value);

            txtAltura.Text = tb07.NU_ALTU.HasValue ? tb07.NU_ALTU.ToString() : "";

            chkDiabetes.Checked = tb07.FL_DIABE == "S" ? true : false;
            drpTipoDiabete.SelectedValue = tb07.CO_TIPO_DIABE;

            chkHipertensao.Checked = tb07.FL_HIPER == "S" ? true : false;
            txtHipertensao.Text = tb07.DE_HIPER;

            drpFumante.SelectedValue = !String.IsNullOrEmpty(tb07.FL_FUMAN) ? tb07.FL_FUMAN : "N";
            txtFumanteAnos.Text = tb07.NU_TEMPO_FUMAN.HasValue ? tb07.NU_TEMPO_FUMAN.ToString() : "";

            drpAlcool.SelectedValue = !String.IsNullOrEmpty(tb07.FL_ALCOO) ? tb07.FL_ALCOO : "N";
            txtAlcoolAnos.Text = tb07.NU_TEMPO_ALCOO.HasValue ? tb07.NU_TEMPO_ALCOO.ToString() : "";

            chkCirurgia.Checked = tb07.FL_CIRUR == "S" ? true : false;
            txtCirurgia.Text = tb07.DE_CIRUR;

            chkAlergiaMedic.Checked = tb07.FL_ALERGIA == "S" ? true : false;
            txtAlergiaMedic.Text = tb07.DE_ALERGIA;

            txtMedicacaoCont.Text = tb07.DE_MEDIC_USO_CONTI;

            hidIdAtendimento.Value =
            hidObserMedicam.Value =
            hidObserExame.Value =
            txtObserMedicam.Text =
            txtObserExame.Text =
            txtObservacoes.Text =
            txtQueixa.Text =
            txtHDA.Text =
            txtExameFis.Text =
            txtHipotese.Text =
            txtAcaoPlanejada.Text =
            txtObserAtend.Text =
            txtPesoAtual.Text =
            txtGordAtual.Text =
            txtPesoAnterior.Text =
            txtGordAnterior.Text =
            txtPesoDesejado.Text =
            txtGordDesejada.Text =
            txtPesoMeta.Text =
            txtGordMeta.Text =
            txtPesoMagro.Text =
            txtGordMagra.Text =
            txtPesoGordo.Text =
            txtGordGorda.Text =
            txtSenha.Text =
            BtnSalvar.OnClientClick =
            BtnFinalizar.OnClientClick = "";

            txtDtAtend.Text = DateTime.Now.ToShortDateString();
            txtHrAtend.Text = DateTime.Now.ToShortTimeString();

            if (grdExame.Rows.Count != 0 || grdMedicamentos.Rows.Count != 0)
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
                txtObserAtend.Text = Atend.DE_OBSER;
                txtQueixa.Text = Atend.DE_QXA_PRINC;
                txtHDA.Text = Atend.DE_HDA;
                txtExameFis.Text = Atend.DE_EXM_FISIC;
                txtHipotese.Text = Atend.DE_HIP_DIAGN;
                txtAcaoPlanejada.Text = Atend.DE_ACAO_REALI;

                txtPesoAtual.Text = Atend.NU_PESO_ATUAL.ToString() ?? "";
                txtGordAtual.Text = Atend.NU_GORD_ATUAL.ToString() ?? "";
                txtPesoAnterior.Text = Atend.NU_PESO_ANTER.ToString() ?? "";
                txtGordAnterior.Text = Atend.NU_GORD_ANTER.ToString() ?? "";
                txtPesoDesejado.Text = Atend.NU_PESO_DESEJ.ToString() ?? "";
                txtGordDesejada.Text = Atend.NU_GORD_DESEJ.ToString() ?? "";
                txtPesoMeta.Text = Atend.NU_PESO_META.ToString() ?? "";
                txtGordMeta.Text = Atend.NU_GORD_META.ToString() ?? "";
                txtPesoMagro.Text = Atend.NU_PESO_MAGRO.ToString() ?? "";
                txtGordMagra.Text = Atend.NU_GORD_MAGRO.ToString() ?? "";
                txtPesoGordo.Text = Atend.NU_PESO_GORDO.ToString() ?? "";
                txtGordGorda.Text = Atend.NU_GORD_GORDO.ToString() ?? "";

                Atend.TBS406_FORML_APLICReference.Load();
                if (Atend.TBS406_FORML_APLIC != null)
                    drpFormlAplic.SelectedValue = Atend.TBS406_FORML_APLIC.ID_FORML.ToString();
                txtFormlAplic.Text = Atend.DE_FORML_APLIC;

                Atend.TBS407_PLANOS_ALIMENReference.Load();
                if (Atend.TBS407_PLANOS_ALIMEN != null)
                    drpPlanoAliment.SelectedValue = Atend.TBS407_PLANOS_ALIMEN.ID_PLANO.ToString();
                txtPlanoAliment.Text = Atend.DE_PLANO_ALIMENT;

                txtSenha.Text = Atend.NU_SENHA_ATEND;
                txtDtAtend.Text = Atend.DT_REALI.ToShortDateString();
                txtHrAtend.Text = Atend.DT_REALI.ToShortTimeString();

                carregaGridExame(Atend.ID_ATEND_AGEND);
                carregaGridMedic(Atend.ID_ATEND_AGEND);
            }
        }

        private void LimparGridsAgendamento()
        {
            grdExame.DataSource =
            grdMedicamentos.DataSource = null;

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

        public void CarregarAnexosAssociados(Int32 coAlu, String tpAnexo)
        {
            var res = (from tbs392 in TBS392_ANEXO_ATEND.RetornaTodosRegistros()
                       where tbs392.TB07_ALUNO.CO_ALU == coAlu && tbs392.TP_ANEXO == tpAnexo
                       select new
                       {
                           tbs392.ID_ANEXO_ATEND,
                           tbs392.NM_TITULO,
                           tbs392.DE_OBSER,
                           DE_OBSER_RES = tbs392.DE_OBSER.Length > 77 ? tbs392.DE_OBSER.Substring(0, 77) + "..." : tbs392.DE_OBSER,
                           tbs392.DT_CADAS,
                           tbs392.TBS390_ATEND_AGEND.NU_REGIS
                       }).ToList();

            grdAnexos.DataSource = res;
            grdAnexos.DataBind();
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

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.ClientID == atual.ClientID)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);

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
                        hidIdAgenda.Value = "";
                }
                else
                    chk.Checked = false;
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
                    tbs174.CO_COL_ATEND = LoginAuxili.CO_COL;
                    tbs174.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
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

            var btn = (LinkButton)sender;

            hidTpAnexo.Value = btn.AccessKey;

            switch (btn.AccessKey)
            {
                case "F":
                    lblTpAnexo.Text = "FOTO";
                    break;
                case "V":
                    lblTpAnexo.Text = "VIDEO";
                    break;
                case "U":
                    lblTpAnexo.Text = "ÁUDIO";
                    break;
                case "A":
                    lblTpAnexo.Text = "ANEXO";
                    break;
            }

            var co_alu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value)).CO_ALU.Value;

            CarregarAnexosAssociados(co_alu, btn.AccessKey);

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
                tbs392.TP_ANEXO = hidTpAnexo.Value;
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
            RptReceitExames2 fpcb = new RptReceitExames2();
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

            string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8270_Prontuario/8271_Relatorios/RelProntuario.aspx");
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var dtIni = data.AddMonths(-1).ToShortDateString();
            var dtFim = data.ToShortDateString();

            C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuario rpt = new C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuario();
            var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, paciente, true, true, dtIni, dtFim, true, dtIni, dtFim, titulo.ToUpper());

            GerarRelatorioPadrão(rpt, lRetorno);
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