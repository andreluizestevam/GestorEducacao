//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//----------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//----------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR          | DESCRIÇÃO RESUMIDA
// -----------+-------------------------------+-------------------------------------
// 05/07/2016 | Tayguara Acioli  TA.05/07/2016| Adicionei a pop up de registro de ocorrências, que fica na master PadraoCadastros.Master.

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Globalization;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Data.Objects;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;
using System.Drawing;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod3
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {              

                CarregaOperadoras();
                CarregarQtde();
                
            }
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //#region Validacoes

            ////==============>Validações dos campos 
            ////Verifica se foi selecionado um Paciente
            //if (ddlNomeUsu.SelectedValue == "")
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente para quem será agendada a consulta.");
            //    ddlNomeUsu.Focus();
            //    return;
            //}

            //bool SelecHorario = false;

            ////Verifica se foi selecionado um horário para marcação da consulta
            //foreach (GridViewRow li in grdHorario.Rows)
            //{
            //    if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
            //    {
            //        SelecHorario = true;

            //        var dt = DateTime.Parse(((HiddenField)li.FindControl("hidData")).Value);
            //        var hr = ((HiddenField)li.FindControl("hidHora")).Value;
            //        var coCol = int.Parse(((HiddenField)li.FindControl("hidCoCol")).Value);
            //        var coAlu = int.Parse(ddlNomeUsu.SelectedValue);

            //        var ddlTipoAgendam = (DropDownList)li.FindControl("ddlTipoAgendam");
            //        //var ddlClasTipoConsulta = (DropDownList)li.FindControl("ddlClasTipoConsulta");
            //        var ddlTipo = (DropDownList)li.FindControl("ddlTipo");
            //        var ddlOper = (DropDownList)li.FindControl("ddlOperAgend");

            //        var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //                   where (tbs174.CO_ALU == coAlu)
            //                   && (tbs174.CO_COL == coCol)
            //                   && (tbs174.DT_AGEND_HORAR == dt)
            //                   && (tbs174.HR_AGEND_HORAR == hr)
            //                   && (tbs174.CO_SITUA_AGEND_HORAR != "C")
            //                   select tbs174).ToList();

            //        if (res != null && res.Count > 0)
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe agendado para este paciente com este profissional na seguinte data:\n" + dt.ToShortDateString() + " " + hr);
            //            return;
            //        }

            //        if (string.IsNullOrEmpty(ddlTipoAgendam.SelectedValue))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de consulta");
            //            ddlTipoAgendam.Focus();
            //            return;
            //        }

            //        if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a Classificação do Agendamento");
            //            ddlTipo.Focus();
            //            return;
            //        }

            //        if (string.IsNullOrEmpty(ddlOper.SelectedValue))
            //        {
            //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar O tipo de Contratação do Agendamento");
            //            ddlOper.Focus();
            //            return;
            //        }
            //    }
            //}

            //bool SelecProfiss = false;

            ////Verifica se foi selecionad um profissional de saúde
            //foreach (GridViewRow li in grdProfi.Rows)
            //{
            //    if (SelecProfiss == false)
            //    {
            //        if (((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked)
            //        {
            //            SelecProfiss = true;
            //            break;
            //        }
            //    }
            //}

            ////Valida a variável booleada criada anteriormente para verificar se foi selecionado um profissional
            //if (SelecProfiss == false)
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Profissional de Saúde para o qual será feito o agendamento de consulta");
            //    grdProfi.Focus();
            //    return;
            //}

            ////Valida a variável booleana criada anteriormente
            //if (SelecHorario == false)
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um horário da agenda para o qual será feita a marcação da consulta.");
            //    grdHorario.Focus();
            //    return;
            //}

            //#endregion

            //if (hidMultiAgend.Value == "S")
            //{
            //    liBtnGrdFinanMater.Visible =
            //    li9.Visible = true;

            //    ScriptManager.RegisterStartupScript(
            //        this.Page,
            //        this.GetType(),
            //        "Acao",
            //        "AbreModalAgendMulti();",
            //        true
            //    );
            //}
            //else //Se não for multiagenda, já vai direto para a persistencia convencional
            //    Persistencias();
        }

        /// <summary>
        /// Executa os métodos para persistência de dados
        /// </summary>
        //private void Persistencias()
        //{
        //    if (lblSitPaciente.CssClass != "sitPacPadrao")
        //    {
        //        ScriptManager.RegisterStartupScript(
        //            this.Page,
        //            this.GetType(),
        //            "Acao",
        //            "AbreModalConfirmAlta();",
        //            true
        //        );

        //        return;
        //    }

        //    //Se for agenda múltipla
        //    int coAlu = int.Parse(ddlNomeUsu.SelectedValue);
        //    TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

        //    if (String.IsNullOrEmpty(tb07.FL_LIST_ESP) || tb07.FL_LIST_ESP == "S")
        //    {
        //        tb07.FL_LIST_ESP = "N";
        //        TB07_ALUNO.SaveOrUpdate(tb07);
        //    }

        //    if (hidMultiAgend.Value == "S")
        //    {
        //        string hr = "";

        //        foreach (GridViewRow lis in grdHorario.Rows)
        //        {
        //            //Verifica a linha que foi selecionada
        //            if (((CheckBox)lis.Cells[0].FindControl("ckSelectHr")).Checked && hr != ((HiddenField)lis.Cells[0].FindControl("hidDataHora")).Value)
        //            {
        //                hr = ((HiddenField)lis.Cells[0].FindControl("hidDataHora")).Value;
        //                DropDownList ddlTpConsul = (((DropDownList)lis.Cells[3].FindControl("ddlTipoAgendam")));
        //                DropDownList ddlTipo = (((DropDownList)lis.Cells[4].FindControl("ddlTipo")));
        //                //DropDownList ddlClasTipoConsulta = (((DropDownList)lis.Cells[4].FindControl("ddlClasTipoConsulta")));
        //                DropDownList ddlOper = (((DropDownList)lis.Cells[5].FindControl("ddlOperAgend")));
        //                DropDownList ddlPlan = (((DropDownList)lis.Cells[6].FindControl("ddlPlanoAgend")));
        //                DropDownList ddlProc = (((DropDownList)lis.Cells[7].FindControl("ddlProcedAgend")));
        //                TextBox txtValor = (((TextBox)lis.Cells[8].FindControl("txtValorAgend")));

        //                int coAgend = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoAgenda")).Value));
        //                string tpConsul = ddTipoM.SelectedValue;
        //                int coEmpAlu = tb07.CO_EMP;
        //                CheckBox chek = ((CheckBox)lis.Cells[5].FindControl("ckConf"));

        //                //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
        //                TBS174_AGEND_HORAR esp174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);
        //                TBS174_AGEND_HORAR agend = new TBS174_AGEND_HORAR();
        //                #region Elabora agenda CO_TP_AGEND
        //                agend.TP_AGEND_HORAR = tpConsul;
        //                agend.TP_CONSU = tpConsul;
        //                agend.DT_AGEND_HORAR = esp174.DT_AGEND_HORAR;
        //                agend.HR_AGEND_HORAR = esp174.HR_AGEND_HORAR;
        //                agend.CO_SITUA_AGEND_HORAR = "A";
        //                agend.DT_SITUA_AGEND_HORAR = DateTime.Now;
        //                agend.CO_EMP_SITUA = LoginAuxili.CO_EMP;
        //                agend.CO_COL_SITUA = LoginAuxili.CO_COL;
        //                agend.FL_AGEND_CONSU = "N";
        //                agend.FL_CONF_AGEND = "N";
        //                agend.FL_ENCAI_AGEND = "N";
        //                agend.CO_COL = esp174.CO_COL;
        //                agend.CO_EMP = esp174.CO_EMP;
        //                agend.CO_DEPT = esp174.CO_DEPT;
        //                agend.ID_DEPTO_LOCAL_RECEP = esp174.CO_DEPT;
        //                agend.TP_CONSU = ddlTipo.SelectedValue;
        //                //agend.CO_ESPEC = coEsp;
        //                agend.HR_DURACAO_AGENDA = esp174.HR_DURACAO_AGENDA;
        //                agend.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
        //                agend.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue)) : null);
        //                agend.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue)) : null);

        //                if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
        //                {
        //                    var proced = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue));
        //                    proced.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
        //                    var valor = proced.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A");
        //                    if (valor != null)
        //                        agend.VL_CONSU_BASE = valor.VL_BASE;
        //                }
        //                agend.VL_CONSUL = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);
        //                #endregion

        //                #region Salva o Paciente na agenda

        //                //int coAlu = int.Parse(ddlNomeUsu.SelectedValue);
        //                //var resal = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
        //                //resal.TB250_OPERAReference.Load();
        //                //resal.TB251_PLANO_OPERAReference.Load();

        //                agend.TP_AGEND_HORAR = esp174.TP_AGEND_HORAR;
        //                agend.CO_EMP_ALU = esp174.CO_EMP;
        //                agend.CO_ALU = coAlu;
        //                agend.TP_CONSU = tpConsul;
        //                agend.FL_CONF_AGEND = "N";
        //                agend.FL_CONFIR_CONSUL_SMS = "N";
        //                //agend.TB250_OPERA = (resal.TB250_OPERA != null ? resal.TB250_OPERA : null);
        //                //agend.TB251_PLANO_OPERA = (resal.TB251_PLANO_OPERA != null ? resal.TB251_PLANO_OPERA : null);

        //                #region Gera Código da Consulta

        //                string coUnid = LoginAuxili.CO_UNID.ToString();
        //                int coEmp = LoginAuxili.CO_EMP;
        //                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

        //                var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //                           where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
        //                           select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

        //                string seq;
        //                int seq2;
        //                int seqConcat;
        //                string seqcon;
        //                if (res == null)
        //                {
        //                    seq2 = 1;
        //                }
        //                else
        //                {
        //                    seq = res.NU_REGIS_CONSUL.Substring(7, 7);
        //                    seq2 = int.Parse(seq);
        //                }

        //                seqConcat = seq2 + 1;
        //                seqcon = seqConcat.ToString().PadLeft(7, '0');

        //                agend.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

        //                //#endregion

        //                #endregion

        //                #endregion


        //                //foreach (GridViewRow row in grdProfi.Rows)
        //                //{
        //                //    //Verifica a linha que foi selecionada
        //                //    if (((CheckBox)row.Cells[0].FindControl("ckSelect")).Checked)
        //                //    {
        //                //        int coProfissional = int.Parse(((HiddenField)row.Cells[0].FindControl("hidCoCold")).ToString());

        //                //        var tipoConsulta = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_COL == coProfissional).Select(x => x.CO_CLASS_PROFI);
        //                //        string CO_TP_AGEND = "";

        //                //        switch (tipoConsulta.ToString())
        //                //        {
        //                //            case "E":
        //                //                CO_TP_AGEND = "EN"; //Enfermeiro(a)
        //                //                break;
        //                //            case "M":
        //                //                CO_TP_AGEND = "AM"; //Atendimento médico
        //                //                break;
        //                //            case "D":
        //                //                CO_TP_AGEND = "AO"; //Odontológico
        //                //                break;
        //                //            case "S":
        //                //                CO_TP_AGEND = "ES"; //Esteticista
        //                //                break;
        //                //            case "N":
        //                //                CO_TP_AGEND = "NT"; //Nutricionista
        //                //                break;
        //                //            case "I":
        //                //                CO_TP_AGEND = "FI"; //FISIOTERAPEUTA
        //                //                break;
        //                //            case "F":
        //                //                CO_TP_AGEND = "FI"; //Fonoaudiólogo
        //                //                break;
        //                //            case "P":
        //                //                CO_TP_AGEND = "PI"; //Psicólogo(a)
        //                //                break;
        //                //            case "T":
        //                //                CO_TP_AGEND = "TO"; //Terapeuta Ocupacional
        //                //                break;
        //                //            case "O":
        //                //                CO_TP_AGEND = "OU"; //OUTROS
        //                //                break;
        //                //            default:
        //                //                CO_TP_AGEND = " - ";
        //                //                break;
        //                //        }
        //                //        agend.TP_AGEND_HORAR = CO_TP_AGEND;// ddlTpConsul.SelectedValue;
        //                //    }
        //                //    else
        //                //    {
        //                //        agend.TP_AGEND_HORAR = "";
        //                //    }
        //                //}

        //                agend.TP_AGEND_HORAR = ddlTpConsul.SelectedValue;
        //                agend.TP_CONSU = tpConsul;

        //                agend.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");
        //                TBS174_AGEND_HORAR.SaveOrUpdate(agend);

        //                if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
        //                {

        //                    //Inclui registro de item de planejamento na tabela TBS386_ITENS_PLANE_AVALI
        //                    #region Inclui o Item de Planjamento

        //                    //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
        //                    TBS386_ITENS_PLANE_AVALI tbs386 = new TBS386_ITENS_PLANE_AVALI();

        //                    //Dados da situação
        //                    tbs386.CO_SITUA = "A";
        //                    tbs386.DT_SITUA = DateTime.Now;
        //                    tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
        //                    tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
        //                    tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
        //                    tbs386.IP_SITUA = Request.UserHostAddress;
        //                    tbs386.DE_RESUM_ACAO = null;

        //                    //Dados básicos do item de planejamento
        //                    tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), agend.CO_ALU.Value);
        //                    tbs386.TBS356_PROC_MEDIC_PROCE = agend.TBS356_PROC_MEDIC_PROCE;
        //                    tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
        //                    tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(coAgend, agend.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
        //                    tbs386.DT_INICI = agend.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
        //                    tbs386.DT_FINAL = agend.DT_AGEND_HORAR; //Verifica qual a última data na lista
        //                    tbs386.FL_AGEND_FEITA_PLANE = "N";

        //                    //Dados do cadastro
        //                    tbs386.DT_CADAS = DateTime.Now;
        //                    tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
        //                    tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
        //                    tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
        //                    tbs386.IP_CADAS = Request.UserHostAddress;
        //                    tbs386.VL_PROCED = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);

        //                    //Data prevista é a data do agendamento associado
        //                    tbs386.DT_AGEND = agend.DT_AGEND_HORAR;

        //                    TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);

        //                    #endregion

        //                    //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
        //                    #region Associa o Item ao Agendamento

        //                    TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();
        //                    tbs389.TBS174_AGEND_HORAR = agend;
        //                    tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
        //                    TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);

        //                    agend.TBS370_PLANE_AVALI = tbs386.TBS370_PLANE_AVALI;
        //                    TBS174_AGEND_HORAR.SaveOrUpdate(agend, true);

        //                    #endregion
        //                }
        //            }
        //        }
        //    }
        //    else //Se não for múltipla
        //    {
        //        #region Convencional

        //        //Comentado para ser melhor implementado posteriormente
        //        #region Valida se o paciente já tem algum agendamento neste mesmo horário e data

        //        //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
        //        //foreach (GridViewRow lis in grdHorario.Rows)
        //        //{
        //        //    //Verifica a linha que foi selecionada
        //        //    if (((CheckBox)lis.Cells[0].FindControl("ckSelectHr")).Checked)
        //        //    {
        //        //        int coAgend = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoAgenda")).Value));
        //        //        //Dados da agenda
        //        //        var dadosAgendamento = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //        //                                where tbs174.ID_AGEND_HORAR == coAgend
        //        //                                select new { tbs174.DT_AGEND_HORAR, tbs174.HR_AGEND_HORAR, tbs174.HR_DURACAO_AGENDA }).FirstOrDefault();

        //        //        //Lista de agendamentos do paciente para o dia em questão
        //        //        var listaAgendaPaciDia = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //        //                                  join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
        //        //                                  where EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) == EntityFunctions.TruncateTime(dadosAgendamento.DT_AGEND_HORAR)
        //        //                                  select new { tb03.NO_COL, tb03.CO_CLASS_PROFI, tbs174.DT_AGEND_HORAR, tbs174.HR_AGEND_HORAR, tbs174.HR_DURACAO_AGENDA }).ToList();

        //        //        //data e hora inicial e final do agendamento em questão
        //        //        TimeSpan tshi = TimeSpan.Parse((dadosAgendamento.HR_AGEND_HORAR + ":00"));
        //        //        TimeSpan tsfin = tshi.Add(TimeSpan.Parse(dadosAgendamento.HR_DURACAO_AGENDA));
        //        //        DateTime dtIniAgenda = dadosAgendamento.DT_AGEND_HORAR.Add(tshi);
        //        //        DateTime dtFinalInclusao = dadosAgendamento.DT_AGEND_HORAR.Add(tsfin);

        //        //        //Percorre a lista de agendamentos do paciente
        //        //        foreach (var i in listaAgendaPaciDia)
        //        //        {
        //        //            //Data e hora inicial e final do agendamento anterior do paciente
        //        //            TimeSpan tshiPa = TimeSpan.Parse((i.HR_AGEND_HORAR + ":00"));
        //        //            TimeSpan tsfinPa = tshi.Add(TimeSpan.Parse(i.HR_DURACAO_AGENDA));
        //        //            DateTime dtIniAgendaPa = i.DT_AGEND_HORAR.Add(tshiPa);
        //        //            DateTime dtFinalInclusaoPa = i.DT_AGEND_HORAR.Add(tsfinPa);

        //        //            //Verifica se a duração do agendamento em questão, vai conflitar com a duração de outro agendamento já registrado para paciente e data
        //        //            if (((dtIniAgenda >= dtIniAgendaPa) && (dtIniAgenda <= dtFinalInclusaoPa))
        //        //                || ((dtFinalInclusao >= dtIniAgendaPa) && (dtFinalInclusao <= dtFinalInclusaoPa)))
        //        //            {
        //        //                string erro = "O horário das " + tshi.Hours.ToString().PadLeft(2, '0')
        //        //                                                    + ":"
        //        //                                                    + tshi.Minutes.ToString().PadLeft(2, '0')
        //        //                                                    + "; "
        //        //                                                    + " do dia " + dadosAgendamento.DT_AGEND_HORAR.ToString("dd/MM/yyyy")
        //        //                                                    + " já está agendado para este paciente para o profissional "
        //        //                                                    + i.NO_COL
        //        //                                                    + " na categoria " + AuxiliGeral.GetNomeClassificacaoFuncional(i.CO_CLASS_PROFI)
        //        //                                                    + ". Favor rever os conflitos.";

        //        //                AuxiliPagina.EnvioMensagemErro(this.Page, erro);
        //        //                return;
        //        //            }
        //        //        }
        //        //    }
        //        //}

        //        #endregion

        //        //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
        //        foreach (GridViewRow lis in grdHorario.Rows)
        //        {
        //            //Verifica a linha que foi selecionada
        //            if (((CheckBox)lis.Cells[0].FindControl("ckSelectHr")).Checked)
        //            {
        //                DropDownList ddlTpConsul = (((DropDownList)lis.Cells[3].FindControl("ddlTipoAgendam")));
        //                DropDownList ddlTipo = (((DropDownList)lis.Cells[4].FindControl("ddlTipo")));
        //                //DropDownList ddlClasTipoConsulta = (((DropDownList)lis.Cells[3].FindControl("ddlClasTipoConsulta")));
        //                DropDownList ddlOper = (((DropDownList)lis.Cells[5].FindControl("ddlOperAgend")));
        //                DropDownList ddlPlan = (((DropDownList)lis.Cells[6].FindControl("ddlPlanoAgend")));
        //                DropDownList ddlProc = (((DropDownList)lis.Cells[7].FindControl("ddlProcedAgend")));
        //                TextBox txtValor = (((TextBox)lis.Cells[8].FindControl("txtValorAgend")));

        //                int coAgend = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoAgenda")).Value));
        //                string tpConsul = ddlTpConsul.SelectedValue;
        //                //foreach (GridViewRow row in grdProfi.Rows)
        //                //{
        //                //    //Verifica a linha que foi selecionada
        //                //    if (((CheckBox)row.Cells[0].FindControl("ckSelect")).Checked)
        //                //    {
        //                //        int coProfissional = int.Parse(((HiddenField)row.Cells[0].FindControl("hidCoCold")).ToString());

        //                //        var tipoConsulta = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_COL == coProfissional).Select(x => x.CO_CLASS_PROFI);                                

        //                //        switch (tipoConsulta.ToString())
        //                //        {
        //                //            case "E":
        //                //                tpConsul = "EN"; //Enfermeiro(a)
        //                //                break;
        //                //            case "M":
        //                //                tpConsul = "AM"; //Atendimento médico
        //                //                break;
        //                //            case "D":
        //                //                tpConsul = "AO"; //Odontológico
        //                //                break;
        //                //            case "S":
        //                //                tpConsul = "ES"; //Esteticista
        //                //                break;
        //                //            case "N":
        //                //                tpConsul = "NT"; //Nutricionista
        //                //                break;
        //                //            case "I":
        //                //                tpConsul = "FI"; //FISIOTERAPEUTA
        //                //                break;
        //                //            case "F":
        //                //                tpConsul = "FI"; //Fonoaudiólogo
        //                //                break;
        //                //            case "P":
        //                //                tpConsul = "PI"; //Psicólogo(a)
        //                //                break;
        //                //            case "T":
        //                //                tpConsul = "TO"; //Terapeuta Ocupacional
        //                //                break;
        //                //            case "O":
        //                //                tpConsul = "OU"; //OUTROS
        //                //                break;
        //                //            default:
        //                //                tpConsul = " - ";
        //                //                break;
        //                //        }
        //                //    }
        //                //}



        //                int coEmpAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault().CO_EMP;
        //                CheckBox chek = ((CheckBox)lis.Cells[5].FindControl("ckConf"));

        //                //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
        //                TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

        //                if (chkEnviaSms.Checked)
        //                    EnviaSMS((tbs174.CO_ALU != null ? false : true), tbs174.HR_AGEND_HORAR, tbs174.DT_AGEND_HORAR, tbs174.CO_COL.Value, tbs174.CO_ESPEC.Value, tbs174.CO_EMP.Value, tb07.NU_TELE_CELU_ALU, tb07.NO_ALU, tb07.CO_ALU);

        //                tbs174.TP_AGEND_HORAR = tpConsul;
        //                tbs174.CO_EMP_ALU = coEmpAlu;
        //                tbs174.CO_ALU = coAlu;
        //                tbs174.TP_CONSU = ddlTipo.SelectedValue;
        //                tbs174.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
        //                tbs174.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue)) : null);
        //                tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue)) : null);
        //                tbs174.FL_CONF_AGEND = "N";
        //                tbs174.FL_CONFIR_CONSUL_SMS = chkEnviaSms.Checked ? "S" : "N";

        //                //tbs174.DT_VENC = (!string.IsNullOrEmpty(txtDtVenciPlan.Text) ? txtDtVenciPlan.Text : null);
        //                //tbs174.NU_PLAN_SAUDE = (!string.IsNullOrEmpty(txtNumeroCartPla.Text) ? txtNumeroCartPla.Text : null);

        //                ////Informações de valores
        //                if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
        //                {
        //                    var proced = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue));
        //                    proced.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
        //                    var valor = proced.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A");
        //                    if (valor != null)
        //                        tbs174.VL_CONSU_BASE = valor.VL_BASE;
        //                }
        //                //tbs174.VL_DESCT = (!string.IsNullOrEmpty(txtVlDscto.Text) ? decimal.Parse(txtVlDscto.Text) : (decimal?)null);
        //                tbs174.VL_CONSUL = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);

        //                #region Gera Código da Consulta

        //                string coUnid = LoginAuxili.CO_UNID.ToString();
        //                int coEmp = LoginAuxili.CO_EMP;
        //                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

        //                var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //                           where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
        //                           select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

        //                string seq;
        //                int seq2;
        //                int seqConcat;
        //                string seqcon;
        //                if (res == null)
        //                {
        //                    seq2 = 1;
        //                }
        //                else
        //                {
        //                    seq = res.NU_REGIS_CONSUL.Substring(7, 7);
        //                    seq2 = int.Parse(seq);
        //                }

        //                seqConcat = seq2 + 1;
        //                seqcon = seqConcat.ToString().PadLeft(7, '0');

        //                tbs174.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

        //                #endregion

        //                //Verifica se foi escolhida operadora, se tiver sido, verifica se tem procedimento correspondente
        //                #region Verifica o Código do Procedimento

        //                //int coOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
        //                //int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);

        //                ////Procura pelo procedimento da Operadora ID_OPER correspondente ao ID_PROC associados pelo campo agrupador para retornar o valor
        //                //var resusu = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
        //                //              where tbs356.ID_AGRUP_PROC_MEDI_PROCE == idProc
        //                //              && tbs356.TB250_OPERA.ID_OPER == coOper
        //                //              && tbs356.CO_SITU_PROC_MEDI == "A"
        //                //              select new { tbs356.ID_PROC_MEDI_PROCE }).FirstOrDefault();

        //                //Se não tiver selecionado operadora, então salva o que tiver sido selecionado como procedimento
        //                //if (coOper == 0)
        //                //    tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue)) : null);
        //                ////tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue)) : null);
        //                //else // Se tiver sido escolhida operadora, verifica se existe procedimento correspondente ao selecionado para a operadora
        //                //    tbs174.TBS356_PROC_MEDIC_PROCE = (resusu != null ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(resusu.ID_PROC_MEDI_PROCE) : null);

        //                #endregion

        //                tbs174.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");

        //                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

        //                if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
        //                {

        //                    //Inclui registro de item de planejamento na tabela TBS386_ITENS_PLANE_AVALI
        //                    #region Inclui o Item de Planjamento

        //                    //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
        //                    TBS386_ITENS_PLANE_AVALI tbs386 = new TBS386_ITENS_PLANE_AVALI();

        //                    //Dados da situação
        //                    tbs386.CO_SITUA = "A";
        //                    tbs386.DT_SITUA = DateTime.Now;
        //                    tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
        //                    tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
        //                    tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
        //                    tbs386.IP_SITUA = Request.UserHostAddress;
        //                    tbs386.DE_RESUM_ACAO = null;

        //                    //Dados básicos do item de planejamento
        //                    tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), tbs174.CO_ALU.Value);
        //                    tbs386.TBS356_PROC_MEDIC_PROCE = tbs174.TBS356_PROC_MEDIC_PROCE;
        //                    tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
        //                    tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(coAgend, tbs174.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
        //                    tbs386.DT_INICI = tbs174.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
        //                    tbs386.DT_FINAL = tbs174.DT_AGEND_HORAR; //Verifica qual a última data na lista
        //                    tbs386.FL_AGEND_FEITA_PLANE = "N";

        //                    //Dados do cadastro
        //                    tbs386.DT_CADAS = DateTime.Now;
        //                    tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
        //                    tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
        //                    tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
        //                    tbs386.IP_CADAS = Request.UserHostAddress;

        //                    //Data prevista é a data do agendamento associado
        //                    tbs386.DT_AGEND = tbs174.DT_AGEND_HORAR;
        //                    tbs386.VL_PROCED = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);

        //                    TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);

        //                    #endregion

        //                    //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
        //                    #region Associa o Item ao Agendamento

        //                    TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();
        //                    tbs389.TBS174_AGEND_HORAR = tbs174;
        //                    tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
        //                    TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);

        //                    tbs174.TBS370_PLANE_AVALI = tbs386.TBS370_PLANE_AVALI;
        //                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

        //                    #endregion
        //                }

        //                //tb07.TB108_RESPONSAVELReference.Load();
        //                //int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);
        //                //int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);

        //                //if (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue))
        //                //    GravaFinanceiroProcedimentos(TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue)),
        //                //        tbs174.CO_ALU.Value, (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP : tb07.CO_ALU),
        //                //        idPlan, idOper, tbs174.ID_AGEND_HORAR, tbs174.CO_COL.Value);
        //            }
        //        }

        //        #endregion
        //    }

        //    if (String.IsNullOrEmpty((string)(Session[SessoesHttp.URLOrigem])))
        //        AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento da Consulta realizado com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        //    else
        //        AuxiliPagina.RedirecionaParaPaginaSucesso("Encaixe de agendamento realizado com sucesso!", Session[SessoesHttp.URLOrigem].ToString());
        //}


       private void CarregarQtde(){
           List<Count> qtde = new List<Count>();

           for (int i = 1; i <= 100; i++)
           {
               var count = new Count();
               count.count = i;
               qtde.Add(count);
           }
           ddlQtde.DataSource = qtde;
           ddlQtde.DataTextField = "count";
           ddlQtde.DataValueField = "count";
           ddlQtde.DataBind();
       }

       public class Count 
       {
           public int count { get; set; }
       }
            
        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, DropDownList ddlOperPlano, DropDownList ddlPlano, TextBox txtValor)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), idOper, idPlan);
                txtValor.Text = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }

        /// <summary>
        /// Grava na tabela de financeiro de procedimentos os devidos dados
        /// </summary>
        private void GravaFinanceiroProcedimentos(TBS356_PROC_MEDIC_PROCE tbs356, int CO_ALU, int CO_RESP, int ID_PLAN, int ID_OPER, int ID_AGEND_HORAR, int CO_COL)
        {
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == CO_COL
                      select new { tb03.CO_EMP }).FirstOrDefault();

            TBS357_PROC_MEDIC_FINAN tbs357 = new TBS357_PROC_MEDIC_FINAN();

            //Recebe objeto com o valor corrente do procedimento para determinado plano de saúde (Quando esta for a situação)
            AuxiliCalculos.ValoresProcedimentosMedicos valPrc = AuxiliCalculos.RetornaValoresProcedimentosMedicos(tbs356.ID_PROC_MEDI_PROCE, ID_OPER, ID_PLAN);

            tbs357.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGEND_HORAR);
            tbs357.TB250_OPERA = (ID_OPER != 0 ? TB250_OPERA.RetornaPelaChavePrimaria(ID_OPER) : null);
            tbs357.CO_COL_INCLU_LANC = LoginAuxili.CO_COL;
            tbs357.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tbs357.IP_INCLU_LANC = Request.UserHostAddress;
            tbs357.CO_COL_PROFI_ATEND = CO_COL;
            tbs357.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(re.CO_EMP);
            tbs357.FL_SITUA = "A";
            tbs357.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs357.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs357.DT_SITUA = DateTime.Now;
            tbs357.IP_SITUA = Request.UserHostAddress;
            tbs357.DT_INCLU_LANC = DateTime.Now;
            tbs357.ID_ITEM = tbs356.ID_PROC_MEDI_PROCE;
            tbs357.NM_ITEM = (tbs356.NM_PROC_MEDI.Length > 100 ? tbs356.NM_PROC_MEDI.Substring(0, 100) : tbs356.NM_PROC_MEDI);
            tbs357.CO_TIPO_ITEM = "PCM";
            tbs357.CO_ORIGEM = "C"; //Determina que a origem desse registro financeiro é uma consulta
            tbs357.CO_ALU = CO_ALU;
            tbs357.CO_RESP = CO_RESP;
            //tbs357.DT_EVENT = DateTime.Now;

            //Questão de valores
            tbs357.VL_CUSTO_PROC = valPrc.VL_CUSTO;
            tbs357.VL_RESTI = valPrc.VL_RESTI;
            tbs357.VL_BASE = valPrc.VL_BASE;
            tbs357.VL_PROCE_LIQUI = valPrc.VL_CALCULADO;
            tbs357.VL_DSCTO = valPrc.VL_DESCONTO;
            tbs357.TBS353_VALOR_PROC_MEDIC_PROCE = (valPrc.ID_VALOR_PROC_MEDIC_PROCE != 0 ? TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(valPrc.ID_VALOR_PROC_MEDIC_PROCE) : null);
            tbs357.TBS361_CONDI_PLANO_SAUDE = (valPrc.ID_CONDI_PLANO_SAUDE != 0 ? TBS361_CONDI_PLANO_SAUDE.RetornaPelaChavePrimaria(valPrc.ID_CONDI_PLANO_SAUDE) : null);

            TBS357_PROC_MEDIC_FINAN.SaveOrUpdate(tbs357, true);
        }

        /// <summary>
        /// Responsável por carregar os pacientes de acordo com o cpf concedido
        /// </summary>
        private void PesquisaPaciente()
        {
            ////Verifica se o usuário optou por pesquisar por CPF ou por NIRE
            //if (chkPesqCpf.Checked)
            //{
            //    string cpf = (txtCPFPaci.Text != "" ? txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim() : "");

            //    //Valida se o usuário digitou ou não o CPF
            //    if (txtCPFPaci.Text == "")
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF para pesquisa");
            //        return;
            //    }

            //    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
            //               where tb07.NU_CPF_ALU == cpf
            //               select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

            //    if (res != null && ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
            //        ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
            //    //txtNisUsu.Text = res.NU_NIS.ToString();
            //}
            //else if (chkPesqNire.Checked)
            //{
            //    //Valida se o usuário deixou o campo em branco.
            //    if (txtNirePaci.Text == "")
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIRE para pesquisa");
            //        return;
            //    }

            //    int nire = int.Parse(txtNirePaci.Text.Trim());

            //    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
            //               where tb07.NU_NIRE == nire
            //               select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

            //    if (res != null && ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
            //        ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
            //    //txtNisUsu.Text = res.NU_NIS.ToString();
            //}
        }

        /// <summary>
        /// Método responsável por enviar SMS caso a opção correspondente tenha sido selecionada
        /// </summary>
        private void EnviaSMS(bool NovoAgendamento, string hora, DateTime data, int CO_COL, int CO_ESPEC, int CO_EMP, string NU_CELULAR, string NO_ALU, int CO_ALU)
        {
            //***IMPORTANTE*** - O limite máximo de caracteres de acordo com a ZENVIA que é quem presta o serviço de envio,
            //é de 140 caracteres para NEXTEL e 150 para DEMAIS OPERADORAS
            TB03_COLABOR tb03 = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == CO_COL).FirstOrDefault();
            string noEspec = TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(w => w.CO_ESPECIALIDADE == CO_ESPEC).FirstOrDefault().NO_ESPECIALIDADE;
            string noEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP).NO_FANTAS_EMP;

            noEspec = noEspec.Length > 23 ? noEspec.Substring(0, 23) : noEspec;
            bool masc = tb03.CO_SEXO_COL == "M" ? true : false;
            string noCol = tb03.NO_COL.Length > 40 ? tb03.NO_COL.Substring(0, 40) : tb03.NO_COL;
            string texto = "";
            if (NovoAgendamento)
                texto = "Consulta na especialidade " + noEspec + " com " + (masc ? "o Dr" : "a Dra ") + noCol + " agendada para o dia " + data.ToString("dd/MM") + ", às " + hora;
            else
                texto = "Consulta na especialidade " + noEspec + " com " + (masc ? "o Dr" : "a Dra ") + noCol + " reagendada para o dia " + data.ToString("dd/MM") + ", às " + hora;

            //Envia a mensagem apenas se o número do celular for diferente de nulo
            if (NU_CELULAR != null)
            {
                var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
                string retorno = "";

                if (admUsuario.QT_SMS_MES_USR != null && admUsuario.QT_SMS_MAXIM_USR != null)
                {
                    if (admUsuario.QT_SMS_MAXIM_USR <= admUsuario.QT_SMS_MES_USR)
                    {
                        retorno = "Saldo do envio de SMS para outras pessoas ultrapassado.";
                        return;
                    }
                }

                if (!Page.IsValid)
                    return;
                try
                {
                    //Salva na tabela de mensagens enviadas, as informações pertinentes
                    TB249_MENSAG_SMS tb249 = new TB249_MENSAG_SMS();
                    tb249.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                    tb249.CO_RECEPT = CO_ALU;
                    tb249.CO_EMP_RECEPT = CO_EMP;
                    tb249.NO_RECEPT_SMS = NO_ALU != "" ? NO_ALU : NO_ALU;
                    tb249.DT_ENVIO_MENSAG_SMS = DateTime.Now;
                    tb249.DES_MENSAG_SMS = texto.Length > 150 ? texto.Substring(0, 150) : texto;
                    tb249.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    string desLogin = LoginAuxili.DESLOGIN.Trim().Length > 15 ? LoginAuxili.DESLOGIN.Trim().Substring(0, 15) : LoginAuxili.DESLOGIN.Trim();

                    SMSAuxili.SMSRequestReturn sMSRequestReturn = SMSAuxili.EnvioSMS(desLogin, Extensoes.RemoveAcentuacoes(texto + "(" + desLogin + ")"),
                                                "55" + NU_CELULAR.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", ""),
                                                DateTime.Now.Ticks.ToString());

                    if ((int)sMSRequestReturn == 0)
                    {
                        admUsuario.QT_SMS_TOTAL_USR = admUsuario.QT_SMS_TOTAL_USR != null ? admUsuario.QT_SMS_TOTAL_USR + 1 : 1;
                        admUsuario.QT_SMS_MES_USR = admUsuario.QT_SMS_MES_USR != null ? admUsuario.QT_SMS_MES_USR + 1 : 1;
                        ADMUSUARIO.SaveOrUpdate(admUsuario, false);

                        tb249.FLA_SMS_SUCESS = "S";
                    }
                    else
                        tb249.FLA_SMS_SUCESS = "N";

                    tb249.CO_TP_CONTAT_SMS = "A";

                    if ((int)sMSRequestReturn == 13)
                        retorno = "Número do destinatário está incompleto ou inválido.";
                    else if ((int)sMSRequestReturn == 80)
                        retorno = "Já foi enviada uma mensagem de sua conta com o mesmo identificador.";
                    else if ((int)sMSRequestReturn == 900)
                        retorno = "Erro de autenticação em account e/ou code.";
                    else if ((int)sMSRequestReturn == 990)
                        retorno = "Seu limite de segurança foi atingido. Contate nosso suporte para verificação/liberação.";
                    else if ((int)sMSRequestReturn == 998)
                        retorno = "Foi invocada uma operação inexistente.";
                    else if ((int)sMSRequestReturn == 999)
                        retorno = "Erro desconhecido. Contate nosso suporte.";


                    tb249.ID_MENSAG_OPERAD = (int)sMSRequestReturn;

                    if ((int)sMSRequestReturn == 0)
                        tb249.CO_STATUS = "E";
                    else
                        tb249.CO_STATUS = "N";

                    TB249_MENSAG_SMS.SaveOrUpdate(tb249, false);
                }
                catch (Exception)
                {
                    retorno = "Mensagem não foi enviada com sucesso.";
                }
                //GestorEntities.CurrentContext.SaveChanges();
            }
        }

        /// <summary>
        /// Carrega Indicacao
        /// </summary>
        private void CarregarIndicacao()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlIndicacao, LoginAuxili.CO_EMP, false, "0", true, 0, false);
        }

        /// <summary>
        /// Carrega as unidades de acordo com a Instituição logada.
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaUnidades(DropDownList ddl, bool selecionar = true)
        {
            AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true, true, selecionar);

            //Carrega apenas as unidades que possuem algum colaborador com FLAG de Profissional de Saúde
            /*var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                       where tb03.FLA_PROFESSOR == "S"
                       select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(w => w.NO_FANTAS_EMP).ToList();

            ddl.DataTextField = "NO_FANTAS_EMP";
            ddl.DataValueField = "CO_EMP";
            ddl.DataSource = res;
            ddl.DataBind();

            if (res.Count() > 0)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Sem Unidades com Plantonistas", ""));*/
        }

        /// <summary>
        /// Carrega as Classificações Profissionais
        /// </summary>
       

        /// <summary>
        /// Responsável por carregar os profissionais
        /// </summary>
        private void CarregaProfissionais()
        {
            //AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfissional, 0, true, "0", true);
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            //if (drpProfissional.SelectedValue == "0" && ddlLocalPaciente.SelectedValue == "0")
            //    AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
            //else
            //{
            //    int localPaciente = ddlLocalPaciente.SelectedValue != "" ? int.Parse(ddlLocalPaciente.SelectedValue) : 0;
            //    int Profissional = drpProfissional.SelectedValue != "" ? int.Parse(drpProfissional.SelectedValue) : 0;

            //    var res = new List<Aluno>();

            //    if (localPaciente != 0)
            //    {
            //        res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
            //               where (localPaciente != 0 ? tb07.CO_EMP_ORIGEM == localPaciente : 0 == 0)
            //               select new Aluno { NO_ALU = tb07.NO_ALU, CO_ALU = tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
            //    }
            //    else
            //    {
            //        res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
            //               join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
            //               where (Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0)
            //               select new Aluno { NO_ALU = tb07.NO_ALU, CO_ALU = tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
            //    }

            //    if (res != null)
            //    {
            //        ddlNomeUsu.DataTextField = "NO_ALU";
            //        ddlNomeUsu.DataValueField = "CO_ALU";
            //        ddlNomeUsu.DataSource = res;
            //        ddlNomeUsu.DataBind();
            //    }

            //    ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));
            //}
        }

        public class Aluno
        {
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
        }

        /// <summary>
        /// Carrega os departamentos de acordo com a empresa selecionada.
        /// </summary>
        private void CarregaDepartamento(DropDownList drpLocal, DropDownList drpUnid)
        {
            int coEmp = (drpUnid.SelectedValue != "" ? int.Parse(drpUnid.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaDepartamentos(drpLocal, coEmp, true);

            /*var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                       where (coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).Distinct().OrderBy(i => i.NO_DEPTO).ToList();

            ddlDept.DataTextField = "NO_DEPTO";
            ddlDept.DataValueField = "CO_DEPTO";
            ddlDept.DataSource = res;
            ddlDept.DataBind();

            ddlDept.Items.Insert(0, new ListItem("Todos", "0"));*/
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
            //if (uf != "")
            //{
            //    var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
            //               where tb904.CO_UF == uf
            //               select new { tb904.NO_CIDADE, tb904.CO_CIDADE });

            //    ddlCidade.DataTextField = "NO_CIDADE";
            //    ddlCidade.DataValueField = "CO_CIDADE";
            //    ddlCidade.DataSource = res;
            //    ddlCidade.DataBind();

            //    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            //}
            //else
            //{
            //    ddlCidade.Items.Clear();
            //    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            //}
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaBairros(ddlBairro, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Limpa a grid de horários
        /// </summary>
      

        /// <summary>
        /// é chamado quando se altera data de início ou final do parâmetro ou se clica em horários disponíveis
        /// </summary>
       

        /// <summary>
        /// Carrega o histórico de agendamentos do paciente recebido como parâmetro;
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricoPaciente(int CO_ALU)
        {
            //int unid = (!string.IsNullOrEmpty(ddlUnidHisPaciente.SelectedValue) ? int.Parse(ddlUnidHisPaciente.SelectedValue) : 0);
            //int local = (!string.IsNullOrEmpty(drpLocalCons.SelectedValue) ? int.Parse(drpLocalCons.SelectedValue) : 0);
            //DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHistoUsuar.Text) ? DateTime.Parse(txtDtIniHistoUsuar.Text) : DateTime.Now);
            //DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHistoUsuar.Text) ? DateTime.Parse(txtDtFimHistoUsuar.Text) : DateTime.Now);

            //var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_EMP equals tb14.TB25_EMPRESA.CO_EMP
            //           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
            //           where tbs174.CO_ALU == CO_ALU
            //           && (tb14.CO_DEPTO == tbs174.CO_DEPT)
            //           && (unid != 0 ? tbs174.CO_EMP == unid : 0 == 0)
            //           && (ddlTipoAgendHistPaciente.SelectedValue != "0" ? tbs174.TP_AGEND_HORAR == ddlTipoAgendHistPaciente.SelectedValue : 0 == 0)
            //           && (local != 0 ? tbs174.CO_DEPT == local : 0 == 0)
            //           && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
            //           && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
            //           //&&   a.DT_AGEND_HORAR >= dtIni && a.DT_AGEND_HORAR <= dtFim
            //           select new HorarioHistoricoPaciente
            //           {
            //               LOCAL = tb14.CO_SIGLA_DEPTO,
            //               APELIDO_PROFISSIONAL = tb03.NO_APEL_COL != null ? tb03.NO_APEL_COL : " - ",
            //               TP_PROCED_X = tbs174.TP_AGEND_HORAR,
            //               TP_PROCED_V = tbs174.TBS356_PROC_MEDIC_PROCE != null ? tbs174.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI : "-",
            //               DT = tbs174.DT_AGEND_HORAR,
            //               HR = tbs174.HR_AGEND_HORAR,
            //               TP_CONTRATO = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : " - ",

            //               Situacao = tbs174.CO_SITUA_AGEND_HORAR,
            //               agendaConfirm = tbs174.FL_CONF_AGEND,
            //               agendaEncamin = tbs174.FL_AGEND_ENCAM,
            //               faltaJustif = tbs174.FL_JUSTI_CANCE
            //           }).OrderBy(w => w.DT).ToList();

            //grdHistorPaciente.DataSource = res;
            //grdHistorPaciente.DataBind();
        }

        /// <summary>
        /// Carrega Operadora e Plano de saúde do paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        //private void SelecionaOperadoraPlanoPaciente(int? CO_ALU, bool Desmarca = false)
        //{
        //    foreach (GridViewRow li in grdHorario.Rows)
        //    {
        //        if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
        //        {
        //            DropDownList ddlOper = (DropDownList)li.Cells[5].FindControl("ddlOperAgend");
        //            DropDownList ddlPlan = (DropDownList)li.Cells[6].FindControl("ddlPlanoAgend");
        //            CarregaOperadoras(ddlOper, "");
        //            CarregarPlanosSaude(ddlPlan, ddlOper);

        //            if (CO_ALU.HasValue)
        //            {
        //                var res = TB07_ALUNO.RetornaPeloCoAlu(CO_ALU.Value);
        //                res.TB250_OPERAReference.Load();
        //                res.TB251_PLANO_OPERAReference.Load();

        //                //Se houver operadora
        //                if (res.TB250_OPERA != null)
        //                {
        //                    ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

        //                    CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
        //                    res.TB251_PLANO_OPERAReference.Load();
        //                    if (res.TB251_PLANO_OPERA != null) //Se houver plano
        //                        ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
        //                }
        //                else
        //                {
        //                    ddlOper.SelectedValue =
        //                    ddlPlan.SelectedValue = "";
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Carrega os tipos de agendamentos e já seleciona o recebido como parâmetro
        /// </summary>
        private void CarregarTiposAgendamentos(DropDownList ddl, string selec, bool InsereVazio = false)
        {
            AuxiliCarregamentos.CarregaTiposAgendamento(ddl, false, false, InsereVazio, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
            ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Carrega os tipos de consulta
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="selec"></param>
        private void CarregaTiposConsulta(DropDownList ddl, string selec, bool InsereVazio = false)
        {
            AuxiliCarregamentos.CarregaTiposConsulta(ddl, false, InsereVazio);
            ddl.SelectedValue = selec;
        }


        private void CarregaClassTiposConsulta(DropDownList ddl, string selec, string tipoConsulta)
        {
            AuxiliCarregamentos.CarregaClassTiposConsulta(ddl, tipoConsulta);
        }
        /// <summary>
        /// Carrega os procedimentos da instituição e seleciona o recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="selec"></param>
        private void CarregaProcedimentos(DropDownList ddl, DropDownList ddlOper, string selec = null)
        {
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_SITU_PROC_MEDI == "A"
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = "CO_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (!string.IsNullOrEmpty(selec) && ddl.Items.FindByValue(selec) != null)
                ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Carrega as operadoras de plano de saúde
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true, true, true);
        }

        /// <summary>
        /// Sobrecarga do método que carrega as operadoras de plano de saúde já selecionando o valor recebido como parâmetro
        /// </summary>
        private void CarregaOperadoras(DropDownList ddl, string selec)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true);
            ddl.SelectedValue = selec;
        }

        private void CarregaPlanoSaudeM()
        {

            //if (ddlOperadoraM.SelectedValue != "" || ddlOperadoraM.SelectedValue != null)
            //{
            //    AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanoM, ddlOperadoraM.SelectedValue, false, false, false);
            //    ddlPlanoM.Items.Insert(0, new ListItem("", ""));
            //    return;
            //}

        }

        /// <summary>
        /// Carrega os planos de saúde da operadora recebida como parâmetro
        /// </summary>
        /// <param name="ddlPlan"></param>
        /// <param name="ddlOper"></param>
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, false, true);
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal da função informada
        /// </summary>
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

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        //private void CarregaGridHorario(int coCol, bool HorariosDisponiveis)
        //{
        //    if (txtDtIniResCons.Text == null || txtDtIniResCons.Text == "")
        //    {

        //    }

        //    DateTime? dtIni = txtDtIniResCons.Text != "" ? DateTime.Parse(txtDtIniResCons.Text) : (DateTime?)null;
        //    DateTime? dtFim = txtDtFimResCons.Text != "" ? DateTime.Parse(txtDtFimResCons.Text) : (DateTime?)null;
        //    if (dtIni > dtFim)
        //    {
        //        AuxiliPagina.EnvioMensagemErro(this.Page, "A data final não pode ser maior do que a inicial");
        //        return;
        //    }
        //    if (chkDom.Checked == false && chkSeg.Checked == false && chkTer.Checked == false && chkQua.Checked == false && chkQui.Checked == false && chkSex.Checked == false && chkSab.Checked == false)
        //    {
        //        AuxiliPagina.EnvioMensagemErro(this.Page, "Pelo menos um dia da semana deve ser selecionado");
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
        //    {
        //        var pac = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));

        //        pac.TB250_OPERAReference.Load();

        //        if (pac.TB250_OPERA != null && ddlOpers.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
        //            ddlOpers.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
        //    }

        //    TimeSpan? hrInicio = txtHrIni.Text != "" ? TimeSpan.Parse(txtHrIni.Text) : (TimeSpan?)null;
        //    TimeSpan? hrFim = txtHrFim.Text != "" ? TimeSpan.Parse(txtHrFim.Text) : (TimeSpan?)null;

        //    //Trata as datas para poder compará-las com as informações no banco
        //    string dataConver = dtIni.Value.ToString("yyyy/MM/dd");
        //    DateTime dtInici = DateTime.Parse(dataConver);

        //    //Trata as datas para poder compará-las com as informações no banco
        //    string dataConverF = dtFim.Value.ToString("yyyy/MM/dd");
        //    DateTime dtFimC = DateTime.Parse(dataConverF);

        //    var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
        //               where a.CO_COL == coCol
        //               && (HorariosDisponiveis ? a.CO_ALU == null || a.CO_SITUA_AGEND_HORAR == "C" : 0 == 0)
        //               && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtInici)  //dtInici
        //               && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFimC))) //dtFimC
        //               select new HorarioSaida
        //               {
        //                   dt = a.DT_AGEND_HORAR,
        //                   hr = a.HR_AGEND_HORAR,
        //                   CO_ALU = a.CO_ALU,
        //                   TP_CONSUL = a.TP_CONSU,
        //                   CO_AGEND = a.ID_AGEND_HORAR,
        //                   FL_CONF = a.FL_CONF_AGEND,
        //                   CO_COL = a.CO_COL,
        //                   CO_DEPTO = a.CO_DEPT,
        //                   CO_EMP = a.CO_EMP,
        //                   CO_ESPEC = a.CO_ESPEC,
        //                   CO_TP_AGEND = a.TP_AGEND_HORAR,
        //                   CO_TP_CONSUL = a.TP_CONSU,
        //                   CO_PLAN = a.TB251_PLANO_OPERA.ID_PLAN,
        //                   CO_OPER = a.TB250_OPERA.ID_OPER,
        //                   ID_PROC = a.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
        //                   CO_CLASS_TP = a.TBS356_PROC_MEDIC_PROCE.CO_CLASS_FUNCI,
        //                   VL_CONSUL = a.VL_CONSUL,

        //                   Situacao = a.CO_SITUA_AGEND_HORAR,
        //                   agendaConfirm = a.FL_CONF_AGEND,
        //                   agendaEncamin = a.FL_AGEND_ENCAM,
        //                   faltaJustif = a.FL_JUSTI_CANCE
        //               }).OrderBy(w => w.dt).ToList();

        //    var lst = new List<HorarioSaida>();

        //    #region Verifica os itens a serem excluídos
        //    if (res.Count > 0)
        //    {
        //        int aux = 0;
        //        foreach (var i in res)
        //        {
        //            int dia = (int)i.dt.DayOfWeek;

        //            switch (dia)
        //            {
        //                case 0:
        //                    if (!chkDom.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //                case 1:
        //                    if (!chkSeg.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //                case 2:
        //                    if (!chkTer.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //                case 3:
        //                    if (!chkQua.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //                case 4:
        //                    if (!chkQui.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //                case 5:
        //                    if (!chkSex.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //                case 6:
        //                    if (!chkSab.Checked)
        //                    { lst.Add(i); }
        //                    break;
        //            }
        //            aux++;
        //        }
        //    }
        //    #endregion

        //    var resNew = res.Except(lst).ToList();

        //    //Se tiver horario de inicio, filtra
        //    if (hrInicio != null)
        //        resNew = resNew.Where(a => a.hrC >= hrInicio).ToList();

        //    //Se tiver horario de termino, filtra
        //    if (hrFim != null)
        //        resNew = resNew.Where(a => a.hrC <= hrFim).ToList();

        //    //Reordena
        //    resNew = resNew.OrderBy(w => w.dt).ThenBy(w => w.hrC).ThenBy(w => w.NO_PAC).ToList();

        //    grdHorario.DataSource = resNew;
        //    grdHorario.DataBind();

        //    foreach (GridViewRow li in grdHorario.Rows)
        //    {
        //        var ddlTipoAgendam = (DropDownList)li.FindControl("ddlTipoAgendam");
        //        var tb03 = TB03_COLABOR.RetornaPeloCoCol(coCol);

        //        ddlTipoAgendam.Enabled = true;

        //        ddlTipoAgendam.SelectedValue = !String.IsNullOrEmpty(tb03.CO_CLASS_PROFI) ? tb03.CO_CLASS_PROFI.Equals("E") ? "EN" : tb03.CO_CLASS_PROFI.Equals("I") ? "FI" :
        //                                       tb03.CO_CLASS_PROFI.Equals("F") ? "FO" : tb03.CO_CLASS_PROFI.Equals("M") ? "AM" : tb03.CO_CLASS_PROFI.Equals("P") ? "PI" :
        //                                       tb03.CO_CLASS_PROFI.Equals("D") ? "AO" : tb03.CO_CLASS_PROFI.Equals("S") ? "ES" : tb03.CO_CLASS_PROFI.Equals("N") ? "NT" :
        //                                       tb03.CO_CLASS_PROFI.Equals("T") ? "TO" : "0" : "0";
        //        if(!ddlTipoAgendam.SelectedValue.Equals("0")){
        //            ddlTipoAgendam.Enabled = false;
        //        }
        //    }

        //    //foreach (GridViewRow li in grdHorario.Rows)
        //    //{
        //    //    //Plano de Saúde
        //    //    string idPlan = ((HiddenField)li.Cells[6].FindControl("hidIdPlan")).Value;
        //    //    DropDownList ddlPlano = ((DropDownList)li.Cells[6].FindControl("ddlPlanoAgend"));
        //    //    CarregaPlanoSaude(ddlPlano, idPlan);
        //    //}
        //}

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RG_RESP,
                           tb108.CO_ORG_RG_RESP,
                           tb108.NU_CPF_RESP,
                           tb108.CO_ESTA_RG_RESP,
                           tb108.DT_NASC_RESP,
                           tb108.CO_SEXO_RESP,
                           tb108.CO_CEP_RESP,
                           tb108.CO_ESTA_RESP,
                           tb108.CO_CIDADE,
                           tb108.CO_BAIRRO,
                           tb108.DE_ENDE_RESP,
                           tb108.DES_EMAIL_RESP,
                           tb108.NU_TELE_CELU_RESP,
                           tb108.NU_TELE_RESI_RESP,
                           tb108.CO_ORIGEM_RESP,
                           tb108.CO_RESP,
                           tb108.NU_TELE_WHATS_RESP,
                           tb108.NM_FACEBOOK_RESP,
                           tb108.NU_TELE_COME_RESP,
                       }).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP;
                txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                txtCEP.Text = res.CO_CEP_RESP;
                ddlUF.SelectedValue = res.CO_ESTA_RESP;
                carregaCidade();
                ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
            }
            //updCadasUsuario.Update();
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                txtCPFMOD.Text = txtCPFResp.Text;
                txtnompac.Text = txtNomeResp.Text;
                txtDtNascPaci.Text = txtDtNascResp.Text;
                ddlSexoPaci.SelectedValue = ddlSexResp.SelectedValue;
                txtTelCelPaci.Text = txtTelCelResp.Text;
                txtTelResPaci.Text = txtTelFixResp.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailPaci.Text = txtEmailResp.Text;
                txtWhatsPaci.Text = txtNuWhatsResp.Text;

                txtEmailPaci.Enabled = false;
                txtCPFMOD.Enabled = false;
                txtnompac.Enabled = false;
                txtDtNascPaci.Enabled = false;
                ddlSexoPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelResPaci.Enabled = false;
                ddlGrParen.Enabled = false;
                txtWhatsPaci.Enabled = false;
            }
            else
            {
                txtCPFMOD.Text = "";
                txtnompac.Text = "";
                txtDtNascPaci.Text = "";
                ddlSexoPaci.SelectedValue = "";
                txtTelCelPaci.Text = "";
                txtTelResPaci.Text = "";
                ddlGrParen.SelectedValue = "";
                txtEmailPaci.Text = "";
                txtWhatsPaci.Text = "";

                txtCPFMOD.Enabled = true;
                txtnompac.Enabled = true;
                txtDtNascPaci.Enabled = true;
                ddlSexoPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelResPaci.Enabled = true;
                ddlGrParen.Enabled = true;
                txtEmailPaci.Enabled = true;
                txtWhatsPaci.Enabled = true;
                hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        public class HorarioHistoricoPaciente
        {
            public string LOCAL { get; set; }
            public string TP_PROCED { get { return this.TP_PROCED_X + " - " + this.TP_PROCED_V; } }
            public string TP_PROCED_X { get; set; }
            private string TP_PROCED_v;
            public string TP_PROCED_V
            {
                get
                {
                    switch (this.TP_PROCED_v)
                    {
                        case "CO":
                            return "Consulta";
                        case "EX":
                            return "Exame";
                        case "SS":
                            return "Serv.Saúde";
                        case "PR":
                            return "Procedimento";
                        case "SA":
                            return "Serv. Ambulatorial";
                        case "OU":
                            return "Outros";
                        default:
                            return " - ";
                    }
                }
                set
                {
                    TP_PROCED_v = value;
                }
            }
            public string APELIDO_PROFISSIONAL { get; set; }
            public string STATUS_V
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarSituacaoAgend(Situacao, agendaEncamin, agendaConfirm, faltaJustif);
                }
            }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DT_HORAR
            {
                get
                {
                    string diaSemana = this.DT.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.DT.ToShortDateString() + " - " + this.HR + " " + diaSemana;
                }
            }
            public string TP_CONTRATO { get; set; }

            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
        }

        public class HorarioSaida
        {
            //Carrega informações gerais do agendamento
            public DateTime dt { get; set; }
            public string hr { get; set; }
            public TimeSpan hrC
            {
                get
                {
                    //DateTime d = DateTime.Parse(hr);
                    return TimeSpan.Parse((hr + ":00"));
                }
            }
            public string hora
            {
                get
                {
                    string diaSemana = this.dt.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + diaSemana;
                }
            }
            public int CO_AGEND { get; set; }
            public int? CO_COL { get; set; }
            public int? CO_ESPEC { get; set; }
            public int? CO_DEPTO { get; set; }
            public int? CO_EMP { get; set; }
            public decimal? VL_CONSUL { get; set; }

            //Carrega as informações do usuário quando já houver agendamento para o horário em questão
            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
                }
            }
            public string NO_PAC
            {
                get
                {
                    var nome = "";
                    try
                    {
                        return (this.CO_ALU.HasValue ? TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == this.CO_ALU).FirstOrDefault().NO_ALU : " - ");
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        nome = "-";
                    }
                    if (!nome.Equals("") || !nome.Equals("-"))
                    {
                        return nome;
                    }
                    else
                    {
                        return nome;
                    }
                }
            }
            public int? CO_ALU { get; set; }
            public string CO_SITU_VALID
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarSituacaoAgend(Situacao, agendaEncamin, agendaConfirm, faltaJustif);
                }
            }
            public string TP_CONSUL { get; set; }
            public string TP_CONSUL_VALID
            {
                get
                {
                    string tipo = "";
                    switch (this.TP_CONSUL)
                    {
                        case "N":
                            tipo = "Normal";
                            break;
                        case "R":
                            tipo = "Retorno";
                            break;
                        case "U":
                            tipo = "Urgência";
                            break;
                        default:
                            tipo = " - ";
                            break;
                    }
                    return tipo;
                }
            }
            public string NO_OPERA { get; set; }

            public string CO_TP_AGEND { get; set; }
            public string CO_TP_CONSUL { get; set; }
            public int? CO_OPER { get; set; }
            public int? CO_PLAN { get; set; }
            public int? ID_PROC { get; set; }
            public string CO_CLASS_TP { get; set; }

            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
        }

        /// <summary>
        /// Carrega a grid de profissionais da saúde
        /// </summary>
        
        public class GrdProfiSaida
        {
            public int CO_COL { get; set; }
            public string MATR_COL { get; set; }
            public string NO_COL
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0').Insert(2, ".").Insert(6, "-");
                    string noCol = (this.NO_COL_RECEB.Length > 27 ? this.NO_COL_RECEB.Substring(0, 27) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_COL_RECEB { get; set; }
            public string NO_EMP { get; set; }
            public string DE_ESP { get; set; }
            public string NU_TEL { get; set; }
            public string NU_TEL_V
            {
                get
                {
                    return AuxiliFormatoExibicao.PreparaTelefone(this.NU_TEL);
                }
            }
            public string CO_CLASS_FUNC { get; set; }
            public string DE_CLASS_FUNC
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_FUNC);
                }
            }
        }

        /// <summary>
        /// Retorna um objeto do planejamento de determinad paciente/agenda recebidos como parâmetro
        /// </summary>
        /// <returns></returns>
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
                TBS370_PLANE_AVALI.SaveOrUpdate(tbs370, true);

                return tbs370;
            }
        }

        /// <summary>
        /// Retorna o último número de ação encontrado (para o planejamento recebido como parâmetro) + 1
        /// </summary>
        /// <param name="CO_ALU"></param>
        /// <param name="ID_PROC"></param>
        private int RecuperaUltimoNrAcao(int ID_PLANE_AVALI)
        {
            var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                       where tbs389.TBS386_ITENS_PLANE_AVALI.TBS370_PLANE_AVALI.ID_PLANE_AVALI == ID_PLANE_AVALI
                       select new { tbs389.TBS386_ITENS_PLANE_AVALI.NR_ACAO }).OrderByDescending(w => w.NR_ACAO).FirstOrDefault();

            /*
             *Retorna o último número de ação encontrado (para a agenda e procedimento recebidos como parâmetro) + 1.
             *Se não houver, retorna o número 1
             */
            return (res != null ? res.NR_ACAO + 1 : 1);
        }

        #endregion

        #region Eventos de componentes

        protected void grdHorario_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Tipo da Agenda
                string tipoAgenda = ((HiddenField)e.Row.Cells[3].FindControl("hidTpAgend")).Value;
                DropDownList ddlTipoAgenda = ((DropDownList)e.Row.Cells[3].FindControl("ddlTipoAgendam"));
                CarregarTiposAgendamentos(ddlTipoAgenda, tipoAgenda);

                //Tipo Consutla
                string tipoConsul = ((HiddenField)e.Row.Cells[4].FindControl("hidTpConsul")).Value;
                DropDownList ddlTipoConsul = ((DropDownList)e.Row.Cells[4].FindControl("ddlTipo"));
                CarregaTiposConsulta(ddlTipoConsul, tipoConsul);

                //Operadora
                string idOper = ((HiddenField)e.Row.Cells[5].FindControl("hidIdOper")).Value;
                DropDownList ddlOper = ((DropDownList)e.Row.Cells[5].FindControl("ddlOperAgend"));
                CarregaOperadoras(ddlOper, idOper);

                //Plano de Saúde
                string idPlan = ((HiddenField)e.Row.Cells[6].FindControl("hidIdPlan")).Value;
                DropDownList ddlPlano = ((DropDownList)e.Row.Cells[6].FindControl("ddlPlanoAgend"));
                CarregarPlanosSaude(ddlPlano, ddlOper);
                ddlPlano.SelectedValue = idPlan;

                //Procedimento
                string idProced = ((HiddenField)e.Row.Cells[7].FindControl("hidIdProced")).Value;
                DropDownList ddlProced = ((DropDownList)e.Row.Cells[7].FindControl("ddlProcedAgend"));
                CarregaProcedimentos(ddlProced, ddlOper, idProced);

                //if (!chkHorDispResCons.Enabled)
                //{
                //    ddlTipoConsul.Enabled = false;

                //    if (String.IsNullOrEmpty(tipoConsul))
                //        ddlTipoConsul.SelectedValue = "E";
                //    else
                //        //ddlTipoAgenda.Enabled =
                //        ddlOper.Enabled =
                //        ddlPlano.Enabled =
                //        ddlProced.Enabled = false;
                //}
            }
        }

        protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

   
        protected void imgCpfPac_OnClick(object sender, EventArgs e)
        {
            CarregaPacientes();
            PesquisaPaciente();
            OcultarPesquisa(true);
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                        && tb07.CO_SITU_ALU != "H" && tb07.CO_SITU_ALU != "O"
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlNomeUsu.DataTextField = "NO_ALU";
                ddlNomeUsu.DataValueField = "CO_ALU";
                ddlNomeUsu.DataSource = res;
                ddlNomeUsu.DataBind();
            }

            ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePacPesq.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlNomeUsu.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        //protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    if (((CheckBox)sender).Checked == true)
        //    {
        //        txtNirePaci.Enabled = true;
        //        chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
        //        txtCPFPaci.Text = "";
        //    }
        //    else
        //    {
        //        txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";
        //    }
        //}

        //protected void chkPesqCpf_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    if (((CheckBox)sender).Checked == true)
        //    {
        //        txtCPFPaci.Enabled = true;
        //        chkPesqNire.Checked = txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";
        //    }
        //    else
        //    {
        //        txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";
        //    }
        //}

        //public void PermissaoMultiplasAgenda(object sender, EventArgs e)
        //{
        //    CheckBox atual = (CheckBox)sender;
        //    bool selec = false;

        //    foreach (GridViewRow l in grdHorario.Rows)
        //    {
        //        CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelectHr"));

        //        if (atual.ClientID != chk.ClientID)
        //        {
        //            if (chk.Checked == false)
        //            {
        //                //Coleta e trata o código do paciente
        //                string coAlu = ((HiddenField)l.Cells[0].FindControl("hidCoAlu")).Value;
        //                int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);
        //                string coAgend = ((HiddenField)l.Cells[0].FindControl("hidCoAgenda")).Value;

        //                //Verifica se o funcionário logado possui permissão para agenda múltiplica
        //                bool PermissMultiAgenda = (ADMUSUARIO.RetornaTodosRegistros().Where(w => w.CodUsuario == LoginAuxili.CO_COL && w.FL_PERMI_AGEND_MULTI == "S").Any());

        //                //Se o usuário não tiver permissão para multiplo agendamento e hover algum paciente na agenda em questão,
        //                //entra em modo de edição, carregando as informações correspondentes nos campos
        //                if ((!PermissMultiAgenda) && (!string.IsNullOrEmpty(coAlu)) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)))
        //                {
        //                    ScriptManager.RegisterStartupScript(
        //                       this.Page,
        //                       this.GetType(),
        //                       "Erro",
        //                       "mostraErroPermi();",
        //                       true
        //                   );

        //                    chk.Checked = false;

        //                    #region Multiagenda não permitida

        //                    //Coleta o tipo da consulta
        //                    string tpCon = ((HiddenField)l.Cells[0].FindControl("hidTpCons")).Value;

        //                    //Coleta controles usados
        //                    DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
        //                    DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");
        //                    DropDownList ddlClass = (((DropDownList)l.Cells[3].FindControl("ddlTipoAgendam")));
        //                    DropDownList ddlTipo = (((DropDownList)l.Cells[4].FindControl("ddlTipo")));

        //                    if (ddlNomeUsu.Items.Contains(new ListItem("", coAluI.ToString())))
        //                        ddlNomeUsu.SelectedValue = coAluI.ToString();

        //                    if (chk.Checked)
        //                    {
        //                        if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
        //                            ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

        //                        if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
        //                            ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados

        //                        hidCoConsul.Value = coAgend.ToString();

        //                        //Marca que foi selecionado para que os outros itens não desabilitem o lnk.
        //                        if (coAluI != 0)
        //                            selec = true;

        //                        #region Seleciona Operadora e Plano do Paciente

        //                        if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
        //                        {
        //                            var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
        //                            res.TB250_OPERAReference.Load();
        //                            res.TB251_PLANO_OPERAReference.Load();

        //                            //Se houver operadora
        //                            if (res.TB250_OPERA != null)
        //                            {
        //                                ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

        //                                CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
        //                                res.TB251_PLANO_OPERAReference.Load();
        //                                if (res.TB251_PLANO_OPERA != null) //Se houver plano
        //                                    ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
        //                            }
        //                        }

        //                        #endregion

        //                        //Se houver 
        //                        //if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
        //                        //    SelecionaOperadoraPlanoPaciente(int.Parse(ddlNomeUsu.SelectedValue));
        //                    }
        //                    else
        //                    {
        //                        ddlClass.SelectedValue = ddlTipo.SelectedValue = ""; //Seleciona vazio novamente

        //                        CarregaOperadoras(ddlOper, "");
        //                        CarregarPlanosSaude(ddlPlan, ddlOper);
        //                        if (!selec)
        //                            hidCoConsul.Value = "";
        //                    }

        //                    #endregion
        //                }

        //                if ((PermissMultiAgenda) && (coAlu != ""))
        //                {
        //                    hidMultiAgend.Value = "S";
        //                    hidEspelhoAgenda.Value = coAgend;
        //                }
        //            }
        //            else
        //                hidMultiAgend.Value = hidEspelhoAgenda.Value = "";
        //        }
        //        else
        //        {
        //            //Desabilita o lnk caso não tenha sido selecionado nenhum item anteriormente
        //            if (!selec)
        //                hidCoConsul.Value = "";
        //        }
        //    }
        //    //UpdHora.Update();
        //}

        //protected void ckSelectHr_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox atual = (CheckBox)sender;
        //    bool selec = false;

        //    foreach (GridViewRow l in grdHorario.Rows)
        //    {
        //        CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelectHr"));

        //        if (atual.ClientID == chk.ClientID)
        //        {
        //            if (chk.Checked)
        //            {
        //                l.BackColor = ColorTranslator.FromHtml("#e0eeee");

        //                //Coleta e trata o código do paciente
        //                string coAlu = ((HiddenField)l.Cells[0].FindControl("hidCoAlu")).Value;
        //                int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);
        //                string coAgend = ((HiddenField)l.Cells[0].FindControl("hidCoAgenda")).Value;

        //                DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
        //                DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");

        //                //Verifica se o funcionário logado possui permissão para agenda múltiplica
        //                bool PermissMultiAgenda = (ADMUSUARIO.RetornaTodosRegistros().Where(w => w.CodUsuario == LoginAuxili.CO_COL && w.FL_PERMI_AGEND_MULTI == "S").Any());

        //                //Se o usuário não tiver permissão para multiplo agendamento e hover algum paciente na agenda em questão,
        //                //entra em modo de edição, carregando as informações correspondentes nos campos
        //                if ((!PermissMultiAgenda) && (!string.IsNullOrEmpty(coAlu)) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)))
        //                {
        //                    ScriptManager.RegisterStartupScript(
        //                       this.Page,
        //                       this.GetType(),
        //                       "Erro",
        //                       "mostraErroPermi();",
        //                       true
        //                   );

        //                    chk.Checked = false;

        //                    #region Multiagenda não permitida

        //                    //Coleta o tipo da consulta
        //                    string tpCon = ((HiddenField)l.Cells[0].FindControl("hidTpCons")).Value;

        //                    //Coleta controles usados

        //                    DropDownList ddlClass = (((DropDownList)l.Cells[3].FindControl("ddlTipoAgendam")));
        //                    DropDownList ddlTipo = (((DropDownList)l.Cells[4].FindControl("ddlTipo")));

        //                    if (ddlNomeUsu.Items.Contains(new ListItem("", coAluI.ToString())))
        //                        ddlNomeUsu.SelectedValue = coAluI.ToString();

        //                    if (chk.Checked)
        //                    {
        //                        if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
        //                            ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

        //                        if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
        //                            ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados

        //                        hidCoConsul.Value = coAgend.ToString();

        //                        //Marca que foi selecionado para que os outros itens não desabilitem o lnk.
        //                        if (coAluI != 0)
        //                            selec = true;

        //                        //Se houver 
        //                        //if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
        //                        //    SelecionaOperadoraPlanoPaciente(int.Parse(ddlNomeUsu.SelectedValue));
        //                    }
        //                    else
        //                    {
        //                        ddlClass.SelectedValue = ddlTipo.SelectedValue = ""; //Seleciona vazio novamente

        //                        CarregaOperadoras(ddlOper, "");
        //                        CarregarPlanosSaude(ddlPlan, ddlOper);
        //                        if (!selec)
        //                            hidCoConsul.Value = "";
        //                    }

        //                    #endregion
        //                }

        //                if ((PermissMultiAgenda) && (coAlu != ddlNomeUsu.SelectedValue) && coAluI != 0)
        //                {
        //                    hidMultiAgend.Value = "S";
        //                    hidEspelhoAgenda.Value = coAgend;
        //                }


        //                #region Seleciona Operadora e Plano do Paciente

        //                if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
        //                {
        //                    var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
        //                    res.TB250_OPERAReference.Load();

        //                    //Se houver operadora
        //                    if (res.TB250_OPERA != null)
        //                    {
        //                        ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

        //                        CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
        //                        res.TB251_PLANO_OPERAReference.Load();
        //                        if (res.TB251_PLANO_OPERA != null) //Se houver plano
        //                            ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
        //                    }
        //                }

        //                #endregion
        //            }
        //            else
        //            {
        //                hidMultiAgend.Value = hidEspelhoAgenda.Value = "";
        //                l.BackColor = Color.Empty;
        //            }
        //        }
        //        else
        //        {
        //            //Desabilita o lnk caso não tenha sido selecionado nenhum item anteriormente
        //            if (!selec)
        //                hidCoConsul.Value = "";
        //        }
        //    }
        //    //UpdHora.Update();  
        //}

        //protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        //{
        //    CarregaGridHorariosAlter();
        //}

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            VerificarNireAutomatico();
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            //updCadasUsuario.Update();
            AbreModalPadrao("AbreModalInfosCadas();");

        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            chkPaciMoraCoResp.Checked = chkPaciEhResp.Checked;

            AbreModalPadrao("AbreModalInfosCadas();");
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
                    carregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro();
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
            AbreModalPadrao("AbreModalInfosCadas();");
            //updCadasUsuario.Update();
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
            AbreModalPadrao("AbreModalInfosCadas();");
        }

        protected void lnkCadastroCompleto_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("3106_CadastramentoUsuariosSimp/Busca.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                this.Session[SessoesHttp.URLOrigem] = HttpContext.Current.Request.Url.PathAndQuery;

                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo.Replace("Busca", "Cadastro"), HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
            }
        }

        protected void lnkSalvar_OnClick(object sender, EventArgs e)
        {
            try
            {
                AbreModalPadrao("AbreModalInfosCadas();");
                if (string.IsNullOrEmpty(txtnompac.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Paciente é requerido");
                    txtnompac.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtNomeResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nome do Responsável é requerido");
                    txtNomeResp.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(ddlSexoPaci.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Sexo do Paciente é requerido");
                    ddlSexoPaci.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtNuNis.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O Nire do Paciente é requerido");
                    txtNuNis.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtApelido.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O campo apelido e  obrigatório ");
                    txtApelido.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtDtNascResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Data nascimento e obrigatório ");
                    txtDtNascResp.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(txtDtNascPaci.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Data nascimento  e obrigatório ");
                    txtDtNascPaci.Focus();
                    //updCadasUsuario.Update();
                    return;
                }
                if (string.IsNullOrEmpty(ddlBairro.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione o bairro");
                    ddlBairro.Focus();
                    return;
                }

                var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                var cpfValido = true;

                if (!String.IsNullOrEmpty(txtCPFResp.Text))
                {
                    if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                        txtCPFResp.Focus();
                        return;
                    }
                }
                else if (tb25.FL_CPF_RESP_OBRIGATORIO == "S")
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do responsável é obrigatório");
                    txtCPFResp.Focus();
                    return;
                }
                else if (tb25.FL_CPF_RESP_OBRIGATORIO == "N" && String.IsNullOrEmpty(txtCPFResp.Text))
                {
                    var cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    //Enquanto existir, calcula um novo cpf
                    while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado || w.NU_CONTROLE == cpfGerado).Any())
                        cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    txtCPFResp.Text = cpfGerado;
                    cpfValido = false;
                }

                if (!String.IsNullOrEmpty(txtCPFMOD.Text))
                {
                    if (!AuxiliValidacao.ValidaCpf(txtCPFMOD.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do paciente invalido!");
                        txtCPFMOD.Focus();
                        return;
                    }
                }
                else if (tb25.FL_CPF_PAC_OBRIGATORIO == "S")
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente é obrigatório");
                    txtCPFMOD.Focus();
                    return;
                }

                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                TB108_RESPONSAVEL tb108;

                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                var cpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");
                var resp = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NU_CPF_RESP == cpfResp).FirstOrDefault();

                var respExist = false;
                if (resp == null && !cpfValido)
                {
                    var dtNasc = DateTime.Parse(txtDtNascResp.Text);
                    var res = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NO_RESP == txtNomeResp.Text && r.DT_NASC_RESP == dtNasc).FirstOrDefault();

                    if (res != null)
                    {
                        resp = res;
                        respExist = true;
                    }
                }

                if (resp != null && (!String.IsNullOrEmpty(cpfResp) || respExist))
                    tb108 = resp;
                else if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    string NomeApeliResp = "";

                    if (!string.IsNullOrEmpty(txtNomeResp.Text))
                    {
                        var nomeResp = txtNomeResp.Text.Split(' ');
                        NomeApeliResp = nomeResp[0] + (nomeResp.Length > 1 ? " " + nomeResp[1] : "");
                    }

                    tb108 = new TB108_RESPONSAVEL();

                    tb108.NU_CONTROLE =
                    tb108.NU_CPF_RESP = cpfResp;
                    tb108.FL_CPF_VALIDO = cpfValido ? "S" : "N";
                    tb108.NO_RESP = txtNomeResp.Text.ToUpper();
                    tb108.NO_APELIDO_RESP = NomeApeliResp.ToUpper();
                    tb108.CO_RG_RESP = txtNuIDResp.Text;
                    tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                    tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                    tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                    tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                    tb108.CO_CEP_RESP = txtCEP.Text;
                    tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                    tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                    tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                    tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                    tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                    tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.CO_ORIGEM_RESP = "NN";
                    tb108.CO_SITU_RESP = "A";
                    tb108.DT_SITU_RESP = DateTime.Now;

                    //Atribui valores vazios para os campos not null da tabela de Responsável.
                    tb108.FL_NEGAT_CHEQUE = "V";
                    tb108.FL_NEGAT_SERASA = "V";
                    tb108.FL_NEGAT_SPC = "V";
                    tb108.CO_INST = 0;
                    tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
                }
                else
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value));

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                TB07_ALUNO tb07;

                //Verifica antes se já existe o paciente algum paciente com o mesmo CPF informado nos campos, caso não exista, cria um novo
                var cpfPac = txtCPFMOD.Text.Replace(".", "").Replace("-", "");
                var pac = TB07_ALUNO.RetornaTodosRegistros().Where(a => a.NU_CPF_ALU == cpfPac).FirstOrDefault();

                var pacExist = false;
                if (pac == null && String.IsNullOrEmpty(cpfPac) || (pac != null && String.IsNullOrEmpty(cpfPac)))
                {
                    var dtNasc = DateTime.Parse(txtDtNascPaci.Text);
                    var res = TB07_ALUNO.RetornaTodosRegistros().Where(p => p.NO_ALU == txtnompac.Text && p.DT_NASC_ALU == dtNasc).FirstOrDefault();

                    if (res != null)
                    {
                        pac = res;
                        pacExist = true;
                    }
                }

                if (pac != null && (!String.IsNullOrEmpty(cpfPac) || pacExist))
                    tb07 = pac;
                else if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    tb07 = new TB07_ALUNO();

                    #region Bloco foto
                    int codImagem = upImagemAluno.GravaImagem();
                    tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                    #endregion

                    tb07.NO_ALU = txtnompac.Text.ToUpper();
                    tb07.NU_CPF_ALU = cpfPac;
                    tb07.NU_NIS = decimal.Parse(txtNuNis.Text);
                    tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                    tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                    tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_WHATS_ALU = txtWhatsPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                    tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                    tb07.CO_EMP = LoginAuxili.CO_EMP;
                    tb07.TB25_EMPRESA1 = tb25;
                    tb07.TB25_EMPRESA = tb25;
                    tb07.TB108_RESPONSAVEL = tb108;
                    tb07.NO_APE_ALU = txtApelido.Text.ToUpper();
                    if (chkPaciMoraCoResp.Checked)
                    {
                        tb07.CO_CEP_ALU = txtCEP.Text;
                        tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                        tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                        tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                    }

                    //Salva os valores para os campos not null da tabela de Usuário
                    tb07.CO_INST = LoginAuxili.ORG_CODIGO_ORGAO;
                    tb07.DT_CADA_ALU = tb07.DT_SITU_ALU = tb07.DT_ENTRA_INSTI = DateTime.Now;
                    tb07.CO_SITU_ALU = "A";
                    tb07.TP_DEF = "N";
                    tb07.FL_LIST_ESP = "N";

                    if (!String.IsNullOrEmpty(ddlIndicacao.SelectedValue))
                    {
                        tb07.CO_EMP_INDICACAO = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlIndicacao.SelectedValue)).CO_EMP;
                        tb07.CO_COL_INDICACAO = int.Parse(ddlIndicacao.SelectedValue);
                        tb07.DT_INDICACAO = DateTime.Now;
                    }

                    #region trata para criação do nire

                    var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                    int nir = 0;
                    if (res == null)
                    {
                        nir = 1;
                    }
                    else
                    {
                        nir = res.NU_NIRE;
                    }

                    int nirTot = nir + 1;

                    #endregion

                    tb07.NU_NIRE = nirTot;

                    tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
                }
                else
                    tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value));

                divResp.Visible = false;
                divSuccessoMessage.Visible = true;
                lblMsg.Text = "Usuário salvo com êxito!";
                lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
                lblMsg.Visible = true;
                lblMsgAviso.Visible = true;
                lblSitPaciente.Text = "EM ATENDIMENTO";
                lblSitPaciente.CssClass = "sitPacPadrao";

                CarregaPacientes();
                ddlNomeUsu.SelectedValue = tb07.CO_ALU.ToString();
                OcultarPesquisa(true);
                //updTopo.Update();

                #endregion
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        protected void ddlOperadora_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlPlano, ddlOperadora);
            CarregaProcedimentos(ddlProcedimento, ddlOperadora);
        }

        protected void ddlQtde_SelectedIndexChanged(object sender, EventArgs e)
        {
            int qtde = !string.IsNullOrEmpty(ddlQtde.SelectedValue) ? int.Parse(ddlQtde.SelectedValue) : 0;
            decimal vlUnit = !string.IsNullOrEmpty(txtValorUnit.Text) ? decimal.Parse(txtValorUnit.Text) : 0;
            txtValorTotal.Text = "R$ " + (qtde * vlUnit).ToString();
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProc = !string.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;
            var res = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(x => x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc).FirstOrDefault().VL_BASE;
            txtValorUnit.Text = res.ToString();
        }

        protected void drpProfissional_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPacientes();
            OcultarPesquisa(true);
        }

        protected void ddlLocalPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPacientes();
            OcultarPesquisa(true);
        }

        protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblSitPaciente.Text = " - ";

            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var coAlu = int.Parse(ddlNomeUsu.SelectedValue);
                CarregaGridHistoricoPaciente(coAlu);
                //SelecionaOperadoraPlanoPaciente(coAlu);

                var pac = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                pac.TB250_OPERAReference.Load();

                //if (pac.TB250_OPERA != null && ddlOpers.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                //    ddlOpers.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
                //else
                //    ddlOpers.SelectedValue = "";

                switch (pac.CO_SITU_ALU)
                {
                    case "A":
                        lblSitPaciente.Text = "EM ATENDIMENTO";
                        lblSitPaciente.CssClass = "sitPacPadrao";
                        break;
                    case "V":
                        lblSitPaciente.Text = "EM ANÁLISE";
                        lblSitPaciente.CssClass = "sitPacAnalise";
                        break;
                    case "E":
                        lblSitPaciente.Text = "ALTA (NORMAL)";
                        lblSitPaciente.CssClass = "sitPacAlta";
                        break;
                    case "D":
                        lblSitPaciente.Text = "ALTA (DESISTÊNCIA)";
                        lblSitPaciente.CssClass = "sitPacAlta";
                        break;
                    //case "I":
                    //    lblSitPaciente.Text = "Inativo";
                    //    lblSitPaciente.CssClass = "";
                    //    break;
                }

                //Verifica se o funcionário logado possui permissão para agenda múltiplica
                bool PermissMultiAgenda = (ADMUSUARIO.RetornaTodosRegistros().Where(w => w.CodUsuario == LoginAuxili.CO_COL && w.FL_PERMI_AGEND_MULTI == "S").Any());

                //Se o usuário não tiver permissão para multiplo agendamento e hover algum paciente na agenda em questão,
                //entra em modo de edição, carregando as informações correspondentes nos campos
                ////if ((!PermissMultiAgenda) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)) && (!string.IsNullOrEmpty(hidCoConsul.Value)))
                ////{
                ////    ScriptManager.RegisterStartupScript(
                ////       this.Page,
                ////       this.GetType(),
                ////       "Erro",
                ////       "mostraErroPermi();",
                ////       true
                ////   );

                ////    ddlNomeUsu.SelectedValue = "";
                ////}
            }
            //else
                //lecionaOperadoraPlanoPaciente((int?)null, true);
        }

        protected void imgPesqHistPaciente_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                CarregaGridHistoricoPaciente(int.Parse(ddlNomeUsu.SelectedValue));
        }

        
        //protected void ddlOpers_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow li in grdHorario.Rows)
        //    {
        //        //Só marca os outros, se o registro estiver selecionado
        //        if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
        //        {
        //            DropDownList ddlOper = (DropDownList)li.FindControl("ddlOperAgend");
        //            DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanoAgend");
        //            DropDownList ddlProc = (DropDownList)li.FindControl("ddlProcedAgend");

        //            ddlOper.SelectedValue = ddlOpers.SelectedValue;
        //            CarregarPlanosSaude(ddlPlan, ddlOper);
        //            CarregaProcedimentos(ddlProc, ddlOper);
        //        }
        //    }
        //}

        protected void lnkMultiSim_OnClick(object sender, EventArgs e)
        {
            //var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
            //res.TB251_PLANO_OPERAReference.Load();
            //res.TB250_OPERAReference.Load();

            //if ((res.TB250_OPERA == null) || (res.TB251_PLANO_OPERA == null))
            //{
            //    ScriptManager.RegisterStartupScript(
            //         this.Page,
            //         this.GetType(),
            //         "Acao",
            //         "AbreModaloperaConfirm();",
            //         true
            //     );
            //}
            //else
            //{
            //Persistencias();
            //}
        }

        protected void lnkMultiNao_OnClick(object sender, EventArgs e)
        {
        }


        protected void lnkOperaNao_OnClick(object sender, EventArgs e)
        {
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
                        txtNuNis.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNuNis.Text = "1";
                }
            }
        }

        #endregion
    }
}