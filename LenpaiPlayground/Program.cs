using DasLenpai;
using DasLenpai.CodeAnalysis;
using DasLenpai.MacroProcessor;
using DasLenpai.NodeSystem;

public static class Symbols
{
    public static readonly Symbol Plus = new Symbol("'+");
    public static readonly Symbol Equalss = new Symbol("'=");
    public static readonly Symbol PlusEquals = new Symbol("'+=");
    public static readonly Symbol UInt = new Symbol("!u32");
    public static readonly Symbol String = new Symbol("!str");
    public static readonly Symbol Escaped = new Symbol("#escaped");
    public static readonly Symbol EntryPoint = new Symbol("#entry");
}

public static class Playground
{
    public static void Main()
    {
        var node = Lenpai.Call(Symbols.PlusEquals, Lenpai.List(
            Lenpai.Identifier("sheee"), Lenpai.Literal(12, Symbols.UInt), Lenpai.ListNode(Lenpai.Missing)
            ), style: NodeStyle.BinaryOperator);

        var proBuilder = Lenpai.MacroProcessor();
        proBuilder.AddMacro(Lenpai.Macro(Symbols.PlusEquals, MacroKind.Call | MacroKind.Literal, (node) =>
        {
            return Lenpai.Call(Symbols.Equalss, 
                Lenpai.List(node.Args[0], 
                    Lenpai.Call(Symbols.Plus, Lenpai.List(node.Args[0], node.Args[1]), style: NodeStyle.BinaryOperator)
                ),
                style: NodeStyle.BinaryOperator);
        }));
        var processor = proBuilder.ToProcessor();

        var newNode = processor.Process(node);

        var a = 1;
    }
}