//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: ******
// DATA DE CRIAÇÃO: 16/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 16/05/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 02/07/2013| André Nobre Vinagre        | Colocado o tratamento para aceitar o valor zero
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3510_LancNotaProvaFinal
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
            CarregaDisciplina();
            CarregaSerieCurso();
            CarregaTurma();
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
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || coMat == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            decimal dcmMedia;

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se existiu ocorrência de nota
                if (((TextBox)linha.Cells[3].FindControl("txtNota")).Text != "")
                {
                    if (!decimal.TryParse(((TextBox)linha.Cells[3].FindControl("txtNota")).Text, out dcmMedia))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Existe nota de recuperação informada inválida.");
                        return;
                    }

                    flgOcoNota = true;
                    //----------------> Média deve estar entre 0 e 100
                    if (Decimal.Parse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text) > 100)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Nota deve estar entre 0 e 100");
                        return;
                    }
                }
            }

            if (!flgOcoNota)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma nota foi informada");
                return;
            }
            
//--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((TextBox)linha.Cells[3].FindControl("txtNota")).Text != "")
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
                        decimal? notaProvaFinal = decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtNota")).Text);
                        tb079.VL_PROVA_FINAL = notaProvaFinal == 0 ? null : notaProvaFinal;

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

            divGrid.Visible = true;

            var curso = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            if (curso == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A série/curso não foi localizada.");
                return;
            }
            if (curso.MED_FINAL_CUR == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A média final da série/curso escolhida não foi especificada.");
                return;
            }

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU into resultadoM
                             from tb08 in resultadoM.DefaultIfEmpty()
                              where tb08 != null
                                 && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                              && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia 
                              && tb08.CO_SIT_MAT == "A"
                              join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR into resultadoC
                              from tb01 in resultadoC.DefaultIfEmpty()
                              where tb01 != null
                              select new NotasAluno
                              {
                                  CO_ALU = tb079.CO_ALU, 
                                  NO_ALU = tb08.TB07_ALUNO.NO_ALU, 
                                  NU_NIRE = tb08.TB07_ALUNO.NU_NIRE, 
                                  NOTA = tb079.VL_PROVA_FINAL,
                                  ENABLED = (((tb079.VL_MEDIA_FINAL ?? 0) < (tb01.MED_FINAL_CUR ?? 0)) || (tb079.VL_PROVA_FINAL != null && tb079.VL_PROVA_FINAL > 0)),
                                  Status = ((tb079.VL_MEDIA_FINAL ?? 0) >= (tb01.MED_FINAL_CUR ?? 0) ? "Aprovado" : "Pendente")
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
            ddlAno.Items.Clear();
            ddlAno.Items.AddRange(AuxiliBaseApoio.AnosDDL(LoginAuxili.CO_EMP));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlModalidade.Items.Clear();
                ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
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
                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataBind();

                ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlSerieCurso.Items.Clear();
                ddlSerieCurso.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlAno.SelectedValue, true));
            }
            else
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
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

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();

                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }        
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.AddRange(AuxiliBaseApoio.TurmasDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue, ddlAno.SelectedValue, true));
            }
            else
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
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

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //todo: Adicionar a disciplina na classe.
        /// <summary>
        /// Método que carrega o dropdown de Disciplinas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaDisciplina()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
            ddlDisciplina.Items.Clear();
            ddlDisciplina.Items.AddRange(AuxiliBaseApoio.MateriasDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue, ddlTurma.SelectedValue, ddlAno.SelectedValue, true));
            }
            else
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                int ano = int.Parse(ddlAno.SelectedValue);
                ddlDisciplina.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                            && rm.CO_MODU_CUR == modalidade
                                            && rm.CO_CUR == serie
                                            && rm.CO_ANO_REF == ano
                                            && rm.CO_TUR == turma
                                            select new
                                            {
                                                tb107.NO_MATERIA,
                                                rm.CO_MAT,
                                                tb107.ID_MATERIA
                                            }).Distinct();
                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataBind();

                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
            }
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
            CarregaModalidades();
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

            CarregaGrid();
        }

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            public string NO_ALU { get; set; }
            public decimal? NOTA { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public bool ENABLED { get; set; }
            public string Status { get; set; }

            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }
        }
        #endregion
    }
}