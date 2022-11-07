using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace C2BR.GestorEducacao.LicenseValidator
{
    public static class SerializableHelper
    {
        public static bool Deserialize<T>(string xml, out T value, out Exception ex) where T : class
        {
            ex = null;
            value = null;

            try
            {
                if (xml.StartsWith("?"))
                    xml = xml.Remove(0, 1);

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                UTF8Encoding encoding = new UTF8Encoding();
                TextReader txtReader = new StringReader(xml);

                value = (T)serializer.Deserialize(txtReader);

                return true;
            }
            catch (Exception exc)
            {
                ex = exc;
                return false;
            }
        }

        public static bool Serialize<T>(T obj, out string xml, out Exception ex)
        {
            ex = null;
            xml = null;
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.IndentChars = "";
                settings.Indent = false;
                settings.NewLineChars = "";

                using (StringWriter sw = new StringWriter())
                {
                    using (XmlWriter xw = XmlWriter.Create(sw, settings))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(xw, obj);
                        xml = sw.ToString();

                        if (xml.StartsWith("?"))
                            xml = xml.Remove(0, 1);
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                ex = exc;
                return false;
            }
        }
    }
}
