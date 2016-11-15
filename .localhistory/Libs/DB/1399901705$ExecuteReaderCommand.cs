﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{   
    public sealed class ExecuteReaderCommand : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> SqlCommandString { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        public OutArgument<object> DataReader { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Console.Clear();
            // Obtain the runtime value of the Text input argument
            string strCommand = context.GetValue(this.SqlCommandString);
            NpgsqlCommand command = new NpgsqlCommand(strCommand, DB.DBParameters.Conn);
            NpgsqlDataReader dr;
            try
            {
                dr = command.ExecuteReader();
                DataReader.Set(context, dr);
                var a = DataReader.Get(context);

            }
                
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can not Execute Command: " + strCommand);
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return;
            }
            
            var date = DateTime.Now;
            int i = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            while (dr.Read())
            {
                i++;
                Console.Write("ID: \t" + i);
                for (int field = 0; field < dr.FieldCount; field++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\t" + dr.GetName(field)+" ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(dr[field]);

                }
                Console.WriteLine("");

            }
            Console.ResetColor();
            Console.WriteLine("Timess: " + (DateTime.Now - date).Milliseconds);
        }
    }
    public sealed class ExecuteReaderCommand2
    {
        // Define an activity input argument of type string
        public string SqlCommandString { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        public object DataReader { get; set; }
        public object Execute()
        {
            Console.Clear();
            // Obtain the runtime value of the Text input argument
            Npgsql.NpgsqlConnection conn = new NpgsqlConnection(DB.DBParameters.Conn.ConnectionString);
            conn.Open();
            string strCommand = SqlCommandString;
            NpgsqlCommand command = new NpgsqlCommand(strCommand, DB.DBParameters.Conn);
            NpgsqlDataReader dr;
            try
            {
                dr = command.ExecuteReader();
                //DataReader = dr;
                return dr;
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can not Execute Command: " + strCommand);
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
    }
}
