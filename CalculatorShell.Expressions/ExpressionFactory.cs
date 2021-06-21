using CalculatorShell.Expressions.Internals;
using CalculatorShell.Expressions.Internals.Expressions;
using CalculatorShell.Expressions.Properties;
using CalculatorShell.Maths;
using System;
using System.Globalization;
using System.Text;

namespace CalculatorShell.Expressions
{
    public static class ExpressionFactory
    {
        private static CultureInfo _culture = CultureInfo.InvariantCulture;
        private static Tokenizer? _tokenizer;
        private static Token _currentToken;
        private static IVariables? _variables;

        private static readonly TokenSet FirstFactor = new(TokenType.Function1, TokenType.Function2, TokenType.Variable, TokenType.OpenParen);
        private static readonly TokenSet FirstFactorPrefix = FirstFactor + TokenType.Constant;
        private static readonly TokenSet FirstUnaryExp = FirstFactorPrefix + TokenType.Minus + TokenType.Not;
        private static readonly TokenSet FirstMultExp = new(FirstUnaryExp);
        private static readonly TokenSet FirstExpExp = new(FirstUnaryExp);

        public static AngleMode CurrentAngleMode
        {
            get => Trigonometry.AngleMode;
            set => Trigonometry.AngleMode = value;
        }

        public static IExpression? Parse(string function, IVariables variables, CultureInfo? culture)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));

            _culture = culture;   
            _tokenizer = new Tokenizer(function, culture);
            _currentToken = new Token("", TokenType.None);
            _variables = variables;

            if (!Next())
            {
                throw new ExpressionEngineException(Resources.EmptyFunction);
            }
            var exp = ParseAddExpression();
            var leftover = new StringBuilder();
            while (_currentToken.Type != TokenType.Eof)
            {
                leftover.Append(_currentToken.Value);
                Next();
            }
            if (leftover.Length > 0)
            {
                throw new ExpressionEngineException(Resources.TrailingChars);
            }
            return exp;
        }

        private static bool Next()
        {
            if (_currentToken.Type == TokenType.Eof)
            {
                throw new ExpressionEngineException(Resources.OutOfTokens);
            }
            _currentToken = _tokenizer?.Next() ?? new Token(string.Empty, TokenType.None);

            return _currentToken.Type != TokenType.Eof;
        }

        private static bool Check(TokenSet tokens) => tokens.Contains(_currentToken.Type);

        private static void Eat(TokenType type)
        {
            if (_currentToken.Type != type)
            {
                throw new ExpressionEngineException(Resources.MissingToken, type);
            }
            Next();
        }

        private static IExpression? ParseAddExpression()
        {
            if (Check(FirstMultExp))
            {
                var exp = ParseMultExpression();

                while (Check(new TokenSet(TokenType.Plus, TokenType.Minus, TokenType.Or)))
                {
                    var opType = _currentToken.Type;
                    Eat(opType);
                    if (!Check(FirstMultExp))
                    {
                        throw new ExpressionEngineException(Resources.ExpectedExpression);
                    }
                    var right = ParseMultExpression();

                    switch (opType)
                    {
                        case TokenType.Plus:
                            exp = new Add(exp, right);
                            break;

                        case TokenType.Or:
                            exp = new Or(exp, right);
                            break;

                        case TokenType.Minus:
                            exp = new Subtract(exp, right);
                            break;

                        default:
                            throw new ExpressionEngineException(Resources.ExpectedPlussminus, opType);
                    }
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private static IExpression? ParseMultExpression()
        {
            if (Check(FirstExpExp))
            {
                var exp = ParseExpExpression();

                while (Check(new TokenSet(TokenType.Multiply, TokenType.Divide, TokenType.And)))
                {
                    var opType = _currentToken.Type;
                    Eat(opType);
                    if (!Check(FirstExpExp))
                    {
                        throw new ExpressionEngineException(Resources.ExpectedExpressionAfterMultDiv);
                    }
                    var right = ParseExpExpression();

                    switch (opType)
                    {
                        case TokenType.Multiply:
                            exp = new Multiply(exp, right);
                            break;

                        case TokenType.Divide:
                            exp = new Divide(exp, right);
                            break;

                        case TokenType.And:
                            exp = new And(exp, right);
                            break;

                        default:
                            throw new ExpressionEngineException(Resources.ExpectedMultiplyDivide, opType);
                    }
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private static IExpression? ParseExpExpression()
        {
            if (Check(FirstUnaryExp))
            {
                var exp = ParseUnaryExpression();

                if (Check(new TokenSet(TokenType.Exponent)))
                {
                    var opType = _currentToken.Type;
                    Eat(opType);
                    if (!Check(FirstUnaryExp))
                    {
                        throw new ExpressionEngineException(Resources.ExpectedExpressionAfterExponent);
                    }
                    var right = ParseUnaryExpression();

                    switch (opType)
                    {
                        case TokenType.Exponent:
                            exp = new Exponent(exp, right);
                            break;

                        default:
                            throw new ExpressionEngineException(Resources.ExpectedExponent, opType);
                    }
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private static IExpression? ParseUnaryExpression()
        {
            var negate = false;
            var not = false;
            if (_currentToken.Type == TokenType.Minus)
            {
                Eat(TokenType.Minus);
                negate = true;
            }
            if (_currentToken.Type == TokenType.Not)
            {
                Eat(TokenType.Not);
                not = true;
            }

            if (Check(FirstFactorPrefix))
            {
                var exp = ParseFactorPrefix();

                if (negate)
                {
                    return new Negate(exp);
                }
                else if (not)
                {
                    return new Not(exp);
                }

                return exp;
            }
            throw new ExpressionEngineException(Resources.InvalidExpression);
        }

        private static IExpression? ParseFactorPrefix()
        {
            IExpression? exp = null;
            if (_currentToken.Type == TokenType.Constant)
            {
                bool succes = NumberParser.TryParse(_currentToken.Value, out var result, _culture);
                if (!succes)
                {
                    throw new ExpressionEngineException(Resources.InvalidExpression);
                }
                exp = new Constant(result);
                Eat(TokenType.Constant);
            }

            if (Check(FirstFactor))
            {
                if (exp == null)
                {
                    return ParseFactor();
                }
                return new Multiply(exp, ParseFactor());
            }
            // This should be impossible because bad symbols are caught by Tokenizer,
            //  constants would have been parsed in the if-statement above, and
            //  anything else is treated as a Factor (UndefinedVariableException
            //  will be thrown when you try to evaluate the function).
            if (exp == null)
            {
                throw new ExpressionEngineException(Resources.InvalidExpression);
            }
            return exp;
        }

        private static IExpression? ParseFactor()
        {
            IExpression? exp = null;
            do
            {
                IExpression? right = null;
                switch (_currentToken.Type)
                {
                    case TokenType.Variable:

                        if (_variables == null)
                            throw new ExpressionEngineException(Resources.NoVariables);

                        right = new Variable(_currentToken.Value, _variables);
                        Eat(TokenType.Variable);
                        break;

                    case TokenType.Function1:
                        right = ParseFunction();
                        break;
                    case TokenType.Function2:
                        right = ParseFunction2();
                        break;

                    case TokenType.OpenParen:
                        Eat(TokenType.OpenParen);
                        right = ParseAddExpression();
                        Eat(TokenType.CloseParen);
                        break;

                    default:
                        throw new ExpressionEngineException(Resources.UnexpectedTokenInFactior, _currentToken.Type);
                }

                exp = (exp == null) ? right : new Multiply(exp, right);
            }
            while (Check(FirstFactor));

            return exp;
        }

        private static IExpression? ParseFunction()
        {
            var opType = _currentToken.Type;
            var function = _currentToken.Value;
            Eat(opType);
            Eat(TokenType.OpenParen);
            var exp = ParseAddExpression();
            Eat(TokenType.CloseParen);

            if (opType == TokenType.Function1
                && FunctionFactory.IsSignleParamFunction(function))
            {
                return FunctionFactory.Create(function, exp);
            }
            else
            {
                throw new ExpressionEngineException(Resources.UnknownFuction, function);
            }
        }

        private static IExpression? ParseFunction2()
        {
            var opType = _currentToken.Type;
            var function = _currentToken.Value;
            Eat(opType);
            Eat(TokenType.OpenParen);
            var exp1 = ParseAddExpression();
            Eat(TokenType.ArgumentDivider);
            var exp2 = ParseAddExpression();
            Eat(TokenType.CloseParen);

            if (opType == TokenType.Function2
                && FunctionFactory.IsTwoParamFunction(function))
            {
                return FunctionFactory.Create(function, exp1, exp2);
            }
            else
            {
                throw new ExpressionEngineException(Resources.UnknownFuction, function);
            }
        }
    }
}
