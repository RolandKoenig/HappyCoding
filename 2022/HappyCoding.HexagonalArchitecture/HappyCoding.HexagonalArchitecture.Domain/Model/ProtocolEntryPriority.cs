using Light.GuardClauses;

namespace HappyCoding.HexagonalArchitecture.Domain.Model;

public readonly struct ProtocolEntryPriority : IEquatable<ProtocolEntryPriority>
{
    public static readonly ProtocolEntryPriority High = new(2);

    public static readonly ProtocolEntryPriority Normal = new(1);

    public static readonly ProtocolEntryPriority Low = new(0);

    public static readonly IEnumerable<ProtocolEntryPriority> All = new[]
    {
        High,
        Normal,
        Low
    };

    public int Priority { get; }

    public ProtocolEntryPriority(int priorityValue)
    {
        priorityValue.MustBeIn(new Range<int>(0, 2), nameof(priorityValue));

        var test = 1..2;

        this.Priority = priorityValue;
    }

    public override string ToString()
    {
        return this.Priority switch
        {
            2 => nameof(High),
            1 => nameof(Normal),
            0 => nameof(Low),
            _ => throw new DomainException($"Unsupported value for priority: {this.Priority}")
        };
    }

    public override int GetHashCode()
    {
        return Priority;
    }

    public bool Equals(ProtocolEntryPriority other)
    {
        return Priority == other.Priority;
    }

    public override bool Equals(object? obj)
    {
        return obj is ProtocolEntryPriority other && Equals(other);
    }
    
    public static bool operator ==(ProtocolEntryPriority left, ProtocolEntryPriority right)
    {
        return left.Priority == right.Priority;
    }
    
    public static bool operator !=(ProtocolEntryPriority left, ProtocolEntryPriority right)
    {
        return left.Priority != right.Priority;
    }
    
    public static bool operator >(ProtocolEntryPriority left, ProtocolEntryPriority right)
    {
        return left.Priority > right.Priority;
    }
    
    public static bool operator <(ProtocolEntryPriority left, ProtocolEntryPriority right)
    {
        return left.Priority < right.Priority;
    }
}