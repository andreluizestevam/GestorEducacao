//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE USUÁRIOS DO SISTEMA.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 01/03/2013| Victor Martins Machado     | Foi criado um campo chamad chkAltBolsaAlu
//           |                            | que indica se o usuário tem permissão ou não
//           |                            | de alterar o desconto dado ao aluno na
//           |                            | matrícula. Este novo campo é inserido
//           |                            | em uma nova coluna da tabela ADMUSUARIO
//           |                            | chamada FLA_ALT_BOL_ALU.
// ----------+----------------------------+-------------------------------------
// 02/04/2013| André Nobre Vinagre        | Foram criados os campos:
//           |                            | FLA_ALT_BOL_ESPE_ALU => Flag de alteração para bolsa especial
//           |                            | FLA_ALT_REG_PAG_MAT => Flag de alteração de registro de pagamentos de matrícula
//           |                            | FLA_ALT_PARAM_MAT => Flag de alteração de parâmetros da matrícula
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 03/04/2013| André Nobre Vinagre        | Consertado o layout.
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Sql;
using C2BR.GestorEducacao.UI;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0301_ManutencaoUsuarioSistema
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

                ///Define altura e largura da imagem do funcionário
                upImagemColab.ImagemLargura = 90;
                upImagemColab.ImagemAltura = 122;

                //upImagemColab.Visible = false;
                upImagemColab.ImagemLargura = 86;
                upImagemColab.ImagemAltura = 105;
                upImagemColab.MostraProcurar = false;
                txtInstituicao.Text = LoginAuxili.ORG_NOME_ORGAO;
                CarregaUnidades();
                CarregaColaborador();
                txtDtSituacaoMUS.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (tb149.TP_CTRLE_MENSA_SMS == "I")
                {
                    txtQtdSMSMes.Enabled = tb149.FL_ENVIO_SMS == "S";
                }
                else
                    txtQtdSMSMes.Enabled = true;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    ddlUnidadeMUS.Enabled = ddlColMUS.Enabled = false;

                CarregaCamposUnidEscol();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario()
        {
            CarregaFormulario(Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol)));
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if ((txtQtdSMSMes.Enabled) && (txtQtdSMSMes.Text != ""))
            {
                var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (ddlTpUsuMUS.SelectedValue == "P")
                {
                    if (tb149.QT_MAX_SMS_PROFE != null)
                    {
                        if (int.Parse(txtQtdSMSMes.Text) > tb149.QT_MAX_SMS_PROFE)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Quantidade máxima de SMS mês não pode ser maior que " + tb149.QT_MAX_SMS_PROFE.ToString());
                            return;
                        }
                    }
                }
                else
                {
                    if (tb149.QT_MAX_SMS_FUNCI != null)
                    {
                        if (int.Parse(txtQtdSMSMes.Text) > tb149.QT_MAX_SMS_FUNCI)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Quantidade máxima de SMS mês não pode ser maior que " + tb149.QT_MAX_SMS_FUNCI.ToString());
                            return;
                        }
                    }
                }
            }

            int coCol = int.Parse(ddlColMUS.SelectedValue);

            //--------> Faz a verificaçao se o Login informado tem registro na base de dados
            int ocoLogin = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                            where admUsuario.desLogin == txtLoginMUS.Text.TrimStart().TrimEnd()
                            select new { admUsuario.CodUsuario }).Count();

            //--------> Faz a verificaçao se o Login informado existe e se está associado ao usuário informado
            int ocoAssocUsu = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                               where admUsuario.CodUsuario == coCol
                               && admUsuario.desLogin == txtLoginMUS.Text.TrimStart().TrimEnd()
                               select new { admUsuario.CodUsuario }).Count();

            //--------> Faz a condiçao para saber se usuário informado existe, se tudo OK, continua 
            if (ocoLogin > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) && ocoAssocUsu == 0)
                AuxiliPagina.EnvioMensagemErro(this, MensagensErro.LoginJaCadastrado);
            else
            {
                ADMUSUARIO admUsuario;
                int coEmp = int.Parse(ddlUnidadeMUS.SelectedValue);
                var tpUsu = ddlTpUsuMUS.SelectedValue;
                var strSenhaMD5 = "";
                var CO_EMP_USU = 0;
                var NU_CPF_USU = "";

                if (tpUsu == "A")
                {
                    var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coCol);
                    admUsuario = ADMUSUARIO.RetornaPeloCodUsuario(coCol);
                    strSenhaMD5 = LoginAuxili.GerarMD5(tb07.NU_CPF_ALU);
                    CO_EMP_USU = LoginAuxili.CO_EMP;
                    NU_CPF_USU = tb07.NU_CPF_ALU;
                }
                else if (tpUsu == "R")
                {
                    var tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coCol);
                    admUsuario = ADMUSUARIO.RetornaPeloCodUsuario(coCol);
                    strSenhaMD5 = LoginAuxili.GerarMD5(tb108.NU_CPF_RESP);
                    CO_EMP_USU = LoginAuxili.CO_EMP;
                    NU_CPF_USU = tb108.NU_CPF_RESP;
                }
                else
                {
                    var tb03 = RetornaEntidade(coCol);
                    admUsuario = ADMUSUARIO.RetornaPelaUnidColabor(tb03.CO_EMP, tb03.CO_COL);
                    strSenhaMD5 = LoginAuxili.GerarMD5(tb03.NU_CPF_COL);
                    CO_EMP_USU = tb03.CO_EMP;
                    NU_CPF_USU = tb03.NU_CPF_COL;
                }

                //------------> Se retorno NULL cria um novo registro
                if (admUsuario == null)
                {
                    admUsuario = new ADMUSUARIO();

                    //----------------> Cria uma senha padrao quando é um novo registro de usuário
                    //strSenhaMD5 = LoginAuxili.GerarMD5(String.Concat(DateTime.Now.Day.ToString(), DateTime.Now.Millisecond.ToString(), tb03.CO_COL));
                    //A senha costumava ser 123456, agora foi definido o cpf do colaborador
                    //strSenhaMD5 = LoginAuxili.GerarMD5("123456");
                    admUsuario.desSenha = strSenhaMD5;
                    admUsuario.CodInstituicao = LoginAuxili.ORG_CODIGO_ORGAO;
                    admUsuario.CO_EMP = CO_EMP_USU;
                    admUsuario.CodUsuario = coCol;
                    admUsuario.flaPrimeiroAcesso = "S";
                }

                admUsuario.desLogin = txtLoginMUS.Text;
                admUsuario.TipoUsuario = ddlTpUsuMUS.SelectedValue;
                admUsuario.FLA_MANUT_CAIXA = ddlUsuaCaixaMUS.SelectedValue;
                LoginAuxili.FLA_ALT_BOL_ALU = ddlUsuaCaixaMUS.SelectedValue;
                admUsuario.FL_MANUT_COBRANCA = drpUsrCobranca.SelectedValue;
                if (!String.IsNullOrEmpty(txtPercCobranca.Text))
                    admUsuario.NU_PERCT_COBRANCA = decimal.Parse(txtPercCobranca.Text);

                if ((txtQtdSMSMes.Enabled) && (txtQtdSMSMes.Text != ""))
                {
                    admUsuario.QT_SMS_MAXIM_USR = int.Parse(txtQtdSMSMes.Text);
                }
                else
                {
                    admUsuario.QT_SMS_MAXIM_USR = null;
                }

                //------------> Faz a verificaçao dos dias de acesso selecionados
                foreach (ListItem lstDiaAcesso in cbDiaAcessoMUS.Items)
                {
                    if (lstDiaAcesso.Value == "SG")
                        admUsuario.FLA_ACESS_SEG = lstDiaAcesso.Selected ? "S" : "N";

                    if (lstDiaAcesso.Value == "TR")
                        admUsuario.FLA_ACESS_TER = lstDiaAcesso.Selected ? "S" : "N";

                    if (lstDiaAcesso.Value == "QR")
                        admUsuario.FLA_ACESS_QUA = lstDiaAcesso.Selected ? "S" : "N";

                    if (lstDiaAcesso.Value == "QN")
                        admUsuario.FLA_ACESS_QUI = lstDiaAcesso.Selected ? "S" : "N";

                    if (lstDiaAcesso.Value == "SX")
                        admUsuario.FLA_ACESS_SEX = lstDiaAcesso.Selected ? "S" : "N";

                    if (lstDiaAcesso.Value == "SB")
                        admUsuario.FLA_ACESS_SAB = lstDiaAcesso.Selected ? "S" : "N";

                    if (lstDiaAcesso.Value == "DG")
                        admUsuario.FLA_ACESS_DOM = lstDiaAcesso.Selected ? "S" : "N";
                }

                int intRetorno = 0;
                int hrIni = int.TryParse(txtHoraAcessoIMUS.Text.Replace(":", ""), out intRetorno) ? intRetorno : 0;
                int hrFin = int.TryParse(txtHoraAcessoFMUS.Text.Replace(":", ""), out intRetorno) ? intRetorno : 0;

                admUsuario.HR_INIC_ACESSO = hrIni != 0 ? hrIni.ToString() : null;
                admUsuario.HR_FIM_ACESSO = hrFin != 0 ? hrFin.ToString() : null;
                admUsuario.FLA_MANUT_PONTO = chkFreqManuMUS.Checked ? "S" : "N";
                admUsuario.FLA_MANUT_RESER_BIBLI = chkBibliReservaMUS.Checked ? "S" : "N";
                admUsuario.FLA_SMS = chkSerEnvioSMSMUS.Checked ? "S" : "N";
                admUsuario.FLA_ALT_BOL_ALU = chkAltBolsaAlu.Checked ? "S" : "N";
                admUsuario.FLA_ALT_BOL_ESPE_ALU = chkAltBolsaEspecAlu.Checked ? "S" : "N";
                admUsuario.FLA_ALT_REG_PAG_MAT = chkAltPagtoMatric.Checked ? "S" : "N";
                admUsuario.FLA_ALT_PARAM_MAT = chkAltParamMatric.Checked ? "S" : "N";
                admUsuario.FLA_PERM_LANC_MULTI = chkFazLancMultiNotas.Checked ? "S" : "N";
                admUsuario.FL_PERMI_AGEND_MULTI = (chkPermAgendaMult.Checked ? "S" : "N");
                admUsuario.FL_PERMI_MOVIM_LOCAL = (chkPermMovLocal.Checked ? "S" : "N");
                admUsuario.FL_REVER_BAIXA_ATEND = (chkPermReverter.Checked ? "S" : "N");
                admUsuario.FL_FINAL_ATEND_RECEP = (chkPermFinalizarRecepcao.Checked ? "S" : "N");
                admUsuario.ClassifUsuario = ddlClaUsuMUS.SelectedValue;
                admUsuario.SituUsuario = ddlStatusMUS.SelectedValue;
                admUsuario.DataStatus = Convert.ToDateTime(txtDtSituacaoMUS.Text);
                admUsuario.CodUnidadeCadastro = LoginAuxili.CO_EMP;
                admUsuario.CodUsuarioCadastro = LoginAuxili.CO_COL;
                admUsuario.DataCadastro = DateTime.Now;
                admUsuario.FL_MOBILE_PHONE = (chkMobilePhone.Checked ? "S" : "N");

                //------------> Informa a senha criada, atraves de SMS, para o usuário cadastrado
                if ((QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    && chkAltLoginSMSMUS.Checked)
                {
                    string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).sigla;
                    string urlSistema = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NM_URL_UNID_B;
                    string texto = (LoginAuxili.CO_TIPO_UNID == "PGS" ? "(Portal Saúde)" : "(Portal Educacao)") + " Cadastro efetuado! Login: " + txtLoginMUS.Text + " Senha Inicial: " + NU_CPF_USU + " - Acesse: " + urlSistema;
                    int lenTex = texto.Length;

                    SMSAuxili.EnvioSMS(siglaUnid, texto,
                                  "55" + txtCelularMUS.Text, DateTime.Now.Ticks.ToString());
                }

                ADMUSUARIO.SaveOrUpdate(admUsuario, true);

                if (chkMobilePhone.Checked)
                    SalvaUsuarioAPP(ddlClaUsuMUS.SelectedValue == "M", admUsuario.desSenha, admUsuario.CodUsuario);

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                else
                    if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        /// <param name="coCol">Id do funcionário</param>
        private void CarregaFormulario(int coCol)
        {
            string tipo = QueryStringAuxili.QueryStringValor(QueryStrings.Id);
            ADMUSUARIO admUsuario;
            C2BR.GestorEducacao.BusinessEntities.MSSQL.Image img;
            var tpUsu = ddlTpUsuMUS.SelectedValue;
            var NO_USU = "";
            var CO_EMP_USU = 0;

            CarregaUnidades();
            

            if (tpUsu == "A" || tipo == "A")
            {
                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coCol);

                if (tb07 == null)
                    return;

                tb07.ImageReference.Load();
                img = tb07.Image;

                ddlUnidadeMUS.SelectedValue = LoginAuxili.CO_EMP.ToString();
                ddlColMUS.SelectedValue = tb07.CO_ALU.ToString();
                txtApelidoMUS.Text = tb07.NO_APE_ALU;
                txtEmailMUS.Text = "";
                txtCelularMUS.Text = tb07.NU_TELE_CELU_ALU != null ? tb07.NU_TELE_CELU_ALU : "";
                txtTelefoneMUS.Text = tb07.NU_TELE_RESI_ALU != null ? tb07.NU_TELE_RESI_ALU : "";

                txtDepartamentoMUS.Text = "Aluno";

                txtFuncaoMUS.Text = "Aluno";

                admUsuario = ADMUSUARIO.RetornaPeloCodUsuario(coCol);
                NO_USU = tb07.NO_ALU;
                CO_EMP_USU = LoginAuxili.CO_EMP;
            }
            else if (tpUsu == "R" || tipo == "R")
            {
                var tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coCol);

                if (tb108 == null)
                    return;

                tb108.ImageReference.Load();
                img = tb108.Image;

                ddlUnidadeMUS.SelectedValue = LoginAuxili.CO_EMP.ToString();
                ddlColMUS.SelectedValue = tb108.CO_RESP.ToString();
                txtApelidoMUS.Text = tb108.NO_APELIDO_RESP;
                txtEmailMUS.Text = tb108.DES_EMAIL_RESP != null ? tb108.DES_EMAIL_RESP : "";
                txtCelularMUS.Text = tb108.NU_TELE_CELU_RESP != null ? tb108.NU_TELE_CELU_RESP : "";
                txtTelefoneMUS.Text = tb108.NU_TELE_RESI_RESP != null ? tb108.NU_TELE_RESI_RESP : "";

                txtDepartamentoMUS.Text = "Responsável Financeiro";

                txtFuncaoMUS.Text = "Responsável";

                admUsuario = ADMUSUARIO.RetornaPeloCodUsuario(coCol);
                NO_USU = tb108.NO_RESP;
                CO_EMP_USU = LoginAuxili.CO_EMP;
            }
            else
            {
                var tb03 = RetornaEntidade(coCol);

                if (tb03 == null)
                    return;

                tb03.ImageReference.Load();
                img = tb03.Image;

                ddlUnidadeMUS.SelectedValue = tb03.CO_EMP.ToString();
                CarregaColaborador();
                ddlColMUS.SelectedValue = tb03.CO_COL.ToString();
                txtApelidoMUS.Text = tb03.NO_APEL_COL;
                txtEmailMUS.Text = tb03.CO_EMAIL_FUNC_COL != null ? tb03.CO_EMAIL_FUNC_COL : "";
                txtCelularMUS.Text = tb03.NU_TELE_CELU_COL != null ? tb03.NU_TELE_CELU_COL : "";
                txtTelefoneMUS.Text = tb03.NU_TELE_RESI_COL != null ? tb03.NU_TELE_RESI_COL : "";

                var vTb14 = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                             where tb14.CO_DEPTO == tb03.CO_DEPTO
                             select new { tb14.NO_DEPTO }).FirstOrDefault();

                txtDepartamentoMUS.Text = vTb14 != null ? vTb14.NO_DEPTO : "";

                var vTb15 = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                             where tb15.CO_FUN == tb03.CO_FUN
                             select new { tb15.NO_FUN }).FirstOrDefault();

                txtFuncaoMUS.Text = vTb15 != null ? vTb15.NO_FUN : "";

                admUsuario = ADMUSUARIO.RetornaPelaUnidColabor(tb03.CO_EMP, tb03.CO_COL);
                NO_USU = tb03.NO_COL;
                CO_EMP_USU = tb03.CO_EMP;
            }

            upImagemColab.MostraProcurar = false;
            if (img != null)
                upImagemColab.CarregaImagem(img.ImageId);
            else
                upImagemColab.CarregaImagem(0);

            if (admUsuario != null)
            {
                txtLoginMUS.Text = admUsuario.desLogin;
                ddlTpUsuMUS.SelectedValue = admUsuario.TipoUsuario;
                ddlUsuaCaixaMUS.SelectedValue = admUsuario.FLA_MANUT_CAIXA == null ? "N" : admUsuario.FLA_MANUT_CAIXA;

                drpUsrCobranca.SelectedValue = !String.IsNullOrEmpty(admUsuario.FL_MANUT_COBRANCA) ? admUsuario.FL_MANUT_COBRANCA : "N";
                txtPercCobranca.Text = admUsuario.NU_PERCT_COBRANCA.HasValue ? admUsuario.NU_PERCT_COBRANCA.Value.ToString() : "";

                foreach (ListItem lstDiaAcesso in cbDiaAcessoMUS.Items)
                {
                    if (lstDiaAcesso.Value == "SG")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_SEG == "S";

                    if (lstDiaAcesso.Value == "TR")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_TER == "S";

                    if (lstDiaAcesso.Value == "QR")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_QUA == "S";

                    if (lstDiaAcesso.Value == "QN")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_QUI == "S";

                    if (lstDiaAcesso.Value == "SX")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_SEX == "S";

                    if (lstDiaAcesso.Value == "SB")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_SAB == "S";

                    if (lstDiaAcesso.Value == "DG")
                        lstDiaAcesso.Selected = admUsuario.FLA_ACESS_DOM == "S";
                }

                txtHoraAcessoIMUS.Text = admUsuario.HR_INIC_ACESSO != null ? int.Parse(admUsuario.HR_INIC_ACESSO).ToString("0000") : "";
                txtHoraAcessoFMUS.Text = admUsuario.HR_FIM_ACESSO != null ? int.Parse(admUsuario.HR_FIM_ACESSO).ToString("0000") : "";
                chkFreqManuMUS.Checked = admUsuario.FLA_MANUT_PONTO == "S";
                chkBibliReservaMUS.Checked = admUsuario.FLA_MANUT_RESER_BIBLI == "S";
                chkAltBolsaAlu.Checked = admUsuario.FLA_ALT_BOL_ALU == "S";
                chkAltBolsaEspecAlu.Checked = admUsuario.FLA_ALT_BOL_ESPE_ALU == "S";
                chkAltPagtoMatric.Checked = admUsuario.FLA_ALT_REG_PAG_MAT == "S";
                chkAltParamMatric.Checked = admUsuario.FLA_ALT_PARAM_MAT == "S";
                chkSerEnvioSMSMUS.Checked = admUsuario.FLA_SMS == "S";
                chkPermAgendaMult.Checked = admUsuario.FL_PERMI_AGEND_MULTI == "S";
                chkPermMovLocal.Checked = admUsuario.FL_PERMI_MOVIM_LOCAL != null ? admUsuario.FL_PERMI_MOVIM_LOCAL.Equals("S") : false;
                chkPermFinalizarRecepcao.Checked = (admUsuario.FL_FINAL_ATEND_RECEP == FlagAuxi.SIM);
                chkPermReverter.Checked = (admUsuario.FL_REVER_BAIXA_ATEND == FlagAuxi.SIM);
                chkFazLancMultiNotas.Checked = admUsuario.FLA_PERM_LANC_MULTI == "S";
                chkMobilePhone.Checked = admUsuario.FL_MOBILE_PHONE == "S";
                ddlTpUsuMUS.Enabled = false;
                ddlClaUsuMUS.SelectedValue = admUsuario.ClassifUsuario;
                ddlStatusMUS.SelectedValue = admUsuario.SituUsuario;

                if (txtQtdSMSMes.Enabled)
                {
                    txtQtdSMSMes.Text = admUsuario.QT_SMS_MAXIM_USR.ToString();
                }
            }
            else
            {
                string strNomeColaborador = NO_USU;
                strNomeColaborador = strNomeColaborador.TrimEnd().TrimStart();

                string[] stg = (strNomeColaborador).Split(' ');
                int qtd = stg.Count();
                string strDesLogin = (stg[0].Substring(0, 1) + stg[qtd - 1]).ToLower();

                int loginUsado = (from lAdmUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                  where lAdmUsuario.desLogin == strDesLogin && lAdmUsuario.CO_EMP == CO_EMP_USU && lAdmUsuario.CodUsuario == coCol
                                  select new { lAdmUsuario.desLogin }).Count();

                if (loginUsado > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    AuxiliPagina.EnvioMensagemErro(this, "Login gerado pelo sistema já está em uso, favor entrar em contato com o Suporte Técnico.");

                txtLoginMUS.Enabled = true;
                txtLoginMUS.Text = strDesLogin;
            }
        }

        /// <summary>
        /// Salva o usuário já sendo salvo em contexto, na tabela de usuários de aplicativo
        /// </summary>
        /// <param name="gestor"></param>
        private void SalvaUsuarioAPP(bool gestor, string senha, int coCol)
        {
            //Se já houver usuário de aplicativo para este profissional, retorna
            if (TBS384_USUAR_APP.RetornaTodosRegistros().Where(w => w.CO_COL == coCol).Any())
                return;

            TBS384_USUAR_APP tbs384 = new TBS384_USUAR_APP();
            //Dados gerais
            tbs384.CO_TIPO = (gestor ? "G" : "C");
            tbs384.DE_LOGIN = txtLoginMUS.Text;
            tbs384.DE_SENHA = senha;
            tbs384.NM_USUAR = ddlColMUS.SelectedItem.Text;
            tbs384.NM_APELI_USUAR = txtApelidoMUS.Text;
            tbs384.CO_COL = coCol;

            //Situação
            tbs384.CO_SITUA = ddlStatusMUS.SelectedValue;
            tbs384.DT_SITUA = DateTime.Now;
            tbs384.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs384.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs384.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs384.IP_SITUA = Request.UserHostAddress;

            //Cadastro
            tbs384.DT_CADAS = DateTime.Now;
            tbs384.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs384.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs384.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs384.IP_CADAS = Request.UserHostAddress;

            TBS384_USUAR_APP.SaveOrUpdate(tbs384, true);
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="coCol">Id do funcionário</param>
        /// <returns>Entidade TB03_COLABOR</returns>
        private TB03_COLABOR RetornaEntidade(int? coCol)
        {
            if (coCol == null)
                coCol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol);

            int idCol = Convert.ToInt32(coCol);

            return TB03_COLABOR.RetornaPeloCoCol(idCol);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que preenche o DropDown de funcionário de acordo com os parâmetros necessários
        /// </summary>
        private void CarregaColaborador()
        {
            string tipo = QueryStringAuxili.QueryStringValor(QueryStrings.Id);
            var tpUsu = ddlTpUsuMUS.SelectedValue;
            int? coCol = Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));
            int coEmp = ddlUnidadeMUS.SelectedValue != "" ? int.Parse(ddlUnidadeMUS.SelectedValue) : 0;

            if (tipo == "A" || tpUsu == "A")
            {
                if (!coCol.HasValue || coCol == 0)
                {

                    var resultado = from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                    join tb07 in TB07_ALUNO.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb07.CO_ALU
                                    where admUsuario.CO_EMP == coEmp
                                          && admUsuario.TipoUsuario == "A"
                                    select new { CO_COL = tb07.CO_ALU, NO_COL = tb07.NO_ALU };

                    ddlColMUS.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                            select new { CO_COL = tb07.CO_ALU, NO_COL = tb07.NO_ALU.ToUpper() }).Except(resultado).OrderBy(c => c.NO_COL); ;

                }
                else {
                    ddlColMUS.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                            select new { CO_COL = tb07.CO_ALU, NO_COL = tb07.NO_ALU.ToUpper() }).OrderBy(c => c.NO_COL);
                }
            }
            else if (tipo == "R" || tpUsu == "R")
            {
                if (!coCol.HasValue || coCol == 0)
                {

                    var resultado = from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                    join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                    where admUsuario.CO_EMP == coEmp
                                          && admUsuario.TipoUsuario == "R"
                                    select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP };

                    ddlColMUS.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() }).Except(resultado).OrderBy(c => c.NO_COL); ;

                }
                else
                {
                    ddlColMUS.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() }
                                    ).OrderBy(c => c.NO_COL);
                }
            }
            else
            {
                if (!coCol.HasValue || coCol == 0)
                {
                    string strFlaProfessor = (ddlTpUsuMUS.SelectedValue == "P" || ddlTpUsuMUS.SelectedValue == "S" ? "S" : "N");

                    var resultado = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                     join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                     where admUsuario.CO_EMP == coEmp
                                     select new { CO_COL = tb03.CO_COL, NO_COL = tb03.NO_COL }).Concat(
                                            from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                            join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                            where tb108.CO_INST == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() });

                    ddlColMUS.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                            where tb03.CO_EMP == coEmp && tb03.FLA_PROFESSOR == strFlaProfessor
                                            select new { CO_COL = tb03.CO_COL, NO_COL = tb03.NO_COL.ToUpper() }).Concat(
                                            from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                            where tb108.CO_INST == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() }
                                            ).Except(resultado).OrderBy(c => c.NO_COL);
                }
                else
                {
                    ddlColMUS.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                            where tb03.CO_EMP == coEmp
                                            select new { CO_COL = tb03.CO_COL, NO_COL = tb03.NO_COL.ToUpper() }).Concat(
                                            from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                            where tb108.CO_INST == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() }
                                            ).OrderBy(c => c.NO_COL);
                }
            }

            ddlColMUS.DataTextField = "NO_COL";
            ddlColMUS.DataValueField = "CO_COL";
            ddlColMUS.DataBind();
            
            if (!coCol.HasValue || coCol == 0)
                ddlColMUS.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlColMUS.SelectedValue = coCol.ToString();
        }

        /// <summary>
        /// Método que carrega dropdown das Unidade Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidadeMUS.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                        join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                        where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                        select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeMUS.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeMUS.DataValueField = "CO_EMP";
            ddlUnidadeMUS.DataBind();

            int? coEmp = Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp));

            if (coEmp == null || coEmp == 0)
            {
                ddlUnidadeMUS.SelectedValue = LoginAuxili.CO_EMP.ToString();
                CarregaColaborador();
            }
            else
            {
                ddlUnidadeMUS.SelectedValue = coEmp.ToString();
                CarregaColaborador();
            }

            if ((ddlUnidadeMUS.SelectedValue != "") && (txtQtdSMSMes.Enabled))
            {
                int coEmpRefer = int.Parse(ddlUnidadeMUS.SelectedValue);

                var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (tb149.TP_CTRLE_MENSA_SMS == "U")
                {
                    var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmpRefer);

                    if (tb83.FL_ENVIO_SMS != null)
                    {
                        txtQtdSMSMes.Enabled = tb83.FL_ENVIO_SMS == "S";
                    }
                }
                else
                {
                    txtQtdSMSMes.Enabled = tb149.FL_ENVIO_SMS == "S";
                }

            }

        }

        /// <summary>
        /// Carrega a categoria funcional de acordo com o tipo de unidade logada
        /// </summary>
        private void CarregaCamposUnidEscol()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    //Alterações do tipo
                    ddlTpUsuMUS.Items.Clear();
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Profissional Saúde", "S"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Selecione", ""));

                    //Alterações nas permissões de acesso
                    chkGestorEducacao.Text = "Portal Saúde";
                    chkMobilePhone.Text = "Mobile Phone";

                    chkAltPagtoMatric.Visible =
                    chkAltParamMatric.Visible =
                    chkAltBolsaAlu.Visible =
                    chkAltBolsaEspecAlu.Visible =
                    chkFazLancMultiNotas.Visible = false;

                    chkPermAgendaMult.Visible =
                    chkPermMovLocal.Visible =
                    chkPermFinalizarRecepcao.Visible =
                    chkPermReverter.Visible = true;
                    break;
                case "PGE":
                default:
                    //Alterações do tipo
                    ddlTpUsuMUS.Items.Clear();
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Aluno", "A"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Responsável", "R"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Professor", "S"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    ddlTpUsuMUS.Items.Insert(0, new ListItem("Selecione", ""));

                    //Alterações nas permissões de acesso
                    chkGestorEducacao.Text = "Gestor Educação";
                    chkMobilePhone.Text = "Ensino Remoto";

                    chkAltPagtoMatric.Visible =
                    chkAltParamMatric.Visible =
                    chkAltBolsaAlu.Visible =
                    chkAltBolsaEspecAlu.Visible =
                    chkFazLancMultiNotas.Visible = true;

                    chkPermAgendaMult.Visible =
                    chkPermMovLocal.Visible =
                    chkPermFinalizarRecepcao.Visible =
                    chkPermReverter.Visible = false;

                    break;
            }
        }

        #endregion

        protected void ddlUnidadeMUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaborador();

            if ((ddlUnidadeMUS.SelectedValue != "") && (txtQtdSMSMes.Enabled))
            {
                int coEmpRefer = int.Parse(ddlUnidadeMUS.SelectedValue);

                var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (tb149.TP_CTRLE_MENSA_SMS == "U")
                {
                    var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmpRefer);

                    if (tb83.FL_ENVIO_SMS != null)
                    {
                        txtQtdSMSMes.Enabled = tb83.FL_ENVIO_SMS == "S";
                    }
                }
                else
                {
                    txtQtdSMSMes.Enabled = tb149.FL_ENVIO_SMS == "S";
                }
            }
        }

        protected void ddlTpUsuMUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaborador();
        }

        protected void ddlColMUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coPessoa = ddlColMUS.SelectedValue != "" ? int.Parse(ddlColMUS.SelectedValue) : 0;
            if (coPessoa != 0)
                CarregaFormulario(Convert.ToInt32(coPessoa));
        }
    }
}
