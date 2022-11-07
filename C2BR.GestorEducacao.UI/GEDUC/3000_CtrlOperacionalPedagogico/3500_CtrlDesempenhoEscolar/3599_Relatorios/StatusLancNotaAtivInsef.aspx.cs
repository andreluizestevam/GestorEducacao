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
    public partial class StatusLancNotaAtivInsef : System.Web.UI.Page
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
                CarregaMedidas();
            }
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

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            Imprime();
        }

        private void Imprime()
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
            strP_CO_BIMESTRE = ddlReferencia.SelectedValue;

            //--------> Prepara a linha de parâmetros
            string deMod = (strP_CO_MODU_CUR != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(strP_CO_MODU_CUR).DE_MODU_CUR : "Todos");
            string noCur = (strP_CO_CUR != 0 ? TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == strP_CO_CUR).FirstOrDefault().NO_CUR : "Todos");
            string noTur = (strP_CO_TUR != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).NO_TURMA : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "( Ano Letivo: " + strP_CO_ANO_MES_MAT + " - Modalidade: " + deMod + " - Curso: " + noCur + " - Turma: " + noTur + " )";

            RptStatusLancNotaAtivInsef rpt = new RptStatusLancNotaAtivInsef();
            lRetorno = rpt.InitReport(parametros, infos, strP_CO_EMP_REF, strP_CO_ANO_MES_MAT, strP_CO_CUR, strP_CO_MODU_CUR, strP_CO_TUR, strP_CO_BIMESTRE, LoginAuxili.CO_EMP);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

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
                AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, coEmp, modalidade, serie, true);
            else
                AuxiliCarregamentos.CarregaTurmasSiglaProfeResp(ddlTurma, coEmp, modalidade, serie, LoginAuxili.CO_COL, ano, true);
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
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);

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
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, true);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, anoGrade, true);

            CarregaTurma();
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
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            Imprime();
        }
    }
}