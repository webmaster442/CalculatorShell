namespace CalculatorShell.Maths
{
    public static class FractionConverter
    {
        private static double CalculateError(Fraction f, double number)
        {
            return number - f.ToDouble();
        }

        public static (Fraction fraction, double error) ConvertToFraction(double number, int digits = 10)
        {
            Fraction f = Fraction.ToFraction(number, digits);
            double error = CalculateError(f, number);

            return (f, error);
        }

        public static (double number, double error) ConvertToDouble(Fraction f)
        {
            double number = f.ToDouble();
            Fraction reparsed = Fraction.ToFraction(number);
            double error = CalculateError(reparsed, number);
            return (number, error);
        }
    }
}
