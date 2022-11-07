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

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1106_CtrlPlanos
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_PLAN" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOM_OPER",
                HeaderText = "Operadora",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOM_PLAN",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
       {
           DataField = "NM_SIGLA_PLAN",
           HeaderText = "Sigla"
       });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_SITU_PLAN",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {
                int IdOperadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;

                var resultado = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                                 where (txtNomePlano.Text != "" ? tb251.NOM_PLAN.Contains(txtNomePlano.Text) : txtNomePlano.Text == "")
                                 && (txtSigla.Text != "" ? tb251.NM_SIGLA_PLAN.Equals(txtSigla.Text) : txtSigla.Text == "")
                                 && (ddlOperadora.SelectedValue != "0" ? tb251.TB250_OPERA.ID_OPER == IdOperadora : 0 == 0)
                                 && (ddlSitu.SelectedValue != "0" ? tb251.ST_PLAN == ddlSitu.SelectedValue : "" == "")
                                 select new
                                 {
                                     tb251.ID_PLAN,
                                     NOM_PLAN = tb251.NOM_PLAN,
                                     NM_SIGLA_PLAN = tb251.NOM_PLAN == null ? " - " : tb251.NOM_PLAN,
                                     FL_SITU_PLAN = tb251.FL_SITU_PLAN == "A" ? "Ativo" : tb251.FL_SITU_PLAN == "I" ? "Inativo" : tb251.FL_SITU_PLAN == "S" ? "Suspenso" : " - ",
                                     NOM_OPER = tb251.TB250_OPERA.NOM_OPER
                                 }).OrderBy(e => e.NOM_PLAN);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_PLAN"));

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

        #endregion
    }
}