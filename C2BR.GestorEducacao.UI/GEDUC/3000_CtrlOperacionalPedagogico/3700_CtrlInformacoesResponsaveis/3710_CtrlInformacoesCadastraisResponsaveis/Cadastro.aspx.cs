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
// 12/03/2013| André Nobre Vinagre        | Colocado para carregar o mapa do GoogleMaps.
//           |                            | 
//           |                            | 
//-----------+----------------------------+---------------------------------------
// 03/06/2013| Thales Pinho Andrade       | Desativando o Map Google
//           |                            |
//           |                            |             
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3700_CtrlInformacoesResponsaveis.F3710_CtrlInformacoesCadastraisResponsaveis
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
                    
                    
                    txtDtSituacaoResp.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDtNascResp.Text = "01/01/1900";
                    txtIdentidadeResp.Text = "999999";
                    txtOrgEmissorResp.Text = "X";
                    txtDtEmissaoResp.Text = "01/01/1900";
                    ddlIdentidadeUFResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    //
                    txtCepResp.Text = tb000.CEP_CODIGO.ToString();
                    txtLogradouroResp.Text = "X";
                    ddlUfResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO.ToString();
                    ddlBairroResp.SelectedValue = tb000.CO_BAIRRO.ToString();
                    ddlCidadeResp.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();

                }
//------------> Define largura e altura da imagem do responsável
                upImagemResp.ImagemLargura = 90;
                upImagemResp.ImagemAltura = 110;

                CarregaUfs(ddlUfResp);
                CarregaUfs(ddlIdentidadeUFResp);
                CarregaUfs(ddlUfNacionalidadeResp);
                CarregaUfs(ddlUfTituloResp);
                ddlUfTituloResp.Items.Insert(0, new ListItem("", ""));
                CarregaUfs(ddlUfEmpResp);
                ddlUfEmpResp.Items.Insert(0, new ListItem("", ""));
                ddlUfResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                CarregaCidades(ddlCidadeResp, ddlUfResp);
                CarregaCidades(ddlCidadeEmpResp, ddlUfEmpResp);
                CarregaBairros(ddlBairroResp, ddlCidadeResp);
                CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
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

            int idImagem = upImagemResp.GravaImagem();            
            int intRetorno = 0;
            decimal decimalRetorno = 0;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string strCpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");

//------------> Faz a verificação de ocorrência de responsável para o CPF informado ( quando inclusão )
                var ocorRespon = from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                 where lTb108.NU_CPF_RESP == strCpfResp
                                 select new { lTb108.CO_RESP };

                if (ocorRespon.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe responsável cadastrado com os dados informados.");
                    return;
                }    
            }

            TB108_RESPONSAVEL tb108 = RetornaEntidade();

//--------> Se inclusão, define a FL_INCLU_RESP(flag de inclusão) = true e FL_ALTER_RESP(flag de alteração) = false
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb108.FL_INCLU_RESP = true;
                tb108.FL_ALTER_RESP = false;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }

//--------> Se alteração, define a FL_ALTER_RESP(flag de alteração) = true
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                tb108.FL_ALTER_RESP = true;
            }

