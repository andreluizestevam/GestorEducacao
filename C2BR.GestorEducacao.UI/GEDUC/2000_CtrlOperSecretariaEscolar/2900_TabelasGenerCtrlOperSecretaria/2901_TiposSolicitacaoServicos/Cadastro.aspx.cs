//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: CADASTRAMENTO DOS TIPOS DE SOLICITAÇÃO DE SERVIÇOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 23/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 da conta contábil
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

/*Início das Regras de Negócios
Localização do Arquivo da Funcionalidade no Ambiente da Solução*/
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2901_TiposSolicitacaoServicos
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ckbAtualizaFinanc.Checked = true;

                CarregaGrupoItemSolicitacao();
                CarregaUnidade();
                CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
                CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
                CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
                CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
                CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
                CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
                CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
                CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
                CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
                CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
                CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
                CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
                CarregaAgrupadores();
                CarregaArquivoRTF();
                CarregaHistorio();
                ckbTxMatricula.Enabled = false;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB66_TIPO_SOLIC tb66 = RetornaEntidade();

            tb66.TB061_GRUPO_SOLIC = ddlGrupoTipo.SelectedValue != "" ? TB061_GRUPO_SOLIC.RetornaPelaChavePrimaria(int.Parse(ddlGrupoTipo.SelectedValue)) : null;
            tb66.CO_SITU_SOLI = ddlSituacao.SelectedValue;
            tb66.DT_SITU_SOLI = DateTime.Parse(txtData.Text);
            tb66.NO_TIPO_SOLI = txtDescricao.Text;

            decimal vl;
            tb66.VL_UNIT_SOLI = (decimal.TryParse(txtValor.Text, out vl)) ? (decimal?)vl : decimal.Zero;

            //
            tb66.FL_ATUALI_FINAN_TPSOLIC = ckbAtualizaFinanc.Checked ? "S" : "N";
            tb66.CO_SIGLA_TPSOLIC = txtSigla.Text;
            tb66.TB009_RTF_DOCTOS = ddlArquivoRTF.SelectedValue != "" ? TB009_RTF_DOCTOS.RetornaPelaChavePrimaria(int.Parse(ddlArquivoRTF.SelectedValue)) : null;
            tb66.FL_ITEM_MATRIC_TPSOLIC = ckbItemMatr.Checked ? "S" : "N";
            tb66.ID_HISTOR_FINANC_TPSOLIC = int.Parse(ddlhistorico.SelectedValue);
            tb66.ID_AGRUP_RECEI_TPSOLIC = ddlAgrupa.SelectedValue != "" ? (int?)int.Parse(ddlAgrupa.SelectedValue) : null;
            tb66.FL_TAXA_MATRIC = ckbTxMatricula.Checked ? "S" : "N";

            if (ckbAtualizaFinanc.Checked)
            {
                int? n = null;
                tb66.CO_SEQU_PC = ddlContaContabilA.SelectedValue != "" ? int.Parse(ddlContaContabilA.SelectedValue) : n;
                tb66.CO_SEQU_PC_BANCO = ddlContaContabilB.SelectedValue != "" ? int.Parse(ddlContaContabilB.SelectedValue) : n;
                tb66.CO_SEQU_PC_CAIXA = ddlContaContabilC.SelectedValue != "" ? int.Parse(ddlContaContabilC.SelectedValue) : n;
                tb66.TB89_UNIDADES = ddlUnidade.SelectedValue != "" ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)) : null;
            }

            CurrentPadraoCadastros.CurrentEntity = tb66;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB66_TIPO_SOLIC tb66 = RetornaEntidade();

            if (tb66 != null)
            {
                tb66.TB89_UNIDADESReference.Load();
                tb66.TB009_RTF_DOCTOSReference.Load();
                tb66.TB061_GRUPO_SOLICReference.Load();


                ddlGrupoTipo.SelectedValue = tb66.TB061_GRUPO_SOLIC != null ? tb66.TB061_GRUPO_SOLIC.ID_GRUPO_SOLIC.ToString() : "";
                // txtCodigo.Text = tb66.CO_TIPO_SOLI.ToString();
                txtSigla.Text = tb66.CO_SIGLA_TPSOLIC;

                txtData.Text = tb66.DT_SITU_SOLI.ToString("dd/MM/yyyy");
                txtDescricao.Text = tb66.NO_TIPO_SOLI;
                // txtNomeProcessoExterno.Text = tb66.NO_PROC_EXTE_SOLI;
                ddlArquivoRTF.SelectedValue = tb66.TB009_RTF_DOCTOS != null ? tb66.TB009_RTF_DOCTOS.ID_DOCUM.ToString() : "";                
                ddlSituacao.SelectedValue = tb66.CO_SITU_SOLI;
                txtValor.Text = tb66.VL_UNIT_SOLI != null ? tb66.VL_UNIT_SOLI.Value.ToString("0.00") : "0";
                ddlUnidade.SelectedValue = tb66.TB89_UNIDADES != null ? tb66.TB89_UNIDADES.CO_UNID_ITEM.ToString() : "";
                ckbItemMatr.Checked = tb66.FL_ITEM_MATRIC_TPSOLIC == "S";
                ckbAtualizaFinanc.Checked = tb66.FL_ATUALI_FINAN_TPSOLIC == "S";
                ddlAgrupa.SelectedValue = tb66.ID_AGRUP_RECEI_TPSOLIC != null ? tb66.ID_AGRUP_RECEI_TPSOLIC.ToString() : "";
                ddlhistorico.SelectedValue = tb66.ID_HISTOR_FINANC_TPSOLIC.ToString();
            }

            if (tb66.CO_SEQU_PC != null)
            {
                TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb66.CO_SEQU_PC);
                tb56.TB54_SGRP_CTAReference.Load();
                tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                ddlTipoContaA.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
                ddlGrupoContaA.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
                ddlSubGrupoContaA.SelectedValue = tb56.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
                ddlSubGrupo2ContaA.SelectedValue = tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
                ddlContaContabilA.SelectedValue = tb56.CO_SEQU_PC.ToString();
                CarregaCodigoContaContabil(ddlContaContabilA, txtCodigoContaContabilA);
            }

            // Conta Contábil Caixa
            if (tb66.CO_SEQU_PC_CAIXA != null)
            {
                TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb66.CO_SEQU_PC_CAIXA);
                tb56.TB54_SGRP_CTAReference.Load();
                tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                ddlTipoContaC.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
                ddlGrupoContaC.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
                ddlSubGrupoContaC.SelectedValue = tb56.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
                ddlSubGrupo2ContaC.SelectedValue = tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
                ddlContaContabilC.SelectedValue = tb56.CO_SEQU_PC.ToString();
                CarregaCodigoContaContabil(ddlContaContabilC, txtCodigoContaContabilC);
            }

            // Conta Contábil Banco
            if (tb66.CO_SEQU_PC_BANCO != null)
            {
                TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb66.CO_SEQU_PC_BANCO);
                tb56.TB54_SGRP_CTAReference.Load();
                tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                ddlTipoContaB.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
                ddlGrupoContaB.SelectedValue = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA.ToString();
                CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
                ddlSubGrupoContaB.SelectedValue = tb56.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();
                CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
                ddlSubGrupo2ContaB.SelectedValue = tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";
                CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
                ddlContaContabilB.SelectedValue = tb56.CO_SEQU_PC.ToString();
                CarregaCodigoContaContabil(ddlContaContabilB, txtCodigoContaContabilB);
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB66_TIPO_SOLIC</returns>
        private TB66_TIPO_SOLIC RetornaEntidade()
        {
            TB66_TIPO_SOLIC tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb66 == null) ? new TB66_TIPO_SOLIC() : tb66;
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupa.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupa.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupa.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupa.DataBind();

            ddlAgrupa.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Grupo Item Solicitação 
        /// </summary>
        private void CarregaGrupoItemSolicitacao() {
            var res = (from tb061 in TB061_GRUPO_SOLIC.RetornaTodosRegistros()
                       where tb061.CO_SITUA_GRUPO_SOLIC == "A"
                       select new { 
                           tb061.ID_GRUPO_SOLIC, tb061.NM_GRUPO_SOLIC 
                       }).OrderBy(x => x.NM_GRUPO_SOLIC).ToList();
            ddlGrupoTipo.DataSource = res;


            ddlGrupoTipo.DataTextField = "NM_GRUPO_SOLIC";
            ddlGrupoTipo.DataValueField = "ID_GRUPO_SOLIC";
            ddlGrupoTipo.DataBind();

            ddlGrupoTipo.Items.Insert(0, new ListItem("Selecione", ""));           
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Conta Contábil
        /// </summary>
        private void CarregaGrupoContasContabeis(DropDownList ddltipo, DropDownList ddlGrupo)
        {
            var res = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                       where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                           && tb53.TP_GRUP_CTA == ddltipo.SelectedValue
                       select new { tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA, tb53.NR_GRUP_CTA }).OrderBy(p => p.NR_GRUP_CTA).ToList();

            ddlGrupo.DataSource = from r in res
                                  select new
                                  {
                                      r.CO_GRUP_CTA,
                                      DE_GRUP_CTA = r.NR_GRUP_CTA.ToString().PadLeft(2, '0') + " - " + r.DE_GRUP_CTA
                                  };

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubgrupo(DropDownList ddlGrupo, DropDownList ddlSubGrupo)
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
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo2
        /// </summary>
        private void CarregaSubgrupo2(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlSubGrupo2)
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            var result = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                          where tb055.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta && tb055.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
                          select new
                          {
                              tb055.NR_SGRUP2_CTA,
                              tb055.DE_SGRUP2_CTA,
                              tb055.CO_SGRUP2_CTA
                          }).ToList();

            ddlSubGrupo2.DataSource = (from res in result
                                                select new
                                                {
                                                    DE_SGRUP2_CTA = res.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP2_CTA,
                                                    res.CO_SGRUP2_CTA
                                                });

            ddlSubGrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2.DataBind();

            ddlSubGrupo2.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contábil
        /// </summary>
        private void CarregaContasContabeis(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlSubGrupo2, DropDownList ddlCtaContabil)
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coSubGrupo2 = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : 0;

            ddlCtaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                         where tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         && tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == coGrupo
                                         && tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSubGrupo
                                         && (coSubGrupo2 != 0 ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSubGrupo2 : 0 == 0)
                                         select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy(p => p.DE_CONTA_PC);

            ddlCtaContabil.DataTextField = "DE_CONTA_PC";
            ddlCtaContabil.DataValueField = "CO_SEQU_PC";
            ddlCtaContabil.DataBind();

            ddlCtaContabil.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega informações da Conta Contábil selecionada
        /// </summary>
        private void CarregaCodigoContaContabil(DropDownList ddlCtaContabil, TextBox txtCodiConta)
        {
            int coSequPc = ddlCtaContabil.SelectedValue != "" ? int.Parse(ddlCtaContabil.SelectedValue) : 0;

            txtCodiConta.Text = "";

            if (coSequPc == 0)
                return;

            TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc);
            tb56.TB54_SGRP_CTAReference.Load();
            TB055_SGRP2_CTA tb55 = TB055_SGRP2_CTA.RetornaPelaChavePrimaria(tb56.TB055_SGRP2_CTA != null ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA : 0);
            tb56.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
            string tipoConta = tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA == "A" ? "1" : "3";

            txtCodiConta.Text = tipoConta + "." + tb56.TB54_SGRP_CTA.TB53_GRP_CTA.NR_GRUP_CTA.ToString().PadLeft(2, '0') + "." + tb56.TB54_SGRP_CTA.NR_SGRUP_CTA.ToString().PadLeft(3, '0')
                + "." + (tb55 != null ? tb55.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') : "XXX")
                + "." + tb56.NU_CONTA_PC.ToString().PadLeft(4, '0');
        }
        
        private void CarregaHistorio()
        {

            var res = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                       select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO }).OrderBy(p => p.DE_HISTORICO).ToList();
            
            ddlhistorico.DataSource = res;
            ddlhistorico.DataTextField = "DE_HISTORICO";
            ddlhistorico.DataValueField = "CO_HISTORICO";
            ddlhistorico.DataBind();
        }
        
        private void CarregaArquivoRTF()
        {
            var res = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                       where tb009.CO_SITUS_DOCUM == "A"
                       select new { tb009.ID_DOCUM, tb009.NM_DOCUM }).ToList();
            ddlArquivoRTF.DataSource = res;

            ddlArquivoRTF.DataTextField = "NM_DOCUM";
            ddlArquivoRTF.DataValueField = "ID_DOCUM";
            ddlArquivoRTF.DataBind();

            ddlArquivoRTF.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Evento dos componentes da página

        protected void ddlTipoContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlGrupoContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupoContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupo2ContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlContaContabilA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil(ddlContaContabilA, txtCodigoContaContabilA);
        }                        

        protected void ddlTipoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlGrupoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlSubGrupoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlSubGrupo2ContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlSubGrupo2ContaB, ddlContaContabilB);
        }

        protected void ddlContaContabilB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil(ddlContaContabilB, txtCodigoContaContabilB);
        }

        protected void ddlTipoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlGrupoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlSubGrupoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlSubGrupo2ContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlSubGrupo2ContaC, ddlContaContabilC);
        }

        protected void ddlContaContabilC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoContaContabil(ddlContaContabilC, txtCodigoContaContabilC);
        }

        protected void ckbItemMatr_checkedChange(object sender, EventArgs e)
        {
            CheckBox ck = (CheckBox)sender;
            if (ck.Checked)
            {
                ckbTxMatricula.Enabled = true;
            }
            else
            {
                ckbTxMatricula.Enabled = false;
                ckbTxMatricula.Checked = false;
            }
        }

        #endregion
    }
}
