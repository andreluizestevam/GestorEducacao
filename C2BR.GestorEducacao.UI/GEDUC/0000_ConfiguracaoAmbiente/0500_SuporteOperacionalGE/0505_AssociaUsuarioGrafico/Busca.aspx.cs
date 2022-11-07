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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0505_AssociaUsuarioGrafico
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
            CarregaUsuarios();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_GRAFI_USUAR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Usuário"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_TITULO_GRAFI",
                HeaderText = "Título"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FLA_STATUS",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idUsuario = ddlUsuario.SelectedValue != "T" ? int.Parse(ddlUsuario.SelectedValue) : 0;

            var resultado = (from tb308 in TB308_GRAFI_USUAR.RetornaTodosRegistros()
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb308.ADMUSUARIO.CodUsuario equals tb03.CO_COL
                             where idUsuario != 0 ? tb308.ADMUSUARIO.ideAdmUsuario == idUsuario : idUsuario == 0
                               select new 
                               {
                                   tb308.ID_GRAFI_USUAR, tb03.NO_COL, tb308.TB307_GRAFI_GERAL.NM_TITULO_GRAFI,
                                   FLA_STATUS = tb308.FLA_STATUS == "A" ? "Ativa" : "Inativa"
                               }).OrderBy(b => b.NO_COL).ThenBy(b => b.NM_TITULO_GRAFI);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_GRAFI_USUAR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Métodos

        private void CarregaUsuarios()
        {
            ddlUsuario.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                     join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                     select new { tb03.NO_COL, admUsuario.ideAdmUsuario }).OrderBy(a => a.NO_COL);

            ddlUsuario.DataTextField = "NO_COL";
            ddlUsuario.DataValueField = "ideAdmUsuario";
            ddlUsuario.DataBind();

            ddlUsuario.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion
    }
}