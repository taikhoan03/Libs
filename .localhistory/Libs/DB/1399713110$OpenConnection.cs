using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{

    protected sealed class OpenConnection : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> DBConnectionString { get; set; }
        //public InArgument<bool> IsConnected { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        public OutArgument<bool> OpenConnectionSuccess { get; set; }

        
        protected override void Execute(CodeActivityContext context)
        {
            string _DBConnectionString = context.GetValue(this.DBConnectionString);
            if (DBParameters.Conn == null)
            {
                DBParameters.Conn = new NpgsqlConnection(_DBConnectionString);
            }
            //Chỉ mở Connection nếu nó chưa mở hoặc bị Broken
            if (DBParameters.Conn.FullState.Equals(System.Data.ConnectionState.Closed)
                || DBParameters.Conn.FullState.Equals(System.Data.ConnectionState.Broken))
            {
                try
                {
                    DBParameters.Conn.Open();
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
