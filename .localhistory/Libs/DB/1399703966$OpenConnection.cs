using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{

    public sealed class OpenConnection : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> DBConnectionString { get; set; }
        //public InArgument<bool> IsConnected { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        public OutArgument<bool> OpenConnectionSuccess { get; set; }

        //private static NpgsqlConnection conn = new NpgsqlConnection(Crunch_Domain.Clore_DB.ConnectionHelper.ConnectionString.Replace("XpoProvider=Postgres;", String.Empty));
        //private static NpgsqlConnection connWithTrans = new NpgsqlConnection(Crunch_Domain.Clore_DB.ConnectionHelper.ConnectionString.Replace("XpoProvider=Postgres;", String.Empty) + "Enlist=true");
        private static NpgsqlConnection conn;//new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;");
        private static NpgsqlConnection connWithTrans = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=Clore_DB;Enlist=true");
        protected override void Execute(CodeActivityContext context)
        {
            string _DBConnectionString = context.GetValue(this.DBConnectionString);
            if(string.IsNullOrEmpty(_DBConnectionString)){

            }
            //Chỉ mở Connection nếu nó chưa mở hoặc bị Broken
            if (conn.FullState.Equals(System.Data.ConnectionState.Closed)
                || conn.FullState.Equals(System.Data.ConnectionState.Broken))
            {
                try
                {
                    conn.Open();
                    OpenConnectionSuccess.Set(context, true);
                }
                catch (Exception)
                {
                    OpenConnectionSuccess.Set(context, false);
                }
            }
        }
    }
}
