//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: GERA GRADE DE HORÁRIO PARA TURMA (MODALIDADE/SÉRIE) 
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3025_GeraGradeHorarioTurma
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaModalidades();
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                CarregaSerieCurso(modalidade);
                CarregaTurma(modalidade);
                CarregaDiaSemana();
                CarregaTempoAula();
                CarregaProfessor();

                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurno.Items.Insert(0, new ListItem("Selecione", ""));
                ddlAno.Items.Insert(0, new ListItem("Selecione", ""));

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    ddlAno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = ddlDisciplina.Enabled =
                    ddlDiaSemana.Enabled = ddlTurno.Enabled = ddlTempoAula.Enabled = divGridView.Visible = ddlProfessor.Enabled = ddlTpHorario.Enabled = true;
                    ulDados.Style.Add("margin-right", "350px;");
                    ulDados.Style.Add("margin-left", "338px;");
                }
                else
                {
                    ulDados.Style.Add("margin-right", "259px;");
                    ulDados.Style.Add("margin-left", "338px !important;");
                    divGridView.Visible = false;
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB05_GRD_HORAR tb05 = RetornaEntidade();

            if (tb05.CO_EMP == 0 && tb05.CO_CUR == 0 && tb05.CO_TUR == 0)
            {
                if ((ddlTpHorario.SelectedValue == "DEP") && (ddlProfessor.SelectedValue == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou o tipo de grade Dependência, favor selecionar o Professor responsável.");
                    return;
                }

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                int coAnoGrade = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                int diaSemana = ddlDiaSemana.SelectedValue != "" ? int.Parse(ddlDiaSemana.SelectedValue) : 0;
                int tempoAula = ddlTempoAula.SelectedValue != "" ? int.Parse(ddlTempoAula.SelectedValue) : 0;
                int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

                int ocorTb05 = (from lTb05 in TB05_GRD_HORAR.RetornaTodosRegistros()
                               where lTb05.CO_EMP == LoginAuxili.CO_EMP && lTb05.CO_MODU_CUR == modalidade && lTb05.CO_CUR == serie && lTb05.CO_TUR == turma
                               && lTb05.CO_DIA_SEMA_GRD == diaSemana && lTb05.NR_TEMPO == tempoAula && lTb05.CO_ANO_GRADE == coAnoGrade
                               && lTb05.TP_TURNO == ddlTurno.SelectedValue && lTb05.CO_MAT == materia
                               select new { lTb05.CO_EMP }).Count();
                

                if (ocorTb05 > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe registro cadastrado com essas informações");
                    return;
                }

                tb05 = new TB05_GRD_HORAR();
                tb05.CO_EMP = LoginAuxili.CO_EMP;
                tb05.TP_HORAR_AGEND = ddlTpHorario.SelectedValue;
                tb05.CO_COL = (ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : (int?)null);
                tb05.CO_MODU_CUR = modalidade;
                tb05.CO_CUR = serie;
                tb05.CO_TUR = turma;
                tb05.CO_ANO_GRADE = coAnoGrade;
                tb05.CO_MAT = materia;
                tb05.CO_DIA_SEMA_GRD = diaSemana;
                tb05.TP_TURNO = ddlTurno.SelectedValue;
                tb05.NR_TEMPO = tempoAula;
                tb05.DT_CADAS = DateTime.Now;
                tb05.TB131_TEMPO_AULA = TB131_TEMPO_AULA.RetornaPelaChavePrimaria(tb05.CO_EMP, tb05.CO_MODU_CUR, tb05.CO_CUR, tb05.NR_TEMPO, tb05.TP_TURNO);
            }

            CurrentPadraoCadastros.CurrentEntity = tb05;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB05_GRD_HORAR tb05 = RetornaEntidade();

            if (tb05 != null)
            {
                tb05.TB131_TEMPO_AULAReference.Load();

                ddlModalidade.SelectedValue = tb05.CO_MODU_CUR.ToString();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao) 
                    || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                {
                    CarregaSerieCurso(tb05.CO_MODU_CUR);
                    ddlSerieCurso.SelectedValue = tb05.CO_CUR.ToString();
                    CarregaTurma(tb05.CO_MODU_CUR);
                }

                ddlTurma.SelectedValue = tb05.CO_TUR.ToString();
                ddlDisciplina.SelectedValue = tb05.CO_MAT.ToString();
                ddlDiaSemana.SelectedValue = tb05.CO_DIA_SEMA_GRD.ToString();
                ddlTurno.SelectedValue = tb05.TP_TURNO;
                CarregaTempoAula();
                ddlTempoAula.SelectedValue = tb05.NR_TEMPO.ToString();
                ddlProfessor.SelectedValue = tb05.CO_COL.ToString();

                ddlTpHorario.SelectedValue = tb05.TP_HORAR_AGEND.ToString();

                CarregaAnos();
                ddlAno.SelectedValue = tb05.CO_ANO_GRADE.ToString().Trim();

                if (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    CarregaDisciplina(tb05.CO_MODU_CUR, tb05.CO_CUR, tb05.CO_ANO_GRADE.ToString());
                    ddlDisciplina.SelectedValue = tb05.CO_MAT.ToString();
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB05_GRD_HORAR</returns>
        private TB05_GRD_HORAR RetornaEntidade()
        {
            TB05_GRD_HORAR tb05 = TB05_GRD_HORAR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp),
                                                            QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur),
                                                            QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur),
                                                            QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur),
                                                            QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoMat),
                                                            QueryStringAuxili.RetornaQueryStringPelaChave("CoDia") != null ? int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("CoDia")) : 0,
                                                            QueryStringAuxili.RetornaQueryStringPelaChave("TpTurno"),
                                                            QueryStringAuxili.RetornaQueryStringPelaChave("NuTemp") != null ? int.Parse(QueryStringAuxili.RetornaQueryStringPelaChave("NuTemp")) : 0,
                                                            QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Ano));
            return (tb05 == null) ? new TB05_GRD_HORAR() : tb05;
        }

        #endregion        

        #region Carregamento

        /// <summary>
        /// Método que carrega o grid de Grade de Turma
        /// </summary>
        private void CarregaGridGradeTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAnoGrade = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            var resultado = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             join tb05 in TB05_GRD_HORAR.RetornaTodosRegistros() on tb02.CO_MAT equals tb05.CO_MAT
                             where tb05.CO_EMP == LoginAuxili.CO_EMP && tb05.CO_ANO_GRADE == coAnoGrade && tb05.CO_MODU_CUR == modalidade
                             && tb05.CO_CUR == serie && tb05.CO_TUR == turma && tb05.CO_MAT == materia
                             select new
                             {
                                 tb05.CO_EMP, tb05.CO_MODU_CUR, tb05.CO_CUR, tb05.CO_TUR, tb02.CO_MAT, tb107.NO_MATERIA, tb05.CO_DIA_SEMA_GRD,
                                 NU_TEMP_AULA_GRD = tb05.NR_TEMPO, tb05.CO_ANO_GRADE, HR_INIC_AULA_GRD = tb05.TB131_TEMPO_AULA.HR_INICIO,
                                 HR_TERM_AULA_GRD = tb05.TB131_TEMPO_AULA.HR_TERMI,
                                 NO_DIA_SEMA_GRD = (tb05.CO_DIA_SEMA_GRD.Equals(0) ? "Domingo" :
                                                    (tb05.CO_DIA_SEMA_GRD.Equals(1) ? "Segunda-Feira" :
                                                    (tb05.CO_DIA_SEMA_GRD.Equals(2) ? "Terça-Feira" :
                                                    (tb05.CO_DIA_SEMA_GRD.Equals(3) ? "Quarta-Feira" :
                                                    (tb05.CO_DIA_SEMA_GRD.Equals(4) ? "Quinta-Feira" :
                                                    (tb05.CO_DIA_SEMA_GRD.Equals(5) ? "Sexta-Feira" :
                                                    (tb05.CO_DIA_SEMA_GRD.Equals(6) ? "Sábado" : ""))))))),
                                 N_TEMP_AULA_GRD = (tb05.NR_TEMPO.Equals(1) ? "1º Tempo" :
                                                    (tb05.NR_TEMPO.Equals(2) ? "2º Tempo" :
                                                    (tb05.NR_TEMPO.Equals(3) ? "3º Tempo" :
                                                    (tb05.NR_TEMPO.Equals(4) ? "4º Tempo" :
                                                    (tb05.NR_TEMPO.Equals(5) ? "5º Tempo" :
                                                    (tb05.NR_TEMPO.Equals(6) ? "6º Tempo" :
                                                    (tb05.NR_TEMPO.Equals(7) ? "7º Tempo" : ""))))))),
                             }).OrderBy( m => m.NO_MATERIA );

            liGridView.Visible = true;
            GrdGradeTurma.DataSource = resultado;
            GrdGradeTurma.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                 where tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                 select new 
                                 {
                                     CO_ANO_GRADE = tb43.CO_ANO_GRADE.Trim()
                                 }).OrderByDescending(g => g.CO_ANO_GRADE).Distinct().OrderByDescending(w => w.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("Selecione", ""));

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

            CarregaAnos();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso(int? coModuCur)
        {
            int modalidade = 0;         
            string anoGrade = ddlAno.SelectedValue;

            ddlAno.Items.Clear();
            ddlTurma.Items.Clear();

            if (ddlModalidade.SelectedValue != "" || coModuCur > 0)
            {
                if (!String.IsNullOrEmpty(coModuCur.ToString()))
                    modalidade = Convert.ToInt32(coModuCur);
                else
                    modalidade = int.Parse(ddlModalidade.SelectedValue);

                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).Distinct().OrderBy(g => g.CO_SIGL_CUR);

                ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma(int? coModuCur)
        {
            int modalidade;
            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método responsável pelo carregamento dos professores. Se o Tipo desta grade for Dependencia, carrega todos, caso não seja, carrega apenas os professores associados
        /// </summary>
        private void CarregaProfessor()
        {
            if (ddlTpHorario.SelectedValue == "DEP")
            {
                AuxiliCarregamentos.carregaProfessores(ddlProfessor, LoginAuxili.CO_EMP);
            }
            else
            {
                
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serieCurso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                //int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
                int coUnid = LoginAuxili.CO_EMP;
                int disciplina = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
                int ano = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);

                AuxiliCarregamentos.CarregaProfessoresRespMateria(ddlProfessor, coUnid, modalidade, serieCurso, turma, disciplina, ano);
            }

            ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        /// <param name="coModalidade">Id da modalidade</param>
        /// <param name="coCur">Id da série</param>
        /// <param name="ano">Ano de referência</param>
        private void CarregaDisciplina(int coModalidade, int coCur, string ano)
        {
            ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                        join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb02.CO_MAT equals tb43.CO_MAT
                                        where tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.CO_ANO_GRADE == ano && tb43.TB44_MODULO.CO_MODU_CUR == coModalidade && tb43.CO_CUR == coCur
                                        select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy( m => m.NO_MATERIA );

            ddlDisciplina.DataValueField = "CO_MAT";
            ddlDisciplina.DataTextField = "NO_MATERIA";
            ddlDisciplina.DataBind();

        }

        /// <summary>
        /// Método que carrega o dropdown de Dias da Semana
        /// </summary>
        private void CarregaDiaSemana()
        {
            ddlDiaSemana.Items.Insert(0, new ListItem("Sábado", "6"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Sexta-Feira", "5"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Quinta-Feira", "4"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Quarta-Feira", "3"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Terça-Feira", "2"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Segunda-Feira", "1"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Domingo", "0"));
            ddlDiaSemana.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tempo de Aula
        /// </summary>
        private void CarregaTempoAula()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string strTurno = ddlTurno.SelectedValue;

            ddlTempoAula.DataSource = from tb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                                      where tb131.CO_EMP == LoginAuxili.CO_EMP && tb131.CO_MODU_CUR == modalidade && tb131.CO_CUR == serie && tb131.TP_TURNO == strTurno
                                      select new { tb131.NR_TEMPO, HORARIO = tb131.HR_INICIO + " - " + tb131.HR_TERMI };

            ddlTempoAula.DataTextField = "HORARIO";
            ddlTempoAula.DataValueField = "NR_TEMPO";
            ddlTempoAula.DataBind();

            ddlTempoAula.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void BindGrdGradeTurma(int newPageIndex)
        {
            CarregaGridGradeTurma();
            GrdGradeTurma.PageIndex = newPageIndex;
            GrdGradeTurma.DataBind();
        }

        /// <summary>
        /// Método que faz a chamada de outros métodos que carregam os DropDowns
        /// </summary>
        private void CarregaCombos()
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaDiaSemana();
            CarregaTempoAula();
        }

        /// <summary>
        /// Método que faz a verificação se os campos estão selecionados ou não
        /// </summary>
        private void VerificaCamposNaoSelecionados()
        {
            if (ddlModalidade.SelectedValue.Trim() == "")
                lblMsg.Text = " Selecione a Modalidade";
            else if (ddlSerieCurso.SelectedValue.Trim() == "")
                lblMsg.Text = " Selecione a Série/Curso";
            else if (ddlTurma.SelectedValue.Trim() == "")
                lblMsg.Text = " Selecione a Turma";
            else if (ddlAno.SelectedValue.Trim() == "")
                lblMsg.Text = " Selecione o Ano";
            else if (ddlDisciplina.SelectedValue.Trim() == "")
                lblMsg.Text = " Selecione a Disciplina";
            else
                lblMsg.Text = "";
        }
        #endregion

        protected void GrdGradeTurma_DataBound(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = GrdGradeTurma.BottomPagerRow;

            if (gridViewRow != null)
            {
                DropDownList listaPaginas = (DropDownList)gridViewRow.Cells[0].FindControl("ddlGrdPages");

                if (listaPaginas != null)
                    for (int i = 0; i < GrdGradeTurma.PageCount; i++)
                    {
                        int numeroPagina = i + 1;
                        ListItem lstItem = new ListItem(numeroPagina.ToString());

                        if (i == GrdGradeTurma.PageIndex)
                            lstItem.Selected = true;

                        listaPaginas.Items.Add(lstItem);
                    }
            }
        }

        protected void GrdGradeTurma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrdGradeTurma(e.NewPageIndex);
        }

        protected void ddlGrdPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = GrdGradeTurma.BottomPagerRow;
            if (gridViewRow != null)
            {
                DropDownList listaPaginas = (DropDownList)gridViewRow.Cells[0].FindControl("ddlGrdPages");
                BindGrdGradeTurma(listaPaginas.SelectedIndex);
            }
        }       

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            liGridView.Visible = false;

            ddlSerieCurso.Items.Clear();
            ddlAno.Items.Clear();
            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();

            if (ddlModalidade.SelectedValue != "")
            {
                int modalidade = int.Parse(ddlModalidade.SelectedValue);

                CarregaSerieCurso(modalidade);
                CarregaAnos();
                ddlAno.SelectedValue = DateTime.Now.Year.ToString();
                CarregaTurma(modalidade);
                CarregaProfessor();

                if (ddlSerieCurso.SelectedValue != "")
                    CarregaDisciplina(modalidade, int.Parse(ddlSerieCurso.SelectedValue), ddlAno.SelectedValue);
            }

            VerificaCamposNaoSelecionados();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            liGridView.Visible = false;

            if (ddlModalidade.SelectedValue != "")
            {
                int modalidade = int.Parse(ddlModalidade.SelectedValue);

                if (ddlSerieCurso.SelectedValue != "")
                    CarregaDisciplina(modalidade, int.Parse(ddlSerieCurso.SelectedValue), ddlAno.SelectedValue);
            }

            VerificaCamposNaoSelecionados();
            CarregaProfessor();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            liGridView.Visible = false;

            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();

            if (ddlModalidade.SelectedValue != "")
            {
                int modalidade = int.Parse(ddlModalidade.SelectedValue);

                CarregaTurma(modalidade);

                if (ddlSerieCurso.SelectedValue != "" && ddlTurma.SelectedValue != "")
                    CarregaDisciplina(modalidade, int.Parse(ddlSerieCurso.SelectedValue), ddlAno.SelectedValue);
            }

            VerificaCamposNaoSelecionados();
            CarregaProfessor();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            liGridView.Visible = false;

            ddlDisciplina.Items.Clear();

            if (ddlTurma.SelectedValue != "")
                CarregaDisciplina(int.Parse(ddlModalidade.SelectedValue), int.Parse(ddlSerieCurso.SelectedValue), ddlAno.SelectedValue);

            VerificaCamposNaoSelecionados();
            CarregaProfessor();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (ddlDisciplina.SelectedValue != "")
                BindGrdGradeTurma(0);

            CarregaProfessor();
        }

        protected void ddlTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTempoAula();
        }

        protected void ddlTpHorario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfessor();
        }
    }
}