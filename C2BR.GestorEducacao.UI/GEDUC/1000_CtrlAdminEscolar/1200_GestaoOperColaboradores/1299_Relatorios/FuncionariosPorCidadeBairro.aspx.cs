//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: RELAÇÃO DE COLABORADORES POR CIDADE E BAIRRO
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
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores;


//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios
{
    public partial class FuncionariosPorCidadeBairro : System.Web.UI.Page
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
                CarregaUnidadesLotacaoContrato();
                CarregaDropDown();
                CarregaCidades();
                CarregaBairros();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/1000_CtrlAdminEscolar/1200_GestaoOperColaboradores/1299_Relatorios/FuncionariosPorCidadeBairro.aspx");
            //--------> Variáveis de parâmetro do Relatório
            string strP_UF, strP_CO_SITU_COL, strINFOS;
            int strP_CO_EMP, strP_CO_EMP_COL, strP_CO_CIDADE, strP_CO_BAIRRO;

            //--------> Inicializa as variáveis

            strP_UF = null;
            strP_CO_SITU_COL = null;

            string url1 = HttpContext.Current.Request.Url.AbsolutePath;
            string url2 = HttpContext.Current.Request.Url.AbsoluteUri;
            string url3 = HttpContext.Current.Request.Url.Authority;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_COL = int.Parse(ddlUnidade.SelectedValue);
            strP_UF = ddlUF.SelectedValue;
            strP_CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
            strP_CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
            strP_CO_SITU_COL = ddlSituacao.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);            

            RptColaborPorCidadeBairro fpcb = new RptColaborPorCidadeBairro();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_SITU_COL,strP_CO_EMP_UNID_CONT, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strINFOS, NomeFuncionalidadeCadastrada.ToUpper());
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

        }
        #endregion

        #region Carregamento DropDown
        /// <summary>
        /// Método que carrega o Unidade
        /// </summary>
        private void CarregaUnidadesLotacaoContrato()
        {

            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);

        }


        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.Items.Clear();

            if (ddlUF.SelectedValue != "T")
            {
                ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

                ddlCidade.DataTextField = "NO_CIDADE";
                ddlCidade.DataValueField = "CO_CIDADE";
                ddlCidade.DataBind();
            }

            ddlCidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "0" ? int.Parse(ddlCidade.SelectedValue) : 0;

            ddlBairro.Items.Clear();

            if (coCidade != 0)
            {
                ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataBind();
            }

            ddlBairro.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e UFs
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

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "0"));

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("Todas", "T"));
        }
        #endregion

        protected void ddlUF_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }
    }
}
