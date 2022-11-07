//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PS - Portal SAÚDE
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL FUNCIONARIOS DE RH
// SUBMÓDULO: SAÚDE - Cadastro Gen[erico
// OBJETIVO: RELAÇÃO DE USUÁRIOS POR CIDADE/BAIRRO
// DATA DE CRIAÇÃO:12/03/2014 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 12/03/2014| Valeska Medeiros de Sousa  | Criação do Relatório de Usuários por Cidade/Bairro

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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios
{
    public partial class RelacaoUsuarioCidadeBairro : System.Web.UI.Page
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
                CarregaCidades();
                CarregaBairros();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            int lRetorno;
            string infos, parametros, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_UF = ddlUF.SelectedValue;
            strP_CO_CIDADE = ddlCidade.SelectedValue;
            strP_CO_BAIRRO = ddlBairro.SelectedValue;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "(Unidade de Contrato: " + ddlUnidade.SelectedItem.ToString() +
                         " - UF: " + ddlUF.SelectedItem.ToString() +
                         " - Cidade: " + ddlCidade.SelectedItem.ToString() +
                         " - Bairro: " + ddlBairro.SelectedItem.ToString() ;
            
            rptRelacaoUsuarioCidadeBairro rpt = new rptRelacaoUsuarioCidadeBairro();
            lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e UFs
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "T" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;

                if (ddlCidade.Items.Count > 1)
                    ddlBairro.Items.Insert(0, new ListItem("Todos", "T"));
                else
                    ddlBairro.Items.Clear();

                return;
            }

            ddlBairro.Enabled = true;

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Items.Insert(0, new ListItem("Todos", "T"));
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
    }
}