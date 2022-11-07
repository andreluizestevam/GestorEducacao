//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS - DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5500_CtrlFluxoCaixa.F5599_Relatorios
{
    public partial class FluxoCaixa : System.Web.UI.Page
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
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strParametrosRelatorio, strINFOS, tpRelatorio, strOrigemPgto;
            DateTime dtInicio, dtFim;
            int codEmp, codEmpRef;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            codEmp = LoginAuxili.CO_EMP;
            codEmpRef = int.Parse(ddlUnidade.SelectedValue);
            tpRelatorio = ddlTipoRelatorio.SelectedValue;
            strOrigemPgto = ddlOrigemPgto.SelectedValue;
            dtInicio = DateTime.ParseExact(txtDtInicio.Text, "dd/MM/yyyy", null);
            dtFim = DateTime.ParseExact(txtDtFim.Text, "dd/MM/yyyy", null);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            var emp = TB25_EMPRESA.RetornaPelaChavePrimaria(codEmpRef);
            if (!emp.DT_SALDO_INICIAL.HasValue || !emp.VL_SALDO_INICIAL.HasValue)
            {
                AuxiliPagina.EnvioMensagemErro(this, "É necessário cadastrar a data e o saldo inicial da unidade.");
                return;
            }

            if (dtInicio < emp.DT_SALDO_INICIAL.Value)
            {
                AuxiliPagina.EnvioMensagemErro(this, "O período inicial deve ser superior ao cadastrado no saldo inicial da unidade.");
                return;
            }

            string siglaUnid = emp.sigla;

            strParametrosRelatorio = "(Unidade: " + siglaUnid
                    + " - Período: " + dtInicio.ToString("dd/MM/yy") + " à " + dtFim.ToString("dd/MM/yy") + ")";

            if (tpRelatorio == "Analitico")
            {
                RptFluxoCaixaAnalitico rpt = new RptFluxoCaixaAnalitico();
                lRetorno = rpt.InitReport(strParametrosRelatorio, codEmp, codEmpRef, dtInicio, dtFim, strOrigemPgto, strINFOS);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                RptFluxoCaixaConsolidado rpt = new RptFluxoCaixaConsolidado();
                lRetorno = rpt.InitReport(strParametrosRelatorio, codEmp, codEmpRef, dtInicio, dtFim, strOrigemPgto, strINFOS);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        #endregion
    }
}
