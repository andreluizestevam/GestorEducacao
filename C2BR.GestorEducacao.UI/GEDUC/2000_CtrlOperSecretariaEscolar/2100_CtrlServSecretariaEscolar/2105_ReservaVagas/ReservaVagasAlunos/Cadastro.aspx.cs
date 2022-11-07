//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: RESERVA DE VAGAS DE MATRÍCULAS
// OBJETIVO: RESERVAS DE VAGAS LETIVAS
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data.Objects;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2105_ReservaVagas.ReservaVagasAlunos
{
    public partial class Cadastro : System.Web.UI.Page
    {        
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        private static TB07_ALUNO tbAluno;
        private static TB108_RESPONSAVEL tbResponsavel;

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
            bool geraRelatorio = false;

            if (!IsPostBack)
            {
//------------> Faz a validação para saber se o evento é de exibição do relatório gerado.
                if (Session["ApresentaRelatorio"] != null)
                    if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                    {
                        geraRelatorio = true;
                        AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
//--------------------> Limpa a variável de sessão com o url do relatório.
                        Session.Remove("URLRelatorio");
                        Session.Remove("ApresentaRelatorio");
                        PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                        isreadonly.SetValue(this.Request.QueryString, false, null);
                        this.Request.QueryString.Remove("ApresentaRelatorio");
                        isreadonly.SetValue(this.Request.QueryString, true, null);
                    }

                tbAluno = null;
                tbResponsavel = null;
                CarregaUfs(ddlUfResponsavelRVL);
                CarregaUfs(ddlUfCandidatoRVL);

                CarregaUnidadeSugeridas();
                CarregaUnidadeSugerida2();
                CarregaUnidadeSugerida3();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaMotivosCancelamento();

                ddlSemestreRVL.Items.Clear();
                ddlSemestreRVL.Items.Insert(0, new ListItem("1", "1"));
                ddlSemestreRVL.Items.Insert(1, new ListItem("2", "2"));

                if ((QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) || (geraRelatorio))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

                    txtDataCadastroRVL.Text = dataAtual;
                    txtDataStatusRVL.Text = dataAtual;

                    ddlUfResponsavelRVL.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
                    ddlCidadeResponsavelRVL.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
                    CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);

                    if (DateTime.Now.Month > 6)
                        ddlSemestreRVL.SelectedValue = "2";
                    else
                        ddlSemestreRVL.SelectedValue = "1";

                    ddlAnoRVL.Items.Insert(0, new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
                    ddlAnoRVL.Items.Insert(1, new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));

                    txtCpfResponsavelRVL.Enabled = btnValidarCpfResponsavel.Enabled = ddlUnidadeSugeridaRVL.Enabled = ddlTipoReservaRVL.Enabled = 
                    ddlModalidadeRVL.Enabled = ddlSerieCursoRVL.Enabled = ddlTurnoRVL.Enabled = ddlSemestreRVL.Enabled = ddlAnoRVL.Enabled = chkGeraCompro.Checked = true;
                    chkGeraCompro.Enabled = false;
                }
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    HabilitarCamposResponsavel(false);
                    HabilitarCamposCandidato(false);
                    txtCpfResponsavelRVL.Enabled = btnValidarCpfResponsavel.Enabled = ddlUnidadeSugeridaRVL.Enabled = ddlTipoReservaRVL.Enabled = 
                    ddlModalidadeRVL.Enabled = ddlSerieCursoRVL.Enabled = ddlTurnoRVL.Enabled = ddlSemestreRVL.Enabled = ddlAnoRVL.Enabled = chkGeraCompro.Checked = false;
                    chkGeraCompro.Enabled = true;
                }

                HabilitarCamposResponsavel(false);
                HabilitarCamposCandidato(false);
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ddlSituacaoRVL.SelectedValue == "C" && ddlMotivoCancelRVL.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Motivo do cancelamento deve ser informado.");
                return;
            }

            int proxRegistro = 0;
            int retornaInt;
            
//--------> Verifica se aluno tem matrícula em aberto ou se o ano da última matrícula menor que ano atual menos dois 
            if (tbAluno != null)
            {
                var ocoMat = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                              where tb08.TB07_ALUNO.CO_ALU.Equals(tbAluno.CO_ALU)
                              select new { tb08.CO_SIT_MAT, tb08.CO_ANO_MES_MAT }).ToList();

                if (ocoMat.Count() > 0)
                {
                    string situaMatri = ocoMat.Last().CO_SIT_MAT;
                    int anoMesMat = int.Parse(ocoMat.Last().CO_ANO_MES_MAT);

                    if (situaMatri.Equals("A"))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno possui matrícula em aberto.");
                        return;
                    }
                    else if (situaMatri.Equals("F"))
                    {
                        if (anoMesMat > DateTime.Now.Year - 2)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno não necessita de reserva, deve seguir direto para Rematrícula.");
                            return;
                        }
                    }
                }
            }

