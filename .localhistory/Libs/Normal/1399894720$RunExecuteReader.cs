using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;
using System.Reflection;
namespace Libs
{
    public class Test
    {
        public int ID { get; set; }
    }
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

            //var instanceOfClass = default(T);
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var parameters = new object[0];
            var instanceOfClass = constructor.Invoke(parameters);
            return merge(result, instanceOfClass);
            //return instanceOfClass;
        }
        private static object merge(object dbObject, object DestinationClassObject)
        {
            //reinit some properties
            Npgsql.NpgsqlDataReader dr = (Npgsql.NpgsqlDataReader)dbObject;
            Dictionary<string, string> flatDBField = new Dictionary<string, string>();
            while (dr.Read())
            {
                for (int field = 0; field < dr.FieldCount; field++)
                {
                    flatDBField.Add(dr.GetName(field), dr[field].ToString());
                }

            }
            Type type = DestinationClassObject.GetType();
            List<PropertyInfo> properties = type.GetProperties(BindingFlags.Public).ToList();

            
            for (int i = 0; i < properties.Count; i++)
            {
                if (!String.IsNullOrEmpty(flatDBField[properties[i].Name]))
                {
                    properties[i].SetValue(DestinationClassObject,
                        flatDBField[properties[i].Name]
                        , null);
                }
            }
            return DestinationClassObject;
        }
    }
}
