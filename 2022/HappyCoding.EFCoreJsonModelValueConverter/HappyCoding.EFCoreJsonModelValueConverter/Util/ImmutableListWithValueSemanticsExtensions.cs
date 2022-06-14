using System.Collections.Immutable;

namespace HappyCoding.EFCoreJsonModelValueConverter.Util;

// Original code from..
// https://gist.github.com/jhgbrt/4bf2cf7e5c077f7326c8b82160a9c59a

static class ImmutableListWithValueSemanticsExtensions
{
    public static ImmutableListWithValueSemantics<T> WithValueSemantics<T>(this IImmutableList<T> list) => new ImmutableListWithValueSemantics<T>(list);
}