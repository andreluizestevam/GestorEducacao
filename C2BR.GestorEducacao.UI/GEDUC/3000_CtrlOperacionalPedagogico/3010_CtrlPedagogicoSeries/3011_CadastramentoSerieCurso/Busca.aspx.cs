//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: CADASTRAMENTO DE SÉRIES/CURSOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3011_CadastramentoSerieCurso
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
            {
                CarregaModalidades();
                CarregaClassificacoes();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_CUR", "CO_MODU_CUR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CLASS_CUR",
                HeaderText = "Classificação",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade",
            });            

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Série/Curso"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "VL_TOTA_CUR";
            bf1.HeaderText = "Valor R$";
            bf1.DataFormatString = "{0:N}";
            bf1.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "GRADE",
                HeaderText = "Grade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "MEDIA",
                HeaderText = "Média"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FREQ",
                HeaderText = "Frequência"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            var resultado = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                            join tb133 in TB133_CLASS_CUR.RetornaTodosRegistros() on tb01.CO_NIVEL_CUR equals tb133.CO_SIGLA_CLASS_CUR
                            where tb01.CO_EMP == LoginAuxili.CO_EMP && (txtNome.Text != "" ? tb01.NO_CUR.Contains(txtNome.Text) : txtNome.Text == "")
                            && (modalidade != 0 ? tb01.CO_MODU_CUR == modalidade : modalidade == 0)
                            && (ddlClassificacao.SelectedValue != "" ? tb01.CO_NIVEL_CUR == ddlClassificacao.SelectedValue : ddlClassificacao.SelectedValue == "")
                            && (ddlSituacao.SelectedValue != "T" ? tb01.CO_SITU == ddlSituacao.SelectedValue : ddlSituacao.SelectedValue == "T")
                            select new
                            {
                                tb01.CO_MODU_CUR,
                                DE_MODU_CUR = tb01.TB44_MODULO.DE_MODU_CUR.Length > 35 ? tb01.TB44_MODULO.DE_MODU_CUR.Substring(0, 35) + "..." : tb01.TB44_MODULO.DE_MODU_CUR,
                                tb01.CO_CUR, tb01.NO_CUR, tb01.CO_SIGL_CUR,
                                tb01.QT_CARG_HORA_CUR, tb133.NO_CLASS_CUR, MEDIA = tb01.MED_FINAL_CUR,
                                FREQ = tb01.CO_PARAM_FREQUE == "M" ? "Por Matéria" : "Por Dia",
                                GRADE = tb01.CO_TIPO_GRADE_CUR == "M" ? "Mista" : tb01.CO_TIPO_GRADE_CUR == "F" ? "Fechada" : "Aberta", tb01.VL_TOTA_CUR
                            }).OrderBy( c => c.DE_MODU_CUR ).ThenBy( c => c.NO_CLASS_CUR ).ThenBy( c => c.NO_CUR );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));

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

//====> Método que carrega o DropDown de Classificação
        private void CarregaClassificacoes()
        {
            ddlClassificacao.DataSource = TB133_CLASS_CUR.RetornaTodosRegistros();

            ddlClassificacao.DataTextField = "NO_CLASS_CUR";
            ddlClassificacao.DataValueField = "CO_SIGLA_CLASS_CUR";
            ddlClassificacao.DataBind();

            ddlClassificacao.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion
    }
}
