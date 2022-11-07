//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS/RECURSOS - GERAL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios
{
    public partial class EmissMovtoRetorCobraBanca : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                CarregaBancos();
                CarregaAgencias();
                CarregaContas();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strINFOS, strP_IDEBANCO, strP_CO_CONTA, strP_DT_INI_VENC, strP_DT_FIM_VENC, strP_DT_INI_CREDI, strP_DT_FIM_CREDI,
                strP_STATUS, strP_ORDEM;
            int strP_CO_AGENCIA;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_IDEBANCO = ddlBanco.SelectedValue;
            strP_CO_AGENCIA = int.Parse(ddlAgencia.SelectedValue);
            strP_CO_CONTA = ddlConta.SelectedValue;
            strP_DT_INI_VENC = txtDtVenctoIni.Text;
            strP_DT_FIM_VENC = txtDtVenctoFim.Text;
            strP_DT_INI_CREDI = txtDataPeriodoIni.Text;
            strP_DT_FIM_CREDI = txtDataPeriodoFim.Text;
            strP_STATUS = ddlStaDocumento.SelectedValue;
            strP_ORDEM = ddlOrdenRelat.SelectedValue;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Banco: " + ddlBanco.SelectedItem.ToString() + " - Ag.: " + ddlAgencia.SelectedItem.ToString()
                + " - Conta: " + ddlConta.SelectedItem.ToString() +
                " - Periodo Vecto: " + (txtDtVenctoIni.Text != "" ? DateTime.Parse(txtDtVenctoIni.Text).ToString("dd/MM/yy") : "***") +
                " à " + (txtDtVenctoFim.Text != "" ? DateTime.Parse(txtDtVenctoFim.Text).ToString("dd/MM/yy") : "***") +
                " - Periodo Crédito: " + (txtDataPeriodoIni.Text != "" ? DateTime.Parse(txtDataPeriodoIni.Text).ToString("dd/MM/yy") : "***") +
                " à " + (txtDataPeriodoFim.Text != "" ? DateTime.Parse(txtDataPeriodoFim.Text).ToString("dd/MM/yy") : "***") +
                " - Status: " + ddlStaDocumento.SelectedItem.ToString() + " - Ordenado por: " + ddlOrdenRelat.SelectedItem.ToString() + " )";

            RptEmissMovtoRetorCobraBanca fpcb = new RptEmissMovtoRetorCobraBanca();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP,
                strP_IDEBANCO, strP_CO_AGENCIA, strP_CO_CONTA, strP_STATUS, strP_DT_INI_VENC, strP_DT_FIM_VENC, strP_DT_INI_CREDI, strP_DT_FIM_CREDI, strP_ORDEM, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy(b => b.DESBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agências
        /// </summary>
        private void CarregaAgencias()
        {
            string strIdeBanco = ddlBanco.SelectedValue;

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros().AsEnumerable()
                                     where tb30.IDEBANCO == strIdeBanco
                                     select new
                                     {
                                         tb30.CO_AGENCIA,
                                         DESCRICAO = string.IsNullOrEmpty(ddlBanco.SelectedValue) ?
                                                 string.Format("({0}) {1} - {2}", tb30.IDEBANCO, tb30.CO_AGENCIA, tb30.NO_AGENCIA) :
                                                 string.Format("{0} - {1}", tb30.CO_AGENCIA, tb30.NO_AGENCIA)
                                     }).OrderBy(a => a.DESCRICAO);

            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataTextField = "DESCRICAO";
            ddlAgencia.DataBind();

            ddlAgencia.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Contas
        /// </summary>
        private void CarregaContas()
        {
            string strIdeBanco = ddlBanco.SelectedValue;
            int coAgencia = int.Parse(ddlAgencia.SelectedValue);

            ddlConta.DataSource = (from tb224 in TB224_CONTA_CORRENTE.RetornaTodosRegistros()
                                   where tb224.TB30_AGENCIA.IDEBANCO == strIdeBanco && tb224.TB30_AGENCIA.CO_AGENCIA == coAgencia
                                   && tb224.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                   select new { tb224.CO_CONTA }).OrderBy(a => a.CO_CONTA);

            ddlConta.DataValueField = "CO_CONTA";
            ddlConta.DataTextField = "CO_CONTA";
            ddlConta.DataBind();

            ddlConta.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
            CarregaContas();
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContas();
        }
    }
}
