//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE INFORMAÇÕES DA INSTITUIÇÃO DE ENSINO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfInstEnsino
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ORG_CODIGO_ORGAO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "ORG_NUMERO_CNPJ",
                HeaderText = "CNPJ",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "ORG_NOME_ORGAO",
                HeaderText = "Nome Fantasia"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CID_CODIGO_UF",
                HeaderText = "UF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CIDADE",
                HeaderText = "Cidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_BAIRRO",
                HeaderText = "Bairro"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "tel1",
                HeaderText = "Fone 1"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            decimal decimalRetorno;

            decimal.TryParse(txtCnpjIIE.Text.Replace(".", "").Replace("/", "").Replace("-", ""), out decimalRetorno);

            var resultado = (from tb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                             where (txtNomeInstIIE.Text != "" ? tb000.ORG_NOME_ORGAO.Contains(txtNomeInstIIE.Text) : txtNomeInstIIE.Text == "")
                             && (txtCnpjIIE.Text != "" ? tb000.ORG_NUMERO_CNPJ.Equals(decimalRetorno) : txtCnpjIIE.Text == "")
                             select new
                             {
                                tb000.ORG_CODIGO_ORGAO, ORG_NUMERO_CNPJ = tb000.ORG_NUMERO_CNPJ, tb000.ORG_NOME_ORGAO, tb000.ORG_ENDERE_ORGAO,
                                tb000.CID_CODIGO_UF, CO_CIDADE = tb000.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE, CO_BAIRRO = tb000.TB905_BAIRRO.NO_BAIRRO,
                                tel1 = tb000.ORG_NUMERO_FONE1, tel2 = tb000.ORG_NUMERO_FONE2, ORG_NUMERO_FAX1 = tb000.ORG_NUMERO_FAX1                                    
                             }).OrderBy( i => i.ORG_NOME_ORGAO ).ToList();

            var resultado2 = (from result2 in resultado
                              select new
                              {
                                  result2.ORG_CODIGO_ORGAO, ORG_NUMERO_CNPJ = result2.ORG_NUMERO_CNPJ.ToString().Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-"),
                                  result2.ORG_NOME_ORGAO, result2.ORG_ENDERE_ORGAO, result2.CID_CODIGO_UF, result2.CO_CIDADE, result2.CO_BAIRRO, 
                                  tel1 = result2.tel1 != null ? result2.tel1.ToString().Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                  tel2 = result2.tel2 != null ? result2.tel2.ToString().Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                  ORG_NUMERO_FAX1 = result2.ORG_NUMERO_FAX1 != null ? result2.ORG_NUMERO_FAX1.ToString().Insert(0, "(").Insert(3, ") ").Insert(9, "-") : ""
                              }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ORG_CODIGO_ORGAO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}