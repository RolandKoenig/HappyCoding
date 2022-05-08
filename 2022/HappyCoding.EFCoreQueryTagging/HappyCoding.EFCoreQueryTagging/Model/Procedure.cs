namespace HappyCoding.EFCoreQueryTagging.Model;

public class Procedure
{
    public string ID { get; init; }

    public DateTime CreateTimestampUtc { get; init; }

    public int Field1 { get; init; }

    public int Field2 { get; init; }

    public int Field3 { get; init; }

    public string Field4 { get; init; } = null!;

    public string Field5 { get; init; } = null!;

    public  int Field6 { get; set; }

    public List<ProcedureActivity> Activities { get; init; } = null!;
}