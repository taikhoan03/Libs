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
        public object ExecuteReader<T>(String command, bool CloseConnectionOnDone)
        {
            Database objDB = this;
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
                        {"InputType",""}
                    });
            else
                outputs = WorkflowInvoker.Invoke(wf,
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"SqlCommandString",command},
                        {"ConnectionInput",Conn},
                        {"IsCloseAfterExecute",CloseConnectionOnDone},
                        {"InputType",typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null)}
                    });
            var result = outputs["ListRecords"];
            //close Connection
            if (CloseConnectionOnDone)
                Conn.Close();
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
        //public String ConnectionString { get; set; }
        //private NpgsqlConnection _connection;
        //public NpgsqlConnection Connection
        //{
        //    get
        //    {
        //        return _connection;
        //    }
        //    private set
        //    {
        //        _connection = new NpgsqlConnection(this.ConnectionString);
        //    }
        //}
    }
}
