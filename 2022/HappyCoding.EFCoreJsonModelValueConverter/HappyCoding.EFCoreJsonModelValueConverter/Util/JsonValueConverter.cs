using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCoding.EFCoreJsonModelValueConverter.Util;

internal class JsonValueConverter<TModel> : ValueConverter<TModel, byte[]>
    where TModel : class
{
    public JsonValueConverter()
        : base(
            model => JsonModelSerializer.SerializeJsonModel(model),
            serializedData => JsonModelSerializer.DeserializeJsonModel<TModel>(serializedData),
            null)
    {

    }
}