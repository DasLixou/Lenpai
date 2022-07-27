using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Lenpai.Utils
{
    public static class LUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> List<T>(params T[] args) => ImmutableList.CreateRange(args);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T>.Builder Builder<T>() => ImmutableList.CreateBuilder<T>();
    }
}
