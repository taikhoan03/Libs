using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.AdHocCode
{
    public sealed class GetAll : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            NpgsqlCommand command = new NpgsqlCommand("select * FROM \"Plan_Doc\" limit 1000000", DB.DBParameters.Conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            //var package = new Crunch_DataObject.ManualPlan.CrunchPackage();
            var date = DateTime.Now;
            int i = 0;
            
            while (dr.Read())
            {
                i++;
                Console.Write("ID: \t" + i);
                for (int field = 0; field < dr.FieldCount; field++)
                {
                    
                }
                Console.Write("ID: \t{6}\t{0}\t{1}\t{2}\t{3}\t{4}\t{5} \n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5],i);
                
            }
            Console.WriteLine("Timess: "+(DateTime.Now - date).Milliseconds);
        }
    }
}
