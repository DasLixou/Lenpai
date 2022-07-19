using DasLenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        internal CallNode(Symbol symbol, ImmutableList<INode> args, ImmutableList<INode> attrs, CodeRange range, NodeStyle style, INode parent, bool convertParents)
        {
            Symbol = symbol;
            Args = convertParents ? args.ConvertAll(_ => _.WithParent(this)) : args;
            Attrs = convertParents ? attrs.ConvertAll(_ => _.WithParent(this)) : attrs;
            Range = range;
            Style = style;
            Parent = parent;
        }

        public INode WithSymbol(Symbol symbol) => new CallNode(symbol, Args, Attrs, Range, Style, Parent, false);
        public INode WithArgs(ImmutableList<INode> args) => _Args(args.ConvertAll(_ => _.WithParent(this)));
        public INode PlusArgs(ImmutableList<INode> args) => _Args(Args.AddRange(args.ConvertAll(_ => _.WithParent(this))));
        public INode PlusArg(INode arg) => _Args(Args.Add(arg.WithParent(this)));
        public INode WithAttrs(ImmutableList<INode> attrs) => _Attrs(attrs.ConvertAll(_ => _.WithParent(this)));
        public INode PlusAttrs(ImmutableList<INode> attrs) => _Attrs(Attrs.AddRange(attrs.ConvertAll(_ => _.WithParent(this))));
        public INode PlusAttr(INode attr) => _Attrs(Attrs.Add(attr.WithParent(this)));
        public INode WithRange(CodeRange range) => new CallNode(Symbol, Args, Attrs, range, Style, Parent, false);
        public INode WithStyle(NodeStyle style) => new CallNode(Symbol, Args, Attrs, Range, style, Parent, false);
        public INode WithParent(INode parent) => new CallNode(Symbol, Args, Attrs, Range, Style, parent, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private INode _Args(ImmutableList<INode> args) => new CallNode(Symbol, args, Attrs, Range, Style, Parent, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private INode _Attrs(ImmutableList<INode> attrs) => new CallNode(Symbol, Args, attrs, Range, Style, Parent, false);

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
