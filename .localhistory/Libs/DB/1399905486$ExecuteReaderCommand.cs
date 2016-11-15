using System;
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
        public OutArgument<object> ListRecords { get; set; }
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
                ListRecords.Set(context, dr);
                var a = ListRecords.Get(context);

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
    public class ExecuteReaderCommand2
    {
        // Define an activity input argument of type string
        public string SqlCommandString { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        public object DataReader { get; set; }
        public object Execute<T>()
        {
            Console.Clear();
            // Obtain the runtime value of the Text input argument
            if (DB.DBParameters.Conn.FullState == System.Data.ConnectionState.Broken || DB.DBParameters.Conn.FullState == System.Data.ConnectionState.Closed)
                DB.DBParameters.Conn.Open();

            //string strCommand = SqlCommandString;
            NpgsqlCommand command = new NpgsqlCommand(SqlCommandString, DB.DBParameters.Conn);
            NpgsqlDataReader dr;
            try
            {
                dr = command.ExecuteReader();
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    var type = typeof(T);
                    var instanceOfClass = type.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                    var properties = type.GetProperties().ToList();
                    try
                    {
                        for (int fieldIndex = 0; fieldIndex < dr.FieldCount; fieldIndex++)
                        {
                            for (int icount = 0; icount < properties.Count; icount++)
                            {
                                var drname=dr.GetName(fieldIndex);
                                if (properties[icount].Name.ToLower() == drname.ToLower())
                                    properties[icount].SetValue(instanceOfClass, Convert.ChangeType(dr[fieldIndex].ToString(),properties[icount].PropertyType), null);
                            }
                            
                        }

                    }
                    catch (Exception)
                    {

                    }
                    
                    list.Add((T)instanceOfClass);
                    for (int field = 0; field < dr.FieldCount; field++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\t" + dr.GetName(field) + " ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(dr[field]);

                    }
                    Console.WriteLine("");
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("Can not Execute Command: " + strCommand);
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            finally
            {
                DB.DBParameters.Conn.Close();
            }
            return null;
        }
    }
}
