//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: AGENDA DE ATIVIDADES PROFISSIONAIS
// OBJETIVO: REATIVAÇÃO DE ATIVIDADES FUNCIONAIS AGENDADAS ENCERRADAS
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
using System.Linq;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1314_ReativaAgendaAtividadeFinaCanc
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
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
                    
//----------------> Faz a verificação do Status da Tarefa para saber se é possível reabri-la
                    if (!tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND) &&
                        !tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND) &&
                        !tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND))
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Status atual da Tarefa não permite reabri-la!");
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            double coChaveUnica = txtChaveUnica.Text != "" ? int.Parse(txtChaveUnica.Text) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coCol = ddlNomeResponsavelReabertura.SelectedValue != "" ? int.Parse(ddlNomeResponsavelReabertura.SelectedValue) : 0;

            if (Page.IsValid)
            {
                DateTime dataAtual = DateTime.Now;

                TB137_TAREFAS_AGENDA tb137 = new TB137_TAREFAS_AGENDA();

//------------> Informaçoes do funcionário que será delegada a tarefa
                tb137.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                tb137.CO_EMP = coEmp;
                tb137.CO_COL = coCol;

//------------> Informaçoes do funcionário que solicitou a tarefa
                tb137.CO_ORGAO_SOLIC_TAREF_AGEND = LoginAuxili.ORG_CODIGO_ORGAO;
                tb137.TB03_COLABOR1 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                tb137.NM_RESUM_TAREF_AGEND = txtTitulo.Text;
                tb137.DE_DETAL_TAREF_AGEND = txtDescricao.Text;
                tb137.DE_OBSERV_TAREF_AGEND = txtMotivoReabertura.Text;
                tb137.DT_CADAS_TAREF_AGEND = dataAtual;
                tb137.DT_COMPR_TAREF_AGEND = DateTime.Parse(txtDtReabertura.Text);
                tb137.DT_LIMIT_TAREF_AGEND = DateTime.Parse(txtDtLimiteReabertura.Text);
                tb137.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(ddlPrioridadeReabertura.SelectedValue);

                tb137.TB139_SITU_TAREF_AGEND = TB139_SITU_TAREF_AGEND.RetornaPelaChavePrimaria(TB139_SITU_TAREF_AGEND.TC.CO_SITU_TAREF_AGEND);
                tb137.CO_FLA_SMS_TAREF_AGEND = bool.Parse(ddlEnviarSMS.SelectedValue) ? "S" : "N";
                tb137.CO_FLA_REABERTA = "S";

                tb137.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb137.ORG_CODIGO_ORGAO);
                tb137.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(tb137.CO_EMP, tb137.CO_COL);
                tb137.CO_CHAVE_UNICA_TAREF = coChaveUnica;
                int tar = TB137_TAREFAS_AGENDA.SaveOrUpdate(tb137, true);

//------------> Atualiza os dados do colaborador que repassou a tarefa no registro da tarefa original
                TB137_TAREFAS_AGENDA tarefaOriginal = TB137_TAREFAS_AGENDA.RetornaPelaChaveUnicaEIdent(coChaveUnica, int.Parse(txtNumTarefa.Text));
                tarefaOriginal.TB03_COLABOR2 = tb137.TB03_COLABOR;
                tarefaOriginal.CO_ORGAO_REPAS_TAREF_AGEND = tb137.TB03_COLABOR.ORG_CODIGO_ORGAO;
                tarefaOriginal.DE_OBSERV_TAREF_AGEND = txtMotivoReabertura.Text;
                tarefaOriginal.DT_REPAS_TAREF_AGEND = dataAtual;
                tarefaOriginal.TB139_SITU_TAREF_AGEND = TB139_SITU_TAREF_AGEND.TB;
                int tarOri = TB137_TAREFAS_AGENDA.SaveOrUpdate(tarefaOriginal, true);

