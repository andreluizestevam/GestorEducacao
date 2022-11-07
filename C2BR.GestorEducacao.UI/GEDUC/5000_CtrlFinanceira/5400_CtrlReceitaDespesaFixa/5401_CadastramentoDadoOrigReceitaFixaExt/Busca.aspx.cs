//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: CADASTRAMENTO DE DADOS CADASTRAIS DE ORIGENS DE RECEITAS FIXAS EXTERNAS
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5401_CadastramentoDadoOrigReceitaFixaExt
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_CLIENTE" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FAN_CLI",
                HeaderText = "Nome"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "CO_CPFCGC_CLI";
            bf1.HeaderText = "Código";
            bf1.ItemStyle.CssClass = "colunaNumerica";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CON_CLI",
                HeaderText = "Contato"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TEL_FIXO_CONTCLI",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_UF_CLI",
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
            var resultado = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()                            
                            join tb104 in TB104_CONT_CLIENTE.RetornaTodosRegistros() on tb103.CO_CLIENTE equals tb104.TB103_CLIENTE.CO_CLIENTE
                             where (txtNome.Text != "" ? tb103.NO_FAN_CLI.Contains(txtNome.Text) : 0 == 0)
                            select new
                            {                      
                                tb103.CO_CLIENTE, tb103.NO_FAN_CLI, tb103.CO_UF_CLI, NO_CIDADE = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE, tb104.NO_CON_CLI,
                                CO_TEL_FIXO_CONTCLI = tb104.CO_TEL_FIXO_CONTCLI.Length >= 10 ? tb104.CO_TEL_FIXO_CONTCLI.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb104.CO_TEL_FIXO_CONTCLI,
                                CO_CPFCGC_CLI = (tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length >= 11) ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length >= 14) ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI),
                                CO_TEL1_CLI = tb103.CO_TEL1_CLI.Length >= 10 ? tb103.CO_TEL1_CLI.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb103.CO_TEL1_CLI,
                                CO_FAX_CLI = tb103.CO_FAX_CLI.Length >= 10 ? tb103.CO_FAX_CLI.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb103.CO_FAX_CLI                                
                            }).OrderBy(c => c.NO_FAN_CLI);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(Resources.QueryStrings.Id, "CO_CLIENTE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}