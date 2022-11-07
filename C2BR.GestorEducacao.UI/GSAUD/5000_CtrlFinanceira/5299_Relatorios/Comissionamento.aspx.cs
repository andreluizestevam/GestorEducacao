//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: HISTÓRICO FINANCEIRO DE ALUNOS - SÉRIE/TURMA
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
using C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução GSAUD.5000_CtrlFinanceira.5299_Relatorios
namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios
{
    public partial class Comissionamento : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarUnidades();
                CarregarProfissionais();
                CarregaGrupos();
                CarregaSubGrupos();
                CarregarProcedimentosMedicos();
                CarregarTiposComissao();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string infos, titulo, parametros;

            int unidade = int.Parse(ddlUnidade.SelectedValue);
            int profissional = int.Parse(ddlProfissional.SelectedValue);
            int grupo = int.Parse(ddlGrupo.SelectedValue);
            int subgrupo = int.Parse(ddlSubGrupo.SelectedValue);
            int procedimento = int.Parse(ddlProcedimento.SelectedValue);
            string tipo = ddlTipo.SelectedValue;
            string situacao = ddlSituacao.SelectedValue;

            var siglaUnidade = ddlUnidade.SelectedValue == "0" ? "TODAS" : TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).sigla;

            parametros = "( Unidade : " + siglaUnidade.ToUpper()
                + " - Profissional : " + ddlProfissional.SelectedItem.Text.ToUpper()
                + " - Grupo : " + ddlGrupo.SelectedItem.Text.ToUpper()
                + " - SubGrupo : " + ddlSubGrupo.SelectedItem.Text.ToUpper()
                + " - Procedimento : " + (ddlProcedimento.SelectedValue == "0" ? "TODOS" : ddlProcedimento.SelectedItem.Text.Substring(1, 8))
                + " - Tipo : " + ddlTipo.SelectedItem.Text.ToUpper()
                + " - Situação : " + ddlSituacao.SelectedItem.Text.ToUpper() + " )";

            titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/5000_CtrlFinanceira/5299_Relatorios/Comissionamento.aspx");
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptComissionamento rpt = new RptComissionamento();
            var lRetorno = rpt.InitReport(titulo, parametros, infos, LoginAuxili.CO_EMP, unidade, profissional, grupo, subgrupo, procedimento, situacao, tipo);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        private void CarregarUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregarProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, true, "0", true, 0, false);
        }

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo, true);
        }

        /// <summary>
        /// Carrega so SubGrupos
        /// </summary>
        private void CarregaSubGrupos()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, coGrupo, true);
        }

        /// <summary>
        /// Carrega os procedimentos de acordo com grupo e subgrupo;
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarProcedimentosMedicos()
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProcedimento, ddlGrupo, ddlSubGrupo, 0, true, true, null, true, false);
        }

        private void CarregarTiposComissao()
        {
            AuxiliCarregamentos.CarregaTiposComissao(ddlTipo, true);
        }

        #endregion

        #region Eventos de componentes da pagina

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
            CarregarProcedimentosMedicos();
        }

        protected void ddlSubGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarProcedimentosMedicos();
        }

        #endregion
    }
}
