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
// 26/02/2013|Victor Martins Machado      |ano da frequência foi vem da data
//           |                            |(txtData).
// ----------+----------------------------+-------------------------------------
//           |                            |Foi tirado a data e colocado um intervalo de data
// 26/03/2013|Caio Mendonça               |Foi criado uma segunda grid de data
//           |                            |
// ----------+----------------------------+-------------------------------------
//           |                            |Corrigido o carregamento da gride de alunos
// 04/04/2013|André Nobre Vinagre         |
//           |                            |
// ----------+----------------------------+-------------------------------------
// 25/04/2013| André Nobre Vinagre        |Corrigida a questão da frequencia para pegar apenas
//           |                            |por uma data
//           |                            |
// ----------+----------------------------+-------------------------------------
// 16/07/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 09/06/2014| Victor Martins Machado     | Corrigido o problema da homologação da frequencia no ato do lançamento
//           |                            |

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3408_ReplicFreqAtivd
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
                    CarregaSerieCurso();
                    CarregaTurma();
                    CarregaTempo();
                    carregaBusca();
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
                    var disciplina = par[6];

                    txtData.Text = data;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    CarregaTempo();
                    ddlTempo.SelectedValue = coTempo;

                    ddlBimestre.SelectedValue = coBime;

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
                    ddlTempo.Enabled = false;

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

                    int coMod = (modalidade != "" ? int.Parse(modalidade) : 0);
                    int coCur = (serieCurso != "" ? int.Parse(serieCurso) : 0);

                    //Coleta qual o tipo de frequência do curso selecionado
                    string tpFreq = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coMod, coCur).CO_PARAM_FREQ_TIPO;

                    //if (tpFreq == "M")
                    //{
                    CarregaMaterias();
                    ddlDisciplina.SelectedValue = disciplina;
                    ddlDisciplina.Enabled = false;
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
            foreach (GridViewRow al in grdAtividades.Rows)
            {
                if (((CheckBox)al.Cells[0].FindControl("ckSelect")).Checked == false)
                {
                    coAtivProfTur = int.Parse(((HiddenField)al.Cells[0].FindControl("hidCoAtiv")).Value);
                    int cotempAtiv = int.Parse(((HiddenField)al.Cells[0].FindControl("hidCoTemp")).Value);

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
                    + ddlTempo.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlDisciplina.SelectedValue;
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
                        + ddlTempo.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlDisciplina.SelectedValue;
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
                      + ddlTempo.SelectedValue + ";" + ddlBimestre.SelectedValue + ";" + ddlDisciplina.SelectedValue;
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
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid(int coAtiv = 0, int tempoAula = 0)
        {
            int? materia = ddlDisciplina.SelectedValue != "" ? (int?)int.Parse(ddlDisciplina.SelectedValue) : null;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
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
            if (materia != null) // VALIDA SE FOI SELECIONADA UMA MATÉRIA
            {
                //var query = (from tb08 in ctx.TB08_MATRCUR
                //             join tb07 in ctx.TB07_ALUNO on tb08.CO_ALU equals tb07.CO_ALU
                //             where tb08.CO_EMP == LoginAuxili.CO_EMP
                //             && tb08.CO_ANO_MES_MAT == anoMesMat.Trim()
                //             && tb08.CO_CUR == serie
                //             && tb08.CO_TUR == turma
                //             && tb08.CO_SIT_MAT == "A"
                //             select new AlunosMat
                //             {
                //                 CO_ALU = tb07.CO_ALU,
                //                 NO_ALU = tb07.NO_ALU,
                //                 CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                //                 NU_NIRE = tb07.NU_NIRE,
                //                 CO_SIT_MAT = tb08.CO_SIT_MAT
                //             }).ToList();

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
                            "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + " and f.CO_MAT = " + materia + (tempoAula != 0 ? " and f.NR_TEMPO = " + tempoAula : "") + " and f.CO_BIMESTRE = '" + bimestre + "') " +
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
                            "on (m.CO_ALU = f.CO_ALU and CO_ATIV_PROF_TUR = " + coAtiv + " and YEAR(f.DT_FRE) = " + data.Year + " and MONTH(f.DT_FRE) = " + data.Month + " and DAY(f.DT_FRE) = " + data.Day + (tempoAula != 0 ? " and f.NR_TEMPO = " + tempoAula : "") + " and f.CO_BIMESTRE = '" + bimestre + "') " +
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

            //var lstAlunoMatricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
            //                     join tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
            //                        on tb08.TB07_ALUNO.CO_ALU equals tb132.TB07_ALUNO.CO_ALU
            //                     where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
            //                     && tb08.CO_ANO_MES_MAT == anoMesMat
            //                     && tb08.CO_CUR == serie
            //                     && tb08.CO_TUR == turma
            //                     && tb132.DT_FRE.Year == data.Year && tb132.DT_FRE.Month == data.Month && tb132.DT_FRE.Day == data.Day
            //                     && (materia != null ? tb132.CO_MAT == materia : materia == null)
            //                     && (tempoAula != 0 ? tb132.NR_TEMPO == tempoAula : tempoAula == 0)
            //                     && tb132.CO_BIMESTRE == bimestre
            //                     select new AlunosMat
            //                     {
            //                         CO_ALU = tb08.CO_ALU,
            //                         NO_ALU = tb08.TB07_ALUNO.NO_ALU,
            //                         CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
            //                         NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
            //                         CO_SIT_MAT = tb08.CO_SIT_MAT,
            //                         CO_FLAG_FREQ_ALUNO = tb132.CO_FLAG_FREQ_ALUNO,
            //                         DT_FRE = tb132.DT_FRE
            //                     }).ToList().OrderBy(m => m.NO_ALU);

            //if (lstAlunoMatricula.Count() == 0)
            //{
            //    lstAlunoMatricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
            //                         where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
            //                         && tb08.CO_ANO_MES_MAT == anoMesMat
            //                         && tb08.CO_CUR == serie
            //                         && tb08.CO_TUR == turma
            //                         && tb08.CO_SIT_MAT == "A"

            //                         select new AlunosMat
            //                         {
            //                             CO_ALU = tb08.CO_ALU,
            //                             NO_ALU = tb08.TB07_ALUNO.NO_ALU,
            //                             CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
            //                             NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
            //                             CO_SIT_MAT = tb08.CO_SIT_MAT,
            //                             CO_FLAG_FREQ_ALUNO = "S",
            //                             DT_FRE = data
            //                         }).ToList().OrderBy(m => m.NO_ALU);
            //}

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
            grdBusca.DataBind();

            carregaFreqAluno();
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
                    //Valida se é professor, caso seja tras apenas as matérias associadas à ele
                    ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                join tbresp in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb02.CO_MAT equals tbresp.CO_MAT
                                                where tb02.CO_MODU_CUR == modalidade
                                                && tb02.CO_CUR == serie
                                                && tb107.NO_SIGLA_MATERIA == "MSR"
                                                && tbresp.CO_ANO_REF == anoInt
                                                select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataBind();

                    //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                    //ddlDisciplina.SelectedValue = "0";
                }

            }
            else
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    string anog = anoInt.ToString();
                    int coem = LoginAuxili.CO_EMP;
                    var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                               where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anog && tb43.CO_EMP == coem
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
                    ddlDisciplina.DataSource = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                                join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                where tbres.CO_MODU_CUR == modalidade
                                                && tbres.CO_CUR == serie
                                                && tbres.CO_COL_RESP == LoginAuxili.CO_COL
                                                && tbres.CO_TUR == turma
                                                select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                    ddlDisciplina.DataTextField = "NO_MATERIA";
                    ddlDisciplina.DataValueField = "CO_MAT";
                    ddlDisciplina.DataBind();

                    ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                    ddlDisciplina.SelectedValue = "0";
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
                    //ddlDisciplina.Enabled = false;
                    //ddlDisciplina.Items.Clear();
                    //ddlDisciplina.Items.Insert(0, new ListItem("", ""));
                    //ddlDisciplina.SelectedValue = "";
                }
            }

            CarregaTurma();
            LimpaGrides();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
            CarregaTempo();
            LimpaGrides();
        }

        protected void txtData_OnTextChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaGrides();
        }
        //protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LimpaGrides();
        //}


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
            int coMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coAti = hdCoAtivProfTur.Value != "" ? int.Parse(hdCoAtivProfTur.Value) : 0;
            int nrTem = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            DateTime dtAtv = DateTime.Parse(txtData.Text);

            //Coleta qual o tipo de frequência do curso selecionado
            string tpFreq = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coMod, coCur).CO_PARAM_FREQ_TIPO;

            //Valida se a frequência é com atividade ou não
            if (coAti == 0)
            {
                if (tpFreq == "M")
                {

                    #region Pesquisa as atividades com matérias

                    var retorno = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                   where tb119.CO_EMP == coEmp
                                   && tb119.CO_MODU_CUR == coMod
                                   && tb119.CO_CUR == coCur
                                   && tb119.CO_TUR == coTur
                                   && tb119.CO_MAT == coMat
                                   && tb119.DT_ATIV_REAL == dtAtv
                                   //&& tb119.CO_TEMPO_ATIV == 
                                   select new atividade
                                   {
                                       deTema = tb119.DE_TEMA_AULA,
                                       coAtiv = tb119.CO_ATIV_PROF_TUR,
                                       chkSel = false,
                                       coTempo = tb119.CO_TEMPO_ATIV,
                                       nomeTempo = tb119.CO_TEMPO_ATIV,
                                   }).ToList();

                    grdAtividades.DataSource = retorno;
                    grdAtividades.DataBind();

                    #endregion
                }
                else
                {
                    #region Pesquisa as atividades com lançamento sem matéria

                    var retorno = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                   where tb119.CO_EMP == coEmp
                                   && tb119.CO_MODU_CUR == coMod
                                   && tb119.CO_CUR == coCur
                                   && tb119.CO_TUR == coTur
                                   && tb119.DT_ATIV_REAL == dtAtv
                                   select new atividade
                                   {
                                       deTema = tb119.DE_TEMA_AULA,
                                       coAtiv = tb119.CO_ATIV_PROF_TUR,
                                       chkSel = false,
                                       coTempo = tb119.CO_TEMPO_ATIV,
                                   }).ToList();

                    grdAtividades.DataSource = retorno;
                    grdAtividades.DataBind();

                    #endregion
                }
            }
            else
            {
                if (tpFreq == "M")
                {
                    #region Pesquisa as atividades com matérias

                    //Verifica Quantos tempos o Usuário selecionou na funcionalidade anterior, para que dessa maneira o select possa pesquisar Todos da Página anterior.
                    int? qtTemposAux = (Session["auxQtTempos"] != null ? (int)HttpContext.Current.Session["auxQtTempos"] - 1 : (int?)null);
                    int auxTempo2 = (qtTemposAux.HasValue ? coAti - qtTemposAux.Value : 0);

                    var retorno = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                   where tb119.CO_EMP == coEmp
                                   && tb119.CO_MODU_CUR == coMod
                                   && tb119.CO_CUR == coCur
                                   && tb119.CO_TUR == coTur
                                   && tb119.CO_MAT == coMat
                                   && tb119.DT_ATIV_REAL == dtAtv
                                   && (qtTemposAux != null ? tb119.CO_ATIV_PROF_TUR >= auxTempo2 && tb119.CO_ATIV_PROF_TUR <= coAti : tb119.CO_ATIV_PROF_TUR == coAti)
                                   select new atividade
                                   {
                                       deTema = tb119.DE_TEMA_AULA,
                                       coAtiv = tb119.CO_ATIV_PROF_TUR,
                                       chkSel = true,
                                       coTempo = tb119.CO_TEMPO_ATIV,
                                   }).ToList();

                    grdAtividades.DataSource = retorno;
                    grdAtividades.DataBind();

                    #endregion
                }
                else
                {
                    #region Pesquisa as atividades com lançamento sem matéria

                    //Verifica Quantos tempos o Usuário selecionou na funcionalidade anterior, para que dessa maneira o select possa pesquisar Todos da Página anterior.
                    int? qtTemposAux = (Session["auxQtTempos"] != null ? (int)HttpContext.Current.Session["auxQtTempos"] - 1 : (int?)null);
                    int auxTempo2 = (qtTemposAux.HasValue ? coAti - qtTemposAux.Value : 0);

                    var retorno = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                   where tb119.CO_EMP == coEmp
                                   && tb119.CO_MODU_CUR == coMod
                                   && tb119.CO_CUR == coCur
                                   && tb119.CO_TUR == coTur
                                   && tb119.DT_ATIV_REAL == dtAtv
                                   && (qtTemposAux != null ? tb119.CO_ATIV_PROF_TUR >= auxTempo2 && tb119.CO_ATIV_PROF_TUR <= coAti : tb119.CO_ATIV_PROF_TUR == coAti)
                                   select new atividade
                                   {
                                       deTema = tb119.DE_TEMA_AULA,
                                       coAtiv = tb119.CO_ATIV_PROF_TUR,
                                       chkSel = true,
                                       coTempo = tb119.CO_TEMPO_ATIV,
                                   }).ToList();

                    grdAtividades.DataSource = retorno;
                    grdAtividades.DataBind();

                    #endregion
                }
            }
        }

        public class atividade
        {
            public string deTema { get; set; }
            public int coAtiv { get; set; }
            public bool chkSel { get; set; }
            public int coTempo { get; set; }
            public int nomeTempo { get; set; }
            public string freq
            {
                get
                {
                    var res = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where tb132.CO_ATIV_PROF_TUR == this.coAtiv
                               select tb132).ToList();

                    return (res.Count == 0 ? "Não" : "Sim");
                }
            }
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
        }

        private void carregaFreqAluno()
        {
            foreach (GridViewRow li in grdBusca.Rows)
            {
                string lblFlagPres = ((HiddenField)li.Cells[2].FindControl("hdCoFlagFreq")).Value;
                ((DropDownList)li.Cells[2].FindControl("ddlFreq")).SelectedValue = lblFlagPres;
            }
        }

        //protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HiddenField lblFlagPres = (HiddenField)e.Row.FindControl("hdCoFlagFreq");
        //        DropDownList ddlFlagFreq = (DropDownList)e.Row.FindControl("ddlFreq");
        //        ddlFlagFreq.SelectedValue = lblFlagPres.Value;

        //        ((DropDownList)e.Row.FindControl("ddlFreq")).SelectedValue = ((HiddenField)e.Row.FindControl("hdCoFlagFreq")).Value;
        //    }
        //}

        // Desmarca todos os checkbox que não forem clicados
        protected void ckSelect_CheckedChange(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;
            int coAtiv = 0;
            int nrTp = 0;

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
                            nrTp = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoTp")).Value);
                            string freqlanc = linha.Cells[3].Text;
                            if (freqlanc == "N&#227;o")
                                chk.Checked = false;

                            chkRepeteFreq.Checked = false;
                        }
                    }
                }
            }

            CarregaGrid(coAtiv, nrTp);
        }

        #region Funções de Campo

        protected void btnRegFreq_Click(object sender, EventArgs e)
        {
            // Salva
            //if (AcaoBarraCadastro())
            //{
            //    TB119_ATIV_PROF_TURMA tb119 = (TB119_ATIV_PROF_TURMA)Session["TB119_ATIV_PROF_TURMA"];
            //    if (chkAtivNaoPlanej.Checked)
            //    {
            //        // Redireciona para sem planejamento
            //        Response.Redirect("~/GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3404_LancManuFreqAluTurmaSemRegAtiv/Cadastro.aspx?moduloId=997&moduloNome=Frequ%C3%AAncia%20de%20Aluno%20-%20Lan%C3%A7amento%20por%20S%C3%A9rie/Turma%20(Sem%20Planejamento)&moduloId=997");

            //    }
            //    else
            //    {
            //        // Redireciona para com planejamento
            //        Response.Redirect("~/GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3402_LancManutFreqAlunoSerieTurma/Cadastro.aspx?moduloId=452&moduloNome=Frequ%C3%AAncia%20de%20Aluno%20-%20Lan%C3%A7amento%20por%20S%C3%A9rie/Turma%20(Com%20Planejamento)&moduloId=452");

            //    }
            //}
        }

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