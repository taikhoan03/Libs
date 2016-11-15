using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;
namespace TestAdhocLibs
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start flow:");
            Activity workflow1 = new Libs.DB.OpenDB();
            //WorkflowInvoker.Invoke(workflow1);
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(workflow1);
            //textBox2.Text = outputs["greeting"].ToString();
            var a = (bool)outputs["OpenConnectionSuccess"];
            Console.WriteLine("Inputed: " + a.ToString());
        }
    }
}
