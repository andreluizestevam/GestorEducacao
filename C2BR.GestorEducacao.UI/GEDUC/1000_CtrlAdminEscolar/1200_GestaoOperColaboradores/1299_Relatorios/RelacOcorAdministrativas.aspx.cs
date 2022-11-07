//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: FICHA INDIVIDUAL COMPLETA DE INFORMAÇÕES DE FUNCIONÁRIO
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
    public partial class RelacOcorAdministrativas : System.Web.UI.Page
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
                CarregaFuncionarios();
                CarregaTipoOcorrencia();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string strTIPO_PRESENCA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).TP_PONTO_FUNC;

            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_COL;
            string strTipoOcorrencia, strINFOS;

            //--------> Inicializa as variáveis
            strP_CO_EMP = 0;
            strP_CO_COL = 0;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_COL = int.Parse(ddlFuncionarios.SelectedValue);
            strTipoOcorrencia = ddlTipoOcorrencia.SelectedValue;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptOcorAdministrativas rpt = new RptOcorAdministrativas();
            lRetorno = rpt.InitReport(strP_CO_EMP, strP_CO_COL, strTipoOcorrencia, strINFOS);
            Session["Report"] = rpt;

            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e de Ano Base
        /// </summary>
        private void CarregaDropDown()
        {
            int intAno = DateTime.Now.Year;

            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        //====> Método que carrega o DropDown de Tipo de Ocorrência
        protected void CarregaTipoOcorrencia()
        {
            ddlTipoOcorrencia.DataSource = TB150_TIPO_OCORR.RetornaTodosRegistros().Where(t => t.TP_USU == "F").OrderBy(t => t.DE_TIPO_OCORR);

            ddlTipoOcorrencia.DataTextField = "DE_TIPO_OCORR";
            ddlTipoOcorrencia.DataValueField = "CO_SIGL_OCORR";
            ddlTipoOcorrencia.DataBind();

            ddlTipoOcorrencia.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            var lst = from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                      select new
                      {
                          Nome = tb03.NO_COL,
                          tb03.CO_COL,
                          tb03.FLA_PROFESSOR
                      };

            ddlFuncionarios.DataSource = lst.OrderBy(c => c.Nome);

            ddlFuncionarios.DataTextField = "Nome";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();
            ddlFuncionarios.Items.Insert(0, new ListItem("Todos", "0"));

            ddlFuncionarios.Enabled = ddlFuncionarios.Items.Count > 0;
        }

        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }
    }
}
