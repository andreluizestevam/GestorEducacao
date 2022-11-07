using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas;
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

namespace C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro._1119_Relatorios
{
    public partial class PosicaoFinancConsolidada : System.Web.UI.Page
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

            }
        }
               

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS, str_SIT;
            int lRetorno;


            //--------> Variáveis de parâmetro do Relatório
            string strP_TIPO, strP_CO_EMP;
            DateTime? strDT_INI, strDT_FIM;

            //--------> Inicializa as variáveis
            List<string> strSIT_PAG = new List<string>();

            strP_TIPO = null;
            strINFOS = null;
            strP_CO_EMP = null;
            str_SIT = "";
            // 
            strDT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strDT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);

            for (int i = 0; i < cblSituacao.Items.Count; ++i)
            {
                if (cblSituacao.Items[i].Selected)
                {
                    strSIT_PAG.Add(cblSituacao.Items[i].Value);
                    str_SIT += cblSituacao.Items[i].Text + ",";
                }
            }
            str_SIT = str_SIT.TrimEnd(',');
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_TIPO = ddlTipo.SelectedValue;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Emissão Por: " + ddlTipo.SelectedItem.ToString() +
                    " - Situação: " + str_SIT + " - Período: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text+" )";

            RptPosicaoFinancConsCtaReceber fpcb = new RptPosicaoFinancConsCtaReceber();
            lRetorno = fpcb.InitReport(LoginAuxili.CO_EMP, strParametrosRelatorio, strP_CO_EMP,
                strSIT_PAG, strP_TIPO, strDT_INI, strDT_FIM, strINFOS);
            Session["Report"] = fpcb;
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

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion
    }
}