using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2900_TabelasGenerCtrlOperSecretaria._2910_GrupoItemSolicitacao
{
    public partial class Busca : System.Web.UI.Page
    {

        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_GRUPO_SOLIC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_GRUPO_SOLIC",
                HeaderText = "CÓDIGO",                 
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_GRUPO_SOLIC",
                HeaderText = "NOME",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUA_GRUPO_SOLIC",
                HeaderText = "SITUAÇÃO",
            });
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            // int grp = ddlGrupoTipo.SelectedValue != "" ? int.Parse(ddlGrupoTipo.SelectedValue) : 0;

            var resultado = (from tb061 in TB061_GRUPO_SOLIC.RetornaTodosRegistros()
                             where txtTipoSolicitacao.Text != "" ? tb061.NM_GRUPO_SOLIC == txtTipoSolicitacao.Text : 0 == 0
                             && ddlSituacao.SelectedValue != "" ? tb061.CO_SITUA_GRUPO_SOLIC == ddlSituacao.SelectedValue : 0 == 0
                             select new
                             {
                                 tb061.ID_GRUPO_SOLIC,
                                 tb061.NM_GRUPO_SOLIC,
                                 tb061.CO_GRUPO_SOLIC,
                                 CO_SITUA_GRUPO_SOLIC = tb061.CO_SITUA_GRUPO_SOLIC == "A" ? "Ativo" : "Inativo"
                             }).OrderBy(t => new { t.NM_GRUPO_SOLIC }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_GRUPO_SOLIC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion


    }
}