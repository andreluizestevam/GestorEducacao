//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE INFORMAÇÕES DE UNIDADES DE ENSINO E DE APOIO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 das contas contábeis
//           |                            |
// ----------+----------------------------+-------------------------------------
// 18/06/2013| André Nobre Vinagre        | Adicionada as datas dos bimestres das avaliações
//           |                            |
// ----------+----------------------------+-------------------------------------
// 30/07/2013| André Nobre Vinagre        | Corrigido o carregamento da imagem da unidade
//           |                            |
// ----------+----------------------------+-------------------------------------
// 10/02/2014| Vinícius Reis              | Removido os textbox de grade horária e incluída uma grid
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Text;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfUnidEnsinoApoio
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variáveis

        public string sTipoCtrleDescricao;
        public int globalCoEmp;

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
            TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            sTipoCtrleDescricao = tb149.TP_CTRLE_DESCR;

            if (IsPostBack) return;
            
            VerificaTipoUnidadeLogada();

            globalCoEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            CarregaGridImposto();
            imgUnidadeIUE.ImagemLargura = 130;

            if (tb149.TP_CTRLE_AVAL == TipoControle.I.ToString())
                txtContrAval.Text = "Instituição de Ensino";
            else if (tb149.TP_CTRLE_AVAL == TipoControle.M.ToString())
                txtContrAval.Text = "Modalidade de Ensino";
            else
                txtContrAval.Text = "Unidade Escolar";

            if (tb149.TP_CTRLE_METOD == TipoControle.U.ToString())
                txtContrMetod.Text = "Unidade Escolar";
            else
                txtContrMetod.Text = "Instituição de Ensino";

            if (tb149.TP_CTRLE_DESCR == TipoControle.I.ToString())
            {
                txtTipoCtrlQuemSomos.Text = txtTipoCtrlNossaHisto.Text = txtTipoCtrlPropoPedag.Text = "Instituição de Ensino";
                txtQuemSomos.Html = tb149.DES_QUEM_SOMOS != null ? tb149.DES_QUEM_SOMOS : "";
                txtNossaHisto.Html = tb149.DES_NOSSA_HISTO != null ? tb149.DES_NOSSA_HISTO : "";
                txtPropoPedag.Html = tb149.DES_PROPO_PEDAG != null ? tb149.DES_PROPO_PEDAG : "";

                txtQuemSomos.Enabled = txtNossaHisto.Enabled = txtPropoPedag.Enabled = false;
            }
            else
            {
                txtTipoCtrlQuemSomos.Text = txtTipoCtrlNossaHisto.Text = txtTipoCtrlPropoPedag.Text = "Unidade Escolar";
            }

            chkEnvioSMS.Checked = chkEnvioSMS.Enabled = tb149.FL_ENVIO_SMS == "S";

//--------> Verifica se controle de Datas é Institucional ou da Unidade e carrega de acordo com essa informação
            if (tb149.FLA_CTRL_DATA == TipoControle.I.ToString())
            {
                txtTipoContrDatas.Text = "Instituição de Ensino";
                CarregaDadosControleData(tb149);
            }
            else if (tb149.FLA_CTRL_DATA == TipoControle.U.ToString())
                txtTipoContrDatas.Text = "Unidade Escolar";
            
//--------> Faz a verificação para saber se o controle de metodologia é pela instituição
            if (tb149.TP_CTRLE_METOD == TipoControle.I.ToString())
            {
                ddlMetodEnsino.SelectedValue = tb149.TP_ENSINO;
                ddlFormaAvali.SelectedValue = tb149.TP_FORMA_AVAL;
                ddlMetodEnsino.Enabled = ddlFormaAvali.Enabled = false;

                if (ddlFormaAvali.SelectedValue == "C")
                {
//----------------> Carrega Conceitos de acordo com a Instituição informada
                    var tb200 = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                 where iTb200.ORG_CODIGO_ORGAO == tb149.ORG_CODIGO_ORGAO && iTb200.TB25_EMPRESA == null
                                 select iTb200).OrderByDescending(eq => eq.VL_NOTA_MIN);

                    int i = 1;

                    if (tb200 != null)
                    {
                        foreach (var iTb200 in tb200)
                        {
                            if (i == 1)
                            {
                                txtDescrConce1.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce1.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni1.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim1.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 2)
                            {
                                txtDescrConce2.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce2.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni2.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim2.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 3)
                            {
                                txtDescrConce3.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce3.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni3.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim3.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 4)
                            {
                                txtDescrConce4.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce4.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni4.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim4.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 5)
                            {
                                txtDescrConce5.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce5.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni5.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim5.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            i++;
                        }
                    }
                }                
            }

//--------> Habilita/Desabilita os campos de Controle de Avaliaçao de acordo com o Tipo de Controle
            if (tb149.TP_CTRLE_AVAL == TipoControle.M.ToString())
            {
                HabilitaControleAvaliacao(false);
            }
            else if (tb149.TP_CTRLE_AVAL == TipoControle.I.ToString())
            {
                HabilitaControleAvaliacao(false);

                txtMediaAprovGeral.Text = tb149.VL_MEDIA_CURSO != null ? tb149.VL_MEDIA_CURSO.Value.ToString() : "";
                txtMediaAprovDireta.Text = tb149.VL_MEDIA_APROV_DIRETA != null ? tb149.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                txtMediaProvaFinal.Text = tb149.VL_MEDIA_PROVA_FINAL != null ? tb149.VL_MEDIA_PROVA_FINAL.Value.ToString() : "";

                ddlPerioAval.SelectedValue = tb149.TP_PERIOD_AVAL;
            }
            else if (tb149.TP_CTRLE_AVAL == TipoControle.U.ToString())
                HabilitaControleAvaliacao(true);

            rblInforCadastro.SelectedValue = "1";
            tabDadosCadas.Style.Add("display", "block");

            if (tb149.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString())
                txtTipoContrOpera.Text = "Unidade Escolar";
            else
                txtTipoContrOpera.Text = "Instituição de Ensino";

            txtInstituicaoPrinc.Text = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NOME_ORGAO;

            CarregaTipoEnsino();

            CarregaUFs();
            CarregaCidades();
            CarregaBairros();

            CarregaNucleoUnidade();
            CarregaCentroCusto();

            CarregaTipo(ddlGrupoTxServSecre);
            CarregaSubGrupo(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre);
            CarregaSubGrupo2(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre);
            CarregaConta(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre, ddlContaContabilTxServSecre);

            CarregaTipo(ddlGrupoTxServBibli);
            CarregaSubGrupo(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli);
            CarregaSubGrupo2(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli);
            CarregaConta(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli, ddlContaContabilTxServBibli);

            CarregaTipo(ddlGrupoTxMatri);
            CarregaSubGrupo(ddlGrupoTxMatri, ddlSubGrupoTxMatri);
            CarregaSubGrupo2(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri);
            CarregaConta(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri, ddlContaContabilTxMatri);

            CarregaTipo(ddlGrupoAtiviExtra);
            CarregaSubGrupo(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra);
            CarregaSubGrupo2(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra);
            CarregaConta(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra, ddlContaContabilAtiviExtra);

            CarregaTipo(ddlGrupoContaBanco);
            CarregaSubGrupo(ddlGrupoContaBanco, ddlSubGrupoContaBanco);
            CarregaSubGrupo2(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco);
            CarregaConta(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco, ddlContaContabilContaBanco);

            CarregaTipo(ddlGrupoContaCaixa);
            CarregaSubGrupo(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa);
            CarregaSubGrupo2(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa);
            CarregaConta(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa, ddlContaContabilContaCaixa);


            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol1);
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol1,ddlNomeSecreEscol1);
            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol2);
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol2, ddlNomeSecreEscol2);
            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol3);
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol3, ddlNomeSecreEscol3);
            CarregaUnidadeNomeBibliEscol(ddlSiglaUnidBibliEscol1);
            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol1, ddlNomeBibliEscol1);
            CarregaUnidadeNomeBibliEscol(ddlSiglaUnidBibliEscol2);
            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol2, ddlNomeBibliEscol2);
            CarregaBoletos();
            CarregaFuncionariosDirecao();
            CarregaFuncionariosCoordenacao();
            CarregaAgrupadoCAR(ddlAgrupCAR);

            CarregaBoletins();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string now = DateTime.Now.ToString("dd/MM/yyyy");
                txtDtCadastroIUE.Text = txtDtStatusIUE.Text = now;                
                txtTipoCtrlFrequ.Text = "Instituição de Ensino";
                ddlPermiMultiFrequ.Enabled = false;

//------------> Carrega informações do Quadro de Frequência Funcional de acordo com a Instituição informada
                var tb300 = TB300_QUADRO_HORAR_FUNCI.RetornaPelaInstituicao(LoginAuxili.ORG_CODIGO_ORGAO);
                int indice = 0;
                if (tb300 != null)
                {
                    var result = (from res in tb300
                                  select new
                                  {
                                      res.ID_QUADRO_HORAR_FUNCI,
                                      res.CO_SIGLA_TIPO_PONTO,
                                      LIMIT_ENTRA = res.HR_LIMIT_ENTRA != null ? res.HR_LIMIT_ENTRA.Insert(2, ":") : null,
                                      ENTRA_TURNO1 = res.HR_ENTRA_TURNO1 != null ? res.HR_ENTRA_TURNO1.Insert(2, ":") : null,
                                      SAIDA_TURNO1 = res.HR_SAIDA_TURNO1 != null ? res.HR_SAIDA_TURNO1.Insert(2, ":") : null,
                                      ENTRA_INTER = res.HR_ENTRA_INTER != null ? res.HR_ENTRA_INTER.Insert(2, ":") : null,
                                      SAIDA_INTER = res.HR_SAIDA_INTER != null ? res.HR_SAIDA_INTER.Insert(2, ":") : null,
                                      ENTRA_TURNO2 = res.HR_ENTRA_TURNO2 != null ? res.HR_ENTRA_TURNO2.Insert(2, ":") : null,
                                      SAIDA_TURNO2 = res.HR_SAIDA_TURNO2 != null ? res.HR_SAIDA_TURNO2.Insert(2, ":") : null,
                                      LIMIT_SAIDA = res.HR_LIMIT_SAIDA != null ? res.HR_LIMIT_SAIDA.Insert(2, ":") : null,
                                      ENTRA_EXTRA = res.HR_ENTRA_EXTRA != null ? res.HR_ENTRA_EXTRA.Insert(2, ":") : null,
                                      SAIDA_EXTRA = res.HR_SAIDA_EXTRA != null ? res.HR_SAIDA_EXTRA.Insert(2, ":") : null,
                                      LIMIT_SAIDA_EXTRA = res.HR_LIMIT_SAIDA_EXTRA != null ? res.HR_LIMIT_SAIDA_EXTRA.Insert(2, ":") : null

                                  }).OrderBy(o => o.CO_SIGLA_TIPO_PONTO);

                    grdHorarios.DataSource = result.ToList();
                    grdHorarios.DataBind();

                    
                }
                else
                    ddlPermiMultiFrequ.Enabled = true;

                if (tb149.TP_CTRLE_SECRE_ESCOL == TipoControle.I.ToString())
                {
                    txtTipoCtrlSecreEscol.Text = "Instituição de Ensino";
                    CarregaDadosSecretariaEscolar(tb149);
                }
                else
                    txtTipoCtrlSecreEscol.Text = "Unidade Escolar";

                if (tb149.TP_CTRLE_BIBLI == TipoControle.I.ToString())
                {
                    txtTipoCtrlBibliEscol.Text = "Instituição de Ensino";
                    CarregaDadosBibliotecaEscolar(tb149);
                }
                else
                    txtTipoCtrlBibliEscol.Text = "Unidade Escolar";

                if (tb149.TP_CTRLE_MENSA_SMS == TipoControle.I.ToString())
                {
                    txtTipoCtrlMensagSMS.Text = "Instituição de Ensino";
                    CarregaDadosMensagensSMS(tb149);
                }
                else
                    txtTipoCtrlMensagSMS.Text = "Unidade Escolar";

                if (tb149.TP_CTRLE_CTA_CONTAB == TipoControle.I.ToString())
                {
                    txtTipoCtrlContabil.Text = "Instituição de Ensino";
                    CarregaDadosContaContabil(tb149);
                }
                else
                    txtTipoCtrlContabil.Text = "Unidade Escolar";
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                decimal retornaDecimal = 0;
                int ctNumGeral = 0;
                int qtdeDiasAnterior = 0;
                int qtdeDiasPosterior = 0;

                if (!(cblTipoEnsino.Items[0].Selected 
                    || cblTipoEnsino.Items[1].Selected 
                    || cblTipoEnsino.Items[2].Selected 
                    || cblTipoEnsino.Items[3].Selected 
                    || cblTipoEnsino.Items[4].Selected
                    || cblTipoEnsino.Items[5].Selected))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Tipo de Ensino deve ser informado.");
                    return;
                }

                ctNumGeral = ddlDirGeralDir1.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;
                ctNumGeral = ddlDirGeralDir2.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;
                ctNumGeral = ddlDirGeralDir3.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;
                ctNumGeral = ddlDirGeralDir4.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;

                if (ctNumGeral > 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Permitido apenas um(a) diretor(a) geral.");
                    return;
                }

                ctNumGeral = 0;

                ctNumGeral = ddlDirGeralCoord1.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;
                ctNumGeral = ddlDirGeralCoord2.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;
                ctNumGeral = ddlDirGeralCoord3.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;
                ctNumGeral = ddlDirGeralCoord4.SelectedValue == "S" ? ctNumGeral + 1 : ctNumGeral;

                if (ctNumGeral > 1)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Permitido apenas um(a) coordenador(a) geral.");
                    return;
                }

                if (txtVlJurosSecre.Text != "" ? !decimal.TryParse(txtVlJurosSecre.Text, out retornaDecimal) : false)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Juros diário da secretaria incorreto.");
                    return;
                }

                if (txtJurosDiario.Text != "" ? !decimal.TryParse(txtJurosDiario.Text, out retornaDecimal) : false)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Juros diário informado incorreto.");
                    return;
                }

                int idFotoUnidade = imgUnidadeIUE.GravaImagem();

                TB25_EMPRESA tb25 = RetornaEntidade();

                //Apenas verifica se existe unidade matriz se estiver marcado
                if (chkUnidadeMatriz.Checked)
                {
                    // Faz a verificação para saber se  existe  matriz Cadastrado
                    var ExisteMatriz = TB25_EMPRESA.RetornaTodosRegistros().Where(a => a.FL_UNID_MATRIZ == "S" && a.CO_EMP != tb25.CO_EMP).Any();

                    if (ExisteMatriz)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Ja existe uma matriz parametrizada no sistema");
                        return;
                    }
                }
//------------> Carrega informações da instituição, para verificar se o controle é pelo mesmo ou pela Unidade
                TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (tb25 == null)
                {                    
//----------------> Faz a verificação para saber se o controle da Metodologia é pela Unidade
                    if ((tb149.TP_CTRLE_METOD == TipoControle.U.ToString()) && (ddlMetodEnsino.SelectedValue == "" || ddlFormaAvali.SelectedValue == ""))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Parâmetros de Metodologia devem ser informados");
                        return;
                    }

                    tb25 = new TB25_EMPRESA();
                    tb25.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                }
                else
                {
                    tb25.TB82_DTCT_EMPReference.Load();
                    tb25.TB83_PARAMETROReference.Load();
                }

//------------> Se inclusão, define a FL_INCLU_EMP(flag de inclusão) = true e FL_ALTER_EMP(flag de alteração) = false
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb25.FL_INCLU_EMP = true;
                    tb25.FL_ALTER_EMP = false;
                }

