namespace DasLenpai.NodeSystem
{
    [Flags]
    public enum NodeStyle : byte
    {
        Default =           0b00000000,
        Statement =         0b00000001,
        Block =             0b00000010,
        BinaryOperator =    0b00000100,
        UnaryOperator =     0b00001000,

        Binary =            0b00010000,
        Hex =               0b00100000,

        Paren =             0b01000000,
    }
}
