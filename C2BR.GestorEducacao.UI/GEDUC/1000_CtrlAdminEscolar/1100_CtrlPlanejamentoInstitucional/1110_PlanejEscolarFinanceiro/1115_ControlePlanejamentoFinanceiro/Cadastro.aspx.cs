//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE GRUPO DE CONTAS CONTÁBIL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 23/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 da conta contábil
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1115_ControlePlanejamentoFinanceiro
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
            if (IsPostBack) return;

            TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            if (tb000 != null)
            {
                tb000.TB149_PARAM_INSTIReference.Load();

                txtInstituicao.Text = tb000.ORG_NOME_ORGAO;

                if (tb000.TB149_PARAM_INSTI.TP_CTRLE_CTA_CONTAB == "U")
                {
                    txtTipoCtrlPlaneFinan.Text = "Unidade Escolar";

                    var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                where iTb25.CO_EMP == LoginAuxili.CO_EMP
                                select new { iTb25.NO_FANTAS_EMP, iTb25.sigla, iTb25.CO_CPFCGC_EMP }).First();

                    txtUnidadeEscolar.Text = tb25.NO_FANTAS_EMP;
                    txtCodIdenticacao.Text = tb25.sigla.ToUpper();
                    txtCNPJ.Text = tb25.CO_CPFCGC_EMP;
                }
                else
                    txtTipoCtrlPlaneFinan.Text = "Instituição de Ensino";
            }

            CarregaAno();
            CarregaDotacaoOrcamentaria();
            CarregaCentroCusto();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                if (LoginAuxili.CO_COL > 0)
                {
                    var tb03 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                    tb03.TB25_EMPRESAReference.Load();

                    txtSiglaEmprRespoCadas.Text = tb03.TB25_EMPRESA.sigla.ToUpper();
                    txtRespoCadas.Text = tb03.NO_COL;
                    txtSiglaEmprRespoStatus.Text = tb03.TB25_EMPRESA.sigla.ToUpper();
                    txtRespoStatus.Text = tb03.NO_COL;
                }
                
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {                        
            if (!Page.IsValid)
            {
                return;
            }

            TB111_PLANEJ_FINAN tb111 = RetornaEntidade();

            if (tb111 == null)
            {
                int anoRefer = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;
                int dotacOrcam = ddlDotacOrcam.SelectedValue != "" ? int.Parse(ddlDotacOrcam.SelectedValue) : 0;
                int ctaContab = ddlContaContabil.SelectedValue != "" ? int.Parse(ddlContaContabil.SelectedValue) : 0;
                int centrCusto = ddlCentroCusto.SelectedValue != "" ? int.Parse(ddlCentroCusto.SelectedValue) : 0;

                var verifOcorr = (from iTb111 in TB111_PLANEJ_FINAN.RetornaTodosRegistros()
                                  where anoRefer != 0 ? iTb111.CO_ANO_REF == anoRefer : anoRefer == 0
                                  && dotacOrcam != 0 ? iTb111.TB305_DOTAC_ORCAM.ID_DOTAC_ORCAM == dotacOrcam : dotacOrcam == 0
                                  && ctaContab != 0 ? iTb111.TB56_PLANOCTA.CO_SEQU_PC == ctaContab : ctaContab == 0
                                  && centrCusto != 0 ? iTb111.TB099_CENTRO_CUSTO.CO_CENT_CUSTO == centrCusto : centrCusto == 0
                                  select iTb111);

                if (verifOcorr.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Planejamento informado já cadastrado.");
                    return;
                }

                tb111 = new TB111_PLANEJ_FINAN();

                tb111.DT_CADASTRO = DateTime.Now;
                tb111.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            }

            decimal retornaDecimal = 0;
            TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            tb111.TB000_INSTITUICAO = tb000;

            if (tb000 != null)
            {
                tb000.TB149_PARAM_INSTIReference.Load();

                if (tb000.TB149_PARAM_INSTI.TP_CTRLE_CTA_CONTAB == "U")
                {
                    tb111.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                }                
            }

            tb111.CO_ANO_REF = int.Parse(ddlAnoRefer.Text);
            tb111.TB305_DOTAC_ORCAM = TB305_DOTAC_ORCAM.RetornaPelaChavePrimaria(int.Parse(ddlDotacOrcam.SelectedValue));
            tb111.TB56_PLANOCTA = ddlContaContabil.SelectedValue != "" ? TB56_PLANOCTA.RetornaPelaChavePrimaria(int.Parse(ddlContaContabil.SelectedValue)) : null;
            tb111.TB099_CENTRO_CUSTO = ddlCentroCusto.SelectedValue != "" ? TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(int.Parse(ddlCentroCusto.SelectedValue)) : null;

            tb111.VL_PLAN_MES1 = decimal.TryParse(txtValorJanei.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES2 = decimal.TryParse(txtValorFever.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES3 = decimal.TryParse(txtValorMarco.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES4 = decimal.TryParse(txtValorAbril.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES5 = decimal.TryParse(txtValorMaio.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES6 = decimal.TryParse(txtValorJunho.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES7 = decimal.TryParse(txtValorJulho.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES8 = decimal.TryParse(txtValorAgost.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES9 = decimal.TryParse(txtValorSetem.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES10 = decimal.TryParse(txtValorOutub.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES11 = decimal.TryParse(txtValorNovem.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
            tb111.VL_PLAN_MES12 = decimal.TryParse(txtValorDezem.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;

            tb111.TB03_COLABOR1 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tb111.DT_STATUS = DateTime.Now;
            tb111.CO_STATUS = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb111;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB111_PLANEJ_FINAN tb111 = RetornaEntidade();

            if (tb111 != null)
            {
                tb111.TB305_DOTAC_ORCAMReference.Load();
                tb111.TB56_PLANOCTAReference.Load();
                tb111.TB099_CENTRO_CUSTOReference.Load();
//------------> Funcionário Cadastro
                tb111.TB03_COLABORReference.Load();
//------------> Funcionário Status
                tb111.TB03_COLABOR1Reference.Load();

                ddlAnoRefer.Enabled = ddlDotacOrcam.Enabled = ddlTipoCtaContab.Enabled = ddlGrupoCtaContab.Enabled = ddlSubGrupoCtaContab.Enabled =
                ddlContaContabil.Enabled = ddlCentroCusto.Enabled = false;

                ddlAnoRefer.SelectedValue = tb111.CO_ANO_REF.ToString();
                CarregaDotacaoOrcamentaria();
                ddlDotacOrcam.SelectedValue = tb111.TB305_DOTAC_ORCAM.ID_DOTAC_ORCAM.ToString();
                PreencheCamposDotacao(tb111.TB305_DOTAC_ORCAM.ID_DOTAC_ORCAM);

                if (tb111.TB56_PLANOCTA != null)
                {
                    TB56_PLANOCTA planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb111.TB56_PLANOCTA.CO_SEQU_PC);
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                    ddlTipoCtaContab.SelectedValue = planoConta.TB54_SGRP_CTA.TB53_GRP_CTA.TP_GRUP_CTA;
                    CarregaGrupo();
                    ddlGrupoCtaContab.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo();
                    ddlSubGrupoCtaContab.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2();
                    ddlSubGrupo2CtaContab.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta();
                    ddlContaContabil.SelectedValue = tb111.TB56_PLANOCTA.CO_SEQU_PC.ToString();
                    txtContaContab.Text = planoConta.DE_CONTA_PC;
                }

                if (tb111.TB099_CENTRO_CUSTO != null)
                {
                    ddlCentroCusto.SelectedValue = tb111.TB099_CENTRO_CUSTO.CO_CENT_CUSTO.ToString();
                    txtCentroCusto.Text = tb111.TB099_CENTRO_CUSTO.DE_CENT_CUSTO;
                }

                txtValorJanei.Text = tb111.VL_PLAN_MES1 != null ? tb111.VL_PLAN_MES1.Value.ToString("N2") : "";
                txtValorFever.Text = tb111.VL_PLAN_MES2 != null ? tb111.VL_PLAN_MES2.Value.ToString("N2") : "";
                txtValorMarco.Text = tb111.VL_PLAN_MES3 != null ? tb111.VL_PLAN_MES3.Value.ToString("N2") : "";
                txtValorAbril.Text = tb111.VL_PLAN_MES4 != null ? tb111.VL_PLAN_MES4.Value.ToString("N2") : "";
                txtValorMaio.Text = tb111.VL_PLAN_MES5 != null ? tb111.VL_PLAN_MES5.Value.ToString("N2") : "";
                txtValorJunho.Text = tb111.VL_PLAN_MES6 != null ? tb111.VL_PLAN_MES6.Value.ToString("N2") : "";
                txtValorJulho.Text = tb111.VL_PLAN_MES7 != null ? tb111.VL_PLAN_MES7.Value.ToString("N2") : "";
                txtValorAgost.Text = tb111.VL_PLAN_MES8 != null ? tb111.VL_PLAN_MES8.Value.ToString("N2") : "";
                txtValorSetem.Text = tb111.VL_PLAN_MES9 != null ? tb111.VL_PLAN_MES9.Value.ToString("N2") : "";
                txtValorOutub.Text = tb111.VL_PLAN_MES10 != null ? tb111.VL_PLAN_MES10.Value.ToString("N2") : "";
                txtValorNovem.Text = tb111.VL_PLAN_MES11 != null ? tb111.VL_PLAN_MES11.Value.ToString("N2") : "";
                txtValorDezem.Text = tb111.VL_PLAN_MES12 != null ? tb111.VL_PLAN_MES12.Value.ToString("N2") : "";

                CalculaValores();

                if (tb111.TB03_COLABOR != null)
                {
                    tb111.TB03_COLABOR.TB25_EMPRESAReference.Load();

                    txtSiglaEmprRespoCadas.Text = tb111.TB03_COLABOR.TB25_EMPRESA.sigla.ToUpper();
                    txtRespoCadas.Text = tb111.TB03_COLABOR.NO_COL;
                }

                if (tb111.TB03_COLABOR1 != null)
                {
                    tb111.TB03_COLABOR1.TB25_EMPRESAReference.Load();

                    txtSiglaEmprRespoStatus.Text = tb111.TB03_COLABOR1.TB25_EMPRESA.sigla.ToUpper();
                    txtRespoStatus.Text = tb111.TB03_COLABOR1.NO_COL;
                }

                txtDtSituacao.Text = tb111.DT_STATUS.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tb111.CO_STATUS;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB111_PLANEJ_FINAN</returns>
        private TB111_PLANEJ_FINAN RetornaEntidade()
        {
            return TB111_PLANEJ_FINAN.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Método que preenche informações da Dotação selecionada
        /// </summary>
        /// <param name="idDotacao">Id da dotação orçamentária</param>
        private void PreencheCamposDotacao(int idDotacao)
        {
            LimpaCamposDotacao();

            var resultado = (from tb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                             where tb305.ID_DOTAC_ORCAM == idDotacao
                             select new
                             {
                                 tb305.VL_PLANE_DOTAC_ORCAM,
                                 tb305.TB304_ORIGE_FINAN.SIGLA_ORIGE_FINAN,
                                 tb305.NO_TITUL_DOTAC_ORCAM,
                                 tb305.ID_DOTAC_ORCAM
                             }).First();

            txtValorDotacOrcam.Text = resultado.VL_PLANE_DOTAC_ORCAM.ToString("N2");
            txtOrigeFinan.Text = resultado.SIGLA_ORIGE_FINAN;
            txtTitulDotacOrcam.Text = resultado.NO_TITUL_DOTAC_ORCAM;

            int anoRef = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;

            var valorTotalDotacOrcam = (from iTb111 in TB111_PLANEJ_FINAN.RetornaTodosRegistros()
                                        where iTb111.CO_ANO_REF == anoRef
                                        && iTb111.TB305_DOTAC_ORCAM.ID_DOTAC_ORCAM == idDotacao
                                        select new
                                        {
                                            TOTAL = (iTb111.VL_PLAN_MES1 + iTb111.VL_PLAN_MES2 + iTb111.VL_PLAN_MES3 +
                                                    iTb111.VL_PLAN_MES4 + iTb111.VL_PLAN_MES5 + iTb111.VL_PLAN_MES6 +
                                                    iTb111.VL_PLAN_MES7 + iTb111.VL_PLAN_MES8 + iTb111.VL_PLAN_MES9 +
                                                    iTb111.VL_PLAN_MES10 + iTb111.VL_PLAN_MES11 + iTb111.VL_PLAN_MES12)
                                        }).Sum(q => q.TOTAL);

            if (valorTotalDotacOrcam > 0)
            {
                decimal resulDifer = resultado.VL_PLANE_DOTAC_ORCAM - valorTotalDotacOrcam.Value;

                if (resulDifer > 0)
                {
                    txtValorDispoDotacOrcam.Text = "+ " + resulDifer.ToString("N2");
                    txtValorDispoDotacOrcam.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    txtValorDispoDotacOrcam.Text = resulDifer.ToString("N2");
                    txtValorDispoDotacOrcam.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
                txtValorDispoDotacOrcam.Text = "";
        }

        /// <summary>
        /// Método que limpa informações da Dotação
        /// </summary>
        private void LimpaCamposDotacao()
        {
            txtValorDotacOrcam.Text = "";
            txtOrigeFinan.Text = "";
            txtValorDispoDotacOrcam.Text = "";
            txtTitulDotacOrcam.Text = "";
        }

        /// <summary>
        /// Método que preenche valor dos campos Total, Total semestre 1 e Total semestre 2
        /// </summary>
        private void CalculaValores()
        {
            decimal valorSemes1, valorSemes2 = 0;

            valorSemes1 = txtValorJanei.Text != "" ? decimal.Parse(txtValorJanei.Text) : 0;
            valorSemes1 = valorSemes1 + (txtValorFever.Text != "" ? decimal.Parse(txtValorFever.Text) : 0);
            valorSemes1 = valorSemes1 + (txtValorMarco.Text != "" ? decimal.Parse(txtValorMarco.Text) : 0);
            valorSemes1 = valorSemes1 + (txtValorAbril.Text != "" ? decimal.Parse(txtValorAbril.Text) : 0);
            valorSemes1 = valorSemes1 + (txtValorMaio.Text != "" ? decimal.Parse(txtValorMaio.Text) : 0);
            valorSemes1 = valorSemes1 + (txtValorJunho.Text != "" ? decimal.Parse(txtValorJunho.Text) : 0);

            valorSemes2 = txtValorJulho.Text != "" ? decimal.Parse(txtValorJulho.Text) : 0;
            valorSemes2 = valorSemes2 + (txtValorAgost.Text != "" ? decimal.Parse(txtValorAgost.Text) : 0);
            valorSemes2 = valorSemes2 + (txtValorSetem.Text != "" ? decimal.Parse(txtValorSetem.Text) : 0);
            valorSemes2 = valorSemes2 + (txtValorOutub.Text != "" ? decimal.Parse(txtValorOutub.Text) : 0);
            valorSemes2 = valorSemes2 + (txtValorNovem.Text != "" ? decimal.Parse(txtValorNovem.Text) : 0);
            valorSemes2 = valorSemes2 + (txtValorDezem.Text != "" ? decimal.Parse(txtValorDezem.Text) : 0);

            if (valorSemes1 > 0)
            {
                txtValorTotalSemes1.Text = valorSemes1.ToString("N2");
            }

            if (valorSemes2 > 0)
            {
                txtValorTotalSemes2.Text = valorSemes2.ToString("N2");
            }

            if (valorSemes1 + valorSemes2 > 0)
            {
                txtTotalMensal.Text = (valorSemes1 + valorSemes2).ToString("N2");
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Ano
        /// </summary>
        private void CarregaAno()
        {
            ddlAnoRefer.DataSource = (from tb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                                      select new { tb305.CO_ANO_REF }).OrderByDescending( d => d.CO_ANO_REF );

            ddlAnoRefer.DataTextField = "CO_ANO_REF";
            ddlAnoRefer.DataValueField = "CO_ANO_REF";
            ddlAnoRefer.DataBind();
        }

        /// <summary>
        /// Método que carrega dropdown de Dotaçao Orçamentária
        /// </summary>
        private void CarregaDotacaoOrcamentaria()
        {
            LimpaCamposDotacao();
            int anoRefer = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;

            var resultado = (from tb305 in TB305_DOTAC_ORCAM.RetornaTodosRegistros()
                             where tb305.CO_ANO_REF == anoRefer
                             select new 
                             { 
                                 tb305.ID_DOTAC_ORCAM, tb305.CO_ANO_REF, tb305.TB261_SUBGRUPO.CO_SUBGRUPO,
                                 tb305.TB261_SUBGRUPO.TB260_GRUPO.CO_GRUPO, tb305.CO_DOTAC_ORCAM
                             }).ToList();

            ddlDotacOrcam.DataSource = (from result in resultado
                                        select new
                                        {
                                            result.ID_DOTAC_ORCAM,
                                            CO_DOTAC_ORCAM = string.Format("{0}.{1}.{2}.{3}",
                                            result.CO_ANO_REF.ToString("0000"), result.CO_GRUPO,
                                            result.CO_SUBGRUPO, result.CO_DOTAC_ORCAM.ToString("000"))
                                        }).OrderBy(r => r.CO_DOTAC_ORCAM);

            ddlDotacOrcam.DataTextField = "CO_DOTAC_ORCAM";
            ddlDotacOrcam.DataValueField = "ID_DOTAC_ORCAM";
            ddlDotacOrcam.DataBind();

            ddlDotacOrcam.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupos de Conta
        /// </summary>
        private void CarregaGrupo()
        {
            var resultado = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                             where tb53.TP_GRUP_CTA == ddlTipoCtaContab.SelectedValue
                             && tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { tb53.NR_GRUP_CTA, tb53.CO_GRUP_CTA, tb53.DE_GRUP_CTA }).ToList();

            ddlGrupoCtaContab.DataSource = (from result in resultado
                                            select new
                                            {
                                                result.CO_GRUP_CTA,
                                                NR_GRUP_CTA = result.NR_GRUP_CTA.Value.ToString("00") + " - " +result.DE_GRUP_CTA
                                            }).OrderBy(r => r.NR_GRUP_CTA);

            ddlGrupoCtaContab.DataTextField = "NR_GRUP_CTA";
            ddlGrupoCtaContab.DataValueField = "CO_GRUP_CTA";
            ddlGrupoCtaContab.DataBind();

            ddlGrupoCtaContab.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo de Conta
        /// </summary>
        private void CarregaSubGrupo()
        {
            int coGrupCta = ddlGrupoCtaContab.SelectedValue != "" ? int.Parse(ddlGrupoCtaContab.SelectedValue) : 0;

            var resultado = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                             where tb54.CO_GRUP_CTA == coGrupCta
                             select new { tb54.NR_SGRUP_CTA, tb54.CO_SGRUP_CTA, tb54.DE_SGRUP_CTA }).ToList();

            ddlSubGrupoCtaContab.DataSource = (from result in resultado
                                               select new
                                               {
                                                   result.CO_SGRUP_CTA,
                                                   NR_SGRUP_CTA = result.NR_SGRUP_CTA.Value.ToString("000") + " - " + result.DE_SGRUP_CTA
                                               }).OrderBy(r => r.NR_SGRUP_CTA);

            ddlSubGrupoCtaContab.DataTextField = "NR_SGRUP_CTA";
            ddlSubGrupoCtaContab.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupoCtaContab.DataBind();

            ddlSubGrupoCtaContab.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo2 de Conta
        /// </summary>
        private void CarregaSubGrupo2()
        {
            int coGrupCta = ddlGrupoCtaContab.SelectedValue != "" ? int.Parse(ddlGrupoCtaContab.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupoCtaContab.SelectedValue != "" ? int.Parse(ddlSubGrupoCtaContab.SelectedValue) : 0;

            var result = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                          join tb54 in TB54_SGRP_CTA.RetornaTodosRegistros() on tb055.TB54_SGRP_CTA.CO_SGRUP_CTA equals tb54.CO_SGRUP_CTA
                          where tb54.CO_GRUP_CTA == coGrupCta && tb54.CO_SGRUP_CTA == coSGrupCta
                          select new
                          {
                              tb055.NR_SGRUP2_CTA,
                              tb055.DE_SGRUP2_CTA,
                              tb055.CO_SGRUP2_CTA
                          }).ToList();

            ddlSubGrupo2CtaContab.DataSource = (from res in result
                                       select new
                                       {
                                           DE_SGRUP2_CTA = res.NR_SGRUP2_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP2_CTA,
                                           res.CO_SGRUP2_CTA
                                       });

            ddlSubGrupo2CtaContab.DataTextField = "DE_SGRUP2_CTA";
            ddlSubGrupo2CtaContab.DataValueField = "CO_SGRUP2_CTA";
            ddlSubGrupo2CtaContab.DataBind();

            ddlSubGrupo2CtaContab.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contabil
        /// </summary>
        private void CarregaConta()
        {
            int coGrupCta = ddlGrupoCtaContab.SelectedValue != "" ? int.Parse(ddlGrupoCtaContab.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupoCtaContab.SelectedValue != "" ? int.Parse(ddlSubGrupoCtaContab.SelectedValue) : 0;
            int coSGrup2Cta = ddlSubGrupo2CtaContab.SelectedValue != "" ? int.Parse(ddlSubGrupo2CtaContab.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta && tb56.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta
                             && (coSGrup2Cta != 0 ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSGrup2Cta : 0 == 0)
                             select new { tb56.NU_CONTA_PC, tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).ToList();

            ddlContaContabil.DataSource = (from result in resultado
                                           select new
                                           {
                                               result.CO_SEQU_PC,
                                               NU_CONTA_PC = result.NU_CONTA_PC.Value.ToString("0000") + " - " + result.DE_CONTA_PC
                                           }).OrderBy(r => r.NU_CONTA_PC);

            ddlContaContabil.DataTextField = "NU_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_SEQU_PC";
            ddlContaContabil.DataBind();

            ddlContaContabil.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega os dropdowns de Centro de Custo
        /// </summary>
        private void CarregaCentroCusto()
        {
            var resultado = (from lTb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                             where lTb099.TB14_DEPTO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { lTb099.NU_CTA_CENT_CUSTO, lTb099.CO_CENT_CUSTO, lTb099.DE_CENT_CUSTO }).ToList();

            ddlCentroCusto.DataSource = (from result in resultado
                                         select new
                                         {
                                             result.CO_CENT_CUSTO, NU_CTA_CENT_CUSTO = result.NU_CTA_CENT_CUSTO + " - " + result.DE_CENT_CUSTO
                                         }).OrderBy(r => r.NU_CTA_CENT_CUSTO);

            ddlCentroCusto.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCusto.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCusto.DataBind();

            ddlCentroCusto.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region Validações

        protected void cvContaContabil_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoCtaContab.SelectedValue != "")
            {
                if (ddlSubGrupoCtaContab.SelectedValue == "" || ddlContaContabil.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }
        #endregion

        protected void ddlDotacOrcam_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDotacOrcam = ddlDotacOrcam.SelectedValue != "" ? int.Parse(ddlDotacOrcam.SelectedValue) : 0;

            if (idDotacOrcam > 0)
            {
                PreencheCamposDotacao(idDotacOrcam);
            }
            else
            {
                txtValorDotacOrcam.Text = "";
                txtOrigeFinan.Text = "";
                txtTitulDotacOrcam.Text = "";
                txtValorDispoDotacOrcam.Text = "";
            }
        }

        protected void ddlTipoCtaContab_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtContaContab.Text = "";
            CarregaGrupo();
            CarregaSubGrupo();
            CarregaSubGrupo2();
            CarregaConta();
        }

        protected void ddlGrupoCtaContab_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtContaContab.Text = "";
            CarregaSubGrupo();
            CarregaSubGrupo2();
            CarregaConta();
        }

        protected void ddlSubGrupoCtaContab_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtContaContab.Text = "";
            CarregaSubGrupo2();
            CarregaConta();
        }

        protected void ddlSubGrupo2CtaContab_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtContaContab.Text = "";
            CarregaConta();
        }

        protected void ddlContaContabil_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabil.SelectedValue != "" ? int.Parse(ddlContaContabil.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).First();

            txtContaContab.Text = resultado.DE_CONTA_PC;
        }

        protected void ddlCentroCusto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCentroCusto = ddlCentroCusto.SelectedValue != "" ? int.Parse(ddlCentroCusto.SelectedValue) : 0;

            var resultado = (from lTb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                             where lTb099.CO_CENT_CUSTO == idCentroCusto
                             select new { lTb099.DE_CENT_CUSTO }).First();

            txtCentroCusto.Text = resultado.DE_CENT_CUSTO;
        }

        protected void txtValorJanei_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorFever_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorMarco_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorAbril_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorMaio_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorJunho_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorJulho_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorAgost_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorSetem_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorOutub_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorNovem_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorDezem_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void lnkAtualizaValor_Click(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDotacaoOrcamentaria();
        }
    }
}