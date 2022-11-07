//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: RELAÇÃO DE COLABORADORES PARAMETRIZADA
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
    public partial class RelacaoFuncionariosParamet : System.Web.UI.Page
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
                VerificarTipoUnidade();
                CarregaDropDown();
                CarregaCidades();
                CarregaBairros();
                CarregaUnidadesLotacaoContrato();
                CarregaClassificacoesFuncionais();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_FLA_PROFESSOR, strP_TP_DEF, strP_CO_SEXO_COL, strP_CO_UF, strINFOS;
            int strP_CO_EMP, strP_CO_FUN, strP_CO_INST, strP_CO_CIDADE, strP_CO_BAIRRO;

            //--------> Inicializa as variáveis
            strP_FLA_PROFESSOR = null;
            strP_TP_DEF = null;
            strP_CO_SEXO_COL = null;
            strP_CO_UF = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_FLA_PROFESSOR = ddlCategoria.SelectedValue != "0" ? ddlCategoria.SelectedValue : "T";
            strP_CO_FUN = int.Parse(ddlFuncao.SelectedValue);
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strP_CO_INST = int.Parse(ddlGrauInstrucao.SelectedValue);
            strP_TP_DEF = ddlDeficiencia.SelectedValue;
            strP_CO_SEXO_COL = ddlSexo.SelectedValue;
            strP_CO_UF = ddlUF.SelectedValue;
            strP_CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
            strP_CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/1000_CtrlAdminEscolar/1200_GestaoOperColaboradores/1299_Relatorios/RelacaoFuncionariosParamet.aspx");


            strParametrosRelatorio = "( Unid: " + ddlUnidade.SelectedItem + " - Unid Lotação: " + ddlUnidContrato.SelectedItem.Text + " - Categ: " + ddlCategoria.SelectedItem +
            " - Função: " + ddlFuncao.SelectedItem + " - Grau de Instrução: " + ddlGrauInstrucao.SelectedItem + " - Def: " + ddlDeficiencia.SelectedItem +
            " - Sx: " + ddlSexo.SelectedItem + " - UF: " + ddlUF.SelectedItem + " - Cidade: " + ddlCidade.SelectedItem + " - Bairro: " + ddlBairro.SelectedItem.Text;

            strParametrosRelatorio += (LoginAuxili.CO_TIPO_UNID == "PGS" ? " - Class Profi: " + ddlClassFunc.SelectedItem.Text + " )" : " )");

            RptColaborParametrizada rpt = new RptColaborParametrizada();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_FUN, strP_CO_INST, strP_TP_DEF, strP_CO_SEXO_COL, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strINFOS, strP_CO_EMP_UNID_CONT, NomeFuncionalidadeCadastrada, ddlClassFunc.SelectedValue);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "0" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;

                ddlBairro.Items.Insert(0, new ListItem("Todos", "0"));

                return;
            }

            ddlBairro.Enabled = true;

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares, Funcoes, Grau de Instrucao e UFs
        /// </summary>
        private void CarregaDropDown()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);

            ddlFuncao.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

            ddlFuncao.DataTextField = "NO_FUN";
            ddlFuncao.DataValueField = "CO_FUN";
            ddlFuncao.DataBind();

            ddlFuncao.Items.Insert(0, new ListItem("Todos", "0"));

            ddlGrauInstrucao.DataSource = TB18_GRAUINS.RetornaTodosRegistros();

            ddlGrauInstrucao.DataTextField = "NO_INST";
            ddlGrauInstrucao.DataValueField = "CO_INST";
            ddlGrauInstrucao.DataBind();

            ddlGrauInstrucao.Items.Insert(0, new ListItem("Todos", "0"));

            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("Todos", "T"));
            ddlUF.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO.ToString();
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

        /// <summary>
        /// Método que carrega o dropdown de Unidades De contrato
        /// </summary>
        private void CarregaUnidadesLotacaoContrato()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true, true, false);
        }

        /// <summary>
        /// Carrega as classificações funcionais
        /// </summary>
        private void CarregaClassificacoesFuncionais()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFunc, true);
        }

        /// <summary>
        /// Verifica o tipo da unidade e altera informacoes especificas
        /// </summary>
        private void VerificarTipoUnidade()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    ddlCategoria.Items.Clear();
                    ddlCategoria.Items.Insert(0, new ListItem("Professores", "P"));
                    ddlCategoria.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlCategoria.Items.Insert(0, new ListItem("Todos", "0"));
                    liClassFunc.Visible = false;
                    break;
                case "PGS":
                    ddlCategoria.Items.Clear();
                    ddlCategoria.Items.Insert(0, new ListItem("Profissionais Saúde", "S"));
                    ddlCategoria.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlCategoria.Items.Insert(0, new ListItem("Todos", "0"));
                    liClassFunc.Visible = true;
                    break;

            }
        }
    }
}
