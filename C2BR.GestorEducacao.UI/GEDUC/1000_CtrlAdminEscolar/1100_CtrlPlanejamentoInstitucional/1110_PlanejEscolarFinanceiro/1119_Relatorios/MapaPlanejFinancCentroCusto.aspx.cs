//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: MAPA ANALÍTICO/SINTÉTICO DE PLANEJAMENTO ANUAL FINANCEIRO POR CENTRO DE CUSTO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1119_Relatorios
{
    public partial class MapaPlanejFinancCentroCusto : System.Web.UI.Page
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
                CarregaDepartamento();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_TP_CONTA, strP_TP_RELATORIO, strP_CO_DEPTO;
            int strP_CO_EMP, strP_CO_ANO_INI, strP_CO_ANO_FIM;

            //--------> Inicializa as variáveis
            strP_TP_CONTA = null;
            strP_TP_RELATORIO = null;
            strP_CO_DEPTO = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_TP_CONTA = ddlVisualizacao.SelectedValue;
            strP_TP_RELATORIO = ddlTipo.SelectedValue;
            strP_CO_ANO_INI = int.Parse(txtAnoBaseIni.Text);
            strP_CO_ANO_FIM = int.Parse(txtAnoBaseFim.Text);
            strP_CO_DEPTO = ddlDepartamento.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            if (ddlVisualizacao.SelectedValue == "C")
            {
                if (ddlTipo.SelectedValue.ToString() == "A")
                    strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Ano Base: " + txtAnoBaseIni.Text + " à " + txtAnoBaseFim.Text +
                " - Visualização: Receitas - Tipo: Analítico )";
                else
                    strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Ano Base: " + txtAnoBaseIni.Text + " à " + txtAnoBaseFim.Text +
                " - Visualização: Receitas - Tipo: Diferença )";
            }
            else
            {
                if (ddlTipo.SelectedValue.ToString() == "A")
                    strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Ano Base: " + txtAnoBaseIni.Text + " à " + txtAnoBaseFim.Text +
                " - Visualização: Despesas - Tipo: Analítico )";
                else
                    strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Ano Base: " + txtAnoBaseIni.Text + " à " + txtAnoBaseFim.Text +
                " - Visualização: Despesas - Tipo: Diferença )";
            }

            if (ddlTipo.SelectedValue.ToString() == "A")
            {
                RptMapaPlanejFinancCentroCusto fpcb = new RptMapaPlanejFinancCentroCusto();
                lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_DEPTO,
                    strP_CO_ANO_INI, strP_CO_ANO_FIM, strP_TP_CONTA, strINFOS);
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                RptMapaPlanejFinancCentroCustoDif fpcb = new RptMapaPlanejFinancCentroCustoDif();
                lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_DEPTO,
                    strP_CO_ANO_INI, strP_CO_ANO_FIM, strP_TP_CONTA, strINFOS);
                Session["Report"] = fpcb;
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
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

        }

        /// <summary>
        /// Método que carrega o dropdown de Departamentos
        /// </summary>
        void CarregaDepartamento()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (coEmp != 0)
            {
                ddlDepartamento.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                              where tb14.TB25_EMPRESA.CO_EMP == coEmp
                                              select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).Distinct().OrderBy(d => d.NO_DEPTO);

                ddlDepartamento.DataTextField = "NO_DEPTO";
                ddlDepartamento.DataValueField = "CO_DEPTO";
                ddlDepartamento.DataBind();

                ddlDepartamento.Items.Insert(0, new ListItem("Todos", "T"));
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento();
        }
    }
}
