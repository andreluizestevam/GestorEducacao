//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE PONTO DO COLABORADOR
// OBJETIVO: REGISTRO DE PLANTÃO
// DATA DE CRIAÇÃO: 20/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//11/06/2014| MAXWELL ALMEIDA            | Criação da página para encaminhamento do Pré-Atendimento para o Atendimento propriamente dito.
//30/12/2014| MAXWELL ALMEIDA            | Inserção de regra para salvar o código do Pré-Atendimento no Direcionamento
//20/02/2017| Samira Lira                | Inserir opção de definir o(s) profissional(is)

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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8240_RegisAgendaAvaliacao
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Eventos

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtDtAgenda.Text = DateTime.Now.ToShortDateString();
                txtDtAgenda.Enabled = true;

                //txtCPFResp.Text = "00000000000";
                txtDtNascResp.Text = "01/01/1900";
                txtDtNascPaci.Text = "01/01/1900";
                txtNuIDResp.Text = "000000";
                txtOrgEmiss.Text = "SSP";
                //dtDataContato.Text = DateTime.Now.ToString();
                //txtDtAgenda.Text = DateTime.Now.ToString();
                //txtHrAgenda.Text = string.Format("{0}{1}", DateTime.Now.Hour, DateTime.Now.Minute);
                //txtDtAgenda.Enabled = txtHrAgenda.Enabled = false;

                //------------> Tamanho da imagem de Aluno
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;
                upImagemAluno.MostraProcurar = false;

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOper, false, true, false, true);
                CarregarPlanosSaude(ddlPlan, ddlOper);
                CarregarCategoriasPlano(ddlCateg, ddlPlan);
                carregaCidade();
                carregaBairro();
                CarregaDadosUnidLogada();
                VerificarNireAutomatico();
                CarregarFuncoesSimp();
                carregaGridSolicitacoes();
                CarregarDeficiencias();


            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        /// <summary>
        /// Salva as informações nas tabelas cabíveis, TB108, TB07 e TBS195
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (SalvaEntidades())
                AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento de Consulta/Avaliação realizado com Sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario()
        {
            CarregaFormulario();
            carregarGridMedicosAvali();
        }

        protected void grdMedicosAvali_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                ////Tipo Consutla
                //string tipoConsul = ((HiddenField)e.Row.Cells[5].FindControl("hidLocalAtendimento")).Value;
                //DropDownList ddlTipoConsul = ((DropDownList)e.Row.Cells[5].FindControl("ddlLocalAtendimento"));

                //AuxiliCarregamentos.CarregaDepartamentos(ddlTipoConsul, LoginAuxili.CO_EMP, false);
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações de acordo com o registro que se deseja editar
        /// </summary>
        private void CarregaFormulario()
        {
            try
            {
                TBS372_AGEND_AVALI tbs372 = RetornaEntidade();

                if (tbs372 != null)
                {
                    tbs372.TB250_OPERAReference.Load();
                    tbs372.TB251_PLANO_OPERAReference.Load();
                    tbs372.TB367_CATEG_PLANO_SAUDEReference.Load();

                    CarregaDadosPaciente(tbs372.CO_ALU, tbs372.CO_RESP);

                    txtDtAgenda.Text = tbs372.DT_AGEND.ToString();
                    //txtHrAgenda.Text = tbs372.HR_AGEND;
                    //chkPsico.Checked = (tbs372.CO_INDIC_PROFI_PSICO == "S" ? true : false);
                    //chkFono.Checked = (tbs372.CO_INDIC_PROFI_FONOA == "S" ? true : false);
                    //chkTeraOcup.Checked = (tbs372.CO_INDIC_PROFI_TEROC == "S" ? true : false);
                    //chkFisio.Checked = (tbs372.CO_INDIC_PROFI_FISIO == "S" ? true : false);
                    //chkOutro.Checked = (tbs372.CO_INDIC_PROFI_OUTRO == "S" ? true : false);


                    //chkPscicPeda.Checked = (tbs372.CO_INDIC_PROFI_PEG == "S" ? true : false);
                    //chkOdonto.Checked = (tbs372.CO_INDIC_PROFI_ODO == "S" ? true : false);
                    //chkMed.Checked = (tbs372.CO_INDIC_PROFI_MEDICO == "S" ? true : false);

                    //chkCmPlano.Checked = (tbs372.TP_CONTR_PLANO == "S" ? true : false);
                    //chkCmParticular.Checked = (tbs372.TP_CONTR_PARTI == "S" ? true : false);
                    //chkCmOutro.Checked = (tbs372.TP_CONTR_OUTRO == "S" ? true : false);
                    chkCortesia.Checked = (tbs372.FL_CORTESIA == "S" ? true : false);

                    txtNecessidade.Text = tbs372.DE_OBSER_NECES;
                    txtDtSitua.Text = tbs372.DT_SITUA.ToString();

                    //Duas situações excepcionais que são inseridas apenas caso necessário
                    if (tbs372.CO_SITUA == "E")
                        ddlSituacao.Items.Insert(0, new ListItem("Encaminhado", "E"));

                    if (tbs372.CO_SITUA == "R")
                        ddlSituacao.Items.Insert(0, new ListItem("Realizado", "R"));

                    //Se estiver encaminhado ou realizado, nada poderá ser alterado
                    if ((tbs372.CO_SITUA == "E") || (tbs372.CO_SITUA == "R") || (tbs372.CO_SITUA == "C"))
                    {
                        DesabilitaCampos();
                        AbreMensagemInfos("As informações de agnendamento não podem ser alteradas, pois o agendamento está cancelado!");

                    }
                    ddlSituacao.SelectedValue = hidSituacao.Value = tbs372.CO_SITUA;
                    ddlTipo.SelectedValue = hidTipo.Value = tbs372.FL_TIPO_AGENDA;
                    txtLocal.Text = tbs372.DE_LOCAL;

                    if (tbs372.FL_TIPO_AGENDA == "P")
                    {
                        lblQueixa.Text = "Local ";
                        txtLocal.Visible = true;
                        ddlQueixa.Visible = false;
                    }
                    else
                    {
                        lblQueixa.Text = "Queixa ";
                        txtLocal.Visible = false;
                        ddlQueixa.Visible = true;
                    }

                    ddlOper.SelectedValue = (tbs372.TB250_OPERA != null ? tbs372.TB250_OPERA.ID_OPER.ToString() : "");
                    CarregarPlanosSaude(ddlPlan, ddlOper);
                    ddlPlan.SelectedValue = (tbs372.TB251_PLANO_OPERA != null ? tbs372.TB251_PLANO_OPERA.ID_PLAN.ToString() : "");
                    CarregarCategoriasPlano(ddlCateg, ddlPlan);
                    ddlCateg.SelectedValue = (tbs372.TB367_CATEG_PLANO_SAUDE != null ? tbs372.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE.ToString() : "");
                    txtVencPlano.Text = tbs372.DT_VENC_PLAN.HasValue ? tbs372.DT_VENC_PLAN.Value.ToShortDateString() : "";
                    txtNumeroPlano.Text = !String.IsNullOrEmpty(TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value)).NU_PLANO_SAUDE) ? TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value)).NU_PLANO_SAUDE : "";

                    CarregarGridSolicitacoes(tbs372.ID_AGEND_AVALI);
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao caregar  formulário" + " - " + ex.Message);
                return;
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS372_AGEND_AVALI</returns>
        private TBS372_AGEND_AVALI RetornaEntidade()
        {
            TBS372_AGEND_AVALI tb372 = TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb372 == null) ? new TBS372_AGEND_AVALI() : tb372;
        }

        /// <summary>
        /// Desabilita todos os campos da página
        /// </summary>
        private void DesabilitaCampos(bool Habilita = false)
        {
            txtNuProntuario.Enabled = lnkProfissionaisAtend.Enabled = txtVencPlano.Enabled = txtCpfPaci.Enabled = txtNuNisPaci.Enabled = txtnompac.Enabled = ddlSexoPaci.Enabled
                = txtDtNascPaci.Enabled = ddlEtniaAlu.Enabled = ddlOrigemPaci.Enabled = txtNuCarSaude.Enabled = txtTelCelPaci.Enabled
                = txtTelResPaci.Enabled = txtWhatsPaci.Enabled = txtCPFResp.Enabled = txtNomeResp.Enabled = ddlSexResp.Enabled
                = txtDtNascResp.Enabled = ddlGrParen.Enabled = txtNuIDResp.Enabled = txtOrgEmiss.Enabled = ddlUFOrgEmis.Enabled
                = txtTelFixResp.Enabled = txtTelCelResp.Enabled = txtTelComResp.Enabled = txtNuWhatsResp.Enabled = txtDeFaceResp.Enabled
                = ddlFuncao.Enabled = txtEmailResp.Enabled =
                txtCEP.Enabled = ddlUF.Enabled = ddlCidade.Enabled = ddlBairro.Enabled = txtLograEndResp.Enabled =
                txtEmailPaci.Enabled = chkPaciEhResp.Enabled = chkPaciMoraCoResp.Enabled
                = ddlTipo.Enabled = txtDtAgenda.Enabled = ddlSituacao.Enabled = txtDtSitua.Enabled
                /*= chkPsico.Enabled = chkFono.Enabled = chkTeraOcup.Enabled = chkFisio.Enabled = chkPscicPeda.Enabled
                = chkOutro.Enabled*/ = chkCortesia.Enabled//= chkCmPlano.Enabled = chkCmParticular.Enabled = chkCmOutro.Enabled
                = ddlOper.Enabled = ddlPlan.Enabled = ddlCateg.Enabled = txtLocal.Enabled = ddlQueixa.Enabled = txtNumeroPlano.Enabled
                = txtNecessidade.Enabled = grdSolicitacoes.Enabled = btnMaisSolicitacoes.Enabled = Habilita;
        }

        /// <summary>
        /// Carrega a grid com os procedimentos solicitados
        /// </summary>
        private void CarregarGridSolicitacoes(int ID_AGEND_AVALI)
        {
            var res = (from tbs373 in TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros()
                       where tbs373.TBS372_AGEND_AVALI.ID_AGEND_AVALI == ID_AGEND_AVALI
                       select new
                       {
                           PROCEDIMENTO = tbs373.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           QTDE = tbs373.QT_SESSO,
                           TPCONTR = tbs373.TP_CONTR,
                           DE = tbs373.TBS387_DEFIC != null ? tbs373.TBS387_DEFIC.ID_DEFIC : 0,

                       }).ToList();
            foreach (var item in res)
            {
                if (item.DE != 0)
                    ddlQueixa.SelectedValue = Convert.ToString(item.DE).ToString();
            }
            grdSolicitacoes.DataSource = res;
            grdSolicitacoes.DataBind();

            ///Laço com a finalidade de setar as informações nos campos
            foreach (GridViewRow i in grdSolicitacoes.Rows)
            {
                DropDownList ddlGrupoProc = (DropDownList)i.Cells[0].FindControl("ddlGrupoProc");
                DropDownList ddlSubGrupo = (DropDownList)i.Cells[1].FindControl("ddlSubGrupo");
                CarregarGrupos(ddlGrupoProc);
                CarregaSubGrupos(ddlSubGrupo, ddlGrupoProc);

                DropDownList ddlProc = (((DropDownList)i.FindControl("ddlProcedimento")));
                TextBox txtQtde = (((TextBox)i.FindControl("txtQtde")));
                DropDownList ddlTipoContrato = (((DropDownList)i.FindControl("ddlTipoContrato")));

                //Seta as informações nos campos
                txtQtde.Text = (((HiddenField)i.FindControl("hidQtde")).Value);
                ddlTipoContrato.SelectedValue = (((HiddenField)i.FindControl("hidTpContrato")).Value);
                //Carrega os procedimentos
                CarregarProcedimentosMedicos(ddlProc, new DropDownList(), new DropDownList());

                ddlProc.SelectedValue = (((HiddenField)i.FindControl("hidIdProc")).Value);
            }
        }

        /// <summary>
        /// Método responsável por salvar as entidades.
        /// </summary>
        private bool SalvaEntidades()
        {
            if (string.IsNullOrEmpty(hidCoPac.Value))
                VerificarNireAutomatico();

            bool erros = false;

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            if (ddlSexoPaci.SelectedValue == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }
            { AbreMensagemInfos("O Sexo do Paciente é Requerido"); return false; }

            if (txtDtNascPaci.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }
            { AbreMensagemInfos("A Data de Nascimento do Paciente é Requerida"); return false; }

            if (txtNuProntuario.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do PRONTUÁRIO do Paciente é Requerido"); erros = true; }
            { AbreMensagemInfos("O Número do PRONTUÁRIO do Paciente é Requerido"); return false; }

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Nome do Responsável é Requerido"); return false; }

            //if (txtCPFResp.Text == "")
            ////{ AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }
            //{ AbreMensagemInfos("O CPF do Responsável é Requerido"); return false; }

            if (ddlSexResp.SelectedValue == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Sexo do Responsável é Requerido"); return false; }

            if (txtDtNascResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("A Data de Nascimento do Responsável é Requerida"); return false; }

            if (txtNuIDResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número da Identidade do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Número da Identidade do Responsável é Requerido"); return false; }

            if (txtCEP.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do Endereço do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O CEP do Endereço do Responsável é Requerido"); return false; }

            if (ddlUF.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O UF do Endereço do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("O UF do Endereço do Responsável é Requerida"); return false; }

            if (ddlCidade.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("A Cidade do Responsável é Requerida"); return false; }

            if (ddlBairro.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Bairro do Responsável é Requerido"); return false; }

            if (txtLograEndResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Logradouro do Responsável é Requerido"); return false; }

            //----------------------------------------------------- Valida os campos do Gerais -----------------------------------------------------

            if (string.IsNullOrEmpty(ddlOper.SelectedValue))
            { AbreMensagemInfos("Favor informar o Tipo de Contratação"); ddlOper.Focus(); return false; }

            if (string.IsNullOrEmpty(ddlPlan.SelectedValue))
            { AbreMensagemInfos("Favor informar o Plano"); ddlPlan.Focus(); return false; }

            //Se for Consulta de Avaliação, é obrigatório informar data e hora
            if (ddlTipo.SelectedValue == "C")
            {
                if (string.IsNullOrEmpty(txtDtAgenda.Text))


                { AbreMensagemInfos("O Registro está como Agendamento de Consulta de Avaliação. Favor informar a Data da mesma"); return false; }

                //if (string.IsNullOrEmpty(txtHrAgenda.Text))
                //{ AbreMensagemInfos("O Registro está como Agendamento de Consulta de Avaliação. Favor informar a Hora da mesma"); return false; }
            }

            //Se for Procedimentos, é obrigatório informar data e hora
            if (ddlTipo.SelectedValue == "P")
            {
                if (string.IsNullOrEmpty(txtDtAgenda.Text))


                { AbreMensagemInfos("O Registro está como Procedimentos. Favor informar a Data"); return false; }

                //if (string.IsNullOrEmpty(txtHrAgenda.Text))
                //{ AbreMensagemInfos("O Registro está como Procedimentos. Favor informar a Hora"); return false; }
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            var cpfValido = true;

            if (!String.IsNullOrEmpty(txtCPFResp.Text))
            {
                if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                    txtCPFResp.Focus();
                    return false;
                }
            }
            else if (tb25.FL_CPF_RESP_OBRIGATORIO == "S")
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do responsável é obrigatório");
                txtCPFResp.Focus();
                return false;
            }
            else if (tb25.FL_CPF_RESP_OBRIGATORIO == "N" && String.IsNullOrEmpty(txtCPFResp.Text))
            {
                if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    var cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    //Enquanto existir, calcula um novo cpf
                    while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado || w.NU_CONTROLE == cpfGerado).Any())
                        cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                    txtCPFResp.Text = cpfGerado;
                }
                cpfValido = false;
            }

            if (!String.IsNullOrEmpty(txtCpfPaci.Text))
            {
                if (!AuxiliValidacao.ValidaCpf(txtCpfPaci.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do paciente invalido!");
                    txtCpfPaci.Focus();
                    return false;
                }
            }
            else if (tb25.FL_CPF_PAC_OBRIGATORIO == "S")
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente é obrigatório");
                txtCpfPaci.Focus();
                return false;
            }
          
            if (ddlTipo.SelectedValue != "L")
            {
                bool verificaProfHorario = false;
                bool verificaProfSelecionado = false;
                bool verificaProfLocal = false;

                foreach (GridViewRow row in grdMedicosAvali.Rows)
                {
                    bool chk = ((CheckBox)row.Cells[0].FindControl("chkProfAvali")).Checked;
                    string dtAgenda = ((TextBox)row.Cells[4].FindControl("Horario")).Text;
                    string local = ((DropDownList)row.Cells[5].FindControl("ddlLocalAtendimento")).SelectedValue;

                    if (chk)
                    {
                        verificaProfSelecionado = true;
                        if (string.IsNullOrEmpty(dtAgenda))
                        {
                            ViewState["verificaProfHorario"] = true;
                            verificaProfHorario = true;
                            break;
                        }

                        if (string.IsNullOrEmpty(local) || local == "0")
                        {
                            verificaProfLocal = true;
                            break;
                        }
                    }
                }

                if (verificaProfHorario)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Você deve inserir um horário para o profissional selecionado.");
                    return false;
                }

                if (verificaProfLocal)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Você deve inserir um local para o profissional selecionado.");
                    return false;
                }

                if (!verificaProfSelecionado)
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Você deve selecionar pelo menos um profissional de atendimento.");
                    return false;
                }
            }

           

            #region Verifica o NIRE

            //Apenas verifica novo nire, se for inclusão de paciente
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                string strTipoNireAuto = "";
                //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado

                tb25.TB83_PARAMETROReference.Load();
                if (tb25.TB83_PARAMETRO != null)
                    strTipoNireAuto = tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO != null ? tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO : "";

                if (strTipoNireAuto != "")
                {
                    //----------------> Faz a verificação para saber se o NIRE é automático ou não
                    if (strTipoNireAuto == "N")
                    {
                        if (txtNuProntuario.Text.Replace(".", "").Replace("-", "") == "")
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Nº CONTROLE deve ser informado.");
                            return false;
                        }

                        int nuNire = int.Parse(txtNuProntuario.Text.Replace(".", "").Replace("-", ""));

                        ///-------------------> Faz a verificação para saber se o NIRE informado já existe
                        var ocorrNIRE = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                         where lTb07.NU_NIRE == nuNire
                                         select new { lTb07.CO_ALU, lTb07.NO_ALU }).FirstOrDefault();


                        if (ocorrNIRE != null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Nº CONTROLE informado já existe para o(a) Paciente(a) " + ocorrNIRE.NO_ALU + ".");
                            return false;
                        }
                    }
                    else
                    {
                        ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                        int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                          select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                        if (lastCoAlu != null)
                        {
                            txtNuProntuario.Text = (lastCoAlu.Value + 1).ToString();
                        }
                        else
                            txtNuProntuario.Text = "1";
                    }
                }
            }

            #endregion

            string cpfPac = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();
            decimal? nis = (decimal?)null;

            if (string.IsNullOrEmpty(txtNuProntuario.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nº CONTROLE precisa ser informado!");
                return false;
            }

            #region Certificações de que não está sendo salva redundância

            //Apenas realiza as verificações abaixo quando for inclusão de novo registro 
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
                {
                    nis = decimal.Parse(txtNuNisPaci.Text);

                    //Verifica se já existe algum paciente com o CPF informado
                    if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfPac).Any() && (!string.IsNullOrEmpty(cpfPac)))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o CPF informado!");
                        return false;
                    }
                    //Verifica se já existe algum paciente com o CNES/SUS/NIS informado
                    else if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIS == nis).Any() && (!string.IsNullOrEmpty(txtNuNisPaci.Text)))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o SUS/CNES informado!");
                        return false;
                    }
                }

                //Verifica se já existe algum paciente com o CPF informado
                if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfPac).Any() && (!string.IsNullOrEmpty(cpfPac)))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o CPF informado!");
                    return false;
                }

                int nire = int.Parse(txtNuProntuario.Text);
                //Verifica se já existe algum paciente com o NIRE/Nº CONTROLE informado
                if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nire).Any())
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o Nº CONTROLE informado!");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                DateTime dataNasctoAlu = DateTime.Parse(txtDtNascPaci.Text);

                ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( quando inclusão )
                var ocorrAlu = from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where lTb07.NO_ALU == txtnompac.Text && lTb07.CO_SEXO_ALU == ddlSexoPaci.SelectedValue && lTb07.DT_NASC_ALU == dataNasctoAlu
                               select new { lTb07.CO_ALU };

                if (ocorrAlu.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Paciente já cadastrado no sistema com o mesmo nome, sexo e data de nascimento.");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                DateTime dataNasctoAlu = DateTime.Parse(txtDtNascPaci.Text);

                ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( diferente do aluno informado na alteração )
                var ocorrAlu = from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where lTb07.NO_ALU == txtnompac.Text && lTb07.CO_SEXO_ALU == ddlSexoPaci.SelectedValue
                               && lTb07.DT_NASC_ALU == dataNasctoAlu && lTb07.CO_ALU != coAlu
                               select new { lTb07.CO_ALU };

                if (ocorrAlu.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Paciente já cadastrado no sistema com o mesmo nome, sexo e data de nascimento!");
                    return false;
                }
            }

            #endregion

            if (erros != true)
            {
                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                TB108_RESPONSAVEL tb108;
                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                var cpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");
                var resp = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NU_CPF_RESP == cpfResp).FirstOrDefault();

                if (resp != null && !String.IsNullOrEmpty(cpfResp) && string.IsNullOrEmpty(hidCoResp.Value))
                    tb108 = resp;
                else if (string.IsNullOrEmpty(hidCoResp.Value))
                    tb108 = new TB108_RESPONSAVEL();
                else
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value));

                if (tb108.NU_CONTROLE != cpfResp && (cpfValido || (!cpfValido && String.IsNullOrEmpty(tb108.NU_CONTROLE))))
                    tb108.NU_CONTROLE = cpfResp;

                tb108.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                tb108.NO_RESP = txtNomeResp.Text.ToUpper();
                tb108.NU_CPF_RESP = cpfResp;
                tb108.FL_CPF_VALIDO = cpfValido ? "S" : "N";
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

                //Atribui valores vazios para os campos not null da tabela de Responsável.
                tb108.DT_SITU_RESP = DateTime.Now;
                tb108.FL_NEGAT_CHEQUE = "V";
                tb108.FL_NEGAT_SERASA = "V";
                tb108.FL_NEGAT_SPC = "V";
                tb108.CO_INST = 0;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                TB108_RESPONSAVEL.SaveOrUpdate(tb108, true);

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                TB07_ALUNO tb07;

                var pac = TB07_ALUNO.RetornaTodosRegistros().FirstOrDefault(a => a.NU_CPF_ALU == cpfPac);

                if (pac != null && !String.IsNullOrEmpty(cpfPac) && string.IsNullOrEmpty(hidCoPac.Value))
                    tb07 = pac;
                else if (string.IsNullOrEmpty(hidCoPac.Value)) //Se não tiver sido buscado nenhum, cria um novo objeto da entidade
                    tb07 = new TB07_ALUNO();
                else //Se não, busca o paciente correspondente
                    tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value));

                #region Bloco foto
                int codImagem = upImagemAluno.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion

                tb07.CO_ORIGEM_ALU = ddlOrigemPaci.SelectedValue;
                tb07.NO_ALU = txtnompac.Text.ToUpper();
                tb07.NO_APE_ALU = tb07.NO_ALU.Split(' ')[0].Length > 25 ? tb07.NO_ALU.Substring(0, 23) : tb07.NO_ALU.Split(' ')[0];
                tb07.NU_NIRE = int.Parse(txtNuProntuario.Text);
                tb07.NU_CPF_ALU = cpfPac;
                tb07.NU_NIS = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
                tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                tb07.TBS387_DEFIC = (!string.IsNullOrEmpty(ddlDeficiencia.SelectedValue) ? TBS387_DEFIC.RetornaPelaChavePrimaria(int.Parse(ddlDeficiencia.SelectedValue)) : null);

                if (chkPaciEhResp.Checked)
                {
                    tb07.CO_RG_ALU = txtNuIDResp.Text;
                    tb07.CO_ORG_RG_ALU = txtOrgEmiss.Text;
                    tb07.CO_ESTA_RG_ALU = ddlUFOrgEmis.SelectedValue;
                    tb07.NO_WEB_ALU = txtEmailResp.Text;
                    tb07.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                }

                //Esses valores de empresas não podem ser alterados na edição, apenas inseridos quando novo
                if (tb07.CO_ALU == 0)
                {
                    tb07.CO_INST = LoginAuxili.ORG_CODIGO_ORGAO;
                    tb07.CO_EMP = LoginAuxili.CO_EMP;
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb07.DT_CADA_ALU = tb07.DT_ENTRA_INSTI = DateTime.Now;
                    tb07.CO_SITU_ALU = "A";
                    tb07.TP_DEF = "N";
                }

                tb07.DT_SITU_ALU = DateTime.Now;
                tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);
                tb07.TP_RACA = ddlEtniaAlu.SelectedValue != "" ? ddlEtniaAlu.SelectedValue : null;

                //Endereço
                tb07.CO_CEP_ALU = txtCEP.Text;
                tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                tb07.FL_LIST_ESP = drpLocacao.SelectedValue;

                //Operadora
                tb07.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
                tb07.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue)) : null);
                tb07.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCateg.SelectedValue) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCateg.SelectedValue)) : null);
                tb07.DT_VENC_PLAN = !String.IsNullOrEmpty(txtVencPlano.Text) ? DateTime.Parse(txtVencPlano.Text) : (DateTime?)null;
                tb07.NU_PLANO_SAUDE = !String.IsNullOrEmpty(txtNumeroPlano.Text) ? txtNumeroPlano.Text : null;

                TB07_ALUNO.SaveOrUpdate(tb07, true);

                #endregion

                //Os dados do Agendamento na tbs372
                #region Salva na tbs372

                #region Dados do Usuário Logado

                int coEmpColLogado = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

                #endregion

                var lstTbs372 = TBS372_AGEND_AVALI.RetornaTodosRegistros();

                foreach (GridViewRow row in grdMedicosAvali.Rows)
                {

                    bool chk = ((CheckBox)row.Cells[0].FindControl("chkProfAvali")).Checked;
                    var dtAgenda = (!string.IsNullOrEmpty(txtDtAgenda.Text) ? DateTime.Parse(txtDtAgenda.Text) : (DateTime?)null);
                    string _coCol = ((HiddenField)row.Cells[0].FindControl("hidcoCol")).Value;

                    if (chk)
                    {
                        string _coEmp = ((HiddenField)row.Cells[0].FindControl("hidcoEmpColPla")).Value;
                        string horario = ((TextBox)row.Cells[4].FindControl("Horario")).Text;
                        string locAtendimento = ((DropDownList)row.Cells[5].FindControl("ddlLocalAtendimento")).SelectedValue;

                        var hidcoCol = 0;

                        int.TryParse(_coCol, out hidcoCol);

                        var tbs372 = lstTbs372.FirstOrDefault(p => p.CO_ALU == tb07.CO_ALU && dtAgenda == p.DT_AGEND.Value && p.TBS460_AGEND_AVALI_PROFI.Any(x => x.CO_COL_AVALI == hidcoCol));

                        if (tbs372 != null)
                        {
                            tbs372.TBS460_AGEND_AVALI_PROFI.Load();
                        }
                        else
                        {
                            tbs372 = new TBS372_AGEND_AVALI();
                        }

                        //var tbs460Item = tbs372.TBS460_AGEND_AVALI_PROFI.FirstOrDefault();

                        //var tbs460ColAvali = tbs460Item != null ? tbs460Item.CO_COL_AVALI.ToString() : "0";

                        //if (!(tbs372 != null && tbs460ColAvali == _coCol))
                        //{

                        //}

                        //======================> Dados Gerais
                        tbs372.CO_ALU = tb07.CO_ALU;
                        tbs372.CO_RESP = tb108.CO_RESP;
                        tbs372.NU_CPF_RESP = txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim();

                        //======================> Dados gerais
                        tbs372.DT_AGEND = (!string.IsNullOrEmpty(txtDtAgenda.Text) ? DateTime.Parse(txtDtAgenda.Text) : (DateTime?)null);
                        tbs372.HR_AGEND = (!string.IsNullOrEmpty(horario) ? horario : null);
                        //tbs372.TP_CONTR_PLANO = (chkCmPlano.Checked ? "S" : "N");
                        //tbs372.TP_CONTR_PARTI = (chkCmParticular.Checked ? "S" : "N");
                        //tbs372.TP_CONTR_OUTRO = (chkCmOutro.Checked ? "S" : "N");
                        tbs372.FL_CORTESIA = (chkCortesia.Checked ? "S" : "N");

                        //======================> Dados gerais
                        tbs372.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
                        tbs372.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue)) : null);
                        tbs372.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCateg.SelectedValue) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCateg.SelectedValue)) : null);
                        tbs372.DT_VENC_PLAN = !String.IsNullOrEmpty(txtVencPlano.Text) ? DateTime.Parse(txtVencPlano.Text) : (DateTime?)null;

                        tbs372.FL_TIPO_AGENDA = ddlTipo.SelectedValue;
                        tbs372.DE_LOCAL = txtLocal.Text;

                        //======================> Dados da Categoria
                        tbs372.CO_INDIC_PROFI_FISIO = (chkFisio.Checked ? "S" : "N");
                        tbs372.CO_INDIC_PROFI_FONOA = (chkFono.Checked ? "S" : "N");
                        tbs372.CO_INDIC_PROFI_TEROC = (chkTeraOcup.Checked ? "S" : "N");
                        tbs372.CO_INDIC_PROFI_PSICO = (chkPsico.Checked ? "S" : "N");
                        //tbs372.CO_INDIC_PROFI_OUTRO = (chkOutro.Checked ? "S" : "N");


                        //tbs372.CO_INDIC_PROFI_PEG = (chkPscicPeda.Checked ? "S" : "N");
                        tbs372.CO_INDIC_PROFI_ODO = (chkOdonto.Checked ? "S" : "N");
                        tbs372.CO_INDIC_PROFI_MEDICO = (chkMed.Checked ? "S" : "N");


                        tbs372.DE_OBSER_NECES = (!string.IsNullOrEmpty(txtNecessidade.Text) ? txtNecessidade.Text : null);

                        //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
                        switch (tbs372.EntityState)
                        {
                            case EntityState.Added:
                            case EntityState.Detached:
                                //======================> Dados do Cadastro
                                tbs372.DT_CADAS = DateTime.Now;
                                tbs372.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs372.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs372.CO_EMP_COL_CADAS = coEmpColLogado;
                                tbs372.IP_CADAS = Request.UserHostAddress;
                                break;
                        }

                        //Verifica se foi alterada a situação, e salva as informações caso tenha sido
                        if (hidSituacao.Value != ddlSituacao.SelectedValue || tbs372.EntityState == EntityState.Added)
                        {
                            //======================> Dados da Situação
                            tbs372.CO_SITUA = ddlSituacao.SelectedValue;
                            tbs372.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs372.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_COL_SITUA = coEmpColLogado;
                            tbs372.IP_SITUA = Request.UserHostAddress;
                            tbs372.DT_SITUA = DateTime.Now;
                        }

                        //Se estiver sendo cancelado
                        if (ddlSituacao.SelectedValue == "C")
                        {
                            tbs372.DE_OBSER_CANCE = (!string.IsNullOrEmpty(txtObsCancelamento.Text) ? txtObsCancelamento.Text : null);
                            tbs372.DT_CANCE = DateTime.Now;
                            tbs372.CO_COL_CANCE = LoginAuxili.CO_COL;
                            tbs372.CO_EMP_CANCE = LoginAuxili.CO_EMP;
                            tbs372.IP_CANCE = Request.UserHostAddress;
                        }
                        else //Se não estiver com o status de cancelado
                        {
                            tbs372.DE_OBSER_CANCE = null;
                            tbs372.DT_CANCE = (DateTime?)null;
                            tbs372.CO_COL_CANCE = (int?)null;
                            tbs372.CO_EMP_CANCE = (int?)null;
                            tbs372.IP_CANCE = null;
                        }

                        TBS372_AGEND_AVALI.SaveOrUpdate(tbs372);

                        #region Profissional

                        var tbs460 = tbs372.TBS460_AGEND_AVALI_PROFI.FirstOrDefault();
                        //var tbs460 = TBS460_AGEND_AVALI_PROFI.RetornaTodosRegistros().FirstOrDefault(x =>  && x.TBS372_AGEND_AVALI.ID_AGEND_AVALI == tbs372.ID_AGEND_AVALI);

                        if (tbs460 == null)
                        {
                            tbs460 = new TBS460_AGEND_AVALI_PROFI();
                        }

                        //foreach (var item in _tbs460)
                        //{
                        //    TBS460_AGEND_AVALI_PROFI.Delete(item, true);
                        //}

                        //foreach (GridViewRow row in grdMedicosAvali.Rows)
                        //{

                        tbs460.CO_COL_AVALI = int.Parse(_coCol);
                        tbs460.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs460.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs460.CO_EMP_AVALI = int.Parse(_coEmp);
                        tbs460.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs460.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs460.CO_SITUA = "A";
                        tbs460.DT_CADAS = DateTime.Now;
                        tbs460.DT_SITUA = DateTime.Now;
                        tbs460.IP_CADAS = Request.UserHostAddress;
                        tbs460.TBS372_AGEND_AVALI = tbs372;
                        TBS460_AGEND_AVALI_PROFI.SaveOrUpdate(tbs460);
                        //}
                        //}
                        //}
                        #endregion

                        #region Grava log de alteração de tipo caso necessário

                        //Se o tipo estiver diferente do de quando o registro foi carregado, e não estiver sendo feita uma inclusao
                        if ((hidTipo.Value != ddlTipo.SelectedValue) && (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)))
                        {
                            TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs380iii = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();
                            tbs380iii.TBS372_AGEND_AVALI = tbs372;

                            tbs380iii.FL_TIPO_AGENDA = ddlTipo.SelectedValue;

                            tbs380iii.FL_TIPO_LOG = "T"; //Tipo
                            tbs380iii.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs380iii.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs380iii.DT_CADAS = DateTime.Now;
                            tbs380iii.IP_CADAS = Request.UserHostAddress;
                            TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs380iii);
                        }

                        #endregion

                        //Se estiver sendo imposto o valor de cancelado, ou se estiver sendo removido
                        if ((ddlSituacao.SelectedValue == "C") || (hidSituacao.Value == "C"))
                        {
                            //Salva o log de alteração de status
                            TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs380 = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();
                            tbs380.TBS372_AGEND_AVALI = tbs372;

                            tbs380.DE_OBSER = (!string.IsNullOrEmpty(txtObsCancelamento.Text) ? txtObsCancelamento.Text : null);
                            tbs380.FL_JUSTI = "N";
                            tbs380.CO_SITUA_AGEND = (tbs372.CO_SITUA == "C" ? "C" : "A");

                            tbs380.FL_TIPO_LOG = "C";
                            tbs380.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs380.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs380.DT_CADAS = DateTime.Now;
                            tbs380.IP_CADAS = Request.UserHostAddress;
                            TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs380);
                        }

                        #region Limpa os itens já cadastrados antes de inserir os novos

                        var lstItens = TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros().Where(w => w.TBS372_AGEND_AVALI.ID_AGEND_AVALI == tbs372.ID_AGEND_AVALI).ToList();

                        //Exclui todas as associações dos itens
                        foreach (var i in lstItens)
                            TBS373_AGEND_AVALI_ITENS.Delete(i, true);

                        #endregion

                #endregion

                        //Os dados da Recepção na tbs367
                        #region Salva na tbs367

                        #region Trata sequencial
                        //Trata para gerar um Código do Encaminhamento
                        var res2 = (from tbs367pesq in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                                    select new { tbs367pesq.ID_RECEP_SOLIC, tbs367pesq.NU_REGIS_RECEP_SOLIC }).OrderByDescending(w => w.ID_RECEP_SOLIC).FirstOrDefault();

                        int teste = res2.ID_RECEP_SOLIC;

                        int seqConcat;
                        string seqcon;
                        string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                        string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                        seqConcat = res2.ID_RECEP_SOLIC + 1;
                        seqcon = seqConcat.ToString().PadLeft(6, '0');

                        string CodigoRecepcao = string.Format("RAP{0}{1}{2}", ano, mes, seqcon);
                        #endregion

                        #region Dados do Usuário Logado

                        //int coEmpColLogado = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

                        #endregion

                        #region Dados do Colaborador selecionado para análise

                        int? coEmpColAnalise = (LoginAuxili.CO_COL != null ? TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP : (int?)null);
                        int coAlu = tb07.CO_ALU;
                        int coResp = tb108.CO_RESP;

                        #endregion

                        TBS367_RECEP_SOLIC tbs367 = new TBS367_RECEP_SOLIC();

                        //======================> Dados Gerais
                        tbs367.TBS372_AGEND_AVALI = (tbs372.ID_AGEND_AVALI != null ? TBS372_AGEND_AVALI.RetornaPelaChavePrimaria(tbs372.ID_AGEND_AVALI) : null);
                        tbs367.NU_REGIS_RECEP_SOLIC = CodigoRecepcao;
                        tbs367.CO_EMP = LoginAuxili.CO_EMP;
                        tbs367.CO_ALU = coAlu;
                        tbs367.CO_RESP = coResp;

                        //======================> Dados do Cadastro
                        tbs367.DT_CADAS = DateTime.Now;
                        tbs367.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs367.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs367.CO_EMP_COL_CADAS = coEmpColLogado;
                        tbs367.IP_CADAS = Request.UserHostAddress;

                        //======================> Dados do Colaborador que irá analisar previamente
                        tbs367.CO_COL_ANALI = (LoginAuxili.CO_COL != null ? LoginAuxili.CO_COL : (int?)null);
                        tbs367.CO_EMP_COL_ANALI = (coEmpColAnalise.HasValue ? coEmpColAnalise.Value : (int?)null);

                        //======================> Dados da Situação
                        tbs367.CO_SITUA = "A";
                        tbs367.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs367.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs367.CO_EMP_COL_SITUA = coEmpColLogado;
                        tbs367.IP_SITUA = Request.UserHostAddress;
                        tbs367.DT_SITUA = DateTime.Now;

                        TBS367_RECEP_SOLIC.SaveOrUpdate(tbs367);

                        #endregion

                        //Percorre a grid de solicitações e persiste as informações
                        foreach (GridViewRow i in grdSolicitacoes.Rows)
                        {
                            #region Coleta os Dados
                            string ddlProc, txtQtde, ddlFormaContrato, hidValor;

                            ddlProc = ((DropDownList)i.Cells[2].FindControl("ddlProcedimento")).SelectedValue;
                            hidValor = ((HiddenField)i.Cells[2].FindControl("hidValUnitProc")).Value;
                            txtQtde = ((TextBox)i.Cells[3].FindControl("txtQtde")).Text;
                            ddlFormaContrato = ((DropDownList)i.Cells[4].FindControl("ddlTipoContrato")).SelectedValue;

                            //Persiste se tiver algum valor em qualquer um dos três 
                            if ((!string.IsNullOrEmpty(ddlProc) || (!string.IsNullOrEmpty(txtQtde)) || (!string.IsNullOrEmpty(ddlFormaContrato))))
                            {
                                //Salva objeto da entidade tbs373 que armazena os itens solicitados em uma recepção
                                #region Salva entidade tbs373

                                TBS373_AGEND_AVALI_ITENS tbs373 = new TBS373_AGEND_AVALI_ITENS();

                                //======================> Dados Gerais
                                tbs373.TBS372_AGEND_AVALI = tbs372;
                                tbs373.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProc) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc)) : null);
                                tbs373.VL_PROC_UNIT = (!string.IsNullOrEmpty(hidValor) ? Convert.ToDecimal(hidValor) : (decimal?)null);
                                tbs373.QT_SESSO = (!string.IsNullOrEmpty(txtQtde) ? int.Parse(txtQtde) : (int?)null);
                                tbs373.TP_CONTR = (!string.IsNullOrEmpty(ddlFormaContrato) ? ddlFormaContrato : null);

                                //======================> Dados do Cadastro
                                tbs373.DT_CADAS = DateTime.Now;
                                tbs373.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs373.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs373.CO_EMP_COL_CADAS = coEmpColLogado;
                                tbs373.IP_CADAS = Request.UserHostAddress;

                                //======================> Dados da Situação
                                tbs373.CO_SITUA = "A";
                                tbs373.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                                tbs373.CO_COL_SITUA = LoginAuxili.CO_COL;
                                tbs373.CO_EMP_COL_SITUA = coEmpColLogado;
                                tbs373.IP_SITUA = Request.UserHostAddress;
                                tbs373.DT_SITUA = DateTime.Now;
                                tbs373.TBS387_DEFIC = (!string.IsNullOrEmpty(ddlQueixa.SelectedValue) ? TBS387_DEFIC.RetornaPelaChavePrimaria(int.Parse(ddlQueixa.SelectedValue)) : null);
                                TBS373_AGEND_AVALI_ITENS.SaveOrUpdate(tbs373);

                                #endregion

                            }

                            #endregion
                        }
                        
                        //salva se o tipo for de consulta avaliação
                        if (ddlTipo.SelectedValue == "C")
                        {
                            SalvarAgendaHorario(tbs460, tbs372, tb07, int.Parse(locAtendimento));
                        }

                    }
                    else
                    {
                        int codigoCol = 0;
                        int.TryParse(_coCol, out codigoCol);

                        if (codigoCol != 0)
                        {
                            //exclui itens que existem na base mas não estão mais selecionados
                            var verificaAgendamento = lstTbs372.FirstOrDefault(p => p.CO_ALU == tb07.CO_ALU && p.TBS460_AGEND_AVALI_PROFI.Any(o => o.CO_COL_AVALI == codigoCol) && dtAgenda == p.DT_AGEND.Value);

                            if (verificaAgendamento != null)
                            {

                                verificaAgendamento.TBS367_RECEP_SOLIC.Load();
                                verificaAgendamento.TBS460_AGEND_AVALI_PROFI.Load();
                                verificaAgendamento.TBS380_LOG_ALTER_STATUS_AGEND_AVALI.Load();
                                var tbs460Item = verificaAgendamento.TBS460_AGEND_AVALI_PROFI.FirstOrDefault();

                                //obtem o registro da tbs174 referente a esse agendamento
                                var exclui174 = TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                    .FirstOrDefault(p => p.CO_COL == tbs460Item.CO_COL_AVALI && p.FL_CONSU_AVALIA == "S" && p.CO_ALU == tb07.CO_ALU && p.DT_AGEND_HORAR == verificaAgendamento.DT_AGEND && p.HR_AGEND_HORAR == verificaAgendamento.HR_AGEND);

                                var lstExclui367 = verificaAgendamento.TBS367_RECEP_SOLIC.ToList();
                                var lstExclui460 = verificaAgendamento.TBS460_AGEND_AVALI_PROFI.ToList();
                                var lstExclui380 = verificaAgendamento.TBS380_LOG_ALTER_STATUS_AGEND_AVALI.ToList();

                                for (int i = lstExclui367.Count - 1; i >= 0; i--)
                                {
                                    var item = lstExclui367[i];
                                    TBS367_RECEP_SOLIC.Delete(item, true);

                                }

                                for (int i = lstExclui460.Count - 1; i >= 0; i--)
                                {
                                    var item = lstExclui460[i];
                                    TBS460_AGEND_AVALI_PROFI.Delete(item, true);

                                }

                                for (int i = lstExclui380.Count - 1; i >= 0; i--)
                                {
                                    var item = lstExclui380[i];


                                    TBS380_LOG_ALTER_STATUS_AGEND_AVALI.Delete(item, true);

                                }

                                TBS372_AGEND_AVALI.Delete(verificaAgendamento, true);

                                TBS174_AGEND_HORAR.Delete(exclui174, true);
                            }
                        }
                    }



                }
                return true;
            }
            return false;
        }

        private void SalvarAgendaHorario(TBS460_AGEND_AVALI_PROFI tbs460, TBS372_AGEND_AVALI tbs372, TB07_ALUNO tb07, int localAtendimento)
        {

            var verificaAgend = TBS174_AGEND_HORAR.RetornaTodosRegistros()
               .FirstOrDefault(p => p.CO_COL == tbs460.CO_COL_AVALI && ((p.CO_AGEND_AVALI_PROFI == tbs460.ID_AGEND_AVALI_PROFI) || (p.DT_AGEND_HORAR == tbs372.DT_AGEND && p.HR_AGEND_HORAR == tbs372.HR_AGEND && !p.CO_ALU.HasValue)));

            var tb03 = TB03_COLABOR.RetornaPeloCoCol(tbs460.CO_COL_AVALI);

            var agend = new TBS174_AGEND_HORAR();

            //verificar se está vazio e é AV(FL_CONSU_AVALIA)
            if (verificaAgend != null)
            {
                agend = verificaAgend;
            }
            
            tbs372.TB251_PLANO_OPERAReference.Load();
            tbs372.TB250_OPERAReference.Load();

            agend.CO_ALU = tbs372.CO_ALU;
            agend.DT_AGEND_HORAR = (DateTime)tbs372.DT_AGEND;
            agend.HR_AGEND_HORAR = tbs372.HR_AGEND;
            agend.CO_EMP = tbs372.CO_EMP_CADAS;
            agend.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            agend.CO_COL_SITUA = LoginAuxili.CO_COL;
            agend.FL_AGEND_CONSU = "N";
            agend.FL_CONF_AGEND = "N";
            agend.FL_ENCAI_AGEND = "N";
            agend.CO_COL = tbs460.CO_COL_AVALI;
            agend.CO_SITUA_AGEND_HORAR = tbs372.CO_SITUA;
            agend.DT_SITUA_AGEND_HORAR = tbs372.DT_SITUA;
            agend.CO_EMP_ALU = tb07.CO_EMP;
            agend.FL_CONF_AGEND = "N";
            agend.FL_CONFIR_CONSUL_SMS = "N";
            agend.FL_CONSU_AVALIA = "S";
            agend.CO_DEPT = localAtendimento;
            agend.TB251_PLANO_OPERA = tbs372.TB251_PLANO_OPERA;
            agend.TB250_OPERA = tbs372.TB250_OPERA;
            agend.NU_PLAN_SAUDE = tb07.NU_PLANO_SAUDE;
            agend.CO_ESPEC = tb03.CO_ESPEC;
          
            agend.CO_AGEND_AVALI_PROFI = tbs460.ID_AGEND_AVALI_PROFI;
            TBS174_AGEND_HORAR.SaveOrUpdate(agend);

        }

        /// <summary>
        /// Carrega as funções simplificadas
        /// </summary>
        private void CarregarFuncoesSimp()
        {
            AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlFuncao, false);
        }
        /// <summary>
        /// Carrega as funções simplificadas
        /// </summary>
        private void CarregarDeficiencias()
        {
            AuxiliCarregamentos.CarregaDeficienciasNova(ddlDeficiencia, false);
            AuxiliCarregamentos.CarregaDeficienciasNova(ddlQueixa, false);

        }

        #region Classes de Saída

        /// <summary>
        /// Preenche a Grid de Consultas com os registros previamente cadastrados na funcionalidade e processo de registro de consulta.
        /// </summary>
        public class Consultas
        {
            public int ANTIGOS { get; set; }

            public string NO_COL { get; set; }
            public int CO_ALU { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
            public string NO_PAC_RECEB { get; set; }
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
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(CO_CLASS_PROFI, false);
                }
            }
            public int CO_ESPEC { get; set; }

            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
                }
            }
            public string CO_SITU { get; set; }
            public string CO_SITU_VALID
            {
                get
                {
                    string situacao = "";
                    switch (this.CO_SITU)
                    {
                        case "A":
                            situacao = "Aberto";
                            break;
                        case "C":
                            situacao = "Cancelado";
                            break;
                        case "I":
                            situacao = "Inativo";
                            break;
                        case "S":
                            situacao = "Suspenso";
                            break;
                    }

                    return situacao;
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
        }

        /// <summary>
        /// Classe de saída para a Grid de Médicos
        /// </summary>
        //public class MedicosPlantonistas
        //{
        //    public string NO_COL_V { get; set; }
        //    public string NO_COL
        //    {
        //        get
        //        {
        //            return (this.NO_COL_V.Length > 21 ? this.NO_COL_V.Substring(0, 21) + "..." : this.NO_COL_V);
        //        }
        //    }
        //    public int co_col { get; set; }
        //    public int co_emp_col_pla { get; set; }
        //    public string CO_CLASS_PROFI { get; set; }
        //    public string NO_CLASS_PROFI
        //    {
        //        get
        //        {
        //            return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_PROFI);
        //        }
        //    }
        //    public int? CO_ESPEC { get; set; }
        //    public string CO_TIPO_RISCO { get; set; }
        //    public string LOCAL { get; set; }
        //}

        #endregion

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
        /// Carrega as informações da unidade logada em campos definidos
        /// </summary>
        private void CarregaDadosUnidLogada()
        {
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            res.TB83_PARAMETROReference.Load();

            //Verifica se existe integração com o financeiro
            //if (res.TB83_PARAMETRO != null)
            //    chkRespFinanc.Visible = res.TB83_PARAMETRO.FL_INTEG_FINAN == "S" ? true : false;

            txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_EMP;
            txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_END_EMP;
        }

        /// <summary>
        /// Método responsável por receber os valores por parâmetros e inserir nos campos correspondentes
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="Nome"></param>
        /// <param name="sexo"></param>
        /// <param name="dtNasc"></param>
        /// <param name="RG"></param>
        /// <param name="ORGrg"></param>
        /// <param name="UFrg"></param>
        /// <param name="TelFixo"></param>
        /// <param name="TelCelu"></param>
        /// <param name="TelCome"></param>
        /// <param name="Whats"></param>
        /// <param name="Face"></param>
        /// <param name="CEP"></param>
        /// <param name="UF"></param>
        /// <param name="Cidade"></param>
        /// <param name="Bairro"></param>
        /// <param name="Logradouro"></param>
        /// <param name="Email"></param>
        private void CarregarDadosResponsavel(string cpf, string Nome, string sexo, DateTime dtNasc, string RG,
            string ORGrg, string UFrg, string TelFixo, string TelCelu, string TelCome, string Whats, string Face,
            string CEP, string UF, int Cidade, int? Bairro, string Logradouro, string Email)
        {
            txtCPFResp.Text = cpf;
            txtNomeResp.Text = Nome;
            ddlSexResp.SelectedValue = sexo;
            txtDtNascResp.Text = dtNasc.ToString();
            txtNuIDResp.Text = RG;
            txtOrgEmiss.Text = ORGrg;
            ddlUFOrgEmis.SelectedValue = UFrg;
            txtTelFixResp.Text = TelFixo;
            txtTelCelResp.Text = TelCelu;
            txtTelComResp.Text = TelCome;
            txtNuWhatsResp.Text = Whats;
            txtCEP.Text = CEP;
            ddlUF.SelectedValue = UF;
            carregaCidade();
            ddlCidade.SelectedValue = (Cidade != 0 ? Cidade.ToString() : "");
            carregaBairro();
            ddlBairro.SelectedValue = (Bairro != 0 && Cidade != 0 ? Bairro.ToString() : "");
            txtLograEndResp.Text = Logradouro;
            txtEmailResp.Text = Email;
        }

        /// <summary>
        /// Carrega as Informações de Responsável e Paciente, de acordo com o registro que é clicado na Grid de Pré-Atendimentos.
        /// </summary>
        /// <param name="ID_PRE_ATEND"></param>
        //private void CarregaCampos(int ID_AGENDA)
        //{
        //    var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGENDA);
        //    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
        //    tb07.TB108_RESPONSAVELReference.Load();
        //    var tb108 = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL : null);

        //    hidCoPac.Value = tbs174.CO_ALU.ToString();

        //    //Carrega essas informações apenas se o paciente da consulta tiver um responsável associado 
        //    if (tb108 != null)
        //    {
        //        tb108.TB904_CIDADEReference.Load();
        //        tb108.TB904_CIDADE1Reference.Load();

        //        txtCPFResp.Text = tb108.NU_CPF_RESP;
        //        txtNomeResp.Text = tb108.NO_RESP.ToUpper();
        //        txtDtNascResp.Text = tb108.DT_NASC_RESP.ToString();
        //        ddlSexResp.SelectedValue = tb108.CO_SEXO_RESP;
        //        txtTelFixResp.Text = tb108.NU_TELE_RESI_RESP;
        //        txtTelCelResp.Text = tb108.NU_TELE_CELU_RESP;

        //        CarregarDadosResponsavel(tb108.NU_CPF_RESP, tb108.NO_RESP.ToUpper(), tb108.CO_SEXO_RESP, tb108.DT_NASC_RESP.Value, tb108.CO_RG_RESP
        //                   , tb108.CO_ORG_RG_RESP, tb108.CO_ESTA_RG_RESP, tb108.NU_TELE_RESI_RESP, tb108.NU_TELE_CELU_RESP,
        //                   tb108.NU_TELE_COME_RESP, tb108.NU_TELE_WHATS_RESP, tb108.NM_FACEBOOK_RESP, tb108.CO_CEP_RESP,
        //                   tb108.CO_ESTA_RESP, (tb108.CO_CIDADE.HasValue ? tb108.CO_CIDADE.Value : 0), tb108.CO_BAIRRO, tb108.DE_ENDE_RESP, "");
        //    }
        //    else
        //    {
        //    }

        //    //Carrega as informações do Paciente
        //    txtnompac.Text = tb07.NO_ALU.ToUpper();
        //    txtCpfPaci.Text = tb07.NU_CPF_ALU;
        //    txtNuNisPaci.Text = tb07.NU_NIS.ToString().PadLeft(7, '0');
        //    txtTelResPaci.Text = tb07.NU_TELE_RESI_ALU;
        //    txtTelCelPaci.Text = tb07.NU_TELE_CELU_ALU;
        //    //chkPesqCPFUsu.Checked = true;
        //    txtDtNascPaci.Text = tb07.DT_NASC_ALU.ToString();
        //    ddlSexoPaci.SelectedValue = tb07.CO_SEXO_ALU;
        //    ddlGrParen.SelectedValue = tb07.CO_GRAU_PAREN_RESP;

        //    upImagemAluno.ImagemLargura = 70;
        //    upImagemAluno.ImagemAltura = 85;
        //    upImagemAluno.MostraProcurar = false;

        //    tb07.TB250_OPERAReference.Load();
        //    //if (tb07.TB250_OPERA != null)
        //    //    ddlOperPlano.SelectedValue = tb07.TB250_OPERA.ID_OPER.ToString();
        //    //else
        //    //    ddlOperPlano.SelectedValue = "";

        //    #region Instancia objeto da entidade para mostrar a foto correspondente

        //    string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
        //    var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
        //                 where tb07li.NU_CPF_ALU == cpfPac
        //                 select new { tb07li.CO_ALU }).FirstOrDefault();

        //    int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

        //    Decimal nis = 0;
        //    if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
        //    {
        //        nis = decimal.Parse(txtNuNisPaci.Text.Trim());
        //    }

        //    var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
        //                  where tb07ob.NU_NIS == nis
        //                  select new { tb07ob.CO_ALU }).FirstOrDefault();

        //    int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

        //    if ((!paExis.HasValue) && (!paExisNis.HasValue))
        //        upImagemAluno.CarregaImagem(0);
        //    else
        //    {
        //        int coPac = (paExis.HasValue ? paExis.Value : paExisNis.Value);
        //        var resupac = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
        //    }

        //    #endregion
        //}

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                //Se tiver cpf no paciente, carrega no responsável, se não tiver, deixa os 000...
                txtCPFResp.Text = txtCpfPaci.Text;
                txtNomeResp.Text = txtnompac.Text;
                txtDtNascResp.Text = txtDtNascPaci.Text;
                ddlSexResp.SelectedValue = ddlSexoPaci.SelectedValue;
                txtTelCelResp.Text = txtTelCelPaci.Text;
                txtTelFixResp.Text = txtTelResPaci.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailResp.Text = txtEmailPaci.Text;
                txtNuWhatsResp.Text = txtWhatsPaci.Text;

                PesquisaCarregaResp((int?)null, txtCPFResp.Text);

                //txtEmailPaci.Enabled = false;
                //txtCPFMOD.Enabled = false;
                //txtnompac.Enabled = false;
                //txtDtNascPaci.Enabled = false;
                //ddlSexoPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelResPaci.Enabled = false;
                //ddlGrParen.Enabled = false;
                //txtWhatsPaci.Enabled = false;
            }
            else
            {
                txtCPFResp.Text = "";
                txtNomeResp.Text = "";
                txtDtNascResp.Text = "01/01/1950";
                ddlSexResp.SelectedValue = "";
                txtTelCelResp.Text = "";
                txtTelFixResp.Text = "";
                txtEmailResp.Text = "";
                txtNuWhatsResp.Text = "";

                //txtCPFMOD.Enabled = true;
                //txtnompac.Enabled = true;
                //txtDtNascPaci.Enabled = true;
                //ddlSexoPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelResPaci.Enabled = true;
                //ddlGrParen.Enabled = true;
                //txtEmailPaci.Enabled = true;
                //txtWhatsPaci.Enabled = true;
                //hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp, string cpfRespParam = null)
        {
            string cpfResp = (string.IsNullOrEmpty(cpfRespParam) ?
                txtCPFResp.Text.Replace(".", "").Replace("-", "") : cpfRespParam.Replace(".", "").Replace("-", ""));

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select tb108).FirstOrDefault();

            if (res != null && (co_resp.HasValue || !String.IsNullOrEmpty(cpfResp)))
            {
                txtNomeResp.Text = res.NO_RESP.ToUpper();
                txtNumContResp.Text = res.NU_CONTROLE;
                if (res.FL_CPF_VALIDO == "S")
                    txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                //txtCEP.Text = res.CO_CEP_RESP;
                //ddlUF.SelectedValue = res.CO_ESTA_RESP;
                //carregaCidade();
                //ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                //carregaBairro();
                //ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                //txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();

                res.TBS366_FUNCAO_SIMPLReference.Load();
                if (res.TBS366_FUNCAO_SIMPL != null)
                    ddlFuncao.SelectedValue = res.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString();
            }
        }

        /// <summary>
        /// Carrega os dados do paciente de acordo com o id recebido
        /// </summary>
        private void CarregaDadosPaciente(int CO_ALU, int CO_RESP)
        {
            var res = TB07_ALUNO.RetornaPeloCoAlu(CO_ALU);

            if (res != null)
            {
                res.TBS379_RESTR_ATENDReference.Load();

                txtnompac.Text = res.NO_ALU.ToUpper();
                txtNuProntuario.Text = res.NU_NIRE.ToString();
                txtCpfPaci.Text = res.NU_CPF_ALU;
                txtNuNisPaci.Text = res.NU_NIS.ToString();
                txtDtNascPaci.Text = res.DT_NASC_ALU.ToString();
                ddlSexoPaci.SelectedValue = res.CO_SEXO_ALU;
                txtTelCelPaci.Text = res.NU_TELE_CELU_ALU;
                txtTelResPaci.Text = res.NU_TELE_RESI_ALU;
                ddlGrParen.SelectedValue = res.CO_GRAU_PAREN_RESP;
                txtEmailPaci.Text = res.NO_EMAIL_PAI;
                txtWhatsPaci.Text = res.NU_TELE_WHATS_ALU;
                hidCoPac.Value = res.CO_ALU.ToString();
                ddlEtniaAlu.SelectedValue = res.TP_RACA;
                drpLocacao.SelectedValue = res.FL_LIST_ESP;

                txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_ENDE_ALU;
                ddlUF.SelectedValue = res.CO_ESTA_ALU;
                txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_ALU;
                res.TB905_BAIRROReference.Load();

                carregaCidade();
                ddlCidade.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_BAIRRO.ToString() : "";

                res.TBS387_DEFICReference.Load();
                ddlDeficiencia.SelectedValue = res.TBS387_DEFIC != null ? Convert.ToString(res.TBS387_DEFIC.ID_DEFIC).ToString() : "";
                res.ImageReference.Load();
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                if (res.Image != null)
                    upImagemAluno.CarregaImagem(res.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);

                res.TB108_RESPONSAVELReference.Load();
                if (res.TB108_RESPONSAVEL != null && res.NU_CPF_ALU == res.TB108_RESPONSAVEL.NU_CPF_RESP && res.NO_ALU == res.TB108_RESPONSAVEL.NO_RESP)
                    chkPaciEhResp.Checked = true;

                //Se tiver restrição, mostra o label de restrição e esconde o outro
                if (res.TBS379_RESTR_ATEND != null)
                {
                    this.lblComRestricao.Visible = true;
                    this.lblSemRestricao.Visible = false;
                }
                else //Se não tiver restrição
                {
                    this.lblSemRestricao.Visible = true;
                    this.lblComRestricao.Visible = false;
                }

                PesquisaCarregaResp(CO_RESP);
            }
        }

        /// <summary>
        /// Pesquisa se já existe um Paciente com o CPF informado na tabela de Pacientes, se já existe ele preenche as informações do Paciente 
        /// </summary>
        private void PesquisaCarregaPaci()
        {
            string cpfPaci = (!string.IsNullOrEmpty(txtCpfPaci.Text) ? txtCpfPaci.Text.Replace(".", "").Replace("-", "") : string.Empty);
            decimal? nis = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
            int? prontuario = (!string.IsNullOrEmpty(txtNuProntuario.Text) ? int.Parse(txtNuProntuario.Text) : (int?)null);

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where (rdbPesqCPF.Checked ? tb07.NU_CPF_ALU == cpfPaci : 0 == 0)
                       && (rdbPesqNIS.Checked && nis.HasValue ? tb07.NU_NIS == nis.Value : 0 == 0)
                       && (rdbPesqProntuario.Checked && prontuario.HasValue ? tb07.NU_NIRE == prontuario : 0 == 0)
                       select tb07).FirstOrDefault();

            if (res != null)
            {
                txtnompac.Text = res.NO_ALU.ToUpper();
                txtCpfPaci.Text = res.NU_CPF_ALU;
                txtNuNisPaci.Text = res.NU_NIS.ToString();
                txtDtNascPaci.Text = res.DT_NASC_ALU.ToString();
                ddlSexoPaci.SelectedValue = res.CO_SEXO_ALU;
                txtTelCelPaci.Text = res.NU_TELE_CELU_ALU;
                txtTelResPaci.Text = res.NU_TELE_RESI_ALU;
                ddlGrParen.SelectedValue = res.CO_GRAU_PAREN_RESP;
                txtEmailPaci.Text = res.NO_EMAIL_PAI;
                txtWhatsPaci.Text = res.NU_TELE_WHATS_ALU;
                hidCoPac.Value = res.CO_ALU.ToString();
                ddlEtniaAlu.SelectedValue = res.TP_RACA;
                drpLocacao.SelectedValue = res.FL_LIST_ESP;

                txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_ENDE_ALU;
                ddlUF.SelectedValue = res.CO_ESTA_ALU;
                txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_ALU;
                res.TB905_BAIRROReference.Load();

                carregaCidade();
                ddlCidade.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_BAIRRO.ToString() : "";

                //if (res.TB905_BAIRRO != null)
                //{
                //    carregaCidade();
                //    res.TB905_BAIRRO.TB904_CIDADEReference.Load();
                //    if (res.TB905_BAIRRO.TB904_CIDADE != null)
                //    {
                //        ListItem it1 = ddlCidade.Items.FindByValue(res.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString());
                //        if (it1 != null)
                //            ddlCidade.SelectedValue = it1.Value;

                //        carregaBairro();
                //        ListItem it2 = ddlCidade.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString());
                //        if (it2 != null)
                //            ddlCidade.SelectedValue = it2.Value;
                //    }
                //}

                res.ImageReference.Load();
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                if (res.Image != null)
                    upImagemAluno.CarregaImagem(res.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);

                res.TB108_RESPONSAVELReference.Load();

                if (res.TB108_RESPONSAVEL != null)
                    PesquisaCarregaResp(res.TB108_RESPONSAVEL.CO_RESP);
            }
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        private void GridPreAtendimentoDesmarcada()
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_Select_Grid", "N");
            HttpContext.Current.Session.Remove("VL_Agenda_DMB");
            LimpaCampos();
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        //private void GridMedicosPlantonistasDesmarcada()
        //{
        //    HttpContext.Current.Session.Remove("CoCol");
        //    HttpContext.Current.Session.Remove("coEmp");

        //    //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
        //    HttpContext.Current.Session.Add("FL_Select_Grid_MEDIC", "N");
        //    HttpContext.Current.Session.Remove("VL_MEDIC");
        //}

        ///// <summary>
        ///// Métodos padrões à serem chamados quando uma linha da grid de pré-atendimento for selecionada
        ///// </summary>
        //private void GridMedicosPlantonistasSelecionada(string coEspec, int coColPlantonista, int coEmpColPlantonista)
        //{
        //    //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
        //    HttpContext.Current.Session.Add("FL_Select_Grid_MEDIC", "S");

        //    //Guarda o Valor do Médico Plantonista, para fins de posteriormente comparar este valor 
        //    HttpContext.Current.Session.Add("VL_MEDIC", coColPlantonista);

        //    HttpContext.Current.Session.Add("CoCol", coColPlantonista);
        //    HttpContext.Current.Session.Add("coEmp", coEmpColPlantonista);
        //}

        /// <summary>
        /// Limpa as informações de todos os campos
        /// </summary>
        private void LimpaCampos()
        {
            txtCPFResp.Text = txtNomeResp.Text = txtNuIDResp.Text = txtOrgEmiss.Text = ddlUFOrgEmis.SelectedValue =
                txtDtNascResp.Text = ddlSexResp.SelectedValue = txtCEP.Text = ddlCidade.SelectedValue
                = ddlBairro.SelectedValue = txtLograEndResp.Text = txtEmailResp.Text = txtTelCelResp.Text = txtTelFixResp.Text
                = txtNuNisPaci.Text = txtCpfPaci.Text = txtDtNascPaci.Text = ddlSexoPaci.SelectedValue
                = txtTelResPaci.Text = txtTelCelPaci.Text = ddlGrParen.SelectedValue = txtEmailPaci.Text
                = ddlUF.SelectedValue = txtnompac.Text = txtNuWhatsResp.Text = txtWhatsPaci.Text = txtDeFaceResp.Text = "";

            ExecutaJavaScript();
        }

        /// <summary>
        /// Executa método javascript que corrige algumas regras faltantes
        /// </summary>
        private void ExecutaJavaScript()
        {
            //ScriptManager.RegisterStartupScript(
            //    this.Page,
            //    this.GetType(),
            //    "Acao",
            //    "carregaPadroes();",
            //    true
            //);
        }

        /// <summary>
        /// Abre mensagem com informações
        /// </summary>
        private void AbreMensagemInfos(string texto)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + texto + "');", true);
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel2, GetType(), "newmsgE", "AbreMensagem('" + texto + "\');", true);
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
                        txtNuProntuario.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNuProntuario.Text = "1";
                }
            }
        }

        #region Carregamentos na Grid de Solicitações

        /// <summary>
        /// Carrega os grupos de procedimentos em ddl recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarGrupos(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddl, true);
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        private void CarregaSubGrupos(DropDownList ddlSubGrupo, DropDownList ddlGrupoProc)
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, ddlGrupoProc, true);
        }

        /// <summary>
        /// Percorre a grid de solicitações e totaliza os valores referentes
        /// </summary>
        private void CarregarValoresTotaisFooter()
        {
            decimal VlTotal = 0;
            decimal VlDesconto = 0;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                //Coleta os valores da linha
                string Valor, Desconto;
                Valor = ((TextBox)li.Cells[8].FindControl("txtVlUnitario")).Text;
                Desconto = ((TextBox)li.Cells[9].FindControl("txtVlDesconto")).Text;

                //Soma os valores com os valores das outras linhas da grid
                VlTotal += (!string.IsNullOrEmpty(Valor) ? decimal.Parse(Valor) : 0);
                VlDesconto += (!string.IsNullOrEmpty(Desconto) ? decimal.Parse(Desconto) : 0);
            }

            decimal vlLiquido = VlTotal - VlDesconto;
        }

        /// <summary>
        /// Carrega as Operadoras de saúde
        /// </summary>
        private void CarregarOperadoras(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true, true);
        }

        /// <summary>
        /// Carrega os planos de saúde da operadora recebida como parâmetro
        /// </summary>
        /// <param name="ddlPlan"></param>
        /// <param name="ddlOper"></param>
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, true, false, true);
        }

        /// <summary>
        /// Carrega as categorias dos planos de saúde
        /// </summary>
        /// <param name="ddlCateg"></param>
        /// <param name="ddlPlan"></param>
        private void CarregarCategoriasPlano(DropDownList ddlCateg, DropDownList ddlPlan)
        {
            AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCateg, ddlPlan, false, true, false, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarProcedimentosMedicos(DropDownList ddl, DropDownList ddlGrupo, DropDownList ddlSubGrupo)
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddl, ddlGrupo, ddlSubGrupo, 0, false, true, null, true);
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, int ddlOperPlano, int ddlPlano, HiddenField hidValorUnitario)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = ddlOperPlano;
            int idPlan = ddlPlano;

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                //if (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                //    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), idOper, idPlan);
                hidValorUnitario.Value = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGrid(int Index)
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "GRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SUBGRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TPCONTR";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                linha = dtV.NewRow();
                linha["GRUPO"] = ((DropDownList)li.Cells[0].FindControl("ddlGrupoProc")).SelectedValue;
                linha["SUBGRUPO"] = ((DropDownList)li.Cells[1].FindControl("ddlSubGrupo")).SelectedValue;
                linha["PROCEDIMENTO"] = ((DropDownList)li.Cells[2].FindControl("ddlProcedimento")).SelectedValue;
                linha["QTDE"] = ((TextBox)li.Cells[3].FindControl("txtQtde")).Text;
                linha["TPCONTR"] = ((DropDownList)li.Cells[4].FindControl("ddlTipoContrato")).SelectedValue;
                dtV.Rows.Add(linha);
            }

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_AgendAval"] = dtV;

            carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridSolicitacoes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "GRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SUBGRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TPCONTR";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                linha = dtV.NewRow();
                linha["GRUPO"] = ((DropDownList)li.Cells[0].FindControl("ddlGrupoProc")).SelectedValue;
                linha["SUBGRUPO"] = ((DropDownList)li.Cells[1].FindControl("ddlSubGrupo")).SelectedValue;
                linha["PROCEDIMENTO"] = ((DropDownList)li.Cells[2].FindControl("ddlProcedimento")).SelectedValue;
                linha["QTDE"] = ((TextBox)li.Cells[3].FindControl("txtQtde")).Text;
                linha["TPCONTR"] = ((DropDownList)li.Cells[4].FindControl("ddlTipoContrato")).SelectedValue;
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["GRUPO"] = "";
            linha["SUBGRUPO"] = "";
            linha["PROCEDIMENTO"] = "";
            linha["QTDE"] = "";
            linha["TPCONTR"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic_AgendAval"] = dtV;

            carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContexto()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_AgendAval"];

            grdSolicitacoes.DataSource = dtV;
            grdSolicitacoes.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlGrupo, ddlSubGrupo, ddlProc, ddlTpPgto;
                TextBox txtQtde;
                ddlGrupo = (DropDownList)li.Cells[0].FindControl("ddlGrupoProc");
                ddlSubGrupo = (DropDownList)li.Cells[1].FindControl("ddlSubGrupo");
                ddlProc = (DropDownList)li.Cells[2].FindControl("ddlProcedimento");
                txtQtde = (TextBox)li.Cells[3].FindControl("txtQtde");
                txtQtde = (TextBox)li.Cells[3].FindControl("txtQtde");
                ddlTpPgto = (DropDownList)li.Cells[4].FindControl("ddlTipoContrato");

                string Grupo, SubGrupo, Procedimento, nuQtde, tpPgto;

                //Coleta os valores do dtv
                Grupo = dtV.Rows[aux]["GRUPO"].ToString();
                SubGrupo = dtV.Rows[aux]["SUBGRUPO"].ToString();
                Procedimento = dtV.Rows[aux]["PROCEDIMENTO"].ToString();
                nuQtde = dtV.Rows[aux]["QTDE"].ToString();
                tpPgto = dtV.Rows[aux]["TPCONTR"].ToString();

                CarregarGrupos(ddlGrupo);
                ddlGrupo.SelectedValue = Grupo;

                CarregaSubGrupos(ddlSubGrupo, ddlGrupo);
                ddlSubGrupo.SelectedValue = SubGrupo;

                CarregarProcedimentosMedicos(ddlProc, ddlGrupo, ddlSubGrupo);
                ddlProc.SelectedValue = Procedimento;

                txtQtde.Text = nuQtde;

                ddlTpPgto.SelectedValue = tpPgto;

                aux++;
            }
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridSolicitacoes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "GRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SUBGRUPO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TPCONTR";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i <= 2)
            {
                linha = dtV.NewRow();
                linha["GRUPO"] = "";
                linha["SUBGRUPO"] = "";
                linha["PROCEDIMENTO"] = "";
                linha["QTDE"] = "";
                linha["TPCONTR"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridSolic_AgendAval", dtV);

            grdSolicitacoes.DataSource = dtV;
            grdSolicitacoes.DataBind();

            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlProc, ddlGrupoProc, ddlSubGrupo;
                ddlGrupoProc = (DropDownList)li.Cells[0].FindControl("ddlGrupoProc");
                ddlSubGrupo = (DropDownList)li.Cells[1].FindControl("ddlSubGrupo");
                ddlProc = (DropDownList)li.Cells[2].FindControl("ddlProcedimento");
                CarregarGrupos(ddlGrupoProc);
                CarregaSubGrupos(ddlSubGrupo, ddlGrupoProc);
                CarregarProcedimentosMedicos(ddlProc, ddlGrupoProc, ddlSubGrupo);
            }
        }

        #endregion

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

        /// Método responsável por carregar os médicos plantonistas na grid correspondente
        /// </summary>
        /// <param name="CO_ESPEC"></param>
        private void carregarGridMedicosAvali()
        {
            List<string> funcao = new List<string>();
            bool todos = true;

            if (chkOdonto.Checked)
            {
                funcao.Add("D");
                todos = false;
            }
            if (chkMed.Checked)
            {
                funcao.Add("M");
                todos = false;
            }
            if (chkPsico.Checked)
            {
                funcao.Add("P");
                todos = false;
            }
            if (chkFono.Checked)
            {
                funcao.Add("F");
                todos = false;
            }
            if (chkTeraOcup.Checked)
            {
                funcao.Add("T");
                todos = false;
            }
            if (chkFisio.Checked)
            {
                funcao.Add("I");
                todos = false;
            }

            string cpfPac = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();

            TB07_ALUNO pac = null;
            var tbs372 = new List<TBS372_AGEND_AVALI>();

            var dtAgenda = (!string.IsNullOrEmpty(txtDtAgenda.Text) ? DateTime.Parse(txtDtAgenda.Text) : (DateTime?)null);

            if (!string.IsNullOrEmpty(hidCoPac.Value))
            {
                pac = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value));

                int coAlu = 0;
                if (pac != null)
                {
                    coAlu = pac.CO_ALU;
                }

                tbs372 = TBS372_AGEND_AVALI.RetornaTodosRegistros().Where(p => dtAgenda == p.DT_AGEND.Value && p.CO_ALU == coAlu).ToList();
            }

            //TBS372_AGEND_AVALI tbs372 = RetornaEntidade();
            List<int> ids = new List<int>();
            List<int> coCol = new List<int>();
            var lst460 = new List<TBS460_AGEND_AVALI_PROFI>();
            var lstHorarios = new Dictionary<int, string>();
            var lstDepartamentos = new Dictionary<int, int>();

            if (tbs372 != null && tbs372.Count > 0)
            {
                foreach (var item in tbs372)
                {
                    item.TBS460_AGEND_AVALI_PROFI.Load();
                    var tbs460 = item.TBS460_AGEND_AVALI_PROFI.FirstOrDefault(); //TBS460_AGEND_AVALI_PROFI.RetornaTodosRegistros().FirstOrDefault(x => x.TBS372_AGEND_AVALI.ID_AGEND_AVALI == item.ID_AGEND_AVALI);
                    if (tbs460 != null)
                    {
                        var tbs174 = TBS174_AGEND_HORAR.RetornaTodosRegistros().FirstOrDefault(o => o.CO_COL == tbs460.CO_COL_AVALI && o.DT_AGEND_HORAR == item.DT_AGEND && o.HR_AGEND_HORAR.Trim() == item.HR_AGEND.Trim());

                        ids.Add(tbs460.ID_AGEND_AVALI_PROFI);
                        coCol.Add(tbs460.CO_COL_AVALI);
                        lst460.Add(tbs460);
                        var coColAval = tbs460.CO_COL_AVALI;
                        var horario = item.HR_AGEND;
                        var localConsulta = tbs174 != null ? tbs174.CO_DEPT : null;

                        ViewState["verificaProfHorario"] = true;

                        if (coColAval != 0)
                        {
                            if (!string.IsNullOrEmpty(horario) && !lstHorarios.ContainsKey(coColAval))
                            {
                                lstHorarios.Add(coColAval, horario);
                            }

                            if (localConsulta != null && !lstDepartamentos.ContainsKey(coColAval))
                            {
                                lstDepartamentos.Add(coColAval, (int)localConsulta);
                            }
                        }
                    }
                }
            }

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FL_PROFI_AVALI == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       && todos ? tb03.CO_CLASS_PROFI == tb03.CO_CLASS_PROFI : funcao.Contains(tb03.CO_CLASS_PROFI)
                       select new MedicosAvali
                       {
                           co_col = tb03.CO_COL,
                           check = coCol.Contains(tb03.CO_COL) ? true : false,
                           _NO_COL = tb03.NO_COL,
                           //colaborador = tb03.CO_COL,
                           _co_emp_col_pla = tb03.CO_EMP,
                           _CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           _CO_ESPEC = tb03.CO_ESPEC,
                           _TELEFONE = tb03.NU_TELE_CELU_COL,
                           LocalAtendimento = "0",
                           CO_DEPTO = "0"
                       }).OrderBy(w => w._NO_COL).ToList();

            foreach (var item in res)
            {
                item.Horario = (lstHorarios.Count > 0) ? lstHorarios.FirstOrDefault(o => o.Key == item.co_col).Value : string.Empty;
                item.LocalAtendimento = item.CO_DEPTO = (lstDepartamentos.Count > 0) ? lstDepartamentos.FirstOrDefault(o => o.Key == item.co_col).Value.ToString() : string.Empty;
            }

            if (res.Count > 0)
            {
                grdMedicosAvali.DataSource = res;
                grdMedicosAvali.DataBind();

                foreach (GridViewRow linha in grdMedicosAvali.Rows)
                {
                    bool chk = ((CheckBox)linha.Cells[0].FindControl("chkProfAvali")).Checked;
                    int _coCol = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);

                    DropDownList ddlTipoConsul = ((DropDownList)linha.Cells[5].FindControl("ddlLocalAtendimento"));

                    AuxiliCarregamentos.CarregaDepartamentos(ddlTipoConsul, LoginAuxili.CO_EMP, false);

                    if (chk)
                    {
                        ddlTipoConsul.SelectedValue = res.FirstOrDefault(p => p.co_col == _coCol).CO_DEPTO;
                    }

                }
                //grdMedicosAvali.DataSource = res;
                //grdMedicosAvali.DataBind();
            }
            else
            {
                grdMedicosAvali.DataSource = null;
                grdMedicosAvali.DataBind();
            }
        }

        /// <summary>
        /// Classe de saída para a Grid de Médicos
        /// </summary>
        public class MedicosAvali
        {
            public bool check { get; set; }
            public int co_col { get; set; }
            public string _NO_COL { get; set; }
            public string NO_COL
            {
                get
                {
                    return (this._NO_COL.Length > 60 ? this._NO_COL.Substring(0, 60) + "..." : this._NO_COL);
                }
            }
            //public int colaborador { get; set; }
            public int _co_emp_col_pla { get; set; }
            public string _CO_CLASS_PROFI { get; set; }
            public string CO_DEPTO { get; set; }
            public string _NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this._CO_CLASS_PROFI);
                }
            }
            public int? _CO_ESPEC { get; set; }
            public string _CO_TIPO_RISCO { get; set; }
            public string LocalAtendimento { get; set; }
            public string _TELEFONE { get; set; }
            public string _TELEFONE_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this._TELEFONE) ? AuxiliFormatoExibicao.PreparaTelefone(this._TELEFONE) : " - ");
                }
            }

            public string Horario { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void ddlGrupoProc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    DropDownList ddlGrupoProc = (DropDownList)linha.Cells[0].FindControl("ddlGrupoProc");
                    DropDownList ddlSubGrupo = (DropDownList)linha.Cells[1].FindControl("ddlSubGrupo");
                    DropDownList ddlProc = (DropDownList)linha.Cells[2].FindControl("ddlProcedimento");

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    if (ddlGrupoProc.ClientID == atual.ClientID)
                    {
                        CarregaSubGrupos(ddlSubGrupo, ddlGrupoProc);
                        CarregarProcedimentosMedicos(ddlProc, ddlGrupoProc, ddlSubGrupo);
                    }
                }
            }
        }

        protected void ddlSubGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    DropDownList ddlGrupoProc = (DropDownList)linha.Cells[0].FindControl("ddlGrupoProc");
                    DropDownList ddlSubGrupo = (DropDownList)linha.Cells[1].FindControl("ddlSubGrupo");
                    DropDownList ddlProc = (DropDownList)linha.Cells[2].FindControl("ddlProcedimento");

                    //Carrega os procedimentos de acordo com grupo e subgrupo
                    if (ddlSubGrupo.ClientID == atual.ClientID)
                        CarregarProcedimentosMedicos(ddlProc, ddlGrupoProc, ddlSubGrupo);
                }
            }
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlProc;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    ddlProc = (DropDownList)linha.Cells[2].FindControl("ddlProcedimento");
                    HiddenField hidValorUnitario = (HiddenField)linha.Cells[2].FindControl("hidValUnitProc");
                    //textbox que vai receber valor calculado

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    if (ddlProc.ClientID == atual.ClientID)
                        CalcularPreencherValoresTabelaECalculado(ddlProc, 0, 0, hidValorUnitario);
                }
            }
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ExecutaJavaScript();
            ddlCidade.Focus();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ExecutaJavaScript();

            ddlBairro.Focus();
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            chkPaciMoraCoResp.Checked = chkPaciEhResp.Checked;
        }

        protected void ChkProfAvali_OnCheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow linha in grdMedicosAvali.Rows)
            {
                bool chk = ((CheckBox)linha.Cells[0].FindControl("chkProfAvali")).Checked;

                if (chk)
                {
                    ViewState["verificaProfHorario"] = true;
                    break;
                }
            }

        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
            //ExecutaJavaScript();
        }

        protected void imbPesqPaci_OnClick(object sender, EventArgs e)
        {
            #region Validações

            //Se não houver nenhum tipo de pesquisa marcado
            if ((!rdbPesqCPF.Checked) && (!rdbPesqNIS.Checked) && (!rdbPesqProntuario.Checked))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar ao menos uma das opções de pesquisa!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por CPF mas nenhum tenha sido informado
            if ((rdbPesqCPF.Checked) && (string.IsNullOrEmpty(txtCpfPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por CPF mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por NIS mas nenhum tenha sido informado
            if ((rdbPesqNIS.Checked) && (string.IsNullOrEmpty(txtNuNisPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por NIS mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por PRONTUÁRIO mas nenhum tenha sido informado
            if ((rdbPesqProntuario.Checked) && (string.IsNullOrEmpty(txtNuProntuario.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por Prontuário mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            #endregion

            PesquisaCarregaPaci();
            //UpdatePanel2.Update();
            ExecutaJavaScript();
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
            ExecutaJavaScript();
        }

        protected void btnMaisLinhaChequePgto_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridSolicitacoes();
        }

        protected void btnMaisSolicitacoes_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridSolicitacoes();
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[5].FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGrid(aux);
        }

        protected void imgInfos_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[2].FindControl("imgInfos");

                    if (img.ClientID == atual.ClientID)
                    {
                        string vlUnit = ((HiddenField)linha.Cells[2].FindControl("hidValUnitProc")).Value;
                        string procSelec = ((DropDownList)linha.Cells[2].FindControl("ddlProcedimento")).SelectedValue;

                        if (!string.IsNullOrEmpty(procSelec))
                        {
                            int proc = int.Parse(procSelec);
                            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                       where tbs356.ID_PROC_MEDI_PROCE == proc
                                       select new
                                       {
                                           tbs356.DE_OBSE_PROC_MEDI,
                                           tbs356.NM_PROC_MEDI,
                                           tbs356.ID_PROC_MEDI_PROCE,
                                           tbs356.CO_PROC_MEDI,
                                       }).FirstOrDefault();

                            if (res != null)
                            {
                                txtCodProc.Text = res.CO_PROC_MEDI;
                                txtNomeProc.Text = res.NM_PROC_MEDI;
                                txtObservacaoProc.Text = res.DE_OBSE_PROC_MEDI;
                                txtVlUnitProc.Text = vlUnit;
                            }
                        }

                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalInfoProcedimento();",
                            true
                        );
                    }
                }
            }
        }

        protected void grdSolicitacoes_OnRowDeleting(object sender, EventArgs e)
        {

        }

        protected void ddlOper_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlPlan, ddlOper);
        }

        protected void ddlPlan_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarCategoriasPlano(ddlCateg, ddlPlan);
        }

        protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtDtAgenda.Text = DateTime.Now.ToString();
            txtDtAgenda.Enabled = true;
            //txtHrAgenda.Text = string.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);
            //txtHrAgenda.Enabled = true;

            if (ddlTipo.SelectedValue == "P")
            {
                lblQueixa.Text = "Local ";
                txtLocal.Visible = true;
                ddlQueixa.Visible = false;
            }
            else
            {
                lblQueixa.Text = "Queixa ";
                txtLocal.Visible = false;
                ddlQueixa.Visible = true;
            }
        }

        protected void ddlSituacao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSituacao.SelectedValue != hidSituacao.Value)
                txtDtSitua.Text = DateTime.Now.ToString();

            if (ddlSituacao.SelectedValue == "C")
            {
                ScriptManager.RegisterStartupScript(
                     this.Page,
                     this.GetType(),
                     "Acao",
                     "AbreModalCancelamento();",
                     true
                 );
            }
        }

        protected void lnkProfissionaisAtend_OnClick(object sender, EventArgs e)
        {
            if (grdMedicosAvali.Rows.Count <= 0)
            {
                carregarGridMedicosAvali();
            }

            AbreModalPadrao("AbreModalSelAvaliador();");
        }

        protected void imgPesqAgendaAtendimento_OnClick(object sender, EventArgs e)
        {
            carregarGridMedicosAvali();
            AbreModalPadrao("AbreModalSelAvaliador();");
        }

        #endregion
    }
}