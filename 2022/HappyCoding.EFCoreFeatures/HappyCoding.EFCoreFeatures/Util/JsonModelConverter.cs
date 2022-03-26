using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCoding.EFCoreFeatures.Util;

internal class JsonModelConverter<T> : ValueConverter<JsonModel<T>?, byte[]?>
    where T : class
{
    public JsonModelConverter()
        : base(
            model => model != null ? model.RawBytes : null,
            rawBytes => rawBytes != null ? new JsonModel<T>(rawBytes) : null)
    {

    }
}
