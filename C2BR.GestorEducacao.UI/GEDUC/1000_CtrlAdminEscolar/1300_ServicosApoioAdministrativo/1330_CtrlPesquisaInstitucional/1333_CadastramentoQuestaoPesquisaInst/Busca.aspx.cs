//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE QUESTÕES PARA PESQUISAS INSITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1333_CadastramentoQuestaoPesquisaInst
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

            CarregaTipo();            
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TIPO_AVAL", "CO_TITU_AVAL", "NU_QUES_AVAL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TITU_AVAL",
                HeaderText = "Grupo de Questões"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_QUES_AVAL",
                HeaderText = "ID"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_QUES_AVAL",
                HeaderText = "Alternativa de Questão"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            { 
               DataField = "CO_ESTI_AVAL",
               HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coTipoAval = ddlTipoAvaliacao.SelectedValue != "" ? int.Parse(ddlTipoAvaliacao.SelectedValue) : 0;

            var resultado = (from tb71 in TB71_QUEST_AVAL.RetornaTodosRegistros()
                             join tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros() on tb71.CO_TIPO_AVAL equals tb73.CO_TIPO_AVAL
                             join tb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros() on tb71.CO_TITU_AVAL equals tb72.CO_TITU_AVAL
                             where (coTipoAval != 0 ? tb73.CO_TIPO_AVAL == coTipoAval : coTipoAval == 0)
                             select new
                             {                                    
                                tb72.NO_TITU_AVAL, tb71.CO_TITU_AVAL, tb71.NU_QUES_AVAL, tb71.DE_QUES_AVAL, tb71.CO_TIPO_AVAL,
                                CO_ESTI_AVAL = tb73.CO_ESTI_AVAL == "A" ? "Ativo" : "Inativo"
                             }).OrderBy( t => t.NO_TITU_AVAL );

            CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("tipoAvaliacao", "CO_TIPO_AVAL"));
            queryStringKeys.Add(new KeyValuePair<string, string>("titAvaliacao", "CO_TITU_AVAL"));
            queryStringKeys.Add(new KeyValuePair<string, string>("nuQuest", "NU_QUES_AVAL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Tipos de Avaliação
        private void CarregaTipo()
        {
            ddlTipoAvaliacao.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                           select new { tb73.CO_TIPO_AVAL, tb73.NO_TIPO_AVAL });

            ddlTipoAvaliacao.DataTextField = "NO_TIPO_AVAL";
            ddlTipoAvaliacao.DataValueField = "CO_TIPO_AVAL";
            ddlTipoAvaliacao.DataBind();

            ddlTipoAvaliacao.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
