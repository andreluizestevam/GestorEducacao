//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: CANCELA SOLICITAÇÃO E ITENS DE SERVIÇOS.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.CancelaSolicitacaoItensServicos
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

            CarregaUnidades(ddlUnidadeEducacional);
            CarregaUnidades(ddlUnidadeEntrega);
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();

            var tb64 = RetornaEntidade();

            CarregaSolicitacoes(tb64);

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                txtObservacao.Enabled = txtDataCancelamento.Enabled = cblSolicitacoes.Enabled = true;
                txtDataCancelamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (cblSolicitacoes.SelectedValue == "")
                AuxiliPagina.EnvioMensagemErro(this, "No mínimo uma solicitação deve ser selecionada");

            TB64_SOLIC_ATEND tb64 = RetornaEntidade();

            tb64.CO_TELE_CONT = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb64.NU_TELE_RESP_SOLIC_ATEND = txtTelefoneResp.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb64.DE_OBS_SOLI = txtObservacao.Text;

            foreach (ListItem lstSolic in cblSolicitacoes.Items)
            {
                TB65_HIST_SOLICIT tb65 = TB65_HIST_SOLICIT.RetornaPelaChavePrimaria(tb64.CO_EMP, tb64.CO_ALU, tb64.CO_CUR, tb64.CO_SOLI_ATEN, int.Parse(lstSolic.Value));

                if (lstSolic.Selected)
                {
                    tb65.CO_SITU_SOLI = SituacaoItemSolicitacao.C.ToString();
                    tb65.DT_STATUS = DateTime.Now;

                    if (GestorEntities.SaveOrUpdate(tb65) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item");
                        return;
                    }
                }
            }

            bool todosCancelados = true;
            bool todosEntreguesOuCancelados = true;

//--------> Faz a verificação para saber se todos os itens estão cancelados e.ou entregues
            foreach (TB65_HIST_SOLICIT lstTb65 in tb64.TB65_HIST_SOLICIT) 
            {
                if (lstTb65.CO_SITU_SOLI != SituacaoItemSolicitacao.C.ToString())
                {
                    todosCancelados = false;
                    if (lstTb65.CO_SITU_SOLI != SituacaoItemSolicitacao.E.ToString())
                        todosEntreguesOuCancelados = false;
                }
            }

            if (todosCancelados)
                tb64.CO_SIT = SituacaoSolicitacao.C.ToString();
            else if (todosEntreguesOuCancelados)
                tb64.CO_SIT = SituacaoSolicitacao.F.ToString();
            else
                tb64.CO_SIT = SituacaoSolicitacao.A.ToString();

            CurrentPadraoCadastros.CurrentEntity = tb64;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB64_SOLIC_ATEND tb64 = RetornaEntidade();

            if (tb64 != null)
            {
                ddlUnidadeEducacional.SelectedValue = tb64.CO_EMP_ALU.ToString();

                tb64.TB25_EMPRESA1Reference.Load();
                ddlUnidadeEntrega.SelectedValue = tb64.TB25_EMPRESA1.CO_EMP.ToString();

                ddlModalidade.SelectedValue = tb64.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb64.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb64.CO_TUR.ToString();
                CarregaAluno();
                
//------------> Carrega informações do aluno e preenche os campos correspondentes
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(tb64.CO_ALU);
                txtNire.Text = tb07.NU_NIRE.ToString("00000000").Insert(5, ".").Insert(2, ".");
                ddlAluno.SelectedValue = tb64.CO_ALU.ToString();
                txtTelefone.Text = tb64.CO_TELE_CONT.ToString();

                if (tb64.TB108_RESPONSAVEL != null)
                    txtResponsavel.Text = tb64.TB108_RESPONSAVEL.NO_RESP.ToString();

                txtTelefoneResp.Text = tb64.NU_TELE_RESP_SOLIC_ATEND != null ? tb64.NU_TELE_RESP_SOLIC_ATEND.ToString() : string.Empty;
                txtNumeroSolicitacao.Text = tb64.NU_DCTO_SOLIC;
                txtPrevisao.Text = tb64.DT_PREV_ENTR.ToString("dd/MM/yyyy");
                txtDataCancelamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o CheckBoxList de Solicitações
        /// </summary>
        /// <param name="tb64">Entidade TB64_SOLIC_ATEND</param>
        private void CarregaSolicitacoes(TB64_SOLIC_ATEND tb64)
        {
            string situacaoAberta = SituacaoItemSolicitacao.A.ToString();

            cblSolicitacoes.DataSource = from tb65 in tb64.TB65_HIST_SOLICIT
                                         where tb65.CO_SITU_SOLI == situacaoAberta
                                         join tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros() on tb65.CO_TIPO_SOLI equals tb66.CO_TIPO_SOLI
                                         select new { tb66.CO_TIPO_SOLI, tb66.NO_TIPO_SOLI };

            cblSolicitacoes.DataTextField = "NO_TIPO_SOLI";
            cblSolicitacoes.DataValueField = "CO_TIPO_SOLI";
            cblSolicitacoes.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        /// <param name="ddl">DropDown de unidade</param>
        private void CarregaUnidades(DropDownList ddl)
        {
            ddl.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros() select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddl.DataTextField = "NO_FANTAS_EMP";
            ddl.DataValueField = "CO_EMP";
            ddl.DataBind();
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
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidadeEducacional.SelectedValue != "" ? int.Parse(ddlUnidadeEducacional.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                            where tb01.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
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
            if (ddlAluno.SelectedValue != "")
            {
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlAluno.SelectedValue));
                txtNire.Text = tb07.NU_NIRE.ToString("00.000.000");

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
