using DasLenpai.CodeAnalysis;
using DasLenpai.MacroProcessor.Macros;
using DasLenpai.NodeSystem.Nodes;
using System.Collections.Immutable;

namespace DasLenpai.MacroProcessor
{
    public class MacroProcessor
    {
        private readonly ImmutableDictionary<Symbol, CallMacro> CallMacros;
        private readonly ImmutableDictionary<Symbol, LiteralMacro> LiteralMacros;
        private readonly ImmutableDictionary<Symbol, IdentifierMacro> IdentifierMacros;

        internal MacroProcessor(ImmutableDictionary<Symbol, CallMacro> callMacros, ImmutableDictionary<Symbol, LiteralMacro> literalMacros, ImmutableDictionary<Symbol, IdentifierMacro> identifierMacros)
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

            IMacro macro = null;
            if(result.Kind == NodeKind.Call)
            {
                if(CallMacros.ContainsKey(result.Symbol))
                {
                    macro = CallMacros[result.Symbol];
                }
            } else if(result.Kind == NodeKind.Literal)
            {
                if(LiteralMacros.ContainsKey(result.Symbol))
                {
                    macro = LiteralMacros[result.Symbol];
                }
            } else if(result.Kind == NodeKind.Identifier)
            {
                if(IdentifierMacros.ContainsKey(result.Symbol))
                {
                    macro = LiteralMacros[result.Symbol];
                }
            }

            if(macro != null)
            {
                result = macro.Callback(result);
                if(macro.ReProcess)
                {
                    result = Process(result);
                }
            }

            return result;
        }
    }

    public class MacroProcessorBuilder
    {
        private readonly ImmutableDictionary<Symbol, CallMacro>.Builder CallMacros;
        private readonly ImmutableDictionary<Symbol, LiteralMacro>.Builder LiteralMacros;
        private readonly ImmutableDictionary<Symbol, IdentifierMacro>.Builder IdentifierMacros;

        internal MacroProcessorBuilder()
        {
            CallMacros = ImmutableDictionary.CreateBuilder<Symbol, CallMacro>();
            LiteralMacros = ImmutableDictionary.CreateBuilder<Symbol, LiteralMacro>();
            IdentifierMacros = ImmutableDictionary.CreateBuilder<Symbol, IdentifierMacro>();
        }

        public void AddMacro(IMacro macro)
        {
            if(macro.Kind == MacroKind.Call)
            {
                CallMacros.Add(macro.Symbol, (CallMacro)macro);
            }
            else if (macro.Kind == MacroKind.Literal)
            {
                LiteralMacros.Add(macro.Symbol, (LiteralMacro)macro);
            }
            else if (macro.Kind == MacroKind.Identifier)
            {
                IdentifierMacros.Add(macro.Symbol, (IdentifierMacro)macro);
            }
        }

        public MacroProcessor ToProcessor() => new MacroProcessor(CallMacros.ToImmutable(), LiteralMacros.ToImmutable(), IdentifierMacros.ToImmutable());
    }
}
