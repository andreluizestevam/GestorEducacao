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
// 10/09/2014| Maxwell Almeida            | Criada a funcionalidade


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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3507_CalculoMediaEspecificoAtivi
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
            }
            divGrid.Visible = divLegenda.Visible = false;
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

                    ddlBimestre.SelectedValue = coBime;

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
            bool flgOcoMedia = false;

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int intBimestre = ddlBimestre.SelectedValue == "B1" ? 1 : ddlBimestre.SelectedValue == "B2" ? 2 : ddlBimestre.SelectedValue == "B3" ? 3 : 4;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série e Turma devem ser informadas.");
                return;
            }

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se existiu ocorrência de nota
                if (((TextBox)linha.Cells[7].FindControl("txtMDFinal")).Text != " - ")
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
                if (intBimestre == 1)
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (intBimestre == 2)
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (intBimestre == 3)
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
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
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Média do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((TextBox)linha.Cells[7].FindControl("txtMDFinal")).Text != " - ")
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

                    decimal dcmMedia = decimal.Parse(((TextBox)linha.Cells[7].FindControl("txtMDFinal")).Text);

                    if (tb079 != null)
                    {
                        //----------------> Atribui o valor de média informado de acordo com o bimestre
                        switch (intBimestre)
                        {
                            case 1: tb079.VL_NOTA_BIM1 = dcmMedia;
                                tb079.VL_NOTA_BIM1_ORI = dcmMedia;
                                break;
                            case 2: tb079.VL_NOTA_BIM2 = dcmMedia;
                                tb079.VL_NOTA_BIM2_ORI = dcmMedia;
                                break;
                            case 3: tb079.VL_NOTA_BIM3 = dcmMedia;
                                tb079.VL_NOTA_BIM3_ORI = dcmMedia;
                                break;
                            case 4: tb079.VL_NOTA_BIM4 = dcmMedia;
                                tb079.VL_NOTA_BIM4_ORI = dcmMedia;
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
                      + ddlBimestre.SelectedValue + ";" + ddlAluno.SelectedValue + ";" + ddlDisciplina.SelectedValue;
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
            string Bim = ddlBimestre.SelectedValue;
            decimal baseMedia = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie).MED_FINAL_CUR ?? 0;

            divGrid.Visible = divLegenda.Visible = true;

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
                                 NO_ALU_RECEB = tb08.TB07_ALUNO.NO_ALU,
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 CO_BIMESTRE = ddlBimestre.SelectedValue,
                                 CO_ANO = intAno,
                                 CO_EMP = tb08.CO_EMP,
                                 CO_CUR = tb08.CO_CUR,
                                 CO_MODU_CUR = tb08.TB44_MODULO.CO_MODU_CUR,
                                 ID_MATERIA = tb107.ID_MATERIA,
                                 ordImp = tb43.CO_ORDEM_IMPRE ?? 20,
                                 noMateria = tb107.NO_MATERIA,
                                 CO_MAT = tb02.CO_MAT,
                                 FL_AGRUPADORA = tb43.FL_DISCI_AGRUPA,
                                 NT_MAX = tb43.VL_NOTA_MAXIM,
                                 NT_MAX_ATIV = tb43.VL_NOTA_MAXIM_ATIVI,
                                 NT_MAX_PROV = tb43.VL_NOTA_MAXIM_PROVA,
                                 NT_MAX_SIMU = tb43.VL_NOTA_MAXIM_SIMUL,
                                 FL_NOTA1_MEDIA = tb43.FL_NOTA1_MEDIA,
                             }).OrderBy(w => w.NO_ALU_RECEB).ThenBy(o => o.ordImp).ThenBy(y => y.noMateria).ToList();

            divGrid.Visible = divLegenda.Visible = true;

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
            divGrid.Visible = divLegenda.Visible = false;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = divLegenda.Visible = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = divLegenda.Visible = false;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = divLegenda.Visible = false;
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
            /*<=========================== REGRAS DE CÁLCULO DA MÉDIA DO COLÉGIO ESPECÍFICO =====================>
             (n1 + n2 + n3 / 3) + na

              n1 = Nota1 (prova nota 1)
              n2 = Nota2 (prova nota 2)
              n3 = Simulado (Soma Simulados da Disciplina e divide pela quantidade)
              na = Nota Atividade (Soma as notas de atividade da disciplina)
              
              
              Disciplinas Agrupadoras
              n1 = Soma todas as provas nota 1 lançadas para as disciplinas filhas
              n2 = Soma todas as provas nota 2 lançadas para as disciplinas filhas
              n3 = Simulado (Soma Simulados da Disciplina e divide pela quantidade)
              na = Soma todas as notas de atividades lançadas para as disciplinas filhas
             */

            //Dados do Aluno
            public int CO_ALU { get; set; }
            public string NO_ALU_RECEB { get; set; }
            public string NO_ALU
            {
                get
                {
                    //return (this.NO_ALU_RECEB.Length > 29 ? this.NO_ALU_RECEB.Substring(0, 29) + "..." : NO_ALU_RECEB);
                    return this.NO_ALU_RECEB;
                }
            }
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
            public int? ordImp { get; set; }
            public string noMateria { get; set; }
            public int CO_MAT { get; set; }
            public string FL_AGRUPADORA { get; set; }
            public string FL_AGRUPADORA_V
            {
                get
                {
                    //Feito dessa forma pois nem todos os itens tem informado se é ou não agrupadora
                    return (this.FL_AGRUPADORA == "S" ? "S" : "N");
                }
            }
            public decimal? NT_MAX { get; set; }
            public decimal? NT_MAX_PROV { get; set; }
            public decimal? NT_MAX_SIMU { get; set; }
            public decimal? NT_MAX_ATIV { get; set; }
            public string FL_NOTA1_MEDIA { get; set; }

            //Dados das notas
            public string NOTA1
            {
                get
                {
                    //Tratamento feito quando a disciplina em questao e agrupadora
                    if (this.FL_AGRUPADORA_V == "S")
                    {
                        string ano = this.CO_ANO.ToString();

                        //faz uma verificacao para identificar quais as disciplinas filhas da disciplina em questao
                        var liagrup = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                       where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                                       && tb43.CO_CUR == this.CO_CUR
                                       && tb43.ID_MATER_AGRUP == this.CO_MAT
                                       && tb43.CO_ANO_GRADE == ano
                                       select new { tb107.ID_MATERIA }).ToList();

                        decimal notaAtividade = 0;
                        decimal Media = 0;
                        int count = 0;
                        //Percorre a lista de disciplinas agrupadas contabilizando as notas
                        foreach (var li in liagrup)
                        {
                            var lisAgruAtiv = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                               join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                               where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                               && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                               && tb49.CO_ANO == this.CO_ANO
                                               && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                               && tb49.TB107_CADMATERIAS.ID_MATERIA == li.ID_MATERIA
                                               && tb273.CO_SIGLA_ATIV == "PR"
                                               && tb49.CO_REFER_NOTA == "N1"
                                               select new
                                               {
                                                   tb273.CO_SIGLA_ATIV,
                                                   tb49.VL_NOTA,
                                               }).ToList();

                            foreach (var l in lisAgruAtiv)
                            {
                                notaAtividade += l.VL_NOTA;
                                count++;
                            }
                        }

                        //faz o calculo final para atribuir a nota das atividades das disciplinas agrupadas
                        if (count > 0)
                        {
                            //Media = notaAtividade / count;
                            Media = notaAtividade;
                            if (this.NT_MAX_PROV.HasValue)
                            {
                                Media = (Media >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : Media);
                                return Media.ToString("N2");
                            }
                            else
                                return Media.ToString("N2");
                        }
                        else
                            return " - ";
                    }
                    //Caso nao seja agrupadora, realiza o calculo das notas da maneira convencional
                    else
                    {
                        //Faz o cálculo da média em simulados lançados para o ano dentro do contexto
                        var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                      join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                      where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                      && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                      && tb49.CO_ANO == this.CO_ANO
                                      && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                      && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                      && tb273.CO_SIGLA_ATIV == "PR"
                                      && tb49.CO_REFER_NOTA == "N1"
                                      select new
                                      {
                                          tb273.CO_SIGLA_ATIV,
                                          tb49.VL_NOTA,
                                      }).FirstOrDefault();

                        decimal media = 0;
                        if (result != null)
                        {
                            if (this.NT_MAX_PROV.HasValue)
                            {
                                media = (result.VL_NOTA >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : result.VL_NOTA);
                                return media.ToString("N2");
                            }
                            else
                                return result.VL_NOTA.ToString("N2");
                        }
                        else
                            return " - ";
                    }
                }
            }
            public string NOTA2
            {
                get
                {
                                        //Tratamento feito quando a disciplina em questao e agrupadora
                    if (this.FL_AGRUPADORA_V == "S")
                    {
                        string ano = this.CO_ANO.ToString();

                        //faz uma verificacao para identificar quais as disciplinas filhas da disciplina em questao
                        var liagrup = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                       where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                                       && tb43.CO_CUR == this.CO_CUR
                                       && tb43.ID_MATER_AGRUP == this.CO_MAT
                                       && tb43.CO_ANO_GRADE == ano
                                       select new { tb107.ID_MATERIA }).ToList();

                        decimal notaAtividade = 0;
                        decimal Media = 0;
                        int count = 0;
                        //Percorre a lista de disciplinas agrupadas contabilizando as notas
                        foreach (var li in liagrup)
                        {
                            var lisAgruAtiv = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                               join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                               where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                               && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                               && tb49.CO_ANO == this.CO_ANO
                                               && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                               && tb49.TB107_CADMATERIAS.ID_MATERIA == li.ID_MATERIA
                                               && tb273.CO_SIGLA_ATIV == "PR"
                                               && tb49.CO_REFER_NOTA == "N2"
                                               select new
                                               {
                                                   tb273.CO_SIGLA_ATIV,
                                                   tb49.VL_NOTA,
                                               }).ToList();

                            foreach (var l in lisAgruAtiv)
                            {
                                notaAtividade += l.VL_NOTA;
                                count++;
                            }
                        }

                        //faz o calculo final para atribuir a nota das atividades das disciplinas agrupadas
                        if (count > 0)
                        {
                            //Media = notaAtividade / count;
                            Media = notaAtividade;
                            if (this.NT_MAX_PROV.HasValue)
                            {
                                Media = (Media >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : Media);
                                return Media.ToString("N2");
                            }
                            else
                                return Media.ToString("N2");
                        }
                        else
                            return " - ";
                    }
                    //Caso nao seja agrupadora, realiza o calculo das notas da maneira convencional
                    else
                    {
                        //Faz o cálculo da média em simulados lançados para o ano dentro do contexto
                        var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                      join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                      where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                      && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                      && tb49.CO_ANO == this.CO_ANO
                                      && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                      && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                      && tb273.CO_SIGLA_ATIV == "PR"
                                      && tb49.CO_REFER_NOTA == "N2"
                                      select new
                                      {
                                          tb273.CO_SIGLA_ATIV,
                                          tb49.VL_NOTA,
                                      }).FirstOrDefault();

                        decimal media = 0;
                        if (result != null)
                        {
                            if (this.NT_MAX_PROV.HasValue)
                            {
                                media = (result.VL_NOTA >= this.NT_MAX_PROV.Value ? this.NT_MAX_PROV.Value : result.VL_NOTA);
                                return media.ToString("N2");
                            }
                            else
                                return result.VL_NOTA.ToString("N2");
                        }
                        else
                            return " - ";
                    }
                }
            }
            public string MDSML_V
            {
                get
                {
                        //Faz o cálculo da média em simulados lançados para o ano dentro do contexto
                        var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                      join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                      where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                      && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                      && tb49.CO_ANO == this.CO_ANO
                                      && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                      && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                      && tb273.CO_SIGLA_ATIV == "SI"
                                      select new
                                      {
                                          tb273.CO_SIGLA_ATIV,
                                          tb49.VL_NOTA,
                                      }).ToList();

                        decimal nota = 0;
                        decimal Media = 0;
                        int count = 0;
                        foreach (var l in result)
                        {
                            nota += l.VL_NOTA;
                            count++;
                        }

                        if (result.Count >= 1)
                        {
                            Media = nota / count;
                            if (this.NT_MAX_SIMU.HasValue)
                            {
                                Media = (Media >= this.NT_MAX_SIMU.Value ? this.NT_MAX_SIMU.Value : Media);
                                return Media.ToString("N2");
                            }
                            else
                                return Media.ToString("N2");
                        }
                        else
                            return " - ";
                    
                }
            }
            public string MDATI_V
            {
                get
                {
                    //Tratamento feito quando a disciplina em questao e agrupadora
                    if (this.FL_AGRUPADORA_V == "S")
                    {
                        string ano = this.CO_ANO.ToString();

                        //faz uma verificacao para identificar quais as disciplinas filhas da disciplina em questao
                        var liagrup = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                       where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                                       && tb43.CO_CUR == this.CO_CUR
                                       && tb43.ID_MATER_AGRUP == this.CO_MAT
                                       && tb43.CO_ANO_GRADE == ano
                                       select new { tb107.ID_MATERIA }).ToList();

                        decimal notaAtividade = 0;
                        decimal Media = 0;
                        int count = 0;
                        //Percorre a lista de disciplinas agrupadas contabilizando as notas
                        foreach (var li in liagrup)
                        {
                            var lisAgruAtiv = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                               join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                               where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                               && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                               && tb49.CO_ANO == this.CO_ANO
                                               && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                               && tb49.TB107_CADMATERIAS.ID_MATERIA == li.ID_MATERIA
                                               && tb273.CO_SIGLA_ATIV == "AT"
                                               select new
                                               {
                                                   tb273.CO_SIGLA_ATIV,
                                                   tb49.VL_NOTA,
                                               }).ToList();

                            foreach (var l in lisAgruAtiv)
                            {
                                notaAtividade += l.VL_NOTA;
                                count++;
                            }
                        }

                        //faz o calculo final para atribuir a nota das atividades das disciplinas agrupadas
                        if (count > 0)
                        {
                            //Media = notaAtividade / count;
                            Media = notaAtividade;
                            if (this.NT_MAX_ATIV.HasValue)
                            {
                                Media = (Media >= this.NT_MAX_ATIV.Value ? this.NT_MAX_ATIV.Value : Media);
                                return Media.ToString("N2");
                            }
                            else
                                return Media.ToString("N2");
                        }
                        else
                            return " - ";
                    }
                        //Caso nao seja agrupadora, realiza o calculo das notas da maneira convencional
                    else
                    {
                        //Faz o cálculo da média em atividades lançadas para o ano dentro do contexto
                        var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                      join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                      where tb49.TB07_ALUNO.CO_ALU == this.CO_ALU
                                      && tb49.TB01_CURSO.CO_CUR == this.CO_CUR
                                      && tb49.CO_ANO == this.CO_ANO
                                      && tb49.CO_BIMESTRE == this.CO_BIMESTRE
                                      && tb49.TB107_CADMATERIAS.ID_MATERIA == this.ID_MATERIA
                                      && tb273.CO_SIGLA_ATIV == "AT"
                                      select new
                                      {
                                          tb273.CO_SIGLA_ATIV,
                                          tb49.VL_NOTA,
                                      }).ToList();

                        decimal notaAtividade = 0;
                        decimal Media = 0;
                        foreach (var l in result)
                        {
                            notaAtividade += l.VL_NOTA;
                        }

                        if (result.Count >= 1)
                        {
                            //Media = notaAtividade / result.Count;
                            Media = notaAtividade;
                            if (this.NT_MAX_ATIV.HasValue)
                            {
                                Media = (Media >= this.NT_MAX_ATIV.Value ? this.NT_MAX_ATIV.Value : Media);
                                return Media.ToString("N2");
                            }
                            else
                                return Media.ToString("N2");
                        }
                        else
                            return " - ";
                    }
                }
            }
            public string MDBIM_V
            {
                get
                {
                    int qtDiv = 0;
                    decimal n1, n2, n3, na, mediaFinal;
                    n1 = n2 = n3 = na = mediaFinal = 0;

                    //Trata os valores e soma um auxiliar que sera usado na divisao para calcular a media
                    #region Verifica as notas lançadas e prepara para o cálculo

                    //Provas NOTA1
                    if (this.NOTA1 != " - ")
                    {
                        n1 = decimal.Parse(this.NOTA1);
                        qtDiv++;
                    }

                    //Provas NOTA2
                    if (this.NOTA2 != " - ")
                    {
                        n2 = decimal.Parse(this.NOTA2);
                        qtDiv++;
                    }

                    //Simulados
                    if (this.MDSML_V != " - ")
                    {
                        n3 = decimal.Parse(this.MDSML_V);
                        qtDiv++;
                    }

                    //Atividades
                    if (this.MDATI_V != " - ")
                    {
                        na = decimal.Parse(this.MDATI_V);
                        qtDiv++;
                    }

                    #endregion

                    if (qtDiv > 0)
                    {
                        mediaFinal += n1 + n2 + n3;
                        decimal m = mediaFinal / 3;
                        m += na;

                        //Prepara a nota máxima
                        decimal ntmx = (this.NT_MAX.HasValue ? this.NT_MAX.Value : 10);

                        //Verifica se foi escolhido que a nota1 é a média
                        if (this.FL_NOTA1_MEDIA == "S")
                        {
                            if (n1 > ntmx)
                                return ntmx.ToString("N2");
                            else
                                return n1.ToString("N2");
                        }
                        else
                            //Caso não tenha sido escolhida a opção para que a nota1 é a mpedia, faz o cálculo normal
                        {
                            if (m >= ntmx)
                                return ntmx.ToString("N2");
                            else
                                return m.ToString("N2");
                        }
                    }
                    else
                        return " - ";
                        //return (this.MDATI_V != " - " ? na.ToString("N2") : " - ");
                }
            }
            //public string RESULT_V
            //{
            //    get
            //    {
            //        if (this.MDFIM_V != " - ")
            //        {
            //            //Calcula a nota, respeitando a nota máxima para a disciplina em questão
            //            decimal mdfim = decimal.Parse(this.MDFIM_V);
            //            decimal ntCalculada = mdfim + (this.NTBIM.HasValue ? this.NTBIM.Value : 0);
            //            decimal ntmx = (this.NT_MAX.HasValue ? this.NT_MAX.Value : 10);
            //            if ((this.NTBIM.HasValue ? this.NTBIM.Value : 0) >= ntmx)
            //                return this.NTBIM.Value.ToString("N2");
            //            else
            //            {
            //                return (ntCalculada >= ntmx ? ntmx.ToString("N2") : ntCalculada.ToString("N2"));
            //            }
            //        }
            //        else
            //            return this.NTBIM.HasValue ? this.NTBIM.Value.ToString("N2") : " - ";
            //    }
            //}
        }
        #endregion
    }
}