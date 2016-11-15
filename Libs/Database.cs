using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs
{
    public class Database
    {
        public NpgsqlConnection Conn;
        public object ExecuteReader<T>(String command, bool CloseConnectionOnDone, CommandType CType=CommandType.Command,NpgsqlParameter[] parameters=null,int timeout=30)
        {
            Database objDB = this;
            if (objDB.Conn == null)
                throw new Exception("Please create Connection(NpgsqlConnection)");
            //Activity wf = new DB.OpenConnection();
            //WorkflowInvoker.Invoke(wf,
            //    new System.Collections.Generic.Dictionary<string, object>
            //    {
            //        {"DBConnectionString",objDB.Conn.ConnectionString}
            //    });
            //Conn.Open();
            Activity wf = new DB.ExecuteReaderCommand();
            IDictionary<string, object> outputs = null;

            //basic type
            if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
                outputs = WorkflowInvoker.Invoke(wf,
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"SqlCommandString",command},
                        {"ConnectionInput",Conn},
                        {"IsCloseAfterExecute",CloseConnectionOnDone},
                        {"Command_Type",CType},
                        {"Parameters",parameters},
                        {"Timeout",timeout},
                        {"InputType",""}
                    });
            else
                outputs = WorkflowInvoker.Invoke(wf,
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"SqlCommandString",command},
                        {"ConnectionInput",Conn},
                        {"IsCloseAfterExecute",CloseConnectionOnDone},
                        {"Command_Type",CType},
                        {"Parameters",parameters},
                        {"Timeout",timeout},
                        {"InputType",typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null)}
                    });
            var result = outputs["ListRecords"];
            //close Connection
            //if (CloseConnectionOnDone)
            //    Conn.Close();
            //Simple type
            if (result.GetType() == typeof(T))
                return result;
            else
            {
                
                var tmpList = (List<object>)result;
                var listresult = new List<T>();
                for (int i = 0; i < tmpList.Count; i++)
                {
                    listresult.Add((T)tmpList[i]);
                }
                return listresult;
            }
        }

        
    }
    public enum CommandType
    {
        Command=0,
        StoredProcedure=1
    }
}
