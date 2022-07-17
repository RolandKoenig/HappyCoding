using HappyCoding.HexagonalArchitecture.Domain.Model;

namespace HappyCoding.HexagonalArchitecture.Domain.Tests.Model;

public class ProtocolEntryPriorityTests
{
    [Fact]
    public void CompareOperators()
    {
        var priorityHighest = ProtocolEntryPriority.High;
        var priorityHighest2 = ProtocolEntryPriority.High;
        var priorityLowest = ProtocolEntryPriority.Low;

        Assert.True(priorityHighest == priorityHighest2);
        Assert.False(priorityHighest == priorityLowest);
        
        Assert.True(priorityHighest != priorityLowest);
        Assert.False(priorityHighest != priorityHighest2);
        
        Assert.True(priorityHighest > priorityLowest);
        Assert.False(priorityLowest > priorityHighest);
        
        Assert.True(priorityLowest < priorityHighest);
        Assert.False(priorityHighest < priorityLowest);
    }
}