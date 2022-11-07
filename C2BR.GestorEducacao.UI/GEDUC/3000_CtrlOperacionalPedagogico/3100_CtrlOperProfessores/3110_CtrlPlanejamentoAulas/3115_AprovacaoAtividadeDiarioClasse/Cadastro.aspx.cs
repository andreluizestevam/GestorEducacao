using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3115_AprovacaoAtividadeDiarioClasse
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
            if (IsPostBack) return;

            CarregaAnos();
            CarregaProfessorHomolog();
            divGrid.Visible = false;
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB119_ATIV_PROF_TURMA tb119;

            //--------> Varre toda a grid de Alunos
            foreach (GridViewRow linha in grdPlanoAulas.Rows)
            {
                int coPlaAula = Convert.ToInt32(grdPlanoAulas.DataKeys[linha.RowIndex].Values[0]);
                int coCol = ddlCocol.SelectedValue != "" ? int.Parse(ddlCocol.SelectedValue) : 0;

                tb119 = TB119_ATIV_PROF_TURMA.RetornaPelaChavePrimaria(coPlaAula);
                
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    tb119.FL_HOMOL_DIARIO = "S";
                else
                    tb119.FL_HOMOL_DIARIO = "N";


                TB119_ATIV_PROF_TURMA.SaveOrUpdate(tb119, true);
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", Request.Url.AbsoluteUri);
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("Selecione", ""));
            ddlAno.SelectedValue = "";

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();
            ddlMes.Items.Clear();
            ddlProfessor.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
            ddlModalidade.SelectedValue = "";

            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();
            ddlMes.Items.Clear();
            ddlProfessor.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            ddlSerieCurso.Items.Clear();
            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where (modalidade == -1 ? 0 == 0 : tb01.CO_MODU_CUR == modalidade)
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "-1"));
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            ddlSerieCurso.SelectedValue = "";

            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();
            ddlMes.Items.Clear();
            ddlProfessor.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            ddlTurma.Items.Clear();
            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where (modalidade == -1 ? 0 == 0 : tb06.CO_MODU_CUR == modalidade)
                                       && (serie == -1 ? 0 == 0 : tb06.CO_CUR == serie)
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
                ddlTurma.Items.Insert(0, new ListItem("Todos", "-1"));
            }

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            ddlTurma.SelectedValue = "";

            ddlDisciplina.Items.Clear();
            ddlMes.Items.Clear();
            ddlProfessor.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            ddlDisciplina.Items.Clear();
            ddlDisciplina.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                        where (modalidade == -1 ? 0 == 0 : tb02.CO_MODU_CUR == modalidade)
                                        && (serie == -1 ? 0 == 0 : tb02.CO_CUR == serie)
                                        && tb02.CO_EMP == LoginAuxili.CO_EMP
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                        select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA).DistinctBy(u => u.CO_MAT);

            ddlDisciplina.DataTextField = "NO_MATERIA";
            ddlDisciplina.DataValueField = "CO_MAT";
            ddlDisciplina.DataBind();
            ddlDisciplina.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
            ddlDisciplina.SelectedValue = "";

            ddlMes.Items.Clear();
            ddlProfessor.Items.Clear();
        }

        /// <summary>
        /// Carrega a lista de meses disponiveis
        /// </summary>
        private void CarregaMes()
        {
            ddlMes.Items.Clear();
            ddlMes.Items.Insert(0, new ListItem("Selecione", ""));
            ddlMes.Items.Insert(1, new ListItem("Todos", "-1"));
            ddlMes.Items.Insert(2, new ListItem("Janeiro", "1"));
            ddlMes.Items.Insert(3, new ListItem("Fevereiro", "2"));
            ddlMes.Items.Insert(4, new ListItem("Março", "3"));
            ddlMes.Items.Insert(5, new ListItem("Abril", "4"));
            ddlMes.Items.Insert(6, new ListItem("Maio", "5"));
            ddlMes.Items.Insert(7, new ListItem("Junho", "6"));
            ddlMes.Items.Insert(8, new ListItem("Julho", "7"));
            ddlMes.Items.Insert(9, new ListItem("Agosto", "8"));
            ddlMes.Items.Insert(10, new ListItem("Setembro", "9"));
            ddlMes.Items.Insert(11, new ListItem("Outubro", "10"));
            ddlMes.Items.Insert(12, new ListItem("Novembro", "11"));
            ddlMes.Items.Insert(13, new ListItem("Dezembro", "12"));
            ddlMes.SelectedValue = "";
            ddlProfessor.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Professores
        /// </summary>
        private void CarregaProfessor()
        {
            ddlProfessor.Items.Clear();
            ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                       where tb03.FLA_PROFESSOR == "S"
                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlProfessor.DataTextField = "NO_COL";
            ddlProfessor.DataValueField = "CO_COL";
            ddlProfessor.DataBind();

            ddlProfessor.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));
            ddlProfessor.SelectedValue = "";

        }

        /// <summary>
        /// Método que carrega o dropdown de Professor Homologação
        /// </summary>
        private void CarregaProfessorHomolog()
        {
            ddlCocol.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                   where tb03.CO_COL == LoginAuxili.CO_COL
                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlCocol.DataTextField = "NO_COL";
            ddlCocol.DataValueField = "CO_COL";
            ddlCocol.DataBind();
            ddlCocol.SelectedValue = LoginAuxili.CO_COL.ToString();
        }

        /// <summary>
        /// Método que carrega a grid de Alunos
        /// </summary>
        private void CarregaGrid()
        {
            string coAnoRefPla = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : DateTime.Now.Year.ToString();
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int coCol = ddlProfessor.SelectedValue != "" ? int.Parse(ddlProfessor.SelectedValue) : 0;
            int mesRefer = ddlMes.SelectedValue != "" ? int.Parse(ddlMes.SelectedValue) : 0;

            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb119.CO_CUR equals tb01.CO_CUR
                             join tb06 in TB06_TURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb119.CO_TUR equals tb06.CO_TUR
                             join tb03 in TB03_COLABOR.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb119.CO_COL_ATIV equals tb03.CO_COL
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_ANO_MES_MAT == coAnoRefPla
                             && (modalidade != -1 ? tb119.CO_MODU_CUR == modalidade : 0 == 0)
                             && (serie != -1 ? tb119.CO_CUR == serie : 0 == 0) 
                             && (turma != -1 ? tb119.CO_TUR == turma : 0 == 0)
                             && (materia != -1 ? tb119.CO_MAT == materia : 0 == 0) 
                             && (coCol != -1 ? tb119.CO_COL_ATIV == coCol : 0 == 0)
                             && (mesRefer != -1 ? tb119.DT_ATIV_REAL.Month == mesRefer : 0 == 0)
                             && tb06.CO_CUR == tb119.CO_CUR 
                             select new
                             {
                                 tb119.HR_INI_ATIV,
                                 tb119.HR_TER_ATIV,
                                 tb119.DT_ATIV_REAL,
                                 tb03.NO_COL,
                                 tb119.CO_ATIV_PROF_TUR,
                                 FLA_HOMOLOG = (tb119.FL_HOMOL_DIARIO == "S" ? true : false),
                                 tb119.DE_TEMA_AULA,
                                 tb01.CO_SIGL_CUR,
                                 tb06.TB129_CADTURMAS.CO_SIGLA_TURMA
                             }).OrderBy(p => p.NO_COL).ThenBy(p => p.DT_ATIV_REAL);


            //Habilita o botão de salvar

            divGrid.Visible = true;

            grdPlanoAulas.DataKeyNames = new string[] { "CO_ATIV_PROF_TUR" };

            grdPlanoAulas.DataSource = resultado;
            grdPlanoAulas.DataBind();
        }
        #endregion

        #region Eventos componentes
        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaModalidades();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                if (((DropDownList)sender).SelectedValue != "-1")
                {
                    ddlSerieCurso.Enabled = ddlTurma.Enabled = true;
                    CarregaSerieCurso();
                }
                else
                {
                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = "-1";
                    ddlSerieCurso.Enabled = false;
                    CarregaTurma();
                    ddlTurma.SelectedValue = "-1";
                    ddlTurma.Enabled = false;
                    CarregaMaterias();
                }
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaMaterias();
        }

        protected void ddlProfessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                CarregaGrid();
                CarregaProfessorHomolog();
            }
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaProfessor();
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaMes();
        }

        protected void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow linha in grdPlanoAulas.Rows)
            {
                ((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked = ((CheckBox)sender).Checked;
            }
        }
        
        #endregion
    }
}