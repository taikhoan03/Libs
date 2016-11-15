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
        /// Generate (String)XML from object
        /// </summary>
        /// <typeparam name="T">Class object can be convert to XML string, created by Hà Chí Cường</typeparam>
        /// <param name="objectToSerialize">This object can be convert to XML string, created by Hà Chí Cường</param>
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
        /// <summary>
        /// Convert (String)XML to object
        /// </summary>
        /// <typeparam name="T">XML string can be convert to Object, created by Hà Chí Cường</typeparam>
        /// <param name="objectToSerialize">This XML string can be convert to object, created by Hà Chí Cường</param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate (String)XML from LIST object
        /// </summary>
        /// <typeparam name="T">Class object can be convert to XML string, created by Hà Chí Cường</typeparam>
        /// <param name="objectToSerialize">This methods can be used for a LIST,
        /// This object can be convert to XML string, created by Hà Chí Cường</param>
        /// <returns></returns>
        public static string XmlSerialize<T>(this List<T> objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.Formatting = Formatting.Indented;
            xmlSerializer.Serialize(xmlWriter, objectToSerialize);

            return stringWriter.ToString();
        }
        /// <summary>
        /// Convert (String)XML to LIST object
        /// </summary>
        /// <typeparam name="T">XML string can be convert to LIST Object, created by Hà Chí Cường</typeparam>
        /// <param name="objectToSerialize">This methods can be used for a LIST,
        /// This XML string can be convert to LIST object, created by Hà Chí Cường</param>
        /// <returns></returns>
        public static List<T> XMLStringToListObject<T>(this List<T> objectToSerialize, string xml)
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
