//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE PAIS OU RESPONSÁVEIS 
// OBJETIVO: CADASTRAMENTO DE PAIS OU RESPONSÁVEIS DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/03/2014| Julio Gleisson Rodrigues   |  Copia da Tela \GEDUC\3000_CtrlOperacionalPedagogico\
//           |                            |  3700_CtrlInformacoesResponsaveis\3710_CtrlInformacoesCadastraisResponsaveis
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 27/11/2021| Artur Benevenuto Coelho    | Ajustes de nome conjugue para conjuge
// ----------+----------------------------+-------------------------------------
// 28/11/2021| Artur Benevenuto Coelho    | Reajuste do layout e adição de campos
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 01/12/2021| Artur Benevenuto Coelho    | Copia da Tela \GSAUD\3000_ControleInformacoesUsuario\
//           |                            | 3100_ControleInformacoesCadastraisUsuario\3105_CtrlInformacoesResponsaveis


using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Artem.Web.UI.Controls;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3111_CtrlInformacoesBeneficiarios
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {

                    //------------> Variável que guarda informações da instituição do usuário logado
                    var tb000 = (from iTb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                                 where iTb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new
                                 {
                                     iTb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN,
                                     iTb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO,
                                     iTb000.TB905_BAIRRO.CO_BAIRRO,
                                     iTb000.CEP_CODIGO
                                 }).First();


                    txtDtSituacaoAlu.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDtNascAlu.Text = "01/01/1900";
                    txtIdentidadeAlu.Text = "999999";
                    txtOrgEmissorAlu.Text = "X";
                    txtDtEmissaoAlu.Text = "01/01/1900";
                    ddlIdentidadeUFAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    //
                    txtCepAlu.Text = tb000.CEP_CODIGO.ToString();
                    txtLogradouroAlu.Text = "X";
                    ddlUfAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO.ToString();
                    ddlBairroAlu.SelectedValue = tb000.CO_BAIRRO.ToString();
                    ddlCidadeAlu.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();

                }
                //------------> Define largura e altura da imagem do beneficiário
                upImagemAlu.ImagemLargura = 90;
                upImagemAlu.ImagemAltura = 110;

                CarregaUfs(ddlUfAlu);
                CarregaUfs(ddlIdentidadeUFAlu);
                CarregaUfs(ddlUfNacionalidadeAlu);
                CarregaUfs(ddlUfTituloAlu);
                ddlUfTituloAlu.Items.Insert(0, new ListItem("", ""));
                CarregaUfs(ddlUfEmpAlu);
                ddlUfEmpAlu.Items.Insert(0, new ListItem("", ""));
                ddlUfAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                CarregaCidades(ddlCidadeAlu, ddlUfAlu);
                CarregaCidades(ddlCidadeEmpAlu, ddlUfEmpAlu);
                CarregaBairros(ddlBairroAlu, ddlCidadeAlu);
                CarregaBairros(ddlBairroEmpAlu, ddlCidadeEmpAlu);
                CarregaGrausInstrucao();
                CarregaCursosFormacao();
                CarregaNacionalidades();
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

            int idImagem = upImagemAlu.GravaImagem();
            int intRetorno = 0;
            decimal decimalRetorno = 0;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string strCpfAlu = txtCPFAlu.Text.Replace("-", "").Replace(".", "");

                //------------> Faz a verificação de ocorrência de beneficiário para o CPF informado ( quando inclusão )
                var ocorAlu = from ltb07 in TB07_ALUNO.RetornaTodosRegistros()
                                 where ltb07.NU_CPF_ALU == strCpfAlu
                                 select new { ltb07.CO_ALU };

                if (ocorAlu.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe beneficiário cadastrado com os dados informados.");
                    return;
                }
            }

            TB07_ALUNO tb07 = RetornaEntidadeTb07();
            TBG151_HISTO_SALAR_RESPO tbg151 = null;//--RetornaEntidadeTbg151(tb07.TB108_RESPONSAVEL);
            var cpfAlu = txtCPFAlu.Text.Replace("-", "").Replace(".", "");

            if (tb07.CO_ALU == 0)
            {
                var alu = TB07_ALUNO.RetornaTodosRegistros().Where(r => r.NU_CPF_ALU == cpfAlu).FirstOrDefault();

                if (alu != null)
                    tb07 = alu;
            }

            //--------> Se inclusão, define a FL_INCLU_RESP(flag de inclusão) = true e FL_ALTER_RESP(flag de alteração) = false
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb07.FL_INCLU_ALU = true;
                tb07.FL_ALTER_ALU = false;
                //--tb07.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }

            //--------> Se alteração, define a FL_ALTER_RESP(flag de alteração) = true
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                tb07.FL_ALTER_ALU = true;
            }

            //--------> Bloco 1
            tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(idImagem);
            tb07.NU_CPF_ALU = cpfAlu;
            tb07.NU_NIS = txtNISAlu.Text.Trim() != "" ? (decimal?)decimal.Parse(txtNISAlu.Text) : null;
            tb07.CO_FLAG_ALU_FUNC = chkAluFunc.Checked ? "S" : "N";
            tb07.DT_NASC_ALU = txtDtNascAlu.Text.Trim() != null ? (DateTime?)DateTime.Parse(txtDtNascAlu.Text) : null;
            tb07.NO_ALU = txtNomeAlu.Text.ToUpper();
            tb07.NO_APE_ALU = txtApelidoAlu.Text.ToUpper();
            tb07.CO_SEXO_ALU = ddlSexoAlu.SelectedValue;
            tb07.CO_NACI_ALU = ddlNacionalidadeAlu.SelectedValue;
            tb07.DE_NATU_ALU = txtNaturalidadeAlu.Text != "" ? txtNaturalidadeAlu.Text : null;
            tb07.CO_UF_NATU_ALU = ddlNacionalidadeAlu.SelectedValue == "BR" && ddlUfNacionalidadeAlu.SelectedValue != "" ? ddlUfNacionalidadeAlu.SelectedValue : null;
            tb07.CO_ORIGEM_ALU = ddlOrigemAlu.SelectedValue;
            tb07.CO_INST = int.Parse(ddlGrauInstrucaoAlu.SelectedValue);
            tb07.CO_TIPO_SANGUE_ALU = ddlTpSangueFAlu.SelectedValue != "" ? ddlTpSangueFAlu.SelectedValue : null;
            tb07.CO_STATUS_SANGUE_ALU = ddlStaSangueFAlu.SelectedValue != "" ? ddlStaSangueFAlu.SelectedValue : null;
            tb07.RENDA_FAMILIAR = ddlRendaAlu.SelectedValue;
            tb07.TP_RACA = ddlCorRacaAlu.SelectedValue != "" ? ddlCorRacaAlu.SelectedValue : null;
            tb07.TP_DEF = ddlDeficienciaAlu.SelectedValue;
            tb07.CO_ESTA_CIVIL_ALU = ddlEstadoCivilAlu.SelectedValue != "" ? ddlEstadoCivilAlu.SelectedValue : null;
            /*tb07.QT_MENOR_DEPEN_ALU = int.TryParse(txtQtdMenoresAlu.Text, out intRetorno) ? (int?)intRetorno : null;
            tb07.QT_MAIOR_DEPEN_ALU = int.TryParse(txtQtdMaioresAlu.Text, out intRetorno) ? (int?)intRetorno : null;
            tb07.CO_FLAG_CONJUG_ALU = chkRespFunc.Checked ? "S" : "N";
            tb07.NO_CONJUG_ALU = txtNomeRespAlu.Text != "" ? txtNomeRespAlu.Text : null;
            tb07.DT_NASC_CONJUG_ALU = txtDtRespAlu.Text != "" ? (DateTime?)Convert.ToDateTime(txtDtRespAlu.Text) : null;
            tb07.CO_SEXO_CONJUG_ALU = ddlSexoRespAlu.SelectedValue;
            tb07.NU_CPF_CONJUG_ALU = txtCPFRespAlu.Text.Replace("-", "").Replace(".", "");*/

            //--------> Bloco 2
            tb07.DES_EMAIL_ALU = txtEmailAlu.Text.Trim() != "" ? txtEmailAlu.Text : null;
            tb07.NU_TELE_RESI_ALU = txtTelCelularAlu.Text.Trim() != "" ? txtTelCelularAlu.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb07.FL_CEL_WHATS = chkWA.Checked ? "S" : "N";
            tb07.NU_TELE_CELU_ALU = txtTelResidencialAlu.Text.Trim() != "" ? txtTelResidencialAlu.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb07.CO_RG_ALU = txtIdentidadeAlu.Text;
            tb07.DT_EMIS_RG_ALU = txtDtEmissaoAlu.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtEmissaoAlu.Text) : null;
            tb07.CO_ORG_RG_ALU = txtOrgEmissorAlu.Text;
            tb07.CO_ESTA_RG_ALU = ddlIdentidadeUFAlu.SelectedValue;
            tb07.NU_TIT_ELE = txtNumeroTituloAlu.Text.Trim() != "" ? txtNumeroTituloAlu.Text : null;
            tb07.NU_ZONA_ELE = txtZonaAlu.Text.Trim() != "" ? txtZonaAlu.Text : null;
            tb07.NU_SEC_ELE = txtSecaoAlu.Text.Trim() != "" ? txtSecaoAlu.Text : null;
            tb07.CO_UF_TIT_ELE = ddlUfTituloAlu.SelectedValue;
            tb07.NU_PASSAPORTE_ALU = int.TryParse(txtPassaporteAlu.Text, out intRetorno) ? (int?)intRetorno : null;
            tb07.NU_CARTAO_SAUDE = txtCarteiraSaudeAlu.Text.Trim() != "" ? txtCarteiraSaudeAlu.Text.Trim() : null;
            tb07.NO_MAE_ALU = txtNomeMaeAlu.Text.Trim() != "" ? txtNomeMaeAlu.Text.Trim() : null;
            tb07.NO_PAI_ALU = txtNomePaiAlu.Text.Trim() != "" ? txtNomePaiAlu.Text.Trim() : null;
            tb07.FLA_OBITO_MAE = chkObtP.Checked ? "S" : "N";
            tb07.FLA_OBITO_PAI = chkObtM.Checked ? "S" : "N";

            //--------> Bloco 3
            tb07.NO_EMPR_ALU = txtEmpresaAlu.Text.Trim() != "" ? txtEmpresaAlu.Text : null;
            tb07.DT_ADMI_EMPR_ALU = txtDtAdmissaoAlu.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtAdmissaoAlu.Text) : null;
            //tb07.DT_TERM_ATIV_ALU = txtDtSaidaAlu.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtSaidaAlu.Text) : null;
            tb07.NO_FUNCAO_ALU = ddlFuncaoAlu.SelectedValue.ToString();
            //tb07.NO_FUNCAO_ALU = txtFuncaoAlu.Text.Trim() != "" ? txtFuncaoAlu.Text : null;
            tb07.NO_SETOR_ALU = txtDepartamentoAlu.Text.Trim() != "" ? txtDepartamentoAlu.Text : null;
            tb07.NU_TELE_COME_ALU = txtTelEmpresaAlu.Text.Trim() != "" ? txtTelEmpresaAlu.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb07.FL_TEL_FUNC_WHATS = chkWAFunc.Checked ? "S" : "N";
            tb07.DES_EMAIL_EMP = txtEmailFuncionalAlu.Text.Trim() != "" ? txtEmailFuncionalAlu.Text : null;
            tb07.CO_SITU_ALU = ddlSituacaoAlu.SelectedValue;
            tb07.DT_SITU_ALU = DateTime.Now;
            tb07.DE_ENDE_ALU = txtLogradouroAlu.Text;
            tb07.NU_ENDE_ALU = decimal.TryParse(txtNumeroAlu.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb07.DE_COMP_ALU = txtComplementoAlu.Text.Trim() != "" ? txtComplementoAlu.Text : null;
            tb07.TB905_BAIRRO = int.TryParse(ddlBairroAlu.SelectedValue, out intRetorno) ? TB905_BAIRRO.RetornaPelaChavePrimaria(intRetorno) : null;
            //--tb07.CO_CIDADE = int.TryParse(ddlCidadeAlu.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb07.CO_ESTA_ALU = ddlUfAlu.SelectedValue;
            tb07.CO_CEP_ALU = txtCepAlu.Text.Replace("-", "");
            tb07.CO_FLAG_RESID_PROPR = chkResPro.Checked ? "S" : "N";
            tb07.DE_ENDE_EMPRE_ALU = txtLogradouroEmpAlu.Text.Trim() != "" ? txtLogradouroEmpAlu.Text : null;
            tb07.NU_ENDE_EMPRE_ALU = int.TryParse(txtNumeroEmpAlu.Text, out intRetorno) ? (int?)intRetorno : null;
            tb07.DE_COMP_EMPRE_ALU = txtComplementoEmpAlu.Text.Trim() != "" ? txtComplementoEmpAlu.Text : null;
            //--tb07.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.TryParse(ddlCidadeEmpAlu.SelectedValue, out intRetorno) ? intRetorno : 0);
            tb07.CO_BAIRRO_EMPRE_ALU = int.TryParse(ddlBairroEmpAlu.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            //--tb07.TB74_UF = TB74_UF.RetornaPelaChavePrimaria(ddlUfEmpAlu.SelectedValue);
            tb07.CO_CEP_EMPRE_ALU = txtCepEmpresaAlu.Text.Trim() != "" ? txtCepEmpresaAlu.Text.Replace("-", "") : null;
            tb07.DES_OBS_ALU = txtObservacoesAlu.Text.Trim() != "" ? txtObservacoesAlu.Text.Replace("-", "") : null;
            tb07.DT_ALT_REGISTRO = DateTime.Now;
            tb07.FL_RESTR_ATEND = (chkRestrPlano.Checked ? "S" : "N");
            tb07.DT_VENC_PLAN = txtDtVencimento.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtVencimento.Text) : null;

            tb07.NM_FACEBOOK_ALU = txtFacebookAlu.Text;
            //tb07.NM_TWITTER_ALU = txtTwitterAlu.Text;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb07.FL_NEGAT_CHEQUE = "N";
                tb07.FL_NEGAT_SERASA = "N";
                tb07.FL_NEGAT_SPC = "N";
            }

            CurrentPadraoCadastros.CurrentEntity = tb07;

            if (tbg151 != null)
            {
                tbg151.ANO_MES = DateTime.Now.ToString("yyyyMM");
                tbg151.CO_SITUA = ddlSituacaoAlu.SelectedValue.ToString();
                tbg151.DT_SITUA = DateTime.Now;
                tbg151.VL_PRINC_REND = int.Parse(txtSalarioBruto.Text.Replace(".", "").Replace(",00", ""));
                tbg151.VL_PRINC_DESC = int.Parse(txtDescSalario.Text.Replace(".", "").Replace(",00", ""));
                tbg151.VL_PRINC_LIQU = int.Parse(txtSalarioLiqui.Text.Replace(".", "").Replace(",00", ""));

                tbg151.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tbg151.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_COL, LoginAuxili.CO_EMP);
                tbg151.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL;
            }
        }
        #endregion
        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB07_ALUNO tb07 = RetornaEntidadeTb07();
            TB108_RESPONSAVEL tb108 = null;
            TBG151_HISTO_SALAR_RESPO tbg151 = null;

            if (tb07 != null)
            {
                tb07.ImageReference.Load();
                /*tb07.TB904_CIDADEReference.Load();
                tb07.TB74_UFReference.Load();*/
                tb07.TB905_BAIRROReference.Load();
                tb07.TB108_RESPONSAVELReference.Load();

                if (tb07.Image != null)
                    upImagemAlu.CarregaImagem(tb07.Image.ImageId);
                else
                    upImagemAlu.CarregaImagem(0);

                tb108 = tb07.TB108_RESPONSAVEL;

                if (tb108 != null)
                    tbg151 = RetornaEntidadeTbg151(tb108);

                txtDtVencimento.Text = tb07.DT_VENC_PLAN != null ? tb07.DT_VENC_PLAN.Value.ToString("dd/MM/yyyy") : "";
                chkRestrPlano.Checked = tb07.FL_PENDE_PLANO_CONVE == "S" ? true : false;
                txtCPFAlu.Text = tb07.NU_CPF_ALU;
                txtNomeAlu.Text = tb07.NO_ALU.ToUpper();
                txtNISAlu.Text = tb07.NU_NIS.ToString();
                txtDtNascAlu.Text = tb07.DT_NASC_ALU != null ? tb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                ddlSexoAlu.SelectedValue = tb07.CO_SEXO_ALU;
                ddlEstadoCivilAlu.SelectedValue = tb07.CO_ESTA_CIVIL_ALU != null ? tb07.CO_ESTA_CIVIL_ALU : "";
                ddlDeficienciaAlu.SelectedValue = tb07.TP_DEF;
                ddlGrauInstrucaoAlu.SelectedValue = tb07.CO_INST.ToString();
                //ddlCursoFormacaoAlu.SelectedValue = tb108 != null ? tb108.CO_CURFORM_RESP.ToString() : "";
                ddlRendaAlu.SelectedValue = tb07.RENDA_FAMILIAR;
                txtLogradouroAlu.Text = tb07.DE_ENDE_ALU;
                txtNumeroAlu.Text = tb07.NU_ENDE_ALU.ToString();
                txtComplementoAlu.Text = tb07.DE_COMP_ALU;
                ddlUfAlu.SelectedValue = tb07.CO_ESTA_ALU;
                CarregaCidades(ddlCidadeAlu, ddlUfAlu);
                ddlCidadeAlu.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                CarregaBairros(ddlBairroAlu, ddlCidadeAlu);
                ddlBairroAlu.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_BAIRRO.ToString() : "";
                txtCepAlu.Text = tb07.CO_CEP_ALU;
                txtTelCelularAlu.Text = tb07.NU_TELE_RESI_ALU;
                chkWA.Checked = tb07.FL_CEL_WHATS == "S";
                txtTelResidencialAlu.Text = tb07.NU_TELE_CELU_ALU;
                txtEmailAlu.Text = tb07.DES_EMAIL_ALU;
                /*txtQtdMenoresAlu.Text = tb07.QT_MENOR_DEPEN_RESP.ToString();
                txtQtdMaioresAlu.Text = tb07.QT_MAIOR_DEPEN_RESP.ToString();*/
                txtIdentidadeAlu.Text = tb07.CO_RG_ALU;
                txtDtEmissaoAlu.Text = tb07.DT_EMIS_RG_ALU != null ? tb07.DT_EMIS_RG_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtOrgEmissorAlu.Text = tb07.CO_ORG_RG_ALU;
                ddlIdentidadeUFAlu.SelectedValue = tb07.CO_ESTA_RG_ALU;
                txtNumeroTituloAlu.Text = tb07.NU_TIT_ELE;
                txtZonaAlu.Text = tb07.NU_ZONA_ELE;
                txtSecaoAlu.Text = tb07.NU_SEC_ELE;
                ddlUfTituloAlu.SelectedValue = tb07.CO_UF_TIT_ELE;
                txtEmpresaAlu.Text = tb07.NO_EMPR_ALU;
                txtDepartamentoAlu.Text = tb07.NO_SETOR_ALU;
                CarregaFuncoes(ddlFuncaoAlu, tb07.NO_FUNCAO_ALU);
                //txtFuncaoAlu.Text = tb07.NO_FUNCAO_ALU;
                txtEmailFuncionalAlu.Text = tb07.DES_EMAIL_EMP;
                txtDtAdmissaoAlu.Text = tb07.DT_ADMI_EMPR_ALU != null ? tb07.DT_ADMI_EMPR_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtLogradouroEmpAlu.Text = tb07.DE_ENDE_EMPRE_ALU;
                txtNumeroEmpAlu.Text = tb07.NU_ENDE_EMPRE_ALU.ToString();
                txtComplementoEmpAlu.Text = tb07.DE_COMP_EMPRE_ALU;
                ddlUfEmpAlu.SelectedValue = tb07.CO_UF_EMPRE_ALU != null ? tb07.CO_UF_EMPRE_ALU : "";
                if (ddlUfEmpAlu.SelectedValue != "")
                {
                    CarregaCidades(ddlCidadeEmpAlu, ddlUfEmpAlu);
                    ddlCidadeEmpAlu.SelectedValue = tb07.CO_CIDADE_EMPRE_ALU == null ? "" : tb07.CO_CIDADE_EMPRE_ALU.ToString();
                    CarregaBairros(ddlBairroEmpAlu, ddlCidadeEmpAlu);
                    ddlBairroEmpAlu.SelectedValue = tb07.CO_BAIRRO_EMPRE_ALU.ToString();
                }
                txtCepEmpresaAlu.Text = tb07.CO_CEP_EMPRE_ALU;
                txtTelEmpresaAlu.Text = tb07.NU_TELE_COME_ALU;
                chkWAFunc.Checked = tb07.FL_TEL_FUNC_WHATS == "S";
                chkAluFunc.Checked = tb07.CO_FLAG_ALU_FUNC == "S";
                txtCarteiraSaudeAlu.Text = tb07.NU_CARTAO_SAUDE != null ? tb07.NU_CARTAO_SAUDE.ToString() : "";
                txtApelidoAlu.Text = tb07.NO_APE_ALU;
                ddlNacionalidadeAlu.SelectedValue = tb07.CO_NACI_ALU;
                ddlUfNacionalidadeAlu.Enabled = ddlNacionalidadeAlu.SelectedValue == "BR";
                txtNaturalidadeAlu.Text = tb07.DE_NATU_ALU;
                ddlUfNacionalidadeAlu.SelectedValue = tb07.CO_UF_NATU_ALU != null ? tb07.CO_UF_NATU_ALU : "";
                ddlOrigemAlu.SelectedValue = tb07.CO_ORIGEM_ALU != null ? tb07.CO_ORIGEM_ALU : "SR";
                ddlTpSangueFAlu.SelectedValue = tb07.CO_TIPO_SANGUE_ALU != null ? tb07.CO_TIPO_SANGUE_ALU : "";
                ddlStaSangueFAlu.SelectedValue = tb07.CO_STATUS_SANGUE_ALU != null ? tb07.CO_STATUS_SANGUE_ALU : "";
                ddlCorRacaAlu.SelectedValue = tb07.TP_RACA != null ? tb07.TP_RACA : "";
                if (tb108 != null)
                {
                    chkRespFunc.Checked = tb108.CO_FLAG_RESP_FUNC == "S";
                    txtNomeRespAlu.Text = tb108.NO_RESP;
                    txtDtRespAlu.Text = tb108.DT_NASC_RESP != null ? ((DateTime)tb108.DT_NASC_RESP).ToString("dd/MM/yyyy") : "";
                    ddlSexoRespAlu.SelectedValue = tb108.CO_SEXO_RESP != null ? tb108.CO_SEXO_RESP : "M";
                    txtCPFRespAlu.Text = tb108.NU_CPF_RESP;
                }
                txtPassaporteAlu.Text = tb07.NU_PASSAPORTE_ALU != null ? tb07.NU_PASSAPORTE_ALU.ToString() : "";
                txtNomeMaeAlu.Text = tb07.NO_MAE_ALU;
                txtNomePaiAlu.Text = tb07.NO_PAI_ALU;
                chkObtP.Checked = tb07.FLA_OBITO_MAE == "S";
                chkObtM.Checked = tb07.FLA_OBITO_PAI == "S";
                //txtDtSaidaAlu.Text = tb07.DT_TERM_ATIV_ALU != null ? ((DateTime)tb07.DT_TERM_ATIV_RESP).ToString("dd/MM/yyyy") : "";
                ddlSituacaoAlu.SelectedValue = tb07.CO_SITU_ALU != null ? tb07.CO_SITU_ALU : "A";
                txtDtSituacaoAlu.Text = tb07.DT_SITU_ALU != null ? tb07.DT_SITU_ALU.Value.ToString("dd/MM/yyyy") : "";
                chkResPro.Checked = tb07.CO_FLAG_RESID_PROPR == "S";
                txtObservacoesAlu.Text = tb07.DES_OBS_ALU;

                txtFacebookAlu.Text = tb07.NM_FACEBOOK_ALU;
                //txtTwitterAlu.Text = tb07.NM_TWITTER_ALU;

                txtSalarioBruto.Text = (tbg151 != null ? tbg151.VL_PRINC_REND : 0).ToString().PadLeft(6, '0') + ",00";
                txtDescSalario.Text = (tbg151 != null ? tbg151.VL_PRINC_DESC : 0).ToString().PadLeft(6, '0') + ",00";
                txtSalarioLiqui.Text = (tbg151 != null ? tbg151.VL_PRINC_LIQU : 0).ToString().PadLeft(6, '0') + ",00";

                if (tb07.DE_ENDE_ALU != null)
                {
                    string descEnd = tb07.DE_ENDE_ALU + ",";
                    descEnd = tb07.NU_ENDE_ALU != null ? descEnd + tb07.NU_ENDE_ALU.ToString() + "," : descEnd;
                    carregaMapa(descEnd + ddlCidadeAlu.SelectedItem + " - " + tb07.CO_ESTA_ALU);
                }
                else
                    tbMap.Visible = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB108_RESPONSAVEL</returns>
        private TB07_ALUNO RetornaEntidadeTb07()
        {
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id), LoginAuxili.CO_EMP);
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBG151_HISTO_SALAR_RESPO</returns>
        private TBG151_HISTO_SALAR_RESPO RetornaEntidadeTbg151(TB108_RESPONSAVEL tb108)
        {
            var r = TBG151_HISTO_SALAR_RESPO.RetornaPeloResponsavel(tb108.CO_RESP).FirstOrDefault();
            TBG151_HISTO_SALAR_RESPO tbg151 = null;

            if (r != null)
                tbg151 = TBG151_HISTO_SALAR_RESPO.RetornaPelaChavePrimaria(r.ID_HISTO_SALAR_RESPO);
            else
            {
                tbg151 = new TBG151_HISTO_SALAR_RESPO();
                /*tbg151.TB108_RESPONSAVEL = tb07;
                tbg151.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tbg151.ANO_MES = DateTime.Now.ToString("yyyyMM");*/
            }

            return tbg151;
        }

        /// <summary>
        /// Apresenta o mapa com a localização do endereço do beneficiário
        /// </summary>
        /// <param name="endResponsavel">Endereço do beneficiário</param>
        private void carregaMapa(string endResponsavel)
        {
            if (AuxiliValidacao.IsConnected())
            {

                // PARA ATIVAR O MAP GOOGLE ATIVAR o Visible para true
                //------------> Chave GoogleMaps
                GMapa.Key = ConfigurationManager.AppSettings.Get(AppSettings.GoogleMapsKey);
                tbMap.Visible = true;
                GMapa.Visible = true;
                GMapa.Address = endResponsavel;
                GMapa.Markers.Clear();
                GMapa.Markers.Add(new GoogleMarker(endResponsavel));

                GMapa.Zoom = 15;
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Nacionalidades
        /// </summary>
        private void CarregaNacionalidades()
        {
            ddlNacionalidadeAlu.DataSource = TB299_PAISES.RetornaTodosRegistros();

            ddlNacionalidadeAlu.DataTextField = "NO_PAISES";
            ddlNacionalidadeAlu.DataValueField = "CO_ISO_PAISES";
            ddlNacionalidadeAlu.DataBind();

            ddlNacionalidadeAlu.SelectedValue = "BR";
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddlUFCarreg">DropDown de UF</param>
        private void CarregaUfs(DropDownList ddlUFCarreg)
        {
            ddlUFCarreg.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUFCarreg.DataTextField = "CODUF";
            ddlUFCarreg.DataValueField = "CODUF";
            ddlUFCarreg.DataBind();

            if (ddlUFCarreg == ddlUfNacionalidadeAlu)
            {
                ddlUFCarreg.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        /// <param name="ddlCidadeCarreg">DropDown de cidade</param>
        /// <param name="ddlUFSelec">DropDown de UF</param>
        private void CarregaCidades(DropDownList ddlCidadeCarreg, DropDownList ddlUFSelec)
        {
            ddlCidadeCarreg.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUFSelec.SelectedValue);

            ddlCidadeCarreg.DataTextField = "NO_CIDADE";
            ddlCidadeCarreg.DataValueField = "CO_CIDADE";
            ddlCidadeCarreg.DataBind();

            ddlCidadeCarreg.Enabled = ddlCidadeCarreg.Items.Count > 0;
            ddlCidadeCarreg.Items.Insert(0, "");
        }

        private void CarregaFuncoes(DropDownList ddlFuncaoCarreg)
        {
            ddlFuncaoCarreg.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

            ddlFuncaoCarreg.DataTextField = "NO_FUN";
            ddlFuncaoCarreg.DataValueField = "CO_FUN";
            ddlFuncaoCarreg.DataBind();

            ddlFuncaoCarreg.Enabled = ddlFuncaoCarreg.Items.Count > 0;
            ddlFuncaoCarreg.Items.Insert(0, "");
        }

        private void CarregaFuncoes(DropDownList ddlFuncaoCarreg, String NO_FUN)
        {
            CarregaFuncoes(ddlFuncaoCarreg);

            var r = TB15_FUNCAO.RetornaPeloNome(NO_FUN);

            ddlFuncaoCarreg.SelectedValue = r != null ? r.CO_FUN.ToString() : "";
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        /// <param name="ddlBairroCarreg">DropDown de bairro</param>
        /// <param name="ddlCidadeSelec">DropDown de cidade</param>
        private void CarregaBairros(DropDownList ddlBairroCarreg, DropDownList ddlCidadeSelec)
        {
            int coCidade = ddlCidadeSelec.SelectedValue != "" ? int.Parse(ddlCidadeSelec.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairroCarreg.Enabled = false;
                ddlBairroCarreg.Items.Clear();
                return;
            }
            else
            {
                ddlBairroCarreg.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairroCarreg.DataTextField = "NO_BAIRRO";
                ddlBairroCarreg.DataValueField = "CO_BAIRRO";
                ddlBairroCarreg.DataBind();

                ddlBairroCarreg.Enabled = ddlBairroCarreg.Items.Count > 0;
                ddlBairroCarreg.Items.Insert(0, "");
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Grau de Instrução
        /// </summary>
        private void CarregaGrausInstrucao()
        {
            ddlGrauInstrucaoAlu.DataSource = TB18_GRAUINS.RetornaTodosRegistros();

            ddlGrauInstrucaoAlu.DataTextField = "NO_INST";
            ddlGrauInstrucaoAlu.DataValueField = "CO_INST";
            ddlGrauInstrucaoAlu.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Curso de Formação
        /// </summary>
        private void CarregaCursosFormacao()
        {
            //string grauInst = TB18_GRAUINS.RetornaPelaChavePrimaria(int.Parse(ddlGrauInstrucaoAlu.SelectedValue)).CO_SIGLA_INST;

            //ddlCursoFormacaoAlu.DataSource = TB100_ESPECIALIZACAO.RetornaPeloTipo(grauInst.Substring(0,2)).OrderBy( e => e.DE_ESPEC );

            //ddlCursoFormacaoAlu.DataTextField = "DE_ESPEC";
            //ddlCursoFormacaoAlu.DataValueField = "CO_ESPEC";
            //ddlCursoFormacaoAlu.DataBind();
            //ddlCursoFormacaoAlu.Enabled = ddlCursoFormacaoAlu.Items.Count > 0;
        }
        #endregion

        #region Validadores

        //====> Método que faz a validação de CPF
        protected void cvValidaCPF(object source, ServerValidateEventArgs e)
        {
            string strCpf = e.Value.Replace(".", "").Replace("-", "");
            e.IsValid = AuxiliValidacao.ValidaCpf(strCpf);

            if (!e.IsValid)
            {
                AuxiliPagina.EnvioMensagemErro(this, MensagensErro.CPFInvalido);
            }
        }
        #endregion


        //====> Preenche os campos de endereço do beneficiário de acordo com o CEP, se o mesmo possuir registro na base de dados
        protected void btnPesquisarCepAlu_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepAlu.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepAlu.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroAlu.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfAlu.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeAlu, ddlUfAlu);
                    ddlCidadeAlu.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroAlu, ddlCidadeAlu);
                    ddlBairroAlu.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroAlu.Text = ddlBairroAlu.SelectedValue = ddlCidadeAlu.SelectedValue = "";
                    ddlUfAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    AuxiliPagina.EnvioMensagemErro(this.Page, "CEP não encontrado");
                    return;
                }
            }
        }

        //====> Preenche os campos de endereço da empresa do beneficiário de acordo com o CEP, se o mesmo possuir registro na base de dados
        protected void btnPesquisarCepEmpAlu_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepEmpresaAlu.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepEmpresaAlu.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroEmpAlu.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfEmpAlu.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeEmpAlu, ddlUfEmpAlu);
                    ddlCidadeEmpAlu.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroEmpAlu, ddlCidadeEmpAlu);
                    ddlBairroEmpAlu.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroEmpAlu.Text = ddlBairroEmpAlu.SelectedValue = ddlCidadeEmpAlu.SelectedValue = "";
                    ddlUfEmpAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    AuxiliPagina.EnvioMensagemErro(this.Page, "CEP não encontrado");
                    return;
                }
            }
        }

        protected void ddlGrauInstrucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCursosFormacao();
        }

        protected void ddlNacionalidadeAlu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNacionalidadeAlu.SelectedValue == "BR")
            {
                ddlUfNacionalidadeAlu.Enabled = true;
            }
            else
            {
                ddlUfNacionalidadeAlu.Enabled = false;
                ddlUfNacionalidadeAlu.SelectedValue = "";
            }
        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidadeAlu, ddlUfAlu);
            CarregaBairros(ddlBairroAlu, ddlCidadeAlu);
        }

        protected void ddlUfEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidadeEmpAlu, ddlUfEmpAlu);
            CarregaBairros(ddlBairroEmpAlu, ddlCidadeEmpAlu);
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlBairroAlu, ddlCidadeAlu);
        }

        protected void ddlCidadeEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlBairroEmpAlu, ddlCidadeEmpAlu);
        }

    }
}