//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS DE SAÚDE (ATESTADOS MÉDICOS) DO COLABORADOR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1206_RegistroOcorrenciaSaudeColaborador
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
            CarregaUnidades();
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            CarregaColaboradores();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_ATEST_MEDIC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Colaborador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CID",
                HeaderText = "Doença"
            });

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DT_CONSU";
            bfRealizado.HeaderText = "Consulta";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);

            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "DT_ENTRE_ATEST";
            bfRealizado1.HeaderText = "Entrega";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            bfRealizado1.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;            
            DateTime? dataConsulta = null;
            DateTime dataRetorno;
            dataConsulta = DateTime.TryParse(txtDataConsulta.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;

            var resultado = (from tb143 in TB143_ATEST_MEDIC.RetornaTodosRegistros()
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb143.CO_USU equals tb03.CO_COL
                            where tb143.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && (coCol != 0 ? tb143.CO_USU == coCol : coCol == 0)
                            && (dataConsulta != null ? dataConsulta == tb143.DT_CONSU : dataConsulta == null) && tb143.TP_USU == "F"
                            select new
                            {
                                tb143.IDE_ATEST_MEDIC, tb03.NO_COL, tb143.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID,
                                tb143.DT_CONSU, tb143.DT_ENTRE_ATEST
                            }).OrderBy(p => p.DT_CONSU);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_ATEST_MEDIC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }        
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }

//====> Método que carrega o DropDown de Colaboradores
        private void CarregaColaboradores()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
    }
}