//--------> Bloco 1
            tb108.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(idImagem);
            tb108.NU_CPF_RESP = txtCPFResp.Text.Replace(".", "").Replace("-", "");
            tb108.NU_NIS_RESP = txtNISResp.Text.Trim() != "" ? (decimal?)decimal.Parse(txtNISResp.Text) : null;
            tb108.CO_FLAG_RESP_FUNC = chkRespFunc.Checked ? "S" : "N";
            tb108.DT_NASC_RESP = txtDtNascResp.Text.Trim() != null ? (DateTime?)DateTime.Parse(txtDtNascResp.Text) : null;
            tb108.NO_RESP = txtNomeResp.Text.ToUpper();
            tb108.NO_APELIDO_RESP = txtApelidoResp.Text.ToUpper();
            tb108.CO_SEXO_RESP = ddlSexoResp.SelectedValue;
            tb108.CO_NACI_RESP = ddlNacionalidadeResp.SelectedValue;
            tb108.DE_NATU_RESP = txtNaturalidadeResp.Text != "" ? txtNaturalidadeResp.Text : null;
            tb108.CO_UF_NATU_RESP = ddlNacionalidadeResp.SelectedValue == "BR" && ddlUfNacionalidadeResp.SelectedValue  != ""? ddlUfNacionalidadeResp.SelectedValue : null;
            tb108.CO_ORIGEM_RESP = ddlOrigemResp.SelectedValue;
            tb108.CO_INST = int.Parse(ddlGrauInstrucaoResp.SelectedValue);
            tb108.CO_TIPO_SANGUE_RESP = ddlTpSangueFResp.SelectedValue != "" ? ddlTpSangueFResp.SelectedValue : null;
            tb108.CO_STATUS_SANGUE_RESP = ddlStaSangueFResp.SelectedValue != "" ? ddlStaSangueFResp.SelectedValue : null;
            tb108.RENDA_FAMILIAR_RESP = ddlRendaResp.SelectedValue;
            tb108.TP_RACA_RESP = ddlCorRacaResp.SelectedValue != "" ? ddlCorRacaResp.SelectedValue : null;
            tb108.QT_MENOR_DEPEN_RESP = int.TryParse(txtQtdMenoresResp.Text, out intRetorno) ? (int?)intRetorno : null;
            tb108.TP_DEF_RESP = ddlDeficienciaResp.SelectedValue;
            tb108.QT_MAIOR_DEPEN_RESP = int.TryParse(txtQtdMaioresResp.Text, out intRetorno) ? (int?)intRetorno : null;
            tb108.CO_ESTADO_CIVIL_RESP = ddlEstadoCivilResp.SelectedValue != "" ? ddlEstadoCivilResp.SelectedValue : null;
            tb108.CO_FLAG_CONJUG_RESP = chkConjFunc.Checked ? "S" : "N";
            tb108.NO_CONJUG_RESP = txtNomeConjugueResp.Text != "" ? txtNomeConjugueResp.Text : null;
            tb108.DT_NASC_CONJUG_RESP = txtDtConjuResp.Text != "" ? (DateTime?)Convert.ToDateTime(txtDtConjuResp.Text) : null;
            tb108.CO_SEXO_CONJUG_RESP = ddlSexoConjugResp.SelectedValue;
            tb108.NU_CPF_CONJUG_RESP = txtCPFConjugResp.Text.Replace("-", "").Replace(".", "");     
            
//--------> Bloco 2
            tb108.DES_EMAIL_RESP = txtEmailResp.Text.Trim() != "" ? txtEmailResp.Text : null;
            tb108.NU_TELE_RESI_RESP = txtTelCelularResp.Text.Trim() != "" ? txtTelCelularResp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb108.NU_TELE_CELU_RESP = txtTelResidencialResp.Text.Trim() != "" ? txtTelResidencialResp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb108.CO_RG_RESP = txtIdentidadeResp.Text;
            tb108.DT_EMIS_RG_RESP = txtDtEmissaoResp.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtEmissaoResp.Text) : null;
            tb108.CO_ORG_RG_RESP = txtOrgEmissorResp.Text;
            tb108.CO_ESTA_RG_RESP = ddlIdentidadeUFResp.SelectedValue;
            tb108.NU_TIT_ELE = txtNumeroTituloResp.Text.Trim() != "" ? txtNumeroTituloResp.Text : null;
            tb108.NU_ZONA_ELE = txtZonaResp.Text.Trim() != "" ? txtZonaResp.Text : null;
            tb108.NU_SEC_ELE = txtSecaoResp.Text.Trim() != "" ? txtSecaoResp.Text : null;
            tb108.CO_UF_TIT_ELE_RESP = ddlUfTituloResp.SelectedValue;
            tb108.NU_PASSAPORTE_RESP = int.TryParse(txtPassaporteResp.Text, out intRetorno) ? (int?)intRetorno : null;
            tb108.NR_CARTEI_SAUDE_RESP = txtCarteiraSaudeResp.Text.Trim() != "" ? txtCarteiraSaudeResp.Text.Trim() : null;
            tb108.NO_MAE_RESP = txtNomeMaeResp.Text.Trim() != "" ? txtNomeMaeResp.Text.Trim() : null;
            tb108.NO_PAI_RESP = txtNomePaiResp.Text.Trim() != "" ? txtNomePaiResp.Text.Trim() : null;
            
