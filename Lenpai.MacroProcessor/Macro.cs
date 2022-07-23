using Lenpai.CodeAnalysis;
using Lenpai.NodeSystem.Nodes;
using System.Runtime.CompilerServices;

namespace Lenpai.MacroProcessor
{
    public static class Macro
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MacroProcessorBuilder Processor() => new MacroProcessorBuilder();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMacro Create(Symbol symbol, MacroKind kinds, Func<INode, INode> callback, bool reProcess = false) => new MacroImpl(symbol, callback, reProcess, kinds);
    }
}
