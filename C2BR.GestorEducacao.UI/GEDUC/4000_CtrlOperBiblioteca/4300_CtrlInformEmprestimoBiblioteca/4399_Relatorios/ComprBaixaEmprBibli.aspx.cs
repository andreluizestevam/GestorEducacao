﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: REGISTRO E BAIXA DE EMPRÉSTIMOS
// OBJETIVO: COMPROVANTE DE BAIXA DE EMPRÉSTIMO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4399_Relatorios
{
    public partial class ComprBaixaEmprBibli : System.Web.UI.Page
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
                CarregaEmprestimos();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_NUM_EMP;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprBaixaEmprBibli");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_NUM_EMP = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_NUM_EMP = ddlEmprestimo.SelectedValue;

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelComprBaixaEmprBibli(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUM_EMP, LoginAuxili.ORG_NUMERO_CNPJ.ToString());

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }
        #endregion

        #region Carregamento DropDown

//====> 
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

        /// <summary>
        /// Método que carrega o dropdown de Empréstimos
        /// </summary>
        private void CarregaEmprestimos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            var resultado = (from tb36 in TB36_EMPR_BIBLIOT.RetornaTodosRegistros()
                             where tb36.TB25_EMPRESA.CO_EMP == coEmp
                             select new { tb36.TB25_EMPRESA.CO_EMP, tb36.DT_EMPR_BIBLIOT, tb36.CO_NUM_EMP }).ToList();

            ddlEmprestimo.DataSource = (from result in resultado
                                        select new
                                        {
                                            result.CO_NUM_EMP,
                                            NUM_EMPRESTIMO = "Nº " + result.CO_EMP.ToString("0000") + "-" + result.DT_EMPR_BIBLIOT.ToString("yy") + "/" +
                                            result.DT_EMPR_BIBLIOT.Month.ToString("00") + "." + result.CO_NUM_EMP.ToString("000000")
                                        }).OrderBy( r => r.CO_NUM_EMP );

            ddlEmprestimo.DataTextField = "NUM_EMPRESTIMO";
            ddlEmprestimo.DataValueField = "CO_NUM_EMP";
            ddlEmprestimo.DataBind();
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaEmprestimos();
        }
    }
}