//--------> Verifica se aluno já tem reserva
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                DateTime dataNascto = tbAluno == null ? DateTime.Parse(txtDataNascimentoCandidatoRVL.Text) : (DateTime)tbAluno.DT_NASC_ALU;
                string nomeCandid = tbAluno == null ? txtNomeCandidatoRVL.Text : tbAluno.NO_ALU;

                var ocoReser = (from iTb052 in TB052_RESERV_MATRI.RetornaTodosRegistros()
                                where iTb052.NO_ALU.Equals(nomeCandid) && iTb052.DT_NASC_ALU == dataNascto && iTb052.CO_STATUS == "A"
                                select new { iTb052.NU_RESERVA }).ToList();

                if (ocoReser.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno possui reserva de matrícula em aberto.");
                    return;
                }
            }

            TB052_RESERV_MATRI tb052 = RetornaEntidade();

            DateTime dataAtual = DateTime.Now;

            int coEmp = ddlUnidadeSugeridaRVL.SelectedValue != "" ? int.Parse(ddlUnidadeSugeridaRVL.SelectedValue) : 0;
            int coEmp2 = ddlUnidadeSugerida2RVL.SelectedValue != "" ? int.Parse(ddlUnidadeSugerida2RVL.SelectedValue) : 0;
            int coEmp3 = ddlUnidadeSugerida3RVL.SelectedValue != "" ? int.Parse(ddlUnidadeSugerida3RVL.SelectedValue) : 0;

            int modalidade = ddlModalidadeRVL.SelectedValue != "" ? int.Parse(ddlModalidadeRVL.SelectedValue) : 0;
            int serie = ddlSerieCursoRVL.SelectedValue != "" ? int.Parse(ddlSerieCursoRVL.SelectedValue) : 0;

            if (tb052 == null)
            {
                tb052 = new TB052_RESERV_MATRI();

                var nuReserva = (from lTb052 in TB052_RESERV_MATRI.RetornaTodosRegistros().OrderBy( r => r.DT_CADASTRO )
                                 where lTb052.CO_EMP_CADASTRO == coEmp
                                 select new { lTb052.NU_RESERVA }).ToList();

//------------> Se nuReserva for NULL então é a primeira reserva
                proxRegistro = nuReserva.Count() == 0 ? 1 : int.Parse(nuReserva.ToList().Last().NU_RESERVA.Split('.').LastOrDefault()) + 1;
                tb052.NU_RESERVA = string.Format("{0}.{1}.{2}", dataAtual.Year.ToString(), dataAtual.Month.ToString().PadLeft(2, '0'), proxRegistro.ToString().PadLeft(4, '0'));
                tb052.CO_EMP_CADASTRO = LoginAuxili.CO_EMP;
                tb052.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                tb052.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                tb052.DT_CADASTRO = DateTime.Parse(txtDataCadastroRVL.Text);
                tb052.QTD_DEPEND = int.Parse(txtQtdDepenRVL.Text);
                tb052.VR_RENDA_FAMILIAR_RESP = ddlRendaFamiliarRVL.SelectedValue;
                tb052.CO_GRAU_PAREN_RESP = ddlGrauParentescoRVL.SelectedValue;

                tb052.TP_CADASTRO = "W";
                tb052.TP_RESERVA = ddlTipoReservaRVL.SelectedValue;
                tb052.NU_ANO_LETIVO = int.Parse(ddlAnoRVL.SelectedValue);
                tb052.NU_SEM_LETIVO = ddlSemestreRVL.SelectedValue;
                tb052.CO_PERI_TUR = ddlTurnoRVL.SelectedValue;
                tb052.CO_STATUS = ddlSituacaoRVL.SelectedValue;
                tb052.DE_OBS_RESERV_MATRI = txtObsReservaRVL.Text != "" ? txtObsReservaRVL.Text : null;

                if (tb052.CO_STATUS == "C")
                    tb052.TB57_MOTIVCANC = TB57_MOTIVCANC.RetornaPelaChavePrimaria(int.Parse(ddlMotivoCancelRVL.Text));

                tb052.DT_STATUS = DateTime.Parse(txtDataStatusRVL.Text);
                tb052.DT_VALIDADE_RESERV = DateTime.Parse(txtDataValidadeRVL.Text);                

//------------> Se o responsável pesquisado existir então atribui seu código a reserva, senão grava os campos informados na tabela de reserva
                if (tbResponsavel != null)
                    tb052.TB108_RESPONSAVEL = tbResponsavel;
                else
                {
                    tb052.NO_RESP = txtNomeResponsavelRVL.Text;
                    tb052.NU_CPF_RESP = txtCpfResponsavelRVL.Text.Replace(".", "").Replace("-", "");
                    tb052.DT_NASC_RESP = txtDataNascimentoResponsavelRVL.Text != "" ? (DateTime?)Convert.ToDateTime(txtDataNascimentoResponsavelRVL.Text) : null;
                    tb052.CO_ESTADO_CIVIL_RESP = ddlEstadoCivilRVL.SelectedValue;
                    tb052.CO_RG_RESP = txtNumRGRespRVL.Text != "" ? txtNumRGRespRVL.Text : null;
                    tb052.CO_ORG_RG_RESP = txtOrgaoRGRespRVL.Text != "" ? txtOrgaoRGRespRVL.Text : null;
                    tb052.DT_EMIS_RG_RESP = txtDtEmissaoRGRespRVL.Text != "" ? (DateTime?)Convert.ToDateTime(txtDtEmissaoRGRespRVL.Text) : null;
                    tb052.NU_TEL_RESP = txtTelResidencialResponsavelRVL.Text != "" ? txtTelResidencialResponsavelRVL.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb052.NU_CEL_RESP = txtTelCelularResponsavelRVL.Text != "" ? txtTelCelularResponsavelRVL.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb052.DE_EMAIL_RESP = txtEmailResponsavelRVL.Text != "" ? txtEmailResponsavelRVL.Text : null;
                    tb052.DE_END_RESP = txtLogradouroCandidatoRVL.Text;
                    tb052.DE_COM_END_RESP = txtComplementoResponsavelRVL.Text != "" ? txtComplementoResponsavelRVL.Text : null;
                    tb052.NU_END_RESP = int.TryParse(txtNumeroResponsavelRVL.Text, out retornaInt) ? (int?)retornaInt : null;
                    tb052.TB905_BAIRRO1 = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroResponsavelRVL.SelectedValue));
                    tb052.CO_CEP_RESP = txtCepResponsavelRVL.Text != "" ? txtCepResponsavelRVL.Text.Replace("-", "") : null;
                    tb052.NO_EMP_TRAB_RESP = txtEmpresaRespRVL.Text != "" ? txtEmpresaRespRVL.Text : null;
                    tb052.NU_TEL_TRAB_RESP = txtNumEmpresaRespRVL.Text != "" ? txtNumEmpresaRespRVL.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                    tb052.NO_FUNC_TRAB_RESP = txtFuncaoRespRVL.Text != "" ? txtFuncaoRespRVL.Text : null;
                    tb052.DE_EMAIL_TRAB_RESP = txtEmpresaRespRVL.Text != "" ? txtEmpresaRespRVL.Text : null;
                }

//------------> Se o aluno pesquisado existir então atribui seu código a reserva, senão grava os campos informados na tabela de reserva
                if (tbAluno != null)
                {
                    tb052.TB07_ALUNO = tbAluno;
                    tb052.NO_ALU = tbAluno.NO_ALU;
                    tb052.DT_NASC_ALU = tbAluno.DT_NASC_ALU;
                }
                else
                {
                    tb052.NO_ALU = txtNomeCandidatoRVL.Text;
                    tb052.NU_CPF_ALU = txtCpfCandidatoRVL.Text.Replace(".", "").Replace("-", "");
                    tb052.CO_RG_ALU = txtNumRGAluRVL.Text != "" ? txtNumRGAluRVL.Text : null;
                    tb052.CO_ORG_RG_ALU = txtOrgaoRGAluRVL.Text != "" ? txtOrgaoRGAluRVL.Text : null;
                    tb052.DT_EMIS_RG_ALU = txtDtEmissRGAluRVL.Text != "" ? (DateTime?)Convert.ToDateTime(txtDtEmissRGAluRVL.Text) : null;
                    tb052.NO_MAE_ALU = txtNomeMaeAlunoRVL.Text != "" ? txtNomeMaeAlunoRVL.Text : null;
                    tb052.NO_PAI_ALU = txtNomePaiAlunoRVL.Text != "" ? txtNomePaiAlunoRVL.Text : null;
                    tb052.NU_NIRE_ALU = int.TryParse(txtNireRVL.Text, out retornaInt) ? (int?)retornaInt : null;
                    tb052.NO_NACIO_ALU = ddlNacionalidadeCandidatoRVL.SelectedValue;
                    tb052.CO_SEXO_ALU = ddlSexoCandidatoRVL.SelectedValue;
                    tb052.CO_GRAU_PARENT_RESP = ddlGrauParentescoRVL.SelectedValue;
                    tb052.TP_DEFIC_ALU = ddlDeficienciaCandidatoRVL.SelectedValue;
                    tb052.DT_NASC_ALU = DateTime.Parse(txtDataNascimentoCandidatoRVL.Text);
                    tb052.DE_END_ALU = txtLogradouroCandidatoRVL.Text;
                    tb052.DE_COM_END_ALU = txtComplementoCandidatoRVL.Text != "" ? txtComplementoCandidatoRVL.Text : null;
                    tb052.NU_END_ALU = int.TryParse(txtNumeroCandidatoRVL.Text, out retornaInt) ? (int?)retornaInt : null;
                    tb052.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroCandidatoRVL.SelectedValue));
                    tb052.CO_CEP_ALU = txtCepCandidatoRVL.Text != "" ? txtCepCandidatoRVL.Text.Replace("-", "") : null;
                    tb052.DE_OBS_ALU = txtObservacoesRVL.Text != "" ? txtObservacoesRVL.Text : null;
                }
            }
            else
            {
                tb052.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                tb052.TB25_EMPRESA = coEmp2 != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp2) : null;
                tb052.TB25_EMPRESA1 = coEmp3 != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp3) : null;
                tb052.TP_CADASTRO = "W";
                tb052.TP_RESERVA = ddlTipoReservaRVL.SelectedValue;
                tb052.NU_ANO_LETIVO = int.Parse(ddlAnoRVL.SelectedValue);
                tb052.NU_SEM_LETIVO = ddlSemestreRVL.SelectedValue;
                tb052.CO_PERI_TUR = ddlTurnoRVL.SelectedValue;
                tb052.CO_STATUS = ddlSituacaoRVL.SelectedValue;
                tb052.DE_OBS_RESERV_MATRI = txtObsReservaRVL.Text != "" ? txtObsReservaRVL.Text : null;
                tb052.TB57_MOTIVCANC = tb052.CO_STATUS == "C" ? TB57_MOTIVCANC.RetornaPelaChavePrimaria(int.Parse(ddlMotivoCancelRVL.Text)) : null;
                tb052.DT_STATUS = DateTime.Parse(txtDataStatusRVL.Text);
                tb052.DT_VALIDADE_RESERV = DateTime.Parse(txtDataValidadeRVL.Text);
            }

            

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                GestorEntities.SaveOrUpdate(tb052);

