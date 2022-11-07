//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: TIPO DE ENDEREÇO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0908_TipoEndereco
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_TIPO_ENDERECO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { 
                DataField = "CO_TIPO_ENDERECO", 
                HeaderText = "Código" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { 
                DataField = "NM_TIPO_ENDERECO", 
                HeaderText = "Nome" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { DataField = "CO_SITUACAO", 
                HeaderText = "Situação" 
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {
            var resultado = (from tb238 in TB238_TIPO_ENDERECO.RetornaTodosRegistros()
                            where tb238.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && txtNomeTE.Text != "" ? tb238.NM_TIPO_ENDERECO.Contains(txtNomeTE.Text) : txtNomeTE.Text == ""
                            && ddlSituacaoTE.SelectedValue != "T" ? tb238.CO_SITUACAO == ddlSituacaoTE.SelectedValue : ddlSituacaoTE.SelectedValue == "T"
                            select new 
                            {
                                tb238.ID_TIPO_ENDERECO, tb238.CO_TIPO_ENDERECO, tb238.NM_TIPO_ENDERECO,
                                CO_SITUACAO = tb238.CO_SITUACAO.ToUpper().Equals("A") ? "Ativo" : "Inativo"                             
                            }).OrderBy( t => t.NM_TIPO_ENDERECO );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds() 
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_TIPO_ENDERECO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}