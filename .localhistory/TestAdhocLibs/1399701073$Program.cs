using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
namespace TestAdhocLibs
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start flow:");
            Activity workflow1 = new Workflow1();
            //WorkflowInvoker.Invoke(workflow1);
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(workflow1);
            //textBox2.Text = outputs["greeting"].ToString();
            var a = outputs["Text"].ToString();
            Console.WriteLine("Inputed:" + a);
        }
    }
}
