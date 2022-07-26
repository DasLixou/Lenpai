namespace Lenpai.NodeSystem
{
    [Flags]
    public enum NodeStyle : byte
    {
        Default =           0b10000000,
        Statement =         0b10000001,
        Block =             0b10000010,
        BinaryOperator =    0b00000101,
        UnaryOperator =     0b00000110,

        Paren =             0b10001000,

        Binary =            0b00010101,
        Hex =               0b00010110,

        SingleQuote =       0b00011001,
        DoubleQuote =       0b00011010,
    }
}
