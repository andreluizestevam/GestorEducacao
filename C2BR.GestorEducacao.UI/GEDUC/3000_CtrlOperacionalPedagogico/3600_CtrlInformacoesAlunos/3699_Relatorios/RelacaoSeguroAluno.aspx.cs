using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios
{
    public partial class RelacaoSeguroAluno : System.Web.UI.Page
    {

        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos da pagina

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaAno();
                CarregaModalidade();
                CarregaSeries();
                CarregaTurma();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            ///Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            int coEmp, coMod, coSer, coTur;
            string infos, parametros, coAno, coTipo;
            bool cabecalho;
            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coEmp = int.Parse(ddlUnidade.SelectedValue);
            coMod = int.Parse(ddlModalidade.SelectedValue);
            coSer = int.Parse(ddlSerie.SelectedValue);
            coTur = int.Parse(ddlTurma.SelectedValue);
            coAno = ddlAno.SelectedValue;
            cabecalho = cbCabecalho.Checked;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = string.Format("(Unidade: {0} - Modalidade: {1} - Série/Curso: {2} - Turma: {3} - Ano: {4} )",
                        ddlUnidade.SelectedItem,
                        ddlModalidade.SelectedItem,
                        ddlSerie.SelectedItem,
                        ddlTurma.SelectedItem,
                        ddlAno.SelectedItem);

            RptRelacaoSeguroAluno rpt = new RptRelacaoSeguroAluno();
            lRetorno = rpt.InitReport(parametros, coEmp, LoginAuxili.CO_EMP, coMod, coSer, coTur, coAno, infos, cabecalho);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregadores
        /// <summary>
        /// Carrega todas as unidades disponiveis ao usuário atual conectado
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";

            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Todas", "0"));
        }
        /// <summary>
        /// Carrega todos os anos de matrículas ativas no sistema
        /// </summary>
        private void CarregaAno()
        {
            ddlAno.Items.Clear();
            string ano = DateTime.Now.Year.ToString();
            string anoAnterior = (DateTime.Now.Year - 1).ToString();
            ddlAno.Items.Insert(0, new ListItem(ano, ano));
            ddlAno.Items.Insert(0, new ListItem(anoAnterior, anoAnterior));
            ddlAno.Items.Insert(0, new ListItem("Todos", "0"));
            ddlAno.DataBind();
        }
        
        /// <summary>
        /// Carrega todas as modalidades cadastradas para a instituição atual
        /// </summary>
        private void CarregaModalidade()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }
        /// <summary>
        /// Carrega todas as séries para a unidade, ano, modalidade escolhidas
        /// </summary>
        private void CarregaSeries()
        {
            ddlSerie.Items.Clear();
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
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerie.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                       join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                       where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
                                       select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerie.DataTextField = "NO_CUR";
                ddlSerie.DataValueField = "CO_CUR";
                ddlSerie.DataBind();
            }

            ddlSerie.Items.Insert(0, new ListItem("Todas", "0"));
        }
        /// <summary>
        /// Carrega todas as turmas para a unidade, ano, modalidade e série escolhidas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerie.SelectedValue != "0" ? int.Parse(ddlSerie.SelectedValue) : 0;

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

        #endregion

        #region Eventos de componentes do sistema

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAno();
            CarregaModalidade();
            CarregaSeries();
            CarregaTurma();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSeries();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSeries();
            CarregaTurma();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        #endregion


    }
}