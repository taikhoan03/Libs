﻿using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;
using System.Reflection;
namespace Libs
{
    
    public static class ExtMethods//:System.Activities.Activity
    {
        public static object ExecuteReader<T>(this Database obj, String command)
        {
            Activity wf = new DB.OpenConnection();
            WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"DBConnectionString",DB.DBParameters.Conn.ConnectionString}
                });

            wf = new DB.ExecuteReaderCommand();
            IDictionary<string, object> outputs=WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"SqlCommandString",command}
                });

            wf = new DB.CloseConnection();
            WorkflowInvoker.Invoke(wf);


            var result = (Npgsql.NpgsqlDataReader)outputs["DataReader"];
            if (result == null)
                return null;
            var type = typeof(T);
            return result;
        }
        private static void merge(object dbObject, object DestinationClassObject)
        {
            //reinit some properties
            Npgsql.NpgsqlDataReader dr = (Npgsql.NpgsqlDataReader)dbObject;
            Type type = DestinationClassObject.GetType();
            List<PropertyInfo> properties = type.GetProperties(BindingFlags.Public).ToList();

            Dictionary<string, string> flatDBField = new Dictionary<string, string>();
            while (dr.Read())
            {
                for (int field = 0; field < dr.FieldCount; field++)
                {
                    flatDBField.Add(dr.GetName(field), dr[field]);
                }

            }

            foreach (PropertyInfo property in properties)
            {
                Field fieldItem = XmlTabFields.FindItemByName(xml, property.Name);
                if (fieldItem == null) continue;
                if (String.IsNullOrEmpty(fieldItem.Value)) continue;
                property.SetValue(result, Convert.ChangeType(fieldItem.Value.ToUpper(), property.PropertyType), null);


            }
        }
    }
}
