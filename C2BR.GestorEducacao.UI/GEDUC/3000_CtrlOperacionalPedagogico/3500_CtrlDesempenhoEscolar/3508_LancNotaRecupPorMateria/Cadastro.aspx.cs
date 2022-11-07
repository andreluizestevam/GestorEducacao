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
// 02/07/2013| André Nobre Vinagre        | Colocado o tratamento para aceitar o valor zero
//           |                            |
// ----------+----------------------------+-------------------------------------
// 11/06/2014| Victor Martins Machado     | Comentada a linha que valida se o campo de nota da recuperação
//           |                            | para liberar o lançamento, independente  de qualquer validação
//           |                            | (temporário).
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3508_LancNotaRecupPorMateria
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
                CarregaDisciplina();
                CarregaSerieCurso();
                CarregaTurma();
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
                    var materia = par[4];
                    var coBime = par[5];

                    CarregaAnos();
                    ddlAno.SelectedValue = ano;

                    CarregaModalidades();
                    ddlModalidade.SelectedValue = modalidade;

                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = serieCurso;

                    CarregaTurma();
                    ddlTurma.SelectedValue = turma;

                    CarregaDisciplina();
                    ddlDisciplina.SelectedValue = materia;

                    ddlReferencia.SelectedValue = coBime;

                    persist = true;
                    HttpContext.Current.Session.Remove("BuscaSuperior");
                }
            }
            return persist;
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
            decimal nota = 0;

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
                    if (!decimal.TryParse(((TextBox)linha.Cells[2].FindControl("txtNota")).Text, out dcmMedia))
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

            //if (!flgOcoNota)
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma nota foi informada");
            //    return;
            //}

            //bool TirouNota = false;
            //bool Insercao = false;
