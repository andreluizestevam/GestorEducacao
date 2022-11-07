//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: CONTROLE DE CONTRATOS DE COMPROMISSOS
// OBJETIVO: CADASTRAMENTO DE TIPOS DE SUBCATEGORIAS DE CONTRATOS DE COMPROMISSOS INSTITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1502_CadastroSubCategoria
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
                CarregaCategorias();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_SUB_CATEG_CONTR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_CATEG_CONTR",
                HeaderText = "Categoria"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SUB_CATEG_CONTR",
                HeaderText = "Código"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SUB_CATEG_CONTR",
                HeaderText = "Descrição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUACAO",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idCateg = ddlCateg.SelectedValue != "" ? int.Parse(ddlCateg.SelectedValue) : 0;

            var resultado = (from tb314 in TB314_SUB_CATEG_CONTR.RetornaTodosRegistros()
                             where (txtSubCateg.Text != "" ? tb314.NM_SUB_CATEG_CONTR.Contains(txtSubCateg.Text) : txtSubCateg.Text == "")
                             && (idCateg != 0 ? tb314.TB313_CATEG_CONTR.ID_CATEG_CONTR == idCateg : idCateg == 0)
                             && tb314.TB313_CATEG_CONTR.CO_SITUACAO == "A"
                             select new
                             {
                                 tb314.NM_SUB_CATEG_CONTR, tb314.TB313_CATEG_CONTR.NM_CATEG_CONTR, tb314.ID_SUB_CATEG_CONTR,
                                 tb314.CO_SUB_CATEG_CONTR, CO_SITUACAO = tb314.CO_SITUACAO == "A" ? "Ativa" : "Inativa"
                             }).OrderBy(m => m.NM_CATEG_CONTR).ThenBy(m => m.NM_SUB_CATEG_CONTR).ThenBy(m => m.CO_SITUACAO);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_SUB_CATEG_CONTR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o dropdown de Categorias
        private void CarregaCategorias()
        {
            ddlCateg.DataSource = (from tb313 in TB313_CATEG_CONTR.RetornaTodosRegistros()
                                   where tb313.CO_SITUACAO == "A"
                                   select new { tb313.ID_CATEG_CONTR, tb313.NM_CATEG_CONTR }).OrderBy(a => a.NM_CATEG_CONTR);

            ddlCateg.DataValueField = "ID_CATEG_CONTR";
            ddlCateg.DataTextField = "NM_CATEG_CONTR";
            ddlCateg.DataBind();

            ddlCateg.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
