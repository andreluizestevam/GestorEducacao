//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO DE FREQÜÊNCIA DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 31/07/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 31/07/2013| Victor Martins Machado     | Criação da funcionalidade.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 05/08/2013| Victor Martins Machado     | Correção do problema da página não carregar as frequências lençadas.
//           |                            | Foi colocada a população da combo de frequência no DataBound da grid.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 19/03/2014| Victor Martins Machado     | Criação da regra que verifica se a turma é única ou não para mostra,
//           |                            | se for o caso, somente a matéria de turma única, com sigla "MSR"
//           |                            | na função CarregaMaterias.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 19/03/2014| Victor Martins Machado     | Criação da regra que verifica se a turma é única ou Não para mostrar,
//           |                            | se for o caso, somente a matéria de turma única, com sigla "MSR"
//           |                            | na função ddlTurma_SelectedIndexChanged
//           |                            | 
// 22/07/2014| Maxwell ALmeida            | Alteração para que os campos superiores se mantenham com seus valores após o salvamento do registro.

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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3118_ManuDiarioClasse
{
    public partial class Cadastro : System.Web.UI.Page
    {
        // PADRÃO DE PÁGINAS DE CADASTRO
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //====> CRIAÇÃO DAS INSTÂNCIAS UTILIZADAS
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(CurrentPadraoCadastro_OnDelete);
        }

        // MÉTODO EXECUTADO QUANDO A PÁGINA É CARREGADA
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // VALIDA SE É A PRIMEIRA VEZ QUE A PAGINA É CARREGADA, SE NÃO É UM OBJETO DA PÁGINA RECARREGANDO A MESMA
            {
                CarregaAnos(); // EXECUTA O MÉTODO QUE CARREGA OS ANOS
                CarregaModalidade(); // EXECUTA O MÉTODO QUE CARREGA AS MODALIDADES
                CarregarProfessores();
                CarregaTempo(); // EXECUTA O MÉTODO QUE CARREGA OS TEMPOS

                txtDataFreq.Text = txtDataFreqAte.Text = DateTime.Now.ToString();
                CarregaInfosBusca();
            }
        }

        //====> PROCESSO DE INCLUSÃO, ALTERAÇÃO E EXCLUSÃO DE REGISTROS NA ENTIDADE DO BD, APÓS A AÇÃO DE SALVAR
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique == CurrentPadraoCadastros.BarraCadastro.botaoDelete)
            {
                AbreModalCliente();
                return;
            }

            try
            {
                if (verificaCampos())
                {
                    return;
                }

                int qtdFreq = 0;
                int qtdAtiv = 0;


                qtdAtiv = AlteraAtividades(); // EXECUTA A FUNÇÃO QUE ALTERA AS ATIVIDADES
                qtdFreq = AlteraFrequencias(); // EXECUTA A FUNÇÃO QUE HOMOLOGA AS FREQUÊNCIAS

                string t = "";
                if (qtdAtiv == 1)
                    t += qtdAtiv.ToString() + " atividade homologada ";
                else
                    t += qtdAtiv.ToString() + " atividades homologadas ";

                if (qtdFreq == 1)
                    t += qtdAtiv.ToString() + " frequência homologada";
                else
                    t += qtdFreq.ToString() + " frequências homologadas ";

                //Envia os dados do parâmetro de busca para uma variável em sessão, para recarregar os campos novamente depois de salvar o registro.
                var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerie.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                    + ddlBimestre.SelectedValue + ";" + txtDataFreq.Text + ";" + txtDataFreqAte.Text + ";" + ddlMateria.SelectedValue + ";" + ddlTempo.SelectedValue + ";" + ddlProfessor.SelectedValue;
                HttpContext.Current.Session["ParametrosBusca"] = parametros;

                AuxiliPagina.RedirecionaParaPaginaSucesso("Operação realizada com sucesso com " + t, Request.Url.AbsoluteUri);
            }
            catch
            {
                throw;
            }
        }

        void CurrentPadraoCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            AbreModalCliente();
        }

        //protected void ExcluiAtividade()
        //{
        //    int? materia = ddlMateria.SelectedValue != "" ? (int?)int.Parse(ddlMateria.SelectedValue) : null;
        //    int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
        //    int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
        //    int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
        //    int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
        //    string bimestre = ddlBimestre.SelectedValue;
        //    decimal anoMesMat = decimal.Parse(ddlAno.SelectedValue);
        //    DateTime data = DateTime.Parse(txtDataFreq.Text);
        //    DateTime dataAte = DateTime.Parse(txtDataFreqAte.Text);
        //    int coAtivProfTur = 0;

        //    RegistroLog log = new RegistroLog();

        //    TB119_ATIV_PROF_TURMA tb119;
        //    TB132_FREQ_ALU tb132;

        //    //var lst = IQueryable;
        //    List<TB132_FREQ_ALU> res = new List<TB132_FREQ_ALU>(); ;

        //    var ctx = GestorEntities.CurrentContext;

        //    // Passa por todos os registros da grid de atividade
        //    foreach (GridViewRow linha in grdAtividades.Rows)
        //    {
        //        // Valida se o registro foi selecionedo
        //        if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
        //        {
        //            coAtivProfTur = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCoAtv")).Value);
        //            tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtivProfTur);

        //            //if (TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == tb119.CO_ATIV_PROF_TUR).Any())
        //            //{
        //            //    res = (from fre in TB132_FREQ_ALU.RetornaTodosRegistros()
        //            //           where fre.TB01_CURSO.CO_CUR == serie
        //            //            && fre.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == modalidade
        //            //            && fre.CO_ANO_REFER_FREQ_ALUNO == anoMesMat
        //            //            && fre.CO_TUR == turma
        //            //            && fre.DT_FRE >= data
        //            //            && fre.DT_FRE <= dataAte
        //            //            && (tempoAula == -1 ? true : tempoAula == 0 ? fre.NR_TEMPO == 0 : fre.NR_TEMPO == tempoAula)
        //            //            && fre.CO_BIMESTRE == bimestre
        //            //            && (materia == null ? 0 == 0 : fre.CO_MAT == materia)
        //            //            && (coAtivProfTur == null ? (fre.CO_ATIV_PROF_TUR == 0 || fre.CO_ATIV_PROF_TUR == null) : fre.CO_ATIV_PROF_TUR == coAtivProfTur)
        //            //           select fre).ToList();

        //            //    if (res.Count() > 0)
        //            //    {
        //            //        foreach (TB132_FREQ_ALU t in res)
        //            //        {
        //            //            if (GestorEntities.Delete(t) <= 0)
        //            //            {
        //            //                AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir frequência.");
        //            //                return;
        //            //            }
        //            //            tb132 = t;
        //            //        }
        //            //        tb132 = new TB132_FREQ_ALU();
        //            //        log.RegistroLOG(tb132, RegistroLog.ACAO_DELETE);
        //            //    }
        //            //}

        //            if (GestorEntities.Delete(tb119) <= 0)
        //            {
        //                AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir atividade.");
        //                return;
        //            }

        //            log.RegistroLOG(tb119, RegistroLog.ACAO_DELETE);
        //        }
        //        else
        //        {
        //            //if (((HiddenField)linha.Cells[2].FindControl("hdFlHomol")).Value != "S")
        //            //{
        //            //    AuxiliPagina.EnvioMensagemErro(this, "A atividade selecionada não pode ser excluída porque esta homologada.");
        //            //    return;
        //            //}
        //        }
        //    }

        //    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro(s) Excluídos com Sucesso", Request.Url.AbsoluteUri);
        //}

        #endregion

        #region Carregamento

        /// <summary>
        /// Carrega as datas da frequência de acordo com o bimestre selecionado no campo Bimestre
        /// </summary>
        private void CarregaDatasFrequeBimestre()
        {
            var ctx = GestorEntities.CurrentContext;
            string bimestre = ddlBimestre.SelectedValue;
            int unidade = LoginAuxili.CO_EMP;


            #region Retorna as Datas dos Bimestres
            var datasBimestre = (from tb82 in ctx.TB82_DTCT_EMP
                                 where tb82.CO_EMP == unidade

                                 select new
                                 {
                                     // datas inicias dos bimestres
                                     dataInicialBimestre1 = tb82.DT_PERIO_INICI_BIM1,
                                     dataInicialBimestre2 = tb82.DT_PERIO_INICI_BIM2,
                                     dataInicialBimestre3 = tb82.DT_PERIO_INICI_BIM3,
                                     dataInicialBimestre4 = tb82.DT_PERIO_INICI_BIM4,

                                     // datas finais dos bimestres
                                     dataFinalBimestre1 = tb82.DT_PERIO_FINAL_BIM1,
                                     dataFinalBimestre2 = tb82.DT_PERIO_FINAL_BIM2,
                                     dataFinalBimestre3 = tb82.DT_PERIO_FINAL_BIM3,
                                     dataFinalBimestre4 = tb82.DT_PERIO_FINAL_BIM4,

                                 }).OrderBy(o => o.dataInicialBimestre1).FirstOrDefault();

            switch (bimestre)
            {
                case "B1":

                    if ((!datasBimestre.dataInicialBimestre1.HasValue) || (!datasBimestre.dataFinalBimestre1.HasValue))
                    {
                        carregaDatasPadroes();
                    }
                    else
                    {
                        txtDataFreq.Text = datasBimestre.dataInicialBimestre1.ToString();
                        txtDataFreqAte.Text = datasBimestre.dataFinalBimestre1.ToString();
                        txtDataFreq.Enabled = txtDataFreqAte.Enabled = false;
                    }

                    break;
                case "B2":

                    if ((!datasBimestre.dataInicialBimestre2.HasValue) || (!datasBimestre.dataFinalBimestre2.HasValue))
                    {
                        carregaDatasPadroes();
                    }
                    else
                    {
                        txtDataFreq.Text = datasBimestre.dataInicialBimestre2.ToString();
                        txtDataFreqAte.Text = datasBimestre.dataFinalBimestre2.ToString();
                        txtDataFreq.Enabled = txtDataFreqAte.Enabled = false;
                    }

                    break;
                case "B3":

                    if ((!datasBimestre.dataInicialBimestre3.HasValue) || (!datasBimestre.dataFinalBimestre3.HasValue))
                    {
                        carregaDatasPadroes();
                    }
                    else
                    {
                        txtDataFreq.Text = datasBimestre.dataInicialBimestre3.ToString();
                        txtDataFreqAte.Text = datasBimestre.dataFinalBimestre3.ToString();
                        txtDataFreq.Enabled = txtDataFreqAte.Enabled = false;
                    }
                    break;

                case "B4":
                    if ((!datasBimestre.dataInicialBimestre4.HasValue) || (!datasBimestre.dataFinalBimestre4.HasValue))
                    {
                        carregaDatasPadroes();
                    }
                    else
                    {
                        txtDataFreq.Text = datasBimestre.dataInicialBimestre4.ToString();
                        txtDataFreqAte.Text = datasBimestre.dataFinalBimestre4.ToString();
                        txtDataFreq.Enabled = txtDataFreqAte.Enabled = false;
                    }

                    break;
                default:
                    txtDataFreq.Enabled = txtDataFreqAte.Enabled = true;
                    txtDataFreq.Text = txtDataFreqAte.Text = DateTime.Now.ToString();
                    break;
            }

            #endregion

        }

        /// <summary>
        /// Carrega as informações no parâmetro de busca superior, para vir com tudo informado após salvar um registro.
        /// </summary>
        private void CarregaInfosBusca()
        {
            if (HttpContext.Current.Session["ParametrosBusca"] != null)
            {
                var parametros = HttpContext.Current.Session["ParametrosBusca"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var ano = par[0];
                    var modalidade = par[1];
                    var serieCurso = par[2];
                    var turma = par[3];
                    var Bimestre = par[4];
                    var DataIniFreq = par[5];
                    var DataFimFreq = par[6];
                    var Materia = par[7];
                    var Tempo = par[8];
                    var Professor = par[9];

                    ddlAno.SelectedValue = ano;
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerie();
                    ddlSerie.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    ddlBimestre.SelectedValue = Bimestre;

                    txtDataFreq.Text = DataIniFreq;
                    txtDataFreqAte.Text = DataFimFreq;

                    CarregarProfessores();
                    ddlProfessor.SelectedValue = Professor;

                    CarregaMaterias();
                    ddlMateria.SelectedValue = Materia;

                    CarregaTempo();
                    ddlTempo.SelectedValue = Tempo;

                    HttpContext.Current.Session.Remove("ParametrosBusca");
                    PesquisaGrides();
                }
            }
        }

        /// <summary>
        /// Carrega a primeira data do ano no campo de início e a última no campo final.
        /// </summary>
        private void carregaDatasPadroes()
        {
            txtDataFreq.Enabled = txtDataFreqAte.Enabled = true;

            //Trata as datas de início
            int ano = DateTime.Now.Year;
            DateTime dateini = new DateTime(ano, 01, 01);
            DateTime dateFim = new DateTime(ano, 12, 30);
            txtDataFreq.Text = dateini.ToString();
            txtDataFreqAte.Text = dateFim.ToString();
        }

        /// <summary>
        /// Carrega os tipos de Atividades
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaTiposAtividades(DropDownList ddl, string QualSelecionar)
        {
            var res = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                       where tb273.CO_SITUA_ATIV == "A"
                       select new { tb273.CO_SIGLA_ATIV, tb273.NO_TIPO_ATIV }).OrderBy(o => o.NO_TIPO_ATIV);

            ddl.DataTextField = "NO_TIPO_ATIV";
            ddl.DataValueField = "CO_SIGLA_ATIV";
            ddl.DataSource = res;
            ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("Selecione", ""));

            ddl.SelectedValue = QualSelecionar;
        }

        //====> MÉTODO QUE CARREGA OS ANOS
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending(m => m.CO_ANO_MES_MAT);

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
        }

        //====> MÉTODO QUE CARREGA AS MODALIDADES
        private void CarregaModalidade()
        {
            int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR == "S")
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);
            else
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true, true);

            CarregaSerie();
        }

        //====> MÉTODO QUE CARREGA AS SÉRIES
        private void CarregaSerie()
        {
            int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
            int coModu = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR == "S")
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerie, coModu, LoginAuxili.CO_COL, ano, false);
            else
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, coModu, LoginAuxili.CO_EMP, false, false, false, true, true);

            CarregaTurma();
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal seleção dos itens de exclusão
        /// </summary>
        private void AbreModalCliente()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModal();",
                true
            );
        }

        //====> MÉTODO QUE CARREGA AS TURMAS
        private void CarregaTurma()
        {
            int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
            int coModu = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlSerie.SelectedValue) ? int.Parse(ddlSerie.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR == "S")
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, coModu, coCur, LoginAuxili.CO_COL, ano, false);
            else
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, coModu, coCur, false, false, true, true);

            CarregaMaterias();
            CarregarProfessores();
        }

        /// <summary>
        /// Carrega os professores de acordo com os parâmetros informados
        /// </summary>
        private void CarregarProfessores()
        {
            int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int professor = LoginAuxili.CO_COL;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = (!string.IsNullOrEmpty(ddlMateria.SelectedValue) ? int.Parse(ddlMateria.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaProfessoresRespMateria(ddlProfessor, LoginAuxili.CO_EMP, modalidade, serie, turma, 0, ano, false);
            ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));

            if (LoginAuxili.FLA_PROFESSOR == "S")
            {
                ddlProfessor.Enabled = false;

                //Se a pessoa logada é professora, verifica antes se está disponível no dropdown
                ListItem itCol = ddlProfessor.Items.FindByValue(LoginAuxili.CO_COL.ToString());
                if (itCol != null)
                    ddlProfessor.SelectedValue = LoginAuxili.CO_COL.ToString();
                else // Se não estiver, trata erro
                {
                    ddlProfessor.Items.Clear();
                    ddlProfessor.Items.Insert(0, new ListItem("Você, professor(a), não está associado nesses parâmetros", ""));
                }
            }
        }

        //====> MÉTODO QUE CARREGA AS MATÉRIAS
        private void CarregaMaterias()
        {
            int ano = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? int.Parse(ddlAno.SelectedValue) : 0);
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int professor = (!string.IsNullOrEmpty(ddlProfessor.SelectedValue) ? int.Parse(ddlProfessor.SelectedValue) : 0);
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string turmaUnica = "N";
            if (modalidade != 0 && serie != 0 && turma != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, turma).CO_FLAG_RESP_TURMA;
            }

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
                    ddlMateria.DataTextField = "NO_MATERIA";
                    ddlMateria.DataValueField = "CO_MAT";
                    ddlMateria.DataSource = res;
                    ddlMateria.DataBind();

                    if (res.Count() > 1)
                        ddlMateria.Items.Insert(0, new ListItem("Selecione", "0"));
                }
            }
            else
            {
                int Professor = (!string.IsNullOrEmpty(ddlProfessor.SelectedValue) ? int.Parse(ddlProfessor.SelectedValue) : 0);

                var res = (from tbres in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbres.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where tbres.CO_MODU_CUR == modalidade
                           && tbres.CO_CUR == serie
                           && tbres.CO_COL_RESP == professor
                           && tbres.CO_TUR == turma
                           select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(d => d.CO_MAT);

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "CO_MAT";
                ddlMateria.DataSource = res;
                ddlMateria.DataBind();

                ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        //====> MÉTODO QUE CARREGA OS TEMPOS DA FREQUÊNCIA
        private void CarregaTempo()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            var tb06 = (from lTb06 in TB06_TURMAS.RetornaTodosRegistros()
                        where lTb06.CO_EMP == LoginAuxili.CO_EMP
                        && lTb06.CO_MODU_CUR == modalidade
                        && lTb06.CO_CUR == serie
                        && lTb06.CO_TUR == turma
                        select new { lTb06.CO_PERI_TUR }).FirstOrDefault();

            string strTurno = tb06 != null ? tb06.CO_PERI_TUR : "";

            ddlTempo.DataSource = (from tb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
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

            ddlTempo.DataTextField = "tempoCompleto";
            ddlTempo.DataValueField = "nrTempo";
            ddlTempo.DataBind();

            ddlTempo.Items.Insert(0, new ListItem("Não definido", "0"));
            ddlTempo.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        //====> MÉTODO QUE CARREGA A GRID DE ATIVIDADES
        private void CarregaGridAtividade()
        {
            string coAnoRefPla = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue.Trim() : "";
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int professor = (!string.IsNullOrEmpty(ddlProfessor.SelectedValue) ? int.Parse(ddlProfessor.SelectedValue) : 0);
            DateTime dtFre = DateTime.Parse(txtDataFreq.Text);
            DateTime dtFreAte = DateTime.Parse(txtDataFreqAte.Text);
            int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_ANO_MES_MAT == coAnoRefPla
                             && (materia != 0 ? tb119.CO_MAT == materia : materia == 0)
                             && tb119.CO_MODU_CUR == modalidade && tb119.CO_CUR == serie && tb119.CO_TUR == turma
                                 //&& tb119.DT_ATIV_REAL.Year == dtFre.Year
                                 //&& tb119.DT_ATIV_REAL.Month == dtFre.Month
                                 //&& tb119.DT_ATIV_REAL.Day == dtFre.Day
                             && tb119.DT_ATIV_REAL >= dtFre
                             && tb119.DT_ATIV_REAL <= dtFreAte
                             && tb119.CO_COL_ATIV == professor
                             && (tempoAula == -1 ? true : tempoAula == 0 ? tb119.CO_TEMPO_ATIV == 0 : tb119.CO_TEMPO_ATIV == tempoAula)
                             select new AtivSaida
                             {
                                 chk = tb119.FL_HOMOL_ATIV,
                                 CO_EMP = tb119.CO_EMP,
                                 CO_CUR = tb119.CO_CUR,
                                 CO_TUR = tb119.CO_TUR,
                                 CO_COL = tb119.CO_COL,
                                 CO_MAT = tb119.CO_MAT,
                                 FL_HOMOL_ATIV = tb119.FL_HOMOL_ATIV,
                                 DT_ATIV_REAL = tb119.DT_ATIV_REAL,
                                 DE_RES_ATIV = tb119.DE_RES_ATIV,
                                 CO_ATIV_PROF_TUR = tb119.CO_ATIV_PROF_TUR,
                                 CO_TEMPO_ATIV = tb119.CO_TEMPO_ATIV,
                                 DE_TEMA = tb119.DE_TEMA_AULA,
                                 CO_TIPO_ATIVIDADE = tb119.CO_TIPO_ATIV
                             }).OrderByDescending(p => p.DT_ATIV_REAL).Distinct();

            divGrid.Visible = true;

            grdAtividades.DataKeyNames = new string[] { "CO_ATIV_PROF_TUR" };

            grdAtividades.DataSource = resultado;
            grdAtividades.DataBind();
        }

        /// <summary>
        /// Percorre a grid de frequencias selecionadas e exclui os registros correspondentes
        /// </summary>
        private int ExcluiFrequencias()
        {
            RegistroLog log = new RegistroLog();
            int qtFreqExclu = 0;
            foreach (GridViewRow li in grdFreq.Rows)
            {
                //Exclui os itens selecionados
                if (((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked)
                {
                    int idFreq = int.Parse(((HiddenField)li.Cells[3].FindControl("hdIdFrequ")).Value);
                    TB132_FREQ_ALU tb132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(idFreq);

                    //Retorna para a página caso a frequência que está sendo excluída ainda esteja homologada
                    //if (tb132.FL_HOMOL_FREQU == "S")
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this.Page, "A Frequência em questão não pode ser excluída, pois ainda se encontra homologada.");
                    //    return 999;
                    //}

                    //Exclui apenas se não estiver homologada
                    if (((HiddenField)li.FindControl("hdFlHomolA")).Value == "N")
                        TB132_FREQ_ALU.Delete(tb132, true);

                    qtFreqExclu++;

                    log.RegistroLOG(tb132, RegistroLog.ACAO_DELETE);
                }
            }
            return qtFreqExclu;
        }

        /// <summary>
        /// Exclui todas as atividades selecionadas
        /// </summary>
        private int ExcluiAtividades()
        {
            RegistroLog log = new RegistroLog();
            int qtAtiviExclu = 0;
            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                //exclui as atividades selecionadas
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    int coAtivProfTur = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCoAtv")).Value);
                    TB119_ATIV_PROF_TURMA tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtivProfTur);

                    //Retorna para a página caso a Atividade que está sendo excluída ainda esteja homologada
                    //if (tb119.FL_HOMOL_ATIV == "S")
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this.Page, "A Atividade em questão não pode ser excluída, pois ainda se encontra homologada.");
                    //    return 999;
                    //}

                    //Apenas se estiver não homologada
                    if (((HiddenField)linha.FindControl("hdFlHomol")).Value == "N")
                        TB119_ATIV_PROF_TURMA.Delete(tb119, true);
                    else //Se esttiver homologada, gerar erro
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A atividade selecionada já está homologada, não é possível excluí-la!");
                        return 0;
                    }
                    qtAtiviExclu++;

                    log.RegistroLOG(tb119, RegistroLog.ACAO_DELETE);
                }
            }
            return qtAtiviExclu;
        }

        /// <summary>
        /// É executado quando se pesquisa a grid.
        /// </summary>
        private void PesquisaGrides()
        {
            if (verificaCampos())
                return;
            //====> DECLARAÇÃO DAS VARIÁVEIS UTILIZADAS NA FUNÇÃO
            int coModu = int.Parse(ddlModalidade.SelectedValue); // CÓDIGO DA MODALIDADE
            int coCur = int.Parse(ddlSerie.SelectedValue); // CÓDIGO DA SÉRIE
            int coTur = int.Parse(ddlTurma.SelectedValue); // CÓDIGO DA TURMA
            string coAno = ddlAno.SelectedValue; // ANO DE REFERÊNCIA
            string dtFre = txtDataFreq.Text; // DATA DA FREQUÊNCIA

            //====> ZERA A GRID DE ATIVIDADES
            grdAtividades.DataSource = null;
            grdAtividades.DataBind();

            //===> VALIDA SE OS CAMPOS MODALIDADE, SÉRIE, TURMA E ANO FORAM SELECIONADOS, POSSUEM VALOR
            if (coModu == 0 || coCur == 0 || coTur == 0 || coAno == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Os campos de Ano, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            //====> VALIDA SE O CAMPO DATA DA FREQUÊNCIA POSSUI VALOR
            if (dtFre == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência deve ser informada.");
                return;
            }

            //====> VALIDA SE A DATA DA FREQUÊNCIA INFORMADA É UMA DATA FUTURA
            if (DateTime.Parse(dtFre) > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência não pode ser uma data futura.");
                return;
            }

            //====> EXECUTA O MÉTODO QUE CARREGA AS ATIVIDADES NA GRID, DE ACORDO COM O FILTRO SELECIONADO
            CarregaGridAtividade();

            //====> EXECUTA O MÉTODO QUE CARREGA AS FREQUÊNCIAS NA GRUD, DE ACORDO COM O FILTRO SELECIONADO
            //CarregaGridFrequencia();
        }

        public class AtivSaida
        {
            public string DE_TEMA { get; set; }
            public string chk { get; set; }
            public bool chkValid
            {
                get
                {
                    if (this.chk == "S")
                        return true;
                    else
                        return false;
                }
            }
            //public bool nChk 
            //{
            //    get
            //    {
            //        return !this.chk;
            //    }
            //}
            public int CO_EMP { get; set; }
            public int CO_CUR { get; set; }
            public int CO_TUR { get; set; }
            public int CO_COL { get; set; }
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
            public int CO_TEMPO_ATIV { get; set; }
            public string CO_TIPO_ATIVIDADE { get; set; }
        }

        //====> MÉTODO QUE CARREGA A GRID DE FREQUÊNCIAS
        private void CarregaGridFrequencia(int? coAtividade = null)
        {
            int? materia = ddlMateria.SelectedValue != "" ? (int?)int.Parse(ddlMateria.SelectedValue) : null;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            string bimestre = ddlBimestre.SelectedValue;
            string anoMesMat = ddlAno.SelectedValue;
            DateTime data = DateTime.Parse(txtDataFreq.Text);
            DateTime dataAte = DateTime.Parse(txtDataFreqAte.Text);
            var ctx = GestorEntities.CurrentContext;
            //====> VALIDA SE O USUÁRIO SELECIONOU A MODALIDADE/SÉRIE/TURMA/TEMPO
            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdFreq.DataBind();
                return;
            }
            string sql = "";

            #region Consulta utilizando o linq
            var lista = (from mat in ctx.TB08_MATRCUR.AsQueryable()
                         where mat.CO_EMP == LoginAuxili.CO_EMP
                         && mat.CO_ANO_MES_MAT == anoMesMat
                         && mat.CO_CUR == serie
                         && mat.CO_TUR == turma
                         && mat.TB44_MODULO.CO_MODU_CUR == modalidade
                         join fre in ctx.TB132_FREQ_ALU.AsQueryable() on mat.CO_ALU equals fre.TB07_ALUNO.CO_ALU into resultado
                         from fre in resultado.DefaultIfEmpty()
                         where
                             //   fre.DT_FRE.Year == data.Year
                             //&& fre.DT_FRE.Month == data.Month
                             //&& fre.DT_FRE.Day == data.Day
                            fre.DT_FRE >= data
                         && fre.DT_FRE <= dataAte
                         && (tempoAula == -1) ? true : fre.NR_TEMPO == tempoAula
                             //&& fre.CO_BIMESTRE == bimestre
                         && (materia == null ? 0 == 0 : fre.CO_MAT == materia)
                         && (coAtividade == null ? (fre.CO_ATIV_PROF_TUR == 0 || fre.CO_ATIV_PROF_TUR == null) : fre.CO_ATIV_PROF_TUR == coAtividade)
                         select new FrequSaida
                         {
                             chk = fre.FL_HOMOL_FREQU == "N" ? true : false,
                             CO_ALU = mat.TB07_ALUNO.CO_ALU,
                             NO_ALU = mat.TB07_ALUNO.NO_ALU,
                             CO_ANO_MES_MAT = mat.CO_ANO_MES_MAT,
                             NIRE = mat.TB07_ALUNO.NU_NIRE,
                             CO_SIT_MAT = mat.CO_SIT_MAT,
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
                grdFreq.DataKeyNames = new string[] { "CO_ALU" };

                grdFreq.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
                grdFreq.DataBind(); // CARREGA OS VALORES DA GRID, DE ACORDO COM A LISTA RETORNADA
            }
            else
            {
                var lstAlunoMatricula = lista.ToList();
                grdFreq.DataKeyNames = new string[] { "CO_ALU" };

                grdFreq.DataSource = (lstAlunoMatricula.Count() > 0) ? lstAlunoMatricula : null;
                grdFreq.DataBind(); // CARREGA OS VALORES DA GRID, DE ACORDO COM A LISTA RETORNADA
            }
            #endregion
        }

        public class FrequSaida
        {
            public bool chk { get; set; }
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public string CO_ANO_MES_MAT { get; set; }
            public int NIRE { get; set; }
            public string CO_SIT_MAT { get; set; }
            public string CO_FLAG_FREQ_ALUNO { get; set; }
            public DateTime DT_FRE { get; set; }
            public string FL_HOMOL_FREQU { get; set; }
            public int ID_FREQU_ALUNO { get; set; }
            public int CO_ATIV_PROF_TUR { get; set; }
        }

        #endregion

        #region Métodos de campo

        //====> MÉTODO EXECUTADO QUANDO O ANO DE REFERÊNCIA É SELECIONADO
        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerie();
            CarregaTurma();
            CarregaMaterias();
        }

        //====> MÉTODO EXECUTADO QUANDO A MODALIDADE É SELECIONADA
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
            CarregaTurma();
            CarregaMaterias();
            CarregaTempo();
        }

        //====> MÉTODO EXECUTADO QUANDO A SÉRIE É SELECIONADA
        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
            CarregaTempo();
        }

        //====> MÉTODO EXECUTADO QUANDO A TURMA É SELECIONADA
        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            string turmaUnica = coTur != 0 ? TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coModu, coCur, coTur).CO_FLAG_RESP_TURMA : "N";

            CarregaMaterias();

            //---------> Velida se a turma á única
            if (turmaUnica == "S")
            {
                //-------------> Se a turma for única o sistema retorna somente a matéria de turma única, com sigla "MSR", e deixa o campo de matéria desabilitado
                int idMatMsr = TB107_CADMATERIAS.RetornaTodosRegistros().Where(m => m.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                int coMatMsr = TB02_MATERIA.RetornaTodosRegistros().Where(cm => cm.CO_EMP == LoginAuxili.CO_EMP && cm.CO_MODU_CUR == coModu && cm.CO_CUR == coCur && cm.ID_MATERIA == idMatMsr).FirstOrDefault().CO_MAT;
                ddlMateria.SelectedValue = coMatMsr.ToString();
                ddlMateria.Enabled = false;
            }
            else
            {
                ddlMateria.Enabled = true;
            }

            CarregaTempo();
            CarregarProfessores();
        }

        //====> MÉTODO EXECUTADO QUANDO O BIMESTRE É ALTERADO, SERVE PARA CARREGAR AUTOMATICAMENTE AS DATAS DA FREQUÊNCIA
        protected void ddlBimestre_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDatasFrequeBimestre();
        }

        //====> MÉTODO EXECUTADO QUANDO O USUÁRIO CLICA NO BOTÃO "PESQUISAR"
        protected void btnPesqGride_Click(object sender, EventArgs e)
        {
            PesquisaGrides();
        }

        //====> MÉTODO QUE DESTACA AS ATIVIDADES JA HOMOLOGADAS NA GRID
        protected void grdAtividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string flHomol = ((HiddenField)e.Row.FindControl("hdFlHomol")).Value; // PEGA O STATUS DE HOMOLOGAÇÃO DA ATIVIDADE

                if (flHomol == "S") // VALIDA SE A ATVIDADE JÁ ESTÁ HOMOLOGADA
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183); // ALTERA A COR DE FUNDO DA LINHA DA ATIVIDADE DA GRID

                DropDownList ddl = ((DropDownList)e.Row.FindControl("ddlTipoAtiv"));
                string tipo = (((HiddenField)e.Row.FindControl("hidTipoAtiv")).Value);
                CarregaTiposAtividades(ddl, tipo);
                ddl.SelectedValue = tipo;
            }
        }

        //====> MÉTODO QUE DESTACA AS FREQUÊNCIAS JA HOMOLOGADAS NA GRID
        protected void grdFreq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string flHomol = ((HiddenField)e.Row.FindControl("hdFlHomolA")).Value; // PEGA O STATUS DE HOMOLOGAÇÃO DA ATIVIDADE

                if (flHomol == "S") // VALIDA SE A ATVIDADE JÁ ESTÁ HOMOLOGADA
                {
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183); // ALTERA A COR DE FUNDO DA LINHA DA ATIVIDADE DA GRID
                    ((CheckBox)e.Row.FindControl("chkAlunoFalta")).Enabled = false;
                }

                //Marca caso seja falta e deixa desmarcado caso seja presença
                ((CheckBox)e.Row.FindControl("chkAlunoFalta")).Checked = (((HiddenField)e.Row.FindControl("hdCoFlagFreq")).Value == "N" ? true : false);
            }
        }

        //====> MÉTODO QUE ALTERA AS ATIVIDADES
        protected int AlteraAtividades()
        {
            try
            {
                int i = 0; // VARIÁVEL QUE CONTEM O RETORNO DA FUNÇÃO (A QUANTIDADE DE REGISTROS ALTERADOS)
                if (grdAtividades.Rows.Count > 0) // VERIFICA SE EXISTEM ATIVIDADES NA GRID DE ATIVIDADES
                {
                    int coAtv = 0; // VARIÁVEL QUE RECEBE O CÓDIGO DA ATIVIDADE DA GRID
                    string deRes = ""; // VARIÁVEL QUE RECEBE O RECUMO DA ATIVIDADE DA GRID
                    string deTema = ""; // VARIÁVEL QUE RECEBE TEMA
                    string TipoAtividade = ""; // TIPO DA ATIVIDADE
                    TB119_ATIV_PROF_TURMA tb119 = null; // VARIÁVEL QUE RECEBE O OBJETO TB119, QUE É A ATIVIDADE
                    TB142_LOG_ATIV_PROF_TURMA tb142 = null; // VARIÁVEL QUE RECEBE O OBJETO TB142, QUE É O LOG DE HOMOLOGAÇÃO DA ATIVIDADE
                    TB132_FREQ_ALU tb132 = null;// VARIÁVEL QUE RECEBE O OBJETO TB132, QUE É A FREQUÊNCIA
                    DateTime dtAtiv;
                    foreach (GridViewRow gvr in grdAtividades.Rows) // PASSA POR CADA REGISTRO DA GRID DE ATIVIDADES
                    {
                        tb119 = null; // ZERA O OBJETO TB119
                        tb142 = null; // ZERA O OBJETO TB142
                        if (((CheckBox)gvr.Cells[0].FindControl("ckSelect")).Checked) // VERIFICA SE A ATIVIDADE FOI SELECIONADA
                        {
                            if (((HiddenField)gvr.Cells[0].FindControl("hdFlHomol")).Value == "S")
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "A Atividade selecionada já está homologada, não é possível realizar alterações!!");
                                return 0;
                            }

                            coAtv = Convert.ToInt32(((HiddenField)gvr.Cells[1].FindControl("hdCoAtv")).Value); // COLOCA O CÓDIGO DA ATIVIDADE DA GRID NA VARIÁVEL COATV
                            deRes = ((TextBox)gvr.Cells[1].FindControl("txtResumo")).Text; // COLOCA O RESUMO DA ATIVIDADE DA GRID NA VARIÁVEL DERES
                            deTema = ((TextBox)gvr.Cells[3].FindControl("txtTema")).Text; // COLOCA O TEMA DA ATIVIDADE DA GRID NA VARIÁVEL DETEMA
                            TipoAtividade = ((DropDownList)gvr.Cells[3].FindControl("ddlTipoAtiv")).SelectedValue; // COLOCA O TIPO DA ATIVIDADE NA VARIÁVEL
                            tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtv); // RETORNA A ATIVIDADE DE ACORDO COM O CÓDIGO VINDO DA GRID
                            if (!DateTime.TryParse(((TextBox)gvr.Cells[2].FindControl("txtDataAtiv")).Text, out dtAtiv))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Data da atividade inválida.");
                                return 0;
                            }

                            if (string.IsNullOrEmpty(TipoAtividade)) //valida se foi selecionado um tipo de atividade
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar o tipo da atividade selecionada!");
                                return 0;
                            }

                            if (string.IsNullOrEmpty(deTema)) //valida se foi informado o tema da atividade
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar o Tema da atividade selecionada!");
                            }

                            if (string.IsNullOrEmpty(deRes)) //valida se foi informada a descrição da atividade
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso informar a descrição da atividade selecionada!");
                            }

                            if (tb119 != null) // VALIDA SE A ATIVIDADE EXISTE
                            {
                                tb142 = new TB142_LOG_ATIV_PROF_TURMA(); // CRIA UM NOVO OBJETO DE LOG DE HOMOLOGAÇÃO DA ATIVIDADE, TB142
                                //tb142.TB119_ATIV_PROF_TURMA = tb119;
                                tb142.CO_ATIV_PROF_TUR = coAtv;
                                tb142.CO_EMP_COL = LoginAuxili.CO_EMP;
                                tb142.CO_COL = LoginAuxili.CO_COL;
                                tb142.DT_LOG_ATIV = DateTime.Now;
                                tb142.DE_RES_ATIV_ANTES = tb119.DE_RES_ATIV;
                                tb142.DE_RES_ATIV_DEPOIS = deRes;
                                TB142_LOG_ATIV_PROF_TURMA.SaveOrUpdate(tb142, true); // SALVA O LOG

                                tb119.DE_RES_ATIV = deRes; // COLOCA O RESUMO ALTERADO NA ATIVIDADE
                                tb119.DE_TEMA_AULA = deTema;
                            }

                            tb119.DT_ATIV_REAL = dtAtiv;
                            tb119.TB273_TIPO_ATIVIDADE = TB273_TIPO_ATIVIDADE.RetornaPelaSigla(TipoAtividade);
                            tb119.DE_TEMA_AULA = deTema;
                            tb119.DE_RES_ATIV = deRes;
                            TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, true); // SALVA A ATIVIDADE
                            i++;
                        }
                    }
                }
                return i;
            }
            catch
            {
                throw;
            }
        }

        //====> MÉTODO QUE ALTERA AS FREQUÊNCIAS
        protected int AlteraFrequencias()
        {
            try
            {
                int i = 0; // VARIÁVEL QUE CONTEM O RETORNO DA FUNÇÃO (QUANTIDADE DE REGISTROS ALTERADOS)
                if (hidCoAtivSel.Value != "0")
                {
                    if (grdFreq.Rows.Count > 0) // VALIDA SE EXISTEM REGISTROS NA GRID DE FREQUÊNCIA
                    {
                        int idFrequ = 0; // VARIÁVEL QUE CONTEM O ID DA FREQUÊNCIA
                        TB132_FREQ_ALU tb132 = null; // VARIAVEL QUE RECEBE A FREQUÊNCIA
                        TB141_LOG_FREQU_ALUNO tb141 = null; // VARIAVEL QUE RECEBE O LOG DA FREQUÊNCIA
                        string flFrequ = ""; // VARIÁVEL QUE RECEBE A FREQUÊNCIA (S = PRESENÇA, N = FALTA)

                        //É possível alterar a data da atividade, e quando isso é feito, a data das frequências também precisam ser alteradas
                        #region Coleta Data Atividade
                        DateTime? dt = (DateTime?)null;
                        foreach (GridViewRow atv in grdAtividades.Rows)
                        {
                            if (((CheckBox)atv.FindControl("ckSelect")).Checked)
                            {
                                dt = DateTime.Parse(((TextBox)atv.FindControl("txtDataAtiv")).Text);
                                break;
                            }
                        }

                        #endregion

                        foreach (GridViewRow gvr in grdFreq.Rows) // PASSA POR TODAS AS LINHAS DA GRID DE FREQUÊNCIA
                        {
                            tb132 = null; // ZERA A VARIÁVEL DE FREQUÊNCIA
                            tb141 = null; // ZERA A VARIÁVEL DE LOG

                            if (((CheckBox)gvr.Cells[0].FindControl("ckSelect")).Checked) // VALIDA SE O CHECKBOX DA FREQUÊNCIA ESTAVA MARCADO
                            {
                                if (((HiddenField)gvr.Cells[3].FindControl("hdFlHomolA")).Value == "N") // ALTERA APENAS SE A FREQUÊNCIA NÃO ESTIVER HOMOLOGADA
                                {
                                    if (((HiddenField)gvr.Cells[3].FindControl("hdIdFrequ")).Value != "") // VALIDA SE EXISTE FREQUÊNCIA LANÇADA PARA O ALUNO
                                    {
                                        idFrequ = Convert.ToInt32(((HiddenField)gvr.Cells[3].FindControl("hdIdFrequ")).Value); // ATRIBUI O ID DA FREQUÊNCIA DA GRID NA VARIÁVEL "IDFREQU"
                                        flFrequ = ((CheckBox)gvr.Cells[3].FindControl("chkAlunoFalta")).Checked ? "N" : "S"; // ATRIBUI A FREQUÊNCIA DA GRID NA VARIÁVEL "FLFREQU"
                                        tb132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(idFrequ); // RETORNA O OBJETO COM OS DADOS DA FREQUÊNCIA A PARTIR DO ID
                                        if (tb132 != null) // VALIDA SE EXISTE FREQUÊNCIA LANÇADA
                                        {
                                            if (flFrequ != tb132.CO_FLAG_FREQ_ALUNO) // VALIDA SE A FREQUÊNCIA LANÇADA É DIFERENTE DA SELECIONADA NA GRID
                                            {
                                                // GRAVA O LOG DA FREQUÊNCIA
                                                tb141 = new TB141_LOG_FREQU_ALUNO();
                                                tb141.TB132_FREQ_ALU = tb132;
                                                tb141.CO_EMP_COL = LoginAuxili.CO_EMP;
                                                tb141.CO_COL = LoginAuxili.CO_COL;
                                                tb141.DT_LOG_FREQU = DateTime.Now;
                                                tb141.CO_FLAG_FREQ_ALUNO_ANTES = tb132.CO_FLAG_FREQ_ALUNO;
                                                tb141.CO_FLAG_FREQ_ALUNO_DEPOIS = flFrequ;
                                                TB141_LOG_FREQU_ALUNO.SaveOrUpdate(tb141, true);
                                            }
                                            tb132.CO_FLAG_FREQ_ALUNO = flFrequ;
                                            tb132.DT_FRE = (dt.HasValue ? dt.Value : DateTime.Now);
                                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                                        }
                                        else
                                        {
                                            int coModu = int.Parse(ddlModalidade.SelectedValue); // RETORNA A MODALIDADE SELECIONADA PELO USUÁRIO
                                            int coCur = int.Parse(ddlSerie.SelectedValue); // RETORNA A SÉRIE SELECIONADA PELO USUÁRIO
                                            int coTur = int.Parse(ddlTurma.SelectedValue); // RETORNA A TURMA SELECIONADA PELO USUÁRIO
                                            int coMat = Convert.ToInt32(((HiddenField)gvr.Cells[3].FindControl("hidCoMat")).Value); // RETORNA A METÉRIA SELECIONADA PELO USUÁRIO
                                            int coAlu = Convert.ToInt32(((HiddenField)gvr.Cells[3].FindControl("hdCoAluno")).Value); // RETORNA O CÓDIGO DO ALUNO DA GRID DE FREQUÊNCIA
                                            DateTime dtFreq = DateTime.Parse(txtDataFreq.Text); // RETORNA A DATA SELECIONADA PELO USUÁRIO
                                            string coBim = ddlBimestre.SelectedValue; // RETORNA O BIMESTRE SELECIONADO PELO USUÁRIO
                                            int nrTem = int.Parse(ddlTempo.SelectedValue); // RETORNA O TEMPO SELECIONADO PELO USUÁRIO

                                            // GRAVA UMA NOVA FREQUÊNCIA
                                            tb132 = new TB132_FREQ_ALU();
                                            tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coModu, coCur);
                                            tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                                            tb132.CO_FLAG_FREQ_ALUNO = flFrequ;
                                            tb132.CO_TUR = coTur;
                                            tb132.CO_MAT = coMat;
                                            tb132.DT_FRE = (dt.HasValue ? dt.Value : DateTime.Now);
                                            tb132.CO_COL = LoginAuxili.CO_COL;
                                            tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                                            tb132.CO_ATIV_PROF_TUR = 0;
                                            tb132.CO_ANO_REFER_FREQ_ALUNO = dt.HasValue ? dt.Value.Year : DateTime.Now.Year;
                                            tb132.CO_BIMESTRE = coBim;
                                            tb132.NR_TEMPO = nrTem;
                                            tb132.FL_HOMOL_FREQU = hidHomAtivSel.Value;
                                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);

                                            // GRAVA O LOG DA FREQUÊNCIA
                                            tb141 = new TB141_LOG_FREQU_ALUNO();
                                            tb141.TB132_FREQ_ALU = tb132;
                                            tb141.CO_EMP_COL = LoginAuxili.CO_EMP;
                                            tb141.CO_COL = LoginAuxili.CO_COL;
                                            tb141.DT_LOG_FREQU = DateTime.Now;
                                            tb141.CO_FLAG_FREQ_ALUNO_ANTES = null;
                                            tb141.CO_FLAG_FREQ_ALUNO_DEPOIS = flFrequ;
                                            TB141_LOG_FREQU_ALUNO.SaveOrUpdate(tb141, true);
                                        }
                                    }
                                    else
                                    {
                                        int coModu = int.Parse(ddlModalidade.SelectedValue); // RETORNA A MODALIDADE SELECIONADA PELO USUÁRIO
                                        int coCur = int.Parse(ddlSerie.SelectedValue); // RETORNA A SÉRIE SELECIONADA PELO USUÁRIO
                                        int coTur = int.Parse(ddlTurma.SelectedValue); // RETORNA A TURMA SELECIONADA PELO USUÁRIO
                                        int coMat = Convert.ToInt32(((HiddenField)gvr.Cells[3].FindControl("hidCoMat")).Value); // RETORNA A METÉRIA SELECIONADA PELO USUÁRIO
                                        int coAlu = Convert.ToInt32(((HiddenField)gvr.Cells[3].FindControl("hdCoAluno")).Value); // RETORNA O CÓDIGO DO ALUNO DA GRID DE FREQUÊNCIA
                                        DateTime dtFreq = DateTime.Parse(txtDataFreq.Text); // RETORNA A DATA SELECIONADA PELO USUÁRIO
                                        string coBim = ddlBimestre.SelectedValue; // RETORNA O BIMESTRE SELECIONADO PELO USUÁRIO
                                        int nrTem = int.Parse(ddlTempo.SelectedValue); // RETORNA O TEMPO SELECIONADO PELO USUÁRIO

                                        // GRAVA UMA NOVA FREQUÊNCIA
                                        tb132 = new TB132_FREQ_ALU();
                                        tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coModu, coCur);
                                        tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                                        tb132.CO_FLAG_FREQ_ALUNO = flFrequ;
                                        tb132.CO_TUR = coTur;
                                        tb132.CO_MAT = coMat;
                                        tb132.DT_FRE = (dt.HasValue ? dt.Value : DateTime.Now);
                                        tb132.CO_COL = LoginAuxili.CO_COL;
                                        tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;
                                        tb132.CO_ATIV_PROF_TUR = 0;
                                        tb132.CO_ANO_REFER_FREQ_ALUNO = dt.HasValue ? dt.Value.Year : DateTime.Now.Year;
                                        tb132.CO_BIMESTRE = coBim;
                                        tb132.NR_TEMPO = nrTem;
                                        tb132.FL_HOMOL_FREQU = hidHomAtivSel.Value;
                                        TB132_FREQ_ALU.SaveOrUpdate(tb132, true);

                                        // GRAVA O LOG DA FREQUÊNCIA
                                        tb141 = new TB141_LOG_FREQU_ALUNO();
                                        tb141.TB132_FREQ_ALU = tb132;
                                        tb141.CO_EMP_COL = LoginAuxili.CO_EMP;
                                        tb141.CO_COL = LoginAuxili.CO_COL;
                                        tb141.DT_LOG_FREQU = DateTime.Now;
                                        tb141.CO_FLAG_FREQ_ALUNO_ANTES = null;
                                        tb141.CO_FLAG_FREQ_ALUNO_DEPOIS = flFrequ;
                                        TB141_LOG_FREQU_ALUNO.SaveOrUpdate(tb141, true);
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Selecione uma atividade.");
                    return 0;
                }

                return i;
            }
            catch
            {
                throw;
            }
        }

        protected void chkMarcaTodosItensPendentes_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdFreq.HeaderRow.Cells[0].FindControl("chkMarcaTodosItensPendentes"));

            //Percorre alterando o checkbox de acordo com o selecionado em marcar todos
            foreach (GridViewRow li in grdFreq.Rows)
            {
                CheckBox ck = (((CheckBox)li.Cells[0].FindControl("ckSelect")));
                CheckBox ckfalta = (CheckBox)li.FindControl("chkAlunoFalta");

                //Apenas permite marcar as frequencias que não estiverem homologadas
                if (((HiddenField)li.Cells[3].FindControl("hdFlHomolA")).Value == "N") // VALIDA SE A ATVIDADE JÁ ESTÁ HOMOLOGADA
                    ck.Checked = ckfalta.Enabled = chkMarca.Checked;
            }
        }

        protected void ddlProfessor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
        }

        #endregion

        //====> CLASSE UTILIZADA PARA POPULAR A GRID DE FREQUÊNCIAS
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
        /// Verifica os campos obrigatorios
        /// </summary>
        /// <returns></returns>
        protected Boolean verificaCampos()
        {
            Boolean retorno = false;
            if (ddlAno.SelectedValue == "" || ddlAno.SelectedValue == "0")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe o ano.");
            }
            if (ddlModalidade.SelectedValue == "" || ddlModalidade.SelectedValue == "0")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a modalidade.");
            }
            if (ddlSerie.SelectedValue == "" || ddlSerie.SelectedValue == "0")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a serie.");
            }
            if (ddlTurma.SelectedValue == "" || ddlTurma.SelectedValue == "0")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a turma.");
            }
            if (ddlBimestre.SelectedValue == "")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe o bimestre.");
            }
            if (txtDataFreq.Text == "" || txtDataFreq.Text == "0")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a Data Inicial da Frequência.");
            }
            if (txtDataFreqAte.Text == "" || txtDataFreqAte.Text == "0")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a Data Final da Frequência.");
            }
            //if (ddlMateria.SelectedValue == "" || ddlMateria.SelectedValue == "0")
            //{
            //    retorno = true;
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a materia.");
            //}
            if (ddlTempo.SelectedValue == "")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe o tempo.");
            }
            return retorno;
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = ((CheckBox)sender);
            int numeroLinha = -1;
            if (grdAtividades.Rows.Count > 0)
            {
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    CheckBox marcado = (CheckBox)linha.FindControl("ckSelect");
                    CheckBox homol = (CheckBox)linha.FindControl("ckHomol");
                    HiddenField hiFlH = (HiddenField)linha.FindControl("hdFlHomol");
                    HiddenField hiCaA = (HiddenField)linha.FindControl("hdCoAtv");
                    TextBox txtRes = (TextBox)linha.FindControl("txtResumo");
                    TextBox txtTem = (TextBox)linha.FindControl("txtTema");
                    DropDownList txtTipo = (DropDownList)linha.FindControl("ddlTipoAtiv");
                    TextBox txtDtA = (TextBox)linha.FindControl("txtDataAtiv");

                    if (marcado.ClientID != atual.ClientID)
                    {
                        marcado.Checked = false;
                        txtRes.Enabled = false;
                        txtDtA.Enabled = false;
                        homol.Enabled = false;
                        txtTem.Enabled = false;
                        txtTipo.Enabled = false;
                    }
                    else
                    {
                        numeroLinha = linha.RowIndex;
                        hidCoAtivSel.Value = hiCaA.Value;
                        hidHomAtivSel.Value = homol.Checked ? "S" : "N";

                        //Comentada, pois nessa tela de manutenção do diário, o professor não poderá alterar ada
                        //que esteka homologado;
                        //homol.Enabled = marcado.Checked;

                        if ((hiFlH.Value == "N") || (hiFlH.Value == ""))
                        {
                            txtRes.Enabled = txtTem.Enabled = marcado.Checked;
                            txtDtA.Enabled = marcado.Checked;
                            txtTipo.Enabled = marcado.Checked;
                        }
                    }
                }
            }
            if (atual.Checked && numeroLinha > -1)
                CarregaGridFrequencia(int.Parse(grdAtividades.DataKeys[numeroLinha].Value.ToString()));
            else
            {
                grdFreq.DataSource = null;
                grdFreq.DataBind();
            }
        }

        protected void ckSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = ((CheckBox)sender);
            int numeroLinha = -1;
            if (grdAtividades.Rows.Count > 0)
            {
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    CheckBox marcado = (CheckBox)linha.FindControl("ckSelect");
                    CheckBox ckfalta = (CheckBox)linha.FindControl("chkAlunoFalta");

                    ckfalta.Enabled = marcado.Checked; // libera para lançar falta apenas se estiver marcado
                }
            }
            if (atual.Checked && numeroLinha > -1)
                CarregaGridFrequencia(int.Parse(grdAtividades.DataKeys[numeroLinha].Value.ToString()));
            else
            {
                grdFreq.DataSource = null;
                grdFreq.DataBind();
            }
        }

        protected void ckHomol_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = ((CheckBox)sender);
            hidHomAtivSel.Value = atual.Checked ? "S" : "N";
        }

        protected void lnkExcluItens_OnClick(object sender, EventArgs e)
        {
            int qtfreq = 0;
            int qtativ = 0;

            //Verifica se foi selecionada a exclusão da atividade mas não das frequências correspondentes
            if ((chkExcluiAtividades.Checked) && (!chkExcluiFrequencias.Checked))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não é possível excluir a atividade sem excluir as frequências correspondentes à ela, favor selecionar as frequências.");
                return;
            }

            if (chkExcluiFrequencias.Checked)
                qtfreq = ExcluiFrequencias();

            //Código de saída do método quando algo der errado
            if (qtfreq == 999)
                return;

            //Executa se o usuário tiver selecionado exclusão de atividades
            if (chkExcluiAtividades.Checked)
            {
                //Apenas se a exclusão das frequências estiver selecionada
                if (chkExcluiFrequencias.Checked)
                {
                    int qtFreSele = 0;
                    foreach (GridViewRow li in grdFreq.Rows)
                    {
                        if (((CheckBox)li.Cells[0].FindControl("ckselect")).Checked)
                            qtFreSele++;
                    }

                    //Verifica se a quantidade de frequencias selecionadas para exclusão é menor do que a quantidade total 
                    if (qtFreSele < grdFreq.Rows.Count)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Ao Excluir a atividade, é preciso excluir todas as frequências correspondentes, favor selecionar todas.");
                        return;
                    }
                }
                qtativ = ExcluiAtividades();
            }

            //Envia os dados do parâmetro de busca para uma variável em sessão, para recarregar os campos novamente depois de salvar o registro.
            var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerie.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                + ddlBimestre.SelectedValue + ";" + txtDataFreq.Text + ";" + txtDataFreqAte.Text + ";" + ddlMateria.SelectedValue + ";" + ddlTempo.SelectedValue;
            HttpContext.Current.Session["ParametrosBusca"] = parametros;

            AuxiliPagina.RedirecionaParaPaginaSucesso(qtativ + " Atividades excluídas, e " + qtfreq + " Frequências excluídas", Request.Url.AbsoluteUri);
        }
    }
}