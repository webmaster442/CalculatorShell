using System.Globalization;

namespace CalculatorShell.Maths
{
    public struct Vector3 : ICalculatorType
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double Magnitude => Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3 vector)
            {
                return Equals(vector);
            }
            return false;
        }

        public bool Equals(Vector3 other)
        {
            return 
                X == other.X &&
                Y == other.Y &&
                Z == other.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public string ToString(IFormatProvider format)
        {
            return $"X: {X.ToString(format)}; Y: {Y.ToString(format)}; Z: {Z.ToString(format)}";
        }

        public override string? ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator +(Vector3 left, double right)
        {
            return new Vector3(left.X + right, left.Y + right, left.Z + right);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator -(Vector3 left, double right)
        {
            return new Vector3(left.X - right, left.Y - right, left.Z - right);
        }

        public static Vector3 operator *(Vector3 left, double right)
        {
            return new Vector3(left.X * right, left.Y * right, left.Z * right);
        }

        public static double operator *(Vector3 left, Vector3 right)
        {
            return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        }

        public static Vector3 operator /(Vector3 left, double right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }

        public static implicit operator Vector3(Vector2 value)
        {
            return new Vector3(value.X, value.Y, 0);
        }

        public static implicit operator double(Vector3 vector)
        {
            return vector.Magnitude;
        }
    }
}
