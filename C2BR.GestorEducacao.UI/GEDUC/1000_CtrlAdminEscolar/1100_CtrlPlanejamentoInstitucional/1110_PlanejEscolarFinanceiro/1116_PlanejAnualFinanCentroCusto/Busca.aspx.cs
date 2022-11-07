//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: PLANEJAMENTO ANUAL DE FINANCEIRO POR CENTRO DE CUSTO
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1116_PlanejAnualFinanCentroCusto
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

            CarregaAnos();
            CarregaTipo();
            CarregaDepartamento();
            CarregaSubGrupoCContabil();
            ddlCCusto.Items.Insert(0, new ListItem("Todos", ""));
            ddlCtaContabil.Items.Insert(0, new ListItem("Todos", ""));
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ANO_REF", "CO_SEQU_PC", "CO_GRUP_CTA", "CO_CENT_CUSTO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_REF",
                HeaderText = "Ano"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_DEPTO",
                HeaderText = "Depto"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CENT_CUSTO",
                HeaderText = "Centro Custo"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
               DataField = "DE_GRUP_CTA",
               HeaderText = "Tipo"
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
            int coCentCusto = ddlCCusto.SelectedValue != "" ? int.Parse(ddlCCusto.SelectedValue) : 0;
            int coGrpCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;
            int coSequPc = ddlCtaContabil.SelectedValue != "" ? int.Parse(ddlCtaContabil.SelectedValue) : 0;
            int anoRefer = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            var resultado = (from tb112 in TB112_PLANCUSTO.RetornaTodosRegistros()
                             where (anoRefer != 0 ? tb112.CO_ANO_REF == anoRefer : anoRefer == 0)
                             && (coCentCusto != 0 ? tb112.TB099_CENTRO_CUSTO.CO_CENT_CUSTO == coCentCusto : coCentCusto == 0)
                             && (coGrpCta != 0 ? tb112.TB53_GRP_CTA.CO_GRUP_CTA == coGrpCta : coGrpCta == 0)
                             && (coSequPc != 0 ? tb112.TB56_PLANOCTA.CO_SEQU_PC == coSequPc : coSequPc == 0)
                             && tb112.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                tb112.CO_ANO_REF, tb112.TB099_CENTRO_CUSTO.DE_CENT_CUSTO, tb112.TB099_CENTRO_CUSTO.TB14_DEPTO.NO_DEPTO,
                                tb112.TB53_GRP_CTA.DE_GRUP_CTA, tb112.TB56_PLANOCTA.DE_CONTA_PC, tb112.CO_CENT_CUSTO, tb112.CO_SEQU_PC, tb112.CO_GRUP_CTA,
                                Planejado = (tb112.VL_PLAN_MES1 + tb112.VL_PLAN_MES2 + tb112.VL_PLAN_MES3 + tb112.VL_PLAN_MES4 + tb112.VL_PLAN_MES5 + tb112.VL_PLAN_MES6 + tb112.VL_PLAN_MES7 + tb112.VL_PLAN_MES8 + tb112.VL_PLAN_MES9 + tb112.VL_PLAN_MES10 + tb112.VL_PLAN_MES11 + tb112.VL_PLAN_MES12),
                                Realizado = (tb112.VL_REAL_MES_1 + tb112.VL_REAL_MES_2 + tb112.VL_REAL_MES_3 + tb112.VL_REAL_MES_4 + tb112.VL_REAL_MES_5 + tb112.VL_REAL_MES_6 + tb112.VL_REAL_MES_7 + tb112.VL_REAL_MES_8 + tb112.VL_REAL_MES_9 + tb112.VL_REAL_MES_10 + tb112.VL_REAL_MES_11 + tb112.VL_REAL_MES_12)
                             }).OrderBy( p => p.CO_ANO_REF ).ThenBy( p => p.NO_DEPTO ).ThenBy( p => p.DE_CONTA_PC );

            CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("cCusto", "CO_CENT_CUSTO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("cContabil", "CO_SEQU_PC"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tipo", "CO_GRUP_CTA"));
            queryStringKeys.Add(new KeyValuePair<string, string>("ano", "CO_ANO_REF"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Anos de Referência
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb112 in TB112_PLANCUSTO.RetornaTodosRegistros()
                                 where tb112.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb112.CO_ANO_REF }).Distinct().OrderByDescending( p => p.CO_ANO_REF );

            ddlAno.DataTextField = "CO_ANO_REF";
            ddlAno.DataValueField = "CO_ANO_REF";
            ddlAno.DataBind();
        }


//====> Método que carrega o DropDown de Centro de Custo
        private void CarregaCCusto()
        {
            int coDepto = ddlDepto.SelectedValue != "" ? int.Parse(ddlDepto.SelectedValue) : 0;

            ddlCCusto.DataSource = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                                    where tb099.TB14_DEPTO.CO_DEPTO == coDepto                                    
                                    select new { tb099.CO_CENT_CUSTO, tb099.DE_CENT_CUSTO }).OrderBy(c => c.DE_CENT_CUSTO);

            ddlCCusto.DataTextField = "DE_CENT_CUSTO";
            ddlCCusto.DataValueField = "CO_CENT_CUSTO";
            ddlCCusto.DataBind();

            ddlCCusto.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Tipos de Conta
        private void CarregaTipo()
        {
            ddlTipo.DataSource = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                  where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA }).OrderBy(s => s.DE_GRUP_CTA);

            ddlTipo.DataTextField = "DE_GRUP_CTA";
            ddlTipo.DataValueField = "CO_GRUP_CTA";
            ddlTipo.DataBind();
        }

//====> Método que carrega o DropDown de SubGrupo de Conta
        private void CarregaSubGrupoCContabil()
        {
            int coGrpCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;

            ddlSubGrupoConta.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                                              where tb54.CO_GRUP_CTA == coGrpCta
                                              select new { tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA });

            ddlSubGrupoConta.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupoConta.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupoConta.DataBind();

            ddlSubGrupoConta.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Contas Contábeis
        private void CarregaCContabil()
        {
            int coSGrpCta = ddlSubGrupoConta.SelectedValue != "" ? int.Parse(ddlSubGrupoConta.SelectedValue) : 0;

            ddlCtaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                         where tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrpCta
                                         select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy( p => p.DE_CONTA_PC );

            ddlCtaContabil.DataTextField = "DE_CONTA_PC";
            ddlCtaContabil.DataValueField = "CO_SEQU_PC";
            ddlCtaContabil.DataBind();

            ddlCtaContabil.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Departamento
        private void CarregaDepartamento()
        {
            ddlDepto.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                   where tb14.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                   select new { tb14.CO_DEPTO, tb14.NO_DEPTO });

            ddlDepto.DataTextField = "NO_DEPTO";
            ddlDepto.DataValueField = "CO_DEPTO";
            ddlDepto.DataBind();

            ddlDepto.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupoCContabil();
        }

        protected void ddlSubGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCContabil();
        }

        protected void ddlDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCCusto();
        }
    }
}
