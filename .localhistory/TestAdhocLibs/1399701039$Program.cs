﻿using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace TestAdhocLibs
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start flow:");
            Activity workflow1 = new Workflow1();
            WorkflowInvoker.Invoke(workflow1);
        }
    }
}
