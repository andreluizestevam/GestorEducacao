
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PGE - PGS
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//--------------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//--------------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR              | DESCRIÇÃO RESUMIDA
// -----------+-----------------------------------+-------------------------------------
// 01/07/2016 |  Tayguara Acioli     TA.01/07/2016|   Adicionei a pesquisa fonética
// 09/05/2017 | Guilherme Caixeta   TA.01/05/2017 |  Adição de campos e adequação dos campos a modal


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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroConsultaMed
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
                CarregaClassificacoes();
                CarregaUnidades(ddlUnidResCons);
                CarregaEspecialidade(ddlEspMedResCons);
                CarregaDepartamento();
                CarregaPacientes();
                CarregaGridProfi();
                txtDtIniResCons.Text = DateTime.Now.ToShortDateString();
                txtDtFimResCons.Text = DateTime.Now.AddDays(3).ToShortDateString();
                CarregaTiposConsulta(ddlTpCons, "", false);
                //Informações de recebimento
                carregaGridChequesPgto();
                CarregaBanco(ddlBcoPgto1);
                CarregaBanco(ddlBcoPgto2);
                CarregaBanco(ddlBcoPgto3);

                carregaBairro();
                carregaCidade();
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, null, false);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                ddlUFOrgEmis.Items.Insert(0, new ListItem("", ""));
                CarregarIndicacao();
                CarregaOperadoras();
                CarregaOperadorasPlano();
                CarregaPlanos();
                CarregaCategoria();
            }
        }

        protected void imgPesqPacAgenda_OnClick(object sender, EventArgs e)
        {
            var result = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                          where tb07.CO_SITU_ALU != "I"
                          && (tb07.CO_EMP == LoginAuxili.CO_EMP)
                          && (tb07.NO_ALU.Contains(txtPacAgenda.Text))
                          select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (result != null)
            {
                ddlPacAgenda.DataTextField = "NO_ALU";
                ddlPacAgenda.DataValueField = "CO_ALU";
                ddlPacAgenda.DataSource = result;
                ddlPacAgenda.DataBind();
            }

            ddlPacAgenda.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisaAgenda(true);

        }

        private void OcultarPesquisaAgenda(bool ocultar)
        {
            txtPacAgenda.Visible =
            imgPesqPacAgenda.Visible = !ocultar;
            ddlPacAgenda.Visible =
            imgVoltaPesqPacAgenda.Visible = ocultar;
        }

        protected void imgVoltaPesqPacAgenda_OnClick(object sender, EventArgs e)
        {
            ddlPacAgenda.DataSource = null;
            ddlPacAgenda.DataBind();
            OcultarPesquisaAgenda(false);
        }

        //TA.01/07/2016 início
        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var result = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                          where tb07.CO_SITU_ALU != "I"
                          && (tb07.CO_EMP == LoginAuxili.CO_EMP)
                          && (tb07.NO_ALU.Contains(ddlPaciente.Text))
                          select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (result != null && result.Count() > 0)
            {
                ddlNomeUsu.DataTextField = "NO_ALU";
                ddlNomeUsu.DataValueField = "CO_ALU";
                ddlNomeUsu.DataSource = result;
                ddlNomeUsu.DataBind();
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Paciente não cadastrado na base de dados.");
                return;
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
            ddlPaciente.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlNomeUsu.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }
        //TA.01/07/2016 fim
        protected void ButtonSalvarHoraio_Click(object sender, EventArgs e)
        {

        }
        protected void ddlPac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                var coAlu = int.Parse(ddlNomeUsu.SelectedValue);
                var paciente = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, LoginAuxili.CO_EMP);
                if (paciente != null)
                {
                    paciente.TB250_OPERAReference.Load();
                    paciente.TB251_PLANO_OPERAReference.Load();
                    try
                    {
                        ddlOperPlano.SelectedValue = paciente.TB250_OPERA != null ? paciente.TB250_OPERA.ID_OPER.ToString() : "";
                        CarregaPlanoSaude();
                        ddlPlano.SelectedValue = paciente.TB251_PLANO_OPERA != null ? paciente.TB251_PLANO_OPERA.ID_PLAN.ToString() : "";
                        CarregaProcedimentosConsulta();
                        txtNumeroCartPla.Text = paciente.NU_PLANO_SAUDE;
                    }
                    catch (Exception)
                    {
                        AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperPlano, false);
                        CarregaPlanoSaude();
                        CarregaProcedimentosConsulta();
                        txtNumeroCartPla.Text = "";
                    }
                }
            }
        }
        protected void ddlPacAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coAlu = !string.IsNullOrEmpty(ddlPacAgenda.SelectedValue) ? int.Parse(ddlPacAgenda.SelectedValue) : 0;
            carregarGridAgendaPac(coAlu);
        }
        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                if (ddlNomeUsu.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Paciente para quem será agendada a consulta.");
                    return;
                }

                if (ddlTpCons.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de consulta");
                    return;
                }

                bool SelecHorario = false;

                //Verifica se foi selecionado um horário para marcação da consulta
                foreach (GridViewRow li in grdHorario.Rows)
                {
                    if (SelecHorario == false)
                    {
                        if (((CheckBox)li.Cells[0].FindControl("ckSelectHr")).Checked)
                        {
                            SelecHorario = true;
                        }
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
                        }
                    }
                }

                //Valida a variável booleada criada anteriormente para verificar se foi selecionado um profissional
                if (SelecProfiss == false)
                {

                }

                //Valida a variável booleana criada anteriormente
                if (SelecHorario == false)
                {
                    throw new ArgumentException("Por favor selecionar um horário da agenda para o qual será feita a marcação da consulta.");

                }

                //Verifica se o usuário selecionou o tipo da consulta
                if (string.IsNullOrEmpty(ddlTpCons.SelectedValue))
                {
                    throw new ArgumentException("Por favor selecionar o Tipo da Consulta.");
                }

                //Verifica se o usuário selecionou o tipo da consulta
                if (string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                {
                    throw new ArgumentException("Por favor selecionar a Operadora.");
                }

                //Verifica se o usuário selecionou o tipo da consulta
                if (string.IsNullOrEmpty(ddlPlano.SelectedValue))
                {
                    throw new ArgumentException("Por favor selecionar o Plano.");
                }

                //Verifica se o usuário selecionou o tipo da consulta
                if (string.IsNullOrEmpty(ddlProcConsul.SelectedValue))
                {
                    throw new ArgumentException("Por favor selecionar o Procedimento.");
                }

                //É preciso para validar os campos da Aba de Forma de Pagamento
                #region Validação da Aba FORMA DE PAGAMENTO

                //Valida o Campo Dinheiro ----------------------------------------------------------
                decimal? vldiPgto = null;
                if (chkDinhePgto.Checked && txtValDinPgto.Text == "")
                {
                    throw new ArgumentException("O campo Dinheiro está marcado, favor informar o valor.");

                }
                else if (chkDinhePgto.Checked)
                    vldiPgto = decimal.Parse(txtValDinPgto.Text);


                //Valida o Campo Depósito ----------------------------------------------------------
                decimal? vlDepPgto = null;
                if (chkDepoPgto.Checked && txtValDepoPgto.Text == "")
                {
                    throw new ArgumentException("O campo Depósito está marcado, favor informar o valor.");

                }
                else if (chkDepoPgto.Checked)
                    vlDepPgto = decimal.Parse(txtValDepoPgto.Text);


                //Valida o Campo Débito em Conta ----------------------------------------------------------
                decimal? vlDebCPgto = null;
                //if ((chkDebConPgto.Checked) && (txtValDebConPgto.Text == "") || (txtQtMesesDebConPgto.Text == ""))
                //{
                //    if (string.IsNullOrEmpty(txtValDebConPgto.Text))
                //    {
                //        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Débito em Conta está marcado, favor informar o valor.");
                //        return;
                //    }
                //    else if ((string.IsNullOrEmpty(txtQtMesesDebConPgto.Text)))
                //    {
                //        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Débito em Conta está marcado, favor informar a Quantidade de Parcelas.");
                //        return;
                //    }
                //}
                //else if (chkDebConPgto.Checked)
                //    vlDebCPgto = decimal.Parse(txtValDebConPgto.Text);


                //Valida o Campo Transferência ----------------------------------------------------------
                decimal? vlTransPgto = null;
                if (chkTransPgto.Checked && txtValTransPgto.Text == "")
                {
                    throw new ArgumentException("O campo Trasnferência está marcado, favor informar o valor.");

                }
                else if (chkTransPgto.Checked)
                    vlTransPgto = decimal.Parse(txtValTransPgto.Text);


                //Valida o Campo Outros ---------------------------------------------------
                decimal? vloutPgto = null;
                if (chkOutrPgto.Checked && txtValOutPgto.Text == "")
                {
                    throw new ArgumentException("O campo Outros está marcado, favor informar o valor.");


                }
                else if (chkOutrPgto.Checked)
                    vloutPgto = decimal.Parse(txtValOutPgto.Text);


                //Valida a Marcação da opção Cartão de Crédito ---------------------------------------
                if ((chkCartaoCreditoPgto.Checked) && (ddlBandePgto1.SelectedValue == "N") && (ddlBandePgto2.SelectedValue == "N") && (ddlBandePgto3.SelectedValue == "N"))
                {
                    throw new ArgumentException("Você Selecionou a Forma de Pagamento Cartão de Crédito, é preciso preencher os campos condizentes");

                }

                //Valida a Marcação da opção Cartão de Débito ---------------------------------------
                if ((chkDebitPgto.Checked) && (ddlBcoPgto1.SelectedValue == "N") && (ddlBcoPgto2.SelectedValue == "N") && (ddlBcoPgto3.SelectedValue == "N"))
                {
                    throw new ArgumentException("Você Selecionou a Forma de Pagamento Cartão de Débito, é preciso preencher os campos condizentes");

                }


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region Valida as Informações do Pagamento em Cartão de Crédito

                //Valida as Informações do Pagamento em Cartão de Crédito
                if ((ddlBandePgto1.SelectedValue != "N") && ((txtNumPgto1.Text == "") || (txtTitulPgto1.Text == "") || (txtVencPgto1.Text == "") || (txtValCarPgto1.Text == "") || (txtQtParcCC1.Text == "")))
                {
                    throw new ArgumentException("Você Iniciou o preenchimento da Primeira linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                }

                //Valida as Informações do Pagamento em Cartão de Crédito
                if ((ddlBandePgto2.SelectedValue != "N") && ((txtNumPgto2.Text == "") || (txtTitulPgto2.Text == "") || (txtVencPgto2.Text == "") || (txtValCarPgto2.Text == "") || (txtQtParcCC2.Text == "")))
                {
                    throw new ArgumentException("Você Iniciou o preenchimento da Segunda linha em Informações do Cartão de Crédito, favor Informar todos os campos");

                }

                //Valida as Informações do Pagamento em Cartão de Crédito
                if ((ddlBandePgto3.SelectedValue != "N") && ((txtNumPgto3.Text == "") || (txtTitulPgto3.Text == "") || (txtVencPgto3.Text == "") || (txtValCarPgto3.Text == "") || (txtQtParcCC3.Text == "")))
                {
                    throw new ArgumentException("Você Iniciou o preenchimento da Terceira linha em Informações do Cartão de Crédito, favor Informar todos os campos");

                }

                #endregion


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region Valida as Informações do Pagamento em Cartão de Débito

                //Valida as Informações do Pagamento em Cartão de Débito
                if ((ddlBcoPgto1.SelectedValue != "N") && ((txtAgenPgto1.Text == "") || (txtNContPgto1.Text == "") || (txtNuDebtPgto1.Text == "") || (txtNuTitulDebitPgto1.Text == "") || (txtValDebitPgto1.Text == "")))
                {
                    throw new ArgumentException("Você Iniciou o preenchimento da Primeira linha em Informações do Cartão de Débito, favor Informar todos os campos");

                }

                //Valida as Informações do Pagamento em Cartão de Débito
                if ((ddlBcoPgto2.SelectedValue != "N") && ((txtAgenPgto2.Text == "") || (txtNContPgto2.Text == "") || (txtNuDebtPgto2.Text == "") || (txtNuTitulDebitPgto2.Text == "") || (txtValDebitPgto2.Text == "")))
                {
                    throw new ArgumentException("Você Iniciou o preenchimento da Segunda linha em Informações do Cartão de Débito, favor Informar todos os campos");

                }

                //Valida as Informações do Pagamento em Cartão de Débito
                if ((ddlBcoPgto3.SelectedValue != "N") && ((txtAgenPgto3.Text == "") || (txtNContPgto3.Text == "") || (txtNuDebtPgto3.Text == "") || (txtNuTitulDebitPgto3.Text == "") || (txtValDebitPgto3.Text == "")))
                {
                    throw new ArgumentException("Você Iniciou o preenchimento da Terceira linha em Informações do Cartão de Débito, favor Informar todos os campos");

                }

                #endregion


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region Valida as Informações do Pagamento em Cheque
                //Percorre a grid verificando o que está marcado, e vendo se a linha do registro correspondente está preenchida ou não               
                foreach (GridViewRow linhaPgto in grdChequesPgto.Rows)
                {
                    CheckBox chkGrdPgto = ((CheckBox)linhaPgto.Cells[0].FindControl("chkselectGridPgtoCheque"));

                    if (chkGrdPgto.Checked)
                    {
                        //Resgata os valores dos controles da GridView de Cheques
                        string vlBcoChe = ((DropDownList)linhaPgto.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue;
                        string vlAgeChe = ((TextBox)linhaPgto.Cells[2].FindControl("txtAgenChequePgto")).Text;
                        string vlConChe = ((TextBox)linhaPgto.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                        string vlNuChe = ((TextBox)linhaPgto.Cells[4].FindControl("txtNrChequePgto")).Text;
                        string vlNuCpf = ((TextBox)linhaPgto.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                        string vlNoTit = ((TextBox)linhaPgto.Cells[6].FindControl("txtTitulChequePgto")).Text;
                        decimal vlPgtChe = decimal.Parse(((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text);
                        DateTime dtVenChe = DateTime.Parse(((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text);

                        string vlPgtCheAux = ((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text;
                        string dtVenCheAux = ((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text;

                        if ((vlBcoChe == "N") || (vlAgeChe == "") || (vlConChe == "") || (vlNuChe == "") || (vlNuCpf == "") || (vlNoTit == "") || (vlPgtCheAux == "") || (dtVenCheAux == ""))
                        {
                            throw new ArgumentException("Você selecionou um registro de cheque, favor Preecher os campos restantes");

                        }
                        else
                            chkChequePgto.Checked = true;
                    }
                }

                #endregion

                #endregion

                //Percorre a Grid de Horários para alterar os registros agendando a marcação da consulta
                foreach (GridViewRow lis in grdHorario.Rows)
                {
                    //Verifica a linha que foi selecionada
                    if (((CheckBox)lis.Cells[0].FindControl("ckSelectHr")).Checked)
                    {
                        int coAgend = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoAgenda")).Value));
                        string tpConsul = ddlTpCons.SelectedValue;
                        int coAlu = int.Parse(ddlNomeUsu.SelectedValue);
                        int coEmpAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault().CO_EMP;
                        CheckBox chek = ((CheckBox)lis.Cells[5].FindControl("ckConf"));

                        //Retorna um objeto da tabela de Agenda de Consultas para persistí-lo novamente em seguida com os novos dados.
                        TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);
                        TB07_ALUNO tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault();

                        if (chkEnviaSms.Checked)
                        {
                            EnviaSMS((tbs174.CO_ALU != null ? false : true), tbs174.HR_AGEND_HORAR, tbs174.DT_AGEND_HORAR, tbs174.CO_COL.Value, (tbs174.CO_ESPEC.HasValue ? tbs174.CO_ESPEC.Value : 0), tbs174.CO_EMP.Value, tb07.NU_TELE_CELU_ALU, tb07.NO_ALU, tb07.CO_ALU);
                        }

                        if (tbs174.CO_ALU != coAlu && tbs174.CO_ALU > 0)
                        {

                            TBS174_AGEND_HORAR agend = new TBS174_AGEND_HORAR();
                            agend.DT_AGEND_HORAR = tbs174.DT_AGEND_HORAR;
                            agend.HR_AGEND_HORAR = tbs174.HR_AGEND_HORAR;
                            agend.CO_SITUA_AGEND_HORAR = tbs174.CO_SITUA_AGEND_HORAR;
                            agend.DT_SITUA_AGEND_HORAR = tbs174.DT_SITUA_AGEND_HORAR;
                            agend.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            agend.CO_COL_SITUA = LoginAuxili.CO_COL;
                            agend.FL_AGEND_CONSU = "N";
                            agend.FL_CONF_AGEND = "N";
                            agend.FL_ENCAI_AGEND = "N";
                            agend.CO_COL = tbs174.CO_COL;
                            agend.CO_EMP = tbs174.CO_EMP;
                            agend.CO_DEPT = agend.CO_DEPT;
                            agend.ID_DEPTO_LOCAL_RECEP = tbs174.ID_DEPTO_LOCAL_RECEP;
                            //agend.CO_ESPEC = coEsp;
                            agend.HR_DURACAO_AGENDA = tbs174.HR_DURACAO_AGENDA;
                            agend.HR_AGEND_HORAR = string.IsNullOrEmpty(TextBoxNovaHora.Text) ? tbs174.HR_AGEND_HORAR : TextBoxNovaHora.Text;
                            tbs174 = agend;

                        }

                        tbs174.CO_EMP_ALU = coEmpAlu;
                        tbs174.CO_ALU = coAlu;
                        tbs174.TP_CONSU = tpConsul;
                        tbs174.FL_CONF_AGEND = chek.Checked ? "S" : "N";
                        tbs174.FL_CONFIR_CONSUL_SMS = chkEnviaSms.Checked ? "S" : "N";
                        tbs174.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperPlano.SelectedValue)) : null);
                        tbs174.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlano.SelectedValue)) : null);
                        //tbs174.DT_VENC = (!string.IsNullOrEmpty(txtDtVenciPlan.Text) ? txtDtVenciPlan.Text : null);
                        tbs174.NU_PLAN_SAUDE = (!string.IsNullOrEmpty(txtNumeroCartPla.Text) ? txtNumeroCartPla.Text : null);
                        tbs174.TBS356_PROC_MEDIC_PROCE = string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? null : TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue));
                        //Informações de valores
                        tbs174.VL_CONSU_BASE = (!string.IsNullOrEmpty(txtVlBase.Text) ? decimal.Parse(txtVlBase.Text) : (decimal?)null);
                        tbs174.VL_DESCT = (!string.IsNullOrEmpty(txtVlDscto.Text) ? decimal.Parse(txtVlDscto.Text) : (decimal?)null);
                        tbs174.VL_CONSUL = (!string.IsNullOrEmpty(txtVlConsul.Text) ? decimal.Parse(txtVlConsul.Text) : (decimal?)null);

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

                        int coOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
                        int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);

                        tbs174.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue)) : null);

                        #endregion

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

                                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(i, true);

                                TBS386_ITENS_PLANE_AVALI.Delete(Tbs386, true);

                                TBS370_PLANE_AVALI.Delete(Tbs370, true);
                            }
                        }

                        tbs174.TBS370_PLANE_AVALI = RecuperaPlanejamento((tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coAlu);

                        TBS386_ITENS_PLANE_AVALI tbs386 = new TBS386_ITENS_PLANE_AVALI();
                        //Dados do cadastro
                        tbs386.DT_CADAS = DateTime.Now;
                        tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs386.IP_CADAS = Request.UserHostAddress;
                        //Dados da situação
                        tbs386.CO_SITUA = "A";
                        tbs386.DT_SITUA = DateTime.Now;
                        tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs386.IP_SITUA = Request.UserHostAddress;
                        tbs386.DE_RESUM_ACAO = null;

                        //Dados básicos do item de planejamento
                        tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coAlu);
                        tbs386.TBS356_PROC_MEDIC_PROCE = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue)) : null);
                        tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
                        tbs386.QT_SESSO = 1; //Conta quantos itens existem na lista para este mesmo e agenda
                        tbs386.DT_INICI = tbs174.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
                        tbs386.DT_FINAL = tbs174.DT_AGEND_HORAR; //Verifica qual a última data na lista
                        tbs386.FL_AGEND_FEITA_PLANE = "N";
                        tbs386.QT_PROCED = 1;
                        tbs386.ID_OPER = coOper;
                        tbs386.ID_PLAN = String.IsNullOrEmpty(ddlPlano.SelectedValue) ? (int?)null : (int?)int.Parse(ddlPlano.SelectedValue);
                        //tbs386.FL_CORTESIA = (chkCort.Checked ? "S" : "N");

                        //tbs386.VL_PROCED = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);

                        //Data prevista é a data do agendamento associado
                        tbs386.DT_AGEND = tbs174.DT_AGEND_HORAR;

                        TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386, true);

                        //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                        #region Associa o Item ao Agendamento
                        TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == tbs386.ID_ITENS_PLANE_AVALI).FirstOrDefault();
                        if (tbs389 == null)
                        {
                            tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();

                            tbs389.TBS174_AGEND_HORAR = tbs174;
                            tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                        }
                        TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);


                        //agend.CO_ALU = coPaci;                        


                        #endregion

                        TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);
                        CarregaOperadorasPlano();
                        CarregaPlanoSaude();
                        CarregaProcedimentosConsulta();
                        ddlTpCons.SelectedValue = "";
                        txtNumeroCartPla.Text = "";

                        #region Atualiza os dados da Forma de Pagamento

                        #region e

                        if (!string.IsNullOrEmpty(txtValDinPgto.Text))
                            chkDinhePgto.Checked = txtValDinPgto.Enabled = true;

                        if (!string.IsNullOrEmpty(txtValDebConPgto.Text))
                            chkDepoPgto.Checked = txtValDebConPgto.Enabled = true;

                        if ((!string.IsNullOrEmpty(txtValDebConPgto.Text)) && (!string.IsNullOrEmpty(txtQtMesesDebConPgto.Text)))
                            chkDebConPgto.Checked = txtValDebConPgto.Enabled = txtQtMesesDebConPgto.Enabled = true;

                        if (!string.IsNullOrEmpty(txtValTransPgto.Text))
                            chkTransPgto.Checked = txtValTransPgto.Enabled = true;

                        if (!string.IsNullOrEmpty(txtValOutPgto.Text))
                            chkOutrPgto.Checked = txtValOutPgto.Enabled = true;

                        if ((ddlBandePgto1.SelectedValue != "N") || (ddlBandePgto2.SelectedValue != "N") || (ddlBandePgto3.SelectedValue != "N"))
                            chkCartaoCreditoPgto.Checked = ddlBandePgto1.Enabled = ddlBandePgto2.Enabled = ddlBandePgto3.Enabled = true;

                        if ((ddlBcoPgto1.SelectedValue != "N") || (ddlBcoPgto2.SelectedValue != "N") || (ddlBcoPgto3.SelectedValue != "N"))
                            chkDebitPgto.Checked = ddlBcoPgto1.Enabled = ddlBcoPgto2.Enabled = ddlBcoPgto3.Enabled = true;

                        #endregion

                        if ((chkDinhePgto.Checked) || (chkDepoPgto.Checked) || (chkDebConPgto.Checked)
                            || (chkTransPgto.Checked) || (chkOutrPgto.Checked) || (chkCartaoCreditoPgto.Checked)
                            || (chkChequePgto.Checked) || (chkDebitPgto.Checked))
                        {
                            //Salva as informações sobre a forma de pagamento na tabela de tbs363 na TBS363_CONSUL_PAGTO
                            TBS363_CONSUL_PAGTO tbs363 = new TBS363_CONSUL_PAGTO();

                            tbs363.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(tbs174.ID_AGEND_HORAR);

                            //tbs363.QT_PARCE = (txtQtPgto.Text != "" ? int.Parse(txtQtPgto.Text) : (int?)null);
                            tbs363.FL_DINHE = (chkDinhePgto.Checked ? "S" : "N");
                            tbs363.VL_DINHE = (!string.IsNullOrEmpty(txtValDinPgto.Text) ? decimal.Parse(txtValDinPgto.Text) : (decimal?)null);
                            tbs363.FL_DEPOS = (chkDepoPgto.Checked ? "S" : "N");
                            tbs363.VL_DEPOS = (!string.IsNullOrEmpty(txtValDepoPgto.Text) ? decimal.Parse(txtValDepoPgto.Text) : (decimal?)null);
                            //tbs363.FL_DEBIT_CONTA = (chkDebConPgto.Checked ? "S" : "N");
                            //tbs363.VL_DEBIT_CONTA = (!string.IsNullOrEmpty(txtValDebConPgto.Text) ? decimal.Parse(txtValDebConPgto.Text) : (decimal?)null);
                            //tbs363.QT_PARCE_DEBIT_CONTA = (!string.IsNullOrEmpty(txtQtMesesDebConPgto.Text) ? int.Parse(txtQtMesesDebConPgto.Text) : (int?)null);
                            tbs363.FL_TRANS = (chkTransPgto.Checked ? "S" : "N");
                            tbs363.VL_TRANS = (!string.IsNullOrEmpty(txtValTransPgto.Text) ? decimal.Parse(txtValTransPgto.Text) : (decimal?)null);
                            tbs363.FL_OUTRO = (chkOutrPgto.Checked ? "S" : "N");
                            tbs363.VL_OUTRO = (!string.IsNullOrEmpty(txtValOutPgto.Text) ? decimal.Parse(txtValOutPgto.Text) : (decimal?)null);
                            //tbs363.DE_OUTRO = (!string.IsNullOrEmpty(txtObsOutPgto.Text) ? txtObsOutPgto.Text : null);
                            tbs363.FL_CARTA_CRED = (chkCartaoCreditoPgto.Checked ? "S" : "N");
                            tbs363.FL_CARTA_DEBI = (chkDebitPgto.Checked ? "S" : "N");
                            tbs363.FL_CHEQUE = (chkChequePgto.Checked ? "S" : "N");
                            //tbs363.QT_PARCE = (txtQtPgto.Text != "" ? int.Parse(txtQtPgto.Text) : (int?)null);
                            tbs363.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs363.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs363.DT_CADAS = DateTime.Now;
                            //tbe220.VL_DIFER_RECEB = ;

                            tbs363 = TBS363_CONSUL_PAGTO.SaveOrUpdate(tbs363);

                            //Salva as informações sobre o pagamento em cartão, quando este for selecionado na TBS363_CONSUL_PAGTO
                            //Salva as informações da parte de cartão de Crédito
                            if (ddlBandePgto1.SelectedValue != "N")
                            {
                                TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                                tbs364.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                tbs364.CO_BANDE = ddlBandePgto1.SelectedValue;
                                tbs364.CO_NUMER = txtNumPgto1.Text;
                                tbs364.NO_TITUL = txtTitulPgto1.Text;
                                tbs364.DT_VENCI = txtVencPgto1.Text;
                                tbs364.VL_PAGTO = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : (decimal?)null);
                                tbs364.QT_PARCE = (!string.IsNullOrEmpty(txtQtParcCC1.Text) ? int.Parse(txtQtParcCC1.Text) : (int?)null);
                                tbs364.FL_TIPO_TRANSAC = "C";

                                TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                            }

                            if (ddlBandePgto2.SelectedValue != "N")
                            {
                                TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                                tbs364.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                tbs364.CO_BANDE = ddlBandePgto2.SelectedValue;
                                tbs364.CO_NUMER = txtNumPgto2.Text;
                                tbs364.NO_TITUL = txtTitulPgto2.Text;
                                tbs364.DT_VENCI = txtVencPgto2.Text;
                                tbs364.VL_PAGTO = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : (decimal?)null);
                                tbs364.QT_PARCE = (!string.IsNullOrEmpty(txtQtParcCC2.Text) ? int.Parse(txtQtParcCC2.Text) : (int?)null);
                                tbs364.FL_TIPO_TRANSAC = "C";

                                TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                            }

                            if (ddlBandePgto3.SelectedValue != "N")
                            {
                                TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                                tbs364.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                tbs364.CO_BANDE = ddlBandePgto3.SelectedValue;
                                tbs364.CO_NUMER = txtNumPgto3.Text;
                                tbs364.NO_TITUL = txtTitulPgto3.Text;
                                tbs364.DT_VENCI = txtVencPgto3.Text;
                                tbs364.VL_PAGTO = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : (decimal?)null);
                                tbs364.QT_PARCE = (!string.IsNullOrEmpty(txtQtParcCC3.Text) ? int.Parse(txtQtParcCC3.Text) : (int?)null);
                                tbs364.FL_TIPO_TRANSAC = "C";

                                TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                            }

                            //Salva as informações da parte de cartão de Débito
                            if (ddlBcoPgto1.SelectedValue != "N")
                            {
                                TBS364_PAGTO_CARTAO tbs354 = new TBS364_PAGTO_CARTAO();

                                tbs354.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                //tbs354.CO_BCO = int.Parse(ddlBcoPgto1.SelectedValue);
                                tbs354.NR_AGENCI = txtAgenPgto1.Text;
                                tbs354.NR_CONTA = txtNContPgto1.Text;
                                tbs354.CO_NUMER = txtNuDebtPgto1.Text;
                                tbs354.NO_TITUL = txtNuTitulDebitPgto1.Text;
                                tbs354.VL_PAGTO = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : (decimal?)null);
                                tbs354.FL_TIPO_TRANSAC = "D";

                                TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs354, true);
                            }

                            if (ddlBcoPgto2.SelectedValue != "N")
                            {
                                TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                                tbs364.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                //tbs364.CO_BCO = int.Parse(ddlBcoPgto2.SelectedValue);
                                tbs364.NR_AGENCI = txtAgenPgto2.Text;
                                tbs364.NR_CONTA = txtNContPgto2.Text;
                                tbs364.CO_NUMER = txtNuDebtPgto2.Text;
                                tbs364.NO_TITUL = txtNuTitulDebitPgto2.Text;
                                tbs364.VL_PAGTO = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : (decimal?)null);
                                tbs364.FL_TIPO_TRANSAC = "D";

                                TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                            }

                            if (ddlBcoPgto3.SelectedValue != "N")
                            {
                                TBS364_PAGTO_CARTAO tbs364 = new TBS364_PAGTO_CARTAO();

                                tbs364.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                //tbs364.CO_BCO = int.Parse(ddlBcoPgto3.SelectedValue);
                                tbs364.NR_AGENCI = txtAgenPgto3.Text;
                                tbs364.NR_CONTA = txtNContPgto3.Text;
                                tbs364.CO_NUMER = txtNuDebtPgto3.Text;
                                tbs364.NO_TITUL = txtNuTitulDebitPgto3.Text;
                                tbs364.VL_PAGTO = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : (decimal?)null);
                                tbs364.FL_TIPO_TRANSAC = "D";

                                TBS364_PAGTO_CARTAO.SaveOrUpdate(tbs364, true);
                            }

                            //Faz o Cálculo da diferença para Alimentar a coluna VL_DIFER_RECEB
                            #region Cálculo do Valor diferença entre o valor pago e o valor do contrato

                            decimal valDinPg = (txtValDinPgto.Text != "" ? decimal.Parse(txtValDinPgto.Text) : decimal.Parse("0,00"));
                            decimal ValDepPg = (txtValDepoPgto.Text != "" ? decimal.Parse(txtValDepoPgto.Text) : decimal.Parse("0,00"));
                            decimal ValDebCPg = (txtValDebConPgto.Text != "" ? decimal.Parse(txtValDebConPgto.Text) : decimal.Parse("0,00"));
                            decimal valTrasPg = (txtValTransPgto.Text != "" ? decimal.Parse(txtValTransPgto.Text) : decimal.Parse("0,00"));
                            decimal valOutPg = (txtValOutPgto.Text != "" ? decimal.Parse(txtValOutPgto.Text) : decimal.Parse("0,00"));
                            decimal valCredPg1 = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : decimal.Parse("0,00"));
                            decimal valCredPg2 = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : decimal.Parse("0,00"));
                            decimal valCredPg3 = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : decimal.Parse("0,00"));
                            decimal valDebPg1 = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : decimal.Parse("0,00"));
                            decimal valDebPg2 = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : decimal.Parse("0,00"));
                            decimal valDebPg3 = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : decimal.Parse("0,00"));

                            decimal valTotalPgto = valDinPg + ValDepPg + ValDebCPg + valTrasPg + valOutPg + valCredPg1 + valCredPg2 + valCredPg3 + valDebPg1 + valDebPg2 + valDebPg3;


                            //Salva as informações sobre o pagamento em cheque, quando este for selecionado na TBE222_PAGTO_CHEQUE
                            foreach (GridViewRow linhaPgto in grdChequesPgto.Rows)
                            {
                                CheckBox chkGrdPgto = ((CheckBox)linhaPgto.Cells[0].FindControl("chkselectGridPgtoCheque"));

                                if (chkGrdPgto.Checked)
                                {
                                    //Resgata os valores dos controles da GridView de Cheques
                                    int vlBcoChe = int.Parse(((DropDownList)linhaPgto.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue);
                                    string vlAgeChe = ((TextBox)linhaPgto.Cells[2].FindControl("txtAgenChequePgto")).Text;
                                    string vlConChe = ((TextBox)linhaPgto.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                                    string vlNuChe = ((TextBox)linhaPgto.Cells[4].FindControl("txtNrChequePgto")).Text;
                                    string vlNuCpf = ((TextBox)linhaPgto.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                                    string vlNoTit = ((TextBox)linhaPgto.Cells[6].FindControl("txtTitulChequePgto")).Text;
                                    decimal vlPgtChe = decimal.Parse(((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text);
                                    DateTime dtVenChe = DateTime.Parse(((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text);

                                    TBS365_PAGTO_CHEQUE tbs365 = new TBS365_PAGTO_CHEQUE();

                                    tbs365.TBS363_CONSUL_PAGTO = TBS363_CONSUL_PAGTO.RetornaPelaChavePrimaria(tbs363.ID_CONSUL_PAGTO);
                                    //tbs365.CO_BCO = vlBcoChe;
                                    tbs365.NR_AGENCI = vlAgeChe;
                                    tbs365.NR_CONTA = vlConChe;
                                    tbs365.NR_CHEQUE = vlNuChe;
                                    tbs365.NR_CPF = vlNuCpf.Replace(".", "").Replace("-", "");
                                    tbs365.NO_TITUL = vlNoTit;
                                    tbs365.VL_PAGTO = vlPgtChe;
                                    tbs365.DT_VENC = dtVenChe;

                                    TBS365_PAGTO_CHEQUE.SaveOrUpdate(tbs365, true);

                                    //Soma o Valor pago em cheque no Valor Total
                                    valTotalPgto += vlPgtChe;
                                }
                            }

                            //Altera a coluna VL_DIFER_RECEB com a diferença calculada
                            //tbs363.VL_DIFER_RECEB = valTotalPgto - decimal.Parse(txtValContrPgto.Text);
                            TBS363_CONSUL_PAGTO.SaveOrUpdate(tbs363, true);

                            #endregion

                        }

                        #endregion

                        tb07.TB108_RESPONSAVELReference.Load();
                        int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);
                        int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);

                        if (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue))
                            GravaFinanceiroProcedimentos(TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcConsul.SelectedValue)),
                                tbs174.CO_ALU.Value, (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP : tb07.CO_ALU),
                                idPlan, idOper, tbs174.ID_AGEND_HORAR, tbs174.CO_COL.Value);

                    }
                }
                // AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento da Consulta realizado com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Agendamento da Consulta realizado com Sucesso!");
                CarregaGridHorariosAlter();
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioAvisoGeralSistema(this.Page, ex.Message);
            }
        }

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

                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                txtNisUsu.Text = res.NU_NIS.ToString();
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

                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                txtNisUsu.Text = res.NU_NIS.ToString();
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
            string noEspec = TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(w => w.CO_ESPECIALIDADE == CO_ESPEC).FirstOrDefault() != null ? TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(w => w.CO_ESPECIALIDADE == CO_ESPEC).FirstOrDefault().NO_ESPECIALIDADE : "";
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
        /// Carrega especialidades de acordo com a empresa selecionada.
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaEspecialidade(DropDownList ddl)
        {
            int coEmp = ddlUnidResCons.SelectedValue != "" ? int.Parse(ddlUnidResCons.SelectedValue) : 0;

            //Carrega apenas as especialidades que possuem algum colaborador associado
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb03.FLA_PROFESSOR == "S"
                        && (coEmp != 0 ? tb63.CO_EMP == coEmp : 0 == 0)
                       select new { tb63.NO_ESPECIALIDADE, tb63.CO_ESPECIALIDADE }).Distinct().OrderBy(w => w.NO_ESPECIALIDADE).ToList();

            ddl.DataTextField = "NO_ESPECIALIDADE";
            ddl.DataValueField = "CO_ESPECIALIDADE";
            ddl.DataSource = res;
            ddl.DataBind();

            if (res.Count() > 0)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Sem Especialidades com Plantonistas", ""));
        }

        /// <summary>
        /// Carrega Indicacao
        /// </summary>
        private void CarregarIndicacao()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlIndicacao, LoginAuxili.CO_EMP, false, "0", true, 0, false);
        }

        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false);
        }

        /// <summary>
        /// Carrega Planos
        /// </summary>
        private void CarregaPlanos()
        {
            string idOperadora = ddlOperadora.SelectedValue;
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlanoS, idOperadora, false);
        }

        /// <summary>
        /// Carrega Categoria
        /// </summary>
        private void CarregaCategoria()
        {
            AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoria, ddlPlanoS, false);
        }

        /// <summary>
        /// Carrega as unidades de acordo com a Instituição logada.
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaUnidades(DropDownList ddl)
        {
            //AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true);

            //Carrega apenas as unidades que possuem algum colaborador com FLAG de Profissional de Saúde
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
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
                ddl.Items.Insert(0, new ListItem("Sem Unidades com Plantonistas", ""));
        }

        /// <summary>
        /// Carrega as Classificações Profissionais
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassProfi, true);
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega os departamentos de acordo com a empresa selecionada.
        /// </summary>
        private void CarregaDepartamento()
        {
            int coEmp = (ddlUnidResCons.SelectedValue != "" ? int.Parse(ddlUnidResCons.SelectedValue) : 0);
            //AuxiliCarregamentos.CarregaDepartamentos(ddlDept, coEmp, true);
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                       where (coEmp > 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).Distinct().OrderBy(i => i.NO_DEPTO).ToList();

            ddlDept.DataTextField = "NO_DEPTO";
            ddlDept.DataValueField = "CO_DEPTO";
            ddlDept.DataSource = res;
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("Todos", "0"));
            ddlDept.SelectedValue = LoginAuxili.CO_DEPTO > 0 ? LoginAuxili.CO_DEPTO.ToString() : "0";
            if (LoginAuxili.CO_DEPTO > 0)
            {
                ddlUnidResCons.SelectedValue = LoginAuxili.CO_EMP.ToString();
            }
        }

        /// <summary>
        /// É chamado quando se clica em um registro na grid de horários que já possua Agendamento, é rensável por providenciar o carregamento do Paciente e Tipo de Consulta
        /// </summary>
        private void CarregaInfosAgenda(int CO_ALU, string TP_CONSUL)
        {
            if (CO_ALU != 0)
                ddlNomeUsu.SelectedValue = CO_ALU.ToString();

            if (TP_CONSUL != "")
                ddlTpCons.SelectedValue = TP_CONSUL;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_ALU == CO_ALU
                       select new { tb07.NU_NIS }).FirstOrDefault();
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

            if ((uf != "") && (cid != 0))
            {
                var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                           where tb905.CO_CIDADE == cid
                           && (tb905.CO_UF == uf)
                           select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO });

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataSource = res;
                ddlBairro.DataBind();

                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlBairro.Items.Clear();
                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
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
        /// Limpa cadastro de paciente
        /// </summary>
        private void LimparCadastroPaciente()
        {
            
        }

        /// <summary>
        /// é chamado quando se altera data de início ou final do parâmetro ou se clica em horários disponíveis
        /// </summary>
        private void CarregaGridHorariosAlter()
        {
            var marcado = false;
            foreach (GridViewRow li in grdProfi.Rows)
            {
                CheckBox chk = ((CheckBox)li.Cells[0].FindControl("ckSelect"));

                if (chk.Checked)
                {
                    marcado = true;
                    string coColSt = ((HiddenField)li.Cells[0].FindControl("hidCoCol")).Value;
                    var coCol = (!string.IsNullOrEmpty(coColSt) ? int.Parse(coColSt) : 0);

                    CarregaGridHorario(coCol, chkHorDispResCons.Checked);
                }
            }

            if (!marcado)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser selecionado um profissional para a pesquisa!");
        }

        /// <summary>
        /// Carrega grid de horários do profissional
        /// </summary>
        /// <param name="coCol"></param>
        private void CarregaGridHorarioProfi(int coCol)
        {
            DateTime dataAtu = DateTime.Now;
            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join b in TB250_OPERA.RetornaTodosRegistros() on a.TB250_OPERA.ID_OPER equals b.ID_OPER into b_join
                       from b in b_join.DefaultIfEmpty()
                       where a.CO_COL == coCol
                       && ((a.DT_AGEND_HORAR > dataAtu) || (a.DT_AGEND_HORAR == dataAtu))
                       select new HorarioSaida
                       {
                           dt = a.DT_AGEND_HORAR,
                           hr = a.HR_AGEND_HORAR,
                           CO_ALU = a.CO_ALU,
                           TP_CONSUL = a.TP_CONSU,
                           CO_SITU = a.CO_SITUA_AGEND_HORAR,
                           CO_AGEND = a.ID_AGEND_HORAR,
                           FL_CONF = a.FL_CONF_AGEND,
                           NO_OPERA = b.NM_SIGLA_OPER
                       });

            grdHorario.DataSource = res;
            grdHorario.DataBind();
        }

        /// <summary>
        /// Carrega as operadoras de plano de saúde
        /// </summary>
        private void CarregaOperadorasPlano()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperPlano, false);
        }

        /// <summary>
        /// Carrega os planos de saúde de determinada operadora
        /// </summary>
        private void CarregaPlanoSaude()
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperPlano.SelectedValue, false);
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o histórico de ocorrências
        /// </summary>
        private void abreModalInfosCadastrais()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosCadas();",
                true
            );
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o Registro de Informações Financeiras
        /// </summary>
        private void abreModalInfosFinanceiras()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosFinan();",
                true
            );

            //UpdFinanceiro.Update();
        }

        /// <summary>
        /// Carrega as datas de início e fim de consultas de um determinado profissional recebido como parâmetro
        /// </summary>
        private void CarregaDatasIniFim(int CO_COL)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where tbs174.CO_COL == CO_COL
                       select new
                       {
                           tbs174.DT_AGEND_HORAR,
                       }).ToList();

            //Seta a primeira e última data de consultas do colaborador recebido como parâmetro
            if (res.Count > 0)
            {
                txtDtIniResCons.Text = (res != null ? res.FirstOrDefault().DT_AGEND_HORAR.ToString() : DateTime.Now.ToString());
                txtDtFimResCons.Text = (res != null ? res.LastOrDefault().DT_AGEND_HORAR.ToString() : DateTime.Now.ToString());
            }
        }


        private void carregarGridAgendaPac(int coALu)
        {
            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where a.CO_ALU == coALu
                       select new HorarioSaida
                       {
                           dt = a.DT_AGEND_HORAR,
                           hr = a.HR_AGEND_HORAR,
                           CO_ALU = a.CO_ALU,
                           TP_CONSUL = a.TP_CONSU,
                           CO_SITU = a.CO_SITUA_AGEND_HORAR,
                           CO_AGEND = a.ID_AGEND_HORAR,
                           FL_CONF = a.FL_CONF_AGEND,
                           CO_COL = a.CO_COL,
                           CO_DEPTO = a.CO_DEPT,
                           CO_EMP = a.CO_EMP,
                           CO_ESPEC = a.CO_ESPEC,
                       });

            grdHorario.DataSource = res;
            grdHorario.DataBind();
        }

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private void CarregaGridHorario(int coCol, bool HorariosDisponiveis)
        {
            //CarregaDatasIniFim(coCol);
            if (String.IsNullOrEmpty(txtDtIniResCons.Text))
                txtDtIniResCons.Text = DateTime.Now.ToShortDateString();

            if (String.IsNullOrEmpty(txtDtFimResCons.Text))
                txtDtFimResCons.Text = DateTime.Now.AddDays(3).ToShortDateString();

            DateTime dtIni = DateTime.Parse(txtDtIniResCons.Text);
            DateTime dtFim = DateTime.Parse(txtDtFimResCons.Text);

            //Trata as datas para poder compará-las com as informações no banco
            string dataConver = dtIni.ToString("yyyy/MM/dd");
            DateTime dtInici = DateTime.Parse(dataConver);

            //Trata as datas para poder compará-las com as informações no banco
            string dataConverF = dtFim.ToString("yyyy/MM/dd");
            DateTime dtFimC = DateTime.Parse(dataConverF);

            //Pesquisa a agenda de consultas do colaborador filtrando as datas da funcionalidade
            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where a.CO_COL == coCol
                       && (HorariosDisponiveis ? a.CO_ALU == null : true)
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtInici) && EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFimC))
                       select new HorarioSaida
                       {
                           dt = a.DT_AGEND_HORAR,
                           hr = a.HR_AGEND_HORAR,
                           CO_ALU = a.CO_ALU,
                           TP_CONSUL = a.TP_CONSU,
                           CO_SITU = a.CO_SITUA_AGEND_HORAR,
                           CO_AGEND = a.ID_AGEND_HORAR,
                           FL_CONF = a.FL_CONF_AGEND,
                           CO_COL = a.CO_COL,
                           CO_DEPTO = a.CO_DEPT,
                           CO_EMP = a.CO_EMP,
                           CO_ESPEC = a.CO_ESPEC,
                       });

            grdHorario.DataSource = res;
            grdHorario.DataBind();
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

                #region Verifica se já existe

                string cpf = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                if (tb07 != null)
                    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

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

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        protected void CarregaBanco(DropDownList ddlBco)
        {
            AuxiliCarregamentos.CarregaBancos(ddlBco, false, false);
            ddlBco.Items.Insert(0, new ListItem("Nenhum", "N"));
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridChequesPgto()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BcoChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "AgenChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuConChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCpfChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "noTituChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "vlCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencChe";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdChequesPgto.Rows)
            {
                linha = dtV.NewRow();
                linha["BcoChe"] = ((DropDownList)li.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue;
                linha["AgenChe"] = ((TextBox)li.Cells[2].FindControl("txtAgenChequePgto")).Text;
                linha["nuConChe"] = ((TextBox)li.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                linha["nuCheChe"] = ((TextBox)li.Cells[4].FindControl("txtNrChequePgto")).Text;
                linha["nuCpfChe"] = ((TextBox)li.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                linha["noTituChe"] = ((TextBox)li.Cells[6].FindControl("txtTitulChequePgto")).Text;
                linha["vlCheChe"] = ((TextBox)li.Cells[7].FindControl("txtVlChequePgto")).Text;
                linha["dtVencChe"] = ((TextBox)li.Cells[8].FindControl("txtDtVencChequePgto")).Text;
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["BcoChe"] = "";
            linha["AgenChe"] = "";
            linha["nuConChe"] = "";
            linha["nuCheChe"] = "";
            linha["nuCpfChe"] = "";
            linha["noTituChe"] = "";
            linha["vlCheChe"] = "";
            linha["dtVencChe"] = "";
            dtV.Rows.Add(linha);

            Session["GridCheques"] = dtV;

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

            dtV = (DataTable)Session["GridCheques"];

            grdChequesPgto.DataSource = dtV;
            grdChequesPgto.DataBind();

            foreach (GridViewRow lipgtoaux in grdChequesPgto.Rows)
            {
                DropDownList ddlbcoauxChe = ((DropDownList)lipgtoaux.Cells[1].FindControl("ddlBcoChequePgto"));
                CarregaBanco(ddlbcoauxChe);
            }

        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridChequesPgto()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BcoChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "AgenChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuConChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCpfChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "noTituChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "vlCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencChe";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i <= 6)
            {
                linha = dtV.NewRow();
                linha["BcoChe"] = "";
                linha["AgenChe"] = "";
                linha["nuConChe"] = "";
                linha["nuCheChe"] = "";
                linha["nuCpfChe"] = "";
                linha["noTituChe"] = "";
                linha["vlCheChe"] = "";
                linha["dtVencChe"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridCheques", dtV);


            grdChequesPgto.DataSource = dtV;
            grdChequesPgto.DataBind();

            foreach (GridViewRow lipgtoaux in grdChequesPgto.Rows)
            {
                DropDownList ddlbcoauxChe = ((DropDownList)lipgtoaux.Cells[1].FindControl("ddlBcoChequePgto"));
                CarregaBanco(ddlbcoauxChe);
            }
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private void CalcularPreencherValoresTabelaECalculado(int ID_PROC, int ID_OPER, int ID_PLAN)
        {
            //Apenas se tiver sido escolhido algum procedimento
            if (ID_PROC != 0)
            {
                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos(ID_PROC, ID_OPER, ID_PLAN);
                txtVlBase.Text = ob.VL_BASE.ToString("N2"); // Insere o valor base 
                txtVlConsul.Text = txtVlConsulOriginal.Text = ob.VL_CALCULADO.ToString("N2");
                //UpdFinanceiro.Update();
            }
        }

        /// <summary>
        /// Método responsável por carregar os procedimentos que sejam consultas
        /// </summary>
        private void CarregaProcedimentosConsulta()
        {
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);

            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_SITU_PROC_MEDI == "A"
                       && (tbs356.TB250_OPERA.ID_OPER == idOper)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            if (res != null)
            {
                ddlProcConsul.DataTextField = "CO_PROC_MEDI";
                ddlProcConsul.DataValueField = "ID_PROC_MEDI_PROCE";
                ddlProcConsul.DataSource = res;
                ddlProcConsul.DataBind();
            }

            ddlProcConsul.Items.Insert(0, new ListItem("Selecione", ""));
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public class HorarioSaida
        {
            //Carrega informações gerais do agendamento
            public DateTime dt { get; set; }
            public string hr { get; set; }
            public string hora
            {
                get
                {
                    return string.Format("{0} - {1} - {2}", this.dt.ToString("dd/MM/yy"), UppercaseFirst((new System.Globalization.CultureInfo("pt-BR")).DateTimeFormat.GetDayName(dt.DayOfWeek).ToString().Substring(0, 3)), this.hr);
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
                    return (this.CO_ALU.HasValue ? TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU != null && w.CO_ALU == this.CO_ALU).FirstOrDefault().NO_ALU : " - ");
                }
            }
            public string NO_COL
            {
                get
                {
                    return (this.CO_COL.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL != null && w.CO_COL == this.CO_COL).FirstOrDefault().NO_APEL_COL : " - ");
                }
            }
            public int? CO_ALU { get; set; }
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
                        case "P":
                            tipo = "Procedimento";
                            break;
                        case "E":
                            tipo = "Exame";
                            break;
                        case "C":
                            tipo = "Cirúrgia";
                            break;
                        case "V":
                            tipo = "Vacina";
                            break;
                        default:
                            tipo = " - ";
                            break;
                    }
                    return tipo;
                }
            }
            public string NO_OPERA { get; set; }
        }

        /// <summary>
        /// Carrega a grid de profissionais da saúde
        /// </summary>
        private void CarregaGridProfi()
        {
            int coEsp = (!string.IsNullOrEmpty(ddlEspMedResCons.SelectedValue) ? int.Parse(ddlEspMedResCons.SelectedValue) : 0);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidResCons.SelectedValue) ? int.Parse(ddlUnidResCons.SelectedValue) : 0);
            int coDep = (!string.IsNullOrEmpty(ddlDept.SelectedValue) ? int.Parse(ddlDept.SelectedValue) : 0);
            string coClassProfi = ddlClassProfi.SelectedValue;

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
                                tb83.FL_PERM_AGEND_ESTET,
                                tb83.FL_PERM_AGEND_NUTRI,
                                tb83.FL_PERM_AGEND_OUTRO,
                                tb83.FL_PERM_AGEND_PSICO,
                            }).FirstOrDefault();

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb03.FLA_PROFESSOR == "S"
                       && (coEsp != 0 ? tb03.CO_ESPEC == coEsp : coEsp == 0)
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                       && (coDep != 0 ? tb03.CO_DEPTO == coDep : coDep == 0)
                       && (coClassProfi != "0" ? tb03.CO_CLASS_PROFI == coClassProfi : 0 == 0)
                       select new GrdProfiSaida
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL_RECEB = tb03.NO_COL,
                           NO_EMP = tb14.CO_SIGLA_DEPTO,
                           DE_ESP = tb63.NO_ESPECIALIDADE,
                           MATR_COL = tb03.CO_MAT_COL,
                           CO_CLASS = tb03.CO_CLASS_PROFI,
                       }).OrderBy(o => o.NO_COL_RECEB).ToList();

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
        }

        public class GrdProfiSaida
        {
            public string CO_CLASS { get; set; }
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
        }

        #endregion

        #region Eventos de componentes

        protected void ddlOperadora_CheckedChanged(object sender, EventArgs e)
        {
            CarregaPlanos();
            ddlPlanoS.Focus();
            abreModalInfosCadastrais();
        }

        protected void ddlPlano_CheckedChanged(object sender, EventArgs e)
        {
            CarregaCategoria();
            ddlCategoria.Focus();
            abreModalInfosCadastrais();
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
                        ddlPacAgenda.DataSource = null;
                        ddlPacAgenda.DataBind();
                        OcultarPesquisaAgenda(false);
                        string coCol = ((HiddenField)l.Cells[0].FindControl("hidCoCol")).Value;
                        int coColI = (!string.IsNullOrEmpty(coCol) ? int.Parse(coCol) : 0);
                        CarregaGridHorario(coColI, chkHorDispResCons.Checked);
                        //CarregaGridHorarioProfi(coColI);
                    }
                    else
                    {
                        grdHorario.DataSource = null;
                        grdHorario.DataBind();
                    }
                    //UpdHora.Update();
                }
            }
        }

        protected void ddlUnidResCons_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaEspecialidade(ddlEspMedResCons);
            CarregaDepartamento();
            CarregaGridProfi();
            LimparGridHorarios();
        }

        protected void ddlEspMedResCons_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
            LimparGridHorarios();
        }

        protected void ddlDept_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
            LimparGridHorarios();
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            if (chkPasta.Checked)
            {
                if (!string.IsNullOrEmpty(txtPasta.Text))
                {
                    ddlNomeUsu.Items.Clear();
                    var result = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                  where tb07.CO_SITU_ALU != "I"
                                  && (tb07.DE_PASTA_CONTR == txtPasta.Text)
                                  select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

                    if (result != null)
                    {
                        ddlNomeUsu.DataTextField = "NO_ALU";
                        ddlNomeUsu.DataValueField = "CO_ALU";
                        ddlNomeUsu.DataSource = result;
                        ddlNomeUsu.DataBind();
                    }
                    ddlNomeUsu.Items.Insert(0, new ListItem("Selecione", ""));

                    OcultarPesquisa(true);
                }
            }
            else
            {
                PesquisaCarregaResp(null);
                abreModalInfosCadastrais();
            }
        }

        protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtNirePaci.Enabled = true;
                txtPasta.Enabled = chkPasta.Checked = chkPasta.Enabled = chkPesqCpf.Enabled = chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = txtPasta.Text = "";
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
                txtPasta.Enabled = chkPasta.Checked = chkPasta.Enabled = chkPesqNire.Enabled = chkPesqNire.Checked = txtNirePaci.Enabled = false;
                txtNirePaci.Text = txtPasta.Text = "";
            }
            else
            {
                txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";
            }
        }

        protected void chkPasta_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtPasta.Enabled = true;
                txtCPFPaci.Enabled = chkPesqCpf.Enabled = chkPesqCpf.Checked = chkPesqNire.Enabled = chkPesqNire.Checked = txtNirePaci.Enabled = false;
                txtNirePaci.Text = txtCPFPaci.Text = "";
            }
            else
            {
                txtPasta.Enabled = false;
                txtPasta.Text = "";
            }
        }

        protected void ckSelectHr_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            bool selec = false;

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelectHr"));

                if (atual.ClientID != chk.ClientID)
                {
                    chk.Checked = false;
                }
                else if (atual.ClientID == chk.ClientID)
                {
                    //Coleta o tipo da consulta
                    string tpCon = ((HiddenField)l.Cells[0].FindControl("hidTpCons")).Value;

                    //Coleta e trata o código do paciente
                    string coAlu = ((HiddenField)l.Cells[0].FindControl("hidCoAlu")).Value;
                    int coAluI = (!string.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0);

                    string coAgen = ((HiddenField)l.Cells[0].FindControl("hidCoAgenda")).Value;
                    int coAgenI = (!string.IsNullOrEmpty(coAgen) ? int.Parse(coAgen) : 0);



                    //CarregaInfoPaciente(coAluI);
                    var agenda = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgenI);
                    if (string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                    {
                        if (agenda.CO_ALU != null)
                        {
                            agenda.TB250_OPERAReference.Load();
                            agenda.TB251_PLANO_OPERAReference.Load();
                            agenda.TBS356_PROC_MEDIC_PROCEReference.Load();
                            ddlOperPlano.SelectedValue = agenda.TB250_OPERA != null ? agenda.TB250_OPERA.ID_OPER.ToString() : "";
                            CarregaPlanoSaude();
                            ddlPlano.SelectedValue = agenda.TB251_PLANO_OPERA != null ? agenda.TB251_PLANO_OPERA.ID_PLAN.ToString() : "";
                            CarregaProcedimentosConsulta();
                            txtNumeroCartPla.Text = agenda.NU_PLAN_SAUDE;
                            ddlProcConsul.SelectedValue = agenda.TBS356_PROC_MEDIC_PROCE != null ? agenda.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE.ToString() : "";
                        }
                        else
                        {
                            CarregaOperadorasPlano();
                            CarregaPlanoSaude();
                            CarregaProcedimentosConsulta();
                            txtNumeroCartPla.Text = "";
                        }
                        CarregaInfosAgenda(coAluI, tpCon);
                    }

                    if (chk.Checked)
                    {
                        string coAgend = ((HiddenField)l.Cells[0].FindControl("hidCoAgenda")).Value;
                        hidCoConsul.Value = coAgend.ToString();

                        if (coAluI != 0)
                        {
                            TextBoxNovaHora.Text = agenda.HR_AGEND_HORAR;
                            ScriptManager.RegisterStartupScript(
                               this.Page,
                               this.GetType(),
                               "Acao",
                               "AbreModalHoraAgenda();",
                               true
                           );
                            //Marca que foi selecionado para que os outros itens não desabilitem o lnk.
                            selec = true;
                            //lnkImpriGuiaMed.Enabled = true;
                        }
                    }
                    else
                    {
                        if (!selec)
                        {
                            //lnkImpriGuiaMed.Enabled = false;
                            hidCoConsul.Value = "";
                        }
                    }
                }
                else
                {
                    //Desabilita o lnk caso não tenha sido selecionado nenhum item anteriormente
                    if (!selec)
                    {
                        //lnkImpriGuiaMed.Enabled = false;
                        hidCoConsul.Value = "";
                    }
                }
            }

            //UpdHora.Update();
        }


        protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        {
            CarregaGridHorariosAlter();
        }

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            //updCadasUsuario.Update();
            abreModalInfosCadastrais();
        }

        protected void lnkInfosFinanc_OnClick(object sender, EventArgs e)
        {
            txtNmProcedimento.Text = !string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? ddlProcConsul.SelectedItem.Text : "";
            txtNmPacienteMODFinan.Text = !string.IsNullOrEmpty(ddlNomeUsu.SelectedValue) ? ddlNomeUsu.SelectedItem.Text : "";

            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);
            CalcularPreencherValoresTabelaECalculado(idProc, idOper, idPlan);

            abreModalInfosFinanceiras();
            //UpdFinanceiro.Update();
        }

        protected void lnkImpriGuiaMed_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidCoConsul.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um agendamento para imprimir a guia");
                return;
            }

            string infos;

            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);


            RptGuiaConsulta fpcb = new RptGuiaConsulta();
            lRetorno = fpcb.InitReport(infos, coEmp, int.Parse(hidCoConsul.Value));
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            string strURL = String.Format("{0}", Session["URLRelatorio"].ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "newpageE", "customOpen('" + strURL + "\');", true);

            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        protected void lnkSalvarPaciente_OnClick(object sender, EventArgs e)
        {
            TB07_ALUNO tb07 = new TB07_ALUNO();

            //tb07.NO_ALU = txtNomeAluMod.Text;
            //tb07.NU_CPF_ALU = txtCpfAluMod.Text.Replace(".", "").Replace("-", "").Trim();
            //tb07.NU_NIS = (!string.IsNullOrEmpty(txtNisAluMod.Text) ? decimal.Parse(txtNisAluMod.Text) : (decimal?)null);
            //tb07.DT_NASC_ALU = DateTime.Parse(txtDataNascimentoAluMod.Text);
            //tb07.CO_SEXO_ALU = ddlSexoAluMod.SelectedValue;
            ////tb07.NU_TELE_CELU_ALU = txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            ////tb07.NU_TELE_RESI_ALU = txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            ////tb07.CO_GRAU_PAREN_RESP = ddlGrauParen.SelectedValue;
            //tb07.CO_EMP = LoginAuxili.CO_EMP;
            //tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            //tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            ////tb07.TB108_RESPONSAVEL = (tb108 != null ? tb108 : null);

            ////Salva os valores para os campos not null da tabela de Usuário
            //tb07.CO_SITU_ALU = "A";
            //tb07.TP_DEF = "N";

            //#region trata para criação do nire

            //var resNire = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
            //               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

            //int nir = 0;
            //if (resNire == null)
            //{
            //    nir = 1;
            //}
            //else
            //{
            //    nir = resNire.NU_NIRE;
            //}

            //int nirTot = nir + 1;

            //#endregion
            //tb07.NU_NIRE = nirTot;

            TB07_ALUNO.SaveOrUpdate(tb07, true);
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;

            abreModalInfosCadastrais();
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
            abreModalInfosCadastrais();
            //updCadasUsuario.Update();
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
            abreModalInfosCadastrais();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
            abreModalInfosCadastrais();
        }

        protected void lnkSalvar_OnClick(object sender, EventArgs e)
        {
            abreModalInfosCadastrais();
            if (string.IsNullOrEmpty(txtNuNisPaci.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nis do Paciente é requerido");
                txtNuNisPaci.Focus();
                //updCadasUsuario.Update();
                return;
            }

            //Salva os dados do Responsável na tabela 108
            #region Salva Responsável na tb108

            TB108_RESPONSAVEL tb108;
            //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
            if (string.IsNullOrEmpty(hidCoResp.Value))
            {
                tb108 = new TB108_RESPONSAVEL();

                tb108.NO_RESP = txtNomeResp.Text;
                tb108.NU_CPF_RESP = txtCPFResp.Text.Replace("-", "").Replace(".", "").Trim();
                tb108.CO_RG_RESP = txtNuIDResp.Text;
                tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                tb108.DT_NASC_RESP = (!string.IsNullOrEmpty(txtDtNascResp.Text) ? DateTime.Parse(txtDtNascResp.Text) : DateTime.MinValue);
                tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                tb108.CO_CEP_RESP = txtCEP.Text;
                tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                tb108.CO_CIDADE = (!string.IsNullOrEmpty(ddlCidade.SelectedValue) ? int.Parse(ddlCidade.SelectedValue) : 0);
                tb108.CO_BAIRRO = (!string.IsNullOrEmpty(ddlCidade.SelectedValue) ? int.Parse(ddlBairro.SelectedValue) : 0);
                tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.CO_ORIGEM_RESP = "NN";
                tb108.CO_SITU_RESP = "A";

                //Atribui valores vazios para os campos not null da tabela de Responsável.
                tb108.FL_NEGAT_CHEQUE = "V";
                tb108.FL_NEGAT_SERASA = "V";
                tb108.FL_NEGAT_SPC = "V";
                tb108.CO_INST = 0;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
            }
            else
            {
                //Busca em um campo na página, que é preenchido quando se pesquisa um responsável, o CO_RESP, usado pra instanciar um objeto da entidade do responsável em questão.
                if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Responsável para dar continuidade no encaminhamento.");
                    return;
                }

                int coRe = int.Parse(hidCoResp.Value);
                tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe);
            }

            #endregion

            //Salva os dados do Usuário em um registro na tb07
            #region Salva o Usuário na TB07

            ////Verifica antes se já existe o paciente algum paciente com o mesmo CPF e NIS informados nos campos, caso não exista, cria um novo
            //string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
            //var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
            //             where tb07li.NU_CPF_ALU == cpfPac
            //             select new { tb07li.CO_ALU }).FirstOrDefault();

            //int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

            //Decimal nis = 0;
            //if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
            //{
            //    nis = decimal.Parse(txtNuNisPaci.Text.Trim());
            //}

            //var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
            //              where tb07ob.NU_NIS == nis
            //              select new { tb07ob.CO_ALU }).FirstOrDefault();

            //int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

            TB07_ALUNO tb07;
            //if ((!paExis.HasValue) && (!paExisNis.HasValue))
            //{
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                tb07 = new TB07_ALUNO();

                #region Bloco foto
                int codImagem = upImagemAluno.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion

                tb07.NO_ALU = txtnompac.Text;
                tb07.NU_CPF_ALU = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();
                tb07.NU_NIS = decimal.Parse(txtNuNisPaci.Text);
                tb07.DT_NASC_ALU = (!string.IsNullOrEmpty(txtDtNascPaci.Text) ? DateTime.Parse(txtDtNascPaci.Text) : DateTime.MinValue);
                tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                tb07.CO_EMP = LoginAuxili.CO_EMP;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);
                tb07.NO_APE_ALU = txtApelido.Text.ToUpper();
                /*Tabela recebe valor em branco por não receber nulos*/
                tb07.FL_LIST_ESP = "";
                if (chkPaciMoraCoResp.Checked)
                {
                    tb07.CO_CEP_ALU = txtCEP.Text;
                    tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                    tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                }

                //Salva os valores para os campos not null da tabela de Usuário
                tb07.CO_SITU_ALU = "A";
                tb07.TP_DEF = "N";

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
            {
                //if (string.IsNullOrEmpty(hidCoPac.Value))
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Paciente para dar continuidade no encaminhamento.");
                //    return;
                //}

                //Busca em um campo na página, que é preenchido quando se pesquisa um Paciente, o CO_ALU, usado pra instanciar um objeto da entidade do Paciente em questão.
                int coPac = int.Parse(hidCoPac.Value);
                tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
            }

            //Plano de Saúde
            tb07.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue)) : null);

            tb07.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlano.SelectedValue)) : null);

            tb07.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCategoria.SelectedValue) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCategoria.SelectedValue)) : null);

            tb07.NU_PLANO_SAUDE = (!string.IsNullOrEmpty(txtNumeroPlano.Text) ? txtNumeroPlano.Text : null);

            divResp.Visible = false;
            divSuccessoMessage.Visible = true;
            lblMsg.Text = "Usuário salvo com êxito!";
            lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
            lblMsg.Visible = true;
            lblMsgAviso.Visible = true;

            CarregaPacientes();
            ddlNomeUsu.SelectedValue = tb07.CO_ALU.ToString();
            //updTopo.Update();

            #endregion
        }

        protected void ddlOperPlano_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanoSaude();
            CarregaProcedimentosConsulta();
        }

        protected void btnMaisLinhaChequePgto_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridChequesPgto();
            abreModalInfosFinanceiras();
        }

        protected void lnkEfetivaFinanceiro_OnClick(object sender, EventArgs e)
        {

        }

        protected void chkChequePgto_OnCheckedChanged(object sender, EventArgs e)
        {
            grdChequesPgto.Enabled = chkChequePgto.Checked;
            abreModalInfosFinanceiras();
        }

        protected void ddlProcConsul_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);
            CalcularPreencherValoresTabelaECalculado(idProc, idOper, idPlan);
        }

        protected void txtVlDscto_OnTextChanged(object sender, EventArgs e)
        {
            decimal descto = (!string.IsNullOrEmpty(txtVlDscto.Text) ? decimal.Parse(txtVlDscto.Text) : 0);
            decimal vlConsul = (!string.IsNullOrEmpty(txtVlConsulOriginal.Text) ? decimal.Parse(txtVlConsulOriginal.Text) : 0);

            decimal aux = (vlConsul - descto);
            decimal vlFinal = (aux < 0 ? 0 : aux);
            txtVlConsul.Text = vlFinal.ToString("N2");
            abreModalInfosFinanceiras();
        }

        protected void ddlClassProfi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
        }

        private void CarregaTiposConsulta(DropDownList ddl, string selec, bool InsereVazio = false)
        {
            AuxiliCarregamentos.CarregaTiposConsulta(ddl, false, InsereVazio);
            ddl.SelectedValue = selec;
        }

        #endregion
    }
}