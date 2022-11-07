using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1109_CtrlRestrAtend
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_RESTR_ATEND" };
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_RESTR",
                HeaderText = "Código"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_RESTR",
                HeaderText = "NOME",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_RESTR",
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

                var resultado = (from tbs379 in TBS379_RESTR_ATEND.RetornaTodosRegistros()
                                 where (txtCodigo.Text != "" ? tbs379.CO_RESTR.Contains(txtCodigo.Text) : txtCodigo.Text == "")
                                   && (txtNome.Text != "" ? tbs379.NM_RESTR.Contains(txtNome.Text) : txtNome.Text == "")
                                && (ddlSituacao.SelectedValue != "T" ? tbs379.CO_SITUA == ddlSituacao.SelectedValue : "" == "")
                                 select new
                                 {
                                     ID_RESTR_ATEND = tbs379.ID_RESTR_ATEND,
                                     CO_RESTR = tbs379.CO_RESTR,                                  
                                     NM_RESTR = tbs379.NM_RESTR,
                                     DE_RESTR = tbs379.DE_RESTR,
                                     STATUS = tbs379.CO_SITUA == "A" ? "Ativo" : tbs379.CO_SITUA == "I" ? "Inativo" : "Suspenso",

                                 }).OrderBy(e => e.NM_RESTR);



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
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_RESTR_ATEND"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        public class Feriado
        {

            public int ID_AGENDA_FERIADOS { get; set; }
            public DateTime DT_FERIA_RECEBE { get; set; }
            public string DT_FERIA
            {
                get
                {
                    return this.DT_FERIA_RECEBE.ToString("dd/MM/yyyy");


                }
            }
            public string NM_FERIA { get; set; }
            public string TP_FERIA { get; set; }
            public string STATUS { get; set; }


        }
    }
}