//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: ASSOCIAÇÃO DE DISCIPLINA A MODALIDADE/SÉRIE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3015_AssociacaoDisciplinaModuSerie
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
            if (IsPostBack) return;

            CarregaModalidades();
            CarregaSerieCurso();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_MAT", "CO_MODU_CUR", "CO_CUR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Série/Curso"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_MATERIA",
                HeaderText = "Disciplina"
            });

            CurrentPadraoBuscas.GridBusca.PageSize = 15;
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var resultado = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                            where tb02.CO_EMP == LoginAuxili.CO_EMP && (modalidade != 0 ? tb02.CO_MODU_CUR == modalidade : modalidade == 0 ) 
                            && (serie != 0 ? tb02.CO_CUR == serie : serie == 0)                            
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb02.CO_MODU_CUR equals tb44.CO_MODU_CUR
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                            select new
                            {
                                tb02.CO_MAT, tb02.CO_MODU_CUR, tb02.CO_CUR, tb01.NO_CUR, tb44.DE_MODU_CUR, tb107.NO_MATERIA, tb107.ID_MATERIA
                            }).OrderBy( m => m.DE_MODU_CUR ).ThenBy( m => m.NO_CUR ).ThenBy( m => m.NO_MATERIA );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoMat, "CO_MAT"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Modalidades
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Séries
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).OrderBy(c => c.CO_SIGL_CUR);

            ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }
    }
}
