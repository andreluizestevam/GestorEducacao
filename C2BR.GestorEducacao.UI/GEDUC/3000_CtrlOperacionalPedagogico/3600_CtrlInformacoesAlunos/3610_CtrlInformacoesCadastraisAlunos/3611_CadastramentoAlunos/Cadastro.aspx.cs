//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 12/03/2013| André Nobre Vinagre        | Colocado para carregar o mapa do GoogleMaps.
//           |                            | 
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 14/03/2013| André Nobre Vinagre        | Coloquei para carregar a data de situação atual
//           |                            | quando o campo for nulo na edição.
//           |                            |
//-----------+----------------------------+----------------------------------------
//03/06/2013 | Thales Pinho Andrade       | Foi desativado o Serviço de Map do google


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3611_CadastramentoAlunos
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private static Dictionary<string, string> statusAprovacao = AuxiliBaseApoio.chave(statusAprovacaoAluno.ResourceManager, true);
        private static Dictionary<string, string> statusMatricula = AuxiliBaseApoio.chave(statusMatriculaAluno.ResourceManager, true);
        private static Dictionary<string, string> tipoDeficiencia = AuxiliBaseApoio.chave(tipoDeficienciaAluno.ResourceManager);

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                grdMatriculas.Visible = true;
                //------------> Visibilidade do mapa de endereço do aluno
                tbMap.Visible = false;
                txtAnoOri.Text = DateTime.Now.Year.ToString();

//------------> Tamanho da imagem de Aluno
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                CarregaUfs(ddlUfAlu);
                CarregaUfs(ddlUfNacionalidadeAlu);
                CarregaUfs(ddlUfRgAlu);
                CarregaUfs(ddlUfTituloAlu);
                CarregaUfs(ddlUFCertidaoAlu);
                CarregaBolsas();
                CarregaDeficiencia();
                CarregaCidades();

//------------> Variável que guarda informações da instituição do usuário logado
                var tb000 = (from iTb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                             where iTb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { 
                                 iTb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN, 
                                 iTb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO,
                             iTb000.TB905_BAIRRO.CO_BAIRRO,
                             iTb000.CEP_CODIGO}).First();

