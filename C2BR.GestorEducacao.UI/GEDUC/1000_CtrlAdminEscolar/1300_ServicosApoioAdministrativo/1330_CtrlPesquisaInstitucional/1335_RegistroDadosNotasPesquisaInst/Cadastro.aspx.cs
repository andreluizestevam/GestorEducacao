//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: REGISTRO DE DADOS/INFORMAÇÕE E NOTAS A PESQUISAS INSTITUCIONAIS
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
using System.Data.Sql;
using System.Collections.Specialized;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1335_RegistroDadosNotasPesquisaInst
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        static int codFormAtualQS = 0;
        #endregion

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
                if (QueryStringAuxili.OperacaoCorrenteQueryString != QueryStrings.OperacaoInsercao)
                    codFormAtualQS = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                CarregaTiposAvaliacao();
                CarregaPublicosAlvo();
                CarregaCamposIdentificao();
                CarregaItensAvaliacao();
                CarregaModalidades();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB78_PESQ_AVAL tb78 = RetornaEntidade();

            if (QueryStringAuxili.OperacaoCorrenteQueryString != QueryStrings.OperacaoExclusao)
            {
                if (ddlItemAvaliacao.SelectedIndex == ddlItemAvaliacao.Items.Count - 1)
                {
                    SalvaRespostas();
                    tb78.CO_FLAG_FINAL = "S";
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Clique em prosseguir para continuar a avaliação");
                    return;
                }
            }
            CurrentPadraoCadastros.CurrentEntity = tb78;
        }        

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB78_PESQ_AVAL tb78 = RetornaEntidade();

            if (tb78 != null)
            {
                tb78.TB73_TIPO_AVALReference.Load();

                ddlAvaliacao.SelectedValue = tb78.TB73_TIPO_AVAL.CO_TIPO_AVAL.ToString();
                CarregaCamposIdentificao();
                CarregaItensAvaliacao();
                ddlPublicoAlvo.SelectedValue = tb78.CO_FLAG_PUBLICO;

                if (ddlPublicoAlvo.SelectedValue == PublicoAlvoAvaliacao.A.ToString())
                {
                    ddlModalidade.SelectedValue = tb78.CO_MODU_CUR.ToString();
                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = tb78.CO_CUR.ToString();
                    CarregaTurma();
                    ddlTurma.SelectedValue = tb78.CO_TUR.ToString();
                    CarregaMaterias();
                    ddlMateria.SelectedValue = tb78.CO_MAT.ToString();
                    CarregaAluno();
                    ddlAluno.SelectedValue = tb78.CO_AVALIA_PESQ.ToString();
                }

                txtSugestao.Text = tb78.DE_SUGE_AVAL;
                txtData.Text = tb78.DT_AVAL.ToString("dd/MM/yyyy");
                txtSugestao.Text = tb78.DE_SUGE_AVAL;

                if (QueryStringAuxili.OperacaoCorrenteQueryString != QueryStrings.OperacaoExclusao)
                {                    
//----------------> Faz a simulação do clique do botão de prosseguir
                    if (ddlItemAvaliacao.Items.Count <= 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Este tipo de avaliação não contém questões");
                        return;
                    }

                    ddlItemAvaliacao.SelectedIndex = 1;
                    DesabilitaCampos();
                    CarregaGrid();
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB78_PESQ_AVAL</returns>
        private TB78_PESQ_AVAL RetornaEntidade()
        {
            return TB78_PESQ_AVAL.RetornaPelaChavePrimaria(codFormAtualQS);
        }
        
        /// <summary>
        /// Desabilita campos do cabeçalho que não podem ser editados
        /// </summary>
        private void DesabilitaCampos()
        {
            ddlMateria.Enabled = txtSugestao.Enabled = txtData.Enabled = ddlAluno.Enabled = ddlAvaliacao.Enabled = ddlColaborador.Enabled =
            ddlModalidade.Enabled = ddlModalidade.Enabled = ddlPublicoAlvo.Enabled = ddlResponsavel.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = false;
        }

        /// <summary>
        /// Faz o carregamento dos campos de identificação da tela de acordo com o público alvo
        /// </summary>
        private void CarregaCamposIdentificao()
        {
            if (ddlPublicoAlvo.SelectedValue == PublicoAlvoAvaliacao.A.ToString())
            {
                liIdentificacao.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = liAluno.Visible = true;
                liColaborador.Visible = liResponsavel.Visible = false;

                CarregaSerieCurso();
                CarregaMaterias();
                CarregaTurma();
                CarregaAluno();
            }
            else if (ddlPublicoAlvo.SelectedValue == PublicoAlvoAvaliacao.F.ToString())
            {
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = liResponsavel.Visible = liAluno.Visible = false;
                liIdentificacao.Visible = liColaborador.Visible = true;

                CarregaFuncionarios();
            }
            else if (ddlPublicoAlvo.SelectedValue == PublicoAlvoAvaliacao.O.ToString())
            {
                liIdentificacao.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = 
                liColaborador.Visible = liResponsavel.Visible = liAluno.Visible = false;
            }
            else if (ddlPublicoAlvo.SelectedValue == PublicoAlvoAvaliacao.P.ToString())
            {
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = liResponsavel.Visible = liAluno.Visible = false;
                liIdentificacao.Visible = liColaborador.Visible = true;

                CarregaProfessores();
            }
            else if (ddlPublicoAlvo.SelectedValue == PublicoAlvoAvaliacao.R.ToString())
            {
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = liColaborador.Visible = liAluno.Visible = false;
                liIdentificacao.Visible = liResponsavel.Visible = true;

                CarregaResponsaveis();
            }
        }
        
        /// <summary>
        /// Salva as respostas do grid de respostas na base de dados
        /// </summary>
        /// <returns>True ou false</returns>
        private bool SalvaRespostas()
        {
            int codTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                TB70_ITEM_AVAL tb70 = null;
                IOrderedDictionary keys = grdBusca.DataKeys[linha.RowIndex].Values;

                int coTituAval = Convert.ToInt32(keys[0]);
                int nuQuesAval = Convert.ToInt32(keys[1]);

                tb70 = TB70_ITEM_AVAL.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, codFormAtualQS, codTipoAval, coTituAval, nuQuesAval);

                decimal nota = 0;

                if (decimal.TryParse(((TextBox)linha.Cells[1].FindControl("txtNota")).Text, out nota))
                {
                    if (nota < 0 || nota > 10)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Nota deve estar entre 0 e 10");
                        return false;
                    }

                    tb70.VL_NOT_AVAL = nota;
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Todas as notas devem ser atribuidas");
                    return false;
                }

                if (GestorEntities.SaveOrUpdate(tb70) < 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar as notas");
                    return false;
                }
            }
            return true;
        }
       
        /// <summary>
        /// Faz a criação do formuário de respostas jogando os valores nos campos de identifação
        /// </summary>
        private void CriaFormularioRespostas()
        {
            string coFlagPublico = ddlPublicoAlvo.SelectedValue;
            int codTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;

            TB78_PESQ_AVAL tb78 = new TB78_PESQ_AVAL();

            tb78.CO_COL = LoginAuxili.CO_COL;
            tb78.CO_CUR = ddlSerieCurso.SelectedValue == "" ? null : (int?)int.Parse(ddlSerieCurso.SelectedValue);
            tb78.CO_EMP = LoginAuxili.CO_EMP;
            tb78.CO_FLAG_PUBLICO = coFlagPublico;
            tb78.CO_MAT = ddlMateria.SelectedValue == "" ? null : (int?)int.Parse(ddlMateria.SelectedValue);
            tb78.CO_MODU_CUR = ddlModalidade.SelectedValue == "" ? null : (int?)int.Parse(ddlModalidade.SelectedValue);
            tb78.CO_TUR = ddlTurma.SelectedValue == "" ? null : (int?)int.Parse(ddlTurma.SelectedValue);
            tb78.DE_SUGE_AVAL = txtSugestao.Text == "" ? null : txtSugestao.Text;
            tb78.DT_AVAL = DateTime.Parse(txtData.Text);
            tb78.TB73_TIPO_AVAL = TB73_TIPO_AVAL.RetornaPelaChavePrimaria(codTipoAval);
            tb78.CO_FLAG_FINAL = "N";

            if (coFlagPublico == PublicoAlvoAvaliacao.A.ToString())
                tb78.CO_AVALIA_PESQ = ddlAluno.SelectedValue != "" ? (int?)int.Parse(ddlAluno.SelectedValue) : null;
            else if (coFlagPublico == PublicoAlvoAvaliacao.R.ToString())
                tb78.CO_AVALIA_PESQ = ddlResponsavel.SelectedValue != "" ? (int?)int.Parse(ddlResponsavel.SelectedValue) : null;
            else if (coFlagPublico == PublicoAlvoAvaliacao.P.ToString() || coFlagPublico == PublicoAlvoAvaliacao.F.ToString())
                tb78.CO_AVALIA_PESQ = ddlColaborador.SelectedValue == "" ? (int?)int.Parse(ddlColaborador.SelectedValue) : null;

//--------> Salva o formulário de respostas
            if (GestorEntities.SaveOrUpdate(tb78) == 1)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar formulário");
                return;
            }

            codFormAtualQS = tb78.CO_PESQ_AVAL;
            
