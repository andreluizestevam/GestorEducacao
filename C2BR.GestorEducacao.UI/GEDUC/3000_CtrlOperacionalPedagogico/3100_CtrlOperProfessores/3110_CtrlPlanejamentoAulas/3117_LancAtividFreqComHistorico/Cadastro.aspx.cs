//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO DE FREQÜÊNCIA DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 25/02/2013
//------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            |O campo ano (ddlAno) foi retirado e o
// ----------+----------------------------+-------------------------------------
// 13/11/2021|Fabricio S dos Santos       |Criação do metodo de cadastro, correção de habilitação da grdTempo e posição da grdBusca

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3117_LancAtividFreqComHistorico
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
            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString();
                CarregaTipoTempos();
                CarregaMedidas();

                bool red = CarregaParametros();
                if (red == false)
                {
                    CarregaModalidades();
                    carregaBusca();
                }
            }              
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

        /// <summary>
        /// É executado logo depois que o usuário salva um registro, quando a página recarrega, este método é responsável por inserir as devidas informações novamente.
        /// </summary>
        private void carregaBusca()
        {
            if (HttpContext.Current.Session["BuscaSuperior"] != null)
            {
                var parametros = HttpContext.Current.Session["BuscaSuperior"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var data = par[0];
                    var modalidade = par[1];
                    var serieCurso = par[2];
                    var turma = par[3];
                    var coTempo = par[4];
                    var coBime = par[5];
                    var disciplina = par[6];

                    txtData.Text = data;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    ddlReferencia.SelectedValue = coBime;

                    CarregaMaterias();
                    ddlDisciplina.SelectedValue = disciplina;

                    HttpContext.Current.Session.Remove("BuscaSuperior");
                    PesquisaGridClick();
                    //divGridAluno.Visible = true;
                }
            }
        }

        private bool CarregaParametros()
        {
            bool redirect = false;
            if (HttpContext.Current.Session["ParametrosBusca"] != null)
            {
                var parametros = HttpContext.Current.Session["ParametrosBusca"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var modalidade = par[0];
                    var serieCurso = par[1];
                    var turma = par[2];
                    var dtRealizacao = par[3];
                    var disciplina = par[4];
                    var ano = par[5];
                    var coAtivProfTur = par[6];
                    var coColProf = par[7];
                    var coBime = par[8];

                    txtData.Text = dtRealizacao;
                    txtData.Enabled = false;

                    ddlReferencia.SelectedValue = coBime;
                    ddlReferencia.Enabled = false;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;
                    ddlModalidade.Enabled = false;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;
                    ddlSerieCurso.Enabled = false;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;
                    ddlTurma.Enabled = false;

                    int coMod = (modalidade != "" ? int.Parse(modalidade) : 0);
                    int coCur = (serieCurso != "" ? int.Parse(serieCurso) : 0);

                    //Coleta qual o tipo de frequência do curso selecionado
                    string tpFreq = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coMod, coCur).CO_PARAM_FREQ_TIPO;

                    //if (tpFreq == "M")
                    //{
                    CarregaMaterias();
                    ddlDisciplina.SelectedValue = disciplina;
                    //ddlDisciplina.Enabled = false;
                    //}

                    hdCoAtivProfTur.Value = coAtivProfTur;
                    HttpContext.Current.Session.Remove("ParametrosBusca");
                    redirect = true;
                    PesquisaGridClick();
                    //divGridAluno.Visible = true;
                }
            }
            return redirect;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {            
            int qtFrequenciasLancadas = 0;
            if (ddlTempos.SelectedValue == "N")
            {
                qtFrequenciasLancadas = CadastraFreq(qtFrequenciasLancadas, 0);
            }
            else {
                foreach (GridViewRow gdt in grdTempos.Rows)
                {                   
                    if (((CheckBox)gdt.Cells[0].FindControl("cbMarcar")).Checked)
                    {
                        var label = (Label)gdt.Cells[0].FindControl("lblNomeTempo");
                        int tempo = int.Parse(label.Text);
                        qtFrequenciasLancadas = CadastraFreq(qtFrequenciasLancadas, tempo);
                    }
                }
            }    
           AuxiliPagina.RedirecionaParaPaginaSucesso(qtFrequenciasLancadas + " Frequência(s) lançada(s) com êxito!", Request.Url.AbsoluteUri);
        }

        private int CadastraFreq(int qtFrequenciasLancadas, int tempo)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int CoMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;


            //Verifica se é possível já lançar atividade/frequência homologadas
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            res.TB83_PARAMETROReference.Load();
            bool LancHomol = (res.TB83_PARAMETRO.FL_LANCA_FREQ_ALUNO_HOMOL == "S" ? true : false);

            //int coAtivProfTur = TB199_FREQ_FUNC.RetornaTodosRegistros().whe;
            foreach (GridViewRow gda in grdBusca.Rows)
            {
                int coAlu = int.Parse(((HiddenField)gda.Cells[2].FindControl("hdCoAluno")).Value);
                bool coFreqAluno = ((CheckBox)gda.Cells[2].FindControl("chkFreq")).Checked;

                TB132_FREQ_ALU tb132 = RetornaEntidade();

                if (tb132 == null)
                {
                    tb132 = new TB132_FREQ_ALU();
                }
           
                tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                tb132.CO_FLAG_FREQ_ALUNO = (!coFreqAluno ? "S" : "N");
                tb132.CO_TUR = turma;
                tb132.CO_MAT = CoMat;
                tb132.DT_FRE = DateTime.Parse(txtData.Text);
                tb132.CO_COL = LoginAuxili.CO_COL;
                tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                //tb132.CO_ATIV_PROF_TUR = ;
                tb132.CO_ANO_REFER_FREQ_ALUNO = DateTime.Parse(txtData.Text).Year;
                tb132.CO_BIMESTRE = ddlReferencia.SelectedValue;
                tb132.NR_TEMPO = tempo;
                tb132.FL_HOMOL_FREQU = (LancHomol ? "S" : "N");

                TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                qtFrequenciasLancadas++;
            }

            return qtFrequenciasLancadas;
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid(int coAtiv = 0)
        {
            int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string referencia = ddlReferencia.SelectedValue;
            DateTime data = DateTime.Parse(txtData.Text);
            string anoMesMat = data.Year.ToString();
            int anoInt = int.Parse((DateTime.Parse(txtData.Text).Year).ToString());

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            // Instancia o contexto
            var ctx = GestorEntities.CurrentContext;

            string sql = "";
            if (materia != null) // VALIDA SE FOI SELECIONADA UMA MATÉRIA
            {
                var query = (from tb08 in ctx.TB08_MATRCUR
                             join tb07 in ctx.TB07_ALUNO on tb08.CO_ALU equals tb07.CO_ALU
                             where tb08.CO_EMP == LoginAuxili.CO_EMP
                             && tb08.CO_ANO_MES_MAT == anoMesMat.Trim()
                             && tb08.CO_CUR == serie
                             && tb08.CO_TUR == turma
                             && tb08.CO_SIT_MAT == "A"
                             select new AlunosMat
                             {
                                 CO_ALU = tb07.CO_ALU,
                                 NO_ALU = tb07.NO_ALU,
                                 CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                                 NU_NIRE = tb07.NU_NIRE,
                                 CO_SIT_MAT = tb08.CO_SIT_MAT
                             }).ToList();

                // SELECT POR MATÉRIA
                sql = "select distinct " +
                            "a.CO_ALU, " +
                            "a.NO_ALU, " +
                            "m.CO_ANO_MES_MAT, " +
                            "a.NU_NIRE, " +
                            "m.CO_SIT_MAT, " +
                            "f.CO_FLAG_FREQ_ALUNO, " +
                            "f.DT_FRE, " +
                            "f.FL_HOMOL_FREQU, " +
                            "f.ID_FREQ_ALUNO " +
                        "from TB08_MATRCUR m " +
                        "left join TB132_FREQ_ALU f " +
                            "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + " and f.CO_MAT = " + materia + " and f.CO_BIMESTRE = '" + referencia + "') " +
                        "inner join TB07_ALUNO a " +
                            "on (a.CO_ALU = m.CO_ALU) " +
                        "where m.CO_EMP = " + LoginAuxili.CO_EMP +
                        " and m.CO_ANO_MES_MAT = " + anoMesMat.Trim() +
                        " and m.CO_CUR = " + serie +
                        " and m.CO_TUR = " + turma +
                        " and m.CO_SIT_MAT = 'A'" +
                        "order by a.NO_ALU";
            }
            else
            {
                // SELECT SEM MATÉRIA
                sql = "select distinct " +
                            "a.CO_ALU, " +
                            "a.NO_ALU, " +
                            "m.CO_ANO_MES_MAT, " +
                            "a.NU_NIRE, " +
                            "m.CO_SIT_MAT, " +
                            "f.CO_FLAG_FREQ_ALUNO, " +
                            "f.DT_FRE, " +
                            "f.FL_HOMOL_FREQU, " +
                            "f.ID_FREQ_ALUNO " +
                        "from TB08_MATRCUR m " +
                        "left join TB132_FREQ_ALU f " +
                            "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + " and f.CO_BIMESTRE = '" + referencia + "') " +
                        "inner join TB07_ALUNO a " +
                            "on (a.CO_ALU = m.CO_ALU) " +
                        "where m.CO_EMP = " + LoginAuxili.CO_EMP +
                        " and m.CO_ANO_MES_MAT = " + anoMesMat.Trim() +
                        " and m.CO_CUR = " + serie +
                        " and m.CO_TUR = " + turma +
                        " and m.CO_SIT_MAT = 'A'" +
                        "order by a.NO_ALU";
            }

            var lstA = GestorEntities.CurrentContext.ExecuteStoreQuery<AlunosMat>(sql).ToList(); // EXECUTA O SELECT GERANDO UMA LISTA DA CLASSE ALUNOSMAT

            #region Carrega as datas em que houveram frequências para esta turma

            //Lista as datas em que houveram frequencias lançadas para esta modalidade, turma, serie, ano e matéria
            var reFreq = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                          where tb132.CO_ANO_REFER_FREQ_ALUNO == anoInt
                          && tb132.TB01_CURSO.CO_CUR == serie
                          && tb132.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == modalidade
                          && tb132.CO_TUR == turma
                          && tb132.CO_MAT == materia
                          select new { tb132.DT_FRE }).Distinct().OrderByDescending(w => w.DT_FRE).ToList();

            #endregion


            //Verifica as frequências para cada aluno
            #region Verifica as presenças de cada aluno

            foreach (AlunosMat lsFrAlu in lstA) // verifica cada aluno
            {
                int auxQtFreAlu = 3;
                foreach (var lsdtFr in reFreq) // para cada aluno verifica cada data de frequencia
                {
                    if (auxQtFreAlu >= 13)
                        break;

                    //Verifica se houve alguma falta para esse aluno anteriormente e conta
                    int qtFl = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                where tb132.TB07_ALUNO.CO_ALU == lsFrAlu.CO_ALU
                                && tb132.CO_ANO_REFER_FREQ_ALUNO == anoInt
                                && tb132.CO_TUR == turma
                                && EntityFunctions.TruncateTime(tb132.DT_FRE) == EntityFunctions.TruncateTime(lsdtFr.DT_FRE)
                                && tb132.CO_FLAG_FREQ_ALUNO == "N"
                                select new { tb132.ID_FREQ_ALUNO }).ToList().Count;

                    //Altera o cabeçalho Header da coluna para a data correspondente
                    grdBusca.Columns[auxQtFreAlu].HeaderText = lsdtFr.DT_FRE.ToString("dd/MM");

                    //Seta a quantidade de faltas dentro da coluna de acordo com a data
                    switch (auxQtFreAlu)
                    {
                        case 3:
                            lsFrAlu.DT1 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 4:
                            lsFrAlu.DT2 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 5:
                            lsFrAlu.DT3 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 6:
                            lsFrAlu.DT4 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 7:
                            lsFrAlu.DT5 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 8:
                            lsFrAlu.DT6 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 9:
                            lsFrAlu.DT7 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 10:
                            lsFrAlu.DT8 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 11:
                            lsFrAlu.DT9 = qtFl.ToString().PadLeft(2, '0');
                            break;
                        case 12:
                            lsFrAlu.DT10 = qtFl.ToString().PadLeft(2, '0');
                            break;
                    }
                    auxQtFreAlu++;
                }
            }

            #endregion

            var lstAlunoMatricula = lstA.ToList(); // GERA UM OBJETO DO TIO LISTA COM O RESULTADO DO SELECT
            grdBusca.DataKeyNames = new string[] { "CO_ALU" };
            grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
            grdBusca.DataBind();
        }

        #region Classes de Saída

        public class AlunosMat
        {
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

            public string DT1 { get; set; }
            public string DT2 { get; set; }
            public string DT3 { get; set; }
            public string DT4 { get; set; }
            public string DT5 { get; set; }
            public string DT6 { get; set; }
            public string DT7 { get; set; }
            public string DT8 { get; set; }
            public string DT9 { get; set; }
            public string DT10 { get; set; }

            //Altera a classe do Label que apresenta a quantidade de faltas, onde quando houverem faltas, o label ficará em vermelho
            public string CLASSE_DT1
            {
                get
                {
                    return (DT1 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT2
            {
                get
                {
                    return (DT2 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT3
            {
                get
                {
                    return (DT3 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT4
            {
                get
                {
                    return (DT4 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT5
            {
                get
                {
                    return (DT5 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT6
            {
                get
                {
                    return (DT6 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT7
            {
                get
                {
                    return (DT7 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT8
            {
                get
                {
                    return (DT8 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT9
            {
                get
                {
                    return (DT9 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
            public string CLASSE_DT10
            {
                get
                {
                    return (DT10 != "00" ? "lblFaltas" : "lblNormal");
                }
            }
        }

        private class temposSeries
        {
            public int nomeTempo { get; set; }
            public string inicioTempo { get; set; }
            public string terminoTempo { get; set; }
            public Boolean marcarTempo { get; set; }
        }

        #endregion

        /// <summary>
        /// Faz a verificacao em todos os postbacks para mostrar ou nao as informacoes pertinentes
        /// </summary>
        private void VerificaItensSelecionados()
        {
            //Muda o foco de acordo com as opcoes selecionadas e disponiveis
            if ((ddlModalidade.Items.Count >= 1) && (ddlModalidade.SelectedValue == ""))
                ddlModalidade.Focus();
            else if ((ddlSerieCurso.Items.Count >= 1) && (ddlSerieCurso.SelectedValue == ""))
                ddlSerieCurso.Focus();
            else if ((ddlTurma.Items.Count >= 1) && (ddlTurma.SelectedValue == ""))
                ddlTurma.Focus();
            else if (ddlReferencia.SelectedValue == "")
                ddlReferencia.Focus();
            else if (ddlDisciplina.SelectedValue == "")
                ddlDisciplina.Focus();

            //Se tiver apenas a opção "Selecione" na modalidade, então o professor não é associado à nenhuma turma,
            //então o ddl de professor recebe o foco para alteração
            if ((ddlModalidade.Items.Count == 1) && (ddlModalidade.SelectedValue == ""))
                txtData.Focus();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true, true);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);

            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false, false, false, true, true);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);

            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false, false, true, true);
            else
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);

            CarregaMaterias();
        }

        /// <summary>
        /// Carregas os tipoes de tempo
        /// </summary>
        private void CarregaTipoTempos()
        {
            ddlTempos.Items.Clear();
            ddlTempos.Items.Insert(0, new ListItem("Com Registro de Tempo", "S"));
            ddlTempos.Items.Insert(0, new ListItem("Sem Registro de Tempo", "N"));
            ddlTempos.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year.ToString() : "0");
            int anoInt = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            //if (LoginAuxili.FLA_PROFESSOR != "S")
            //    AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, ano, false);
            //else
            //    AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlDisciplina, LoginAuxili.CO_COL, modalidade, serie, anoInt, false);

            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string turmaUnica = "N";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;

                string strFreqTipoParam = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).CO_PARAM_FREQ_TIPO;

                if (strFreqTipoParam != "D")
                {
                    ddlDisciplina.Enabled = true;
                    //CarregaMaterias();
                }
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
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                where tb02.CO_MODU_CUR == modalidade
                                                && tb02.CO_CUR == serie
                                                && tb107.NO_SIGLA_MATERIA == "MSR"
                                                select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataBind();

                    //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                    //ddlDisciplina.SelectedValue = "0";
                }
                else
                {
                    // Valida se é professor, caso seja tras apenas as matérias associadas à ele
                    //var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                    //                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA into l1
                    //                            from lcd in l1.DefaultIfEmpty()
                    //                            join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb02.CO_MAT equals tbresp.CO_MAT into l2
                    //                            from ls in l2.DefaultIfEmpty()
                    //                            where tb02.CO_MODU_CUR == modalidade
                    //                            && tb02.CO_CUR == serie
                    //                            && (tbresp.CO_MAT != null ? tb107.NO_SIGLA_MATERIA == "MSR" : tbresp.CO_MAT == null)
                    //                            && tbresp.CO_ANO_REF == anoInt
                    //                            && tbresp.CO_COL_RESP == LoginAuxili.CO_COL
                    //                            select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);


                    //Verifica se o professor tem  associado à si alguma turma única
                    //var res = (from tbre in TB_RESPON_MATERIA.RetornaTodosRegistros()
                    //           where tbre.CO_MAT == null
                    //           &&   tbre.CO_COL_RESP == LoginAuxili.CO_COL
                    //           &&   tbre.CO_ANO_REF == anoInt
                    //           select new { tbre.CO_MAT }).ToList();

                    ////Resgata as informações da matéria criada pra turmas únicas, Atividades Letivas e alimenta a DropDownList.
                    //if (res.Count > 0)
                    //{
                    //    var resu = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                    //                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                    //                            where tb02.CO_MODU_CUR == modalidade
                    //                            && tb02.CO_CUR == serie
                    //                            && tb107.NO_SIGLA_MATERIA == "MSR"
                    //                            select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA).ToList();

                    //    ddlDisciplina.DataTextField = "NO_MATERIA";
                    //    ddlDisciplina.DataValueField = "CO_MAT";
                    //    ddlDisciplina.DataSource = resu;
                    //    ddlDisciplina.DataBind();
                    //}

                    //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                    //ddlDisciplina.SelectedValue = "0";

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

            }
            else
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    string anog = anoInt.ToString();
                    int coem = LoginAuxili.CO_EMP;
                    var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                               where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anog && tb43.CO_EMP == coem && tb43.FL_LANCA_FREQU == "S"
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).Distinct();

                    if (res != null)
                    {
                        ddlDisciplina.DataTextField = "NO_MATERIA";
                        ddlDisciplina.DataValueField = "CO_MAT";
                        ddlDisciplina.DataSource = res;
                        ddlDisciplina.DataBind();
                    }
                }
                else
                {
                    var resuR = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                 join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tbres.CO_MAT equals tb43.CO_MAT
                                 where tbres.CO_MODU_CUR == modalidade && tb43.FL_LANCA_FREQU == "S"
                                 && tb43.TB44_MODULO.CO_MODU_CUR == tbres.CO_MODU_CUR
                                 && tb43.CO_CUR == tbres.CO_CUR
                                 && tbres.CO_CUR == serie
                                 && tbres.CO_COL_RESP == LoginAuxili.CO_COL
                                 && tbres.CO_TUR == turma
                                 select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    if (resuR.Count() > 0)
                    {
                        ddlDisciplina.DataTextField = "NO_MATERIA";
                        ddlDisciplina.DataValueField = "CO_MAT";
                        ddlDisciplina.DataSource = resuR;
                        ddlDisciplina.DataBind();
                    }

                    //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));                    
                }
            }
        }

        /// <summary>
        /// Método que desabilita as grides
        /// </summary>
        private void LimpaGrides()
        {
            grdBusca.DataSource = null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Carrega a grid de tempos por turma
        /// </summary>
        private void CarregaGridTempos(int? codigo = null, string inicio = null, string termino = null)
        {
            grdTempos.Enabled = ddlTempos.SelectedValue == "S" ? true : false;
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
                        //grdAlunos.DataKeyNames = new string[] { "nomeTempo" };
                        grdTempos.DataSource = tempos;
                        grdTempos.DataBind();
                        liGridAluno.Style.Remove("margin-top");
                        liGridAluno.Style.Add("margin-top","-150px !important;");
                    }
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            //CarregaTurma();
            LimpaGrides();
            VerificaItensSelecionados();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            //CarregaMaterias();
            if (modalidade != 0 && serie != 0)
            {
                string strFreqTipoParam = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).CO_PARAM_FREQ_TIPO;

                if (strFreqTipoParam != "D")
                {
                    ddlDisciplina.Enabled = true;
                    //CarregaMaterias();
                }
                else
                {

                }
            }

            CarregaTurma();
            LimpaGrides();
            VerificaItensSelecionados();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            LimpaGrides();
            VerificaItensSelecionados();
        }

        protected void txtData_OnTextChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlReferencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaItensSelecionados();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaGrides();
        }

        //====> Faz a Pesquisa da gride de Atividade e dos Alunos
        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            PesquisaGridClick();
        }

        private void PesquisaGridClick()
        {
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            if ((modalidade == 0) || (turma == 0) || (serie == 0) || (txtData.Text == ""))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Os campos de Data de frequência, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            if (materia == 0 && ddlDisciplina.Enabled == true)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A disciplina deve ser informada.");
                return;
            }

            if (txtData.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de Frequência deve ser informada.");
                return;
            }

            DateTime DataIni;

            if (!DateTime.TryParse(txtData.Text, out DataIni))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de Frequência informa é inválida.");
                return;
            }

            if (DataIni > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data informada não pode ser superior a data atual.");
                return;
            }

            CarregaGrid();
        }

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField lblFlagPres = (HiddenField)e.Row.FindControl("hdCoFlagFreq");
                //DropDownList ddlFlagFreq = (DropDownList)e.Row.FindControl("ddlFreq");
                //if(lblFlagPres != null)
                //    ddlFlagFreq.SelectedValue = lblFlagPres.Value;

                //((DropDownList)e.Row.FindControl("ddlFreq")).SelectedValue = ((HiddenField)e.Row.FindControl("hdCoFlagFreq")).Value;
            }
        }

        protected void ddlTempos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "" && ((DropDownList)sender).SelectedValue != "0")
            {
                if (((DropDownList)sender).SelectedValue == "S")
                    CarregaGridTempos();
                else
                    grdTempos.Enabled = false;
            }
            else
                grdTempos.Enabled = false;
        }

        protected void ddlTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridTempos();
        }

        #endregion

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB132_FREQ_ALU</returns>
        private TB132_FREQ_ALU RetornaEntidade()
        {
            if (QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null)
                return TB132_FREQ_ALU.RetornaPelaChavePrimaria(Int32.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));
            else
                return TB132_FREQ_ALU.RetornaPelaChavePrimaria(0);
        }
    }
}