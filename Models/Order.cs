using System;
using System.Collections.Generic;

namespace Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetail> Details { get; set; }

        public Order()
        {
            Details = new HashSet<OrderDetail>();

            Status = OrderStatus.Ordered;
            OrderDate = DateTime.Now;
        }
    }
}
