using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;

namespace Libs
{
    public class RunExecuteReader:System.Activities.Activity;
    {
        public static void Execute(String command){
            Activity workflow1 = new DB.OpenConnection();
            WorkflowInvoker.Invoke(workflow1,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"aName","Argument Demo"}
                });
        }
    }
}
