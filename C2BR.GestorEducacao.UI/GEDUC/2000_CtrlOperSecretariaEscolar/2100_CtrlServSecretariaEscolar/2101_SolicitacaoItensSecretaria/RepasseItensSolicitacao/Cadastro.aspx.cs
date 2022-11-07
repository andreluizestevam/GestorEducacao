//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: REPASSE DE ITENS DE SOLICITAÇÕES.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.RepasseItensSolicitacao
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione uma solicitação.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

            if (IsPostBack) return;

            CarregaUnidadesEducacionais();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaUnidadesEntrega();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                TB64_SOLIC_ATEND tb64 = RetornaEntidade();

                string strTipoSolic = QueryStringAuxili.RetornaQueryStringPelaChave("tipo"); // (E)nvio, (R)ecebimento

                if (strTipoSolic == "E")
                {
                    lblDataEnvioReceb.InnerText = "Data Envio";
                    lblSituacao.Text = "*** Envio de Solicitação N° " + tb64.NU_DCTO_SOLIC + " ***";
                }
                else if (strTipoSolic == "R")
                {
                    lblDataEnvioReceb.InnerText = "Recebimento";
                    lblSituacao.Text = "*** Recebimento de Solicitação N° " + tb64.NU_DCTO_SOLIC + " ***";
                }

                CarregaSolicitacoes(tb64);
                HabilitaGrid(false);

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    txtObservacao.Enabled = txtDataEnvioReceb.Enabled = true;
                    HabilitaGrid(true);
                    txtDataEnvioReceb.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    if (strTipoSolic == "E")
                        ddlUnidadeEntrega.Enabled = true;
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidadeEntrega.SelectedValue != "" ? int.Parse(ddlUnidadeEntrega.SelectedValue) : 0;

//--------> Boolean para saber se usuário selecionou algum item para entrega
            bool flagSelecionado = false;

            foreach (GridViewRow linha in grvDocumentos.Rows)
                if (((CheckBox)linha.Cells[0].FindControl("chkSelecione")).Checked)
                    flagSelecionado = true;

            if (!flagSelecionado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Deve ser escolhida no mínimo uma solicitação.");
                return;
            }

            string strTipoSolic = QueryStringAuxili.RetornaQueryStringPelaChave("tipo"); // (E)nvio, (R)ecebimento

            TB64_SOLIC_ATEND tb64 = RetornaEntidade();

            tb64.CO_TELE_CONT = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb64.NU_TELE_RESP_SOLIC_ATEND = txtTelefoneResp.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb64.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

