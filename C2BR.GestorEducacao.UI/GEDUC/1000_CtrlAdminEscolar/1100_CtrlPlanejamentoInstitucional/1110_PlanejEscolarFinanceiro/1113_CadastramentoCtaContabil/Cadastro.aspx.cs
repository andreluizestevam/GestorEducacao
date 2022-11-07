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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 no cadastro
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1113_CadastramentoCtaContabil
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
            if (Page.IsPostBack) return;

            CarregaGrupo();
            CarregaSubgrupo();
            CarregaSubgrupo2();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int intConta;
            if (!int.TryParse(txtNumConta.Text, out intConta))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Número informado não é válido");
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                int nuCta = int.Parse(txtNumConta.Text);
                int nuSGrp = int.Parse(ddlSubgrupo.SelectedValue);
                int nuSGrp2 = int.Parse(ddlSubGrupo2.SelectedValue);
                int nuGrp = int.Parse(ddlGrupo.SelectedValue);

                int ocorrCta = (from lTb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                 where lTb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA.Equals(ddlTipoConta.SelectedValue)
                                 && lTb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == nuGrp
                                 && lTb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == nuSGrp2
                                 && lTb56.TB54_SGRP_CTA.CO_SGRUP_CTA == nuSGrp && lTb56.NU_CONTA_PC == nuCta
                                select new { lTb56.CO_SEQU_PC }).Count();

                if (ocorrCta > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Número de conta já cadastrado para tipo, grupo e subgrupo de conta informado.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int nuCta = int.Parse(txtNumConta.Text);
                int nuSGrp = int.Parse(ddlSubgrupo.SelectedValue);
                int nuGrp = int.Parse(ddlGrupo.SelectedValue);
                int nuSGrp2 = int.Parse(ddlSubGrupo2.SelectedValue);
                int idCta = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                int ocorrCta = (from lTb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                where lTb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA.Equals(ddlTipoConta.SelectedValue)
                                && lTb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == nuGrp
                                && lTb56.TB54_SGRP_CTA.CO_SGRUP_CTA == nuSGrp && lTb56.NU_CONTA_PC == nuCta
                                && lTb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == nuSGrp2
                                && lTb56.CO_SEQU_PC != idCta
                                select new { lTb56.CO_SEQU_PC }).Count();

                if (ocorrCta > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Número de conta já cadastrado para tipo, grupo e subgrupo de conta informado.");
                    return;
                }
            }

            TB56_PLANOCTA tb56 = RetornaEntidade();

            if (tb56 == null)
                tb56 = new TB56_PLANOCTA();

            tb56.TB54_SGRP_CTA = TB54_SGRP_CTA.RetornaPelaChavePrimaria(int.Parse(ddlSubgrupo.SelectedValue));
            tb56.TB055_SGRP2_CTA = TB055_SGRP2_CTA.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo2.SelectedValue));
            tb56.DE_CONTA_PC = txtDescricaoConta.Text;
            tb56.NU_CONTA_PC = int.Parse(txtNumConta.Text);
            tb56.DT_ALT_REGISTRO = DateTime.Now;

//--------> Se for inserção: retorna o último número da conta, soma mais um no mesmo e lança no campo CO_CONTA_PC o resultado
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocoTB56 = (from lTb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                              where lTb56.TB54_SGRP_CTA.CO_SGRUP_CTA == tb56.TB54_SGRP_CTA.CO_SGRUP_CTA
                              && lTb56.TB54_SGRP_CTA.CO_GRUP_CTA == tb56.TB54_SGRP_CTA.CO_GRUP_CTA
                              && lTb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA
                              select lTb56).ToList();

                int intMax = 1;

                if (ocoTB56.Count() > 0)
                {
                    intMax = intMax + ocoTB56.Max(c => c.CO_CONTA_PC);
                }

                tb56.CO_CONTA_PC = intMax;
            }

            CurrentPadraoCadastros.CurrentEntity = tb56;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB56_PLANOCTA tb56 = RetornaEntidade();

            if (tb56 != null)
            {
                tb56.TB54_SGRP_CTAReference.Load();
                tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                ddlTipoConta.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                CarregaGrupo();
                ddlGrupo.SelectedValue = tb56.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();
                txtNumConta.Text = tb56.NU_CONTA_PC != null ? tb56.NU_CONTA_PC.ToString().PadLeft(4,'0') : "";
                CarregaSubgrupo();
                ddlSubgrupo.SelectedValue = tb56.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                CarregaSubgrupo2();
                ddlSubGrupo2.SelectedValue = tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                txtDescricaoConta.Text = tb56.DE_CONTA_PC;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB56_PLANOCTA</returns>
        private TB56_PLANOCTA RetornaEntidade()
        {
            return TB56_PLANOCTA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

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
                                         DE_SGRUP_CTA = res.NR_SGRUP_CTA.ToString().PadLeft(3,'0') + " - " + res.DE_SGRUP_CTA,
                                         res.CO_SGRUP_CTA
                                     }).OrderBy(p => p.DE_SGRUP_CTA);

            ddlSubgrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubgrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubgrupo.DataBind();


            ddlSubgrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //====> Método que carrega o DropDown de SubGrupo2 de Contas
        private void CarregaSubgrupo2()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSGrupCta = ddlSubgrupo.SelectedValue != "" ? int.Parse(ddlSubgrupo.SelectedValue) : 0;

            var result = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                        join tb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on tb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals tb54.CO_SGRUP_CTA
                        where tb54.CO_GRUP_CTA == coGrupCta && tb54.CO_SGRUP_CTA == coSGrupCta
                        select new 
                        {
                            tb055.NR_SGRUP2_CTA, tb055.DE_SGRUP2_CTA, tb055.CO_SGRUP2_CTA }).ToList();

            ddlSubGrupo2.DataSource = (from res in result
                                       select new 
                                       {
                                           DE_SGRUP2_CTA = res.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP2_CTA,
                                           res.CO_SGRUP2_CTA });

            ddlSubGrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2.DataBind();

            ddlSubGrupo2.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlSubgrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo();
            CarregaSubgrupo2();
        }

        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupo();
            CarregaSubgrupo();
            CarregaSubgrupo2();
        }
    }
}