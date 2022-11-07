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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios
{
    public partial class CartaCobranca : System.Web.UI.Page
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
                CarregaAluno();
            }
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
            string infos, parametros;
            int strP_CO_EMP_REF, strP_CO_EMP, strp_CO_ALU;
            string strP_CO_ANO_MES_MAT;
            string nomeFuncionalidade;

            //--------> Inicializa as variáveis
            strP_CO_ANO_MES_MAT = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_CUR = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            strP_CO_MODU_CUR = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            strP_CO_TUR = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
            strp_CO_ALU = int.Parse(ddlAluno.SelectedValue);
            nomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/5000_CtrlFinanceira/5200_CtrlReceitas/5299_Relatorios/CartaCobranca.aspx");

            //--------> Prepara a linha de parâmetros
            string deMod = (strP_CO_MODU_CUR != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(strP_CO_MODU_CUR).DE_MODU_CUR : "Todos");
            string noCur = (strP_CO_CUR != 0 ? TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == strP_CO_CUR).FirstOrDefault().NO_CUR : "Todos");
            string noTur = (strP_CO_TUR != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).NO_TURMA : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "( Ano Letivo: " + (strP_CO_ANO_MES_MAT == "0" ? "Todos" : strP_CO_ANO_MES_MAT) + " - Modalidade: " + deMod + " - Curso: " + noCur + " - Turma: " + noTur + " )";

            RptCartaCobranca rpt = new RptCartaCobranca();
            lRetorno = rpt.InitReport(parametros, infos, strP_CO_EMP_REF, strP_CO_ANO_MES_MAT, strP_CO_CUR, strP_CO_MODU_CUR, strP_CO_TUR, strp_CO_ALU, LoginAuxili.CO_EMP, nomeFuncionalidade);
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
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, coEmp, modalidade, serie, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos.
        /// </summary>
        private void CarregaAluno()
        {
            int? coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int? modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int? curso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int? turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where (coEmp != 0 ? tb07.CO_EMP == coEmp : tb07.CO_EMP == tb07.CO_EMP)
                       && (modalidade != 0 ? tb07.CO_MODU_CUR == modalidade : tb07.CO_MODU_CUR == tb07.CO_MODU_CUR)
                       && (curso != 0 ? tb07.CO_CUR == curso : tb07.CO_CUR == tb07.CO_CUR)
                       && (turma != 0 ? tb07.CO_TUR == turma : tb07.CO_TUR == tb07.CO_TUR)
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU
                       }).Distinct().OrderBy(o => o.NO_ALU);

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }
            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            var res = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                       where tb47.CO_EMP == LoginAuxili.CO_EMP
                       && tb47.CO_ALU == coAlu
                       && tb47.IC_SIT_DOC == "A"
                       select new { ano = tb47.CO_ANO_MES_MAT.Substring(0, 4) }).ToList().OrderBy(w => w.ano).Distinct();

            if (res != null)
            {
                ddlAnoRefer.DataTextField = "ano";
                ddlAnoRefer.DataValueField = "ano";
                ddlAnoRefer.DataSource = res;
                ddlAnoRefer.DataBind();
            }

            ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Eventos Page

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAnos();
        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            Imprime();
        }

        #endregion
    }
}