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
        Console.WriteLine("Start flow:");
            //Activity workflow1 = new Libs.AdHocFlow.GetAll();
            //WorkflowInvoker.Invoke(workflow1);
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(new Workflow1());
    }
}
