//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *******************
// SUBMÓDULO: ****************
// OBJETIVO: *****************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1906_CargoFuncao
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                CarregaGrupoCBO();
                VerificarTipoUnidade();
            }
        }

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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_FUN" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FUN",
                HeaderText = "Descrição"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DESC",
                HeaderText = "GRP-CBO"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CBO_FUN",
                HeaderText = "CODIGO"
            });
            if (LoginAuxili.CO_TIPO_UNID == "PGE")
            {
                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {

                    DataField = "CO_FLAG_CLASSI_MAGIST",
                    HeaderText = "MAG"
                });
            }
            else
            {
                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {

                    DataField = "CO_FLAG_CLASSI_MAGIST",
                    HeaderText = "SAU"

                });
            }
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_CLASSI_ADMINI",
                HeaderText = "ADM"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_CLASSI_OPERAC",
                HeaderText = "OPE"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_CLASSI_NUCLEO",
                HeaderText = "NUC"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()                            
                             where (txtNO_FUN.Text != "" ? tb15.NO_FUN.Contains(txtNO_FUN.Text) : txtNO_FUN.Text == "")
                             && (chkTodos.Checked || tb15.CO_FLAG_CLASSI_ADMINI.Value.Equals(chkCO_FLAG_CLASSI_ADMINI.Checked)
                             && tb15.CO_FLAG_CLASSI_MAGIST.Value.Equals(chkCO_FLAG_CLASSI_MAGIST.Checked)
                             && tb15.CO_FLAG_CLASSI_NUCLEO.Value.Equals(chkCO_FLAG_CLASSI_NUCLEO.Checked)
                             && tb15.CO_FLAG_CLASSI_OPERAC.Value.Equals(chkCO_FLAG_CLASSI_OPERAC.Checked))
                             select new
                             {
                                 //DESC = tb15.TB316_CBO_GRUPO.DE_CBO_GRUPO,
                                 DESC = tb15.TB316_CBO_GRUPO.CO_CBO_GRUPO,
                                 CO_CBO_FUN = tb15.CO_CBO_FUN,
                                 tb15.CO_FUN,                               
                                 tb15.NO_FUN,
                                 CO_FLAG_CLASSI_MAGIST = ((tb15.CO_FLAG_CLASSI_MAGIST == true) ? "Sim" : "Não"),
                                 CO_FLAG_CLASSI_ADMINI = ((tb15.CO_FLAG_CLASSI_ADMINI == true) ? "Sim" : "Não"),
                                 CO_FLAG_CLASSI_OPERAC = ((tb15.CO_FLAG_CLASSI_OPERAC == true) ? "Sim" : "Não"),
                                 CO_FLAG_CLASSI_NUCLEO = ((tb15.CO_FLAG_CLASSI_NUCLEO == true) ? "Sim" : "Não")
                             }).OrderBy(f => f.NO_FUN);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_FUN"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        /// <summary>
        /// Verifica o tipo da unidade e altera informacoes especificas
        /// </summary>
        private void VerificarTipoUnidade()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    chkCO_FLAG_CLASSI_MAGIST.Text = "Magistério";
                    break;
                case "PGS":
                    chkCO_FLAG_CLASSI_MAGIST.Text = "Saúde";

                    break;

            }
        }

        #endregion

        #region Carregamento DropDown
        /// <summary>
        /// Método que carrega o dropdown de Grupo CBO
        /// </summary>
        private void CarregaGrupoCBO()
        {
            ddlGrupoCBO.DataSource = (from tb316 in TB316_CBO_GRUPO.RetornaTodosRegistros()
                                      select new
                                      {
                                          tb316.CO_CBO_GRUPO,
                                          DESC = tb316.CO_CBO_GRUPO + " - " + tb316.DE_CBO_GRUPO
                                      }).OrderBy(e => e.DESC);

            ddlGrupoCBO.DataValueField = "CO_CBO_GRUPO";
            ddlGrupoCBO.DataTextField = "DESC";
            ddlGrupoCBO.DataBind();
            ddlGrupoCBO.Items.Insert(0, new ListItem("Todos", "0"));
        }



        /// <summary>
        /// Método que carrega o dropdown de Codigo CBO
        /// </summary>
        private void CarregaFuncoes()
        {
            ddlFuncaoColab.DataSource = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                         where tb15.TB316_CBO_GRUPO.CO_CBO_GRUPO == ddlGrupoCBO.SelectedValue
                                         select new 
                                         { 
                                             tb15.CO_FUN, 
                                             NO_FUN = tb15.CO_CBO_FUN //+ " - " + tb15.NO_FUN, tb15.CO_CBO_FUN 

                                         }).OrderBy(p => p.CO_FUN);

            ddlFuncaoColab.DataTextField = "NO_FUN";
            ddlFuncaoColab.DataValueField = "CO_FUN";
            ddlFuncaoColab.DataBind();

            ddlFuncaoColab.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        protected void ddlGrupoCBO_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncoes();
        }
    }
}
