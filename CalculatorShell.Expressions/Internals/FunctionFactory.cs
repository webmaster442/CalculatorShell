using CalculatorShell.Expressions.Internals.Expressions;
using System;
using System.Collections.Generic;

namespace CalculatorShell.Expressions.Internals
{
    internal static class FunctionFactory
    {
        private static readonly Dictionary<string, Func<IExpression?, IExpression>> SingleParamFunctions = new()
        {
            { "ln", (child) => new Ln(child) },
            { "sin", (child) => new Sin(child) },
            { "cos", (child) => new Cos(child) },
            { "tan", (child) => new Tan(child) },
        };

        private static readonly Dictionary<string, Func<IExpression?, IExpression?, IExpression>> TwoParamFunctions = new()
        {
            { "root", (child1, child2) => new Root(child1, child2) },
            { "log", (child1, child2) => new Log(child1, child2) },
        };

        public static bool IsSignleParamFunction(string identifier)
        {
            return SingleParamFunctions.ContainsKey(identifier);
        }

        public static bool IsTwoParamFunction(string identifier)
        {
            return TwoParamFunctions.ContainsKey(identifier);
        }

        internal static IExpression Create(string function, IExpression? exp)
        {
            return SingleParamFunctions[function](exp);
        }

        internal static IExpression Create(string function, IExpression? exp1, IExpression? exp2)
        {
            return TwoParamFunctions[function](exp1, exp2);
        }
    }
}
