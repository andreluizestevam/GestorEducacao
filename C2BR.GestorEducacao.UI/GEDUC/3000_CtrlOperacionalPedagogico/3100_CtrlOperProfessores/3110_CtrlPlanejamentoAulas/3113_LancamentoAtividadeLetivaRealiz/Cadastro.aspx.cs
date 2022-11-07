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
//03/07/2014 | Maxwell Almeida            |
//
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3113_LancamentoAtividadeLetivaRealiz
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
                txtDataCad.Text = DateTime.Now.ToString("dd/MM/yyyy");
                chkAtivNaoPlanej.Checked = ddlDisciplina.Enabled = true;

                TB03_COLABOR colL = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

                //Carregamentos valores padrões nos campos
                //ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
                //ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                //ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlMes.Items.Insert(0, new ListItem("Selecione", "0"));
                //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTipoAtiv.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTempos.Items.Insert(0, new ListItem("Selecione", "0"));
                CarregarMeses();
                CarregaMedidas();

                if (colL.FLA_PROFESSOR == "S" && LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M")
                {
                    CarregaProfessor();
                    ddlProfessor.SelectedValue = colL.CO_COL.ToString();
                    ddlProfessor.Enabled = false;

                    grdAlunos.DataBind();
                    AtualizaCamposPlanoAula();
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

        bool AcaoBarraCadastro()
        {
            DateTime dataRealizacao = Convert.ToDateTime(txtDataRe.Text);
            int quantidadeTempos = 0;
            Dictionary<int, temposSeries> temposEscolhidos = new Dictionary<int, temposSeries>();
            int coProfReal = int.Parse(ddlProfessorReal.SelectedValue);

            if (!string.IsNullOrEmpty(hidItemJaSalvo.Value))
                return true;

            if (ddlMes.SelectedValue == "0" || ddlMes.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "O Mês não foi selecionado.");
                return false;
            }
            if (grdAlunos.Rows.Count > 0 && !chkAtivNaoPlanej.Checked)
            {
                int marcados = 0;
                foreach (GridViewRow linha in grdAlunos.Rows)
                {
                    CheckBox box = (CheckBox)linha.Cells[0].FindControl("ckSelect");
                    if (box.Checked)
                        marcados++;
                }
                if (marcados == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Nenhuma atividade selecionada.");
                    return false;
                }
                else if (marcados > 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Por favor selecione apenas uma atividade.");
                    return false;
                }
            }
            else if (!chkAtivNaoPlanej.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma atividade carregada, por favor selecione as opões de filtro corretamente.");
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

            if ((hidTpAulaRequired.Value == "S") && (ddlTempos.SelectedValue == "0"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Tipo do Tempo de Aula é Requerido");
                return false;
            }

            if (ddlTempos.SelectedValue == "S" && grdTempos.Enabled && grdTempos.Rows.Count > 0)
            {
                temposEscolhidos = BuscarMarcadosTempo();
                if (temposEscolhidos.Count == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Marque um tempo para referênciar.");
                    return false;
                }
                else
                {
                    quantidadeTempos = temposEscolhidos.Count;
                }
            }
            else if ((ddlTempos.SelectedValue == "N") || (ddlTempos.SelectedValue == "0"))
            {
                if (txtInicio.Text != "" && txtTermino.Text != "")
                {
                    DateTime dtInicio, dtTermino;

                    // Valida se o horário de início da atividade é válida
                    if (!DateTime.TryParse(txtInicio.Text, out dtInicio))
                    {
                        txtInicio.Focus();
                        AuxiliPagina.EnvioMensagemErro(this, "O horário de início inválido.");
                        return false;
                    }

                    // Valida se o horário de término da atividade é válida
                    if (!DateTime.TryParse(txtTermino.Text, out dtTermino))
                    {
                        txtTermino.Focus();
                        AuxiliPagina.EnvioMensagemErro(this, "O horário de término inválido.");
                        return false;
                    }

                    // Valida se o horário de término da atividade é maior que o horário de início se os valores dos campos de horário de
                    // início e horário de término for diferente de "00:00", se o usuário informar algum valor
                    if (txtInicio.Text != "00:00" || txtTermino.Text != "00:00")
                    {
                        if (dtTermino <= dtInicio)
                        {
                            txtInicio.Focus();
                            AuxiliPagina.EnvioMensagemErro(this, "O horário final deve ser maior que o horário inicial.");
                            return false;
                        }
                    }

                    /*if (!((DateTime.TryParse(txtInicio.Text, out dtInicio) && DateTime.TryParse(txtTermino.Text, out dtTermino))
                       && (dtInicio > DateTime.MinValue && dtTermino > DateTime.MinValue) && (dtTermino > dtInicio)))
                    {
                        txtInicio.Focus();
                        AuxiliPagina.EnvioMensagemErro(this, "O horário final deve ser maior que o horário inicial.");
                        return false;
                    }*/
                    quantidadeTempos = 1;
                }
                else
                {
                    // Coloca o valor default dos horários de início e término da atividade.
                    txtInicio.Text = "00:00";
                    txtTermino.Text = "00:00";

                    /*txtInicio.Focus();
                    AuxiliPagina.EnvioMensagemErro(this, "Por favor informe o horário de inicio e fim da atividade.");
                    return false;*/
                }

            }
            else
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "Selecione o tipo de registro de tempo.");
            //    return false;
            //}

            if (hidTpAulaRequired.Value == "S")
            {
                if (quantidadeTempos <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Período horário inválido.");
                    return false;
                }
            }

            for (int numero = 0; numero < quantidadeTempos; numero++)
            {
                TB119_ATIV_PROF_TURMA tb119;
                string horarioInicio, horarioTermino;
                horarioInicio = horarioTermino = "";
                int numeroTempo = 0;
                if (ddlTempos.SelectedValue == "S")
                {
                    horarioInicio = temposEscolhidos.ElementAt(numero).Value.inicioTempo;
                    horarioTermino = temposEscolhidos.ElementAt(numero).Value.terminoTempo;
                    numeroTempo = temposEscolhidos.ElementAt(numero).Value.nomeTempo;
                }
                else if (ddlTempos.SelectedValue == "N")
                {
                    horarioInicio = txtInicio.Text;
                    horarioTermino = txtTermino.Text;
                    numeroTempo = 0;
                }

                //Opção referência inutilizada por falta de necessidade
                string strCoRef = "SR";//ddlRefer.SelectedValue;
                if (hdCodPlanAula.Value != "0" && hdCodPlanAula.Value != "")
                {
                    int coPlaAula = int.Parse(hdCodPlanAula.Value);

                    TB17_PLANO_AULA tb17 = TB17_PLANO_AULA.RetornaPelaChavePrimaria(coPlaAula);

                    int modalidade = (int)tb17.CO_MODU_CUR;
                    int serie = tb17.CO_CUR;
                    int turma = tb17.CO_TUR;
                    int materia = tb17.CO_MAT;
                    int coColProf = int.Parse(ddlProfessor.SelectedValue);
                    int coCol = int.Parse(ddlCocol.SelectedValue);
                    string coAnoMesMat = ddlAno.SelectedValue;

                    int ocorAtividade = (from iTb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                         where iTb119.CO_PLA_AULA == coPlaAula
                                         where iTb119.CO_TEMPO_ATIV == numeroTempo
                                         select iTb119).Count();

                    if (ocorAtividade > 0)
                    {
                        tb119 = (from lTb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                 where lTb119.CO_PLA_AULA == coPlaAula
                                 select lTb119).FirstOrDefault();
                    }
                    else
                    {
                        tb119 = new TB119_ATIV_PROF_TURMA();
                        tb119.CO_EMP = LoginAuxili.CO_EMP;
                        tb119.CO_MODU_CUR = modalidade;
                        tb119.CO_CUR = serie;
                        tb119.CO_TUR = turma;
                        tb119.CO_MAT = materia;
                        tb119.CO_COL = coCol;
                        tb119.CO_COL_ATIV = coColProf;
                        tb119.CO_PLA_AULA = coPlaAula;
                    }

                    TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(int.Parse(ddlTipoAtiv.SelectedValue));

                    tb119.CO_ANO_MES_MAT = coAnoMesMat;
                    tb119.DT_ATIV_REAL = DateTime.Parse(txtDataRe.Text);
                    //Novos registros
                    tb119.HR_INI_ATIV = horarioInicio;
                    tb119.HR_TER_ATIV = horarioTermino;
                    //tb119.CO_TEMPO_ATIV = numeroTempo;
                    tb119.CO_ATIV_PROF_REAL = coProfReal;
                    tb119.DE_RES_ATIV = txtResumo.Text;
                    tb119.DT_REGISTRO_ATIV = DateTime.Now;
                    tb119.FLA_AULA_PLAN = true;
                    tb119.CO_IP_CADAST = LoginAuxili.IP_USU;
                    tb119.DT_ATIV_REAL_TERM = DateTime.Parse(txtDataRe.Text);
                    tb119.CO_TIPO_ATIV = tb273.CO_SIGLA_ATIV;
                    tb119.TB273_TIPO_ATIVIDADE = tb273;
                    tb119.DE_FORMA_AVALI_ATIV = txtFormaAvaliAtiv.Text != "" ? txtFormaAvaliAtiv.Text : null;
                    tb119.FL_AVALIA_ATIV = ddlAvaliaAtiv.SelectedValue;
                    tb119.DE_TEMA_AULA = txtPlanoAula.Text;
                    tb119.FL_LANCA_NOTA = "S";
                    tb119.CO_REFER_ATIV = strCoRef;
                    tb119.FL_HOMOL_DIARIO = tb17.FLA_HOMOLOG;
                    TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, false);

                    tb17.FLA_EXECUTADA_ATIV = true;
                    tb17.FL_ALTER_PLA = true;
                    tb17.DT_REAL_PLA = DateTime.Parse(txtDataRe.Text);

                    TB17_PLANO_AULA.SaveOrUpdate(tb17, false);

                    GestorEntities.CurrentContext.SaveChanges();

                    Session["TB17_PLANO_AULA"] = tb17;
                }
                else
                {
                    string coAnoRefPla = ddlAno.SelectedValue;
                    int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                    int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                    int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                    int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
                    int coColProf = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
                    int coCol = ddlCocol.SelectedValue != "" ? int.Parse(ddlCocol.SelectedValue) : 0;

                    if (modalidade == 0 || serie == 0 || turma == 0 || materia == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Para cadastrar um registro que não está no Plano de Aula, os campos Modalidade, Série/Curso, Turma e Disciplina são requeridos");
                        return false;
                    }

                    tb119 = new TB119_ATIV_PROF_TURMA();

                    TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(int.Parse(ddlTipoAtiv.SelectedValue));

                    tb119.CO_ANO_MES_MAT = coAnoRefPla;
                    tb119.CO_EMP = LoginAuxili.CO_EMP;
                    tb119.CO_MODU_CUR = modalidade;
                    tb119.CO_CUR = serie;
                    tb119.CO_TUR = turma;
                    tb119.CO_MAT = materia;
                    tb119.CO_COL = coCol;
                    tb119.CO_COL_ATIV = coColProf;
                    tb119.CO_PLA_AULA = null;
                    tb119.DT_ATIV_REAL = DateTime.Parse(txtDataRe.Text);
                    tb119.HR_INI_ATIV = horarioInicio;
                    tb119.HR_TER_ATIV = horarioTermino;
                    tb119.CO_TEMPO_ATIV = numeroTempo;
                    tb119.CO_ATIV_PROF_REAL = coProfReal;
                    tb119.DE_RES_ATIV = txtResumo.Text;
                    tb119.DT_REGISTRO_ATIV = DateTime.Now;
                    tb119.FLA_AULA_PLAN = false;
                    tb119.CO_IP_CADAST = LoginAuxili.IP_USU;
                    tb119.DT_ATIV_REAL_TERM = DateTime.Parse(txtDataRe.Text);
                    tb119.CO_TIPO_ATIV = tb273.CO_SIGLA_ATIV;
                    tb119.DE_FORMA_AVALI_ATIV = txtFormaAvaliAtiv.Text != "" ? txtFormaAvaliAtiv.Text : null;
                    tb119.FL_AVALIA_ATIV = ddlAvaliaAtiv.SelectedValue;
                    tb119.DE_TEMA_AULA = txtPlanoAula.Text;
                    tb119.CO_REFER_ATIV = strCoRef;
                    tb119.TB273_TIPO_ATIVIDADE = tb273;

                    tb119.FL_LANCA_NOTA = "S";

                    TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119);
                }
                Session["TB119_ATIV_PROF_TURMA"] = tb119;
                HttpContext.Current.Session.Add("auxQtTempos", quantidadeTempos);
                hidItemJaSalvo.Value = "1";
            }
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
        /// Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        /// </summary>
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tipo, false, true);
        }

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
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string turmaUnica = "N";
            hidItemJaSalvo.Value = "";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }
            ddlDisciplina.Items.Clear();

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

                if (res.Count() > 0)
                {
                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataSource = res;
                    ddlDisciplina.DataBind();

                    if (res.Count() > 1)
                        ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                }
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

                if (res.Count() > 0)
                {
                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataSource = res;
                    ddlDisciplina.DataBind();

                    if(res.Count() > 1)
                        ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                }
            }
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

            if (LoginAuxili.FLA_PROFESSOR == "S" && LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M")
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
            else
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
                    CarregaTipoTempos();
                    ddlTempos.SelectedValue = "N";
                    ddlTempos.Enabled = false;
                    ddlTurno.SelectedValue = "0";
                    ddlTurno.Enabled = false;
                    txtInicio.Enabled = false;
                    txtTermino.Enabled = false;
                    txtDuracao.Enabled = false;
                    hidTpAulaRequired.Value = "N";
                }
                else
                {
                    CarregaTipoTempos();
                    ddlTempos.SelectedValue = "0";
                    ddlTempos.Enabled = true;
                    ddlTurno.SelectedValue = "0";
                    ddlTurno.Enabled = true;
                    txtInicio.Enabled = true;
                    txtTermino.Enabled = true;
                    txtDuracao.Enabled = true;
                    hidTpAulaRequired.Value = "S";
                }
            }
        }

        /// <summary>
        /// Método que carrega a grid de Alunos
        /// </summary>
        private void CarregaGrid()
        {
            int coAnoRefPla = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coCol = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int mesRefer = ddlMes.SelectedValue != "-1" ? int.Parse(ddlMes.SelectedValue) : 0;

            if (coCol == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Professor é Requerido");
                return;
            }

            var resultado = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb17.CO_CUR equals tb01.CO_CUR
                             join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb17.CO_TUR equals tb06.CO_TUR
                             where tb17.CO_EMP == LoginAuxili.CO_EMP
                             && tb17.CO_ANO_REF_PLA == coAnoRefPla
                             && (modalidade != 0 ? tb17.CO_MODU_CUR == modalidade : modalidade == 0)
                             && (serie != 0 ? tb17.CO_CUR == serie : serie == 0) && (turma != 0 ? tb17.CO_TUR == turma : turma == 0)
                             && (materia != 0 ? tb17.CO_MAT == materia : materia == 0) && tb17.TB03_COLABOR.CO_COL == coCol
                             && (mesRefer != 0 ? tb17.DT_PREV_PLA.Month == mesRefer : mesRefer == 0)
                             && tb01.CO_EMP == LoginAuxili.CO_EMP && tb06.CO_EMP == LoginAuxili.CO_EMP && tb06.CO_CUR == tb17.CO_CUR && tb17.CO_SITU_PLA == "A"
                             select new
                             {
                                 tb17.HR_FIM_AULA_PLA,
                                 tb17.HR_INI_AULA_PLA,
                                 tb17.DT_PREV_PLA,
                                 tb17.TB03_COLABOR.NO_COL,
                                 tb17.CO_PLA_AULA,
                                 tb17.NU_TEMP_PLA,
                                 FLA_HOMOLOG = ((tb17.FLA_HOMOLOG != "S") ? false : true),
                                 tb17.DE_TEMA_AULA,
                                 tb01.CO_SIGL_CUR,
                                 tb06.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                 SITUACAO = tb17.FLA_HOMOLOG == "N" ? "Não Homologada" : tb17.FLA_EXECUTADA_ATIV == true ? "Realizada" : "Não Realizada",
                                 FL_PLANEJ_ATIV = tb17.FL_PLANEJ_ATIV == "S" ? "Sim" : "Não",
                                 FL_AVALIA_ATIV = tb17.FL_AVALIA_ATIV == "S" ? "Sim" : "Não",
                                 DES_TIPO_ATIV = (tb17.CO_TIPO_ATIV == "ANO" ? "Aula Normal" : (tb17.CO_TIPO_ATIV == "AEX" ? "Aula Extra" :
                                 (tb17.CO_TIPO_ATIV == "ARE" ? "Aula de Reforço" : (tb17.CO_TIPO_ATIV == "ARC" ? "Aula de Recuperação" :
                                 (tb17.CO_TIPO_ATIV == "TES" ? "Teste" : (tb17.CO_TIPO_ATIV == "PRO" ? "Prova" : (tb17.CO_TIPO_ATIV == "TRA" ? "Trabalho" :
                                 (tb17.CO_TIPO_ATIV == "AGR" ? "Atividade em Grupo" : (tb17.CO_TIPO_ATIV == "ATE" ? "Atividade Externa" :
                                 (tb17.CO_TIPO_ATIV == "ATI" ? "Atividade Interna" : "Outros"))))))))))
                             }).OrderBy(p => p.NO_COL).ThenBy(p => p.DT_PREV_PLA);

            divGrid.Visible = true;

            grdAlunos.DataKeyNames = new string[] { "CO_PLA_AULA" };

            grdAlunos.DataSource = resultado;
            grdAlunos.DataBind();
        }
        /// <summary>
        /// Solicita o carregamento dos valores da atividades selecionada
        /// </summary>
        /// <param name="linha"></param>
        private void CarregaValoresAtividade(GridViewRow linha)
        {
            hdCodPlanAula.Value = ((HiddenField)linha.Cells[2].FindControl("hdPlaAula")).Value;
            hdNumeroTempo.Value = ((HiddenField)linha.Cells[2].FindControl("hdNumeroTempo")).Value;
            hdInicioTempo.Value = ((HiddenField)linha.Cells[2].FindControl("hdInicioTempo")).Value;
            hdTerminoTempo.Value = ((HiddenField)linha.Cells[2].FindControl("hdTerminoTempo")).Value;

            int coPlaAula = Convert.ToInt32(hdCodPlanAula.Value);
            int coNumeroTempo = Convert.ToInt32(hdNumeroTempo.Value);
            TB119_ATIV_PROF_TURMA Tb119 = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                           where tb119.CO_ATIV_PROF_TUR == coPlaAula
                                           && tb119.CO_TEMPO_ATIV == coNumeroTempo
                                           select tb119
                                 ).FirstOrDefault();

            if (Tb119 != null)
                CarregaControlesPlano(Tb119, coPlaAula, coNumeroTempo, "R");
            else
                CarregaControlesPlano(new TB119_ATIV_PROF_TURMA(), coPlaAula, coNumeroTempo, "P");
        }
        /// <summary>
        /// Método que carrega as informações do Plano de Aula
        /// </summary>
        /// <param name="coPlaAula">Id do plano de aula</param>
        /// <param name="strTipo">Tipo "R"ealizada, "P"lanejada, "N"enhuma</param>
        private void CarregaControlesPlano(TB119_ATIV_PROF_TURMA tb119, int coPlaAula, int coNumeroTempo, string strTipo)
        {
            var tb17 = TB17_PLANO_AULA.RetornaPelaChavePrimaria(coPlaAula);

            if (strTipo == "P")
            {
                tb17.TB273_TIPO_ATIVIDADEReference.Load();
                txtDataRe.Text = tb17.DT_PREV_PLA.ToString("dd/MM/yyyy");
                //
                CarrregarTempoGravado(coNumeroTempo, tb17.HR_INI_AULA_PLA, tb17.HR_FIM_AULA_PLA);
                //txtInicio.Enabled = txtTermino.Enabled = false;
                txtPlanoAula.Text = tb17.DE_TEMA_AULA;
                ddlDisciplina.SelectedValue = tb17.CO_MAT.ToString();
                ddlAvaliaAtiv.SelectedValue = tb17.FL_AVALIA_ATIV;
                ddlTipoAtiv.SelectedValue = tb17.TB273_TIPO_ATIVIDADE != null ? tb17.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV.ToString() : "";
                txtResumo.Text = "";
                txtPlanoAula.Enabled = true;
                chkAtivNaoPlanej.Checked = ddlDisciplina.Enabled = ddlTipoAtiv.Enabled = txtPlanoAula.Enabled = ddlAvaliaAtiv.Enabled = false;
            }
            else
            {
                if (tb119 != null)
                {
                    tb119.TB273_TIPO_ATIVIDADEReference.Load();

                    txtDataRe.Text = tb119.DT_ATIV_REAL.ToString("dd/MM/yyyy");
                    //
                    CarrregarTempoGravado(tb119.CO_TEMPO_ATIV, tb119.HR_INI_ATIV, tb119.HR_TER_ATIV);
                    //txtInicio.Enabled = txtTermino.Enabled = false;
                    txtPlanoAula.Text = tb119.DE_TEMA_AULA != null ? tb119.DE_TEMA_AULA : "";
                    ddlAvaliaAtiv.SelectedValue = tb119.FL_AVALIA_ATIV;
                    txtResumo.Text = tb119.DE_RES_ATIV;
                    txtFormaAvaliAtiv.Text = tb119.DE_FORMA_AVALI_ATIV != null ? tb119.DE_FORMA_AVALI_ATIV : "";
                    txtPlanoAula.Text = tb17 != null ? tb17.DE_TEMA_AULA : "";
                    ddlDisciplina.SelectedValue = tb17.CO_MAT.ToString();
                    ddlTipoAtiv.SelectedValue = tb119.TB273_TIPO_ATIVIDADE != null ? tb119.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV.ToString() : "";
                    txtPlanoAula.Enabled = false;
                    chkAtivNaoPlanej.Checked = ddlDisciplina.Enabled = ddlTipoAtiv.Enabled = txtPlanoAula.Enabled = ddlAvaliaAtiv.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Carrega o tempo marcado na grid ou nos campos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        private void CarrregarTempoGravado(int codigo, string inicio, string termino)
        {
            if (codigo == 0)
            {
                ddlTempos.SelectedValue = "N";
                ControleCamposTempos(true);
                txtInicio.Text = inicio;
                txtTermino.Text = termino;
                txtDuracao.Text = calculaTempo(inicio, termino);
            }
            else
            {
                ddlTempos.SelectedValue = "S";
                CarregaGridTempos(codigo, inicio, termino);
            }
        }

        /// <summary>
        /// Carregas os tipoes de tempo
        /// </summary>
        private void CarregaTipoTempos()
        {
            ddlTempos.Items.Clear();
            ddlTempos.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTempos.SelectedValue = "0";
            ddlTempos.Items.Insert(0, new ListItem("Com Registro de Tempo", "S"));
            ddlTempos.Items.Insert(0, new ListItem("Sem Registro de Tempo", "N"));
        }

        /// <summary>
        /// Carrega a grid de tempos por turma
        /// </summary>
        private void CarregaGridTempos(int? codigo = null, string inicio = null, string termino = null)
        {
            ControleCamposTempos(false);
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coSer = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coTurno = ddlTurno.SelectedValue;

            if (coMod != 0 && coSer != 0 && coTur != 0)
            {
                var turma = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coMod, coSer, coTur);
                if (turma != null && turma.CO_PERI_TUR != null)
                {
                    Boolean MarcaTempo = false;
                    string hrInicio = null;
                    string hrTermino = null;
                    if (codigo != null)
                    {
                        MarcaTempo = true;
                        hrInicio = inicio ?? DateTime.MinValue.ToString("t");
                        hrTermino = termino ?? DateTime.MinValue.ToString("t");
                    }
                    var tempos = (from tb131 in TB131_TEMPO_AULA.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                  where (coMod == -1 ? 0 == 0 : tb131.CO_MODU_CUR == coMod)
                                  && (coSer == -1 ? 0 == 0 : tb131.CO_CUR == coSer)
                                  && ((coTurno != "0") ? tb131.TP_TURNO == coTurno : true)
                                  select new temposSeries
                                  {
                                      nomeTempo = tb131.NR_TEMPO,
                                      inicioTempo = ((codigo != null && codigo == tb131.NR_TEMPO && hrInicio != null) ? hrInicio : (tb131.HR_INICIO == null ? "00:00" : tb131.HR_INICIO)),
                                      terminoTempo = ((codigo != null && codigo == tb131.NR_TEMPO && hrTermino != null) ? hrTermino : (tb131.HR_TERMI == null ? "00:00" : tb131.HR_TERMI)),
                                      marcarTempo = ((codigo != null && codigo == tb131.NR_TEMPO) ? MarcaTempo : false)
                                  }
                                  );

                    if (tempos.Count() > 0)
                    {
                        grdAlunos.DataKeyNames = new string[] { "nomeTempo" };
                        grdTempos.DataSource = tempos;
                        grdTempos.DataBind();
                    }
                }
            }
        }

        /// <summary>
        /// Método que atualiza campos do Plano de Aula
        /// </summary>
        private void AtualizaCamposPlanoAula()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int idmatMsr = 0;
            int matMsr = 0;

            string turmaUnica = "N";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }

            if (turmaUnica == "S")
            {
                idmatMsr = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                matMsr = TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_MODU_CUR == modalidade && m.CO_CUR == serie && m.ID_MATERIA == idmatMsr).FirstOrDefault().CO_MAT;
                ddlDisciplina.SelectedValue = matMsr.ToString();
                ddlDisciplina.Enabled = false;
            }
            else
            {
                //ddlDisciplina.SelectedValue = "0";
                ddlDisciplina.Enabled = true;
            }

            hdCodPlanAula.Value = txtDataRe.Text = txtPlanoAula.Text = txtResumo.Text =
                txtFormaAvaliAtiv.Text = "";
            txtPlanoAula.Enabled = true;
            //ddlDisciplina.SelectedValue = "0";
            txtInicio.Text = txtTermino.Text = txtDuracao.Text = "00:00";
            chkAtivNaoPlanej.Checked = ddlTipoAtiv.Enabled = true;

            ddlTipoAtiv.SelectedValue = "0";
            ddlTempos.SelectedValue = "0";
            grdTempos.DataSource = null;
            grdTempos.DataBind();
        }
        #endregion

        #region Combo
        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            hidItemJaSalvo.Value = "";
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaProfessor();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidItemJaSalvo.Value = "";
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidItemJaSalvo.Value = "";
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
            hidItemJaSalvo.Value = "";
            if (((DropDownList)sender).SelectedValue != "0") {
                CarregarMeses();
                CarregaMaterias();
            }
        }

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSituacao = (Label)e.Row.FindControl("lblSituacao");
                CheckBox cbSelecionado = (CheckBox)e.Row.FindControl("ckSelect");

                if (cbSelecionado.Enabled == false)
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(242, 220, 219);

                if (lblSituacao.Text == "Realizada")
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
            }
        }

        protected void ddlProfessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidItemJaSalvo.Value = "";
            if (((DropDownList)sender).SelectedValue != "0" && ((DropDownList)sender).SelectedValue != "")
            {
                ddlProfessorReal.SelectedValue = ((DropDownList)sender).SelectedValue;
                grdAlunos.DataBind();
                AtualizaCamposPlanoAula();
                CarregaModalidades();
            }
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0" && ((DropDownList)sender).SelectedValue != "")
            {
                CarregaMaterias();
                CarregaProfessorHomolog();
                CarregaTipoAtividade();
                CarregaTipoTempos();
                CarregaGrid();
                AtualizaCamposPlanoAula();
            }
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            HiddenField hfTemporario = new HiddenField();
            int qtdChecada = 0;
            foreach (GridViewRow linha in grdAlunos.Rows)
            {
                CheckBox cbSelecionado = (CheckBox)linha.Cells[0].FindControl("ckSelect");
                if ((cbSelecionado).Checked)
                {
                    qtdChecada++;
                }
            }

            foreach (GridViewRow linha in grdAlunos.Rows)
            {
                CheckBox cbSelecionado = (CheckBox)linha.Cells[0].FindControl("ckSelect");
                if ((cbSelecionado).Checked)
                {
                    if (qtdChecada == 1)
                    {
                        CarregaValoresAtividade(linha);
                    }
                }
            }

            if (qtdChecada > 1)
            {
                hdCodPlanAula.Value = hfTemporario.Value;
            }

            if (qtdChecada == 0)
                AtualizaCamposPlanoAula();
        }

        protected void ddlTempos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "" && ((DropDownList)sender).SelectedValue != "0")
            {
                if (((DropDownList)sender).SelectedValue == "S")
                    CarregaGridTempos();
                else
                    ControleCamposTempos(true);
            }
            else
            {
                ControleCamposTempos();
            }
        }

        protected void txtInicio_TextChanged(object sender, EventArgs e)
        {
            recalcularGrid();
        }

        protected void txtTermino_TextChanged(object sender, EventArgs e)
        {
            recalcularGrid();
        }

        protected void txtInicio_TextChanged1(object sender, EventArgs e)
        {
            txtDuracao.Text = calculaTempo(((TextBox)sender).Text, txtTermino.Text);
        }

        protected void txtTermino_TextChanged1(object sender, EventArgs e)
        {
            txtDuracao.Text = calculaTempo(txtInicio.Text, ((TextBox)sender).Text);
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
        }
        #endregion

        #region Metodos personalizados
        /// <summary>
        /// Realiza o calculo de todas as linha da grid
        /// </summary>
        private void recalcularGrid()
        {
            foreach (GridViewRow linha in grdTempos.Rows)
            {
                TextBox inicio = (TextBox)linha.Cells[2].FindControl("txtInicio");
                TextBox termino = (TextBox)linha.Cells[3].FindControl("txtTermino");
                if (inicio != null && termino != null)
                {
                    string resultado = calculaTempo(inicio.Text, termino.Text);
                    linha.Cells[4].Text = resultado == "" ? linha.Cells[4].Text : resultado;
                }
            }
        }
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
        /// <summary>
        /// Habilita e desabilita todos os campos e/ou grid usados em controle de tempo
        /// </summary>
        /// <param name="tipo"></param>
        private void ControleCamposTempos(Boolean? tipo = null)
        {
            if (tipo == null)
            {
                txtInicio.Enabled = false;
                txtTermino.Enabled = false;
                txtDuracao.Enabled = false;
                grdTempos.Enabled = false;
                ddlTurno.Enabled = false;
            }
            else
            {
                txtInicio.Enabled = (Boolean)tipo;
                txtTermino.Enabled = (Boolean)tipo;
                txtDuracao.Enabled = (Boolean)tipo;
                grdTempos.Enabled = !(Boolean)tipo;
                ddlTurno.Enabled = !(Boolean)tipo;
            }
        }
        /// <summary>
        /// Busca todos os registros marcados na grid tempos
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, temposSeries> BuscarMarcadosTempo()
        {
            int voltas = 0;
            Dictionary<int, temposSeries> temposEscolhidos = new Dictionary<int, temposSeries>();
            foreach (GridViewRow linha in grdTempos.Rows)
            {
                CheckBox marcar = (CheckBox)linha.Cells[0].FindControl("cbMarcar");
                TextBox inicio = (TextBox)linha.Cells[2].FindControl("txtInicio");
                TextBox termino = (TextBox)linha.Cells[2].FindControl("txtTermino");

                if (marcar != null && inicio != null && termino != null && marcar.Checked)
                {
                    temposSeries classe = new temposSeries();
                    classe.marcarTempo = marcar.Checked;
                    classe.nomeTempo = int.Parse(linha.Cells[1].Text);
                    classe.inicioTempo = inicio.Text;
                    classe.terminoTempo = termino.Text;
                    //classe.duracaoTempo = linha.Cells[4].Text;
                    temposEscolhidos.Add(voltas, classe);
                }
                voltas++;
            }
            return temposEscolhidos;
        }

        #endregion

        protected void btnRegFreq_Click(object sender, EventArgs e)
        {
            // Salva
            if (AcaoBarraCadastro())
            {
                //Instancia um objeto da grade do curso para a Disciplina em questão
                int coMat = (ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0);
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           where tb43.CO_ANO_GRADE == ddlAno.SelectedValue
                           && tb43.CO_MAT == coMat
                           select tb43).FirstOrDefault();

                if (res != null)
                {
                    //Valida se é permitido lançar frequência para a disciplina selecionada
                    if (res.FL_LANCA_FREQU != "S")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "De acordo com a grade anual do curso em questão, não é permitido lançar frequência para a Disciplina selecionada");
                        return;
                    }
                }

                TB119_ATIV_PROF_TURMA tb119 = (TB119_ATIV_PROF_TURMA)Session["TB119_ATIV_PROF_TURMA"];
                var parametros = ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";" + txtDataRe.Text + ";" + ddlDisciplina.SelectedValue + ";" + ddlAno.SelectedValue + ";" + tb119.CO_ATIV_PROF_TUR + ";" + ddlProfessor.SelectedValue + ";" + ddlReferencia.SelectedValue;
                HttpContext.Current.Session["ParametrosBusca"] = parametros;
                if (chkAtivNaoPlanej.Checked)
                {
                    // Redireciona para sem planejamento
                    Response.Redirect("~/GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3404_LancManuFreqAluTurmaSemRegAtiv/Cadastro.aspx?moduloId=997&moduloNome=Frequ%C3%AAncia%20de%20Aluno%20-%20Lan%C3%A7amento%20por%20S%C3%A9rie/Turma%20(Sem%20Planejamento)&moduloId=997");

                }
                else
                {
                    // Redireciona para com planejamento
                    Response.Redirect("~/GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3402_LancManutFreqAlunoSerieTurma/Cadastro.aspx?moduloId=452&moduloNome=Frequ%C3%AAncia%20de%20Aluno%20-%20Lan%C3%A7amento%20por%20S%C3%A9rie/Turma%20(Com%20Planejamento)&moduloId=452");

                }
            }
        }

        protected void ddlTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridTempos();
        }
    }
}
