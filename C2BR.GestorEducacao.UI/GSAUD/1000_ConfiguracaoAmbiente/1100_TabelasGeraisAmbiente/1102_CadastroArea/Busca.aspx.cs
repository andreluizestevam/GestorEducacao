//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/03/14 | Débora Lohane              | Criação da funcionalidade para busca Areas
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
namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1102_CadastroArea
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
                CarregaRegiao();
        }
        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_AREA" };


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_REGIAO",
                HeaderText = "Região"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_AREA",
                HeaderText = "Área"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SIGLA",
                HeaderText = "Sigla Área"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int regiao = ddlRegiao.SelectedValue != "" ? int.Parse(ddlRegiao.SelectedValue) : 0;

            var resultado = (from tb907 in TB907_AREA.RetornaTodosRegistros()

                             where ((txtArea.Text) != "" ? tb907.NM_AREA.Contains(txtArea.Text) : txtArea.Text == "")
                             && ((txtSigla.Text) != "" ? tb907.SIGLA.Contains(txtSigla.Text) : txtSigla.Text == "")
                             && (regiao != 0 ? tb907.TB906_REGIAO.ID_REGIAO == regiao : regiao == 0)
                             select new
                             {
                                 tb907.NM_AREA,
                                 tb907.ID_AREA,
                                 tb907.SIGLA,
                                 tb907.TB906_REGIAO.ID_REGIAO,
                                 tb907.TB906_REGIAO.NM_REGIAO
                             }).OrderBy(a => a.ID_REGIAO).ThenBy(a => a.NM_AREA);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;


        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_AREA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de UFs
        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros().OrderBy(r => r.ID_REGIAO);

            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();

            ddlRegiao.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}