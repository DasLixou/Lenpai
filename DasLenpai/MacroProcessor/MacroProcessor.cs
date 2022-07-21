using DasLenpai.CodeAnalysis;
using DasLenpai.MacroProcessor.Macros;
using DasLenpai.NodeSystem.Nodes;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace DasLenpai.MacroProcessor
{
    public class MacroProcessor
    {
        private readonly ImmutableDictionary<(NodeKind kind, Symbol symbol), ImmutableList<IMacro>> Macros;

        internal MacroProcessor(ImmutableDictionary<(NodeKind kind, Symbol symbol), ImmutableList<IMacro>> macros)
        {
            Macros = macros;
        }

        public ImmutableList<INode> Process(ImmutableList<INode> nodes)
        {
            var result = ImmutableList.CreateBuilder<INode>();

            foreach (var node in nodes)
            {
                result.Add(Process(node));
            }

            return result.ToImmutable();
        }

        public INode Process(INode node)
        {
            INode result = node.WithArgs(Process(node.Args));

            var request = (result.Kind, result.Symbol);
            if (Macros.ContainsKey(request))
            {
                result = IterateMacros(result, Macros[request]);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private INode IterateMacros(INode node, IEnumerable<IMacro> macros)
        {
            var n = node;
            foreach (var m in macros)
            {
                n = m.Callback(n);
                if (m.ReProcess)
                {
                    if (n.Symbol != node.Symbol)
                    {
                        return Process(n);
                    }
                    n = Process(n);
                }
                else if (n.Symbol != node.Symbol)
                {
                    break;
                }
            }
            return n;
        }
    }

    public class MacroProcessorBuilder
    {
        private readonly ImmutableDictionary<(NodeKind kind, Symbol symbol), ImmutableList<IMacro>.Builder>.Builder Macros;

        internal MacroProcessorBuilder()
        {
            Macros = ImmutableDictionary.CreateBuilder<(NodeKind, Symbol), ImmutableList<IMacro>.Builder>();
        }

        public void AddMacro(IMacro macro)
        {
            if (macro.Kinds.HasFlag(MacroKind.Call))
            {
                var request = (NodeKind.Call, macro.Symbol);
                if (!Macros.ContainsKey(request))
                {
                    Macros[request] = ImmutableList.CreateBuilder<IMacro>();
                }
                Macros[request].Add(macro);
            }
            if (macro.Kinds.HasFlag(MacroKind.Literal))
            {
                var request = (NodeKind.Literal, macro.Symbol);
                if (!Macros.ContainsKey(request))
                {
                    Macros[request] = ImmutableList.CreateBuilder<IMacro>();
                }
                Macros[request].Add(macro);
            }
            if (macro.Kinds.HasFlag(MacroKind.Identifier))
            {
                var request = (NodeKind.Identifier, macro.Symbol);
                if (!Macros.ContainsKey(request))
                {
                    Macros[request] = ImmutableList.CreateBuilder<IMacro>();
                }
                Macros[request].Add(macro);
            }
        }

        public MacroProcessor ToProcessor() => new MacroProcessor(Macros.ToImmutableDictionary(_ => _.Key, _ => _.Value.ToImmutable()));
    }
}
