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
        Function = 2048,
        ArgumentDivider = 4096,

        OpenParen = 8192,
        CloseParen = 16384,

        Eof = 2147483648
    }
}
