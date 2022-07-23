using System.Diagnostics;
using System.Text;

namespace Lenpai.CodeAnalysis
{
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly record struct CodePosition(uint Line, uint Column)
    {
        public static readonly CodePosition Invalid = new CodePosition(0, 0);

        public static bool operator <=(CodePosition left, CodePosition right)
        {
            if (left.Line < right.Line) return true;
            if (left.Line > right.Line) return false;
            return left.Column <= right.Column;
        }

        public static bool operator >=(CodePosition left, CodePosition right)
        {
            if (left.Line > right.Line) return true;
            if (left.Line < right.Line) return false;
            return left.Column >= right.Column;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Line == 0 || Column == 0)
            {
                builder.Append("INVALID");
            }
            else
            {
                builder.Append(Line);
                builder.Append(':');
                builder.Append(Column);
            }
            return builder.ToString();
        }
    }
}
