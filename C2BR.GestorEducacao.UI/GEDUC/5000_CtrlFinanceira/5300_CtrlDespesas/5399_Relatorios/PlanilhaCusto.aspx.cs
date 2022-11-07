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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5300_CtrlDespesas._5399_Relatorios
{
    public partial class PlanilhaCusto : System.Web.UI.Page
    {
        public string Sigla;
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

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
                CarregaUnidades();
                CarregaAno();
                CarregaModalidade();
                CarregaSerie();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            ///Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            int coEmp, coUndCont, coMod, coSer;
            DateTime dtInicio, dtFim;
            string infos, parametros, coAno;
            //bool valores = false;
            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coEmp = LoginAuxili.CO_EMP;
            coUndCont = AuxiliFormatoExibicao.conversorGenerico<int, string>(ddlUnidade.SelectedValue);
            coMod = AuxiliFormatoExibicao.conversorGenerico<int, string>(ddlModalidade.SelectedValue);
            coSer = AuxiliFormatoExibicao.conversorGenerico<int, string>(ddlSerie.SelectedValue);
            //coTur = AuxiliFormatoExibicao.conversorGenerico<int, string>(ddlTurma.SelectedValue);
            coAno = ddlAno.SelectedValue;
            //coSit = ddlSituacao.SelectedValue;
            dtInicio = (txtDtInicio.Text != "" ? DateTime.ParseExact(txtDtInicio.Text, "dd/MM/yyyy", null) : DateTime.MinValue);
            dtFim = (txtDataFim.Text != "" ? DateTime.ParseExact(txtDataFim.Text, "dd/MM/yyyy", null) : DateTime.MinValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            if ((dtInicio != null && dtInicio != DateTime.MinValue) && (dtFim != null && dtFim != DateTime.MinValue))
            {
                if (dtFim < dtInicio)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data Final não pode ser menor que Data Inicial.");
                    return;
                }
            }

            String HoraInicial;
            int a = dtInicio.Day;
            int b = dtInicio.Month;

            //HoraInicial = String.Format("{0}/{1}", a, b);
            HoraInicial = dtInicio.ToString("dd/MM/yyyy");

            String HoraFinal;
            int c = dtFim.Day;
            int d = dtFim.Month;

            //HoraFinal = String.Format("{0}/{1}", c, d);
            HoraFinal = dtFim.ToString("dd/MM/yyyy");


            
        

            parametros = "-";
                       

            //parametros = string.Format("(Unidade de Contrato: {0} - Modalidade: {1} - Série/Turma: {2} - Turma: {3} - Ano Letivo: {4} - Situação: {7} - Período: {5} à {6} )",
            //            ddlUnidade.SelectedItem,
            //            ddlModalidade.SelectedItem,
            //            ddlSerie.SelectedItem,
            //            ddlTurma.SelectedItem,
            //            ddlAno.SelectedItem,
            //            (txtDtInicio.Text != "" ? txtDtInicio.Text : "Todos"),
            //            (txtDataFim.Text != "" ? txtDataFim.Text : "Todos"),
            //            ddlSituacao.SelectedItem);


            RptPlanilhaCusto rpt = new RptPlanilhaCusto();
            lRetorno = rpt.InitReport(parametros, coUndCont, infos, coMod, coSer, coAno, dtInicio, dtFim);
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
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega todos os anos com matrículas do sistema
        /// </summary>
        private void CarregaAno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAno(ddlAno, coEmp, false);
        }

        /// <summary>
        /// Carrega todos as modalidades disponíveis no sistema
        /// </summary>
        private void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega todas as série de acordo com a modalidade
        /// </summary>
        private void CarregaSerie()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, modalidade, coEmp, true);
        }

        /// <summary>
        /// Carrega todos os anos de acordo com a modalidade, série e ano
        /// </summary>
        //private void CarregaTurma()
        //{
        //    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
        //    int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
        //    int serie = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
        //    AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, true);
        //}

        /// <summary>
        /// Carrega todos os status de matrícula de aluno
        /// </summary>
        //private void CarregaSituacao()
        //{
        //    AuxiliCarregamentos.CarregaSituacaoMatricula(ddlSituacao, true);
        //}

        #endregion

        #region Eventos de componentes da página

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
                CarregaAno();
                CarregaModalidade();
                CarregaSerie();
                //CarregaTurma();
                //CarregaSituacao();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
                CarregaModalidade();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
                CarregaSerie();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
                //CarregaTurma();
        }

        #endregion
    }
}