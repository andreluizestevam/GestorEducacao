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
// 22/01/2015| Maxwell Almeida           | Criação do relatório para emissão de log com dados consolidados

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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE._0599_Relatorios
{
    public partial class CurvaABCLog : System.Web.UI.Page
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
            int coOrdenacao = int.Parse(ddlClassificacao.SelectedValue);
            string strP_ACAO, noPeriodo;
            DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Inicializa as variáveis
            strParametrosRelatorio = "";
            strP_ACAO = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            strP_CO_COL = (!string.IsNullOrEmpty(ddlUsuario.SelectedValue) ? int.Parse(ddlUsuario.SelectedValue) : 0);
            strP_ACAO = ddlAcao.SelectedValue;
            strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);

            strParametrosRelatorio = "( Unid: " + ddlUnidade.SelectedItem.Text.ToUpper() + " - Usuário: "
               + ddlUsuario.SelectedItem.Text.ToUpper() + " - Ação: " + ddlAcao.SelectedItem.Text.ToUpper()
               + " - Relatório ordenado por " + ddlClassificacao.SelectedItem.Text + " - " + ddlTipoOrdem.SelectedItem.Text + " )";

            noPeriodo = txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text;


            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/0000_ConfiguracaoAmbiente/0500_SuporteOperacionalGE/0599_Relatorios/CurvaABCLog.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu + " - " + noPeriodo : "");
            #endregion

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptCurvaABCLog rel = new RptCurvaABCLog();
            lRetorno = rel.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_EMP, strP_CO_COL, strP_ACAO, strP_DT_INI, strP_DT_FIM, strINFOS, coOrdenacao, ddlTipoOrdem.SelectedValue, noPeriodo, ddlClassif.SelectedValue, NO_RELATORIO);
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
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.CO_EMP, tb25.sigla }).OrderBy(e => e.sigla);

            ddlUnidade.DataTextField = "sigla";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));

            CarregaUsuarios();
        }

        /// <summary>
        /// Método que carrega o dropdown de Usuários
        /// </summary>
        private void CarregaUsuarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlUsuario.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                     where (coEmp != 0 ? tb03.CO_EMP == coEmp : 0 == 0)
                                     select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

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

        protected void ddlClassif_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClassificacao.Items.Clear();

            //Muda as opções disponíveis para ordenação de acordo com a alteração da classificação
            if (ddlClassif.SelectedValue == "A")
            {
                ddlClassificacao.Items.Insert(0, new ListItem("QTA - Qtde Total de Acessos", "6"));
                ddlClassificacao.Items.Insert(0, new ListItem("QACA - Qtde de Acessos Com Acao", "5"));
                ddlClassificacao.Items.Insert(0, new ListItem("QASA - Qtde de Acessos Sem Acao", "4"));
                ddlClassificacao.Items.Insert(0, new ListItem("TAF - Tempo de Acesso Funcionalidade", "3"));
                ddlClassificacao.Items.Insert(0, new ListItem("QUA - Quantidade Usuários de Acesso", "2"));
                ddlClassificacao.Items.Insert(0, new ListItem("Atividade", "1"));
            }
            else
            {
                ddlClassificacao.Items.Insert(0, new ListItem("QTA - Qtde Total de Acessos", "6"));
                ddlClassificacao.Items.Insert(0, new ListItem("QACA - Qtde de Acessos Com Acao", "5"));
                ddlClassificacao.Items.Insert(0, new ListItem("QASA - Qtde de Acessos Sem Acao", "4"));
                ddlClassificacao.Items.Insert(0, new ListItem("TAU - Total de Acessos do Usuário", "3"));
                ddlClassificacao.Items.Insert(0, new ListItem("QFA - Quantidade de Funcionalidades Acessadas", "2"));
                ddlClassificacao.Items.Insert(0, new ListItem("Usuário", "1"));
            }
        }
    }
}