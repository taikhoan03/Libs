﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
namespace Libs.DB
{
    protected static class DBParameters
    {
        private static NpgsqlConnection conn;//new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;");
        private static NpgsqlConnection connWithTrans;// = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;Enlist=true");
    }
}
