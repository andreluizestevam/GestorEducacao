//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: REGISTRO DE OCORRÊNCIA DE ITENS DE PATRIMÔNIO
// OBJETIVO: OCORRÊNCIAS DE ITENS DE PATRIMÔNIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6350_CtrlOcorrenciaItensPatrimonio.F6351_RegistroOcorrItensPatrimonio
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
                CarregaDropDown();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_OCORR_PATR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_PATR",
                HeaderText = "Patrimônio"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_TIPO_OCORR",
                HeaderText = "Tipo Ocorrência"
            });

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DT_OCORR";
            bfRealizado.HeaderText = "Dt. Ocorrência";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int idTipoOcorPatr = ddlTipoOcorrencia.SelectedValue != "" ? int.Parse(ddlTipoOcorrencia.SelectedValue) : 0;
            decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;

            var resultado = (from tb230 in TB230_OCORR_PATRIMONIO.RetornaTodosRegistros()
                            where (codPatr != 0 ? tb230.TB212_ITENS_PATRIMONIO.COD_PATR == codPatr : codPatr == 0)
                            && (tb230.TB212_ITENS_PATRIMONIO.CO_EMP == coEmp)
                            && (idTipoOcorPatr != 0 ? tb230.TB229_TIPO_OCOR_PATRIMONIO.ID_TIPO_OCORR_PATR == idTipoOcorPatr : idTipoOcorPatr == 0)
                            select new
                            {
                                tb230.TB212_ITENS_PATRIMONIO.DE_PATR, tb230.ID_OCORR_PATR, tb230.TB229_TIPO_OCOR_PATRIMONIO.DE_TIPO_OCORR, tb230.DT_OCORR
                            }).OrderBy( o => o.DE_PATR );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_OCORR_PATR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares e Tipo de Ocorrência de Patrimônio
        protected void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlTipoOcorrencia.DataSource = TB229_TIPO_OCOR_PATRIMONIO.RetornaTodosRegistros();

            ddlTipoOcorrencia.DataTextField = "DE_TIPO_OCORR";
            ddlTipoOcorrencia.DataValueField = "ID_TIPO_OCORR_PATR";
            ddlTipoOcorrencia.DataBind();

            ddlTipoOcorrencia.Items.Insert(0, new ListItem("", ""));

            CarregaPatrimonio();
        }

//====> Método que carrega o DropDown de Patrimônios
        protected void CarregaPatrimonio()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

            ddlPatrimonio.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                        where tb212.CO_EMP == coEmp && tb212.CO_STATUS != "T" 
                                        select new { tb212.DE_PATR, tb212.COD_PATR });

            ddlPatrimonio.DataTextField = "DE_PATR";
            ddlPatrimonio.DataValueField = "COD_PATR";
            ddlPatrimonio.DataBind();

            ddlPatrimonio.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPatrimonio();
        }
    }
}
