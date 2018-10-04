using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SynnCore.Generics
{
    public class XmlHelper
    {
        static public string ToXml(object o)
        {
            XmlSerializer xml = new XmlSerializer(o.GetType());
            StringWriter sw = new StringWriter();
            XmlWriterSettings s = new XmlWriterSettings();
            s.OmitXmlDeclaration = true;
            s.Indent = true;
            System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sw, s);
            xml.Serialize(xw, o);
            return sw.ToString();
        }

        static public object CreateFromXml(Type t, string data)
        {
            XmlSerializer xml = new XmlSerializer(t);
            TextReader txt = new StringReader(data);
            return xml.Deserialize(txt);
        }

        // generic form of the method
        static public T CreateFromXml<T>(string data)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            TextReader txt = new StringReader(data);
            return (T)xml.Deserialize(txt);
        }

    }
}
