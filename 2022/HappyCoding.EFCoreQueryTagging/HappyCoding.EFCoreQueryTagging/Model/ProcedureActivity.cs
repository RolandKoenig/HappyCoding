namespace HappyCoding.EFCoreQueryTagging.Model;

public class ProcedureActivity
{
    public string ProcessID { get; init; } = null!;

    public long ProcessActivityID { get; init; }

    public DateTime ActivityTimestampUtc { get; init; }

    public int Field1 { get; init; }

    public int Field2 { get; init; }

    public int Field3 { get; init; }

    public string Field4 { get; init; } = null!;

    public string Field5 { get; init; } = null!;

    public int Field6 { get; set; }
}