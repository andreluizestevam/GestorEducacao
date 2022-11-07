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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3070_CtrlEmailSaude._3079_Relatorios
{
    public partial class DemonEnvioEmailPaci : System.Web.UI.Page
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
                CarregaPacientes();

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
            string infos, parametros;
            int strP_CO_EMP, CO_PACI;
           DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            CO_PACI = int.Parse(ddlParce.SelectedValue);
            strP_DT_INI = Convert.ToDateTime(txtDataPeriodoIni.Text);
            strP_DT_FIM = Convert.ToDateTime(txtDataPeriodoFim.Text);

            //--------> Prepara a linha de parâmetros
            string dtIni = (strP_DT_INI.ToString() != "" ? strP_DT_INI.ToString() : "Todos");
            string dtFim = (strP_DT_FIM.ToString() != "" ? strP_DT_FIM.ToString() : "Todos");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Período inicial: " + dtIni + " - Período final: " + dtFim + ")"; 

            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3070_CtrlEmailSaude/3079_Relatorios/DemonEnvioEmailPaci.aspx");
            RptDemonEnvioEmailPaci rpt = new RptDemonEnvioEmailPaci();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, CO_PACI, NomeFuncionalidadeCadastrada, strP_DT_INI, strP_DT_FIM);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        
        /// <summary>
        /// Método responsável por carregar os alunos
        /// </summary>
        private void CarregaPacientes()
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                       select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (res != null)
            {
                ddlParce.DataTextField = "NO_ALU";
                ddlParce.DataValueField = "CO_ALU";
                ddlParce.DataSource = res;
                ddlParce.DataBind();
            }

            ddlParce.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region SelectedIndexChanged

        protected void ddlParce_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            PadraoRelatoriosCorrente_OnAcaoGeraRelatorio();
        }


    }
}