//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: GERA A PESQUISA INSTITUCIONAL
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
namespace C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1300_ServicosApoioAdministrativo._1330_CtrlPesquisaInstitucional._1334_GeraPesquisaInstSaude
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
            CarregaAvaliacao();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID" };

            BoundField bfNuAval = new BoundField();
            bfNuAval.DataField = "NU_AVAL_MASTER";
            bfNuAval.HeaderText = "N° Avaliação";
            bfNuAval.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfNuAval);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_TITU_AVAL",
                HeaderText = "Nome da Questão"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FLA_PUBLICO_ALVO",
                HeaderText = "PÚBLICO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "STATUS",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int anoStatus = txtAno.Text != "" ? int.Parse(txtAno.Text) : 0;

            var resultado = (from tb201 in TB201_AVAL_MASTER.RetornaTodosRegistros()
                             where (coTipoAval != 0 ? tb201.CO_TIPO_AVAL == coTipoAval : coTipoAval == 0)
                             && (coEmp != 0 ? tb201.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                             && (anoStatus != 0 ? tb201.DT_STATUS.Year == anoStatus : anoStatus == 0)
                             select new
                             {
                                 tb201.NU_AVAL_MASTER,
                                 tb201.DT_CADASTRO,
                                 STATUS = tb201.STATUS.Equals("A") ? "Ativo" : "Inativo",
                                 tb201.CO_TIPO_AVAL,
                                 tb201.TB25_EMPRESA.NO_FANTAS_EMP,
                                 tb201.NM_TITU_AVAL,
                                 FLA_PUBLICO_ALVO = tb201.FLA_PUBLICO_ALVO.Equals("A") ? "Pacientes" : tb201.FLA_PUBLICO_ALVO.Equals("P") ? "Professores" :
                                 tb201.FLA_PUBLICO_ALVO.Equals("R") ? "Responsáveis" : tb201.FLA_PUBLICO_ALVO.Equals("F") ? "Funcionários" : "Outros",
                             }).OrderBy(a => a.NO_FANTAS_EMP).ThenBy(a => a.NM_TITU_AVAL).ToList();

            var resultado2 = (from result2 in resultado
                              select new
                              {
                                  ID = result2.NU_AVAL_MASTER,
                                  result2.STATUS,
                                  result2.DT_CADASTRO,
                                  NU_AVAL_MASTER = result2.CO_TIPO_AVAL + "." + result2.DT_CADASTRO.Year + "." +
                                                   result2.DT_CADASTRO.Month + "." + result2.NU_AVAL_MASTER.ToString("0000"),
                                  result2.NO_FANTAS_EMP,
                                  result2.NM_TITU_AVAL,
                                  result2.FLA_PUBLICO_ALVO
                              }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = resultado2.Count() > 0 ? resultado2 : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //----> Método que carrega o dropdown de Unidade
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy(u => u.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));
        }

        //----> Método que carrega o dropdown de Avaliação
        private void CarregaAvaliacao()
        {
            ddlAvaliacao.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                       select new { tb73.CO_TIPO_AVAL, tb73.NO_TIPO_AVAL }).OrderBy(t => t.NO_TIPO_AVAL);

            ddlAvaliacao.DataTextField = "NO_TIPO_AVAL";
            ddlAvaliacao.DataValueField = "CO_TIPO_AVAL";
            ddlAvaliacao.DataBind();

            ddlAvaliacao.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}