//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 10/12/14 | Maxwell Almeida            | Criação da funcionalidade para Cadastro de Operadoras


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1111_UsuarioApp
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos
        string salvaDTSitu;
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
                //Libera o botão de resetar apenas se for uma alteração de registro
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    ddlSitu.SelectedValue = "A";
                    liReset.Visible = true;
                }

                ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Tipo");
                ddlTipo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlUsuario.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Usuário ");
                ddlUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtLogin.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Login");
                txtLogin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlSitu.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Situação");
                ddlSitu.Focus();
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) && string.IsNullOrEmpty(txtSenha.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Na inclusão de um novo registro é preciso informar a Senha");
                txtSenha.Focus();
                return;
            }

            #region Verifica se Login existe

            //Não executa este bloco se for uma exclusão
            //if (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            //{
            //    if (TBS384_USUAR_APP.RetornaTodosRegistros().Where(w => w.DE_LOGIN.Equals(txtLogin.Text.Trim())).Any())
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, string.Format("O usuário informado '{0}' já está em uso em outra conta nesta instituição!", txtLogin.Text));
            //        txtLogin.Focus();
            //        return;
            //    }
            //}

            #endregion

            TBS384_USUAR_APP tbs384 = RetornaEntidade();

            tbs384.CO_TIPO = ddlTipo.SelectedValue;
            tbs384.DE_LOGIN = txtLogin.Text;
            tbs384.DE_SENHA = LoginAuxili.GerarMD5(txtSenha.Text);
            tbs384.NM_USUAR = ddlUsuario.SelectedItem.Text;
            tbs384.NM_APELI_USUAR = (ddlUsuario.SelectedItem.Text.Length > 15 ? ddlUsuario.SelectedItem.Text.Substring(0, 15) : ddlUsuario.SelectedItem.Text);

            #region Grava o id do usuário

            switch (this.ddlTipo.SelectedValue)
            {
                case "G":
                case "C":
                    tbs384.CO_COL = int.Parse(ddlUsuario.SelectedValue);
                    break;
                case "R":
                    tbs384.CO_RESP = int.Parse(ddlUsuario.SelectedValue);
                    break;
                case "P":
                    tbs384.CO_ALU = int.Parse(ddlUsuario.SelectedValue);
                    break;
                default:
                    break;
            }

            #endregion

            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSitu.SelectedValue)
            {
                tbs384.CO_SITUA = ddlSitu.SelectedValue;
                tbs384.DT_SITUA = DateTime.Now;
                tbs384.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs384.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                tbs384.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs384.IP_SITUA = Request.UserHostAddress;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tbs384.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs384.DT_CADAS = DateTime.Now;
                    tbs384.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs384.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs384.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs384.IP_CADAS = Request.UserHostAddress;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs384;
        }
        #endregion

        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            try
            {
                TBS384_USUAR_APP tbs384 = RetornaEntidade();

                if (tbs384 != null)
                {
                    CarregaUsuarios(tbs384.CO_TIPO);

                    ddlTipo.SelectedValue = tbs384.CO_TIPO;
                    //Coleta o código da coluna correspondente com o tipo do usuário
                    ddlUsuario.SelectedValue = (tbs384.CO_TIPO == "G" || tbs384.CO_TIPO == "C" ? tbs384.CO_COL.ToString() : (tbs384.CO_TIPO == "P" ? tbs384.CO_ALU.ToString() : tbs384.CO_RESP.ToString()));
                    txtLogin.Text = tbs384.DE_LOGIN;
                    txtSenha.Enabled = false;
                    ddlSitu.SelectedValue = hidSituacao.Value = tbs384.CO_SITUA;
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao caregar  formulário" + " - " + ex.Message);
                return;
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS384_USUAR_APP</returns>
        private TBS384_USUAR_APP RetornaEntidade()
        {
            TBS384_USUAR_APP tbs384 = TBS384_USUAR_APP.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs384 == null) ? new TBS384_USUAR_APP() : tbs384;
        }

        /// <summary>
        /// Carrega os usuários dinamicamente
        /// </summary>
        /// <param name="co_tipo"></param>
        private void CarregaUsuarios(string co_tipo)
        {
            switch (co_tipo)
            {
                case "G":
                case "C":
                    AuxiliCarregamentos.CarregaProfissionaisSaude(ddlUsuario, 0, true, "0", true, 0, false);
                    break;
                case "R":
                    AuxiliCarregamentos.CarregaResponsaveis(ddlUsuario, LoginAuxili.ORG_CODIGO_ORGAO, true);
                    break;
                case "P":
                    AuxiliCarregamentos.CarregaPacientes(ddlUsuario, 0, true, true);
                    break;
                case "0":
                    ddlUsuario.Items.Clear();
                    ddlUsuario.Items.Insert(0, new ListItem("Todos", "0"));
                    break;
            }
        }

        protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuarios(ddlTipo.SelectedValue);
        }

        protected void lnkResetarSenha_OnClick(object sender, EventArgs e)
        {
            try
            {
                var tbs384 = RetornaEntidade();
                tbs384.DE_SENHA = LoginAuxili.GerarMD5(TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).NU_CPF_COL);
                TBS384_USUAR_APP.SaveOrUpdate(tbs384, true);
                AuxiliPagina.RedirecionaParaPaginaSucesso("Senha do App resetada com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao resetar a senha!");
                return;
            }
        }

        #endregion
    }
}