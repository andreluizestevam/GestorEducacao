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
// 03/03/14 | Débora Lohane              | Criação da funcionalidade para busca de Subareas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1103_CadastroSubArea
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CarregaRegiao();

        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_SUBAREA" };


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_REGIAO",
                HeaderText = "Região"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_AREA",
                HeaderText = "Área"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SUBAREA",
                HeaderText = "Subárea"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SIGLA",
                HeaderText = "Sigla Subárea"
            });
        }
        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {

            int regiao = ddlRegiao.SelectedValue != "" ? int.Parse(ddlRegiao.SelectedValue) : 0;
            int area = ddlArea.SelectedValue != "" ? int.Parse(ddlArea.SelectedValue) : 0;

            var resultado = (from tb908 in TB908_SUBAREA.RetornaTodosRegistros()
                             where (txtSubarea.Text != "" ? tb908.NM_SUBAREA.Contains(txtSubarea.Text) : txtSubarea.Text == "")
                             && (txtSigla.Text != "" ? tb908.SIGLA.Contains(txtSigla.Text) : txtSigla.Text == "")
                              && (regiao != 0 ? tb908.TB907_AREA.TB906_REGIAO.ID_REGIAO == regiao : regiao == 0)
                              && (area != 0 ? tb908.TB907_AREA.ID_AREA == area : area == 0) 
                             
                             select new
                             {
                                 tb908.TB907_AREA.TB906_REGIAO.ID_REGIAO,
                                 tb908.TB907_AREA.TB906_REGIAO.NM_REGIAO,
                                 tb908.TB907_AREA.NM_AREA,
                                 tb908.ID_SUBAREA,
                                 tb908.NM_SUBAREA,
                                 tb908.SIGLA
                                 
                             }).OrderBy(sb => sb.ID_REGIAO).ThenBy(sb => sb.NM_AREA).ThenBy(sb => sb.NM_SUBAREA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }


        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_SUBAREA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown
        
//====> Método que carrega o DropDown de região
        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros().OrderBy( r => r.ID_REGIAO );

            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();

            ddlRegiao.Items.Insert(0, new ListItem("", ""));
        }

//====> Método que carrega o DropDown de Area
        private void CarregaArea()
        {
            int idRegiao = int.Parse(ddlRegiao.SelectedValue);
            ddlArea.DataSource = (from tb906 in TB907_AREA.RetornaPelaRegiao(idRegiao)
                                  select new { tb906.NM_AREA, tb906.ID_AREA });

            ddlArea.DataTextField = "NM_AREA";
            ddlArea.DataValueField = "ID_AREA";
            ddlArea.DataBind();
        }        
        #endregion

        protected void ddlRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaArea();
        }
       

    }
}