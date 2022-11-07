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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 no cadastro
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1115_PlanejAnualFinanCtaContabil
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
                CarregaTipo();
                CarregaSubGrupo();
                CarregaSubGrupo2();
                CarregaConta();
                txtAno.Text = DateTime.Now.Year.ToString();
                txtAno.Enabled =  ddlContaContabil.Enabled =  ddlSubGrupo.Enabled = ddlSubGrupo2.Enabled =  ddlTipo.Enabled = true;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //TB111_PLANCONTAB tb111 = RetornaEntidade();
            //int coSequPc = int.Parse(ddlContaContabil.SelectedValue);
            //Decimal decimalRetorno = 0;

            //var qtdPlanoCta = (from lTb111 in TB111_PLANCONTAB.RetornaTodosRegistros()
            //                   where lTb111.CO_ANO_REF == txtAno.Text
            //                   && lTb111.CO_EMP == LoginAuxili.CO_EMP
            //                   && lTb111.CO_SEQU_PC == coSequPc
            //                   select new { lTb111.CO_SEQU_PC }).Count();

            //if (qtdPlanoCta > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            //    AuxiliPagina.EnvioMensagemErro(this, MensagensErro.DadosExistentes);
            //else
            //{
            //    if (tb111 == null)
            //    {
            //        tb111 = new TB111_PLANCONTAB();

            //        tb111.CO_ANO_REF = txtAno.Text;
            //        tb111.CO_EMP = LoginAuxili.CO_EMP;
            //        tb111.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            //        tb111.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc);
            //        tb111.CO_SEQU_PC = coSequPc;
            //    }

            //    tb111.VL_PLAN_MES1 = Decimal.TryParse(txtQtdeAulasProgJAN.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES2 = Decimal.TryParse(txtQtdeAulasProgFEV.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES3 = Decimal.TryParse(txtQtdeAulasProgMAR.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES4 = Decimal.TryParse(txtQtdeAulasProgABR.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES5 = Decimal.TryParse(txtQtdeAulasProgMAI.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES6 = Decimal.TryParse(txtQtdeAulasProgJUN.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES7 = Decimal.TryParse(txtQtdeAulasProgJUL.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES8 = Decimal.TryParse(txtQtdeAulasProgAGO.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES9 = Decimal.TryParse(txtQtdeAulasProgSET.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES10 = Decimal.TryParse(txtQtdeAulasProgOUT.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES11 = Decimal.TryParse(txtQtdeAulasProgNOV.Text, out decimalRetorno) ? decimalRetorno : 0;
            //    tb111.VL_PLAN_MES12 = Decimal.TryParse(txtQtdeAulasProgDEZ.Text, out decimalRetorno) ? decimalRetorno : 0;

            //    CurrentPadraoCadastros.CurrentEntity = tb111;
            //}
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            //TB111_PLANCONTAB tb111 = RetornaEntidade();

            //if (tb111 != null)
            //{
            //    txtAno.Text = tb111.CO_ANO_REF.ToString();
            //    ddlSubGrupo.SelectedValue = QueryStringAuxili.RetornaQueryStringPelaChave("grupo");
            //    CarregaConta();
            //    ddlContaContabil.SelectedValue = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id);
            //    txtQtdeAulasProgJAN.Text = tb111.VL_PLAN_MES1.ToString();
            //    txtQtdeAulasProgFEV.Text = tb111.VL_PLAN_MES2.ToString();
            //    txtQtdeAulasProgMAR.Text = tb111.VL_PLAN_MES3.ToString();
            //    txtQtdeAulasProgABR.Text = tb111.VL_PLAN_MES4.ToString();
            //    txtQtdeAulasProgMAI.Text = tb111.VL_PLAN_MES5.ToString();
            //    txtQtdeAulasProgJUN.Text = tb111.VL_PLAN_MES6.ToString();
            //    txtQtdeAulasProgJUL.Text = tb111.VL_PLAN_MES7.ToString();
            //    txtQtdeAulasProgAGO.Text = tb111.VL_PLAN_MES8.ToString();
            //    txtQtdeAulasProgSET.Text = tb111.VL_PLAN_MES9.ToString();
            //    txtQtdeAulasProgOUT.Text = tb111.VL_PLAN_MES10.ToString();
            //    txtQtdeAulasProgNOV.Text = tb111.VL_PLAN_MES11.ToString();
            //    txtQtdeAulasProgDEZ.Text = tb111.VL_PLAN_MES12.ToString();
            //    txtQtdeAulasRealJAN.Text = tb111.VL_REAL_MES_1.ToString();
            //    txtQtdeAulasRealFEV.Text = tb111.VL_REAL_MES_2.ToString();
            //    txtQtdeAulasRealMAR.Text = tb111.VL_REAL_MES_3.ToString();
            //    txtQtdeAulasRealABR.Text = tb111.VL_REAL_MES_4.ToString();
            //    txtQtdeAulasRealMAI.Text = tb111.VL_REAL_MES_5.ToString();
            //    txtQtdeAulasRealJUN.Text = tb111.VL_REAL_MES_6.ToString();
            //    txtQtdeAulasRealJUL.Text = tb111.VL_REAL_MES_7.ToString();
            //    txtQtdeAulasRealAGO.Text = tb111.VL_REAL_MES_8.ToString();
            //    txtQtdeAulasRealSET.Text = tb111.VL_REAL_MES_9.ToString();
            //    txtQtdeAulasRealOUT.Text = tb111.VL_REAL_MES_10.ToString();
            //    txtQtdeAulasRealNOV.Text = tb111.VL_REAL_MES_11.ToString();
            //    txtQtdeAulasRealDEZ.Text = tb111.VL_REAL_MES_12.ToString();
            //}
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB111_PLANEJ_FINAN</returns>
        private TB111_PLANEJ_FINAN RetornaEntidade()
        {
            return TB111_PLANEJ_FINAN.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipo
        /// </summary>
        private void CarregaTipo()
        {
            ddlTipo.DataSource = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                                  where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA }).OrderBy(s => s.DE_GRUP_CTA);

            ddlTipo.DataTextField = "DE_GRUP_CTA";
            ddlTipo.DataValueField = "CO_GRUP_CTA";
            ddlTipo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubGrupo()
        {
            int coGrupCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;

            ddlSubGrupo.DataSource = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                                      where tb54.CO_GRUP_CTA == coGrupCta
                                      select new { tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA });

            ddlSubGrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //====> Método que carrega o DropDown de SubGrupo2 de Contas
        private void CarregaSubGrupo2()
        {
            int coGrupCta = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            ddlSubGrupo2.DataSource = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                                       where tb055.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta && tb055.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
                                       select new { tb055.DE_SGRUP2_CTA, tb055.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA });

            ddlSubGrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2.DataBind();

            ddlSubGrupo2.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contabil
        /// </summary>
        private void CarregaConta()
        {
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coSGrup2Cta = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : 0;

            ddlContaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                           where tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
                                           && tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSGrup2Cta
                                           select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy( p => p.DE_CONTA_PC );

            ddlContaContabil.DataTextField = "DE_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_SEQU_PC";
            ddlContaContabil.DataBind();

            ddlContaContabil.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }

        protected void ddlSubGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2();
        }

        protected void ddlSubGrupo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta();
        }
    }
}
