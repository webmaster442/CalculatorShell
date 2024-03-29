﻿using CalculatorShell.Expressions.Properties;
using System.Globalization;
using System.Text;

namespace CalculatorShell.Expressions.Internals
{
    internal class Tokenizer
    {
        private readonly string _function;
        private readonly char[] _specialTokens = new[]
        {
            ':', '_', '.', ',', '[', ']'
        };
        private int _index;
        private readonly CultureInfo _culture;

        public Tokenizer(string function, CultureInfo culture)
        {
            if (function == null)
            {
                function = string.Empty;
            }
            _function = function;
            _index = 0;
            _culture = culture;
        }

        public Token Next()
        {
            while (_index < _function.Length)
            {
                if (IsAllowedToken(_function[_index]))
                {
                    return HandleStrings();
                }
                switch (_function[_index++])
                {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        continue;

                    case '|':
                        return new Token("|", TokenType.Or);
                    case '&':
                        return new Token("&", TokenType.And);
                    case '!':
                        return new Token("!", TokenType.Not);
                    case '+':
                        return new Token("+", TokenType.Plus);
                    case '-':
                        return new Token("-", TokenType.Minus);
                    case '*':
                        return new Token("*", TokenType.Multiply);
                    case '/':
                        return new Token("/", TokenType.Divide);
                    case '%':
                        return new Token("%", TokenType.Mod);
                    case '^':
                        return new Token("^", TokenType.Exponent);
                    case '(':
                        return new Token("(", TokenType.OpenParen);
                    case ')':
                        return new Token(")", TokenType.CloseParen);
                    case ';':
                        return new Token(";", TokenType.ArgumentDivider);
                    default:
                        throw new ExpressionEngineException(Resources.InvalidToken);
                }
            }
            return new Token(string.Empty, TokenType.Eof);
        }

        private Token HandleStrings()
        {
            StringBuilder? temp = new StringBuilder();
            while (_index < _function.Length &&
                IsAllowedToken(_function[_index]))
            {
                temp.Append(_function[_index++]);
            }

            string? identifier = temp.ToString();

            if (NumberParser.TryParse(identifier, out NumberImplementation number, _culture))
            {
                return new Token(identifier, TokenType.Constant);
            }
            else if (FunctionFactory.GetParameterCount(identifier) > 0)
                return new Token(identifier, TokenType.Function);
            else if (VariableNameChecker.IsValidVariableName(identifier))
                return new Token(identifier, TokenType.Variable);
            else
                throw new ExpressionEngineException(Resources.InvalidToken);

        }

        private bool IsAllowedToken(char c)
        {
            bool isText = c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');
            bool isNumber = c is >= '0' and <= '9';
            return isText || isNumber || _specialTokens.Contains(c);
        }
    }
}
