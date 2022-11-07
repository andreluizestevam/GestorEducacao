﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: INFORMAÇÕES DE CALENDÁRIO ESCOLAR
// OBJETIVO: MANUTENÇÃO DE TIPO DE CALENDARIO ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoTipoCalendarioEscolar
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CAT_ID_TIPO_CALEN" };
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAT_NOME_TIPO_CALEN",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAT_SIGLA_TIPO_CALEN",
                HeaderText = "Sigla"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAT_SITUA_TIPO_CALEN",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb152 in TB152_CALENDARIO_TIPO.RetornaTodosRegistros()
                            where ((txtDescricao.Text != "" ? tb152.CAT_NOME_TIPO_CALEN.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                            || (txtDescricao.Text != "" ? tb152.CAT_SIGLA_TIPO_CALEN.Contains(txtDescricao.Text) : txtDescricao.Text == ""))
                            && tb152.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new 
                            {
                                tb152.CAT_NOME_TIPO_CALEN, tb152.CAT_SIGLA_TIPO_CALEN, tb152.CAT_ID_TIPO_CALEN,
                                CAT_SITUA_TIPO_CALEN = tb152.CAT_SITUA_TIPO_CALEN == "A" ? "Ativo" : "Inativo"                                
                            }).OrderBy( c => c.CAT_NOME_TIPO_CALEN );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CAT_ID_TIPO_CALEN"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}