//--------> Bloco 3
            tb108.NO_EMPR_RESP = txtEmpresaResp.Text.Trim() != "" ? txtEmpresaResp.Text : null;
            tb108.DT_ADMIS_RESP = txtDtAdmissaoResp.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtAdmissaoResp.Text) : null;
            tb108.DT_TERM_ATIV_RESP = txtDtSaidaResp.Text.Trim() != "" ? (DateTime?)DateTime.Parse(txtDtSaidaResp.Text) : null;
            tb108.NO_FUNCAO_RESP = txtFuncaoResp.Text.Trim() != "" ? txtFuncaoResp.Text : null;
            tb108.NO_SETOR_RESP = txtDepartamentoResp.Text.Trim() != "" ? txtDepartamentoResp.Text : null;
            tb108.NU_TELE_COME_RESP = txtTelEmpresaResp.Text.Trim() != "" ? txtTelEmpresaResp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb108.DES_EMAIL_EMP = txtEmailFuncionalResp.Text.Trim() != "" ? txtEmailFuncionalResp.Text : null;
            tb108.CO_SITU_RESP = ddlSituacaoResp.SelectedValue;
            tb108.DT_SITU_RESP = DateTime.Now;
            tb108.DE_ENDE_RESP = txtLogradouroResp.Text;
            tb108.NU_ENDE_RESP = decimal.TryParse(txtNumeroResp.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb108.DE_COMP_RESP = txtComplementoResp.Text.Trim() != "" ? txtComplementoResp.Text : null;
            tb108.CO_CIDADE = int.TryParse(ddlCidadeResp.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb108.CO_BAIRRO = int.TryParse(ddlBairroResp.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb108.CO_ESTA_RESP = ddlUfResp.SelectedValue;
            tb108.CO_CEP_RESP = txtCepResp.Text.Replace("-", "");
            tb108.CO_FLAG_RESID_PROPR = chkResPro.Checked ? "S" : "N";
            tb108.DE_ENDE_EMPRE_RESP = txtLogradouroEmpResp.Text.Trim() != "" ? txtLogradouroEmpResp.Text : null;
            tb108.NU_ENDE_EMPRE_RESP = int.TryParse(txtNumeroEmpResp.Text, out intRetorno) ? (int?)intRetorno : null;
            tb108.DE_COMP_EMPRE_RESP = txtComplementoEmpResp.Text.Trim() != "" ? txtComplementoEmpResp.Text : null;
            tb108.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.TryParse(ddlCidadeEmpResp.SelectedValue, out intRetorno) ? intRetorno : 0);
            tb108.CO_BAIRRO_EMPRE_RESP = int.TryParse(ddlBairroEmpResp.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb108.TB74_UF = TB74_UF.RetornaPelaChavePrimaria(ddlUfEmpResp.SelectedValue);
            tb108.CO_CEP_EMPRE_RESP = txtCepEmpresaResp.Text.Trim() != "" ? txtCepEmpresaResp.Text.Replace("-", "") : null;      
            tb108.DE_OBS_RESP = txtObservacoesResp.Text.Trim() != "" ? txtObservacoesResp.Text.Replace("-", "") : null;
            tb108.DT_ALT_REGISTRO = DateTime.Now;

            tb108.NM_FACEBOOK_RESP = txtFacebookResp.Text;
            //tb108.NM_TWITTER_RESP = txtTwitterResp.Text;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb108.FL_NEGAT_CHEQUE = "N";
                tb108.FL_NEGAT_SERASA = "N";
                tb108.FL_NEGAT_SPC = "N";
            }            

            CurrentPadraoCadastros.CurrentEntity = tb108;
        }                       
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB108_RESPONSAVEL tb108 = RetornaEntidade();

            if (tb108 != null)
            {
                tb108.ImageReference.Load();
                tb108.TB904_CIDADEReference.Load();
                tb108.TB74_UFReference.Load();

                if (tb108.Image != null)
                    upImagemResp.CarregaImagem(tb108.Image.ImageId);
                else
                    upImagemResp.CarregaImagem(0);

                txtCPFResp.Text = tb108.NU_CPF_RESP;
                txtNomeResp.Text = tb108.NO_RESP;
                txtNISResp.Text = tb108.NU_NIS_RESP.ToString();
                txtDtNascResp.Text = tb108.DT_NASC_RESP != null ? tb108.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                ddlSexoResp.SelectedValue = tb108.CO_SEXO_RESP;
                ddlEstadoCivilResp.SelectedValue = tb108.CO_ESTADO_CIVIL_RESP != null ? tb108.CO_ESTADO_CIVIL_RESP : "";
                ddlDeficienciaResp.SelectedValue = tb108.TP_DEF_RESP;
                ddlGrauInstrucaoResp.SelectedValue = tb108.CO_INST.ToString();
               // ddlCursoFormacaoResp.SelectedValue = tb108.CO_CURFORM_RESP.ToString();
                ddlRendaResp.SelectedValue = tb108.RENDA_FAMILIAR_RESP;
                txtLogradouroResp.Text = tb108.DE_ENDE_RESP;
                txtNumeroResp.Text = tb108.NU_ENDE_RESP.ToString();
                txtComplementoResp.Text = tb108.DE_COMP_RESP;
                ddlUfResp.SelectedValue = tb108.CO_ESTA_RESP;
                CarregaCidades(ddlCidadeResp, ddlUfResp);
                ddlCidadeResp.SelectedValue = tb108.CO_CIDADE.ToString();
                CarregaBairros(ddlBairroResp, ddlCidadeResp);
                ddlBairroResp.SelectedValue = tb108.CO_BAIRRO.ToString();
                txtCepResp.Text = tb108.CO_CEP_RESP;
                txtTelCelularResp.Text = tb108.NU_TELE_RESI_RESP;
                txtTelResidencialResp.Text = tb108.NU_TELE_CELU_RESP;
                txtEmailResp.Text = tb108.DES_EMAIL_RESP;
                txtQtdMenoresResp.Text = tb108.QT_MENOR_DEPEN_RESP.ToString();
                txtQtdMaioresResp.Text = tb108.QT_MAIOR_DEPEN_RESP.ToString();
                txtIdentidadeResp.Text = tb108.CO_RG_RESP;
                txtDtEmissaoResp.Text = tb108.DT_EMIS_RG_RESP != null ? tb108.DT_EMIS_RG_RESP.Value.ToString("dd/MM/yyyy") : "";
                txtOrgEmissorResp.Text = tb108.CO_ORG_RG_RESP;
                ddlIdentidadeUFResp.SelectedValue = tb108.CO_ESTA_RG_RESP;
                txtNumeroTituloResp.Text = tb108.NU_TIT_ELE;
                txtZonaResp.Text = tb108.NU_ZONA_ELE;
                txtSecaoResp.Text = tb108.NU_SEC_ELE;
                ddlUfTituloResp.SelectedValue = tb108.CO_UF_TIT_ELE_RESP;
                txtEmpresaResp.Text = tb108.NO_EMPR_RESP;
                txtDepartamentoResp.Text = tb108.NO_SETOR_RESP;
                txtFuncaoResp.Text = tb108.NO_FUNCAO_RESP;
                txtEmailFuncionalResp.Text = tb108.DES_EMAIL_EMP;
                txtDtAdmissaoResp.Text = tb108.DT_ADMIS_RESP != null ? tb108.DT_ADMIS_RESP.Value.ToString("dd/MM/yyyy") : "";
                txtLogradouroEmpResp.Text = tb108.DE_ENDE_EMPRE_RESP;
                txtNumeroEmpResp.Text = tb108.NU_ENDE_EMPRE_RESP.ToString();
                txtComplementoEmpResp.Text = tb108.DE_COMP_EMPRE_RESP;
                ddlUfEmpResp.SelectedValue = tb108.TB74_UF != null ? tb108.TB74_UF.CODUF : "";
                if (ddlUfEmpResp.SelectedValue != "")
                {
                    CarregaCidades(ddlCidadeEmpResp, ddlUfEmpResp);
                    ddlCidadeEmpResp.SelectedValue = tb108.TB904_CIDADE == null ? "" : tb108.TB904_CIDADE.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
                    ddlBairroEmpResp.SelectedValue = tb108.CO_BAIRRO_EMPRE_RESP.ToString();
                }                                
                txtCepEmpresaResp.Text = tb108.CO_CEP_EMPRE_RESP;
                txtTelEmpresaResp.Text = tb108.NU_TELE_COME_RESP;
                txtCarteiraSaudeResp.Text = tb108.NR_CARTEI_SAUDE_RESP != null ? tb108.NR_CARTEI_SAUDE_RESP.ToString() : "";                
                chkRespFunc.Checked = tb108.CO_FLAG_RESP_FUNC == "S";
                txtApelidoResp.Text = tb108.NO_APELIDO_RESP;
                ddlNacionalidadeResp.SelectedValue = tb108.CO_NACI_RESP;
                ddlUfNacionalidadeResp.Enabled = ddlNacionalidadeResp.SelectedValue == "BR";
                txtNaturalidadeResp.Text = tb108.DE_NATU_RESP;
                ddlUfNacionalidadeResp.SelectedValue = tb108.CO_UF_NATU_RESP != null ? tb108.CO_UF_NATU_RESP : "";
                ddlOrigemResp.SelectedValue = tb108.CO_ORIGEM_RESP != null ? tb108.CO_ORIGEM_RESP : "SR";
                ddlTpSangueFResp.SelectedValue = tb108.CO_TIPO_SANGUE_RESP != null ? tb108.CO_TIPO_SANGUE_RESP : "";
                ddlStaSangueFResp.SelectedValue = tb108.CO_STATUS_SANGUE_RESP != null ? tb108.CO_STATUS_SANGUE_RESP : "";
                ddlCorRacaResp.SelectedValue = tb108.TP_RACA_RESP != null ? tb108.TP_RACA_RESP : "";
                chkConjFunc.Checked = tb108.CO_FLAG_CONJUG_RESP == "S";
                txtNomeConjugueResp.Text = tb108.NO_CONJUG_RESP;
                txtDtConjuResp.Text = tb108.DT_NASC_CONJUG_RESP != null ? ((DateTime)tb108.DT_NASC_CONJUG_RESP).ToString("dd/MM/yyyy") : "";
                ddlSexoConjugResp.SelectedValue = tb108.CO_SEXO_CONJUG_RESP != null ? tb108.CO_SEXO_CONJUG_RESP : "M";
                txtCPFConjugResp.Text = tb108.NU_CPF_CONJUG_RESP;
                txtPassaporteResp.Text = tb108.NU_PASSAPORTE_RESP != null ? tb108.NU_PASSAPORTE_RESP.ToString() : "";
                txtNomeMaeResp.Text = tb108.NO_MAE_RESP;
                txtNomePaiResp.Text = tb108.NO_PAI_RESP;
                txtDtSaidaResp.Text = tb108.DT_TERM_ATIV_RESP != null ? ((DateTime)tb108.DT_TERM_ATIV_RESP).ToString("dd/MM/yyyy") : "";
                ddlSituacaoResp.SelectedValue = tb108.CO_SITU_RESP != null ? tb108.CO_SITU_RESP : "A";
                txtDtSituacaoResp.Text = tb108.DT_SITU_RESP != null ? tb108.DT_SITU_RESP.Value.ToString("dd/MM/yyyy") : "";
                chkResPro.Checked = tb108.CO_FLAG_RESID_PROPR == "S";
                txtObservacoesResp.Text = tb108.DE_OBS_RESP;

                txtFacebookResp.Text = tb108.NM_FACEBOOK_RESP;
                //txtTwitterResp.Text = tb108.NM_TWITTER_RESP ;

                if (tb108.DE_ENDE_RESP != null)
                {
                    string descEnd = tb108.DE_ENDE_RESP + ",";
                    descEnd = tb108.NU_ENDE_RESP != null ? descEnd + tb108.NU_ENDE_RESP.ToString() + "," : descEnd;
                    carregaMapa( descEnd + ddlCidadeResp.SelectedItem + " - " + tb108.CO_ESTA_RESP);
                }
                else
                    tbMap.Visible = false;
            }                                                   
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB108_RESPONSAVEL</returns>
        private TB108_RESPONSAVEL RetornaEntidade()
        {
            TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb108 == null) ? new TB108_RESPONSAVEL() : tb108;
        }

        /// <summary>
        /// Apresenta o mapa com a localização do endereço do responsável
        /// </summary>
        /// <param name="endResponsavel">Endereço do responsável</param>
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
            ddlNacionalidadeResp.DataSource = TB299_PAISES.RetornaTodosRegistros();

            ddlNacionalidadeResp.DataTextField = "NO_PAISES";
            ddlNacionalidadeResp.DataValueField = "CO_ISO_PAISES";
            ddlNacionalidadeResp.DataBind();

            ddlNacionalidadeResp.SelectedValue = "BR";
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

            if (ddlUFCarreg == ddlUfNacionalidadeResp)
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
            ddlGrauInstrucaoResp.DataSource = TB18_GRAUINS.RetornaTodosRegistros();

            ddlGrauInstrucaoResp.DataTextField = "NO_INST";
            ddlGrauInstrucaoResp.DataValueField = "CO_INST";
            ddlGrauInstrucaoResp.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Curso de Formação
        /// </summary>
        private void CarregaCursosFormacao()
        {
            //string grauInst = TB18_GRAUINS.RetornaPelaChavePrimaria(int.Parse(ddlGrauInstrucaoResp.SelectedValue)).CO_SIGLA_INST;

            //ddlCursoFormacaoResp.DataSource = TB100_ESPECIALIZACAO.RetornaPeloTipo(grauInst.Substring(0,2)).OrderBy( e => e.DE_ESPEC );

            //ddlCursoFormacaoResp.DataTextField = "DE_ESPEC";
            //ddlCursoFormacaoResp.DataValueField = "CO_ESPEC";
            //ddlCursoFormacaoResp.DataBind();
            //ddlCursoFormacaoResp.Enabled = ddlCursoFormacaoResp.Items.Count > 0;
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

//====> Preenche os campos de endereço do responsável de acordo com o CEP, se o mesmo possuir registro na base de dados
        protected void btnPesquisarCepResp_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepResp.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepResp.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where( c => c.CO_CEP == numCep ).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfResp.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeResp, ddlUfResp);
                    ddlCidadeResp.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroResp, ddlCidadeResp);
                    ddlBairroResp.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroResp.Text = ddlBairroResp.SelectedValue = ddlCidadeResp.SelectedValue = "";
                    ddlUfResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    AuxiliPagina.EnvioMensagemErro(this.Page, "CEP não encontrado");
                    return;
                }
            }
        }

