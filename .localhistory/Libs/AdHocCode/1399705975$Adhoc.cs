using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.AdHocCode
{
    public sealed class Adhoc : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            GetAll();
        }
        private static void GetAll()
        {

            NpgsqlCommand command = new NpgsqlCommand("select * FROM \"Document\" limit 10", DB.DBParameters.Conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            //var package = new Crunch_DataObject.ManualPlan.CrunchPackage();
            while (dr.Read())
            {
                Console.Write("{0}\t{1} \n", dr[0], dr[1]);
            }
        }
    }
}
