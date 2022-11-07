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
//           |                            |Criação da Pàgina com o objetivo de o professor lançar frequência
// 04/07/2014|Maxwell Almeida             |em sala de aula, já com ela homologada.

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3407_LancFreqTurmaSemPlanej
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
            //string strMsgGenerica = "blabla";
            //string strMsgObrigatoria = "blzblz";
            //CurrentPadraoCadastros.DefineMensagem(strMsgObrigatoria, strMsgGenerica);

            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString();
                CarregaTempo();
                CarregaModalidades();
                divGridAluno.Visible = false;
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (!grdBusca.Visible)
            {

            }
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
            DateTime data = DateTime.Parse(txtData.Text);
            int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            int coAtivProfTur = hdCoAtivProfTur.Value != "" ? int.Parse(hdCoAtivProfTur.Value) : 0;
            int coAlu = 0;
            string coFreqAluno;
            var quantidadeRegistros = "";
            if (data > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência não pode ser superior a data atual.");
                return;
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;

            #region Validações do bimestre
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlBimestre.SelectedValue == "B1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }
            #endregion

            quantidadeRegistros = grdBusca.Rows.Count.ToString();

            int i = 0;

                    #region Passa pelas linhas da grid de alunos
                    foreach (GridViewRow linha in grdBusca.Rows)
                    {
                        coAlu = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCoAluno")).Value);
                        coFreqAluno = ((DropDownList)linha.Cells[2].FindControl("ddlFreq")).SelectedValue;

                        TB132_FREQ_ALU tb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                                where lTb132.TB07_ALUNO.CO_ALU == coAlu
                                                && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                                && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade
                                                && lTb132.TB01_CURSO.CO_CUR == serie
                                                && lTb132.DT_FRE.Day == data.Day
                                                && lTb132.DT_FRE.Month == data.Month
                                                && lTb132.DT_FRE.Year == data.Year
                                                && lTb132.CO_TUR == turma
                                                && (materia != null ? lTb132.CO_MAT == materia : 0 == 0)
                                                //&& lTb132.NR_TEMPO == cotempAtiv
                                                && lTb132.CO_ATIV_PROF_TUR == coAtivProfTur
                                                && lTb132.FL_HOMOL_FREQU == "S"
                                                select lTb132).FirstOrDefault();

                        if (tb132 == null)
                        {
                            tb132 = new TB132_FREQ_ALU();

                            tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                            tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                            tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                            tb132.CO_TUR = turma;
                            tb132.CO_MAT = materia;
                            tb132.DT_FRE = data;
                            tb132.CO_COL = LoginAuxili.CO_COL;
                            tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;

                            tb132.CO_ATIV_PROF_TUR = coAtivProfTur;

                            tb132.CO_ANO_REFER_FREQ_ALUNO = data.Year;
                            tb132.CO_BIMESTRE = ddlBimestre.SelectedValue;
                            //tb132.NR_TEMPO = cotempAtiv;
                            tb132.FL_HOMOL_FREQU = "N";
                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                        }
                        else
                        {
                            if (tb132.CO_FLAG_FREQ_ALUNO != coFreqAluno)
                            {
                                tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                                tb132.FL_HOMOL_FREQU = "N";
                                TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                            }
                        }

                        if (tb132.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb132) < 1)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                            return;
                        }
                        //else
                        //{
                        //    if (chkAtualizaHist.Checked)
                        //    {
                        //        tb132.TB01_CURSOReference.Load();
                        //        tb132.TB07_ALUNOReference.Load();
                        //        AuxiliGeral.AtualizaHistFreqAlu(this, tb132.TB01_CURSO.CO_EMP, tb132.CO_ANO_REFER_FREQ_ALUNO, tb132.TB01_CURSO.CO_MODU_CUR, tb132.TB01_CURSO.CO_CUR, tb132.CO_TUR, ddlBimestre.SelectedValue);
                        //    }
                        //}
                    }
                    #endregion

                    i++;

            if (i == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma atividade selecionada.");
                return;
            }
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
            int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            string bimestre = ddlBimestre.SelectedValue;
            DateTime data = DateTime.Parse(txtData.Text);
            string anoMesMat = data.Year.ToString();

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGridAluno.Visible = true;

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
                            "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + " and f.CO_MAT = " + materia + " and f.NR_TEMPO = " + tempoAula + " and f.CO_BIMESTRE = '" + bimestre + "') " +
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
                            "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + " and f.NR_TEMPO = " + tempoAula + " and f.CO_BIMESTRE = '" + bimestre + "') " +
                        "inner join TB07_ALUNO a " +
                            "on (a.CO_ALU = m.CO_ALU) " +
                        "where m.CO_EMP = " + LoginAuxili.CO_EMP +
                        " and m.CO_ANO_MES_MAT = " + anoMesMat.Trim() +
                        " and m.CO_CUR = " + serie +
                        " and m.CO_TUR = " + turma +
                        " and m.CO_SIT_MAT = 'A'" +
                        "order by a.NO_ALU";
            }

            var lstA = GestorEntities.CurrentContext.ExecuteStoreQuery<AlunosMat>(sql); // EXECUTA O SELECT GERANDO UMA LISTA DA CLASSE ALUNOSMAT

            var lstAlunoMatricula = lstA.ToList(); // GERA UM OBJETO DO TIO LISTA COM O RESULTADO DO SELECT

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
            grdBusca.DataBind();
        }

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

        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

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

            if (txtData.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "As datas devem ser informadas!");
                return;
            }

            string anoGrade = DateTime.Parse(txtData.Text).Year.ToString();

            //string anoGrade = DateTime.Parse(txtData.Text).Year.ToString();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

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
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }
        private void CarregaTempo()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            var tb06 = (from lTb06 in TB06_TURMAS.RetornaTodosRegistros()
                        where lTb06.CO_EMP == LoginAuxili.CO_EMP
                        && lTb06.CO_MODU_CUR == modalidade
                        && lTb06.CO_CUR == serie
                        && lTb06.CO_TUR == turma
                        select new { lTb06.CO_PERI_TUR }).FirstOrDefault();

            string strTurno = tb06 != null ? tb06.CO_PERI_TUR : "";

            var resultado = (from tb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                             where tb131.CO_EMP == LoginAuxili.CO_EMP
                             && tb131.CO_MODU_CUR == modalidade
                             && tb131.CO_CUR == serie
                             && tb131.TP_TURNO == strTurno
                             select new AuxiliFormatoExibicao.listaTempos
                             {
                                 nrTempo = tb131.NR_TEMPO
                                 ,
                                 turnoTempo = tb131.TP_TURNO
                                 ,
                                 hrInicio = tb131.HR_INICIO
                                 ,
                                 hrFim = tb131.HR_TERMI
                             });

            ddlTempo.Items.Clear();
            ddlTempo.DataSource = resultado;
            ddlTempo.DataTextField = "tempoCompleto";
            ddlTempo.DataValueField = "nrTempo";
            ddlTempo.DataBind();

            ddlTempo.Items.Insert(0, new ListItem("Todos", "0"));
            ddlTempo.Items.Insert(1, new ListItem("Não definido", ""));

        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                        select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

            ddlDisciplina.DataTextField = "NO_MATERIA";
            ddlDisciplina.DataValueField = "CO_MAT";
            ddlDisciplina.DataBind();

            if (ddlDisciplina.Items.Count <= 0)
                ddlDisciplina.Items.Clear();

            ddlDisciplina.Items.Insert(0, new ListItem("Nenhuma", ""));
        }

        /// <summary>
        /// Método que desabilita as grides
        /// </summary>
        private void LimpaGrides()
        {
            grdBusca.DataSource = null;
            grdBusca.DataBind();
        }
        #endregion

        protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaGrides();
        }
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            LimpaGrides();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (modalidade != 0 && serie != 0)
            {
                string strFreqTipoParam = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).CO_PARAM_FREQ_TIPO;

                if (strFreqTipoParam != "D")
                {
                    ddlDisciplina.Enabled = true;
                    CarregaMaterias();
                }
                else
                {
                    ddlDisciplina.Enabled = false;
                    ddlDisciplina.Items.Clear();
                    ddlDisciplina.Items.Insert(0, new ListItem("", ""));
                    ddlDisciplina.SelectedValue = "";
                }
            }

            CarregaTurma();
            LimpaGrides();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTempo();
            LimpaGrides();
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

            if (modalidade == 0 || turma == 0 || serie == 0 || txtData.Text == "")
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
                HiddenField lblFlagPres = (HiddenField)e.Row.FindControl("hdCoFlagFreq");
                DropDownList ddlFlagFreq = (DropDownList)e.Row.FindControl("ddlFreq");
                ddlFlagFreq.SelectedValue = lblFlagPres.Value;

                ((DropDownList)e.Row.FindControl("ddlFreq")).SelectedValue = ((HiddenField)e.Row.FindControl("hdCoFlagFreq")).Value;
            }
        }

        #region Funções de Campo

        #endregion
    }
}