namespace CalculatorShell.Expressions.Internals
{
    internal enum TokenType : uint
    {
        None = 0,
        Constant = 1,
        Variable = 2,
        Not = 4,
        Plus = 8,
        Minus = 16,
        Multiply = 32,
        Divide = 64,
        Mod = 128,
        And = 256,
        Or = 512,
        Exponent = 1024,
        Function1 = 2048,
        Function2 = 4096,
        ArgumentDivider = 8192,

        OpenParen = 16384,
        CloseParen = 32768,

        Eof = 2147483648
    }
}
