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
// 22/07/2014| Maxwell Almeida            | Criação da funcionalidade com o objetivo de homologar apenas as atividades do professor.
// 11/08/2014| Maxwell Almeida            | Inserção da regra responsável por homologar todas as frequências das atividades selecionadas, de acordo com desejo do usuário.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 16/11/2021| Artur Benevenuto Coelho    | Correção de filtro do dropdown de série;
//           |                            | Otimização do carregamento de turmas, prevenindo retorno nulo de consulta.
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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3315_HomolAtividadeLetivaProfe
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
                CarregaSerie(); // EXECUTA O MÉTODO QUE CARREGA AS SÉRIES
                CarregaMaterias(); // EXECUTA O MÉTODO QUE CARREGA AS MATÉRIAS
                CarregaTurma(); // EXECUTA O MÉTODO QUE CARREGA AS TURMAS
                CarregaTempo(); // EXECUTA O MÉTODO QUE CARREGA OS TEMPOS
                CarregaMedidas(); // EXECUTA O MÉTODO QUE CARREGA AS MEDIDAS

                txtDataFreq.Text = txtDataFreqAte.Text = DateTime.Now.ToString();
                CarregaInfosBusca();
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
                
                int qtdAtiv = 0;
                qtdAtiv = homolAtividades(); // EXECUTA A FUNÇÃO QUE HOMOLOGA AS ATIVIDADES
                
                string t = "";
                if (qtdAtiv == 1)
                {
                    t += qtdAtiv.ToString() + " atividade homologada ";
                }
                else
                {
                    t += qtdAtiv.ToString() + " atividades homologadas ";
                }

                //Envia os dados do parâmetro de busca para uma variável em sessão, para recarregar os campos novamente depois de salvar o registro.
                var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerie.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                    + ddlReferencia.SelectedValue + ";" + txtDataFreq.Text + ";" + txtDataFreqAte.Text + ";" + ddlMateria.SelectedValue + ";" + ddlTempo.SelectedValue;
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
            //ExcluiAtividade();
            AbreModalCliente();
        }

        protected void ExcluiAtividade()
        {
            int? materia = ddlMateria.SelectedValue != "" ? (int?)int.Parse(ddlMateria.SelectedValue) : null;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int tempoAula = ddlTempo.SelectedValue != "" ? int.Parse(ddlTempo.SelectedValue) : 0;
            string referencia = ddlReferencia.SelectedValue;
            decimal anoMesMat = decimal.Parse(ddlAno.SelectedValue);
            DateTime data = DateTime.Parse(txtDataFreq.Text);
            DateTime dataAte = DateTime.Parse(txtDataFreqAte.Text);
            int coAtivProfTur = 0;

            RegistroLog log = new RegistroLog();

            TB119_ATIV_PROF_TURMA tb119;
            TB132_FREQ_ALU tb132;

            //var lst = IQueryable;
            List<TB132_FREQ_ALU> res = new List<TB132_FREQ_ALU>(); ;

            var ctx = GestorEntities.CurrentContext;

            // Passa por todos os registros da grid de atividade
            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                // Valida se o registro foi selecionedo
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    coAtivProfTur = int.Parse(((HiddenField)linha.Cells[2].FindControl("hdCoAtv")).Value);
                    tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtivProfTur);

                    if (TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == tb119.CO_ATIV_PROF_TUR).Any())
                    {
                        res = (from fre in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where fre.TB01_CURSO.CO_CUR == serie
                                && fre.TB01_CURSO.TB44_MODULO.CO_MODU_CUR == modalidade
                                && fre.CO_ANO_REFER_FREQ_ALUNO == anoMesMat
                                && fre.CO_TUR == turma
                                && fre.DT_FRE >= data
                                && fre.DT_FRE <= dataAte
                                && (tempoAula == -1 ? true : tempoAula == 0 ? fre.NR_TEMPO == 0 : fre.NR_TEMPO == tempoAula)
                                && fre.CO_BIMESTRE == referencia
                                && (materia == null ? 0 == 0 : fre.CO_MAT == materia)
                                && (coAtivProfTur == null ? (fre.CO_ATIV_PROF_TUR == 0 || fre.CO_ATIV_PROF_TUR == null) : fre.CO_ATIV_PROF_TUR == coAtivProfTur)
                               select fre).ToList();

                        if (res.Count() > 0)
                        {
                            foreach (TB132_FREQ_ALU t in res)
                            {
                                if (GestorEntities.Delete(t) <= 0)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir frequência.");
                                    return;
                                }
                                tb132 = t;
                            }
                            tb132 = new TB132_FREQ_ALU();
                            log.RegistroLOG(tb132, RegistroLog.ACAO_DELETE);
                        }
                    }

                    if (GestorEntities.Delete(tb119) <= 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir atividade.");
                        return;
                    }

                    log.RegistroLOG(tb119, RegistroLog.ACAO_DELETE);
                }
                else
                {
                    //if (((HiddenField)linha.Cells[2].FindControl("hdFlHomol")).Value != "S")
                    //{
                    //    AuxiliPagina.EnvioMensagemErro(this, "A atividade selecionada não pode ser excluída porque esta homologada.");
                    //    return;
                    //}
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro(s) Excluídos com Sucesso", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Carrega as datas da frequência de acordo com o Bimestre selecionado no campo Referência
        /// </summary>
        private void CarregaDatasFrequeBimestre()
        {
            var ctx = GestorEntities.CurrentContext;
            string referencia = ddlReferencia.SelectedValue;
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

            switch (referencia)
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
                    var referencia = par[4];
                    var DataIniFreq = par[5];
                    var DataFimFreq = par[6];
                    var Materia = par[7];
                    var Tempo = par[8];

                    ddlAno.SelectedValue = ano;
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerie();
                    ddlSerie.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    ddlReferencia.SelectedValue = referencia;

                    txtDataFreq.Text = DataIniFreq;
                    txtDataFreqAte.Text = DataFimFreq;

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
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(w => w.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            if (ddlModalidade.Items.Count <= 0)
                ddlModalidade.Items.Clear();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        //====> MÉTODO QUE CARREGA AS SÉRIES
        private void CarregaSerie()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);

            if (coModu != 0)
            {
                ddlSerie.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                       where (coModu != 0 ? tb01.CO_MODU_CUR == coModu : 0 == 0)
                                         && tb01.CO_EMP == LoginAuxili.CO_EMP 
                                       select new { tb01.CO_CUR, tb01.NO_CUR });

                ddlSerie.DataTextField = "NO_CUR";
                ddlSerie.DataValueField = "CO_CUR";
                ddlSerie.DataBind();

                if (ddlSerie.Items.Count <= 0)
                    ddlSerie.Items.Clear();
            }

            ddlSerie.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        //====> MÉTODO QUE CARREGA AS TURMAS
        private void CarregaTurma()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);

            if (coCur != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                                       where (coModu != 0 ? tb06.CO_MODU_CUR == coModu : 0 == 0)
                                       && (coCur != 0 ? tb06.CO_CUR == coCur : 0 == 0)
                                       select new { tb06.CO_TUR, tb129.NO_TURMA });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();

                if (ddlTurma.Items.Count <= 0)
                {
                    ddlTurma.Items.Clear();
                }
            }

            ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        //====> MÉTODO QUE CARREGA AS MATÉRIAS
        private void CarregaMaterias()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string turmaUnica = coTur != 0 ? TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coModu, coCur, coTur).CO_FLAG_RESP_TURMA : "N";

            //---------> Verifica se a turma seleciona é única
            if (turmaUnica == "S")
            {
                //-------------> Se a turma for única o sistema retornará somente a matéria de turma única, com sigla "MSR"
                ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, coModu, coCur)
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         where tb107.NO_SIGLA_MATERIA == "MSR"
                                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);
            }
            else
            {
                //-------------> Se a turma não for única o sistema retornará as matérias normalmente
                ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, coModu, coCur)
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);
            }

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "CO_MAT";
            ddlMateria.DataBind();

            if (ddlMateria.Items.Count <= 0)
                ddlMateria.Items.Clear();

            ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
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

            var res = (from tb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
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
            ddlTempo.DataSource = res;
            ddlTempo.DataBind();

            ddlTempo.Items.Insert(0, new ListItem("Não definido", "0"));
            ddlTempo.Items.Insert(0, new ListItem("Todos", "-1"));
            //ddlTempo.SelectedValue = "-1";
        }

        //====> MÉTODO QUE CARREGA A GRID DE ATIVIDADES
        private void CarregaGridAtividade()
        {
            string coAnoRefPla = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue.Trim() : "";
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
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
                                 CO_TEMPO_ATIV = tb119.CO_TEMPO_ATIV
                             }).OrderByDescending(p => p.DT_ATIV_REAL).Distinct();

            divGrid.Visible = true;

            grdAtividades.DataKeyNames = new string[] { "CO_ATIV_PROF_TUR" };

            grdAtividades.DataSource = resultado;
            grdAtividades.DataBind();
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
            //if (DateTime.Parse(dtFre) > DateTime.Now)
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "Data da Frequência não pode ser uma data futura.");
            //    return;
            //}

            //====> EXECUTA O MÉTODO QUE CARREGA AS ATIVIDADES NA GRID, DE ACORDO COM O FILTRO SELECIONADO
            CarregaGridAtividade();
        }

        public class AtivSaida
        {
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
            var objTb06 = coTur != 0 ? TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coModu, coCur, coTur) : null;
            string turmaUnica = objTb06 != null ? objTb06.CO_FLAG_RESP_TURMA : "N";

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
        }

        //====> MÉTODO EXECUTADO QUANDO O BIMESTRE É ALTERADO, SERVE PARA CARREGAR AUTOMATICAMENTE AS DATAS DA FREQUÊNCIA
        protected void ddlReferencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDatasFrequeBimestre();
        }

        //====> MÉTODO EXECUTADO QUANDO A MATÉRIA É SELECIONADA
        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTempo();
        }

        //====> MÉTODO EXECUTADO QUANDO O TEMPO DA FREQUÊNCIA E SELECIONADO
        protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                {
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183); // ALTERA A COR DE FUNDO DA LINHA DA ATIVIDADE DA GRID
                }
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
                }
                ((DropDownList)e.Row.FindControl("ddlFreq")).SelectedValue = ((HiddenField)e.Row.FindControl("hdCoFlagFreq")).Value;
            }
        }

        //====> MÉTODO QUE EXECUTA A HOMOLOGAÇÃO DAS ATIVIDADES
        protected int homolAtividades()
        {
            try
            {
                int i = 0; // VARIÁVEL QUE CONTEM O RETORNO DA FUNÇÃO (A QUANTIDADE DE REGISTROS ALTERADOS)
                if (grdAtividades.Rows.Count > 0) // VERIFICA SE EXISTEM ATIVIDADES NA GRID DE ATIVIDADES
                {
                    int coAtv = 0; // VARIÁVEL QUE RECEBE O CÓDIGO DA ATIVIDADE DA GRID
                    string deRes = ""; // VARIÁVEL QUE RECEBE O RECUMO DA ATIVIDADE DA GRID
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
                            coAtv = Convert.ToInt32(((HiddenField)gvr.Cells[1].FindControl("hdCoAtv")).Value); // COLOCA O CÓDIGO DA ATIVIDADE DA GRID NA VARIÁVEL COATV
                            deRes = ((TextBox)gvr.Cells[1].FindControl("txtResumo")).Text; // COLOCA O RESUMO DA ATIVIDADE DA GRID NA VARIÁVEL DERES
                            tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtv); // RETORNA A ATIVIDADE DE ACORDO COM O CÓDIGO VINDO DA GRID
                            if (!DateTime.TryParse(((TextBox)gvr.Cells[2].FindControl("txtDataAtiv")).Text, out dtAtiv))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Data da atividade inválida.");
                                return 0;
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
                            }

                            tb119.DT_ATIV_REAL = dtAtiv;
                            if (((CheckBox)gvr.Cells[0].FindControl("ckHomol")).Checked)
                            {
                                tb119.FL_HOMOL_ATIV = "S";
                                tb119.FL_HOMOL_DIARIO = "S";

                                //Verifica se o usuário marcou a opção de homologar frequências das atividades.
                                if (chkHomolFreq.Checked == true)
                                {
                                    //Retorna uma lista das frequências da atividade corrente.
                                    var resFreq = TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == coAtv).ToList();
                                    if (resFreq.Count > 0)
                                    {
                                        //Instancia um objeto de salva ele com flag de homologado para todas as frequências relacionadas à atividade corrente.
                                        foreach (var lis in resFreq)
                                        {
                                            TB132_FREQ_ALU t132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(lis.ID_FREQ_ALUNO);
                                            t132.FL_HOMOL_FREQU = "S";
                                            TB132_FREQ_ALU.SaveOrUpdate(t132, true);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (tb119.FL_HOMOL_ATIV == "S")
                                {
                                    tb119.FL_HOMOL_ATIV = "N";
                                    tb119.FL_HOMOL_DIARIO = "N";
                                }
                            }
                            TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, true); // SALVA A ATIVIDADE
                            i++;
                        }
                    }
                    RegistroLog log = new RegistroLog();
                    log.RegistroLOG(tb119, RegistroLog.ACAO_EDICAO);
                }
                return i;
            }
            catch
            {
                throw;
            }
        }

        //====> MÉTODO QUE RETIRA A HOMOLOGAÇÃO DA ATIVIDADE
        protected int desHomolAtividade()
        {
            try
            {
                int i = 0; // VARIÁVEL QUE CONTEM O RETORNO DA FUNÇÃO (A QUANTIDADE DE REGISTROS ALTERADOS)
                if (grdAtividades.Rows.Count > 0) // VERIFICA SE EXISTEM ATIVIDADES NA GRID DE ATIVIDADES
                {
                    int coAtv = 0; // VARIÁVEL QUE RECEBE O CÓDIGO DA ATIVIDADE DA GRID
                    string deRes = ""; // VARIÁVEL QUE RECEBE O RECUMO DA ATIVIDADE DA GRID
                    TB119_ATIV_PROF_TURMA tb119 = null; // VARIÁVEL QUE RECEBE O OBJETO TB119, QUE É A ATIVIDADE
                    TB142_LOG_ATIV_PROF_TURMA tb142 = null; // VARIÁVEL QUE RECEBE O OBJETO TB142, QUE É O LOG DE HOMOLOGAÇÃO DA ATIVIDADE
                    TB132_FREQ_ALU tb132 = null;// VARIÁVEL QUE RECEBE O OBJETO TB132, QUE É A FREQUÊNCIA

                    foreach (GridViewRow gvr in grdAtividades.Rows) // PASSA POR CADA REGISTRO DA GRID DE ATIVIDADES
                    {
                        tb119 = null; // ZERA O OBJETO TB119
                        tb142 = null; // ZERA O OBJETO TB142
                        if (((CheckBox)gvr.Cells[0].FindControl("ckHomol")).Checked) // VERIFICA SE A ATIVIDADE FOI SELECIONADA
                        {
                            coAtv = Convert.ToInt32(((HiddenField)gvr.Cells[1].FindControl("hdCoAtv")).Value); // COLOCA O CÓDIGO DA ATIVIDADE DA GRID NA VARIÁVEL COATV
                            deRes = ((TextBox)gvr.Cells[1].FindControl("txtResumo")).Text; // COLOCA O RESUMO DA ATIVIDADE DA GRID NA VARIÁVEL DERES
                            tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtv); // RETORNA A ATIVIDADE DE ACORDO COM O CÓDIGO VINDO DA GRID
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
                            }
                            tb119.FL_HOMOL_ATIV = "S";
                            tb119.FL_HOMOL_DIARIO = "S";

                            var res = TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == tb119.CO_ATIV_PROF_TUR).Select(s => s.ID_FREQ_ALUNO).ToList();

                            foreach (int id in res)
                            {
                                tb132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(id);

                                tb132.FL_HOMOL_FREQU = "S";
                                TB132_FREQ_ALU.SaveOrUpdate(tb132, true);
                            }

                            TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, true); // SALVA A ATIVIDADE
                            i++;
                        }
                        else
                        {
                            coAtv = Convert.ToInt32(((HiddenField)gvr.Cells[1].FindControl("hdCoAtv")).Value); // COLOCA O CÓDIGO DA ATIVIDADE DA GRID NA VARIÁVEL COATV
                            deRes = ((TextBox)gvr.Cells[1].FindControl("txtResumo")).Text; // COLOCA O RESUMO DA ATIVIDADE DA GRID NA VARIÁVEL DERES
                            tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtv); // RETORNA A ATIVIDADE DE ACORDO COM O CÓDIGO VINDO DA GRID
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
                            }
                            tb119.FL_HOMOL_ATIV = "N";
                            tb119.FL_HOMOL_DIARIO = "N";

                            TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, true); // SALVA A ATIVIDADE
                            i++;
                        }
                    }
                    RegistroLog log = new RegistroLog();
                    log.RegistroLOG(tb119, RegistroLog.ACAO_EDICAO);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
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
        /// Marca todos os CheckBox da Grid de Atividades, caso o clique no "Marcar Todos" seja para checkar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox todos = (CheckBox)sender;
            if (grdAtividades.Rows.Count > 0)
            {
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    CheckBox marcado = (CheckBox)linha.FindControl("ckSelect");
                    TextBox txtRes = (TextBox)linha.FindControl("txtResumo");
                    TextBox txtDtA = (TextBox)linha.FindControl("txtDataAtiv");
                    CheckBox homol = (CheckBox)linha.FindControl("ckHomol");
                    homol.Enabled = txtRes.Enabled = txtDtA.Enabled = marcado.Checked = marcado.Checked = todos.Checked;

                }
            }
        }

        /// <summary>
        /// Exclui todas as atividades selecionadas e as frequências se for recebido true no parâmetro
        /// </summary>
        /// <param name="excluiFrequencias"></param>
        /// <returns></returns>
        private void RealizaExclusoes(bool excluiFrequencias, bool excluiAtividades)
        {
            RegistroLog log = new RegistroLog();
            int qtAtiviExclu = 0;
            int qtFreqExclu = 0;
            foreach (GridViewRow linha in grdAtividades.Rows)
            {
                //exclui as atividades selecionadas
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    int coAtivProfTur = int.Parse(((HiddenField)linha.Cells[3].FindControl("hdCoAtv")).Value);
                    TB119_ATIV_PROF_TURMA tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coAtivProfTur);

                    //apenas se recebido como parâmetro true
                    if (excluiFrequencias)
                    {
                        //Retorna uma lista das frequências da atividade corrente.
                        var resFreq = TB132_FREQ_ALU.RetornaTodosRegistros().Where(w => w.CO_ATIV_PROF_TUR == coAtivProfTur).ToList();
                        if (resFreq.Count > 0)
                        {
                            //Instancia o objeto referente à cada item de frequência e excluir um a um os itens relacionados à atividade em questão
                            foreach (var lis in resFreq)
                            {
                                TB132_FREQ_ALU t132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(lis.ID_FREQ_ALUNO);
                                t132.FL_HOMOL_FREQU = "S";
                                qtFreqExclu++;
                                TB132_FREQ_ALU.Delete(t132, true);

                                log.RegistroLOG(t132, RegistroLog.ACAO_DELETE);
                            }
                        }
                    }

                    if (excluiAtividades)
                    {
                        TB119_ATIV_PROF_TURMA.Delete(tb119, true);
                        qtAtiviExclu++;

                        log.RegistroLOG(tb119, RegistroLog.ACAO_DELETE);
                    }
                }
            }
            HttpContext.Current.Session["qtExcluAtiv"] = qtAtiviExclu;
            HttpContext.Current.Session["qtExcluFreq"] = qtFreqExclu;
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
            if (ddlReferencia.SelectedValue == "")
            {
                retorno = true;
                AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a referência.");
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
            if (grdAtividades.Rows.Count > 0)
            {
                foreach (GridViewRow linha in grdAtividades.Rows)
                {
                    CheckBox marcado = (CheckBox)linha.FindControl("ckSelect");
                    CheckBox homol = (CheckBox)linha.FindControl("ckHomol");
                    HiddenField hiFlH = (HiddenField)linha.FindControl("hdFlHomol");
                    HiddenField hiCaA = (HiddenField)linha.FindControl("hdCoAtv");
                    TextBox txtRes = (TextBox)linha.FindControl("txtResumo");
                    TextBox txtDtA = (TextBox)linha.FindControl("txtDataAtiv");
                    
                    //if (marcado.Checked)
                    homol.Enabled = txtRes.Enabled = txtDtA.Enabled = marcado.Checked;
                    //if (marcado.ClientID != atual.ClientID)
                    //{
                    //    //marcado.Checked = false;
                    //    txtRes.Enabled = false;
                    //    txtDtA.Enabled = false;
                    //    homol.Enabled = false;
                    //}
                    //else
                    //{
                    //    numeroLinha = linha.RowIndex;
                    //    hidCoAtivSel.Value = hiCaA.Value;
                    //    hidHomAtivSel.Value = homol.Checked ? "S" : "N";

                    //    homol.Enabled = marcado.Checked;

                    //    if ((hiFlH.Value == "N") || (hiFlH.Value == ""))
                    //    {
                    //        txtRes.Enabled = marcado.Checked;
                    //        txtDtA.Enabled = marcado.Checked;
                    //    }
                    //}
                }
            }
        }

        //protected void ckHomol_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox atual = ((CheckBox)sender);
        //    hidHomAtivSel.Value = atual.Checked ? "S" : "N";
        //}

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

            //Código de saída do método quando algo der errado
            if (qtfreq == 999)
                return;

            //Executa se o usuário tiver selecionado exclusão
            RealizaExclusoes(chkExcluiFrequencias.Checked, chkExcluiAtividades.Checked);

            //Coleta as quantidades
            string qtAt = HttpContext.Current.Session["qtExcluAtiv"].ToString();
            string qtFr = HttpContext.Current.Session["qtExcluFreq"].ToString();
            qtativ = (!string.IsNullOrEmpty(qtAt) ? int.Parse(qtAt) : 0);
            qtfreq = (!string.IsNullOrEmpty(qtFr) ? int.Parse(qtFr) : 0);
            HttpContext.Current.Session.Remove("qtExcluAtiv");
            HttpContext.Current.Session.Remove("qtExcluFreq");

            //Envia os dados do parâmetro de busca para uma variável em sessão, para recarregar os campos novamente depois de salvar o registro.
            var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerie.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                + ddlReferencia.SelectedValue + ";" + txtDataFreq.Text + ";" + txtDataFreqAte.Text + ";" + ddlMateria.SelectedValue + ";" + ddlTempo.SelectedValue;
            HttpContext.Current.Session["ParametrosBusca"] = parametros;

            AuxiliPagina.RedirecionaParaPaginaSucesso(qtativ + " Atividades excluídas, e " + qtfreq + " Frequências excluídas", Request.Url.AbsoluteUri);
        }
    }
}