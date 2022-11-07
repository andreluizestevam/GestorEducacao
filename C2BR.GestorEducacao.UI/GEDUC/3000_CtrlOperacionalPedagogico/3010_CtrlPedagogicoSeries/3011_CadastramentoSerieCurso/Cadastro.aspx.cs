//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: CADASTRAMENTO DE SÉRIES/CURSOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 20/03/2013| André Nobre Vinagre        | Criação do histórico de matrícula e mensalidade.
//           |                            | 
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 27/03/2013| Bruno Pinheiro de Sousa    | Adição dos campos de Valores de Contrato.
//           |                            | 
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 09/04/2013| André Nobre Vinagre        | Corrigida inconsistencia no carregamento de valores
//           |                            | à vista
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3011_CadastramentoSerieCurso
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> padroesCalc = AuxiliBaseApoio.chave(padraoCalculoMedia.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaModalidades();
                CarregaModadlidadesProxima();
                CarregaSeries();
                CarregaDepartamentos();
                CarregaCoordenacoes();
                CarregaCoordenadores();
                CarregaClassificacoes();
                CarregaReferenciaSerieRelatorio();
                CarregaContratos();
                CarregaContratosPre();
                CarregaHistoricos();
                CarregaHistoricosPreMatr();
                CarregaConceitos();
                CarregarPadraoCalculo();
                CarregaMDV();
                CarregarTipoBoleto();
                CarregarTipoBoletoPre();
                CarregaBoletos();
                CarregaBoletosPre();

                AuxiliCarregamentos.carregaModalidades(ddlModalidaAnterior, LoginAuxili.ORG_CODIGO_ORGAO, false);
                carregaSerieAnterior();

                ///Valores de matricula
                txtTxMatInt.Enabled = false;
                txtTxMatMan.Enabled = false;
                txtTxMatNoi.Enabled = false;
                txtTxMatTar.Enabled = false;
                txtTxMatEsp.Enabled = false;

                txtVlPraInt.Enabled = false;
                txtVlPraMan.Enabled = false;
                txtVlPraTar.Enabled = false;
                txtVlPraNoi.Enabled = false;
                txtVlPraEsp.Enabled = false;

                txtVlVistInt.Enabled = false;
                txtVlVistMan.Enabled = false;
                txtVlVistNoi.Enabled = false;
                txtVlVistTar.Enabled = false;
                txtVlVistEsp.Enabled = false;

                ///Valores de pré-matricula
                txtVlVistManP.Enabled = false;
                txtVlVistTarP.Enabled = false;
                txtVlVistNoiP.Enabled = false;
                txtVlVistIntP.Enabled = false;
                txtVlVistEspP.Enabled = false;

                txtVlPrazManP.Enabled = false;
                txtVlPrazTarP.Enabled = false;
                txtVlPrazNoiP.Enabled = false;
                txtVlPrazIntP.Enabled = false;
                txtVlPrazEspP.Enabled = false;

                txtTxMatIntP.Enabled = false;
                txtTxMatManP.Enabled = false;
                txtTxMatNoiP.Enabled = false;
                txtTxMatTarP.Enabled = false;
                txtTxMatEspP.Enabled = false;

                ///Valores de quantidade, nota e conceito de recuperacao
                txtQtdMatRecu.Enabled = false;
                txtVLMedRecu.Enabled = false;
                ddlConcMedRecu.Enabled = false;

                ///Valores de quantidade, nota e conceito de dependencia
                txtQtdMatDepe.Enabled = false;
                txtVLMedDepe.Enabled = false;
                ddlConcMedDepe.Enabled = false;

                ///Valores de quantidade, nora e conceito de conselho de classe
                txtQtdMatCons.Enabled = false;
                txtVLMedCons.Enabled = false;
                ddlConcMedCons.Enabled = false;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDataCriacaoSerCur.Text = txtDataSituacaoSerCur.Text = dataAtual;
                }
            }
            else
            {
                txtqtMatRecBim.Enabled = txtVlMedRecuBim.Enabled = chkRecBim.Checked;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            decimal dcmValor = 0;

            if (ddlTpLanctNota.SelectedValue == "N")
            {
                if (!Decimal.TryParse(txtNotaFinalAprovSerCur.Text, out dcmValor))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Nota final informada não é válida.");
                    return;
                }
            }

            //regras dos valores de contrato: caso  o checkbox seja marcado, o usuário deverá informar
            //para a linha correspondente(à vista, à prazo ou Taxa Matricula), os valores para todos os
            //turnos ou o valor integral. Podendo
            //preencher também os dois juntos (turnos e integral),e caso preencha um valor para algum turno
            //deverá preencher para todos os turnos daquela linha.

            //faz as validações para os campos de valores à vista

            if (chkVista.Checked == true)
            {
                //verifica se todos os valores à vista estão vazios
                if ((txtVlVistMan.Text == "" && txtVlVistTar.Text == "" && txtVlVistNoi.Text == "") && txtVlVistInt.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Vista dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                //verifica se quando valor integral está vazio,  todos os campos de turnos estão preenchidos                if (txtVlVistInt.Text == "")
                {
                    if (txtVlVistMan.Text == "" || txtVlVistTar.Text == "" || txtVlVistNoi.Text == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Vista dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                        return;
                    }
                }

                //verifica se todos os valores de turnos estão preenchidos caso algum esteja
                if (txtVlVistMan.Text != "" && (txtVlVistTar.Text == "" || txtVlVistNoi.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Vista dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                if (txtVlVistTar.Text != "" && (txtVlVistMan.Text == "" || txtVlVistNoi.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Vista dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                if (txtVlVistNoi.Text != "" && (txtVlVistMan.Text == "" || txtVlVistTar.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Vista dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }


            }

            //faz as validações para os campos de valores à prazo
            if (chkPrazo.Checked == true)
            {
                //verifica se todos os valores à prazo estão vazios
                if ((txtVlPraMan.Text == "" && txtVlPraTar.Text == "" && txtVlPraNoi.Text == "") && txtVlPraInt.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Prazo dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }
                //verifica se quando valor integral está vazio, todos os campos de turnos estão preenchidos
                if (txtVlPraInt.Text == "")
                {
                    if (txtVlPraMan.Text == "" || txtVlPraTar.Text == "" || txtVlPraNoi.Text == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Prazo dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                        return;
                    }
                }

                //verifica se todos os valores de turnos estão preenchidos caso algum esteja
                if (txtVlPraMan.Text != "" && (txtVlPraTar.Text == "" || txtVlPraNoi.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Prazo dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                if (txtVlPraTar.Text != "" && (txtVlPraMan.Text == "" || txtVlPraNoi.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Prazo dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                if (txtVlPraNoi.Text != "" && (txtVlPraMan.Text == "" || txtVlPraTar.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores à Prazo dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

            }

            //faz as validações para os campos de valores de Taxa de matrícula
            if (chkTaxaMatr.Checked == true)
            {
                //verifica se todos os valores de taxa de matricula estão vazios
                if ((txtTxMatMan.Text == "" && txtTxMatTar.Text == "" && txtTxMatNoi.Text == "") && txtTxMatInt.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores de Taxa de Matrícula dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                //verifica se quando valor integral está vazio, todos os campos de turnos estão preenchidos
                if (txtTxMatInt.Text == "")
                {
                    if (txtTxMatMan.Text == "" || txtTxMatTar.Text == "" || txtTxMatNoi.Text == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valores de Taxa de Matrícula dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                        return;
                    }
                }

                //verifica se todos os valores de turnos estão preenchidos caso algum esteja
                if (txtTxMatMan.Text != "" && (txtTxMatTar.Text == "" || txtTxMatNoi.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores de Taxa de Matrícula dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                if (txtTxMatTar.Text != "" && (txtTxMatMan.Text == "" || txtTxMatNoi.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores de Taxa de Matrícula dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

                if (txtTxMatNoi.Text != "" && (txtTxMatMan.Text == "" || txtTxMatTar.Text == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valores de Taxa de Matrícula dos Turnos(Manhã, Tarde, Noite) ou Valor Integral deve ser informado!");
                    return;
                }

            }

            //verifica se os valores informados em "Valores de Contrato" é maior que o permitido
            if (txtVlVistMan.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à vista - Manhã\" é maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlVistTar.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à vista - Tarde\" é maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlVistNoi.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à vista - Noite\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlVistInt.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à vista - Integral\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlPraMan.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à prazo - Manhã\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlPraTar.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à prazo - Tarde\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlPraNoi.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à prazo - Noite\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtVlPraInt.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"pgto à prazo - Integral\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtTxMatMan.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"taxa matricula - Manhã\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtTxMatTar.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"taxa matricula - Tarde\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtTxMatNoi.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"taxa matricula - Noite\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }

            if (txtTxMatInt.Text.Length > 9)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor informado no campo \"taxa matricula - Integral\" é Maior que o permitido, Maior valor Aceito R$99999,99");
                return;
            }


            if ((int.Parse(txtQtdMesesSerCur.Text) == 0) || (int.Parse(txtQtdMesesSerCur.Text) > 99))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade de meses do Curso/Série informado é inválido.");
                return;
            }

            int modalidade = ddlModalidadeSerCur.SelectedValue != "" ? int.Parse(ddlModalidadeSerCur.SelectedValue) : 0;

            DateTime dataRetorno = DateTime.Now;
            decimal decimalRetorno = 0;
            int intRetorno = 0;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocoSer = (from iTb01 in TB01_CURSO.RetornaTodosRegistros()
                              where iTb01.CO_MODU_CUR == modalidade && iTb01.NO_CUR == txtDescricaoSerCur.Text
                              && iTb01.CO_EMP == LoginAuxili.CO_EMP
                              select iTb01).FirstOrDefault();

                if (ocoSer != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Série informada já cadastrada.");
                    return;
                }
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int coCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                var ocoSer = (from iTb01 in TB01_CURSO.RetornaTodosRegistros()
                              where iTb01.CO_MODU_CUR == modalidade && iTb01.NO_CUR == txtDescricaoSerCur.Text
                              && iTb01.CO_CUR != coCur && iTb01.CO_EMP == LoginAuxili.CO_EMP
                              select iTb01).FirstOrDefault();

                if (ocoSer != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Série informada já cadastrada.");
                    return;
                }
            }

            TB01_CURSO tb01 = RetornaEntidadeTB01();
            TB81_DATA_CTRL tb81 = RetornaEntidadeTB81();
            TB19_INFOR_CURSO tb19 = RetornaEntidadeTB19();

            //--------> Se for um novo registro, deve inserir as chaves primárias
            if (tb01.CO_CUR == 0 && tb01.CO_EMP == 0)
            {
                tb01.CO_EMP = LoginAuxili.CO_EMP;
                tb01.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb01.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
            }

            //--------> Se inclusão, define a FL_INCLU_CUR(flag de inclusão) = true e FL_ALTER_CUR(flag de alteração) = false
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb01.FL_INCLU_CUR = true;
                tb01.FL_ALTER_CUR = false;
            }

            //--------> Se alteração, define a FL_ALTER_CUR(flag de alteração) = true
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                tb01.FL_ALTER_CUR = true;
            }

            tb01.NO_CUR = txtDescricaoSerCur.Text;
            tb01.CO_SIGL_CUR = txtSiglaSerCur.Text;
            tb01.CO_SERIE_REFER = ddlReferSerieRel.SelectedValue != "" ? ddlReferSerieRel.SelectedValue : null;
            //tb01.VL_TOTA_CUR = txtValorSerCur.Text != "" ? (decimal?)Decimal.Parse(txtValorSerCur.Text) : null;
            tb01.NU_QUANT_MESES = int.Parse(txtQtdMesesSerCur.Text);
            tb01.SEQ_IMPRESSAO = int.TryParse(txtNumeroImpressaoSerCur.Text, out intRetorno) ? (int?)intRetorno : null;
            tb01.CO_NIVEL_CUR = ddlClassificacaoSerCur.SelectedValue;
            tb01.CO_SIGL_REFER = txtReferenciaEFSerCur.Text;
            tb01.NO_ENSINO_FUND_ANTERIOR = txtReferenciaEFSerCur.Text;
            tb01.NO_REFER = txtDescricaoEPSerCur.Text;
            tb01.CO_SIGL_REFER = txtSiglaEPSerCur.Text != "" ? txtSiglaEPSerCur.Text : null;
            tb01.CO_DPTO_CUR = int.Parse(ddlDepartamentoSerCur.SelectedValue);
            tb01.CO_SUB_DPTO_CUR = int.Parse(ddlCoordenacaoSerCur.SelectedValue);
            tb01.CO_COOR = ddlCoordenadorSerCur.SelectedValue != "" ? (int?)int.Parse(ddlCoordenadorSerCur.SelectedValue) : null;
            tb01.DE_INF_LEG_CUR = txtInformacaoLegalSerCur.Text;
            tb01.NU_PORTA_CUR = txtNumeroPortariaSerCur.Text;
            tb01.NU_DOU_CUR = txtDouSerCur.Text;
            tb01.DT_CRIA_CUR = DateTime.TryParse(txtDataCriacaoSerCur.Text, out dataRetorno) ? dataRetorno : DateTime.Now;
            tb01.NO_MENT_CUR = txtMentorSerCur.Text;
            tb01.QT_CARG_HORA_CUR = decimal.TryParse(txtCargaHorariaSerCur.Text, out decimalRetorno) ? decimalRetorno : 0;
            tb01.CO_PREDEC_CUR = ddlProximaSerieSerCur.SelectedValue != "" ? (int?)int.Parse(ddlProximaSerieSerCur.SelectedValue) : null;
            tb01.CO_POSTE_CUR = ddlCursoAnterior.SelectedValue != "" ? (int?)int.Parse(ddlCursoAnterior.SelectedValue) : null;
            tb01.PE_CERT_CUR = decimal.TryParse(txtNotaFinalAprovSerCur.Text, out decimalRetorno) ? decimalRetorno : 0;
            tb01.QT_MAT_DEP_MAT = int.TryParse(txtQtdeDependenciaSerCur.Text, out intRetorno) ? (int?)intRetorno : null;
            tb01.CO_PARAM_FREQUE = ddlControleFrequenciaSerCur.SelectedValue;
            tb01.MED_FINAL_CUR = decimal.TryParse(txtMediaFinalAprovSerCur.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb01.QT_AULA_CUR = int.TryParse(txtQtdeAulaSerCur.Text, out intRetorno) ? (int?)intRetorno : null;
            tb01.CO_PARAM_FREQ_TIPO = ddlRegistroFreqSerCur.SelectedValue;
            tb01.PE_FALT_CUR = decimal.TryParse(txtPorcentagemFaltaReprovSerCur.Text, out decimalRetorno) ? decimalRetorno : 0;
            tb01.CO_TIPO_GRADE_CUR = ddlTpGradeCursoSerCur.SelectedValue != "" ? ddlTpGradeCursoSerCur.SelectedValue : null;
            tb01.FLA_DIPLOMA = ddlDiplomaSerCur.SelectedValue;
            tb01.CO_SITU = ddlSituacaoSerCur.SelectedValue;
            tb01.DT_SITU_CUR = DateTime.TryParse(txtDataSituacaoSerCur.Text, out dataRetorno) ? dataRetorno : DateTime.Now;
            tb01.TB009_RTF_DOCTOS = ddlContrPrestServi.SelectedValue != "" ? TB009_RTF_DOCTOS.RetornaPelaChavePrimaria(int.Parse(ddlContrPrestServi.SelectedValue)) : null;
            tb01.ID_DOCUM_PRE = ddlContrPrestServiP.SelectedValue != "" ? int.Parse(ddlContrPrestServiP.SelectedValue) : new int();
            tb01.ID_HISTO_MATRI = ddlHistoricoMatr.SelectedValue != "" ? (int?)int.Parse(ddlHistoricoMatr.SelectedValue) : null;
            tb01.ID_HISTO_PRE_MATRI = ddlHistPreMatr.SelectedValue != "" ? (int?)int.Parse(ddlHistPreMatr.SelectedValue) : null;
            tb01.ID_HISTO_MENSA = ddlHistoricoMensa.SelectedValue != "" ? (int?)int.Parse(ddlHistoricoMensa.SelectedValue) : null;

            tb01.QT_DIAS_PARC1 = txtdpi.Text != "" ? (int?)int.Parse(txtdpi.Text) : null;
            tb01.QT_INTERV_DIAS = txtidp.Text != "" ? (int?)int.Parse(txtidp.Text) : null;
            tb01.FL_VENCI_FIXO = ckFixo.Checked ? "S" : "N";
            //tb01.VL_INTEG = txtValorInteg.Text != "" ? (decimal?)Decimal.Parse(txtValorInteg.Text) : null;
            //tb01.VL_TOTA_CUR = txtValorSerCur.Text != "" ? (decimal?)Decimal.Parse(txtValorSerCur.Text) : null;
            tb01.VL_TOTA_CUR = txtVlPraMan.Text != "" ? Convert.ToDecimal(txtVlPraMan.Text) : 0;
            tb01.VL_INTEG = txtVlPraInt.Text != "" ? Convert.ToDecimal(txtVlPraInt.Text) : 0;

            tb01.VL_CONTMAN_AVIST = txtVlVistMan.Text != "" ? (decimal?)Decimal.Parse(txtVlVistMan.Text) : null;
            tb01.VL_CONTTAR_AVIST = txtVlVistTar.Text != "" ? (decimal?)Decimal.Parse(txtVlVistTar.Text) : null;
            tb01.VL_CONTNOI_AVIST = txtVlVistNoi.Text != "" ? (decimal?)Decimal.Parse(txtVlVistNoi.Text) : null;
            tb01.VL_CONTINT_AVIST = txtVlVistInt.Text != "" ? (decimal?)Decimal.Parse(txtVlVistInt.Text) : null;

            tb01.VL_CONTMAN_APRAZ = txtVlPraMan.Text != "" ? (decimal?)Decimal.Parse(txtVlPraMan.Text) : null;
            tb01.VL_CONTTAR_APRAZ = txtVlPraTar.Text != "" ? (decimal?)Decimal.Parse(txtVlPraTar.Text) : null;
            tb01.VL_CONTNOI_APRAZ = txtVlPraNoi.Text != "" ? (decimal?)Decimal.Parse(txtVlPraNoi.Text) : null;
            tb01.VL_CONTINT_APRAZ = txtVlPraInt.Text != "" ? (decimal?)Decimal.Parse(txtVlPraInt.Text) : null;

            tb01.VL_TXMAT_MAN = txtTxMatMan.Text != "" ? (decimal?)Decimal.Parse(txtTxMatMan.Text) : null;
            tb01.VL_TXMAT_TAR = txtTxMatTar.Text != "" ? (decimal?)Decimal.Parse(txtTxMatTar.Text) : null;
            tb01.VL_TXMAT_NOI = txtTxMatNoi.Text != "" ? (decimal?)Decimal.Parse(txtTxMatNoi.Text) : null;
            tb01.VL_TXMAT_INT = txtTxMatInt.Text != "" ? (decimal?)Decimal.Parse(txtTxMatInt.Text) : null;

            tb01.VL_TXMAT_MAN_PRE = txtTxMatManP.Text != "" ? (decimal?)Decimal.Parse(txtTxMatManP.Text) : null;
            tb01.VL_TXMAT_TAR_PRE = txtTxMatTarP.Text != "" ? (decimal?)Decimal.Parse(txtTxMatTarP.Text) : null;
            tb01.VL_TXMAT_NOI_PRE = txtTxMatNoiP.Text != "" ? (decimal?)Decimal.Parse(txtTxMatNoiP.Text) : null;
            tb01.VL_TXMAT_INT_PRE = txtTxMatIntP.Text != "" ? (decimal?)Decimal.Parse(txtTxMatIntP.Text) : null;

            tb01.FL_VALCON_APRAZ = chkPrazo.Checked == true ? "S" : "N";
            tb01.FL_VALCON_AVIST = chkVista.Checked == true ? "S" : "N";
            tb01.FL_VALCON_TXMAT = chkTaxaMatr.Checked == true ? "S" : "N";
            tb01.FL_VALCON_TXMAT_PRE = chkTaxaMatrP.Checked == true ? "S" : "N";
            tb01.CO_CALC_MEDIA = ddlPadraoCalculo.SelectedValue;

            ///Novas colunas da nova tela de curso 24/12/2013
            tb01.CO_TIPO_LANC_NOTA = (ddlTpLanctNota.SelectedValue != "" ? ddlTpLanctNota.SelectedValue : null);
            tb01.FL_PROVA_FINAL = chkMedPFinal.Checked;
            tb01.VL_NOTA_PROVA_FINAL = ((chkMedPFinal.Checked && txtNotaFinalAprovSerCur.Text != "") ? decimal.Parse(txtNotaFinalAprovSerCur.Text) : new Decimal());
            tb01.FL_RECU = chkRecu.Checked;
            tb01.VL_NOTA_RECU = ((chkRecu.Checked && txtVLMedRecu.Text != "") ? decimal.Parse(txtVLMedRecu.Text) : new Decimal());
            tb01.QT_MATE_RECU = ((chkRecu.Checked && txtQtdMatRecu.Text != "") ? int.Parse(txtQtdMatRecu.Text) : new int());
            tb01.FL_DEPE = chkDepe.Checked;
            tb01.VL_NOTA_DEPE = ((chkDepe.Checked && txtVLMedDepe.Text != "") ? decimal.Parse(txtVLMedDepe.Text) : new Decimal());
            tb01.QT_MATE_DEPE = ((chkDepe.Checked && txtQtdMatDepe.Text != "") ? int.Parse(txtQtdMatDepe.Text) : new int());
            tb01.FL_RECU_BIM = (chkRecBim.Checked ? "S" : "N");
            tb01.VL_NOTA_RECU_BIM = (!string.IsNullOrEmpty(txtVlMedRecuBim.Text) ? decimal.Parse(txtVlMedRecuBim.Text) : (decimal?)null);
            tb01.QT_MATE_RECU_BIM = (!string.IsNullOrEmpty(txtqtMatRecBim.Text) ? int.Parse(txtqtMatRecBim.Text) : (int?)null);
            tb01.FL_CONS = chkCons.Checked;
            tb01.VL_NOTA_CONS = ((chkCons.Checked && txtVLMedCons.Text != "") ? decimal.Parse(txtVLMedCons.Text) : new Decimal());
            tb01.QT_MATE_CONS = ((chkCons.Checked && txtQtdMatCons.Text != "") ? int.Parse(txtQtdMatCons.Text) : new int());
            tb01.NU_MDV = (ddlMdv.SelectedValue != "" ? int.Parse(ddlMdv.SelectedValue) : new int());
            tb01.FL_MENS_TAXA_MATR = (ddlPrimMenTaxMatr.SelectedValue == "S" ? true : false);
            tb01.VL_CONTESP_AVIST = ((chkVista.Checked && txtVlVistEsp.Text != "") ? decimal.Parse(txtVlVistEsp.Text) : new Decimal());
            tb01.VL_CONTESP_APRAZ = ((chkPrazo.Checked && txtVlPraEsp.Text != "") ? decimal.Parse(txtVlPraEsp.Text) : new Decimal());
            tb01.VL_TXMAT_ESP = ((chkTaxaMatr.Checked && txtTxMatEsp.Text != "") ? decimal.Parse(txtTxMatEsp.Text) : new Decimal());
            tb01.ID_BOLETO_MATR = (ddlBolMatr.SelectedValue != "" ? int.Parse(ddlBolMatr.SelectedValue) : new int());
            tb01.FL_VALCON_AVIST_PRE = (chkVlVistMatrP.Checked ? "S" : "N");
            tb01.VL_CONTMAN_AVIST_PRE = ((chkVlVistMatrP.Checked && txtVlVistManP.Text != "") ? decimal.Parse(txtVlVistManP.Text) : new Decimal());
            tb01.VL_CONTTAR_AVIST_PRE = ((chkVlVistMatrP.Checked && txtVlVistTarP.Text != "") ? decimal.Parse(txtVlVistTarP.Text) : new Decimal());
            tb01.VL_CONTNOI_AVIST_PRE = ((chkVlVistMatrP.Checked && txtVlVistNoiP.Text != "") ? decimal.Parse(txtVlVistNoiP.Text) : new Decimal());
            tb01.VL_CONTINT_AVIST_PRE = ((chkVlVistMatrP.Checked && txtVlVistIntP.Text != "") ? decimal.Parse(txtVlVistIntP.Text) : new Decimal());
            tb01.VL_CONTESP_AVIST_PRE = ((chkVlVistMatrP.Checked && txtVlVistEspP.Text != "") ? decimal.Parse(txtVlVistEspP.Text) : new Decimal());
            tb01.FL_VALCON_APRAZ_PRE = (chkVlPrazMatrP.Checked ? "S" : "N");
            tb01.VL_CONTMAN_APRAZ_PRE = ((chkVlPrazMatrP.Checked && txtVlPrazManP.Text != "") ? decimal.Parse(txtVlPrazManP.Text) : new Decimal());
            tb01.VL_CONTTAR_APRAZ_PRE = ((chkVlPrazMatrP.Checked && txtVlPrazTarP.Text != "") ? decimal.Parse(txtVlPrazTarP.Text) : new Decimal());
            tb01.VL_CONTNOI_APRAZ_PRE = ((chkVlPrazMatrP.Checked && txtVlPrazNoiP.Text != "") ? decimal.Parse(txtVlPrazNoiP.Text) : new Decimal());
            tb01.VL_CONTINT_APRAZ_PRE = ((chkVlPrazMatrP.Checked && txtVlPrazIntP.Text != "") ? decimal.Parse(txtVlPrazIntP.Text) : new Decimal());
            tb01.VL_CONTESP_APRAZ_PRE = ((chkVlPrazMatrP.Checked && txtVlPrazEspP.Text != "") ? decimal.Parse(txtVlPrazEspP.Text) : new Decimal());
            tb01.ID_BOLETO_PRE = (ddlBolPreMatr.SelectedValue != "" ? int.Parse(ddlBolPreMatr.SelectedValue) : new int());
            tb01.CO_TIPO_BOLETO_MATR = (ddlTpBol.SelectedValue != "" ? ddlTpBol.SelectedValue : null);
            tb01.CO_TIPO_BOLETO_PRE = (ddlTpBolPreMatr.SelectedValue != "" ? ddlTpBolPreMatr.SelectedValue : null);
            ///Fim novas colunas
            ///Novas colunas curso 26/12/2013
            tb01.CO_PREDEC_MOD = ((ddlProximaModalidade.SelectedValue != null && ddlProximaModalidade.SelectedValue != "") ? int.Parse(ddlProximaModalidade.SelectedValue) : new int());
            tb01.CO_POSTE_MOD = ((ddlModalidaAnterior.SelectedValue != "") ? int.Parse(ddlModalidaAnterior.SelectedValue) : new int());
            ///Fim novas colunas

            if (Page.IsValid)
            {
                if (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                {
                    //----------------> Deve ser salvo primeiro a tabela de série, para depois salvar a de Datas de Controle e Informações da Série 
                    if (GestorEntities.SaveOrUpdate(tb01, true) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");
                    else
                    {
                        if (tb81 == null)
                        {
                            tb81 = new TB81_DATA_CTRL();
                            tb81.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(tb01.CO_EMP, tb01.CO_MODU_CUR, tb01.CO_CUR);
                        }

                        //--------------------> Se for um novo registro, deve inserir as chaves primárias
                        if (tb19 == null)
                        {
                            tb19 = new TB19_INFOR_CURSO();
                            tb19.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(tb01.CO_EMP, tb01.CO_MODU_CUR, tb01.CO_CUR);
                        }

                        tb81.DT_INI_RES = DateTime.TryParse(txtDataInicioReservaSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_FIM_RES = DateTime.TryParse(txtDataFimReservaSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_INI_INSC = DateTime.TryParse(txtDataInicioPreMatriculaSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_FIM_INSC = DateTime.TryParse(txtDataFimPreMatriculaSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_INI_MAT = DateTime.TryParse(txtDataInicioMatriculaSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_FIM_MAT = DateTime.TryParse(txtDataFimMatriculaSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_INI_MANU = DateTime.TryParse(txtDataInicioTrancamentoSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                        tb81.DT_FIM_MANU = DateTime.TryParse(txtDataFimTrancamentoSerCur.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;

                        if (GestorEntities.SaveOrUpdate(tb81) <= 0)
                            AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");
                        else
                        {
                            tb19.DE_OBJE_CUR = txtObjetivoSerCur.Text;
                            if (GestorEntities.SaveOrUpdate(tb19) <= 0)
                                AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");

                            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                                CurrentPadraoCadastros.CurrentEntity = tb81;
                            else
                                CurrentPadraoCadastros.CurrentEntity = tb01;

                        }
                    }
                }
            }
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            TB01_CURSO tb01 = RetornaEntidadeTB01();
            TB81_DATA_CTRL tb81 = RetornaEntidadeTB81();
            TB19_INFOR_CURSO tb19 = RetornaEntidadeTB19();

            if (tb81 != null)
            {
                if (GestorEntities.Delete(tb81) <= 0)
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                else
                {
                    //----------------> Primeiro deve excluir a tabela de informações da série, se não der erro, exclui a tabela de série
                    if (tb19 != null)
                    {
                        if (GestorEntities.Delete(tb19) <= 0)
                            AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                        else if (GestorEntities.Delete(tb01) <= 0)
                            AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                    }
                    else if (GestorEntities.Delete(tb01) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                }
            }
            else
            {
                if (tb19 != null)
                {
                    if (GestorEntities.Delete(tb19) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                    else if (GestorEntities.Delete(tb01) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                }
                else if (GestorEntities.Delete(tb01) <= 0)
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB01_CURSO tb01 = RetornaEntidadeTB01();
            TB81_DATA_CTRL tb81 = RetornaEntidadeTB81();
            TB19_INFOR_CURSO tb19 = RetornaEntidadeTB19();

            if (tb01.CO_CUR != 0)
            {
                CarregaTurmaPreMatr();
                tb01.TB009_RTF_DOCTOSReference.Load();
                //------------> Informações de Série
                //txtValorSerCur.Text = tb01.VL_TOTA_CUR.ToString();
                txtCargaHorariaSerCur.Text = tb01.QT_CARG_HORA_CUR.ToString();
                txtDataCriacaoSerCur.Text = tb01.DT_CRIA_CUR.ToString("dd/MM/yyyy");
                txtDataSituacaoSerCur.Text = tb01.DT_SITU_CUR.ToString("dd/MM/yyyy");
                txtDescricaoSerCur.Text = tb01.NO_CUR;
                txtDescricaoEPSerCur.Text = tb01.NO_REFER;
                txtQtdMesesSerCur.Text = tb01.NU_QUANT_MESES.ToString();
                txtDouSerCur.Text = tb01.NU_DOU_CUR;
                txtInformacaoLegalSerCur.Text = tb01.DE_INF_LEG_CUR;
                txtMediaFinalAprovSerCur.Text = tb01.MED_FINAL_CUR.ToString();
                txtMentorSerCur.Text = tb01.NO_MENT_CUR;
                txtNotaFinalAprovSerCur.Text = tb01.PE_CERT_CUR.ToString();
                txtNumeroImpressaoSerCur.Text = tb01.SEQ_IMPRESSAO.ToString();
                txtNumeroPortariaSerCur.Text = tb01.NU_PORTA_CUR;
                txtObjetivoSerCur.Text = tb01.DE_INF_LEG_CUR;
                txtPorcentagemFaltaReprovSerCur.Text = tb01.PE_FALT_CUR.ToString();
                txtQtdeAulaSerCur.Text = tb01.QT_AULA_CUR.ToString();
                txtQtdeDependenciaSerCur.Text = tb01.QT_MAT_DEP_MAT.ToString();
                txtReferenciaEFSerCur.Text = tb01.NO_ENSINO_FUND_ANTERIOR;
                txtSiglaSerCur.Text = tb01.CO_SIGL_CUR;
                txtSiglaEPSerCur.Text = tb01.CO_SIGL_REFER;
                ddlDiplomaSerCur.SelectedValue = tb01.FLA_DIPLOMA;
                ddlControleFrequenciaSerCur.SelectedValue = tb01.CO_PARAM_FREQUE;
                ddlRegistroFreqSerCur.SelectedValue = tb01.CO_PARAM_FREQ_TIPO;
                ddlDepartamentoSerCur.SelectedValue = tb01.CO_DPTO_CUR.ToString();
                ddlTpGradeCursoSerCur.SelectedValue = tb01.CO_TIPO_GRADE_CUR != null ? tb01.CO_TIPO_GRADE_CUR : "";
                CarregaCoordenacoes();
                ddlCoordenacaoSerCur.SelectedValue = tb01.CO_SUB_DPTO_CUR.ToString();
                ddlModalidadeSerCur.SelectedValue = tb01.CO_MODU_CUR.ToString();
                ddlModalidadeSerCur.Enabled = false;
                if (tb01.CO_PREDEC_CUR != null && tb01.CO_PREDEC_MOD != null)
                {
                    ddlProximaModalidade.SelectedValue = tb01.CO_PREDEC_MOD.ToString() ?? "";
                    CarregaSeries(tb01.CO_PREDEC_MOD ?? 0);
                    ddlProximaSerieSerCur.SelectedValue = tb01.CO_PREDEC_CUR.ToString() ?? "";
                }
                if (tb01.CO_POSTE_CUR != null && tb01.CO_POSTE_MOD != null)
                {
                    ddlModalidaAnterior.SelectedValue = tb01.CO_POSTE_MOD.ToString() ?? "";
                    carregaSerieAnterior();
                    ddlCursoAnterior.SelectedValue = tb01.CO_POSTE_CUR.ToString() ?? "";
                }
                ddlSituacaoSerCur.SelectedValue = tb01.CO_SITU;
                ddlCoordenadorSerCur.SelectedValue = tb01.CO_COOR.ToString();
                ddlClassificacaoSerCur.SelectedValue = tb01.CO_NIVEL_CUR + " ";
                CarregaReferenciaSerieRelatorio();
                ddlReferSerieRel.SelectedValue = tb01.CO_SERIE_REFER != null ? tb01.CO_SERIE_REFER : "";
                ddlContrPrestServi.SelectedValue = tb01.TB009_RTF_DOCTOS != null ? tb01.TB009_RTF_DOCTOS.ID_DOCUM.ToString() : "";
                ddlContrPrestServiP.SelectedValue = tb01.ID_DOCUM_PRE != null ? tb01.ID_DOCUM_PRE.ToString() : "";
                //Select das novas colunas do BD Gestor.
                //txtValorInteg.Text = tb01.VL_INTEG.ToString();
                txtdpi.Text = tb01.QT_DIAS_PARC1 != null ? tb01.QT_DIAS_PARC1.Value.ToString() : "";
                txtidp.Text = tb01.QT_INTERV_DIAS != null ? tb01.QT_INTERV_DIAS.Value.ToString() : "";
                ckFixo.Checked = String.IsNullOrEmpty(tb01.FL_VENCI_FIXO) ? false : tb01.FL_VENCI_FIXO.Equals("S") ? true : false;
                ddlHistoricoMatr.SelectedValue = tb01.ID_HISTO_MATRI != null ? tb01.ID_HISTO_MATRI.ToString() : "";
                ddlHistoricoMensa.SelectedValue = tb01.ID_HISTO_MENSA != null ? tb01.ID_HISTO_MENSA.ToString() : "";
                ddlHistPreMatr.SelectedValue = tb01.ID_HISTO_PRE_MATRI != null ? tb01.ID_HISTO_PRE_MATRI.ToString() : "";
                ddlPadraoCalculo.SelectedValue = tb01.CO_CALC_MEDIA ?? padroesCalc[padraoCalculoMedia.PADR];

                chkRecBim.Checked = (tb01.FL_RECU_BIM == "S" ? true : false);
                txtqtMatRecBim.Text = tb01.QT_MATE_RECU_BIM.ToString();
                txtVlMedRecuBim.Text = tb01.VL_NOTA_RECU_BIM.ToString();

                ///Novas colunas da nova tela de curso 24/12/2013
                ddlTpLanctNota.SelectedValue = tb01.CO_TIPO_LANC_NOTA ?? "";
                chkMedPFinal.Checked = tb01.FL_PROVA_FINAL;
                if (tb01.FL_PROVA_FINAL && tb01.VL_NOTA_PROVA_FINAL != null)
                    txtNotaFinalAprovSerCur.Text = tb01.VL_NOTA_PROVA_FINAL.ToString();
                chkRecu.Checked = tb01.FL_RECU;
                if (tb01.FL_RECU)
                {
                    txtVLMedRecu.Text = (tb01.VL_NOTA_RECU != null ? tb01.VL_NOTA_RECU.ToString() : "");
                    txtQtdMatRecu.Text = (tb01.QT_MATE_RECU != null ? tb01.QT_MATE_RECU.ToString() : "");
                }
                chkDepe.Checked = tb01.FL_DEPE;
                if (tb01.FL_DEPE)
                {
                    txtVLMedDepe.Text = (tb01.VL_NOTA_DEPE != null ? tb01.VL_NOTA_DEPE.ToString() : "");
                    txtQtdMatDepe.Text = (tb01.QT_MATE_DEPE != null ? tb01.QT_MATE_DEPE.ToString() : "");
                }
                chkCons.Checked = tb01.FL_CONS;
                if (tb01.FL_CONS)
                {
                    txtVLMedCons.Text = (tb01.VL_NOTA_CONS != null ? tb01.VL_NOTA_CONS.ToString() : "");
                    txtQtdMatCons.Text = (tb01.QT_MATE_CONS != null ? tb01.QT_MATE_CONS.ToString() : "");
                }
                ddlMdv.SelectedValue = (tb01.NU_MDV != null ? tb01.NU_MDV.ToString() : "");
                ddlPrimMenTaxMatr.SelectedValue = (tb01.FL_MENS_TAXA_MATR ? "S" : "N");
                if (tb01.FL_VALCON_AVIST == "S")
                    txtVlVistEsp.Text = (tb01.VL_CONTESP_AVIST != null ? tb01.VL_CONTESP_AVIST.ToString() : "");
                if (tb01.FL_VALCON_APRAZ == "S")
                    txtVlPraEsp.Text = (tb01.VL_CONTESP_APRAZ != null ? tb01.VL_CONTESP_APRAZ.ToString() : "");
                if (tb01.FL_VALCON_TXMAT == "S")
                    txtTxMatEsp.Text = (tb01.VL_TXMAT_ESP != null ? tb01.VL_TXMAT_ESP.ToString() : "");
                ddlBolMatr.SelectedValue = (tb01.ID_BOLETO_MATR != null ? tb01.ID_BOLETO_MATR.ToString() : "");
                chkVlVistMatrP.Checked = (tb01.FL_VALCON_AVIST_PRE == "S" ? true : false);
                if (chkVlVistMatrP.Checked)
                {
                    txtVlVistManP.Enabled = true;
                    txtVlVistTarP.Enabled = true;
                    txtVlVistNoiP.Enabled = true;
                    txtVlVistIntP.Enabled = true;
                    txtVlVistEspP.Enabled = true;

                    txtVlVistManP.Text = (tb01.VL_CONTMAN_AVIST_PRE != null ? tb01.VL_CONTMAN_AVIST_PRE.ToString() : "");
                    txtVlVistTarP.Text = (tb01.VL_CONTTAR_AVIST_PRE != null ? tb01.VL_CONTTAR_AVIST_PRE.ToString() : "");
                    txtVlVistNoiP.Text = (tb01.VL_CONTNOI_AVIST_PRE != null ? tb01.VL_CONTNOI_AVIST_PRE.ToString() : "");
                    txtVlVistIntP.Text = (tb01.VL_CONTINT_AVIST_PRE != null ? tb01.VL_CONTINT_AVIST_PRE.ToString() : "");
                    txtVlVistEspP.Text = (tb01.VL_CONTESP_AVIST_PRE != null ? tb01.VL_CONTESP_AVIST_PRE.ToString() : "");
                }
                chkVlPrazMatrP.Checked = (tb01.FL_VALCON_APRAZ_PRE == "S" ? true : false);
                if (chkVlPrazMatrP.Checked)
                {
                    txtVlPrazManP.Enabled = true;
                    txtVlPrazTarP.Enabled = true;
                    txtVlPrazNoiP.Enabled = true;
                    txtVlPrazIntP.Enabled = true;
                    txtVlPrazEspP.Enabled = true;

                    txtVlPrazManP.Text = (tb01.VL_CONTMAN_APRAZ_PRE != null ? tb01.VL_CONTMAN_APRAZ_PRE.ToString() : "");
                    txtVlPrazTarP.Text = (tb01.VL_CONTTAR_APRAZ_PRE != null ? tb01.VL_CONTTAR_APRAZ_PRE.ToString() : "");
                    txtVlPrazNoiP.Text = (tb01.VL_CONTNOI_APRAZ_PRE != null ? tb01.VL_CONTNOI_APRAZ_PRE.ToString() : "");
                    txtVlPrazIntP.Text = (tb01.VL_CONTINT_APRAZ_PRE != null ? tb01.VL_CONTINT_APRAZ_PRE.ToString() : "");
                    txtVlPrazEspP.Text = (tb01.VL_CONTESP_APRAZ_PRE != null ? tb01.VL_CONTESP_APRAZ_PRE.ToString() : "");
                }
                ddlBolPreMatr.SelectedValue = (tb01.ID_BOLETO_PRE != null ? tb01.ID_BOLETO_PRE.ToString() : "");
                ddlTpBol.SelectedValue = (tb01.CO_TIPO_BOLETO_MATR != null ? tb01.CO_TIPO_BOLETO_MATR.ToString() : "");
                ddlTpBolPreMatr.SelectedValue = (tb01.CO_TIPO_BOLETO_PRE != null ? tb01.CO_TIPO_BOLETO_PRE.ToString() : "");
                ///Fim novas colunas

                if (tb01.FL_VALCON_AVIST == "S")
                {
                    chkVista.Checked = true;
                    txtVlVistInt.Enabled = true;
                    txtVlVistMan.Enabled = true;
                    txtVlVistTar.Enabled = true;
                    txtVlVistNoi.Enabled = true;
                    txtVlVistEsp.Enabled = true;

                    txtVlVistMan.Text = tb01.VL_CONTMAN_AVIST != null ? tb01.VL_CONTMAN_AVIST.ToString() : "";
                    txtVlVistTar.Text = tb01.VL_CONTTAR_AVIST != null ? tb01.VL_CONTTAR_AVIST.ToString() : "";
                    txtVlVistNoi.Text = tb01.VL_CONTNOI_AVIST != null ? tb01.VL_CONTNOI_AVIST.ToString() : "";
                    txtVlVistInt.Text = tb01.VL_CONTINT_AVIST != null ? tb01.VL_CONTINT_AVIST.ToString() : "";
                    txtVlVistEsp.Text = tb01.VL_CONTESP_AVIST != null ? tb01.VL_CONTESP_AVIST.ToString() : "";
                }

                if (tb01.FL_VALCON_APRAZ == "S")
                {
                    chkPrazo.Checked = true;
                    txtVlPraInt.Enabled = true;
                    txtVlPraMan.Enabled = true;
                    txtVlPraTar.Enabled = true;
                    txtVlPraNoi.Enabled = true;
                    txtVlPraEsp.Enabled = true;

                    txtVlPraMan.Text = tb01.VL_CONTMAN_APRAZ != null ? tb01.VL_CONTMAN_APRAZ.ToString() : "";
                    txtVlPraTar.Text = tb01.VL_CONTTAR_APRAZ != null ? tb01.VL_CONTTAR_APRAZ.ToString() : "";
                    txtVlPraNoi.Text = tb01.VL_CONTNOI_APRAZ != null ? tb01.VL_CONTNOI_APRAZ.ToString() : "";
                    txtVlPraInt.Text = tb01.VL_CONTINT_APRAZ != null ? tb01.VL_CONTINT_APRAZ.ToString() : "";
                    txtVlPraEsp.Text = tb01.VL_CONTESP_APRAZ != null ? tb01.VL_CONTESP_APRAZ.ToString() : "";
                }

                if (tb01.FL_VALCON_TXMAT == "S")
                {
                    chkTaxaMatr.Checked = true;
                    txtTxMatInt.Enabled = true;
                    txtTxMatMan.Enabled = true;
                    txtTxMatNoi.Enabled = true;
                    txtTxMatTar.Enabled = true;
                    txtTxMatEsp.Enabled = true;

                    txtTxMatMan.Text = tb01.VL_TXMAT_MAN != null ? tb01.VL_TXMAT_MAN.ToString() : "";
                    txtTxMatTar.Text = tb01.VL_TXMAT_TAR != null ? tb01.VL_TXMAT_TAR.ToString() : "";
                    txtTxMatNoi.Text = tb01.VL_TXMAT_NOI != null ? tb01.VL_TXMAT_NOI.ToString() : "";
                    txtVlVistInt.Text = tb01.VL_CONTINT_AVIST != null ? tb01.VL_CONTINT_AVIST.ToString() : "";
                    txtTxMatInt.Text = tb01.VL_TXMAT_INT != null ? tb01.VL_TXMAT_INT.ToString() : "";
                    txtTxMatEsp.Text = tb01.VL_TXMAT_ESP != null ? tb01.VL_TXMAT_ESP.ToString() : "";
                }

                if (tb01.FL_VALCON_TXMAT_PRE != null && tb01.FL_VALCON_TXMAT_PRE == "S")
                {
                    chkTaxaMatrP.Checked = true;
                    txtTxMatIntP.Enabled = true;
                    txtTxMatManP.Enabled = true;
                    txtTxMatNoiP.Enabled = true;
                    txtTxMatTarP.Enabled = true;
                    txtTxMatEsp.Enabled = true;

                    txtTxMatManP.Text = tb01.VL_TXMAT_MAN_PRE != null ? tb01.VL_TXMAT_MAN_PRE.ToString() : "";
                    txtTxMatTarP.Text = tb01.VL_TXMAT_TAR_PRE != null ? tb01.VL_TXMAT_TAR_PRE.ToString() : "";
                    txtTxMatNoiP.Text = tb01.VL_TXMAT_NOI_PRE != null ? tb01.VL_TXMAT_NOI_PRE.ToString() : "";
                    txtTxMatIntP.Text = tb01.VL_TXMAT_INT_PRE != null ? tb01.VL_TXMAT_INT_PRE.ToString() : "";
                    txtTxMatEsp.Text = tb01.VL_TXMAT_ESP_PRE != null ? tb01.VL_TXMAT_ESP_PRE.ToString() : "";
                }
            }

            if (tb81 != null)
            {
                //------------> Informações de Datas de Controle
                txtDataInicioReservaSerCur.Text = tb81.DT_INI_RES != null ? tb81.DT_INI_RES.Value.ToString("dd/MM/yyyy") : "";
                txtDataFimReservaSerCur.Text = tb81.DT_FIM_RES != null ? tb81.DT_FIM_RES.Value.ToString("dd/MM/yyyy") : "";
                txtDataInicioPreMatriculaSerCur.Text = tb81.DT_INI_INSC != null ? tb81.DT_INI_INSC.Value.ToString("dd/MM/yyyy") : "";
                txtDataFimPreMatriculaSerCur.Text = tb81.DT_FIM_INSC != null ? tb81.DT_FIM_INSC.Value.ToString("dd/MM/yyyy") : "";
                txtDataInicioMatriculaSerCur.Text = tb81.DT_INI_MAT != null ? tb81.DT_INI_MAT.Value.ToString("dd/MM/yyyy") : "";
                txtDataFimMatriculaSerCur.Text = tb81.DT_FIM_MAT != null ? tb81.DT_FIM_MAT.Value.ToString("dd/MM/yyyy") : "";
                txtDataInicioTrancamentoSerCur.Text = tb81.DT_INI_MANU != null ? tb81.DT_INI_MANU.Value.ToString("dd/MM/yyyy") : "";
                txtDataFimTrancamentoSerCur.Text = tb81.DT_FIM_MANU != null ? tb81.DT_FIM_MANU.Value.ToString("dd/MM/yyyy") : "";
            }

            if (tb19 != null)
                txtObjetivoSerCur.Text = tb19.DE_OBJE_CUR;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB01_CURSO</returns>
        private TB01_CURSO RetornaEntidadeTB01()
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb01 == null) ? new TB01_CURSO() : tb01;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB81_DATA_CTRL</returns>
        private TB81_DATA_CTRL RetornaEntidadeTB81()
        {
            TB81_DATA_CTRL tb81 = TB81_DATA_CTRL.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return tb81;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB19_INFOR_CURSO</returns>
        private TB19_INFOR_CURSO RetornaEntidadeTB19()
        {
            TB19_INFOR_CURSO tb19 = TB19_INFOR_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return tb19;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega as Séries Anterioes
        /// </summary>
        private void carregaSerieAnterior()
        {
            int modalidade = (ddlModalidaAnterior.SelectedValue != "" ? int.Parse(ddlModalidaAnterior.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlCursoAnterior, modalidade, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Adiciona os tipos de cálculos de médias disponíveis, que são 2 padrões
        /// </summary>
        private void CarregarPadraoCalculo()
        {
            ddlPadraoCalculo.Items.Clear();
            ddlPadraoCalculo.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(padraoCalculoMedia.ResourceManager));

            if (ddlPadraoCalculo.Items.FindByValue(padroesCalc[padraoCalculoMedia.PADR]) != null)
                ddlPadraoCalculo.SelectedValue = padroesCalc[padraoCalculoMedia.PADR];
        }

        /// <summary>
        /// Método que carrega o dropdown de Histórico de Matrícula e Mensalidade
        /// </summary>
        private void CarregaHistoricos()
        {
            ddlHistoricoMatr.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                           where tb39.FLA_TIPO_HISTORICO == "C"
                                           select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistoricoMatr.DataTextField = "DE_HISTORICO";
            ddlHistoricoMatr.DataValueField = "CO_HISTORICO";
            ddlHistoricoMatr.DataBind();

            ddlHistoricoMatr.Items.Insert(0, new ListItem("", ""));

            ddlHistoricoMensa.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                            where tb39.FLA_TIPO_HISTORICO == "C"
                                            select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistoricoMensa.DataTextField = "DE_HISTORICO";
            ddlHistoricoMensa.DataValueField = "CO_HISTORICO";
            ddlHistoricoMensa.DataBind();

            ddlHistoricoMensa.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregaHistoricosPreMatr()
        {
            ddlHistPreMatr.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                         where tb39.FLA_TIPO_HISTORICO == "C"
                                         select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistPreMatr.DataTextField = "DE_HISTORICO";
            ddlHistPreMatr.DataValueField = "CO_HISTORICO";
            ddlHistPreMatr.DataBind();

            ddlHistPreMatr.Items.Insert(0, new ListItem("", ""));

            ddlHistPreMatr.DataSource = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                         where tb39.FLA_TIPO_HISTORICO == "C"
                                         select new { tb39.CO_HISTORICO, tb39.DE_HISTORICO });

            ddlHistPreMatr.DataTextField = "DE_HISTORICO";
            ddlHistPreMatr.DataValueField = "CO_HISTORICO";
            ddlHistPreMatr.DataBind();

            ddlHistPreMatr.Items.Insert(0, new ListItem("", ""));

        }

        /// <summary>
        /// Método que carrega o dropdown de Contrato de Prestação de Serviço
        /// </summary>
        private void CarregaContratos()
        {
            ddlContrPrestServi.DataSource = TB009_RTF_DOCTOS.RetornaTodosRegistros().Where(p => p.CO_SITUS_DOCUM == "A" && p.TP_DOCUM == "CO" && p.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlContrPrestServi.DataTextField = "NM_DOCUM";
            ddlContrPrestServi.DataValueField = "ID_DOCUM";
            ddlContrPrestServi.DataBind();

            ddlContrPrestServi.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Contrato de Prestação de Serviço de Pré-Matrícula
        /// </summary>
        private void CarregaContratosPre()
        {
            ddlContrPrestServiP.DataSource = TB009_RTF_DOCTOS.RetornaTodosRegistros().Where(p => p.CO_SITUS_DOCUM == "A" && p.TP_DOCUM == "CO" && p.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlContrPrestServiP.DataTextField = "NM_DOCUM";
            ddlContrPrestServiP.DataValueField = "ID_DOCUM";
            ddlContrPrestServiP.DataBind();

            ddlContrPrestServiP.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Classificação da Série
        /// </summary>
        private void CarregaClassificacoes()
        {
            ddlClassificacaoSerCur.DataSource = TB133_CLASS_CUR.RetornaTodosRegistros();

            ddlClassificacaoSerCur.DataTextField = "NO_CLASS_CUR";
            ddlClassificacaoSerCur.DataValueField = "CO_SIGLA_CLASS_CUR";
            ddlClassificacaoSerCur.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades Escolares
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidadeSerCur.Items.Clear();
            ddlModalidadeSerCur.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
        }

        private void CarregaConceitos()
        {
            var notaConceito = TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros().Where(c => c.CO_SITU_CONC == "A" && c.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP);
            if (notaConceito != null && notaConceito.Count() > 0)
            {
                ddlConcMedRecu.DataSource = notaConceito;
                ddlConcMedRecu.DataTextField = "DE_CONCEITO";
                ddlConcMedRecu.DataValueField = "CO_SIGLA_CONCEITO";
                ddlConcMedRecu.DataBind();
                ddlConcMedRecu.Items.Insert(0, new ListItem("Selecione", "0"));


                ddlMediaConcProvaFinal.DataSource = notaConceito;
                ddlMediaConcProvaFinal.DataTextField = "DE_CONCEITO";
                ddlMediaConcProvaFinal.DataValueField = "CO_SIGLA_CONCEITO";
                ddlMediaConcProvaFinal.DataBind();
                ddlMediaConcProvaFinal.Items.Insert(0, new ListItem("", ""));

                ddlMediaConcAprovCur.DataSource = notaConceito;
                ddlMediaConcAprovCur.DataTextField = "DE_CONCEITO";
                ddlMediaConcAprovCur.DataValueField = "CO_SIGLA_CONCEITO";
                ddlMediaConcAprovCur.DataBind();
                ddlMediaConcAprovCur.Items.Insert(0, new ListItem("", ""));

            }

        }

        private void CarregaMDV()
        {
            ddlMdv.Items.Clear();
            ddlMdv.Items.AddRange(AuxiliBaseApoio.DiaVencimentoTitulo());
        }

        private void CarregarTipoBoleto()
        {
            ddlTpBol.Items.Clear();
            ddlTpBol.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoBoletoBancario.ResourceManager));
            ddlTpBol.Items.Insert(0, new ListItem("Nennhum", ""));
        }

        private void CarregarTipoBoletoPre()
        {
            ddlTpBolPreMatr.Items.Clear();
            ddlTpBolPreMatr.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoBoletoBancario.ResourceManager));
            ddlTpBolPreMatr.Items.Insert(0, new ListItem("Nennhum", ""));
        }

        private void CarregaBoletos()
        {
            int coEmp = LoginAuxili.CO_EMP;

            AuxiliCarregamentos.CarregaBoletos(ddlBolMatr, coEmp, "", 0, 0, false, false);

            ddlBolMatr.Items.Insert(0, new ListItem("Nenhum", ""));

            var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                        where iTb83.CO_EMP == coEmp
                        select new { iTb83.ID_BOLETO_MATRIC, iTb83.ID_BOLETO_MENSA }).FirstOrDefault();

            //if (tb83 != null)
            //{
            //    if (tb83.ID_BOLETO_MENSA != null)
            //    {
            //        ddlBolMatr.SelectedValue = tb83.ID_BOLETO_MENSA.ToString();
            //    }
            //}
        }

        private void CarregaBoletosPre()
        {
            int coEmp = LoginAuxili.CO_EMP;

            AuxiliCarregamentos.CarregaBoletos(ddlBolPreMatr, coEmp, "", 0, 0, false, false);

            ddlBolPreMatr.Items.Insert(0, new ListItem("Nenhum", ""));

            var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                        where iTb83.CO_EMP == coEmp
                        select new { iTb83.ID_BOLETO_MATRIC, iTb83.ID_BOLETO_MENSA }).FirstOrDefault();

            //if (tb83 != null)
            //{
            //    if (tb83.ID_BOLETO_MENSA != null)
            //    {
            //        ddlBolPreMatr.SelectedValue = tb83.ID_BOLETO_MENSA.ToString();
            //    }
            //}
        }


        /// <summary>
        /// Método que carrega o dropdown de Próxima Série
        /// </summary>
        private void CarregaSeries(int codMod = 0)
        {
            if (codMod == 0 && ddlProximaModalidade.SelectedValue != "")
                int.Parse(ddlProximaModalidade.SelectedValue);
            ddlProximaSerieSerCur.Items.Clear();
            ddlProximaSerieSerCur.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, codMod, DateTime.Now.Year, selecione: true));
        }

        /// <summary>
        /// Carrega as modalidades para ser usada como referência para a próxima série
        /// </summary>
        private void CarregaModadlidadesProxima()
        {
            ddlProximaModalidade.Items.Clear();
            ddlProximaModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
        }

        private void CarregaTurmaPreMatr()
        {
            int serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            ddlTurmaPreMatr.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                          join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                                          where tb06.CO_CUR == serie
                                          select new
                                          {
                                              tb06.CO_TUR,
                                              tb129.NO_TURMA
                                          }
                                          );
            ddlTurmaPreMatr.DataTextField = "NO_TURMA";
            ddlTurmaPreMatr.DataValueField = "CO_TUR";
            ddlTurmaPreMatr.DataBind();
            ddlTurmaPreMatr.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamentos da Série
        /// </summary>
        private void CarregaDepartamentos()
        {
            ddlDepartamentoSerCur.DataSource = TB77_DPTO_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP);

            ddlDepartamentoSerCur.DataTextField = "NO_DPTO_CUR";
            ddlDepartamentoSerCur.DataValueField = "CO_DPTO_CUR";
            ddlDepartamentoSerCur.DataBind();

            ddlDepartamentoSerCur.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Coordenações da Série
        /// </summary>
        private void CarregaCoordenacoes()
        {
            int codDepto = ddlDepartamentoSerCur.SelectedValue != "" ? int.Parse(ddlDepartamentoSerCur.SelectedValue) : 0;

            ddlCoordenacaoSerCur.DataSource = (from tb68 in TB68_COORD_CURSO.RetornaTodosRegistros()
                                               where tb68.CO_EMP == LoginAuxili.CO_EMP && tb68.CO_DPTO_CUR == codDepto
                                               select new { tb68.NO_COOR_CUR, tb68.CO_COOR_CUR }).OrderBy(c => c.NO_COOR_CUR);

            ddlCoordenacaoSerCur.DataTextField = "NO_COOR_CUR";
            ddlCoordenacaoSerCur.DataValueField = "CO_COOR_CUR";
            ddlCoordenacaoSerCur.DataBind();

            ddlCoordenacaoSerCur.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Coordenadores
        /// </summary>
        private void CarregaCoordenadores()
        {
            ddlCoordenadorSerCur.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                               select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(c => c.NO_COL);

            ddlCoordenadorSerCur.DataTextField = "NO_COL";
            ddlCoordenadorSerCur.DataValueField = "CO_COL";
            ddlCoordenadorSerCur.DataBind();

            ddlCoordenadorSerCur.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Coordenadores
        /// </summary>
        private void CarregaReferenciaSerieRelatorio()
        {
            string clasSerCur = ddlClassificacaoSerCur.SelectedValue.Trim();

            ddlReferSerieRel.Items.Clear();

            if (clasSerCur == "C") //Creche
            {
                ddlReferSerieRel.Items.Add(new ListItem("1C", "1C"));
                ddlReferSerieRel.Items.Add(new ListItem("2C", "2C"));
            }
            else if (clasSerCur == "E") //EJA
            {
                ddlReferSerieRel.Items.Add(new ListItem("1E", "1E"));
                ddlReferSerieRel.Items.Add(new ListItem("2E", "2E"));
                ddlReferSerieRel.Items.Add(new ListItem("3E", "3E"));
                ddlReferSerieRel.Items.Add(new ListItem("4E", "4E"));
            }
            else if (clasSerCur == "F") //Ensino Fundamental
            {
                ddlReferSerieRel.Items.Add(new ListItem("1F", "1F"));
                ddlReferSerieRel.Items.Add(new ListItem("2F", "2F"));
                ddlReferSerieRel.Items.Add(new ListItem("3F", "3F"));
                ddlReferSerieRel.Items.Add(new ListItem("4F", "4F"));
                ddlReferSerieRel.Items.Add(new ListItem("5F", "5F"));
                ddlReferSerieRel.Items.Add(new ListItem("6F", "6F"));
                ddlReferSerieRel.Items.Add(new ListItem("7F", "7F"));
                ddlReferSerieRel.Items.Add(new ListItem("8F", "8F"));
                ddlReferSerieRel.Items.Add(new ListItem("9F", "9F"));
            }
            else if (clasSerCur == "I") //Infantil
            {
                ddlReferSerieRel.Items.Add(new ListItem("1I", "1I"));
                ddlReferSerieRel.Items.Add(new ListItem("2I", "2I"));
                ddlReferSerieRel.Items.Add(new ListItem("3I", "3I"));
                ddlReferSerieRel.Items.Add(new ListItem("4I", "4I"));
                ddlReferSerieRel.Items.Add(new ListItem("5I", "5I"));
            }
            else if (clasSerCur == "M") //Ensino Médio
            {
                ddlReferSerieRel.Items.Add(new ListItem("1M", "1M"));
                ddlReferSerieRel.Items.Add(new ListItem("2M", "2M"));
                ddlReferSerieRel.Items.Add(new ListItem("3M", "3M"));
            }
            else if (clasSerCur == "O") //Outros
            {
                ddlReferSerieRel.Items.Add(new ListItem("1O", "1O"));
                ddlReferSerieRel.Items.Add(new ListItem("2O", "2O"));
                ddlReferSerieRel.Items.Add(new ListItem("3O", "3O"));
                ddlReferSerieRel.Items.Add(new ListItem("4O", "4O"));
                ddlReferSerieRel.Items.Add(new ListItem("5O", "5O"));
                ddlReferSerieRel.Items.Add(new ListItem("6O", "6O"));
                ddlReferSerieRel.Items.Add(new ListItem("7O", "7O"));
                ddlReferSerieRel.Items.Add(new ListItem("8O", "8O"));
                ddlReferSerieRel.Items.Add(new ListItem("9O", "9O"));
            }
            else if (clasSerCur == "U") //Unificado
            {
                ddlReferSerieRel.Items.Add(new ListItem("1U", "1U"));
                ddlReferSerieRel.Items.Add(new ListItem("2U", "2U"));
                ddlReferSerieRel.Items.Add(new ListItem("3U", "3U"));
            }

            ddlReferSerieRel.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region Eventos de componentes

        protected void ddlModalidaAnterior_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaSerieAnterior();
        }

        protected void chkVista_ChekedChanged(object sender, EventArgs e)
        {
            if (chkVista.Checked == true)
            {
                txtVlVistInt.Enabled = true;
                txtVlVistMan.Enabled = true;
                txtVlVistNoi.Enabled = true;
                txtVlVistTar.Enabled = true;
                txtVlVistEsp.Enabled = true;
            }
            else
            {
                txtVlVistInt.Enabled = false;
                txtVlVistMan.Enabled = false;
                txtVlVistNoi.Enabled = false;
                txtVlVistTar.Enabled = false;
                txtVlVistEsp.Enabled = false;
            }
        }

        protected void chkPrazo_ChekedChanged(object sender, EventArgs e)
        {
            if (chkPrazo.Checked == true)
            {
                txtVlPraInt.Enabled = true;
                txtVlPraMan.Enabled = true;
                txtVlPraTar.Enabled = true;
                txtVlPraNoi.Enabled = true;
                txtVlPraEsp.Enabled = true;
            }
            else
            {
                txtVlPraInt.Enabled = false;
                txtVlPraMan.Enabled = false;
                txtVlPraTar.Enabled = false;
                txtVlPraNoi.Enabled = false;
                txtVlPraEsp.Enabled = false;
            }
        }

        protected void chkTaxaMatr_ChekedChanged(object sender, EventArgs e)
        {
            if (chkTaxaMatr.Checked == true)
            {
                txtTxMatInt.Enabled = true;
                txtTxMatMan.Enabled = true;
                txtTxMatNoi.Enabled = true;
                txtTxMatTar.Enabled = true;
                txtTxMatEsp.Enabled = true;
            }
            else
            {
                txtTxMatInt.Enabled = false;
                txtTxMatMan.Enabled = false;
                txtTxMatNoi.Enabled = false;
                txtTxMatTar.Enabled = false;
                txtTxMatEsp.Enabled = false;
            }
        }

        protected void chkMedPFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedPFinal.Checked)
            {
                txtNotaFinalAprovSerCur.Enabled = true;
            }
            else
            {
                txtNotaFinalAprovSerCur.Enabled = false;
            }
        }

        protected void chkRecu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecu.Checked)
            {
                txtQtdMatRecu.Enabled = true;
                txtVLMedRecu.Enabled = true;
                ddlConcMedRecu.Enabled = true;
            }
            else
            {
                txtQtdMatRecu.Enabled = false;
                txtVLMedRecu.Enabled = false;
                ddlConcMedRecu.Enabled = false;
            }
        }

        protected void chkDepe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDepe.Checked)
            {
                txtQtdMatDepe.Enabled = true;
                txtVLMedDepe.Enabled = true;
                ddlConcMedDepe.Enabled = true;
            }
            else
            {
                txtQtdMatDepe.Enabled = false;
                txtVLMedDepe.Enabled = false;
                ddlConcMedDepe.Enabled = false;
            }
        }

        protected void chkCons_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCons.Checked)
            {
                txtQtdMatCons.Enabled = true;
                txtVLMedCons.Enabled = true;
                ddlConcMedCons.Enabled = true;
            }
            else
            {
                txtQtdMatCons.Enabled = false;
                txtVLMedCons.Enabled = false;
                ddlConcMedCons.Enabled = false;
            }
        }

        protected void chkVlVistMatrP_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtVlVistManP.Enabled = true;
                txtVlVistTarP.Enabled = true;
                txtVlVistNoiP.Enabled = true;
                txtVlVistIntP.Enabled = true;
                txtVlVistEspP.Enabled = true;
            }
            else
            {
                txtVlVistManP.Enabled = false;
                txtVlVistTarP.Enabled = false;
                txtVlVistNoiP.Enabled = false;
                txtVlVistIntP.Enabled = false;
                txtVlVistEspP.Enabled = false;

            }
        }

        protected void chkVlPrazMatrP_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtVlPrazManP.Enabled = true;
                txtVlPrazTarP.Enabled = true;
                txtVlPrazNoiP.Enabled = true;
                txtVlPrazIntP.Enabled = true;
                txtVlPrazEspP.Enabled = true;
            }
            else
            {
                txtVlPrazManP.Enabled = false;
                txtVlPrazTarP.Enabled = false;
                txtVlPrazNoiP.Enabled = false;
                txtVlPrazIntP.Enabled = false;
                txtVlPrazEspP.Enabled = false;

            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCoordenacoes();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                ddlProximaModalidade.SelectedValue = ((DropDownList)sender).SelectedValue;
            }
        }

        protected void ddlClassificacaoSerCur_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaReferenciaSerieRelatorio();
        }

        protected void ddlTpLanctNota_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTpLanctNota.SelectedValue)
            {
                case "N":
                    txtVLMedRecu.Text = ""; // ZERA O VALOR DO CAMPO DE NOTA DA RECUPERACAO
                    txtVLMedRecu.Visible = true; // MOSTRA O CAMPO DE NOTA DA RECUPERACAO
                    ddlConcMedRecu.SelectedValue = "0"; // ZERA O CAMPO DE CONCEITO DA RECUPERACAO
                    ddlConcMedRecu.Visible = false; // ESCONDE O CAMPO DE CONCEITO DA RECUPERACAO

                    txtVLMedDepe.Text = ""; // ZERA O VALOR DO CAMPO DE NOTA DA DEPENDENCIA
                    txtVLMedDepe.Visible = true; // MOSTRA O CAMPO DE NOTA DA DEPENDENCIA
                    ddlConcMedDepe.SelectedValue = "0"; // ZERA O VALOR DO CAMPO DE CONCEITO DA DEPENDENCIA
                    ddlConcMedDepe.Visible = false; // ESCONDE O CAMPO DE CONCEITO DA DEPENDENCIA

                    txtVLMedCons.Text = ""; // ZERA O VALOR DO CAMPO DE NOTA DO CONSELHO DE CLASSE
                    txtVLMedCons.Visible = true; // MOSTRA O CAMPO DE NOTA DO CONSELHO DE CLASSE
                    ddlConcMedCons.SelectedValue = "0"; // ZERA O CAMPO DE CONCEITO DO CONSELHO DE CLASSE
                    ddlConcMedCons.Visible = false; // ESCONDE O CAMPO DE CONCEITO DO CONSELHO DE CLASSE

                    txtMediaFinalAprovSerCur.Text = ""; // ZERA O VALOR DO CAMPO DE MEDIA DE APROVACAO
                    txtMediaFinalAprovSerCur.Visible = true; // MOSTRA O CAMPO DE MEDIA DE APROVACAO
                    ddlMediaConcAprovCur.SelectedValue = ""; // ZERA O VALOR DO CAMPO DE CONCEITO DA MEDIA DE APROVACAO
                    ddlMediaConcAprovCur.Visible = false; // ESCONDE O CAMPO DE CONCEITO DA MEDIA DE APROVACAO

                    txtNotaFinalAprovSerCur.Text = ""; // ZERA O VALOR DO CAMPO DE MEDIA DE PROVA FINAL
                    txtNotaFinalAprovSerCur.Visible = true; // MOSTRA O CAMPO DE MEDIA DE PROVA FINAL
                    ddlMediaConcProvaFinal.SelectedValue = ""; // ZERA O VALOR DO CAMPO DE MEDIA CONCEITUAL DA PROVA FINAL
                    ddlMediaConcProvaFinal.Visible = false;// ESCONDE O CAMPO DE MEDIA CONCEITUAL DA PROVA FINAL
                    break;
                case "C":
                    txtVLMedRecu.Text = ""; // ZERA O CAMPO DE NOTA DA RECUPERACAO
                    txtVLMedRecu.Visible = false; // ESCONDE O CAMPO DE NOTA DA RECUPERACAO
                    ddlConcMedRecu.SelectedValue = "0"; // ZERA O CAMPO DE CONCEITO DA RECUPERACAO
                    ddlConcMedRecu.Visible = true; // MOSTRA O CAMPO DE CONCEITO DA RECUPERACAO

                    txtVLMedDepe.Text = ""; // ZERA O CAMPO DE NOTA DA DEPENDENCIA
                    txtVLMedDepe.Visible = false; // ESCONDE O CAMPO DE NOTA DA DEPENDENCIA
                    ddlConcMedDepe.SelectedValue = "0"; // ZERA O CAMPO DE CONCEITO DA DEPENDENCIA
                    ddlConcMedDepe.Visible = true; // MOSTRA O CAMPO DE CONCEITO DA DEPENDENCIA

                    txtVLMedCons.Text = ""; // ZERA O CAMPO DE NOTA DO CONSELHO DE CLASSE
                    txtVLMedCons.Visible = false; // ESCONDE O CAMPO DE NOTA DO CONSELHO DE CLASSE
                    ddlConcMedCons.SelectedValue = "0"; // ZERA O CAMPO DE CONCEITO DO CONSELHO DE CLASSE
                    ddlConcMedCons.Visible = true; // MOSTRA O CAMPO DE CONCEITO DO CONSELHO DE CLASSE

                    txtMediaFinalAprovSerCur.Text = ""; // ZERA O VALOR DO CAMPO DE MEDIA DE APROVACAO
                    txtMediaFinalAprovSerCur.Visible = false; // ESCONDE O CAMPO DE MEDIA DE APROVACAO
                    ddlMediaConcAprovCur.SelectedValue = ""; // ZERA O VALOR DO CAMPO DE CONCEITO DA MEDIA DE APROVACAO
                    ddlMediaConcAprovCur.Visible = true; // MOSTRA O CAMPO DE CONCEITO DA MEDIA DE APROVACAO

                    txtNotaFinalAprovSerCur.Text = ""; // ZERA O VALOR DO CAMPO DE MEDIA DE PROVA FINAL
                    txtNotaFinalAprovSerCur.Visible = false; // ESCONDE O CAMPO DE MEDIA DE PROVA FINAL
                    ddlMediaConcProvaFinal.SelectedValue = ""; // ZERA O VALOR DO CAMPO DE MEDIA CONCEITUAL DA PROVA FINAL
                    ddlMediaConcProvaFinal.Visible = true;// MOSTRA O CAMPO DE MEDIA CONCEITUAL DA PROVA FINAL
                    break;
            }
        }

        protected void ddlTpBol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletos();
        }

        protected void ddlTpBolPreMatr_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletosPre();
        }

        #region a
        protected void txtSiglaEPSerCur_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtInformacaoLegalSerCur_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtdpi_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        protected void ddlControleFrequenciaSerCur_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean resultado = ((DropDownList)sender).SelectedValue == "M";
            if (resultado)
                ddlRegistroFreqSerCur.SelectedValue = "M";
            ddlRegistroFreqSerCur.Enabled = !resultado;
        }

        protected void chkTaxaMatrP_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtTxMatIntP.Enabled = true;
                txtTxMatManP.Enabled = true;
                txtTxMatNoiP.Enabled = true;
                txtTxMatTarP.Enabled = true;
                txtTxMatEspP.Enabled = true;
            }
            else
            {
                txtTxMatIntP.Enabled = false;
                txtTxMatManP.Enabled = false;
                txtTxMatNoiP.Enabled = false;
                txtTxMatTarP.Enabled = false;
                txtTxMatEspP.Enabled = false;
            }
        }

        protected void ddlProximaModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
            {
                CarregaSeries(int.Parse(((DropDownList)sender).SelectedValue));
            }
        }
        #endregion

    }
}