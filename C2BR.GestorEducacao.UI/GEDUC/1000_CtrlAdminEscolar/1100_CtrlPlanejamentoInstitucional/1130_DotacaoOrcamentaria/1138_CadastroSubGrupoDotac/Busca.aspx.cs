//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *****
// SUBMÓDULO: *****
// OBJETIVO: CADASTRAMENTO DE SUBGRUPOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1138_CadastroSubGrupoDotac
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_SUBGRUPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SUBGRUPO",
                HeaderText = "CÓDIGO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOM_SUBGRUPO",
                HeaderText = "DESCRICAO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_GRUPO",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOM_GRUPO",
                HeaderText = "GRUPO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            var resultado = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                             where (txtSubGrupo.Text != "" ? tb261.NOM_SUBGRUPO.Contains(txtSubGrupo.Text) : txtSubGrupo.Text == "")
                             && (coGrupo != 0 ? tb261.TB260_GRUPO.ID_GRUPO == coGrupo : coGrupo == 0)
                             && tb261.TB260_GRUPO.TP_GRUPO == "D"
                             select new
                             {
                                 tb261.NOM_SUBGRUPO, tb261.TB260_GRUPO.NOM_GRUPO, tb261.ID_SUBGRUPO, tb261.CO_SUBGRUPO,
                                 TP_GRUPO = "Dotação Orçamentária"
                             }).OrderBy(m => m.NOM_GRUPO).ThenBy(m => m.NOM_SUBGRUPO);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_SUBGRUPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o dropdown de Grupos
        private void CarregaGrupos()
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                   where tb260.TP_GRUPO == "D"
                                   select new { tb260.ID_GRUPO, tb260.NOM_GRUPO }).OrderBy(a => a.NOM_GRUPO);

            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
