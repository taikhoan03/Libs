using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs
{
    public class Database
    {
        public String ConnectionString { get; set; }
        private NpgsqlConnection _connection;
        public NpgsqlConnection Connection
        {
            get
            {
                return new NpgsqlConnection(this.ConnectionString);
            }
            private set
            {

            }
        }
    }
}
