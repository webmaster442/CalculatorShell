using System.Globalization;

namespace CalculatorShell.Maths
{
    public struct Vector2 : ICalculatorType
    {
        public double X { get; }
        public double Y { get; }
        public double Magnitude => Math.Sqrt((X * X) + (Y * Y));

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vector2 vector)
            {
                return Equals(vector);
            }
            return false;
        }

        public bool Equals(Vector2 other)
        {
            return
              X == other.X &&
              Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public string ToString(IFormatProvider format)
        {
            return $"X: {X.ToString(format)}; Y: {Y.ToString(format)}";
        }

        public override string? ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator +(Vector2 left, double right)
        {
            return new Vector2(left.X + right, left.Y + right);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator -(Vector2 left, double right)
        {
            return new Vector2(left.X - right, left.Y - right);
        }

        public static Vector2 operator *(Vector2 left, double right)
        {
            return new Vector2(left.X * right, left.Y * right);
        }

        public static double operator *(Vector2 left, Vector2 right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        public static Vector2 operator /(Vector2 left, double right)
        {
            return new Vector2(left.X / right, left.Y / right);
        }

        public static implicit operator double(Vector2 vector)
        {
            return vector.Magnitude;
        }
    }
}
