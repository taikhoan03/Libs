﻿using System;
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
        public Test() { }
        public int ID { get; set; }
        public string username { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class ExtMethods
    {
        public static object ExecuteReader<T>(this Database obj, String command, bool CloseConnectionOnDone)
        {
            Activity wf = new DB.OpenConnection();
            WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"DBConnectionString",DB.DBParameters.Conn.ConnectionString}
                });

            wf = new DB.ExecuteReaderCommand();
            IDictionary<string, object> outputs=null;
            
            //basic type
            if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
                outputs = WorkflowInvoker.Invoke(wf,
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"SqlCommandString",command},
                        {"InputType",""}
                    });
            else
                outputs = WorkflowInvoker.Invoke(wf,
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"SqlCommandString",command},
                        {"InputType",typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null)}
                    });
            var result = outputs["ListRecords"];
            //close Connection
            if (CloseConnectionOnDone)
                DB.DBParameters.Conn.Close();
            //Simple type
            if (result.GetType() == typeof(T))
                return result;
            else
            {
                var tmpList = (List<object>)result;
                var listresult=new List<T>();
                for (int i = 0; i < tmpList.Count; i++)
			    {
                    listresult.Add((T)tmpList[i]);
			    }
                return listresult;
            }
            return result;
        }
        
    }
}
