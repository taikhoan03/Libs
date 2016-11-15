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
            //Activity workflow1 = new Libs.AdHocFlow.GetAll();
            //WorkflowInvoker.Invoke(workflow1);
            //IDictionary<string, object> outputs = WorkflowInvoker.Invoke(new Workflow1());

            Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection("Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB_Dev;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;");
            Console.WriteLine("Enter command");
            var line=Console.ReadLine();
            Libs.RunExecuteReader.Execute(line);
            //Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB_Dev;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;
        }
    }
}
