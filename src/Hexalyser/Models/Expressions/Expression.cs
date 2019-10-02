using System;

namespace Hexalyser.Models.Expressions
{
    public class Expression
    {
        public string Text { get; set; }

        public Expression(string text)
        {
            Text = text;
        }

        public int Evaluate()
        {
            // TODO: Build an expression evaluator here
            return Int32.Parse(Text);
        }
    }
}
