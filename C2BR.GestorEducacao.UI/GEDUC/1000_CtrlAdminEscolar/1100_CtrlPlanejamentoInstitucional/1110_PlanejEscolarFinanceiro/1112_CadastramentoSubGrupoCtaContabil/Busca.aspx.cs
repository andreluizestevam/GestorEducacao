//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE SUBGRUPO DE CONTAS CONTÁBIL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

///Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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

///Início das Regras de Negócios
///Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1112_CadastramentoSubGrupoCtaContabil
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }
        private static Dictionary<string, string> tipoConta = AuxiliBaseApoio.chave(tipoContaCatabil.ResourceManager, true);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                CarregarTipo();
                CarregarGrupos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_SGRUP_CTA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_GRUP_CTA",
                HeaderText = "TIPO CONTA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NR_GRUP_CTA",
                HeaderText = "CÓDIGO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_GRUP_CTA",
                HeaderText = "GRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NR_SGRUP_CTA",
                HeaderText = "CÓDIGO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SGRUP_CTA",
                HeaderText = "SUBGRUPO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string tipo = ddlTipo.SelectedValue != "" ? ddlTipo.SelectedValue : "-1";
            int grupo = (ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : -1);
            string nomeSub = (txtSubGrupo.Text.Trim() != "" ? txtSubGrupo.Text : "-1");
            var resultado = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                             where (tipo != "-1" ? tb54.TB53_GRP_CTA.TP_GRUP_CTA == tipo : 0 == 0)
                             && (grupo == -1 ? 0==0 : tb54.TB53_GRP_CTA.CO_GRUP_CTA == grupo)
                             && (nomeSub != "-1" ? tb54.DE_SGRUP_CTA.Contains(txtSubGrupo.Text) : 0 == 0)
                             && tb54.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             orderby tb54.TB53_GRP_CTA.TP_GRUP_CTA, tb54.TB53_GRP_CTA.CO_GRUP_CTA, tb54.CO_GRUP_CTA
                             select new listaSubGrupo
                             {
                                DE_SGRUP_CTA = tb54.DE_SGRUP_CTA,
                                DE_GRUP_CTA = tb54.TB53_GRP_CTA.DE_GRUP_CTA,
                                CO_SGRUP_CTA = tb54.CO_SGRUP_CTA,
                                NR_GRUP_CTA = tb54.TB53_GRP_CTA.NR_GRUP_CTA,
                                NR_SGRUP_CTA = tb54.NR_SGRUP_CTA,
                                CO_TP_GRUP = tb54.TB53_GRP_CTA.TP_GRUP_CTA
                             });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SGRUP_CTA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown
        /// <summary>
        /// Carrega os tipos de grupos para filtro
        /// </summary>
        private void CarregarTipo()
        {
            ddlTipo.Items.Clear();
            ddlTipo.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoContaCatabil.ResourceManager, todos:true));
        }
        /// <summary>
        /// Carrega os grupos de acordo com os tipos selecionados
        /// </summary>
        private void CarregarGrupos()
        {
            string tipoGrupo = (ddlTipo.SelectedValue != "" ? ddlTipo.SelectedValue : "-1");
            ddlGrupo.Items.Clear();
            ddlGrupo.DataSource = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                   where (tipoGrupo == "-1" ? 0 == 0 : tb53.TP_GRUP_CTA == tipoGrupo)
                                   orderby tb53.TP_GRUP_CTA, tb53.NR_GRUP_CTA
                                   select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA});
            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();
            ddlGrupo.Items.Insert(0, new ListItem("Todos","-1"));
        }
        #endregion

        #region Eventos de componentes
        /// <summary>
        /// Ao trocar a seleção verificar se diferente de "" e realiza o carregamento dos grupos
        /// </summary>
        /// <param name="sender">Objeto</param>
        /// <param name="e">Evento</param>
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarGrupos();
        }
        #endregion

        #region Classes
        /// <summary>
        /// Classe para personalizar a lista de grupos do filtro
        /// </summary>
        private class listaSubGrupo
        {
            public string CO_TP_GRUP 
            {
                set
                {
                    if(tipoConta.Where(f => f.Key == value).DefaultIfEmpty() != null)
                        this.TP_GRUP_CTA = tipoConta[value];
                    else
                        this.TP_GRUP_CTA = "Nenhum";
                }
            }
            public string TP_GRUP_CTA { get; set; }
            public string DE_SGRUP_CTA { get; set; } 
            public string DE_GRUP_CTA { get; set; } 
            public int CO_SGRUP_CTA { get; set; }
            public int? NR_GRUP_CTA { get; set; }
            public int? NR_SGRUP_CTA { get; set; }
        }
        #endregion
    }
}
