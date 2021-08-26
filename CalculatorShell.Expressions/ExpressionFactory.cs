using CalculatorShell.Expressions.Internals;
using CalculatorShell.Expressions.Internals.Expressions;
using CalculatorShell.Expressions.Internals.Logic;
using CalculatorShell.Expressions.Properties;
using CalculatorShell.Maths;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Provides methods to parse string expressions
    /// </summary>
    public static class ExpressionFactory
    {
        private static CultureInfo _culture = CultureInfo.InvariantCulture;
        private static Tokenizer? _tokenizer;
        private static Token _currentToken;
        private static IVariables? _variables;

        private static readonly TokenSet FirstFactor = new(TokenType.Function, TokenType.Variable, TokenType.OpenParen);
        private static readonly TokenSet FirstFactorPrefix = FirstFactor + TokenType.Constant;
        private static readonly TokenSet FirstUnaryExp = FirstFactorPrefix + TokenType.Minus + TokenType.Not;
        private static readonly TokenSet FirstMultExp = new(FirstUnaryExp);
        private static readonly TokenSet FirstExpExp = new(FirstUnaryExp);

        /// <summary>
        /// Current angle mode. Affects Trigonometric and complex functions
        /// </summary>
        public static AngleMode CurrentAngleMode
        {
            get => Trigonometry.AngleMode;
            set => Trigonometry.AngleMode = value;
        }

        /// <summary>
        /// Collection of knwon functions
        /// </summary>
        public static IEnumerable<string> KnownFunctions
        {
            get => FunctionFactory.GetFunctionNames();
        }

        /// <summary>
        /// parse a string expression
        /// </summary>
        /// <param name="function">expression to parse</param>
        /// <param name="variables">variables to use</param>
        /// <param name="culture">culture to use for parsing</param>
        /// <returns>Parsed expression</returns>
        public static IExpression Parse(string function, IVariables variables, CultureInfo culture)
        {
            if (function == null)
            {
                function = string.Empty;
            }

            _culture = culture;
            _tokenizer = new Tokenizer(function.ToLower(culture), culture);
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

        /// <summary>
        /// Parse a given set of minterms or maxterms to a logic expression
        /// </summary>
        /// <param name="terms">Collection of terms that are cared</param>
        /// <param name="dontcareTerms">Collection of terms that don't count</param>
        /// <param name="options">parsing options</param>
        /// <returns>A parsed expression</returns>
        public static IExpression ParseLogic(IEnumerable<int> terms, 
                                             IEnumerable<int>? dontcareTerms, 
                                             ParseLogicOptions options)
        {
            IEnumerable<int> notCared = Enumerable.Empty<int>();
            if (dontcareTerms != null)
                notCared = dontcareTerms;


            string logic = QuineMcclusky.GetSimplified(terms, 
                                                       notCared, 
                                                       Utilities.GetVariableCount(terms, notCared, options.MinVariableCount),
                                                       new QuineMcCluskeyConfig
                                                       {
                                                           AIsLsb = options.LsbDirection == Lsb.AisLsb,
                                                           HazardFree = options.GenerateHazardFree,
                                                           Negate = options.TermKind == TermKind.Maxterm,
                                                       });

            if (options.Variables == null)
                throw new ArgumentNullException(nameof(options.Variables));

            if (string.IsNullOrEmpty(logic))
                logic = "false";

            return Parse(logic, options.Variables, options.Culture);
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

        private static IExpression ParseAddExpression()
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

        private static IExpression ParseMultExpression()
        {
            if (Check(FirstExpExp))
            {
                var exp = ParseExpExpression();

                while (Check(new TokenSet(TokenType.Multiply, TokenType.Divide, TokenType.And, TokenType.Mod)))
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

                        case TokenType.Mod:
                            exp = new Mod(exp, right);
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

        private static IExpression ParseExpExpression()
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

        private static IExpression ParseUnaryExpression()
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

        private static IExpression ParseFactorPrefix()
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

        private static IExpression ParseFactor()
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

                    case TokenType.Function:
                        right = ParseFunction();
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

            if (exp != null)
                return exp;

            throw new ExpressionEngineException(Resources.UnexpectedTokenInFactior, _currentToken.Type);
        }

        private static IExpression ParseFunction()
        {
            var opType = _currentToken.Type;
            var function = _currentToken.Value;

            Eat(opType);
            Eat(TokenType.OpenParen);

            int argumentCount = FunctionFactory.GetParameterCount(function);

            if (opType != TokenType.Function
                || argumentCount <= 0)
            {
                throw new ExpressionEngineException(Resources.UnknownFuction, function);
            }

            IExpression[] arguments = new IExpression[argumentCount];

            for (int i=0; i<argumentCount; i++)
            {
                arguments[i] = ParseAddExpression();
                if (i < (argumentCount - 1))
                {
                    Eat(TokenType.ArgumentDivider);
                }
            }
            Eat(TokenType.CloseParen);

            return FunctionFactory.Create(function, arguments);
        }
    }
}
