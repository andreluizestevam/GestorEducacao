//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: REGISTRO DE QUANTIDADE DE AULAS PROGRAMADAS POR SÉRIE/CURSO E MATÉRIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.RegistroQtdeAulasProgramadas
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

            ddlAno.SelectedValue = DateTime.Now.Year.ToString();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ANO_REF", "CO_MODU_CUR", "CO_CUR", "CO_MAT" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_REF",
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

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_MATERIA",
                HeaderText = "Matéria"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SIGLA_MATERIA",
                HeaderText = "Sigla Mat."
            });

            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "Planejado";
            bfRealizado1.HeaderText = "Planejado";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);

            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "Realizado";
            bfRealizado2.HeaderText = "Realizado";
            bfRealizado2.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;   

            var resultado = (from tbQtdeAulas in TB_QTDE_AULAS.RetornaTodosRegistros()
                            where tbQtdeAulas.CO_EMP == LoginAuxili.CO_EMP && tbQtdeAulas.CO_ANO_REF == ano
                            && (serie != 0 ? tbQtdeAulas.CO_CUR.Equals(serie) : serie == 0)
                            && (modalidade != 0 ? tbQtdeAulas.CO_MODU_CUR.Equals(modalidade) : modalidade == 0)
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on tbQtdeAulas.CO_MODU_CUR equals tb44.CO_MODU_CUR
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on tbQtdeAulas.CO_CUR equals tb01.CO_CUR
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbQtdeAulas.CO_MAT equals tb02.CO_MAT
                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                            select new
                            {
                                tbQtdeAulas.CO_ANO_REF, tbQtdeAulas.CO_MODU_CUR, tbQtdeAulas.CO_CUR, tbQtdeAulas.CO_MAT, tb01.NO_CUR,
                                tb44.DE_MODU_CUR, tb107.NO_MATERIA, tb107.NO_SIGLA_MATERIA,
                                Planejado = (tbQtdeAulas.QT_AULAS_PROG_ABR + tbQtdeAulas.QT_AULAS_PROG_AGO + tbQtdeAulas.QT_AULAS_PROG_DEZ + tbQtdeAulas.QT_AULAS_PROG_FEV + tbQtdeAulas.QT_AULAS_PROG_JAN + tbQtdeAulas.QT_AULAS_PROG_JUL + tbQtdeAulas.QT_AULAS_PROG_JUN + tbQtdeAulas.QT_AULAS_PROG_MAI + tbQtdeAulas.QT_AULAS_PROG_MAR + tbQtdeAulas.QT_AULAS_PROG_NOV + tbQtdeAulas.QT_AULAS_PROG_OUT + tbQtdeAulas.QT_AULAS_PROG_SET),
                                Realizado = (tbQtdeAulas.QT_AULAS_REAL_ABR + tbQtdeAulas.QT_AULAS_REAL_AGO + tbQtdeAulas.QT_AULAS_REAL_DEZ + tbQtdeAulas.QT_AULAS_REAL_FEV + tbQtdeAulas.QT_AULAS_REAL_JAN + tbQtdeAulas.QT_AULAS_REAL_JUL + tbQtdeAulas.QT_AULAS_REAL_JUN + tbQtdeAulas.QT_AULAS_REAL_MAI + tbQtdeAulas.QT_AULAS_REAL_MAR + tbQtdeAulas.QT_AULAS_REAL_NOV + tbQtdeAulas.QT_AULAS_REAL_OUT + tbQtdeAulas.QT_AULAS_REAL_SET)
                            }).OrderByDescending( q => q.CO_ANO_REF ).ThenBy( q => q.DE_MODU_CUR ).ThenBy( q => q.NO_CUR ).ThenBy( q => q.NO_MATERIA );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_REF"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoMat, "CO_MAT"));

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

//====> Método que carrega o DropDown de Anos
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

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }
    }
}
