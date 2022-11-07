//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: CADASTRAMENTO DE COLABORADORES FUNCIONAIS (Funcionários/Professores)
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1201_CadastramentoColaboradoresFuncionais
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);            
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_COL", "CO_EMP" };            

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_MAT_COL",
                HeaderText = "Matrícula"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CPF_COL",
                HeaderText = "CPF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SEXO_COL",
                HeaderText = "Sexo"
            });

            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "DT_INIC_ATIV_COL";
            bfRealizado2.HeaderText = "Admissão";
            bfRealizado2.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CATEG",
                HeaderText = "Categoria"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "UNID"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = int.Parse(ddlUnidade.SelectedValue);

            var resultado = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                            where ((txtNome.Text != "" && tb03.NO_COL.Contains(txtNome.Text)) || txtNome.Text == "")
                            && ((txtCpf.Text != "" && tb03.NU_CPF_COL.Equals(txtCpf.Text.Replace(".", "").Replace("-", ""))) || txtCpf.Text == "")
                            && (ddlDeficiencia.SelectedValue != "T" ? tb03.TP_DEF.Equals(ddlDeficiencia.SelectedValue) : ddlDeficiencia.SelectedValue == "T")
                            && (ddlSituacao.SelectedValue != "T" ? tb03.CO_SITU_COL.Equals(ddlSituacao.SelectedValue) : ddlSituacao.SelectedValue == "T")
                            && (ddlCategoriaFuncional.SelectedValue != "T" ? tb03.FLA_PROFESSOR.Equals(ddlCategoriaFuncional.SelectedValue) : ddlCategoriaFuncional.SelectedValue == "T")
                            && (coEmp != 0 ? tb03.CO_EMP == coEmp : 0 == 0)
                            select new
                            {
                                tb03.CO_EMP, tb03.TB25_EMPRESA.sigla, tb03.CO_COL,
                                CO_MAT_COL = string.IsNullOrEmpty(tb03.CO_MAT_COL) ? "" : tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                tb03.NO_COL, tb03.DT_INIC_ATIV_COL, CO_SEXO_COL = tb03.CO_SEXO_COL == "F" ? "Feminino" : "Masculino",
                                CATEG = tb03.FLA_PROFESSOR == "S" ? "Professor" : "Funcionário",
                                NU_CPF_COL = tb03.NU_CPF_COL.Insert(9, "-").Insert(6, ".").Insert(3, ".")
                            }).OrderBy( c => c.NO_COL );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCol, "CO_COL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
