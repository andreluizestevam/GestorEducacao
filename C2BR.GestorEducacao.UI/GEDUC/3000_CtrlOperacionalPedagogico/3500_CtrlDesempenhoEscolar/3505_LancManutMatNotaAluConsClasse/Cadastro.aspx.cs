//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO, POR MATÉRIA, DE NOTA AO ALUNO DE CONSELHO DE CLASSE
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
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3505_LancManutMatNotaAluConsClasse
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
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            divGrid.Visible = false;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnSave_Click(object sender, EventArgs e)
        {            
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || coAlu == 0)
            {
                grdBusca.DataBind();
                return;
            }

//--------> Varre toda a grid de busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
//------------> Recebe o código da matéria
                int materia = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

//------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                TB079_HIST_ALUNO tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAlu, modalidade, serie, anoRef, materia);

                if (tb079 == null)
                {
                    var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    refAluno.TB25_EMPRESA1Reference.Load();
                    refAluno.TB25_EMPRESAReference.Load();
                    tb079.CO_EMP = refAluno.TB25_EMPRESA.CO_EMP;
                    tb079.CO_ALU = refAluno.CO_ALU;
                    tb079.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb079.CO_ANO_REF = anoRef;
                    tb079.CO_MODU_CUR = modalidade;
                    tb079.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(tb079.CO_MODU_CUR);
                    tb079.CO_CUR = serie;                    
                }
                               
//------------> Faz a verificação para saber se o dado digitado para a nota Conselho é válido
                if (((DropDownList)linha.Cells[6].FindControl("txtNotaConselho")).Enabled)
                {
                    if (((DropDownList)linha.Cells[6].FindControl("txtNotaConselho")).SelectedValue == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Decisão do Conselho deve ser selecionada");
                        return;
                    }
                    else
                        if (((DropDownList)linha.Cells[6].FindControl("txtNotaConselho")).SelectedValue == "R")
                            tb079.VL_NOTA_CONSELHO = 99;
                        else
                        {
                            decimal? mediaCurso = RetornaMediaCurso();
                            decimal? vlrNotaConselho = mediaCurso - tb079.VL_MEDIA_FINAL;
                            tb079.VL_NOTA_CONSELHO = vlrNotaConselho;
                        }                    
                }

                if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                    return;
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }        

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que retorna a Média da Série/Curso
        /// </summary>
        /// <returns>Média do curso(decimal)</returns>
        private decimal? RetornaMediaCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            decimal dcmMediaAprov;            

