//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: CADASTRAMENTO DE COORDENAÇÃO DE DEPARTAMENTO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3013_CadastramentoCoordDepto
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

        void Page_Load()
        {
            if (!Page.IsPostBack) 
                CarregaDepartamentos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_COOR_CUR", "CO_DPTO_CUR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SG_DPTO_CUR",
                HeaderText = "Dpto"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SG_COOR_CUR",
                HeaderText = "Coord"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COOR_CUR",
                HeaderText = "Nome"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coDptoCur = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;

            var resultado = (from tb68 in TB68_COORD_CURSO.RetornaTodosRegistros()
                            where tb68.CO_EMP == LoginAuxili.CO_EMP && (coDptoCur != 0 ? tb68.CO_DPTO_CUR == coDptoCur : coDptoCur == 0)
                            && (txtNome.Text != "" ? tb68.NO_COOR_CUR.Contains(txtNome.Text) : txtNome.Text == "")
                            select new
                            {
                                tb68.TB77_DPTO_CURSO.SG_DPTO_CUR, tb68.CO_DPTO_CUR, tb68.CO_COOR_CUR, tb68.NO_COOR_CUR, tb68.SG_COOR_CUR
                            }).OrderBy( c => c.NO_COOR_CUR );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_COOR_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>("dep", "CO_DPTO_CUR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
        
        #region Carregamento DropDown

//====> Método que carrega o DropDown de Departamentos
        private void CarregaDepartamentos()
        {
            ddlDepartamento.DataSource = TB77_DPTO_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP);

            ddlDepartamento.DataTextField = "NO_DPTO_CUR";
            ddlDepartamento.DataValueField = "CO_DPTO_CUR";
            ddlDepartamento.DataBind();

            ddlDepartamento.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
