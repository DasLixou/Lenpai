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
            Range = INode.calcRange(args);
        }

        public INode WithArgs(ImmutableList<INode> args) => new ListNode(args);

        public override string ToString()
        {
            return $"ListNode[Count = {Args.Count}]";
        }
    }
}
