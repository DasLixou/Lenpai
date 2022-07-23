using Lenpai.NodeSystem.Nodes;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lenpai.NodeSystem
{
    public static class NodePrinter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Style(out StringBuilder builder, INode node, Action<StringBuilder> styleCallback)
        {
            builder = new StringBuilder();

            /* Attributes */
            foreach (var attr in node.Attrs)
            {
                builder.Append("@[");
                builder.Append(attr.ToString());
                builder.Append("] ");
            }

            /* Decorations & StyleCallback */
            var parens = node.Style.HasFlag(NodeStyle.Paren);
            var stmt = node.Style.HasFlag(NodeStyle.Statement);

            if (parens) builder.Append('(');

            styleCallback(builder);

            if (parens) builder.Append(')');
            if (stmt) builder.Append(';');
        }
    }
}