//------------> GERAÇÃO DO RELATÓRIO
//------------> Variáveis obrigatórias para gerar o Relatório
                string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
                int lRetorno;

//------------> Variáveis de parâmetro do Relatório
                string strP_CO_EMP, strP_NU_RESERVA;

                var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
                IRelatorioWeb lIRelatorioWeb;

                strIDSessao = Session.SessionID.ToString();
                strIdentFunc = WRAuxiliares.IdentFunc;
                strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
                strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprReserMatric");

//------------> Criação da Pasta
                if (!Directory.Exists(@strCaminhoRelatorioGerado))
                    Directory.CreateDirectory(@strCaminhoRelatorioGerado);

                strP_CO_EMP = ddlUnidadeSugeridaRVL.SelectedValue;
                strP_NU_RESERVA = string.Format("{0}.{1}.{2}", dataAtual.Year.ToString(), dataAtual.Month.ToString().PadLeft(2, '0'), proxRegistro.ToString().PadLeft(4, '0'));

                lIRelatorioWeb = varRelatorioWeb.CreateChannel();

                lRetorno = lIRelatorioWeb.RelComprReserMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_NU_RESERVA);

                string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
                Session["URLRelatorio"] = strURL;
                Session["ApresentaRelatorio"] = "1";

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Reserva efetuada com sucesso.");

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

                varRelatorioWeb.Close();
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                if (chkGeraCompro.Checked)
                {
                    GestorEntities.SaveOrUpdate(tb052);

//----------------> GERAÇÃO DO RELATÓRIO
//----------------> Variáveis obrigatórias para gerar o Relatório
                    string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
                    int lRetorno;

//----------------> Variáveis de parâmetro do Relatório
                    string strP_CO_EMP, strP_NU_RESERVA;

                    var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
                    IRelatorioWeb lIRelatorioWeb;

                    strIDSessao = Session.SessionID.ToString();
                    strIdentFunc = WRAuxiliares.IdentFunc;
                    strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
                    strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprReserMatric");

//----------------> Criação da Pasta
                    if (!Directory.Exists(@strCaminhoRelatorioGerado))
                        Directory.CreateDirectory(@strCaminhoRelatorioGerado);

                    strP_CO_EMP = ddlUnidadeSugeridaRVL.SelectedValue;
                    strP_NU_RESERVA = txtNumReservaRVL.Text;

                    lIRelatorioWeb = varRelatorioWeb.CreateChannel();

                    lRetorno = lIRelatorioWeb.RelComprReserMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_NU_RESERVA);

                    string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
                    Session["URLRelatorio"] = strURL;
                    Session["ApresentaRelatorio"] = "1";

                    AuxiliPagina.EnvioMensagemSucesso(this.Page, "Reserva efetuada com sucesso.");

                    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

                    varRelatorioWeb.Close();
                }
                else
                {
                    CurrentPadraoCadastros.CurrentEntity = tb052;
                }
            }
        }                
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB052_RESERV_MATRI tb052 = RetornaEntidade();

            if (tb052 != null)
            {
//------------> Bairro do Candidato
                tb052.TB905_BAIRROReference.Load();
//------------> Bairro do Responsável
                tb052.TB905_BAIRRO1Reference.Load();

                tb052.TB01_CURSOReference.Load();
                tb052.TB108_RESPONSAVELReference.Load();
                tb052.TB07_ALUNOReference.Load();
                tb052.TB25_EMPRESAReference.Load();
                tb052.TB25_EMPRESA1Reference.Load();                

//------------> Se o responsável já existe então traz o dados da tabela de responsáveis
                if (tb052.TB108_RESPONSAVEL == null)
                {
                    txtCpfResponsavelRVL.Text = tb052.NU_CPF_RESP;
                    txtNomeResponsavelRVL.Text = tb052.NO_RESP;
                    txtDataNascimentoResponsavelRVL.Text = tb052.DT_NASC_RESP != null ? tb052.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                    ddlEstadoCivilRVL.SelectedValue = tb052.CO_ESTADO_CIVIL_RESP;
                    txtNumRGRespRVL.Text = tb052.CO_RG_RESP != null ? tb052.CO_RG_RESP : "";
                    txtOrgaoRGRespRVL.Text = tb052.CO_ORG_RG_RESP != null ? tb052.CO_ORG_RG_RESP : "";
                    txtDtEmissaoRGRespRVL.Text = tb052.DT_EMIS_RG_RESP != null ? tb052.DT_EMIS_RG_RESP.ToString() : "";
                    ddlGrauParentescoRVL.SelectedValue = tb052.CO_GRAU_PARENT_RESP;
                    txtTelResidencialResponsavelRVL.Text = tb052.NU_TEL_RESP;
                    txtTelCelularResponsavelRVL.Text = tb052.NU_CEL_RESP;
                    txtEmailResponsavelRVL.Text = tb052.DE_EMAIL_RESP;
                    txtLogradouroResponsavelRVL.Text = tb052.DE_END_RESP;
                    txtComplementoResponsavelRVL.Text = tb052.DE_COM_END_RESP;
                    txtNumeroResponsavelRVL.Text = tb052.NU_END_RESP != null ? tb052.NU_END_RESP.ToString() : "";
                    ddlUfResponsavelRVL.SelectedValue = tb052.TB905_BAIRRO1.CO_UF;
                    CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
                    ddlCidadeResponsavelRVL.SelectedValue = tb052.TB905_BAIRRO1.CO_CIDADE.ToString();
                    CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
                    ddlBairroResponsavelRVL.SelectedValue = tb052.TB905_BAIRRO1.CO_BAIRRO.ToString();
                    txtCepResponsavelRVL.Text = tb052.CO_CEP_RESP;
                    txtEmpresaRespRVL.Text = tb052.NO_EMP_TRAB_RESP != null ? tb052.NO_EMP_TRAB_RESP : "";
                    txtNumEmpresaRespRVL.Text = tb052.NU_TEL_TRAB_RESP != null ? tb052.NU_TEL_TRAB_RESP : "";
                    txtFuncaoRespRVL.Text = tb052.NO_FUNC_TRAB_RESP != null ? tb052.NO_FUNC_TRAB_RESP : "";
                    txtEmailEmpRespRVL.Text = tb052.DE_EMAIL_TRAB_RESP != null ? tb052.DE_EMAIL_TRAB_RESP : "";
                }
                else
                {
                    txtCpfResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.NU_CPF_RESP;
                    txtNomeResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.NO_RESP;
                    txtDataNascimentoResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.DT_NASC_RESP != null ? tb052.TB108_RESPONSAVEL.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                    ddlEstadoCivilRVL.SelectedValue = tb052.TB108_RESPONSAVEL.CO_ESTADO_CIVIL_RESP != null ? tb052.TB108_RESPONSAVEL.CO_ESTADO_CIVIL_RESP : "";
                    txtNumRGRespRVL.Text = tb052.TB108_RESPONSAVEL.CO_RG_RESP != null ? tb052.TB108_RESPONSAVEL.CO_RG_RESP : "";
                    txtOrgaoRGRespRVL.Text = tb052.TB108_RESPONSAVEL.CO_ORG_RG_RESP != null ? tb052.TB108_RESPONSAVEL.CO_ORG_RG_RESP : "";
                    txtDtEmissaoRGRespRVL.Text = tb052.TB108_RESPONSAVEL.DT_EMIS_RG_RESP != null ? tb052.TB108_RESPONSAVEL.DT_EMIS_RG_RESP.Value.ToString("dd/MM/yyyy") : "";
                    txtTelResidencialResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.NU_TELE_RESI_RESP;
                    txtTelCelularResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.NU_TELE_CELU_RESP;
                    txtEmailResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.DES_EMAIL_RESP;
                    txtLogradouroResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.DE_ENDE_RESP;
                    txtComplementoResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.DE_COMP_RESP;
                    txtNumeroResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.NU_ENDE_RESP != null ? tb052.TB108_RESPONSAVEL.NU_ENDE_RESP.ToString() : "";
                    ddlUfResponsavelRVL.SelectedValue = tb052.TB108_RESPONSAVEL.CO_ESTA_RESP;
                    CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
                    ddlCidadeResponsavelRVL.SelectedValue = tb052.TB108_RESPONSAVEL.CO_CIDADE.ToString();
                    CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
                    ddlBairroResponsavelRVL.SelectedValue = tb052.TB108_RESPONSAVEL.CO_BAIRRO.ToString();
                    txtCepResponsavelRVL.Text = tb052.TB108_RESPONSAVEL.CO_CEP_RESP;
                    txtEmpresaRespRVL.Text = tb052.TB108_RESPONSAVEL.NO_EMPR_RESP != null ? tb052.TB108_RESPONSAVEL.NO_EMPR_RESP : "";
                    txtNumEmpresaRespRVL.Text = tb052.TB108_RESPONSAVEL.NU_TELE_COME_RESP != null ? tb052.TB108_RESPONSAVEL.NU_TELE_COME_RESP : "";
                    txtFuncaoRespRVL.Text = tb052.TB108_RESPONSAVEL.NO_FUNCAO_RESP != null ? tb052.TB108_RESPONSAVEL.NO_FUNCAO_RESP : "";
                    txtEmailEmpRespRVL.Text = tb052.TB108_RESPONSAVEL.DES_EMAIL_EMP != null ? tb052.TB108_RESPONSAVEL.DES_EMAIL_EMP : "";
                }

//------------> Se o aluno já existe então traz o dados da tabela de alunos
                if (tb052.TB07_ALUNO == null)
                {
                    txtNomeCandidatoRVL.Text = tb052.NO_ALU;
                    txtCpfCandidatoRVL.Text = tb052.NU_CPF_ALU;
                    txtNireRVL.Text = tb052.NU_NIRE_ALU != null ? tb052.NU_NIRE_ALU.ToString() : "";
                    txtNumRGAluRVL.Text = tb052.CO_RG_ALU != null ? tb052.CO_RG_ALU : "";
                    txtOrgaoRGAluRVL.Text = tb052.CO_ORG_RG_ALU != null ? tb052.CO_ORG_RG_ALU : "";
                    txtDtEmissRGAluRVL.Text = tb052.DT_EMIS_RG_ALU != null ? tb052.DT_EMIS_RG_ALU.Value.ToString("dd/MM/yyyy") : "";
                    ddlNacionalidadeCandidatoRVL.SelectedValue = tb052.NO_NACIO_ALU;
                    txtNomeMaeAlunoRVL.Text = tb052.NO_MAE_ALU != null ? tb052.NO_MAE_ALU : "";
                    ddlSexoCandidatoRVL.SelectedValue = tb052.CO_SEXO_ALU;
                    ddlDeficienciaCandidatoRVL.SelectedValue = tb052.TP_DEFIC_ALU;
                    txtNomePaiAlunoRVL.Text = tb052.NO_PAI_ALU != null ? tb052.NO_PAI_ALU : "";
                    txtDataNascimentoCandidatoRVL.Text = tb052.DT_NASC_ALU != null ? tb052.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                    txtLogradouroCandidatoRVL.Text = tb052.DE_END_ALU;
                    txtComplementoCandidatoRVL.Text = tb052.DE_COM_END_ALU;
                    txtNumeroCandidatoRVL.Text = tb052.NU_END_ALU != null ? tb052.NU_END_ALU.ToString() : "";
                    ddlUfCandidatoRVL.SelectedValue = tb052.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);
                    ddlCidadeCandidatoRVL.SelectedValue = tb052.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
                    ddlBairroCandidatoRVL.SelectedValue = tb052.TB905_BAIRRO.CO_BAIRRO.ToString();
                    txtCepCandidatoRVL.Text = tb052.CO_CEP_ALU;
                    txtObservacoesRVL.Text = tb052.DE_OBS_ALU != null ? tb052.DE_OBS_ALU : "";
                }
                else
                {
                    tb052.TB07_ALUNO.TB905_BAIRROReference.Load();

                    txtNomeCandidatoRVL.Text = tb052.TB07_ALUNO.NO_ALU;
                    txtCpfCandidatoRVL.Text = tb052.TB07_ALUNO.NU_CPF_ALU;
                    txtNireRVL.Text = tb052.TB07_ALUNO.NU_NIRE.ToString();
                    txtNumRGAluRVL.Text = tb052.TB07_ALUNO.CO_RG_ALU != null ? tb052.TB07_ALUNO.CO_RG_ALU : "";
                    txtOrgaoRGAluRVL.Text = tb052.TB07_ALUNO.CO_ORG_RG_ALU != null ? tb052.TB07_ALUNO.CO_ORG_RG_ALU : "";
                    txtDtEmissRGAluRVL.Text = tb052.TB07_ALUNO.DT_EMIS_RG_ALU != null ? tb052.TB07_ALUNO.DT_EMIS_RG_ALU.Value.ToString("dd/MM/yyyy") : "";
                    ddlNacionalidadeCandidatoRVL.SelectedValue = tb052.TB07_ALUNO.CO_NACI_ALU;
                    ddlSexoCandidatoRVL.SelectedValue = tb052.TB07_ALUNO.CO_SEXO_ALU;
                    ddlDeficienciaCandidatoRVL.SelectedValue = tb052.TB07_ALUNO.TP_DEF;
                    txtNomeMaeAlunoRVL.Text = tb052.TB07_ALUNO.NO_MAE_ALU != null ? tb052.TB07_ALUNO.NO_MAE_ALU : "";
                    txtNomePaiAlunoRVL.Text = tb052.TB07_ALUNO.NO_PAI_ALU != null ? tb052.TB07_ALUNO.NO_PAI_ALU : "";
                    txtDataNascimentoCandidatoRVL.Text = tb052.TB07_ALUNO.DT_NASC_ALU != null ? tb052.TB07_ALUNO.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                    txtLogradouroCandidatoRVL.Text = tb052.TB07_ALUNO.DE_ENDE_ALU;
                    txtComplementoCandidatoRVL.Text = tb052.TB07_ALUNO.DE_COMP_ALU;
                    txtNumeroCandidatoRVL.Text = tb052.TB07_ALUNO.NU_ENDE_ALU != null ? tb052.TB07_ALUNO.NU_ENDE_ALU.ToString() : "";
                    ddlUfCandidatoRVL.SelectedValue = tb052.TB07_ALUNO.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);
                    ddlCidadeCandidatoRVL.SelectedValue = tb052.TB07_ALUNO.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
                    ddlBairroCandidatoRVL.SelectedValue = tb052.TB07_ALUNO.TB905_BAIRRO.CO_BAIRRO.ToString();
                    txtCepCandidatoRVL.Text = tb052.TB07_ALUNO.CO_CEP_ALU;
                    txtObservacoesRVL.Text = tb052.TB07_ALUNO.DES_OBS_ALU != null ? tb052.TB07_ALUNO.DES_OBS_ALU : "";
                }

                ddlGrauParentescoRVL.SelectedValue = tb052.CO_GRAU_PARENT_RESP;
                ddlRendaFamiliarRVL.SelectedValue = tb052.VR_RENDA_FAMILIAR_RESP != null ? tb052.VR_RENDA_FAMILIAR_RESP : "4";
                txtQtdDepenRVL.Text = tb052.QTD_DEPEND != null ? tb052.QTD_DEPEND.ToString() : "0";
                ddlTipoReservaRVL.SelectedValue = tb052.TP_RESERVA;
                txtNumReservaRVL.Text = tb052.NU_RESERVA;
                ddlUnidadeSugeridaRVL.SelectedValue = tb052.TB01_CURSO.CO_EMP.ToString();
                CarregaUnidadeSugerida2();
                ddlUnidadeSugerida2RVL.SelectedValue = tb052.TB25_EMPRESA != null ? tb052.TB25_EMPRESA.CO_EMP.ToString() : "";
                CarregaUnidadeSugerida3();
                ddlUnidadeSugerida3RVL.SelectedValue = tb052.TB25_EMPRESA1 != null ? tb052.TB25_EMPRESA1.CO_EMP.ToString() : "";
                ddlModalidadeRVL.SelectedValue = tb052.TB01_CURSO.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCursoRVL.SelectedValue = tb052.TB01_CURSO.CO_CUR.ToString();
                ddlSemestreRVL.SelectedValue = tb052.NU_SEM_LETIVO;
                ddlAnoRVL.Items.Insert(0, new ListItem(tb052.NU_ANO_LETIVO.ToString(), tb052.NU_ANO_LETIVO.ToString()));
                ddlTurnoRVL.SelectedValue = tb052.CO_PERI_TUR;
                txtDataCadastroRVL.Text = tb052.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtDataValidadeRVL.Text = tb052.DT_VALIDADE_RESERV.ToString("dd/MM/yyyy");
                ddlSituacaoRVL.SelectedValue = tb052.CO_STATUS;
                txtDataStatusRVL.Text = tb052.DT_STATUS.ToString("dd/MM/yyyy");
                txtObsReservaRVL.Text = tb052.DE_OBS_RESERV_MATRI != null ? tb052.DE_OBS_RESERV_MATRI : "";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB052_RESERV_MATRI</returns>
        private TB052_RESERV_MATRI RetornaEntidade()
        {
            return TB052_RESERV_MATRI.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp));
        }

        /// <summary>
        /// Método que limpa campos do candidato a reserva de matrícula
        /// </summary>
        private void LimparDadosCandidato()
        {
            ddlDeficienciaCandidatoRVL.SelectedValue = "N";
            ddlNacionalidadeCandidatoRVL.SelectedValue = "B";
            chkEndAluRespRVL.Enabled = chkNovoAlunoRVL.Checked = true;
            ddlUfCandidatoRVL.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
            CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);
            CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);            
            ddlSexoCandidatoRVL.SelectedIndex = 0;
            txtNomeCandidatoRVL.Text = txtDataNascimentoCandidatoRVL.Text = txtNireRVL.Text = txtCpfCandidatoRVL.Text = txtNumRGAluRVL.Text = 
            txtOrgaoRGAluRVL.Text = txtDtEmissRGAluRVL.Text = txtNomeMaeAlunoRVL.Text = txtNomePaiAlunoRVL.Text = txtCepCandidatoRVL.Text = 
            txtLogradouroCandidatoRVL.Text = txtNumeroCandidatoRVL.Text = txtObservacoesRVL.Text = txtComplementoCandidatoRVL.Text = "";            
        }

        /// <summary>
        /// Método que limpa campos do responsável
        /// </summary>
        private void LimparDadosResponsavel()
        {
            ddlEstadoCivilRVL.SelectedIndex = ddlGrauParentescoRVL.SelectedIndex = 0;
            ddlUfResponsavelRVL.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
            CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
            ddlCidadeResponsavelRVL.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
            CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
            txtNomeResponsavelRVL.Text = txtDataNascimentoResponsavelRVL.Text = txtNumRGRespRVL.Text = txtOrgaoRGRespRVL.Text = txtDtEmissaoRGRespRVL.Text = 
            ddlRendaFamiliarRVL.SelectedValue = txtQtdDepenRVL.Text = txtTelResidencialResponsavelRVL.Text = txtTelCelularResponsavelRVL.Text = 
            txtEmailResponsavelRVL.Text = txtCepResponsavelRVL.Text = txtLogradouroResponsavelRVL.Text = txtNumeroResponsavelRVL.Text = 
            txtComplementoResponsavelRVL.Text = txtEmpresaRespRVL.Text = txtNumEmpresaRespRVL.Text = txtFuncaoRespRVL.Text = txtEmailEmpRespRVL.Text = "";
        }

        /// <summary>
        /// Método que habilita/desabilita campos do candidato a reserva de matrícula
        /// </summary>
        /// <param name="habilita">Boolean habilita</param>
        private void HabilitarCamposCandidato(bool habilita)
        {
            txtNomeCandidatoRVL.Enabled = chkNovoAlunoRVL.Enabled = txtDataNascimentoCandidatoRVL.Enabled = ddlSexoCandidatoRVL.Enabled = txtNireRVL.Enabled =
            txtCpfCandidatoRVL.Enabled = txtNumRGAluRVL.Enabled = txtOrgaoRGAluRVL.Enabled = txtDtEmissRGAluRVL.Enabled = ddlNacionalidadeCandidatoRVL.Enabled =
            txtNomeMaeAlunoRVL.Enabled = ddlDeficienciaCandidatoRVL.Enabled = txtNomePaiAlunoRVL.Enabled = txtCepCandidatoRVL.Enabled = chkEndAluRespRVL.Enabled =
            ddlUfCandidatoRVL.Enabled = ddlCidadeCandidatoRVL.Enabled = ddlBairroCandidatoRVL.Enabled = txtLogradouroCandidatoRVL.Enabled =
            txtNumeroCandidatoRVL.Enabled = txtComplementoCandidatoRVL.Enabled = txtObservacoesRVL.Enabled = habilita;
        }

        /// <summary>
        /// Método que habilita/desabilita campos do responsável
        /// </summary>
        /// <param name="habilita">Boolean habilita</param>
        private void HabilitarCamposResponsavel(bool habilita)
        {
            txtNomeResponsavelRVL.Enabled = txtDataNascimentoResponsavelRVL.Enabled = ddlEstadoCivilRVL.Enabled = txtNumRGRespRVL.Enabled = 
            txtOrgaoRGRespRVL.Enabled = txtDtEmissaoRGRespRVL.Enabled = ddlGrauParentescoRVL.Enabled = ddlRendaFamiliarRVL.Enabled = txtQtdDepenRVL.Enabled =
            txtTelResidencialResponsavelRVL.Enabled = txtTelCelularResponsavelRVL.Enabled = txtEmailResponsavelRVL.Enabled = txtCepResponsavelRVL.Enabled =
            ddlUfResponsavelRVL.Enabled = ddlCidadeResponsavelRVL.Enabled = ddlBairroResponsavelRVL.Enabled = txtLogradouroResponsavelRVL.Enabled =
            txtNumeroResponsavelRVL.Enabled = txtComplementoResponsavelRVL.Enabled = txtEmpresaRespRVL.Enabled =
            txtNumEmpresaRespRVL.Enabled = txtFuncaoRespRVL.Enabled = txtEmailEmpRespRVL.Enabled = habilita;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDown UF</param>
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
        /// <param name="ddlUf">DropDown UF</param>
        /// <param name="ddlCidade">DropDown cidade</param>
        private void CarregaCidades(DropDownList ddlUf, DropDownList ddlCidade)
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        /// <param name="ddlCidade">DropDown cidade</param>
        /// <param name="ddlBairro">DropDown bairro</param>
        private void CarregaBairros(DropDownList ddlCidade, DropDownList ddlBairro)
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;
                ddlBairro.Items.Clear();
                return;
            }
            else
            {
                ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataBind();

                if (ddlCidade.Enabled)
                    ddlBairro.Enabled = ddlBairro.Items.Count > 0;

                ddlBairro.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidadeRVL.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidadeRVL.DataTextField = "DE_MODU_CUR";
            ddlModalidadeRVL.DataValueField = "CO_MODU_CUR";
            ddlModalidadeRVL.DataBind();

            ddlModalidadeRVL.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidadeRVL.SelectedValue != "" ? int.Parse(ddlModalidadeRVL.SelectedValue) : 0;

            ddlSerieCursoRVL.DataSource = from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                          where tb01.CO_MODU_CUR == modalidade
                                          select new { tb01.CO_CUR, tb01.NO_CUR };

            ddlSerieCursoRVL.DataTextField = "NO_CUR";
            ddlSerieCursoRVL.DataValueField = "CO_CUR";
            ddlSerieCursoRVL.DataBind();

            ddlSerieCursoRVL.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Motivos de Cancelamento
        /// </summary>
        private void CarregaMotivosCancelamento()
        {
            ddlMotivoCancelRVL.DataSource = TB57_MOTIVCANC.RetornaTodosRegistros();

            ddlMotivoCancelRVL.DataTextField = "DE_MOTI_CANC";
            ddlMotivoCancelRVL.DataValueField = "CO_MOTI_CANC";
            ddlMotivoCancelRVL.DataBind();

            ddlMotivoCancelRVL.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidadeSugeridas()
        {
            ddlUnidadeSugeridaRVL.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeSugeridaRVL.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeSugeridaRVL.DataValueField = "CO_EMP";
            ddlUnidadeSugeridaRVL.DataBind();

            ddlUnidadeSugeridaRVL.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares Sugerida2
        /// </summary>
        private void CarregaUnidadeSugerida2()
        {
            int coEmpSuger1 = int.Parse(ddlUnidadeSugeridaRVL.SelectedValue);

            ddlUnidadeSugerida2RVL.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()                                                
                                                where tb25.CO_EMP != coEmpSuger1
                                                && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                                select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeSugerida2RVL.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeSugerida2RVL.DataValueField = "CO_EMP";
            ddlUnidadeSugerida2RVL.DataBind();

            ddlUnidadeSugerida2RVL.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares Sugerida3
        /// </summary>
        private void CarregaUnidadeSugerida3()
        {
            if (ddlUnidadeSugerida2RVL.SelectedValue != "")
            {
                int coEmpSuger1 = int.Parse(ddlUnidadeSugeridaRVL.SelectedValue);
                int coEmpSuger2 = int.Parse(ddlUnidadeSugerida2RVL.SelectedValue);

                ddlUnidadeSugerida3RVL.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                    where tb25.CO_EMP != coEmpSuger1 && tb25.CO_EMP != coEmpSuger2
                                                    && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                                    select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

                ddlUnidadeSugerida3RVL.DataTextField = "NO_FANTAS_EMP";
                ddlUnidadeSugerida3RVL.DataValueField = "CO_EMP";
                ddlUnidadeSugerida3RVL.DataBind();
            }

            ddlUnidadeSugerida3RVL.Items.Insert(0, new ListItem("Selecione", ""));
        }   
        #endregion

        #region Validadores

//====> Método que faz a validação do CPF do responsável
        protected void cvCPF_ServerValidate(object source, ServerValidateEventArgs e)
        {
            string cpf = e.Value.Replace(".", "").Replace("-", "");

            if (!AuxiliValidacao.ValidaCpf(cpf))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }
        #endregion

        protected void btnValidarCpfResponsavel_Click(object sender, EventArgs e)
        {
            string cpf = txtCpfResponsavelRVL.Text.Replace(".", "").Replace("-", "");

            if (!AuxiliValidacao.ValidaCpf(cpf))
            {
                LimparDadosResponsavel();
                HabilitarCamposResponsavel(false);
                LimparDadosCandidato();
                HabilitarCamposCandidato(false);

                ServerValidateEventArgs eArgs = new ServerValidateEventArgs("", false);
                AuxiliPagina.EnvioMensagemErro(this, "Informe um CPF válido");

                return;
            }

            tbResponsavel = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                             where tb108.NU_CPF_RESP == cpf && !tb108.NU_CPF_RESP.Equals("")
                             select tb108).FirstOrDefault();

            if (tbResponsavel != null)
            {
//-----------> Carrega os alunos do responsável selecionado
                var resultado  = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                  where tb07.TB108_RESPONSAVEL.CO_RESP == tbResponsavel.CO_RESP
                                  select new { tb07.CO_ALU, tb07.NO_ALU });

                LimparDadosCandidato();
                HabilitarCamposCandidato(true);

                if (resultado.Count() > 0)
                {
                    ddlCandidatoRVL.DataSource = resultado;
                    ddlCandidatoRVL.DataTextField = "NO_ALU";
                    ddlCandidatoRVL.DataValueField = "CO_ALU";
                    ddlCandidatoRVL.DataBind();

                    int coAluno = int.Parse(ddlCandidatoRVL.SelectedValue);

                    tbAluno = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.CO_ALU == coAluno
                               select tb07).FirstOrDefault();


                    txtCpfCandidatoRVL.Enabled = txtNomeCandidatoRVL.Enabled = txtNireRVL.Enabled = ddlNacionalidadeCandidatoRVL.Enabled = 
                    ddlSexoCandidatoRVL.Enabled = ddlDeficienciaCandidatoRVL.Enabled = txtNumRGAluRVL.Enabled = txtOrgaoRGAluRVL.Enabled =
                    txtDtEmissRGAluRVL.Enabled = txtNomeMaeAlunoRVL.Enabled = txtNomePaiAlunoRVL.Enabled = false;

                    if (tbAluno != null)
                    {
                        tbAluno.TB905_BAIRROReference.Load();

                        txtCpfCandidatoRVL.Text = tbAluno.NU_CPF_ALU;
                        txtNomeCandidatoRVL.Text = tbAluno.NO_ALU;
                        txtNireRVL.Text = tbAluno.NU_NIRE.ToString();
                        ddlNacionalidadeCandidatoRVL.SelectedValue = tbAluno.CO_NACI_ALU;
                        ddlSexoCandidatoRVL.SelectedValue = tbAluno.CO_SEXO_ALU;
                        ddlDeficienciaCandidatoRVL.SelectedValue = tbAluno.TP_DEF;
                        txtDataNascimentoCandidatoRVL.Text = tbAluno.DT_NASC_ALU != null ? tbAluno.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                        txtLogradouroCandidatoRVL.Text = tbAluno.DE_ENDE_ALU;
                        txtNomeMaeAlunoRVL.Text = tbAluno.NO_MAE_ALU != null ? tbAluno.NO_MAE_ALU : "";
                        txtNomePaiAlunoRVL.Text = tbAluno.NO_PAI_ALU != null ? tbAluno.NO_PAI_ALU : "";
                        txtObservacoesRVL.Text = tbAluno.DES_OBS_ALU != null ? tbAluno.DES_OBS_ALU : "";
                        txtNumeroCandidatoRVL.Text = tbAluno.NU_ENDE_ALU.ToString();
                        txtComplementoCandidatoRVL.Text = tbAluno.DE_COMP_ALU;
                        txtCepCandidatoRVL.Text = tbAluno.CO_CEP_ALU;
                        ddlUfCandidatoRVL.SelectedValue = tbAluno.TB905_BAIRRO.CO_UF;

                        ddlCidadeCandidatoRVL.DataSource = TB904_CIDADE.RetornaPeloUF(tbAluno.TB905_BAIRRO.CO_UF);
                        ddlCidadeCandidatoRVL.DataTextField = "NO_CIDADE";
                        ddlCidadeCandidatoRVL.DataValueField = "CO_CIDADE";
                        ddlCidadeCandidatoRVL.DataBind();

                        ddlCidadeCandidatoRVL.SelectedValue = tbAluno.TB905_BAIRRO.CO_CIDADE.ToString();

                        CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);

                        ddlBairroCandidatoRVL.SelectedValue = tbAluno.TB905_BAIRRO.CO_BAIRRO.ToString();

                        txtDataNascimentoCandidatoRVL.Enabled = txtLogradouroCandidatoRVL.Enabled = txtNumeroCandidatoRVL.Enabled = 
                        txtComplementoCandidatoRVL.Enabled = txtCepCandidatoRVL.Enabled = ddlUfCandidatoRVL.Enabled = 
                        ddlCidadeCandidatoRVL.Enabled = ddlBairroCandidatoRVL.Enabled = chkEndAluRespRVL.Enabled = txtObservacoesRVL.Enabled = false;
                    }

                    liCandidato.Visible = chkNovoAlunoRVL.Enabled = true;
                    liNomeCandidato.Visible = chkNovoAlunoRVL.Checked = false;                    
                }
                else
                {
                    liCandidato.Visible = chkNovoAlunoRVL.Enabled = false;
                    liNomeCandidato.Visible = chkNovoAlunoRVL.Checked = true;                    
                }

                txtNomeResponsavelRVL.Text = tbResponsavel.NO_RESP;
                txtDataNascimentoResponsavelRVL.Text = tbResponsavel.DT_NASC_RESP != null ? tbResponsavel.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                ddlEstadoCivilRVL.SelectedValue = tbResponsavel.CO_ESTADO_CIVIL_RESP;
                txtNumRGRespRVL.Text = tbResponsavel.CO_RG_RESP;
                txtOrgaoRGRespRVL.Text = tbResponsavel.CO_ORG_RG_RESP;
                txtDtEmissaoRGRespRVL.Text = tbResponsavel.DT_EMIS_RG_RESP != null ? tbResponsavel.DT_EMIS_RG_RESP.Value.ToString("dd/MM/yyyy") : "";
                ddlRendaFamiliarRVL.SelectedValue = ((tbResponsavel.RENDA_FAMILIAR_RESP == "X") || (tbResponsavel.RENDA_FAMILIAR_RESP == null) ? tbResponsavel.RENDA_FAMILIAR_RESP : "");
                txtLogradouroResponsavelRVL.Text = tbResponsavel.DE_ENDE_RESP;
                txtNumeroResponsavelRVL.Text = tbResponsavel.NU_ENDE_RESP.ToString();
                txtComplementoResponsavelRVL.Text = tbResponsavel.DE_COMP_RESP;
                txtCepResponsavelRVL.Text = tbResponsavel.CO_CEP_RESP;
                ddlUfResponsavelRVL.SelectedValue = tbResponsavel.CO_ESTA_RESP;
                CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
                ddlCidadeResponsavelRVL.SelectedValue = tbResponsavel.CO_CIDADE.ToString();
                CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
                ddlBairroResponsavelRVL.SelectedValue = tbResponsavel.CO_BAIRRO.ToString();
                txtTelResidencialResponsavelRVL.Text = tbResponsavel.NU_TELE_RESI_RESP;
                txtTelCelularResponsavelRVL.Text = tbResponsavel.NU_TELE_CELU_RESP;
                txtEmailResponsavelRVL.Text = tbResponsavel.DES_EMAIL_RESP;
                txtEmpresaRespRVL.Text = tbResponsavel.NO_EMPR_RESP;
                txtNumEmpresaRespRVL.Text = tbResponsavel.NU_TELE_COME_RESP;
                txtFuncaoRespRVL.Text = tbResponsavel.NO_FUNCAO_RESP;
                txtEmailEmpRespRVL.Text = tbResponsavel.DES_EMAIL_EMP;
                txtQtdDepenRVL.Text = ((tbResponsavel.QT_MAIOR_DEPEN_RESP != null ? tbResponsavel.QT_MAIOR_DEPEN_RESP : 0) + (tbResponsavel.QT_MENOR_DEPEN_RESP != null ? tbResponsavel.QT_MENOR_DEPEN_RESP : 0)).ToString();
                HabilitarCamposResponsavel(false);
                ddlGrauParentescoRVL.Enabled = ddlRendaFamiliarRVL.Enabled = txtQtdDepenRVL.Enabled = true;
            }
            else
            {
                LimparDadosResponsavel();
                HabilitarCamposResponsavel(true);
                LimparDadosCandidato();
                HabilitarCamposCandidato(true);
                chkNovoAlunoRVL.Checked = true;
                chkNovoAlunoRVL.Enabled = false;
            }
        }

        protected void ddlModalidadeRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSituacaoRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            liMotivoCancel.Visible = ddlSituacaoRVL.SelectedValue == "C";
        }

        protected void ddlCandidatoRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCandidatoRVL.SelectedValue == "")
            {
                HabilitarCamposCandidato(true);
                LimparDadosCandidato();
            }
            else
            {
//------------> Pega o ID do aluno e carrega informações do mesmo para preenchimento dos campos
                int coAlu = int.Parse(ddlCandidatoRVL.SelectedValue);

                tbAluno = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.CO_ALU == coAlu
                           select tb07).FirstOrDefault();


                txtCpfCandidatoRVL.Enabled = txtNomeCandidatoRVL.Enabled = txtNireRVL.Enabled = ddlNacionalidadeCandidatoRVL.Enabled = 
                ddlSexoCandidatoRVL.Enabled = ddlDeficienciaCandidatoRVL.Enabled = txtNumRGAluRVL.Enabled = txtOrgaoRGAluRVL.Enabled =
                txtDtEmissRGAluRVL.Enabled = txtNomeMaeAlunoRVL.Enabled = txtNomePaiAlunoRVL.Enabled = false;

                if (tbAluno != null)
                {
                    tbAluno.TB905_BAIRROReference.Load();

                    ddlGrauParentescoRVL.SelectedValue = tbAluno.CO_GRAU_PAREN_RESP;
                    txtCpfCandidatoRVL.Text = tbAluno.NU_CPF_ALU;
                    txtNomeCandidatoRVL.Text = tbAluno.NO_ALU;
                    txtNireRVL.Text = tbAluno.NU_NIRE.ToString();
                    ddlNacionalidadeCandidatoRVL.SelectedValue = tbAluno.CO_NACI_ALU;
                    ddlSexoCandidatoRVL.SelectedValue = tbAluno.CO_SEXO_ALU;
                    ddlDeficienciaCandidatoRVL.SelectedValue = tbAluno.TP_DEF;
                    txtDataNascimentoCandidatoRVL.Text = tbAluno.DT_NASC_ALU != null ? tbAluno.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                    txtObservacoesRVL.Text = tbAluno.DES_OBS_ALU != null ? tbAluno.DES_OBS_ALU : "";
                    txtLogradouroCandidatoRVL.Text = tbAluno.DE_ENDE_ALU;
                    txtNumeroCandidatoRVL.Text = tbAluno.NU_ENDE_ALU.ToString();
                    txtComplementoCandidatoRVL.Text = tbAluno.DE_COMP_ALU;
                    txtCepCandidatoRVL.Text = tbAluno.CO_CEP_ALU;
                    ddlUfCandidatoRVL.SelectedValue = tbAluno.TB905_BAIRRO.CO_UF;

                    CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);

                    ddlCidadeCandidatoRVL.SelectedValue = tbAluno.TB905_BAIRRO.CO_CIDADE.ToString();

                    CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);

                    ddlBairroCandidatoRVL.SelectedValue = tbAluno.TB905_BAIRRO.CO_BAIRRO.ToString();

                    txtDataNascimentoCandidatoRVL.Enabled = txtLogradouroCandidatoRVL.Enabled = txtNumeroCandidatoRVL.Enabled = 
                    txtComplementoCandidatoRVL.Enabled = txtCepCandidatoRVL.Enabled = ddlUfCandidatoRVL.Enabled = txtObservacoesRVL.Enabled =
                    ddlCidadeCandidatoRVL.Enabled = ddlBairroCandidatoRVL.Enabled = chkEndAluRespRVL.Enabled = false;
                }
            }
        }

        protected void chkNovoAlunoRVL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNovoAlunoRVL.Checked)
            {
                tbAluno = null;
                liCandidato.Visible = false;
                liNomeCandidato.Visible = true;
                LimparDadosCandidato();
                HabilitarCamposCandidato(true);
            }
            else
            {
                liCandidato.Visible = true;
                liNomeCandidato.Visible = false;
                ddlCandidatoRVL_SelectedIndexChanged(sender, e);
            }

        }

        protected void ddlUnidadeSugerida2RVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidadeSugerida3();        
        }

        protected void ddlUnidadeSugeridaRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidadeSugerida2();
            CarregaUnidadeSugerida3();
            CarregaSerieCurso();
        }

        protected void chkEndAluRespRVL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEndAluRespRVL.Checked)
            {
                txtCepCandidatoRVL.Text = txtCepResponsavelRVL.Text != "" ? txtCepResponsavelRVL.Text : "";
                txtCepCandidatoRVL.Enabled = false;
                if (ddlUfResponsavelRVL.SelectedValue != "")
                {
                    ddlUfCandidatoRVL.SelectedValue = ddlUfResponsavelRVL.SelectedValue;
                    ddlUfCandidatoRVL.Enabled = false;
                    CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);
                    if (ddlCidadeResponsavelRVL.SelectedValue != "")
                        ddlCidadeCandidatoRVL.SelectedValue = ddlCidadeResponsavelRVL.SelectedValue;
                    ddlCidadeCandidatoRVL.Enabled = false;
                    CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
                    if (ddlBairroResponsavelRVL.SelectedValue != "")
                        ddlBairroCandidatoRVL.SelectedValue = ddlBairroResponsavelRVL.SelectedValue;
                    ddlBairroCandidatoRVL.Enabled = false;
                }

                txtLogradouroCandidatoRVL.Text = txtLogradouroResponsavelRVL.Text != "" ? txtLogradouroResponsavelRVL.Text : "";
                txtLogradouroCandidatoRVL.Enabled = false;
                txtNumeroCandidatoRVL.Text = txtNumeroResponsavelRVL.Text != "" ? txtNumeroResponsavelRVL.Text : "";
                txtNumeroCandidatoRVL.Enabled = false;
                txtComplementoCandidatoRVL.Text = txtComplementoResponsavelRVL.Text != "" ? txtComplementoResponsavelRVL.Text : "";
                txtComplementoCandidatoRVL.Enabled = false;
            }
            else
            {
                txtCepCandidatoRVL.Text = txtLogradouroCandidatoRVL.Text = txtNumeroCandidatoRVL.Text = txtComplementoCandidatoRVL.Text = "";
                txtCepCandidatoRVL.Enabled = txtLogradouroCandidatoRVL.Enabled = txtNumeroCandidatoRVL.Enabled = txtComplementoCandidatoRVL.Enabled = true;
                
                ddlUfCandidatoRVL.Enabled = ddlCidadeCandidatoRVL.Enabled = ddlBairroCandidatoRVL.Enabled = true;

                ddlUfCandidatoRVL.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;                
                CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);                
                CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
            }
        }

