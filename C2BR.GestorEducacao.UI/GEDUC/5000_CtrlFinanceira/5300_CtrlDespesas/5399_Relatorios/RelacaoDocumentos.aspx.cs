//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE DESPESAS (CONTAS A PAGAR)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE DESPESAS/COMPROMISSOS DE PAGAMENTOS - GERAL
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5300_CtrlDespesas;
using DevExpress.XtraPrinting.Drawing;
using System.Drawing;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5399_Relatorios
{
    public partial class RelacaoDocumentos : System.Web.UI.Page
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
                CarregaDropDown();
                CarregaAgrupadores();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_FORN, strP_CO_AGRUP, strP_CO_HIST;
            string strP_NU_DOC, strP_DT_VEN_DOC, strP_IC_SIT_DOC, strTipoPesq;
            DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Inicializa as variáveis
            strP_NU_DOC = null;
            strP_DT_VEN_DOC = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strTipoPesq = ddlPesqPor.SelectedValue;

            strP_IC_SIT_DOC = ddlSituacao.SelectedValue;
            strP_CO_FORN = int.Parse(ddlFornecedor.SelectedValue);
            strP_CO_HIST = int.Parse(ddlHistorico.SelectedValue);
            strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strP_NU_DOC = txtNumDoc.Text;
            strP_DT_VEN_DOC = txtDataVencimento.Text;
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "(Busca por: " + ddlPesqPor.SelectedItem.Text + " - Período de: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text +
                            " - Fornecedor: " + ddlFornecedor.SelectedItem.Text + " - Histórico: " + ddlHistorico.SelectedItem.Text + 
                            " - Agrupador: " + ddlAgrupador.SelectedItem.Text +" - Situacao: " + ddlSituacao.SelectedItem.Text + ")"; // ddlStaDocumento.SelectedItem.ToString() +            

            RptRelacaoDocumentos fpcb = new RptRelacaoDocumentos();

            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strTipoPesq, strP_CO_FORN,
                        strP_CO_HIST, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM, strP_DT_VEN_DOC, strP_NU_DOC, strP_CO_AGRUP, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Fornecedores
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlFornecedor.DataSource = TB41_FORNEC.RetornaTodosRegistros().OrderBy(f => f.NO_FAN_FOR);

            ddlFornecedor.DataTextField = "NO_FAN_FOR";
            ddlFornecedor.DataValueField = "CO_FORN";
            ddlFornecedor.DataBind();

            ddlFornecedor.Items.Insert(0, new ListItem("Todos", "0"));
            // historico

            ddlHistorico.DataSource = TB39_HISTORICO.RetornaTodosRegistros().Where( p => p.FLA_TIPO_HISTORICO == "D").OrderBy(d => d.DE_HISTORICO);
            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataBind();

            ddlHistorico.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "D" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion
    }
}
