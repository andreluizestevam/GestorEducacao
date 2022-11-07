//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 05/03/14 | Débora Lohane              | Criação da funcionalidade para geração de relatorios(regiao, area, subarea)

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
using C2BR.GestorEducacao.Reports.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1199_Relatorios
{
    public partial class RelacaoRegiaoAreaSubarea : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            CarregaRegiao();

        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            
            //Variáveis do relatório
            int retorno;
            int regiao, area, subarea, coEmp;
            string ordenacao, infosRodape, parametros;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário

            regiao = int.Parse(ddlRegiao.SelectedValue);
            area = int.Parse(ddlArea.SelectedValue);
            subarea = int.Parse(ddlSubarea.SelectedValue);
            coEmp = LoginAuxili.CO_EMP;
            ordenacao = ddlOrdenacao.SelectedValue;
            infosRodape = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = ("(Região: "+ ddlRegiao.SelectedItem.ToString() + " Área: "+ddlArea.SelectedItem.ToString()+" Subárea: "+ddlSubarea.SelectedItem.ToString()+ " Ordenado por: " +ddlOrdenacao.SelectedItem.ToString()+")" );

            RptRelacaoRelacaoRegiaoAreaSubarea rpt = new RptRelacaoRelacaoRegiaoAreaSubarea();
            retorno = rpt.InitReport(regiao, area, subarea, ordenacao, infosRodape, parametros, coEmp);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(retorno, this.AppRelativeVirtualPath);
            

        }

        #endregion

        #region Carregamento DropDown

        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros().OrderBy(r => r.ID_REGIAO);

            ddlRegiao.Items.Clear();

            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();

            ddlRegiao.Items.Insert(0, new ListItem("Todas", "0"));
            ddlRegiao.SelectedValue = "0";
        }

        private void CarregaArea()
        {
            int idRegiao = (ddlRegiao.SelectedValue) != "0" ? int.Parse(ddlRegiao.SelectedValue) : 0;

            ddlArea.Items.Clear();

            ddlArea.DataSource = (from tb906 in TB907_AREA.RetornaPelaRegiao(idRegiao)
                                  select new { tb906.NM_AREA, tb906.ID_AREA });

            ddlArea.DataTextField = "NM_AREA";
            ddlArea.DataValueField = "ID_AREA";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("Todas", "0"));
            ddlArea.SelectedValue = "0";
        }

        private void CarregaSubarea()
        {
            int idArea = (ddlArea.SelectedValue) != "0" ? int.Parse(ddlArea.SelectedValue) : 0;

            ddlSubarea.Items.Clear();

            ddlSubarea.DataSource = (from tb908 in TB908_SUBAREA.RetornaPelaArea(idArea)
                                     select new { tb908.NM_SUBAREA, tb908.ID_SUBAREA });

            ddlSubarea.DataTextField = "NM_SUBAREA";
            ddlSubarea.DataValueField = "ID_SUBAREA";
            ddlSubarea.DataBind();

            ddlSubarea.Items.Insert(0, new ListItem("Todas", "0"));
            ddlSubarea.SelectedValue = "0";
        }
        #endregion
        protected void ddlRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaArea();
            CarregaSubarea();
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubarea();
        }

    }
}