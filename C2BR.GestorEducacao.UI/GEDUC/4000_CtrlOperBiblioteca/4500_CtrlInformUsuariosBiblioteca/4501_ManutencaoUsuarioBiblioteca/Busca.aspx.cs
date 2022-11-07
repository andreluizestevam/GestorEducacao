//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: MANUTENÇÃO DE USUÁRIOS DE BIBLIOTECA
// OBJETIVO: MANUTENÇÃO DE USUÁRIO DE BIBLIOTECA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4500_CtrlInformUsuariosBiblioteca.F4501_ManutencaoUsuarioBiblioteca
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

        protected void Page_Load()
        {
            if (IsPostBack) return;

            CarregaUnidades();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_USUARIO_BIBLIOT" };
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_USU_BIB",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TIPO",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NUMERO_CONTROLE",
                HeaderText = "N° Ctrl"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "UNIDADE",
                HeaderText = "Unidade"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;    
            
            var resultado = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                            where (txtNome.Text != "" ? tb205.NO_USU_BIB.Contains(txtNome.Text) : txtNome.Text == "")
                            && (ddlTipo.SelectedValue != "" ? tb205.TP_USU_BIB == ddlTipo.SelectedValue : ddlTipo.SelectedValue == "")
                            && (coEmp != 0 ? (tb205.TB03_COLABOR != null && tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp) ||
                            (tb205.TB07_ALUNO != null && tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp) : coEmp == 0)
                            && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new
                            {
                                UNIDADE = tb205.TP_USU_BIB == "A" ? (int?)tb205.TB07_ALUNO.CO_EMP : (tb205.TP_USU_BIB == "P" || tb205.TP_USU_BIB == "F" ? (int?)tb205.TB03_COLABOR.CO_EMP : null),
                                tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB, tb205.ORG_CODIGO_ORGAO,
                                TIPO = tb205.TP_USU_BIB == "A" ? "Aluno" : (tb205.TP_USU_BIB == "P" ? "Professor" : (tb205.TP_USU_BIB == "F" ? "Funcionário" : "Outros")),
                                
                            }).OrderBy(r => r.NO_USU_BIB).ThenBy(r => r.TIPO).ToList();

            var resultado2 = (from result in resultado
                              select new
                              {
                                  UNIDADE = result.UNIDADE.HasValue ? TB25_EMPRESA.RetornaPelaChavePrimaria(result.UNIDADE.Value).NO_FANTAS_EMP : "",
                                  NUMERO_CONTROLE = result.ORG_CODIGO_ORGAO.ToString("000") + "." + result.CO_USUARIO_BIBLIOT.ToString("000000"),
                                  result.CO_USUARIO_BIBLIOT, result.NO_USU_BIB, result.TIPO
                              }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_USUARIO_BIBLIOT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion
    }
}