//====> Preenche os campos de endereço da empresa do responsável de acordo com o CEP, se o mesmo possuir registro na base de dados
        protected void btnPesquisarCepEmpResp_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepEmpresaResp.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepEmpresaResp.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where( c => c.CO_CEP == numCep ).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroEmpResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfEmpResp.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeEmpResp, ddlUfEmpResp);
                    ddlCidadeEmpResp.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
                    ddlBairroEmpResp.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroEmpResp.Text = ddlBairroEmpResp.SelectedValue = ddlCidadeEmpResp.SelectedValue = "";
                    ddlUfEmpResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    AuxiliPagina.EnvioMensagemErro(this.Page, "CEP não encontrado");
                    return;
                }
            }
        }

        protected void ddlGrauInstrucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCursosFormacao();
        }

        protected void ddlNacionalidadeResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNacionalidadeResp.SelectedValue == "BR")
            {
                ddlUfNacionalidadeResp.Enabled = true;
            }
            else
            {
                ddlUfNacionalidadeResp.Enabled = false;
                ddlUfNacionalidadeResp.SelectedValue = "";
            }
        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidadeResp, ddlUfResp);
            CarregaBairros(ddlBairroResp, ddlCidadeResp);
        }

        protected void ddlUfEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidadeEmpResp, ddlUfEmpResp);
            CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlBairroResp, ddlCidadeResp);
        }

        protected void ddlCidadeEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
        }
    }
}