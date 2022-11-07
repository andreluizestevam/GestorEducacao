//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: ESQUECI MINHA SENHA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Net.Mail;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class EsqueciSenha : System.Web.UI.Page
    {
        #region Métodos

        /// <summary>
        /// Recupera a senha do usuário pelo login do usuário informado.
        /// </summary>
        /// <param name="strLoginUsuario">Login do usuário</param>
        private void RecuperaSenhaPeloLogin(string strLoginUsuario)
        {
            ADMUSUARIO admUsuario = ADMUSUARIO.RetornaPeloNomeUsuario(strLoginUsuario);

            if (admUsuario != null)
            {
                string strSenhaUsuario = admUsuario.desSenha;
                if (admUsuario.TipoUsuario.ToLower().Equals("a"))
                    AuxiliPagina.EnvioMensagemErro(this, "Aluno não tem email cadastrado.");
                else
                {
                    TB03_COLABOR colaborador = TB03_COLABOR.RetornaPelaChavePrimaria(admUsuario.CO_EMP, admUsuario.CodUsuario);

                    if (colaborador != null)
                        EnviaSenhaPorEmail("senha", colaborador.CO_EMAI_COL);
                        //EnviaSenhaPorEmail(strSenhaUsuario, colaborador.CO_EMAI_COL);
                    else
                        AuxiliPagina.EnvioMensagemErro(this, "Não existe Funcionário associado ao Nome de Usuário informado.");
                }
            }
            else
                AuxiliPagina.EnvioMensagemErro(this, String.Format("Usuário {0} não encontrado.", strLoginUsuario));
        }

        /// <summary>
        /// Faz o envio da senha do usuário pelo email
        /// </summary>
        /// <param name="strSenhaUsuario">Senha do usuário</param>
        /// <param name="strEmailUsuario">Email do usuário</param>
        private void EnviaSenhaPorEmail(string strSenhaUsuario, string strEmailUsuario)
        {
            if (strEmailUsuario == "")
                AuxiliPagina.EnvioMensagemErro(this, "Usuário não possui email cadastrado, por favor entre em contato com o Administrador do Sistema.");
            else
            {
//------------> Linha de envio de Email
                //new SmtpClient().Send("suporte@c2br.com.br", strEmailUsuario, "Solicitação de Recuperação de Senha", "Sua senha atual é: " + strSenhaUsuario);

                AuxiliPagina.EnvioMensagemErro(this, "Servidor SMTP não está configurado.");
            }
        }
        #endregion

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtNomeUsuario.Text != "")
                RecuperaSenhaPeloLogin(txtNomeUsuario.Text);
        }               
    }
}