//------------> Será enviada uma mensagem de celular para o responsável pelo cadastro da nova tarefa
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao) && bool.Parse(ddlEnviarSMS.SelectedValue))
                {
                    string siglaEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb137.CO_EMP).sigla;

                    SMSAuxili.EnvioSMS(siglaEmp,
                                      "(Portal Educacao) Foi agendada uma nova tarefa sob sua responsabilidade - Acesse: portaleducacao.escolaw.com.br - Mat. Emissor: " + tb137.TB03_COLABOR1.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                      "55" + tb137.TB03_COLABOR.NU_TELE_CELU_COL.ToString(),
                                      DateTime.Now.Ticks.ToString());
                }

                if (tar > 0 && tarOri > 0)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
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
                tb137.TB03_COLABOR2Reference.Load();

                txtMatEmissor.Text = tb137.TB03_COLABOR1.CO_MAT_COL.Insert(5, "-").Insert(2, ".");
                txtEmissor.Text = tb137.TB03_COLABOR1.NO_COL;
                txtUnidadeEmissor.Text = TB25_EMPRESA.RetornaPelaChavePrimaria(tb137.TB03_COLABOR1.CO_EMP).NO_FANTAS_EMP;
                txtMatResponsavel.Text = tb137.TB03_COLABOR.CO_MAT_COL.Insert(5, "-").Insert(2, ".");
                txtNomeResponsavel.Text = tb137.TB03_COLABOR.NO_COL;
                txtUnidadeResponsavel.Text = TB25_EMPRESA.RetornaPelaChavePrimaria(tb137.TB03_COLABOR.CO_EMP).NO_FANTAS_EMP;
                txtTitulo.Text = tb137.NM_RESUM_TAREF_AGEND;                
                txtChaveUnica.Text = tb137.CO_CHAVE_UNICA_TAREF.ToString();
                txtNumTarefa.Text = tb137.CO_IDENT_TAREF.ToString();
                txtDescricao.Text = tb137.DE_DETAL_TAREF_AGEND;
                txtDtCadastro.Text = tb137.DT_CADAS_TAREF_AGEND.ToString("dd/MM/yyyy");
                txtDtCompromisso.Text = tb137.DT_COMPR_TAREF_AGEND.ToString("dd/MM/yyyy");
                txtDtLimite.Text = tb137.DT_LIMIT_TAREF_AGEND.Value.ToString("dd/MM/yyyy");
                ddlPrioridade.SelectedValue = tb137.TB140_PRIOR_TAREF_AGEND.CO_PRIOR_TAREF_AGEND;
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
            if (ddlUnidade.SelectedValue == LoginAuxili.CO_EMP.ToString() && ddlNomeResponsavelReabertura.SelectedValue == LoginAuxili.CO_COL.ToString())
            {
                ddlEnviarSMS.SelectedValue = "False";
                ddlEnviarSMS.Enabled = false;
            }
            else
                ddlEnviarSMS.Enabled = true;
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

            ddlNomeResponsavelReabertura.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlNomeResponsavelReabertura.DataValueField = "CO_COL";
            ddlNomeResponsavelReabertura.DataTextField = "NO_COL";
            ddlNomeResponsavelReabertura.DataBind();

            ddlDepartamento.ClearSelection();
            ddlFuncao.ClearSelection();

            EmissorIgualResponsavel();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coDepto = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNomeResponsavelReabertura.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                                       where tb03.CO_DEPTO == coDepto
                                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlNomeResponsavelReabertura.DataValueField = "CO_COL";
            ddlNomeResponsavelReabertura.DataTextField = "NO_COL";
            ddlNomeResponsavelReabertura.DataBind();

            EmissorIgualResponsavel();
        }

        protected void ddlFuncao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coFun = ddlFuncao.SelectedValue != "" ? int.Parse(ddlFuncao.SelectedValue) : 0;

            ddlNomeResponsavelReabertura.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                                       where tb03.CO_FUN == coFun
                                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlNomeResponsavelReabertura.DataValueField = "CO_COL";
            ddlNomeResponsavelReabertura.DataTextField = "NO_COL";
            ddlNomeResponsavelReabertura.DataBind();

            EmissorIgualResponsavel();
        }

        protected void ddlNomeResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmissorIgualResponsavel();
        }

        #region Validadores

        protected void cvDataReabertura_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtCompromisso.Text) > DateTime.Parse(txtDtReabertura.Text))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataLimiteReabertura_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (DateTime.Parse(txtDtLimiteReabertura.Text) < DateTime.Parse(txtDtLimite.Text))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvCelularResponsavel_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (bool.Parse(ddlEnviarSMS.SelectedValue))
            {
                string strCelular = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeResponsavelReabertura.SelectedValue)).NU_TELE_CELU_COL;

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