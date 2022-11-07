using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Data.Objects.DataClasses;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class ManterEnderecoSN : System.Web.UI.UserControl
    {
        public int IdEndereco { get; set; }
        public int IdFk { get; set; }

        public EnumAuxili.FkEnderecoSN FkEndereco { get; set; }

        public EnumAuxili.TipoManutencao Manutencao { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MontaComboUf();
            }

        }

        private void MontaComboUf()
        {
            ddlUf.DataSource = TSN010_UF.RetornaTodosRegistros().ToList();
            ddlUf.DataTextField = "DE_SIGLA";
            ddlUf.DataValueField = "CO_UF";
            ddlUf.DataBind();

            ddlUf.Items.Add(new ListItem("Selecione", "0"));
            ddlUf.SelectedValue = "0";
        }

        private void MontaComboCidade(string uf)
        {
            if (ddlCidade.Items.Count > 0)
                ddlCidade.Items.Clear();

            ddlCidade.DataSource = null;
            ddlCidade.DataBind();

            ddlCidade.DataSource = TSN009_CIDADE.RetornaTodosRegistros();
            ddlCidade.DataTextField = "DE_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();
            ddlCidade.SelectedIndex = 0;

        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUf.SelectedItem != null && Convert.ToInt32(ddlUf.SelectedValue) > 0)
            {
                MontaComboCidade(ddlUf.SelectedItem.Text);
            }
        }

        private bool Valida()
        {
            bool ret = true;
            if (ddlUf.SelectedItem == null && Convert.ToInt32(ddlUf.SelectedValue) <= 0)
            {
                ret = false;
            }
            return ret;
        }


        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!Valida())
                return;

            var resul = Library.Auxiliares.Util.RetornaEndereco(txtCep.Text);
            if (resul.Resultado)
            {
                txtLogradouro.Text = resul.Logradouro;
                txtBairro.Text = resul.Bairro;
                ddlUf.SelectedItem.Text = resul.Uf;
                MontaComboCidade(resul.Uf);
                ddlCidade.SelectedItem.Text = resul.Cidade;

            }

        }
    }
}