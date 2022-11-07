//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE TRANSFERÊNCIA ESCOLAR
// OBJETIVO: REGISTRO DE TRANSFERÊNCIA DE ALUNOS ENTRE ESCOLAS DA REDE DE ENSINO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3302_RegistroTransfAlunoEscola
{
  public partial class Cadastro : System.Web.UI.Page 
  {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        #region Eventos

        protected override void OnPreInit(EventArgs e) 
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.BarraCadastro.OnNewSearch += new Library.Componentes.BarraCadastro.OnNewSearchHandler(CurrentPadraoNewSearch);
        }
        void CurrentPadraoNewSearch() { }

        protected void Page_Load(object sender, EventArgs e) 
        {
            if (IsPostBack) return;

            ViewState.Add("CodigoUnidade", "N");      
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            divGrid.Visible = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro() 
        {
            int qtdAlunos = 0;

//--------> Varre toda a grid de Aluno
            foreach (GridViewRow linha in grdAlunos.Rows) 
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked) 
                {
                    if (((DropDownList)linha.Cells[4].FindControl("ddlUnidade")).SelectedValue == "") 
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Unidade deve ser selecionada");
                        return;
                    } 
                    else 
                    {

                  
                        


                        qtdAlunos += 1;
                        int coAlu = int.Parse(((HiddenField)linha.FindControl("hdCO_ALU")).Value);
                        int coEmpOrigem = int.Parse(((HiddenField)linha.FindControl("hdCO_EMP")).Value);
                        int coEmpDestino = int.Parse(((DropDownList)linha.FindControl("ddlUnidade")).SelectedValue);
                        int coTur = int.Parse(((DropDownList)linha.FindControl("ddlTurma")).SelectedValue);
                        int coEmpContr = TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur).CO_EMP_UNID_CONT;

                        TB06_TURMAS tb06 = TB06_TURMAS.RetornaPeloCodigo(coTur,coEmpDestino);

                        TB08_MATRCUR tb08_Anterior = TB08_MATRCUR.RetornaPeloAluno(coAlu, ddlAno.SelectedValue);
                        if(tb08_Anterior != null) { 
                            tb08_Anterior.CO_SIT_MAT = "T";
                            tb08_Anterior.DT_SIT_MAT = DateTime.Now;
                            TB08_MATRCUR.SaveOrUpdate(tb08_Anterior);
                        }


                        TB08_MATRCUR tb08_Nova =  new TB08_MATRCUR();
                        tb08_Nova.CO_TUR = coTur;
                        tb08_Nova.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(tb06.CO_MODU_CUR);
                        tb08_Nova.CO_CUR = tb06.CO_CUR;

                        string strProxMatricula = (DateTime.Now.Year.ToString().Substring(2, 2) + coEmpDestino + coAlu);

                        tb08_Nova.CO_ALU_CAD = strProxMatricula;
                        tb08_Nova.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb08_Nova.CO_ANO_MES_MAT = tb08_Anterior.CO_ANO_MES_MAT;
                        tb08_Nova.CO_COL = tb08_Anterior.CO_COL;
                        tb08_Nova.CO_EMP = coEmpDestino; // TB25_EMPRESA.RetornaPelaChavePrimaria(coEmpDestino);
                        tb08_Nova.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmpDestino);
                        tb08_Nova.CO_EMP_UNID_CONT = coEmpContr;
                        tb08_Nova.CO_SIT_MAT = "A";
                        tb08_Nova.CO_STA_APROV = "A";
                        tb08_Nova.CO_STA_APROV_FREQ = "A";
                        tb08_Nova.CO_TUR_ANT = tb08_Anterior.CO_TUR_ANT;
                        tb08_Nova.CO_TURN_MAT = tb08_Anterior.CO_TURN_MAT;
                        tb08_Nova.DT_ALT_REGISTRO = tb08_Anterior.DT_ALT_REGISTRO;
                        tb08_Nova.DT_CAD_MAT = tb08_Anterior.DT_CAD_MAT;
                        tb08_Nova.DT_CADASTRO = DateTime.Now;
                        tb08_Nova.DT_EFE_MAT = tb08_Anterior.DT_EFE_MAT;
                        tb08_Nova.DT_PRI_PAR_MOD_MAT = tb08_Anterior.DT_PRI_PAR_MOD_MAT;
                        tb08_Nova.DT_SIT_MAT = tb08_Anterior.DT_SIT_MAT;
                        tb08_Nova.FLA_ESCOLA_ANO_NOVATO = tb08_Anterior.FLA_ESCOLA_ANO_NOVATO;
                        tb08_Nova.FLA_ESCOLA_NOVATO = tb08_Anterior.FLA_ESCOLA_NOVATO;
                        tb08_Nova.FLA_REMATRICULADO = tb08_Anterior.FLA_REMATRICULADO;
                        tb08_Nova.NOM_USUARIO = LoginAuxili.NOME_USU_LOGADO;
                        tb08_Nova.NU_DIA_VEN_MOD_MAT = tb08_Anterior.NU_DIA_VEN_MOD_MAT;
                        tb08_Nova.NU_INSC_ALU = tb08_Anterior.NU_INSC_ALU;
                        tb08_Nova.NU_SEM_LET = tb08_Anterior.NU_SEM_LET;
                        tb08_Nova.QT_PAR_MOD_MAT = tb08_Anterior.QT_PAR_MOD_MAT;
                        tb08_Nova.TB110_ORIGEM_MAT = tb08_Anterior.TB110_ORIGEM_MAT;
                        tb08_Nova.TB22_TIPOPAG = tb08_Anterior.TB22_TIPOPAG;
                        tb08_Nova.VL_DES_MOD_MAT = tb08_Anterior.VL_DES_MOD_MAT;
                        tb08_Nova.VL_ENT_MOD_MAT = tb08_Anterior.VL_ENT_MOD_MAT;
                        tb08_Nova.VL_PAR_MOD_MAT = tb08_Anterior.VL_PAR_MOD_MAT;
                        tb08_Nova.VL_TOT_MODU_MAT = tb08_Anterior.VL_TOT_MODU_MAT;

                        TB08_MATRCUR.SaveOrUpdate(tb08_Nova);

                        var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                        tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmpDestino);

                        TB07_ALUNO.SaveOrUpdate(tb07, true);

                        TB285_HIST_TRANS_ALUNO tb285 = new TB285_HIST_TRANS_ALUNO();

                        var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb285.TB07_ALUNO = refAluno;
                        refAluno.TB25_EMPRESA1Reference.Load();
                        tb285.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                        tb285.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                        tb285.DT_CADASTRADO = DateTime.Now;
                        tb285.TB06_TURMAS = TB06_TURMAS.RetornaPeloCodigo(coTur);
                        tb285.TB06_TURMAS1 = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue));

                        TB285_HIST_TRANS_ALUNO.SaveOrUpdate(tb285);                        
                    }
                }
            }
      
            if (qtdAlunos > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", Request.Url.AbsoluteUri);
            else
                AuxiliPagina.EnvioMensagemErro(this, "Não há alunos para transferência");
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que retorna a lista de Grades da Série
        /// </summary>
        /// <param name="serie">Id da série</param>
        /// <returns></returns>
        private List<TB43_GRD_CURSO> GradeSerie(int serie)
        {
            string strProxAno = (int.Parse(ddlAno.SelectedValue) + 1).ToString();

            var lstTb43 = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           where tb43.CO_EMP == LoginAuxili.CO_EMP && tb43.CO_ANO_GRADE == strProxAno && tb43.CO_CUR == serie
                           select tb43).ToList();

            return lstTb43;
        }
        #endregion

        #region Carregamento

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
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o grid de Alunos
        /// </summary>
        private void CarregaGrid() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
      
            var lstAlunoMatric = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                  where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue
                                  && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                                  select new 
                                  {
                                      tb08.TB07_ALUNO.NU_NIRE,
                                      tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU,
                                      tb08.CO_SIT_MAT, 
                                      tb08.FLA_REMATRICULADO, tb01.CO_SERIE_REFER, tb08.CO_EMP,
                                      PROX_CURSO = tb08.CO_STA_APROV == "A" ? tb01.CO_PREDEC_CUR : tb08.CO_STA_APROV == "R" ? tb08.CO_CUR : 0,
                                      STATUS = tb08.CO_STA_APROV == "A" ? "Aprovado" : tb08.CO_STA_APROV == "R" ? "Reprovado" : "Cursando"
                                  }).ToList().OrderBy( m => m.NO_ALU );

            var resultado2 = (from result in lstAlunoMatric
                              select new
                              {
                                  NU_NIRE = result.NU_NIRE.ToString().PadLeft(7, '0'),
                                  result.CO_ALU,
                                  result.NO_ALU,
                                  result.CO_SIT_MAT,
                                  result.FLA_REMATRICULADO,
                                  result.CO_SERIE_REFER,
                                  result.CO_EMP,
                                  result.PROX_CURSO,
                                  result.STATUS,
                              }).ToList();

            divGrid.Visible = true;

            if (resultado2.Count() > 0) 
            {
                //Habilita botão salvar
            } 
            else 
            {
                grdAlunos.DataBind();
                return;
            }

            grdAlunos.DataKeyNames = new string[] { "CO_ALU" };

            grdAlunos.DataSource = resultado2;
            grdAlunos.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        /// <param name="ddl">DropDown de unidade</param>
        /// <param name="serie">Id da série</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaUnidadesDdlGrid(DropDownList ddl, string serie, int coEmp) 
        {
            //ddl.DataSource = (from tb134 in TB134_USR_EMP.RetornaTodosRegistros()
            //                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb134.TB25_EMPRESA.CO_EMP equals tb01.CO_EMP
            //                  where tb134.FLA_STATUS == "A" && tb01.CO_SERIE_REFER == serie && tb01.CO_EMP != coEmp
            //                  select new { tb134.TB25_EMPRESA.CO_EMP, tb134.TB25_EMPRESA.NO_FANTAS_EMP }).Distinct().OrderBy( u => u.NO_FANTAS_EMP );

            //ddl.DataTextField = "NO_FANTAS_EMP";
            //ddl.DataValueField = "CO_EMP";
            //ddl.DataBind();

            AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, false, false);
            //ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="ddl">DropDown de Turma</param>
        /// <param name="serie">Id da série</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaTurmaGrid(DropDownList ddl, string serie, int coEmp) 
        {
            ddl.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                              join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb01.CO_CUR equals tb06.CO_CUR
                              where tb01.CO_SERIE_REFER == serie && tb06.CO_EMP == coEmp
                              select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

            ddl.DataTextField = "CO_SIGLA_TURMA";
            ddl.DataValueField = "CO_TUR";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow) 
            {        
                DropDownList ddlUnidade = (DropDownList)e.Row.FindControl("ddlUnidade");
                HiddenField hdSerieRefer = (HiddenField)e.Row.FindControl("hdSerieRefer");
                HiddenField hdCO_EMP = (HiddenField)e.Row.FindControl("hdCO_EMP");

                int coEmp = int.Parse(hdCO_EMP.Value);
                string serie = hdSerieRefer.Value;

                CarregaUnidadesDdlGrid(ddlUnidade, serie, coEmp);
            }
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            grdAlunos.DataBind();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            grdAlunos.DataBind();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            grdAlunos.DataBind();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaGrid();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e) 
        {
            DropDownList ddlUnidade = (DropDownList)sender;
            GridViewRow gridRow = (GridViewRow)ddlUnidade.Parent.Parent;

            DropDownList ddlTurmaGrid = (DropDownList)gridRow.FindControl("ddlTurma");
            HiddenField hdSerieRefer = (HiddenField)gridRow.FindControl("hdSerieRefer");

            if (ddlTurmaGrid != null) 
            {
                string serie = hdSerieRefer.Value;
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                if (coEmp != 0)
                    CarregaTurmaGrid(ddlTurmaGrid, serie, coEmp);
                else
                    ddlTurmaGrid.Items.Clear();
            }      
        }    
  }
}
