//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE CONTAS CONTÁBIL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1113_CadastramentoCtaContabil
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
                CarregaSubgrupo();
                CarregaSubgrupo2();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_SEQU_PC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_GRUP_CTA",
                HeaderText = "TIPO CONTA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_GRUP_CTA",
                HeaderText = "GRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SGRUP_CTA",
                HeaderText = "SUBGRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SGRUP2_CTA",
                HeaderText = "SUBGRUPO2"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CONTA_PC",
                HeaderText = "CÓDIGO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CONTA_PC",
                HeaderText = "CONTA"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string tipo = ddlTipoConta.SelectedValue != "" ? ddlTipoConta.SelectedValue : "-1";
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : -1;
            int coSGrpCta = ddlSubgrupo.SelectedValue != "" ? int.Parse(ddlSubgrupo.SelectedValue) : -1;
            int coSGrp2Cta = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : -1;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             join tb55 in TB055_SGRP2_CTA.RetornaTodosRegistros() on tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA equals tb55.CO_SGRUP2_CTA into resultado1
                             from tb55 in resultado1.DefaultIfEmpty()
                             where (tipo != "-1" ? tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == tipo : 0 == 0)
                             && (coGrupCta != -1 ? tb56.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta : 0 == 0)
                             && (coSGrpCta != -1 ? tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrpCta : 0 == 0)
                             && (coSGrp2Cta != -1 ? tb55.CO_SGRUP2_CTA == coSGrp2Cta : 0 == 0)
                             && (txtConta.Text != "" ? tb56.DE_CONTA_PC.Contains(txtConta.Text) : txtConta.Text == "")
                             && tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new listaSubGrupo2
                             {
                                DE_GRUP_CTA = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.DE_GRUP_CTA,
                                DE_SGRUP_CTA = tb56.TB54_SGRP_CTA.DE_SGRUP_CTA,
                                DE_CONTA_PC = tb56.DE_CONTA_PC,
                                CO_SEQU_PC = tb56.CO_SEQU_PC,
                                NU_CONTA_PC = tb56.NU_CONTA_PC,
                                CO_TP_GRUP = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA,
                                DE_SGRUP2_CTA = tb55.DE_SGRUP2_CTA
                             }).OrderBy(p => p.DE_GRUP_CTA).ThenBy(p => p.DE_SGRUP_CTA).ThenBy(p => p.DE_SGRUP2_CTA).ThenBy(p => p.NU_CONTA_PC);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SEQU_PC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown
        /// <summary>
        /// Carrega todos os tipos de conta contabil do sistema
        /// </summary>
        private void CarregaTipo()
        {
            ddlTipoConta.Items.Clear();
            ddlTipoConta.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoContaCatabil.ResourceManager, todos:true));
        }

        /// <summary>
        /// Método que carrega o DropDown de Grupo de Contas
        /// </summary>
        private void CarregaGrupo()
        {
            string tipo = ddlTipoConta.SelectedValue != "" ? ddlTipoConta.SelectedValue : "-1";
            ddlGrupo.DataSource = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                   where (tipo != "-1" ? tb53.TP_GRUP_CTA == tipo : 0 == 0)
                                   && tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                   select new { tb53.DE_GRUP_CTA, tb53.CO_GRUP_CTA });

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        /// <summary>
        /// Método que carrega o DropDown de SubGrupo de Contas
        /// </summary>
        private void CarregaSubgrupo()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : -1;
            ddlSubgrupo.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                                      where (coGrupCta != -1 ? tb54.CO_GRUP_CTA == coGrupCta : 0 == 0)
                                      select new { tb54.DE_SGRUP_CTA, tb54.CO_SGRUP_CTA });

            ddlSubgrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubgrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubgrupo.DataBind();

            ddlSubgrupo.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        /// <summary>
        /// Método que carrega o DropDown de SubGrupo2 de Contas
        /// </summary>
        private void CarregaSubgrupo2()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : -1;
            int coSGrupCta = ddlSubgrupo.SelectedValue != "" ? int.Parse(ddlSubgrupo.SelectedValue) : -1;

            ddlSubGrupo2.DataSource = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                                       where (coGrupCta != -1 ? tb055.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta : 0 == 0)
                                       && (coSGrupCta != -1 ? tb055.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta : 0 == 0)
                                      select new { tb055.DE_SGRUP2_CTA, tb055.CO_SGRUP2_CTA });

            ddlSubGrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2.DataBind();

            ddlSubGrupo2.Items.Insert(0, new ListItem("Todos", "-1"));
        }
        #endregion

        #region Eventos do componentes da página

        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregaGrupo();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSubgrupo();
        }

        protected void ddlSubgrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSubgrupo2();
        }

        #endregion

        #region Classes
        private class listaSubGrupo2
        {
            public string DE_GRUP_CTA { get; set; }
            public string DE_SGRUP_CTA { get; set; }
            public string DE_CONTA_PC { get; set; } 
            public int CO_SEQU_PC { get; set; }
            public int? NU_CONTA_PC { get; set; }
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
            public string DE_SGRUP2_CTA { get; set; }
        }
        #endregion

    }
}