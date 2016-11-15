using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Activities;
namespace Libs
{
    /// <summary>
    /// Created by cuong.ha
    /// 
    /// </summary>
    public static class ExtMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(this T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.Formatting = Formatting.Indented;
            xmlSerializer.Serialize(xmlWriter, objectToSerialize);

            return stringWriter.ToString();
        }

        public static T XMLStringToObject<T>(this T objectToSerialize,string xml)
        {
            if (String.IsNullOrEmpty(xml)) return default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            object result;
            using (System.IO.TextReader reader = new System.IO.StringReader(xml))
            {
                result = xmlSerializer.Deserialize(reader);
            }
            return (T)result;
        }
        #region List Objects Serialize
        public static string XmlSerialize<T>(this List<T> objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.Formatting = Formatting.Indented;
            xmlSerializer.Serialize(xmlWriter, objectToSerialize);

            return stringWriter.ToString();
        }
        public static List<T> XMLStringToObject<T>(this List<T> objectToSerialize, string xml)
        {
            if (String.IsNullOrEmpty(xml)) return null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            object result;
            using (System.IO.TextReader reader = new System.IO.StringReader(xml))
            {
                result = xmlSerializer.Deserialize(reader);
            }
            return (List<T>)result;
        }
        #endregion
    }
}
