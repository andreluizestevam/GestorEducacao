//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: SUPORTE OPERACIONAL DA BASE DE DADOS
// OBJETIVO: CÓPIA DE SEGURANÇA
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
using Resources;
using System.Configuration;
using C2BR.GestorEducacao.DatabaseManagement;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Threading;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0501_CopiaSeguranca
{
    public partial class GerarCopiaDeSeguranca : System.Web.UI.Page
    {
        public PadraoGenericas CurrentPadraoBuscas { get { return (App_Masters.PadraoGenericas)Master; } }

        #region Variaveis

        static string nomeBackup = String.Format("{0} - {1}", ConfigurationManager.AppSettings.Get(AppSettings.BackupSetName), DateTime.Now.ToShortDateString());
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CurrentPadraoBuscas.DefineMensagem(MensagensAjuda.MessagemCampoObrigatorio, "Ultilize os campos abaixo para configurar a Cópia de Segurança que será realizada.");
            txtNomeCS.Text = nomeBackup;
        }
        #endregion

//====> Método executado quando botão de Ação clicado
        protected void btnActionCS_Click(object sender, EventArgs e)
        {
//--------> Faz a configuração no servidor, para o mesmo executar o backup a partir de informaçoes no web.config
            SQLServer server = new SQLServer(ConfigurationManager.AppSettings.Get(AppSettings.DatabaseName),
                                             ConfigurationManager.AppSettings.Get(AppSettings.BackupConnectionString));            
//--------> Inicia o backup
            server.Backup(nomeBackup, txtDescricaoCS.Text);

            AuxiliPagina.RedirecionaParaPaginaSucesso("Cópia de segurança gerada com sucesso.", Request.Url.AbsoluteUri);
        }        
    }
}
