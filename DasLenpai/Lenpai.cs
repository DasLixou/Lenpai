using DasLenpai.CodeAnalysis;
using DasLenpai.MacroProcessor;
using DasLenpai.MacroProcessor.Macros;
using DasLenpai.NodeSystem;
using DasLenpai.NodeSystem.Nodes;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace DasLenpai
{
    public static class Lenpai
    {
        // Node System
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static INode Call(Symbol? symbol = null, ImmutableList<INode>? args = null, ImmutableList<INode>? attrs = null, CodeRange? range = null, NodeStyle style = NodeStyle.Default)
            => new CallNode(symbol ?? Symbol.Missing, args ?? ImmutableList<INode>.Empty, attrs ?? ImmutableList<INode>.Empty, range ?? CodeRange.Missing, style);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static INode Literal(object? value, Symbol? type = null, ImmutableList<INode>? attrs = null, CodeRange? range = null, NodeStyle style = NodeStyle.Default)
            => new LiteralNode(value, type ?? Symbol.Missing, attrs ?? ImmutableList<INode>.Empty, range ?? CodeRange.Missing, style);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static INode Identifier(Symbol symbol, ImmutableList<INode>? attrs = null, CodeRange? range = null, NodeStyle style = NodeStyle.Default)
            => new IdentifierNode(symbol, attrs ?? ImmutableList<INode>.Empty, range ?? CodeRange.Missing, style);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<INode> List(params INode[] args) => ImmutableList.CreateRange(args);


        // Macro Processor
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MacroProcessorBuilder MacroProcessor() => new MacroProcessorBuilder();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMacro CallMacro(Symbol symbol, Func<INode, INode> callback, bool reProcess) => new CallMacro(symbol, callback, reProcess);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMacro LiteralMacro(Symbol symbol, Func<INode, INode> callback, bool reProcess) => new LiteralMacro(symbol, callback, reProcess);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMacro IdentifierMacro(Symbol symbol, Func<INode, INode> callback, bool reProcess) => new IdentifierMacro(symbol, callback, reProcess);
    }
}