using System.Diagnostics;

namespace DasLenpai.CodeAnalysis
{
    [DebuggerDisplay("{Name,nq}")]
    public readonly struct Symbol : IEquatable<Symbol>
    {
        public static readonly Symbol Missing = new Symbol("");

        public readonly string Name;

        public Symbol(string name)
        {
            Name = string.Intern(name);
        }

        public static implicit operator Symbol(string value)
        {
            return new Symbol(value);
        }

        public override string ToString() => this != Missing ? Name : "! MISSING !";

        public bool Equals(Symbol other) => ReferenceEquals(Name, other.Name);
        public static bool operator ==(Symbol left, Symbol right) => ReferenceEquals(left.Name, right.Name);
        public static bool operator !=(Symbol left, Symbol right) => !ReferenceEquals(left.Name, right.Name);
        public override bool Equals(object? obj) => obj is Symbol && Equals((Symbol)obj);
        public override int GetHashCode() => Name.GetHashCode();
    }
}
