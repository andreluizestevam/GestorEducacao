﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: RELAÇÃO DOS COMPROMISSOS E ACORDOS/ADITIVOS DAS RECEITAS FIXAS EXTERNAS
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5499_Relatorios
{
    public partial class RelacaoReceitasFixas : System.Web.UI.Page
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

                if (ddlTpDocumento.SelectedValue == "0")
                {
                    liCNPJ.Visible = true;
                    liCPF.Visible = false;
                }
                else
                {
                    liCNPJ.Visible = false;
                    liCPF.Visible = true;
                }
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_IC_SIT_RECDES, strP_CO_CON_RECDES, strP_DT_INI, strP_DT_FIM, strP_CO_CPF_CNPJ_RECDES, strP_NO_CLI_RECDES;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strParametrosRelatorio = null;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelReceitasFixas");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_IC_SIT_RECDES = null; 
            strP_CO_CON_RECDES = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;
            strP_CO_CPF_CNPJ_RECDES = null;
            strP_NO_CLI_RECDES = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_IC_SIT_RECDES = ddlStaDocumento.SelectedValue;
            
            if (txtNumDoc.Text != "")
                strP_CO_CON_RECDES = txtNumDoc.Text; 
            else
                strP_CO_CON_RECDES = "T";
   
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;         
            
            if (txtNomeCliente.Text != "")
                strP_NO_CLI_RECDES = txtNomeCliente.Text;
            else
                strP_NO_CLI_RECDES = "T";

            if (ddlTpDocumento.SelectedValue.ToString() == "0")
            {
                if (txtCNPJ.Text != "")
                    strP_CO_CPF_CNPJ_RECDES = txtCNPJ.Text;
                else
                    strP_CO_CPF_CNPJ_RECDES = "T";
            }
            else
            {
                if (txtCNPJ.Text != "")
                    strP_CO_CPF_CNPJ_RECDES = txtCPF.Text;
                else
                    strP_CO_CPF_CNPJ_RECDES = "T";
            }

            strParametrosRelatorio = "( Período de: " + txtDataPeriodoIni.Text + " à " + txtDataPeriodoFim.Text + " - Documentos: " + ddlStaDocumento.SelectedItem.ToString() + " )";           

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelReceitasFixas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_RECDES, strP_CO_CON_RECDES, strP_DT_INI, strP_DT_FIM, strP_CO_CPF_CNPJ_RECDES, strP_NO_CLI_RECDES);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
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
        }
        #endregion

        protected void ddlTpDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTpDocumento.SelectedValue == "0")
            {
                liCNPJ.Visible = true;
                liCPF.Visible = false;
            }
            else
            {
                liCNPJ.Visible = false;
                liCPF.Visible = true;
            }
        }
    }
}