//--------> Varre toda a grid de documentos
            foreach (GridViewRow linha in grvDocumentos.Rows)
            {
//------------> Faz a verificação para saber se linha foi checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelecione")).Checked)
                {
//----------------> Carrega o item selecionado
                    TB65_HIST_SOLICIT tb65 = TB65_HIST_SOLICIT.RetornaPelaChavePrimaria(tb64.CO_EMP, tb64.CO_ALU, tb64.CO_CUR, tb64.CO_SOLI_ATEN, int.Parse(linha.Cells[4].Text));

//----------------> Faz a criação de um novo registro na TB197_REMES_SOLIC
                    TB197_REMES_SOLIC tb197 = new TB197_REMES_SOLIC();
                    tb197.TB65_HIST_SOLICIT = tb65;
                    tb197.CO_ALU = tb65.CO_ALU;
                    tb197.CO_EMP = tb65.CO_EMP;
                    tb197.CO_CUR = tb65.CO_CUR;
                    tb197.CO_SOLI_ATEN = tb65.CO_SOLI_ATEN;
                    tb197.CO_TIPO_SOLI = tb65.CO_TIPO_SOLI;

                    tb197.CO_FLAG_SMS = tb65.TB64_SOLIC_ATEND.CO_FLA_SMS_SOLIC_ATEND;
                    tb197.DE_OBS_REMES = txtObservacao.Text;
                    tb197.DT_REMES_SOLIC = DateTime.Now;
                    tb197.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                    if (strTipoSolic == "E") // Envio
                    {
                        if (tb65.CO_SITU_SOLI == SituacaoItemSolicitacao.T.ToString())
                        {
//------------------------> Agendamento de envio de remessa pendente
                            tb197.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                            tb197.CO_TIPO_ENVREC_REMES = TipoHistoricoRemessaSolicitacao.P.ToString();

                            GestorEntities.SaveOrUpdate(tb197);
                        }
                        else
                        {
//------------------------> Envio de remessa
                            tb197.TB25_EMPRESA = tb65.TB25_EMPRESA;
                            tb197.CO_TIPO_ENVREC_REMES = TipoHistoricoRemessaSolicitacao.E.ToString();

                            GestorEntities.SaveOrUpdate(tb197);

//------------------------> Faz a verificação para saber se existe alguma pendência
                            TB197_REMES_SOLIC tb197_Pendentes = (from lTb197 in TB197_REMES_SOLIC.RetornaPeloItemSolicitacao(tb65.CO_ALU, tb65.CO_EMP, tb65.CO_CUR, tb65.CO_SOLI_ATEN, tb65.CO_TIPO_SOLI)
                                                                 where lTb197.CO_TIPO_ENVREC_REMES == "P"
                                                                 select lTb197).FirstOrDefault();

                            if (tb197_Pendentes != null)
                            {
//----------------------------> Faz a modificação do status da solicitação para "C" (Pendência Concluída) ou "I" (Pendência Ignorada)
                                if (tb197_Pendentes.TB25_EMPRESA.CO_EMP == coEmp)
                                    tb197_Pendentes.CO_TIPO_ENVREC_REMES = TipoHistoricoRemessaSolicitacao.C.ToString();
                                else
                                    tb197_Pendentes.CO_TIPO_ENVREC_REMES = TipoHistoricoRemessaSolicitacao.I.ToString();

                                GestorEntities.SaveOrUpdate(tb197_Pendentes);                               
                            }

                            tb65.TB25_EMPRESA = tb64.TB25_EMPRESA1;
                            tb65.CO_SITU_SOLI = SituacaoItemSolicitacao.T.ToString();
                            tb65.DT_ENVIO_SOLI = DateTime.Parse(txtDataEnvioReceb.Text);
                        }
                    }
                    else if (strTipoSolic == "R") // Recebimento
                    {                 
//--------------------> Recebimento de remessa
                        tb197.TB25_EMPRESA = tb65.TB25_EMPRESA;
                        tb197.CO_TIPO_ENVREC_REMES = TipoHistoricoRemessaSolicitacao.R.ToString();
                        
                        GestorEntities.SaveOrUpdate(tb197);

                        tb65.CO_SITU_SOLI = SituacaoItemSolicitacao.D.ToString();
                        tb65.DT_RECE_SOLI = DateTime.Parse(txtDataEnvioReceb.Text);

//--------------------> Se campo de envio de SMS igual a "S"im, envia uma mensagem de solicitação finalizada e disponível
                        if (tb65.TB64_SOLIC_ATEND.CO_FLA_SMS_SOLIC_ATEND == "S")
                        {
                            string strNomeSolicitacao = tb65.TB66_TIPO_SOLIC.NO_TIPO_SOLI;
                            strNomeSolicitacao = strNomeSolicitacao.Length > 60 ? strNomeSolicitacao.Substring(0, 60) : strNomeSolicitacao;

                            string strTelUnidade = tb65.TB25_EMPRESA.CO_TEL1_EMP;
                            string strSiglaUnidade = tb65.TB25_EMPRESA.sigla;
                            string strTelUsuario = !string.IsNullOrEmpty(tb65.TB64_SOLIC_ATEND.NU_TELE_RESP_SOLIC_ATEND) ?
                                tb65.TB64_SOLIC_ATEND.NU_TELE_RESP_SOLIC_ATEND : tb65.TB64_SOLIC_ATEND.CO_TELE_CONT;

                            if (!string.IsNullOrEmpty(strTelUsuario))
                            {
                                SMSAuxili.EnvioSMS( strSiglaUnidade,
                                    "(Portal Educacao) Solicitacao " + strNomeSolicitacao.RemoveAcentuacoes() + " finalizada e disponivel. Tel: " + strTelUnidade,
                                    "55" + strTelUsuario,
                                    DateTime.Now.Ticks.ToString());
                            }
                        }
                    }

                    tb65.DE_OBS_REMES = txtObservacao.Text;
                    tb65.DT_STATUS = DateTime.Now;

                    if (GestorEntities.SaveOrUpdate(tb65) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item");
                        return;
                    }
                }
            }
            
            tb64.NU_SEM_LET = tb64.NU_SEM_LET;
            CurrentPadraoCadastros.CurrentEntity = tb64;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
