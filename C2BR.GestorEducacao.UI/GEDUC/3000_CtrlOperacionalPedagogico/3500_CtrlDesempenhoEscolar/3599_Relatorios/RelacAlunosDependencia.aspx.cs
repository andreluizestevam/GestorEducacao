//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE LETIVO DE AULAS E ATIVIDADES
// OBJETIVO: HISTÓRICO DAS ATIVIDADES LETIVAS EXECUTADAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 11/07/2014| Maxwell Almeida            | Criação do Relatório de Alunos em dependência
//           |                            | 
//           |                            |

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3501_RelacDependenAlunos;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;
//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios
{
    public partial class RelacAlunosDependencia : System.Web.UI.Page
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
                CarregaFuncionarios();
                CarregaAnos();
                CarregaModalidade();
                carregaDisciplinas();
                CarregaSerieCurso();
                CarregaTurma();

            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros, infos, deModalidde, noCur, noTurma, noCol, ano, noUnidade, deAno;
            int lRetorno, coEmp, Unidade, Colaborador, anoRef, Modalidade, SerieCur, Turma, materia;

            coEmp = LoginAuxili.CO_EMP;
            Unidade = int.Parse(ddlUnidade.SelectedValue);
            Colaborador = int.Parse(ddlFuncionarios.SelectedValue);
            anoRef = int.Parse(ddlAnoRefer.SelectedValue);
            Modalidade = int.Parse(ddlModalidade.SelectedValue);
            SerieCur = int.Parse(ddlSerieCurso.SelectedValue);
            Turma = int.Parse(ddlTurma.SelectedValue);
            materia = int.Parse(ddlDisciplina.SelectedValue);


            deModalidde = (Modalidade != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(Modalidade).DE_MODU_CUR : "Todos");
            noCur = (SerieCur != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, Modalidade, SerieCur).NO_CUR : "Todos");
            noTurma = (Turma != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(Turma).NO_TURMA : "Todos");
            noUnidade = (Unidade != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(Unidade).NO_FANTAS_EMP : "Todos");
            noCol = (Colaborador != 0 ? TB03_COLABOR.RetornaPelaChavePrimaria(Unidade, Colaborador).NO_COL : "Todos");
            ano = anoRef.ToString();
            deAno = (anoRef != 0 ? anoRef.ToString() : "TODOS");

            parametros = "( Ano Referência: " + deAno + " - Modalidade: " + deModalidde.ToUpper() + " - Curso: " + noCur.ToUpper() + " - Turma: " + noTurma.ToUpper() + " - Unidade: " + noUnidade.ToUpper() + " - Professor: " + noCol.ToUpper() + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            switch (ddlOrdenadoPor.SelectedValue)
            {
                case "M":
                    RptRelacAlunosDependencia rtp = new RptRelacAlunosDependencia();
                    lRetorno = rtp.InitReport(parametros, infos, coEmp, ano, Modalidade, SerieCur, Turma, materia, Colaborador);
                    Session["Report"] = rtp;
                    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
                break;

                case "D":
                    RptRelacDepDisci rtp2 = new RptRelacDepDisci();
                    lRetorno = rtp2.InitReport(parametros, infos, coEmp, ano, Modalidade, SerieCur, Turma, materia, Colaborador);
                    Session["Report"] = rtp2;
                    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
                break;

                case "A":
                    RptRelacDepPorAlu rtp3 = new RptRelacDepPorAlu();
                    lRetorno = rtp3.InitReport(parametros, infos, coEmp, ano, Modalidade, SerieCur, Turma, materia, Colaborador);
                    Session["Report"] = rtp3;
                    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
                break;

                case "S":
                    RptRelacDepCurso rtp4 = new RptRelacDepCurso();
                    lRetorno = rtp4.InitReport(parametros, infos, coEmp, ano, Modalidade, SerieCur, Turma, materia, Colaborador);
                    Session["Report"] = rtp4;
                    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
                break;
            }

        }
        #endregion

        #region Carregamento DropDown

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

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários, de acordo com as informações preenchidas nos campos anteriores, verificando na tabela de responsável de matéria/curso/modalidade
        /// </summary>
        private void CarregaFuncionarios()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serieCurso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coMat = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;

            ddlFuncionarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                          join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                          where (
                                                   (modalidade != 0 ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                                                   && (serieCurso != 0 ? tbRM.CO_CUR == serieCurso : 0 == 0)
                                                   && (turma != 0 ? tbRM.CO_TUR == turma : 0 == 0)
                                                   && (coMat != 0 ? tbRM.CO_MAT == coMat : 0 == 0)
                                                   && (tb03.FLA_PROFESSOR == "S")
                                                   )
                                          select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);

            ddlFuncionarios.DataTextField = "NO_COL";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();

            ddlFuncionarios.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (coEmp != 0)
            {
                ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                          where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending(g => g.CO_ANO_MES_MAT);

                ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataBind();
            }
            else
                ddlAnoRefer.Items.Clear();

            
            ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "0"));
            //ddlAnoRefer.SelectedValue = (ddlAnoRefer.Items.FindByText(DateTime.Now.Year.ToString()) == true ? DateTime.Now.Year.ToString() : 0.ToString());
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidade()
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

            ddlModalidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //   string anoGrade = ddlAnoRefer.SelectedValue;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                var res = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                           where tb01.CO_MODU_CUR == modalidade
                           select new { tb01.CO_CUR, tb01.NO_CUR });

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";

                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
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
                ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Carrega as Disciplinas no campo DropDownList, verificando se há um curso selecionado, caso haja, carrega apenas as matérias pertinentes ao curso em questão, caso contrário traz todas.
        /// </summary>
        private void carregaDisciplinas()
        {
            int coCur = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);

            if (coCur != 0)
            {
                var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb02.CO_MAT equals tb43.CO_MAT
                           where tb43.CO_CUR == coCur
                           select new
                           {
                               tb107.NO_MATERIA,
                               tb02.CO_MAT,
                           }).Distinct().OrderBy(w => w.NO_MATERIA);

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataSource = res;
                ddlDisciplina.DataBind();
            }
            else
            {
                var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           select new
                           {
                               tb107.NO_MATERIA,
                               tb02.CO_MAT,
                           }).ToList().OrderBy(w => w.NO_MATERIA).Distinct();

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataSource = res;
                ddlDisciplina.DataBind();
            }

            ddlDisciplina.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        public void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaSerieCurso();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaFuncionarios();
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaTurma();
            carregaDisciplinas();
        }
    }
}
