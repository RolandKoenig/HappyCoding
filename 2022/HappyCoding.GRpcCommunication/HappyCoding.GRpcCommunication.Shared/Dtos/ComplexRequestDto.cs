namespace HappyCoding.GRpcCommunication.Shared.Dtos;

public class ComplexRequestDto
{
    public string? MessageId { get; set; }

    public long TimestampTicks { get; set; }

    public string? LocationId { get; set; }

    public int StationId { get; set; }

    public List<DummyRequestItemDto>? DummyItemList { get; set; }
}

public class DummyRequestItemDto
{
    public string? Property1 { get; set; }

    public string? Property2 { get; set; }

    public int Property3 { get; set; }

    public int Property4 { get; set; }

    public string? Property5 { get; set; }

    public long Property6 { get; set; }
}