//------------> Variável que vai guardar se NIRE é automático ou não
                string strTipoNireAuto = "";

                if (tb000.FLA_CTRL_TIPO_ENSIN == TipoControle.I.ToString())
                {
                    strTipoNireAuto = tb000.FLA_GERA_NIRE_AUTO != null ? tb000.FLA_GERA_NIRE_AUTO : "";
                }
                else if (tb000.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString())
                {
//----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
                    var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                where iTb25.CO_EMP == LoginAuxili.CO_EMP
                                select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

                    strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";
                }

                if (strTipoNireAuto != "")
                {
                    txtNireAlu.Enabled = strTipoNireAuto == "N";
                }
                else
                    txtNireAlu.Enabled = true;
                

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {                   
                    txtDataNascimentoAlu.Text = "01/01/1900";
                    // dados do registro
                    txtCartorioAlu.Text = "X";
                    txtLivroAlu.Text = "999";
                    txtNumeroCertidaoAlu.Text = "999";
                    txtFolhaAlu.Text = "999";
                    txtCidadeCertidaoAlu.Text = LoginAuxili.NO_CIDADE_INSTITUICAO;
                    if (ddlUFCertidaoAlu.Items.FindByValue(LoginAuxili.CO_UF_INSTITUICAO) != null)
                        ddlUFCertidaoAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;

                    ddlRendaFamiliarAlu.SelectedValue = "6";
                    txtCepAlu.Text = tb000.CEP_CODIGO.ToString();
                    txtLogradouroAlu.Text = "X";
                    if (ddlCidadeAlu.Items.FindByValue(LoginAuxili.CO_CIDADE_INSTITUICAO.ToString()) != null)
                        ddlCidadeAlu.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
                    CarregaBairros();
                    if (ddlBairroAlu.Items.FindByValue(tb000.CO_BAIRRO.ToString()) != null)
                        ddlBairroAlu.SelectedValue = tb000.CO_BAIRRO.ToString();
                    if (ddlUfTituloAlu.Items.FindByValue(LoginAuxili.CO_UF_INSTITUICAO) != null)
                        ddlUfTituloAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;

                    if (ddlUfAlu.Items.FindByValue(LoginAuxili.CO_UF_INSTITUICAO) != null)
                        ddlUfAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    if (ddlUfNacionalidadeAlu.Items.FindByValue(LoginAuxili.CO_UF_INSTITUICAO) != null)
                        ddlUfNacionalidadeAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    if (ddlUfRgAlu.Items.FindByValue(LoginAuxili.CO_UF_INSTITUICAO) != null)
                        ddlUfRgAlu.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    txtNomeMaeAlu.Text = "X";                 
                }
                grdMatriculas.Visible = true;
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
            if (CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique == CurrentPadraoCadastros.BarraCadastro.botaoDelete)
            {
                excluirAluno();
                return;
            }
            if (ddlBolsaAlu.SelectedValue != "")
            {
                if (txtValorDescto.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser informado o valor da bolsa/convênio.");
                    return;
                }

                if ((txtPeriodoDeAlu.Text == "") && (txtPeriodoAteAlu.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Deve ser informado o intervalo de datas do desconto da bolsa/convênio.");
                    return; 
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                DateTime dataNasctoAlu = DateTime.Parse(txtDataNascimentoAlu.Text);

                ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( quando inclusão )
                var ocorrAlu = from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where lTb07.NO_ALU == txtNomeAlu.Text && lTb07.CO_SEXO_ALU == ddlSexoAlu.SelectedValue && lTb07.DT_NASC_ALU == dataNasctoAlu
                               select new { lTb07.CO_ALU };

                if (ocorrAlu.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno já cadastrado no sistema.");
                    return;
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                DateTime dataNasctoAlu = DateTime.Parse(txtDataNascimentoAlu.Text);

                ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( diferente do aluno informado na alteração )
                var ocorrAlu = from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where lTb07.NO_ALU == txtNomeAlu.Text && lTb07.CO_SEXO_ALU == ddlSexoAlu.SelectedValue
                               && lTb07.DT_NASC_ALU == dataNasctoAlu && lTb07.CO_ALU != coAlu
                               select new { lTb07.CO_ALU };

                if (ocorrAlu.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno já cadastrado no sistema.");
                    return;
                }
            }

            int intRetorno = 0;
            decimal decimalRetorno = 0;
            DateTime dataRetorno = DateTime.Now;
            int codImagem = upImagemAluno.GravaImagem();

            TB07_ALUNO tb07 = RetornaEntidade();

            tb07.FL_LIST_ESP = "N";

            ///Variável que guarda informações da instituição do usuário logado
            var tb000 = (from iTb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                         where iTb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                         select new { iTb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN, iTb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO }).First();

            ///Variável que vai guardar se NIRE é automático ou não
            string strTipoNireAuto = "";

            if (tb000.FLA_CTRL_TIPO_ENSIN == TipoControle.I.ToString())
            {
                strTipoNireAuto = tb000.FLA_GERA_NIRE_AUTO != null ? tb000.FLA_GERA_NIRE_AUTO : "";
            }
            else if (tb000.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString())
            {
                ///Variável que guarda informações da unidade selecionada pelo usuário logado
                var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           where iTb25.CO_EMP == LoginAuxili.CO_EMP
                           select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

                strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";
            }

            if (strTipoNireAuto != "")
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {                    
                    ///Faz a verificação para saber se o NIRE é automático ou não
                    if (strTipoNireAuto == "N")
                    {
                        if (txtNireAlu.Text == "")
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE deve ser informado.");
                            return;
                        }

                        int nuNire = int.Parse(txtNireAlu.Text);

                        ///Faz a verificação para saber se o NIRE informado já existe
                        var ocorrNIRE = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                         where lTb07.NU_NIRE == nuNire
                                         select new { lTb07.CO_ALU }).ToList();


                        if (ocorrNIRE.Count() > 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE informado já existe para outro aluno.");
                            return;
                        }
                        else
                            tb07.NU_NIRE = int.Parse(txtNireAlu.Text);
                    }
                    else
                    {
                        ///Adiciona no campo NU_NIRE o número do último NU_NIRE + 1
                        var lastNuNire = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros() select new { lTb07.NU_NIRE }).ToList();
                        if (lastNuNire.Count() > 0)
                        {
                            var n = lastNuNire.Max(x => x.NU_NIRE);
                            tb07.NU_NIRE = n + 1;
                        }
                        else
                            tb07.NU_NIRE = 1;
                    }
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    if (strTipoNireAuto == "N")
                    {
                        int nuNire = int.Parse(txtNireAlu.Text);
                        int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                        ///Faz a verificação para saber se o NIRE informado já existe ( diferente do aluno informado )
                        var ocorrNIRE = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                         where lTb07.NU_NIRE == nuNire && lTb07.CO_ALU != coAlu
                                         select new { lTb07.CO_ALU }).ToList();

                        if (ocorrNIRE.Count() > 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE informado já existe para outro aluno.");
                            return;
                        }
                        else
                            tb07.NU_NIRE = int.Parse(txtNireAlu.Text);
                    }
                    else
                        if (txtNireAlu.Text == "")
                        {
                            var lastNuNire = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros() select new { lTb07.NU_NIRE }).OrderBy(a => a.NU_NIRE).ToList();
                            if (lastNuNire.Count() > 0)
                            {
                                tb07.NU_NIRE = lastNuNire.ToList().Max().NU_NIRE + 1;
                            }
                            else
                                tb07.NU_NIRE = 1;
                        }
                }
            }
            else
                tb07.NU_NIRE = int.Parse(txtNireAlu.Text);

            ///Se inclusão, define a FL_INCLU_ALU(flag de inclusão) = true e FL_ALTER_ALU(flag de alteração) = false
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb07.FL_INCLU_ALU = true;
                tb07.FL_ALTER_ALU = false;
            }

            ///Se alteração, define a FL_ALTER_ALU(flag de alteração) = true
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                tb07.FL_ALTER_ALU = true;
            }

            if (tb07.CO_ALU == 0)
            {
                tb07.CO_EMP = LoginAuxili.CO_EMP;
                tb07.DT_SITU_ALU = DateTime.Now;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            }

            #region Bloco foto
            tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
            tb07.CO_TIPO_SANGUE_ALU = ddlTpSangueFAlu.SelectedValue != "" ? ddlTpSangueFAlu.SelectedValue : null;
            tb07.CO_STATUS_SANGUE_ALU = ddlStaSangueFAlu.SelectedValue != "" ? ddlStaSangueFAlu.SelectedValue : null;
            #endregion
            #region Bloco 1
            tb07.NU_NIS = decimal.TryParse(txtNisAlu.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb07.NO_ALU = txtNomeAlu.Text.ToUpper();

            tb07.FL_NEGAT_SERASA = "N";
            tb07.FL_NEGAT_CHEQUE = "N";
            tb07.FL_NEGAT_SPC = "N";

            tb07.NO_APE_ALU = txtApelidoAlu.Text.ToUpper();
            tb07.CO_SEXO_ALU = ddlSexoAlu.SelectedValue != "" ? ddlSexoAlu.SelectedValue : null;
            tb07.DT_NASC_ALU = DateTime.TryParse(txtDataNascimentoAlu.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.TP_RACA = ddlEtniaAlu.SelectedValue != "" ? ddlEtniaAlu.SelectedValue : null;
            tb07.TP_DEF = ddlDeficienciaAlu.SelectedValue;
            tb07.CO_NACI_ALU = ddlNacionalidadeAlu.SelectedValue != "" ? ddlNacionalidadeAlu.SelectedValue : null;
            tb07.DE_NATU_ALU = txtNaturalidadeAlu.Text;
            if (ddlNacionalidadeAlu.SelectedValue == Nacionalidade.B.ToString())
                tb07.CO_UF_NATU_ALU = ddlUfNacionalidadeAlu.SelectedValue != "" ? ddlUfNacionalidadeAlu.SelectedValue : null;
            tb07.CO_ORIGEM_ALU = ddlOrigem.SelectedValue;
            tb07.NO_WEB_ALU = txtEmailAlu.Text;
            tb07.NU_TELE_RESI_ALU = txtTelResidencialAlu.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb07.NU_TELE_CELU_ALU = txtTelCelularAlu.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb07.CO_ANO_ORI = ((txtAnoOri.Text  != "" && txtAnoOri.Text.Length == 4) ? txtAnoOri.Text : null);
            #endregion
            #region Bloco 3
            tb07.FLA_PASSE_ESCOLA = chkMerenEscolar.Checked;
            tb07.QTD_PASSE_ESCOLAR = txtQtdPasseAlu.Text == "" ? 0 : int.Parse(txtQtdPasseAlu.Text);
            tb07.FLA_TRANSP_ESCOLAR = chkTranspEscolar.Checked ? "S" : "N";
            tb07.CO_FLAG_MERENDA = chkMerenEscolar.Checked ? "S" : "N";
            tb07.CO_FLAG_LANCHONETE = chkLanchoneteAlu.Checked ? "S" : "N";
            tb07.VL_CRED_LANCHONETE = decimal.TryParse(txtCredLanchoAlu.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb07.RENDA_FAMILIAR = ddlRendaFamiliarAlu.SelectedValue != "" ? ddlRendaFamiliarAlu.SelectedValue : null;
            #endregion
            #region Bloco 4
            tb07.TP_CERTIDAO = ddlTipoCertidaoAlu.SelectedValue;
            tb07.NU_CERT = txtNumeroCertidaoAlu.Text;
            tb07.DE_CERT_LIVRO = txtLivroAlu.Text;
            tb07.NU_CERT_FOLHA = txtFolhaAlu.Text;
            tb07.DE_CERT_CARTORIO = txtCartorioAlu.Text;
            tb07.NO_CIDA_CARTORIO_ALU = txtCidadeCertidaoAlu.Text;
            tb07.CO_UF_CARTORIO = ddlUFCertidaoAlu.SelectedValue != "" ? ddlUFCertidaoAlu.SelectedValue : null;
            tb07.CO_RG_ALU = txtRgAlu.Text;
            tb07.DT_EMIS_RG_ALU = DateTime.TryParse(txtDataEmissaoRgAlu.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.CO_ORG_RG_ALU = txtOrgaoEmissorAlu.Text;
            tb07.CO_ESTA_RG_ALU = ddlUfRgAlu.SelectedValue != "" ? ddlUfRgAlu.SelectedValue : null;
            tb07.NU_TIT_ELE = txtNumeroTituloAlu.Text;
            tb07.NU_ZONA_ELE = txtZonaAlu.Text;
            tb07.NU_SEC_ELE = txtSecaoAlu.Text;
            tb07.CO_UF_TIT_ELE = ddlUfTituloAlu.SelectedValue != "" ? ddlUfTituloAlu.SelectedValue : null;
            tb07.NU_CPF_ALU = txtCpfAlu.Text.Replace("-", "").Replace(".", "");
            tb07.NU_PASSAPORTE_ALU = txtPassaporteAlu.Text != "" ? (int?)int.Parse(txtPassaporteAlu.Text) : null;
            tb07.NU_CARTAO_SAUDE_ALU = decimal.TryParse(txtCartaoSaudeAlu.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb07.NU_CARTAO_VACINA_ALU = decimal.TryParse(txtCartaoVacinaAlu.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            #endregion
            #region Bloco 5
            tb07.CO_CEP_ALU = txtCepAlu.Text.Replace("-", "");
            tb07.DE_ENDE_ALU = txtLogradouroAlu.Text;
            tb07.NU_ENDE_ALU = int.TryParse(txtNumeroAlu.Text, out intRetorno) ? (int?)intRetorno : null;
            tb07.DE_COMP_ALU = txtComplementoAlu.Text;
            tb07.TB905_BAIRRO = int.TryParse(ddlBairroAlu.SelectedValue, out intRetorno) ? TB905_BAIRRO.RetornaPelaChavePrimaria(intRetorno) : null;
            tb07.CO_ESTA_ALU = ddlUfAlu.SelectedValue != "" ? ddlUfAlu.SelectedValue : null;
            tb07.CO_FLAG_MORA_PAIS = chkMoraPais.Checked ? "S" : "N";
            #endregion
            #region Bloco 6
            tb07.NO_MAE_ALU = txtNomeMaeAlu.Text;
            tb07.DT_NASC_MAE = DateTime.TryParse(txtDataNascMae.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.NU_TEL_MAE = txtTelefMae.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb07.FLA_OBITO_MAE = ddlObitoMae.SelectedValue;
            tb07.NO_EMAIL_MAE = txtEmailMae.Text != "" ? txtEmailMae.Text : null;
            tb07.NO_FACEBOOK_MAE = txtFacebookMae.Text != "" ? txtFacebookMae.Text : null;
            tb07.NO_TWITTER_MAE = txtTwitterMae.Text != "" ? txtTwitterMae.Text : null;
            tb07.NO_PAI_ALU = txtNomePaiAlu.Text;
            tb07.DT_NASC_PAI = DateTime.TryParse(txtDataNascPai.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.NU_TEL_PAI = txtTelefPai.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb07.FLA_OBITO_PAI = ddlObitoPai.SelectedValue;
            tb07.NO_EMAIL_PAI = txtEmailPai.Text != "" ? txtEmailPai.Text : null;
            tb07.NO_FACEBOOK_PAI = txtFacebookPai.Text != "" ? txtFacebookPai.Text : null;
            tb07.NO_TWITTER_PAI = txtTwitterPai.Text != "" ? txtTwitterPai.Text : null;
            tb07.CO_FLAG_PAIS_MORAM_JUNTOS = chkPaisMorJunt.Checked ? "S" : "N";
            tb07.TB148_TIPO_BOLSA = ddlBolsaAlu.SelectedValue != "" ? TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(ddlBolsaAlu.SelectedValue)) : null;
            #endregion

            if (ddlBolsaAlu.SelectedValue != "")
            {
                if (chkDesctoPercBolsa.Checked)
                {
                    tb07.NU_PEC_DESBOL = decimal.TryParse(txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb07.NU_VAL_DESBOL = null;
                }
                else
                {
                    tb07.NU_VAL_DESBOL = decimal.TryParse(txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb07.NU_PEC_DESBOL = null;
                }

                tb07.DT_VENC_BOLSA = DateTime.TryParse(txtPeriodoDeAlu.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb07.DT_VENC_BOLSAF = DateTime.TryParse(txtPeriodoAteAlu.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb07.FLA_BOLSISTA = "S";
            }
            else
            {
                tb07.DT_VENC_BOLSA = tb07.DT_VENC_BOLSAF = null;
                tb07.NU_VAL_DESBOL = null;
                tb07.NU_PEC_DESBOL = null;
                tb07.FLA_BOLSISTA = "N";
            }

            tb07.FL_AUTORI_SAIDA = ddlAutorSaida.SelectedValue;

            tb07.DES_OBSERVACAO = txtObservacoesAlu.Text.Length > 500 ? txtObservacoesAlu.Text.Substring(0, 500) : txtObservacoesAlu.Text;
            tb07.DT_SITU_ALU = tb07.CO_SITU_ALU == ddlSituacaoAlu.SelectedValue ? tb07.DT_SITU_ALU : DateTime.Now;
            tb07.CO_SITU_ALU = ddlSituacaoAlu.SelectedValue;
                                                   
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb07.DT_CADA_ALU = DateTime.Now;

            tb07.DT_ALT_REGISTRO = DateTime.Now;                                                                                                                                                                                                                                                                      
            tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);                                                            

          
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                ///Quando for exclusão deve primeiro excluir os registros de Instituições de Apoio e Programas Sociais
                tb07.TB136_ALU_PROG_SOCIAIS.Clear();
                tb07.TB164_INST_ESP.Clear();
                tb07.TB25_EMPRESA1Reference.Load();

                if (TB07_ALUNO.Delete(tb07, true) > 0)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Excluido com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                ///Quando for inclusão deve primeiro incluir o aluno para depois salvar os registros de Instituições de Apoio e Programas Sociais
                if (GestorEntities.SaveOrUpdate(tb07) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar registro.");
                    return;
                }

                tb07.DT_ALT_REGISTRO = DateTime.Now;
                CurrentPadraoCadastros.CurrentEntity = tb07;
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                tb07.TB164_INST_ESP.Clear();
                tb07.TB136_ALU_PROG_SOCIAIS.Clear();

                tb07.DT_ALT_REGISTRO = DateTime.Now;
                

                if (TB07_ALUNO.SaveOrUpdate(tb07, true) > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Alterado com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

                CurrentPadraoCadastros.CurrentEntity = tb07;
            }            
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            excluirAluno();
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB07_ALUNO tb07 = RetornaEntidade();

            if (tb07 != null)
            {
                tb07.TB108_RESPONSAVELReference.Load();
                tb07.ImageReference.Load();
                tb07.TB164_INST_ESP.Load();
                tb07.TB136_ALU_PROG_SOCIAIS.Load();
                tb07.TB905_BAIRROReference.Load();

                if (tb07.TB108_RESPONSAVEL != null)
                {
                    ///Carrega responsável do aluno e preenche os campos na tela de cadastro do aluno
                    var infoResp = TB108_RESPONSAVEL.RetornaPelaChavePrimaria((int)tb07.TB108_RESPONSAVEL.CO_RESP);

                    txtResponsavelAlu.Text = infoResp.NO_RESP;
                    txtDtNascRespAlu.Text = infoResp.DT_NASC_RESP != null ? infoResp.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                    ddlSexoRespAlu.SelectedValue = infoResp.CO_SEXO_RESP;
                    txtCPFRespAlu.Text = infoResp.NU_CPF_RESP;
                    chkRespFunc.Checked = infoResp.CO_FLAG_RESP_FUNC == "S" ? true : false;
                    txtTelefResiResp.Text = infoResp.NU_TELE_RESI_RESP;
                    txtCelularResp.Text = infoResp.NU_TELE_CELU_RESP;
                }
                
                if (ParentescoResponsavel.PM.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Pai/Mãe";
                else if (ParentescoResponsavel.TI.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Tio(a)";
                else if (ParentescoResponsavel.AV.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Avô/Avó";
                else if (ParentescoResponsavel.PR.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Primo(a)";
                else if (ParentescoResponsavel.CN.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Cunhado(a)";
                else if (ParentescoResponsavel.TU.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Tutor(a)";
                else if (ParentescoResponsavel.IR.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Irmão(ã)";
                else if (ParentescoResponsavel.OU.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    txtGrauParentescoAlu.Text = "Outros";

                if (tb07.Image != null)
                    upImagemAluno.CarregaImagem(tb07.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);
                ddlTpSangueFAlu.SelectedValue = tb07.CO_TIPO_SANGUE_ALU != null ? tb07.CO_TIPO_SANGUE_ALU : "";
                ddlStaSangueFAlu.SelectedValue = tb07.CO_STATUS_SANGUE_ALU != null ? tb07.CO_STATUS_SANGUE_ALU : "";
                txtCartaoSaudeAlu.Text = tb07.NU_CARTAO_SAUDE_ALU == null ? "" : tb07.NU_CARTAO_SAUDE_ALU.ToString();
                txtCartorioAlu.Text = tb07.DE_CERT_CARTORIO;
                txtCepAlu.Text = tb07.CO_CEP_ALU;
                txtComplementoAlu.Text = tb07.DE_COMP_ALU;
                txtCpfAlu.Text = tb07.NU_CPF_ALU;
                txtDataSituacaoAlu.Text = tb07.DT_SITU_ALU != null ? tb07.DT_SITU_ALU.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                txtDataEmissaoRgAlu.Text = tb07.DT_EMIS_RG_ALU != null ? tb07.DT_EMIS_RG_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtDataNascimentoAlu.Text = tb07.DT_NASC_ALU != null ? tb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";                
                txtEmailAlu.Text = tb07.NO_WEB_ALU;
                txtFolhaAlu.Text = tb07.NU_CERT_FOLHA != null?tb07.NU_CERT_FOLHA:"999";
                txtLivroAlu.Text = tb07.DE_CERT_LIVRO !=null? tb07.DE_CERT_LIVRO: "999";
                txtLogradouroAlu.Text = tb07.DE_ENDE_ALU;
                txtNaturalidadeAlu.Text = tb07.DE_NATU_ALU;
                txtNisAlu.Text = tb07.NU_NIS != null ? tb07.NU_NIS.ToString().PadLeft(9, '0') : "";
                txtNireAlu.Text = tb07.NU_NIRE.ToString().PadLeft(9,'0');
                txtNomeAlu.Text = tb07.NO_ALU;
                txtApelidoAlu.Text = tb07.NO_APE_ALU;
                txtNomeMaeAlu.Text = tb07.NO_MAE_ALU;
                txtDataNascMae.Text = tb07.DT_NASC_MAE != null ? tb07.DT_NASC_MAE.Value.ToString("dd/MM/yyyy") : "";
                txtTelefMae.Text = tb07.NU_TEL_MAE;
                ddlObitoMae.SelectedValue = tb07.FLA_OBITO_MAE != null ? tb07.FLA_OBITO_MAE : "N";
                txtEmailMae.Text = tb07.NO_EMAIL_MAE;
                txtFacebookMae.Text = tb07.NO_FACEBOOK_MAE;
                txtTwitterMae.Text = tb07.NO_TWITTER_MAE;
                txtNomePaiAlu.Text = tb07.NO_PAI_ALU;
                txtDataNascPai.Text = tb07.DT_NASC_PAI != null ? tb07.DT_NASC_PAI.Value.ToString("dd/MM/yyyy") : "";
                txtTelefPai.Text = tb07.NU_TEL_PAI;
                ddlObitoPai.SelectedValue = tb07.FLA_OBITO_PAI != null ? tb07.FLA_OBITO_PAI : "N";
                txtEmailPai.Text = tb07.NO_EMAIL_PAI;
                txtFacebookPai.Text = tb07.NO_FACEBOOK_PAI;
                txtTwitterPai.Text = tb07.NO_TWITTER_PAI;
                txtNumeroAlu.Text = tb07.NU_ENDE_ALU != null ? tb07.NU_ENDE_ALU.ToString() : "";
                txtNumeroCertidaoAlu.Text = tb07.NU_CERT;
                txtNumeroTituloAlu.Text = tb07.NU_TIT_ELE;
                txtObservacoesAlu.Text = tb07.DES_OBSERVACAO;
                txtOrgaoEmissorAlu.Text = tb07.CO_ORG_RG_ALU;                
                txtRgAlu.Text = tb07.CO_RG_ALU;
                txtSecaoAlu.Text = tb07.NU_SEC_ELE;
                txtTelCelularAlu.Text = tb07.NU_TELE_CELU_ALU;
                txtTelResidencialAlu.Text = tb07.NU_TELE_RESI_ALU;
                txtZonaAlu.Text = tb07.NU_ZONA_ELE;
                chkMerenEscolar.Checked = tb07.CO_FLAG_MERENDA == "S";
                ddlUfAlu.SelectedValue = tb07.CO_ESTA_ALU;
                CarregaCidades();
                ddlCidadeAlu.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                CarregaBairros();
                ddlBairroAlu.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_BAIRRO.ToString() : "";
                tb07.TB148_TIPO_BOLSAReference.Load();
                #region Bolsa
                if (tb07.TB148_TIPO_BOLSA != null)
                {
                    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                                 where iTb148.CO_TIPO_BOLSA == tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA
                                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.TP_GRUPO_BOLSA }).FirstOrDefault();

                    if (tb148 != null)
                    {
                        ddlTipoBolsa.SelectedValue = tb148.TP_GRUPO_BOLSA;
                        CarregaBolsas();
                        ddlBolsaAlu.SelectedValue = tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString();

                        chkDesctoPercBolsa.Checked = tb07.NU_PEC_DESBOL != null;
                        txtValorDescto.Text = chkDesctoPercBolsa.Checked ? (tb07.NU_PEC_DESBOL != null ? String.Format("{0:N}", tb07.NU_PEC_DESBOL) : "") : (tb07.NU_VAL_DESBOL != null ? String.Format("{0:N}", tb07.NU_VAL_DESBOL) : "");

                        chkDesctoPercBolsa.Enabled = chkDesctoPercBolsa.Enabled = txtValorDescto.Enabled = true;

                        txtPeriodoDeAlu.Text = tb07.DT_VENC_BOLSA != null ? tb07.DT_VENC_BOLSA.Value.ToString("dd/MM/yyyy") : "";
                        txtPeriodoAteAlu.Text = tb07.DT_VENC_BOLSAF != null ? tb07.DT_VENC_BOLSAF.Value.ToString("dd/MM/yyyy") : "";

                        txtPeriodoDeAlu.Enabled = txtPeriodoAteAlu.Enabled = true;
                    }
                    else
                    {
                        chkDesctoPercBolsa.Enabled =
                        txtValorDescto.Enabled = txtPeriodoDeAlu.Enabled = txtPeriodoAteAlu.Enabled = false;
                    }
                }
                #endregion
                ddlDeficienciaAlu.SelectedValue = tb07.TP_DEF;
                //ddlEstadoCivilAlu.SelectedValue = tb07.CO_ESTADO_CIVIL != null ? tb07.CO_ESTADO_CIVIL : "";
                ddlEtniaAlu.SelectedValue = tb07.TP_RACA != null || tb07.TP_RACA == "" ? tb07.TP_RACA : "X";
                ddlNacionalidadeAlu.SelectedValue = tb07.CO_NACI_ALU;
                chkPasseEscolar.Checked = tb07.FLA_PASSE_ESCOLA != null ? tb07.FLA_PASSE_ESCOLA.Value : false;
                txtQtdPasseAlu.Text = tb07.QTD_PASSE_ESCOLAR != null ? tb07.QTD_PASSE_ESCOLAR.ToString() : "";
                chkTranspEscolar.Checked = tb07.FLA_TRANSP_ESCOLAR == "S";
                ddlRendaFamiliarAlu.SelectedValue = tb07.RENDA_FAMILIAR;
                ddlSexoAlu.SelectedValue = tb07.CO_SEXO_ALU;
                ddlSituacaoAlu.SelectedValue = tb07.CO_SITU_ALU;
                ddlTipoCertidaoAlu.SelectedValue = tb07.TP_CERTIDAO;
                ddlUfNacionalidadeAlu.SelectedValue = tb07.CO_UF_NATU_ALU;
                ddlUfRgAlu.SelectedValue = tb07.CO_ESTA_RG_ALU;
                ddlUfTituloAlu.SelectedValue = tb07.CO_UF_TIT_ELE;
                ddlOrigem.SelectedValue = tb07.CO_ORIGEM_ALU != null ? tb07.CO_ORIGEM_ALU : "SR";
                chkLanchoneteAlu.Checked = tb07.CO_FLAG_LANCHONETE == "S";
                txtCredLanchoAlu.Enabled = chkLanchoneteAlu.Checked;
                txtQtdPasseAlu.Enabled = chkPasseEscolar.Checked;                
                txtCredLanchoAlu.Text = tb07.VL_CRED_LANCHONETE != null ? tb07.VL_CRED_LANCHONETE.ToString() : "";
                txtCidadeCertidaoAlu.Text = tb07.NO_CIDA_CARTORIO_ALU;
                ddlUFCertidaoAlu.SelectedValue = tb07.CO_UF_CARTORIO != null ? tb07.CO_UF_CARTORIO : "";
                txtPassaporteAlu.Text = tb07.NU_PASSAPORTE_ALU != null ? tb07.NU_PASSAPORTE_ALU.ToString() : "";
                txtCartaoVacinaAlu.Text = tb07.NU_CARTAO_VACINA_ALU != null ? tb07.NU_CARTAO_VACINA_ALU.ToString() : "";
                chkMoraPais.Checked = tb07.CO_FLAG_MORA_PAIS == "S";
                chkPaisMorJunt.Checked = tb07.CO_FLAG_PAIS_MORAM_JUNTOS == "S";
                ddlAutorSaida.SelectedValue = tb07.FL_AUTORI_SAIDA != null ? tb07.FL_AUTORI_SAIDA : "S";

                var tb08 = (from iTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on iTb08.CO_CUR equals tb01.CO_CUR
                            join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on iTb08.CO_TUR equals tb129.CO_TUR
                            where iTb08.CO_ALU == tb07.CO_ALU
                            select new listaMatriculas{ 
                                CO_ANO_MES_MAT = iTb08.CO_ANO_MES_MAT,
                                DE_MODU_CUR = iTb08.TB44_MODULO.DE_MODU_CUR,
                                CO_SIGL_CUR = tb01.CO_SIGL_CUR,
                                CO_SIGLA_TURMA = tb129.CO_SIGLA_TURMA,
                                CO_SIT_MAT = iTb08.CO_SIT_MAT,
                                CO_STA_APROV = iTb08.CO_STA_APROV
                            }).ToList().OrderByDescending( p => p.CO_ANO_MES_MAT );

                if (tb08.Count() > 0)
                {
                    grdMatriculas.Visible = true;
                    grdMatriculas.DataSource = tb08;
                    grdMatriculas.DataBind();
                }

                
                var tb293 = (from iTb293 in TB293_CUIDAD_SAUDE.RetornaTodosRegistros()
                             where iTb293.TB07_ALUNO.CO_ALU == tb07.CO_ALU
                             select iTb293);

                chkCuidaSaude.Checked = tb293.Count() > 0;

                var tb294 = (from iTb294 in TB294_RESTR_ALIMEN.RetornaTodosRegistros()
                             where iTb294.TB07_ALUNO.CO_ALU == tb07.CO_ALU
                             select iTb294);

                chkRestrAlime.Checked = tb294.Count() > 0;

                DateTime dataAtual = DateTime.Parse(DateTime.Now.ToShortDateString() + " 00:00:00"); 

                var tb47 = (from iTb294 in TB47_CTA_RECEB.RetornaTodosRegistros()
                             where iTb294.CO_ALU == tb07.CO_ALU && iTb294.IC_SIT_DOC == "A"
                             && iTb294.DT_VEN_DOC < dataAtual
                             select iTb294);

                chkPendeFinan.Checked = tb47.Count() > 0;

                var tb123 = (from iTb123 in TB123_EMPR_BIB_ITENS.RetornaTodosRegistros()
                             where iTb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.TB07_ALUNO.CO_ALU == tb07.CO_ALU
                            && iTb123.DT_PREV_DEVO_ACER < dataAtual && iTb123.DT_REAL_DEVO_ACER == null
                             select iTb123);

                chkPendeBibli.Checked = tb123.Count() > 0;

                var tb64 = (from iTb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                            where iTb64.CO_ALU == tb07.CO_ALU
                            && iTb64.DT_PREV_ENTR < dataAtual && iTb64.CO_SIT == "A"
                            select iTb64);

                chkPendeSecre.Checked = tb64.Count() > 0;

                if (tb07.DE_ENDE_ALU != null)
                {                    
                    
                    string descEnd = tb07.DE_ENDE_ALU + ",";
                    descEnd = tb07.NU_ENDE_ALU != null ? descEnd + tb07.NU_ENDE_ALU.ToString() + "," : descEnd;
                    carregaMapa(descEnd + ddlCidadeAlu.SelectedItem + " - " + tb07.CO_ESTA_ALU);
                }
                else
                    tbMap.Visible = false;

                txtAnoOri.Text = tb07.CO_ANO_ORI ?? "";
            }
            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>4
        private TB07_ALUNO RetornaEntidade()
        {
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }
     
        /// <summary>
        /// Apresenta o mapa com a localização do endereço do aluno
        /// </summary>
        /// <param name="endAluno">Endereço do aluno</param>
        private void carregaMapa(string endAluno)
        {
            if (AuxiliValidacao.IsConnected())
            {
                ///ATIVA MAP GOOGLE PARA ATIVAR COLOCAR COMO TRUE 
                tbMap.Visible = true;
                
                ///Chave GoogleMaps
                GMapa.Key = ConfigurationManager.AppSettings.Get(AppSettings.GoogleMapsKey);
                GMapa.Address = endAluno;
                GMapa.Markers.Clear();
                GMapa.Markers.Add(new GoogleMarker(endAluno));

                GMapa.Zoom = 15;
            }
        }
        /// <summary>
        /// Exclusão de aluno
        /// </summary>
        private void excluirAluno()
        {
            bool excluido = false;
            try
            {
                TB07_ALUNO tb07 = RetornaEntidade();

                if (tb07 != null)
                {
                    tb07.TB08_MATRCUR.Load();
                    if (tb07.TB08_MATRCUR.Count > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível excluir pois existe matrícula para o aluno.");
                        return;
                    }
                    tb07.TB48_GRADE_ALUNO.Load();
                    if (tb07.TB48_GRADE_ALUNO.Count > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível excluir pois existe matrícula e grade para o aluno.");
                        return;
                    }
                    tb07.TB205_USUARIO_BIBLIOT.Load();
                    if (tb07.TB205_USUARIO_BIBLIOT.Count > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível excluir pois o aluno é usuário da biblioteca.");
                        return;
                    }
                    var contas = (from tb49 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                      where tb49.CO_ALU == tb07.CO_ALU
                                      select tb49
                                      ).DefaultIfEmpty();
                    if (contas != null && contas.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível excluir pois o aluno possuí títulos.");
                        return;
                    }

                    tb07.TB164_INST_ESP.Clear();
                    tb07.TB136_ALU_PROG_SOCIAIS.Clear();
                    tb07.TB_TRANSF_INTERNA.Clear();
                    tb07.TB052_RESERV_MATRI.Clear();
                    tb07.TB106_ATIVEXTRA_ALUNO.Clear();
                    tb07.TB120_DOC_ALUNO_ENT.Clear();
                    tb07.TB241_ALUNO_ENDERECO.Clear();
                    tb07.TB242_ALUNO_TELEFONE.Clear();
                    tb07.TB293_CUIDAD_SAUDE.Clear();
                    tb07.TB294_RESTR_ALIMEN.Clear();

                    if (GestorEntities.Delete(tb07) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                }
                excluido = true;
            }
            catch
            {
                excluido = false;
            }
            if (excluido)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Aluno excluído com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            else
                AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível excluir o aluno solicitado.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }
        #endregion

        #region Carregamento
        
        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDown de UF</param>
        private void CarregaUfs(DropDownList ddl)
        {
            ddl.DataSource = TB74_UF.RetornaTodosRegistros();

            ddl.DataTextField = "CODUF";
            ddl.DataValueField = "CODUF";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidadeAlu.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUfAlu.SelectedValue);

            ddlCidadeAlu.DataTextField = "NO_CIDADE";
            ddlCidadeAlu.DataValueField = "CO_CIDADE";
            ddlCidadeAlu.DataBind();

            ddlCidadeAlu.Enabled = ddlCidadeAlu.Items.Count > 0;
            ddlCidadeAlu.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidadeAlu.SelectedValue != "" ? int.Parse(ddlCidadeAlu.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairroAlu.Enabled = false;
                ddlBairroAlu.Items.Clear();
                return;
            }
            else
            {
                ddlBairroAlu.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairroAlu.DataTextField = "NO_BAIRRO";
                ddlBairroAlu.DataValueField = "CO_BAIRRO";
                ddlBairroAlu.DataBind();

                ddlBairroAlu.Enabled = ddlBairroAlu.Items.Count > 0;
                ddlBairroAlu.Items.Insert(0, new ListItem("", ""));
            }            
        }

        /// <summary>
        /// Método que carrega o dropdown de Bolsa
        /// </summary>
        private void CarregaBolsas()
        {
            ddlBolsaAlu.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where( c => c.TP_GRUPO_BOLSA == ddlTipoBolsa.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                && c.CO_SITUA_TIPO_BOLSA == "A");

            ddlBolsaAlu.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaAlu.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaAlu.DataBind();

            ddlBolsaAlu.Items.Insert(0, new ListItem("Nenhuma", ""));

            txtValorDescto.Text = txtPeriodoDeAlu.Text = txtPeriodoAteAlu.Text = "";
            chkDesctoPercBolsa.Checked = chkDesctoPercBolsa.Enabled =
            txtValorDescto.Enabled = txtPeriodoDeAlu.Enabled = txtPeriodoAteAlu.Enabled = false;
        }
        /// <summary>
        /// Carrega os tipos de deficiência do aluno
        /// </summary>
        private void CarregaDeficiencia()
        {
            ddlDeficienciaAlu.Items.Clear();
            ddlDeficienciaAlu.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoDeficienciaAluno.ResourceManager));
            ddlDeficienciaAlu.SelectedValue = tipoDeficiencia[tipoDeficienciaAluno.N];
        }

        #endregion

        #region Validadores

        /// <summary>
        /// Método que faz a verificação se CPF informado é válido
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void cvCPF_ServerValidate(object source, ServerValidateEventArgs e)
        {
            string strCpf = e.Value.Replace(".", "").Replace("-", "");
            e.IsValid = AuxiliValidacao.ValidaCpf(strCpf);

            if (!e.IsValid)
            {
                AuxiliPagina.EnvioMensagemErro(this, MensagensErro.CPFInvalido);
            }
        }

        /// <summary>
        /// Método que faz a verificação da obrigatoriedade do NIRE
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void cvVerifObrigNIRE_ServerValidate(object source, ServerValidateEventArgs e)
        {
//--------> Variável que guarda informações da instituição do usuário logado
            var tb000 = (from iTb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                         where iTb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                         select new { iTb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN, iTb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO }).First();

//--------> Variável que vai guardar se NIRE é automático ou não
            string strTipoNireAuto = "";

            if (tb000.FLA_CTRL_TIPO_ENSIN == TipoControle.I.ToString())
            {
                strTipoNireAuto = tb000.FLA_GERA_NIRE_AUTO != null ? tb000.FLA_GERA_NIRE_AUTO : "";
            }
            else if (tb000.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString())
            {
//------------> Variável que guarda informações da unidade selecionada pelo usuário logado
                var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                            where iTb25.CO_EMP == LoginAuxili.CO_EMP
                            select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

                strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";
            }

            if (strTipoNireAuto != "")
            {
                if ((strTipoNireAuto == "N") && (txtNireAlu.Text == ""))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        #endregion

        #region Eventos dos componentes da página

        /// <summary>
        /// Preenche os campos de endereço de acordo com o CEP, se o mesmo possuir registro na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesquisarCepAluno_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepAlu.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepAlu.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    tb235.TB240_TIPO_LOGRADOUROReference.Load();

                    txtLogradouroAlu.Text = tb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA + " " + tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfAlu.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades();
                    ddlCidadeAlu.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros();
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

        protected void ddlBolsa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBolsaAlu.SelectedValue != "")
            {
                chkDesctoPercBolsa.Enabled = txtPeriodoDeAlu.Enabled = txtValorDescto.Enabled = txtPeriodoAteAlu.Enabled = true;

                int coBolsa = int.Parse(ddlBolsaAlu.SelectedValue);

                var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                             where iTb148.CO_TIPO_BOLSA == coBolsa
                             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                if (tb148 != null)
                {
                    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                    chkDesctoPercBolsa.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                    if (tb148.DT_INICI_TIPO_BOLSA != null)
                    {
                        txtPeriodoDeAlu.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                        txtPeriodoAteAlu.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtPeriodoDeAlu.Text = "";
                        txtPeriodoAteAlu.Text = "";
                    }
                }
            }
            else
            {
                txtValorDescto.Text = txtPeriodoDeAlu.Text = txtPeriodoAteAlu.Text = "";
                chkDesctoPercBolsa.Checked = chkDesctoPercBolsa.Enabled =
                txtValorDescto.Enabled = txtPeriodoDeAlu.Enabled = txtPeriodoAteAlu.Enabled = false;
            }
        }

        protected void ddlTipoBolsa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBolsas();
        }

        protected void chkLanchoneteAlu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLanchoneteAlu.Checked)
                txtCredLanchoAlu.Enabled = true;
            else
            {
                txtCredLanchoAlu.Enabled = false;
                txtCredLanchoAlu.Text = "";
            }
        }

        protected void chkPasseEscolar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasseEscolar.Checked)
                txtQtdPasseAlu.Enabled = true;
            else
            {
                txtQtdPasseAlu.Enabled = false;
                txtQtdPasseAlu.Text = "";
            }
        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        protected void ddlNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUfNacionalidadeAlu.Enabled = ddlNacionalidadeAlu.SelectedValue == Nacionalidade.B.ToString();
            ddlUfNacionalidadeAlu.SelectedIndex = 0;
        }

        #endregion

        #region Classe
        /// <summary>
        /// Classe que contem todas as matrículas do aluno
        /// </summary>
        private class listaMatriculas
        {
            public string CO_ANO_MES_MAT { get; set; }
            public string DE_MODU_CUR { get; set; }
            public string CO_SIGL_CUR { get; set; }
            public string CO_SIGLA_TURMA { get; set; }
            public string CO_SIT_MAT {
                set
                {
                    if (value != null && statusMatricula.Keys.Contains(value))
                        this.nomeMatricula = statusMatricula[value];
                    else
                        this.nomeMatricula = "Cursando";
                }
            }
            public string CO_STA_APROV
            {
                set
                {
                    if (value != null && statusAprovacao.Keys.Contains(value))
                        this.nomeAprovacao = statusAprovacao[value];
                    else
                        this.nomeAprovacao = "***";
                }
            }
            public string nomeAprovacao { get; set; }
            public string nomeMatricula { get; set; }
        }
        #endregion
    }
}
