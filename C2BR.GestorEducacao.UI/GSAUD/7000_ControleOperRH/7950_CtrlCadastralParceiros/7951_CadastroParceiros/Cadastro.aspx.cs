//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE PARCEECEDORES DE PRODUTOS E SERVIÇOS
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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Net;
using System.Net.Sockets;
using System.IO;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._7000_ControleOperRH._7950_CtrlCadastralParceiros._7951_CadastroParceiros
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
            CompareValidatorDataAtual.ValueToCompare = DateTime.Now.ToShortDateString();

            if (!Page.IsPostBack)
            {
                //CarregaTipoUnidade();
                formularioEndereco.DdlUf.Enabled = formularioEndereco.DdlCidade.Enabled =
                formularioEndereco.DdlBairro.Enabled = formularioEndereco.TxtLogradouro.Enabled = true;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDtCadastro.Text = txtDtStatus.Text = dataAtual;
                }
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                TB421_PARCEIROS tb421 = RetornaEntidade();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    try
                    {
                        TB421_PARCEIROS.Delete(tb421, true);
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Parceiro excluído com sucesso.", Request.Url.AbsoluteUri);
                    }
                    catch (Exception)
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Não foi possível excluir o Parceiro. Por favor tente novamente ou entre em contato com o suporte.");
                        return;
                    }                    
                }
                else if (tb421.CO_PARCE == 0)
                {
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    {
                        string strCNPJ = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");

                        var ocor = (from iTb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                                    where iTb421.CO_CPFCGC_PARCE == strCNPJ
                                    select new { iTb421.CO_PARCE }).FirstOrDefault();

                        if (ocor != null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "CNPJ/CPF já cadastrado.");
                            return;
                        }
                    }

                    tb421 = new TB421_PARCEIROS();

                    //DADOS GERAIS DO PARCEIRO
                    tb421.TP_PARCE = ddlTipo.SelectedValue != "" ? ddlTipo.SelectedValue : null;
                    tb421.NO_FANTAS_PARCE = txtNome.Text;
                    tb421.NO_SIGLA_PARCE = txtSigla.Text != "" ? txtSigla.Text : null;
                    tb421.DE_RAZSOC_PARCE = txtRazaoSocial.Text;
                    tb421.CO_CPFCGC_PARCE = txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", "");
                    tb421.CO_INS_EST_PARCE = txtInscEstadual.Text != "" ? txtInscEstadual.Text : null;
                    tb421.CO_INS_MUN_PARCE = txtInscMunicipal.Text != "" ? txtInscMunicipal.Text : null;
                    tb421.DE_OBSER_PARCE = txtObservacao.Text != "" ? txtObservacao.Text : null;
                    //CO_ORG_PARCE = 

                    //DADOS ENDEREÇO PARCEIRO
                    tb421.CO_CEP_PARCE = formularioEndereco.TxtCep.Text.Replace("-", "");
                    tb421.DE_END_PARCE = formularioEndereco.TxtLogradouro.Text != "" ? formularioEndereco.TxtLogradouro.Text : null;
                    tb421.NU_END_PARCE = formularioEndereco.TxtNumero.Text != "" ? (int?)int.Parse(formularioEndereco.TxtNumero.Text) : null;
                    tb421.DE_COMPLE_PARCE = formularioEndereco.TxtComplemento.Text != "" ? formularioEndereco.TxtComplemento.Text : null;
                    tb421.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlBairro.SelectedValue));
                    tb421.TB905_BAIRRO.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlCidade.SelectedValue));
                    tb421.TB74_UF = TB74_UF.RetornaPelaChavePrimaria(formularioEndereco.DdlUf.SelectedValue);

                    //DADOS CONTATOS PARCEIRO
                    tb421.CO_TEL1_PARCE = txtTelefone.Text != "" ? txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.CO_TEL2_PARCE = txtTelefone2.Text != "" ? txtTelefone2.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.DE_EMAIL_PARCE = txtEmail.Text != "" ? txtEmail.Text : null;
                    tb421.CO_FAX_PARCE = txtFax.Text != "" ? txtFax.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.CO_WATHS_PARCE = txtWhatsapp.Text != "" ? txtWhatsapp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.NM_SKYPE_PARCE = txtSkype.Text != "" ? txtSkype.Text : null;
                    tb421.DE_WEB_PARCE = txtWebSite.Text != "" ? txtWebSite.Text : null;

                    //DADOS RESPONSAVEL PARCEIRO
                    tb421.NM_RESPO_PARCE = txtNomeRespParceiro.Text != "" ? txtNomeRespParceiro.Text : null;
                    tb421.CO_CPF_RESPO_PARCE = txtCPFRespParceiro.Text != "" ? txtCPFRespParceiro.Text : null;
                    tb421.NM_FUNCAO_RESPO_PARCE = txtCargoRespParceiro.Text != "" ? txtCargoRespParceiro.Text : null;
                    tb421.CO_TELEF_RESPO_PARCE = txtTelRespParceiro.Text != "" ? txtTelRespParceiro.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.CO_WATHS_RESPO_PARCE = txtWhatsapp.Text != "" ? txtWhatsapp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.DE_EMAIL_RESPO_PARCE = txtEmailResp.Text != "" ? txtEmailResp.Text : null;
                    tb421.NM_SKYPE_RESPO_PARCE = txtSkypeResp.Text != "" ? txtSkypeResp.Text : null;
                    tb421.DE_OBSER_RESPO_PARCE = txtObsRespParceiro.Text != "" ? txtObsRespParceiro.Text : null;

                    //DADOS INDICACAO PARCEIRO
                    tb421.FL_FUNCIO_INDIC_PARCE = chcFuncionario.Checked ? "S" : "N";
                    if (chcFuncionario.Checked && ddlNomeIndicacao.Visible == true)
                    {
                        var tb03 = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeIndicacao.SelectedValue));
                        tb421.NM_INDIC_PARCE = tb03.NO_COL != "" || tb03.NO_COL != null ? tb03.NO_COL : null;
                        tb421.CO_EMP_INDIC_PARCE = LoginAuxili.CO_EMP;
                        tb421.CO_COL_INDIC_PARCE = tb03.CO_COL;
                    }
                    else
                    {
                        tb421.NM_INDIC_PARCE = txtNomeIndicacao.Text != "" ? txtNomeIndicacao.Text : null;
                    }
                    if (txtDtIndicacao.Text != "")
                    {
                        tb421.DT_INDIC_PARCE = Convert.ToDateTime(txtDtIndicacao.Text);
                    }
                    tb421.CO_TELEF_INDIC_PARCE = txtTelIndicacao.Text != "" ? txtTelIndicacao.Text : null;
                    tb421.CO_WATHS_INDIC_PARCE = txtWhatsappIndicacao.Text != "" ? txtWhatsappIndicacao.Text : null;
                    tb421.DE_EMAIL_INDIC_PARCE = txtEmailIndicacao.Text != "" ? txtEmailIndicacao.Text : null;
                    tb421.NM_SKYPE_INDIC_PARCE = txtSkypeIndicacao.Text != "" ? txtSkypeIndicacao.Text : null;
                    tb421.DE_OBSER_INDIC_PARCE = txtObsIndicacao.Text != "" ? txtObsIndicacao.Text : null;

                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    string ipDeEnvio = "";
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipDeEnvio = ip.ToString();
                        }
                    }
                    //DADOS CADASTRO E SITUAÇÃO PARCEIRO
                    tb421.NR_IP_CAD_PARCE = ipDeEnvio;
                    tb421.CO_SIT_PARCE = ddlStatus.SelectedValue;
                    tb421.DT_SIT_PARCE = DateTime.Parse(txtDtStatus.Text);
                    tb421.DT_CAD_PARCE = DateTime.Parse(txtDtCadastro.Text);

                    if (FileUploadControl.HasFile)
                    {
                        byte[] bytes;
                        bytes = FileUploadControl.FileBytes;
                        FileUploadControl.PostedFile.InputStream.Read(bytes, 0, bytes.Length);
                        string nomeAnexo = "";
                        if (FileUploadControl.FileName.Length > 100)
                        {
                            nomeAnexo = FileUploadControl.FileName.Substring(0, 99);
                        }
                        tb421.NO_ANEXO = nomeAnexo == "" ? FileUploadControl.FileName : nomeAnexo;
                        tb421.ANEXO = bytes;
                    }

                    //DADOS TIPO DE NEGÓCIO
                    tb421.DE_PROPOS_NEGOC = propNegocio.Text != "" ? propNegocio.Text : null;
                    tb421.CO_AREA_PROSP_NEGOC = ddlAreaProspeccao.SelectedValue;

                    TB421_PARCEIROS.SaveOrUpdate(tb421, true);
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Parceiro cadastrado com sucesso.", Request.Url.AbsoluteUri);
                }
                else
                {
                    //DADOS GERAIS DO PARCEIRO
                    tb421.NO_FANTAS_PARCE = txtNome.Text;
                    tb421.NO_SIGLA_PARCE = txtSigla.Text != "" ? txtSigla.Text : null;
                    tb421.CO_INS_EST_PARCE = txtInscEstadual.Text != "" ? txtInscEstadual.Text : null;
                    tb421.CO_INS_MUN_PARCE = txtInscMunicipal.Text != "" ? txtInscMunicipal.Text : null;
                    tb421.DE_OBSER_PARCE = txtObservacao.Text != "" ? txtObservacao.Text : null;
                    //CO_ORG_PARCE = 

                    //DADOS ENDEREÇO PARCEIRO
                    tb421.CO_CEP_PARCE = formularioEndereco.TxtCep.Text.Replace("-", "");
                    tb421.DE_END_PARCE = formularioEndereco.TxtLogradouro.Text != "" ? formularioEndereco.TxtLogradouro.Text : null;
                    tb421.NU_END_PARCE = formularioEndereco.TxtNumero.Text != "" ? (int?)int.Parse(formularioEndereco.TxtNumero.Text) : null;
                    tb421.DE_COMPLE_PARCE = formularioEndereco.TxtComplemento.Text != "" ? formularioEndereco.TxtComplemento.Text : null;
                    tb421.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlBairro.SelectedValue));
                    tb421.TB905_BAIRRO.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(formularioEndereco.DdlCidade.SelectedValue));
                    tb421.TB74_UF = TB74_UF.RetornaPelaChavePrimaria(formularioEndereco.DdlUf.SelectedValue);

                    //DADOS CONTATOS PARCEIRO
                    tb421.CO_TEL1_PARCE = txtTelefone.Text != "" ? txtTelefone.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.CO_TEL2_PARCE = txtTelefone2.Text != "" ? txtTelefone2.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.DE_EMAIL_PARCE = txtEmail.Text != "" ? txtEmail.Text : null;
                    tb421.CO_FAX_PARCE = txtFax.Text != "" ? txtFax.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.CO_WATHS_PARCE = txtWhatsapp.Text != "" ? txtWhatsapp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.NM_SKYPE_PARCE = txtSkype.Text != "" ? txtSkype.Text : null;
                    tb421.DE_WEB_PARCE = txtWebSite.Text != "" ? txtWebSite.Text : null;

                    //DADOS RESPONSAVEL PARCEIRO
                    tb421.NM_RESPO_PARCE = txtNomeRespParceiro.Text != "" ? txtNomeRespParceiro.Text : null;
                    tb421.CO_CPF_RESPO_PARCE = txtCPFRespParceiro.Text != "" ? txtCPFRespParceiro.Text : null;
                    tb421.NM_FUNCAO_RESPO_PARCE = txtCargoRespParceiro.Text != "" ? txtCargoRespParceiro.Text : null;
                    tb421.CO_TELEF_RESPO_PARCE = txtTelRespParceiro.Text != "" ? txtTelRespParceiro.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.CO_WATHS_RESPO_PARCE = txtWhatsapp.Text != "" ? txtWhatsapp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb421.DE_EMAIL_RESPO_PARCE = txtEmailResp.Text != "" ? txtEmailResp.Text : null;
                    tb421.NM_SKYPE_RESPO_PARCE = txtSkypeResp.Text != "" ? txtSkypeResp.Text : null;
                    tb421.DE_OBSER_RESPO_PARCE = txtObsRespParceiro.Text != "" ? txtObsRespParceiro.Text : null;

                    //DADOS INDICACAO PARCEIRO
                    tb421.FL_FUNCIO_INDIC_PARCE = chcFuncionario.Checked ? "S" : "N";
                    if (chcFuncionario.Checked && ddlNomeIndicacao.Visible == true)
                    {
                        var tb03 = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeIndicacao.SelectedValue));
                        tb421.NM_INDIC_PARCE = tb03.NO_COL != "" || tb03.NO_COL != null ? tb03.NO_COL : null;
                        tb421.CO_EMP_INDIC_PARCE = LoginAuxili.CO_EMP;
                        tb421.CO_COL_INDIC_PARCE = tb03.CO_COL;
                    }
                    else
                    {
                        tb421.NM_INDIC_PARCE = txtNomeIndicacao.Text != "" ? txtNomeIndicacao.Text : null;
                        tb421.CO_EMP_INDIC_PARCE = 0;
                        tb421.CO_COL_INDIC_PARCE = 0;
                    }
                    if (txtDtIndicacao.Text != "")
                    {
                        tb421.DT_INDIC_PARCE = Convert.ToDateTime(txtDtIndicacao.Text);
                    }
                    tb421.CO_TELEF_INDIC_PARCE = txtTelIndicacao.Text != "" ? txtTelIndicacao.Text : null;
                    tb421.CO_WATHS_INDIC_PARCE = txtWhatsappIndicacao.Text != "" ? txtWhatsappIndicacao.Text : null;
                    tb421.DE_EMAIL_INDIC_PARCE = txtEmailIndicacao.Text != "" ? txtEmailIndicacao.Text : null;
                    tb421.NM_SKYPE_INDIC_PARCE = txtSkypeIndicacao.Text != "" ? txtEmailIndicacao.Text : null;
                    tb421.DE_OBSER_INDIC_PARCE = txtObsIndicacao.Text != "" ? txtObsIndicacao.Text : null;

                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    string ipDeEnvio = "";
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipDeEnvio = ip.ToString();
                        }
                    }
                    //DADOS CADASTRO E SITUAÇÃO PARCEIRO
                    tb421.NR_IP_CAD_PARCE = ipDeEnvio;
                    tb421.CO_SIT_PARCE = ddlStatus.SelectedValue;
                    if (txtDtStatus.Text != "")
                    {
                        tb421.DT_SIT_PARCE = DateTime.Parse(txtDtStatus.Text);
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Data de alteração da situação do parceiro precisa ser preenchida");
                    }
                    if (txtDtCadastro.Text != "")
                    {
                        tb421.DT_CAD_PARCE = DateTime.Parse(txtDtCadastro.Text);
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Data Cadastro precisa ser preenchida");
                    }

                    //ANEXO
                    if (FileUploadControl.HasFile)
                    {
                        byte[] bytes;
                        bytes = FileUploadControl.FileBytes;
                        FileUploadControl.PostedFile.InputStream.Read(bytes, 0, bytes.Length);
                        string nomeAnexo = "";
                        if (FileUploadControl.FileName.Length > 100)
                        {
                            nomeAnexo = FileUploadControl.FileName.Substring(0, 99);
                        }
                        tb421.NO_ANEXO = nomeAnexo == "" ? FileUploadControl.FileName : nomeAnexo;
                        tb421.ANEXO = bytes;
                    }

                    //DADOS TIPO DE NEGÓCIO
                    tb421.DE_PROPOS_NEGOC = propNegocio.Text != "" ? propNegocio.Text : null;
                    if (ddlAreaProspeccao.SelectedValue == "0")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Informe a área da proposta de negócio.");
                    }
                    else
                    {
                        tb421.CO_AREA_PROSP_NEGOC = ddlAreaProspeccao.SelectedValue;
                    }

                    TB421_PARCEIROS.SaveOrUpdate(tb421, true);
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Parceiro alterado com sucesso.", Request.Url.AbsoluteUri);
                }
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível salvar ou alterar o cadastro de parceiros, por favor, tente novamente ou entre em contato com o suporte.");
            }

        }
        #endregion

        #region Métodos

        protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtDtStatus.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void chcFuncionario_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFuncionario.Checked)
            {
                txtNomeIndicacao.Visible = false;
                ddlNomeIndicacao.Visible = true;

                ddlNomeIndicacao.DataSource = TB03_COLABOR.RetornaTodosRegistros().Where(x => x.CO_EMP == LoginAuxili.CO_EMP).OrderBy(x => x.NO_COL);
                ddlNomeIndicacao.DataTextField = "NO_COL";
                ddlNomeIndicacao.DataValueField = "CO_COL";
                ddlNomeIndicacao.DataBind();

            }
            else
            {
                txtNomeIndicacao.Visible = true;
                ddlNomeIndicacao.Visible = false;
                txtNomeIndicacao.Text = "";
                txtDtIndicacao.Text = "";
                txtTelIndicacao.Text = "";
                txtWhatsappIndicacao.Text = "";
                txtEmailIndicacao.Text = "";
                txtSkypeIndicacao.Text = "";
                txtObsIndicacao.Text = "";

            }
        }

        #endregion

        #region Carregamanto

        private TB421_PARCEIROS RetornaEntidade()
        {
            TB421_PARCEIROS tb421 = TB421_PARCEIROS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb421 == null) ? new TB421_PARCEIROS() : tb421;
        }

        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB421_PARCEIROS tb421 = RetornaEntidade();

            if (tb421 != null)
            {
                //tb421.TB03_COLABORReference.Load();
                tb421.TB905_BAIRROReference.Load();
                tb421.TB905_BAIRRO.TB904_CIDADEReference.Load();

                //DADOS GERAIS DO PARCEIRO
                ddlTipo.SelectedValue = tb421.TP_PARCE;
                ddlTipo.Enabled = false;
                txtNome.Text = tb421.NO_FANTAS_PARCE.ToUpper();
                txtRazaoSocial.Enabled = false;
                txtRazaoSocial.Text = tb421.DE_RAZSOC_PARCE.ToUpper();
                txtCNPJ.Text = tb421.CO_CPFCGC_PARCE;
                txtSigla.Text = tb421.NO_SIGLA_PARCE;
                txtInscEstadual.Text = tb421.CO_INS_EST_PARCE;
                txtInscMunicipal.Text = tb421.CO_INS_MUN_PARCE;
                txtObservacao.Text = tb421.DE_OBSER_PARCE;

                //DADOS ENDEREÇO PARCEIRO
                formularioEndereco.TxtCep.Text = tb421.CO_CEP_PARCE;
                formularioEndereco.TxtLogradouro.Text = tb421.DE_END_PARCE;
                formularioEndereco.TxtNumero.Text = tb421.NU_END_PARCE.ToString();
                formularioEndereco.TxtComplemento.Text = tb421.DE_COMPLE_PARCE;
                formularioEndereco.CarregaBairros();
                formularioEndereco.DdlBairro.SelectedValue = tb421.TB905_BAIRRO.CO_BAIRRO.ToString();
                formularioEndereco.CarregaCidades();
                formularioEndereco.DdlCidade.SelectedValue = tb421.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString();
                formularioEndereco.CarregaUfs();
                formularioEndereco.DdlUf.SelectedValue = tb421.TB74_UF.CODUF;
                formularioEndereco.TxtComplemento.Text = tb421.DE_END_PARCE;

                //DADOS CONTATOS PARCEIRO
                txtTelefone.Text = tb421.CO_TEL1_PARCE;
                txtTelefone2.Text = tb421.CO_TEL2_PARCE;
                txtEmail.Text = tb421.DE_EMAIL_PARCE;
                txtFax.Text = tb421.CO_FAX_PARCE;
                txtWhatsapp.Text = tb421.CO_WATHS_PARCE;
                txtSkype.Text = tb421.NM_SKYPE_PARCE;
                txtWebSite.Text = tb421.DE_WEB_PARCE;

                //DADOS RESPONSÁVEL PARCEIRO
                txtNomeRespParceiro.Text = tb421.NM_RESPO_PARCE;
                txtCPFRespParceiro.Text = tb421.CO_CPF_RESPO_PARCE;
                txtCargoRespParceiro.Text = tb421.NM_FUNCAO_RESPO_PARCE;
                txtTelRespParceiro.Text = tb421.CO_TELEF_RESPO_PARCE;
                txtWhatsapp.Text = tb421.CO_WATHS_RESPO_PARCE;
                txtEmailResp.Text = tb421.DE_EMAIL_RESPO_PARCE;
                txtSkypeResp.Text = tb421.NM_SKYPE_RESPO_PARCE;
                txtObsRespParceiro.Text = tb421.DE_OBSER_RESPO_PARCE;

                //DADOS INDICACAO PARCEIRO
                chcFuncionario.Checked = tb421.FL_FUNCIO_INDIC_PARCE == "S" ? true : false;
                txtNomeIndicacao.Text = tb421.NM_INDIC_PARCE;
                txtDtIndicacao.Text = tb421.DT_INDIC_PARCE.ToString();
                txtTelIndicacao.Text = tb421.CO_TELEF_INDIC_PARCE;
                txtWhatsappIndicacao.Text = tb421.CO_WATHS_INDIC_PARCE;
                txtEmailIndicacao.Text = tb421.DE_EMAIL_INDIC_PARCE;
                txtSkypeIndicacao.Text = tb421.NM_SKYPE_INDIC_PARCE;
                txtObsIndicacao.Text = tb421.DE_OBSER_INDIC_PARCE;

                //DADOS CADASTRO E SITUAÇÃO  PARCEIRO
                ddlStatus.SelectedValue = tb421.CO_SIT_PARCE;
                txtDtStatus.Text = tb421.DT_SIT_PARCE.ToString("dd/MM/yyyy");
                txtDtCadastro.Text = tb421.DT_CAD_PARCE.ToString("dd/MM/yyyy");

                //DADOS TIPO DE NEGÓCIO
                propNegocio.Text = tb421.DE_PROPOS_NEGOC;
                ddlAreaProspeccao.SelectedValue = tb421.CO_AREA_PROSP_NEGOC;
            }
        }
        #endregion
    }
}