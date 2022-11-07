using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using C2BR.GestorEducacao.LicenseValidator.Properties;

namespace C2BR.GestorEducacao.LicenseValidator
{
    public static class Signer
    {
        //public static void Install(string fileName)
        //{
        //    X509Certificate2 cert = new X509Certificate2(fileName, "cordova.123");
        //    X509Store st = new X509Store(StoreName.TrustedPeople, StoreLocation.CurrentUser);

        //    st.Open(OpenFlags.ReadWrite);
        //    st.Add(cert);
        //    st.Close();
        //}

        public static string SignXml(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml))
                    throw new ArgumentNullException("xml", "O xml passado não pode ser nulo.");

                X509Certificate2 cert = SelectCertificate();
                if (cert == null) // Usuário cancelou a ação
                    return null;

                if (cert.PrivateKey == null)
                    throw new Exception("Não foi possível acessar a chave privada do certificado.");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                SignedXml signedXml = new SignedXml(doc);
                signedXml.SigningKey = cert.PrivateKey;

                Reference reference = new Reference() { Uri = "" };

                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);

                XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                reference.AddTransform(c14);

                signedXml.AddReference(reference);

                KeyInfo keyInfo = new KeyInfo();
                keyInfo.AddClause(new KeyInfoX509Data(cert));

                signedXml.KeyInfo = keyInfo;

                signedXml.ComputeSignature();

                XmlElement xmlSignature = signedXml.GetXml();
                doc.DocumentElement.AppendChild(doc.ImportNode(xmlSignature, true));

                return doc.InnerXml;
            }
            catch { throw; }
        }

        public static bool VerifySignature(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml))
                    throw new ArgumentNullException("xml", "O xml passado não pode ser nulo.");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                RSAKeyValue pKey = new RSAKeyValue();
                pKey.Key = RSA.Create();
                pKey.Key.FromXmlString(Resources.PublicKey);

                SignedXml signed = new SignedXml(doc);
                XmlNodeList nodeLst = doc.GetElementsByTagName("Signature");

                if (nodeLst.Count == 0)
                    throw new Exception("O xml não possui assinatura.");

                signed.LoadXml((XmlElement)nodeLst[0]);
                return signed.CheckSignature(pKey.Key);
            }
            catch { throw; }
        }

        public static string EncryptXml(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml))
                    throw new ArgumentNullException("xml", "O xml passado não pode ser nulo.");

                return Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            }
            catch { throw; }
        }

        public static string DecryptXml(string xmlEncrypt)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlEncrypt))
                    throw new ArgumentNullException("xml", "O xml passado não pode ser nulo.");

                return Encoding.UTF8.GetString(Convert.FromBase64String(xmlEncrypt));
            }
            catch { throw; }
        }

        public static string GetPublicKey()
        {
            X509Certificate2 cert = SelectCertificate();

            if (cert == null)
                return null;

            return cert.PublicKey.Key.ToXmlString(false);
        }

        // Busca certificados
        private static X509Certificate2 SelectCertificate()
        {
            // StoreName.My - nome do repositório do certificado, teoricamente a pasta pessoal
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            // Abre repositório
            store.Open(OpenFlags.ReadOnly);

            // Busca certificados
            // X509KeyUsageFlags.NonRepudiation - certificados para autenticação
            X509Certificate2Collection col = store.Certificates;


            /*
             * 
             * Versão inicial
             * Porém com essa versão não consigo ver o certificado na máquina virtual, porém no servidor consegue visualizar normalmente
             
               X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                 store.Open(OpenFlags.ReadOnly);

                 X509Certificate2Collection col = store.Certificates.Find(X509FindType.FindByKeyUsage,
                X509KeyUsageFlags.NonRepudiation, true);
             
             * 
             */


            X509Certificate2 cert = null;
            X509Certificate2Collection selection = X509Certificate2UI.SelectFromCollection(col,
                "Certificados", "Selecione o certificado", X509SelectionFlag.SingleSelection);

            if (selection.Count > 0)
            {
                X509Certificate2Enumerator en = selection.GetEnumerator();
                en.MoveNext();
                cert = en.Current;
            }

            store.Close();

            return cert;
        }
    }
}
