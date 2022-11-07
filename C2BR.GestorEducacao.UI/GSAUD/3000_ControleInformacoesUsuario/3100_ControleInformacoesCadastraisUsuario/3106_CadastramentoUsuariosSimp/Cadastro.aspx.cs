//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE PAIS OU RESPONSÁVEIS 
// OBJETIVO: CADASTRAMENTO DE PAIS OU RESPONSÁVEIS DE ALUNOS
// DATA DE CRIAÇÃO: 
//---------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//---------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR          | DESCRIÇÃO RESUMIDA
// ----------+-------------------------------+-------------------------------------
// 09/03/2014| Julio Gleisson Rodrigues      |  Copia da Tela \GEDUC\3000_CtrlOperacionalPedagogico\
//           |                               |  3700_CtrlInformacoesResponsaveis\3710_CtrlInformacoesCadastraisResponsaveis
// 14/07/2016| Tayguara Acioli TA.14/07/2016 |  Alterei para preencher o CO_EMP_ORIGEM do paciente igual a unidade cadastrada.

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
using System.Web;
using System.Transactions;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3106_CadastramentoUsuariosSimp
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtAno.Text = DateTime.Now.Year.ToString();
                txtDtNascResp.Text = "01/01/1950";
                txtDtNascPaci.Text = "01/01/2000";
                txtNuIDResp.Text = "000000";
                txtOrgEmiss.Text = "SSP";
                txtDataEntrada.Text = DateTime.Now.ToShortDateString();

                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                ddlUFOrgEmis.Items.Insert(0, new ListItem("", ""));
                CarregaInformacoesSaude();
                carregaCidade();
                carregaBairro();
                CarregaDadosUnidLogada();
                VerificarNireAutomatico();
                CarregarFuncoesSimp();
                CarregarFuncoesSimpMae();
                CarregarFuncoesSimpPai();
                CarregaDeficiencia();
                CarregarIndicacao();
                CarregaProfissionaisResponsaveis(0);

                CarregaOperadoras();
                CarregaPlanos();
                CarregaCategoria();
                CarregaRestricaoDeAtendimento();
                CarregaUf();
                AuxiliCarregamentos.CarregaUFs(ddlUfOrgPac, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUfRep, false, LoginAuxili.CO_EMP);
                carregaBairroResp();
                carregaCidadeResp();

                if (!String.IsNullOrEmpty((string)this.Session[SessoesHttp.URLOrigem]) && !String.IsNullOrEmpty((string)this.Session[SessoesHttp.CodigoMatriculaAluno]))
                    CarregaFormulario();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        private void CarregaFormulario()
        {
            TB07_ALUNO tb07 = RetornaEntidade();

            if (tb07 != null)
            {
                tb07.TBS377_INDIC_PACIENTESReference.Load();
                tb07.TBS366_FUNCAO_SIMPL1Reference.Load();
                tb07.TBS366_FUNCAO_SIMPLReference.Load();
                tb07.TB250_OPERAReference.Load();
                tb07.TB251_PLANO_OPERAReference.Load();
                tb07.TB367_CATEG_PLANO_SAUDEReference.Load();
                tb07.TBS379_RESTR_ATENDReference.Load();
                tb07.TBS387_DEFICReference.Load();
                var ap = TB47_CTA_RECEB.RetornaTodosRegistros().Where(a => a.CO_ALU == tb07.CO_ALU && a.DT_VEN_DOC <= DateTime.Now && a.IC_SIT_DOC == "A").ToList();
                if (ap.Count > 0)
                    chkFinanceiroGer.Checked = true;
                else
                    chkFinanceiroGer.Checked = false;

                CarregaRestricaoDeAtendimento();
                if (tb07.TBS379_RESTR_ATEND != null)
                    ddlRestricaoAtendimento.SelectedValue = tb07.TBS379_RESTR_ATEND.ID_RESTR_ATEND.ToString();
                CarregaProfissionaisResponsaveis(tb07.CO_ALU);
                //txtMesOrg.Text = Convert.ToString(tb07.CO_MES_REFER);
                txtNirePac.Text = tb07.NU_NIRE.ToString().PadLeft(7, '0');
                txtNuNisPaci.Text = tb07.NU_NIS.ToString();
                txtCPFMOD.Text = tb07.NU_CPF_ALU;
                //txtAno.Text = tb07.CO_ANO_ORI;
                txtnompac.Text = tb07.NO_ALU.ToUpper();
                ddlSexoPaci.SelectedValue = tb07.CO_SEXO_ALU;
                txtApelidoPaciente.Text = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU.ToUpper() : "";
                txtDtNascPaci.Text = tb07.DT_NASC_ALU.ToString();
                txtTelCelPaci.Text = tb07.NU_TELE_CELU_ALU;
                txtTelResPaci.Text = tb07.NU_TELE_RESI_ALU;
                txtWhatsPaci.Text = tb07.NU_TELE_WHATS_ALU;
                txtPastaControl.Text = tb07.DE_PASTA_CONTR;
                txtTelMigrado.Text = tb07.TEL_MIGRAR;
                txtDtVencPlan.Text = tb07.DT_VENC_PLAN.ToString();

                txtNumeroIdentidade.Text = tb07.CO_RG_ALU;
                txtOrgao.Text = tb07.CO_ORG_RG_ALU;
                ddlUfOrgPac.SelectedValue = tb07.CO_UF_NATU_ALU;
                txtDataEmissao.Text = tb07.DT_EMIS_RG_ALU.ToString();
                //txtNuCarSaude.Text = tb07.NU_CARTAO_SAUDE;

                //chkDiabetico.Checked = tb07.FL_DIABE == "S" ? true : false;
                //ChkHipertenso.Checked = tb07.FL_HIPER == "S" ? true : false;
                //ChkSensivelLactose.Checked = tb07.FL_LACTO == "S" ? true : false;
                //ChkMedicacaoControlada.Checked = tb07.FL_MEDIC_CONTR == "S" ? true : false;
                //ChkCuidadesEspeciais.Checked = tb07.FL_NECES_CUIDA_ESPEC == "S" ? true : false;

                ddlObitoMae.SelectedValue = tb07.FLA_OBITO_MAE;
                ddlObitoPai.SelectedValue = tb07.FLA_OBITO_PAI;
                chkMaeResp.Checked = (tb07.FL_MAE_RESP_ATEND == "S" ? true : false);
                chkPaiResp.Checked = (tb07.FL_PAI_RESP_ATEND == "S" ? true : false);
                chkMaeResp.Enabled = (ddlObitoMae.SelectedValue == "S" ? false : true);
                chkPaiResp.Enabled = (ddlObitoPai.SelectedValue == "S" ? false : true);

                chkMaeRespFinanc.Checked = (tb07.FL_MAE_RESP_FINAN == "S" ? true : false);
                chkMaeRespFinanc.Enabled = (ddlObitoMae.SelectedValue == "S" ? false : true);

                chkPaiRespFinanc.Checked = (tb07.FL_PAI_RESP_FINAN == "S" ? true : false);
                chkPaiRespFinanc.Enabled = (ddlObitoPai.SelectedValue == "S" ? false : true);

                //Se estiver marcado como mãe ou pai responsável, então desabilita os campos 
                if ((chkMaeRespFinanc.Checked) || (chkPaiRespFinanc.Checked))
                    txtNomeResp.Enabled = ddlFuncao.Enabled = txtTelCelResp.Enabled = chkPaciEhResp.Enabled = false;

                txtDataRestricaoAtendimento.Text = Convert.ToString(tb07.DT_RESTR_ATEND);
                txtFacebookPac.Text = tb07.NO_FACEB_PACI;
                ddlSituacaoAlu.SelectedValue = tb07.CO_SITU_ALU;
                if (tb07.DT_SITU_ALU.HasValue)
                {
                    lblDtSitua.Text = tb07.DT_SITU_ALU.Value.ToShortDateString();
                    lblDtSitua.Visible = true;
                }
                ddlDeficienciaAlu.SelectedValue = (tb07.TBS387_DEFIC != null ? tb07.TBS387_DEFIC.ID_DEFIC.ToString() : "");
                ddlEtniaAlu.SelectedValue = tb07.TP_RACA;
                txtAltura.Text = tb07.NU_ALTU.ToString();
                txtPeso.Text = tb07.NU_PESO.ToString();
                ddlOrigemPaci.SelectedValue = tb07.CO_ORIGEM_ALU;
                txtNaturalidade.Text = tb07.DE_NATU_ALU;
                ddlEstadoCivil.SelectedValue = tb07.CO_ESTADO_CIVIL;
                txtEmailPaci.Text = tb07.NO_WEB_ALU;
                chkDocumentos.Checked = tb07.FL_PENDE_DOCUM == "S" ? true : false;
                chkFinanceiroGer.Checked = tb07.FL_PENDE_FINAN_GER == "S" ? true : false;
                chkPlanoDeSaude.Checked = tb07.FL_PENDE_PLANO_CONVE == "S" ? true : false;

                txtTelMae.Text = tb07.NU_TEL_MAE;
                txtTelPai.Text = tb07.NU_TEL_PAI;
                if (tb07.DT_ENTRA_INSTI.HasValue)
                {
                    txtDataEntrada.Text = tb07.DT_ENTRA_INSTI.ToString();
                    txtDataEntrada.Enabled = false;
                }

                //if (tb07.TBS377_INDIC_PACIENTES != null)
                //    ddlIndicacao.SelectedValue = tb07.TBS377_INDIC_PACIENTES.ID_INDIC_PACIENTES.ToString();

                if (tb07.CO_COL_INDICACAO.HasValue && ddlIndicacao.Items.FindByValue(tb07.CO_COL_INDICACAO.ToString()) != null)
                    ddlIndicacao.SelectedValue = tb07.CO_COL_INDICACAO.ToString();

                txtNomePai.Text = !String.IsNullOrEmpty(tb07.NO_PAI_ALU) ? tb07.NO_PAI_ALU.ToUpper() : "";
                txtNomeMae.Text = !String.IsNullOrEmpty(tb07.NO_MAE_ALU) ? tb07.NO_MAE_ALU.ToUpper() : "";

                if (tb07.TBS366_FUNCAO_SIMPL != null)
                    ddlProfissaoNomeMae.SelectedValue = tb07.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString();

                if (tb07.TBS366_FUNCAO_SIMPL1 != null)
                    ddlProfissaoNomePai.SelectedValue = tb07.TBS366_FUNCAO_SIMPL1.ID_FUNCAO_SIMPL.ToString();

                ddlGrParen.SelectedValue = tb07.CO_GRAU_PAREN_RESP;
                hidCoPac.Value = tb07.CO_ALU.ToString();

                txtLogra.Text = tb07.DE_ENDE_ALU;
                ddlUF.SelectedValue = tb07.CO_ESTA_ALU;
                txtCEP.Text = tb07.CO_CEP_ALU;
                tb07.TB905_BAIRROReference.Load();

                carregaCidade();
                ddlCidade.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_BAIRRO.ToString() : "";

                CarregaOperadoras();
                if (tb07.TB250_OPERA != null)
                    ddlOperadora.SelectedValue = tb07.TB250_OPERA.ID_OPER.ToString();

                CarregaPlanos();
                if (tb07.TB251_PLANO_OPERA != null)
                    ddlPlano.SelectedValue = tb07.TB251_PLANO_OPERA.ID_PLAN.ToString();

                CarregaCategoria();
                if (tb07.TB367_CATEG_PLANO_SAUDE != null)
                    ddlCategoria.SelectedValue = tb07.TB367_CATEG_PLANO_SAUDE.ID_CATEG_PLANO_SAUDE.ToString();

                txtNumeroPlano.Text = tb07.NU_PLANO_SAUDE;

                tb07.ImageReference.Load();
                upImageCadas.ImagemLargura = 70;
                upImageCadas.ImagemAltura = 85;

                if (tb07.Image != null)
                    upImageCadas.CarregaImagem(tb07.Image.ImageId);
                else
                    upImageCadas.CarregaImagem(0);

                tb07.TB108_RESPONSAVELReference.Load();

                if (tb07.TB108_RESPONSAVEL != null)
                {
                    if (tb07.NU_CPF_ALU == tb07.TB108_RESPONSAVEL.NU_CPF_RESP && tb07.NO_ALU == tb07.TB108_RESPONSAVEL.NO_RESP)
                        chkPaciEhResp.Checked = true;

                    PesquisaCarregaResp(tb07.TB108_RESPONSAVEL.CO_RESP);
                }

                txtObservacoes.Text = tb07.DES_OBSERVACAO;
                drpLocacao.SelectedValue = tb07.FL_LIST_ESP;

                #region Trata Informações de Saúde

                //Lista de informações gerais do paciente
                var lstInfosSaude = (from tbs383 in TBS383_INFOS_GERAIS_PACIENTE.RetornaTodosRegistros()
                                     where tbs383.CO_ALU == tbs383.CO_ALU
                                     select new
                                     {
                                         tbs383.TBS382_INFOS_GERAIS.ID_INFOS_GERAIS,
                                     }).ToList();

                //Percorre as informações de saúde associadas
                foreach (var i in lstInfosSaude)
                {
                    //Se houver na listagem, seleciona
                    ListItem ls = lstClassificacao.Items.FindByValue(i.ID_INFOS_GERAIS.ToString());
                    if (ls != null)
                        ls.Selected = true;
                }

                #endregion
            }
        }

        #region Cadastro Usuário de Saúde

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (!Page.IsValid)
            {
                return;
            }
            if (SalvarDados())
                if (!String.IsNullOrEmpty((string)this.Session[SessoesHttp.URLOrigem]))
                {
                    var url = Session[SessoesHttp.URLOrigem].ToString();
                    this.Session[SessoesHttp.URLOrigem] = "";
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Alterações realizadas com êxito!", url);
                }
                else
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Alterações realizadas com êxito!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        /// <summary>
        /// Carrega as informações da unidade logada em campos definidos
        /// </summary>
        private void CarregaDadosUnidLogada()
        {
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            txtCEP.Text = txtCEPResp.Text = "00000000"; //res.CO_CEP_EMP; Era assim, mas o cordova solicitou que carregasse 000 no lugar
            txtLogra.Text = txtLogradouroResp.Text = "XXX";  //res.CO_CEP_EMP; Era assim, mas o cordova solicitou que carregasse XXX no lugar
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select tb108).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP.ToUpper();
                txtNumContResp.Text = res.NU_CONTROLE;
                if (res.FL_CPF_VALIDO == "S")
                    txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;

                res.TBS366_FUNCAO_SIMPLReference.Load();
                if (res.TBS366_FUNCAO_SIMPL != null)
                    ddlFuncao.SelectedValue = res.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString();

                txtCEPResp.Text = res.CO_CEP_RESP;
                ddlUfRep.SelectedValue = res.CO_ESTA_RESP;
                carregaCidadeResp();
                ListItem itcidade = ddlCidadeResp.Items.FindByValue(res.CO_CIDADE.ToString());
                if (itcidade != null)
                    ddlCidadeResp.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";

                carregaBairroResp();
                ListItem itBairro = ddlBairroResp.Items.FindByValue(res.CO_BAIRRO.ToString());
                if (itBairro != null)
                    ddlBairroResp.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";

                txtLogra.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();

                txtLogradouroResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;

            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
            carregaBairro();
        }

        private void carregaCidadeResp()
        {
            string uf = ddlUfRep.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidadeResp, false, uf, LoginAuxili.CO_EMP, true, true);
            carregaBairroResp();
        }

        private void carregaBairroResp()
        {
            string uf = ddlUfRep.SelectedValue;
            int cid = ddlCidadeResp.SelectedValue != "" ? int.Parse(ddlCidadeResp.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaBairros(ddlBairroResp, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as funções simplificadas
        /// </summary>
        private void CarregarFuncoesSimp()
        {
            AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlFuncao, false);
        }

        /// <summary>
        /// Carrega a grid de profissionais responsáveis
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaProfissionaisResponsaveis(int CO_ALU)
        {
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       where tbs174.CO_ALU == CO_ALU
                       && tbs174.CO_SITUA_AGEND_HORAR == "A"
                       select new saidaProfissionais
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL = tb03.CO_SITU_COL == "INA" ? "* " + tb03.NO_COL : tb03.NO_COL,
                           NU_TELE_CELU_COL = tb03.NU_TELE_CELU_COL,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           CO_ALU = tbs174.CO_ALU.Value,
                       }).DistinctBy(w => w.CO_COL).OrderBy(w => w.NO_COL).ToList();

            grdProfiResponsaveis.DataSource = res;
            grdProfiResponsaveis.DataBind();
        }

        public class saidaProfissionais
        {
            public int CO_ALU { get; set; }
            public int CO_COL { get; set; }
            public DateTime DATA { get; set; }
            public string NO_COL { get; set; }
            public string NU_TELE_CELU_COL { get; set; }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_PROFI);
                }
            }
            public string DATAULTIMO
            {
                get
                {

                    var tbs174 = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.CO_ALU == CO_ALU && a.CO_SITUA_AGEND_HORAR != "C" && a.CO_COL == this.CO_COL && a.DT_AGEND_HORAR < DateTime.Now).OrderByDescending(a => a.DT_AGEND_HORAR).FirstOrDefault();
                    if (tbs174 != null)
                    {
                        this.DATA = tbs174.DT_AGEND_HORAR;
                        return tbs174.DT_AGEND_HORAR.ToShortDateString();
                    }

                    return "";

                }

            }
            public string DATAPROC
            {
                get
                {
                    var tbs174 = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.CO_ALU == CO_ALU && a.CO_SITUA_AGEND_HORAR == "A" && a.CO_COL == this.CO_COL && a.DT_AGEND_HORAR >= DateTime.Now).OrderBy(a => a.DT_AGEND_HORAR).FirstOrDefault();
                    if (tbs174 != null)
                        return tbs174.DT_AGEND_HORAR.ToShortDateString();
                    return "";
                }
            }
        }

        /// <summary>
        /// Verifica o nire
        /// </summary>
        private void VerificarNireAutomatico()
        {
            string strTipoNireAuto = "";
            //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
            var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                        where iTb25.CO_EMP == LoginAuxili.CO_EMP
                        select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

            strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";

            if (strTipoNireAuto != "")
            {
                //----------------> Faz a verificação para saber se o NIRE é automático ou não
                if (strTipoNireAuto == "N")
                {
                }
                else
                {
                    ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                    int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                    if (lastCoAlu != null)
                    {
                        txtNirePac.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNirePac.Text = "1";
                }
            }
        }

        /// <summary>
        /// Método responsável por executar os processos de persistência de informações
        /// </summary>
        private bool SalvarDados()
        {
            var cpfValido = true;
            //Tenta validar as informações preenchidas
            try
            {
                #region Valida campos do Paciente

                //if (string.IsNullOrEmpty(txtNuNisPaci.Text))
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "O Nis do Paciente é obrigatório!");
                //    txtNuNisPaci.Focus();
                //    //updCadasUsuario.Update();
                //    return false;
                //}

                if (string.IsNullOrEmpty(txtnompac.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Paciente é obrigatório!");
                    txtnompac.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtApelidoPaciente.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Apelido do Paciente é obrigatório!");
                    txtApelidoPaciente.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ddlSexoPaci.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é obrigatório!");
                    ddlSexoPaci.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtDtNascPaci.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é obrigatória!");
                    txtDtNascPaci.Focus();
                    return false;
                }

                var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                if (!String.IsNullOrEmpty(txtCPFMOD.Text))
                {
                    if (!AuxiliValidacao.ValidaCpf(txtCPFMOD.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do paciente invalido!");
                        txtCPFMOD.Focus();
                        return false;
                    }
                }
                else if (tb25.FL_CPF_PAC_OBRIGATORIO == "S")
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente é obrigatório!");
                    txtCPFMOD.Focus();
                    return false;
                }

                #region Verifica o NIRE

                //Apenas verifica novo nire, se for inclusão de paciente
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string strTipoNireAuto = "";

                    tb25.TB83_PARAMETROReference.Load();
                    if (tb25.TB83_PARAMETRO != null)
                        strTipoNireAuto = tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO != null ? tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO : "";

                    if (strTipoNireAuto != "")
                    {
                        //----------------> Faz a verificação para saber se o NIRE é automático ou não
                        if (strTipoNireAuto == "N")
                        {
                            if (txtNirePac.Text.Replace(".", "").Replace("-", "") == "")
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Nº CONTROLE deve ser informado.");
                                return false;
                            }

                            int nuNire = int.Parse(txtNirePac.Text.Replace(".", "").Replace("-", ""));

                            ///-------------------> Faz a verificação para saber se o NIRE informado já existe
                            var ocorrNIRE = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                             where lTb07.NU_NIRE == nuNire
                                             select new { lTb07.CO_ALU, lTb07.NO_ALU }).FirstOrDefault();


                            if (ocorrNIRE != null)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Nº CONTROLE informado já existe para o(a) Paciente(a) " + ocorrNIRE.NO_ALU + ".");
                                return false;
                            }
                        }
                        else
                        {
                            ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                            int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                              select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                            if (lastCoAlu != null)
                            {
                                txtNirePac.Text = (lastCoAlu.Value + 1).ToString();
                            }
                            else
                                txtNirePac.Text = "1";
                        }
                    }
                }

                #endregion

                string cpfPac = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();
                decimal? nis = (decimal?)null;

                if (string.IsNullOrEmpty(txtNirePac.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Nº CONTROLE precisa ser informado!");
                    return false;
                }

                //Apenas realiza as verificações abaixo quando for inclusão de novo registro 
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
                    {
                        nis = decimal.Parse(txtNuNisPaci.Text);

                        //Verifica se já existe algum paciente com o CPF informado
                        if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfPac).Any() && (!string.IsNullOrEmpty(cpfPac)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o CPF informado!");
                            return false;
                        }
                        //Verifica se já existe algum paciente com o CNES/SUS/NIS informado
                        else if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIS == nis).Any() && (!string.IsNullOrEmpty(txtNuNisPaci.Text)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o SUS/CNES informado!");
                            return false;
                        }
                    }

                    //Verifica se já existe algum paciente com o CPF informado
                    if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfPac).Any() && (!string.IsNullOrEmpty(cpfPac)))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o CPF informado!");
                        return false;
                    }

                    int nire = int.Parse(txtNirePac.Text);
                    //Verifica se já existe algum paciente com o NIRE/Nº CONTROLE informado
                    if (TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nire).Any())
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um paciente cadastrado com o Nº CONTROLE informado!");
                        return false;
                    }
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    DateTime dataNasctoAlu = DateTime.Parse(txtDtNascPaci.Text);

                    ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( quando inclusão )
                    var ocorrAlu = from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where lTb07.NO_ALU == txtnompac.Text && lTb07.CO_SEXO_ALU == ddlSexoPaci.SelectedValue && lTb07.DT_NASC_ALU == dataNasctoAlu
                                   select new { lTb07.CO_ALU };

                    if (ocorrAlu.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Paciente já cadastrado no sistema.");
                        return false;
                    }
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                    DateTime dataNasctoAlu = DateTime.Parse(txtDtNascPaci.Text);

                    ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( diferente do aluno informado na alteração )
                    var ocorrAlu = from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where lTb07.NO_ALU == txtnompac.Text && lTb07.CO_SEXO_ALU == ddlSexoPaci.SelectedValue
                                   && lTb07.DT_NASC_ALU == dataNasctoAlu && lTb07.CO_ALU != coAlu
                                   select new { lTb07.CO_ALU };

                    if (ocorrAlu.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Paciente já cadastrado no sistema.");
                        return false;
                    }
                }

                #endregion

                #region Valida informações do Responsável

                if (string.IsNullOrEmpty(txtNomeResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do responsável é obrigatório!");
                    txtNomeResp.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ddlSexResp.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do responsável é obrigatório!");
                    ddlSexResp.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtDtNascResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do responsável é obrigatória!");
                    txtDtNascResp.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtCEPResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do responsável é obrigatório!");
                    txtCEPResp.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ddlUfRep.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A UF do endereço do responsável é obrigatória!");
                    ddlUfRep.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ddlCidadeResp.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A UF do endereço do responsável é obrigatória!");
                    ddlCidadeResp.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ddlBairroResp.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro do endereço do responsável é obrigatório!");
                    ddlBairroResp.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtLogradouroResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro do endereço do responsável é obrigatório!");
                    txtLogradouroResp.Focus();
                    return false;
                }

                if (!String.IsNullOrEmpty(txtCPFResp.Text))
                {
                    if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
                    {
                        AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                        txtCPFResp.Focus();
                        return false;
                    }
                }
                else if (tb25.FL_CPF_RESP_OBRIGATORIO == "S")
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do responsável é obrigatório!");
                    txtCPFResp.Focus();
                    return false;
                }
                else if (tb25.FL_CPF_RESP_OBRIGATORIO == "N" && String.IsNullOrEmpty(txtCPFResp.Text))
                {
                    if (string.IsNullOrEmpty(hidCoResp.Value))
                    {
                        var cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                        //Enquanto existir, calcula um novo cpf
                        while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado || w.NU_CONTROLE == cpfGerado).Any())
                            cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                        txtCPFResp.Text = cpfGerado;
                    }
                    cpfValido = false;
                }

                #endregion
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao validar as informações preenchidas, favor contactar o suporte! Erro: " + e.Message);
                return false;
            }

            //using (TransactionScope scope = new TransactionScope())
            //{
            //Tenta validar a inclusão/alteração dos dados
            try
            {
                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                TB108_RESPONSAVEL tb108;
                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                var cpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");
                var resp = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NU_CPF_RESP == cpfResp).FirstOrDefault();

                if (resp != null && !String.IsNullOrEmpty(cpfResp) && string.IsNullOrEmpty(hidCoResp.Value))
                    tb108 = resp;
                else if (!string.IsNullOrEmpty(hidCoResp.Value))
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value));
                else
                    tb108 = new TB108_RESPONSAVEL();

                if (tb108.NU_CONTROLE != cpfResp && (cpfValido || (!cpfValido && String.IsNullOrEmpty(tb108.NU_CONTROLE))))
                    tb108.NU_CONTROLE = cpfResp;

                string NomeApeliResp = "";
                #region Apelido dinâmico do responsável

                if (!string.IsNullOrEmpty(txtNomeResp.Text))
                {
                    var nomeResp = txtNomeResp.Text.Split(' ');
                    NomeApeliResp = nomeResp[0] + (nomeResp.Length > 1 ? " " + nomeResp[1] : "");
                }

                #endregion
                tb108.NO_APELIDO_RESP = NomeApeliResp.ToUpper();
                tb108.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                tb108.NO_RESP = txtNomeResp.Text.ToUpper();
                tb108.FL_CPF_VALIDO = cpfValido ? "S" : "N";
                tb108.NU_CPF_RESP = cpfResp;
                tb108.CO_RG_RESP = txtNuIDResp.Text;
                tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                tb108.CO_CEP_RESP = txtCEPResp.Text.Replace("-", "");
                tb108.CO_ESTA_RESP = ddlUfRep.SelectedValue;
                tb108.CO_CIDADE = int.Parse(ddlCidadeResp.SelectedValue);
                tb108.CO_BAIRRO = int.Parse(ddlBairroResp.SelectedValue);
                tb108.DE_ENDE_RESP = txtLogradouroResp.Text;
                tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.CO_ORIGEM_RESP = "NN";
                tb108.CO_SITU_RESP = "A";
                tb108.DT_SITU_RESP = DateTime.Now;
                tb108.NM_FACEBOOK_RESP = txtDeFaceResp.Text;
                //Atribui valores vazios para os campos not null da tabela de Responsável.
                tb108.FL_NEGAT_CHEQUE = "V";
                tb108.FL_NEGAT_SERASA = "V";
                tb108.FL_NEGAT_SPC = "V";
                tb108.CO_INST = 0;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07
                TB07_ALUNO tb07 = RetornaEntidade();

                string cpfPac = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();

                if (tb07.CO_ALU == 0)
                {
                    var pac = TB07_ALUNO.RetornaTodosRegistros().Where(a => a.NU_CPF_ALU == cpfPac).FirstOrDefault();

                    if (pac != null && !String.IsNullOrEmpty(cpfPac) && string.IsNullOrEmpty(hidCoPac.Value))
                        tb07 = pac;
                    else if (!string.IsNullOrEmpty(hidCoPac.Value))
                        tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value));
                }

                #region Bloco foto
                int codImagem = upImageCadas.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion

                tb07.TBS387_DEFIC = (!string.IsNullOrEmpty(ddlDeficienciaAlu.SelectedValue) ? TBS387_DEFIC.RetornaPelaChavePrimaria(int.Parse(ddlDeficienciaAlu.SelectedValue)) : null);
                tb07.NU_NIRE = int.Parse(txtNirePac.Text);
                tb07.NU_NIS = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
                tb07.NU_CPF_ALU = cpfPac;
                //tb07.CO_ANO_ORI = (!string.IsNullOrEmpty(txtAno.Text) ? txtAno.Text : null);
                tb07.NO_ALU = txtnompac.Text.ToUpper();
                tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;

                if (!String.IsNullOrEmpty(ddlIndicacao.SelectedValue) && (!tb07.CO_COL_INDICACAO.HasValue || (tb07.CO_COL_INDICACAO.HasValue && tb07.CO_COL_INDICACAO != int.Parse(ddlIndicacao.SelectedValue))))
                {
                    tb07.CO_EMP_INDICACAO = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlIndicacao.SelectedValue)).CO_EMP;
                    tb07.CO_COL_INDICACAO = int.Parse(ddlIndicacao.SelectedValue);
                    tb07.DT_INDICACAO = DateTime.Now;
                }
                else if (tb07.CO_COL_INDICACAO.HasValue && String.IsNullOrEmpty(ddlIndicacao.SelectedValue))
                {
                    tb07.CO_EMP_INDICACAO = null;
                    tb07.CO_COL_INDICACAO = null;
                    tb07.DT_INDICACAO = null;
                }

                if (tb07.CO_SITU_ALU != ddlSituacaoAlu.SelectedValue || !tb07.DT_SITU_ALU.HasValue)
                    tb07.DT_SITU_ALU = DateTime.Now;
                tb07.CO_SITU_ALU = (!string.IsNullOrEmpty(ddlSituacaoAlu.SelectedValue) ? ddlSituacaoAlu.SelectedValue : null);

                tb07.DT_VENC_PLAN = (!string.IsNullOrEmpty(txtDtVencPlan.Text) ? DateTime.Parse(txtDtVencPlan.Text) : (DateTime?)null);
                tb07.NO_APE_ALU = txtApelidoPaciente.Text.ToUpper();
                tb07.CO_ORIGEM_ALU = ddlOrigemPaci.SelectedValue;
                tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                tb07.NO_FACEB_PACI = (!string.IsNullOrEmpty(txtFacebookPac.Text) ? txtFacebookPac.Text : null);
                //tb07.CO_MES_REFER = Convert.ToInt32(txtMesOrg.Text);
                tb07.NU_ALTU = (!string.IsNullOrEmpty(txtAltura.Text) ? decimal.Parse(txtAltura.Text) : (decimal?)null);
                tb07.NU_PESO = (!string.IsNullOrEmpty(txtPeso.Text) ? decimal.Parse(txtPeso.Text) : (decimal?)null);
                tb07.TBS379_RESTR_ATEND = (!string.IsNullOrEmpty(ddlRestricaoAtendimento.SelectedValue) ? TBS379_RESTR_ATEND.RetornaPelaChavePrimaria(int.Parse(ddlRestricaoAtendimento.SelectedValue)) : null);
                tb07.DT_RESTR_ATEND = (!string.IsNullOrEmpty(txtDataRestricaoAtendimento.Text) ? DateTime.Parse(txtDataRestricaoAtendimento.Text) : (DateTime?)null);

                tb07.CO_ORIGEM_ALU = ddlOrigemPaci.SelectedValue;
                tb07.DE_NATU_ALU = txtNaturalidade.Text;
                tb07.CO_ESTADO_CIVIL = ddlEstadoCivil.SelectedValue;
                tb07.TP_RACA = ddlEtniaAlu.SelectedValue != "" ? ddlEtniaAlu.SelectedValue : null;

                tb07.DE_NATU_ALU = (!string.IsNullOrEmpty(txtNaturalidade.Text) ? txtNaturalidade.Text : null);
                tb07.CO_ESTADO_CIVIL = (!string.IsNullOrEmpty(ddlEstadoCivil.SelectedValue) ? ddlEstadoCivil.SelectedValue : null);
                //tb07.TBS377_INDIC_PACIENTES = (!string.IsNullOrEmpty(ddlIndicacao.SelectedValue) ? TBS377_INDIC_PACIENTES.RetornaPelaChavePrimaria(int.Parse(ddlIndicacao.SelectedValue)) : null);

                tb07.NO_PAI_ALU = (!string.IsNullOrEmpty(txtNomePai.Text) ? txtNomePai.Text.ToUpper() : null);
                tb07.FLA_OBITO_PAI = ddlObitoPai.SelectedValue;
                tb07.TBS366_FUNCAO_SIMPL1 = (!string.IsNullOrEmpty(ddlProfissaoNomePai.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlProfissaoNomePai.SelectedValue)) : null);
                tb07.FL_PAI_RESP_FINAN = (chkPaiRespFinanc.Checked ? "S" : "N");
                tb07.FL_PAI_RESP_ATEND = (chkPaiResp.Checked ? "S" : "N");
                tb07.NO_MAE_ALU = (!string.IsNullOrEmpty(txtNomeMae.Text) ? txtNomeMae.Text.ToUpper() : null);
                tb07.FLA_OBITO_MAE = ddlObitoMae.SelectedValue;
                tb07.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlProfissaoNomeMae.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlProfissaoNomeMae.SelectedValue)) : null);
                tb07.FL_MAE_RESP_FINAN = (chkMaeRespFinanc.Checked ? "S" : "N");
                tb07.FL_MAE_RESP_ATEND = (chkMaeResp.Checked ? "S" : "N");
                tb07.NU_TEL_MAE = (!string.IsNullOrEmpty(txtTelMae.Text) ? txtTelMae.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim() : null);
                tb07.NU_TEL_PAI = (!string.IsNullOrEmpty(txtTelPai.Text) ? txtTelPai.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim() : null);
                tb07.DT_ENTRA_INSTI = (!string.IsNullOrEmpty(txtDataEntrada.Text) ? DateTime.Parse(txtDataEntrada.Text) : (DateTime?)null);
                tb07.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue)) : null);
                tb07.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlano.SelectedValue)) : null);
                tb07.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCategoria.SelectedValue) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCategoria.SelectedValue)) : null);
                tb07.NU_PLANO_SAUDE = (!string.IsNullOrEmpty(txtNumeroPlano.Text) ? txtNumeroPlano.Text : null);
                tb07.CO_RG_ALU = (!string.IsNullOrEmpty(txtNumeroIdentidade.Text) ? txtNumeroIdentidade.Text : null);
                tb07.CO_ORG_RG_ALU = (!string.IsNullOrEmpty(txtOrgao.Text) ? txtOrgao.Text : null);
                tb07.CO_ESTA_RG_ALU = (!string.IsNullOrEmpty(ddlUfOrgPac.SelectedValue) ? ddlUfOrgPac.SelectedValue : null);
                tb07.DT_EMIS_RG_ALU = (!string.IsNullOrEmpty(txtDataEmissao.Text) ? DateTime.Parse(txtDataEmissao.Text) : (DateTime?)null);
                //tb07.CO_UF_NATU_ALU = ddlUfOrgPac.SelectedValue;

                tb07.CO_CEP_ALU = txtCEP.Text.Replace("-", "");
                tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                tb07.TB905_BAIRRO = !string.IsNullOrEmpty(ddlBairro.SelectedValue) ? TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue)) : TB905_BAIRRO.RetornaPelaChavePrimaria(0);
                tb07.DE_ENDE_ALU = txtLogra.Text;
                tb07.NO_WEB_ALU = txtEmailPaci.Text;

                if ((tb07.CO_ALU == 0) || (tb07.CO_ALU == (int?)null))
                {
                    tb07.CO_INST = LoginAuxili.ORG_CODIGO_ORGAO;
                    tb07.DT_CADA_ALU = DateTime.Now;
                    tb07.CO_EMP = LoginAuxili.CO_EMP;
                    tb07.CO_EMP_ORIGEM = LoginAuxili.CO_EMP;    //TA.14/07/2016
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                }

                tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);

                //if (chkPaciMoraCoResp.Checked)
                //{
                //    txtCPFResp.Text = txtCPFMOD.Text;
                //    tb07.CO_CEP_ALU = txtCEP.Text;
                //    tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                //    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                //    tb07.DE_ENDE_ALU = txtLogra.Text;
                //}

                #region trata para criação do nire

                var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                           select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                int nir = 0;
                if (res == null)
                {
                    nir = 1;
                }
                else
                {
                    nir = res.NU_NIRE;
                }

                int nirTot = nir + 1;

                #endregion
                //tb07.NU_NIRE = nirTot;


                tb07.FL_PENDE_DOCUM = chkDocumentos.Checked == true ? "S" : "N";
                tb07.FL_PENDE_FINAN_GER = chkFinanceiroGer.Checked == true ? "S" : "N";
                tb07.FL_PENDE_PLANO_CONVE = chkPlanoDeSaude.Checked == true ? "S" : "N";
                tb07.TP_DEF = "N";
                tb07.DES_OBSERVACAO = txtObservacoes.Text;
                tb07.FL_LIST_ESP = drpLocacao.SelectedValue;
                tb07.DE_PASTA_CONTR = !string.IsNullOrEmpty(txtPastaControl.Text) ? tb07.NU_NIRE.ToString() : txtPastaControl.Text;
                tb07.TEL_MIGRAR = txtTelMigrado.Text;
                tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
                #endregion

                #region Associa as informações de saúde selecionadas

                var lstAssoc = TBS383_INFOS_GERAIS_PACIENTE.RetornaPeloCoAu(tb07.CO_ALU).ToList();

                //Exclui todas as associações das informações de saúde
                foreach (var i in lstAssoc)
                    TBS383_INFOS_GERAIS_PACIENTE.Delete(i, true);

                //Percorre as informações de saúde
                foreach (ListItem i in lstClassificacao.Items)
                {
                    if (i.Selected) //Se estiver selecionado
                    {
                        //Salva nova associação
                        var tbs383 = new TBS383_INFOS_GERAIS_PACIENTE();
                        tbs383.TBS382_INFOS_GERAIS = TBS382_INFOS_GERAIS.RetornaPelaChavePrimaria(int.Parse(i.Value));
                        tbs383.CO_ALU = tb07.CO_ALU;

                        //Dados Cadastrais
                        tbs383.DT_CADAS = DateTime.Now;
                        tbs383.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs383.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs383.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs383.IP_CADAS = Request.UserHostAddress;
                        TBS383_INFOS_GERAIS_PACIENTE.SaveOrUpdate(tbs383, true);
                    }
                }

                #endregion
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao salvar as informações preenchidas, favor contactar o suporte! Erro: " + e.Message);
                return false;
            }

            //scope.Complete();
            return true;
            //}
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaBairros(ddlBairro, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                txtCPFResp.Text = txtCPFMOD.Text;
                txtNomeResp.Text = txtnompac.Text;
                txtDtNascResp.Text = txtDtNascPaci.Text;
                ddlSexResp.SelectedValue = ddlSexoPaci.SelectedValue;
                txtTelCelResp.Text = txtTelCelPaci.Text;
                txtTelFixResp.Text = txtTelResPaci.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailResp.Text = txtEmailPaci.Text;
                txtNuWhatsResp.Text = txtWhatsPaci.Text;

                //txtEmailPaci.Enabled = false;
                //txtCPFMOD.Enabled = false;
                //txtnompac.Enabled = false;
                //txtDtNascPaci.Enabled = false;
                //ddlSexoPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelResPaci.Enabled = false;
                //ddlGrParen.Enabled = false;
                //txtWhatsPaci.Enabled = false;
            }
            else
            {
                txtCPFResp.Text = "";
                txtNomeResp.Text = "";
                txtDtNascResp.Text = "01/01/1950";
                ddlSexResp.SelectedValue = "";
                txtTelCelResp.Text = "";
                txtTelFixResp.Text = "";
                txtEmailResp.Text = "";
                txtNuWhatsResp.Text = "";

                //txtCPFMOD.Enabled = true;
                //txtnompac.Enabled = true;
                //txtDtNascPaci.Enabled = true;
                //ddlSexoPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelResPaci.Enabled = true;
                //ddlGrParen.Enabled = true;
                //txtEmailPaci.Enabled = true;
                //txtWhatsPaci.Enabled = true;
                //hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Carrega as informações de saúde
        /// </summary>
        private void CarregaInformacoesSaude()
        {
            AuxiliCarregamentos.CarregaInformacoesSaude(lstClassificacao);
        }

        /// <summary>
        /// Carrega os tipos de deficiência do aluno
        /// </summary>
        private void CarregaDeficiencia()
        {
            AuxiliCarregamentos.CarregaDeficienciasNova(ddlDeficienciaAlu, false);
        }

        /// <summary>
        /// Carrega as funções simplificadas pai
        /// </summary>
        private void CarregarFuncoesSimpPai()
        {

            AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlProfissaoNomePai, false);
        }

        /// <summary>
        /// Carrega as funções simplificadas Mae
        /// </summary>
        private void CarregarFuncoesSimpMae()
        {
            AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlProfissaoNomeMae, false);
        }
        /// <summary>
        /// Carrega Indicacao
        /// </summary>
        private void CarregarIndicacao()
        {
            //AuxiliCarregamentos.CarregaIndicadores(ddlIndicacao, false);
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlIndicacao, LoginAuxili.CO_EMP, false, "0", true, 0, false);
        }

        protected void imgPesqCEP_OnClick(object sender, EventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogra.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    carregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogra.Text = "";
                    ddlBairro.SelectedValue = "";
                    ddlCidade.SelectedValue = "";
                    ddlUF.SelectedValue = "";
                }
            }
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            chkPaciMoraCoResp.Checked = chkPaciEhResp.Checked;
        }

        protected void chkPaciMoraCoResp_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPaciMoraCoResp.Checked)
            {
                txtLogradouroResp.Text = txtLogra.Text;
                ddlUfRep.SelectedValue = ddlUF.SelectedValue;

                carregaCidadeResp();
                ddlCidadeResp.SelectedValue = ddlCidade.SelectedValue;
                carregaBairroResp();
                ddlBairroResp.SelectedValue = ddlBairro.SelectedValue;
                txtCEPResp.Text = txtCEP.Text;
            }
            else
            {
                AuxiliCarregamentos.CarregaUFs(ddlUfRep, false);
                carregaCidadeResp();
                carregaBairroResp();
                txtLogradouroResp.Text = txtCEPResp.Text = "";
            }
        }

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            //updCadasUsuario.Update();
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
        }

        protected void txtCPFResp_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCPFResp.Text))
            {
                string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim();
                var resp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                            where tb108.NU_CPF_RESP == cpfResp
                            select new { tb108.CO_RESP }).FirstOrDefault();

                if (resp != null)
                {
                    PesquisaCarregaResp(resp.CO_RESP);
                }
            }
        }

        protected void ddlCidadeResp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairroResp();
            ddlBairroResp.Focus();
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega Operadoras
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false);
        }

        /// <summary>
        /// Carrega Planos
        /// </summary>
        private void CarregaPlanos()
        {
            string idOperadora = ddlOperadora.SelectedValue;
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, idOperadora, false);
        }

        /// <summary>
        /// Carrega Categoria
        /// </summary>
        private void CarregaCategoria()
        {
            AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoria, ddlPlano, false);
        }

        /// <summary>
        /// Carrega Restrição De Atendimento
        /// </summary>
        private void CarregaRestricaoDeAtendimento()
        {
            AuxiliCarregamentos.CarregaRestricoesAtendimento(ddlRestricaoAtendimento, false);
        }

        /// <summary>
        /// Carrega Restrição De Atendimento
        /// </summary>
        private void CarregaUf()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUF, false);
            AuxiliCarregamentos.CarregaUFs(ddlUfRep, false);
        }

        #endregion

        /// <summary>
        /// Retorna a entidade e contexto, quando houver
        /// </summary>
        /// <returns></returns>
        /// 
        private TB07_ALUNO RetornaEntidade()
        {
            TB07_ALUNO tb07 = new TB07_ALUNO();
            if (!String.IsNullOrEmpty((string)this.Session[SessoesHttp.URLOrigem]) && !String.IsNullOrEmpty((string)this.Session[SessoesHttp.CodigoMatriculaAluno]))
            {
                tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse((string)this.Session[SessoesHttp.CodigoMatriculaAluno]));
                this.Session[SessoesHttp.CodigoMatriculaAluno] = "";
            }
            else
                tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        #region Funções de Campo

        protected void ddlOperadora_CheckedChanged(object sender, EventArgs e)
        {
            CarregaPlanos();
        }

        protected void ddlPlano_CheckedChanged(object sender, EventArgs e)
        {
            CarregaCategoria();
        }

        protected void ddlUFResp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidadeResp();
            ddlCidadeResp.Focus();
        }

        protected void ddlRestricaoAtendimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlRestricaoAtendimento.SelectedValue))
                txtDataRestricaoAtendimento.Text = DateTime.Now.ToString();
            else
                txtDataRestricaoAtendimento.Text = "";
        }

        protected void ddlObitoMae_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Se tiver alguma responsabilidade marcada, avisa 
            if (ddlObitoMae.SelectedValue == "S")
            {
                if (chkMaeResp.Checked)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor desmarcar que a mãe é a Responsável pelo Acompanhamento para poder alterar para óbito");
                    chkMaeResp.Focus();
                    ddlObitoMae.SelectedValue = "N";
                    return;
                }

                if (chkMaeRespFinanc.Checked)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor desmarcar que a mãe é a Responsável Financeira para poder alterar para óbito");
                    chkMaeRespFinanc.Focus();
                    ddlObitoMae.SelectedValue = "N";
                    return;
                }
            }
            chkMaeResp.Enabled = chkMaeRespFinanc.Enabled = (ddlObitoMae.SelectedValue == "S" ? false : true);
        }

        protected void ddlObitoPai_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Se tiver alguma responsabilidade marcada, avisa 
            if (ddlObitoPai.SelectedValue == "S")
            {
                if (chkPaiResp.Checked)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor desmarcar que o Pai é o Responsável pelo Acompanhamento para poder alterar para óbito");
                    chkPaiResp.Focus();
                    ddlObitoPai.SelectedValue = "N";
                    return;
                }

                if (chkMaeRespFinanc.Checked)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor desmarcar que o Pai é o Responsável Financeira para poder alterar para óbito");
                    chkMaeRespFinanc.Focus();
                    ddlObitoPai.SelectedValue = "N";
                    return;
                }
            }
            chkPaiResp.Enabled = chkPaiRespFinanc.Enabled = (ddlObitoPai.SelectedValue == "S" ? false : true);
        }

        protected void chkMaeRespFinanc_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkPaiRespFinanc.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Só pode haver um responsável financeiro");
                chkMaeRespFinanc.Checked = false;
                chkPaiRespFinanc.Focus();
                return;
            }

            //Se clicou para marcar, carrega as informações já inseridas
            if (chkMaeRespFinanc.Checked)
            {
                txtNomeResp.Text = txtNomeMae.Text;
                ddlFuncao.SelectedValue = ddlProfissaoNomeMae.SelectedValue;
                txtTelCelResp.Text = txtTelMae.Text;

                txtNomeResp.Enabled = ddlFuncao.Enabled = txtTelCelResp.Enabled = chkPaciEhResp.Enabled = false;
            }
            else // Se não, limpa.
            {
                txtNomeResp.Text = ddlFuncao.SelectedValue = txtTelCelResp.Text = "";
                txtNomeResp.Enabled = ddlFuncao.Enabled = txtTelCelResp.Enabled = chkPaciEhResp.Enabled = true;
            }
        }

        protected void chkPaiRespFinanc_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkMaeRespFinanc.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Só pode haver um responsável financeiro");
                chkPaiRespFinanc.Checked = false;
                chkMaeRespFinanc.Focus();
                return;
            }

            //Se clicou para marcar, carrega as informações já inseridas
            if (chkPaiRespFinanc.Checked)
            {
                txtNomeResp.Text = txtNomePai.Text;
                ddlFuncao.SelectedValue = ddlProfissaoNomePai.SelectedValue;
                txtTelCelResp.Text = txtTelPai.Text;

                txtNomeResp.Enabled = ddlFuncao.Enabled = txtTelCelResp.Enabled = chkPaciEhResp.Enabled = false;
            }
            else // Se não, limpa.
            {
                txtNomeResp.Text = ddlFuncao.SelectedValue = txtTelCelResp.Text = "";
                txtNomeResp.Enabled = ddlFuncao.Enabled = txtTelCelResp.Enabled = chkPaciEhResp.Enabled = true;
            }
        }

        #endregion
    }
}