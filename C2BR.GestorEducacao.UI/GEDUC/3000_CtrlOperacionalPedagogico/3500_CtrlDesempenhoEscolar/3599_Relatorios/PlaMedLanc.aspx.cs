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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios
{
    public partial class PlaMedLanc : System.Web.UI.Page
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
                liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                CarregaAnos();
                CarregaModalidades();
                CarregaAlunos();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            imprime();
        }

        private void imprime()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR;
            string infos, parametros/*,strP_CO_ANO_REFER,strTipo*/;
            int strP_CO_EMP_REF, strP_CO_EMP, strP_CO_MAT, CO_ALU;
            string strP_CO_ANO_MES_MAT, strP_CO_BIMESTRE;

            //--------> Inicializa as variáveis
            // strP_CO_ANO_REFER = null;
            strP_CO_ANO_MES_MAT = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_MODU_CUR = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_BIMESTRE = ddlBimestre.SelectedValue;
            strP_CO_MAT = int.Parse(ddlMateria.SelectedValue);
            CO_ALU = int.Parse(ddlAluno.SelectedValue);

            //--------> Prepara a linha de parâmetros
            string deMod = (strP_CO_MODU_CUR != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(strP_CO_MODU_CUR).DE_MODU_CUR : "Todos");
            string noCur = (strP_CO_CUR != 0 ? TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == strP_CO_CUR).FirstOrDefault().NO_CUR : "Todos");
            string noTur = (strP_CO_TUR != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).NO_TURMA : "Todos");
            int id_materia = (strP_CO_MAT != 0 ? TB02_MATERIA.RetornaTodosRegistros().Where(w => w.CO_MAT == strP_CO_MAT).FirstOrDefault().ID_MATERIA : 0);
            string noMat = (id_materia != 0 ? TB107_CADMATERIAS.RetornaTodosRegistros().Where(t => t.ID_MATERIA == id_materia).FirstOrDefault().NO_MATERIA : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "( Ano Letivo: " + strP_CO_ANO_MES_MAT + " - Modalidade: " + deMod + " - Curso: " + noCur + " - Turma: " + noTur + " - Disciplina: " + noMat + " )"; // - Matéria: " + noMat + 
            bool formulario = (chkForm.Checked ? true : false);

            RptPlaMedLanc rpt = new RptPlaMedLanc();
            lRetorno = rpt.InitReport(parametros, infos, strP_CO_EMP_REF, strP_CO_ANO_MES_MAT, strP_CO_CUR, strP_CO_MODU_CUR, strP_CO_TUR, strP_CO_BIMESTRE, strP_CO_MAT, LoginAuxili.CO_EMP, formulario, ddlAgrupPor.SelectedValue, CO_ALU);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método responsável por carregar os alunos
        /// </summary>
        private void CarregaAlunos()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string ano = ddlAnoRefer.SelectedValue;
            int turma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);

            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, coEmp, modalidade, serie, turma, ano, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, coEmp, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasSiglaProfeResp(ddlTurma, coEmp, modalidade, serie, LoginAuxili.CO_COL, ano, false);

            CarregaMateria();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAnoRefer, LoginAuxili.CO_EMP, true);
        }

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
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = int.Parse(ddlAnoRefer.SelectedValue);

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);

            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int anoGrade = int.Parse(ddlAnoRefer.SelectedValue);

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, anoGrade, false);

            CarregaTurma();           
        }

        private void CarregaMateria()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAnoRefer.SelectedValue;
            string turmaUnica = "N";

            if (coMod != 0 && coCur != 0 && coTur != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(coEmp, coMod, coCur, coTur).CO_FLAG_RESP_TURMA;
            }

            if (turmaUnica == "S")
            {
                var res = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where (coEmp != 0 ? tb02.CO_EMP == coEmp : 0 == 0)
                           && (coMod != 0 ? tb02.CO_MODU_CUR == coMod : 0 == 0)
                           && (coCur != 0 ? tb02.CO_CUR == coCur : 0 == 0)
                           && tb107.NO_SIGLA_MATERIA == "MSR"
                           select new
                           {
                               tb107.NO_MATERIA,
                               tb02.CO_MAT
                           });

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "CO_MAT";
                ddlMateria.DataSource = res;
                ddlMateria.DataBind();
            }
            else
            {

                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlMateria, LoginAuxili.CO_EMP, coMod, coCur, ano, true);
                }
                else
                {
                    //int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                    //int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                    //int turma = ddlTurma.SelectedValue != "0" ? int.Parse(ddlTurma.SelectedValue) : 0;
                    //int anoInt = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                    AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlMateria, LoginAuxili.CO_COL, coMod, coCur, int.Parse(ano), true);
                }
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMateria();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMateria();
            CarregaAlunos();
        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            PadraoRelatoriosCorrente_OnAcaoGeraRelatorio();
        }

        protected void ddlAgrupPor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAgrupPor.SelectedValue == "A")
            {
                liAlun.Visible = true;
                liDisc.Visible = false;
                ddlMateria.SelectedValue = "0";
            }
            else
            {
                liAlun.Visible = false;
                ddlAluno.SelectedValue = "0";
                liDisc.Visible = true;
            }
        }
    }
}