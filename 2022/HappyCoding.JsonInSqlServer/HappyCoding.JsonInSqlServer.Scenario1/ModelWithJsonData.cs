using System;
using System.ComponentModel.DataAnnotations;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class ModelWithJsonData
    { 
        [StringLength(30)]
        public string ID { get; private set; }

        public DateTimeOffset Timestamp1 { get; private set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset Timestamp2 { get; private set; } = DateTimeOffset.Now;

        public string JsonData { get; private set; }

        private ModelWithJsonData()
        {

        }

        public ModelWithJsonData(string id)
        {
            this.ID = id;
        }
    }
}
