//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: BOLETIM DE DESEMPENHO ESCOLAR DO ALUNO - MODELO 3
// DATA DE CRIAÇÃO: 14/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 14/05/2013| André Nobre Vinagre        |Criada a funcionalidade
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
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios
{
    public partial class RelatorioMediaAlunos : System.Web.UI.Page
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
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaDisciplina();
                CarregaAluno();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, infos, strParam;

            //--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            strP_CO_MAT = null;
            strParam = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_CO_ALU = ddlAlunos.SelectedValue;
            strP_CO_MAT = ddlDisciplina.SelectedValue;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            int coAlu = ddlAlunos.SelectedValue != "T" ? int.Parse(ddlAlunos.SelectedValue) : 0;

            strParam = "( Ano: " + ddlAnoRefer.SelectedItem.ToString().Trim() + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Série: " +
                ddlSerieCurso.SelectedItem.ToString() + " - Turma: " + ddlTurma.SelectedItem.ToString() + " )";

            bool mosAgrup = chkAgrupadora.Checked ? true : false;

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(strP_CO_EMP));
            tb25.TB83_PARAMETROReference.Load();
            string tipoAval = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL == null ? "N" : tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;
            if (tipoAval.Equals("T"))
            {
                RptRelatorioMediaAlunosTrimestre rpt = new RptRelatorioMediaAlunosTrimestre();

                lRetorno = rpt.InitReport(strParam, LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR), coAlu, int.Parse(strP_CO_MAT), mosAgrup);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                RptRelatorioMediaAlunos rpt = new RptRelatorioMediaAlunos();

                lRetorno = rpt.InitReport(strParam, LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR), coAlu, int.Parse(strP_CO_MAT), mosAgrup);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }


        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;
            AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, coEmp, modalidade, serie, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAnoRefer.DataSource = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                      where tb079.TB25_EMPRESA.CO_EMP == coEmp
                                      select new { tb079.CO_ANO_REF }).Distinct().OrderByDescending(h => h.CO_ANO_REF);

            ddlAnoRefer.DataTextField = "CO_ANO_REF";
            ddlAnoRefer.DataValueField = "CO_ANO_REF";
            ddlAnoRefer.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, false, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAnoRefer.SelectedValue;

            ddlDisciplina.Items.Clear();

            if (turma != 0)
            {
                ddlDisciplina.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb48.CO_MAT equals tb02.CO_MAT
                                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                            where tb48.TB44_MODULO.CO_MODU_CUR == modalidade && tb48.CO_CUR == serie
                                            && tb48.CO_ANO_MES_MAT == anoMesMat && tb48.CO_TUR == turma
                                            select new { tb48.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(g => g.NO_MATERIA);

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "CO_MAT";
                ddlDisciplina.DataBind();
            }

            ddlDisciplina.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAnoRefer.SelectedValue;

            ddlAlunos.Items.Clear();

            ddlAlunos.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                    where (modalidade != 0 ? tb48.TB44_MODULO.CO_MODU_CUR == modalidade : 0 == 0)
                                    && (serie != 0 ? tb48.CO_CUR == serie : 0 == 0)
                                    && (tb48.CO_ANO_MES_MAT == anoMesMat)
                                    && (turma != 0 ? tb48.CO_TUR == turma : 0 == 0)
                                    select new { tb48.TB07_ALUNO.CO_ALU, tb48.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

            ddlAlunos.DataTextField = "NO_ALU";
            ddlAlunos.DataValueField = "CO_ALU";
            ddlAlunos.DataBind();


            ddlAlunos.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaDisciplina();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaDisciplina();
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaDisciplina();
            CarregaAluno();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModalidade.Items.Count > 0)
            {
                CarregaSerieCurso();
                CarregaTurma();
                CarregaDisciplina();
                CarregaAluno();
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDisciplina();
            CarregaAluno();
        }
    }
}