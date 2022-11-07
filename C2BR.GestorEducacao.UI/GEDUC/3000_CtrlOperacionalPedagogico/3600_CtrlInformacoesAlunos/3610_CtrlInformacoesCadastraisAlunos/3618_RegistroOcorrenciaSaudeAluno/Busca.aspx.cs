//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: REGISTRO DE OCORRÊNCIAS DE SAÚDE (ATESTADOS MÉDICOS) DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3618_RegistroOcorrenciaSaudeAluno
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
            CarregaAlunos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_ATEST_MEDIC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Aluno"
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
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;            
            DateTime? dataConsulta = null;
            DateTime dataRetorno;
            dataConsulta = DateTime.TryParse(txtDataConsulta.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;

            var resultado = (from tb143 in TB143_ATEST_MEDIC.RetornaTodosRegistros()
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb143.CO_USU equals tb07.CO_ALU
                            where tb143.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && (coAlu != 0 ? tb143.CO_USU == coAlu : coAlu == 0)
                            && (dataConsulta != null ? dataConsulta == tb143.DT_CONSU : dataConsulta == null) && tb143.TP_USU == "A"
                            select new
                            {
                                tb143.IDE_ATEST_MEDIC, tb07.NO_ALU, tb143.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID,
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

//====> Método que carrega o DropDown de Alunos
        private void CarregaAlunos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                   select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(c => c.NO_ALU);

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }
    }
}
