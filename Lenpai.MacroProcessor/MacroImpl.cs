using Lenpai.CodeAnalysis;
using Lenpai.NodeSystem.Nodes;

namespace Lenpai.MacroProcessor
{
    public readonly struct MacroImpl : IMacro
    {
        public MacroKind Kinds { get; init; }
        public Symbol Symbol { get; init; }
        public bool ReProcess { get; init; }
        public Func<INode, INode> Callback { get; init; }

        internal MacroImpl(Symbol symbol, Func<INode, INode> callback, bool reProcess, MacroKind kinds)
        {
            Symbol = symbol;
            Callback = callback;
            ReProcess = reProcess;
            Kinds = kinds;
        }
    }
}
