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
            var singleQuote = node.Style.HasFlag(NodeStyle.SingleQuote);
            var doubleQuote = node.Style.HasFlag(NodeStyle.DoubleQuote);

            if (parens) builder.Append('(');
            if (singleQuote) builder.Append('\'');
            if (doubleQuote) builder.Append('"');

            styleCallback(builder);

            if (singleQuote) builder.Append('\'');
            if (doubleQuote) builder.Append('"');
            if (parens) builder.Append(')');
            if (stmt) builder.Append(';');
        }
    }
}
