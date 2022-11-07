using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios
{
    public partial class ResumoAvaliacao : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, infos, strParam, strBim;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            strP_CO_MAT = null;
            strParam = null;
            strBim = null;
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_CO_ALU = ddlAlunos.SelectedValue;
            strP_CO_MAT = ddlDisciplina.SelectedValue;
            strBim = ddlReferencia.SelectedValue.Equals("T1") ? "1" : ddlReferencia.SelectedValue.Equals("T2") ? "2" : ddlReferencia.SelectedValue.Equals("T3") ? "3" : ddlReferencia.SelectedValue;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            int coAlu = ddlAlunos.SelectedValue != "T" ? int.Parse(ddlAlunos.SelectedValue) : 0;

            strParam = "Ano: " + ddlAnoRefer.SelectedItem.ToString().Trim() + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Série: " +
                ddlSerieCurso.SelectedItem.ToString() + " - Turma: " + ddlTurma.SelectedItem.ToString();

            RptResumoAvaliacao rpt = new RptResumoAvaliacao();

            lRetorno = rpt.InitReport(strParam, LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue ?? "0"), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR ?? "0"), int.Parse(strP_CO_CUR ?? "0"), int.Parse(strP_CO_TUR ?? "0"), coAlu, int.Parse(strP_CO_MAT ?? "0"), int.Parse(strBim ?? "0"));
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

        }
        #endregion

        #region Carregamento DropDown
        
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

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlUnidade.SelectedValue = "0";

            ddlAnoRefer.Items.Clear();
            ddlReferencia.Items.Clear();
            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();
            ddlAlunos.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos(int? coEmp)
        {
            ddlAnoRefer.DataSource = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                      where tb079.TB25_EMPRESA.CO_EMP == coEmp
                                      select new { tb079.CO_ANO_REF }).Distinct().OrderByDescending(h => h.CO_ANO_REF);

            ddlAnoRefer.DataTextField = "CO_ANO_REF";
            ddlAnoRefer.DataValueField = "CO_ANO_REF";
            ddlAnoRefer.DataBind();
            ddlAnoRefer.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlAnoRefer.SelectedValue = "0";

            ddlReferencia.Items.Clear();
            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();
            ddlAlunos.Items.Clear();

        }

        //private void CarregaReferencia()
        //{
        //    //ddlReferencia.Items.Insert(0, new ListItem("4º Bimestre", "4"));
        //    ddlReferencia.Items.Insert(0, new ListItem("3º Trimestre", "3"));
        //    ddlReferencia.Items.Insert(0, new ListItem("2º Trimestre", "2"));
        //    ddlReferencia.Items.Insert(0, new ListItem("1º Trimestre", "1"));
        //    ddlReferencia.Items.Insert(0, new ListItem("Selecione", "0"));
        //    ddlReferencia.SelectedValue = "0";

        //    ddlModalidade.Items.Clear();
        //    ddlSerieCurso.Items.Clear();
        //    ddlTurma.Items.Clear();
        //    ddlDisciplina.Items.Clear();
        //    ddlAlunos.Items.Clear();
        //}

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlModalidade.SelectedValue = "0";

            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlDisciplina.Items.Clear();
            ddlAlunos.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso(int? coEmp, int? coModuCur, string anoGrade = "")
        {
            if (coEmp != null
                && anoGrade != ""
                && coModuCur != null
                )
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == coModuCur
                                            && tb43.CO_ANO_GRADE == anoGrade
                                            && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();

                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlSerieCurso.SelectedValue = "0";

                ddlTurma.Items.Clear();
                ddlDisciplina.Items.Clear();
                ddlAlunos.Items.Clear();
            }
            else
                ddlSerieCurso.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma(int? coModuCur, int? coSer, int? coEmp)
        {
            if (coEmp != null 
                && coSer != null
                && coModuCur != null
                )
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == coModuCur 
                                       && tb06.CO_CUR == coSer
                                       && tb06.CO_EMP == coEmp
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();

                ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTurma.SelectedValue = "0";

                ddlDisciplina.Items.Clear();
                ddlAlunos.Items.Clear();
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina(int? coEmp, int? coMod, int? coSer, int? coTur, string anoMesMat = "")
        {
            if (coEmp != null
                && coMod != null
                && coSer != null
                && coTur != null
                && anoMesMat != ""
                )
            {
                ddlDisciplina.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb48.CO_MAT equals tb02.CO_MAT
                                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                            where tb48.TB44_MODULO.CO_MODU_CUR == coMod && tb48.CO_CUR == coSer
                                            && tb48.CO_ANO_MES_MAT == anoMesMat && tb48.CO_TUR == coTur
                                            select new { tb48.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(g => g.NO_MATERIA);

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataBind();
                ddlDisciplina.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlDisciplina.SelectedValue = "0";

                ddlAlunos.Items.Clear();
            }
            else
                ddlDisciplina.Items.Clear();

        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno(int? coEmp, int? coMod, int? coSer, int? coTur, string anoMesMat = "")
        {
            if (coEmp != null
                && coMod != null
                && coSer != null
                && coTur != null
                && anoMesMat != ""
                )
            {
                ddlAlunos.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                        where tb48.TB44_MODULO.CO_MODU_CUR == coMod && tb48.CO_CUR == coSer
                                        && tb48.CO_ANO_MES_MAT == anoMesMat && tb48.CO_TUR == coTur
                                        select new { tb48.TB07_ALUNO.CO_ALU, tb48.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
                ddlAlunos.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlAlunos.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlAlunos.SelectedValue = "0";
            }
            else
                ddlAlunos.Items.Clear();

            
        }
        #endregion

        #region Box changed
        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            CarregaAnos(coEmp);
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                //CarregaReferencia();
                CarregaMedidas();
        }

        protected void ddlReferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            string anoRef = ddlAnoRefer.SelectedValue;
            int coMod = int.Parse(((DropDownList)sender).SelectedValue);
            CarregaSerieCurso(coEmp, coMod, anoRef);
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            string anoRef = ddlAnoRefer.SelectedValue;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coSer = int.Parse(((DropDownList)sender).SelectedValue);
            CarregaTurma(coEmp: coEmp, coModuCur: coMod, coSer: coSer);
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            string anoRef = ddlAnoRefer.SelectedValue;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coSer = int.Parse(ddlSerieCurso.SelectedValue);
            int coTur = int.Parse(((DropDownList)sender).SelectedValue);
            CarregaDisciplina(coEmp, coMod, coSer, coTur, anoRef);
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
            string anoRef = ddlAnoRefer.SelectedValue;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coSer = int.Parse(ddlSerieCurso.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            CarregaAluno(coEmp, coMod, coSer, coTur, anoRef);
        }
        #endregion
    }
}