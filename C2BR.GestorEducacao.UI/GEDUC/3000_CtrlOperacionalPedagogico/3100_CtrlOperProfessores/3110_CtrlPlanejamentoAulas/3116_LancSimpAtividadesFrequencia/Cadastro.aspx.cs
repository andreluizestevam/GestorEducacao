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
//27/01/2015 | Maxwell Almeida            | Criação da página para lançamento de atividade e frequência simples
//-----------+----------------------------+-------------------------------------
//14/11/2021 |Fabricio S dos Santos       | Homologar as Frequências, colocando FL_HOMOL_FREQU = S (TB132_FREQ_ALU), quando Checkbox assinalado,
//           |                            | corrigir erro que ao abrir para edição se NU_TEMP_PLA fosse = 0 
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
using System.Data.Objects;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3116_LancSimpAtividadesFrequencia
{
    public partial class Cadastro : System.Web.UI.Page
    {

        int qtdLinhasDisciplinas = 0;
        int qtdLinhasAlunos = 0;

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
            CurrentPadraoCadastros.BarraCadastro.HabilitarBotoes("btnNewSearch", false);
            //Page.MaintainScrollPositionOnPostBack = true;
           
            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaMedidas();
                txtDataRe.Text = DateTime.Now.ToString();

                TB03_COLABOR colL = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault();

                if (colL.FLA_PROFESSOR == "S" && LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M")
                {
                    CarregaProfessor();
                    ddlProfessor.SelectedValue = colL.CO_COL.ToString();
                    ddlProfessor.Enabled = false;
                    CarregaModalidades();
                }
                else
                    CarregaProfessor();

                CarregaModalidades();
            }
            else
            {
                VerificaMostraDisciplinas(true);
                VerificaDisciplinaSelecioanda();
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
            int coProfReal = int.Parse(ddlProfessor.SelectedValue);
            bool selecDisci = false;
            bool selecTempo = false;

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

            if (!VerificaDatasPeriodoBimestre())
                return false;

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
            int coCol = LoginAuxili.CO_COL;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Para cadastrar um registro que não está no Plano de Aula, os campos Modalidade, Série/Curso, Turma e Disciplina são requeridos");
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

            int qtAtividadesLancadas = 0;
            int qtFrequenciasLancadas = 0;
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

                    if (string.IsNullOrEmpty(((DropDownList)li.Cells[2].FindControl("ddlTipoAtiv")).SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o tipo de atividade correspondente");
                        return false;
                    }

                    if (string.IsNullOrEmpty(((TextBox)li.Cells[4].FindControl("txtTemaAula")).Text))
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
                    string tipo = (((DropDownList)li.Cells[2].FindControl("ddlTipoAtiv")).SelectedValue);
                    string Tema = ((TextBox)li.Cells[4].FindControl("txtTemaAula")).Text;
                    string descricao = (((TextBox)li.Cells[4].FindControl("txtDescAtividade")).Text);
                    string coPla = (((HiddenField)li.Cells[1].FindControl("hidIdPlanejamento")).Value);
                    ListBox lisbox = ((ListBox)li.Cells[3].FindControl("lstTempo"));

                    //Verifica se é possível já lançar atividade/frequência homologadas
                    var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    res.TB83_PARAMETROReference.Load();
                    bool LancHomol = (res.TB83_PARAMETRO.FL_LANCA_FREQ_ALUNO_HOMOL == "S" ? true : false);

                    selecTempo = false;
                    //Salva os registros de acordo com os tempos selecionados no listbox
                    foreach (ListItem listb in lisbox.Items)
                    {
                        if (listb.Selected == true)
                        {
                            tb119 = new TB119_ATIV_PROF_TURMA();

                            TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(int.Parse(tipo));

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
                            tb119.FL_HOMOL_ATIV = (LancHomol ? "S" : "N");
                            tb119.FL_HOMOL_DIARIO = (LancHomol ? "S" : "N");
                            tb119.CO_PLA_AULA = (!string.IsNullOrEmpty(coPla) ? int.Parse(coPla) : (int?)null);

                            //Trata o tempo
                            int tempo = listb.Value != "N" ? int.Parse(listb.Value) : 0;
                            string deTempo = listb.Text;
                            tb119.HR_INI_ATIV = listb.Value != "N" ? deTempo.Substring(4, 5) : "0";
                            tb119.HR_TER_ATIV = listb.Value != "N" ? deTempo.Substring(12, 5) : "0";
                            tb119.CO_TEMPO_ATIV = tempo;
                            selecTempo = true;
                            quantidadeTempos++;
                            TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119);
                            qtAtividadesLancadas++;

                            //Se foi uma atividade planejada, altera o status do planejamento para realizado
                            if (!string.IsNullOrEmpty(coPla))
                            {
                                TB17_PLANO_AULA tb17 = TB17_PLANO_AULA.RetornaPelaChavePrimaria(int.Parse(coPla));
                                tb17.FLA_EXECUTADA_ATIV = true;
                                tb17.FL_ALTER_PLA = true;
                                tb17.DT_REAL_PLA = DateTime.Parse(txtDataRe.Text);
                                TB17_PLANO_AULA.SaveOrUpdate(tb17, true);
                            }

                            //Parte responsável por percorrer a grid de alunos e lançar as presenças/faltas
                            #region Frequências

                            foreach (GridViewRow gda in grdBusca.Rows)
                            {
                                int coAlu = int.Parse(((HiddenField)gda.Cells[2].FindControl("hdCoAluno")).Value);
                                bool coFreqAluno = ((CheckBox)gda.Cells[2].FindControl("chkTevePresenca")).Checked;

                                TB132_FREQ_ALU tb132 = new TB132_FREQ_ALU();

                                tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                                tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                                tb132.CO_FLAG_FREQ_ALUNO = (!coFreqAluno ? "S" : "N");
                                tb132.CO_TUR = turma;
                                tb132.CO_MAT = CoMat;
                                tb132.DT_FRE = DateTime.Parse(txtDataRe.Text);
                                tb132.CO_COL = LoginAuxili.CO_COL;
                                tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                                tb132.CO_ATIV_PROF_TUR = tb119.CO_ATIV_PROF_TUR;
                                tb132.CO_ANO_REFER_FREQ_ALUNO = DateTime.Parse(txtDataRe.Text).Year;
                                tb132.CO_BIMESTRE = ddlReferencia.SelectedValue;
                                tb132.NR_TEMPO = tempo;
                                tb132.FL_HOMOL_FREQU = (chkHomologar.Checked ? "S" : "N");

                                TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                                qtFrequenciasLancadas++;

                                //Se estiver selecionada a opção para enviar sms para responsável de aluno ausente
                                if (chkMandaSMSResp.Checked)
                                {
                                    //Apenas se for para falta
                                    if (coFreqAluno)
                                    {
                                        TB07_ALUNO tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu).FirstOrDefault();
                                        EnviaSMS(coAlu, tb119.HR_INI_ATIV, tb119.HR_TER_ATIV, DateTime.Parse(txtDataRe.Text));
                                    }
                                }
                            }

                            #endregion
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
            HttpContext.Current.Session.Add("auxQtAtivds", qtAtividadesLancadas);
            HttpContext.Current.Session.Add("auxQtFreqs", qtFrequenciasLancadas);
            return true;
        }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (!string.IsNullOrEmpty(CO_ATIV_PROF_TURMA.Value))
            {
                if (!SalvaEdicao())
                    return;
            }
            else
            {
                if (!AcaoBarraCadastro())
                    return;
            }

            string qtAtiv = HttpContext.Current.Session["auxQtAtivds"].ToString();
            string qtFreq = HttpContext.Current.Session["auxQtFreqs"].ToString();
            AuxiliPagina.RedirecionaParaPaginaSucesso(qtAtiv + " Atividade(s) e " + qtFreq + " Frequência(s) lançada(s) com êxito!", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        private bool SalvaEdicao()
        {
            //Instancia objeto da atividade para alterar
            TB119_ATIV_PROF_TURMA tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(int.Parse(CO_ATIV_PROF_TURMA.Value));

            int qtAtividadesLancadas = 0;
            int qtFrequenciasLancadas = 0;
            foreach (GridViewRow li in grdDisciplinas.Rows)
            {
                CheckBox chk = ((CheckBox)li.Cells[0].FindControl("ckSelect"));

                if (chk.Checked)
                {
                    //Altera a atividade
                    #region Atividade

                    //Coleta informacoes preenchidas na grid de disciplinas
                    string tipo = (((DropDownList)li.Cells[2].FindControl("ddlTipoAtiv")).SelectedValue);
                    string Tema = ((TextBox)li.Cells[4].FindControl("txtTemaAula")).Text;
                    string descricao = (((TextBox)li.Cells[4].FindControl("txtDescAtividade")).Text);
                    string coPla = (((HiddenField)li.Cells[1].FindControl("hidIdPlanejamento")).Value);
                    ListBox list = ((ListBox)li.Cells[3].FindControl("lstTempo"));
                    int vlTempo = 0;

                    //Trata o tempo
                    /*================================= TEMPO FAZ PARTE DA CHAVE, NAO PODE SER ALTERADO=========================================*/
                    //foreach (ListItem listb in list.Items)
                    //{
                    //    if (listb.Selected == true)
                    //    {
                    //        int tempo = listb.Value != "N" ? int.Parse(listb.Value) : 0;
                    //        string deTempo = listb.Text;
                    //        tb119.HR_INI_ATIV = listb.Value != "N" ? deTempo.Substring(4, 5) : "0";
                    //        tb119.HR_TER_ATIV = listb.Value != "N" ? deTempo.Substring(12, 5) : "0";
                    //        tb119.CO_TEMPO_ATIV = tempo;

                    //        break;
                    //    }
                    //}

                    #region Log de Alteracoes em Atividades

                    TB142_LOG_ATIV_PROF_TURMA tb142 = new TB142_LOG_ATIV_PROF_TURMA(); // CRIA UM NOVO OBJETO DE LOG DE HOMOLOGAÇÃO DA ATIVIDADE, TB142
                    //tb142.TB119_ATIV_PROF_TURMA = tb119;
                    tb142.CO_ATIV_PROF_TUR = Convert.ToInt32(CO_ATIV_PROF_TURMA.Value);
                    tb142.CO_EMP_COL = LoginAuxili.CO_EMP;
                    tb142.CO_COL = LoginAuxili.CO_COL;
                    tb142.DT_LOG_ATIV = DateTime.Now;
                    tb142.DE_RES_ATIV_ANTES = tb119.DE_RES_ATIV;
                    tb142.DE_RES_ATIV_DEPOIS = descricao;
                    TB142_LOG_ATIV_PROF_TURMA.SaveOrUpdate(tb142, true); // SALVA O LOG

                    #endregion

                    tb119.DE_RES_ATIV = descricao; // COLOCA O RESUMO ALTERADO NA ATIVIDADE
                    tb119.DE_TEMA_AULA = Tema;
                    tb119.DT_ATIV_REAL = DateTime.Parse(txtDataRe.Text);
                    tb119.TB273_TIPO_ATIVIDADE = TB273_TIPO_ATIVIDADE.RetornaPelaSigla(tipo);
                    TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, true); // SALVA A ATIVIDADE

                    qtAtividadesLancadas++;

                    #endregion
                    list.Enabled = false;
                    //Parte responsável por percorrer a grid de alunos e lançar as presenças/faltas
                    #region Frequências

                    foreach (GridViewRow gda in grdBusca.Rows)
                    {
                        bool coFreqAluno = ((CheckBox)gda.Cells[2].FindControl("chkTevePresenca")).Checked;
                        int idFreq = int.Parse(((HiddenField)gda.Cells[0].FindControl("hdIdFreq")).Value);

                        TB132_FREQ_ALU tb132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(idFreq);
                        tb132.CO_FLAG_FREQ_ALUNO = (!coFreqAluno ? "S" : "N");
                        tb132.DT_FRE = DateTime.Parse(txtDataRe.Text);
                        tb132.NR_TEMPO = vlTempo;
                        tb132.FL_HOMOL_FREQU = (chkHomologar.Checked ? "S" : "N");
                        TB132_FREQ_ALU.SaveOrUpdate(tb132, true);

                        qtFrequenciasLancadas++;                       
                    }
                    #endregion             
                }

                HttpContext.Current.Session.Add("auxQtAtivds", qtAtividadesLancadas);
                HttpContext.Current.Session.Add("auxQtFreqs", qtFrequenciasLancadas);
            }
            return true;
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);

            //ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
            //                     select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            //ddlAno.DataTextField = "CO_ANO_GRADE";
            //ddlAno.DataValueField = "CO_ANO_GRADE";
            //ddlAno.DataBind();
            ////ddlAno.Items.Insert(0, new ListItem("Selecione", "0"));
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
            if (ddlTurma.SelectedValue != "")
            {
                CarregaDisciplinas((!string.IsNullOrEmpty(txtDataRe.Text) ? DateTime.Parse(txtDataRe.Text) : DateTime.Now));
                PesquisaGridClick();
            }
            else
            {
                ulAtividade2.Visible = false;
            }

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

            if (ddlReferencia.SelectedValue != "")
            {
                CarregaDisciplinas((!string.IsNullOrEmpty(txtDataRe.Text) ? DateTime.Parse(txtDataRe.Text) : DateTime.Now));
                PesquisaGridClick();
            }
            VerificaMostraDisciplinas();
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

                ddlProfessor.SelectedValue = LoginAuxili.CO_COL.ToString();
                ddlProfessor.Enabled = false;
            }

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            //ddlMes.Items.Clear();
        }

        /// <summary>
        /// Carrega os tipos de atividade da tabela TB273_TIPO_ATIVIDADE
        /// </summary>
        private void CarregaTipoAtividade(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.DataSource = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                              where tb273.CO_SITUA_ATIV == "A"
                              select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV }).OrderBy(o => o.NO_TIPO_ATIV);

            ddl.DataTextField = "NO_TIPO_ATIV";
            ddl.DataValueField = "ID_TIPO_ATIV";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
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

            ddl.Items.Insert(0, new ListItem("Sem Tempo", "N"));
            ddl.SelectedValue = "N";
        }

        /// <summary>
        /// Método responsável por carregar a grid
        /// </summary>
        private void CarregaDisciplinas(DateTime DT)
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

                grdBusca.DataKeyNames = new string[] { "CO_MAT" };
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

                grdBusca.DataKeyNames = new string[] { "CO_MAT" };
                grdDisciplinas.DataSource = res;
                grdDisciplinas.DataBind();
            }

            foreach (GridViewRow li in grdDisciplinas.Rows)
            {
                ListBox list = ((ListBox)li.Cells[3].FindControl("lstTempo"));
                CarregaTempo(list);
                DropDownList ddlTpAtiv = (((DropDownList)li.Cells[2].FindControl("ddlTipoAtiv")));
                CarregaTipoAtividade(ddlTpAtiv);

                int coMat = int.Parse(((HiddenField)li.Cells[1].FindControl("hdCoMat")).Value);
                TextBox txt = (((TextBox)li.Cells[4].FindControl("txtTemaAula")));
                TextBox txtDes = (((TextBox)li.Cells[4].FindControl("txtDescAtividade")));
                HiddenField hidPla = (((HiddenField)li.Cells[1].FindControl("hidIdPlanejamento")));
                VerificaExistePlanejamento(serie, turma, professor, coMat, txt, hidPla, ddlTpAtiv, list, txtDes, DT);
            }
        }

        /// <summary>
        /// Verifica se existe planejamento para os parametros em contexto
        /// </summary>
        private void VerificaExistePlanejamento(int CO_CUR, int CO_TUR, int CO_COL, int CO_MAT, TextBox txtTema, HiddenField hidCoPla, DropDownList ddlTpAtiv, ListBox lstTempos, TextBox descricao, DateTime DT)
        {
            var res = (from tb17 in TB17_PLANO_AULA.RetornaTodosRegistros()
                       where tb17.CO_EMP == LoginAuxili.CO_EMP
                       && tb17.CO_CUR == CO_CUR
                       && tb17.CO_TUR == CO_TUR
                       && tb17.CO_COL == CO_COL
                       && tb17.CO_MAT == CO_MAT
                       && tb17.FLA_EXECUTADA_ATIV == false
                       && (EntityFunctions.TruncateTime(tb17.DT_PREV_PLA) == EntityFunctions.TruncateTime(DT))
                       select new
                       {
                           tb17.DE_TEMA_AULA,
                           tb17.NU_TEMP_PLA,
                           tb17.CO_PLA_AULA,
                           tb17.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV,
                           tb17.DE_METODOLOGIA,
                       }).FirstOrDefault();

            //Apenas se houver planejamento
            if (res != null)
            {
                txtTema.Text = res.DE_TEMA_AULA;
                hidCoPla.Value = res.CO_PLA_AULA.ToString();
                ddlTpAtiv.SelectedValue = res.ID_TIPO_ATIV.ToString();
                if (res.NU_TEMP_PLA.HasValue && lstTempos.Items.FindByValue(res.NU_TEMP_PLA.ToString()) != null)
                    lstTempos.SelectedValue = res.NU_TEMP_PLA.ToString();
                else
                {
                    CarregaTempo(lstTempos);
                    if (res.NU_TEMP_PLA.HasValue && lstTempos.Items.FindByValue(res.NU_TEMP_PLA.ToString()) != null)
                        lstTempos.SelectedValue = res.NU_TEMP_PLA.ToString();
                }
                descricao.Text = res.DE_METODOLOGIA;
            }
        }

        /// <summary>
        /// Verifica qual a primeira data do bimestre e retorna
        /// </summary>
        /// <returns></returns>
        private DateTime VerificaPrimeiraDataBimestre()
        {
            #region Verifica Datas Bimestre

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataReali = DateTime.Parse(txtDataRe.Text);
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlReferencia.SelectedValue == "B1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1 != null)
                    {
                        return (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1.HasValue ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1.Value : DateTime.Now);
                    }
                    else
                        return DateTime.Now;
                }
                else if (ddlReferencia.SelectedValue == "B2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2 != null)
                    {
                        return (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2.HasValue ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2.Value : DateTime.Now);
                    }
                    else
                        return DateTime.Now;
                }
                else if (ddlReferencia.SelectedValue == "B3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3 != null)
                    {
                        return (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3.HasValue ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3.Value : DateTime.Now);
                    }
                    else
                        return DateTime.Now;
                }
                else
                {
                    if (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4 != null)
                    {
                        return (tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4.HasValue ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4.Value : DateTime.Now);
                    }
                    else
                        return DateTime.Now;
                }
            }
            else
                return DateTime.Now;

            #endregion
        }

        /// <summary>
        /// Verifica, de acordo com o bimestre selecionado e a data de realização da atividade informada, se a data está dentro do período do bimestre
        /// </summary>
        private bool VerificaDatasPeriodoBimestre()
        {
            #region Verifica Datas Bimestre

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataReali = DateTime.Parse(txtDataRe.Text);
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlReferencia.SelectedValue == "B1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if ((tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 > dataReali) || (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 < dataReali))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A data de realização da atividade está fora do período do bimestre selecionado. " + tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1.Value.ToString("dd/MM/yy") + " à " + tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1.Value.ToString("dd/MM/yy"));
                            txtDataRe.Focus();
                            return false;
                        }
                    }
                }
                else if (ddlReferencia.SelectedValue == "B2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if ((tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 > dataReali) || (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 < dataReali))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A data de realização da atividade está fora do período do bimestre selecionado. " + tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2.Value.ToString("dd/MM/yy") + " à " + tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2.Value.ToString("dd/MM/yy"));
                            txtDataRe.Focus();
                            return false;
                        }
                    }
                }
                else if (ddlReferencia.SelectedValue == "B3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if ((tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 > dataReali) || (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 < dataReali))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A data de realização da atividade está fora do período do bimestre selecionado. " + tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3.Value.ToString("dd/MM/yy") + " à " + tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3.Value.ToString("dd/MM/yy"));
                            txtDataRe.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                    {
                        if ((tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 > dataReali) || (tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 < dataReali))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A data de realização da atividade está fora do período do bimestre selecionado. " + tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4.Value.ToString("dd/MM/yy") + " à " + tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4.Value.ToString("dd/MM/yy"));
                            txtDataRe.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
            #endregion
        }

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid(int coAtiv = 0)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string referencia = ddlReferencia.SelectedValue;
            string anoMesMat = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            // Instancia o contexto
            var ctx = GestorEntities.CurrentContext;

            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                       where tb08.CO_EMP == LoginAuxili.CO_EMP
                       && tb08.CO_ANO_MES_MAT == anoMesMat
                       && tb08.CO_CUR == serie
                       && tb08.CO_TUR == turma
                       && tb08.CO_SIT_MAT == "A"
                       select new AlunosMat
                       {
                           CO_ALU = tb07.CO_ALU,
                           NO_ALU = tb07.NO_ALU,
                           CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                           NU_NIRE = tb07.NU_NIRE,
                           CO_SIT_MAT = tb08.CO_SIT_MAT,
                           // CO_ATIV_PROF_TUR = tb08.tb,
                       }).Distinct().OrderBy(w => w.NO_ALU).ToList();

            var lstAlunoMatricula = res.ToList(); // GERA UM OBJETO DO TIO LISTA COM O RESULTADO DO SELECT

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Verifica se existe alguma disciplina selecionada e desaparece a grid de alunos caso não tenha
        /// </summary>
        private void VerificaDisciplinaSelecioanda()
        {
            bool Tem = false;
            foreach (GridViewRow li in grdDisciplinas.Rows)
            {
                //Verifica se está selecionado
                if (((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked)
                {
                    Tem = true;
                    break;
                }
            }

            //Se não tiver nenhum selecionado, então some os alunos
            if (!Tem)
                ulAlunos2.Visible = false;
        }

        /// <summary>
        /// Faz a verificacao em todos os postbacks para mostrar ou nao as informacoes pertinentes
        /// </summary>
        private void VerificaMostraDisciplinas(bool CarregamentoPagina = false)
        {
            //Muda o foco de acordo com as opcoes selecionadas e disponiveis
            if ((ddlModalidade.Items.Count > 1) && (ddlModalidade.SelectedValue == ""))
                ddlModalidade.Focus();
            else if ((ddlSerieCurso.Items.Count > 1) && (ddlSerieCurso.SelectedValue == ""))
                ddlSerieCurso.Focus();
            else if ((ddlTurma.Items.Count > 1) && (ddlTurma.SelectedValue == ""))
                ddlTurma.Focus();
            else if (ddlReferencia.SelectedValue == "")
                ddlReferencia.Focus();

            //Se tiver apenas a opção "Selecione" na modalidade, então o professor não é associado à nenhuma turma,
            //então o ddl de professor recebe o foco para alteração
            if ((ddlModalidade.Items.Count == 1) && (ddlModalidade.SelectedValue == ""))
                ddlProfessor.Focus();
        }

        /// <summary>
        /// Método responsável por enviar sms ao responsável de aluno(a) ausente
        /// </summary>
        /// <param name="NU_CELULAR"></param>
        /// <param name="CO_ALU"></param>
        /// <param name="CO_RESP"></param>
        /// <param name="hrInicio"></param>
        /// <param name="hrFinal"></param>
        private void EnviaSMS(int CO_ALU, string hrInicio, string hrFinal, DateTime dataRea)
        {
            //***IMPORTANTE*** - O limite máximo de caracteres de acordo com a ZENVIA que é quem presta o serviço de envio,
            //é de 140 caracteres para NEXTEL e 150 caracteres para DEMAIS OPERADORAS
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == CO_ALU).FirstOrDefault();
            tb07.TB108_RESPONSAVELReference.Load();

            if (tb07.TB108_RESPONSAVEL == null) //Se não tiver responsável associado, retorna.
                return;

            TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.CO_RESP == tb07.TB108_RESPONSAVEL.CO_RESP).FirstOrDefault();
            string NO_ALU = tb07.NO_ALU;
            string NU_CELULAR = tb108.NU_TELE_CELU_RESP;

            string noEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NO_FANTAS_EMP;

            bool masc = tb108.CO_SEXO_RESP == "M" ? true : false;
            string noResp = tb108.NO_RESP.Length > 30 ? tb108.NO_RESP.Substring(0, 30) : tb108.NO_RESP;
            string noAlu = NO_ALU.Length > 30 ? NO_ALU.Substring(0, 30) : NO_ALU;
            string texto = "";

            texto = (masc ? "Sr " : "Sra ") + noResp + ", informamos que " + noAlu + " faltou à aula" + (hrInicio == "0" ? " sem período" : " de " + hrInicio + " à " + hrFinal) + " dia " + dataRea.ToString("dd/MM");

            //Envia a mensagem apenas se o número do celular for diferente de nulo
            if (NU_CELULAR != null)
            {
                var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
                string retorno = "";

                if (admUsuario.QT_SMS_MES_USR != null && admUsuario.QT_SMS_MAXIM_USR != null)
                {
                    if (admUsuario.QT_SMS_MAXIM_USR <= admUsuario.QT_SMS_MES_USR)
                    {
                        //retorno = "Saldo do envio de SMS para outras pessoas ultrapassado.";
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
                    tb249.CO_EMP_RECEPT = LoginAuxili.CO_EMP;
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

        //====> MÉTODO QUE CARREGA A GRID DE ATIVIDADES
        private void CarregaGridAtividade()
        {
            string coAnoRefPla = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue.Trim() : "";
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string referencia = ddlReferencia.SelectedValue != "" ? ddlReferencia.SelectedValue.Trim() : "";
            int Professor = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            DateTime dtFre = DateTime.Parse(txtDataRe.Text);

            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_ANO_MES_MAT == coAnoRefPla
                             && tb119.CO_MODU_CUR == modalidade
                             && tb119.CO_CUR == serie
                             && tb119.CO_TUR == turma
                                 //&& tb119.CO_BIMESTRE == Bimestre
                             && tb119.CO_COL_ATIV == Professor
                             select new AtividadeSaida
                             {
                                 CO_ATIV_PROF_TUR = tb119.CO_ATIV_PROF_TUR,
                                 FL_HOMOL_ATIV = tb119.FL_HOMOL_ATIV == "N" ? "Não" : "Sim",
                                 DT_ATIV_REAL = tb119.DT_ATIV_REAL,
                                 DE_RES_ATIV = tb119.DE_RES_ATIV,
                                 CO_MAT = tb119.CO_MAT,
                                 DE_TEMA = tb119.DE_TEMA_AULA,
                             }).OrderByDescending(p => p.DT_ATIV_REAL).Distinct().ToList();
            if (resultado.Count > 0)
            {
                ulAlunos2.Visible = true;
                li1.Visible = true;
                grdAtividades.DataSource = resultado;
                grdAtividades.DataBind();
            }
            else
            {
                //divGrid.Visible = true;
                li1.Visible = false;
            }


        }

        /// <summary>
        /// Método responsável por carregar a grid
        /// </summary>
        private void CarregaDisciplinasAtividade(int? CodAtividade)
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
                              && tb02.CO_MAT == CodAtividade
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                grdBusca.DataKeyNames = new string[] { "CO_MAT" };
                grdDisciplinas.DataSource = res;
                grdDisciplinas.DataBind();
            }
            else
            {
                var res = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where tbres.CO_MODU_CUR == modalidade
                            && tb02.CO_MAT == CodAtividade
                           && tbres.CO_CUR == serie
                           && tbres.CO_COL_RESP == professor
                           && tbres.CO_TUR == turma
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                grdBusca.DataKeyNames = new string[] { "CO_MAT" };
                grdDisciplinas.DataSource = res;
                grdDisciplinas.DataBind();
            }

            foreach (GridViewRow li in grdDisciplinas.Rows)
            {
                ListBox list = ((ListBox)li.Cells[3].FindControl("lstTempo"));
                CarregaTempo(list);
                DropDownList ddlTpAtiv = (((DropDownList)li.Cells[2].FindControl("ddlTipoAtiv")));
                CarregaTipoAtividade(ddlTpAtiv);

                int coMat = int.Parse(((HiddenField)li.Cells[1].FindControl("hdCoMat")).Value);
                TextBox txt = (((TextBox)li.Cells[4].FindControl("txtTemaAula")));
                TextBox txtDes = (((TextBox)li.Cells[4].FindControl("txtDescAtividade")));
                HiddenField hidPla = (((HiddenField)li.Cells[1].FindControl("hidIdPlanejamento")));
                //VerificaExistePlanejamento(serie, turma, professor, coMat, txt, hidPla, ddlTpAtiv, list, txtDes, DT);
            }
        }

        // ====> MÉTODO QUE CARREGA A GRID DE FREQUÊNCIAS
        private void CarregaGridFrequencia(int coAtividade)
        {
            var ctx = GestorEntities.CurrentContext;
            #region Consulta utilizando o linq
            var lista = (from fre in ctx.TB132_FREQ_ALU.AsQueryable()
                         where fre.CO_ATIV_PROF_TUR == coAtividade
                         select new
                         {
                             chk = fre.CO_FLAG_FREQ_ALUNO == "N" ? true : false,
                             CO_ALU = fre.TB07_ALUNO.CO_ALU,
                             NO_ALU = fre.TB07_ALUNO.NO_ALU,
                             CO_ANO_MES_MAT = fre.CO_ANO_REFER_FREQ_ALUNO,
                             NIRE = fre.TB07_ALUNO.NU_NIRE,
                             CO_FLAG_FREQ_ALUNO = fre.CO_FLAG_FREQ_ALUNO,
                             DT_FRE = fre.DT_FRE,
                             FL_HOMOL_FREQU = fre.FL_HOMOL_FREQU,
                             ID_FREQU_ALUNO = fre.ID_FREQ_ALUNO,
                             CO_ATIV_PROF_TUR = fre.CO_ATIV_PROF_TUR,
                         }
                         ).Distinct().OrderBy(o => o.NO_ALU);

            if (coAtividade != null)
            {
                var lstAlunoMatricula = lista.Where(w => w.CO_ATIV_PROF_TUR == coAtividade).ToList();
                grdBusca.DataKeyNames = new string[] { "CO_ALU" };

                grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
                grdBusca.DataBind(); // CARREGA OS VALORES DA GRID, DE ACORDO COM A LISTA RETORNADA
            }
            else
            {
                var lstAlunoMatricula = lista.ToList();
                grdBusca.DataKeyNames = new string[] { "CO_ALU" };

                grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
                grdBusca.DataBind(); // CARREGA OS VALORES DA GRID, DE ACORDO COM A LISTA RETORNADA
            }
            #endregion
        }

        #endregion

        #region Class

        public class AtividadeSaida
        {
            //public string chk { get; set; }
            //public bool chkValid
            //{
            //    get
            //    {
            //        if (this.chk == "S")
            //            return true;
            //        else
            //            return false;
            //    }
            //}
            ////public bool nChk 
            ////{
            ////    get
            ////    {
            ////        return !this.chk;
            ////    }
            ////}
            public int CO_MAT { get; set; }
            public string FL_HOMOL_ATIV { get; set; }
            public DateTime DT_ATIV_REAL { get; set; }
            public string DT_ATIV
            {
                get
                {
                    return this.DT_ATIV_REAL.ToString("dd/MM/yyyy");
                }
            }
            public string DE_RES_ATIV { get; set; }
            public int CO_ATIV_PROF_TUR { get; set; }
            public string DE_TEMA { get; set; }
            //public int CO_TEMPO_ATIV { get; set; }
        }

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

        public class AlunosMat
        {
            public string CO_ATIV_PROF_TUR { get; set; }
            public int? CO_ALU { get; set; } // CODIGO DO ALUNO
            public string NO_ALU { get; set; } // NOME DO ALUNO
            public string CO_ANO_MES_MAT { get; set; } // ANO DE MATRÍCULA
            public int? NU_NIRE { get; set; } // NIRE DO ALUNO
            public int? ID_FREQU_ALUNO { get; set; } // ID DA FREQUÊNCIA LANÇADA
            public string FL_HOMOL_FREQU { get; set; } // FLAG DE HOMOLOGAÇÃO
            // NIRE COM NÚMERO DE CARACTERES AJUSTADO, PADRÃO 9
            public string NIRE
            {
                get
                {
                    string n = this.NU_NIRE.ToString(); // RECEBE O VALOR DO NIRE
                    while (n.Length < 9) // LOOP QUE ACONTECE ENQUANTO A QUANTIDADE DE CARACTERES DO NIRE FOR MENOR QUE 9
                    {
                        n = "0" + n; // INCLUI "0" A ESQUERDA DO NIRE
                    }
                    return n; // RETORNA O NIRE AJUSTADO PARA O OBJETO
                }
            }
            public string CO_SIT_MAT { get; set; } // SITUAÇÃO DA MATRÍCULA DO ALUNO
            public string CO_FLAG_FREQ_ALUNO { get; set; } // FREQUÊNCIA DO ALUNO (S = PRESENÇA, N = FALTA)
            public DateTime? DT_FRE { get; set; } // DATA DA FREQUÊNCIA
            public bool chk
            {
                get
                {
                    return false;
                }
            }
        }

        #endregion

        #region Metodos personalizados

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

        private void PesquisaGridClick()
        {
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            //if ((modalidade == 0) || (turma == 0) || (serie == 0) || (txtDataRe.Text == ""))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "Os campos de Data de frequência, Modalidade, Série e Turma devem ser informados.");
            //    return;
            //}

            //if (txtDataRe.Text == "")
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "Data deve ser informada.");
            //    return;
            //}

            //DateTime DataIni;

            //if (!DateTime.TryParse(txtDataRe.Text, out DataIni))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "Data de Frequência informa é inválida.");
            //    return;
            //}

            //if (DataIni > DateTime.Now)
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "Data informada não pode ser superior a data atual.");
            //    return;
            //}

            CarregaGrid();
        }

        protected void txtDataRe_OnTextChanged(object sender, EventArgs e)
        {
            CarregaDisciplinas((!string.IsNullOrEmpty(txtDataRe.Text) ? DateTime.Parse(txtDataRe.Text) : DateTime.Now));
        }

        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                CarregaProfessor();
                CarregaModalidades();
            }
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

        //protected void ddlTipoAtiv_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (((DropDownList)sender).SelectedValue != "0")
        //    {
        //        int tipoAtiv = ddlTipoAtiv.SelectedValue != "" ? int.Parse(ddlTipoAtiv.SelectedValue) : 0;

        //        if (tipoAtiv != 0)
        //        {
        //            string flLancaNota = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where tb273.CO_SITUA_ATIV == "A" && tb273.ID_TIPO_ATIV == tipoAtiv select new { tb273.FL_LANCA_NOTA_ATIV }).FirstOrDefault().FL_LANCA_NOTA_ATIV;
        //            ddlAvaliaAtiv.SelectedValue = flLancaNota;
        //        }
        //    }

        //    if (ddlTipoAtiv.SelectedValue != "0")
        //        liDisc.Visible = true;
        //    else
        //        ddlTipoAtiv.Focus();

        //    if (ddlTurma.SelectedValue != "")
        //    {
        //        CarregaDisciplinas();
        //        PesquisaGridClick();
        //    }
        //    else
        //    {
        //        ulAtividade.Visible = false;
        //    }
        //}

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTurma.SelectedValue != "")
            {
                //CarregaDisciplinas();
                PesquisaGridClick();
                CarregaDisciplinas((!string.IsNullOrEmpty(txtDataRe.Text) ? DateTime.Parse(txtDataRe.Text) : DateTime.Now));
            }
            else
            {
                ulAtividade2.Visible = false;
            }
        }

        protected void ddlReferencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ddlReferencia.SelectedValue != "") && (!string.IsNullOrEmpty(ddlAno.SelectedValue))
                && (!string.IsNullOrEmpty(ddlProfessor.SelectedValue)) && (!string.IsNullOrEmpty(ddlModalidade.SelectedValue))
                && (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue)) && (!string.IsNullOrEmpty(ddlTurma.SelectedValue)))
            {
                //CarregaDisciplinas();
                //PesquisaGridClick();
                ulAtividade2.Visible = liDisc.Visible = true;
                PesquisaGridClick();
                txtDataRe.Text = VerificaPrimeiraDataBimestre().ToString();
                CarregaDisciplinas((!string.IsNullOrEmpty(txtDataRe.Text) ? DateTime.Parse(txtDataRe.Text) : DateTime.Now));
            }
            else
            {
                ulAtividade2.Visible = liDisc.Visible = false;
            }
            CarregaGridAtividade();
        }

        protected void ddlProfessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0" && ((DropDownList)sender).SelectedValue != "")
            {
                //ddlProfessorReal.SelectedValue = ((DropDownList)sender).SelectedValue;
                grdDisciplinas.DataSource = null;
                grdDisciplinas.DataBind();
                CarregaModalidades();
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
                        ListBox tempo = (((ListBox)linha.Cells[3].FindControl("lstTempo")));
                        TextBox tema = (((TextBox)linha.Cells[4].FindControl("txtTemaAula")));
                        TextBox desc = (((TextBox)linha.Cells[4].FindControl("txtDescAtividade")));
                        DropDownList tipo = (((DropDownList)linha.Cells[2].FindControl("ddlTipoAtiv")));

                        if (chk.Checked)
                        {
                            tempo.Enabled = tema.Enabled = desc.Enabled = tipo.Enabled = true;
                            tema.Focus();
                            ulAlunos2.Visible = true;
                        }
                        else
                        {
                            tempo.Enabled = tema.Enabled = desc.Enabled = tipo.Enabled = false;
                            VerificaDisciplinaSelecioanda();
                        }
                    }
                }
            }
        }

        protected void ckAtividades_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdDisciplinas.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades para pupular a linha 
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                    if (atual.ClientID == chk.ClientID)
                    {
                        #region Se for o checkbox que invocou o postback

                        if (chk.Checked == true)
                        {
                            string CoMatAtividade = (((HiddenField)linha.Cells[0].FindControl("hdCoMatAtividade")).Value);
                            string CoProfAtividadeTurma = (((HiddenField)linha.Cells[0].FindControl("hdCoAtividadeProfTurma")).Value);
                            int CoProfTurma = Convert.ToInt32(CoProfAtividadeTurma);
                            foreach (GridViewRow li in grdDisciplinas.Rows)
                            {

                                string CoMateria = (((HiddenField)li.Cells[0].FindControl("hdCoMat")).Value);
                                if (CoMatAtividade == CoMateria)
                                {
                                    var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros().Where(a => a.CO_ATIV_PROF_TUR == CoProfTurma)
                                                     select new
                                                     {
                                                         Tempo = tb119.CO_TEMPO_ATIV,
                                                         tema = tb119.DE_TEMA_AULA,
                                                         desc = tb119.DE_RES_ATIV,
                                                         tipo = tb119.CO_TIPO_ATIV,
                                                         data = tb119.DT_ATIV_REAL,
                                                         CoAtividade = tb119.CO_ATIV_PROF_TUR

                                                     }).SingleOrDefault();

                                    if (resultado != null)
                                    {
                                        CheckBox k = (((CheckBox)li.Cells[0].FindControl("ckSelect")));
                                        k.Checked = true;

                                        ListBox tempo = (((ListBox)li.Cells[3].FindControl("lstTempo")));
                                        TextBox tema = (((TextBox)li.Cells[4].FindControl("txtTemaAula")));
                                        TextBox desc = (((TextBox)li.Cells[4].FindControl("txtDescAtividade")));
                                        DropDownList tipo = (((DropDownList)li.Cells[2].FindControl("ddlTipoAtiv")));

                                        TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaTodosRegistros().Where(w => w.CO_SIGLA_ATIV == resultado.tipo).FirstOrDefault();

                                        if (resultado.Tempo != 0 && tempo.Items.FindByValue(resultado.Tempo.ToString()) != null)
                                            tempo.SelectedValue = Convert.ToString(resultado.Tempo);
                                        else
                                        {
                                            CarregaTempo(tempo);
                                            if (resultado.Tempo != 0 && tempo.Items.FindByValue(resultado.Tempo.ToString()) != null)
                                                tempo.SelectedValue = Convert.ToString(resultado.Tempo);
                                        }

                                        tempo.SelectedValue = resultado.Tempo != 0 ? Convert.ToString(resultado.Tempo) : "N";                                   
                                        tema.Text = Convert.ToString(resultado.desc);
                                        desc.Text = resultado.desc;
                                        tipo.SelectedValue = (tb273 != null ? tb273.ID_TIPO_ATIV.ToString() : "");
                                        txtDataRe.Text = resultado.data.ToString();
                                        CO_ATIV_PROF_TURMA.Value = Convert.ToString(resultado.CoAtividade);

                                        tempo.Enabled = tema.Enabled = desc.Enabled = tipo.Enabled = true;
                                        CarregaGridFrequencia(CoProfTurma);
                                        tema.Focus();
                                        ulAlunos2.Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            CO_ATIV_PROF_TURMA.Value = "";
                            txtDataRe.Text = VerificaPrimeiraDataBimestre().ToString();
                            CarregaDisciplinas((!string.IsNullOrEmpty(txtDataRe.Text) ? DateTime.Parse(txtDataRe.Text) : DateTime.Now));
                        }

                        #endregion
                    }
                    else
                        chk.Checked = false;
                }
            }
        }

        #endregion

        #region Torna Grids Clicáveis

        protected void grdDisciplinas_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.cursor='pointer';");
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdDisciplinas.UniqueID + "','Select$" + qtdLinhasDisciplinas + "')");
                qtdLinhasDisciplinas = qtdLinhasDisciplinas + 1;
            }
        }

        protected void grdBusca_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.cursor='pointer';");
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdBusca.UniqueID + "','Select$" + qtdLinhasAlunos + "')");
                qtdLinhasAlunos = qtdLinhasAlunos + 1;
            }
        }

        protected void grdDisciplinas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ////--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            //if (grdDisciplinas.DataKeys[grdDisciplinas.SelectedIndex].Value != null)
            //{
            //    // Passa por todos os registros da grid de Pré-Atendimentos
            //    foreach (GridViewRow linha in grdDisciplinas.Rows)
            //    {
            //        CheckBox chk = (CheckBox)linha.Cells[0].FindControl("ckselect");
            //        int idCoMat = int.Parse((((HiddenField)linha.Cells[0].FindControl("hdCoMat")).Value));

            //        //Se a linha clicada for a mesma na qual está sendo passada agora
            //        if (idCoMat == Convert.ToInt32(grdDisciplinas.DataKeys[grdDisciplinas.SelectedIndex].Value))
            //        {
            //            ListBox tempo = (((ListBox)linha.Cells[2].FindControl("lstTempo")));
            //            TextBox tema = (((TextBox)linha.Cells[3].FindControl("txtTemaAula")));
            //            TextBox desc = (((TextBox)linha.Cells[3].FindControl("txtDescAtividade")));

            //            //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
            //            if (chk.Checked)
            //            {
            //                if (((GridView)sender != tema) && ((TextBox)sender != desc) && ((ListBox)sender != tempo))
            //                {
            //                    chk.Checked = tempo.Enabled = tema.Enabled = desc.Enabled = false;
            //                }
            //            }
            //            //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
            //            else
            //            {
            //                chk.Checked = tempo.Enabled = tema.Enabled = desc.Enabled = true;
            //                tema.Focus();
            //                ulAlunos.Visible = true;
            //            }
            //        }
            //    }
            //}
        }

        protected void grdBusca_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdBusca.DataKeys[grdBusca.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    int coAlu = int.Parse((((HiddenField)linha.Cells[2].FindControl("hdCoAluno")).Value));

                    //Se a linha clicada for a mesma na qual está sendo passada agora
                    if (coAlu == Convert.ToInt32(grdBusca.DataKeys[grdBusca.SelectedIndex].Value))
                    {
                        CheckBox chk = (CheckBox)linha.Cells[2].FindControl("chkTevePresenca");

                        //Alterna marcando ou desmarcando, sempre o contrário do que está atualmente
                        chk.Checked = !chk.Checked;
                    }
                }
            }
        }

        #endregion
    }
}