//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//----------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//----------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR          | DESCRIÇÃO RESUMIDA
// -----------+-------------------------------+-------------------------------------
// 05/07/2016 | Tayguara Acioli  TA.05/07/2016| Adicionei a pop up de registro de ocorrências.

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.DataClasses;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.App_Masters
{
    public partial class PadraoCadastros : System.Web.UI.MasterPage
    {
        #region Propriedades

        public BarraCadastro BarraCadastro { get { return BarraCadastroRef; } }
        public EntityObject CurrentEntity { get; set; }
        public EntityObject SecondaryEntity { get; set; }
        public string OperacaoCorrenteQueryString { get { return QueryStringAuxili.OperacaoCorrenteQueryString; } }
        RegistroLog registroLog = new RegistroLog();
        #endregion

        #region Eventos (Declaração)

        public delegate void OnAcaoBarraCadastroHandler();
        public event OnAcaoBarraCadastroHandler OnAcaoBarraCadastro;

        public delegate void OnCarregaFormularioHandler();
        public event OnCarregaFormularioHandler OnCarregaFormulario;
        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BarraCadastroRef.OnAction += new BarraCadastro.OnActionHandler(ActionsBarRef_OnAction);
            this.OnCarregaFormulario += new OnCarregaFormularioHandler(PadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemEdicao);

            if (!IsPostBack)
            {                
                if (OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                    DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemEdicao);
                else 
                if (OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    OnCarregaFormulario();
                    DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, MensagensAjuda.MessagemDetalhe);
                }

//------------> Faz o registro na tabela de log de acordo com a ação executada
                registroLog.RegistroLOG(null, RegistroLog.NENHUMA_ACAO);

                CarregarTiposOcorrencia();
            }
        }

        void PadraoCadastros_OnCarregaFormulario() { BarraCadastroRef.Entity = CurrentEntity; }

        void ActionsBarRef_OnAction()
        {
            if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            if (OnAcaoBarraCadastro != null)
                OnAcaoBarraCadastro();

            BarraCadastroRef.Entity = CurrentEntity;
        }   
        #endregion

        #region Métodos

        /// <summary>
        /// Define a mensagem que aparecerá na parte superior da tela de cadastro.
        /// </summary>
        /// <param name="strMsgObrigatoria">Mensagem de campo obrigatório</param>
        /// <param name="strMsgGenerica">Mensagem genérica</param>
        public void DefineMensagem(string strMsgObrigatoria, string strMsgGenerica)
        {
            lblMensagCampoObrig.Text = strMsgObrigatoria;
            lblMensagGenerica.Text = strMsgGenerica;
        }

        private void CarregarTiposOcorrencia()
        {
            AuxiliCarregamentos.CarregaTiposOcorrencia(ddlTipoOcorr, false);
        }

        //private void AbreModalOcorrencia()
        //{
        //    ScriptManager.RegisterStartupScript(
        //        this.Page,
        //        this.GetType(),
        //        "Acao",
        //        "AbreModalOcorrencia();",
        //        true
        //    );
        //}

        protected void drpPacOcorr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int pacienteSelecionado = drpPacOcorr.SelectedValue != "" && drpPacOcorr.SelectedValue != null ? int.Parse(drpPacOcorr.SelectedValue) : 0;
            
            carregaGridOcorrenciaPaciente(pacienteSelecionado);
            
            //AbreModalOcorrencia();
        }

        private void carregaGridOcorrenciaPaciente(int co_alu)
        {

            var res = (from tbs408 in TBS408_OCORR_PACIE.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs408.CO_COL_CADAS equals tb03.CO_COL
                       where tbs408.CO_ALU == co_alu
                       select new HistoricoOcorrencias 
                       { 
                        DataOcorrencia = tbs408.DT_OCORR,
                        TipoOcorrencia = tbs408.TP_OCORR,
                        Ocorrencia = tbs408.DE_OCORR,
                        Responsavel = tb03.NO_APEL_COL
                       }).ToList();

            grdHistorOcorrPaciente.DataSource = res;
            grdHistorOcorrPaciente.DataBind();

        }

        protected void imgbRegisOcorr_OnClick(object sender, EventArgs e)
        {
            //AbreModalOcorrencia();
        }

        protected void imgbPesqPacOcorr_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtPacOcorr.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                drpPacOcorr.DataTextField = "NO_ALU";
                drpPacOcorr.DataValueField = "CO_ALU";
                drpPacOcorr.DataSource = res;
                drpPacOcorr.DataBind();
            }

            drpPacOcorr.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisaOcorr(true);
        }

        protected void imgbVoltarPesqOcorr_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaOcorr(false);
            carregaGridOcorrenciaPaciente(0);
        }

        private void OcultarPesquisaOcorr(bool ocultar)
        {
            txtPacOcorr.Visible =
            imgbPesqPacOcorr.Visible = !ocultar;
            drpPacOcorr.Visible =
            imgbVoltarPesqOcorr.Visible = ocultar;

            //AbreModalOcorrencia();
        }

        protected void lnkbRegistOcorr_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(drpPacOcorr.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Por favor selecione um paciente!");
                //AbreModalOcorrencia();
                return;
            }

            if (string.IsNullOrEmpty(ddlTipoOcorr.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Por favor selecione o tipo da ocorrência!");
                //AbreModalOcorrencia();
                return;
            }

            if (string.IsNullOrEmpty(txtTituloOcorr.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Por favor informe o titulo da ocorrência!");
                //AbreModalOcorrencia();
                return;
            }

            TBS408_OCORR_PACIE tbs408 = new TBS408_OCORR_PACIE();

            tbs408.CO_ALU = int.Parse(drpPacOcorr.SelectedValue);
            tbs408.CO_EMP_OCORR = LoginAuxili.CO_EMP;

            //Dados de cadastro
            tbs408.DT_CADAS = DateTime.Now;
            tbs408.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs408.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
            tbs408.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs408.IP_CADAS = Request.UserHostAddress;

            tbs408.DT_OCORR = DateTime.Now;
            tbs408.TP_OCORR = ddlTipoOcorr.SelectedValue;
            tbs408.NO_OCORR = txtTituloOcorr.Text.Trim();
            tbs408.DE_OCORR = txtOcorrencia.Text;
            tbs408.DE_ACAO_OCORR = txtAcaoOcorr.Text.Trim();
            tbs408.CO_SITU = "A";

            TBS408_OCORR_PACIE.SaveOrUpdate(tbs408, true);

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Ocorrência registrada com sucesso!");

            limpaCamposOcorrencia();
            imgbVoltarPesqOcorr_OnClick(sender, e);
        }

        protected void limpaCamposOcorrencia()
        {
            ddlTipoOcorr.SelectedIndex = 0;
            txtOcorrencia.Text =
            txtAcaoOcorr.Text =
            txtTituloOcorr.Text = "";
        }

        
        #endregion

        #region Classes

        public class HistoricoOcorrencias 
        { 
            public DateTime DataOcorrencia { get; set; }
            public string TipoOcorrencia { get; set; }
            public string Tipo {                 
                get
                {
                    return AuxiliFormatoExibicao.RetornarTipoOcorrencia(TipoOcorrencia);
                }}
            public string Ocorrencia { get; set; }
            public string Responsavel { get; set; }
        }

        #endregion
    }
}
