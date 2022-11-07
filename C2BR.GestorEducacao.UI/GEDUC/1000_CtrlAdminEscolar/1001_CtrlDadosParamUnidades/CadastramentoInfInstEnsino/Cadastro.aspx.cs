//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE INFORMAÇÕES DA INSTITUIÇÃO DE ENSINO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 das contas contábeis
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.UI;
using System.Text;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfInstEnsino
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
            if (Page.IsPostBack) return;

            rblInforCadastro.SelectedValue = "1";
            tabDadosCadas.Style.Add("display", "block");

            CarregaNucleoInstituicao();
            CarregaUFs();
            CarregaCidades();
            CarregaBairros();

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

            CarregaCentroCusto();

            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol1);
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol1, ddlNomeSecreEscol1);
            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol2);
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol2, ddlNomeSecreEscol2);
            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol3);
            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol3, ddlNomeSecreEscol3);
            CarregaUnidadeNomeBibliEscol(ddlSiglaUnidBibliEscol1);
            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol1, ddlNomeBibliEscol1);
            CarregaUnidadeNomeBibliEscol(ddlSiglaUnidBibliEscol2);
            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol2, ddlNomeBibliEscol2);

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string now = DateTime.Now.ToString("dd/MM/yyyy");

                txtDtCadastroIIE.Text = txtDtStatusIIE.Text = now;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //if (!ValidaHorarios()) return;

            if (txtDtIniContr.Text != "")
            {
                if (txtDtFimContr.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data Fim de Contrato deve ser informada.");
                    return;
                }
                else
                {
                    if (DateTime.Parse(txtDtIniContr.Text) > DateTime.Parse(txtDtFimContr.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Data Início deve ser menor que Data Fim de contrato.");
                        return;
                    }
                }
            }

            if (Page.IsValid)
            {
                decimal decimalRetorno = 0;

                if (txtVlJurosSecre.Text != "" ? !decimal.TryParse(txtVlJurosSecre.Text, out decimalRetorno) : false)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Juros diário da secretaria incorreto.");
                    return;
                }

                TB000_INSTITUICAO tb000 = RetornaEntidade();

                if (tb000 == null)
                    tb000 = new TB000_INSTITUICAO();
                else
                {
                    tb000.TB149_PARAM_INSTIReference.Load();
                    //  tb000.TB905_BAIRROReference.Load();
                }

                //if (tb000.TB905_BAIRRO == null)
                  //  tb000.TB905_BAIRRO = new TB905_BAIRRO();

                int intRetorno;                
                DateTime dataRetorno;

                int idFotoOrgao = imgOrgaoIIE.GravaImagem();

                tb000.Image3 = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(idFotoOrgao);
                tb000.ORG_NUMERO_CNPJ = txtCNPJIUE.Text != "" && decimal.TryParse(txtCNPJIUE.Text.Replace("-", "").Replace(".", "").Replace("/", ""), out decimalRetorno) ? decimalRetorno : 0;
                tb000.ORG_NOME_ORGAO = txtNomeIIE.Text;
                tb000.ORG_NOME_FANTAS_ORGAO = txtNomeFantaIIE.Text;
                tb000.TB_NUCLEO_INST = ddlNucleoIUE.SelectedValue != "" ? TB_NUCLEO_INST.RetornaPeloCoNucleo(int.Parse(ddlNucleoIUE.SelectedValue)) : null;
                tb000.ORG_CO_INS_ESTA_ORGAO = txtInscEstadualIIE.Text != "" ? txtInscEstadualIIE.Text : null;
                tb000.ORG_NU_NIS_ORGAO = int.TryParse(txtNISIIE.Text.Replace("-", ""), out intRetorno) ? (int?)intRetorno : null;
                tb000.ORG_NOME_REDUZI = txtSiglaIIE.Text;
                tb000.ORG_NUMERO_CONTR = txtNumContr.Text != "" ? txtNumContr.Text : null;
                tb000.DT_INI_CONTR = DateTime.TryParse(txtDtIniContr.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb000.DT_FIM_CONTR = DateTime.TryParse(txtDtFimContr.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb000.NO_RESPO_CONTR = txtNomeRespon.Text != "" ? txtNomeRespon.Text : null;
                tb000.NU_CPF_RESPO_CONTR = txtCPFRespon.Text != "" ? txtCPFRespon.Text.Replace(".","").Replace("-","") : null;
                tb000.DT_NASCTO_RESPO_CONTR = DateTime.TryParse(txtDtNasctoRespon.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb000.NU_TELE_FIXO_RESPO_CONTR = txtTelFixoRespo.Text != "" ? txtTelFixoRespo.Text.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb000.NU_TELE_CELU_RESPO_CONTR = txtTelCeluRespo.Text != "" ? txtTelCeluRespo.Text.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb000.DE_EMAIL_RESPO_CONTR = txtEmailRespo.Text != "" ? txtEmailRespo.Text : null;
                tb000.CEP_CODIGO = int.TryParse(txtCEPIIE.Text.Replace("-", ""), out intRetorno) ? (int?)intRetorno : null;
                tb000.ORG_ENDERE_ORGAO = txtLogradouroIIE.Text != "" ? txtLogradouroIIE.Text : null;
                tb000.ORG_ENDERE_NUMERO = txtNumeroIIE.Text;
                tb000.ORG_ENDERE_COMPLE = txtComplementoIIE.Text != "" ? txtComplementoIIE.Text : null;
                tb000.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroIIE.SelectedValue));
                tb000.CID_CODIGO_UF = ddlUFIIE.SelectedValue;
                tb000.ORG_CO_GEORE_LATIT_ORGAO = txtLatitude.Text != "" ? txtLatitude.Text : null;
                tb000.ORG_CO_GEORE_LONGI_ORGAO = txtLongitude.Text != "" ? txtLongitude.Text : null;
                tb000.ORG_NUMERO_FONE1 = decimal.TryParse(txtTelefoneIIE.Text.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", ""), out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb000.ORG_NUMERO_FONE2 = decimal.TryParse(txtTelefone2IIE.Text.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", ""), out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb000.ORG_EMAIL_CONTAT = txtEmailIIE.Text != "" ? txtEmailIIE.Text : null;
                tb000.ORG_NUMERO_FAX1 = decimal.TryParse(txtFaxIIE.Text.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", ""), out decimalRetorno) ? (decimal?)decimalRetorno : null;                
                tb000.ORG_HOME_PAGE = txtWebSiteIIE.Text != "" ? txtWebSiteIIE.Text : null;
                tb000.ORG_DE_OBS_ORGAO = txtObservacaoIIE.Text != "" ? txtObservacaoIIE.Text : null;
                tb000.ORG_CO_DOCTO_CONST_ORGAO = txtNumDocto.Text != "" ? txtNumDocto.Text : null;
                tb000.ORG_DT_CONST_ORGAO = DateTime.TryParse(txtDataConstituicao.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb000.ORG_STATUS_CODIGO = ddlSitucaoIIE.SelectedValue;
                tb000.ORG_STATUS_DATA = DateTime.Parse(txtDtStatusIIE.Text);
                tb000.ORG_DATA_CADAST = DateTime.Parse(txtDtCadastroIIE.Text);                
                
//------------> Dados da tabela TB149_PARAM_INSTI             
                if (tb000.TB149_PARAM_INSTI == null)
                    tb000.TB149_PARAM_INSTI = new TB149_PARAM_INSTI();
                //else
                  //  tb000.TB149_PARAM_INSTIReference.Load();

//------------> Informação de Multifrequência
                if (ddlPermiMultiFrequ.Enabled)
                    tb000.TB149_PARAM_INSTI.FL_MULTI_FREQU = ddlPermiMultiFrequ.SelectedValue;
                else
                    tb000.TB149_PARAM_INSTI.FL_MULTI_FREQU = null;


                tb000.TB149_PARAM_INSTI.TP_USO_LOGO = ddlUsoLogo.SelectedValue;
                tb000.TB149_PARAM_INSTI.FLA_CTRL_FREQ = "I";
                tb000.TB149_PARAM_INSTI.FLA_CTRL_HFUNC = "I";
                tb000.TB149_PARAM_INSTI.TP_HORA_FUNC = "MTN";
                tb000.TB149_PARAM_INSTI.HR_FUNCI_MANHA_INICI = txtHoraIniTurno1.Text != "" ? txtHoraIniTurno1.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_MANHA_FINAL = txtHoraFimTurno1.Text != "" ? txtHoraFimTurno1.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_TARDE_INICI = txtHoraIniTurno2.Text != "" ? txtHoraIniTurno2.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_TARDE_FINAL = txtHoraFimTurno2.Text != "" ? txtHoraFimTurno2.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_NOITE_INICI = txtHoraIniTurno3.Text != "" ? txtHoraIniTurno3.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_NOITE_FINAL = txtHoraFimTurno3.Text != "" ? txtHoraFimTurno3.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_ULTIM_TURNO_INICI = txtHoraIniTurno4.Text != "" ? txtHoraIniTurno4.Text.Replace(":", "") : null;
                tb000.TB149_PARAM_INSTI.HR_FUNCI_ULTIM_TURNO_FINAL = txtHoraFimTurno4.Text != "" ? txtHoraFimTurno4.Text.Replace(":", "") : null;

                tb000.TB149_PARAM_INSTI.HR_LIMITE_MANHA_INICI = "";
                tb000.TB149_PARAM_INSTI.HR_LIMITE_MANHA_FINAL = "";
                tb000.TB149_PARAM_INSTI.HR_LIMITE_TARDE_INICI = "";
                tb000.TB149_PARAM_INSTI.HR_LIMITE_TARDE_FINAL = "";
                tb000.TB149_PARAM_INSTI.HR_LIMITE_NOITE_INICI = "";
                tb000.TB149_PARAM_INSTI.HR_LIMITE_NOITE_FINAL = "";

//------------> Informações de Controle Quem Somos
                tb000.TB149_PARAM_INSTI.TP_CTRLE_DESCR = ddlTipoCtrlDescr.SelectedValue;
                tb000.TB149_PARAM_INSTI.DES_QUEM_SOMOS = txtQuemSomos.Html != "" ? txtQuemSomos.Html : null;
                tb000.TB149_PARAM_INSTI.DES_NOSSA_HISTO = txtNossaHisto.Html != "" ? txtNossaHisto.Html : null;
                tb000.TB149_PARAM_INSTI.DES_PROPO_PEDAG = txtPropoPedag.Html != "" ? txtPropoPedag.Html : null;

//------------> Informações de Controle Pedagógico/Matrículas
                tb000.TB149_PARAM_INSTI.TP_CTRLE_METOD = ddlTipoCtrleMetod.SelectedValue;

                if (ddlTipoCtrleMetod.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.TP_ENSINO = ddlMetodEnsino.SelectedValue;
                    tb000.TB149_PARAM_INSTI.TP_FORMA_AVAL = ddlFormaAvali.SelectedValue;
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.TP_ENSINO = null;
                    tb000.TB149_PARAM_INSTI.TP_FORMA_AVAL = null;
                }
                
                tb000.TB149_PARAM_INSTI.TP_CTRLE_AVAL = ddlTipoCtrleAval.SelectedValue;
                tb000.TB149_PARAM_INSTI.TP_AVALI = ddlPerioAval.SelectedValue;

                if (ddlTipoCtrleAval.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.TP_PERIOD_AVAL = ddlPerioAval.SelectedValue;

                    tb000.TB149_PARAM_INSTI.VL_MEDIA_CURSO = decimal.TryParse(txtMediaAprovGeral.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_APROV_DIRETA = decimal.TryParse(txtMediaAprovDireta.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_PROVA_FINAL = decimal.TryParse(txtMediaProvaFinal.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;

                    tb000.TB149_PARAM_INSTI.FL_RECUPER = chkRecuperEscol.Checked;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_RECUPER = decimal.TryParse(txtMediaRecuperEscol.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.QT_MATERIAS_RECUPER = int.TryParse(txtQtdeMaterRecuper.Text, out intRetorno) ? (int?)intRetorno : null;

                    tb000.TB149_PARAM_INSTI.FL_DEPEND = chkDepenEscol.Checked;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_DEPEND = decimal.TryParse(txtMediaDepenEscol.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.QT_MATERIAS_DEPEND = int.TryParse(txtQtdMaterDepenEscol.Text, out intRetorno) ? (int?)intRetorno : null;

                    tb000.TB149_PARAM_INSTI.FL_CONSELHO = chkConselho.Checked;
                    tb000.TB149_PARAM_INSTI.VL_LIMIT_MEDIA_CONSELHO = decimal.TryParse(txtLimitMediaConseEscol.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.QT_MATERIAS_CONSELHO = int.TryParse(txtQtdMaxMaterConse.Text, out intRetorno) ? (int?)intRetorno : null;                                                            
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.TP_PERIOD_AVAL = null;

                    tb000.TB149_PARAM_INSTI.VL_MEDIA_CURSO = null;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_APROV_DIRETA = null;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_PROVA_FINAL = null;

                    tb000.TB149_PARAM_INSTI.FL_RECUPER = null;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_RECUPER = null;
                    tb000.TB149_PARAM_INSTI.QT_MATERIAS_RECUPER = null;

                    tb000.TB149_PARAM_INSTI.FL_DEPEND = null;
                    tb000.TB149_PARAM_INSTI.VL_MEDIA_DEPEND = null;
                    tb000.TB149_PARAM_INSTI.QT_MATERIAS_DEPEND = null;

                    tb000.TB149_PARAM_INSTI.FL_CONSELHO = null;
                    tb000.TB149_PARAM_INSTI.VL_LIMIT_MEDIA_CONSELHO = null;
                    tb000.TB149_PARAM_INSTI.QT_MATERIAS_CONSELHO = null; 
                }

                tb000.TB149_PARAM_INSTI.FLA_CTRL_DATA = ddlTipoCtrleDatas.SelectedValue;

                if (ddlTipoCtrleDatas.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.DT_INI_RES = DateTime.TryParse(txtReservaMatriculaDtInicioIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_RES = DateTime.TryParse(txtReservaMatriculaDtFimIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_INI_INSC = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_INSC = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_MAT = DateTime.TryParse(txtMatriculaInicioIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_MAT = DateTime.TryParse(txtMatriculaFimIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_INI_REMAN_ALUNO = DateTime.TryParse(txtDataRemanAlunoIni.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_REMAN_ALUNO = DateTime.TryParse(txtDataRemanAlunoFim.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_INI_TRANS_INTER = DateTime.TryParse(txtDtInicioTransInter.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_TRANS_INTER = DateTime.TryParse(txtDtFimTransInter.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_INI_TRAN = DateTime.TryParse(txtTrancamentoMatriculaInicioIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_TRAN = DateTime.TryParse(txtTrancamentoMatriculaFimIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_INI_ALTMAT = DateTime.TryParse(txtAlteracaoMatriculaInicioIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_ALTMAT = DateTime.TryParse(txtAlteracaoMatriculaFimIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_INI_REMAT = DateTime.TryParse(txtRematriculaInicioIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_REMAT = DateTime.TryParse(txtRematriculaFimIUE.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.DT_INI_RES = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_RES = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_INSC = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_INSC = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_MAT = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_MAT = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_REMAN_ALUNO = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_REMAN_ALUNO = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_TRANS_INTER = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_TRANS_INTER = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_TRAN = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_TRAN = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_ALTMAT = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_ALTMAT = null;
                    tb000.TB149_PARAM_INSTI.DT_INI_REMAT = null;
                    tb000.TB149_PARAM_INSTI.DT_FIM_REMAT = null;
                }

                tb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN = ddlTipoControleTpEnsinoIIE.SelectedValue;
                if (ddlTipoControleTpEnsinoIIE.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO = ddlGerarNisIIE.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FLA_GERA_MATR_AUTO = ddlGerarMatriculaIIE.SelectedValue;
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO = null;
                    tb000.TB149_PARAM_INSTI.FLA_GERA_MATR_AUTO = null;
                }
                

//------------> Informações de Controle Secretaria Escolar
                tb000.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL = ddlTipoCtrlSecreEscol.SelectedValue;

                if (ddlTipoCtrlSecreEscol.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.FL_USUAR_FUNCI_SECRE = chkUsuarFunc.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_USUAR_PROFE_SECRE = chkUsuarProf.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_USUAR_ALUNO_SECRE = chkUsuarAluno.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_SECRE = int.TryParse(txtIdadeMinimAlunoSecreEscol.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_RESPO_SECRE = chkUsuarPaisRespo.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_USUAR_OUTRO_SECRE = chkUsuarOutro.Checked ? "S" : "N";

                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_SECRE = txtHorarIniT1SecreEscol.Text != "" ? txtHorarIniT1SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_SECRE = txtHorarFimT1SecreEscol.Text != "" ? txtHorarFimT1SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_SECRE = txtHorarIniT2SecreEscol.Text != "" ? txtHorarIniT2SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_SECRE = txtHorarFimT2SecreEscol.Text != "" ? txtHorarFimT2SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_SECRE = txtHorarIniT3SecreEscol.Text != "" ? txtHorarIniT3SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_SECRE = txtHorarFimT3SecreEscol.Text != "" ? txtHorarFimT3SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_SECRE = txtHorarIniT4SecreEscol.Text != "" ? txtHorarIniT4SecreEscol.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_SECRE = txtHorarFimT4SecreEscol.Text != "" ? txtHorarFimT4SecreEscol.Text.Replace(":", "") : null;

                    tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_SECRE = ddlGeraNumSolicAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_SECRE = int.TryParse(txtNumIniciSolicAuto.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_CTRLE_PRAZO_ENT_SECRE = ddlContrPrazEntre.SelectedValue;
                    tb000.TB149_PARAM_INSTI.NU_DIAS_ENT_SOL = int.TryParse(txtQtdeDiasEntreSolic.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FLA_CTRLE_DIAS_SOLIC = ddlAlterPrazoEntreSolic.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FL_APRE_VALOR_SERV_SECRE = ddlFlagApresValorServi.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FL_ABONA_VALOR_SERV_SECRE = ddlAbonaValorServiSolic.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FL_ALTER_VALOR_SERV_SECRE = ddlApresValorServiSolic.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_SECRE = ddlFlagIncluContaReceb.SelectedValue;

                    tb000.TB149_PARAM_INSTI.CO_FLAG_GERA_BOLETO_SERV_SECR = ddlGeraBoletoServiSecre.SelectedValue;
                    tb000.TB149_PARAM_INSTI.TP_BOLETO_BANC = ddlTipoBoletoServiSecre.SelectedValue != "" ? ddlTipoBoletoServiSecre.SelectedValue : null;
                    //Formatando o valor do Juros para salvar
                    if (txtVlJurosSecre.Text == "")
                    {
                        decimal jurosDec = decimal.Zero;
                        decimal txtVlJur = txtVlJurosSecre.Text != "" ? decimal.Parse(txtVlJurosSecre.Text) : 0;
                        bool convertido = decimal.TryParse(string.Format("{0:0.000}", txtVlJur), out jurosDec);
                        tb000.TB149_PARAM_INSTI.VL_PEC_JUROS = convertido ? (decimal?)jurosDec : null;
                    }
                    else
                        tb000.TB149_PARAM_INSTI.VL_PEC_JUROS = null;
                    tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_JUROS = ddlFlagTipoJurosSecre.SelectedValue;
                    tb000.TB149_PARAM_INSTI.VL_PEC_MULTA = decimal.TryParse(txtVlMultaSecre.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_MULTA = ddlFlagTipoMultaSecre.SelectedValue;

                    //tb000.TB149_PARAM_INSTI.cablinha1 = txtCabec1Relatorio.Text != "" ? txtCabec1Relatorio.Text : null;
                    //tb000.TB149_PARAM_INSTI.cablinha2 = txtCabec2Relatorio.Text != "" ? txtCabec2Relatorio.Text : null;
                    //tb000.TB149_PARAM_INSTI.cablinha3 = txtCabec3Relatorio.Text != "" ? txtCabec3Relatorio.Text : null;
                    //tb000.TB149_PARAM_INSTI.cablinha4 = txtCabec4Relatorio.Text != "" ? txtCabec4Relatorio.Text : null;

//----------------> Secretário(a) 1                
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR = ddlNomeSecreEscol1.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeSecreEscol1.SelectedValue)) : null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE1 = int.TryParse(ddlClassifSecre1.SelectedValue, out intRetorno) ? (int?)intRetorno : null;

//----------------> Secretário(a) 2                
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR2 = ddlNomeSecreEscol2.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeSecreEscol2.SelectedValue)) : null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE2 = int.TryParse(ddlClassifSecre2.SelectedValue, out intRetorno) ? (int?)intRetorno : null;

//----------------> Secretário(a) 3                
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR3 = ddlNomeSecreEscol3.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeSecreEscol3.SelectedValue)) : null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE3 = int.TryParse(ddlClassifSecre3.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.FL_USUAR_FUNCI_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_PROFE_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_ALUNO_SECRE = null;
                    tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_RESPO_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_OUTRO_SECRE = null;

                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_SECRE = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_SECRE = null;

                    tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_SECRE = null;
                    tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_CTRLE_PRAZO_ENT_SECRE = null;
                    tb000.TB149_PARAM_INSTI.NU_DIAS_ENT_SOL = null;
                    tb000.TB149_PARAM_INSTI.FLA_CTRLE_DIAS_SOLIC = null;
                    tb000.TB149_PARAM_INSTI.FL_APRE_VALOR_SERV_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_ABONA_VALOR_SERV_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_ALTER_VALOR_SERV_SECRE = null;
                    tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_SECRE = null;

                    tb000.TB149_PARAM_INSTI.CO_FLAG_GERA_BOLETO_SERV_SECR = null;
                    tb000.TB149_PARAM_INSTI.TP_BOLETO_BANC = null;
                    tb000.TB149_PARAM_INSTI.VL_PEC_JUROS = null;
                    tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_JUROS = null;
                    tb000.TB149_PARAM_INSTI.VL_PEC_MORA = null;
                    tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_MOR = null;

                    //tb000.TB149_PARAM_INSTI.cablinha1 = null;
                    //tb000.TB149_PARAM_INSTI.cablinha2 = null;
                    //tb000.TB149_PARAM_INSTI.cablinha3 = null;
                    //tb000.TB149_PARAM_INSTI.cablinha4 = null;

                    tb000.TB149_PARAM_INSTI.TB03_COLABOR = null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE1 = null;
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR2 = null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE2 = null;
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR3 = null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE3 = null;
                }

//------------> Informações de Controle Biblioteca Escolar
                tb000.TB149_PARAM_INSTI.TP_CTRLE_BIBLI = ddlTipoCtrlBibliEscol.SelectedValue;

                if (ddlTipoCtrlBibliEscol.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.FLA_RESER_OUTRA_UNID = ddlFlagReserBibli.SelectedValue != "" ? ddlFlagReserBibli.SelectedValue : null;
                    tb000.TB149_PARAM_INSTI.QT_ITENS_ALUNO_BIBLI = int.TryParse(txtQtdeItensReser.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.QT_DIAS_RESER_BIBLI = int.TryParse(txtQtdeMaxDiasReser.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_EMPRE_BIBLI = ddlFlagEmpreBibli.SelectedValue;
                    tb000.TB149_PARAM_INSTI.QT_ITENS_EMPRE_BIBLI = int.TryParse(txtQtdeItensEmpre.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_DIAS_EMPRE_BIBLI = int.TryParse(txtQtdeMaxDiasEmpre.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_NU_EMPRE_AUTO_BIBLI = ddlGeraNumEmpreAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.NU_EMPRE_INICI_BIBLI = int.TryParse(txtNumIniciEmpreAuto.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_TAXA_EMPRE_BIBLI = ddlFlagTaxaEmpre.SelectedValue;
                    tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI = int.TryParse(txtDiasBonusTaxaEmpre.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.VL_TAXA_DIA_EMPRE_BIBLI = decimal.TryParse(txtValorTaxaEmpre.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_MULTA_EMPRE_BIBLI = ddlFlagMultaAtraso.SelectedValue;
                    tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI = int.TryParse(txtDiasBonusMultaEmpre.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.VL_MULTA_DIA_ATRASO_BIBLI = decimal.TryParse(txtValorMultaEmpre.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_BIBLI = ddlGeraNumItemAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_BIBLI = int.TryParse(txtNumIniciItemAuto.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_NU_ISBN_OBRIG_BIBLI = ddlNumISBNObrig.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_BIBLI = ddlFlarIncluContaRecebBibli.SelectedValue;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_FUNCI_BIBLI = chkUsuarFuncBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_USUAR_PROFE_BIBLI = chkUsuarProfBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_USUAR_ALUNO_BIBLI = chkUsuarAlunoBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI = int.TryParse(txtIdadeMinimAlunoBibli.Text, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_RESPO_BIBLI = chkUsuarRespBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_USUAR_OUTRO_BIBLI = chkUsuarOutroBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_BIBLI = txtHorarIniT1Bibli.Text != "" ? txtHorarIniT1Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_BIBLI = txtHorarFimT1Bibli.Text != "" ? txtHorarFimT1Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_BIBLI = txtHorarIniT2Bibli.Text != "" ? txtHorarIniT2Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_BIBLI = txtHorarFimT2Bibli.Text != "" ? txtHorarFimT2Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_BIBLI = txtHorarIniT3Bibli.Text != "" ? txtHorarIniT3Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_BIBLI = txtHorarFimT3Bibli.Text != "" ? txtHorarFimT3Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_BIBLI = txtHorarIniT4Bibli.Text != "" ? txtHorarIniT4Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_BIBLI = txtHorarFimT4Bibli.Text != "" ? txtHorarFimT4Bibli.Text.Replace(":", "") : null;
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR1 = ddlNomeBibliEscol1.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeBibliEscol1.SelectedValue)) : null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI1 = int.TryParse(ddlClassifBibli1.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR4 = ddlNomeBibliEscol2.SelectedValue != "" ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNomeBibliEscol2.SelectedValue)) : null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI2 = int.TryParse(ddlClassifBibli2.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.FLA_RESER_OUTRA_UNID = null;
                    tb000.TB149_PARAM_INSTI.QT_ITENS_ALUNO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.QT_DIAS_RESER_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.QT_ITENS_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_DIAS_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_TAXA_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.VL_TAXA_DIA_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_MULTA_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.VL_MULTA_DIA_ATRASO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_NU_ISBN_OBRIG_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_FUNCI_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_PROFE_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_ALUNO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_RESPO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.FL_USUAR_OUTRO_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_BIBLI = null;
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR1 = null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI1 = null;
                    tb000.TB149_PARAM_INSTI.TB03_COLABOR4 = null;
                    tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI2 = null;
                }

//------------> Informações de Controle Contábil
                tb000.TB149_PARAM_INSTI.TP_CTRLE_CTA_CONTAB = ddlTipoCtrleContaContab.SelectedValue;

                if (ddlTipoCtrleContaContab.SelectedValue == TipoControle.I.ToString())
                {
                    if (ddlGrupoTxServSecre.SelectedValue != "")
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSSOL = int.Parse(ddlCentroCustoTxServSecre.SelectedValue);
                        tb000.TB149_PARAM_INSTI.CO_CTSOL_EMP = int.Parse(ddlContaContabilTxServSecre.SelectedValue);
                    }
                    else
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSSOL = null;
                        tb000.TB149_PARAM_INSTI.CO_CTSOL_EMP = null;
                    }

                    if (ddlGrupoTxServBibli.SelectedValue != "")
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSBIB = int.Parse(ddlCentroCustoTxServBibli.SelectedValue);
                        tb000.TB149_PARAM_INSTI.CO_CTABIB_EMP = int.Parse(ddlContaContabilTxServBibli.SelectedValue);
                    }
                    else
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSBIB = null;
                        tb000.TB149_PARAM_INSTI.CO_CTABIB_EMP = null;
                    }

                    if (ddlGrupoTxMatri.SelectedValue != "")
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSMAT = int.Parse(ddlCentroCustoTxMatri.SelectedValue);
                        tb000.TB149_PARAM_INSTI.CO_CTAMAT_EMP = int.Parse(ddlContaContabilTxMatri.SelectedValue);
                    }
                    else
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSMAT = null;
                        tb000.TB149_PARAM_INSTI.CO_CTAMAT_EMP = null;
                    }                    

                    if (ddlGrupoAtiviExtra.SelectedValue != "")
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_ATIVI_EXTRA = int.Parse(ddlCentroCustoAtiviExtra.SelectedValue);
                        tb000.TB149_PARAM_INSTI.CO_CTA_ATIVI_EXTRA = int.Parse(ddlContaContabilAtiviExtra.SelectedValue);
                    }
                    else
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_ATIVI_EXTRA = null;
                        tb000.TB149_PARAM_INSTI.CO_CTA_ATIVI_EXTRA = null;
                    }

                    if (ddlGrupoContaCaixa.SelectedValue != "")
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_CAIXA = ddlCentroCustoContaCaixa.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoContaCaixa.SelectedValue) : null;
                        tb000.TB149_PARAM_INSTI.CO_CTA_CAIXA = int.Parse(ddlContaContabilContaCaixa.SelectedValue);
                    }
                    else
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_CAIXA = null;
                        tb000.TB149_PARAM_INSTI.CO_CTA_CAIXA = null;
                    }

                    if (ddlGrupoContaBanco.SelectedValue != "")
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_BANCO = ddlCentroCustoContaBanco.SelectedValue != "" ? (int?)int.Parse(ddlCentroCustoContaBanco.SelectedValue) : null;
                        tb000.TB149_PARAM_INSTI.CO_CTA_BANCO = int.Parse(ddlContaContabilContaBanco.SelectedValue);
                    }
                    else
                    {
                        tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_BANCO = null;
                        tb000.TB149_PARAM_INSTI.CO_CTA_BANCO = null;
                    }
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.CO_CENT_CUSSOL = null;
                    tb000.TB149_PARAM_INSTI.CO_CTSOL_EMP = null;
                    tb000.TB149_PARAM_INSTI.CO_CENT_CUSBIB = null;
                    tb000.TB149_PARAM_INSTI.CO_CTABIB_EMP = null;
                    tb000.TB149_PARAM_INSTI.CO_CENT_CUSMAT = null;
                    tb000.TB149_PARAM_INSTI.CO_CTAMAT_EMP = null;
                    tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_ATIVI_EXTRA = null;
                    tb000.TB149_PARAM_INSTI.CO_CTA_ATIVI_EXTRA = null;
                    tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_CAIXA = null;
                    tb000.TB149_PARAM_INSTI.CO_CTA_CAIXA = null;
                    tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_BANCO = null;
                    tb000.TB149_PARAM_INSTI.CO_CTA_BANCO = null;
                }

//------------> Informações de Controle de Mensagens SMS
                tb000.TB149_PARAM_INSTI.TP_CTRLE_MENSA_SMS = ddlTipoControleMensaSMS.SelectedValue;

                tb000.TB149_PARAM_INSTI.FL_ENVIO_SMS = chkEnvioSMS.Checked ? "S" : "N";
                if (chkEnvioSMS.Checked)
	            {
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_PROFE = txtQtdMaxProfe.Text != "" ? (int?)int.Parse(txtQtdMaxProfe.Text) : null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_FUNCI = txtQtdMaxFunci.Text != "" ? (int?)int.Parse(txtQtdMaxFunci.Text) : null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_RESPO = txtQtdMaxRespo.Text != "" ? (int?)int.Parse(txtQtdMaxRespo.Text) : null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_ALUNO = txtQtdMaxAluno.Text != "" ? (int?)int.Parse(txtQtdMaxAluno.Text) : null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_OUTRO = txtQtdMaxOutros.Text != "" ? (int?)int.Parse(txtQtdMaxOutros.Text) : null;
	            }
                else
                {
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_PROFE = null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_FUNCI = null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_RESPO = null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_ALUNO = null;
                    tb000.TB149_PARAM_INSTI.QT_MAX_SMS_OUTRO = null;
                }
                
                if (ddlTipoControleMensaSMS.SelectedValue == TipoControle.I.ToString())
                {
                    tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_SOLIC = chkSMSSolicSecreEscol.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_SOLIC = ddlFlagSMSSolicEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_SOLIC = txtMsgSMSSolic.Text != "" ? txtMsgSMSSolic.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_ENTRE = chkSMSEntreSecreEscol.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_ENTRE = ddlFlagSMSEntreEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_ENTRE = txtMsgSMSEntre.Text != "" ? txtMsgSMSEntre.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_OUTRO = chkSMSOutroSecreEscol.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_OUTRO = ddlFlagSMSOutroEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_OUTRO = txtMsgSMSOutro.Text != "" ? txtMsgSMSOutro.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_RESER = chkSMSReserVagas.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RESER = ddlFlagSMSReserVagasEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RESER = txtMsgSMSReserVagas.Text != "" ? txtMsgSMSReserVagas.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_RENOV = chkSMSRenovMatri.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RENOV = ddlFlagSMSRenovMatriEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RENOV = txtMsgSMSRenovMatri.Text != "" ? txtMsgSMSRenovMatri.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_NOVA = chkSMSMatriNova.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_NOVA = ddlFlagSMSMatriNovaEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_NOVA = txtMsgSMSMatriNova.Text != "" ? txtMsgSMSMatriNova.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_RESER = chkSMSReserBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_RESER = ddlFlagSMSReserBibliEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_RESER = txtMsgSMSReserBibli.Text != "" ? txtMsgSMSReserBibli.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_EMPRE = chkSMSEmpreBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE = ddlFlagSMSEmpreBibliEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_EMPRE = txtMsgSMSEmpreBibli.Text != "" ? txtMsgSMSEmpreBibli.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_DIVER = chkSMSDiverBibli.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_DIVER = ddlFlagSMSDiverBibliEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_DIVER = txtMsgSMSDiverBibli.Text != "" ? txtMsgSMSDiverBibli.Text : null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_FALTA_ALUNO = chkSMSFaltaAluno.Checked ? "S" : "N";
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_FALTA_ALUNO = ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue;
                    tb000.TB149_PARAM_INSTI.DES_SMS_FALTA_ALUNO = txtMsgSMSFaltaAluno.Text != "" ? txtMsgSMSFaltaAluno.Text : null;
                }
                else
                {
                    tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_SOLIC = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_SOLIC = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_SOLIC = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_ENTRE = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_ENTRE = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_ENTRE = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_OUTRO = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_OUTRO = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_OUTRO = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_RESER = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RESER = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RESER = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_RENOV = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RENOV = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RENOV = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_NOVA = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_NOVA = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_NOVA = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_RESER = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_RESER = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_RESER = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_EMPRE = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_EMPRE = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_DIVER = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_DIVER = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_DIVER = null;
                    tb000.TB149_PARAM_INSTI.FL_SMS_FALTA_ALUNO = null;
                    tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_FALTA_ALUNO = null;
                    tb000.TB149_PARAM_INSTI.DES_SMS_FALTA_ALUNO = null;
                }                

                CurrentPadraoCadastros.CurrentEntity = tb000;
            }
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB000_INSTITUICAO tb000 = RetornaEntidade();

            if (tb000 != null)
            {
                tb000.Image3Reference.Load();

                if (tb000.Image3 != null)
                    imgOrgaoIIE.CarregaImagem(tb000.Image3.ImageId);
                else
                    imgOrgaoIIE.CarregaImagem(0);

                tb000.TB905_BAIRROReference.Load();
                tb000.TB_NUCLEO_INSTReference.Load();

                txtCNPJIUE.Text = tb000.ORG_NUMERO_CNPJ.ToString().PadLeft(14,'0');
                txtNomeIIE.Text = tb000.ORG_NOME_ORGAO;
                txtNomeFantaIIE.Text = tb000.ORG_NOME_FANTAS_ORGAO;
                txtSiglaIIE.Text = tb000.ORG_NOME_REDUZI;
                ddlNucleoIUE.SelectedValue = tb000.TB_NUCLEO_INST != null ? tb000.TB_NUCLEO_INST.CO_NUCLEO.ToString() : "";
                txtNumContr.Text = tb000.ORG_NUMERO_CONTR != null ? tb000.ORG_NUMERO_CONTR : "";
                txtDtIniContr.Text = tb000.DT_INI_CONTR != null ? tb000.DT_INI_CONTR.Value.ToString("dd/MM/yyyy") : "";
                txtDtFimContr.Text = tb000.DT_FIM_CONTR != null ? tb000.DT_FIM_CONTR.Value.ToString("dd/MM/yyyy") : "";
                txtInscEstadualIIE.Text = tb000.ORG_CO_INS_ESTA_ORGAO != null ? tb000.ORG_CO_INS_ESTA_ORGAO : "";
                txtNISIIE.Text = tb000.ORG_NU_NIS_ORGAO != null ? tb000.ORG_NU_NIS_ORGAO.ToString() : "";
                txtNomeRespon.Text = tb000.NO_RESPO_CONTR != null ? tb000.NO_RESPO_CONTR : "";
                txtCPFRespon.Text = tb000.NU_CPF_RESPO_CONTR != null ? tb000.NU_CPF_RESPO_CONTR : "";
                txtDtNasctoRespon.Text = tb000.DT_NASCTO_RESPO_CONTR != null ? tb000.DT_NASCTO_RESPO_CONTR.Value.ToString("dd/MM/yyyy") : "";
                txtTelFixoRespo.Text = tb000.NU_TELE_FIXO_RESPO_CONTR != null ? tb000.NU_TELE_FIXO_RESPO_CONTR : "";
                txtTelCeluRespo.Text = tb000.NU_TELE_CELU_RESPO_CONTR != null ? tb000.NU_TELE_CELU_RESPO_CONTR : "";
                txtEmailRespo.Text = tb000.DE_EMAIL_RESPO_CONTR != null ? tb000.DE_EMAIL_RESPO_CONTR : "";
                txtCEPIIE.Text = tb000.CEP_CODIGO != null ? tb000.CEP_CODIGO.Value.ToString() : "";
                txtLogradouroIIE.Text = tb000.ORG_ENDERE_ORGAO;
                txtNumeroIIE.Text = tb000.ORG_ENDERE_NUMERO;
                txtComplementoIIE.Text = tb000.ORG_ENDERE_COMPLE;
                ddlUFIIE.SelectedValue = tb000.CID_CODIGO_UF;

//------------> Carrega Informações da Cidade e Bairro da Instituição
                if (tb000.TB905_BAIRRO != null)
                {
                    CarregaCidades();
                    ddlCidadeIIE.SelectedValue = tb000.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros();
                    ddlBairroIIE.SelectedValue = tb000.TB905_BAIRRO.CO_BAIRRO.ToString();
                }

                txtLatitude.Text = tb000.ORG_CO_GEORE_LATIT_ORGAO != null ? tb000.ORG_CO_GEORE_LATIT_ORGAO : "";
                txtLongitude.Text = tb000.ORG_CO_GEORE_LONGI_ORGAO != null ? tb000.ORG_CO_GEORE_LONGI_ORGAO : "";

                txtTelefoneIIE.Text = tb000.ORG_NUMERO_FONE1 != null ? tb000.ORG_NUMERO_FONE1.Value.ToString() : "";
                txtTelefone2IIE.Text = tb000.ORG_NUMERO_FONE2 != null ? tb000.ORG_NUMERO_FONE2.Value.ToString() : "";
                txtEmailIIE.Text = tb000.ORG_EMAIL_CONTAT;
                txtFaxIIE.Text = tb000.ORG_NUMERO_FAX1 != null ? tb000.ORG_NUMERO_FAX1.Value.ToString() : "";
                txtWebSiteIIE.Text = tb000.ORG_HOME_PAGE;
                txtObservacaoIIE.Text = tb000.ORG_DE_OBS_ORGAO;
                txtNumDocto.Text = tb000.ORG_CO_DOCTO_CONST_ORGAO;
                ddlSitucaoIIE.SelectedValue = tb000.ORG_STATUS_CODIGO;
                txtDataConstituicao.Text = tb000.ORG_DT_CONST_ORGAO != null ? tb000.ORG_DT_CONST_ORGAO.Value.ToString("dd/MM/yyyy") : "";
                txtDtStatusIIE.Text = tb000.ORG_STATUS_DATA != null ? tb000.ORG_STATUS_DATA.Value.ToString("dd/MM/yyyy") : "";
                txtDtCadastroIIE.Text = tb000.ORG_DATA_CADAST.ToString("dd/MM/yyyy");

//------------> Carrega informações da frequência funcional de acordo com a Instituição selecionada
                var tb300 = TB300_QUADRO_HORAR_FUNCI.RetornaPelaInstituicao(tb000.ORG_CODIGO_ORGAO);
                int indice = 0;
                if (tb300 != null)
                {
                    txtTipoCtrlFrequ.Text = "Instituição de Ensino";
                    ddlPermiMultiFrequ.Enabled = true;

                    foreach (var iTb300 in tb300)
                    {
                        if (indice == 0)
                        {
                            txtLimiteEntHTP1.Text = iTb300.HR_LIMIT_ENTRA != null ? iTb300.HR_LIMIT_ENTRA : "";
                            txtTurno1EntHTP1.Text = iTb300.HR_ENTRA_TURNO1 != null ? iTb300.HR_ENTRA_TURNO1 : "";
                            txtTurno1SaiHTP1.Text = iTb300.HR_SAIDA_TURNO1 != null ? iTb300.HR_SAIDA_TURNO1 : "";
                            txtInterEntHTP1.Text = iTb300.HR_ENTRA_INTER != null ? iTb300.HR_ENTRA_INTER : "";
                            txtInterSaiHTP1.Text = iTb300.HR_SAIDA_INTER != null ? iTb300.HR_SAIDA_INTER : "";
                            txtTurno2EntHTP1.Text = iTb300.HR_ENTRA_TURNO2 != null ? iTb300.HR_ENTRA_TURNO2 : "";
                            txtTurno2SaiHTP1.Text = iTb300.HR_SAIDA_TURNO2 != null ? iTb300.HR_SAIDA_TURNO2 : "";
                            txtLimiteSaiHTP1.Text = iTb300.HR_LIMIT_SAIDA != null ? iTb300.HR_LIMIT_SAIDA : "";
                            txtExtraEntHTP1.Text = iTb300.HR_ENTRA_EXTRA != null ? iTb300.HR_ENTRA_EXTRA : "";
                            txtExtraSaiHTP1.Text = iTb300.HR_SAIDA_EXTRA != null ? iTb300.HR_SAIDA_EXTRA : "";
                            txtLimiteExtraSaiHTP1.Text = iTb300.HR_LIMIT_SAIDA_EXTRA != null ? iTb300.HR_LIMIT_SAIDA_EXTRA : "";
                        }

                        if (indice == 1)
                        {
                            txtLimiteEntHTP2.Text = iTb300.HR_LIMIT_ENTRA != null ? iTb300.HR_LIMIT_ENTRA : "";
                            txtTurno1EntHTP2.Text = iTb300.HR_ENTRA_TURNO1 != null ? iTb300.HR_ENTRA_TURNO1 : "";
                            txtTurno1SaiHTP2.Text = iTb300.HR_SAIDA_TURNO1 != null ? iTb300.HR_SAIDA_TURNO1 : "";
                            txtInterEntHTP2.Text = iTb300.HR_ENTRA_INTER != null ? iTb300.HR_ENTRA_INTER : "";
                            txtInterSaiHTP2.Text = iTb300.HR_SAIDA_INTER != null ? iTb300.HR_SAIDA_INTER : "";
                            txtTurno2EntHTP2.Text = iTb300.HR_ENTRA_TURNO2 != null ? iTb300.HR_ENTRA_TURNO2 : "";
                            txtTurno2SaiHTP2.Text = iTb300.HR_SAIDA_TURNO2 != null ? iTb300.HR_SAIDA_TURNO2 : "";
                            txtLimiteSaiHTP2.Text = iTb300.HR_LIMIT_SAIDA != null ? iTb300.HR_LIMIT_SAIDA : "";
                            txtExtraEntHTP2.Text = iTb300.HR_ENTRA_EXTRA != null ? iTb300.HR_ENTRA_EXTRA : "";
                            txtExtraSaiHTP2.Text = iTb300.HR_SAIDA_EXTRA != null ? iTb300.HR_SAIDA_EXTRA : "";
                            txtLimiteExtraSaiHTP2.Text = iTb300.HR_LIMIT_SAIDA_EXTRA != null ? iTb300.HR_LIMIT_SAIDA_EXTRA : "";
                        }

                        if (indice == 2)
                        {
                            txtLimiteEntHTP3.Text = iTb300.HR_LIMIT_ENTRA != null ? iTb300.HR_LIMIT_ENTRA : "";
                            txtTurno1EntHTP3.Text = iTb300.HR_ENTRA_TURNO1 != null ? iTb300.HR_ENTRA_TURNO1 : "";
                            txtTurno1SaiHTP3.Text = iTb300.HR_SAIDA_TURNO1 != null ? iTb300.HR_SAIDA_TURNO1 : "";
                            txtInterEntHTP3.Text = iTb300.HR_ENTRA_INTER != null ? iTb300.HR_ENTRA_INTER : "";
                            txtInterSaiHTP3.Text = iTb300.HR_SAIDA_INTER != null ? iTb300.HR_SAIDA_INTER : "";
                            txtTurno2EntHTP3.Text = iTb300.HR_ENTRA_TURNO2 != null ? iTb300.HR_ENTRA_TURNO2 : "";
                            txtTurno2SaiHTP3.Text = iTb300.HR_SAIDA_TURNO2 != null ? iTb300.HR_SAIDA_TURNO2 : "";
                            txtLimiteSaiHTP3.Text = iTb300.HR_LIMIT_SAIDA != null ? iTb300.HR_LIMIT_SAIDA : "";
                            txtExtraEntHTP3.Text = iTb300.HR_ENTRA_EXTRA != null ? iTb300.HR_ENTRA_EXTRA : "";
                            txtExtraSaiHTP3.Text = iTb300.HR_SAIDA_EXTRA != null ? iTb300.HR_SAIDA_EXTRA : "";
                            txtLimiteExtraSaiHTP3.Text = iTb300.HR_LIMIT_SAIDA_EXTRA != null ? iTb300.HR_LIMIT_SAIDA_EXTRA : "";
                        }

                        if (indice == 3)
                        {
                            txtLimiteEntHTP4.Text = iTb300.HR_LIMIT_ENTRA != null ? iTb300.HR_LIMIT_ENTRA : "";
                            txtTurno1EntHTP4.Text = iTb300.HR_ENTRA_TURNO1 != null ? iTb300.HR_ENTRA_TURNO1 : "";
                            txtTurno1SaiHTP4.Text = iTb300.HR_SAIDA_TURNO1 != null ? iTb300.HR_SAIDA_TURNO1 : "";
                            txtInterEntHTP4.Text = iTb300.HR_ENTRA_INTER != null ? iTb300.HR_ENTRA_INTER : "";
                            txtInterSaiHTP4.Text = iTb300.HR_SAIDA_INTER != null ? iTb300.HR_SAIDA_INTER : "";
                            txtTurno2EntHTP4.Text = iTb300.HR_ENTRA_TURNO2 != null ? iTb300.HR_ENTRA_TURNO2 : "";
                            txtTurno2SaiHTP4.Text = iTb300.HR_SAIDA_TURNO2 != null ? iTb300.HR_SAIDA_TURNO2 : "";
                            txtLimiteSaiHTP4.Text = iTb300.HR_LIMIT_SAIDA != null ? iTb300.HR_LIMIT_SAIDA : "";
                            txtExtraEntHTP4.Text = iTb300.HR_ENTRA_EXTRA != null ? iTb300.HR_ENTRA_EXTRA : "";
                            txtExtraSaiHTP4.Text = iTb300.HR_SAIDA_EXTRA != null ? iTb300.HR_SAIDA_EXTRA : "";
                            txtLimiteExtraSaiHTP4.Text = iTb300.HR_LIMIT_SAIDA_EXTRA != null ? iTb300.HR_LIMIT_SAIDA_EXTRA : "";
                        }

                        if (indice == 4)
                        {
                            txtLimiteEntHTP5.Text = iTb300.HR_LIMIT_ENTRA != null ? iTb300.HR_LIMIT_ENTRA : "";
                            txtTurno1EntHTP5.Text = iTb300.HR_ENTRA_TURNO1 != null ? iTb300.HR_ENTRA_TURNO1 : "";
                            txtTurno1SaiHTP5.Text = iTb300.HR_SAIDA_TURNO1 != null ? iTb300.HR_SAIDA_TURNO1 : "";
                            txtInterEntHTP5.Text = iTb300.HR_ENTRA_INTER != null ? iTb300.HR_ENTRA_INTER : "";
                            txtInterSaiHTP5.Text = iTb300.HR_SAIDA_INTER != null ? iTb300.HR_SAIDA_INTER : "";
                            txtTurno2EntHTP5.Text = iTb300.HR_ENTRA_TURNO2 != null ? iTb300.HR_ENTRA_TURNO2 : "";
                            txtTurno2SaiHTP5.Text = iTb300.HR_SAIDA_TURNO2 != null ? iTb300.HR_SAIDA_TURNO2 : "";
                            txtLimiteSaiHTP5.Text = iTb300.HR_LIMIT_SAIDA != null ? iTb300.HR_LIMIT_SAIDA : "";
                            txtExtraEntHTP5.Text = iTb300.HR_ENTRA_EXTRA != null ? iTb300.HR_ENTRA_EXTRA : "";
                            txtExtraSaiHTP5.Text = iTb300.HR_SAIDA_EXTRA != null ? iTb300.HR_SAIDA_EXTRA : "";
                            txtLimiteExtraSaiHTP5.Text = iTb300.HR_LIMIT_SAIDA_EXTRA != null ? iTb300.HR_LIMIT_SAIDA_EXTRA : "";
                        }

                        indice++;
                    }
                }
                else
                {
                    txtTipoCtrlFrequ.Text = "Unidade Escolar";
                    ddlPermiMultiFrequ.Enabled = false;
                }

                
//------------> Carrega dados da tabela TB149_PARAM_INSTI no formulário
                tb000.TB149_PARAM_INSTIReference.Load();

                if (tb000.TB149_PARAM_INSTI != null)
                {
                    ddlPermiMultiFrequ.SelectedValue = tb000.TB149_PARAM_INSTI.FL_MULTI_FREQU != null ? tb000.TB149_PARAM_INSTI.FL_MULTI_FREQU : "S";

                    txtHoraIniTurno1.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_MANHA_INICI;
                    txtHoraFimTurno1.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_MANHA_FINAL;
                    txtHoraIniTurno2.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_TARDE_INICI;
                    txtHoraFimTurno2.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_TARDE_FINAL;
                    txtHoraIniTurno3.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_NOITE_INICI;
                    txtHoraFimTurno3.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_NOITE_FINAL;
                    txtHoraIniTurno4.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_ULTIM_TURNO_INICI;
                    txtHoraFimTurno4.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_ULTIM_TURNO_FINAL;

                    ddlUsoLogo.SelectedValue = tb000.TB149_PARAM_INSTI.TP_USO_LOGO != null ? tb000.TB149_PARAM_INSTI.TP_USO_LOGO : "I";
//----------------> Preenche informaçoes de Quem Somos; Nossa História; Proposta Pedagógica   
                    ddlTipoCtrlDescr.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_DESCR != null ? tb000.TB149_PARAM_INSTI.TP_CTRLE_DESCR : "I";
                    txtQuemSomos.Html = tb000.TB149_PARAM_INSTI.DES_QUEM_SOMOS != null ? tb000.TB149_PARAM_INSTI.DES_QUEM_SOMOS : "";
                    txtTipoCtrlPropoPedag.Text = txtTipoCtrlNossaHisto.Text = ddlTipoCtrlDescr.SelectedValue == "I" ? "Instituição de Ensino" : "Unidade Escolar";
                    txtNossaHisto.Html = tb000.TB149_PARAM_INSTI.DES_NOSSA_HISTO != null ? tb000.TB149_PARAM_INSTI.DES_NOSSA_HISTO : "";
                    txtPropoPedag.Html = tb000.TB149_PARAM_INSTI.DES_PROPO_PEDAG != null ? tb000.TB149_PARAM_INSTI.DES_PROPO_PEDAG : "";

//----------------> Controle Pedagógico/Matrículas *******************

//----------------> Carrega o tipo de Controle dos Parâmetros de Avaliação
                    ddlTipoCtrleMetod.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_METOD;
                    if (ddlTipoCtrleMetod.SelectedValue == TipoControle.I.ToString())
                    {
                        ddlMetodEnsino.SelectedValue = tb000.TB149_PARAM_INSTI.TP_ENSINO != null ? tb000.TB149_PARAM_INSTI.TP_ENSINO : "S";
                        ddlFormaAvali.SelectedValue = tb000.TB149_PARAM_INSTI.TP_FORMA_AVAL != null ? tb000.TB149_PARAM_INSTI.TP_FORMA_AVAL : "N";

                        if (ddlFormaAvali.SelectedValue == "C")
                        {
//------------------------> Carrega informações de Conceito de acordo com a instituição selecionada
                            var tb200 = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                         where iTb200.ORG_CODIGO_ORGAO == tb000.ORG_CODIGO_ORGAO && iTb200.TB25_EMPRESA == null
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
                        ddlMetodEnsino.Enabled = ddlFormaAvali.Enabled = false;
                    }

                    ddlTipoCtrleAval.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_AVAL;


//----------------> Faz a verificação para saber se o controle da Avaliação é pela Instituição
                    if (ddlTipoCtrleAval.SelectedValue == TipoControle.I.ToString())
                    {
                        ddlPerioAval.SelectedValue = tb000.TB149_PARAM_INSTI.TP_PERIOD_AVAL;

                        txtMediaAprovGeral.Text = tb000.TB149_PARAM_INSTI.VL_MEDIA_CURSO != null ? tb000.TB149_PARAM_INSTI.VL_MEDIA_CURSO.Value.ToString() : "";
                        txtMediaAprovDireta.Text = tb000.TB149_PARAM_INSTI.VL_MEDIA_APROV_DIRETA != null ? tb000.TB149_PARAM_INSTI.VL_MEDIA_APROV_DIRETA.Value.ToString() : "";
                        txtMediaProvaFinal.Text = tb000.TB149_PARAM_INSTI.VL_MEDIA_PROVA_FINAL != null ? tb000.TB149_PARAM_INSTI.VL_MEDIA_PROVA_FINAL.Value.ToString() : "";

                        chkRecuperEscol.Checked = tb000.TB149_PARAM_INSTI.FL_RECUPER != null ? tb000.TB149_PARAM_INSTI.FL_RECUPER.Value : false;
                        txtMediaRecuperEscol.Enabled = txtQtdeMaterRecuper.Enabled = chkRecuperEscol.Checked;
                        txtMediaRecuperEscol.Text = tb000.TB149_PARAM_INSTI.VL_MEDIA_RECUPER != null ? tb000.TB149_PARAM_INSTI.VL_MEDIA_RECUPER.Value.ToString() : "";
                        txtQtdeMaterRecuper.Text = tb000.TB149_PARAM_INSTI.QT_MATERIAS_RECUPER != null ? tb000.TB149_PARAM_INSTI.QT_MATERIAS_RECUPER.Value.ToString() : "";

                        chkDepenEscol.Checked = tb000.TB149_PARAM_INSTI.FL_DEPEND != null ? tb000.TB149_PARAM_INSTI.FL_DEPEND.Value : false;
                        txtMediaDepenEscol.Enabled = txtQtdMaterDepenEscol.Enabled = chkDepenEscol.Checked;
                        txtMediaDepenEscol.Text = tb000.TB149_PARAM_INSTI.VL_MEDIA_DEPEND != null ? tb000.TB149_PARAM_INSTI.VL_MEDIA_DEPEND.Value.ToString() : "";
                        txtQtdMaterDepenEscol.Text = tb000.TB149_PARAM_INSTI.QT_MATERIAS_DEPEND != null ? tb000.TB149_PARAM_INSTI.QT_MATERIAS_DEPEND.Value.ToString() : "";

                        chkConselho.Checked = tb000.TB149_PARAM_INSTI.FL_CONSELHO != null ? tb000.TB149_PARAM_INSTI.FL_CONSELHO.Value : false;
                        txtLimitMediaConseEscol.Enabled = txtQtdMaxMaterConse.Enabled = chkConselho.Checked;
                        txtLimitMediaConseEscol.Text = tb000.TB149_PARAM_INSTI.VL_LIMIT_MEDIA_CONSELHO != null ? tb000.TB149_PARAM_INSTI.VL_LIMIT_MEDIA_CONSELHO.Value.ToString() : "";                        
                        txtQtdMaxMaterConse.Text = tb000.TB149_PARAM_INSTI.QT_MATERIAS_CONSELHO != null ? tb000.TB149_PARAM_INSTI.QT_MATERIAS_CONSELHO.Value.ToString() : "";                         
                    }
                    else
                    {
                        ddlPerioAval.Enabled = txtMediaAprovGeral.Enabled = txtMediaAprovDireta.Enabled = txtMediaProvaFinal.Enabled = chkRecuperEscol.Enabled =
                        txtMediaRecuperEscol.Enabled = txtQtdeMaterRecuper.Enabled = chkDepenEscol.Enabled = txtMediaDepenEscol.Enabled =
                        txtQtdMaterDepenEscol.Enabled = chkConselho.Enabled = txtLimitMediaConseEscol.Enabled = txtQtdMaxMaterConse.Enabled = false;
                    }

                    ddlTipoCtrleDatas.SelectedValue = tb000.TB149_PARAM_INSTI.FLA_CTRL_DATA;

                    if (ddlTipoCtrleDatas.SelectedValue == TipoControle.I.ToString())
                    {
                        txtReservaMatriculaDtInicioIUE.Text = tb000.TB149_PARAM_INSTI.DT_INI_RES != null ? tb000.TB149_PARAM_INSTI.DT_INI_RES.Value.ToString("dd/MM/yyyy") : "";
                        txtReservaMatriculaDtFimIUE.Text = tb000.TB149_PARAM_INSTI.DT_FIM_RES != null ? tb000.TB149_PARAM_INSTI.DT_FIM_RES.Value.ToString("dd/MM/yyyy") : "";
                        txtRematriculaInicioIUE.Text = tb000.TB149_PARAM_INSTI.DT_INI_REMAT != null ? tb000.TB149_PARAM_INSTI.DT_INI_REMAT.Value.ToString("dd/MM/yyyy") : "";
                        txtRematriculaFimIUE.Text = tb000.TB149_PARAM_INSTI.DT_FIM_REMAT != null ? tb000.TB149_PARAM_INSTI.DT_FIM_REMAT.Value.ToString("dd/MM/yyyy") : "";
                        txtMatriculaInicioIUE.Text = tb000.TB149_PARAM_INSTI.DT_INI_MAT != null ? tb000.TB149_PARAM_INSTI.DT_INI_MAT.Value.ToString("dd/MM/yyyy") : "";
                        txtMatriculaFimIUE.Text = tb000.TB149_PARAM_INSTI.DT_FIM_MAT != null ? tb000.TB149_PARAM_INSTI.DT_FIM_MAT.Value.ToString("dd/MM/yyyy") : "";
                        txtDataRemanAlunoIni.Text = tb000.TB149_PARAM_INSTI.DT_INI_REMAN_ALUNO != null ? tb000.TB149_PARAM_INSTI.DT_INI_REMAN_ALUNO.Value.ToString("dd/MM/yyyy") : "";
                        txtDataRemanAlunoFim.Text = tb000.TB149_PARAM_INSTI.DT_FIM_REMAN_ALUNO != null ? tb000.TB149_PARAM_INSTI.DT_FIM_REMAN_ALUNO.Value.ToString("dd/MM/yyyy") : "";
                        txtDtInicioTransInter.Text = tb000.TB149_PARAM_INSTI.DT_INI_TRANS_INTER != null ? tb000.TB149_PARAM_INSTI.DT_INI_TRANS_INTER.Value.ToString("dd/MM/yyyy") : "";
                        txtDtFimTransInter.Text = tb000.TB149_PARAM_INSTI.DT_FIM_TRANS_INTER != null ? tb000.TB149_PARAM_INSTI.DT_FIM_TRANS_INTER.Value.ToString("dd/MM/yyyy") : "";
                        txtTrancamentoMatriculaInicioIUE.Text = tb000.TB149_PARAM_INSTI.DT_INI_TRAN != null ? tb000.TB149_PARAM_INSTI.DT_INI_TRAN.Value.ToString("dd/MM/yyyy") : "";
                        txtTrancamentoMatriculaFimIUE.Text = tb000.TB149_PARAM_INSTI.DT_FIM_TRAN != null ? tb000.TB149_PARAM_INSTI.DT_FIM_TRAN.Value.ToString("dd/MM/yyyy") : "";
                        txtAlteracaoMatriculaInicioIUE.Text = tb000.TB149_PARAM_INSTI.DT_INI_ALTMAT != null ? tb000.TB149_PARAM_INSTI.DT_INI_ALTMAT.Value.ToString("dd/MM/yyyy") : "";
                        txtAlteracaoMatriculaFimIUE.Text = tb000.TB149_PARAM_INSTI.DT_FIM_ALTMAT != null ? tb000.TB149_PARAM_INSTI.DT_FIM_ALTMAT.Value.ToString("dd/MM/yyyy") : "";
                    }
                    else
                    {
                        txtReservaMatriculaDtInicioIUE.Enabled = txtReservaMatriculaDtFimIUE.Enabled = txtRematriculaInicioIUE.Enabled =
                        txtRematriculaFimIUE.Enabled = txtMatriculaInicioIUE.Enabled = txtMatriculaFimIUE.Enabled = txtDataRemanAlunoIni.Enabled =
                        txtDataRemanAlunoFim.Enabled = txtDtInicioTransInter.Enabled = txtDtFimTransInter.Enabled = txtTrancamentoMatriculaInicioIUE.Enabled =
                        txtTrancamentoMatriculaFimIUE.Enabled = txtAlteracaoMatriculaInicioIUE.Enabled = txtAlteracaoMatriculaFimIUE.Enabled = false;
                    }
//***************************************

//----------------> Controle Secretaria Escolar *******************
                    ddlTipoCtrlSecreEscol.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL;

//----------------> Faz a verificação para saber se o controle de Secretaria Escolar é pela Instituição
                    if (ddlTipoCtrlSecreEscol.SelectedValue == TipoControle.I.ToString())
                    {
                        chkUsuarFunc.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_FUNCI_SECRE == "S";
                        chkUsuarProf.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_PROFE_SECRE == "S";
                        chkUsuarAluno.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_ALUNO_SECRE == "S";
                        txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarAluno.Checked;
                        txtIdadeMinimAlunoSecreEscol.Text = tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_SECRE != null ? tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_SECRE.Value.ToString() : "";
                        chkUsuarPaisRespo.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_RESPO_SECRE == "S";
                        chkUsuarOutro.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_OUTRO_SECRE == "S";

                        txtHorarIniT1SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_SECRE : "";
                        txtHorarFimT1SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_SECRE : "";
                        txtHorarIniT2SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_SECRE : "";
                        txtHorarFimT2SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_SECRE : "";
                        txtHorarIniT3SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_SECRE : "";
                        txtHorarFimT3SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_SECRE : "";
                        txtHorarIniT4SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_SECRE : "";
                        txtHorarFimT4SecreEscol.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_SECRE != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_SECRE : "";

                        ddlGeraNumSolicAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_SECRE != null ? tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_SECRE : "N";
                        txtNumIniciSolicAuto.Enabled = ddlGeraNumSolicAuto.SelectedValue == "S";
                        txtNumIniciSolicAuto.Text = tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_SECRE != null ? tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_SECRE.Value.ToString() : "";
                        ddlContrPrazEntre.SelectedValue = tb000.TB149_PARAM_INSTI.FL_CTRLE_PRAZO_ENT_SECRE != null ? tb000.TB149_PARAM_INSTI.FL_CTRLE_PRAZO_ENT_SECRE : "N";
                        txtQtdeDiasEntreSolic.Enabled = ddlAlterPrazoEntreSolic.Enabled = ddlContrPrazEntre.SelectedValue == "S";
                        txtQtdeDiasEntreSolic.Text = tb000.TB149_PARAM_INSTI.NU_DIAS_ENT_SOL != null ? tb000.TB149_PARAM_INSTI.NU_DIAS_ENT_SOL.Value.ToString() : "";
                        ddlAlterPrazoEntreSolic.SelectedValue = tb000.TB149_PARAM_INSTI.FLA_CTRLE_DIAS_SOLIC != null ? tb000.TB149_PARAM_INSTI.FLA_CTRLE_DIAS_SOLIC : "N";

                        ddlFlagApresValorServi.SelectedValue = tb000.TB149_PARAM_INSTI.FL_APRE_VALOR_SERV_SECRE != null ? tb000.TB149_PARAM_INSTI.FL_APRE_VALOR_SERV_SECRE : "N";
                        ddlAbonaValorServiSolic.Enabled = ddlApresValorServiSolic.Enabled = ddlFlagApresValorServi.SelectedValue == "S";
                        ddlAbonaValorServiSolic.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ABONA_VALOR_SERV_SECRE != null ? tb000.TB149_PARAM_INSTI.FL_ABONA_VALOR_SERV_SECRE : "N";
                        ddlApresValorServiSolic.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ALTER_VALOR_SERV_SECRE != null ? tb000.TB149_PARAM_INSTI.FL_ALTER_VALOR_SERV_SECRE : "N";

                        ddlFlagIncluContaReceb.SelectedValue = tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_SECRE != null ? tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_SECRE : "N";
                        if (ddlFlagIncluContaReceb.SelectedValue == "N")
                        {
                            ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = ddlFlagTipoJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                             ddlFlagTipoMultaSecre.Enabled = false;
                        }
                        else
                        {
                            ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = ddlFlagTipoJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                             ddlFlagTipoMultaSecre.Enabled = true;
                            ddlGeraBoletoServiSecre.SelectedValue = tb000.TB149_PARAM_INSTI.CO_FLAG_GERA_BOLETO_SERV_SECR != null ? tb000.TB149_PARAM_INSTI.CO_FLAG_GERA_BOLETO_SERV_SECR : "N";
                            ddlTipoBoletoServiSecre.Enabled = ddlGeraBoletoServiSecre.SelectedValue == "S";
                            ddlTipoBoletoServiSecre.SelectedValue = tb000.TB149_PARAM_INSTI.TP_BOLETO_BANC != null ? tb000.TB149_PARAM_INSTI.TP_BOLETO_BANC : "";
                            txtVlJurosSecre.Text = tb000.TB149_PARAM_INSTI.VL_PEC_JUROS != null ? tb000.TB149_PARAM_INSTI.VL_PEC_JUROS.ToString() : "";
                            ddlFlagTipoJurosSecre.SelectedValue = tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_JUROS != null ? tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_JUROS : "V";
                            txtVlMultaSecre.Text = tb000.TB149_PARAM_INSTI.VL_PEC_MULTA != null ? tb000.TB149_PARAM_INSTI.VL_PEC_MULTA.ToString() : "";
                            ddlFlagTipoMultaSecre.SelectedValue = tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_MULTA != null ? tb000.TB149_PARAM_INSTI.CO_FLAG_TP_VALOR_MULTA : "V";
                        }
                        //txtCabec1Relatorio.Text = tb000.TB149_PARAM_INSTI.cablinha1;
                        //txtCabec2Relatorio.Text = tb000.TB149_PARAM_INSTI.cablinha2;
                        //txtCabec3Relatorio.Text = tb000.TB149_PARAM_INSTI.cablinha3;
                        //txtCabec4Relatorio.Text = tb000.TB149_PARAM_INSTI.cablinha4;

//--------------------> Secretário(s)
                        if (tb000.TB149_PARAM_INSTI.TB03_COLABOR != null)
                        {
                            ddlSiglaUnidSecreEscol1.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR.CO_EMP.ToString();
                            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol1, ddlNomeSecreEscol1);
                            ddlNomeSecreEscol1.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR.CO_COL.ToString();
                            ddlClassifSecre1.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE1 != null ? tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE1.ToString() : "1";
                        }

                        if (tb000.TB149_PARAM_INSTI.TB03_COLABOR2 != null)
                        {
                            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol2);
                            ddlSiglaUnidSecreEscol2.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR2.CO_EMP.ToString();
                            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol2, ddlNomeSecreEscol2);
                            ddlNomeSecreEscol2.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR2.CO_COL.ToString();
                            ddlClassifSecre2.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE2 != null ? tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE2.ToString() : "2";
                        }

                        if (tb000.TB149_PARAM_INSTI.TB03_COLABOR3 != null)
                        {
                            CarregaUnidadeNomeSecreEscol(ddlSiglaUnidSecreEscol3);
                            ddlSiglaUnidSecreEscol3.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR3.CO_EMP.ToString();
                            CarregaSecretarioEscolar(ddlSiglaUnidSecreEscol3, ddlNomeSecreEscol3);
                            ddlNomeSecreEscol3.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR3.CO_COL.ToString();
                            ddlClassifSecre3.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE3 != null ? tb000.TB149_PARAM_INSTI.CO_CLASS_SECRE3.ToString() : "3";
                        }
                    }
                    else
                    {
                        chkUsuarFunc.Enabled = chkUsuarProf.Enabled = chkUsuarAluno.Enabled = txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarPaisRespo.Enabled =
                        chkUsuarOutro.Enabled = txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled =
                        txtHorarFimT2SecreEscol.Enabled = txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled = txtHorarIniT4SecreEscol.Enabled =
                        txtHorarFimT4SecreEscol.Enabled = ddlGeraNumSolicAuto.Enabled = txtNumIniciSolicAuto.Enabled = ddlContrPrazEntre.Enabled =
                        txtQtdeDiasEntreSolic.Enabled = ddlAlterPrazoEntreSolic.Enabled = ddlFlagApresValorServi.Enabled = ddlAbonaValorServiSolic.Enabled =
                        ddlApresValorServiSolic.Enabled = ddlFlagIncluContaReceb.Enabled = ddlGeraBoletoServiSecre.Enabled = ddlTipoBoletoServiSecre.Enabled =
                        txtVlJurosSecre.Enabled = ddlFlagTipoJurosSecre.Enabled = txtVlMultaSecre.Enabled = ddlFlagTipoMultaSecre.Enabled =
                        //txtCabec1Relatorio.Enabled = txtCabec2Relatorio.Enabled = txtCabec3Relatorio.Enabled = txtCabec4Relatorio.Enabled = 
                        ddlSiglaUnidSecreEscol1.Enabled = ddlNomeSecreEscol1.Enabled = ddlClassifSecre1.Enabled = ddlSiglaUnidSecreEscol2.Enabled = 
                        ddlNomeSecreEscol2.Enabled = ddlClassifSecre2.Enabled = ddlSiglaUnidSecreEscol3.Enabled = ddlNomeSecreEscol3.Enabled = ddlClassifSecre3.Enabled = false;
                    }
//***************************************

//----------------> Controle Biblioteca Escolar *******************
                    ddlTipoCtrlBibliEscol.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_BIBLI;

//----------------> Faz a verificação para saber se o controle de Biblioteca Escolar é pela Instituição
                    if (ddlTipoCtrlBibliEscol.SelectedValue == TipoControle.I.ToString())
                    {
                        //Verificar se é para criar um novo campo ou alterar esse!
                        ddlFlagReserBibli.SelectedValue = tb000.TB149_PARAM_INSTI.FLA_RESER_OUTRA_UNID != null ? tb000.TB149_PARAM_INSTI.FLA_RESER_OUTRA_UNID : "N";
                        txtQtdeItensReser.Enabled = txtQtdeMaxDiasReser.Enabled = ddlFlagReserBibli.SelectedValue == "S";
                        txtQtdeItensReser.Text = tb000.TB149_PARAM_INSTI.QT_ITENS_ALUNO_BIBLI != null ? tb000.TB149_PARAM_INSTI.QT_ITENS_ALUNO_BIBLI.Value.ToString() : "";
                        txtQtdeMaxDiasReser.Text = tb000.TB149_PARAM_INSTI.QT_DIAS_RESER_BIBLI != null ? tb000.TB149_PARAM_INSTI.QT_DIAS_RESER_BIBLI.Value.ToString() : "";
                        //
                        ddlFlagEmpreBibli.SelectedValue = tb000.TB149_PARAM_INSTI.FL_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_EMPRE_BIBLI : "N";
                        txtQtdeItensEmpre.Enabled = txtQtdeMaxDiasEmpre.Enabled = ddlFlagEmpreBibli.SelectedValue == "S";
                        txtQtdeItensEmpre.Text = tb000.TB149_PARAM_INSTI.QT_ITENS_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.QT_ITENS_EMPRE_BIBLI.Value.ToString() : "";
                        txtQtdeMaxDiasEmpre.Text = tb000.TB149_PARAM_INSTI.QT_MAX_DIAS_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.QT_MAX_DIAS_EMPRE_BIBLI.Value.ToString() : "";

                        ddlGeraNumEmpreAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_NU_EMPRE_AUTO_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_NU_EMPRE_AUTO_BIBLI : "N";
                        txtNumIniciEmpreAuto.Enabled = ddlGeraNumEmpreAuto.SelectedValue == "S";
                        txtNumIniciEmpreAuto.Text = tb000.TB149_PARAM_INSTI.NU_EMPRE_INICI_BIBLI != null ? tb000.TB149_PARAM_INSTI.NU_EMPRE_INICI_BIBLI.Value.ToString() : "";

                        ddlFlagTaxaEmpre.SelectedValue = tb000.TB149_PARAM_INSTI.FL_TAXA_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_TAXA_EMPRE_BIBLI : "N";
                        txtDiasBonusTaxaEmpre.Enabled = txtValorTaxaEmpre.Enabled = ddlFlagTaxaEmpre.SelectedValue == "S";
                        txtDiasBonusTaxaEmpre.Text = tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_TAXA_EMPRE_BIBLI.Value.ToString() : "";
                        txtValorTaxaEmpre.Text = tb000.TB149_PARAM_INSTI.VL_TAXA_DIA_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.VL_TAXA_DIA_EMPRE_BIBLI.Value.ToString() : "";

                        ddlFlagMultaAtraso.SelectedValue = tb000.TB149_PARAM_INSTI.FL_MULTA_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_MULTA_EMPRE_BIBLI : "N";
                        txtDiasBonusMultaEmpre.Enabled = txtValorMultaEmpre.Enabled = ddlFlagMultaAtraso.SelectedValue == "S";
                        txtDiasBonusMultaEmpre.Text = tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI != null ? tb000.TB149_PARAM_INSTI.QT_DIAS_BONUS_MULTA_EMPRE_BIBLI.Value.ToString() : "";
                        txtValorMultaEmpre.Text = tb000.TB149_PARAM_INSTI.VL_MULTA_DIA_ATRASO_BIBLI != null ? tb000.TB149_PARAM_INSTI.VL_MULTA_DIA_ATRASO_BIBLI.Value.ToString() : "";

                        ddlGeraNumItemAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_NU_SOLIC_AUTO_BIBLI : "N";
                        txtNumIniciItemAuto.Enabled = ddlGeraNumItemAuto.SelectedValue == "S";
                        txtNumIniciItemAuto.Text = tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_BIBLI != null ? tb000.TB149_PARAM_INSTI.NU_SOLIC_INICI_BIBLI.Value.ToString() : "";

                        ddlNumISBNObrig.SelectedValue = tb000.TB149_PARAM_INSTI.FL_NU_ISBN_OBRIG_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_NU_ISBN_OBRIG_BIBLI : "N";
                        ddlFlarIncluContaRecebBibli.SelectedValue = tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_BIBLI != null ? tb000.TB149_PARAM_INSTI.FL_INCLU_TAXA_CAR_BIBLI : "N";

                        chkUsuarFuncBibli.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_FUNCI_BIBLI == "S";
                        chkUsuarProfBibli.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_PROFE_BIBLI == "S";
                        chkUsuarAlunoBibli.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_ALUNO_BIBLI == "S";
                        txtIdadeMinimAlunoBibli.Enabled = chkUsuarAlunoBibli.Checked;
                        txtIdadeMinimAlunoBibli.Text = tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI != null ? tb000.TB149_PARAM_INSTI.NU_IDADE_MINIM_USUAR_ALUNO_BIBLI.Value.ToString() : "";
                        chkUsuarRespBibli.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_RESPO_BIBLI == "S";
                        chkUsuarOutroBibli.Checked = tb000.TB149_PARAM_INSTI.FL_USUAR_OUTRO_BIBLI == "S";

                        txtHorarIniT1Bibli.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO1_BIBLI : "";
                        txtHorarFimT1Bibli.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO1_BIBLI : "";
                        txtHorarIniT2Bibli.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO2_BIBLI : "";
                        txtHorarFimT2Bibli.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO2_BIBLI : "";
                        txtHorarIniT3Bibli.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO3_BIBLI : "";
                        txtHorarFimT3Bibli.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO3_BIBLI : "";
                        txtHorarIniT4Bibli.Text = tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_INI_TURNO4_BIBLI : "";
                        txtHorarFimT4Bibli.Text = tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_BIBLI != null ? tb000.TB149_PARAM_INSTI.HR_FIM_TURNO4_BIBLI : "";

//--------------------> Bibliotecário(a) 1
                        if (tb000.TB149_PARAM_INSTI.TB03_COLABOR1 != null)
                        {
                            ddlSiglaUnidBibliEscol1.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR1.CO_EMP.ToString();
                            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol1, ddlNomeBibliEscol1);
                            ddlNomeBibliEscol1.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR1.CO_COL.ToString();
                            ddlClassifBibli1.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI1 != null ? tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI1.ToString() : "1";
                        }

//--------------------> Bibliotecário(a) 2
                        if (tb000.TB149_PARAM_INSTI.TB03_COLABOR4 != null)
                        {
                            ddlSiglaUnidBibliEscol2.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR4.CO_EMP.ToString();
                            CarregaBibliotecarioEscolar(ddlSiglaUnidBibliEscol2, ddlNomeBibliEscol2);
                            ddlNomeBibliEscol2.SelectedValue = tb000.TB149_PARAM_INSTI.TB03_COLABOR4.CO_COL.ToString();
                            ddlClassifBibli2.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI2 != null ? tb000.TB149_PARAM_INSTI.CO_CLASS_BIBLI2.ToString() : "2";
                        }
                         
                    }
                    else
                    {
                        ddlFlagReserBibli.Enabled = txtQtdeItensReser.Enabled = txtQtdeMaxDiasReser.Enabled = ddlFlagEmpreBibli.Enabled = txtQtdeItensEmpre.Enabled =
                        txtQtdeMaxDiasEmpre.Enabled = ddlGeraNumEmpreAuto.Enabled = txtNumIniciEmpreAuto.Enabled = ddlFlagTaxaEmpre.Enabled =
                        txtDiasBonusTaxaEmpre.Enabled = txtValorTaxaEmpre.Enabled = ddlFlagMultaAtraso.Enabled = txtDiasBonusMultaEmpre.Enabled =
                        txtValorMultaEmpre.Enabled = ddlGeraNumItemAuto.Enabled = txtNumIniciItemAuto.Enabled = ddlNumISBNObrig.Enabled =
                        ddlFlarIncluContaRecebBibli.Enabled = chkUsuarFuncBibli.Enabled = chkUsuarProfBibli.Enabled = chkUsuarAlunoBibli.Enabled =
                        txtIdadeMinimAlunoBibli.Enabled = chkUsuarRespBibli.Enabled = chkUsuarOutroBibli.Enabled = txtHorarIniT1Bibli.Enabled =
                        txtHorarFimT1Bibli.Enabled = txtHorarIniT2Bibli.Enabled = txtHorarFimT2Bibli.Enabled = txtHorarIniT3Bibli.Enabled =
                        txtHorarFimT3Bibli.Enabled = txtHorarIniT4Bibli.Enabled = txtHorarFimT4Bibli.Enabled = ddlSiglaUnidBibliEscol1.Enabled =
                        ddlNomeBibliEscol1.Enabled = ddlClassifBibli1.Enabled = ddlSiglaUnidBibliEscol2.Enabled = ddlNomeBibliEscol2.Enabled = ddlClassifBibli2.Enabled = false;
                    }
//***************************************

//----------------> Controle Contábil *******************
                    ddlTipoCtrleContaContab.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_CTA_CONTAB;

//----------------> Faz a verificação para saber se o controle contábil é pela Instituição
                    if (ddlTipoCtrleContaContab.SelectedValue == TipoControle.I.ToString())
                    {
                        TB56_PLANOCTA planoConta = new TB56_PLANOCTA();

                        if (tb000.TB149_PARAM_INSTI.CO_CTSOL_EMP != null)
                        {
                            planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb000.TB149_PARAM_INSTI.CO_CTSOL_EMP.Value);
                            planoConta.TB54_SGRP_CTAReference.Load();
                            planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                            ddlGrupoTxServSecre.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                            CarregaSubGrupo(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre);
                            ddlSubGrupoTxServSecre.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                            CarregaSubGrupo2(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre);
                            ddlSubGrupo2TxServSecre.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                            CarregaConta(ddlGrupoTxServSecre, ddlSubGrupoTxServSecre, ddlSubGrupo2TxServSecre, ddlContaContabilTxServSecre);
                            ddlContaContabilTxServSecre.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CTSOL_EMP.Value.ToString();
                            txtCtaContabTxServSecre.Text = planoConta.DE_CONTA_PC;
                        }

                        if (tb000.TB149_PARAM_INSTI.CO_CTABIB_EMP != null)
                        {
                            planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb000.TB149_PARAM_INSTI.CO_CTABIB_EMP.Value);
                            planoConta.TB54_SGRP_CTAReference.Load();
                            planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                            ddlGrupoTxServBibli.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                            CarregaSubGrupo(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli);
                            ddlSubGrupoTxServBibli.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                            CarregaSubGrupo2(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli);
                            ddlSubGrupo2TxServBibli.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                            CarregaConta(ddlGrupoTxServBibli, ddlSubGrupoTxServBibli, ddlSubGrupo2TxServBibli, ddlContaContabilTxServBibli);
                            ddlContaContabilTxServBibli.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CTABIB_EMP.Value.ToString();
                            txtCtaContabTxServBibli.Text = planoConta.DE_CONTA_PC;
                        }

                        if (tb000.TB149_PARAM_INSTI.CO_CTAMAT_EMP != null)
                        {
                            planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb000.TB149_PARAM_INSTI.CO_CTAMAT_EMP.Value);
                            planoConta.TB54_SGRP_CTAReference.Load();
                            planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                            ddlGrupoTxMatri.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                            CarregaSubGrupo(ddlGrupoTxMatri, ddlSubGrupoTxMatri);
                            ddlSubGrupoTxMatri.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                            CarregaSubGrupo2(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri);
                            ddlSubGrupo2TxMatri.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                            CarregaConta(ddlGrupoTxMatri, ddlSubGrupoTxMatri, ddlSubGrupo2TxMatri, ddlContaContabilTxMatri);
                            ddlContaContabilTxMatri.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CTAMAT_EMP.Value.ToString();
                            txtCtaContabTxMatri.Text = planoConta.DE_CONTA_PC;
                        }

                        if (tb000.TB149_PARAM_INSTI.CO_CTA_ATIVI_EXTRA != null)
                        {
                            planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb000.TB149_PARAM_INSTI.CO_CTA_ATIVI_EXTRA.Value);
                            planoConta.TB54_SGRP_CTAReference.Load();
                            planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                            ddlGrupoAtiviExtra.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                            CarregaSubGrupo(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra);
                            ddlSubGrupoAtiviExtra.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                            CarregaSubGrupo2(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra);
                            ddlSubGrupo2AtiviExtra.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                            CarregaConta(ddlGrupoAtiviExtra, ddlSubGrupoAtiviExtra, ddlSubGrupo2AtiviExtra, ddlContaContabilAtiviExtra);
                            ddlContaContabilAtiviExtra.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CTA_ATIVI_EXTRA.Value.ToString();
                            txtCtaContabAtiviExtra.Text = planoConta.DE_CONTA_PC;
                        }

                        if (tb000.TB149_PARAM_INSTI.CO_CTA_CAIXA != null)
                        {
                            planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb000.TB149_PARAM_INSTI.CO_CTA_CAIXA.Value);
                            planoConta.TB54_SGRP_CTAReference.Load();
                            planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                            ddlGrupoContaCaixa.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                            CarregaSubGrupo(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa);
                            ddlSubGrupoContaCaixa.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                            CarregaSubGrupo2(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa);
                            ddlSubGrupo2ContaCaixa.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                            CarregaConta(ddlGrupoContaCaixa, ddlSubGrupoContaCaixa, ddlSubGrupo2ContaCaixa, ddlContaContabilContaCaixa);
                            ddlContaContabilContaCaixa.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CTA_CAIXA.Value.ToString();
                            txtCtaContabCaixa.Text = planoConta.DE_CONTA_PC;
                        }

                        if (tb000.TB149_PARAM_INSTI.CO_CTA_BANCO != null)
                        {
                            planoConta = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb000.TB149_PARAM_INSTI.CO_CTA_BANCO.Value);
                            planoConta.TB54_SGRP_CTAReference.Load();
                            planoConta.TB54_SGRP_CTA.TB53_GRP_CTAReference.Load();

                            ddlGrupoContaBanco.SelectedValue = planoConta.TB54_SGRP_CTA.CO_GRUP_CTA.ToString();

                            CarregaSubGrupo(ddlGrupoContaBanco, ddlSubGrupoContaBanco);
                            ddlSubGrupoContaBanco.SelectedValue = planoConta.TB54_SGRP_CTA.CO_SGRUP_CTA.ToString();

                            CarregaSubGrupo2(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco);
                            ddlSubGrupo2ContaCaixa.SelectedValue = planoConta.TB055_SGRP2_CTA != null ? planoConta.TB055_SGRP2_CTA.CO_SGRUP2_CTA.ToString() : "";

                            CarregaConta(ddlGrupoContaBanco, ddlSubGrupoContaBanco, ddlSubGrupo2ContaBanco, ddlContaContabilContaBanco);
                            ddlContaContabilContaBanco.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CTA_BANCO.Value.ToString();
                            txtCtaContabBanco.Text = planoConta.DE_CONTA_PC;
                        }

                        ddlCentroCustoTxServSecre.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CENT_CUSSOL != null ? tb000.TB149_PARAM_INSTI.CO_CENT_CUSSOL.Value.ToString() : "";
                        ddlCentroCustoTxServBibli.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CENT_CUSBIB != null ? tb000.TB149_PARAM_INSTI.CO_CENT_CUSBIB.Value.ToString() : "";
                        ddlCentroCustoTxMatri.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CENT_CUSMAT != null ? tb000.TB149_PARAM_INSTI.CO_CENT_CUSMAT.Value.ToString() : "";                        
                        ddlCentroCustoAtiviExtra.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_ATIVI_EXTRA != null ? tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_ATIVI_EXTRA.Value.ToString() : "";
                        ddlCentroCustoContaBanco.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_BANCO != null ? tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_BANCO.Value.ToString() : "";
                        ddlCentroCustoContaCaixa.SelectedValue = tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_CAIXA != null ? tb000.TB149_PARAM_INSTI.CO_CENT_CUSTO_CAIXA.Value.ToString() : "";                        
                    }
                    else
                    {
                        ddlGrupoTxServSecre.Enabled = ddlSubGrupoTxServSecre.Enabled = ddlSubGrupo2TxServSecre.Enabled = ddlCentroCustoTxServSecre.Enabled = ddlContaContabilTxServSecre.Enabled =
                        ddlGrupoTxServBibli.Enabled = ddlSubGrupoTxServBibli.Enabled = ddlSubGrupo2TxServBibli.Enabled = ddlCentroCustoTxServBibli.Enabled = ddlContaContabilTxServBibli.Enabled =
                        ddlGrupoTxMatri.Enabled = ddlSubGrupoTxMatri.Enabled = ddlSubGrupo2TxMatri.Enabled = ddlCentroCustoTxMatri.Enabled = ddlContaContabilTxMatri.Enabled =
                        ddlGrupoAtiviExtra.Enabled = ddlSubGrupoAtiviExtra.Enabled = ddlSubGrupo2AtiviExtra.Enabled = ddlCentroCustoAtiviExtra.Enabled = ddlContaContabilAtiviExtra.Enabled =
                        ddlGrupoContaCaixa.Enabled = ddlSubGrupoContaCaixa.Enabled = ddlSubGrupo2ContaCaixa.Enabled = ddlCentroCustoContaCaixa.Enabled = ddlContaContabilContaCaixa.Enabled =
                        ddlGrupoContaBanco.Enabled = ddlSubGrupoContaBanco.Enabled = ddlSubGrupo2ContaBanco.Enabled = ddlCentroCustoContaBanco.Enabled = ddlContaContabilContaBanco.Enabled = false;
                    }
//***************************************

//----------------> Controle de Mensagens SMS *******************
                    ddlTipoControleMensaSMS.SelectedValue = tb000.TB149_PARAM_INSTI.TP_CTRLE_MENSA_SMS;

                    chkEnvioSMS.Checked = tb000.TB149_PARAM_INSTI.FL_ENVIO_SMS == "S";

                    if (chkEnvioSMS.Checked)
                    {
                        txtQtdMaxFunci.Text = tb000.TB149_PARAM_INSTI.QT_MAX_SMS_FUNCI != null ? tb000.TB149_PARAM_INSTI.QT_MAX_SMS_FUNCI.ToString() : "";
                        txtQtdMaxProfe.Text = tb000.TB149_PARAM_INSTI.QT_MAX_SMS_PROFE != null ? tb000.TB149_PARAM_INSTI.QT_MAX_SMS_PROFE.ToString() : "";
                        txtQtdMaxRespo.Text = tb000.TB149_PARAM_INSTI.QT_MAX_SMS_RESPO != null ? tb000.TB149_PARAM_INSTI.QT_MAX_SMS_RESPO.ToString() : "";
                        txtQtdMaxAluno.Text = tb000.TB149_PARAM_INSTI.QT_MAX_SMS_ALUNO != null ? tb000.TB149_PARAM_INSTI.QT_MAX_SMS_ALUNO.ToString() : "";
                        txtQtdMaxOutros.Text = tb000.TB149_PARAM_INSTI.QT_MAX_SMS_OUTRO != null ? tb000.TB149_PARAM_INSTI.QT_MAX_SMS_OUTRO.ToString() : "";

                        txtQtdMaxFunci.Enabled = txtQtdMaxProfe.Enabled = txtQtdMaxRespo.Enabled = txtQtdMaxAluno.Enabled = txtQtdMaxOutros.Enabled = true;
                    }

//----------------> Faz a verificação para saber se o controle de mensagens SMS é pela Instituição
                    if (ddlTipoControleMensaSMS.SelectedValue == TipoControle.I.ToString())
                    {
                        chkSMSSolicSecreEscol.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_SOLIC == "S";
                        ddlFlagSMSSolicEnvAuto.Enabled = txtMsgSMSSolic.Enabled = chkSMSSolicSecreEscol.Checked;
                        ddlFlagSMSSolicEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_SOLIC != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_SOLIC : "N";
                        txtMsgSMSSolic.Text = tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_SOLIC != null ? tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_SOLIC : "";
                        chkSMSEntreSecreEscol.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_ENTRE == "S";
                        ddlFlagSMSEntreEnvAuto.Enabled = txtMsgSMSEntre.Enabled = chkSMSEntreSecreEscol.Checked;
                        ddlFlagSMSEntreEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_ENTRE != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_ENTRE : "N";
                        txtMsgSMSEntre.Text = tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_ENTRE != null ? tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_ENTRE : "";
                        chkSMSOutroSecreEscol.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_SECRE_OUTRO == "S";
                        ddlFlagSMSOutroEnvAuto.Enabled = txtMsgSMSOutro.Enabled = chkSMSOutroSecreEscol.Checked;
                        ddlFlagSMSOutroEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_OUTRO != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_SECRE_OUTRO : "N";
                        txtMsgSMSOutro.Text = tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_OUTRO != null ? tb000.TB149_PARAM_INSTI.DES_SMS_SECRE_OUTRO : "";
                        chkSMSReserVagas.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_RESER == "S";
                        ddlFlagSMSReserVagasEnvAuto.Enabled = txtMsgSMSReserVagas.Enabled = chkSMSReserVagas.Checked;
                        ddlFlagSMSReserVagasEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RESER != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RESER : "N";
                        txtMsgSMSReserVagas.Text = tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RESER != null ? tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RESER : "";
                        chkSMSRenovMatri.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_RENOV == "S";
                        ddlFlagSMSRenovMatriEnvAuto.Enabled = txtMsgSMSRenovMatri.Enabled = chkSMSRenovMatri.Checked;
                        ddlFlagSMSRenovMatriEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RENOV != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_RENOV : "N";
                        txtMsgSMSRenovMatri.Text = tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RENOV != null ? tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_RENOV : "";
                        chkSMSMatriNova.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_MATRI_NOVA == "S";
                        ddlFlagSMSMatriNovaEnvAuto.Enabled = txtMsgSMSMatriNova.Enabled = chkSMSMatriNova.Checked;
                        ddlFlagSMSMatriNovaEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_NOVA != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_MATRI_NOVA : "N";
                        txtMsgSMSMatriNova.Text = tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_NOVA != null ? tb000.TB149_PARAM_INSTI.DES_SMS_MATRI_NOVA : "";
                        chkSMSReserBibli.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_RESER == "S";
                        ddlFlagSMSReserBibliEnvAuto.Enabled = txtMsgSMSReserBibli.Enabled = chkSMSReserBibli.Checked;
                        ddlFlagSMSReserBibliEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_RESER != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_RESER : "N";
                        txtMsgSMSReserBibli.Text = tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_RESER != null ? tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_RESER : "";
                        chkSMSEmpreBibli.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_EMPRE == "S";
                        ddlFlagSMSEmpreBibliEnvAuto.Enabled = txtMsgSMSEmpreBibli.Enabled = chkSMSEmpreBibli.Checked;
                        ddlFlagSMSEmpreBibliEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_EMPRE : "N";
                        txtMsgSMSEmpreBibli.Text = tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_EMPRE != null ? tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_EMPRE : "";
                        chkSMSDiverBibli.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_BIBLI_DIVER == "S";
                        ddlFlagSMSDiverBibliEnvAuto.Enabled = txtMsgSMSDiverBibli.Enabled = chkSMSDiverBibli.Checked;
                        ddlFlagSMSDiverBibliEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_DIVER != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_BIBLI_DIVER : "N";
                        txtMsgSMSDiverBibli.Text = tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_DIVER != null ? tb000.TB149_PARAM_INSTI.DES_SMS_BIBLI_DIVER : "";
                        chkSMSFaltaAluno.Checked = tb000.TB149_PARAM_INSTI.FL_SMS_FALTA_ALUNO == "S";
                        ddlFlagSMSFaltaAlunoEnvAuto.Enabled = txtMsgSMSFaltaAluno.Enabled = chkSMSFaltaAluno.Checked;
                        ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue = tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_FALTA_ALUNO != null ? tb000.TB149_PARAM_INSTI.FL_ENVIO_AUTO_SMS_FALTA_ALUNO : "N";
                        txtMsgSMSFaltaAluno.Text = tb000.TB149_PARAM_INSTI.DES_SMS_FALTA_ALUNO != null ? tb000.TB149_PARAM_INSTI.DES_SMS_FALTA_ALUNO : ""; 
                    }
                    else
                    {
                        chkSMSSolicSecreEscol.Enabled = ddlFlagSMSSolicEnvAuto.Enabled = txtMsgSMSSolic.Enabled =
                        chkSMSEntreSecreEscol.Enabled = ddlFlagSMSEntreEnvAuto.Enabled = txtMsgSMSEntre.Enabled =
                        chkSMSOutroSecreEscol.Enabled = ddlFlagSMSReserVagasEnvAuto.Enabled = txtMsgSMSOutro.Enabled =
                        chkSMSReserVagas.Enabled = ddlFlagSMSOutroEnvAuto.Enabled = txtMsgSMSReserVagas.Enabled =
                        chkSMSRenovMatri.Enabled = ddlFlagSMSRenovMatriEnvAuto.Enabled = txtMsgSMSRenovMatri.Enabled =
                        chkSMSMatriNova.Enabled = ddlFlagSMSMatriNovaEnvAuto.Enabled = txtMsgSMSMatriNova.Enabled =
                        chkSMSReserBibli.Enabled = ddlFlagSMSReserBibliEnvAuto.Enabled = txtMsgSMSReserBibli.Enabled =
                        chkSMSEmpreBibli.Enabled = ddlFlagSMSReserBibliEnvAuto.Enabled = txtMsgSMSEmpreBibli.Enabled =
                        chkSMSDiverBibli.Enabled = ddlFlagSMSDiverBibliEnvAuto.Enabled = txtMsgSMSDiverBibli.Enabled =
                        chkSMSFaltaAluno.Enabled = ddlFlagSMSFaltaAlunoEnvAuto.Enabled = txtMsgSMSFaltaAluno.Enabled = false;
                    }
//***************************************

                    txtHoraIniTurno1.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_MANHA_INICI;
                    txtHoraFimTurno1.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_MANHA_FINAL;
                    txtHoraIniTurno2.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_TARDE_INICI;
                    txtHoraFimTurno2.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_TARDE_FINAL;
                    txtHoraIniTurno3.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_NOITE_INICI;
                    txtHoraFimTurno3.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_NOITE_FINAL;
                    txtHoraIniTurno4.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_ULTIM_TURNO_INICI;
                    txtHoraFimTurno4.Text = tb000.TB149_PARAM_INSTI.HR_FUNCI_ULTIM_TURNO_FINAL;

                    ddlTipoControleTpEnsinoIIE.SelectedValue = tb000.TB149_PARAM_INSTI.FLA_CTRL_TIPO_ENSIN;

                    if (ddlTipoControleTpEnsinoIIE.SelectedValue == "I")
                    {
                        ddlGerarNisIIE.SelectedValue = tb000.TB149_PARAM_INSTI.FLA_GERA_NIRE_AUTO;
                        ddlGerarMatriculaIIE.SelectedValue = tb000.TB149_PARAM_INSTI.FLA_GERA_MATR_AUTO;
                    }
                    else
                    {
                        ddlGerarNisIIE.Enabled = ddlGerarMatriculaIIE.Enabled = false;
                        ddlGerarNisIIE.SelectedValue = ddlGerarMatriculaIIE.SelectedValue = "N";
                    }                                        
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB000_INSTITUICAO</returns>
        private TB000_INSTITUICAO RetornaEntidade()
        {
            return TB000_INSTITUICAO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Núcleo da Unidade
        /// </summary>
        private void CarregaNucleoInstituicao()
        {
            ddlNucleoIUE.DataSource = (from tbNucleoInst in TB_NUCLEO_INST.RetornaTodosRegistros()
                                       select new { tbNucleoInst.CO_NUCLEO, tbNucleoInst.NO_SIGLA_NUCLEO }).OrderBy(n => n.NO_SIGLA_NUCLEO);

            ddlNucleoIUE.DataTextField = "NO_SIGLA_NUCLEO";
            ddlNucleoIUE.DataValueField = "CO_NUCLEO";
            ddlNucleoIUE.DataBind();

            ddlNucleoIUE.Items.Insert(0, "");
        }
        
        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUFs()
        {
            ddlUFIIE.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUFIIE.DataTextField = "CODUF";
            ddlUFIIE.DataValueField = "CODUF";
            ddlUFIIE.DataBind();

            ddlUFIIE.Items.Insert(0, "");
            ddlUFIIE.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidadeIIE.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUFIIE.SelectedValue);

            ddlCidadeIIE.DataTextField = "NO_CIDADE";
            ddlCidadeIIE.DataValueField = "CO_CIDADE";
            ddlCidadeIIE.DataBind();

            ddlCidadeIIE.Enabled = ddlCidadeIIE.Items.Count > 0;
            ddlCidadeIIE.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos
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
                                       result.CO_GRUP_CTA,
                                       DE_GRUP_CTA = string.Format("{0} - {1}", result.NR_GRUP_CTA.Value.ToString("00"), result.DE_GRUP_CTA)
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
                          where tb055.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta && tb055.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
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
        /// /// <param name="ddlSubGrupo2">DropDown de subgrupo de conta 2</param>
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
        /// Método que carrega os dropdowns de Centro de Custo
        /// </summary>
        private void CarregaCentroCusto()
        {
            var resultado = (from lTb099 in TB099_CENTRO_CUSTO.RetornaTodosRegistros()
                             where lTb099.TB14_DEPTO.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new { lTb099.NU_CTA_CENT_CUSTO, lTb099.DE_CENT_CUSTO, lTb099.CO_CENT_CUSTO }).ToList();

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
            ddlCentroCustoContaCaixa.Items.Insert(0, new ListItem("", ""));
            ddlCentroCustoContaBanco.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            if (ddlCidadeIIE.SelectedValue == "")
            {
                ddlBairroIIE.Enabled = false;
                ddlBairroIIE.Items.Clear();
                ddlBairroIIE.Items.Insert(0, "");
                return;
            }
            else
            {
                int coCidade = int.Parse(ddlCidadeIIE.SelectedValue);

                ddlBairroIIE.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);
                ddlBairroIIE.DataTextField = "NO_BAIRRO";
                ddlBairroIIE.DataValueField = "CO_BAIRRO";
                ddlBairroIIE.DataBind();

                ddlBairroIIE.Enabled = ddlBairroIIE.Items.Count > 0;
                ddlBairroIIE.Items.Insert(0, "");
            }
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
                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

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
                                   select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlCoCol.DataValueField = "CO_COL";
            ddlCoCol.DataTextField = "NO_COL";
            ddlCoCol.DataBind();

            ddlCoCol.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Validações

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

        protected void cvDataMatricula_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if ((txtMatriculaInicioIUE.Text == "" && txtMatriculaFimIUE.Text != "") ||
                (txtMatriculaFimIUE.Text == "" && txtMatriculaInicioIUE.Text != ""))
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

        private bool ValidaHorarios()
        {
            DateTime horaLimiteIni;
            DateTime horaLimiteFim;
            DateTime hora;
            DateTime hora2;

//--------> Validação horário Manhã
            if (txtHoraIniTurno1.Text != "" && txtHoraFimTurno1.Text != "")
            {
                horaLimiteIni = DateTime.Parse("07:00");
                horaLimiteFim = DateTime.Parse("12:00");
                hora = new DateTime();
                hora2 = new DateTime();

                if (!(DateTime.TryParse(txtHoraIniTurno1.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da manhã devem estar entre 7:00 e 12:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraFimTurno1.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da manhã devem estar entre 7:00 e 12:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraIniTurno1.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da manhã devem estar entre 7:00 e 12:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraFimTurno1.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da manhã devem estar entre 7:00 e 12:00");
                    return false;
                }

                hora = new DateTime();
                if ((DateTime.TryParse(txtHoraFimTurno1.Text, out hora)) && (DateTime.TryParse(txtHoraIniTurno1.Text, out hora2)) &&
                    hora <= hora2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de saída deve ser maior que horário de entrada");
                    return false;
                }

                hora = new DateTime();
                if ((DateTime.TryParse(txtHoraFimTurno1.Text, out hora)) && (DateTime.TryParse(txtHoraIniTurno1.Text, out hora2)) && hora <= hora2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de saída deve ser maior que horário de entrada");
                    return false;
                }
            }
            

//--------> Validação horário Tarde
            if (txtHoraIniTurno2.Text != "" && txtHoraFimTurno2.Text != "")
            {
                horaLimiteIni = DateTime.Parse("13:00");
                horaLimiteFim = DateTime.Parse("18:00");
                hora = new DateTime();
                hora2 = new DateTime();

                if (!(DateTime.TryParse(txtHoraIniTurno2.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Tarde devem estar entre 13:00 e 18:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraFimTurno2.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Tarde devem estar entre 13:00 e 18:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraIniTurno2.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Tarde devem estar entre 13:00 e 18:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraFimTurno2.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Tarde devem estar entre 13:00 e 18:00");
                    return false;
                }

                hora = new DateTime();
                if ((DateTime.TryParse(txtHoraFimTurno2.Text, out hora)) && (DateTime.TryParse(txtHoraIniTurno2.Text, out hora2)) &&
                    hora <= hora2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de saída deve ser maior que horário de entrada");
                    return false;
                }

                hora = new DateTime();
                if ((DateTime.TryParse(txtHoraFimTurno2.Text, out hora)) && (DateTime.TryParse(txtHoraIniTurno2.Text, out hora2)) && hora <= hora2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de saída deve ser maior que horário de entrada");
                    return false;
                }
            }            

//--------> Validação horário Noite
            if (txtHoraIniTurno3.Text != "" && txtHoraFimTurno3.Text != "")
            {
                horaLimiteIni = DateTime.Parse("18:00");
                horaLimiteFim = DateTime.Parse("23:00");
                hora = new DateTime();
                hora2 = new DateTime();

                if (!(DateTime.TryParse(txtHoraIniTurno3.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Noite devem estar entre 18:00 e 23:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraFimTurno3.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Noite devem estar entre 18:00 e 23:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraIniTurno3.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Noite devem estar entre 18:00 e 23:00");
                    return false;
                }

                hora = new DateTime();
                if (!(DateTime.TryParse(txtHoraFimTurno3.Text, out hora)) ||
                    !(hora.TimeOfDay >= horaLimiteIni.TimeOfDay && hora.TimeOfDay <= horaLimiteFim.TimeOfDay))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horários da Noite devem estar entre 18:00 e 23:00");
                    return false;
                }

                hora = new DateTime();
                if ((DateTime.TryParse(txtHoraFimTurno3.Text, out hora)) && (DateTime.TryParse(txtHoraIniTurno3.Text, out hora2)) &&
                    hora <= hora2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de saída deve ser maior que horário de entrada");
                    return false;
                }

                hora = new DateTime();
                if ((DateTime.TryParse(txtHoraFimTurno3.Text, out hora)) && (DateTime.TryParse(txtHoraIniTurno3.Text, out hora2)) && hora <= hora2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de saída deve ser maior que horário de entrada");
                    return false;
                }
            }            

            return true;
        }
        #endregion

        protected void ddlUFIIE_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidadeIIE_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        protected void ddlTipoCtrleMetod_SelectedIndexChanged(object sender, EventArgs e)
        {
//--------> Faz a verificação para saber o tipo de controle de metodologia é por Unidade
            if (ddlTipoCtrleMetod.SelectedValue == TipoControle.U.ToString())
	        {
		        ddlMetodEnsino.SelectedValue = "S";
                ddlFormaAvali.SelectedValue = "N";
                txtDescrConce1.Text = txtSiglaConce1.Text = txtNotaIni1.Text = txtNotaFim1.Text =
                txtDescrConce2.Text = txtSiglaConce2.Text = txtNotaIni2.Text = txtNotaFim2.Text =
                txtDescrConce3.Text = txtSiglaConce3.Text = txtNotaIni3.Text = txtNotaFim3.Text =
                txtDescrConce4.Text = txtSiglaConce4.Text = txtNotaIni4.Text = txtNotaFim4.Text =
                txtDescrConce5.Text = txtSiglaConce5.Text = txtNotaIni5.Text = txtNotaFim5.Text = "";
	        }

            ddlMetodEnsino.Enabled = ddlFormaAvali.Enabled = ddlTipoCtrleMetod.SelectedValue == "I";
        }

        protected void ddlFormaAvali_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormaAvali.SelectedValue == "C")
            {
//------------> recebe o ID da instituição pela QueryString
                int idTb000 = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                if (idTb000 > 0)
                {
//----------------> Carrega os Conceitos de acordo com a Instituição informada
                    var tb200 = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                 where iTb200.ORG_CODIGO_ORGAO == idTb000 && iTb200.TB25_EMPRESA == null
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
            else
            {
                txtDescrConce1.Text = txtSiglaConce1.Text = txtNotaIni1.Text = txtNotaFim1.Text =
                txtDescrConce2.Text = txtSiglaConce2.Text = txtNotaIni2.Text = txtNotaFim2.Text =
                txtDescrConce3.Text = txtSiglaConce3.Text = txtNotaIni3.Text = txtNotaFim3.Text =
                txtDescrConce4.Text = txtSiglaConce4.Text = txtNotaIni4.Text = txtNotaFim4.Text =
                txtDescrConce5.Text = txtSiglaConce5.Text = txtNotaIni5.Text = txtNotaFim5.Text = "";
            }
        }

        protected void ddlTipoCtrleAval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCtrleAval.SelectedValue == TipoControle.U.ToString())
            {
                ddlPerioAval.SelectedValue = "B";
                chkConselho.Checked = chkRecuperEscol.Checked = chkDepenEscol.Checked = false;
                txtMediaAprovGeral.Text = txtMediaAprovDireta.Text = txtMediaProvaFinal.Text = txtMediaRecuperEscol.Text = txtQtdeMaterRecuper.Text =
                txtMediaDepenEscol.Text = txtQtdMaterDepenEscol.Text = txtLimitMediaConseEscol.Text = txtQtdMaxMaterConse.Text = "";
            }
            
            txtMediaRecuperEscol.Enabled = txtQtdeMaterRecuper.Enabled = chkRecuperEscol.Checked && ddlTipoCtrleAval.SelectedValue == "I";
            txtMediaDepenEscol.Enabled = txtQtdMaterDepenEscol.Enabled = chkDepenEscol.Checked && ddlTipoCtrleAval.SelectedValue == "I";
            txtQtdMaxMaterConse.Enabled = txtLimitMediaConseEscol.Enabled = chkConselho.Checked && ddlTipoCtrleAval.SelectedValue == "I";
            txtMediaAprovGeral.Enabled = txtMediaAprovDireta.Enabled = txtMediaProvaFinal.Enabled = chkRecuperEscol.Enabled = chkDepenEscol.Enabled =
            chkConselho.Enabled = ddlPerioAval.Enabled = ddlTipoCtrleAval.SelectedValue == "I";
        }

        protected void ddlTipoCtrleDatas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCtrleDatas.SelectedValue == TipoControle.U.ToString())
            {
                txtReservaMatriculaDtInicioIUE.Text = txtReservaMatriculaDtFimIUE.Text = txtRematriculaInicioIUE.Text = txtRematriculaFimIUE.Text = txtMatriculaInicioIUE.Text =
                txtMatriculaFimIUE.Text = txtDataRemanAlunoIni.Text = txtDataRemanAlunoFim.Text = txtDtInicioTransInter.Text = txtDtFimTransInter.Text =
                txtTrancamentoMatriculaInicioIUE.Text = txtTrancamentoMatriculaFimIUE.Text = txtAlteracaoMatriculaInicioIUE.Text = txtAlteracaoMatriculaFimIUE.Text = "";
            }

            txtReservaMatriculaDtInicioIUE.Enabled = txtReservaMatriculaDtFimIUE.Enabled = txtRematriculaInicioIUE.Enabled = txtRematriculaFimIUE.Enabled = txtMatriculaInicioIUE.Enabled =
            txtMatriculaFimIUE.Enabled = txtDataRemanAlunoIni.Enabled = txtDataRemanAlunoFim.Enabled = txtDtInicioTransInter.Enabled = txtDtFimTransInter.Enabled =
            txtTrancamentoMatriculaInicioIUE.Enabled = txtTrancamentoMatriculaFimIUE.Enabled = txtAlteracaoMatriculaInicioIUE.Enabled = txtAlteracaoMatriculaFimIUE.Enabled = ddlTipoCtrleDatas.SelectedValue == "I";
        }

        protected void ddlTipoCtrlSecreEscol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCtrlSecreEscol.SelectedValue == TipoControle.U.ToString())
            {
                chkUsuarFunc.Checked = chkUsuarProf.Checked = chkUsuarAluno.Checked = chkUsuarPaisRespo.Checked = chkUsuarOutro.Checked = false;
                ddlGeraNumSolicAuto.SelectedValue = ddlContrPrazEntre.SelectedValue = ddlAlterPrazoEntreSolic.SelectedValue = 
                ddlFlagApresValorServi.SelectedValue = ddlAbonaValorServiSolic.SelectedValue = ddlApresValorServiSolic.SelectedValue =
                ddlFlagIncluContaReceb.SelectedValue = ddlGeraBoletoServiSecre.SelectedValue = "N";
                ddlFlagTipoJurosSecre.SelectedValue = ddlFlagTipoMultaSecre.SelectedValue = "V";
                txtIdadeMinimAlunoSecreEscol.Text = txtHorarIniT1SecreEscol.Text = txtHorarFimT1SecreEscol.Text =
                txtHorarIniT2SecreEscol.Text = txtHorarFimT2SecreEscol.Text = txtHorarIniT3SecreEscol.Text = txtHorarFimT3SecreEscol.Text =
                txtHorarIniT4SecreEscol.Text = txtHorarFimT4SecreEscol.Text =
                txtNumIniciSolicAuto.Text = txtQtdeDiasEntreSolic.Text =
                txtVlJurosSecre.Text = txtVlMultaSecre.Text = ddlTipoBoletoServiSecre.SelectedValue =                
                ddlSiglaUnidSecreEscol1.SelectedValue = ddlNomeSecreEscol1.SelectedValue = ddlSiglaUnidSecreEscol2.SelectedValue = 
                ddlNomeSecreEscol2.SelectedValue = ddlSiglaUnidSecreEscol3.SelectedValue = ddlNomeSecreEscol3.SelectedValue = "";
                ddlClassifSecre1.SelectedValue = "1";
                ddlClassifSecre2.SelectedValue = "2";
                ddlClassifSecre3.SelectedValue = "3";
            }

            txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarAluno.Checked && ddlTipoCtrlSecreEscol.SelectedValue == "I";
            txtNumIniciSolicAuto.Enabled = ddlGeraNumSolicAuto.SelectedValue == "S" && ddlTipoCtrlSecreEscol.SelectedValue == "I";
            txtQtdeDiasEntreSolic.Enabled = ddlAlterPrazoEntreSolic.Enabled = ddlContrPrazEntre.SelectedValue == "S" && ddlTipoCtrlSecreEscol.SelectedValue == "I";
            ddlApresValorServiSolic.Enabled = ddlAbonaValorServiSolic.Enabled = ddlFlagApresValorServi.SelectedValue == "S" && ddlTipoCtrlSecreEscol.SelectedValue == "I";
            ddlTipoBoletoServiSecre.Enabled = ddlGeraBoletoServiSecre.SelectedValue == "S" && ddlTipoCtrlSecreEscol.SelectedValue == "I";
            ddlFlagTipoJurosSecre.Enabled = ddlFlagTipoMultaSecre.Enabled = ddlGeraBoletoServiSecre.Enabled = txtVlJurosSecre.Enabled = txtVlMultaSecre.Enabled =
                ddlFlagIncluContaReceb.SelectedValue == "S" && ddlTipoCtrlSecreEscol.SelectedValue == "I";


            txtHorarIniT1SecreEscol.Enabled = txtHorarFimT1SecreEscol.Enabled = txtHorarIniT2SecreEscol.Enabled = txtHorarFimT2SecreEscol.Enabled = 
            txtHorarIniT3SecreEscol.Enabled = txtHorarFimT3SecreEscol.Enabled = txtHorarIniT4SecreEscol.Enabled = txtHorarFimT4SecreEscol.Enabled =             
            ddlGeraNumSolicAuto.Enabled = ddlContrPrazEntre.Enabled =
            chkUsuarFunc.Enabled = chkUsuarProf.Enabled = chkUsuarAluno.Enabled = chkUsuarPaisRespo.Enabled = chkUsuarOutro.Enabled =            
            ddlFlagApresValorServi.Enabled = ddlFlagIncluContaReceb.Enabled = ddlSiglaUnidSecreEscol1.Enabled = ddlNomeSecreEscol1.Enabled =
            ddlSiglaUnidSecreEscol2.Enabled = ddlNomeSecreEscol2.Enabled = ddlSiglaUnidSecreEscol3.Enabled = ddlNomeSecreEscol3.Enabled =
            ddlClassifSecre1.Enabled = ddlClassifSecre2.Enabled = ddlClassifSecre3.Enabled = ddlTipoCtrlSecreEscol.SelectedValue == "I";
        }

        protected void ddlTipoCtrlBibliEscol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCtrlBibliEscol.SelectedValue == TipoControle.U.ToString())
            {
                chkUsuarFuncBibli.Checked = chkUsuarAlunoBibli.Checked = chkUsuarRespBibli.Checked = chkUsuarOutroBibli.Checked = chkUsuarProfBibli.Checked = false;
                ddlFlagReserBibli.SelectedValue = ddlFlagEmpreBibli.SelectedValue = ddlGeraNumEmpreAuto.SelectedValue =
                ddlFlagTaxaEmpre.SelectedValue = ddlFlagMultaAtraso.SelectedValue = ddlGeraNumItemAuto.SelectedValue =
                ddlNumISBNObrig.SelectedValue = ddlFlarIncluContaRecebBibli.SelectedValue = "N";
                txtQtdeItensReser.Text = txtQtdeMaxDiasReser.Text = txtQtdeItensEmpre.Text =
                txtQtdeMaxDiasEmpre.Text = txtNumIniciEmpreAuto.Text = txtDiasBonusTaxaEmpre.Text = txtValorTaxaEmpre.Text =
                txtDiasBonusMultaEmpre.Text = txtValorMultaEmpre.Text = txtNumIniciItemAuto.Text = txtIdadeMinimAlunoBibli.Text =
                txtHorarIniT1Bibli.Text = txtHorarFimT1Bibli.Text = txtHorarIniT2Bibli.Text = txtHorarFimT2Bibli.Text =
                txtHorarIniT3Bibli.Text = txtHorarFimT3Bibli.Text = txtHorarIniT4Bibli.Text = txtHorarFimT4Bibli.Text =
                ddlSiglaUnidBibliEscol1.SelectedValue = ddlNomeBibliEscol1.SelectedValue = ddlSiglaUnidBibliEscol2.SelectedValue = 
                ddlNomeBibliEscol2.SelectedValue = "";
                ddlClassifBibli1.SelectedValue = "1";
                ddlClassifBibli2.SelectedValue = "2";
            }

            txtIdadeMinimAlunoBibli.Enabled = chkUsuarAlunoBibli.Checked && ddlTipoCtrlBibliEscol.SelectedValue == "I";
            txtQtdeItensReser.Enabled = txtQtdeMaxDiasReser.Enabled = ddlFlagReserBibli.SelectedValue == "S" && ddlTipoCtrlBibliEscol.SelectedValue == "I";
            txtQtdeItensEmpre.Enabled = txtQtdeMaxDiasEmpre.Enabled = ddlFlagEmpreBibli.SelectedValue == "S" && ddlTipoCtrlBibliEscol.SelectedValue == "I";
            txtNumIniciEmpreAuto.Enabled = ddlGeraNumEmpreAuto.SelectedValue == "S" && ddlTipoCtrlBibliEscol.SelectedValue == "I";
            txtDiasBonusTaxaEmpre.Enabled = txtValorTaxaEmpre.Enabled = ddlFlagTaxaEmpre.SelectedValue == "S" && ddlTipoCtrlBibliEscol.SelectedValue == "I";
            txtDiasBonusMultaEmpre.Enabled = txtValorMultaEmpre.Enabled = ddlFlagMultaAtraso.SelectedValue == "S" && ddlTipoCtrlBibliEscol.SelectedValue == "I";
            txtNumIniciItemAuto.Enabled = ddlGeraNumItemAuto.SelectedValue == "S" && ddlTipoCtrlBibliEscol.SelectedValue == "I";

            txtHorarIniT1Bibli.Enabled = txtHorarFimT1Bibli.Enabled = txtHorarIniT2Bibli.Enabled = txtHorarFimT2Bibli.Enabled =
            txtHorarIniT3Bibli.Enabled = txtHorarFimT3Bibli.Enabled = txtHorarIniT4Bibli.Enabled = txtHorarFimT4Bibli.Enabled =
            ddlFlagReserBibli.Enabled = ddlFlagEmpreBibli.Enabled = ddlGeraNumEmpreAuto.Enabled =
            ddlFlagTaxaEmpre.Enabled = ddlFlagMultaAtraso.Enabled = ddlGeraNumItemAuto.Enabled =
            ddlNumISBNObrig.Enabled = ddlFlarIncluContaRecebBibli.Enabled = chkUsuarFuncBibli.Enabled = chkUsuarAlunoBibli.Enabled = 
            chkUsuarRespBibli.Enabled = chkUsuarOutroBibli.Enabled = chkUsuarProfBibli.Enabled = ddlSiglaUnidBibliEscol1.Enabled = 
            ddlNomeBibliEscol1.Enabled = ddlSiglaUnidBibliEscol2.Enabled = ddlClassifBibli1.Enabled = 
            ddlClassifBibli2.Enabled = ddlNomeBibliEscol2.Enabled = ddlTipoCtrlBibliEscol.SelectedValue == "I";
        }

        protected void ddlTipoCtrleContaContab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCtrleContaContab.SelectedValue == TipoControle.U.ToString())
            {
                ddlGrupoTxServSecre.SelectedValue = ddlSubGrupoTxServSecre.SelectedValue = ddlSubGrupo2TxServSecre.SelectedValue = ddlContaContabilTxServSecre.SelectedValue =
                ddlCentroCustoTxServSecre.SelectedValue = ddlGrupoTxServBibli.SelectedValue = ddlSubGrupoTxServBibli.SelectedValue = ddlSubGrupo2TxServBibli.SelectedValue =
                ddlContaContabilTxServBibli.SelectedValue = ddlCentroCustoTxServBibli.SelectedValue =
                ddlGrupoTxMatri.SelectedValue = ddlSubGrupoTxMatri.SelectedValue = ddlSubGrupo2TxMatri.SelectedValue = ddlContaContabilTxMatri.SelectedValue =
                ddlCentroCustoTxMatri.SelectedValue = ddlGrupoContaBanco.SelectedValue = ddlSubGrupoContaBanco.SelectedValue = ddlSubGrupo2ContaBanco.SelectedValue =
                ddlContaContabilContaBanco.SelectedValue = ddlCentroCustoContaBanco.SelectedValue = ddlGrupoAtiviExtra.SelectedValue =
                ddlSubGrupoAtiviExtra.SelectedValue = ddlSubGrupo2AtiviExtra.SelectedValue = ddlContaContabilAtiviExtra.SelectedValue = ddlCentroCustoAtiviExtra.SelectedValue =
                ddlGrupoContaCaixa.SelectedValue = ddlSubGrupoContaCaixa.SelectedValue = ddlSubGrupo2ContaCaixa.SelectedValue = ddlContaContabilContaCaixa.SelectedValue =
                ddlCentroCustoContaCaixa.SelectedValue = "";
            }

            ddlGrupoTxServSecre.Enabled = ddlSubGrupoTxServSecre.Enabled = ddlSubGrupo2TxServSecre.Enabled = ddlContaContabilTxServSecre.Enabled =
            ddlCentroCustoTxServSecre.Enabled = ddlGrupoTxServBibli.Enabled = ddlSubGrupoTxServBibli.Enabled = ddlSubGrupo2TxServBibli.Enabled =
            ddlContaContabilTxServBibli.Enabled = ddlCentroCustoTxServBibli.Enabled =
            ddlGrupoTxMatri.Enabled = ddlSubGrupoTxMatri.Enabled = ddlSubGrupo2TxMatri.Enabled = ddlContaContabilTxMatri.Enabled =
            ddlCentroCustoTxMatri.Enabled = ddlGrupoContaCaixa.Enabled = ddlSubGrupoContaCaixa.Enabled = ddlSubGrupo2ContaCaixa.Enabled =
            ddlContaContabilContaCaixa.Enabled = ddlCentroCustoContaCaixa.Enabled = ddlGrupoAtiviExtra.Enabled =
            ddlSubGrupoAtiviExtra.Enabled = ddlSubGrupo2AtiviExtra.Enabled = ddlContaContabilAtiviExtra.Enabled = ddlCentroCustoAtiviExtra.Enabled =
            ddlGrupoContaBanco.Enabled = ddlSubGrupoContaBanco.Enabled = ddlSubGrupo2ContaBanco.Enabled = ddlContaContabilContaBanco.Enabled =
            ddlCentroCustoContaBanco.Enabled = ddlTipoCtrleContaContab.SelectedValue == "I";
        }

        protected void ddlTipoControleTpEnsinoIIE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoControleTpEnsinoIIE.SelectedValue == TipoControle.U.ToString())
            {
                ddlGerarNisIIE.SelectedValue = ddlGerarMatriculaIIE.SelectedValue = "N";
            }

            ddlGerarNisIIE.Enabled = ddlGerarMatriculaIIE.Enabled = ddlTipoControleTpEnsinoIIE.SelectedValue == "I";
        }

        protected void ddlTipoControleMensaSMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoControleMensaSMS.SelectedValue == TipoControle.U.ToString())
            {
                chkSMSSolicSecreEscol.Checked = chkSMSEntreSecreEscol.Checked = chkSMSOutroSecreEscol.Checked = chkSMSOutroSecreEscol.Checked = chkSMSReserBibli.Checked = 
                chkSMSEmpreBibli.Checked = chkSMSDiverBibli.Checked = chkSMSReserVagas.Checked = chkSMSRenovMatri.Checked = chkSMSMatriNova.Checked =
                chkSMSFaltaAluno.Checked = false;
                ddlFlagSMSSolicEnvAuto.SelectedValue = ddlFlagSMSEntreEnvAuto.SelectedValue = ddlFlagSMSOutroEnvAuto.SelectedValue =
                ddlFlagSMSReserBibliEnvAuto.SelectedValue = ddlFlagSMSEmpreBibliEnvAuto.SelectedValue = ddlFlagSMSDiverBibliEnvAuto.SelectedValue =
                ddlFlagSMSReserVagasEnvAuto.SelectedValue = ddlFlagSMSRenovMatriEnvAuto.SelectedValue = ddlFlagSMSMatriNovaEnvAuto.SelectedValue = 
                ddlFlagSMSFaltaAlunoEnvAuto.SelectedValue = "N";
                txtMsgSMSSolic.Text = txtMsgSMSEntre.Text = txtMsgSMSOutro.Text =
                txtMsgSMSReserBibli.Text = txtMsgSMSEmpreBibli.Text = txtMsgSMSDiverBibli.Text = txtMsgSMSReserVagas.Text =
                txtMsgSMSRenovMatri.Text = txtMsgSMSMatriNova.Text = txtMsgSMSFaltaAluno.Text = "";
            }

            ddlFlagSMSSolicEnvAuto.Enabled = txtMsgSMSSolic.Enabled = chkSMSSolicSecreEscol.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";
            ddlFlagSMSEntreEnvAuto.Enabled = txtMsgSMSEntre.Enabled = chkSMSEntreSecreEscol.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";
            ddlFlagSMSOutroEnvAuto.Enabled = txtMsgSMSOutro.Enabled = chkSMSOutroSecreEscol.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";

            ddlFlagSMSReserBibliEnvAuto.Enabled = txtMsgSMSReserBibli.Enabled = chkSMSReserBibli.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";
            ddlFlagSMSEmpreBibliEnvAuto.Enabled = txtMsgSMSEmpreBibli.Enabled = chkSMSEmpreBibli.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";
            ddlFlagSMSDiverBibliEnvAuto.Enabled = txtMsgSMSDiverBibli.Enabled = chkSMSDiverBibli.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";

            ddlFlagSMSReserVagasEnvAuto.Enabled = txtMsgSMSReserVagas.Enabled = chkSMSReserVagas.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";
            ddlFlagSMSRenovMatriEnvAuto.Enabled = txtMsgSMSRenovMatri.Enabled = chkSMSRenovMatri.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";
            ddlFlagSMSMatriNovaEnvAuto.Enabled = txtMsgSMSMatriNova.Enabled = chkSMSMatriNova.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";

            ddlFlagSMSFaltaAlunoEnvAuto.Enabled = txtMsgSMSFaltaAluno.Enabled = chkSMSFaltaAluno.Checked && ddlTipoControleMensaSMS.SelectedValue == "I";

            chkSMSSolicSecreEscol.Enabled = chkSMSEntreSecreEscol.Enabled = chkSMSOutroSecreEscol.Enabled = chkSMSOutroSecreEscol.Enabled = chkSMSReserBibli.Enabled =
            chkSMSEmpreBibli.Enabled = chkSMSDiverBibli.Enabled = chkSMSReserVagas.Enabled = chkSMSRenovMatri.Enabled = chkSMSMatriNova.Enabled =
            chkSMSFaltaAluno.Enabled = ddlTipoControleMensaSMS.SelectedValue == "I";
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

        protected void chkEnvioSMS_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEnvioSMS.Checked)
            {
                txtQtdMaxFunci.Text = txtQtdMaxProfe.Text = txtQtdMaxRespo.Text = txtQtdMaxAluno.Text = txtQtdMaxOutros.Text = "";
            }

            txtQtdMaxFunci.Enabled = txtQtdMaxProfe.Enabled = txtQtdMaxRespo.Enabled = txtQtdMaxAluno.Enabled = txtQtdMaxOutros.Enabled = chkEnvioSMS.Checked;
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

        protected void ddlFlagMultaAtraso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlagMultaAtraso.SelectedValue == "N")
                txtValorMultaEmpre.Text = txtDiasBonusMultaEmpre.Text = "";

            txtValorMultaEmpre.Enabled = txtDiasBonusMultaEmpre.Enabled = ddlFlagMultaAtraso.SelectedValue == "S";
        }

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

        protected void ddlGeraNumEmpreAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraNumEmpreAuto.SelectedValue != "S")
                txtNumIniciEmpreAuto.Text = "";

            txtNumIniciEmpreAuto.Enabled = ddlGeraNumEmpreAuto.SelectedValue == "S";
        }

        protected void ddlGeraNumItemAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraNumItemAuto.SelectedValue != "S")
                txtNumIniciItemAuto.Text = "";

            txtNumIniciItemAuto.Enabled = ddlGeraNumItemAuto.SelectedValue == "S";
        }

        protected void chkUsuarAlunoBibli_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsuarAlunoBibli.Checked)
                txtIdadeMinimAlunoBibli.Text = "";

            txtIdadeMinimAlunoBibli.Enabled = chkUsuarAlunoBibli.Checked;
        }

        protected void chkUsuarAluno_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsuarAluno.Checked)
                txtIdadeMinimAlunoSecreEscol.Text = "";

            txtIdadeMinimAlunoSecreEscol.Enabled = chkUsuarAluno.Checked;
        }

        protected void ddlGeraNumSolicAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGeraNumSolicAuto.SelectedValue != "S")
                txtNumIniciSolicAuto.Text = "";

            txtNumIniciSolicAuto.Enabled = ddlGeraNumSolicAuto.SelectedValue == "S";
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

        protected void chkRecuperEscol_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkRecuperEscol.Checked)
                txtQtdeMaterRecuper.Text = txtMediaRecuperEscol.Text = "";

            txtQtdeMaterRecuper.Enabled = txtMediaRecuperEscol.Enabled = chkRecuperEscol.Checked;
        }

        protected void chkDepenEscol_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDepenEscol.Checked)
                txtQtdMaterDepenEscol.Text = txtMediaDepenEscol.Text = "";

            txtQtdMaterDepenEscol.Enabled = txtMediaDepenEscol.Enabled = chkDepenEscol.Checked;
        }

        protected void chkConselho_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkConselho.Checked)
                txtLimitMediaConseEscol.Text = txtQtdMaxMaterConse.Text = "";

            txtQtdMaxMaterConse.Enabled = txtLimitMediaConseEscol.Enabled = chkConselho.Checked;
        }

        protected void btnPesqCEP_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCEPIIE.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEPIIE.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    tb235.TB240_TIPO_LOGRADOUROReference.Load();

                    txtLogradouroIIE.Text = tb235.TB240_TIPO_LOGRADOURO.DE_TIPO_LOGRA + " " + tb235.NO_ENDER_CEP;                    
                    tb235.TB905_BAIRROReference.Load();
                    ddlUFIIE.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades();
                    ddlCidadeIIE.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros();
                    ddlBairroIIE.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
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

            txtCtaContabTxServBibli.Text = resultado != null ? resultado.DE_CONTA_PC : "";
        }

        protected void ddlContaContabilTxMatri_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContaContab = ddlContaContabilTxMatri.SelectedValue != "" ? int.Parse(ddlContaContabilTxMatri.SelectedValue) : 0;

            var resultado = (from tb56 in TB56_PLANOCTA.RetornaTodosRegistros()
                             where tb56.CO_SEQU_PC == idContaContab
                             select new { tb56.DE_CONTA_PC }).FirstOrDefault();

            txtCtaContabTxMatri.Text = resultado != null ? resultado.DE_CONTA_PC : "";
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
    }
}