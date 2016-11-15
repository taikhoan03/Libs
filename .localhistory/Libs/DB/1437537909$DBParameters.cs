using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs.DB
{
    public static class DBParameters
    {
        public static NpgsqlConnection Conn;//new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;");
        public static NpgsqlConnection ConnWithTrans;// = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;Enlist=true");
    }
}
