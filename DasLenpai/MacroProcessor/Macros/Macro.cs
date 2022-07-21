using DasLenpai.CodeAnalysis;
using DasLenpai.NodeSystem.Nodes;

namespace DasLenpai.MacroProcessor.Macros
{
    public readonly struct Macro : IMacro
    {
        public MacroKind Kinds { get; init; }
        public Symbol Symbol { get; init; }
        public bool ReProcess { get; init; }
        public Func<INode, INode> Callback { get; init; }

        internal Macro(Symbol symbol, Func<INode, INode> callback, bool reProcess, MacroKind kinds)
        {
            Symbol = symbol;
            Callback = callback;
            ReProcess = reProcess;
            Kinds = kinds;
        }
    }
}
