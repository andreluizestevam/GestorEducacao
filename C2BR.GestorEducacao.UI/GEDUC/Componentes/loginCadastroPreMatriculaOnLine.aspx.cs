using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Net.Mail;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class loginCadastroPreMatriculaOnLine : System.Web.UI.Page
    {
        string enderecoPreMatricula;
        protected void Page_Load(object sender, EventArgs e)
        {
            enderecoPreMatricula = "/GEDUC/2000_CtrlOperSecretariaEscolar/2100_CtrlServSecretariaEscolar/2113_PreMatriculaAluno/cadastro.aspx?moduloId=1066&moduloNome=Pré-Matrícula de Aluno&moduloId=1066";
            if (!IsPostBack)
            {
                Session.Clear();
                controleCampos();
            }
        }

        #region Metodos personalizados
        /// <summary>
        /// Realiza o controle de visibilidade dos campos de login, senha e cadastro
        /// </summary>
        /// <param name="cadastro">Parametro que caso null mostra o campo de cpf, true mostre os campos de cadastro e false o de senha</param>
        protected void controleCampos(bool? cadastro = null)
        {
            switch (cadastro)
            {
                case null:
                    divCpf.Visible = true;
                    txtCpf.Text = "";
                    txtCpf.Focus();
                    divSenha.Visible = divCadastro.Visible = false;
                    break;
                case true:
                    divCadastro.Visible = true;
                    txtSenhaNova.Text = txtSenhaNovaConfirma.Text = "";
                    txtSenhaNova.Focus();
                    divCpf.Visible = divSenha.Visible = false;
                    CarregarUnidades();
                    break;
                case false:
                    divSenha.Visible = true;
                    lblCadastrado.Visible = true;
                    divCpf.Visible = divCadastro.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Verifica se o usuário e a senha digitados estão válidos
        /// </summary>
        /// <param name="senha">Informe se verifica também a senha ou não</param>
        /// <returns></returns>
        protected bool verificarUsuario(bool senha = false)
        {
            string senhaMd5 = "";
            if (PreAuxili.senhaResponsavel != "")
                senhaMd5 = LoginAuxili.GerarMD5(PreAuxili.senhaResponsavel);
            var usuario = (from usu in ADMUSUARIO.RetornaTodosRegistros()
                               where usu.desLogin == PreAuxili.cpfResponsavel
                               && (senha ? usu.desSenha == senhaMd5 : 0 == 0)
                               select usu).FirstOrDefault();
            if (usuario == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Habilita e mostra a mensagem de erro/aviso
        /// </summary>
        /// <param name="mensagem">Mensagem e ser mostrada</param>
        protected void mostrarAviso(string mensagem = "")
        {
            txtErro.Visible = true;
            txtErro.Text = mensagem;
        }
        #endregion

        #region Eventos de componentes
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (txtCpf.Text != "")
            {
                if (AuxiliValidacao.ValidaCpf(txtCpf.Text))
                {
                    PreAuxili.cpfResponsavel = txtCpf.Text;
                    controleCampos(!verificarUsuario());
                }
                else
                {
                    mostrarAviso("CPF digitado inválido! Por favor informe um CPF válido.");
                    controleCampos();
                }
            }
            else
            {
                mostrarAviso("Informe o CPF.");
                controleCampos();
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (dpUnidades.SelectedValue != "")
            {
                if (txtSenhaNova.Text != ""
                    && txtSenhaNovaConfirma.Text != ""
                    && txtSenhaNova.Text == txtSenhaNovaConfirma.Text
                    )
                {
                    PreAuxili.senhaResponsavel = txtSenhaNova.Text;
                    PreAuxili.codigoUnidadePreMatricula = int.Parse(dpUnidades.SelectedValue);
                    if (PreAuxili.cadastroUsuarioResponsavel())
                    {
                        controleCampos(false);
                        mostrarAviso("Cadastro realizado com sucesso!");
                    }
                    else
                    {
                        controleCampos();
                        mostrarAviso("Não foi possível realizar o cadastro, por favor tente novamente.");
                    }
                }
                else
                {
                    mostrarAviso("As senhas digitadas não conferem.");
                }
            }
            else
            {
                controleCampos(true);
                mostrarAviso("Selecione a unidade escolar.");
            }
        }
        #endregion

        #region Carregadores
        protected void CarregarUnidades()
        {
            dpUnidades.Items.Clear();
            dpUnidades.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).DistinctBy(d => d.CO_EMP).OrderBy(o => o.NO_FANTAS_EMP);
            dpUnidades.DataTextField = "NO_FANTAS_EMP";
            dpUnidades.DataValueField = "CO_EMP";
            dpUnidades.DataBind();
            dpUnidades.Items.Insert(0, new ListItem("Selecione", ""));
            dpUnidades.SelectedValue = "";
        }
        #endregion
    }
}