//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

//---> Utilizado para informar se conexão como a internet está OK
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliValidacao
    {
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Faz a verificação para saber se o usuário tem acesso ao sistema no dia informado.
        /// </summary>
        /// <param name="strSeg">flag de acesso na segunda-feira</param>
        /// <param name="strTer">flag de acesso na terça-feira</param>
        /// <param name="strQua">flag de acesso na quarta-feira</param>
        /// <param name="strQui">flag de acesso na quinta-feira</param>
        /// <param name="strSex">flag de acesso na sexta-feira</param>
        /// <param name="strSab">flag de acesso no sábado</param>
        /// <param name="strDom">flag de acesso no domingo</param>
        /// <returns>Se usuário tem acesso no dia informado retorna true, senão false</returns>
        public static bool ValidaAcessoDiaSemana(string strSeg, string strTer, string strQua, string strQui, string strSex, string strSab, string strDom)
        {
            bool lblAcesso = false;

//--------> Faz a verificação do dia corrente Exemplo: Domingo
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
            System.Globalization.DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            string diaSemanaCorrente = culture.TextInfo.ToTitleCase(dtfi.GetAbbreviatedDayName(DateTime.Now.DayOfWeek));

//--------> Faz a verificação para saber se o usuário tem permissão para acessar no dia informado
            switch (diaSemanaCorrente)
            {
                case "Seg":
                    if (strSeg == "S")
                        lblAcesso = true;
                    break;
                case "Ter":
                    if (strTer == "S")
                        lblAcesso = true;
                    break;
                case "Qua":
                    if (strQua == "S")
                        lblAcesso = true;
                    break;
                case "Qui":
                    if (strQui == "S")
                        lblAcesso = true;
                    break;
                case "Sex":
                    if (strSex == "S")
                        lblAcesso = true;
                    break;
                case "Sab":
                    if (strSab == "S")
                        lblAcesso = true;
                    break;
                case "Sáb":
                    if (strSab == "S")
                        lblAcesso = true;
                    break;
                case "Dom":
                    if (strDom == "S")
                        lblAcesso = true;
                    break;
            }

            return lblAcesso;
        }

        /// <summary>
        /// Validaca CNPJ
        /// </summary>
        /// <param name="strCnpj">CNPJ</param>
        /// <returns></returns>
        public static bool ValidaCnpj(string strCnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;
            string digito;
            string tempCnpj;

            strCnpj = strCnpj.Trim();
            strCnpj = strCnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (strCnpj.Length != 14)
                return false;

            tempCnpj = strCnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return strCnpj.EndsWith(digito);

        }

        /// <summary>
        /// Valida CPF
        /// </summary>
        /// <param name="strCpf">CPF</param>
        /// <returns></returns>
        public static bool ValidaCpf(string cpf)
        {
            #region Validações básicas
            if (cpf == "")
                return false;
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
                    break;
            }
            #endregion
            #region Variavéis
            int digito1, digito2;
            int soma = 0;
            string digitado = "";
            string calculado = "";
            int[] peso1 = new int[] { 10, 9, 8, 7, 6 ,5, 4, 3, 2};
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2};
            int[] n = new int[11];
            #endregion
            
            try
            {
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(0, 1));
                n[2] = Convert.ToInt32(cpf.Substring(0, 1));
                n[3] = Convert.ToInt32(cpf.Substring(0, 1));
                n[4] = Convert.ToInt32(cpf.Substring(0, 1));
                n[5] = Convert.ToInt32(cpf.Substring(0, 1));
                n[6] = Convert.ToInt32(cpf.Substring(0, 1));
                n[7] = Convert.ToInt32(cpf.Substring(0, 1));
                n[8] = Convert.ToInt32(cpf.Substring(0, 1));
                n[9] = Convert.ToInt32(cpf.Substring(0, 1));
                n[10] = Convert.ToInt32(cpf.Substring(0, 1));
            }
            catch(Exception e)
            {
                return false;
            }

            #region Digito 1
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
                soma += (peso1[i] * Convert.ToInt32(n[i]));
            int resto = soma % 11;
            if (resto == 1 || resto == 0)
                digito1 = 0;
            else
                digito1 = 11 - resto;
            soma = 0;
            #endregion

            #region Digito 2
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
                soma += (peso2[i] * Convert.ToInt32(n[i]));
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                digito2 = 0;
            else
                digito2 = 11 - resto;
            soma = 0;
            #endregion

            calculado = digito1.ToString() + digito2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            if (calculado == digitado)
                return true;
            else
                return true;
        }

        /// <summary>
        /// Gera um CPF válido
        /// </summary>
        /// <param name="ComPontos"></param>
        /// <returns></returns>
        public static string GerarNovoCPF(bool ComPontos)
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;

            if (ComPontos)
                semente = string.Format("{0}.{1}.{2}-{3}", semente.Substring(0, 3), semente.Substring(3, 3), semente.Substring(6, 3), semente.Substring(9, 2));

            return semente;
        }

        /// <summary>
        /// Validade se hora informada é válida
        /// </summary>
        /// <param name="hora">string de hora</param>
        /// <returns></returns>
        public static bool ValidaHora(string hora)
        {
            Regex r = new Regex(@"([0-1][0-9]|2[0-3]):[0-5][0-9]");
            Match m = r.Match(hora);
            return m.Success;
        }

        /// <summary>
        /// Método que verifica se internet está disponível
        /// </summary>
        /// <returns>Retorna true se internet disponível, senão retorna false</returns>
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }        
    }
}
