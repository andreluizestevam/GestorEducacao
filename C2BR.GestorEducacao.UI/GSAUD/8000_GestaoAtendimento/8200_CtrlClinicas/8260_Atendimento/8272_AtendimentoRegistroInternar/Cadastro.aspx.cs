//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR  |   O.S.  | DESCRIÇÃO RESUMIDA
// ---------+-----------------------+---------+--------------------------------
// 27/01/17 | Samira Lira           |         | Criação da funcionalidade para registro de atendimento de internação

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
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Drawing;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8272_AtendimentoRegistroInternar
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

                if (LoginAuxili.FLA_PROFESSOR != "S") // || (tb03.CO_CLASS_PROFI == null || tb03.CO_CLASS_PROFI == "O"))
                {
                    AbreModalPadrao("AbreModalAvisoPermissao()");
                }

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                carregarGridPaciente(0);
                carregarProfissional();
                txtDtAtend.Text = DateTime.Now.ToShortDateString();
                txtHrAtendIni.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                txtDataRepasse.Text = DateTime.Now.ToShortDateString();
                txtHoraRepasse.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

                carregarOperadoras(ddlOperProcPlan);
                carregarOperadoras(ddlOperPlanoServAmbu);
                CarregarPlanos(ddlPlanProcPlan, ddlOperProcPlan);
                CarregarPlanos(ddlOperPlanoServAmbu, ddlPlanoServAmbu);
                CarregaSubGrupoMedicamento(ddlGrupo, ddlSubGrupo);
                CarregaSubGrupoMedicamento(drpGrupoMedic, drpSubGrupoMedic, true);
                CarregaSubGrupos();
                CarregaGrupoMedicamento(ddlGrupo);
                CarregaGrupoMedicamento(drpGrupoMedic, true);
                carregaClassificacaoRisco();

                txtVlBase.Enabled = txtVlCusto.Enabled = txtVlRestitu.Enabled = true;

            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            Persistencias(false);
        }

        #endregion

        #region Inicializar

        private void Persistencias(bool Alta)
        {
            string id = HttpContext.Current.Session["VL_REGIS_INTER"] != null ? HttpContext.Current.Session["VL_REGIS_INTER"].ToString() : "";
            int idRegisInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            if (idRegisInter > 0)
            {
                try
                {
                    int coAlu = !string.IsNullOrEmpty(HttpContext.Current.Session["coAlu"].ToString()) ? int.Parse(HttpContext.Current.Session["coAlu"].ToString()) : 0;
                    int idAtendAgend = 0;
                    int idAtendInter = HttpContext.Current.Session["VL_ATEND_INTER"] != null ? int.Parse(HttpContext.Current.Session["VL_ATEND_INTER"].ToString()) : 0;


                    #region tbs456
                    var _tbs456 = TBS456_INTER_REGIS_ATEND.RetornaPelaChavePrimaria(idAtendInter);
                    var tbs456 = _tbs456 != null ? _tbs456 : new TBS456_INTER_REGIS_ATEND();
                    if (_tbs456 == null)
                    {
                        var tbs455 = TBS455_AGEND_PROF_INTER.RetornaPelaChavePrimaria(idRegisInter);
                        tbs455.TBS451_INTER_REGISTReference.Load();
                        tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                        tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                        idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                        if (!string.IsNullOrEmpty(ddlProfResp.SelectedValue))
                        {
                            int? coColRepas = !string.IsNullOrEmpty(ddlProfResp.SelectedValue) ? int.Parse(ddlProfResp.SelectedValue) : (int?)null;
                            tbs455.TB159_AGENDA_PLANT_COLABORReference.Load();
                            tbs455.TB159_AGENDA_PLANT_COLABOR.TB03_COLABORReference.Load();
                            if (tbs455.TB159_AGENDA_PLANT_COLABOR.TB03_COLABOR.CO_COL != coColRepas)
                            {
                                tbs456.DT_REPAS_PROFI = !string.IsNullOrEmpty(txtDataRepasse.Text) ? DateTime.Parse(txtDataRepasse.Text) : (DateTime?)null;
                                tbs456.HR_REPAS_PROF = !string.IsNullOrEmpty(txtHoraRepasse.Text) ? TimeSpan.Parse(txtHoraRepasse.Text) : (TimeSpan?)null;
                                tbs456.CO_COL_REPAS = coColRepas;
                                tbs455.TB159_AGENDA_PLANT_COLABOR.TB03_COLABOR.CO_COL = (int)coColRepas;
                                TBS455_AGEND_PROF_INTER.SaveOrUpdate(tbs455, true);
                            }
                        }
                        tbs456.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs456.DT_CADAS = DateTime.Now;
                        tbs456.IP_CADAS = Request.UserHostAddress;
                    }
                    else
                    {
                        _tbs456.TBS455_AGEND_PROF_INTERReference.Load();
                        _tbs456.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGISTReference.Load();
                        _tbs456.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                        _tbs456.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                        idAtendAgend = _tbs456.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                    }

                    var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);

                    //finalizar no atendimento tbs390
                    if (Alta)
                    {
                        tbs390.CO_SITUA = "F";
                        TBS390_ATEND_AGEND.SaveOrUpdate(tbs390, true);
                    }
                    tbs456.CLASS_RISCO = ddlClassRisco.SelectedValue;
                    tbs456.CO_COL_ATEND = LoginAuxili.CO_COL;
                    tbs456.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                    //I = internado, A = Alta, C = cancelado
                    tbs456.CO_SITUA = Alta ? "A" : "I";

                    tbs456.DE_OBSER_ATEND = txtObservacaoAtendimento.Text;
                    tbs456.DE_QXA_PRINC = txtQueixa.Text;
                    tbs456.DT_ATEND = !string.IsNullOrEmpty(txtDtAtend.Text) ? DateTime.Parse(txtDtAtend.Text) : DateTime.Now.Date;

                    tbs456.FL_DORES = drpDores.SelectedValue;
                    tbs456.FL_ENJOO = drpEnjoos.SelectedValue;
                    tbs456.FL_FEBRE = drpFebre.SelectedValue;
                    tbs456.FL_VOMIT = drpVomitos.SelectedValue;
                    tbs456.HR_FIM_ATEND = !string.IsNullOrEmpty(txtHrAtenFim.Text) ? TimeSpan.Parse(txtHrAtenFim.Text) : DateTime.Now.TimeOfDay;
                    tbs456.HR_INI_ATEND = !string.IsNullOrEmpty(txtHrAtendIni.Text) ? TimeSpan.Parse(txtHrAtendIni.Text) : DateTime.Now.TimeOfDay;
                    tbs456.HR_GLICE = txtHrGlic.Text;
                    tbs456.HR_PRES_ARTE = txtHrPressao.Text;
                    tbs456.HR_TEMP = txtHrTemp.Text;
                    tbs456.NU_GLICE = !string.IsNullOrEmpty(txtGlic.Text) ? decimal.Parse(txtGlic.Text) : (Decimal?)null;
                    tbs456.NU_PESO_ATUAL = !string.IsNullOrEmpty(txtPeso.Text) ? decimal.Parse(txtPeso.Text) : (Decimal?)null;
                    tbs456.NU_PRES_ARTE = !string.IsNullOrEmpty(txtPressao.Text) ? decimal.Parse(txtPressao.Text) : (Decimal?)null;
                    tbs456.NU_TEMP = !string.IsNullOrEmpty(txtTemp.Text) ? decimal.Parse(txtTemp.Text) : (Decimal?)null;
                    tbs456.TBS455_AGEND_PROF_INTER = TBS455_AGEND_PROF_INTER.RetornaPelaChavePrimaria(idRegisInter);

                    #region Trata sequencial
                    var res = (from tbs411pesq in TBS456_INTER_REGIS_ATEND.RetornaTodosRegistros()
                               select new { tbs411pesq.NU_REGIS }).OrderByDescending(w => w.NU_REGIS).FirstOrDefault();

                    string seq3;
                    int seq4 = 0;
                    int seqConcat1;
                    string seqcon1;
                    string ano1 = DateTime.Now.Year.ToString().Substring(2, 2);
                    string mes1 = DateTime.Now.Month.ToString().PadLeft(2, '0');

                    if (res != null && res.NU_REGIS != null)
                    {
                        seq3 = res.NU_REGIS.Substring(6, 6);
                        seq4 = int.Parse(seq3);
                    }

                    seqConcat1 = seq4 + 1;
                    seqcon1 = seqConcat1.ToString().PadLeft(6, '0');

                    tbs456.NU_REGIS = string.Format("RI{0}{1}{2}", ano1, mes1, seqcon1);

                    #endregion

                    TBS456_INTER_REGIS_ATEND.SaveOrUpdate(tbs456, true);

                    #endregion

                    #region Armazena os Medicamentos

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
                        tbs399.TBS456_INTER_REGIS_ATEND = tbs456;

                        tbs399.DT_CADAS = DateTime.Now;
                        tbs399.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs399.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs399.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                        tbs399.IP_CADAS = Request.UserHostAddress;
                        tbs399.TBS390_ATEND_AGEND = tbs390;
                        TBS399_ATEND_MEDICAMENTOS.SaveOrUpdate(tbs399, true);
                    }

                    #endregion

                    #region Armazena os Exames

                    if ((!string.IsNullOrEmpty(hidCheckEmitirGuiaExame.Value) && hidCheckEmitirGuiaExame.Value.Equals("true"))
                        || (string.IsNullOrEmpty(hidCheckEmitirGuiaExame.Value) && string.IsNullOrEmpty(hidCheckSolicitarExame.Value)
                        || (hidCheckEmitirGuiaExame.Value.Equals("false") && hidCheckSolicitarExame.Value.Equals("true"))))
                    {

                        foreach (GridViewRow i in grdExame.Rows)
                        {
                            DropDownList ddlExame = (DropDownList)i.FindControl("ddlExame");

                            TBS398_ATEND_EXAMES tbs398 = new TBS398_ATEND_EXAMES();
                            tbs398.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlExame.SelectedValue));
                            tbs398.TBS456_INTER_REGIS_ATEND = tbs456;
                            tbs398.DE_OBSER = (!string.IsNullOrEmpty(hidObserExame.Value) ? hidObserExame.Value : null);
                            tbs398.DT_CADAS = DateTime.Now;
                            tbs398.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs398.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs398.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                            tbs398.IP_CADAS = Request.UserHostAddress;
                            tbs398.TBS390_ATEND_AGEND = tbs390;
                            TBS398_ATEND_EXAMES.SaveOrUpdate(tbs398, true);
                        }
                    }

                    if ((!string.IsNullOrEmpty(hidCheckSolicitarExame.Value) && hidCheckSolicitarExame.Value.Equals("true")))
                    {
                        foreach (GridViewRow i in grdExame.Rows)
                        {
                            DropDownList ddlExame = (DropDownList)i.FindControl("ddlExame");
                            var idExame = !string.IsNullOrEmpty(ddlExame.SelectedValue) ? int.Parse(ddlExame.SelectedValue) : 0;

                            var tbs411 = new TBS411_EXAMES_ESTERNOS();
                            tbs411.TBS456_INTER_REGIS_ATEND = tbs456;
                            var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idExame);
                            tbs411.TBS356_PROC_MEDIC_PROCE = tbs356;

                            var tbs353 = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPeloProcedimento(tbs356.ID_PROC_MEDI_PROCE);

                            tbs356.TB250_OPERAReference.Load();
                            tbs411.TB250_OPERA = tbs356.TB250_OPERA;
                            tbs411.FL_CORTESIA = "N";
                            tbs411.NU_QTD_PROCED = 1;
                            tbs411.FL_REQUISICAO = "S";
                            tbs411.VL_PROCED_BASE = tbs353.VL_BASE;
                            tbs411.VL_PROCED = tbs353.VL_BASE;
                            tbs411.NO_SOLICITANTE = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, tbs456.CO_COL_CADAS).NO_COL;
                            //Dados da situação e cadastro do exame
                            tbs411.CO_SITUA = "A";
                            tbs411.DT_SITUA = tbs411.DT_CADAS = DateTime.Now;
                            tbs411.CO_COL_SITUA = tbs411.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs411.CO_EMP_COL_SITUA = tbs411.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                            tbs411.CO_EMP_SITUA = tbs411.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs411.IP_SITUA = tbs411.IP_CADAS = Request.UserHostAddress;
                            tbs411.FL_ATEND = "AT";
                            tbs411.CO_ALU = coAlu;

                            #region Trata sequencial
                            var res2 = (from tbs411pesq in TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                                        select new { tbs411pesq.NU_REGISTRO }).OrderByDescending(w => w.NU_REGISTRO).FirstOrDefault();

                            string seq;
                            int seq2 = 0;
                            int seqConcat;
                            string seqcon;
                            string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                            string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                            if (res2 != null && res2.NU_REGISTRO != null)
                            {
                                seq = res2.NU_REGISTRO.Substring(6, 6);
                                seq2 = int.Parse(seq);
                            }

                            seqConcat = seq2 + 1;
                            seqcon = seqConcat.ToString().PadLeft(6, '0');

                            tbs411.NU_REGISTRO = string.Format("EE{0}{1}{2}", ano, mes, seqcon);

                            #endregion
                            TBS411_EXAMES_ESTERNOS.SaveOrUpdate(tbs411, true);
                        }
                    }

                    grdExame.DataSource = null;
                    grdExame.DataBind();

                    #endregion

                    #region Armazena os Serviços Ambulatoriais

                    TBS426_SERVI_AMBUL tbs426 = new TBS426_SERVI_AMBUL();
                    tbs426.TBS390_ATEND_AGEND = tbs390;
                    tbs390.TBS174_AGEND_HORARReference.Load();
                    tbs426.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR);
                    tbs426.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs426.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs426.DT_CADASTRO = DateTime.Now;
                    tbs426.DE_OBSER = hidObsSerAmbulatoriais.Value;
                    tbs426.IP_CADAS = Request.UserHostAddress;
                    tbs426.TBS456_INTER_REGIS_ATEND = tbs456;

                    TBS426_SERVI_AMBUL.SaveOrUpdate(tbs426, true);
                    didIdServAmbulatorial.Value = tbs426.ID_SERVI_AMBUL.ToString();
                    if (tbs426.ID_SERVI_AMBUL > 0)
                    {
                        if (grdServAmbulatoriais.Rows.Count > 0)
                        {
                            foreach (GridViewRow i in grdServAmbulatoriais.Rows)
                            {
                                DropDownList ddlServAmbulatoriais = (DropDownList)i.FindControl("ddlServAmbulatorial");
                                if (ddlServAmbulatoriais != null)
                                {
                                    TBS427_SERVI_AMBUL_ITENS tbs427 = new TBS427_SERVI_AMBUL_ITENS();
                                    tbs427.TBS426_SERVI_AMBUL = tbs426;
                                    tbs427.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlServAmbulatoriais.SelectedValue));
                                    //tbs427.DE_COMPL = txtComplemento;
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
                                }
                            }

                            grdServAmbulatoriais.DataSource = null;
                            grdServAmbulatoriais.DataBind();
                        }
                        else
                        {
                            TBS426_SERVI_AMBUL.Delete(tbs426, true);
                        }
                    }

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro salvo com sucesso.", Request.Url.AbsoluteUri);

                    #endregion
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                    return;
                }

            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para realizar a operação");
                return;
            }
        }

        private void carregaClassificacaoRisco()
        {
            AuxiliCarregamentos.CarregaClassificacaoRisco(ddlClassRisco, false);
        }

        private void carregarProfissional()
        {
            var profissionalPlantao = TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                        .Select(x => new
                                        {
                                            x.TB03_COLABOR.CO_COL,
                                            NO_COL = (!string.IsNullOrEmpty(x.TB03_COLABOR.DE_FUNC_COL) ? x.TB03_COLABOR.DE_FUNC_COL : "S/R") + " - " + x.TB03_COLABOR.NO_APEL_COL,

                                        }).DistinctBy(x => x.CO_COL).ToList();


            ddlProfResp.DataSource = profissionalPlantao.OrderBy(x => x.NO_COL);
            ddlProfResp.DataTextField = "NO_COL";
            ddlProfResp.DataValueField = "CO_COL";
            ddlProfResp.DataBind();
            ddlProfResp.Items.Insert(0, new ListItem("Selecione", ""));
            ddlProfResp.SelectedValue = LoginAuxili.CO_COL.ToString();
        }

        private void carregarGridPaciente(int tipoFiltro)
        {
            string nomePaciente = txtNomePacPesqAgendAtend.Text;
            DateTime dateNow = DateTime.Now.Date;

            var profissionalPlantao = TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                                        .Where(x => x.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL
                                               && (x.DT_INICIO_PREV >= dateNow && dateNow <= x.DT_TERMIN_PREV))
                                        .Select(w => new
                                        {
                                            w.CO_AGEND_PLANT_COLAB,
                                            w.TB03_COLABOR.CO_COL,
                                            w.TB03_COLABOR.NO_COL,
                                            w.TB03_COLABOR.NO_APEL_COL,
                                        }).FirstOrDefault();

            List<Paciente> pacList = new List<Paciente>();
            if (profissionalPlantao != null)
            {
                var res = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros()
                        .Where(x => x.TB159_AGENDA_PLANT_COLABOR.CO_AGEND_PLANT_COLAB == profissionalPlantao.CO_AGEND_PLANT_COLAB
                            && (!string.IsNullOrEmpty(nomePaciente) ? x.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.NO_ALU.Contains(nomePaciente) : "0" == "0")
                    //&& (!x.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.CO_SITUA.Equals("F"))
                            )
                        .Select(w => new
                        {
                            CO_AGEND_COL_PLAN = w.ID_AGEND_PROFI_INTER,
                            CO_AGEND_PLANT = w.TB159_AGENDA_PLANT_COLABOR.CO_AGEND_PLANT_COLAB,
                            CO_ALU = w.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.CO_ALU,
                            LOCAL = w.TBS451_INTER_REGIST.TB14_DEPTO.NO_DEPTO,
                            NO_ALU = w.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.NO_ALU,
                            SEXO = w.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.CO_SEXO_ALU,
                            idadeAlu = w.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.DT_NASC_ALU.Value,
                            UNIDADE = w.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.TB25_EMPRESA.NO_FANTAS_EMP,
                            dtPrevista = w.DT_ACOMP_PROFI,
                            HR_PREVISTA = w.HR_ACOMP_PROFI,
                            PRIORIDADE = w.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.CO_TIPO_RISCO
                        }).DistinctBy(x => x.CO_ALU).ToList();

                foreach (var item in res)
                {
                    var tbs456 = TBS456_INTER_REGIS_ATEND.RetornaTodosRegistros().Where(x => x.TBS455_AGEND_PROF_INTER.ID_AGEND_PROFI_INTER == item.CO_AGEND_COL_PLAN).FirstOrDefault();
                    var p = new Paciente();
                    p.Atendido = tbs456 != null ? "S" : "N";
                    p.ID_REGIS_ATEND_INTER = tbs456 != null ? tbs456.ID_REGIS_ATEND_INTER : (int?)null;
                    p.CO_AGEND_COL_PLAN = item.CO_AGEND_COL_PLAN;
                    p.CO_AGEND_PLANT = item.CO_AGEND_PLANT;
                    p.CO_ALU = item.CO_ALU;
                    p.PRIORIDADE = item.PRIORIDADE;
                    p.dtPrevista = item.dtPrevista;
                    p.HR_PREVISTA = item.HR_PREVISTA;
                    p.idadeAlu = item.idadeAlu;
                    p.LOCAL = item.LOCAL;
                    p.NO_ALU = item.NO_ALU;
                    p.SEXO = item.SEXO;
                    p.UNIDADE = item.UNIDADE;

                    pacList.Add(p);
                }
            }

            var pacOrdenada = tipoFiltro == 1 ? pacList.OrderBy(x => x.NO_ALU) : tipoFiltro == 2 ? pacList.OrderBy(x => x.PRIORIDADE) : pacList.OrderBy(x => x.LOCAL).ThenBy(x => x.NO_ALU);

            grdPacientes.DataSource = pacOrdenada;
            grdPacientes.DataBind();
        }

        private void carregarGridAtendimento(int idAtendimentoInter)
        {
            var tbs456 = TBS456_INTER_REGIS_ATEND.RetornaTodosRegistros()
                            .Where(x => x.TBS455_AGEND_PROF_INTER.ID_AGEND_PROFI_INTER == idAtendimentoInter).OrderBy(x => x.DT_ATEND)
                            .Select(x => new DemonstrativoAtendimento
                            {
                                data = x.DT_CADAS,
                                Funcao = x.TBS455_AGEND_PROF_INTER.TB159_AGENDA_PLANT_COLABOR.TB03_COLABOR.DE_FUNC_COL,
                                IdAtendimento = x.ID_REGIS_ATEND_INTER,
                                Local = x.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TB14_DEPTO.NO_DEPTO,
                                Unidade = x.TBS455_AGEND_PROF_INTER.TB159_AGENDA_PLANT_COLABOR.TB03_COLABOR.TB25_EMPRESA.sigla,
                                Profissional = x.TBS455_AGEND_PROF_INTER.TB159_AGENDA_PLANT_COLABOR.TB03_COLABOR.NO_APEL_COL
                            }).ToList();

            grdHitoricoAtendimento.DataSource = tbs456.OrderByDescending(x => x.data);
            grdHitoricoAtendimento.DataBind();
            UpdatePanel2.Update();
        }

        private void carregarCamposTextoAtend(int idAtendimentoInter)
        {
            limparCampos();
            var tbs456 = TBS456_INTER_REGIS_ATEND.RetornaPelaChavePrimaria(idAtendimentoInter);
            if (tbs456 != null)
            {
                ////Geral
                txtQueixa.Text = tbs456.DE_QXA_PRINC;
                txtObservacaoAtendimento.Text = tbs456.DE_OBSER_ATEND;

                //Leitura
                txtPressao.Text = tbs456.NU_PRES_ARTE.ToString();
                txtHrPressao.Text = tbs456.HR_PRES_ARTE;
                txtTemp.Text = tbs456.NU_TEMP.ToString();
                //txtHrTemp.Text = tbs456.h
                txtGlic.Text = tbs456.NU_GLICE.ToString();
                txtHrGlic.Text = tbs456.HR_GLICE;
                txtPeso.Text = tbs456.NU_PESO_ATUAL.ToString();
                drpDores.SelectedValue = tbs456.FL_DORES;
                drpEnjoos.SelectedValue = tbs456.FL_ENJOO;
                drpVomitos.SelectedValue = tbs456.FL_VOMIT;
                drpFebre.SelectedValue = tbs456.FL_FEBRE;

                //Registro de atendimento
                txtDtAtend.Text = tbs456.DT_ATEND.ToShortDateString();
                txtHrAtendIni.Text = tbs456.HR_INI_ATEND.HasValue ? (tbs456.HR_INI_ATEND.Value.Hours + ":" + (tbs456.HR_INI_ATEND.Value.Minutes != 0 ? tbs456.HR_INI_ATEND.Value.Minutes : 00)).ToString() : null;
                txtHrAtenFim.Text = tbs456.HR_FIM_ATEND.HasValue ? (tbs456.HR_FIM_ATEND.Value.Hours + ":" + (tbs456.HR_FIM_ATEND.Value.Minutes != 0 ? tbs456.HR_FIM_ATEND.Value.Minutes : 00)).ToString() : null;
                ddlClassRisco.SelectedValue = String.IsNullOrEmpty(tbs456.CLASS_RISCO) ? "0" : tbs456.CLASS_RISCO;
                preencherCorClassRisco(tbs456.CLASS_RISCO);
                ddlProfResp.SelectedValue = tbs456.CO_COL_ATEND.ToString();
            }
        }

        private void preencherCorClassRisco(string classRisco)
        {
            int valorCor = !string.IsNullOrEmpty(classRisco) ? int.Parse(classRisco) : 0;
            switch (valorCor)
            {
                case 0:
                    txtClassRiso.BackColor = Color.White;
                    break;
                case 1:
                    txtClassRiso.BackColor = Color.Red;
                    break;
                case 2:
                    txtClassRiso.BackColor = Color.Orange;
                    break;
                case 3:
                    txtClassRiso.BackColor = Color.Yellow;
                    break;
                case 4:
                    txtClassRiso.BackColor = Color.Green;
                    break;
                case 5:
                    txtClassRiso.BackColor = Color.Blue;
                    break;
                default:
                    txtClassRiso.BackColor = Color.White;
                    break;
            }
        }

        private void carregarGridProcedimento(int idRegisInter, int idAtendimentoInter)
        {
            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaPelaChavePrimaria(idRegisInter);
            tbs455.TBS451_INTER_REGISTReference.Load();
            List<Procedimentos> procList = new List<Procedimentos>();

            var tbs452 = TBS452_INTER_PROCE.RetornaTodosRegistros()
                            .Where(x => x.TBS451_INTER_REGIST.ID_INTER_REGIS == tbs455.TBS451_INTER_REGIST.ID_INTER_REGIS)
                            .Select(x => new Procedimentos
                            {
                                idOrigemProc = x.ID_INTER_PROCE,
                                origemProc = "Reg. Internação",
                                nomeProcedimento = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                data = x.TBS451_INTER_REGIST.DT_INTER,
                                Descricao = x.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                idProcedimento = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                Tipo = x.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI.Equals("OP") ? "OPM" : "PR",
                                Situacao = "NA"
                            }).ToList();

            foreach (var tbs in tbs452)
            {
                var x = new Procedimentos();
                x.data = tbs.data;
                x.Descricao = tbs.Descricao;
                x.idOrigemProc = tbs.idOrigemProc;
                x.idProcedimento = tbs.idProcedimento;
                x.nomeProcedimento = tbs.nomeProcedimento;
                x.origemProc = tbs.origemProc;
                x.Situacao = tbs.Situacao;
                x.Tipo = tbs.Tipo;
                procList.Add(x);
            }

            var tbs456 = TBS456_INTER_REGIS_ATEND.RetornaTodosRegistros().Where(x => x.TBS455_AGEND_PROF_INTER.ID_AGEND_PROFI_INTER == idAtendimentoInter).ToList();

            foreach (var item in tbs456)
            {
                item.TBS455_AGEND_PROF_INTERReference.Load();
                item.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGISTReference.Load();
                item.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                item.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();

                var tbs398 = TBS398_ATEND_EXAMES.RetornaTodosRegistros()
                                .Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == item.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND
                                       && (x.TBS456_INTER_REGIS_ATEND.ID_REGIS_ATEND_INTER == null))
                                .Select(x => new Procedimentos
                                {
                                    idOrigemProc = x.ID_ATEND_EXAMES,
                                    origemProc = "Aten. Hospitalar",
                                    nomeProcedimento = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                    data = x.DT_CADAS,
                                    Descricao = x.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                    idProcedimento = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                    Tipo = "EX",
                                    Situacao = "NA"
                                }).ToList();

                foreach (var tbs in tbs398)
                {
                    var x = new Procedimentos();
                    x.data = tbs.data;
                    x.Descricao = tbs.Descricao;
                    x.idOrigemProc = tbs.idOrigemProc;
                    x.idProcedimento = tbs.idProcedimento;
                    x.nomeProcedimento = tbs.nomeProcedimento;
                    x.origemProc = tbs.origemProc;
                    x.Situacao = tbs.Situacao;
                    x.Tipo = tbs.Tipo;
                    procList.Add(x);
                }

                var _tbs398 = TBS398_ATEND_EXAMES.RetornaTodosRegistros()
                                .Where(x => x.TBS456_INTER_REGIS_ATEND.ID_REGIS_ATEND_INTER == item.ID_REGIS_ATEND_INTER)
                                .Select(x => new Procedimentos
                                {
                                    idOrigemProc = x.ID_ATEND_EXAMES,
                                    origemProc = "Aten. Internação",
                                    nomeProcedimento = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                    data = x.DT_CADAS,
                                    Descricao = x.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                    idProcedimento = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                    Tipo = "EX",
                                    Situacao = "NA"
                                }).ToList();

                foreach (var tbs in _tbs398)
                {
                    var x = new Procedimentos();
                    x.data = tbs.data;
                    x.Descricao = tbs.Descricao;
                    x.idOrigemProc = tbs.idOrigemProc;
                    x.idProcedimento = tbs.idProcedimento;
                    x.nomeProcedimento = tbs.nomeProcedimento;
                    x.origemProc = tbs.origemProc;
                    x.Situacao = tbs.Situacao;
                    x.Tipo = tbs.Tipo;
                    procList.Add(x);
                }

                var tbs411 = TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                               .Where(x => x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == item.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND
                                      && (x.TBS456_INTER_REGIS_ATEND.ID_REGIS_ATEND_INTER == null))
                               .Select(x => new Procedimentos
                               {
                                   idOrigemProc = x.ID_EXAME,
                                   origemProc = "Aten. Hospitalar",
                                   nomeProcedimento = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                   data = x.DT_CADAS,
                                   Descricao = x.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                   idProcedimento = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                   Tipo = "EE",
                                   Situacao = "NA"
                               }).ToList();

                foreach (var tbs in tbs411)
                {
                    var x = new Procedimentos();
                    x.data = tbs.data;
                    x.Descricao = tbs.Descricao;
                    x.idOrigemProc = tbs.idOrigemProc;
                    x.idProcedimento = tbs.idProcedimento;
                    x.nomeProcedimento = tbs.nomeProcedimento;
                    x.origemProc = tbs.origemProc;
                    x.Situacao = tbs.Situacao;
                    x.Tipo = tbs.Tipo;
                    procList.Add(x);
                }

                var _tbs411 = TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                                 .Where(x => x.TBS456_INTER_REGIS_ATEND.ID_REGIS_ATEND_INTER == item.ID_REGIS_ATEND_INTER)
                                 .Select(x => new Procedimentos
                                 {
                                     idOrigemProc = x.ID_EXAME,
                                     origemProc = "Aten. Internação",
                                     nomeProcedimento = x.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                     data = x.DT_CADAS,
                                     Descricao = x.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                     idProcedimento = x.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                     Tipo = "EE",
                                     Situacao = "NA"
                                 }).ToList();

                foreach (var tbs in _tbs411)
                {
                    var x = new Procedimentos();
                    x.data = tbs.data;
                    x.Descricao = tbs.Descricao;
                    x.idOrigemProc = tbs.idOrigemProc;
                    x.idProcedimento = tbs.idProcedimento;
                    x.nomeProcedimento = tbs.nomeProcedimento;
                    x.origemProc = tbs.origemProc;
                    x.Situacao = tbs.Situacao;
                    x.Tipo = tbs.Tipo;
                    procList.Add(x);
                }

                var tbs426 = TBS426_SERVI_AMBUL.RetornaTodosRegistros()
                                .Join(TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros(), x => x.ID_SERVI_AMBUL, y => y.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL, (x, y) => new { x, y })
                                .Where(w => w.x.TBS390_ATEND_AGEND.ID_ATEND_AGEND == item.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND
                                        && w.x.TBS456_INTER_REGIS_ATEND.ID_REGIS_ATEND_INTER == null)
                                .Select(z => new Procedimentos
                                     {
                                         idOrigemProc = z.y.ID_LISTA_SERVI_AMBUL,
                                         origemProc = "Aten. Hospitalar",
                                         nomeProcedimento = z.y.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                         data = z.x.DT_CADASTRO,
                                         Descricao = z.y.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                         idProcedimento = z.y.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                         Tipo = "EE"
                                     }).ToList();

                foreach (var tbs in tbs426)
                {
                    var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros().Where(a => a.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL == tbs.idOrigemProc).FirstOrDefault();

                    var x = new Procedimentos();
                    x.data = tbs.data;
                    x.Descricao = tbs.Descricao;
                    x.idOrigemProc = tbs.idOrigemProc;
                    x.idProcedimento = tbs.idProcedimento;
                    x.nomeProcedimento = tbs.nomeProcedimento;
                    x.origemProc = tbs.origemProc;
                    x.Situacao = tbs428 != null && tbs428.IS_APLIC_SERVI_AMBUL.Equals("S") ? "S" : "N";
                    x.Tipo = tbs.Tipo;
                    procList.Add(x);
                }

                var _tbs426 = TBS426_SERVI_AMBUL.RetornaTodosRegistros()
                               .Join(TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros(), x => x.ID_SERVI_AMBUL, y => y.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL, (x, y) => new { x, y })
                    //.Join(TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros(), a => a.y.ID_LISTA_SERVI_AMBUL, b => b.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL, (a, b) => new{a, b})
                               .Where(w => w.x.TBS456_INTER_REGIS_ATEND.ID_REGIS_ATEND_INTER == item.ID_REGIS_ATEND_INTER)
                               .Select(z => new Procedimentos
                               {
                                   idOrigemProc = z.y.ID_LISTA_SERVI_AMBUL,
                                   origemProc = "Aten. Hospitalar",
                                   nomeProcedimento = z.y.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                   data = z.x.DT_CADASTRO,
                                   Descricao = z.y.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI,
                                   idProcedimento = z.y.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                   Tipo = "EE"
                               }).ToList();

                foreach (var tbs in _tbs426)
                {
                    var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros().Where(a => a.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL == tbs.idOrigemProc).FirstOrDefault();
                    var x = new Procedimentos();
                    x.data = tbs.data;
                    x.Descricao = tbs.Descricao;
                    x.idOrigemProc = tbs.idOrigemProc;
                    x.idProcedimento = tbs.idProcedimento;
                    x.nomeProcedimento = tbs.nomeProcedimento;
                    x.origemProc = tbs.origemProc;
                    x.Situacao = tbs428 != null && tbs428.IS_APLIC_SERVI_AMBUL.Equals("S") ? "S" : "N";
                    x.Tipo = tbs.Tipo;
                    procList.Add(x);
                }
            }

            grdProcedimentos.DataSource = procList.OrderBy(x => x.Tipo).ThenBy(x => x.data);
            grdProcedimentos.DataBind();
            UpdatePanel6.Update();
        }

        private void limparCampos()
        {
            grdHitoricoAtendimento.DataSource =
            grdProfSolicitado.DataSource =
            grdItensCID.DataSource =
            txtQueixa.Text =
            txtObservacaoAtendimento.Text =
            txtPressao.Text =
            txtHrPressao.Text =
            txtTemp.Text =
            txtHrTemp.Text =
            txtGlic.Text =
            txtNomePacPesqAgendAtend.Text =
            txtDtIniProcedimentos.Text =
            txtDtFimProcedimentos.Text =
            txtDataIni.Text =
            txtDataFim.Text =
            txtHrAtenFim.Text =
            txtHrGlic.Text =
            txtPeso.Text = null;
            grdHitoricoAtendimento.DataBind();
            grdProfSolicitado.DataBind();
            grdItensCID.DataBind();
            drpDores.SelectedValue =
            drpEnjoos.SelectedValue =
            drpVomitos.SelectedValue =
            drpFebre.SelectedValue =
            ddlClassRisco.SelectedValue =
            ddlOrdenarGrdPaciente.SelectedValue =
            ddlProfResp.SelectedValue = "";

            txtClassRiso.BackColor = Color.White;
            OcultarPesquisaProfSolicitado(false);
            OcultarPesquisa(false);

            txtDtAtend.Text = DateTime.Now.ToShortDateString();
            txtHrAtendIni.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            txtDataRepasse.Text = DateTime.Now.ToShortDateString();
            txtHoraRepasse.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }

        #endregion

        #region Funções de Campo

        protected void BtnSalvar_OnClick(object sender, EventArgs e)
        {
            Persistencias(false);
        }

        protected void BtnFinalizar_OnClick(object sender, EventArgs e)
        {
            Persistencias(true);
        }

        protected void imgPesqAgendamentos_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlOrdenarGrdPaciente.SelectedValue))
            {
                var tipoFiltro = ddlOrdenarGrdPaciente.SelectedValue.Equals("1") ? 1 : 2;
                carregarGridPaciente(tipoFiltro);
            }
            else
            {
                carregarGridPaciente(0);
            }
        }

        protected void chkSelectPaciente_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            foreach (GridViewRow row in grdPacientes.Rows)
            {
                var chk = ((CheckBox)row.Cells[0].FindControl("chkSelectPaciente"));
                if (chk.ClientID != atual.ClientID)
                {
                    chk.Checked = false;
                }
                else
                {
                    if (chk.Checked)
                    {
                        string idAtendimentoPlantonista = ((HiddenField)row.Cells[0].FindControl("hidCoAgendPlant")).Value;
                        int idAtenimentoInter = !string.IsNullOrEmpty(((HiddenField)row.Cells[0].FindControl("hidIdRegisAtendInter")).Value) ? int.Parse(((HiddenField)row.Cells[0].FindControl("hidIdRegisAtendInter")).Value) : 0;
                        int idRegisInter = !string.IsNullOrEmpty(((HiddenField)row.Cells[0].FindControl("hidCoAgendColPlan")).Value) ? int.Parse(((HiddenField)row.Cells[0].FindControl("hidCoAgendColPlan")).Value) : 0;
                        string coAlu = ((HiddenField)row.Cells[0].FindControl("hidCoAlu")).Value;
                        HttpContext.Current.Session.Add("VL_ATEND_INTER", idAtenimentoInter);
                        HttpContext.Current.Session.Add("VL_REGIS_INTER", idRegisInter);
                        HttpContext.Current.Session.Add("coAlu", coAlu);

                        var x = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                        carregarGridAtendimento(idAtenimentoInter);
                        carregarCamposTextoAtend(idAtenimentoInter);
                        carregarGridProcedimento(idRegisInter, idAtenimentoInter);
                        carregarCamposTextoAtend(idAtenimentoInter);
                    }
                    else
                    {
                        limparCampos();
                        HttpContext.Current.Session.Remove("VL_ATEND_INTER");
                        HttpContext.Current.Session.Remove("coAlu");
                    }
                }
            }
        }

        protected void ddlClassRisco_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valorCor = !string.IsNullOrEmpty(ddlClassRisco.SelectedValue) ? int.Parse(ddlClassRisco.SelectedValue) : 0;
            switch (valorCor)
            {
                case 0:
                    txtClassRiso.BackColor = Color.White;
                    break;
                case 1:
                    txtClassRiso.BackColor = Color.Red;
                    break;
                case 2:
                    txtClassRiso.BackColor = Color.Orange;
                    break;
                case 3:
                    txtClassRiso.BackColor = Color.Yellow;
                    break;
                case 4:
                    txtClassRiso.BackColor = Color.Green;
                    break;
                case 5:
                    txtClassRiso.BackColor = Color.Blue;
                    break;
                default:
                    txtClassRiso.BackColor = Color.White;
                    break;
            }
        }

        protected void ddlOrdenarGrdPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlOrdenarGrdPaciente.SelectedValue))
            {
                var tipoFiltro = ddlOrdenarGrdPaciente.SelectedValue.Equals("1") ? 1 : 2;
                carregarGridPaciente(tipoFiltro);
            }
        }

        #region Anexos

        protected void btnAnexos_OnClick(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                int idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
                string alu = (HttpContext.Current.Session["coAlu"]).ToString();
                int coAlu = !string.IsNullOrEmpty(alu) ? int.Parse(alu) : 0;
                CarregarAnexosAssociados(coAlu);
                AbreModalPadrao("AbreModalAnexos();");
                UpdatePanel10.Update();
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a agenda!");
                grdPacientes.Focus();
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

        protected void lnkbAnexar_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = (HttpContext.Current.Session["VL_ATEND_INTER"]) != null ? (int)HttpContext.Current.Session["VL_ATEND_INTER"] : 0;
            int coAlu = (HttpContext.Current.Session["coAlu"]) != null ? (int)HttpContext.Current.Session["coAlu"] : 0;
            if (!flupAnexo.HasFile)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor selecionar o arquivo");
                AbreModalPadrao("AbreModalAnexos();");
                return;
            }

            if (idAtendInter <= 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para anexar o arquivo");
                return;
            }

            try
            {
                var tbs392 = new TBS392_ANEXO_ATEND();
                //tbs392.TBS390_ATEND_AGEND = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value));
                var co_alu = coAlu;
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

                //TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Arquivo anexado com sucesso!");
            }
            catch (Exception erro)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Ocorreu um erro ao salvar o arquivo. Erro:" + erro.Message);
                AbreModalPadrao("AbreModalAnexos();");
                return;
            }
        }

        protected void imgVisualizarAnexo_OnClick(object sender, EventArgs e) { }

        private void CarregarAnexosAssociados(int coALu)
        {
            var res = (from tbs392 in TBS392_ANEXO_ATEND.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs392.CO_COL_CADAS equals tb03.CO_COL
                       where tbs392.TB07_ALUNO.CO_ALU == coALu
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

        #endregion

        #region Profissional Solicitado

        protected void imgPesProfSolicitado_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaProfSolicitado(true);

            string nome = txtProSolicitado.Text;

            var res = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.FLA_PROFESSOR.Equals("S") && x.NO_COL.Contains(nome)).Select(x => new { NomeCol = (!string.IsNullOrEmpty(x.DE_FUNC_COL) ? x.DE_FUNC_COL : "S/R") + " - " + x.NO_APEL_COL, coCol = x.CO_COL }).OrderBy(x => x.NomeCol);

            drpProSolicitado.DataSource = res;
            drpProSolicitado.DataTextField = "NomeCol";
            drpProSolicitado.DataValueField = "coCol";
            drpProSolicitado.DataBind();

            drpProSolicitado.Items.Insert(0, new ListItem("Selecione", ""));

            UpdatePanel4.Update();
        }

        protected void drpProSolicitado_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            List<itemProfSolicitado> listProfSol = new List<itemProfSolicitado>();
            itemProfSolicitado profSol2 = new itemProfSolicitado();
            foreach (GridViewRow row in grdProfSolicitado.Rows)
            {
                itemProfSolicitado profSol = new itemProfSolicitado();
                profSol.NomeCol = ((Label)row.Cells[0].FindControl("lblNomeProf")).Text; ;
                HiddenField hid = ((HiddenField)row.Cells[1].FindControl("hidIdProfSol"));
                profSol.coCol = !string.IsNullOrEmpty(hid.Value) ? int.Parse(hid.Value) : 0;

                listProfSol.Add(profSol);
            }

            OcultarPesquisaProfSolicitado(false);
            int id = !string.IsNullOrEmpty(drpProSolicitado.SelectedValue) ? int.Parse(drpProSolicitado.SelectedValue) : 0;
            var res = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_COL == id).Select(x => new itemProfSolicitado { NomeCol = x.DE_FUNC_COL + " - " + x.NO_APEL_COL, coCol = x.CO_COL, Obs = "", Anam = "", Acao = "", CID = "", Exam = "" }).FirstOrDefault();

            profSol2.coCol = res.coCol;
            profSol2.NomeCol = res.NomeCol;

            listProfSol.Add(profSol2);

            grdProfSolicitado.DataSource = listProfSol;
            grdProfSolicitado.DataBind();

            UpdatePanel7.Update();
        }

        protected void imgVoltarPesProfSOlicitado_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaProfSolicitado(false);
        }

        private void OcultarPesquisaProfSolicitado(bool ocultar)
        {
            txtProSolicitado.Visible =
            imgPesProfSolicitado.Visible = !ocultar;
            drpProSolicitado.Visible =
            imgVoltarPesProfSOlicitado.Visible = ocultar;

            UpdatePanel4.Update();
        }

        protected void btnDelProfSol_OnClick(object sender, EventArgs e)
        {
            ImageButton clickedButton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;

            string cell1 = row.Cells[1].Text;

            int index = row.RowIndex;

            List<itemProfSolicitado> listProfSol = new List<itemProfSolicitado>();

            foreach (GridViewRow item in grdProfSolicitado.Rows)
            {
                if (item.RowIndex != index)
                {
                    itemProfSolicitado profSol = new itemProfSolicitado();
                    HiddenField hidIdItem = ((HiddenField)row.Cells[0].FindControl("idItemProf"));
                    profSol.idItem = !string.IsNullOrEmpty(hidIdItem.Value) ? int.Parse(hidIdItem.Value) : 0;
                    profSol.NomeCol = ((Label)row.Cells[0].FindControl("lblNomeProf")).Text; ;
                    HiddenField hid = ((HiddenField)row.Cells[1].FindControl("hidIdProfSol"));
                    profSol.coCol = !string.IsNullOrEmpty(hid.Value) ? int.Parse(hid.Value) : 0;
                    profSol.Obs = ((TextBox)row.Cells[3].FindControl("txtObsProfSol")).Text;
                    profSol.Anam = ((TextBox)row.Cells[3].FindControl("txtAnamRepas")).Text;
                    profSol.Acao = ((TextBox)row.Cells[3].FindControl("txtAcaoRepas")).Text;
                    profSol.Exam = ((TextBox)row.Cells[3].FindControl("IdsExamRepas")).Text;
                    profSol.CID = ((TextBox)row.Cells[3].FindControl("txtItemCID")).Text;

                    listProfSol.Add(profSol);
                }
                {
                    HiddenField hidIdItem = ((HiddenField)row.Cells[0].FindControl("idItemProf"));
                    int idItem = !string.IsNullOrEmpty(hidIdItem.Value) ? int.Parse(hidIdItem.Value) : 0;
                    if (idItem > 0)
                    {
                        TBS438_ITENS_ATENDIMENTO.DeletePorID(idItem);
                    }
                }
                grdProfSolicitado.DataSource = listProfSol;
                grdProfSolicitado.DataBind();
                UpdatePanel4.Update();
            }
        }

        #endregion

        #region CID

        protected void drpDefCid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idCID = !string.IsNullOrEmpty(drpDefCid.SelectedValue) ? int.Parse(drpDefCid.SelectedValue) : 0;

            carregarGrdProtocoloCID(idCID);
            //AbreModalPadrao("AbreModalProtocoloCID();");
        }

        private void carregarGrdProtocoloCID(int id)
        {
            if (id > 0)
            {
                List<int> listaidCID = new List<int>();
                List<CID> listaCID = new List<CID>();

                foreach (GridViewRow row in grdItensCID.Rows)
                {
                    int coCID = int.Parse(((HiddenField)row.Cells[0].FindControl("idListaCID")).Value);
                    listaidCID.Add(coCID);
                }

                listaidCID.Add(id);

                foreach (var item in listaidCID)
                {
                    int countProtocolo = TBS434_PROTO_CID.RetornaTodosRegistros().Where(x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == item).Count();

                    bool existeProtocolo = countProtocolo > 0 ? true : false;

                    var res = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                                                .Join(TBS438_ITENS_ATENDIMENTO.RetornaTodosRegistros(), y => y.IDE_CID, x => x.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID, (y, x) => new { y, x })
                                                .Where(w => w.y.IDE_CID == item)
                                                .Select(w => new CID { idItem = w.x.ID_ITEM, idCID = w.y.IDE_CID, coCID = w.y.CO_CID, descCID = w.y.DE_CID, existeProtocolo = existeProtocolo }).FirstOrDefault();
                    if (res != null)
                    {
                        listaCID.Add(res);
                    }
                    else
                    {
                        var query = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros().Where(x => x.IDE_CID == item)
                            .Select(w => new CID { idItem = 0, idCID = w.IDE_CID, coCID = w.CO_CID, descCID = w.DE_CID, existeProtocolo = existeProtocolo }).FirstOrDefault();
                        listaCID.Add(query);
                    }
                }

                grdItensCID.DataSource = listaCID.DistinctBy(x => x.idCID);

                foreach (GridViewRow row in grdItensCID.Rows)
                {
                    string existe = ((HiddenField)row.Cells[2].FindControl("hidExisteProtocolo")).Value;
                    bool existeProtocolo = !string.IsNullOrEmpty(existe) && existe.Equals("true") ? true : false;

                    if (!existeProtocolo)
                    {
                        row.Cells[2].Visible = false;
                    }
                }
                grdItensCID.DataBind();
                UpdatePanel8.Update();
            }
        }

        protected void btnDelCID_OnClick(object sender, EventArgs e)
        {
            ImageButton clickedButton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int index = row.RowIndex;

            List<CID> listaCID = new List<CID>();

            foreach (GridViewRow item in grdItensCID.Rows)
            {
                if (item.RowIndex != index)
                {
                    CID itemCID = new CID();
                    HiddenField hidIdItem = ((HiddenField)item.Cells[0].FindControl("hidIdItemCID"));
                    itemCID.idItem = !string.IsNullOrEmpty(hidIdItem.Value) ? int.Parse(hidIdItem.Value) : 0;
                    HiddenField hidId = ((HiddenField)item.Cells[0].FindControl("idListaCID"));
                    itemCID.idCID = !string.IsNullOrEmpty(hidId.Value) ? int.Parse(hidId.Value) : 0;
                    itemCID.coCID = ((Label)item.Cells[0].FindControl("lblProtCID")).Text;
                    //itemCID.descCID = ((Label)row.Cells[0].FindControl("lblProtTpCID")).ToolTip;
                    string existeProt = ((HiddenField)item.Cells[2].FindControl("hidExisteProtocolo")).Value;
                    itemCID.existeProtocolo = !string.IsNullOrEmpty(existeProt) && existeProt.Equals("true") ? true : false;

                    listaCID.Add(itemCID);
                }
                else
                {
                    HiddenField hidId = ((HiddenField)item.Cells[0].FindControl("hidIdItemCID"));
                    int idItem = !string.IsNullOrEmpty(hidId.Value) ? int.Parse(hidId.Value) : 0;
                    if (idItem > 0)
                    {
                        try
                        {
                            var tbs441 = TBS441_ATEND_HOSPIT_ITEN_PROTO_CID.RetornaTodosRegistros().Where(x => x.TBS438_ITENS_ATENDIMENTO1.ID_ITEM == idItem).Select(x => new { x.ID_ITEM_CID_ATEND }).ToList();
                            for (int i = 0; i < tbs441.Count; i++)
                            {
                                int itemTBS441 = tbs441[i].ID_ITEM_CID_ATEND;
                                var res = TBS441_ATEND_HOSPIT_ITEN_PROTO_CID.RetornaPelaChavePrimaria(itemTBS441);
                                TBS441_ATEND_HOSPIT_ITEN_PROTO_CID.Delete(res, true);
                            }

                            TBS438_ITENS_ATENDIMENTO.DeletePorID(idItem);
                        }
                        catch (Exception ex)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível excluir o item do atendimento, por favor tente novamente ou entre em contato com o suporte. Erro: " + ex.Message);
                        }
                    }
                }

            }
            grdItensCID.DataSource = listaCID;

            foreach (GridViewRow linha in grdItensCID.Rows)
            {
                string existe = ((HiddenField)linha.Cells[2].FindControl("hidExisteProtocolo")).Value;
                bool existeProtocolo = !string.IsNullOrEmpty(existe) && existe.Equals("true") ? true : false;

                if (!existeProtocolo)
                {
                    linha.Cells[2].Visible = false;
                }
            }
            grdItensCID.DataBind();
        }

        protected void imgbPesqCID_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(true);

            string nomeCID = txtDefCid.Text;

            var res = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                .Where(x => x.NO_CID.Contains(nomeCID) || x.CO_CID.Contains(nomeCID))
                .Select(x => new { x.NO_CID, x.IDE_CID }).OrderBy(x => x.NO_CID);
            drpDefCid.DataSource = res;
            drpDefCid.DataTextField = "NO_CID";
            drpDefCid.DataValueField = "IDE_CID";
            drpDefCid.DataBind();

            drpDefCid.Items.Insert(0, new ListItem("Selecione", ""));

            UpdatePanel5.Update();
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtDefCid.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            drpDefCid.Visible =
            imgbVoltarPesq.Visible = ocultar;

            UpdatePanel5.Update();
        }

        #endregion

        #region Receita
        private void CarregaGrupoMedicamento(DropDownList drp, bool relatorio = false)
        {
            AuxiliCarregamentos.CarregaGruposItens(drp, relatorio);
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

        protected void ExcluiItemGridMedic(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridMedic();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_MEDIC"] = dtV;

            carregaGridNovaComContextoMedic();
        }

        private void CarregaSubGrupoMedicamento(DropDownList drpGrupo, DropDownList drpSubGrupo, bool relatorio = false)
        {
            int idGrupo = drpGrupo.SelectedValue != "" ? int.Parse(drpGrupo.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaSubGruposItens(drpSubGrupo, idGrupo, relatorio);
        }

        protected void lnkMedic_OnClick(object sender, EventArgs e)
        {
            txtObserMedicam.Text = hidObserMedicam.Value;

            grdPesqMedic.DataSource = null;
            grdPesqMedic.DataBind();
            AbreModalPadrao("AbreModalMedicamentos();");
            UpdatePanel9.Update();
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

        #endregion

        #region Novo Medicamento

        protected void lnkNovoMedicam_OnClick(object sender, EventArgs e)
        {
            AbreModalPadrao("AbreModalNovoMedic();");
            UpdatePanel9.Update();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupoMedicamento(ddlGrupo, ddlSubGrupo);
            AbreModalPadrao("AbreModalNovoMedic();");
        }

        protected void lnkNovoMedic_OnClick(object sender, EventArgs e)
        {
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
            tb90.TB124_TIPO_PRODUTO = TB124_TIPO_PRODUTO.RetornaPelaChavePrimaria(16);

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
            tb90.CO_MS_ANVISA = null;
            tb90.NO_PRINCIPIO_ATIVO = (!string.IsNullOrEmpty(txtPrinAtiv.Text) ? txtPrinAtiv.Text : null);
            tb90.NU_DUR_PROD = 0;


            TB90_PRODUTO.SaveOrUpdate(tb90, true);
            drpGrupoMedic.SelectedValue = ddlGrupo.SelectedValue;
            drpSubGrupoMedic.SelectedValue = ddlSubGrupo.SelectedValue;
            txtMedicamento.Text = tb90.NO_PROD;
            CarregarMedicamentos();
        }

        protected void BtnReceituario_Click(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;

                try
                {
                    tbs455.TBS451_INTER_REGISTReference.Load();
                    tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                    tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                    tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                    idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    RptReceitMedic2 fpcb = new RptReceitMedic2();
                    var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, idAgendHorar);

                    GerarRelatorioPadrão(fpcb, lRetorno);
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível concluir a operação. Erro: " + ex.Message);
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível concluir a operação.");
                return;
            }
        }

        #endregion

        #region Exames

        protected void lnkAmbul_OnClick(object sender, EventArgs e)
        {
            txtObsServAmbulatoriais.Text = hidObsSerAmbulatoriais.Value;

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void lnkExame_OnClick(object sender, EventArgs e)
        {
            txtObserExame.Text = hidObserExame.Value;
            chkIsGuiaExame.Checked = !string.IsNullOrEmpty(hidCheckEmitirGuiaExame.Value) && hidCheckEmitirGuiaExame.Value.Equals("true") ? true : false;
            chkIsExameExterno.Checked = !string.IsNullOrEmpty(hidCheckSolicitarExame.Value) && hidCheckSolicitarExame.Value.Equals("true") ? true : false;

            AbreModalPadrao("AbreModalExames();");
        }

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

        private DataTable CriarColunasELinhaGridServAmbulatoriais()
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

            //dcATM = new DataColumn();
            //dcATM.DataType = System.Type.GetType("System.String");
            //dcATM.ColumnName = "COMPLEMENTO";
            //dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VALOR";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdServAmbulatoriais.Rows)
            {
                linha = dtV.NewRow();
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlServAmbulatorial")).SelectedValue);
                linha["NMPROCED"] = (((TextBox)li.FindControl("txtDeServAmbulatorial")).Text);
                //linha["COMPLEMENTO"] = (((TextBox)li.FindControl("txtComServAmbulatorial")).Text);
                linha["VALOR"] = (((TextBox)li.FindControl("txtValorServAmbulatorial")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }

        private void CarregarProcedimentos(DropDownList ddlprocp, int oper = 0, string tipoProc = null)
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlprocp, oper, false, true, tipoProc, true);
        }

        protected void carregaGridNovaComContextoAmbulatorio()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_PLA"];

            grdServAmbulatoriais.DataSource = dtV;
            grdServAmbulatoriais.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdServAmbulatoriais.Rows)
            {
                DropDownList ddlCodigoi;
                TextBox txtNmProced;
                TextBox valorProced;
                //TextBox compProced;
                ddlCodigoi = ((DropDownList)li.FindControl("ddlServAmbulatorial"));
                txtNmProced = ((TextBox)li.FindControl("txtDeServAmbulatorial"));
                valorProced = ((TextBox)li.FindControl("txtValorServAmbulatorial"));
                //compProced = ((TextBox)li.FindControl("txtValorServAmbulatorial"));

                string codigo, nmProced, vlrProced;//, complemento;

                //Coleta os valores do dtv da modal popup
                codigo = dtV.Rows[aux]["PROCED"].ToString();
                nmProced = dtV.Rows[aux]["NMPROCED"].ToString();
                //complemento = dtV.Rows[aux]["COMPLEMENTO"].ToString();
                vlrProced = dtV.Rows[aux]["VALOR"].ToString();

                var opr = 0;

                if (!String.IsNullOrEmpty(ddlOperPlanoServAmbu.SelectedValue) && (!String.IsNullOrEmpty(ddlPlanoServAmbu.SelectedValue) && int.Parse(ddlPlanoServAmbu.SelectedValue) != 0))
                {
                    var plan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanoServAmbu.SelectedValue));
                    plan.TB250_OPERAReference.Load();
                    opr = plan.TB250_OPERA != null ? plan.TB250_OPERA.ID_OPER : 0;
                }

                CarregarProcedimentos(ddlCodigoi, opr, "SA");
                ddlCodigoi.SelectedValue = codigo;

                valorProced.Text = vlrProced;
                aux++;
            }
        }

        protected void CriaNovaLinhaGridServicosAmulatoriais()
        {
            DataTable dtV = CriarColunasELinhaGridServAmbulatoriais();

            DataRow linha = dtV.NewRow();
            linha["PROCED"] = "";
            linha["NMPROCED"] = "";
            //linha["COMPLEMENTO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_PROC_PLA"] = dtV;

            carregaGridNovaComContextoAmbulatorio();
        }

        protected void ExcluiItemGridExame(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridExame();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_PLA"] = dtV;

            carregaGridNovaComContextoExame();
        }

        protected void ExcluiItemGridServAmbulatoriais(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridServAmbulatoriais();

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_PLA"] = dtV;

            carregaGridNovaComContextoAmbulatorio();
        }

        private void CarregarPlanos(DropDownList ddlPlanop, DropDownList ddlOperp)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanop, ddlOperp, true, true, true);
        }

        private void carregarOperadoras(DropDownList ddlOperp)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperp, true, true, true, false);
        }

        protected void lnkAddProcPla_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridExame();

            AbreModalPadrao("AbreModalExames();");
        }

        protected void lnkAddProcPlaAmbulatorial_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridServicosAmulatoriais();

            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void btnGuiaServAmbulatoriais_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
                var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                int lRetorno = 0;
                RptGuiaAmbulatorial rpt = new RptGuiaAmbulatorial();
                lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, idAtendAgend, 12);
                GerarRelatorioPadrão(rpt, lRetorno);
            }
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

        protected void imgServAmbulatoriaisPla_OnClick(object sender, EventArgs e)
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
            ExcluiItemGridServAmbulatoriais(aux);

            AbreModalPadrao("AbreModalAmbulatorio();");
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

        protected void ddlServAmbu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdServAmbulatoriais.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdServAmbulatoriais.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlServAmbulatorial");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        TextBox txtDesProced = (TextBox)linha.FindControl("txtDeServAmbulatorial");
                        TextBox vlrProced = (TextBox)linha.FindControl("txtValorServAmbulatorial");

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
            AbreModalPadrao("AbreModalAmbulatorio();");
        }

        protected void BtnExames_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
            }

            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            RptReceitExames2 fpcb = new RptReceitExames2();
            var lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, idAtendAgend);

            GerarRelatorioPadrão(fpcb, lRetorno);
        }

        protected void BtnGuiaExames_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
            }
            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            RptGuiaExames rpt = new RptGuiaExames();
            var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, idAtendAgend);

            GerarRelatorioPadrão(rpt, lRetorno);
        }

        #endregion

        #region Novo Exame

        private void CarregaSubGrupos()
        {
            int coGrupo = ddlGrupo2.SelectedValue != "" ? int.Parse(ddlGrupo2.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo2, coGrupo, false);
        }

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

        #region Ficha de Atendimento

        protected void lnkFicha_OnClick(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(hidIdAgenda.Value))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente através da agenda!");
            //    grdPacientes.Focus();
            //    return;
            //}

            //txtQxsFicha.Text = txtQueixa.Text;
            //txtAnamneseFicha.Text = txtHDA.Text;
            //txtDiagnosticoFicha.Text = "";

            AbreModalPadrao("AbreModalFichaAtendimento();");
        }

        protected void lnkbImprimirFicha_Click(object sender, EventArgs e)
        {
            //int idAtendInter = 0;
            //if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            //{
            //    string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
            //    idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o atendimento para realizar emissão de guia de exames");
            //    return;
            //}

            //int idAtendAgend = idAtendInter > 0 ? (TBS456_INTER_REGIS_ATEND.RetornaTodosRegistros().Where(x => x.TBS455_AGEND_PROF_INTER.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault()).TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS455_AGEND_PROF_INTER.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND : 0;

            //string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //RptFichaAtend2 rpt = new RptFichaAtend2();
            //var retorno = rpt.InitReport("FICHA DE ATENDIMENTO", infos, LoginAuxili.CO_EMP, co_alu, 0, txtObsFicha.Text, txtQxsFicha.Text, txtAnamneseFicha.Text, txtDiagnosticoFicha.Text, txtExameFicha.Text);

            //GerarRelatorioPadrão(rpt, retorno);
        }

        #endregion

        #region Atestado

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

            if (string.IsNullOrEmpty(txtQtdDias.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É necessario informar a quantidade de dias para a emissão!");
                return;
            }

            int ck = 0;


            //TBS390_ATEND_AGEND tbs390 = (string.IsNullOrEmpty(hidIdAtendimento.Value) ? new TBS390_ATEND_AGEND() : TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimento.Value)));

            //var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidIdAgenda.Value));
            //var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
            }

            foreach (GridViewRow li in grdPacAtestado.Rows)
            {
                RadioButton rdo = (((RadioButton)li.FindControl("rbtPaciente")));

                if (rdo.Checked)
                {
                    var nmPac = ((HiddenField)li.FindControl("hidNmPac")).Value;

                    //if (tbs390 != null)
                    //{
                    if (chkAtestado.Checked)
                    {
                        //try
                        //{
                        var rgPac = ((HiddenField)li.FindControl("hidRgPac")).Value;
                        var hora = ((HiddenField)li.FindControl("hidHora")).Value;

                        var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                        RptAtestadoMedico2 fpcb = new RptAtestadoMedico2();

                        //                    TBS333_ATEST_MEDIC_PACIE tbs333 = new TBS333_ATEST_MEDIC_PACIE();
                        //                    tbs333.ID_DOCUM = 0;

                        //                    HiddenField paciente = (HiddenField)li.Cells[0].FindControl("hidCoALuAtes");
                        //                    HiddenField coResp = (HiddenField)li.Cells[0].FindControl("hidCoRespAtest");

                        //                    tbs333.IDE_CID = !string.IsNullOrEmpty(txtCid.Text) ? int.Parse(txtCid.Text) : 0;
                        //                    tbs333.ID_ATEND_MEDIC = !string.IsNullOrEmpty(hidIdAtendimento.Value) ? int.Parse(hidIdAtendimento.Value) : tbs390.ID_ATEND_AGEND;
                        //                    tbs333.CO_ALU = int.Parse(paciente.Value);
                        //                    tbs333.QT_DIAS = int.Parse(txtQtdDias.Text);
                        //                    tbs333.DT_ATEST_MEDIC = DateTime.Parse(txtDtAtestado.Text);
                        //                    tbs333.DT_CADAS = DateTime.Now;
                        //                    tbs333.CO_EMP_MEDIC = LoginAuxili.CO_EMP;
                        //                    tbs333.CO_COL_MEDIC = LoginAuxili.CO_COL;
                        //                    tbs333.CO_EMP = LoginAuxili.CO_EMP;
                        //                    tbs333.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(coResp.Value));

                        //                    #region Sequencial NR Registro

                        //                    string coUnid = LoginAuxili.CO_UNID.ToString();
                        //                    int coEmp = LoginAuxili.CO_EMP;
                        //                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                        //                    var res = (from tbs333pesq in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                        //                               where tbs333pesq.CO_EMP == coEmp && tbs333pesq.NU_REGIS_ATEST_MEDIC != null
                        //                               select new { tbs333pesq.NU_REGIS_ATEST_MEDIC }).OrderByDescending(w => w.NU_REGIS_ATEST_MEDIC).FirstOrDefault();

                        //                    string seq;
                        //                    int seq2;
                        //                    int seqConcat;
                        //                    string seqcon;
                        //                    if (res == null)
                        //                    {
                        //                        seq2 = 1;
                        //                    }
                        //                    else
                        //                    {
                        //                        seq = res.NU_REGIS_ATEST_MEDIC.Substring(7, 6);
                        //                        seq2 = int.Parse(seq);
                        //                    }

                        //                    seqConcat = seq2 + 1;
                        //                    seqcon = seqConcat.ToString().PadLeft(6, '0');

                        //                    tbs333.NU_REGIS_ATEST_MEDIC = "AT" + ano + coUnid.PadLeft(3, '0') + seqcon;


                        var lRetorno = fpcb.InitReport("Atestado Médico", infos, LoginAuxili.CO_EMP, nmPac, txtQtdDias.Text, chkCid.Checked, txtCid.Text, rgPac, txtDtAtestado.Text, hora, LoginAuxili.CO_COL);

                        //                    if (lRetorno > 0)
                        //                    {
                        //                        TBS333_ATEST_MEDIC_PACIE.SaveOrUpdate(tbs333, true);
                        //                    }

                        GerarRelatorioPadrão(fpcb, lRetorno);
                        //                }
                        //                catch (Exception ex)
                        //                {
                        //                    AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                        //                    return;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, salve o atendimento.");
                        //                return;
                    }
                    //}

                    //        if (chkComparecimento.Checked)
                    //        {
                    //            var nmResp = ((HiddenField)li.FindControl("hidNmResp")).Value;

                    //            var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    //            RptDclComparecimento fpcb = new RptDclComparecimento();
                    //            var lRetorno = fpcb.InitReport("Declaração de Comparecimento", infos, LoginAuxili.CO_EMP, nmPac, nmResp, this.drpPrdComparecimento.SelectedItem.Text, txtDtAtestado.Text, LoginAuxili.CO_COL);

                    //            GerarRelatorioPadrão(fpcb, lRetorno);
                    //}

                    ck++;
                }
                //}

                if (ck == 0)
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser selecionado pelo menos um paciente!");
            }
        }

        #endregion

        #region Guia

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

        #region Obito

        protected void BtnObito_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;

                var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgendHorar);
                var tbs390 = TBS390_ATEND_AGEND.RetornaPelaChavePrimaria(idAtendAgend);
                tbs390.TB07_ALUNOReference.Load();
                txtPacienteObito.Text = tbs390.TB07_ALUNO.NO_ALU;
                txtRegAtendimentoObito.Text = tbs390.NU_REGIS;
                txtDataObito.Text = DateTime.Now.ToShortDateString();
                txtHoraObito.Text = DateTime.Now.ToShortTimeString();

                AbreModalPadrao("AbreModalObito();");
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum atendimento foi encontrado, por favor selecione um ou salve o registro corrente.");
            }
        }

        protected void btnSalvarObito_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
            }

            try
            {
                if (idAtendAgend > 0)
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
                    throw new ArgumentException("Nenhum atendimento foi encontrado, por favor selecione um ou salve o registro corrente.");
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        #endregion

        #region Prontuario

        protected void lnkbProntuario_OnClick(object sender, EventArgs e)
        {

            int idAtendInter = 0;
            int coAlu = 0;

            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
                gerarProntuario(idAtendInter);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para realizar a operação");
                return;
            }
        }

        protected void imgBtnFichaAtend_Click(object sender, EventArgs e)
        {
            ImageButton clickedButton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;
            int index = row.RowIndex;
            int idAtendInterPlan = 0;

            foreach (GridViewRow item in grdPacientes.Rows)
            {
                if (item.RowIndex == index)
                {
                    idAtendInterPlan = !string.IsNullOrEmpty(((HiddenField)row.Cells[0].FindControl("hidCoAgendColPlan")).Value) ? int.Parse(((HiddenField)row.Cells[0].FindControl("hidCoAgendColPlan")).Value) : 0;
                }
            }
            if (idAtendInterPlan > 0)
            {
                gerarProntuario(idAtendInterPlan);
            }
        }

        private void gerarProntuario(int idAtendInterPlan)
        {
            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInterPlan).FirstOrDefault();

            if (tbs455 != null)
            {
                try
                {
                    var data = DateTime.Now;
                    int idAgendHorar = 0;
                    int paciente = 0;

                    tbs455.TBS451_INTER_REGISTReference.Load();
                    tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                    tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                    tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                    idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;

                    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgendHorar);
                    if (tbs174 != null)
                    {
                        paciente = tbs174.CO_ALU.Value;
                        data = tbs174.DT_AGEND_HORAR;
                    }

                    string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8270_Prontuario/8271_Relatorios/RelProntuario.aspx");
                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

                    var dtIni = data.AddMonths(-1).ToShortDateString();
                    var dtFim = data.ToShortDateString();

                    C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuario rpt = new C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios.RptProntuario();
                    var lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, paciente, true, true, dtIni, dtFim, true, dtIni, dtFim, titulo.ToUpper());

                    GerarRelatorioPadrão(rpt, lRetorno);
                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível completar a operação. Por favor tente novamente ou entre em contato com o suporte. Erro: " + ex.Message);
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível completar a operação. Por favor tente novamente ou entre em contato com o suporte.");
                return;
            }
        }

        #endregion

        protected void lnkbAtendSim_OnClick(object sender, EventArgs e)
        {
            int idAtendInter = 0;
            int idAtendAgend = 0;
            if (HttpContext.Current.Session["VL_ATEND_INTER"] != null)
            {
                string id = HttpContext.Current.Session["VL_ATEND_INTER"].ToString();
                idAtendInter = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um atendimento para proseguir com a operação.");
                return;
            }

            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros().Where(x => x.ID_AGEND_PROFI_INTER == idAtendInter).FirstOrDefault();

            if (tbs455 != null)
            {
                var data = DateTime.Now;
                int idAgendHorar = 0;
                tbs455.TBS451_INTER_REGISTReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORARReference.Load();
                idAtendAgend = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.ID_ATEND_AGEND;
                idAgendHorar = tbs455.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.ID_AGEND_HORAR;
            }

            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAtendAgend);


            if (tbs174.FL_AGEND_ENCAM == "S")
            {
                tbs174.FL_AGEND_ENCAM = "A";

                tbs174.DT_ATEND = DateTime.Now;
                tbs174.CO_COL_ATEND = LoginAuxili.CO_COL;
                tbs174.CO_EMP_COL_ATEND = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                tbs174.CO_EMP_ATEND = LoginAuxili.CO_EMP;
                tbs174.IP_ATEND = Request.UserHostAddress;
                tbs174.HR_ATEND_INICIO = DateTime.Now;
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

        #region Classes de Saída

        public class Paciente
        {
            public int? ID_REGIS_ATEND_INTER { get; set; }
            public int CO_ALU { get; set; }
            public int CO_AGEND_PLANT { get; set; }
            public int CO_AGEND_COL_PLAN { get; set; }
            public string NO_ALU { get; set; }
            public string LOCAL { get; set; }
            public string SEXO { get; set; }
            public DateTime idadeAlu { get; set; }
            public string IDADE
            {
                get { return AuxiliFormatoExibicao.FormataDataNascimento(this.idadeAlu, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto); }
            }
            public string UNIDADE { get; set; }
            public DateTime dtPrevista { get; set; }
            public string DT_PREVISTA { get { return this.dtPrevista.ToString("dd/MM/yy"); } }
            public TimeSpan HR_PREVISTA { get; set; }
            public string DTHR { get { return DT_PREVISTA + " " + this.HR_PREVISTA.Hours.ToString() + ":" + (this.HR_PREVISTA.Minutes.ToString().Length == 1 ? this.HR_PREVISTA.Minutes.ToString() + "0" : this.HR_PREVISTA.Minutes.ToString()); } }
            public int? PRIORIDADE { get; set; }
            public Color COR_PRIORIDADE
            {
                get
                {
                    switch (PRIORIDADE)
                    {
                        case 0:
                            return Color.White;
                        case 1:
                            return Color.Red;
                        case 2:
                            return Color.Orange;
                        case 3:
                            return Color.Yellow;
                        case 4:
                            return Color.Green;
                        case 5:
                            return Color.Blue;
                        default:
                            return Color.White;
                    }
                }
            }
            public string TOOLTIP_PRIORIDADE
            {
                get
                {
                    switch (PRIORIDADE)
                    {
                        case 0:
                            return "Nenhuma";
                        case 1:
                            return "Emergência";
                        case 2:
                            return "Muito Urgente";
                        case 3:
                            return "Urgente";
                        case 4:
                            return "Pouco Urgente";
                        case 5:
                            return "Não urgente";
                        default:
                            return "Não definida";
                    }
                }
            }
            public string Atendido { get; set; }
            public bool isAtendido { get { return this.Atendido.Equals("S") ? true : false; } }
            public bool naoAtendido { get { return this.Atendido.Equals("N") ? true : false; } }
            public bool Cancelado { get { return this.Atendido.Equals("C") ? true : false; } }
        }

        public class ProfissionalSaude
        {
            public int CO_COL { get; set; }
            public string NO_COL { get; set; }
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

        public class itemProfSolicitado
        {
            public int idItem { get; set; }
            public int coCol { get; set; }
            public string NomeCol { get; set; }
            public string Obs { get; set; }
            public string Anam { get; set; }
            public string Acao { get; set; }
            public string Exam { get; set; }
            public string CID { get; set; }
        }

        public class CID
        {
            public int? idItem { get; set; }
            public int idCID { get; set; }
            public string descCID { get; set; }
            public string coCID { get; set; }
            public bool existeProtocolo { get; set; }
        }

        public class Procedimentos
        {
            public int idProcedimento { get; set; }
            public int idOrigemProc { get; set; }
            public string origemProc { get; set; }
            public string nomeProcedimento { get; set; }
            public string Tipo { get; set; }
            public DateTime? data { get; set; }
            public string DataHora { get { return this.data.Value.ToString("dd/MM/yy") + " - " + this.data.Value.ToString("HH:mm"); } }
            public string Descricao { get; set; }
            public string Situacao { get; set; }
            public bool isAplic { get { return this.Situacao.Equals("S") ? true : false; } }
            public bool isNaoReali { get { return this.Situacao.Equals("N") ? true : false; } }
            public bool isCancelado { get { return this.Situacao.Equals("C") ? true : false; } }
            public bool isNaoInfo { get { return this.Situacao.Equals("NA") ? true : false; } }
        }

        public class DemonstrativoAtendimento
        {
            public int IdAtendimento { get; set; }
            public DateTime data { get; set; }
            public string DataHora { get { return this.data.ToString("dd/MM/yy") + " - " + this.data.ToString("HH:mm"); } }
            public string Funcao { get; set; }
            public string Profissional { get; set; }
            public string Unidade { get; set; }
            public string Local { get; set; }
        }

        #endregion

    }
}