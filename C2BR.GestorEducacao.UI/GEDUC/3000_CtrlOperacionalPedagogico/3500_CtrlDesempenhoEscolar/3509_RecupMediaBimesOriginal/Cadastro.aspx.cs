//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: ******
// DATA DE CRIAÇÃO: 15/05/2013 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 15/05/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 16/05/2013| André Nobre Vinagre        | Quando alterado o valor da prova final deve ficar nulo
//           |                            |

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3509_RecupMediaBimesOriginal
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
            
            CarregaAnos();
            CarregaModalidades();
            divGrid.Visible = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool flgOcoNota = false;            

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string strRefer = ddlReferencia.SelectedValue;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || coMat == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se algum aluno foi selecionado
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    flgOcoNota = true;
                }
            }

            if (!flgOcoNota)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum aluno(a) foi selecionado(a)");
                return;
            }
            
//--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma && iTb079.CO_MAT == coMat
                                              select iTb079).FirstOrDefault();

                    if (tb079 != null)
                    {
                        if (strRefer == "S1")
                        {

                            tb079.VL_RECU_SEM1 = null;
                            tb079.VL_NOTA_BIM1 = tb079.VL_NOTA_BIM1_ORI;
                            tb079.VL_NOTA_BIM1_ANT = null;
                            tb079.VL_NOTA_BIM2 = tb079.VL_NOTA_BIM2_ORI;
                            tb079.VL_NOTA_BIM2_ANT = null;
                        }
                        else
                        {
                            tb079.VL_RECU_SEM2 = null;
                            tb079.VL_NOTA_BIM3 = tb079.VL_NOTA_BIM3_ORI;
                            tb079.VL_NOTA_BIM3_ANT = null;
                            tb079.VL_NOTA_BIM4 = tb079.VL_NOTA_BIM4_ORI;
                            tb079.VL_NOTA_BIM4_ANT = null;
                        }

                        tb079.VL_PROVA_FINAL = null;
                    }

                    if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                        return;
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
            string anoMesMat = ddlAno.SelectedValue;
            string strRefer = ddlReferencia.SelectedValue;

            divGrid.Visible = true;            

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                              where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                              && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia 
                              && tb08.CO_SIT_MAT == "A"
                              select new NotasAluno
                              {
                                  CO_ALU = tb079.CO_ALU, NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                  NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                  Media1 = strRefer == "S1" ? tb079.VL_NOTA_BIM1 : tb079.VL_NOTA_BIM3,
                                  Media2 = strRefer == "S1" ? tb079.VL_NOTA_BIM2 : tb079.VL_NOTA_BIM4,
                                  RECU = strRefer == "S1" ? tb079.VL_RECU_SEM1 : tb079.VL_RECU_SEM2,
                                  ENABLED = strRefer == "S1" ? (tb079.VL_RECU_SEM1 != null ? true : false)
                                  : (tb079.VL_RECU_SEM2 != null ? true : false)
                              }).ToList();

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
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (turma == 0 || modalidade == 0 || serie == 0 )
                return;

            ddlDisciplina.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                        where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE.Equals(ddlAno.SelectedValue)
                                        join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                        where tb107.CO_EMP == LoginAuxili.CO_EMP
                                        select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy( g => g.NO_MATERIA );

            ddlDisciplina.DataTextField = "NO_MATERIA";
            ddlDisciplina.DataValueField = "CO_MAT";
            ddlDisciplina.DataBind();
            
            ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion      

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaDisciplina();
            grdBusca.DataSource = null;
            grdBusca.DataBind();
            divGrid.Visible = false;
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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

            if (ddlReferencia.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Referência deve ser informada.");
                return;
            }

            CarregaGrid();
        }

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            public string NO_ALU { get; set; }
            public decimal? Media1 { get; set; }
            public decimal? Media2 { get; set; }
            public decimal? RECU { get; set; }
            public bool ENABLED { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }

            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }
        }
        #endregion
    }
}