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
// 24/06/2013| Victor Martins Machado     | Incluído a regra de atualização da média do agrupador das matérias,
//           |                            | de acordo com a grade de notas anual, e alteração da combo de 
//           |                            | disciplinas que só devem retornar as matérias que não possuem filhos,
//           |                            | não são agrupadores.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 02/07/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3514_CalculoMediaEsplanada
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
            bool flgOcoMedia = false;

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            int intBimestre = ddlBimestre.SelectedValue == "B1" ? 1 : ddlBimestre.SelectedValue == "B2" ? 2 : ddlBimestre.SelectedValue == "B3" ? 3 : 4;
            string anoRef = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || coMat == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Modalidade, Série, Turma e Disciplina devem ser informados.");
                return;
            }

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                //Verifica se existiu ocorrência de nota
                if (((TextBox)linha.Cells[6].FindControl("txtMedia")).Text != "-")
                {
                    flgOcoMedia = true;
                }
            }

            if (!flgOcoMedia)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma média foi calculada.");
                return;
            }

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            #region Valida Bimestres
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

            #endregion
            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((TextBox)linha.Cells[6].FindControl("txtMedia")).Text != "-")
                {
                    //------------> Recebe o código do aluno
                    int coAlu = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[0]);

                    //------------> Recebe o dado da tabela de histórico de acordo com as chaves passadas
                    TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                              where iTb079.CO_ALU == coAlu && iTb079.CO_ANO_REF == ddlAno.SelectedValue
                                                 && iTb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                                 && iTb079.CO_CUR == serie && iTb079.CO_TUR == turma
                                                 && iTb079.CO_MAT == coMat
                                              select iTb079).FirstOrDefault();

                    int? idAgr = (from g in TB43_GRD_CURSO.RetornaTodosRegistros() where g.CO_EMP == LoginAuxili.CO_EMP && g.CO_ANO_GRADE == anoRef && g.CO_CUR == serie && g.TB44_MODULO.CO_MODU_CUR == modalidade && g.CO_MAT == coMat select new { g.ID_MATER_AGRUP }).FirstOrDefault().ID_MATER_AGRUP;

                    TB079_HIST_ALUNO haAgr = null;

                    if (idAgr != null)
                    {
                        haAgr = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAlu, modalidade, serie, anoRef, idAgr.Value);
                    }

                    string v = "";
                    if (tb079 != null)
                    {
                        //----------------> Atribui o valor de média informado de acordo com o bimestre
                        switch (intBimestre)
                        {
                            case 1:
                                v = ((TextBox)linha.Cells[6].FindControl("txtMedia")).Text;
                                tb079.VL_NOTA_BIM1 = v != "" ? decimal.Parse(v) : (decimal?)null;
                                tb079.VL_NOTA_BIM1_ORI = v != "" ? decimal.Parse(v) : (decimal?)null;
                                break;
                            case 2:
                                v = ((TextBox)linha.Cells[6].FindControl("txtMedia")).Text;
                                tb079.VL_NOTA_BIM2 = v != "" ? decimal.Parse(v) : (decimal?)null;
                                tb079.VL_NOTA_BIM2_ORI = v != "" ? decimal.Parse(v) : (decimal?)null;
                                break;
                            case 3:
                                v = ((TextBox)linha.Cells[6].FindControl("txtMedia")).Text;
                                tb079.VL_NOTA_BIM3 = v != "" ? decimal.Parse(v) : (decimal?)null;
                                tb079.VL_NOTA_BIM3_ORI = v != "" ? decimal.Parse(v) : (decimal?)null;
                                break;
                            case 4:
                                v = ((TextBox)linha.Cells[6].FindControl("txtMedia")).Text;
                                tb079.VL_NOTA_BIM4 = v != "" ? decimal.Parse(v) : (decimal?)null;
                                tb079.VL_NOTA_BIM4_ORI = v != "" ? decimal.Parse(v) : (decimal?)null;
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
            decimal? dcmN1, dcmN2, dcmN3, dcmN4, dcmN5, dcmN6;
            //decimal dcmCalcNotas;

            int idMateria = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                             where tb02.CO_MAT == materia
                             select new { tb02.ID_MATERIA }).First().ID_MATERIA;

            divGrid.Visible = true;

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia
                             && tb08.CO_SIT_MAT == "A"
                             && (coAlu != 0 ? tb08.CO_ALU == coAlu : 0 == 0)
                             select new NotasAluno
                             {
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE
                             }).ToList();

            string classCur = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                               where tb01.CO_CUR == serie
                               && tb01.CO_EMP == LoginAuxili.CO_EMP
                               && tb01.TB44_MODULO.CO_MODU_CUR == modalidade
                               select new { tb01.CO_NIVEL_CUR }).FirstOrDefault().CO_NIVEL_CUR;

            foreach (var res in resultado)
            {
                int c = 0; // Contador do cálculo de média
                decimal so = 0; // Somatório do cálculo de média

                dcmN1 = null;
                dcmN2 = null;
                dcmN3 = null;

                var lstNotas = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                where tb49.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                                && tb49.CO_ANO == intAno
                                && tb49.TB01_CURSO.CO_CUR == serie
                                && tb49.TB06_TURMAS.CO_TUR == turma
                                && tb49.TB107_CADMATERIAS.ID_MATERIA == idMateria
                                && tb49.CO_BIMESTRE == ddlBimestre.SelectedValue
                                && tb49.TB07_ALUNO.CO_ALU == res.CO_ALU
                                select new
                                {
                                    tb49.TB273_TIPO_ATIVIDADE.CO_SIGLA_ATIV,
                                    tb49.VL_NOTA
                                }).ToList();

                if (lstNotas.Count() > 0)
                {
                    #region Calcula a nota dos pontos da avaliação mensal (AVM)

                    var lstN1 = (from n1 in lstNotas
                                 where n1.CO_SIGLA_ATIV.ToUpper() == "AVM"
                                 select new { n1.VL_NOTA }).OrderByDescending(p => p.VL_NOTA);

                    if (lstN1.Count() >= 1)
                    {
                        //dcmN1 = lstN1.Count() >= 3 ? lstN1.Take(3).Sum(p => p.VL_NOTA) : lstN1.Sum(p => p.VL_NOTA);
                        dcmN1 = lstN1.Sum(s => s.VL_NOTA);
                        dcmN1 = dcmN1 > 8 ? 8 : dcmN1;

                        res.NOTA1 = dcmN1.Value.ToString("N2");
                    }
                    else
                    {
                        res.NOTA1 = "-";
                    }

                    #endregion

                    #region Calcula a nota dos pontos da avaliação bimestral (AVB)

                    var lstN2 = (from n1 in lstNotas
                                 where n1.CO_SIGLA_ATIV.ToUpper() == "AVB"
                                 select new { n1.VL_NOTA }).OrderByDescending(p => p.VL_NOTA);

                    if (lstN2.Count() >= 1)
                    {
                        dcmN2 = lstN2.Sum(s => s.VL_NOTA);
                        dcmN2 = dcmN2 > 10 ? 10 : dcmN2;

                        res.NOTA2 = dcmN2.Value.ToString("N2");
                    }
                    else
                    {
                        res.NOTA2 = "-";
                    }


                    #endregion

                    #region Calcula a nota dos pontos de nota interativa (NIN)

                    var lstN3 = (from n1 in lstNotas
                                 where n1.CO_SIGLA_ATIV.ToUpper() == "NIN"
                                 select new { n1.VL_NOTA }).OrderByDescending(p => p.VL_NOTA);

                    if (lstN3.Count() >= 1)
                    {
                        dcmN3 = lstN3.Sum(s => s.VL_NOTA);
                        dcmN3 = dcmN3 > 2 ? 2 : dcmN3;

                        res.NOTA3 = dcmN3.Value.ToString("N2");
                    }
                    else
                    {
                        res.NOTA3 = "-";
                    }


                    #endregion

                    #region Calcula a média

                    dcmN1 = dcmN1 == null ? 0 : dcmN1.Value;
                    dcmN2 = dcmN2 == null ? 0 : dcmN2.Value;
                    dcmN3 = dcmN3 == null ? 0 : dcmN3.Value;

                    decimal media = (dcmN1.Value + dcmN2.Value + dcmN3.Value) / 2;

                    res.MEDIA = media.ToString("N2");

                    #endregion
                }

            }

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
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

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
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

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

        private void CarregaAluno()
        {
            ddlAluno.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   where tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_TUR == turma &&
                                   tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_CUR == serie && tb08.CO_SIT_MAT == "A"
                                   select new
                                   {
                                       tb08.TB07_ALUNO.CO_ALU,
                                       tb08.TB07_ALUNO.NO_ALU
                                   }).OrderBy(r => r.NO_ALU);

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            //int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            //int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            //if (turma == 0 || modalidade == 0 || serie == 0)
            //    return;

            //ddlDisciplina.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
            //                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE.Equals(ddlAno.SelectedValue)
            //                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
            //                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
            //                            join tbAgr in TB43_GRD_CURSO.RetornaTodosRegistros() on tb43.CO_MAT equals tbAgr.ID_MATER_AGRUP into agr
            //                            from tbAgr in agr.DefaultIfEmpty()
            //                            where tb107.CO_EMP == LoginAuxili.CO_EMP
            //                            && tbAgr == null
            //                            select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(g => g.NO_MATERIA);

            //ddlDisciplina.DataTextField = "NO_MATERIA";
            //ddlDisciplina.DataValueField = "CO_MAT";
            //ddlDisciplina.DataBind();

            //ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            if (turma == 0 || modalidade == 0 || serie == 0)
                return;

            ddlDisciplina.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                        where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_ANO_GRADE.Equals(ddlAno.SelectedValue)
                                        join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                        join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                        where tb107.CO_EMP == LoginAuxili.CO_EMP
                                        select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(g => g.NO_MATERIA);

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
            public string CO_BIMESTRE { get; set; }
            public string NOTA1 { get; set; }
            public string NOTA2 { get; set; }
            public string NOTA3 { get; set; }
            public string MEDIA { get; set; }
            public int NU_NIRE { get; set; }
            public int CO_ALU { get; set; }
            public string DescNU_NIRE { get { return this.NU_NIRE.ToString().PadLeft(7, '0'); } }
        }
        #endregion
    }
}