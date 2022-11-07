
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


namespace C2BR.GestorEducacao.UI.GEDUC._7000_ControleOperRH._7950_CtrlCadastralParceiros._7951_CadastroParceiros
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

        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CurrentPadraoBuscas.GridBusca.Columns[0].HeaderStyle.Width = new Unit(100, UnitType.Pixel);
                CurrentPadraoBuscas.GridBusca.Columns[0].ItemStyle.Width = new Unit(100, UnitType.Pixel);
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_PARCE" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_PARCE",
                HeaderText = "Nome"

            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CPFCGC_PARCE",
                HeaderText = "Código",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TEL1_PARCE",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_WATHS_PARCE",
                HeaderText = "Whats App"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CIDADE",
                HeaderText = "Cidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CODUF",
                HeaderText = "UF"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb41 in TB421_PARCEIROS.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb41.NO_FANTAS_PARCE.Contains(txtNome.Text) : txtNome.Text == "" &&
                                    ddlAreaProspeccao.SelectedValue != "" ? tb41.CO_AREA_PROSP_NEGOC.Contains(ddlAreaProspeccao.SelectedValue) : ddlAreaProspeccao.SelectedValue == "" ||
                                    txtNome.Text != "" ? tb41.NO_FANTAS_PARCE.Contains(txtNome.Text) : txtNome.Text == "" ||
                                    ddlAreaProspeccao.SelectedValue != "" ? tb41.CO_AREA_PROSP_NEGOC.Contains(ddlAreaProspeccao.SelectedValue) : ddlAreaProspeccao.SelectedValue == "")
                             select new
                             {
                                 tb41.CO_PARCE,
                                 tb41.NO_FANTAS_PARCE,
                                 tb41.TB74_UF.CODUF,
                                 tb41.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                 tb41.CO_WATHS_PARCE,
                                 CO_CPFCGC_PARCE = (tb41.TP_PARCE == "F" && tb41.CO_CPFCGC_PARCE.Length >= 11) ? tb41.CO_CPFCGC_PARCE.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb41.TP_PARCE == "J" && tb41.CO_CPFCGC_PARCE.Length >= 14) ? tb41.CO_CPFCGC_PARCE.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb41.CO_CPFCGC_PARCE),
                                 CO_TEL1_PARCE = tb41.CO_TEL1_PARCE.Insert(0, "(").Insert(3, ") ").Insert(9, "-"),
                                 CO_FAX_PARCE = tb41.CO_FAX_PARCE.Insert(0, "(").Insert(3, ") ").Insert(9, "-")
                             }).OrderBy(f => f.NO_FANTAS_PARCE);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_PARCE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }


        #endregion
    }
}