//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: ALTERAR SENHA DO USUÁRIO
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class AlterarSenha : System.Web.UI.Page
    {
        #region Métodos

        /// <summary>
        /// Salva a nova senha na tabela de usuário (ADMUSUARIO).
        /// </summary>
        /// <param name="admUsuario">Entidade ADMUSUARIO</param>
        private void SalvaNovaSenha(ADMUSUARIO admUsuario)
        {
            admUsuario.desSenha = LoginAuxili.GerarMD5(txtConfirmaNovaSenha.Text);
            ADMUSUARIO.SaveOrUpdate(admUsuario);

            lblMensagem.Text = "Nova Senha salva com Sucesso!";
            lblMensagem.Visible = true;
        }
        #endregion

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string strSenhaMD5 = LoginAuxili.GerarMD5(txtSenhaAtual.Text);

            if (!Page.IsValid)
                return;

            ADMUSUARIO admUsuario = (from lAdmUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                     where lAdmUsuario.desSenha == strSenhaMD5 && lAdmUsuario.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                                     select lAdmUsuario).FirstOrDefault();

            if (admUsuario != null)
                SalvaNovaSenha(admUsuario);
            else
                AuxiliPagina.EnvioMensagemErro(this, "Senha Atual Incorreta. Por Favor preencha o campo Senha Atual e tente novamente.");
        }
    }
}