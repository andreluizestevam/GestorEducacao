//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: AGENDA DE ATIVIDADES PROFISSIONAIS
// OBJETIVO: REGISTRO/AGENDAMENTO DE ATIVIDADES FUNCIONAIS 
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//---> Utilizado para informar se conexão como a internet está OK
using System.Runtime.InteropServices;
using System.Diagnostics;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1311_RegistroAgendaAtividade
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDtCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");                

                CarregaUnidade();
                CarregaDepartamento();                
                CarregaFuncao();
                CarregaPrioridadeTarefa();
                CarregaStatusTarefa();
                
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    ddlUnidade.Enabled = ddlDepartamento.Enabled = ddlFuncao.Enabled = ddlNomeResponsavel.Enabled = txtTitulo.Enabled = txtDescricao.Enabled = 
                    txtDtCompromisso.Enabled = txtDtLimite.Enabled = ddlPrioridade.Enabled = ddlEnviarSMS.Enabled = true;
                    txtEmissor.Text = LoginAuxili.NOME_USU_LOGADO;
                    txtUnidadeEmissor.Text = LoginAuxili.NO_FANTAS_EMP;
                    ddlStatusTarefa.SelectedValue = TB139_SITU_TAREF_AGEND.TC.CO_SITU_TAREF_AGEND;
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    pnlChaveUnica.Visible = pnlNumTarefa.Visible = pnlObservacao.Visible = true;

                    TB137_TAREFAS_AGENDA tb137 = RetornaEntidade();
                    tb137.TB139_SITU_TAREF_AGENDReference.Load();
                    tb137.TB03_COLABOR1Reference.Load();

                    ddlStatusTarefa.Items.Clear();
                    
//----------------> Faz a verificação para saber se Status da Tarefa está "TA", se sim, a opção ediçao fica habilitada
                    if (tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TA.CO_SITU_TAREF_AGEND))
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TA.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TA.CO_SITU_TAREF_AGEND));

                    ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.EA.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.EA.CO_SITU_TAREF_AGEND));
                    ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TF.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND));
//----------------> Faz a verificação do Status da Tarefa para saber se é possível editá-la
                    if (tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TR.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TB.CO_SITU_TAREF_AGEND))
                    {
                        ddlStatusTarefa.Items.Clear();
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.CR.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND));
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.CO.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND));
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TR.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TR.CO_SITU_TAREF_AGEND));
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TF.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND));
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TB.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TB.CO_SITU_TAREF_AGEND));
                    }
                    else
                    {
//--------------------> Disponível edição da Tarefa
                        if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                        {
                            txtObservacao.Enabled = true;
                            lblSMS.InnerText = "Reenviar Msg SMS";
                        }

                        ddlStatusTarefa.Enabled = true;
                        
//--------------------> Verifica se EMISSOR é igual ao RESPONSÁVEL
                        if (tb137.TB03_COLABOR.CO_EMP.Equals(tb137.TB03_COLABOR1.CO_EMP) && tb137.TB03_COLABOR.CO_COL.Equals(tb137.TB03_COLABOR1.CO_COL))
                        {                            
//------------------------> Insere Status "Cancelado pela Origem" para escolha, pois o emissor igual ao responsável
                            ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.CO.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND));
                            
//------------------------> Não disponibiliza o envio de SMS
                            ddlEnviarSMS.SelectedValue = "False";
                        }
                        else
                        {                            
//------------------------> Insere Status "Cancelado pelo Responsável" para escolha, pois o emissor não é igual ao responsável
                            ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.CR.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND));
                            ddlStatusTarefa.Enabled = ddlEnviarSMS.Enabled = true;
                        }
                    }
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                TB137_TAREFAS_AGENDA tb137 = RetornaEntidade();

                if (tb137 != null)
                {
                    int verifOcorTarefa = (from lTb137 in TB137_TAREFAS_AGENDA.RetornaPelaChaveUnica(tb137.CO_CHAVE_UNICA_TAREF)
                                           select new { lTb137.CO_CHAVE_UNICA_TAREF }).Count();

                    if (verifOcorTarefa > 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não foi possível excluir a Tarefa. Verifique se existe uma Tarefa associada a mesma.");
                        return;
                    }
                    else
                    {
                        CurrentCadastroMasterPage.CurrentEntity = tb137;
                        return;
                    }
                }
            }

            if (Page.IsValid)
            {
                TB137_TAREFAS_AGENDA tb137;

                DateTime dataAtual = DateTime.Now;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb137 = new TB137_TAREFAS_AGENDA();
                    
//----------------> Informaçoes do funcionário que será delegada a tarefa
                    tb137.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                    tb137.CO_EMP = int.Parse(ddlUnidade.SelectedValue);
                    tb137.CO_COL = int.Parse(ddlNomeResponsavel.SelectedValue);
                    tb137.DT_CADAS_TAREF_AGEND = dataAtual;
                }
                else
                    tb137 = RetornaEntidade();

