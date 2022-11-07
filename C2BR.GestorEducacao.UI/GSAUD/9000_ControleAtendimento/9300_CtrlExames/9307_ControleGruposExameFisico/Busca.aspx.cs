//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: CADASTRAMENTO DE GRUPOS DE PESQUISAS INSTITUCIONAIS
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9307_ControleGruposExameFisico
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
            {
                CarregaEspecialidade();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_GRUPO_FISIC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_GRUPO_FISIC",
                HeaderText = " Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ESPECIALIDADE",
                HeaderText = " Especialidade"
            });

             CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_SITUA_GRUPO_FISIC",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var idEspec = !String.IsNullOrEmpty(ddlEspec.SelectedValue) ? int.Parse(ddlEspec.SelectedValue) : 0;

            var resultado = (from tbs431 in TBS431_GRUPO_EXAME_FISIC.RetornaTodosRegistros()
                             join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs431.CO_ESPECIALIDADE equals tb63.CO_ESPECIALIDADE
                             where (!String.IsNullOrEmpty(txtGrupo.Text) ? tbs431.NO_GRUPO_FISIC.Contains(txtGrupo.Text) : true)
                             && (idEspec != 0 ? tb63.CO_ESPECIALIDADE == idEspec : true)
                             && (!String.IsNullOrEmpty(ddlSitu.SelectedValue) ? tbs431.FL_SITUA_GRUPO_FISIC == ddlSitu.SelectedValue : true)
                             select new 
                             {
                                 tbs431.ID_GRUPO_FISIC,
                                 tbs431.NO_GRUPO_FISIC,
                                 tb63.NO_ESPECIALIDADE,
                                 FL_SITUA_GRUPO_FISIC = tbs431.FL_SITUA_GRUPO_FISIC.Equals("A") ? "Ativo" : "Inativo"
                             }).OrderBy(t => t.NO_GRUPO_FISIC);

            CurrentPadraoBuscas.GridBusca.DataSource = resultado ;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_GRUPO_FISIC"));
            
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento

        public void CarregaEspecialidade()
        {
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, LoginAuxili.CO_EMP, 0, true);
        }

        #endregion

        #region Métodos


        #endregion
    }
}
