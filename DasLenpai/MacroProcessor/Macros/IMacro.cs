using DasLenpai.CodeAnalysis;
using DasLenpai.NodeSystem.Nodes;

namespace DasLenpai.MacroProcessor.Macros
{
    public interface IMacro
    {
        public MacroKind Kinds { get; }
        public virtual Symbol Symbol { get { return null; } }
        /// <summary>
        /// When <code>true</code>, the MacroProcessor will process the result node after processing this node
        /// </summary>
        public bool ReProcess { get; }
        public Func<INode, INode> Callback { get; }
    }

    [Flags]
    public enum MacroKind : byte
    {
        Call =       0b001,
        Literal =    0b010,
        Identifier = 0b100
    }
}
