namespace CalculatorShell.Expressions.Internals.Logic
{
    internal class ImplicantRelationship
    {
        public Implicant A { get; set; }
        public Implicant B { get; set; }

        public ImplicantRelationship(Implicant first, Implicant second)
        {
            A = first;
            B = second;
        }
    }
}
