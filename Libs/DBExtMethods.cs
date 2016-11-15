using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
namespace Libs
{
    public class DBExtMethods
    {
        //public object ExecuteReader<T>(this Database objDB, String command, bool CloseConnectionOnDone)
        //{
        //    Activity wf = new DB.OpenConnection();
        //    WorkflowInvoker.Invoke(wf,
        //        new System.Collections.Generic.Dictionary<string, object>
        //        {
        //            {"DBConnectionString",objDB.Conn.ConnectionString}
        //        });

        //    wf = new DB.ExecuteReaderCommand();
        //    IDictionary<string, object> outputs = null;

        //    //basic type
        //    if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
        //        outputs = WorkflowInvoker.Invoke(wf,
        //            new System.Collections.Generic.Dictionary<string, object>
        //            {
        //                {"SqlCommandString",command},
        //                {"InputType",""}
        //            });
        //    else
        //        outputs = WorkflowInvoker.Invoke(wf,
        //            new System.Collections.Generic.Dictionary<string, object>
        //            {
        //                {"SqlCommandString",command},
        //                {"InputType",typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null)}
        //            });
        //    var result = outputs["ListRecords"];
        //    //close Connection
        //    if (CloseConnectionOnDone)
        //        objDB.Conn.Close();
        //    //Simple type
        //    if (result.GetType() == typeof(T))
        //        return result;
        //    else
        //    {
                
        //        var tmpList = (List<object>)result;
        //        var listresult = new List<T>();
        //        for (int i = 0; i < tmpList.Count; i++)
        //        {
        //            listresult.Add((T)tmpList[i]);
        //        }
        //        return listresult;
        //    }
        //}
    }
}
