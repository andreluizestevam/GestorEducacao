using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1110_CtrlInfosSaude
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_INFOS_GERAIS" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_INFOS_GERAIS",
                HeaderText = "NOME",
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SIGLA_INFOS_GERAIS",
                HeaderText = "SIGLA"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_INFOS_GERAIS",
                HeaderText = "Descrição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "STATUS",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {


                var resultado = (from tbs382 in TBS382_INFOS_GERAIS.RetornaTodosRegistros()
                                 where (txtSigla.Text != "" ? tbs382.NM_SIGLA_INFOS_GERAIS.Contains(txtSigla.Text) : txtSigla.Text == "")
                                && (txtNome.Text != "" ? tbs382.NM_INFOS_GERAIS.Contains(txtNome.Text) : txtNome.Text == "")
                                 && (txtDescricao.Text != "" ? tbs382.DE_INFOS_GERAIS.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                                 && (ddlSituacao.SelectedValue != "T" ? tbs382.CO_SITUA == ddlSituacao.SelectedValue : "" == "")
                                 select new
                                 {
                                     ID_INFOS_GERAIS = tbs382.ID_INFOS_GERAIS,
                                     NM_INFOS_GERAIS = tbs382.NM_INFOS_GERAIS,
                                     NM_SIGLA_INFOS_GERAIS = tbs382.NM_SIGLA_INFOS_GERAIS,
                                     DE_INFOS_GERAIS = tbs382.DE_INFOS_GERAIS,
                                     STATUS = tbs382.CO_SITUA == "A" ? "Ativo" : tbs382.CO_SITUA == "I" ? "Inativo" : "Suspenso",

                                 }).OrderBy(e => e.NM_INFOS_GERAIS);



                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_INFOS_GERAIS"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}