//--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                nota = 0;
                if (((TextBox)linha.Cells[3].FindControl("txtNota")).Text != "")
                {
                    nota = decimal.Parse(((TextBox)linha.Cells[3].FindControl("txtNota")).Text);

                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma && iTb079.CO_MAT == coMat
                                                 && iTb079.CO_ANO_REF == anoRef
                                              select iTb079).FirstOrDefault();

                    if (tb079 != null)
                    {
                        switch (strRefer)
                        {
                            case "T1":
                                tb079.VL_RECU_BIM1 = nota;
                                break;
                            case "T2":
                                tb079.VL_RECU_BIM2 = nota;
                                break;
                            case "T3":
                                tb079.VL_RECU_BIM3 = nota;
                                break;
                            case "FI":
                                tb079.VL_PROVA_FINAL = nota;
                                break;
                        }
                    }
                    
                    //Insercao = true;

                    if (tb079.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb079) < 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                        return;
                    }   
                }
                else
                {
                    //Caso não tenha nota na grid, será salvo null, retirando a nota de recuperação.

                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079ob = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma && iTb079.CO_MAT == coMat
                                                 && iTb079.CO_ANO_REF == anoRef
                                              select iTb079).FirstOrDefault();

                    if (tb079ob != null)
                    {
                        switch (strRefer)
                        {
                            case "T1":
                                tb079ob.VL_RECU_BIM1 = null;
                                break;
                            case "T2":
                                tb079ob.VL_RECU_BIM2 = null;
                                break;
                            case "T3":
                                tb079ob.VL_RECU_BIM3 = null;
                                break;
                           case "FI":
                                tb079ob.VL_PROVA_FINAL = null;
                                break;
                        }
                        //TirouNota = true;
                        TB079_HIST_ALUNO.SaveOrUpdate(tb079ob, true);
                    }
                }
            }


            //Persiste as informações inseridas nos parâmetros para serem colocadas novamente depois que tudo for salvo, facilitando no momento de calcular diversas médias.
            var parametros = ddlAno.SelectedValue + ";" + ddlModalidade.SelectedValue + ";" + ddlSerieCurso.SelectedValue + ";" + ddlTurma.SelectedValue + ";"
                       + ddlDisciplina.SelectedValue + ";" + ddlReferencia.SelectedValue;
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
            string anoMesMat = ddlAno.SelectedValue;
            string strRefer = ddlReferencia.SelectedValue;
            decimal baseMedia = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP,modalidade,serie).MED_FINAL_CUR ?? 0;
            decimal vlMediaBim = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP,modalidade,serie).VL_NOTA_RECU_BIM ?? 0;
            divGrid.Visible = true;
            liLegen.Visible = true;

            if(vlMediaBim == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe nota mínima de para filtro de aprovação do Bimestre informada no cadastro do curso selecionado. Favor providenciar.");
                return;
            }

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb079.CO_CUR equals tb01.CO_CUR
                              where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat && tb079.CO_ANO_REF == anoMesMat
                              && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia 
                              && tb08.CO_SIT_MAT == "A"
                              select new NotasAluno
                              {
                                  CO_ALU = tb079.CO_ALU, 
                                  NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                  NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                  b1 = tb079.VL_NOTA_BIM1,
                                  b2 = tb079.VL_NOTA_BIM2,
                                  b3 = tb079.VL_NOTA_BIM3,
                                  b4 = tb079.VL_NOTA_BIM4,
                                  b1r = tb079.VL_RECU_BIM1,
                                  b2r = tb079.VL_RECU_BIM2,
                                  b3r = tb079.VL_RECU_BIM3,
                                  b4r = tb079.VL_RECU_BIM4,
                                  pfr = tb079.VL_PROVA_FINAL,
                                  mdFinal = tb079.VL_MEDIA_FINAL,
                                  QtMatRecupFim = tb01.QT_MATE_RECU,
                                  QtMatRecupBim = tb01.QT_MATE_RECU_BIM,
                                  modalidade = modalidade,
                                  serie = serie,
                                  turma = turma,
                                  ano = anoMesMat,
                                  baseMedia = baseMedia,
                                  baseMediaBim = tb01.VL_NOTA_RECU_BIM,
                                  baseMediaFinal = tb01.MED_FINAL_CUR,
                                  referencia = strRefer
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
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
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
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
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
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
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
        /// Método que carrega o dropdown de Disciplinas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int ano = int.Parse(ddlAno.SelectedValue);
            string anostr = ddlAno.SelectedValue;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, anostr, false);
            }
            else
            {
                AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlDisciplina, LoginAuxili.CO_COL, modalidade, serie, ano, false);
            }
        }

        /// <summary>
        /// Verifica a quantidade de matérias nas quais o aluno está de recuperação
        /// </summary>
        /// <param name="CO_ALU"></param>
        /// <returns></returns>
        protected static int VerificaQuantRecupAluno(int CO_ALU, int modalidade, int serie, int turma, string anoMesMat, string strRefer)
        {
            decimal baseMedia = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP,modalidade,serie).MED_FINAL_CUR ?? 0;

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb079.CO_CUR equals tb01.CO_CUR
                              where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat && tb079.CO_ANO_REF == anoMesMat
                              && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_ALU == CO_ALU
                              && tb08.CO_SIT_MAT == "A"
                             select new NotasAluno
                             {
                                 b1 = tb079.VL_NOTA_BIM1,
                                 b2 = tb079.VL_NOTA_BIM2,
                                 b3 = tb079.VL_NOTA_BIM3,
                                 b4 = tb079.VL_NOTA_BIM4,
                                 b1r = tb079.VL_RECU_BIM1,
                                 b2r = tb079.VL_RECU_BIM2,
                                 b3r = tb079.VL_RECU_BIM3,
                                 b4r = tb079.VL_RECU_BIM4,
                                 pfr = tb079.VL_PROVA_FINAL,
                                 mdFinal = tb079.VL_MEDIA_FINAL,
                                 baseMedia = baseMedia,
                                 baseMediaFinal = tb01.MED_FINAL_CUR,
                                 referencia = strRefer
                             }).ToList();

            int qtRecu = 0;
            foreach (var li in resultado)
            {
                    switch (strRefer)
                    {
                        case "B1":
                            if ((li.b1 != null && li.b1 < li.baseMedia) || li.b1r != null)
                                qtRecu++;
                            break;
                        case "B2":
                            if ((li.b2 != null && li.b2 < li.baseMedia) || li.b2r != null)
                                qtRecu++;
                            break;
                        case "B3":
                            if ((li.b3 != null && li.b3 < li.baseMedia) || li.b3r != null)
                                qtRecu++;
                            break;
                        case "B4":
                            if ((li.b4 != null && li.b4 < li.baseMedia) || li.b4r != null)
                                qtRecu++;
                            break;
                        case "S1":
                            if ((li.s1 != null && li.s1 < li.baseMedia) || li.s1r != null)
                                qtRecu++;
                            break;
                        case "S2":
                            if ((li.s2 != null && li.s2 < li.baseMedia) || li.s2r != null)
                                qtRecu++;
                            break;
                        case "FI":
                            if ((li.mdFinal.HasValue && li.mdFinal < li.baseMediaFinal))
                                qtRecu++;
                            break;
                }
            }
            return qtRecu;
        }
        #endregion      

        #region Eventos de Componentes
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

            if (ddlReferencia.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Referência deve ser informada.");
                return;
            }

            CarregaGrid();
        }

        #endregion

        #region Lista de Notas dos Alunos
        public class NotasAluno
        {
            public string NO_ALU { get; set; }
            public decimal? NOTA {
                get
                {
                    decimal? retorno = 0;
                    switch(this.referencia)
                    {
                        case "B1":
                            retorno = this.b1r;
                            break;
                        case "B2":
                            retorno = this.b2r;
                            break;
                        case "B3":
                            retorno = this.b3r;
                            break;
                        case "B4":
                            retorno = this.b4r;
                            break;
                        case "S1":
                            retorno = this.s1r;
                            break;
                        case "S2":
                            retorno = this.s2r;
                            break;
                        case "FI":
                            retorno = this.pfr;
                            break;
                    }
                    return retorno;
                }
            }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public bool notaHabilit { get; set; }
            public string RECUP {
                get
                {
                    string retorno = "Não";
                    decimal? mediaAlu = 0;
                    switch(this.referencia)
                    {
                        case "B1":
                            retorno = ((this.b1 != null && this.b1 < this.baseMediaBim) || this.b1r != null)  ? "Sim" : "Não";
                            mediaAlu = this.b1;

                            break;
                        case "B2":
                            retorno = ((this.b2 != null && this.b2 < this.baseMediaBim) || this.b2r != null) ? "Sim" : "Não";
                            mediaAlu = this.b2;
                            break;
                        case "B3":
                            retorno = ((this.b3 != null && this.b3 < this.baseMediaBim) || this.b3r != null) ? "Sim" : "Não";
                            mediaAlu = this.b3;
                            break;
                        case "B4":
                            retorno = ((this.b4 != null && this.b4 < this.baseMediaBim) || this.b4r != null) ? "Sim" : "Não";
                            mediaAlu = this.b4;
                            break;
                        
                        case "FI":
                            int qtRecu = VerificaQuantRecupAluno(this.CO_ALU, this.modalidade, this.serie, this.turma, this.ano, this.referencia);
                            retorno = ((this.mdFinal.HasValue && this.mdFinal < this.baseMediaFinal) || this.mdFinal != null) ? "Sim" : "Não";
                            mediaAlu = this.mdFinal;
                    #region Desuso
                                //Calcula a média final, somando as notas dos 4 bimestres dividindo por 2 e verificando se está abaixo do mínimo permitido no cadastro do curso.
                                //Pega a nota maior entre a nota do bimestre ou a nota da prova de recuperação, identificando se ela foi feita ou não 
                                decimal nota1 = 0;
                                //Usado para validar se a nota do 4º bimestre foi lançada
                                bool nota1LC = false;
                                if (this.b1r.HasValue)
                                {
                                    nota1 = (this.b1.HasValue ? (this.b1r.Value > this.b1.Value ? this.b1r.Value : this.b1.Value) : this.b1r.Value);
                                }
                                else
                                {
                                    nota1 = this.b1.HasValue ? this.b1.Value : 0;
                                    nota1LC = (this.b1.HasValue ? true : false);
                                }

                                decimal nota2 = 0;
                                //Usado para validar se a nota do 4º bimestre foi lançada
                                bool nota2LC = false;
                                if (this.b2r.HasValue)
                                {
                                    nota2 = (this.b2.HasValue ? (this.b2r.Value > this.b2.Value ? this.b2r.Value : this.b2.Value) : this.b2r.Value);
                                }
                                else
                                {
                                    nota2 = this.b2.HasValue ? this.b2.Value : 0;
                                    nota2LC = (this.b2.HasValue ? true : false);
                                }

                                decimal nota3 = 0;
                                //Usado para validar se a nota do 4º bimestre foi lançada
                                bool nota3LC = false;
                                if (this.b3r.HasValue)
                                {
                                    nota3 = (this.b3.HasValue ? (this.b3r.Value > this.b3.Value ? this.b3r.Value : this.b3.Value) : this.b3r.Value);
                                }
                                else
                                {
                                    nota3 = this.b3.HasValue ? this.b3.Value : 0;
                                    nota3LC = (this.b3.HasValue ? true : false);
                                }

                                decimal nota4 = 0;
                                //Usado para validar se a nota do 4º bimestre foi lançada
                                bool nota4LC = false;
                                if (this.b4r.HasValue)
                                {
                                    nota4 = (this.b4.HasValue ? (this.b4r.Value > this.b4.Value ? this.b4r.Value : this.b4.Value) : this.b4r.Value);
                                }
                                else
                                {
                                    nota4 = this.b4.HasValue ? this.b4.Value : 0;
                                    nota4LC = (this.b4.HasValue ? true : false);
                                }

                                //decimal nota1 = this.b1.HasValue ? this.b1.Value : 0;
                                //decimal nota2 = this.b2.HasValue ? this.b2.Value : 0;
                                //decimal nota3 = this.b3.HasValue ? this.b3.Value : 0;
                                //decimal nota4 = this.b4.HasValue ? this.b4.Value : 0;
                                decimal total = nota1 + nota2 + nota3 + nota4;
                                decimal media = total / 4;
                                string mediaS = media.ToString("N2");
                                decimal mediaV = decimal.Parse(mediaS);

                                //Filtra se o aluno está reprovado por quantidade de matérias em recuperação, ou se está aprovado ou em recuperação final
                                if (qtRecu > this.QtMatRecupFim)
                                {
                                    retorno = "Rep";
                                }
                                else
                                {
                                    if (mediaV < this.baseMedia)
                                        retorno = "Sim";
                                    else
                                        retorno = "Não";
                                }
                                
                                //Valida se alguma das notas bimestrais já foi lançada, caso já tenha sido então mostra o calculo da média, caso não manda null para virar NL
                                if ((nota1LC == false) && (nota2LC == false) && (nota3LC == false) && (nota4LC == false))
                                    mediaAlu = null;
                                else
                                    mediaAlu = mediaV;

#endregion
                            break;
                    }

                    if ((this.referencia == "B2") && (DateTime.Now.Year == 2014))
                        this.notaHabilit = true;
                    else
                        this.notaHabilit = (retorno == "Sim" ? true : false);

                    //Recebe a média da nota do aluno de acordo com o bimestre selecionado
                    this.MEDALU = (mediaAlu.HasValue ? mediaAlu.Value.ToString() : "NL");
                    return retorno;              
                }
            }
            public bool Habilitar
            {
                get
                {
                    // Comentado temporariamente para liberar o lançamento da média
                    //return this.RECUP == "Sim" ? true : false;
                    return true;
                }
            }
            public decimal? b1 { get; set; }
            public decimal? b2 { get; set; }
            public decimal? b3 { get; set; }
            public decimal? b4 { get; set; }
            public decimal? s1 { get; set; }
            public decimal? s2 { get; set; }
            public decimal? b1r { get; set; }
            public decimal? b2r { get; set; }
            public decimal? b3r { get; set; }
            public decimal? b4r { get; set; }
            public decimal? s1r { get; set; }
            public decimal? s2r { get; set; }
            public decimal? pfr { get; set; }
            public decimal? mdFinal { get; set; }
            public string MEDALU { get; set; }
            public decimal baseMedia { get; set; }
            public decimal? baseMediaBim { get; set; }
            public int? QtMatRecupFim { get; set; }
            public int? QtMatRecupBim { get; set; }
            public decimal? baseMediaFinal { get; set; }
            public int modalidade { get; set; }
            public int serie { get; set; }
            public int turma { get; set; }
            public string ano { get; set; }
            public string referencia { get; set; }
            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(9, '0'); } }
        }
        #endregion
    }
}