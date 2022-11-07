//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
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
using C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6300_CtrlDisponibilidade;

namespace C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6300_ControleDisponibilidade._6399_Relatorios
{
    public partial class DemonDisponiItensEstoque : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaGrupo();
                CarregaSubGrupo();
                CarregaProdutos();
                //CarregaMarca();
                //CarregaUnidadeMedida();
            }
        }

        #endregion

        #region Carregamentos
        /// <summary>
        /// Método que carrega as informações de solicitações na grid grdSoli
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros;
            string infos;
            int lRetorno, coGrupo, CoSubGrupo, coUnid, coItem;

            coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            coItem = ddlProduto.SelectedValue != "" ? int.Parse(ddlProduto.SelectedValue) : 0;
            coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            CoSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            parametros = "( Unidade: " + ddlUnidade.SelectedItem.Text.ToUpper() + " - Grupo: "
                + ddlGrupo.SelectedItem.Text.ToUpper() + " - Subgrupo: " + ddlSubGrupo.SelectedItem.Text.ToUpper()
                + " - Item: " + ddlProduto.SelectedItem.Text + " - Período: " + IniPeri.Text + " à " + FimPeri.Text
                + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDemonDIsponiItensEstoque fpcb = new RptDemonDIsponiItensEstoque();
            lRetorno = fpcb.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, coGrupo, CoSubGrupo, coItem, IniPeri.Text, FimPeri.Text, ddlClassificacao.SelectedValue, ddlTipoOrdem.SelectedValue, (chkGraficos.Checked ? true : false), (chkRelatorio.Checked ? true : false));
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo
        /// </summary>
        private void CarregaGrupo()
        {
            AuxiliCarregamentos.CarregaGruposItens(ddlGrupo, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubGrupo()
        {
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposItens(ddlSubGrupo, idGrupo, true);
        }

        /// <summary>
        /// Carrega os Produtos Cadastrados
        /// </summary>
        private void CarregaProdutos()
        {
            AuxiliCarregamentos.CarregaProdutos(ddlProduto, LoginAuxili.CO_EMP, true);
        }

        #endregion

        #region Funções de campo

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AuxiliCarregamentos.CarregaProdutos(ddlProduto, (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0), true);
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }

        #endregion
    }
}