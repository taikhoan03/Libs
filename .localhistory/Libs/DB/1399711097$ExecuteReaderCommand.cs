using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{

    public sealed class ExecuteReaderCommand : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> SqlCommandString { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            Console.Clear();
            // Obtain the runtime value of the Text input argument
            string strCommand = context.GetValue(this.SqlCommandString);
            NpgsqlCommand command = new NpgsqlCommand(strCommand, DB.DBParameters.Conn);
            NpgsqlDataReader dr;
            try
            {
                dr = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not Execute Command: " + strCommand);
                Console.WriteLine(ex.Message);
                return;
            }

            //var package = new Crunch_DataObject.ManualPlan.CrunchPackage();
            var date = DateTime.Now;
            int i = 0;

            while (dr.Read())
            {
                i++;
                Console.Write("ID: \t" + i);
                for (int field = 0; field < dr.FieldCount; field++)
                {
                    Console.Write("\t" + dr[field]);
                }
                //Console.Write("ID: \t{6}\t{0}\t{1}\t{2}\t{3}\t{4}\t{5} \n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5],i);
                Console.WriteLine("");

            }
            Console.WriteLine("Timess: " + (DateTime.Now - date).Milliseconds);
        }
    }
}
