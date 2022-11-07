//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: ATERAÇÃO DE GRADE DO ALUNO
// DATA DE CRIAÇÃO: 11/04/2014
//------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            | 

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
using System.Data;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar._3042_AlteraGradeAluno
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
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAno();
                CarregaModalidade();
                CarregaSerie();
                CarregaTurma();
                CarregaAluno();
                CarregaSerieExt();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //foreach (GridViewRow lst in grdGradeAluno.Rows)
            //{
            //    string coFlag = ((DropDownList)lst.Cells[4].FindControl("ddlStatus")).SelectedValue;
            //    CheckBox chk = ((CheckBox)lst.Cells[0].FindControl("ckSelect"));

            //    if ((chk.Checked) && (coFlag == "N"))
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "A Matéria selecionada não pode ser salva com o Status 'Não Matriculado'.");
            //        return;
            //    }
            //}

            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;
            bool novoRegistro = false;

            TB48_GRADE_ALUNO tb48 = new TB48_GRADE_ALUNO();
            TB079_HIST_ALUNO tb79 = new TB079_HIST_ALUNO();

            RegistroLog log = new RegistroLog();

            //Percorre os registros da grid de Grade do Aluno. Caso o aluno ainda não tenha a matéria selecionada na sua grade, ele cria um novo registro
            foreach (GridViewRow li in grdGradeAluno.Rows)
            {
                //CheckBox chk = ((CheckBox)li.Cells[0].FindControl("ckSelect"));

                //if (chk.Checked)
                //{
                int coMat = int.Parse(((HiddenField)li.Cells[4].FindControl("hidCoMat")).Value);
                string ano = ((HiddenField)li.Cells[4].FindControl("hidCoAno")).Value;
                string coFlag = ((DropDownList)li.Cells[4].FindControl("ddlStatus")).SelectedValue;

                tb48 = carregaGradeAluno(ano, coAlu, coMat, coMod, coTur);

                if (tb48 != null)
                {
                    tb48.CO_FLAG_STATUS = coFlag;
                    TB48_GRADE_ALUNO.SaveOrUpdate(tb48, true);
                }
                else
                {
                    //Insere um novo registro referente à nova matéria no histórico do aluno
                    tb79 = new TB079_HIST_ALUNO();
                    tb79.CO_ALU = coAlu;
                    tb79.CO_ANO_REF = coAno;
                    tb79.CO_CUR = coCur;
                    tb79.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb79.CO_MAT = coMat;
                    tb79.CO_MODU_CUR = coMod;
                    tb79.CO_TUR = coTur;
                    tb79.CO_USUARIO = (int?)LoginAuxili.CO_COL;
                    //tb79.DT_LANC = DateTime.Now;
                    tb79.DT_LANC = DateTime.Parse("05/01/" + coAno);
                    tb79.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
                    tb79.FL_TIPO_LANC_MEDIA = "N";
                    tb79.CO_FLAG_STATUS = "A";

                    //Insere um novo registro para a Matéria selecionada na grade do Aluno em questão.
                    TB48_GRADE_ALUNO tb48ob = new TB48_GRADE_ALUNO();

                    tb48ob.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb48ob.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48ob.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
                    tb48ob.CO_CUR = coCur;
                    tb48ob.CO_ANO_MES_MAT = coAno;
                    tb48ob.NU_SEM_LET = "1";
                    tb48ob.CO_MAT = coMat;
                    tb48ob.CO_STAT_MATE = "E";
                    tb48ob.CO_TUR = coTur;
                    tb48ob.CO_FLAG_STATUS = coFlag;
                    tb48ob.CO_CUR_ORIGE = coCur;
                    novoRegistro = true;

                    TB48_GRADE_ALUNO.SaveOrUpdate(tb48ob, true);
                    TB079_HIST_ALUNO.SaveOrUpdate(tb79, true);
                }
                //}
            }

            //Grava o Log de Edição quando ele clicar em gravar.
            log.RegistroLOG(tb48, RegistroLog.ACAO_EDICAO);

            foreach (GridViewRow l in grdGradeCrusos.Rows)
            {
                if (((CheckBox)l.Cells[0].FindControl("ckSelect")).Checked)
                {
                    int coMat = int.Parse(((HiddenField)l.Cells[0].FindControl("hidCoMat")).Value);
                    int coCurOrige = int.Parse(((HiddenField)l.Cells[0].FindControl("hidCoCur")).Value);

                    //Insere um novo registro referente à nova matéria no histórico do aluno
                    tb79 = new TB079_HIST_ALUNO();
                    tb79.CO_ALU = coAlu;
                    tb79.CO_ANO_REF = coAno;
                    tb79.CO_CUR = coCur;
                    tb79.CO_EMP = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).CO_EMP;
                    tb79.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb79.CO_MAT = coMat;
                    tb79.CO_MODU_CUR = coMod;
                    tb79.CO_TUR = coTur;
                    tb79.CO_USUARIO = (int?)LoginAuxili.CO_COL;
                    //tb79.DT_LANC = DateTime.Now;
                    tb79.DT_LANC = DateTime.Parse("05/01/" + coAno);
                    tb79.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
                    tb79.FL_TIPO_LANC_MEDIA = "N";
                    tb79.CO_FLAG_STATUS = "A";

                    //Insere a nova matéria na grade do Aluno
                    tb48 = new TB48_GRADE_ALUNO();
                    tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb48.CO_ANO_MES_MAT = coAno;
                    tb48.CO_CUR = coCur;
                    tb48.CO_MAT = coMat;
                    tb48.CO_MODU_CUR = coMod;
                    tb48.CO_STAT_MATE = "E";
                    tb48.CO_TUR = coTur;
                    tb48.NU_SEM_LET = "1";
                    tb48.CO_FLAG_STATUS = "A";
                    tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
                    tb48.CO_CUR_ORIGE = coCurOrige;

                    TB48_GRADE_ALUNO.SaveOrUpdate(tb48, true);
                    TB079_HIST_ALUNO.SaveOrUpdate(tb79, true);

                    novoRegistro = true;
                }
            }

            //Grava o Log de Gravar apenas se ele inserir uma nova matéria à grade do Aluno
            if (novoRegistro == true)
            {
                log.RegistroLOG(tb48, RegistroLog.ACAO_GRAVAR);
                AuxiliPagina.RedirecionaParaPaginaSucesso("Matéria(s) associada(s) à Grade com Sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Alterado com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());

            #region Desuso

            //// Passa pela grade do aluno atualizando o status das matérias alteradas
            //TB48_GRADE_ALUNO ga;
            //TB079_HIST_ALUNO tb79;

            //string coFlag = "A";
            //int coMat = 0;
            //foreach (GridViewRow l in grdGradeAluno.Rows)
            //{
            //    coFlag = ((DropDownList)l.Cells[4].FindControl("ddlStatus")).SelectedValue;
            //    coMat = int.Parse(((HiddenField)l.Cells[0].FindControl("hidCoMat")).Value);
            //    ga = TB48_GRADE_ALUNO.RetornaTodosRegistros().Where(w => w.CO_EMP == coEmp && w.CO_MODU_CUR == coMod && w.CO_CUR == coCur && w.CO_TUR == coTur && w.CO_ALU == coAlu && w.CO_ANO_MES_MAT == coAno && w.CO_MAT == coMat).FirstOrDefault();

            //    if (ga != null)
            //    {
            //        if (ga.CO_FLAG_STATUS != coFlag)
            //        {
            //            ga.CO_FLAG_STATUS = coFlag;
            //            TB48_GRADE_ALUNO.SaveOrUpdate(ga, false);
            //        }
            //    }
            //}
            //ga = new TB48_GRADE_ALUNO();
            //log.RegistroLOG(ga, RegistroLog.ACAO_EDICAO);

            //// Passa pela grid de matérias extras
            //coMat = 0;
            //int coCurOrige = 0;

            //foreach (GridViewRow l in grdGradeCrusos.Rows)
            //{
            //    if (((CheckBox)l.Cells[0].FindControl("ckSelect")).Checked)
            //    {
            //        coMat = int.Parse(((HiddenField)l.Cells[0].FindControl("hidCoMat")).Value);
            //        coCurOrige = int.Parse(((HiddenField)l.Cells[0].FindControl("hidCoCur")).Value);

            //        tb79 = new TB079_HIST_ALUNO();
            //        tb79.CO_ALU = coAlu;
            //        tb79.CO_ANO_REF = coAno;
            //        tb79.CO_CUR = coCur;
            //        tb79.CO_EMP = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).CO_EMP;
            //        tb79.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            //        tb79.CO_MAT = coMat;
            //        tb79.CO_MODU_CUR = coMod;
            //        tb79.CO_TUR = coTur;
            //        tb79.CO_USUARIO = new int?(LoginAuxili.CO_COL);
            //        //tb79.DT_LANC = DateTime.Now;
            //        tb79.DT_LANC = DateTime.Parse("05/01/" + coAno);
            //        tb79.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
            //        tb79.FL_TIPO_LANC_MEDIA = "N";
            //        tb79.CO_FLAG_STATUS = "A";
            //        TB079_HIST_ALUNO.SaveOrUpdate(tb79, false);


            //        ga = new TB48_GRADE_ALUNO();
            //        ga.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            //        ga.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            //        ga.CO_ANO_MES_MAT = coAno;
            //        ga.CO_CUR = coCur;
            //        ga.CO_MAT = coMat;
            //        ga.CO_MODU_CUR = coMod;
            //        ga.CO_STAT_MATE = "E";
            //        ga.CO_TUR = coTur;
            //        ga.NU_SEM_LET = "1";
            //        ga.CO_FLAG_STATUS = "A";
            //        ga.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            //        ga.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
            //        ga.CO_CUR_ORIGE = coCurOrige;
            //        TB48_GRADE_ALUNO.SaveOrUpdate(ga, false);
            //    }
            //}
            //ga = new TB48_GRADE_ALUNO();
            //log.RegistroLOG(ga, RegistroLog.ACAO_GRAVAR);

            //tb79 = new TB079_HIST_ALUNO();
            //log.RegistroLOG(tb79, RegistroLog.ACAO_GRAVAR);

            //AuxiliPagina.EnvioMensagemSucesso(this, "Grade do Aluno Alterada com Sucesso");

            #endregion
        }

        /// <summary>
        /// Carrega uma lista da grade do aluno em questão
        /// </summary>
        /// <param name="ID_MATRCUR_PAGTO"></param>
        /// <param name="ID_PAGTO_CARTAO"></param>
        /// <returns></returns>
        protected TB48_GRADE_ALUNO carregaGradeAluno(string CO_ANO_MES_MAT, int CO_ALU, int CO_MAT, int CO_MODU_CUR, int CO_TUR)
        {
            TB48_GRADE_ALUNO tb48 = (from tbli48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                     where tbli48.CO_ANO_MES_MAT == CO_ANO_MES_MAT
                                       && tbli48.CO_MAT == CO_MAT
                                       && tbli48.CO_ALU == CO_ALU
                                       && tbli48.CO_MODU_CUR == CO_MODU_CUR
                                       && tbli48.CO_TUR == CO_TUR
                                     select tbli48).FirstOrDefault();
            return tb48;
        }

        /// <summary>
        /// Mátodo que carrega o formulário de acordo com o registro selecionado na página de buscas
        /// </summary>
        private void CarregaFormulario()
        {
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);
            int coMod = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            int coCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            int coTur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoTur);
            string coAno = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano).Substring(0, 4);

            CarregaAno();
            ddlAno.SelectedValue = coAno;

            CarregaModalidade();
            ddlModalidade.SelectedValue = coMod.ToString();

            CarregaSerie();
            ddlSerie.SelectedValue = coCur.ToString();

            CarregaTurma();
            ddlTurma.SelectedValue = coTur.ToString();

            CarregaAluno();
            ddlAluno.SelectedValue = coAlu.ToString();

            CarregaSerieExt();

            CarregaGridGradeAluno();
            CarregaGridGradeCursos();
        }

        #endregion

        #region Classes de formatação de saída

        /// <summary>
        /// Classe que formata a saída da combo de ano (criada para evitar ano com espaços em branco)
        /// </summary>
        public class ComboAno
        {
            public string coAno { get; set; }
            public string ano
            {
                get
                {
                    return this.coAno.Trim();
                }
            }
        }

        /// <summary>
        /// Classe que formata a saída da grid de grade do aluno
        /// </summary>
        public class GridGradeAluno
        {
            public string ano { get; set; }
            public string noMat { get; set; }
            public string noMatValid
            {
                get
                {
                    return (this.noMat.Length > 40 ? this.noMat.Substring(0, 40) + "..." : this.noMat);
                }
            }
            public string noSig { get; set; }
            public int qtCh { get; set; }
            public int coMat { get; set; }
            public string coFlag { get; set; }
            public bool chkSel { get; set; }
            public string noCur { get; set; }
            public int coCurN { get; set; }
            public int? coCurOr { get; set; }
        }

        /// <summary>
        /// Classe que formata a saída da grid de grade dos cursos
        /// </summary>
        public class GridGradeCursos
        {
            public int? ordimp { get; set; }
            public string noMat { get; set; }
            public string noCur { get; set; }
            public int qtCh { get; set; }
            public int coMat { get; set; }
            public int coCur { get; set; }
            public bool chkSel { get; set; }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Mátodo que carrega a grid com a grade do aluno na grid grdGradeAluno
        /// </summary>
        protected void CarregaGridGradeAluno()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = 0;
            int coCur = 0;
            int coTur = 0;
            int coAlu = 0;

            int.TryParse(ddlModalidade.SelectedValue, out coMod);
            int.TryParse(ddlSerie.SelectedValue, out coCur);
            int.TryParse(ddlTurma.SelectedValue, out coTur);
            int.TryParse(ddlAluno.SelectedValue, out coAlu);

            string coAno = ddlAno.SelectedValue;

            var res = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb48.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb48.CO_CUR equals tb01.CO_CUR
                       join tb01Orig in TB01_CURSO.RetornaTodosRegistros() on tb48.CO_CUR_ORIGE equals tb01Orig.CO_CUR into l1
                       from lcurOri in l1.DefaultIfEmpty()
                       where tb48.CO_EMP == coEmp
                       && tb48.CO_MODU_CUR == coMod
                       && tb48.CO_CUR == coCur
                       && tb48.CO_TUR == coTur
                       && tb48.CO_ALU == coAlu
                       && tb48.CO_ANO_MES_MAT == coAno
                       select new GridGradeAluno
                       {
                           noMat = tb107.NO_MATERIA,
                           noSig = tb107.NO_SIGLA_MATERIA,
                           qtCh = tb02.QT_CARG_HORA_MAT,
                           coMat = tb02.CO_MAT,
                           coFlag = tb48.CO_FLAG_STATUS,
                           ano = tb48.CO_ANO_MES_MAT,
                           noCur = (lcurOri != null ? lcurOri.CO_SIGL_CUR : tb01.CO_SIGL_CUR),
                           coCurN = tb48.CO_CUR,
                           coCurOr = tb48.CO_CUR_ORIGE,
                       }).OrderBy(o => o.noMat);

            grdGradeAluno.DataSource = res;
            grdGradeAluno.DataBind();

            VerificaDisciplinasDependencia();
        }

        /// <summary>
        /// Método que carrega a grid com as matérias de todos os cursos, menos do curso selecionado pelo usuário. Carrega a grid grdGradeCursos
        /// </summary>
        protected void CarregaGridGradeCursos()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int coCurExt = ddlCurExt.SelectedValue != "" ? int.Parse(ddlCurExt.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;

            if (coCurExt == 0)
            {
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
                           where tb43.CO_ANO_GRADE == coAno
                           select new GridGradeCursos
                           {
                               noMat = tb107.NO_MATERIA,
                               qtCh = tb02.QT_CARG_HORA_MAT,
                               coMat = tb02.CO_MAT,
                               noCur = tb01.NO_CUR,
                               coCur = tb01.CO_CUR,
                               ordimp = tb43.CO_ORDEM_IMPRE ?? 20,
                           }).Distinct().OrderBy(w => w.noCur).ThenBy(y => y.ordimp).ThenBy(u => u.noMat).ToList();

                grdGradeCrusos.DataSource = res;
                grdGradeCrusos.DataBind();
            }
            else
            {
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
                           where tb43.CO_ANO_GRADE == coAno
                           && tb43.CO_CUR == coCurExt
                           select new GridGradeCursos
                           {
                               noMat = tb107.NO_MATERIA,
                               qtCh = tb02.QT_CARG_HORA_MAT,
                               coMat = tb02.CO_MAT,
                               noCur = tb01.NO_CUR,
                               coCur = tb01.CO_CUR,
                               ordimp = tb43.CO_ORDEM_IMPRE ?? 20,
                           }).Distinct().OrderBy(w => w.noCur).ThenBy(y => y.ordimp).ThenBy(u => u.noMat).ToList();

                grdGradeCrusos.DataSource = res;
                grdGradeCrusos.DataBind();
            }

            //CriaNovaLinhaGridChequesPgto();
        }


        /// <summary>
        /// Verifica quais as Disciplinas na grade do Aluno são referentes à Dependência, e deixa a cor da linha em questão diferente.
        /// </summary>
        private void VerificaDisciplinasDependencia()
        {
            foreach (GridViewRow li in grdGradeAluno.Rows)
            {
                HiddenField hdcoCur = ((HiddenField)li.Cells[4].FindControl("hidCoCur"));
                HiddenField hdcoCurOrg = ((HiddenField)li.Cells[4].FindControl("hidCoCurOrg"));

                if ((!string.IsNullOrEmpty(hdcoCur.Value)) || (!string.IsNullOrEmpty(hdcoCurOrg.Value)))
                    if ((hdcoCur.Value != hdcoCurOrg.Value) && (!string.IsNullOrEmpty(hdcoCurOrg.Value)))
                    {
                        li.BackColor = System.Drawing.Color.LightCyan;
                    }
            }
        }

        //protected void grdGradeAluno_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    HiddenField hdcoCur = ((HiddenField)e.Row.Cells[4].FindControl("hidCoCur"));
        //    HiddenField hdcoCurOrg = ((HiddenField)e.Row.Cells[4].FindControl("hidCoCurOrg"));

        //    if ((!string.IsNullOrEmpty(hdcoCur.Value)) || (!string.IsNullOrEmpty(hdcoCurOrg.Value)))
        //        if (hdcoCur.Value != hdcoCurOrg.Value)
        //        {
        //            e.Row.BackColor = System.Drawing.Color.LightCyan;
        //        }
        //}

        /// <summary>
        /// Mátodo que carrega os anos na combo ddlAno
        /// </summary>
        protected void CarregaAno()
        {
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       select new ComboAno
                       {
                           coAno = tb43.CO_ANO_GRADE
                       }).Distinct().OrderByDescending(o => o.coAno);

            ddlAno.DataTextField = "ano";
            ddlAno.DataValueField = "ano";

            ddlAno.DataSource = res;
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega as modalidades na combo de ddlModalidade
        /// </summary>
        protected void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega os cursos na combo ddlSerie
        /// </summary>
        protected void CarregaSerie()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, coMod, coEmp, false);
        }

        /// <summary>
        /// Método que carrega as matérias dos cursos, fora o curso que o usuário selecionou nos filtros
        /// </summary>
        protected void CarregaSerieExt()
        {
            var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                       where tb01.CO_EMP == LoginAuxili.CO_EMP
                       select new
                       {
                           tb01.NO_CUR,
                           tb01.CO_CUR
                       });

            ddlCurExt.DataTextField = "NO_CUR";
            ddlCurExt.DataValueField = "CO_CUR";
            ddlCurExt.DataSource = res;
            ddlCurExt.DataBind();
            ddlCurExt.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega as turmas na combo ddlTurma
        /// </summary>
        protected void CarregaTurma()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, coMod, coCur, false);
        }

        /// <summary>
        /// Método que carrega os alunos na combo ddlAluno
        /// </summary>
        protected void CarregaAluno()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;

            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, coEmp, coMod, coCur, coTur, coAno, false);
        }
        #endregion

        #region Métodos de campo

        /// <summary>
        /// Método que é executado quando uma modalidade é selecionada
        /// </summary>
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
        }

        /// <summary>
        /// Método que é executado quando a série é selecionada
        /// </summary>
        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        /// <summary>
        /// Método que é executado quando a turma é selecionada
        /// </summary>
        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        /// <summary>
        /// Método que é executado quando o aluno é selecionado
        /// </summary>
        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridGradeAluno();
            CarregaGridGradeCursos();
        }

        /// <summary>
        /// É executado quando o Curso Extra é alterado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCurExt_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridGradeCursos();
        }

        // Verifica se a matéria do checkbox clicado já está associada na grade do Aluno, e a desmarca.
        protected void ckSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk = new CheckBox();
            var res = new { ano = "" };
            bool aux = false;

            // Valida se a grid de atividades possui algum registro
            if (grdGradeCrusos.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdGradeCrusos.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelect");
                    int coMat = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoMat")).Value);
                    int coAlu = int.Parse(ddlAluno.SelectedValue);
                    string ano = ddlAno.SelectedValue;

                    if (chk.ClientID == atual.ClientID)
                    {
                        res = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                               where tb48.CO_ALU == coAlu
                               && tb48.CO_MAT == coMat
                               //&& tb48.CO_ANO_MES_MAT == ano
                               select new
                               {
                                   ano = tb48.CO_ANO_MES_MAT,
                               }).FirstOrDefault();

                        aux = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                               where tb48.CO_ALU == coAlu
                               && tb48.CO_MAT == coMat
                               && tb48.CO_ANO_MES_MAT == ano
                               select tb48).Any();

                        if (aux == true)
                            chk.Checked = false;
                    }
                }
            }

            //Trata a mensagem de erro, para o caso de o aluno já ter a matéria no ano corrente ou não.
            if (aux == true)
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Aluno em questão já possui a matéria selecionada no ano corrente.");
            else if (res != null)
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Aluno em questão já possui a Matéria selecionada em sua grade no ano " + res.ano + ". Ao selecionar e salvar o registro, você incluirá esta matéria como Dependência na Grade do Aluno em questão.");
        }

        #endregion
    }
}