//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5901_FormaPagto
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TIPO_PAGA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_PAGA",
                HeaderText = "Forma de Pagamento"
            });

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "QT_DIAS_PARC";
            bfRealizado.HeaderText = "Dias Parc";
            bfRealizado.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
            

            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "NU_PARC_PAGA";
            bfRealizado1.HeaderText = "Nu Parc";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);
          
            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "PE_CORR_PARC";
            bfRealizado2.HeaderText = "% Correção";
            bfRealizado2.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb22 in TB22_TIPOPAG.RetornaTodosRegistros()
                             where (txtTipoPagam.Text != "" ? tb22.NO_TIPO_PAGA.Contains(txtTipoPagam.Text) : txtTipoPagam.Text == "")
                            select tb22).OrderBy(t => t.NO_TIPO_PAGA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_TIPO_PAGA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
