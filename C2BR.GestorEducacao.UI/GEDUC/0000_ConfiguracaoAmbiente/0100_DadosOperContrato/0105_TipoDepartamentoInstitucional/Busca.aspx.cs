//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: DADOS OPERACIONAIS DE CONTRATO
// OBJETIVO: DEPARTAMENTO INSTITUCIONAL.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0105_TipoDepartamentoInstitucional
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_DEPTO_TIPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_DEPTO_TIPO",
                HeaderText = "Tipo Departamento/Local"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CLASS",
                HeaderText = "Classificação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb174 in TB174_DEPTO_TIPO.RetornaTodosRegistros()
                             where (txtNomeDepto.Text != "" ? (tb174.NO_DEPTO_TIPO.Contains(txtNomeDepto.Text)) : txtNomeDepto.Text == "")
                            && tb174.TB03_COLABOR.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                            && (ddlSitua.SelectedValue != "" ? tb174.CO_SITU_TIPO.Contains(ddlSitua.SelectedValue) : ddlSitua.SelectedValue == "")
                            && (ddlClass.SelectedValue != "" ? tb174.CO_CLASS_TIPO_LOCAL.Equals(ddlClass.SelectedValue) : 0 == 0)
                             select new { tb174.ID_DEPTO_TIPO, tb174.NO_DEPTO_TIPO, tb174.CO_SITU_TIPO,
                                          CO_SITU = (tb174.CO_SITU_TIPO == "A" ? "Ativo" : "Inativo"),
                                          CO_CLASS = (tb174.CO_CLASS_TIPO_LOCAL == "ADM" ? "Administrativo" : tb174.CO_CLASS_TIPO_LOCAL == "FIN" ? "Financeiro" :
                                                      tb174.CO_CLASS_TIPO_LOCAL == "OPE" ? "Operacional" : tb174.CO_CLASS_TIPO_LOCAL == "ACO" ? "Acomodação" : tb174.CO_CLASS_TIPO_LOCAL == "ATE" ? "Atendimento" : tb174.CO_CLASS_TIPO_LOCAL == "TEC" ? "Técnico" : "-")
                             }).OrderBy(d => d.NO_DEPTO_TIPO);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_DEPTO_TIPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
