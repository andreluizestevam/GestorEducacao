//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: MOVIMENTAÇÃO DE ITENS DE PATRIMÔNIO
// OBJETIVO: TRANSFERÊNCIA EXTERNA DE ITENS DE PATRIMÔNIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6323_TransferenciaExternaBens
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
            if (IsPostBack) return;

            CarregaUnidade();
            CarregaPatrimonio();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_HISTO_MOVIM_PATRI_EXT" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_PATR",
                HeaderText = "Patrimônio"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_INST_DESTI",
                HeaderText = "Instituição Destino"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_MOVIM_PATRI_EXT",
                HeaderText = "Dt Transferência",
                DataFormatString = "{0:d}"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_STATUS",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;

            var resultado = (from tb231 in TB231_PATRI_HISTO_MOVIM_EXTERNA.RetornaTodosRegistros()
                            where (tb231.TB212_ITENS_PATRIMONIO.CO_EMP == coEmp)
                            && (codPatr != 0 ? tb231.TB212_ITENS_PATRIMONIO.COD_PATR == codPatr : codPatr == 0)
                            select new
                            {
                                tb231.TB212_ITENS_PATRIMONIO.DE_PATR, tb231.NO_INST_DESTI, tb231.DT_MOVIM_PATRI_EXT, 
                                tb231.CO_STATUS, tb231.IDE_HISTO_MOVIM_PATRI_EXT
                            }).OrderByDescending( p => p.DT_MOVIM_PATRI_EXT );


            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_HISTO_MOVIM_PATRI_EXT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
        
        #region Carregamento DropDown

//====> Método que carrega o DropDown de Patrimônios
        private void CarregaPatrimonio()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

            ddlPatrimonio.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                        where tb212.CO_EMP == coEmp && tb212.CO_STATUS == "T"
                                        select new { tb212.DE_PATR, tb212.COD_PATR });

            ddlPatrimonio.DataTextField = "DE_PATR";
            ddlPatrimonio.DataValueField = "COD_PATR";
            ddlPatrimonio.DataBind();

            ddlPatrimonio.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPatrimonio();
        }
    }
}
