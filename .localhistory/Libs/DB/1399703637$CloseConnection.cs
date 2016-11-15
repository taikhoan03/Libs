using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{

    public sealed class CloseConnection : CodeActivity
    {
        
        private static NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;");
        private static NpgsqlConnection connWithTrans = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;Enlist=true");
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                conn.Close();
                Console.WriteLine("CloseConnection: Closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CloseConnection: " + ex.Message);
            }
        }
    }
}
