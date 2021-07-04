using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Expressions.Internals.Logic
{
    internal class Implicant : IEquatable<Implicant?>
    {
        public string Mask { get; set; } //number mask.
        public List<int> Minterms { get; }

        public Implicant()
        {
            Mask = string.Empty;
            Minterms = new List<int>(); //original integers in group.
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Implicant);
        }

        public bool Equals(Implicant? other)
        {
            return other != null &&
                   Mask == other.Mask &&
                   Minterms.SequenceEqual(other.Minterms);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Mask, Minterms);
        }

    }
}
