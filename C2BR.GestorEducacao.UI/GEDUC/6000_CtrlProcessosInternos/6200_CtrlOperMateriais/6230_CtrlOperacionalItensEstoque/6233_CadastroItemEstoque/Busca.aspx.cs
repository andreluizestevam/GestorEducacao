//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE ITENS DE ESTOQUE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6233_CadastroItemEstoque
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
                CarregaSubGrupo();
                CarregaTipoProduto();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_PROD" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField { 
            DataField = "TIPO",
            HeaderText = "TIPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CATEGORIA",
                HeaderText = "CATEGORIA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_PROD",
                HeaderText = "Produto"
            });
                     
            BoundField bfRef = new BoundField();
            bfRef.DataField = "codRef";
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
            int ? coTipProd = ddlTipoProduto.SelectedValue  != ""? int.Parse(ddlTipoProduto.SelectedValue) : 0;
            int ? coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int ? coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            var resultado = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                             where ((txtDescricao.Text != "" ? tb90.NO_PROD.Contains(txtDescricao.Text) : txtDescricao.Text == ""))
                             && ((txtCodRef.Text != "" ? tb90.CO_REFE_PROD.Contains(txtCodRef.Text) : txtCodRef.Text == ""))
                             && (coTipProd != 0 ? tb90.TB124_TIPO_PRODUTO.CO_TIP_PROD == coTipProd : coTipProd == 0)
                             && (coGrupo != 0 ? tb90.TB260_GRUPO.ID_GRUPO == coGrupo : 0 == 0)
                             && (coSubGrupo != 0 ? tb90.TB261_SUBGRUPO.ID_SUBGRUPO == coSubGrupo : 0 == 0)
                             select new 
                             {
                                 TIPO = tb90.TB124_TIPO_PRODUTO == null ? "-" : tb90.TB124_TIPO_PRODUTO.DE_TIP_PROD,
                                 CATEGORIA = tb90.TB95_CATEGORIA == null && string.IsNullOrEmpty(tb90.TB95_CATEGORIA.DES_CATEG) ? "-" : tb90.TB95_CATEGORIA.DES_CATEG,
                                 tb90.NO_PROD, 
                                 codRef = !string.IsNullOrEmpty(tb90.CO_REFE_PROD) ? tb90.CO_REFE_PROD : "-", 
                                 tb90.VL_UNIT_PROD, 
                                 tb90.CO_PROD
                             }).OrderBy(p => p.NO_PROD);

            //foreach (var item in resultado)
            //{
            //    var Grupo = TB90_PRODUTO.RetornaPelaChavePrimaria(item.CO_PROD, LoginAuxili.CO_EMP);
            //    Grupo.TB260_GRUPOReference.Load();

            //    if (Grupo.TB260_GRUPO == null)
            //    {
            //        var tb90 = TB90_PRODUTO.RetornaPeloCoProd(item.CO_PROD);
            //        int? idGrupo = TB260_GRUPO.RetornaTodosRegistros().Where(x => x.NOM_GRUPO.Equals("DIVERSOS") && x.CO_GRUPO.Equals("DIVERSOS")).FirstOrDefault().ID_GRUPO;
            //        tb90.TB260_GRUPO = TB260_GRUPO.RetornaPelaChavePrimaria(idGrupo.Value);
            //        tb90.TB261_SUBGRUPO = TB261_SUBGRUPO.RetornaTodosRegistros().Where(x => x.TB260_GRUPO.ID_GRUPO == idGrupo.Value).FirstOrDefault();
            //        TB90_PRODUTO.SaveOrUpdate(tb90, true);
            //    }
            //}

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
