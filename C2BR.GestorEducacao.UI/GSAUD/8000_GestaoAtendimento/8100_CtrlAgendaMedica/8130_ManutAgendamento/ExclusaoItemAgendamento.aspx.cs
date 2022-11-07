//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE RECEITAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA        |  NOME DO PROGRAMADOR          | DESCRIÇÃO RESUMIDA
// -------------+-------------------------------+------------------------------
//  28/06/2016  Tayguara Acioli   TA.28.06.2016  Adicionei o campo de pesquisa do paciente de forma fonetica.
//  04/10/2016  Diogo Gomes                      Inclusão nova condiçao para retornar os agendamentos com situação 'W'
//
//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Data.Objects;
using System.Globalization;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8130_ManutAgendamento
{
    public partial class ExclusaoItemAgendamento : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return ((PadraoCadastros)this.Master); } }
        private Dictionary<string, string> tipoAgrupador = AuxiliBaseApoio.chave(tipoAgrupadorFinanceiro.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaUnidade();
                CarregaPacientes();

                CarregaClassFuncionais();
                CarregaProfissionais();

                Session["PacsVerificados"] = new List<int>();
            }
        }

        //--->TA.28.06.2016 início
        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));

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
            ddlAluno.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }
        //--->TA.28.06.2016 fim

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (String.IsNullOrEmpty(drpTipoAcao.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de ação!");
                return;
            }

            foreach (GridViewRow li in grdResumo.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("chkSelect")).Checked)
                {
                    #region Altera as informações dos agendamentos para nulos

                    int idAgenda = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdAgenda")).Value);

                    TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgenda);

                    if (drpTipoAcao.SelectedValue == "E")
                    {
                        tbs174.CO_ALU = (int?)null;
                        tbs174.TP_CONSU = null;
                        tbs174.FL_CONF_AGEND = "N";
                        tbs174.FL_CONFIR_CONSUL_SMS = null;
                        tbs174.VL_CONSU_BASE = (decimal?)null;
                        tbs174.VL_DESCT = (decimal?)null;
                        tbs174.TB250_OPERA = null;
                        tbs174.TB251_PLANO_OPERA = null;
                        tbs174.NU_PLAN_SAUDE = null;
                        tbs174.TBS356_PROC_MEDIC_PROCE = null;
                        tbs174.VL_CONSUL = (decimal?)null;
                        tbs174.CO_CLASS_RISCO = (int?)null;
                        tbs174.TP_AGEND_HORAR = null;
                        tbs174.CO_TIPO_PROC_MEDI = null;
                    }
                    else if (drpTipoAcao.SelectedValue == "C")
                    {
                        tbs174.CO_SITUA_AGEND_HORAR = "C";
                        tbs174.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs174.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs174.DT_SITUA_AGEND_HORAR = DateTime.Now;

                        tbs174.FL_JUSTI_CANCE = "C";
                        tbs174.DE_OBSER_CANCE = "";
                        tbs174.DT_CANCE = DateTime.Now;
                        tbs174.CO_COL_CANCE = LoginAuxili.CO_COL;
                        tbs174.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                        tbs174.IP_CANCE = Request.UserHostAddress;
                    }


                    #region Tbs458 (Tabela referentes ao tratamento odontológico)

                    var tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == tbs174.ID_AGEND_HORAR).ToList();
                    foreach (var item in tbs458)
                    {
                        var _tbs458 = TBS458_TRATA_PLANO.RetornaPelaChavePrimaria(item.ID_TRATA_PLANO);
                        TBS458_TRATA_PLANO.Delete(_tbs458, true);
                    }

                    #endregion

                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

                    #endregion

                    #region Salva no log

                    if (drpTipoAcao.SelectedValue == "E")
                    {
                        TBS374_LOG_ITENS_AGEND tbs374 = new TBS374_LOG_ITENS_AGEND();

                        tbs374.TBS174_AGEND_HORAR = tbs174;

                        tbs374.CO_ALU_ANTES = (int?)null;
                        tbs374.TP_CONSUL_ANTES = null;
                        tbs374.FL_CONFIR_AGEND_ANTES = null;
                        tbs374.FL_CONFIR_CONSUL_SMS_ANTES = null;
                        tbs374.VL_CONSU_BASE_ANTES = (decimal?)null;
                        tbs374.VL_DESCT_ANTES = (decimal?)null;
                        tbs374.ID_OPER_ANTES = (int?)null;
                        tbs374.ID_PLAN_ANTES = (int?)null;
                        tbs374.NU_PLAN_SAUDE = (int?)null;
                        tbs374.ID_PROC_MEDI_PROCE_ANTES = (int?)null;
                        tbs374.VL_CONSUL_ANTES = (decimal?)null;
                        tbs374.CO_CLASS_RISCO_ANTES = (int?)null;
                        tbs374.TP_AGEND_HORAR_ANTES = null;
                        tbs374.CO_TIPO_PROC_MEDI_ANTES = null;

                        tbs374.CO_ALU_DEPOIS = (int?)null;
                        tbs374.TP_CONSUL_DEPOIS = null;
                        tbs374.FL_CONFIR_AGEND_DEPOIS = null;
                        tbs374.FL_CONFIR_CONSUL_SMS_DEPOIS = null;
                        tbs374.VL_CONSU_BASE_DEPOIS = (decimal?)null;
                        tbs374.VL_DESCT_DEPOIS = (decimal?)null;
                        tbs374.ID_OPER_DEPOIS = (int?)null;
                        tbs374.ID_PLAN_DEPOIS = (int?)null;
                        tbs374.NU_PLAN_DEPOIS = (int?)null;
                        tbs374.ID_PROC_MEDI_PROCE_DEPOIS = (int?)null;
                        tbs374.VL_CONSUL_DEPOIS = (decimal?)null;
                        tbs374.CO_CLASS_RISCO_DEPOIS = (int?)null;
                        tbs374.TP_AGEND_HORAR_DEPOIS = null;
                        tbs374.CO_TIPO_PROC_MEDI_DEPOIS = null;

                        tbs374.CO_COL_EXEC = LoginAuxili.CO_COL;
                        tbs374.CO_EMP_COL_EXEC = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs374.CO_EMP_EXEC = LoginAuxili.CO_EMP;
                        tbs374.DT_EXEC = DateTime.Now;
                        tbs374.IP_EXEC = Request.UserHostAddress;
                        tbs374.CO_TIPO_ALTER_ITENS_AGENDA = "E";
                        tbs374.DE_OBSER = txtObservacoes.Text;

                        TBS374_LOG_ITENS_AGEND.SaveOrUpdate(tbs374, true);
                    }
                    else if (drpTipoAcao.SelectedValue == "C")
                    {
                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();
                        tbs375.TBS174_AGEND_HORAR = tbs174;

                        tbs375.FL_JUSTI = "C";
                        tbs375.DE_OBSER = txtObservacoes.Text;
                        tbs375.CO_SITUA_AGEND_HORAR = "C";

                        tbs375.FL_TIPO_LOG = "C";
                        tbs375.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs375.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs375.DT_CADAS = DateTime.Now;
                        tbs375.IP_CADAS = Request.UserHostAddress;
                        TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375);
                    }

                    #endregion
                }
            }

            VerificarAlteracaoSituacaoPaciente();
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o DropDown Nome da Fonte com os Alunos
        /// </summary>
        private void CarregaPacientes()
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaPacientes(ddlAluno, coEmp, true, true);
        }

        /// <summary>
        /// Método que carrega o DropDown de Unidades de Contrato
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as classificações funcionais
        /// </summary>
        private void CarregaClassFuncionais()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncional, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        /// <summary>
        /// Carrega os profissionais
        /// </summary>
        private void CarregaProfissionais()
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            string classFunc = ddlClassFuncional.SelectedValue;
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, coEmp, true, classFunc, true);
        }

        private void VerificarAlteracaoSituacaoPaciente()
        {
            var ultimo = false;
            foreach (GridViewRow li in grdResumo.Rows)
            {
                var chk = (CheckBox)li.FindControl("chkSelect");

                if (chk.Checked)
                {
                    var dtInicio = DateTime.Parse(txtDataPeriodoIni.Text);
                    chk.Checked = false;

                    int coAlu = int.Parse(((HiddenField)li.FindControl("hidCoAlu")).Value);

                    if (!((List<int>)Session["PacsVerificados"]).Contains(coAlu))
                    {
                        var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                   where tbs174.CO_ALU == coAlu && tbs174.DT_AGEND_HORAR >= dtInicio
                                   select tbs174).ToList();

                        if (res == null || res.Count == 0)
                        {
                            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                            if (tb07.CO_SITU_ALU == "A")
                            {
                                hidCoAluAlterar.Value = tb07.CO_ALU.ToString();
                                lblPaciente.Text = tb07.NO_ALU.ToUpper();
                                ScriptManager.RegisterStartupScript(
                                    this.Page,
                                    this.GetType(),
                                    "Acao",
                                    "AbreModalSituacaoPaciente();",
                                    true
                                );
                                break;
                            }
                        }
                    }
                }

                if (li.RowIndex == (grdResumo.Rows.Count - 1))
                    ultimo = true;
            }

            if (ultimo)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Alterado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #endregion

        #region Eventos de componentes da pagina

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Session["PacsVerificados"] = new List<int>();
            if (string.IsNullOrEmpty(txtDataPeriodoIni.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data inicial está vazia ");
                return;
            }

            if (string.IsNullOrEmpty(txtDataPeriodoFim.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data Final está vazia ");
                return;
            }
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int coAlu = (!string.IsNullOrEmpty(ddlAluno.SelectedValue) ? int.Parse(ddlAluno.SelectedValue) : 0);
            int coProfi = (!string.IsNullOrEmpty(ddlProfissional.SelectedValue) ? int.Parse(ddlProfissional.SelectedValue) : 0);
            string coClassProfi = ddlClassFuncional.SelectedValue;

            DateTime dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            DateTime dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);
            //-----------------------------------------------------------------------------------------
            TimeSpan? hrInicio = txtHrIni.Text != "" ? TimeSpan.Parse(txtHrIni.Text) : (TimeSpan?)null;
            TimeSpan? hrFim = txtHrFim.Text != "" ? TimeSpan.Parse(txtHrFim.Text) : (TimeSpan?)null;
            //-----------------------------------------------------------------------------------------
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       where (tbs174.CO_SITUA_AGEND_HORAR == "A" || tbs174.CO_SITUA_AGEND_HORAR == "W" )
                           && (coAlu != 0 ? tbs174.CO_ALU == coAlu : 0 == 0)
                           && (coEmp != 0 ? tbs174.CO_EMP == coEmp : 0 == 0)
                           && (coClassProfi != "0" ? tb03.CO_CLASS_PROFI == coClassProfi : 0 == 0)
                           && (coProfi != 0 ? tbs174.CO_COL == coProfi : 0 == 0)
                           && (tbs174.DT_AGEND_HORAR >= dtInicio)
                           && (tbs174.DT_AGEND_HORAR <= dtFim)
                       select new Dummy
                       {
                           CO_ALU = tb07.CO_ALU,
                           NO_PAC = tb07.NO_ALU,
                           NU_NIRE = tb07.NU_NIRE,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           CO_CLASS_FUNC = tbs174.TP_AGEND_HORAR,
                           CO_MATRICULA = tb03.CO_MAT_COL,
                           NO_COL = tb03.NO_APEL_COL,
                           ID_AGEND_HORAR = tbs174.ID_AGEND_HORAR,
                            CO_SITUA_AGEND_HORAR = tbs174.CO_SITUA_AGEND_HORAR == "W" ? "Movimentado" : tbs174.CO_SITUA_AGEND_HORAR == "A" ? "Agendado" : "-"
                       }).OrderBy(w => w.DT).ThenBy(w => w.HR).ToList();

            var lst = new List<Dummy>();

            #region Verifica os itens a serem excluídos
            if (res.Count > 0)
            {
                int aux = 0;
                foreach (var i in res)
                {
                    int dia = (int)i.DT.DayOfWeek;

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


            var resNew = res.Except(lst).Where(w => w.DT >= dtInicio && w.DT <= dtFim).ToList();
            //Se tiver horario de inicio, filtra
            if (hrInicio != null)
                resNew = resNew.Where(a => a.hrC >= hrInicio).ToList();

            //Se tiver horario de termino, filtra
            if (hrFim != null)
                resNew = resNew.Where(a => a.hrC <= hrFim).ToList();

            this.grdResumo.DataSource = resNew;
            this.grdResumo.DataBind();

            divGrid.Visible = liResumo.Visible = true;
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPacientes();
            CarregaProfissionais();
        }

        protected void chkMarcaTodosItens_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdResumo.HeaderRow.Cells[0].FindControl("chkMarcaTodosItens"));

            //Percorre alterando o checkbox de acordo com o selecionado em marcar todos
            foreach (GridViewRow li in grdResumo.Rows)
            {
                CheckBox ck = (((CheckBox)li.Cells[0].FindControl("chkSelect")));
                ck.Checked = chkMarca.Checked;
            }
        }

        protected void ddlClassFuncional_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionais();
        }

        protected void lnkbAlterSim_OnClick(object sender, EventArgs e)
        {
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoAluAlterar.Value));

            tb07.CO_SITU_ALU = drpSituacao.SelectedValue;
            tb07.DT_SITU_ALU = DateTime.Now;

            TB07_ALUNO.SaveOrUpdate(tb07);

            ((List<int>)Session["PacsVerificados"]).Add(int.Parse(hidCoAluAlterar.Value));

            VerificarAlteracaoSituacaoPaciente();
        }

        protected void lnkbAlterNao_OnClick(object sender, EventArgs e)
        {
            ((List<int>)Session["PacsVerificados"]).Add(int.Parse(hidCoAluAlterar.Value));

            VerificarAlteracaoSituacaoPaciente();
        }

        #endregion
    }

    public class Dummy
    {
        public int CO_ALU { get; set; }
        public string hora
        {
            get
            {
                string diaSemana = this.DT.ToString("ddd", new CultureInfo("pt-BR"));
                return this.DT.ToShortDateString() + " - " + this.HR + " " + diaSemana;
            }
        }
        public string PACIENTE
        {
            get
            {
                return this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.NO_PAC;
            }
        }
        public int NU_NIRE { get; set; }
        public string NO_PAC { get; set; }
        public DateTime DT { get; set; }
        public string DT_VALID
        {
            get
            {
                return this.DT.ToString("dd/MM/yyyy");
            }
        }
        public string HR { get; set; }
        public string CO_CLASS_FUNC { get; set; }
        public int ID_AGEND_HORAR { get; set; }
        public TimeSpan hrC
        {
            get
            {
                //DateTime d = DateTime.Parse(hr);
                return TimeSpan.Parse((HR + ":00"));
            }
        }
        public string NO_CLASS_FUNC
        {
            get
            {
                return (AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_FUNC));
            }
        }
        public string NO_COL { get; set; }
        public string CO_MATRICULA { get; set; }

        public string CO_SITUA_AGEND_HORAR { get; set; }
    }
}