//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class SMSAuxili
    {
        /// <summary>
        /// Faz o envio da menssagem para o numero de celular informado.
        /// </summary>
        /// <param name="nomeRemetente">Nome do remetente</param>
        /// <param name="mensagEnviada">Mensagem a ser enviada.</param>
        /// <param name="numeroCelular">O numero do celular para enviar a mensagem. Exemplo: 551167057491 (55 País; 11 Código da Área, 67057491 Numero do Telefone)</param>
        /// <param name="strIdMensag">Define um ID para a mensagem</param>
        public static SMSRequestReturn EnvioSMS(string nomeRemetente, string mensagEnviada, string numeroCelular, string strIdMensag)
        {
            if (strIdMensag.Length > 20)
                return SMSRequestReturn.IDOverflow;
            if (numeroCelular.Length > 12)
                return SMSRequestReturn.IncorrectOrIncompleteToMobileNumber;
            if ((mensagEnviada.Length + nomeRemetente.Length) > 142)
                return SMSRequestReturn.MessageContentOverflow;
            try
            {
                StringBuilder strBuiServicoURL = new StringBuilder("http://system.human.com.br/GatewayIntegration/msgSms.do?");

                string nomeDaConta = "c2br";
                string senhaDaConta = "HqYiPRwQmn";

                strBuiServicoURL.AppendFormat("dispatch=send&account={3}&code={4}&msg={0}&from={1}&to={2}",
                                            mensagEnviada,
                                            nomeRemetente,
                                            numeroCelular,
                                            nomeDaConta,
                                            senhaDaConta);

                if (!String.IsNullOrEmpty(strIdMensag))
                    strBuiServicoURL.AppendFormat("&id={0}", strIdMensag);

                WebClient webClient = new WebClient();
                return GetSMSRequestReturn(webClient.DownloadString(strBuiServicoURL.ToString()));
            }
            catch (Exception)
            {                
                throw;
                // 
            }
        }

        /// <summary>
        /// Transforma a string de retorno do WebService em um enumerador do tipo SMSRequestReturn.
        /// </summary>
        /// <param name="webServiceReturn">A string de retorno do WebService.</param>
        /// <returns>SMSRequestReturn que representa o retorno do WebService.</returns>
        static SMSRequestReturn GetSMSRequestReturn(string webServiceReturn)
        {
            SMSRequestReturn returnMessage = SMSRequestReturn.MessageSent;

            if (webServiceReturn.Contains(((int)SMSRequestReturn.AccountLimitReached).ToString()))
                return SMSRequestReturn.AccountLimitReached;
            else if (webServiceReturn.Contains((((int)SMSRequestReturn.AuthenticationError)).ToString()))
                return SMSRequestReturn.AuthenticationError;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.EmptyMessageContent).ToString()))
                return SMSRequestReturn.EmptyMessageContent;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.EmptyToMobileNumber).ToString()))
                return SMSRequestReturn.EmptyToMobileNumber;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.IDOverflow).ToString()))
                return SMSRequestReturn.IDOverflow;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.IncorrectOrIncompleteToMobileNumber).ToString()))
                return SMSRequestReturn.IncorrectOrIncompleteToMobileNumber;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.MessageBodyInvalid).ToString()))
                return SMSRequestReturn.MessageBodyInvalid;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.MessageContentOverflow).ToString()))
                return SMSRequestReturn.MessageContentOverflow;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.MessageWithSameIDAlreadySent).ToString()))
                return SMSRequestReturn.MessageWithSameIDAlreadySent;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.SchedulingDateInvalidOrIncorrect).ToString()))
                return SMSRequestReturn.SchedulingDateInvalidOrIncorrect;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.UnknownError).ToString()))
                return SMSRequestReturn.UnknownError;
            else if (webServiceReturn.Contains(((int)SMSRequestReturn.WrongOperationRequested).ToString()))
                return SMSRequestReturn.WrongOperationRequested;

            return returnMessage;
        }

        /// <summary>
        /// Códigos de status para o retorno do WebService
        /// </summary>
        public enum SMSRequestReturn
        {
            /// <summary>
            /// Mensagem enviada com sucesso
            /// </summary>
            MessageSent = 000,

            /// <summary>
            /// Mensagem vazia
            /// </summary>
            EmptyMessageContent = 010,

            /// <summary>
            /// Corpo da mensagem inválido.
            /// </summary>
            MessageBodyInvalid = 011,

            /// <summary>
            /// Corpo da mensagem excedeu o limite. Os campos
            /// 'from' e 'body' devem ter juntos no máximo 142
            /// caracteres.
            /// </summary>
            MessageContentOverflow = 012,

            /// <summary>
            /// Número do destinatário está incompleto ou inválido.
            /// O número deve conter o código do país e código de
            /// área além do número. Apenas dígitos são aceitos.
            /// </summary>
            IncorrectOrIncompleteToMobileNumber = 013,
            
            /// <summary>
            /// Número do destinatário está vazio
            /// </summary>
            EmptyToMobileNumber = 014,

            /// <summary>
            /// A data de agendamento está mal formatada. O
            /// formato correto deve ser: "dd/MM/aaaa hh:mm:ss"
            /// </summary>
            SchedulingDateInvalidOrIncorrect = 015,

            /// <summary>
            /// ID informado ultrapassou o limite de 20 caracteres.
            /// </summary>
            IDOverflow = 016,

            /// <summary>
            /// Já foi enviada uma mensagem de sua conta com o
            /// mesmo identificador.
            /// </summary>
            MessageWithSameIDAlreadySent = 080,

            /// <summary>
            /// Erro de autenticação em "account" e/ou "code".
            /// </summary>
            AuthenticationError = 900,

            /// <summary>
            /// Seu limite de segurança foi atingido. Contate nosso
            /// suporte para verificação/liberação
            /// </summary>
            AccountLimitReached = 990,

            /// <summary>
            /// Foi invocada uma operação inexistente.
            /// </summary>
            WrongOperationRequested = 998,

            /// <summary>
            /// Erro desconhecido. Contate nosso suporte.
            /// </summary>
            UnknownError = 999
        }
    }
}
