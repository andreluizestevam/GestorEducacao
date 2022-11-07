//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: CADASTRAMENTO DE ACORDOS/ADITIVOS DE COMPROMISSOS DE RECEITAS EXTERNAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5403_CadastramentoAditCompReceitaFixa
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "CO_CON_RECDES", "CO_ADITI_RECDES" };

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_CAD_RECDES";
            bf1.HeaderText = "Cadastro";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            bf1.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CON_RECDES",
                HeaderText = "Contrato",
                DataFormatString = "{0:d}"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CPFCGC_CLI",
                HeaderText = "CNPJ/CPF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FAN_CLI",
                HeaderText = "Nome Cliente"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strCNPJ = txtCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            var resultado = (from tb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb37.TB103_CLIENTE.DE_RAZSOC_CLI.Contains(txtNome.Text) : txtNome.Text == "")
                             && (strCNPJ != "" ? tb37.TB103_CLIENTE.CO_CPFCGC_CLI.Equals(strCNPJ) : strCNPJ == "")
                             && tb37.TP_CON_RECDES == "C" && tb37.CO_ADITI_RECDES == 0
                            select new
                            {
                                tb37.CO_ADITI_RECDES, tb37.DT_CAD_RECDES, tb37.CO_CON_RECDES, tb37.TB103_CLIENTE.NO_FAN_CLI, tb37.CO_EMP,
                                CO_CPFCGC_CLI = tb37.TB103_CLIENTE.TP_CLIENTE == "F" ? tb37.TB103_CLIENTE.CO_CPFCGC_CLI.Insert(3, ".").Insert(8, ".").Insert(12, "-") : tb37.TB103_CLIENTE.CO_CPFCGC_CLI.Insert(2, ".").Insert(6, ".").Insert(9, "/")                                    
                            }).OrderBy(d => d.DT_CAD_RECDES).ThenBy(p => p.NO_FAN_CLI);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>("con", "CO_CON_RECDES"));
            queryStringKeys.Add(new KeyValuePair<string, string>("adi", "CO_ADITI_RECDES"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}