//====> Preenche os campos de endereço do responsável de acordo com o CEP, se o mesmo possuir registro na base de dados
        protected void btnPesquisarCep_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepResponsavelRVL.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepResponsavelRVL.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where( c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroResponsavelRVL.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfResponsavelRVL.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
                    ddlCidadeResponsavelRVL.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
                    ddlBairroResponsavelRVL.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroResponsavelRVL.Text = ddlBairroResponsavelRVL.SelectedValue = 
                    ddlCidadeResponsavelRVL.SelectedValue = ddlUfResponsavelRVL.SelectedValue = "";
                }
            }
        }

//====> Preenche os campos de endereço do Aluno Candidato de acordo com o CEP, se o mesmo possuir registro na base de dados
        protected void btnPesqCEPCandid_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepCandidatoRVL.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepCandidatoRVL.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where( c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroCandidatoRVL.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfCandidatoRVL.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);
                    ddlCidadeCandidatoRVL.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
                    ddlBairroCandidatoRVL.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroCandidatoRVL.Text = ddlBairroCandidatoRVL.SelectedValue =
                    ddlCidadeCandidatoRVL.SelectedValue = ddlUfCandidatoRVL.SelectedValue = "";
                }
            }
        }

        protected void ddlUfResponsavelRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlUfResponsavelRVL, ddlCidadeResponsavelRVL);
            CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
        }

        protected void ddlCidadeResponsavelRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlCidadeResponsavelRVL, ddlBairroResponsavelRVL);
        }

        protected void ddlUfCandidatoRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlUfCandidatoRVL, ddlCidadeCandidatoRVL);
            CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
        }

        protected void ddlCidadeCandidatoRVL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlCidadeCandidatoRVL, ddlBairroCandidatoRVL);
        }
    }
}