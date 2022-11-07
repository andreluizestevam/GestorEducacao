//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: AGENDA DE ATIVIDADES PROFISSIONAIS
// OBJETIVO: ATUALIZAÇÃO DE STATUS DE ATIVIDADES FUNCIONAIS AGENDADAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 15/05/2013| André Nobre Vinagre        | Corrigida a questão do carregamento quando inclusão
//           | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1313_AtualizaPosicaoAgendaAtividade
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

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
                //if (Double.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)) == 0)
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))                    
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, Consulte uma Tarefa.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);                    

                txtDtCadastro.Text = DateTime.Now.ToShortDateString();

                CarregaUnidade();
                CarregaDepartamento();
                CarregaFuncao();
                CarregaPrioridadeTarefa();
                CarregaStatusTarefa();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    TB137_TAREFAS_AGENDA tb137 = RetornaEntidade();
                    tb137.TB139_SITU_TAREF_AGENDReference.Load();
                    tb137.TB03_COLABOR1Reference.Load();

//----------------> Habilita apenas os Status possíveis para edição
                    ddlStatusTarefa.Items.Clear();
                    ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.EA.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.EA.CO_SITU_TAREF_AGEND));
                    ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TF.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND));

//----------------> Faz a verificação do Status da Tarefa para saber se é possível editá-la
                    if (tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TR.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND) ||
                        tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND))
                    {
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.CR.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND));
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.TR.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.TR.CO_SITU_TAREF_AGEND));
                        ddlStatusTarefa.Items.Add(new ListItem(TB139_SITU_TAREF_AGEND.CO.DE_SITU_TAREF_AGEND, TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND));
                    }
                    else
                    {
//--------------------> Disponível edição da Tarefa
                        ddlStatusTarefa.Enabled = txtObservacao.Enabled = true;

//--------------------> Verifica se EMISSOR é igual ao RESPONSÁVEL
                        if (tb137.TB03_COLABOR.CO_EMP == tb137.TB03_COLABOR1.CO_EMP && tb137.TB03_COLABOR.CO_COL == tb137.TB03_COLABOR1.CO_COL)
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
                            ddlEnviarSMS.Enabled = true;
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
            if (Page.IsValid)
            {
                DateTime dataAtual = DateTime.Now;

                TB137_TAREFAS_AGENDA tb137 = RetornaEntidade();

                tb137.TB139_SITU_TAREF_AGEND = TB139_SITU_TAREF_AGEND.RetornaPelaChavePrimaria(ddlStatusTarefa.SelectedValue);

//------------> Faz a verificação para saber se tarefa está sendo finalizada
                if (tb137.TB139_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF))
                    tb137.DT_REALIZ_TAREF_AGEND = dataAtual;
                
                tb137.DE_OBSERV_TAREF_AGEND = tb137.DE_OBSERV_TAREF_AGEND;

                int coIdent = TB137_TAREFAS_AGENDA.SaveOrUpdate(tb137, true);
                
//------------> Faz a verificaçao para saber se tarefa foi salva
                if (coIdent > 0 && tb137 != null)
                {
                    if (bool.Parse(ddlEnviarSMS.SelectedValue))
                    {
                        string siglaEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb137.CO_EMP).sigla;
                        
//--------------------> Será enviada uma mensagem de celular para o emissor da tarefa ao atualizar o status
                        string msg = "{0} - Acesse: portaleducacao.escolaw.com.br - Mat. Colaborador: " + tb137.TB03_COLABOR.CO_MAT_COL.Insert(5, "-").Insert(2, ".");
                        if (tb137.TB139_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF))
                        {
                            msg = string.Format(msg, "(Portal Educacao) Uma tarefa de sua origem foi finalizada");

                            SMSAuxili.EnvioSMS(siglaEmp, msg, "55" + tb137.TB03_COLABOR1.NU_TELE_CELU_COL.ToString(), DateTime.Now.Ticks.ToString());
                        }
                        else if (tb137.TB139_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CR))
                        {
                            msg = string.Format(msg, "(Portal Educacao) Uma tarefa de sua origem foi cancelada");

                            SMSAuxili.EnvioSMS(siglaEmp, msg, "55" + tb137.TB03_COLABOR1.NU_TELE_CELU_COL.ToString(), DateTime.Now.Ticks.ToString());
                        }
                        else if (tb137.TB139_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CO))
                        {
                            msg = "(Portal Educacao) Uma tarefa sob sua responsabilidade foi cancelada - Acesse: portaleducacao.escolaw.com.br - Mat. Emissor: " + tb137.TB03_COLABOR1.CO_MAT_COL.Insert(5, "-").Insert(2, ".");

                            SMSAuxili.EnvioSMS(siglaEmp, msg, "55" + tb137.TB03_COLABOR.NU_TELE_CELU_COL.ToString(), DateTime.Now.Ticks.ToString());
                        }
                    }

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }
        }        
        #endregion

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

                ddlNomeResponsavel.DataSource = TB03_COLABOR.RetornaPelaEmpresa(tb137.CO_EMP);
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
            ddlDepartamento.DataSource = TB14_DEPTO.RetornaTodosRegistros().Where(p => p.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

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
                                             select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

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

        protected void cvCelular_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (bool.Parse(ddlEnviarSMS.SelectedValue))
            {
                TB137_TAREFAS_AGENDA tarefa = RetornaEntidade();
                tarefa.TB03_COLABORReference.Load();
                tarefa.TB03_COLABOR1Reference.Load();

                string strCelular = "";

                if (ddlStatusTarefa.SelectedValue.Equals(TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND) || 
                    ddlStatusTarefa.SelectedValue.Equals(TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND))
                {
//----------------> Número do celular do emissor da tarefa
                    if (tarefa.TB03_COLABOR1 != null)
                        strCelular = tarefa.TB03_COLABOR1.NU_TELE_CELU_COL;
                }
                else if (ddlStatusTarefa.SelectedValue.Equals(TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND))
                {
//----------------> Número do celular do responsável pela tarefa
                    if (tarefa.TB03_COLABOR != null)
                        strCelular = tarefa.TB03_COLABOR.NU_TELE_CELU_COL;
                }

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