//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: *******************
// SUBMÓDULO: ****************
// OBJETIVO: *****************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1904_EspecialidadeFuncional
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

            CarregaEspecialidade();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ESPECIALIDADE", "CO_ESPEC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_ESPEC",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_ESPEC",
                HeaderText = "Curso"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ESPECIALIDADE",
                HeaderText = "Especialidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SIGLA_ESPECIALIDADE",
                HeaderText = "Sigla"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEspec = ddlCurso.SelectedValue != "" ? int.Parse(ddlCurso.SelectedValue) : 0;

            var resultado = (from tb100 in TB100_ESPECIALIZACAO.RetornaTodosRegistros().AsEnumerable()
                              join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros().AsEnumerable() on tb100.CO_ESPEC equals tb63.CO_ESPEC
                              where (tb100.TP_ESPEC.Equals("GR"))
                              && (coEspec != 0 ? tb100.CO_ESPEC == coEspec : coEspec == 0)
                              && (txtDescricao.Text != "" ? tb63.NO_ESPECIALIDADE.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                              && tb63.CO_EMP == LoginAuxili.CO_EMP
                              select new
                              {
                                  tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE, tb63.NO_SIGLA_ESPECIALIDADE, tb100.DE_ESPEC, tb100.TP_ESPEC, tb100.CO_ESPEC
                              }).OrderBy( e => e.TP_ESPEC ).OrderBy( e => e.NO_ESPECIALIDADE ).ToList();

            var resultado2 = (from result2 in resultado
                              select new
                              {
                                  result2.CO_ESPECIALIDADE, result2.NO_ESPECIALIDADE, result2.NO_SIGLA_ESPECIALIDADE,
                                  result2.DE_ESPEC, result2.CO_ESPEC,
                                  TP_ESPEC = (EnumAuxili.GetEnum<ClassificacaoCurso>(result2.TP_ESPEC)).GetValue()                                  
                              }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ESPECIALIDADE"));
            queryStringKeys.Add(new KeyValuePair<string, string>("espec", "CO_ESPEC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Cursos
        private void CarregaEspecialidade()
        {
            ddlCurso.DataSource = (from tb100 in TB100_ESPECIALIZACAO.RetornaTodosRegistros().Where(t => t.TP_ESPEC.Equals("GR"))
                                   select new { tb100.CO_ESPEC, tb100.DE_ESPEC }).OrderBy( e => e.DE_ESPEC );

            ddlCurso.DataTextField = "DE_ESPEC";
            ddlCurso.DataValueField = "CO_ESPEC";
            ddlCurso.DataBind();

            ddlCurso.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaEspecialidade();
        }
    }
}