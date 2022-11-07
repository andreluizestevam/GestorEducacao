using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSN._9000_Banner
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                txtNomeBanner.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN002_BANNER TSN002;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN002 = new TSN002_BANNER();
                TSN002.NO_BANNER = txtNomeBanner.Text;
                TSN002.DE_BANNER = txtDescricaoBanner.Text;
                TSN002.TP_BANNER = ddlTipoBanner.Text;
                TSN002.DE_BANNER_URL = txtURLBanner.Text;
                TSN002.FL_PUBLI_CADAS = ddlPubliCadas.SelectedValue;
                TSN002.FL_PUBLIC_MENU = ddlPubliMenu.SelectedValue;
                TSN002.FL_PUBLIC_PRONT = ddlPubliPront.SelectedValue;
                TSN002.FL_PUBLIC_AGEND = ddlPubliAgend.SelectedValue;
                TSN002.FL_PUBLIC_CONSU = ddlPubliConsu.SelectedValue;
                TSN002.FL_PUBLIC_EXAME = ddlPubliExame.SelectedValue;
                TSN002.FL_PUBLIC_SERVI = ddlPubliServi.SelectedValue;
                TSN002.FL_PUBLIC_ESTAB = ddlPubliEstab.SelectedValue;
                TSN002.FL_PUBLIC_DICAS = ddlPubliDicas.SelectedValue;
                TSN002.FL_PUBLIC_MEDIC = ddlPubliMedic.SelectedValue;
                TSN002.DT_PUBLC_INICIO = Convert.ToDateTime(txtDataPublicIni.Text);
                TSN002.DT_PUBLIC_FIM = Convert.ToDateTime(txtDataPublicFim.Text);
                TSN002.QT_PUBLIC_DIA = Convert.ToInt32(txtQtPublicDia.Text);
                TSN002.QT_VISUAL_BANNER = Convert.ToInt32(txtQtVisualBanner.Text);
                TSN002.QT_USUAR_BANNER = Convert.ToInt32(txtQtUsuarBanner.Text);
                TSN002.VL_CONTR_PUBLIC = Convert.ToDecimal(txtVlContrPublic.Text);
                TSN002.DT_CONTR_PUBLIC = DateTime.Now;
                TSN002.NR_CONTR_PUBLIC = Convert.ToInt32(txtNrContrPublic.Text);

            }
            else
                TSN002 = RetornaEntidade();

            TSN002.DE_ARTIGO = txtDescricao.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN002;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN002_BANNER TSN002 = RetornaEntidade();

            if (TSN002 != null)
            {
                txtDescricao.Text = TSN002.DE_ARTIGO;
                txtLink.Text = TSN002.DE_LINK;
                txtImagem.Text = TSN002.IM_ARTIGO_URL;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN001_ARTIGOS</returns>
        private TSN002_BANNER RetornaEntidade()
        {

            TSN002_BANNER TSN002 = TSN002_BANNER.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN002 == null) ? new TSN002_BANNER() : TSN002;
        }
        #endregion
    }
}