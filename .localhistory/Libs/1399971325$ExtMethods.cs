﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Activities;
namespace Libs
{
    public static class ExtMethods
    {
        
        public static string XmlSerialize<T>(this T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.Formatting = Formatting.Indented;
            xmlSerializer.Serialize(xmlWriter, objectToSerialize);

            return stringWriter.ToString();
        }

        public static T XMLStringToObject<T>(string xml)
        {
            if (String.IsNullOrEmpty(xml)) return null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            object result;
            using (System.IO.TextReader reader = new System.IO.StringReader(xml))
            {
                result = xmlSerializer.Deserialize(reader);
            }
            return (T)result;
        }

        public string ToXMLString()
        {
            var entity = this;
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(stringWriter);
            serializer.Serialize(writer, entity);
            return stringWriter.ToString();
        }
        
    }
}