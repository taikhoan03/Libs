using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;
namespace Libs
{
    public class RunExecuteReader
    {
        //Console.WriteLine("Start flow:");
        Activity workflow1 = new Libs.DB.OpenConnection();
        WorkflowInvoker.Invoke(workflow1,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"aName","Argument Demo"}
                });
    }
}
