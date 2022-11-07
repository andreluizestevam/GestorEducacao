using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlFuncao
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

            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_FUNCAO_SIMPL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_FUNCAO",
                HeaderText = "NOME",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_SITUA",
                HeaderText = "SITUACÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {


            var resultado = (from tb366 in TBS366_FUNCAO_SIMPL.RetornaTodosRegistros()
                             where (txtNomeOper.Text != "" ? tb366.NM_FUNCAO.Contains(txtNomeOper.Text) : "" == "")// txtNomeOper.Text == "T")
                             && (ddlSituacao.SelectedValue != "" ? tb366.FL_SITUA.Equals(ddlSituacao.SelectedValue) : "" == "")
                             select new
                             {
                                 ID_FUNCAO_SIMPL = tb366.ID_FUNCAO_SIMPL,
                                 NM_FUNCAO = tb366.NM_FUNCAO,
                                 FL_SITUA = tb366.FL_SITUA == "A" ? "Ativo" : tb366.FL_SITUA == "I" ? "Inativo" : tb366.FL_SITUA == "S" ? "Suspenso" : " - ",
                             }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_FUNCAO_SIMPL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

    }
}