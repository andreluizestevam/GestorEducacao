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
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7300_CtrlCadastralInforFunc._7310_CadastroColabor
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaClassificacoesFuncionais();
            }
        }
        protected void CarregaClassificacoesFuncionais()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassificacaoProficiona, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
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
                HeaderText = "Matríc"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Nome"
            });

            //CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            //{
            //    DataField = "NU_CPF_COL",
            //    HeaderText = "CPF"
            //});


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CLASS",
                HeaderText = "Classificação"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_FUNC_COL",
                HeaderText = "Cargo/Função"
            });
          
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SIT",
                HeaderText = "Situa"
            });
            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "DT_INIC_ATIV_COL";
            bfRealizado2.HeaderText = "Admissão";
            bfRealizado2.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "UNID"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                             where ((txtNome.Text != "" && tb03.NO_COL.Contains(txtNome.Text)) || txtNome.Text == "")
                             && ((txtCpf.Text != "" && tb03.NU_CPF_COL.Equals(txtCpf.Text.Replace(".", "").Replace("-", ""))) || txtCpf.Text == "")
                             && ((txtFuncao.Text != "" && tb03.DE_FUNC_COL.Contains(txtFuncao.Text)) || txtFuncao.Text == "")
                             && (ddlDeficiencia.SelectedValue != "T" ? tb03.TP_DEF.Equals(ddlDeficiencia.SelectedValue) : ddlDeficiencia.SelectedValue == "T")
                             && (ddlSituacao.SelectedValue != "T" ? tb03.CO_SITU_COL.Equals(ddlSituacao.SelectedValue) : ddlSituacao.SelectedValue == "T")
                             && (ddlClassificacaoProficiona.SelectedValue != "0" ? tb03.CO_CLASS_PROFI.Equals(ddlClassificacaoProficiona.SelectedValue) : ddlClassificacaoProficiona.SelectedValue == "0")
                             && (ddlCategoriaFuncional.SelectedValue != "T" ? tb03.FLA_PROFESSOR.Equals(ddlCategoriaFuncional.SelectedValue) : ddlCategoriaFuncional.SelectedValue == "T")
                             && tb03.CO_EMP == LoginAuxili.CO_EMP
                             select new 
                             {
                                 tb03.CO_EMP,
                                 tb03.TB25_EMPRESA.sigla,
                                 tb03.CO_COL,
                                 CO_MAT_COL = string.IsNullOrEmpty(tb03.CO_MAT_COL) ? "" : tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                 NO_COL = tb03.NO_COL.Length > 25 ? tb03.NO_COL.Substring(0, 18) + "..." : tb03.NO_COL,
                                 tb03.DT_INIC_ATIV_COL,
                                 //CO_SEXO_COL = tb03.CO_SEXO_COL == "F" ? "Feminino" : "Masculino",
                                 //CATEG = tb03.FLA_PROFESSOR == "S" ? "Prof. Saúde" : tb03.FLA_PROFESSOR == "N" ? "Funcionário" : tb03.FLA_PROFESSOR == "P" ? "Pres. Servi." : tb03.FLA_PROFESSOR == "E" ? "Estagiário" : "Outros",
                                 CO_CLASS = tb03.CO_CLASS_PROFI == "A" ? "Avaliador(a)" : tb03.CO_CLASS_PROFI == "E" ? "Enfermeiro(a)" :
                                 tb03.CO_CLASS_PROFI == "M" ? "Médico(a)" : tb03.CO_CLASS_PROFI == "D" ? "Odontólogo(a)" : tb03.CO_CLASS_PROFI == "S" ? "Esteticista" :
                                 tb03.CO_CLASS_PROFI == "P" ? "Psicólogo(a)" : tb03.CO_CLASS_PROFI == "I" ? "Fisioterapeuta" : tb03.CO_CLASS_PROFI == "F" ? "Fonoaudiólogo(a)" :
                                  tb03.CO_CLASS_PROFI == "T" ? "Terapeuta Ocupacional" : tb03.CO_CLASS_PROFI == "N" ? "Nutricionista" : tb03.CO_CLASS_PROFI == "O" ? "Outros" : "",
                                 DE_FUNC_COL = tb03.DE_FUNC_COL.Length > 25 ? tb03.DE_FUNC_COL.Substring(0, 18) + "..." : tb03.DE_FUNC_COL,
                                 //Sigla
                                  SIT = tb03.CO_SITU_COL,
                                // SIT = tb03.CO_SITU_COL == "ATI" ? "Atividade Interna" : tb03.CO_SITU_COL == "ATE" ? "Atividade Externa" : tb03.CO_SITU_COL == "FCE" ? "Cedido" :
                                // tb03.CO_SITU_COL == "FES" ? "Estagiário" : tb03.CO_SITU_COL == "LFR" ? "Licença Funcional" : tb03.CO_SITU_COL == "LME" ? "Licença Médica" : tb03.CO_SITU_COL == "LMA" ? "Licença Maternidade" :
                                //tb03.CO_SITU_COL == "SUS" ? "Suspenso" : tb03.CO_SITU_COL == "TRE" ? "Treinamento" : "Férias",
                                 //NU_CPF_COL = tb03.NU_CPF_COL.Insert(9, "-").Insert(6, ".").Insert(3, ".")
                             }).OrderBy(c => c.NO_COL);
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