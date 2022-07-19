using DasLenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace DasLenpai.NodeSystem.Nodes
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class CallNode : INode
    {
        public NodeKind Kind => NodeKind.Call;
        public Symbol Symbol { get; }
        public ImmutableList<INode> Args { get; }
        public ImmutableList<INode> Attrs { get; }
        public CodeRange Range { get; }
        public NodeStyle Style { get; }
        public INode Parent { get; }

        internal CallNode(Symbol symbol, ImmutableList<INode> args, ImmutableList<INode> attrs, CodeRange range, NodeStyle style, INode parent)
        {
            Symbol = symbol;
            Args = args.ConvertAll(_ => _.WithParent(this));
            Attrs = attrs.ConvertAll(_ => _.WithParent(this));
            Range = range;
            Style = style;
            Parent = parent;
        }

        public INode WithSymbol(Symbol symbol) => new CallNode(symbol, Args, Attrs, Range, Style, Parent);
        public INode WithArgs(ImmutableList<INode> args) => new CallNode(Symbol, args, Attrs, Range, Style, Parent);
        public INode WithAttrs(ImmutableList<INode> attrs) => new CallNode(Symbol, Args, attrs, Range, Style, Parent);
        public INode WithRange(CodeRange range) => new CallNode(Symbol, Args, Attrs, range, Style, Parent);
        public INode WithStyle(NodeStyle style) => new CallNode(Symbol, Args, Attrs, Range, style, Parent);
        public INode WithParent(INode parent) => new CallNode(Symbol, Args, Attrs, Range, Style, parent);

        public override string ToString()
        {
            NodePrinter.Style(out var builder, this, _ => _StyleNode(_));

            return builder.ToString();
        }

        private void _StyleNode(StringBuilder builder)
        {
            if(Style.HasFlag(NodeStyle.Block))
            {
                builder.AppendLine("{");
                foreach (var node in Args)
                {
                    builder.Append('\t');
                    builder.AppendLine(node.ToString());
                }
                builder.Append("}");
            }
            else if(Style.HasFlag(NodeStyle.BinaryOperator))
            {
                builder.Append(Args[0]);
                builder.Append(' ');
                builder.Append(Symbol);
                builder.Append(' ');
                builder.Append(Args[1]);
            }
            else if (Style.HasFlag(NodeStyle.UnaryOperator))
            {
                builder.Append(Args[0]);
                builder.Append(Symbol);
            }
            else
            {
                builder.Append(Symbol);
                builder.Append('(');
                builder.Append(string.Join(", ", Args));
                builder.Append(')');
            }
        }
    }
}
