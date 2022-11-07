//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: TABELAS DE APOIO - ESTOQUE
// OBJETIVO: CADASTRAMENTO DE SUBGRUPOS ITENS
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6294_CadastroSubGrupoItens
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
            if (!IsPostBack)
                CarregaGrupos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_SUBGRP_ITEM" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_GRUPO_ITEM",
                HeaderText = "Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SUBGRP_ITEM",
                HeaderText = "SubGrupo"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_ALT_REGISTRO";
            bf1.HeaderText = "Dt Alter";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            var resultado = (from tb88 in TB88_SUBGRUPO_ITENS.RetornaTodosRegistros()
                             where (txtSubGrupo.Text != "" ? tb88.NO_SUBGRP_ITEM.Contains(txtSubGrupo.Text) : txtSubGrupo.Text == "")
                             && (coGrupo != 0 ? tb88.TB87_GRUPO_ITENS.CO_GRUPO_ITEM == coGrupo : coGrupo == 0)
                             select new
                             {
                                 tb88.NO_SUBGRP_ITEM, tb88.TB87_GRUPO_ITENS.NO_GRUPO_ITEM, tb88.CO_SUBGRP_ITEM, tb88.DT_ALT_REGISTRO
                             }).OrderBy(m => m.NO_GRUPO_ITEM).ThenBy(m => m.NO_SUBGRP_ITEM);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SUBGRP_ITEM"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o dropdown de Grupos
        private void CarregaGrupos()
        {
            ddlGrupo.DataSource = (from tb87 in TB87_GRUPO_ITENS.RetornaTodosRegistros()
                                   select new { tb87.CO_GRUPO_ITEM, tb87.NO_GRUPO_ITEM }).OrderBy(a => a.NO_GRUPO_ITEM);

            ddlGrupo.DataValueField = "CO_GRUPO_ITEM";
            ddlGrupo.DataTextField = "NO_GRUPO_ITEM";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
