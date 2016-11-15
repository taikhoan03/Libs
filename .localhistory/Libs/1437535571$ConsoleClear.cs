using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Libs
{

    public sealed class ConsoleClear : CodeActivity
    {
        
        protected override void Execute(CodeActivityContext context)
        {
            Console.Clear();
        }
    }
}
