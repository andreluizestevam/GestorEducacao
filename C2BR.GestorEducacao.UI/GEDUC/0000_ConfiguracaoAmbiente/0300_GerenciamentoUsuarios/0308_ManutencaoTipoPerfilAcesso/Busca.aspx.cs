//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE TIPO DE PERFIL DE ACESSO.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0308_ManutencaoTipoPerfilAcesso
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
                CarregaUnidade();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "idePerfilAcesso" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "nomeTipoPerfilAcesso",
                HeaderText = "Perfil", 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "statusTipoPerfilAcesso",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidadeTPA.SelectedValue != "" ? int.Parse(ddlUnidadeTPA.SelectedValue) : 0;

            var resultado = (from admPerfilAcesso in AdmPerfilAcesso.RetornaTodosRegistros()
                             where admPerfilAcesso.TB25_EMPRESA.CO_EMP == coEmp
                                && (ddlStatusPerfilTPA.SelectedValue.Equals("-1") ? ddlStatusPerfilTPA.SelectedValue == "-1" : admPerfilAcesso.statusTipoPerfilAcesso == ddlStatusPerfilTPA.SelectedValue)
                                && (txtNomePerfilTPA.Text == "" ? txtNomePerfilTPA.Text == "" : admPerfilAcesso.nomeTipoPerfilAcesso.Contains(txtNomePerfilTPA.Text))
                                && admPerfilAcesso.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                && admPerfilAcesso.idePerfilAcesso != 32
                                select new
                                {
                                    admPerfilAcesso.TB25_EMPRESA.NO_FANTAS_EMP, admPerfilAcesso.TB000_INSTITUICAO.ORG_NOME_ORGAO, admPerfilAcesso.ORG_CODIGO_ORGAO,
                                    admPerfilAcesso.nomeTipoPerfilAcesso, admPerfilAcesso.idePerfilAcesso,
                                    statusTipoPerfilAcesso = (admPerfilAcesso.statusTipoPerfilAcesso == "A" ? "Ativo" : (admPerfilAcesso.statusTipoPerfilAcesso == "I" ? "Inativo" : "Suspenso")),
                                }).OrderBy(c => c.nomeTipoPerfilAcesso);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "idePerfilAcesso"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        
        #endregion    
    
        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades
        private void CarregaUnidade()
        {
            ddlUnidadeTPA.DataSource = (from tb25 in TB25_EMPRESA.RetornaPeloIDeAdmUser(LoginAuxili.IDEADMUSUARIO)
                                        where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidadeTPA.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeTPA.DataValueField = "CO_EMP";
            ddlUnidadeTPA.DataBind();

            ddlUnidadeTPA.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion
    }
}
