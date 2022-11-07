//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//----------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//----------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR          | DESCRIÇÃO RESUMIDA
// -----------+-------------------------------+-------------------------------------
// 05/07/2016 | Tayguara Acioli  TA.05/07/2016| Adicionei a pop up de registro de ocorrências, que fica na master PadraoCadastros.Master.

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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento._8251_EncamExames
{
    public partial class Cadastro : System.Web.UI.Page
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

                txtHrCancelamento.Text = dtIni.ToShortTimeString();
                IniPeri.Text = txtDtIniAgendaExame.Text = txtDtCancelamento.Text = dtIni.ToString();
                FimPeri.Text = txtDtFimAgendaExame.Text = dtFim.ToString();
                carregaLocal();
                ddlLocal.SelectedValue = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL).CO_DEPTO.ToString();
                CarregaAgendamentoExames();
                CarregaConsultasAgendadas();

                //------------> Tamanho da imagem de Paciente na Modal
                updImagePacienteMODAL.ImagemLargura = 70;
                updImagePacienteMODAL.ImagemAltura = 85;

                AuxiliCarregamentos.CarregaUFs(ddlUfMODAL, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFRespMODAL, false, LoginAuxili.CO_EMP);

                carregaCidade(ddlCidadeMODAL, ddlUfMODAL);
                carregaBairro(ddlUfMODAL, ddlCidadeMODAL, ddlBairroMODAL);

            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
        }

        #endregion

        #region Métodos

        #region Métodos da Aba de Registro de Solicitações

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
                        case "T": //TA.07/06/2016
                            return "Triagem";
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
                        //Trata quando é de Triagem //TA.07/06/2016
                        case "T":
                            s = (this.FL_AGEND_ENCAM == "S" ? "Encaminhado para a Triagem" : "Remoção de encaminhamento para Triagem");
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
                        default:
                            s = "/Library/IMG/PGS_SF_AgendaEmAberto.png";
                            break;
                    }
                    return s;
                }
            }
            public string OBS { get; set; }
        }

        private void carregaLocal()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_RECEP.Equals("S")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(x => x.NO_DEPTO);

            ddlLocal.DataTextField = "NO_DEPTO";
            ddlLocal.DataValueField = "CO_DEPTO";
            ddlLocal.DataSource = res;
            ddlLocal.DataBind();

            ddlLocal.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Carrega a Grid de Registros de Consultas em aberto para o dia
        /// </summary>
        private void CarregaConsultasAgendadas()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);
            var nomePac = txtNomePacPesqAgendAtend.Text.Trim().ToUpper();
            var nomeProf = txtNomeProfPesqAtend.Text;
            int local = ddlLocal.SelectedValue.Equals("") ? -1 : int.Parse(ddlLocal.SelectedValue);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                       where (tbs174.CO_EMP == LoginAuxili.CO_EMP)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" /*&& tbs174.FL_JUSTI_CANCE != "C" && tbs174.FL_JUSTI_CANCE != "P"*/ : "" == "")
                       && (!String.IsNullOrEmpty(nomeProf) ? tb03.NO_COL.Contains(nomeProf) : 0 == 0)
                       && (!string.IsNullOrEmpty(nomePac) ? tb07.NO_ALU.ToUpper().Contains(nomePac) : 0 == 0)
                       && ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (tb07.CO_SITU_ALU == "A")
                       && (tb03.CO_SITU_COL == "ATI")
                       && (local == -1 ? 0 == 0 : tbs174.ID_DEPTO_LOCAL_RECEP == local)
                       select new Consultas
                       {
                           NO_RESP = tb07.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                           NO_PAC_RECEB = (!string.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU),
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
                           NO_CLASS_PROFI = tbs174.TP_CONSU != "V" ? tb03.DE_FUNC_COL : "VACINA",
                           TELEFONE_PROFI = tb03.NU_TELE_CELU_COL,
                           FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                           faltaJustif = tbs174.FL_JUSTI_CANCE,
                           LOCAL = String.IsNullOrEmpty(tb14.CO_SIGLA_DEPTO) ? "-" : tb14.CO_SIGLA_DEPTO,

                           Cortesia = tbs174.FL_CORTESIA,
                           Contratacao = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : "",
                           ContratParticular = tbs174.TB250_OPERA != null ? (tbs174.TB250_OPERA.FL_INSTI_OPERA != null && tbs174.TB250_OPERA.FL_INSTI_OPERA == "S") : false,

                           flPendFinanc = !String.IsNullOrEmpty(tb07.FL_PENDE_FINAN_GER) ? tb07.FL_PENDE_FINAN_GER == "S" : false,
                           FaltasConsec = !String.IsNullOrEmpty(tb07.FL_FALTOSO) ? tb07.FL_FALTOSO == "S" : false
                       }).OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).ThenBy(x => x.NO_PAC_RECEB).ToList();

            grdAgendamentos.DataSource = res;
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
            public string LOCAL { get; set; }
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

            public string imagem_URL
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarUrlImagemAgend(this.CO_SITU, this.FL_AGEND_ENCAM, this.FL_CONF, faltaJustif);
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

            public bool flPendFinanc { get; set; }
            public bool FaltasConsec { get; set; }
            //{
            //    get
            //    {
            //        var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //                   where tbs174.CO_ALU == CO_ALU
            //                   && tbs174.DT_AGEND_HORAR <= dt_Consul
            //                   select tbs174).OrderByDescending(a => new { a.DT_AGEND_HORAR, a.HR_AGEND_HORAR }).ToList();

            //        if (res != null && res.Count > 2)//Se não tivar mais que 2 agendas passadas não pode ter 3 faltas
            //        {
            //            var numFaltas = 0;
            //            var numAgends = 0;

            //            foreach (var i in res)
            //            {
            //                if ((i.DT_AGEND_HORAR == dt_Consul && TimeSpan.Parse(i.HR_AGEND_HORAR) < TimeSpan.Parse(hr_Consul)) || i.DT_AGEND_HORAR < dt_Consul)
            //                {
            //                    numAgends++;

            //                    if (i.CO_SITUA_AGEND_HORAR == "C")
            //                        numFaltas++;
            //                }

            //                if (numAgends == 3)
            //                    break;
            //            }

            //            if (numFaltas == 3)
            //                return true;
            //        }

            //        return false;
            //    }
            //}

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
        /// Carrega os agendamentos de avaliações
        /// </summary>
        private void CarregaAgendamentoExames()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniAgendaExame.Text) ? DateTime.Parse(txtDtIniAgendaExame.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimAgendaExame.Text) ? DateTime.Parse(txtDtFimAgendaExame.Text) : DateTime.Now);
            string noPac = txtNomePacPesqExames.Text.Trim();

            var res = (from tbs411 in TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs411.CO_ALU equals tb07.CO_ALU
                       where (!string.IsNullOrEmpty(noPac) ? tb07.NO_ALU.ToUpper().Contains(noPac) : 0 == 0)
                       && (EntityFunctions.TruncateTime(tbs411.DT_CADAS) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs411.DT_CADAS) <= EntityFunctions.TruncateTime(dtFim))
                       select new Exames
                       {
                           ID_EXAME = tbs411.ID_EXAME,
                           CO_ALU = tbs411.CO_ALU,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           dt_Consul = tbs411.DT_CADAS,
                           CO_SITU = tbs411.CO_SITUA,
                           FL_CONF = tbs411.FL_CONFIR,
                           FL_JUST = tbs411.FL_JUSTI_CANCE,

                           GRUPO = tbs411.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                           SUBGRUPO = tbs411.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                           PROCEDIMENTO = tbs411.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,

                           Cortesia = tbs411.FL_CORTESIA,
                           Contratacao = tbs411.TB250_OPERA.NM_SIGLA_OPER,
                           ContratParticular = (tbs411.TB250_OPERA.FL_INSTI_OPERA != null && tbs411.TB250_OPERA.FL_INSTI_OPERA == "S")
                       }).OrderBy(c => c.dt_Consul).ToList();

            grdAgendaExames.DataSource = res;
            grdAgendaExames.DataBind();
        }

        public class Exames
        {
            public int ID_EXAME { get; set; }

            public int CO_ALU { get; set; }
            public DateTime dt_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy - HH:mm");
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
            public string GRUPO { get; set; }
            public string SUBGRUPO { get; set; }
            public string PROCEDIMENTO { get; set; }

            public string FL_CONF { get; set; }
            public string CO_SITU { get; set; }
            public string FL_JUST { get; set; }

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
                                && tbs174.CO_SITUA_AGEND_HORAR == "A"
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

        private void CarregarPacientesComparecimento(DropDownList drp, DateTime dt)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.CO_EMP == LoginAuxili.CO_EMP && tbs174.DT_AGEND_HORAR == dt
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            var res_ = (from tbs411 in TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs411.CO_ALU equals tb07.CO_ALU
                        where EntityFunctions.TruncateTime(tbs411.DT_CADAS) == dt
                        select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res_ != null && res_.Count > 0 && res != null)
                foreach (var i in res_)
                    res.Add(i);

            if (res != null && res.Count > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res;
                drp.DataBind();
            }
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
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavelMODAL()
        {
            if (chkPaciEhRespMODAL.Checked)
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

                PesquisaCarregaRespMODAL((int?)null, txtCPFRespMODAL.Text);
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

        public enum EObjetoLogAgenda
        {
            paraAtendimento,
            paraExames
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
            else if (etipo == EObjetoLogAgenda.paraExames) //Se for para avaliação //TA.06/07/2016
            {
                #region Para Avaliação

                if (CO_TIPO_ALTERACAO == "P" || CO_TIPO_ALTERACAO == "E")
                {
                    var tbs411 = TBS411_EXAMES_ESTERNOS.RetornaPelaChavePrimaria(idAgenda);
                    tbs411.FL_CONFIR = (flagSim ? "S" : "N");
                    tbs411.CO_SITUA = flagSim && CO_TIPO_ALTERACAO == "E" ? "E" : "A";
                    tbs411.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs411.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs411.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs411.IP_SITUA = Request.UserHostAddress;
                    tbs411.DT_SITUA = DateTime.Now;

                    TBS411_EXAMES_ESTERNOS.SaveOrUpdate(tbs411, true);
                }

                #endregion
            }
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
                        string caminho = img.ImageUrl;
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidIdAgend")).Value);
                        CarregaGridLog(idAgenda); //Carrega o log do item clicado

                        var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   select new { tb07.NO_ALU, tb07.CO_SEXO_ALU }).FirstOrDefault();

                        //Atribui as informações da linha clicada aos campos correspondentes na modal
                        txtNomePaciMODLOG.Text = res.NO_ALU;
                        txtSexoMODLOG.Text = res.CO_SEXO_ALU;

                        AbreModalPadrao("AbreModalLog();");
                    }
                }
            }
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

                    if (t.Days <= 9 || (!String.IsNullOrEmpty(txtNomePacPesqAgendAtend.Text) || !String.IsNullOrEmpty(txtNomeProfPesqAtend.Text)))
                        CarregaConsultasAgendadas();
                    else
                        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período não pode ser maior do que 10 dias!");
                }
            }
        }

        protected void imgPesqAgendaExame_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDtIniAgendaExame.Text) && !String.IsNullOrEmpty(txtDtFimAgendaExame.Text))
            {
                var inicio = DateTime.Parse(txtDtIniAgendaExame.Text);
                var fim = DateTime.Parse(txtDtFimAgendaExame.Text);

                if (inicio > fim)
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período final não pode ser menor que o inicial!");
                else
                {
                    var t = fim - inicio;

                    if (t.Days <= 14 || !String.IsNullOrEmpty(txtNomePacPesqExames.Text))
                        CarregaAgendamentoExames();
                    else
                        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O período não pode ser maior do que 15 dias!");
                }
            }
        }

        protected void ddlUfMODAL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade(ddlCidadeMODAL, ddlUfMODAL);
            ddlCidadeMODAL.Focus();

            AbreModalInfosCadastrais();
        }

        protected void ddlCidadeMODAL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro(ddlUfMODAL, ddlCidadeMODAL, ddlBairroMODAL);
            ddlBairroMODAL.Focus();

            AbreModalInfosCadastrais();
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

        protected void chkPaciEhRespMODAL_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPaciEhRespMODAL.Checked)
                chkPaciMoraCoRespMODAL.Checked = true;

            carregaPaciehoResponsavelMODAL();
            AbreModalInfosCadastrais();
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

                            ScriptManager.RegisterStartupScript(
                                this.Page,
                                this.GetType(),
                                "Acao",
                                (res.FL_PERM_ATEND_TRIAGEM == "S" ? "AbreProximoPasso();" : "AbreModal();"),
                                true
                            );

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

        protected void imgPresenteAA_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendaExames.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaExames.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgPresenteAA");
                    if (img.ClientID == atual.ClientID)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidAgendaExame")).Value);

                        SalvaLogAlteracaoStatusAgenda(idAgenda, "P", (img.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico"), EObjetoLogAgenda.paraExames);

                        var tbs411 = TBS411_EXAMES_ESTERNOS.RetornaPelaChavePrimaria(idAgenda);

                        if (tbs411 != null && img.ImageUrl == "/Library/IMG/PGS_PacienteNaoChegou.ico")
                        {
                            hidAgendSelec.Value = idAgenda.ToString();

                            AbreModalPadrao("AbreModalEncamAtend();");
                        }
                    }
                }
            }

            CarregaAgendamentoExames();
        }

        protected void imgEncamAA_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendaExames.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaExames.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgEncamAA");
                    if (img.ClientID == atual.ClientID)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidAgendaExame")).Value);

                        SalvaLogAlteracaoStatusAgenda(idAgenda, "E", (img.ImageUrl == "/Library/IMG/PGS_IC_EncaminharOut.png"), EObjetoLogAgenda.paraExames);
                    }
                }
            }

            CarregaAgendamentoExames();
        }

        protected void imgCancelarAA_OnClick(object sender, EventArgs e)
        {
            divCance.Visible = divDesCance.Visible = false;
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdAgendaExames.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaExames.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgCancelarAA");

                    if (img.ClientID == atual.ClientID)
                    {
                        int idAgenda = int.Parse(((HiddenField)linha.FindControl("hidAgendaExame")).Value);
                        hidIdAgendaCancel.Value = idAgenda.ToString();
                        hidTipoAgenda.Value = "EX";

                        var tbs411 = TBS411_EXAMES_ESTERNOS.RetornaPelaChavePrimaria(idAgenda);

                        if (tbs411.CO_SITUA != "C")
                            divCance.Visible = true;
                        else
                            divDesCance.Visible = true;

                        AbreModalCancelamentoAgenda();
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

                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Status de Item de Agenda alterado com sucesso!");

                #endregion
            }
            else if (hidTipoAgenda.Value == "EX") //Cancelamento de agendamento para Avaliação
            {
                #region para Avaliação

                var tbs411 = TBS411_EXAMES_ESTERNOS.RetornaPelaChavePrimaria(int.Parse(hidIdAgendaCancel.Value));

                //Atualiza a imagem de cancelamento 
                foreach (GridViewRow li in grdAgendaExames.Rows)
                {
                    if ((((HiddenField)li.FindControl("hidAgendaExame")).Value) == hidIdAgendaCancel.Value)
                    {
                        //Se estiver cancelado, abre, senão, cancela
                        tbs411.CO_SITUA = (tbs411.CO_SITUA == "C" ? "A" : "C");
                        tbs411.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs411.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs411.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs411.DT_SITUA = DateTime.Now;
                        tbs411.IP_SITUA = Request.UserHostAddress;

                        //Grava as informações de cancelamento
                        #region Grava informações de situação de Cancelamento

                        //Se estiver com status de aberto grava as informações de cancelamento como NULL
                        if (tbs411.CO_SITUA == "A")
                        {
                            tbs411.FL_JUSTI_CANCE =
                            tbs411.DE_OBSER_CANCE = null;
                            tbs411.DT_CANCE = (DateTime?)null;
                            tbs411.CO_COL_CANCE =
                            tbs411.CO_EMP_CANCE = (int?)null;
                            tbs411.IP_CANCE = null;
                        }
                        else //Se estiver com status de cancelado, grava as informações de cancelamento
                        {
                            tbs411.FL_JUSTI_CANCE = rdblTiposCancelamento.SelectedValue;
                            tbs411.DE_OBSER_CANCE = (!string.IsNullOrEmpty(txtObserCancelamento.Text) ? txtObserCancelamento.Text : null);
                            tbs411.DT_CANCE = DateTime.Now;
                            tbs411.CO_COL_CANCE = LoginAuxili.CO_COL;
                            tbs411.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                            tbs411.IP_CANCE = Request.UserHostAddress;
                        }

                        #endregion

                        TBS411_EXAMES_ESTERNOS.SaveOrUpdate(tbs411, true);
                    }
                }

                CarregaAgendamentoExames();

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

        protected void grdAgendaExames_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                string flConfir = (((HiddenField)e.Row.FindControl("hidFlConfirmado")).Value);
                string coSitua = (((HiddenField)e.Row.FindControl("hidCoSitua")).Value);
                ImageButton imgCancel = (ImageButton)e.Row.FindControl("imgCancelarAA");
                ImageButton imgPresenca = (ImageButton)e.Row.FindControl("imgPresenteAA");
                ImageButton imgEncam = (ImageButton)e.Row.FindControl("imgEncamAA");

                if (coSitua == "A")
                {
                    if (flConfir == "S")
                    {
                        imgEncam.OnClientClick = "return confirm ('Confirmar o encaminhamento do paciente?');";
                        imgCancel.OnClientClick = "alert('Não é possivel cancelar após o registro da presença do paciente!'); return false;";

                        imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                    }
                    else
                    {
                        imgEncam.OnClientClick = "alert('Não é possivel encaminhar antes de registrar a presença do paciente e editar suas informações!'); return false;";

                        imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteNaoChegou.ico";
                    }
                }
                else if (coSitua == "E" || coSitua == "R")
                {
                    imgCancel.OnClientClick = "alert('Não é possivel cancelar após o encaminhamento do paciente!'); return false;";
                    imgPresenca.OnClientClick = "alert('Não é possivel desmarcar após o encaminhamento do paciente!'); return false;";

                    imgPresenca.ImageUrl = "/Library/IMG/PGS_PacienteChegou.ico";
                    imgEncam.ImageUrl = "/Library/IMG/PGS_IC_EncaminharIn.png";
                }
                else if (coSitua == "C")
                {
                    imgEncam.OnClientClick = "alert('Não é possivel encaminhar após o cancelamento do agendamento!'); return false;";
                    imgPresenca.OnClientClick = "alert('Não é possivel realizar presença após o cancelamento do agendamento!'); return false;";

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

        protected void lnkEncamSim_OnClick(object sender, EventArgs e)
        {
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

        protected void lnkEncamTriagem_OnClick(object sender, EventArgs e)
        {
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
                SalvaLogAlteracaoStatusAgenda(int.Parse(hidAgendSelec.Value), "E", true, EObjetoLogAgenda.paraExames);

            CarregaAgendamentoExames();
        }

        protected void lnkbEncaixe_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("8120_RegistroConsulMedMod2/Cadastro.aspx")
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
                                    && (tbs174.CO_SITUA_AGEND_HORAR == "C" || tbs174.CO_ALU == null)
                               select new
                               {
                                   tbs174.ID_AGEND_HORAR,
                                   tbs174.HR_AGEND_HORAR
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
                            agNvo.HR_AGEND_HORAR = drpHora.SelectedItem.Text;
                            agNvo.HR_DURACAO_AGENDA = agAtl.HR_DURACAO_AGENDA;
                        }

                        agNvo.CO_ALU = agAtl.CO_ALU;
                        agNvo.CO_COL_SITUA = LoginAuxili.CO_COL;
                        agNvo.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        agNvo.CO_EMP = agAtl.CO_EMP;
                        agNvo.CO_EMP_ALU = agAtl.CO_EMP_ALU;
                        agNvo.CO_ESPEC = agAtl.CO_ESPEC;
                        agNvo.CO_DEPT = agAtl.CO_DEPT;
                        agNvo.CO_SITUA_AGEND_HORAR = agAtl.CO_SITUA_AGEND_HORAR;
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

                        agAtl.CO_SITUA_AGEND_HORAR = "C";
                        agAtl.CO_COL_SITUA = LoginAuxili.CO_COL;
                        agAtl.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        agAtl.DT_SITUA_AGEND_HORAR = DateTime.Now;

                        agAtl.FL_JUSTI_CANCE = "M";
                        agAtl.DE_OBSER_CANCE = "Movimentação de agenda";
                        agAtl.DT_CANCE = DateTime.Now;
                        agAtl.CO_COL_CANCE = LoginAuxili.CO_COL;
                        agAtl.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                        agAtl.IP_CANCE = Request.UserHostAddress;

                        TBS174_AGEND_HORAR.SaveOrUpdate(agAtl, true);

                        #endregion

                        #region Atualizar log Agendamento antigo

                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();
                        tbs375.TBS174_AGEND_HORAR = agAtl;

                        tbs375.FL_JUSTI = "M";
                        tbs375.DE_OBSER = "Movimentação de agenda";
                        tbs375.CO_SITUA_AGEND_HORAR = "C";

                        tbs375.FL_TIPO_LOG = "M";
                        tbs375.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs375.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs375.DT_CADAS = DateTime.Now;
                        tbs375.IP_CADAS = Request.UserHostAddress;

                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375);

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

            if (grdAgendaExames.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAgendaExames.Rows)
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
            txtObsGuia.Text = "";
            txtObsGuia.Attributes.Add("MaxLength", "180");
            drpOperGuia.Items.Clear();
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(drpOperGuia, false, false, false, true, false);
            drpOperGuia.Items.Insert(0, new ListItem("PADRÃO", "0"));

            if (!String.IsNullOrEmpty(txtDtGuia.Text))
            {
                drpPacienteGuia.Items.Clear();
                //CarregarPacientesComparecimento(drpPacienteGuia, DateTime.Parse(txtDtGuia.Text));
                CarregarPacientesGuia(drpPacienteGuia);
                drpPacienteGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));
                AbreModalPadrao("AbreModalGuiaPlano();");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");

            CarregaAgendGuia();
        }

        private void CarregarPacientesGuia(DropDownList drp)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where tbs174.CO_EMP == LoginAuxili.CO_EMP
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            var res_ = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                        where tbs372.FL_TIPO_AGENDA == "C"
                        select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res_ != null && res_.Count > 0 && res != null)
                foreach (var i in res_)
                    res.Add(i);

            if (res != null && res.Count > 0)
            {
                drp.DataTextField = "NO_ALU";
                drp.DataValueField = "CO_ALU";
                drp.DataSource = res;
                drp.DataBind();
            }
        }

        private void CarregaAgendGuia()
        {
            int coAlu = int.Parse(drpPacienteGuia.SelectedValue);
            DateTime dtGuia = DateTime.Parse(txtDtGuia.Text);

            if (coAlu != 0)
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.DT_AGEND_HORAR == dtGuia)
                           where (tbs174.CO_ALU == coAlu)
                           select new AgendGuia { dtAgend = tbs174.DT_AGEND_HORAR, hrAgend = tbs174.HR_AGEND_HORAR, nuRegis = tbs174.NU_REGIS_CONSUL, idAgend = tbs174.ID_AGEND_HORAR }).ToList();

                if (res != null)
                {
                    ddlAgendGuia.DataTextField = "NO_AGEND";
                    ddlAgendGuia.DataValueField = "idAgend";
                    ddlAgendGuia.DataSource = res;
                    ddlAgendGuia.DataBind();
                }
            }

            ddlAgendGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));
        }

        public class AgendGuia
        {
            public DateTime dtAgend { get; set; }
            public string hrAgend { get; set; }
            public string nuRegis { get; set; }
            public int idAgend { get; set; }

            public string NO_AGEND
            {
                get
                {
                    return this.dtAgend.ToString("dd/MM/yyyy") + " " + this.hrAgend + "  " + this.nuRegis;
                }
            }
        }

        protected void drpPacienteGuia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgendGuia();
            AbreModalPadrao("AbreModalGuiaPlano();");
        }

        protected void txtDtGuia_OnTextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDtGuia.Text))
            {
                drpPacienteGuia.Items.Clear();
                CarregarPacientesComparecimento(drpPacienteGuia, DateTime.Parse(txtDtGuia.Text));
                drpPacienteGuia.Items.Insert(0, new ListItem("EM BRANCO", "0"));
                chkGuiaConsol.Checked = false;
                txtDtGuiaIni.Text = "";
                txtDtGuiaFim.Text = "";
                AbreModalPadrao("AbreModalGuiaPlano();");
            }
            else
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a data de comparecimento!");
        }

        protected void lnkbImprimirGuia_OnClick(object sender, EventArgs e)
        {
            int paciente = int.Parse(drpPacienteGuia.SelectedValue);
            int agend = int.Parse(ddlAgendGuia.SelectedValue);

            DateTime? dtIni = chkGuiaConsol.Checked ? !String.IsNullOrEmpty(txtDtGuiaIni.Text) ? DateTime.Parse(txtDtGuiaIni.Text) : (DateTime?)null : (DateTime?)null;
            DateTime? dtFim = chkGuiaConsol.Checked ? !String.IsNullOrEmpty(txtDtGuiaFim.Text) ? DateTime.Parse(txtDtGuiaFim.Text) : (DateTime?)null : (DateTime?)null;

            if (chkGuiaConsol.Checked && (dtIni == null || dtFim == null))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para emitir a guia com procedimentos consolidados, preencha o período.");
                txtDtGuiaIni.Focus();
                txtDtGuiaFim.Focus();
                AbreModalPadrao("AbreModalGuiaPlano();");
                return;
            }

            RptGuiaAtend rpt = new RptGuiaAtend();
            var retorno = rpt.InitReport(paciente, txtObsGuia.Text, drpOperGuia.SelectedValue, txtDtGuia.Text, LoginAuxili.CO_COL, agend, chkGuiaConsol.Checked, dtIni, dtFim);

            GerarRelatorioPadrão(rpt, retorno);
        }

        protected void lnkbRecebimento_OnClick(object sender, EventArgs e)
        {
            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("5102_Receber/Cadastro.aspx")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
                HttpContext.Current.Response.Redirect("/" + String.Format("{0}?moduloNome={1}", admModuloMatr.nomURLModulo, HttpContext.Current.Server.UrlEncode(admModuloMatr.nomModulo)));
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
            CarregaConsultasAgendadas();
        }

        #endregion
    }
}