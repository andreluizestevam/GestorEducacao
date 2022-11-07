//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: EXCLUI GRADE DE DISCIPLINA ANO/MODALIDADE/SÉRIE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3017_ExcluiGradeAnualDisciplina
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
            CarregaModalidades();
            CarregaSerieCurso();
            divGrid.Visible = false;
        }

//----> Método que faz a chamada de outro método de exclusão da Grade de Série
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            ExcluirGrades();
        }
        #endregion

        #region Métodos
        
        /// <summary>
        /// Método que exclui registros selecionados na grid da TB43_GRD_CURSO
        /// </summary>
        private void ExcluirGrades()
        {
            TB43_GRD_CURSO tb43;

            int qtdGrade = 0;

            //--------> Varre toda a grid da Grade da Série
            foreach (GridViewRow linha in grdGrade.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    qtdGrade++;

                    int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                    int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                    int materia = Convert.ToInt32(((HiddenField)linha.Cells[0].FindControl("hdCodMat")).Value);
                    string strSemestre = ((HiddenField)linha.Cells[0].FindControl("hdNumSem")).Value;
                    string coAnoMesMat = ddlAno.SelectedValue;

                    var lstTb48 = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                   where tb48.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb48.CO_MODU_CUR == modalidade
                                   && tb48.CO_MAT == materia && tb48.CO_ANO_MES_MAT == coAnoMesMat && tb48.CO_CUR == serie && tb48.NU_SEM_LET == strSemestre
                                   select new { tb48.CO_EMP } ).ToList();

                    if (lstTb48.Count == 0)
                    {
                        tb43 = new TB43_GRD_CURSO();
                        tb43 = TB43_GRD_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coAnoMesMat, serie, materia);

                        TB43_GRD_CURSO.Delete(tb43, true);
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Uma ou mais grades não podem ser excluídas porque existem vínculos.");
                        return;
                    }
                }
            }

            if (qtdGrade > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Grade(s) excluída(s).", Request.Url.AbsoluteUri);
            else
                AuxiliPagina.EnvioMensagemErro(this, "Não há grades selecionadas");
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                 where tb43.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).Distinct().OrderBy(c => c.CO_SIGL_CUR);

                ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega a grid de Matérias
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string strCoAnoGrade = ddlAno.SelectedValue;

            if (strCoAnoGrade == "" || modalidade == 0 || serie == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Ano, Modalidade e Série/Curso devem ser selecionados");
                grdGrade.DataBind();
                return;
            }

            var listaGrade = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                              join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                              join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                              where tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.CO_ANO_GRADE == strCoAnoGrade
                              && tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                              select new
                              {
                                  tb107.NO_MATERIA, tb43.QTDE_CH_SEM, tb43.QTDE_AULA_SEM, tb43.NU_SEM_GRADE, tb43.CO_MAT
                              }).ToList().OrderBy( g => g.NO_MATERIA );

            divGrid.Visible = chkSelecionarTodos.Visible = true;

            if (listaGrade.Count() > 0)
                chkSelecionarTodos.Visible = true;
            else
            {
                grdGrade.DataBind();
                chkSelecionarTodos.Visible = false;
                return;
            }

            grdGrade.DataSource = listaGrade;
            grdGrade.DataBind();
        }
        #endregion

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            grdGrade.DataBind();
            chkSelecionarTodos.Visible = false;
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            grdGrade.DataBind();
            chkSelecionarTodos.Visible = false;
        }  

        protected void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in grdGrade.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("ckSelect");
                chkSel.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}
