using System;
using System.ComponentModel.DataAnnotations;
using HappyCoding.JsonInSqlServer.JsonModel;

namespace HappyCoding.JsonInSqlServer.Scenario2
{
    public class ModelWithJsonData2
    { 
        [StringLength(30)]
        public string ID { get; private set; }

        public DateTimeOffset Timestamp1 { get; private set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset Timestamp2 { get; private set; } = DateTimeOffset.Now;

        public byte[] JsonData { get; private set; }

        private ModelWithJsonData2()
        {

        }

        public ModelWithJsonData2(string id, JsonRoot jsonRoot)
        {
            this.ID = id;
            this.JsonData = JsonRootSerializer.SerializeToJson(jsonRoot);
        }

        public JsonRoot GetJsonRoot()
        {
            return JsonRootSerializer.DeserializeFromValue(this.JsonData);
        }

        public void SetJsonRoot(JsonRoot jsonRoot)
        {
            this.JsonData = JsonRootSerializer.SerializeToJson(jsonRoot);
        }
    }
}
