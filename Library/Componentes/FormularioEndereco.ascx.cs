//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class FormEndereco : System.Web.UI.UserControl
    {
        #region Propriedades

        public TextBox TxtCep
        {
            get { return txtCEP; }
            set { txtCEP = value; }
        }

        public TextBox TxtLogradouro
        {
            get { return txtLogradouro; }
            set { txtLogradouro = value; }
        }

        public TextBox TxtNumero
        {
            get { return txtNumero; }
            set { txtNumero = value; }
        }

        public TextBox TxtComplemento
        {
            get { return txtComplemento; }
            set { txtComplemento = value; }
        }

        public DropDownList DdlBairro
        {
            get { return ddlBairro; }
            set { ddlBairro = value; }
        }

        public DropDownList DdlCidade
        {
            get { return ddlCidade; }
            set { ddlCidade = value; }
        }

        public DropDownList DdlUf
        {
            get { return ddlUF; }
            set { ddlUF = value; }
        }
        #endregion

        #region Eventos

        protected void Page_Load()
        {
            if (IsPostBack) return;

            txtCEP.Enabled =
            btnPesquisarCep.Enabled =            
            txtComplemento.Enabled =
            txtNumero.Enabled = !(QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoExclusao);
            CarregaUfs();
            CarregaCidades();
            CarregaBairros();
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Carrega o dropdown de UFs
        /// </summary>
        public void CarregaUfs()
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros().OrderBy(r => r.CODUF);
            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega o dropdown de cidades
        /// </summary>
        public void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue).OrderBy(r => r.NO_CIDADE);
            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();
            ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega o dropdown de bairros
        /// </summary>
        public void CarregaBairros()
        {
            if (ddlCidade.SelectedValue == "")
            {
                ddlBairro.Items.Clear();
                ddlBairro.Items.Insert(0, new ListItem("", ""));
                return;
            }
            else
            {
                int coCidade = int.Parse(ddlCidade.SelectedValue);

                ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade).OrderBy(r => r.NO_BAIRRO);
                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataBind();
                ddlBairro.Items.Insert(0, new ListItem("", ""));
            }
            
        }        
        #endregion

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        protected void btnPesquisarCep_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where( c => c.CO_CEP == numCep ).FirstOrDefault();

                if (tb235 != null)
                {
                    tb235.TB905_BAIRROReference.Load();
                    txtLogradouro.Text = tb235.NO_ENDER_CEP;
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouro.Text = ddlBairro.SelectedValue = ddlCidade.SelectedValue = ddlUF.SelectedValue = "";
                    AuxiliPagina.EnvioMensagemErro(this.Page, "CEP não encontrado");
                    return;
                }
            }
        }
    }
}