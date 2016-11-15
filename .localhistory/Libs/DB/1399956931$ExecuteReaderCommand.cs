using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{   
    /// <summary>
    /// Not work yet
    /// </summary>
    public sealed class ExecuteReaderCommand : CodeActivity<object>
    {
        // Define an activity input argument of type string
        public InArgument<string> SqlCommandString { get; set; }
        public InArgument<object> InputType { get; set; }

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
            
            var watch=new System.Diagnostics.Stopwatch();
            watch.Start();
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
            Console.WriteLine("Timess: " + watch.ElapsedMilliseconds);
            watch.Stop();

        }
    }
    #region backup
    //public sealed class ExecuteReaderCommand : CodeActivity
    //{
    //    // Define an activity input argument of type string
    //    public InArgument<string> SqlCommandString { get; set; }

    //    // If your activity returns a value, derive from CodeActivity<TResult>
    //    // and return the value from the Execute method.
    //    public OutArgument<object> ListRecords { get; set; }
    //    protected override void Execute(CodeActivityContext context)
    //    {
    //        Console.Clear();
    //        // Obtain the runtime value of the Text input argument
    //        string strCommand = context.GetValue(this.SqlCommandString);
    //        NpgsqlCommand command = new NpgsqlCommand(strCommand, DB.DBParameters.Conn);
    //        NpgsqlDataReader dr;
    //        try
    //        {
    //            dr = command.ExecuteReader();
    //            ListRecords.Set(context, dr);
    //            var a = ListRecords.Get(context);

    //        }
                
    //        catch (Exception ex)
    //        {
    //            Console.ForegroundColor = ConsoleColor.Red;
    //            Console.WriteLine("Can not Execute Command: " + strCommand);
    //            Console.WriteLine(ex.Message);
    //            Console.ResetColor();
    //            return;
    //        }
            
    //        var watch=new System.Diagnostics.Stopwatch();
    //        watch.Start();
    //        int i = 0;
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        while (dr.Read())
    //        {
    //            i++;
    //            Console.Write("ID: \t" + i);
    //            for (int field = 0; field < dr.FieldCount; field++)
    //            {
    //                Console.ForegroundColor = ConsoleColor.Green;
    //                Console.Write("\t" + dr.GetName(field)+" ");
    //                Console.ForegroundColor = ConsoleColor.Cyan;
    //                Console.Write(dr[field]);

    //            }
    //            Console.WriteLine("");

    //        }
    //        Console.ResetColor();
    //        Console.WriteLine("Timess: " + watch.ElapsedMilliseconds);
    //        watch.Stop();

    //    }
    //}
    #endregion
    /// <summary>
    /// Normal Class
    /// </summary>
    public static class ExecuteReaderCommand2
    {
        // Define an activity input argument of type string
        //public string SqlCommandString { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        //public object DataReader { get; set; }
        public static object ExecuteReaderTable<T>(String SqlCommandString,bool CloseConnectionOnDone)
        {
            // Obtain the runtime value of the Text input argument
            if (DB.DBParameters.Conn.FullState == System.Data.ConnectionState.Broken || DB.DBParameters.Conn.FullState == System.Data.ConnectionState.Closed)
                DB.DBParameters.Conn.Open();

            //string strCommand = SqlCommandString;
            NpgsqlCommand command = new NpgsqlCommand(SqlCommandString, DB.DBParameters.Conn);
            NpgsqlDataReader dr;
            List<T> list=null;
            try
            {
                dr = command.ExecuteReader();
                list = new List<T>();
                var type = typeof(T);

                var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();
                int fieldCount = dr.FieldCount;
                //int propertyCount = properties.Count;
                #region Filter
                List<System.Reflection.PropertyInfo> propertiesFilter=new List<System.Reflection.PropertyInfo>();
                for (int icount = 0; icount < properties.Count; icount++)
                {
                    if(properties[icount].CanWrite && properties[icount].CanRead)
                        propertiesFilter.Add(properties[icount]);
                }
                int propertyFilterCount = propertiesFilter.Count;
                #endregion
                while (dr.Read())
                {
                    if (type.GetConstructor(Type.EmptyTypes) == null)
                    {
                        return Convert.ChangeType(dr[0].ToString(), typeof(T));
                    }
                    var instanceOfClass = type.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                    try
                    {
                        for (int fieldIndex = 0; fieldIndex < fieldCount; fieldIndex++)
                        {
                            for (int icount = 0; icount < propertyFilterCount; icount++)
                            {
                                var drname=dr.GetName(fieldIndex);
                                if (propertiesFilter[icount].Name.ToLower() == drname.ToLower())
                                    propertiesFilter[icount].SetValue(instanceOfClass, Convert.ChangeType(dr[fieldIndex].ToString(),properties[icount].PropertyType), null);
                            }
                            
                        }

                    }
                    catch (Exception)
                    {

                    }
                    
                    list.Add((T)instanceOfClass);
                    
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can not Execute Command: " + SqlCommandString);
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if(CloseConnectionOnDone)
                    DB.DBParameters.Conn.Close();
            }
            return list;
        }
    }
}