//--------> Faz a criação das respostas correspondentes a cada título do formulário
            foreach (ListItem lstItmAval in ddlItemAvaliacao.Items)
            {
                if (lstItmAval.Value == "")
                    continue;

                CriaRespostasTitulo(int.Parse(lstItmAval.Value));
            }
        }

        /// <summary>
        /// Faz a criação das respostas para um título específico, com as notas vazias
        /// </summary>
        /// <param name="coTituAval">Id do título da avaliação</param>
        private void CriaRespostasTitulo(int coTituAval)
        {
            int codTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;
            
//--------> Recebe as questões do título selecionado
            var lstQuestoes = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                            where tb73.CO_TIPO_AVAL == codTipoAval
                            join tb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros() on tb73.CO_TIPO_AVAL equals tb72.CO_TIPO_AVAL 
                            join tb71 in TB71_QUEST_AVAL.RetornaTodosRegistros() on tb73.CO_TIPO_AVAL equals tb71.CO_TIPO_AVAL
                            where tb71.CO_TITU_AVAL == tb72.CO_TITU_AVAL && tb72.CO_TITU_AVAL == coTituAval
                            select new
                            {
                                tb71.CO_TIPO_AVAL, tb71.CO_TITU_AVAL, tb71.NU_QUES_AVAL
                            }).OrderBy( q => q.NU_QUES_AVAL ).ToList();
            
