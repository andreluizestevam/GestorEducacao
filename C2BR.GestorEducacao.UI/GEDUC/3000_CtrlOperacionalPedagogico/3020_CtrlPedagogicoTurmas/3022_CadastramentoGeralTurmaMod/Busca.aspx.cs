//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: CADASTRAMENTO GERAL DE TURMAS POR MODALIDADE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3022_CadastramentoGeralTurmaMod
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
                CarregaModalidade();
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {            
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TUR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TURMA",
                HeaderText = "Turma"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_TURMA",
                HeaderText = "Sigla"
            });
            /*
            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "CO_FLAG_MULTI_SERIE";
            bfRealizado1.HeaderText = "Multi Série";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_TIPO_TURMA",
                HeaderText = "Tipo Turma"
            });
            
            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DT_STATUS_TURMA";
            bfRealizado.HeaderText = "Dt Status";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
            */
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "Unidade Financeira"
            });

            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "CO_STATUS_TURMA";
            bfRealizado2.HeaderText = "Status";
            bfRealizado2.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);

            CurrentPadraoBuscas.GridBusca.Width = 550;
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            var resultado = (from tb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb129.CO_EMP_UNID_CONT equals tb25.CO_EMP
                             where (txtNO_TURMA.Text.Trim() != "" ? tb129.NO_TURMA.Contains(txtNO_TURMA.Text.Trim()) : txtNO_TURMA.Text.Trim() == "")
                             && (modalidade != 0 ? tb129.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                             && tb129.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             select new
                             {
                                tb129.CO_TUR, tb129.TB44_MODULO.DE_MODU_CUR, tb129.NO_TURMA, tb129.CO_SIGLA_TURMA, tb129.DT_STATUS_TURMA,                                
                                CO_FLAG_MULTI_SERIE = tb129.CO_FLAG_MULTI_SERIE.ToUpper().Equals("S") ? "Sim" : "Não",
                                CO_FLAG_TIPO_TURMA = tb129.CO_FLAG_TIPO_TURMA.ToUpper().Equals("P") ? "Presencial" : (tb129.CO_FLAG_TIPO_TURMA.ToUpper().Equals("S") ? "Semi-Presencial" : "Não-Presencial"),
                                CO_STATUS_TURMA = tb129.CO_STATUS_TURMA.ToUpper().Equals("A") ? "Ativo" : "Inativo",
                                sigla = tb25.sigla,
                             }).OrderBy( c => c.DE_MODU_CUR ).ThenBy( c => c.NO_TURMA );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {             
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_TUR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Modalidades
        protected void CarregaModalidade()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion
    }
}