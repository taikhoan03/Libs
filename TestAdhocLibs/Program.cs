using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;
using System.Xml.Serialization;
using SmartFormat;
using System.Text.RegularExpressions;
//using log4net;
//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TestAdhocLibs
{

    class Program
    {
        //private static readonly ILog Log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );
        static void Main(string[] args)
        {
            var str = "Hello";
            var a = str[0];
            var b = "10".IsNumeric();
            var c = "10a".IsNumeric();
            var b0123 = "0123".IsNumeric();
            var d = "9995000263".IsNumeric();
            var detail = new SmartFormat.SmartFormatter();
            var parsingFormat=detail.Parser.ParseFormat("{abc} hello");
            var test1 = new SmartFormat.Core.Parsing.Placeholder(parsingFormat,0,2);
            foreach (var item in parsingFormat.Items)
            {
                if(item is SmartFormat.Core.Parsing.Placeholder)
                {
                    Console.WriteLine(item.Text.Substring(1,item.Text.Length-2));
                }
            }
            var obj = new System.Dynamic.ExpandoObject();
            IDictionary<string, object> myUnderlyingObject = obj;
            myUnderlyingObject.Add("abc", "fjdslkfj");
            myUnderlyingObject.Add("Address", "2103         152ND                     AVE  NE 98007");
            //obj. = "fjdskl";
            //var test2 = "{abc} hello".GetPlaceHolderName_ExpandObject(obj);
            //var test3 = "{Address}+2".GetPlaceHolderName_ExpandObject(obj);
            
            //test1.Selectors.
            //parsingFormat.
            //string pattern = @"\{(?:#:)(.*?)\}";//
            string pattern = @"\{(.*?)\}";//
            string query = "Hallo {g:test1} asdasd {p:test1} sdfsdf{o:test1}";
            query = "Hallo {test1} asdasd {test\\2} sdfsdf{test3}";

            var matches = Regex.Matches(query, pattern);

            foreach (Match m in matches)
            {
                Console.WriteLine(m.Groups[1].Value);
            }

            var cc = "Hallo {test1} asdasd {test\\2} sdfsdf{test3}{test3}{test1}{test3}".FindPlaceHolder();

            Console.WriteLine("Start flow:");
            //Activity workflow1 = new Libs.AdHocFlow.GetAll();
            //WorkflowInvoker.Invoke(workflow1);
            //Dictionary<string, object> arguments = new Dictionary<string, object>();
            //arguments.Add("InputType", typeof(Test));
            //IDictionary<string, object> outputs = WorkflowInvoker.Invoke(new Workflow1());
            //var docs = (List<Test>)outputs["ListRecords"];
            var isNumeric = "123.432".IsNumeric();
            //var a = new A();
            //a.name = "name";
            //a.ee = "ee";
            //a.setaddress("address");
            var xml = a.XmlSerialize();
            runNormalClass();
            //testXMLExt();
            //object c=null;
            //try
            //{
            //    var a = c.ToString();
            //    throw new NullReferenceException();
            //}
            //catch (Exception ex)
            //{
                
            //    Log.Debug("gggg");
            //}
            
        }
        public static void addFormater()
        {
            Smart.Default.AddExtensions(new SubstringFormatter());
            
        }
        private static void runNormalClass()
        {
            //string connectionString = "Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;";
            //Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection(connectionString);
            ////Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection("Server=localhost;User Id=postgres;Password=123456;Database=Clore_DB;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;");
            ////Libs.DB.DBParameters.Conn = new Npgsql.NpgsqlConnection("Server=192.168.101.242;User Id=postgres;Password=@dminids@2013;Database=Clore_DB;Encoding=UNICODE;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=1024;");
            //Console.WriteLine("Enter command");
            //var line = "select * from \"Document\" where \"statusID\"='99'";
            ////var line = "Select * from \"Plan_Doc\" limit 1000";//Console.ReadLine();
            //Libs.Database db = new Database();
            ////db.ConnectionString = connectionString;
            ////db.
            ////Libs.RunExecuteReader.Execute(line);
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //watch.Start();
            //var a=db.ExecuteReader<Crunch_DataObject.Document>(line,true);
            //Console.WriteLine("Times: "+watch.ElapsedMilliseconds.ToString()+" miliseconds");

            //Console.WriteLine(((List<Crunch_DataObject.Document>)a).Count);
            //watch.Stop();
            //Console.ReadLine();
            //line = "Select count(*) from \"Plan_Doc\"";
            //var b = db.ExecuteReader<string>(line, true);
            //Console.WriteLine("Count: "+b.ToString());
            
        }
        public static void testXMLExt()
        {
            Console.WriteLine("XML test");
            var test = new Test();
            test.ID=1;
            test.username="username "+test.ID;
            Console.WriteLine(test.XmlSerialize<Test>());
            var b = test.XmlSerialize().XMLStringToObject<Test>();

            var listTest = new List<Test>{
                test,test,test
            };
            Console.WriteLine(listTest.XmlSerialize<Test>());
            var c = listTest.XmlSerialize().XMLStringToListObject<Test>();
            var g = c;
        }

    }
    [Serializable]
    public class A
    {
        public string name;
        private string address;
        [XmlIgnore]
        public string ee;

        public void setaddress(string add)
        {
            address = add;
        }
    }
}