//--------> Para cada questão existente cria um novo registro de resposta com nota vazia
            foreach (var questao in lstQuestoes)
            {
                TB70_ITEM_AVAL tb70 = TB70_ITEM_AVAL.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, codFormAtualQS, questao.CO_TIPO_AVAL, questao.CO_TITU_AVAL, questao.NU_QUES_AVAL);

                if (tb70 != null) continue;
                tb70 = new TB70_ITEM_AVAL();

                tb70.CO_EMP = LoginAuxili.CO_EMP;
                tb70.CO_PESQ_AVAL = codFormAtualQS;
                tb70.CO_TIPO_AVAL = questao.CO_TIPO_AVAL;
                tb70.CO_TITU_AVAL = questao.CO_TITU_AVAL;
                tb70.NU_QUES_AVAL = questao.NU_QUES_AVAL;
                tb70.TB71_QUEST_AVAL = TB71_QUEST_AVAL.RetornaPelaChavePrimaria(questao.CO_TITU_AVAL, questao.CO_TIPO_AVAL, questao.NU_QUES_AVAL);
                tb70.TB78_PESQ_AVAL = TB78_PESQ_AVAL.RetornaPelaChavePrimaria(codFormAtualQS);
                tb70.VL_NOT_AVAL = null;

                if (GestorEntities.SaveOrUpdate(tb70) < 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao gerar formulário de respostas");
                    return;
                }
            }
        }

        /// <summary>
        /// Método que carrega a grid de Questões
        /// </summary>
        private void CarregaGrid()
        {
            string coFlagPublico = ddlPublicoAlvo.SelectedValue;
            int codTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;
            int coTituAval = ddlItemAvaliacao.SelectedValue != "" ? int.Parse(ddlItemAvaliacao.SelectedValue) : 0;


            grdBusca.DataKeyNames = new string[] { "CO_TITU_AVAL", "NU_QUES_AVAL" };

            grdBusca.DataSource = from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                  where tb73.CO_TIPO_AVAL == codTipoAval
                                  join tb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros() on tb73.CO_TIPO_AVAL equals tb72.CO_TIPO_AVAL 
                                  join tb71 in TB71_QUEST_AVAL.RetornaTodosRegistros() on tb72.CO_TIPO_AVAL equals tb71.CO_TIPO_AVAL
                                  join tb78 in TB78_PESQ_AVAL.RetornaTodosRegistros() on tb73.CO_TIPO_AVAL equals tb78.TB73_TIPO_AVAL.CO_TIPO_AVAL                                        
                                  join tb70 in TB70_ITEM_AVAL.RetornaTodosRegistros()
                                  on tb71.CO_TIPO_AVAL equals tb70.CO_TIPO_AVAL 
                                  where tb70.CO_TITU_AVAL == tb71.CO_TITU_AVAL && tb70.NU_QUES_AVAL == tb71.NU_QUES_AVAL 
                                  && tb70.CO_PESQ_AVAL == tb78.CO_PESQ_AVAL && tb72.CO_TITU_AVAL == coTituAval && tb71.CO_TITU_AVAL == tb72.CO_TITU_AVAL 
                                  && tb78.CO_FLAG_PUBLICO == coFlagPublico && tb78.CO_PESQ_AVAL == codFormAtualQS
                                  select new
                                  {
                                       tb71.CO_TITU_AVAL, tb71.NU_QUES_AVAL, tb71.DE_QUES_AVAL, tb70.VL_NOT_AVAL
                                  };
            grdBusca.DataBind();
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Público Alvo
        /// </summary>
        private void CarregaPublicosAlvo()
        {
            ddlPublicoAlvo.Load<PublicoAlvoAvaliacao>();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades Escolares
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
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurma.Items.Clear();

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).OrderBy( t => t.NO_TURMA );

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   where (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                                   && (serie != 0 ? tb08.CO_CUR == serie : serie == 0) && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                   && (turma != 0 ? tb08.CO_TUR == turma : turma == 0)
                                   select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy( m => m.NO_ALU );

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Avaliação
        /// </summary>
        private void CarregaTiposAvaliacao()
        {
            ddlAvaliacao.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                       select new { tb73.NO_TIPO_AVAL, tb73.CO_TIPO_AVAL });

            ddlAvaliacao.DataTextField = "NO_TIPO_AVAL";
            ddlAvaliacao.DataValueField = "CO_TIPO_AVAL";
            ddlAvaliacao.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            liMateria.Visible = true;

            ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                     where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                     join tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                     on tb43.CO_MAT equals tb02.CO_MAT
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                                     on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).OrderBy( m => m.NO_MATERIA );

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "CO_MAT";
            ddlMateria.DataBind();

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Itens de Avaliação
        /// </summary>
        private void CarregaItensAvaliacao()
        {
            int coTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;

            ddlItemAvaliacao.DataSource = (from tb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros()
                                           where tb72.CO_TIPO_AVAL == coTipoAval
                                           select new { tb72.CO_TITU_AVAL, tb72.NO_TITU_AVAL });

            ddlItemAvaliacao.DataTextField = "NO_TITU_AVAL";
            ddlItemAvaliacao.DataValueField = "CO_TITU_AVAL";
            ddlItemAvaliacao.DataBind();

            ddlItemAvaliacao.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Professores
        /// </summary>
        private void CarregaProfessores()
        {
            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         where tb03.FLA_PROFESSOR == "S"
                                         select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         where tb03.FLA_PROFESSOR == "N"
                                         select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Responsáveis
        /// </summary>
        private void CarregaResponsaveis()
        {
            ddlResponsavel.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                         join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                         on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                         select new { tb108.CO_RESP, tb108.NO_RESP }).Distinct().OrderBy( r => r.NO_RESP );

            ddlResponsavel.DataTextField = "NO_RESP";
            ddlResponsavel.DataValueField = "CO_RESP";
            ddlResponsavel.DataBind();

            ddlResponsavel.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion 
   
        protected void btnProsseguir_Click(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString != QueryStrings.OperacaoDetalhe)
            {
                if (ddlItemAvaliacao.Items.Count <= 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Este tipo de avaliação não contém questões");
                    return;
                }

                if (ddlItemAvaliacao.SelectedIndex == 0)
                {
                    ddlItemAvaliacao.SelectedIndex++;
                    DesabilitaCampos();
                    CriaFormularioRespostas();
                    CarregaGrid();
                }
                else if (ddlItemAvaliacao.SelectedIndex < ddlItemAvaliacao.Items.Count - 1)
                {
                    if (SalvaRespostas())
                    {
                        ddlItemAvaliacao.SelectedIndex++;
                        CarregaGrid();
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Questionário finalizado! Clique em salvar");
                    return;
                }
            }
            else
            {
                if (ddlItemAvaliacao.SelectedIndex == 0)
                {
                    ddlItemAvaliacao.SelectedIndex++;
                    DesabilitaCampos();
                    CarregaGrid();
                }
                else if (ddlItemAvaliacao.SelectedIndex < ddlItemAvaliacao.Items.Count - 1)
                {
                    ddlItemAvaliacao.SelectedIndex++;
                    CarregaGrid();
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Questionário finalizado.");
                    return;
                }
            }
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaMaterias();
            CarregaTurma();
            CarregaAluno();
            CarregaItensAvaliacao();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            CarregaTurma();
            CarregaAluno();
            CarregaItensAvaliacao();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaItensAvaliacao();
        }

        protected void ddlAvaliacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCamposIdentificao();
            CarregaItensAvaliacao();
        }

        protected void ddlItemAvaliacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void ddlPublicoAlvo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCamposIdentificao();
        }
    }
}
