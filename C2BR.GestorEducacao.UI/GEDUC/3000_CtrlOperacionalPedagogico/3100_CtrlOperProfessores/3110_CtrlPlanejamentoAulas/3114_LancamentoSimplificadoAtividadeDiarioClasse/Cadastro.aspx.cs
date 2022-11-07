//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: LANÇAMENTO DAS ATIVIDADES LETIVAS REALIZADAS PELO PROFESSOR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 10/04/2013| André Nobre Vinagre        | Corrigida inconsistencia quando selecionava atividade
// ----------+----------------------------+-------------------------------------
// 25/04/2013| Victor Martins Machado     | Alteração feita na combo ddlTipoAtiv, que passou a pegar
//           |                            | seus valores da tabela TB273.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 18/03/2014| Victor Martins Machado     | Criação do if para retornar a matéria padrão para turma única
//           |                            | na função CarregaMaterias (
//           |                            | esta alteração não está completa por um imprevisto que precisa ser revisado 
//           |                            | com o Cordova). Mas o sistema está rodando sem erro.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 18/03/2014| Victor Martins Machado     | Alteração da regra do campo TURNO, das informações da atividade.
//           |                            | Este campo será desabilitado quando o tipo de registro for igual
//           |                            | a "N", Sem Registro de Tempo.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 20/03/2014| Victor Martins Machado     | Alteração da regra dos campos de horário de início e término da atividade.
//           |                            | Agora o sistema valida as seguintes possibilidades:
//           |                            |         1. Valida se os horários são válidos, se são horários válidos;
//           |                            |         2. Valida se o horário de término é maior que o horário de início, quando
//           |                            |         pelo menos um dos campos são informados;
//           |                            |         3. Caso não exista valor informado nos campos, o sistema utiliza "00:00"
//           |                            |         nos dois campos.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 28/03/2014| Victor Martins Machado     | Desabilitado o botão salvar da barra de cadastro para evitar o erro 
//           |                            | do lançamento de frequência sem planejamento, enquanto a funcionalidade
//           |                            | é adequada.
//           |                            | 
// 03/07/2014| Maxwell Almeida            |
// ----------+----------------------------+-------------------------------------
// 14/11/2021| Fabricio S dos Santos      |Adição de parametro de tempo ao redirecionar para funcionalidade de frequência
//
//
//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Web;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3114_LancamentoSimplificadoAtividadeDiarioClasse
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Desabilita o botão salvar da barra de cadastro (temporário)
            //CurrentPadraoCadastros.BarraCadastro.HabilitarBotoes("btnSave", false);

            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaMedidas();
                txtDataCad.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataRe.Text = DateTime.Now.ToString();

                TB03_COLABOR colL = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

                //Carregamentos valores padrões nos campos
                //ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
                //ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                //ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlMes.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", "0"));
                CarregarMeses();

                if (colL.FLA_PROFESSOR == "S")
                {
                    CarregaProfessor();
                    ddlProfessor.SelectedValue = colL.CO_COL.ToString();
                    ddlProfessor.Enabled = false;

                    CarregaModalidades();
                    //CarregaSerieCurso();
                    //CarregaTurma();
                }
                else
                {
                    CarregaProfessor();
                    //CarregaSerieCurso();
                    //CarregaTurma();
                }
            }

            if (IsPostBack) return;

        }

        //====> Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tipo, false, true);
        }

        bool AcaoBarraCadastro()
        {
            DateTime dataRealizacao = Convert.ToDateTime(txtDataRe.Text);
            int quantidadeTempos = 0;
            Dictionary<int, temposSeries> temposEscolhidos = new Dictionary<int, temposSeries>();
            int coProfReal = int.Parse(ddlProfessorReal.SelectedValue);
            bool selecDisci = false;
            bool selecTempo = false;

            if (ddlMes.SelectedValue == "0" || ddlMes.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "O Mês não foi selecionado.");
                return false;
            }

            if (dataRealizacao.Year.ToString() != ddlAno.SelectedValue.Replace(" ", ""))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Ano da Data de Realização está diferente do Ano de Referência informado");
                return false;
            }

            if (dataRealizacao > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de realização não pode ser maior que data atual.");
                return false;
            }

            TB119_ATIV_PROF_TURMA tb119;
            string horarioInicio, horarioTermino;
            horarioInicio = horarioTermino = "";
            int numeroTempo = 0;
            //Opção de bimestre referência inutilizada por falta de necessidade
            string strCoRef = "SR";//ddlRefer.SelectedValue;

            string coAnoRefPla = ddlAno.SelectedValue;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coColProf = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int coCol = ddlCocol.SelectedValue != "" ? int.Parse(ddlCocol.SelectedValue) : 0;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Para cadastrar um registro que não está no Plano de Aula, os campos Modalidade, Série/Curso, Turma e Disciplina são requeridos");
                return false;
            }

            if (ddlTipoAtiv.SelectedValue == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de atividade correspondente");
                return false;
            }

            //Parte responsável por validar se foi selecinada ao menos uma disciplina
            #region Valida Disciplina

            foreach (GridViewRow grD in grdDisciplinas.Rows)
            {
                if (((CheckBox)grD.Cells[0].FindControl("ckSelect")).Checked)
                {
                    selecDisci = true;
                    break;
                }
            }

            if (selecDisci == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos uma Disciplina para salvar os dados");
                return false;
            }
            #endregion

            foreach (GridViewRow li in grdDisciplinas.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked)
                {
                    int CoMat = int.Parse(((HiddenField)li.Cells[0].FindControl("hdCoMat")).Value);
                    //Coleta o nome da Disciplina selecionada para melhor tratar os erros subsequentes
                    string noMat = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                    join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                    where tb02.CO_MAT == CoMat
                                    select new { tb107.NO_MATERIA }).FirstOrDefault().NO_MATERIA;

                    if (string.IsNullOrEmpty(((TextBox)li.Cells[3].FindControl("txtTemaAula")).Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Tema da Aula para a Disciplina " + noMat);
                        return false;
                    }

                    if (string.IsNullOrEmpty(((TextBox)li.Cells[4].FindControl("txtDescAtividade")).Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Descrição da Aula para a Disciplina " + noMat);
                        return false;
                    }

                    //Coleta as informações da Grid
                    string Tema = ((TextBox)li.Cells[3].FindControl("txtTemaAula")).Text;
                    string descricao = (((TextBox)li.Cells[4].FindControl("txtDescAtividade")).Text);
                    ListBox lisbox = ((ListBox)li.Cells[2].FindControl("lstTempo"));

                    selecTempo = false;
                    //Salva os registros de acordo com os tempos selecionados no listbox
                    foreach (ListItem listb in lisbox.Items)
                    {
                        if (listb.Selected == true)
                        {

                            tb119 = new TB119_ATIV_PROF_TURMA();

                            TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(int.Parse(ddlTipoAtiv.SelectedValue));

                            tb119.CO_MAT = CoMat;
                            tb119.DE_TEMA_AULA = Tema;
                            tb119.DE_RES_ATIV = descricao;

                            tb119.CO_ANO_MES_MAT = coAnoRefPla;
                            tb119.CO_EMP = LoginAuxili.CO_EMP;
                            tb119.CO_MODU_CUR = modalidade;
                            tb119.CO_CUR = serie;
                            tb119.CO_TUR = turma;
                            tb119.CO_COL = coCol;
                            tb119.CO_COL_ATIV = coColProf;
                            tb119.CO_PLA_AULA = null;
                            tb119.DT_ATIV_REAL = DateTime.Parse(txtDataRe.Text);
                            tb119.HR_INI_ATIV = horarioInicio;
                            tb119.HR_TER_ATIV = horarioTermino;
                            tb119.CO_TEMPO_ATIV = numeroTempo;
                            tb119.CO_ATIV_PROF_REAL = coProfReal;
                            tb119.DT_REGISTRO_ATIV = DateTime.Now;
                            tb119.FLA_AULA_PLAN = false;
                            tb119.CO_IP_CADAST = LoginAuxili.IP_USU;
                            tb119.DT_ATIV_REAL_TERM = DateTime.Parse(txtDataRe.Text);
                            tb119.CO_TIPO_ATIV = tb273.CO_SIGLA_ATIV;
                            tb119.FL_AVALIA_ATIV = ddlAvaliaAtiv.SelectedValue;
                            tb119.CO_REFER_ATIV = strCoRef;
                            tb119.TB273_TIPO_ATIVIDADE = tb273;
                            tb119.FL_LANCA_NOTA = "S";

                            //Trata o tempo
                            int tempo = listb.Value != "N" ? int.Parse(listb.Value) : 0;
                            string deTempo = listb.Text;
                            tb119.HR_INI_ATIV = listb.Value != "N" ? deTempo.Substring(4, 5) : "0";
                            tb119.HR_TER_ATIV = listb.Value != "N" ? deTempo.Substring(12, 5) : "0";
                            tb119.CO_TEMPO_ATIV = tempo;
                            selecTempo = true;
                            quantidadeTempos++;
                            TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119);
                            Session["TB119_ATIV_PROF_TURMA"] = tb119;
                        }
                    }

                    //Valida se ao menos um tempo foi selecionado para a disciplina em questão
                    if (selecTempo == false)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos um tempo para a Disciplina " + noMat);
                        return false;
                    }
                }
            }
            HttpContext.Current.Session.Add("auxQtTempos", quantidadeTempos);
            return true;
        }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (!AcaoBarraCadastro())
                return;
            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
            //ddlAno.Items.Insert(0, new ListItem("Selecione", "0"));
            //ddlAno.SelectedValue = DateTime.Now.Year.ToString();

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            //ddlMes.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            int professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, professor, ano, false);
            CarregaSerieCurso();

            //ddlSerieCurso.Items.Clear();
            //ddlTurma.Items.Clear();
            //ddlMes.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int anoGrade = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            string ano = ddlAno.SelectedValue;
            AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, professor, anoGrade, false);
            verificaTurmaUnica();
            CarregaTurma();

            //ddlMes.Items.Clear();
            //CarregaMaterias();
        }
        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int anoGrade = (ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, professor, anoGrade, false);
            CarregarMeses();
        }

        /// <summary>
        /// Inseri os meses disponíveis para o filtro
        /// </summary>
        private void CarregarMeses()
        {
            ddlMes.Items.Clear();
            ddlMes.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlMes.Items.Insert(1, new ListItem("Todos", "-1"));
            ddlMes.Items.Insert(2, new ListItem("Janeiro", "1"));
            ddlMes.Items.Insert(3, new ListItem("Fevereiro", "2"));
            ddlMes.Items.Insert(4, new ListItem("Março", "3"));
            ddlMes.Items.Insert(5, new ListItem("Abril", "4"));
            ddlMes.Items.Insert(6, new ListItem("Maio", "5"));
            ddlMes.Items.Insert(7, new ListItem("Junho", "6"));
            ddlMes.Items.Insert(8, new ListItem("Julho", "7"));
            ddlMes.Items.Insert(9, new ListItem("Agosto", "8"));
            ddlMes.Items.Insert(10, new ListItem("Setembro", "9"));
            ddlMes.Items.Insert(11, new ListItem("Outubro", "10"));
            ddlMes.Items.Insert(12, new ListItem("Novembro", "11"));
            ddlMes.Items.Insert(13, new ListItem("Dezembro", "12"));
            ddlMes.SelectedValue = "0";
        }
        /// <summary>
        /// Método que carrega o dropdown de Professor
        /// </summary>
        private void CarregaProfessor()
        {
            //ddlProfessor.Items.Clear();

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var colabores = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                 where tb03.FLA_PROFESSOR == "S"
                                 select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
                ddlProfessor.DataSource = colabores;

                ddlProfessor.DataTextField = "NO_COL";
                ddlProfessor.DataValueField = "CO_COL";
                ddlProfessor.DataBind();

                ddlProfessor.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlProfessor.SelectedValue = "0";

                ddlProfessorReal.Items.Clear();
                ddlProfessorReal.DataSource = colabores;
            }
            else
            {
                var colabores = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                 where tb03.FLA_PROFESSOR == "S"
                                 && tb03.CO_COL == LoginAuxili.CO_COL
                                 select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);
                ddlProfessor.DataSource = colabores;

                ddlProfessor.DataTextField = "NO_COL";
                ddlProfessor.DataValueField = "CO_COL";
                ddlProfessor.DataBind();

                ddlProfessor.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlProfessor.SelectedValue = "0";

                ddlProfessorReal.Items.Clear();
                ddlProfessorReal.DataSource = colabores;
            }

            #region Campo realizador


            ddlProfessorReal.DataTextField = "NO_COL";
            ddlProfessorReal.DataValueField = "CO_COL";
            ddlProfessorReal.DataBind();

            ddlProfessorReal.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlProfessorReal.SelectedValue = "0";
            #endregion

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            //ddlMes.Items.Clear();
        }
        /// <summary>
        /// Método que carrega o dropdown de Professor Homologação
        /// </summary>
        private void CarregaProfessorHomolog()
        {
            ddlCocol.Items.Clear();
            ddlCocol.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlCocol.DataTextField = "NO_COL";
            ddlCocol.DataValueField = "CO_COL";
            ddlCocol.DataBind();

            ddlCocol.SelectedValue = LoginAuxili.CO_COL.ToString();
        }
        /// <summary>
        /// Carrega os tipos de atividade da tabela TB273_TIPO_ATIVIDADE
        /// </summary>
        private void CarregaTipoAtividade()
        {
            ddlTipoAtiv.Items.Clear();
            ddlTipoAtiv.DataSource = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                                      where tb273.CO_SITUA_ATIV == "A"
                                      select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV }).OrderBy(o => o.NO_TIPO_ATIV);

            ddlTipoAtiv.DataTextField = "NO_TIPO_ATIV";
            ddlTipoAtiv.DataValueField = "ID_TIPO_ATIV";
            ddlTipoAtiv.DataBind();

            ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTipoAtiv.SelectedValue = "0";
        }

        /// <summary>
        /// Verifica a configuração de turma única no cadastro do curso selecionado.
        /// </summary>
        private void verificaTurmaUnica()
        {
            if (ddlSerieCurso.SelectedValue != "")
            {
                CarregaTurma();

                int coCur = int.Parse(ddlSerieCurso.SelectedValue);
                string valTipoFreqSerie = TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == coCur).FirstOrDefault().CO_PARAM_FREQUE;
                if (valTipoFreqSerie == "D")
                {
                    hidTpAulaRequired.Value = "N";
                }
                else
                {
                    hidTpAulaRequired.Value = "S";
                }
            }
        }

        /// <summary>
        /// Carrega os tempos cadastrados
        /// </summary>
        private void CarregaTempo(ListBox ddl, int? codigo = null)
        {
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coSer = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (coMod != 0 && coSer != 0 && coTur != 0)
            {
                var turma = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coMod, coSer, coTur);
                if (turma != null && turma.CO_PERI_TUR != null)
                {
                    Boolean MarcaTempo = false;
                    if (codigo != null)
                    {
                        MarcaTempo = true;
                    }
                    var tempos = (from tb131 in TB131_TEMPO_AULA.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                  where (coMod == -1 ? 0 == 0 : tb131.CO_MODU_CUR == coMod)
                                  && (coSer == -1 ? 0 == 0 : tb131.CO_CUR == coSer)
                                  select new temposSeries
                                  {
                                      nomeTempo = tb131.NR_TEMPO,
                                      inicioTempo = tb131.HR_INICIO,
                                      terminoTempo = tb131.HR_TERMI,
                                      marcarTempo = ((codigo != null && codigo == tb131.NR_TEMPO) ? MarcaTempo : false)
                                  }
                                  );

                    if (tempos.Count() > 0)
                    {
                        ddl.DataTextField = "concat";
                        ddl.DataValueField = "nomeTempo";
                        ddl.DataSource = tempos;
                        ddl.DataBind();
                    }
                }
            }

            ddl.Items.Insert(0, new ListItem("Sem Registro de Tempo", "N"));
        }

        /// <summary>
        /// Método responsável por carregar a grid
        /// </summary>
        private void CarregaDisciplinas()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string turmaUnica = "N";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }
            grdDisciplinas.DataSource = null;
            grdDisciplinas.DataBind();

            #region Verificação e criação da matéria de turma única
            //---------> Verifica se a turma será única ou não
            if (turmaUnica == "S")
            {
                //-------------> Verifica se existe uma matéria com sigla "MSR", que é a matéria padrão para turma única
                if (!TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").Any())
                {
                    //-----------------> Cria uma matéria para ser a padrão de turma única, MSR.
                    TB107_CADMATERIAS cm = new TB107_CADMATERIAS();

                    cm.CO_EMP = LoginAuxili.CO_EMP;
                    cm.NO_SIGLA_MATERIA = "MSR";
                    cm.NO_MATERIA = "Atividades Letivas";
                    cm.NO_RED_MATERIA = "Atividades";
                    cm.DE_MATERIA = "Matéria utilizada no lançamento de atividades para professores de turma única.";
                    cm.CO_STATUS = "A";
                    cm.DT_STATUS = DateTime.Now;
                    cm.CO_CLASS_BOLETIM = 4;
                    TB107_CADMATERIAS.SaveOrUpdate(cm);

                    CurrentPadraoCadastros.CurrentEntity = cm;

                    //-----------------> Vincula a matéria MSR ao curso selecionado
                    int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cma => cma.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                    TB02_MATERIA m = new TB02_MATERIA();

                    m.CO_EMP = LoginAuxili.CO_EMP;
                    m.CO_MODU_CUR = modalidade;
                    m.CO_CUR = serie;
                    m.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                    m.ID_MATERIA = idMat;
                    m.QT_CRED_MAT = null;
                    m.QT_CARG_HORA_MAT = 800;
                    m.DT_INCL_MAT = DateTime.Now;
                    m.DT_SITU_MAT = DateTime.Now;
                    m.CO_SITU_MAT = "I";
                    TB02_MATERIA.SaveOrUpdate(m);

                    CurrentPadraoCadastros.CurrentEntity = m;
                }
                else
                {
                    //-----------------> Verifica se a matéria MSR está vinculada ao curso
                    int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                    if (!TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_EMP == LoginAuxili.CO_EMP && m.CO_MODU_CUR == modalidade && m.CO_CUR == serie && m.ID_MATERIA == idMat).Any())
                    {
                        //---------------------> Vincula a matéria MSR ao curso selecionado.
                        TB02_MATERIA m = new TB02_MATERIA();

                        m.CO_EMP = LoginAuxili.CO_EMP;
                        m.CO_MODU_CUR = modalidade;
                        m.CO_CUR = serie;
                        m.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                        m.ID_MATERIA = idMat;
                        m.QT_CRED_MAT = null;
                        m.QT_CARG_HORA_MAT = 800;
                        m.DT_INCL_MAT = DateTime.Now;
                        m.DT_SITU_MAT = DateTime.Now;
                        m.CO_SITU_MAT = "I";
                        TB02_MATERIA.SaveOrUpdate(m);

                        CurrentPadraoCadastros.CurrentEntity = m;
                    }
                }
            }
            #endregion


            // Verifica se a turma selecionada pelo usuário é turma única
            if (turmaUnica == "S")
            {
                // No caso de ser turma única o sistema deve retornar somente a matéria com sigla MSR, que é a matéria
                // padrão para turmas únicas, que não precisam de controle por matéria.
                var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where tb02.CO_MODU_CUR == modalidade
                           && tb02.CO_CUR == serie
                           && tb107.NO_SIGLA_MATERIA == "MSR"
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                grdDisciplinas.DataSource = res;
                grdDisciplinas.DataBind();
            }
            else
            {
                var res = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where tbres.CO_MODU_CUR == modalidade
                           && tbres.CO_CUR == serie
                           && tbres.CO_COL_RESP == professor
                           && tbres.CO_TUR == turma
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                grdDisciplinas.DataSource = res;
                grdDisciplinas.DataBind();
            }

            foreach (GridViewRow li in grdDisciplinas.Rows)
            {
                ListBox list = ((ListBox)li.Cells[2].FindControl("lstTempo"));
                CarregaTempo(list);
            }
        }
        #endregion

        #region Combo
        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaProfessor();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            verificaTurmaUnica();
        }

        protected void ddlTipoAtiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                int tipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;

                if (tipoAtiv != 0)
                {
                    string flLancaNota = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where tb273.CO_SITUA_ATIV == "A" && tb273.ID_TIPO_ATIV == tipoAtiv select new { tb273.FL_LANCA_NOTA_ATIV }).FirstOrDefault().FL_LANCA_NOTA_ATIV;
                    ddlAvaliaAtiv.SelectedValue = flLancaNota;
                }
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregarMeses();
                CarregaDisciplinas();
            }
        }

        protected void ddlProfessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0" && ((DropDownList)sender).SelectedValue != "")
            {
                ddlProfessorReal.SelectedValue = ((DropDownList)sender).SelectedValue;
                CarregaModalidades();
            }
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0" && ((DropDownList)sender).SelectedValue != "")
            {
                CarregaProfessorHomolog();
                CarregaTipoAtividade();
                CarregaDisciplinas();
            }
        }

        protected void btnRegFreq_Click(object sender, EventArgs e)
        {

            // Salva
            if (AcaoBarraCadastro())
            {
                TB119_ATIV_PROF_TURMA tb119 = (TB119_ATIV_PROF_TURMA)Session["TB119_ATIV_PROF_TURMA"];
                var parametros = ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";" + txtDataRe.Text + ";" + ddlProfessor.SelectedValue + ";" + ddlAno.SelectedValue + ";" + ddlReferencia.SelectedValue + ";" + tb119.CO_ATIV_PROF_TUR + ";" + tb119.CO_TEMPO_ATIV;
                HttpContext.Current.Session["ParametrosBusca"] = parametros;

                // Redireciona para sem planejamento
                Response.Redirect("~/GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3409_LancManuFreqSemAtivPorProfessor/Cadastro.aspx?moduloId=1171&moduloNome=Frequ%C3%AAncia%20de%20Alunos%20-%20Lan%C3%A7amento%20por%20Professor&moduloId=1171");
            }
        }

        protected void ckSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdDisciplinas.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdDisciplinas.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                    }
                    else
                    {
                        ListBox tempo = (((ListBox)linha.Cells[2].FindControl("lstTempo")));
                        TextBox tema = (((TextBox)linha.Cells[3].FindControl("txtTemaAula")));
                        TextBox desc = (((TextBox)linha.Cells[4].FindControl("txtDescAtividade")));

                        if (chk.Checked)
                        {
                            tempo.Enabled = tema.Enabled = desc.Enabled = true;
                        }
                        else
                        {
                            tempo.Enabled = tema.Enabled = desc.Enabled = false;
                        }
                    }
                }
            }
        }

        #endregion

        #region Classe
        private class temposSeries
        {
            public int nomeTempo { get; set; }
            public string inicioTempo { get; set; }
            public string terminoTempo { get; set; }
            public Boolean marcarTempo { get; set; }
            public string duracaoTempo
            {
                get
                {
                    if (this.inicioTempo != "" && this.terminoTempo != "")
                    {
                        return calculaTempo(this.inicioTempo, this.terminoTempo);
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            public string concat
            {
                get
                {
                    return this.nomeTempo + " - " + this.inicioTempo + " - " + this.terminoTempo;
                }
            }
        }
        #endregion

        #region Metodos personalizados

        /// <summary>
        /// Realiza o cálculo de tempo
        /// </summary>
        /// <param name="inicio">Hora de inicio</param>
        /// <param name="termino">Hora de termino</param>
        /// <returns></returns>
        private static string calculaTempo(string inicio = "", string termino = "")
        {
            DateTime dtInicio, dtTermino;
            DateTime.TryParse(inicio, out dtInicio);
            DateTime.TryParse(termino, out dtTermino);
            TimeSpan dtResultado;

            if (dtInicio != null && dtTermino != null && dtInicio != DateTime.MinValue && dtTermino != DateTime.MinValue)
            {
                if (dtTermino > dtInicio)
                {
                    dtResultado = dtTermino - dtInicio;
                    return DateTime.Parse(dtResultado.ToString()).ToString("t");
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
