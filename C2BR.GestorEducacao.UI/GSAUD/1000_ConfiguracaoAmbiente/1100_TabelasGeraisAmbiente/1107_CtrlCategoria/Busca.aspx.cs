//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE TIPO DE PERFIL DE ACESSO.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 09/12/2014| Maxwell Almeida           | Criação da funcionalidade Busca por Operadoras

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


namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1107_CtrlCategoria
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
                CarregaOperadorasPlanSaude();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_CATEG_PLANO_SAUDE" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SIGLA_OPER",
                HeaderText = "Operadora",
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SIGLA_PLAN",
                HeaderText = "Plano",
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_CATEG",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SIGLA_CATEG",
                HeaderText = "Sigla"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_SITUA",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int IdOperadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
            int IdPlano = ddlPlano.SelectedValue != "" ? int.Parse(ddlPlano.SelectedValue) : 0;
            var resultado = (from tb251 in TB367_CATEG_PLANO_SAUDE.RetornaTodosRegistros()
                             where (txtNomeCategoria.Text != "" ? tb251.NM_CATEG.Contains(txtNomeCategoria.Text) : "" == "")
                             && (txtSigla.Text != "" ? tb251.NM_SIGLA_CATEG.Equals(txtSigla.Text) : ""== "")
                               && (ddlSitu.SelectedValue != "0" ? tb251.FL_SITUA == ddlSitu.SelectedValue : "0" == "0")
                             && (IdOperadora != 0 ? tb251.TB250_OPERA.ID_OPER == IdOperadora : 0 == 0)
                             && (IdPlano != 0 ? tb251.TB251_PLANO_OPERA.ID_PLAN == IdPlano : 0 == 0)
                             select new
                             {
                                 ID_CATEG_PLANO_SAUDE = tb251.ID_CATEG_PLANO_SAUDE,
                                 NM_CATEG = tb251.NM_CATEG == null? " - " : tb251.NM_CATEG,
                                 NM_SIGLA_CATEG = tb251.NM_SIGLA_CATEG == null ? " - " : tb251.NM_SIGLA_CATEG,
                                 FL_SITUA = tb251.FL_SITUA == "A" ? "Ativo" : tb251.FL_SITUA == "I" ? "Inativo" : tb251.FL_SITUA == "S" ? "Suspenso" : " - ",
                                 NM_SIGLA_OPER = tb251.TB250_OPERA.NOM_OPER == null ? " - " : tb251.TB250_OPERA.NOM_OPER,
                                 NM_SIGLA_PLAN = tb251.TB251_PLANO_OPERA.NM_SIGLA_PLAN == null ? " - " : tb251.TB251_PLANO_OPERA.NM_SIGLA_PLAN,
                             }).OrderBy(e => e.NM_CATEG);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_CATEG_PLANO_SAUDE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        private void CarregaOperadorasPlanSaude()
        {
            try
            {

                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }


        }


        private void CarregaPlanosSaude()
        {
            try
            {

                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, true);

            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }


        }

        protected void ddOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlanosSaude();
        }

        #endregion
    }
}