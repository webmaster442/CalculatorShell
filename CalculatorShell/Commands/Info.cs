namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Info : Eval
    {
        public override void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(1);

            INumber? result = EvaluateExpression(arguments, out IExpression parsed);
            output.WriteLine("Number type: {0}", result.NumberType);
            switch (result.NumberType)
            {
                case Expressions.NumberType.Double:
                    PrintDoubleInfo(result.GetDouble(), output);
                    break;
            }
            Memory.SetExpression("$ans", parsed);
        }

        private string Encode(byte[] bytes)
        {
            return string.Join(' ', bytes.Select(x => Convert.ToString(x, 16)));
        }

        private void PrintDoubleInfo(double v, ICommandConsole output)
        {
            output.WriteLine("Value: {0}", v);
            output.WriteLine("bytes representation: {0}", Encode(BitConverter.GetBytes(v)));
            if (v.IsInteger())
            {
                Console.WriteLine("-".PadRight(60, '-'));
                int bits = GetBits(v);
                output.WriteLine("Bits:        {0}", bits);
                output.WriteLine("Binary Form: {0}", Represent(v, 2, bits));
                output.WriteLine("Hex Form:    {0}", Represent(v, 16, bits));
                output.WriteLine("Octal Form:  {0}", Represent(v, 8, bits));
            }

        }

        private string Represent(double v, int system, int bits)
        {
            string representation = Convert.ToString(Convert.ToInt64(v), system);
            return representation.PadRight(bits);
        }

        private static int GetBits(double v)
        {
            v = Math.Abs(v);
            return v switch
            {
                < byte.MaxValue => 8,
                < ushort.MaxValue => 16,
                < uint.MaxValue => 32,
                < ulong.MaxValue => 64,
                _ => 64
            };
        }
    }
}