//--------> Recebe os parâmetros da instituição para verificar o campo de média final do curso
            TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            TB83_PARAMETRO tb83 = null;
            TB44_MODULO tb44 = null;

            if (tb149.TP_CTRLE_AVAL == TipoControle.I.ToString())
                dcmMediaAprov = tb149.VL_MEDIA_APROV_DIRETA != null ? tb149.VL_MEDIA_APROV_DIRETA.Value : 0;
            else if (tb149.TP_CTRLE_AVAL == TipoControle.U.ToString())
            {
                if (tb83 == null)
                    tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                dcmMediaAprov = tb83.VL_MEDIA_APROV_DIRETA != null ? tb83.VL_MEDIA_APROV_DIRETA.Value : 0;
            }
            else
            {
                if (tb44 == null)
                    tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                dcmMediaAprov = tb44.VL_MEDIA_APROV_DIRETA != null ? tb44.VL_MEDIA_APROV_DIRETA.Value : 0;
            }

            return dcmMediaAprov;
        }

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || coAlu == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGrid.Visible = true;            

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb08.CO_ALU == coAlu                              
                             select new
                             {
                                 tb02.CO_MAT, tb107.NO_MATERIA, tb107.NO_SIGLA_MATERIA, tb079.CO_ANO_REF, tb079.CO_CUR, tb08.CO_SIT_MAT,
                                 CO_ALU_CAD = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."), ENABLED = tb079.CO_STA_APROV_MATERIA == "C" ? true : false,
                                 tb079.VL_MEDIA_FINAL, tb079.VL_PROVA_FINAL, tb079.VL_NOTA_CONSELHO,
                                 SINTESE_BIM = (tb079.VL_NOTA_BIM1 + tb079.VL_NOTA_BIM2 + tb079.VL_NOTA_BIM3 + tb079.VL_NOTA_BIM4) != null ? (tb079.VL_NOTA_BIM1 + tb079.VL_NOTA_BIM2 + tb079.VL_NOTA_BIM3 + tb079.VL_NOTA_BIM4) / 4 : null,
                                 CO_STA_APROV_MATERIA = tb079.CO_STA_APROV_MATERIA == "A" ? "Aprovado" : tb079.CO_STA_APROV_MATERIA == "C" ? "Em Conselho" : tb079.CO_STA_APROV_MATERIA == "R" ? "Reprovado" : tb079.CO_STA_APROV_MATERIA == "P" ? "Em Prova Final" : "",
                                 DCO_SIT_MAT = tb08.CO_SIT_MAT == "A" ? "Em Aberto" : tb08.CO_SIT_MAT == "X" ? "Transferido" : tb08.CO_SIT_MAT == "F" ? "Finalizado" : tb08.CO_SIT_MAT == "T" ? "Trancado" : tb08.CO_SIT_MAT == "C" ? "Cancelado" : "Pendente",
                                 STATUS = tb08.CO_STA_APROV == "A" ? "Aprovado" : tb08.CO_STA_APROV == "R" ? "Reprovado" : ""
                              }).ToList();

            var resultado2 = (from result in resultado
                              select new
                              {
                                 result.CO_MAT, result.NO_MATERIA, result.NO_SIGLA_MATERIA, result.CO_ANO_REF, result.CO_ALU_CAD, result.CO_CUR,
                                 VL_MEDIA_FINAL = result.VL_MEDIA_FINAL == null ? "******" : result.VL_MEDIA_FINAL.Value.ToString("0.00"),
                                 SB = result.SINTESE_BIM == null ? "*****" : result.SINTESE_BIM.Value.ToString("0.00"),
                                 VL_PROVA_FINAL = result.VL_PROVA_FINAL == null ? "*****" : result.VL_PROVA_FINAL.Value.ToString("0.00"),
                                 result.ENABLED, result.CO_STA_APROV_MATERIA, result.CO_SIT_MAT, result.DCO_SIT_MAT, result.STATUS,
                                 VL_NOTA_CONSELHO = result.VL_NOTA_CONSELHO == null && result.ENABLED == false ? "*****" : result.VL_NOTA_CONSELHO == null && result.ENABLED == true ? null : result.VL_NOTA_CONSELHO.Value.ToString("0.00")
                              }).ToList().OrderBy( r => r.NO_MATERIA );


            // Habilita o botão de salvar
           
            grdBusca.DataKeyNames = new string[] { "CO_MAT" };

            grdBusca.DataSource = resultado2.Count() > 0 ? resultado2 : null;
            grdBusca.DataBind();
        }

        /// <summary>
        /// Método que carrega o DropDown de Anos
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
        /// Método que carrega o DropDown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            }
            else
            {
                int ano = int.Parse(ddlAno.SelectedValue);
                ddlModalidade.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                             && rm.CO_ANO_REF == ano
                                            select new
                                            {
                                                mo.DE_MODU_CUR,
                                                rm.CO_MODU_CUR
                                            }).Distinct();
            }
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o DropDown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                                where tb01.CO_MODU_CUR == modalidade
                                                join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                                where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                                select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);
                }
                else
                {
                    int ano = int.Parse(ddlAno.SelectedValue);
                    ddlSerieCurso.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                                join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                                                join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                                                where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                                && rm.CO_MODU_CUR == modalidade
                                                && rm.CO_ANO_REF == ano
                                                select new
                                                {
                                                    c.NO_CUR,
                                                    rm.CO_CUR
                                                }).Distinct();
                }
                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o DropDown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                           select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR });
                }
                else
                {
                    int ano = int.Parse(ddlAno.SelectedValue);
                    ddlTurma.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                                           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                           && rm.CO_MODU_CUR == modalidade
                                           && rm.CO_CUR == serie
                                           && rm.CO_ANO_REF == ano
                                           select new
                                           {
                                               t.NO_TURMA,
                                               rm.CO_TUR,
                                               t.CO_SIGLA_TURMA
                                           }).Distinct();
                }
                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o DropDown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (turma != 0 && modalidade != 0 && serie != 0)
            {
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       join tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb079.CO_ALU
                                       where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                                       && tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue && tb08.CO_CUR == serie && tb08.CO_TUR == turma
                                       && tb08.CO_SIT_MAT == "A" && tb079.CO_STA_APROV_MATERIA == "C"
                                       select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy( a => a.NO_ALU );

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();

                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }
        #endregion       

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAluno();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlConselho = (DropDownList)e.Row.FindControl("txtNotaConselho");
                HiddenField hfMateria = (HiddenField)e.Row.FindControl("hdMat");
                int materia = int.Parse(hfMateria.Value);

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
                string anoRef = ddlAno.SelectedValue;

                if (modalidade == 0 || serie == 0 || turma == 0 || coAlu == 0)
                {
                    grdBusca.DataBind();
                    return;
                }

                decimal dcmNotaConselho = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                           where tb079.CO_ALU == coAlu && tb079.CO_ANO_REF == anoRef && tb079.CO_CUR == serie
                                           && tb079.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb079.CO_MAT == materia
                                           select new { tb079.VL_NOTA_CONSELHO }).FirstOrDefault().VL_NOTA_CONSELHO.Value;

                if (dcmNotaConselho == 99 && ddlConselho.Enabled == true)
                    ddlConselho.SelectedValue = "R";
                else if (dcmNotaConselho != 99 && ddlConselho.Enabled == true) 
                    ddlConselho.SelectedValue = "A";
            }
        }
    }
}