using DasLenpai.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DasLenpai.NodeSystem.Nodes
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class ListNode : INode
    {
        public NodeKind Kind => NodeKind.List;
        public ImmutableList<INode> Args { get; }
        public CodeRange Range { get; }

        internal ListNode(ImmutableList<INode> args, CodeRange range)
        {
            Args = args;
            Range = range;
        }

        public INode WithArgs(ImmutableList<INode> args) => new ListNode(args, Range);
        public INode WithRange(CodeRange range) => new ListNode(Args, range);

        public override string ToString()
        {
            return $"ListNode[Count = {Args.Count}]";
        }
    }
}
