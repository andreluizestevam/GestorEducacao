//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: TIPOS DE BOLSA ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2908_AgrupadorBolsa
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_AGRUP_BOLSA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_AGRUP_BOLSA",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_REFER_AGRUP_BOLSA",
                HeaderText = "Código"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_CADAS_AGRUP_BOLSA";
            bf1.HeaderText = "Dt Cadas";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf2 = new BoundField();
            bf2.DataField = "DT_SITUA_AGRUP_BOLSA";
            bf2.HeaderText = "Dt Status";
            bf2.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Status",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb317 in TB317_AGRUP_BOLSA.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb317.NO_AGRUP_BOLSA.Contains(txtNome.Text) : txtNome.Text == "")
                             && (ddlSituacao.SelectedValue != "T" ? tb317.CO_SITUA_AGRUP_BOLSA == ddlSituacao.SelectedValue : ddlSituacao.SelectedValue == "T")
                             && tb317.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 tb317.NO_AGRUP_BOLSA, tb317.CO_REFER_AGRUP_BOLSA, tb317.ID_AGRUP_BOLSA,
                                 tb317.DT_CADAS_AGRUP_BOLSA, tb317.DT_SITUA_AGRUP_BOLSA,
                                 Status = (tb317.CO_SITUA_AGRUP_BOLSA.Equals("A") ? "Ativo" : "Inativo")
                             }).OrderBy(t => t.NO_AGRUP_BOLSA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_AGRUP_BOLSA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
