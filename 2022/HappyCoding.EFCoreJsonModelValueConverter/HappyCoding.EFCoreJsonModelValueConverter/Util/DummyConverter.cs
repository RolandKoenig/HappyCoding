using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HappyCoding.EFCoreJsonModelValueConverter.Util
{
    internal class DummyConverter : JsonConverter<ImmutableListWithValueSemantics<int>>
    {


        public override void WriteJson(JsonWriter writer, ImmutableListWithValueSemantics<int>? value, JsonSerializer serializer)
        {
            

        }

        public override ImmutableListWithValueSemantics<int>? ReadJson(JsonReader reader, Type objectType,
            ImmutableListWithValueSemantics<int>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = serializer.Deserialize<int[]>(reader);
            var array = ImmutableArray.Create(result);
            return array.WithValueSemantics();
        }
    }
}
