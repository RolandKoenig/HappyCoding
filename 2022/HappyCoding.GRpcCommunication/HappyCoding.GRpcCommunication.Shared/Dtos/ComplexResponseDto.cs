namespace HappyCoding.GRpcCommunication.Shared.Dtos;

public class ComplexResponseDto
{
    public string? Message { get; set; }

    public List<DummyResponseItemDto>? DummyItemList { get; set; }
}

public class DummyResponseItemDto
{
    public int Property1 { get; set; }
}