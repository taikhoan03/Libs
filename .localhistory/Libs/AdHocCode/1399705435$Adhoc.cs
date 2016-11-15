using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs.AdHocCode
{
    internal class Adhoc
    {
        public static List<Crunch_DataObject.ManualPlan.CrunchPackage> GetDocInPackage_Not_Planned()
        {
            
            NpgsqlCommand command = new NpgsqlCommand("select myPackage.\"ID\",groupPackage.\"Total\",myPackage.state_code,myPackage.\"county_ID\",myPackage.download_date,\"fileID\", \"film_tracker_ID\"  from (select \"fileID\", count(*) as \"Total\" FROM \"Document\" WHERE \"statusID\"=19 group by \"fileID\") groupPackage INNER JOIN \"Download_File\" myPackage on myPackage.\"ID\"=groupPackage.\"fileID\"", DB.DBParameters.Conn);
            var packages = new List<Crunch_DataObject.ManualPlan.CrunchPackage>();
            try
            {

                //serverversion = (String)command.ExecuteScalar();
                //Console.WriteLine("PostgreSQL server version: {0}", serverversion);

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


            finally
            {

                conn.Close();
            }
            return packages;
        }
    }
}
