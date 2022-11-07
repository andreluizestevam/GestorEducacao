//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//25/04/2014    Maxwell Almeida             Criação da página de Filtro de Relatórios.

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3099_ExtratoMonitoria
{
    public partial class ExtrMonitoria : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaModalidade();
                carregaMateria();

                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
                ddlProfessor.Items.Insert(0, new ListItem("Todas", "0"));
                txtAno.Text = DateTime.Now.Year.ToString();
            }
        }
        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string parametros;
            string infos;
            int coEmp, lRetorno, modalidade, serieCurso, Turma, materia, professor;
            string noModalidade, noCur, noTurma, noMat, noProfessor, ano;


            coEmp = LoginAuxili.CO_EMP;
            materia = int.Parse(ddlMateria.SelectedValue);
            modalidade = int.Parse(ddlModalidade.SelectedValue);
            serieCurso = int.Parse(ddlSerieCurso.SelectedValue);
            Turma = int.Parse(ddlTurma.SelectedValue);
            professor = int.Parse(ddlProfessor.SelectedValue);
            ano = txtAno.Text;

            noModalidade = (modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(modalidade).DE_MODU_CUR : "Todos");
            noCur = (serieCurso != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serieCurso).NO_CUR : "Todos");
            noTurma = (Turma != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(Turma).NO_TURMA : "Todos");             
            noMat = (materia != 0 ? TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, materia).NO_MATERIA : "todos");
            noProfessor = (professor != 0 ? TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, professor).NO_COL : "Todos"); 

            parametros = "( Modalidade: " + noModalidade.ToUpper() + " - Série/Curso: " + noCur.ToUpper() + " - Turma: " + noTurma.ToUpper() 
                + " - Matéria: " + noMat.ToUpper() + " - Professor: " + noProfessor.ToUpper() + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptExtMonitoria fpcb = new RptExtMonitoria();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, modalidade, serieCurso, Turma, materia, professor, ano);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        private void carregaModalidade()
        {
            var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                       where tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                       select new
                       {
                           tb44.CO_MODU_CUR,
                           tb44.DE_MODU_CUR
                       });

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";

            ddlModalidade.DataSource = res;
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Carrega a Série e Curso de acordo com a modalidade informada.
        /// </summary>
        private void carregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                var res = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                           where tb01.CO_MODU_CUR == modalidade
                           //join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                           //where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                           select new { tb01.CO_CUR, tb01.NO_CUR });

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";

                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
            }
        }

        /// <summary>
        /// Carrega as Turmas de acordo com série e curso.
        /// </summary>
        private void carregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {

                var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                           where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade
                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";

                ddlTurma.DataSource = res;
                ddlTurma.DataBind();
                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
            }
        }

        private void carregaMateria()
        {
            var res = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                       select new { tb107.NO_MATERIA, tb107.ID_MATERIA });

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "ID_MATERIA";

            ddlMateria.DataSource = res;
            ddlMateria.DataBind();

            ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void carregaProfessor()
        {
            int mod = int.Parse(ddlModalidade.SelectedValue);
            int ser = int.Parse(ddlSerieCurso.SelectedValue);
            int tur = int.Parse(ddlTurma.SelectedValue);
            int mat = int.Parse(ddlMateria.SelectedValue);

            if ((mod != 0) && (ser != 0))
            {

            var res = (from tb188 in TB188_MONIT_CURSO_PROFE.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb188.CO_COL equals tb03.CO_COL
                       where tb03.FLA_PROFESSOR == "S"
                       && tb03.FL_ATIVI_MONIT == "S"
                       && (mod != 0 ? tb188.CO_MODU_CUR == mod : 0 == 0)
                       && (ser != 0 ? tb188.CO_CUR == ser : 0 == 0)
                       && (tur != 0 ? tb188.CO_TUR == tur : 0 == 0)
                       && (mat != 0 ? tb188.CO_MAT == mat : 0 == 0)
                       select new { tb03.CO_COL, tb03.NO_COL }).ToList();

            ddlProfessor.DataTextField = "NO_COL";
            ddlProfessor.DataValueField = "CO_COL";
            ddlProfessor.DataSource = res;
            ddlProfessor.DataBind();
            ddlProfessor.Items.Insert(0, new ListItem("Todas", "0"));

            }
            else
            {
                ddlProfessor.Items.Clear();
                ddlProfessor.Items.Insert(0, new ListItem("Todas", "0"));
            }

        }


        #endregion

        #region Funções de Campo

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaSerieCurso();
            carregaTurma();
            carregaProfessor();

        }

        protected void ddlSerieCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaTurma();
            carregaProfessor();

        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaProfessor();
        }

        protected void ddlMateria_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaProfessor();
        }

        #endregion
    }
}