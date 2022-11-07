//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: EMISSÃO DA RELAÇÃO DE CEPS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//17/10/2014| Maxwell Almeida            | Criação da funcionalidade para listagem de CID

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
using C2BR.GestorEducacao.Reports.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0999_Relatorios
{
    public partial class RelacCID : System.Web.UI.Page
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
                CarregaCidGeral();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno, cidGeral;

            //--------> Variáveis de parâmetro do Relatório
            string noCIDGeral, coOrdPor, noOrdPor, coSituacao, noSituacao, noCidGeral, infos, parametros;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coOrdPor = ddlOrdPor.SelectedValue;
            cidGeral = (!string.IsNullOrEmpty(ddlCidGeral.SelectedValue) ? int.Parse(ddlCidGeral.SelectedValue) : 0);
            coSituacao = ddlSituacao.SelectedValue;

            noOrdPor = ddlOrdPor.SelectedItem.Text;
            noSituacao = ddlSituacao.SelectedItem.Text;
            noCidGeral = ddlCidGeral.SelectedItem.Text;

            parametros = "( CID Geral: " + noCidGeral.ToUpper() + " - Situação: " + noSituacao + " - Relatório ordenado por " + noOrdPor + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptRelacCID rpt = new RptRelacCID();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coOrdPor, cidGeral, coSituacao);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidGeral()
        {
            AuxiliCarregamentos.CarregaCIDGeral(ddlCidGeral, true);
        }

        #endregion
    }
}
