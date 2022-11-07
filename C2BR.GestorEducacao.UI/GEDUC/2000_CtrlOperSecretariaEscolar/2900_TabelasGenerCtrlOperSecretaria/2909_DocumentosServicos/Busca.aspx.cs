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

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_SolicitacaoItensSecretaria.DocumentosServicos
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }


        #region Events
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
                //Altera os tipos para o caso de a empresa logada ser uma empresa de Saúde
                //if (TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).CO_FLAG_ENSIN_CURSO == "N")
                //{
                //    ddlTipoDoc.Items.Clear();
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Todos", "T"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Atestado", "AM"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Contrato", "CO"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Declaração", "DE"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Recibo", "RE"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Outros", "OT"));
                //}
            }
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {

            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_DOCUM" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_DOCUM",
                HeaderText = "Sigla do Documento"
            });           
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_DOCUM",
                HeaderText = "Tipo do Documento"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_DOCUM",
                HeaderText = "Nome do Documento"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUS_DOCUM",
                HeaderText = "Situação"
            });
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                             where tb009.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && (ddlStatus.SelectedValue != "" && ddlStatus.SelectedValue != "T" ? tb009.CO_SITUS_DOCUM == ddlStatus.SelectedValue.Trim() : 0==0)
                             && (ddlTipoDoc.SelectedValue != "" && ddlTipoDoc.SelectedValue != "T" ? tb009.TP_DOCUM == ddlTipoDoc.SelectedValue.Trim() : 0 == 0)
                             && (txtNomeDoc.Text != "" ? tb009.NM_DOCUM.Contains(txtNomeDoc.Text) : 0==0)
                             && (txtSiglaDoc.Text != "" ? tb009.CO_SIGLA_DOCUM.Contains(txtSiglaDoc.Text) : 0==0)
                             select new
                             {
                                 tb009.ID_DOCUM,
                                 TP_DOCUM = (tb009.TP_DOCUM == "AT" ? "ATA" : (tb009.TP_DOCUM == "CE" ? "Certidão" : (tb009.TP_DOCUM == "CO" ? "Contrato" : (tb009.TP_DOCUM == "CC" ? "Certificado" :
                                 (tb009.TP_DOCUM == "DE" ? "Declaração" : (tb009.TP_DOCUM == "HI" ? "Histórico" : (tb009.TP_DOCUM == "RE" ? "Recibo" : "Outros"))))))),
                                 tb009.CO_SIGLA_DOCUM, 
                                 tb009.NM_DOCUM,
                                 CO_SITUS_DOCUM = (tb009.CO_SITUS_DOCUM == "A" ? "Ativo" : "Inativo")
                             }).OrderByDescending(s => s.CO_SIGLA_DOCUM).ThenBy(s => s.CO_SIGLA_DOCUM);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_DOCUM"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion


        private string GetTipo(string CoTipo)
        {
            string str = "";
            switch (CoTipo)
            {
                case "AT": str = "Ata"; break;
                case "CE": str = "Certidão"; break;
                case "CO": str = "Contrato"; break;
                case "CC": str = "Certificado"; break;
                case "DE": str = "Declaração"; break;
                case "HI": str = "Histórico"; break;
                case "RE": str = "Recibo"; break;
                case "OT": str = "Outros"; break;
            }

            return str;
        }

       
    }
}