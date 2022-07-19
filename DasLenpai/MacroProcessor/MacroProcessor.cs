using DasLenpai.CodeAnalysis;
using DasLenpai.MacroProcessor.Macros;
using DasLenpai.NodeSystem.Nodes;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace DasLenpai.MacroProcessor
{
    public class MacroProcessor
    {
        private readonly ImmutableDictionary<Symbol, ImmutableList<CallMacro>> CallMacros;
        private readonly ImmutableDictionary<Symbol, ImmutableList<LiteralMacro>> LiteralMacros;
        private readonly ImmutableDictionary<Symbol, ImmutableList<IdentifierMacro>> IdentifierMacros;

        internal MacroProcessor(ImmutableDictionary<Symbol, ImmutableList<CallMacro>> callMacros, ImmutableDictionary<Symbol, ImmutableList<LiteralMacro>> literalMacros, ImmutableDictionary<Symbol, ImmutableList<IdentifierMacro>> identifierMacros)
        {
            CallMacros = callMacros;
            LiteralMacros = literalMacros;
            IdentifierMacros = identifierMacros;
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

            if (result.Kind == NodeKind.Call)
            {
                if (CallMacros.ContainsKey(result.Symbol))
                {
                    result = IterateMacros(result, CallMacros[result.Symbol].Cast<IMacro>());
                }
            }
            else if (result.Kind == NodeKind.Literal)
            {
                if (LiteralMacros.ContainsKey(result.Symbol))
                {
                    result = IterateMacros(result, LiteralMacros[result.Symbol].Cast<IMacro>());
                }
            }
            else if (result.Kind == NodeKind.Identifier)
            {
                if (IdentifierMacros.ContainsKey(result.Symbol))
                {
                    result = IterateMacros(result, IdentifierMacros[result.Symbol].Cast<IMacro>());
                }
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
                if (n.Symbol != node.Symbol)
                {
                    return Process(n);
                }
                else if (m.ReProcess)
                {
                    n = Process(n);
                    if (n.Symbol != node.Symbol)
                    {
                        return Process(n);
                    }
                }
            }
            return n;
        }
    }

    public class MacroProcessorBuilder
    {
        private readonly ImmutableDictionary<Symbol, ImmutableList<CallMacro>.Builder>.Builder CallMacros;
        private readonly ImmutableDictionary<Symbol, ImmutableList<LiteralMacro>.Builder>.Builder LiteralMacros;
        private readonly ImmutableDictionary<Symbol, ImmutableList<IdentifierMacro>.Builder>.Builder IdentifierMacros;

        internal MacroProcessorBuilder()
        {
            CallMacros = ImmutableDictionary.CreateBuilder<Symbol, ImmutableList<CallMacro>.Builder>();
            LiteralMacros = ImmutableDictionary.CreateBuilder<Symbol, ImmutableList<LiteralMacro>.Builder>();
            IdentifierMacros = ImmutableDictionary.CreateBuilder<Symbol, ImmutableList<IdentifierMacro>.Builder>();
        }

        public void AddMacro(IMacro macro)
        {
            if (macro.Kind == MacroKind.Call)
            {
                if (!CallMacros.ContainsKey(macro.Symbol))
                {
                    CallMacros[macro.Symbol] = ImmutableList.CreateBuilder<CallMacro>();
                }
                CallMacros[macro.Symbol].Add((CallMacro)macro);
            }
            else if (macro.Kind == MacroKind.Literal)
            {
                if (!LiteralMacros.ContainsKey(macro.Symbol))
                {
                    LiteralMacros[macro.Symbol] = ImmutableList.CreateBuilder<LiteralMacro>();
                }
                LiteralMacros[macro.Symbol].Add((LiteralMacro)macro);
            }
            else if (macro.Kind == MacroKind.Identifier)
            {
                if (!IdentifierMacros.ContainsKey(macro.Symbol))
                {
                    IdentifierMacros[macro.Symbol] = ImmutableList.CreateBuilder<IdentifierMacro>();
                }
                IdentifierMacros[macro.Symbol].Add((IdentifierMacro)macro);
            }
        }

        public MacroProcessor ToProcessor() => new MacroProcessor(CallMacros.ToImmutableDictionary(_ => _.Key, _ => _.Value.ToImmutable()), LiteralMacros.ToImmutableDictionary(_ => _.Key, _ => _.Value.ToImmutable()), IdentifierMacros.ToImmutableDictionary(_ => _.Key, _ => _.Value.ToImmutable()));
    }
}
