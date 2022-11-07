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
// 06/12/2017 | Warllison Lima   TA.06/12/2017| Alteração da consulta dos registros de recepção. Linha -2094


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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod22
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
                DadosAgenda.Visible = false;
                CarregaUnidades(ddlUnidResCons);
                CarregaUnidades(ddlLocalPaciente, false);
                CarregaClassificacoes();
                CarregaUnidades(ddlUnidHisPaciente);
                CarregaDepartamento(ddlDept, ddlUnidResCons);
                CarregaDepartamento(drpLocalCons, ddlUnidHisPaciente);
                CarregaProfissionais();
                //CarregaPacientes();
                CarregaGridProfi();
                txtDtIniResCons.Text = DateTime.Now.ToShortDateString();
                txtDtFimResCons.Text = DateTime.Now.AddDays(5).ToShortDateString();
                txtDtIniHistoUsuar.Text = DateTime.Now.AddDays(-90).ToShortDateString();
                txtDtFimHistoUsuar.Text = DateTime.Now.AddDays(30).ToShortDateString();

                CarregarTiposAgendamentos(ddlClassFunci, "", true);
                CarregaTiposConsulta(ddlTipoAg, "", true);
                CarregaTiposConsulta(ddTipoM, "", true);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP, true);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaTiposAgendamento(ddlTipoAgendHistPaciente, true, false, false, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
                ddlUFOrgEmis.Items.Insert(0, new ListItem("", ""));
                carregaCidade();
                carregaBairro();
                txtDtNascResp.Text = "01/01/1900";
                txtDtNascPaci.Text = "01/01/1900";
                txtNuIDResp.Text = "000000";
                txtOrgEmiss.Text = "SSP";
                var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                txtCEP.Text = tb25.CO_CEP_EMP;
                txtLograEndResp.Text = tb25.DE_END_EMP;

                //CarregaOperadoras();

                VerificarNireAutomatico();

                CarregarIndicacao();
                //CarregaLocalRecep();

                //if (!String.IsNullOrEmpty((string)(Session[SessoesHttp.URLOrigem])))
                //{
                //    chkHorDispResCons.Checked = true;
                //    chkHorDispResCons.Enabled = false;
                //}
            }
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            #region Validacoes

            //==============>Validações dos campos 
            //Verifica se foi selecionado um Paciente



            bool SelecHorario = false;

            //Verifica se foi selecioado o local de recepção
            //if (String.IsNullOrEmpty(ddlLocalRecep.SelectedValue))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o local para recepção");
            //    ddlLocalRecep.Focus();
            //    return;
            //}

            //Verifica se foi selecionado um horário para marcação da consulta
            foreach (GridViewRow li in grdHorario.Rows)
            {
                if (((CheckBox)li.FindControl("ckSelectHr")).Checked)
                {
                    SelecHorario = true;

                    var dt = DateTime.Parse(((HiddenField)li.FindControl("hidData")).Value);
                    var hr = ((HiddenField)li.FindControl("hidHora")).Value;
                    var coCol = int.Parse(((HiddenField)li.FindControl("hidCoCol")).Value);
                    int coAlu;

                    var ddlTipoAgendam = (DropDownList)li.FindControl("ddlTipoAgendam");
                    //var ddlClasTipoConsulta = (DropDownList)li.FindControl("ddlClasTipoConsulta");
                    var ddlTipo = (DropDownList)li.FindControl("ddlTipo");
                    var ddlOper = (DropDownList)li.FindControl("ddlOperAgend");
                    var ddlLocal = (DropDownList)li.FindControl("ddlLocalRecep");
                    int coAgend = int.Parse((((HiddenField)li.FindControl("hidCoAgenda")).Value));


                    if (ddlNomeUsu.SelectedValue == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente para quem será agendada a consulta.");
                        ddlNomeUsu.Focus();
                        return;
                    }
                    else
                    {
                        coAlu = int.Parse(ddlNomeUsu.SelectedValue);
                    }

                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               where (tbs174.CO_ALU == coAlu)
                               && (tbs174.CO_COL == coCol)
                               && (tbs174.DT_AGEND_HORAR == dt)
                               && (tbs174.HR_AGEND_HORAR == hr)
                               && (tbs174.CO_SITUA_AGEND_HORAR != "C")
                               select tbs174).ToList();

                    if (res != null && res.Count > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe agendado para este paciente com este profissional na seguinte data:\n" + dt.ToShortDateString() + " " + hr);
                        return;
                    }

                    //if (string.IsNullOrEmpty(ddlTipoAgendam.SelectedValue))
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de consulta");
                    //    ddlTipoAgendam.Focus();
                    //    return;
                    //}

                    if (string.IsNullOrEmpty(ddlLocal.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o local para consulta");
                        ddlLocal.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a Classificação do Agendamento");
                        ddlTipo.Focus();
                        return;
                    }

                    var resultProced = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.ID_AGEND_HORAR == coAgend && x.TBS356_PROC_MEDIC_PROCE != null).ToList();

                    var resProced = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                     join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                                     join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                                     where (tbs174.ID_AGEND_HORAR == coAgend)
                                     select new { }).ToList();

                    if (resProced == null || resProced.Count <= 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione pelo menos um procedimento para o paciente");
                        return;
                    }

                    //if (string.IsNullOrEmpty(ddlOper.SelectedValue))
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar O tipo de Contratação do Agendamento");
                    //    ddlOper.Focus();
                    //    return;
                    //}
                }
            }

            bool SelecProfiss = false;

            //Verifica se foi selecionad um profissional de saúde
            foreach (GridViewRow li in grdProfi.Rows)
            {
                if (SelecProfiss == false)
                {
                    if (((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        SelecProfiss = true;
                        break;
                    }
                }
            }

            //Valida a variável booleada criada anteriormente para verificar se foi selecionado um profissional
            if (SelecProfiss == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Profissional de Saúde para o qual será feito o agendamento de consulta");
                grdProfi.Focus();
                return;
            }

            //Valida a variável booleana criada anteriormente
            if (SelecHorario == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um horário da agenda para o qual será feita a marcação da consulta.");
                grdHorario.Focus();
                return;
            }

            #endregion

            if (hidMultiAgend.Value == "S")
            {
                liBtnGrdFinanMater.Visible =
                li9.Visible = true;

                ScriptManager.RegisterStartupScript(
                    this.Page,
                    this.GetType(),
                    "Acao",
                    "AbreModalAgendMulti();",
                    true
                );
            }
            else //Se não for multiagenda, já vai direto para a persistencia convencional
                Persistencias();
        }

        /// <summary>
        /// Executa os métodos para persistência de dados
        /// </summary>
        private void Persistencias()
        {
            if (lblSitPaciente.CssClass != "sitPacPadrao")
            {
                ScriptManager.RegisterStartupScript(
                    this.Page,
                    this.GetType(),
                    "Acao",
                    "AbreModalConfirmAlta();",
                    true
                );

                return;
            }

            //Se for agenda múltipla
            int coAlu = int.Parse(ddlNomeUsu.SelectedValue);
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

            if (String.IsNullOrEmpty(tb07.FL_LIST_ESP) || tb07.FL_LIST_ESP == "S")
            {
                tb07.FL_LIST_ESP = "N";
                TB07_ALUNO.SaveOrUpdate(tb07);
            }

            if (hidMultiAgend.Value == "S")
            {
                string hr = "";

                foreach (GridViewRow lis in grdHorario.Rows)
                {
                    //Verifica a linha que foi selecionada
                    if (((CheckBox)lis.FindControl("ckSelectHr")).Checked && hr != ((HiddenField)lis.FindControl("hidDataHora")).Value)
                    {
                        hr = ((HiddenField)lis.FindControl("hidDataHora")).Value;
                        DropDownList ddlTpConsul = (((DropDownList)lis.FindControl("ddlTipoAgendam")));
                        DropDownList ddlTipo = (((DropDownList)lis.FindControl("ddlTipo")));
                        DropDownList ddlNumOrdem = (((DropDownList)lis.FindControl("ddlNumOrdem")));
                        //DropDownList ddlClasTipoConsulta = (((DropDownList)lis.Cells[4].FindControl("ddlClasTipoConsulta")));
                        DropDownList ddlOper = (((DropDownList)lis.FindControl("ddlOperAgend")));
                        DropDownList ddlPlan = (((DropDownList)lis.FindControl("ddlPlanoAgend")));
                        DropDownList ddlProc = (((DropDownList)lis.FindControl("ddlProcedAgend")));
                        TextBox txtValor = (((TextBox)lis.FindControl("txtValorAgend")));
                        DropDownList ddlLocal = (DropDownList)lis.FindControl("ddlLocalRecep");

                        int coAgend = int.Parse((((HiddenField)lis.FindControl("hidCoAgenda")).Value));
                        string tpConsul = ddTipoM.SelectedValue;
                        int coEmpAlu = tb07.CO_EMP;
                        CheckBox chek = ((CheckBox)lis.FindControl("ckConf"));

                        //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
                        TBS174_AGEND_HORAR esp174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);
                        TBS174_AGEND_HORAR agend = new TBS174_AGEND_HORAR();
                        #region Elabora agenda CO_TP_AGEND

                        if (!String.IsNullOrEmpty(ddlNumOrdem.SelectedValue))
                        {
                            foreach (GridViewRow l in grdHorario.Rows)
                            {
                                Label lblIndex = (((Label)l.FindControl("lblIndice")));

                                if (ddlNumOrdem.SelectedValue == lblIndex.Text)
                                {
                                    int coAg = int.Parse((((HiddenField)l.FindControl("hidCoAgenda")).Value));

                                    var tbagend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAg);

                                    if (tbagend != null)
                                        agend.NU_RAP_RETORNO = tbagend.NU_REGIS_CONSUL;
                                    else
                                    {
                                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O agendamento selecionado é inválido. Por favor, verifique se o agendamento selecionado é um horário anterior ao atual");
                                        ddlNumOrdem.Focus();
                                        return;
                                    }

                                }
                            }
                        }

                        agend.TP_AGEND_HORAR = tpConsul;
                        agend.TP_CONSU = tpConsul;
                        agend.DT_AGEND_HORAR = esp174.DT_AGEND_HORAR;
                        agend.HR_AGEND_HORAR = esp174.HR_AGEND_HORAR;
                        agend.CO_SITUA_AGEND_HORAR = "A";
                        agend.DT_SITUA_AGEND_HORAR = DateTime.Now;
                        agend.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        agend.CO_COL_SITUA = LoginAuxili.CO_COL;
                        agend.FL_AGEND_CONSU = "N";
                        agend.FL_CONF_AGEND = "N";
                        agend.FL_ENCAI_AGEND = "N";
                        agend.FL_JUSTI_CANCE = "";
                        agend.CO_COL = esp174.CO_COL;
                        agend.CO_EMP = esp174.CO_EMP;
                        agend.CO_DEPT = esp174.CO_DEPT;
                        agend.TP_CONSU = esp174.TP_CONSU;
                        //agend.CO_ESPEC = coEsp;
                        agend.HR_DURACAO_AGENDA = esp174.HR_DURACAO_AGENDA;

                        string Triagem = Request["T"];
                        if (!string.IsNullOrEmpty(Triagem))
                        {
                            agend.IS_AGEND_TRIAG = "S";
                            agend.FL_AGEND_ENCAM = "T";
                            agend.DT_PRESE = DateTime.Now;
                            agend.CO_COL_PRESE = LoginAuxili.CO_COL;
                            agend.CO_EMP_COL_PRESE = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            agend.CO_EMP_PRESE = LoginAuxili.CO_EMP;
                            agend.IP_PRESE = Request.UserHostAddress;
                            agend.FL_CONF_AGEND = "S";
                        }
                        else
                        {
                            agend.FL_CONF_AGEND = "N";
                        }

                        //if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
                        //{
                        //    var proced = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue));
                        //    proced.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                        //    var valor = proced.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A");
                        //    if (valor != null)
                        //        agend.VL_CONSU_BASE = valor.VL_BASE;
                        //}
                        agend.VL_CONSUL = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);
                        #endregion

                        #region Salva o Paciente na agenda

                        agend.TP_AGEND_HORAR = esp174.TP_AGEND_HORAR;
                        agend.CO_EMP_ALU = esp174.CO_EMP;
                        agend.CO_ALU = coAlu;
                        agend.TP_CONSU = tpConsul;
                        agend.FL_CONFIR_CONSUL_SMS = "N";

                        agend.ID_DEPTO_LOCAL_RECEP = int.Parse(ddlLocal.SelectedValue);


                        #region Gera Código da Consulta

                        string coUnid = LoginAuxili.CO_UNID.ToString();
                        int coEmp = LoginAuxili.CO_EMP;
                        string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                        var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                   where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
                                   select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

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
                            seq = res.NU_REGIS_CONSUL.Substring(7, 7);
                            seq2 = int.Parse(seq);
                        }

                        seqConcat = seq2 + 1;
                        seqcon = seqConcat.ToString().PadLeft(7, '0');

                        agend.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

                        //#endregion

                        #endregion

                        #endregion

                        //agend.TP_AGEND_HORAR = ddlTpConsul.SelectedValue;
                        agend.TP_CONSU = tpConsul;

                        //agend.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");

//                        var connStr = "Data Source=user-pc;Initial Catalog=BDGC_XX_MODELO;Persist Security Info=True;User ID=sa;Password=@#!CJr;MultipleActiveResultSets=True";
//                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr);
//                        try
//                        {
//                            conn.Open();
//                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(@"UPDATE [BDGC_XX_Modelo].[dbo].[TSN011_AGENDA_CREDENCIADO]
//                                       SET 
//                                           [CO_FORMA_PAGTO] = @CO_FORMA_PAGTO
//                                          ,[CO_SITUACAO_PAGTO] = @CO_SITUACAO_PAGTO
//                                          ,[CO_SITUACAO_AGENDA] = @CO_SITUACAO_AGENDA
//                                     WHERE @ID_AGEND_HORAR", conn);
//                            cmd.Parameters.AddWithValue("@CO_FORMA_PAGTO", agend.);
//                            cmd.Parameters.AddWithValue("@CO_SITUACAO_PAGTO", agend);
//                            cmd.Parameters.AddWithValue("@CO_SITUACAO_AGENDA", agend.CO_SITUA_AGEND_HORAR);
//                            cmd.ExecuteNonQuery();
//                        }
//                        catch (Exception ex)
//                        {
//                            Response.Write("Error Occurred:" + ex.Message.ToString());
//                        }
//                        finally
//                        {
//                            conn.Close();
//                        }

                        TBS174_AGEND_HORAR.SaveOrUpdate(agend);
                    }
                }
            }
            else //Se não for múltipla
            {
                #region Convencional

                //Comentado para ser melhor implementado posteriormente
                #region Valida se o paciente já tem algum agendamento neste mesmo horário e data
                //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
                foreach (GridViewRow lis in grdHorario.Rows)
                {
                    //Verifica a linha que foi selecionada
                    if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                    {
                        int coAgend = int.Parse((((HiddenField)lis.FindControl("hidCoAgenda")).Value));
                        //Dados da agenda
                        var dadosAgendamento = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                                where tbs174.ID_AGEND_HORAR == coAgend
                                                select new { tbs174.DT_AGEND_HORAR, tbs174.HR_AGEND_HORAR, tbs174.HR_DURACAO_AGENDA }).FirstOrDefault();

                        //Lista de agendamentos do paciente para o dia em questão
                        var listaAgendaPaciDia = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                                  join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                                                  where EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) == EntityFunctions.TruncateTime(dadosAgendamento.DT_AGEND_HORAR)
                                                  && tbs174.CO_ALU == coAlu
                                                  select new { tb03.NO_COL, tb03.CO_CLASS_PROFI, tbs174.DT_AGEND_HORAR, tbs174.HR_AGEND_HORAR, tbs174.HR_DURACAO_AGENDA }).ToList();

                        if (!string.IsNullOrEmpty(dadosAgendamento.HR_DURACAO_AGENDA))
                        {
                            //data e hora inicial e final do agendamento em questão
                            TimeSpan tshi = TimeSpan.Parse((dadosAgendamento.HR_AGEND_HORAR + ":00"));
                            TimeSpan tsfin = tshi.Add(TimeSpan.Parse(dadosAgendamento.HR_DURACAO_AGENDA));
                            DateTime dtIniAgenda = dadosAgendamento.DT_AGEND_HORAR.Add(tshi);
                            DateTime dtFinalInclusao = dadosAgendamento.DT_AGEND_HORAR.Add(tsfin);

                            //Percorre a lista de agendamentos do paciente
                            foreach (var i in listaAgendaPaciDia)
                            {
                                //Data e hora inicial e final do agendamento anterior do paciente
                                TimeSpan tshiPa = TimeSpan.Parse((i.HR_AGEND_HORAR + ":00"));
                                TimeSpan tsfinPa = tshi.Add(TimeSpan.Parse(i.HR_DURACAO_AGENDA));
                                DateTime dtIniAgendaPa = i.DT_AGEND_HORAR.Add(tshiPa);
                                DateTime dtFinalInclusaoPa = i.DT_AGEND_HORAR.Add(tsfinPa);

                                //Verifica se a duração do agendamento em questão, vai conflitar com a duração de outro agendamento já registrado para paciente e data
                                if (((dtIniAgenda >= dtIniAgendaPa) && (dtIniAgenda <= dtFinalInclusaoPa))
                                    || ((dtFinalInclusao >= dtIniAgendaPa) && (dtFinalInclusao <= dtFinalInclusaoPa)))
                                {
                                    string erro = "O horário das " + tshi.Hours.ToString().PadLeft(2, '0')
                                                                        + ":"
                                                                        + tshi.Minutes.ToString().PadLeft(2, '0')
                                                                        + "; "
                                                                        + " do dia " + dadosAgendamento.DT_AGEND_HORAR.ToString("dd/MM/yyyy")
                                                                        + " já está agendado para este paciente para o profissional "
                                                                        + i.NO_COL
                                                                        + " na categoria " + AuxiliGeral.GetNomeClassificacaoFuncional(i.CO_CLASS_PROFI)
                                                                        + ". Favor rever os conflitos.";

                                    AuxiliPagina.EnvioMensagemErro(this.Page, erro);
                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion

                //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
                foreach (GridViewRow lis in grdHorario.Rows)
                {
                    //Verifica a linha que foi selecionada
                    if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                    {
                        DropDownList ddlTpConsul = (((DropDownList)lis.FindControl("ddlTipoAgendam")));
                        DropDownList ddlTipo = (((DropDownList)lis.FindControl("ddlTipo")));
                        //DropDownList ddlClasTipoConsulta = (((DropDownList)lis.Cells[3].FindControl("ddlClasTipoConsulta")));
                        DropDownList ddlOper = (((DropDownList)lis.FindControl("ddlOperAgend")));
                        DropDownList ddlPlan = (((DropDownList)lis.FindControl("ddlPlanoAgend")));
                        DropDownList ddlProc = (((DropDownList)lis.FindControl("ddlProcedAgend")));
                        TextBox txtValor = (((TextBox)lis.FindControl("txtValorAgend")));
                        DropDownList ddlLocal = (DropDownList)lis.FindControl("ddlLocalRecep");
                        DropDownList ddlNumOrdem = (((DropDownList)lis.FindControl("ddlNumOrdem")));

                        int coAgend = int.Parse((((HiddenField)lis.FindControl("hidCoAgenda")).Value));
                        //string tpConsul = ddlTpConsul.SelectedValue;


                        int coEmpAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault().CO_EMP;
                        CheckBox chek = ((CheckBox)lis.FindControl("ckConf"));

                        //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
                        TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                        if (!String.IsNullOrEmpty(ddlNumOrdem.SelectedValue))
                        {
                            foreach (GridViewRow l in grdHorario.Rows)
                            {
                                Label lblIndex = (((Label)l.FindControl("lblIndice")));

                                if (ddlNumOrdem.SelectedValue == lblIndex.Text)
                                {
                                    int coAg = int.Parse((((HiddenField)l.FindControl("hidCoAgenda")).Value));

                                    var tbagend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAg);

                                    if (tbagend != null)
                                        tbs174.NU_RAP_RETORNO = tbagend.NU_REGIS_CONSUL;
                                    else
                                    {
                                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O agendamento selecionado é inválido. Por favor, verifique se o agendamento selecionado é um horário anterior ao atual");
                                        ddlNumOrdem.Focus();
                                        return;
                                    }

                                }
                            }
                        }

                        if (chkEnviaSms.Checked)
                            EnviaSMS((tbs174.CO_ALU != null ? false : true), tbs174.HR_AGEND_HORAR, tbs174.DT_AGEND_HORAR, tbs174.CO_COL.Value, tbs174.CO_ESPEC.Value, tbs174.CO_EMP.Value, tb07.NU_TELE_CELU_ALU, tb07.NO_ALU, tb07.CO_ALU);

                        tbs174.TP_AGEND_HORAR = null;
                        tbs174.CO_EMP_ALU = coEmpAlu;
                        tbs174.CO_ALU = coAlu;
                        tbs174.TP_CONSU = ddlTipo.SelectedValue;
                        tbs174.FL_CONF_AGEND = "N";
                        tbs174.FL_CONFIR_CONSUL_SMS = chkEnviaSms.Checked ? "S" : "N";
                        tbs174.ID_DEPTO_LOCAL_RECEP = int.Parse(ddlLocal.SelectedValue);
                        tbs174.FL_JUSTI_CANCE = "";

                        tbs174.VL_CONSUL = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);

                        #region Gera Código da Consulta

                        string coUnid = LoginAuxili.CO_UNID.ToString();
                        int coEmp = LoginAuxili.CO_EMP;
                        string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                        var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                   where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
                                   select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

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
                            seq = res.NU_REGIS_CONSUL.Substring(7, 7);
                            seq2 = int.Parse(seq);
                        }

                        seqConcat = seq2 + 1;
                        seqcon = seqConcat.ToString().PadLeft(7, '0');

                        tbs174.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

                        #endregion

                        //Verifica se foi escolhida operadora, se tiver sido, verifica se tem procedimento correspondente
                        #region Verifica o Código do Procedimento

                        //int coOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
                        //int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);

                        ////Procura pelo procedimento da Operadora ID_OPER correspondente ao ID_PROC associados pelo campo agrupador para retornar o valor
                        //var resusu = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                        //              where tbs356.ID_AGRUP_PROC_MEDI_PROCE == idProc
                        //              && tbs356.TB250_OPERA.ID_OPER == coOper
                        //              && tbs356.CO_SITU_PROC_MEDI == "A"
                        //              select new { tbs356.ID_PROC_MEDI_PROCE }).FirstOrDefault();

                        //Se não tiver selecionado operadora, então salva o que tiver sido selecionado como procedimento
                        //if (coOper == 0)
                        //    tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue)) : null);
                        ////tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue)) : null);
                        //else // Se tiver sido escolhida operadora, verifica se existe procedimento correspondente ao selecionado para a operadora
                        //    tbs174.TBS356_PROC_MEDIC_PROCE = (resusu != null ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(resusu.ID_PROC_MEDI_PROCE) : null);

                        #endregion

                        tbs174.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                    }
                }

                #endregion
            }

            if (String.IsNullOrEmpty((string)(Session[SessoesHttp.URLOrigem])))
                AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento da Consulta realizado com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Encaixe de agendamento realizado com sucesso!", Session[SessoesHttp.URLOrigem].ToString());
        }

        private int VerificaSelecionadosGridHorario()
        {
            int qntItensSelecionados = 0;

            foreach (GridViewRow lis in grdHorario.Rows)
            {
                //Verifica a linha que foi selecionada
                if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                {
                    qntItensSelecionados++;
                }
            }

            return qntItensSelecionados;
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

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((idProc), idOper, idPlan);
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
            //Verifica se o usuário optou por pesquisar por CPF ou por NIRE
            if (chkPesqCpf.Checked)
            {
                string cpf = (txtCPFPaci.Text != "" ? txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim() : "");

                //Valida se o usuário digitou ou não o CPF
                if (txtCPFPaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF para pesquisa");
                    return;
                }

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpf
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                if (res != null && ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
                    ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                //txtNisUsu.Text = res.NU_NIS.ToString();
            }
            else if (chkPesqNire.Checked)
            {
                //Valida se o usuário deixou o campo em branco.
                if (txtNirePaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIRE para pesquisa");
                    return;
                }

                int nire = int.Parse(txtNirePaci.Text.Trim());

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_NIRE == nire
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                if (res != null && ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
                    ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                //txtNisUsu.Text = res.NU_NIS.ToString();
            }
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
        }

        /// <summary>
        /// Carrega as Classificações Profissionais
        /// </summary>
        private void CarregaClassificacoes()
        {
            int coEmp = (ddlUnidResCons.SelectedValue != "" ? int.Parse(ddlUnidResCons.SelectedValue) : 0);
            ddlClassProfi.Items.Clear();
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassProfi, true, coEmp, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        /// <summary>
        /// Responsável por carregar os profissionais
        /// </summary>
        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfissional, 0, true, "0", true);
            drpProfissional.SelectedIndex = drpProfissional.Items.IndexOf(drpProfissional.Items.FindByText(LoginAuxili.NOME_USU_LOGADO));
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            if (drpProfissional.SelectedValue == "0" && ddlLocalPaciente.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
            else
            {
                int localPaciente = ddlLocalPaciente.SelectedValue != "" ? int.Parse(ddlLocalPaciente.SelectedValue) : 0;
                int Profissional = drpProfissional.SelectedValue != "" ? int.Parse(drpProfissional.SelectedValue) : 0;

                var res = new List<Aluno>();

                if (localPaciente != 0)
                {
                    res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where (localPaciente != 0 ? tb07.CO_EMP_ORIGEM == localPaciente : 0 == 0)
                           select new Aluno { NO_ALU = tb07.NO_ALU, CO_ALU = tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
                }
                else
                {
                    res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                           where (Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0)
                           select new Aluno { NO_ALU = tb07.NO_ALU, CO_ALU = tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();
                }

                if (res != null)
                {
                    ddlNomeUsu.DataTextField = "NO_ALU";
                    ddlNomeUsu.DataValueField = "CO_ALU";
                    ddlNomeUsu.DataSource = res;
                    ddlNomeUsu.DataBind();
                }

                ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));
            }
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

            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO, DE_DEPTO = tb14.CO_SIGLA_DEPTO + " - " + tb14.NO_DEPTO });

            if (res != null)
            {
                drpLocal.DataTextField = "DE_DEPTO";
                drpLocal.DataValueField = "CO_DEPTO";
                drpLocal.DataSource = res;
                drpLocal.DataBind();
            }

            drpLocal.Items.Insert(0, new ListItem("Todos", "0"));

        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);

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
        private void LimparGridHorarios()
        {
            grdHorario.DataSource = null;
            grdHorario.DataBind();
            //UpdHora.Update();
        }

        /// <summary>
        /// é chamado quando se altera data de início ou final do parâmetro ou se clica em horários disponíveis
        /// </summary>
        private void CarregaGridHorariosAlter()
        {
            int coCol = 0;
            foreach (GridViewRow li in grdProfi.Rows)
            {
                CheckBox chk = ((CheckBox)li.Cells[0].FindControl("ckSelect"));

                if (chk.Checked == true)
                {
                    string coColSt = ((HiddenField)li.Cells[0].FindControl("hidCoCol")).Value;
                    string coClassFunci = ((HiddenField)li.Cells[0].FindControl("hidClassFuncProfi")).Value;
                    coCol = (!string.IsNullOrEmpty(coColSt) ? int.Parse(coColSt) : 0);
                    int? index = null;
                    foreach (GridViewRow row in grdHorario.Rows)
                    {
                        CheckBox chkHR = ((CheckBox)row.Cells[0].FindControl("ckSelectHr"));
                        if (chkHR.Checked)
                        {
                            index = row.RowIndex;
                        }
                    }
                    CarregaGridHorario(coCol, chkHorDispResCons.Checked, coClassFunci, false, index.HasValue ? index.Value : (int?)null);
                }

            }
        }

        /// <summary>
        /// Carrega o histórico de agendamentos do paciente recebido como parâmetro;
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricoPaciente(int CO_ALU)
        {
            int unid = (!string.IsNullOrEmpty(ddlUnidHisPaciente.SelectedValue) ? int.Parse(ddlUnidHisPaciente.SelectedValue) : 0);
            int local = (!string.IsNullOrEmpty(drpLocalCons.SelectedValue) ? int.Parse(drpLocalCons.SelectedValue) : 0);
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHistoUsuar.Text) ? DateTime.Parse(txtDtIniHistoUsuar.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHistoUsuar.Text) ? DateTime.Parse(txtDtFimHistoUsuar.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_EMP equals tb14.TB25_EMPRESA.CO_EMP
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                       join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                       join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs386.ID_OPER equals tb250.ID_OPER
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs174.CO_ALU == CO_ALU
                       && (tb14.CO_DEPTO == tbs174.CO_DEPT)
                       && (unid != 0 ? tbs174.CO_EMP == unid : 0 == 0)
                       && (ddlTipoAgendHistPaciente.SelectedValue != "0" ? tbs174.TP_AGEND_HORAR == ddlTipoAgendHistPaciente.SelectedValue : 0 == 0)
                       && (local != 0 ? tbs174.CO_DEPT == local : 0 == 0)
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                       //&&   a.DT_AGEND_HORAR >= dtIni && a.DT_AGEND_HORAR <= dtFim
                       select new HorarioHistoricoPaciente
                       {
                           CO_AGEND = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                           LOCAL = tb14.CO_SIGLA_DEPTO,
                           APELIDO_PROFISSIONAL = tb03.NO_APEL_COL != null ? tb03.NO_APEL_COL : " - ",
                           TP_PROCED_X = tbs174.TP_AGEND_HORAR,
                           TP_PROCED_V = tbs386.TBS356_PROC_MEDIC_PROCE != null ? tbs386.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI : "-",
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           TP_CONTRATO = tbs386.ID_OPER != null ? tb250.NM_SIGLA_OPER : " - ",
                           ESPEC = tb63.NO_ESPECIALIDADE,

                           Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = tbs174.FL_CONF_AGEND,
                           agendaEncamin = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE
                       }).Concat(from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_EMP equals tb14.TB25_EMPRESA.CO_EMP
                                 join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                                 join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                                 where tbs174.CO_ALU == CO_ALU
                                 && (tb14.CO_DEPTO == tbs174.CO_DEPT)
                                 && (unid != 0 ? tbs174.CO_EMP == unid : 0 == 0)
                                 && (ddlTipoAgendHistPaciente.SelectedValue != "0" ? tbs174.TP_AGEND_HORAR == ddlTipoAgendHistPaciente.SelectedValue : 0 == 0)
                                 && (local != 0 ? tbs174.CO_DEPT == local : 0 == 0)
                                 && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                                 && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                                 //&&   a.DT_AGEND_HORAR >= dtIni && a.DT_AGEND_HORAR <= dtFim
                                 select new HorarioHistoricoPaciente
                                 {
                                     CO_AGEND = tbs174.ID_AGEND_HORAR,
                                     LOCAL = tb14.CO_SIGLA_DEPTO,
                                     APELIDO_PROFISSIONAL = tb03.NO_APEL_COL != null ? tb03.NO_APEL_COL : " - ",
                                     TP_PROCED_X = tbs174.TP_AGEND_HORAR,
                                     TP_PROCED_V = tbs174.TBS356_PROC_MEDIC_PROCE != null ? tbs174.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI : "-",
                                     DT = tbs174.DT_AGEND_HORAR,
                                     HR = tbs174.HR_AGEND_HORAR,
                                     TP_CONTRATO = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : " - ",
                                     ESPEC = tb03.DE_FUNC_COL, //tb63.NO_ESPECIALIDADE,

                                     Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                     agendaConfirm = tbs174.FL_CONF_AGEND,
                                     agendaEncamin = tbs174.FL_AGEND_ENCAM,
                                     faltaJustif = tbs174.FL_JUSTI_CANCE
                                 }).DistinctBy(x => x.CO_AGEND).OrderBy(w => w.DT).ThenBy(w => w.HR).ThenBy(w => w.LOCAL).ToList();
            
            grdHistorPaciente.DataSource = res;
            grdHistorPaciente.DataBind();
        }

        /// <summary>
        /// Carrega Operadora e Plano de saúde do paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void SelecionaOperadoraPlanoPaciente(int? CO_ALU, bool Desmarca = false)
        {
            foreach (GridViewRow li in grdHorario.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlOper = (DropDownList)li.Cells[5].FindControl("ddlOperAgend");
                    DropDownList ddlPlan = (DropDownList)li.Cells[6].FindControl("ddlPlanoAgend");
                    DropDownList ddlProce = (DropDownList)li.Cells[7].FindControl("ddlProcedAgend");
                    CarregaOperadoras(ddlOper, "");
                    CarregarPlanosSaude(ddlPlan, ddlOper);
                    CarregaProcedimentos(ddlProce, ddlOper);

                    if (CO_ALU.HasValue)
                    {
                        var res = TB07_ALUNO.RetornaPeloCoAlu(CO_ALU.Value);
                        res.TB250_OPERAReference.Load();
                        res.TB251_PLANO_OPERAReference.Load();

                        //Se houver operadora
                        if (res.TB250_OPERA != null)
                        {
                            ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

                            CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
                            res.TB251_PLANO_OPERAReference.Load();
                            if (res.TB251_PLANO_OPERA != null) //Se houver plano
                                ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
                        }
                        else
                        {
                            ddlOper.SelectedValue =
                            ddlPlan.SelectedValue = "";
                        }
                    }
                }
            }
        }

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
        private void CarregaProcedimentos(DropDownList ddl, DropDownList ddlOper, string selec = null, string tipo = null)
        {

            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            string tipoV = (String.IsNullOrEmpty(tipo) ? null : tipo.Equals("P") ? "PR" : tipo.Equals("N") ? "CO" : tipo.Equals("R") ? "CO" : tipo.Equals("E") ? "EX" : tipo.Equals("V") ? "VA" : tipo.Equals("C") ? "CI" : tipo.Equals("O") ? "OU" : null);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_SITU_PROC_MEDI == "A"
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       && (String.IsNullOrEmpty(tipoV) ? 0 == 0 : tbs356.CO_TIPO_PROC_MEDI == tipoV)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = "CO_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem(".'. Selecione .'.", ""));

            if (!string.IsNullOrEmpty(selec) && ddl.Items.FindByValue(selec) != null)
                ddl.SelectedValue = selec;           
        }

        /// <summary>
        /// Carrega as operadoras de plano de saúde
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadoraM, false, false, true);
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOpers, false, true, true, true, false);
            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    var ddlOperV = (DropDownList)linha.Cells[5].FindControl("ddlOperAgend");
                    var ddlProc = (DropDownList)linha.Cells[7].FindControl("ddlProcedAgend");
                    CarregaProcedimentos(ddlProc, ddlOperV);
                }
            }
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

            if (ddlOperadoraM.SelectedValue != "" || ddlOperadoraM.SelectedValue != null)
            {
                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanoM, ddlOperadoraM.SelectedValue, false, false, false);
                ddlPlanoM.Items.Insert(0, new ListItem("", ""));
                return;
            }

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
        private void CarregaGridHorario(int coCol, bool HorariosDisponiveis, string coClassFunci = null, bool limparHorarSemPaci = false, int? linhaSelecinada = null)
        {
            if (txtDtIniResCons.Text == null || txtDtIniResCons.Text == "")
            {

            }

            DateTime? dtIni = txtDtIniResCons.Text != "" ? DateTime.Parse(txtDtIniResCons.Text) : (DateTime?)null;
            DateTime? dtFim = txtDtFimResCons.Text != "" ? DateTime.Parse(txtDtFimResCons.Text) : (DateTime?)null;
            if (dtIni > dtFim)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A data final não pode ser maior do que a inicial");
                return;
            }
            if (chkDom.Checked == false && chkSeg.Checked == false && chkTer.Checked == false && chkQua.Checked == false && chkQui.Checked == false && chkSex.Checked == false && chkSab.Checked == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Pelo menos um dia da semana deve ser selecionado");
                return;
            }

            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var pac = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));

                pac.TB250_OPERAReference.Load();

                if (pac.TB250_OPERA != null && ddlOpers.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                    ddlOpers.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
            }

            TimeSpan? hrInicio = txtHrIni.Text != "" ? TimeSpan.Parse(txtHrIni.Text) : (TimeSpan?)null;
            TimeSpan? hrFim = txtHrFim.Text != "" ? TimeSpan.Parse(txtHrFim.Text) : (TimeSpan?)null;

            //Trata as datas para poder compará-las com as informações no banco
            string dataConver = dtIni.Value.ToString("yyyy/MM/dd");
            DateTime dtInici = DateTime.Parse(dataConver);

            //Trata as datas para poder compará-las com as informações no banco
            string dataConverF = dtFim.Value.ToString("yyyy/MM/dd");
            DateTime dtFimC = DateTime.Parse(dataConverF);

            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on a.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                       join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                       where a.CO_COL == coCol
                       && (HorariosDisponiveis ? a.CO_ALU == null || a.CO_SITUA_AGEND_HORAR == "C" : 0 == 0)
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtInici)  //dtInici
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFimC))) //dtFimC
                       select new HorarioSaida
                       {
                           NU_RAP_RETORNO = (a.NU_RAP_RETORNO != null ? a.NU_RAP_RETORNO : ""),
                           dt = a.DT_AGEND_HORAR,
                           hr = a.HR_AGEND_HORAR,
                           CO_ALU = a.CO_ALU,
                           TP_CONSUL = a.TP_CONSU,
                           CO_AGEND = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                           FL_CONF = a.FL_CONF_AGEND,
                           CO_COL = a.CO_COL,
                           CO_DEPTO = a.CO_DEPT,
                           CO_EMP = a.CO_EMP,
                           CO_ESPEC = a.CO_ESPEC,
                           CO_TP_AGEND = a.TP_AGEND_HORAR,
                           CO_TP_CONSUL = a.TP_CONSU,
                           CO_PLAN = a.TB251_PLANO_OPERA.ID_PLAN,
                           CO_OPER = a.TB250_OPERA.ID_OPER,
                           ID_PROC = a.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           CO_CLASS_TP = a.TBS356_PROC_MEDIC_PROCE.CO_CLASS_FUNCI,
                           VL_CONSUL = a.VL_CONSUL,
                           ID_DEPTO_LOCAL_RECEP = a.ID_DEPTO_LOCAL_RECEP,

                           Situacao = a.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = a.FL_CONF_AGEND,
                           agendaEncamin = a.FL_AGEND_ENCAM,
                           faltaJustif = a.FL_JUSTI_CANCE,
                           QTP = tbs386.QT_PROCED,
                           QTPValor = tbs386.VL_PROCED
                       }).Concat(from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                 where a.CO_COL == coCol
                                 && (HorariosDisponiveis ? a.CO_ALU == null || a.CO_SITUA_AGEND_HORAR == "C" : 0 == 0)
                                 && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtInici)  //dtInici
                                 && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFimC))) //dtFimC
                                 select new HorarioSaida
                                 {
                                     NU_RAP_RETORNO = (a.NU_RAP_RETORNO != null ? a.NU_RAP_RETORNO : ""),
                                     dt = a.DT_AGEND_HORAR,
                                     hr = a.HR_AGEND_HORAR,
                                     CO_ALU = a.CO_ALU,
                                     TP_CONSUL = a.TP_CONSU,
                                     CO_AGEND = a.ID_AGEND_HORAR,
                                     FL_CONF = a.FL_CONF_AGEND,
                                     CO_COL = a.CO_COL,
                                     CO_DEPTO = a.CO_DEPT,
                                     CO_EMP = a.CO_EMP,
                                     CO_ESPEC = a.CO_ESPEC,
                                     CO_TP_AGEND = a.TP_AGEND_HORAR,
                                     CO_TP_CONSUL = a.TP_CONSU,
                                     CO_PLAN = a.TB251_PLANO_OPERA.ID_PLAN,
                                     CO_OPER = a.TB250_OPERA.ID_OPER,
                                     ID_PROC = a.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                     CO_CLASS_TP = a.TBS356_PROC_MEDIC_PROCE.CO_CLASS_FUNCI,
                                     VL_CONSUL = a.VL_CONSUL,
                                     ID_DEPTO_LOCAL_RECEP = a.ID_DEPTO_LOCAL_RECEP,

                                     Situacao = a.CO_SITUA_AGEND_HORAR,
                                     agendaConfirm = a.FL_CONF_AGEND,
                                     agendaEncamin = a.FL_AGEND_ENCAM,
                                     faltaJustif = a.FL_JUSTI_CANCE,
                                     QTP = 0,
                                     QTPValor = 0
                                 }).OrderBy(w => w.dt).ToList();


            var lst = new List<HorarioSaida>();

            #region Verifica os itens a serem excluídos
            if (res.Count > 0)
            {
                int aux = 0;
                int? QTPTotal = 0;
                decimal? QTPValorTotal = 0;
                foreach (var i in res)
                {
                    foreach (var x in res.Where(x => x.CO_AGEND == i.CO_AGEND))
                    {
                        if (x.CO_AGEND == i.CO_AGEND)
                        {
                            QTPTotal += i.QTP;
                            QTPValorTotal += i.QTP * i.QTPValor;
                        }
                    }

                    i.QTPTotal = QTPTotal;
                    i.QTPVTotal = QTPValorTotal;
                    QTPTotal = 0;
                    QTPValorTotal = 0;

                    int dia = (int)i.dt.DayOfWeek;

                    switch (dia)
                    {
                        case 0:
                            if (!chkDom.Checked)
                            { lst.Add(i); }
                            break;
                        case 1:
                            if (!chkSeg.Checked)
                            { lst.Add(i); }
                            break;
                        case 2:
                            if (!chkTer.Checked)
                            { lst.Add(i); }
                            break;
                        case 3:
                            if (!chkQua.Checked)
                            { lst.Add(i); }
                            break;
                        case 4:
                            if (!chkQui.Checked)
                            { lst.Add(i); }
                            break;
                        case 5:
                            if (!chkSex.Checked)
                            { lst.Add(i); }
                            break;
                        case 6:
                            if (!chkSab.Checked)
                            { lst.Add(i); }
                            break;
                    }
                    aux++;
                }
            }
            #endregion

            var resNew = res.Except(lst).ToList();

            //if (dtIni != null && dtFim != null)
            //    resNew = resNew.Where(a => a.dt >= dtIni && a.dt <= dtFim).ToList();

            //Se tiver horario de inicio, filtra
            if (hrInicio != null)
                resNew = resNew.Where(a => a.hrC >= hrInicio).ToList();

            //Se tiver horario de termino, filtra
            if (hrFim != null)
                resNew = resNew.Where(a => a.hrC <= hrFim).ToList();

            //Reordena
            resNew = resNew.DistinctBy((w => w.CO_AGEND)).OrderBy(w => w.dt).ThenBy(w => w.hrC).ThenBy(w => w.NO_PAC).ToList();

            int numOrd = 0;
            foreach (var i in resNew)
            {
                numOrd++;
                i.numOrdem = numOrd;
            }

            List<int> rowSel = new List<int>();
            foreach (GridViewRow row in grdHorario.Rows)
            {
                CheckBox chkHR = ((CheckBox)row.Cells[0].FindControl("ckSelectHr"));
                if (chkHR.Checked)
                {
                    rowSel.Add(row.RowIndex);
                }
            }

            grdHorario.DataSource = resNew;
            grdHorario.DataBind();

            foreach (GridViewRow li in grdHorario.Rows)
            {
                CheckBox chkHR = ((CheckBox)li.Cells[0].FindControl("ckSelectHr"));
                HiddenField nuRapRetor = ((HiddenField)li.FindControl("hidNuRapRetorno"));
                HiddenField coAgend = ((HiddenField)li.FindControl("hidCoAgenda"));
                HiddenField CoAlu = ((HiddenField)li.FindControl("hidCoAlu"));
                HiddenField hidCoDepto = ((HiddenField)li.FindControl("hidCoDepto"));
                HiddenField hidCoRecep = ((HiddenField)li.FindControl("hidCoRecep"));
                HiddenField hidTpCons = ((HiddenField)li.FindControl("hidTpCons"));
                DropDownList ddlNumOrdem = ((DropDownList)li.FindControl("ddlNumOrdem"));
                DropDownList ddlTipoAgend = ((DropDownList)li.FindControl("ddlTipoAgendam"));
                DropDownList ddlTipo = ((DropDownList)li.FindControl("ddlTipo"));
                DropDownList ddlplan = ((DropDownList)li.FindControl("ddlPlanoAgend"));
                DropDownList ddlcontrat = ((DropDownList)li.FindControl("ddlOperAgend"));
                DropDownList ddlLocal = ((DropDownList)li.FindControl("ddlLocalRecep"));
                TextBox txtQtp = ((TextBox)li.FindControl("txtQtp"));
                TextBox txtQtpV = ((TextBox)li.FindControl("txtValorAgend"));
                Label lblIndex = ((Label)li.FindControl("lblIndice"));

                if (rowSel.Count > 0 && rowSel.Any(x => x == li.RowIndex))
                {
                    chkHR.Checked = true;
                }

                if (limparHorarSemPaci)
                {
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(coAgend.Value));

                    if (tbs174.CO_ALU == null)
                    {
                        tbs174.TP_CONSU = null;
                        tbs174.TB250_OPERA = null;
                        tbs174.TB251_PLANO_OPERA = null;
                        tbs174.TBS356_PROC_MEDIC_PROCE = null;
                        tbs174.TBS355_PROC_MEDIC_SGRUP = null;
                        tbs174.TBS354_PROC_MEDIC_GRUPO = null;
                        tbs174.TBS370_PLANE_AVALI = null;
                        tbs174.TBS386_ITENS_PLANE_AVALI = null;

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

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

                                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(i);

                                TBS370_PLANE_AVALI.Delete(Tbs386.TBS370_PLANE_AVALI);

                                TBS386_ITENS_PLANE_AVALI.Delete(Tbs386, true);

                                
                            }
                        }
                    }
                }

                CarregaNumOrdem(ddlNumOrdem);

                ddlNumOrdem.Items.Remove(ddlNumOrdem.Items.FindByText(lblIndex.Text));

                if (!String.IsNullOrEmpty(nuRapRetor.Value))
                    ddlNumOrdem.Enabled = false;

                if (ddlTipo != null)
                {
                    if (!String.IsNullOrEmpty(hidTpCons.Value))
                    {
                        ddlTipo.SelectedValue = hidTpCons.Value;
                    }
                }

                if (ddlTipoAgend != null)
                {
                    switch (coClassFunci)
                    {
                        case "E":
                            ddlTipoAgend.SelectedValue = "EN";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "M":
                            ddlTipoAgend.SelectedValue = "AM";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "D":
                            ddlTipoAgend.SelectedValue = "AO";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "S":
                            ddlTipoAgend.SelectedValue = "ES";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "N":
                            ddlTipoAgend.SelectedValue = "NT";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "I":
                            ddlTipoAgend.SelectedValue = "FI";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "F":
                            ddlTipoAgend.SelectedValue = "FO";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "P":
                            ddlTipoAgend.SelectedValue = "PI";
                            ddlTipoAgend.Enabled = false;
                            break;
                        case "T":
                            ddlTipoAgend.SelectedValue = "TO";
                            ddlTipoAgend.Enabled = false;
                            break;
                        default:
                            ddlTipoAgend.SelectedValue = "";
                            ddlTipoAgend.Enabled = true;
                            break;
                    }
                }

                if (ddlLocal.SelectedValue == "" || (String.IsNullOrEmpty(ADMUSUARIO.RetornaPeloCodUsuario(LoginAuxili.CO_COL).FL_PERMI_MOVIM_LOCAL) ? false : ADMUSUARIO.RetornaPeloCodUsuario(LoginAuxili.CO_COL).FL_PERMI_MOVIM_LOCAL.Equals("S")))
                    ddlLocal.Enabled = true;
                else
                    ddlLocal.Enabled = false;

                if (String.IsNullOrEmpty(hidCoRecep.Value))
                {
                    ddlLocal.SelectedValue = hidCoDepto.Value;
                }
                else
                {
                    ddlLocal.SelectedValue = hidCoRecep.Value;
                }

                txtQtp.Enabled = false;
                txtQtpV.Enabled = false;


                int idAgend = int.Parse(coAgend.Value);
                int? qtp = 0;
                decimal? qtpv = 0;
                var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(X => X.TBS174_AGEND_HORAR.ID_AGEND_HORAR == idAgend).ToList();

                if (tbs389.Count > 0)
                {
                    foreach (var i in tbs389)
                    {
                        i.TBS174_AGEND_HORARReference.Load();
                        i.TBS386_ITENS_PLANE_AVALIReference.Load();

                        var tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(i.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI);
                        qtp += tbs386.QT_PROCED;
                        qtpv += (tbs386.VL_PROCED * tbs386.QT_PROCED);
                    }

                    txtQtp.Text = qtp != null ? qtp.ToString() : "";
                    txtQtpV.Text = qtpv != null ? qtpv.ToString() : "";
                }
            }
        }

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
            public int CO_AGEND { get; set; }
            public string LOCAL { get; set; }
            public string ESPEC { get; set; }
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
            public string NU_RAP_RETORNO { get; set; }
            public int numOrdem { get; set; }
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
                    string local = this.CO_DEPTO == null ? " " : TB14_DEPTO.RetornaPelaChavePrimaria((int)this.CO_DEPTO).CO_SIGLA_DEPTO;
                    string diaSemana = this.dt.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + diaSemana + " - " + local;
                }
            }
            public int CO_AGEND { get; set; }
            public int? CO_COL { get; set; }
            public int? CO_ESPEC { get; set; }
            public int? CO_DEPTO { get; set; }
            public int? CO_EMP { get; set; }
            public int? ID_DEPTO_LOCAL_RECEP { get; set; }
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
            public int? QTP { get; set; }
            public int? QTPTotal { get; set; }
            public decimal? QTPValor { get; set; }
            public decimal? QTPVTotal { get; set; }

            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
        }

        /// <summary>
        /// Carrega a grid de profissionais da saúde
        /// </summary>
        private void CarregaGridProfi()
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidResCons.SelectedValue) ? int.Parse(ddlUnidResCons.SelectedValue) : 0);
            int coDep = (!string.IsNullOrEmpty(ddlDept.SelectedValue) ? int.Parse(ddlDept.SelectedValue) : 0);
            string coClassProfi = ddlClassProfi.SelectedValue;
            string Triagem = Request["T"];

            List<string> clss;

            //Caso não tenha sido selecionada uma classificação ele verifica as disponiveis para a empresa do usuário logado
            if (coClassProfi != "0")
                clss = new List<string>();
            else
                clss = AuxiliCarregamentos.RecuperarClassificacoesDisponiveis(AuxiliCarregamentos.ETiposClassificacoes.agendamento);

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb03.FLA_PROFESSOR == "S" && tb03.CO_SITU_COL == "ATI"
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                       && (coDep != 0 ? tb03.CO_DEPTO == coDep : coDep == 0)
                       && (!string.IsNullOrEmpty(Triagem) ? tb03.CO_CLASS_PROFI == "O" : tb03.CO_CLASS_PROFI == tb03.CO_CLASS_PROFI)
                       && (coClassProfi != "0" ? tb03.CO_CLASS_PROFI == coClassProfi : clss.Contains(tb03.CO_CLASS_PROFI))
                       select new GrdProfiSaida
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL_RECEB = tb03.NO_COL,
                           NO_EMP = tb25.sigla,
                           MATR_COL = tb03.CO_MAT_COL,
                           NU_TEL = tb03.NU_TELE_CELU_COL,
                           CO_CLASS_FUNC = tb63.NO_ESPECIALIDADE,
                           DescEspec = tb03.DE_FUNC_COL,
                           SIGLA_DEPTO = tb14.CO_SIGLA_DEPTO
                       }).OrderBy(o => o.NO_COL_RECEB).ToList();

            grdProfi.DataSource = res;
            grdProfi.DataBind();
        }

        public class GrdProfiSaida
        {
            public int CO_COL { get; set; }
            public string MATR_COL { get; set; }
            public string  DescEspec { get; set; }
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
            public string SIGLA_DEPTO { get; set; }
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

        // INICIO - Retorna locais de recepção
        public void CarregaLocalRecep(DropDownList ddlLocalRecep, string idLocal)
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_RECEP.Equals("S")
                       select new { DEPTO = tb14.CO_SIGLA_DEPTO + " - " + tb14.NO_DEPTO, tb14.CO_DEPTO }).OrderBy(w => w.DEPTO).ToList();

            if (res != null)
            {
                ddlLocalRecep.DataTextField = "DEPTO";
                ddlLocalRecep.DataValueField = "CO_DEPTO";
                ddlLocalRecep.DataSource = res;
                ddlLocalRecep.DataBind();
            }

            ddlLocalRecep.Items.Insert(0, new ListItem("Selecione", ""));

            if (!String.IsNullOrEmpty(idLocal))
                ddlLocalRecep.SelectedValue = idLocal;
        }
        // FIM - Retorna locais de recepção
        public void CarregaNumOrdem(DropDownList ddlNumOrdem)
        {
            int num = 0;
            List<NumOrdem> numOrd = new List<NumOrdem>();
            foreach (GridViewRow i in grdHorario.Rows)
            {
                num++;

                numOrd.Add(new NumOrdem(num));
            }

            numOrd.OrderBy(x => x.Valor);

            ddlNumOrdem.DataSource = numOrd;
            ddlNumOrdem.DataTextField = "Valor";
            ddlNumOrdem.DataValueField = "Valor";
            ddlNumOrdem.DataBind();

            ddlNumOrdem.Items.Insert(0, new ListItem("", ""));
        }

        class NumOrdem
        {
            public int Valor { get; set; }

            public NumOrdem(int valor)
            {
                this.Valor = valor;
            }
        }
        #endregion

        #region Eventos de componentes

        protected void grdHorario_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Local
                string local = ((HiddenField)e.Row.FindControl("hidLocalRecep")).Value;
                DropDownList ddlLocalRecep = ((DropDownList)e.Row.FindControl("ddlLocalRecep"));
                CarregaLocalRecep(ddlLocalRecep, local);

                ////Tipo da Agenda
                //string tipoAgenda = ((HiddenField)e.Row.FindControl("hidTpAgend")).Value;
                //DropDownList ddlTipoAgenda = ((DropDownList)e.Row.Cells[3].FindControl("ddlTipoAgendam"));
                //CarregarTiposAgendamentos(ddlTipoAgenda, tipoAgenda);

                //Tipo Consutla
                string tipoConsul = ((HiddenField)e.Row.FindControl("hidTpConsul")).Value;
                DropDownList ddlTipoConsul = ((DropDownList)e.Row.Cells[4].FindControl("ddlTipo"));
                CarregaTiposConsulta(ddlTipoConsul, tipoConsul);

                ////Operadora
                //string idOper = ((HiddenField)e.Row.FindControl("hidIdOper")).Value;
                //DropDownList ddlOper = ((DropDownList)e.Row.Cells[5].FindControl("ddlOperAgend"));
                //CarregaOperadoras(ddlOperAgend, idOper);
                ////ddlOpers.Enabled = false;

                ////Plano de Saúde
                //string idPlan = ((HiddenField)e.Row.FindControl("hidIdPlan")).Value;
                //DropDownList ddlPlano = ((DropDownList)e.Row.Cells[6].FindControl("ddlPlanoAgend"));
                //CarregarPlanosSaude(ddlPlanoAgend, ddlOper);
                //ddlPlano.SelectedValue = idPlan;
                //ddlPlano.Enabled = false;

                //Procedimento
                //string idProced = ((HiddenField)e.Row.FindControl("hidIdProced")).Value;
                //DropDownList ddlProced = ((DropDownList)e.Row.Cells[7].FindControl("ddlProcedAgend"));
                //CarregaProcedimentos(ddlProcedAgend, ddlOper, idProced);

                if (!chkHorDispResCons.Enabled)
                {
                    ddlTipoConsul.Enabled = false;

                    if (String.IsNullOrEmpty(tipoConsul))
                        ddlTipoConsul.SelectedValue = "E";
                }
                //    else
                //        //ddlTipoAgenda.Enabled =
                //        ddlOper.Enabled =
                //        ddlPlano.Enabled =
                //        ddlProced.Enabled = false;
                //}
            }
        }


        protected void ckSelect_CheckedChangedP(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow l in grdProfi.Rows)
            {
                CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelect"));

                if (atual.ClientID != chk.ClientID)
                {
                    chk.Checked = false;
                }
                else
                {
                    if (chk.Checked == true)
                    {
                        string coCol = ((HiddenField)l.Cells[0].FindControl("hidCoCol")).Value;
                        string coClassFunci = ((HiddenField)l.Cells[0].FindControl("hidClassFuncProfi")).Value;
                        hidCoColSelec.Value = coCol;
                        int coColI = (!string.IsNullOrEmpty(coCol) ? int.Parse(coCol) : 0);
                        CarregaGridHorario(coColI, chkHorDispResCons.Checked, coClassFunci, true);
                        //CarregaGridHorarioProfi(coColI);
                    }
                    else
                    {
                        grdHorario.DataSource = null;
                        grdHorario.DataBind();
                        hidCoColSelec.Value = string.Empty;
                    }
                    //UpdHora.Update();
                }
            }
        }

        protected void ddlUnidResCons_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento(ddlDept, ddlUnidResCons);
            //CarregaGridProfi();
            LimparGridHorarios();
        }

        protected void ddlUnidHisPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento(drpLocalCons, ddlUnidHisPaciente);
            LimparGridHorarios();
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

        protected void ChkTodos_OnCheckedChanged(object sender, EventArgs e)
        {
            PermissaoMultiplasAgenda(sender, e);
            CheckBox chkMarca = ((CheckBox)grdHorario.HeaderRow.Cells[0].FindControl("chkMarcaTodosItens"));

            foreach (GridViewRow l in grdHorario.Rows)
            {
                DropDownList ddlClass = (((DropDownList)l.Cells[3].FindControl("ddlTipoAgendam")));
                DropDownList ddlTipo = (((DropDownList)l.Cells[4].FindControl("ddlTipo")));
                DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
                DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");
                DropDownList ddlProced = (DropDownList)l.Cells[7].FindControl("ddlProcedAgend");

                if (chkMarca.Checked)
                {
                    CheckBox ck = (((CheckBox)l.Cells[0].FindControl("ckSelectHr")));
                    ck.Checked = true;

                    if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
                        ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

                    if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
                        ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados
                    //CarregaProcedimentos(ddlProced, ddlOper);

                    #region Seleciona Operadora e Plano do Paciente

                    if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                    {
                        var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
                        res.TB250_OPERAReference.Load();
                        res.TB251_PLANO_OPERAReference.Load();

                        //Se houver operadora
                        //if (res.TB250_OPERA != null)
                        //{
                        //    ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

                        //    if (ddlOper.SelectedValue == "")
                        //        ddlOper.Enabled = true;
                        //    else
                        //        ddlOper.Enabled = false;

                        //    CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
                        //    res.TB251_PLANO_OPERAReference.Load();
                        //    if (res.TB251_PLANO_OPERA != null) //Se houver plano
                        //        ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
                        //    if (ddlPlan.SelectedValue == "")
                        //        ddlPlan.Enabled = true;
                        //    else
                        //        ddlPlan.Enabled = false;
                        //}
                    }

                    #endregion
                }
                else
                {
                    CheckBox ck = (((CheckBox)l.Cells[0].FindControl("ckSelectHr")));
                    ck.Checked = false;

                    ddlClass.SelectedValue = ddlTipo.SelectedValue = ""; //Seleciona vazio novamente

                    //CarregaOperadoras(ddlOper, "");
                    //CarregarPlanosSaude(ddlPlan, ddlOper);
                    //CarregaProcedimentos(ddlProced, ddlOper);
                }
            }
        }

        protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtNirePaci.Enabled = true;
                chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void chkPesqCpf_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtCPFPaci.Enabled = true;
                chkPesqNire.Checked = txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        public void PermissaoMultiplasAgenda(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            bool selec = false;

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelectHr"));

                if (atual.ClientID != chk.ClientID)
                {
                    if (chk.Checked == false)
                    {
                        //Coleta e trata o código do paciente
                        string coAlu = ((HiddenField)l.Cells[0].FindControl("hidCoAlu")).Value;
                        int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);
                        string coAgend = ((HiddenField)l.Cells[0].FindControl("hidCoAgenda")).Value;

                        //Verifica se o funcionário logado possui permissão para agenda múltiplica
                        bool PermissMultiAgenda = (ADMUSUARIO.RetornaTodosRegistros().Where(w => w.CodUsuario == LoginAuxili.CO_COL && w.FL_PERMI_AGEND_MULTI == "S").Any());

                        //Se o usuário não tiver permissão para multiplo agendamento e hover algum paciente na agenda em questão,
                        //entra em modo de edição, carregando as informações correspondentes nos campos
                        if ((!PermissMultiAgenda) && (!string.IsNullOrEmpty(coAlu)) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)))
                        {
                            ScriptManager.RegisterStartupScript(
                               this.Page,
                               this.GetType(),
                               "Erro",
                               "mostraErroPermi();",
                               true
                           );

                            chk.Checked = false;

                            #region Multiagenda não permitida

                            //Coleta o tipo da consulta
                            string tpCon = ((HiddenField)l.Cells[0].FindControl("hidTpCons")).Value;

                            //Coleta controles usados
                            DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
                            DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");
                            DropDownList ddlClass = (((DropDownList)l.Cells[3].FindControl("ddlTipoAgendam")));
                            DropDownList ddlTipo = (((DropDownList)l.Cells[4].FindControl("ddlTipo")));
                            DropDownList ddlProce = (DropDownList)l.Cells[7].FindControl("ddlProcedAgend");

                            if (ddlNomeUsu.Items.Contains(new ListItem("", coAluI.ToString())))
                                ddlNomeUsu.SelectedValue = coAluI.ToString();

                            if (chk.Checked)
                            {
                                if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
                                    ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

                                if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
                                    ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados
                                CarregaProcedimentos(ddlProce, ddlOper);

                                hidCoConsul.Value = coAgend.ToString();

                                //Marca que foi selecionado para que os outros itens não desabilitem o lnk.
                                if (coAluI != 0)
                                    selec = true;

                                #region Seleciona Operadora e Plano do Paciente

                                if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                                {
                                    var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
                                    res.TB250_OPERAReference.Load();
                                    res.TB251_PLANO_OPERAReference.Load();

                                    //Se houver operadora
                                    if (res.TB250_OPERA != null)
                                    {
                                        ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

                                        CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
                                        res.TB251_PLANO_OPERAReference.Load();
                                        if (res.TB251_PLANO_OPERA != null) //Se houver plano
                                            ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
                                    }
                                }

                                #endregion

                                //Se houver 
                                //if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                                //    SelecionaOperadoraPlanoPaciente(int.Parse(ddlNomeUsu.SelectedValue));
                            }
                            else
                            {
                                ddlClass.SelectedValue = ddlTipo.SelectedValue = ""; //Seleciona vazio novamente

                                //CarregaOperadoras(ddlOper, "");
                                //CarregarPlanosSaude(ddlPlan, ddlOper);
                                if (!selec)
                                    hidCoConsul.Value = "";
                            }

                            #endregion
                        }

                        if ((PermissMultiAgenda) && (coAlu != ""))
                        {
                            hidMultiAgend.Value = "S";
                            hidEspelhoAgenda.Value = coAgend;
                        }
                    }
                    else
                        hidMultiAgend.Value = hidEspelhoAgenda.Value = "";
                }
                else
                {
                    //Desabilita o lnk caso não tenha sido selecionado nenhum item anteriormente
                    if (!selec)
                        hidCoConsul.Value = "";
                }
            }
            //UpdHora.Update();
        }

        protected void ckSelectHr_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            bool selec = false;

            int qntMarcados = 0;
            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.FindControl("ckSelectHr"));
                string coAlu = ((HiddenField)l.FindControl("hidCoAlu")).Value;
                int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);

                if (chk.Checked)
                {
                    qntMarcados++;
                }

            }

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelectHr"));
                //Coleta e trata o código do paciente
                string coAlu = ((HiddenField)l.FindControl("hidCoAlu")).Value;
                int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);
                string coAgend = ((HiddenField)l.FindControl("hidCoAgenda")).Value;
                DropDownList ddlTipo = (((DropDownList)l.FindControl("ddlTipo")));

                if (coAluI == 0 && qntMarcados > 1)
                {
                    ddlTipo.Items.RemoveAt(2);
                }
                else if (qntMarcados <= 1)
                {
                    if (!ddlTipo.Items.Contains(ddlTipo.Items.FindByValue("R")))
                        ddlTipo.Items.Insert(2, new ListItem("Consulta Retorno", "R"));
                }

                if (atual.ClientID == chk.ClientID)
                {
                    if (chk.Checked)
                    {
                        //DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
                        //DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");
                        //DropDownList ddlProce = (DropDownList)l.Cells[7].FindControl("ddlProcedAgend");

                        //Verifica se o funcionário logado possui permissão para agenda múltiplica
                        bool PermissMultiAgenda = (ADMUSUARIO.RetornaTodosRegistros().Where(w => w.CodUsuario == LoginAuxili.CO_COL && w.FL_PERMI_AGEND_MULTI == "S").Any());

                        //Se o usuário não tiver permissão para multiplo agendamento e hover algum paciente na agenda em questão,
                        //entra em modo de edição, carregando as informações correspondentes nos campos
                        if ((!PermissMultiAgenda) && (!string.IsNullOrEmpty(coAlu)) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)))
                        {
                            // ScriptManager.RegisterStartupScript(
                            //    this.Page,
                            //    this.GetType(),
                            //    "Erro",
                            //    "mostraErroPermi();",
                            //    true
                            //);
                            AuxiliPagina.EnvioMensagemErro(this.Page, "MSG: Usuário sem permissão para agendamento de mais um paciente no mesmo horário.");

                            chk.Checked = false;

                            #region Multiagenda não permitida

                            //Coleta o tipo da consulta
                            string tpCon = ((HiddenField)l.FindControl("hidTpCons")).Value;

                            //Coleta controles usados

                            DropDownList ddlClass = (((DropDownList)l.FindControl("ddlTipoAgendam")));


                            if (ddlNomeUsu.Items.Contains(new ListItem("", coAluI.ToString())))
                                ddlNomeUsu.SelectedValue = coAluI.ToString();

                            if (chk.Checked)
                            {
                                //if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
                                //    ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

                                if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
                                    ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados

                                hidCoConsul.Value = coAgend.ToString();

                                //Marca que foi selecionado para que os outros itens não desabilitem o lnk.
                                if (coAluI != 0)
                                    selec = true;

                                //Se houver 
                                //if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                                //    SelecionaOperadoraPlanoPaciente(int.Parse(ddlNomeUsu.SelectedValue));
                            }
                            else
                            {
                                //ddlClass.SelectedValue = ddlTipo.SelectedValue = ""; //Seleciona vazio novamente

                                //CarregaOperadoras(ddlOper, "");
                                //CarregarPlanosSaude(ddlPlan, ddlOper);
                                if (!selec)
                                    hidCoConsul.Value = "";
                            }

                            #endregion
                        }

                        if ((PermissMultiAgenda) && (coAlu != ddlNomeUsu.SelectedValue) && coAluI != 0)
                        {
                            hidMultiAgend.Value = "S";
                            hidEspelhoAgenda.Value = coAgend;
                        }


                        //#region Seleciona Operadora e Plano do Paciente

                        //if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                        //{
                        //    var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
                        //    res.TB250_OPERAReference.Load();

                        //    //Se houver operadora
                        //    if (res.TB250_OPERA != null)
                        //    {
                        //        ddlOper.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

                        //        CarregarPlanosSaude(ddlPlan, ddlOper); // Recarrega os planos de acordo com a operadora
                        //        res.TB251_PLANO_OPERAReference.Load();
                        //        if (res.TB251_PLANO_OPERA != null) //Se houver plano
                        //            ddlPlan.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();
                        //        CarregaProcedimentos(ddlProce, ddlOper);
                        //    }
                        //}

                        //#endregion
                    }
                    else
                        hidMultiAgend.Value = hidEspelhoAgenda.Value = "";
                }
                else
                {
                    //Desabilita o lnk caso não tenha sido selecionado nenhum item anteriormente
                    if (!selec)
                        hidCoConsul.Value = "";
                }
            }
            //UpdHora.Update();  
        }

        protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        {
            CarregaGridHorariosAlter();
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
                txtnompac.Text = res.NO_ALU.ToUpper();
                txtCPFMOD.Text = res.NU_CPF_ALU;
                txtNuNis.Text = res.NU_NIRE.ToString().PadLeft(7, '0');
                //txtSUSPaciMODAL.Text = res.NU_NIS.ToString();
                txtDtNascPaci.Text = res.DT_NASC_ALU.ToString();
                ddlSexoPaci.SelectedValue = res.CO_SEXO_ALU;
                txtTelCelPaci.Text = res.NU_TELE_CELU_ALU;
                txtTelResPaci.Text = res.NU_TELE_RESI_ALU;
                ddlGrParen.SelectedValue = res.CO_GRAU_PAREN_RESP;
                txtEmailPaci.Text = res.NO_EMAIL_PAI;
                txtWhatsPaci.Text = res.NU_TELE_WHATS_ALU;
                hidCoAluMod.Value = res.CO_ALU.ToString();
                txtApelido.Text = res.NO_APE_ALU;
                txtPastaControle.Text = res.DE_PASTA_CONTR;
                txtTelMigrado.Text = res.TEL_MIGRAR;
                ddlIndicacao.SelectedValue = res.CO_COL_INDICACAO.ToString() ?? "";
                //txtNuCarSaude.Text = res.NU_CARTAO_SAUDE.ToString() ?? res.NU_CARTAO_SAUDE_ALU.ToString();
                //ddlEtniaPaciMODAL.SelectedValue = res.TP_RACA;

                txtLograEndResp.Text = res.DE_ENDE_ALU;
                ddlUF.SelectedValue = res.CO_ESTA_ALU;
                txtCEP.Text = res.CO_CEP_ALU;

                res.TB905_BAIRROReference.Load();
                carregaCidade();
                if (res.TB905_BAIRRO != null && ddlCidade.Items.FindByValue(res.TB905_BAIRRO.CO_CIDADE.ToString()) != null)
                    ddlCidade.SelectedValue = res.TB905_BAIRRO.CO_CIDADE.ToString();
                carregaBairro();
                if (res.TB905_BAIRRO != null && ddlBairro.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString()) != null)
                    ddlBairro.SelectedValue = res.TB905_BAIRRO.CO_BAIRRO.ToString();

                res.ImageReference.Load();
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                if (res.Image != null)
                    upImagemAluno.CarregaImagem(res.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);

                res.TB108_RESPONSAVELReference.Load();

                //if (res.TB108_RESPONSAVEL != null)
                //    PesquisaCarregaResp(res.TB108_RESPONSAVEL.CO_RESP);
                if (coResp.HasValue)
                    PesquisaCarregaRespMODAL(coResp);
            }
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaRespMODAL(int? co_resp, string cpfRespParam = null)
        {
            string cpfResp = (string.IsNullOrEmpty(cpfRespParam) ?
                txtCPFResp.Text.Replace(".", "").Replace("-", "") : cpfRespParam.Replace(".", "").Replace("-", ""));

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
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoRespMod.Value = res.CO_RESP.ToString();
                //this.lblComRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE == "S" ? true : false);
                //this.lblSemRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE != "S" ? true : false);
            }
        }

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            // André Janela PopUp
            if (!String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(int.Parse(ddlNomeUsu.SelectedValue), LoginAuxili.CO_EMP);
                var tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb07.TB108_RESPONSAVEL.CO_RESP);
                PesquisaCarregaPaciMODAL(tb07.CO_ALU, tb108.CO_RESP);
            }


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
                    tb07.DE_PASTA_CONTR = !string.IsNullOrEmpty(txtPastaControle.Text) ? nirTot.ToString() : txtPastaControle.Text;
                    tb07.TEL_MIGRAR = txtTelMigrado.Text;
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

                //pac.TB250_OPERAReference.Load();

                //if (pac.TB250_OPERA != null && ddlOpers.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                //    ddlOpers.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
                //else
                //    ddlOpers.SelectedValue = "";
                txtCPFPaci.Text = AuxiliFormatoExibicao.preparaCPFCNPJ(pac.NU_CPF_ALU);
                txtNirePaci.Text = pac.NU_NIRE.ToString();
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
                if ((!PermissMultiAgenda) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)) && (!string.IsNullOrEmpty(hidCoConsul.Value)))
                {
                    ScriptManager.RegisterStartupScript(
                       this.Page,
                       this.GetType(),
                       "Erro",
                       "mostraErroPermi();",
                       true
                   );

                    ddlNomeUsu.SelectedValue = "";
                }
            }
            //else
            //SelecionaOperadoraPlanoPaciente((int?)null, true);
        }

        protected void imgPesqHistPaciente_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                CarregaGridHistoricoPaciente(int.Parse(ddlNomeUsu.SelectedValue));
        }

        protected void imgPesqProfissionais_OnClick(object sender, EventArgs e)
        {
            LimparGridHorarios();
            CarregaGridProfi();
        }

        protected void ddlOperAgend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.Cells[5].FindControl("ddlOperAgend");
                    DropDownList ddlPlan = (DropDownList)linha.Cells[6].FindControl("ddlPlanoAgend");
                    DropDownList ddlProc = (DropDownList)linha.Cells[6].FindControl("ddlProcedAgend");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        CarregarPlanosSaude(ddlPlan, ddl);
                        CarregaProcedimentos(ddlProc, ddl);
                    }
                }
            }
        }

        protected void ddlOperAgendM_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanoSaudeM();
            DadosAgenda.Visible = true;
            AbreModalPadrao("AbreModalAgendMulti();");
        }

        protected void ddlProcedAgend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlOper, ddlPlan, ddlProc;

            int qntProced = 0;
            bool existeProcedimento = false; //Define se existe um procedimento igual já selecionado

            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedimentos.Rows)
                {
                    ddlProc = (DropDownList)linha.FindControl("ddlProcMod");

                    if (ddlProc.SelectedValue.Equals(atual.SelectedValue))
                    {
                        qntProced++;
                        if (qntProced > 1)
                        {
                            existeProcedimento = true;
                            ddlProc.Focus();
                            break;
                        }
                    }
                }
            }

            if (!existeProcedimento)
            {
                if (grdProcedimentos.Rows.Count != 0)
                {
                    foreach (GridViewRow linha in grdProcedimentos.Rows)
                    {
                        ddlOper = (DropDownList)linha.FindControl("ddlContratProc");
                        ddlPlan = (DropDownList)linha.FindControl("ddlPlanoProc");
                        ddlProc = (DropDownList)linha.FindControl("ddlProcMod");
                        TextBox txtValor = (TextBox)linha.FindControl("txtValorUnit");

                        //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                        if (ddlProc.ClientID == atual.ClientID)
                            CalcularPreencherValoresTabelaECalculado(ddlProc, ddlOper, ddlPlan, txtValor);
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "MSG: Este procedimento já foi listado.");
                atual.SelectedValue = null;
            }
            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void ddlClassFunci_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //    foreach (GridViewRow li in grdHorario.Rows)
            //    {
            //        //Só marca os outros, se o registro estiver selecionado
            //        if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
            //        {
            //            DropDownList ddlClass = (((DropDownList)li.Cells[3].FindControl("ddlTipoAgendam")));
            //            ddlClass.SelectedValue = ddlClassFunci.SelectedValue;
            //        }
            //    }
        }

        protected void ddlTipoAg_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Só marca os outros, se o registro estiver selecionado
                if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlTipo = (((DropDownList)li.Cells[4].FindControl("ddlTipo")));
                    ddlTipo.SelectedValue = ddlTipoAg.SelectedValue;
                }
            }
        }

        protected void ddlOpers_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                ddl = (DropDownList)li.FindControl("ddlContratProc");
                //Só marca os outros, se o registro estiver selecionado
                if (ddl.ClientID == atual.ClientID)
                {
                    DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanoProc");
                    DropDownList ddlProc = (DropDownList)li.FindControl("ddlProcMod");
                    var tbs174_tipoConsu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendProced.Value)).TP_CONSU;

                    CarregarPlanosSaude(ddlPlan, ddl);
                    CarregaProcedimentos(ddlProc, ddl, null, tbs174_tipoConsu);
                }
            }
            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void Qtp_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                txt = (TextBox)li.FindControl("txtQTPMod");
                //Só marca os outros, se o registro estiver selecionado
                if (txt.ClientID == atual.ClientID)
                {
                    TextBox txtValorUnit = (TextBox)li.FindControl("txtValorUnit");
                    TextBox txtValorTotal = (TextBox)li.FindControl("txtValorTotalMod");

                    decimal result = ((String.IsNullOrEmpty(txtValorUnit.Text) ? 0 : Decimal.Parse(txtValorUnit.Text)) * (String.IsNullOrEmpty(txt.Text) ? 0 : Decimal.Parse(txt.Text)));

                    txtValorTotal.Text = result.ToString();
                }
            }
            AbreModalPadrao("AbreModalProcedHorar();");
        }

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
            Persistencias();
            //}
        }

        protected void lnkMultiNao_OnClick(object sender, EventArgs e)
        {
        }

        protected void lnkOperaSim_OnClick(object sender, EventArgs e)
        {
            Persistencias();
        }

        protected void lnkOperaNao_OnClick(object sender, EventArgs e)
        {
        }

        protected void lnkbConfAlta_OnClick(object sender, EventArgs e)
        {
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));

            tb07.CO_SITU_ALU = "A";

            TB07_ALUNO.SaveOrUpdate(tb07);

            lblSitPaciente.CssClass = "sitPacPadrao";
            Persistencias();
        }

        #region Modal Procedimentos da Grid Horários
        protected void chkRetornaProced_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            if (atual.Checked)
            {
                int coPaci;
                int coAgend;
                LimparGridProced();
                if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    carregaGridProced();
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);
                    coAgend = int.Parse(hidCoAgendProced.Value);

                    carregaGridProced(coAgend, coPaci, true);
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);

                    carregaGridProced(0, coPaci, true);
                }
            }
            else
            {
                int coPaci;
                int coAgend;
                LimparGridProced();
                if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    carregaGridProced();
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);
                    coAgend = int.Parse(hidCoAgendProced.Value);

                    carregaGridProced(coAgend, coPaci, true);
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);

                    carregaGridProced(0, coPaci, true);
                }
            }
            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void chkCortProc_OnChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox ck;
            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow li in grdProcedimentos.Rows)
                {
                    TextBox valorUnit;
                    TextBox valorTotal;
                    TextBox txtQtp;
                    valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                    valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                    txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                    ck = ((CheckBox)li.FindControl("chkCortProc"));
                    if (ck.ClientID == atual.ClientID)
                    {
                        if (ck.Checked)
                        {
                            valorUnit.Enabled = false;
                            valorTotal.Enabled = false;
                        }
                    }
                }
            }
            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void imgProcedHorar_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            chkRetornaProced.Checked = false;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgProcedHorar");
                    if (img.ClientID == atual.ClientID)
                    {
                        DropDownList ddlTipoHorar = ((DropDownList)linha.FindControl("ddlTipo"));
                        if (String.IsNullOrEmpty(ddlTipoHorar.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "MSG: Selecione o tipo do agendamento antes de definir os procedimentos.");
                            return;
                        }
                        else
                        {
                            HiddenField coAgend = ((HiddenField)linha.FindControl("hidCoAgenda"));
                            hidCoAgendProced.Value = coAgend.Value;
                            HiddenField CoAlu = ((HiddenField)linha.FindControl("hidCoAlu"));
                            hidCoPaciProced.Value = String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? !String.IsNullOrEmpty(CoAlu.Value) ? CoAlu.Value : ddlNomeUsu.SelectedValue : ddlNomeUsu.SelectedValue;
                            LimparGridProced();
                            if (String.IsNullOrEmpty(CoAlu.Value) && String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                            {
                                carregaGridProced(int.Parse(coAgend.Value));
                            }
                            else if (!String.IsNullOrEmpty(CoAlu.Value) && String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                            {
                                carregaGridProced(int.Parse(coAgend.Value), int.Parse(CoAlu.Value));
                            }
                            else if (String.IsNullOrEmpty(CoAlu.Value) && !String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                            {
                                carregaGridProced(int.Parse(coAgend.Value), int.Parse(ddlNomeUsu.SelectedValue));
                            }
                            else if (!String.IsNullOrEmpty(coAgend.Value) && String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) && !String.IsNullOrEmpty(CoAlu.Value))
                            {
                                carregaGridProced(int.Parse(coAgend.Value), int.Parse(CoAlu.Value));
                            }
                            else if (!String.IsNullOrEmpty(coAgend.Value) && !String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) && String.IsNullOrEmpty(CoAlu.Value))
                            {
                                carregaGridProced(int.Parse(coAgend.Value), int.Parse(ddlNomeUsu.SelectedValue));
                            }
                            else if (!String.IsNullOrEmpty(coAgend.Value) && !String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) && !String.IsNullOrEmpty(CoAlu.Value))
                            {
                                carregaGridProced(int.Parse(coAgend.Value), int.Parse(CoAlu.Value));
                            }
                        }
                    }
                }
            }

            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void lnkAddProcPla_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                CriaNovaLinhaGridProced();
            }
            else
            {
                int coPaci = int.Parse(hidCoPaciProced.Value);
                int coAgend = int.Parse(hidCoAgendProced.Value);

                CriaNovaLinhaGridProced(coPaci);
            }

            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void lnkConfirmarProced_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hidCoPaciProced.Value))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um paciente antes de atribuir procedimentos");
                    return;
                }

                int coPaci = int.Parse(hidCoPaciProced.Value);
                int coAgend = int.Parse(hidCoAgendProced.Value);

                int qntItensSelecionados = 0;
                //Inclui registro de item de planejamento na tabela TBS386_ITENS_PLANE_AVALI
                #region Inclui o Item de Planjamento
                foreach (GridViewRow lis in grdHorario.Rows)
                {
                    //Verifica a linha que foi selecionada
                    if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                    {
                        qntItensSelecionados++;
                    }
                }

                if (qntItensSelecionados > 0)
                {
                    foreach (GridViewRow lis in grdHorario.Rows)
                    {
                        //Verifica a linha que foi selecionada
                        if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                        {
                            HiddenField hidCoAgend = ((HiddenField)lis.FindControl("hidCoAgenda"));

                            if (grdProcedimentos.Rows.Count != 0)
                            {
                                foreach (GridViewRow li in grdProcedimentos.Rows)
                                {
                                    DropDownList ddlContrat;
                                    DropDownList ddlPlan;
                                    DropDownList ddlProced;
                                    DropDownList ddlSolic;
                                    TextBox txtCart;
                                    TextBox txtIdItem;
                                    TextBox valorUnit;
                                    TextBox valorTotal;
                                    TextBox txtQtp;
                                    CheckBox chkCort;
                                    ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                                    ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                                    ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                                    ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                                    txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                                    valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                                    valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                                    txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                                    txtIdItem = ((TextBox)li.FindControl("txtCoItemProced"));
                                    chkCort = ((CheckBox)li.FindControl("chkCortProc"));

                                    if (string.IsNullOrEmpty(ddlProced.SelectedValue))
                                    {
                                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um procedimento");
                                        ddlProced.Focus();
                                        AbreModalPadrao("AbreModalProcedHorar();");
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(txtQtp.Text))
                                    {
                                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe a quantidade de procedimentos");
                                        txtQtp.Focus();
                                        AbreModalPadrao("AbreModalProcedHorar();");
                                        return;
                                    }
                                    if (coPaci == 0 || coPaci <= 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Para confirmar, é necessário selecionar um paciente");
                                        AbreModalPadrao("AbreModalProcedHorar();");
                                        return;
                                    }

                                    TBS174_AGEND_HORAR agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgend.Value));

                                    if (((agend.TB250_OPERA == null) || (agend.TB251_PLANO_OPERA == null) || (agend.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == null)) && (String.IsNullOrEmpty(txtIdItem.Text)))
                                    {
                                        agend.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlContrat.SelectedValue));
                                        agend.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue));
                                        agend.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                                        agend.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                                        agend.FL_CORTESIA = (chkCort.Checked ? "S" : "N");
                                        agend.VL_CONSUL = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);
                                        TBS174_AGEND_HORAR.SaveOrUpdate(agend, true);
                                    }
                                    //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
                                    TBS386_ITENS_PLANE_AVALI tbs386;
                                    if (String.IsNullOrEmpty(txtIdItem.Text) || int.Parse(txtIdItem.Text) <= 0)
                                    {
                                        tbs386 = new TBS386_ITENS_PLANE_AVALI();
                                        //Dados do cadastro
                                        tbs386.DT_CADAS = DateTime.Now;
                                        tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                                        tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                        tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                        tbs386.IP_CADAS = Request.UserHostAddress;
                                    }
                                    else
                                    {
                                        tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(int.Parse(txtIdItem.Text));
                                    }
                                    //Dados da situação
                                    tbs386.CO_SITUA = "A";
                                    tbs386.DT_SITUA = DateTime.Now;
                                    tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                                    tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                    tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                    tbs386.IP_SITUA = Request.UserHostAddress;
                                    tbs386.DE_RESUM_ACAO = null;

                                    //Dados básicos do item de planejamento
                                    tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                                    tbs386.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                                    tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
                                    tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(int.Parse(hidCoAgend.Value), TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue)).ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
                                    tbs386.DT_INICI = agend.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
                                    tbs386.DT_FINAL = agend.DT_AGEND_HORAR; //Verifica qual a última data na lista
                                    tbs386.FL_AGEND_FEITA_PLANE = "N";
                                    tbs386.QT_PROCED = int.Parse(txtQtp.Text);
                                    tbs386.ID_OPER = String.IsNullOrEmpty(ddlContrat.Text) ? null : (int?)int.Parse(ddlContrat.Text);
                                    tbs386.ID_PLAN = String.IsNullOrEmpty(ddlPlan.Text) ? null : (int?)int.Parse(ddlPlan.Text);
                                    tbs386.FL_CORTESIA = (chkCort.Checked ? "S" : "N");

                                    tbs386.VL_PROCED = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);

                                    //Data prevista é a data do agendamento associado
                                    tbs386.DT_AGEND = agend.DT_AGEND_HORAR;

                                    TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386, true);

                                    //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                                    #region Associa o Item ao Agendamento
                                    TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == tbs386.ID_ITENS_PLANE_AVALI).FirstOrDefault();
                                    if (tbs389 == null)
                                    {
                                        tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();

                                        tbs389.TBS174_AGEND_HORAR = agend;
                                        tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                                    }
                                    TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);


                                    //agend.CO_ALU = coPaci;                        


                                    #endregion
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (grdProcedimentos.Rows.Count != 0)
                    {
                        foreach (GridViewRow li in grdProcedimentos.Rows)
                        {
                            DropDownList ddlContrat;
                            DropDownList ddlSolic;
                            DropDownList ddlPlan;
                            DropDownList ddlProced;
                            TextBox txtCart;
                            TextBox txtIdItem;
                            TextBox valorUnit;
                            TextBox valorTotal;
                            TextBox txtQtp;
                            CheckBox chkCort;
                            ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                            ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                            ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                            ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                            txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                            valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                            valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                            txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                            txtIdItem = ((TextBox)li.FindControl("txtCoItemProced"));
                            chkCort = ((CheckBox)li.FindControl("chkCortProc"));

                            if (string.IsNullOrEmpty(ddlProced.SelectedValue))
                            {
                                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um procedimento");
                                ddlProced.Focus();
                                AbreModalPadrao("AbreModalProcedHorar();");
                                return;
                            }
                            if (string.IsNullOrEmpty(txtQtp.Text))
                            {
                                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe a quantidade de procedimentos");
                                txtQtp.Focus();
                                AbreModalPadrao("AbreModalProcedHorar();");
                                return;
                            }
                            if (coPaci == 0 || coPaci <= 0)
                            {
                                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Para confirmar, é necessário selecionar um paciente");
                                AbreModalPadrao("AbreModalProcedHorar();");
                                return;
                            }

                            TBS174_AGEND_HORAR agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                            if ((String.IsNullOrEmpty(txtIdItem.Text)))
                            {
                                agend.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlContrat.SelectedValue));
                                agend.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue));
                                agend.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                                agend.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                                agend.FL_CORTESIA = (chkCort.Checked ? "S" : "N");
                                agend.VL_CONSUL = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);
                                TBS174_AGEND_HORAR.SaveOrUpdate(agend, true);
                            }
                            //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
                            TBS386_ITENS_PLANE_AVALI tbs386;
                            if (String.IsNullOrEmpty(txtIdItem.Text) || int.Parse(txtIdItem.Text) <= 0)
                            {
                                tbs386 = new TBS386_ITENS_PLANE_AVALI();
                                //Dados do cadastro
                                tbs386.DT_CADAS = DateTime.Now;
                                tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                                tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs386.IP_CADAS = Request.UserHostAddress;
                            }
                            else
                            {
                                tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(int.Parse(txtIdItem.Text));
                            }
                            //Dados da situação
                            tbs386.CO_SITUA = "A";
                            tbs386.DT_SITUA = DateTime.Now;
                            tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                            tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs386.IP_SITUA = Request.UserHostAddress;
                            tbs386.DE_RESUM_ACAO = null;

                            //Dados básicos do item de planejamento
                            tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                            tbs386.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                            tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
                            tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(coAgend, TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue)).ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
                            tbs386.DT_INICI = agend.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
                            tbs386.DT_FINAL = agend.DT_AGEND_HORAR; //Verifica qual a última data na lista
                            tbs386.FL_AGEND_FEITA_PLANE = "N";
                            tbs386.QT_PROCED = int.Parse(txtQtp.Text);
                            tbs386.ID_OPER = String.IsNullOrEmpty(ddlContrat.Text) ? null : (int?)int.Parse(ddlContrat.Text);
                            tbs386.ID_PLAN = String.IsNullOrEmpty(ddlPlan.Text) ? null : (int?)int.Parse(ddlPlan.Text);
                            tbs386.FL_CORTESIA = (chkCort.Checked ? "S" : "N");

                            tbs386.VL_PROCED = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);

                            //Data prevista é a data do agendamento associado
                            tbs386.DT_AGEND = agend.DT_AGEND_HORAR;

                            TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386, true);

                            //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                            #region Associa o Item ao Agendamento
                            TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == tbs386.ID_ITENS_PLANE_AVALI).FirstOrDefault();
                            if (tbs389 == null)
                            {
                                tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();

                                tbs389.TBS174_AGEND_HORAR = agend;
                                tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                            }
                            TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);

                            if (txtCart.Enabled == true && !String.IsNullOrEmpty(txtCart.Text))
                            {
                                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coPaci);
                                tb07.NU_CARTAO_SAUDE = txtCart.Text;
                                TB07_ALUNO.SaveOrUpdate(tb07, true);
                            }

                            #endregion
                        }
                    }
                }
                #endregion

                //LimparGridHorarios();
                CarregaGridHorariosAlter();
                //carregaGridNovaComContextoProced();
                //AbreModalPadrao("AbreModalProcedHorar();");
            }
            catch (Exception) { }
        }

        protected void carregaGridProced(int coAgend = 0, int coPaci = 0, bool carregarAnteriores = false)
        {
            try
            {
                DataTable dtV;
                dtV = null;
                dtV = CriarColunasELinhaGridProced();
                Session["GridSolic_PROC_PLA"] = dtV;

                if (coPaci != 0 && carregarAnteriores)
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                               join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                               where (tbs174.CO_ALU == coPaci)
                               select new GridProced
                               {
                                   ID_ITENS_PLANE_AVALI = null,
                                   ID_AGEND_HORAR = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                                   CONTRAT = tbs386.ID_OPER,
                                   SOLIC = tbs386.CO_COL_CADAS,
                                   PLANO = tbs386.ID_PLAN,
                                   CART = tb07.NU_PLANO_SAUDE,
                                   PROCED = tbs356.ID_PROC_MEDI_PROCE,
                                   UNIT = tbs353.VL_BASE,
                                   QTP = tbs386.QT_PROCED,
                                   CORT = tbs386.FL_CORTESIA
                               }).DistinctBy(x => x.PROCED).ToList();

                    dtV.Rows.Clear();

                    foreach (var i in res)
                    {
                        var linha = dtV.NewRow();
                        linha["CONTRAT"] = i.CONTRAT;
                        linha["PLANO"] = i.PLANO;
                        linha["SOLIC"] = i.SOLIC;
                        linha["CART"] = i.CART;
                        linha["PROCED"] = i.PROCED;
                        linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                        linha["UNIT"] = i.UNIT;
                        linha["QTP"] = i.QTP;
                        linha["TOTAL"] = i.UNIT * i.QTP;
                        linha["ID_ITENS_PLANE_AVALI"] = i.ID_ITENS_PLANE_AVALI;
                        dtV.Rows.Add(linha);
                    }
                }
                else if (coAgend != 0 && coPaci != 0)
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on coPaci equals tb07.CO_ALU
                               join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                               join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                               where (tbs174.ID_AGEND_HORAR == coAgend)
                               select new GridProced
                               {
                                   ID_ITENS_PLANE_AVALI = tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI,
                                   ID_AGEND_HORAR = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                                   CONTRAT = tbs386.ID_OPER,
                                   SOLIC = tbs386.CO_COL_CADAS,
                                   PLANO = tbs386.ID_PLAN,
                                   CART = tb07.NU_PLANO_SAUDE,
                                   PROCED = tbs356.ID_PROC_MEDI_PROCE,
                                   UNIT = tbs353.VL_BASE,
                                   QTP = tbs386.QT_PROCED,
                                   CORT = tbs386.FL_CORTESIA
                               }).ToList();

                    dtV.Rows.Clear();

                    foreach (var i in res)
                    {
                        var linha = dtV.NewRow();
                        linha["CONTRAT"] = i.CONTRAT;
                        linha["PLANO"] = i.PLANO;
                        linha["SOLIC"] = i.SOLIC;
                        linha["CART"] = i.CART;
                        linha["PROCED"] = i.PROCED;
                        linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                        linha["UNIT"] = i.UNIT;
                        linha["QTP"] = i.QTP;
                        linha["TOTAL"] = i.UNIT * i.QTP;
                        linha["ID_ITENS_PLANE_AVALI"] = i.ID_ITENS_PLANE_AVALI;
                        dtV.Rows.Add(linha);
                    }
                }
                else if (coAgend != 0 && coPaci == 0)
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                               join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                               where (tbs174.ID_AGEND_HORAR == coAgend)
                               select new GridProced
                               {
                                   ID_ITENS_PLANE_AVALI = tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI,
                                   ID_AGEND_HORAR = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                                   CONTRAT = tbs386.ID_OPER,
                                   SOLIC = tbs386.CO_COL_CADAS,
                                   PLANO = tbs386.ID_PLAN,
                                   CART = "",
                                   PROCED = tbs356.ID_PROC_MEDI_PROCE,
                                   UNIT = tbs353.VL_BASE,
                                   QTP = tbs386.QT_PROCED,
                                   CORT = tbs386.FL_CORTESIA
                               }).ToList();

                    dtV.Rows.Clear();

                    foreach (var i in res)
                    {
                        var linha = dtV.NewRow();
                        linha["CONTRAT"] = i.CONTRAT;
                        linha["PLANO"] = i.PLANO;
                        linha["SOLIC"] = i.SOLIC;
                        linha["CART"] = i.CART;
                        linha["PROCED"] = i.PROCED;
                        linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                        linha["UNIT"] = i.UNIT;
                        linha["QTP"] = i.QTP;
                        linha["TOTAL"] = i.UNIT * i.QTP;
                        linha["ID_ITENS_PLANE_AVALI"] = i.ID_ITENS_PLANE_AVALI;
                        dtV.Rows.Add(linha);
                    }
                }
                else if (coAgend == 0 && coPaci != 0)
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                               join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                               where (tbs174.CO_ALU == coPaci)
                               select new GridProced
                               {
                                   ID_ITENS_PLANE_AVALI = tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI,
                                   ID_AGEND_HORAR = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                                   CONTRAT = tbs386.ID_OPER,
                                   SOLIC = tbs386.CO_COL_CADAS,
                                   PLANO = tbs386.ID_PLAN,
                                   CART = tb07.NU_PLANO_SAUDE,
                                   PROCED = tbs356.ID_PROC_MEDI_PROCE,
                                   UNIT = tbs353.VL_BASE,
                                   QTP = tbs386.QT_PROCED,
                                   CORT = tbs386.FL_CORTESIA
                               }).ToList();

                    dtV.Rows.Clear();

                    foreach (var i in res)
                    {
                        var linha = dtV.NewRow();
                        linha["CONTRAT"] = i.CONTRAT;
                        linha["PLANO"] = i.PLANO;
                        linha["SOLIC"] = i.SOLIC;
                        linha["CART"] = i.CART;
                        linha["PROCED"] = i.PROCED;
                        linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                        linha["UNIT"] = i.UNIT;
                        linha["QTP"] = i.QTP;
                        linha["TOTAL"] = i.UNIT * i.QTP;
                        linha["ID_ITENS_PLANE_AVALI"] = i.ID_ITENS_PLANE_AVALI;
                        dtV.Rows.Add(linha);
                    }
                }
                else
                {
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                               join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                               join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                               where (tbs174.ID_AGEND_HORAR == coAgend)
                               select new GridProced
                               {
                                   ID_ITENS_PLANE_AVALI = tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI,
                                   ID_AGEND_HORAR = tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                                   CONTRAT = tbs386.ID_OPER,
                                   SOLIC = tbs386.CO_COL_CADAS,
                                   PLANO = tbs386.ID_PLAN,
                                   CART = "",
                                   PROCED = tbs356.ID_PROC_MEDI_PROCE,
                                   UNIT = tbs353.VL_BASE,
                                   QTP = tbs386.QT_PROCED,
                                   CORT = tbs386.FL_CORTESIA
                               }).ToList();

                    dtV.Rows.Clear();

                    foreach (var i in res)
                    {
                        var linha = dtV.NewRow();
                        linha["CONTRAT"] = i.CONTRAT;
                        linha["PLANO"] = i.PLANO;
                        linha["SOLIC"] = i.SOLIC;
                        linha["CART"] = i.CART;
                        linha["PROCED"] = i.PROCED;
                        linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                        linha["UNIT"] = i.UNIT;
                        linha["QTP"] = i.QTP;
                        linha["TOTAL"] = i.UNIT * i.QTP;
                        linha["ID_ITENS_PLANE_AVALI"] = i.ID_ITENS_PLANE_AVALI;
                        dtV.Rows.Add(linha);
                    }
                }

                HttpContext.Current.Session.Add("GridSolic_PROC_PLA", dtV);

                carregaGridNovaComContextoProced();
            }
            catch (Exception) { }
        }

        public class GridProced
        {
            public int? ID_ITENS_PLANE_AVALI { get; set; }
            public int ID_AGEND_HORAR { get; set; }
            public int PROCED { get; set; }
            public int? SOLIC { get; set; }
            public int? QTP { get; set; }
            public decimal UNIT { get; set; }
            public int? CONTRAT { get; set; }
            public int? PLANO { get; set; }
            public string CART { get; set; }
            public string CORT { get; set; }
        }

        protected void CriaNovaLinhaGridProced(int paci = 0)
        {
            try
            {
                Session["GridSolic_PROC_PLA"] = null;

                DataTable dtV = CriarColunasELinhaGridProced();

                if (paci != 0)
                {
                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where (tb07.CO_ALU == paci)
                               select new CriaNovaLinhaGridProcedClass
                               {
                                   CO_ALU = tb07.CO_ALU,
                                   CONTRAT = tb07.TB250_OPERA.ID_OPER,
                                   PLANO = tb07.TB251_PLANO_OPERA.ID_PLAN,
                                   CART = tb07.NU_PLANO_SAUDE
                               }).FirstOrDefault();

                    DataRow linha = dtV.NewRow();
                    linha["CONTRAT"] = res.CONTRAT != null || res.CONTRAT != 0 ? res.CONTRAT.ToString() : "";
                    linha["PLANO"] = res.PLANO != null || res.PLANO != 0 ? res.PLANO.ToString() : "";
                    linha["CART"] = !String.IsNullOrEmpty(res.CART) ? res.CART : "";
                    linha["SOLIC"] = LoginAuxili.CO_COL.ToString();
                    linha["PROCED"] = "";
                    linha["CORT"] = false;
                    linha["UNIT"] = "";
                    linha["QTP"] = "";
                    linha["TOTAL"] = "";
                    linha["ID_ITENS_PLANE_AVALI"] = "";
                    dtV.Rows.Add(linha);
                }
                else
                {
                    DataRow linha = dtV.NewRow();
                    linha["CONTRAT"] = "";
                    linha["SOLIC"] = LoginAuxili.CO_COL.ToString();
                    linha["PLANO"] = "";
                    linha["CART"] = "";
                    linha["PROCED"] = "";
                    linha["CORT"] = false;
                    linha["UNIT"] = "";
                    linha["QTP"] = "";
                    linha["TOTAL"] = "";
                    linha["ID_ITENS_PLANE_AVALI"] = "";
                    dtV.Rows.Add(linha);
                }

                Session["GridSolic_PROC_PLA"] = dtV;
                carregaGridNovaComContextoProced();

            }
            catch (Exception) { }
        }

        public class CriaNovaLinhaGridProcedClass
        {
            public int CO_ALU { get; set; }
            public int? CONTRAT { get; set; }
            public int? PLANO { get; set; }
            public string CART { get; set; }
        }

        private DataTable CriarColunasELinhaGridProced()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_ITENS_PLANE_AVALI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CONTRAT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SOLIC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CART";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.Boolean");
            dcATM.ColumnName = "CORT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "UNIT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TOTAL";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                linha = dtV.NewRow();
                linha["CONTRAT"] = (((DropDownList)li.FindControl("ddlContratProc")).SelectedValue);
                linha["PLANO"] = (((DropDownList)li.FindControl("ddlPlanoProc")).SelectedValue);
                linha["SOLIC"] = (((DropDownList)li.FindControl("ddlSolicProc")).SelectedValue);
                linha["CART"] = (((TextBox)li.FindControl("txtNrCartProc")).Text);
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProcMod")).SelectedValue);
                linha["CORT"] = (((CheckBox)li.FindControl("chkCortProc")).Checked);
                linha["UNIT"] = (((TextBox)li.FindControl("txtValorUnit")).Text);
                linha["QTP"] = (((TextBox)li.FindControl("txtQTPMod")).Text);
                linha["TOTAL"] = (((TextBox)li.FindControl("txtValorTotalMod")).Text);
                linha["ID_ITENS_PLANE_AVALI"] = (((TextBox)li.FindControl("txtCoItemProced")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        protected void carregaGridNovaComContextoProced()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_PLA"];

            grdProcedimentos.DataSource = dtV;
            grdProcedimentos.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                DropDownList ddlContrat;
                DropDownList ddlPlan;
                DropDownList ddlProced;
                DropDownList ddlSolic;
                TextBox txtCart;
                TextBox valorUnit;
                TextBox valorTotal;
                TextBox txtQtp;
                CheckBox chkCort;
                TextBox idItemProced;
                ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                chkCort = ((CheckBox)li.FindControl("chkCortProc"));
                idItemProced = ((TextBox)li.FindControl("txtCoItemProced"));

                string solic, contrat, plano, cart, proced, unit, qtp, total, cort, idItem;

                //Coleta os valores do dtv da modal popup
                solic = dtV.Rows[aux]["SOLIC"].ToString();
                contrat = dtV.Rows[aux]["CONTRAT"].ToString();
                plano = dtV.Rows[aux]["PLANO"].ToString();
                cart = dtV.Rows[aux]["CART"].ToString();
                proced = dtV.Rows[aux]["PROCED"].ToString();
                unit = dtV.Rows[aux]["UNIT"].ToString();
                qtp = dtV.Rows[aux]["QTP"].ToString();
                total = dtV.Rows[aux]["TOTAL"].ToString();
                cort = dtV.Rows[aux]["CORT"].ToString();
                idItem = dtV.Rows[aux]["ID_ITENS_PLANE_AVALI"].ToString();

                var opr = 0;

                //if (!String.IsNullOrEmpty(ddlPlanProcPlan.SelectedValue) && int.Parse(ddlPlanProcPlan.SelectedValue) != 0)
                //{
                //    var plan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanProcPlan.SelectedValue));
                //    plan.TB250_OPERAReference.Load();
                //    opr = plan.TB250_OPERA != null ? plan.TB250_OPERA.ID_OPER : 0;
                //}

                //CarregarProcedimentos(ddlCodigoi, opr, "EX");

                var tbs174_tipoConsu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendProced.Value)).TP_CONSU;

                lblTituloProcedMod.Text = (tbs174_tipoConsu.Equals("P") ? "PROCEDIMENTO" : tbs174_tipoConsu.Equals("N") ? "CONSULTA" : tbs174_tipoConsu.Equals("E") ? "EXAME" : tbs174_tipoConsu.Equals("V") ? "VACINA" : tbs174_tipoConsu.Equals("C") ? "CIRURGIA" : "OUTROS");
                CarregaOperadoras(ddlContrat, contrat);
                CarregarPlanosSaude(ddlPlan, ddlContrat);
                CarregaProcedimentos(ddlProced, ddlContrat, proced, tbs174_tipoConsu);
                AuxiliCarregamentos.CarregaProfissionaisSaude(ddlSolic, LoginAuxili.CO_EMP, false, "0");
                //SelecionaOperadoraPlanoPaciente();
                //ddlContrat.SelectedValue = contrat;
                ddlSolic.SelectedValue = solic;
                ddlPlan.SelectedValue = plano;
                CalcularPreencherValoresTabelaECalculado(ddlProced, ddlContrat, ddlPlan, valorUnit);
                txtCart.Text = cart;
                valorTotal.Text = total;
                txtQtp.Text = qtp;
                chkCort.Checked = Convert.ToBoolean(cort);
                idItemProced.Text = idItem;
                aux++;
                if (chkCort.Checked)
                {
                    valorUnit.Enabled = false;
                    valorTotal.Enabled = false;
                }

                if (String.IsNullOrEmpty(cart))
                {
                    txtCart.Enabled = true;
                }
                else
                {
                    txtCart.Enabled = false;
                }
            }
        }

        protected void imgExcPla_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int idItem = 0;
            int aux = 0;
            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedimentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcPla");
                    TextBox hidIdItem = ((TextBox)linha.FindControl("txtCoItemProced"));
                    idItem = (String.IsNullOrEmpty(hidIdItem.Text) ? 0 : int.Parse(hidIdItem.Text));
                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridProced(aux);
            LimparGridProced();
            carregaGridNovaComContextoProced();
            //LimparGridHorarios();
            //CarregaGridHorariosAlter();
            AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void ExcluiItemGridProced(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridProced();
            //try
            //{
            //    if (idItem != 0)
            //    {
            //        var tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(idItem);
            //        var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == idItem).FirstOrDefault();

            //        TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(tbs389, true);
            //        TBS386_ITENS_PLANE_AVALI.Delete(tbs386, true);
            //    }

            //}
            //catch (Exception) { }
            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_PLA"] = dtV;
            carregaGridNovaComContextoProced();
        }

        protected void LimparGridProced()
        {
            grdProcedimentos.DataSource = null;
            grdProcedimentos.DataBind();
        }
        #endregion

        #region Modal Procedimentos da Grid Histórico
        protected void imgProcedHistor_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            if (grdHistorPaciente.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHistorPaciente.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgProcedHistor");
                    if (img.ClientID == atual.ClientID)
                    {
                        HiddenField hidCoAgend = (HiddenField)linha.FindControl("hidCoAgend");
                        LimparGridProcedHistor();
                        carregaGridProcedHistor(int.Parse(hidCoAgend.Value));
                    }
                }
            }

            AbreModalPadrao("AbreModalProcedHistor();");
        }

        protected void LimparGridProcedHistor()
        {
            grdProcedHistor.DataSource = null;
            grdProcedHistor.DataBind();
        }

        protected void carregaGridProcedHistor(int coAgend)
        {
            DataTable dtV;
            dtV = null;
            dtV = CriarColunasELinhaGridProcedHistor();
            Session["Grid_PROC_HISTOR"] = dtV;

            int coPaci = String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? 0 : int.Parse(ddlNomeUsu.SelectedValue);

            if (coAgend != 0 && coPaci != 0)
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                           join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                           join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs386.ID_OPER equals tb250.ID_OPER
                           where (tbs174.CO_ALU == coPaci)
                           && (tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == coAgend)
                           select new
                           {
                               CODIGO = tbs386.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                               TIPO = tbs174.TP_CONSU.Equals("N") ? "Consulta" : tbs174.TP_CONSU.Equals("E") ? "Exame" : tbs174.TP_CONSU.Equals("P") ? "Procedimento" : tbs174.TP_CONSU.Equals("V") ? "Vacina" : "-",
                               DESC = tbs386.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               CONTRAT = tb250.NM_SIGLA_OPER,
                               PROCED = tbs356.ID_PROC_MEDI_PROCE,
                               UNIT = tbs353.VL_BASE,
                               QTP = tbs386.QT_PROCED,
                               CORT = tbs386.FL_CORTESIA
                           }).ToList().OrderBy(x => x.TIPO).ThenBy(x => x.DESC);

                dtV.Rows.Clear();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["CODIGO"] = i.CODIGO;
                    linha["TIPO"] = i.TIPO;
                    linha["DESCRICAO"] = i.DESC;
                    linha["ValorUnit"] = i.UNIT;
                    linha["QTD"] = i.QTP;
                    linha["TOTAL"] = i.UNIT * i.QTP;
                    linha["CONTRAT"] = i.CONTRAT;
                    linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                    dtV.Rows.Add(linha);
                }
            }
            else
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                           join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                           join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs386.ID_OPER equals tb250.ID_OPER
                           where (tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == coAgend)
                           select new
                           {
                               CODIGO = tbs386.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                               TIPO = tbs174.TP_CONSU.Equals("N") ? "Consulta" : tbs174.TP_CONSU.Equals("E") ? "Exame" : tbs174.TP_CONSU.Equals("P") ? "Procedimento" : tbs174.TP_CONSU.Equals("V") ? "Vacina" : "-",
                               DESC = tbs386.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               CONTRAT = tb250.NM_SIGLA_OPER,
                               PROCED = tbs356.ID_PROC_MEDI_PROCE,
                               UNIT = tbs353.VL_BASE,
                               QTP = tbs386.QT_PROCED,
                               CORT = tbs386.FL_CORTESIA
                           }).ToList().OrderBy(x => x.TIPO).ThenBy(x => x.DESC);

                dtV.Rows.Clear();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["CODIGO"] = i.CODIGO;
                    linha["TIPO"] = i.TIPO;
                    linha["DESCRICAO"] = i.DESC;
                    linha["ValorUnit"] = i.UNIT;
                    linha["QTD"] = i.QTP;
                    linha["TOTAL"] = i.UNIT * i.QTP;
                    linha["CONTRAT"] = i.CONTRAT;
                    linha["CORT"] = (String.IsNullOrEmpty(i.CORT) ? false : i.CORT.Equals("S") ? true : false);
                    dtV.Rows.Add(linha);
                }
            }

            HttpContext.Current.Session.Add("Grid_PROC_HISTOR", dtV);

            carregaGridNovaComContextoProcedHistor();
        }

        private DataTable CriarColunasELinhaGridProcedHistor()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CODIGO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TIPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DESCRICAO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ValorUnit";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTD";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TOTAL";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CONTRAT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.Boolean");
            dcATM.ColumnName = "CORT";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdProcedHistor.Rows)
            {
                linha = dtV.NewRow();
                linha["CODIGO"] = (((Label)li.FindControl("lblCodigoProced")).Text);
                linha["TIPO"] = (((Label)li.FindControl("lblTipoProced")).Text);
                linha["DESCRICAO"] = (((Label)li.FindControl("lblDescricaoProced")).Text);
                linha["ValorUnit"] = (((Label)li.FindControl("lblValorUnitProced")).Text);
                linha["QTD"] = (((Label)li.FindControl("lblQtdProced")).Text);
                linha["TOTAL"] = (((Label)li.FindControl("lblTotalProced")).Text);
                linha["CONTRAT"] = (((Label)li.FindControl("lblContratProced")).Text);
                linha["CORT"] = (((CheckBox)li.FindControl("chkCortProced")).Checked);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        protected void carregaGridNovaComContextoProcedHistor()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["Grid_PROC_HISTOR"];

            grdProcedHistor.DataSource = dtV;
            grdProcedHistor.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdProcedHistor.Rows)
            {
                Label Cod = (Label)li.FindControl("lblCodigoProced");
                Label Tipo = (Label)li.FindControl("lblTipoProced");
                Label Desc = (Label)li.FindControl("lblDescricaoProced");
                Label ValorUnit = (Label)li.FindControl("lblValorUnitProced");
                Label Qtp = (Label)li.FindControl("lblQtdProced");
                Label ValorTotal = (Label)li.FindControl("lblTotalProced");
                Label Contrat = (Label)li.FindControl("lblContratProced");
                CheckBox Cort = (CheckBox)li.FindControl("chkCortProced");

                string cod, tipo, desc, unit, qtp, total, contrat, cort;

                //Coleta os valores do dtv da modal popup
                cod = dtV.Rows[aux]["CODIGO"].ToString();
                tipo = dtV.Rows[aux]["TIPO"].ToString();
                desc = dtV.Rows[aux]["DESCRICAO"].ToString();
                unit = dtV.Rows[aux]["ValorUnit"].ToString();
                qtp = dtV.Rows[aux]["QTD"].ToString();
                total = dtV.Rows[aux]["TOTAL"].ToString();
                contrat = dtV.Rows[aux]["CONTRAT"].ToString();
                cort = dtV.Rows[aux]["CORT"].ToString();

                Cod.Text = cod;
                Tipo.Text = tipo;
                Desc.Text = desc;
                ValorUnit.Text = unit;
                Qtp.Text = qtp;
                ValorTotal.Text = total;
                Contrat.Text = contrat;
                Cort.Checked = Boolean.Parse(cort);

                aux++;
            }
        }

        #endregion

        #region Modal Tipo Consulta Retorno
        protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList atual = (DropDownList)sender;

                int qntItensSelecionados = 0;
                var emp = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                emp.TB83_PARAMETROReference.Load();
                int? diasLimite = emp.TB83_PARAMETRO.NU_VALID_RETORNO != null ? emp.TB83_PARAMETRO.NU_VALID_RETORNO.Value : 0;
                foreach (GridViewRow lis in grdHorario.Rows)
                {
                    //Verifica a linha que foi selecionada
                    if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                    {
                        qntItensSelecionados++;
                    }
                }

                if (qntItensSelecionados > 1)
                {
                    foreach (GridViewRow lis in grdHorario.Rows)
                    {
                        //Verifica a linha que foi selecionada
                        if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                        {
                            HiddenField hidCoAgend = ((HiddenField)lis.FindControl("hidCoAgenda"));

                            int coAgend;
                            if (String.IsNullOrEmpty(hidCoAgend.Value))
                            {

                            }
                            else
                            {
                                coAgend = int.Parse(hidCoAgend.Value);
                                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                                tbs174.TP_CONSU = atual.SelectedValue;

                                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                                CarregaGridHorariosAlter();
                            }
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow l in grdHorario.Rows)
                    {
                        DropDownList ddl = ((DropDownList)l.FindControl("ddlTipo"));

                        if (atual.ClientID == ddl.ClientID)
                        {
                            HiddenField hidAgend = ((HiddenField)l.FindControl("hidCoAgenda"));
                            HiddenField hidCoCol = ((HiddenField)l.FindControl("hidCoCol"));
                            int coAgend;
                            int coCol;
                            if (String.IsNullOrEmpty(hidAgend.Value))
                            {

                            }
                            else
                            {
                                coAgend = int.Parse(hidAgend.Value);
                                coCol = int.Parse(hidCoCol.Value);
                                if (ddl.SelectedValue.Equals("R"))
                                {
                                    int coPaci = String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? 0 : int.Parse(ddlNomeUsu.SelectedValue);

                                    if (coPaci <= 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um paciente para marcar consulta de retorno.");
                                        txtNomePacPesq.Focus();
                                        ddlNomeUsu.Focus();
                                        CarregaGridHorariosAlter();
                                        return;
                                    }
                                    hidCoColAgendRetorno.Value = coCol.ToString();
                                    hidCoAgendRetorno.Value = coAgend.ToString();
                                    LimparGridAgendaRetorno();
                                    carregaGridGridAgendaRetorno(diasLimite);
                                    AbreModalPadrao("AbreModalAgendaRetorno();");
                                }
                                else
                                {
                                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                                    tbs174.TP_CONSU = ddl.SelectedValue;

                                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                                    CarregaGridHorariosAlter();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        protected void chkAgendaR_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox atual = (CheckBox)sender;

                foreach (GridViewRow l in grdAgendaRetorno.Rows)
                {
                    CheckBox chk = ((CheckBox)l.FindControl("chkAgendaR"));

                    if (atual.ClientID != chk.ClientID)
                    {
                        if (chk.Checked)
                            chk.Checked = false;
                    }

                }
                AbreModalPadrao("AbreModalAgendaRetorno();");
            }
            catch (Exception) { }
        }

        protected void lnkConfirmarRetorno_OnClick(object sender, EventArgs e)
        {
            try
            {
                int qntMarcados = 0;
                foreach (GridViewRow l in grdAgendaRetorno.Rows)
                {
                    CheckBox chk = ((CheckBox)l.FindControl("chkAgendaR"));

                    if (chk.Checked)
                    {
                        qntMarcados++;
                    }
                }

                if (qntMarcados > 0)
                {

                    foreach (GridViewRow l in grdAgendaRetorno.Rows)
                    {
                        CheckBox chk = ((CheckBox)l.FindControl("chkAgendaR"));
                        Label Rap = (Label)l.FindControl("lblRapAgendaR");

                        if (chk.Checked)
                        {
                            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendRetorno.Value));
                            tbs174.NU_RAP_RETORNO = Rap.Text;
                            tbs174.TP_CONSU = "R";
                            TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                        }
                    }
                }

                CarregaGridHorariosAlter();
            }
            catch (Exception) { }
        }

        protected void LimparGridAgendaRetorno()
        {
            grdAgendaRetorno.DataSource = null;
            grdAgendaRetorno.DataBind();
        }

        protected void carregaGridGridAgendaRetorno(int? diasLimite = 0)
        {
            DataTable dtV;
            dtV = null;
            dtV = CriarColunasELinhaGridAgendaRetorno();
            Session["Grid_Agenda_Retorno"] = dtV;

            lblValidadeRetorno.InnerText = "VALIDADE DE RETORNO: " + diasLimite.ToString() + " DIAS";

            int coPaci = String.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? 0 : int.Parse(ddlNomeUsu.SelectedValue);

            if (coPaci <= 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário selecionar um paciente para marcar consulta de retorno.");
                txtNomePacPesq.Focus();
                ddlNomeUsu.Focus();
                return;
            }
            DateTime dataAtual = DateTime.Now;
            DateTime dataLimite = dataAtual;

            if (coPaci != 0)
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                           join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI equals tbs386.ID_ITENS_PLANE_AVALI
                           join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs386.ID_OPER equals tb250.ID_OPER
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where (tbs174.CO_ALU == coPaci)
                           && (tbs174.CO_SITUA_AGEND_HORAR == "R")
                           && (tbs174.TP_CONSU != "R")
                           select new
                           {
                               DATA = tbs389.TBS174_AGEND_HORAR.DT_AGEND_HORAR,
                               HORA = tbs389.TBS174_AGEND_HORAR.HR_AGEND_HORAR,
                               RAP = tbs389.TBS174_AGEND_HORAR.NU_REGIS_CONSUL,
                               CO_COL = tb03.CO_COL,
                               PROFI = tb03.NO_APEL_COL,
                               ESPEC = tb03.DE_FUNC_COL
                           }).ToList().OrderBy(x => x.PROFI).ThenBy(x => x.DATA).ThenBy(x => x.HORA).ToList();

                if (diasLimite.HasValue && diasLimite > 0)
                {
                    dataLimite = dataLimite.AddDays(-diasLimite.Value);
                    res = res.Where(x => x.DATA >= dataLimite && x.DATA <= dataAtual).ToList();
                }

                dtV.Rows.Clear();

                foreach (var i in res)
                {
                    var linha = dtV.NewRow();
                    linha["CO_COL"] = i.CO_COL;
                    linha["DATA"] = i.DATA.ToShortDateString();
                    linha["HORA"] = i.HORA;
                    linha["RAP"] = i.RAP;
                    linha["PROFISSIONAL"] = i.PROFI;
                    linha["ESPECIALIDADE"] = i.ESPEC;
                    dtV.Rows.Add(linha);
                }
            }

            HttpContext.Current.Session.Add("Grid_Agenda_Retorno", dtV);

            carregaGridNovaComContextoGridAgendaRetorno();
        }

        private DataTable CriarColunasELinhaGridAgendaRetorno()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CO_COL";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DATA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "HORA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "RAP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROFISSIONAL";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ESPECIALIDADE";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdAgendaRetorno.Rows)
            {
                linha = dtV.NewRow();
                linha["CO_COL"] = (((Label)li.FindControl("lblCoColAgendaR")).Text);
                linha["DATA"] = (((Label)li.FindControl("lblDataAgendaR")).Text);
                linha["HORA"] = (((Label)li.FindControl("lblHoraAgendaR")).Text);
                linha["RAP"] = (((Label)li.FindControl("lblRapAgendaR")).Text);
                linha["PROFISSIONAL"] = (((Label)li.FindControl("lblProfiAgendaR")).Text);
                linha["ESPECIALIDADE"] = (((Label)li.FindControl("lblEspecAgendaR")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        protected void carregaGridNovaComContextoGridAgendaRetorno()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["Grid_Agenda_Retorno"];

            grdAgendaRetorno.DataSource = dtV;
            grdAgendaRetorno.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdAgendaRetorno.Rows)
            {
                var chk = (CheckBox)li.FindControl("chkAgendaR");
                Label CoCol = (Label)li.FindControl("lblCoColAgendaR");
                Label Data = (Label)li.FindControl("lblDataAgendaR");
                Label Hora = (Label)li.FindControl("lblHoraAgendaR");
                Label Rap = (Label)li.FindControl("lblRapAgendaR");
                Label Profi = (Label)li.FindControl("lblProfiAgendaR");
                Label Espec = (Label)li.FindControl("lblEspecAgendaR");

                string cocol, data, hora, rap, profi, espec;

                //Coleta os valores do dtv da modal popup
                cocol = dtV.Rows[aux]["CO_COL"].ToString();
                data = dtV.Rows[aux]["DATA"].ToString();
                hora = dtV.Rows[aux]["HORA"].ToString();
                rap = dtV.Rows[aux]["RAP"].ToString();
                profi = dtV.Rows[aux]["PROFISSIONAL"].ToString();
                espec = dtV.Rows[aux]["ESPECIALIDADE"].ToString();

                CoCol.Text = cocol;
                Data.Text = data;
                Hora.Text = hora;
                Rap.Text = rap;
                Profi.Text = profi;
                Espec.Text = espec;

                aux++;

                if (CoCol.Text != hidCoColAgendRetorno.Value)
                    chk.Enabled = false;
            }
        }

        #endregion

        protected void LinkButtonSIM_Click(object sender, EventArgs e)
        {
            Persistencias();
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