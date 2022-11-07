//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: TIPOS DE MODALIDADES EDUCACIONAIS
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2904_ModalidadesEducacionais
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> tipoMedia = AuxiliBaseApoio.chave(tipoMediaBimestral.ResourceManager);

        #region Variaveis

        static string strFormaAvaliacao;

        #endregion

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
            if (IsPostBack) return;

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
            CarregaCentrosCusto();

//--------> Carrega informações dos parâmetros da Instituição
            TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            TB83_PARAMETRO tb83 = null;

            strFormaAvaliacao = "";

//--------> Faz a verificação para saber qual a forma de avaliação, se deve ser pego da instituição ou da Unidade
            if (tb149.TP_CTRLE_METOD == TipoControle.I.ToString())
                strFormaAvaliacao = tb149.TP_FORMA_AVAL;
            else if (tb149.TP_CTRLE_METOD == TipoControle.U.ToString()) 
            {
//------------> Carrega informações dos parâmetros da Unidade
                tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                strFormaAvaliacao = tb83.TP_FORMA_AVAL;
            }

//--------> Exibe os campos de média/conceito de acordo com a forma de avaliação
            if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.C])
            {
                CarregaConceitos(ddlMediaAprovacaoDireta, tb149.TP_CTRLE_METOD == TipoControle.U.ToString());
                CarregaConceitos(ddlMediaCurso, tb149.TP_CTRLE_METOD == TipoControle.U.ToString());
                CarregaConceitos(ddlMediaRecuperacao, tb149.TP_CTRLE_METOD == TipoControle.U.ToString());
            }

            ddlTipoControle.SelectedValue = tb149.TP_CTRLE_AVAL;

