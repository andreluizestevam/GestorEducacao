//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: ******
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 28/09/2016| Samira Lira                | Criada a funcionalidade


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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3530_CalculosMediasNotasAtividades.CalculoColegioInsef
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            bool reca = CarregaParametros();

            if (reca == false)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
                CarregaDisciplina();
                CarregaMedidas();
            }
            ;
        }


        //====> Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        private void CarregaMedidas()
        {
            var tipo = "T";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tipo, false, true);
        }


        private bool CarregaParametros()
        {
            bool persist = false;
            if (HttpContext.Current.Session["BuscaSuperior"] != null)
            {
                var parametros = HttpContext.Current.Session["BuscaSuperior"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var ano = par[0];
                    var modalidade = par[1];
                    var serieCurso = par[2];
                    var turma = par[3];
                    var coBime = par[4];
                    var Aluno = par[5];
                    var materia = par[6];

                    CarregaAnos();
                    ddlAno.SelectedValue = ano;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    CarregaMedidas();
                    ddlReferencia.SelectedValue = coBime;

                    CarregaAluno();
                    ddlAluno.SelectedValue = Aluno;

                    CarregaDisciplina();
                    ddlDisciplina.SelectedValue = materia;

                    persist = true;
                    HttpContext.Current.Session.Remove("BuscaSuperior");
                }
            }
            return persist;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgOcoMedia = false;

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                int intTrimestre = ddlReferencia.SelectedValue == "T1" ? 1 : ddlReferencia.SelectedValue == "T2" ? 2 : 3;
                string anoRef = ddlAno.SelectedValue;

                if (modalidade == 0 || serie == 0 || turma == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Curso e Turma devem ser informadas.");
                    return;
                }

                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    //Verifica se existiu ocorrência de nota
                    if (((TextBox)linha.Cells[6].FindControl("txtMDFinal")).Text != " - ")
                    {
                        flgOcoMedia = true;
                    }
                }

                if (!flgOcoMedia)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma média foi calculada");
                    return;
                }

                var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                tb25.TB82_DTCT_EMPReference.Load();

                DateTime dataLacto = DateTime.Now.Date;
                if (tb25.TB82_DTCT_EMP != null)
                {
                    if (intTrimestre == 1)
                    {
                        if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                        {
                            if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                return;
                            }
                        }
                    }
                    else if (intTrimestre == 2)
                    {
                        if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                        {
                            if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                        {
                            if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                                return;
                            }
                        }
                    }
                }

                //--------> Varre toda a grid de Busca
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    if (((TextBox)linha.Cells[6].FindControl("txtMDFinal")).Text != " - ")
                    {
                        //------------> Recebe o código do aluno
                        int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                        int coMat = int.Parse(((HiddenField)linha.Cells[2].FindControl("hidCO_MAT")).Value);

                        //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                        TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                                  where iTb079.CO_ALU == coAlu && iTb079.CO_ANO_REF == ddlAno.SelectedValue
                                                     && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                     && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma
                                                     && iTb079.CO_MAT == coMat
                                                  select iTb079).FirstOrDefault();

                        decimal? dcmMedia = decimal.Parse(((TextBox)linha.Cells[4].FindControl("txtMDFinal")).Text);

                        if (tb079 != null)
                        {
                            //----------------> Atribui o valor de média informado de acordo com o bimestre
                            switch (intTrimestre)
                            {
                                case 1:
                                    tb079.VL_NOTA_TRI1 =
                                    tb079.VL_MEDIA_TRI1 =
                                    tb079.VL_NOTA_TRI1_ORI = dcmMedia;
                                    tb079.VL_MEDIA_ANUAL = (((1 * dcmMedia) + (2 * (tb079.VL_MEDIA_TRI2.HasValue ? tb079.VL_MEDIA_TRI2 : 0)) + (3 * (tb079.VL_MEDIA_TRI3.HasValue ? tb079.VL_MEDIA_TRI3 : 0))) / 6);
                                    tb079.DT_LANC = DateTime.Now;
                                    break;
                                case 2:
                                    tb079.VL_NOTA_TRI2 =
                                    tb079.VL_MEDIA_TRI2 =
                                    tb079.VL_NOTA_TRI2_ORI = dcmMedia;
                                    tb079.VL_MEDIA_ANUAL = (((1 * (tb079.VL_MEDIA_TRI1.HasValue ? tb079.VL_MEDIA_TRI1 : 0)) + (2 * dcmMedia) + (3 * (tb079.VL_MEDIA_TRI3.HasValue ? tb079.VL_MEDIA_TRI3 : 0))) / 6);
                                    tb079.DT_LANC = DateTime.Now;
                                    break;
                                case 3:
                                    tb079.VL_NOTA_TRI3 =
                                    tb079.VL_MEDIA_TRI3 =
                                    tb079.VL_NOTA_TRI3_ORI = dcmMedia;
                                    tb079.VL_MEDIA_ANUAL = (((1 * (tb079.VL_MEDIA_TRI1.HasValue ? tb079.VL_MEDIA_TRI1 : 0)) + (2 * (tb079.VL_MEDIA_TRI2.HasValue ? tb079.VL_MEDIA_TRI2 : 0)) + (3 * dcmMedia)) / 6);
                                    tb079.DT_LANC = DateTime.Now;
                                    break;
                            }

                            TB079_HIST_ALUNO.SaveOrUpdate(tb079, true);
                        }
                    }
                }

                //Persiste as informações inseridas nos parâmetros para serem colocadas novamente depois que tudo for salvo, facilitando no momento de calcular diversas médias.
                var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                          + ddlReferencia.SelectedValue + ";" + ddlAluno.SelectedValue + ";" + ddlDisciplina.SelectedValue;
                HttpContext.Current.Session["BuscaSuperior"] = parametros;

                AuxiliPagina.RedirecionaParaPaginaSucesso("Registros salvos com sucesso.", Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível salvar a média calculada, por favor  tente novamente ou entre em contato com o suporte.");
            }

        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            string anoMesMat = ddlAno.SelectedValue;
            int intAno = int.Parse(ddlAno.SelectedValue.Trim());
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string Bim = ddlReferencia.SelectedValue;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int codMateria = materia != 0 ? TB02_MATERIA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, materia, serie).ID_MATERIA : 0;

            divGrid.Visible = true;

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_MAT equals tb43.CO_MAT
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb43.CO_EMP == tb079.CO_EMP
                             && tb43.CO_MAT == tb079.CO_MAT
                             && tb43.CO_CUR == tb079.CO_CUR
                             && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                             && tb079.CO_ANO_REF == anoMesMat
                             && tb079.CO_MODU_CUR == modalidade
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma
                             && (coAlu != 0 ? tb08.CO_ALU == coAlu : 0 == 0)
                             && (materia != 0 ? tb079.CO_MAT == materia : 0 == 0)
                             && tb43.ID_MATER_AGRUP == null
                             select new NotasAluno
                             {
                                 CO_ALU = tb08.TB07_ALUNO.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 CO_BIMESTRE = ddlReferencia.SelectedValue,
                                 CO_ANO = intAno,
                                 CO_EMP = tb08.CO_EMP,
                                 CO_CUR = tb08.CO_CUR,
                                 CO_MODU_CUR = tb08.TB44_MODULO.CO_MODU_CUR,
                                 ID_MATERIA = tb107.ID_MATERIA,
                                 noMateria = tb107.NO_MATERIA,
                                 CO_MAT = tb02.CO_MAT,
                                 VL_CALCU = tb02.VL_CALCU_MEDIA == null ? 1 : (decimal)tb02.VL_CALCU_MEDIA,
                                 FL_PROVA = tb02.FL_TESTE_PROVA == "S" ? true : false,
                                 FL_TRAB = tb02.FL_TRABA == "S" ? true : false,
                                 FL_PROJETO = tb02.FL_PROJE == "S" ? true : false,
                                 FL_CONCEITO = tb02.FL_CONCE == "S" ? true : false,
                                 FL_AVALI_ESPECI = tb02.FL_AVALI_ESPEC == "S" ? true : false,
                                 FL_AVALI_GLOBAL = tb02.FL_AVALI_GLOBA == "S" ? true : false,
                                 FL_SIMULADO = tb02.FL_SIMUL == "S" ? true : false,
                                 FL_ATIVI_AVALIA = tb02.FL_ATIVI_AVALI == "S" ? true : false,
                                 FL_ATIVI_PRATIC = tb02.FL_ATIVI_PRATI == "S" ? true : false,
                                 FL_REDACAO = tb02.FL_REDAC == "S" ? true : false,
                                 VL_MEDIA_TRI1 = tb079.VL_MEDIA_TRI1.HasValue ?  tb079.VL_MEDIA_TRI1 : 0,
                                 VL_MEDIA_TRI2 = tb079.VL_MEDIA_TRI2.HasValue ?  tb079.VL_MEDIA_TRI2 : 0,
                                 VL_MEDIA_TRI3 = tb079.VL_MEDIA_TRI3.HasValue ?  tb079.VL_MEDIA_TRI3 : 0
                             }).OrderBy(w => w.NO_ALU).ThenBy(y => y.noMateria).ToList();

            var notas = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                         join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                         where tb49.CO_BIMESTRE == Bim
                         && (coAlu != 0 ? tb49.TB07_ALUNO.CO_ALU == coAlu : 0 == 0)
                         && tb49.CO_ANO == intAno
                         && tb49.TB06_TURMAS.CO_TUR == turma
                         && (codMateria != 0 ? tb49.TB107_CADMATERIAS.ID_MATERIA == codMateria : 0 == 0)
                         select tb49).ToList();

            foreach (var res in resultado)
            {
                foreach (var n in notas)
                {
                    n.TB07_ALUNOReference.Load();
                    n.TB273_TIPO_ATIVIDADEReference.Load();
                    n.TB107_CADMATERIASReference.Load();

                    
                    if (res.CO_ALU == n.TB07_ALUNO.CO_ALU && n.TB107_CADMATERIAS.ID_MATERIA == res.ID_MATERIA)
                    {
                        switch (n.TB273_TIPO_ATIVIDADE.CO_SIGLA_ATIV)
                        {
                            case "PR":
                                if (n.CO_TIPO_ATIV.Trim() == "1")
                                {
                                    res.PV1 = n.VL_NOTA;
                                }
                                else
                                {
                                    res.PV2 = n.VL_NOTA;
                                }
                                break;
                            case "TB":
                                res.TB1 = n.VL_NOTA;
                                break;
                            case "PJ":
                                res.PJ1 = n.VL_NOTA;
                                break;
                            case "CT":
                                res.CT1 = n.VL_NOTA;
                                break;
                            case "AE":
                                res.AE1 = n.VL_NOTA;
                                break;
                            case "AG":
                                res.AG1 = n.VL_NOTA;
                                break;
                            case "SI":
                                res.SI1 = n.VL_NOTA;
                                break;
                            case "AA":
                                res.AA1 = n.VL_NOTA;
                                break;
                            case "AP":
                                res.AP1 = n.VL_NOTA;
                                break;
                            case "RD":
                                res.RD1 = n.VL_NOTA;
                                break;
                            default:
                                break;
                        }
                    }
                }

                res.limitaColunas(grdBusca);
            }

            divGrid.Visible = true;

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };

            grdBusca.DataSource = resultado.Count() > 0 ? resultado.OrderBy(p => p.NO_ALU) : null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.carregaSeriesGradeCurso(ddlSerieCurso, modalidade, anoGrade, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
        }

        private void CarregaAluno()
        {
            ddlAluno.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, LoginAuxili.CO_EMP, modalidade, serie, turma, ddlAno.SelectedValue, true);
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal para registro de ocorrência
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
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue;

            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == ano && tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.ID_MATER_AGRUP == null
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);

            if (res != null)
            {
                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataSource = res;
                ddlDisciplina.DataBind();
            }

            ddlDisciplina.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void btnPesqGride_Click(object sender, EventArgs e) //ok
        {
            if (ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == "" || ddlDisciplina.SelectedValue == ""
                || ddlAno.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ano, Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            CarregaGrid();
        }

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            /*public NotasAluno()
            {
                this.FL_PROVA = false;
                this.FL_TRAB = false;
                this.FL_PROJETO = false;
                this.FL_CONCEITO = false;
                this.FL_AVALI_ESPECI = false;
                this.FL_AVALI_GLOBAL = false;
                this.FL_SIMULADO = false;
                this.FL_ATIVI_AVALIA = false;
                this.FL_ATIVI_PRATIC = false;
                this.FL_REDACAO = false;
            }*/

            //Dados do Aluno
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public int NU_NIRE { get; set; }
            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }

            //Códigos de auxílio para buscas
            public string CO_BIMESTRE { get; set; }
            public int CO_ANO { get; set; }
            public int CO_EMP { get; set; }
            public int CO_CUR { get; set; }
            public int CO_MODU_CUR { get; set; }
            public int ID_MATERIA { get; set; }

            //Dados da Disciplina
            public string noMateria { get; set; }
            public int CO_MAT { get; set; }
            public decimal VL_CALCU { get; set; }

            public bool FL_PROVA { get; set; }
            public bool FL_TRAB { get; set; }
            public bool FL_PROJETO { get; set; }
            public bool FL_CONCEITO { get; set; }
            public bool FL_AVALI_ESPECI { get; set; }
            public bool FL_AVALI_GLOBAL { get; set; }
            public bool FL_SIMULADO { get; set; }
            public bool FL_ATIVI_AVALIA { get; set; }
            public bool FL_ATIVI_PRATIC { get; set; }
            public bool FL_REDACAO { get; set; }

            public void limitaColunas(GridView grdBusca)
            {
                foreach (DataControlField col in grdBusca.Columns)
                {
                    if (!this.FL_PROVA && (col.HeaderText.Trim() == "PRV/TST 1"))
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_PROVA && col.HeaderText.Trim() == "PRV/TST 2")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_TRAB && col.HeaderText.Trim() == "TRABALHO")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_PROJETO && col.HeaderText.Trim() == "PROJETO")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (col.HeaderText.Trim() == "CONCEITO")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_AVALI_ESPECI && col.HeaderText.Trim() == "AVAL ESPEC")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_AVALI_GLOBAL && col.HeaderText.Trim() == "AVAL GLOBAL")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_SIMULADO && col.HeaderText.Trim() == "SIMULADO")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_ATIVI_AVALIA && col.HeaderText.Trim() == "ATIV AVAL")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_ATIVI_PRATIC && col.HeaderText.Trim() == "ATIV PRÁT")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;
                    }
                    if (!this.FL_REDACAO && col.HeaderText.Trim() == "REDAÇÃO")
                    {
                        grdBusca.Columns[grdBusca.Columns.IndexOf(col)].Visible = false;

                    }
                }
            }

            // Notas inseridas na tb49
            public decimal PV1 { get; set; }
            public decimal PV2 { get; set; }
            public decimal TB1 { get; set; }
            public decimal PJ1 { get; set; }
            public decimal CT1 { get; set; }
            public decimal AE1 { get; set; }
            public decimal AG1 { get; set; }
            public decimal SI1 { get; set; }
            public decimal AA1 { get; set; }
            public decimal AP1 { get; set; }
            public decimal RD1 { get; set; }

            public decimal? VL_MEDIA_TRI1 { get; set; }
            public decimal? VL_MEDIA_TRI2 { get; set; }
            public decimal? VL_MEDIA_TRI3 { get; set; }

            public string PV1_V { get { return PV1 == 0 ? " - " : PV1.ToString(); } }
            public string PV2_V { get { return PV2 == 0 ? " - " : PV2.ToString(); } }
            public string TB1_V { get { return TB1 == 0 ? " - " : TB1.ToString(); } }
            public string PJ1_V { get { return PJ1 == 0 ? " - " : PJ1.ToString(); } }
            public string CT1_V { get { return CT1 == 0 ? " - " : CT1.ToString(); } }
            public string AE1_V { get { return AE1 == 0 ? " - " : AE1.ToString(); } }
            public string AG1_V { get { return AG1 == 0 ? " - " : AG1.ToString(); } }
            public string SI1_V { get { return SI1 == 0 ? " - " : SI1.ToString(); } }
            public string AA1_V { get { return AA1 == 0 ? " - " : AA1.ToString(); } }
            public string AP1_V { get { return AP1 == 0 ? " - " : AP1.ToString(); } }
            public string RD1_V { get { return RD1 == 0 ? " - " : RD1.ToString(); } }


            public string MDBIM
            {
                get
                {
                    decimal mediaFinal;

                    mediaFinal = (PV1 + PV2 + TB1 + PJ1 + CT1 + AE1 + AG1 + SI1 + +AA1 + AP1 + RD1) / VL_CALCU;

                    return (mediaFinal != 0 ? mediaFinal.ToString("#.##") : " - ");
                }
            }
        }
        #endregion
    }
}