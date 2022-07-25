﻿using Lenpai.CodeAnalysis;
using System.Collections.Immutable;

namespace Lenpai.NodeSystem.Nodes
{
    public interface INode
    {
        public NodeKind Kind { get; }
        public virtual Symbol Symbol { get { return Symbol.Missing; } }
        public virtual ImmutableList<INode> Args { get { return ImmutableList<INode>.Empty; } }
        public virtual ImmutableList<INode> Attrs { get { return ImmutableList<INode>.Empty; } }
        public virtual object? Value { get { return null; } }
        public virtual Symbol Type { get { return Symbol; } }
        public virtual CodeRange Range { get { return CodeRange.Missing; } }
        public virtual NodeStyle Style { get { return NodeStyle.Default; } }

        public virtual void Deconstruct(out string symbol, out ImmutableList<INode> args, out ImmutableList<INode> attrs, out object? value)
        {
            symbol = Symbol.ToString();
            args = Args;
            attrs = Attrs;
            value = Value;
        }

        public virtual INode WithSymbol(Symbol symbol) { return this; }
        public virtual INode WithArgs(ImmutableList<INode> args) { return this; }
        public virtual INode PlusArgs(ImmutableList<INode> args) { return WithArgs(Args.AddRange(args)); }
        public virtual INode PlusArg(INode arg) { return WithArgs(Args.Add(arg)); }
        public virtual INode WithAttrs(ImmutableList<INode> attrs) { return this; }
        public virtual INode PlusAttrs(ImmutableList<INode> attrs) { return WithAttrs(Attrs.AddRange(attrs)); }
        public virtual INode PlusAttr(INode attr) { return WithAttrs(Attrs.Add(attr)); }
        public virtual INode WithValue(object? value) { return this; }
        public virtual INode WithType(Symbol type) { return WithSymbol(type); }
        public virtual INode WithRange(CodeRange range) { return this; }
        public virtual INode WithStyle(NodeStyle style) { return this; }
        public virtual INode PlusStyle(NodeStyle style) { return WithStyle(Style | style); }

        protected static CodeRange calcRange(ImmutableList<INode> args)
        {
            if (!args.Any()) return CodeRange.Missing;
            var range = args[0].Range;
            foreach (var arg in args) { range = range.Expand(arg.Range); }
            return range;
        }
    }

    public enum NodeKind: byte
    {
        Call, Literal, Identifier, List
    }
}
