//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: ***
// OBJETIVO: ***
// DATA DE CRIAÇÃO: 04/06/2013
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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5800_CtrlDiversosFinanceiro._5810_CtrlCheques;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5819_Relatorios
{
    public partial class RelacaoChequeEmitidoInstituicao : System.Web.UI.Page
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

//====> Método que faz a chamada de outro método de acordo com o Tipo
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS, strTipoCliente, strSituacao;
            DateTime strDt_Ini, strDt_Fim;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, codCliente;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP_REF = LoginAuxili.CO_EMP;
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);

            strDt_Ini = DateTime.Parse(txtDataPeriodoIni.Text);
            strDt_Fim = DateTime.Parse(txtDataPeriodoFim.Text);            

            if (strDt_Fim < strDt_Ini)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data de FIM do período não pode ser menor que a data de INICIO.");
                return;
            }
            strSituacao = ddlSituacao.SelectedValue;
            strTipoCliente = ddlTpCliente.SelectedValue;
            codCliente = ddlTpCliente.SelectedValue != "T" ? int.Parse(ddlResponsavel.SelectedValue) : 0;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Tipo: " + ddlTpCliente.SelectedItem.Text
                + " - Período: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text + " )";

            RptRelacaoChequeEmitidoInstituicao fpcb = new RptRelacaoChequeEmitidoInstituicao();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strTipoCliente, codCliente, strDt_Ini, strDt_Fim, strSituacao, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }                    
        #endregion

        #region Métodos

        /// <summary>
        /// Método que controla a visibilidade dos campos
        /// </summary>
        private void ControlaVisibilidade()
        {
            if (ddlTpCliente.SelectedValue == "R")
            {
                ddlResponsavel.Enabled = true;

                ddlResponsavel.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                             select new { ID = tb108.CO_RESP, NOME = tb108.NO_RESP });

                ddlResponsavel.DataTextField = "NOME";
                ddlResponsavel.DataValueField = "ID";
                ddlResponsavel.DataBind();

                ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else if (ddlTpCliente.SelectedValue == "C")
            {
                ddlResponsavel.Enabled = true;

                ddlResponsavel.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                             select new { ID = tb103.CO_CLIENTE, NOME = tb103.NO_FAN_CLI });

                ddlResponsavel.DataTextField = "NOME";
                ddlResponsavel.DataValueField = "ID";
                ddlResponsavel.DataBind();

                ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlResponsavel.Items.Clear();
                ddlResponsavel.Enabled = false;
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
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();            

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ControlaVisibilidade();
        }
        #endregion

        protected void ddlTpCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaVisibilidade();
        }
    }
}
