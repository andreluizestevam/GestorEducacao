using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3050_CtrlEmail;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3050_CtrlEmail._3059_Relatorios
{
    public partial class DemonEnvioEmail : System.Web.UI.Page
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
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAlunos();
                CarregaResponsavel();

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
            string infos, parametros;
            int strP_CO_EMP, CO_ALU, CO_RESP;
            DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_MODU_CUR = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            CO_ALU = int.Parse(ddlAluno.SelectedValue);
            CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
            strP_DT_INI = Convert.ToDateTime(txtDataPeriodoIni.Text);
            strP_DT_FIM = Convert.ToDateTime(txtDataPeriodoFim.Text);

            //--------> Prepara a linha de parâmetros
            string deMod = (strP_CO_MODU_CUR != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(strP_CO_MODU_CUR).DE_MODU_CUR : "Todos");
            string noCur = (strP_CO_CUR != 0 ? TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == strP_CO_CUR).FirstOrDefault().NO_CUR : "Todos");
            string noTur = (strP_CO_TUR != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).NO_TURMA : "Todos");
            string dtIni = (strP_DT_INI.ToString() != "" ? strP_DT_INI.ToString() : "Todos");
            string dtFim = (strP_DT_FIM.ToString() != "" ? strP_DT_FIM.ToString() : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "( Modalidade: " + deMod + " - Curso: " + noCur + " - Turma: " + noTur + "  - Período inicial: " + dtIni + " - Período final: " + dtFim + ")"; 

            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3050_CtrlEmail/3059_Relatorios/DemonEnvioEmail.aspx");
            RptDemonEnvioEmail rpt = new RptDemonEnvioEmail();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, strP_CO_CUR, strP_CO_MODU_CUR, strP_CO_TUR, LoginAuxili.CO_EMP, CO_ALU, NomeFuncionalidadeCadastrada, CO_RESP, strP_DT_INI, strP_DT_FIM);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);

            CarregaSerieCurso();
            CarregaTurma();
            CarregaAlunos();
            CarregaResponsavel();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, true);

            CarregaTurma();
            CarregaAlunos();
            CarregaResponsavel();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, true);

            CarregaAlunos();
            CarregaResponsavel();
        }

        /// <summary>
        /// Método responsável por carregar os alunos
        /// </summary>
        private void CarregaAlunos()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = (ddlTurma.SelectedValue != "0" ? int.Parse(ddlTurma.SelectedValue) : 0);

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_EMP == LoginAuxili.CO_EMP
                       select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (modalidade != 0)
            {
                res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.CO_EMP == LoginAuxili.CO_EMP
                           && tb07.CO_MODU_CUR == modalidade
                           select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();
                if (serie != 0)
                {
                    res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.CO_EMP == LoginAuxili.CO_EMP
                           && tb07.CO_MODU_CUR == modalidade
                           && tb07.CO_CUR == serie
                           select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();
                    if (turma != 0) 
                    {
                        res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.CO_EMP == LoginAuxili.CO_EMP
                               && tb07.CO_MODU_CUR == modalidade
                               && tb07.CO_CUR == serie
                               && tb07.CO_TUR == turma
                               select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();
                    }
                }
            }

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));

            CarregaResponsavel();
        }

        /// <summary>
        /// Método responsável por carregar os responsável
        /// </summary>
        private void CarregaResponsavel()
        {
            int aluno = (ddlAluno.SelectedValue != "0" ? int.Parse(ddlAluno.SelectedValue) : 0);

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                       where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RESP,
                       }).ToList();

            if (aluno != 0)
            {
                 res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                           where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           && tb07.CO_ALU == aluno
                           select new
                           {
                               tb108.NO_RESP,
                               tb108.CO_RESP,
                           }).ToList();
            }

            if (res != null)
            {
                ddlResponsavel.DataTextField = "NO_RESP";
                ddlResponsavel.DataValueField = "CO_RESP";
                ddlResponsavel.DataSource = res;
                ddlResponsavel.DataBind();
            }

            ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region SelectedIndexChanged

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAlunos();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAlunos();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaResponsavel();
        }

        #endregion

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            PadraoRelatoriosCorrente_OnAcaoGeraRelatorio();
        }


    }
}