using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs
{
    public static class Database
    {
        public static String ConnectionString { get; set; }
        private static NpgsqlConnection _connection;
        public static NpgsqlConnection Connection
        {
            get
            {
                return _connection;
            }
            private set
            {
                _connection = new NpgsqlConnection(ConnectionString);
            }
        }
    }
}
