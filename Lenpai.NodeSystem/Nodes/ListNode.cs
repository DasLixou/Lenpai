using Lenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Lenpai.NodeSystem.Nodes
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class ListNode : INode
    {
        public NodeKind Kind => NodeKind.List;
        public ImmutableList<INode> Args { get; init; }
        public CodeRange Range { get; init; }

        internal ListNode(ImmutableList<INode> args)
        {
            Args = args;
            Range = calcRange(args);
        }

        public INode WithArgs(ImmutableList<INode> args) => new ListNode(args);

        public override string ToString()
        {
            return $"ListNode[Count = {Args.Count}]";
        }

        private static CodeRange calcRange(ImmutableList<INode> args)
        {
            if (!args.Any()) return CodeRange.Missing;
            CodePosition from = args[0].Range.From;
            CodePosition to = args[0].Range.To;
            for (int i = 1; i < args.Count; i++)
            {
                var node = args[i];
                if (node.Range.From < from)
                {
                    from = node.Range.From;
                }
                else if (node.Range.To > to)
                {
                    to = node.Range.To;
                }
            }
            return new CodeRange(from, to);
        }
    }
}
