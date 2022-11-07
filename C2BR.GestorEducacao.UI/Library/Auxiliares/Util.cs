using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class Util
    {
        public static string RetornaIpCliente()
        {
            var ret = HttpContext.Current.Request.UserHostAddress;
            if (!string.IsNullOrEmpty(ret) && ret.Length >= 9)
                return ret;

            System.Web.HttpContext context = System.Web.HttpContext.Current;
            return (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                     context.Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();

        }

        public static EnderecoSN RetornaEndereco(string cep)
        {
            cep = cep.Replace("-", "").Replace(".", "").Replace(",", "").Trim();
            EnderecoSN ret = new EnderecoSN();
            XmlDocument cepXml = new XmlDocument();
            cepXml.Load("http://webservice.uni5.net/web_cep.php?auth=2025cea9af5a514d1daea52a07219b5e&formato=xml&cep=" + cep);
            XmlElement root = cepXml.DocumentElement;
           // XmlNodeList nodes = root.SelectNodes("webservicecep");

            ret.Logradouro = string.Format("{0} {1}", root["tipo_logradouro"].InnerText, root["logradouro"].InnerText);
            ret.Bairro = root["bairro"].InnerText;
            ret.Cidade = root["cidade"].InnerText;
            ret.Uf = root["uf"].InnerText;           
            ret.Resultado = !string.IsNullOrEmpty(root["uf"].InnerText);

            return ret;
        }
    }

    public class EnderecoSN
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public bool Resultado { get; set; }


    }
}