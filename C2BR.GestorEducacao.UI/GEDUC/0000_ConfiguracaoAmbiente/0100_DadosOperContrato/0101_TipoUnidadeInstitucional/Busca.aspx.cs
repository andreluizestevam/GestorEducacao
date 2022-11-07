﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: DADOS OPERACIONAIS DE CONTRATO
// OBJETIVO: TIPO DE UNIDADE INSTITUCIONAL.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0101_TipoUnidadeInstitucional
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TIPOEMP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPOEMP",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CL_CLAS_EMP",
                HeaderText = "Classificação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb24 in TB24_TPEMPRESA.RetornaTodosRegistros()
                             where tb24.NO_TIPOEMP.Contains(txtTipoUnidade.Text) || txtTipoUnidade.Text == ""
                             orderby tb24.NO_TIPOEMP 
                             select new 
                             {
                                 tb24.NO_TIPOEMP, tb24.CO_TIPOEMP,
                                 CL_CLAS_EMP = tb24.CL_CLAS_EMP == "E" ? "Escola" : (tb24.CL_CLAS_EMP == "F" ? "Fornecedor" : "Cliente")
                             }).OrderBy( p => p.CL_CLAS_EMP);
            
            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_TIPOEMP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion        
    }
}