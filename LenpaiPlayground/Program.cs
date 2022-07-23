using Lenpai.CodeAnalysis;
using Lenpai.MacroProcessor;
using Lenpai.NodeSystem;

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
        var node = Node.Call(Symbols.PlusEquals, Node.List(
            Node.Identifier("sheee"), Node.Literal(12, Symbols.UInt), Node.ListNode(Node.Missing)
            ), style: NodeStyle.BinaryOperator);

        var isPlusEquals = node switch
        {
            ("'+=", _, _, _) => true,
            _ => false
        };

        var proBuilder = Macro.Processor();
        proBuilder.AddMacro(Macro.Create(Symbols.PlusEquals, MacroKind.Call | MacroKind.Literal, (node) =>
        {
            return Node.Call(Symbols.Equalss,
                Node.List(node.Args[0],
                    Node.Call(Symbols.Plus, Node.List(node.Args[0], node.Args[1]), style: NodeStyle.BinaryOperator)
                ),
                style: NodeStyle.BinaryOperator);
        }));
        var processor = proBuilder.ToProcessor();

        var newNode = processor.Process(node);

        var a = 1;
    }
}