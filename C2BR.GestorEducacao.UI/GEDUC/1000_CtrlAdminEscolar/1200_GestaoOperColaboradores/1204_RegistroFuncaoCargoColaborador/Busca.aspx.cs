//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: ASSOCIAÇÃO DE FUNÇÕES/CARGOS FUNCIONAIS A COLABORADORES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1204_RegistroFuncaoCargoColaborador
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
            CarregaUnidade();
            CarregaColaborador();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_GESTOR_UNIDAD" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Colaborador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_DEPTO",
                HeaderText = "Depto"
            });

             CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FUN",
                HeaderText = "Função"
            });
        
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_INICIO_ATIVID",
                HeaderText = "Dt Inicio",
                DataFormatString = "{0:d}"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU_GEST",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != ""? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != ""? int.Parse(ddlColaborador.SelectedValue) : 0;
            
            var resultado = (from tb59 in TB59_GESTOR_UNIDAD.RetornaTodosRegistros()
                            join tb134 in TB134_USR_EMP.RetornaTodosRegistros() 
                             on tb59.TB25_EMPRESA.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                            where tb59.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            && (coCol != 0 ? tb59.TB03_COLABOR.CO_COL == coCol : coCol == 0)
                            && (coEmp != 0 ? tb59.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                            && tb134.ADMUSUARIO.CodUsuario == LoginAuxili.CO_COL
                            select new
                            {
                                tb59.TB03_COLABOR.NO_COL, tb59.TB25_EMPRESA.sigla, tb59.DT_INICIO_ATIVID,
                                tb59.DT_TERMIN_ATIVID, tb59.TB14_DEPTO.NO_DEPTO, tb59.IDE_GESTOR_UNIDAD,
                                CO_SITU_GEST = tb59.CO_SITU_GEST == "A" ? "Ativo" : tb59.CO_SITU_GEST == "I" ? "Inativo" : "Cancelado", tb59.TB15_FUNCAO.NO_FUN
                            }).Distinct().OrderByDescending(p => p.DT_INICIO_ATIVID);

            CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_GESTOR_UNIDAD"));
                        
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Colaboradores
        private void CarregaColaborador()
        {
            int coEmp = ddlUnidade.SelectedValue != ""? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaborador();
        }
    }
}
