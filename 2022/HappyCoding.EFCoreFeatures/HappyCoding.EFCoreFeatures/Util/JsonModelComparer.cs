using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HappyCoding.EFCoreFeatures.Util;

public class JsonModelComparer<T> : ValueComparer<JsonModel<T>>
    where T : class
{
    /// <inheritdoc />
    public JsonModelComparer()
        : base((left, right) => (left == right) && (left == null || !left.HasChanges),
            x => x.GetHashCode())
    {

    }

}
