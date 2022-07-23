using System.Diagnostics;

namespace Lenpai.CodeAnalysis
{
    [DebuggerDisplay("{From} - {To}")]
    public readonly record struct CodeRange(CodePosition From, CodePosition To)
    {
        public static readonly CodeRange Missing = new(CodePosition.Invalid, CodePosition.Invalid);

        public bool Contains(CodeRange other) => From <= other.From && To >= other.To;
    }
}
