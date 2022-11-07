//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: ANALISE SOLICITAÇÃO DE SERVIÇOS.
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.AnaliseSolicitacaoServicos
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione uma solicitação.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

            if (IsPostBack) return;
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaBusca();

            TB65_HIST_SOLICIT tb65 = RetornaEntidade();

            tb65.TB25_EMPRESAReference.Load();
            tb65.TB64_SOLIC_ATENDReference.Load();

            tb65.DE_LOCALI_SOLI = txtLocal.Text;
            tb65.DE_MOTI_DEFE_SOLI = txtMotivo.Text;

            bool flagFinalizou = false;

            if (tb65.DT_FIM_SOLI == null && ddlFinalizar.SelectedValue == "S")
            {
                if (txtDataFinalizacao.Text == "") 
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de finalização deve ser informada");
                    return;
                }

                tb65.DT_FIM_SOLI = DateTime.Parse(txtDataFinalizacao.Text);
                tb65.CO_SITU_SOLI = SituacaoItemSolicitacao.F.ToString();
                flagFinalizou = true;
            }

            tb65.TB64_SOLIC_ATEND.localizacao = txtLocal.Text;
            
//--------> Quando é um novo registro, primeiro salva a solicitação e posteriormente os tipos de solicitação selecionados
            if (GestorEntities.SaveOrUpdate(tb65) <= 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item.");
                return;
            }

//--------> Verifica se a solicitação foi finalizada e a unidade executora é a mesma que a unidade de entrega
            if (flagFinalizou && tb65.TB25_EMPRESA.CO_EMP == tb65.TB64_SOLIC_ATEND.CO_EMP_ALU)
            {                
//------------> Faz o envio de SMS se a mesma estiver selecionada
                if (tb65.TB64_SOLIC_ATEND.CO_FLA_SMS_SOLIC_ATEND == "S")
                {
                    string nomeServico = tb65.TB66_TIPO_SOLIC.NO_TIPO_SOLI;
                    nomeServico = nomeServico.Length > 60 ? nomeServico.Substring(0, 60) : nomeServico;

                    string strTelUnidade = tb65.TB25_EMPRESA.CO_TEL1_EMP;
                    string strSiglaUnidade = tb65.TB25_EMPRESA.sigla;
                    string strTelUsuario = !string.IsNullOrEmpty(tb65.TB64_SOLIC_ATEND.NU_TELE_RESP_SOLIC_ATEND) ?
                        tb65.TB64_SOLIC_ATEND.NU_TELE_RESP_SOLIC_ATEND : tb65.TB64_SOLIC_ATEND.CO_TELE_CONT;

                    if (!string.IsNullOrEmpty(strTelUsuario))
                    {
                        SMSAuxili.EnvioSMS(strSiglaUnidade, "(Portal Educacao) Solicitacao " + nomeServico.RemoveAcentuacoes() 
                            + " finalizada e disponivel. Tel: " + strTelUnidade, "55" + strTelUsuario, DateTime.Now.Ticks.ToString());
                    }
                }
            }

            tb65.CO_DEFE_SOLI = ddlPendencia.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb65;
        }        

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB65_HIST_SOLICIT tb65 = RetornaEntidade();

            if (tb65 != null)
            {
                tb65.TB64_SOLIC_ATENDReference.Load();
                tb65.TB66_TIPO_SOLICReference.Load();
                tb65.TB89_UNIDADESReference.Load();

                txtAluno.Text = TB07_ALUNO.RetornaPeloCoAlu(tb65.CO_ALU).NO_ALU;
                txtNumeroSolicitacao.Text = tb65.TB64_SOLIC_ATEND.NU_DCTO_SOLIC;
                txtItemSolicitacao.Text = tb65.TB66_TIPO_SOLIC.NO_TIPO_SOLI;
                ddlPendencia.SelectedValue = tb65.CO_DEFE_SOLI != null ? tb65.CO_DEFE_SOLI : "N";
                txtMotivo.Enabled = ddlPendencia.SelectedValue == "S";
                txtMotivo.Text = tb65.DE_MOTI_DEFE_SOLI;
                txtLocal.Text = tb65.TB64_SOLIC_ATEND.localizacao;
                ddlFinalizar.SelectedValue = "N";
                ddlFinalizar.Enabled = ddlPendencia.SelectedValue == "N";
                txtDataFinalizacao.Text = tb65.DT_FIM_SOLI != null ? tb65.DT_FIM_SOLI.Value.ToString("dd/MM/yyyy") : "";
                txtValorUnit.Text = tb65.VA_SOLI_ATEN != null ? tb65.VA_SOLI_ATEN.Value.ToString("#,##0.00") : "";
                txtUnidadeItem.Text = tb65.TB89_UNIDADES != null ? tb65.TB89_UNIDADES.NO_UNID_ITEM : "";
                txtQtdeItem.Text = tb65.QT_ITENS_SOLI_ATEN != null ? tb65.QT_ITENS_SOLI_ATEN.ToString() : "";
                txtValorTotal.Text = tb65.QT_ITENS_SOLI_ATEN != null && tb65.VA_SOLI_ATEN != null ? (tb65.QT_ITENS_SOLI_ATEN.Value * tb65.VA_SOLI_ATEN.Value).ToString("#,##0.00") : "";

                var tb08 = (from iTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                            join tb01 in TB01_CURSO.RetornaTodosRegistros() on iTb08.CO_CUR equals tb01.CO_CUR
                            join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on iTb08.CO_TUR equals tb129.CO_TUR
                            where iTb08.CO_ALU == tb65.CO_ALU && iTb08.CO_CUR == tb65.CO_CUR
                            && iTb08.CO_EMP == tb65.CO_EMP && iTb08.TB44_MODULO.CO_MODU_CUR == tb65.TB64_SOLIC_ATEND.CO_MODU_CUR
                            select new { iTb08.TB44_MODULO.DE_MODU_CUR, tb01.NO_CUR, tb129.CO_SIGLA_TURMA }).FirstOrDefault();

                if (tb08 != null)
                {
                    txtModalidade.Text = tb08.DE_MODU_CUR;
                    txtSerie.Text = tb08.NO_CUR;
                    txtTurma.Text = tb08.CO_SIGLA_TURMA;
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB65_HIST_SOLICIT</returns>
        private TB65_HIST_SOLICIT RetornaEntidade()
        {
            TB65_HIST_SOLICIT tb65 =
                TB65_HIST_SOLICIT.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("tip"));
            return (tb65 == null) ? new TB65_HIST_SOLICIT() : tb65;
        }
        #endregion

        protected void ddlPendencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataFinalizacao.Text = "";

            if (ddlPendencia.SelectedValue == "S")
            {
                txtMotivo.Enabled = true;
                ddlFinalizar.Enabled = false;
                ddlFinalizar.SelectedIndex = 0;
            }
            else
            {
                txtMotivo.Enabled = false;
                ddlFinalizar.Enabled = true;
            }
        }

        protected void ddlFinalizar_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataFinalizacao.Text = ddlFinalizar.SelectedValue == "S" ? DateTime.Now.ToString("dd/MM/yyyy") : "";            
        }
    }
}
