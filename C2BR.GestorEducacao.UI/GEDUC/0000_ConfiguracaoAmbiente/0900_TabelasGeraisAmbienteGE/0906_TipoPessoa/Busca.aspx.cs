//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: TIPO DE PESSOA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0906_TipoPessoa
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_TIPO_PESSOA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { 
                DataField = "CO_TIPO_PESSOA", 
                HeaderText = "Código" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { 
                DataField = "NM_TIPO_PESSOA", 
                HeaderText = "Nome" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { 
                DataField = "CO_SITUACAO", 
                HeaderText = "Situação" 
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {

            var resultado = (from tb237 in TB237_TIPO_PESSOA.RetornaTodosRegistros()
                            where tb237.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO 
                            && txtNomeTP.Text != "" ? tb237.NM_TIPO_PESSOA.Contains(txtNomeTP.Text) : txtNomeTP.Text == ""
                            && ddlSituacaoTP.SelectedValue != "" ? tb237.CO_SITUACAO == ddlSituacaoTP.SelectedValue : ddlSituacaoTP.SelectedValue == ""
                            select new 
                            {
                                tb237.ID_TIPO_PESSOA, tb237.CO_TIPO_PESSOA, tb237.NM_TIPO_PESSOA,
                                CO_SITUACAO = tb237.CO_SITUACAO.ToUpper().Equals("A") ? "Ativo" : "Inativo"                                
                            }).OrderBy( t => t.NM_TIPO_PESSOA );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds() 
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_TIPO_PESSOA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}