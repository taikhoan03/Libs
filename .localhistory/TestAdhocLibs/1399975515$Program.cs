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
            //Dictionary<string, object> arguments = new Dictionary<string, object>();
            //arguments.Add("InputType", typeof(Test));
            //IDictionary<string, object> outputs = WorkflowInvoker.Invoke(new Workflow1());
            //var docs = (List<Test>)outputs["ListRecords"];

            runNormalClass();
            testXMLExt();
        }
        private static void runNormalClass()
        {
            string connectionString = "Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;";
            Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection(connectionString);
            //Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection("Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;");
            //Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection("Server=192.168.101.242;User Id=postgres;Password=@dminids@2013;Database=Clore_DB;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;");
            Console.WriteLine("Enter command");
            var line = "Select * from \"AdhocDoc\" limit 10";
            //var line = "Select * from \"Plan_Doc\" limit 1000";//Console.ReadLine();
            Libs.Database db = new Database();
            //db.ConnectionString = connectionString;
            //db.
            //Libs.RunExecuteReader.Execute(line);
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var a=db.ExecuteReader<Test>(line,true);
            Console.WriteLine("Times: "+watch.ElapsedMilliseconds.ToString()+" miliseconds");

            Console.WriteLine(((List<Test>)a).Count);
            watch.Stop();
            Console.ReadLine();
            line = "Select count(*) from \"Plan_Doc\"";
            var b = db.ExecuteReader<string>(line, true);
            Console.WriteLine("Count: "+b.ToString());
            
        }
        public static void testXMLExt()
        {
            Console.WriteLine("XML test");
            var test = new Test();
            test.ID=1;
            test.username="username "+test.ID;
            Console.WriteLine(test.XmlSerialize<Test>());
            var b = test.XMLStringToObject<Test>(test.XmlSerialize());

            var listTest = new List<Test>{
                test,test,test
            };
            Console.WriteLine(listTest.XmlSerialize<Test>());
            var c = test.XMLStringToListObject<Test>(listTest.XmlSerialize());
            var g = c;
        }
    }
}
