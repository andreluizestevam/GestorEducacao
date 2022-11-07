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

///Início das Regras de Negócios
///Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1117_CadastramentoSubGrupo2CtaContabil
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaTipo();
                CarregaGrupo();
                CarregaSubGrupo();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_SGRUP2_CTA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_GRUP_CTA",
                HeaderText = "TIPO CONTA"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NR_GRUP_CTA",
                HeaderText = "CÓD"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_GRUP_CTA",
                HeaderText = "GRUPO"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NR_SGRUP_CTA",
                HeaderText = "CÓD"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SGRUP_CTA",
                HeaderText = "SUBGRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NR_SGRUP2_CTA",
                HeaderText = "CÓD"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SGRUP2_CTA",
                HeaderText = "SUBGRUPO2"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string tipo = ddlTipoConta.SelectedValue != "" ? ddlTipoConta.SelectedValue : "-1";
            int grupo = (ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : -1);
            int sgrupo = (ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : -1);

            var resultado = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                             where (tipo != "-1" ? tb055.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == tipo : 0 == 0)
                             && (grupo != -1 ? tb055.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == grupo : 0 == 0)
                             && (sgrupo != -1 ? tb055.TB54_SGRP_CTA.CO_GRUP_CTA == sgrupo : 0 == 0)
                             && (txtSubGrupo2.Text.Trim() != "" ? tb055.DE_SGRUP2_CTA.Contains(txtSubGrupo2.Text) : 0 == 0)
                             select new listaSubGrupo2
                             {
                                 DE_SGRUP_CTA = tb055.TB54_SGRP_CTA.DE_SGRUP_CTA,
                                 DE_GRUP_CTA = tb055.TB54_SGRP_CTA.TB53_GRP_CTA.DE_GRUP_CTA,
                                 NR_GRUP_CTA = tb055.TB54_SGRP_CTA.TB53_GRP_CTA.NR_GRUP_CTA,
                                 NR_SGRUP_CTA = tb055.TB54_SGRP_CTA.NR_SGRUP_CTA,
                                 CO_TP_GRUP = tb055.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA,
                                 CO_SGRUP2_CTA = tb055.CO_SGRUP2_CTA,
                                 DE_SGRUP2_CTA = tb055.DE_SGRUP2_CTA,
                                 NR_SGRUP2_CTA = tb055.NR_SGRUP2_CTA
                             }).OrderBy(g => g.NR_GRUP_CTA).ThenBy(g => g.NR_SGRUP_CTA).ThenBy(g => g.NR_SGRUP2_CTA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SGRUP2_CTA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento
        /// <summary>
        /// Carrega o tipo de conta contábil do sistema
        /// </summary>
        private void CarregaTipo()
        {
            ddlTipoConta.Items.Clear();
            ddlTipoConta.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoContaCatabil.ResourceManager, todos:true));
        }
        /// <summary>
        /// Carrega os grupos contabeis do sistema pelo o tipo
        /// </summary>
        private void CarregaGrupo()
        {
            string tipo = ddlTipoConta.SelectedValue != "" ? ddlTipoConta.SelectedValue : "-1";

            ddlGrupo.Items.Clear();
            ddlGrupo.DataSource = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                         where (tipo != "-1" ? tb53.TP_GRUP_CTA == tipo : 0 == 0)
                         select new
                         {
                             tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA
                         });

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlGrupo.SelectedValue = "-1";
        }
        /// <summary>
        /// Carrega todos os subgrupos por grupo do sistema
        /// </summary>
        private void CarregaSubGrupo()
        {
            string tipo = ddlTipoConta.SelectedValue != "" ? ddlTipoConta.SelectedValue : "-1";
            int grupo = (ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : -1);

            ddlSubGrupo.Items.Clear();
            ddlSubGrupo.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                          where (grupo != -1 ? tb54.CO_GRUP_CTA == grupo : 0 == 0)
                          select new
                          {
                              tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA
                          });

            ddlSubGrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0,  new ListItem("Todos","-1"));
            ddlSubGrupo.SelectedValue = "-1";
        }

        #endregion

        #region Classes
        private class listaSubGrupo2
        {
            public string DE_SGRUP_CTA { get; set; }
            public string DE_GRUP_CTA { get; set; }
            public int? NR_GRUP_CTA { get; set; }
            public int? NR_SGRUP_CTA { get; set; }
            public string CO_TP_GRUP 
            {
                set
                {
                    if (tipoConta.Where(f => f.Key == value).DefaultIfEmpty() != null)
                        this.TP_GRUP_CTA = tipoConta[value];
                    else
                        this.TP_GRUP_CTA = "Nenhum";
                }
            }
            public string TP_GRUP_CTA { get; set; } 
            public int CO_SGRUP2_CTA { get; set; } 
            public string DE_SGRUP2_CTA { get; set; }
            public int? NR_SGRUP2_CTA { get; set; }
        }
        #endregion

        #region Eventos dos componentes da página
        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaGrupo();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSubGrupo();
        }
        #endregion
    }
}
