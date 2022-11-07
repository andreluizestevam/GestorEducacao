//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: LANÇAMENTO DE ENTRADA E SAÍDA DE ITENS DE ESTOQUE
// OBJETIVO: REGISTRO DE MOVIMENTAÇÃO DE ESTOQUE.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6250_LanctoEntradaSaidaItensEstoque.F6251_RegistroMovimentacaoEstoque
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
                CarregaProduto();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_MOV" };
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_PROD_RED",
                HeaderText = "Produto"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DOC_PROD",
                HeaderText = "N° Doc",
                DataFormatString = "{0:d}"
            });

            BoundField bfRef = new BoundField();
            bfRef.DataField = "DT_MOV_PROD";
            bfRef.HeaderText = "Data Movimentação";
            bfRef.ItemStyle.CssClass = "codCol";
            bfRef.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRef);

            BoundField bfRef1 = new BoundField();
            bfRef1.DataField = "QT_MOV_PROD";
            bfRef1.HeaderText = "Qtd Movimento";
            bfRef1.ItemStyle.CssClass = "codCol";
            bfRef1.DataFormatString = "{0:N0}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRef1);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coProd = ddlProduto.SelectedValue != "" ? int.Parse(ddlProduto.SelectedValue) : 0;
          
            var resultado = (from tb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros()
                            where (coProd != 0 ? tb91.TB90_PRODUTO.CO_PROD == coProd : coProd == 0)
                            && tb91.TB93_TIPO_MOVIMENTO.FLA_TP_MOV == ddlTipoMovimentacao.SelectedValue
                            select new 
                            {
                                tb91.TB93_TIPO_MOVIMENTO.FLA_TP_MOV, tb91.TB90_PRODUTO.NO_PROD_RED, tb91.TB90_PRODUTO.CO_PROD,
                                tb91.DT_MOV_PROD, tb91.CO_MOV, tb91.QT_MOV_PROD, tb91.NU_DOC_PROD 
                            }).OrderBy( m => m.NO_PROD_RED );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_MOV"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        protected void imgbPesqProduto_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(true);

            string produto = txtProduto.Text;

            ddlProduto.DataSource = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                                     where tb90.NO_PROD.Contains(produto) && tb90.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                     select new { tb90.CO_PROD, tb90.NO_PROD_RED }).OrderBy(p => p.NO_PROD_RED);

            ddlProduto.DataTextField = "NO_PROD_RED";
            ddlProduto.DataValueField = "CO_PROD";
            ddlProduto.DataBind();

            ddlProduto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtProduto.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlProduto.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Produtos
        private void CarregaProduto()
        {
            ddlProduto.DataSource = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                                     where tb90.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                     select new { tb90.CO_PROD, tb90.NO_PROD }).OrderBy( p => p.NO_PROD );

            ddlProduto.DataTextField = "NO_PROD";
            ddlProduto.DataValueField = "CO_PROD";
            ddlProduto.DataBind();

            ddlProduto.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
