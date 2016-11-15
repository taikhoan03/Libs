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
            NpgsqlCommand command = new NpgsqlCommand("select * FROM \"Document\" limit 100", DB.DBParameters.Conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            //var package = new Crunch_DataObject.ManualPlan.CrunchPackage();
            while (dr.Read())
            {
                Console.Write("{0}\t{1} \n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            }
        }
    }
}
