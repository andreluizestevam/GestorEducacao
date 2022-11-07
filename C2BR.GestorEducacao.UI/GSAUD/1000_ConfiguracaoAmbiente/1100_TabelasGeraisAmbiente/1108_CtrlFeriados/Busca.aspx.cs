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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1108_CtrlFeriados
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_AGENDA_FERIADOS" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_FERIA",
                HeaderText = "DATA FERIADO",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_FERIA",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_FERIA",
                HeaderText = "TIPO FERIADO"
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
                DateTime? Data = txtData.Text == "" ? null : Data = Convert.ToDateTime(txtData.Text);
                var resultado = (from tb376 in TBS376_AGENDA_FERIADOS.RetornaTodosRegistros()
                                 where Data != null ? tb376.DT_FERIA == Data : "" == ""
                                 && (txtNomeferiado.Text != "" ? tb376.NM_FERIA.Equals(txtNomeferiado.Text) : txtNomeferiado.Text == "")
                                 && (ddlTipoFeriado.SelectedValue != "0" ? tb376.TP_FERIA == ddlTipoFeriado.SelectedValue : 0 == 0)
                                 && (ddlSituacao.SelectedValue != "T" ? tb376.CO_SITUA == ddlSituacao.SelectedValue : "" == "")
                                 select new Feriado
                                 {
                                     ID_AGENDA_FERIADOS = tb376.ID_AGENDA_FERIADOS,
                                     DT_FERIA_RECEBE = tb376.DT_FERIA,
                                     NM_FERIA =  tb376.NM_FERIA,
                                     TP_FERIA = tb376.TP_FERIA == "N" ? "Feriado Nacional" : tb376.TP_FERIA == "E" ? "Feriado Estadual" : "Feriado Municipal",
                                     STATUS = tb376.CO_SITUA == "A" ? "Ativo" : tb376.CO_SITUA == "I" ? "Inativo" : "Suspenso",

                                 }).OrderBy(e => e.DT_FERIA_RECEBE);
               


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
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_AGENDA_FERIADOS"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        public class Feriado {

            public int ID_AGENDA_FERIADOS { get; set; }
            public DateTime DT_FERIA_RECEBE { get; set; }
            public string  DT_FERIA {
                get {
                    return this.DT_FERIA_RECEBE.ToString("dd/MM/yyyy");
                
                         
                }
            }
            public string NM_FERIA { get; set; }
            public string TP_FERIA { get; set; }
            public string STATUS { get; set; }
        
        
        }
    }


}