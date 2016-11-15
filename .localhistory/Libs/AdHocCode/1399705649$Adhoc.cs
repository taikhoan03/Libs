using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs.AdHocCode
{
    internal class Adhoc
    {
        public static void GetDocInPackage_Not_Planned()
        {

            NpgsqlCommand command = new NpgsqlCommand("select * FROM \"Document\" limit 10", DB.DBParameters.Conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            var package = new Crunch_DataObject.ManualPlan.CrunchPackage();
            while (dr.Read())
            {
                package = new Crunch_DataObject.ManualPlan.CrunchPackage();
                for (var i = 0; i < dr.FieldCount; i++)
                {
                    package.ID = (int)dr[0];
                    package.TotalDocs_Not_Planned = int.Parse(dr[1].ToString());
                    package.StateCode = dr[2].ToString();
                    package.CountyID = (int)dr[3];
                    package.DownloadDate = DateTime.Parse(dr[4].ToString());
                    package.DownloadFileID = (int)dr[5];
                    package.FilmID = dr[6].ToString();
                }
                Console.WriteLine();
                Console.Write("{0} \t", package);
                packages.Add(package);

            }
        }
    }
}
