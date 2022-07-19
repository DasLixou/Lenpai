using DasLenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        internal IdentifierNode(Symbol type, ImmutableList<INode> attrs, CodeRange range, NodeStyle style, INode parent, bool convertParents)
        {
            Symbol = type;
            Attrs = convertParents ? attrs.ConvertAll(_ => _.WithParent(this)) : attrs;
            Range = range;
            Style = style;
            Parent = parent;
        }

        public INode WithSymbol(Symbol symbol) => new IdentifierNode(symbol, Attrs, Range, Style, Parent, false);
        public INode WithAttrs(ImmutableList<INode> attrs) => _Attrs(attrs.ConvertAll(_ => _.WithParent(this)));
        public INode PlusAttrs(ImmutableList<INode> attrs) => _Attrs(Attrs.AddRange(attrs.ConvertAll(_ => _.WithParent(this))));
        public INode PlusAttr(INode attr) => _Attrs(Attrs.Add(attr.WithParent(this)));
        public INode WithRange(CodeRange range) => new IdentifierNode(Symbol, Attrs, range, Style, Parent, false);
        public INode WithStyle(NodeStyle style) => new IdentifierNode(Symbol, Attrs, Range, style, Parent, false);
        public INode WithParent(INode parent) => new IdentifierNode(Symbol, Attrs, Range, Style, parent, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private INode _Attrs(ImmutableList<INode> attrs) => new IdentifierNode(Symbol, attrs, Range, Style, Parent, false);

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
