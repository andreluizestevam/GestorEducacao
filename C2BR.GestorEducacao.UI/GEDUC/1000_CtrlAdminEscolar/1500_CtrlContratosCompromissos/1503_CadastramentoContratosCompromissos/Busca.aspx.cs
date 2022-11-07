//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: CONTROLE DE CONTRATOS DE COMPROMISSOS
// OBJETIVO: CADASTRAMENTO DE CONTRATOS DE COMPROMISSOS INSTITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1503_CadastramentoContratosCompromissos
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "CO_CONTR_COMPR", "CO_ADITI_CONTR_COMPR" };

            BoundField bf1 = new BoundField();
            bf1.DataField = "DT_CAD_CONTR_COMPR";
            bf1.HeaderText = "Cadastro";
            bf1.DataFormatString = "{0:dd/MM/yyyy}";
            bf1.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CONTR_COMPR",
                HeaderText = "Contrato"
            });

            BoundField bfCO_ADITI_RECDES = new BoundField();
            bfCO_ADITI_RECDES.DataField = "CO_ADITI_CONTR_COMPR";
            bfCO_ADITI_RECDES.HeaderText = "N° Aditivo";
            bfCO_ADITI_RECDES.ItemStyle.CssClass = "centro";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfCO_ADITI_RECDES);

            BoundField bf2 = new BoundField();
            bf2.DataField = "CO_CPFCGC_CLI";
            bf2.HeaderText = "CNPJ/CPF";
            bf2.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FAN_CLI",
                HeaderText = "Nome Cliente"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_CON_CONTR_COMPR",
                HeaderText = "Tipo"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strCNPJ = txtCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", ""); 

            var resultado = (from tb312 in TB312_CONTR_COMPR.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb312.TB103_CLIENTE.DE_RAZSOC_CLI.Contains(txtNome.Text) : txtNome.Text == "")
                            && (strCNPJ != "" ? tb312.TB103_CLIENTE.CO_CPFCGC_CLI.Equals(strCNPJ) : strCNPJ == "")
                            select new
                            {
                                tb312.CO_ADITI_CONTR_COMPR, tb312.DT_CAD_CONTR_COMPR, tb312.CO_CONTR_COMPR, tb312.TB103_CLIENTE.NO_FAN_CLI, tb312.CO_EMP,
                                CO_CPFCGC_CLI = tb312.TB103_CLIENTE.TP_CLIENTE == "F" ? tb312.TB103_CLIENTE.CO_CPFCGC_CLI.Insert(3, ".").Insert(8, ".").Insert(12, "-") : tb312.TB103_CLIENTE.CO_CPFCGC_CLI.Insert(2, ".").Insert(6, ".").Insert(9, "/"),
                                TP_CON_CONTR_COMPR = tb312.TP_CON_CONTR_COMPR == "D" ? "Despesa" : "Receita"
                            }).OrderBy( p => p.DT_CAD_CONTR_COMPR ).ThenBy( d => d.NO_FAN_CLI );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>("con", "CO_CONTR_COMPR"));
            queryStringKeys.Add(new KeyValuePair<string, string>("adi", "CO_ADITI_CONTR_COMPR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}