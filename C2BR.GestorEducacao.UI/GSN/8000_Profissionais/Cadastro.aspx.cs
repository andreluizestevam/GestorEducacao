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


namespace C2BR.GestorEducacao.UI.GSN._8000_Profissionais
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
                ManterEnderecoSN.Manutencao = EnumAuxili.TipoManutencao.Insercao;
                ManterEnderecoSN.IdFk = 0;
                ManterEnderecoSN.FkEndereco = EnumAuxili.FkEnderecoSN.Profissional;
                ManterEnderecoSN.IdEndereco = 0;
            }
            if (Page.IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                txtNome.Enabled = true;
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TSN013_PROFISSIONAIS TSN013;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                TSN013 = new TSN013_PROFISSIONAIS();
                TSN013.NM_PROFISSIONAL = txtNome.Text;
                TSN013.CO_APELIDO = txtApelido.Text;
                TSN013.DT_NASCIMENTO = Convert.ToDateTime(txtDataNascimento.Text);
                TSN013.CO_SEXO = ddlSexo.SelectedValue;
                TSN013.CO_CPF = txtCPF.Text;
                TSN013.CO_RG_NUMERO = txtRG.Text;
                TSN013.CO_RG_EMISSOR = txtRGEmissor.Text;
                TSN013.TSN010_UF.DE_UF = txtRGUF.Text;
                TSN013.DE_EMAIL = txtEmail.Text;
                TSN013.DE_TELEFONE_CELULAR = txtCelular.Text;
                TSN013.DE_TELEFONE_FIXO = txtTelFixo.Text;
                TSN013.CO_CLASSE_ORGAO = txtUFConselho.Text;
                TSN013.CO_CLASSE_NUMERO = Convert.ToInt32(txtCarteirinha.Text);
                TSN013.TSN010_UF.DE_SIGLA = txtUFConselho.Text;

            }
            else
                TSN013 = RetornaEntidade();

            TSN013.NM_PROFISSIONAL = txtNome.Text;

            CurrentPadraoCadastros.CurrentEntity = TSN013;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TSN013_PROFISSIONAIS TSN013 = RetornaEntidade();

            if (TSN013 != null)
            {

                txtNome.Text = TSN013.NM_PROFISSIONAL;
                txtApelido.Text = TSN013.CO_APELIDO;
                txtDataNascimento.Text = Convert.ToString(TSN013.DT_NASCIMENTO);
                ddlSexo.SelectedValue = TSN013.CO_SEXO;
                txtCPF.Text = TSN013.CO_CPF;
                txtRG.Text = TSN013.CO_RG_NUMERO;
                txtRGEmissor.Text =TSN013.CO_RG_EMISSOR;
                //txtRGUF.Text =TSN013.TSN010_UF.DE_UF;
                txtEmail.Text = TSN013.DE_EMAIL;
                txtCelular.Text =TSN013.DE_TELEFONE_CELULAR;
                txtTelFixo.Text =TSN013.DE_TELEFONE_FIXO;
                txtUFConselho.Text = TSN013.CO_CLASSE_ORGAO;
                txtCarteirinha.Text = Convert.ToString(TSN013.CO_CLASSE_NUMERO);
                //txtUFConselho.Text = TSN013.TSN010_UF.DE_SIGLA;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TSN013_PROFISSIONAIS</returns>
        private TSN013_PROFISSIONAIS RetornaEntidade()
        {

            TSN013_PROFISSIONAIS TSN013 = TSN013_PROFISSIONAIS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (TSN013 == null) ? new TSN013_PROFISSIONAIS() : TSN013;
        }
        #endregion
    }
}