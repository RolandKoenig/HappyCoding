using System;
using System.Collections.Generic;

namespace HappyCoding.EFCoreWithBulkExtensions.Model
{
    public class Order
    {
        public Guid ID { get; private set; }

        public DateTimeOffset CreateDate { get; private set; }

        public List<OrderPosition> Positions { get; private set; } = new();

        private Order()
        {

        }

        public Order(Guid id, DateTimeOffset createDate)
        {
            this.ID = id;
            this.CreateDate = createDate;
        }
    }
}
