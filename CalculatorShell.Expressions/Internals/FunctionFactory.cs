using CalculatorShell.Expressions.Internals.Expressions;
using CalculatorShell.Maths;
using System;
using System.Collections.Generic;
using System.Linq;

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
            { "abs", (child) => new Abs(child) },
            { "arcsin", (child) => new Function(child, Trigonometry.ArcSin, "arcsin") },
            { "arccos", (child) => new Function(child, Trigonometry.ArcCos, "arccos") },
            { "arctan", (child) => new Function(child, Trigonometry.ArcTan, "arctan") },
            { "arcctg", (child) => new Function(child, Trigonometry.ArcCtg, "arcctg") },
            { "deg2rad", (child) => new Function(child, DoubleFunctions.DegToRad, "deg2rad") },
            { "rad2deg", (child) => new Function(child, DoubleFunctions.RadToDeg, "rad2deg") },
            { "grad2deg", (child) => new Function(child, DoubleFunctions.GradToDeg, "grad2deg") },
            { "deg2grad", (child) => new Function(child, DoubleFunctions.DegToGrad, "deg2grad") },
            { "grad2rad", (child) => new Function(child, DoubleFunctions.GradToRad, "grad2rad") },
            { "rad2grad", (child) => new Function(child, DoubleFunctions.RadToGrad, "rad2grad") },
            { "floor", (child) => new Function(child, Math.Floor, "floor") },
            { "ceil", (child) => new Function(child, Math.Ceiling, "ceil") },
            { "sign", (child) => new Function(child, x => Math.Sign(x), "sign") },
            { "not", (child) => new Function(child, DoubleWrappedLogic.Not, "not") },
        };

        internal static IEnumerable<string> GetFunctionNames()
        {
            return SingleParamFunctions.Keys.Union(TwoParamFunctions.Keys);
        }

        private static readonly Dictionary<string, Func<IExpression?, IExpression?, IExpression>> TwoParamFunctions = new()
        {
            { "root", (child1, child2) => new Root(child1, child2) },
            { "log", (child1, child2) => new Log(child1, child2) },
            { "and", (child1, child2) => new Function2(child1, child2, DoubleWrappedLogic.And, "and") },
            { "or", (child1, child2) => new Function2(child1, child2, DoubleWrappedLogic.Or, "or") },
            { "xor", (child1, child2) => new Function2(child1, child2, DoubleWrappedLogic.Xor, "xor") },
            { "shl", (child1, child2) => new Function2(child1, child2, DoubleWrappedLogic.Xor, "shl") },
            { "shr", (child1, child2) => new Function2(child1, child2, DoubleWrappedLogic.Xor, "shr") },
        };

        public static int GetParameterCount(string identifier)
        {
            if (SingleParamFunctions.ContainsKey(identifier))
                return 1;
            else if (TwoParamFunctions.ContainsKey(identifier))
                return 2;
            else
                return -1;
        }

        internal static IExpression Create(string function, IExpression[] arguments)
        {
            if (arguments.Length == 1)
                return SingleParamFunctions[function](arguments[0]);
            else if (arguments.Length == 2)
                return TwoParamFunctions[function](arguments[0], arguments[1]);
            else
                throw new InvalidOperationException();
        }
    }
}
