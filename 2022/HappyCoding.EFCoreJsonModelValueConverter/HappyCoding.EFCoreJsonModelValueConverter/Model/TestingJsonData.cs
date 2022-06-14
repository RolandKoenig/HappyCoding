using System.Collections.Immutable;
using HappyCoding.EFCoreJsonModelValueConverter.Util;

namespace HappyCoding.EFCoreJsonModelValueConverter.Model;

public record TestingJsonData
{
    public string DetailField1 { get; init; } = "";

    public string DetailField2 { get; init; } = "";

    public string DetailField3 { get; init; } = "";

    public string DetailField4 { get; init; } = "";

    public string DetailField5 { get; init; } = "";
}