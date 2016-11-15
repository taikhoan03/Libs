﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Libs
{
    public static class SerializationExtensions
    {

        //public static T XMLStringToListObject<T>(this string serialized)
        //{
            
        //    return Deserialize<T>(serialized);
        //}
        
        //public static string XmlSerialize<T>(this T obj)
        //{
        //    return Serialize<T>(obj);
        //}
        public static string Serialize<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var stm = new XmlTextWriter(writer))
            {
                stm.Formatting = Formatting.Indented;
                serializer.WriteObject(stm, obj);
                return writer.ToString();
            }
        }
        public static T Deserialize<T>(this string serialized)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(serialized))
            using (var stm = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(stm);
            }
        }
    }
}
