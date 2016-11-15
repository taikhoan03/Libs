using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
namespace ActivityLibrary1
{
    public sealed class ReadLine : NativeActivity
    {
        public OutArgument<string> Text { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {
            
            // Obtain the runtime value of the Text input argument
            string text = Console.ReadLine();
            //Console.WriteLine("Inputed: " + this.Text.Get(context));
            Text.Set(context, text);
        }
    }
}
