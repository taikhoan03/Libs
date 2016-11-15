using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;

namespace Libs
{
    public static class RunExecuteReader//:System.Activities.Activity
    {
        public static T Execute<T>(this T i,String command){
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
            return i;
        }
    }
}
