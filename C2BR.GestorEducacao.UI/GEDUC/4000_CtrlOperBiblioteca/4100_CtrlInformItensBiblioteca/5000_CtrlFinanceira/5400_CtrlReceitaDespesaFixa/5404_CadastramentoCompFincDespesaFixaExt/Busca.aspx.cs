//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS E DESPESAS FIXAS
// OBJETIVO: CADASTRAMENTO DE COMPROMISSOS FINANCEIROS DE DESPESAS FIXAS EXTERNAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5404_CadastramentoCompFincDespesaFixaExt
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

            BoundField bfCO_ADITI_RECDES = new BoundField();
            bfCO_ADITI_RECDES.DataField = "CO_ADITI_RECDES";
            bfCO_ADITI_RECDES.HeaderText = "Adit";
            bfCO_ADITI_RECDES.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfCO_ADITI_RECDES);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CON_RECDES",
                HeaderText = "Contrato"
            });

            BoundField bf7 = new BoundField();
            bf7.DataField = "DT_INI_CON_RECDES";
            bf7.HeaderText = "Início";
            bf7.DataFormatString = "{0:dd/MM/yyyy}";
            bf7.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf7);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DES_TIPO_DOC",
                HeaderText = "Tipo Documento"
            });

            BoundField bf2 = new BoundField();
            bf2.DataField = "CO_CPFCGC_FORN";
            bf2.HeaderText = "Código";
            bf2.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FAN_FOR",
                HeaderText = "Fornecedor"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb37 in TB37_RECDES_FIXA.RetornaTodosRegistros()
                            where (txtNome.Text != "" ? tb37.TB41_FORNEC.NO_FAN_FOR.Contains(txtNome.Text) : txtNome.Text == "") && tb37.TP_CON_RECDES == "D"
                            select new
                            {
                                tb37.DT_CAD_RECDES, tb37.DT_INI_CON_RECDES, tb37.CO_CON_RECDES, tb37.CO_ADITI_RECDES, tb37.TB086_TIPO_DOC.DES_TIPO_DOC, tb37.TP_CON_RECDES,tb37.TB41_FORNEC.NO_FAN_FOR, tb37.CO_EMP,
                                CO_CPFCGC_FORN = (tb37.TB41_FORNEC.TP_FORN == "F" && tb37.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 11) ? tb37.TB41_FORNEC.CO_CPFCGC_FORN.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                ((tb37.TB41_FORNEC.TP_FORN == "J" && tb37.TB41_FORNEC.CO_CPFCGC_FORN.Length >= 14) ? tb37.TB41_FORNEC.CO_CPFCGC_FORN.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb37.TB41_FORNEC.CO_CPFCGC_FORN)
                            }).OrderByDescending(r => r.DT_CAD_RECDES);

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