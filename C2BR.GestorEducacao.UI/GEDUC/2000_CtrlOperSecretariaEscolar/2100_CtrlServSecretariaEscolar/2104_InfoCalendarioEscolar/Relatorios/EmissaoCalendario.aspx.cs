//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: INFORMAÇÕES DE CALENDÁRIO ESCOLAR
// OBJETIVO: EMISSÃO DO CALENDÁRIO ESCOLAR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2104_Calendario;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using DevExpress.XtraScheduler.Reporting;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.Relatorios
{
    public partial class EmissaoCalendario : System.Web.UI.Page
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
                CarregaDropDown();
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
          /*  string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_ANO_REFER, strP_TP_CALEND;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strParametrosRelatorio = null;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelCalendario");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_ANO_REFER = null;
            strP_TP_CALEND = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_ANO_REFER = ddlAnoRefer.SelectedValue;
            strP_TP_CALEND = ddlTpCalendario.SelectedValue;

            if (ddlTpCalendario.SelectedValue != "T")
                strParametrosRelatorio = "CALENDÁRIO " + ddlTpCalendario.SelectedItem.ToString() + " - " + ddlAnoRefer.SelectedValue.ToString();
            else
                strParametrosRelatorio = "CALENDÁRIO INSTITUCIONAL - " + ddlAnoRefer.SelectedValue.ToString();
            string 
            lIRelatorioWeb = varRelatorioWeb.CreateChannel();*/
            string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            int tipo = int.Parse(ddlTpCalendario.SelectedValue);
            string Ano = ddlAnoRefer.SelectedValue;
            string parametros = "";
            parametros = "(Unidade: " + ddlUnidade.Text + " - Ano: " + ddlAnoRefer.Text + " - Tipo: " + ddlTpCalendario.Text + ")";
            int lRetorno;
            RptCalendario2013 rpt = new RptCalendario2013();
            lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, tipo, parametros, Ano);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

           /* lRetorno = lIRelatorioWeb.RelCalendario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ANO_REFER, strP_TP_CALEND);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();*/
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares, Ano de Referência e Tipo de Calendário
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

            ddlAnoRefer.DataSource = (from tb157 in TB157_CALENDARIO_ATIVIDADES.RetornaTodosRegistros()
                                      select new { tb157.CAL_ANO_REFER_CALEND }).Distinct().OrderByDescending(c => c.CAL_ANO_REFER_CALEND);

            ddlAnoRefer.DataTextField = "CAL_ANO_REFER_CALEND";
            ddlAnoRefer.DataValueField = "CAL_ANO_REFER_CALEND";
            ddlAnoRefer.DataBind();

            ddlTpCalendario.Items.Clear();

            ddlTpCalendario.DataSource = TB152_CALENDARIO_TIPO.RetornaTodosRegistros().Where( c => c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO );

            ddlTpCalendario.DataTextField = "CAT_NOME_TIPO_CALEN";
            ddlTpCalendario.DataValueField = "CAT_ID_TIPO_CALEN";
            ddlTpCalendario.DataBind();

            ddlTpCalendario.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion
    }
}
