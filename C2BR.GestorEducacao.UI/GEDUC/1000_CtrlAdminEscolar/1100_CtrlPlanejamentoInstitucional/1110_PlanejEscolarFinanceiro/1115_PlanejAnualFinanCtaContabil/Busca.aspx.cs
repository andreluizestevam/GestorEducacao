//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: PLANEJAMENTO ANUAL DE FINANCEIRO POR CONTA CONTÁBIL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1115_PlanejAnualFinanCtaContabil
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

        void Page_Load()
        {
            if (IsPostBack) return;
            CarregaTipo();
            CarregaSub();
            CarregaSub2();
            CarregaConta();
            CarregaAnos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_SEQU_PC", "CO_ANO_REF", "CO_SGRUP_CTA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_REF",
                HeaderText = "Ano"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CONTA_PC",
                HeaderText = "Conta"
            });

            BoundField bfPlanejado = new BoundField();
            bfPlanejado.DataField = "Planejado";
            bfPlanejado.HeaderText = "Planejado";
            bfPlanejado.ItemStyle.CssClass = "numeroCol";
            bfPlanejado.DataFormatString = "{0:N2}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfPlanejado);

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "Realizado";
            bfRealizado.HeaderText = "Realizado";
            bfRealizado.ItemStyle.CssClass = "numeroCol";
            bfPlanejado.DataFormatString = "{0:N2}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coSequPc = ddlContaContabil.SelectedValue != "" ? int.Parse(ddlContaContabil.SelectedValue) : 0;
            int coGrupCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coSGrupCta2 = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : 0;
            int coAnoRef = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            
            var resultado = (from tb111 in TB111_PLANEJ_FINAN.RetornaTodosRegistros()
                             where tb111.CO_ANO_REF == coAnoRef && tb111.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && ( coGrupCta != 0 ? tb111.TB56_PLANOCTA.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta : coGrupCta == 0)
                             && (coSGrupCta != 0 ? tb111.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta : coSGrupCta == 0)
                             && (coSGrupCta2 != 0 ? tb111.TB56_PLANOCTA.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSGrupCta2 : coSGrupCta2 == 0)
                             select new 
                             {
                                tb111.CO_ANO_REF, tb111.TB56_PLANOCTA.CO_SEQU_PC, tb111.TB56_PLANOCTA.DE_CONTA_PC, tb111.TB56_PLANOCTA.TB54_SGRP_CTA.CO_SGRUP_CTA,
                                Planejado = (tb111.VL_PLAN_MES1 + tb111.VL_PLAN_MES2 + tb111.VL_PLAN_MES3 + tb111.VL_PLAN_MES4 + tb111.VL_PLAN_MES5 + tb111.VL_PLAN_MES6 + tb111.VL_PLAN_MES7 + tb111.VL_PLAN_MES8 + tb111.VL_PLAN_MES9 + tb111.VL_PLAN_MES10 + tb111.VL_PLAN_MES11 + tb111.VL_PLAN_MES12),
                                Realizado = (tb111.VL_REAL_MES_1 + tb111.VL_REAL_MES_2 + tb111.VL_REAL_MES_3 + tb111.VL_REAL_MES_4 + tb111.VL_REAL_MES_5 + tb111.VL_REAL_MES_6 + tb111.VL_REAL_MES_7 + tb111.VL_REAL_MES_8 + tb111.VL_REAL_MES_9 + tb111.VL_REAL_MES_10 + tb111.VL_REAL_MES_11 + tb111.VL_REAL_MES_12)                                    
                             }).OrderBy( p => p.CO_ANO_REF ).ThenBy( p => p.DE_CONTA_PC );
                               
            CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SEQU_PC"));
            queryStringKeys.Add(new KeyValuePair<string, string>("grupo", "CO_SGRUP_CTA"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_REF"));
            
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion     

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Tipo de Contas
        private void CarregaTipo()
        {
            ddlTipo.DataSource = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                 where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA }).OrderBy( s => s.DE_GRUP_CTA);

            ddlTipo.DataTextField = "DE_GRUP_CTA";
            ddlTipo.DataValueField = "CO_GRUP_CTA";
            ddlTipo.DataBind();
        }

//====> Método que carrega o DropDown de Anos de Referência
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb111 in TB111_PLANEJ_FINAN.RetornaTodosRegistros()
                                 where tb111.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb111.CO_ANO_REF }).Distinct().OrderByDescending( p => p.CO_ANO_REF );

            ddlAno.DataTextField = "CO_ANO_REF";
            ddlAno.DataValueField = "CO_ANO_REF";
            ddlAno.DataBind();
        }

//====> Método que carrega o DropDown de SubGrupo de Contas
        private void CarregaSub()
        {
            int coGrupCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;

            ddlSubGrupo.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                                      where tb54.CO_GRUP_CTA == coGrupCta
                                      select new { tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA });

            ddlSubGrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }

        //====> Método que carrega o DropDown de SubGrupo2 de Contas
        private void CarregaSub2()
        {
            int coGrupCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            ddlSubGrupo2.DataSource = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                                       join tb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on tb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals tb54.CO_SGRUP_CTA
                                       where tb54.CO_GRUP_CTA == coGrupCta && tb54.CO_SGRUP_CTA == coSGrupCta
                                       select new { tb055.DE_SGRUP2_CTA, tb055.CO_SGRUP2_CTA });

            ddlSubGrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2.DataBind();

            ddlSubGrupo2.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Contas Contábeis
        private void CarregaConta()
        {
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coSGrup2Cta = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : 0;

            ddlContaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                           where (coSGrupCta != 0 ? tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta : coSGrupCta == 0)
                                           && (coSGrup2Cta != 0 ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSGrup2Cta : coSGrup2Cta == 0)
                                           select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy( p => p.DE_CONTA_PC );

            ddlContaContabil.DataTextField = "DE_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_SEQU_PC";
            ddlContaContabil.DataBind();

            ddlContaContabil.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSub();
        }

        protected void ddlSubGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSub2();
        }

        protected void ddlSubGrupo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta();
        }
    }
}
