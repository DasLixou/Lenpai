using System.Diagnostics;

namespace Lenpai.CodeAnalysis
{
    [DebuggerDisplay("{From} - {To}")]
    public readonly record struct CodeRange(CodePosition From, CodePosition To)
    {
        public static readonly CodeRange Missing = new(CodePosition.Invalid, CodePosition.Invalid);

        public bool Contains(CodeRange other) => From <= other.From && To >= other.To;

        public CodeRange Expand(CodePosition position)
        {
            CodePosition from = position < From ? position : From;
            CodePosition to = position > To ? position : To;
            return new CodeRange(from, to);
        }

        public CodeRange Expand(CodeRange range)
        {
            CodePosition from = (range.From < From || From == CodePosition.Invalid) && range.From != CodePosition.Invalid ? range.From : From;
            CodePosition to = (range.To > To || To == CodePosition.Invalid) && range.To != CodePosition.Invalid ? range.To : To;
            return new CodeRange(from, to);
        }
    }
}
