//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *******************
// SUBMÓDULO: ****************
// OBJETIVO: *****************
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
using System.Data.Objects;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1903_CursosFormacaoEspecializacao
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

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ESPEC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_ESPEC",
                HeaderText = "Descrição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SIGLA_ESPEC",
                HeaderText = "Sigla"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_ESPEC",
                HeaderText = "Tipo"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            DynamicWhereClauseHelper dnmWhereClauseHelpers = new DynamicWhereClauseHelper();

            ObjectQuery<TB100_ESPECIALIZACAO> tb100 = TB100_ESPECIALIZACAO.RetornaTodosRegistros();

            if (rblTipo.SelectedValue == "0" || rblTipo.SelectedValue ==  "")
            {
                foreach (ListItem lstItem in rblTipo.Items)
                    dnmWhereClauseHelpers.AddOrEqualsParameter("TP_ESPEC", lstItem.Value);
            }
            else
            {
                foreach (ListItem lstItem in rblTipo.Items)
                    if (lstItem.Selected)
                        dnmWhereClauseHelpers.AddOrEqualsParameter("TP_ESPEC", lstItem.Value);
            }

            if (txtNO_SIGLA_ESPEC.Text != "")
                dnmWhereClauseHelpers.AddAndEqualsParameter("NO_SIGLA_ESPEC", txtNO_SIGLA_ESPEC.Text);

            if (txtDE_ESPEC.Text != "")
                dnmWhereClauseHelpers.AddAndEqualsParameter("DE_ESPEC", txtDE_ESPEC.Text);

            CurrentPadraoBuscas.GridBusca.DataSource = (tb100.Where(dnmWhereClauseHelpers).Count() > 0) ? (tb100.Where(dnmWhereClauseHelpers)).OrderBy(p => p.DE_ESPEC) : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ESPEC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}