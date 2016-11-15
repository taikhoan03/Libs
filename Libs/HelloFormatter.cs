using SmartFormat.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libs
{
    public class HelloFormatter : IFormatter
    {
        private string[] names = new[] { "hello", "hi" };
        public string[] Names { get { return names; } set { this.names = value; } }

        public bool TryEvaluateFormat(IFormattingInfo formattingInfo)
        {
            var iCanHandleThisInput = formattingInfo.CurrentValue is bool;
            if (!iCanHandleThisInput)
                return false;

            formattingInfo.Write("HELLO ");
            if ((bool)formattingInfo.CurrentValue)
                formattingInfo.Write(formattingInfo.FormatterOptions);
            else
                formattingInfo.Write(formattingInfo.Format.GetLiteralText());

            return true;
        }
    }
    public class SubstringFormatter : IFormatter
    {
        private string[] names = new[] { "Substring" };
        public string[] Names { get { return names; } set { this.names = value; } }

        public bool TryEvaluateFormat(IFormattingInfo formattingInfo)
        {
            //var iCanHandleThisInput = formattingInfo.CurrentValue is bool;
            //if (!iCanHandleThisInput)
            //    return false;
            var options = formattingInfo.FormatterOptions.Split(new char[] { ',' });
            var index = int.Parse(options[0]);
            var length= int.Parse(options[1]);
            formattingInfo.Write(formattingInfo.CurrentValue.ToString().ToUpper().Substring(index,length));
            //if ((bool)formattingInfo.CurrentValue)
            //    formattingInfo.Write(formattingInfo.FormatterOptions);
            //else
            //    formattingInfo.Write(formattingInfo.Format.GetLiteralText());

            return true;
        }
    }
    public class LeftFormatter : IFormatter
    {
        private string[] names = new[] { "Left" };
        public string[] Names { get { return names; } set { this.names = value; } }

        public bool TryEvaluateFormat(IFormattingInfo formattingInfo)
        {
            //var iCanHandleThisInput = formattingInfo.CurrentValue is bool;
            //if (!iCanHandleThisInput)
            //    return false;
            //var options = formattingInfo.FormatterOptions.Split(new char[] { ',' });
            var length = int.Parse(formattingInfo.FormatterOptions);
            //var length = int.Parse(options[1]);
            formattingInfo.Write(formattingInfo.CurrentValue.ToString().Substring(0, length));
            //if ((bool)formattingInfo.CurrentValue)
            //    formattingInfo.Write(formattingInfo.FormatterOptions);
            //else
            //    formattingInfo.Write(formattingInfo.Format.GetLiteralText());

            return true;
        }
    }
}
