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
// 12/12/2014| Maxwell Almeida            | Criada a funcionalidade para analisar e alterar o status da nota para quem está em conselho de classe

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3517_CalculoConselhoClasse
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
            divGrid.Visible = false;
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
            bool flAlunosEmConselho = false;

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série e Turma devem ser informadas.");
                return;
            }

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se existiu ocorrência de nota
                if (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value == "C")
                {
                    flAlunosEmConselho = true;
                    break;
                }
            }

            if (!flAlunosEmConselho)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma Aluno(a) em Conselho");
                return;
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se está de conselho
                if (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value == "C")
                {
                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                    int coMat = int.Parse(((HiddenField)linha.Cells[4].FindControl("hidCoMat")).Value);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu && iTb079.CO_ANO_REF == ddlAno.SelectedValue
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma
                                                 && iTb079.CO_MAT == coMat
                                              select iTb079).FirstOrDefault();

                    if (tb079 != null)
                    {
                        //----------------> Atribui o valor de STATUS informado de acordo com o trimestre
                        switch (ddlReferencia.SelectedValue)
                        {
                            case "B1":
                                tb079.FL_STATU_MEDIA_BIM1 = (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value);
                                break;
                            case "B2":
                                tb079.FL_STATU_MEDIA_BIM2 = (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value);
                                break;
                            case "B3":
                                tb079.FL_STATU_MEDIA_BIM3 = (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value);
                                break;
                            case "B4":
                                tb079.FL_STATU_MEDIA_BIM4 = (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value);
                                break;
                            case "FN":
                                tb079.FL_STATU_MEDIA_FINAL = (((HiddenField)linha.Cells[3].FindControl("hidCoStatus")).Value);
                                break;
                        }

                        if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                            return;
                        }
                    }
                }
            }

            //Persiste as informações inseridas nos parâmetros para serem colocadas novamente depois que tudo for salvo, facilitando no momento de calcular diversas médias.
            var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                      + ddlReferencia.SelectedValue + ";" + ddlAluno.SelectedValue + ";" + ddlDisciplina.SelectedValue;
            HttpContext.Current.Session["BuscaSuperior"] = parametros;

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;
            int intAno = int.Parse(ddlAno.SelectedValue.Trim());
            decimal baseMedia = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).MED_FINAL_CUR ?? 0;
            decimal baseConse = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).VL_NOTA_CONS ?? 0;

            //int idMateria = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
            //                 where tb02.CO_MAT == materia
            //                 select new { tb02.ID_MATERIA }).First().ID_MATERIA;

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
                             && tb08.CO_SIT_MAT == "A"
                             //Verifica se foi selecionada a opção de gerar a grid com as matérias agrupadoras ou não
                             //&& (chkCalculAgrupadora.Checked ? tb43.ID_MATER_AGRUP == null && tb08.CO_SIT_MAT == "A" : tb08.CO_SIT_MAT == "A")
                             select new NotasAluno
                             {
                                 CO_BIMESTRE = ddlReferencia.SelectedValue,
                                 ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU_RECEB = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 CO_MAT = tb079.CO_MAT,
                                 noMateria = tb107.NO_MATERIA,
                                 baseMedia = baseMedia,
                                 baseConse = baseConse,

                                 //Nota Bimestral
                                 NTAuxBim1 = tb079.VL_NOTA_BIM1,
                                 NTAuxBim2 = tb079.VL_NOTA_BIM2,
                                 NTAuxBim3 = tb079.VL_NOTA_BIM3,
                                 NTAuxBim4 = tb079.VL_NOTA_BIM4,
                                 NTAuxNotaFinal = tb079.VL_MEDIA_FINAL,
                                 //Recuperação
                                 NTAuxRecp1 = tb079.VL_RECU_BIM1,
                                 NTAuxRecp2 = tb079.VL_RECU_BIM2,
                                 NTAuxRecp3 = tb079.VL_RECU_BIM3,
                                 NTAuxRecp4 = tb079.VL_RECU_BIM4,
                                 NTAuxRecpFinal = tb079.VL_PROVA_FINAL,
                                 //Médias
                                 NTMediaBim1 = tb079.VL_MEDIA_BIM1,
                                 NTMediaBim2 = tb079.VL_MEDIA_BIM2,
                                 NTMediaBim3 = tb079.VL_MEDIA_BIM3,
                                 NTMediaBim4 = tb079.VL_MEDIA_BIM4,
                                 NTMediaFinal = tb079.VL_MEDIA_FINAL,
                                 //Conselho
                                 NTConseBim1 = tb079.VL_CONSE_BIM1,
                                 NTConseBim2 = tb079.VL_CONSE_BIM2,
                                 NTConseBim3 = tb079.VL_CONSE_BIM3,
                                 NTConseBim4 = tb079.VL_CONSE_BIM4,
                                 NTConseFinal = tb079.VL_CONSE_FINAL,
                                 //Status de todos os bimestres
                                 fl_status_bim1 = tb079.FL_STATU_MEDIA_BIM1,
                                 fl_status_bim2 = tb079.FL_STATU_MEDIA_BIM2,
                                 fl_status_bim3 = tb079.FL_STATU_MEDIA_BIM3,
                                 fl_status_bim4 = tb079.FL_STATU_MEDIA_BIM4,
                                 fl_status_final = tb079.FL_STATU_MEDIA_FINAL,

                             }).OrderBy(w => w.NO_ALU_RECEB).ThenBy(o => o.ordImp).ThenBy(y => y.noMateria).ToList();

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
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE == ddlAno.SelectedValue && tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.ID_MATER_AGRUP == null
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

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                string co_status = (((HiddenField)e.Row.Cells[3].FindControl("hidCoStatus")).Value);
                if (co_status == "F")
                    e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                else if (co_status == "C")
                    e.Row.BackColor = System.Drawing.Color.SandyBrown;
            }
        }

        protected void btnPesqGride_Click(object sender, EventArgs e)
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
            public int? ordImp { get; set; }
            public decimal baseMedia { get; set; }
            public decimal baseConse { get; set; }
            public string noMateria { get; set; }
            public int CO_MAT { get; set; }
            public string NO_ALU_RECEB { get; set; }
            public string NO_ALU
            {
                get
                {
                    return (this.NO_ALU_RECEB.Length > 29 ? this.NO_ALU_RECEB.Substring(0, 29) + "..." : NO_ALU_RECEB);
                }
            }
            public string CO_BIMESTRE { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }

            public string NOTA_VALID
            {
                get
                {
                    switch (this.CO_BIMESTRE)
                    {
                        case "B1":
                            return (this.NTAuxBim1.HasValue ? this.NTAuxBim1.Value.ToString("N2") : " - ");
                        case "B2":
                            return (this.NTAuxBim2.HasValue ? this.NTAuxBim2.Value.ToString("N2") : " - ");
                        case "B3":
                            return (this.NTAuxBim3.HasValue ? this.NTAuxBim3.Value.ToString("N2") : " - ");
                        case "B4":
                            return (this.NTAuxBim4.HasValue ? this.NTAuxBim4.Value.ToString("N2") : " - ");
                        case "FN":
                            return (this.NTAuxNotaFinal.HasValue ? this.NTAuxNotaFinal.Value.ToString("N2") : " - ");
                        default:
                            return " - ";
                    }
                }
            }
            public string RECUP_VALID
            {
                get
                {
                    switch (this.CO_BIMESTRE)
                    {
                        case "B1":
                            return (this.NTAuxRecp1.HasValue ? this.NTAuxRecp1.Value.ToString("N2") : " - ");
                        case "B2":
                            return (this.NTAuxRecp2.HasValue ? this.NTAuxRecp2.Value.ToString("N2") : " - ");
                        case "B3":
                            return (this.NTAuxRecp3.HasValue ? this.NTAuxRecp3.Value.ToString("N2") : " - ");
                        case "B4":
                            return (this.NTAuxRecp4.HasValue ? this.NTAuxRecp4.Value.ToString("N2") : " - ");
                        case "FN":
                            return (this.NTAuxRecpFinal.HasValue ? this.NTAuxRecpFinal.Value.ToString("N2") : " - ");
                        default:
                            return " - ";
                    }
                }
            }
            public string NTCON_VALID
            {
                get
                {
                    switch (this.CO_BIMESTRE)
                    {
                        case "B1":
                            return (this.NTConseBim1.HasValue ? this.NTConseBim1.Value.ToString("N2") : " - ");
                        case "B2":
                            return (this.NTConseBim2.HasValue ? this.NTConseBim2.Value.ToString("N2") : " - ");
                        case "B3":
                            return (this.NTConseBim3.HasValue ? this.NTConseBim3.Value.ToString("N2") : " - ");
                        case "B4":
                            return (this.NTConseBim4.HasValue ? this.NTConseBim4.Value.ToString("N2") : " - ");
                        case "FN":
                            return (this.NTConseFinal.HasValue ? this.NTConseFinal.Value.ToString("N2") : " - ");
                        default:
                            return " - ";
                    }
                }
            }
            public string MEDIA_VALID
            {
                get
                {
                    switch (this.CO_BIMESTRE)
                    {
                        case "B1":
                            return (this.NTMediaBim1.HasValue ? this.NTMediaBim1.Value.ToString("N2") : " - ");
                        case "B2":
                            return (this.NTMediaBim2.HasValue ? this.NTMediaBim2.Value.ToString("N2") : " - ");
                        case "B3":
                            return (this.NTMediaBim3.HasValue ? this.NTMediaBim3.Value.ToString("N2") : " - ");
                        case "B4":
                            return (this.NTMediaBim4.HasValue ? this.NTMediaBim4.Value.ToString("N2") : " - ");
                        case "FN":
                            return (this.NTMediaFinal.HasValue ? this.NTMediaFinal.Value.ToString("N2") : " - ");
                        default:
                            return " - ";
                    }
                }
            }
            public string STATUS_VALID
            {
                get
                {
                    switch (this.CO_BIMESTRE)
                    {
                        case "B1":
                            return (this.fl_status_bim1 != "F" ? (this.EM_CONSELHO == true ? "C" : this.fl_status_bim1) : this.fl_status_bim1);
                        case "B2":
                            return (this.fl_status_bim2 != "F" ? (this.EM_CONSELHO == true ? "C" : this.fl_status_bim2) : this.fl_status_bim2);
                        case "B3":
                            return (this.fl_status_bim3 != "F" ? (this.EM_CONSELHO == true ? "C" : this.fl_status_bim3) : this.fl_status_bim3);
                        case "B4":
                            return (this.fl_status_bim4 != "F" ? (this.EM_CONSELHO == true ? "C" : this.fl_status_bim4) : this.fl_status_bim4);
                        case "FN":
                            return (this.fl_status_final != "F" ? (this.EM_CONSELHO == true ? "C" : this.fl_status_final) : this.fl_status_final);
                        default:
                            return "A";
                    }
                }
            }

            public decimal? NTAuxBim1 { get; set; }
            public decimal? NTAuxBim2 { get; set; }
            public decimal? NTAuxBim3 { get; set; }
            public decimal? NTAuxBim4 { get; set; }
            public decimal? NTAuxNotaFinal { get; set; }

            public decimal? NTAuxRecp1 { get; set; }
            public decimal? NTAuxRecp2 { get; set; }
            public decimal? NTAuxRecp3 { get; set; }
            public decimal? NTAuxRecp4 { get; set; }
            public decimal? NTAuxRecpFinal { get; set; }

            public decimal? NTMediaBim1 { get; set; }
            public decimal? NTMediaBim2 { get; set; }
            public decimal? NTMediaBim3 { get; set; }
            public decimal? NTMediaBim4 { get; set; }
            public decimal? NTMediaFinal { get; set; }

            public decimal? NTConseBim1 { get; set; }
            public decimal? NTConseBim2 { get; set; }
            public decimal? NTConseBim3 { get; set; }
            public decimal? NTConseBim4 { get; set; }
            public decimal? NTConseFinal { get; set; }

            public string fl_status_bim1 { get; set; }
            public string fl_status_bim2 { get; set; }
            public string fl_status_bim3 { get; set; }
            public string fl_status_bim4 { get; set; }
            public string fl_status_final { get; set; }

            public string STATUS
            {
                get
                {
                    switch (this.STATUS_VALID)
                    {
                        case "C":
                            if ((this.NOTA_VALID == " - ") && (this.RECUP_VALID == " - "))
                                return "Não Lançada";
                            else
                                return "Em Conselho";
                        case "F":
                            if ((this.NOTA_VALID == " - ") && (this.RECUP_VALID == " - "))
                                return "Não Lançada";
                            else
                                return "Finalizado";
                        case "R":
                            if ((this.NOTA_VALID == " - ") && (this.RECUP_VALID == " - "))
                                return "Não Lançada";
                            else
                                return "Em Recuperação";
                        case "A":
                        default:
                            if ((this.NOTA_VALID == " - ") && (this.RECUP_VALID == " - "))
                                return "Não Lançada";
                            else
                                return "Em Aberto";
                    }
                }
            }
            public bool EM_CONSELHO
            {
                get
                {
                    decimal nota = (this.NOTA_VALID != " - " ? decimal.Parse(this.NOTA_VALID) : 0);
                    decimal recup = (this.RECUP_VALID != " - " ? decimal.Parse(this.RECUP_VALID) : 0);
                    decimal notaMaior = nota > recup ? nota : recup;

                    return notaMaior >= this.baseConse && notaMaior < this.baseMedia;
                }
            }

            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(7, '0'); } }
        }
        #endregion
    }
}