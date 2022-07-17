using DasLenpai.NodeSystem.Nodes;
using System.Text;

namespace DasLenpai.NodeSystem
{
    public static class NodePrinter
    {
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
