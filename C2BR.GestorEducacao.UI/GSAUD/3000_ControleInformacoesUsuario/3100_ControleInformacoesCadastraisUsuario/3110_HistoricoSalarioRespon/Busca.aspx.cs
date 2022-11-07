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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3110_HistoricoSalarioRespon
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

        void Page_Load()
        {
            if (IsPostBack) return;

            CarregaUnidades();
            CarregaAssociados();           
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_HISTO_SALAR_RESPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_RESP",
                HeaderText = "Associado"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "ANO_MES",
                HeaderText = "Referência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VL_PRINC_REND",
                HeaderText = "Rendimento"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idUnidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int idAssociado = ddlAssociado.SelectedValue != "" ? int.Parse(ddlAssociado.SelectedValue) : 0;

            var resultado = (from tbg151 in TBG151_HISTO_SALAR_RESPO.RetornaTodosRegistros()
                             where (idUnidade != 0 ? tbg151.TB25_EMPRESA.CO_EMP == idUnidade : true)
                             && (idAssociado != 0 ? tbg151.TB108_RESPONSAVEL.CO_RESP == idAssociado : true)
                             && (txtAnoMes.Text!= "" ? tbg151.ANO_MES == txtAnoMes.Text.Replace("/","") : true)             
                             join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tbg151.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP                           
                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbg151.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP                            
                             select new { tbg151.ID_HISTO_SALAR_RESPO, tb25.sigla,  tb108.NO_RESP, ANO_MES = tbg151.ANO_MES.Insert(4,"/"), tbg151.VL_PRINC_REND });         

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_HISTO_SALAR_RESPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Unidades de Patrimonio
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataBind();
        }

        //====> Método que carrega o DropDown de Colaboradores
        private void CarregaAssociados()
        {
            ddlAssociado.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                       select new { tb108.CO_RESP, tb108.NO_RESP }); ;
            ddlAssociado.DataTextField = "NO_RESP";
            ddlAssociado.DataValueField = "CO_RESP";
            ddlAssociado.DataBind();

            ddlAssociado.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}