//--------> (E)nvio, (R)ecebimento
            string strTipoSolic = QueryStringAuxili.RetornaQueryStringPelaChave("tipo"); // (E)nvio, (R)ecebimento

            TB64_SOLIC_ATEND tb64 = RetornaEntidade();

            if (tb64 != null)
            {
                ddlUnidadeEducacional.SelectedValue = tb64.CO_EMP_ALU.ToString();

                tb64.TB25_EMPRESA1Reference.Load();

//------------> Se for enviar, a unidade de entrega é a definida na solicitação, se for recebimento, é a unidade logada
                if (strTipoSolic == "E")
                    ddlUnidadeEntrega.SelectedValue = tb64.TB25_EMPRESA1.CO_EMP.ToString();
                else if (strTipoSolic == "R")
                    ddlUnidadeEntrega.SelectedValue = LoginAuxili.CO_EMP.ToString();

                ddlModalidade.SelectedValue = tb64.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb64.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb64.CO_TUR.ToString();
                CarregaAluno();

//------------> Carrega informações do Aluno
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tb64.CO_ALU);
                txtNire.Text = tb07.NU_NIRE.ToString("00000000").Insert(5, ".").Insert(2, ".");
                ddlAluno.SelectedValue = tb64.CO_ALU.ToString();
                txtTelefone.Text = tb64.CO_TELE_CONT.ToString();

                if (tb64.TB108_RESPONSAVEL != null)
                {
                    txtCpfResponsavel.Text = tb64.TB108_RESPONSAVEL.NU_CPF_RESP;
                    txtResponsavel.Text = tb64.TB108_RESPONSAVEL.NO_RESP.ToString();
                }

                txtTelefoneResp.Text = tb64.NU_TELE_RESP_SOLIC_ATEND != null ? tb64.NU_TELE_RESP_SOLIC_ATEND.ToString() : "";
                txtPrevisao.Text = tb64.DT_PREV_ENTR.ToString("dd/MM/yyyy");
                txtDataEnvioReceb.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB64_SOLIC_ATEND</returns>
        private TB64_SOLIC_ATEND RetornaEntidade()
        {
            TB64_SOLIC_ATEND tb64 = TB64_SOLIC_ATEND.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb64 == null) ? new TB64_SOLIC_ATEND() : tb64;
        }

        /// <summary>
        /// Habilita ou não as linhas da grid de documentos
        /// </summary>
        /// <param name="enable">Boolean habilita</param>
        private void HabilitaGrid(bool enable)
        {
            foreach (GridViewRow linha in grvDocumentos.Rows)
                linha.Enabled = enable;
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Documentos
        /// </summary>
        /// <param name="tb64">Entidade TB64_SOLIC_ATEND</param>
        private void CarregaSolicitacoes(TB64_SOLIC_ATEND tb64)
        {
//--------> (E)nvio, (R)ecebimento
            string strTipoSolic = QueryStringAuxili.RetornaQueryStringPelaChave("tipo");

            string situacaoFinalizada = SituacaoItemSolicitacao.F.ToString();
            string situacaoEmTransito = SituacaoItemSolicitacao.T.ToString();
            string situacaoDisponivel = SituacaoItemSolicitacao.D.ToString();

            if (strTipoSolic == "E")
            {
                grvDocumentos.DataSource = from tb65 in VW65_HIST_SOLIC.RetornaTodosRegistros()
                                           where tb65.CO_ALU == tb64.CO_ALU && tb65.CO_CUR == tb64.CO_CUR && tb65.CO_EMP == tb64.CO_EMP
                                           && tb65.CO_SOLI_ATEN == tb64.CO_SOLI_ATEN &&
                                           ((tb65.CO_SITU_SOLI == situacaoFinalizada && tb65.CO_EMP_ALU == LoginAuxili.CO_EMP) ||
                                           (tb65.CO_SITU_SOLI == situacaoDisponivel && tb65.UNID_ENTREGA == LoginAuxili.CO_EMP) ||
                                           (!tb65.PENDENTE.Value && tb65.CO_SITU_SOLI == situacaoEmTransito && tb65.UNID_ENTREGA == LoginAuxili.CO_EMP))
                                           select new
                                           {
                                               tb65.CO_TIPO_SOLI, tb65.NO_TIPO_SOLI, tb65.NO_EMP_ENTREGA,
                                               SITUACAO = tb65.PENDENTE.Value ? "Pendente" : (tb65.CO_SITU_SOLI == "F" ? "Finalizada" : (tb65.CO_SITU_SOLI == "D" ? "Disponível" : "Em Trânsito"))
                                           };
            }
            else if (strTipoSolic == "R")
            {
                grvDocumentos.DataSource = from tb65 in VW65_HIST_SOLIC.RetornaTodosRegistros()
                                           where tb65.CO_ALU == tb64.CO_ALU && tb65.CO_CUR == tb64.CO_CUR && tb65.CO_EMP == tb64.CO_EMP
                                           && tb65.CO_SOLI_ATEN == tb64.CO_SOLI_ATEN && (tb65.CO_SITU_SOLI == situacaoEmTransito && tb65.UNID_ENTREGA == LoginAuxili.CO_EMP)
                                           select new
                                           {
                                               tb65.CO_TIPO_SOLI, tb65.NO_TIPO_SOLI, tb65.NO_EMP_ENTREGA,
                                               SITUACAO = tb65.CO_SITU_SOLI == "F" ? "Finalizada" : (tb65.CO_SITU_SOLI == "D" ? "Disponível" : "Em Trânsito")
                                           };
            }

            grvDocumentos.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidadesEducacionais()
        {
            ddlUnidadeEducacional.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidadeEducacional.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeEducacional.DataValueField = "CO_EMP";
            ddlUnidadeEducacional.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares de Entrega
        /// </summary>
        private void CarregaUnidadesEntrega()
        {
            string strTipoSolic = QueryStringAuxili.RetornaQueryStringPelaChave("tipo");

            ddlUnidadeEntrega.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where (strTipoSolic == "E" && tb25.CO_EMP != LoginAuxili.CO_EMP) || strTipoSolic == "R"
                                            select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidadeEntrega.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeEntrega.DataValueField = "CO_EMP";
            ddlUnidadeEntrega.DataBind();

            ddlUnidadeEntrega.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Série
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidadeEducacional.SelectedValue != "" ? int.Parse(ddlUnidadeEducacional.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                            where tb01.CO_MODU_CUR == modalidade
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turma
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).OrderBy( t => t.NO_TURMA );

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = ddlUnidadeEducacional.SelectedValue != "" ? int.Parse(ddlUnidadeEducacional.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (turma != 0)
            {
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                                       && (serie != 0 ? tb08.CO_CUR == serie : serie == 0) && tb08.TB25_EMPRESA.CO_EMP == coEmp
                                       && (turma != 0 ? tb08.CO_TUR == turma : turma == 0)
                                       select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy( m => m.NO_ALU );

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
            }
            else
                ddlAluno.Items.Clear();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion   
   
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            if (coAlu != 0)
            {
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                txtNire.Text = tb07.NU_NIRE.ToString("00000000").Insert(5, ".").Insert(2, ".");

                if (tb07.TB108_RESPONSAVEL != null)
                {
                    txtResponsavel.Text = tb07.TB108_RESPONSAVEL.NO_RESP;
                    txtTelefoneResp.Text = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP;
                }
                else
                {
                    txtResponsavel.Text = "";
                    txtTelefoneResp.Text = "";
                }
            }
            else
                txtNire.Text = "";
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlUnidadeEducacional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }
    }
}
