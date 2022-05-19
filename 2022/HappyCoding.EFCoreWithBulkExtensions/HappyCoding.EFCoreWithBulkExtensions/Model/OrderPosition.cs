using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.EFCoreWithBulkExtensions.Model
{
    public class OrderPosition
    {
        public Order Order { get; private set; }

        public Guid OrderID { get; private set; }

        public int PositionID { get; private set; }

        public string ArticleNumber { get; private set; }

        public double Quantity { get; private set; }

        private OrderPosition()
        {

        }

        public OrderPosition(Order order, int positionID, string articleNumber, double quantity)
        {
            Order = order;
            OrderID = order.ID;
            PositionID = positionID;
            ArticleNumber = articleNumber;
            Quantity = quantity;
        }
    }
}
