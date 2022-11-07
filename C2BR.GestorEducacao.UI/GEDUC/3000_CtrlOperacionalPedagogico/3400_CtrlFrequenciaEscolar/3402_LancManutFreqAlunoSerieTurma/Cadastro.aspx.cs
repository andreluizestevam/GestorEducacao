//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO DE FREQÜÊNCIA DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 16/07/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3402_LancManutFreqAlunoSerieTurma
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
                divGridAluno.Visible = false;
                CarregaParametros();
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaMaterias();
                CarregaTurma();
                
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


        private void CarregaParametros()
        {
            if (HttpContext.Current.Session["ParametrosBusca"] != null)
            {
                // Pega os valores dos parâmetros salvos na session
                var parametros = HttpContext.Current.Session["ParametrosBusca"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var ctx = GestorEntities.CurrentContext;

                    var par = parametros.ToString().Split(';');
                    var modalidade = int.Parse(par[0]);
                    var serieCurso = par[1];
                    var turma = par[2];
                    var dtRealizacao = par[3];
                    var disciplina = par[4];
                    var ano = par[5];

                    ddlAno.SelectedValue = ano;

                    // Passa o valor de data para a pesquisa de acordo com a data de realização
                    txtPeriodoDe.Text = dtRealizacao;
                    txtPeriodoAte.Text = dtRealizacao;

                    // Carrega e seleciona a modalidade salva
                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade.ToString();

                    // Carrega e seleciona a série/curso salva
                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    // Carrega e seleciona a turma salva
                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    // Carrega e seleciona a matéria salva
                    CarregaMaterias();
                    ddlDisciplina.SelectedValue = disciplina;

                    // Carrega a referência
                        //CarregarBimestre();
                    CarregaMedidas();

                    // Faz a pesquisa da configuração de Bimestres da unidade
                    var Bimestres = (from tb82 in ctx.TB82_DTCT_EMP
                                     where tb82.CO_EMP == LoginAuxili.CO_EMP
                                     select new
                                     {
                                         DtInicioB1 = tb82.DT_PERIO_INICI_BIM1,
                                         DtFinalB1 = tb82.DT_PERIO_FINAL_BIM1,
                                         DtInicioB2 = tb82.DT_PERIO_INICI_BIM2,
                                         DtFinalB2 = tb82.DT_PERIO_FINAL_BIM2,
                                         DtInicioB3 = tb82.DT_PERIO_INICI_BIM3,
                                         DtFinalB3 = tb82.DT_PERIO_FINAL_BIM3,
                                         DtInicioB4 = tb82.DT_PERIO_INICI_BIM4,
                                         DtFinalB4 = tb82.DT_PERIO_FINAL_BIM4,
                                     }).First();

                    var dataRealizacao = DateTime.Parse(dtRealizacao);

                    // Verifica em qual Trimestre se encaixa a data de realização da atividade e seleciona o Trimestre correspondente na combo
                    if (Bimestres.DtFinalB1 != null)
                        if (dataRealizacao >= Bimestres.DtInicioB1.Value && dataRealizacao <= Bimestres.DtFinalB1.Value)
                            ddlReferencia.SelectedValue = "B1";
                    if (Bimestres.DtFinalB2 != null)
                        if (dataRealizacao >= Bimestres.DtInicioB2.Value && dataRealizacao <= Bimestres.DtFinalB2.Value)
                            ddlReferencia.SelectedValue = "B2";
                    if (Bimestres.DtFinalB3 != null)
                        if (dataRealizacao >= Bimestres.DtInicioB3.Value && dataRealizacao <= Bimestres.DtFinalB3.Value)
                            ddlReferencia.SelectedValue = "B3";
                    if (Bimestres.DtFinalB4 != null)
                        if (dataRealizacao >= Bimestres.DtInicioB4.Value && dataRealizacao <= Bimestres.DtFinalB4.Value)
                            ddlReferencia.SelectedValue = "B4";

                    // Faz a pesquisa
                    PesquisaGridClick();
                }
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int? materia = (ddlDisciplina.SelectedValue == "-1" || ddlDisciplina.SelectedValue == "") ? null : (int?)int.Parse(ddlDisciplina.SelectedValue);
            bool ocorAtiv = false;
            var quantidadeRegistros = "";
            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlReferencia.SelectedValue == "B1")
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
                else if (ddlReferencia.SelectedValue == "B2")
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
                else if (ddlReferencia.SelectedValue == "B3")
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
            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    ocorAtiv = true;
                    DateTime dataFreq = DateTime.Parse(((Label)linha.Cells[2].FindControl("lblDtAtiv")).Text);
                    if (dataFreq > DateTime.Now)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência não pode ser superior a data atual.");
                        return;
                    }
                }
            }

            if (!ocorAtiv)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não existe(m) Atividade(s) selecionada(s).");
                return;
            }
            quantidadeRegistros = grdBusca.Rows.Count.ToString();
            foreach (GridViewRow linhaAtiv in grdAtividades.Rows)
            {
                if (((CheckBox)linhaAtiv.Cells[0].FindControl("ckSelect")).Checked)
                {
                    TB132_FREQ_ALU tb132;

                    int coAtividade = Convert.ToInt32(grdAtividades.DataKeys[linhaAtiv.RowIndex].Values[0]);
                    DateTime dataFreq = DateTime.Parse(((Label)linhaAtiv.Cells[2].FindControl("lblDtAtiv")).Text);

                    //----------------> Varre toda a grid de Busca
                    foreach (GridViewRow linha in grdBusca.Rows)
                    {
                        //--------------------> Recebe o código do Aluno
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        var tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtividade);
                        int numTempo = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
                        //--------------------> Faz a verificação para saber se já existe frequência para o dia informado
                        TB132_FREQ_ALU ocoTb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                                   where lTb132.TB07_ALUNO.CO_ALU == coAlu
                                                   && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                                   && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade
                                                   && lTb132.TB01_CURSO.CO_CUR == serie
                                                   && lTb132.DT_FRE == dataFreq
                                                   && lTb132.CO_TUR == turma
                                                   && lTb132.CO_MAT == tb119.CO_MAT
                                                   && lTb132.NR_TEMPO == numTempo
                                                   && lTb132.CO_ATIV_PROF_TUR == coAtividade
                                                   select lTb132).FirstOrDefault();

                        string strTipoFrequencia = ((DropDownList)linha.Cells[2].FindControl("ddlFreq")).SelectedValue;

                        if (ocoTb132 != null)
                        {
                            if (ocoTb132.CO_FLAG_FREQ_ALUNO != strTipoFrequencia)
                            {
                                ocoTb132.CO_FLAG_FREQ_ALUNO = strTipoFrequencia;
                                if (ocoTb132.NR_TEMPO == null) ocoTb132.NR_TEMPO = numTempo;
                                ocoTb132.CO_BIMESTRE = ddlReferencia.SelectedValue;
                                ocoTb132.FL_HOMOL_FREQU = "S";
                                TB132_FREQ_ALU.SaveOrUpdate(ocoTb132, true);

                                //if (chkAtualizaHist.Checked)
                                //{
                                //    ocoTb132.TB01_CURSOReference.Load();
                                //    ocoTb132.TB07_ALUNOReference.Load();

                                //    AuxiliGeral.AtualizaHistFreqAlu(this, ocoTb132.TB01_CURSO.CO_EMP, ocoTb132.CO_ANO_REFER_FREQ_ALUNO, ocoTb132.TB01_CURSO.CO_MODU_CUR, ocoTb132.TB01_CURSO.CO_CUR, ocoTb132.CO_TUR, ddlBimestre.SelectedValue);
                                //}
                            }
                        }
                        else
                        {
                            tb132 = new TB132_FREQ_ALU();

                            tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                            tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                            tb132.CO_FLAG_FREQ_ALUNO = strTipoFrequencia;
                            tb132.CO_TUR = turma;
                            tb132.CO_MAT = tb119.CO_MAT;
                            tb132.DT_FRE = dataFreq;
                            tb132.CO_COL = LoginAuxili.CO_COL;
                            tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                            tb132.CO_ATIV_PROF_TUR = coAtividade;
                            tb132.CO_ANO_REFER_FREQ_ALUNO = dataFreq.Year;
                            tb132.CO_BIMESTRE = ddlReferencia.SelectedValue;
                            tb132.NR_TEMPO = numTempo;
                            tb132.FL_HOMOL_FREQU = "S";
                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);

                            if (tb132.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb132) < 1)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                                return;
                            }
                            //else
                            //{
                            //    if (chkAtualizaHist.Checked)
                            //    {
                            //        AuxiliGeral.AtualizaHistFreqAlu(this, tb132.TB01_CURSO.CO_EMP, tb132.CO_ANO_REFER_FREQ_ALUNO, tb132.TB01_CURSO.CO_MODU_CUR, tb132.TB01_CURSO.CO_CUR, tb132.CO_TUR, ddlBimestre.SelectedValue);
                            //    }
                            //}
                        }
                    }
                }
            }
            AuxiliPagina.EnvioAvisoGeralSistema(this, quantidadeRegistros + " Frequências registradas com sucesso para a atividade selecionada!");
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);

            //ddlModalidade.Items.Clear();
            //ddlSerieCurso.Items.Clear();
            //ddlTurma.Items.Clear();
            //ddlBimestre.Items.Clear();
            //ddlTempo.Items.Clear();
            if (ddlDisciplina.Enabled)
                ddlDisciplina.Items.Clear();
            LimpaGrides();
        }

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int tempo = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : -1;
            List<int> atividade = AtividadeEscolhida();
            int codigoAtividade, tempoAtividade;
            string anoMesMat = ddlAno.SelectedValue;
            decimal anoMes = decimal.Parse(ddlAno.SelectedValue);
            string Bimestre = ddlReferencia.SelectedValue;
            int materia = ddlDisciplina.SelectedValue == "" ? 0 : int.Parse(ddlDisciplina.SelectedValue);
            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGridAluno.Visible = true;

            var lstAlunoMatricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()

                                     where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat && tb08.CO_CUR == serie
                                     && tb08.CO_TUR == turma && tb08.CO_SIT_MAT == "A"
                                     select new listaAlunos
                                     {
                                         CO_ALU = tb08.CO_ALU,
                                         NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                         CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                                         CO_ALU_CAD = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."),
                                         CO_SIT_MAT = tb08.CO_SIT_MAT,
                                         Nota_Aluno = "S"
                                     }).ToList().OrderBy(m => m.NO_ALU);

            if (atividade.Count > 0)
            {
                codigoAtividade = atividade[0];
                tempoAtividade = atividade[1];
                var lstAtividades = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                     where tb132.CO_TUR == turma
                                     && tb132.TB01_CURSO.CO_CUR == serie
                                     && tb132.CO_ATIV_PROF_TUR == codigoAtividade
                                     && tb132.NR_TEMPO == tempoAtividade
                                     && tb132.CO_ANO_REFER_FREQ_ALUNO == anoMes
                                     && tb132.CO_BIMESTRE == Bimestre
                                     && (materia == -1 ? 0 == 0 : tb132.CO_MAT == materia)
                                     select tb132);
                if (lstAtividades != null && lstAtividades.Count() > 0)
                {
                    foreach (var linha in lstAlunoMatricula)
                    {
                        var linhasFiltradas = lstAtividades.Where(f => f.TB07_ALUNO.CO_ALU == linha.CO_ALU).DefaultIfEmpty();
                        if (linhasFiltradas != null && linhasFiltradas.Count() > 0)
                        {
                            string linhaA = linhasFiltradas.FirstOrDefault() == null ? "S" : linhasFiltradas.FirstOrDefault().CO_FLAG_FREQ_ALUNO;
                            if (linhaA != null && linhaA != "")
                                linha.Nota_Aluno = linhaA;
                        }
                    }
                }
            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid de Atividades
        /// </summary>
        private void CarregaGridAtividade()
        {
            string coAnoRefPla = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : "";
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = (ddlDisciplina.SelectedValue == "-1" || ddlDisciplina.SelectedValue == "") ? 0 : int.Parse(ddlDisciplina.SelectedValue);
            int tempo = ddlTempo.SelectedValue == "" ? 0 : int.Parse(ddlTempo.SelectedValue);
            DateTime dtInicio = DateTime.Parse(txtPeriodoDe.Text);
            DateTime dtFim = DateTime.Parse(txtPeriodoAte.Text);

            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_ANO_MES_MAT == coAnoRefPla
                             && (materia != 0 ? tb119.CO_MAT == materia : materia == 0)
                             && tb119.CO_MODU_CUR == modalidade
                             && tb119.CO_CUR == serie
                             && tb119.CO_TUR == turma
                             && tb119.DT_ATIV_REAL >= dtInicio
                             && tb119.DT_ATIV_REAL <= dtFim
                             && tb119.CO_TEMPO_ATIV == tempo
                             select new listaAtividades
                             {
                                 DT_ATIV_REAL = tb119.DT_ATIV_REAL,
                                 DE_TEMA_AULA = tb119.DE_TEMA_AULA,
                                 CO_ATIV_PROF_TUR = tb119.CO_ATIV_PROF_TUR,
                                 CO_TEMPO_ATIV = tb119.CO_TEMPO_ATIV,
                                 FL_PLANEJ_ATIV = (tb119.FLA_AULA_PLAN ? "Sim" : "Não"),
                                 DES_TIPO_ATIV = (tb119.CO_TIPO_ATIV == "ANO" ? "Aula Normal" : (tb119.CO_TIPO_ATIV == "AEX" ? "Aula Extra" :
                                 (tb119.CO_TIPO_ATIV == "ARE" ? "Aula de Reforço" : (tb119.CO_TIPO_ATIV == "ARC" ? "Aula de Recuperação" :
                                 (tb119.CO_TIPO_ATIV == "TES" ? "Teste" : (tb119.CO_TIPO_ATIV == "PRO" ? "Prova" : (tb119.CO_TIPO_ATIV == "TRA" ? "Trabalho" :
                                 (tb119.CO_TIPO_ATIV == "AGR" ? "Atividade em Grupo" : (tb119.CO_TIPO_ATIV == "ATE" ? "Atividade Externa" :
                                 (tb119.CO_TIPO_ATIV == "ATI" ? "Atividade Interna" : "Outros"))))))))))
                             }).OrderBy(p => p.DT_ATIV_REAL);

            divGrid.Visible = true;

            grdAtividades.DataKeyNames = new string[] { "CO_ATIV_PROF_TUR", "CO_TEMPO_ATIV" };
            grdAtividades.DataSource = resultado;
            grdAtividades.DataBind();

            if (grdAtividades.Rows.Count > 0)
            {
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    ((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked = false;
                }
            }

        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            }
            else
            {
                int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);
            }
            //ddlSerieCurso.Items.Clear();
            //ddlTurma.Items.Clear();
            //ddlBimestre.Items.Clear();
            //ddlTempo.Items.Clear();
            //if (ddlDisciplina.Enabled)
            //    ddlDisciplina.Items.Clear();
            LimpaGrides();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
                }
                else
                {
                    int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                    AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, false);
                }
                LimpaGrides();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
            }
            else
            {
                int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                AuxiliCarregamentos.CarregaTurmasSiglaProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, false);
            }
            LimpaGrides();
        }

        //private void CarregarBimestre()
        //{
        //    ddlReferencia.Items.Clear();
        //    ddlReferencia.Items.Insert(0, new ListItem("Bimestre 3", "B3"));
        //    ddlReferencia.Items.Insert(0, new ListItem("Bimestre 2", "B2"));
        //    ddlReferencia.Items.Insert(0, new ListItem("Bimestre 1", "B1"));
        //    ddlReferencia.Items.Insert(0, new ListItem("Selecione", "0"));
        //    ddlReferencia.SelectedValue = "0";

        //    ddlTempo.Items.Clear();
        //    if (ddlDisciplina.Enabled)
        //        ddlDisciplina.Items.Clear();
        //    LimpaGrides();
        //}

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
            ddlTempo.Items.Insert(0, new ListItem("Sem Registro", "0"));
            ddlTempo.Items.Insert(0, new ListItem("Selecione", "00"));
            ddlTempo.SelectedValue = "00";

            if (ddlDisciplina.Enabled)
                ddlDisciplina.Items.Clear();
            LimpaGrides();

        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias, verifica se o usuário é professor.
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlDisciplina.Items.Clear();
                string anog = ddlAno.SelectedValue;
                int coem = LoginAuxili.CO_EMP;
                //ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                //                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                //                            select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == anog && tb43.CO_EMP == coem && tb43.FL_LANCA_FREQU == "S"
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).Distinct();

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataSource = res;
                ddlDisciplina.DataBind();

                ddlDisciplina.Items.Insert(0, new ListItem("Todas", "-1"));
                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
            }
            else
            {
                int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                //ddlDisciplina.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                //                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                //                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                //                         where rm.CO_COL_RESP == LoginAuxili.CO_COL
                //                         && rm.CO_MODU_CUR == modalidade
                //                         && rm.CO_CUR == serie
                //                         && rm.CO_ANO_REF == ano
                //                         && rm.CO_TUR == turma
                //                         select new
                //                         {
                //                             tb107.NO_MATERIA,
                //                             rm.CO_MAT,
                //                             tb107.ID_MATERIA
                //                         }).Distinct();

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

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataSource = resuR;
                ddlDisciplina.DataBind();

                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
            }      
        }

        /// <summary>
        /// Método que desabilita as grides
        /// </summary>
        private void LimpaGrides()
        {
            grdAtividades.DataSource = null;
            grdAtividades.DataBind();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
        }
        #endregion

        #region Classe
        private class listaAtividades
        {
            public DateTime DT_ATIV_REAL { get; set; }
            public string DE_TEMA_AULA { get; set; }
            public int CO_ATIV_PROF_TUR { get; set; }
            public int CO_TEMPO_ATIV { get; set; }
            public string FL_PLANEJ_ATIV { get; set; }
            public string DES_TIPO_ATIV { get; set; }
            public string NU_TEMPO
            {
                get
                {
                    return this.CO_TEMPO_ATIV == 0 ? "Sem Regis" : this.CO_TEMPO_ATIV.ToString();
                }
            }
            public Boolean MarcarLinha
            {
                get
                {
                    return false;
                }
            }
        }

        private class listaAlunos
        {
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public string CO_ANO_MES_MAT { get; set; }
            public string CO_ALU_CAD { get; set; }
            public string CO_SIT_MAT { get; set; }
            public string Nota_Aluno { get; set; }
        }
        #endregion

        #region Eventos componentes

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaSerieCurso();
                CarregaMaterias();
                CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

                if (modalidade != 0 && serie != 0)
                {
                    string strFreqTipoParam = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).CO_PARAM_FREQ_TIPO;

                    if (strFreqTipoParam != "D")
                    {
                        ddlDisciplina.Enabled = true;
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
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                //CarregarTrimestre();
                CarregaMedidas();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpaGrides();
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaMaterias();
                CarregaTurma();
        }

        protected void grdAtividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbSelecionado = (CheckBox)e.Row.FindControl("ckSelect");
                cbSelecionado.Checked = true;
            }
        }

        //====> Faz a Pesquisa da gride de Atividade e dos Alunos
        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            PesquisaGridClick();
        }

        private void PesquisaGridClick()
        {
            if (!ddlDisciplina.Enabled || (ddlDisciplina.SelectedValue != "0" && ddlDisciplina.SelectedValue != ""))
            {
                grdAtividades.DataSource = null;
                grdAtividades.DataBind();
                grdBusca.DataSource = null;
                grdBusca.DataBind();
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                int materia = (ddlDisciplina.SelectedValue == "-1" || ddlDisciplina.SelectedValue == "") ? 0 : int.Parse(ddlDisciplina.SelectedValue);

                if (modalidade == 0 || turma == 0 || serie == 0 || ddlAno.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Os campos de Ano, Modalidade, Série e Turma devem ser informados.");
                    return;
                }

                if (txtPeriodoDe.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data de Início deve ser informada.");
                    return;
                }

                if (txtPeriodoAte.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data Fim deve ser informada.");
                    return;
                }

                if ((DateTime.Parse(txtPeriodoDe.Text) > DateTime.Now) || (DateTime.Parse(txtPeriodoAte.Text) > DateTime.Now))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data informada não pode ser superior a data atual.");
                    return;
                }

                if (DateTime.Parse(txtPeriodoDe.Text) > DateTime.Parse(txtPeriodoAte.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data inicial deve ser menor que data final.");
                    return;
                }

                CarregaGridAtividade();
                CarregaGrid();
            }
        }

        protected void ddlReferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                CarregaTempo();
        }

        protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "00")
            {
                if (ddlDisciplina.Enabled)
                    CarregaMaterias();
            }
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            int quantidade = 0;
            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                CheckBox marcado = (CheckBox)linha.Cells[0].FindControl("ckSelect");
                if (marcado != null && marcado.Checked)
                    quantidade++;
            }
            if (quantidade > 1)
                ((CheckBox)sender).Checked = false;
            if (((CheckBox)sender).Checked && quantidade == 1)
                CarregaGrid();
        }

        #endregion

        #region Metodos personalizados
        /// <summary>
        /// /Retorna a atividade selecionada
        /// </summary>
        /// <returns></returns>
        private List<int> AtividadeEscolhida()
        {
            List<int> retorno = new List<int>();
            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    retorno.Add(Convert.ToInt32(grdAtividades.DataKeys[linha.RowIndex].Values[0]));
                    retorno.Add(Convert.ToInt32(grdAtividades.DataKeys[linha.RowIndex].Values[1]));
                }
            }
            return retorno;
        }
        #endregion

    }
}