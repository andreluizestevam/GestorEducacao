//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE FORNECEDORES DE PRODUTOS E SERVIÇOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+----------------------------------------
// 11/04/2013| André Nobre Vinagre        | Criado o botão para redirecionar a tela de cadastramento
//           |                            | de título a pagar.
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 12/04/2013| André Nobre Vinagre        | Alterado o redirecionamento do botão para a tela de cadastro
//           |                            | propriamente dita do "títulos a pagar".
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6234_CadastroFornecedorProdServ
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

            ADMMODULO admModuloMatr = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                       where admMod.nomURLModulo.Contains("5301_CadastramentoTituloDespesaPgto")
                                       select admMod).FirstOrDefault();

            if (admModuloMatr != null)
            {
                lnkCadasTitulPagam.HRef = "/" + String.Format("{0}?op=insert&moduloNome=+Cadastramento+Geral+de+Títulos+de+Despesas/Compromissos+de+Pagamentos&", admModuloMatr.nomURLModulo.Replace("Busca.aspx","Cadastro.aspx"));
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_FORN" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FAN_FOR",
                HeaderText = "Nome Fantasia"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CPFCGC_FORN",
                HeaderText = "Código",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TEL1_FORN",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FAX_FORN",
                HeaderText = "Fax"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_UF_FORN",
                HeaderText = "UF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CIDADE",
                HeaderText = "Cidade"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb41.NO_FAN_FOR.Contains(txtNome.Text) : txtNome.Text == "")
                             && tb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO.Equals(LoginAuxili.ORG_CODIGO_ORGAO)
                             select new
                             {
                                 tb41.CO_FORN, tb41.NO_FAN_FOR, tb41.CO_UF_FORN, NO_CIDADE = tb41.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                 CO_CPFCGC_FORN = (tb41.TP_FORN == "F" && tb41.CO_CPFCGC_FORN.Length >= 11) ? tb41.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb41.TP_FORN == "J" && tb41.CO_CPFCGC_FORN.Length >= 14) ? tb41.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb41.CO_CPFCGC_FORN),
                                 CO_TEL1_FORN = tb41.CO_TEL1_FORN.Insert(0, "(").Insert(3, ") ").Insert(9, "-"),
                                 CO_FAX_FORN = tb41.CO_FAX_FORN.Insert(0, "(").Insert(3, ") ").Insert(9, "-")
                             }).OrderBy( f => f.NO_FAN_FOR );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_FORN"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}