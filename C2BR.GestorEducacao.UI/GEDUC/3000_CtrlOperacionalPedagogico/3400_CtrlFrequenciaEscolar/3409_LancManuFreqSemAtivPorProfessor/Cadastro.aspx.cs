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
//           |                            |
// 28/10/2014| Maxwell Almeida            | Criação da funcionalidade com base no lançamento convencional, com a diferença de que é por professor

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3409_LancManuFreqSemAtivPorProfessor
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
                bool red = CarregaParametros();
                if (red == false)
                {
                    txtData.Text = DateTime.Now.ToString();
                    CarregaModalidades();
                    CarregaTempo();
                    carregaBusca();
                    CarregaMedidas();
                    CarregaColaboradores();
                    VerificaLancFreqUnidade();
                }
            }
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
                    var professor = par[6];

                    txtData.Text = data;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    CarregaTempo();
                    ddlTempo.SelectedValue = coTempo;

                    CarregaColaboradores();
                    ddlColaborador.SelectedValue = professor;

                    if (ddlBimestre.Items.FindByValue(coBime) != null)
                        ddlBimestre.SelectedValue = coBime;

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
                    var professor = par[4];
                    var ano = par[5];
                    var coBime = par[6];
                    var coAtivProfTur = par[7];
                    var tempo = par[8];

                    txtData.Text = dtRealizacao;
                    txtData.Enabled = false;
                    ddlTempo.Enabled = false;

                    var tipo = "B";
                    AuxiliCarregamentos.CarregaMedidasTemporais(ddlBimestre, false, tipo, false, true);



                    if (ddlBimestre.Items.FindByValue(coBime) != null)
                        ddlBimestre.SelectedValue = coBime;
                    ddlBimestre.Enabled = false;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;
                    ddlModalidade.Enabled = false;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;
                    ddlSerieCurso.Enabled = false;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;
                    ddlTurma.Enabled = false;

                    CarregaColaboradores();
                    ddlColaborador.SelectedValue = professor;

                    CarregaTempo();
                    ddlTempo.SelectedValue = tempo;

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
            if (!grdBusca.Visible)
            {

            }
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
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

            DateTime dataLacto =  DateTime.Now.Date;

            if (ddlBimestre.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione o Bimestre para o qual a frequência será lançada");
            }

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
                else if (ddlBimestre.SelectedValue == "B4")
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
                else if (ddlBimestre.SelectedValue == "T1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }
            #endregion

            quantidadeRegistros = grdBusca.Rows.Count.ToString();

            int i = 0;
            foreach (GridViewRow al in grdAtividades.Rows)
            {
                if (((CheckBox)al.Cells[0].FindControl("ckSelect")).Checked)
                {
                    coAtivProfTur = int.Parse(((HiddenField)al.Cells[0].FindControl("hidCoAtiv")).Value);
                    int cotempAtiv = int.Parse(((HiddenField)al.Cells[0].FindControl("hidCoTemp")).Value);
                    int materia = int.Parse(((HiddenField)al.Cells[0].FindControl("hidCoMat")).Value);

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
                                                && lTb132.NR_TEMPO == cotempAtiv
                                                && lTb132.CO_ATIV_PROF_TUR == coAtivProfTur
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
                            tb132.NR_TEMPO = cotempAtiv;
                            tb132.FL_HOMOL_FREQU = (chkLancFreqHomol.Checked ? "S" : "N");
                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                        }
                        else
                        {
                            if (tb132.CO_FLAG_FREQ_ALUNO != coFreqAluno)
                            {
                                tb132.CO_FLAG_FREQ_ALUNO = coFreqAluno;
                                tb132.FL_HOMOL_FREQU = (chkLancFreqHomol.Checked ? "S" : "N");
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
                }
            }

            if (i == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma atividade selecionada.");
                return;
            }

            //Trata a mensagem de Sucesso, para o caso de o Funcionário estar lançando as Frequências para mais de um Tempo.
            if (chkRepeteFreq.Checked)
            {
                //Verifica quantoas linhas estão selecionadas 
                int auxQtTp = 0;

                foreach (GridViewRow lin in grdAtividades.Rows)
                {
                    CheckBox chkau = ((CheckBox)lin.Cells[0].FindControl("ckSelect"));
                    if (chkau.Checked)
                        auxQtTp++;
                }

                var parametros = txtData.Text + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                    + ddlTempo.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlColaborador.SelectedValue;
                HttpContext.Current.Session["BuscaSuperior"] = parametros;

                //Gera a mensagem de sucesso e limpa as variáveis de sessão, para quando a página for recarregada ela venha com os campos habilitados e sem nada selecionado.
                string msgSucess = quantidadeRegistros + " Frequências registradas com sucesso para os " + auxQtTp + " tempos selecionados!";
                AuxiliPagina.RedirecionaParaPaginaSucesso(msgSucess, HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
            else
            {
                string hdTp = "";
                foreach (GridViewRow li in grdAtividades.Rows)
                {
                    CheckBox chkau = ((CheckBox)li.Cells[0].FindControl("ckSelect"));
                    if (chkau.Checked)
                        hdTp = (((HiddenField)li.Cells[0].FindControl("hidCoTemp")).Value);
                }

                //Verifica se há apenas uma linha, se tiver, o processo morre neste ponto, é gerada a mensagem de sucesso padrão e as variáveis em sessão são descartadas,
                //deixando a funcionalidade pronta para receber um novo registro.
                if (grdAtividades.Rows.Count == 1)
                {

                    var parametros = txtData.Text + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                        + ddlTempo.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlColaborador.SelectedValue;
                    HttpContext.Current.Session["BuscaSuperior"] = parametros;

                    //Mensagem de Sucesso Padrão.
                    AuxiliPagina.RedirecionaParaPaginaSucesso(quantidadeRegistros + " Frequências registradas com sucesso para o " + hdTp + "ºTempo !", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
                }
                else
                {
                    //Passa de linha em linha da Grid de Ativdades e verifica se já existe lançamento de nota para a atividade em questão, se já existir, é desabilitado
                    //a linha em questão, assim o usuário sabe para qual atividade já foi lançada frequência.
                    foreach (GridViewRow liativ in grdAtividades.Rows)
                    {
                        CheckBox chkAtivEna = ((CheckBox)liativ.Cells[0].FindControl("ckSelect"));
                        int HDCoAtiv = int.Parse((((HiddenField)liativ.Cells[0].FindControl("hidCoAtiv")).Value));

                        bool resultFreq = TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == HDCoAtiv).Any();

                        if (resultFreq == true)
                        {
                            chkAtivEna.Enabled = false;
                            chkAtivEna.Checked = false;
                        }
                    }

                    //Verifica se é a última atividade à ser lançada frequência, caso já tenha lançado frequência para todas as atividades, o processo morre na 
                    //tela de sucesso padrão, e na limpeza das variáveis de sessão.
                    bool auxCountRowsAtiv = false;

                    foreach (GridViewRow lia in grdAtividades.Rows)
                    {
                        if (auxCountRowsAtiv == false)
                        {
                            CheckBox chkAtivEna = ((CheckBox)lia.Cells[0].FindControl("ckSelect"));

                            if (chkAtivEna.Enabled == false)
                                auxCountRowsAtiv = false;
                            else
                                auxCountRowsAtiv = true;
                        }
                    }

                    var parametros = txtData.Text + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                      + ddlTempo.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlColaborador.SelectedValue;
                    HttpContext.Current.Session["BuscaSuperior"] = parametros;

                    if (auxCountRowsAtiv == false)
                        AuxiliPagina.RedirecionaParaPaginaSucesso(quantidadeRegistros + " Frequências registradas com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
                    else
                        AuxiliPagina.EnvioAvisoGeralSistema(this, quantidadeRegistros + " Frequências registradas com sucesso para o " + hdTp + "ºTempo !");
                }
            }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Verifica se no cadastro da unidade logada, a flag de lançamento de frequência está marcada, caso esteja marca o checkbox correspondente
        /// </summary>
        private void VerificaLancFreqUnidade()
        {
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            res.TB83_PARAMETROReference.Load();
            chkLancFreqHomol.Checked = (res.TB83_PARAMETRO.FL_LANCA_FREQ_ALUNO_HOMOL == "S" ? true : false);
        }

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid(int coAtiv = 0)
        {
            //int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
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

            //divGridAluno.Visible = true;

            // Instancia o contexto
            var ctx = GestorEntities.CurrentContext;

            string sql = "";
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

            //var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
            //           join tb132 in TB132_FREQ_ALU.RetornaTodosRegistros() on tb08.CO_ALU equals tb132.TB07_ALUNO.CO_ALU into l1
            //           from ls in l1.DefaultIfEmpty()
            //           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
            //           join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb43.CO_CUR
            //           where ls.CO_ATIV_PROF_TUR == coAtiv
            //           && ls.DT_FRE.Year == data.Year && ls.DT_FRE.Month == data.Month && ls.DT_FRE.Day == data.Day
            //           && ls.NR_TEMPO == tempoAula
            //           && ls.CO_BIMESTRE == bimestre
            //           && tb08.CO_EMP == LoginAuxili.CO_EMP
            //           && tb08.CO_ANO_MES_MAT == anoMesMat.Trim()
            //           && tb08.CO_CUR == serie
            //           && tb08.CO_TUR == turma
            //           && tb08.CO_SIT_MAT == "A"

            //           && tb43.TB44_MODULO.CO_MODU_CUR == tb08.
            //           select new AlunosMat
            //           {
            //               CO_ALU = tb07.CO_ALU,
            //               NO_ALU = tb07.NO_ALU,
            //               CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
            //               NU_NIRE = tb07.NU_NIRE,
            //               CO_SIT_MAT = tb08.CO_SIT_MAT,
            //               CO_FLAG_FREQ_ALUNO = ls.CO_FLAG_FREQ_ALUNO,
            //               DT_FRE = ls.DT_FRE,
            //               FL_HOMOL_FREQU = ls.FL_HOMOL_FREQU,
            //               ID_FREQU_ALUNO = ls.ID_FREQ_ALUNO,
            //           }).Distinct().OrderBy(w => w.NO_ALU).ToList();

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
            int ano = (txtData.Text != "" ? DateTime.Parse(txtData.Text).Year : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
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
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
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
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);

            CarregaColaboradores();
        }

        /// <summary>
        /// Carrega os Tempos
        /// </summary>
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

        //====> Método que carrega o DropDown de Medidas
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlBimestre, false, tipo, false, true);
        }

        /// <summary>
        /// Carrega os Colaboradores de acordo com os parâmetros
        /// </summary>
        private void CarregaColaboradores()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int ano = (!string.IsNullOrEmpty(txtData.Text) ? int.Parse(txtData.Text.Substring(6, 4)) : DateTime.Now.Year);
            AuxiliCarregamentos.CarregaProfessoresRespMateria(ddlColaborador, LoginAuxili.CO_EMP, modalidade, serie, turma, 0, ano);
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
            //CarregaTurma();
            LimpaGrides();
            CarregaColaboradores();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            CarregaTurma();
            LimpaGrides();
            CarregaColaboradores();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
            CarregaTempo();
            LimpaGrides();
        }

        protected void txtData_OnTextChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void grdAtividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        //-----------------------------------------------------------------------
        // Esta função é referente ao campo de ano (ddlAno) que foi retirado.
        //-----------------------------------------------------------------------
        //protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CarregaModalidades();
        //    CarregaSerieCurso();
        //    CarregaTurma();
        //    grdBusca.DataSource = null;
        //    grdBusca.DataBind();
        //}

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
            int colabor = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;

            if ((modalidade == 0) || (turma == 0) || (serie == 0) || (txtData.Text == ""))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Os campos de Data de frequência, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            if (colabor == 0)
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

            CarregaGridAtividade();
            CarregaGrid();
        }



        /// <summary>
        /// Método que carrega a grid de Data
        /// </summary>
        private void CarregaGridAtividade()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int nrTem = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            int coAti = hdCoAtivProfTur.Value != "" ? int.Parse(hdCoAtivProfTur.Value) : 0;
            DateTime dtAtv = DateTime.Parse(txtData.Text);
            int ano = (!string.IsNullOrEmpty(txtData.Text) ? int.Parse(txtData.Text.Substring(6, 4)) : DateTime.Now.Year);
            int colab = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;

            //Coleta qual o tipo de frequência do curso selecionado
            string tpFreq = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coMod, coCur).CO_PARAM_FREQ_TIPO;

            //Verifica Quantos tempos o Usuário selecionou na funcionalidade anterior, para que dessa maneira o select possa pesquisar Todos da Página anterior.
            int? qtTemposAux = (Session["auxQtTempos"] != null ? (int)HttpContext.Current.Session["auxQtTempos"] - 1 : (int?)null);
            int auxTempo2 = (qtTemposAux.HasValue ? coAti - qtTemposAux.Value : 0);

            //Valida se a frequência é com atividade ou não
            if (tpFreq == "M")
            {
                #region Pesquisa

                var retorno = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                               join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb119.CO_COL_ATIV equals tbresp.CO_COL_RESP
                               join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tbresp.CO_MAT equals tb43.CO_MAT
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb119.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               where tb119.CO_EMP == coEmp
                               && tb43.FL_LANCA_FREQU == "S"
                               && tb43.TB44_MODULO.CO_MODU_CUR == tbresp.CO_MODU_CUR
                               && tb43.CO_CUR == tbresp.CO_CUR
                               && tb119.CO_MODU_CUR == coMod
                               && tb119.CO_CUR == coCur
                               && tb119.CO_TUR == coTur
                               && tb119.DT_ATIV_REAL == dtAtv
                               && tbresp.CO_MODU_CUR == tb119.CO_MODU_CUR
                               && tbresp.CO_CUR == tb119.CO_CUR
                                   //&& tbresp.CO_MAT == tb119.CO_MAT
                               && tbresp.CO_ANO_REF == ano
                               && tbresp.CO_COL_RESP == colab
                                   //Filtra a atividade apenas se tiver vido o código
                               && (coAti != 0 ? (qtTemposAux != null ? tb119.CO_ATIV_PROF_TUR >= auxTempo2 && tb119.CO_ATIV_PROF_TUR <= coAti : tb119.CO_ATIV_PROF_TUR == coAti) : 0 == 0)

                               select new atividade
                               {
                                   deTema = tb119.DE_TEMA_AULA,
                                   coAtiv = tb119.CO_ATIV_PROF_TUR,
                                   chkSel = false,
                                   coTempo = tb119.CO_TEMPO_ATIV,
                                   CO_MAT = tb119.CO_MAT,
                                   NO_MATERIA = tb107.NO_MATERIA,
                                   CO_ATIV_PROF_TUR = tb119.CO_ATIV_PROF_TUR,
                               }).DistinctBy(w => w.CO_ATIV_PROF_TUR).ToList();

                grdAtividades.DataSource = retorno;
                grdAtividades.DataBind();

                #endregion
            }
            else
            {
                #region Pesquisa as atividades com lançamento sem matéria

                var retorno = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                               join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb119.CO_COL_ATIV equals tbresp.CO_COL_RESP
                               join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tbresp.CO_MAT equals tb43.CO_MAT
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb119.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               where tb119.CO_EMP == coEmp
                               && tb43.FL_LANCA_FREQU == "S"
                               && tb43.TB44_MODULO.CO_MODU_CUR == tbresp.CO_MODU_CUR
                               && tb43.CO_CUR == tbresp.CO_CUR
                               && tb119.CO_MODU_CUR == coMod
                               && tb119.CO_CUR == coCur
                               && tb119.CO_TUR == coTur
                               && tb119.DT_ATIV_REAL == dtAtv
                               && tbresp.CO_MODU_CUR == tb119.CO_MODU_CUR
                               && tbresp.CO_CUR == tb119.CO_CUR
                                   //&& tbresp.CO_MAT == tb119.CO_MAT
                               && tbresp.CO_ANO_REF == ano
                               && tbresp.CO_COL_RESP == colab
                                   //Filtra a atividade apenas se tiver vido o código
                               && (coAti != 0 ? (qtTemposAux != null ? tb119.CO_ATIV_PROF_TUR >= auxTempo2 && tb119.CO_ATIV_PROF_TUR <= coAti : tb119.CO_ATIV_PROF_TUR == coAti) : 0 == 0)
                               select new atividade
                               {
                                   deTema = tb119.DE_TEMA_AULA,
                                   coAtiv = tb119.CO_ATIV_PROF_TUR,
                                   chkSel = false,
                                   coTempo = tb119.CO_TEMPO_ATIV,
                                   CO_MAT = tb119.CO_MAT,
                                   NO_MATERIA = tb107.NO_MATERIA,
                                   CO_ATIV_PROF_TUR = tb119.CO_ATIV_PROF_TUR,
                               }).DistinctBy(w => w.CO_ATIV_PROF_TUR).ToList();

                grdAtividades.DataSource = retorno;
                grdAtividades.DataBind();

                #endregion
            }
            HttpContext.Current.Session.Remove("auxQtTempos");
        }

        public class atividade
        {
            public string deTema { get; set; }
            public int coAtiv { get; set; }
            public bool chkSel { get; set; }
            public int coTempo { get; set; }
            public string tempo
            {
                get
                {
                    if (coTempo != 0)
                    {
                        return coTempo.ToString() + "° tempo";
                    }
                    else
                    {
                        return "Sem Registro";
                    }
                }
            }
            public int CO_MAT { get; set; }
            public string NO_MATERIA { get; set; }
            public int CO_ATIV_PROF_TUR { get; set; }
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

        // Desmarca todos os checkbox que não forem clicados
        protected void ckSelect_CheckedChange(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;
            int coAtiv = 0;

            // Valida se a grid de atividades possui algum registro
            if (grdAtividades.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coAtiv = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAtiv")).Value);
                            chkRepeteFreq.Checked = false;
                        }
                    }
                }
            }

            CarregaGrid(coAtiv);
        }

        #region Funções de Campo

        //É executado quando se clica no check "Repetir Frequência", tem a finalidade de marcar todos os checkes da Grid de Atividades.
        protected void chkRepeteFreq_OnCheckedChanged(object sende, EventArgs e)
        {
            if (chkRepeteFreq.Checked)
            {
                foreach (GridViewRow lines in grdAtividades.Rows)
                {
                    CheckBox chkli = ((CheckBox)lines.Cells[0].FindControl("ckSelect"));

                    if (chkli.Enabled == true)
                        chkli.Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow lines in grdAtividades.Rows)
                {
                    CheckBox chkli = ((CheckBox)lines.Cells[0].FindControl("ckSelect"));
                    chkli.Checked = false;
                }
            }
        }
        #endregion
    }
}