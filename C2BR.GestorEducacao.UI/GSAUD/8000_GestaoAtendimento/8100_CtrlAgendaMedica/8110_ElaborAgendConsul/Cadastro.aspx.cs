//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: GC - Gestor Clínica
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: Elaboração de Agenda Clínica
// SUBMÓDULO: Controle Operacional de Agenda de Profissionais
// OBJETIVO: Elaboração de Agenda Clínica
// DATA DE CRIAÇÃO: 08/03/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/06/2014|Victor Martins Machado      | Criada a grid que lista os profissionais
//           |                            | e a função que carrega os mesmos nela.
//           |                            | 

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Data.Objects;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8110_ElaborAgendConsul
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //====> CRIAÇÃO DAS INSTÂNCIAS UTILIZADAS
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            //CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(CurrentPadraoCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaClassificacoes();
                CarregaUnidades();
                CarregaDepartamento();
                CarregaGridProfi();

                dtIniMod.Text = DateTime.Now.ToShortDateString();
                dtFimMod.Text = DateTime.Now.ToShortDateString();
            }
        }

        public obValidacaoAgenda ValidaConflitosAgendamentos(int coCol, DateTime tDtIni, DateTime dtFim, TimeSpan tHrIni, TimeSpan tHrFim, TimeSpan tHrInt)
        {
            string horariosConflitantes = "";
            obValidacaoAgenda obValAgenda = new obValidacaoAgenda();
            TimeSpan tsAux = tHrIni;

            //Para cada data dentro do intervalo
            while (tDtIni <= dtFim)
            {
                bool Permite = true;

                //Verifica se o dia da semana correspondente à data em questão, está marcado nos parâmetros
                int dia = (int)tDtIni.DayOfWeek;

                #region Verificação do Dia da Semana

                switch (dia)
                {
                    case 0:
                        if (!chkDom.Checked)
                            Permite = false;
                        break;
                    case 1:
                        if (!chkSeg.Checked)
                            Permite = false;
                        break;
                    case 2:
                        if (!chkTer.Checked)
                            Permite = false;
                        break;
                    case 3:
                        if (!chkQua.Checked)
                            Permite = false;
                        break;
                    case 4:
                        if (!chkQui.Checked)
                            Permite = false;
                        break;
                    case 5:
                        if (!chkSex.Checked)
                            Permite = false;
                        break;
                    case 6:
                        if (!chkSab.Checked)
                            Permite = false;
                        break;
                }

                #endregion

                if (Permite)
                {
                    #region Verificação para cada Data

                    //faz uma lista de todos os agendamentos desse colaborador nessa data
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               where tbs174.CO_COL == coCol
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) == EntityFunctions.TruncateTime(tDtIni)
                               select new { tbs174.HR_AGEND_HORAR, tbs174.HR_DURACAO_AGENDA }).ToList();

                    horariosConflitantes += "\n \n - Data: " + tDtIni.ToString("dd/MM/yyyy") + " - horários: ";
                    //Para cada horário entre o intervalo informado
                    while (tHrIni <= tHrFim)
                    {
                        //Calcula a hora final do agendamento de acordo com a inicial e o intervalo
                        TimeSpan hrFinalAgenda = tHrIni.Add(tHrInt);

                        //Apenas salva, se o horário em que essa agenda vai terminar, 
                        //for menor ou igual ao horário final estipulado na página
                        if (hrFinalAgenda <= tHrFim)
                        {
                            #region Verificação para Cada Horário

                            bool PermiteHorario = true;

                            #region Valida as informações de tempo de descanso

                            //Se tiver sido informado um horário de início para o intervalo de descanso
                            if ((!string.IsNullOrEmpty(txtIntervaloInicio.Text)) && (!string.IsNullOrEmpty(txtIntervaloFim.Text)))
                            {
                                TimeSpan hrinidescanso = TimeSpan.Parse(txtIntervaloInicio.Text);
                                TimeSpan hrfimdescanso = TimeSpan.Parse(txtIntervaloFim.Text).Add(new TimeSpan(0, -1, 0));

                                if (((tHrIni >= hrinidescanso) && (tHrIni <= hrfimdescanso)) //Se o horário de início está entre o período de descanso
                                    || (((hrFinalAgenda.Add(new TimeSpan(0, -1, 0))) >= hrinidescanso) && ((hrFinalAgenda.Add(new TimeSpan(0, -1, 0))) <= hrfimdescanso))) //Se o horário final está entre o período de descanso debitando um minuto para total dequação
                                {
                                    PermiteHorario = false;
                                }
                            }

                            #endregion

                            //Apenas se o horário for permitido e não conflitante com o intervalo informado
                            if (PermiteHorario)
                            {
                                //Para cada agenda do dia do colaborador
                                foreach (var it in res)
                                {
                                    //Coleta e prepara data e hora inicial e final do registro de agenda do colaborador
                                    TimeSpan tshi = TimeSpan.Parse((it.HR_AGEND_HORAR + ":00"));
                                    //se o valor HR_DURACAO_AGENDA for nulo, deixa o agendamento com o tempo padrão de 30 min
                                    TimeSpan tshf = !string.IsNullOrEmpty(it.HR_DURACAO_AGENDA) ? tshi.Add(TimeSpan.Parse((it.HR_DURACAO_AGENDA + ":00"))) : tshi.Add(TimeSpan.Parse(("00:30")));
                                    DateTime dtInitiRegis = tDtIni.Add(tshi);
                                    DateTime dtFinalRegis = tDtIni.Add(tshf);

                                    //Coleta e prepara data e hora inicial e final a qual está tentando ser incluída
                                    TimeSpan tsfin = tHrIni.Add(tHrInt).Add(new TimeSpan(0, -1, 0));
                                    DateTime dtInitiInclusao = tDtIni.Add(tHrIni);
                                    DateTime dtFinalInclusao = tDtIni.Add(tsfin);

                                    //Verifica se a duração do agendamento em questão, vai conflitar com a duração de outro agendamento já registrado para este colaborador e data
                                    if (((dtInitiInclusao >= dtInitiRegis) && (dtInitiInclusao <= dtFinalRegis))
                                        || ((dtFinalInclusao >= dtInitiRegis) && (dtFinalInclusao <= dtFinalRegis)))
                                    {
                                        //Se entrar aqui, conflitou com algum outro agendamento
                                        obValAgenda.comErro = true;
                                        horariosConflitantes += tHrIni.Hours.ToString().PadLeft(2, '0')
                                                                + ":"
                                                                + tHrIni.Minutes.ToString().PadLeft(2, '0')
                                                                + "; ";

                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        tHrIni = tHrIni.Add(tHrInt); // Incrementa o horário
                    }
                    #endregion
                }
                tHrIni = tsAux; // Retorna o horário original
                tDtIni = tDtIni.AddDays(1);
            }
            obValAgenda.erro = horariosConflitantes;
            return obValAgenda;
        }

        public class obValidacaoAgenda
        {
            public string erro;
            public bool comErro = false;
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            #region Validação dos campos

            DateTime dtIni;
            if (!DateTime.TryParse(txtDtIni.Text, out dtIni))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de início inválida.");
                return;
            }

            DateTime dtFim;
            if (!DateTime.TryParse(txtDtFim.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de término inválida.");
                return;
            }

            if (dtFim < dtIni)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de término não pode ser menor que a data de início.");
                return;
            }

            string hrIni = txtHrIni.Text;
            if (string.IsNullOrEmpty(hrIni))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de início não informado.");
                return;
            }

            string hrFim = txtHrFim.Text;
            if (string.IsNullOrEmpty(hrFim))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de término não informado.");
                return;
            }

            string hrInter = txtHrInterv.Text;
            if (string.IsNullOrEmpty(hrInter))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de intervalo não informado.");
                return;
            }

            //Valida se os horários são válidos
            #region Valida horários


            TimeSpan tHrIni;
            if (!TimeSpan.TryParse(hrIni, out tHrIni))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de início informado inválido.");
                return;
            }

            TimeSpan tHrFim;
            if (!TimeSpan.TryParse(hrFim, out tHrFim))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de término informado inválido.");
                return;
            }

            TimeSpan tHrInt;
            if (!TimeSpan.TryParse(hrInter, out tHrInt))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Tempo de Intervalo entre agendamentos informado inválido.");
                return;
            }

            #region Verifica data inicial

            //Verifica validade da hora inicial
            #region Horario Inicial

            int hrI = tHrIni.Hours;
            int mnI = tHrIni.Minutes;

            if (tHrFim < tHrIni)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de Fim não pode ser menor que o Horário de Início.");
                txtHrIni.Focus();
                return;
            }

            if (hrI > 24 || (hrI == 24 && mnI >= 0) || mnI >= 60 || (hrI == 0 && mnI == 0))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Horário de Início de atendimento do Profissional deve ser entre 00:01 a 23:59");
                txtHrIni.Focus();
                return;
            }

            #endregion

            //Verifica validade da hora final
            #region Horario Final

            int hrF = tHrFim.Hours;
            int mnF = tHrFim.Minutes;

            if (hrF > 24 || (hrF == 24 && mnF > 0) || mnF >= 60 || (hrF == 0 && mnF == 0))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Horário de Fim de atendimento do Profissional deve ser entre 00:01 a 24:00");
                txtHrFim.Focus();
                return;
            }

            #endregion

            //Verifica validade do intervalo
            #region Intervalo

            int hrT = tHrInt.Hours;
            int mnT = tHrInt.Minutes;

            if (hrT > 24 || (hrT == 24 && mnT > 0) || mnT >= 60 || (hrT == 0 && mnT == 0))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Tempo de atendimento deve ser entre 00:01 a 24:00");
                txtHrInterv.Focus();
                return;
            }

            #endregion

            #endregion

            #region Valida Horário de Descanso

            //Valida se informou o horário de início mas não o de término
            if ((!string.IsNullOrEmpty(txtIntervaloInicio.Text)) && (string.IsNullOrEmpty(txtIntervaloFim.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ao informar o Horário de início do descanso, é preciso também informar o horário final!");
                return;
            }

            //Valida se informou o horário de Término mas não o de início
            if ((!string.IsNullOrEmpty(txtIntervaloFim.Text)) && (string.IsNullOrEmpty(txtIntervaloInicio.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ao informar o Horário de início do descanso, é preciso também informar o horário final!");
                return;
            }

            //Valida se o horário de término do descanso é realmente após o de início
            if ((!string.IsNullOrEmpty(txtIntervaloInicio.Text)) && (!string.IsNullOrEmpty(txtIntervaloFim.Text))) // Se ambos tiverem sido informados
            {
                TimeSpan tsin = TimeSpan.Parse(txtIntervaloInicio.Text);
                TimeSpan tsfi = TimeSpan.Parse(txtIntervaloFim.Text);

                if (tsfi < tsin) //Se o horário de termino for menor que o de início
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Horário de término do descanso não pode ser anterior ao horário de início do descanso!");
                    return;
                }
            }

            #endregion

            #endregion

            int coCol = hidCoCol.Value != "" ? int.Parse(hidCoCol.Value) : 0;
            if (coCol == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione um colaborador.");
                return;
            }

            if (ddlUnid.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione uma Unidade.");
                return;
            }

            if (ddlDepto.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione um Local de Agendamento para o Profissional");
                return;
            }

            if (!chkDom.Checked && !chkSeg.Checked && !chkTer.Checked && !chkQua.Checked && !chkQui.Checked && !chkSex.Checked && !chkSab.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser assinalado pelo menos um dia da semana (Domingo a Sábado) para atendimento pelo Profissional");
                return;
            }

            int coEmp = int.Parse(ddlUnid.SelectedValue);

            int coDep = int.Parse(ddlDepto.SelectedValue);
            #endregion

            int i = 1;

            DateTime tDtIni = dtIni;

            if (!chkAgendaMulti.Checked)
            {
                obValidacaoAgenda obValida = ValidaConflitosAgendamentos(coCol, tDtIni, dtFim, tHrIni, tHrFim, tHrInt);

                if (obValida.comErro)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Agendamento com conflito - Rever ou fazer de forma individual - Conflitos: \n" + obValida.erro);
                    return;
                }
            }

            TimeSpan tsAux = tHrIni;
            while (tDtIni <= dtFim)
            {
                bool Permite = true;

                //Verifica se o dia da semana correspondente à data em questão, está marcado nos parâmetros
                int dia = (int)tDtIni.DayOfWeek;

                #region Verificação do Dia da Semana

                switch (dia)
                {
                    case 0:
                        if (!chkDom.Checked)
                            Permite = false;
                        break;
                    case 1:
                        if (!chkSeg.Checked)
                            Permite = false;
                        break;
                    case 2:
                        if (!chkTer.Checked)
                            Permite = false;
                        break;
                    case 3:
                        if (!chkQua.Checked)
                            Permite = false;
                        break;
                    case 4:
                        if (!chkQui.Checked)
                            Permite = false;
                        break;
                    case 5:
                        if (!chkSex.Checked)
                            Permite = false;
                        break;
                    case 6:
                        if (!chkSab.Checked)
                            Permite = false;
                        break;
                }

                #endregion

                if (Permite)
                {
                    while (tHrIni <= tHrFim)
                    {
                        //Executa esse bloco para cada Horário
                        #region Para cada Horário

                        //Calcula a hora final do agendamento de acordo com a inicial e o intervalo
                        TimeSpan hrFinalAgenda = tHrIni.Add(tHrInt);

                        //Apenas salva, se o horário em que essa agenda vai terminar, 
                        //for menor ou igual ao horário final estipulado na página
                        if (hrFinalAgenda <= tHrFim)
                        {
                            bool PermiteHorario = true;

                            #region Valida as informações de tempo de descanso

                            //Se tiver sido informado um horário de início para o intervalo de descanso
                            if ((!string.IsNullOrEmpty(txtIntervaloInicio.Text)) && (!string.IsNullOrEmpty(txtIntervaloFim.Text)))
                            {
                                TimeSpan hrinidescanso = TimeSpan.Parse(txtIntervaloInicio.Text);
                                TimeSpan hrfimdescanso = TimeSpan.Parse(txtIntervaloFim.Text).Add(new TimeSpan(0, -1, 0));

                                if (((tHrIni >= hrinidescanso) && (tHrIni <= hrfimdescanso)) //Se o horário de início está entre o período de descanso
                                    || (((hrFinalAgenda.Add(new TimeSpan(0, -1, 0))) >= hrinidescanso) && ((hrFinalAgenda.Add(new TimeSpan(0, -1, 0))) <= hrfimdescanso))) //Se o horário final está entre o período de descanso debitando um minuto para total dequação
                                {
                                    PermiteHorario = false;
                                }
                            }

                            #endregion

                            //Apenas se o horário for permitido e não conflitante com o intervalo informado
                            if (PermiteHorario)
                            {
                                TBS174_AGEND_HORAR agend = new TBS174_AGEND_HORAR();

                                agend.DT_AGEND_HORAR = tDtIni;
                                agend.HR_AGEND_HORAR = tHrIni.ToString("c").Substring(0, 5);

                                agend.CO_SITUA_AGEND_HORAR = "A";
                                agend.DT_SITUA_AGEND_HORAR = DateTime.Now;
                                agend.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                agend.CO_COL_SITUA = LoginAuxili.CO_COL;
                                agend.FL_AGEND_CONSU = "N";
                                agend.FL_CONF_AGEND = "N";
                                agend.FL_ENCAI_AGEND = "N";
                                agend.CO_COL = coCol;
                                agend.CO_EMP = coEmp;
                                agend.CO_DEPT = coDep;
                                agend.ID_DEPTO_LOCAL_RECEP = coDep;
                                //agend.CO_ESPEC = coEsp;
                                agend.HR_DURACAO_AGENDA = tHrInt.Add(new TimeSpan(0, -1, 0)).ToString("c").Substring(0, 5);

                                TBS174_AGEND_HORAR.SaveOrUpdate(agend, true);

                                // Salva dados da agenda em nova tabela TSN011_Agenda_Credenciado
                                var connStr = "Data Source=user-pc;Initial Catalog=BDGC_XX_MODELO;Persist Security Info=True;User ID=sa;Password=@#!CJr;MultipleActiveResultSets=True";
                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr);
                                try
                                {
                                    conn.Open();
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(@"INSERT INTO [BDGC_XX_Modelo].[dbo].[TSN011_AGENDA_CREDENCIADO]
                                   ([ID_AGEND_HORAR]
                                   ,[CO_PROFISSIONAIS]
                                   ,[CO_CREDENCIADO]
                                   ,[DT_AGENDA]
                                   ,[HR_AGENDA]
                                   ,[CO_FORMA_PAGTO]
                                   ,[CO_SITUACAO_PAGTO]
                                   ,[CO_SITUACAO_AGENDA])
                                    VALUES
                                   (@ID_AGEND_HORAR
                                   ,@CO_PROFISSIONAIS 
                                   ,@CO_CREDENCIADO
                                   ,@DT_AGENDA
                                   ,@HR_AGENDA
                                   ,@CO_FORMA_PAGTO
                                   ,@CO_SITUACAO_PAGTO
                                   ,@CO_SITUACAO_AGENDA)", conn);
                                    cmd.Parameters.AddWithValue("@ID_AGEND_HORAR", agend.ID_AGEND_HORAR);
                                    cmd.Parameters.AddWithValue("@CO_PROFISSIONAIS", agend.CO_COL);
                                    cmd.Parameters.AddWithValue("@CO_CREDENCIADO", agend.CO_EMP);
                                    cmd.Parameters.AddWithValue("@DT_AGENDA", agend.DT_AGEND_HORAR);
                                    cmd.Parameters.AddWithValue("@HR_AGENDA", agend.HR_AGEND_HORAR);
                                    cmd.Parameters.AddWithValue("@CO_FORMA_PAGTO", 5);
                                    cmd.Parameters.AddWithValue("@CO_SITUACAO_PAGTO", "N");
                                    cmd.Parameters.AddWithValue("@CO_SITUACAO_AGENDA", agend.CO_SITUA_AGEND_HORAR);
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("Error Occurred:" + ex.Message.ToString());
                                }
                                finally
                                {
                                    conn.Close();
                                }

                            }
                        }

                        //Incrementa no loop para percorrer todos os horários no intervalo
                        tHrIni = tHrIni.Add(tHrInt);

                        #endregion
                    }
                }
                tHrIni = tsAux; // Ao finalizar uma data, retorna o valor inicial do horário de início

                //Incrementa no loop para percorrer todas as datas no intervalo
                tDtIni = tDtIni.AddDays(1);
                i++;
            }
            AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento Salvo com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #region Funções de carga de dados

        /// <summary>
        /// Método que carrega as unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega os departamentos
        /// </summary>
        private void CarregaDepartamento()
        {
            int coEmp = ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0;
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       join tb174 in TB174_DEPTO_TIPO.RetornaTodosRegistros() on tb14.CO_TIPO_DEPTO equals tb174.ID_DEPTO_TIPO
                       where coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0
                       && tb174.CO_CLASS_TIPO_LOCAL.Equals("TEC")
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO, DE_DEPTO = (tb14.CO_SIGLA_DEPTO + " - " + tb14.NO_DEPTO) }).OrderBy(x => x.NO_DEPTO);

            ddlDepto.DataTextField = "DE_DEPTO";
            ddlDepto.DataValueField = "CO_DEPTO";
            ddlDepto.DataSource = res;
            ddlDepto.DataBind();
            ddlDepto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega os profissionais na grid de profissionais
        /// </summary>
        private void CarregaGridProfi()
        {
            int coEmp = ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0;
            int coCol = !string.IsNullOrEmpty(drpProSolicitado.SelectedValue) ? int.Parse(drpProSolicitado.SelectedValue) : 0;
            string clasFunc = ddlClassFunc.SelectedValue;

            //Coleta quais são as classificações para as quais é possível efetuar direcionamentos parametrizadas na unidade
            var dadosEmp = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                            join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.CO_EMP equals tb83.CO_EMP
                            where tb25.CO_EMP == LoginAuxili.CO_EMP
                            select new
                            {
                                tb83.FL_PERM_AGEND_ENFER,
                                tb83.FL_PERM_AGEND_FISIO,
                                tb83.FL_PERM_AGEND_FONOA,
                                tb83.FL_PERM_AGEND_MEDIC,
                                tb83.FL_PERM_AGEND_ODONT,
                                tb83.FL_PERM_AGEND_OUTRO,
                                tb83.FL_PERM_AGEND_PSICO,
                                tb83.FL_PERM_AGEND_ESTET,
                                tb83.FL_PERM_AGEND_NUTRI,
                                tb83.FL_PERM_AGEND_TERAP_OCUPA
                            }).FirstOrDefault();

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                       where tb03.FLA_PROFESSOR == "S"
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                       && (coCol > 0 ? tb03.CO_COL == coCol : 0 == 0)
                       && (tb03.CO_CLASS_PROFI != null)
                       && (!tb03.CO_SITU_COL.Equals("DEM"))
                       && (!tb03.CO_SITU_COL.Equals("FES"))
                       && (!tb03.CO_SITU_COL.Equals("INA"))
                       && (clasFunc != "0" ? tb03.CO_CLASS_PROFI == clasFunc : 0 == 0)
                       select new GrdProfiSaida
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL_RECEB = tb03.NO_COL,
                           MATR_COL = tb03.CO_MAT_COL,
                           NO_EMP = tb25.sigla,
                           DE_ESP = tb63.NO_ESPECIALIDADE,
                           CO_CLASS = tb03.CO_CLASS_PROFI,
                           SIGLA_DEPTO = tb14.CO_SIGLA_DEPTO,
                           CO_SITU = tb03.CO_SITU_COL,
                           SITU = (tb03.CO_SITU_COL.Equals("ATI") ? "Ativo" : tb03.CO_SITU_COL.Equals("FCE") ? "Cedido" : tb03.CO_SITU_COL.Equals("FER") ? "Férias" :
                                   tb03.CO_SITU_COL.Equals("LFR") ? "Licença Funcional" : tb03.CO_SITU_COL.Equals("LMA") ? "Licença Maternidade" : tb03.CO_SITU_COL.Equals("LME") ? "Licença Médica" :
                                   tb03.CO_SITU_COL.Equals("SUS") ? "Suspenso" : tb03.CO_SITU_COL.Equals("TRE") ? "Treinamento" : "-"),
                           //TEL = tb03.NU_TELE_CELU_COL,
                           TEL = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                       }).OrderBy(w => w.NO_COL_RECEB).ToList();

            var lst = new List<GrdProfiSaida>();

            if (dadosEmp != null)
            {
                #region Verifica os itens a serem excluídos
                if (res.Count > 0)
                {
                    int aux = 0;
                    foreach (var i in res)
                    {
                        switch (i.CO_CLASS)
                        {
                            case "M":
                                if (dadosEmp.FL_PERM_AGEND_MEDIC != "S")
                                { lst.Add(i); }
                                break;
                            case "E":
                                if (dadosEmp.FL_PERM_AGEND_ENFER != "S")
                                { lst.Add(i); }
                                break;
                            case "I":
                                if (dadosEmp.FL_PERM_AGEND_FISIO != "S")
                                { lst.Add(i); }
                                break;
                            case "F":
                                if (dadosEmp.FL_PERM_AGEND_FONOA != "S")
                                { lst.Add(i); }
                                break;
                            case "D":
                                if (dadosEmp.FL_PERM_AGEND_ODONT != "S")
                                { lst.Add(i); }
                                break;
                            case "S":
                                if (dadosEmp.FL_PERM_AGEND_ESTET != "S")
                                { lst.Add(i); }
                                break;
                            case "N":
                                if (dadosEmp.FL_PERM_AGEND_NUTRI != "S")
                                { lst.Add(i); }
                                break;
                            case "O":
                                if (dadosEmp.FL_PERM_AGEND_OUTRO != "S")
                                { lst.Add(i); }
                                break;
                            case "P":
                                if (dadosEmp.FL_PERM_AGEND_PSICO != "S")
                                { lst.Add(i); }
                                break;
                            case "T":
                                if (dadosEmp.FL_PERM_AGEND_TERAP_OCUPA != "S")
                                { lst.Add(i); }
                                break;
                            default:
                                break;

                        }
                        aux++;
                    }
                }
                #endregion
            }

            res = res.Except(lst).ToList();

            grdProfi.DataSource = res;
            grdProfi.DataBind();

            foreach (GridViewRow l in grdProfi.Rows)
            {
                CheckBox chkL = ((CheckBox)l.FindControl("ckSelect"));
                HiddenField hidSitu = ((HiddenField)l.FindControl("hidSitu"));

                chkL.Enabled = hidSitu.Value.Equals("ATI") ? true : false;

            }
        }

        /// <summary>
        /// Carrega as classificações
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFunc, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
            // AuxiliCarregamentos.CarregaTiposAgendamento(ddlClassFunc, true, false, false, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        /// <summary>
        /// Classe que formata as informações apresentadas na grid de profissionais
        /// </summary>
        public class GrdProfiSaida
        {
            public string TEL { get; set; }
            public int CO_COL { get; set; }
            public string SIGLA_DEPTO { get; set; }
            public string MATR_COL { get; set; }
            public string CO_SITU { get; set; }
            public string SITU { get; set; }
            public string NO_COL
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0').Insert(2, ".").Insert(6, "-");
                    string noCol = (this.NO_COL_RECEB.Length > 70 ? this.NO_COL_RECEB.Substring(0, 70) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_COL_RECEB { get; set; }
            public string NO_EMP { get; set; }
            public string DE_ESP { get; set; }
            public string CO_CLASS { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS);
                }
            }
        }

        #endregion

        #region Funções de Campo

        /// <summary>
        /// Método executado quando a unidade é selecionada
        /// </summary>
        /// <param name="sender">Recebe o objeto que executou o método (ddlUnid)</param>
        /// <param name="e"></param>
        protected void ddlUnid_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento();
            CarregaGridProfi();
        }

        /// <summary>
        /// Método execitado quando um colaborador é selecionado na grid de profissionais
        /// </summary>
        /// <param name="sender">Objeto que acionou o método (CheckBox da grid de profissionais)</param>
        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow l in grdProfi.Rows)
            {
                CheckBox chkL = ((CheckBox)l.Cells[0].FindControl("ckSelect"));

                if (chk.ClientID == chkL.ClientID)
                {
                    HiddenField hidCol = ((HiddenField)l.Cells[0].FindControl("hidCol"));

                    hidCoCol.Value = hidCol.Value;
                }
                else
                    chkL.Checked = false;
            }
        }

        protected void ddlClassFunc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
        }

        protected void imgInfosAgend_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdProfi.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProfi.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgAgend");

                    if (img.ClientID == atual.ClientID)
                    {
                        int coCol = int.Parse(((HiddenField)linha.FindControl("hidCol")).Value);

                        hidCoCol.Value = coCol.ToString();
                        hidCoColMod.Value = coCol.ToString();
                        CarregaGridAgendMod(coCol);
                        AbreModalInfosAgend();
                    }
                }
            }
        }

        protected void imgImprimirAgend_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdProfi.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProfi.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgImprimirAgend");

                    if (img.ClientID == atual.ClientID)
                    {
                        lblProfImp.Text = "";
                        hidCoColImp.Value = "";
                        int coCol = int.Parse(((HiddenField)linha.FindControl("hidCol")).Value);
                        hidCoColImp.Value = ((HiddenField)linha.FindControl("hidCol")).Value;
                        var tb03 = TB03_COLABOR.RetornaPeloCoCol(coCol);
                        lblProfImp.Text = tb03.NO_COL + " | " + tb03.CO_SIGLA_ENTID_PROFI + " - " + tb03.NU_ENTID_PROFI;
                        AbreModalImprimirAgend();
                    }
                }
            }
        }

        protected void ImprimirAgend_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDtIniImp.Text) || string.IsNullOrEmpty(txtDtFimImp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "As datas iniciais e/ou finais são ogrigatórias.");
                    txtDtIniImp.Focus();
                    AbreModalImprimirAgend();
                }

                DateTime dtIni = DateTime.Parse(txtDtIniImp.Text);
                DateTime dtFim = DateTime.Parse(txtDtFimImp.Text);
                
                if (dtFim < dtIni)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data de término não pode ser menor que a data de início.");
                    AbreModalImprimirAgend();
                    return;
                }

                int coCol = int.Parse(hidCoColImp.Value);
                RptGuiaAgendProf rpt = new RptGuiaAgendProf();
                string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                var retorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, coCol, dtIni, dtFim);
                GerarRelatorioPadrão(rpt, retorno);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        protected void imgPesqAgendMod_OnClick(object sender, EventArgs e)
        {
            int coCol = int.Parse(hidCoColMod.Value);
            CarregaGridAgendMod(coCol);
            AbreModalInfosAgend();
        }

        protected void imgPesProfSolicitado_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaProfSolicitado(true);

            string nome = txtProSolicitado.Text;

            var res = TB03_COLABOR.RetornaTodosRegistros()
                        .Where(x => x.NO_COL.Contains(nome) && x.FLA_PROFESSOR.Equals("S"))
                        .Select(x => new
                            {
                                NomeCol = (!string.IsNullOrEmpty(x.DE_FUNC_COL) ? x.DE_FUNC_COL : "S/R") + " - " + x.NO_APEL_COL,
                                coCol = x.CO_COL
                            })
                         .OrderBy(x => x.NomeCol);

            drpProSolicitado.DataSource = res;
            drpProSolicitado.DataTextField = "NomeCol";
            drpProSolicitado.DataValueField = "coCol";
            drpProSolicitado.DataBind();

            drpProSolicitado.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void drpProSolicitado_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarPesquisaProfSolicitado(false);
            CarregaGridProfi();
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
        }

        /// <summary>
        /// Abre a modal com informações de agendamento
        /// </summary>
        private void AbreModalInfosAgend()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosAgend();",
                true
            );
        }

        private void AbreModalImprimirAgend()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalImprimirAgend();",
                true
            );
        }

        /// <summary>
        /// Método que carrega os agendamentos da modal
        /// </summary>
        private void CarregaGridAgendMod(int coCol)
        {
            var profi = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                         where tb03.CO_COL == coCol
                         select new
                         {
                             NO_PROFI = tb03.NO_COL,
                             CRM_PROFI = tb03.CO_SIGLA_ENTID_PROFI,
                             NR_CRM_PROFI = tb03.NU_ENTID_PROFI,
                             DE_PROFI = (String.IsNullOrEmpty(tb03.CO_SIGLA_ENTID_PROFI) || String.IsNullOrEmpty(tb03.NU_ENTID_PROFI) ?
                             tb03.NO_COL : (tb03.NO_COL + " | " + tb03.CO_SIGLA_ENTID_PROFI + " - " + tb03.NU_ENTID_PROFI))
                         }).FirstOrDefault();
            noProfiMod.Text = profi.DE_PROFI;

            //int coCol = int.Parse(hidCoCol.Value);
            DateTime dtIni = (String.IsNullOrEmpty(dtIniMod.Text) ? DateTime.MinValue : DateTime.Parse(dtIniMod.Text));
            DateTime dtFim = (String.IsNullOrEmpty(dtFimMod.Text) ? DateTime.MinValue : DateTime.Parse(dtFimMod.Text));
            
            if (dtFim < dtIni)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de término não pode ser menor que a data de início.");
                AbreModalInfosAgend();
                return;
            }

            var departamentos = TB14_DEPTO.RetornaTodosRegistros();
            var lstGrdModal = new List<GrdModal>();

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       //join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       //join tbs460 in TBS460_AGEND_AVALI_PROFI.RetornaTodosRegistros() on tb03.CO_COL equals tbs460.CO_COL_AVALI
                       //join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                       where ((tbs174.CO_COL == coCol || tbs174.FL_CONSU_AVALIA == "S" && tbs174.CO_COL == coCol)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni) 
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                       select new GrdModal
                      {
                          ID_DEPTO = tbs174.CO_DEPT,
                          DT_AGEND = tbs174.DT_AGEND_HORAR,
                          CO_PACI = tbs174.CO_ALU,
                          HR_AGEND = tbs174.HR_AGEND_HORAR,
                          SITU = tbs174.CO_SITUA_AGEND_HORAR,
                          LOCAL = string.Empty,
                          FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                          FL_CONF = tbs174.FL_CONF_AGEND,
                          FL_SITUA_TRIAGEM = tbs174.FL_SITUA_TRIAGEM,
                          FL_JUSTI_CANCE = tbs174.FL_JUSTI_CANCE,
                          FL_CONSU_AVALIA = tbs174.FL_CONSU_AVALIA
                         
                      }).OrderBy(w => w.DT_AGEND).ThenBy(w => w.HR_AGEND).ToList();

            foreach (var item in res)
            {
                item.TIPO = "AV";

                if (item.FL_CONSU_AVALIA != "S")
                {
                    item.TIPO = RecuperaSituacaoAgenda(item.SITU, item.FL_CONF, item.FL_AGEND_ENCAM, item.FL_JUSTI_CANCE);
                }

                item.LOCAL = departamentos.Any(p => p.CO_DEPTO == item.ID_DEPTO) ? departamentos.FirstOrDefault(p => p.CO_DEPTO == item.ID_DEPTO).NO_DEPTO.ToString() : "-";

            }


            lstGrdModal = res.DistinctBy(p => p.HR_AGEND + p.DT_AGEND.ToShortDateString()).ToList();

            if (lstGrdModal.Count > 0)
            {
                foreach (var a in lstGrdModal)
                {
                 
                    var groupInfo = res.Where(p => p.HR_AGEND + p.DT_AGEND.ToShortDateString() == a.HR_AGEND + a.DT_AGEND.ToShortDateString());
                    var nomes = groupInfo.Select(n => n.NO_PACI);
                    var tipos = groupInfo.Select(t => t.TIPO);
                    var local = groupInfo.Select(l => l.LOCAL);
                    var pasta = groupInfo.Select(p => p.PASTA);

                    a.NOME = string.Join("<br />", nomes.ToArray());
                    a.TIPO = string.Join("<br />", tipos.ToArray());
                    a.LOCAL = string.Join("<br />", local.ToArray());
                    a.DESC_PASTA = string.Join("<br />", pasta.ToArray());

                }

                grdAgendMod.DataSource = lstGrdModal;
                grdAgendMod.DataBind();
            }
            else
            {
                grdAgendMod.DataSource = null;
                grdAgendMod.DataBind();
            }
        }

        /// <summary>
        /// Retorna o nome da situação
        /// </summary>
        /// <param name="CO_SITUA"></param>
        private string RecuperaSituacaoAgenda(string CO_SITUA, string FL_CONFIR, string FL_ENCAM, string fl_justif)
        {
            string s = "";
            switch (CO_SITUA)
            {
                case "C":
                    //Se estiver como cancelado, verifica se foi falta justificada ou não
                    s = fl_justif != null ? (fl_justif == "S" ? "FJ" : "FA") : "CA";
                    break;
                case "A":
                    //Se estiver ativo, verifica se está como presente e encaminhado, ou apenas agendado
                    if (FL_ENCAM == "S")
                        s = "EN";
                    else
                        s = (FL_CONFIR == "S" ? "PR" : "AG");
                    break;
                case "R":
                    //Se estiver R, foi realizada
                    s = "RE";
                    break;
                default:
                    s = "**";
                    break;
            }
            return s;
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

        public class GrdModal
        {
            public DateTime DT_AGEND { get; set; }
            public int? ID_DEPTO { get; set; }
            public string FL_JUSTI_CANCE { get; set; }
            public string TIPO { get; set; }
            public string FL_CONSU_AVALIA { get; set; }
            public string NOME { get; set; }
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
            public string DESC_PASTA { get; set; }
            private String _NO_PACI;
            public String NO_PACI
            {
                get
                {
                    return (this.CO_PACI.HasValue ? TB07_ALUNO.RetornaPeloCoAlu(CO_PACI.Value).NO_ALU.Length > 37 ? TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).NO_ALU.Substring(0, 37) + "..." : TB07_ALUNO.RetornaPeloCoAlu((int)this.CO_PACI).NO_ALU : " - ");
                }
                set { _NO_PACI = value; }
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
    }
}