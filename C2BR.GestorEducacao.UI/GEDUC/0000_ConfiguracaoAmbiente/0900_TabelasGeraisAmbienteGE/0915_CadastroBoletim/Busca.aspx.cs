//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRO DE BOLETIM
// DATA DE CRIAÇÃO: 02/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 02/05/2013| Victor Martins Machado     | Criação da funcionalidade
//           |                            | 

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

namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0200_ConfiguracaoModulos._0204_CadastroBoletim
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

        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_BOL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_BOL",
                HeaderText = "Boletim",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_STAT",
                HeaderText = "Status"
            });
        }
        
        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string nomBoletim = txtBoletim.Text;

            var resultado = (from b in ADMBOLETIM.RetornaTodosRegistros()
                             where b.NO_BOL.Contains(nomBoletim)
                             select new Boletim
                             {
                                 CO_BOL = b.CO_BOL,
                                 NO_BOL = b.NO_BOL,
                                 FL_STAT = b.FL_STAT == "A" ? "Ativo" : b.FL_STAT == "I" ? "Inativo" : ""
                             }).OrderBy(o => o.NO_BOL);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        public class Boletim
        {
            public int CO_BOL { get; set; }
            public string NO_BOL { get; set; }
            public string FL_STAT { get; set; }
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {             
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_BOL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}