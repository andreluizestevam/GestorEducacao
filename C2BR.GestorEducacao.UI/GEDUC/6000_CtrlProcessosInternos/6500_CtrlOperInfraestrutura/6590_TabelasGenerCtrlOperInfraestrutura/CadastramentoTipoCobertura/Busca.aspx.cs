﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: INFRAESTRUTURA  DE ENGENHARIA
// SUBMÓDULO: TABELAS DE APOIO - MANUTENÇÃO DE INFRA
// OBJETIVO:  CADASTRAMENTO DO TIPO DE COBERTURA.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.CadastramentoTipoCobertura
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TIPO_COBER" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_TIPO_COBER",
                HeaderText = "Tipo Cobertura"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_TIPO_COBER",
                HeaderText = "Sigla"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb181 in TB181_TIPO_COBER.RetornaTodosRegistros()
                             where (txtDescricao.Text != "" ? tb181.DE_TIPO_COBER.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                             || (txtDescricao.Text != "" ? tb181.CO_SIGLA_TIPO_COBER.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                             select tb181);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_TIPO_COBER"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}