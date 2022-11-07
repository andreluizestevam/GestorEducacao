using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios
{
    public partial class RelacaoAlunoTelefones : System.Web.UI.Page
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
                txtUnidadeEscola.Text = LoginAuxili.NO_FANTAS_EMP;
                CarregaUnidades();
                liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaOrdemRelatorio();
            }

        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR;
            string infos, parametros/*,strP_CO_ANO_REFER,strTipo*/;
            int strP_CO_EMP_REF, strP_CO_EMP;
            string strP_CO_ANO_MES_MAT;
            string ordem;

            //--------> Inicializa as variáveis
            // strP_CO_ANO_REFER = null;
            strP_CO_ANO_MES_MAT = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            ordem = ddlOrdem.SelectedValue;

            // Variáveis para os parametros
            string SIG_EMP = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).sigla;
            string NO_MODU = strP_CO_MODU_CUR != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(strP_CO_MODU_CUR).DE_MODU_CUR : "Todos";
            string NO_CUR = strP_CO_CUR != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR).NO_CUR : "Todos";
            string NO_TUR = strP_CO_TUR != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).NO_TURMA : "Todos";


            // strP_CO_ANO_REFER = txtAnoBase.Text;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Unidade: " + SIG_EMP.Trim() + " - Ano: " + strP_CO_ANO_MES_MAT + " - Modalidade: " + NO_MODU + " - Série/Curso: " + NO_CUR + " - Turma: " + NO_TUR + ")";

            RptRelacaoAlunosTelefones rpt = new RptRelacaoAlunosTelefones();
            lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, infos, strP_CO_EMP_REF, ordem, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
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
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";

            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Todas", "0"));
            //ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddlTurma.Enabled = true;

                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
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
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            string ano = DateTime.Now.Year.ToString();
            string proximoAno = (DateTime.Now.Year + 1).ToString();
            ddlAnoRefer.Items.Insert(0, new ListItem(ano, ano));
            ddlAnoRefer.Items.Insert(0, new ListItem(proximoAno, proximoAno));
            ddlAnoRefer.DataBind();

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
            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();
            /*=========================================================
             * Esta linha foi comentada por que o relatório precisa
             * utilizar a serie/curso da unidade corrente, e não
             * da unidade de contrato.
             *=========================================================
             * Victor Martins Machado - 06/08/2013
             *========================================================*/
            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coEmp = LoginAuxili.CO_EMP;
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
        }

        private void CarregaOrdemRelatorio()
        {
            ddlOrdem.Items.Clear();
            ddlOrdem.Items.Add(new ListItem() { Text = "Nire", Value = "nire" });
            ddlOrdem.Items.Add(new ListItem() { Text = "Nome", Value = "nome" });

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
    }
}