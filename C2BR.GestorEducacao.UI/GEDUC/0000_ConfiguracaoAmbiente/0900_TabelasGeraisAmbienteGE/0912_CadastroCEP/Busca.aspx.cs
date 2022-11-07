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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0912_CadastroCEP
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
            if (!IsPostBack)
                CarregaUF();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_CEP" };

            BoundField bfCep = new BoundField();
            bfCep.DataField = "CO_CEP";
            bfCep.HeaderText = "CEP";
            bfCep.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfCep);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_TIPO_LOGRA",
                HeaderText = "Tipo Logr."
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ENDER_CEP",
                HeaderText = "Logradouro"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_UF",
                HeaderText = "UF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CIDADE",
                HeaderText = "Cidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_BAIRRO",
                HeaderText = "Bairro"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "COORDENADA",
                HeaderText = "Coordenada"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            int coBairro = ddlBairro.SelectedValue != "" ? int.Parse(ddlBairro.SelectedValue) : 0;

            var resultado = (from tb235 in TB235_CEP.RetornaTodosRegistros().Include(typeof(TB240_TIPO_LOGRADOURO).Name).Include("TB905_BAIRRO.TB904_CIDADE").AsEnumerable()
                                select new
                                {
                                    CO_CEP = string.Format("{0:00000-000}", tb235.CO_CEP), tb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA, tb235.NO_ENDER_CEP,
                                    tb235.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE, tb235.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                    tb235.TB905_BAIRRO.CO_BAIRRO, tb235.TB905_BAIRRO.NO_BAIRRO, tb235.TB905_BAIRRO.TB904_CIDADE.CO_UF,
                                    COORDENADA = tb235.NR_LATIT_CEP.HasValue && tb235.NR_LONGI_CEP.HasValue ? string.Format("{0}º{1}{2}º{3}", 
                                    tb235.NR_LATIT_CEP, tb235.TP_LATIT_CEP, tb235.NR_LONGI_CEP, tb235.TP_LONGI_CEP) : ""
                                }).ToList();

            if (ddlUf.SelectedValue != "")
                resultado = resultado.Where(w => w.CO_UF == ddlUf.SelectedValue).ToList();

            if (coCidade != 0)
                resultado = resultado.Where(w => w.CO_CIDADE == coCidade).ToList();

            if (coBairro != 0)
                resultado = resultado.Where(w => w.CO_BAIRRO == coBairro).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_CEP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de UFs
        private void CarregaUF()
        {
            ddlUf.DataSource = TB74_UF.RetornaTodosRegistros().OrderBy(u => u.CODUF);

            ddlUf.DataTextField = "CODUF";
            ddlUf.DataValueField = "CODUF";
            ddlUf.DataBind();

            ddlUf.Items.Insert(0, new ListItem("", ""));
        }

//====> Método que carrega o DropDown de Cidades
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue).OrderBy(c => c.NO_CIDADE);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

//====> Método que carrega o DropDown de Bairros
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