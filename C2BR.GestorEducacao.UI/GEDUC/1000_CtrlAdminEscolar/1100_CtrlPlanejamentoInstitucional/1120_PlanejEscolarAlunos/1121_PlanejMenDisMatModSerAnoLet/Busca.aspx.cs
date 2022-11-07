//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: PLANEJAMENTO MENSAL DE DISPONIBILIDADE DE MATRÍCULAS POR MODALIDADE/SÉRIE POR ANO LETIVO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1121_PlanejMenDisMatModSerAnoLet
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
            CarregaAnos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "co_cur", "co_ano_ref" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "co_ano_ref",
                HeaderText = "Ano"
            });

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

            BoundField bfPlanejado = new BoundField();
            bfPlanejado.DataField = "Planejado";
            bfPlanejado.HeaderText = "Planejado";
            bfPlanejado.ItemStyle.CssClass = "numeroCol";
            bfPlanejado.DataFormatString = "{0:N0}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado);

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "Realizado";
            bfRealizado.HeaderText = "Realizado";
            bfRealizado.ItemStyle.CssClass = "numeroCol";
            bfPlanejado.DataFormatString = "{0:N0}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var resultado = (from tb155 in tb155_plan_matr.RetornaTodosRegistros()
                             where tb155.co_ano_ref == ddlAno.SelectedValue
                             && (modalidade != 0 ? tb155.co_modu_cur == modalidade : modalidade == 0) && (serie != 0 ? tb155.co_cur == serie : serie == 0)
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb155.co_cur equals tb01.CO_CUR
                             join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb155.co_modu_cur equals tb44.CO_MODU_CUR
                             where tb01.CO_EMP == LoginAuxili.CO_EMP
                             select new
                             {
                                tb155.co_ano_ref, tb155.co_cur, tb01.NO_CUR, tb44.DE_MODU_CUR,
                                Planejado = (tb155.vl_plan_mes1 + tb155.vl_plan_mes2 + tb155.vl_plan_mes3 + tb155.vl_plan_mes4 + tb155.vl_plan_mes5 + tb155.vl_plan_mes6 + tb155.vl_plan_mes7 + tb155.vl_plan_mes8 + tb155.vl_plan_mes9 + tb155.vl_plan_mes10 + tb155.vl_plan_mes11 + tb155.vl_plan_mes12),
                                Realizado = (tb155.vl_real_mes1 + tb155.vl_real_mes2 + tb155.vl_real_mes3 + tb155.vl_real_mes4 + tb155.vl_real_mes5 + tb155.vl_real_mes6 + tb155.vl_real_mes7 + tb155.vl_real_mes8 + tb155.vl_real_mes9 + tb155.vl_real_mes10 + tb155.vl_real_mes11 + tb155.vl_real_mes12)
                             }).OrderBy( p => p.NO_CUR );

            CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "co_ano_ref"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "co_cur"));

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
        }

//====> Método que carrega o DropDown de Anos de Referência
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

//====> Método que carrega o DropDown de Séries
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                       where tb01.CO_MODU_CUR == modalidade
                                       select new { tb01.CO_CUR, tb01.NO_CUR }).OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }
    }
}
