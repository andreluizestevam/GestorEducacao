//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: CADASTRAMENTO DOS TIPOS DE SOLICITAÇÃO DE SERVIÇOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2901_TiposSolicitacaoServicos
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas {get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnPreInit(e);           

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);

            if (!IsPostBack)
                CarregaGrupoItemSolicitacao();
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TIPO_SOLI" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_GRUPO_SOLIC",
                HeaderText = "GRUPO",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_SOLI",
                HeaderText = "DESCRIÇÃO",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_TPSOLIC",
                HeaderText = "SIGLA",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_ATUALI_FINAN_TPSOLIC",
                HeaderText = "FIN",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_ITEM_MATRIC_TPSOLIC",
                HeaderText = "MAT",
            });

        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int grp = ddlGrupoTipo.SelectedValue != "" ? int.Parse(ddlGrupoTipo.SelectedValue):0;

            var resultado = (from tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros()                             
                            where   (tb66.TB000_INSTITUICAO == null || tb66.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO)
                           && (txtTipoSolicitacao.Text != "" ? tb66.NO_TIPO_SOLI.Contains(txtTipoSolicitacao.Text) : 0 == 0                          
                            && ddlGrupoTipo.SelectedValue != "" ? tb66.TB061_GRUPO_SOLIC.ID_GRUPO_SOLIC == grp  : 0 == 0)
                             select new { 
                                    tb66.CO_TIPO_SOLI,
                                    tb66.NO_TIPO_SOLI, 
                                    tb66.CO_SIGLA_TPSOLIC,
                                    tb66.TB061_GRUPO_SOLIC.NM_GRUPO_SOLIC, 
                                    FL_ITEM_MATRIC_TPSOLIC =  tb66.FL_ITEM_MATRIC_TPSOLIC == "S"? "SIM": "NÃO",
                                    FL_ATUALI_FINAN_TPSOLIC = tb66.FL_ATUALI_FINAN_TPSOLIC == "S" ? "SIM" : "NÃO"
                             }).OrderBy(t => new {t.NM_GRUPO_SOLIC, t.NO_TIPO_SOLI}).ToList();
                                            
            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_TIPO_SOLI"));
                    
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Grupo Item Solicitação 
        /// </summary>
        private void CarregaGrupoItemSolicitacao()
        {
            var res = (from tb061 in TB061_GRUPO_SOLIC.RetornaTodosRegistros()
                       where tb061.CO_SITUA_GRUPO_SOLIC == "A"
                       select new
                       {
                           tb061.ID_GRUPO_SOLIC,
                           tb061.NM_GRUPO_SOLIC
                       }).OrderBy(x => x.NM_GRUPO_SOLIC).ToList();
            ddlGrupoTipo.DataSource = res;


            ddlGrupoTipo.DataTextField = "NM_GRUPO_SOLIC";
            ddlGrupoTipo.DataValueField = "ID_GRUPO_SOLIC";
            ddlGrupoTipo.DataBind();

            ddlGrupoTipo.Items.Insert(0, new ListItem("Selecione", ""));
        }

    }
}