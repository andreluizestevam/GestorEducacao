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
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1116_PlanejAnualFinanCentroCusto
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAno.Text = DateTime.Now.Year.ToString();
                CarregaDepartamento();
                CarregaGrupo();
                CarregaSubgrupo();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtAno.Enabled = ddlCContabil.Enabled = ddlCCusto.Enabled = ddlDepto.Enabled = ddlTipoConta.Enabled = ddlGrupo.Enabled = ddlSubgrupo.Enabled = true;

                if (QueryStringAuxili.RetornaQueryStringComoIntPelaChave("cCusto") > 0)
                    txtAno.Text = QueryStringAuxili.RetornaQueryStringPelaChave("ano");

            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB112_PLANCUSTO tb112 = RetornaEntidade();

            int coCentCusto = ddlCCusto.SelectedValue != "" ? int.Parse(ddlCCusto.SelectedValue) : 0;
            int coDepto = ddlDepto.SelectedValue != "" ? int.Parse(ddlDepto.SelectedValue) : 0;
            int coSequPc = ddlCContabil.SelectedValue != "" ? int.Parse(ddlCContabil.SelectedValue) : 0;
            int coGrpCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int anoRefer = int.Parse(txtAno.Text);
            decimal decimalRetorno = 0;

            int qtdRegistro = (from lTb112 in TB112_PLANCUSTO.RetornaTodosRegistros()
                               where lTb112.CO_ANO_REF == anoRefer && lTb112.CO_CENT_CUSTO == coCentCusto 
                               && lTb112.CO_EMP == LoginAuxili.CO_EMP && lTb112.TB56_PLANOCTA.CO_SEQU_PC == coSequPc
                               select new { lTb112.CO_SEQU_PC }).Count();


            if (qtdRegistro > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.EnvioMensagemErro(this, MensagensErro.DadosExistentes);
            else
            {
                if (tb112 == null)
                {
                    tb112 = new TB112_PLANCUSTO();

                    tb112.CO_ANO_REF = anoRefer;
                    tb112.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb112.TB099_CENTRO_CUSTO = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                                                where tb099.CO_CENT_CUSTO == coCentCusto && tb099.TB14_DEPTO.CO_DEPTO == coDepto
                                                select tb099).FirstOrDefault();

                    tb112.TB56_PLANOCTA = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                           where tb56.CO_SEQU_PC == coSequPc
                                           select tb56).FirstOrDefault();

                    tb112.TB53_GRP_CTA = TB53_GRP_CTA.RetornaPelaChavePrimaria(coGrpCta);
                }

                tb112.VL_PLAN_MES1 = decimal.TryParse(txtQtdeAulasProgJAN.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES2 = decimal.TryParse(txtQtdeAulasProgFEV.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES3 = decimal.TryParse(txtQtdeAulasProgMAR.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES4 = decimal.TryParse(txtQtdeAulasProgABR.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES5 = decimal.TryParse(txtQtdeAulasProgMAI.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES6 = decimal.TryParse(txtQtdeAulasProgJUN.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES7 = decimal.TryParse(txtQtdeAulasProgJUL.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES8 = decimal.TryParse(txtQtdeAulasProgAGO.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES9 = decimal.TryParse(txtQtdeAulasProgSET.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES10 = decimal.TryParse(txtQtdeAulasProgOUT.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES11 = decimal.TryParse(txtQtdeAulasProgNOV.Text, out decimalRetorno) ? decimalRetorno : 0;
                tb112.VL_PLAN_MES12 = decimal.TryParse(txtQtdeAulasProgDEZ.Text, out decimalRetorno) ? decimalRetorno : 0;

                CurrentPadraoCadastros.CurrentEntity = tb112;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB112_PLANCUSTO tb112 = RetornaEntidade();

            if (tb112 != null)
            {
                txtAno.Text = tb112.CO_ANO_REF.ToString();

                ddlDepto.SelectedValue = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                                          where tb099.CO_CENT_CUSTO == tb112.CO_CENT_CUSTO
                                          select new { tb099.TB14_DEPTO.CO_DEPTO }).FirstOrDefault().CO_DEPTO.ToString();

                CarregaCCusto();
                ddlCCusto.SelectedValue = tb112.CO_CENT_CUSTO.ToString();

                TB56_PLANOCTA planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb112.CO_SEQU_PC);

                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                    ddlTipoConta.SelectedValue = planoConta.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                    CarregaGrupo();
                    ddlGrupo.SelectedValue = planoConta.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                    CarregaSubgrupo();
                    ddlSubgrupo.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                    CarregaCContabil();
                    ddlCContabil.SelectedValue = planoConta.CO_SEQU_PC.ToString();
                }

                txtQtdeAulasProgJAN.Text = tb112.VL_PLAN_MES1.ToString();
                txtQtdeAulasProgFEV.Text = tb112.VL_PLAN_MES2.ToString();
                txtQtdeAulasProgMAR.Text = tb112.VL_PLAN_MES3.ToString();
                txtQtdeAulasProgABR.Text = tb112.VL_PLAN_MES4.ToString();
                txtQtdeAulasProgMAI.Text = tb112.VL_PLAN_MES5.ToString();
                txtQtdeAulasProgJUN.Text = tb112.VL_PLAN_MES6.ToString();
                txtQtdeAulasProgJUL.Text = tb112.VL_PLAN_MES7.ToString();
                txtQtdeAulasProgAGO.Text = tb112.VL_PLAN_MES8.ToString();
                txtQtdeAulasProgSET.Text = tb112.VL_PLAN_MES9.ToString();
                txtQtdeAulasProgOUT.Text = tb112.VL_PLAN_MES10.ToString();
                txtQtdeAulasProgNOV.Text = tb112.VL_PLAN_MES11.ToString();
                txtQtdeAulasProgDEZ.Text = tb112.VL_PLAN_MES12.ToString();
                txtQtdeAulasRealJAN.Text = tb112.VL_REAL_MES_1.ToString();
                txtQtdeAulasRealFEV.Text = tb112.VL_REAL_MES_2.ToString();
                txtQtdeAulasRealMAR.Text = tb112.VL_REAL_MES_3.ToString();
                txtQtdeAulasRealABR.Text = tb112.VL_REAL_MES_4.ToString();
                txtQtdeAulasRealMAI.Text = tb112.VL_REAL_MES_5.ToString();
                txtQtdeAulasRealJUN.Text = tb112.VL_REAL_MES_6.ToString();
                txtQtdeAulasRealJUL.Text = tb112.VL_REAL_MES_7.ToString();
                txtQtdeAulasRealAGO.Text = tb112.VL_REAL_MES_8.ToString();
                txtQtdeAulasRealSET.Text = tb112.VL_REAL_MES_9.ToString();
                txtQtdeAulasRealOUT.Text = tb112.VL_REAL_MES_10.ToString();
                txtQtdeAulasRealNOV.Text = tb112.VL_REAL_MES_11.ToString();
                txtQtdeAulasRealDEZ.Text = tb112.VL_REAL_MES_12.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB112_PLANCUSTO</returns>
        private TB112_PLANCUSTO RetornaEntidade()
        {
            return TB112_PLANCUSTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("ano"), QueryStringAuxili.RetornaQueryStringComoIntPelaChave("cCusto"),
                LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave("cContabil"), QueryStringAuxili.RetornaQueryStringComoIntPelaChave("tipo"));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Centro de Custo
        /// </summary>
        private void CarregaCCusto()
        {
            int coDepto = ddlDepto.SelectedValue != "" ? int.Parse(ddlDepto.SelectedValue) : 0;

            ddlCCusto.DataSource = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                                    where tb099.TB14_DEPTO.CO_DEPTO == coDepto
                                    select new { tb099.CO_CENT_CUSTO, tb099.DE_CENT_CUSTO }).OrderBy( c => c.DE_CENT_CUSTO );

            ddlCCusto.DataTextField = "DE_CENT_CUSTO";
            ddlCCusto.DataValueField = "CO_CENT_CUSTO";
            ddlCCusto.DataBind();

            ddlCCusto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo
        /// </summary>
        private void CarregaGrupo()
        {
            var result = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                          where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                          && tb53.TP_GRUP_CTA == ddlTipoConta.SelectedValue
                          select new { tb53.DE_GRUP_CTA, tb53.CO_GRUP_CTA, tb53.NR_GRUP_CTA }).ToList();

            ddlGrupo.DataSource = (from res in result
                                   select new
                                   {
                                       DE_GRUP_CTA = res.NR_GRUP_CTA.ToString().PadLeft(2, '0') + " - " + res.DE_GRUP_CTA,
                                       res.CO_GRUP_CTA
                                   }).OrderBy(p => p.DE_GRUP_CTA);

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();


            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubgrupo()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            var result = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                          where tb54.CO_GRUP_CTA == coGrupCta
                          select new { tb54.DE_SGRUP_CTA, tb54.CO_SGRUP_CTA, tb54.NR_SGRUP_CTA }).ToList();

            ddlSubgrupo.DataSource = (from res in result
                                      select new
                                      {
                                          DE_SGRUP_CTA = res.NR_SGRUP_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP_CTA,
                                          res.CO_SGRUP_CTA
                                      }).OrderBy(p => p.DE_SGRUP_CTA);

            ddlSubgrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubgrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubgrupo.DataBind();


            ddlSubgrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contábil
        /// </summary>
        private void CarregaCContabil()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSGrupCta = ddlSubgrupo.SelectedValue != "" ? int.Parse(ddlSubgrupo.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta && tb56.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta
                             select new { tb56.NU_CONTA_PC, tb56.DE_CONTA_PC, tb56.CO_SEQU_PC }).ToList();

            ddlCContabil.DataSource = (from result in resultado
                                           select new
                                           {
                                               result.CO_SEQU_PC,
                                               DE_CONTA_PC = string.Format("{0} - {1}", result.NU_CONTA_PC.Value.ToString("00000"), result.DE_CONTA_PC)
                                           }).OrderBy(r => r.DE_CONTA_PC);

            ddlCContabil.DataTextField = "DE_CONTA_PC";
            ddlCContabil.DataValueField = "CO_SEQU_PC";
            ddlCContabil.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamento
        /// </summary>
        private void CarregaDepartamento()
        {
            ddlDepto.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                   where tb14.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                   select new { tb14.CO_DEPTO, tb14.NO_DEPTO });

            ddlDepto.DataTextField = "NO_DEPTO";
            ddlDepto.DataValueField = "CO_DEPTO";
            ddlDepto.DataBind();

            ddlDepto.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCCusto();
        }

        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupo();
            CarregaSubgrupo();
            CarregaCContabil();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo();
            CarregaCContabil();
        }

        protected void ddlSubGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCContabil();
        }        
    }
}
