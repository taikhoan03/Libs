﻿using System;
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
            //Activity workflow1 = new Libs.AdHocFlow.GetAll();
            //WorkflowInvoker.Invoke(workflow1);
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(new Workflow1());
            //textBox2.Text = outputs["greeting"].ToString();
            //var a = (bool)outputs["OpenConnectionSuccess"];
            //Console.WriteLine("Inputed: " + a.ToString());

            //Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB_Dev;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;
        }
    }
}
