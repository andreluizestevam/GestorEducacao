//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: MATRÍCULAS DE ALUNOS NOVOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 20/02/2014| Vinícius Reis              | Criação da matricula resumida com base
//           |                            | na matrícula completa.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 24/06/2014| Victor Martins Machado     | Inclusão das opções de informações adicionais.
//           |                            | 
//-----------+----------------------------+-------------------------------------------
//27/08/2014 | Maxwell Almeida            | Alteração das máscaras de dinheiro no financeiro
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;
using System.Configuration;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.MatriculaResumidaFinanc
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> tipoContrato = AuxiliBaseApoio.chave(tipoContratoCurso.ResourceManager);
        private Dictionary<string, string> tipoValor = AuxiliBaseApoio.chave(tipoValorCurso.ResourceManager);
        private Dictionary<string, string> tipoTurma = AuxiliBaseApoio.chave(tipoTurnoTurma.ResourceManager);

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //lnkFormPgto.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.Text='Aguarde, Enviando...'; this.disabled=true; " + this.GetPostBackEventReference(lnkFormPgto, string.Empty) + ";");
            //lnkEfetMatric.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.Text='Aguarde, Enviando...'; this.disabled=true; " + this.GetPostBackEventReference(lnkFormPgto, string.Empty) + ";");

            if (!base.IsPostBack)
            {
                //MontaGridGrdCurso();
                //MontaGridCursoExtra();

                carregaGridChequesPgto();
                CarregaBanco(ddlBcoPgto1);
                CarregaBanco(ddlBcoPgto2);
                CarregaBanco(ddlBcoPgto3);
                carregaCidadesRefeita();
                CarregaBairrosRefeito();
                VerificaExistenciaEscolaridade();

                if (LoginAuxili.ORG_CODIGO_ORGAO == 0 || LoginAuxili.ORG_CODIGO_ORGAO == null)
                {
                    string script = "window.parent.location = '{0}';";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", string.Format(script, "/logout.aspx"), true);

                    return;
                }

                if (txtNomeAluno.Text != "")
                {
                    //--------> Valida se o evento é de exibição do relatório gerado.
                    if (Session["ApresentaRelatorio"] != null)
                        if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                        {
                            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                            //----------------> Limpa a var de sessão com o url do relatório.
                            Session.Remove("URLRelatorio");
                            Session.Remove("ApresentaRelatorio");
                            //----------------> Limpa a ref da url utilizada para carregar o relatório.
                            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                            isreadonly.SetValue(this.Request.QueryString, false, null);
                            isreadonly.SetValue(this.Request.QueryString, true, null);
                        }
                }
                else
                {
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
                }

                DateTime dataIniMatric;
                DateTime dataFimMatric;

                TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                //------------> Faz a verificação para saber se o controle de datas é por Instituição ou Unidade, depois carrega informações do mesmo
                if (tb149.FLA_CTRL_DATA == TipoControle.I.ToString())
                {
                    dataIniMatric = Convert.ToDateTime(tb149.DT_INI_MAT);
                    dataFimMatric = Convert.ToDateTime(tb149.DT_FIM_MAT);
                }
                else if (tb149.FLA_CTRL_DATA == TipoControle.U.ToString())
                {
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb25.TB82_DTCT_EMPReference.Load();
                    dataIniMatric = Convert.ToDateTime(tb25.TB82_DTCT_EMP.DT_INI_MAT);
                    dataFimMatric = Convert.ToDateTime(tb25.TB82_DTCT_EMP.DT_FIM_MAT);
                }
                else
                    dataIniMatric = dataFimMatric = DateTime.Now;

                //------------> Variável que vai guardar se NIRE é automático ou não
                string strTipoNireAuto = "";

                if (tb149.FLA_CTRL_TIPO_ENSIN == TipoControle.I.ToString())
                {
                    strTipoNireAuto = tb149.FLA_GERA_NIRE_AUTO != null ? tb149.FLA_GERA_NIRE_AUTO : "";
                }
                else if (tb149.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString())
                {
                    //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
                    var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                where iTb25.CO_EMP == LoginAuxili.CO_EMP
                                select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

                    strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";
                }

                if (strTipoNireAuto != "")
                {
                    txtNireAluno.Enabled = strTipoNireAuto == "N";
                }
                else
                    txtNireAluno.Enabled = true;

                //------------> Faz a verificação para saber se Data de Matrícula está no período correto
                DateTime dataHoje = DateTime.Now;
                if ((dataIniMatric.Date > dataHoje.Date) || (dataFimMatric.Date < dataHoje.Date))
                    AuxiliPagina.RedirecionaParaNenhumaPagina("Não é possível efetuar Matrícula. Data de Matrícula está fora do Período determinado.", C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
                else
                {
                    if (txtNomeAluno.Text != "")
                    {
                        //--------> Valida se o evento é de exibição do relatório gerado.
                        if (Session["ApresentaRelatorio"] != null)
                            if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                            {
                                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                                //----------------> Limpa a var de sessão com o url do relatório.
                                Session.Remove("URLRelatorio");
                                Session.Remove("ApresentaRelatorio");
                                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                                isreadonly.SetValue(this.Request.QueryString, false, null);
                                isreadonly.SetValue(this.Request.QueryString, true, null);
                            }
                    }
                    else
                    {
                        Session.Remove("URLRelatorio");
                        Session.Remove("ApresentaRelatorio");
                    }

                    this.CarregaUfs(this.ddlUFETA);
                    CarregaTipoTelefone();
                    CarregaTipoEndereço();
                    this.CarregaUfs(this.ddlUFCEA);
                    this.CarregaUfs(this.ddlUfResp);
                    //ddlUfResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    this.CarregaUfs(this.ddlIdentidadeUFResp);
                    this.CarregaUfs(this.ddlUfNacionalidadeResp);
                    this.CarregaUfs(this.ddlUfEmpResp);
                    this.ddlUfEmpResp.Items.Insert(0, new ListItem("", null));
                    this.ddlUfEmpResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    this.ddlUFETA.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    this.CarregaCidades(this.ddlCidadeETA, this.ddlUFETA);
                    this.CarregaBairros(this.ddlBairroETA, this.ddlCidadeETA);
                    this.CarregaCidades(this.ddlCidadeEmpResp, this.ddlUfEmpResp);
                    this.CarregaCidades(this.ddlCidadeResp, this.ddlUfResp);
                    ddlCidadeResp.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
                    CarregaBairrosRefeito();
                    //this.CarregaBairros(this.ddlBairroResp, this.ddlCidadeResp);
                    this.CarregaGrausInstrucao();
                    this.CarregaCursosFormacao();
                    this.CarregaUfs(this.ddlUFAluno);
                    ddlUFAluno.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    this.CarregaCidades(this.ddlCidadeAluno, this.ddlUFAluno);
                    ddlCidadeAluno.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
                    this.CarregaBairros(this.ddlBairroAluno, this.ddlCidadeAluno);
                    this.CarregaUfs(this.ddlUfNacionalidadeAluno);
                    this.CarregaUnidadesMedidas();
                    this.CarregaBolsas();
                    this.CarregaBolsasAlt();
                    this.CarregaAtividade();
                    this.CarregaGrideSolicitacao();
                    this.ddlUFAluno.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    this.ddlUfNacionalidadeAluno.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    this.ddlCidadeAluno.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
                    this.CarregaNacionalidades();
                    this.ddlNacioResp.SelectedValue = "BR";
                    this.CarregaTipoContrato();
                    this.CarregaTipoValor();
                    this.CarregaDiasVencimento();
                    CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
                    CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
                    CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlContaContabilA);
                    CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
                    CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
                    CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlContaContabilB);
                    CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
                    CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
                    CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlContaContabilC);
                    CarregaHistoricos();
                    CarregaAgrupadores();
                    CarreagCursoExtra();
                }

                //-----------> Valida se o CheckBox Atualizar Financeira está checado, se for o caso, o formulário estará disponível
                if (!chkAtualiFinan.Checked)
                {
                    ddlTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        txtValorContratoCalc.Enabled =
                        ddlValorContratoCalc.Enabled =
                        ddlTipoContrato.Enabled =
                        chkTipoContrato.Enabled =
                        chkGeraTotalParce.Enabled =
                        txtQtdeParcelas.Enabled =
                        RequiredFieldValidator6.Enabled =
                        chkDataPrimeiraParcela.Enabled =
                        txtDtPrimeiraParcela.Enabled =
                        txtValorPrimParce.Enabled =
                        chkManterDesconto.Enabled =
                        ddlTpBolsaAlt.Enabled =
                        ddlBolsaAlunoAlt.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled =
                        ddlTipoDesctoMensa.Enabled =
                        txtQtdeMesesDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled =
                        ddlBoleto.Enabled =
                        txtValorContratoCalc.Enabled =
                        ddlDiaVecto.Enabled =
                        chkTipoContrato.Enabled = false;
                }
                else
                {
                    var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                                  where adUs.CodUsuario == LoginAuxili.CO_COL
                                  select new { adUs.FLA_ALT_BOL_ALU, adUs.FLA_ALT_BOL_ESPE_ALU, adUs.FLA_ALT_PARAM_MAT }).FirstOrDefault();

                    if (admUsu != null)
                    {
                        //-----------> Valida se o usuário possui permissão para alterar o desconto dado ao aluno.
                        if (admUsu.FLA_ALT_BOL_ALU == "S")
                        {
                            chkManterDesconto.Enabled = true;
                        }
                        else
                        {
                            chkManterDesconto.Enabled =
                            txtValorDescto.Enabled =
                            chkManterDescontoPerc.Enabled =
                            txtPeriodoIniDesconto.Enabled =
                            txtPeriodoFimDesconto.Enabled = false;
                        }

                        //-----------> Valida se o usuário possui permissão para alterar o desconto especial dado ao aluno.
                        if (admUsu.FLA_ALT_BOL_ESPE_ALU == "S")
                        {
                            ddlTipoDesctoMensa.Enabled =
                            txtDesctoMensa.Enabled = true;
                        }
                        else
                        {
                            ddlTipoDesctoMensa.Enabled =
                            txtDesctoMensa.Enabled =
                            txtMesIniDesconto.Enabled = false;
                        }

                        //-----------> Valida se o usuário possui permissão para alterar parâmetros da matrícula
                        if (admUsu.FLA_ALT_PARAM_MAT == "S")
                        {
                            chkTipoContrato.Enabled =
                            ddlTipoValorCurso.Enabled =
                            chkGeraTotalParce.Enabled =
                            chkDataPrimeiraParcela.Enabled = true;
                        }
                        else
                        {
                            chkTipoContrato.Enabled =
                            ddlTipoValorCurso.Enabled =
                            chkGeraTotalParce.Enabled =
                            txtValorContratoCalc.Enabled =
                            chkDataPrimeiraParcela.Enabled = false;
                        }
                    }
                    else
                    {
                        chkManterDesconto.Enabled =
                            ddlTpBolsaAlt.Enabled =
                            ddlBolsaAlunoAlt.Enabled =
                            txtValorDescto.Enabled =
                            chkManterDescontoPerc.Enabled =
                            txtPeriodoIniDesconto.Enabled =
                            txtPeriodoFimDesconto.Enabled = false;
                        ddlTipoDesctoMensa.Enabled =
                            txtQtdeMesesDesctoMensa.Enabled =
                            txtDesctoMensa.Enabled =
                            txtMesIniDesconto.Enabled =
                            chkTipoContrato.Enabled =
                            ddlTipoValorCurso.Enabled =
                            chkGeraTotalParce.Enabled =
                            txtValorContratoCalc.Enabled =
                            chkDataPrimeiraParcela.Enabled = false;
                    }
                }
                //this.ControleImagemResp.ImagemLargura = this.ControleImagemAluno.ImagemLargura = 64;
                //this.ControleImagemResp.ImagemAltura = this.ControleImagemAluno.ImagemAltura = 85;
            }
            else
            {
                verificaCamposSelecionados();

                if (txtNomeAluno.Text != "")
                {
                    //--------> Valida se o evento é de exibição do relatório gerado.
                    if (Session["ApresentaRelatorio"] != null)
                        if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                        {
                            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                            //----------------> Limpa a var de sessão com o url do relatório.
                            Session.Remove("URLRelatorio");
                            Session.Remove("ApresentaRelatorio");
                            //----------------> Limpa a ref da url utilizada para carregar o relatório.
                            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                            isreadonly.SetValue(this.Request.QueryString, false, null);
                            isreadonly.SetValue(this.Request.QueryString, true, null);
                        }
                }
                else
                {
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
                }

            }

            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue) && !chkAlterValorContr.Checked)
            {
                //-----------> Pega o Curso
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                if (varSer != null)
                {
                    string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                        ddlTipoContrato.SelectedValue,
                        turnoTurma,
                        varSer,
                        this.Page, false);
                    if (retornoValor == string.Empty)
                        return;
                    else
                        txtValorContratoCalc.Text = retornoValor;

                    atualizaParcela();
                }
            }

            if (txtDtPrimeiraParcela.Text == "")
            {
                txtDtPrimeiraParcela.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            controleCamposCurso();
            ControlaFormularioAluno(false);
            ControlaFormularioResponsavel(true);
            verificaNonoDigito();
        }

        private void ControlaFormularioResponsavel(bool habilitado)
        {
            txtNomeResp.Enabled = habilitado;
            ddlSexoResp.Enabled = habilitado;
            txtDtNascResp.Enabled = habilitado;
            ddlTpSangueResp.Enabled = habilitado;
            ddlStaTpSangueResp.Enabled = habilitado;
            ddlEstadoCivilResp.Enabled = habilitado;
            ddlNacioResp.Enabled = habilitado;
            txtNaturalidadeResp.Enabled = habilitado;
            ddlUfNacionalidadeResp.Enabled = habilitado;
            ddlGrauInstrucaoResp.Enabled = habilitado;
            txtMaeResp.Enabled = habilitado;
            txtNISResp.Enabled = habilitado;
            txtCPFRespDados.Enabled = habilitado;
            txtIdentidadeResp.Enabled = habilitado;
            txtDtEmissaoResp.Enabled = habilitado;
            txtOrgEmissorResp.Enabled = habilitado;
            ddlIdentidadeUFResp.Enabled = habilitado;
            txtEmailResp.Enabled = habilitado;
            txtTelCelularResp.Enabled = habilitado;
            txtTelCelularRespNonoDig.Enabled = habilitado;
            txtCepResp.Enabled = habilitado;
            btnPesqCEPR.Enabled = habilitado;
            ddlUfResp.Enabled = habilitado;
            ddlCidadeResp.Enabled = habilitado;
            ddlBairroResp.Enabled = habilitado;
            txtLogradouroResp.Enabled = habilitado;
            txtNumeroResp.Enabled = habilitado;
            txtComplementoResp.Enabled = habilitado;
            txtTelResidencialResp.Enabled = habilitado;
            ddlProfissaoResp.Enabled = habilitado;
            ddlTrabResp.Enabled = habilitado;
            txtMesAnoTrabResp.Enabled = habilitado;
            txtCepEmpresaResp.Enabled = habilitado;
            btnCEPEmp.Enabled = habilitado;
            ddlUfEmpResp.Enabled = habilitado;
            ddlCidadeEmpResp.Enabled = habilitado;
            txtTelEmpresaResp.Enabled = habilitado;
            txtObservacoesResp.Enabled = habilitado;
            txtNomeEmpresaResp.Enabled = habilitado;
            chkResponAluno.Enabled = habilitado;
            chkRespFunc.Enabled = habilitado;
            btnAddRespon.Enabled = habilitado;

        }

        private void ControlaFormularioAluno(bool habilitado)
        {
            //--------> Desabilita os campos do aluno
            txtNireAluno.Enabled = habilitado;
            txtNomeAluno.Enabled = habilitado;
            txtApelAluno.Enabled = habilitado;
            txtCpfAluno.Enabled = habilitado;
            ddlSexoAluno.Enabled = habilitado;
            txtDataNascimentoAluno.Enabled = habilitado;
            ddlNacionalidadeAluno.Enabled = habilitado;
            txtNaturalidadeAluno.Enabled = habilitado;
            ddlUfNacionalidadeAluno.Enabled = habilitado;
            ddlTpSangueAluno.Enabled = habilitado;
            ddlStaSangueAluno.Enabled = habilitado;
            ddlDeficienciaAluno.Enabled = habilitado;
            ddlTransporteEscolarAluno.Enabled = habilitado;
            ddlRotaA.Enabled = habilitado;
            ddlPasseEscolarAluno.Enabled = habilitado;
            ddlPermiSaidaAluno.Enabled = habilitado;
            txtTelResidencialAluno.Enabled = habilitado;
            txtEmailAluno.Enabled = habilitado;
            txtTelCelularAluno.Enabled = habilitado;
            txtTelCelularAlunoNoveDigit.Enabled = habilitado;
            txtNomeMaeAlunoValid.Enabled = habilitado;
            txtNomePaiAluno.Enabled = habilitado;
            txtOrgEmiRGAlu.Enabled = habilitado;
            txtRGAluno.Enabled = habilitado;
            txtCertNascAlu.Enabled = habilitado;
            txtCepAluno.Enabled = habilitado;
            btnPesqCEPA.Enabled = habilitado;
            ddlUFAluno.Enabled = habilitado;
            ddlCidadeAluno.Enabled = habilitado;
            ddlBairroAluno.Enabled = habilitado;
            txtLogradouroAluno.Enabled = habilitado;
            txtNumeroAluno.Enabled = habilitado;
            lnkAtualizaAlu.Enabled = habilitado;
        }

        #endregion

        #region Inclusões

        /// <summary>
        /// Método que faz a inclusão de um novo registro de telefone do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkIncTel_Click(object sender, EventArgs e)
        {
            ControlaTabs("TEA");
            //ControlaChecks(chkTelAddAlu);

            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = int.Parse(hdCodAluno.Value);
            string telefone = txtTelETA.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            int numDDD = int.Parse(telefone.Substring(0, 2));
            int numTel = int.Parse(telefone.Substring(2, 8));

            TB242_ALUNO_TELEFONE tb242 = new TB242_ALUNO_TELEFONE();
            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb242.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb242.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb242.TB239_TIPO_TELEFONE = TB239_TIPO_TELEFONE.RetornaPelaChavePrimaria(int.Parse(ddlTpTelef.SelectedValue));
            tb242.NO_CONTATO = txtNomeContETA.Text != "" ? txtNomeContETA.Text : null;
            tb242.DES_OBSERVACAO = txtObsETA.Text != "" ? txtObsETA.Text : null;
            tb242.NR_DDD = numDDD;
            tb242.NR_TELEFONE = numTel;
            tb242.CO_SITUACAO = "A";
            tb242.DT_SITUACAO = DateTime.Now;

            TB242_ALUNO_TELEFONE.SaveOrUpdate(tb242, true);

            CarregaGridTelefones(coAlu, refAluno.TB25_EMPRESA1.CO_EMP);
        }

        /// <summary>
        /// Método que faz a inclusão de um novo registro de endereço do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkIncEnd_Click(object sender, EventArgs e)
        {
            ControlaTabs("ENA");
            //ControlaChecks(chkEndAddAlu);

            decimal decimalRetorno;
            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = int.Parse(hdCodAluno.Value);

            TB241_ALUNO_ENDERECO tb241 = new TB241_ALUNO_ENDERECO();
            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb241.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb241.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb241.TB238_TIPO_ENDERECO = TB238_TIPO_ENDERECO.RetornaPelaChavePrimaria(int.Parse(ddlTpEnderETA.SelectedValue));
            tb241.CO_CEP = txtCepETA.Text.Replace("-", "");
            tb241.DS_ENDERECO = txtLograETA.Text;
            tb241.DS_COMPLEMENTO = txtCompETA.Text;
            tb241.NR_ENDERECO = decimal.TryParse(this.txtNumETA.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb241.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroETA.SelectedValue));
            tb241.CO_SITUACAO = "A";
            tb241.DT_SITUACAO = DateTime.Now;
            tb241.FL_PRINCIPAL = false;

            TB241_ALUNO_ENDERECO.SaveOrUpdate(tb241, true);

            CarregaGridEnderecos(coAlu, refAluno.TB25_EMPRESA1.CO_EMP);
        }

        /// <summary>
        /// Método que faz a inclusão de um novo registro de cuidados de saúde do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkIncCEA_Click(object sender, EventArgs e)
        {
            ControlaTabs("CEA");
            //ControlaChecks(chkCuiEspAlu);

            int intRetorno;
            DateTime dataRetorno;
            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = int.Parse(hdCodAluno.Value);

            TB293_CUIDAD_SAUDE tb293 = new TB293_CUIDAD_SAUDE();
            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb293.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb293.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb293.TP_CUIDADO_SAUDE = ddlTpCui.SelectedValue;
            tb293.TP_APLICAC_CUIDADO = ddlTpApli.SelectedValue;
            tb293.HR_APLICAC_CUIDADO = txtHrAplic.Text.Replace(":", "") != "" ? txtHrAplic.Text : null;
            tb293.NM_REMEDIO_CUIDADO = txtDescCEA.Text;
            tb293.DE_OBSERV_CUIDADO = txtObsCEA.Text != "" ? txtObsCEA.Text : null;
            tb293.DE_DOSE_REMEDIO_CUIDADO = int.TryParse(this.txtQtdeCEA.Text, out intRetorno) ? (int?)intRetorno : null;
            tb293.NM_MEDICO_CUIDADO = txtNomeMedCEA.Text != "" ? txtNomeMedCEA.Text : null;
            tb293.NR_CRM_MEDICO_CUIDADO = txtNumCRMCEA.Text != "" ? txtNumCRMCEA.Text : null;
            tb293.CO_UF_MEDICO = ddlUFCEA.SelectedValue != "" ? ddlUFCEA.SelectedValue : null;
            tb293.NR_TELEF_MEDICO = txtTelCEA.Text.Replace("(", "").Replace(")", "").Replace("-", "") != "" ? txtTelCEA.Text.Replace("(", "").Replace(")", "").Replace("-", "") : null;
            tb293.FL_RECEITA_CUIDADO = ddlRecCEA.SelectedValue;
            tb293.TB89_UNIDADES = ddlUniCEA.SelectedValue != "" ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUniCEA.SelectedValue)) : null;
            tb293.DT_RECEITA_INI = DateTime.TryParse(this.txtDataPeriodoIni.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb293.DT_RECEITA_FIM = DateTime.TryParse(this.txtDataPeriodoFim.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb293.DT_RECEITA_CUIDADO = DateTime.Now;
            tb293.CO_STATUS_MEDICAC = "A";

            TB293_CUIDAD_SAUDE.SaveOrUpdate(tb293, true);

            txtHrAplic.Text = txtDescCEA.Text = txtObsCEA.Text = txtQtdeCEA.Text = txtNomeMedCEA.Text =
            txtNumCRMCEA.Text = txtTelCEA.Text = txtDataPeriodoIni.Text = txtDataPeriodoFim.Text = "";
            ddlUFCEA.SelectedIndex = ddlRecCEA.SelectedIndex = ddlTpCui.SelectedIndex = ddlTpApli.SelectedIndex = 0;

            CarregaGridCudEsp(coAlu, refAluno.TB25_EMPRESA1.CO_EMP);
        }

        /// <summary>
        /// Método que faz a inclusão de um novo registro de restrição alimentar do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkIncRestAli_Click(object sender, EventArgs e)
        {
            ControlaTabs("RAD");
            //ControlaChecks(chkResAliAlu);

            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = int.Parse(hdCodAluno.Value);
            DateTime dataRetorno;

            TB294_RESTR_ALIMEN tb294 = new TB294_RESTR_ALIMEN();
            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb294.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb294.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb294.ID_REFER_GEDUC_RESTR_ALIMEN = txtCodRestri.Text != "" ? txtCodRestri.Text : null;
            tb294.TP_RESTR_ALIMEN = ddlTpRestri.SelectedValue;
            tb294.NM_RESTR_ALIMEN = txtDescRestri.Text;
            tb294.DE_ACAO_RESTR_ALIMEN = txtAcaoRestri.Text != "" ? txtAcaoRestri.Text : null;
            tb294.DT_INFORM_RESTR_ALIMEN = DateTime.Now;
            tb294.DT_INICIO_RESTR_ALIMEN = DateTime.Parse(this.txtDtIniRestri.Text);
            tb294.DT_TERMI_RESTR_ALIMEN = DateTime.TryParse(this.txtDtFimRestri.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb294.CO_GRAU_RESTR_ALIMEN = ddlGrauRestri.SelectedValue;

            TB294_RESTR_ALIMEN.SaveOrUpdate(tb294, true);

            txtCodRestri.Text = txtDescRestri.Text = txtAcaoRestri.Text = txtDtIniRestri.Text = txtDtFimRestri.Text = "";
            ddlTpRestri.SelectedIndex = ddlGrauRestri.SelectedIndex = 0;

            CarregaGridResAli(coAlu, refAluno.TB25_EMPRESA1.CO_EMP);
        }

        /// <summary>
        /// Método que faz a inclusão de um novo registro de atividade extra do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkIncAtiExt_Click(object sender, EventArgs e)
        {
            ControlaTabs("AEA");
            //ontrolaChecks(chkRegAtiExt);

            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = int.Parse(hdCodAluno.Value);
            Decimal decimalRetorno;
            DateTime dataRetorno;

            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

            TB106_ATIVEXTRA_ALUNO tb106 = new TB106_ATIVEXTRA_ALUNO();
            tb106.TB07_ALUNO = tb07;
            tb106.TB105_ATIVIDADES_EXTRAS = TB105_ATIVIDADES_EXTRAS.RetornaPelaChavePrimaria(int.Parse(ddlAtivExtra.SelectedValue));
            tb106.VL_ATIV_EXTRA = Decimal.TryParse(this.txtValorAEA.Text, out decimalRetorno) ? (Decimal?)decimalRetorno : null;
            tb106.DT_CAD_ATIV = DateTime.Now;
            tb106.DT_INI_ATIV = DateTime.TryParse(this.txtDtIniAEA.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb106.DT_VENC_ATIV = DateTime.TryParse(this.txtDtFimAEA.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.TB25_EMPRESA1Reference.Load();
            tb106.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb07.TB25_EMPRESA1.CO_EMP);

            TB106_ATIVEXTRA_ALUNO.SaveOrUpdate(tb106, true);

            ddlAtivExtra.SelectedIndex = 0;
            txtValorAEA.Text = txtDtIniAEA.Text = txtDtFimAEA.Text = "";

            CarregaGridAtivExtra(coAlu, tb07.TB25_EMPRESA1.CO_EMP);
        }

        /// <summary>
        /// Faz a Confirmação da modalidade, série e turma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkConfirModSerTur_Click(object sender, EventArgs e)
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPeloCoCur(int.Parse(ddlSerieCurso.SelectedValue));

            if (tb01 != null)
            {
                int coAlu = Convert.ToInt32(this.hdCodAluno.Value);
                if (tb01.CO_NIVEL_CUR == "I" || tb01.CO_NIVEL_CUR == "F"
                                || tb01.CO_NIVEL_CUR == "M")
                {
                    //----------------> Faz a verificação de matrícula de aluno de acordo com classificação da série                   
                    if (coAlu > 0)
                    {
                        var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                      join iTb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals iTb01.CO_CUR
                                      where lTb08.TB07_ALUNO.CO_ALU == coAlu && (lTb08.CO_SIT_MAT == "A" || lTb08.CO_SIT_MAT == "R")
                                      && (iTb01.CO_NIVEL_CUR == "I" || iTb01.CO_NIVEL_CUR == "F"
                                      || iTb01.CO_NIVEL_CUR == "M")
                                      select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                        if (ocoMat.Count() > 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno do Ensino Regular possui matrícula em aberto.");
                            return;
                        }
                    }
                }
                else
                {
                    string anoP = PreAuxili.proximoAnoMat<string>(txtAno.Text);
                    int turma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
                    var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  where lTb08.TB07_ALUNO.CO_ALU == coAlu && lTb08.CO_CUR == tb01.CO_CUR && lTb08.CO_SIT_MAT == "A"
                                  && lTb08.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR && lTb08.CO_ANO_MES_MAT.Contains(anoP)
                                  //&& lTb08.CO_TUR == turma
                                  select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                    if (ocoMat.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno já possui matrícula para ano, modalidade e série/curso informados.");
                        return;
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Série informada não existe.");
                return;
            }
            //txtQtdeParcelas.Text = tb01.NU_QUANT_MESES.ToString();

            //Código responsável por calcular a quantidade de meses restantes para o fim do ano e inserir na quantidade de parcelas, que já vem como proporcional meses
            int aux1 = 12 - DateTime.Now.Month;
            int aux2 = aux1 + 1;
            txtQtdeParcelas.Text = aux2.ToString();

            ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = txtAno.Enabled = ddlTipoValorCurso.Enabled = false;
            lblSucDadosMatr.Visible = true;

            CarregaGridDocumentos();

            lnkConfirModSerTur.Enabled = false;

            chkMenEscAlu.Checked = true;

            //if (TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).CO_FLAG_ENSIN_CURSO != "S")
            //    ControlaTabs("ENA");
            //else

            #region Grava Dados para a grade do aluno
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CO_MAT";
            dt.Columns.Add(dc);

            int coMat = 0;
            foreach (GridViewRow linha in grdGrdCurso.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    coMat = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCodMat")).Value);

                    dr = dt.NewRow();
                    dr["CO_MAT"] = coMat;
                    dt.Rows.Add(dr);
                }
            }

            coMat = 0;
            foreach (GridViewRow linha in grdCursoExt.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    coMat = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCodMat")).Value);

                    dr = dt.NewRow();
                    dr["CO_MAT"] = coMat;
                    dt.Rows.Add(dr);
                }
            }
            dt.AcceptChanges();
            Session["dtCoMat"] = dt;

            #endregion

            carregaValorTaxaMatricula();
            ControlaTabs("MEN");

            //AuxiliPagina.EnvioMensagemSucesso(this, "Modalidade, Série, Turma e Ano confirmado com sucesso.");

            int coEmpUnidCont = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            if (TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmpUnidCont).FL_INTEG_FINAN == "S")
            {
                CarregaBoletos();
                CarregaDiasVencimento();
                CarregaTipoContrato();
                CarregaTipoValor();
                chkMenEscAlu.Enabled = chkAtualiFinan.Checked = true;
            }
            else
            {
                chkMenEscAlu.Enabled = chkAtualiFinan.Checked = false;
            }

            CalculaValorPrimeiraParcela();
        }

        /// <summary>
        /// Faz a Inclusão/Alteração do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAtualizaAlu_Click(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(this.ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = this.hdCodAluno.Value != "" ? Convert.ToInt32(this.hdCodAluno.Value) : 0;
            decimal decimalRetorno;
            DateTime dataRetorno;
            int coImagem = this.ControleImagemAluno.GravaImagem();

            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

            if (tb07 == null)
            {
                //------------> Variável que guarda informações da instituição do usuário logado
                var tb000 = (from iTb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                             where iTb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { iTb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN, iTb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO }).First();

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
                    //----------------> Faz a verificação para saber se o NIRE é automático ou não
                    if (strTipoNireAuto == "N")
                    {
                        if (txtNireAluno.Text.Replace(".", "").Replace("-", "") == "")
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE deve ser informado.");
                            ControlaFormularioAluno(true);
                            ControlaFormularioResponsavel(false);
                            return;
                        }

                        int nuNire = int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", ""));

                        ///-------------------> Faz a verificação para saber se o NIRE informado já existe
                        var ocorrNIRE = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                         where lTb07.NU_NIRE == nuNire
                                         select new { lTb07.CO_ALU, lTb07.NO_ALU }).FirstOrDefault();


                        if (ocorrNIRE != null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE informado já existe para o(a) aluno(a) " + ocorrNIRE.NO_ALU + ".");
                            ControlaFormularioAluno(true);
                            ControlaFormularioResponsavel(false);
                            return;
                        }
                    }
                    else
                    {
                        ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                        int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                          select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                        if (lastCoAlu != null)
                        {
                            txtNireAluno.Text = (lastCoAlu.Value + 1).ToString();
                        }
                        else
                            txtNireAluno.Text = "1";
                    }
                }
                else
                {
                    if (txtNireAluno.Text.Replace(".", "").Replace("-", "") == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE deve ser informado.");
                        ControlaFormularioAluno(true);
                        ControlaFormularioResponsavel(false);
                        return;
                    }

                    int nuNire = int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", ""));

                    ///---------------> Faz a verificação para saber se o NIRE informado já existe
                    var ocorrNIRE = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                     where lTb07.NU_NIRE == nuNire
                                     select new { lTb07.CO_ALU, lTb07.NO_ALU }).FirstOrDefault();


                    if (ocorrNIRE != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Número do NIRE informado pertence ao aluno(a)" + ocorrNIRE.NO_ALU + ".");
                        ControlaFormularioAluno(true);
                        ControlaFormularioResponsavel(false);
                        return;
                    }
                }

                tb07 = new TB07_ALUNO();
                tb07.CO_EMP = coEmp;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                tb07.DT_SITU_ALU = new DateTime?(DateTime.Now);
                tb07.DT_CADA_ALU = new DateTime?(DateTime.Now);
                tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                tb07.NU_NIRE = int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", ""));
                tb07.FL_INCLU_ALU = true;
                tb07.FL_ALTER_ALU = false;

                tb07.CO_ORIGEM_ALU = "0";
                tb07.TP_RACA = "X";
                tb07.TP_CERTIDAO = "N";
                tb07.DE_CERT_LIVRO = "XXX";
                tb07.NU_CERT_FOLHA = "XXX";
                tb07.NO_CIDA_CARTORIO_ALU = "XXX";
                tb07.DE_CERT_CARTORIO = "XXX";
                tb07.RENDA_FAMILIAR = "6";
                tb07.CO_FLAG_MERENDA = "N";
            }
            else
            {
                tb07.TB25_EMPRESA1Reference.Load();

                if (tb07.TB25_EMPRESA1.CO_EMP != coEmp)
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                tb07.FL_ALTER_ALU = true;
            }

            int intRetorno = 0;

            tb07.CO_EMP_ORIGEM = coEmp;
            tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(coImagem);
            tb07.NO_ALU = this.txtNomeAluno.Text.ToUpper();
            tb07.NO_APE_ALU = this.txtApelAluno.Text != "" ? this.txtApelAluno.Text : null;
            tb07.CO_SEXO_ALU = this.ddlSexoAluno.SelectedValue != "" ? this.ddlSexoAluno.SelectedValue : null;
            tb07.DT_NASC_ALU = DateTime.TryParse(this.txtDataNascimentoAluno.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.CO_TIPO_SANGUE_ALU = this.ddlTpSangueAluno.SelectedValue != "" ? this.ddlTpSangueAluno.SelectedValue : null;
            tb07.CO_STATUS_SANGUE_ALU = this.ddlStaSangueAluno.SelectedValue != "" ? this.ddlStaSangueAluno.SelectedValue : null;
            tb07.TP_DEF = this.ddlDeficienciaAluno.SelectedValue;
            tb07.CO_NACI_ALU = this.ddlNacionalidadeAluno.SelectedValue != "" ? this.ddlNacionalidadeAluno.SelectedValue : null;
            tb07.DE_NATU_ALU = this.txtNaturalidadeAluno.Text;
            if (this.ddlNacionalidadeAluno.SelectedValue == Nacionalidade.B.ToString())
                tb07.CO_UF_NATU_ALU = this.ddlUfNacionalidadeAluno.SelectedValue != "" ? this.ddlUfNacionalidadeAluno.SelectedValue : null;

            if (LoginAuxili.FL_NONO_DIGITO_TELEF != "S")
                tb07.NU_TELE_CELU_ALU = this.txtTelCelularAluno.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") != "" ? this.txtTelCelularAluno.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null;
            else
                tb07.NU_TELE_CELU_ALU = this.txtTelCelularAlunoNoveDigit.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") != "" ? this.txtTelCelularAlunoNoveDigit.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null;

            tb07.NU_TELE_RESI_ALU = this.txtTelResidencialAluno.Text.Replace("(", "").Replace(")", "").Replace("-", "") != "" ? this.txtTelResidencialAluno.Text.Replace("(", "").Replace(")", "").Replace("-", "") : null;
            tb07.NO_WEB_ALU = this.txtEmailAluno.Text;
            tb07.NO_MAE_ALU = this.txtNomeMaeAlunoValid.Text;
            tb07.NO_PAI_ALU = this.txtNomePaiAluno.Text;
            tb07.CO_RG_ALU = this.txtRGAluno.Text;
            tb07.CO_ORG_RG_ALU = this.txtOrgEmiRGAlu.Text;
            tb07.NU_CERT = this.txtCertNascAlu.Text;
            tb07.CO_CEP_ALU = this.txtCepAluno.Text.Replace("-", "");
            tb07.CO_ESTA_ALU = this.ddlUFAluno.SelectedValue != "" ? this.ddlUFAluno.SelectedValue : null;
            tb07.DE_ENDE_ALU = this.txtLogradouroAluno.Text;
            tb07.NU_ENDE_ALU = Decimal.TryParse(this.txtNumeroAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            //tb07.DE_COMP_ALU = null;
            tb07.TB905_BAIRRO = int.TryParse(this.ddlBairroAluno.SelectedValue, out intRetorno) ? TB905_BAIRRO.RetornaPelaChavePrimaria(intRetorno) : null;
            tb07.NU_CPF_ALU = this.txtCpfAluno.Text.Replace("-", "").Replace(".", "") != "" ? this.txtCpfAluno.Text.Replace("-", "").Replace(".", "") : null;
            tb07.CO_INST = LoginAuxili.ORG_CODIGO_ORGAO;
            //if (chkDesctoPercBolsa.Checked)
            //{
            //    tb07.NU_PEC_DESBOL = decimal.TryParse(this.txtDescontoAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            //    tb07.NU_VAL_DESBOL = null;
            //}
            //else
            //{
            //    tb07.NU_PEC_DESBOL = null;
            //    tb07.NU_VAL_DESBOL = decimal.TryParse(this.txtDescontoAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            //}

            tb07.FLA_PASSE_ESCOLA = new bool?(bool.Parse(this.ddlPasseEscolarAluno.SelectedValue));
            tb07.FLA_TRANSP_ESCOLAR = this.ddlTransporteEscolarAluno.SelectedValue;
            tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(this.hdCodResp.Value));
            tb07.FL_AUTORI_SAIDA = ddlPermiSaidaAluno.SelectedValue;
            tb07.CO_SITU_ALU = "A";
            tb07.DT_ALT_REGISTRO = new DateTime?(DateTime.Now);
            tb07.FL_LIST_ESP = "N";

            TB07_ALUNO.SaveOrUpdate(tb07);
            GestorEntities.CurrentContext.SaveChanges();

            //txtNoInfAluno.Text = tb07.NO_ALU.ToUpper();
            //txtNumNIRE.Text = tb07.NU_NIRE.ToString();

            //var iTb07 = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
            //             where lTb07.NO_ALU == txtNomeAluno.Text && lTb07.NO_MAE_ALU == txtNomeMaeAlunoValid.Text
            //             select new { lTb07.CO_ALU, lTb07.CO_MODU_CUR, lTb07.CO_CUR, lTb07.CO_TUR }).FirstOrDefault();

            int nireAl = txtNireAluno.Text != "" ? int.Parse(txtNireAluno.Text) : 0;
            var iTb07 = (from tb7 in TB07_ALUNO.RetornaTodosRegistros()
                         where tb7.NU_NIRE == nireAl
                         select new { tb7.CO_ALU, tb7.CO_MODU_CUR, tb7.CO_CUR, tb7.CO_TUR }).FirstOrDefault();

            if (iTb07 != null)
            {

                //ddlTpBolsaAlt.SelectedValue = ddlTipoBolsa.SelectedValue;
                //CarregaBolsasAlt();
                //ddlBolsaAlunoAlt.SelectedValue = ddlBolsaAluno.SelectedValue;
                //chkManterDescontoPerc.Checked = chkDesctoPercBolsa.Checked;
                //txtValorDescto.Text = txtDescontoAluno.Text;
                //txtPeriodoIniDesconto.Text = txtPeriodoDeIniBolAluno.Text;
                //txtPeriodoFimDesconto.Text = txtPeriodoTerBolAluno.Text;

                this.hdCodAluno.Value = iTb07.CO_ALU.ToString();

                var iTb01 = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                             where tb01.CO_CUR == iTb07.CO_CUR
                             select new { tb01.CO_PREDEC_CUR, tb01.CO_PREDEC_MOD }).FirstOrDefault();

                if (iTb01 != null && iTb01.CO_PREDEC_CUR != null && iTb07.CO_TUR != null)
                {
                    var tb129 = (from iTb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                 where iTb129.CO_TUR == iTb07.CO_TUR
                                 select iTb129).FirstOrDefault();


                    CarregaModalidades();
                    try
                    {
                        ddlModalidade.SelectedValue = iTb01.CO_PREDEC_MOD.ToString();
                        CarregaSerieCurso();
                        ddlSerieCurso.SelectedValue = iTb01.CO_PREDEC_CUR.ToString();
                    }
                    catch (Exception)
                    {
                        CarregaSerieCurso();
                        ddlSerieCurso.SelectedIndex = 0;
                    }
                    CarregaTurma();
                    if (tb129 != null && tb129.CO_TUR_PROX_MATR != null)
                    {
                        try
                        {
                            ddlTurma.SelectedValue = tb129.CO_TUR_PROX_MATR.ToString();

                            if (this.ddlTurma.SelectedValue != "" && this.ddlSerieCurso.SelectedValue != "" && this.ddlModalidade.SelectedValue != "")
                            {
                                int coTur = int.Parse(this.ddlTurma.SelectedValue);
                                int coCur = int.Parse(this.ddlSerieCurso.SelectedValue);
                                int coModuCur = int.Parse(this.ddlModalidade.SelectedValue);

                                string turno = TB06_TURMAS.RetornaTodosRegistros().Where(p => p.CO_CUR.Equals(coCur) &&
                                                p.CO_MODU_CUR.Equals(coModuCur) && p.CO_TUR.Equals(coTur)
                                                ).FirstOrDefault().CO_PERI_TUR;

                                if (turno == "M")
                                    this.txtTurno.Text = "MANHÃ";
                                else if (turno == "N")
                                    this.txtTurno.Text = "NOITE";
                                else
                                    this.txtTurno.Text = "TARDE";
                            }
                        }
                        catch (Exception)
                        {
                            ddlTurma.SelectedIndex = 0;
                        }
                    }

                }
                else
                {
                    CarregaModalidades();
                    CarregaSerieCurso();
                    CarregaTurma();
                }

            }
            else
            {
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
            }

            //txtAno.Text = PreAuxili.anoPreMatricula().ToString();
            txtAno.Text = DateTime.Now.Year.ToString();

            if ((this.hdCodAluno.Value != "") && (this.txtNumReserva.Text != ""))
            {
                TB052_RESERV_MATRI tb052 = TB052_RESERV_MATRI.RetornaPelaChavePrimaria(this.txtNumReserva.Text, LoginAuxili.CO_EMP);
                tb052.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(this.hdCodAluno.Value));
                TB052_RESERV_MATRI.SaveOrUpdate(tb052, true);
            }

            int intAluno = int.Parse(hdCodAluno.Value);

            if ((this.hdCodAluno.Value != "") && (txtCepAluno.Text != ""))
            {
                string cepAlu = txtCepAluno.Text.Replace("-", "");

                TB241_ALUNO_ENDERECO tb241 = (TB241_ALUNO_ENDERECO.RetornaTodosRegistros().Where(a => a.TB07_ALUNO.CO_ALU == intAluno &&
                                              a.TB07_ALUNO.CO_EMP == coEmp && a.CO_CEP == cepAlu)).FirstOrDefault();
                if (tb241 == null)
                {
                    tb241 = new TB241_ALUNO_ENDERECO();
                    var refAluno = TB07_ALUNO.RetornaPeloCoAlu(intAluno);
                    tb241.TB07_ALUNO = refAluno;
                    refAluno.TB25_EMPRESA1Reference.Load();
                    tb241.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb241.TB238_TIPO_ENDERECO = (from tb238 in TB238_TIPO_ENDERECO.RetornaTodosRegistros()
                                                 where tb238.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb238.CO_TIPO_ENDERECO == "RES"
                                                 select tb238).FirstOrDefault();
                }

                tb241.DS_ENDERECO = txtLogradouroAluno.Text;
                tb241.NR_ENDERECO = decimal.TryParse(this.txtNumeroAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb241.DS_COMPLEMENTO = "";
                tb241.CO_CEP = cepAlu;
                tb241.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroAluno.SelectedValue));
                tb241.CO_SITUACAO = "A";
                tb241.DT_SITUACAO = DateTime.Now;
                tb241.FL_PRINCIPAL = true;

                TB241_ALUNO_ENDERECO.SaveOrUpdate(tb241, true);
            }

            if ((this.hdCodAluno.Value != "") && (txtTelResidencialAluno.Text.Replace("(", "").Replace(")", "").Replace("-", "") != ""))
            {
                string strTelefoneAluno = txtTelResidencialAluno.Text.Replace("(", "").Replace(")", "").Replace("-", "");
                int numDDD = int.Parse(strTelefoneAluno.Substring(0, 2));
                int numTel = int.Parse(strTelefoneAluno.Substring(2, 8));

                TB242_ALUNO_TELEFONE tb242 = (TB242_ALUNO_TELEFONE.RetornaTodosRegistros().Where(a => a.TB07_ALUNO.CO_ALU == intAluno &&
                                              a.TB07_ALUNO.CO_EMP == coEmp && a.NR_DDD == numDDD && a.NR_TELEFONE == numTel)).FirstOrDefault();
                if (tb242 == null)
                {
                    tb242 = new TB242_ALUNO_TELEFONE();
                    var refAluno = TB07_ALUNO.RetornaPeloCoAlu(intAluno);
                    tb242.TB07_ALUNO = refAluno;
                    refAluno.TB25_EMPRESA1Reference.Load();
                    tb242.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb242.TB239_TIPO_TELEFONE = (from tb239 in TB239_TIPO_TELEFONE.RetornaTodosRegistros()
                                                 where tb239.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb239.CO_TIPO_TELEFONE == "RES"
                                                 select tb239).FirstOrDefault();
                }

                tb242.NR_DDD = int.Parse(strTelefoneAluno.Substring(0, 2));
                tb242.NR_TELEFONE = int.Parse(strTelefoneAluno.Substring(2, 8));
                tb242.CO_SITUACAO = "A";
                tb242.DT_SITUACAO = DateTime.Now;

                TB242_ALUNO_TELEFONE.SaveOrUpdate(tb242, true);
            }

            // = txtNIREAluETA.Text = txtNI txtNREAluCEA.Text =IREAluRAA.Text = txtNIREAluTA.Text = txtNIREAluDoc.Text =
            txtNIREAluME.Text = txtNIREAluAEA.Text = txtNireAluno.Text.PadLeft(9, '0');
            txtNomeAluETA.Text = txtNomeAluCEA.Text = txtNomeAluRAA.Text = txtNomeAluDoc.Text =
            txtNomeAluME.Text = txtNomeAluTA.Text = txtNomeAluAEA.Text = txtNomeAluno.Text;
            txtNISAluETA.Text = txtNISAluCEA.Text = txtNISAluRAA.Text = txtNISAluTA.Text =
            txtNISAluDoc.Text = txtNISAluME.Text = txtNISAluAEA.Text;
            //this.txtNumNIRE.Enabled = this.btnPesqNIRE.Enabled = false;
            txtNoInfAluno.Text = txtNireAluno.Text.PadLeft(7, '0') + " - " + txtNomeAluno.Text;
            this.btnPesqNIRE.Enabled = false;
            //this.chkCadBasAlu.Checked = true;            
            //this.lblSucInfAlu.Visible = true;
            CarregaGridTelefones(intAluno, coEmp);
            CarregaGridEnderecos(intAluno, coEmp);
            CarregaGridCudEsp(intAluno, coEmp);
            CarregaGridResAli(intAluno, coEmp);
            CarregaGridAtivExtra(intAluno, coEmp);

            //--------> Desabilita o botão de pesquisa do aluno pelo NIRE
            lnkAtualizaAlu.Enabled = false;

            ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = txtAno.Enabled = ddlTipoValorCurso.Enabled = true;

            //ControlaTabs("ALU");
            MontaGridGrdCurso();
            montaGridCursoExtraVazia();
            //MontaGridCursoExtra();

            ControlaTabs("GRM");

            ControlaFormularioAluno(false);
            ControlaFormularioResponsavel(false);

            //AuxiliPagina.EnvioMensagemSucesso(this, "Aluno atualizado com sucesso.");
        }

        /// <summary>
        /// Faz a Inclusão/Alteração do Responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgAdd_Click(object sender, EventArgs e)
        {
            int intRetorno = 0;
            decimal decimalRetorno = 0;
            DateTime dataAtual = DateTime.Now;
            int coResp = this.hdCodResp.Value != "" ? Convert.ToInt32(this.hdCodResp.Value) : 0;

            TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            //------------> Variável que vai guardar se NIRE é automático ou não
            string strTipoNireAuto = "";

            if (tb149.FLA_CTRL_TIPO_ENSIN == TipoControle.I.ToString())
            {
                strTipoNireAuto = tb149.FLA_GERA_NIRE_AUTO != null ? tb149.FLA_GERA_NIRE_AUTO : "";
            }
            else if (tb149.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString())
            {
                //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
                var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                            where iTb25.CO_EMP == LoginAuxili.CO_EMP
                            select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

                strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";
            }

            if (!string.IsNullOrEmpty(strTipoNireAuto) && strTipoNireAuto == "N")
            {
                txtNireAluno.Enabled = false;
            }
            else
            {
                txtNireAluno.Enabled = true;
                var tb07 = TB07_ALUNO.RetornaTodosRegistros().OrderBy(s => s.NU_NIRE).Select(s => s.NU_NIRE).ToList();
                if (tb07.Count() > 0)
                {
                    var ultimoNire = tb07.ElementAt(tb07.Count() - 1);
                    txtNireAluno.Text = (ultimoNire + 1).ToString();
                }
            }

            TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);

            if (tb108 == null)
            {
                string strCpfResp = this.txtCPFRespDados.Text.Replace(".", "").Replace("-", "");

                //------------> Faz a verificação de ocorrência de responsável para o CPF informado ( quando inclusão )
                var ocorRespon = from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                 where lTb108.NU_CPF_RESP == strCpfResp
                                 select new { lTb108.CO_RESP };

                if (ocorRespon.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe responsável cadastrado com o CPF informado.");
                    return;
                }

                tb108 = new TB108_RESPONSAVEL();
                tb108.FL_INCLU_RESP = true;
                tb108.FL_ALTER_RESP = false;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb108.FL_NEGAT_CHEQUE = "N";
                tb108.FL_NEGAT_SERASA = "N";
                tb108.FL_NEGAT_SPC = "N";
            }
            else
            {
                string strCpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");

                //------------> Faz a verificação de ocorrência de responsável para o CPF informado ( quando alteração )
                var ocorRespon = from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                 where lTb108.NU_CPF_RESP == strCpfResp && lTb108.CO_RESP != coResp
                                 select new { lTb108.CO_RESP };

                if (ocorRespon.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe responsável cadastrado com o CPF informado.");
                    return;
                }

                tb108.FL_ALTER_RESP = true;
            }

            //tb108.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(coImagem);
            tb108.NO_RESP = this.txtNomeResp.Text.ToUpper();
            tb108.NU_NIS_RESP = decimal.TryParse(this.txtNISResp.Text.Trim(), out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb108.CO_SEXO_RESP = this.ddlSexoResp.SelectedValue;
            tb108.DT_NASC_RESP = this.txtDtNascResp.Text.Trim() != "" ? new DateTime?(DateTime.Parse(this.txtDtNascResp.Text)) : null;
            tb108.TP_DEF_RESP = "N";
            tb108.NU_PASSAPORTE_RESP = 0;
            tb108.CO_FLAG_RESP_FUNC = chkRespFunc.Checked ? "S" : "N";
            tb108.DES_EMAIL_RESP = this.txtEmailResp.Text.Trim() != "" ? this.txtEmailResp.Text : null;
            tb108.DE_NATU_RESP = this.txtNaturalidadeResp.Text.Trim() != "" ? this.txtNaturalidadeResp.Text.Trim() : null;
            tb108.CO_UF_NATU_RESP = this.ddlUfNacionalidadeResp.SelectedValue != "" ? this.ddlUfNacionalidadeResp.SelectedValue : null;
            tb108.CO_TIPO_SANGUE_RESP = this.ddlTpSangueResp.SelectedValue != "" ? this.ddlTpSangueResp.SelectedValue : null;
            tb108.CO_STATUS_SANGUE_RESP = this.ddlStaTpSangueResp.SelectedValue != "" ? this.ddlStaTpSangueResp.SelectedValue : null;
            tb108.CO_ESTADO_CIVIL_RESP = this.ddlEstadoCivilResp.SelectedValue != "" ? this.ddlEstadoCivilResp.SelectedValue : null;
            tb108.CO_NACI_RESP = this.ddlNacioResp.SelectedValue;

            if (LoginAuxili.FL_NONO_DIGITO_TELEF != "S")
                tb108.NU_TELE_CELU_RESP = this.txtTelCelularResp.Text.Trim() != "" ? this.txtTelCelularResp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            else
                tb108.NU_TELE_CELU_RESP = this.txtTelCelularRespNonoDig.Text.Trim() != "" ? this.txtTelCelularRespNonoDig.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;

            tb108.NU_TELE_RESI_RESP = this.txtTelResidencialResp.Text.Trim() != "" ? this.txtTelResidencialResp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb108.NU_CPF_RESP = this.txtCPFRespDados.Text.Replace(".", "").Replace("-", "");
            tb108.CO_RG_RESP = this.txtIdentidadeResp.Text;
            tb108.DT_EMIS_RG_RESP = this.txtDtEmissaoResp.Text.Trim() != "" ? new DateTime?(DateTime.Parse(this.txtDtEmissaoResp.Text)) : null;
            tb108.CO_ORG_RG_RESP = this.txtOrgEmissorResp.Text;
            tb108.CO_ESTA_RG_RESP = this.ddlIdentidadeUFResp.SelectedValue;
            tb108.NU_TIT_ELE = "X";
            tb108.NU_ZONA_ELE = "X";
            tb108.NU_SEC_ELE = "X";
            tb108.CO_UF_TIT_ELE_RESP = "0";
            tb108.NO_MAE_RESP = this.txtMaeResp.Text;
            tb108.NO_PAI_RESP = "X";
            tb108.DE_ENDE_RESP = this.txtLogradouroResp.Text;
            tb108.NU_ENDE_RESP = decimal.TryParse(this.txtNumeroResp.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb108.DE_COMP_RESP = this.txtComplementoResp.Text.Trim() != "" ? this.txtComplementoResp.Text : null;
            tb108.CO_CIDADE = int.TryParse(ddlCidadeResp.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb108.CO_BAIRRO = int.TryParse(this.ddlBairroResp.SelectedValue, out intRetorno) ? (int?)(intRetorno) : null;
            tb108.CO_ESTA_RESP = this.ddlUfResp.SelectedValue;
            tb108.CO_CEP_RESP = this.txtCepResp.Text.Replace("-", "");
            tb108.QT_MENOR_DEPEN_RESP = null;
            tb108.QT_MAIOR_DEPEN_RESP = null;
            tb108.CO_TIPO_RESI = "X";
            tb108.QT_ANOS_RESI = null;
            tb108.RENDA_FAMILIAR_RESP = "X";
            tb108.CO_INST = int.Parse(this.ddlGrauInstrucaoResp.SelectedValue);
            tb108.CO_FLA_TRABALHO = this.ddlTrabResp.SelectedValue;
            tb108.CO_MESANO_TRABALHO = this.txtMesAnoTrabResp.Text != "" ? (this.txtMesAnoTrabResp.Text.Substring(3, 4) + "/" + this.txtMesAnoTrabResp.Text.Substring(0, 2)) : null;
            tb108.TB15_FUNCAO = this.ddlProfissaoResp.SelectedValue != "" ? TB15_FUNCAO.RetornaPelaChavePrimaria(int.Parse(this.ddlProfissaoResp.SelectedValue)) : null;
            tb108.DE_ENDE_EMPRE_RESP = null;
            tb108.NU_ENDE_EMPRE_RESP = null;
            tb108.DE_COMP_EMPRE_RESP = null;
            tb108.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.TryParse(this.ddlCidadeEmpResp.SelectedValue, out intRetorno) ? intRetorno : 0);
            tb108.CO_BAIRRO_EMPRE_RESP = null;
            tb108.NO_EMPR_RESP = txtNomeEmpresaResp.Text;
            tb108.TB74_UF = TB74_UF.RetornaPelaChavePrimaria(this.ddlUfEmpResp.SelectedValue);
            tb108.CO_CEP_EMPRE_RESP = this.txtCepEmpresaResp.Text.Trim() != "" ? this.txtCepEmpresaResp.Text.Replace("-", "") : null;
            tb108.NU_TELE_COME_RESP = this.txtTelEmpresaResp.Text.Trim() != "" ? this.txtTelEmpresaResp.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
            tb108.CO_ORIGEM_RESP = "0";
            tb108.DT_ALT_REGISTRO = new DateTime?(dataAtual);
            TB108_RESPONSAVEL.SaveOrUpdate(tb108, true);

            if (this.hdCodResp.Value == "")
            {
                this.hdCodResp.Value = (from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                        where lTb108.NO_RESP == txtNomeResp.Text && lTb108.NU_CPF_RESP == txtCPFRespDados.Text.Replace(".", "").Replace("-", "")
                                        select new { lTb108.CO_RESP }).FirstOrDefault().CO_RESP.ToString();

                txtCPFResp.Text = txtCPFRespDados.Text;
                txtNoRespCPF.Text = txtNomeResp.Text.ToUpper();
            }
            else if (this.hdCodResp.Value != "")
            {
                coResp = Convert.ToInt32(this.hdCodResp.Value);

                if (this.txtNumReserva.Text != "")
                {
                    TB052_RESERV_MATRI reserva = TB052_RESERV_MATRI.RetornaPelaChavePrimaria(this.txtNumReserva.Text, LoginAuxili.CO_EMP);
                    reserva.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);
                    TB052_RESERV_MATRI.SaveOrUpdate(reserva, true);
                }
            }

            //--------> Atualiza os textboxs com o nome do responsável
            //txtNoRespCPF.Text = txtResponsavelAluno.Text = txtNomeResp.Text;

            this.txtCPFResp.Enabled = this.btnCPFResp.Enabled = this.chkRecResResp.Enabled = false;
            this.lblSucInfResp.Visible = true;
            if (ddlSituMatAluno.SelectedValue == "S")
            {
                btnPesqNIRE.Enabled = true;
            }

            if (chkResponAluno.Checked)
            {
                string cpfResp = this.txtCPFRespDados.Text.Replace(".", "").Replace("-", "");
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(this.ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

                TB07_ALUNO tb07 = (from iTB07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where iTB07.NU_CPF_ALU == cpfResp
                                   select iTB07).FirstOrDefault();

                if (tb07 == null)
                {
                    tb07 = new TB07_ALUNO();
                    tb07.CO_EMP = coEmp;
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb07.DT_SITU_ALU = new DateTime?(DateTime.Now);
                    tb07.DT_CADA_ALU = new DateTime?(DateTime.Now);
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    tb07.FL_INCLU_ALU = true;
                    tb07.FL_ALTER_ALU = false;

                    ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                    int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                    if (lastCoAlu != null)
                    {
                        txtNomeAluno.Text = txtNomeResp.Text.ToUpper();
                        txtNireAluno.Text = (lastCoAlu.Value + 1).ToString();
                        tb07.NU_NIRE = lastCoAlu.Value + 1;
                    }
                    else
                    {
                        txtNomeAluno.Text = "";
                        txtNireAluno.Text = "1";
                        tb07.NU_NIRE = 1;
                    }
                }
                else
                {
                    tb07.TB25_EMPRESA1Reference.Load();

                    if (tb07.TB25_EMPRESA1.CO_EMP != coEmp)
                        tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    tb07.FL_ALTER_ALU = true;
                    txtNireAluno.Text = tb07.NU_NIRE.ToString();
                    txtNomeAluno.Text = tb07.NO_ALU.ToUpper();
                }

                //tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(coImagem);
                tb07.NO_ALU = this.txtNomeResp.Text;
                //tb07.NO_APE_ALU = this.txtApelR.Text != "" ? this.txtApelAluno.Text : null;
                tb07.CO_ORIGEM_ALU = "0";
                //tb07.NU_NIS = decimal.TryParse(this.txtNisAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb07.CO_SEXO_ALU = this.ddlSexoResp.SelectedValue != "" ? this.ddlSexoResp.SelectedValue : null;
                tb07.DT_NASC_ALU = this.txtDtNascResp.Text.Trim() != "" ? new DateTime?(DateTime.Parse(this.txtDtNascResp.Text)) : null;
                tb07.CO_TIPO_SANGUE_ALU = this.ddlTpSangueResp.SelectedValue != "" ? this.ddlTpSangueResp.SelectedValue : null;
                tb07.CO_STATUS_SANGUE_ALU = this.ddlStaTpSangueResp.SelectedValue != "" ? this.ddlStaTpSangueResp.SelectedValue : null;
                tb07.TP_RACA = "X";
                tb07.TP_DEF = this.ddlDeficienciaAluno.SelectedValue;
                tb07.CO_ESTADO_CIVIL = this.ddlEstadoCivilResp.SelectedValue != "" ? this.ddlEstadoCivilResp.SelectedValue : null;
                tb07.CO_NACI_ALU = ddlNacioResp.SelectedValue == "BR" ? "B" : "E";
                tb07.DE_NATU_ALU = this.txtNaturalidadeResp.Text;
                if (this.ddlNacioResp.SelectedValue == "BR")
                    tb07.CO_UF_NATU_ALU = this.ddlUfNacionalidadeResp.SelectedValue != "" ? this.ddlUfNacionalidadeResp.SelectedValue : null;

                //verifica se a empresa do funcionário logado possui 9 dígito em seu cadastro
                if (LoginAuxili.FL_NONO_DIGITO_TELEF != "S")
                    tb07.NU_TELE_CELU_ALU = this.txtTelCelularResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") != "" ? this.txtTelCelularResp.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : null;
                else
                    tb07.NU_TELE_CELU_ALU = this.txtTelCelularRespNonoDig.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") != "" ? this.txtTelCelularRespNonoDig.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : null;

                tb07.NU_TELE_RESI_ALU = this.txtTelResidencialResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") != "" ? this.txtTelResidencialResp.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : null;
                tb07.CO_FLAG_PAIS_MORAM_JUNTOS = "N";
                tb07.CO_FLAG_MORA_PAIS = "N";
                tb07.NO_WEB_ALU = this.txtEmailResp.Text;
                tb07.NO_MAE_ALU = this.txtMaeResp.Text != "" ? this.txtMaeResp.Text : "XXXXX";
                tb07.NO_PAI_ALU = "X";
                tb07.CO_CEP_ALU = this.txtCepResp.Text.Replace("-", "");
                tb07.CO_ESTA_ALU = this.ddlUfResp.SelectedValue != "" ? this.ddlUfResp.SelectedValue : null;
                tb07.DE_ENDE_ALU = this.txtLogradouroResp.Text;
                tb07.NU_ENDE_ALU = Decimal.TryParse(this.txtNumeroResp.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb07.DE_COMP_ALU = this.txtComplementoResp.Text != "" ? this.txtComplementoResp.Text : null;
                tb07.TB905_BAIRRO = int.TryParse(this.ddlBairroResp.SelectedValue, out intRetorno) ? TB905_BAIRRO.RetornaPelaChavePrimaria(intRetorno) : null;
                tb07.TP_CERTIDAO = "N";
                tb07.NU_CERT = "XX";
                tb07.DE_CERT_LIVRO = "XX";
                tb07.NU_CERT_FOLHA = "XX";
                tb07.NO_CIDA_CARTORIO_ALU = "XXXXX";
                tb07.CO_UF_CARTORIO = LoginAuxili.CO_UF_INSTITUICAO;
                tb07.DE_CERT_CARTORIO = "XXXXX";
                tb07.CO_RG_ALU = this.txtIdentidadeResp.Text;
                tb07.DT_EMIS_RG_ALU = this.txtDtEmissaoResp.Text.Trim() != "" ? new DateTime?(DateTime.Parse(this.txtDtEmissaoResp.Text)) : null;
                tb07.CO_ORG_RG_ALU = this.txtOrgEmissorResp.Text;
                tb07.CO_ESTA_RG_ALU = this.ddlIdentidadeUFResp.SelectedValue != "" ? this.ddlIdentidadeUFResp.SelectedValue : null;
                tb07.NU_CPF_ALU = cpfResp != "" ? cpfResp : null;
                //tb07.NU_CARTAO_SAUDE_ALU = decimal.TryParse(this.txtCartaoSaudeAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                //tb07.NU_CARTAO_VACINA_ALU = decimal.TryParse(this.txtCartaoVacinAluno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb07.NU_TIT_ELE = "X";
                tb07.NU_ZONA_ELE = "X";
                tb07.NU_SEC_ELE = "X";
                tb07.CO_UF_TIT_ELE = "0";
                tb07.RENDA_FAMILIAR = "6";
                //tb07.FLA_PASSE_ESCOLA = null;
                //tb07.FLA_TRANSP_ESCOLAR = null;
                //tb07.CO_FLAG_MERENDA = this.ddlMerendAluno.SelectedValue;           
                tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(this.hdCodResp.Value));
                tb07.FL_AUTORI_SAIDA = "S";
                tb07.CO_SITU_ALU = "A";
                tb07.CO_INST = LoginAuxili.ORG_CODIGO_ORGAO;
                tb07.DT_ALT_REGISTRO = new DateTime?(DateTime.Now);
                tb07.CO_EMP_ORIGEM = LoginAuxili.CO_EMP;
                tb07.FL_LIST_ESP = "N";
                TB07_ALUNO.SaveOrUpdate(tb07);

                GestorEntities.CurrentContext.SaveChanges();

                var iTb07 = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                             where lTb07.NU_CPF_ALU == cpfResp
                             select new { lTb07.CO_ALU }).FirstOrDefault();

                if (iTb07 != null)
                {
                    this.hdCodAluno.Value = tb07.CO_ALU.ToString();

                    CarregaModalidades();
                    CarregaSerieCurso();
                    CarregaTurma();

                    txtAno.Text = PreAuxili.anoPreMatricula().ToString();

                    int intAluno = int.Parse(hdCodAluno.Value);

                    if ((this.hdCodAluno.Value != "") && (txtCepResp.Text != ""))
                    {
                        string cepAlu = txtCepResp.Text.Replace("-", "");

                        TB241_ALUNO_ENDERECO tb241 = (TB241_ALUNO_ENDERECO.RetornaTodosRegistros().Where(a => a.TB07_ALUNO.CO_ALU == intAluno &&
                                                      a.TB07_ALUNO.CO_EMP == coEmp && a.CO_CEP == cepAlu)).FirstOrDefault();
                        if (tb241 == null)
                        {
                            tb241 = new TB241_ALUNO_ENDERECO();
                            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(intAluno);
                            tb241.TB07_ALUNO = refAluno;
                            refAluno.TB25_EMPRESA1Reference.Load();
                            tb241.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                            tb241.TB238_TIPO_ENDERECO = (from tb238 in TB238_TIPO_ENDERECO.RetornaTodosRegistros()
                                                         where tb238.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb238.CO_TIPO_ENDERECO == "RES"
                                                         select tb238).FirstOrDefault();
                        }

                        tb241.DS_ENDERECO = txtLogradouroResp.Text;
                        tb241.NR_ENDERECO = decimal.TryParse(this.txtNumeroResp.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                        tb241.DS_COMPLEMENTO = txtComplementoResp.Text;
                        tb241.CO_CEP = cepAlu;
                        tb241.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroResp.SelectedValue));
                        tb241.CO_SITUACAO = "A";
                        tb241.DT_SITUACAO = DateTime.Now;
                        tb241.FL_PRINCIPAL = true;

                        TB241_ALUNO_ENDERECO.SaveOrUpdate(tb241, true);
                    }

                    if ((this.hdCodAluno.Value != "") && (txtTelResidencialResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") != ""))
                    {
                        string strTelefoneAluno = txtTelResidencialResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                        int numDDD = int.Parse(strTelefoneAluno.Substring(0, 2));
                        int numTel = int.Parse(strTelefoneAluno.Substring(2, 8));

                        TB242_ALUNO_TELEFONE tb242 = (TB242_ALUNO_TELEFONE.RetornaTodosRegistros().Where(a => a.TB07_ALUNO.CO_ALU == intAluno &&
                                                      a.TB07_ALUNO.CO_EMP == coEmp && a.NR_DDD == numDDD && a.NR_TELEFONE == numTel)).FirstOrDefault();
                        if (tb242 == null)
                        {
                            tb242 = new TB242_ALUNO_TELEFONE();
                            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(intAluno);
                            tb242.TB07_ALUNO = refAluno;
                            refAluno.TB25_EMPRESA1Reference.Load();
                            tb242.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                            tb242.TB239_TIPO_TELEFONE = (from tb239 in TB239_TIPO_TELEFONE.RetornaTodosRegistros()
                                                         where tb239.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb239.CO_TIPO_TELEFONE == "RES"
                                                         select tb239).FirstOrDefault();
                        }

                        tb242.NR_DDD = int.Parse(strTelefoneAluno.Substring(0, 2));
                        tb242.NR_TELEFONE = int.Parse(strTelefoneAluno.Substring(2, 8));
                        tb242.CO_SITUACAO = "A";
                        tb242.DT_SITUACAO = DateTime.Now;

                        TB242_ALUNO_TELEFONE.SaveOrUpdate(tb242, true);
                    }

                    txtNIREAluETA.Text = txtNIREAluCEA.Text = txtNIREAluRAA.Text = txtNIREAluTA.Text = txtNIREAluDoc.Text =
                   txtNIREAluME.Text = txtNIREAluAEA.Text = txtNIREAluMU.Text = txtNireAluno.Text.PadLeft(9, '0');
                    txtNomeAluETA.Text = txtNomeAluCEA.Text = txtNomeAluRAA.Text = txtNomeAluDoc.Text =
                    txtNomeAluME.Text = txtNomeAluTA.Text = txtNomeAluAEA.Text = txtNomeAluMU.Text = txtNomeResp.Text;
                    txtNISAluETA.Text = txtNISAluCEA.Text = txtNISAluRAA.Text = txtNISAluTA.Text =
                        txtNISAluDoc.Text = txtNISAluME.Text = txtNISAluAEA.Text = txtNISAluMU.Text = tb07.NU_NIS != null ? tb07.NU_NIS.Value.ToString() : "";
                    //this.txtNumNIRE.Enabled = this.btnPesqNIRE.Enabled = false;
                    this.btnPesqNIRE.Enabled = false;
                    txtNoInfAluno.Text = tb07.NO_ALU.ToUpper();
                    //this.lblSucInfAlu.Visible = true;
                    CarregaGridTelefones(intAluno, coEmp);
                    CarregaGridEnderecos(intAluno, coEmp);
                    CarregaGridCudEsp(intAluno, coEmp);
                    CarregaGridResAli(intAluno, coEmp);
                    CarregaGridAtivExtra(intAluno, coEmp);

                    //--------> Desabilita o botão de pesquisa do aluno pelo NIRE
                    lnkAtualizaAlu.Enabled = false;

                    ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = txtAno.Enabled = ddlTipoValorCurso.Enabled = true;
                    //this.lblSucInfAlu.Visible = true;
                    //ControlaTabs("ALU");

                    MontaGridGrdCurso();
                    montaGridCursoExtraVazia();
                    //MontaGridCursoExtra();

                    ControlaTabs("GRM");

                    ControlaFormularioAluno(false);
                    ControlaFormularioResponsavel(false);
                    txtAno.Text = DateTime.Now.Year.ToString();
                    return;
                }
            }
            ControlaFormularioAluno(true);
            ControlaFormularioResponsavel(false);
            //AuxiliPagina.EnvioMensagemSucesso(this, "Responsável atualizado com sucesso.");
        }

        /// <summary>
        /// Faz a Efetivação da Matrícula do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEfetMatric_Click(object sender, EventArgs e)
        {
            efetivaMatricula();
        }

        #endregion

        #region Exclusões

        //====> Método que exclue registro de telefone do aluno
        protected void lnkExcTel_Click(object sender, EventArgs e)
        {
            ControlaTabs("TEA");
            //ControlaChecks(chkTelAddAlu);

            //--------> Percorre todas as linhas da grid de Telefones
            foreach (GridViewRow linha in grdTelETA.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    HiddenField hdID_ALUNO_TELEFONE = ((HiddenField)linha.Cells[0].FindControl("hdID_ALUNO_TELEFONE"));
                    int idAluTel = Convert.ToInt32(hdID_ALUNO_TELEFONE.Value);

                    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
                    int coAlu = int.Parse(hdCodAluno.Value);

                    TB242_ALUNO_TELEFONE alutel = TB242_ALUNO_TELEFONE.RetornaPelaChavePrimaria(idAluTel);

                    TB242_ALUNO_TELEFONE.Delete(alutel, true);

                    CarregaGridTelefones(coAlu, coEmp);
                }
            }
        }

        //====> Método que exclue registro de endereço do aluno
        protected void lnkExcEnd_Click(object sender, EventArgs e)
        {
            ControlaTabs("ENA");
            //ControlaChecks(chkEndAddAlu);

            //--------> Percorre todas as linhas da grid de Endereços
            foreach (GridViewRow linha in grdEndETA.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    HiddenField hdID_ALUNO_ENDERECO = ((HiddenField)linha.Cells[0].FindControl("hdID_ALUNO_ENDERECO"));
                    int idAluEnd = Convert.ToInt32(hdID_ALUNO_ENDERECO.Value);

                    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
                    int coAlu = int.Parse(hdCodAluno.Value);

                    TB241_ALUNO_ENDERECO aluend = TB241_ALUNO_ENDERECO.RetornaPelaChavePrimaria(idAluEnd);

                    TB241_ALUNO_ENDERECO.Delete(aluend, true);

                    CarregaGridEnderecos(coAlu, coEmp);
                }
            }
        }

        //====> Método que exclue registro de cuidados da saúde do aluno
        protected void lnkExcCEA_Click(object sender, EventArgs e)
        {
            ControlaTabs("CEA");
            //ControlaChecks(chkCuiEspAlu);

            //--------> Percorre todas as linhas da grid de Medicação
            foreach (GridViewRow linha in grdCuiEsp.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    HiddenField hdID_MEDICACAO = ((HiddenField)linha.Cells[0].FindControl("hdID_MEDICACAO"));
                    int idAluCuiEsp = Convert.ToInt32(hdID_MEDICACAO.Value);

                    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
                    int coAlu = int.Parse(hdCodAluno.Value);

                    TB293_CUIDAD_SAUDE aluend = TB293_CUIDAD_SAUDE.RetornaPelaChavePrimaria(idAluCuiEsp);

                    TB293_CUIDAD_SAUDE.Delete(aluend, true);

                    CarregaGridCudEsp(coAlu, coEmp);
                }
            }
        }

        //====> Método que exclue registro de restrição alimentar do aluno
        protected void lnkExcRestAli_Click(object sender, EventArgs e)
        {
            ControlaTabs("RAD");
            //ControlaChecks(chkResAliAlu);

            //--------> Percorre todas as linhas da grid de Restrição Alimentar
            foreach (GridViewRow linha in grdRestrAlim.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    HiddenField hdID_RESTR_ALIMEN = ((HiddenField)linha.Cells[0].FindControl("hdID_RESTR_ALIMEN"));
                    int idAluResAli = Convert.ToInt32(hdID_RESTR_ALIMEN.Value);

                    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
                    int coAlu = int.Parse(hdCodAluno.Value);

                    TB294_RESTR_ALIMEN aluend = TB294_RESTR_ALIMEN.RetornaPelaChavePrimaria(idAluResAli);

                    TB294_RESTR_ALIMEN.Delete(aluend, true);

                    CarregaGridResAli(coAlu, coEmp);
                }
            }
        }

        //====> Método que exclue registro de atividade extra do aluno
        protected void lnkExcAtiExt_Click(object sender, EventArgs e)
        {
            ControlaTabs("AEA");
            //ControlaChecks(chkRegAtiExt);

            //--------> Percorre todas as linhas da grid de Atividades
            foreach (GridViewRow linha in grdAtividade.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    HiddenField hdCO_ATIV_EXTRA = ((HiddenField)linha.Cells[0].FindControl("hdCO_ATIV_EXTRA"));
                    HiddenField hdCO_INSC_ATIV = ((HiddenField)linha.Cells[0].FindControl("hdCO_INSC_ATIV"));

                    int idAtiExt = Convert.ToInt32(hdCO_ATIV_EXTRA.Value);
                    int idCO_INSC_ATIV = Convert.ToInt32(hdCO_INSC_ATIV.Value);

                    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
                    int coAlu = int.Parse(hdCodAluno.Value);

                    TB106_ATIVEXTRA_ALUNO atiExt = TB106_ATIVEXTRA_ALUNO.RetornaPelaChavePrimaria(coEmp, coAlu, idAtiExt, idCO_INSC_ATIV);

                    TB106_ATIVEXTRA_ALUNO.Delete(atiExt, true);

                    CarregaGridAtivExtra(coAlu, coEmp);
                }
            }
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método responsável por verificar se existe o tipo de escolaridade padrão "Não informado", caso não exista é criado um automaticamente
        /// </summary>
        protected void VerificaExistenciaEscolaridade()
        {
            bool tem = (from tb18 in TB18_GRAUINS.RetornaTodosRegistros()
                        where tb18.CO_SIGLA_INST == "NIF"
                        select tb18).Any();

            if (tem == false)
            {
                TB18_GRAUINS tb18ob = new TB18_GRAUINS();
                tb18ob.NO_INST = "Não Informado";
                tb18ob.CO_SIGLA_INST = "NIF";
                tb18ob.CO_AGRUPADOR = "NIF";
                tb18ob.NU_CLASSI_IMPRES = 1;
                TB18_GRAUINS.SaveOrUpdate(tb18ob, true);
            }
        }

        /// <summary>
        /// Método responsável por verificar quais checkboxes estão selecionados e habilitar ou não o campo relacionado à ele
        /// </summary>
        protected void verificaCamposSelecionados()
        {
            if (chkPgtoAluno.Checked)
            {
                txtValDinPgto.Enabled = chkDinhePgto.Checked;
                txtValDepoPgto.Enabled = chkDepoPgto.Checked;
                txtValDebConPgto.Enabled = txtQtMesesDebConPgto.Enabled = chkDebConPgto.Checked;
                txtValTransPgto.Enabled = chkTransPgto.Checked;
                txtValOutPgto.Enabled = chkOutrPgto.Checked;

                if (ddlBandePgto1.SelectedValue != "N")
                    txtNumPgto1.Enabled = txtTitulPgto1.Enabled = txtVencPgto1.Enabled = txtQtParcCC1.Enabled = txtValCarPgto1.Enabled = ddlBandePgto1.Enabled = true;
                if (ddlBandePgto2.SelectedValue != "N")
                    txtNumPgto2.Enabled = txtTitulPgto2.Enabled = txtVencPgto2.Enabled = txtQtParcCC2.Enabled = txtValCarPgto2.Enabled = ddlBandePgto2.Enabled = true;
                if (ddlBandePgto3.SelectedValue != "N")
                    txtNumPgto3.Enabled = txtTitulPgto3.Enabled = txtVencPgto3.Enabled = txtQtParcCC3.Enabled = txtValCarPgto3.Enabled = ddlBandePgto3.Enabled = true;

                if (ddlBcoPgto1.SelectedValue != "N")
                    txtAgenPgto1.Enabled = txtNContPgto1.Enabled = txtNuDebtPgto1.Enabled = txtNuTitulDebitPgto1.Enabled = txtValDebitPgto1.Enabled = ddlBcoPgto1.Enabled = true;
                if (ddlBcoPgto2.SelectedValue != "N")
                    txtAgenPgto2.Enabled = txtNContPgto2.Enabled = txtNuDebtPgto2.Enabled = txtNuTitulDebitPgto2.Enabled = txtValDebitPgto2.Enabled = ddlBcoPgto2.Enabled = true;
                if (ddlBcoPgto3.SelectedValue != "N")
                    txtAgenPgto3.Enabled = txtNContPgto3.Enabled = txtNuDebtPgto3.Enabled = txtNuTitulDebitPgto3.Enabled = txtValDebitPgto3.Enabled = ddlBcoPgto3.Enabled = true;

                ddlBandePgto1.Enabled = ddlBandePgto2.Enabled = ddlBandePgto3.Enabled = chkCartaoCreditoPgto.Checked;
                ddlBcoPgto1.Enabled = ddlBcoPgto2.Enabled = ddlBcoPgto3.Enabled = chkDebitPgto.Checked;
            }
            else if (chkMenEscAlu.Checked)
            {
                ddlValorContratoCalc.Enabled = ddlValorContratoCalc.Enabled = chkValorContratoCalc.Checked;
                txtDtPrimeiraParcela.Enabled = chkDataPrimeiraParcela.Checked;

                if ((ddlTipoContrato.SelectedValue == "P") && (chkDataPrimeiraParcela.Checked))
                    txtValorPrimParce.Enabled = true;
                else
                    txtValorPrimParce.Enabled = false;
            }
        }

        /// <summary>
        /// Calcula o valor da primeira parcela de acordo com o que o usuário informa
        /// </summary>
        private void CalculaValorPrimeiraParcela()
        {
            decimal vlContr = (!string.IsNullOrEmpty(txtValorContratoCalc.Text) ? decimal.Parse(txtValorContratoCalc.Text) : 0);
            int qtPar = (!string.IsNullOrEmpty(txtQtdeParcelas.Text) ? int.Parse(txtQtdeParcelas.Text) : 0);
            decimal tot = vlContr / qtPar;
            txtValorPrimParce.Text = tot.ToString("N2");
        }

        /// <summary>
        /// Método responsável por fazer todas as devidas verificações e salvamentos
        /// </summary>
        protected void efetivaMatricula()
        {

            int coAlu;

            if (txtAno.Text == "" || ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Ano, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            if (this.hdCodAluno.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Primeiro cadastre o Aluno.");
                return;
            }
            else
            {
                coAlu = Convert.ToInt32(this.hdCodAluno.Value);
                //int nireAluno = txtNIREAluMU.Text != "" ? int.Parse(txtNIREAluMU.Text) : 0;
                //coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nireAluno).FirstOrDefault().CO_ALU;

                int proxTurma = Convert.ToInt32(this.ddlTurma.SelectedValue);
                int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(proxTurma).CO_EMP_UNID_CONT;

                if (chkAtualiFinan.Checked)
                {
                    if (grdNegociacao.Rows.Count == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "É necessário gerar a grid de mensalidades.");
                        return;
                    }

                    TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(Convert.ToInt32(this.ddlModalidade.SelectedValue));

                    if (tb44.CO_SEQU_PC == null || tb44.CO_SEQU_PC_BANCO == null || tb44.CO_SEQU_PC_CAIXA == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Modalidade selecionada para cadastro no financeiro não possui código de conta contábil ativa, de caixa e de banco associados.");
                        return;
                    }
                }

                int modalidade = Convert.ToInt32(this.ddlModalidade.SelectedValue);
                int proxSerie = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
                int qtdZero = 6 - coAlu.ToString().Length;
                string strAluno = "";

                for (int i = 0; i < qtdZero; i++)
                    strAluno = strAluno + "0";

                strAluno = strAluno + coAlu.ToString();
                //string strProxMatricula = DateTime.Now.Year.ToString().Substring(2, 2) + coEmp.ToString() + strAluno;
                string strProxMatricula = PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + proxSerie.ToString().PadLeft(3, '0') + strAluno;
                //string proxAno = DateTime.Now.Year.ToString();
                string proxAno = PreAuxili.proximoAnoMat<string>(txtAno.Text);
                string turno = TB06_TURMAS.RetornaPeloCodigo(proxTurma).CO_PERI_TUR;

                TB01_CURSO hTb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);

                TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");

                int turma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);

                if (hTb01 != null)
                {
                    //int coAlu = Convert.ToInt32(this.hdCodAluno.Value);
                    if (hTb01.CO_NIVEL_CUR == "I" || hTb01.CO_NIVEL_CUR == "F"
                                    || hTb01.CO_NIVEL_CUR == "M")
                    {
                        //----------------> Faz a verificação de matrícula de aluno de acordo com classificação da série                   
                        if (coAlu > 0)
                        {
                            var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                          join iTb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals iTb01.CO_CUR
                                          where lTb08.TB07_ALUNO.CO_ALU == coAlu && (lTb08.CO_SIT_MAT == "A" || lTb08.CO_SIT_MAT == "R")
                                          && (iTb01.CO_NIVEL_CUR == "I" || iTb01.CO_NIVEL_CUR == "F"
                                          || iTb01.CO_NIVEL_CUR == "M")
                                          select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                            if (ocoMat.Count() > 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno do Ensino Regular possui matrícula em aberto.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        string anoP = PreAuxili.proximoAnoMat<string>(txtAno.Text);
                        //int turma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
                        var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                      where lTb08.TB07_ALUNO.CO_ALU == coAlu && lTb08.CO_CUR == hTb01.CO_CUR && lTb08.CO_SIT_MAT == "A"
                                      && lTb08.TB44_MODULO.CO_MODU_CUR == hTb01.CO_MODU_CUR && lTb08.CO_ANO_MES_MAT.Contains(anoP)
                                      //&& lTb08.CO_TUR == turma
                                      select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                        if (ocoMat.Count() > 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno já possui matrícula para ano, modalidade e série/curso informados.");
                            return;
                        }
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Série informada não existe.");
                    return;
                }

                //var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                //              where lTb08.TB07_ALUNO.CO_ALU == coAlu && lTb08.CO_CUR == hTb01.CO_CUR && lTb08.CO_SIT_MAT == "A"
                //              && lTb08.TB44_MODULO.CO_MODU_CUR == hTb01.CO_MODU_CUR && lTb08.CO_ANO_MES_MAT.Contains(proxAno)
                //              && lTb08.CO_TUR == turma
                //              select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                //if (ocoMat != null)
                //{
                //    AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetuar Matrícula. Aluno já matriculado.");
                //    return;
                //}

                //É preciso para validar os campos da Aba de Forma de Pagamento
                #region Validação da Aba FORMA DE PAGAMENTO

                //Valida o Campo Dinheiro ----------------------------------------------------------
                decimal? vldiPgto = null;
                if (chkDinhePgto.Checked && txtValDinPgto.Text == "")
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Dinheiro está marcado, favor informar o valor.");
                    return;
                }
                else if (chkDinhePgto.Checked)
                    vldiPgto = decimal.Parse(txtValDinPgto.Text);


                //Valida o Campo Depósito ----------------------------------------------------------
                decimal? vlDepPgto = null;
                if (chkDepoPgto.Checked && txtValDepoPgto.Text == "")
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Depósito está marcado, favor informar o valor.");
                    return;
                }
                else if (chkDepoPgto.Checked)
                    vlDepPgto = decimal.Parse(txtValDepoPgto.Text);


                //Valida o Campo Débito em Conta ----------------------------------------------------------
                decimal? vlDebCPgto = null;
                //if ((chkDebConPgto.Checked) && (txtValDebConPgto.Text == "") || (txtQtMesesDebConPgto.Text == ""))
                //{
                //    if (string.IsNullOrEmpty(txtValDebConPgto.Text))
                //    {
                //        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Débito em Conta está marcado, favor informar o valor.");
                //        return;
                //    }
                //    else if ((string.IsNullOrEmpty(txtQtMesesDebConPgto.Text)))
                //    {
                //        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Débito em Conta está marcado, favor informar a Quantidade de Parcelas.");
                //        return;
                //    }
                //}
                //else if (chkDebConPgto.Checked)
                //    vlDebCPgto = decimal.Parse(txtValDebConPgto.Text);


                //Valida o Campo Transferência ----------------------------------------------------------
                decimal? vlTransPgto = null;
                if (chkTransPgto.Checked && txtValTransPgto.Text == "")
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Trasnferência está marcado, favor informar o valor.");
                    return;
                }
                else if (chkTransPgto.Checked)
                    vlTransPgto = decimal.Parse(txtValTransPgto.Text);


                //Valida o Campo Outros ---------------------------------------------------
                decimal? vloutPgto = null;
                if (chkOutrPgto.Checked && txtValOutPgto.Text == "")
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "O campo Outros está marcado, favor informar o valor.");
                    return;

                }
                else if (chkOutrPgto.Checked)
                    vloutPgto = decimal.Parse(txtValOutPgto.Text);


                //Valida a Marcação da opção Cartão de Crédito ---------------------------------------
                if ((chkCartaoCreditoPgto.Checked) && (ddlBandePgto1.SelectedValue == "N") && (ddlBandePgto2.SelectedValue == "N") && (ddlBandePgto3.SelectedValue == "N"))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Selecionou a Forma de Pagamento Cartão de Crédito, é preciso preencher os campos condizentes");
                    return;
                }

                //Valida a Marcação da opção Cartão de Débito ---------------------------------------
                if ((chkDebitPgto.Checked) && (ddlBcoPgto1.SelectedValue == "N") && (ddlBcoPgto2.SelectedValue == "N") && (ddlBcoPgto3.SelectedValue == "N"))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Selecionou a Forma de Pagamento Cartão de Débito, é preciso preencher os campos condizentes");
                    return;
                }


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region Valida as Informações do Pagamento em Cartão de Crédito

                //Valida as Informações do Pagamento em Cartão de Crédito
                if ((ddlBandePgto1.SelectedValue != "N") && ((txtNumPgto1.Text == "") || (txtTitulPgto1.Text == "") || (txtVencPgto1.Text == "") || (txtValCarPgto1.Text == "") || (txtQtParcCC1.Text == "")))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Iniciou o preenchimento da Primeira linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                    return;
                }

                //Valida as Informações do Pagamento em Cartão de Crédito
                if ((ddlBandePgto2.SelectedValue != "N") && ((txtNumPgto2.Text == "") || (txtTitulPgto2.Text == "") || (txtVencPgto2.Text == "") || (txtValCarPgto2.Text == "") || (txtQtParcCC2.Text == "")))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Iniciou o preenchimento da Segunda linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                    return;
                }

                //Valida as Informações do Pagamento em Cartão de Crédito
                if ((ddlBandePgto3.SelectedValue != "N") && ((txtNumPgto3.Text == "") || (txtTitulPgto3.Text == "") || (txtVencPgto3.Text == "") || (txtValCarPgto3.Text == "") || (txtQtParcCC3.Text == "")))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Iniciou o preenchimento da Terceira linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                    return;
                }

                #endregion


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region Valida as Informações do Pagamento em Cartão de Débito

                //Valida as Informações do Pagamento em Cartão de Débito
                if ((ddlBcoPgto1.SelectedValue != "N") && ((txtAgenPgto1.Text == "") || (txtNContPgto1.Text == "") || (txtNuDebtPgto1.Text == "") || (txtNuTitulDebitPgto1.Text == "") || (txtValDebitPgto1.Text == "")))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Iniciou o preenchimento da Primeira linha em Informações do Cartão de Débito, favor Informar todos os campos");
                    return;
                }

                //Valida as Informações do Pagamento em Cartão de Débito
                if ((ddlBcoPgto2.SelectedValue != "N") && ((txtAgenPgto2.Text == "") || (txtNContPgto2.Text == "") || (txtNuDebtPgto2.Text == "") || (txtNuTitulDebitPgto2.Text == "") || (txtValDebitPgto2.Text == "")))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Iniciou o preenchimento da Segunda linha em Informações do Cartão de Débito, favor Informar todos os campos");
                    return;
                }

                //Valida as Informações do Pagamento em Cartão de Débito
                if ((ddlBcoPgto3.SelectedValue != "N") && ((txtAgenPgto3.Text == "") || (txtNContPgto3.Text == "") || (txtNuDebtPgto3.Text == "") || (txtNuTitulDebitPgto3.Text == "") || (txtValDebitPgto3.Text == "")))
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você Iniciou o preenchimento da Terceira linha em Informações do Cartão de Débito, favor Informar todos os campos");
                    return;
                }

                #endregion


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region Valida as Informações do Pagamento em Cheque
                //Percorre a grid verificando o que está marcado, e vendo se a linha do registro correspondente está preenchida ou não               
                foreach (GridViewRow linhaPgto in grdChequesPgto.Rows)
                {
                    CheckBox chkGrdPgto = ((CheckBox)linhaPgto.Cells[0].FindControl("chkselectGridPgtoCheque"));

                    if (chkGrdPgto.Checked)
                    {
                        //Resgata os valores dos controles da GridView de Cheques
                        string vlBcoChe = ((DropDownList)linhaPgto.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue;
                        string vlAgeChe = ((TextBox)linhaPgto.Cells[2].FindControl("txtAgenChequePgto")).Text;
                        string vlConChe = ((TextBox)linhaPgto.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                        string vlNuChe = ((TextBox)linhaPgto.Cells[4].FindControl("txtNrChequePgto")).Text;
                        string vlNuCpf = ((TextBox)linhaPgto.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                        string vlNoTit = ((TextBox)linhaPgto.Cells[6].FindControl("txtTitulChequePgto")).Text;
                        decimal vlPgtChe = decimal.Parse(((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text);
                        DateTime dtVenChe = DateTime.Parse(((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text);

                        string vlPgtCheAux = ((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text;
                        string dtVenCheAux = ((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text;

                        if ((vlBcoChe == "N") || (vlAgeChe == "") || (vlConChe == "") || (vlNuChe == "") || (vlNuCpf == "") || (vlNoTit == "") || (vlPgtCheAux == "") || (dtVenCheAux == ""))
                        {
                            AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Você selecionou um registro de cheque, favor Preecher os campos restantes");
                            return;
                        }
                        else
                            chkChequePgto.Checked = true;
                    }
                }

                #endregion

                #endregion

                // Monta o texto do desconto, apresentado no relatório
                string noDesconto = "";
                noDesconto = ddlTpBolsaAlt.SelectedValue == "N" ? "XXX " : chkManterDesconto.Checked == false ? "XXX " : ddlTpBolsaAlt.SelectedValue == "C" ? "CON " : "BOL ";
                int bolsa = ddlBolsaAlunoAlt.SelectedValue != "" ? int.Parse(ddlBolsaAlunoAlt.SelectedValue) : 0;
                string bolsaPerc = txtValorDescto.Text;
                bolsaPerc += chkManterDescontoPerc.Checked ? "%" : "";
                noDesconto += chkManterDesconto.Checked ? (bolsa != 0 ? TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(r => r.CO_TIPO_BOLSA == bolsa).FirstOrDefault().NO_TIPO_BOLSA : "*****") : "ESPECIAL " + bolsaPerc;

                tb08 = new TB08_MATRCUR();
                var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                tb08.TB07_ALUNO = refAluno;
                refAluno.TB25_EMPRESA1Reference.Load();
                tb08.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                tb08.CO_EMP_UNID_CONT = coEmp;
                tb08.CO_ALU_CAD = strProxMatricula;
                tb08.CO_ANO_MES_MAT = proxAno;
                tb08.CO_COL = new int?(LoginAuxili.CO_COL);
                tb08.CO_CUR = proxSerie;
                tb08.CO_SIT_MAT = "A";
                tb08.CO_TUR = new int?(proxTurma);
                tb08.DT_CAD_MAT = DateTime.Now;
                tb08.DT_CADASTRO = new DateTime?(DateTime.Now);
                tb08.DT_EFE_MAT = DateTime.Now;
                tb08.DT_SIT_MAT = DateTime.Now;

                #region Calcula o sequencial do contrato

                // Pega o ultimo sequencial cadastrado para o aluno
                var ultimaSeqMatr = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                                     where m.CO_ALU == refAluno.CO_ALU
                                     select new
                                     {
                                         m.NR_SEQ_CONTR,
                                         m.DT_CAD_MAT
                                     }).OrderByDescending(o => o.DT_CAD_MAT).FirstOrDefault();

                int valseqMatr;
                if (ultimaSeqMatr != null)
                {
                    if (ultimaSeqMatr.NR_SEQ_CONTR != null)
                        valseqMatr = ultimaSeqMatr.NR_SEQ_CONTR.Value + 1;
                    else
                        valseqMatr = 1;
                }
                else
                    valseqMatr = 1;

                string auxCodiContratoMatricur = proxAno.Substring(2, 2) + "." + refAluno.NU_NIRE.ToString().PadLeft(6, '0') + "." + valseqMatr.ToString().PadLeft(2, '0');
                tb08.NR_CONTR_MATRI = auxCodiContratoMatricur;

                #endregion


                //===> Pega o tipo de bolsa selecionado pelo usuário e grava na tabela TB08_MATRCUR que é a tabela de matrícula
                if (ddlBolsaAlunoAlt.SelectedValue != "")
                {
                    TB148_TIPO_BOLSA tb148M = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(ddlBolsaAlunoAlt.SelectedValue));
                    tb08.TB148_TIPO_BOLSA = tb148M;
                }

                //===> Pega o responsável selecionado pelo usuário e gravana tablea de matrícula, TB08_MATRCUR
                int coResp = this.hdCodResp.Value != "" ? Convert.ToInt32(this.hdCodResp.Value) : 0;
                TB108_RESPONSAVEL tb108M = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);
                if (tb108M != null)
                {
                    tb08.TB108_RESPONSAVEL = tb108M;
                }
                tb08.CO_TURN_MAT = turno;

                #region Verfica flags novato
                var matriculas = (from tb08m in TB08_MATRCUR.RetornaTodosRegistros()
                                  where tb08m.CO_ALU == coAlu
                                  select tb08m).DefaultIfEmpty();
                if (matriculas != null && matriculas.Count() > 0)
                {
                    tb08.FLA_REMATRICULADO = "S";
                    tb08.FLA_ESCOLA_NOVATO = false;
                    if (matriculas.Where(f => f.CO_ANO_MES_MAT == proxAno).Count() > 0)
                        tb08.FLA_ESCOLA_ANO_NOVATO = false;
                    else
                        tb08.FLA_ESCOLA_ANO_NOVATO = true;
                }
                else
                {
                    tb08.FLA_REMATRICULADO = "N";
                    tb08.FLA_ESCOLA_NOVATO = true;
                    tb08.FLA_ESCOLA_ANO_NOVATO = true;
                }
                #endregion

                tb08.NU_SEM_LET = "1";
                tb08.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                tb08.NO_DESCONTO = noDesconto;

                #region Atualiza valores no cadastro da matrícula
                if (chkAtualiFinan.Checked)
                {
                    tb08.VL_TOT_MODU_MAT = decimal.Parse(txtTotalMensa.Text);
                    tb08.VL_DES_MOD_MAT = decimal.Parse(txtTotalDesctoEspec.Text);
                    tb08.VL_DES_BOL_MOD_MAT = decimal.Parse(txtTotalDesctoBolsa.Text);
                    tb08.VL_ENT_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    tb08.VL_PAR_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    int qtPar = 0;
                    foreach (GridViewRow l in grdNegociacao.Rows)
                    {
                        if (int.Parse(l.Cells[1].Text) != 0)
                        {
                            qtPar++;
                        }
                        else
                        {
                            tb08.VL_TAXA_MATRIC = decimal.Parse(l.Cells[3].Text);
                        }
                    }

                    //tb08.QT_PAR_MOD_MAT = grdNegociacao.Rows.Count;
                    tb08.QT_PAR_MOD_MAT = qtPar;

                    //==========> Este if verifica se existe mais de um registro na grid de neciação
                    if (grdNegociacao.Rows.Count > 1)
                    {
                        //=========> Caso tenha mais de 1 registro, ele pega o valor do próximo registro
                        tb08.NU_DIA_VEN_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[1].Cells[2].Text).Day;
                    }
                    else
                    {
                        //=========> Caso tenha somente 1 registro, ele pega o valor deste único registro
                        tb08.NU_DIA_VEN_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[0].Cells[2].Text).Day;
                    }
                    tb08.DT_PRI_PAR_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[0].Cells[2].Text);
                    tb08.QT_PAR_DES_MAT = txtQtdeMesesDesctoMensa.Text != "" ? (int?)int.Parse(txtQtdeMesesDesctoMensa.Text) : null;
                }
                #endregion

                tb08.FL_INCLU_MAT = true;
                tb08.FL_ALTER_MAT = false;
                TB08_MATRCUR.SaveOrUpdate(tb08);
                this.Session[SessoesHttp.CodigoMatriculaAluno] = tb08.CO_ALU_CAD;

                #region Atualiza o histórico do aluno
                List<TB43_GRD_CURSO> tb43 = this.GradeSerie(proxSerie);

                DataTable dt = (DataTable)Session["dtCoMat"];
                int coMat = 0;
                foreach (DataRow linha in dt.Rows)
                {
                    coMat = int.Parse(linha["CO_MAT"].ToString());
                    var ocoTb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where iTb079.CO_EMP == refAluno.TB25_EMPRESA1.CO_EMP && iTb079.CO_ALU == coAlu
                                    && iTb079.CO_MODU_CUR == modalidade && iTb079.CO_CUR == proxSerie && iTb079.CO_ANO_REF == proxAno
                                    && iTb079.CO_MAT == coMat
                                    select iTb079).FirstOrDefault();

                    if (ocoTb079 != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetuar Matrícula. Histórico de Aluno já cadastrado.");
                        return;
                    }

                    TB079_HIST_ALUNO tb79 = new TB079_HIST_ALUNO();
                    tb79.CO_ALU = coAlu;
                    tb79.CO_ANO_REF = proxAno;
                    tb79.CO_CUR = proxSerie;
                    tb79.CO_EMP = refAluno.TB25_EMPRESA1.CO_EMP;
                    tb79.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb79.CO_MAT = coMat;
                    tb79.CO_MODU_CUR = modalidade;
                    tb79.CO_TUR = proxTurma;
                    tb79.CO_USUARIO = new int?(LoginAuxili.CO_COL);
                    //tb79.DT_LANC = DateTime.Now;
                    tb79.DT_LANC = DateTime.Parse("05/01/" + proxAno);
                    tb79.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                    tb79.FL_TIPO_LANC_MEDIA = "N";
                    tb79.CO_FLAG_STATUS = "A";
                    TB079_HIST_ALUNO.SaveOrUpdate(tb79, false);
                    TB48_GRADE_ALUNO tb48 = new TB48_GRADE_ALUNO();
                    tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb48.CO_ANO_MES_MAT = proxAno;
                    tb48.CO_CUR = proxSerie;
                    tb48.CO_MAT = coMat;
                    tb48.CO_MODU_CUR = modalidade;
                    tb48.CO_STAT_MATE = "E";
                    tb48.CO_TUR = proxTurma;
                    tb48.NU_SEM_LET = "1";
                    tb48.CO_FLAG_STATUS = "A";
                    tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                    TB48_GRADE_ALUNO.SaveOrUpdate(tb48, false);
                }
                #endregion

                #region Atualiza a matrícula master
                TB80_MASTERMATR tb80 = TB80_MASTERMATR.RetornaPelaChavePrimaria(modalidade, coAlu, proxAno, null);
                if (tb80 == null)
                {
                    tb80 = new TB80_MASTERMATR();
                    tb80.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb80.CO_ALU_CAD = strProxMatricula;
                    tb80.CO_ANO_MES_MAT = proxAno;
                    tb80.CO_COL = new int?(LoginAuxili.CO_COL);
                    tb80.CO_CUR = proxSerie;
                    tb80.CO_SITU_MTR = "A";
                    //tb80.DT_CADA_MTR = DateTime.Now;
                    //tb80.DT_SITU_MTR = DateTime.Now;
                    tb80.DT_CADA_MTR = DateTime.Parse("05/01/" + proxAno);
                    tb80.DT_SITU_MTR = DateTime.Parse("05/01/" + proxAno);
                    //tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Now);
                    tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Parse("05/01/" + proxAno));
                    tb80.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb80.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                }
                else
                {
                    tb80.CO_SITU_MTR = "A";
                    //tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Now);
                    tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Parse("05/01/" + proxAno));
                }
                TB80_MASTERMATR.SaveOrUpdate(tb80, false);
                #endregion

                #region Atualiza a quantidade vagas
                var qtdDispVaga = (from tb289 in TB289_DISP_VAGA_TURMA.RetornaTodosRegistros()
                                   where tb289.TB25_EMPRESA.CO_EMP == refAluno.TB25_EMPRESA1.CO_EMP && tb289.TB44_MODULO.CO_MODU_CUR == modalidade
                                   && tb289.CO_CUR == proxSerie && tb289.CO_ANO == proxAno && tb289.CO_TUR == proxTurma && tb289.CO_PERI_TUR == turno
                                   select tb289).FirstOrDefault();

                if (qtdDispVaga != null)
                {
                    if (!qtdDispVaga.QTDE_VAG_MAT.HasValue)
                        qtdDispVaga.QTDE_VAG_MAT = 1;
                    else
                        qtdDispVaga.QTDE_VAG_MAT += 1;

                    TB289_DISP_VAGA_TURMA.SaveOrUpdate(qtdDispVaga, false);
                }
                if (this.txtNumReserva.Text != string.Empty)
                {
                    TB052_RESERV_MATRI tb052 = TB052_RESERV_MATRI.RetornaPelaChavePrimaria(this.txtNumReserva.Text, LoginAuxili.CO_EMP);
                    tb052.CO_STATUS = "E";
                    TB052_RESERV_MATRI.SaveOrUpdate(tb052, false);
                }
                #endregion

                #region Atualiza o financeiro
                if (chkAtualiFinan.Checked)
                {
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                    if (tb44.CO_SEQU_PC != null && tb44.CO_SEQU_PC_BANCO != null && tb44.CO_SEQU_PC_CAIXA != null)
                    {
                        //----------------> Cria uma lista da TB47_CTA_RECEB
                        List<TB47_CTA_RECEB> lstTb47 = new List<TB47_CTA_RECEB>();

                        TB47_CTA_RECEB tb47;

                        //----------------> Lança a(s) parcela(s) no contas a receber
                        for (int i = 0; i < grdNegociacao.Rows.Count; i++)
                        {
                            tb47 = new TB47_CTA_RECEB();
                            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                            tb47.CO_EMP_UNID_CONT = coEmp;
                            tb47.NU_DOC = grdNegociacao.Rows[i].Cells[0].Text;
                            tb47.NU_PAR = int.Parse(grdNegociacao.Rows[i].Cells[1].Text);
                            tb47.QT_PAR = grdNegociacao.Rows.Count;
                            tb47.DT_CAD_DOC = DateTime.Now;
                            //tb47.DT_CAD_DOC = DateTime.Parse("05/01/" + proxAno);
                            tb47.DE_COM_HIST = "VALOR MENSALIDADE ESCOLAR.";
                            tb47.VR_TOT_DOC = decimal.Parse(txtTotalMensa.Text);
                            tb47.VR_PAR_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[3].Text);
                            tb47.DT_VEN_DOC = DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text);
                            tb47.VL_DES_BOLSA_ALUNO = decimal.Parse(grdNegociacao.Rows[i].Cells[4].Text);
                            tb47.VR_DES_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[5].Text);
                            tb47.VR_MUL_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[7].Text);
                            tb47.VR_JUR_DOC = decimal.Parse(string.Format("{0:0.0000}", decimal.Parse(grdNegociacao.Rows[i].Cells[8].Text)));
                            tb47.DT_EMISS_DOCTO = DateTime.Now;


                            // Alterar para o campo do CO_AGRUP_REC
                            tb47.CO_AGRUP_RECDESP = TB83_PARAMETRO.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP).CO_AGRUP_REC;
                            //tb47.DT_EMISS_DOCTO = DateTime.Parse("05/01/" + proxAno);

                            //--------------------> Flag emissão boleto "S"im ou "N"ão
                            if (ddlBoleto.SelectedValue != "")
                            {
                                tb47.FL_EMITE_BOLETO = "S";
                                tb47.FL_TIPO_PREV_RECEB = "B";
                                //------------------------> Salvando o tipo de documento "Boleto Bancário"
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);

                                //------------------------> Dados do boleto bancário
                                tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoleto.SelectedValue));
                            }
                            else
                            {
                                tb47.FL_EMITE_BOLETO = "N";
                                //------------------------> Salvando o tipo de documento "Recibo"
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(1);
                            }

                            tb47.TB39_HISTORICO = hTb01.ID_HISTO_MENSA == null ? (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                                                                  where tb39.DE_HISTORICO.Contains("Mensalidade")
                                                                                  select tb39).FirstOrDefault() : TB39_HISTORICO.RetornaPelaChavePrimaria((int)hTb01.ID_HISTO_MENSA);

                            if (tb44.CO_CENT_CUSTO != null)
                                tb47.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb44.CO_CENT_CUSTO.Value);

                            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb44.CO_SEQU_PC.Value);
                            tb47.CO_SEQU_PC_BANCO = tb44.CO_SEQU_PC_BANCO.Value;
                            tb47.CO_SEQU_PC_CAIXA = tb44.CO_SEQU_PC_CAIXA.Value;

                            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                            tb47.CO_FLAG_TP_VALOR_MUL = "P";
                            tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "P";
                            tb47.CO_FLAG_TP_VALOR_DES = "V";
                            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                            tb47.CO_FLAG_TP_VALOR_OUT = "V";

                            tb47.IC_SIT_DOC = "A";
                            tb47.TP_CLIENTE_DOC = "A";
                            ///Formato =>Ano: XXXX - Série: XXXXX - Turma: XXXXX - Turno: XXXXX
                            tb47.DE_OBS_BOL_MAT = "Ano: " + DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text).ToString("yyyy") + " - Série/Curso: " + ddlSerieCurso.SelectedItem + " - Turma: " + ddlTurma.SelectedItem + " - Turno: " + txtTurno.Text;

                            tb47.DE_OBS = "MENSALIDADE ESCOLAR";

                            tb47.TP_CLIENTE_DOC = "A";

                            tb47.CO_ALU = coAlu;
                            //tb47.CO_ANO_MES_MAT = DateTime.Now.Year.ToString();
                            tb47.CO_ANO_MES_MAT = PreAuxili.proximoAnoMat<string>(txtAno.Text);
                            tb47.NU_SEM_LET = "1";
                            tb47.CO_CUR = proxSerie;
                            tb47.CO_TUR = proxTurma;
                            tb47.CO_MODU_CUR = modalidade;
                            tb47.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hdCodResp.Value));

                            tb47.DT_SITU_DOC = DateTime.Now;
                            tb47.DT_ALT_REGISTRO = DateTime.Now;
                            //tb47.DT_ALT_REGISTRO = DateTime.Parse("05/01/" + proxAno);
                            ///Atualiza o código da bolsa
                            refAluno.TB148_TIPO_BOLSAReference.Load();
                            if (refAluno.TB148_TIPO_BOLSA != null)
                                tb47.TB148_TIPO_BOLSA = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(refAluno.TB148_TIPO_BOLSA.CO_TIPO_BOLSA);

                            lstTb47.Add(tb47);
                        }
                    }
                }
                #endregion

                bool ocoTxMatr = false;

                #region Desuso Registro de Material Coletivo anterior

                #region Atualiza o fardamento da matrícula
                ///Varre toda a gride de Solicitações e salva na tabela TB114_FARDMAT 
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked && txtDtPrevisao.Text != "")
                    {
                        var tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));
                        tb66.TB89_UNIDADESReference.Load();
                        decimal? valorUnitario = tb66.VL_UNIT_SOLI;
                        int? coUnid = tb66.TB89_UNIDADES != null ? (int?)tb66.TB89_UNIDADES.CO_UNID_ITEM : null;
                        int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;

                        TB114_FARDMAT tb114 =
                            new TB114_FARDMAT
                            {
                                TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO),
                                CO_EMP = LoginAuxili.CO_EMP,
                                CO_ALU = coAlu,
                                CO_ANO_MATRIC = proxAno,
                                CO_MOD = modalidade,
                                CO_CUR = proxSerie,
                                CO_TUR = proxTurma,
                                TB66_TIPO_SOLIC = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0])),
                                ID_UNID_TPSOLIC = coUnid,
                                VL_UNIT_TPSOLIC = valorUnitario,
                                QT_SOLIC = qtde,
                                DT_SOLIC_TPSOLIC = DateTime.Now,
                                DT_PREVI_TPSOLIC = DateTime.Parse(txtDtPrevisao.Text),
                                FL_SITUA_FINAN = ckbAtualizaFinancSolic.Checked ? "A" : null,
                                NM_DOCUM_TITUL = ckbAtualizaFinancSolic.Checked ? !chkConsolValorTitul.Checked ? "SM" + PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + "." + txtNireAluno.Text.Replace(".", "").Replace("-", "").PadLeft(6, '0') + tb66.CO_TIPO_SOLI.ToString().PadLeft(3, '0') + ".01"
                                : "SM" + PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + "." + txtNireAluno.Text.Replace(".", "").Replace("-", "").PadLeft(8, '0') + ".01" : null,
                                DT_REGIS_TPSOLIC = DateTime.Now,
                                CO_COL = LoginAuxili.CO_COL
                            };

                        GestorEntities.SaveOrUpdate(tb114);


                    }
                    if ((((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked) && (((Label)linha.Cells[6].FindControl("txMatr")).Text == "S"))
                    {
                        var varTb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");
                        varTb08.VL_TAXA_MATRIC = decimal.Parse(((Label)linha.Cells[2].FindControl("lblValor")).Text);
                        GestorEntities.SaveOrUpdate(varTb08);
                        ocoTxMatr = true;
                    }

                }

                #endregion

                #region Atualiza o financeiro de itens da secretaria

                if ((ckbAtualizaFinancSolic.Checked) && (txtValorTotal.Text != "") && (txtDtVectoSolic.Text != ""))
                {
                    if (chkConsolValorTitul.Checked)
                    {
                        #region Titulo Consolidado

                        if (Decimal.Parse(txtValorTotal.Text) > 0)
                        {
                            int coHist = ocoTxMatr ? (hTb01.ID_HISTO_MATRI != null ? (int)hTb01.ID_HISTO_MATRI : ddlHistorico.SelectedValue != "" ? int.Parse(ddlHistorico.SelectedValue) : 0) :
                                ddlHistorico.SelectedValue != "" ? int.Parse(ddlHistorico.SelectedValue) : 0;
                            int coAgrup = ddlAgrupador.SelectedValue != "" ? int.Parse(ddlAgrupador.SelectedValue) : 0;
                            int coSequPc = ddlContaContabilA.SelectedValue != "" ? int.Parse(ddlContaContabilA.SelectedValue) : 0;
                            int coSequPcBanco = ddlContaContabilB.SelectedValue != "" ? int.Parse(ddlContaContabilB.SelectedValue) : 0;
                            int coSequPcCaixa = ddlContaContabilC.SelectedValue != "" ? int.Parse(ddlContaContabilC.SelectedValue) : 0;

                            if ((coHist != 0) && (coAgrup != 0) && (coSequPcBanco != 0) && (coSequPcCaixa != 0))
                            {
                                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                                TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hdCodResp.Value));
                                tb108.TB74_UFReference.Load();

                                if (tb108 != null)
                                {
                                    //--------> Faz a verificação para saber se já existe registro cadastrado no Contas a Receber
                                    TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(refAluno.TB25_EMPRESA1.CO_EMP, "SM" + PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + "." + txtNireAluno.Text.Replace(".", "").Replace("-", "").PadLeft(8, '0') + ".01", 1);

                                    if (tb47 == null)
                                    {
                                        tb47 = new TB47_CTA_RECEB();
                                        tb47.CO_ALU = coAlu;
                                        tb47.TB108_RESPONSAVEL = tb108;
                                        tb47.CO_ANO_MES_MAT = PreAuxili.proximoAnoMat<string>(txtAno.Text);
                                        tb47.CO_CUR = proxSerie;
                                        tb47.CO_TUR = proxTurma;
                                        tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                                        tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                                        tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                                        tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                                        tb47.CO_FLAG_TP_VALOR_DES = "V";
                                        tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                                        tb47.CO_FLAG_TP_VALOR_OUT = "V";
                                        tb47.FL_EMITE_BOLETO = tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                                        tb47.CO_MODU_CUR = modalidade;

                                        tb47.CO_NOS_NUM = (tb25.CO_PROX_NOS_NUM != null ? tb25.CO_PROX_NOS_NUM : "");

                                        tb47.DE_COM_HIST = "Solicitação de itens na matrícula realizada no dia " + DateTime.Now.ToString("dd/MM/yyyy");
                                        tb47.DT_ALT_REGISTRO = DateTime.Now;
                                        tb47.DT_CAD_DOC = DateTime.Now;
                                        tb47.DT_EMISS_DOCTO = DateTime.Now;
                                        tb47.DT_SITU_DOC = DateTime.Now;
                                        tb47.DT_VEN_DOC = (txtDtVectoSolic.Text != "" ? DateTime.Parse(txtDtVectoSolic.Text) : DateTime.Now);
                                        tb47.IC_SIT_DOC = "A";
                                        tb47.NU_DOC = "SM" + PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + "." + txtNireAluno.Text.Replace(".", "").Replace("-", "").PadLeft(8, '0') + ".01";
                                        tb47.NU_PAR = 1;
                                        tb47.QT_PAR = 1;
                                        tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                                        tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                                        tb47.TB099_CENTRO_CUSTO = (tb25.CO_CENT_CUSSOL.HasValue ? (TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value)) : null);
                                        tb47.TB227_DADOS_BOLETO_BANCARIO = (tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? (ddlBoletoSolic.SelectedValue != "" ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoletoSolic.SelectedValue)) : null) : null);
                                        tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(coHist);
                                        tb47.CO_AGRUP_RECDESP = coAgrup;
                                        tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc);
                                        tb47.CO_SEQU_PC_BANCO = coSequPcBanco;
                                        tb47.CO_SEQU_PC_CAIXA = coSequPcCaixa;
                                        tb47.TP_CLIENTE_DOC = "A";
                                        tb47.VR_JUR_DOC = (tb25.VL_PEC_JUROS != null ? (decimal.Parse(string.Format("{0:0.0000}", tb25.VL_PEC_JUROS))) : new decimal());
                                        tb47.VR_MUL_DOC = (tb25.VL_PEC_MULTA != null ? (tb25.VL_PEC_MULTA) : new decimal());
                                        tb47.VR_PAR_DOC = (txtValorTotal.Text != "" ? decimal.Parse(txtValorTotal.Text) : 0);
                                        tb47.VR_TOT_DOC = (txtValorTotal.Text != "" ? decimal.Parse(txtValorTotal.Text) : 0);

                                        GestorEntities.SaveOrUpdate(tb47);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Título Não Consolidado
                        if (txtDtVectoSolic.Text != "")
                        {
                            //----------------> Varre toda a gride de Solicitações
                            foreach (GridViewRow linha in grdSolicitacoes.Rows)
                            {
                                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                                {
                                    ocoTxMatr = (((Label)linha.Cells[6].FindControl("txMatr")).Text == "S");
                                    var tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));
                                    decimal? valorUnitario = tb66.VL_UNIT_SOLI;
                                    int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;

                                    if (valorUnitario != null)
                                    {
                                        int coHist = ocoTxMatr ? (hTb01.ID_HISTO_MATRI != null ? (int)hTb01.ID_HISTO_MATRI : tb66.ID_HISTOR_FINANC_TPSOLIC != null ? tb66.ID_HISTOR_FINANC_TPSOLIC.Value : 0) :
                                        tb66.ID_HISTOR_FINANC_TPSOLIC != null ? tb66.ID_HISTOR_FINANC_TPSOLIC.Value : 0;
                                        int coAgrup = tb66.ID_AGRUP_RECEI_TPSOLIC != null ? tb66.ID_AGRUP_RECEI_TPSOLIC.Value : 0;
                                        int coSequPc = tb66.CO_SEQU_PC != null ? tb66.CO_SEQU_PC.Value : 0;
                                        int coSequPcBanco = tb66.CO_SEQU_PC_BANCO != null ? tb66.CO_SEQU_PC_BANCO.Value : 0;
                                        int coSequPcCaixa = tb66.CO_SEQU_PC_CAIXA != null ? tb66.CO_SEQU_PC_CAIXA.Value : 0;

                                        if (coSequPc != 0)
                                        {
                                            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                                            TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hdCodResp.Value));
                                            tb108.TB74_UFReference.Load();

                                            if (tb108 != null)
                                            {
                                                //--------> Faz a verificação para saber se já existe registro cadastrado no Contas a Receber
                                                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(refAluno.TB25_EMPRESA1.CO_EMP, "SM" + PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + "." + txtNireAluno.Text.Replace(".", "").Replace("-", "").PadLeft(6, '0') + tb66.CO_TIPO_SOLI.ToString().PadLeft(3, '0') + ".01", 1);

                                                //--------> Se não, cria um novo registro
                                                if (tb47 == null)
                                                {
                                                    tb47 = new TB47_CTA_RECEB();
                                                    tb47.CO_ALU = coAlu;

                                                    tb47.TB108_RESPONSAVEL = tb108;
                                                    tb47.CO_ANO_MES_MAT = PreAuxili.proximoAnoMat<string>(txtAno.Text);
                                                    tb47.CO_CUR = proxSerie;
                                                    tb47.CO_TUR = proxTurma;
                                                    tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                                                    tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                                                    tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                                                    tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                                                    tb47.CO_FLAG_TP_VALOR_DES = "V";
                                                    tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                                                    tb47.CO_FLAG_TP_VALOR_OUT = "V";
                                                    tb47.FL_EMITE_BOLETO = tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                                                    tb47.CO_MODU_CUR = modalidade;
                                                    tb47.CO_NOS_NUM = (tb25.CO_PROX_NOS_NUM != null ? tb25.CO_PROX_NOS_NUM : "");
                                                    tb47.DE_COM_HIST = "Solicitação de itens na matrícula realizada no dia " + DateTime.Now.ToString("dd/MM/yyyy");
                                                    tb47.DT_ALT_REGISTRO = DateTime.Now;
                                                    tb47.DT_CAD_DOC = DateTime.Now;
                                                    tb47.DT_EMISS_DOCTO = DateTime.Now;
                                                    tb47.DT_SITU_DOC = DateTime.Now;
                                                    tb47.DT_VEN_DOC = (txtDtVectoSolic.Text != "" ? DateTime.Parse(txtDtVectoSolic.Text) : DateTime.Now);
                                                    tb47.IC_SIT_DOC = "A";
                                                    tb47.NU_DOC = "SM" + PreAuxili.proximoAnoMat<string>(txtAno.Text).Substring(2, 2) + "." + txtNireAluno.Text.Replace(".", "").Replace("-", "").PadLeft(6, '0') + tb66.CO_TIPO_SOLI.ToString().PadLeft(3, '0') + ".01";
                                                    tb47.NU_PAR = 1;
                                                    tb47.QT_PAR = 1;
                                                    tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                                                    tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                                                    tb47.TB099_CENTRO_CUSTO = (tb25.CO_CENT_CUSSOL.HasValue ? (TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value)) : null);
                                                    tb47.TB227_DADOS_BOLETO_BANCARIO = (tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? (ddlBoletoSolic.SelectedValue != "" ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoletoSolic.SelectedValue)) : null) : null);
                                                    tb47.TB39_HISTORICO = coHist != 0 ? TB39_HISTORICO.RetornaPelaChavePrimaria(coHist) : tb25.CO_HIST_SOL != null ? TB39_HISTORICO.RetornaPelaChavePrimaria(tb25.CO_HIST_SOL.Value) : null;
                                                    tb47.CO_AGRUP_RECDESP = coAgrup != 0 ? (int?)coAgrup : null;
                                                    tb47.TB56_PLANOCTA = coSequPc != 0 ? TB56_PLANOCTA.RetornaPelaChavePrimaria(coSequPc) : TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTSOL_EMP.Value);
                                                    tb47.CO_SEQU_PC_BANCO = coSequPcBanco != 0 ? coSequPcBanco : tb25.CO_CTA_BANCO != null ? tb25.CO_CTA_BANCO : null;
                                                    tb47.CO_SEQU_PC_CAIXA = coSequPcCaixa != 0 ? coSequPcCaixa : tb25.CO_CTA_CAIXA != null ? tb25.CO_CTA_CAIXA : null;
                                                    tb47.TP_CLIENTE_DOC = "A";
                                                    tb47.VR_JUR_DOC = (tb25.VL_PEC_JUROS != null ? (decimal.Parse(string.Format("{0:0.0000}", tb25.VL_PEC_JUROS))) : new decimal());
                                                    tb47.VR_MUL_DOC = (tb25.VL_PEC_MULTA != null ? (tb25.VL_PEC_MULTA) : new decimal());
                                                    tb47.VR_PAR_DOC = ((valorUnitario.Value.Equals(null) ? 0 : valorUnitario.Value) * qtde);
                                                    tb47.VR_TOT_DOC = (txtValorTotal.Text != "" ? decimal.Parse(txtValorTotal.Text) : 0);

                                                    GestorEntities.SaveOrUpdate(tb47);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }

                #endregion

                #endregion

                #region Atualiza os dados do aluno da série

                TB01_CURSO tb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);

                if (tb01 != null)
                {
                    if (tb01.CO_NIVEL_CUR == "F" || tb01.CO_NIVEL_CUR == "M" || tb01.CO_NIVEL_CUR == "I")
                    {
                        var varTb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                        varTb07.CO_MODU_CUR = modalidade;
                        varTb07.CO_CUR = proxSerie;
                        varTb07.CO_TUR = proxTurma;

                        TB07_ALUNO.SaveOrUpdate(varTb07, true);
                    }
                }

                #endregion

                #region Atualiza os dados da Forma de Pagamento

                if ((chkPgtoAluno.Checked) && (chkDinhePgto.Checked) || (chkDepoPgto.Checked) || (chkDebConPgto.Checked) || (chkTransPgto.Checked) || (chkOutrPgto.Checked) || (chkCartaoCreditoPgto.Checked) || (chkChequePgto.Checked) || (chkDebitPgto.Checked))
                {
                    //Salva as informações sobre a forma de pagamento na tabela de TBE220 na TBE220_MATRCUR_PAGTO
                    TBE220_MATRCUR_PAGTO tbe220 = new TBE220_MATRCUR_PAGTO();

                    tbe220.CO_EMP_M = refAluno.TB25_EMPRESA1.CO_EMP;
                    tbe220.CO_ALU_M = coAlu;
                    tbe220.CO_CUR_M = proxSerie;
                    tbe220.CO_ANO_M = proxAno;
                    tbe220.NU_SEM_M = 1.ToString();
                    tbe220.CO_ALU_CAD = strProxMatricula;

                    tbe220.QT_PARCE = (txtQtPgto.Text != "" ? int.Parse(txtQtPgto.Text) : (int?)null);
                    tbe220.FL_DINHE = (chkDinhePgto.Checked ? "S" : "N");
                    tbe220.VL_DINHE = (vldiPgto.HasValue ? vldiPgto.Value : (decimal?)null);
                    tbe220.FL_DEPOS = (chkDepoPgto.Checked ? "S" : "N");
                    tbe220.VL_DEPOS = (vlDepPgto.HasValue ? vlDepPgto.Value : (decimal?)null);
                    tbe220.FL_DEBIT_CONTA = (chkDebConPgto.Checked ? "S" : "N");
                    tbe220.VL_DEBIT_CONTA = (vlDebCPgto.HasValue ? vlDebCPgto.Value : (decimal?)null);
                    tbe220.QT_PARCE_DEBIT_CONTA = (!string.IsNullOrEmpty(txtQtMesesDebConPgto.Text) ? int.Parse(txtQtMesesDebConPgto.Text) : (int?)null);
                    tbe220.FL_TRANS = (chkTransPgto.Checked ? "S" : "N");
                    tbe220.VL_TRANS = (vlTransPgto.HasValue ? vlTransPgto.Value : (decimal?)null);
                    tbe220.FL_OUTRO = (chkOutrPgto.Checked ? "S" : "N");
                    tbe220.VL_OUTRO = (txtValOutPgto.Text != "" ? decimal.Parse(txtValOutPgto.Text) : (decimal?)null);
                    tbe220.FL_CARTA_CRED = (chkCartaoCreditoPgto.Checked ? "S" : "N");
                    tbe220.FL_CARTA_DEBI = (chkDebitPgto.Checked ? "S" : "N");
                    tbe220.FL_CHEQUE = (chkChequePgto.Checked ? "S" : "N");
                    tbe220.QT_PARCE = (txtQtPgto.Text != "" ? int.Parse(txtQtPgto.Text) : (int?)null);
                    tbe220.CO_COL = LoginAuxili.CO_COL;
                    tbe220.CO_EMP = LoginAuxili.CO_EMP;
                    tbe220.DT_CAD = DateTime.Now;
                    //tbe220.VL_DIFER_RECEB = ;

                    tbe220 = TBE220_MATRCUR_PAGTO.SaveOrUpdate(tbe220);

                    //Salva as informações sobre o pagamento em cartão, quando este for selecionado na TBE221_PAGTO_CARTAO
                    //Salva as informações da parte de cartão de Crédito
                    if (ddlBandePgto1.SelectedValue != "N")
                    {
                        TBE221_PAGTO_CARTAO tbe221 = new TBE221_PAGTO_CARTAO();

                        tbe221.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                        tbe221.CO_BANDE = ddlBandePgto1.SelectedValue;
                        tbe221.CO_NUMER = txtNumPgto1.Text;
                        tbe221.NO_TITUL = txtTitulPgto1.Text;
                        tbe221.DT_VENCI = txtVencPgto1.Text;
                        tbe221.VL_PAGTO = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : (decimal?)null);
                        tbe221.QT_PARCE = (!string.IsNullOrEmpty(txtQtParcCC1.Text) ? int.Parse(txtQtParcCC1.Text) : (int?)null);
                        tbe221.FL_TIPO_TRANSAC = "C";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }

                    if (ddlBandePgto2.SelectedValue != "N")
                    {
                        TBE221_PAGTO_CARTAO tbe221 = new TBE221_PAGTO_CARTAO();

                        tbe221.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                        tbe221.CO_BANDE = ddlBandePgto2.SelectedValue;
                        tbe221.CO_NUMER = txtNumPgto2.Text;
                        tbe221.NO_TITUL = txtTitulPgto2.Text;
                        tbe221.DT_VENCI = txtVencPgto2.Text;
                        tbe221.VL_PAGTO = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : (decimal?)null);
                        tbe221.QT_PARCE = (!string.IsNullOrEmpty(txtQtParcCC2.Text) ? int.Parse(txtQtParcCC2.Text) : (int?)null);
                        tbe221.FL_TIPO_TRANSAC = "C";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }

                    if (ddlBandePgto3.SelectedValue != "N")
                    {
                        TBE221_PAGTO_CARTAO tbe221 = new TBE221_PAGTO_CARTAO();

                        tbe221.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                        tbe221.CO_BANDE = ddlBandePgto3.SelectedValue;
                        tbe221.CO_NUMER = txtNumPgto3.Text;
                        tbe221.NO_TITUL = txtTitulPgto3.Text;
                        tbe221.DT_VENCI = txtVencPgto3.Text;
                        tbe221.VL_PAGTO = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : (decimal?)null);
                        tbe221.QT_PARCE = (!string.IsNullOrEmpty(txtQtParcCC3.Text) ? int.Parse(txtQtParcCC3.Text) : (int?)null);
                        tbe221.FL_TIPO_TRANSAC = "C";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }

                    //Salva as informações da parte de cartão de Débito
                    if (ddlBcoPgto1.SelectedValue != "N")
                    {
                        TBE221_PAGTO_CARTAO tbe221 = new TBE221_PAGTO_CARTAO();

                        tbe221.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                        tbe221.CO_BCO = int.Parse(ddlBcoPgto1.SelectedValue);
                        tbe221.NR_AGENCI = txtAgenPgto1.Text;
                        tbe221.NR_CONTA = txtNContPgto1.Text;
                        tbe221.CO_NUMER = txtNuDebtPgto1.Text;
                        tbe221.NO_TITUL = txtNuTitulDebitPgto1.Text;
                        tbe221.VL_PAGTO = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : (decimal?)null);
                        tbe221.FL_TIPO_TRANSAC = "D";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }

                    if (ddlBcoPgto2.SelectedValue != "N")
                    {
                        TBE221_PAGTO_CARTAO tbe221 = new TBE221_PAGTO_CARTAO();

                        tbe221.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                        tbe221.CO_BCO = int.Parse(ddlBcoPgto2.SelectedValue);
                        tbe221.NR_AGENCI = txtAgenPgto2.Text;
                        tbe221.NR_CONTA = txtNContPgto2.Text;
                        tbe221.CO_NUMER = txtNuDebtPgto2.Text;
                        tbe221.NO_TITUL = txtNuTitulDebitPgto2.Text;
                        tbe221.VL_PAGTO = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : (decimal?)null);
                        tbe221.FL_TIPO_TRANSAC = "D";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }

                    if (ddlBcoPgto3.SelectedValue != "N")
                    {
                        TBE221_PAGTO_CARTAO tbe221 = new TBE221_PAGTO_CARTAO();

                        tbe221.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                        tbe221.CO_BCO = int.Parse(ddlBcoPgto3.SelectedValue);
                        tbe221.NR_AGENCI = txtAgenPgto3.Text;
                        tbe221.NR_CONTA = txtNContPgto3.Text;
                        tbe221.CO_NUMER = txtNuDebtPgto3.Text;
                        tbe221.NO_TITUL = txtNuTitulDebitPgto3.Text;
                        tbe221.VL_PAGTO = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : (decimal?)null);
                        tbe221.FL_TIPO_TRANSAC = "D";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }

                    //Faz o Cálculo da diferença para Alimentar a coluna VL_DIFER_RECEB
                    #region Cálculo do Valor diferença entre o valor pago e o valor do contrato

                    decimal valDinPg = (txtValDinPgto.Text != "" ? decimal.Parse(txtValDinPgto.Text) : decimal.Parse("0,00"));
                    decimal ValDepPg = (txtValDepoPgto.Text != "" ? decimal.Parse(txtValDepoPgto.Text) : decimal.Parse("0,00"));
                    decimal ValDebCPg = (txtValDebConPgto.Text != "" ? decimal.Parse(txtValDebConPgto.Text) : decimal.Parse("0,00"));
                    decimal valTrasPg = (txtValTransPgto.Text != "" ? decimal.Parse(txtValTransPgto.Text) : decimal.Parse("0,00"));
                    decimal valOutPg = (txtValOutPgto.Text != "" ? decimal.Parse(txtValOutPgto.Text) : decimal.Parse("0,00"));
                    decimal valCredPg1 = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : decimal.Parse("0,00"));
                    decimal valCredPg2 = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : decimal.Parse("0,00"));
                    decimal valCredPg3 = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : decimal.Parse("0,00"));
                    decimal valDebPg1 = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : decimal.Parse("0,00"));
                    decimal valDebPg2 = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : decimal.Parse("0,00"));
                    decimal valDebPg3 = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : decimal.Parse("0,00"));

                    decimal valTotalPgto = valDinPg + ValDepPg + ValDebCPg + valTrasPg + valOutPg + valCredPg1 + valCredPg2 + valCredPg3 + valDebPg1 + valDebPg2 + valDebPg3;


                    //Salva as informações sobre o pagamento em cheque, quando este for selecionado na TBE222_PAGTO_CHEQUE
                    foreach (GridViewRow linhaPgto in grdChequesPgto.Rows)
                    {
                        CheckBox chkGrdPgto = ((CheckBox)linhaPgto.Cells[0].FindControl("chkselectGridPgtoCheque"));

                        if (chkGrdPgto.Checked)
                        {
                            //Resgata os valores dos controles da GridView de Cheques
                            int vlBcoChe = int.Parse(((DropDownList)linhaPgto.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue);
                            string vlAgeChe = ((TextBox)linhaPgto.Cells[2].FindControl("txtAgenChequePgto")).Text;
                            string vlConChe = ((TextBox)linhaPgto.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                            string vlNuChe = ((TextBox)linhaPgto.Cells[4].FindControl("txtNrChequePgto")).Text;
                            string vlNuCpf = ((TextBox)linhaPgto.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                            string vlNoTit = ((TextBox)linhaPgto.Cells[6].FindControl("txtTitulChequePgto")).Text;
                            decimal vlPgtChe = decimal.Parse(((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text);
                            DateTime dtVenChe = DateTime.Parse(((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text);

                            TBE222_PAGTO_CHEQUE tbe222 = new TBE222_PAGTO_CHEQUE();

                            tbe222.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220.ID_MATRCUR_PAGTO);
                            tbe222.CO_BCO = vlBcoChe;
                            tbe222.NR_AGENCI = vlAgeChe;
                            tbe222.NR_CONTA = vlConChe;
                            tbe222.NR_CHEQUE = vlNuChe;
                            tbe222.NR_CPF = vlNuCpf.Replace(".", "").Replace("-", "");
                            tbe222.NO_TITUL = vlNoTit;
                            tbe222.VL_PAGTO = vlPgtChe;
                            tbe222.DT_VENC = dtVenChe;

                            TBE222_PAGTO_CHEQUE.SaveOrUpdate(tbe222, true);

                            //Soma o Valor pago em cheque no Valor Total
                            valTotalPgto += vlPgtChe;
                        }
                    }

                    //Altera a coluna VL_DIFER_RECEB com a diferença calculada
                    tbe220.VL_DIFER_RECEB = valTotalPgto - decimal.Parse(txtValContrPgto.Text);
                    TBE220_MATRCUR_PAGTO.SaveOrUpdate(tbe220, true);

                    #endregion

                }

                lnkSucPagtoEscAlu.Visible = true;
                chkPgtoAluno.Checked = true;
                chkPgtoAluno.Enabled = false;
                ControlaTabs("ENA");


                chkEndAddAlu.Enabled = chkTelAddAlu.Enabled = chkDocMat.Enabled =
                    chkMatEsc.Enabled = chkResAliAlu.Enabled = chkCuiEspAlu.Enabled = chkRegAtiExt.Enabled = chkMatrColet.Enabled = true;

                CarregaCamposRegistroMaterial();

                #endregion

                #region Finalização

                GestorEntities.CurrentContext.SaveChanges();

                if (chkAtualiFinan.Checked)
                {
                    if (ddlBoleto.SelectedValue != "")
                    {
                        lnkBolCarne.Enabled = true;
                    }
                    lnkRecMatric.Enabled = lnkFichaMatric.Enabled = lnkCarteira.Enabled = lnkRecibo.Enabled = true;

                    var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                                  where adUs.CodUsuario == LoginAuxili.CO_COL
                                  select new { adUs.FLA_ALT_REG_PAG_MAT }).FirstOrDefault();

                    if (admUsu != null)
                    {
                        if (admUsu.FLA_ALT_REG_PAG_MAT == "S")
                        {
                            ADMMODULO admModulo = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                                   where admMod.nomURLModulo.Contains("5103_RecebPagamCompromisso")
                                                   select admMod).FirstOrDefault();

                            if (admModulo != null)
                            {
                                lnkRealiPagto.HRef = "/" + String.Format("{0}?moduloNome=+Registro+de+Recebimento+ou+Pagamento+de+Compromissos+Financeiros.&", admModulo.nomURLModulo);
                            }

                            ADMMODULO admModuloCheque = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                                         where admMod.nomURLModulo.Contains("5811_CadastramentoChequeEmitidoInstit")
                                                         select admMod).FirstOrDefault();

                            if (admModuloCheque != null)
                            {
                                lnkCheque.HRef = "/" + String.Format("{0}?moduloNome=+Cadastramento+de+Cheques+Recebidos.&", admModuloCheque.nomURLModulo);
                            }
                        }
                    }

                    DesabilitaCamposMatricula();
                    AuxiliPagina.EnvioMensagemSucesso(this, "Matrícula realizada e atualização de dados e mensalidades efetuadas com sucesso");
                }
                else
                {
                    lnkRecMatric.Enabled = lnkFichaMatric.Enabled = lnkCarteira.Enabled = lnkRecibo.Enabled = true;
                    DesabilitaCamposMatricula();
                    AuxiliPagina.EnvioMensagemSucesso(this, "Matrícula realizada e atualização de dados efetuadas com sucesso");
                }

                #endregion

            }/// Fim verificação se o aluno já foi cadastrado
        }

        /// <summary>
        /// Carrega os valores de Taxa de Matrícula no campo responsável por apresentá-los
        /// </summary>
        protected void carregaValorTaxaMatricula()
        {
            //--------> Retorna o turno da turma selecionada (V - Vespertino, M - Matutino, N - Noturno)
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            decimal menCurPre = 0;

            ///Verifica se a primeira mensalidade é a taxa de matrícula e se foi informada a taxa de matrícula
            if (!varSer.FL_MENS_TAXA_MATR && varSer.FL_VALCON_TXMAT == "S")
            {
                if (turnoTurma == tipoTurma[tipoTurnoTurma.M])
                    menCurPre = varSer.VL_TXMAT_MAN ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.V])
                    menCurPre = varSer.VL_TXMAT_TAR ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.N])
                    menCurPre = varSer.VL_TXMAT_NOI ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.I])
                    menCurPre = varSer.VL_TXMAT_INT ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.E])
                    menCurPre = varSer.VL_TXMAT_ESP ?? decimal.Zero;
            }

            txtVlTxMatricula.Text = menCurPre.ToString();
            txtVlTxMatricula.Enabled = true;
        }

        /// <summary>
        /// Verifica se a empresa logada possui o nono dígito configurado ou não.
        /// </summary>
        protected void verificaNonoDigito()
        {
            if (LoginAuxili.FL_NONO_DIGITO_TELEF == "S")
            {
                txtTelCelularRespNonoDig.Visible = txtTelCelularAlunoNoveDigit.Visible = true;
                txtTelCelularResp.Visible = txtTelCelularAluno.Visible = false;
            }
            else
            {
                txtTelCelularResp.Visible = txtTelCelularAluno.Visible = true;
                txtTelCelularRespNonoDig.Visible = txtTelCelularAlunoNoveDigit.Visible = false;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        protected void CarregaBanco(DropDownList ddlBco)
        {
            AuxiliCarregamentos.CarregaBancos(ddlBco, false, false);
            ddlBco.Items.Insert(0, new ListItem("Nenhum", "N"));
        }

        /// <summary>
        /// Carrega as matérias na grid de cursos extras, para deixar flexível e com possibilidade de inserção de matérias além das que já existem na grade do curso.
        /// </summary>
        protected void CarreagCursoExtra()
        {
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            coCur = 172;

            var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                       where tb01.CO_CUR != coCur
                       select new
                       {
                           tb01.CO_CUR,
                           tb01.NO_CUR
                       });

            ddlCurExt.DataTextField = "NO_CUR";
            ddlCurExt.DataValueField = "CO_CUR";

            ddlCurExt.DataSource = res;
            ddlCurExt.DataBind();

            ddlCurExt.Items.Insert(0, new ListItem("Totos", ""));
        }

        /// <summary>
        /// Monta a Grid de Matérias quando um determinado curso é selecionado.
        /// </summary>
        protected void MontaGridGrdCurso()
        {
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            //string anoAtual = DateTime.Now.Year.ToString();
            string anoAtual = (!string.IsNullOrEmpty(txtAno.Text) ? txtAno.Text : DateTime.Now.Year.ToString());
            //coCur = 172;

            //if (coCur != 0)
            //{
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       where tb43.CO_ANO_GRADE == anoAtual
                       && tb43.CO_CUR == coCur
                       select new
                       {
                           tb107.NO_MATERIA,
                           tb107.NO_SIGLA_MATERIA,
                           tb02.QT_CARG_HORA_MAT,
                           tb02.CO_MAT
                       }).Distinct().ToList();

            grdGrdCurso.DataSource = res;
            //}
            grdGrdCurso.DataBind();
        }

        /// <summary>
        /// Monta a Grid Vazia, é o padrão de quando a página é carregada inicialmente.
        /// </summary>
        protected void montaGridCursoExtraVazia()
        {
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
                       where tb43.CO_CUR == 0
                       select new GridCursoExtra
                       {
                           noMateria = tb107.NO_MATERIA,
                           noSigla = tb107.NO_SIGLA_MATERIA,
                           qtCarga = tb02.QT_CARG_HORA_MAT,
                           coMat = tb02.CO_MAT,
                           noCur = tb01.CO_SIGL_CUR,
                       }).Distinct().ToList();

            grdCursoExt.DataSource = res;
            grdCursoExt.DataBind();
        }

        /// <summary>
        /// Monta a Grid de Cursos Extras, somente quando é selecionado o Campo Série/Curso, ou quando é selecionado uma opção no DDL de Curso Exta
        /// </summary>
        protected void MontaGridCursoExtra()
        {
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coCurExt = ddlCurExt.SelectedValue != "" ? int.Parse(ddlCurExt.SelectedValue) : 0;
            string anoAtual = DateTime.Now.Year.ToString();
            //coCur = 172;

            if (coCurExt == 0)
            {
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
                           where tb43.CO_ANO_GRADE == anoAtual
                           && tb43.CO_CUR != coCur
                           select new GridCursoExtra
                           {
                               noMateria = tb107.NO_MATERIA,
                               noSigla = tb107.NO_SIGLA_MATERIA,
                               qtCarga = tb02.QT_CARG_HORA_MAT,
                               coMat = tb02.CO_MAT,
                               noCur = tb01.CO_SIGL_CUR
                           }).Distinct().ToList();

                grdCursoExt.DataSource = res;
                grdCursoExt.DataBind();
            }
            else
            {
                var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
                           where tb43.CO_ANO_GRADE == anoAtual
                           && tb43.CO_CUR == coCurExt
                           select new GridCursoExtra
                           {
                               noMateria = tb107.NO_MATERIA,
                               noSigla = tb107.NO_SIGLA_MATERIA,
                               qtCarga = tb02.QT_CARG_HORA_MAT,
                               coMat = tb02.CO_MAT,
                               noCur = tb01.NO_CUR
                           }).Distinct().ToList();

                grdCursoExt.DataSource = res;
                grdCursoExt.DataBind();
            }
        }

        protected void CriaNovaLinhaGridChequesPgto()
        {

            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BcoChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "AgenChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuConChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCpfChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "noTituChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "vlCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencChe";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdChequesPgto.Rows)
            {
                linha = dtV.NewRow();
                linha["BcoChe"] = ((DropDownList)li.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue;
                linha["AgenChe"] = ((TextBox)li.Cells[2].FindControl("txtAgenChequePgto")).Text;
                linha["nuConChe"] = ((TextBox)li.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                linha["nuCheChe"] = ((TextBox)li.Cells[4].FindControl("txtNrChequePgto")).Text;
                linha["nuCpfChe"] = ((TextBox)li.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                linha["noTituChe"] = ((TextBox)li.Cells[6].FindControl("txtTitulChequePgto")).Text;
                linha["vlCheChe"] = ((TextBox)li.Cells[7].FindControl("txtVlChequePgto")).Text;
                linha["dtVencChe"] = ((TextBox)li.Cells[8].FindControl("txtDtVencChequePgto")).Text;
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["BcoChe"] = "";
            linha["AgenChe"] = "";
            linha["nuConChe"] = "";
            linha["nuCheChe"] = "";
            linha["nuCpfChe"] = "";
            linha["noTituChe"] = "";
            linha["vlCheChe"] = "";
            linha["dtVencChe"] = "";
            dtV.Rows.Add(linha);

            Session["GridCheques"] = dtV;

            carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContexto()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridCheques"];

            grdChequesPgto.DataSource = dtV;
            grdChequesPgto.DataBind();

            foreach (GridViewRow lipgtoaux in grdChequesPgto.Rows)
            {
                DropDownList ddlbcoauxChe = ((DropDownList)lipgtoaux.Cells[1].FindControl("ddlBcoChequePgto"));
                CarregaBanco(ddlbcoauxChe);
            }

        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridChequesPgto()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BcoChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "AgenChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuConChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCpfChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "noTituChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "vlCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencChe";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i <= 6)
            {
                linha = dtV.NewRow();
                linha["BcoChe"] = "";
                linha["AgenChe"] = "";
                linha["nuConChe"] = "";
                linha["nuCheChe"] = "";
                linha["nuCpfChe"] = "";
                linha["noTituChe"] = "";
                linha["vlCheChe"] = "";
                linha["dtVencChe"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridCheques", dtV);


            grdChequesPgto.DataSource = dtV;
            grdChequesPgto.DataBind();

            foreach (GridViewRow lipgtoaux in grdChequesPgto.Rows)
            {
                DropDownList ddlbcoauxChe = ((DropDownList)lipgtoaux.Cells[1].FindControl("ddlBcoChequePgto"));
                CarregaBanco(ddlbcoauxChe);
            }
        }

        public class GridCursoExtra
        {
            public string noMateria { get; set; }
            public string noSigla { get; set; }
            public int qtCarga { get; set; }
            public string noCur { get; set; }
            public int coMat { get; set; }
            public string nome
            {
                get
                {
                    return noMateria;
                }
            }
        }

        /// <summary>
        /// Monta a Grid de parcelas do contrato.
        /// </summary>
        protected void MontaGridNegociacao()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            //int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int nireAluno = txtNIREAluMU.Text != "" ? int.Parse(txtNIREAluMU.Text) : 0;
            //int coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nireAluno).FirstOrDefault().CO_ALU;
            int coAlu = this.hdCodAluno.Value != "" ? Convert.ToInt32(this.hdCodAluno.Value) : 0;
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, LoginAuxili.CO_EMP);

            //--------> Retorna o turno da turma selecionada (V - Vespertino, M - Matutino, N - Noturno)
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmp);

            //--------> Mensalidade do Curso
            decimal menCur = 0;

            /// Valor taxa matrícula
            decimal menCurPre = 0;

            //--------> Verifica se o usuário selecionou um tipo de contrato
            if (ddlTipoContrato.SelectedValue == "")
            {
                ddlTipoContrato.SelectedValue = "P";
                ddlTipoContrato.DataBind();
            }

            #region Verifica o tipo de valor

            string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                ddlTipoContrato.SelectedValue,
                turnoTurma,
                varSer,
                this.Page, false);
            if (retornoValor == string.Empty)
                return;
            else
            {
                if (chkAlterValorContr.Checked)
                {
                    ///Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                    switch (ddlValorContratoCalc.SelectedValue)
                    {
                        ///Proporcional a quantidade de meses
                        case "P":
                            if (txtValorContratoCalc.Text != "")
                            {
                                ///menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                            }
                            else
                                menCur = (Decimal.Parse(retornoValor) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                            break;

                        ///Total de meses do curso
                        case "T":
                            if (txtValorContratoCalc.Text != "")
                            {
                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                            }
                            else
                                menCur = Decimal.Parse(retornoValor);
                            break;
                    }
                }
                else
                {
                    menCur = Decimal.Parse(retornoValor);
                }
            }

            #endregion

            if (menCur == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Série/Curso informado não apresenta mensalidade.");
                return;
            }
            else
            {
                if (chkDataPrimeiraParcela.Checked)
                {
                    if (Decimal.Parse(txtValorPrimParce.Text) > menCur)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor da 1ª Parcela esta inconsistente.");
                        return;
                    }
                }

                if (txtDesctoMensa.Text != "")
                {
                    if (Decimal.Parse(txtDesctoMensa.Text) > menCur)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor de Desconto Especial inconsistente.");
                        return;
                    }
                }
            }
            int qtdParcCur = int.Parse(txtQtdeParcelas.Text);
            if (qtdParcCur == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade de parcelas informada para Série/Curso deve ser maior que zero.");
                return;
            }
            int qtdDiasInterMeses = varSer.QT_INTERV_DIAS != null ? varSer.QT_INTERV_DIAS.Value : 0;
            decimal desCur = 0;
            decimal totalMensa = 0;
            decimal totalDescto = 0;
            decimal totalDesctoEspec = 0;
            decimal totalValorLiqui = 0;
            decimal multaUnid = tb83.VL_PERCE_MULTA != null ? (decimal)tb83.VL_PERCE_MULTA : 0;
            decimal jurosUnid = tb83.VL_PERCE_JUROS != null ? (decimal)tb83.VL_PERCE_JUROS : 0;
            int diaVenctoUnid = int.Parse(ddlDiaVecto.SelectedValue);
            int qtdeMesesDescto = txtQtdeMesesDesctoMensa.Text != "" ? int.Parse(txtQtdeMesesDesctoMensa.Text) : 0;
            int mesDescto = txtMesIniDesconto.Text != "" ? int.Parse(txtMesIniDesconto.Text) : 0;
            //int numNire = txtNireAluno.Text.Replace(".", "").Replace("-", "") != "" ? int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", "")) : 0;
            int numNire = tb07.NU_NIRE;

            if ((menCur > 0) && (numNire > 0))
            {
                DateTime dataSelec;
                if (chkDataPrimeiraParcela.Checked && txtDtPrimeiraParcela.Text != "")
                //if (txtDtPrimeiraParcela.Text != "")
                {
                    dataSelec = DateTime.Parse(txtDtPrimeiraParcela.Text);
                }
                else
                {
                    int ano = int.Parse(txtAno.Text);
                    dataSelec = DateTime.Now.Year == PreAuxili.proximoAnoMat<int>(txtAno.Text) ? (int.Parse(ddlDiaVecto.SelectedValue) > 27 && DateTime.Now.Month == 2 ? DateTime.Parse("27/" + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString()) : DateTime.Parse(ddlDiaVecto.SelectedValue + '/' + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString())) : DateTime.Parse(ddlDiaVecto.SelectedValue + "/01/" + PreAuxili.proximoAnoMat<string>(txtAno.Text));
                    if (DateTime.Now.Year == PreAuxili.proximoAnoMat<int>(txtAno.Text) && dataSelec < DateTime.Now)
                    {
                        if (varSer.QT_DIAS_PARC1 != null)
                        {
                            if (varSer.QT_DIAS_PARC1 > 0)
                            {
                                dataSelec = DateTime.Now.AddDays((double)varSer.QT_DIAS_PARC1);
                            }
                        }
                        else
                            dataSelec = DateTime.Now;
                    }
                    else
                    {
                        if (varSer.QT_DIAS_PARC1 != null)
                        {
                            if (varSer.QT_DIAS_PARC1 > 0)
                            {
                                dataSelec = dataSelec.AddDays((double)varSer.QT_DIAS_PARC1);
                            }
                        }
                    }
                }

                if (int.Parse(txtQtdeParcelas.Text) > varSer.NU_QUANT_MESES)
                {
                    return;
                }

                //DateTime dataSelec = DateTime.Parse( ddlDiaVecto.SelectedValue + "/01/" + txtAno.Text);  
                int parcelas = 0;

                //--------> Valida se o usuário escolheu calcular o valor do contrato e selecionou a opção "Todos (Todos os meses)"
                //if (chkValorContratoCalc.Checked && ddlValorContratoCalc.SelectedValue == "T" && !String.IsNullOrEmpty(varSer.NU_QUANT_MESES.ToString())
                //    && )
                //{
                //    parcelas = varSer.NU_QUANT_MESES;
                //}
                //else
                //{
                //    parcelas = qtdParcCur > 0 && qtdParcCur == 12 && (!chkGeraTotalParce.Checked) ? qtdParcCur - dataSelec.Month + 1 : qtdParcCur;
                //}
                if (ddlValorContratoCalc.SelectedValue == "P")
                {
                    parcelas = qtdParcCur > 0 && qtdParcCur == 12 && (!chkGeraTotalParce.Checked) ? qtdParcCur - dataSelec.Month + 1 : qtdParcCur;
                }
                else
                {
                    parcelas = qtdParcCur;
                }

                int anoDesctoIni = txtPeriodoIniDesconto.Text != "" ? DateTime.Parse(txtPeriodoIniDesconto.Text).Year : dataSelec.Year;
                int anoDesctoFim = txtPeriodoFimDesconto.Text != "" ? DateTime.Parse(txtPeriodoFimDesconto.Text).Year : 0;
                int mesIniDescto = txtPeriodoIniDesconto.Text != "" ? DateTime.Parse(txtPeriodoIniDesconto.Text).Month : 0;
                int mesFimDescto = txtPeriodoFimDesconto.Text != "" ? DateTime.Parse(txtPeriodoFimDesconto.Text).Month : 0;
                int mesSelecionado = dataSelec.Month;
                decimal desEspec = txtDesctoMensa.Text != "" ? (decimal)decimal.Parse(txtDesctoMensa.Text) : 0;
                decimal valorLiqui = 0;
                decimal valorPrimParce = txtValorPrimParce.Text != "" ? Decimal.Parse(txtValorPrimParce.Text) : 0;
                int countDesconto = 1; // Contador para o desconto especial                

                if (ddlTipoContrato.SelectedValue == "V")
                {
                    parcelas = 1;
                }

                //Verifica se a quantidade de parcelas do desconto especial é maior que a qtde de parcelas geradas
                if (txtDesctoMensa.Text != "")
                {
                    DateTime dataTeste = dataSelec;
                    int contParcTeste = 1;
                    for (int i = 2; i <= parcelas; i++)
                    {
                        contParcTeste = i;
                        if (i == 2)
                        {
                            dataTeste = qtdDiasInterMeses > 0 ? dataTeste.AddDays((double)(qtdDiasInterMeses + 1)) : dataTeste.AddMonths(i - 1);
                        }
                        else
                            dataTeste = qtdDiasInterMeses > 0 ? dataTeste.AddDays((double)(qtdDiasInterMeses)) : dataTeste.AddMonths(1);

                        if (qtdDiasInterMeses == 0)
                        {
                            dataTeste = DateTime.Parse(((diaVenctoUnid != 0) ? (diaVenctoUnid > 27 && dataTeste.Month == 2 ? "27" : diaVenctoUnid.ToString("00")) : "05") + "/" + dataTeste.Month.ToString("00") + "/" + dataTeste.Year.ToString());
                        }

                        //-----> Caso o usuário não tenha escolhido a quantidade de parcelas e o ano da parcela for maior que o 
                        if ((!chkGeraTotalParce.Checked) && (dataTeste.Year > dataSelec.Year) && (ddlValorContratoCalc.SelectedValue != "T"))
                        {
                            i = parcelas;
                        }
                    }

                    if (ddlTipoDesctoMensa.SelectedValue == "M")
                    {
                        if (int.Parse(txtQtdeMesesDesctoMensa.Text) > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Qtde meses de desconto especial inconsistente.");
                            return;
                        }

                        if (mesDescto > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Mês de início de desconto (MID) inválido.");
                            return;
                        }

                        if ((mesDescto + int.Parse(txtQtdeMesesDesctoMensa.Text) - 1) > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Qtde de mes de desconto especial combinado ao mês de início de desconto estão inconsistentes.");
                            return;
                        }
                    }
                    else
                    {
                        qtdeMesesDescto = (parcelas == contParcTeste ? parcelas : (contParcTeste - 1));
                        desEspec = desEspec / (parcelas == contParcTeste ? parcelas : (contParcTeste - 1));
                    }
                }

                decimal valorDemaisParc = 0;
                if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked && parcelas > 1)
                {
                    valorDemaisParc = Decimal.Parse(((menCur - valorPrimParce) / (parcelas - 1)).ToString("N2"));
                    menCur = valorPrimParce;
                }
                else
                {
                    //------------> Divide o valor total do curso pela quantidade de parcela do curso, encontrando o valor mensal
                    menCur = Decimal.Parse((menCur / parcelas).ToString("N2"));
                }

                //------------> Verifica se o valor do desconto especial é maior que o valor da parcela, se for o caso, o sistema apresenta uma mensagem de erro.
                if (desEspec > menCur)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de desconto especial inconsistente.");
                    return;
                }

                if (txtValorDescto.Text != "")
                {
                    desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                }
                else
                    desCur = 0;

                if (chkDataPrimeiraParcela.Checked == false)
                {
                    txtValorPrimParce.Text = menCur.ToString("N2");
                }

                grdNegociacao.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

                DataTable Dt = new DataTable();

                Dt.Columns.Add("CO_EMP");

                Dt.Columns.Add("NU_DOC");

                Dt.Columns.Add("NU_PAR");

                Dt.Columns.Add("DT_CAD_DOC");

                Dt.Columns.Add("dtVencimento");

                Dt.Columns.Add("valorParcela");

                Dt.Columns.Add("valorBolsa");

                Dt.Columns.Add("valorDescto");

                Dt.Columns.Add("valorLiquido");

                Dt.Columns.Add("valorMulta");

                Dt.Columns.Add("valorJuros");

                #region Adicionar taxa de matrícula

                ///Verifica se a primeira mensalidade é a taxa de pré-matrícula
                //if (!varSer.FL_MENS_TAXA_MATR)
                if (varSer.FL_VALCON_TXMAT != "S")
                {
                    //AuxiliPagina.EnvioMensagemSucesso(this.Page, "Não existe taxa de matrícula na série/curso escolhida.");
                    chkTaxaMatricula.Checked = chkTaxaMatricula.Enabled = txtVlTxMatricula.Enabled = false;
                    txtVlTxMatricula.Text = "";
                }
                else
                {
                    //chkTaxaMatricula.Checked = chkTaxaMatricula.Enabled = true;

                    //Verifica se está marcada a opção de gerar com a taxa de matrícula
                    if (chkTaxaMatricula.Checked == true)
                    {
                        ///Verifica se a primeira mensalidade é a taxa de matrícula e se foi informada a taxa de matrícula
                        if (!varSer.FL_MENS_TAXA_MATR && varSer.FL_VALCON_TXMAT == "S")
                        {
                            //if (turnoTurma == tipoTurma[tipoTurnoTurma.M])
                            //    menCurPre = varSer.VL_TXMAT_MAN ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.V])
                            //    menCurPre = varSer.VL_TXMAT_TAR ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.N])
                            //    menCurPre = varSer.VL_TXMAT_NOI ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.I])
                            //    menCurPre = varSer.VL_TXMAT_INT ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.E])
                            //    menCurPre = varSer.VL_TXMAT_ESP ?? decimal.Zero;

                            menCurPre = (txtVlTxMatricula.Text != "" ? Decimal.Parse(txtVlTxMatricula.Text) : 0);
                            if (menCurPre > 0)
                            {
                                Dt.Rows.Add(coEmp,
                                    "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "00", "00",
                                    DateTime.Now.ToString("dd/MM/yyyy"),
                                    DateTime.Now.ToString("dd/MM/yyyy"),
                                    menCurPre.ToString("N2"),
                                    decimal.Zero.ToString("N2"),
                                    decimal.Zero.ToString("N2"),
                                    menCurPre.ToString("N2"),
                                    decimal.Zero.ToString("N2"),
                                    decimal.Zero.ToString("N3")
                                    );
                            }
                            else
                            {
                                ///Verifica se a taxa de pré-matrícula é igual ou menos a zero
                                if (menCurPre <= 0)
                                    AuxiliPagina.EnvioMengaemConfirmacao(this.Page, "Valor da taxa de matrícula não é superior a zero.");
                            }
                        }
                    }
                }

                #endregion

                if ((anoDesctoFim != 0) && (anoDesctoFim >= dataSelec.Year))
                {
                    if ((anoDesctoIni < dataSelec.Year) && (anoDesctoFim > dataSelec.Year))
                    {
                        valorLiqui = menCur - desCur;
                        if (valorLiqui < 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                            grdNegociacao.DataSource = null;
                            grdNegociacao.DataBind();
                            return;
                        }
                        totalMensa = totalMensa + menCur;
                        totalDescto = totalDescto + desCur;
                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                            grdNegociacao.DataSource = null;
                            grdNegociacao.DataBind();
                            return;
                        }
                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                        totalValorLiqui = totalValorLiqui + valorLiqui;
                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                            dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                            valorLiqui.ToString("N2"),
                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                    }
                    else if ((anoDesctoIni < dataSelec.Year) && (anoDesctoFim == dataSelec.Year))
                    {
                        if (dataSelec.Month <= mesFimDescto)
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                    else if ((anoDesctoIni == dataSelec.Year) && (anoDesctoFim > dataSelec.Year))
                    {
                        if (dataSelec.Month >= mesIniDescto)
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                    else if ((anoDesctoIni == dataSelec.Year) && (anoDesctoFim == dataSelec.Year))
                    {
                        if ((dataSelec.Month >= mesIniDescto) && (dataSelec.Month <= mesFimDescto))
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                }
                else
                {
                    desCur = 0;
                    valorLiqui = menCur - desCur;
                    if (valorLiqui < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                        grdNegociacao.DataSource = null;
                        grdNegociacao.DataBind();
                        return;
                    }
                    totalMensa = totalMensa + menCur;
                    totalDescto = totalDescto + desCur;
                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                        grdNegociacao.DataSource = null;
                        grdNegociacao.DataBind();
                        return;
                    }
                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                    totalValorLiqui = totalValorLiqui + valorLiqui;
                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                        dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                        valorLiqui.ToString("N2"),
                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                }

                if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked)
                {
                    menCur = valorDemaisParc;
                }
                DateTime dataVectoMensa = dataSelec;
                DateTime dtInicDescto, dtFimDescto = DateTime.Now;
                int mesDescontoFim = 0;
                qtdeMesesDescto = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0) > 0 ? qtdeMesesDescto - 1 : qtdeMesesDescto;
                if (parcelas > 1)
                {
                    decimal t = 0;
                    if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked)
                    {
                        t = valorPrimParce;
                    }
                    else
                    {
                        t = 0;
                    }

                    for (int i = 2; i <= parcelas; i++)
                    {
                        t = t + menCur;

                        if (i == parcelas)
                        {
                            decimal tt = 0;
                            if (valorPrimParce == 0 || !chkDataPrimeiraParcela.Checked)
                            {
                                tt = menCur * parcelas;
                            }
                            else
                            {
                                tt = (menCur * (parcelas - 1)) + valorPrimParce;
                            }

                            if (tt > decimal.Parse(txtValorContratoCalc.Text))
                            {
                                decimal d = tt - decimal.Parse(txtValorContratoCalc.Text);
                                menCur = menCur - d;
                            }
                            else
                            {
                                if (tt < decimal.Parse(txtValorContratoCalc.Text))
                                {
                                    decimal d = decimal.Parse(txtValorContratoCalc.Text) - tt;
                                    menCur = menCur + d;
                                }
                            }
                        }

                        mesSelecionado++;
                        // Determina o Mês fim para o desconto
                        mesDescontoFim = mesDescto + (qtdeMesesDescto - 1);
                        if (mesDescontoFim > 12)
                        {
                            mesDescontoFim = mesDescontoFim - 12;
                        }

                        if (dataVectoMensa.Month >= mesDescto && dataVectoMensa.Year == DateTime.Now.Year)
                        {
                            countDesconto++;
                        }
                        else
                        {
                            if (dataVectoMensa.Year > DateTime.Now.Year && dataVectoMensa.Month >= mesDescontoFim)
                            {
                                countDesconto++;
                            }
                        }

                        if (i == 2)
                        {
                            dataVectoMensa = qtdDiasInterMeses > 0 ? dataVectoMensa.AddDays((double)(qtdDiasInterMeses + 1)) : dataVectoMensa.AddMonths(i - 1);
                        }
                        else
                            dataVectoMensa = qtdDiasInterMeses > 0 ? dataVectoMensa.AddDays((double)(qtdDiasInterMeses)) : dataVectoMensa.AddMonths(1);

                        if (qtdDiasInterMeses == 0)
                        {
                            dataVectoMensa = DateTime.Parse(((diaVenctoUnid != 0) ? (diaVenctoUnid > 27 && dataVectoMensa.Month == 2 ? "27" : diaVenctoUnid.ToString("00")) : "05") + "/" + dataVectoMensa.Month.ToString("00") + "/" + dataVectoMensa.Year.ToString());
                        }

                        //-----> Caso o usuário não tenha escolhido a quantidade de parcelas e o ano da parcela for maior que o 
                        if ((!chkGeraTotalParce.Checked) && (dataVectoMensa.Year > dataSelec.Year) && (ddlValorContratoCalc.SelectedValue != "T"))
                        {
                            i = parcelas;
                        }
                        else
                        {
                            if (i == 2)
                            {
                                desCur = 0;
                                if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out dtInicDescto) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out dtFimDescto))
                                {
                                    if ((dtInicDescto <= dataVectoMensa) && (dtFimDescto >= dataVectoMensa))
                                    {
                                        if (txtValorDescto.Text != "")
                                        {
                                            desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                                        }
                                        else
                                            desCur = 0;
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                    else
                                    {
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                }
                                else
                                {
                                    valorLiqui = menCur - desCur;
                                    if (valorLiqui < 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    totalMensa = totalMensa + menCur;
                                    totalDescto = totalDescto + desCur;
                                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                    totalValorLiqui = totalValorLiqui + valorLiqui;
                                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                        dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                        valorLiqui.ToString("N2"),
                                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                }
                            }
                            else
                            {
                                desCur = 0;
                                if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out dtInicDescto) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out dtFimDescto))
                                {
                                    if ((dtInicDescto <= dataVectoMensa) && (dtFimDescto >= dataVectoMensa))
                                    {
                                        if (txtValorDescto.Text != "")
                                        {
                                            desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                                        }
                                        else
                                            desCur = 0;
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                    else
                                    {
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                }
                                else
                                {
                                    valorLiqui = menCur - desCur;
                                    if (valorLiqui < 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    totalMensa = totalMensa + menCur;
                                    totalDescto = totalDescto + desCur;
                                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                    totalValorLiqui = totalValorLiqui + valorLiqui;
                                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                        dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                        valorLiqui.ToString("N2"),
                                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                }
                            }
                        }
                        qtdeMesesDescto = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0) > 0 ? qtdeMesesDescto - 1 : qtdeMesesDescto;
                    }
                }

                grdNegociacao.DataSource = Dt;
                grdNegociacao.DataBind();

                txtTotalMensa.Text = totalMensa.ToString("N2");
                txtTotalDesctoBolsa.Text = totalDescto.ToString("N2");
                txtTotalDesctoEspec.Text = totalDesctoEspec.ToString("N2");
                txtTotalLiquiContra.Text = totalValorLiqui.ToString("N2");
            }
        }

        /// <summary>
        /// Método que carrega informações do responsável
        /// </summary>
        /// <param name="resp">Id do responsável</param>
        /// <param name="CPFResp">CPF do responsável</param>
        protected void CarregaResponsaveis(int resp, string CPFResp)
        {
            TB108_RESPONSAVEL tb108 = (from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                       where resp != 0 ? lTb108.CO_RESP == resp : resp == 0
                                       && CPFResp != "" ? lTb108.NU_CPF_RESP == CPFResp : CPFResp == ""
                                       && lTb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                       select lTb108).FirstOrDefault<TB108_RESPONSAVEL>();

            if (tb108 != null)
            {
                if (CPFResp == "")
                    this.txtCPFResp.Text = tb108.NU_CPF_RESP;

                tb108.ImageReference.Load();
                //this.ControleImagemResp.ImagemLargura = 64;
                //this.ControleImagemResp.ImagemAltura = 85;
                //this.ControleImagemResp.CarregaImagem(0);
                if (tb108.Image != null)
                {
                    if (System.IO.File.Exists(@"" + HttpRuntime.AppDomainAppPath + "Imagens\\Responsavel\\" + tb108.NU_CPF_RESP + ".JPG"))
                    {
                        imgResp.Src = "../../../../../Imagens/Responsavel/" + tb108.NU_CPF_RESP + ".JPG";
                    }
                    else
                    {
                        imgResp.Src = "../../../../../Library/IMG/Gestor_SemImagem.png";
                    }

                    //this.ControleImagemR.ImageId = tb108.Image.ImageId;
                    //this.ControleImagemR.CarregaImagem();
                    //this.ControleImagemR.CarregaImagem(tb108.Image.ImageId);
                }
                else
                {
                    imgResp.Src = "../../../../../Library/IMG/Gestor_SemImagem.png";
                }

                //this.ddlOrigemResp.SelectedValue = tb108.CO_ORIGEM_RESP;
                this.hdCodResp.Value = tb108.CO_RESP.ToString();
                this.txtNoRespCPF.Text = tb108.NO_RESP.ToUpper();
                this.txtCPFRespDados.Text = tb108.NU_CPF_RESP;
                this.txtNomeResp.Text = tb108.NO_RESP.ToUpper();
                this.txtNISResp.Text = tb108.NU_NIS_RESP.ToString();
                this.txtDtNascResp.Text = tb108.DT_NASC_RESP != null ? tb108.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                this.ddlSexoResp.SelectedValue = tb108.CO_SEXO_RESP;
                this.ddlEstadoCivilResp.SelectedValue = tb108.CO_ESTADO_CIVIL_RESP != null ? tb108.CO_ESTADO_CIVIL_RESP : "";
                //this.ddlDeficienciaResp.SelectedValue = tb108.TP_DEF_RESP;
                //this.txtPassaporteResp.Text = tb108.NU_PASSAPORTE_RESP != null ? tb108.NU_PASSAPORTE_RESP.ToString() : "";
                this.chkRespFunc.Checked = tb108.CO_FLAG_RESP_FUNC == "S";
                this.ddlGrauInstrucaoResp.SelectedValue = tb108.CO_INST.ToString();
                this.ddlTpSangueResp.SelectedValue = tb108.CO_TIPO_SANGUE_RESP != null ? tb108.CO_TIPO_SANGUE_RESP.Trim() : "";
                this.ddlStaTpSangueResp.SelectedValue = tb108.CO_STATUS_SANGUE_RESP != null ? tb108.CO_STATUS_SANGUE_RESP : "";
                this.ddlNacioResp.SelectedValue = tb108.CO_NACI_RESP;
                this.txtMaeResp.Text = tb108.NO_MAE_RESP;
                //this.txtPaiResp.Text = tb108.NO_PAI_RESP;
                //this.txtDepMenResp.Text = tb108.QT_MENOR_DEPEN_RESP != null ? tb108.QT_MENOR_DEPEN_RESP.ToString() : "";
                //this.txtDepMaiResp.Text = tb108.QT_MAIOR_DEPEN_RESP != null ? tb108.QT_MAIOR_DEPEN_RESP.ToString() : "";
                this.txtMesAnoTrabResp.Text = tb108.CO_MESANO_TRABALHO == null || tb108.CO_MESANO_TRABALHO == "" ? "" : (tb108.CO_MESANO_TRABALHO.Substring(5, 2) + "/" + tb108.CO_MESANO_TRABALHO.Substring(0, 4));
                //this.txtQtdAnoResidResp.Text = tb108.QT_ANOS_RESI != null ? tb108.QT_ANOS_RESI.ToString() : "";
                tb108.TB15_FUNCAOReference.Load();
                this.ddlProfissaoResp.SelectedValue = tb108.TB15_FUNCAO != null ? tb108.TB15_FUNCAO.CO_FUN.ToString() : "";
                //this.ddlRendaResp.SelectedValue = tb108.RENDA_FAMILIAR_RESP;
                this.txtLogradouroResp.Text = tb108.DE_ENDE_RESP;
                this.txtNumeroResp.Text = tb108.NU_ENDE_RESP.ToString();
                this.txtComplementoResp.Text = tb108.DE_COMP_RESP;
                this.ddlUfResp.SelectedValue = tb108.CO_ESTA_RESP;
                //this.CarregaCidades(this.ddlCidadeResp, this.ddlUfResp);
                //this.ddlCidadeResp.SelectedValue = tb108.CO_CIDADE.ToString();
                carregaCidadesRefeita();

                //Verifica se existe na lista de cidades, uma cidade de acordo com a UF no cadastro do responsável, caso não exista, apresenta uma mensagem de erro tratada.
                ListItem liCR = ddlCidadeResp.Items.FindByValue(tb108.CO_CIDADE.ToString());
                if (liCR != null)
                {
                    ddlCidadeResp.SelectedValue = tb108.CO_CIDADE.ToString();
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade no cadastro do responsável não condiz com a UF no cadastro do mesmo, favor informar a correta.");
                    ddlCidadeResp.Focus();
                }

                //this.CarregaBairros(this.ddlBairroResp, this.ddlCidadeResp);
                //this.ddlBairroResp.SelectedValue = tb108.CO_BAIRRO.ToString();
                CarregaBairrosRefeito();

                //Verifica se existe na lista de bairros, um bairros de acordo com a Cidade no cadastro do responsável, caso não exista, apresenta uma mensagem de erro tratada.
                ListItem liBR = ddlBairroResp.Items.FindByValue(tb108.CO_BAIRRO.ToString());
                if (liBR != null)
                {
                    ddlBairroResp.SelectedValue = tb108.CO_BAIRRO.ToString();
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro no cadastro do responsável não condiz com a Cidade no cadastro do mesmo, favor informar o correto.");
                    ddlBairroResp.Focus();
                }

                this.txtCepResp.Text = tb108.CO_CEP_RESP;
                this.txtTelCelularResp.Text = VerificaNonoDigitoTamanhoCampo(tb108.NU_TELE_CELU_RESP);
                this.txtTelCelularRespNonoDig.Text = VerificaNonoDigitoTamanhoCampo(tb108.NU_TELE_CELU_RESP);
                this.txtTelResidencialResp.Text = tb108.NU_TELE_RESI_RESP;
                this.txtEmailResp.Text = tb108.DES_EMAIL_RESP;
                this.txtNaturalidadeResp.Text = tb108.DE_NATU_RESP;
                this.CarregaUfs(this.ddlUfNacionalidadeResp);
                this.ddlUfNacionalidadeResp.SelectedValue = tb108.CO_UF_NATU_RESP;
                this.txtIdentidadeResp.Text = tb108.CO_RG_RESP;
                this.txtDtEmissaoResp.Text = tb108.DT_EMIS_RG_RESP != null ? tb108.DT_EMIS_RG_RESP.Value.ToString("dd/MM/yyyy") : "";
                this.txtOrgEmissorResp.Text = tb108.CO_ORG_RG_RESP;
                this.CarregaUfs(this.ddlIdentidadeUFResp);
                this.ddlIdentidadeUFResp.SelectedValue = tb108.CO_ESTA_RG_RESP;
                this.txtNomeEmpresaResp.Text = tb108.NO_EMPR_RESP;
                //this.txtNumeroTituloResp.Text = tb108.NU_TIT_ELE;
                //this.txtZonaResp.Text = tb108.NU_ZONA_ELE;
                //this.txtSecaoResp.Text = tb108.NU_SEC_ELE;
                //this.CarregaUfs(this.ddlUfTituloResp);

                //if ((tb108.CO_UF_TIT_ELE_RESP != null) && (tb108.CO_UF_TIT_ELE_RESP.Replace(" ", "") != ""))
                //    this.ddlUfTituloResp.SelectedValue = tb108.CO_UF_TIT_ELE_RESP;

                //this.txtLogradouroEmpResp.Text = tb108.DE_ENDE_EMPRE_RESP;
                //this.txtNumeroEmpResp.Text = tb108.NU_ENDE_EMPRE_RESP.ToString();
                //this.txtComplementoEmpResp.Text = tb108.DE_COMP_EMPRE_RESP;
                this.CarregaUfs(this.ddlUfEmpResp);
                this.ddlUfEmpResp.SelectedValue = tb108.TB74_UF != null ? tb108.TB74_UF.CODUF : "";
                if (ddlUfEmpResp.SelectedValue != "")
                {
                    this.CarregaCidades(this.ddlCidadeEmpResp, this.ddlUfEmpResp);

                    tb108.TB904_CIDADEReference.Load();
                    if (tb108.TB904_CIDADE != null)
                    {
                        ListItem liciE = ddlCidadeEmpResp.Items.FindByValue(tb108.TB904_CIDADE.CO_CIDADE.ToString());
                        if (liciE != null)
                        {
                            ddlCidadeEmpResp.SelectedValue = tb108.TB904_CIDADE.CO_CIDADE.ToString();
                        }
                        else
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade da Empresa no cadastro do responsável não condiz com a Cidade no cadastro do mesmo, favor informar a correta.");
                            ddlBairroResp.Focus();
                        }
                    }

                    //this.ddlCidadeEmpResp.SelectedValue = tb108.TB904_CIDADE != null ? tb108.TB904_CIDADE.CO_CIDADE.ToString() : "";
                    //this.CarregaBairros(this.ddlBairroEmpResp, this.ddlCidadeEmpResp);
                    //this.ddlBairroEmpResp.SelectedValue = tb108.CO_BAIRRO_EMPRE_RESP != null ? tb108.CO_BAIRRO_EMPRE_RESP.ToString() : "";
                }
                this.txtCepEmpresaResp.Text = tb108.CO_CEP_EMPRE_RESP;
                this.txtTelEmpresaResp.Text = tb108.NU_TELE_COME_RESP;
            }
            else
                //Atribui automaticamente o CPF pesquisado ao campo correspondente no cadastro, quando o CPF pesquisado não existir
                txtCPFRespDados.Text = txtCPFResp.Text;
        }

        /// <summary>
        /// Verifica se a empresa logada está habilitada para nove dígitos nos telefones, caso esteja verifica se o número recebido como parâmetro possui 10 caracteres no total, incluindo o DDD, caso possua adiciona um dígito novo 9 no retorno, caso contrário retorna o número puro.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private string VerificaNonoDigitoTamanhoCampo(string numero)
        {
            string result = "";
            if (LoginAuxili.FL_NONO_DIGITO_TELEF == "S")
            {
                if (!string.IsNullOrEmpty(numero))
                {
                    if (numero.Length == 10)
                    {
                        string auxNU, auxNU1;
                        auxNU = numero.Substring(2, 2);
                        auxNU1 = numero.Substring(2, 1);
                        //Verifica se o número se inicia com os dígitos que receberam a adaptação dos nove dígitos, segundo cartilha da Anatel.
                        if ((auxNU1 == "6") || (auxNU1 == "8") || (auxNU1 == "9") || (auxNU == "70") || (auxNU == "71") || (auxNU == "72") || (auxNU == "72") || (auxNU == "73") || (auxNU == "74") || (auxNU == "75") || (auxNU == "76") || (auxNU == "79"))
                        {
                            //numero.Insert(2, "9");
                            string part1 = numero.Substring(0, 2);
                            string part2 = numero.Substring(2, 8);
                            result = part1 + "9" + part2;
                        }
                    }
                }
            }
            if (result != "")
                return result;
            else
                return numero;
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Medida
        /// </summary>
        private void CarregaUnidadesMedidas()
        {
            ddlUniCEA.DataSource = TB89_UNIDADES.RetornaTodosRegistros().Where(u => (u.FL_CATEG_UNID == "T") || (u.FL_CATEG_UNID == "S")).OrderBy(u => u.SG_UNIDADE);

            ddlUniCEA.DataTextField = "SG_UNIDADE";
            ddlUniCEA.DataValueField = "CO_UNID_ITEM";
            ddlUniCEA.DataBind();
        }

        /// <summary>
        /// Método que carrega informações do aluno
        /// </summary>
        /// <param name="coAlu">Id do aluno</param>
        /// <param name="coEmp">Id da unidade</param>
        /// <param name="nuNire">Número NIRE do aluno</param>
        protected void CarregaAluno(int coAlu, int coEmp, int nuNire)
        {
            var tb07 = (from ltb07 in TB07_ALUNO.RetornaTodosRegistros()
                        where (coAlu != 0 ? ltb07.CO_ALU == coAlu : coAlu == 0)
                        && (coEmp != 0 ? ltb07.CO_EMP == coEmp : coEmp == 0)
                        && (nuNire != 0 ? ltb07.NU_NIRE == nuNire : nuNire == 0)
                        select ltb07).FirstOrDefault();

            if (tb07 != null)
            {
                this.txtCertNascAlu.Text = tb07.NU_CERT;
                this.txtRGAluno.Text = tb07.CO_RG_ALU;
                this.txtOrgEmiRGAlu.Text = tb07.CO_ORG_RG_ALU;

                tb07.ImageReference.Load();
                tb07.TB108_RESPONSAVELReference.Load();
                tb07.TB905_BAIRROReference.Load();

                this.hdCodAluno.Value = tb07.CO_ALU.ToString();
                this.ControleImagemAluno.ImagemLargura = 64;
                this.ControleImagemAluno.ImagemAltura = 85;
                if (tb07.Image != null)
                {
                    this.ControleImagemAluno.ImageId = tb07.Image.ImageId;
                    this.ControleImagemAluno.CarregaImagem(tb07.Image.ImageId);
                }
                else
                    this.ControleImagemAluno.CarregaImagem(0);

                this.txtNomeAluno.Text = tb07.NO_ALU.ToUpper();
                this.txtNireAluno.Text = tb07.NU_NIRE.ToString().PadLeft(9, '0');
                this.ddlSexoAluno.SelectedValue = tb07.CO_SEXO_ALU;
                this.txtDataNascimentoAluno.Text = tb07.DT_NASC_ALU != null ? tb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                this.ddlTpSangueAluno.SelectedValue = tb07.CO_TIPO_SANGUE_ALU != null ? tb07.CO_TIPO_SANGUE_ALU.Trim() : "";
                this.ddlStaSangueAluno.SelectedValue = tb07.CO_STATUS_SANGUE_ALU != null ? tb07.CO_STATUS_SANGUE_ALU : "";
                this.ddlDeficienciaAluno.SelectedValue = tb07.TP_DEF;
                this.ddlNacionalidadeAluno.SelectedValue = tb07.CO_NACI_ALU;
                this.txtNaturalidadeAluno.Text = tb07.DE_NATU_ALU;
                this.ddlUfNacionalidadeAluno.SelectedValue = tb07.CO_UF_NATU_ALU;
                this.txtTelCelularAluno.Text = VerificaNonoDigitoTamanhoCampo(tb07.NU_TELE_CELU_ALU);
                this.txtTelCelularAlunoNoveDigit.Text = VerificaNonoDigitoTamanhoCampo(tb07.NU_TELE_CELU_ALU);
                this.txtTelResidencialAluno.Text = tb07.NU_TELE_RESI_ALU;
                this.txtEmailAluno.Text = tb07.NO_WEB_ALU;
                this.txtNomePaiAluno.Text = tb07.NO_PAI_ALU;
                this.txtNomeMaeAlunoValid.Text = tb07.NO_MAE_ALU;
                this.txtApelAluno.Text = tb07.NO_APE_ALU != null ? tb07.NO_APE_ALU : "";
                this.txtCepAluno.Text = tb07.CO_CEP_ALU;
                this.txtLogradouroAluno.Text = tb07.DE_ENDE_ALU;
                this.txtNumeroAluno.Text = tb07.NU_ENDE_ALU != null ? tb07.NU_ENDE_ALU.ToString() : "";
                this.ddlUFAluno.SelectedValue = tb07.CO_ESTA_ALU;
                this.CarregaCidades(this.ddlCidadeAluno, this.ddlUFAluno);
                ListItem liCR = ddlCidadeAluno.Items.FindByValue((tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_CIDADE.ToString() : ""));
                if (liCR != null)
                {
                    this.ddlCidadeAluno.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                }
                this.CarregaBairros(this.ddlBairroAluno, this.ddlCidadeAluno);
                ListItem liBR = ddlBairroAluno.Items.FindByValue((tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_BAIRRO.ToString() : ""));
                if (liBR != null)
                {
                    this.ddlBairroAluno.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_BAIRRO.ToString() : "";
                }
                this.txtCpfAluno.Text = tb07.NU_CPF_ALU;
                this.ddlPermiSaidaAluno.SelectedValue = tb07.FL_AUTORI_SAIDA != null ? tb07.FL_AUTORI_SAIDA : "N";
                tb07.TB148_TIPO_BOLSAReference.Load();
                if (tb07.TB148_TIPO_BOLSA != null)
                {
                    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                                 where iTb148.CO_TIPO_BOLSA == tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA
                                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.TP_GRUPO_BOLSA }).FirstOrDefault();

                }
                else
                {
                    CarregaBolsas();
                }

                string passeEsolar = "false";
                if (tb07.FLA_PASSE_ESCOLA != null)
                {
                    if (tb07.FLA_PASSE_ESCOLA.Value)
                    {
                        passeEsolar = "true";
                    }
                    else
                    {
                        passeEsolar = "false";
                    }
                }
                this.ddlPasseEscolarAluno.SelectedValue = passeEsolar;
                this.ddlTransporteEscolarAluno.SelectedValue = tb07.FLA_TRANSP_ESCOLAR != null ? tb07.FLA_TRANSP_ESCOLAR.ToString() : "N";
                this.HabilitaCamposBolsa();
            }
            else
            {
                // Limpa campos caso não retorne nenhum aluno pelo NIRE

                this.hdCodAluno.Value = "";
                this.ControleImagemAluno.ImagemLargura = 64;
                this.ControleImagemAluno.ImagemAltura = 85;
                //if (tb07.Image != null)
                //{
                //    this.ControleImagemAluno.ImageId = tb07.Image.ImageId;
                //    this.ControleImagemAluno.CarregaImagem(tb07.Image.ImageId);
                //}
                //else
                this.ControleImagemAluno.CarregaImagem(0);

                this.txtNomeAluno.Text = "";
                this.txtNireAluno.Text = "";
                this.ddlSexoAluno.SelectedValue = "";
                this.txtDataNascimentoAluno.Text = "";
                this.ddlTpSangueAluno.SelectedValue = "";
                this.ddlStaSangueAluno.SelectedValue = "";
                this.ddlDeficienciaAluno.SelectedValue = "N";
                this.ddlNacionalidadeAluno.SelectedValue = "B";
                this.txtNaturalidadeAluno.Text = "";
                this.ddlUfNacionalidadeAluno.SelectedValue = "";
                this.txtTelCelularAluno.Text = "";
                this.txtTelCelularAlunoNoveDigit.Text = "";
                this.txtTelResidencialAluno.Text = "";
                this.txtEmailAluno.Text = "";
                this.txtNomeMaeAlunoValid.Text = "";
                this.txtNomePaiAluno.Text = "";
                this.txtOrgEmiRGAlu.Text = "";
                this.txtRGAluno.Text = "";
                this.txtCertNascAlu.Text = "";
                this.txtApelAluno.Text = "";
                this.txtCepAluno.Text = "";
                this.txtLogradouroAluno.Text = "";
                this.txtNumeroAluno.Text = "";
                this.ddlUFAluno.SelectedValue = "";
                this.CarregaCidades(this.ddlCidadeAluno, this.ddlUFAluno);
                this.ddlCidadeAluno.SelectedValue = "";
                this.CarregaBairros(this.ddlBairroAluno, this.ddlCidadeAluno);
                this.ddlBairroAluno.SelectedValue = "";
                this.txtCpfAluno.Text = "";
                this.ddlPermiSaidaAluno.SelectedValue = "N";
                //tb07.TB148_TIPO_BOLSA = null;
                //if (tb07.TB148_TIPO_BOLSA != null)
                //{
                //    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //                 where iTb148.CO_TIPO_BOLSA == tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA
                //                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.TP_GRUPO_BOLSA }).FirstOrDefault();

                //}
                //else
                //{
                CarregaBolsas();
                //}

                this.ddlPasseEscolarAluno.SelectedValue = "false";
                this.ddlTransporteEscolarAluno.SelectedValue = "N";
                this.HabilitaCamposBolsa();
            }
            //UpdatePanel1.Update();
        }

        /// <summary>
        /// Método que carrega o dropdown de Profissões
        /// </summary>
        private void CarregaCursosFormacao()
        {
            this.ddlProfissaoResp.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

            this.ddlProfissaoResp.DataTextField = "NO_FUN";
            this.ddlProfissaoResp.DataValueField = "CO_FUN";
            this.ddlProfissaoResp.DataBind();

            this.ddlProfissaoResp.Enabled = this.ddlProfissaoResp.Items.Count > 0;
            this.ddlProfissaoResp.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Endereço
        /// </summary>
        private void CarregaTipoEndereço()
        {
            ddlTpEnderETA.DataSource = TB238_TIPO_ENDERECO.RetornaTodosRegistros().Where(t => t.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && t.CO_SITUACAO == "A");

            ddlTpEnderETA.DataTextField = "NM_TIPO_ENDERECO";
            ddlTpEnderETA.DataValueField = "ID_TIPO_ENDERECO";
            ddlTpEnderETA.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Telefone
        /// </summary>
        private void CarregaTipoTelefone()
        {
            ddlTpTelef.DataSource = TB239_TIPO_TELEFONE.RetornaTodosRegistros().Where(t => t.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && t.CO_SITUACAO == "A");

            ddlTpTelef.DataTextField = "NM_TIPO_TELEFONE";
            ddlTpTelef.DataValueField = "ID_TIPO_TELEFONE";
            ddlTpTelef.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        /// <param name="ddlCidade">DropDown de cidade</param>
        /// <param name="ddlUF">DropDown de UF</param>
        private void CarregaCidades(DropDownList ddlCidade, DropDownList ddlUF)
        {
            //ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            //ddlCidade.DataTextField = "NO_CIDADE";
            //ddlCidade.DataValueField = "CO_CIDADE";
            //ddlCidade.DataBind();

            //ddlCidade.Enabled = ddlCidade.Items.Count > 0;
            //ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, ddlUF.SelectedValue, LoginAuxili.CO_EMP, true);
        }

        private void carregaCidadesRefeita()
        {
            string coufResp = ddlUfResp.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidadeResp, false, coufResp, LoginAuxili.CO_EMP, true);
            CarregaBairrosRefeito();
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairro
        /// </summary>
        /// <param name="ddlBairro">DropDown de bairro</param>
        /// <param name="ddlCidade">DropDown de cidade</param>
        private void CarregaBairros(DropDownList ddlBairro, DropDownList ddlCidade)
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

                ddlBairro.Enabled = ddlBairro.Items.Count > 0;
                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        private void CarregaBairrosRefeito()
        {
            int cocid = ddlCidadeResp.SelectedValue != "" ? int.Parse(ddlCidadeResp.SelectedValue) : 0;

            if (cocid != 0)
            {
                var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                           where tb905.TB904_CIDADE.CO_CIDADE == cocid
                           select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO });

                ddlBairroResp.DataValueField = "CO_BAIRRO";
                ddlBairroResp.DataTextField = "NO_BAIRRO";
                ddlBairroResp.DataSource = res;
                ddlBairroResp.DataBind();

                ddlBairroResp.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlBairroResp.Items.Clear();
                ddlBairroResp.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Grau de Instrução
        /// </summary>
        private void CarregaGrausInstrucao()
        {
            ddlGrauInstrucaoResp.DataSource = TB18_GRAUINS.RetornaTodosRegistros().OrderBy(w => w.NU_CLASSI_IMPRES);

            ddlGrauInstrucaoResp.DataTextField = "NO_INST";
            ddlGrauInstrucaoResp.DataValueField = "CO_INST";
            ddlGrauInstrucaoResp.DataBind();

            //Verifica se existe e seleciona a opção "Não Informada" de padrão
            int? idGrau = TB18_GRAUINS.RetornaTodosRegistros().Where(o => o.CO_SIGLA_INST == "NIF").FirstOrDefault().CO_INST;
            if (idGrau.HasValue)
            {
                ListItem li = ddlGrauInstrucaoResp.Items.FindByValue(idGrau.Value.ToString());
                if (li != null)
                    ddlGrauInstrucaoResp.SelectedValue = idGrau.Value.ToString();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Nacionalidades
        /// </summary>
        private void CarregaNacionalidades()
        {
            ddlNacioResp.DataSource = TB299_PAISES.RetornaTodosRegistros();

            ddlNacioResp.DataTextField = "NO_PAISES";
            ddlNacioResp.DataValueField = "CO_ISO_PAISES";
            ddlNacioResp.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddlUF">DropDown de UF</param>
        private void CarregaUfs(DropDownList ddlUF)
        {
            AuxiliCarregamentos.CarregaUFs(ddlUF, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Bolsa
        /// </summary>
        private void CarregaBolsas()
        {
            //ddlBolsaAluno.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTipoBolsa.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
            //    && c.CO_SITUA_TIPO_BOLSA == "A");

            //ddlBolsaAluno.DataTextField = "NO_TIPO_BOLSA";
            //ddlBolsaAluno.DataValueField = "CO_TIPO_BOLSA";
            //ddlBolsaAluno.DataBind();

            //ddlBolsaAluno.Items.Insert(0, new ListItem("Nenhuma", ""));

            //txtDescontoAluno.Text = txtPeriodoDeIniBolAluno.Text = txtPeriodoTerBolAluno.Text = "";
            //chkDesctoPercBolsa.Checked = chkDesctoPercBolsa.Enabled =
            //txtDescontoAluno.Enabled = txtPeriodoDeIniBolAluno.Enabled = txtPeriodoTerBolAluno.Enabled = false;
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Bolsa
        /// </summary>
        private void CarregaBolsasAlt()
        {
            ddlBolsaAlunoAlt.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTpBolsaAlt.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                && c.CO_SITUA_TIPO_BOLSA == "A");

            ddlBolsaAlunoAlt.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataBind();

            ddlBolsaAlunoAlt.Items.Insert(0, new ListItem("Nenhuma", ""));
            ddlBolsaAlunoAlt.Items.Insert(1, new ListItem("Livre", "0"));

            txtValorDescto.Text = txtPeriodoIniDesconto.Text = txtPeriodoFimDesconto.Text = "";
            chkManterDescontoPerc.Checked = chkManterDescontoPerc.Enabled =
            txtValorDescto.Enabled = txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = false;
        }

        /// <summary>
        /// Método que habilita campos da gride
        /// </summary>
        protected void HabilitaCamposGride()
        {
            int retornaInt = 0;
            decimal dcmValor = 0;
            decimal valorTotal = 0;

            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                //------------> Faz a verificação dos itens marcados na Grid de Itens Emprestados
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Enabled = true;
                    if (((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text == "")
                    {
                        ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text = "1";
                        //qtdTotalItem = qtdTotalItem + 1;
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                            valorTotal = valorTotal + dcmValor;
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                    else
                    {
                        if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text, out retornaInt))
                        {
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                if (retornaInt > 0)
                                {
                                    //qtdTotalItem = qtdTotalItem + retornaInt;
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                    valorTotal = valorTotal + (dcmValor * retornaInt);
                                }
                                else
                                {
                                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor).ToString("#,##0.00");
                                    valorTotal = valorTotal + dcmValor;
                                }
                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                        else
                        {
                            ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                            //qtdTotalItem = qtdTotalItem + 1;
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                valorTotal = valorTotal + dcmValor;
                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                    }
                }
                else
                {
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Enabled = false;
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "";
                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                }
            }

            txtValorTotal.Text = valorTotal.ToString("#,##0.00");
        }

        /// <summary>
        /// Método que calcula o valor total da linha gride
        /// </summary>
        protected void CalculaValorTotal()
        {
            int retornaInt = 0;
            decimal dcmValor = 0;
            decimal valorTotal = 0;

            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                //------------> Faz a verificação dos itens marcados na Grid de Itens Emprestados
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text, out retornaInt))
                    {
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            if (retornaInt > 0)
                            {
                                //qtdTotalItem = qtdTotalItem + retornaInt;
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                valorTotal = valorTotal + (dcmValor * retornaInt);
                            }
                            else
                            {
                                ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                                //qtdTotalItem = qtdTotalItem + 1;
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor).ToString("#,##0.00");
                                valorTotal = valorTotal + dcmValor;
                            }
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                    else
                    {
                        ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                        //qtdTotalItem = qtdTotalItem + 1;
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                            valorTotal = valorTotal + dcmValor;
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                }
            }

            txtValorTotal.Text = valorTotal.ToString("#,##0.00");
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

            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
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

            ddlSubGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Conta Contábil
        /// </summary>
        private void CarregaContasContabeis(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlCtaContabil)
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            ddlCtaContabil.DataSource = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                                         where tb56.TB54_SGRP_CTA.TB53_GRP_CTA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         && tb56.TB54_SGRP_CTA.TB53_GRP_CTA.CO_GRUP_CTA == coGrupo
                                         && tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSubGrupo
                                         select new { tb56.CO_SEQU_PC, tb56.DE_CONTA_PC }).OrderBy(p => p.DE_CONTA_PC);

            ddlCtaContabil.DataTextField = "DE_CONTA_PC";
            ddlCtaContabil.DataValueField = "CO_SEQU_PC";
            ddlCtaContabil.DataBind();

            ddlCtaContabil.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Histórico
        /// </summary>
        private void CarregaHistoricos()
        {
            ddlHistorico.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                       where tb39.FLA_TIPO_HISTORICO == "C"
                                       select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataBind();

            ddlHistorico.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, caso seja do curso extra, não carrega o curso recebido como parâmetro
        /// </summary>
        private void CarregaSerieCurso(int? CO_CUR = null, bool CursoExtra = false)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = PreAuxili.proximoAnoMat<string>(txtAno.Text);

            if (CursoExtra == false)
            {
                if (modalidade != 0)
                {
                    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                                where tb01.CO_MODU_CUR == modalidade && tb01.CO_SITU == "A"
                                                join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                                where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                                select new { tb01.CO_CUR, tb01.NO_CUR, tb43.TB44_MODULO.DE_MODU_CUR }).Distinct().OrderBy(w => w.DE_MODU_CUR).ThenBy(c => c.NO_CUR);

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataBind();
                }
                else
                    ddlSerieCurso.Items.Clear();

                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                if (modalidade != 0)
                {
                    ddlCurExt.DataSource = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb44.CO_MODU_CUR equals tb01.CO_MODU_CUR
                                            where tb01.CO_EMP == LoginAuxili.CO_EMP
                                            && tb01.CO_CUR != CO_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR, tb44.DE_MODU_CUR }).Distinct().OrderBy(o => o.DE_MODU_CUR).ThenBy(o => o.NO_CUR).ToList();

                    ddlCurExt.DataTextField = "NO_CUR";
                    ddlCurExt.DataValueField = "CO_CUR";
                    ddlCurExt.DataBind();
                }
                else
                    ddlCurExt.Items.Clear();

                ddlCurExt.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new
                                       {
                                           /*CO_SIGLA_TURMA = tb06.TB129_CADTURMAS.CO_SIGLA_TURMA + " (" +
                                           (tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER != null ?
                                           (tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 1 ? "0T202" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 2 ? "0T302" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 3 ? "0T402" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 4 ? "0T403" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 5 ? "0M503" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 6 ? "0T502" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 7 ? "0T503" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 8 ? "1M204" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 9 ? "1T202" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 10 ? "1T203" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 11 ? "1T902" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 12 ? "2M101" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 13 ? "2M103" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 14 ? "2M201" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 15 ? "2M203" :
                                            "2M301") : tb06.TB129_CADTURMAS.CO_SIGLA_TURMA) + ")",*/
                                           tb06.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                           tb06.CO_TUR
                                       }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que preenche a grid de documentos
        /// </summary>
        private void CarregaGridDocumentos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int serie = ddlSerieCurso.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSerieCurso.SelectedValue);
            int modalidade = ddlModalidade.SelectedValue == "" ? 0 : Convert.ToInt32(ddlModalidade.SelectedValue);

            if (serie != 0 && modalidade != 0)
            {
                grdDocumentos.DataSource = (from tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros()
                                            join tb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros() on tb121.CO_TP_DOC_MAT equals tb208.CO_TP_DOC_MAT
                                            where tb208.CO_EMP == coEmp && tb208.CO_CUR == serie && tb208.CO_MODU_CUR == modalidade
                                            select tb121).OrderBy(p => p.DE_TP_DOC_MAT);

                grdDocumentos.DataBind();
            }
        }

        /// <summary>
        /// Método que carrega a Gride de Solicitações
        /// </summary>
        private void CarregaGrideSolicitacao()
        {
            var lstTb66 = from tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros()
                          where tb66.FL_ITEM_MATRIC_TPSOLIC == "S"
                          select new
                          {
                              Codigo = tb66.CO_TIPO_SOLI,
                              Descricao = tb66.NO_TIPO_SOLI,
                              Valor = tb66.VL_UNIT_SOLI != null ? tb66.VL_UNIT_SOLI.Value : 0,
                              DescUnidade = tb66.TB89_UNIDADES != null ? tb66.TB89_UNIDADES.SG_UNIDADE : "",
                              Qtde = "",
                              Total = "",
                              Inclu = true,
                              Checked = false,
                              txMatric = tb66.FL_TAXA_MATRIC
                          };

            grdSolicitacoes.DataKeyNames = new string[] { "Codigo" };
            grdSolicitacoes.DataSource = lstTb66;
            grdSolicitacoes.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Atividades Extras
        /// </summary>
        private void CarregaAtividade()
        {
            ddlAtivExtra.DataSource = TB105_ATIVIDADES_EXTRAS.RetornaTodosRegistros();

            ddlAtivExtra.DataValueField = "CO_ATIV_EXTRA";
            ddlAtivExtra.DataTextField = "DES_ATIV_EXTRA";
            ddlAtivExtra.DataBind();

            if (ddlAtivExtra.SelectedValue != "")
            {
                int codAE = int.Parse(ddlAtivExtra.SelectedValue);

                var tb105 = (from lTb105 in TB105_ATIVIDADES_EXTRAS.RetornaTodosRegistros()
                             where lTb105.CO_ATIV_EXTRA == codAE
                             select new { lTb105.SIGLA_ATIV_EXTRA, lTb105.VL_ATIV_EXTRA }).FirstOrDefault();

                if (tb105 != null)
                {
                    txtSiglaAEA.Text = tb105.SIGLA_ATIV_EXTRA;
                    txtValorAEA.Text = tb105.VL_ATIV_EXTRA != null ? tb105.VL_ATIV_EXTRA.ToString() : "";
                }
                else
                    txtSiglaAEA.Text = txtValorAEA.Text = "";
            }
        }

        /// <summary>
        /// Carrega os tipo de contrato de pagamento de curso
        /// </summary>
        private void CarregaTipoContrato()
        {
            ddlTipoContrato.Items.Clear();
            ddlTipoContrato.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoContratoCurso.ResourceManager));
        }

        /// <summary>
        /// Carrega os tipos de períodos para base do tipo de valor para pagamento do curso
        /// </summary>
        private void CarregaTipoValor()
        {
            ddlTipoValorCurso.Items.Clear();
            ddlTipoValorCurso.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoValorCurso.ResourceManager));
            ddlTipoValorCurso.SelectedValue = tipoValor[tipoValorCurso.P];
        }

        /// <summary>
        /// Carrega os dias de vencimento para as mensalidades da matrícula (Será alterado para buscar em uma tabela no banco)
        /// </summary>
        private void CarregaDiasVencimento()
        {
            ddlDiaVecto.Items.Clear();
            ddlDiaVecto.Items.AddRange(AuxiliBaseApoio.DiaVencimentoTitulo());
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serie = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            var curso = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            int diaVencimento = ((curso != null && curso.NU_MDV != null) ? (curso.NU_MDV ?? 5) : 5);
            if (ddlDiaVecto.Items.FindByValue(diaVencimento.ToString()) != null)
                ddlDiaVecto.SelectedValue = diaVencimento.ToString();
        }
        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coBoleto = (TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, int.Parse(ddlModalidade.SelectedValue), int.Parse(ddlSerieCurso.SelectedValue)).ID_BOLETO_MATR ?? 0);
            int coMod = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);

            AuxiliCarregamentos.CarregaBoletos(ddlBoleto, coEmp, "E", coMod, coCur, false, false);

            ddlBoleto.Items.Insert(0, new ListItem("Nenhum", ""));

            if (coBoleto > 0 && ddlBoleto.Items.FindByValue(coBoleto.ToString()) != null)
                ddlBoleto.SelectedValue = coBoleto.ToString();

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoSolic, coEmp, "E", coMod, coCur, false, false);
            
            ddlBoletoSolic.Items.Insert(0, new ListItem("", ""));

            if (coBoleto > 0 && ddlBoletoSolic.Items.FindByValue(coBoleto.ToString()) != null)
                ddlBoletoSolic.SelectedValue = coBoleto.ToString();

            var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                        where iTb83.CO_EMP == coEmp
                        select new { iTb83.ID_BOLETO_MATRIC, iTb83.ID_BOLETO_MENSA }).FirstOrDefault();

            if (tb83 != null)
            {
                if (tb83.ID_BOLETO_MENSA != null)
                {
                    ddlBoleto.SelectedValue = tb83.ID_BOLETO_MENSA.ToString();
                }
            }


        }

        /// <summary>
        /// Método que carrega a grid de Atividades Extras
        /// </summary>
        /// <param name="coAlu">Id do aluno</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaGridAtivExtra(int coAlu, int coEmp)
        {
            grdAtividade.DataSource = (from tb106 in TB106_ATIVEXTRA_ALUNO.RetornaTodosRegistros()
                                       where tb106.CO_ALU == coAlu && tb106.CO_EMP == coEmp
                                       select new
                                       {
                                           tb106.TB105_ATIVIDADES_EXTRAS.DES_ATIV_EXTRA,
                                           tb106.TB105_ATIVIDADES_EXTRAS.SIGLA_ATIV_EXTRA,
                                           tb106.VL_ATIV_EXTRA,
                                           tb106.DT_VENC_ATIV,
                                           tb106.DT_INI_ATIV,
                                           tb106.CO_ALU,
                                           tb106.CO_EMP,
                                           tb106.CO_ATIV_EXTRA,
                                           tb106.CO_INSC_ATIV
                                       });
            grdAtividade.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid de Restrições Alimentares
        /// </summary>
        /// <param name="coAlu">Id do aluno</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaGridResAli(int coAlu, int coEmp)
        {
            grdRestrAlim.DataSource = from tb294 in TB294_RESTR_ALIMEN.RetornaTodosRegistros()
                                      where tb294.TB07_ALUNO.CO_ALU == coAlu && tb294.TB07_ALUNO.CO_EMP == coEmp
                                      select new
                                      {
                                          tb294.ID_RESTR_ALIMEN,
                                          tb294.NM_RESTR_ALIMEN,
                                          TP_RESTR_ALIMEN = tb294.TP_RESTR_ALIMEN.Equals("M") ? "Médica" : tb294.TP_RESTR_ALIMEN.Equals("A") ? "Alimentar" : tb294.TP_RESTR_ALIMEN.Equals("L") ? "Alergia" :
                                          tb294.TP_RESTR_ALIMEN.Equals("R") ? "Responsável" : "Outros",
                                          tb294.ID_REFER_GEDUC_RESTR_ALIMEN,
                                          tb294.DE_ACAO_RESTR_ALIMEN,
                                          tb294.DT_INICIO_RESTR_ALIMEN,
                                          tb294.DT_TERMI_RESTR_ALIMEN,
                                          CO_GRAU_RESTR_ALIMEN = tb294.CO_GRAU_RESTR_ALIMEN == "B" ? "Baixo Risco" : tb294.CO_GRAU_RESTR_ALIMEN == "A" ? "Alto Risco" : "Médio Risco"
                                      };

            grdRestrAlim.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid de Cuidados Especiais
        /// </summary>
        /// <param name="coAlu">Id do aluno</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaGridCudEsp(int coAlu, int coEmp)
        {
            grdCuiEsp.DataSource = from tb293 in TB293_CUIDAD_SAUDE.RetornaTodosRegistros()
                                   where tb293.TB07_ALUNO.CO_ALU == coAlu && tb293.TB07_ALUNO.CO_EMP == coEmp
                                   select new
                                   {
                                       tb293.ID_MEDICACAO,
                                       tb293.HR_APLICAC_CUIDADO,
                                       TP_APLICAC_CUIDADO = tb293.TP_APLICAC_CUIDADO.Equals("O") ? "Via Oral" : "",
                                       TP_CUIDADO = tb293.TP_CUIDADO_SAUDE.Equals("M") ? "Medicação" : tb293.TP_CUIDADO_SAUDE.Equals("A") ? "Acompanhamento" :
                                       tb293.TP_CUIDADO_SAUDE.Equals("C") ? "Curativo" : "Outras",
                                       tb293.NM_REMEDIO_CUIDADO,
                                       tb293.DE_DOSE_REMEDIO_CUIDADO,
                                       UNIDADE = tb293.TB89_UNIDADES.SG_UNIDADE,
                                       tb293.DE_OBSERV_CUIDADO,
                                       tb293.NM_MEDICO_CUIDADO,
                                       tb293.NR_CRM_MEDICO_CUIDADO,
                                       tb293.CO_UF_MEDICO,
                                       NR_TELEF_MEDICO = tb293.NR_TELEF_MEDICO.Length > 0 ? tb293.NR_TELEF_MEDICO.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                       tb293.DT_RECEITA_INI,
                                       tb293.DT_RECEITA_FIM,
                                       FL_RECEITA_CUIDADO = tb293.FL_RECEITA_CUIDADO == "S" ? "Sim" : "Não"
                                   };

            grdCuiEsp.DataBind();

            if (grdCuiEsp.Rows.Count > 0)
                grdCuiEsp.Width = 1100;
            else
                grdCuiEsp.Width = 680;
        }

        /// <summary>
        /// Método que carrega a grid de Telefones do Aluno
        /// </summary>
        /// <param name="coAlu">Id do aluno</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaGridTelefones(int coAlu, int coEmp)
        {
            var resultado = (from tb242 in TB242_ALUNO_TELEFONE.RetornaTodosRegistros()
                             where tb242.TB07_ALUNO.CO_ALU == coAlu && tb242.TB07_ALUNO.CO_EMP == coEmp
                             select new
                             {
                                 tb242.TB239_TIPO_TELEFONE.NM_TIPO_TELEFONE,
                                 tb242.ID_ALUNO_TELEFONE,
                                 tb242.NR_DDD,
                                 tb242.NR_TELEFONE,
                                 tb242.NO_CONTATO,
                                 tb242.DES_OBSERVACAO
                             }).ToList();

            var resultado2 = from result in resultado
                             select new
                             {
                                 result.NM_TIPO_TELEFONE,
                                 result.ID_ALUNO_TELEFONE,
                                 result.NO_CONTATO,
                                 result.DES_OBSERVACAO,
                                 telefone = "(" + result.NR_DDD.ToString() + ") " + result.NR_TELEFONE.ToString().Insert(4, "-")
                             };

            grdTelETA.DataSource = resultado2;
            grdTelETA.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid de Endereços do Aluno
        /// </summary>
        /// <param name="coAlu">Id do aluno</param>
        /// <param name="coEmp">Id da unidade</param>
        private void CarregaGridEnderecos(int coAlu, int coEmp)
        {
            grdEndETA.DataSource = (from tb241 in TB241_ALUNO_ENDERECO.RetornaTodosRegistros()
                                    where tb241.TB07_ALUNO.CO_ALU == coAlu && tb241.TB07_ALUNO.CO_EMP == coEmp
                                    select new
                                    {
                                        tb241.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO,
                                        tb241.ID_ALUNO_ENDERECO,
                                        cep = tb241.CO_CEP.Insert(5, "-"),
                                        tb241.DS_ENDERECO,
                                        tb241.NR_ENDERECO,
                                        tb241.DS_COMPLEMENTO,
                                        tb241.TB905_BAIRRO.NO_BAIRRO
                                    });

            grdEndETA.DataBind();
        }

        #region ====================================================== REGISTRO DE MATERIAL COLETIVO ===============================================================

        //============================================ REGISTRO DE MATERIAL COLETIVO ======================================================
        #region REGISTRO DE MATERIAL COLETIVO

        /// <summary>
        /// Preenche a grid de parcelas com infomações vazias
        /// </summary>
        private void CarregaGridVazia()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NU_DOC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NU_PAR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencimento";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorParcela";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorDescto";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorLiquido";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorMulta";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorJuros";
            dtV.Columns.Add(dcATM);

            //int i = 1;
            DataRow linha;
            //while (i <= 2)
            //{
            linha = dtV.NewRow();
            linha["NU_DOC"] = "";
            linha["NU_PAR"] = "";
            linha["dtVencimento"] = "";
            linha["valorParcela"] = "";
            linha["valorDescto"] = "";
            linha["valorLiquido"] = "";
            linha["valorMulta"] = "";
            linha["valorJuros"] = "";
            dtV.Rows.Add(linha);

            //}

            //Session["TabelaMedicAtendMed"] = dtV;

            grdParcelasMaterial.DataSource = dtV;
            grdParcelasMaterial.DataBind();
        }

        /// <summary>
        /// Carrega os dias de vencimento para as mensalidades da matrícula (Será alterado para buscar em uma tabela no banco)
        /// </summary>
        private void CarregaDiasVencimentoTabMatrColet()
        {
            ddlDiaVectoParcMater.Items.AddRange(AuxiliBaseApoio.DiaVencimentoTitulo());
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serie = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            var curso = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            int diaVencimento = ((curso != null && curso.NU_MDV != null) ? (curso.NU_MDV ?? 5) : 5);

            if (ddlDiaVectoParcMater.Items.FindByValue(diaVencimento.ToString()) != null)
                ddlDiaVectoParcMater.SelectedValue = diaVencimento.ToString();
        }

        /// <summary>
        /// Carrega a grid de tipos de solicitações de material
        /// </summary>
        private void CarregaGrideSolicitacaoMatrColet()
        {
            var lstTb66 = from tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros()
                          where tb66.FL_ITEM_MATRIC_TPSOLIC == "S"
                          select new
                          {
                              Codigo = tb66.CO_TIPO_SOLI,
                              Descricao = tb66.NO_TIPO_SOLI,
                              Valor = tb66.VL_UNIT_SOLI != null ? tb66.VL_UNIT_SOLI.Value : 0,
                              DescUnidade = tb66.TB89_UNIDADES != null ? tb66.TB89_UNIDADES.SG_UNIDADE : "",
                              Qtde = "",
                              Desconto = "",
                              Total = "",
                              Inclu = true,
                              Checked = false,
                              txMatric = tb66.FL_TAXA_MATRIC
                          };

            grdSolicitacoesMatrColet.DataKeyNames = new string[] { "Codigo" };
            grdSolicitacoesMatrColet.DataSource = lstTb66;
            grdSolicitacoesMatrColet.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletosTabMatrColet()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coBoleto = (TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, int.Parse(ddlModalidade.SelectedValue), int.Parse(ddlSerieCurso.SelectedValue)).ID_BOLETO_MATR ?? 0);
            int coMod = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoSolicMatrColet, coEmp, "U", coMod, coCur, false, false);

            ddlBoletoSolicMatrColet.Items.Insert(0, new ListItem("Nenhum", ""));
            
            //if (coBoleto > 0 && ddlBoletoSolicMatrColet.Items.FindByValue(coBoleto.ToString()) != null)
            //    ddlBoletoSolicMatrColet.SelectedValue = coBoleto.ToString();

            var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                        where iTb83.CO_EMP == coEmp
                        select new { iTb83.ID_BOLETO_MATRIC, iTb83.ID_BOLETO_MENSA, iTb83.ID_BOLETO_MATER_UNIFO }).FirstOrDefault();

            if (tb83 != null)
            {
                //if (tb83.ID_BOLETO_MENSA != null)
                //{
                //    ddlBoleto.SelectedValue = tb83.ID_BOLETO_MENSA.ToString();
                //}

                if (tb83.ID_BOLETO_MATER_UNIFO != null)
                {
                    ddlBoletoSolicMatrColet.SelectedValue = tb83.ID_BOLETO_MATER_UNIFO.ToString();
                }
            }

        }

        /// <summary>
        /// Método responsável por preencher a grid de parcelas na aba de registro de material coletivo
        /// </summary>
        protected void MontaGridParcelasMaterialTabMatrColet()
        {
            int nireAluno = txtNIREAluMU.Text != "" ? int.Parse(txtNIREAluMU.Text) : 0;
            int coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nireAluno).FirstOrDefault().CO_ALU;

            int i = 1; // Contador das parcelas
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmp);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            //int numNire = txtNireAluno.Text.Replace(".", "").Replace("-", "") != "" ? int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", "")) : 0;
            int colu = coAlu;
            int numNire = TB07_ALUNO.RetornaPeloCoAlu(colu).NU_NIRE;
            decimal valPar = 0;
            decimal valLiq = 0;
            decimal valDes = 0;
            decimal valMul = tb83.VL_PERCE_MULTA != null ? (decimal)tb83.VL_PERCE_MULTA : 0;
            decimal valJur = tb83.VL_PERCE_JUROS != null ? (decimal)tb83.VL_PERCE_JUROS : 0;
            decimal ValTotal = txtValorTotalMatrColet.Text != "" ? decimal.Parse(txtValorTotalMatrColet.Text) : 0;
            int QtdPar = txtQtdParcelas.Text != "" ? int.Parse(txtQtdParcelas.Text) : 1;
            string nuPar = "";
            DateTime dataSelec = txtDtVectoSolicMatrColet.Text != "" ? DateTime.Parse(txtDtVectoSolicMatrColet.Text) : DateTime.Now;
            int mesVenc = 1;
            string tpDesconto = ddlTipoDesctoParc.SelectedValue;
            int QtdMesDesconto = txtQtdeMesesDesctoParc.Text != "" ? int.Parse(txtQtdeMesesDesctoParc.Text) : 0;
            decimal vlDesconto = txtDesctoMensaParc.Text != "" ? decimal.Parse(txtDesctoMensaParc.Text) : 0;
            int PID = txtMesIniDescontoParc.Text != "" ? int.Parse(txtMesIniDescontoParc.Text) : 0;
            decimal vlValidTotal = 0;
            int dia = int.Parse(ddlDiaVectoParcMater.SelectedValue);
            int mes = 0;
            int ano = 0;
            string tpDesc = ddlTipoDesctoParc.SelectedValue;


            grdParcelasMaterial.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

            DataTable Dt = new DataTable();
            Dt.Columns.Add("CO_EMP");
            Dt.Columns.Add("NU_DOC");
            Dt.Columns.Add("NU_PAR");
            Dt.Columns.Add("DT_CAD_DOC");
            Dt.Columns.Add("dtVencimento");
            Dt.Columns.Add("valorParcela");
            Dt.Columns.Add("valorDescto");
            Dt.Columns.Add("valorLiquido");
            Dt.Columns.Add("valorMulta");
            Dt.Columns.Add("valorJuros");

            if (chkValUnicTituMatrColet.Checked)
            {
                #region Títulos consolidados

                valPar = Math.Round((ValTotal / QtdPar), 2);

                if (valPar <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar parcelas com valores menores ou iguais a 0.");
                    return;
                }

                int qtMes = QtdMesDesconto;
                while (i <= QtdPar)
                {
                    mes = dataSelec.Month;
                    ano = dataSelec.Year;
                    nuPar = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();
                    valDes = 0;
                    valLiq = valPar;
                    vlValidTotal = valPar * QtdPar;

                    // Valida se o valor total das parcelas está de acordo com o valor total dos itens selecionados
                    // se os valores forem diferentes o sistema calcula a diferencia e adiciona na ultima parcela
                    if (vlValidTotal != ValTotal && i == QtdPar)
                    {
                        valPar = ValTotal > vlValidTotal ? valPar + (ValTotal - vlValidTotal) : valPar + (vlValidTotal - ValTotal);
                    }

                    // Determina a data de vencimento das parcelas, a partir da segunda parcela, de acordo com as informações de data da primeira parcela, melhor dia de
                    // vencimento e o ano, ajustado se necessário
                    if (i != 1)
                    {
                        mes = mes + 1;
                        if (mesVenc == 11)
                        {
                            //mesVenc = 1;
                            mes = 1;
                            ano = ano + 1;
                        }
                        if (mes == 13)
                        {
                            mes = 1;
                            ano = ano + 1;
                        }
                        dataSelec = DateTime.Parse(dia.ToString("D2") + "/" + mes.ToString("D2") + "/" + ano);
                    }

                    if (tpDesc == "M")
                    {
                        // Determina em qual parcela irá iniciar o desconto, de acordo com o PID informado
                        if (i >= PID)
                        {
                            if (qtMes > 0)
                            {
                                valDes = vlDesconto;
                                valLiq = valPar - valDes;
                                qtMes--;
                            }
                        }
                    }
                    else
                    {
                        valDes = Math.Round((vlDesconto / QtdPar), 2);
                        valLiq = valPar - valDes;
                    }

                    if (valDes >= valPar)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar parcelas com descontos maiores ou iguais ao valor das parcelas.");
                        return;
                    }

                    // Monta as linhas da GRID
                    Dt.Rows.Add(
                        coEmp,
                        "MU" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + nuPar,
                        nuPar,
                        DateTime.Now.ToString("dd/MM/yyyy"),
                        dataSelec.ToString("dd/MM/yyyy"),
                        valPar.ToString("N2"),
                        valDes.ToString("N2"),
                        valLiq.ToString("N2"),
                        valMul.ToString("N2"),
                        valJur.ToString("N3")
                    );
                    mesVenc++;
                    i++;
                }
                #endregion
            }
            else
            {
                #region Títulos não consolidados

                foreach (GridViewRow linha in grdSolicitacoesMatrColet.Rows)
                {
                    // Faz a verificação dos itens marcados na Grid de Itens Emprestados
                    if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                    {
                        valPar = decimal.Parse(((Label)linha.Cells[2].FindControl("lblTotal")).Text);
                        if (valPar <= 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar parcelas com valores menores ou iguais a 0.");
                            return;
                        }
                        mes = dataSelec.Month;
                        ano = dataSelec.Year;
                        nuPar = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();
                        valDes = 0;
                        valLiq = valPar;

                        // Determina a data de vencimento das parcelas, a partir da segunda parcela, de acordo com as informações de data da primeira parcela, melhor dia de
                        // vencimento e o ano, ajustado se necessário
                        if (i != 1)
                        {
                            mes = mes + 1;
                            if (mesVenc == 11)
                            {
                                mesVenc = 1;
                                mes = 1;
                                ano = ano + 1;
                            }
                            dataSelec = DateTime.Parse(dia.ToString("D2") + "/" + mes.ToString("D2") + "/" + ano);
                        }

                        // Monta as linhas da GRID
                        Dt.Rows.Add(
                            coEmp,
                            "MU" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + nuPar,
                            nuPar,
                            DateTime.Now.ToString("dd/MM/yyyy"),
                            dataSelec.ToString("dd/MM/yyyy"),
                            valPar.ToString("N2"),
                            valDes.ToString("N2"),
                            valLiq.ToString("N2"),
                            valMul.ToString("N2"),
                            valJur.ToString("N3")
                        );

                        mesVenc++;
                        i++;
                    }
                }
                #endregion
            }

            grdParcelasMaterial.DataSource = Dt;
            grdParcelasMaterial.DataBind();
        }

        #endregion

        #region Funções de Campo

        protected void ddlTipoDesctoParc_SelectedIndexChanged(object sender, EventArgs e)
        {

            string tpDesc = ddlTipoDesctoParc.SelectedValue;

            switch (tpDesc)
            {
                case "T":
                    txtQtdeMesesDesctoParc.Enabled = false;
                    txtQtdeMesesDesctoParc.Text = "";

                    txtMesIniDescontoParc.Enabled = false;
                    txtMesIniDescontoParc.Text = "";
                    break;
                case "M":
                    txtQtdeMesesDesctoParc.Enabled = true;
                    txtQtdeMesesDesctoParc.Text = "";

                    txtMesIniDescontoParc.Enabled = true;
                    txtMesIniDescontoParc.Text = "";
                    break;
            }
        }

        //protected void chkValUnicTituMatrColet_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkValUnicTituMatrColet.Checked)
        //    {
        //        txtQtdParcelas.Enabled = true;
        //        ddlTipoDesctoParc.Enabled = true;
        //        if (ddlTipoDesctoParc.SelectedValue == "M")
        //        {
        //            txtQtdeMesesDesctoParc.Enabled = true;
        //            txtMesIniDescontoParc.Enabled = true;
        //        }
        //        else
        //        {
        //            txtQtdeMesesDesctoParc.Enabled = false;
        //            txtMesIniDescontoParc.Enabled = false;
        //        }
        //        txtDesctoMensaParc.Enabled = true;
        //        txtQtdParcelas.Text = "";

        //        txtQtdParcelas.Text = "1";
        //    }
        //    else
        //    {
        //        txtQtdParcelas.Enabled = false;
        //        ddlTipoDesctoParc.Enabled = false;
        //        txtQtdeMesesDesctoParc.Enabled = false;
        //        txtMesIniDescontoParc.Enabled = false;
        //        txtDesctoMensaParc.Enabled = false;
        //        txtQtdParcelas.Text = "";
        //    }

        //if (chkConsolValorTitul.Checked)
        //{
        //    ddlHistorico.Enabled = ddlAgrupador.Enabled = 
        //    ddlGrupoContaA.Enabled = ddlSubGrupoContaA.Enabled = ddlContaContabilA.Enabled = ddlTipoContaA.Enabled =
        //    ddlGrupoContaB.Enabled = ddlSubGrupoContaB.Enabled = ddlContaContabilB.Enabled = ddlTipoContaB.Enabled =
        //    ddlGrupoContaC.Enabled = ddlSubGrupoContaC.Enabled = ddlContaContabilC.Enabled = ddlTipoContaC.Enabled = true;
        //}
        //else
        //{
        //    ddlHistorico.Enabled = ddlAgrupador.Enabled =
        //    ddlGrupoContaA.Enabled = ddlSubGrupoContaA.Enabled = ddlContaContabilA.Enabled = ddlTipoContaA.Enabled =
        //    ddlGrupoContaB.Enabled = ddlSubGrupoContaB.Enabled = ddlContaContabilB.Enabled = ddlTipoContaB.Enabled =
        //    ddlGrupoContaC.Enabled = ddlSubGrupoContaC.Enabled = ddlContaContabilC.Enabled = ddlTipoContaC.Enabled = false;
        //    ddlHistorico.SelectedValue = ddlAgrupador.SelectedValue =
        //    ddlGrupoContaA.SelectedValue = ddlSubGrupoContaA.SelectedValue = ddlContaContabilA.SelectedValue =
        //    ddlGrupoContaB.SelectedValue = ddlSubGrupoContaB.SelectedValue = ddlContaContabilB.SelectedValue =
        //    ddlGrupoContaC.SelectedValue = ddlSubGrupoContaC.SelectedValue = ddlContaContabilC.SelectedValue = "";
        //}
        //}

        //protected void ckAtualizaFinancSolicMatrColet_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ckAtualizaFinancSolicMatrColet.Checked)
        //    {
        //        #region chkConsolValorTitul
        //        int i = 0;
        //        foreach (GridViewRow linha in grdSolicitacoesMatrColet.Rows)
        //        {
        //            if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
        //            {
        //                i++;
        //            }
        //        }

        //        if (i > 1)
        //        {
        //            chkValUnicTituMatrColet.Checked = false;
        //            chkValUnicTituMatrColet.Enabled = true;
        //        }
        //        else
        //        {
        //            chkValUnicTituMatrColet.Checked = false;
        //            chkValUnicTituMatrColet.Enabled = false;
        //        }
        //        #endregion

        //        if (chkValUnicTituMatrColet.Checked)
        //        {
        //            txtQtdParcelas.Text = "";
        //            txtQtdParcelas.Enabled = true;

        //            ddlTipoDesctoParc.SelectedValue = "T";
        //            ddlTipoDesctoParc.Enabled = true;

        //            txtDesctoMensaParc.Text = "";
        //            txtDesctoMensaParc.Enabled = true;

        //            //lnkMatUniAlu.Enabled = true;

        //            lnkBoletoMater.Enabled = true;
        //        }

        //        ddlDiaVectoParcMater.Enabled = true;

        //        //lnkGrava.Enabled = true;

        //        lnkMontaGridParcMater.Enabled = true;

        //        ddlBoletoSolicMatrColet.SelectedValue = "";
        //        ddlBoletoSolicMatrColet.Enabled = true;
        //    }
        //    else
        //    {
        //        chkValUnicTituMatrColet.Checked = false;
        //        chkValUnicTituMatrColet.Enabled = false;

        //        txtQtdParcelas.Text = "";
        //        txtQtdParcelas.Enabled = false;

        //        txtDtVectoSolicMatrColet.Enabled = false;

        //        ddlDiaVectoParcMater.Enabled = false;

        //        ddlTipoDesctoParc.SelectedValue = "T";
        //        ddlTipoDesctoParc.Enabled = false;

        //        txtQtdeMesesDesctoParc.Text = "";
        //        txtQtdeMesesDesctoParc.Enabled = false;

        //        txtDesctoMensaParc.Text = "";
        //        txtDesctoMensaParc.Enabled = false;

        //        txtMesIniDescontoParc.Text = "";
        //        txtMesIniDescontoParc.Enabled = false;

        //        ddlBoletoSolicMatrColet.SelectedValue = "";
        //        ddlBoletoSolicMatrColet.Enabled = false;

        //        lnkMontaGridParcMater.Enabled = false;

        //        lnkBoletoMater.Enabled = false;
        //    }
        //}

        protected void lnkBoletoMater_Click(object sender, EventArgs e)
        {
            //--------> Instancia um novo conjunto de dados de boleto na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //--------> Dados do Aluno e Unidade
            int nireAluno = txtNIREAluMU.Text != "" ? int.Parse(txtNIREAluMU.Text) : 0;
            int coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nireAluno).FirstOrDefault().CO_ALU;
            int coemp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coModu = Convert.ToInt32(this.ddlModalidade.SelectedValue);
            int coCur = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
            int coTur = Convert.ToInt32(this.ddlTurma.SelectedValue);


            //--------> Recupera dados do Responsável do Aluno
            var s = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                     where tb07.CO_ALU == coAlu
                     join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                     join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                     select new
                     {
                         NOME = tb108.NO_RESP,
                         BAIRRO = tb905.NO_BAIRRO,
                         CEP = tb108.CO_CEP_RESP,
                         CIDADE = tb904.NO_CIDADE,
                         ENDERECO = tb108.DE_ENDE_RESP,
                         NUMERO = tb108.NU_ENDE_RESP,
                         COMPL = tb108.DE_COMP_RESP,
                         UF = tb904.CO_UF,
                         CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP
                     }).FirstOrDefault();

            int iGrdNeg = 1;

            foreach (GridViewRow linha in grdParcelasMaterial.Rows)
            {
                int coEmp = Convert.ToInt32(grdParcelasMaterial.DataKeys[linha.RowIndex].Values[0]);
                string nuDoc = grdParcelasMaterial.DataKeys[linha.RowIndex].Values[1].ToString();
                int nuPar = Convert.ToInt32(grdParcelasMaterial.DataKeys[linha.RowIndex].Values[2]);
                string strInstruBoleto = "";

                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, nuDoc, nuPar);

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Boleto Associado!");
                    return;
                }
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                tb47.TB108_RESPONSAVELReference.Load();

                //------------> Se o título for gerado para um aluno:
                if (tb47.TB108_RESPONSAVEL == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                    return;
                }

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                    return;
                }

                //------------> Obtém a unidade
                TB25_EMPRESA unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(coemp);

                InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                //------------> Informações do Boleto
                boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

                /*
                 * Esta parte do código valida se o título já possui um nosso número, se já tiver, ele usa o NossoNúmero do título, registrado na tabela TB47, caso contrário,
                 * ele pega o próximo NossoNúmero registrado no banco, tabela TB29.
                 * */
                if (tb47.CO_NOS_NUM != null && tb47.CO_NOS_NUM.Trim() != "")
                {
                    boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
                }
                else
                {
                    boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    tb47.CO_NOS_NUM = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    TB47_CTA_RECEB.SaveOrUpdate(tb47, true);
                }

                boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                boleto.Valor = tb47.VR_PAR_DOC; //valor da parcela do documento
                if (tb47.VR_DES_DOC != null)
                {
                    boleto.Desconto = tb47.VR_DES_DOC.Value; // Valor do desconto do documento
                }
                boleto.Vencimento = tb47.DT_VEN_DOC;

                //------------> Informações do Cedente
                boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                boleto.CpfCnpjCedente = unidade.CO_CPFCGC_EMP;
                boleto.NomeCedente = unidade.NO_RAZSOC_EMP;

                /**
                 * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
                 * */
                #region Valida layout do boleto gerado
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                tb25.TB000_INSTITUICAOReference.Load();
                tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
                //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

                if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
                {
                    switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                else
                {
                    switch (tb25.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                #endregion

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                {
                    var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                    strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                }

                //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                boleto.Instrucoes = strInstruBoleto;

                //------------> Chave do Título do Contas a Receber
                boleto.CO_EMP = tb47.CO_EMP;
                boleto.NU_DOC = tb47.NU_DOC;
                boleto.NU_PAR = tb47.NU_PAR;
                boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

                //------------> Faz a adição de instruções ao Boleto
                boleto.Instrucoes += "<br>";
                //boleto.Instrucoes += "(*) " + multaMoraDesc + "<br>";


                //------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                string CnpjCPF = "";

                //------------> Ano Refer: - Matrícula: - Nº NIRE:
                //------------> Modalidade: - Série: - Turma: - Turno:
                var inforAluno = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                  where tb07.CO_ALU == tb47.CO_ALU
                                  select new
                                  {
                                      tb07.NU_NIRE,
                                      tb07.NO_ALU
                                  }).FirstOrDefault();

                string deModu = "";
                if (tb47.CO_MODU_CUR != null)
                {
                    deModu = TB44_MODULO.RetornaPelaChavePrimaria(tb47.CO_MODU_CUR.Value).DE_MODU_CUR;
                }
                else
                {
                    deModu = TB44_MODULO.RetornaPelaChavePrimaria(coModu).DE_MODU_CUR;
                }

                string noCur = "";
                if (tb47.CO_CUR != null)
                {
                    noCur = TB01_CURSO.RetornaPelaChavePrimaria(coemp, coModu, tb47.CO_CUR.Value).NO_CUR;
                }
                else
                {
                    noCur = TB01_CURSO.RetornaPelaChavePrimaria(coemp, coModu, coCur).NO_CUR;
                }

                var infTur = (from tb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                              join tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                on tb129.CO_TUR equals tb06.CO_TUR
                              where tb129.CO_TUR == coTur
                              select new
                              {
                                  tb129.CO_SIGLA_TURMA,
                                  tb06.CO_PERI_TUR
                              }).FirstOrDefault();

                string turno = "";
                switch (infTur.CO_PERI_TUR)
                {
                    case "M":
                        turno = "Matutino";
                        break;
                    case "V":
                        turno = "Vespertino";
                        break;
                    case "N":
                        turno = "Noturno";
                        break;
                }

                if (inforAluno != null)
                {
                    CnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                        // Esta informação não é necessária no boleto por se tratar de um código interno de identificação do aluno
                        // por isso a mesma foi retirada das observações do boleto.
                        // " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") +
                                 " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + tb47.CO_ANO_MES_MAT.Trim() +
                                 "<br>" + deModu + " - Série: " + noCur +
                                 " - Turma: " + infTur.CO_SIGLA_TURMA + " - Turno: " + turno;
                    //CnpjCPF = "Ano Refer: " + inforAluno.CO_ANO_MES_MAT.Trim() + " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") + " - Nº NIRE: " +
                    //    inforAluno.NU_NIRE.ToString() + "<br> Modalidade: " + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                    //    " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO + " <br> Aluno(a): " + inforAluno.NO_ALU;

                    boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                }

                boleto.Instrucoes += CnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";

                //------------> Informações do Sacado
                boleto.BairroSacado = s.BAIRRO;
                boleto.CepSacado = s.CEP;
                boleto.CidadeSacado = s.CIDADE;
                boleto.CpfCnpjSacado = s.CPFCNPJ;
                boleto.EnderecoSacado = s.ENDERECO + " " + s.NUMERO + " " + s.COMPL;
                boleto.NomeSacado = s.NOME;
                boleto.UfSacado = s.UF;

                //------------> Adiciona o título atual na Sessão
                ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                /*
                * Esta parte do código atualiza o NossoNúmero do título (TB47).
                * Esta linha foi incluída para resolver o problema de boletos diferentes sendo gerados para um mesmo
                * título
                * */

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);
                //===> Incluí o nosso número na tabela de nossos números por título
                TB045_NOS_NUM tb045 = TB045_NOS_NUM.RetornaPelaChavePrimaria(tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, tb47.CO_NOS_NUM);

                if (tb045 == null)
                {
                    tb045 = new TB045_NOS_NUM();
                    tb045.NU_DOC = tb47.NU_DOC;
                    tb045.NU_PAR = tb47.NU_PAR;
                    tb045.DT_CAD_DOC = tb47.DT_CAD_DOC;
                    tb045.DT_NOS_NUM = DateTime.Now;
                    tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                    //===> Pega as informações da empresa/unidade
                    TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);
                    tb045.TB25_EMPRESA = emp;
                    //===> Pega as informações do colaborador
                    TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    tb045.TB03_COLABOR = tb03;
                    tb045.CO_NOS_NUM = tb47.CO_NOS_NUM;
                    tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;
                    GestorEntities.SaveOrUpdate(tb045, true);
                }

                long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                int casas = u.CO_PROX_NOS_NUM.Length;
                string mask = string.Empty;
                foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                GestorEntities.SaveOrUpdate(u, true);

                iGrdNeg++;
            }

            //--------> Gera e exibe os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

        protected void chkDescPer_ChenckedChanged(object sender, EventArgs e)
        {
            int retornaInt = 0;
            int qtdItens = 0;
            decimal dcmValor = 0;
            decimal valorTotal = 0;
            decimal valorDesconto = 0;

            foreach (GridViewRow linha in grdSolicitacoesMatrColet.Rows)
            {

                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    qtdItens++;
                    ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Enabled = true;
                    ((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Enabled = true;
                    ((CheckBox)linha.Cells[4].FindControl("chkDescPer")).Enabled = true;
                    if (((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text == "")
                    {
                        ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text = "1";
                        //qtdTotalItem = qtdTotalItem + 1;
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                            {
                                if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                {
                                    valorTotal = valorTotal + (dcmValor - ((valorDesconto / 100) * dcmValor));
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - ((valorDesconto / 100) * dcmValor)).ToString("#,##0.00");
                                }
                                else
                                {
                                    valorTotal = valorTotal + dcmValor;
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                }
                            }
                            else
                            {
                                if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                {
                                    valorTotal = valorTotal + (dcmValor - valorDesconto);
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - valorDesconto).ToString("#,##0.00");
                                }
                                else
                                {
                                    valorTotal = valorTotal + dcmValor;
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                }
                            }
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                    else
                    {
                        if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text, out retornaInt))
                        {
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                if (retornaInt > 0)
                                {
                                    //qtdTotalItem = qtdTotalItem + retornaInt;
                                    if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + ((dcmValor - ((valorDesconto / 100) * dcmValor)) * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = ((dcmValor - ((valorDesconto / 100) * dcmValor)) * retornaInt).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + (dcmValor * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                        }
                                    }
                                    else
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + ((dcmValor - valorDesconto) * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = ((dcmValor - valorDesconto) * retornaInt).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + (dcmValor * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                        }
                                    }
                                    //valorTotal = valorTotal + (dcmValor * retornaInt);
                                }
                                else
                                {
                                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                                    if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + (dcmValor - ((valorDesconto / 100) * dcmValor));
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = ((valorDesconto / 100) * dcmValor).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + dcmValor;
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                        }
                                    }
                                    else
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + (dcmValor - valorDesconto);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - valorDesconto).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + dcmValor;
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                        else
                        {
                            ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                            //qtdTotalItem = qtdTotalItem + 1;
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                                {
                                    if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                    {
                                        valorTotal = valorTotal + (dcmValor - ((valorDesconto / 100) * dcmValor));
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - ((valorDesconto / 100) * dcmValor)).ToString("#,##0.00");
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal + dcmValor;
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                    }
                                }
                                else
                                {
                                    if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                    {
                                        valorTotal = valorTotal + (dcmValor - valorDesconto);
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - valorDesconto).ToString("#,##0.00");
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal + dcmValor;
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                    }
                                }

                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                    }
                }
                else
                {
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Enabled = false;
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "";
                    ((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked = false;
                    ((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text = "";
                    ((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Enabled = false;
                    ((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Enabled = false;
                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                }

            }
            if (qtdItens > 1)
            {
                if (ckbAtualizaFinancSolic.Checked)
                {
                    if (!chkConsolValorTitul.Checked)
                    {
                        chkConsolValorTitul.Enabled = true;
                        //chkConsolValorTitul.Checked = false;

                        txtQtdParcelas.Text = "";
                        //txtQtdParcelas.Enabled = false;

                        ddlTipoDesctoParc.Enabled = false;

                        txtDesctoMensaParc.Text = "";
                        txtDesctoMensaParc.Enabled = false;

                        txtQtdeMesesDesctoParc.Text = "";
                        txtQtdeMesesDesctoParc.Enabled = false;

                        txtMesIniDescontoParc.Text = "";
                        txtMesIniDescontoParc.Enabled = false;
                    }
                }
                else
                {
                    //chkConsolValorTitul.Enabled = false;
                    //chkConsolValorTitul.Checked = false;

                    txtQtdParcelas.Text = "";
                    //txtQtdParcelas.Enabled = false;

                    ddlTipoDesctoParc.Enabled = false;

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = false;

                    txtQtdeMesesDesctoParc.Text = "";
                    txtQtdeMesesDesctoParc.Enabled = false;

                    txtMesIniDescontoParc.Text = "";
                    txtMesIniDescontoParc.Enabled = false;
                }
            }
            else
            {
                if (ckbAtualizaFinancSolic.Checked)
                {
                    //chkConsolValorTitul.Enabled = false;
                    //chkConsolValorTitul.Checked = false;

                    txtQtdParcelas.Text = "";
                    txtQtdParcelas.Enabled = true;

                    ddlTipoDesctoParc.Enabled = true;
                    ddlTipoDesctoParc.SelectedValue = "T";

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = true;

                    txtQtdeMesesDesctoParc.Text = "";
                    txtQtdeMesesDesctoParc.Enabled = false;

                    txtMesIniDescontoParc.Text = "";
                    txtMesIniDescontoParc.Enabled = false;
                }
                else
                {
                    chkConsolValorTitul.Enabled = true;
                    chkConsolValorTitul.Checked = false;

                    txtQtdParcelas.Text = "";
                    txtQtdParcelas.Enabled = true;

                    ddlTipoDesctoParc.Enabled = true;
                    ddlTipoDesctoParc.SelectedValue = "T";

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = false;

                    txtQtdeMesesDesctoParc.Text = "";
                    txtQtdeMesesDesctoParc.Enabled = false;

                    txtMesIniDescontoParc.Text = "";
                    txtMesIniDescontoParc.Enabled = false;
                }
            }
            txtValorTotalMatrColet.Text = valorTotal.ToString("#,##0.00");
            //CarregaAlunoTabMatrColet();
        }

        protected void lnkMontaGridParcMaterial_Click(object sender, EventArgs e)
        {
            //ControlaTabs("UMA");
            //ControlaChecks(chkMenEscAlu);

            if (ddlSerieCurso.SelectedValue == "")
            {
                AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível gerar grid de mensalidade porque série não foi selecionada.");
                return;
            }

            if (!ckAtualizaFinancSolicMatrColet.Checked)
            {
                AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível gerar grid de mensalidade porque não será atualizado no financeiro.");
                return;
            }

            if (ddlBoletoSolicMatrColet.SelectedValue == "")
            {
                AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível gerar grid de mensalidade porque não foi informado o boleto bancário");
                return;
            }


            if (chkValUnicTituMatrColet.Checked)
            {
                if (txtQtdParcelas.Text == "")
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível gerar grid de mensalidade porque não foi informado a quantidade de parcelas (QT).");
                    return;
                }

                if (txtDtVectoSolicMatrColet.Text == "")
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível gerar grid de mensalidade porque não foi informada a data de vencimento da primeira parcela.");
                    return;
                }
            }

            MontaGridParcelasMaterialTabMatrColet();
        }

        protected void lnkMatUniAlu_Click(object sender, EventArgs e)
        {
            //chkMatEsc.Enabled = false;
            //lblSucMatEsc.Visible = true;

            ////AuxiliPagina.EnvioMensagemSucesso(this, "Material(s) e Uniforme(s) atualizado(s) com sucesso.");

            //chkDocMat.Checked = true;
            //ControlaTabs("DOC");

            int nireAluno = txtNIREAluMU.Text != "" ? int.Parse(txtNIREAluMU.Text) : 0;
            int coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_NIRE == nireAluno).FirstOrDefault().CO_ALU;
            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            int proxTurma = Convert.ToInt32(this.ddlTurma.SelectedValue);
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(proxTurma).CO_EMP_UNID_CONT;
            int modalidade = Convert.ToInt32(this.ddlModalidade.SelectedValue);
            int proxSerie = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
            int codResp = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).TB108_RESPONSAVEL.CO_RESP;
            //int qtdZero = 6 - coAlu.ToString().Length;
            string proxAno = DateTime.Now.Year.ToString();
            //string proxAno = PreAuxili.proximoAnoMat<string>(txtAno.Text);
            string turno = TB06_TURMAS.RetornaPeloCodigo(proxTurma).CO_PERI_TUR;
            TB47_CTA_RECEB tb47;
            TB01_CURSO hTb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);
            //TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");
            DateTime dtPrev;
            if (txtDtPrevisaoMatrColet.Text != "")
            {
                dtPrev = DateTime.Parse(txtDtPrevisaoMatrColet.Text);

                if (dtPrev.Date < DateTime.Now.Date)
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível efetivar porque a data de previsão da entrega não pode ser uma data passada.");
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível efetivar porque a data de previsão da entrega não foi informada.");
                return;
            }


            #region Atualiza o fardamento da matrícula
            ///Varre toda a gride de Solicitações e salva na tabela TB114_FARDMAT 
            foreach (GridViewRow linha in grdSolicitacoesMatrColet.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked && txtDtPrevisaoMatrColet.Text != "")
                {
                    var tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));
                    tb66.TB89_UNIDADESReference.Load();
                    decimal? valorUnitario = tb66.VL_UNIT_SOLI;
                    int? coUnid = tb66.TB89_UNIDADES != null ? (int?)tb66.TB89_UNIDADES.CO_UNID_ITEM : null;
                    int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;
                    decimal vlDescSol = ((TextBox)linha.Cells[0].FindControl("txtDescSolic")).Text != "" ? decimal.Parse(((TextBox)linha.Cells[0].FindControl("txtDescSolic")).Text) : 0;
                    int flDescPorc = ((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked ? 1 : 0;


                    TB114_FARDMAT tb114 =
                        new TB114_FARDMAT
                        {
                            TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO),
                            CO_EMP = LoginAuxili.CO_EMP,
                            CO_ALU = coAlu,
                            CO_ANO_MATRIC = proxAno,
                            CO_MOD = modalidade,
                            CO_CUR = proxSerie,
                            CO_TUR = proxTurma,
                            TB66_TIPO_SOLIC = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0])),
                            ID_UNID_TPSOLIC = coUnid,
                            VL_UNIT_TPSOLIC = valorUnitario,
                            QT_SOLIC = qtde,
                            DT_SOLIC_TPSOLIC = DateTime.Now,
                            DT_PREVI_TPSOLIC = dtPrev,
                            FL_SITUA_FINAN = null,
                            NM_DOCUM_TITUL = null,
                            DT_REGIS_TPSOLIC = DateTime.Now,
                            CO_COL = LoginAuxili.CO_COL,
                            VL_DESC_TPSOLIC = vlDescSol,
                            FL_DESC_PORCENT = flDescPorc
                        };

                    GestorEntities.SaveOrUpdate(tb114);


                }

                if ((((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked) && (((HiddenField)linha.Cells[0].FindControl("hidTxMatric")).Value == "S"))
                {
                    var varTb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");
                    varTb08.VL_TAXA_MATRIC = decimal.Parse(((Label)linha.Cells[2].FindControl("lblValor")).Text);
                    GestorEntities.SaveOrUpdate(varTb08);
                }

            }

            #endregion

            #region Atualiza o financeiro de itens da secretaria

            if ((ckAtualizaFinancSolicMatrColet.Checked) && (txtValorTotalMatrColet.Text != "") && (txtDtVectoSolicMatrColet.Text != ""))
            {
                foreach (GridViewRow linha in grdParcelasMaterial.Rows)
                {
                    int coHist = hTb01.ID_HISTO_MATERIAL != null ? hTb01.ID_HISTO_MATERIAL.Value : 0;
                    decimal valPar = decimal.Parse(linha.Cells[3].Text);
                    decimal valDes = decimal.Parse(linha.Cells[4].Text);
                    decimal valMul = decimal.Parse(linha.Cells[6].Text);
                    decimal valJur = decimal.Parse(linha.Cells[7].Text);

                    if (coHist != 0)
                    {
                        TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                        TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(codResp);
                        TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                        tb108.TB74_UFReference.Load();

                        if (tb108 != null)
                        {
                            tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(refAluno.TB25_EMPRESA1.CO_EMP, linha.Cells[0].Text, int.Parse(linha.Cells[1].Text));

                            if (tb47 == null)
                            {
                                tb47 = new TB47_CTA_RECEB();
                                //tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                                //tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                                //tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                                //tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                                tb47.CO_ALU = coAlu;
                                tb47.TB108_RESPONSAVEL = tb108;
                                tb47.CO_ANO_MES_MAT = PreAuxili.proximoAnoMat<string>(proxAno);
                                tb47.CO_CUR = proxSerie;
                                tb47.CO_TUR = proxTurma;
                                tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                                tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                                tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                                tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                                tb47.CO_FLAG_TP_VALOR_DES = "V";
                                tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                                tb47.CO_FLAG_TP_VALOR_OUT = "V";
                                tb47.FL_EMITE_BOLETO = tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                                tb47.CO_MODU_CUR = modalidade;


                                tb47.CO_NOS_NUM = (tb25.CO_PROX_NOS_NUM != null ? tb25.CO_PROX_NOS_NUM : "");

                                tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(coHist);
                                tb47.DE_COM_HIST = "Solicitação de material coletivo / uniforme realizada no dia " + DateTime.Now.ToString("dd/MM/yyyy");
                                tb47.DT_ALT_REGISTRO = DateTime.Now;
                                tb47.DT_CAD_DOC = DateTime.Now;
                                tb47.DT_EMISS_DOCTO = DateTime.Now;
                                tb47.DT_SITU_DOC = DateTime.Now;
                                tb47.DT_VEN_DOC = DateTime.Parse(linha.Cells[2].Text);
                                tb47.IC_SIT_DOC = "A";
                                tb47.NU_DOC = linha.Cells[0].Text;
                                tb47.NU_PAR = int.Parse(linha.Cells[1].Text);
                                tb47.QT_PAR = grdParcelasMaterial.Rows.Count;
                                tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                                tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                                tb47.TB099_CENTRO_CUSTO = (tb25.CO_CENT_CUSSOL.HasValue ? (TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value)) : null);
                                tb47.TB227_DADOS_BOLETO_BANCARIO = (tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? (ddlBoletoSolicMatrColet.SelectedValue != "" ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoletoSolicMatrColet.SelectedValue)) : null) : null);
                                tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(coHist);
                                tb47.CO_AGRUP_RECDESP = TB83_PARAMETRO.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP).CO_AGRUP_REC;
                                tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb44.CO_SEQU_PC.Value);
                                tb47.CO_SEQU_PC_BANCO = tb44.CO_SEQU_PC_BANCO.Value;
                                tb47.CO_SEQU_PC_CAIXA = tb44.CO_SEQU_PC_CAIXA.Value;
                                tb47.TP_CLIENTE_DOC = "A";
                                tb47.VR_JUR_DOC = valJur;
                                tb47.VR_MUL_DOC = valMul;
                                tb47.VR_PAR_DOC = valPar;
                                tb47.VR_DES_DOC = valDes;
                                tb47.VR_TOT_DOC = (txtValorTotalMatrColet.Text != "" ? decimal.Parse(txtValorTotalMatrColet.Text) : 0);

                                GestorEntities.SaveOrUpdate(tb47);

                            }
                            else
                            {
                                AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível efetivar e atualizar o financeiro porque já existe no sistema um título com este Número de Documento.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        AuxiliPagina.EnvioAvisoGeralSistema(this, "Não é possível efetivar e atualizar o financeiro porque não existe histórico de material coletivo / uniforme cadastrado para o curso.");
                        return;
                    }
                }


            }

            #endregion

            #region Habilita/Desabilia botões

            lnkBoletoMater.Enabled = true;
            ckAtualizaFinancSolicMatrColet.Enabled = false;
            chkValUnicTituMatrColet.Enabled = false;
            txtValorTotalMatrColet.Enabled = false;
            txtQtdParcelas.Enabled = false;
            txtDtVectoSolicMatrColet.Enabled = false;
            ddlDiaVectoParcMater.Enabled = false;
            ddlTipoDesctoParc.Enabled = false;
            txtQtdeMesesDesctoParc.Enabled = false;
            txtDesctoMensaParc.Enabled = false;
            txtMesIniDescontoParc.Enabled = false;
            ddlBoletoSolicMatrColet.Enabled = false;
            txtDtPrevisaoMatrColet.Enabled = false;
            lnkMontaGridParcMater.Enabled = false;

            // Desabilita os campos da grid de itens da solicitação
            //foreach (GridViewRow linha in grdSolicitacoes.Rows)
            //{
            //    ((CheckBox)linha.Cells[0].FindControl("chkSelect")).Enabled = false;
            //    ((TextBox)linha.Cells[3].FindControl("txtDescSolic")).Enabled = false;
            //    ((CheckBox)linha.Cells[3].FindControl("chkDescPer")).Enabled = false;
            //    ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Enabled = false;
            //}

            grdSolicitacoesMatrColet.Enabled = false;
            grdParcelasMaterial.Enabled = false;

            #endregion

            AuxiliPagina.EnvioMensagemSucesso(this, "Material(s) e Uniforme(s) efetivado com sucesso.");
            lblMatrColet.Visible = true;
            //ControlaTabs("UMA");
            ControlaTabs("UNI");
        }

        #endregion

        private void CarregaCamposRegistroMaterial()
        {
            CarregaGrideSolicitacao();
            CarregaDiasVencimentoTabMatrColet();
            CarregaGridVazia();
            chkValUnicTituMatrColet.Enabled = chkValUnicTituMatrColet.Checked = true;
            CarregaGrideSolicitacaoMatrColet();
            CarregaBoletosTabMatrColet();

            txtDtVectoSolicMatrColet.Text = DateTime.Now.ToString();
        }
        //============================================ REGISTRO DE MATERIAL COLETIVO ======================================================

        #endregion


        #endregion

        #region Controles

        /// <summary>
        /// Método para geração do boleto
        /// </summary>
        protected void GeraBoleto()
        {
            //--------> Instancia um novo conjunto de dados de boleto na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //--------> Dados do Aluno e Unidade
            int coAlu = int.Parse(hdCodAluno.Value);
            int coemp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;

            //--------> Recupera dados do Responsável do Aluno
            var s = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                     where tb07.CO_ALU == coAlu
                     join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                     join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                     select new
                     {
                         NOME = tb108.NO_RESP,
                         BAIRRO = tb905.NO_BAIRRO,
                         CEP = tb108.CO_CEP_RESP,
                         CIDADE = tb904.NO_CIDADE,
                         ENDERECO = tb108.DE_ENDE_RESP,
                         NUMERO = tb108.NU_ENDE_RESP,
                         COMPL = tb108.DE_COMP_RESP,
                         UF = tb904.CO_UF,
                         CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP
                     }).FirstOrDefault();

            int iGrdNeg = 1;
            //--------> Varre os títulos da grid
            foreach (GridViewRow row in grdNegociacao.Rows)
            {
                int coEmp = Convert.ToInt32(grdNegociacao.DataKeys[row.RowIndex].Values[0]);
                string nuDoc = grdNegociacao.DataKeys[row.RowIndex].Values[1].ToString();
                int nuPar = Convert.ToInt32(grdNegociacao.DataKeys[row.RowIndex].Values[2]);
                string strInstruBoleto = "";

                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, nuDoc, nuPar);

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Boleto Associado!");
                    return;
                }
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                tb47.TB108_RESPONSAVELReference.Load();

                //------------> Se o título for gerado para um aluno:
                if (tb47.TB108_RESPONSAVEL == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                    return;
                }

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                    return;
                }

                //------------> Obtém a unidade
                TB25_EMPRESA unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(coemp);

                InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                //------------> Informações do Boleto
                boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

                /*
                 * Esta parte do código valida se o título já possui um nosso número, se já tiver, ele usa o NossoNúmero do título, registrado na tabela TB47, caso contrário,
                 * ele pega o próximo NossoNúmero registrado no banco, tabela TB29.
                 * */
                if (tb47.CO_NOS_NUM != null)
                {
                    boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
                }
                else
                {
                    boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                }
                boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                boleto.Valor = tb47.VR_PAR_DOC; //valor da parcela do documento
                boleto.Vencimento = tb47.DT_VEN_DOC;

                //------------> Informações do Cedente
                boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                boleto.CpfCnpjCedente = unidade.CO_CPFCGC_EMP;
                boleto.NomeCedente = unidade.NO_RAZSOC_EMP;

                boleto.Desconto =
                        ((!tb47.VR_DES_DOC.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                ? (boleto.Valor * tb47.VR_DES_DOC.Value / 100)
                                : tb47.VR_DES_DOC.Value))
                        + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                ? (boleto.Valor * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                : tb47.VL_DES_BOLSA_ALUNO.Value)));

                /**
                 * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
                 * */
                #region Valida layout do boleto gerado
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                tb25.TB000_INSTITUICAOReference.Load();
                tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
                //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

                if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
                {
                    switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                else
                {
                    switch (tb25.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                #endregion

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                {
                    var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                    strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                }

                //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                boleto.Instrucoes = strInstruBoleto;

                //------------> Chave do Título do Contas a Receber
                boleto.CO_EMP = tb47.CO_EMP;
                boleto.NU_DOC = tb47.NU_DOC;
                boleto.NU_PAR = tb47.NU_PAR;
                boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

                //                string multaMoraDesc = "";

                ////------------> Informações da Multa
                //                multaMoraDesc += tb47.VR_MUL_DOC != null && tb47.VR_MUL_DOC.Value != 0 ?
                //                    (tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
                //                    (boleto.Valor * (decimal)tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

                ////------------> Informações da Mora
                //                multaMoraDesc += tb47.VR_JUR_DOC != null && tb47.VR_JUR_DOC.Value != 0 ?
                //                     (tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
                //                     (boleto.Valor * (decimal)tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
                //                        tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

                ////------------> Informações do desconto
                //                multaMoraDesc += tb47.VR_DES_DOC != null && tb47.VR_DES_DOC.Value != 0 ?
                //                     (tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + tb47.VR_DES_DOC.Value.ToString("0.00") + "% (R$ " +
                //                     (boleto.Valor * (decimal)tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
                //                        tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

                //                multaMoraDesc += tb47.VL_DES_BOLSA_ALUNO != null && tb47.VL_DES_BOLSA_ALUNO.Value != 0 ?
                //                         (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P" ? " - Descto Bolsa: " + tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00") + "% (R$ " +
                //                         (boleto.Valor * (decimal)tb47.VL_DES_BOLSA_ALUNO.Value / 100).ToString("0.00") + ")" : " - Descto Bolsa: R$ " +
                //                            tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00")) : "";


                //------------> Faz a adição de instruções ao Boleto
                boleto.Instrucoes += "<br>";
                //boleto.Instrucoes += "(*) " + multaMoraDesc + "<br>";


                //------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                string CnpjCPF = "";

                //------------> Ano Refer: - Matrícula: - Nº NIRE:
                //------------> Modalidade: - Série: - Turma: - Turno:
                var inforAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                  join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                  where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                  && tb08.CO_ALU == tb47.CO_ALU
                                  select new
                                  {
                                      tb08.TB44_MODULO.DE_MODU_CUR,
                                      tb01.NO_CUR,
                                      tb129.CO_SIGLA_TURMA,
                                      tb08.CO_ANO_MES_MAT,
                                      tb08.CO_ALU_CAD,
                                      tb08.TB07_ALUNO.NU_NIRE,
                                      tb08.TB07_ALUNO.NO_ALU,
                                      TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                  }).FirstOrDefault();

                if (inforAluno != null)
                {
                    CnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                                 " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") +
                                 " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                                 "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                                 " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO;
                    //CnpjCPF = "Ano Refer: " + inforAluno.CO_ANO_MES_MAT.Trim() + " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") + " - Nº NIRE: " +
                    //    inforAluno.NU_NIRE.ToString() + "<br> Modalidade: " + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                    //    " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO + " <br> Aluno(a): " + inforAluno.NO_ALU;

                    boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                }

                boleto.Instrucoes += CnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";

                //------------> Informações do Sacado
                boleto.BairroSacado = s.BAIRRO;
                boleto.CepSacado = s.CEP;
                boleto.CidadeSacado = s.CIDADE;
                boleto.CpfCnpjSacado = s.CPFCNPJ;
                boleto.EnderecoSacado = s.ENDERECO + " " + s.NUMERO + " " + s.COMPL;
                boleto.NomeSacado = s.NOME;
                boleto.UfSacado = s.UF;

                //------------> Adiciona o título atual na Sessão
                ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                /*
                 * Esta validação verifica se o título já possui NossoNúmaro, se não for o caso, ele atualiza o título incluíndo um novo NossoNúmero, e atualiza a tabela
                 * TB29 para incrementar o próximo NossoNúmero do banco.
                 * */
                if (tb47.CO_NOS_NUM == null)
                {
                    if ((iGrdNeg <= grdNegociacao.Rows.Count) && (grdNegociacao.Rows.Count > 1))
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                        /*
                         * Esta parte do código atualiza o NossoNúmero do título (TB47).
                         * Esta linha foi incluída para resolver o problema de boletos diferentes sendo gerados para um mesmo
                         * título
                         * */
                        tb47.CO_NOS_NUM = u.CO_PROX_NOS_NUM;
                        GestorEntities.SaveOrUpdate(tb47, true);

                        //===> Incluí o nosso número na tabela de nossos números por título
                        TB045_NOS_NUM tb045 = new TB045_NOS_NUM();
                        tb045.NU_DOC = tb47.NU_DOC;
                        tb045.NU_PAR = tb47.NU_PAR;
                        tb045.DT_CAD_DOC = tb47.DT_CAD_DOC;
                        tb045.DT_NOS_NUM = DateTime.Now;
                        tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                        //===> Pega as informações da empresa/unidade
                        TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);
                        tb045.TB25_EMPRESA = emp;
                        //===> Pega as informações do colaborador
                        TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                        tb045.TB03_COLABOR = tb03;
                        tb045.CO_NOS_NUM = tb47.CO_NOS_NUM;
                        tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;
                        GestorEntities.SaveOrUpdate(tb045, true);

                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }
                }

                iGrdNeg++;
            }

            //--------> Gera e exibe os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

        /// <summary>
        /// Método para limpar campos do Aluno
        /// </summary>
        protected void LimparAluno()
        {
            this.ddlUFAluno.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
            this.ddlPermiSaidaAluno.SelectedValue = "N";
            this.CarregaCidades(this.ddlCidadeAluno, this.ddlUFAluno);
            this.CarregaBairros(this.ddlBairroAluno, this.ddlCidadeAluno);
            this.ddlDeficienciaAluno.SelectedIndex = this.ddlNacionalidadeAluno.SelectedIndex = 1;
            //this.chkPaisMorJunt.Checked = this.chkMoraPais.Checked = this.chkDesctoPercBolsa.Checked = false;
            this.ControleImagemAluno.CarregaImagem(0);
            this.txtNomeAluno.Text = this.txtNireAluno.Text = txtApelAluno.Text =
           this.ddlSexoAluno.SelectedValue = this.txtDataNascimentoAluno.Text = this.ddlTpSangueAluno.SelectedValue = this.ddlStaSangueAluno.SelectedValue =
            this.txtNaturalidadeAluno.Text = this.ddlUfNacionalidadeAluno.SelectedValue =
           this.txtTelCelularAluno.Text = this.txtTelResidencialAluno.Text = this.txtEmailAluno.Text = txtTelCelularAlunoNoveDigit.Text =
           this.txtNomeMaeAlunoValid.Text = this.txtNomePaiAluno.Text = this.txtCepAluno.Text = this.txtCertNascAlu.Text = this.txtRGAluno.Text = this.txtOrgEmiRGAlu.Text =
           this.txtLogradouroAluno.Text = this.txtNumeroAluno.Text = this.txtCpfAluno.Text =
            this.ddlPasseEscolarAluno.SelectedValue =
           this.ddlTransporteEscolarAluno.SelectedValue = "SR";
        }

        /// <summary>
        /// Método para limpar campos do Responsável
        /// </summary>
        protected void LimparResponsavel()
        {
            this.ddlUfResp.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
            this.CarregaCidades(this.ddlCidadeResp, this.ddlUfResp);
            this.CarregaBairros(this.ddlBairroResp, this.ddlCidadeResp);
            this.CarregaUfs(this.ddlUfEmpResp);
            this.CarregaCidades(this.ddlCidadeEmpResp, this.ddlUfEmpResp);
            //this.CarregaBairros(this.ddlBairroEmpResp, this.ddlCidadeEmpResp);
            this.CarregaUfs(this.ddlIdentidadeUFResp);
            this.CarregaUfs(this.ddlUfNacionalidadeResp);
            //this.CarregaUfs(this.ddlUfTituloResp);
            this.ddlGrauInstrucaoResp.SelectedIndex = this.ddlNacioResp.SelectedIndex = 1;
            this.chkRespFunc.Checked = false;
            //this.ControleImagemResp.CarregaImagem(0);
            this.txtNoRespCPF.Text = this.txtCPFResp.Text = this.txtNomeResp.Text = this.txtNISResp.Text = this.txtDtNascResp.Text =
            this.ddlSexoResp.SelectedValue = this.ddlEstadoCivilResp.SelectedValue = this.ddlTpSangueResp.SelectedValue =
            this.ddlStaTpSangueResp.SelectedValue = this.txtMaeResp.Text =
            this.txtMesAnoTrabResp.Text = this.ddlProfissaoResp.SelectedValue =
            this.txtLogradouroResp.Text = this.txtNumeroResp.Text = this.txtComplementoResp.Text = this.txtCepResp.Text = this.txtTelCelularResp.Text = this.txtTelCelularRespNonoDig.Text =
            this.txtTelResidencialResp.Text = this.txtEmailResp.Text = this.txtNaturalidadeResp.Text = this.txtIdentidadeResp.Text =
            this.txtDtEmissaoResp.Text = this.txtOrgEmissorResp.Text =
            this.txtCepEmpresaResp.Text = this.txtTelEmpresaResp.Text = "";
            this.txtNomeEmpresaResp.Text = "";
        }

        /// <summary>
        /// Método para habilitar campos da bolsa de estudo do Aluno
        /// </summary>
        private void HabilitaCamposBolsa()
        {
            //this.txtDescontoAluno.Enabled = this.chkDesctoPercBolsa.Enabled = this.txtPeriodoDeIniBolAluno.Enabled = this.txtPeriodoTerBolAluno.Enabled = this.ddlBolsaAluno.SelectedValue != "";
        }

        /// <summary>
        /// Método para desabilitar campos após a matrícula        
        /// </summary>
        private void DesabilitaCamposMatricula()
        {
            btnPesqReserva.Enabled = ddlSituMatAluno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = lnkEfetMatric.Enabled = ddlTipoValorCurso.Enabled =
            ddlUnidade.Enabled = false;
            chkMenEscAlu.Checked = true;
            //lblSucEndAddAlu.Visible = lblSucTelAddAlu.Visible = lblSucCuiEspAlu.Visible = lblSucResAliAlu.Visible = lblSucRegAtiExt.Visible =
            //lblSucMatEsc.Visible = lblSucDocAlu.Visible = lnkSucMenEscAlu.Visible = true;
            chkAtualiFinan.Enabled = ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled =
            lnkMontaGridMensa.Enabled = lnkMenAlu.Enabled = grdDocumentos.Enabled = lnkAtuDoctos.Enabled = false;
            ControlaTabs("MEN");
        }

        /// <summary>
        /// Método que controla visibilidade das Tabs da tela de matrícula
        /// </summary>
        /// <param name="tab">Nome da tab</param>
        protected void ControlaTabs(string tab)
        {
            //tabAluno.Style.Add("display", "none");
            tabCuiEspAdd.Style.Add("display", "none");
            tabEndAdd.Style.Add("display", "none");
            tabTelAdd.Style.Add("display", "none");
            tabResp.Style.Add("display", "none");
            tabResAliAdd.Style.Add("display", "none");
            tabUniMat.Style.Add("display", "none");
            tabDocumentos.Style.Add("display", "none");
            tabAtiExtAlu.Style.Add("display", "none");
            tabMenEsc.Style.Add("display", "none");
            tabGradeMater.Style.Add("display", "none");
            TabFormaPgto.Style.Add("display", "none");
            tabMaterialColetivo.Style.Add("display", "none");

            if (tab == "CEA")
                tabCuiEspAdd.Style.Add("display", "block");
            else if (tab == "ENA")
                tabEndAdd.Style.Add("display", "block");
            else if (tab == "TEA")
                tabTelAdd.Style.Add("display", "block");

            else if (tab == "RAD")
                tabResAliAdd.Style.Add("display", "block");
            else if (tab == "UMA")
                tabUniMat.Style.Add("display", "block");
            else if (tab == "DOC")
                tabDocumentos.Style.Add("display", "block");
            else if (tab == "AEA")
                tabAtiExtAlu.Style.Add("display", "block");
            else if (tab == "MEN")
                tabMenEsc.Style.Add("display", "block");
            else if (tab == "GRM")
                tabGradeMater.Style.Add("display", "block");
            else if (tab == "PGT")
                TabFormaPgto.Style.Add("display", "block");
            else if (tab == "UNI")
                tabMaterialColetivo.Style.Add("display", "block");
        }

        /// <summary>
        /// Método para controlar Checks da tela de cadastro de matrícula
        /// </summary>
        /// <param name="chk"></param>
        protected void ControlaChecks(CheckBox chk)
        {
            chkMenEscAlu.Checked = false;

            chk.Checked = true;
        }

        /// <summary>
        /// Controle dos campos para não aparecer caso unidade to tipo curso
        /// </summary>
        private void controleCamposCurso()
        {
            if (TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).CO_FLAG_ENSIN_CURSO == "S")
            {
                //string imagemQuatro = imgNumeroInformacoesAdd.Src;
                //imgNumeroMensalidades.Src = imagemQuatro;
                //divInformacoesAdd.Visible = false;

            }
        }

        #endregion

        #region Eventos personalizados
        protected void atualizaParcela()
        {
            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue) && String.IsNullOrEmpty(txtValorPrimParce.Text))
            {
                //-------> Valor calculada a partir do turno da turma
                //-----------> Pega o Curso
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                if (varSer != null)
                {
                    //-----> Determina o número de parcelas utilizado no cálculo das parcelas
                    int qtdParcelas = 0;
                    if (chkGeraTotalParce.Checked)
                    {
                        qtdParcelas = int.Parse(txtQtdeParcelas.Text);
                    }

                    DateTime dataSelec;
                    if (chkDataPrimeiraParcela.Checked && txtDtPrimeiraParcela.Text != "")
                    {
                        dataSelec = DateTime.Parse(txtDtPrimeiraParcela.Text);
                    }
                    else
                    {
                        dataSelec = DateTime.Now.Year == PreAuxili.proximoAnoMat<int>(txtAno.Text) ? (int.Parse(ddlDiaVecto.SelectedValue) > 27 && DateTime.Now.Month == 2 ? DateTime.Parse("27/" + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString()) : DateTime.Parse(ddlDiaVecto.SelectedValue + '/' + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString())) : DateTime.Parse(ddlDiaVecto.SelectedValue + "/01/" + PreAuxili.proximoAnoMat<string>(txtAno.Text));
                        if (DateTime.Now.Year == PreAuxili.proximoAnoMat<int>(txtAno.Text) && dataSelec < DateTime.Now)
                        {
                            if (varSer.QT_DIAS_PARC1 != null)
                            {
                                if (varSer.QT_DIAS_PARC1 > 0)
                                {
                                    dataSelec = DateTime.Now.AddDays((double)varSer.QT_DIAS_PARC1);
                                }
                            }
                            else
                                dataSelec = DateTime.Now;
                        }
                        else
                        {
                            if (varSer.QT_DIAS_PARC1 != null)
                            {
                                if (varSer.QT_DIAS_PARC1 > 0)
                                {
                                    dataSelec = dataSelec.AddDays((double)varSer.QT_DIAS_PARC1);
                                }
                            }
                        }
                    }

                    switch (ddlValorContratoCalc.SelectedValue)
                    {
                        case "P":
                            qtdParcelas = varSer.NU_QUANT_MESES > 0 && varSer.NU_QUANT_MESES == 12 && (!chkGeraTotalParce.Checked) ? varSer.NU_QUANT_MESES - dataSelec.Month + 1 : varSer.NU_QUANT_MESES;
                            break;
                        case "T":
                            qtdParcelas = varSer.NU_QUANT_MESES;
                            break;
                    }

                    decimal x = 0;
                    string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                        ddlTipoContrato.SelectedValue,
                        turnoTurma,
                        varSer,
                        this.Page, false);

                    if (retornoValor == string.Empty)
                        return;
                    else
                    {
                        x = decimal.Parse(retornoValor);
                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                    }
                }
            }
        }

        private List<TB43_GRD_CURSO> GradeSerie(int serie)
        {
            string coAnoGrade = DateTime.Now.Year.ToString();
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

            return TB43_GRD_CURSO.RetornaTodosRegistros().Where(g => g.CO_CUR == serie && g.CO_ANO_GRADE == coAnoGrade && g.CO_EMP == coEmp).ToList<TB43_GRD_CURSO>();
        }
        #endregion

        #region Eventos de componentes

        ///// <summary>
        ///// É executado quando o usuário clica no botão finalizar da tela de Forma de Pagamento
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void lnkFinalFormaPgto_OnClick(object sender, EventArgs e)
        //{


        //    lnkSucPagtoEscAlu.Visible = true;
        //    chkPgtoAluno.Enabled = false;

        //}

        protected void btnNovo_onclick(object sender, EventArgs e)
        {
            AuxiliPagina.RedirecionaParaPaginaSucesso("Providenciando Nova Matrícula...", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        /// <summary>
        /// É chamado quando o usuário marca ou desmarca a opção de Gerar com Taxa de Matrícula na Matrícula Resumida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkTaxaMatricula_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                carregaValorTaxaMatricula();
            }
            else
            {
                txtVlTxMatricula.Enabled = false;
                txtVlTxMatricula.Text = "";
            }
        }

        /// <summary>
        /// Método que trata o clique do botão de CPF do responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCPFResp_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlSituMatAluno.SelectedValue != "S")
            {
                if (this.txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim() != this.hdfCPFRespRes.Value)
                {
                    this.chkRecResResp.Checked = false;
                    this.LimparResponsavel();
                }
            }

            if (this.txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim() != "")
                this.CarregaResponsaveis(0, this.txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim());
        }

        /// <summary>
        /// Método que trata o clique do botão de NIRE do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesqNIRE_Click(object sender, ImageClickEventArgs e)
        {
            ControlaFormularioAluno(true);
            ControlaFormularioResponsavel(false);
            if (this.txtNireAluno.Text.Replace(".", "").Replace("-", "").Trim() != "")
                this.CarregaAluno(0, 0, int.Parse(this.txtNireAluno.Text.Replace(".", "").Replace("-", "").Trim()));
        }

        /// <summary>
        /// Método que trata o clique do botão de Reserva da Matrícula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesqReserva_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtNumReserva.Text.Trim() != "")
            {
                this.LimparResponsavel();
                this.LimparAluno();

                var reser = (from lTb052 in TB052_RESERV_MATRI.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTb052.TB01_CURSO.CO_CUR equals tb01.CO_CUR
                             where tb01.CO_EMP == lTb052.TB01_CURSO.CO_EMP && tb01.TB44_MODULO.CO_MODU_CUR == lTb052.TB01_CURSO.CO_MODU_CUR
                             && lTb052.NU_RESERVA == txtNumReserva.Text.Trim() && lTb052.CO_STATUS == "A"
                             select new { tb01.NO_CUR, tb01.CO_NIVEL_CUR, tb01.TB44_MODULO.DE_MODU_CUR, lTb052 }).FirstOrDefault();

                if (reser != null)
                {
                    btnPesqNIRE.Enabled = false;
                    //txtNumNIRE.Enabled = false;
                    this.ddlSituMatAluno.SelectedValue = "R";
                    reser.lTb052.TB01_CURSOReference.Load();
                    reser.lTb052.TB07_ALUNOReference.Load();
                    reser.lTb052.TB25_EMPRESAReference.Load();
                    reser.lTb052.TB25_EMPRESA1Reference.Load();

                    if (reser.CO_NIVEL_CUR == "I")
                        this.txtDadosReserva.Text = "Ensino Infantil";
                    else if (reser.CO_NIVEL_CUR == "F")
                        this.txtDadosReserva.Text = "Ensino Fundamental";
                    else if (reser.CO_NIVEL_CUR == "M")
                        this.txtDadosReserva.Text = "Ensino Médio";
                    else if (reser.CO_NIVEL_CUR == "G")
                        this.txtDadosReserva.Text = "Graduação";
                    else if (reser.CO_NIVEL_CUR == "C")
                        this.txtDadosReserva.Text = "Pós-Graduação";
                    else if (reser.CO_NIVEL_CUR == "R")
                        this.txtDadosReserva.Text = "Mestrado";
                    else if (reser.CO_NIVEL_CUR == "D")
                        this.txtDadosReserva.Text = "Doutorado";
                    else if (reser.CO_NIVEL_CUR == "S")
                        this.txtDadosReserva.Text = "Pós-Doutorado";
                    else if (reser.CO_NIVEL_CUR == "E")
                        this.txtDadosReserva.Text = "Especialização";
                    else if (reser.CO_NIVEL_CUR == "P")
                        this.txtDadosReserva.Text = "Preparatório";
                    else
                        this.txtDadosReserva.Text = "Outros";

                    this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - " + reser.NO_CUR;

                    if (reser.lTb052.CO_PERI_TUR == "M")
                        this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - Manhã";
                    else if (reser.lTb052.CO_PERI_TUR == "N")
                        this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - Noturno";
                    else
                        this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - Vespertino";

                    int unid2 = (reser.lTb052.TB25_EMPRESA != null) ? reser.lTb052.TB25_EMPRESA.CO_EMP : 0;
                    int unid3 = (reser.lTb052.TB25_EMPRESA1 != null) ? reser.lTb052.TB25_EMPRESA1.CO_EMP : 0;

                    var unidEns = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                  where tb25.CO_EMP.Equals(reser.lTb052.TB01_CURSO.CO_EMP) &&
                                  (unid2 != 0 ? tb25.CO_EMP.Equals(unid2) : unid2 == 0) &&
                                  (unid3 != 0 ? tb25.CO_EMP.Equals(unid3) : unid3 == 0)
                                  select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP };

                    this.ddlUnidade.DataSource = unidEns;
                    this.ddlUnidade.DataTextField = "NO_FANTAS_EMP";
                    this.ddlUnidade.DataValueField = "CO_EMP";
                    this.ddlUnidade.DataBind();
                    this.ddlUnidade.SelectedValue = reser.lTb052.TB01_CURSO.CO_EMP.ToString();
                    this.ddlModalidade.Items.Clear();
                    this.ddlModalidade.Items.Insert(0, new ListItem(reser.DE_MODU_CUR, reser.lTb052.TB01_CURSO.CO_MODU_CUR.ToString()));
                    this.ddlModalidade.Enabled = false;
                    this.ddlSerieCurso.Items.Clear();
                    this.ddlSerieCurso.Items.Insert(0, new ListItem(reser.NO_CUR, reser.lTb052.TB01_CURSO.CO_CUR.ToString()));
                    this.ddlSerieCurso.Enabled = false;

                    CarregaGridDocumentos();

                    this.ddlTurma.Items.Clear();
                    this.ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                                where tb06.CO_CUR == reser.lTb052.TB01_CURSO.CO_CUR && tb06.CO_MODU_CUR == reser.lTb052.TB01_CURSO.CO_MODU_CUR
                                                && tb06.CO_EMP == reser.lTb052.TB01_CURSO.CO_EMP
                                                select new { tb06.CO_TUR, tb06.TB129_CADTURMAS.CO_SIGLA_TURMA }).OrderBy(t => t.CO_SIGLA_TURMA);

                    this.ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    this.ddlTurma.DataValueField = "CO_TUR";
                    this.ddlTurma.DataBind();
                    /*
                    if (this.ddlTurma.SelectedValue != "")
                    {
                        int coTur = int.Parse(this.ddlTurma.SelectedValue);
                        string turno = TB06_TURMAS.RetornaTodosRegistros().Where(p => p.CO_CUR.Equals(reser.lTb052.TB01_CURSO.CO_CUR) &&
                                        p.CO_MODU_CUR.Equals(reser.lTb052.TB01_CURSO.CO_MODU_CUR) &&
                                        p.CO_EMP.Equals(reser.lTb052.TB01_CURSO.CO_EMP) && p.CO_TUR.Equals(coTur)
                                        ).FirstOrDefault().CO_PERI_TUR;

                        if (turno == "M")
                            this.txtTurno.Text = "Manhã";
                        else if (turno == "N")
                            this.txtTurno.Text = "Noturno";
                        else
                            this.txtTurno.Text = "Vespertino";
                    }
                    */
                    reser.lTb052.TB108_RESPONSAVELReference.Load();

                    //----------------> Verifica se na tabela de reserva responsável já é cadastrado.
                    if (reser.lTb052.TB108_RESPONSAVEL != null)
                    {
                        this.CarregaResponsaveis(reser.lTb052.TB108_RESPONSAVEL.CO_RESP, "");
                        this.chkRecResResp.Checked = this.chkRecResResp.Enabled = true;
                        this.hdfCPFRespRes.Value = reser.lTb052.TB108_RESPONSAVEL.NU_CPF_RESP;
                    }
                    else
                    {
                        this.chkRecResResp.Checked = this.chkRecResResp.Enabled = true;
                        reser.lTb052.TB905_BAIRRO1Reference.Load();
                        this.txtNomeResp.Text = reser.lTb052.NO_RESP.ToUpper();
                        this.txtNoRespCPF.Text = reser.lTb052.NO_RESP.ToUpper();
                        this.txtCPFResp.Text = reser.lTb052.NU_CPF_RESP;
                        this.hdfCPFRespRes.Value = reser.lTb052.NU_CPF_RESP;
                        this.txtCPFResp.Text = reser.lTb052.NU_CPF_RESP;
                        this.txtTelResidencialResp.Text = reser.lTb052.NU_TEL_RESP;
                        this.txtTelCelularResp.Text = reser.lTb052.NU_CEL_RESP;
                        this.txtTelCelularRespNonoDig.Text = reser.lTb052.NU_CEL_RESP;
                        this.CarregaUfs(this.ddlUfResp);
                        this.ddlUfResp.SelectedValue = (reser.lTb052.TB905_BAIRRO1 != null) ? reser.lTb052.TB905_BAIRRO1.CO_UF : "";
                        this.CarregaCidades(this.ddlCidadeResp, this.ddlUfResp);
                        reser.lTb052.TB905_BAIRRO1Reference.Load();
                        this.ddlCidadeResp.SelectedValue = reser.lTb052.TB905_BAIRRO1.CO_CIDADE.ToString();
                        this.CarregaBairros(this.ddlBairroResp, this.ddlCidadeResp);
                        this.ddlBairroResp.SelectedValue = reser.lTb052.TB905_BAIRRO1.CO_BAIRRO.ToString();
                        this.txtDtNascResp.Text = reser.lTb052.DT_NASC_RESP != null ? reser.lTb052.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                        this.txtCepResp.Text = reser.lTb052.CO_CEP_RESP;
                        this.txtEmailResp.Text = reser.lTb052.DE_EMAIL_RESP;
                        this.txtNumeroResp.Text = reser.lTb052.NU_END_RESP != null ? reser.lTb052.NU_END_RESP.ToString() : "";
                        this.txtLogradouroResp.Text = reser.lTb052.DE_END_RESP;
                        this.txtComplementoResp.Text = reser.lTb052.DE_COM_END_RESP;
                    }

                    reser.lTb052.TB07_ALUNOReference.Load();
                    if (reser.lTb052.TB07_ALUNO != null)
                        this.CarregaAluno(reser.lTb052.TB07_ALUNO.CO_ALU, reser.lTb052.TB07_ALUNO.CO_EMP, 0);
                    else
                    {
                        reser.lTb052.TB905_BAIRROReference.Load();
                        reser.lTb052.TB905_BAIRROReference.Load();
                        this.ControleImagemAluno.ImagemLargura = 64;
                        this.ControleImagemAluno.ImagemAltura = 85;
                        this.txtNomeAluno.Text = reser.lTb052.NO_ALU.ToUpper();
                        //this.txtNoInfAluno.Text = reser.lTb052.NO_ALU.ToUpper();
                        this.ddlNacionalidadeAluno.SelectedValue = reser.lTb052.NO_NACIO_ALU;
                        this.txtTelCelularAluno.Text = reser.lTb052.NU_CEL_ALU;
                        this.txtTelCelularAlunoNoveDigit.Text = reser.lTb052.NU_CEL_ALU;
                        this.txtCpfAluno.Text = reser.lTb052.NU_CPF_ALU;
                        this.txtTelResidencialAluno.Text = reser.lTb052.NU_TEL_ALU;
                        this.txtCepAluno.Text = reser.lTb052.CO_CEP_ALU;
                        this.txtNomeMaeAlunoValid.Text = reser.lTb052.NO_MAE_ALU;
                        this.CarregaUfs(this.ddlUFAluno);
                        this.ddlUFAluno.SelectedValue = (reser.lTb052.TB905_BAIRRO != null) ? reser.lTb052.TB905_BAIRRO.CO_UF.ToString() : "";
                        this.CarregaCidades(this.ddlCidadeAluno, this.ddlUFAluno);
                        this.ddlCidadeAluno.SelectedValue = (reser.lTb052.TB905_BAIRRO != null) ? reser.lTb052.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                        this.CarregaBairros(this.ddlBairroAluno, this.ddlCidadeAluno);
                        this.ddlBairroAluno.SelectedValue = (reser.lTb052.TB905_BAIRRO != null) ? reser.lTb052.TB905_BAIRRO.CO_BAIRRO.ToString() : "";
                        this.txtNumeroAluno.Text = reser.lTb052.NU_END_ALU != null ? reser.lTb052.NU_END_ALU.ToString() : "";
                        this.txtLogradouroAluno.Text = reser.lTb052.DE_END_ALU;
                        //this.txtComplementoAluno.Text = reser.lTb052.DE_COM_END_ALU;
                        this.ddlSexoAluno.SelectedValue = reser.lTb052.CO_SEXO_ALU;
                        this.txtDataNascimentoAluno.Text = reser.lTb052.DT_NASC_ALU != null ? reser.lTb052.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                    }
                }
                else
                {
                    this.txtDadosReserva.Text = "";
                    this.ddlUnidade.Items.Clear();
                    this.CarregaModalidades();
                    this.CarregaSerieCurso();
                    this.CarregaTurma();
                    btnPesqNIRE.Enabled = true;
                    //btnPesqNIRE.Enabled = txtNumNIRE.Enabled = true;
                    ddlSituMatAluno.SelectedValue = "S";
                }
            }
        }

        /// <summary>
        /// Preenche os campos de endereço do aluno de acordo com o CEP, se o mesmo possuir registro na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesqCEPA_Click(object sender, ImageClickEventArgs e)
        {
            ControlaTabs("ALU");

            if (txtCepAluno.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepAluno.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    tb235.TB240_TIPO_LOGRADOUROReference.Load();

                    txtLogradouroAluno.Text = tb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA + " " + tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUFAluno.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeAluno, ddlUFAluno);
                    ddlCidadeAluno.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroAluno, ddlCidadeAluno);
                    ddlBairroAluno.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                    txtLogradouroAluno.Text = ddlBairroAluno.SelectedValue = ddlCidadeAluno.SelectedValue = ddlUFAluno.SelectedValue = "";
            }
            UpdatePanel1.Update();
        }

        /// <summary>
        /// Preenche os campos de endereço do responsável de acordo com o CEP, se o mesmo possuir registro na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesqCEPR_Click(object sender, ImageClickEventArgs e)
        {
            ControlaTabs("RES");

            if (txtCepResp.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepResp.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

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
                    txtLogradouroResp.Text = "";
                    ddlBairroResp.SelectedValue = "";
                    ddlCidadeResp.SelectedValue = "";
                    ddlUfResp.SelectedValue = "";
                }
            }
            UpdatePanel2.Update();
        }

        /// <summary>
        /// Preenche os campos de endereço da empresa do responsável de acordo com o CEP, se o mesmo possuir registro na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCEPEmp_Click(object sender, ImageClickEventArgs e)
        {
            ControlaTabs("RES");

            if (txtCepEmpresaResp.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepEmpresaResp.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    //txtLogradouroEmpResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfEmpResp.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeEmpResp, ddlUfEmpResp);
                    ddlCidadeEmpResp.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    //CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
                    //ddlBairroEmpResp.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else { }
                //txtLogradouroEmpResp.Text = ddlBairroEmpResp.SelectedValue = ddlCidadeEmpResp.SelectedValue = ddlUfEmpResp.SelectedValue = "";
            }
        }

        /// <summary>
        /// Preenche os campos de endereço do CEP, se o mesmo possuir registro na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCEPETA_Click(object sender, ImageClickEventArgs e)
        {
            ControlaTabs("ENA");

            if (txtCepETA.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepETA.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograETA.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUFETA.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidadeETA, ddlUFETA);
                    ddlCidadeETA.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairroETA, ddlCidadeETA);
                    ddlBairroETA.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                    txtLograETA.Text = ddlBairroETA.SelectedValue = ddlCidadeETA.SelectedValue = ddlUFETA.SelectedValue = "";
            }
        }

        protected void ddlUfEmpResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidadeEmpResp, ddlUfEmpResp);
            //CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
            ddlCidadeEmpResp.Focus();
        }

        protected void ddlCidadeEmpResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CarregaBairros(ddlBairroEmpResp, ddlCidadeEmpResp);
        }

        protected void ddlUfResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CarregaCidades(ddlCidadeResp, ddlUfResp);
            carregaCidadesRefeita();
            CarregaBairrosRefeito();
            //CarregaBairros(ddlBairroResp, ddlCidadeResp);
            UpdatePanel2.Update();
            ddlCidadeResp.Focus();
        }

        protected void ddlCidadeResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CarregaBairros(ddlBairroResp, ddlCidadeResp);
            CarregaBairrosRefeito();
            UpdatePanel2.Update();
            ddlBairroResp.Focus();
        }

        protected void ddlUFAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidadeAluno, ddlUFAluno);
            CarregaBairros(ddlBairroAluno, ddlCidadeAluno);
            ControlaFormularioAluno(true);
            ControlaFormularioResponsavel(false);
            ddlCidadeAluno.Focus();
        }

        protected void ddlUFETA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("ENA");
            CarregaCidades(ddlCidadeETA, ddlUFETA);
            CarregaBairros(ddlBairroETA, ddlCidadeETA);
        }

        protected void ddlCidadeAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlBairroAluno, ddlCidadeAluno);
            ControlaFormularioAluno(true);
            ControlaFormularioResponsavel(false);
            ddlBairroAluno.Focus();
        }

        protected void ddlCidadeETA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("ENA");
            CarregaBairros(ddlBairroETA, ddlCidadeETA);
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            grdGrdCurso.DataSource = grdCursoExt.DataSource = null;
            grdGrdCurso.DataBind();
            grdCursoExt.DataBind();
            ddlSerieCurso.Focus();
            ControlaTabs("GRM");
        }

        protected void chkRecResResp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRecResResp.Checked && (this.txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim() != this.hdfCPFRespRes.Value))
            {
                this.LimparResponsavel();
                if (this.hdCodResp.Value != "")
                {
                    this.CarregaResponsaveis(0, this.hdfCPFRespRes.Value);
                    this.txtCPFResp.Text = this.txtCPFRespDados.Text;
                }
                else
                {
                    TB052_RESERV_MATRI tb052 = TB052_RESERV_MATRI.RetornaPelaChavePrimaria(this.txtNumReserva.Text, LoginAuxili.CO_EMP);

                    if (tb052 != null)
                    {
                        tb052.TB905_BAIRRO1Reference.Load();
                        tb052.TB905_BAIRRO1Reference.Load();

                        this.txtNomeResp.Text = tb052.NO_RESP.ToUpper();
                        this.txtNoRespCPF.Text = tb052.NO_RESP.ToUpper();
                        this.txtCPFRespDados.Text = tb052.NU_CPF_RESP;
                        this.txtCPFResp.Text = tb052.NU_CPF_RESP;
                        this.txtTelResidencialResp.Text = tb052.NU_TEL_RESP;
                        this.txtTelCelularResp.Text = tb052.NU_CEL_RESP;
                        this.txtTelCelularRespNonoDig.Text = tb052.NU_CEL_RESP;
                        this.CarregaUfs(this.ddlUfResp);
                        this.ddlUfResp.SelectedValue = (tb052.TB905_BAIRRO1 != null) ? tb052.TB905_BAIRRO1.CO_UF : "";
                        this.CarregaCidades(this.ddlCidadeResp, this.ddlUfResp);
                        this.ddlCidadeResp.SelectedValue = tb052.TB905_BAIRRO1.CO_CIDADE.ToString();
                        this.CarregaBairros(this.ddlBairroResp, this.ddlCidadeResp);
                        this.ddlBairroResp.SelectedValue = tb052.TB905_BAIRRO1.CO_BAIRRO.ToString();
                        this.txtDtNascResp.Text = tb052.DT_NASC_RESP != null ? tb052.DT_NASC_RESP.Value.ToString("dd/MM/yyyy") : "";
                        this.txtCepResp.Text = tb052.CO_CEP_RESP;
                        this.txtEmailResp.Text = tb052.DE_EMAIL_RESP;
                        this.txtNumeroResp.Text = tb052.NU_END_RESP != null ? tb052.NU_END_RESP.ToString() : "";
                        this.txtLogradouroResp.Text = tb052.DE_END_RESP;
                        this.txtComplementoResp.Text = tb052.DE_COM_END_RESP;
                    }
                }
            }
        }

        protected void chkGeraTotalParce_CheckedChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");

            //Verifica se o usuário escolheu proprocional meses ou total, só atribui a quantidade do cadastro do curso quando se é total
            if (ddlValorContratoCalc.SelectedValue == "T")
            {
                TB01_CURSO serie = TB01_CURSO.RetornaPeloCoCur(int.Parse(this.ddlSerieCurso.SelectedValue));

                txtQtdeParcelas.Text = serie.NU_QUANT_MESES.ToString();
            }
            txtQtdeParcelas.Enabled = chkGeraTotalParce.Checked;
        }

        protected void ddlValorContratoCalc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Verifica se é total ou proporcional, para atribuir os devidos valores para cada caso
            if (ddlValorContratoCalc.SelectedValue == "P")
            {
                int aux1 = 12 - DateTime.Now.Month;
                int aux2 = aux1 + 1;
                txtQtdeParcelas.Text = aux2.ToString();
            }
            else
            {
                int serie = TB01_CURSO.RetornaPeloCoCur(int.Parse(this.ddlSerieCurso.SelectedValue)).NU_QUANT_MESES;

                txtQtdeParcelas.Text = serie.ToString();
                txtQtdeParcelas.Enabled = chkGeraTotalParce.Checked;
            }

            CalculaValorPrimeiraParcela();
        }

        protected void chkManterDesconto_CheckedChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");

            if (chkManterDesconto.Checked)
            {
                // Habilita os campos de alteração do desconto do aluno
                ddlTpBolsaAlt.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoIniDesconto.Enabled =
                txtPeriodoFimDesconto.Enabled = true;

                // Garante que o tipo de desconto, da alteração, seja igual ao tipo de desconto do cadastro de aluno
                //ddlTpBolsaAlt.SelectedValue = ddlTipoBolsa.SelectedValue;

                CarregaBolsasAlt();

                //ddlBolsaAlunoAlt.SelectedValue = ddlBolsaAluno.SelectedValue;

                //txtPeriodoIniDesconto.Text = txtPeriodoDeIniBolAluno.Text;
                //txtPeriodoFimDesconto.Text = txtPeriodoTerBolAluno.Text;

                //txtValorDescto.Text = txtDescontoAluno.Text;
                //chkManterDescontoPerc.Checked = chkDesctoPercBolsa.Checked;

                #region Carrega a bolsa cadastrada para o aluno
                //int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);
                //var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //             where iTb148.CO_TIPO_BOLSA == coBolsa
                //             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                //if (tb148 != null)
                //{
                //    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                //    chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                //    if (tb148.DT_INICI_TIPO_BOLSA != null)
                //    {
                //        txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //        txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //    }
                //    else
                //    {
                //        txtPeriodoIniDesconto.Text = "";
                //        txtPeriodoFimDesconto.Text = "";
                //    }
                //}
                #endregion

            }
            else
            {
                // Habilita os campos de alteração do desconto do aluno
                ddlTpBolsaAlt.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoIniDesconto.Enabled =
                txtValorDescto.Enabled =
                chkManterDescontoPerc.Enabled =
                txtPeriodoFimDesconto.Enabled = false;

                // Garante que o tipo de desconto, da alteração, seja igual ao tipo de desconto do cadastro de aluno
                // ddlTpBolsaAlt.SelectedValue = ddlTipoBolsa.SelectedValue;

                CarregaBolsasAlt();

                //ddlBolsaAlunoAlt.SelectedValue = ddlBolsaAluno.SelectedValue;

                //txtPeriodoIniDesconto.Text = txtPeriodoDeIniBolAluno.Text;
                //txtPeriodoFimDesconto.Text = txtPeriodoTerBolAluno.Text;

                //txtValorDescto.Text = txtDescontoAluno.Text;
                //chkManterDescontoPerc.Checked = chkDesctoPercBolsa.Checked;

                #region Carrega a bolsa cadastrada para o aluno
                //int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);
                //var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //             where iTb148.CO_TIPO_BOLSA == coBolsa
                //             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                //if (tb148 != null)
                //{
                //    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                //    chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                //    if (tb148.DT_INICI_TIPO_BOLSA != null)
                //    {
                //        txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //        txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //    }
                //    else
                //    {
                //        txtPeriodoIniDesconto.Text = "";
                //        txtPeriodoFimDesconto.Text = "";
                //    }
                //}
                #endregion
            }

        }

        protected void chkAtualiFinan_CheckedChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");
            ControlaChecks(chkMenEscAlu);

            grdNegociacao.DataSource = null;
            grdNegociacao.DataBind();

            if (chkAtualiFinan.Checked)
            {
                ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtDesctoMensa.Enabled = true;
                ddlTipoDesctoMensa.SelectedValue = "T";
                #region Habilita os campos
                ddlTipoValorCurso.Enabled =
                chkTipoContrato.Enabled =
                chkValorContratoCalc.Enabled =
                chkAlterValorContr.Enabled =
                chkGeraTotalParce.Enabled =
                chkDataPrimeiraParcela.Enabled =
                chkManterDesconto.Enabled =
                ddlTipoDesctoMensa.Enabled =
                txtDesctoMensa.Enabled =
                ddlBoleto.Enabled =
                ddlDiaVecto.Enabled =
                chkTipoContrato.Enabled = true;
                txtMesIniDesconto.Enabled = false;
                #endregion

                var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                              where adUs.CodUsuario == LoginAuxili.CO_COL
                              select new { adUs.FLA_ALT_BOL_ALU, adUs.FLA_ALT_BOL_ESPE_ALU, adUs.FLA_ALT_PARAM_MAT }).FirstOrDefault();

                if (admUsu != null)
                {
                    //-----------> Valida se o usuário possui permissão para alterar o desconto dado ao aluno.
                    if (admUsu.FLA_ALT_BOL_ALU == "S")
                    {
                        chkManterDesconto.Enabled = true;
                    }
                    else
                    {
                        chkManterDesconto.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled = false;
                    }

                    //-----------> Valida se o usuário possui permissão para alterar o desconto especial dado ao aluno.
                    if (admUsu.FLA_ALT_BOL_ESPE_ALU == "S")
                    {
                        ddlTipoDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled = true;
                    }
                    else
                    {
                        ddlTipoDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled = false;
                    }

                    //-----------> Valida se o usuário possui permissão para alterar parâmetros da matrícula
                    if (admUsu.FLA_ALT_PARAM_MAT == "S")
                    {
                        chkTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        chkDataPrimeiraParcela.Enabled = true;
                    }
                    else
                    {
                        chkTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        txtValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        chkDataPrimeiraParcela.Enabled = false;
                    }
                }
                else
                {
                    chkManterDesconto.Enabled =
                        ddlTpBolsaAlt.Enabled =
                        ddlBolsaAlunoAlt.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled = false;
                    ddlTipoDesctoMensa.Enabled =
                        txtQtdeMesesDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled =
                        chkTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        txtValorContratoCalc.Enabled =
                        chkDataPrimeiraParcela.Enabled = false;
                }
            }
            else
            {
                ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = false;
                txtQtdeMesesDesctoMensa.Text = txtDesctoMensa.Text = txtTotalMensa.Text =
                    txtTotalDesctoBolsa.Text = txtTotalDesctoEspec.Text = txtTotalLiquiContra.Text = "";
                ddlTipoDesctoMensa.SelectedValue = "T";
                #region Desabilita os campos
                ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled =
                    chkTipoContrato.Enabled =
                    ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled =
                    chkValorContratoCalc.Enabled =
                    txtValorContratoCalc.Enabled =
                    chkAlterValorContr.Enabled =
                    ddlValorContratoCalc.Enabled =
                    chkGeraTotalParce.Enabled =
                    txtQtdeParcelas.Enabled =
                    RequiredFieldValidator6.Enabled =
                    chkDataPrimeiraParcela.Enabled =
                    txtDtPrimeiraParcela.Enabled =
                    txtValorPrimParce.Enabled =
                    chkManterDesconto.Enabled =
                    ddlTpBolsaAlt.Enabled =
                    ddlBolsaAlunoAlt.Enabled =
                    txtValorDescto.Enabled =
                    chkManterDescontoPerc.Enabled =
                    txtPeriodoIniDesconto.Enabled =
                    txtPeriodoFimDesconto.Enabled =
                    ddlTipoDesctoMensa.Enabled =
                    txtQtdeMesesDesctoMensa.Enabled =
                    txtDesctoMensa.Enabled =
                    txtMesIniDesconto.Enabled =
                    ddlBoleto.Enabled =
                    ddlDiaVecto.Enabled =
                    chkTipoContrato.Enabled = false;
                #endregion
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            MontaGridGrdCurso();
            MontaGridCursoExtra();
            ControlaTabs("GRM");
            ddlTurma.Focus();
            if (ddlSerieCurso.SelectedValue != "")
                CarregaSerieCurso(int.Parse(ddlSerieCurso.SelectedValue), true);
            chkMenEscAlu.Checked = false;
        }

        protected void ddlSituMatAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSituMatAluno.SelectedValue == "S")
            {
                AuxiliPagina.RedirecionaParaPaginaCadastro();
                this.Session[SessoesHttp.CodigoMatriculaAluno] = null;
            }
            else
            {
                btnPesqNIRE.Enabled = false;
            }
            // = txtNumNIRE.Enabled = false;
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(this.ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(this.ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(this.ddlTurma.SelectedValue) : 0;

            if (turma != 0)
            {
                var turno = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                             where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade && tb06.CO_TUR == turma
                             select new { tb06.CO_PERI_TUR }).FirstOrDefault();

                if (turno != null && turno.CO_PERI_TUR != null)
                {
                    string strTurno = turno.CO_PERI_TUR;
                    if (strTurno == "M")
                        this.txtTurno.Text = "MANHÃ";
                    else if (strTurno == "N")
                        this.txtTurno.Text = "NOITE";
                    else
                        this.txtTurno.Text = "TARDE";
                }
            }
            ControlaTabs("GRM");
            chkMenEscAlu.Checked = false;
            lnkConfirModSerTur.Enabled = true;
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(this.ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(this.ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(this.ddlSerieCurso.SelectedValue) : 0;
            string strSerieRef = TB01_CURSO.RetornaTodosRegistros().Where(p => p.CO_CUR.Equals(serie)).FirstOrDefault().CO_SERIE_REFER;

            if (strSerieRef != "")
            {
                var tb01 = TB01_CURSO.RetornaTodosRegistros().Where(s => s.CO_MODU_CUR == modalidade && s.CO_EMP == coEmp).FirstOrDefault();

                if (tb01 != null)
                {
                    this.ddlSerieCurso.Items.Clear();
                    this.ddlSerieCurso.Items.Insert(0, new ListItem(tb01.NO_CUR, tb01.CO_CUR.ToString()));

                    this.ddlTurma.Items.Clear();
                    this.ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                                where tb06.CO_MODU_CUR == modalidade && tb06.CO_EMP == coEmp && tb06.CO_CUR == tb01.CO_CUR
                                                select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                    this.ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    this.ddlTurma.DataValueField = "CO_TUR";
                    this.ddlTurma.DataBind();

                    if (this.ddlTurma.SelectedValue != "")
                    {
                        int turma = int.Parse(this.ddlTurma.SelectedValue);

                        string strTurno = TB06_TURMAS.RetornaTodosRegistros().Where(t => t.CO_CUR == tb01.CO_CUR && t.CO_MODU_CUR == modalidade && t.CO_TUR == turma).FirstOrDefault().CO_PERI_TUR;

                        if (strTurno == "M")
                            this.txtTurno.Text = "MANHÃ";
                        else if (strTurno == "N")
                            this.txtTurno.Text = "NOITE";
                        else
                            this.txtTurno.Text = "TARDE";
                    }
                }
                else
                {
                    this.ddlUnidade.SelectedValue = TB01_CURSO.RetornaTodosRegistros().Where(c => c.CO_CUR == serie).FirstOrDefault().CO_EMP.ToString();
                    AuxiliPagina.EnvioMensagemErro(this, "Unidade escolhida não possui série cadastrada.");
                }
            }
        }

        protected void lnkAtualizaEndAlu_Click(object sender, EventArgs e)
        {
            chkEndAddAlu.Enabled = false;
            lblSucEndAddAlu.Visible = true;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Endereço(s) atualizado(s) com sucesso.");

            chkTelAddAlu.Checked = true;
            ControlaTabs("TEA");
        }

        protected void lnkAtualizaTelAlu_Click(object sender, EventArgs e)
        {
            chkTelAddAlu.Enabled = false;
            lblSucTelAddAlu.Visible = true;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Telefone(s) atualizado(s) com sucesso.");

            chkCuiEspAlu.Checked = true;
            ControlaTabs("CEA");
        }

        protected void lnkCuiEspAlu_Click(object sender, EventArgs e)
        {
            chkCuiEspAlu.Enabled = false;
            lblSucCuiEspAlu.Visible = true;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Cuidado(s) Especial(is) atualizado(s) com sucesso.");

            chkResAliAlu.Checked = true;
            ControlaTabs("RAD");
        }

        protected void lnkResAliAlu_Click(object sender, EventArgs e)
        {
            chkResAliAlu.Enabled = false;
            lblSucResAliAlu.Visible = true;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Restrição(s) Alimentar(es) atualizada(s) com sucesso.");

            chkRegAtiExt.Checked = true;
            ControlaTabs("AEA");
        }

        protected void lnkAtiExtAlu_Click(object sender, EventArgs e)
        {
            chkRegAtiExt.Enabled = false;
            lblSucRegAtiExt.Visible = true;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Atividade(s) Extra(s) atualizada(s) com sucesso.");

            chkDocMat.Checked = true;
            ControlaTabs("DOC");
        }

        protected void btnMaisLinhaChequePgto_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridChequesPgto();
        }

        protected void lnkMenAlu_Click(object sender, EventArgs e)
        {
            if (chkAtualiFinan.Checked)
            {
                if (grdNegociacao.Rows.Count == 0)
                {
                    ControlaTabs("MEN");
                    AuxiliPagina.EnvioMensagemErro(this, "É necessário criar a grid de mensalidades.");
                    return;
                }
            }

            lnkSucMenEscAlu.Visible = true;
            chkMenEscAlu.Enabled = false;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Mensalidade(s) verificada(s) com sucesso.");

            chkMenEscAlu.Enabled = chkAtualiFinan.Enabled = ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = lnkMenAlu.Enabled =
                txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = lnkMontaGridMensa.Enabled = lnkMenAlu.Enabled = false;

            #region Altera o desconto do aluno
            decimal decimalRetorno;
            DateTime dataRetorno;

            int coAlu = this.hdCodAluno.Value != "" ? Convert.ToInt32(this.hdCodAluno.Value) : 0;
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            if (tb07 != null)
            {
                tb07.TB148_TIPO_BOLSA = this.ddlBolsaAlunoAlt.SelectedValue != "" && this.ddlBolsaAlunoAlt.SelectedValue != "0" ? TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(this.ddlBolsaAlunoAlt.SelectedValue)) : null;
                if (chkManterDescontoPerc.Checked)
                {
                    tb07.NU_PEC_DESBOL = decimal.TryParse(this.txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb07.NU_VAL_DESBOL = null;
                }
                else
                {
                    tb07.NU_PEC_DESBOL = null;
                    tb07.NU_VAL_DESBOL = decimal.TryParse(this.txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                }

                tb07.DT_VENC_BOLSA = DateTime.TryParse(this.txtPeriodoIniDesconto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb07.DT_VENC_BOLSAF = DateTime.TryParse(this.txtPeriodoFimDesconto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            }
            TB07_ALUNO.SaveOrUpdate(tb07);
            #endregion

            if (decimal.Parse(txtValorContratoCalc.Text) > 0)
            {
                chkPgtoAluno.Checked = true;
                chkPgtoAluno.Enabled = true;

                txtNomeAlunoPgto.Text = txtNomeAluno.Text;
                txtNireAlunoPgto.Text = txtNireAluno.Text;
                txtNisAlunoPgto.Text = txtNISAluAEA.Text;
                txtValContrPgto.Text = txtValorContratoCalc.Text;

                //contabiliza o total de desconto da matrícula
                decimal totalDesconto = ((!string.IsNullOrEmpty(txtTotalDesctoBolsa.Text) ? decimal.Parse(txtTotalDesctoBolsa.Text) : 0) + (!string.IsNullOrEmpty(txtTotalDesctoEspec.Text) ? decimal.Parse(txtTotalDesctoEspec.Text) : 0));
                txtValDescTotPGTO.Text = totalDesconto.ToString("N2");

                //Verifica se foi gerado algum boleto, caso tenha sido, soma ele para apresentar na aba forma de pagamento
                if ((ddlBoleto.SelectedValue != "") && (grdNegociacao.Rows.Count > 0))
                {
                    txtTotalBolPGTO.Text = txtTotalLiquiContra.Text;
                }

                txtQtPgto.Text = grdNegociacao.Rows.Count.ToString();
                txtQtPgto.Enabled = false;
                //txtNrContraPgto.Enabled = false;

                ControlaTabs("PGT");
                chkMenEscAlu.Checked = true;
            }

            //chkEndAddAlu.Checked = true;
            //ControlaTabs("ENA");
        }

        protected void lnkAtuDoctos_Click(object sender, EventArgs e)
        {
            int coAlu = hdCodAluno.Value != null ? int.Parse(hdCodAluno.Value) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            //bool alter = false;

            TB120_DOC_ALUNO_ENT tb120;

            //--------> Percorre todas as linhas da grid de Documentos
            foreach (GridViewRow row in grdDocumentos.Rows)
            {
                HiddenField hdCoTpDoc = ((HiddenField)row.Cells[2].FindControl("hdCoTpDoc"));
                int codDoc = Convert.ToInt32(hdCoTpDoc.Value);

                if (((CheckBox)row.Cells[0].FindControl("ckSelect")).Checked)
                {
                    //alter = true;
                    tb120 = (from lTb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                             where lTb120.CO_EMP == coEmp && lTb120.CO_ALU == coAlu && lTb120.CO_TP_DOC_MAT == codDoc
                             select lTb120).FirstOrDefault();

                    if (tb120 == null)
                    {
                        tb120 = new TB120_DOC_ALUNO_ENT();
                        var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb120.CO_ALU = coAlu;
                        tb120.CO_TP_DOC_MAT = codDoc;
                        tb120.CO_EMP = coEmp;
                        tb120.TB07_ALUNO = refAluno;
                        tb120.TB121_TIPO_DOC_MATRICULA = TB121_TIPO_DOC_MATRICULA.RetornaPelaChavePrimaria(codDoc);
                        refAluno.TB25_EMPRESA1Reference.Load();
                        tb120.TB25_EMPRESA = refAluno.TB25_EMPRESA1;

                        TB120_DOC_ALUNO_ENT.SaveOrUpdate(tb120, true);
                    }
                }
                else
                {
                    tb120 = (from lTb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                             where lTb120.CO_EMP == coEmp && lTb120.CO_ALU == coAlu && lTb120.CO_TP_DOC_MAT == codDoc
                             select lTb120).FirstOrDefault();

                    if (tb120 != null)
                        TB120_DOC_ALUNO_ENT.Delete(tb120, false);
                }
            }

            grdDocumentos.Enabled = lnkAtuDoctos.Enabled = chkDocMat.Enabled = false;
            lblSucDocAlu.Visible = true;

            //if (alter)
            //AuxiliPagina.EnvioMensagemSucesso(this, "Documento(s) atualizado(s) com sucesso.");            

            int coEmpUnidCont = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            if (TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmpUnidCont).FL_INTEG_FINAN == "S")
            {
                //CarregaBoletos();
                //chkMenEscAlu.Enabled = chkAtualiFinan.Checked = true;

                chkMenEscAlu.Checked = true;
                ControlaTabs("MEN");
            }
            else
            {
                chkMenEscAlu.Enabled = chkAtualiFinan.Enabled = ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = lnkMenAlu.Enabled =
                txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = lnkMontaGridMensa.Enabled = lnkMenAlu.Enabled = false;
            }
            chkMenEscAlu.Checked = true;

            if (chkMenEscAlu.Enabled)
                ControlaTabs("MEN");
            else
                ControlaTabs("DOC");
        }

        protected void ddlAtivExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("AEA");
            //ControlaChecks(chkRegAtiExt);

            if (ddlAtivExtra.SelectedValue != "")
            {
                int codAE = int.Parse(ddlAtivExtra.SelectedValue);

                var tb105 = (from lTb105 in TB105_ATIVIDADES_EXTRAS.RetornaTodosRegistros()
                             where lTb105.CO_ATIV_EXTRA == codAE
                             select new { lTb105.SIGLA_ATIV_EXTRA, lTb105.VL_ATIV_EXTRA }).FirstOrDefault();

                if (tb105 != null)
                {
                    txtSiglaAEA.Text = tb105.SIGLA_ATIV_EXTRA;
                    txtValorAEA.Text = tb105.VL_ATIV_EXTRA != null ? tb105.VL_ATIV_EXTRA.ToString() : "";
                }
                else
                    txtSiglaAEA.Text = txtValorAEA.Text = "";
            }
        }

        protected void ddlTipoDesctoMensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");

            if (ddlTipoDesctoMensa.SelectedValue == "M")
            {
                txtQtdeMesesDesctoMensa.Enabled = true;
                txtMesIniDesconto.Enabled = true;
            }
            else
            {
                txtQtdeMesesDesctoMensa.Enabled = false;
                txtQtdeMesesDesctoMensa.Text = "";
                txtMesIniDesconto.Enabled = false;
                txtMesIniDesconto.Text = "";
            }
        }

        protected void lnkBolCarne_Click(object sender, EventArgs e)
        {
            if (ddlBoleto.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, pois não existe boleto selecionado.");
                return;
            }
            else
            {
                GeraBoleto();
                imgRecMatric.Src = "/Library/IMG/Gestor_IcoImpres.ico";
                lblRecibo.Text = "CONTRATO";
                imgBolCarne.Src = "/Library/IMG/Gestor_IcoImpres.ico";
                lblBoleto.Text = "BOLETO";
                imgEfetiMatric.Src = "/Library/IMG/Gestor_CheckSucess.png";
                //lblEfetiMatric.Text = "EFETIVAR";
            }
        }

        protected void lnkRecMatric_Click(object sender, EventArgs e)
        {
            if (Session[SessoesHttp.CodigoMatriculaAluno] == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível imprimir Recibo/Protocolo de Matrícula. Aluno deve ser matriculado.");
                return;
            }

            //--------> Variáveis obrigatórias para gerar o Relatório
            string codAlunoCad;
            int lRetorno;
            string anoRef = PreAuxili.proximoAnoMat<string>(txtAno.Text);
            //--------> Variáveis de parâmetro do Relatório
            int codEmp;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codAlunoCad = Session[SessoesHttp.CodigoMatriculaAluno].ToString();

            RptContrato rpt = new RptContrato();
            lRetorno = rpt.InitReport(codEmp, codAlunoCad, "M", anoRef);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            HttpContext.Current.Session["ApresentaRelatorio"] = 1;

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            /*
            if (Session[SessoesHttp.CodigoMatriculaAluno] == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível imprimir Recibo/Protocolo de Matrícula. Aluno deve ser matriculado.");
                return;
            }
//--------> GERAÇÃO DO RELATÓRIO ###
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU_CAD;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprReserMatric");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

            strP_CO_EMP = LoginAuxili.CO_EMP.ToString();
            strP_CO_ALU_CAD = Session[SessoesHttp.CodigoMatriculaAluno].ToString();

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelComprMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU_CAD);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            Session["ApresentaRelatorio"] = "1";

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Comprovante de matrícula gerado com sucesso.");

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();*/
        }

        protected void lnkFichaMatric_Click(object sender, EventArgs e)
        {
            if (Session[SessoesHttp.CodigoMatriculaAluno] == null || Session[SessoesHttp.CodigoMatriculaAluno] == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível imprimir a ficha de Matrícula. Aluno deve ser matriculado.");
                return;
            }

            //--------> Variáveis obrigatórias para gerar o Relatório
            string codAlunoCad;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string codEmp;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP.ToString();
            codAlunoCad = Session[Resources.SessoesHttp.CodigoMatriculaAluno].ToString();
            if (codAlunoCad != "")
            {
                string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                string parametros = "";
                C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno.RptFichaMatricAluno rpt = new C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno.RptFichaMatricAluno();
                lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, infos, codEmp, codAlunoCad, DateTime.Now.Year.ToString(), "Mat");
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";
                HttpContext.Current.Session["ApresentaRelatorio"] = 1;
            }
        }

        protected void ddlNacioResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNacioResp.SelectedValue == "BR")
                ddlUfNacionalidadeResp.Enabled = true;
            else
            {
                ddlUfNacionalidadeResp.Enabled = false;
                ddlUfNacionalidadeResp.SelectedValue = "";
            }
        }

        protected void ddlBolsaAlunoAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");

            if (ddlBolsaAlunoAlt.SelectedValue == "")
            {
                txtValorDescto.Text = txtPeriodoIniDesconto.Text = txtPeriodoFimDesconto.Text = "";
                txtValorDescto.Enabled = chkManterDescontoPerc.Checked = chkManterDescontoPerc.Enabled = txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = false;
            }
            else
            {
                if (ddlBolsaAlunoAlt.SelectedValue == "0")
                {
                    txtValorDescto.Enabled =
                    chkManterDescontoPerc.Enabled =
                    txtPeriodoIniDesconto.Enabled =
                    txtPeriodoFimDesconto.Enabled = true;

                    txtValorDescto.Text = "";
                    txtPeriodoIniDesconto.Text = "";
                    txtPeriodoFimDesconto.Text = "";
                    chkManterDescontoPerc.Checked = true;
                }
                else
                {
                    txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = true;
                    txtValorDescto.Enabled = chkManterDescontoPerc.Enabled = false;
                    int coBolsa = int.Parse(ddlBolsaAlunoAlt.SelectedValue);

                    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                                 where iTb148.CO_TIPO_BOLSA == coBolsa
                                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                    if (tb148 != null)
                    {
                        txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                        chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                        if (tb148.DT_INICI_TIPO_BOLSA != null)
                        {
                            txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                            txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtPeriodoIniDesconto.Text = "";
                            txtPeriodoFimDesconto.Text = "";
                        }
                    }
                }
            }
        }

        protected void ddlBolsaAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlBolsaAluno.SelectedValue == "")
            //{
            //    txtDescontoAluno.Text = txtPeriodoDeIniBolAluno.Text = txtPeriodoTerBolAluno.Text = "";
            //    txtDescontoAluno.Enabled = chkDesctoPercBolsa.Checked = chkDesctoPercBolsa.Enabled = txtPeriodoDeIniBolAluno.Enabled = txtPeriodoTerBolAluno.Enabled = false;
            //}
            //else
            //{
            //    txtDescontoAluno.Enabled = txtPeriodoDeIniBolAluno.Enabled = chkDesctoPercBolsa.Enabled = txtPeriodoTerBolAluno.Enabled = true;
            //    int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);

            //    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
            //                 where iTb148.CO_TIPO_BOLSA == coBolsa
            //                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

            //    if (tb148 != null)
            //    {
            //        txtDescontoAluno.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
            //        chkDesctoPercBolsa.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

            //        if (tb148.DT_INICI_TIPO_BOLSA != null)
            //        {
            //            txtPeriodoDeIniBolAluno.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
            //            txtPeriodoTerBolAluno.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
            //        }
            //        else
            //        {
            //            txtPeriodoDeIniBolAluno.Text = "";
            //            txtPeriodoTerBolAluno.Text = "";
            //        }
            //    }
            //}
        }

        protected void ddlTpBolsaAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");

            if (ddlTpBolsaAlt.SelectedValue == "")
            {
                txtValorDescto.Text = "";
                txtPeriodoFimDesconto.Text = "";
                txtPeriodoFimDesconto.Text = "";

                txtValorDescto.Enabled =
                txtPeriodoFimDesconto.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoFimDesconto.Enabled = false;

                CarregaBolsasAlt();
            }
            else
            {
                CarregaBolsasAlt();
            }
        }

        /// <summary>
        /// Método que carrega a grid de mensalidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMontaGridMensa_Click(object sender, EventArgs e)
        {
            ControlaTabs("MEN");
            ControlaChecks(chkMenEscAlu);

            int parcelas = 12; //- DateTime.Now.Month + 1;

            if (ddlSerieCurso.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque série não foi selecionada.");
                return;
            }

            if (!chkAtualiFinan.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não será atualizado no financeiro.");
                return;
            }

            if (chkDataPrimeiraParcela.Checked)
            {
                if (txtValorPrimParce.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor da primeira parcela deve ser informado.");
                    return;
                }
                else
                {
                    if (Decimal.Parse(txtValorPrimParce.Text) == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor 1ª Parcela não pode ser zero ou negativo.");
                        return;
                    }
                }
            }

            if (chkAlterValorContr.Checked)
            {
                if (txtValorContratoCalc.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor de Contrato deve ser informado.");
                    return;
                }
                else
                {
                    if (Decimal.Parse(txtValorContratoCalc.Text) == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor Editado de Contrato não pode ser zero ou negativo.");
                        return;
                    }
                }
            }

            if (chkManterDesconto.Checked)
            {
                if (txtPeriodoIniDesconto.Text != "" && txtPeriodoFimDesconto.Text != "")
                {
                    DateTime validaData;
                    if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out validaData) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out validaData))
                    {
                        if (DateTime.Parse(txtPeriodoIniDesconto.Text) > DateTime.Parse(txtPeriodoFimDesconto.Text))
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Data de fim de período inválida");
                            return;
                        }
                    }
                }
            }

            if (txtDesctoMensa.Text != "")
            {
                if (Decimal.Parse(txtDesctoMensa.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor de Desconto Especial não pode ser zero ou negativo.");
                    return;
                }
            }

            if ((ddlTipoDesctoMensa.SelectedValue == "M") && (txtDesctoMensa.Text != ""))
            {
                if (txtQtdeMesesDesctoMensa.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Quantidade de meses de desconto especial deve ser informada.");
                    return;
                }

                if (txtMesIniDesconto.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Número da Parcela de início de desconto deve ser informada.");
                    return;
                }

                if (int.Parse(txtQtdeMesesDesctoMensa.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque mês informado igual a zero.");
                    return;
                }

                if (int.Parse(txtMesIniDesconto.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque número da parcela de início informado igual a zero.");
                    return;
                }
            }

            MontaGridNegociacao();
        }

        protected void lnkCarteira_Click(object sender, EventArgs e)
        {
            int lRetorno, strSerie;

            int coEmp, coModu, coCur, coTur, coAlu = 0;
            string infos, parametros, coAno, deModu, noEmp, noCur, noTur, noAlu, codAlu, dtValidade = "";

            coAno = DateTime.Now.Year.ToString(); //ddlAno.SelectedValue;
            coEmp = LoginAuxili.CO_EMP;
            coModu = int.Parse(ddlModalidade.SelectedValue);
            coCur = int.Parse(ddlSerieCurso.SelectedValue);
            coTur = int.Parse(ddlTurma.SelectedValue);
            codAlu = Session[Resources.SessoesHttp.CodigoMatriculaAluno].ToString();
            dtValidade = "30/03/" + (DateTime.Now.Year + 1);

            coAlu = TB08_MATRCUR.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == codAlu).FirstOrDefault().CO_ALU;

            noEmp = (coEmp != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).NO_FANTAS_EMP : "TODOS");
            deModu = (coModu != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(coModu).CO_SIGLA_MODU_CUR : "TODOS");
            noCur = (coCur != 0 && coModu != 0 && coEmp != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coModu, coCur).CO_SIGL_CUR : "TODOS");
            noTur = (coTur != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur).CO_SIGLA_TURMA : "TODOS");
            noAlu = (coAlu != 0 && coEmp != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).NO_ALU : "TODOS");

            int strNIRE;
            strSerie = int.Parse(ddlSerieCurso.SelectedValue);
            strNIRE = txtNireAluno.Text != "" ? int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", "")) : 0;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Ano: " + coAno + " - Unidade: " + noEmp + " - Modalidade: " + deModu + " - Série/Curso: " + noCur + " - Turma: " + noTur + " - Aluno: " + noAlu + ")";

            //RptAlunoCarteira fpcb = new RptAlunoCarteira();
            RptCarteiraEstudantil fpcb = new RptCarteiraEstudantil();
            //lRetorno = fpcb.InitReport(strSerie, strNIRE);
            lRetorno = fpcb.InitReport(infos, parametros, coAno, coEmp, coEmp, coModu, coCur, coTur, coAlu, dtValidade);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            HttpContext.Current.Session["ApresentaRelatorio"] = 1;

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void lnkRecibo_OnClick(object sender, EventArgs e)
        {
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            string coalucad = (string)this.Session[SessoesHttp.CodigoMatriculaAluno];

            RptReciboPagamento fpcb = new RptReciboPagamento();
            lRetorno = fpcb.InitReport(coEmp, coalucad);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        protected void ddlTipoBolsa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBolsas();
        }

        protected void txtQtdeSolic_TextChanged(object sender, EventArgs e)
        {
            CalculaValorTotal();
        }

        protected void ddlTipoContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaA, ddlGrupoContaA);
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlContaContabilA);
        }

        protected void ddlSubGrupoContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlContaContabilA);
        }

        protected void ddlGrupoContaA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaA, ddlSubGrupoContaA);
            CarregaContasContabeis(ddlGrupoContaA, ddlSubGrupoContaA, ddlContaContabilA);
        }

        protected void ddlTipoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaB, ddlGrupoContaB);
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlContaContabilB);
        }

        protected void ddlGrupoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaB, ddlSubGrupoContaB);
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlContaContabilB);
        }

        protected void ddlSubGrupoContaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaB, ddlSubGrupoContaB, ddlContaContabilB);
        }

        protected void ddlTipoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoContasContabeis(ddlTipoContaC, ddlGrupoContaC);
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlContaContabilC);
        }

        protected void ddlGrupoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo(ddlGrupoContaC, ddlSubGrupoContaC);
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlContaContabilC);
        }

        protected void ddlSubGrupoContaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContasContabeis(ddlGrupoContaC, ddlSubGrupoContaC, ddlContaContabilC);
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCamposGride();
        }

        protected void ckbAtualizaFinancSolic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAtualiFinan.Checked)
            {
                chkConsolValorTitul.Enabled = ddlBoletoSolic.Enabled = txtDtVectoSolic.Enabled = true;
            }
            else
            {
                chkConsolValorTitul.Enabled = ddlBoletoSolic.Enabled = txtDtVectoSolic.Enabled = false;
                ddlBoletoSolic.SelectedValue = txtDtVectoSolic.Text = "";
            }
        }

        protected void chkConsolValorTitul_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConsolValorTitul.Checked)
            {
                ddlHistorico.Enabled = ddlAgrupador.Enabled =
                ddlGrupoContaA.Enabled = ddlSubGrupoContaA.Enabled = ddlContaContabilA.Enabled = ddlTipoContaA.Enabled =
                ddlGrupoContaB.Enabled = ddlSubGrupoContaB.Enabled = ddlContaContabilB.Enabled = ddlTipoContaB.Enabled =
                ddlGrupoContaC.Enabled = ddlSubGrupoContaC.Enabled = ddlContaContabilC.Enabled = ddlTipoContaC.Enabled = true;
            }
            else
            {
                ddlHistorico.Enabled = ddlAgrupador.Enabled =
                ddlGrupoContaA.Enabled = ddlSubGrupoContaA.Enabled = ddlContaContabilA.Enabled = ddlTipoContaA.Enabled =
                ddlGrupoContaB.Enabled = ddlSubGrupoContaB.Enabled = ddlContaContabilB.Enabled = ddlTipoContaB.Enabled =
                ddlGrupoContaC.Enabled = ddlSubGrupoContaC.Enabled = ddlContaContabilC.Enabled = ddlTipoContaC.Enabled = false;
                ddlHistorico.SelectedValue = ddlAgrupador.SelectedValue =
                ddlGrupoContaA.SelectedValue = ddlSubGrupoContaA.SelectedValue = ddlContaContabilA.SelectedValue =
                ddlGrupoContaB.SelectedValue = ddlSubGrupoContaB.SelectedValue = ddlContaContabilB.SelectedValue =
                ddlGrupoContaC.SelectedValue = ddlSubGrupoContaC.SelectedValue = ddlContaContabilC.SelectedValue = "";
            }
        }

        protected void chkAlterValorContr_CheckedChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");

            if (chkAlterValorContr.Checked)
            {
                txtValorContratoCalc.Enabled = true;
            }
            else
            {
                txtValorContratoCalc.Enabled = false;
                txtValorContratoCalc.Text = "";

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                switch (turnoTurma)
                {
                    case "M":
                        //--------> Turma Matutina
                        #region Turno Matutino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTMAN_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "V":
                        //--------> Turma Vespertina
                        #region Turno Vespertino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "N":
                        //--------> Turma Noturna
                        #region Turno Noturno
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                }
            }
        }

        //--------------------------------------------REGRAS ABA FORMA DE PAGAMENTO------------------------------------------------

        #region Funções de Campo na Aba Forma de Pagamento

        #region Cartão de Débito na Aba Forma de Pagamento

        //Habilitado os DropDownList do Banco do Cartão, quando for clicado no checkbox
        protected void chkDebitPgto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDebitPgto.Checked)
            {
                ddlBcoPgto1.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBcoPgto1.SelectedValue != "N")
                {
                    txtAgenPgto1.Enabled =
                    txtNContPgto1.Enabled =
                    txtNuDebtPgto1.Enabled =
                    txtNuTitulDebitPgto1.Enabled =
                    txtValDebitPgto1.Enabled = true;
                }

                ddlBcoPgto2.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBcoPgto2.SelectedValue != "N")
                {
                    txtAgenPgto2.Enabled =
                    txtNContPgto2.Enabled =
                    txtNuDebtPgto2.Enabled =
                    txtNuTitulDebitPgto2.Enabled =
                    txtValDebitPgto2.Enabled = true;
                }

                ddlBcoPgto3.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBcoPgto3.SelectedValue != "N")
                {
                    txtAgenPgto3.Enabled =
                    txtNContPgto3.Enabled =
                    txtNuDebtPgto3.Enabled =
                    txtNuTitulDebitPgto3.Enabled =
                    txtValDebitPgto3.Enabled = true;
                }
            }
            else
            {
                ddlBcoPgto1.Enabled =
                ddlBcoPgto2.Enabled =
                ddlBcoPgto3.Enabled = false;

                //Desabilita a primeira linha
                txtAgenPgto1.Enabled =
                txtNContPgto1.Enabled =
                txtNuDebtPgto1.Enabled =
                txtNuTitulDebitPgto1.Enabled =
                txtValDebitPgto1.Enabled = false;

                //Desabilita a segunda linha
                txtAgenPgto2.Enabled =
                txtNContPgto2.Enabled =
                txtNuDebtPgto2.Enabled =
                txtNuTitulDebitPgto2.Enabled =
                txtValDebitPgto2.Enabled = false;

                //Desabilita a Terceira linha
                txtAgenPgto3.Enabled =
                txtNContPgto3.Enabled =
                txtNuDebtPgto3.Enabled =
                txtNuTitulDebitPgto3.Enabled =
                txtValDebitPgto3.Enabled = false;
            }
        }

        #endregion

        #region Cartão de Crédito na Aba Forma de Pagamento

        #endregion

        #endregion

        //--------------------------------------------FIM REGRAS ABA FORMA DE PAGAMENTO------------------------------------------------

        //_____________________________________TESTE IMPORTANTE--------------------------------------------------

        //_____________________________________TESTE IMPORTANTE--------------------------------------------------

        protected void chkTipoContrato_CheckedChange(object sender, EventArgs e)
        {
            ControlaTabs("MEN");
            ControlaChecks(chkMenEscAlu);

            if (chkTipoContrato.Checked)
            {
                ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled = true;
            }
            else
            {
                ddlTipoContrato.SelectedValue = "P";
                ddlTipoValorCurso.SelectedValue = tipoValor[tipoValorCurso.P];

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
                txtQtdeParcelas.Text = varSer.NU_QUANT_MESES.ToString();
                string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                    ddlTipoContrato.SelectedValue,
                    turnoTurma,
                    varSer,
                    this.Page, false);
                if (retornoValor == string.Empty)
                    return;
                else
                    txtValorContratoCalc.Text = retornoValor;

                ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled = false;
            }
        }

        protected void ddlTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");
            ControlaChecks(chkMenEscAlu);
            ///Desabilita a funcionalidade de primeir parcela para contrato A Vista
            if (ddlTipoContrato.SelectedValue == tipoContrato[tipoContratoCurso.V])
            {
                //txtValorPrimParce.Text = "";
                txtValorPrimParce.Text = txtValorContratoCalc.Text;
                txtQtdeParcelas.Text = "1";
                chkGeraTotalParce.Enabled = false;
                txtValorPrimParce.Enabled = chkGeraTotalParce.Checked = txtQtdeParcelas.Enabled = false;
                txtDtPrimeiraParcela.Enabled = true;
            }
            else
            {
                chkDataPrimeiraParcela.Enabled = true;
                //txtValorPrimParce.Text = "";
                int aux1 = 12 - DateTime.Now.Month;
                int aux2 = aux1 + 1;

                //Verifica qual o tipo de parcelas está selecionado e preenche a qtdd de parcelas de acordo com tal
                if (ddlValorContratoCalc.SelectedValue == "P")
                    txtQtdeParcelas.Text = aux2.ToString();
                else
                    txtQtdeParcelas.Text = TB01_CURSO.RetornaPeloCoCur(int.Parse(this.ddlSerieCurso.SelectedValue)).NU_QUANT_MESES.ToString();

                CalculaValorPrimeiraParcela();

                chkGeraTotalParce.Enabled = true;
            }
            ///Pega o Curso
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                ddlTipoContrato.SelectedValue,
                turnoTurma,
                varSer,
                this.Page, false);
            if (retornoValor == string.Empty)
                return;
            else
                txtValorContratoCalc.Text = retornoValor;
        }

        protected void txtQtdeParcelas_TextChanged(object sender, EventArgs e)
        {
            //-----------> Pega o Curso
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);

            if (ddlTipoContrato.SelectedValue == "P")
            {
                if (txtQtdeParcelas.Text != "")
                {
                    if (int.Parse(txtQtdeParcelas.Text) <= 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas a prazo deve ser maior que 1.");
                        return;
                    }

                    if (int.Parse(txtQtdeParcelas.Text) > varSer.NU_QUANT_MESES)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas a prazo não pode ser maior que quantidade de parcelas do contrato.");
                        return;
                    }
                }
            }

            int aux1 = 12 - DateTime.Now.Month;
            int aux2 = aux1 + 1;
            if (aux2.ToString() != txtQtdeParcelas.Text)
                ddlValorContratoCalc.SelectedValue = "T";
            else
                ddlValorContratoCalc.SelectedValue = "P";

            CalculaValorPrimeiraParcela();
        }

        protected void ck2Select_CheckedChanged(object sender, EventArgs e)
        {
            //bool chkContrato = false;
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            var txMat = ((Label)row.Cells[6].FindControl("txMatr")).Text;
            if (txMat == "S")
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    if (grdSolicitacoes.Rows.Count > 0)
                    {
                        if (((Label)linha.Cells[6].FindControl("txMatr")).Text == "S")
                        {
                            ((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked = false;

                        }

                    }
                }
                ((CheckBox)row.Cells[0].FindControl("chkSelect")).Checked = true;
            }

            HabilitaCamposGride();
        }

        protected void txtAno_TextChanged(object sender, EventArgs e)
        {
            ControlaTabs("GRM");
            TextBox ano = (TextBox)sender;
            if (ano.Text != "" && ano.Text.Length == 4)
            {
                int anoD = 0;
                if (int.TryParse(ano.Text, out anoD))
                {
                    if (anoD > (DateTime.Now.Year + 1))
                        ano.Text = PreAuxili.anoPreMatricula().ToString();
                }
                else
                    ano.Text = PreAuxili.anoPreMatricula().ToString();
            }
            else
            {
                ano.Text = PreAuxili.anoPreMatricula().ToString();
            }
        }

        protected void ddlTipoValorCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("MEN");
            ControlaChecks(chkMenEscAlu);
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int unidade = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP);
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(unidade, modalidade, serie);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            string retornoValor = PreAuxili.valorContratoCurso(
                ((DropDownList)sender).SelectedValue,
                ddlTipoContrato.SelectedValue,
                turnoTurma,
                varSer,
                this.Page, false);
            if (retornoValor == string.Empty)
                return;
            else
                txtValorContratoCalc.Text = retornoValor;
        }

        protected void ddlCurExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            MontaGridCursoExtra();
        }

        protected void ckSelAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelTodos = (CheckBox)sender;

            if (chkSelTodos.Checked)
            {
                selecionaTodos(true);
            }
            else
            {
                selecionaTodos(false);
            }
        }

        protected void selecionaTodos(bool st)
        {
            foreach (GridViewRow linha in grdGrdCurso.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Enabled != false)
                {
                    ((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked = st;
                }
            }
        }
        #endregion
    }

}