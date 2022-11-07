//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0912_CadastroCEP
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CarregaUfs();
            CarregaTipoLogradouro();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                CurrentPadraoCadastros.CurrentEntity = RetornaEntidade();
                return;
            }
            if (Page.IsValid)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    int coCep = int.Parse(txtCep.Text.Replace("-", ""));

                    var ocoCEP = from iTb235 in TB235_CEP.RetornaTodosRegistros()
                            where iTb235.CO_CEP == coCep
                            select iTb235;

                    if (ocoCEP.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "CEP já cadastrado.");
                        return;
                    }
                }

                decimal dcmLatitude, dcmLongitude;
                if (decimal.TryParse(txtLatitude.Text, out dcmLatitude) &&
                    decimal.TryParse(txtLongitude.Text, out dcmLongitude))
                {
                    if (dcmLatitude > 360 || dcmLongitude > 360)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Coordenadas inválidas. Valor deve ser menor ou igual a 360º.");
                        return;
                    }
                }

                TB235_CEP tb235 = RetornaEntidade();

                if (tb235 == null)
                {
                    tb235 = new TB235_CEP();
                    tb235.CO_CEP = int.Parse(txtCep.Text.Replace("-", string.Empty));
                }

                tb235.TB240_TIPO_LOGRADOURO = TB240_TIPO_LOGRADOURO.RetornaPelaChavePrimaria(int.Parse(ddlTipoLogradouro.SelectedValue));
                tb235.NO_ENDER_CEP = txtEndereco.Text;
                tb235.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                tb235.NR_LATIT_CEP = txtLatitude.Text != "" ? decimal.Parse(txtLatitude.Text) : (decimal?)null;
                tb235.TP_LATIT_CEP = ddlTipoLatitude.SelectedValue;
                tb235.NR_LONGI_CEP = txtLongitude.Text != "" ? decimal.Parse(txtLongitude.Text) : (decimal?)null;
                tb235.TP_LONGI_CEP = ddlTipoLongitude.SelectedValue;

                CurrentPadraoCadastros.CurrentEntity = tb235;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB235_CEP tb235 = RetornaEntidade();

            if (tb235 != null)
            {
                tb235.TB905_BAIRROReference.Load();
                tb235.TB240_TIPO_LOGRADOUROReference.Load();

                txtCep.Text = string.Format("{0:00000000}", tb235.CO_CEP);
                ddlTipoLogradouro.SelectedValue = tb235.TB240_TIPO_LOGRADOURO.ID_TIPO_LOGRA.ToString();
                txtEndereco.Text = tb235.NO_ENDER_CEP;
                ddlUf.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                CarregaCidades();
                ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                CarregaBairros();
                ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                txtLatitude.Text = tb235.NR_LATIT_CEP.HasValue ? tb235.NR_LATIT_CEP.ToString() : "";
                ddlTipoLatitude.SelectedValue = tb235.TP_LATIT_CEP;
                txtLongitude.Text = tb235.NR_LONGI_CEP.HasValue ? tb235.NR_LONGI_CEP.ToString() : "";
                ddlTipoLongitude.SelectedValue = tb235.TP_LONGI_CEP;
            }          
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB235_CEP</returns>
        private TB235_CEP RetornaEntidade()
        {
            string strCep = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) ?? "";
            if (strCep != "")
                strCep = strCep.Replace("-", "");

            int coCep;
            int.TryParse(strCep, out coCep);

            return TB235_CEP.RetornaPelaChavePrimaria(coCep);
        }

        #endregion       

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Logradouro
        /// </summary>
        private void CarregaTipoLogradouro()
        {
            ddlTipoLogradouro.DataSource = (from tb240 in TB240_TIPO_LOGRADOURO.RetornaTodosRegistros()
                                            select new { tb240.ID_TIPO_LOGRA, tb240.DE_TIPO_LOGRA }).OrderBy(o => o.DE_TIPO_LOGRA);
            ddlTipoLogradouro.DataTextField = "DE_TIPO_LOGRA";
            ddlTipoLogradouro.DataValueField = "ID_TIPO_LOGRA";
            ddlTipoLogradouro.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUfs()
        {
            ddlUf.DataSource = TB74_UF.RetornaTodosRegistros();
            ddlUf.DataTextField = "CODUF";
            ddlUf.DataValueField = "CODUF";
            ddlUf.DataBind();

            ddlUf.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = (from tb904 in TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue)
                                      select new { tb904.CO_CIDADE, tb904.NO_CIDADE });

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            ddlBairro.DataSource = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                                    where tb905.CO_CIDADE == coCidade
                                    select new { tb905.CO_BAIRRO, tb905.NO_BAIRRO } ).OrderBy(r => r.NO_BAIRRO);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            ddlBairro.Items.Clear();
        }
        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }
    }
}