//------------> Se alteração, define a FL_ALTER_EMP(flag de alteração) = true
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    tb25.FL_ALTER_EMP = true;
                }

                if (tb25.TB82_DTCT_EMP == null)
                    tb25.TB82_DTCT_EMP = new TB82_DTCT_EMP();

                if (tb25.TB83_PARAMETRO == null)
                    tb25.TB83_PARAMETRO = new TB83_PARAMETRO();

                int retornaInt = 0;                
                DateTime retornaData = new DateTime();
               
                //Validação de datas dos bimestres
                if (txtPeriodoIniBim1.Text != "")
                {
                    if (DateTime.Parse(txtPeriodoIniBim1.Text) > DateTime.Parse(txtPeriodoFimBim1.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Período do 1º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }
                if (txtLactoIniBim1.Text != "")
                {
                    if (DateTime.Parse(txtLactoIniBim1.Text) > DateTime.Parse(txtLactoFimBim1.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Lançamento do 1º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }

                if (txtPeriodoIniBim2.Text != "")
                {
                    if (DateTime.Parse(txtPeriodoIniBim2.Text) > DateTime.Parse(txtPeriodoFimBim2.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Período do 2º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }
                if (txtLactoIniBim2.Text != "")
                {
                    if (DateTime.Parse(txtLactoIniBim2.Text) > DateTime.Parse(txtLactoFimBim2.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Lançamento do 2º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }

                if (txtPeriodoIniBim3.Text != "")
                {
                    if (DateTime.Parse(txtPeriodoIniBim3.Text) > DateTime.Parse(txtPeriodoFimBim3.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Período do 3º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }
                if (txtLactoIniBim3.Text != "")
                {
                    if (DateTime.Parse(txtLactoIniBim3.Text) > DateTime.Parse(txtLactoFimBim3.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Lançamento do 3º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }

                if (txtPeriodoIniBim4.Text != "")
                {
                    if (DateTime.Parse(txtPeriodoIniBim4.Text) > DateTime.Parse(txtPeriodoFimBim4.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Período do 4º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }
                if (txtLactoIniBim4.Text != "")
                {
                    if (DateTime.Parse(txtLactoIniBim4.Text) > DateTime.Parse(txtLactoFimBim4.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início do Lançamento do 4º Bimestre não pode ser superior a Data Final.");
                        return;
                    }
                }



                int.TryParse(txtQtdeDiasAnterior.Text, out qtdeDiasAnterior);
                int.TryParse(txtQtdeDiasPosterior.Text, out qtdeDiasPosterior);

                if (qtdeDiasAnterior > 0 && qtdeDiasAnterior > 120)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "O campo QDA não pode possuir um valor maior que 120.");
                    return;
                }

                if (qtdeDiasPosterior > 0 && qtdeDiasPosterior > 120)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "O campo QDP não pode possuir um valor maior que 120.");
                    return;
                }

                //********************

                tb25.NO_EMAIL = txtEmail.Text != "" ? txtEmail.Text : null;
                tb25.SE_EMAIL = txtSenha.Text != "" ? txtSenha.Text : null;
                tb25.HR_LIMITE_MANHA = "";
                tb25.HR_LIMITE_NOITE = "";
                tb25.HR_LIMITE_TARDE = "";
                tb25.FL_UNID_MATRIZ = (chkUnidadeMatriz.Checked == true ? "S" : "N");
//------------> Informaçoes da Unidade Escolar
                tb25.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(idFotoUnidade);
                tb25.NO_FANTAS_EMP = txtNomeIUE.Text;
                tb25.sigla = txtSiglaIUE.Text;
                tb25.CO_CPFCGC_EMP = txtCNPJIUE.Text.Replace("-", "").Replace(".", "").Replace("/", "");
                tb25.NO_RAZSOC_EMP = txtRazaoSocialIUE.Text;
                tb25.CO_NUCLEO = int.TryParse(ddlNucleoIUE.SelectedValue, out retornaInt) ? (int?)retornaInt : null;
                tb25.FLA_UNID_GESTORA = "N";
                tb25.CO_FLAG_ENSIN_CURSO = cblTipoEnsino.Items[0].Selected ? "S" : "N";
                tb25.CO_FLAG_ENSIN_FUNDA = cblTipoEnsino.Items[1].Selected ? "S" : "N";
                tb25.CO_FLAG_ENSIN_INFAN = cblTipoEnsino.Items[2].Selected ? "S" : "N";
                tb25.CO_FLAG_ENSIN_MEDIO = cblTipoEnsino.Items[3].Selected ? "S" : "N";
                tb25.CO_FLAG_ENSIN_SUPER = cblTipoEnsino.Items[4].Selected ? "S" : "N";
                tb25.CO_FLAG_ENSIN_OUTRO = cblTipoEnsino.Items[5].Selected ? "S" : "N";
                tb25.TB24_TPEMPRESA = TB24_TPEMPRESA.RetornaPelaChavePrimaria(1);
                tb25.TB162_CLAS_INST = TB162_CLAS_INST.RetornaPelaChavePrimaria(1);
                tb25.CO_CEP_EMP = txtCEPIUE.Text.Replace("-", "");
                tb25.DE_END_EMP = txtLogradouroIUE.Text != "" ? txtLogradouroIUE.Text : null;
                tb25.NU_END_EMP = int.TryParse(txtNumeroLograIUE.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.DE_COM_ENDE_EMP = txtComplementoIUE.Text != "" ? txtComplementoIUE.Text : null;                
                tb25.CO_BAIRRO = int.Parse(ddlBairroIUE.SelectedValue);
                tb25.CO_CIDADE = int.Parse(ddlCidadeIUE.SelectedValue);
                tb25.CO_UF_EMP = ddlUFIUE.SelectedValue;
                tb25.CO_GEORE_LATIT_EMP = txtLatitude.Text != "" ? txtLatitude.Text : null;
                tb25.CO_GEORE_LONGI_EMP = txtLongitude.Text != "" ? txtLongitude.Text : null;
                tb25.NU_INEP = int.TryParse(txtINEPIUE.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.CO_INS_ESTA_EMP = txtInscEstadualIUE.Text != "" ? txtInscEstadualIUE.Text : null;
                tb25.CO_TEL1_EMP = txtTelefoneIUE.Text != "" ? txtTelefoneIUE.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb25.CO_TEL2_EMP = txtTelefoneIUE2.Text != "" ? txtTelefoneIUE2.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb25.NO_EMAIL_EMP = txtEmailIUE.Text != "" ? txtEmailIUE.Text : null;
                tb25.CO_FAX_EMP = txtFaxIUE.Text != "" ? txtFaxIUE.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb25.NO_WEB_EMP = txtWebSiteIUE.Text != "" ? txtWebSiteIUE.Text : null;
                tb25.CO_SIT_EMP = ddlStatusIUE.SelectedValue;
                tb25.DT_SIT_EMP = DateTime.Parse(txtDtStatusIUE.Text);
                tb25.CO_DOCTO_CONST = txtNumDocto.Text != "" ? txtNumDocto.Text : null;
                tb25.DT_CONST = txtDataConstituicao.Text != "" ? (DateTime?)DateTime.Parse(txtDataConstituicao.Text) : null;
                tb25.DT_CAD_EMP = DateTime.Parse(txtDtCadastroIUE.Text);                
                tb25.DE_OBS_EMP = txtObservacaoIUE.Text != "" ? txtObservacaoIUE.Text : null;
                tb25.DE_TITUL_BOLET = (!string.IsNullOrEmpty(txtNomAvalAluno.Text) ? txtNomAvalAluno.Text : null);

                tb25.DT_SALDO_INICIAL = txtDtSaldo.Text != "" ? (DateTime?)DateTime.Parse(txtDtSaldo.Text) : null;
                tb25.VL_SALDO_INICIAL = !string.IsNullOrEmpty(txtSaldo.Text) ? (decimal?)decimal.Parse(txtSaldo.Text) : null;

//------------> Informaçoes de Controle Operacional
                tb25.TP_HORA_FUNC = "MT";

                tb25.HR_FUNCI_MANHA_INIC = txtHoraIniTurno1.Text != "" ? txtHoraIniTurno1.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_MANHA_FIM = txtHoraFimTurno1.Text != "" ? txtHoraFimTurno1.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_TARDE_INIC = txtHoraIniTurno2.Text != "" ? txtHoraIniTurno2.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_TARDE_FIM = txtHoraFimTurno2.Text != "" ? txtHoraFimTurno2.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_NOITE_INIC = txtHoraIniTurno3.Text != "" ? txtHoraIniTurno3.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_NOITE_FIM = txtHoraFimTurno3.Text != "" ? txtHoraFimTurno3.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_ULTIM_TURNO_INIC = txtHoraIniTurno4.Text != "" ? txtHoraIniTurno4.Text.Replace(":", "") : null;
                tb25.HR_FUNCI_ULTIM_TURNO_FIM = txtHoraFimTurno4.Text != "" ? txtHoraFimTurno4.Text.Replace(":", "") : null;

//------------> Informações de Quem Somos; Nossa História; Proposta Pedagógica
                tb25.TP_CTRLE_DESCR = tb149.TP_CTRLE_DESCR == TipoControle.U.ToString() ? "U" : "I";
                tb25.DES_QUEM_SOMOS = txtQuemSomos.Html != "" ? txtQuemSomos.Html : null;
                tb25.DES_NOSSA_HISTO = txtNossaHisto.Html != "" ? txtNossaHisto.Html : null;
                tb25.DES_PROPO_PEDAG = txtPropoPedag.Html != "" ? txtPropoPedag.Html : null;

                tb25.HR_LIMITE_MANHA_INIC = null;
                tb25.HR_LIMITE_MANHA_FIM = null;
                tb25.HR_LIMITE_TARDE_INIC = null;
                tb25.HR_LIMITE_TARDE_FIM = null;
                tb25.HR_LIMITE_NOITE_INIC = null;
                tb25.HR_LIMITE_NOITE_FIM = null;

//------------> Informação de Multifrequência
                tb25.FL_NONO_DIGITO_TELEF = LoginAuxili.FL_NONO_DIGITO_TELEF = (chkNonoDigito.Checked ? "S" : "N");

//------------> Informação de Multifrequência
                if (ddlPermiMultiFrequ.Enabled)
                    tb25.TB83_PARAMETRO.FL_MULTI_FREQU = ddlPermiMultiFrequ.SelectedValue;
                else
                    tb25.TB83_PARAMETRO.FL_MULTI_FREQU = null;

//------------> Informações de Controle Gestores da Unidade
                tb25.TB83_PARAMETRO.NU_VALID_RETORNO = int.TryParse(txtValidRetorno.Text, out retornaInt) ? (int?)retornaInt : 0;
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_TRIAGEM = RadioButtonListAtendimento.SelectedValue;
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_AGEND = CheckBoxAgendamento.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_ENCAI = CheckBoxEncaixe.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_MOVIM = CheckBoxMovimentacao.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_GUIA = CheckBoxGuia.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_FICHA = CheckBoxFicha.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_ATEND = CheckBoxRecSimples.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_CONTR = CheckBoxRecContrato.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_CAIXA = CheckBoxRecCaixa.Checked ? "S" : "N";

                ////Agendamento
                //tb25.TB83_PARAMETRO.QT_DIAS_ANTERIOR = qtdeDiasAnterior;
                //tb25.TB83_PARAMETRO.QT_DIAS_POSTERIOR = qtdeDiasPosterior;

               //Atendimento 
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_PRONT_PAD = CheckBoxProntPadrao.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_PRONT_MOD = CheckBoxProntModular.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEN_MED = CheckBoxPresqMedicamentos.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_EXAM = CheckBoxPresqExames.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_AMB = CheckBoxPresqAmbu.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_GUIA = CheckBoxEmitirGuia.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_FIXA_ATEND = CheckBoxFichaAtend.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ATEST = CheckBoxEmitirAtestado.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ARQ = CheckBoxAnexarArq.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_OBSERV = CheckBoxObserv.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ENCAM = CheckBoFazerxEncaminha.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_INTERNACAO = CheckBoxRegInternacao.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_LAUDO_TEC = CheckBoxEmissaoAtestado.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_CIRURGIA = CheckBoxSolicCirurgia.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_SALVAR = CheckBoxSalvarAtend.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ESPERA = CheckBoxManterEspera.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_FINALIZAR = CheckBoxFinalizarAtend.Checked ? "S" : "N";


                tb25.TB83_PARAMETRO.CO_DIR1 = ddlFuncDir1.SelectedValue != "" ? (int?)int.Parse(ddlFuncDir1.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_DIR1 = txtTitulDir1.Text != "" ? txtTitulDir1.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR1 = ddlTipoEnsinoDir1.SelectedValue != "" ? ddlTipoEnsinoDir1.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR1 = ddlDirGeralDir1.SelectedValue != "" ? ddlDirGeralDir1.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_DIR2 = ddlFuncDir2.SelectedValue != "" ? (int?)int.Parse(ddlFuncDir2.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_DIR2 = txtTitulDir2.Text != "" ? txtTitulDir2.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR2 = ddlTipoEnsinoDir2.SelectedValue != "" ? ddlTipoEnsinoDir2.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR2 = ddlDirGeralDir2.SelectedValue != "" ? ddlDirGeralDir2.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_DIR3 = ddlFuncDir3.SelectedValue != "" ? (int?)int.Parse(ddlFuncDir3.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_DIR3 = txtTitulDir3.Text != "" ? txtTitulDir3.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR3 = ddlTipoEnsinoDir3.SelectedValue != "" ? ddlTipoEnsinoDir3.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR3 = ddlDirGeralDir3.SelectedValue != "" ? ddlDirGeralDir3.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_DIR4 = ddlFuncDir4.SelectedValue != "" ? (int?)int.Parse(ddlFuncDir4.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_DIR4 = txtTitulDir4.Text != "" ? txtTitulDir4.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR4 = ddlTipoEnsinoDir4.SelectedValue != "" ? ddlTipoEnsinoDir4.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR4 = ddlDirGeralDir4.SelectedValue != "" ? ddlDirGeralDir4.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_COOR1 = ddlFuncCoord1.SelectedValue != "" ? (int?)int.Parse(ddlFuncCoord1.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_COOR1 = txtTitulCoord1.Text != "" ? txtTitulCoord1.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR1 = ddlTipoEnsinoCoord1.SelectedValue != "" ? ddlTipoEnsinoCoord1.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR1 = ddlDirGeralCoord1.SelectedValue != "" ? ddlDirGeralCoord1.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_COOR2 = ddlFuncCoord2.SelectedValue != "" ? (int?)int.Parse(ddlFuncCoord2.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_COOR2 = txtTitulCoord2.Text != "" ? txtTitulCoord2.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR2 = ddlTipoEnsinoCoord2.SelectedValue != "" ? ddlTipoEnsinoCoord2.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR2 = ddlDirGeralCoord2.SelectedValue != "" ? ddlDirGeralCoord2.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_COOR3 = ddlFuncCoord3.SelectedValue != "" ? (int?)int.Parse(ddlFuncCoord3.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_COOR3 = txtTitulCoord3.Text != "" ? txtTitulCoord3.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR3 = ddlTipoEnsinoCoord3.SelectedValue != "" ? ddlTipoEnsinoCoord3.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR3 = ddlDirGeralCoord3.SelectedValue != "" ? ddlDirGeralCoord3.SelectedValue : null;
                tb25.TB83_PARAMETRO.CO_COOR4 = ddlFuncCoord4.SelectedValue != "" ? (int?)int.Parse(ddlFuncCoord4.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NO_TITUL_COOR4 = txtTitulCoord4.Text != "" ? txtTitulCoord4.Text : null;
                tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR4 = ddlTipoEnsinoCoord4.SelectedValue != "" ? ddlTipoEnsinoCoord4.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR4 = ddlDirGeralCoord4.SelectedValue != "" ? ddlDirGeralCoord4.SelectedValue : null;

//------------> Informações de Controle Pedagógico/Matrículas
                tb25.FL_TIPO_METOD = ddlMetodEnsino.SelectedValue;
                tb25.CO_FORMA_AVALIACAO = ddlFormaAvali.SelectedValue;
                tb25.TB83_PARAMETRO.TP_ENSINO = ddlMetodEnsino.SelectedValue;
                tb25.TB83_PARAMETRO.TP_FORMA_AVAL = ddlFormaAvali.SelectedValue;

                tb25.TB83_PARAMETRO.TP_PERIOD_AVAL = ddlPerioAval.SelectedValue;

///-----------> Lança as médias na tabela de parâmetro da unidade
                tb25.TB83_PARAMETRO.VL_MEDIA_APROV_DIRETA = decimal.TryParse(txtMediaAprovDireta.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
                tb25.TB83_PARAMETRO.VL_MEDIA_CURSO = decimal.TryParse(txtMediaAprovGeral.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
                tb25.TB83_PARAMETRO.VL_MEDIA_PROVA_FINAL = decimal.TryParse(txtMediaProvaFinal.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;

///-----------> Lança as datas de controle
                tb25.TB82_DTCT_EMP.DT_INI_RES = DateTime.TryParse(txtReservaMatriculaDtInicioIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_RES = DateTime.TryParse(txtReservaMatriculaDtFimIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_INI_INSC = null;
                tb25.TB82_DTCT_EMP.DT_FIM_INSC = null;
                tb25.TB82_DTCT_EMP.DT_INI_MAT = DateTime.TryParse(txtMatriculaInicioIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_MAT = DateTime.TryParse(txtMatriculaFimIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_INI_REMAN_ALUNO = DateTime.TryParse(txtDataRemanAlunoIni.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_REMAN_ALUNO = DateTime.TryParse(txtDataRemanAlunoFim.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_INI_TRANS_INTER = DateTime.TryParse(txtDtInicioTransInter.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_TRANS_INTER = DateTime.TryParse(txtDtFimTransInter.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_INI_TRAN = DateTime.TryParse(txtTrancamentoMatriculaInicioIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_TRAN = DateTime.TryParse(txtTrancamentoMatriculaFimIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_INI_ALTMAT = DateTime.TryParse(txtAlteracaoMatriculaInicioIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_ALTMAT = DateTime.TryParse(txtAlteracaoMatriculaFimIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_INI_REMAT = DateTime.TryParse(txtRematriculaInicioIUE.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_FIM_REMAT = DateTime.TryParse(txtRematriculaFimIUE.Text, out retornaData) ? (DateTime?)retornaData : null;

///-----------> Lança as datas de controle do bimestre
                tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1 = DateTime.TryParse(txtPeriodoIniBim1.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM1 = DateTime.TryParse(txtPeriodoFimBim1.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 = DateTime.TryParse(txtLactoIniBim1.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 = DateTime.TryParse(txtLactoFimBim1.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2 = DateTime.TryParse(txtPeriodoIniBim2.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM2 = DateTime.TryParse(txtPeriodoFimBim2.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 = DateTime.TryParse(txtLactoIniBim2.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 = DateTime.TryParse(txtLactoFimBim2.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3 = DateTime.TryParse(txtPeriodoIniBim3.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM3 = DateTime.TryParse(txtPeriodoFimBim3.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 = DateTime.TryParse(txtLactoIniBim3.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 = DateTime.TryParse(txtLactoFimBim3.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4 = DateTime.TryParse(txtPeriodoIniBim4.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM4 = DateTime.TryParse(txtPeriodoFimBim4.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 = DateTime.TryParse(txtLactoIniBim4.Text, out retornaData) ? (DateTime?)retornaData : null;
                tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 = DateTime.TryParse(txtLactoFimBim4.Text, out retornaData) ? (DateTime?)retornaData : null;
                           
//------------> Informaçoes de Secretaria Escolar
                tb25.TB83_PARAMETRO.TP_CTRLE_SECRE_ESCOL = tb149.TP_CTRLE_SECRE_ESCOL == TipoControle.U.ToString() ? "U" : "I";
                tb25.TB83_PARAMETRO.FL_NU_SOLIC_AUTO_SECRE = ddlGeraNumSolicAuto.SelectedValue;
                tb25.TB83_PARAMETRO.NU_SOLIC_INICI_SECRE = int.TryParse(txtNumIniciSolicAuto.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FL_CTRLE_PRAZO_ENT_SECRE = ddlContrPrazEntre.SelectedValue;
                tb25.TB83_PARAMETRO.NU_DIAS_ENT_SOL = int.TryParse(txtQtdeDiasEntreSolic.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FLA_CTRLE_DIAS_SOLIC = ddlAlterPrazoEntreSolic.SelectedValue;
                tb25.TB83_PARAMETRO.FL_APRE_VALOR_SERV_SECRE = ddlFlagApresValorServi.SelectedValue;
                tb25.TB83_PARAMETRO.FL_ABONA_VALOR_SERV_SECRE = ddlAbonaValorServiSolic.SelectedValue;
                tb25.TB83_PARAMETRO.FL_ALTER_VALOR_SERV_SECRE = ddlApresValorServiSolic.SelectedValue;
                tb25.TB83_PARAMETRO.FL_INCLU_TAXA_CAR_SECRE = ddlFlagIncluContaReceb.SelectedValue;
                tb25.CO_FLAG_GERA_BOLETO_SERV_SECR = ddlGeraBoletoServiSecre.SelectedValue;
                tb25.TP_BOLETO_BANC = ddlTipoBoletoServiSecre.SelectedValue != "" ? ddlTipoBoletoServiSecre.SelectedValue : null;
                tb25.TB83_PARAMETRO.FL_USUAR_FUNCI_SECRE = chkUsuarFunc.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_USUAR_PROFE_SECRE = chkUsuarProf.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_USUAR_ALUNO_SECRE = chkUsuarAluno.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.NU_IDADE_MINIM_USUAR_ALUNO_SECRE = int.TryParse(txtIdadeMinimAlunoSecreEscol.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FL_USUAR_RESPO_SECRE = chkUsuarPaisRespo.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_USUAR_OUTRO_SECRE = chkUsuarOutro.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_HORAR_SECRE_IGUAL_UNID = chkHorAtiSecreEscol.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.HR_INI_TURNO1_SECRE = txtHorarIniT1SecreEscol.Text != "" ? txtHorarIniT1SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO1_SECRE = txtHorarFimT1SecreEscol.Text != "" ? txtHorarFimT1SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_INI_TURNO2_SECRE = txtHorarIniT2SecreEscol.Text != "" ? txtHorarIniT2SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO2_SECRE = txtHorarFimT2SecreEscol.Text != "" ? txtHorarFimT2SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_INI_TURNO3_SECRE = txtHorarIniT3SecreEscol.Text != "" ? txtHorarIniT3SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO3_SECRE = txtHorarFimT3SecreEscol.Text != "" ? txtHorarFimT3SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_INI_TURNO4_SECRE = txtHorarIniT4SecreEscol.Text != "" ? txtHorarIniT4SecreEscol.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO4_SECRE = txtHorarFimT4SecreEscol.Text != "" ? txtHorarFimT4SecreEscol.Text.Replace(":", "") : null;
//------------> Secretário(a) 1                
                tb25.TB83_PARAMETRO.TB03_COLABOR = ddlNomeSecreEscol1.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeSecreEscol1.SelectedValue)) : null;
                tb25.TB83_PARAMETRO.CO_CLASS_SECRE1 = int.TryParse(ddlClassifSecre1.SelectedValue, out retornaInt) ? (int?)retornaInt : null;

//------------> Secretário(a) 2                
                tb25.TB83_PARAMETRO.TB03_COLABOR2 = ddlNomeSecreEscol2.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeSecreEscol2.SelectedValue)) : null;
                tb25.TB83_PARAMETRO.CO_CLASS_SECRE2 = int.TryParse(ddlClassifSecre2.SelectedValue, out retornaInt) ? (int?)retornaInt : null;

//------------> Secretário(a) 3                
                tb25.TB83_PARAMETRO.TB03_COLABOR3 = ddlNomeSecreEscol3.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeSecreEscol3.SelectedValue)) : null;
                tb25.TB83_PARAMETRO.CO_CLASS_SECRE3 = int.TryParse(ddlClassifSecre3.SelectedValue, out retornaInt) ? (int?)retornaInt : null;

                string teste = ((decimal?)retornaDecimal).ToString();
                //Formatando o valor do Juros para salvar
                if (txtVlJurosSecre.Text != "")
                {
                    decimal jurosDec = decimal.Zero;
                    bool convertido = decimal.TryParse(string.Format("{0:0.000}", decimal.Parse(txtVlJurosSecre.Text)), out jurosDec);
                    tb25.VL_PEC_JUROS = convertido ? (decimal?)jurosDec : null; 
                }
                               
                tb25.CO_FLAG_TP_VALOR_JUROS = ddlFlagTipoJurosSecre.SelectedValue;
                tb25.VL_PEC_MULTA = decimal.TryParse(txtVlMultaSecre.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
                tb25.CO_FLAG_TP_VALOR_MUL = ddlFlagTipoMultaSecre.SelectedValue;

                tb25.TB83_PARAMETRO.FLA_CTRL_TIPO_ENSIN = tb149.FLA_CTRL_TIPO_ENSIN == TipoControle.U.ToString() ? "U" : "I";
                tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO = ddlGerarNireIIE.SelectedValue;
                tb25.TB83_PARAMETRO.FLA_GERA_MATR_AUTO = ddlGerarMatriculaIIE.SelectedValue;

//------------> Informaçoes de Controle de Biblioteca Escolar
                tb25.TB83_PARAMETRO.TP_CTRLE_BIBLI = tb149.TP_CTRLE_BIBLI == TipoControle.U.ToString() ? "U" : "I";
                //Verificar se é para criar um novo campo ou alterar esse!
                tb25.TB83_PARAMETRO.FLA_RESER_OUTRA_UNID = ddlFlagReserBibli.SelectedValue != "" ? ddlFlagReserBibli.SelectedValue : null;
                tb25.TB83_PARAMETRO.QT_ITENS_ALUNO_BIBLI = int.TryParse(txtQtdeItensReser.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.QT_DIAS_RESER_BIBLI = int.TryParse(txtQtdeMaxDiasReser.Text, out retornaInt) ? (int?)retornaInt : null;
                //
                tb25.TB83_PARAMETRO.FL_EMPRE_BIBLI = ddlFlagEmpreBibli.SelectedValue;
                tb25.TB83_PARAMETRO.QT_ITENS_EMPRE_BIBLI = int.TryParse(txtQtdeItensEmpre.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.QT_MAX_DIAS_EMPRE_BIBLI = int.TryParse(txtQtdeMaxDiasEmpre.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FL_NU_EMPRE_AUTO_BIBLI = ddlGeraNumEmpreAuto.SelectedValue;
                tb25.TB83_PARAMETRO.NU_EMPRE_INICI_BIBLI = int.TryParse(txtNumIniciEmpreAuto.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FL_TAXA_EMPRE_BIBLI = ddlFlagTaxaEmpre.SelectedValue;
                tb25.TB83_PARAMETRO.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI = int.TryParse(txtDiasBonusTaxaEmpre.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.VL_TAXA_DIA_EMPRE_BIBLI = decimal.TryParse(txtValorTaxaEmpre.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
                tb25.TB83_PARAMETRO.FL_MULTA_EMPRE_BIBLI = ddlFlagMultaAtraso.SelectedValue;
                tb25.TB83_PARAMETRO.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI = int.TryParse(txtDiasBonusMultaEmpre.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.VL_MULTA_DIA_ATRASO_BIBLI = decimal.TryParse(txtValorMultaEmpre.Text, out retornaDecimal) ? (decimal?)retornaDecimal : null;
                tb25.TB83_PARAMETRO.FL_NU_SOLIC_AUTO_BIBLI = ddlGeraNumItemAuto.SelectedValue;
                tb25.TB83_PARAMETRO.NU_SOLIC_INICI_BIBLI = int.TryParse(txtNumIniciItemAuto.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FL_NU_ISBN_OBRIG_BIBLI = ddlNumISBNObrig.SelectedValue;
                tb25.TB83_PARAMETRO.FL_INCLU_TAXA_CAR_BIBLI = ddlFlarIncluContaRecebBibli.SelectedValue;
                tb25.TB83_PARAMETRO.FL_USUAR_FUNCI_BIBLI = chkUsuarFuncBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_USUAR_PROFE_BIBLI = chkUsuarProfBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_USUAR_ALUNO_BIBLI = chkUsuarAlunoBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI = int.TryParse(txtIdadeMinimAlunoBibli.Text, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.FL_USUAR_RESPO_BIBLI = chkUsuarRespBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_USUAR_OUTRO_BIBLI = chkUsuarOutroBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_HORAR_BIBLI_IGUAL_UNID = chkHorAtiBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.HR_INI_TURNO1_BIBLI = txtHorarIniT1Bibli.Text != "" ? txtHorarIniT1Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO1_BIBLI = txtHorarFimT1Bibli.Text != "" ? txtHorarFimT1Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_INI_TURNO2_BIBLI = txtHorarIniT2Bibli.Text != "" ? txtHorarIniT2Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO2_BIBLI = txtHorarFimT2Bibli.Text != "" ? txtHorarFimT2Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_INI_TURNO3_BIBLI = txtHorarIniT3Bibli.Text != "" ? txtHorarIniT3Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO3_BIBLI = txtHorarFimT3Bibli.Text != "" ? txtHorarFimT3Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_INI_TURNO4_BIBLI = txtHorarIniT4Bibli.Text != "" ? txtHorarIniT4Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.HR_FIM_TURNO4_BIBLI = txtHorarFimT4Bibli.Text != "" ? txtHorarFimT4Bibli.Text.Replace(":", "") : null;
                tb25.TB83_PARAMETRO.TB03_COLABOR1 = ddlNomeBibliEscol1.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeBibliEscol1.SelectedValue)) : null;
                tb25.TB83_PARAMETRO.CO_CLASS_BIBLI1 = int.TryParse(ddlClassifBibli1.SelectedValue, out retornaInt) ? (int?)retornaInt : null;
                tb25.TB83_PARAMETRO.TB03_COLABOR4 = ddlNomeBibliEscol2.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeBibliEscol2.SelectedValue)) : null;
                tb25.TB83_PARAMETRO.CO_CLASS_BIBLI2 = int.TryParse(ddlClassifBibli2.SelectedValue, out retornaInt) ? (int?)retornaInt : null;

//------------> Não estão sendo utilizado em lugar nenhum do sistema
                tb25.TB83_PARAMETRO.FL_ATUA_END_RES_RES = "N";
                tb25.TB83_PARAMETRO.FL_ATUA_END_RES_EFE = "N";
//******************************************************************                

//------------> Informações Contábeis
                tb25.TP_CTRLE_CTA_CONTAB = tb149.TP_CTRLE_CTA_CONTAB == TipoControle.U.ToString() ? "U" : "I";
                if (ddlGrupoTxServSecre.SelectedValue != "")
                {
                    tb25.CO_CENT_CUSSOL = ddlCentroCustoTxServSecre.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoTxServSecre.SelectedValue) : null;
                    tb25.CO_CTSOL_EMP = int.Parse(ddlContaContabilTxServSecre.SelectedValue);
                }
                else
                {
                    tb25.CO_CENT_CUSSOL = null;
                    tb25.CO_CTSOL_EMP = null;
                }

                if (ddlGrupoTxServBibli.SelectedValue != "")
                {
                    tb25.CO_CENT_CUSBIB = ddlCentroCustoTxServBibli.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoTxServBibli.SelectedValue) : null;
                    tb25.CO_CTABIB_EMP = int.Parse(ddlContaContabilTxServBibli.SelectedValue);
                }
                else
                {
                    tb25.CO_CENT_CUSBIB = null;
                    tb25.CO_CTABIB_EMP = null;
                }

                if (ddlGrupoTxMatri.SelectedValue != "")
                {
                    tb25.CO_CENT_CUSMAT = ddlCentroCustoTxMatri.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoTxMatri.SelectedValue) : null;
                    tb25.CO_CTAMAT_EMP = int.Parse(ddlContaContabilTxMatri.SelectedValue);
                }
                else
                {
                    tb25.CO_CENT_CUSMAT = null;
                    tb25.CO_CTAMAT_EMP = null;
                }

                if (ddlGrupoContaCaixa.SelectedValue != "")
                {
                    tb25.CO_CENT_CUSTO_CAIXA = ddlCentroCustoContaCaixa.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoContaCaixa.SelectedValue) : null;
                    tb25.CO_CTA_CAIXA = int.Parse(ddlContaContabilContaCaixa.SelectedValue);
                }
                else
                {
                    tb25.CO_CENT_CUSTO_CAIXA = null;
                    tb25.CO_CTA_CAIXA = null;
                }

                if (ddlGrupoContaBanco.SelectedValue != "")
                {
                    tb25.CO_CENT_CUSTO_BANCO = ddlCentroCustoContaBanco.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoContaBanco.SelectedValue) : null;
                    tb25.CO_CTA_BANCO = int.Parse(ddlContaContabilContaBanco.SelectedValue);
                }
                else
                {
                    tb25.CO_CENT_CUSTO_BANCO = null;
                    tb25.CO_CTA_BANCO = null;
                }

                if (ddlGrupoAtiviExtra.SelectedValue != "")
                {
                    tb25.CO_CENT_CUSTO_ATIVI_EXTRA = ddlCentroCustoAtiviExtra.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoAtiviExtra.SelectedValue) : null;
                    tb25.CO_CTA_ATIVI_EXTRA = int.Parse(ddlContaContabilAtiviExtra.SelectedValue);
                }
                else
                {
                    tb25.CO_CENT_CUSTO_ATIVI_EXTRA = null;
                    tb25.CO_CTA_ATIVI_EXTRA = null;
                }

//------------> Informaçoes de Controle de Mensagens SMS
                tb25.TB83_PARAMETRO.TP_CTRLE_MENSA_SMS = tb149.TP_CTRLE_MENSA_SMS == TipoControle.U.ToString() ? "U" : "I";
                tb25.TB83_PARAMETRO.FL_ENVIO_SMS = chkEnvioSMS.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_SMS_SECRE_SOLIC = chkSMSSolicSecreEscol.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_SOLIC = ddlFlagSMSSolicEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_SECRE_SOLIC = txtMsgSMSSolic.Text != "" ? txtMsgSMSSolic.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_SECRE_ENTRE = chkSMSEntreSecreEscol.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_ENTRE = ddlFlagSMSEntreEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_SECRE_ENTRE = txtMsgSMSEntre.Text != "" ? txtMsgSMSEntre.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_SECRE_OUTRO = chkSMSOutroSecreEscol.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_OUTRO = ddlFlagSMSOutroEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_SECRE_OUTRO = txtMsgSMSOutro.Text != "" ? txtMsgSMSOutro.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_MATRI_RESER = chkSMSReserVagas.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_RESER = ddlFlagSMSReserVagasEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_MATRI_RESER = txtMsgSMSReserVagas.Text != "" ? txtMsgSMSReserVagas.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_MATRI_RENOV = chkSMSRenovMatri.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_RENOV = ddlFlagSMSRenovMatriEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_MATRI_RENOV = txtMsgSMSRenovMatri.Text != "" ? txtMsgSMSRenovMatri.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_MATRI_NOVA = chkSMSMatriNova.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_NOVA = ddlFlagSMSMatriNovaEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_MATRI_NOVA = txtMsgSMSMatriNova.Text != "" ? txtMsgSMSMatriNova.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_BIBLI_RESER = chkSMSReserBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_RESER = ddlFlagSMSReserBibliEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_BIBLI_RESER = txtMsgSMSReserBibli.Text != "" ? txtMsgSMSReserBibli.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_BIBLI_EMPRE = chkSMSEmpreBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE = ddlFlagSMSEmpreBibliEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_BIBLI_EMPRE = txtMsgSMSEmpreBibli.Text != "" ? txtMsgSMSEmpreBibli.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_BIBLI_DIVER = chkSMSDiverBibli.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_DIVER = ddlFlagSMSDiverBibliEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_BIBLI_DIVER = txtMsgSMSDiverBibli.Text != "" ? txtMsgSMSDiverBibli.Text : null;
                tb25.TB83_PARAMETRO.FL_SMS_FALTA_ALUNO = chkSMSFaltaAluno.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_FALTA_ALUNO = ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue;
                tb25.TB83_PARAMETRO.DES_SMS_FALTA_ALUNO = txtMsgSMSFaltaAluno.Text != "" ? txtMsgSMSFaltaAluno.Text : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_MATRIC = ddlBoletoMatric.SelectedValue != "" ? (int?)int.Parse(ddlBoletoMatric.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_MATRIC_RENOV = ddlBoletoRenovacao.SelectedValue != "" ? (int?)int.Parse(ddlBoletoRenovacao.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_MENSA = ddlBoletoMensalidade.SelectedValue != "" ? (int?)int.Parse(ddlBoletoMensalidade.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_ATIVI_EXTRA = ddlBoletoAtiviExtra.SelectedValue != "" ? (int?)int.Parse(ddlBoletoAtiviExtra.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_BIBLI = ddlBoletoBiblioteca.SelectedValue != "" ? (int?)int.Parse(ddlBoletoBiblioteca.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_SERV_SECRE = ddlBoletoServSecre.SelectedValue != "" ? (int?)int.Parse(ddlBoletoServSecre.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_SERV_DIVER = ddlBoletoServDiver.SelectedValue != "" ? (int?)int.Parse(ddlBoletoServDiver.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_NEGOC = ddlBoletoNegociacao.SelectedValue != "" ? (int?)int.Parse(ddlBoletoNegociacao.SelectedValue) : null;
                tb25.TB83_PARAMETRO.ID_BOLETO_OUTRO = ddlBoletoOutros.SelectedValue != "" ? (int?)int.Parse(ddlBoletoOutros.SelectedValue) : null;
                tb25.TB83_PARAMETRO.NU_DIA_VENCTO = txtDiaVencto.Text != "" ? (int?)int.Parse(txtDiaVencto.Text) : null;
                tb25.TB83_PARAMETRO.VL_PERCE_JUROS = txtJurosDiario.Text != "" ? (decimal?)decimal.Parse(txtJurosDiario.Text) : null;
                tb25.TB83_PARAMETRO.VL_PERCE_MULTA = txtMultaMensal.Text != "" ? (decimal?)decimal.Parse(txtMultaMensal.Text) : null;
                tb25.TB83_PARAMETRO.FL_INTEG_FINAN = chkIntegFinan.Checked ? "S" : "N";
                tb25.TB83_PARAMETRO.FL_LANCA_FREQ_ALUNO_HOMOL = chkLancaFreqHomol.Checked ? "S" : "N";
               // Alterar depois
                tb25.TB83_PARAMETRO.CO_AGRUP_REC = ddlAgrupCAR.SelectedValue != "" ? (int?)int.Parse(ddlAgrupCAR.SelectedValue) : null; ;

                #region Grava permissões de atendimento/agendamento/direcionamento/acolhimento 

                //De agendamento de atendimento
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_MEDIC = (chkPermMedicAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_ODONT = (chkPermOdontAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_ESTET = (chkPermEstetAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_NUTRI = (chkPermNutriAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_ENFER = (chkPermEnferAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_PSICO = (chkPermPsicoAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_FISIO = (chkPermFisioAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_FONOA = (chkPermFonoaAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_TERAP_OCUPA = (chkPermTeOcuAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_MUSIC = (chkPermMusicAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_SERV_MOVEL = (chkPermServMovAgend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_AGEND_OUTRO = (chkPermOutroAgend.Checked ? "S" : "N");

                //De Direcionamento/Atendimento/Recepção
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_MEDIC = (chkPermMedicDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_ODONT = (chkPermOdontDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_ESTET = (chkPermEstetDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_NUTRI = (chkPermNutriDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_ENFER = (chkPermEnfermDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_PSICO = (chkPermPsicoDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_FISIO = (chkPermFisioDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_FONOA = (chkPermFonoaDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_TERAP_OCUPA = (chkPermTeOCuDirec.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_DIREC_OUTRO = (chkPermOutroDirec.Checked ? "S" : "N");

                //De Triagem/Acolhimento
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_MEDIC = (chkPermMedicAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_ODONT = (chkPermOdontAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_ESTET = (chkPermEstetAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_NUTRI = (chkPermNutriAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_ENFER = (chkPermEnfermAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_PSICO = (chkPermPsicoAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_FISIO = (chkPermFisioAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_FONOA = (chkPermFonoaAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_TERAP_OCUPA = (chkPermTeOcuAcolh.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ACOLH_OUTRO = (chkPermOutroAcolh.Checked ? "S" : "N");

                //De Atendimento Médico
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_MEDIC = (chkPermMedicAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_ODONT = (chkPermOdontAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_ESTET = (chkPermEstetAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_NUTRI = (chkPermNutriAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_ENFER = (chkPermEnfermAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_PSICO = (chkPermPsicoAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_FISIO = (chkPermFisioAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_FONOA = (chkPermFonoaAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_TERAP_OCUPA = (chkPermTeOcuAtend.Checked ? "S" : "N");
                tb25.TB83_PARAMETRO.FL_PERM_ATEND_OUTRO = (chkPermOutroAtend.Checked ? "S" : "N");

                #endregion

                // Alterar os boletins associados a unidade
                foreach (GridViewRow r in grdBoletim.Rows)
                {
                    int coBol = Convert.ToInt32(grdBoletim.DataKeys[r.RowIndex].Values[0]);

                    ADMUNIDBOLETIM bolUnid = (from ub in ADMUNIDBOLETIM.RetornaTodosRegistros()
                                              where ub.ADMBOLETIM.CO_BOL == coBol && ub.TB25_EMPRESA.CO_EMP == tb25.CO_EMP
                                              select ub).FirstOrDefault();

                    if (((CheckBox)r.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        if (bolUnid == null)
                        {
                            bolUnid = new ADMUNIDBOLETIM();
                        }

                        ADMBOLETIM bol = ADMBOLETIM.RetornaPelaChavePrimaria(coBol);

                        bolUnid.TB25_EMPRESA = tb25;
                        bolUnid.ADMBOLETIM = bol;
                        bolUnid.DT_CRIAC = DateTime.Now;

                        CurrentPadraoCadastros.CurrentEntity = bolUnid;
                    }
                    else
                    {
                        if (bolUnid != null)
                        {
                            ADMUNIDBOLETIM.Delete(bolUnid, false);
                        }
                    }
                }

                string horario = "";
                if (tb25.HR_FUNCI_ULTIM_TURNO_FIM != null)
                {
                    horario = "Funcionamento: " + "Seg a Sex de "
                            + tb25.HR_FUNCI_MANHA_INIC.Substring(0, 2) + ":"
                            + tb25.HR_FUNCI_MANHA_INIC.Substring(2, 2) + " às " + tb25.HR_FUNCI_ULTIM_TURNO_FIM.Substring(0, 2)
                            + ":" + tb25.HR_FUNCI_ULTIM_TURNO_FIM.Substring(2, 2);
                }
                else if (tb25.HR_FUNCI_NOITE_FIM != null){
                    horario = "Funcionamento: " + "Seg a Sex de "
                        + tb25.HR_FUNCI_MANHA_INIC.Substring(0, 2) + ":"
                        + tb25.HR_FUNCI_MANHA_INIC.Substring(2, 2) + " às " + tb25.HR_FUNCI_NOITE_FIM.Substring(0, 2)
                        + ":" + tb25.HR_FUNCI_NOITE_FIM.Substring(2, 2);
                }
                else if (tb25.HR_FUNCI_TARDE_FIM != null)
                {
                    horario = "Funcionamento: " + "Seg a Sex de "
                        + tb25.HR_FUNCI_MANHA_INIC.Substring(0, 2) + ":"
                        + tb25.HR_FUNCI_MANHA_INIC.Substring(2, 2) + " às " + tb25.HR_FUNCI_TARDE_FIM.Substring(0, 2)
                        + ":" + tb25.HR_FUNCI_TARDE_FIM.Substring(2, 2);
                }
                
                var connStr = "Data Source=user-pc;Initial Catalog=BDGC_XX_MODELO;Persist Security Info=True;User ID=sa;Password=@#!CJr;MultipleActiveResultSets=True";
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr);

                
                    try
                    {
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(@"INSERT INTO [BDGC_XX_Modelo].[dbo].[TSN012_CREDENCIADOS]
                       ([CO_TIPO_CREDENCIADO]
                       ,[NM_PRESTADOR]
                       ,[DE_CONSELHO]
                       ,[DT_SITUA_CREDENCIADO]
                       ,[CO_SITUA_CREDENCIADO]
                       ,[FL_UNID_PRINCIPAL]
                       ,[DE_URL]
                       ,[DE_ATENDIMENTO]
                       ,[DE_EMAIL]
                       ,[DE_TELEFONE])
                        VALUES
                       (@CO_TIPO_CREDENCIADO
                       ,@NM_PRESTADOR
                       ,@DE_CONSELHO
                       ,@DT_SITUA_CREDENCIADO
                       ,@CO_SITUA_CREDENCIADO
                       ,@FL_UNID_PRINCIPAL
                       ,@DE_URL
                       ,@DE_ATENDIMENTO
                       ,@DE_EMAIL
                       ,@DE_TELEFONE)", conn);
                        cmd.Parameters.AddWithValue("@CO_TIPO_CREDENCIADO", 1);// add valor default por falta de valor real
                        cmd.Parameters.AddWithValue("@NM_PRESTADOR", tb25.NO_FANTAS_EMP);
                        cmd.Parameters.AddWithValue("@DE_CONSELHO", "CRM");
                        //cmd.Parameters.AddWithValue("@NM_RANKING", tb25.ran); nulo
                        cmd.Parameters.AddWithValue("@DT_SITUA_CREDENCIADO", tb25.DT_SIT_EMP);
                        cmd.Parameters.AddWithValue("@CO_SITUA_CREDENCIADO", tb25.CO_SIT_EMP);
                        cmd.Parameters.AddWithValue("@FL_UNID_PRINCIPAL", tb25.FLA_UNID_GESTORA);
                        //cmd.Parameters.AddWithValue("@CO_UNID_FATURAMENTO", tb25.);
                        //cmd.Parameters.AddWithValue("@CO_UNID_PRINCIPAL", tb25.CO_UNID);
                        //cmd.Parameters.AddWithValue("@FL_ACESSIBILIDADE", agend.CO_SITUA_AGEND_HORAR);
                        //cmd.Parameters.AddWithValue("@CO_UNID_TB25", 282);
                        cmd.Parameters.AddWithValue("@DE_URL", tb25.NM_URL_UNID_B ?? Convert.DBNull);
                        cmd.Parameters.AddWithValue("@DE_ATENDIMENTO", horario ?? Convert.DBNull);
                        cmd.Parameters.AddWithValue("@DE_EMAIL", tb25.NO_EMAIL ?? Convert.DBNull);
                        cmd.Parameters.AddWithValue("@DE_TELEFONE", tb25.CO_TEL1_EMP ?? Convert.DBNull);
                        //cmd.Parameters.AddWithValue("@DE_WHATSAPP", tb25);
                        //cmd.Parameters.AddWithValue("@FL_ATENDE_24H", tb25.fl);
                        //cmd.Parameters.AddWithValue("@FL_SAUDE_PUBLICA", tb25.fl);
                        cmd.ExecuteNonQuery();
                   } 
                    catch (Exception ex)
                    {
                        Response.Write("Error Occurred:" + ex.Message.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }

                CurrentPadraoCadastros.CurrentEntity = tb25;
            }
        }     
        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB25_EMPRESA tb25 = RetornaEntidade();

            if (tb25 != null)
            {
//------------> Preenche informações da mensageria (email e senha)
                txtEmail.Text = tb25.NO_EMAIL;
                txtSenha.Text = tb25.SE_EMAIL;

//------------> Preenche informações da unidade no Quem somos
                txtUnidadeEscolarQS.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoQS.Text = tb25.sigla;
                txtCNPJQS.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Nossa História
                txtUnidadeEscolarNH.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoNH.Text = tb25.sigla;
                txtCNPJNH.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Proposta Pedagógica
                txtUnidadeEscolarPP.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoPP.Text = tb25.sigla;
                txtCNPJPP.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Controle de Frequência Funcional
                txtUnidadeEscolarFF.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoFF.Text = tb25.sigla;
                txtCNPJFF.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Pedagógico / Matrículas
                txtUnidadeEscolarPM.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoPM.Text = tb25.sigla;
                txtCNPJPM.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Gestores da Unidade
                txtUnidadeEscolarGU.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoGU.Text = tb25.sigla;
                txtCNPJGU.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Controle de Saúde
                txtUnidadeCS.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoCS.Text = tb25.sigla;
                txtCNPJCS.Text = tb25.CO_CPFCGC_EMP;

//------------> Preenche informações da unidade no Financeiro
                txtUnidadeEscolarFI.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacaoFI.Text = tb25.sigla;
                txtCNPJFI.Text = tb25.CO_CPFCGC_EMP;

                tb25.TB000_INSTITUICAOReference.Load();
                tb25.TB82_DTCT_EMPReference.Load();
                tb25.TB24_TPEMPRESAReference.Load();
                tb25.TB162_CLAS_INSTReference.Load();
                tb25.ImageReference.Load();
                //tb25.Image1Reference.Load();
                tb25.TB83_PARAMETROReference.Load();

//------------> Carrega informações da Unidade Escolar
                if (tb25.Image != null)
                    imgUnidadeIUE.CarregaImagem(tb25.Image.ImageId);
                else
                    imgUnidadeIUE.CarregaImagem(0);

                chkUnidadeMatriz.Checked = (tb25.FL_UNID_MATRIZ == "S" ? true : false);
                txtNomeIUE.Text = tb25.NO_FANTAS_EMP;
                txtSiglaIUE.Text = tb25.sigla;
                txtCNPJIUE.Text = tb25.CO_CPFCGC_EMP;
                txtRazaoSocialIUE.Text = tb25.NO_RAZSOC_EMP;
                ddlNucleoIUE.SelectedValue = tb25.CO_NUCLEO != null ? tb25.CO_NUCLEO.ToString() : "";
                cblTipoEnsino.Items[0].Selected = tb25.CO_FLAG_ENSIN_CURSO == "S";
                cblTipoEnsino.Items[1].Selected = tb25.CO_FLAG_ENSIN_FUNDA == "S";
                cblTipoEnsino.Items[2].Selected = tb25.CO_FLAG_ENSIN_INFAN == "S";
                cblTipoEnsino.Items[3].Selected = tb25.CO_FLAG_ENSIN_MEDIO == "S";
                cblTipoEnsino.Items[4].Selected = tb25.CO_FLAG_ENSIN_SUPER == "S"; 
                cblTipoEnsino.Items[5].Selected = tb25.CO_FLAG_ENSIN_OUTRO == "S";    
           
//------------> Carrega o campo do 9º Dígito.
                chkNonoDigito.Checked = (tb25.FL_NONO_DIGITO_TELEF == "S" ? true : false);

                txtCEPIUE.Text = tb25.CO_CEP_EMP;
                txtLogradouroIUE.Text = tb25.DE_END_EMP;
                txtNumeroLograIUE.Text = tb25.NU_END_EMP != null ? tb25.NU_END_EMP.ToString() : "";
                txtComplementoIUE.Text = tb25.DE_COM_ENDE_EMP;
                ddlUFIUE.SelectedValue = tb25.CO_UF_EMP;
                CarregaCidades();
                ddlCidadeIUE.SelectedValue = tb25.CO_CIDADE.ToString();
                CarregaBairros();
                ddlBairroIUE.SelectedValue = tb25.CO_BAIRRO.ToString();
                txtLatitude.Text = tb25.CO_GEORE_LATIT_EMP != null ? tb25.CO_GEORE_LATIT_EMP.ToString() : "";
                txtLongitude.Text = tb25.CO_GEORE_LONGI_EMP != null ? tb25.CO_GEORE_LONGI_EMP.ToString() : "";
                txtNumDocto.Text = tb25.CO_DOCTO_CONST != null ? tb25.CO_DOCTO_CONST.ToString() : null;
                txtDataConstituicao.Text = tb25.DT_CONST != null ? tb25.DT_CONST.Value.ToString("dd/MM/yyyy") : "";
                txtINEPIUE.Text = tb25.NU_INEP != null ? tb25.NU_INEP.ToString() : "";
                txtInscEstadualIUE.Text = tb25.CO_INS_ESTA_EMP;
                txtTelefoneIUE.Text = tb25.CO_TEL1_EMP;
                txtTelefoneIUE2.Text = tb25.CO_TEL2_EMP;
                txtEmailIUE.Text = tb25.NO_EMAIL_EMP;
                txtFaxIUE.Text = tb25.CO_FAX_EMP;
                txtWebSiteIUE.Text = tb25.NO_WEB_EMP;
                ddlStatusIUE.SelectedValue = tb25.CO_SIT_EMP;
                txtDtStatusIUE.Text = tb25.DT_SIT_EMP.ToString("dd/MM/yyyy");
                txtDtCadastroIUE.Text = tb25.DT_CAD_EMP.ToString("dd/MM/yyyy");
                txtObservacaoIUE.Text = tb25.DE_OBS_EMP;
                txtNomAvalAluno.Text = tb25.DE_TITUL_BOLET;

                txtDtSaldo.Text = tb25.DT_SALDO_INICIAL != null ? tb25.DT_SALDO_INICIAL.Value.ToString("dd/MM/yyyy") : "";
                txtSaldo.Text = tb25.VL_SALDO_INICIAL != null ? tb25.VL_SALDO_INICIAL.ToString() : "";
                
                CarregaDadosHorFuncionamento(tb25);                
                
//------------> Carrega informações da instituição para análise
                TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (tb149 != null)
                {
//----------------> Verifica se controle de Datas é Institucional ou da Unidade e carrega de acordo com essa informação
                    if (tb149.FLA_CTRL_DATA == TipoControle.I.ToString())
                    {
                        CarregaDadosControleData(tb149);
                    }
                    else if (tb149.FLA_CTRL_DATA == TipoControle.U.ToString())
                        if (tb25.TB82_DTCT_EMP != null)
                            CarregaDadosControleData(tb25);
                    
//----------------> Preenche informaçoes de Quem Somos; Nossa História; Proposta Pedagógica
                    if (tb149.TP_CTRLE_DESCR == TipoControle.I.ToString())
                    {
                        txtQuemSomos.Html = tb149.DES_QUEM_SOMOS != null ? tb149.DES_QUEM_SOMOS : "";
                        txtNossaHisto.Html = tb149.DES_NOSSA_HISTO != null ? tb149.DES_NOSSA_HISTO : "";
                        txtPropoPedag.Html = tb149.DES_PROPO_PEDAG != null ? tb149.DES_PROPO_PEDAG : "";

                        txtQuemSomos.Enabled = txtNossaHisto.Enabled = txtPropoPedag.Enabled = false;
                    }
                    else
                    {
                        txtQuemSomos.Html = tb25.DES_QUEM_SOMOS != null ? tb25.DES_QUEM_SOMOS : "";
                        txtNossaHisto.Html = tb25.DES_NOSSA_HISTO != null ? tb25.DES_NOSSA_HISTO : "";
                        txtPropoPedag.Html = tb25.DES_PROPO_PEDAG != null ? tb25.DES_PROPO_PEDAG : "";
                    }                        
                }
                else
                {
                    if (tb25.TB82_DTCT_EMP != null)
                    {
                        CarregaDadosControleData(tb25);
                        CarregaDadosHorFuncionamento(tb25);
                    }
                }

                if (tb25.TB83_PARAMETRO != null)
                {
//----------------> Informaçoes de Secretaria Escolar
                    if (tb149.TP_CTRLE_SECRE_ESCOL == TipoControle.I.ToString())
                        CarregaDadosSecretariaEscolar(tb149);
                    else
                        CarregaDadosSecretariaEscolar(tb25);

//----------------> Informaçoes de Controle de Biblioteca Escolar
                    if (tb149.TP_CTRLE_BIBLI == TipoControle.I.ToString())
                        CarregaDadosBibliotecaEscolar(tb149);
                    else
                        CarregaDadosBibliotecaEscolar(tb25);

//----------------> Informaçoes de Controle de Mensagens SMS
                    if (tb149.TP_CTRLE_MENSA_SMS == TipoControle.I.ToString())
                        CarregaDadosMensagensSMS(tb149);
                    else
                        CarregaDadosMensagensSMS(tb25);

                    if (tb25.TB83_PARAMETRO.FL_ENVIO_SMS != null && chkEnvioSMS.Enabled)
	                {
                        chkEnvioSMS.Checked = tb25.TB83_PARAMETRO.FL_ENVIO_SMS == "S";
	                }

                    txtValidRetorno.Text = tb25.TB83_PARAMETRO.NU_VALID_RETORNO != null ? tb25.TB83_PARAMETRO.NU_VALID_RETORNO.ToString() : "";
                    RadioButtonListAtendimento.SelectedValue = tb25.TB83_PARAMETRO.FL_PERM_ATEND_TRIAGEM;
                    CheckBoxAgendamento.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_AGEND == "S";
                    CheckBoxEncaixe.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_ENCAI == "S";
                    CheckBoxMovimentacao.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_MOVIM == "S";
                    CheckBoxGuia.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_GUIA == "S";
                    CheckBoxFicha.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_FICHA == "S";
                    CheckBoxRecContrato.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_CONTR == "S";
                    CheckBoxRecSimples.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_ATEND == "S";
                    CheckBoxRecCaixa.Checked = tb25.TB83_PARAMETRO.FL_PAINEL_RECEP_RECEB_CAIXA == "S";
                    ////agendamento
                    //txtQtdeDiasPosterior.Text = tb25.TB83_PARAMETRO.QT_DIAS_POSTERIOR.GetValueOrDefault(0) != 0 ? tb25.TB83_PARAMETRO.QT_DIAS_POSTERIOR.ToString() : string.Empty;
                    //txtQtdeDiasAnterior.Text = tb25.TB83_PARAMETRO.QT_DIAS_ANTERIOR.GetValueOrDefault(0) != 0 ? tb25.TB83_PARAMETRO.QT_DIAS_ANTERIOR.ToString() : string.Empty;
                    
                    //atendimento
                    CheckBoxProntPadrao.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_PRONT_PAD == "S";
                    CheckBoxProntModular.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_PRONT_MOD  == "S";
                    CheckBoxPresqMedicamentos.Checked =  tb25.TB83_PARAMETRO.FL_OPCAO_ATEN_MED == "S";
                    CheckBoxPresqExames.Checked  = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_EXAM == "S";
                    CheckBoxPresqAmbu.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_AMB == "S";
                    CheckBoxEmitirGuia.Checked  = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_GUIA == "S";
                    CheckBoxFichaAtend.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_FIXA_ATEND == "S";
                    CheckBoxEmitirAtestado.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ATEST == "S";
                    CheckBoxAnexarArq.Checked  = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ARQ == "S";
                    CheckBoxObserv.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_OBSERV == "S";
                    CheckBoFazerxEncaminha.Checked   = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ENCAM =="S";
                    CheckBoxRegInternacao.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_INTERNACAO == "S";
                    CheckBoxEmissaoAtestado.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_LAUDO_TEC == "S";
                    CheckBoxSolicCirurgia.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_CIRURGIA == "S";
                    CheckBoxSalvarAtend.Checked  = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_SALVAR == "S";
                    CheckBoxManterEspera.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_ESPERA == "S";
                    CheckBoxFinalizarAtend.Checked = tb25.TB83_PARAMETRO.FL_OPCAO_ATEND_FINALIZAR == "S";
//------------> Informações de Controle Gestores da Unidade
                    ddlFuncDir1.SelectedValue = tb25.TB83_PARAMETRO.CO_DIR1 != null ? tb25.TB83_PARAMETRO.CO_DIR1.ToString() : "";
                    txtTitulDir1.Text = tb25.TB83_PARAMETRO.NO_TITUL_DIR1;
                    ddlTipoEnsinoDir1.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR1;
                    ddlDirGeralDir1.SelectedValue = tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR1;
                    ddlFuncDir2.SelectedValue = tb25.TB83_PARAMETRO.CO_DIR2 != null ? tb25.TB83_PARAMETRO.CO_DIR2.ToString() : "";
                    txtTitulDir2.Text = tb25.TB83_PARAMETRO.NO_TITUL_DIR2;
                    ddlTipoEnsinoDir2.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR2;
                    ddlDirGeralDir2.SelectedValue = tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR2;
                    ddlFuncDir3.SelectedValue = tb25.TB83_PARAMETRO.CO_DIR3 != null ? tb25.TB83_PARAMETRO.CO_DIR3.ToString() : "";
                    txtTitulDir3.Text = tb25.TB83_PARAMETRO.NO_TITUL_DIR3;
                    ddlTipoEnsinoDir3.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR3;
                    ddlDirGeralDir3.SelectedValue = tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR3;
                    ddlFuncDir4.SelectedValue = tb25.TB83_PARAMETRO.CO_DIR4 != null ? tb25.TB83_PARAMETRO.CO_DIR4.ToString() : "";
                    txtTitulDir4.Text = tb25.TB83_PARAMETRO.NO_TITUL_DIR4;
                    ddlTipoEnsinoDir4.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_DIR4;
                    ddlDirGeralDir4.SelectedValue = tb25.TB83_PARAMETRO.FL_DIR_GERAL_DIR4;

                    ddlFuncCoord1.SelectedValue = tb25.TB83_PARAMETRO.CO_COOR1 != null ? tb25.TB83_PARAMETRO.CO_COOR1.ToString() : "";
                    txtTitulCoord1.Text = tb25.TB83_PARAMETRO.NO_TITUL_COOR1;
                    ddlTipoEnsinoCoord1.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR1;
                    ddlDirGeralCoord1.SelectedValue = tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR1;
                    ddlFuncCoord2.SelectedValue = tb25.TB83_PARAMETRO.CO_COOR2 != null ? tb25.TB83_PARAMETRO.CO_COOR2.ToString() : "";
                    txtTitulCoord2.Text = tb25.TB83_PARAMETRO.NO_TITUL_COOR2;
                    ddlTipoEnsinoCoord2.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR2;
                    ddlDirGeralCoord2.SelectedValue = tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR2;
                    ddlFuncCoord3.SelectedValue = tb25.TB83_PARAMETRO.CO_COOR3 != null ? tb25.TB83_PARAMETRO.CO_COOR3.ToString() : "";
                    txtTitulCoord3.Text = tb25.TB83_PARAMETRO.NO_TITUL_COOR3;
                    ddlTipoEnsinoCoord3.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR3;
                    ddlDirGeralCoord3.SelectedValue = tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR3;
                    ddlFuncCoord4.SelectedValue = tb25.TB83_PARAMETRO.CO_COOR4 != null ? tb25.TB83_PARAMETRO.CO_COOR4.ToString() : "";
                    txtTitulCoord4.Text = tb25.TB83_PARAMETRO.NO_TITUL_COOR4;
                    ddlTipoEnsinoCoord4.SelectedValue = tb25.TB83_PARAMETRO.CO_TP_ENSIN_COOR4;
                    ddlDirGeralCoord4.SelectedValue = tb25.TB83_PARAMETRO.FL_COOR_GERAL_COOR4;
                    

//----------------> Informaçoes de Controle Financeiro
                    ddlBoletoMatric.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_MATRIC != null ? tb25.TB83_PARAMETRO.ID_BOLETO_MATRIC.ToString() : "";
                    ddlBoletoRenovacao.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_MATRIC_RENOV != null ? tb25.TB83_PARAMETRO.ID_BOLETO_MATRIC_RENOV.ToString() : "";
                    ddlBoletoMensalidade.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_MENSA != null ? tb25.TB83_PARAMETRO.ID_BOLETO_MENSA.ToString() : "";
                    ddlBoletoAtiviExtra.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_ATIVI_EXTRA != null ? tb25.TB83_PARAMETRO.ID_BOLETO_ATIVI_EXTRA.ToString() : "";
                    ddlBoletoBiblioteca.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_BIBLI != null ? tb25.TB83_PARAMETRO.ID_BOLETO_BIBLI.ToString() : "";
                    ddlBoletoServSecre.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_SERV_SECRE != null ? tb25.TB83_PARAMETRO.ID_BOLETO_SERV_SECRE.ToString() : "";
                    ddlBoletoServDiver.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_SERV_DIVER != null ? tb25.TB83_PARAMETRO.ID_BOLETO_SERV_DIVER.ToString() : "";
                    ddlBoletoNegociacao.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_NEGOC != null ? tb25.TB83_PARAMETRO.ID_BOLETO_NEGOC.ToString() : "";
                    ddlBoletoOutros.SelectedValue = tb25.TB83_PARAMETRO.ID_BOLETO_OUTRO != null ? tb25.TB83_PARAMETRO.ID_BOLETO_OUTRO.ToString() : "";
                    ddlAgrupCAR.SelectedValue = tb25.TB83_PARAMETRO.CO_AGRUP_REC != null ? tb25.TB83_PARAMETRO.CO_AGRUP_REC.ToString() :"";
                    txtDiaVencto.Text = tb25.TB83_PARAMETRO.NU_DIA_VENCTO != null ? tb25.TB83_PARAMETRO.NU_DIA_VENCTO.ToString() : "";
                    txtJurosDiario.Text = tb25.TB83_PARAMETRO.VL_PERCE_JUROS != null ? tb25.TB83_PARAMETRO.VL_PERCE_JUROS.ToString() : "";
                    txtMultaMensal.Text = tb25.TB83_PARAMETRO.VL_PERCE_MULTA != null ? tb25.TB83_PARAMETRO.VL_PERCE_MULTA.ToString() : "";
                    chkIntegFinan.Checked = tb25.TB83_PARAMETRO.FL_INTEG_FINAN == "S";

                    #region Grava permissões de atendimento/agendamento/direcionamento/acolhimento

                    //De agendamento de atendimento
                    chkPermMedicAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_MEDIC == "S" ? true : false);
                    chkPermOdontAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_ODONT == "S" ? true : false);
                    chkPermNutriAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_NUTRI == "S" ? true : false);
                    chkPermEstetAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_ESTET == "S" ? true : false);
                    chkPermEnferAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_ENFER == "S" ? true : false);
                    chkPermPsicoAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_PSICO == "S" ? true : false);
                    chkPermFisioAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_FISIO == "S" ? true : false);
                    chkPermFonoaAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_FONOA == "S" ? true : false);
                    chkPermTeOcuAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_TERAP_OCUPA == "S" ? true : false);
                    chkPermMusicAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_MUSIC == "S" ? true : false);
                    chkPermServMovAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_SERV_MOVEL == "S" ? true : false);
                    chkPermOutroAgend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_AGEND_OUTRO == "S" ? true : false);

                    //De Direcionamento/Atendimento/Recepção
                    chkPermMedicDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_MEDIC == "S" ? true : false);
                    chkPermOdontDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_ODONT == "S" ? true : false);
                    chkPermNutriDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_NUTRI == "S" ? true : false);
                    chkPermEstetDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_ESTET == "S" ? true : false);
                    chkPermEnfermDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_ENFER == "S" ? true : false);
                    chkPermPsicoDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_PSICO  == "S" ? true : false);
                    chkPermFisioDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_FISIO == "S" ? true : false);
                    chkPermFonoaDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_FONOA == "S" ? true : false);
                    chkPermTeOCuDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_TERAP_OCUPA == "S" ? true : false);
                    chkPermOutroDirec.Checked = (tb25.TB83_PARAMETRO.FL_PERM_DIREC_OUTRO == "S" ? true : false);

                    //De Triagem/Acolhimento
                    chkPermMedicAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_MEDIC == "S" ? true : false);
                    chkPermOdontAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_ODONT == "S" ? true : false);
                    chkPermNutriAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_NUTRI == "S" ? true : false);
                    chkPermEstetAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_ESTET == "S" ? true : false);
                    chkPermEnfermAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_ENFER == "S" ? true : false);
                    chkPermPsicoAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_PSICO == "S" ? true : false);
                    chkPermFisioAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_FISIO == "S" ? true :false);
                    chkPermFonoaAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_FONOA  == "S" ? true :false);
                    chkPermTeOcuAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_TERAP_OCUPA == "S" ? true : false);
                    chkPermOutroAcolh.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ACOLH_OUTRO == "S" ? true : false);

                    //De Atendimento Médico
                    chkPermMedicAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_MEDIC == "S" ? true : false);
                    chkPermOdontAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_ODONT == "S" ? true : false);
                    chkPermNutriAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_NUTRI == "S" ? true : false);
                    chkPermEstetAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_ESTET == "S" ? true : false);
                    chkPermEnfermAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_ENFER == "S" ? true : false);
                    chkPermPsicoAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_PSICO == "S" ? true : false);
                    chkPermFisioAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_FISIO == "S" ? true : false);
                    chkPermFonoaAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_FONOA == "S" ? true : false);
                    chkPermTeOcuAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_TERAP_OCUPA == "S" ? true : false);
                    chkPermOutroAtend.Checked = (tb25.TB83_PARAMETRO.FL_PERM_ATEND_OUTRO == "S" ? true : false);

                    #endregion

//------------> Carrega os campos de lançamento de frequência

                    if (tb25.TB83_PARAMETRO != null)
                    {
                        if (tb25.TB83_PARAMETRO.FL_LANCA_FREQ_ALUNO_HOMOL != null)
                        {
                            if (tb25.TB83_PARAMETRO.FL_LANCA_FREQ_ALUNO_HOMOL == "S")
                                chkLancaFreqHomol.Checked = true;
                            else
                                chkLancaFreqHomol.Checked = false;
                        }
                    }
                }

                txtTipoCtrlFrequ.Text = "";
//------------> Carrega informações do Quadro de Horários Funcionais de acordo com a Unidade informada
                var tb300 = TB300_QUADRO_HORAR_FUNCI.RetornaPelaUnidade(tb25.CO_EMP);

                if (tb300.Count() > 0)
                {
                    int indice = 0;
                    txtTipoCtrlFrequ.Text = "Unidade de Ensino";
                    ddlPermiMultiFrequ.Enabled = true;
                    
//----------------> Formata a informação do quadro de horários de funcionários validando se está nulo ou não para que não haja erro de metodo de formatação que receba nulo
                    var result = (from res in tb300
                                  select new
                                  {
                                      res.ID_QUADRO_HORAR_FUNCI,
                                      res.CO_SIGLA_TIPO_PONTO,
                                      LIMIT_ENTRA = res.HR_LIMIT_ENTRA != null ? res.HR_LIMIT_ENTRA.Insert(2,":") : null,
                                      ENTRA_TURNO1 = res.HR_ENTRA_TURNO1 != null ? res.HR_ENTRA_TURNO1.Insert(2,":") : null,
                                      SAIDA_TURNO1 = res.HR_SAIDA_TURNO1 != null ? res.HR_SAIDA_TURNO1.Insert(2,":") : null,
                                      ENTRA_INTER = res.HR_ENTRA_INTER != null ? res.HR_ENTRA_INTER.Insert(2,":") : null,
                                      SAIDA_INTER = res.HR_SAIDA_INTER != null ? res.HR_SAIDA_INTER.Insert(2,":") : null,
                                      ENTRA_TURNO2 = res.HR_ENTRA_TURNO2 != null ? res.HR_ENTRA_TURNO2.Insert(2,":") : null,
                                      SAIDA_TURNO2 = res.HR_SAIDA_TURNO2 != null ? res.HR_SAIDA_TURNO2.Insert(2,":") : null,
                                      LIMIT_SAIDA = res.HR_LIMIT_SAIDA != null ? res.HR_LIMIT_SAIDA.Insert(2,":") : null,
                                      ENTRA_EXTRA = res.HR_ENTRA_EXTRA != null ? res.HR_ENTRA_EXTRA.Insert(2,":") : null,
                                      SAIDA_EXTRA = res.HR_SAIDA_EXTRA != null ? res.HR_SAIDA_EXTRA.Insert(2,":") : null,
                                      LIMIT_SAIDA_EXTRA = res.HR_LIMIT_SAIDA_EXTRA != null ? res.HR_LIMIT_SAIDA_EXTRA.Insert(2, ":") : null

                                  }).OrderBy(o => o.CO_SIGLA_TIPO_PONTO);

                    grdHorarios.DataSource = result.ToList();
                    grdHorarios.DataBind();

                    
                }
                else
                {
//----------------> Carrega Quadro de Horário Funcional de acordo com a Instituição informada
                    var tb300_inst = TB300_QUADRO_HORAR_FUNCI.RetornaPelaInstituicao(LoginAuxili.ORG_CODIGO_ORGAO);
                    int indice = 0;                    

                    if (tb300_inst != null)
                    {
                        txtTipoCtrlFrequ.Text = "Instituição de Ensino";
                        ddlPermiMultiFrequ.Enabled = false;

                        var result = (from res in tb300
                                      select new
                                      {
                                          res.ID_QUADRO_HORAR_FUNCI,
                                          res.CO_SIGLA_TIPO_PONTO,
                                          LIMIT_ENTRA = res.HR_LIMIT_ENTRA != null ? res.HR_LIMIT_ENTRA.Insert(2, ":") : null,
                                          ENTRA_TURNO1 = res.HR_ENTRA_TURNO1 != null ? res.HR_ENTRA_TURNO1.Insert(2, ":") : null,
                                          SAIDA_TURNO1 = res.HR_SAIDA_TURNO1 != null ? res.HR_SAIDA_TURNO1.Insert(2, ":") : null,
                                          ENTRA_INTER = res.HR_ENTRA_INTER != null ? res.HR_ENTRA_INTER.Insert(2, ":") : null,
                                          SAIDA_INTER = res.HR_SAIDA_INTER != null ? res.HR_SAIDA_INTER.Insert(2, ":") : null,
                                          ENTRA_TURNO2 = res.HR_ENTRA_TURNO2 != null ? res.HR_ENTRA_TURNO2.Insert(2, ":") : null,
                                          SAIDA_TURNO2 = res.HR_SAIDA_TURNO2 != null ? res.HR_SAIDA_TURNO2.Insert(2, ":") : null,
                                          LIMIT_SAIDA = res.HR_LIMIT_SAIDA != null ? res.HR_LIMIT_SAIDA.Insert(2, ":") : null,
                                          ENTRA_EXTRA = res.HR_ENTRA_EXTRA != null ? res.HR_ENTRA_EXTRA.Insert(2, ":") : null,
                                          SAIDA_EXTRA = res.HR_SAIDA_EXTRA != null ? res.HR_SAIDA_EXTRA.Insert(2, ":") : null,
                                          LIMIT_SAIDA_EXTRA = res.HR_LIMIT_SAIDA_EXTRA != null ? res.HR_LIMIT_SAIDA_EXTRA.Insert(2, ":") : null

                                      }).OrderBy(o => o.CO_SIGLA_TIPO_PONTO);

                        grdHorarios.DataSource = result.ToList();
                        grdHorarios.DataBind();
                                                
                    }
                    else
                        ddlPermiMultiFrequ.Enabled = true;
                }                

//------------> Carrega informaçoes de Controle Contábil de acordo com o tipo ([I]nstituição/[U]nidade)
                if (tb149.TP_CTRLE_CTA_CONTAB == TipoControle.I.ToString())
                    CarregaDadosContaContabil(tb149);
                else
                    CarregaDadosContaContabil(tb25);                

                if (tb25.TB83_PARAMETRO != null)
                {
//----------------> Verifica se controle metodologia é da Unidade e carrega de acordo com essa informação
                    if (tb149.TP_CTRLE_METOD == TipoControle.U.ToString())
                    {
                        ddlMetodEnsino.SelectedValue = tb25.TB83_PARAMETRO.TP_ENSINO;
                        ddlFormaAvali.SelectedValue = tb25.TB83_PARAMETRO.TP_FORMA_AVAL;

                        if (ddlFormaAvali.SelectedValue == "C")
                        {
//------------------------> Carrega Conceitos de acordo com a Unidade informada
                            var tb200 = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                         where iTb200.TB25_EMPRESA.CO_EMP == tb25.CO_EMP
                                         select iTb200).OrderByDescending(e => e.VL_NOTA_MIN);

                            int i = 1;

                            if (tb200 != null)
                            {
                                foreach (var iTb200 in tb200)
                                {
                                    if (i == 1)
                                    {
                                        txtDescrConce1.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce1.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni1.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim1.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 2)
                                    {
                                        txtDescrConce2.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce2.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni2.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim2.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 3)
                                    {
                                        txtDescrConce3.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce3.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni3.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim3.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 4)
                                    {
                                        txtDescrConce4.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce4.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni4.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim4.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 5)
                                    {
                                        txtDescrConce5.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce5.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni5.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim5.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    i++;
                                }
                            }
                        }                        
                    }
                    else
                    {
                        if (tb149.TP_FORMA_AVAL == "C")
                        {
//------------------------> Carrega conceitos de acordo com a Instituição informada
                            var tb200 = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                         where iTb200.ORG_CODIGO_ORGAO == tb149.ORG_CODIGO_ORGAO && iTb200.TB25_EMPRESA == null
                                         select iTb200).OrderByDescending(e => e.VL_NOTA_MIN);

                            int i = 1;

                            if (tb200 != null)
                            {
                                foreach (var iTb200 in tb200)
                                {
                                    if (i == 1)
                                    {
                                        txtDescrConce1.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce1.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni1.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim1.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 2)
                                    {
                                        txtDescrConce2.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce2.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni2.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim2.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 3)
                                    {
                                        txtDescrConce3.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce3.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni3.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim3.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 4)
                                    {
                                        txtDescrConce4.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce4.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni4.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim4.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    if (i == 5)
                                    {
                                        txtDescrConce5.Text = iTb200.DE_CONCEITO;
                                        txtSiglaConce5.Text = iTb200.CO_SIGLA_CONCEITO;
                                        txtNotaIni5.Text = iTb200.VL_NOTA_MIN.ToString();
                                        txtNotaFim5.Text = iTb200.VL_NOTA_MAX.ToString();
                                    }

                                    i++;
                                }
                            }
                        }                        
                    }

//----------------> Verifica se controle de parametros de avaliaçao é da Unidade e carrega de acordo com essa informação
                    if (tb149.TP_CTRLE_AVAL == TipoControle.U.ToString())
                    {
                        ddlPerioAval.SelectedValue = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;
                        
                        txtMediaAprovDireta.Text = tb25.TB83_PARAMETRO.VL_MEDIA_APROV_DIRETA != null ? tb25.TB83_PARAMETRO.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                        txtMediaAprovGeral.Text = tb25.TB83_PARAMETRO.VL_MEDIA_CURSO != null ? tb25.TB83_PARAMETRO.VL_MEDIA_CURSO.Value.ToString() : "";
                        
                        txtMediaProvaFinal.Text = tb25.TB83_PARAMETRO.VL_MEDIA_PROVA_FINAL != null ? tb25.TB83_PARAMETRO.VL_MEDIA_PROVA_FINAL.Value.ToString() : "";
                        
                        ddlGerarNireIIE.Enabled = ddlGerarMatriculaIIE.Enabled = true;
                        ddlGerarNireIIE.SelectedValue = tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO != null ? tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO : "N";
                        ddlGerarMatriculaIIE.SelectedValue = tb25.TB83_PARAMETRO.FLA_GERA_MATR_AUTO != null ? tb25.TB83_PARAMETRO.FLA_GERA_MATR_AUTO : "N";                        

                        HabilitaControleAvaliacao(true);
                    }
                }                
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB25_EMPRESA</returns>
        private TB25_EMPRESA RetornaEntidade()
        {
            return TB25_EMPRESA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
		
        private void CarregaGridImposto()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SIGLA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IMPOS";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "APLI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TIPO";
            dtV.Columns.Add(dcATM);

            //int i = 1;
            DataRow linha;
            //while (i <= 2)
            //{
            linha = dtV.NewRow();
            linha["SIGLA"] = "Confins";
            linha["IMPOS"] = "Contribuição para o Financiamento da Seguridade Social";
            linha["APLI"] = "Quinzenal";
			linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "CSLL";
            linha["IMPOS"] = "Contribuição Social sobre o Lucro Líquido";
			linha["APLI"] = "Mensal";
			linha["TIPO"] = "Outros";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "CSLL";
            linha["IMPOS"] = "Fundo de Garantia de Tempo de Serviço";
            linha["APLI"] = "Trimestral";
			linha["TIPO"] = "Outros";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "INSS";
            linha["IMPOS"] = "Contribuição ao Instituto Nacional de Seguridade Social";
			linha["APLI"] = "Mensal";
            linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "PIS";
            linha["IMPOS"] = "Programas de Integração Social";
			linha["APLI"] = "Anual";
			linha["TIPO"] = "Outros";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "PASEP";
            linha["IMPOS"] = "Programas de Integração Social";
			linha["APLI"] = "Trimestral";
			linha["TIPO"] = "Outros";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "IR";
            linha["IMPOS"] = "Imposto de Renda";
			linha["APLI"] = "Anual";
			linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "ISS";
            linha["IMPOS"] = "Imposto sobre Serviços";
			linha["APLI"] = "Mensal";
			linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "IOF";
            linha["IMPOS"] = "Contribuição sobre Operações Financeiras";
			linha["APLI"] = "Mensal";
			linha["TIPO"] = "Outros";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "IPTU";
            linha["IMPOS"] = "Imposto sobre a Propriedade Predial e Territorial Urbana";
			linha["APLI"] = "Anual";
            linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);
			
			linha = dtV.NewRow();
            linha["SIGLA"] = "IPVA";
            linha["IMPOS"] = "Imposto sobre a Propriedade de Veículos Automotores";
			linha["APLI"] = "Anual";
            linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "IPI";
            linha["IMPOS"] = "Imposto sobre Produtos Industrializados";
            linha["APLI"] = "Anual";
            linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            linha = dtV.NewRow();
            linha["SIGLA"] = "ITBI";
            linha["IMPOS"] = "Imposto sobre a Transmissão de Bens Imóveis";
            linha["APLI"] = "Anual";
            linha["TIPO"] = "Federal";
            dtV.Rows.Add(linha);

            grdImpostos.DataSource = dtV;
            grdImpostos.DataBind();
        }

        /// <summary>
        /// Verifica o tipo de unidade logada e trata algumas informações
        /// </summary>
        private void VerificaTipoUnidadeLogada()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    rblInforControle.Items.RemoveAt(3);
                    rblInforControle.Items.RemoveAt(4);
                    rblInforControle.Items.RemoveAt(6);
                    rblInforControle.Items.RemoveAt(7);
                    rblInforCadastro.Items.RemoveAt(3);
                    break;
                case "PGE":
                    rblInforControle.Items.RemoveAt(0);
                    rblInforControle.Items.RemoveAt(1);
                    break;
                case "EMP":
                    rblInforControle.Items.RemoveAt(0);
                    rblInforControle.Items.RemoveAt(2);
                    rblInforControle.Items.RemoveAt(4);
                    rblInforControle.Items.RemoveAt(5);
                    rblInforControle.Items.RemoveAt(8);
                    break;
            }
        }

        /// <summary>
        /// Método que carrega dados de Controle de Datas de acordo com a Instituição informada
        /// </summary>
        /// <param name="tb149">Entidade TB149_PARAM_INSTI</param>
        protected void CarregaDadosControleData(TB149_PARAM_INSTI tb149)
        {
            txtReservaMatriculaDtInicioIUE.Text = tb149.DT_INI_RES != null ? tb149.DT_INI_RES.Value.ToString("dd/MM/yyyy") : "";
            txtReservaMatriculaDtFimIUE.Text = tb149.DT_FIM_RES != null ? tb149.DT_FIM_RES.Value.ToString("dd/MM/yyyy") : "";
            txtRematriculaInicioIUE.Text = tb149.DT_INI_REMAT != null ? tb149.DT_INI_REMAT.Value.ToString("dd/MM/yyyy") : "";
            txtRematriculaFimIUE.Text = tb149.DT_FIM_REMAT != null ? tb149.DT_FIM_REMAT.Value.ToString("dd/MM/yyyy") : "";
            txtMatriculaInicioIUE.Text = tb149.DT_INI_MAT != null ? tb149.DT_INI_MAT.Value.ToString("dd/MM/yyyy") : "";
            txtMatriculaFimIUE.Text = tb149.DT_FIM_MAT != null ? tb149.DT_FIM_MAT.Value.ToString("dd/MM/yyyy") : "";
            txtDataRemanAlunoIni.Text = tb149.DT_INI_REMAN_ALUNO != null ? tb149.DT_INI_REMAN_ALUNO.Value.ToString("dd/MM/yyyy") : "";
            txtDataRemanAlunoFim.Text = tb149.DT_FIM_REMAN_ALUNO != null ? tb149.DT_FIM_REMAN_ALUNO.Value.ToString("dd/MM/yyyy") : "";
            txtDtInicioTransInter.Text = tb149.DT_INI_TRANS_INTER != null ? tb149.DT_INI_TRANS_INTER.Value.ToString("dd/MM/yyyy") : "";
            txtDtFimTransInter.Text = tb149.DT_FIM_TRANS_INTER != null ? tb149.DT_FIM_TRANS_INTER.Value.ToString("dd/MM/yyyy") : "";            
            txtTrancamentoMatriculaInicioIUE.Text = tb149.DT_INI_TRAN != null ? tb149.DT_INI_TRAN.Value.ToString("dd/MM/yyyy") : "";
            txtTrancamentoMatriculaFimIUE.Text = tb149.DT_FIM_TRAN != null ? tb149.DT_FIM_TRAN.Value.ToString("dd/MM/yyyy") : "";
            txtAlteracaoMatriculaInicioIUE.Text = tb149.DT_INI_ALTMAT != null ? tb149.DT_INI_ALTMAT.Value.ToString("dd/MM/yyyy") : "";
            txtAlteracaoMatriculaFimIUE.Text = tb149.DT_FIM_ALTMAT != null ? tb149.DT_FIM_ALTMAT.Value.ToString("dd/MM/yyyy") : "";
            txtReservaMatriculaDtInicioIUE.Enabled = txtReservaMatriculaDtFimIUE.Enabled = txtDtInicioTransInter.Enabled = txtRematriculaInicioIUE.Enabled =
            txtRematriculaFimIUE.Enabled = txtDataRemanAlunoIni.Enabled = txtDataRemanAlunoFim.Enabled =
            txtDtFimTransInter.Enabled = txtMatriculaInicioIUE.Enabled = txtMatriculaFimIUE.Enabled = txtTrancamentoMatriculaInicioIUE.Enabled =
            txtTrancamentoMatriculaFimIUE.Enabled = txtAlteracaoMatriculaInicioIUE.Enabled = txtAlteracaoMatriculaFimIUE.Enabled = false;
        }

        /// <summary>
        /// Método que carrega dados de Controle de Datas de acordo com a Unidade informada
        /// </summary>
        /// <param name="tb25">Entidade TB25_EMPRESA</param>
        protected void CarregaDadosControleData(TB25_EMPRESA tb25)
        {
            txtReservaMatriculaDtInicioIUE.Text = tb25.TB82_DTCT_EMP.DT_INI_RES != null ? tb25.TB82_DTCT_EMP.DT_INI_RES.Value.ToString("dd/MM/yyyy") : "";
            txtReservaMatriculaDtFimIUE.Text = tb25.TB82_DTCT_EMP.DT_FIM_RES != null ? tb25.TB82_DTCT_EMP.DT_FIM_RES.Value.ToString("dd/MM/yyyy") : "";
            txtRematriculaInicioIUE.Text = tb25.TB82_DTCT_EMP.DT_INI_REMAT != null ? tb25.TB82_DTCT_EMP.DT_INI_REMAT.Value.ToString("dd/MM/yyyy") : "";
            txtRematriculaFimIUE.Text = tb25.TB82_DTCT_EMP.DT_FIM_REMAT != null ? tb25.TB82_DTCT_EMP.DT_FIM_REMAT.Value.ToString("dd/MM/yyyy") : "";
            txtMatriculaInicioIUE.Text = tb25.TB82_DTCT_EMP.DT_INI_MAT != null ? tb25.TB82_DTCT_EMP.DT_INI_MAT.Value.ToString("dd/MM/yyyy") : "";
            txtMatriculaFimIUE.Text = tb25.TB82_DTCT_EMP.DT_FIM_MAT != null ? tb25.TB82_DTCT_EMP.DT_FIM_MAT.Value.ToString("dd/MM/yyyy") : "";
            txtDataRemanAlunoIni.Text = tb25.TB82_DTCT_EMP.DT_INI_REMAN_ALUNO != null ? tb25.TB82_DTCT_EMP.DT_INI_REMAN_ALUNO.Value.ToString("dd/MM/yyyy") : "";
            txtDataRemanAlunoFim.Text = tb25.TB82_DTCT_EMP.DT_FIM_REMAN_ALUNO != null ? tb25.TB82_DTCT_EMP.DT_FIM_REMAN_ALUNO.Value.ToString("dd/MM/yyyy") : "";
            txtDtInicioTransInter.Text = tb25.TB82_DTCT_EMP.DT_INI_TRANS_INTER != null ? tb25.TB82_DTCT_EMP.DT_INI_TRANS_INTER.Value.ToString("dd/MM/yyyy") : "";
            txtDtFimTransInter.Text = tb25.TB82_DTCT_EMP.DT_FIM_TRANS_INTER != null ? tb25.TB82_DTCT_EMP.DT_FIM_TRANS_INTER.Value.ToString("dd/MM/yyyy") : "";            
            txtTrancamentoMatriculaInicioIUE.Text = tb25.TB82_DTCT_EMP.DT_INI_TRAN != null ? tb25.TB82_DTCT_EMP.DT_INI_TRAN.Value.ToString("dd/MM/yyyy") : "";
            txtTrancamentoMatriculaFimIUE.Text = tb25.TB82_DTCT_EMP.DT_FIM_TRAN != null ? tb25.TB82_DTCT_EMP.DT_FIM_TRAN.Value.ToString("dd/MM/yyyy") : "";
            txtAlteracaoMatriculaInicioIUE.Text = tb25.TB82_DTCT_EMP.DT_INI_ALTMAT != null ? tb25.TB82_DTCT_EMP.DT_INI_ALTMAT.Value.ToString("dd/MM/yyyy") : "";
            txtAlteracaoMatriculaFimIUE.Text = tb25.TB82_DTCT_EMP.DT_FIM_ALTMAT != null ? tb25.TB82_DTCT_EMP.DT_FIM_ALTMAT.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoIniBim1.Text = tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM1.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoFimBim1.Text = tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM1 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM1.Value.ToString("dd/MM/yyyy") : "";
            txtLactoIniBim1.Text = tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1.Value.ToString("dd/MM/yyyy") : "";
            txtLactoFimBim1.Text = tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoIniBim2.Text = tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM2.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoFimBim2.Text = tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM2 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM2.Value.ToString("dd/MM/yyyy") : "";
            txtLactoIniBim2.Text = tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2.Value.ToString("dd/MM/yyyy") : "";
            txtLactoFimBim2.Text = tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoIniBim3.Text = tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM3.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoFimBim3.Text = tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM3 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM3.Value.ToString("dd/MM/yyyy") : "";
            txtLactoIniBim3.Text = tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3.Value.ToString("dd/MM/yyyy") : "";
            txtLactoFimBim3.Text = tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoIniBim4.Text = tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_INICI_BIM4.Value.ToString("dd/MM/yyyy") : "";
            txtPeriodoFimBim4.Text = tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM4 != null ? tb25.TB82_DTCT_EMP.DT_PERIO_FINAL_BIM4.Value.ToString("dd/MM/yyyy") : "";
            txtLactoIniBim4.Text = tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4.Value.ToString("dd/MM/yyyy") : "";
            txtLactoFimBim4.Text = tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null ? tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4.Value.ToString("dd/MM/yyyy") : "";
        }

        /// <summary>
        /// Método que carrega dados de Secretaria Escolar de acordo com a Instituição informada
        /// </summary>
        /// <param name="tb149">Entidade TB149_PARAM_INSTI</param>
        protected void CarregaDadosSecretariaEscolar(TB149_PARAM_INSTI tb149)
        {
            tb149.TB03_COLABORReference.Load();
            tb149.TB03_COLABOR2Reference.Load();
            tb149.TB03_COLABOR3Reference.Load();

            txtTipoCtrlSecreEscol.Text = "Instituição de Ensino";
            ddlGeraNumSolicAuto.SelectedValue = tb149.FL_NU_SOLIC_AUTO_SECRE != null ? tb149.FL_NU_SOLIC_AUTO_SECRE : "N";
            txtNumIniciSolicAuto.Enabled = ddlGeraNumSolicAuto.Enabled = false;
            txtNumIniciSolicAuto.Text = tb149.NU_SOLIC_INICI_SECRE != null ? tb149.NU_SOLIC_INICI_SECRE.Value.ToString() : "";
            ddlContrPrazEntre.SelectedValue = tb149.FL_CTRLE_PRAZO_ENT_SECRE != null ? tb149.FL_CTRLE_PRAZO_ENT_SECRE : "N";
            txtQtdeDiasEntreSolic.Enabled = ddlAlterPrazoEntreSolic.Enabled = ddlContrPrazEntre.Enabled = false;
            txtQtdeDiasEntreSolic.Text = tb149.NU_DIAS_ENT_SOL != null ? tb149.NU_DIAS_ENT_SOL.Value.ToString() : "";
            ddlAlterPrazoEntreSolic.Enabled = false;
            ddlAlterPrazoEntreSolic.SelectedValue = tb149.FLA_CTRLE_DIAS_SOLIC != null ? tb149.FLA_CTRLE_DIAS_SOLIC : "N";

            ddlFlagApresValorServi.SelectedValue = tb149.FL_APRE_VALOR_SERV_SECRE != null ? tb149.FL_APRE_VALOR_SERV_SECRE : "N";
            ddlApresValorServiSolic.Enabled = ddlAbonaValorServiSolic.Enabled = ddlFlagApresValorServi.Enabled = false;
            ddlAbonaValorServiSolic.SelectedValue = tb149.FL_ABONA_VALOR_SERV_SECRE != null ? tb149.FL_ABONA_VALOR_SERV_SECRE : "N";
            ddlApresValorServiSolic.SelectedValue = tb149.FL_ALTER_VALOR_SERV_SECRE != null ? tb149.FL_ALTER_VALOR_SERV_SECRE : "N";

            ddlFlagIncluContaReceb.Enabled = false;
            ddlFlagIncluContaReceb.SelectedValue = tb149.FL_INCLU_TAXA_CAR_SECRE != null ? tb149.FL_INCLU_TAXA_CAR_SECRE : "N";            
            ddlGeraBoletoServiSecre.SelectedValue = tb149.CO_FLAG_GERA_BOLETO_SERV_SECR != null ? tb149.CO_FLAG_GERA_BOLETO_SERV_SECR : "N";
            ddlTipoBoletoServiSecre.Enabled = ddlGeraBoletoServiSecre.Enabled = false;
            ddlTipoBoletoServiSecre.SelectedValue = tb149.TP_BOLETO_BANC != null ? tb149.TP_BOLETO_BANC : "";
            chkUsuarFunc.Enabled = chkUsuarProf.Enabled = chkUsuarPaisRespo.Enabled = chkUsuarOutro.Enabled = false;
            chkUsuarFunc.Checked = tb149.FL_USUAR_FUNCI_SECRE == "S";
            chkUsuarProf.Checked = tb149.FL_USUAR_PROFE_SECRE == "S";
            chkUsuarAluno.Checked = tb149.FL_USUAR_ALUNO_SECRE == "S";
            txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarAluno.Enabled = false;
            txtIdadeMinimAlunoSecreEscol.Text = tb149.NU_IDADE_MINIM_USUAR_ALUNO_SECRE != null ? tb149.NU_IDADE_MINIM_USUAR_ALUNO_SECRE.Value.ToString() : "";
            chkUsuarPaisRespo.Checked = tb149.FL_USUAR_RESPO_SECRE == "S";
            chkUsuarOutro.Checked = tb149.FL_USUAR_OUTRO_SECRE == "S";
            txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled =
            txtHorarFimT2SecreEscol.Enabled = txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled =
            txtHorarIniT4SecreEscol.Enabled = txtHorarFimT4SecreEscol.Enabled = false;

            chkHorAtiSecreEscol.Enabled = false;
            if (chkHorAtiSecreEscol.Checked)
            {
                txtHorarIniT1SecreEscol.Text = txtHoraIniTurno1.Text;
                txtHorarFimT1SecreEscol.Text = txtHoraFimTurno1.Text;
                txtHorarIniT2SecreEscol.Text = txtHoraIniTurno2.Text;
                txtHorarFimT2SecreEscol.Text = txtHoraFimTurno2.Text;
                txtHorarIniT3SecreEscol.Text = txtHoraIniTurno3.Text;
                txtHorarFimT3SecreEscol.Text = txtHoraFimTurno3.Text;
                txtHorarIniT4SecreEscol.Text = txtHoraIniTurno4.Text;
                txtHorarFimT4SecreEscol.Text = txtHoraFimTurno4.Text;
            }
            else
            {
                txtHorarIniT1SecreEscol.Text = tb149.HR_INI_TURNO1_SECRE != null ? tb149.HR_INI_TURNO1_SECRE : "";
                txtHorarFimT1SecreEscol.Text = tb149.HR_FIM_TURNO1_SECRE != null ? tb149.HR_FIM_TURNO1_SECRE : "";
                txtHorarIniT2SecreEscol.Text = tb149.HR_INI_TURNO2_SECRE != null ? tb149.HR_INI_TURNO2_SECRE : "";
                txtHorarFimT2SecreEscol.Text = tb149.HR_FIM_TURNO2_SECRE != null ? tb149.HR_FIM_TURNO2_SECRE : "";
                txtHorarIniT3SecreEscol.Text = tb149.HR_INI_TURNO3_SECRE != null ? tb149.HR_INI_TURNO3_SECRE : "";
                txtHorarFimT3SecreEscol.Text = tb149.HR_FIM_TURNO3_SECRE != null ? tb149.HR_FIM_TURNO3_SECRE : "";
                txtHorarIniT4SecreEscol.Text = tb149.HR_INI_TURNO4_SECRE != null ? tb149.HR_INI_TURNO4_SECRE : "";
                txtHorarFimT4SecreEscol.Text = tb149.HR_FIM_TURNO4_SECRE != null ? tb149.HR_FIM_TURNO4_SECRE : "";
            }  
            
            txtVlJurosSecre.Enabled = txtVlMultaSecre.Enabled = ddlFlagTipoJurosSecre.Enabled = ddlFlagTipoMultaSecre.Enabled = false;
            txtVlJurosSecre.Text = tb149.VL_PEC_JUROS != null ? tb149.VL_PEC_JUROS.ToString() : "";
            ddlFlagTipoJurosSecre.SelectedValue = tb149.CO_FLAG_TP_VALOR_JUROS != null ? tb149.CO_FLAG_TP_VALOR_JUROS : "V";
            txtVlMultaSecre.Text = tb149.VL_PEC_MULTA != null ? tb149.VL_PEC_MULTA.ToString() : "";
            ddlFlagTipoMultaSecre.SelectedValue = tb149.CO_FLAG_TP_VALOR_MULTA != null ? tb149.CO_FLAG_TP_VALOR_MULTA : "V";

            //txtCabec1Relatorio.Enabled = txtCabec2Relatorio.Enabled = txtCabec3Relatorio.Enabled = txtCabec4Relatorio.Enabled = false;
            //txtCabec1Relatorio.Text = tb149.cablinha1;
            //txtCabec2Relatorio.Text = tb149.cablinha2;
            //txtCabec3Relatorio.Text = tb149.cablinha3;
            //txtCabec4Relatorio.Text = tb149.cablinha4;

            ddlSiglaUnidSecreEscol1.Enabled = ddlNomeSecreEscol1.Enabled = ddlClassifSecre1.Enabled =
            ddlSiglaUnidSecreEscol2.Enabled = ddlNomeSecreEscol2.Enabled = ddlClassifSecre2.Enabled =
            ddlSiglaUnidSecreEscol3.Enabled = ddlNomeSecreEscol3.Enabled = ddlClassifSecre3.Enabled = false;
//--------> Secretário(s)
            if (tb149.TB03_COLABOR != null)
            {
                ddlSiglaUnidSecreEscol1.SelectedValue = tb149.TB03_COLABOR.CO_EMP.ToString();
                CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol1, ddlNomeSecreEscol1);
                ddlNomeSecreEscol1.SelectedValue = tb149.TB03_COLABOR.CO_COL.ToString();
                ddlClassifSecre1.SelectedValue = tb149.CO_CLASS_SECRE1 != null ? tb149.CO_CLASS_SECRE1.ToString() : "1";
            }

            if (tb149.TB03_COLABOR2 != null)
            {
                CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol2);
                ddlSiglaUnidSecreEscol2.SelectedValue = tb149.TB03_COLABOR2.CO_EMP.ToString();
                CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol2, ddlNomeSecreEscol2);
                ddlNomeSecreEscol2.SelectedValue = tb149.TB03_COLABOR2.CO_COL.ToString();
                ddlClassifSecre2.SelectedValue = tb149.CO_CLASS_SECRE2 != null ? tb149.CO_CLASS_SECRE2.ToString() : "2";
            }

            if (tb149.TB03_COLABOR3 != null)
            {
                CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol3);
                ddlSiglaUnidSecreEscol3.SelectedValue = tb149.TB03_COLABOR3.CO_EMP.ToString();
                CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol3, ddlNomeSecreEscol3);
                ddlNomeSecreEscol3.SelectedValue = tb149.TB03_COLABOR3.CO_COL.ToString();
                ddlClassifSecre3.SelectedValue = tb149.CO_CLASS_SECRE3 != null ? tb149.CO_CLASS_SECRE3.ToString() : "3";
            }

            if (tb149.FLA_CTRL_TIPO_ENSIN == "I")
            {
                ddlGerarNireIIE.Enabled = ddlGerarMatriculaIIE.Enabled = false;
                ddlGerarNireIIE.SelectedValue = tb149.FLA_GERA_NIRE_AUTO != null ? tb149.FLA_GERA_NIRE_AUTO : "N";
                ddlGerarMatriculaIIE.SelectedValue = tb149.FLA_GERA_MATR_AUTO != null ? tb149.FLA_GERA_MATR_AUTO : "N";
            }            

            ddlPermiMultiFrequ.SelectedValue = tb149.FL_MULTI_FREQU != null ? tb149.FL_MULTI_FREQU : "S";
        }

        /// <summary>
        /// Método que carrega dados de Secretaria Escolar de acordo com a Unidade informada
        /// </summary>
        /// <param name="tb25">Entidade TB25_EMPRESA</param>
        protected void CarregaDadosSecretariaEscolar(TB25_EMPRESA tb25)
        {
            tb25.TB83_PARAMETRO.TB03_COLABORReference.Load();
            tb25.TB83_PARAMETRO.TB03_COLABOR2Reference.Load();
            tb25.TB83_PARAMETRO.TB03_COLABOR3Reference.Load();

            txtTipoCtrlSecreEscol.Text = "Unidade Escolar";
            ddlGeraNumSolicAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_NU_SOLIC_AUTO_SECRE != null ? tb25.TB83_PARAMETRO.FL_NU_SOLIC_AUTO_SECRE : "N";
            txtNumIniciSolicAuto.Enabled = ddlGeraNumSolicAuto.SelectedValue == "S";
            txtNumIniciSolicAuto.Text = tb25.TB83_PARAMETRO.NU_SOLIC_INICI_SECRE != null ? tb25.TB83_PARAMETRO.NU_SOLIC_INICI_SECRE.Value.ToString() : "";
            ddlContrPrazEntre.SelectedValue = tb25.TB83_PARAMETRO.FL_CTRLE_PRAZO_ENT_SECRE != null ? tb25.TB83_PARAMETRO.FL_CTRLE_PRAZO_ENT_SECRE : "N";
            txtQtdeDiasEntreSolic.Enabled = ddlAlterPrazoEntreSolic.Enabled = ddlContrPrazEntre.SelectedValue == "S";
            txtQtdeDiasEntreSolic.Text = tb25.TB83_PARAMETRO.NU_DIAS_ENT_SOL != null ? tb25.TB83_PARAMETRO.NU_DIAS_ENT_SOL.Value.ToString() : "";
            ddlAlterPrazoEntreSolic.SelectedValue = tb25.TB83_PARAMETRO.FLA_CTRLE_DIAS_SOLIC != null ? tb25.TB83_PARAMETRO.FLA_CTRLE_DIAS_SOLIC : "N";

            ddlFlagApresValorServi.Enabled = true;
            ddlFlagApresValorServi.SelectedValue = tb25.TB83_PARAMETRO.FL_APRE_VALOR_SERV_SECRE != null ? tb25.TB83_PARAMETRO.FL_APRE_VALOR_SERV_SECRE : "N";
            ddlAbonaValorServiSolic.Enabled = ddlFlagApresValorServi.Enabled = ddlApresValorServiSolic.SelectedValue == "S";
            ddlAbonaValorServiSolic.SelectedValue = tb25.TB83_PARAMETRO.FL_ABONA_VALOR_SERV_SECRE != null ? tb25.TB83_PARAMETRO.FL_ABONA_VALOR_SERV_SECRE : "N";
            ddlApresValorServiSolic.SelectedValue = tb25.TB83_PARAMETRO.FL_ALTER_VALOR_SERV_SECRE != null ? tb25.TB83_PARAMETRO.FL_ALTER_VALOR_SERV_SECRE : "N";

            ddlFlagIncluContaReceb.SelectedValue = tb25.TB83_PARAMETRO.FL_INCLU_TAXA_CAR_SECRE != null ? tb25.TB83_PARAMETRO.FL_INCLU_TAXA_CAR_SECRE : "N";
            if (ddlFlagIncluContaReceb.SelectedValue == "N")
	        {
                ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = ddlFlagTipoJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                 ddlFlagTipoMultaSecre.Enabled = false;
	        }
            else
            {
                ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = ddlFlagTipoJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                 ddlFlagTipoMultaSecre.Enabled = true;
                ddlGeraBoletoServiSecre.SelectedValue = tb25.CO_FLAG_GERA_BOLETO_SERV_SECR != null ? tb25.CO_FLAG_GERA_BOLETO_SERV_SECR : "N";
                ddlTipoBoletoServiSecre.Enabled = ddlGeraBoletoServiSecre.SelectedValue == "S";
                ddlTipoBoletoServiSecre.SelectedValue = tb25.TP_BOLETO_BANC != null ? tb25.TP_BOLETO_BANC : "";
                txtVlJurosSecre.Text = tb25.VL_PEC_JUROS != null ? tb25.VL_PEC_JUROS.ToString() : "";
                ddlFlagTipoJurosSecre.SelectedValue = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                txtVlMultaSecre.Text = tb25.VL_PEC_MULTA != null ? tb25.VL_PEC_MULTA.ToString() : "";
                ddlFlagTipoMultaSecre.SelectedValue = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
            }
            
            chkUsuarFunc.Checked = tb25.TB83_PARAMETRO.FL_USUAR_FUNCI_SECRE == "S";
            chkUsuarProf.Checked = tb25.TB83_PARAMETRO.FL_USUAR_PROFE_SECRE == "S";
            chkUsuarAluno.Checked = tb25.TB83_PARAMETRO.FL_USUAR_ALUNO_SECRE == "S";
            txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarAluno.Checked;
            txtIdadeMinimAlunoSecreEscol.Text = tb25.TB83_PARAMETRO.NU_IDADE_MINIM_USUAR_ALUNO_SECRE != null ? tb25.TB83_PARAMETRO.NU_IDADE_MINIM_USUAR_ALUNO_SECRE.Value.ToString() : "";
            chkUsuarPaisRespo.Checked = tb25.TB83_PARAMETRO.FL_USUAR_RESPO_SECRE == "S";
            chkUsuarOutro.Checked = tb25.TB83_PARAMETRO.FL_USUAR_OUTRO_SECRE == "S";

            chkHorAtiSecreEscol.Enabled = true;
            chkHorAtiSecreEscol.Checked = tb25.TB83_PARAMETRO.FL_HORAR_SECRE_IGUAL_UNID == "S";
            if (chkHorAtiSecreEscol.Checked)
            {
                txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled = txtHorarFimT2SecreEscol.Enabled =
                txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled = txtHorarIniT4SecreEscol.Enabled = txtHorarFimT4SecreEscol.Enabled = false;
                txtHorarIniT1SecreEscol.Text = txtHoraIniTurno1.Text;
                txtHorarFimT1SecreEscol.Text = txtHoraFimTurno1.Text;
                txtHorarIniT2SecreEscol.Text = txtHoraIniTurno2.Text;
                txtHorarFimT2SecreEscol.Text = txtHoraFimTurno2.Text;
                txtHorarIniT3SecreEscol.Text = txtHoraIniTurno3.Text;
                txtHorarFimT3SecreEscol.Text = txtHoraFimTurno3.Text;
                txtHorarIniT4SecreEscol.Text = txtHoraIniTurno4.Text;
                txtHorarFimT4SecreEscol.Text = txtHoraFimTurno4.Text;
            }
            else
            {
                txtHorarIniT1SecreEscol.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO1_SECRE != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO1_SECRE : "";
                txtHorarFimT1SecreEscol.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO1_SECRE != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO1_SECRE : "";
                txtHorarIniT2SecreEscol.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO2_SECRE != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO2_SECRE : "";
                txtHorarFimT2SecreEscol.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO2_SECRE != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO2_SECRE : "";
                txtHorarIniT3SecreEscol.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO3_SECRE != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO3_SECRE : "";
                txtHorarFimT3SecreEscol.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO3_SECRE != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO3_SECRE : "";
                txtHorarIniT4SecreEscol.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO4_SECRE != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO4_SECRE : "";
                txtHorarFimT4SecreEscol.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO4_SECRE != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO4_SECRE : "";
            }                                                         

            //txtCabec1Relatorio.Text = tb25.cablinha1;
            //txtCabec2Relatorio.Text = tb25.cablinha2;
            //txtCabec3Relatorio.Text = tb25.cablinha3;
            //txtCabec4Relatorio.Text = tb25.cablinha4;

//--------> Secretário(s)
            if (tb25.TB83_PARAMETRO.TB03_COLABOR != null)
            {
                ddlSiglaUnidSecreEscol1.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR.CO_EMP.ToString();
                CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol1, ddlNomeSecreEscol1);
                ddlNomeSecreEscol1.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR.CO_COL.ToString();
                ddlClassifSecre1.SelectedValue = tb25.TB83_PARAMETRO.CO_CLASS_SECRE1 != null ? tb25.TB83_PARAMETRO.CO_CLASS_SECRE1.ToString() : "1";
            }

            if (tb25.TB83_PARAMETRO.TB03_COLABOR2 != null)
            {
                CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol2);
                ddlSiglaUnidSecreEscol2.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR2.CO_EMP.ToString();
                CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol2, ddlNomeSecreEscol2);
                ddlNomeSecreEscol2.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR2.CO_COL.ToString();
                ddlClassifSecre2.SelectedValue = tb25.TB83_PARAMETRO.CO_CLASS_SECRE2 != null ? tb25.TB83_PARAMETRO.CO_CLASS_SECRE2.ToString() : "2";
            }

            if (tb25.TB83_PARAMETRO.TB03_COLABOR3 != null)
            {
                CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol3);
                ddlSiglaUnidSecreEscol3.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR3.CO_EMP.ToString();
                CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol3, ddlNomeSecreEscol3);
                ddlNomeSecreEscol3.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR3.CO_COL.ToString();
                ddlClassifSecre3.SelectedValue = tb25.TB83_PARAMETRO.CO_CLASS_SECRE3 != null ? tb25.TB83_PARAMETRO.CO_CLASS_SECRE3.ToString() : "3";
            }

            ddlGerarNireIIE.SelectedValue = tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO != null ? tb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO : "N";
            ddlGerarMatriculaIIE.SelectedValue = tb25.TB83_PARAMETRO.FLA_GERA_MATR_AUTO != null ? tb25.TB83_PARAMETRO.FLA_GERA_MATR_AUTO : "N";
            ddlPermiMultiFrequ.SelectedValue = tb25.TB83_PARAMETRO.FL_MULTI_FREQU != null ? tb25.TB83_PARAMETRO.FL_MULTI_FREQU : "S";
        }

        /// <summary>
        /// Método que carrega dados de Biblioteca Escolar de acordo com a Instituição informada
        /// </summary>
        /// <param name="tb149">Entidade TB149_PARAM_INSTI</param>
        protected void CarregaDadosBibliotecaEscolar(TB149_PARAM_INSTI tb149)
        {
            tb149.TB03_COLABOR1Reference.Load();
            tb149.TB03_COLABOR4Reference.Load();

            txtTipoCtrlBibliEscol.Text = "Instituição de Ensino";
            //Verificar se é para criar um novo campo ou alterar esse!
            ddlFlagReserBibli.SelectedValue = tb149.FLA_RESER_OUTRA_UNID != null ? tb149.FLA_RESER_OUTRA_UNID : "N";
            txtQtdeItensReser.Enabled = txtQtdeMaxDiasReser.Enabled = ddlFlagReserBibli.Enabled = false;
            txtQtdeItensReser.Text = tb149.QT_ITENS_ALUNO_BIBLI != null ? tb149.QT_ITENS_ALUNO_BIBLI.Value.ToString() : "";
            txtQtdeMaxDiasReser.Text = tb149.QT_DIAS_RESER_BIBLI != null ? tb149.QT_DIAS_RESER_BIBLI.Value.ToString() : "";
            //
            ddlFlagEmpreBibli.SelectedValue = tb149.FL_EMPRE_BIBLI != null ? tb149.FL_EMPRE_BIBLI : "N";
            txtQtdeItensEmpre.Enabled = txtQtdeMaxDiasEmpre.Enabled = ddlFlagEmpreBibli.Enabled = false;
            txtQtdeItensEmpre.Text = tb149.QT_ITENS_EMPRE_BIBLI != null ? tb149.QT_ITENS_EMPRE_BIBLI.Value.ToString() : "";
            txtQtdeMaxDiasEmpre.Text = tb149.QT_MAX_DIAS_EMPRE_BIBLI != null ? tb149.QT_MAX_DIAS_EMPRE_BIBLI.Value.ToString() : "";

            ddlGeraNumEmpreAuto.SelectedValue = tb149.FL_NU_EMPRE_AUTO_BIBLI != null ? tb149.FL_NU_EMPRE_AUTO_BIBLI : "N";
            txtNumIniciEmpreAuto.Enabled = ddlGeraNumEmpreAuto.Enabled = false;
            txtNumIniciEmpreAuto.Text = tb149.NU_EMPRE_INICI_BIBLI != null ? tb149.NU_EMPRE_INICI_BIBLI.Value.ToString() : "";

            ddlFlagTaxaEmpre.SelectedValue = tb149.FL_TAXA_EMPRE_BIBLI != null ? tb149.FL_TAXA_EMPRE_BIBLI : "N";
            txtDiasBonusTaxaEmpre.Enabled = txtValorTaxaEmpre.Enabled = ddlFlagTaxaEmpre.Enabled = false;
            txtDiasBonusTaxaEmpre.Text = tb149.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI != null ? tb149.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI.Value.ToString() : "";
            txtValorTaxaEmpre.Text = tb149.VL_TAXA_DIA_EMPRE_BIBLI != null ? tb149.VL_TAXA_DIA_EMPRE_BIBLI.Value.ToString() : "";
            ddlFlagMultaAtraso.SelectedValue = tb149.FL_MULTA_EMPRE_BIBLI != null ? tb149.FL_MULTA_EMPRE_BIBLI : "N";
            txtDiasBonusMultaEmpre.Enabled = txtValorMultaEmpre.Enabled = ddlFlagMultaAtraso.Enabled = false;
            txtDiasBonusMultaEmpre.Text = tb149.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI != null ? tb149.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI.Value.ToString() : "";
            txtValorMultaEmpre.Text = tb149.VL_MULTA_DIA_ATRASO_BIBLI != null ? tb149.VL_MULTA_DIA_ATRASO_BIBLI.Value.ToString() : "";
            ddlGeraNumItemAuto.SelectedValue = tb149.FL_NU_SOLIC_AUTO_BIBLI != null ? tb149.FL_NU_SOLIC_AUTO_BIBLI : "N";
            txtNumIniciItemAuto.Enabled = ddlGeraNumItemAuto.Enabled = ddlNumISBNObrig.Enabled = ddlFlarIncluContaRecebBibli.Enabled = false;
            txtNumIniciItemAuto.Text = tb149.NU_SOLIC_INICI_BIBLI != null ? tb149.NU_SOLIC_INICI_BIBLI.Value.ToString() : "";
            ddlNumISBNObrig.SelectedValue = tb149.FL_NU_ISBN_OBRIG_BIBLI != null ? tb149.FL_NU_ISBN_OBRIG_BIBLI : "N";
            ddlFlarIncluContaRecebBibli.SelectedValue = tb149.FL_INCLU_TAXA_CAR_BIBLI != null ? tb149.FL_INCLU_TAXA_CAR_BIBLI : "N";
            chkUsuarFuncBibli.Enabled = chkUsuarProfBibli.Enabled = txtIdadeMinimAlunoBibli.Enabled = chkUsuarAlunoBibli.Enabled =
            chkUsuarRespBibli.Enabled = chkUsuarOutroBibli.Enabled = false;
            chkUsuarFuncBibli.Checked = tb149.FL_USUAR_FUNCI_BIBLI == "S";
            chkUsuarProfBibli.Checked = tb149.FL_USUAR_PROFE_BIBLI == "S";
            chkUsuarAlunoBibli.Checked = tb149.FL_USUAR_ALUNO_BIBLI == "S";            
            txtIdadeMinimAlunoBibli.Text = tb149.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI != null ? tb149.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI.Value.ToString() : "";
            chkUsuarRespBibli.Checked = tb149.FL_USUAR_RESPO_BIBLI == "S";
            chkUsuarOutroBibli.Checked = tb149.FL_USUAR_OUTRO_BIBLI == "S";
            txtHorarIniT1Bibli.Enabled = txtHorarFimT1Bibli.Enabled = txtHorarIniT2Bibli.Enabled = txtHorarFimT2Bibli.Enabled =
            txtHorarIniT3Bibli.Enabled = txtHorarFimT3Bibli.Enabled = txtHorarIniT4Bibli.Enabled = txtHorarFimT4Bibli.Enabled = false;

            chkHorAtiBibli.Enabled = false;
            if (chkHorAtiBibli.Checked)
            {
                txtHorarIniT1Bibli.Text = txtHoraIniTurno1.Text;
                txtHorarFimT1Bibli.Text = txtHoraFimTurno1.Text;
                txtHorarIniT2Bibli.Text = txtHoraIniTurno2.Text;
                txtHorarFimT2Bibli.Text = txtHoraFimTurno2.Text;
                txtHorarIniT3Bibli.Text = txtHoraIniTurno3.Text;
                txtHorarFimT3Bibli.Text = txtHoraFimTurno3.Text;
                txtHorarIniT4Bibli.Text = txtHoraIniTurno4.Text;
                txtHorarFimT4Bibli.Text = txtHoraFimTurno4.Text;
            }
            else
            {
                txtHorarIniT1Bibli.Text = tb149.HR_INI_TURNO1_BIBLI != null ? tb149.HR_INI_TURNO1_BIBLI : "";
                txtHorarFimT1Bibli.Text = tb149.HR_FIM_TURNO1_BIBLI != null ? tb149.HR_FIM_TURNO1_BIBLI : "";
                txtHorarIniT2Bibli.Text = tb149.HR_INI_TURNO2_BIBLI != null ? tb149.HR_INI_TURNO2_BIBLI : "";
                txtHorarFimT2Bibli.Text = tb149.HR_FIM_TURNO2_BIBLI != null ? tb149.HR_FIM_TURNO2_BIBLI : "";
                txtHorarIniT3Bibli.Text = tb149.HR_INI_TURNO3_BIBLI != null ? tb149.HR_INI_TURNO3_BIBLI : "";
                txtHorarFimT3Bibli.Text = tb149.HR_FIM_TURNO3_BIBLI != null ? tb149.HR_FIM_TURNO3_BIBLI : "";
                txtHorarIniT4Bibli.Text = tb149.HR_INI_TURNO4_BIBLI != null ? tb149.HR_INI_TURNO4_BIBLI : "";
                txtHorarFimT4Bibli.Text = tb149.HR_FIM_TURNO4_BIBLI != null ? tb149.HR_FIM_TURNO4_BIBLI : "";
            }   
            
            ddlSiglaUnidBibliEscol1.Enabled = ddlNomeBibliEscol1.Enabled = ddlClassifBibli1.Enabled =
            ddlSiglaUnidBibliEscol2.Enabled = ddlNomeBibliEscol2.Enabled = ddlClassifBibli2.Enabled = false;
//--------> Bibliotecário(a) 1
            if (tb149.TB03_COLABOR1 != null)
            {
                ddlSiglaUnidBibliEscol1.SelectedValue = tb149.TB03_COLABOR1.CO_EMP.ToString();
                CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol1, ddlNomeBibliEscol1);
                ddlNomeBibliEscol1.SelectedValue = tb149.TB03_COLABOR1.CO_COL.ToString();
                ddlClassifBibli1.SelectedValue = tb149.CO_CLASS_BIBLI1 != null ? tb149.CO_CLASS_BIBLI1.ToString() : "1";
            }

//--------> Bibliotecário(a) 2
            if (tb149.TB03_COLABOR4 != null)
            {
                ddlSiglaUnidBibliEscol2.SelectedValue = tb149.TB03_COLABOR4.CO_EMP.ToString();
                CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol2, ddlNomeBibliEscol2);
                ddlNomeBibliEscol2.SelectedValue = tb149.TB03_COLABOR4.CO_COL.ToString();
                ddlClassifBibli2.SelectedValue = tb149.CO_CLASS_BIBLI2 != null ? tb149.CO_CLASS_BIBLI2.ToString() : "2";
            }
        }

        /// <summary>
        /// Método que carrega dados de Biblioteca Escolar de acordo com a Unidade informada
        /// </summary>
        /// <param name="tb25">Entidade TB25_EMPRESA</param>
        protected void CarregaDadosBibliotecaEscolar(TB25_EMPRESA tb25)
        {
            tb25.TB83_PARAMETRO.TB03_COLABOR1Reference.Load();
            tb25.TB83_PARAMETRO.TB03_COLABOR4Reference.Load();

            txtTipoCtrlBibliEscol.Text = "Unidade Escolar";
            //Verificar se é para criar um novo campo ou alterar esse!
            ddlFlagReserBibli.SelectedValue = tb25.TB83_PARAMETRO.FLA_RESER_OUTRA_UNID != null ? tb25.TB83_PARAMETRO.FLA_RESER_OUTRA_UNID : "N";
            txtQtdeItensReser.Enabled = txtQtdeMaxDiasReser.Enabled = ddlFlagReserBibli.SelectedValue == "S";
            txtQtdeItensReser.Text = tb25.TB83_PARAMETRO.QT_ITENS_ALUNO_BIBLI != null ? tb25.TB83_PARAMETRO.QT_ITENS_ALUNO_BIBLI.Value.ToString() : "";
            txtQtdeMaxDiasReser.Text = tb25.TB83_PARAMETRO.QT_DIAS_RESER_BIBLI != null ? tb25.TB83_PARAMETRO.QT_DIAS_RESER_BIBLI.Value.ToString() : "";
            //
            ddlFlagEmpreBibli.SelectedValue = tb25.TB83_PARAMETRO.FL_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.FL_EMPRE_BIBLI : "N";
            txtQtdeItensEmpre.Enabled = txtQtdeMaxDiasEmpre.Enabled = ddlFlagEmpreBibli.SelectedValue == "S";
            txtQtdeItensEmpre.Text = tb25.TB83_PARAMETRO.QT_ITENS_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.QT_ITENS_EMPRE_BIBLI.Value.ToString() : "";
            txtQtdeMaxDiasEmpre.Text = tb25.TB83_PARAMETRO.QT_MAX_DIAS_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.QT_MAX_DIAS_EMPRE_BIBLI.Value.ToString() : "";

            ddlGeraNumEmpreAuto.Enabled = true;
            ddlGeraNumEmpreAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_NU_EMPRE_AUTO_BIBLI != null ? tb25.TB83_PARAMETRO.FL_NU_EMPRE_AUTO_BIBLI : "N";
            txtNumIniciEmpreAuto.Enabled = ddlGeraNumEmpreAuto.SelectedValue == "S";
            txtNumIniciEmpreAuto.Text = tb25.TB83_PARAMETRO.NU_EMPRE_INICI_BIBLI != null ? tb25.TB83_PARAMETRO.NU_EMPRE_INICI_BIBLI.Value.ToString() : "";
            
            ddlFlagTaxaEmpre.SelectedValue = tb25.TB83_PARAMETRO.FL_TAXA_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.FL_TAXA_EMPRE_BIBLI : "N";
            txtDiasBonusTaxaEmpre.Enabled = txtValorTaxaEmpre.Enabled = ddlFlagTaxaEmpre.SelectedValue == "S";
            txtDiasBonusTaxaEmpre.Text = tb25.TB83_PARAMETRO.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI.Value.ToString() : "";
            txtValorTaxaEmpre.Text = tb25.TB83_PARAMETRO.VL_TAXA_DIA_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.VL_TAXA_DIA_EMPRE_BIBLI.Value.ToString() : "";
            ddlFlagMultaAtraso.SelectedValue = tb25.TB83_PARAMETRO.FL_MULTA_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.FL_MULTA_EMPRE_BIBLI : "N";
            txtDiasBonusMultaEmpre.Enabled = txtValorMultaEmpre.Enabled = ddlFlagMultaAtraso.SelectedValue == "S";
            txtDiasBonusMultaEmpre.Text = tb25.TB83_PARAMETRO.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI != null ? tb25.TB83_PARAMETRO.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI.Value.ToString() : "";
            txtValorMultaEmpre.Text = tb25.TB83_PARAMETRO.VL_MULTA_DIA_ATRASO_BIBLI != null ? tb25.TB83_PARAMETRO.VL_MULTA_DIA_ATRASO_BIBLI.Value.ToString() : "";
            ddlGeraNumItemAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_NU_SOLIC_AUTO_BIBLI != null ? tb25.TB83_PARAMETRO.FL_NU_SOLIC_AUTO_BIBLI : "N";
            txtNumIniciItemAuto.Enabled = ddlGeraNumItemAuto.SelectedValue == "S";
            txtNumIniciItemAuto.Text = tb25.TB83_PARAMETRO.NU_SOLIC_INICI_BIBLI != null ? tb25.TB83_PARAMETRO.NU_SOLIC_INICI_BIBLI.Value.ToString() : "";
            ddlNumISBNObrig.SelectedValue = tb25.TB83_PARAMETRO.FL_NU_ISBN_OBRIG_BIBLI != null ? tb25.TB83_PARAMETRO.FL_NU_ISBN_OBRIG_BIBLI : "N";
            ddlFlarIncluContaRecebBibli.SelectedValue = tb25.TB83_PARAMETRO.FL_INCLU_TAXA_CAR_BIBLI != null ? tb25.TB83_PARAMETRO.FL_INCLU_TAXA_CAR_BIBLI : "N";
            chkUsuarFuncBibli.Checked = tb25.TB83_PARAMETRO.FL_USUAR_FUNCI_BIBLI == "S";
            chkUsuarProfBibli.Checked = tb25.TB83_PARAMETRO.FL_USUAR_PROFE_BIBLI == "S";
            chkUsuarAlunoBibli.Checked = tb25.TB83_PARAMETRO.FL_USUAR_ALUNO_BIBLI == "S";
            txtIdadeMinimAlunoBibli.Enabled = chkUsuarAlunoBibli.Checked;
            txtIdadeMinimAlunoBibli.Text = tb25.TB83_PARAMETRO.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI != null ? tb25.TB83_PARAMETRO.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI.Value.ToString() : "";
            chkUsuarRespBibli.Checked = tb25.TB83_PARAMETRO.FL_USUAR_RESPO_BIBLI == "S";
            chkUsuarOutroBibli.Checked = tb25.TB83_PARAMETRO.FL_USUAR_OUTRO_BIBLI == "S";

            chkHorAtiBibli.Enabled = true;
            chkHorAtiBibli.Checked = tb25.TB83_PARAMETRO.FL_HORAR_BIBLI_IGUAL_UNID == "S";
            if (chkHorAtiBibli.Checked)
            {
                txtHorarIniT1Bibli.Enabled = txtHorarFimT1Bibli.Enabled = txtHorarIniT2Bibli.Enabled = txtHorarFimT2Bibli.Enabled =
                txtHorarIniT3Bibli.Enabled = txtHorarFimT3Bibli.Enabled = txtHorarIniT4Bibli.Enabled = txtHorarFimT4Bibli.Enabled = false;
                txtHorarIniT1Bibli.Text = txtHoraIniTurno1.Text;
                txtHorarFimT1Bibli.Text = txtHoraFimTurno1.Text;
                txtHorarIniT2Bibli.Text = txtHoraIniTurno2.Text;
                txtHorarFimT2Bibli.Text = txtHoraFimTurno2.Text;
                txtHorarIniT3Bibli.Text = txtHoraIniTurno3.Text;
                txtHorarFimT3Bibli.Text = txtHoraFimTurno3.Text;
                txtHorarIniT4Bibli.Text = txtHoraIniTurno4.Text;
                txtHorarFimT4Bibli.Text = txtHoraFimTurno4.Text;
            }
            else
            {
                txtHorarIniT1Bibli.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO1_BIBLI != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO1_BIBLI : "";
                txtHorarFimT1Bibli.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO1_BIBLI != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO1_BIBLI : "";
                txtHorarIniT2Bibli.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO2_BIBLI != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO2_BIBLI : "";
                txtHorarFimT2Bibli.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO2_BIBLI != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO2_BIBLI : "";
                txtHorarIniT3Bibli.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO3_BIBLI != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO3_BIBLI : "";
                txtHorarFimT3Bibli.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO3_BIBLI != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO3_BIBLI : "";
                txtHorarIniT4Bibli.Text = tb25.TB83_PARAMETRO.HR_INI_TURNO4_BIBLI != null ? tb25.TB83_PARAMETRO.HR_INI_TURNO4_BIBLI : "";
                txtHorarFimT4Bibli.Text = tb25.TB83_PARAMETRO.HR_FIM_TURNO4_BIBLI != null ? tb25.TB83_PARAMETRO.HR_FIM_TURNO4_BIBLI : ""; 
            }         
            
//--------> Bibliotecário(a) 1
            if (tb25.TB83_PARAMETRO.TB03_COLABOR1 != null)
            {
                ddlSiglaUnidBibliEscol1.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR1.CO_EMP.ToString();
                CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol1, ddlNomeBibliEscol1);
                ddlNomeBibliEscol1.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR1.CO_COL.ToString();
                ddlClassifBibli1.SelectedValue = tb25.TB83_PARAMETRO.CO_CLASS_BIBLI1 != null ? tb25.TB83_PARAMETRO.CO_CLASS_BIBLI1.ToString() : "1";
            }

//--------> Bibliotecário(a) 2
            if (tb25.TB83_PARAMETRO.TB03_COLABOR4 != null)
            {
                ddlSiglaUnidBibliEscol2.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR4.CO_EMP.ToString();
                CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol2, ddlNomeBibliEscol2);
                ddlNomeBibliEscol2.SelectedValue = tb25.TB83_PARAMETRO.TB03_COLABOR4.CO_COL.ToString();
                ddlClassifBibli2.SelectedValue = tb25.TB83_PARAMETRO.CO_CLASS_BIBLI2 != null ? tb25.TB83_PARAMETRO.CO_CLASS_BIBLI2.ToString() : "2";
            }
        }

        /// <summary>
        /// Método que carrega dados de Mensagens SMS de acordo com a Instituição informada
        /// </summary>
        /// <param name="tb149">Entidade TB149_PARAM_INSTI</param>
        protected void CarregaDadosMensagensSMS(TB149_PARAM_INSTI tb149)
        {
            txtTipoCtrlMensagSMS.Text = "Instituição de Ensino";
            chkSMSSolicSecreEscol.Checked = tb149.FL_SMS_SECRE_SOLIC == "S";
            ddlFlagSMSSolicEnvAuto.Enabled = txtMsgSMSSolic.Enabled = chkSMSSolicSecreEscol.Enabled = false;
            ddlFlagSMSSolicEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_SECRE_SOLIC != null ? tb149.FL_ENVIO_AUTO_SMS_SECRE_SOLIC : "N";
            txtMsgSMSSolic.Text = tb149.DES_SMS_SECRE_SOLIC != null ? tb149.DES_SMS_SECRE_SOLIC : "";
            chkSMSEntreSecreEscol.Checked = tb149.FL_SMS_SECRE_ENTRE == "S";
            ddlFlagSMSEntreEnvAuto.Enabled = txtMsgSMSEntre.Enabled = chkSMSEntreSecreEscol.Enabled = false;
            ddlFlagSMSEntreEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_SECRE_ENTRE != null ? tb149.FL_ENVIO_AUTO_SMS_SECRE_ENTRE : "N";
            txtMsgSMSEntre.Text = tb149.DES_SMS_SECRE_ENTRE != null ? tb149.DES_SMS_SECRE_ENTRE : "";
            chkSMSOutroSecreEscol.Checked = tb149.FL_SMS_SECRE_OUTRO == "S";
            ddlFlagSMSOutroEnvAuto.Enabled = txtMsgSMSOutro.Enabled = chkSMSOutroSecreEscol.Enabled = false;
            ddlFlagSMSOutroEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_SECRE_OUTRO != null ? tb149.FL_ENVIO_AUTO_SMS_SECRE_OUTRO : "N";
            txtMsgSMSOutro.Text = tb149.DES_SMS_SECRE_OUTRO != null ? tb149.DES_SMS_SECRE_OUTRO : "";
            chkSMSReserVagas.Checked = tb149.FL_SMS_MATRI_RESER == "S";
            ddlFlagSMSReserVagasEnvAuto.Enabled = txtMsgSMSReserVagas.Enabled = chkSMSReserVagas.Enabled = false;
            ddlFlagSMSReserVagasEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_MATRI_RESER != null ? tb149.FL_ENVIO_AUTO_SMS_MATRI_RESER : "N";
            txtMsgSMSReserVagas.Text = tb149.DES_SMS_MATRI_RESER != null ? tb149.DES_SMS_MATRI_RESER : "";
            chkSMSRenovMatri.Checked = tb149.FL_SMS_MATRI_RENOV == "S";
            ddlFlagSMSRenovMatriEnvAuto.Enabled = txtMsgSMSRenovMatri.Enabled = chkSMSRenovMatri.Enabled = false;
            ddlFlagSMSRenovMatriEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_MATRI_RENOV != null ? tb149.FL_ENVIO_AUTO_SMS_MATRI_RENOV : "N";
            txtMsgSMSRenovMatri.Text = tb149.DES_SMS_MATRI_RENOV != null ? tb149.DES_SMS_MATRI_RENOV : "";
            chkSMSMatriNova.Checked = tb149.FL_SMS_MATRI_NOVA == "S";
            ddlFlagSMSMatriNovaEnvAuto.Enabled = txtMsgSMSMatriNova.Enabled = chkSMSMatriNova.Enabled = false;
            ddlFlagSMSMatriNovaEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_MATRI_NOVA != null ? tb149.FL_ENVIO_AUTO_SMS_MATRI_NOVA : "N";
            txtMsgSMSMatriNova.Text = tb149.DES_SMS_MATRI_NOVA != null ? tb149.DES_SMS_MATRI_NOVA : "";
            chkSMSReserBibli.Checked = tb149.FL_SMS_BIBLI_RESER == "S";
            ddlFlagSMSReserBibliEnvAuto.Enabled = txtMsgSMSReserBibli.Enabled = chkSMSReserBibli.Enabled = false;
            ddlFlagSMSReserBibliEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_BIBLI_RESER != null ? tb149.FL_ENVIO_AUTO_SMS_BIBLI_RESER : "N";
            txtMsgSMSReserBibli.Text = tb149.DES_SMS_BIBLI_RESER != null ? tb149.DES_SMS_BIBLI_RESER : "";
            chkSMSEmpreBibli.Checked = tb149.FL_SMS_BIBLI_EMPRE == "S";
            ddlFlagSMSEmpreBibliEnvAuto.Enabled = txtMsgSMSEmpreBibli.Enabled = chkSMSEmpreBibli.Enabled = false;
            ddlFlagSMSEmpreBibliEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE != null ? tb149.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE : "N";
            txtMsgSMSEmpreBibli.Text = tb149.DES_SMS_BIBLI_EMPRE != null ? tb149.DES_SMS_BIBLI_EMPRE : "";
            chkSMSDiverBibli.Checked = tb149.FL_SMS_BIBLI_DIVER == "S";
            ddlFlagSMSDiverBibliEnvAuto.Enabled = txtMsgSMSDiverBibli.Enabled = chkSMSDiverBibli.Enabled = false;
            ddlFlagSMSDiverBibliEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_BIBLI_DIVER != null ? tb149.FL_ENVIO_AUTO_SMS_BIBLI_DIVER : "N";
            txtMsgSMSDiverBibli.Text = tb149.DES_SMS_BIBLI_DIVER != null ? tb149.DES_SMS_BIBLI_DIVER : "";
            chkSMSFaltaAluno.Checked = tb149.FL_SMS_FALTA_ALUNO == "S";
            ddlFlagSMSFaltaAlunoEnvAuto.Enabled = txtMsgSMSFaltaAluno.Enabled = chkSMSFaltaAluno.Enabled = false;
            ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue = tb149.FL_ENVIO_AUTO_SMS_FALTA_ALUNO != null ? tb149.FL_ENVIO_AUTO_SMS_FALTA_ALUNO : "N";
            txtMsgSMSFaltaAluno.Text = tb149.DES_SMS_FALTA_ALUNO != null ? tb149.DES_SMS_FALTA_ALUNO : "";  

           
           
           
        }

        /// <summary>
        /// Método que carrega dados de Mensagens SMS de acordo com a Unidade informada
        /// </summary>
        /// <param name="tb25">Entidade TB25_EMPRESA</param>
        protected void CarregaDadosMensagensSMS(TB25_EMPRESA tb25)
        {
            txtTipoCtrlMensagSMS.Text = "Unidade Escolar";
            chkSMSSolicSecreEscol.Checked = tb25.TB83_PARAMETRO.FL_SMS_SECRE_SOLIC == "S";
            ddlFlagSMSSolicEnvAuto.Enabled = txtMsgSMSSolic.Enabled = chkSMSSolicSecreEscol.Checked;
            ddlFlagSMSSolicEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_SOLIC != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_SOLIC : "N";
            txtMsgSMSSolic.Text = tb25.TB83_PARAMETRO.DES_SMS_SECRE_SOLIC != null ? tb25.TB83_PARAMETRO.DES_SMS_SECRE_SOLIC : "";
            chkSMSEntreSecreEscol.Checked = tb25.TB83_PARAMETRO.FL_SMS_SECRE_ENTRE == "S";
            ddlFlagSMSEntreEnvAuto.Enabled = txtMsgSMSEntre.Enabled = chkSMSEntreSecreEscol.Checked;
            ddlFlagSMSEntreEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_ENTRE != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_ENTRE : "N";
            txtMsgSMSEntre.Text = tb25.TB83_PARAMETRO.DES_SMS_SECRE_ENTRE != null ? tb25.TB83_PARAMETRO.DES_SMS_SECRE_ENTRE : "";
            chkSMSOutroSecreEscol.Checked = tb25.TB83_PARAMETRO.FL_SMS_SECRE_OUTRO == "S";
            ddlFlagSMSOutroEnvAuto.Enabled = txtMsgSMSOutro.Enabled = chkSMSOutroSecreEscol.Checked;
            ddlFlagSMSOutroEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_OUTRO != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_SECRE_OUTRO : "N";
            txtMsgSMSOutro.Text = tb25.TB83_PARAMETRO.DES_SMS_SECRE_OUTRO != null ? tb25.TB83_PARAMETRO.DES_SMS_SECRE_OUTRO : "";
            chkSMSReserVagas.Checked = tb25.TB83_PARAMETRO.FL_SMS_MATRI_RESER == "S";
            ddlFlagSMSReserVagasEnvAuto.Enabled = txtMsgSMSReserVagas.Enabled = chkSMSReserVagas.Checked;
            ddlFlagSMSReserVagasEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_RESER != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_RESER : "N";
            txtMsgSMSReserVagas.Text = tb25.TB83_PARAMETRO.DES_SMS_MATRI_RESER != null ? tb25.TB83_PARAMETRO.DES_SMS_MATRI_RESER : "";
            chkSMSRenovMatri.Checked = tb25.TB83_PARAMETRO.FL_SMS_MATRI_RENOV == "S";
            ddlFlagSMSRenovMatriEnvAuto.Enabled = txtMsgSMSRenovMatri.Enabled = chkSMSRenovMatri.Checked;
            ddlFlagSMSRenovMatriEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_RENOV != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_RENOV : "N";
            txtMsgSMSRenovMatri.Text = tb25.TB83_PARAMETRO.DES_SMS_MATRI_RENOV != null ? tb25.TB83_PARAMETRO.DES_SMS_MATRI_RENOV : "";
            chkSMSMatriNova.Checked = tb25.TB83_PARAMETRO.FL_SMS_MATRI_NOVA == "S";
            ddlFlagSMSMatriNovaEnvAuto.Enabled = txtMsgSMSMatriNova.Enabled = chkSMSMatriNova.Checked;
            ddlFlagSMSMatriNovaEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_NOVA != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_MATRI_NOVA : "N";
            txtMsgSMSMatriNova.Text = tb25.TB83_PARAMETRO.DES_SMS_MATRI_NOVA != null ? tb25.TB83_PARAMETRO.DES_SMS_MATRI_NOVA : "";
            chkSMSReserBibli.Checked = tb25.TB83_PARAMETRO.FL_SMS_BIBLI_RESER == "S";
            ddlFlagSMSReserBibliEnvAuto.Enabled = txtMsgSMSReserBibli.Enabled = chkSMSReserBibli.Checked;
            ddlFlagSMSReserBibliEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_RESER != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_RESER : "N";
            txtMsgSMSReserBibli.Text = tb25.TB83_PARAMETRO.DES_SMS_BIBLI_RESER != null ? tb25.TB83_PARAMETRO.DES_SMS_BIBLI_RESER : "";
            chkSMSEmpreBibli.Checked = tb25.TB83_PARAMETRO.FL_SMS_BIBLI_EMPRE == "S";
            ddlFlagSMSEmpreBibliEnvAuto.Enabled = txtMsgSMSEmpreBibli.Enabled = chkSMSEmpreBibli.Checked;
            ddlFlagSMSEmpreBibliEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE : "N";
            txtMsgSMSEmpreBibli.Text = tb25.TB83_PARAMETRO.DES_SMS_BIBLI_EMPRE != null ? tb25.TB83_PARAMETRO.DES_SMS_BIBLI_EMPRE : "";
            chkSMSDiverBibli.Checked = tb25.TB83_PARAMETRO.FL_SMS_BIBLI_DIVER == "S";
            ddlFlagSMSDiverBibliEnvAuto.Enabled = txtMsgSMSDiverBibli.Enabled = chkSMSDiverBibli.Checked;
            ddlFlagSMSDiverBibliEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_DIVER != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_BIBLI_DIVER : "N";
            txtMsgSMSDiverBibli.Text = tb25.TB83_PARAMETRO.DES_SMS_BIBLI_DIVER != null ? tb25.TB83_PARAMETRO.DES_SMS_BIBLI_DIVER : "";
            chkSMSFaltaAluno.Checked = tb25.TB83_PARAMETRO.FL_SMS_FALTA_ALUNO == "S";
            ddlFlagSMSFaltaAlunoEnvAuto.Enabled = txtMsgSMSFaltaAluno.Enabled = chkSMSFaltaAluno.Checked;
            ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue = tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_FALTA_ALUNO != null ? tb25.TB83_PARAMETRO.FL_ENVIO_AUTO_SMS_FALTA_ALUNO : "N";
            txtMsgSMSFaltaAluno.Text = tb25.TB83_PARAMETRO.DES_SMS_FALTA_ALUNO != null ? tb25.TB83_PARAMETRO.DES_SMS_FALTA_ALUNO : "";  
        }

        /// <summary>
        /// Método que carrega dados de Controle Contabil de acordo com a Instituição informada
        /// </summary>
        /// <param name="tb149">Entidade TB149_PARAM_INSTI</param>
        protected void CarregaDadosContaContabil(TB149_PARAM_INSTI tb149)
        {
            txtTipoCtrlContabil.Text = "Instituição de Ensino";
            ddlGrupoTxServSecre.Enabled = ddlSubGrupoTxServSecre.Enabled = ddlSubGrupo2TxServSecre.Enabled = ddlContaContabilTxServSecre.Enabled = ddlCentroCustoTxServSecre.Enabled =
            ddlGrupoTxServBibli.Enabled = ddlSubGrupoTxServBibli.Enabled = ddlSubGrupo2TxServBibli.Enabled = ddlContaContabilTxServBibli.Enabled = ddlCentroCustoTxServBibli.Enabled =
            ddlGrupoTxMatri.Enabled = ddlSubGrupoTxMatri.Enabled = ddlSubGrupo2TxMatri.Enabled = ddlContaContabilTxMatri.Enabled = ddlCentroCustoTxMatri.Enabled =
            ddlGrupoContaCaixa.Enabled = ddlSubGrupoContaCaixa.Enabled = ddlSubGrupo2ContaCaixa.Enabled = ddlContaContabilContaCaixa.Enabled = ddlCentroCustoContaCaixa.Enabled =
            ddlGrupoAtiviExtra.Enabled = ddlSubGrupoAtiviExtra.Enabled = ddlSubGrupo2AtiviExtra.Enabled = ddlContaContabilAtiviExtra.Enabled = ddlCentroCustoAtiviExtra.Enabled =
            ddlGrupoContaBanco.Enabled = ddlSubGrupoContaBanco.Enabled = ddlSubGrupo2ContaBanco.Enabled = ddlContaContabilContaBanco.Enabled = ddlCentroCustoContaBanco.Enabled = false;

            TB56_PLANOCTA planoConta = new TB56_PLANOCTA();

            if (tb149.CO_CTSOL_EMP != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb149.CO_CTSOL_EMP.Value);
                planoConta.TB54_SGRP_CTAReference.Load();
                planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                planoConta.TB055_SGRP2_CTAReference.Load();

                ddlGrupoTxServSecre.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                CarregaSubGrupo(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre);
                ddlSubGrupoTxServSecre.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                CarregaSubGrupo2(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre);
                ddlSubGrupo2TxServSecre.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                CarregaConta(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre, ddlContaContabilTxServSecre);
                ddlContaContabilTxServSecre.SelectedValue = tb149.CO_CTSOL_EMP.Value.ToString();
                txtCtaContabTxServSecre.Text = planoConta.DE_CONTA_PC;
            }

            if (tb149.CO_CTABIB_EMP != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb149.CO_CTABIB_EMP.Value);
                planoConta.TB54_SGRP_CTAReference.Load();
                planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                planoConta.TB055_SGRP2_CTAReference.Load();

                ddlGrupoTxServBibli.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                CarregaSubGrupo(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli);
                ddlSubGrupoTxServBibli.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                CarregaSubGrupo2(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli);
                ddlSubGrupo2TxServBibli.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                CarregaConta(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli, ddlContaContabilTxServBibli);
                ddlContaContabilTxServBibli.SelectedValue = tb149.CO_CTABIB_EMP.Value.ToString();
                txtCtaContabTxServBibli.Text = planoConta.DE_CONTA_PC;
            }

            if (tb149.CO_CTAMAT_EMP != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb149.CO_CTAMAT_EMP.Value);
                planoConta.TB54_SGRP_CTAReference.Load();
                planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                planoConta.TB055_SGRP2_CTAReference.Load();

                ddlGrupoTxMatri.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                CarregaSubGrupo(ddlGrupoTxMatri, ddlSubGrupoTxMatri);
                ddlSubGrupoTxMatri.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                CarregaSubGrupo2(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri);
                ddlSubGrupo2TxMatri.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                CarregaConta(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri, ddlContaContabilTxMatri);
                ddlContaContabilTxMatri.SelectedValue = tb149.CO_CTAMAT_EMP.Value.ToString();
                txtCtaContabTxMatri.Text = planoConta.DE_CONTA_PC;
            }            
            
            if (tb149.CO_CTA_ATIVI_EXTRA != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb149.CO_CTA_ATIVI_EXTRA.Value);
                planoConta.TB54_SGRP_CTAReference.Load();
                planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                planoConta.TB055_SGRP2_CTAReference.Load();

                ddlGrupoAtiviExtra.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                CarregaSubGrupo(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra);
                ddlSubGrupoAtiviExtra.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                CarregaSubGrupo2(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra);
                ddlSubGrupo2AtiviExtra.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                CarregaConta(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra, ddlContaContabilAtiviExtra);
                ddlContaContabilAtiviExtra.SelectedValue = tb149.CO_CTA_ATIVI_EXTRA.Value.ToString();
                txtCtaContabAtiviExtra.Text = planoConta.DE_CONTA_PC;
            }
            
            if (tb149.CO_CTA_CAIXA != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb149.CO_CTA_CAIXA.Value);
                planoConta.TB54_SGRP_CTAReference.Load();
                planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                planoConta.TB055_SGRP2_CTAReference.Load();

                ddlGrupoContaCaixa.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                CarregaSubGrupo(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa);
                ddlSubGrupoContaCaixa.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                CarregaSubGrupo2(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa);
                ddlSubGrupo2ContaCaixa.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                CarregaConta(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa, ddlContaContabilContaCaixa);
                ddlContaContabilContaCaixa.SelectedValue = tb149.CO_CTA_CAIXA.Value.ToString();
                txtCtaContabCaixa.Text = planoConta.DE_CONTA_PC;
            }
            
            if (tb149.CO_CTA_BANCO != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb149.CO_CTA_BANCO.Value);
                planoConta.TB54_SGRP_CTAReference.Load();
                planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                planoConta.TB055_SGRP2_CTAReference.Load();

                ddlGrupoContaBanco.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                CarregaSubGrupo(ddlGrupoContaBanco, ddlSubGrupoContaBanco);
                ddlSubGrupoContaBanco.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                CarregaSubGrupo2(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco);
                ddlSubGrupo2ContaBanco.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                CarregaConta(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco, ddlContaContabilContaBanco);
                ddlContaContabilContaBanco.SelectedValue = tb149.CO_CTA_BANCO.Value.ToString();
                txtCtaContabBanco.Text = planoConta.DE_CONTA_PC;
            }


            ddlCentroCustoTxServSecre.Enabled = ddlCentroCustoTxServBibli.Enabled = ddlCentroCustoTxMatri.Enabled = ddlCentroCustoAtiviExtra.Enabled =
            ddlCentroCustoContaBanco.Enabled = ddlCentroCustoContaCaixa.Enabled = false;
            

            ddlCentroCustoTxServSecre.SelectedValue = tb149.CO_CENT_CUSSOL != null ? tb149.CO_CENT_CUSSOL.Value.ToString() : "";
            ddlCentroCustoTxServBibli.SelectedValue = tb149.CO_CENT_CUSBIB != null ? tb149.CO_CENT_CUSBIB.Value.ToString() : "";
            ddlCentroCustoTxMatri.SelectedValue = tb149.CO_CENT_CUSMAT != null ? tb149.CO_CENT_CUSMAT.Value.ToString() : "";            
            ddlCentroCustoAtiviExtra.SelectedValue = tb149.CO_CENT_CUSTO_ATIVI_EXTRA != null ? tb149.CO_CENT_CUSTO_ATIVI_EXTRA.Value.ToString() : "";
            ddlCentroCustoContaCaixa.SelectedValue = tb149.CO_CENT_CUSTO_CAIXA != null ? tb149.CO_CENT_CUSTO_CAIXA.Value.ToString() : "";
            ddlCentroCustoContaBanco.SelectedValue = tb149.CO_CENT_CUSTO_BANCO != null ? tb149.CO_CENT_CUSTO_BANCO.Value.ToString() : "";
        }

        /// <summary>
        /// Método que carrega dados de Controle Contabil de acordo com a Unidade informada
        /// </summary>
        /// <param name="tb25">Entidade TB25_EMPRESA</param>
        protected void CarregaDadosContaContabil(TB25_EMPRESA tb25)
        {
            txtTipoCtrlContabil.Text = "Unidade Escolar";

            TB56_PLANOCTA planoConta = new TB56_PLANOCTA();

            if (tb25.CO_CTSOL_EMP != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTSOL_EMP.Value);
                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    planoConta.TB055_SGRP2_CTAReference.Load();

                    ddlGrupoTxServSecre.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre);
                    ddlSubGrupoTxServSecre.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre);
                    ddlSubGrupo2TxServSecre.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre, ddlContaContabilTxServSecre);
                    ddlContaContabilTxServSecre.SelectedValue = tb25.CO_CTSOL_EMP.Value.ToString();
                    txtCtaContabTxServSecre.Text = planoConta.DE_CONTA_PC;
                }                
            }

            if (tb25.CO_CTABIB_EMP != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTABIB_EMP.Value);
                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    planoConta.TB055_SGRP2_CTAReference.Load();

                    ddlGrupoTxServBibli.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli);
                    ddlSubGrupoTxServBibli.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli);
                    ddlSubGrupo2TxServBibli.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli, ddlContaContabilTxServBibli);
                    ddlContaContabilTxServBibli.SelectedValue = tb25.CO_CTABIB_EMP.Value.ToString();
                    txtCtaContabTxServBibli.Text = planoConta.DE_CONTA_PC;
                }                
            }

            if (tb25.CO_CTAMAT_EMP != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTAMAT_EMP.Value);
                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    planoConta.TB055_SGRP2_CTAReference.Load();

                    ddlGrupoTxMatri.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo(ddlGrupoTxMatri, ddlSubGrupoTxMatri);
                    ddlSubGrupoTxMatri.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri);
                    ddlSubGrupo2TxMatri.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri, ddlContaContabilTxMatri);
                    ddlContaContabilTxMatri.SelectedValue = tb25.CO_CTAMAT_EMP.Value.ToString();
                    txtCtaContabTxMatri.Text = planoConta.DE_CONTA_PC;
                }                
            }            

            if (tb25.CO_CTA_ATIVI_EXTRA != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTA_ATIVI_EXTRA.Value);
                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    planoConta.TB055_SGRP2_CTAReference.Load();

                    ddlGrupoAtiviExtra.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra);
                    ddlSubGrupoAtiviExtra.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra);
                    ddlSubGrupo2AtiviExtra.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra, ddlContaContabilAtiviExtra);
                    ddlContaContabilAtiviExtra.SelectedValue = tb25.CO_CTA_ATIVI_EXTRA.Value.ToString();
                    txtCtaContabAtiviExtra.Text = planoConta.DE_CONTA_PC;
                }                
            }

            if (tb25.CO_CTA_CAIXA != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTA_CAIXA.Value);
                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    planoConta.TB055_SGRP2_CTAReference.Load();

                    ddlGrupoContaCaixa.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa);
                    ddlSubGrupoContaCaixa.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa);
                    ddlSubGrupo2ContaCaixa.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa, ddlContaContabilContaCaixa);
                    ddlContaContabilContaCaixa.SelectedValue = tb25.CO_CTA_CAIXA.Value.ToString();
                    txtCtaContabCaixa.Text = planoConta.DE_CONTA_PC;
                }                
            }

            if (tb25.CO_CTA_BANCO != null)
            {
                planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb25.CO_CTA_BANCO.Value);
                if (planoConta != null)
                {
                    planoConta.TB54_SGRP_CTAReference.Load();
                    planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();
                    planoConta.TB055_SGRP2_CTAReference.Load();

                    ddlGrupoContaBanco.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                    CarregaSubGrupo(ddlGrupoContaBanco, ddlSubGrupoContaBanco);
                    ddlSubGrupoContaBanco.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                    CarregaSubGrupo2(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco);
                    ddlSubGrupo2ContaBanco.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                    CarregaConta(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco, ddlContaContabilContaBanco);
                    ddlContaContabilContaBanco.SelectedValue = tb25.CO_CTA_BANCO.Value.ToString();
                    txtCtaContabBanco.Text = planoConta.DE_CONTA_PC;
                }                
            }

            ddlCentroCustoTxServSecre.SelectedValue = tb25.CO_CENT_CUSSOL != null ? tb25.CO_CENT_CUSSOL.Value.ToString() : "";
            ddlCentroCustoTxServBibli.SelectedValue = tb25.CO_CENT_CUSBIB != null ? tb25.CO_CENT_CUSBIB.Value.ToString() : "";
            ddlCentroCustoTxMatri.SelectedValue = tb25.CO_CENT_CUSMAT != null ? tb25.CO_CENT_CUSMAT.Value.ToString() : "";
            ddlCentroCustoAtiviExtra.SelectedValue = tb25.CO_CENT_CUSTO_ATIVI_EXTRA != null ? tb25.CO_CENT_CUSTO_ATIVI_EXTRA.Value.ToString() : "";
            ddlCentroCustoContaCaixa.SelectedValue = tb25.CO_CENT_CUSTO_CAIXA != null ? tb25.CO_CENT_CUSTO_CAIXA.Value.ToString() : "";
            ddlCentroCustoContaBanco.SelectedValue = tb25.CO_CENT_CUSTO_BANCO != null ? tb25.CO_CENT_CUSTO_BANCO.Value.ToString() : "";
        }

        /// <summary>
        /// Método que carrega dados de horário de funcionamento de acordo com a Unidade informada
        /// </summary>
        /// <param name="tb25">Entidade TB25_EMPRESA</param>
        protected void CarregaDadosHorFuncionamento(TB25_EMPRESA tb25)
        {
            txtHoraIniTurno1.Text = tb25.HR_FUNCI_MANHA_INIC;
            txtHoraFimTurno1.Text = tb25.HR_FUNCI_MANHA_FIM;
            txtHoraIniTurno2.Text = tb25.HR_FUNCI_TARDE_INIC;
            txtHoraFimTurno2.Text = tb25.HR_FUNCI_TARDE_FIM;
            txtHoraIniTurno3.Text = tb25.HR_FUNCI_NOITE_INIC;
            txtHoraFimTurno3.Text = tb25.HR_FUNCI_NOITE_FIM;
            txtHoraIniTurno4.Text = tb25.HR_FUNCI_ULTIM_TURNO_INIC;
            txtHoraFimTurno4.Text = tb25.HR_FUNCI_ULTIM_TURNO_FIM;
        }

        /// <summary>
        /// Método que habilita ou desabilita campos do Controle de Avaliação
        /// </summary>
        /// <param name="enable">Boolean de habilitação</param>
        private void HabilitaControleAvaliacao(bool enable)
        {
            ddlPerioAval.Enabled = txtMediaAprovGeral.Enabled =
            txtMediaAprovDireta.Enabled = txtMediaProvaFinal.Enabled = enable;
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Núcleo da Unidade
        /// </summary>
        private void CarregaNucleoUnidade()
        {
            ddlNucleoIUE.DataSource = (from tbNucleoInst in TB_NUCLEO_INST.RetornaTodosRegistros()
                                       select new { tbNucleoInst.CO_NUCLEO, tbNucleoInst.NO_SIGLA_NUCLEO }).OrderBy( n => n.NO_SIGLA_NUCLEO );

            ddlNucleoIUE.DataTextField = "NO_SIGLA_NUCLEO";
            ddlNucleoIUE.DataValueField = "CO_NUCLEO";
            ddlNucleoIUE.DataBind();

            ddlNucleoIUE.Items.Insert(0, "");   
        }

        /// <summary>
        /// Carrega os tipo de ensino disponíveis para as unidades
        /// </summary>
        private void CarregaTipoEnsino()
        {
            cblTipoEnsino.Items.Clear();
            cblTipoEnsino.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoEnsino.ResourceManager));
        }

        /// <summary>
        /// Método que carrega os dropdowns de Centro de Custo
        /// </summary>
        private void CarregaCentroCusto()
        {
            var resultado = (from lTb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                             where lTb099.TB14_DEPTO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { lTb099.NU_CTA_CENT_CUSTO, lTb099.DE_CENT_CUSTO, lTb099.CO_CENT_CUSTO}).ToList();

            var tb099 = (from result in resultado
                         select new
                         {
                             result.CO_CENT_CUSTO,
                             NU_CTA_CENT_CUSTO = string.Format("{0} - {1}", result.NU_CTA_CENT_CUSTO, result.DE_CENT_CUSTO)
                         }).OrderBy(r => r.NU_CTA_CENT_CUSTO);

            ddlCentroCustoTxServSecre.DataSource = tb099.OrderBy(c => c.NU_CTA_CENT_CUSTO);
            ddlCentroCustoTxServSecre.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCustoTxServSecre.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCustoTxServSecre.DataBind();

            ddlCentroCustoTxServBibli.DataSource = tb099.OrderBy(c => c.NU_CTA_CENT_CUSTO);
            ddlCentroCustoTxServBibli.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCustoTxServBibli.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCustoTxServBibli.DataBind();

            ddlCentroCustoTxMatri.DataSource = tb099.OrderBy(c => c.NU_CTA_CENT_CUSTO);
            ddlCentroCustoTxMatri.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCustoTxMatri.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCustoTxMatri.DataBind();            

            ddlCentroCustoAtiviExtra.DataSource = tb099.OrderBy(c => c.NU_CTA_CENT_CUSTO);
            ddlCentroCustoAtiviExtra.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCustoAtiviExtra.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCustoAtiviExtra.DataBind();

            ddlCentroCustoContaBanco.DataSource = tb099.OrderBy(c => c.NU_CTA_CENT_CUSTO);
            ddlCentroCustoContaBanco.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCustoContaBanco.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCustoContaBanco.DataBind();

            ddlCentroCustoContaCaixa.DataSource = tb099.OrderBy(c => c.NU_CTA_CENT_CUSTO);
            ddlCentroCustoContaCaixa.DataTextField = "NU_CTA_CENT_CUSTO";
            ddlCentroCustoContaCaixa.DataValueField = "CO_CENT_CUSTO";
            ddlCentroCustoContaCaixa.DataBind();


            ddlCentroCustoTxServSecre.Items.Insert(0, new ListItem("", ""));
            ddlCentroCustoTxServBibli.Items.Insert(0, new ListItem("", ""));
            ddlCentroCustoTxMatri.Items.Insert(0, new ListItem("", ""));     
            ddlCentroCustoAtiviExtra.Items.Insert(0, new ListItem("", ""));
            ddlCentroCustoContaBanco.Items.Insert(0, new ListItem("", ""));
            ddlCentroCustoContaCaixa.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUFs()
        {
            ddlUFIUE.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUFIUE.DataTextField = "CODUF";
            ddlUFIUE.DataValueField = "CODUF";
            ddlUFIUE.DataBind();
            ddlUFIUE.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;

            ddlUFIUE.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidadeIUE.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUFIUE.SelectedValue);

            ddlCidadeIUE.DataTextField = "NO_CIDADE";
            ddlCidadeIUE.DataValueField = "CO_CIDADE";
            ddlCidadeIUE.DataBind();

            ddlCidadeIUE.Enabled = ddlCidadeIUE.Items.Count > 0;
            ddlCidadeIUE.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            if (ddlCidadeIUE.SelectedValue == "")
            {
                ddlBairroIUE.Enabled = false;
                ddlBairroIUE.Items.Clear();
                ddlBairroIUE.Items.Insert(0, "");
                return;
            }
            else
            {
                int coCidade = int.Parse(ddlCidadeIUE.SelectedValue);

                ddlBairroIUE.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairroIUE.DataTextField = "NO_BAIRRO";
                ddlBairroIUE.DataValueField = "CO_BAIRRO";
                ddlBairroIUE.DataBind();

                ddlBairroIUE.Enabled = ddlBairroIUE.Items.Count > 0;
                ddlBairroIUE.Items.Insert(0, "");
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupos de Conta
        /// </summary>
        /// <param name="ddlGrupo">Dropdown de grupo de conta</param>
        private void CarregaTipo(DropDownList ddlGrupo)
        {
            var resultado = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                             where (tb53.TP_GRUP_CTA == "C" || tb53.TP_GRUP_CTA == "A") && tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { tb53.NR_GRUP_CTA, tb53.DE_GRUP_CTA, tb53.CO_GRUP_CTA }).ToList();

            ddlGrupo.DataSource = (from result in resultado
                                   select new
                                   {
                                      result.CO_GRUP_CTA, DE_GRUP_CTA = string.Format("{0} - {1}", result.NR_GRUP_CTA.Value.ToString("00"), result.DE_GRUP_CTA)
                                   }).OrderBy(r => r.DE_GRUP_CTA);   

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo de Conta
        /// </summary>
        /// <param name="ddlGrupo">DropDown de grupo de conta</param>
        /// <param name="ddlSubGrupo">DropDown de subgrupo de conta</param>
        private void CarregaSubGrupo(DropDownList ddlGrupo, DropDownList ddlSubGrupo)
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            var resultado = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                             where tb54.CO_GRUP_CTA == coGrupCta
                             select new { tb54.NR_SGRUP_CTA, tb54.DE_SGRUP_CTA, tb54.CO_SGRUP_CTA }).ToList();

            ddlSubGrupo.DataSource = (from result in resultado
                                      select new
                                      {
                                        result.CO_SGRUP_CTA,
                                        DE_SGRUP_CTA = string.Format("{0} - {1}", result.NR_SGRUP_CTA.Value.ToString("000"), result.DE_SGRUP_CTA)
                                      }).OrderBy(r => r.DE_SGRUP_CTA);            

            ddlSubGrupo.DataTextField = "DE_SGRUP_CTA";
            ddlSubGrupo.DataValueField = "CO_SGRUP_CTA";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo2 de Conta
        /// </summary>
        /// <param name="ddlGrupo">DropDown de grupo de conta</param>
        /// <param name="ddlSubGrupo">DropDown de subgrupo de conta</param>
        /// /// <param name="ddlSubGrupo2">DropDown de subgrupo de conta 2</param>
        private void CarregaSubGrupo2(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlSubGrupo2)
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            var result = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                          where tb055.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta 
                          && tb055.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
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
        /// Método que carrega o dropdown de Conta Contabil
        /// </summary>
        /// <param name="ddlGrupo">DropDown de grupo de conta</param>
        /// <param name="ddlSubGrupo">DropDown de subgrupo de conta</param>
        /// <param name="ddlContaContabil">DropDown de conta contábil</param>
        private void CarregaConta(DropDownList ddlGrupo, DropDownList ddlSubGrupo, DropDownList ddlSubGrupo2, DropDownList ddlContaContabil)
        {
            int coGrupCta = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSGrupCta = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coSGrupCta2 = ddlSubGrupo2.SelectedValue != "" ? int.Parse(ddlSubGrupo2.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta && tb56.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta
                             && (coSGrupCta2 != 0 ? tb56.TB055_SGRP2_CTA.CO_SGRUP2_CTA == coSGrupCta2 : 0 == 0)
                             select new { tb56.NU_CONTA_PC, tb56.DE_CONTA_PC, tb56.CO_SEQU_PC }).ToList();

            ddlContaContabil.DataSource = (from result in resultado
                                           select new
                                           {
                                               result.CO_SEQU_PC,
                                              DE_CONTA_PC = string.Format("{0} - {1}", result.NU_CONTA_PC.Value.ToString("0000"), result.DE_CONTA_PC)
                                           }).OrderBy(r => r.DE_CONTA_PC);  

            ddlContaContabil.DataTextField = "DE_CONTA_PC";
            ddlContaContabil.DataValueField = "CO_SEQU_PC";
            ddlContaContabil.DataBind();

            ddlContaContabil.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Siglas da Unidade do Secretário Escolar
        /// </summary>
        /// <param name="ddlCoEmp">DropDown de unidade do secretário</param>
        private void CarregaUnidadeNomeSecreEscol(DropDownList ddlCoEmp)
        {
            ddlCoEmp.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                 select new { tb25.CO_EMP, tb25.sigla }).OrderBy(e => e.sigla);

            ddlCoEmp.DataValueField = "CO_EMP";
            ddlCoEmp.DataTextField = "sigla";
            ddlCoEmp.DataBind();

            ddlCoEmp.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Siglas da Unidade do Bibliotecário Escolar
        /// </summary>
        /// <param name="ddlCoEmp">DropDown de unidade do bibliotecário</param>
        private void CarregaUnidadeNomeBibliEscol(DropDownList ddlCoEmp)
        {
            ddlCoEmp.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                 select new { tb25.CO_EMP, tb25.sigla }).OrderBy(e => e.sigla);

            ddlCoEmp.DataValueField = "CO_EMP";
            ddlCoEmp.DataTextField = "sigla";
            ddlCoEmp.DataBind();

            ddlCoEmp.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Nomes do Secretário Escolar
        /// </summary>
        /// <param name="ddlCoEmp">DropDown de unidade do secretário</param>
        /// <param name="ddlCoCol">DropDown de secretário</param>
        private void CarregaSecretarioEscolar(DropDownList ddlCoEmp, DropDownList ddlCoCol)
        {
            int coEmp = ddlCoEmp.SelectedValue != "" ? int.Parse(ddlCoEmp.SelectedValue) : 0;

            ddlCoCol.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                            select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlCoCol.DataValueField = "CO_COL";
            ddlCoCol.DataTextField = "NO_COL";
            ddlCoCol.DataBind();

            ddlCoCol.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Nomes do Bibliotecário Escolar
        /// </summary>
        /// <param name="ddlCoEmp">DropDown de unidade do bibliotecário</param>
        /// <param name="ddlCoCol">DropDown de bibliotecário</param>
        private void CarregaBibliotecarioEscolar(DropDownList ddlCoEmp, DropDownList ddlCoCol)
        {
            int coEmp = ddlCoEmp.SelectedValue != "" ? int.Parse(ddlCoEmp.SelectedValue) : 0;

            ddlCoCol.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                            select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlCoCol.DataValueField = "CO_COL";
            ddlCoCol.DataTextField = "NO_COL";
            ddlCoCol.DataBind();

            ddlCoCol.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega os dropdowns de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            AuxiliCarregamentos.CarregaBoletos(ddlBoletoMatric, globalCoEmp, "M", 0, 0, false, false);

            ddlBoletoMatric.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoRenovacao, globalCoEmp, "R", 0, 0, false, false);

            ddlBoletoRenovacao.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoMensalidade, globalCoEmp, "E", 0, 0, false, false);

            ddlBoletoMensalidade.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoAtiviExtra, globalCoEmp, "A", 0, 0, false, false);

            ddlBoletoAtiviExtra.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoBiblioteca, globalCoEmp, "B", 0, 0, false, false);
            
            ddlBoletoBiblioteca.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoServSecre, globalCoEmp, "S", 0, 0, false, false);
            
            ddlBoletoServSecre.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoServDiver, globalCoEmp, "D", 0, 0, false, false);

            ddlBoletoServDiver.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoNegociacao, globalCoEmp, "N", 0, 0, false, false);
            
            ddlBoletoNegociacao.Items.Insert(0, new ListItem("", ""));

            AuxiliCarregamentos.CarregaBoletos(ddlBoletoOutros, globalCoEmp, "O", 0, 0, false, false);
            
            ddlBoletoOutros.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários da Direção
        private void CarregaFuncionariosDirecao()
        {
            ddlFuncDir1.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncDir1.DataValueField = "CO_COL";
            ddlFuncDir1.DataTextField = "NO_COL";
            ddlFuncDir1.DataBind();

            ddlFuncDir1.Items.Insert(0, new ListItem("", ""));

            ddlFuncDir2.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncDir2.DataValueField = "CO_COL";
            ddlFuncDir2.DataTextField = "NO_COL";
            ddlFuncDir2.DataBind();

            ddlFuncDir2.Items.Insert(0, new ListItem("", ""));

            ddlFuncDir3.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncDir3.DataValueField = "CO_COL";
            ddlFuncDir3.DataTextField = "NO_COL";
            ddlFuncDir3.DataBind();

            ddlFuncDir3.Items.Insert(0, new ListItem("", ""));

            ddlFuncDir4.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncDir4.DataValueField = "CO_COL";
            ddlFuncDir4.DataTextField = "NO_COL";
            ddlFuncDir4.DataBind();

            ddlFuncDir4.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários da Coordenação
        private void CarregaFuncionariosCoordenacao()
        {
            ddlFuncCoord1.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncCoord1.DataValueField = "CO_COL";
            ddlFuncCoord1.DataTextField = "NO_COL";
            ddlFuncCoord1.DataBind();

            ddlFuncCoord1.Items.Insert(0, new ListItem("", ""));

            ddlFuncCoord2.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncCoord2.DataValueField = "CO_COL";
            ddlFuncCoord2.DataTextField = "NO_COL";
            ddlFuncCoord2.DataBind();

            ddlFuncCoord2.Items.Insert(0, new ListItem("", ""));

            ddlFuncCoord3.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncCoord3.DataValueField = "CO_COL";
            ddlFuncCoord3.DataTextField = "NO_COL";
            ddlFuncCoord3.DataBind();

            ddlFuncCoord3.Items.Insert(0, new ListItem("", ""));

            ddlFuncCoord4.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                      select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlFuncCoord4.DataValueField = "CO_COL";
            ddlFuncCoord4.DataTextField = "NO_COL";
            ddlFuncCoord4.DataBind();

            ddlFuncCoord4.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Siglas da Unidade do Bibliotecário Escolar
        /// </summary>
        /// <param name="ddlCoEmp">DropDown de unidade do bibliotecário</param>
        private void CarregaAgrupadoCAR(DropDownList ddlAgrupReceita)
        {
            ddlAgrupReceita.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                          select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP});

            ddlAgrupReceita.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupReceita.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupReceita.DataBind();

            ddlAgrupReceita.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregaBoletins()
        {
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            var bol = (from b in ADMBOLETIM.RetornaTodosRegistros()
                       where b.FL_STAT == "A"
                       select new Boletim
                       {
                           CO_BOL = b.CO_BOL,
                           NO_BOL = b.NO_BOL,
                           CHK_SEL = b.ADMUNIDBOLETIM.Where(r => r.TB25_EMPRESA.CO_EMP == coEmp && r.ADMBOLETIM.CO_BOL == b.CO_BOL).Any()
                       }).OrderBy(o => o.CO_BOL);

            divGrid.Visible = true;

            grdBoletim.DataKeyNames = new string[] { "CO_BOL" };

            grdBoletim.DataSource = bol;
            grdBoletim.DataBind();
        }

        public class Boletim
        {
            public int CO_BOL { get; set; }
            public bool CHK_SEL { get; set; }
            public string NO_BOL { get; set; }
        }

        #endregion

        #region Validações

        protected void cvDataReservaMatricula_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtReservaMatriculaDtInicioIUE.Text == "" && txtReservaMatriculaDtFimIUE.Text != "") ||
               (txtReservaMatriculaDtFimIUE.Text == "" && txtReservaMatriculaDtInicioIUE.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataRematricula_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtRematriculaInicioIUE.Text == "" && txtRematriculaFimIUE.Text != "") ||
               (txtRematriculaFimIUE.Text == "" && txtRematriculaInicioIUE.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataRemanAluno_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtDataRemanAlunoIni.Text == "" && txtDataRemanAlunoFim.Text != "") ||
               (txtDataRemanAlunoFim.Text == "" && txtDataRemanAlunoIni.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataTransInter_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtDtInicioTransInter.Text == "" && txtDtFimTransInter.Text != "") ||
               (txtDtFimTransInter.Text == "" && txtDtInicioTransInter.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataMatricula_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtMatriculaInicioIUE.Text == "" && txtMatriculaFimIUE.Text != "") ||
                (txtMatriculaFimIUE.Text == "" && txtMatriculaInicioIUE.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataTrancamentoMatricula_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtTrancamentoMatriculaInicioIUE.Text == "" && txtTrancamentoMatriculaFimIUE.Text != "") ||
               (txtTrancamentoMatriculaFimIUE.Text == "" && txtTrancamentoMatriculaInicioIUE.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataAlteracaoMatricula_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtAlteracaoMatriculaInicioIUE.Text == "" && txtAlteracaoMatriculaFimIUE.Text != "") ||
                 (txtAlteracaoMatriculaFimIUE.Text == "" && txtAlteracaoMatriculaInicioIUE.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvContaContabilTxServSecre_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoTxServSecre.SelectedValue != "")
            {
                if (ddlSubGrupoTxServSecre.SelectedValue == "" || ddlContaContabilTxServSecre.SelectedValue == "" || ddlCentroCustoTxServSecre.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvContaContabilTxMatri_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoTxMatri.SelectedValue != "")
            {
                if (ddlSubGrupoTxMatri.SelectedValue == "" || ddlContaContabilTxMatri.SelectedValue == "" || ddlCentroCustoTxMatri.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }        

        protected void cvContaContabilAtividaExtra_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoAtiviExtra.SelectedValue != "")
            {
                if (ddlSubGrupoAtiviExtra.SelectedValue == "" || ddlContaContabilAtiviExtra.SelectedValue == "" || ddlCentroCustoAtiviExtra.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvContaContabilCaixa_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoContaCaixa.SelectedValue != "")
            {
                if (ddlSubGrupoContaCaixa.SelectedValue == "" || ddlContaContabilContaCaixa.SelectedValue == "" || ddlCentroCustoContaCaixa.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvContaContabilBanco_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoContaBanco.SelectedValue != "")
            {
                if (ddlSubGrupoContaBanco.SelectedValue == "" || ddlContaContabilContaBanco.SelectedValue == "" || ddlCentroCustoContaBanco.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvContaContabilTxServBibli_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (ddlGrupoTxServBibli.SelectedValue != "")
            {
                if (ddlSubGrupoTxServBibli.SelectedValue == "" || ddlContaContabilTxServBibli.SelectedValue == "" || ddlCentroCustoTxServBibli.SelectedValue == "")
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvDataPeriodoBim1_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtPeriodoIniBim1.Text == "" && txtPeriodoFimBim1.Text != "") ||
               (txtPeriodoIniBim1.Text != "" && txtPeriodoFimBim1.Text == ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataLactoBim1_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtLactoIniBim1.Text == "" && txtLactoFimBim1.Text != "") ||
               (txtLactoFimBim1.Text == "" && txtLactoIniBim1.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataPeriodoBim2_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtPeriodoIniBim2.Text == "" && txtPeriodoFimBim2.Text != "") ||
               (txtPeriodoFimBim2.Text == "" && txtPeriodoIniBim2.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataLactoBim2_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtLactoIniBim2.Text == "" && txtLactoFimBim2.Text != "") ||
               (txtLactoFimBim2.Text == "" && txtLactoIniBim2.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataPeriodoBim3_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtPeriodoIniBim3.Text == "" && txtPeriodoFimBim3.Text != "") ||
               (txtPeriodoFimBim3.Text == "" && txtPeriodoIniBim3.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataLactoBim3_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtLactoIniBim3.Text == "" && txtLactoFimBim3.Text != "") ||
               (txtLactoFimBim3.Text == "" && txtLactoIniBim3.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataPeriodoBim4_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtPeriodoIniBim4.Text == "" && txtPeriodoFimBim4.Text != "") ||
               (txtPeriodoFimBim4.Text == "" && txtPeriodoIniBim4.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }

        protected void cvDataLactoBim4_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtLactoIniBim4.Text == "" && txtLactoFimBim4.Text != "") ||
               (txtLactoFimBim4.Text == "" && txtLactoIniBim4.Text != ""))
            {
                e.IsValid = false;
                AuxiliPagina.EnvioMensagemErro(this, "");
            }
        }
        #endregion

        #region Eventos dos componentes da página

        protected void ddlFlagMultaAtraso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagMultaAtraso.SelectedValue == "N")
                txtValorMultaEmpre.Text = txtDiasBonusMultaEmpre.Text = "";

            txtValorMultaEmpre.Enabled = txtDiasBonusMultaEmpre.Enabled = ddlFlagMultaAtraso.SelectedValue == "S";                        
        }

        //protected void chcSenha_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    TB25_EMPRESA tb25 = RetornaEntidade();

        //    if (chcSenha.Checked)
        //    {
        //        txtSenha.Text = tb25.SE_EMAIL;
        //    }
        //    else
        //    {
        //        txtSenha.Text = "****************";
        //    }
            
        //}

        protected void ddlFlagTaxaEmpre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagTaxaEmpre.SelectedValue == "N")
                txtValorTaxaEmpre.Text = txtDiasBonusTaxaEmpre.Text = "";

            txtValorTaxaEmpre.Enabled = txtDiasBonusTaxaEmpre.Enabled = ddlFlagTaxaEmpre.SelectedValue == "S";                        
        }

        protected void ddlFlagEmpreBibli_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagEmpreBibli.SelectedValue == "N")
                txtQtdeMaxDiasEmpre.Text = txtQtdeItensEmpre.Text = "";

            txtQtdeMaxDiasEmpre.Enabled = txtQtdeItensEmpre.Enabled = ddlFlagEmpreBibli.SelectedValue == "S";                        
        }

        protected void ddlFlagReserBibli_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagReserBibli.SelectedValue == "N")
                txtQtdeMaxDiasReser.Text = txtQtdeItensReser.Text = "";

            txtQtdeMaxDiasReser.Enabled = txtQtdeItensReser.Enabled = ddlFlagReserBibli.SelectedValue == "S";                        
        }

        protected void ddlUFIUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidadeIUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        protected void ddlGrupoTxMatri_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo(ddlGrupoTxMatri, ddlSubGrupoTxMatri);
        }

        protected void ddlSubGrupoTxMatri_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri);
        }

        protected void ddlSubGrupo2TxMatri_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri, ddlContaContabilTxMatri);
        }

        protected void ddlGrupoTxServBibli_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli);
        }

        protected void ddlSubGrupoTxServBibli_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli);
        }

        protected void ddlSubGrupo2TxServBibli_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli, ddlContaContabilTxServBibli);
        }

        protected void ddlGrupoTxServSecre_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre);
        }

        protected void ddlSubGrupoTxServSecre_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre);
        }

        protected void ddlSubGrupo2TxServSecre_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre, ddlContaContabilTxServSecre);
        }

        protected void ddlGrupoContaCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa);
        }

        protected void ddlSubGrupoContaCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa);
        }

        protected void ddlSubGrupo2ContaCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa, ddlContaContabilContaCaixa);
        }

        protected void ddlGrupoContaBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo(ddlGrupoContaBanco, ddlSubGrupoContaBanco);
        }

        protected void ddlSubGrupoContaBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco);
        }

        protected void ddlSubGrupo2ContaBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco, ddlContaContabilContaBanco);
        }

        protected void ddlGrupoAtiviExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra);
        }

        protected void ddlSubGrupoAtiviExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra);
        }

        protected void ddlSubGrupo2AtiviExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaConta(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra, ddlContaContabilAtiviExtra);
        }

        protected void btnPesqCEP_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCEPIUE.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEPIUE.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    tb235.TB240_TIPO_LOGRADOUROReference.Load();

                    txtLogradouroIUE.Text = tb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA + " " + tb235.NO_ENDER_CEP;                    
                    tb235.TB905_BAIRROReference.Load();
                    ddlUFIUE.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades();
                    ddlCidadeIUE.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros();
                    ddlBairroIUE.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
            }
        }
        
        protected void ddlGeraNumSolicAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraNumSolicAuto.SelectedValue != "S")
                txtNumIniciSolicAuto.Text = "";
            
            txtNumIniciSolicAuto.Enabled = ddlGeraNumSolicAuto.SelectedValue == "S";
        }

        protected void ddlGeraNumItemAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraNumItemAuto.SelectedValue != "S")
                txtNumIniciItemAuto.Text = "";

            txtNumIniciItemAuto.Enabled = ddlGeraNumItemAuto.SelectedValue == "S";
        }

        protected void ddlGeraNumEmpreAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraNumEmpreAuto.SelectedValue != "S")
                txtNumIniciEmpreAuto.Text = "";

            txtNumIniciEmpreAuto.Enabled = ddlGeraNumEmpreAuto.SelectedValue == "S";
        }

        protected void ddlContrPrazEntre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlContrPrazEntre.SelectedValue != "S")
                txtQtdeDiasEntreSolic.Text = "";

            txtQtdeDiasEntreSolic.Enabled = ddlAlterPrazoEntreSolic.Enabled = ddlContrPrazEntre.SelectedValue == "S";
        }

        protected void ddlFlagApresValorServi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagApresValorServi.SelectedValue != "S")
                ddlAbonaValorServiSolic.SelectedValue = ddlApresValorServiSolic.SelectedValue = "N";

            ddlAbonaValorServiSolic.Enabled = ddlApresValorServiSolic.Enabled = ddlFlagApresValorServi.SelectedValue == "S";
        }

        protected void ddlGeraBoletoServiSecre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraBoletoServiSecre.SelectedValue != "S")
                ddlTipoBoletoServiSecre.SelectedValue = "";

            ddlTipoBoletoServiSecre.Enabled = ddlGeraBoletoServiSecre.SelectedValue == "S";
        }

        protected void ddlFlagIncluContaReceb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagIncluContaReceb.SelectedValue != "S")
            {
                ddlTipoBoletoServiSecre.SelectedValue = "";
                ddlGeraBoletoServiSecre.SelectedValue = "N";
                txtVlJurosSecre.Text = txtVlMultaSecre.Text = "";
                ddlFlagTipoJurosSecre.SelectedValue = ddlFlagTipoMultaSecre.SelectedValue = "V";
                ddlTipoBoletoServiSecre.Enabled = ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                    ddlFlagTipoJurosSecre.Enabled = ddlFlagTipoMultaSecre.Enabled = false;
            }
            else
            {
                ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                    ddlFlagTipoJurosSecre.Enabled = ddlFlagTipoMultaSecre.Enabled = true;
            }
        }
        
        protected void ddlSiglaUnidSecreEscol1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol1, ddlNomeSecreEscol1);
        }

        protected void ddlSiglaUnidSecreEscol2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol2, ddlNomeSecreEscol2);
        }

        protected void ddlSiglaUnidSecreEscol3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol3, ddlNomeSecreEscol3);
        }

        protected void ddlSiglaUnidBibliEscol1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol1, ddlNomeBibliEscol1);
        }

        protected void ddlSiglaUnidBibliEscol2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol2, ddlNomeBibliEscol2);
        }

        protected void chkUsuarAluno_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsuarAluno.Checked)
                txtIdadeMinimAlunoSecreEscol.Text = "";
            
            txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarAluno.Checked;
        }

        protected void chkHorAtiSecreEscol_CheckedChanged(object sender, EventArgs e)
        {
            if (txtTipoCtrlSecreEscol.Text != "Instituição de Ensino")
            {
                if (chkHorAtiSecreEscol.Checked)
                {
                    txtHorarIniT1SecreEscol.Text = txtHorarFimT1SecreEscol.Text = txtHorarIniT2SecreEscol.Text =
                    txtHorarFimT2SecreEscol.Text = txtHorarIniT3SecreEscol.Text = txtHorarFimT3SecreEscol.Text =
                    txtHorarIniT4SecreEscol.Text = txtHorarFimT4SecreEscol.Text = "";
                    txtHorarIniT1SecreEscol.Text = txtHoraIniTurno1.Text;
                    txtHorarFimT1SecreEscol.Text = txtHoraFimTurno1.Text;
                    txtHorarIniT2SecreEscol.Text = txtHoraIniTurno2.Text;
                    txtHorarFimT2SecreEscol.Text = txtHoraFimTurno2.Text;
                    txtHorarIniT3SecreEscol.Text = txtHoraIniTurno3.Text;
                    txtHorarFimT3SecreEscol.Text = txtHoraFimTurno3.Text;
                    txtHorarIniT4SecreEscol.Text = txtHoraIniTurno4.Text;
                    txtHorarFimT4SecreEscol.Text = txtHoraFimTurno4.Text;
                    txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled =
                    txtHorarFimT2SecreEscol.Enabled = txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled =
                    txtHorarIniT4SecreEscol.Enabled = txtHorarFimT4SecreEscol.Enabled = false;
                }
                else
                {
                    txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled =
                    txtHorarFimT2SecreEscol.Enabled = txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled =
                    txtHorarIniT4SecreEscol.Enabled = txtHorarFimT4SecreEscol.Enabled = true;
                }
            }
            
        }

        protected void chkHorAtiBibli_CheckedChanged(object sender, EventArgs e)
        {
            if (txtTipoCtrlBibliEscol.Text != "Instituição de Ensino")
            {
                if (chkHorAtiBibli.Checked)
                {
                    txtHorarIniT1Bibli.Text = txtHorarFimT1Bibli.Text = txtHorarIniT2Bibli.Text =
                    txtHorarFimT2Bibli.Text = txtHorarIniT3Bibli.Text = txtHorarFimT3Bibli.Text =
                    txtHorarIniT4Bibli.Text = txtHorarFimT4Bibli.Text = "";
                    txtHorarIniT1Bibli.Text = txtHoraIniTurno1.Text;
                    txtHorarFimT1Bibli.Text = txtHoraFimTurno1.Text;
                    txtHorarIniT2Bibli.Text = txtHoraIniTurno2.Text;
                    txtHorarFimT2Bibli.Text = txtHoraFimTurno2.Text;
                    txtHorarIniT3Bibli.Text = txtHoraIniTurno3.Text;
                    txtHorarFimT3Bibli.Text = txtHoraFimTurno3.Text;
                    txtHorarIniT4Bibli.Text = txtHoraIniTurno4.Text;
                    txtHorarFimT4Bibli.Text = txtHoraFimTurno4.Text;
                    txtHorarIniT1Bibli.Enabled = txtHorarFimT1Bibli.Enabled = txtHorarIniT2Bibli.Enabled =
                    txtHorarFimT2Bibli.Enabled = txtHorarIniT3Bibli.Enabled = txtHorarFimT3Bibli.Enabled =
                    txtHorarIniT4Bibli.Enabled = txtHorarFimT4Bibli.Enabled = false;
                }
                else
                {
                    txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled =
                    txtHorarFimT2SecreEscol.Enabled = txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled =
                    txtHorarIniT4SecreEscol.Enabled = txtHorarFimT4SecreEscol.Enabled = true;
                }
            }

        }

        protected void chkUsuarAlunoBibli_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsuarAlunoBibli.Checked)
                txtIdadeMinimAlunoBibli.Text = "";

            txtIdadeMinimAlunoBibli.Enabled = chkUsuarAlunoBibli.Checked;
        }

        protected void chkSMSSolicSecreEscol_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSSolicSecreEscol.Checked)
            {
                txtMsgSMSSolic.Text = "";
                ddlFlagSMSSolicEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSSolic.Enabled = ddlFlagSMSSolicEnvAuto.Enabled = chkSMSSolicSecreEscol.Checked;
        }

        protected void chkSMSEntreSecreEscol_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSEntreSecreEscol.Checked)
            {
                txtMsgSMSEntre.Text = "";
                ddlFlagSMSEntreEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSEntre.Enabled = ddlFlagSMSEntreEnvAuto.Enabled = chkSMSEntreSecreEscol.Checked;
        }

        protected void chkSMSOutroSecreEscol_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSOutroSecreEscol.Checked)
            {
                txtMsgSMSOutro.Text = "";
                ddlFlagSMSOutroEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSOutro.Enabled = ddlFlagSMSOutroEnvAuto.Enabled = chkSMSOutroSecreEscol.Checked;
        }

        protected void chkSMSReserVagas_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSReserVagas.Checked)
            {
                txtMsgSMSReserVagas.Text = "";
                ddlFlagSMSReserVagasEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSReserVagas.Enabled = ddlFlagSMSReserVagasEnvAuto.Enabled = chkSMSReserVagas.Checked;
        }

        protected void chkSMSRenovMatri_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSRenovMatri.Checked)
            {
                txtMsgSMSRenovMatri.Text = "";
                ddlFlagSMSRenovMatriEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSRenovMatri.Enabled = ddlFlagSMSRenovMatriEnvAuto.Enabled = chkSMSRenovMatri.Checked;
        }

        protected void chkSMSMatriNova_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSMatriNova.Checked)
            {
                txtMsgSMSMatriNova.Text = "";
                ddlFlagSMSMatriNovaEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSMatriNova.Enabled = ddlFlagSMSMatriNovaEnvAuto.Enabled = chkSMSMatriNova.Checked;
        }

        protected void chkSMSReserBibli_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSReserBibli.Checked)
            {
                txtMsgSMSReserBibli.Text = "";
                ddlFlagSMSReserBibliEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSReserBibli.Enabled = ddlFlagSMSReserBibliEnvAuto.Enabled = chkSMSReserBibli.Checked;
        }

        protected void chkSMSEmpreBibli_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSEmpreBibli.Checked)
            {
                txtMsgSMSEmpreBibli.Text = "";
                ddlFlagSMSEmpreBibliEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSEmpreBibli.Enabled = ddlFlagSMSEmpreBibliEnvAuto.Enabled = chkSMSEmpreBibli.Checked;
        }

        protected void chkSMSDiverBibli_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSDiverBibli.Checked)
            {
                txtMsgSMSDiverBibli.Text = "";
                ddlFlagSMSDiverBibliEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSDiverBibli.Enabled = ddlFlagSMSDiverBibliEnvAuto.Enabled = chkSMSDiverBibli.Checked;
        }

        protected void chkSMSFaltaAluno_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMSFaltaAluno.Checked)
            {
                txtMsgSMSFaltaAluno.Text = "";
                ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue = "N";
            }

            txtMsgSMSFaltaAluno.Enabled = ddlFlagSMSFaltaAlunoEnvAuto.Enabled = chkSMSFaltaAluno.Checked;
        }

        protected void ddlFormaAvali_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormaAvali.SelectedValue == "C")
            {
//------------> Recebe o ID da Unidade informada pela QueryString
                int idTb25 = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                if (idTb25 > 0)
                {
//----------------> Carrega conceito da Unidade informada
                    var tb200 = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                 where iTb200.TB25_EMPRESA.CO_EMP == idTb25
                                 select iTb200).OrderByDescending(eq => eq.VL_NOTA_MAX);

                    int i = 0;

                    if (tb200 != null)
                    {
                        foreach (var iTb200 in tb200)
                        {
                            if (i == 0)
                            {
                                txtDescrConce1.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce1.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni1.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim1.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 1)
                            {
                                txtDescrConce2.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce2.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni2.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim2.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 2)
                            {
                                txtDescrConce3.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce3.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni3.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim3.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 3)
                            {
                                txtDescrConce4.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce4.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni4.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim4.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            if (i == 4)
                            {
                                txtDescrConce5.Text = iTb200.DE_CONCEITO;
                                txtSiglaConce5.Text = iTb200.CO_SIGLA_CONCEITO;
                                txtNotaIni5.Text = iTb200.VL_NOTA_MIN.ToString();
                                txtNotaFim5.Text = iTb200.VL_NOTA_MAX.ToString();
                            }

                            i++;
                        }                     
                    }
                }
            }
            else
            {
                txtDescrConce1.Text = txtSiglaConce1.Text = txtNotaIni1.Text = txtNotaFim1.Text =
                txtDescrConce2.Text = txtSiglaConce2.Text = txtNotaIni2.Text = txtNotaFim2.Text =
                txtDescrConce3.Text = txtSiglaConce3.Text = txtNotaIni3.Text = txtNotaFim3.Text =
                txtDescrConce4.Text = txtSiglaConce4.Text = txtNotaIni4.Text = txtNotaFim4.Text =
                txtDescrConce5.Text = txtSiglaConce5.Text = txtNotaIni5.Text = txtNotaFim5.Text = "";
            }
        }

        protected void ddlContaContabilTxServSecre_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilTxServSecre.SelectedValue != "" ? int.Parse(ddlContaContabilTxServSecre.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabTxServSecre.Text = resultado != null ? resultado.DE_CONTA_PC : "";
        }

        protected void ddlContaContabilTxServBibli_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilTxServBibli.SelectedValue != "" ? int.Parse(ddlContaContabilTxServBibli.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabTxServBibli.Text = resultado != null ?  resultado.DE_CONTA_PC : "";
        }

        protected void ddlContaContabilTxMatri_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilTxMatri.SelectedValue != "" ? int.Parse(ddlContaContabilTxMatri.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabTxMatri.Text = resultado != null ?  resultado.DE_CONTA_PC : "";
        }

        protected void ddlContaContabilContaCaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilContaCaixa.SelectedValue != "" ? int.Parse(ddlContaContabilContaCaixa.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabCaixa.Text = resultado != null ? resultado.DE_CONTA_PC : "";
        }

        protected void ddlContaContabilContaBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilContaBanco.SelectedValue != "" ? int.Parse(ddlContaContabilContaBanco.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabBanco.Text = resultado != null ? resultado.DE_CONTA_PC : "";
        }

        protected void ddlContaContabilAtiviExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilAtiviExtra.SelectedValue != "" ? int.Parse(ddlContaContabilAtiviExtra.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabAtiviExtra.Text = resultado != null ? resultado.DE_CONTA_PC : "";
        }

        #endregion
    }
}