//--------> Verifica se o tipo de controle de avaliação é pela modalidade
            if (tb149.TP_CTRLE_AVAL == TipoControle.M.ToString())
            {
                HabilitaControleAvaliacao(true);
//------------> Se o controle for no Módulo, os campos serão carregados no CarregaFormulario()
            }
            else if (tb149.TP_CTRLE_AVAL == TipoControle.I.ToString())
            {
                HabilitaControleAvaliacao(false);
                ddlPeriodicidade.SelectedValue = tb149.TP_PERIOD_AVAL;

                if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.N])
                {
                    txtMediaAprovacaoDireta.Text = tb149.VL_MEDIA_APROV_DIRETA != null ? tb149.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                    txtMediaCurso.Text = tb149.VL_MEDIA_CURSO != null ? tb149.VL_MEDIA_CURSO.Value.ToString() : "";
                    txtMediaRecuperacao.Text = tb149.VL_MEDIA_RECUPER != null ? tb149.VL_MEDIA_RECUPER.Value.ToString() : "";
                }
                else if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.C])
                {
                    ddlMediaAprovacaoDireta.SelectedValue = tb149.VL_MEDIA_APROV_DIRETA != null ? tb149.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                    ddlMediaCurso.SelectedValue = tb149.VL_MEDIA_CURSO != null ? tb149.VL_MEDIA_CURSO.Value.ToString() : "";
                    ddlMediaRecuperacao.SelectedValue = tb149.VL_MEDIA_RECUPER != null ? tb149.VL_MEDIA_RECUPER.Value.ToString() : "";
                }
            }
            else if (tb149.TP_CTRLE_AVAL == TipoControle.U.ToString())
            {
                HabilitaControleAvaliacao(false);

                if (tb83 == null)
                    tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                ddlPeriodicidade.SelectedValue = tb83.TP_PERIOD_AVAL;

                if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.N])
                {
                    txtMediaAprovacaoDireta.Text = tb83.VL_MEDIA_APROV_DIRETA != null ? tb83.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                    txtMediaCurso.Text = tb83.VL_MEDIA_CURSO != null ? tb83.VL_MEDIA_CURSO.Value.ToString() : "";
                    txtMediaRecuperacao.Text = tb83.VL_MEDIA_RECUPER != null ? tb83.VL_MEDIA_RECUPER.Value.ToString() : "";
                }
                else if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.C])
                {
                    ddlMediaAprovacaoDireta.SelectedValue = tb83.VL_MEDIA_APROV_DIRETA != null ? tb83.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                    ddlMediaCurso.SelectedValue = tb83.VL_MEDIA_CURSO != null ? tb83.VL_MEDIA_CURSO.Value.ToString() : "";
                    ddlMediaRecuperacao.SelectedValue = tb83.VL_MEDIA_RECUPER != null ? tb83.VL_MEDIA_RECUPER.Value.ToString() : "";
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if ((ddlContaContabilA.SelectedValue == ddlContaContabilB.SelectedValue) || (ddlContaContabilA.SelectedValue == ddlContaContabilC.SelectedValue) ||
                (ddlContaContabilC.SelectedValue == ddlContaContabilB.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Conta contábil ativa, de caixa e de banco devem ser diferentes.");
                return;
            }

            TB44_MODULO tb44 = RetornaEntidade();            

            if (QueryStringAuxili.OperacaoCorrenteQueryString != QueryStrings.OperacaoExclusao)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb44.FL_INCLU_MODU_CUR = true;
                    tb44.FL_ALTER_MODU_CUR = false;
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    tb44.FL_ALTER_MODU_CUR = true;
                }

                tb44.DE_MODU_CUR = txtDescricao.Text;
                tb44.CO_SIGLA_MODU_CUR = txtSigla.Text.Trim().ToUpper();
                tb44.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb44.CO_SEQU_PC = ddlContaContabilA.SelectedValue != "" ? (int?)int.Parse(ddlContaContabilA.SelectedValue) : null;
                tb44.CO_SEQU_PC_CAIXA = ddlContaContabilC.SelectedValue != "" ? (int?)int.Parse(ddlContaContabilC.SelectedValue) : null;
                tb44.CO_SEQU_PC_BANCO = ddlContaContabilB.SelectedValue != "" ? (int?)int.Parse(ddlContaContabilB.SelectedValue) : null;
                tb44.CO_CENT_CUSTO = ddlCentroCusto.SelectedValue != "" ? (int?)int.Parse(ddlCentroCusto.SelectedValue) : null;

//------------> Só salva as informações de Parâmetros de Avaliação se o controle for da modalidade
                if (ddlTipoControle.SelectedValue == TipoControle.M.ToString())
                {
                    tb44.TP_PERIOD_AVAL = ddlPeriodicidade.SelectedValue;

                    if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.N])
                    {
                        tb44.VL_MEDIA_APROV_DIRETA = txtMediaAprovacaoDireta.Text != "" ? (decimal?)decimal.Parse(txtMediaAprovacaoDireta.Text) : null;
                        tb44.VL_MEDIA_CURSO = txtMediaCurso.Text != "" ? (decimal?)decimal.Parse(txtMediaCurso.Text) : null;
                        tb44.VL_MEDIA_RECUPER = txtMediaRecuperacao.Text != "" ? (decimal?)decimal.Parse(txtMediaRecuperacao.Text) : null;
                    }
                    else if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.C])
                    {
                        tb44.VL_MEDIA_APROV_DIRETA = ddlMediaAprovacaoDireta.SelectedValue != "" ? (decimal?)decimal.Parse(ddlMediaAprovacaoDireta.SelectedValue) : null;
                        tb44.VL_MEDIA_CURSO = ddlMediaCurso.SelectedValue != "" ? (decimal?)decimal.Parse(ddlMediaCurso.SelectedValue) : null;
                        tb44.VL_MEDIA_RECUPER = ddlMediaRecuperacao.SelectedValue != "" ? (decimal?)decimal.Parse(ddlMediaRecuperacao.SelectedValue) : null;
                    }
                }
            }

            CurrentPadraoCadastros.CurrentEntity = tb44;
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB44_MODULO tb44 = RetornaEntidade();

            if (tb44 != null)
            {
                txtDescricao.Text = tb44.DE_MODU_CUR;
                txtSigla.Text = tb44.CO_SIGLA_MODU_CUR;

                // Conta Contábil Ativa
                if (tb44.CO_SEQU_PC != null)
                {
                    TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb44.CO_SEQU_PC);
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
                if (tb44.CO_SEQU_PC_CAIXA != null)
                {
                    TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb44.CO_SEQU_PC_CAIXA);
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
                if (tb44.CO_SEQU_PC_BANCO != null)
                {
                    TB56_PLANOCTA tb56 = TB56_PLANOCTA.RetornaPelaChavePrimaria((int)tb44.CO_SEQU_PC_BANCO);
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

                ddlCentroCusto.SelectedValue = tb44.CO_CENT_CUSTO != null ? tb44.CO_CENT_CUSTO.ToString() : "";

                if (ddlTipoControle.SelectedValue == TipoControle.M.ToString())
                {
                    ddlPeriodicidade.SelectedValue = tb44.TP_PERIOD_AVAL;


                    if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.N])
                    {
                        txtMediaAprovacaoDireta.Text = tb44.VL_MEDIA_APROV_DIRETA != null ? tb44.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                        txtMediaCurso.Text = tb44.VL_MEDIA_CURSO != null ? tb44.VL_MEDIA_CURSO.Value.ToString() : "";
                        txtMediaRecuperacao.Text = tb44.VL_MEDIA_RECUPER != null ? tb44.VL_MEDIA_RECUPER.Value.ToString() : "";
                    }
                    else if (strFormaAvaliacao == tipoMedia[tipoMediaBimestral.C])
                    {
                        ddlMediaAprovacaoDireta.SelectedValue = tb44.VL_MEDIA_APROV_DIRETA != null ? tb44.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                        ddlMediaCurso.SelectedValue = tb44.VL_MEDIA_CURSO != null ? tb44.VL_MEDIA_CURSO.Value.ToString() : "";
                        ddlMediaRecuperacao.SelectedValue = tb44.VL_MEDIA_RECUPER != null ? tb44.VL_MEDIA_RECUPER.Value.ToString() : "";
                    }

                    HabilitaControleAvaliacao(true);                    
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB44_MODULO</returns>
        private TB44_MODULO RetornaEntidade()
        {
            TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb44 == null) ? new TB44_MODULO() : tb44;
        }

        /// <summary>
        /// Método que habilita/desabilita os campos de controle de avaliação
        /// </summary>
        /// <param name="enable">Boolen habilita</param>
        private void HabilitaControleAvaliacao(bool enable) 
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoExclusao)
                enable = false;

            ddlMediaAprovacaoDireta.Enabled = ddlMediaCurso.Enabled = ddlMediaRecuperacao.Enabled = ddlPeriodicidade.Enabled = txtMediaAprovacaoDireta.Enabled = txtMediaCurso.Enabled = txtMediaRecuperacao.Enabled = enable;

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
        #endregion

        #region Carregamento DropDown

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
                          join tb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on tb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals tb54.CO_SGRUP_CTA
                          where tb54.CO_GRUP_CTA == coGrupCta && tb54.CO_SGRUP_CTA == coSGrupCta
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
        /// Método que carrega o DropDown de Conceitos
        /// </summary>
        /// <param name="ddl">DropDown de equivalencia nota/conceito</param>
        /// <param name="controleUnidade">Boolean controle unidade</param>
        private void CarregaConceitos(DropDownList ddl, bool controleUnidade)
        {
            ddl.DataSource = (from tb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                              where tb200.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              && (!controleUnidade || tb200.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP)
                              select new { tb200.CO_SIGLA_CONCEITO, tb200.DE_CONCEITO, tb200.VL_NOTA_MAX }).OrderBy( e => e.CO_SIGLA_CONCEITO );

            ddl.DataTextField = "CO_SIGLA_CONCEITO";
            ddl.DataValueField = "VL_NOTA_MAX";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Centro de Custo
        /// </summary>
        private void CarregaCentrosCusto()
        {
            ddlCentroCusto.Items.Clear();

            var varTb099 = (from tb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                            where tb099.TB14_DEPTO.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                            select new { tb099.CO_CENT_CUSTO, tb099.DE_CENT_CUSTO, tb099.TB14_DEPTO.CO_SIGLA_DEPTO }).OrderBy(r => r.CO_SIGLA_DEPTO).ThenBy(r => r.DE_CENT_CUSTO);

            foreach (var iTb099 in varTb099)
                ddlCentroCusto.Items.Add(new ListItem(iTb099.CO_SIGLA_DEPTO + " - " + iTb099.DE_CENT_CUSTO, iTb099.CO_CENT_CUSTO.ToString()));

            ddlCentroCusto.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion


        protected void ddlTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo2(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupo2ContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlSubGrupo2ContaA, ddlContaContabilA);
        }

        protected void ddlContaContabil_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}