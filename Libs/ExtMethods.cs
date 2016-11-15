using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Activities;
using System.Text.RegularExpressions;

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

            using (StringWriter stringWriter = new StringWriter())
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

                xmlWriter.Formatting = Formatting.Indented;
                xmlSerializer.Serialize(xmlWriter, objectToSerialize);


                xmlWriter.Close();

                return stringWriter.ToString();
            }

        }
        /// <summary>
        /// Convert (String)XML to object
        /// </summary>
        /// <typeparam name="T">XML string can be convert to Object, created by Hà Chí Cường</typeparam>
        /// <param name="objectToSerialize">This XML string can be convert to object, created by Hà Chí Cường</param>
        /// <returns></returns>
        public static T XMLStringToObject<T>(this string objectToSerialize)
        {
            if (String.IsNullOrEmpty(objectToSerialize)) return default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            object result;
            using (System.IO.TextReader reader = new System.IO.StringReader(objectToSerialize))
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

            using (StringWriter stringWriter = new StringWriter())
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

                xmlWriter.Formatting = Formatting.Indented;
                xmlSerializer.Serialize(xmlWriter, objectToSerialize);
                xmlWriter.Close();
                return stringWriter.ToString();
            }
            
        }
        public static string extractErrorMessage(this Exception ex)
        {
            return ex.Message + Environment.NewLine + ex.StackTrace;
        }
        /// <summary>
        /// Convert (String)XML to LIST object
        /// </summary>
        /// <typeparam name="T">XML string can be convert to LIST Object, created by Hà Chí Cường</typeparam>
        /// <param name="objectToSerialize">This methods can be used for a LIST,
        /// This XML string can be convert to LIST object, created by Hà Chí Cường</param>
        /// <returns></returns>
        public static List<T> XMLStringToListObject<T>(this string objectToSerialize)
        {
            if (String.IsNullOrEmpty(objectToSerialize)) return null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            object result;
            using (System.IO.TextReader reader = new System.IO.StringReader(objectToSerialize))
            {
                result = xmlSerializer.Deserialize(reader);
            }
            return (List<T>)result;
        }



        public static string FormatWith(this string format, object source)

        {

            return FormatWith(format, null, source);

        }
        public static string FormatWith_ExpandObject(this string format, System.Dynamic.ExpandoObject source)

        {

            return FormatWith_ExpandObject(format,null, source);

        }
        public static object Eval_ExpandObject(this string format, System.Dynamic.ExpandoObject source)

        {
            var formatted = format.FormatWith_ExpandObject(source);
            var expression = new ExpressionEvaluator.CompiledExpression(formatted);
            //var result = expression.Eval();
            return expression.Eval();

        }


        public static string FormatWith(this string format, IFormatProvider provider, object source)

        {
            return SmartFormat.Smart.Format(format, source);
            if (format == null)

                throw new ArgumentNullException("format");



            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",

              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);



            List<object> values = new List<object>();

            string rewrittenFormat = r.Replace(format, delegate (Match m)

            {

                Group startGroup = m.Groups["start"];

                Group propertyGroup = m.Groups["property"];

                Group formatGroup = m.Groups["format"];

                Group endGroup = m.Groups["end"];



                values.Add((propertyGroup.Value == "0")

                  ? source

                  : System.Web.UI.DataBinder.Eval(source, propertyGroup.Value));



                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value

                  + new string('}', endGroup.Captures.Count);

            });



            return string.Format(provider, rewrittenFormat, values.ToArray());

        }
        public static List<string> GetPlaceHolderName_ExpandObject(this string format)
        {
            var rs = new List<string>();
            var detail = new SmartFormat.SmartFormatter();
            var parsingFormat = detail.Parser.ParseFormat(format);
            //IDictionary<string, object> myUnderlyingObject = source;
            foreach (var item in parsingFormat.Items)
            {
                if (item is SmartFormat.Core.Parsing.Placeholder)
                {
                    //Console.WriteLine(item.Text.Substring(1, item.Text.Length - 2));
                    var key = item.RawText.Substring(1, item.Text.Length - 2);
                    //var value = myUnderlyingObject[key];// SmartFormat.Smart.Format(item.RawText, source);// item.Text.FormatWith_ExpandObject(source);
                    rs.Add(key);
                }
            }
            return rs;
        }
        public static Dictionary<string, object> GetPlaceHolderNameAndValue_ExpandObject(this string format, System.Dynamic.ExpandoObject source)
        {
            var rs = new Dictionary<string, object>();
            var detail = new SmartFormat.SmartFormatter();
            var parsingFormat = detail.Parser.ParseFormat(format);
            IDictionary<string, object> myUnderlyingObject = source;
            foreach (var item in parsingFormat.Items)
            {
                if (item is SmartFormat.Core.Parsing.Placeholder)
                {
                    //Console.WriteLine(item.Text.Substring(1, item.Text.Length - 2));
                    var key = item.RawText.Substring(1, item.Text.Length - 2);
                    var value = myUnderlyingObject[key];// SmartFormat.Smart.Format(item.RawText, source);// item.Text.FormatWith_ExpandObject(source);
                    if(!rs.ContainsKey(key))
                        rs.Add(key, value);
                }
            }
            return rs;
        }
        public static string FormatWith_ExpandObject(this string format, IFormatProvider provider, System.Dynamic.ExpandoObject source)

        {
            
            return SmartFormat.Smart.Format(format, source);
            

        }
        public static bool IsNumeric(this string str)

        {
            if (string.IsNullOrEmpty(str)) return false;
            if (string.IsNullOrWhiteSpace(str)) return false;
            //if (str.Contains("+")) return false;
            //if (str.Contains("-")) return false;
            //if (str.Contains("*")) return false;
            //if (str.Contains("/")) return false;
            if (str[0] == '0') return false;
            return IsDigitsOnly(str);
            //Regex regex = new Regex("^[0-9]*$");
            //return regex.IsMatch(str);
            decimal myNum = 0;
            //String testVar = "Not A Number";

            if (decimal.TryParse(str, out myNum))
            {
                // it is a number
                return true;
            }
            return false;
            //else
            //{
            //    // it is not a number
            //}
            //return FormatWith(format, null, source);

        }
        public static bool IsNumeric_(string str)

        {
            if (string.IsNullOrEmpty(str)) return false;
            if (string.IsNullOrWhiteSpace(str)) return false;
            //if (str.Contains("+")) return false;
            //if (str.Contains("-")) return false;
            //if (str.Contains("*")) return false;
            //if (str.Contains("/")) return false;
            if (str[0] == '0') return false;
            return IsDigitsOnly(str);
            //Regex regex = new Regex("^[0-9]*$");
            //return regex.IsMatch(str);
            decimal myNum = 0;
            //String testVar = "Not A Number";

            if (decimal.TryParse(str, out myNum))
            {
                // it is a number
                return true;
            }
            return false;
            //else
            //{
            //    // it is not a number
            //}
            //return FormatWith(format, null, source);

        }
        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static string ExceptionMessageDetails(this Exception ex, string customMessage)
        {
            return "Error Note: " + customMessage + Environment.NewLine +
                "Message: " + ex.Message + Environment.NewLine +
                "Stacktrace: " + ex.StackTrace;
        }
        /// <summary>
        /// Tìm kiếm các Placeholder trong chuỗi, Note: nên tổng hợp các Rule thành 1 chuỗi lớn rồi chạy hàm này (Distinct)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IEnumerable<string> FindPlaceHolder(this string str)
        {
            string pattern = @"\{(.*?)\}";//
            string query = str;// "Hallo {g:test1} asdasd {p:test1} sdfsdf{o:test1}";
            //query = "Hallo {test1} asdasd {test\\2} sdfsdf{test3}";

            var matches = Regex.Matches(query, pattern);
            var duplicatedHolders = new List<string>();
            foreach (Match m in matches)
            {
                //Console.WriteLine(m.Groups[1].Value);
                duplicatedHolders.Add(m.Groups[1].Value);
            }
            return duplicatedHolders.Distinct();
        }
        //public static bool IsNumeric(System.Dynamic.ExpandoObject object str)

        //{
        //    decimal myNum = 0;
        //    //String testVar = "Not A Number";

        //    if (decimal.TryParse(str.ToString(), out myNum))
        //    {
        //        // it is a number
        //        return true;
        //    }
        //    return false;
        //    //else
        //    //{
        //    //    // it is not a number
        //    //}
        //    //return FormatWith(format, null, source);

        //}
        #endregion
    }
}
