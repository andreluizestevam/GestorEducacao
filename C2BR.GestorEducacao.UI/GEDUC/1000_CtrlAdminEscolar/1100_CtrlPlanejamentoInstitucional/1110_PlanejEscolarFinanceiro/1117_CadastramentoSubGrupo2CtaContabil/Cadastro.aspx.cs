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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1117_CadastramentoSubGrupo2CtaContabil
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                ddlTipoConta.Enabled = false;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int intNrSGrupo;
            if (!int.TryParse(txtNumSubGrupo.Text, out intNrSGrupo))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Número informado não é válido");
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                int nuSGrp = int.Parse(ddlSubGrupo.SelectedValue);
                int nuSGrp2 = int.Parse(txtNumSubGrupo.Text);
                int nuGrp = int.Parse(ddlGrupo.SelectedValue);

                int ocorrSGrp = (from lTb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                                 where lTb055.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA.Equals(ddlTipoConta.SelectedValue)
                                && lTb055.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == nuGrp
                                && lTb055.TB54_SGRP_CTA.CO_SGRUP_CTA == nuSGrp
                                && lTb055.NR_SGRUP2_CTA == nuSGrp2
                                 select new { lTb055.CO_SGRUP2_CTA }).Count();

                if (ocorrSGrp > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Número de subgrupo2 já cadastrado para tipo e grupo de conta informado.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int nuSGrp2 = int.Parse(txtNumSubGrupo.Text);
                int nuGrp = int.Parse(ddlGrupo.SelectedValue);
                int nuSGrp = int.Parse(ddlSubGrupo.SelectedValue);
                int idSGrp2 = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                int ocorrSGrp = (from lTb055 in TB055_SGRP2_CTA.RetornaTodosRegistros() 
                                 join lTb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on lTb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals lTb54.CO_SGRUP_CTA
                                 where lTb54.TB53_GRP_CTA.TP_GRUP_CTA.Equals(ddlTipoConta.SelectedValue)
                                 && lTb54.TB53_GRP_CTA.CO_GRUP_CTA == nuGrp
                                 && lTb54.CO_SGRUP_CTA == nuSGrp 
                                 && lTb055.NR_SGRUP2_CTA == nuSGrp2 
                                 && lTb055.CO_SGRUP2_CTA != idSGrp2
                                 select new { lTb54.CO_SGRUP_CTA }).Count();

                if (ocorrSGrp > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Número de subgrupo2 já cadastrado para tipo e grupo de conta informado.");
                    return;
                }
            }

            TB055_SGRP2_CTA tb055 = RetornaEntidade();

            if (tb055 == null)
                tb055 = new TB055_SGRP2_CTA();

            //if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            //{
            //    tb055.CO_GRUP_CTA = int.Parse(ddlGrupo.SelectedValue);
            //    tb54.TB53_GRP_CTA = TB53_GRP_CTA.RetornaPelaChavePrimaria(tb54.CO_GRUP_CTA);
            //}

            tb055.TB54_SGRP_CTA = TB54_SGRP_CTA.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo.SelectedValue));
            tb055.DE_SGRUP2_CTA = txtDescricaoSubGrupo.Text;
            tb055.NR_SGRUP2_CTA = int.Parse(txtNumSubGrupo.Text);
            tb055.DT_ALT_REGISTRO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb055;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB055_SGRP2_CTA tb055 = RetornaEntidade();

            if (tb055 != null)
            {
                tb055.TB54_SGRP_CTAReference.Load();
                var tb54 = tb055.TB54_SGRP_CTA;

                if (tb54 != null)
                {
                    tb54.TB53_GRP_CTAReference.Load();

                    ddlTipoConta.SelectedValue = tb54.TB53_GRP_CTA.TP_GRUP_CTA;
                    CarregaGrupo();
                    ddlGrupo.SelectedValue = tb54.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                    CarregaSubgrupo();
                    ddlSubGrupo.SelectedValue = tb54.CO_SGRUP_CTA.ToString();
                }
                
                txtDescricaoSubGrupo.Text = tb055.DE_SGRUP2_CTA;
                txtNumSubGrupo.Text = tb055.NR_SGRUP2_CTA != null ? tb055.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') : "";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB54_SGRP_CTA</returns>
        private TB055_SGRP2_CTA RetornaEntidade()
        {
            return TB055_SGRP2_CTA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
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

            ddlSubGrupo.DataSource = (from res in result
                                      select new
                                      {
                                          DE_SGRUP_CTA = res.NR_SGRUP_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP_CTA,
                                          res.CO_SGRUP_CTA
                                      }).OrderBy(p => p.DE_SGRUP_CTA);

            ddlSubGrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupo.DataBind();


            ddlSubGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupo();
            CarregaSubgrupo();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo();
        }
    }
}