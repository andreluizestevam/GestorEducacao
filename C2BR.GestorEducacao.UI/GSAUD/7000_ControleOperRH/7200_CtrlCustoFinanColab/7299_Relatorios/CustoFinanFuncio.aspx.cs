//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: DEMONSTRATIVO OPERACIONAL FINANCEIRO (SALÁRIO) DE COLABORADORES
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7200_CtrlCustoFinanColab._7299_Relatorios
{
    public partial class CustoFinanFuncio : System.Web.UI.Page
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
                CarregaUnidadesLotacaoContrato();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o ddlOpcoesRelatorio
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlOpcoesRelatorio.SelectedValue == "U")
                RelCustoFinFunc();
            else if (ddlOpcoesRelatorio.SelectedValue == "D")
                RelCustoFinFuncDept();
            else if (ddlOpcoesRelatorio.SelectedValue == "F")
                RelCustoFinFuncFunc();
            else if (ddlOpcoesRelatorio.SelectedValue == "C")
                RelCustoFinFuncTpcon();
        }

        //====> Processo de Geração do Relatório
        void RelCustoFinFunc()
        {
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/7000_ControleOperRH/7200_CtrlCustoFinanColab/7299_Relatorios/CustoFinanFuncio.aspx");
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS;
            int strP_CO_EMP, strP_CO_EMP_COL;

            //--------> Inicializa as variáveis
            strP_FLA_PROFESSOR = null;
            strP_CO_SEXO_COL = null;
            strP_tp_def = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strP_CO_EMP_COL = int.Parse(ddlDescOpRelatorio.SelectedValue);
            strP_FLA_PROFESSOR = ddlCategoria.SelectedValue;
            strP_CO_SEXO_COL = ddlSexo.SelectedValue;
            strP_tp_def = ddlDeficiencia.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio += "( " + ddlOpcoesRelatorio.SelectedItem + ": " + ddlDescOpRelatorio.SelectedItem;
            strParametrosRelatorio += " - Categoria: " + ddlCategoria.SelectedItem;
            strParametrosRelatorio += " - Sexo: " + ddlSexo.SelectedItem;
            strParametrosRelatorio += " - Deficiência: " + ddlDeficiencia.SelectedItem + " )";

            RptDemoUnidadeFuncional fpcb = new RptDemoUnidadeFuncional();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_COL, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS, false, NomeFuncionalidadeCadastrada, strP_CO_EMP_UNID_CONT);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Processo de Geração do Relatório
        void RelCustoFinFuncDept()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS; ;
            int strP_CO_EMP, strP_CO_DEP_COL;

            //--------> Inicializa as variáveis
            strP_FLA_PROFESSOR = null;
            strP_CO_SEXO_COL = null;
            strP_tp_def = null;
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/7000_ControleOperRH/7200_CtrlCustoFinanColab/7299_Relatorios/CustoFinanFuncio.aspx.cs");
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_DEP_COL = int.Parse(ddlDescOpRelatorio.SelectedValue);
            strP_FLA_PROFESSOR = ddlCategoria.SelectedValue;
            strP_CO_SEXO_COL = ddlSexo.SelectedValue;
            strP_tp_def = ddlDeficiencia.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strParametrosRelatorio += "( " + ddlOpcoesRelatorio.SelectedItem + ": " + ddlDescOpRelatorio.SelectedItem;
            strParametrosRelatorio += " - Categoria: " + ddlCategoria.SelectedItem;
            strParametrosRelatorio += " - Sexo: " + ddlSexo.SelectedItem;
            strParametrosRelatorio += " - Deficiência: " + ddlDeficiencia.SelectedItem + " )";

            RptDemoDepartamento fpcb = new RptDemoDepartamento();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_DEP_COL, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS, false, strP_CO_EMP_UNID_CONT, NomeFuncionalidadeCadastrada);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Processo de Geração do Relatório
        void RelCustoFinFuncFunc()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/7000_ControleOperRH/7200_CtrlCustoFinanColab/7299_Relatorios/CustoFinanFuncio.aspx.cs");
            //--------> Variáveis de parâmetro do Relatório
            string strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS;
            int strP_CO_EMP, strP_CO_FUN_COL;

            //--------> Inicializa as variáveis
            strP_FLA_PROFESSOR = null;
            strP_CO_SEXO_COL = null;
            strP_tp_def = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_FUN_COL = int.Parse(ddlDescOpRelatorio.SelectedValue);
            strP_FLA_PROFESSOR = ddlCategoria.SelectedValue;
            strP_CO_SEXO_COL = ddlSexo.SelectedValue;
            strP_tp_def = ddlDeficiencia.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strParametrosRelatorio += "( " + ddlOpcoesRelatorio.SelectedItem + ": " + ddlDescOpRelatorio.SelectedItem;
            strParametrosRelatorio += " - Categoria: " + ddlCategoria.SelectedItem;
            strParametrosRelatorio += " - Sexo: " + ddlSexo.SelectedItem;
            strParametrosRelatorio += " - Deficiência: " + ddlDeficiencia.SelectedItem + " )";

            RptDemoFuncao fpcb = new RptDemoFuncao();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_FUN_COL, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS, false, strP_CO_EMP_UNID_CONT, NomeFuncionalidadeCadastrada);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Processo de Geração do Relatório
        void RelCustoFinFuncTpcon()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/7000_ControleOperRH/7200_CtrlCustoFinanColab/7299_Relatorios/CustoFinanFuncio.aspx.cs");
            //--------> Variáveis de parâmetro do Relatório
            string strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS; ;
            int strP_CO_EMP, strP_CO_TP_CO_COL;

            //--------> Inicializa as variáveis
            strP_FLA_PROFESSOR = null;
            strP_CO_SEXO_COL = null;
            strP_tp_def = null;
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_TP_CO_COL = int.Parse(ddlDescOpRelatorio.SelectedValue);
            strP_FLA_PROFESSOR = ddlCategoria.SelectedValue;
            strP_CO_SEXO_COL = ddlSexo.SelectedValue;
            strP_tp_def = ddlDeficiencia.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio += "( " + ddlOpcoesRelatorio.SelectedItem + ": " + ddlDescOpRelatorio.SelectedItem;
            strParametrosRelatorio += " - Categoria: " + ddlCategoria.SelectedItem;
            strParametrosRelatorio += " - Sexo: " + ddlSexo.SelectedItem;
            strParametrosRelatorio += " - Deficiência: " + ddlDeficiencia.SelectedItem + " )";

            RptDemoTpContrato fpcb = new RptDemoTpContrato();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_TP_CO_COL, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def, strINFOS, false, strP_CO_EMP_UNID_CONT, NomeFuncionalidadeCadastrada);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamendo DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaDropDown()
        {
            lblDescOpRelatorio.Text = "Unidade/Escola";

            ddlDescOpRelatorio.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                             select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct();

            ddlDescOpRelatorio.DataTextField = "NO_FANTAS_EMP";
            ddlDescOpRelatorio.DataValueField = "CO_EMP";
            ddlDescOpRelatorio.DataBind();

            ddlDescOpRelatorio.Width = 226;

            ddlDescOpRelatorio.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlDescOpRelatorio.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        //====> Método que carrega o dropdown ddlDescOpRelatorio de acordo com o tipo (ddlOpcoesRelatorio)
        protected void ddlOpcoesRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDescOpRelatorio.Items.Clear();

            if (ddlOpcoesRelatorio.SelectedValue == "U")
            {
                lblDescOpRelatorio.Text = "Unidade/Escola";

                ddlDescOpRelatorio.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                 select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct();

                ddlDescOpRelatorio.DataTextField = "NO_FANTAS_EMP";
                ddlDescOpRelatorio.DataValueField = "CO_EMP";
                ddlDescOpRelatorio.DataBind();
                ddlDescOpRelatorio.Width = 226;

                ddlDescOpRelatorio.SelectedValue = LoginAuxili.CO_EMP.ToString();
            }
            else if (ddlOpcoesRelatorio.SelectedValue == "D")
            {
                lblDescOpRelatorio.Text = "Departamento";

                ddlDescOpRelatorio.DataSource = TB14_DEPTO.RetornaTodosRegistros();

                ddlDescOpRelatorio.DataTextField = "NO_DEPTO";
                ddlDescOpRelatorio.DataValueField = "CO_DEPTO";
                ddlDescOpRelatorio.DataBind();
                ddlDescOpRelatorio.Width = 225;
            }
            else if (ddlOpcoesRelatorio.SelectedValue == "F")
            {
                lblDescOpRelatorio.Text = "Função";

                ddlDescOpRelatorio.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

                ddlDescOpRelatorio.DataTextField = "NO_FUN";
                ddlDescOpRelatorio.DataValueField = "CO_FUN";
                ddlDescOpRelatorio.DataBind();
                ddlDescOpRelatorio.Width = 170;
            }
            else if (ddlOpcoesRelatorio.SelectedValue == "C")
            {
                lblDescOpRelatorio.Text = "Tipo de Contrato";

                ddlDescOpRelatorio.DataSource = TB20_TIPOCON.RetornaTodosRegistros();

                ddlDescOpRelatorio.DataTextField = "NO_TPCON";
                ddlDescOpRelatorio.DataValueField = "CO_TPCON";
                ddlDescOpRelatorio.DataBind();
                ddlDescOpRelatorio.Width = 120;
            }

            ddlDescOpRelatorio.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades De contrato
        /// </summary>
        private void CarregaUnidadesLotacaoContrato()
        {

            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);

        }
    }
}
