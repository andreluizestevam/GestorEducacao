//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 17/10/2014| Maxwell Almeida            |  Criação da funcionalidade para registro de Colaboradores de Campanhas de Saúde
//           |                            | 
//           |                            |  

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9117_AtendimentoCampanhaAgenda
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Váriaveis

        int qtdLinhasGrid = 0;
        #endregion

        private static Dictionary<string, string> tipoDeficiencia = AuxiliBaseApoio.chave(tipoDeficienciaAluno.ResourceManager);

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

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

                IniPeriAG.Text = FimPeriAG.Text = data.ToString();
                txtIniAgenda.Text = data.AddDays(-5).ToString();
                txtFimAgenda.Text = data.AddDays(15).ToString();

                CarregaAgendamentos();

                CarregaTipos();
                CarregaSituacoes();
                //CarregaUF();
                //CarregaBairros("", 0);
                //CarregaCidades("");
                CarregaClassificacoes();
                CarregaCompetencias();
                //CarregaPacientes();
                CarregaGrid();
                //CarregaResponsaveis();
                CarregaUnidades();
                CarregaVacinas(0);
                //CarregaDeficiencia();

                CarregaUFLocal();
                CarregaCidadesLocal();
                CarregaBairrosLocal();

                //carregaGridVacinas();

                //AuxiliCarregamentos.CarregaUFs(ddlUfRgUsu, false, LoginAuxili.CO_EMP);

                txtDataAtend.Text = DateTime.Now.Date.ToString();
                txtHoraAtend.Text = DateTime.Now.ToString("HH:mm");

                CarregaDados();
            }
            else
            {
                //int idCamp = 0;
                ////Coleta o ID da Campanha selecionada
                //foreach (GridViewRow li in grdCampSaude.Rows)
                //{
                //    if (((CheckBox)li.Cells[0].FindControl("chkSelectCamp")).Checked)
                //    {
                //        idCamp = int.Parse(((HiddenField)li.Cells[0].FindControl("hidCoCampan")).Value);
                //        break;
                //    }
                //}

                //int idVacina = (!string.IsNullOrEmpty(ddlVacina.SelectedValue) ? int.Parse(ddlVacina.SelectedValue) : 0);
                //int coAlu = !string.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? int.Parse(ddlNomeUsu.SelectedValue) : 0;

                //VerificaExisteAtendimento(coAlu, idCamp);

                ////Executa a verificação de se há vacinas ou não para o usuário selecionado apenas se a campanha for de vacinação
                //if (idCamp != 0)
                //{
                //    if (TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(idCamp).CO_TIPO_CAMPAN == "V")
                //    {
                //        VerificaVacinasCampanhaUsuario(idVacina, idCamp, coAlu);
                //    }
                //}
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ExecutaRotinasPersistencia())
            {
                SalvaEmSessaoDados();
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro de atendimento de Campanha de Saúde Realizado.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Executa as rotinas de persistências de dados
        /// </summary>
        private bool ExecutaRotinasPersistencia()
        {
            if (string.IsNullOrEmpty(hidIdCampa.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a campanha para a qual o atendimento está sendo registrado");
                return false;
            }

            //apresenta retorno caso não tenha sido selecionado nenhum usuário
            //if ((chkEhUsuarCadastrado.Checked) && ((string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente que receberá o atendimento na campanha");
            //    ddlNomeUsu.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(hidIdAgenda.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda para definir o paciente!");
                grdPacientes.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDataAtend.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Campo Data do Atendimento é requerido");
                txtDataAtend.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtHoraAtend.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O campo Hora do Atendimento é requerido");
                txtHoraAtend.Focus();
                return false;
            }

            //VerificaDadosUsuarioRespon();

            if (string.IsNullOrEmpty(hidCoAlu.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Usuário antes de salvar.");
                return false;
            }

            if (string.IsNullOrEmpty(hidCoResp.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Responsável antes de salvar.");
                return false;
            }

            int coAlu = int.Parse(hidCoAlu.Value);
            var infoPaci = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where tb07.CO_ALU == coAlu
                            select new
                            {
                                tb07.CO_EMP,
                                tb07.CO_ALU,
                                tb07.TB108_RESPONSAVEL.CO_RESP
                            }).FirstOrDefault();

            int idCampanha = int.Parse(hidIdCampa.Value);

            //Executa esta rotina apenas se a campanha não for de vacinação, pois a verificação na campanha de vacinação é em outro processo
            if (TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(idCampanha).CO_TIPO_CAMPAN != "V")
            {
                if (VerificaExisteAtendimento(coAlu, idCampanha))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Usuário já possui atendimento nesta campanha");
                    return false;
                }
            }

            TBS341_CAMP_ATEND tbs341 = new TBS341_CAMP_ATEND();

            tbs341.TBS339_CAMPSAUDE = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(idCampanha);
            tbs341.DT_ATEND_CAMP = DateTime.Parse(txtDataAtend.Text);
            tbs341.HR_ATEND_CAMP = txtHoraAtend.Text;
            tbs341.CO_EMP_ALU = infoPaci.CO_EMP;
            tbs341.CO_ALU = infoPaci.CO_ALU;
            tbs341.CO_RESP_ALU = infoPaci.CO_RESP;
            tbs341.CO_EMP_ATEND = ddlUnidCampan.SelectedValue != "" ? int.Parse(ddlUnidCampan.SelectedValue) : (int?)null;
            tbs341.CO_COL_ATEND = LoginAuxili.CO_COL;
            tbs341.NM_LOCAL_ATEND = (!string.IsNullOrEmpty(txtNomeLocal.Text) ? txtNomeLocal.Text : null);
            tbs341.DE_OBSER_ATEND = (!string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : null);
            tbs341.CO_IP_REGIS_USUAR_CAMPAN = Request.UserHostAddress;
            tbs341.DT_CADAS = DateTime.Now;
            tbs341.CO_ALU = int.Parse(hidCoAlu.Value);
            tbs341.CO_RESP_ALU = int.Parse(hidCoResp.Value);
            tbs341.TB74_UF = (!string.IsNullOrEmpty(ddlUFLocal.SelectedValue) ? TB74_UF.RetornaPelaChavePrimaria(ddlUFLocal.SelectedValue) : null);
            tbs341.TB904_CIDADE = (!string.IsNullOrEmpty(ddlCidadeLocal.SelectedValue) ? TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(ddlCidadeLocal.SelectedValue)) : null);
            tbs341.CO_BAIRRO = (!string.IsNullOrEmpty(ddlBairroLocal.SelectedValue) ? int.Parse(ddlBairroLocal.SelectedValue) : (int?)null);
            tbs341.NU_TEL_LOCAL = (!string.IsNullOrEmpty(txtTelLocal.Text) ? txtTelLocal.Text.Replace("(", "").Replace(")", "").Replace("-", "").Trim() : null);
            tbs341.DE_ENDE_LOCAL = (!string.IsNullOrEmpty(txtEndeLocal.Text) ? txtEndeLocal.Text : null);

            TBS341_CAMP_ATEND.SaveOrUpdate(tbs341, true);

            //Salva as vacinas apenas se for campanha de vacinação
            if (TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(idCampanha).CO_TIPO_CAMPAN == "V")
            {
                //Realiza as persistências de dados de vacinas relacionadas à este atendimento
                foreach (GridViewRow vac in grdVacinas.Rows)
                {
                    int idVacina = int.Parse((((HiddenField)vac.Cells[3].FindControl("hidIdVacina")).Value));

                    TBS359_VACIN_ATEND_CAMPA tbs359 = new TBS359_VACIN_ATEND_CAMPA();
                    tbs359.TBS341_CAMP_ATEND = tbs341;
                    tbs359.TBS339_CAMPSAUDE = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(tbs341.TBS339_CAMPSAUDE.ID_CAMPAN);
                    tbs359.TBS345_VACINA = TBS345_VACINA.RetornaPelaChavePrimaria(idVacina);
                    tbs359.CO_COL = LoginAuxili.CO_COL;
                    tbs359.CO_EMP = LoginAuxili.CO_EMP;
                    tbs359.DT_CADAS = DateTime.Now;
                    tbs359.IP_CADAS = Request.UserHostAddress;

                    tbs359.DT_PROX_DOSE = DateTime.Parse(txtProxDose.Text);
                    tbs359.DE_DOSE_APLCDA = !string.IsNullOrEmpty(drpDoseAplicada.SelectedValue) ? int.Parse(drpDoseAplicada.SelectedValue) : (int?)null;

                    TBS359_VACIN_ATEND_CAMPA.SaveOrUpdate(tbs359, true);
                }
            }
            return true;
        }

        /// <summary>
        /// Carrega os tipos de campanhas
        /// </summary>
        private void CarregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, true);
        }

        /// <summary>
        /// Carrega as competências
        /// </summary>
        private void CarregaCompetencias()
        {
            AuxiliCarregamentos.CarregaCompetenciasCampanhaSaude(ddlCompetencia, true);
        }

        /// <summary>
        /// Carrega as Classificações
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesCampanhaSaude(ddlClassCamp, true);
        }

        /// <summary>
        /// Carrega as Situacoes
        /// </summary>
        private void CarregaSituacoes()
        {
            AuxiliCarregamentos.CarregaSituacaoCampanhaSaude(ddlSituCampSaude, true);
        }

        /// <summary>
        /// Carrega as Vacinas associadas à campanha de saúde recebida como parâmetro
        /// </summary>
        /// <param name="ID_CAMPAN"></param>
        private void CarregaVacinas(int ID_CAMPAN)
        {
            //AuxiliCarregamentos.CarregaVacinas(ddlVacina, false, false);

            var res = (from tbs360 in TBS360_VACIN_CAMPSAUDE.RetornaTodosRegistros()
                       where tbs360.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                       select new { tbs360.TBS345_VACINA.NM_VACINA, tbs360.TBS345_VACINA.ID_VACINA }).OrderBy(w => w.NM_VACINA).ToList();

            ddlVacina.DataTextField = "NM_VACINA";
            ddlVacina.DataValueField = "ID_VACINA";
            ddlVacina.DataSource = res;
            ddlVacina.DataBind();

            ddlVacina.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega a Grid de campanhas em andamento
        /// </summary>
        private void CarregaGrid()
        {
            string comp = ddlCompetencia.SelectedValue;
            string classif = ddlClassCamp.SelectedValue;
            string tipo = ddlTipoCamp.SelectedValue;
            string situ = ddlSituCampSaude.SelectedValue;
            DateTime dtAtu = DateTime.Now;
            var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                       where (comp != "0" ? tbs339.CO_COMPE_TIPO_CAMPAN == comp : 0 == 0)
                       && (classif != "0" ? tbs339.CO_CLASS_CAMPAN == classif : 0 == 0)
                       && (tipo != "0" ? tbs339.CO_TIPO_CAMPAN == tipo : 0 == 0)
                       && (situ != "0" ? tbs339.CO_SITUA_TIPO_CAMPAN == situ : 0 == 0)
                       && ((dtAtu >= tbs339.DT_INICI_CAMPAN) && (dtAtu <= tbs339.DT_TERMI_CAMPAN))
                       select new campanhaSaude
                       {
                           ID_CAMPAN = tbs339.ID_CAMPAN,
                           noCampa = tbs339.NM_CAMPAN,
                           tipo = tbs339.CO_TIPO_CAMPAN,
                           comp = tbs339.CO_COMPE_TIPO_CAMPAN,
                           dataInicio = tbs339.DT_INICI_CAMPAN,
                           HORA = tbs339.HR_INICI_CAMPAN,
                           classi = tbs339.CO_CLASS_CAMPAN,
                       }).ToList();

            grdCampSaude.DataSource = res;
            grdCampSaude.DataBind();
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
                       && (tbs174.TP_CONSU == "V")
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
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into late
                       from IIlaten in late.DefaultIfEmpty()
                       where tbs174.CO_ALU == CO_ALU && tbs174.TP_CONSU == "V"
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
                       select new saidaHistoricoAgenda
                       {
                           horaAgenda = tbs174.HR_AGEND_HORAR,
                           dtAgenda = tbs174.DT_AGEND_HORAR,
                           dtAgenda_Atend = (IIlaten != null ? IIlaten.DT_REALI : (DateTime?)null),
                           DE_ACAO = (IIlaten != null ? (!string.IsNullOrEmpty(IIlaten.DE_ACAO_REALI) ? IIlaten.DE_ACAO_REALI : " - ") : (!string.IsNullOrEmpty(tbs174.DE_ACAO_PLAN) ? tbs174.DE_ACAO_PLAN : " - ")),
                           CLASS_FUNCI_R = tb03.CO_CLASS_PROFI,
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
            public DateTime? dtAgenda_Atend { get; set; }
            public string dtAgenda_Atend_V
            {
                get
                {
                    return (this.dtAgenda_Atend.HasValue ? this.dtAgenda_Atend.Value.ToString("dd/MM/yy") + " - " + dtAgenda_Atend.Value.ToString("HH:mm") : " - ");
                }
            }
            public string DE_ACAO { get; set; }
            public string CLASS_FUNCI_R { get; set; }
            public string CLASS_FUNCI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CLASS_FUNCI_R);
                }
            }

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
            public string imagem_URL_ACAO
            {
                get
                {
                    //Se puder clicar, então está em aberto, e mostra mão negativa, do contrário, mostra positiva
                    return (!this.dtAgenda_Atend.HasValue ? "/Library/IMG/PGS_IC_Negativo.png" : "/Library/IMG/PGS_IC_Positivo.png");
                }
            }
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        //private void CarregaPacientes()
        //{
        //    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
        //               select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

        //    if (res != null)
        //    {
        //        ddlNomeUsu.DataTextField = "NO_ALU";
        //        ddlNomeUsu.DataValueField = "CO_ALU";
        //        ddlNomeUsu.DataSource = res;
        //        ddlNomeUsu.DataBind();
        //    }

        //    ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));
        //}

        /// <summary>
        /// Carrega os tipos de deficiência do aluno
        /// </summary>
        //private void CarregaDeficiencia()
        //{
        //    ddlDeficUsu.Items.Clear();
        //    ddlDeficUsu.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoDeficienciaAluno.ResourceManager));
        //    ddlDeficUsu.SelectedValue = tipoDeficiencia[tipoDeficienciaAluno.N];
        //}

        /// <summary>
        /// Calcula a Idade do Paciente de acordo com a data de nascimento inserida no campo DT Nascimento.
        /// </summary>
        //private void CalculaIdadeUsu()
        //{
        //    //int anos = DateTime.Now.Year - dtNasci.Year;

        //    //if (DateTime.Now.Month < dtNasci.Month || (DateTime.Now.Month == dtNasci.Month && DateTime.Now.Day < dtNasci.Day))
        //    //    anos--;

        //    //string idade = anos.ToString();

        //    DateTime dtNasci = DateTime.Parse(txtDtNascUsu.Text);

        //    lblIdade.Text = AuxiliFormatoExibicao.FormataDataNascimento(dtNasci);
        //}

        /// <summary>
        /// Carrega todas as UFs
        /// </summary>
        //private void CarregaUF()
        //{
        //    AuxiliCarregamentos.CarregaUFs(ddlUFUsu, false, null, false);
        //    ddlUFUsu.Items.Insert(0, new ListItem("", ""));
        //}

        /// <summary>
        /// Método responsável por carregar a UF do Local de Atendimento
        /// </summary>
        private void CarregaUFLocal()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUFLocal, false, null, false, true);
        }

        /// <summary>
        /// Carrega as Cidades
        /// </summary>
        //private void CarregaCidades(string COUF)
        //{
        //    AuxiliCarregamentos.CarregaCidades(ddlCidadeUsu, false, COUF, LoginAuxili.CO_EMP, true, false);
        //    ddlCidadeUsu.Items.Insert(0, new ListItem("", ""));
        //}

        /// <summary>
        /// Método responsável por carregar a Cidade do Local de Atendimento
        /// </summary>
        private void CarregaCidadesLocal()
        {
            AuxiliCarregamentos.CarregaCidades(ddlCidadeLocal, false, ddlUFLocal.SelectedValue, LoginAuxili.CO_EMP, true, false, true);
        }

        /// <summary>
        /// Carrega os Bairros
        /// </summary>
        //private void CarregaBairros(string COUF, int CO_CIDADE)
        //{
        //    AuxiliCarregamentos.CarregaBairros(ddlBairroUsu, COUF, CO_CIDADE, false, false);
        //    ddlBairroUsu.Items.Insert(0, new ListItem("", ""));
        //}

        /// <summary>
        /// Método responsável por carregar o Bairro do Local de Atendimento
        /// </summary>
        private void CarregaBairrosLocal()
        {
            int cid = ddlCidadeLocal.SelectedValue != "" ? int.Parse(ddlCidadeLocal.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaBairros(ddlBairroLocal, ddlUFLocal.SelectedValue, cid, false, false, true);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidCampan, LoginAuxili.ORG_CODIGO_ORGAO, false, false, false);
            ddlUnidCampan.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega os Responsáveis
        /// </summary>
        //private void CarregaResponsaveis()
        //{
        //    AuxiliCarregamentos.CarregaResponsaveis(ddlResp, LoginAuxili.ORG_CODIGO_ORGAO, false, false);
        //    ddlResp.Items.Insert(0, new ListItem("", ""));
        //}

        /// <summary>
        /// Responsável por carregar os pacientes de acordo com o cpf concedido
        /// </summary>
        //private void PesquisaPaciente()
        //{
        //    //Verifica se o usuário optou por pesquisar por CPF ou por NIRE
        //    if (chkPesqCpf.Checked)
        //    {
        //        string cpf = (txtCPFPaci.Text != "" ? txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim() : "");

        //        //Valida se o usuário digitou ou não o CPF
        //        if (txtCPFPaci.Text == "")
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF para pesquisa");
        //            return;
        //        }

        //        var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
        //                   where tb07.NU_CPF_ALU == cpf
        //                   select tb07).FirstOrDefault();

        //        if (res != null)
        //        {
        //            ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
        //            txtNomePaciente.Text = res.NO_ALU;
        //            txtNisUsu.Text = res.NU_NIS.ToString();
        //            txtCPFUsuaInfo.Text = res.NU_CPF_ALU;
        //            txtRGUsu.Text = res.CO_RG_ALU;
        //            ddlSexoUsu.SelectedValue = res.CO_SEXO_ALU;
        //            txtDtNascUsu.Text = res.DT_NASC_ALU.ToString();
        //            txtTelResUsu.Text = res.NU_TELE_RESI_ALU;
        //            ddlUFUsu.SelectedValue = res.CO_ESTA_ALU;

        //            res.TB108_RESPONSAVELReference.Load();
        //            if (res.TB108_RESPONSAVEL != null)
        //            {
        //                ddlResp.SelectedValue = res.TB108_RESPONSAVEL.CO_RESP.ToString(); ;
        //                txtCPFResp.Text = res.TB108_RESPONSAVEL.NU_CPF_RESP;
        //                ddlSexResp.SelectedValue = res.TB108_RESPONSAVEL.CO_SEXO_RESP;
        //            }

        //            res.TB905_BAIRROReference.Load();
        //            //this.CarregaCidades(res.CO_ESTA_ALU);
        //            //this.ddlCidadeUsu.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE.ToString() : "";
        //            //this.CarregaBairros(res.CO_ESTA_ALU, (res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE : 0));
        //            //this.ddlBairroUsu.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_BAIRRO.ToString() : "";

        //            res.TB905_BAIRROReference.Load();
        //            this.CarregaCidades(res.CO_ESTA_ALU);
        //            if (res.TB905_BAIRRO != null)
        //            {
        //                //Carrega e preenche a Cidade
        //                ListItem liCR = ddlCidadeUsu.Items.FindByValue(res.TB905_BAIRRO.CO_CIDADE.ToString());
        //                if (liCR != null)
        //                {
        //                    ddlCidadeUsu.SelectedValue = res.TB905_BAIRRO.CO_CIDADE.ToString();
        //                }
        //                else
        //                {
        //                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade no cadastro do Paciente não condiz com a UF no cadastro do mesmo, favor informar a correta.");
        //                    ddlCidadeUsu.Focus();
        //                }

        //                this.CarregaBairros(res.CO_ESTA_ALU, (res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE : 0));
        //                //Carrega e preenhe o bairro
        //                ListItem liBR = ddlBairroUsu.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString());
        //                if (liBR != null)
        //                {
        //                    ddlBairroUsu.SelectedValue = res.TB905_BAIRRO.CO_BAIRRO.ToString();
        //                }
        //                else
        //                {
        //                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro no cadastro do Paciente não condiz com a Cidade no cadastro do mesmo, favor informar o correto.");
        //                    ddlBairroUsu.Focus();
        //                }
        //            }

        //            //Verifica se existe na lista de bairros, um bairros de acordo com a Cidade no cadastro do responsável, caso não exista, apresenta uma mensagem de erro tratada.
        //            res.TB905_BAIRROReference.Load();

        //            CalculaIdadeUsu();
        //            //liDadosUsuario.Visible = true;
        //        }

        //    }
        //    else if (chkPesqNire.Checked)
        //    {
        //        //Valida se digitou
        //        string nispaci = txtNirePaci.Text.Trim();

        //        if (string.IsNullOrEmpty(nispaci))
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIS o qual será pesquisado.");
        //            return;
        //        }

        //        int nisPaciI = int.Parse(nispaci);

        //        var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
        //                   where tb07.NU_NIS == nisPaciI
        //                   select tb07).FirstOrDefault();

        //        //Verifica se tem algum paciente com esse nis e preenche as informacoes
        //        if (res != null)
        //        {
        //            ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
        //            txtNomePaciente.Text = res.NO_ALU;
        //            txtNisUsu.Text = res.NU_NIS.ToString();
        //            txtCPFUsuaInfo.Text = res.NU_CPF_ALU;
        //            txtRGUsu.Text = res.CO_RG_ALU;
        //            ddlSexoUsu.SelectedValue = res.CO_SEXO_ALU;
        //            txtDtNascUsu.Text = res.DT_NASC_ALU.ToString();
        //            txtTelResUsu.Text = res.NU_TELE_RESI_ALU;
        //            ddlUFUsu.SelectedValue = res.CO_ESTA_ALU;

        //            res.TB108_RESPONSAVELReference.Load();
        //            if (res.TB108_RESPONSAVEL != null)
        //            {
        //                ddlResp.SelectedValue = res.TB108_RESPONSAVEL.CO_RESP.ToString(); ;
        //                txtCPFResp.Text = res.TB108_RESPONSAVEL.NU_CPF_RESP;
        //                ddlSexResp.SelectedValue = res.TB108_RESPONSAVEL.CO_SEXO_RESP;
        //            }

        //            res.TB905_BAIRROReference.Load();
        //            this.CarregaCidades(res.CO_ESTA_ALU);

        //            if (res.TB905_BAIRRO != null)
        //            {
        //                //Carrega e preenche a Cidade
        //                ListItem liCR = ddlCidadeUsu.Items.FindByValue(res.TB905_BAIRRO.CO_CIDADE.ToString());
        //                if (liCR != null)
        //                {
        //                    ddlCidadeUsu.SelectedValue = res.TB905_BAIRRO.CO_CIDADE.ToString();
        //                }
        //                else
        //                {
        //                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade no cadastro do Paciente não condiz com a UF no cadastro do mesmo, favor informar a correta.");
        //                    ddlCidadeUsu.Focus();
        //                }

        //                this.CarregaBairros(res.CO_ESTA_ALU, (res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE : 0));
        //                //Carrega e preenhe o bairro
        //                ListItem liBR = ddlBairroUsu.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString());
        //                if (liBR != null)
        //                {
        //                    ddlBairroUsu.SelectedValue = res.TB905_BAIRRO.CO_BAIRRO.ToString();
        //                }
        //                else
        //                {
        //                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro no cadastro do Paciente não condiz com a Cidade no cadastro do mesmo, favor informar o correto.");
        //                    ddlBairroUsu.Focus();
        //                }

        //                //Verifica se existe na lista de bairros, um bairros de acordo com a Cidade no cadastro do responsável, caso não exista, apresenta uma mensagem de erro tratada.
        //                res.TB905_BAIRROReference.Load();

        //                //liDadosUsuario.Visible = true;
        //                CalculaIdadeUsu();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// providencia as alterações necessárias para quando for ou não uma unidade cadastrada
        /// </summary>
        /// <param name="EhCadastrada"></param>
        private void EhUnidadeCadastrada(bool EhCadastrada)
        {
            chkEhUnidCadas.Checked = EhCadastrada;
            liUnidCampa.Visible = EhCadastrada;
            liLocalCampa.Visible = !EhCadastrada;
        }

        /// <summary>
        /// Carrega os dados do endereço de realização de campanha de saúde de acordo com o ID_CAMP recebido como parâmetro
        /// </summary>
        /// <param name="ID_CAMP"></param>
        private void CarregaDadosCampanha(int ID_CAMP)
        {
            var res = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(ID_CAMP);
            hidIdCampa.Value = ID_CAMP.ToString();

            if (res != null)
            {
                //Se a campanha estiver sendo realizada em uma unidade cadastrada, executa as ações correspondentes
                if (res.CO_EMP_LOCAL_CAMPAN != null)
                    EhUnidadeCadastrada(true);
                else
                    EhUnidadeCadastrada(false);

                //Carrega as informações de local da campanha conforme id recebido como parâmetro
                txtNomeLocal.Text = res.NM_LOCAL_CAMPAN;
                ddlUnidCampan.SelectedValue = res.CO_EMP_LOCAL_CAMPAN.ToString();
                txtTelLocal.Text = res.NR_TELEF_LOCAL_CAMPAN;
                txtEndeLocal.Text = res.DE_ENDERE_LOCAL_CAMPAN;
                ddlUFLocal.SelectedValue = (!string.IsNullOrEmpty(res.CO_UF_LOCAL_CAMPAN) ? res.CO_UF_LOCAL_CAMPAN : "");
                CarregaCidadesLocal();
                ddlCidadeLocal.SelectedValue = (res.CO_CIDAD_LOCAL_CAMPAN.HasValue ? res.CO_CIDAD_LOCAL_CAMPAN.ToString() : "");
                CarregaBairrosLocal();
                ddlBairroLocal.SelectedValue = (res.CO_BAIRRO_LOCAL_CAMPAN.HasValue ? res.CO_BAIRRO_LOCAL_CAMPAN.ToString() : "");

                int coAlu = !string.IsNullOrEmpty(hidCoAlu.Value) ? int.Parse(hidCoAlu.Value) : 0;//!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? int.Parse(ddlNomeUsu.SelectedValue) : 0;
                VerificaExisteAtendimento(coAlu, res.ID_CAMPAN);

                //Verifica se a campanha é de vacinação, caso seja, apresenta as informações de Vacinas
                if (TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(ID_CAMP).CO_TIPO_CAMPAN == "V")
                {
                    liAddVacina.Visible =
                    liGridVacinas.Visible = true;
                }
                else
                {
                    liAddVacina.Visible =
                    liGridVacinas.Visible = false;
                }

                CarregaGridHistórico(res.ID_CAMPAN);
                CarregaVacinas(res.ID_CAMPAN);
            }
        }

        /// <summary>
        /// Método responsável por fazer a limpeza das informações da Campanha de Saúde
        /// </summary>
        private void LimpaDadosCampanha()
        {
            txtNomeLocal.Text = ddlUnidCampan.SelectedValue = ddlUFLocal.SelectedValue = txtEndeLocal.Text
            = ddlCidadeLocal.SelectedValue = ddlBairroLocal.SelectedValue = txtTelLocal.Text = "";
        }

        /// <summary>
        /// Método que verifica se ainda existe alguma linha selecionada, e caso exista, carrega as informações dela, caso não exista nenhuma, limpa todos os dados
        /// </summary>
        private void VerificaGridMarcadaCarregaEndereco()
        {
            bool TemClicado = false;
            foreach (GridViewRow li in grdCampSaude.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("chkSelectCamp")).Checked)
                {
                    int idCampan = Convert.ToInt32(grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value);
                    CarregaDadosCampanha(idCampan);
                    TemClicado = true;
                    break;
                }
            }

            if (!TemClicado)
                LimpaDadosCampanha();
        }

        /// <summary>
        /// Carrega a grid de histórico de pacientes atendidos na campanha de saúde recebida como parâmetro
        /// </summary>
        /// <param name="ID_CAMPAN"></param>
        private void CarregaGridHistórico(int ID_CAMPAN)
        {
            var res = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs341.CO_ALU equals tb07.CO_ALU
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs341.CO_EMP_ATEND equals tb25.CO_EMP into l1
                       from ls in l1.DefaultIfEmpty()
                       where tbs341.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                       select new HistAtendCampa
                       {
                           DT_ATEND = tbs341.DT_ATEND_CAMP,
                           HR_ATEND = tbs341.HR_ATEND_CAMP,
                           NO_ALU = tb07.NO_ALU,
                           DE_ACAO = "",
                           NO_EMP = ls != null ? ls.sigla : "Outro",
                       }).OrderByDescending(w => w.DT_ATEND).ThenBy(w => w.NO_ALU).ToList();

            grdHistUsuariosAtendidos.DataSource = res;
            grdHistUsuariosAtendidos.DataBind();
        }

        /// <summary>
        /// Método responsável por controlar a liberação dos campos referentes às informações do usuário ou do responsável, dependendo do que for escolhido
        /// </summary>
        /// <param name="habilita"></param>
        /// <param name="op"></param>
        //private void ControlaLiberacaoDadosUsuario(bool habilita, OpHabilita op)
        //{
        //    if (op == OpHabilita.usuario)
        //    {
        //        txtNomePaciente.Enabled = ddlSexoUsu.Enabled = txtCPFPaci.Enabled = txtDtNascUsu.Enabled
        //            = txtNuCarSaude.Enabled = txtCPFPaci.Enabled = txtRGUsu.Enabled = txtOrgRgUsu.Enabled = ddlUfRgUsu.Enabled
        //            = ddlDeficUsu.Enabled = txtTelResUsu.Enabled = txtTelCelUsu.Enabled = txtTelWhatsUsu.Enabled
        //            = txtEmailUsu.Enabled = txtCepEndeUsu.Enabled = ddlUFUsu.Enabled = ddlCidadeUsu.Enabled
        //            = ddlBairroUsu.Enabled = txtLograUsu.Enabled = txtCPFUsuaInfo.Enabled = habilita;
        //    }
        //    else
        //        ddlSexResp.Enabled = txtDtNascResp.Enabled = txtCPFResp.Enabled = habilita;
        //}

        /// <summary>
        /// Limpa os campos de acordo com o parâmetro recebido
        /// </summary>
        /// <param name="?"></param>
        //private void LimpaCamposDados(OpHabilita op)
        //{
        //    if (op == OpHabilita.usuario)
        //    {
        //        txtNomePaciente.Text = ddlSexoUsu.Text = txtCPFPaci.Text = txtDtNascUsu.Text
        //            = txtNuCarSaude.Text = txtCPFPaci.Text = txtRGUsu.Text = txtOrgRgUsu.Text = ddlUfRgUsu.Text
        //            = txtTelResUsu.Text = txtTelCelUsu.Text = txtTelWhatsUsu.Text
        //            = txtEmailUsu.Text = txtCepEndeUsu.Text = ddlUFUsu.SelectedValue = ddlCidadeUsu.SelectedValue
        //            = ddlBairroUsu.SelectedValue = txtLograUsu.Text = txtCPFUsuaInfo.Text = "";

        //        ddlDeficUsu.Text = "N";

        //        ddlCidadeUsu.Items.Clear();
        //        ddlBairroUsu.Items.Clear();
        //        ddlCidadeUsu.Items.Insert(0, new ListItem("", ""));
        //        ddlBairroUsu.Items.Insert(0, new ListItem("", ""));
        //    }
        //    else
        //        ddlSexResp.Text = txtDtNascResp.Text = txtCPFResp.Text = "";
        //}

        /// <summary>
        /// Método responsável por salvar os dados do usuário e do responsável
        /// </summary>
        //private void VerificaDadosUsuarioRespon()
        //{
        //    #region Salva Responsável

        //    TB108_RESPONSAVEL tb108;
        //    //Se for um responsável não cadastrado, efetua o salvamento das informações em um novo registro
        //    if ((string.IsNullOrEmpty(ddlResp.SelectedValue)) && (string.IsNullOrEmpty(hidCoResp.Value)))
        //    {
        //        string cpfREsp = txtCPFResp.Text.Replace("-", "").Replace(".", "").Trim();

        //        var res = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfREsp).FirstOrDefault();

        //        //Verifica se existe algum responsável com o cpf do campo, caso não exista, cadastra, caso exista, carrega o código no campo correspondente
        //        if (res == null)
        //        {
        //            tb108 = new TB108_RESPONSAVEL();

        //            tb108.NO_RESP = txtNomeResp.Text;
        //            tb108.NU_CPF_RESP = cpfREsp;
        //            tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
        //            tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
        //            tb108.NU_TELE_CELU_RESP = txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        //            tb108.NU_TELE_RESI_RESP = txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        //            tb108.CO_ORIGEM_RESP = "NN";
        //            tb108.CO_SITU_RESP = "A";

        //            ///Poe o mesmo endereço do aluno no responsável
        //            tb108.CO_CEP_RESP = (!string.IsNullOrEmpty(txtCepEndeUsu.Text) ? txtCepEndeUsu.Text : null);
        //            tb108.TB74_UF = (!string.IsNullOrEmpty(ddlUFUsu.SelectedValue) ? TB74_UF.RetornaPelaChavePrimaria(ddlUFUsu.SelectedValue) : null);
        //            tb108.CO_BAIRRO = (!string.IsNullOrEmpty(ddlBairroUsu.SelectedValue) ? int.Parse(ddlBairroUsu.SelectedValue) : (int?)null);
        //            tb108.DE_ENDE_RESP = (!string.IsNullOrEmpty(txtLograUsu.Text) ? txtLograUsu.Text : null);

        //            //Atribui valores vazios para os campos not null da tabela de Responsável.
        //            tb108.FL_NEGAT_CHEQUE = "V";
        //            tb108.FL_NEGAT_SERASA = "V";
        //            tb108.FL_NEGAT_SPC = "V";
        //            tb108.CO_INST = 0;
        //            tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

        //            tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
        //            hidCoResp.Value = tb108.CO_RESP.ToString();
        //        }
        //        else
        //            hidCoResp.Value = res.CO_RESP.ToString();

        //    }

        //    #endregion

        //    //Salva os dados do Usuário em um registro na tb07
        //    #region Salva o Usuário na TB07

        //    #region Verificações de Consistência

        //    //Executa as verificações caso seja um cadastro novo de paciente
        //    if (!chkEhUsuarCadastrado.Checked)
        //    {
        //        if (string.IsNullOrEmpty(txtNomePaciente.Text))
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o nome do Usuário");
        //            txtNomePaciente.Focus();
        //            return;
        //        }

        //        if (string.IsNullOrEmpty(txtDtNascUsu.Text))
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data de Nascimento do Usuário");
        //            txtDtNascUsu.Focus();
        //            return;
        //        }

        //        if (string.IsNullOrEmpty(ddlSexoUsu.SelectedValue))
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Sexo do Usuário");
        //            ddlSexoUsu.Focus();
        //            return;
        //        }

        //        if (string.IsNullOrEmpty(ddlSexoUsu.SelectedValue))
        //        {
        //            AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Sexo do Usuário");
        //            ddlSexoUsu.Focus();
        //            return;
        //        }
        //    }

        //    #endregion

        //    TB07_ALUNO tb07;
        //    if ((string.IsNullOrEmpty(ddlNomeUsu.SelectedValue)) && (string.IsNullOrEmpty(hidCoAlu.Value)))
        //    {
        //        string cpfUsu = txtCPFUsuaInfo.Text.Replace(".", "").Replace("-", "").Trim();
        //        var res = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfUsu).FirstOrDefault();

        //        //Verifica se existe algum Usuário com o cpf do campo, caso não exista, cadastra, caso exista, carrega o código no campo correspondente
        //        if (res == null)
        //        {
        //            tb07 = new TB07_ALUNO();

        //            tb07.NO_ALU = txtNomePaciente.Text;
        //            tb07.CO_SEXO_ALU = ddlSexoUsu.SelectedValue;
        //            tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascUsu.Text);
        //            tb07.NU_CARTAO_SAUDE_ALU = (!string.IsNullOrEmpty(txtNuCarSaude.Text) ? decimal.Parse(txtNuCarSaude.Text) : (decimal?)null);
        //            tb07.NU_CPF_ALU = cpfUsu;
        //            tb07.CO_RG_ALU = (!string.IsNullOrEmpty(txtRGUsu.Text) ? txtRGUsu.Text : null);
        //            tb07.CO_ORG_RG_ALU = (!string.IsNullOrEmpty(ddlUfRgUsu.SelectedValue) ? ddlUfRgUsu.SelectedValue : null);
        //            tb07.TP_DEF = (!string.IsNullOrEmpty(ddlDeficUsu.SelectedValue) ? ddlDeficUsu.SelectedValue : null);
        //            tb07.NU_TELE_CELU_ALU = (!string.IsNullOrEmpty(txtTelCelUsu.Text) ? txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
        //            tb07.NU_TELE_RESI_ALU = (!string.IsNullOrEmpty(txtTelResUsu.Text) ? txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
        //            tb07.NU_TELE_WHATS_ALU = (!string.IsNullOrEmpty(txtTelWhatsUsu.Text) ? txtTelWhatsUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null);
        //            tb07.NO_WEB_ALU = (!string.IsNullOrEmpty(txtEmailUsu.Text) ? txtEmailUsu.Text : null);
        //            tb07.CO_CEP_ALU = (!string.IsNullOrEmpty(txtCepEndeUsu.Text) ? txtCepEndeUsu.Text : null);
        //            tb07.CO_ESTA_ALU = (!string.IsNullOrEmpty(ddlUFUsu.SelectedValue) ? ddlUFUsu.SelectedValue : null);
        //            tb07.TB905_BAIRRO = (!string.IsNullOrEmpty(ddlBairroUsu.SelectedValue) ? TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroUsu.SelectedValue)) : null);
        //            tb07.DE_ENDE_ALU = (!string.IsNullOrEmpty(txtLograUsu.Text) ? txtLograUsu.Text : null);

        //            tb07.CO_EMP = LoginAuxili.CO_EMP;
        //            tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
        //            tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

        //            //Salva os valores para os campos not null da tabela de Usuário
        //            tb07.CO_SITU_ALU = "A";
        //            tb07.TP_DEF = "N";

        //            #region trata para criação do nire

        //            var resNire = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
        //                           select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

        //            int nir = 0;
        //            if (resNire == null)
        //            {
        //                nir = 1;
        //            }
        //            else
        //            {
        //                nir = resNire.NU_NIRE;
        //            }

        //            int nirTot = nir + 1;

        //            #endregion
        //            tb07.NU_NIRE = nirTot;

        //            tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
        //            hidCoAlu.Value = tb07.CO_ALU.ToString();
        //        }
        //        else
        //            hidCoAlu.Value = res.CO_ALU.ToString();
        //    }

        //    #endregion
        //}

        private void CriaNovaLinhaGridChequesPgto(string SIGLA, string VACINA, int ID_VACINA)
        {
            DataTable dtV = new DataTable();
            //dtV = (DataTable)Session["grdVacinasAdm"];

            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "numero";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "sigla";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VACINA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_VACINA";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            int count = 0;
            foreach (GridViewRow li in grdVacinas.Rows)
            {
                linha = dtV.NewRow();
                linha["numero"] = li.Cells[0].Text;
                linha["sigla"] = li.Cells[1].Text;
                linha["VACINA"] = li.Cells[2].Text;
                linha["ID_VACINA"] = (((HiddenField)li.Cells[3].FindControl("hidIdVacina")).Value);
                dtV.Rows.Add(linha);

                count++;
            }

            linha = dtV.NewRow();
            linha["numero"] = (count == 0 ? 1 : count);
            linha["sigla"] = SIGLA;
            linha["VACINA"] = Extensoes.RemoveAcentuacoes(VACINA);
            linha["ID_VACINA"] = ID_VACINA;
            dtV.Rows.Add(linha);

            grdVacinas.DataSource = dtV;
            grdVacinas.DataBind();

            HttpContext.Current.Session.Add("grdVacinasAdm", dtV);
            //Session["grdVacinasAdm"] = dtV;
            //carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        private void carregaGridNovaComContexto()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["grdVacinasAdm"];

            grdVacinas.DataSource = dtV;
            grdVacinas.DataBind();
        }

        /// <summary>
        /// Carrega a Grid de Vacinas vazia
        /// </summary>
        private void carregaGridVacinas()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;
            HttpContext.Current.Session.Add("grdVacinasAdm", dtV);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "numero";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "sigla";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VACINA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_VACINA";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i <= 6)
            {
                linha = dtV.NewRow();
                linha["numero"] = string.Empty;
                linha["sigla"] = string.Empty;
                linha["VACINA"] = string.Empty;
                linha["ID_VACINA"] = string.Empty;
                dtV.Rows.Add(linha);
                i++;
            }

            //HttpContext.Current.Session.Add("grdVacinasAdm", dtV);
            grdVacinas.DataSource = dtV;
            grdVacinas.DataBind();
        }

        /// <summary>
        /// Salva as informações em tela em session para recarregar posteriormente
        /// </summary>
        private void SalvaEmSessaoDados()
        {
            var parametros = ddlTipoCamp.Text + ";" + ddlCompetencia.SelectedValue + ";" + ddlClassCamp.SelectedValue + ";" + ddlSituCampSaude.SelectedValue + ";"
                //+ (chkEhUnidCadas.Checked ? "S" : "N") + ";" + ddlUnidCampan.SelectedValue + ";" + txtNomeLocal.Text + ";"
                //+ ddlUFLocal.SelectedValue + ";" + ddlCidadeLocal.SelectedValue + ";" + ddlBairroLocal.SelectedValue + ";"
                //+ txtTelLocal.Text + ";" + txtEndeLocal.Text + ";" 
                + txtObservacao.Text + ";" + hidCoAlu.Value + ";" + hidCoResp.Value;

            HttpContext.Current.Session["InfosAtendCMPS"] = parametros;
        }

        /// <summary>
        /// Carrega os dados salvos em sessão
        /// </summary>
        private void CarregaDados()
        {
            if (HttpContext.Current.Session["InfosAtendCMPS"] != null)
            {
                var parametros = HttpContext.Current.Session["InfosAtendCMPS"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');

                    var tipoCampanha = par[0];
                    var Competencia = par[1];
                    var classificacao = par[2];
                    var situacao = par[3];
                    var observacao = par[4];
                    var coAlu = par[5];
                    var coResp = par[6];

                    ddlTipoCamp.SelectedValue = tipoCampanha;
                    ddlCompetencia.SelectedValue = Competencia;
                    ddlClassCamp.SelectedValue = classificacao;
                    ddlSituCampSaude.SelectedValue = situacao;
                    txtObservacao.Text = observacao;

                    hidCoAlu.Value = coAlu;
                    //CarregaUsuarios(int.Parse(coAlu));

                    hidCoResp.Value = coResp;
                    //CarregaDadosResponsavel(int.Parse(coResp));
                }

                HttpContext.Current.Session.Remove("InfosAtendCMPS");
            }
        }

        /// <summary>
        /// Carrega os Dados do Usuário nos campos correspondentes de acordo com o CO_ALU recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        //private void CarregaUsuarios(int CO_ALU)
        //{
        //    var res = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == CO_ALU).FirstOrDefault();

        //    if (res != null)
        //    {
        //        ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
        //        txtNomePaciente.Text = res.NO_ALU;
        //        ddlSexoUsu.SelectedValue = res.CO_SEXO_ALU;
        //        txtDtNascUsu.Text = res.DT_NASC_ALU.ToString();
        //        txtNuCarSaude.Text = res.NU_CARTAO_SAUDE_ALU.ToString();
        //        txtCPFUsuaInfo.Text = res.NU_CPF_ALU;
        //        txtRGUsu.Text = res.CO_RG_ALU;
        //        txtOrgRgUsu.Text = res.CO_ORG_RG_ALU;
        //        ddlUfRgUsu.SelectedValue = res.CO_ESTA_RG_ALU;
        //        ddlDeficUsu.SelectedValue = res.TP_DEF;
        //        txtTelResUsu.Text = res.NU_TELE_RESI_ALU;
        //        txtTelCelUsu.Text = res.NU_TELE_CELU_ALU;
        //        txtTelWhatsUsu.Text = res.NU_TELE_WHATS_ALU;
        //        txtEmailUsu.Text = res.NO_WEB_ALU;

        //        ddlUFUsu.SelectedValue = res.CO_ESTA_ALU;
        //        txtLograUsu.Text = res.DE_ENDE_ALU;

        //        res.TB108_RESPONSAVELReference.Load();
        //        if (res.TB108_RESPONSAVEL != null)
        //        {
        //            ddlResp.SelectedValue = hidCoResp.Value = res.TB108_RESPONSAVEL.CO_RESP.ToString(); ;
        //            txtCPFResp.Text = res.TB108_RESPONSAVEL.NU_CPF_RESP;
        //            ddlSexResp.SelectedValue = res.TB108_RESPONSAVEL.CO_SEXO_RESP;
        //        }

        //        txtCepEndeUsu.Text = res.CO_CEP_ALU;

        //        res.TB905_BAIRROReference.Load();
        //        this.CarregaCidades(res.CO_ESTA_ALU);

        //        if (res.TB905_BAIRRO != null)
        //        {
        //            //Carrega e preenche a Cidade
        //            ListItem liCR = ddlCidadeUsu.Items.FindByValue(res.TB905_BAIRRO.CO_CIDADE.ToString());
        //            if (liCR != null)
        //            {
        //                ddlCidadeUsu.SelectedValue = res.TB905_BAIRRO.CO_CIDADE.ToString();
        //            }
        //            else
        //            {
        //                AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade no cadastro do Paciente não condiz com a UF no cadastro do mesmo, favor informar a correta.");
        //                ddlCidadeUsu.Focus();
        //            }

        //            this.CarregaBairros(res.CO_ESTA_ALU, (res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE : 0));
        //            //Carrega e preenhe o bairro
        //            ListItem liBR = ddlBairroUsu.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString());
        //            if (liBR != null)
        //            {
        //                ddlBairroUsu.SelectedValue = res.TB905_BAIRRO.CO_BAIRRO.ToString();
        //            }
        //            else
        //            {
        //                AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro no cadastro do Paciente não condiz com a Cidade no cadastro do mesmo, favor informar o correto.");
        //                ddlBairroUsu.Focus();
        //            }

        //            //Verifica se existe na lista de bairros, um bairros de acordo com a Cidade no cadastro do responsável, caso não exista, apresenta uma mensagem de erro tratada.
        //            res.TB905_BAIRROReference.Load();

        //            CalculaIdadeUsu();
        //        }

        //        //liDadosUsuario.Visible = true;
        //    }
        //    else
        //    {
        //        //liDadosUsuario.Visible = false;
        //    }
        //}

        /// <summary>
        /// Carrega os Dados do Responsável de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="CO_RESP"></param>
        //private void CarregaDadosResponsavel(int CO_RESP)
        //{
        //    var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
        //               where tb108.CO_RESP == CO_RESP
        //               select new
        //               {
        //                   tb108.NU_CPF_RESP,
        //                   tb108.CO_SEXO_RESP,
        //                   tb108.DT_NASC_RESP,
        //                   tb108.CO_RESP,
        //               }).FirstOrDefault();

        //    if (res != null)
        //    {
        //        txtCPFResp.Text = res.NU_CPF_RESP;
        //        ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
        //        ddlResp.SelectedValue = res.CO_RESP.ToString();
        //        txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
        //    }
        //    else
        //    {
        //        txtCPFResp.Text = ddlSexResp.SelectedValue = txtDtNascResp.Text = "";
        //    }
        //}

        /// <summary>
        /// De acordo com o usuário e campanha recebidos como parâmetro, verifica se já existe registro de atendimento
        /// </summary>
        /// <param name="CO_ALU"></param>
        /// <param name="ID_CAMPAN"></param>
        private bool VerificaExisteAtendimento(int CO_ALU, int ID_CAMPAN)
        {
            if (ID_CAMPAN != 0)
            {
                if (TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(ID_CAMPAN).CO_TIPO_CAMPAN != "V")
                {

                    var res = TBS341_CAMP_ATEND.RetornaPeloUsuarioECampanha(CO_ALU, ID_CAMPAN);

                    if (res != null)
                    {
                        string nomeUsu = "PACIENTE";//ddlNomeUsu.SelectedItem.Text;
                        string siglaUnid = (res.CO_EMP_ATEND.HasValue ? TB25_EMPRESA.RetornaPelaChavePrimaria(res.CO_EMP_ATEND.Value).sigla : res.NM_LOCAL_ATEND);

                        ExecutaJavaScript();
                        lblInfoTop.Text = "*** USUÁRIO JÁ POSSUI ATENDIMENTO NESTA CAMPANHA ***";
                        lblInfoBot.Text = nomeUsu.ToUpper() + " - DIA " + res.DT_ATEND_CAMP.ToString("dd/MM/yyyy") + " " + res.HR_ATEND_CAMP + " - UNIDADE " + siglaUnid;
                        liErros.Visible = true;
                        return true;
                    }
                    else
                    {
                        liErros.Visible = false;
                        ExecutaJqueryEscondeErro();
                        //lblsErros.Visible = false;
                        return false;
                    }
                }
                else
                {
                    liErros.Visible = false;
                    ExecutaJqueryEscondeErro();
                    //lblsErros.Visible = false;
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Método responsável por verificar se existe determinada vacina associada à um determinado Usuário em uma determinada Campanha de Saúde
        /// </summary>
        private void VerificaVacinasCampanhaUsuario(int idVacina, int idCamp, int coAlu)
        {
            //Verifica se o paciente selecionado já tomou esta vacina nesta campanha
            var vacin = TBS359_VACIN_ATEND_CAMPA.RetornaPaciVacinCampan(idVacina, idCamp, coAlu);

            if (vacin != null)
            {
                vacin.TBS341_CAMP_ATENDReference.Load();

                string nomeUsu = "PACIENTE";//ddlNomeUsu.SelectedItem.Text;
                string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(vacin.CO_EMP).sigla;

                ExecutaJavaScript();
                lblInfoTop.Text = "*** USUÁRIO JÁ POSSUI ESTA VACINA PARA ESTA CAMPANHA ***";
                lblInfoBot.Text = nomeUsu.ToUpper() + " - DIA " + vacin.DT_CADAS.ToString("dd/MM/yyyy") + " " + vacin.DT_CADAS.ToString("HH:mm") + " - UNIDADE " + siglaUnid;
                ddlVacina.Focus();
                liErros.Visible = true;
                return;
            }
            else
            {
                ExecutaJqueryEscondeErro();
            }

            //Verifica se a vacina em questão já está associada na grid
            foreach (GridViewRow li in grdVacinas.Rows)
            {
                string idVacinaliGrd = (((HiddenField)li.Cells[3].FindControl("hidIdVacina")).Value);
                string idvac = idVacina.ToString();
                //int idVacinaliGrd = Convert.ToInt32(grdVacinas.DataKeys[li.RowIndex].Values[0]);

                if (idVacinaliGrd == idvac)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Vacina " + ddlVacina.SelectedItem.Text + " já está associada na grid de Vacinas.");
                    return;
                }
            }

            //Cadastra nova linha com dados da vacina apenas se realmente tiver recebido o código de uma vacina válido por parâmetro
            if (idVacina != 0)
            {
                var res = TBS345_VACINA.RetornaPelaChavePrimaria(idVacina);
                CriaNovaLinhaGridChequesPgto(res.CO_SIGLA_VACINA, res.NM_VACINA, res.ID_VACINA);
            }
        }

        /// <summary>
        /// Realiza as verificações para esconder os erros da página, caso não haja nenhuma inconsistencia, ou mostrá-los caso contrário
        /// </summary>
        private void VerificacoesErros()
        {
            int idVacina = !string.IsNullOrEmpty(ddlVacina.SelectedValue) ? int.Parse(ddlVacina.SelectedValue) : 0;
            int idCamp = !string.IsNullOrEmpty(hidIdCampa.Value) ? int.Parse(hidIdCampa.Value) : 0;
            int coAlu = !string.IsNullOrEmpty(hidCoAlu.Value) ? int.Parse(hidCoAlu.Value) : 0;//!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? int.Parse(ddlNomeUsu.SelectedValue) : 0;

            if (idCamp != 0)
            {
                if (TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(idCamp).CO_TIPO_CAMPAN == "V")
                {
                    VerificaVacinasCampanhaUsuario(idVacina, idCamp, coAlu);
                }
                else
                    VerificaExisteAtendimento(coAlu, idCamp);
            }
            else
                VerificaExisteAtendimento(coAlu, idCamp);
        }

        /// <summary>
        /// Executa método javascript que corrige algumas regras faltantes
        /// </summary>
        private void ExecutaJavaScript()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "ErroVacinas();",
                true
            );
        }

        /// <summary>
        /// Executa método javascript que corrige algumas regras faltantes
        /// </summary>
        private void ExecutaJqueryEscondeErro()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "EscondeErro();",
                true
            );
        }

        #endregion

        #region Classe

        public enum OpHabilita
        {
            usuario,
            responsavel,
        }

        public class campanhaSaude
        {
            public int ID_CAMPAN { get; set; }
            public string noCampa { get; set; }
            public string tipo { get; set; }
            public string tipo_Valid
            {
                get
                {
                    string tipo = "";
                    switch (this.tipo)
                    {
                        case "V":
                            tipo = "Vacinação";
                            break;
                        case "A":
                            tipo = "Ações";
                            break;
                        case "P":
                            tipo = "Programas";
                            break;
                    }

                    return tipo;
                }
            }
            public string comp { get; set; }
            public string comp_Valid
            {
                get
                {
                    string s = "";
                    switch (this.comp)
                    {
                        case "F":
                            s = "Federal";
                            break;
                        case "E":
                            s = "Estadual";
                            break;
                        case "M":
                            s = "Municipal";
                            break;
                        case "X":
                            s = "Conjunta";
                            break;
                        case "O":
                            s = "Outras";
                            break;
                    }
                    return s;

                }
            }
            public string classi { get; set; }
            public string classi_Valid
            {
                get
                {
                    string s = "";
                    switch (this.classi)
                    {
                        case "EDU":
                            s = "Educativa";
                            break;
                        case "TEM":
                            s = "Temática";
                            break;
                        case "PRO":
                            s = "Programada";
                            break;
                        case "EPI":
                            s = "Epidemia";
                            break;
                        case "OUT":
                            s = "Outras";
                            break;
                    }
                    return s;
                }
            }
            public DateTime dataInicio { get; set; }
            public string dataValid
            {
                get
                {
                    return this.dataInicio.ToString("dd/MM/yy");
                }
            }
            public string HORA { get; set; }
        }

        public class HistAtendCampa
        {
            public DateTime DT_ATEND { get; set; }
            public string HR_ATEND { get; set; }
            public string dataValid
            {
                get
                {
                    return this.DT_ATEND.ToString("dd/MM/yy") + " " + this.HR_ATEND;
                }
            }
            public string NO_ALU { get; set; }
            public string DE_ACAO { get; set; }
            public string NO_EMP { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void grdCampSaude_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdCampSaude.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;
            }
        }

        protected void grdCampSaude_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdCampSaude.Rows)
                {
                    grdVacinas.DataSource = null;
                    grdVacinas.DataBind();

                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkSelectCamp");
                    int idCamp = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidCoCampan")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (idCamp == Convert.ToInt32(grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value))
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            hidIdCampa.Value = "";
                            VerificaGridMarcadaCarregaEndereco();
                            //GridPreAtendimentoDesmarcada();
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                            int idCampan = Convert.ToInt32(grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value);

                            CarregaDadosCampanha(idCamp);
                        }
                    }
                    else
                        chk.Checked = false;
                }
            }
        }

        protected void chkEhUnidCadas_OnCheckedChanged(object sender, EventArgs e)
        {
            //Altera o que mostra em relação à onde o Atendimento está sendo atendiddo
            if (chkEhUnidCadas.Checked)
            {
                liUnidCampa.Visible = true;
                liLocalCampa.Visible = false;
            }
            else
            {
                liUnidCampa.Visible = false;
                liLocalCampa.Visible = true;
            }
        }

        protected void chkSelectCamp_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            grdVacinas.DataSource = null;
            grdVacinas.DataBind();

            // Valida se a grid de atividades possui algum registro
            if (grdCampSaude.Rows.Count != 0)
            {
                grdVacinas.DataSource = null;
                grdVacinas.DataBind();

                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdCampSaude.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelectCamp");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID == atual.ClientID)
                    {
                        if (chk.Checked)
                        {
                            int idCampan = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCampan")).Value);
                            CarregaDadosCampanha(idCampan);
                        }
                        else
                        {
                            hidIdCampa.Value = "";
                            VerificaGridMarcadaCarregaEndereco();
                        }
                    }
                    else
                        chk.Checked = false;
                }
            }
        }

        protected void imgPesqAgendamentos_OnClick(object sender, EventArgs e)
        {
            grdHistoricoAgenda.DataSource = null;
            grdHistoricoAgenda.DataBind();
            CarregaAgendamentos();
        }

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            foreach (GridViewRow li in grdPacientes.Rows)
            {
                chk = (CheckBox)li.FindControl("chkSelectPaciente");

                if (chk.ClientID == atual.ClientID)
                {
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidIdAgenda")).Value);
                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);

                    if (chk.Checked)
                    {
                        hidCoAlu.Value = tbs174.CO_ALU.ToString();

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
                            }
                        }
                    }
                    else
                        hidIdAgenda.Value = hidCoAlu.Value = "";
                }
                else
                    chk.Checked = false;
            }
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
                        }
                    }
                }
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
                    }
                    else
                        hidIdAgenda.Value = "";
                }
                else
                    chk.Checked = false;
            }
        }

        //protected void imgCpfResp_OnClick(object sender, EventArgs e)
        //{
        //    PesquisaPaciente();
        //}

        //protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    if (((CheckBox)sender).Checked == true)
        //    {
        //        txtNirePaci.Enabled = true;
        //        chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
        //        txtCPFPaci.Text = "";

        //        chkPesqCartSaude.Checked = txtCartSaudePesq.Enabled = false;
        //        txtCartSaudePesq.Text = "";
        //    }
        //    else
        //    {
        //        txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";
        //    }
        //}

        //protected void chkPesqCartSaude_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    if (((CheckBox)sender).Checked == true)
        //    {
        //        txtCartSaudePesq.Enabled = true;
        //        chkPesqNire.Checked = txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";
        //        chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
        //        txtCPFPaci.Text = "";
        //    }
        //    else
        //    {
        //        txtCartSaudePesq.Enabled = false;
        //        txtCartSaudePesq.Text = "";
        //    }
        //}

        //protected void chkPesqCpf_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    if (((CheckBox)sender).Checked == true)
        //    {
        //        txtCPFPaci.Enabled = true;
        //        chkPesqNire.Checked = txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";

        //        chkPesqCartSaude.Checked = txtCartSaudePesq.Enabled = false;
        //        txtCartSaudePesq.Text = "";
        //    }
        //    else
        //    {
        //        txtNirePaci.Enabled = false;
        //        txtNirePaci.Text = "";
        //    }
        //}

        //protected void ddlUFUsu_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    VerificacoesErros();
        //    CarregaCidades(ddlUFUsu.SelectedValue);
        //}

        //protected void ddlCidadeUsu_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    VerificacoesErros();
        //    int coCidade = (!string.IsNullOrEmpty(ddlCidadeUsu.SelectedValue) ? int.Parse(ddlCidadeUsu.SelectedValue) : 0);
        //    CarregaBairros(ddlUFUsu.SelectedValue, coCidade);
        //}

        protected void imgPesq_OnClick(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        //protected void txtDtNasc_OnTextChanged(object sender, EventArgs e)
        //{
        //    CalculaIdadeUsu();
        //}

        //protected void ddlResp_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    hidCoResp.Value = ddlResp.SelectedValue;
        //    int coResp = ddlResp.SelectedValue != "" ? int.Parse(ddlResp.SelectedValue) : 0;
        //    CarregaDadosResponsavel(coResp);
        //}

        //protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int coAlu = ddlNomeUsu.SelectedValue != "" ? int.Parse(ddlNomeUsu.SelectedValue) : 0;
        //    int idcamp = !string.IsNullOrEmpty(hidIdCampa.Value) ? int.Parse(hidIdCampa.Value) : 0;
        //    CarregaUsuarios(coAlu);
        //    hidCoAlu.Value = ddlNomeUsu.SelectedValue;

        //    //Limpa a grid de vacinas quando o usuário for alterado
        //    grdVacinas.DataSource = null;
        //    grdVacinas.DataBind();

        //    VerificaExisteAtendimento(coAlu, idcamp);
        //}

        protected void ddlUFLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificacoesErros();
            CarregaCidadesLocal();
        }

        protected void ddlCidadeLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificacoesErros();
            CarregaBairrosLocal();
        }

        //protected void chkEhUsuarCadastrado_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    ControlaLiberacaoDadosUsuario(!chkEhUsuarCadastrado.Checked, OpHabilita.usuario);

        //    //Altera o que mostra em relação à onde o Atendimento está sendo atendiddo
        //    if (chkEhUsuarCadastrado.Checked)
        //    {
        //        liUsuario.Visible = true;
        //        liNomeUsuario.Visible = false;
        //        liApenasCadNovo.Visible = false;
        //        LimpaCamposDados(OpHabilita.usuario);
        //    }
        //    else
        //    {
        //        liUsuario.Visible = false;
        //        liNomeUsuario.Visible = true;
        //        liApenasCadNovo.Visible = true;
        //        ddlNomeUsu.SelectedValue = hidCoAlu.Value = "";
        //        LimpaCamposDados(OpHabilita.usuario);
        //    }
        //}

        //protected void chkEhRespCadastrado_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    ControlaLiberacaoDadosUsuario(!chkEhRespCadastrado.Checked, OpHabilita.responsavel);

        //    //Altera o que mostra em relação à onde o Atendimento está sendo atendiddo
        //    if (chkEhRespCadastrado.Checked)
        //    {
        //        liRespSelec.Visible = true;
        //        liNomeResp.Visible = false;
        //        LimpaCamposDados(OpHabilita.responsavel);
        //    }
        //    else
        //    {
        //        liRespSelec.Visible = false;
        //        liNomeResp.Visible = true;
        //        ddlResp.SelectedValue = hidCoResp.Value = "";
        //        LimpaCamposDados(OpHabilita.responsavel);
        //    }
        //}

        protected void lnkExc_OnClick(object sender, EventArgs e)
        {
            DataTable dtV = new DataTable();
            dtV = (DataTable)Session["grdVacinasAdm"];

            LinkButton atual = (LinkButton)sender;
            LinkButton img;

            if (grdVacinas.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdVacinas.Rows)
                {
                    img = (LinkButton)linha.Cells[3].FindControl("lnkExc");
                    string idvacina = ((HiddenField)linha.Cells[3].FindControl("hidIdVacina")).Value;

                    //Verifica se a imagem é a mesma que iniciou o postback
                    if (img.ClientID == atual.ClientID)
                    {
                        //Percorre todos os itens no datarow da session
                        for (int i = dtV.Rows.Count - 1; i >= 0; i--)
                        {
                            //Exclui a linha de registro correspondente
                            DataRow dr = dtV.Rows[i];
                            if (dr["ID_VACINA"].ToString() == idvacina)
                                dtV.Rows.Remove(dr);
                        }
                    }
                }
            }

            grdVacinas.DataSource = dtV;
            grdVacinas.DataBind();
            HttpContext.Current.Session.Add("grdVacinasAdm", dtV);
        }

        protected void lnkIncVacina_OnClick(object sender, EventArgs e)
        {
            int idVacina = !string.IsNullOrEmpty(ddlVacina.SelectedValue) ? int.Parse(ddlVacina.SelectedValue) : 0;
            int idCamp = !string.IsNullOrEmpty(hidIdCampa.Value) ? int.Parse(hidIdCampa.Value) : 0;
            int coAlu = !string.IsNullOrEmpty(hidCoAlu.Value) ? int.Parse(hidCoAlu.Value) : 0;//!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? int.Parse(ddlNomeUsu.SelectedValue) : 0;
            VerificaVacinasCampanhaUsuario(idVacina, idCamp, coAlu);
        }

        //protected void chkUsuarioEhResponsavel_OnCheckedChanged(object sender, EventArgs e)
        //{
            //if (chkUsuarioEhResponsavel.Checked)
            //{
            //    txtNomeResp.Text = txtNomePaciente.Text;
            //    ddlSexResp.SelectedValue = ddlSexoUsu.SelectedValue;
            //    txtDtNascResp.Text = txtDtNascUsu.Text;
            //    txtCPFResp.Text = txtCPFUsuaInfo.Text;

            //    liRespSelec.Visible = false;
            //    liNomeResp.Visible = true;

            //    chkEhRespCadastrado.Checked = false;
            //}
            //else
            //{
            //    txtNomeResp.Text = "";
            //    ddlSexResp.SelectedValue = "";
            //    txtDtNascResp.Text = "";
            //    txtCPFResp.Text = "";

            //    liRespSelec.Visible = true;
            //    liNomeResp.Visible = false;
            //    chkEhRespCadastrado.Checked = true;
            //}
        //}

        protected void lnkImpGuiaExame_OnClick(object sender, EventArgs e)
        {
            if (ExecutaRotinasPersistencia())
            {
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro de atendimento de Campanha de Saúde Realizado.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }

        #endregion
    }
}