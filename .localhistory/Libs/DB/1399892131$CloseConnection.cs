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
        
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                if(Database.DBParameters.Conn!=null)
                    Database.DBParameters.Conn.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
