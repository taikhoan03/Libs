using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Npgsql;
namespace Libs.DB
{

    public sealed class DB : CodeActivity
    {
        // Define an activity input argument of type string
        //public InArgument<string> Text { get; set; }
        public InArgument<bool> IsConnected { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        public OutArgument<bool> OpenConnectionSuccess { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            bool IsConnected = context.GetValue(this.IsConnected);
        }
    }
}
