//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: SUPORTE OPERACIONAL DA BASE DE DADOS
// OBJETIVO: EMISSÃO DO LOG DE ATIVIDADES DO USUÁRIO
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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0599_Relatorios
{
    public partial class LogAtividUsuario : System.Web.UI.Page
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
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_FUNCIO, strP_CO_COL;
            string strP_ACAO;
            DateTime strP_DT_INI, strP_DT_FIM;

//--------> Inicializa as variáveis
            strParametrosRelatorio = "";
            strP_ACAO = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_FUNCIO = int.Parse(ddlFuncionalidade.SelectedValue);
            strP_CO_COL = int.Parse(ddlUsuario.SelectedValue);
            strP_ACAO = ddlAcao.SelectedValue;
            strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptLogAtividUsuario rel = new RptLogAtividUsuario();
            lRetorno = rel.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_FUNCIO, strP_CO_COL, strP_ACAO, strP_DT_INI, strP_DT_FIM, strINFOS);
            Session["Report"] = rel;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }                

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Funcionalidades
        /// </summary>
        private void CarregaDropDown()
        {
            ddlFuncionalidade.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                                            where admModulo.flaStatus == "A"
                                            select new { admModulo.ideAdmModulo, admModulo.nomModulo }).OrderBy( a => a.nomModulo );

            ddlFuncionalidade.DataTextField = "nomModulo";
            ddlFuncionalidade.DataValueField = "ideAdmModulo";
            ddlFuncionalidade.DataBind();

            ddlFuncionalidade.Items.Insert(0, new ListItem("Todos", "0"));

            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.CO_EMP, tb25.sigla }).OrderBy( e => e.sigla );

            ddlUnidade.DataTextField = "sigla";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", "T"));

            CarregaUsuarios();
        }

        /// <summary>
        /// Método que carrega o dropdown de Usuários
        /// </summary>
        private void CarregaUsuarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlUsuario.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                     select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlUsuario.DataTextField = "NO_COL";
            ddlUsuario.DataValueField = "CO_COL";
            ddlUsuario.DataBind();

            ddlUsuario.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion      
  
        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuarios();
        }
    }
}
