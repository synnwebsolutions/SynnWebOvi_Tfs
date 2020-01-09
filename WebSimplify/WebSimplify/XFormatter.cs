using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace WebSimplify
{
    public static class XFormatter
    {
        public static string FormatXmlFromString(string xml)
        {
            dynamic obj = xml;
            StringBuilder sb = new StringBuilder();
            
            XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(xml));

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        sb.AppendLine(reader.Name);

                        while (reader.MoveToNextAttribute()) // Read the attributes.
                            sb.AppendLine( reader.Name + "='" + reader.Value + "'");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        sb.AppendLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        sb.AppendLine(reader.Name);
                        break;
                }
            }
            return sb.ToString();
        }

        internal static T ParseBackXml<T>(string parsedXml)
        {
           throw  new NotImplementedException();
        }
    }
}