//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: RELAÇÃO DE COLABORADORES POR UNIDADE FUNCIONAL
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
    public partial class RelacFuncPorUnidade : System.Web.UI.Page
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
                CarregaUnidades();
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
            string strP_CO_SITU_COL, strINFOS;
            int strP_CO_EMP, strP_CO_EMP_COL;

//--------> Inicializa as variáveis
            strP_CO_SITU_COL = null;
            strINFOS = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_COL = int.Parse(ddlUnidade.SelectedValue);
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strP_CO_SITU_COL = ddlSituacao.SelectedValue;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            strParametrosRelatorio = "( Unidade Cadastro: " + ddlUnidade.SelectedItem.Text 
                + " - Unidade Lotação/Contrato: " + ddlUnidContrato.SelectedItem.Text + " - Situação: "
                + ddlSituacao.SelectedItem.Text + " - Agrupado Por: " + ddlAgrupadoPor.SelectedItem.Text;

            strParametrosRelatorio += (LoginAuxili.CO_TIPO_UNID == "PGS" ? " - Class Profissinal: " + ddlClassFunc.SelectedItem.Text + " )" : " )");
            string NoRelatorio = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/1000_CtrlAdminEscolar/1200_GestaoOperColaboradores/1299_Relatorios/RelacFuncPorUnidade.aspx");

            RptColaborPorUnidade rpt = new RptColaborPorUnidade();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP,strP_CO_EMP_UNID_CONT , strP_CO_EMP_COL, strP_CO_SITU_COL, strINFOS, ddlClassFunc.SelectedValue, NoRelatorio, ddlAgrupadoPor.SelectedValue);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "../../../../../GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Verifica o tipo de unidade logada e faz adequações
        /// </summary>
        private void VerificarTipoUnidade()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    liClassFunc.Visible = true;
                    break;
                case "PGE":
                default:
                    liClassFunc.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Carrega as classificações funcionais
        /// </summary>
        private void CarregaClassificacoesFuncionais()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFunc, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades De contrato
        /// </summary>
        private void CarregaUnidadesLotacaoContrato()
        {

            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true, true, false);

        }

        #endregion
    }
}
