//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: RELAÇÃO DE COLABORADORES ANIVERSARIANTES
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
    public partial class FuncionariosAniversariantes : System.Web.UI.Page
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
                CarregaUnidadesCadastro();
            CarregaUnidadesLotacaoContrato();
            VerificarTipoUnidade();

        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("/GEDUC/1000_CtrlAdminEscolar/1200_GestaoOperColaboradores/1299_Relatorios/FuncionariosAniversariantes.aspx");
            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_COL, strP_MES ;
            string flaProf, strINFOS;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            int strP_CO_EMP_UNID_CONT = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            strP_CO_EMP_COL = int.Parse(ddlUnidade.SelectedValue);
            strP_MES = int.Parse(ddlMesReferencia.SelectedValue);
            flaProf = ddlTipoColaborador.SelectedValue != "0" ? ddlTipoColaborador.SelectedValue : "T";
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptColaborAniversariante fpcb = new RptColaborAniversariante();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_COL, flaProf, strP_MES, strINFOS, NomeFuncionalidadeCadastrada.ToUpper(), strP_CO_EMP_UNID_CONT);
           
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidadesCadastro()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);

        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades De contrato
        /// </summary>
        private void CarregaUnidadesLotacaoContrato()
        {

            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);

        }

        /// <summary>
        /// Verifica o tipo da unidade e altera informacoes especificas
        /// </summary>
        private void VerificarTipoUnidade()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    ddlTipoColaborador.Items.Clear();
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Professores", "P"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Todos", "0"));
                    break;
                case "PGS":
                    ddlTipoColaborador.Items.Clear();
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Profissionais Saúde", "S"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Todos", "0"));

                    break;

            }
        }
        #endregion
    }
}