//------------> Informaçoes do funcionário que solicitou a tarefa
                tb137.CO_ORGAO_SOLIC_TAREF_AGEND = LoginAuxili.ORG_CODIGO_ORGAO;
                tb137.TB03_COLABOR1 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                tb137.NM_RESUM_TAREF_AGEND = txtTitulo.Text;
                tb137.DE_DETAL_TAREF_AGEND = txtDescricao.Text;                
                tb137.DT_COMPR_TAREF_AGEND = DateTime.Parse(txtDtCompromisso.Text);
                tb137.DT_LIMIT_TAREF_AGEND = DateTime.Parse(txtDtLimite.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
                tb137.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(ddlPrioridade.SelectedValue);
                tb137.DE_OBSERV_TAREF_AGEND = txtObservacao.Text;
                tb137.TB139_SITU_TAREF_AGEND = TB139_SITU_TAREF_AGEND.RetornaPelaChavePrimaria(ddlStatusTarefa.SelectedValue);
                tb137.CO_FLA_SMS_TAREF_AGEND = bool.Parse(ddlEnviarSMS.SelectedValue) ? "S" : "N";
                tb137.CO_FLA_REABERTA = "N";
                
//------------> Faz a verificação para saber se tarefa está sendo finalizada
                if (tb137.TB139_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF))
                    tb137.DT_REALIZ_TAREF_AGEND = dataAtual;
                tb137.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb137.ORG_CODIGO_ORGAO);
                tb137.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(tb137.CO_EMP, tb137.CO_COL);

                TB137_TAREFAS_AGENDA.SaveOrUpdate(tb137, true);

                if (tb137.CO_IDENT_TAREF > 0 && tb137 != null)
                {
                    string strChaveUnica = tb137.ORG_CODIGO_ORGAO.ToString() + tb137.CO_EMP.ToString() + tb137.CO_COL.ToString() + tb137.CO_IDENT_TAREF.ToString();

                    tb137.CO_CHAVE_UNICA_TAREF = long.Parse(strChaveUnica);

                    if ((QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao)))
                    {

                      if (IsConnected()) 
                      {                        
//--------------------> Será enviada uma mensagem de celular para o responsável pelo cadastro da nova tarefa
                        if (bool.Parse(ddlEnviarSMS.SelectedValue)) {
                            string siglaEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb137.CO_EMP).sigla;

                          SMSAuxili.EnvioSMS(siglaEmp,
                                            "(Portal Educacao) Foi agendada uma nova tarefa sob sua responsabilidade - Acesse: portaleducacao.escolaw.com.br - Mat. Emissor: " + tb137.TB03_COLABOR1.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                            "55" + tb137.TB03_COLABOR.NU_TELE_CELU_COL.ToString(),
                                            DateTime.Now.Ticks.ToString());
                        }

                        TB137_TAREFAS_AGENDA.SaveOrUpdate(tb137, true);
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

                      } else {

                          TB137_TAREFAS_AGENDA.SaveOrUpdate(tb137, true);
                          AuxiliPagina.RedirecionaParaPaginaSucesso("Registro adicionado com sucesso! SMS não enviado por falta de Conexão com a Internet , efetuar o reenvio mais tarde !", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                      } 
                    }
                    else
                        CurrentCadastroMasterPage.CurrentEntity = tb137;
                }
            }
        }        

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
//---------> TB03_COLABOR = RESPONSÁVEL PELA TAREFA
//---------> TB03_COLABOR1 = EMISSOR DA TAREFA

            TB137_TAREFAS_AGENDA tb137 = RetornaEntidade();

            if (tb137 != null)
            {
                tb137.TB03_COLABOR1Reference.Load();

                txtEmissor.Text = tb137.TB03_COLABOR1.NO_COL;
                txtUnidadeEmissor.Text = TB25_EMPRESA.RetornaPelaChavePrimaria(tb137.TB03_COLABOR1.CO_EMP).NO_FANTAS_EMP;
                ddlUnidade.SelectedValue = tb137.CO_EMP.ToString();
                ddlDepartamento.SelectedValue = tb137.TB03_COLABOR.CO_DEPTO.ToString();
                ddlFuncao.SelectedValue = tb137.TB03_COLABOR.CO_FUN.ToString();

                ddlNomeResponsavel.DataSource = TB03_COLABOR.RetornaPelaEmpresa(tb137.CO_EMP).OrderBy(e => e.NO_COL);
                ddlNomeResponsavel.DataValueField = "CO_COL";
                ddlNomeResponsavel.DataTextField = "NO_COL";
                ddlNomeResponsavel.DataBind();

                ddlNomeResponsavel.SelectedValue = tb137.CO_COL.ToString();

                txtTitulo.Text = tb137.NM_RESUM_TAREF_AGEND;
                txtChaveUnica.Text = tb137.CO_CHAVE_UNICA_TAREF.ToString();
                txtNumTarefa.Text = tb137.CO_IDENT_TAREF.ToString();
                txtDescricao.Text = tb137.DE_DETAL_TAREF_AGEND;
                txtDtCadastro.Text = tb137.DT_CADAS_TAREF_AGEND.ToString("dd/MM/yyyy");
                txtDtCompromisso.Text = tb137.DT_COMPR_TAREF_AGEND.ToString("dd/MM/yyyy");
                txtDtLimite.Text = tb137.DT_LIMIT_TAREF_AGEND.Value.ToString("dd/MM/yyyy");
                ddlPrioridade.SelectedValue = tb137.TB140_PRIOR_TAREF_AGEND.CO_PRIOR_TAREF_AGEND;
                txtObservacao.Text = tb137.DE_OBSERV_TAREF_AGEND;
                ddlStatusTarefa.SelectedValue = tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND;                
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB137_TAREFAS_AGENDA</returns>
        private TB137_TAREFAS_AGENDA RetornaEntidade()
        {
            return TB137_TAREFAS_AGENDA.RetornaPelaChaveUnicaEIdent(Double.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoIdentTarefa));
        }

        /// <summary>
        /// Método que carrega informações verificando se o emissor é igual ao responsável da tarefa
        /// </summary>
        private void EmissorIgualResponsavel()
        {
            if (ddlUnidade.SelectedValue.Equals(LoginAuxili.CO_EMP.ToString()) && ddlNomeResponsavel.SelectedValue.Equals(LoginAuxili.CO_COL.ToString()))
            {
                ddlStatusTarefa.SelectedValue = TB139_SITU_TAREF_AGEND.TA.CO_SITU_TAREF_AGEND;

                ddlEnviarSMS.SelectedValue = "False";
                ddlEnviarSMS.Enabled = false;
            }
            else
            {
                ddlStatusTarefa.SelectedValue = TB139_SITU_TAREF_AGEND.TC.CO_SITU_TAREF_AGEND;
                ddlEnviarSMS.Enabled = true;
            }
        }

        /// <summary>
        /// Método que verifica se internet está disponível
        /// </summary>
        /// <returns></returns>
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidade
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamento
        /// </summary>
        private void CarregaDepartamento()
        {
            ddlDepartamento.DataSource = TB14_DEPTO.RetornaTodosRegistros().Where( p => p.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlDepartamento.DataValueField = "CO_DEPTO";
            ddlDepartamento.DataTextField = "CO_SIGLA_DEPTO";
            ddlDepartamento.DataBind();

            ddlDepartamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Função
        /// </summary>
        private void CarregaFuncao()
        {
            ddlFuncao.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

            ddlFuncao.DataValueField = "CO_FUN";
            ddlFuncao.DataTextField = "NO_FUN";
            ddlFuncao.DataBind();

            ddlFuncao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Prioridade de Tarefas
        /// </summary>
        private void CarregaPrioridadeTarefa()
        {
            ddlPrioridade.DataSource = TB140_PRIOR_TAREF_AGEND.RetornaTodosRegistros();
            ddlPrioridade.DataValueField = "CO_PRIOR_TAREF_AGEND";
            ddlPrioridade.DataTextField = "DE_PRIOR_TAREF_AGEND";
            ddlPrioridade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Status de Tarefas
        /// </summary>
        private void CarregaStatusTarefa()
        {
            ddlStatusTarefa.DataSource = TB139_SITU_TAREF_AGEND.RetornaTodosRegistros();
            ddlStatusTarefa.DataValueField = "CO_SITU_TAREF_AGEND";
            ddlStatusTarefa.DataTextField = "DE_SITU_TAREF_AGEND";
            ddlStatusTarefa.DataBind();
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNomeResponsavel.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                             select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlNomeResponsavel.DataValueField = "CO_COL";
            ddlNomeResponsavel.DataTextField = "NO_COL";
            ddlNomeResponsavel.DataBind();

            ddlDepartamento.ClearSelection();
            ddlFuncao.ClearSelection();

            EmissorIgualResponsavel();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coDepto = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNomeResponsavel.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                             where tb03.CO_DEPTO == coDepto
                                             select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlNomeResponsavel.DataValueField = "CO_COL";
            ddlNomeResponsavel.DataTextField = "NO_COL";
            ddlNomeResponsavel.DataBind();

            EmissorIgualResponsavel();
        }

        protected void ddlFuncao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coFun = ddlFuncao.SelectedValue != "" ? int.Parse(ddlFuncao.SelectedValue) : 0;

            ddlNomeResponsavel.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                             where tb03.CO_FUN == coFun
                                             select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlNomeResponsavel.DataValueField = "CO_COL";
            ddlNomeResponsavel.DataTextField = "NO_COL";
            ddlNomeResponsavel.DataBind();

            EmissorIgualResponsavel();
        }

        protected void ddlNomeResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmissorIgualResponsavel();
        }        

        #region Validadores

        protected void cvDataCompromisso_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtCompromisso.Text) > DateTime.Parse(txtDtLimite.Text))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataLimite_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtLimite.Text) < DateTime.Parse(txtDtCadastro.Text))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvCelularResponsavel_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (bool.Parse(ddlEnviarSMS.SelectedValue))
            {
                string strCelular = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeResponsavel.SelectedValue)).NU_TELE_CELU_COL;

                long longCelular;

                if (!long.TryParse(strCelular, out longCelular) || longCelular.Equals(0))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }
        #endregion
    }
}