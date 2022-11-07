//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: SAÚDE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE ITENS DE ESTOQUE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 06/03/14 | Vinícius Reis              | Adaptação da tela de cadastro de itens 
//          |                            | no estoque 

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

namespace C2BR.GestorEducacao.UI.GSAUD._4000_ControleOperacionalEstoque._4100_ControleOperacionalMateriais._4110_ControleOperacionalItensEstoque._4111_CadastroItemEstoque
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
            {
                CarregaGrupo();
                CarregaTipoProduto();
                ddlSubGrupo.Items.Insert(0, new ListItem("Todos", "-1"));
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_PROD" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_PROD_RED",
                HeaderText = "Produto"
            });

            BoundField bfRef = new BoundField();
            bfRef.DataField = "CO_REFE_PROD";
            bfRef.HeaderText = "Referência";
            bfRef.ItemStyle.CssClass = "codCol";
            bfRef.DataFormatString = "{0:N0}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRef);

            BoundField bfVl = new BoundField();
            bfVl.DataField = "VL_UNIT_PROD";
            bfVl.HeaderText = "Valor";
            bfVl.ItemStyle.CssClass = "numeroCol";
            bfVl.DataFormatString = "{0:N2}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfVl);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coTipProd = ddlTipoProduto.SelectedValue != "" ? int.Parse(ddlTipoProduto.SelectedValue) : 0;

            var resultado = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                             where ((txtDescricao.Text != "" ? tb90.NO_PROD.Contains(txtDescricao.Text) : txtDescricao.Text == ""))
                             && ((txtCodRef.Text != "" ? tb90.CO_REFE_PROD.Contains(txtCodRef.Text) : txtCodRef.Text == ""))
                             && (coTipProd != 0 ? tb90.TB124_TIPO_PRODUTO.CO_TIP_PROD == coTipProd : coTipProd == 0)
                             select new { tb90.NO_PROD_RED, tb90.CO_REFE_PROD, tb90.VL_UNIT_PROD, tb90.CO_PROD }).OrderBy(p => p.NO_PROD_RED);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_PROD"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Grupos
        private void CarregaGrupo()
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                   where tb260.TP_GRUPO.Equals("E")
                                   select new { tb260.ID_GRUPO, tb260.NOM_GRUPO });

            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }

        //====> Método que carrega o DropDown de SubGrupos
        private void CarregaSubGrupo()
        {
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubGrupo.DataSource = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                                      where tb261.TB260_GRUPO.ID_GRUPO == idGrupo
                                      select new { tb261.ID_SUBGRUPO, tb261.NOM_SUBGRUPO });

            ddlSubGrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubGrupo.DataTextField = "NOM_SUBGRUPO";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }

        //====> Método que carrega o DropDown de Tipos de Produto
        private void CarregaTipoProduto()
        {
            ddlTipoProduto.DataSource = (from tb124 in TB124_TIPO_PRODUTO.RetornaTodosRegistros()
                                         select new { tb124.DE_TIP_PROD, tb124.CO_TIP_PROD });

            ddlTipoProduto.DataTextField = "DE_TIP_PROD";
            ddlTipoProduto.DataValueField = "CO_TIP_PROD";
            ddlTipoProduto.DataBind();

            ddlTipoProduto.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }
    }
}