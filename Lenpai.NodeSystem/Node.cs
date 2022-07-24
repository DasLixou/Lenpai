using Lenpai.CodeAnalysis;
using Lenpai.NodeSystem.Nodes;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Lenpai.NodeSystem
{
    public static class Node
    {
        public static readonly INode Missing = Identifier(Symbol.Missing);

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
        public static INode ListNode(params INode[] args)
            => new ListNode(ImmutableList.CreateRange(args));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static INode ListNode(ImmutableList<INode>? args)
            => new ListNode(args ?? ImmutableList<INode>.Empty);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<INode> List(params INode[] args) => ImmutableList.CreateRange(args);
    }
}
