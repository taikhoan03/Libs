using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;

namespace Libs
{
    
    public static class ExtMethods//:System.Activities.Activity
    {
        public static object ExecuteReader<T>(this T obj, String command)
        {
            Activity wf = new Database.OpenConnection();
            WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"DBConnectionString",Database.DBParameters.Conn.ConnectionString}
                });

            wf = new Database.ExecuteReaderCommand();
            IDictionary<string, object> outputs=WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"SqlCommandString",command}
                });

            wf = new Database.CloseConnection();
            WorkflowInvoker.Invoke(wf);


            var result = (Npgsql.NpgsqlDataReader)outputs["DataReader"];
            var type = typeof(T);
            return result;
        }
    }
}
