namespace CalculatorShell.Expressions.Internals
{
    internal struct TokenSet
    {
        private readonly uint _tokens;

        public TokenSet(params TokenType[] tokens)
        {
            _tokens = 0;
            foreach (var token in tokens)
            {
                _tokens |= (uint)token;
            }
        }

        public TokenSet(TokenSet set)
        {
            _tokens = set._tokens;
        }

        private TokenSet(uint tokens)
        {
            _tokens = tokens;
        }

        public static TokenSet operator +(TokenSet t1, TokenType t2)
        {
            return new TokenSet(t1._tokens | (uint)t2);
        }

        public bool Contains(TokenType type)
        {
            return (_tokens & (uint)type) != 0;
        }
    }
}
