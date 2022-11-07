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
// 10/12/2014| Maxwell Almeida            | Criação da funcionalidade para alteração da média final

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3519_AlterMediaFinal
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
            VerificaMostraDisciplinas();
            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            divGrid.Visible = false;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flgOcoMedia = false;

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série e Turma devem ser informados.");
                return;
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                string mediaAntiga = (((TextBox)linha.Cells[3].FindControl("txtMDAntiga")).Text);
                string mediaNova = (((TextBox)linha.Cells[4].FindControl("txtMDNova")).Text);

                //Verifica se a nota foi alterada e se está diferente de vazio
                if ((mediaAntiga != mediaNova) && (!string.IsNullOrEmpty(mediaNova)))
                {
                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                    int coMat = int.Parse(((HiddenField)linha.Cells[3].FindControl("hidCoMat")).Value);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu && iTb079.CO_ANO_REF == ddlAno.SelectedValue
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma
                                                 && iTb079.CO_MAT == coMat
                                              select iTb079).FirstOrDefault();

                    if (tb079 != null)
                    {
                        //----------------> Atribui o valor de média informado de acordo com o bimestre
                        tb079.VL_MEDIA_ANUAL =
                            tb079.VL_MEDIA_FINAL = decimal.Parse(mediaNova);

                        //Quando selecionado, salva a média final informada nas médias bimestrais
                        if (chkMediaBimMedFinal.Checked)
                            tb079.VL_MEDIA_BIM1 = tb079.VL_MEDIA_BIM2 = tb079.VL_MEDIA_BIM3 = tb079.VL_MEDIA_BIM4 = decimal.Parse(mediaNova);

                        if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                            return;
                        }
                    }
                }
            }

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

            divGrid.Visible = true;

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_MAT equals tb43.CO_MAT
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma
                             && tb08.CO_SIT_MAT == "A"
                             && (coAlu != 0 ? tb08.CO_ALU == coAlu : 0 == 0)
                             && (materia != 0 ? tb079.CO_MAT == materia : 0 == 0)
                             && tb43.FL_LANCA_NOTA == "S"
                             && tb43.CO_MAT == tb079.CO_MAT
                             && tb43.CO_CUR == tb079.CO_CUR
                             && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                             && tb43.ID_MATER_AGRUP == null
                             select new NotasAluno
                             {
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 NO_MAT = tb107.NO_MATERIA,
                                 CO_MAT = tb02.CO_MAT,
                                 mdfim = tb079.VL_MEDIA_FINAL,
                             }).ToList();

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
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true, true);
            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false, false, false, true, true);
            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false, false, true, true);
            CarregaAluno();
        }

        /// <summary>
        /// Faz a verificacao em todos os postbacks para mostrar ou nao as informacoes pertinentes
        /// </summary>
        private void VerificaMostraDisciplinas(bool CarregamentoPagina = false)
        {
            //Muda o foco de acordo com as opcoes selecionadas e disponiveis
            if ((ddlModalidade.Items.Count > 1) && (ddlModalidade.SelectedValue == ""))
                ddlModalidade.Focus();
            else if ((ddlSerieCurso.Items.Count > 1) && (ddlSerieCurso.SelectedValue == ""))
                ddlSerieCurso.Focus();
            else if ((ddlTurma.Items.Count > 1) && (ddlTurma.SelectedValue == ""))
                ddlTurma.Focus();
        }

        private void CarregaAluno()
        {
            ddlAluno.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   where tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_TUR == turma &&
                                   tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_CUR == serie
                                   && tb08.CO_SIT_MAT != "C"
                                   && tb08.CO_SIT_MAT != "X"
                                   select new
                                   {
                                       tb08.TB07_ALUNO.CO_ALU,
                                       tb08.TB07_ALUNO.NO_ALU
                                   }).OrderBy(r => r.NO_ALU);

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (turma == 0 || modalidade == 0 || serie == 0)
                return;

            ddlDisciplina.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                        where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE.Equals(ddlAno.SelectedValue)
                                        && tb43.ID_MATER_AGRUP == null
                                        join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                        where tb107.CO_EMP == LoginAuxili.CO_EMP
                                        select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(g => g.NO_MATERIA);

            ddlDisciplina.DataTextField = "NO_MATERIA";
            ddlDisciplina.DataValueField = "CO_MAT";
            ddlDisciplina.DataBind();

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
            public string NO_ALU { get; set; }
            public string NO_MAT { get; set; }
            public int CO_MAT { get; set; }
            public string CO_BIMESTRE { get; set; }
            public decimal? mdfim { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(7, '0'); } }
        }
        #endregion
    }
}