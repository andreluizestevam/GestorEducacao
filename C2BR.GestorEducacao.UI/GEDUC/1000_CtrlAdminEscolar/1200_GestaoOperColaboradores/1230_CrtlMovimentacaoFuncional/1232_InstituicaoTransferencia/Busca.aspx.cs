//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO DE MOVIMENTAÇÃO FUNCIONAL
// OBJETIVO: CADASTRAMENTO DE INSTITUIÇÃO DE TRANSFERÊNCIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1232_InstituicaoTransferencia
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_INSTIT_TRANSF" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_INSTIT_TRANSF",
                HeaderText = "Nome Fantasia"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CPFCGC_INSTIT_TRANSF",
                HeaderText = "CNPJ",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TEL1_INSTIT_TRANSF",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FAX_INSTIT_TRANSF",
                HeaderText = "Fax"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_UF_INSTIT_TRANSF",
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
            string strCNPJ = txtCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            var resultado = (from tb285 in TB285_INSTIT_TRANSF.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb285.NO_INSTIT_TRANSF.Contains(txtNome.Text) : txtNome.Text == "")
                             && (strCNPJ != "" ? tb285.CO_CPFCGC_INSTIT_TRANSF.Equals(strCNPJ) : strCNPJ == "")
                             select new
                             {
                                 tb285.ID_INSTIT_TRANSF,
                                 tb285.NO_INSTIT_TRANSF,
                                 tb285.CO_UF_INSTIT_TRANSF,
                                 NO_CIDADE = tb285.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                 CO_CPFCGC_INSTIT_TRANSF = tb285.CO_CPFCGC_INSTIT_TRANSF.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-"),
                                 CO_TEL1_INSTIT_TRANSF = tb285.CO_TEL1_INSTIT_TRANSF.Insert(0, "(").Insert(3, ") ").Insert(9, "-"),
                                 CO_FAX_INSTIT_TRANSF = tb285.CO_FAX_INSTIT_TRANSF.Insert(0, "(").Insert(3, ") ").Insert(9, "-")
                             }).OrderBy(f => f.NO_INSTIT_TRANSF);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_INSTIT_TRANSF"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}