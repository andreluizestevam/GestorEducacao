//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQUÊNCIA
// OBJETIVO: CURVA ABC DE FREQUÊNCIA (FUNCIONÁRIOS)
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1229_Relatorios
{
    public partial class CurvaABCFreqColaborador : System.Web.UI.Page
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

        //====> Método que faz a chamada de outro método de acordo com o ddlOpcoesRelatorio
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlOpcoesRelatorio.SelectedValue == "F")
                RelPorFuncao();
            else
                RelPorInstituicao();

        }

        //====> Processo de Geração do Relatório
        void RelPorFuncao()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;
            string infos;

            //--------> Variáveis de parâmetro do Relatório
            int codEmp, codFuncao;
            DateTime dtInicio, dtFim;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codFuncao = ddlDescOpRelatorio.SelectedValue == "T"
                ? 0 : int.Parse(ddlDescOpRelatorio.SelectedValue);
            dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptCurvaABCFreqFuncFuncao rpt = new RptCurvaABCFreqFuncFuncao();
            lRetorno = rpt.InitReport(codEmp, codFuncao, dtInicio, dtFim, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Processo de Geração do Relatório
        void RelPorInstituicao()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;
            string infos;

            //--------> Variáveis de parâmetro do Relatório
            int codEmp, codInst;
            DateTime dtInicio, dtFim;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codInst = int.Parse(ddlDescOpRelatorio.SelectedValue);
            dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptCurvaABCFreqFuncInstituicao rpt = new RptCurvaABCFreqFuncInstituicao();
            lRetorno = rpt.InitReport(codEmp, codInst, dtInicio, dtFim, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
         
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown ddlDescOpRelatorio
        /// </summary>
        private void CarregaDropDown()
        {
            lblDescOpRelatorio.Text = "Unidade/Escola";

            ddlDescOpRelatorio.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                             join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                             where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                             select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlDescOpRelatorio.DataTextField = "NO_FANTAS_EMP";
            ddlDescOpRelatorio.DataValueField = "CO_EMP";
            ddlDescOpRelatorio.DataBind();

            ddlDescOpRelatorio.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlDescOpRelatorio.Width = 226;
        }
        #endregion

        //====> Método que carrega o dropdown ddlDescOpRelatorio de acordo com as opçoes
        protected void ddlOpcoesRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOpcoesRelatorio.SelectedValue == "U")
            {
                lblDescOpRelatorio.Text = "Unidade/Escola";

                ddlDescOpRelatorio.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                 join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                                 where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                                 select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(u => u.NO_FANTAS_EMP);

                ddlDescOpRelatorio.DataTextField = "NO_FANTAS_EMP";
                ddlDescOpRelatorio.DataValueField = "CO_EMP";
                ddlDescOpRelatorio.DataBind();
                ddlDescOpRelatorio.Width = 226;
            }
            else
            {
                lblDescOpRelatorio.Text = "Função";
                ddlDescOpRelatorio.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

                ddlDescOpRelatorio.DataTextField = "NO_FUN";
                ddlDescOpRelatorio.DataValueField = "CO_FUN";
                ddlDescOpRelatorio.DataBind();
                ddlDescOpRelatorio.Width = 200;
                ddlDescOpRelatorio.Items.Insert(0, new ListItem("Todos", "T"));
            }
        }
    }
}
