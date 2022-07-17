using DasLenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace DasLenpai.NodeSystem.Nodes
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class LiteralNode : INode
    {
        public NodeKind Kind => NodeKind.Literal;
        public object? Value { get; }
        public Symbol Symbol { get; }
        public ImmutableList<INode> Attrs { get; }
        public CodeRange Range { get; }
        public NodeStyle Style { get; }

        internal LiteralNode(object? value, Symbol type, ImmutableList<INode> attrs, CodeRange range, NodeStyle style)
        {
            Value = value;
            Symbol = type;
            Attrs = attrs;
            Range = range;
            Style = style;
        }

        public INode WithSymbol(Symbol symbol) => new LiteralNode(Value, symbol, Attrs, Range, Style);
        public INode WithValue(object? value) => new LiteralNode(value, Symbol, Attrs, Range, Style);
        public INode WithAttrs(ImmutableList<INode> attrs) => new LiteralNode(Value, Symbol, attrs, Range, Style);
        public INode WithRange(CodeRange range) => new LiteralNode(Value, Symbol, Attrs, range, Style);
        public INode WithStyle(NodeStyle style) => new LiteralNode(Value, Symbol, Attrs, Range, style);

        public override string ToString()
        {
            NodePrinter.Style(out var builder, this, _ => _StyleNode(_));

            return builder.ToString();
        }

        private void _StyleNode(StringBuilder builder)
        {
            if (Symbol == Symbol.Missing)
            {
                _Value(builder);
            }
            else
            {
                builder.Append(Symbol.ToString());
                builder.Append('(');
                _Value(builder);
                builder.Append(')');
            }
        }

        private void _Value(StringBuilder builder)
        {
            if(Value == null)
            {
                builder.Append("null");
            } else if(Style.HasFlag(NodeStyle.Binary))
            {
                builder.Append("0b");
                builder.Append(Convert.ToString((long)Convert.ChangeType(Value, typeof(long)), 2));
            } else if (Style.HasFlag(NodeStyle.Hex))
            {
                builder.Append("0x");
                builder.Append(Convert.ToString((long)Convert.ChangeType(Value, typeof(long)), 16));
            } else
            {
                builder.Append(Value.ToString());
            }
        }
    }
}
