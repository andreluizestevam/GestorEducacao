using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Data.Objects;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8130_ManutAgendamento
{
    public partial class EdicaoAgendamento : System.Web.UI.Page
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
                CarregaProfissionais();
                txtDtIniResCons.Text = DateTime.Now.ToString();
                txtDtFimResCons.Text = DateTime.Now.ToString();

                CarregarTiposAgendamentos(ddlClassFunci, "", true);
                CarregaTiposConsulta(ddlTipoAg, "", true);

                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOpers, false, true, true, true, false);
                AuxiliCarregamentos.CarregaPlanosSaude(drpPlano, "", false, true, true, true);
            }
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            #region Validacoes

            //==============>Validações dos campos 
            //Verifica se foi selecionado um Paciente
            if (ddlNomeUsu.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente.");
                ddlNomeUsu.Focus();
                return;
            }

            bool SelecHorario = false;

            //Verifica se foi selecionado um horário para marcação da consulta
            foreach (GridViewRow li in grdHorario.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    SelecHorario = true;

                    var dt = DateTime.Parse(((HiddenField)li.FindControl("hidData")).Value);
                    var hr = ((HiddenField)li.FindControl("hidHora")).Value;
                    var coCol = int.Parse(((HiddenField)li.FindControl("hidCoCol")).Value);
                    var coAlu = int.Parse(ddlNomeUsu.SelectedValue);

                    var ddlTpConsul = (DropDownList)li.FindControl("ddlTipoAgendam");
                    var ddlTipo = (DropDownList)li.FindControl("ddlTipo");
                    var ddlOper = (DropDownList)li.FindControl("ddlOperAgend");

                    if (string.IsNullOrEmpty(ddlTpConsul.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de consulta");
                        ddlTpConsul.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a Classificação do Agendamento");
                        ddlTipo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(ddlOper.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar O tipo de Contratação do Agendamento");
                        ddlOper.Focus();
                        return;
                    }
                }
            }

            //Valida a variável booleana criada anteriormente
            if (SelecHorario == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um horário da agenda.");
                grdHorario.Focus();
                return;
            }

            #endregion
            Persistencias();
        }

        /// <summary>
        /// Executa os métodos para persistência de dados
        /// </summary>
        private void Persistencias()
        {
            //Se for agenda múltipla
            int coAlu = int.Parse(ddlNomeUsu.SelectedValue);
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

            if (String.IsNullOrEmpty(tb07.FL_LIST_ESP) || tb07.FL_LIST_ESP == "S")
            {
                tb07.FL_LIST_ESP = "N";
                TB07_ALUNO.SaveOrUpdate(tb07);
            }


            //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
            foreach (GridViewRow lis in grdHorario.Rows)
            {
                //Verifica a linha que foi selecionada
                if (((CheckBox)lis.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlTpConsul = (((DropDownList)lis.Cells[3].FindControl("ddlTipoAgendam")));
                    DropDownList ddlTipo = (((DropDownList)lis.Cells[4].FindControl("ddlTipo")));
                    DropDownList ddlOper = (((DropDownList)lis.Cells[5].FindControl("ddlOperAgend")));
                    DropDownList ddlPlan = (((DropDownList)lis.Cells[6].FindControl("ddlPlanoAgend")));
                    DropDownList ddlProc = (((DropDownList)lis.Cells[7].FindControl("ddlProcedAgend")));
                    TextBox txtValor = (((TextBox)lis.Cells[8].FindControl("txtValorAgend")));

                    int coAgend = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoAgenda")).Value));
                    string tpConsul = ddlTpConsul.SelectedValue;

                    //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
                    TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                    tbs174.TP_AGEND_HORAR = tpConsul;
                    tbs174.TP_CONSU = ddlTipo.SelectedValue;
                    tbs174.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
                    tbs174.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue)) : null);
                    tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue)) : null);

                    ////Informações de valores
                    if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
                    {
                        var proced = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc.SelectedValue));
                        var valor = proced.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A");
                        if (valor != null)
                            tbs174.VL_CONSU_BASE = valor.VL_BASE;
                    }
                    tbs174.VL_CONSUL = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);

                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento da Consulta realizado com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
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

                if (ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
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

                if (ddlNomeUsu.Items.FindByValue(res.CO_ALU.ToString()) != null)
                    ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                //txtNisUsu.Text = res.NU_NIS.ToString();
            }
        }

        /// <summary>
        /// Responsável por carregar os profissionais
        /// </summary>
        private void CarregaProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(drpProfissional, 0, true, "0", true);
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            if (drpProfissional.SelectedValue == "0")
                AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
            else
            {
                int Profissional = drpProfissional.SelectedValue != "" ? int.Parse(drpProfissional.SelectedValue) : 0;

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                           where Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0
                           select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

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
                    CarregaOperadoras(ddlOper, "");
                    CarregarPlanosSaude(ddlPlan, ddlOper);

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

            if (!string.IsNullOrEmpty(selec))
                ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Sobrecarga do método que carrega as operadoras de plano de saúde já selecionando o valor recebido como parâmetro
        /// </summary>
        private void CarregaOperadoras(DropDownList ddl, string selec)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true);
            ddl.SelectedValue = selec;
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
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private void CarregaGridHorario()
        {
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

            var pac = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));

            pac.TB250_OPERAReference.Load();

            if (pac.TB250_OPERA != null && ddlOpers.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                ddlOpers.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();

            pac.TB251_PLANO_OPERAReference.Load();

            if (pac.TB251_PLANO_OPERA != null && drpPlano.Items.FindByValue(pac.TB251_PLANO_OPERA.ID_PLAN.ToString()) != null)
                drpPlano.SelectedValue = pac.TB251_PLANO_OPERA.ID_PLAN.ToString();

            TimeSpan? hrInicio = txtHrIni.Text != "" ? TimeSpan.Parse(txtHrIni.Text) : (TimeSpan?)null;
            TimeSpan? hrFim = txtHrFim.Text != "" ? TimeSpan.Parse(txtHrFim.Text) : (TimeSpan?)null;

            //Trata as datas para poder compará-las com as informações no banco
            string dataConver = dtIni.Value.ToString("yyyy/MM/dd");
            DateTime dtInici = DateTime.Parse(dataConver);

            //Trata as datas para poder compará-las com as informações no banco
            string dataConverF = dtFim.Value.ToString("yyyy/MM/dd");
            DateTime dtFimC = DateTime.Parse(dataConverF);

            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where a.CO_ALU == pac.CO_ALU && a.CO_SITUA_AGEND_HORAR == "A"
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtInici)  //dtInici
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFimC))) //dtFimC
                       select new HorarioSaida
                       {
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
                           VL_CONSUL = a.VL_CONSUL,

                           Situacao = a.CO_SITUA_AGEND_HORAR,
                           agendaConfirm = a.FL_CONF_AGEND,
                           agendaEncamin = a.FL_AGEND_ENCAM,
                           faltaJustif = a.FL_JUSTI_CANCE
                       }).OrderBy(w => w.dt).ToList();

            var lst = new List<HorarioSaida>();

            #region Verifica os itens a serem excluídos
            if (res.Count > 0)
            {
                int aux = 0;
                foreach (var i in res)
                {
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

            //Se tiver horario de inicio, filtra
            if (hrInicio != null)
                resNew = resNew.Where(a => a.hrC >= hrInicio).ToList();

            //Se tiver horario de termino, filtra
            if (hrFim != null)
                resNew = resNew.Where(a => a.hrC <= hrFim).ToList();

            //Reordena
            resNew = resNew.OrderBy(w => w.dt).ThenBy(w => w.hrC).ToList();

            grdHorario.DataSource = resNew;
            grdHorario.DataBind();
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
                    string diaSemana = this.dt.ToString("ddd", new System.Globalization.CultureInfo("pt-BR"));
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + diaSemana;
                }
            }
            public int CO_AGEND { get; set; }
            public int? CO_COL { get; set; }
            public int? CO_ESPEC { get; set; }
            public int? CO_DEPTO { get; set; }
            public int? CO_EMP { get; set; }

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
                    return (this.CO_ALU.HasValue ? TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == this.CO_ALU).FirstOrDefault().NO_ALU : " - ");
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
            public decimal? VL_CONSUL { get; set; }

            public string Situacao { get; set; }
            public string agendaConfirm { get; set; }
            public string agendaEncamin { get; set; }
            public string faltaJustif { get; set; }
        }

        #endregion

        #region Eventos de componentes

        protected void grdHorario_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Tipo da Agenda
                string tipoAgenda = ((HiddenField)e.Row.FindControl("hidTpAgend")).Value;
                DropDownList ddlTipoAgenda = ((DropDownList)e.Row.FindControl("ddlTipoAgendam"));
                CarregarTiposAgendamentos(ddlTipoAgenda, tipoAgenda);

                //Tipo Consutla
                string tipoConsul = ((HiddenField)e.Row.FindControl("hidTpConsul")).Value;
                DropDownList ddlTipoConsul = ((DropDownList)e.Row.FindControl("ddlTipo"));
                CarregaTiposConsulta(ddlTipoConsul, tipoConsul);

                //Operadora
                string idOper = ((HiddenField)e.Row.FindControl("hidIdOper")).Value;
                DropDownList ddlOper = ((DropDownList)e.Row.FindControl("ddlOperAgend"));
                CarregaOperadoras(ddlOper, idOper);

                //Plano de Saúde
                string idPlan = ((HiddenField)e.Row.FindControl("hidIdPlan")).Value;
                DropDownList ddlPlano = ((DropDownList)e.Row.FindControl("ddlPlanoAgend"));
                CarregarPlanosSaude(ddlPlano, ddlOper);
                ddlPlano.SelectedValue = idPlan;

                //Procedimento
                string idProced = ((HiddenField)e.Row.FindControl("hidIdProced")).Value;
                DropDownList ddlProced = ((DropDownList)e.Row.FindControl("ddlProcedAgend"));
                CarregaProcedimentos(ddlProced, ddlOper, idProced);
            }
        }

        protected void imgCpfPac_OnClick(object sender, EventArgs e)
        {
            CarregaPacientes();
            PesquisaPaciente();
            OcultarPesquisa(true);
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
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
            CheckBox chkMarca = ((CheckBox)grdHorario.HeaderRow.Cells[0].FindControl("chkMarcaTodosItens"));

            foreach (GridViewRow l in grdHorario.Rows)
            {
                DropDownList ddlClass = (((DropDownList)l.Cells[3].FindControl("ddlTipoAgendam")));
                DropDownList ddlTipo = (((DropDownList)l.Cells[4].FindControl("ddlTipo")));
                DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
                DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");

                if (chkMarca.Checked)
                {
                    CheckBox ck = (((CheckBox)l.Cells[0].FindControl("ckSelectHr")));
                    ck.Checked = true;

                    if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
                        ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

                    if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
                        ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados

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
                }
                else
                {
                    CheckBox ck = (((CheckBox)l.Cells[0].FindControl("ckSelectHr")));
                    ck.Checked = false;

                    ddlClass.SelectedValue = ddlTipo.SelectedValue = ""; //Seleciona vazio novamente

                    CarregaOperadoras(ddlOper, "");
                    CarregarPlanosSaude(ddlPlan, ddlOper);
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

        protected void ckSelectHr_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelectHr"));

                if (atual.ClientID == chk.ClientID)
                {
                    if (chk.Checked)
                    {
                        //Coleta e trata o código do paciente
                        string coAlu = ((HiddenField)l.Cells[0].FindControl("hidCoAlu")).Value;
                        int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);
                        string coAgend = ((HiddenField)l.Cells[0].FindControl("hidCoAgenda")).Value;

                        DropDownList ddlOper = (DropDownList)l.Cells[5].FindControl("ddlOperAgend");
                        DropDownList ddlPlan = (DropDownList)l.Cells[6].FindControl("ddlPlanoAgend");

                        //Coleta o tipo da consulta
                        string tpCon = ((HiddenField)l.Cells[0].FindControl("hidTpCons")).Value;

                        //Coleta controles usados

                        DropDownList ddlClass = (((DropDownList)l.Cells[3].FindControl("ddlTipoAgendam")));
                        DropDownList ddlTipo = (((DropDownList)l.Cells[4].FindControl("ddlTipo")));

                        if (!string.IsNullOrEmpty(ddlClassFunci.SelectedValue))
                            ddlClass.SelectedValue = ddlClassFunci.SelectedValue; //Só marca se estiverem selecionados

                        if (!string.IsNullOrEmpty(ddlTipoAg.SelectedValue))
                            ddlTipo.SelectedValue = ddlTipoAg.SelectedValue; //Só marca se estiverem selecionados

                        #region Seleciona Operadora e Plano do Paciente

                        if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                        {
                            var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
                            res.TB250_OPERAReference.Load();

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
                    }
                }
            }
        }

        protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        {
            //Verifica se foi selecionado um Paciente
            if (ddlNomeUsu.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente.");
                ddlNomeUsu.Focus();
                return;
            }

            CarregaGridHorario();
        }

        protected void drpProfissional_OnSelectedIndexChanged(object sender, EventArgs e)
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
                SelecionaOperadoraPlanoPaciente(coAlu);

                var pac = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                pac.TB250_OPERAReference.Load();

                if (pac.TB250_OPERA != null && ddlOpers.Items.FindByValue(pac.TB250_OPERA.ID_OPER.ToString()) != null)
                    ddlOpers.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();
                else
                    ddlOpers.SelectedValue = "";

                pac.TB251_PLANO_OPERAReference.Load();

                if (pac.TB251_PLANO_OPERA != null && drpPlano.Items.FindByValue(pac.TB251_PLANO_OPERA.ID_PLAN.ToString()) != null)
                    drpPlano.SelectedValue = pac.TB251_PLANO_OPERA.ID_PLAN.ToString();
                else
                    drpPlano.SelectedValue = "";

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

                CarregaGridHorario();
            }
            else
                SelecionaOperadoraPlanoPaciente((int?)null, true);
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

        protected void ddlProcedAgend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlOper, ddlPlan, ddlProc;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddlOper = (DropDownList)linha.Cells[5].FindControl("ddlOperAgend");
                    ddlPlan = (DropDownList)linha.Cells[6].FindControl("ddlPlanoAgend");
                    ddlProc = (DropDownList)linha.Cells[7].FindControl("ddlProcedAgend");
                    TextBox txtValor = (TextBox)linha.Cells[8].FindControl("txtValorAgend");

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    if (ddlProc.ClientID == atual.ClientID)
                        CalcularPreencherValoresTabelaECalculado(ddlProc, ddlOper, ddlPlan, txtValor);
                }
            }
        }

        protected void ddlClassFunci_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Só marca os outros, se o registro estiver selecionado
                if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlClass = (((DropDownList)li.Cells[3].FindControl("ddlTipoAgendam")));
                    ddlClass.SelectedValue = ddlClassFunci.SelectedValue;
                }
            }
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
            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Só marca os outros, se o registro estiver selecionado
                if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlOper = (DropDownList)li.FindControl("ddlOperAgend");
                    DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanoAgend");
                    DropDownList ddlProc = (DropDownList)li.FindControl("ddlProcedAgend");

                    ddlOper.SelectedValue = ddlOpers.SelectedValue;
                    CarregarPlanosSaude(drpPlano, ddlOpers);
                    CarregarPlanosSaude(ddlPlan, ddlOper);
                    CarregaProcedimentos(ddlProc, ddlOper);
                }
            }
        }

        protected void drpPlano_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Só marca os outros, se o registro estiver selecionado
                if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlOper = (DropDownList)li.FindControl("ddlOperAgend");
                    DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanoAgend");
                    DropDownList ddlProc = (DropDownList)li.FindControl("ddlProcedAgend");

                    CarregarPlanosSaude(ddlPlan, ddlOper);

                    if (!String.IsNullOrEmpty(drpPlano.SelectedValue) && ddlPlan.Items.FindByValue(drpPlano.SelectedValue) != null)
                        ddlPlan.SelectedValue = drpPlano.SelectedValue;
                }
            }
        }

        #endregion
    }
}