//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: SAÚDE
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE FAMÍLIA
// OBJETIVO: CADASTRAMENTO DE FAMÍLIA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 04/03/2014| Vinícius Reis              | Criada a funcionalidade de cadastro de família.
//           |                            | 
//           |                            | 

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

namespace C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2101_CadastroFamilia
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }
        private static Dictionary<string, string> tipoDef = AuxiliBaseApoio.chave(tipoDeficienciaAluno.ResourceManager, true);

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_FAMILIA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FAMILIA",
                HeaderText = "CÓDIGO"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_RESP_FAM",
                HeaderText = "Nome"
            });
            //CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            //{
            //    DataField = "NU_CPF_RESP_FAM",
            //    HeaderText = "CPF"
            //});
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb075 in TB075_FAMILIA.RetornaTodosRegistros()
                             where (txtCodigo.Text != "" ? tb075.CO_FAMILIA == txtCodigo.Text.ToUpper() : true)
                              && (txtNome.Text != "" ? tb075.NO_RESP_FAM.Contains(txtNome.Text) : true)
                              && (txtCpf.Text != "" ? tb075.NU_CPF_RESP_FAM.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : true)
                             select new listaFamilia
                             {
                                 CO_FAMILIA = tb075.CO_FAMILIA,
                                 NO_RESP_FAM = tb075.NO_RESP_FAM,
                                 NU_CPF_RESP_FAM = tb075.NU_CPF_RESP_FAM.Length == 11 ? tb075.NU_CPF_RESP_FAM.Insert(3, ".").Insert(7, ".").Insert(11, "-") : ""
                             }).OrderBy(a => a.CO_FAMILIA);

            if (resultado != null && resultado.Count() > 0)
                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_FAMILIA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregadores

        #endregion

        #region Classes
        /// <summary>
        /// Classe com os campos necessários para carregar a grid de buscas
        /// </summary>
        private class listaFamilia
        {
            public string CO_FAMILIA { get; set; }
            public string NO_RESP_FAM { get; set; }
            public string NU_CPF_RESP_FAM { get; set; }
           
        }

        #endregion
    }
}