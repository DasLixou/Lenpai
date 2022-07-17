using DasLenpai.CodeAnalysis;
using DasLenpai.NodeSystem.Nodes;

namespace DasLenpai.MacroProcessor.Macros
{
    public readonly struct LiteralMacro : IMacro
    {
        public MacroKind Kind => MacroKind.Literal;
        public Symbol Symbol { get; init; }
        public bool ReProcess { get; init; }
        public Func<INode, INode> Callback { get; init; }

        internal LiteralMacro(Symbol symbol, Func<INode, INode> callback, bool reProcess)
        {
            Symbol = symbol;
            Callback = callback;
            ReProcess = reProcess;
        }
    }
}
