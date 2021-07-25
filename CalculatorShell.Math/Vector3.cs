using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculatorShell.Maths
{
    public sealed class Vector3 : IEquatable<Vector3?>, ICalculatorType
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as Vector3);
        }

        public bool Equals(Vector3? other)
        {
            return other != null &&
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

        public static bool operator ==(Vector3? left, Vector3? right)
        {
            return EqualityComparer<Vector3>.Default.Equals(left, right);
        }

        public static bool operator !=(Vector3? left, Vector3? right)
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

        public static implicit operator Vector3 (Vector2 value)
        {
            return new Vector3(value.X, value.Y, 0);
        }

        public static implicit operator double(Vector3 vector)
        {
            return vector.Magnitude;
        }
    }
}
