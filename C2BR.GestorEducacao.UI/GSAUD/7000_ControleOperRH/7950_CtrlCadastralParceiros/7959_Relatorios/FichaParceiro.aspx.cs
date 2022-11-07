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
// 25/02/2015| Maxwell Almeida           | Inserida regra para alterar o label professor para profissional de saúde
//          |                            | quando a unidade logada for de saúde

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
using C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7950_CtrlCadastralParceiros;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7950_CtrlCadastralParceiros._7959_Relatorios
{
    public partial class FichaParceiro : System.Web.UI.Page
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
                CarregaParceiros();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;
            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/7000_ControleOperRH/7950_CtrlCadastralParceiros/7959_Relatorios/FichaParceiro.aspx");

            //--------> Variáveis de parâmetro do Relatório
            int coEmp, strP_CO_PARCE;
            string strINFOS;

            //--------> Inicializa as variáveis
            strP_CO_PARCE = 0;
            coEmp = LoginAuxili.CO_EMP;
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_PARCE = int.Parse(ddlParceiro.SelectedValue);

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaParceiro rpt = new RptFichaParceiro();
            lRetorno = rpt.InitReport(coEmp, strP_CO_PARCE, strINFOS, NomeFuncionalidadeCadastrada.ToUpper());

            if (NomeFuncionalidadeCadastrada == "")
            {
                rpt.Titulo = "FICHA DE INFORMAÇÕES DO PARCEIRO DE NEGÓCIO";
            }
            else
            {
                rpt.Titulo = NomeFuncionalidadeCadastrada.ToUpper();
            }

            Session["Report"] = rpt;

            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaParceiros()
        {
            var lst = from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                      select new
                      {
                          Parceiro = tb421.DE_RAZSOC_PARCE,
                          tb421.CO_PARCE,
                      };

            ddlParceiro.DataSource = lst.OrderBy(c => c.Parceiro);

            ddlParceiro.DataTextField = "Parceiro";
            ddlParceiro.DataValueField = "CO_PARCE";
            ddlParceiro.DataBind();
            ddlParceiro.Items.Insert(0, new ListItem("Selecione", ""));

            ddlParceiro.Enabled = ddlParceiro.Items.Count > 0;
        }

        #endregion

    }
}
