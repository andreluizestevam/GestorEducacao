//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE INFORMAÇÕES DE UNIDADES DE ENSINO E DE APOIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfUnidEnsinoApoio
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CPFCGC_EMP",
                HeaderText = "CNPJ",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Nome Fantasia"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_END_EMP",
                HeaderText = "Endereço"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CIDADE",
                HeaderText = "Cidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_UF_EMP",
                HeaderText = "UF"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strCNPJ = txtCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            var resultado = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                             join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb25.CO_BAIRRO equals tb905.CO_BAIRRO
                             where (txtNomeUnidade.Text != "" ? tb25.NO_FANTAS_EMP.Contains(txtNomeUnidade.Text) : txtNomeUnidade.Text == "")
                             && (txtCnpj.Text != "" ? tb25.CO_CPFCGC_EMP.Equals(strCNPJ) : txtCnpj.Text == "")
                             && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 CO_CPFCGC_EMP = tb25.CO_CPFCGC_EMP,//.Length == 11 ? tb25.CO_CPFCGC_EMP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb25.CO_CPFCGC_EMP.Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-"),
                                 NO_FANTAS_EMP = tb25.NO_FANTAS_EMP.Substring(0, 30) + (tb25.NO_FANTAS_EMP.Length > 30 ? "..." : ""),
                                 DE_END_EMP = tb25.DE_END_EMP.Substring(0, 25) + (tb25.DE_END_EMP.Length > 25 ? "..." : ""),
                                 tb25.CO_EMP,
                                 tb25.CO_UF_EMP,
                                 tb905.TB904_CIDADE.NO_CIDADE

                             }).OrderBy(e => e.NO_FANTAS_EMP);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_EMP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}