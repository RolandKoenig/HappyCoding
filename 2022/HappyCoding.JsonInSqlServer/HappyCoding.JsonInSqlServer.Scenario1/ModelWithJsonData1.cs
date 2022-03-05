using System;
using System.ComponentModel.DataAnnotations;
using HappyCoding.JsonInSqlServer.JsonModel;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class ModelWithJsonData1
    { 
        [StringLength(30)]
        public string ID { get; private set; }

        public DateTimeOffset Timestamp1 { get; private set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset Timestamp2 { get; private set; } = DateTimeOffset.Now;

        public bool ReducedPropertySize { get; private set; }

        public string JsonData { get; private set; }

        private ModelWithJsonData1()
        {

        }

        public ModelWithJsonData1(string id, JsonRoot jsonRoot, bool reducedPropertySize)
        {
            this.ID = id;

            this.ReducedPropertySize = reducedPropertySize;
            this.JsonData = JsonRootSerializer.SerializeToJson(jsonRoot, this.ReducedPropertySize);
        }

        public JsonRoot GetJsonRoot()
        {
            return JsonRootSerializer.DeserializeFromValue(this.JsonData, this.ReducedPropertySize);
        }

        public void SetJsonRoot(JsonRoot jsonRoot)
        {
            this.JsonData = JsonRootSerializer.SerializeToJson(jsonRoot, this.ReducedPropertySize);
        }
    }
}
