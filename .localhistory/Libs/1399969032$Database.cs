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
        public NpgsqlCommand Connection
        {
            get
            {
                return new NpgsqlConnection(this.ConnectionString);
            }
        }
    }
}
