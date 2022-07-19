using DasLenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace DasLenpai.NodeSystem.Nodes
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class IdentifierNode : INode
    {
        public NodeKind Kind => NodeKind.Identifier;
        public Symbol Symbol { get; }
        public ImmutableList<INode> Attrs { get; }
        public CodeRange Range { get; }
        public NodeStyle Style { get; }
        public INode Parent { get; }

        internal IdentifierNode(Symbol type, ImmutableList<INode> attrs, CodeRange range, NodeStyle style, INode parent)
        {
            Symbol = type;
            Attrs = attrs.ConvertAll(_ => _.WithParent(this));
            Range = range;
            Style = style;
            Parent = parent;
        }

        public INode WithSymbol(Symbol symbol) => new IdentifierNode(symbol, Attrs, Range, Style, Parent);
        public INode WithAttrs(ImmutableList<INode> attrs) => new IdentifierNode(Symbol, attrs, Range, Style, Parent);
        public INode WithRange(CodeRange range) => new IdentifierNode(Symbol, Attrs, range, Style, Parent);
        public INode WithStyle(NodeStyle style) => new IdentifierNode(Symbol, Attrs, Range, style, Parent);
        public INode WithParent(INode parent) => new IdentifierNode(Symbol, Attrs, Range, Style, parent);

        public override string ToString()
        {
            NodePrinter.Style(out var builder, this, _ => _StyleNode(_));

            return builder.ToString();
        }

        private void _StyleNode(StringBuilder builder)
        {
            builder.Append(Symbol);
        }
    }
}
