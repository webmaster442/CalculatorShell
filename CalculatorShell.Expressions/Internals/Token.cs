﻿namespace CalculatorShell.Expressions.Internals
{
    internal struct Token
    {
        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }

        public string Value { get; }
        public TokenType Type { get; }
    }
}
