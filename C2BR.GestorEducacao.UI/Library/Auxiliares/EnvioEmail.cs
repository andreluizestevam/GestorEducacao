//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class EnvioEmail
    {
        /// <summary>
        /// Método de envio de um email.
        /// </summary>
        /// <param name="corpoEmail">Corpo do email</param>
        /// <param name="emailTO">Para quem vai ser enviado o email</param>
        /// <param name="emailCC">Com cópia oculta para alguém</param>
        /// <param name="assunto">Assunto</param>
        /// <param name="tipoHTML">Tipo do email (se é HTML(true) ou texto puro(false))</param>
        public static void EnviaEMail(string corpoEmail, string emailTO, string emailCC, string assunto, bool tipoHTML)
        {
            try
            {
//------------> Instancia da Classe de Mensagem
                MailMessage mailMessage = new MailMessage();

//------------> Remetente E-mail do Gestor Educacao
                mailMessage.From = new MailAddress("suporte@c2br.com.br");

//------------> Destinario
                if (emailTO != "")
                    mailMessage.To.Add(emailTO);

//------------> Com copia oculta
                if (emailCC != "")
                    mailMessage.Bcc.Add(emailCC);

//------------> Assunto
                mailMessage.Subject = assunto;

//------------> A mensagem é do tipo HTML(true) ou Texto Puro (false)?
                mailMessage.IsBodyHtml = tipoHTML;

//------------> Corpo da Mensagem
                mailMessage.Body = corpoEmail;

//------------> Instancia a Classe de Envio
                SmtpClient smtpClient = new SmtpClient("smtp.mail.net.br");

//------------> Credencial para envio por SMTP Seguro (APENAS QUANDO O SERVIDOR EXIGE AUTENTICAÇÃO)   
                smtpClient.Credentials = new NetworkCredential("suporte@c2br.com.br", "C2BrSuporte");

//------------> Envia a mensagem   
                smtpClient.Send(mailMessage);

            }
            catch (Exception f)
            {
                //lblMensagemErro.Text = f.Message;
            }
        }
    }
}