//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE OCORRÊNCIAS DISCIPLINARES
// OBJETIVO: CADASTRAMENTO DE OCORRÊNCIAS DISCIPLINARES DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 19/02/2015| Maxwell Almeida           | Criação da funcionalidade para busca das ocorrência salvas

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3643_CadasOcorrSalvas
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaCategorias();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_OCORR_DISCI" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "categ",
                HeaderText = "CATEGORIA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_OCORR",
                HeaderText = "DESCRIÇÃO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_OCORR",
                HeaderText = "SIGLA"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {

            var resultado = (from tbe196 in TBE196_OCORR_DISCI.RetornaTodosRegistros()
                             where (!string.IsNullOrEmpty(txtDescricao.Text) ? tbe196.DE_OCORR.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                             && (ddlTipo.SelectedValue != "0" ? tbe196.CO_CATEG == ddlTipo.SelectedValue : ddlTipo.SelectedValue == "0")
                             select new
                             {
                                 tbe196.ID_OCORR_DISCI,
                                 tbe196.DE_OCORR,
                                 tbe196.CO_SIGLA_OCORR,
                                 categ = (tbe196.CO_CATEG == "A" ? "Aluno(a)" : tbe196.CO_CATEG == "F" ? "Funcionário(a)" :
                                 tbe196.CO_CATEG == "R" ? "Responsável" : "Professor"),
                             }).OrderBy(o => o.DE_OCORR);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_OCORR_DISCI"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega as categorias
        /// </summary>
        private void CarregaCategorias()
        {
            AuxiliCarregamentos.CarregaCategoriaOcorrencias(ddlTipo, true);
        }

        #endregion
    }
}