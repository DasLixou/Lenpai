using Lenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lenpai.NodeSystem.Nodes
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

        internal CallNode(Symbol symbol, ImmutableList<INode> args, ImmutableList<INode> attrs, CodeRange range, NodeStyle style)
        {
            Symbol = symbol;
            Args = args;
            Attrs = attrs;
            Range = range;
            Style = style;
        }

        public INode WithSymbol(Symbol symbol) => new CallNode(symbol, Args, Attrs, Range, Style);
        public INode WithArgs(ImmutableList<INode> args) => new CallNode(Symbol, args, Attrs, Range, Style);
        public INode WithAttrs(ImmutableList<INode> attrs) => new CallNode(Symbol, Args, attrs, Range, Style);
        public INode WithRange(CodeRange range) => new CallNode(Symbol, Args, Attrs, range, Style);
        public INode WithStyle(NodeStyle style) => new CallNode(Symbol, Args, Attrs, Range, style);

        public override string ToString()
        {
            NodePrinter.Style(out var builder, this, _ => _StyleNode(_));

            return builder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
