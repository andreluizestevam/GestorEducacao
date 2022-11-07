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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito
{
    public partial class CreditosAlunos : System.Web.UI.Page
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

                carregaAlunoRecebCred();
                CarregaAlunoTransCred();
                carregaModalidade();
                carregaMateria();

                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));


            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros;
            string infos;
            int coEmp, lRetorno, modalidade, serieCurso, Turma, alunoReceb, alunoTrans, materia, anoRef;
            string dataIni, dataFim, deModalidde, noCur, noTurma, noAluno, noBeneficiario, noMat, Periodo, anogr;


            coEmp = LoginAuxili.CO_EMP;
            materia = int.Parse(ddlMateria.SelectedValue);
            alunoTrans = int.Parse(ddlAlunoTransCred.SelectedValue);
            modalidade = int.Parse(ddlModalidade.SelectedValue);
            serieCurso = int.Parse(ddlSerieCurso.SelectedValue);
            Turma = int.Parse(ddlTurma.SelectedValue);
            alunoReceb = int.Parse(ddlAlunoReceb.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;
            anoRef = ano;

            deModalidde = (modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(modalidade).DE_MODU_CUR : "Todos");
            noCur = (serieCurso != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serieCurso).NO_CUR : "Todos");
            noTurma = (Turma != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(Turma).NO_TURMA : "Todos"); //TB06_TURMAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serieCurso, Turma).TB129_CADTURMAS.NO_TURMA : "Todos");
            noAluno = (alunoTrans != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(alunoTrans, LoginAuxili.CO_EMP).NO_ALU : "Todos");
            noBeneficiario = (alunoReceb != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(alunoReceb, LoginAuxili.CO_EMP).NO_ALU : "Todos");
            noMat = (materia != 0 ? TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, materia).NO_MATERIA : "todos");
            Periodo = dataIni + " à " + dataFim;
            anogr = anoRef.ToString();

            parametros = "( Modalidade: " + deModalidde.ToUpper() + " - Série/Curso: " + noCur.ToUpper() + " - Turma: " + noTurma.ToUpper() + " - Aluno" + noAluno.ToUpper() + " - Beneficiário: " + noBeneficiario.ToUpper() + " - Matéria: " + noMat.ToUpper() + " - Período: " + Periodo.ToUpper() + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptCreditosAlunos fpcb = new RptCreditosAlunos();
            lRetorno = fpcb.InitReport(parametros, infos, alunoTrans, coEmp, materia, alunoReceb, modalidade, serieCurso, Turma, dataIni, dataFim, anoRef);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        public int ano
        {
            get
            {
                int teste = int.Parse(IniPeri.Text.Substring(6, 4));
                return teste;
            }
        }

        //private void carregaAno()
        //{
        //    string teste = IniPeri.Text.Substring(6, 4);

        //    ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
        //                         where tb08.DT_CAD_MAT.Year >= DateTime.Now.Year - 1 && tb08.DT_CAD_MAT.Year <= DateTime.Now.Year
        //                         select new { ANO_CAD_MAT = tb08.DT_CAD_MAT.Year }).Distinct().OrderByDescending(m => m.ANO_CAD_MAT);

        //    ddlAno.DataTextField = "ANO_CAD_MAT";
        //    ddlAno.DataValueField = "ANO_CAD_MAT";
        //    ddlAno.DataBind();
        //}


        /// <summary>
        /// Carrega os Alunos de acordo com as informações de Modalidade, Série e Turma para que o aluno que transferiu o crédito seja selecionado.
        /// </summary>
        private void CarregaAlunoTransCred()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serieCurso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //string anoGrade = int.parse

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                       where (
                       (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : 0 == 0)
                       && (serieCurso != 0 ? tb08.CO_CUR == serieCurso : 0 == 0)
                       && (turma != 0 ? tb08.CO_TUR == turma : 0 == 0)
                           //&& (anoGrade != "0" ? tb08.CO_ANO_MES_MAT == anoGrade : 0 == 0)
                       && (tb08.CO_SIT_MAT == "A")
                       )
                       //where (( tb07.CO_EMP == LoginAuxili.CO_EMP) && (  ))
                       select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy(m => m.NO_ALU);

            ddlAlunoTransCred.DataTextField = "NO_ALU";
            ddlAlunoTransCred.DataValueField = "CO_ALU";

            ddlAlunoTransCred.DataSource = res;
            ddlAlunoTransCred.DataBind();

            ddlAlunoTransCred.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os Alunos de acordo com as informações de Modalidade, Série e Turma para que o aluno que recebeu o crédito seja selecionado.
        /// </summary>
        private void carregaAlunoRecebCred()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serieCurso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //string anoGrade = ddlAno.SelectedValue;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                       where (
                       (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : 0 == 0)
                       && (serieCurso != 0 ? tb08.CO_CUR == serieCurso : 0 == 0)
                       && (turma != 0 ? tb08.CO_TUR == turma : 0 == 0)
                           //&& (anoGrade != "0" ? tb08.CO_ANO_MES_MAT == anoGrade : 0 == 0)
                       && (tb08.CO_SIT_MAT == "A")
                       )
                       //where (( tb07.CO_EMP == LoginAuxili.CO_EMP) && (  ))
                       select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy(m => m.NO_ALU);

            ddlAlunoReceb.DataTextField = "NO_ALU";
            ddlAlunoReceb.DataValueField = "CO_ALU";

            ddlAlunoReceb.DataSource = res;
            ddlAlunoReceb.DataBind();

            ddlAlunoReceb.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega as Modalidades
        /// </summary>
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

        /// <summary>
        /// Carrega as Matérias
        /// </summary>
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

        #endregion

        #region Funções de Campo

        protected void ddlSerieCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaTurma();
            CarregaAlunoTransCred();
            carregaAlunoRecebCred();
        }

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaSerieCurso();
            CarregaAlunoTransCred();
            carregaAlunoRecebCred();
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunoTransCred();
            carregaAlunoRecebCred();
        }

        #endregion
    }
}