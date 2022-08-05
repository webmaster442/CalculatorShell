namespace CalculatorShell.Maths
{
    public static class DoubleWrappedLogic
    {
        private static double Do(double a, double b, Func<long, long, long> action)
        {
            if (!a.IsInteger() || !b.IsInteger())
            {
                throw new TypeException("Bitwise operations only supported on integer numbers");
            }

            return action.Invoke(a.ToInteger(), b.ToInteger());
        }

        public static double And(double a, double b) => Do(a, b, (ai, bi) => ai & bi);
        public static double Or(double a, double b) => Do(a, b, (ai, bi) => ai | bi);
        public static double Xor(double a, double b) => Do(a, b, (ai, bi) => ai ^ bi);
        public static double Shl(double a, double b) => Do(a, b, (ai, bi) => ai << (int)bi);
        public static double Shr(double a, double b) => Do(a, b, (ai, bi) => ai >> (int)bi);

        public static double Not(double a)
        {
            if (!a.IsInteger())
                throw new TypeException("Bitwise operations only supported on integer numbers");
            return ~a.ToInteger();
        }
    }
}
