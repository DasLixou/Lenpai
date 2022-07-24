using Lenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lenpai.NodeSystem.Nodes
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class IdentifierNode : INode
    {
        public NodeKind Kind => NodeKind.Identifier;
        public Symbol Symbol { get; init; }
        public ImmutableList<INode> Attrs { get; init; }
        public CodeRange Range { get; init; }
        public NodeStyle Style { get; init; }

        internal IdentifierNode(Symbol type, ImmutableList<INode> attrs, CodeRange range, NodeStyle style)
        {
            Symbol = type;
            Attrs = attrs;
            Range = range;
            Style = style;
        }

        public INode WithSymbol(Symbol symbol) => new IdentifierNode(symbol, Attrs, Range, Style);
        public INode WithAttrs(ImmutableList<INode> attrs) => new IdentifierNode(Symbol, attrs, Range, Style);
        public INode WithRange(CodeRange range) => new IdentifierNode(Symbol, Attrs, range, Style);
        public INode WithStyle(NodeStyle style) => new IdentifierNode(Symbol, Attrs, Range, style);

        public override string ToString()
        {
            NodePrinter.Style(out var builder, this, _ => _StyleNode(_));

            return builder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _StyleNode(StringBuilder builder)
        {
            builder.Append(Symbol);
        }
    }
}
